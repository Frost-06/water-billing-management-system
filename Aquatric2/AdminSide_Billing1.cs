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
    public partial class AdminSide_Billing1 : Form {
        SqlConnection AdminSide_BillingCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ryzen\source\repos\Aquatric2\Aquatric2\AquatricDatabase.mdf;Integrated Security=True;Connect Timeout=30");
        public static int jan, feb, mar, apr, may, jun, jul, aug, sep, oct, nov, dec;

        public AdminSide_Billing1() {
            InitializeComponent();
            AdminSide_BillingCon.Open();
            
        }

        private void AdminSide_Billing1_Load(object sender, EventArgs e) {

            userIDText.Text = AdminSide_Billing.idNumberToPass;

            SqlCommand comConsumptions = new SqlCommand("select sum(consumption) from MeterInformations  where userID=" + userIDText.Text + "", AdminSide_BillingCon);
            SqlCommand comConsumptions1 = new SqlCommand("select count(consumption) from MeterInformations  where userID=" + userIDText.Text + "", AdminSide_BillingCon);

            try {
                int com2 = (Int32)comConsumptions.ExecuteScalar();
                int com1 = (Int32)comConsumptions1.ExecuteScalar();
                double nums1 = com2 / com1;
                averageConsumptiontxt.Text = Math.Ceiling(nums1).ToString();
            } catch(Exception ex) {

            }

            //count the pending in consumption            

            //count the pending mounthly dues and total its balance
            SqlCommand comTblPending = new SqlCommand("select date,amount,status from MonthlyDues where userID=" + userIDText.Text + "", AdminSide_BillingCon);
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
                SqlCommand comTblPending1 = new SqlCommand("select userID,amount,status from MonthlyDues  where status LIKE '%" + pendingTxtBox.Text + "%' and  userID=" + userIDText.Text + "", AdminSide_BillingCon);

                //count for the number of pending status
                SqlCommand countNumberOfPendingData = new SqlCommand("select count (status) FROM MonthlyDues where userID=" + userIDText.Text + " and status LIKE '%" + pendingTxtBox.Text + "%'", AdminSide_BillingCon);

                //count for the number of status
                SqlCommand countNumberOfStatusData = new SqlCommand("select count (status) FROM MonthlyDues where userID=" + userIDText.Text + "", AdminSide_BillingCon);

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

                //balanceTxt.Text = monthlyAmount.ToString();

                rdr6.Close();

                //output into piechart 
                total = totalStatus - pendingData;
                rdr2.Close();

                //data for MeterInformations sql database get the consumption value
                SqlCommand comConsumption = new SqlCommand("select *from MeterInformations where userID=" + userIDText.Text + "order by date asc", AdminSide_BillingCon);
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

                    //consumptionAmountTxt.Text = consumptionAmount.ToString();
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
            SqlCommand comConsumption1 = new SqlCommand("select *from MeterInformations where userID=" + userIDText.Text + "order by date asc", AdminSide_BillingCon);
            try {
                SqlDataReader rdr7 = comConsumption1.ExecuteReader();

                if (rdr7.Read()) {
                    rdr7.Close();
                    SqlCommand comTblPending2 = new SqlCommand("select *from MeterInformations  where status LIKE '%" + pendingTxtBox.Text + "%' and  userID=" + userIDText.Text + "", AdminSide_BillingCon);

                    //count for the number of pending status
                    SqlCommand countNumberOfPendingData1 = new SqlCommand("select count (status) FROM MeterInformations where userID=" + userIDText.Text + " and status LIKE '%" + pendingTxtBox.Text + "%'", AdminSide_BillingCon);

                    //count for the number of status
                    SqlCommand countNumberOfStatusData1 = new SqlCommand("select count (status) FROM MeterInformations where userID=" + userIDText.Text + "", AdminSide_BillingCon);

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

                        //balanceTxt.Text = String.Format("{0:0.00}", (monthlyAmount + monthlyAmount3)).ToString();

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
                Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" },
                // force the separator step to 1, so it always display all labels
                Separator = new Separator {
                    Step = 1,
                    IsEnabled = false //disable it to make it invisible.
                },
                LabelsRotation = 1
            });

            cartesianChart1.AxisY.Add(new Axis {
                Title = "Consumption",
                MinValue = 0,

            });

            cartesianChart1.AxisX[0].Separator.StrokeThickness = 0;
            cartesianChart1.AxisY[0].Separator.StrokeThickness = 0;

            // totalAmountDueTxt.Text = ((double.Parse(currentWaterBillTxt.Text) + double.Parse(collectionDueTxt.Text)) + double.Parse(balanceTxt.Text)).ToString("0.00");

            //bar graph
            cartesianChart2.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Completed",
                    Values = new ChartValues<double> {(total+total3)},
                    Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#079FEA"),
                    MaxColumnWidth = 120
                },
            };


            //adding series will update and animate the chart automatically
            cartesianChart2.Series.Add(
                new ColumnSeries {
                    Title = "Pending",
                    Values = new ChartValues<double> { (pendingData + pendingData1) },
                    Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#38B7E8"),
                    MaxColumnWidth = 120
                });

            cartesianChart2.AxisX.Add(new Axis {
                Labels = new[] { "" },
                // force the separator step to 1, so it always display all labels
                Separator = new Separator {
                    Step = 1,
                    IsEnabled = false //disable it to make it invisible.
                },
                LabelsRotation = 1
            });

            cartesianChart2.AxisY.Add(new Axis {
                LabelFormatter = value => value.ToString("N"),
                MinValue = 0,
            });

            cartesianChart2.AxisX[0].Separator.StrokeThickness = 0;
            cartesianChart2.AxisY[0].Separator.StrokeThickness = 0;
            totalPaidTxt.Text = (total + total3).ToString();
        }
    }
}
