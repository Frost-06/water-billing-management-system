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

namespace Aquatric2 {
    public partial class Registration : Form {
        public static string fname, lname, userID;
        public static int counter = 0;

        SqlConnection RegistrationCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ryzen\source\repos\Aquatric2\Aquatric2\AquatricDatabase.mdf;Integrated Security=True;Connect Timeout=30");
       
        public Registration() {
            InitializeComponent();
        }
        //Login
        private void ThreadLogin() {
            Application.Run(new Login());
        }

        //Dashboard
        private void ThreadDashboard() {
            Application.Run(new Dashboard());
        }

        [STAThread]
        private void signupBtn_Click(object sender, EventArgs e) {
            RegistrationCon.Open();
            int num;

            if (fnametxtBox.Text == "" || lnametxtBox.Text == "" || blockTxt.Text == "" || lotTxt.Text=="" || emailtxtBox.Text == "" || passwordtxtBox.Text == "" || verifyPasswordtxtBox.Text == "") {
                fieldsrequired.Visible = true;
                passwordverificationLbl.Visible = false;
                fnametxtBox.Text = "";
                lnametxtBox.Text = "";
                blockTxt.Text = "";
                emailtxtBox.Text = "";
                phonenumbertxtBox.Text = "";
                passwordtxtBox.Text = "";
                verifyPasswordtxtBox.Text = "";
                RegistrationCon.Close();

            } else if (passwordtxtBox.Text!=verifyPasswordtxtBox.Text) {
                passwordverificationLbl.Visible = true;
                fieldsrequired.Visible = false;
                passwordtxtBox.Text = "";
                verifyPasswordtxtBox.Text = "";
                RegistrationCon.Close();
            } else {
                counter++;

                //get the last value in a row
                SqlCommand IDCheckCom = new SqlCommand("select *from registrationTable order by userID DESC", RegistrationCon);
                IDCheckCom.ExecuteNonQuery();

                SqlDataReader dr = IDCheckCom.ExecuteReader();
                dr.Read();

                if (!(dr.HasRows)) {
                    dr.Close();
                    num = 1000;
                    SqlCommand RegistrationCom = new SqlCommand("insert into registrationTable values(" + num + ",'" + fnametxtBox.Text + "','" + lnametxtBox.Text + "','" + blockTxt.Text + "','" + lotTxt.Text + "','" + phonenumbertxtBox.Text + "','" + emailtxtBox.Text + "','" + passwordtxtBox.Text + "','" + verifyPasswordtxtBox.Text + "','" + picturetxtBox.Text + "')", RegistrationCon);
                    RegistrationCom.ExecuteNonQuery();
                    MessageBox.Show("RECORD SAVED!");

                    userID = num.ToString();
                    fname = fnametxtBox.Text;
                    lname = lnametxtBox.ToString();

                    //opening dashboard
                    Thread ThreadDash = new Thread(new ThreadStart(ThreadDashboard)); //you create a new thread
                    this.Close();
                    ThreadDash.SetApartmentState(ApartmentState.STA);
                    ThreadDash.Start();
                } else {
                    num = int.Parse(dr["userID"].ToString());
                    dr.Close();
                    num++;
                    SqlCommand RegistrationCom = new SqlCommand("insert into registrationTable values(" + num + ",'" + fnametxtBox.Text + "','" + lnametxtBox.Text + "','" + blockTxt.Text + "','" + lotTxt.Text + "','" + phonenumbertxtBox.Text + "','" + emailtxtBox.Text + "','" + passwordtxtBox.Text + "','" + verifyPasswordtxtBox.Text + "','" + picturetxtBox.Text + "')", RegistrationCon);
                    RegistrationCom.ExecuteNonQuery();
                    MessageBox.Show("RECORD SAVED!");

                    userID = num.ToString();
                    fname = fnametxtBox.Text;
                    lname = lnametxtBox.Text;

                    //opening dashboard
                    Thread ThreadDash = new Thread(new ThreadStart(ThreadDashboard)); //you create a new thread
                    this.Close();
                    ThreadDash.SetApartmentState(ApartmentState.STA);
                    ThreadDash.Start();
                }

                dr.Close();
                RegistrationCon.Close();

            }
        }

        //browse
        private void browseBtn_Click(object sender, EventArgs e) {
            //to upload images
            string imgPath = "";
            OpenFileDialog importImg = new OpenFileDialog();

            importImg.Filter = "Choose Image(*.jpg;*.png) | *.jpg;*.png;*";

            Thread t = new Thread(() => {
                if (importImg.ShowDialog() == DialogResult.OK) {
                    imgPath = importImg.FileName;
                    picturetxtBox.Text = imgPath;
                    imageBox.ImageLocation = imgPath;
                    try {
                        imageBox.Image = Image.FromFile(importImg.FileName);
                    } catch (Exception) {
                        MessageBox.Show("Invalid format");
                    }
                }
            });

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }

        private void fnametxtBox_TextChanged(object sender, EventArgs e) {
            
        }

        private void fnametxtBox_Click(object sender, EventArgs e) {
            
        }

        private void phonenumbertxtBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) {
                e.Handled = true;
            }
        }

        private void fnametxtBox_KeyPress(object sender, KeyPressEventArgs e) {
            fieldsrequired.Visible = false;
            passwordverificationLbl.Visible = false;
        }

        //sign in
        private void signinLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            //opening Signin
            Thread ThreadSignin = new Thread(new ThreadStart(ThreadLogin)); //you create a new thread
            this.Close();
            ThreadSignin.Start();
        }
    }
}
