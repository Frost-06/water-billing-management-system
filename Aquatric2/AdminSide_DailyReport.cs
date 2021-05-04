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
using System;

namespace Aquatric2 {
    public partial class AdminSide_DailyReport : Form {
        SqlConnection AdminSide_DailyReportCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ryzen\source\repos\Aquatric2\Aquatric2\AquatricDatabase.mdf;Integrated Security=True;Connect Timeout=30");
        public static double actDay1,actDay2, actDay3, actDay4, actDay5, actDay6, actDay7, actDay8, actDay9, actDay10, actDay11, actDay12, actDay13, actDay14, actDay15, actDay16, actDay17, actDay18, actDay19, actDay20, actDay21, actDay22, actDay23, actDay24, actDay25, actDay26, actDay27, actDay28, actDay29, actDay30, actDay31;
        public static double nonDay1, nonDay2, nonDay3, nonDay4, nonDay5, nonDay6, nonDay7, nonDay8, nonDay9, nonDay10, nonDay11, nonDay12, nonDay13, nonDay14, nonDay15, nonDay16, nonDay17, nonDay18, nonDay19, nonDay20, nonDay21, nonDay22, nonDay23, nonDay24, nonDay25, nonDay26, nonDay27, nonDay28, nonDay29, nonDay30, nonDay31;

        public static double aactDay1, aactDay2, aactDay3, aactDay4, aactDay5, aactDay6, aactDay7, aactDay8, aactDay9, aactDay10, aactDay11, aactDay12, aactDay13, aactDay14, aactDay15, aactDay16, aactDay17, aactDay18, aactDay19, aactDay20, aactDay21, aactDay22, aactDay23, aactDay24, aactDay25, aactDay26, aactDay27, aactDay28, aactDay29, aactDay30, aactDay31;
        public static double nnonDay1, nnonDay2, nnonDay3, nnonDay4, nnonDay5, nnonDay6, nnonDay7, nnonDay8, nnonDay9, nnonDay10, nnonDay11, nnonDay12, nnonDay13, nnonDay14, nnonDay15, nnonDay16, nnonDay17, nnonDay18, nnonDay19, nnonDay20, nnonDay21, nnonDay22, nnonDay23, nnonDay24, nnonDay25, nnonDay26, nnonDay27, nnonDay28, nnonDay29, nnonDay30, nnonDay31;
       
