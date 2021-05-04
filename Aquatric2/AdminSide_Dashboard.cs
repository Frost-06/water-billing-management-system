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
    public partial class AdminSide_Dashboard : Form {
        SqlConnection AdministratorDashboardCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ryzen\source\repos\Aquatric2\Aquatric2\AquatricDatabase.mdf;Integrated Security=True;Connect Timeout=30");
        public static double jan, feb, mar, apr, may, jun, jul, aug, sep, oct, nov, dec;

        private void ThreadSeeDetails() {
            Application.Run(new SeeDetails_ComparisonPanel());
        }

        private void seeDetailsBtn_Click(object sender, EventArgs e) {
            Thread ThreadCompare = new Thread(new ThreadStart(ThreadSeeDetails)); //you create a new thread
            ThreadCompare.SetApartmentState(ApartmentState.STA);
            ThreadCompare.Start();
        }

        public static int jan1, feb1, mar1, apr1, may1, jun1, jul1, aug1, sep1, oct1, nov1, dec1;
        public static double jan2, feb2, mar2, apr2, may2, jun2, jul2, aug2, sep2, oct2, nov2, dec2;
        public static double jan3, feb3, mar3, apr3, may3, jun3, jul3, aug3, sep3, oct3, nov3, dec3;

        public AdminSide_Dashboard() {
            InitializeComponent();

            AdministratorDashboardCon.Open();
        }

        private void AdminSide_Dashboard_Load(object sender, EventArgs e) {
            //total users
            SqlCommand comTotalUser = new SqlCommand("select count (userID) FROM registrationTable", AdministratorDashboardCon);

            int counter1 = (Int32)comTotalUser.ExecuteScalar();

            totalUsersTxt.Text = counter1.ToString();

            //total collected
            string a = "completed";
            SqlCommand comTotalCollected = new SqlCommand("select count (status) FROM MeterInformations  where status LIKE '%" + a + "%'", AdministratorDashboardCon);

            int com1 = (Int32)comTotalCollected.ExecuteScalar();

            SqlCommand comTotalCollected1 = new SqlCommand("select count (status) FROM MonthlyDues  where status LIKE '%" + a + "%'", AdministratorDashboardCon);

            int com2 = (Int32)comTotalCollected1.ExecuteScalar();

            totalCollectedLbl.Text = (com1+com2).ToString();

            //pending monthly consumption
            string pendingTxt = "pending";
            string[] monthWithPendingStatus = new string[12];
            double[] monthWithPendingConsumption = new double[12];
            int count1 = 0;

            SqlCommand comTblPendingofConsumption = new SqlCommand("select month(date),sum(amount),status from MeterInformations  where status LIKE '%" + pendingTxt + "%' group by month(date),status", AdministratorDashboardCon);

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

            //pending monthly dues   
            string[] monthWithPendingStatus1 = new string[12];
            double[] monthWithPendingCollection = new double[12];
            int count2 = 0;

            SqlCommand comTblPendingofCollection = new SqlCommand("select month(date),sum(amount),status from MonthlyDues  where status LIKE '%" + pendingTxt + "%' group by month(date),status", AdministratorDashboardCon);

            SqlDataReader reader2 = comTblPendingofCollection.ExecuteReader();


            while (reader2.Read()) {
                count2++;
                monthWithPendingStatus1[count2] = reader2[0].ToString();
                monthWithPendingCollection[count2] = double.Parse(reader2[1].ToString());

                Console.WriteLine(monthWithPendingStatus[count2] + " " + monthWithPendingConsumption[count2]);

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

            string completedTxt = "completed";
            //total earnings per month water bills
            SqlCommand comforTotalEarningsWaterbill = new SqlCommand("select year(date),month(date),sum(amount),status from MeterInformations where status LIKE '%" + completedTxt + "%' group by year(date), month(date),status order by year(date), month(date)", AdministratorDashboardCon);
            SqlDataReader rdr3 = comforTotalEarningsWaterbill.ExecuteReader();

            double[] monthlyConsumptionEarnings = new double[12];

            int counter2 = 0;

            while (rdr3.Read()) {
                counter2++;
                monthlyConsumptionEarnings[counter2] = double.Parse(rdr3[2].ToString());

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
            SqlCommand comforTotalEarningsHomeownersCollection = new SqlCommand("select year(date),month(date),sum(amount),status from MonthlyDues where status LIKE '%" + completedTxt + "%' group by year(date), month(date),status order by year(date), month(date)", AdministratorDashboardCon);
            SqlDataReader rdr4 = comforTotalEarningsHomeownersCollection.ExecuteReader();

            int[] monthlyCollectionEarnings = new int[12];

            int counter3 = 0;

            while (rdr4.Read()) {
                counter3++;

                monthlyCollectionEarnings[counter3] = int.Parse(rdr4[2].ToString());

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

            //this month and last month
            double month3, month4;

            if (counter2 == 12) {
                month3 = dec + dec1;
                month4 = nov + nov1;

                thisMonthLbl.Text = String.Format("{0:0.00}", month3.ToString());
                lastMonthLbl.Text = String.Format("{0:0.00}", month4.ToString());
            } else if (counter2 == 11) {
                month3 = nov + nov1;
                month4 = oct + oct1;

                thisMonthLbl.Text = String.Format("{0:0.00}", month3.ToString());
                lastMonthLbl.Text = String.Format("{0:0.00}", month4.ToString());
            } else if (counter2 == 10) {
                month3 = oct + oct1;
                month4 = sep + sep1;

                thisMonthLbl.Text = String.Format("{0:0.00}", month3.ToString());
                lastMonthLbl.Text = String.Format("{0:0.00}", month4.ToString());
            } else if (counter2 == 9) {
                month3 = sep + sep1;
                month4 = aug + aug1;


                thisMonthLbl.Text = String.Format("{0:0.00}", month3.ToString());
                lastMonthLbl.Text = String.Format("{0:0.00}", month4.ToString());
            } else if (counter2 == 8) {
                month3 = aug + aug1;
                month4 = jul + jul1;

                thisMonthLbl.Text = String.Format("{0:0.00}", month3.ToString());
                lastMonthLbl.Text = String.Format("{0:0.00}", month4.ToString());
            } else if (counter2 == 7) {
                month3 = jul + jul1;
                month4 = jun + jun1;

                thisMonthLbl.Text = String.Format("{0:0.00}", month3.ToString());
                lastMonthLbl.Text = String.Format("{0:0.00}", month4.ToString());
            } else if (counter2 == 6) {
                month3 = jun + jun1;
                month4 = may + may1;

                thisMonthLbl.Text = String.Format("{0:0.00}", month3.ToString());
                lastMonthLbl.Text = String.Format("{0:0.00}", month4.ToString());
            } else if (counter2 == 5) {
                month3 = may + may1;
                month4 = apr + apr1;

                thisMonthLbl.Text = String.Format("{0:0.00}", month3.ToString());
                lastMonthLbl.Text = String.Format("{0:0.00}", month4.ToString());
            } else if (counter2 == 4) {
                month3 = apr + apr;
                month4 = mar + mar1;

                thisMonthLbl.Text = String.Format("{0:0.00}", month3.ToString());
                lastMonthLbl.Text = String.Format("{0:0.00}", month4.ToString());
            } else if (counter2 == 3) {
                month3 = mar + mar1;
                month4 = feb + feb1;

                thisMonthLbl.Text = String.Format("{0:0.00}", month3.ToString());
                lastMonthLbl.Text = String.Format("{0:0.00}", month4.ToString());
            } else if (counter2 == 2) {
                month3 = feb + feb1;
                month4 = jan + jan1;

                thisMonthLbl.Text = String.Format("{0:0.00}", month3.ToString());
                lastMonthLbl.Text = String.Format("{0:0.00}", month4.ToString());
            } else if (counter2 == 1) {
                month3 = jan + jan1;

                thisMonthLbl.Text = String.Format("{0:0.00}", month3.ToString());
                lastMonthLbl.Text = "None";
            }

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

            // this month percentage
            double increaseThisMonth = double.Parse(thisMonthLbl.Text);
            double increaseLastMonth = double.Parse(lastMonthLbl.Text);

            double percentThisMonth, percentLastMonth;

            percentThisMonth = ((increaseThisMonth - increaseLastMonth) / increaseLastMonth) * 100;

            if (percentThisMonth < 0) {
                thisMonthPercent.ForeColor = System.Drawing.Color.Crimson;
                thisMonthPercent.Text = String.Format("{0:0.00}", percentThisMonth).ToString() + "%";
            } else {
                thisMonthPercent.ForeColor = System.Drawing.Color.LimeGreen;
                thisMonthPercent.Text = "+" + String.Format("{0:0.00}", percentThisMonth).ToString() + "%";
            }

            //last month percentage
            double month5;

            percentLastMonth = ((increaseLastMonth - increaseThisMonth) / increaseThisMonth) * 100;

            if (percentLastMonth == 12 && percentLastMonth == 12) {
                month5 = oct + oct1;

                percentLastMonth = ((increaseLastMonth - month5) / month5) * 100;

            } else if (percentLastMonth == 11 && percentLastMonth == 11) {
                month5 = sep + sep1;

                percentLastMonth = ((increaseLastMonth - month5) / month5) * 100;
            }

            if (percentLastMonth < 0) {
                increaseLastMonthLbl.ForeColor = System.Drawing.Color.Crimson;
                increaseLastMonthLbl.Text = String.Format("{0:0.00}", percentLastMonth).ToString() + "%";
            } else {
                increaseLastMonthLbl.ForeColor = System.Drawing.Color.LimeGreen;
                increaseLastMonthLbl.Text = "+" + String.Format("{0:0.00}", percentLastMonth).ToString() + "%";
            }

            //total earnings
            totalEarningsLbl.Text = String.Format("{0:0.00}", (jan + feb + mar + apr + may + jun + jul + aug + sep + oct + nov + dec + jan1 + feb1 + mar1 + apr1 + may1 + jun1 + jul1 + aug1 + sep1 + oct1 + nov1 + dec1)).ToString();
            pendingPaymentLbl.Text = String.Format("{0:0.00}", (jan2 + feb2 + mar2 + apr2 + may2 + jun2 + jul2 + aug2 + sep2 + oct2 + nov2 + dec2 + jan3 + feb3 + mar3 + apr3 + may3 + jun3 + jul3 + aug3 + sep3 + oct3 + nov3 + dec3)).ToString();

            //
            summaryChart.Series = new SeriesCollection {
                new LineSeries {
                    Title = "Completed",
                    Values = new ChartValues<double> {(jan+jan1), (feb+feb1), (mar+mar1), (apr+apr1), (may+may1), (jun+jun1), (jul+jul1), (aug+aug1), (sep+sep1), (oct+oct1), (nov+nov1), (dec+dec1)},
                    PointGeometrySize = 9
                },

                new LineSeries {
                    Title = "Pending",
                    Values = new ChartValues<double> {(jan2+jan3), (feb2+feb3), (mar2+mar3), (apr2+apr3), (may2+may3), (jun2+jun3), (jul2+jul3), (aug2+aug3), (sep2+sep3), (oct2+oct3), (nov2+nov3), (dec2+dec3)},
                    PointGeometrySize = 9
                },
            };

            summaryChart.AxisX.Add(new Axis {
                Title = "Month",
                Labels = new[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" },
                // force the separator step to 1, so it always display all labels     
                Separator = new Separator {          
                    Step = 1,
                    IsEnabled = false //disable it to make it invisible.
                },
                LabelsRotation = 15
            });

            summaryChart.AxisY.Add(new Axis {
                Title = "Amount",
                MinValue = 0,
            });

            summaryChart.AxisX[0].Separator.StrokeThickness = 0;
            summaryChart.AxisY[0].Separator.StrokeThickness = 0;


            // This is your color to convert from
            System.Drawing.Color color;
            System.Windows.Media.Color newColor = System.Windows.Media.Color.FromRgb(7, 159, 234);

            double goalEarnings = (int.Parse(totalUsersTxt.Text) * 150.45) + (int.Parse(totalUsersTxt.Text) * 600);

            //goal gauge
            goalGauge.From = 0;
            goalGauge.To = goalEarnings;
            goalGauge.Value = double.Parse(thisMonthLbl.Text);
            goalGauge.ToColor = newColor;
            goalGaugeLbl.Text = thisMonthLbl.Text;

        }
    }
}
