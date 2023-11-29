using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmaEasy
{
    public partial class Check_Medicine : Form
    {
        String Query;
        DataControler data;
        public Check_Medicine()
        {
            InitializeComponent();
            data= new DataControler();
        }

        private void txtCheckMedicine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtCheckMedicine.SelectedIndex == 0)
            {
                Query =
                    @"SELECT * FROM MEDICINES WHERE Expire_Date >= getdate()";
                 DataSet set= data.GetData(Query);
                Medicine_Data.DataSource = set.Tables[0];
                setData(Query, "Valid Medicines", Color.DarkBlue);


            }
            else if (txtCheckMedicine.SelectedIndex ==1)
            {
                Query =
                   @"SELECT * FROM MEDICINES WHERE Expire_Date <= getdate()";
                setData(Query, "Expired Medicines", Color.Red);

            }
            else if(txtCheckMedicine.SelectedIndex == 2)
            {
                Query =
                   @"SELECT * FROM MEDICINES";
                setData(Query, "All Medicines", Color.Black);
            }
           
        }
        private void setData(String query, string CheckName, Color color)
        {
          DataSet set=  data.GetData(query);
            Medicine_Data.DataSource = set.Tables[0];
            MedicineChecklbl1.Text = CheckName;
            MedicineChecklbl1.ForeColor = color;
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
