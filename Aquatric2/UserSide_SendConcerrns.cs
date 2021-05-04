using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Data.SqlClient;

namespace Aquatric2 {
    public partial class UserSide_SendConcerrns : Form {
        SqlConnection UserSide_Complaints = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ryzen\source\repos\Aquatric2\Aquatric2\AquatricDatabase.mdf;Integrated Security=True;Connect Timeout=30");
        public static int idNum, caseNo;
        public static string dateNow,actions;
        DataTable complaintsTable = new DataTable();

        public UserSide_SendConcerrns() {
            

            int num=0;
            UserSide_Complaints.Open();
            //get the last value in a row
            SqlCommand IDCheckCom = new SqlCommand("select *from HelpCenter order by userID DESC", UserSide_Complaints);
            IDCheckCom.ExecuteNonQuery();

            SqlDataReader dr = IDCheckCom.ExecuteReader();
            dr.Read();

            if (!(dr.HasRows)) {
                dr.Close();
                num = 8100;
            } else {
                num = int.Parse(dr["userID"].ToString());
                dr.Close();
                num++;
            }

            InitializeComponent();
            caseNo = num;
            fromTxt.Text = UserSide_Billing.fromEmailAddress;
            idNum = UserSide_Billing.idNumber;
            dateNow = DateTime.Now.ToString("M/d/yyyy");
            actions = "processing";


        }

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        private bool m_aeroEnabled;                     // variables for box shadow
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        public struct MARGINS {                          // struct for box shadow       
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        private const int WM_NCHITTEST = 0x84;          // variables for dragging the form
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        protected override CreateParams CreateParams {
            get {
                m_aeroEnabled = CheckAeroEnabled();

                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW;

                return cp;
            }
        }

        private bool CheckAeroEnabled() {
            if (Environment.OSVersion.Version.Major >= 6) {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }

        protected override void WndProc(ref Message m) {
            switch (m.Msg) {
                case WM_NCPAINT:                        // box shadow
                    if (m_aeroEnabled) {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS() {
                            bottomHeight = 1,
                            leftWidth = 1,
                            rightWidth = 1,
                            topHeight = 1
                        };
                        DwmExtendFrameIntoClientArea(this.Handle, ref margins);

                    }
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)     // drag the form
                m.Result = (IntPtr)HTCAPTION;
        }

        private void UserSide_SendConcerrns_Load(object sender, EventArgs e) {
            // daatgrid for monthly consumption 
            SqlDataAdapter helpCenterCom = new SqlDataAdapter("select caseNo,date,description,actions from HelpCenter where userID=" + idNum + "order by date desc", UserSide_Complaints);
            helpCenterCom.Fill(complaintsTable);
            complaintsGrid.DataSource = complaintsTable;
        }

        private void attachFileBtn_Click(object sender, EventArgs e) {
            openFileDialog1.ShowDialog();
            openFileDialog1.FileName = "";
            lblLocation.Text = openFileDialog1.FileName;
        }

        private void sendBtn_Click(object sender, EventArgs e) {
            string adminEmail = "andredulaguinita@gmail.com";
            try {

                if (lblLocation.Text == "") {
                    MailMessage mail = new MailMessage();
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress(fromTxt.Text);
                    mail.To.Add(adminEmail);
                    mail.Subject = subjectTxt.Text;
                    mail.Body = bodyTxt.Text;

                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential(fromTxt.Text, passwordTxt.Text);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    smtp.UseDefaultCredentials = false;

                    SqlCommand insertingIntoMeterInformationCom = new SqlCommand("insert into HelpCenter (userID,caseNo,date,description,actions) values ('" + idNum + "','" + caseNo + "','" + dateNow + "','" + bodyTxt.Text + "','" + actions + "')", UserSide_Complaints);
                    insertingIntoMeterInformationCom.ExecuteNonQuery();

                    complaintsGrid.Rows.Clear();

                    // daatgrid for monthly consumption 
                    SqlDataAdapter helpCenterCom = new SqlDataAdapter("select caseNo,date,description,actions from HelpCenter where userID="+idNum+"",UserSide_Complaints);
                    helpCenterCom.Fill(complaintsTable);
                    complaintsGrid.DataSource = complaintsTable;

                } else {
                    MailMessage mail = new MailMessage();
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress(fromTxt.Text);
                    mail.To.Add(adminEmail);
                    mail.Subject = subjectTxt.Text;
                    mail.Body = bodyTxt.Text;

                    Attachment attachment;
                    attachment = new Attachment(lblLocation.Text);
                    mail.Attachments.Add(attachment);

                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential(fromTxt.Text, passwordTxt.Text);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    smtp.UseDefaultCredentials = false;

                    SqlCommand insertingIntoMeterInformationCom = new SqlCommand("insert into HelpCenter (userID,caseNo,date,description,actions) values ('" + idNum + "','" + caseNo + "','" + dateNow + "','" + bodyTxt.Text + "','" + actions + "')", UserSide_Complaints);
                    insertingIntoMeterInformationCom.ExecuteNonQuery();

                    complaintsGrid.Rows.Clear();

                    // daatgrid for monthly consumption 
                    SqlDataAdapter helpCenterCom = new SqlDataAdapter("select caseNo,date,description,actions from HelpCenter where userID=" + idNum + "", UserSide_Complaints);
                    helpCenterCom.Fill(complaintsTable);
                    complaintsGrid.DataSource = complaintsTable;
                }
                
                MessageBox.Show("Mail has been successfully sent!", "Email Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);

            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
