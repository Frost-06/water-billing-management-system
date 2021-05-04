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

namespace Aquatric2 {
    public partial class AdminSide_UserReport : Form {
        SqlConnection AdminSide_UserReportCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ryzen\source\repos\Aquatric2\Aquatric2\AquatricDatabase.mdf;Integrated Security=True;Connect Timeout=30");
        public static int jan, feb, mar, apr, may, jun, jul, aug, sep, oct, nov, dec;
        public static string idNumberToPass;

        private void userIDComboBox_MouseHover(object sender, EventArgs e) {          
        }

        private void userIDComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            userIDTxt.Text = userIDComboBox.Text;
            userIDText.Text = userIDComboBox.Text;

            SqlCommand userData = new SqlCommand("select *from registrationTable where userID=" + userIDText.Text + "", AdminSide_UserReportCon);
            userData.ExecuteNonQuery();

            SqlDataReader rdrs = userData.ExecuteReader();
            string fname1="", lname1="";
            while (rdrs.Read()) {
                fname1 = rdrs["fname"].ToString();
                lname1 = rdrs["lname"].ToString();
                blockTxt.Text = rdrs["block"].ToString();
                lotTxt.Text = rdrs["lot"].ToString();
            }

            rdrs.Close();

            fnameText.Text = (char.ToUpper(fname1[0])) + fname1.Substring(1);
            lnameText.Text = (char.ToUpper(lname1[0])) + lname1.Substring(1);


            if (userIDText.Text == userIDTxt.Text) {
                idNumberToPass = userIDText.Text;
                userDashboard1Panel.Visible = true;
                userDashboard2Panel.Visible = true;

                AdminSide_UserReport1 frm = new AdminSide_UserReport1() {
                    TopLevel = false,
                    TopMost = true
                };
                frm.FormBorderStyle = FormBorderStyle.None;
                this.userDashboard1Panel.Controls.Add(frm);
                frm.Show();
                frm.BringToFront();
                
                AdminSide_UserReport2 frm1 = new AdminSide_UserReport2() {
                    TopLevel = false,
                    TopMost = true
                };
                frm1.FormBorderStyle = FormBorderStyle.None;
                this.userDashboard2Panel.Controls.Add(frm1);
                frm1.Show();
                frm1.BringToFront();               
            }  
        }

