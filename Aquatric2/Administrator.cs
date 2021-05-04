using System;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;

namespace Aquatric2 {
    public partial class Administrator : Form {

        public static string fname, lname,type;

        SqlConnection AdminCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ryzen\source\repos\Aquatric2\Aquatric2\AquatricDatabase.mdf;Integrated Security=True;Connect Timeout=30");
        public Administrator() {
            InitializeComponent();
            AdminCon.Open();
        }


        //Adminstrator
        private void ThreadUser() {
            Application.Run(new Login());
        }

        //Adminstrator Dashboard
        private void ThreadAdminDashboard()
        {
            Application.Run(new Administrator_Dashboard());
        }

        private void userBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            //opening user login
            Thread ThreadSignup = new Thread(new ThreadStart(ThreadUser)); //you create a new thread
            this.Close();
            ThreadSignup.Start();
        }

        [STAThread]
        private void signinBtn_Click(object sender, EventArgs e) {
            if (usernameTxtBox.Text == "" || passwordTxtBox.Text == "") {
                MessageBox.Show("Input Fields Required");
            } else {
                SqlCommand SiginCom = new SqlCommand("select *from adminTable where username LIKE '%" + usernameTxtBox.Text + "%' AND password LIKE '%" + passwordTxtBox.Text + "%'", AdminCon);
                SqlDataReader dr = SiginCom.ExecuteReader();

                if (dr.Read()) {
                    if (dr["password"].ToString() == passwordTxtBox.Text) {
                        dr.Close();
                        SqlCommand comAdminData = new SqlCommand("select adminID,username,fname,lname,type from adminTable where username like '%" + usernameTxtBox.Text + "%'", AdminCon);
                        SqlDataReader rdr1 = comAdminData.ExecuteReader();

                        while (rdr1.Read()) {
                            fname = rdr1["fname"].ToString();
                            lname = rdr1["lname"].ToString();
                            type = rdr1["type"].ToString();
                        }
                        rdr1.Close();
                        AdminCon.Close();

                        MessageBox.Show("Log-in Succesfully");

                        //opening user login
                        Thread ThreadDashboard = new Thread(new ThreadStart(ThreadAdminDashboard)); //you create a new thread
                        this.Close();
                        ThreadDashboard.SetApartmentState(ApartmentState.STA);
                        ThreadDashboard.Start();
                    } else {
                        MessageBox.Show("Invalid Credentials");
                    }
                }
                dr.Close();
            }
            
            
        }
    }
}
