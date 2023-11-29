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
    public partial class Modify_Medicines : Form
    {
        string Query;
        DataControler data;
        DataSet set;
        Int64 Total_Quantity;

        public Modify_Medicines()
        {
            InitializeComponent();
            data=new DataControler();
            set = new DataSet();
        }

       

        private void Searchbtn_Click(object sender, EventArgs e)
        {
            if (SearchBox.Text != "")
            {
                Query =
                       @"Select * From MEDICINES WHERE Medicine_Code = '" + SearchBox.Text + "'";
                set = data.GetData(Query);
                if (set.Tables[0].Rows.Count != 0) 
                {
                    IDBox.Text = set.Tables[0].Rows[0][0].ToString();
                    CodeBox.Text = set.Tables[0].Rows[0][1].ToString();
                    NameBox.Text = set.Tables[0].Rows[0][2].ToString();
                    Manufacturing_Date.Text = set.Tables[0].Rows[0][3].ToString();
                    EX_DateBox.Text = set.Tables[0].Rows[0][4].ToString();
                    QuantityBox.Text = set.Tables[0].Rows[0][5].ToString();
                    UnitBox.Text = set.Tables[0].Rows[0][6].ToString();
                    PriceBox.Text = set.Tables[0].Rows[0][7].ToString();
                    TypeBox.Text = set.Tables[0].Rows[0][8].ToString();
                }
                else
                {
                    MessageBox.Show("No Medicine with  ID : " + SearchBox.Text + " exitst.", "PharmaEasy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                ClearAll();
            }

        }
        private void ClearAll()
        {
            IDBox.Clear();
            CodeBox.Clear();
            NameBox.Clear();
            Manufacturing_Date.Text = "";
            EX_DateBox.Text = "";
            QuantityBox.Clear();
            UnitBox.SelectedIndex = -1;
            PriceBox.Clear();
            TypeBox.SelectedIndex = -1;
            if (addquantityBox.Text != "0")
            {
                addquantityBox.Text = "0";
            }
            else
            {
                addquantityBox.Text = "0";

            }
        }

        
        private void Updatebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (NameBox.Text != "" && Manufacturing_Date.Text != "" && EX_DateBox.Text != "" && UnitBox.Text != "" && QuantityBox.Text != "" && PriceBox.Text != "" && TypeBox.Text != "")
                {
                    Int64 Code = Int64.Parse(CodeBox.Text);
                    String Name = NameBox.Text;
                    DateTime Mdate = DateTime.Parse(Manufacturing_Date.Text);
                    String type = TypeBox.Text;
                    String unit = UnitBox.Text;
                    DateTime Edate = DateTime.Parse(EX_DateBox.Text);
                    float price = float.Parse(PriceBox.Text);
                    Int64 Quantity = Int64.Parse(QuantityBox.Text);
                    Int64 add_quantity = Int64.Parse(addquantityBox.Text);
                    Total_Quantity = Quantity + add_quantity;
                    Query = @"UPDATE MEDICINES SET Medicine_Name = '" + Name + "',Manufacturing_Date ='" + Mdate + "', Expire_Date='" + Edate + "', Quantity= '" + Total_Quantity + "',Unit='" + unit + "',Price ='" + price + "',Type ='" + type + "'  where Medicine_Code = '" + SearchBox.Text + "'";
                    data.GetData(Query);
                    MessageBox.Show("Medicine Data Updated Sucessfuly", "PharmaEasy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("You Must Enter Data in Empty Sections !", "PharmaEasy", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("IF you Want not Increase Quantity put 0 in Section 'ADD Quantity'. ", "PharmaEasy", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void Backbtn_Click(object sender, EventArgs e)
        {
            Pharmacist pharmacist = new Pharmacist();
            pharmacist.Show();
            this.Hide();
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            LogInForm logInForm = new LogInForm();
            logInForm.Show();
            this.Hide();
        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        
    }
}