        private void AdminSide_UserReport_Load(object sender, EventArgs e) {
            
            SqlCommand userData = new SqlCommand("select *from registrationTable where userID=" + userIDText.Text + "", AdminSide_UserReportCon);
            userData.ExecuteNonQuery();

            SqlDataReader rdrs = userData.ExecuteReader();
            string fname1 = "", lname1 = "";
            while (rdrs.Read()) {
                fname1 = rdrs["fname"].ToString();
                lname1 = rdrs["lname"].ToString();
                blockTxt.Text = rdrs["block"].ToString();
                lotTxt.Text = rdrs["lot"].ToString();
            }

            rdrs.Close();

            fnameText.Text = (char.ToUpper(fname1[0])) + fname1.Substring(1);
            lnameText.Text = (char.ToUpper(lname1[0])) + lname1.Substring(1);

          

            SqlCommand userDataComboBox = new SqlCommand("select *from registrationTable", AdminSide_UserReportCon);
            userDataComboBox.ExecuteNonQuery();

            SqlDataReader rdr9 = userDataComboBox.ExecuteReader();

            while (rdr9.Read()) {
                userIDComboBox.Items.Add(rdr9["userID"].ToString());
            }

            rdr9.Close();

            //latest bills
          
            SqlCommand comLatestBills = new SqlCommand("select *from latestPayment where userID=" + userIDText.Text + "", AdminSide_UserReportCon);
            SqlDataReader readerForLatestBills = comLatestBills.ExecuteReader();

            while (readerForLatestBills.Read()) {
                dateTxt.Text = readerForLatestBills["latestDate"].ToString();
                waterconsumptionTxt.Text = readerForLatestBills["latestConsumption"].ToString();
                currentWaterBillTxt.Text = readerForLatestBills["latestConsumptionAmount"].ToString();
                collectionDueTxt.Text = readerForLatestBills["latestCollectionAmount"].ToString();
            }

            readerForLatestBills.Close();
            
            //count the pending in consumption            

            //count the pending mounthly dues and total its balance
            SqlCommand comTblPending = new SqlCommand("select date,amount,status from MonthlyDues where userID=" + userIDText.Text + "", AdminSide_UserReportCon);
            SqlDataReader rdr6 = comTblPending.ExecuteReader();

            double totalAmount;
            int counter = 0;
            double[] k = new double[13];
            double monthlyAmount = 0;
            int total = 0;
            int pendingData = 0;

            if (rdr6.Read()) {
                rdr6.Close();

                //selecting value that has pending status
                SqlCommand comTblPending1 = new SqlCommand("select userID,amount,status from MonthlyDues  where status LIKE '%" + pendingTxtBox.Text + "%' and  userID=" + userIDText.Text + "", AdminSide_UserReportCon);

                //count for the number of pending status
                SqlCommand countNumberOfPendingData = new SqlCommand("select count (status) FROM MonthlyDues where userID=" + userIDText.Text + " and status LIKE '%" + pendingTxtBox.Text + "%'", AdminSide_UserReportCon);

                //count for the number of status
                SqlCommand countNumberOfStatusData = new SqlCommand("select count (status) FROM MonthlyDues where userID=" + userIDText.Text + "", AdminSide_UserReportCon);

                //convert sqlcommand into int values
                pendingData = (Int32)countNumberOfPendingData.ExecuteScalar();

                int totalStatus = (Int32)countNumberOfStatusData.ExecuteScalar();

                SqlDataReader rdr2 = comTblPending1.ExecuteReader();

                while (rdr2.Read()) {

                    totalAmount = int.Parse(rdr2["amount"].ToString());
                    counter++;
                    k[counter] = totalAmount;

                    //balance of monthly dues
                    monthlyAmount = k[1] + k[2] + k[3] + k[4] + k[5] + k[6] + k[7] + k[8] + k[9] + k[10] + k[11] + k[12];
                }

                balanceTxt.Text = monthlyAmount.ToString();

                rdr6.Close();

                //output into piechart 
                total = totalStatus - pendingData;
                rdr2.Close();

                //data for MeterInformations sql database get the consumption value
                SqlCommand comConsumption = new SqlCommand("select *from MeterInformations where userID=" + userIDText.Text + "order by date asc", AdminSide_UserReportCon);
                SqlDataReader rdr3 = comConsumption.ExecuteReader();

                int[] consump = new int[12];
                int counter1 = 0;
                int[] months = new int[12];
                double[] k1 = new double[12];
                double consumptionAmount;
                int counter2 = 0;



                while (rdr3.Read()) {
                    counter1++;

                    k1[counter2] = double.Parse(rdr3["amount"].ToString());

                    consumptionAmount = k1[0] + k1[1] + k1[2] + k1[3] + k1[4] + k1[5] + k1[6] + k1[7] + k1[8] + k1[9] + k1[10] + k1[11];

                    counter2++;

                    consump[counter1] = int.Parse(rdr3["consumption"].ToString());
                    months[counter1] = consump[counter1];

                    consumptionAmountTxt.Text = consumptionAmount.ToString();
                }
                rdr3.Close();


                //check consumptions count if it reach 12(represents the number of months"december") ex: if number of months is 11("november") december will be=0 and so on...
                switch (counter1) {
                    case 12:
                        dec = months[12];
                        nov = months[11];
                        oct = months[10];
                        sep = months[9];
                        aug = months[8];
                        jul = months[7];
                        jun = months[6];
                        may = months[5];
                        apr = months[4];
                        mar = months[3];
                        feb = months[2];
                        jan = months[1];
                        break;

                    case 11:
                        dec = 0;
                        nov = months[11];
                        oct = months[10];
                        sep = months[9];
                        aug = months[8];
                        jul = months[7];
                        jun = months[6];
                        may = months[5];
                        apr = months[4];
                        mar = months[3];
                        feb = months[2];
                        jan = months[1];
                        break;

                    case 10:
                        dec = 0;
                        nov = 0;
                        oct = months[10];
                        sep = months[9];
                        aug = months[8];
                        jul = months[7];
                        jun = months[6];
                        may = months[5];
                        apr = months[4];
                        mar = months[3];
                        feb = months[2];
                        jan = months[1];
                        break;

                    case 9:
                        dec = 0;
                        nov = 0;
                        oct = 0;
                        sep = months[9];
                        aug = months[8];
                        jul = months[7];
                        jun = months[6];
                        may = months[5];
                        apr = months[4];
                        mar = months[3];
                        feb = months[2];
                        jan = months[1];
                        break;

                    case 8:
                        dec = 0;
                        nov = 0;
                        oct = 0;
                        sep = 0;
                        aug = months[8];
                        jul = months[7];
                        jun = months[6];
                        may = months[5];
                        apr = months[4];
                        mar = months[3];
                        feb = months[2];
                        jan = months[1];
                        break;

                    case 7:
                        dec = 0;
                        nov = 0;
                        oct = 0;
                        sep = 0;
                        aug = 0;
                        jul = months[7];
                        jun = months[6];
                        may = months[5];
                        apr = months[4];
                        mar = months[3];
                        feb = months[2];
                        jan = months[1];
                        break;

                    case 6:
                        dec = 0;
                        nov = 0;
                        oct = 0;
                        sep = 0;
                        aug = 0;
                        jul = 0;
                        jun = months[6];
                        may = months[5];
                        apr = months[4];
                        mar = months[3];
                        feb = months[2];
                        jan = months[1];
                        break;

                    case 5:
                        dec = 0;
                        nov = 0;
                        oct = 0;
                        sep = 0;
                        aug = 0;
                        jul = 0;
                        jun = 0;
                        may = months[5];
                        apr = months[4];
                        mar = months[3];
                        feb = months[2];
                        jan = months[1];
                        break;

                    case 4:
                        dec = 0;
                        nov = 0;
                        oct = 0;
                        sep = 0;
                        aug = 0;
                        jul = 0;
                        jun = 0;
                        may = 0;
                        apr = months[4];
                        mar = months[3];
                        feb = months[2];
                        jan = months[1];
                        break;

                    case 3:
                        dec = 0;
                        nov = 0;
                        oct = 0;
                        sep = 0;
                        aug = 0;
                        jul = 0;
                        jun = 0;
                        may = 0;
                        apr = 0;
                        mar = months[3];
                        feb = months[2];
                        jan = months[1];
                        break;

                    case 2:
                        dec = 0;
                        nov = 0;
                        oct = 0;
                        sep = 0;
                        aug = 0;
                        jul = 0;
                        jun = 0;
                        may = 0;
                        apr = 0;
                        mar = 0;
                        feb = months[2];
                        jan = months[1];
                        break;

                    case 1:
                        dec = 0;
                        nov = 0;
                        oct = 0;
                        sep = 0;
                        aug = 0;
                        jul = 0;
                        jun = 0;
                        may = 0;
                        apr = 0;
                        mar = 0;
                        feb = 0;
                        jan = months[1];
                        break;

                    default:
                        dec = 0;
                        nov = 0;
                        oct = 0;
                        sep = 0;
                        aug = 0;
                        jul = 0;
                        jun = 0;
                        may = 0;
                        apr = 0;
                        mar = 0;
                        feb = 0;
                        jan = 0;
                        break;

                }
            }


            int pendingData1 = 0;
            double totalAmount1;

            int counter3 = 0;
            double[] k3 = new double[13];
            double monthlyAmount3 = 0;
            int total3 = 0;

            //data for MeterInformations sql database get the consumption value
            SqlCommand comConsumption1 = new SqlCommand("select *from MeterInformations where userID=" + userIDText.Text + "order by date asc", AdminSide_UserReportCon);
            try {
                SqlDataReader rdr7 = comConsumption1.ExecuteReader();

                if (rdr7.Read()) {
                    rdr7.Close();
                    SqlCommand comTblPending2 = new SqlCommand("select *from MeterInformations  where status LIKE '%" + pendingTxtBox.Text + "%' and  userID=" + userIDText.Text + "", AdminSide_UserReportCon);

                    //count for the number of pending status
                    SqlCommand countNumberOfPendingData1 = new SqlCommand("select count (status) FROM MeterInformations where userID=" + userIDText.Text + " and status LIKE '%" + pendingTxtBox.Text + "%'", AdminSide_UserReportCon);

                    //count for the number of status
                    SqlCommand countNumberOfStatusData1 = new SqlCommand("select count (status) FROM MeterInformations where userID=" + userIDText.Text + "", AdminSide_UserReportCon);

                    //convert sqlcommand into int values
                    pendingData1 = (Int32)countNumberOfPendingData1.ExecuteScalar();
                    Console.WriteLine(pendingData1);
                    int totalStatus1 = (Int32)countNumberOfStatusData1.ExecuteScalar();

                    SqlDataReader rdr4 = comTblPending2.ExecuteReader();

                    while (rdr4.Read()) {

                        totalAmount1 = double.Parse(rdr4["amount"].ToString());
                        counter3++;
                        k3[counter] = totalAmount1;

                        //balance of consumption dues
                        monthlyAmount3 += k3[1] + k3[2] + k3[3] + k3[4] + k3[5] + k3[6] + k3[7] + k3[8] + k3[9] + k3[10] + k3[11] + k3[12];

                        Console.WriteLine(monthlyAmount3);
                        //output into bar graph

                         balanceTxt.Text = String.Format("{0:0.00}", (monthlyAmount + monthlyAmount3)).ToString();

                    }
                    total3 = totalStatus1 - pendingData1;
                    rdr4.Close();
                }
            } catch (Exception ex) {

            }




            //cartesian chart for consumption
            cartesianChart1.Series = new SeriesCollection {
                new LineSeries
                {
                    Title = "Monthly Consumption",
                    Values = new ChartValues<double> {jan, feb, mar, apr, may, jun, jul, aug, sep, oct, nov, dec},
                    PointGeometrySize = 9
                },
            };

            cartesianChart1.AxisX.Add(new Axis {
                Title = "Month",
                Labels = new[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" }
            });

            cartesianChart1.AxisY.Add(new Axis {
                Title = "Consumption",
                MinValue = 0,
            });

            cartesianChart1.AxisX[0].Separator.StrokeThickness = 0;
            cartesianChart1.AxisY[0].Separator.StrokeThickness = 0;


            //pie chart for pending balance
            /*
            pieChart1.Series = new SeriesCollection {
                new PieSeries
                {
                    Title = "Completed",
                    Values = new ChartValues<double> {total},
                    Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#079FEA")
                },
                new PieSeries
                {
                    Title = "Pending",
                    Values = new ChartValues<double> {pendingData},
                    Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#38B7E8")
                },
            };

            // Set the legend location to appear in the Right side of the chart
            pieChart1.LegendLocation = LegendLocation.Bottom;
            */

            //bar graph
            cartesianChart2.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Completed",
                    Values = new ChartValues<double> {total},
                    Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#079FEA"),
                    MaxColumnWidth = 120
                },
            };


