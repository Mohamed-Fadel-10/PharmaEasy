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
    public partial class Modify_Users : Form
    {
        private BindingSource bindingSource;
        DataControler data = new DataControler();
        public Modify_Users()
        {
            InitializeComponent();
        }

        private void Modify_Users_Load(object sender, EventArgs e)
        {
           Add_Pharmaceutical_Office office = new Add_Pharmaceutical_Office();
            string Query = @"Select * from Users ";
            DataSet Data = data.GetData(Query);
            dataGridView1.DataSource = Data.Tables[0];
            UserRoleBox.Items.Add("Admin");
            UserRoleBox.Items.Add("Pharmacist");
            dataGridView1.ClearSelection();
        }

        private void Backbtn_Click(object sender, EventArgs e)
        {
            AdminDashboard admin = new AdminDashboard();
            this.Hide();
            admin.Show();
        }

        private void Updatebtn_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentRow.Cells["Name"].Value = NameBox.Text;
            dataGridView1.CurrentRow.Cells["Password"].Value = PasswordBox.Text;
            dataGridView1.CurrentRow.Cells["Phone"].Value = PhoneBox.Text;
            dataGridView1.CurrentRow.Cells["Email"].Value = EmailBox.Text;
            dataGridView1.CurrentRow.Cells["username"].Value = UsernameBox.Text;
            dataGridView1.CurrentRow.Cells["DateOfBirth"].Value = DobBox.Text;
            dataGridView1.CurrentRow.Cells["UserRole"].Value = UserRoleBox.Text;
            try
            {
                String UpdateQuery
                  = @"Update Users set Name ='" + NameBox.Text + "' , Password = '" + PasswordBox.Text + "' ,  Phone = '" + PhoneBox.Text + "'" +
                  ",Email = '" + EmailBox.Text + "',username ='" + UsernameBox.Text + "', DateOfBirth ='" + DobBox.Text + "', UserRole = '" + UserRoleBox.Text + "' where ID = '" + UserIDBox.Text + "'";

                data.Add_Users_to_Database(UpdateQuery);
                MessageBox.Show("Data user Update Successfuly", "PharmaEasy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UserIDBox.Clear();
                NameBox.Clear();
                PasswordBox.Clear();
                PhoneBox.Clear();
                EmailBox.Clear();
                UsernameBox.Clear();
                DobBox.Text = "";
                UserRoleBox.SelectedIndex = -1;
            }
            catch (Exception)
            {
                MessageBox.Show("User Name Is already Used Before ! ", "PharmaEasy", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int RowsIndex = dataGridView1.SelectedCells[0].RowIndex;
                if (MessageBox.Show("Are you sure you want to delete this user? ", "PharmaEasy", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        if (RowsIndex != null)
                        {
                            dataGridView1.Rows.RemoveAt(RowsIndex);
                            String DeleteQuery = @"Delete from Users Where  ID = '" + UserIDBox.Text + "'";
                            data.Add_Users_to_Database(DeleteQuery);
                            MessageBox.Show("User deleted successfully ", "PharmaEasy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            UserIDBox.Clear();
                            NameBox.Clear();
                            PasswordBox.Clear();
                            PhoneBox.Clear();
                            EmailBox.Clear();
                            UsernameBox.Clear();
                            DobBox.Text = "";
                            UserRoleBox.SelectedIndex = -1;
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("No Cells Are Found ! ", "PharmaEasy", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("No cell is selected.", "PharmaEasy", MessageBoxButtons.OK, MessageBoxIcon.Warning);


            }
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {

            String Query = @"Select * from Users Where username Like '" + SearchBox.Text + "%' ";
            DataSet Data = data.GetData(Query);
            dataGridView1.DataSource = Data.Tables[0];
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            UserIDBox.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
            NameBox.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
            PasswordBox.Text = dataGridView1.CurrentRow.Cells["Password"].Value.ToString();
            PhoneBox.Text = dataGridView1.CurrentRow.Cells["Phone"].Value.ToString();
            EmailBox.Text = dataGridView1.CurrentRow.Cells["Email"].Value.ToString();
            UsernameBox.Text = dataGridView1.CurrentRow.Cells["username"].Value.ToString();
            DobBox.Text = dataGridView1.CurrentRow.Cells["DateOfBirth"].Value.ToString();
            UserRoleBox.Text = dataGridView1.CurrentRow.Cells["UserRole"].Value.ToString();
        }

        private void LogOutbtn_Click(object sender, EventArgs e)
        {
            LogInForm logInForm = new LogInForm();
            this.Hide();
            logInForm.Show();

        }

        private void Backbtn_Click_1(object sender, EventArgs e)
        {
            AdminDashboard admin = new AdminDashboard();
            admin.Show();
            this.Hide();
        }

        
    }
}
