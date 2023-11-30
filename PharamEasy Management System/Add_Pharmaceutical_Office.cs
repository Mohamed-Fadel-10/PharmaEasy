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
    public partial class Add_Pharmaceutical_Office : Form
    {
        DataControler data;
        public Add_Pharmaceutical_Office()
        {
            InitializeComponent();
            data = new DataControler();

        }
        private void AddUserBtn_Click(object sender, EventArgs e)
        {
            String Name = NameBox.Text;
            String Email = EmailBox.Text;
            Int64 Phone = Int64.Parse(PhoneBox.Text);
            String Address = AddressBox.Text;
            String Query = (@"Insert into Pharmaceutical_Office (Name,Email,Address,Phone) Values ('" + Name + "','" + Email + "','" + Address + "','" + Phone + "')");

            data.Add_Users_to_Database(Query);
            MessageBox.Show("Pharmaceutical_Office Added Successfuly ");
        }

        private void RestDataUser_Click(object sender, EventArgs e)
        {
            NameBox.Clear();
            EmailBox.Clear();
            PhoneBox.Clear();
            AddressBox.Clear();
        }

        private void Backbtn_Click(object sender, EventArgs e)
        {
            AdminDashboard admin = new AdminDashboard();
            admin.Show();
            this.Hide();

        }

        private void LogOutbtn_Click(object sender, EventArgs e)
        {
            LogInForm logInForm = new LogInForm();
            logInForm.Show();
            this.Hide();

        }

       
    }
}