        public AdminSide_DailyReport() {
            InitializeComponent();
            AdminSide_DailyReportCon.Open();

            MonthlyConsumptionGrid.ColumnCount = 2;
            collectionGrid.ColumnCount = 2;
        }
        public static int dateForComboBox = 0;
        private void AdminSide_DailyReport_Load(object sender, EventArgs e) {
            actDay1 = 0; actDay2 = 0; actDay3 = 0; actDay4 = 0; actDay5 = 0; actDay6 = 0; actDay7 = 0; actDay8 = 0; actDay9 = 0; actDay10 = 0; 
            actDay11 = 0; actDay12 = 0; actDay13 = 0; actDay14 = 0; actDay15 = 0; actDay16 = 0; actDay17 = 0; actDay18 = 0; actDay19 = 0; actDay20 = 0; 
            actDay21 = 0; actDay22 = 0; actDay23 = 0; actDay24 = 0; actDay25 = 0; actDay26 = 0; actDay27 = 0; actDay28 = 0; actDay29 = 0; actDay30 = 0; actDay31 = 0;
            nonDay1 = 0; nonDay2 = 0; nonDay3 = 0; nonDay4 = 0; nonDay5 = 0; nonDay6 = 0; nonDay7 = 0; nonDay8 = 0; nonDay9 = 0; nonDay10 = 0; 
            nonDay11 = 0; nonDay12 = 0; nonDay13 = 0; nonDay14 = 0; nonDay15 = 0; nonDay16 = 0; nonDay17 = 0; nonDay18 = 0; nonDay19 = 0; nonDay20 = 0; 
            nonDay21 = 0; nonDay22 = 0; nonDay23 = 0; nonDay24 = 0; nonDay25 = 0; nonDay26 = 0; nonDay27 = 0; nonDay28 = 0; nonDay29 = 0; nonDay30 = 0; nonDay31 = 0;

            aactDay1 = 0; aactDay2 = 0; aactDay3 = 0; aactDay4 = 0; aactDay5 = 0; aactDay6 = 0; aactDay7 = 0; aactDay8 = 0; aactDay9 = 0; aactDay10 = 0; 
            aactDay11 = 0; aactDay12 = 0; aactDay13 = 0; aactDay14 = 0; aactDay15 = 0; aactDay16 = 0; aactDay17 = 0; aactDay18 = 0; aactDay19 = 0; aactDay20 = 0; 
            aactDay21 = 0; aactDay22 = 0; aactDay23 = 0; aactDay24 = 0; aactDay25 = 0; aactDay26 = 0; aactDay27 = 0; aactDay28 = 0; aactDay29 = 0; aactDay30 = 0; aactDay31 = 0;
            nnonDay1 = 0; nnonDay2 = 0; nnonDay3 = 0; nnonDay4 = 0; nnonDay5 = 0; nnonDay6 = 0; nnonDay7 = 0; nnonDay8 = 0; nnonDay9 = 0; nnonDay10 = 0; 
            nnonDay11 = 0; nnonDay12 = 0; nnonDay13 = 0; nnonDay14 = 0; nnonDay15 = 0; nnonDay16 = 0; nnonDay17 = 0; nnonDay18 = 0; nnonDay19 = 0; nnonDay20 = 0; 
            nnonDay21 = 0; nnonDay22 = 0; nnonDay23 = 0; nnonDay24 = 0; nnonDay25 = 0; nnonDay26 = 0; nnonDay27 = 0; nnonDay28 = 0; nnonDay29 = 0; nnonDay30 = 0; nnonDay31 = 0;
            //total users
            SqlCommand comMonth = new SqlCommand("select count(consumption) FROM MeterInformations where userID=1000", AdminSide_DailyReportCon);

            int counter1 = (Int32)comMonth.ExecuteScalar();
            switch (counter1) {
                case 1:
                    paymentComboBoxTab.SelectedItem = "January";
                    break;
                case 2:
                    paymentComboBoxTab.SelectedItem = "February";
                    break;
                case 3:
                    paymentComboBoxTab.SelectedItem = "March";
                    break;
                case 4:
                    paymentComboBoxTab.SelectedItem = "April";
                    break;
                case 5:
                    paymentComboBoxTab.SelectedItem = "May";
                    break;
                case 6:
                    paymentComboBoxTab.SelectedItem = "June";
                    break;
                case 7:
                    paymentComboBoxTab.SelectedItem = "July";
                    break;
                case 8:
                    paymentComboBoxTab.SelectedItem = "August";
                    break;
                case 9:
                    paymentComboBoxTab.SelectedItem = "September";
                    break;
                case 10:
                    paymentComboBoxTab.SelectedItem = "October";
                    break;
                case 11:
                    paymentComboBoxTab.SelectedItem = "November";
                    break;
                case 12:
                    paymentComboBoxTab.SelectedItem = "December";
                    break;
            }

            dailyreportChart.Series.Clear();
            dailyreportChart.Refresh();

            dailyreportChart1.Series.Clear();
            dailyreportChart1.Refresh();

            MonthlyConsumptionGrid.Rows.Clear();
            collectionGrid.Rows.Clear();

            totalEarningsLbl.Text = "";
            pendingPaymentLbl.Text = "";

            performComboBox(counter1);
            perform1ComboBox(counter1);
        }
        
