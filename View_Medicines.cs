using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmaEasy
{
    public partial class View_Medicines : Form
    {
        string Query;
        DataControler data;
        public View_Medicines()
        {
            InitializeComponent();
            data = new DataControler();
            Medicines_Data_View.ClearSelection();
        }

        private void Medicin_Name_Search_TextChanged(object sender, EventArgs e)
        {
            String Query = @"Select * from MEDICINES Where Medicine_Name Like '" + Medicin_Name_Search.Text + "%' ";
            UploadDataMediciens(Query);
        }

        private void View_Medicines_Load(object sender, EventArgs e)
        {
            Query =
              @"Select * from MEDICINES";
              UploadDataMediciens(Query);
        }
        private void UploadDataMediciens(String Query)
        {
            DataSet set = data.GetData(Query);
            Medicines_Data_View.DataSource = set.Tables[0];
        }

        string MedicineID;
        private void Medicines_Data_View_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                MedicineID = Medicines_Data_View.Rows[e.RowIndex].Cells[1].Value.ToString();

            }
            catch { }
        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure to Delets this Medicine ?", "PharmaEasy", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Query =
                    @"delete from  MEDICINES Where Medicine_Code = '" + Medicines_Data_View.CurrentRow.Cells["Medicine_Code"].Value + "'";
                data.GetData(Query);
                MessageBox.Show("Medicine have been deleted Sucessfuly ", "PharmaEasy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                View_Medicines_Load(this, null);

            }
        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
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
              int nLeftRect, // x-coordinate of upper-left corner
              int nTopRect, // y-coordinate of upper-left corner
              int nRightRect, // x-coordinate of lower-right corner
              int nBottomRect, // y-coordinate of lower-right corner
              int nWidthEllipse, // height of ellipse
              int nHeightEllipse // width of ellipse
             );

            [System.Runtime.InteropServices.DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
            public static extern bool DeleteObject(System.IntPtr hObject);
            [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
            public static extern bool ReleaseCapture();

            [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
            public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);


        }


        private void BACK_BTN_Click(object sender, EventArgs e)
        {
            Pharmacist pharmacist = new Pharmacist();
            pharmacist.Show();
            this.Hide();
        }

        private void Log_Out_Click(object sender, EventArgs e)
        {
            LogInForm logInForm = new LogInForm();
            logInForm.Show();
            this.Hide();
        }


    }
}
