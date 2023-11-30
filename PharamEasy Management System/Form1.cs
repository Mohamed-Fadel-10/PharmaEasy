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
    public partial class LogInForm : Form
    {
        DataControler data = new DataControler();
        String Query;
        DataSet set;
        AdminDashboard adminDashboard;
        Profile profile;


        public LogInForm()
        {
            InitializeComponent();
            adminDashboard = new AdminDashboard();
            profile= new Profile();
            

        }


        private void SignIn_Click(object sender, EventArgs e)
        {
            if (UserName.Text == "admin".ToLower() && Password.Text == "admin".ToLower())
            {
                adminDashboard.Show();
                this.Hide();
            }
            else
            {
                Query = @"Select * from Users where username = '" + UserName.Text + "' and Password = '" + Password.Text + "'";
                set = data.GetData(Query);
                if (set.Tables[0].Rows.Count != 0)
                {
                    String UserRole = set.Tables[0].Rows[0][7].ToString();
                    if (UserRole == "Admin")
                    {
                        UserData.Username = UserName.Text; // Store the username in UserData
                        adminDashboard = new AdminDashboard();
                        adminDashboard.Show();
                        this.Hide();
                    }
                    else
                    {
                        Pharmacist pharmacist = new Pharmacist();
                        pharmacist.Show();
                        this.Hide();
                    }
                    
                }

                else
                {
                    if (set.Tables[0].Columns[4].ToString() != UserName.Text )
                    {
                        UserNameError. Visible = true;  
                        
                    }
                   else if( set.Tables[0].Columns[5].ToString() != Password.Text)
                    {
                      PasswordError.Visible = true;
                    }
                   

                }
            }
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            UserName.Clear();   
            Password.Clear();
           
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
    }
}
