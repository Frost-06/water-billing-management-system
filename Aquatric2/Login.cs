using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
using Guna.UI2.WinForms.Suite;

namespace Aquatric2 {
    public partial class Login : Form {
        public static string fname,lname,userID;

        SqlConnection LoginCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ryzen\source\repos\Aquatric2\Aquatric2\AquatricDatabase.mdf;Integrated Security=True;Connect Timeout=30");
   

        public Login() {
            InitializeComponent();
        }

        //Signup
        private void ThreadRegistration() {
            Application.Run(new Registration());
        }

        //Dashboard
        private void ThreadDashboard() {
            Application.Run(new Dashboard());
        }

        //Adminstrator
        private void ThreadAdmin() {
            Application.Run(new Administrator());
        }

        private void adminBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            //opening admin login
            Thread ThreadSignup = new Thread(new ThreadStart(ThreadAdmin)); //you create a new thread
            this.Close();
            ThreadSignup.Start();
        }

        private void signinBtn_KeyPress(object sender, KeyPressEventArgs e) {
    
        }

        private void signinBtn_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                signinBtn.PerformClick();
            }
                
        }

        [STAThread]
        private void signinBtn_Click(object sender, EventArgs e) {
            LoginCon.Open();

            if (emailaddTxtBox.Text == "" && passwordTxtBox.Text == "") {
                MessageBox.Show("All Fields Required!");
            } else {
                SqlCommand SiginCom = new SqlCommand("select *from registrationTable where email LIKE '%" + emailaddTxtBox.Text + "%' AND password LIKE '%" + passwordTxtBox.Text + "%'", LoginCon);
                SqlDataReader dr = SiginCom.ExecuteReader();

                if (dr.Read()) {
                    if(dr["password"].ToString()==passwordTxtBox.Text) {
                        userID = dr["userID"].ToString();
                        fname = dr["fname"].ToString();
                        lname = dr["lname"].ToString();

                        LoginCon.Close();

                        MessageBox.Show("Log-in Succesfully");
                        //opening dashboard
                        Thread ThreadDash = new Thread(new ThreadStart(ThreadDashboard)); //you create a new thread
                        this.Close();
                        ThreadDash.SetApartmentState(ApartmentState.STA);
                        ThreadDash.Start();
                    } else {
                        MessageBox.Show("Invalid Credentials");
                    }
                    
                }
            }

            LoginCon.Close();
        }

        //sign up
        private void signupLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            //opening signup
            Thread ThreadSignup = new Thread(new ThreadStart(ThreadRegistration)); //you create a new thread
            this.Close();
            ThreadSignup.Start();
        }
    }
}
