using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PharmaEasy.Add_New_Medicine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace PharmaEasy
{
    public partial class Sell_Medicine : Form
    {
        String Query;
        DataControler data;
        DataSet Data;
        string[] Types = { "Syrup", "Tablets", "Capsules","Ampoules", "Effervescent","Cream","Gel" };
        string[] Unites = { "Tablet", "Bottle", "Ampoule", "bag"};
        public Sell_Medicine()
        {
            InitializeComponent();
            data = new DataControler();
        }
        private void Sell_Medicine_Load(object sender, EventArgs e)
        {

            string query = "SELECT * FROM MEDICINES WHERE Expire_Date >= GETDATE() AND Quantity > 0";
            Data = data.GetData(query);
            All_Medicines.DataSource = Data.Tables[0];
            UnitBox.DataSource = Unites;
            All_Medicines.Columns[2].Visible = true;
            All_Medicines.Columns[1].Visible = false;
            All_Medicines.Columns[6].Visible = false;
            All_Medicines.Columns[4].Visible = false;
            All_Medicines.Columns[7].Visible = false;
            All_Medicines.Columns[5].Visible = false;
            All_Medicines.Columns[8].Visible = false;
            All_Medicines.Columns[0].Visible = false;
            All_Medicines.Columns[3].Visible = false;
            mType.DataSource = Types;
            UnitBox.SelectedIndex = -1;
            mType.SelectedIndex = -1;


        }
        public Double Sum()
        {
            Double sum = 0;

            for (int i = 0; i < Items.Rows.Count; i++)
            {
                sum += Convert.ToDouble(Items.Rows[i].Cells[6].Value);
            }
            return sum;

        }

       
      


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            string query = "SELECT * FROM MEDICINES WHERE Medicine_Name LIKE '" + textBox1.Text + "%' AND  Quantity > 0";
            Data = data.GetData(query);
            All_Medicines.DataSource = Data.Tables[0];

        }


        class ClsPrint
    {
        #region Variables

        int iCellHeight = 0; //Used to get/set the datagridview cell height
        int iTotalWidth = 0; //
        int iRow = 0;//Used as counter
        bool bFirstPage = false; //Used to check whether we are printing first page
        bool bNewPage = false;// Used to check whether we are printing a new page
        int iHeaderHeight = 0; //Used for the header height
        StringFormat strFormat; //Used to format the grid rows.
        ArrayList arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
        ArrayList arrColumnWidths = new ArrayList();//Used to save column widths
        private PrintDocument _printDocument = new PrintDocument();
        private DataGridView gw = new DataGridView();
        private string _ReportHeader;

        #endregion

        public ClsPrint(DataGridView gridview, string ReportHeader)
        {
            _printDocument.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            _printDocument.BeginPrint += new PrintEventHandler(printDocument1_BeginPrint);
            gw = gridview;
            _ReportHeader = ReportHeader;
        }

        public void PrintForm()
        {
            ////Open the print dialog
            //PrintDialog printDialog = new PrintDialog();
            //printDialog.Document = _printDocument;
            //printDialog.UseEXDialog = true;

            ////Get the document
            //if (DialogResult.OK == printDialog.ShowDialog())
            //{
            //    _printDocument.DocumentName = "Test Page Print";
            //    _printDocument.Print();
            //}

            //Open the print preview dialog
            PrintPreviewDialog objPPdialog = new PrintPreviewDialog();
            objPPdialog.Document = _printDocument;
            objPPdialog.ShowDialog();
        }
            private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
            {
                //try
                //{
                //Set the left margin
                int iLeftMargin = e.MarginBounds.Left;
                //Set the top margin
                int iTopMargin = e.MarginBounds.Top;
                //Whether more pages have to print or not
                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;

                //For the first page to print set the cell width and header height
                if (bFirstPage)
                {
                    foreach (DataGridViewColumn GridCol in gw.Columns)
                    {
                        iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                            (double)iTotalWidth * (double)iTotalWidth *
                            ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                        iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                            GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                        // Save width and height of headers
                        arrColumnLefts.Add(iLeftMargin);
                        arrColumnWidths.Add(iTmpWidth);
                        iLeftMargin += iTmpWidth;
                    }
                }
                //Loop till all the grid rows not get printed
                while (iRow <= gw.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = gw.Rows[iRow];
                    //Set the cell height
                    iCellHeight = GridRow.Height + 5;
                    int iCount = 0;
                    //Check whether the current page settings allows more rows to print
                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                    else
                    {

                        if (bNewPage)
                        {
                            //Draw Header
                            e.Graphics.DrawString(_ReportHeader,
                                new Font(gw.Font, FontStyle.Bold),
                                Brushes.Black, e.MarginBounds.Left,
                                e.MarginBounds.Top - e.Graphics.MeasureString(_ReportHeader,
                                new Font(gw.Font, FontStyle.Bold),
                                e.MarginBounds.Width).Height - 4);

                            String strDate = "";
                            //Draw Date
                            e.Graphics.DrawString(strDate,
                                new Font(gw.Font, FontStyle.Bold), Brushes.Black,
                                e.MarginBounds.Left +
                                (e.MarginBounds.Width - e.Graphics.MeasureString(strDate,
                                new Font(gw.Font, FontStyle.Bold),
                                e.MarginBounds.Width).Width),
                                e.MarginBounds.Top - e.Graphics.MeasureString(_ReportHeader,
                                new Font(new Font(gw.Font, FontStyle.Bold),
                                FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            DataGridViewColumn[] _GridCol = new DataGridViewColumn[gw.Columns.Count];
                            int colcount = 0;
                            //Convert ltr to rtl
                            foreach (DataGridViewColumn GridCol in gw.Columns)
                            {
                                _GridCol[colcount++] = GridCol;
                            }
                            for (int i = (_GridCol.Count() - 1); i >= 0; i--)
                            {
                                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawRectangle(Pens.Black,
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawString(_GridCol[i].HeaderText,
                                    _GridCol[i].InheritedStyle.Font,
                                    new SolidBrush(_GridCol[i].InheritedStyle.ForeColor),
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                iCount++;
                            }
                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
                        }
                        iCount = 0;
                        DataGridViewCell[] _GridCell = new DataGridViewCell[GridRow.Cells.Count];
                        int cellcount = 0;
                        //Convert ltr to rtl
                        foreach (DataGridViewCell Cel in GridRow.Cells)
                        {
                            _GridCell[cellcount++] = Cel;
                        }
                        //Draw Columns Contents                
                        for (int i = (_GridCell.Count() - 1); i >= 0; i--)
                        {
                            if (_GridCell[i].Value != null)
                            {
                                e.Graphics.DrawString(_GridCell[i].FormattedValue.ToString(),
                                    _GridCell[i].InheritedStyle.Font,
                                    new SolidBrush(_GridCell[i].InheritedStyle.ForeColor),
                                    new RectangleF((int)arrColumnLefts[iCount],
                                    (float)iTopMargin,
                                    (int)arrColumnWidths[iCount], (float)iCellHeight),
                                    strFormat);
                            }
                            //Drawing Cells Borders 
                            e.Graphics.DrawRectangle(Pens.Black,
                                new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                (int)arrColumnWidths[iCount], iCellHeight));
                            iCount++;
                        }
                    }
                    iRow++;
                    iTopMargin += iCellHeight;
                }
                //If more lines exist, print another page.
                if (bMorePagesToPrint)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
                //}
                //catch (Exception exc)
                //{
                //    MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK,
                //       MessageBoxIcon.Error);
                //}

            }

      

            private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
            {
                try
                {
                    strFormat = new StringFormat();
                    strFormat.Alignment = StringAlignment.Center;
                    strFormat.LineAlignment = StringAlignment.Center;
                    strFormat.Trimming = StringTrimming.EllipsisCharacter;

                    arrColumnLefts.Clear();
                    arrColumnWidths.Clear();
                    iCellHeight = 0;
                    iRow = 0;
                    bFirstPage = true;
                    bNewPage = true;

                    // Calculating Total Widths
                    iTotalWidth = 0;
                    foreach (DataGridViewColumn dgvGridCol in gw.Columns)
                    {
                        iTotalWidth += dgvGridCol.Width;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        
    }




        private void PrintBtn_Click(object sender, EventArgs e)
        {
            //printPreviewDialog1.Document = printDocument1;
            //printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            //printPreviewDialog1.ShowDialog();
            ClsPrint _ClsPrint = new ClsPrint(Items, "header doc text");
            _ClsPrint.PrintForm();

        }
      

     private void Removebtn_Click(object sender, EventArgs e)
{
    if (Items.SelectedCells.Count > 0)
    {
        int RowsIndex = Items.SelectedCells[0].RowIndex;

        if (MessageBox.Show("Are you sure you want to delete this MEDICINE? ", "PharmaEasy", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            try
            {
                if (RowsIndex != null)
                {
                    double removedItemPrice = double.Parse(Items.CurrentRow.Cells["Price"].Value.ToString());

                    
                    double currentTotalPrice = double.Parse(totalprice.Text);
                    currentTotalPrice -= removedItemPrice;
                    totalprice.Text = currentTotalPrice.ToString();

                   
                    Items.Rows.RemoveAt(RowsIndex);

                    Query = @"SELECT Quantity from MEDICINES WHERE Medicine_Code = '" + CodeBox.Text + "'";
                    Int64 set = data.GetScalerFunctios(Query);
                    Int64 CurrentQuantity = Int64.Parse(Items.CurrentRow.Cells["_quantity"].Value.ToString());
                    Int64 updateQuantity = set + CurrentQuantity;

                    Query = @"UPDATE MEDICINES SET Quantity = '" + updateQuantity + "' WHERE Medicine_Code = '" + CodeBox.Text + "'";
                    data.Add_Users_to_Database(Query);

                    MessageBox.Show("Medicine deleted successfully ", "PharmaEasy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No Cells Are Found ! ", "PharmaEasy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
    else
    {
        MessageBox.Show("No cell is selected.", "PharmaEasy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}

        private void All_Medicines_CellClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            IDBox.Text = All_Medicines.CurrentRow.Cells["Medicine_ID"].Value.ToString();
            CodeBox.Text = All_Medicines.CurrentRow.Cells["Medicine_Code"].Value.ToString();
            NameBox.Text = All_Medicines.CurrentRow.Cells["Medicine_Name"].Value.ToString();
            Expire_Date_Box.Text = All_Medicines.CurrentRow.Cells["Expire_Date"].Value.ToString();
            Quantity.Text = All_Medicines.CurrentRow.Cells["Quantity"].Value.ToString();
            UnitBox.Text = All_Medicines.CurrentRow.Cells["Unit"].Value.ToString();
            PriceBox.Text = All_Medicines.CurrentRow.Cells["Price"].Value.ToString();
            mType.Text = All_Medicines.CurrentRow.Cells["Type"].Value.ToString();
            Quantity.Text = "1";
            Quantity.Focus();
        }
       

        private void Add_To_Cart_Click(object sender, EventArgs e)
        {
            String query2;
            try
            {
                
                Int64 enteredQuantity = Int64.Parse(Quantity.Text);
                query2 = @"SELECT Quantity from MEDICINES WHERE Medicine_Code = '" + CodeBox.Text + "'";
                Int64 set = data.GetScalerFunctios(query2);

                if (enteredQuantity > 0 )
                {
                    
                    if (set >= enteredQuantity)
                    {
                        Int64 currentQuantity = set - enteredQuantity;
                        query2 = @"UPDATE MEDICINES SET Quantity = '" + currentQuantity + "' WHERE Medicine_Code = '" + CodeBox.Text + "'";
                        data.Add_Users_to_Database(query2);
                        Int64 ID = Int64.Parse(IDBox.Text);
                        Int64 Code = Int64.Parse(CodeBox.Text);
                        String Name = NameBox.Text;
                        String type = mType.Text;
                        String unit = UnitBox.Text;
                        DateTime Edate = DateTime.Parse(Expire_Date_Box.Text);
                        Int64 quantity = enteredQuantity;
                        float price = float.Parse(PriceBox.Text) * quantity;
                        DateTime date = DateTime.Now;
                        if (CodeBox.Text != ""&& IDBox.Text!=""&& NameBox.Text!=""&& Quantity.Text!=""&& Expire_Date_Box.Text!=""&& PriceBox.Text!=""&& UnitBox.Text!=""&& mType.Text != "")
                        {
                            Items.Rows.Add(ID, Code, Name, type, unit, Edate, price, quantity, date);

                            totalprice.Text = Sum().ToString();
                            Items.ClearSelection();
                        }
                       

                    }
                    else
                    {
                        MessageBox.Show($"The Quantity of {NameBox.Text} in stock less than Quantity You Want , in stock : {set}", "PharmaEasy", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);

                    }
                }
                else
                {
                   
                   
                  MessageBox.Show($"Quantity for {NameBox.Text} is less than the Quantity you want. ", "PharmaEasy", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                    
                }
        }
            catch (Exception)
            {
                MessageBox.Show("Quantity section is not allowed to be empty!", "PharmaEasy", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
            }
}

        private void Savebtn_Click(object sender, EventArgs e)
        {
            String query, query2;
            Int64 enteredQuantity = Int64.Parse(Quantity.Text);

            if (Items.Rows != null)
            {
                if (Items.Rows.Count > 0)
                {
                    decimal totalPriceValue = decimal.Parse(totalprice.Text);

                    for (int i = 0; i < Items.Rows.Count-1; i++)
                    {
                       
                        query =
                        @"INSERT INTO FATURA (Medicine_ID,CODE,NAME,TYPE,UNIT,EXPIRE_DATE,PRICE,QUANTITY,TIME) VALUES ('" + Int64.Parse(Items.Rows[i].Cells["ID"].Value.ToString()) + "','" + Int64.Parse(Items.Rows[i].Cells["Code"].Value.ToString()) + "','" + Items.Rows[i].Cells["Name"].Value + "','" + Items.Rows[i].Cells["Type"].Value + "','" + Items.Rows[i].Cells["_unit"].Value + "' ,'" + DateTime.Parse(Items.Rows[i].Cells["Edate"].Value.ToString()) + "','" + float.Parse(Items.Rows[i].Cells["Price"].Value.ToString()) + "','" + Int64.Parse(Items.Rows[i].Cells["_quantity"].Value.ToString()) + "','" + DateTime.Parse(Items.Rows[i].Cells["Time"].Value.ToString()) + "' )";
                        data = new DataControler();
                        data.Add_Users_to_Database(query);
                        

                    }
                    query =
                        @"Insert INTO FATURA (Total_Fatura_price) values(" + totalPriceValue + ")";
                    data.Add_Users_to_Database(query);
                   

                    MessageBox.Show("Fatura Has Been Saved Successfuly ", "PharmaEasy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Items.Rows.Clear();
                    totalprice.Text = "";
                    
                }
            }
        }

        private void Items_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Items.Rows.Count > 0)
                {
                    CodeBox.Text = Items.CurrentRow.Cells["Code"].Value.ToString();
                    NameBox.Text = Items.CurrentRow.Cells["Name"].Value.ToString();
                    Expire_Date_Box.Text = Items.CurrentRow.Cells["Edate"].Value.ToString();
                    Quantity.Text = Items.CurrentRow.Cells["_quantity"].Value.ToString();
                    UnitBox.Text = Items.CurrentRow.Cells["_unit"].Value.ToString();
                    PriceBox.Text = Items.CurrentRow.Cells["Price"].Value.ToString();
                    mType.Text = Items.CurrentRow.Cells["Type"].Value.ToString();
                }
                else
                {
                    Items.CurrentRow.Cells["Code"].Value=0;
                    Items.CurrentRow.Cells["Name"].Value = "";
                    Items.CurrentRow.Cells["Edate"].Value = "9/9/2020";
                    Items.CurrentRow.Cells["_quantity"].Value = 0;
                    Items.CurrentRow.Cells["_unit"].Value = "";
                    Items.CurrentRow.Cells["Price"].Value = 0;
                    Items.CurrentRow.Cells["Type"].Value = "";

                }
            }
            catch (Exception ) {
                MessageBox.Show("There is no Items in This List To Remove It ", "PharmaEasy", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            int cornerRadius = 40; 
            IntPtr ptr = NativeMethods.CreateRoundRectRgn(0, 0, this.Width, this.Height, cornerRadius, cornerRadius);
            panel2.Region = System.Drawing.Region.FromHrgn(ptr);
            NativeMethods.DeleteObject(ptr);
        }
        public class NativeMethods
        {
            [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
            public static extern System.IntPtr CreateRoundRectRgn
             (
              int nLeftRect, 
              int nTopRect, 
              int nRightRect, 
              int nBottomRect, 
              int nWidthEllipse, 
              int nHeightEllipse 
             );

            [System.Runtime.InteropServices.DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
            public static extern bool DeleteObject(System.IntPtr hObject);
            [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
            public static extern bool ReleaseCapture();

            [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
            public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        }

        private void Backbtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Want To Save Fatura And Exit ?","PharmaEasy",MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==DialogResult.Yes) {
                Savebtn_Click(sender, e);
                Pharmacist pharmacist = new Pharmacist();
                pharmacist.Show();
                this.Hide();
            } 
            else
            {
                Pharmacist pharmacist = new Pharmacist();
                pharmacist.Show();
                this.Hide();
            }
           
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            LogInForm logInForm = new LogInForm();
            logInForm.Show();
            this.Hide();
        }

       
    }
}


