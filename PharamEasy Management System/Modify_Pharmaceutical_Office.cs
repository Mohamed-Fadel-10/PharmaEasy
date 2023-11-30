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
    public partial class Modify_Pharmaceutical_Office : Form
    {
        DataControler data;
        public Modify_Pharmaceutical_Office()
        {
            InitializeComponent();
            data = new DataControler();
        }


        private void Modify_Pharmaceutical_Office_Load(object sender, EventArgs e)
        {
            String Query = @"Select * from Pharmaceutical_Office";
            DataSet Data = data.GetData(Query);
            Pharmaceutical_Data_grv.DataSource = Data.Tables[0];
            Pharmaceutical_Data_grv.ClearSelection();

        }

       

        private void Pharmaceutical_Data_grv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            IDBox.Text = Pharmaceutical_Data_grv.CurrentRow.Cells["ID"].Value.ToString();
            NameBox.Text = Pharmaceutical_Data_grv.CurrentRow.Cells["Name"].Value.ToString();
            EmailBox.Text = Pharmaceutical_Data_grv.CurrentRow.Cells["Email"].Value.ToString();
            AddressBox.Text = Pharmaceutical_Data_grv.CurrentRow.Cells["Address"].Value.ToString();
            PhoneBox.Text = Pharmaceutical_Data_grv.CurrentRow.Cells["Phone"].Value.ToString();

        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            String Query = @"Select * from Pharmaceutical_Office where Name Like '" + SearchBox.Text + "%'";
            DataSet Data = data.GetData(Query);
            Pharmaceutical_Data_grv.DataSource = Data.Tables[0];

        }

        private void Updatebtn_Click(object sender, EventArgs e)
        {
            Pharmaceutical_Data_grv.CurrentRow.Cells["Name"].Value = NameBox.Text;
            Pharmaceutical_Data_grv.CurrentRow.Cells["Email"].Value = EmailBox.Text;
            Pharmaceutical_Data_grv.CurrentRow.Cells["Address"].Value = AddressBox.Text;
            Pharmaceutical_Data_grv.CurrentRow.Cells["Phone"].Value = PhoneBox.Text;

            try
            {
                String UpdateQuery
              = @"Update Pharmaceutical_Office set Name = '" + NameBox.Text + "' , Email = '" + EmailBox.Text + "' ,  Phone = '" + PhoneBox.Text + "' ,  Address ='" + AddressBox.Text + "' where ID = '" + IDBox.Text + "'";

            data.Add_Users_to_Database(UpdateQuery);
            MessageBox.Show("Pharmaceutical_Data Updated Successfuly", "PharmaEasy", MessageBoxButtons.OK, MessageBoxIcon.Information);
            NameBox.Clear();
            IDBox.Clear();
            EmailBox.Clear();
            PhoneBox.Clear();
            AddressBox.Clear();

            }
            catch (Exception)
            {
                MessageBox.Show("User Name Is already Used Before ! ", "PharmaEasy", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            if (Pharmaceutical_Data_grv.SelectedCells.Count > 0)
            {
                int RowsIndex = Pharmaceutical_Data_grv.SelectedCells[0].RowIndex;
                if (MessageBox.Show("Are you sure you want to delete this Pharmaceutical_Office ? ", "PharmaEasy", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Pharmaceutical_Data_grv.Rows.RemoveAt(RowsIndex);
                    String DeleteQuery = @"Delete from Pharmaceutical_Office Where  ID = '" + IDBox.Text + "'";
                    data.Add_Users_to_Database(DeleteQuery);
                    MessageBox.Show("Pharmaceutical_Office deleted successfully ", "PharmaEasy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    NameBox.Clear();
                    IDBox.Clear();
                    EmailBox.Clear();
                    PhoneBox.Clear();
                    AddressBox.Clear();
                }
            }
            else
                MessageBox.Show("No cell is selected.", "PharmaEasy", MessageBoxButtons.OK, MessageBoxIcon.Warning);


        }
        private void Backbtn_Click(object sender, EventArgs e)
        {
            AdminDashboard admin = new AdminDashboard();

            admin.Show();
            this.Hide();
        }
        private void LogOutbtn_Click(object sender, EventArgs e)
        {
            LogInForm logInForm=new LogInForm();
        
           logInForm.Show();
            this.Hide();

        }
    }
}
