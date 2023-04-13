using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Common;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CTHardware_EmployeeManagement

{
    public class EmployeePayroll
    {
        public int Id { get; set; }
        public string PayrollId { get; set; }
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public float DailyRate { get; set; }
        public float UndertimeAmount { get; set; }
        public float OvertimeAmount { get; set; }
        public float Deductions { get; set; }
        public float Witholdings { get; set; }
        public float Bonus { get; set; }
        public float GrossPay { get; set; }
        public float NetPay { get; set; }

        public EmployeePayroll(int id, string payrollId, string employeeId, string firstName, string lastName, string position, DateTime dateFrom, DateTime dateTo, float dailyRate, float undertimeAmount, float overtimeAmount, float deductions, float witholdings, float bonus, float grossPay, float netPay)
        {
            Id = id;
            PayrollId = payrollId;
            EmployeeId = employeeId;
            FirstName = firstName;
            LastName = lastName;
            Position = position;
            DateFrom = dateFrom;
            DateTo = dateTo;
            DailyRate = dailyRate;
            UndertimeAmount = undertimeAmount;
            OvertimeAmount = overtimeAmount;
            Deductions = deductions;
            Witholdings = witholdings;
            Bonus = bonus;
            GrossPay = grossPay;
            NetPay = netPay;
        }
    }
    public partial class addAttendance : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        attendancesList frmlist;
        double basicSalary = 0;
        String status = "";
        public addAttendance(attendancesList frm)
        {
            cn = new SqlConnection(dbcon.MyConnection());
            InitializeComponent();
            frmlist= frm;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void addAttendance_Load(object sender, EventArgs e)
        {
            dtp_date.Value = DateTime.Now;
            dtp_timeIn.Value = DateTime.Now;
            dtp_timeOut.Value = DateTime.Now;
            dtp_timeIn.Format = DateTimePickerFormat.Time;
            dtp_timeOut.Format = DateTimePickerFormat.Time;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                cn.Open();

                using (cm = new SqlCommand("SELECT * FROM tbl_employeesInfo WHERE employeeId = @employeeId", cn))
                {
                    cm.Parameters.AddWithValue("@employeeId", tbt_employeeId.Text);
                    using (dr = cm.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                // Do something with the data
             
                                tbt_firstName.Text = dr["firstName"].ToString();
                                tbt_lastName.Text = dr["lastName"].ToString();
                                tbt_position.Text = dr["position"].ToString();
                                basicSalary = double.Parse(dr["basicSalary"].ToString());
                            }
                        }
                        else
                        {
                            MessageBox.Show("No Employee found for the search ID: " + tbt_employeeId.Text, "Search Employee", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to the database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
        }
        private void Clear()
        {
            tbt_employeeId.Clear();
            tbt_firstName.Clear();
            tbt_lastName.Clear();
            tbt_lastName.Clear();
            tbt_position.Clear();
            dtp_date.Value = DateTime.Now;
            dtp_timeIn.Value = DateTime.Now;
            dtp_timeOut.Value = DateTime.Now;
            checkBox1.Checked = false;
            rtbt_comment.Clear();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<EmployeePayroll> employeePayrolls = new List<EmployeePayroll>();

            try
            {
                // Global Variabkle
                int expectedWorkHours = 9;
                String checkStatusTimeIn = "";
                String checkStatusTimeOut = "";
                DateTime attendanceDate = dtp_date.Value.Date;
                DateTime timeOutValue = dtp_timeOut.Value;
                DateTime timeInValue = dtp_timeIn.Value;
                double hourlyRate = basicSalary / expectedWorkHours;
                double overtimeAmountDay = 0;
                double underTimeAmountDay = 0;


                // Get the Desired Time In and Out && Current Time In and Out
                TimeSpan desiredTimeOut = new TimeSpan(17, 0, 0); // 5:00 PM
                TimeSpan desiredTimeIn= new TimeSpan(8, 0, 0); // 8:00 AM
                TimeSpan dateTimeIn= timeInValue.TimeOfDay; // Extract the time portion from the DateTime value
                TimeSpan dateTimeOut= timeOutValue.TimeOfDay; // Extract the time portion from the DateTime value

                //Calculate Overtime
                double overtimeRate = hourlyRate * 1.25;
                TimeSpan totalOvetime = dateTimeOut - desiredTimeOut;
                double totalOvertimeHours = totalOvetime.TotalHours;
                overtimeAmountDay = totalOvertimeHours * overtimeRate;

                //Calculate Undertime
                TimeSpan totalUndertime = dateTimeIn - desiredTimeIn;
                double totalUndertimeHours = totalUndertime.TotalHours;
                underTimeAmountDay = totalUndertimeHours * hourlyRate;

              
                // Check wether the employee is absent or present (if dateTimeIn is greater than 8:00 am status will be late)
                if (checkBox1.Checked)
                {
                    status = "Absent";
                }
                else
                {
                    if (dateTimeIn <= desiredTimeIn)
                    {
                        status = "Present";
                    }
                    else
                    {
                        status = "Late";
                    }
                }

                try
                {

                    cn.Open();
                    cm = new SqlCommand("SELECT * FROM tbl_employeePayroll where employeeId like '" + tbt_employeeId.Text + "'", cn);
                    dr = cm.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {

                            employeePayrolls.Add(new EmployeePayroll(
             id: (int)dr["id"],
             payrollId: (string)dr["payroll_Id"],
             employeeId: (string)dr["employeeId"],
             firstName: (string)dr["firstName"],
             lastName: (string)dr["lastName"],
             position: (string)dr["position"],
             dateFrom: (DateTime)dr["dateFrom"],
             dateTo: (DateTime)dr["dateTo"],
             dailyRate: (float)(double)dr["dailyRate"],
             undertimeAmount: (float)(double)dr["undertimeAmount"],
             overtimeAmount: (float)(double)dr["overtimeAmount"],
             deductions: (float)(double)dr["deductions"],
             witholdings: (float)(double)dr["witholdings"],
             bonus: (float)(double)dr["bonus"],
             grossPay: (float)(double)dr["grossPay"],
             netPay: (float)(double)dr["netPay"]
         ));

                        }
                    }

                    dr.Close();
                    cn.Close();


                    EmployeePayroll[] employeePayrollsArray = employeePayrolls.ToArray();

                    int id = 0;
                    double totalOvertimeAmount = 0;
                    double totalUndertimeAmount = 0;
                    double totalGross = 0;
                    double netPay = 0;
                    
   
                    foreach (EmployeePayroll record in employeePayrollsArray)
                    {
                        // Add your code here to process each record
                        // Example: Print the fields of each record
                        if (attendanceDate >= record.DateFrom && attendanceDate <= record.DateTo)
                        {
                            id = record.Id;
                            if (checkBox1.Checked)
                            {
                                totalOvertimeAmount = 0 + record.OvertimeAmount;
                                totalGross = record.GrossPay;
                                totalUndertimeAmount = basicSalary + record.UndertimeAmount;
                                netPay = totalGross - (totalUndertimeAmount + record.Witholdings + record.Deductions) + (totalOvertimeAmount + record.Bonus);
                                break;

                            }
                            else
                            {
                                totalOvertimeAmount = float.Parse(overtimeAmountDay.ToString()) + record.OvertimeAmount;
                                totalUndertimeAmount = float.Parse(underTimeAmountDay.ToString()) + record.UndertimeAmount;
                                totalGross = record.GrossPay;
                                netPay = totalGross - (totalUndertimeAmount + record.Witholdings + record.Deductions) + (totalOvertimeAmount + record.Bonus);
                                break;

                            }
                            
                        }
                    }

                    double roundedtotalOvertimeAmount = Math.Round(totalOvertimeAmount, 2);
                    double roundedtotalUndertimeAmount = Math.Round(totalUndertimeAmount, 2);
                    double roundedtotalGross = Math.Round(totalGross, 2);
                    double roundednetPay = Math.Round(netPay, 2);
                    if (id > 0)
                    {

                        cn.Open();
                        cm = new SqlCommand("update tbl_employeePayroll set overtimeAmount = @overtimeAmount, undertimeAmount = @undertimeAmount, grossPay = @grossPay, netPay = @netPay where id like '" + id + "'", cn);
                        cm.Parameters.AddWithValue("@overtimeAmount", roundedtotalOvertimeAmount);
                        cm.Parameters.AddWithValue("@undertimeAmount", roundedtotalUndertimeAmount);
                        cm.Parameters.AddWithValue("@grossPay", roundedtotalGross);
                        cm.Parameters.AddWithValue("@netPay", roundednetPay);
                        cm.ExecuteNonQuery();
                        cn.Close();
                    }



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


                if (MessageBox.Show("Are you sure you want to save this attendance?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (checkBox1.Checked) { checkStatusTimeIn = "not applicable"; checkStatusTimeOut = "not applicable"; } else { checkStatusTimeIn = dtp_timeIn.Text; checkStatusTimeOut = dtp_timeOut.Text; }
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO tbl_attendancesInfo(employeeId, firstName, lastName, position, date, timeIn, timeOut, status, comment, overTime, underTime)" +
                        "VALUES(@employeeId, @firstName, @lastName, @position, @date, @timeIn, @timeOut, @status, @comment, @overTime, @underTime)", cn);
                    cm.Parameters.AddWithValue("@employeeId", tbt_employeeId.Text);
                    cm.Parameters.AddWithValue("@firstName", tbt_firstName.Text);
                    cm.Parameters.AddWithValue("@lastName", tbt_lastName.Text);
                    cm.Parameters.AddWithValue("@position", tbt_position.Text);
                    cm.Parameters.AddWithValue("@date", attendanceDate);
                    cm.Parameters.AddWithValue("@timeIn", checkStatusTimeIn);
                    cm.Parameters.AddWithValue("@timeOut", checkStatusTimeOut);
                    cm.Parameters.AddWithValue("@status", status);
                    cm.Parameters.AddWithValue("@comment", rtbt_comment.Text);
                    cm.Parameters.AddWithValue("@overTime", (checkBox1.Checked) ? 0 : overtimeAmountDay);
                    cm.Parameters.AddWithValue("@underTime", (checkBox1.Checked) ? basicSalary : underTimeAmountDay);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been successfully saved");
                    Clear();
                    frmlist.LoadData();
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cb_status_SelectedIndexChanged(object sender, EventArgs e)
        {
    
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dtp_timeIn.Enabled = !checkBox1.Checked;
            dtp_timeOut.Enabled = !checkBox1.Checked;
            if (checkBox1.Checked)
            {
                status = "Absent";
             
            }else
            {
                status = "Present";
               
            }
        }
    }
}
