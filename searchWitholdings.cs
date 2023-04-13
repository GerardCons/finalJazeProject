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
using System.Windows.Forms.VisualStyles;
using static CTHardware_EmployeeManagement.createDeduction;
using static CTHardware_EmployeeManagement.searchWitholdings;

namespace CTHardware_EmployeeManagement
{
    public partial class searchWitholdings : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        payrollList frmlist;
        bool isFetched = false;

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
        public searchWitholdings(payrollList flist)
        {
            cn = new SqlConnection(dbcon.MyConnection());
            InitializeComponent();
            frmlist = flist;
        }

        private void createWitholdings_Load(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
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

                            }
                        }
                        else
                        {
                            MessageBox.Show("No Employee found for the search ID: " + tbt_employeeId.Text, "Search Employee", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                using (cm = new SqlCommand("SELECT * FROM tbl_employeeWitholding WHERE employeeId = @employeeId", cn))
                {
                    cm.Parameters.AddWithValue("@employeeId", tbt_employeeId.Text);
                    using (dr = cm.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                // Do something with the data

                                tbt_pagibigId.Text = dr["pagIbigId"].ToString();
                                tbt_philHealthId.Text = dr["philHealthId"].ToString();
                                tbt_sssId.Text = dr["sssId"].ToString();
                                tbt_pagibigTax.Text = dr["pagIbigAmount"].ToString();
                                tbt_philHealthTax.Text = dr["philHealthAmount"].ToString();
                                tbt_sssTax.Text = dr["sssAmount"].ToString();
                                tbt_basicSalary.Text = dr["salaryAmount"].ToString();
                                tbt_totalAmount.Text = dr["totalTax"].ToString();
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
                isFetched = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {


                List<EmployeePayroll> employeePayrolls = new List<EmployeePayroll>();

                if (MessageBox.Show("Are you sure you want to update this data?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("update tbl_employeeWitholding set philHealthId = @philHealthId, sssId = @sssId, pagIbigId = @pagIbigId, philHealthAmount = @philHealthAmount, sssAmount = @sssAmount, pagIbigAmount = @pagIbigAmount" +
                        ", salaryAmount = @salaryAmount, totalTax = @totalTax " +
                        "where employeeId like '" + tbt_employeeId.Text + "'", cn);

                    cm.Parameters.AddWithValue("@philHealthId", tbt_philHealthId.Text);
                    cm.Parameters.AddWithValue("@sssId", tbt_sssId.Text);
                    cm.Parameters.AddWithValue("@pagIbigId", tbt_pagibigId.Text);
                    cm.Parameters.AddWithValue("@philHealthAmount", double.Parse(tbt_philHealthTax.Text));
                    cm.Parameters.AddWithValue("@sssAmount", double.Parse(tbt_sssTax.Text));
                    cm.Parameters.AddWithValue("@pagIbigAmount", double.Parse(tbt_pagibigTax.Text));
                    cm.Parameters.AddWithValue("@salaryAmount", double.Parse(tbt_basicSalary.Text));
                    cm.Parameters.AddWithValue("@totalTax", double.Parse(tbt_totalAmount.Text));
                    cm.ExecuteNonQuery();
                    cn.Close();


                  
                    cn.Open();
                    cm = new SqlCommand("select * from tbl_employeePayroll where employeeId = @employeeId", cn);
                    cm.Parameters.AddWithValue("@employeeId", tbt_employeeId.Text);
                    dr = cm.ExecuteReader();
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
                    dr.Close();
                    cn.Close();

                    EmployeePayroll[] employeePayrollsArray = employeePayrolls.ToArray();

                    foreach (EmployeePayroll record in employeePayrollsArray)
                    {
                        // Add your code here to process each record
                        // Example: Print the fields of each record
                        double netpay = 0;
                        netpay = record.GrossPay - (double.Parse(tbt_totalAmount.Text) + record.Deductions + record.UndertimeAmount) + (record.OvertimeAmount + record.Bonus);
                        cn.Open();
                        cm = new SqlCommand("update tbl_employeePayroll set witholdings = @witholdings, netpay = @netpay where id like '" + record.Id + "'", cn);
                        cm.Parameters.AddWithValue("@witholdings", float.Parse(tbt_totalAmount.Text));
                        cm.Parameters.AddWithValue("@netpay", float.Parse(netpay.ToString()));
                        cm.ExecuteNonQuery();
                        cn.Close();
                        this.Dispose();

                    }

                    frmlist.LoadData();

                    MessageBox.Show("Employeee Witholdings has been updated successfully");
                    this.Dispose();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbt_sssTax_TextChanged(object sender, EventArgs e)
        {
   
     
        }

        private void tbt_philHealthTax_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbt_pagibigTax_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void tbt_sssTax_Leave(object sender, EventArgs e)
        {
            if (isFetched == true)
            {
                double totalNet = 0;
                totalNet = double.Parse(tbt_sssTax.Text) + double.Parse(tbt_philHealthTax.Text) + double.Parse(tbt_pagibigTax.Text);
                tbt_totalAmount.Text = totalNet.ToString();
            }
        }

        private void tbt_philHealthTax_Leave(object sender, EventArgs e)
        {
            if (isFetched == true)
            {
                double totalNet = 0;
                totalNet = double.Parse(tbt_sssTax.Text) + double.Parse(tbt_philHealthTax.Text) + double.Parse(tbt_pagibigTax.Text);
                tbt_totalAmount.Text = totalNet.ToString();
            }
        }

        private void tbt_pagibigTax_Leave(object sender, EventArgs e)
        {
            if (isFetched == true)
            {
                double totalNet = 0;
                totalNet = double.Parse(tbt_sssTax.Text) + double.Parse(tbt_philHealthTax.Text) + double.Parse(tbt_pagibigTax.Text);
                tbt_totalAmount.Text = totalNet.ToString();
            }
        }
    }
}
