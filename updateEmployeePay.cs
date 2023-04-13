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
    public partial class updateEmployeePay : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        payrollList frmlist;
        public updateEmployeePay(payrollList frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            frmlist = frm;
        }

        private void updateEmployeePay_Load(object sender, EventArgs e)
        {
            cn.Open();
            cm = new SqlCommand("select * from tbl_employeePayroll where id = @id", cn);
            cm.Parameters.AddWithValue("@id", lblId.Text);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {

            
                tb_payrollId.Text = dr["payroll_Id"].ToString();
                tb_employeeId.Text = dr["employeeId"].ToString();
                tb_firstName.Text = dr["firstName"].ToString();
                tb_lastName.Text = dr["lastName"].ToString();
                tb_position.Text = dr["position"].ToString();
                dtp_from.Value = DateTime.Parse(dr["dateFrom"].ToString());
                dtp_to.Value = DateTime.Parse(dr["dateTo"].ToString());
                tb_dailyRate.Text = dr["dailyRate"].ToString();
                tb_overtimeAmount.Text = dr["overtimeAmount"].ToString();
                tb_undertimeAmount.Text = dr["undertimeAmount"].ToString();
                tb_witholdings.Text = dr["witholdings"].ToString();
                tb_bonus.Text = dr["bonus"].ToString();
                tb_gross.Text = dr["grossPay"].ToString();
                tb_netPay.Text = dr["netPay"].ToString();
            }
            dr.Close();
            cn.Close();

            DateTime dateTo = dtp_to.Value;
            DateTime dateToOneAM = new DateTime(dateTo.Year, dateTo.Month, dateTo.Day, 0, 0, 0);
            DateTime dateFrom = dtp_from.Value;
            DateTime dateFromOneAM = new DateTime(dateFrom.Year, dateFrom.Month, dateFrom.Day, 0, 0, 0);
            Double totalDeductions = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tbl_deductionInfo WHERE  employeeId = @employeeId AND date >= @startDate AND date <= @endDate", cn);
            cm.Parameters.AddWithValue("@startDate", dateFromOneAM);
            cm.Parameters.AddWithValue("@employeeId", tb_employeeId.Text);
            cm.Parameters.AddWithValue("@endDate", dateToOneAM);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {

                totalDeductions = totalDeductions + Double.Parse(dr["amount"].ToString());
            }

            tb_deductions.Text = totalDeductions.ToString();
            dr.Close();
            cn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double totalGross = 0;
            double netPay = 0;

            totalGross = double.Parse(tb_gross.Text);
            netPay = totalGross - Double.Parse(tb_undertimeAmount.Text) - Double.Parse(tb_witholdings.Text) - Double.Parse(tb_deductions.Text) + Double.Parse(tb_bonus.Text) + Double.Parse(tb_overtimeAmount.Text);


                
            tb_gross.Text = totalGross.ToString();
               tb_netPay.Text = netPay.ToString();

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dateFrom = dtp_from.Value;
                DateTime dateTo = dtp_to.Value;

                if (MessageBox.Show("Are you sure you want to update this data?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("update tbl_employeePayroll set employeeId = @employeeId, firstName = @firstName, lastName = @lastName, position = @position, payroll_id = @payroll_id, dateFrom = @dateFrom" +
                        ", dateTo = @dateTo, dailyRate = @dailyRate, overtimeAmount = @overtimeAmount, undertimeAmount = @undertimeAmount, witholdings = @witholdings, bonus = @bonus, grossPay = @grossPay, netPay = @netPay " +
                        "where id like '" + lblId.Text + "'", cn);

                    cm.Parameters.AddWithValue("@employeeId", tb_employeeId.Text);
                    cm.Parameters.AddWithValue("@payroll_id", tb_payrollId.Text);
                    cm.Parameters.AddWithValue("@firstName", tb_firstName.Text);
                    cm.Parameters.AddWithValue("@lastName", tb_lastName.Text);
                    cm.Parameters.AddWithValue("@position", tb_position.Text);
                    cm.Parameters.AddWithValue("@dateFrom", dateFrom);
                    cm.Parameters.AddWithValue("@dateTo", dateTo);
                    cm.Parameters.AddWithValue("@dailyRate", Double.Parse(tb_dailyRate.Text));
                    cm.Parameters.AddWithValue("@overtimeAmount", Double.Parse(tb_overtimeAmount.Text));
                    cm.Parameters.AddWithValue("@undertimeAmount", Double.Parse(tb_undertimeAmount.Text));
                    cm.Parameters.AddWithValue("@witholdings", Double.Parse(tb_witholdings.Text));
                    cm.Parameters.AddWithValue("@bonus", Double.Parse(tb_bonus.Text));
                    cm.Parameters.AddWithValue("@grossPay", Double.Parse(tb_gross.Text));
                    cm.Parameters.AddWithValue("@netPay", Double.Parse(tb_netPay.Text));
                    cm.ExecuteNonQuery();
                    cn.Close();

                    MessageBox.Show("Employee Pay Data has been updated successfully");
                    frmlist.LoadData();
                    this.Dispose();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tb_netPay_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