        private void paymentComboBoxTab_SelectedIndexChanged(object sender, EventArgs e) {
            if(paymentComboBoxTab.SelectedItem == "January") {
                dateForComboBox = 1;
                dailyreportChart.Series.Clear();
                dailyreportChart.Refresh();

                dailyreportChart1.Series.Clear();
                dailyreportChart1.Refresh();

                MonthlyConsumptionGrid.Rows.Clear();
                collectionGrid.Rows.Clear();

                totalEarningsLbl.Text = "";
                pendingPaymentLbl.Text = "";

                dailyReportPanel.Visible = true;

                AdminSide_DailyReport1 frm = new AdminSide_DailyReport1() {
                    TopLevel = false,
                    TopMost = true
                };
                frm.FormBorderStyle = FormBorderStyle.None;
                this.dailyReportPanel.Controls.Add(frm);
                frm.Show();
                frm.BringToFront();

            } else if (paymentComboBoxTab.SelectedItem == "February") {
                dateForComboBox = 2;
                dailyreportChart.Series.Clear();
                dailyreportChart.Refresh();

                dailyreportChart1.Series.Clear();
                dailyreportChart1.Refresh();

                MonthlyConsumptionGrid.Rows.Clear();
                collectionGrid.Rows.Clear();

                totalEarningsLbl.Text = "";
                pendingPaymentLbl.Text = "";

                dailyReportPanel.Visible = true;

                AdminSide_DailyReport1 frm = new AdminSide_DailyReport1() {
                    TopLevel = false,
                    TopMost = true
                };
                frm.FormBorderStyle = FormBorderStyle.None;
                this.dailyReportPanel.Controls.Add(frm);
                frm.Show();
                frm.BringToFront();
            } else if (paymentComboBoxTab.SelectedItem == "March") {
                dateForComboBox = 3;
                dailyreportChart.Series.Clear();
                dailyreportChart.Refresh();

                dailyreportChart1.Series.Clear();
                dailyreportChart1.Refresh();

                MonthlyConsumptionGrid.Rows.Clear();
                collectionGrid.Rows.Clear();

                totalEarningsLbl.Text = "";
                pendingPaymentLbl.Text = "";

                dailyReportPanel.Visible = true;

                AdminSide_DailyReport1 frm = new AdminSide_DailyReport1() {
                    TopLevel = false,
                    TopMost = true
                };
                frm.FormBorderStyle = FormBorderStyle.None;
                this.dailyReportPanel.Controls.Add(frm);
                frm.Show();
                frm.BringToFront();
            } else if (paymentComboBoxTab.SelectedItem == "April") {
                dateForComboBox = 4;
                dailyreportChart.Series.Clear();
                dailyreportChart.Refresh();

                dailyreportChart1.Series.Clear();
                dailyreportChart1.Refresh();

                MonthlyConsumptionGrid.Rows.Clear();
                collectionGrid.Rows.Clear();

                totalEarningsLbl.Text = "";
                pendingPaymentLbl.Text = "";

                dailyReportPanel.Visible = true;

                AdminSide_DailyReport1 frm = new AdminSide_DailyReport1() {
                    TopLevel = false,
                    TopMost = true
                };
                frm.FormBorderStyle = FormBorderStyle.None;
                this.dailyReportPanel.Controls.Add(frm);
                frm.Show();
                frm.BringToFront();
            } else if (paymentComboBoxTab.SelectedItem == "May") {
                dateForComboBox = 5;
                dailyreportChart.Series.Clear();
                dailyreportChart.Refresh();

                dailyreportChart1.Series.Clear();
                dailyreportChart1.Refresh();

                MonthlyConsumptionGrid.Rows.Clear();
                collectionGrid.Rows.Clear();

                totalEarningsLbl.Text = "";
                pendingPaymentLbl.Text = "";

                dailyReportPanel.Visible = true;

                AdminSide_DailyReport1 frm = new AdminSide_DailyReport1() {
                    TopLevel = false,
                    TopMost = true
                };
                frm.FormBorderStyle = FormBorderStyle.None;
                this.dailyReportPanel.Controls.Add(frm);
                frm.Show();
                frm.BringToFront();
            } else if (paymentComboBoxTab.SelectedItem == "June") {
                dateForComboBox = 6;
                dailyreportChart.Series.Clear();
                dailyreportChart.Refresh();

                dailyreportChart1.Series.Clear();
                dailyreportChart1.Refresh();

                MonthlyConsumptionGrid.Rows.Clear();
                collectionGrid.Rows.Clear();

                totalEarningsLbl.Text = "";
                pendingPaymentLbl.Text = "";

                dailyReportPanel.Visible = true;

                AdminSide_DailyReport1 frm = new AdminSide_DailyReport1() {
                    TopLevel = false,
                    TopMost = true
                };
                frm.FormBorderStyle = FormBorderStyle.None;
                this.dailyReportPanel.Controls.Add(frm);
                frm.Show();
                frm.BringToFront();
            } else if (paymentComboBoxTab.SelectedItem == "July") {
                dateForComboBox = 7;
                dailyreportChart.Series.Clear();
                dailyreportChart.Refresh();

                dailyreportChart1.Series.Clear();
                dailyreportChart1.Refresh();

                MonthlyConsumptionGrid.Rows.Clear();
                collectionGrid.Rows.Clear();

                totalEarningsLbl.Text = "";
                pendingPaymentLbl.Text = "";

                dailyReportPanel.Visible = true;

                AdminSide_DailyReport1 frm = new AdminSide_DailyReport1() {
                    TopLevel = false,
                    TopMost = true
                };
                frm.FormBorderStyle = FormBorderStyle.None;
                this.dailyReportPanel.Controls.Add(frm);
                frm.Show();
                frm.BringToFront();
            } else if (paymentComboBoxTab.SelectedItem == "August") {
                dateForComboBox = 8;
                dailyreportChart.Series.Clear();
                dailyreportChart.Refresh();

                dailyreportChart1.Series.Clear();
                dailyreportChart1.Refresh();

                MonthlyConsumptionGrid.Rows.Clear();
                collectionGrid.Rows.Clear();

                totalEarningsLbl.Text = "";
                pendingPaymentLbl.Text = "";

                dailyReportPanel.Visible = true;

                AdminSide_DailyReport1 frm = new AdminSide_DailyReport1() {
                    TopLevel = false,
                    TopMost = true
                };
                frm.FormBorderStyle = FormBorderStyle.None;
                this.dailyReportPanel.Controls.Add(frm);
                frm.Show();
                frm.BringToFront();
            } else if (paymentComboBoxTab.SelectedItem == "September") {
                dateForComboBox = 9;
                dailyreportChart.Series.Clear();
                dailyreportChart.Refresh();

                dailyreportChart1.Series.Clear();
                dailyreportChart1.Refresh();

                MonthlyConsumptionGrid.Rows.Clear();
                collectionGrid.Rows.Clear();

                totalEarningsLbl.Text = "";
                pendingPaymentLbl.Text = "";

                dailyReportPanel.Visible = true;

                AdminSide_DailyReport1 frm = new AdminSide_DailyReport1() {
                    TopLevel = false,
                    TopMost = true
                };
                frm.FormBorderStyle = FormBorderStyle.None;
                this.dailyReportPanel.Controls.Add(frm);
                frm.Show();
                frm.BringToFront();
            } else if (paymentComboBoxTab.SelectedItem == "October") {
                dateForComboBox = 10;
                dailyreportChart.Series.Clear();
                dailyreportChart.Refresh();

                dailyreportChart1.Series.Clear();
                dailyreportChart1.Refresh();

                MonthlyConsumptionGrid.Rows.Clear();
                collectionGrid.Rows.Clear();

                totalEarningsLbl.Text = "";
                pendingPaymentLbl.Text = "";

                dailyReportPanel.Visible = true;

                AdminSide_DailyReport1 frm = new AdminSide_DailyReport1() {
                    TopLevel = false,
                    TopMost = true
                };
                frm.FormBorderStyle = FormBorderStyle.None;
                this.dailyReportPanel.Controls.Add(frm);
                frm.Show();
                frm.BringToFront();
            } else if (paymentComboBoxTab.SelectedItem == "November") {
                dateForComboBox = 11;
                dailyreportChart.Series.Clear();
                dailyreportChart.Refresh();

                dailyreportChart1.Series.Clear();
                dailyreportChart1.Refresh();

                MonthlyConsumptionGrid.Rows.Clear();
                collectionGrid.Rows.Clear();

                totalEarningsLbl.Text = "";
                pendingPaymentLbl.Text = "";

                dailyReportPanel.Visible = true;

                AdminSide_DailyReport1 frm = new AdminSide_DailyReport1() {
                    TopLevel = false,
                    TopMost = true
                };
                frm.FormBorderStyle = FormBorderStyle.None;
                this.dailyReportPanel.Controls.Add(frm);
                frm.Show();
                frm.BringToFront();
            } else if (paymentComboBoxTab.SelectedItem == "December") {
                dateForComboBox = 12;
                dailyreportChart.Series.Clear();
                dailyreportChart.Refresh();

                dailyreportChart1.Series.Clear();
                dailyreportChart1.Refresh();

                MonthlyConsumptionGrid.Rows.Clear();
                collectionGrid.Rows.Clear();

                totalEarningsLbl.Text = "";
                pendingPaymentLbl.Text = "";

                dailyReportPanel.Visible = true;

                AdminSide_DailyReport1 frm = new AdminSide_DailyReport1() {
                    TopLevel = false,
                    TopMost = true
                };
                frm.FormBorderStyle = FormBorderStyle.None;
                this.dailyReportPanel.Controls.Add(frm);
                frm.Show();
                frm.BringToFront();
            }
        }

