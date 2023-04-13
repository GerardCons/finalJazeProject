using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CTHardware_EmployeeManagement
{
    public partial class createPayroll : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        payrollList frmlist;
        SqlDataReader drr;
        public createPayroll(payrollList frm)
        {
            cn = new SqlConnection(dbcon.MyConnection());
            InitializeComponent();
            frmlist= frm;
        }

        private void tbt_firstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime dateTo = dtp_to.Value;
            DateTime dateToOneAM = new DateTime(dateTo.Year, dateTo.Month, dateTo.Day, 0, 0, 0);
            DateTime dateFrom = dtp_from.Value;
            DateTime dateFromOneAM = new DateTime(dateFrom.Year, dateFrom.Month, dateFrom.Day, 0, 0, 0);
            TimeSpan difference = dateTo - dateFrom;
            int totalDifference = difference.Days + 2;


            bool overlapExists = await CheckDateRangeOverlapAsync(dateFrom, dateTo);
            if (overlapExists) {
                MessageBox.Show("The date range you have selected for the payroll is already existing in our database, please try a date range for the new payroll");
            
            }
            else
            {
                try
                {

                    if (MessageBox.Show("Are you sure you want to save this payroll?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (difference.Days > 0)
                        {
                            cn.Open();
                            cm = new SqlCommand("INSERT INTO tbl_payroll(payroll_Id, dateTo, dateFrom, totalDays)" +
                                "VALUES(@payroll_Id, @dateTo, @dateFrom, @totalDays)", cn);
                            cm.Parameters.AddWithValue("@payroll_Id", tbt_payroll.Text);
                            cm.Parameters.AddWithValue("@dateTo", dateToOneAM);
                            cm.Parameters.AddWithValue("@dateFrom", dateFromOneAM);
                            cm.Parameters.AddWithValue("@totalDays", totalDifference);
                            cm.ExecuteNonQuery();
                            cn.Close();

                            cn.Open();
                            cm = new SqlCommand("select * from tbl_employeesInfo order by id asc", cn);

                            SqlDataAdapter da = new SqlDataAdapter(cm);
                            DataSet ds = new DataSet();
                            da.Fill(ds, "employees");

                            foreach (DataRow dr in ds.Tables["employees"].Rows)
                            {
                                string employeeId = dr["employeeId"].ToString();
                                string firstName = dr["firstName"].ToString();
                                string lastName = dr["lastName"].ToString();
                                string position = dr["position"].ToString();
                                string basicSalary = dr["basicSalary"].ToString();

                                Double totalOvertime = 0;
                                Double totalUnderTime = 0;
                                Double totalDeductions = 0;
                                Double withHoldings = 0;
                                double totalGross = 0;
                                double netPay = 0;
                                double bonus = 0;
                                using (cm = new SqlCommand("SELECT * FROM tbl_attendancesInfo WHERE  employeeId = @employeeId AND date >= @startDate AND date <= @endDate", cn))
                                {
                                    cm.Parameters.AddWithValue("@startDate", dateFromOneAM);
                                    cm.Parameters.AddWithValue("@employeeId", employeeId);
                                    cm.Parameters.AddWithValue("@endDate", dateToOneAM);
                                    using (drr = cm.ExecuteReader())
                                    {
                                        if (drr.HasRows)
                                        {
                                            while (drr.Read())
                                            {
                                                totalOvertime = totalOvertime + Double.Parse(drr["overTime"].ToString());
                                                totalUnderTime = totalUnderTime + Double.Parse(drr["underTime"].ToString());


                                            }
                                        }

                                    }
                                }
                                cn.Close();

                                cn.Open();

                                using (cm = new SqlCommand("SELECT * FROM tbl_deductionInfo WHERE  employeeId = @employeeId AND date >= @startDate AND date <= @endDate", cn))
                                {
                                    cm.Parameters.AddWithValue("@startDate", dateFromOneAM);
                                    cm.Parameters.AddWithValue("@employeeId", employeeId);
                                    cm.Parameters.AddWithValue("@endDate", dateToOneAM);
                                    using (drr = cm.ExecuteReader())
                                    {
                                        if (drr.HasRows)
                                        {
                                            while (drr.Read())
                                            {
                                                // Do something with the data
                                                totalDeductions = totalDeductions + Double.Parse(drr["amount"].ToString());

                                            }

                                        }

                                    }
                                }
                                cn.Close();

                                cn.Open();

                                using (cm = new SqlCommand("SELECT * FROM tbl_employeeWitholding WHERE employeeId = @employeeId", cn))
                                {
                                    cm.Parameters.AddWithValue("@employeeId", employeeId);
                                    using (drr = cm.ExecuteReader())
                                    {
                                        if (drr.HasRows)
                                        {
                                            while (drr.Read())
                                            {
                                                // Do something with the data

                                                withHoldings = Double.Parse(drr["totalTax"].ToString());


                                            }
                                        }

                                    }
                                }

                                cn.Close();


                                totalGross = (Double.Parse(basicSalary) * totalDifference);
                                netPay = totalGross - (totalUnderTime + withHoldings) + (totalOvertime + bonus);

                                cn.Open();
                                cm = new SqlCommand("INSERT INTO tbl_employeePayroll(payroll_Id, employeeId, firstName, lastName, position, dateFrom, dateTo, dailyRate, overtimeAmount, undertimeAmount, deductions, witholdings, bonus, grossPay, netPay)" +
                                    "VALUES(@payroll_Id, @employeeId, @firstName, @lastName, @position, @dateFrom, @dateTo, @dailyRate, @overtimeAmount, @undertimeAmount, @deductions, @witholdings, @bonus, @grossPay, @netPay)", cn);
                                cm.Parameters.AddWithValue("@payroll_Id", tbt_payroll.Text);
                                cm.Parameters.AddWithValue("@employeeId", employeeId);
                                cm.Parameters.AddWithValue("@firstName", firstName);
                                cm.Parameters.AddWithValue("@lastName", lastName);
                                cm.Parameters.AddWithValue("@position", position);
                                cm.Parameters.AddWithValue("@dateFrom", dateFromOneAM);
                                cm.Parameters.AddWithValue("@dateTo", dateToOneAM);
                                cm.Parameters.AddWithValue("@dailyRate", double.Parse(basicSalary));
                                cm.Parameters.AddWithValue("@overtimeAmount", totalOvertime);
                                cm.Parameters.AddWithValue("@undertimeAmount", totalUnderTime);
                                cm.Parameters.AddWithValue("@deductions", totalDeductions);
                                cm.Parameters.AddWithValue("@witholdings", withHoldings);
                                cm.Parameters.AddWithValue("@bonus", bonus);
                                cm.Parameters.AddWithValue("@grossPay", totalGross);
                                cm.Parameters.AddWithValue("@netPay", netPay);
                                cm.ExecuteNonQuery();
                            }

                            cn.Close();

                            MessageBox.Show("Record has been successfully saved");
                            Close();

                            frmlist.LoadData();


                        }
                        else
                        {
                            MessageBox.Show("Please input a valid leave duration");
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
           
        }

        private void createPayroll_Load(object sender, EventArgs e)
        {
            int i = 0;
            cn.Open();
            cm = new SqlCommand("select * from tbl_payroll order by id asc", cn);
            drr = cm.ExecuteReader();
            while (drr.Read())
            {
                i++;
            }
            i++;
            drr.Close();
            cn.Close();
            tbt_payroll.Text = i.ToString();
        }
        public Task<bool> CheckDateRangeOverlapAsync(DateTime start, DateTime end)
        {
            bool isOverlapped = false;
            cn.Open();
            cm = new SqlCommand("select * from tbl_payroll WHERE (dateFrom >= @fromDate AND dateFrom <= @toDate) " +
                "OR (dateTo >= @fromDate AND dateTo <= @toDate) " +
                "OR (@fromDate >= dateFrom AND @fromDate <= dateTo)" +
                "OR (@toDate >= dateFrom AND @toDate <= dateTo)", cn);
            cm.Parameters.AddWithValue("@fromDate", start);
            cm.Parameters.AddWithValue("@toDate", end);
            drr = cm.ExecuteReader();
            while (drr.Read())
            {
               isOverlapped= true;
            }
          
            drr.Close();
            cn.Close();
            return Task.FromResult(isOverlapped);
        }
    }
}
