using System;
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
    public partial class AddNewUser : Form
    {
        DataControler data = new DataControler();
        private string Text;
        private int len = 0;
        public AddNewUser()
        {
            InitializeComponent();
        }

        private void AddNewUser_Load(object sender, EventArgs e)
        {
            UserRoleCmb.Items.Add("Admin");
            UserRoleCmb.Items.Add("Pharmacist");
            Text = labelText.Text;
            labelText.Text = "";
            
        }

     

        private void RestDataUser_Click(object sender, EventArgs e)
        {
            UserNameBox.Clear();
            UserPasswordBox.Clear();
            PhoneBox.Clear();
            EmailBox.Clear();
            NameBox.Clear();
            DOBBox.Text = string.Empty;

        }

        private void AddUserBtn_Click(object sender, EventArgs e)
        {
            String name = NameBox.Text;
            String Password = UserPasswordBox.Text;
            Int64 Phone = Int64.Parse(PhoneBox.Text);
            String Email = EmailBox.Text;
            String UserName = UserNameBox.Text;
            String DateOfBirth = DOBBox.Text;
            String UserRole = UserRoleCmb.Text;
            try
            {
                string Query = (@"insert into Users (Name,Phone,Email,username,Password,DateOfBirth,UserRole) values('" + name + "','" + Phone + "','" + Email + "','" + UserName + "','" + Password + "','" + DateOfBirth + "','" + UserRole + "')");

                data.Add_Users_to_Database(Query);
                MessageBox.Show(" User Added Successfuly ");

            }
            catch (Exception)
            {
                MessageBox.Show("Error User Name is already useed Before", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
     
        private void UserNameBox_TextChanged(object sender, EventArgs e)
        {
            String Query = @"select * from Users where username = '" + UserNameBox.Text + "'";
            DataSet dataSet = data.GetData(Query);
            if (dataSet.Tables[0].Rows.Count == 0)
            {
                ValidateUsername.ImageLocation = @"D:\PharmaEasy\Photos\yes.png";
            }
            else
                ValidateUsername.ImageLocation = @"D:\PharmaEasy\Photos\no.png""";
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
        private void Bachbtn_Click(object sender, EventArgs e)
        {
            AdminDashboard admin = new AdminDashboard();
            admin.Show();
            this.Hide();

        }
        private void logoutbtn_Click(object sender, EventArgs e)
        {
            LogInForm logInForm = new LogInForm();
            this.Hide();
            logInForm.Show();
        }


    }
}