        string status = "completed";
        string status1 = "pending";

        //january
        private void performComboBox(int dates) {    
            
            int count = 0;

            string[] day = new string[31];
            double[] amount = new double[31];

            SqlCommand comRetrieveMonth = new SqlCommand("select day(date),sum(amount),status from MeterInformations where month(date)=" + dates + " and status LIKE '%" + status + "%' group by day(date),status order by day(date)", AdminSide_DailyReportCon);

            SqlDataReader reader2 = comRetrieveMonth.ExecuteReader();

            while (reader2.Read()) {
                count++;
                day[count] = reader2[0].ToString();
                amount[count] = double.Parse(reader2[1].ToString());

                string[] tblRow1 = {
                    day[count],amount[count].ToString()
                };

                MonthlyConsumptionGrid.Rows.Add(tblRow1);


                switch (day[count]) {
                    case "1":
                        actDay1 = amount[count];
                        break;

                    case "2":
                        actDay2 = amount[count];
                        break;

                    case "3":
                        actDay3 = amount[count];
                        break;

                    case "4":
                        actDay4 = amount[count];
                        break;

                    case "5":
                        actDay5 = amount[count];
                        break;

                    case "6":
                        actDay6 = amount[count];
                        break;

                    case "7":
                        actDay7 = amount[count];
                        break;

                    case "8":
                        actDay8 = amount[count];
                        break;

                    case "9":
                        actDay9 = amount[count];
                        break;

                    case "10":
                        actDay10 = amount[count];
                        break;

                    case "11":
                        actDay11 = amount[count];
                        break;

                    case "12":
                        actDay12 = amount[count];
                        break;

                    case "13":
                        actDay13 = amount[count];
                        break;

                    case "14":
                        actDay14 = amount[count];
                        break;

                    case "15":
                        actDay15 = amount[count];
                        break;

                    case "16":
                        actDay16 = amount[count];
                        break;

                    case "17":
                        actDay17 = amount[count];
                        break;

                    case "18":
                        actDay18 = amount[count];
                        break;

                    case "19":
                        actDay19 = amount[count];
                        break;

                    case "20":
                        actDay20 = amount[count];
                        break;

                    case "21":
                        actDay21 = amount[count];
                        break;

                    case "22":
                        actDay22 = amount[count];
                        break;

                    case "23":
                        actDay23 = amount[count];
                        break;

                    case "24":
                        actDay24 = amount[count];
                        break;

                    case "25":
                        actDay25 = amount[count];
                        break;

                    case "26":
                        actDay26 = amount[count];
                        break;

                    case "27":
                        actDay27 = amount[count];
                        break;

                    case "28":
                        actDay28 = amount[count];
                        break;

                    case "29":
                        actDay29 = amount[count];
                        break;

                    case "30":
                        actDay30 = amount[count];
                        break;

                    case "31":
                        actDay31 = amount[count];
                        break;
                }
            }

            reader2.Close();

            int count1 = 0;

            string[] day2 = new string[31];
            double[] amount2 = new double[31];

            SqlCommand comRetrieveMonth1 = new SqlCommand("select day(date),sum(amount),status from MeterInformations where month(date)=" + dates + " and status LIKE '%" + status1 + "%' group by day(date),status order by day(date)", AdminSide_DailyReportCon);

            SqlDataReader reader3 = comRetrieveMonth1.ExecuteReader();

            while (reader3.Read()) {
                count1++;
                day2[count1] = reader3[0].ToString();
                amount2[count1] = double.Parse(reader3[1].ToString());

                switch (day2[count1]) {
                    case "1":
                        nonDay1 = amount2[count1];
                        break;

                    case "2":
                        nonDay2 = amount2[count1];
                        break;

                    case "3":
                        nonDay3 = amount2[count1];
                        break;

                    case "4":
                        nonDay4 = amount2[count1];
                        break;

                    case "5":
                        nonDay5 = amount2[count1];
                        break;

                    case "6":
                        nonDay6 = amount2[count1];
                        break;

                    case "7":
                        nonDay7 = amount2[count1];
                        break;

                    case "8":
                        nonDay8 = amount2[count1];
                        break;

                    case "9":
                        nonDay9 = amount2[count1];
                        break;

                    case "10":
                        nonDay10 = amount2[count1];
                        break;

                    case "11":
                        nonDay11 = amount2[count1];
                        break;

                    case "12":
                        nonDay12 = amount2[count1];
                        break;

                    case "13":
                        nonDay13 = amount2[count1];
                        break;

                    case "14":
                        nonDay14 = amount2[count1];
                        break;

                    case "15":
                        nonDay15 = amount2[count1];
                        break;

                    case "16":
                        nonDay16 = amount2[count1];
                        break;

                    case "17":
                        nonDay17 = amount2[count1];
                        break;

                    case "18":
                        nonDay18 = amount2[count1];
                        break;

                    case "19":
                        nonDay19 = amount2[count1];
                        break;

                    case "20":
                        nonDay20 = amount2[count1];
                        break;

                    case "21":
                        nonDay21 = amount2[count1];
                        break;

                    case "22":
                        nonDay22 = amount2[count1];
                        break;

                    case "23":
                        nonDay23 = amount2[count1];
                        break;

                    case "24":
                        nonDay24 = amount2[count1];
                        break;

                    case "25":
                        nonDay25 = amount2[count1];
                        break;

                    case "26":
                        nonDay26 = amount2[count1];
                        break;

                    case "27":
                        nonDay27 = amount2[count1];
                        break;

                    case "28":
                        nonDay28 = amount2[count1];
                        break;

                    case "29":
                        nonDay29 = amount2[count1];
                        break;

                    case "30":
                        nonDay30 = amount2[count1];
                        break;

                    case "31":
                        nonDay31 = amount2[count1];
                        break;
                }
            }

            reader3.Close();

            dailyreportChart.Refresh();

            //water consumption daily report
            dailyreportChart.Series = new SeriesCollection {
                new ColumnSeries {
                    Title = "Completed",
                    Values = new ChartValues<double> { actDay1,actDay2, actDay3, actDay4, actDay5, actDay6, actDay7, actDay8, actDay9, actDay10, actDay11, actDay12, actDay13, actDay14, actDay15, actDay16, actDay17, actDay18, actDay19, actDay20, actDay21, actDay22, actDay23, actDay24, actDay25, actDay26, actDay27, actDay28, actDay29, actDay30, actDay31},
                     Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#079FEA"),

                },

                new ColumnSeries {
                    Title = "Pending",
                    Values = new ChartValues<double> { nonDay1, nonDay2, nonDay3, nonDay4, nonDay5, nonDay6, nonDay7, nonDay8, nonDay9, nonDay10, nonDay11, nonDay12, nonDay13, nonDay14, nonDay15, nonDay16, nonDay17, nonDay18, nonDay19, nonDay20, nonDay21, nonDay22, nonDay23, nonDay24, nonDay25, nonDay26, nonDay27, nonDay28, nonDay29, nonDay30, nonDay31 },
                    Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#e23939"),
                }
            };

            dailyreportChart.AxisX.Add(new Axis {
                Title = "Days",
                Labels = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "6", "27", "28", "29", "30", "31" }
            });

