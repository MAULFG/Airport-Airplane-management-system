using Airport_Airplane_management_system.Presenter;
using Airport_Airplane_management_system.View.Forms.AdminPages;
using Airport_Airplane_management_system.View.Forms.LoginPages;
using Airport_Airplane_management_system.View.Forms.UserPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View
{
    public partial class Main1 : Form
    {
        public Main1()
        {
            InitializeComponent();
        }
        public void ShowUser()
        {
            loginPage1.Hide();
            forgetUserControl1.Hide();
            signupusercontrol1.Hide();
            userDashboard1.Show();
        }
        public void ShowAdmin()
        {
            loginPage1.Hide();
            forgetUserControl1.Hide();
            signupusercontrol1.Hide();
            userDashboard1.Hide();
        }
        public void ShowForget()
        {
            loginPage1.Hide();
            forgetUserControl1.Show();
            signupusercontrol1.Hide();
            userDashboard1.Hide();
        }
        public void ShowSignUp()
        {
            loginPage1.Hide();
            forgetUserControl1.Hide();
            signupusercontrol1.Show();
            userDashboard1.Hide();

        }
        public void ShowLogin()
        {
            loginPage1.Show();
            forgetUserControl1.Hide();
            signupusercontrol1.Hide();
            userDashboard1.Hide();
        }
        public void Main1_Load(object sender, EventArgs e)
        {
            loginPage1.Show();
            forgetUserControl1.Hide();
            signupusercontrol1.Hide();
            userDashboard1.Hide();
        }

        private void loginPage1_Load(object sender, EventArgs e)
        {

        }

        private void forgetUserControl1_Load(object sender, EventArgs e)
        {

        }

        private void signupusercontrol1_Load(object sender, EventArgs e)
        {

        }

        private void signupusercontrol1_Load_1(object sender, EventArgs e)
        {

        }

        private void userDashboard1_Load(object sender, EventArgs e)
        {

        }
    }
}
