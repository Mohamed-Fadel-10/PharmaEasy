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
    public partial class Profile : Form
    {
        string Query;
        DataControler data;
        DataSet ds;

        public Profile()
        {
            InitializeComponent();
            UserNameLogin.Text = UserData.Username;
            data = new DataControler();
            ds = new DataSet();
        }
      


        private void Profile_Load(object sender, EventArgs e)
        {
             //DateTime.Parse(BirthDateBox.Text);
          Query=@"select * from Users where username = '"+ UserNameLogin.Text+"'";
            ds=data.GetData(Query);
            //DateTime Date;
            IDBox.Text = ds.Tables[0].Rows[0][0].ToString();
            NameBox.Text = ds.Tables[0].Rows[0][1].ToString();
            PhoneBox.Text = ds.Tables[0].Rows[0][2].ToString();
            EmailBox.Text = ds.Tables[0].Rows[0][3].ToString();
            BirthDateBox.Text = DateTime.Parse(ds.Tables[0].Rows[0][6].ToString()).ToString(); ;
            UserNameBox.Text = ds.Tables[0].Rows[0][4].ToString();
            PasswordBox.Text = ds.Tables[0].Rows[0][5].ToString();
            UserRoleCmb.Text = ds.Tables[0].Rows[0][7].ToString();

        }
       

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Backbtn_Click(object sender, EventArgs e)
        {
            AdminDashboard adminDashboard = new AdminDashboard();
            adminDashboard.Show();
            this.Hide();
        }

        private void LogOutbtn_Click(object sender, EventArgs e)
        {
            LogInForm logInForm = new LogInForm();
            logInForm.Show();
            this.Hide();
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                String UpdateQuery
                  = @"Update Users set Name ='" + NameBox.Text + "' , Password = '" + PasswordBox.Text + "' ,  Phone = '" + PhoneBox.Text + "'" +
                  ",Email = '" + EmailBox.Text + "',username ='" + UserNameBox.Text + "', DateOfBirth ='" + BirthDateBox.Text + "', UserRole = '" + UserRoleCmb.Text + "' where ID = '" + IDBox.Text + "'";

                data.Add_Users_to_Database(UpdateQuery);
                MessageBox.Show("Data user Update Successfuly", "PharmaEasy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                IDBox.Clear();
                NameBox.Clear();
                PasswordBox.Clear();
                PhoneBox.Clear();
                EmailBox.Clear();
                UserNameBox.Clear();
                BirthDateBox.Text = "";
                UserRoleCmb.SelectedIndex = -1;
            }
            catch (Exception)
            {
                MessageBox.Show("User Name Is already Used Before ! ", "PharmaEasy", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }
    }
}
