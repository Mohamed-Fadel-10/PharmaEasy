using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PharmaEasy
{
    public partial class AdminDashboard : Form
    {
        public String Query;
        int Data;
        DataControler data;
        DataSet set;
        Profile profile;
      
      
        public AdminDashboard()
        {
            InitializeComponent();
            userNameLabel.Text = UserData.Username;
            data =new DataControler();
            set=new DataSet();
            getdata();
            
        }
        private void getdata()
        {
             Query =
              @"Select Count(ID) from Users ";
            Data = data.GetScalerFunctios(Query);
            userNum.Text = Data.ToString();

            Query =
                @"Select Count(*) from Pharmaceutical_Office";
           Data = data.GetScalerFunctios(Query);
            PhNum.Text= Data.ToString();

            Query =
               @"Select COUNT(*) from MEDICINES ";
            Data = data.GetScalerFunctios(Query);
            MedLabel.Text = Data.ToString();

            Query =
            @"SELECT COUNT(*) FROM MEDICINES WHERE Expire_Date <= getdate()";
            Data = data.GetScalerFunctios(Query);
            EXlabl.Text = Data.ToString();


        }
 
        private void AddUsers_Click(object sender, EventArgs e)
        {
            AddNewUser addNewUser = new AddNewUser();
            addNewUser.Show();
            this.Hide();
        }
        private void Add_New_Pharmaceutical_Click(object sender, EventArgs e)
        {
            Add_Pharmaceutical_Office office = new Add_Pharmaceutical_Office();
            office.Show();
            this.Hide();
        }

      

        private void ViewUsersbtn_Click(object sender, EventArgs e)
        {
            Modify_Users modify_Users = new Modify_Users();
            modify_Users.Show();
            this.Hide();

        }

        private void ViewPharmaceuticalbtn_Click(object sender, EventArgs e)
        {
            Modify_Pharmaceutical_Office office = new Modify_Pharmaceutical_Office();
            office.Show();
            this.Hide();
        }

       

        private void Profilebtn_Click(object sender, EventArgs e)
        {
            Profile profile = new Profile();
            profile.Show();
            this.Hide();
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            LogInForm logInForm = new LogInForm();
            logInForm.Show();
            this.Hide();
        }

       
    }
}