            //adding series will update and animate the chart automatically
            cartesianChart2.Series.Add(
                new ColumnSeries {
                    Title = "Pending",
                    Values = new ChartValues<double> { pendingData },
                    Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#38B7E8"),
                    MaxColumnWidth = 120
                });

            cartesianChart2.AxisX.Add(new Axis {
                Labels = new[] { "" }
            });

            cartesianChart2.AxisY.Add(new Axis {
                LabelFormatter = value => value.ToString("N"),
                MinValue = 0,
            });

            cartesianChart2.AxisX[0].Separator.StrokeThickness = 0;
            cartesianChart2.AxisY[0].Separator.StrokeThickness = 0;

            //bar graph
            cartesianChart3.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Completed",
                    Values = new ChartValues<double> {total3},
                    Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#079FEA"),
                    MaxColumnWidth = 120
                },
            };


            //adding series will update and animate the chart automatically
            cartesianChart3.Series.Add(
                new ColumnSeries {
                    Title = "Pending",
                    Values = new ChartValues<double> { pendingData1 },
                    Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#38B7E8"),
                    MaxColumnWidth = 120
                });

            cartesianChart3.AxisX.Add(new Axis {
                Labels = new[] { "" }
            });

            cartesianChart3.AxisY.Add(new Axis {
                LabelFormatter = value => value.ToString("N"),
                MinValue = 0,
            });

            cartesianChart3.AxisX[0].Separator.StrokeThickness = 0;
            cartesianChart3.AxisY[0].Separator.StrokeThickness = 0;

            //total amount due for the month
             totalAmountDueTxt.Text = ((double.Parse(currentWaterBillTxt.Text) + double.Parse(collectionDueTxt.Text)) + double.Parse(balanceTxt.Text)).ToString("0.00");

            MonthlyConsumption.ColumnCount = 4;
            MonthlyCollectionGrid.ColumnCount = 3;

            // datagrid for monthly consumption

            string date, consumption, amount, status1;
            SqlCommand consumptionCom = new SqlCommand("select registrationTable.userID,MeterInformations.date,MeterInformations.consumption,MeterInformations.amount,MeterInformations.status from registrationTable,MeterInformations where registrationTable.userID=MeterInformations.userID and registrationTable.userID=" + userIDText.Text + "order by date desc", AdminSide_UserReportCon);
            consumptionCom.ExecuteNonQuery();

            SqlDataReader rdr = consumptionCom.ExecuteReader();

            while (rdr.Read()) {
                date = rdr["date"].ToString();
                consumption = rdr["consumption"].ToString();
                amount = rdr["amount"].ToString();
                status1 = rdr["status"].ToString();

                string[] tblRow = { date, consumption, amount, status1 };

                MonthlyConsumption.Rows.Add(tblRow);
            }

            rdr.Close();

            //data grid for monthly collections
            string date1, amount1, status;
            SqlCommand collectionCom = new SqlCommand("select registrationTable.userID,MonthlyDues.userID,MonthlyDues.date,MonthlyDues.amount,MonthlyDues.status from registrationTable,MonthlyDues where registrationTable.userID=MonthlyDues.userID and registrationTable.userID=" + userIDText.Text + "order by date desc", AdminSide_UserReportCon);
            collectionCom.ExecuteNonQuery();

            SqlDataReader rdr1 = collectionCom.ExecuteReader();

            while (rdr1.Read()) {
                date1 = rdr1["date"].ToString();
                amount1 = rdr1["amount"].ToString();
                status = rdr1["status"].ToString();

                string[] tblRow = { date1, amount1, status };

                MonthlyCollectionGrid.Rows.Add(tblRow);
            }

            rdr1.Close();
        }

        public AdminSide_UserReport() {
            InitializeComponent();
            AdminSide_UserReportCon.Open();

            
        }

    }
}
