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

namespace Aquatric2 {
    public partial class UserSide_Settings : Form {
        public static int count = 0;
        public static int userIDForSettings=0;
        SqlConnection UserSide_SettingsCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ryzen\source\repos\Aquatric2\Aquatric2\AquatricDatabase.mdf;Integrated Security=True;Connect Timeout=30");
       
        public UserSide_Settings() {
            InitializeComponent();
            UserSide_SettingsCon.Open();

            if(Registration.counter == 1) {
                userIDForSettings = int.Parse(Registration.userID);

            } else {
                userIDForSettings = int.Parse(Login.userID);
            }
        }

        private void viewPassword_Click(object sender, EventArgs e) {
            if(count==0) {
                viewPassword.Image = Properties.Resources.pass_on1;
                viewPassword.ImageOffset = new Point(0, 0);
                viewPassword.ImageSize = new Size(25, 16);
                passwordtxtBox.UseSystemPasswordChar = true;
            } if (count % 2 != 0) {
                count++;
                viewPassword.Image = Properties.Resources.pass_off1;
                viewPassword.ImageOffset = new Point(0, 0);
                viewPassword.ImageSize = new Size(25, 19);
                passwordtxtBox.UseSystemPasswordChar = false;

            } else {
                viewPassword.Image = Properties.Resources.pass_on1;
                viewPassword.ImageOffset = new Point(0, 0);
                viewPassword.ImageSize = new Size(25, 16);
                passwordtxtBox.UseSystemPasswordChar = true;
            }

            count++;
        }

        private void viewPassword_CheckedChanged(object sender, EventArgs e) {
            if (passwordtxtBox.UseSystemPasswordChar == true) {
                viewPassword.Image = Properties.Resources.pass_off1;
                viewPassword.ImageOffset = new Point(0, 0);
                viewPassword.ImageSize = new Size(25, 19);
                passwordtxtBox.UseSystemPasswordChar = false;
            } else {
                viewPassword.Image = Properties.Resources.pass_on1;
                viewPassword.ImageOffset = new Point(0, 0);
                viewPassword.ImageSize = new Size(25, 16);
                passwordtxtBox.UseSystemPasswordChar = false;
            }
        }

        private void UserSide_Settings_Load(object sender, EventArgs e) {
            SqlCommand comForUserView = new SqlCommand("select *from registrationTable where userID=" + userIDForSettings + "", UserSide_SettingsCon);
            SqlDataReader rdr = comForUserView.ExecuteReader();

            while(rdr.Read()) {
                fnametxtBox.Text = rdr["fname"].ToString();
                lnametxtBox.Text = rdr["lname"].ToString();
                blockTxt.Text = rdr["block"].ToString();
                lotTxt.Text = rdr["lot"].ToString();
                phonenumbertxtBox.Text = rdr["phone"].ToString();
                emailtxtBox.Text = rdr["email"].ToString();
                passwordtxtBox.Text = rdr["password"].ToString();
                verifyPasswordtxtBox.Text = rdr["verifyPassword"].ToString();
                picturetxtBox.Text = rdr["image"].ToString();
            }

        }

        private void editBtn_Click(object sender, EventArgs e) {
            fnametxtBox.Enabled = true;
            lnametxtBox.Enabled = true;
            blockTxt.Enabled = true;
            lotTxt.Enabled = true;
            phonenumbertxtBox.Enabled = true;
            emailtxtBox.Enabled = true;
            passwordtxtBox.Enabled = true;
            verifyPasswordtxtBox.Enabled = true;
            picturetxtBox.Enabled = true;
        }

        private void saveChangesBtn_Click(object sender, EventArgs e) {
            SqlCommand updateregistrationTableCom = new SqlCommand("update registrationTable set fname='" + fnametxtBox.Text + "',lname='" + lnametxtBox.Text + "',block='" + blockTxt.Text + "',lot='" + lotTxt.Text + "',phone='" + phonenumbertxtBox.Text + "',email='" + emailtxtBox.Text + "',password='" + passwordtxtBox.Text + "',verifyPassword='" + passwordtxtBox.Text + "',image='" + picturetxtBox.Text + "' where userID=" + userIDForSettings + "", UserSide_SettingsCon);
            updateregistrationTableCom.ExecuteNonQuery();
            MessageBox.Show("Record Updated");
        }

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
    }
}

