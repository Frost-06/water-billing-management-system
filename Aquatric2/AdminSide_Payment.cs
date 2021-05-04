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
    public partial class AdminSide_Payment : Form {
        SqlConnection AdminSide_PaymentCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ryzen\source\repos\Aquatric2\Aquatric2\AquatricDatabase.mdf;Integrated Security=True;Connect Timeout=30");
        
        DataTable consumptionGrid = new DataTable();
        DataTable collectionGrid = new DataTable();
        DataTable latestPaymentGrid = new DataTable();

        public static string number, amount, number2, number3, rowAdvance;
        public static string userID, latestDate1, latestConsumption1, latestConsumptionAmount1, latestCollection1;
        public static double jan2, feb2, mar2, apr2, may2, jun2, jul2, aug2, sep2, oct2, nov2, dec2;       

        public static double jan3, feb3, mar3, apr3, may3, jun3, jul3, aug3, sep3, oct3, nov3, dec3;

        private void comboBoxIdNumber_Click(object sender, EventArgs e) {
            advanceMoneyAmountTxt.Text = "0";
            startingMonthTxt.Text = "None";
            endingMonthTxt.Text = "None";
            advanceBalanceTxt.Text = "0";
        }

        public static string idNumber;

        private void searchForLatestPayment_TextChanged(object sender, EventArgs e) {
            SqlDataAdapter adapt = new SqlDataAdapter("select *from latestPayment where latestDate  LIKE '%" + searchForLatestPayment.Text + "%' or userID LIKE '%" + searchForLatestPayment.Text + "%'", AdminSide_PaymentCon);
            latestPaymentGrid = new DataTable();
            adapt.Fill(latestPaymentGrid);
            latestPaymentDataGrid.DataSource = latestPaymentGrid;
        }        

        private void MonthlyConsumption1_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0) {
                DataGridViewRow rows = this.MonthlyConsumption1.Rows[e.RowIndex];
                number = rows.Cells[0].Value.ToString();
                dateTxt1.Text = rows.Cells[1].Value.ToString();
                consumptionTxt.Text = rows.Cells[2].Value.ToString();                
                amount = rows.Cells[3].Value.ToString();
                statusTxt.Text = rows.Cells[4].Value.ToString();
            }
        }
      
        private void MonthlyCollectionGrid1_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0) {
                DataGridViewRow rows = this.MonthlyCollectionGrid1.Rows[e.RowIndex];
                number2 = rows.Cells[0].Value.ToString();
                date2Txt.Text = rows.Cells[1].Value.ToString();
                amountTxt.Text = rows.Cells[2].Value.ToString();
                statusTxt1.Text = rows.Cells[3].Value.ToString();
            }
        }

        private void latestPaymentDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0) {
                DataGridViewRow rows = this.latestPaymentDataGrid.Rows[e.RowIndex];
                number3 = rows.Cells[0].Value.ToString();
                newUserID.Text = rows.Cells[1].Value.ToString();
                latestDate.Text = rows.Cells[6].Value.ToString();
                latestConsumption.Text = rows.Cells[3].Value.ToString();
                latestConsumptionAmount.Text = rows.Cells[4].Value.ToString();
                latestCollection.Text = rows.Cells[5].Value.ToString();

                latestTotal.Text = "";
                outstandingBalanceTxt.Text = "";

                //pending monthly consumption
                string pendingTxt = "pending";
                string[] monthWithPendingStatus = new string[12];
                double[] monthWithPendingConsumption = new double[12];
                int count1 = 0;

                SqlCommand comTblPendingofConsumption = new SqlCommand("select month(date),sum(amount),status from MeterInformations  where status LIKE '%" + pendingTxt + "%' and userID LIKE '%" + newUserID.Text + "%' group by month(date),status", AdminSide_PaymentCon);

                SqlDataReader reader1 = comTblPendingofConsumption.ExecuteReader();

                while (reader1.Read()) {
                    count1++;
                    monthWithPendingStatus[count1] = reader1[0].ToString();
                    monthWithPendingConsumption[count1] = double.Parse(reader1[1].ToString());

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

                SqlCommand comTblPendingofCollection = new SqlCommand("select month(date),sum(amount),status from MonthlyDues  where status LIKE '%" + pendingTxt + "%' and userID LIKE '%" + newUserID.Text + "%' group by month(date),status", AdminSide_PaymentCon);

                SqlDataReader reader2 = comTblPendingofCollection.ExecuteReader();


                while (reader2.Read()) {
                    count2++;
                    monthWithPendingStatus1[count2] = reader2[0].ToString();
                    monthWithPendingCollection[count2] = double.Parse(reader2[1].ToString());


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

                latestTotal.Text = (double.Parse(latestConsumptionAmount.Text) + double.Parse(latestCollection.Text)).ToString();
                outstandingBalanceTxt.Text = String.Format("{0:0.00}", (jan2 + feb2 + mar2 + apr2 + may2 + jun2 + jul2 + aug2 + sep2 + oct2 + nov2 + dec2 + jan3 + feb3 + mar3 + apr3 + may3 + jun3 + jul3 + aug3 + sep3 + oct3 + nov3 + dec3)).ToString();
            }
        }

        private void monthlyCollectionSearch_TextChanged(object sender, EventArgs e) {
            if (userIDTxt.Text == "") {

            } else {
                SqlDataAdapter adapt = new SqlDataAdapter("select rows,date,amount,status from MonthlyDues where date  LIKE '%" + monthlyCollectionSearch.Text + "%' and userID=" + userIDTxt.Text + "", AdminSide_PaymentCon);
                collectionGrid = new DataTable();
                adapt.Fill(collectionGrid);
                MonthlyCollectionGrid1.DataSource = collectionGrid;
            }
        }        

        private void waterBillsSearch_TextChanged(object sender, EventArgs e) {
            if (userIDTxt.Text == "" || userIDTxt.Text == null) {

            } else {
                SqlDataAdapter adapt1 = new SqlDataAdapter("select rows,date,consumption,amount,status from MeterInformations where date LIKE '%" + waterBillsSearch.Text + "%' and userID=" + userIDTxt.Text + " order by date desc", AdminSide_PaymentCon);

                consumptionGrid = new DataTable();
                adapt1.Fill(consumptionGrid);
                MonthlyConsumption1.DataSource = consumptionGrid;

            }
        }
       
        private void generateBtn_Click(object sender, EventArgs e) {

            bool result = false;


            //view advance payment
            SqlCommand viewAdvancePaymentCom = new SqlCommand("select *from AdvancePayment where userID=" + idNumber + "", AdminSide_PaymentCon);
            viewAdvancePaymentCom.ExecuteNonQuery();

            SqlDataReader rdrs1 = viewAdvancePaymentCom.ExecuteReader();

            while (rdrs1.Read()) {
                advanceMoneyAmountTxt.Text = rdrs1["amount"].ToString();
                startingMonthTxt.Text = rdrs1["startingMonth"].ToString();
                endingMonthTxt.Text = rdrs1["endingMonth"].ToString();
                advanceBalanceTxt.Text = rdrs1["remainingBalance"].ToString();
                rowNumTxt.Text = rdrs1["rows"].ToString();
            }

            rdrs1.Close();

            double consumpAmount = 0;
            double collectionAmount = 0;
            string completed = "completed";
            string pending = "pending";

            //data for MeterInformations sql database get the total amount
            SqlCommand comTotalAmount = new SqlCommand("select *from MeterInformations where userID=" + userIDTxt.Text + "order by date asc", AdminSide_PaymentCon);
            SqlDataReader rdr = comTotalAmount.ExecuteReader();

            double[] k1 = new double[12];
            double consumptionAmount;
            int counter = 0;

            while (rdr.Read()) {
                k1[counter] = double.Parse(rdr["amount"].ToString());
                consumptionAmount = k1[0] + k1[1] + k1[2] + k1[3] + k1[4] + k1[5] + k1[6] + k1[7] + k1[8] + k1[9] + k1[10] + k1[11];
                counter++;
                consumpAmount = consumptionAmount;
            }

            rdr.Close();


            double totalAmount;
            double[] k = new double[100];
            double monthlyAmount = 0;

            //selecting value that is completed status
            SqlCommand comTblCompleted = new SqlCommand("select amount,status from MonthlyDues  where status LIKE '%" + completed + "%' and  userID=" + userIDTxt.Text + "", AdminSide_PaymentCon);
            SqlDataReader rdr1 = comTblCompleted.ExecuteReader();
            while (rdr1.Read()) {

                totalAmount = int.Parse(rdr1["amount"].ToString());
                counter++;
                k[counter] = totalAmount;

                monthlyAmount += k[counter];
                collectionAmount = monthlyAmount;
            }

            // totalPayment.Text = (consumpAmount + collectionAmount).ToString();

            rdr1.Close();

            double totalAmount1;
            double[] k2 = new double[100];
            double monthlyAmount1 = 0;

            //selecting value that is pending status
            SqlCommand comTblPending = new SqlCommand("select amount,status from MonthlyDues  where status LIKE '%" + pending + "%' and  userID=" + userIDTxt.Text + "", AdminSide_PaymentCon);
            SqlDataReader rdr3 = comTblPending.ExecuteReader();
            while (rdr3.Read()) {

                totalAmount1 = int.Parse(rdr3["amount"].ToString());
                counter++;
                k2[counter] = totalAmount1;

                monthlyAmount1 += k2[counter];
                //totalBalance.Text = monthlyAmount1.ToString();
            }

            //totalPayment.Text = (consumpAmount + collectionAmount).ToString();

            rdr3.Close();

            consumptionGrid.Rows.Clear();
            collectionGrid.Rows.Clear();


            //daatgrid for monthly consumption
            SqlDataAdapter consumptionCom = new SqlDataAdapter("select MeterInformations.rows,MeterInformations.date,MeterInformations.consumption,MeterInformations.amount,MeterInformations.status from registrationTable,MeterInformations where registrationTable.userID=MeterInformations.userID and registrationTable.userID=" + userIDTxt.Text + "order by date desc", AdminSide_PaymentCon);
            consumptionCom.Fill(consumptionGrid);
            MonthlyConsumption1.DataSource = consumptionGrid;


            //data grid for monthly collections
            SqlDataAdapter collectionCom = new SqlDataAdapter("select MonthlyDues.rows,MonthlyDues.date,MonthlyDues.amount,MonthlyDues.status from registrationTable,MonthlyDues where registrationTable.userID=MonthlyDues.userID and registrationTable.userID=" + userIDTxt.Text + "order by date desc", AdminSide_PaymentCon);
            collectionCom.Fill(collectionGrid);
            MonthlyCollectionGrid1.DataSource = collectionGrid;

            MonthlyConsumption1.Columns[0].Width = 1;
            MonthlyCollectionGrid1.Columns[0].Width = 1;


            //registration name
            string fname, lname;
            SqlCommand userDataCom = new SqlCommand("select *from registrationTable where userID=" + userIDTxt.Text + "", AdminSide_PaymentCon);
            userDataCom.ExecuteNonQuery();

            SqlDataReader rdr2 = userDataCom.ExecuteReader();

            while (rdr2.Read()) {
                fname = rdr2["fname"].ToString();
                lname = rdr2["lname"].ToString();

                fnameTxt.Text = char.ToUpper(fname[0]) + fname.Substring(1);
                lnameTxt.Text = char.ToUpper(lname[0]) + lname.Substring(1);
            }

            rdr2.Close();


        }        

        public AdminSide_Payment() {
            InitializeComponent();
            AdminSide_PaymentCon.Open();
        }

        private void AdminSide_Payment_Load(object sender, EventArgs e) {
          
            //waterComboBox.ControlAdded("");

            waterComboBox.Items.Add("Save");
            waterComboBox.Items.Add("Update");
            waterComboBox.Items.Add("Delete");

            SqlCommand userDataComboBox = new SqlCommand("select *from registrationTable", AdminSide_PaymentCon);
            userDataComboBox.ExecuteNonQuery();

            SqlDataReader rdr = userDataComboBox.ExecuteReader();

            while (rdr.Read()) {
                comboBoxIdNumber.Items.Add(rdr["userID"].ToString());
            }

            //view advance payment
            rdr.Close();
            try {
                SqlCommand viewAdvancePaymentCom = new SqlCommand("select *from AdvancePayment where userID=" + userIDTxt.Text + "", AdminSide_PaymentCon);
                viewAdvancePaymentCom.ExecuteNonQuery();

                SqlDataReader rdrs1 = viewAdvancePaymentCom.ExecuteReader();

                while (rdrs1.Read()) {
                    advanceMoneyAmountTxt.Text = rdrs1["amount"].ToString();
                    startingMonthTxt.Text = rdrs1["startingMonth"].ToString();
                    endingMonthTxt.Text = rdrs1["endingMonth"].ToString();
                    advanceBalanceTxt.Text = rdrs1["remainingBalance"].ToString();
                    
                }

                rdrs1.Close();
            } catch(Exception ex) {
                advanceMoneyAmountTxt.Text = "0";
                startingMonthTxt.Text = "None";
                endingMonthTxt.Text = "None";
                advanceBalanceTxt.Text = "0";
            }

            //datagrid for monthly consumption
            SqlDataAdapter latestPaymentCom = new SqlDataAdapter("select *from latestPayment order by userID desc", AdminSide_PaymentCon);
            latestPaymentCom.Fill(latestPaymentGrid);
            latestPaymentDataGrid.DataSource = latestPaymentGrid;

            latestPaymentDataGrid.Columns[0].Width = 1;
            latestPaymentDataGrid.Columns[1].Width = 90;
            latestPaymentDataGrid.Columns[3].Width = 90;

        }



        private void waterComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (waterComboBox.SelectedItem == "Delete") {
                if (dateTxt1.Text == "" || consumptionTxt.Text == "") {
                    MessageBox.Show("All Fields Required");
                } else {
                    SqlCommand deleteMeterInformationCom = new SqlCommand("delete from MeterInformations where rows = " + number + " ", AdminSide_PaymentCon);
                    deleteMeterInformationCom.ExecuteNonQuery();

                    MessageBox.Show("Record Deleted");

                    dateTxt1.Text = "";
                    consumptionTxt.Text = "";
                    statusTxt.Text = "";

                    int rowIndex = MonthlyConsumption1.CurrentCell.RowIndex;
                    MonthlyConsumption1.Rows.RemoveAt(rowIndex);
                }
            } else if(waterComboBox.SelectedItem == "Save") {
                if (dateTxt1.Text == "" || consumptionTxt.Text == "") {
                    MessageBox.Show("All Fields Required");
                } else {
                     /*1 to 10 150.45
                     * 11 to 20 + by 16.80
                     * 21 to 30 + by 19.77
                     * 30 and up + by 25.50 */
                    double amountOfWaterBills;
                    int consump = int.Parse(consumptionTxt.Text);

                    if (consump <= 10 && consump >= 1) {
                        amountOfWaterBills = 150.45;
                    } else if (consump <= 20 && consump >= 11) {
                        amountOfWaterBills = 150.45 + 16.80;
                    } else if (consump <= 30 && consump >= 21) {
                        amountOfWaterBills = 150.45 + 19.77;
                    } else if (consump >= 31) {
                        amountOfWaterBills = 150.45 + 25.50;
                    } else {
                        amountOfWaterBills = 0;
                    }

                    string totalAmountOfWaterBills = String.Format("{0:0.00}", amountOfWaterBills);
                    SqlCommand insertingIntoMeterInformationCom = new SqlCommand("insert into MeterInformations (userID,date,consumption,amount,status) values ('" + userIDTxt.Text + "','" + dateTxt1.Text + "','" + consumptionTxt.Text + "','" + totalAmountOfWaterBills + "','" + statusTxt.Text + "')", AdminSide_PaymentCon);
                    insertingIntoMeterInformationCom.ExecuteNonQuery();

                    //for advance payment
                    if (double.Parse(advanceBalanceTxt.Text) < amountOfWaterBills) {
                        MessageBox.Show("The remaining balance is less than by the amount required");
                    } else {
                        double totalForAdvancePayment = double.Parse(advanceBalanceTxt.Text) - amountOfWaterBills;
                        advanceBalanceTxt.Text = totalForAdvancePayment.ToString();

                        SqlCommand updateAdvancePayment = new SqlCommand("update AdvancePayment set remainingBalance='" + advanceBalanceTxt.Text + "' where rows=" + rowNumTxt.Text + "", AdminSide_PaymentCon);
                        updateAdvancePayment.ExecuteNonQuery();
                    }

                    MessageBox.Show("Succesfully Added");

                    dateTxt1.Text = "";
                    consumptionTxt.Text = "";
                    statusTxt.Text = "";

                    consumptionGrid.Rows.Clear();
                    // collectionGrid.Rows.Clear();
                    
                    //daatgrid for monthly consumption
                    SqlDataAdapter consumptionCom = new SqlDataAdapter("select MeterInformations.rows,MeterInformations.date,MeterInformations.consumption,MeterInformations.amount,MeterInformations.status from registrationTable,MeterInformations where registrationTable.userID=MeterInformations.userID and registrationTable.userID=" + userIDTxt.Text + "order by date desc", AdminSide_PaymentCon);
                    consumptionCom.Fill(consumptionGrid);
                    MonthlyConsumption1.DataSource = consumptionGrid;
                }   
            } else if(waterComboBox.SelectedItem == "Update") {
                if (dateTxt1.Text == "" || dateTxt1.Text == null || consumptionTxt.Text == "" || consumptionTxt.Text == null) {
                    MessageBox.Show("All Fields Required");

                } else {
                        /*1 to 10 150.45
                     * 11 to 20 + by 16.80
                     * 21 to 30 + by 19.77
                     * 30 and up + by 25.50 */
                    double amountOfWaterBills;
                    int consump = int.Parse(consumptionTxt.Text);

                    if (consump <= 10 && consump >= 1) {
                        amountOfWaterBills = 150.45;
                    } else if (consump <= 20 && consump >= 11) {
                        amountOfWaterBills = 150.45 + 16.80;
                    } else if (consump <= 30 && consump >= 21) {
                        amountOfWaterBills = 150.45 + 19.77;
                    } else if (consump >= 31) {
                        amountOfWaterBills = 150.45 + 25.50;
                    } else {
                        amountOfWaterBills = 0;
                    }

                    string totalAmountOfWaterBills = String.Format("{0:0.00}", amountOfWaterBills);
                    SqlCommand updateMeterInformationCom = new SqlCommand("update MeterInformations set date='" + dateTxt1.Text + "',consumption='" + consumptionTxt.Text + "',amount='" + totalAmountOfWaterBills + "',status='" + statusTxt.Text + "' where rows=" + number + "", AdminSide_PaymentCon);
                    updateMeterInformationCom.ExecuteNonQuery();
                    MessageBox.Show("Succesfully Updated");

                    dateTxt1.Text = "";
                    consumptionTxt.Text = "";
                    statusTxt.Text = "";

                    consumptionGrid.Rows.Clear();

                    // daatgrid for monthly consumption 
                    SqlDataAdapter consumptionCom = new SqlDataAdapter("select MeterInformations.rows,MeterInformations.date,MeterInformations.consumption,MeterInformations.amount,MeterInformations.status from registrationTable,MeterInformations where registrationTable.userID=MeterInformations.userID and registrationTable.userID=" + userIDTxt.Text + "order by date desc", AdminSide_PaymentCon);
                    consumptionCom.Fill(consumptionGrid);
                    MonthlyConsumption1.DataSource = consumptionGrid;
                }
            }
        }

        private void collectionComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if(collectionComboBox.Text == "Save") {
                if (date2Txt.Text == "" || amountTxt.Text == "" || statusTxt1.Text == "") {
                    MessageBox.Show("All Fields Required");
                } else {
                    SqlCommand addMontlyDueInformation = new SqlCommand("insert into MonthlyDues(UserID, date, amount, status)values('" + userIDTxt.Text + "','" + date2Txt.Text + "','" + amountTxt.Text + "','" + statusTxt1.Text + "')", AdminSide_PaymentCon);
                    addMontlyDueInformation.ExecuteNonQuery();
                    MessageBox.Show("Successfully Added");

                    //for advance payment
                    if (double.Parse(advanceBalanceTxt.Text) < double.Parse(amountTxt.Text)) {
                        MessageBox.Show("The remaining balance is less than by the amount required");
                    } else {
                        double totalForAdvancePayment = double.Parse(advanceBalanceTxt.Text) - double.Parse(amountTxt.Text);
                        advanceBalanceTxt.Text = totalForAdvancePayment.ToString();

                        SqlCommand updateAdvancePayment = new SqlCommand("update AdvancePayment set remainingBalance='" + advanceBalanceTxt.Text + "' where rows=" + rowNumTxt.Text + "", AdminSide_PaymentCon);
                        updateAdvancePayment.ExecuteNonQuery();
                    }

                    collectionGrid.Rows.Clear();

                    SqlDataAdapter MonthlyDuesCom = new SqlDataAdapter("select MonthlyDues.rows,MonthlyDues.date,MonthlyDues.status,MonthlyDues.amount from registrationTable,MonthlyDues where registrationTable.userID=MonthlyDues.userID and registrationTable.userID=" + userIDTxt.Text + "order by date desc", AdminSide_PaymentCon);
                    MonthlyDuesCom.Fill(collectionGrid);
                    MonthlyCollectionGrid1.DataSource = collectionGrid;

                    date2Txt.Text = "";
                    amountTxt.Text = "";
                    statusTxt1.Text = "";
                }
            } else if(collectionComboBox.Text =="Delete") {
                if (date2Txt.Text == "" || amountTxt.Text == "" || statusTxt1.Text == "") {
                    MessageBox.Show("All Fields Required");
                } else {
                    SqlCommand deleteMonthlyDuesCon = new SqlCommand("delete from MonthlyDues where rows = " + number2 + " ", AdminSide_PaymentCon);
                    deleteMonthlyDuesCon.ExecuteNonQuery();

                    MessageBox.Show("Record Deleted");

                    date2Txt.Text = "";
                    amountTxt.Text = "";
                    statusTxt1.Text = "";

                    int rowIndex = MonthlyCollectionGrid1.CurrentCell.RowIndex;
                    MonthlyCollectionGrid1.Rows.RemoveAt(rowIndex);
                }
            } else if(collectionComboBox.Text=="Update") {
                if (date2Txt.Text == "" || date2Txt.Text == null || amountTxt.Text == "" || amountTxt.Text == null || statusTxt1.Text == "" || statusTxt1.Text == null) {
                    MessageBox.Show("All Fields Required");
                } else {
                    SqlCommand updateMeterInformationCom = new SqlCommand("update MonthlyDues set date='" + date2Txt.Text + "',amount='" + amountTxt.Text + "',status='" + statusTxt1.Text + "' where rows=" + number2 + "", AdminSide_PaymentCon);
                    updateMeterInformationCom.ExecuteNonQuery();
                    MessageBox.Show("Succesffuly Updated");

                    date2Txt.Text = "";
                    amountTxt.Text = "";
                    statusTxt1.Text = "";

                    collectionGrid.Rows.Clear();

                    //data grid for monthly collections
                    SqlDataAdapter collectionCom = new SqlDataAdapter("select MonthlyDues.rows,MonthlyDues.date,MonthlyDues.amount,MonthlyDues.status from registrationTable,MonthlyDues where registrationTable.userID=MonthlyDues.userID and registrationTable.userID=" + userIDTxt.Text + "order by date desc", AdminSide_PaymentCon);
                    collectionCom.Fill(collectionGrid);
                    MonthlyCollectionGrid1.DataSource = collectionGrid;
                }
            }
        }

        private void statusComboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            if(statusComboBox1.SelectedItem == "completed") {
                statusTxt.Text = "completed";
            } else {
                statusTxt.Text = "pending";
            }
        }

        private void statusTxtComboBox2_SelectedIndexChanged(object sender, EventArgs e) {
            if (statusTxtComboBox2.SelectedItem == "completed") {
                statusTxt1.Text = "completed";
            } else {
                statusTxt1.Text = "pending";
            }
        }

        private void comboBoxIdNumber_SelectedIndexChanged(object sender, EventArgs e) {
            userIDTxt.Text = comboBoxIdNumber.Text;
            idNumber = userIDTxt.Text;            
        }

        private void ThreadAdvancePayment() {
            Application.Run(new AdminSide_AdvancePayment());
        }

        private void paymentComboBoxTab_SelectedIndexChanged(object sender, EventArgs e) {
            if(paymentComboBoxTab.SelectedItem == "Latest") {
                latestBillingPanel.Visible = true;
            } else {
                latestBillingPanel.Visible = false;
            }
            if (paymentComboBoxTab.SelectedItem == "Advance") {
                Thread ThreadStart = new Thread(new ThreadStart(ThreadAdvancePayment)); //you create a new thread
                ThreadStart.SetApartmentState(ApartmentState.STA);
                ThreadStart.Start();
            }

        }

        string monthName = "";

        private void latestPaymentOptionComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (latestPaymentOptionComboBox.SelectedItem == "Save") {
                if (latestDate.Text == "" || latestConsumption.Text == "" || latestCollection.Text == "") {
                    MessageBox.Show("All Fields Required");
                } else {
                    DateTime thisDate = DateTime.Parse(latestDate.Text);
                    switch (thisDate.Month) {
                        case 1:
                            monthName = "Jan";
                            break;
                        case 2:
                            monthName = "Feb";
                            break;
                        case 3:
                            monthName = "Mar";
                            break;
                        case 4:
                            monthName = "Apr";
                            break;
                        case 5:
                            monthName = "May";
                            break;
                        case 6:
                            monthName = "Jun";
                            break;
                        case 7:
                            monthName = "Jul";
                            break;
                        case 8:
                            monthName = "Aug";
                            break;
                        case 9:
                            monthName = "Sep";
                            break;
                        case 10:
                            monthName = "Oct";
                            break;
                        case 11:
                            monthName = "Nov";
                            break;
                        case 12:
                            monthName = "Dec";
                            break;
                    }

                    int consump1 = int.Parse(latestConsumption.Text);
                    double amountOfWaterBills1;

                    if (consump1 <= 10 && consump1 >= 1) {
                        amountOfWaterBills1 = 150.45;
                    } else if (consump1 <= 20 && consump1 >= 11) {
                        amountOfWaterBills1 = 150.45 + 16.80;
                    } else if (consump1 <= 30 && consump1 >= 21) {
                        amountOfWaterBills1 = 150.45 + 19.77;
                    } else if (consump1 >= 31) {
                        amountOfWaterBills1 = 150.45 + 25.50;
                    } else {
                        amountOfWaterBills1 = 0;
                    }


                    latestDate1 = monthName + " " + thisDate.Year;
                    latestConsumptionAmount.Text = amountOfWaterBills1.ToString();


                    SqlCommand addLatestPayment = new SqlCommand("insert into latestPayment(UserID, latestDate, latestConsumption, latestConsumptionAmount, latestCollectionAmount,billingdueDate)values('" + userIDTxt.Text + "','" + latestDate1 + "','" + latestConsumption.Text + "','" + amountOfWaterBills1 + "','" + latestCollection.Text + "','" + latestDate.Text + "')", AdminSide_PaymentCon);
                    addLatestPayment.ExecuteNonQuery();

                    latestPaymentGrid.Rows.Clear();

                    //daatgrid for latest payments
                    SqlDataAdapter latestPaymentCom = new SqlDataAdapter("select *from latestPayment order by userID desc", AdminSide_PaymentCon);
                    latestPaymentCom.Fill(latestPaymentGrid);
                    latestPaymentDataGrid.DataSource = latestPaymentGrid;

                    latestPaymentDataGrid.Columns[0].Width = 1;
                    latestPaymentDataGrid.Columns[1].Width = 90;
                    latestPaymentDataGrid.Columns[3].Width = 90;
                    

                    //pending monthly consumption
                    string pendingTxt = "pending";
                    string[] monthWithPendingStatus = new string[12];
                    double[] monthWithPendingConsumption = new double[12];
                    int count1 = 0;

                    SqlCommand comTblPendingofConsumption = new SqlCommand("select month(date),sum(amount),status from MeterInformations  where status LIKE '%" + pendingTxt + "%' and userID LIKE '%" + userIDTxt.Text + "%' group by month(date),status", AdminSide_PaymentCon);

                    SqlDataReader reader1 = comTblPendingofConsumption.ExecuteReader();

                    while (reader1.Read()) {
                        count1++;
                        monthWithPendingStatus[count1] = reader1[0].ToString();
                        monthWithPendingConsumption[count1] = double.Parse(reader1[1].ToString());


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

                    SqlCommand comTblPendingofCollection = new SqlCommand("select month(date),sum(amount),status from MonthlyDues  where status LIKE '%" + pendingTxt + "%' and userID LIKE '%" + userIDTxt.Text + "%' group by month(date),status", AdminSide_PaymentCon);

                    SqlDataReader reader2 = comTblPendingofCollection.ExecuteReader();


                    while (reader2.Read()) {
                        count2++;
                        monthWithPendingStatus1[count2] = reader2[0].ToString();
                        monthWithPendingCollection[count2] = double.Parse(reader2[1].ToString());

                        switch (monthWithPendingStatus[count2]) {
                            case "1":
                                jan3 = monthWithPendingConsumption[count2];
                                break;

                            case "2":
                                feb3 = monthWithPendingConsumption[count2];
                                break;

                            case "3":
                                mar3 = monthWithPendingConsumption[count2];
                                break;

                            case "4":
                                apr3 = monthWithPendingConsumption[count2];
                                break;

                            case "5":
                                may3 = monthWithPendingConsumption[count2];
                                break;

                            case "6":
                                jun3 = monthWithPendingConsumption[count2];
                                break;

                            case "7":
                                jul3 = monthWithPendingConsumption[count2];
                                break;

                            case "8":
                                aug3 = monthWithPendingConsumption[count2];
                                break;

                            case "9":
                                sep3 = monthWithPendingConsumption[count2];
                                break;

                            case "10":
                                oct3 = monthWithPendingConsumption[count2];
                                break;

                            case "11":
                                nov3 = monthWithPendingConsumption[count2];
                                break;
                        }
                    }

                    latestTotal.Text = "0";
                    outstandingBalanceTxt.Text = "0";
                    reader2.Close();

                    if (latestConsumptionAmount.Text == "") {
                    } else {
                        latestTotal.Text = (double.Parse(latestConsumptionAmount.Text) + double.Parse(latestCollection.Text)).ToString();
                    }
                    outstandingBalanceTxt.Text = String.Format("{0:0.00}", (jan2 + feb2 + mar2 + apr2 + may2 + jun2 + jul2 + aug2 + sep2 + oct2 + nov2 + dec2 + jan3 + feb3 + mar3 + apr3 + may3 + jun3 + jul3 + aug3 + sep3 + oct3 + nov3 + dec3)).ToString();
                    
                    MessageBox.Show("Sucessfully Added");

                    userIDTxt.Text = "";
                    fnameTxt.Text = "";
                    lnameTxt.Text = "";
                    latestDate.Text = "";
                    latestConsumption.Text = "";
                    latestConsumptionAmount.Text = "";
                    latestCollection.Text = "";
                    latestTotal.Text = "0";
                }
                
            } else if (latestPaymentOptionComboBox.SelectedItem == "Delete") {
                if (latestDate.Text == "" || latestConsumption.Text == "" || latestCollection.Text == "") {
                    MessageBox.Show("All Fields Required");
                } else {
                    SqlCommand deleteLatestPayment = new SqlCommand("delete from latestPayment where rows = " + number3 + " ", AdminSide_PaymentCon);
                    deleteLatestPayment.ExecuteNonQuery();

                    MessageBox.Show("Record Deleted");

                    userIDTxt.Text = "";
                    fnameTxt.Text = "";
                    lnameTxt.Text = "";
                    latestDate.Text = "";
                    latestConsumption.Text = "";
                    latestConsumptionAmount.Text = "";
                    latestCollection.Text = "";
                    latestTotal.Text = "0";

                    int rowIndex = latestPaymentDataGrid.CurrentCell.RowIndex;
                    latestPaymentDataGrid.Rows.RemoveAt(rowIndex);
                }
            } else if(latestPaymentOptionComboBox.SelectedItem == "Update") {
                if (latestDate.Text == "" || latestConsumption.Text == "" || latestCollection.Text == "") {
                    MessageBox.Show("All Fields Required");
                } else {
                    DateTime thisDate = DateTime.Parse(latestDate.Text);
                    switch (thisDate.Month) {
                        case 1:
                            monthName = "Jan";
                            break;
                        case 2:
                            monthName = "Feb";
                            break;
                        case 3:
                            monthName = "Mar";
                            break;
                        case 4:
                            monthName = "Apr";
                            break;
                        case 5:
                            monthName = "May";
                            break;
                        case 6:
                            monthName = "Jun";
                            break;
                        case 7:
                            monthName = "Jul";
                            break;
                        case 8:
                            monthName = "Aug";
                            break;
                        case 9:
                            monthName = "Sep";
                            break;
                        case 10:
                            monthName = "Oct";
                            break;
                        case 11:
                            monthName = "Nov";
                            break;
                        case 12:
                            monthName = "Dec";
                            break;
                    }

                    latestDate1 = monthName + " " + thisDate.Year;

                    int consump1 = int.Parse(latestConsumption.Text);
                    double amountOfWaterBills1;

                    if (consump1 <= 10 && consump1 >= 1) {
                        amountOfWaterBills1 = 150.45;
                    } else if (consump1 <= 20 && consump1 >= 11) {
                        amountOfWaterBills1 = 150.45 + 16.80;
                    } else if (consump1 <= 30 && consump1 >= 21) {
                        amountOfWaterBills1 = 150.45 + 19.77;
                    } else if (consump1 >= 31) {
                        amountOfWaterBills1 = 150.45 + 25.50;
                    } else {
                        amountOfWaterBills1 = 0;
                    }

                    latestConsumptionAmount.Text = amountOfWaterBills1.ToString();

                    SqlCommand updateLatestPayments = new SqlCommand("update latestPayment set latestDate='" + latestDate1 + "',latestConsumption='" + latestConsumption.Text + "',latestConsumptionAmount='" + latestConsumptionAmount.Text + "',latestCollectionAmount='" + latestCollection.Text + "',billingdueDate='" + latestDate.Text + "' where rows=" + number3 + "", AdminSide_PaymentCon);
                    updateLatestPayments.ExecuteNonQuery();

                    MessageBox.Show("Succesffuly Updated");

                    userIDTxt.Text = "";
                    fnameTxt.Text = "";
                    lnameTxt.Text = "";
                    latestDate.Text = "";
                    latestConsumption.Text = "";
                    latestConsumptionAmount.Text = "";
                    latestCollection.Text = "";
                    latestTotal.Text = "0";

                    latestPaymentGrid.Rows.Clear();

                    //daatgrid for latest payments
                    SqlDataAdapter latestPaymentCom = new SqlDataAdapter("select *from latestPayment order by userID desc", AdminSide_PaymentCon);
                    latestPaymentCom.Fill(latestPaymentGrid);
                    latestPaymentDataGrid.DataSource = latestPaymentGrid;

                    latestPaymentDataGrid.Columns[0].Width = 1;
                    latestPaymentDataGrid.Columns[1].Width = 90;
                    latestPaymentDataGrid.Columns[3].Width = 90;

                    //pending monthly consumption
                    string pendingTxt = "pending";
                    string[] monthWithPendingStatus = new string[12];
                    double[] monthWithPendingConsumption = new double[12];
                    int count1 = 0;

                    SqlCommand comTblPendingofConsumption = new SqlCommand("select month(date),sum(amount),status from MeterInformations  where status LIKE '%" + pendingTxt + "%' and userID LIKE '%" + userIDTxt.Text + "%' group by month(date),status", AdminSide_PaymentCon);

                    SqlDataReader reader1 = comTblPendingofConsumption.ExecuteReader();

                    while (reader1.Read()) {
                        count1++;
                        monthWithPendingStatus[count1] = reader1[0].ToString();
                        monthWithPendingConsumption[count1] = double.Parse(reader1[1].ToString());

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

                    SqlCommand comTblPendingofCollection = new SqlCommand("select month(date),sum(amount),status from MonthlyDues  where status LIKE '%" + pendingTxt + "%' and userID LIKE '%" + userIDTxt.Text + "%' group by month(date),status", AdminSide_PaymentCon);

                    SqlDataReader reader2 = comTblPendingofCollection.ExecuteReader();


                    while (reader2.Read()) {
                        count2++;
                        monthWithPendingStatus1[count2] = reader2[0].ToString();
                        monthWithPendingCollection[count2] = double.Parse(reader2[1].ToString());

                        switch (monthWithPendingStatus[count2]) {
                            case "1":
                                jan3 = monthWithPendingConsumption[count2];
                                break;

                            case "2":
                                feb3 = monthWithPendingConsumption[count2];
                                break;

                            case "3":
                                mar3 = monthWithPendingConsumption[count2];
                                break;

                            case "4":
                                apr3 = monthWithPendingConsumption[count2];
                                break;

                            case "5":
                                may3 = monthWithPendingConsumption[count2];
                                break;

                            case "6":
                                jun3 = monthWithPendingConsumption[count2];
                                break;

                            case "7":
                                jul3 = monthWithPendingConsumption[count2];
                                break;

                            case "8":
                                aug3 = monthWithPendingConsumption[count2];
                                break;

                            case "9":
                                sep3 = monthWithPendingConsumption[count2];
                                break;

                            case "10":
                                oct3 = monthWithPendingConsumption[count2];
                                break;

                            case "11":
                                nov3 = monthWithPendingConsumption[count2];
                                break;
                        }
                    }

                    reader2.Close();
                    if (latestConsumptionAmount.Text == "") {                 
                    } else {
                        latestTotal.Text = (double.Parse(latestConsumptionAmount.Text) + double.Parse(latestCollection.Text)).ToString();
                    }
                    outstandingBalanceTxt.Text = String.Format("{0:0.00}", (jan2 + feb2 + mar2 + apr2 + may2 + jun2 + jul2 + aug2 + sep2 + oct2 + nov2 + dec2 + jan3 + feb3 + mar3 + apr3 + may3 + jun3 + jul3 + aug3 + sep3 + oct3 + nov3 + dec3)).ToString();
                }
            }
        }



    }
}