            dailyreportChart.AxisY.Add(new Axis {
                Title = "Earnings",
                LabelFormatter = value => value.ToString("N")
            });

            dailyreportChart.AxisX[0].Separator.StrokeThickness = 0;
            dailyreportChart.AxisY[0].Separator.StrokeThickness = 0;

            totalEarningsLbl.Text = String.Format("{0:0.00}", (actDay1 + actDay2 + actDay3 + actDay4 + actDay5 + actDay6 + actDay7 + actDay8 + actDay9 + actDay10 + actDay11 + actDay12 + actDay13 + actDay14 + actDay15 + actDay16 + actDay17 + actDay18 + actDay19 + actDay20 + actDay21 + actDay22 + actDay23 + actDay24 + actDay25 + actDay26 + actDay27 + actDay28 + actDay29 + actDay30 + actDay31)).ToString();
            pendingPaymentLbl.Text = String.Format("{0:0.00}", (nonDay1 + nonDay2 + nonDay3 + nonDay4 + nonDay5 + nonDay6 + nonDay7 + nonDay8 + nonDay9 + nonDay10 + nonDay11 + nonDay12 + nonDay13 + nonDay14 + nonDay15 + nonDay16 + nonDay17 + nonDay18 + nonDay19 + nonDay20 + nonDay21 + nonDay22 + nonDay23 + nonDay24 + nonDay25 + nonDay26 + nonDay27 + nonDay28 + nonDay29 + nonDay30 + nonDay31)).ToString();
        }

