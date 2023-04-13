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
    public partial class payrollList : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        private bool isSearched = false;
        public payrollList()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            LoadData();
        }

        private void payrollList_Load(object sender, EventArgs e)
        {
   
        }

        private void button2_Click(object sender, EventArgs e)
        {
      
        }

        private void button1_Click(object sender, EventArgs e)
        {
            createDeduction frm = new createDeduction(this);
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            searchWitholdings frm = new searchWitholdings(this);
            frm.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            String id = "";
            String employeeId = "";
            String firstName = "";
            String lastName = "";
            String position = "";
            String payId = "";
            String payroll_Id = "";
            DateTime dateFrom = DateTime.Now;
            DateTime dateTo = DateTime.Now;
            Double dailyRate = 0;
            Double overtimeAmount = 0;
            Double undertimeAmount = 0;
            Double deductions = 0;
            Double witholdings = 0;
            Double bonus = 0;
            Double grossPay = 0;
            Double netPay = 0;
            if (colName == "edit")
            {

                updateEmployeePay frm = new updateEmployeePay(this);
                frm.lblId.Text = dataGridView1[0, e.RowIndex].Value.ToString();
                frm.ShowDialog();


            }
            else if (colName == "print")
            {

                double philHealthAmount = 0;
                double sssAmount = 0;
                double pagIbigAmount = 0;

                cn.Open();
                cm = new SqlCommand("select * from tbl_employeePayroll where id = @id", cn);
                cm.Parameters.AddWithValue("@id", dataGridView1[0, e.RowIndex].Value.ToString());
                dr = cm.ExecuteReader();
                while (dr.Read())
                {

                    id = dr["id"].ToString();
                    payroll_Id = dr["payroll_Id"].ToString();
                    employeeId = dr["employeeId"].ToString();
                    firstName = dr["firstName"].ToString();
                    lastName = dr["lastName"].ToString();
                    position = dr["position"].ToString();
                    dateFrom = DateTime.Parse(dr["dateFrom"].ToString());
                    dateTo = DateTime.Parse(dr["dateTo"].ToString());
                    dailyRate = Double.Parse(dr["dailyRate"].ToString());
                    overtimeAmount = Double.Parse(dr["overtimeAmount"].ToString());
                    undertimeAmount = Double.Parse(dr["undertimeAmount"].ToString());
                    deductions = Double.Parse(dr["deductions"].ToString());
                    witholdings = Double.Parse(dr["witholdings"].ToString());
                    bonus = Double.Parse(dr["bonus"].ToString());
                    grossPay = Double.Parse(dr["grossPay"].ToString());
                    netPay = Double.Parse(dr["netPay"].ToString());
                }
                dr.Close();
                cn.Close();


                cn.Open();
                cm = new SqlCommand("select * from tbl_employeeWitholding where employeeId = @employeeId", cn);
                cm.Parameters.AddWithValue("@employeeId", employeeId);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {


                    philHealthAmount = Double.Parse(dr["philHealthAmount"].ToString());
                    sssAmount = Double.Parse(dr["sssAmount"].ToString());
                    pagIbigAmount = Double.Parse(dr["pagIbigAmount"].ToString());
                }
                dr.Close();
                cn.Close();

                payslip frm = new payslip(this);


                frm.lbl_employeeId.Text = employeeId;
                frm.lbl_employeeName.Text = firstName + " " + lastName;
                DateTime dateFromDate = dateFrom.Date;
                DateTime dateToDate = dateFrom.Date;
                string formattedDateFrom = dateFromDate.ToString("yyyy-MM-dd");
                string formattedDateTo = dateToDate.ToString("yyyy-MM-dd");
                frm.lbl_period.Text = formattedDateFrom + " - " + formattedDateTo;
                frm.lbl_basicPay.Text = dailyRate.ToString();
                frm.lbl_deduction.Text = deductions.ToString();
                frm.lbl_grossPay.Text = grossPay.ToString();
                frm.lbl_netPay.Text = netPay.ToString();
                frm.lbl_overtime.Text = overtimeAmount.ToString();
                frm.lbl_undertime.Text = undertimeAmount.ToString();
                frm.lbl_bonus.Text = bonus.ToString();
                frm.lbl_otPay.Text = overtimeAmount.ToString();
                frm.lbl_position.Text = position;
                frm.lbl_totalWith.Text = witholdings.ToString();
                frm.lbl_pagibigAmount.Text = pagIbigAmount.ToString();
                frm.lbl_philhealthAmount.Text = philHealthAmount.ToString();
                frm.lbl_sssAmount.Text = sssAmount.ToString();
                frm.lbl_netPay.Text = netPay.ToString();

                frm.ShowDialog();


            }
            else if (colName == "delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cn.Open();
                    cm = new SqlCommand("delete from tbl_employeePayroll where id = @id", cn);
                    cm.Parameters.AddWithValue("@id", dataGridView1[0, e.RowIndex].Value.ToString());
                    cm.ExecuteNonQuery();
                    cn.Close();
                    LoadData();
                    MessageBox.Show("Record was successfully deleted");
                }
            }
        }
        public void LoadData()
        {

            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("select * from tbl_employeePayroll order by id asc", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr["id"].ToString(), dr["payroll_Id"].ToString(), $"{dr["firstName"]} {dr["lastName"]}", dr["position"].ToString(), $"{dr["dateFrom"].ToString().Split(" ")[0]} - {dr["dateTo"].ToString().Split(" ")[0]}", dr["deductions"].ToString(), dr["undertimeAmount"].ToString(), dr["overtimeAmount"].ToString(), dr["grossPay"].ToString(), dr["netPay"].ToString());
            }
            dr.Close();
            cn.Close();

            cn.Open();
            cm = new SqlCommand("select * from tbl_payroll order by id asc", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                string value = dr["payroll_Id"].ToString();
                cb_payId.Items.Add(value);
            }
            dr.Close();
            cn.Close();


        }
        private void button5_Click(object sender, EventArgs e)
        {
            createPayroll frm = new createPayroll(this);
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (isSearched)
            {
                dataGridView1.Rows.Clear();
                cn.Open();
                cm = new SqlCommand("select * from tbl_employeePayroll order by id asc", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr["id"].ToString(), dr["payroll_Id"].ToString(), $"{dr["firstName"]} {dr["lastName"]}", dr["position"].ToString(), $"{dr["dateFrom"].ToString().Split(" ")[0]} - {dr["dateTo"].ToString().Split(" ")[0]}", dr["deductions"].ToString(), dr["undertimeAmount"].ToString(), dr["overtimeAmount"].ToString(), dr["grossPay"].ToString(), dr["netPay"].ToString());
                }
                dr.Close();
                cn.Close();

                button4.Text = "Search";
                isSearched = false;
            }
            else
            {
                try
                {
                    dataGridView1.Rows.Clear();
                    cn.Open();

                    using (cm = new SqlCommand("SELECT * FROM tbl_employeePayroll WHERE payroll_Id = @payroll_Id", cn))
                    {
                        cm.Parameters.AddWithValue("@payroll_Id", cb_payId.Text);
                        using (dr = cm.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    // Do something with the data
                                    tbt_from.Text = dr["dateFrom"].ToString().Split(" ")[0];
                                    tbt_to.Text = dr["dateTo"].ToString().Split(" ")[0];

                                    dataGridView1.Rows.Add(dr["id"].ToString(), dr["payroll_Id"].ToString(), $"{dr["firstName"]} {dr["lastName"]}", dr["position"].ToString(), $"{dr["dateFrom"].ToString().Split(" ")[0]} - {dr["dateTo"].ToString().Split(" ")[0]}", dr["deductions"].ToString(), dr["undertimeAmount"].ToString(), dr["overtimeAmount"].ToString(), dr["grossPay"].ToString(), dr["netPay"].ToString());

                                }
                            }
                            else
                            {
                                MessageBox.Show("No Employee found for the payroll id: " + cb_payId.Text, "Search Employee", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    button4.Text = "Clear";
                    isSearched = true;
                }

             
            }
         
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
