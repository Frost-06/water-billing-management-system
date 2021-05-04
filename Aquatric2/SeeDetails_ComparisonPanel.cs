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
using System.Runtime.InteropServices;

namespace Aquatric2 {
    public partial class SeeDetails_ComparisonPanel : Form {
        SqlConnection SeeDetails_ComparisonPanelCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ryzen\source\repos\Aquatric2\Aquatric2\AquatricDatabase.mdf;Integrated Security=True;Connect Timeout=30");
        public static double jan, feb, mar, apr, may, jun, jul, aug, sep, oct, nov, dec;
        public static int jan1, feb1, mar1, apr1, may1, jun1, jul1, aug1, sep1, oct1, nov1, dec1;
        public static double jan2, feb2, mar2, apr2, may2, jun2, jul2, aug2, sep2, oct2, nov2, dec2;
        public static double jan3, feb3, mar3, apr3, may3, jun3, jul3, aug3, sep3, oct3, nov3, dec3;

        public SeeDetails_ComparisonPanel() {
            InitializeComponent();
            m_aeroEnabled = false;
            SeeDetails_ComparisonPanelCon.Open();

            MonthlyConsumptionGrid.ColumnCount = 3;
            gridMonthlyCollection.ColumnCount = 3;

        }

        private void SeeDetails_ComparisonPanel_Load(object sender, EventArgs e) {          
            //pending monthly consumption
            string pendingTxt = "pending";
            string[] monthWithPendingStatus = new string[12];
            double[] monthWithPendingConsumption = new double[12];
            int count1 = 0;

            SqlCommand comTblPendingofConsumption = new SqlCommand("select month(date),sum(amount),status from MeterInformations  where status LIKE '%" + pendingTxt + "%' group by month(date),status", SeeDetails_ComparisonPanelCon);

            SqlDataReader reader1 = comTblPendingofConsumption.ExecuteReader();

            while (reader1.Read()) {
                count1++;
                monthWithPendingStatus[count1] = reader1[0].ToString();
                monthWithPendingConsumption[count1] = double.Parse(reader1[1].ToString());

                Console.WriteLine(monthWithPendingStatus[count1] + " " + monthWithPendingConsumption[count1]);

                switch (monthWithPendingStatus[count1]) {
                    case "1":
                        jan2 = monthWithPendingConsumption[count1];
                        break;

                    case "2":
                        feb2 = monthWithPendingConsumption[count1];
                        break;

                    case "3":
                        mar2 = monthWithPendingConsumption[count1];
                        break;

                    case "4":
                        apr2 = monthWithPendingConsumption[count1];
                        break;

                    case "5":
                        may2 = monthWithPendingConsumption[count1];
                        break;

                    case "6":
                        jun2 = monthWithPendingConsumption[count1];
                        break;

                    case "7":
                        jul2 = monthWithPendingConsumption[count1];
                        break;

                    case "8":
                        aug2 = monthWithPendingConsumption[count1];
                        break;

                    case "9":
                        sep2 = monthWithPendingConsumption[count1];
                        break;

                    case "10":
                        oct2 = monthWithPendingConsumption[count1];
                        break;

                    case "11":
                        nov2 = monthWithPendingConsumption[count1];
                        break;
                }
            }


            reader1.Close();

            //pending monthly dues consumption
            string[] monthWithPendingStatus1 = new string[12];
            double[] monthWithPendingCollection = new double[12];
            int count2 = 0;

            SqlCommand comTblPendingofCollection = new SqlCommand("select month(date),sum(amount),status from MonthlyDues  where status LIKE '%" + pendingTxt + "%' group by month(date),status", SeeDetails_ComparisonPanelCon);

            SqlDataReader reader2 = comTblPendingofCollection.ExecuteReader();


            while (reader2.Read()) {
                count2++;
                monthWithPendingStatus1[count2] = reader2[0].ToString();
                monthWithPendingCollection[count2] = double.Parse(reader2[1].ToString());

                Console.WriteLine(monthWithPendingStatus1[count2] + " " + monthWithPendingCollection[count2]);

                switch (monthWithPendingStatus[count2]) {
                    case "1":
                        jan3 = monthWithPendingCollection[count2];
                        break;

                    case "2":
                        feb3 = monthWithPendingCollection[count2];
                        break;

                    case "3":
                        mar3 = monthWithPendingCollection[count2];
                        break;

                    case "4":
                        apr3 = monthWithPendingCollection[count2];
                        break;

                    case "5":
                        may3 = monthWithPendingCollection[count2];
                        break;

                    case "6":
                        jun3 = monthWithPendingCollection[count2];
                        break;

                    case "7":
                        jul3 = monthWithPendingCollection[count2];
                        break;

                    case "8":
                        aug3 = monthWithPendingCollection[count2];
                        break;

                    case "9":
                        sep3 = monthWithPendingCollection[count2];
                        break;

                    case "10":
                        oct3 = monthWithPendingCollection[count2];
                        break;

                    case "11":
                        nov3 = monthWithPendingCollection[count2];
                        break;
                }
            }


            reader2.Close();

            //total earnings per month water bills
            string completedTxt = "completed";
            SqlCommand comforTotalEarningsWaterbill = new SqlCommand("select year(date),month(date),sum(amount),status from MeterInformations where status LIKE '%" + completedTxt + "%' group by year(date), month(date),status order by year(date), month(date)", SeeDetails_ComparisonPanelCon);
            SqlDataReader rdr3 = comforTotalEarningsWaterbill.ExecuteReader();

            double[] monthlyConsumptionEarnings = new double[12];

            int counter2 = 0;

            while (rdr3.Read()) {
                string year = rdr3[0].ToString();
                string month = rdr3[1].ToString();

                counter2++;
                monthlyConsumptionEarnings[counter2] = double.Parse(rdr3[2].ToString());

                string amounts = rdr3[2].ToString();

                switch (month) {
                    case "1":
                        month = "January";
                        break;

                    case "2":
                        month = "February";
                        break;

                    case "3":
                        month = "March";
                        break;

                    case "4":
                        month = "April";
                        break;

                    case "5":
                        month = "May";
                        break;

                    case "6":
                        month = "June";
                        break;

                    case "7":
                        month = "July";
                        break;

                    case "8":
                        month = "August";
                        break;

                    case "9":
                        month = "September";
                        break;

                    case "10":
                        month = "October";
                        break;

                    case "11":
                        month = "November";
                        break;

                    case "12":
                        month = "December";
                        break;

                    default:
                        month = "None";
                        break;


                }
                string[] tblRow1 = {
                    year, month, amounts
                };

                MonthlyConsumptionGrid.Rows.Add(tblRow1);

                switch (counter2) {
                    case 12:
                        dec = monthlyConsumptionEarnings[12];
                        nov = monthlyConsumptionEarnings[11];
                        oct = monthlyConsumptionEarnings[10];
                        sep = monthlyConsumptionEarnings[9];
                        aug = monthlyConsumptionEarnings[8];
                        jul = monthlyConsumptionEarnings[7];
                        jun = monthlyConsumptionEarnings[6];
                        may = monthlyConsumptionEarnings[5];
                        apr = monthlyConsumptionEarnings[4];
                        mar = monthlyConsumptionEarnings[3];
                        feb = monthlyConsumptionEarnings[2];
                        jan = monthlyConsumptionEarnings[1];
                        break;

                    case 11:
                        dec = 0;
                        nov = monthlyConsumptionEarnings[11];
                        oct = monthlyConsumptionEarnings[10];
                        sep = monthlyConsumptionEarnings[9];
                        aug = monthlyConsumptionEarnings[8];
                        jul = monthlyConsumptionEarnings[7];
                        jun = monthlyConsumptionEarnings[6];
                        may = monthlyConsumptionEarnings[5];
                        apr = monthlyConsumptionEarnings[4];
                        mar = monthlyConsumptionEarnings[3];
                        feb = monthlyConsumptionEarnings[2];
                        jan = monthlyConsumptionEarnings[1];
                        break;

                    case 10:
                        dec = 0;
                        nov = 0;
                        oct = monthlyConsumptionEarnings[10];
                        sep = monthlyConsumptionEarnings[9];
                        aug = monthlyConsumptionEarnings[8];
                        jul = monthlyConsumptionEarnings[7];
                        jun = monthlyConsumptionEarnings[6];
                        may = monthlyConsumptionEarnings[5];
                        apr = monthlyConsumptionEarnings[4];
                        mar = monthlyConsumptionEarnings[3];
                        feb = monthlyConsumptionEarnings[2];
                        jan = monthlyConsumptionEarnings[1];
                        break;

                    case 9:
                        dec = 0;
                        nov = 0;
                        oct = 0;
                        sep = monthlyConsumptionEarnings[9];
                        aug = monthlyConsumptionEarnings[8];
                        jul = monthlyConsumptionEarnings[7];
                        jun = monthlyConsumptionEarnings[6];
                        may = monthlyConsumptionEarnings[5];
                        apr = monthlyConsumptionEarnings[4];
                        mar = monthlyConsumptionEarnings[3];
                        feb = monthlyConsumptionEarnings[2];
                        jan = monthlyConsumptionEarnings[1];
                        break;

                    case 8:
                        dec = 0;
                        nov = 0;
                        oct = 0;
                        sep = 0;
                        aug = monthlyConsumptionEarnings[8];
                        jul = monthlyConsumptionEarnings[7];
                        jun = monthlyConsumptionEarnings[6];
                        may = monthlyConsumptionEarnings[5];
                        apr = monthlyConsumptionEarnings[4];
                        mar = monthlyConsumptionEarnings[3];
                        feb = monthlyConsumptionEarnings[2];
                        jan = monthlyConsumptionEarnings[1];
                        break;

                    case 7:
                        dec = 0;
                        nov = 0;
                        oct = 0;
                        sep = 0;
                        aug = 0;
                        jul = monthlyConsumptionEarnings[7];
                        jun = monthlyConsumptionEarnings[6];
                        may = monthlyConsumptionEarnings[5];
                        apr = monthlyConsumptionEarnings[4];
                        mar = monthlyConsumptionEarnings[3];
                        feb = monthlyConsumptionEarnings[2];
                        jan = monthlyConsumptionEarnings[1];
                        break;

                    case 6:
                        dec = 0;
                        nov = 0;
                        oct = 0;
                        sep = 0;
                        aug = 0;
                        jul = 0;
                        jun = monthlyConsumptionEarnings[6];
                        may = monthlyConsumptionEarnings[5];
                        apr = monthlyConsumptionEarnings[4];
                        mar = monthlyConsumptionEarnings[3];
                        feb = monthlyConsumptionEarnings[2];
                        jan = monthlyConsumptionEarnings[1];
                        break;

                    case 5:
                        dec = 0;
                        nov = 0;
                        oct = 0;
                        sep = 0;
                        aug = 0;
                        jul = 0;
                        jun = 0;
                        may = monthlyConsumptionEarnings[5];
                        apr = monthlyConsumptionEarnings[4];
                        mar = monthlyConsumptionEarnings[3];
                        feb = monthlyConsumptionEarnings[2];
                        jan = monthlyConsumptionEarnings[1];
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
                        apr = monthlyConsumptionEarnings[4];
                        mar = monthlyConsumptionEarnings[3];
                        feb = monthlyConsumptionEarnings[2];
                        jan = monthlyConsumptionEarnings[1];
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
                        mar = monthlyConsumptionEarnings[3];
                        feb = monthlyConsumptionEarnings[2];
                        jan = monthlyConsumptionEarnings[1];
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
                        feb = monthlyConsumptionEarnings[2];
                        jan = monthlyConsumptionEarnings[1];
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
                        jan = monthlyConsumptionEarnings[1];
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


            rdr3.Close();
            //total earnings per month homeowners collection
            SqlCommand comforTotalEarningsHomeownersCollection = new SqlCommand("select year(date),month(date),sum(amount),status from MonthlyDues where status LIKE '%" + completedTxt + "%' group by year(date), month(date),status order by year(date), month(date)", SeeDetails_ComparisonPanelCon);
            SqlDataReader rdr4 = comforTotalEarningsHomeownersCollection.ExecuteReader();

            int[] monthlyCollectionEarnings = new int[12];

            int counter3 = 0;

            while (rdr4.Read()) {
                string year1 = rdr4[0].ToString();
                string month1 = rdr4[1].ToString();

                counter3++;

                monthlyCollectionEarnings[counter3] = int.Parse(rdr4[2].ToString());

                string amounts1 = rdr4[2].ToString();

                switch (month1) {
                    case "1":
                        month1 = "January";
                        break;

                    case "2":
                        month1 = "February";
                        break;

                    case "3":
                        month1 = "March";
                        break;

                    case "4":
                        month1 = "April";
                        break;

                    case "5":
                        month1 = "May";
                        break;

                    case "6":
                        month1 = "June";
                        break;

                    case "7":
                        month1 = "July";
                        break;

                    case "8":
                        month1 = "August";
                        break;

                    case "9":
                        month1 = "September";
                        break;

                    case "10":
                        month1 = "October";
                        break;

                    case "11":
                        month1 = "November";
                        break;

                    case "12":
                        month1 = "December";
                        break;

                    default:
                        month1 = "None";
                        break;
                }

                string[] tblRow2 = {
                    year1, month1, amounts1
                };

                gridMonthlyCollection.Rows.Add(tblRow2);

                switch (counter3) {
                    case 12:
                        dec1 = monthlyCollectionEarnings[12];
                        nov1 = monthlyCollectionEarnings[11];
                        oct1 = monthlyCollectionEarnings[10];
                        sep1 = monthlyCollectionEarnings[9];
                        aug1 = monthlyCollectionEarnings[8];
                        jul1 = monthlyCollectionEarnings[7];
                        jun1 = monthlyCollectionEarnings[6];
                        may1 = monthlyCollectionEarnings[5];
                        apr1 = monthlyCollectionEarnings[4];
                        mar1 = monthlyCollectionEarnings[3];
                        feb1 = monthlyCollectionEarnings[2];
                        jan1 = monthlyCollectionEarnings[1];
                        break;

                    case 11:
                        dec1 = 0;
                        nov1 = monthlyCollectionEarnings[11];
                        oct1 = monthlyCollectionEarnings[10];
                        sep1 = monthlyCollectionEarnings[9];
                        aug1 = monthlyCollectionEarnings[8];
                        jul1 = monthlyCollectionEarnings[7];
                        jun1 = monthlyCollectionEarnings[6];
                        may1 = monthlyCollectionEarnings[5];
                        apr1 = monthlyCollectionEarnings[4];
                        mar1 = monthlyCollectionEarnings[3];
                        feb1 = monthlyCollectionEarnings[2];
                        jan1 = monthlyCollectionEarnings[1];
                        break;

                    case 10:
                        dec1 = 0;
                        nov1 = 0;
                        oct1 = monthlyCollectionEarnings[10];
                        sep1 = monthlyCollectionEarnings[9];
                        aug1 = monthlyCollectionEarnings[8];
                        jul1 = monthlyCollectionEarnings[7];
                        jun1 = monthlyCollectionEarnings[6];
                        may1 = monthlyCollectionEarnings[5];
                        apr1 = monthlyCollectionEarnings[4];
                        mar1 = monthlyCollectionEarnings[3];
                        feb1 = monthlyCollectionEarnings[2];
                        jan1 = monthlyCollectionEarnings[1];
                        break;

                    case 9:
                        dec1 = 0;
                        nov1 = 0;
                        oct1 = 0;
                        sep1 = monthlyCollectionEarnings[9];
                        aug1 = monthlyCollectionEarnings[8];
                        jul1 = monthlyCollectionEarnings[7];
                        jun1 = monthlyCollectionEarnings[6];
                        may1 = monthlyCollectionEarnings[5];
                        apr1 = monthlyCollectionEarnings[4];
                        mar1 = monthlyCollectionEarnings[3];
                        feb1 = monthlyCollectionEarnings[2];
                        jan1 = monthlyCollectionEarnings[1];
                        break;

                    case 8:
                        dec1 = 0;
                        nov1 = 0;
                        oct1 = 0;
                        sep1 = 0;
                        aug1 = monthlyCollectionEarnings[8];
                        jul1 = monthlyCollectionEarnings[7];
                        jun1 = monthlyCollectionEarnings[6];
                        may1 = monthlyCollectionEarnings[5];
                        apr1 = monthlyCollectionEarnings[4];
                        mar1 = monthlyCollectionEarnings[3];
                        feb1 = monthlyCollectionEarnings[2];
                        jan1 = monthlyCollectionEarnings[1];
                        break;

                    case 7:
                        dec1 = 0;
                        nov1 = 0;
                        oct1 = 0;
                        sep1 = 0;
                        aug1 = 0;
                        jul1 = monthlyCollectionEarnings[7];
                        jun1 = monthlyCollectionEarnings[6];
                        may1 = monthlyCollectionEarnings[5];
                        apr1 = monthlyCollectionEarnings[4];
                        mar1 = monthlyCollectionEarnings[3];
                        feb1 = monthlyCollectionEarnings[2];
                        jan1 = monthlyCollectionEarnings[1];
                        break;

                    case 6:
                        dec1 = 0;
                        nov1 = 0;
                        oct1 = 0;
                        sep1 = 0;
                        aug1 = 0;
                        jul1 = 0;
                        jun1 = monthlyCollectionEarnings[6];
                        may1 = monthlyCollectionEarnings[5];
                        apr1 = monthlyCollectionEarnings[4];
                        mar1 = monthlyCollectionEarnings[3];
                        feb1 = monthlyCollectionEarnings[2];
                        jan1 = monthlyCollectionEarnings[1];
                        break;

                    case 5:
                        dec1 = 0;
                        nov1 = 0;
                        oct1 = 0;
                        sep1 = 0;
                        aug1 = 0;
                        jul1 = 0;
                        jun1 = 0;
                        may1 = monthlyCollectionEarnings[5];
                        apr1 = monthlyCollectionEarnings[4];
                        mar1 = monthlyCollectionEarnings[3];
                        feb1 = monthlyCollectionEarnings[2];
                        jan1 = monthlyCollectionEarnings[1];
                        break;

                    case 4:
                        dec1 = 0;
                        nov1 = 0;
                        oct1 = 0;
                        sep1 = 0;
                        aug1 = 0;
                        jul1 = 0;
                        jun1 = 0;
                        may1 = 0;
                        apr1 = monthlyCollectionEarnings[4];
                        mar1 = monthlyCollectionEarnings[3];
                        feb1 = monthlyCollectionEarnings[2];
                        jan1 = monthlyCollectionEarnings[1];
                        break;

                    case 3:
                        dec1 = 0;
                        nov1 = 0;
                        oct1 = 0;
                        sep1 = 0;
                        aug1 = 0;
                        jul1 = 0;
                        jun1 = 0;
                        may1 = 0;
                        apr1 = 0;
                        mar1 = monthlyCollectionEarnings[3];
                        feb1 = monthlyCollectionEarnings[2];
                        jan1 = monthlyCollectionEarnings[1];
                        break;

                    case 2:
                        dec1 = 0;
                        nov1 = 0;
                        oct1 = 0;
                        sep1 = 0;
                        aug1 = 0;
                        jul1 = 0;
                        jun1 = 0;
                        may1 = 0;
                        apr1 = 0;
                        mar1 = 0;
                        feb1 = monthlyCollectionEarnings[2];
                        jan1 = monthlyCollectionEarnings[1];
                        break;

                    case 1:
                        dec1 = 0;
                        nov1 = 0;
                        oct1 = 0;
                        sep1 = 0;
                        aug1 = 0;
                        jul1 = 0;
                        jun1 = 0;
                        may1 = 0;
                        apr1 = 0;
                        mar1 = 0;
                        feb1 = 0;
                        jan1 = monthlyCollectionEarnings[1];
                        break;

                    default:
                        dec1 = 0;
                        nov1 = 0;
                        oct1 = 0;
                        sep1 = 0;
                        aug1 = 0;
                        jul1 = 0;
                        jun1 = 0;
                        may1 = 0;
                        apr1 = 0;
                        mar1 = 0;
                        feb1 = 0;
                        jan1 = 0;
                        break;
                }
            }

            rdr4.Close();


            //total earnings water bill
            totalWaterBillsEarnings.Text = String.Format("{0:0.00}", (jan + feb + mar + apr + may + jun + jul + aug + sep + oct + nov + dec)).ToString();

            //total collection earnings
            totalCollectionEarnings.Text = String.Format("{0:0.00}", (jan1 + feb1 + mar1 + apr1 + may1 + jun1 + jul1 + aug1 + sep1 + oct1 + nov1 + dec1)).ToString();

            //pending water bill
            pendingWaterBill.Text = String.Format("{0:0.00}", ((jan2 + feb2 + mar2 + apr2 + may2 + jun2 + jul2 + aug2 + sep2 + oct2 + nov2 + dec2))).ToString();

            //pending collections
            pendingCollections.Text = String.Format("{0:0.00}", (jan3 + feb3 + mar3 + apr3 + may3 + jun3 + jul3 + aug3 + sep3 + oct3 + nov3 + dec3)).ToString();


            //percent comparison between water bill and collections 
            //percentThisMonth = ((increaseThisMonth - increaseLastMonth) / increaseLastMonth) * 100;

            double percentConsumption = ((double.Parse(totalWaterBillsEarnings.Text) - double.Parse(totalCollectionEarnings.Text)) / double.Parse(totalCollectionEarnings.Text)) * 100;

            if (percentConsumption < 0) {
                consumptionPercentage.ForeColor = System.Drawing.Color.Crimson;
                consumptionPercentage.Text = String.Format("{0:0.00}", percentConsumption).ToString() + "%";
            } else {
                consumptionPercentage.ForeColor = System.Drawing.Color.LimeGreen;
                consumptionPercentage.Text = "+" + String.Format("{0:0.00}", percentConsumption).ToString() + "%";
            }

            double percentCollection = ((double.Parse(totalCollectionEarnings.Text) - double.Parse(totalWaterBillsEarnings.Text)) / double.Parse(totalWaterBillsEarnings.Text)) * 100;

            if (percentCollection < 0) {
                collectionPercentage.ForeColor = System.Drawing.Color.Crimson;
                collectionPercentage.Text = String.Format("{0:0.00}", percentCollection).ToString() + "%";
            } else {
                collectionPercentage.ForeColor = System.Drawing.Color.LimeGreen;
                collectionPercentage.Text = "+" + String.Format("{0:0.00}", percentCollection).ToString() + "%";
            }

            //charts water bill earnings
            waterConsumptionChart.Series = new SeriesCollection {
                new LineSeries {
                    Title = "Completed",
                    Values = new ChartValues<double> {jan, feb, mar, apr, may, jun, jul, aug, sep, oct, nov, dec},
                    PointGeometrySize = 9
                },

                new LineSeries {
                    Title = "Pending",
                    Values = new ChartValues<double> {jan2, feb2, mar2, apr2, may2, jun2, jul2, aug2, sep2, oct2, nov2, dec2},
                    PointGeometrySize = 9
                },
            };

            waterConsumptionChart.AxisX.Add(new Axis {
                Title = "Month",
                Labels = new[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" }
            });

            waterConsumptionChart.AxisY.Add(new Axis {
                Title = "Amount",
                MinValue = 0,
            });

            waterConsumptionChart.AxisX[0].Separator.StrokeThickness = 0;
            waterConsumptionChart.AxisY[0].Separator.StrokeThickness = 0;


            //charts collection bill earnings
            collectionBillsChart.Series = new SeriesCollection {
                new LineSeries {
                    Title = "Completed",
                    Values = new ChartValues<double> {jan1, feb1, mar1, apr1, may1, jun1, jul1, aug1, sep1, oct1, nov1, dec1},
                    PointGeometrySize = 9

                },

                new LineSeries {
                    Title = "Pending",
                    Values = new ChartValues<double> {jan3, feb3, mar3, apr3, may3, jun3, jul3, aug3, sep3, oct3, nov3, dec3},
                    PointGeometrySize = 9

                },
            };

            collectionBillsChart.AxisX.Add(new Axis {
                Title = "Month",
                Labels = new[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" },

                /* make x axis enable
                Separator = new Separator // force the separator step to 1, so it always display all labels
                {
                    Step = 1,
                    IsEnabled = false //disable it to make it invisible.
                },
                LabelsRotation = 15
                */
            });

            collectionBillsChart.AxisY.Add(new Axis {
                Title = "Amount",
                MinValue = 0,
            });

            collectionBillsChart.AxisX[0].Separator.StrokeThickness = 0;
            collectionBillsChart.AxisY[0].Separator.StrokeThickness = 0;

        }
            
        //shadow for form
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn (
        int nLeftRect, // x-coordinate of upper-left corner
        int nTopRect, // y-coordinate of upper-left corner
        int nRightRect, // x-coordinate of lower-right corner
        int nBottomRect, // y-coordinate of lower-right corner
        int nWidthEllipse, // height of ellipse
        int nHeightEllipse // width of ellipse
     );

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
        }
    }
