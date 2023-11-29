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
    public partial class Pharmacist : Form
    {
        String Query;
        DataSet set;
        DataControler data;
        Int64 Count;
        public Pharmacist()
        {
            InitializeComponent();
            set=new DataSet();
            data=new DataControler();
            Count=0;
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            LogInForm logInForm = new LogInForm();
            logInForm.Show();
            this.Hide();
        }

        private void addmedicinebtn_Click(object sender, EventArgs e)
        {
            Add_New_Medicine medicine = new Add_New_Medicine();
            medicine.Show();
            this.Hide();
        }

        private void Pharmacist_Load(object sender, EventArgs e)
        {
            Query =
                @"Select Count(Medicine_Name) from MEDICINES where Expire_Date >= getdate()";
               set=data.GetData(Query);
            Count = Int64.Parse(set.Tables[0].Rows[0][0].ToString());
            this.chart1.Series["Valid Medicines"].Points.AddXY("Medicine Validity Chart", Count);

            Query =
                @"Select Count(Medicine_Name) from MEDICINES where Expire_Date <= getdate()";
            set = data.GetData(Query);
            Count = Int64.Parse(set.Tables[0].Rows[0][0].ToString());

            this.chart1.Series["Expired Medicines"].Points.AddXY("Medicine Validity Chart", Count);

           String Query1 =
                @"Select COUNT(*) from MEDICINES ";
            int MediciensNum = data.GetScalerFunctios(Query1);
            MNum.Text=MediciensNum.ToString();
           String Query2 =
                @"Select COUNT(*) FROM MEDICINES WHERE Expire_Date >= getdate()";
            int Valid_Mediciens_Num = data.GetScalerFunctios(Query2);
            Valid_Medicines.Text = Valid_Mediciens_Num.ToString();
          String  Query3 =
                @"SELECT COUNT(*) FROM MEDICINES WHERE Expire_Date <= getdate()";
            int EX_Mediciens_Num = data.GetScalerFunctios(Query3);
            Ex_Medicines.Text = EX_Mediciens_Num.ToString();
           String Query4 =
                @"SELECT  COUNT(*) FROM MEDICINES WHERE Quantity = 0 ";
            int  Deficiencies_Mediciens_Num = data.GetScalerFunctios(Query4);
            Drug_deficiencies.Text = Deficiencies_Mediciens_Num.ToString();



        }


        private void modifynedicinebtn_Click(object sender, EventArgs e)
        {
            Modify_Medicines modify =new Modify_Medicines();
            modify.Show();
            this.Hide();

        }

        private void view_medicines_Click(object sender, EventArgs e)
        {
            View_Medicines view =new View_Medicines ();
            view.Show ();
            this.Hide();
        }

        private void salemedicinebtn_Click(object sender, EventArgs e)
        {
            Sell_Medicine sell = new Sell_Medicine();
            sell.Show();
            this.Hide();
        }

        private void checkmedicine_Click(object sender, EventArgs e)
        {
            Check_Medicine check = new Check_Medicine();
            check.Show();
            this.Hide();
        }

   

    }
}
