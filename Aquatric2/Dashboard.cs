using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Media;

namespace Aquatric2 {
    public partial class Dashboard : Form {
        SqlConnection DashboardCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ryzen\source\repos\Aquatric2\Aquatric2\AquatricDatabase.mdf;Integrated Security=True;Connect Timeout=30");
        public static string userIDForDashboard;
        //Log in
        private void ThreadUser() {
            Application.Run(new Login());
        }

        private void logoutBtn_Click(object sender, EventArgs e) {
            fnameText.Text = "";
            lnameText.Text = "";
            userIDText.Text = "";
            if (Registration.counter == 1) {
                Registration.counter = 0;
            } else {

            }

            UserSide_Dashboard.jan = 0;
            UserSide_Dashboard.feb = 0;
            UserSide_Dashboard.mar = 0;
            UserSide_Dashboard.apr = 0;
            UserSide_Dashboard.may = 0;
            UserSide_Dashboard.jun = 0;
            UserSide_Dashboard.jul = 0;
            UserSide_Dashboard.aug = 0;
            UserSide_Dashboard.sep = 0;
            UserSide_Dashboard.oct = 0;
            UserSide_Dashboard.nov = 0;
            UserSide_Dashboard.dec = 0;

            this.Dispose(true);
            this.Refresh();
            this.Controls.Clear();
            this.InitializeComponent();
            
            //opening admin login
            Thread ThreadLogin = new Thread(new ThreadStart(ThreadUser)); //you create a new thread  
       
           // this.Close();          
            ThreadLogin.Start();
        }

        public Dashboard() {
            InitializeComponent();

            //data of user log in
            string fname = Registration.fname;
            string lname = Registration.lname;

            string fname1 = Login.fname;
            string lname1 = Login.lname;

            //save user data first name last name and userID
            if (Registration.counter == 1) {
                userIDText.Text = Registration.userID;
                userIDForDashboard = Registration.userID;

                fnameText.Text = char.ToUpper(fname[0]) + fname.Substring(1);
                lnameText.Text = char.ToUpper(lname[0]) + lname.Substring(1);
            } else {
                userIDText.Text = Login.userID;
                userIDForDashboard = Login.userID;
                fnameText.Text = (char.ToUpper(fname1[0])) + fname1.Substring(1);
                lnameText.Text = (char.ToUpper(lname1[0])) + lname1.Substring(1);
            }
        }

        private void Dashboard_Load(object sender, EventArgs e) {
            string picTxt="";
            DashboardCon.Open();
            UserSide_Dashboard frm = new UserSide_Dashboard() {
                TopLevel = false,
                TopMost = true
            };
            frm.FormBorderStyle = FormBorderStyle.None;
            this.panelFormOpen.Controls.Add(frm);
            frm.Show();
            frm.BringToFront();

            SqlCommand comForBlockLot = new SqlCommand("select *from registrationTable where userID=" + userIDText.Text + "", DashboardCon);
            SqlDataReader rdr = comForBlockLot.ExecuteReader();

            while (rdr.Read()) {
                blockTxt.Text = rdr["block"].ToString();
                lotTxt.Text = rdr["lot"].ToString();
                picTxt = rdr["image"].ToString();
                UserProfile.ImageLocation = rdr["image"].ToString();
            }

        }

        private void dashboardBtn_Click(object sender, EventArgs e) {
            UserSide_Dashboard frm = new UserSide_Dashboard() {
                TopLevel = false,
                TopMost = true
            };
            frm.FormBorderStyle = FormBorderStyle.None;
            this.panelFormOpen.Controls.Add(frm);
            frm.Show();
            frm.BringToFront();

            dashboardBtn.FillColor = System.Drawing.Color.FromArgb(7, 159, 234);
            dashboardBtn.Image = Properties.Resources.Asset_8;
            dashboardBtn.ImageOffset = new Point(0, 0);
            dashboardBtn.ImageSize = new Size(19, 19);
            dashboardBtn.ForeColor = System.Drawing.Color.White;

            settingsBtn.FillColor = System.Drawing.Color.FromArgb(255, 255, 255);
            settingsBtn.Image = Properties.Resources.Asset_351;
            settingsBtn.ImageOffset = new Point(-7, 0);
            settingsBtn.ImageSize = new Size(19, 19);
            settingsBtn.ForeColor = System.Drawing.Color.Black;

            billingBtn.FillColor = System.Drawing.Color.FromArgb(255, 255, 255);
            billingBtn.Image = Properties.Resources.Asset_12;
            billingBtn.ImageOffset = new Point(-11, 0);
            billingBtn.ImageSize = new Size(19, 19);
            billingBtn.ForeColor = System.Drawing.Color.Black;
        }