        private void perform1ComboBox(int dates) {
            //homeowners collection
            int count2 = 0;

            string[] day3 = new string[31];
            double[] amount3 = new double[31];

            SqlCommand comRetrieveMonth2 = new SqlCommand("select day(date),sum(amount),status from MonthlyDues where month(date)=" + dates + " and status LIKE '%" + status + "%' group by day(date),status order by day(date)", AdminSide_DailyReportCon);

            SqlDataReader reader4 = comRetrieveMonth2.ExecuteReader();

            while (reader4.Read()) {
                count2++;
                day3[count2] = reader4[0].ToString();
                amount3[count2] = double.Parse(reader4[1].ToString());

                string[] tblRow1 = {
                    day3[count2],amount3[count2].ToString()
                };

                collectionGrid.Rows.Add(tblRow1);

                switch (day3[count2]) {
                    case "1":
                        aactDay1 = amount3[count2];
                        break;

                    case "2":
                        aactDay2 = amount3[count2];
                        break;

                    case "3":
                        aactDay3 = amount3[count2];
                        break;

                    case "4":
                        aactDay4 = amount3[count2];
                        break;

                    case "5":
                        aactDay5 = amount3[count2];
                        break;

                    case "6":
                        aactDay6 = amount3[count2];
                        break;

                    case "7":
                        aactDay7 = amount3[count2];
                        break;

                    case "8":
                        aactDay8 = amount3[count2];
                        break;

                    case "9":
                        aactDay9 = amount3[count2];
                        break;

                    case "10":
                        aactDay10 = amount3[count2];
                        break;

                    case "11":
                        aactDay11 = amount3[count2];
                        break;

                    case "12":
                        aactDay12 = amount3[count2];
                        break;

                    case "13":
                        aactDay13 = amount3[count2];
                        break;

                    case "14":
                        aactDay14 = amount3[count2];
                        break;

                    case "15":
                        aactDay15 = amount3[count2];
                        break;

                    case "16":
                        aactDay16 = amount3[count2];
                        break;

                    case "17":
                        aactDay17 = amount3[count2];
                        break;

                    case "18":
                        aactDay18 = amount3[count2];
                        break;

                    case "19":
                        aactDay19 = amount3[count2];
                        break;

                    case "20":
                        aactDay20 = amount3[count2];
                        break;

                    case "21":
                        aactDay21 = amount3[count2];
                        break;

                    case "22":
                        aactDay22 = amount3[count2];
                        break;

                    case "23":
                        aactDay23 = amount3[count2];
                        break;

                    case "24":
                        aactDay24 = amount3[count2];
                        break;

                    case "25":
                        aactDay25 = amount3[count2];
                        break;

                    case "26":
                        aactDay26 = amount3[count2];
                        break;

                    case "27":
                        aactDay27 = amount3[count2];
                        break;

                    case "28":
                        aactDay28 = amount3[count2];
                        break;

                    case "29":
                        aactDay29 = amount3[count2];
                        break;

                    case "30":
                        aactDay30 = amount3[count2];
                        break;

                    case "31":
                        aactDay31 = amount3[count2];
                        break;
                }
            }

            reader4.Close();

            int count4 = 0;

            string[] day4 = new string[31];
            double[] amount4 = new double[31];

            SqlCommand comRetrieveMonth4 = new SqlCommand("select day(date),sum(amount),status from MonthlyDues where month(date)=" + dates + " and status LIKE '%" + status1 + "%' group by day(date),status order by day(date)", AdminSide_DailyReportCon);

            SqlDataReader reader5 = comRetrieveMonth4.ExecuteReader();

            while (reader5.Read()) {
                count4++;
                day4[count4] = reader5[0].ToString();
                amount4[count4] = double.Parse(reader5[1].ToString());

                switch (day4[count4]) {
                    case "1":
                        nnonDay1 = amount4[count4];
                        break;

                    case "2":
                        nnonDay2 = amount4[count4];
                        break;

                    case "3":
                        nnonDay3 = amount4[count4];
                        break;

                    case "4":
                        nnonDay4 = amount4[count4];
                        break;

                    case "5":
                        nnonDay5 = amount4[count4];
                        break;

                    case "6":
                        nnonDay6 = amount4[count4];
                        break;

                    case "7":
                        nnonDay7 = amount4[count4];
                        break;

                    case "8":
                        nnonDay8 = amount4[count4];
                        break;

                    case "9":
                        nnonDay9 = amount4[count4];
                        break;

                    case "10":
                        nnonDay10 = amount4[count4];
                        break;

                    case "11":
                        nnonDay11 = amount4[count4];
                        break;

                    case "12":
                        nnonDay12 = amount4[count4];
                        break;

                    case "13":
                        nnonDay13 = amount4[count4];
                        break;

                    case "14":
                        nnonDay14 = amount4[count4];
                        break;

                    case "15":
                        nnonDay15 = amount4[count4];
                        break;

                    case "16":
                        nnonDay16 = amount4[count4];
                        break;

                    case "17":
                        nnonDay17 = amount4[count4];
                        break;

                    case "18":
                        nnonDay18 = amount4[count4];
                        break;

                    case "19":
                        nnonDay19 = amount4[count4];
                        break;

                    case "20":
                        nnonDay20 = amount4[count4];
                        break;

                    case "21":
                        nnonDay21 = amount4[count4];
                        break;

                    case "22":
                        nnonDay22 = amount4[count4];
                        break;

                    case "23":
                        nnonDay23 = amount4[count4];
                        break;

                    case "24":
                        nnonDay24 = amount4[count4];
                        break;

                    case "25":
                        nnonDay25 = amount4[count4];
                        break;

                    case "26":
                        nnonDay26 = amount4[count4];
                        break;

                    case "27":
                        nnonDay27 = amount4[count4];
                        break;

                    case "28":
                        nnonDay28 = amount4[count4];
                        break;

                    case "29":
                        nnonDay29 = amount4[count4];
                        break;

                    case "30":
                        nnonDay30 = amount4[count4];
                        break;

                    case "31":
                        nnonDay31 = amount4[count4];
                        break;
                }
            }

            reader5.Close();

            dailyreportChart.Refresh();

            //homeowner daily report
            dailyreportChart1.Series = new SeriesCollection {
                new ColumnSeries {
                    Title = "Completed",
                    Values = new ChartValues<double> { aactDay1, aactDay2, aactDay3, aactDay4, aactDay5, aactDay6, aactDay7, aactDay8, aactDay9, aactDay10, aactDay11, aactDay12, aactDay13, aactDay14, aactDay15, aactDay16, aactDay17, aactDay18, aactDay19, aactDay20, aactDay21, aactDay22, aactDay23, aactDay24, aactDay25, aactDay26, aactDay27, aactDay28, aactDay29, aactDay30, aactDay31 },
                     Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#079FEA"),

                },

                new ColumnSeries {
                    Title = "Pending",
                    Values = new ChartValues<double> { nnonDay1, nnonDay2, nnonDay3, nnonDay4, nnonDay5, nnonDay6, nnonDay7, nnonDay8, nnonDay9, nnonDay10, nnonDay11, nnonDay12, nnonDay13, nnonDay14, nnonDay15, nnonDay16, nnonDay17, nnonDay18, nnonDay19, nnonDay20, nnonDay21, nnonDay22, nnonDay23, nnonDay24, nnonDay25, nnonDay26, nnonDay27, nnonDay28, nnonDay29, nnonDay30, nnonDay31 },
                    Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#e23939"),
                }
            };

            dailyreportChart1.AxisX.Add(new Axis {
                Title = "Days",
                Labels = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "6", "27", "28", "29", "30", "31" }
            });

