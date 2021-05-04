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
using System.Xml;
using System.Collections;

namespace Aquatric2
{
    public partial class Administrator_Dashboard : Form
    {      
        private void dashboardBtn_Click(object sender, EventArgs e) {
            AdminSide_Dashboard frm = new AdminSide_Dashboard() {
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
            
            reportBtn.FillColor = System.Drawing.Color.FromArgb(255, 255, 255);
            reportBtn.Image = Properties.Resources.Asset_10;
            reportBtn.ImageSize = new Size(19, 19);
            reportBtn.ForeColor = System.Drawing.Color.Black;
            
            manageUserBtn.FillColor = System.Drawing.Color.FromArgb(255, 255, 255);
            manageUserBtn.Image = Properties.Resources.Asset_331;
            manageUserBtn.ImageSize = new Size(19, 19);
            manageUserBtn.ForeColor = System.Drawing.Color.Black;
            
            closeDropdown.Start();   
        }
     
        public Administrator_Dashboard() {
            InitializeComponent();
            
            string fname = Administrator.fname;
            string lname = Administrator.lname;
            string type = Administrator.type;

            if (type == "admin") {
                manageUserBtn.Visible = true;
            } else {
                manageUserBtn.Visible  = false;
            }
             
            fnameText.Text = char.ToUpper(fname[0]) + fname.Substring(1);
            lnameText.Text = char.ToUpper(lname[0]) + lname.Substring(1);
            adminTypeTxt.Text = char.ToUpper(type[0]) + type.Substring(1);   
                     
        }

        //Log in
        private void ThreadAdmin() {
            Application.Run(new Administrator());
        }
  
        private void logoutBtn_Click(object sender, EventArgs e) {
            //opening admin login
            Thread ThreadLogin = new Thread(new ThreadStart(ThreadAdmin)); //you create a new thread
            this.Close();
            ThreadLogin.Start();
        }

        //load the administrator dashboard
        private void Administrator_Dashboard_Load(object sender, EventArgs e) {
            AdminSide_Dashboard frm = new AdminSide_Dashboard() {
                TopLevel = false,
                TopMost = true
            };
            frm.FormBorderStyle = FormBorderStyle.None;
            this.panelFormOpen.Controls.Add(frm);
            frm.Show();
        }

        private bool isCollapsed, isCollapsed1;
        private void dropdwonTimer_Tick(object sender, EventArgs e) {
            if(isCollapsed) { 
                dropwdownReport.Height += 10;
                if(dropwdownReport.Size==dropwdownReport.MaximumSize) {
                    dropdwonTimer.Stop();
                    isCollapsed = false;
                }
            } else {
                dropwdownReport.Height -= 10;
                if (dropwdownReport.Size == dropwdownReport.MinimumSize) {
                    dropdwonTimer.Stop();
                    isCollapsed = true;
                }
            }
        }

        private void dropdownTimer1_Tick(object sender, EventArgs e) {
            if (isCollapsed1) {
                dropDownManage.Height += 10;
                if (dropDownManage.Size == dropDownManage.MaximumSize) {
                    dropdownTimer1.Stop();
                    isCollapsed1 = false;
                }
            } else {
                dropDownManage.Height -= 10;
                if (dropDownManage.Size == dropDownManage.MinimumSize) {
                    dropdownTimer1.Stop();
                    isCollapsed1 = true;
                }
            }
        }

        private void closeDropdown_Tick(object sender, EventArgs e) {
        }

        private void reportBtn_Click_1(object sender, EventArgs e) {
            dropdwonTimer.Start();

            reportBtn.FillColor = System.Drawing.Color.FromArgb(7, 159, 234);
            reportBtn.Image = Properties.Resources.Asset_9;
            reportBtn.ImageOffset = new Point(-14, 0);
            reportBtn.TextOffset = new Point(-5, 0);
            reportBtn.ImageSize = new Size(19, 19);
            reportBtn.ForeColor = System.Drawing.Color.White;

            
            dashboardBtn.FillColor = System.Drawing.Color.FromArgb(255, 255, 255);
            dashboardBtn.Image = Properties.Resources.Asset_33;
            dashboardBtn.ImageOffset = new Point(0, 0);
            dashboardBtn.ImageSize = new Size(19, 19);
            dashboardBtn.ForeColor = System.Drawing.Color.Black;

            manageUserBtn.FillColor = System.Drawing.Color.FromArgb(255, 255, 255);
            manageUserBtn.Image = Properties.Resources.Asset_331;
            manageUserBtn.ImageSize = new Size(19, 19);
            manageUserBtn.ForeColor = System.Drawing.Color.Black;
        }

        private void monthlyReportBtn_Click(object sender, EventArgs e) {
            AdminSide_MonthlyReport frm = new AdminSide_MonthlyReport() {
                TopLevel = false,
                TopMost = true
            };
            frm.FormBorderStyle = FormBorderStyle.None;
            this.panelFormOpen.Controls.Add(frm);
            frm.Show();
            frm.BringToFront();
            monthlyReportBtn.ForeColor = System.Drawing.Color.FromArgb(7, 159, 234);
        }

        private void paymnetsBtn_Click(object sender, EventArgs e) {
            AdminSide_Payment frm = new AdminSide_Payment() {
                TopLevel = false,
                TopMost = true
            };
            frm.FormBorderStyle = FormBorderStyle.None;
            this.panelFormOpen.Controls.Add(frm);
            frm.Show();
            frm.BringToFront();
            paymnetsBtn.ForeColor = System.Drawing.Color.FromArgb(7, 159, 234);
        }

        private void userDetailsBtn_Click(object sender, EventArgs e) {
            AdminSide_ManageUsers frm = new AdminSide_ManageUsers() {
                TopLevel = false,
                TopMost = true
            };
            frm.FormBorderStyle = FormBorderStyle.None;
            this.panelFormOpen.Controls.Add(frm);
            frm.Show();
            frm.BringToFront();
            userDetailsBtn.ForeColor = System.Drawing.Color.FromArgb(7, 159, 234);
        }

        private void dailyreportBtn_Click(object sender, EventArgs e) {
            AdminSide_DailyReport frm = new AdminSide_DailyReport() {
                TopLevel = false,
                TopMost = true
            };
            frm.FormBorderStyle = FormBorderStyle.None;
            this.panelFormOpen.Controls.Add(frm);
            frm.Show();
            frm.BringToFront();
            dailyreportBtn.ForeColor = System.Drawing.Color.FromArgb(7, 159, 234);
        }

        private void userReportsBtn_Click(object sender, EventArgs e) {
            AdminSide_UserReport frm = new AdminSide_UserReport() {
                TopLevel = false,
                TopMost = true
            };
            frm.FormBorderStyle = FormBorderStyle.None;
            this.panelFormOpen.Controls.Add(frm);
            frm.Show();
            frm.BringToFront();
            userReportsBtn.ForeColor = System.Drawing.Color.FromArgb(7, 159, 234);
        }

        private void billingBtn_Click(object sender, EventArgs e) {
            AdminSide_Billing frm = new AdminSide_Billing() {
                TopLevel = false,
                TopMost = true
            };
            frm.FormBorderStyle = FormBorderStyle.None;
            this.panelFormOpen.Controls.Add(frm);
            frm.Show();
            frm.BringToFront();
            billingBtn.ForeColor = System.Drawing.Color.FromArgb(7, 159, 234);
        }
        private void helpCenter_Click(object sender, EventArgs e) {
            AdminSide_Complaints frm = new AdminSide_Complaints() {
                TopLevel = false,
                TopMost = true
            };
            frm.FormBorderStyle = FormBorderStyle.None;
            this.panelFormOpen.Controls.Add(frm);
            frm.Show();
            frm.BringToFront();
            helpCenter.ForeColor = System.Drawing.Color.FromArgb(7, 159, 234);
        }

        private void manageUserBtn_Click(object sender, EventArgs e) {
            manageUserBtn.FillColor = System.Drawing.Color.FromArgb(7, 159, 234);
            manageUserBtn.Image = Properties.Resources.Asset_321;
            manageUserBtn.ImageOffset = new Point(-6, 0);
            manageUserBtn.TextOffset = new Point(2, 0);
            manageUserBtn.ImageSize = new Size(19, 19);
            manageUserBtn.ForeColor = System.Drawing.Color.White;

            reportBtn.FillColor = System.Drawing.Color.FromArgb(255, 255, 255);
            reportBtn.Image = Properties.Resources.Asset_10;
            reportBtn.ImageSize = new Size(19, 19);
            reportBtn.ForeColor = System.Drawing.Color.Black;

            dashboardBtn.FillColor = System.Drawing.Color.FromArgb(255, 255, 255);
            dashboardBtn.Image = Properties.Resources.Asset_33;
            dashboardBtn.ImageOffset = new Point(0, 0);
            dashboardBtn.ImageSize = new Size(19, 19);
            dashboardBtn.ForeColor = System.Drawing.Color.Black;
            dropdownTimer1.Start();
        }
    }
}