        private void billingBtn_Click(object sender, EventArgs e) {
            UserSide_Billing frm = new UserSide_Billing() {
                TopLevel = false,
                TopMost = true
            };
            frm.FormBorderStyle = FormBorderStyle.None;
            this.panelFormOpen.Controls.Add(frm);
            frm.Show();
            frm.BringToFront();

            billingBtn.FillColor = System.Drawing.Color.FromArgb(7, 159, 234);
            billingBtn.Image = Properties.Resources.Asset_11;
            billingBtn.ImageOffset = new Point(-11, 0);
            billingBtn.ImageSize = new Size(19, 19);
            billingBtn.ForeColor = System.Drawing.Color.White;

            settingsBtn.FillColor = System.Drawing.Color.FromArgb(255, 255, 255);
            settingsBtn.Image = Properties.Resources.Asset_351;
            settingsBtn.ImageOffset = new Point(-7, 0);
            settingsBtn.ImageSize = new Size(19, 19);
            settingsBtn.ForeColor = System.Drawing.Color.Black;

            dashboardBtn.FillColor = System.Drawing.Color.FromArgb(255, 255, 255);
            dashboardBtn.Image = Properties.Resources.Asset_33;
            dashboardBtn.ImageOffset = new Point(0, 0);
            dashboardBtn.ImageSize = new Size(19, 19);
            dashboardBtn.ForeColor = System.Drawing.Color.Black;
        }
        private bool isCollapsed;

        private void dropdownTimer_Tick(object sender, EventArgs e) {
            if (isCollapsed) {
                dropDownSettings.Height += 10;
                if (dropDownSettings.Size == dropDownSettings.MaximumSize) {
                    dropdownTimer.Stop();
                    isCollapsed = false;
                }
            } else {
                dropDownSettings.Height -= 10;
                if (dropDownSettings.Size == dropDownSettings.MinimumSize) {
                    dropdownTimer.Stop();
                    isCollapsed = true;
                }
            }
        }

        private void settingsBtn_Click(object sender, EventArgs e) {
            dropdownTimer.Start();

            settingsBtn.FillColor = System.Drawing.Color.FromArgb(7, 159, 234);
            settingsBtn.Image = Properties.Resources.Asset_341;
            settingsBtn.ImageOffset = new Point(-7, 0);
            settingsBtn.ImageSize = new Size(19, 19);
            settingsBtn.ForeColor = System.Drawing.Color.White;

            dashboardBtn.FillColor = System.Drawing.Color.FromArgb(255, 255, 255);
            dashboardBtn.Image = Properties.Resources.Asset_33;
            dashboardBtn.ImageOffset = new Point(0, 0);
            dashboardBtn.ImageSize = new Size(19, 19);
            dashboardBtn.ForeColor = System.Drawing.Color.Black;

            billingBtn.FillColor = System.Drawing.Color.FromArgb(255, 255, 255);
            billingBtn.Image = Properties.Resources.Asset_12;
            billingBtn.ImageOffset = new Point(-11, 0);
            billingBtn.ImageSize = new Size(19, 19);
            billingBtn.ForeColor = System.Drawing.Color.Black;


        }

        private void personalDetailsBtn_Click(object sender, EventArgs e) {
            UserSide_Settings frm = new UserSide_Settings() {
                TopLevel = false,
                TopMost = true
            };
            frm.FormBorderStyle = FormBorderStyle.None;
            this.panelFormOpen.Controls.Add(frm);
            frm.Show();
            frm.BringToFront();

            personalDetailsBtn.ForeColor = System.Drawing.Color.FromArgb(7, 159, 234);
        }
    }
}
