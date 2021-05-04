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
using System.Data.SqlClient;

namespace Aquatric2 {
    public partial class AdminSide_AdvancePayment : Form {
        SqlConnection AdminSide_AdvancePaymentCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ryzen\source\repos\Aquatric2\Aquatric2\AquatricDatabase.mdf;Integrated Security=True;Connect Timeout=30");
        public AdminSide_AdvancePayment() {
            InitializeComponent();
            AdminSide_AdvancePaymentCon.Open();
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

        private void guna2Panel2_Paint(object sender, PaintEventArgs e) {

        }

        private void guna2HtmlLabel7_Click(object sender, EventArgs e) {

        }

        private void AdminSide_AdvancePayment_Load(object sender, EventArgs e) {
            userIDTxt.Text = AdminSide_Payment.idNumber;
        }

        private void saveBtn_Click(object sender, EventArgs e) {
            if(userIDTxt.Text=="") {
                MessageBox.Show("Input Fields Required");
            } else {
                SqlCommand advancePaymentCom = new SqlCommand("insert into AdvancePayment (userID,amount,startingMonth,endingMonth,remainingBalance) values ('" + userIDTxt.Text + "','" + amountTxt.Text + "','" + startingMonthComboBox.Text + "','" + endingMonthComboBox.Text + "','" + amountTxt.Text + "')", AdminSide_AdvancePaymentCon);
                advancePaymentCom.ExecuteNonQuery();

                MessageBox.Show("Succesfully Added");
                
                amountTxt.Text = "";
                startingMonthComboBox.Text = "";
                endingMonthComboBox.Text = "";
            }
        }
    }
}
