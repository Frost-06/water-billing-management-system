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
    public partial class AdminSide_Complaints : Form {
        SqlConnection AdminSide_ComplaintsCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ryzen\source\repos\Aquatric2\Aquatric2\AquatricDatabase.mdf;Integrated Security=True;Connect Timeout=30");
        DataTable complaintsTable = new DataTable();
        public static string number,userIDUser;
        public AdminSide_Complaints() {
            InitializeComponent();
            AdminSide_ComplaintsCon.Open();
            
        }

        private void AdminSide_Complaints_Load(object sender, EventArgs e) {
           

            // daatgrid for monthly consumption 
            SqlDataAdapter helpCenterCom = new SqlDataAdapter("select rows,userID,caseNo,date,description,actions from HelpCenter order by userID desc", AdminSide_ComplaintsCon);
            helpCenterCom.Fill(complaintsTable);
            complaintsGrid.DataSource = complaintsTable;

            complaintsGrid.Columns[0].Width = 1;
            complaintsGrid.Columns[1].Width = 60;
            complaintsGrid.Columns[2].Width = 60;
            complaintsGrid.Columns[3].Width = 90;
            complaintsGrid.Columns[5].Width = 120;
        }

        private void actionsComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            actionsTxt.Text = actionsComboBox.Text;
        }

        private void complaintsComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if(complaintsComboBox.SelectedItem=="Update") {
                SqlCommand updateHelpCenter = new SqlCommand("update HelpCenter set actions='" + actionsTxt.Text + "' where rows=" + number + "", AdminSide_ComplaintsCon);
                updateHelpCenter.ExecuteNonQuery();
                MessageBox.Show("Successfully Updated");

                complaintsTable.Rows.Clear();

                // daatgrid for monthly consumption 
                SqlDataAdapter helpCenterCom = new SqlDataAdapter("select rows,userID,caseNo,date,description,actions from HelpCenter order by userID desc", AdminSide_ComplaintsCon);
                helpCenterCom.Fill(complaintsTable);
                complaintsGrid.DataSource = complaintsTable;

                descriptionTxt.Text = "";
                actionsTxt.Text = "";

            } else if(complaintsComboBox.SelectedItem == "Delete") {
                SqlCommand deleteHelpCenter = new SqlCommand("delete from HelpCenter where rows = " + number + " ", AdminSide_ComplaintsCon);
                deleteHelpCenter.ExecuteNonQuery();

                MessageBox.Show("Record Deleted");

                descriptionTxt.Text = "";
                actionsTxt.Text = "";
                toTxt.Text = "";

                int rowIndex = complaintsGrid.CurrentCell.RowIndex;
                complaintsGrid.Rows.RemoveAt(rowIndex);
            }
        }

        private void sendBtn_Click(object sender, EventArgs e) {
            try {
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(fromTxt.Text);
                mail.To.Add(toTxt.Text);
                mail.Subject = subjectTxt.Text;
                mail.Body = bodyTxt.Text;              

                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential(fromTxt.Text, passwordTxt.Text);
                smtp.EnableSsl = true;
                smtp.Send(mail);
                smtp.UseDefaultCredentials = false;

                MessageBox.Show("Mail has been successfully sent!", "Email Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SqlCommand notifyCom = new SqlCommand("insert into Notification values(" + userIDUser + ",'" + bodyTxt.Text + "')", AdminSide_ComplaintsCon);
                notifyCom.ExecuteNonQuery();

            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void complaintsGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0) {
                DataGridViewRow rows = this.complaintsGrid.Rows[e.RowIndex];
                number = rows.Cells[0].Value.ToString();
                userIDUser = rows.Cells[1].Value.ToString();
                descriptionTxt.Text = rows.Cells[4].Value.ToString();
                actionsTxt.Text = rows.Cells[5].Value.ToString();

                SqlCommand comTotalAmount = new SqlCommand("select email from registrationTable where userID=" + userIDUser + "", AdminSide_ComplaintsCon);
                SqlDataReader rdr = comTotalAmount.ExecuteReader();

                while (rdr.Read()) {
                    toTxt.Text = rdr["email"].ToString();
                }
                rdr.Close();
            }
        }
    }
}
