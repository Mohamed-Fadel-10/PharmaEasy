using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace PharmaEasy
{
    public partial class Add_New_Medicine : Form
    {
        string Query;
        string[] Types = { "Syrup", "Tablets", "Capsules", "Ampoules" , "Effervescent ","Cream","Gell"};
        string[] Unites = { "Box", "Stripe", "bag", "Ampoule" };
        DataControler data;
        DataSet set;


        public Add_New_Medicine()
        {
            InitializeComponent();
            data = new DataControler();
            set = new DataSet();
        }
        private void Add_New_Medicine_Load(object sender, EventArgs e)
        {
            TypeBox.DataSource = Types;
            UnitBox.DataSource = Unites;
            Expire_Date_Box.BackColor = Color.FromArgb(125, 206, 19);

        }

        private void AddMedicinebtn_Click(object sender, EventArgs e)
        {
            try
            {

                if (CodeBox.Text != "" && NameBox.Text != "" && Manufacturing_Date_Box.Text != "" && Expire_Date_Box.Text != "" && quantityBox.Text != "" && UnitBox.Text != "" && priceBox.Text != "" && TypeBox.Text != "")
                {
                    Int64 Code = Int64.Parse(CodeBox.Text);
                    String Name = NameBox.Text;
                    DateTime Manufacturing_Date = DateTime.Parse(Manufacturing_Date_Box.Text);
                    DateTime Expire_Date = DateTime.Parse(Expire_Date_Box.Text);
                    Int64 Quantity = Int64.Parse(quantityBox.Text);
                    String Unit = UnitBox.Text;
                    float Price = float.Parse(priceBox.Text);
                    String type = TypeBox.Text;

                    Query = @"insert into MEDICINES (Medicine_Code,Medicine_Name,Manufacturing_Date,Expire_Date,Quantity,Unit,Price,Type) 
                values ('" + Code + "','" + Name + "','" + Manufacturing_Date + "','" + Expire_Date + "','" + Quantity + "','" + Unit + "','" + Price + "','" + type + "')";
                    data.Add_Users_to_Database(Query);
                    MessageBox.Show("Medicine Added Sucessfuly", "PharmaEasy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Enter All Data", "PharmaEasy", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Medicine Code Already Used Befor", "PharmaEasy", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

      

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            int cornerRadius = 40; 
            IntPtr ptr = NativeMethods.CreateRoundRectRgn(0, 0, this.Width, this.Height, cornerRadius, cornerRadius);
            panel3.Region = System.Drawing.Region.FromHrgn(ptr);
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
        private void Backbtn_Click(object sender, EventArgs e)
        {
            Pharmacist pharmacist = new Pharmacist();
            pharmacist.Show();
            this.Hide();
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            LogInForm logIn=new LogInForm();
            logIn.Show();
            this.Hide();
        }
    }
}
