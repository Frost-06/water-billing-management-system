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
    public partial class AdminSide_ManageUsers : Form {
        SqlConnection AdminSide_ManageUsersCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ryzen\source\repos\Aquatric2\Aquatric2\AquatricDatabase.mdf;Integrated Security=True;Connect Timeout=30");
        DataTable usersGrid = new DataTable();
        public AdminSide_ManageUsers() {
            InitializeComponent();
            AdminSide_ManageUsersCon.Open();
        }

        private void AdminSide_ManageUsers_Load(object sender, EventArgs e) {
            int num;

            //get the last value in a row
            SqlCommand IDCheckCom = new SqlCommand("select *from registrationTable order by userID DESC", AdminSide_ManageUsersCon);
            IDCheckCom.ExecuteNonQuery();

            SqlDataReader dr = IDCheckCom.ExecuteReader();
            dr.Read();

            if (!(dr.HasRows)) {
                dr.Close();
                num = 1000;
                userID.Text = num.ToString();             

                usersGrid.Rows.Clear();

                //data grid for users
                SqlDataAdapter usersAdapter = new SqlDataAdapter("select *from registrationTable", AdminSide_ManageUsersCon);
                usersAdapter.Fill(usersGrid);
                usersGridView.DataSource = usersGrid;

                usersGridView.Columns[0].Width = 101;
                usersGridView.Columns[1].Width = 130;
                usersGridView.Columns[2].Width = 130;
                usersGridView.Columns[3].Width = 70;
                usersGridView.Columns[4].Width = 70;
                usersGridView.Columns[5].Width = 140;
                usersGridView.Columns[6].Width = 186;
                usersGridView.Columns[7].Width = 102;
                usersGridView.Columns[8].Width = 0;
                usersGridView.Columns[9].Width = 0;
            } else {
                num = int.Parse(dr["userID"].ToString());
                dr.Close();
                num++;
                userID.Text = num.ToString();

                usersGrid.Rows.Clear();

                //data grid for users
                SqlDataAdapter usersAdapter = new SqlDataAdapter("select *from registrationTable", AdminSide_ManageUsersCon);
                usersAdapter.Fill(usersGrid);
                usersGridView.DataSource = usersGrid;

                usersGridView.Columns[0].Width = 101;
                usersGridView.Columns[1].Width = 130;
                usersGridView.Columns[2].Width = 130;
                usersGridView.Columns[3].Width = 70;
                usersGridView.Columns[4].Width = 70;
                usersGridView.Columns[5].Width = 140;
                usersGridView.Columns[6].Width = 186;
                usersGridView.Columns[7].Width = 102;
                usersGridView.Columns[8].Width = 0;
                usersGridView.Columns[9].Width = 0;
            }

        }
        bool yes = false;
        private void usersGridView_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0) {
                DataGridViewRow rows = usersGridView.Rows[e.RowIndex];
                userID.Text = rows.Cells[0].Value.ToString();
                fnametxtBox.Text = rows.Cells[1].Value.ToString();
                lnametxtBox.Text = rows.Cells[2].Value.ToString();
                blockTxt.Text = rows.Cells[3].Value.ToString();
                lotTxt.Text = rows.Cells[4].Value.ToString();
                phonenumbertxtBox.Text = rows.Cells[5].Value.ToString();
                emailtxtBox.Text = rows.Cells[6].Value.ToString();
                passwordtxtBox.Text = rows.Cells[7].Value.ToString();
                picturetxtBox.Text = rows.Cells[9].Value.ToString();
                imageBox.ImageLocation = picturetxtBox.Text;
            }

            yes = true;
        }

        private void browseBtn_Click(object sender, EventArgs e) {
            //to upload images
            string imgPath = "";
            OpenFileDialog importImg = new OpenFileDialog();

            importImg.Filter = "Choose Image(*.jpg;*.png) | *.jpg;*.png;*";

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
        }

        private void userIDSearch_TextChanged(object sender, EventArgs e) {
            if (userIDSearch.Text == "") {
                usersGrid.Rows.Clear();
                SqlDataAdapter adapt = new SqlDataAdapter("select *from registrationTable", AdminSide_ManageUsersCon);
                usersGrid = new DataTable();
                adapt.Fill(usersGrid);
                usersGridView.DataSource = usersGrid;
            } else {
                usersGrid.Rows.Clear();
                SqlDataAdapter adapt = new SqlDataAdapter("select *from registrationTable where userID LIKE '%" + userIDSearch.Text + "%'", AdminSide_ManageUsersCon);
                usersGrid = new DataTable();
                adapt.Fill(usersGrid);
                usersGridView.DataSource = usersGrid;
            }
        }

        private void userDetailsComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if(userDetailsComboBox.SelectedItem == "Save") {
                if (userID.Text == "") {
                    MessageBox.Show("All Fields Required");
                } else if (yes == true) {
                    MessageBox.Show("ID already exist");                   
                } else {                   
                    int num;
                    SqlCommand insertUserCom = new SqlCommand("insert into registrationTable(userID, fname, lname, block, lot, phone, email, password, verifyPassword, image)values('" + userID.Text + "','" + fnametxtBox.Text + "','" + lnametxtBox.Text + "','" + blockTxt.Text + "','" + lotTxt.Text + "','" + phonenumbertxtBox.Text + "','" + emailtxtBox.Text + "','" + passwordtxtBox.Text + "','" + passwordtxtBox.Text + "','" + picturetxtBox.Text + "')", AdminSide_ManageUsersCon);
                    insertUserCom.ExecuteNonQuery();

                    usersGrid.Rows.Clear();

                    //data grid for users
                    SqlDataAdapter usersAdapter = new SqlDataAdapter("select *from registrationTable", AdminSide_ManageUsersCon);
                    usersAdapter.Fill(usersGrid);
                    usersGridView.DataSource = usersGrid;

                    usersGridView.Columns[0].Width = 101;
                    usersGridView.Columns[1].Width = 130;
                    usersGridView.Columns[2].Width = 130;
                    usersGridView.Columns[3].Width = 70;
                    usersGridView.Columns[4].Width = 70;
                    usersGridView.Columns[5].Width = 140;
                    usersGridView.Columns[6].Width = 186;
                    usersGridView.Columns[7].Width = 102;
                    usersGridView.Columns[8].Width = 0;
                    usersGridView.Columns[9].Width = 0;

                    MessageBox.Show("Record Saved");
                    num = int.Parse(userID.Text);
                    num++;
                    userID.Text = num.ToString();

                    fnametxtBox.Text = "";
                    lnametxtBox.Text = "";
                    blockTxt.Text =
                    lotTxt.Text = "";
                    phonenumbertxtBox.Text = "";
                    emailtxtBox.Text =
                    passwordtxtBox.Text = "";
                    picturetxtBox.Text = "";

                }
            } else if(userDetailsComboBox.SelectedItem =="Update") {
                if (userID.Text == "") {
                    MessageBox.Show("All Fields Required");
                } else {
                    SqlCommand updateregistrationTableCom = new SqlCommand("update registrationTable set fname='" + fnametxtBox.Text + "',lname='" + lnametxtBox.Text + "',block='" + blockTxt.Text + "',lot='" + lotTxt.Text + "',phone='" + phonenumbertxtBox.Text + "',email='" + emailtxtBox.Text + "',password='" + passwordtxtBox.Text + "',verifyPassword='" + passwordtxtBox.Text + "',image='" + picturetxtBox.Text + "' where userID=" + userID.Text + "", AdminSide_ManageUsersCon);
                    updateregistrationTableCom.ExecuteNonQuery();
                    MessageBox.Show("Record Updated");

                    int num;

                    //get the last value in a row
                    SqlCommand IDCheckCom = new SqlCommand("select *from registrationTable order by userID DESC", AdminSide_ManageUsersCon);
                    IDCheckCom.ExecuteNonQuery();

                    SqlDataReader dr = IDCheckCom.ExecuteReader();
                    dr.Read();

                    if (!(dr.HasRows)) {
                        dr.Close();
                        num = 1000;
                        userID.Text = num.ToString();

                        usersGrid.Rows.Clear();

                        //data grid for users
                        SqlDataAdapter usersAdapter = new SqlDataAdapter("select *from registrationTable", AdminSide_ManageUsersCon);
                        usersAdapter.Fill(usersGrid);
                        usersGridView.DataSource = usersGrid;

                        usersGridView.Columns[0].Width = 101;
                        usersGridView.Columns[1].Width = 130;
                        usersGridView.Columns[2].Width = 130;
                        usersGridView.Columns[3].Width = 70;
                        usersGridView.Columns[4].Width = 70;
                        usersGridView.Columns[5].Width = 140;
                        usersGridView.Columns[6].Width = 186;
                        usersGridView.Columns[7].Width = 102;
                        usersGridView.Columns[8].Width = 0;
                        usersGridView.Columns[9].Width = 0;

                        fnametxtBox.Text = "";
                        lnametxtBox.Text = "";
                        blockTxt.Text =
                        lotTxt.Text = "";
                        phonenumbertxtBox.Text = "";
                        emailtxtBox.Text =
                        passwordtxtBox.Text = "";
                        picturetxtBox.Text = "";
                    } else {
                        num = int.Parse(dr["userID"].ToString());
                        dr.Close();
                        num++;
                        userID.Text = num.ToString();

                        usersGrid.Rows.Clear();

                        //data grid for users
                        SqlDataAdapter usersAdapter = new SqlDataAdapter("select *from registrationTable", AdminSide_ManageUsersCon);
                        usersAdapter.Fill(usersGrid);
                        usersGridView.DataSource = usersGrid;

                        usersGridView.Columns[0].Width = 101;
                        usersGridView.Columns[1].Width = 130;
                        usersGridView.Columns[2].Width = 130;
                        usersGridView.Columns[3].Width = 70;
                        usersGridView.Columns[4].Width = 70;
                        usersGridView.Columns[5].Width = 140;
                        usersGridView.Columns[6].Width = 186;
                        usersGridView.Columns[7].Width = 102;
                        usersGridView.Columns[8].Width = 0;
                        usersGridView.Columns[9].Width = 0;

                        fnametxtBox.Text = "";
                        lnametxtBox.Text = "";
                        blockTxt.Text =
                        lotTxt.Text = "";
                        phonenumbertxtBox.Text = "";
                        emailtxtBox.Text =
                        passwordtxtBox.Text = "";
                        picturetxtBox.Text = "";
                    }
                }
                } else if(userDetailsComboBox.SelectedItem =="Delete") {
                if (userID.Text == "") {
                    MessageBox.Show("All Fields Required");
                } else {
                    SqlCommand deleteregistrationCon = new SqlCommand("delete from registrationTable where userID = " + userID.Text + " ", AdminSide_ManageUsersCon);
            deleteregistrationCon.ExecuteNonQuery();

            MessageBox.Show("Record Deleted");

            usersGrid.Rows.Clear();

            int num;

            //get the last value in a row
            SqlCommand IDCheckCom = new SqlCommand("select *from registrationTable order by userID DESC", AdminSide_ManageUsersCon);
            IDCheckCom.ExecuteNonQuery();

            SqlDataReader dr = IDCheckCom.ExecuteReader();
            dr.Read();

            if (!(dr.HasRows)) {
                dr.Close();
                num = 1000;
                userID.Text = num.ToString();

                usersGrid.Rows.Clear();

                //data grid for users
                SqlDataAdapter usersAdapter = new SqlDataAdapter("select *from registrationTable", AdminSide_ManageUsersCon);
                usersAdapter.Fill(usersGrid);
                usersGridView.DataSource = usersGrid;

                usersGridView.Columns[0].Width = 101;
                usersGridView.Columns[1].Width = 130;
                usersGridView.Columns[2].Width = 130;
                usersGridView.Columns[3].Width = 70;
                usersGridView.Columns[4].Width = 70;
                usersGridView.Columns[5].Width = 140;
                usersGridView.Columns[6].Width = 186;
                usersGridView.Columns[7].Width = 102;
                usersGridView.Columns[8].Width = 0;
                usersGridView.Columns[9].Width = 0;

                fnametxtBox.Text = "";
                lnametxtBox.Text = "";
                blockTxt.Text =
                lotTxt.Text = "";
                phonenumbertxtBox.Text = "";
                emailtxtBox.Text =
                passwordtxtBox.Text = "";
                picturetxtBox.Text = "";
            } else {
                num = int.Parse(dr["userID"].ToString());
                dr.Close();
                num++;
                userID.Text = num.ToString();

                usersGrid.Rows.Clear();

                //data grid for users
                SqlDataAdapter usersAdapter = new SqlDataAdapter("select *from registrationTable", AdminSide_ManageUsersCon);
                usersAdapter.Fill(usersGrid);
                usersGridView.DataSource = usersGrid;

                usersGridView.Columns[0].Width = 101;
                usersGridView.Columns[1].Width = 130;
                usersGridView.Columns[2].Width = 130;
                usersGridView.Columns[3].Width = 70;
                usersGridView.Columns[4].Width = 70;
                usersGridView.Columns[5].Width = 140;
                usersGridView.Columns[6].Width = 186;
                usersGridView.Columns[7].Width = 102;
                usersGridView.Columns[8].Width = 0;
                usersGridView.Columns[9].Width = 0;

                fnametxtBox.Text = "";
                lnametxtBox.Text = "";
                blockTxt.Text =
                lotTxt.Text = "";
                phonenumbertxtBox.Text = "";
                emailtxtBox.Text =
                passwordtxtBox.Text = "";
                picturetxtBox.Text = "";
            }
                }
            }
        }
    }
}