            dailyreportChart1.AxisY.Add(new Axis {
                Title = "Earnings",
                LabelFormatter = value => value.ToString("N")
            });

            dailyreportChart1.AxisX[0].Separator.StrokeThickness = 0;
            dailyreportChart1.AxisY[0].Separator.StrokeThickness = 0;

            totalEarningsLbl1.Text = String.Format("{0:0.00}", (aactDay1 + aactDay2 + aactDay3 + aactDay4 + aactDay5 + aactDay6 + aactDay7 + aactDay8 + aactDay9 + aactDay10 + aactDay11 + aactDay12 + aactDay13 + aactDay14 + aactDay15 + aactDay16 + aactDay17 + aactDay18 + aactDay19 + aactDay20 + aactDay21 + aactDay22 + aactDay23 + aactDay24 + aactDay25 + aactDay26 + aactDay27 + aactDay28 + aactDay29 + aactDay30 + aactDay31)).ToString();
            pendingPaymentLbl1.Text = String.Format("{0:0.00}", (nnonDay1 + nnonDay2 + nnonDay3 + nnonDay4 + nnonDay5 + nnonDay6 + nnonDay7 + nnonDay8 + nnonDay9 + nnonDay10 + nnonDay11 + nnonDay12 + nnonDay13 + nnonDay14 + nnonDay15 + nnonDay16 + nnonDay17 + nnonDay18 + nnonDay19 + nnonDay20 + nnonDay21 + nnonDay22 + nnonDay23 + nnonDay24 + nnonDay25 + nnonDay26 + nnonDay27 + nnonDay28 + nnonDay29 + nnonDay30 + nnonDay31)).ToString();
        }
    }
}
