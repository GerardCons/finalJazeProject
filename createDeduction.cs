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
using static CTHardware_EmployeeManagement.createDeduction;

namespace CTHardware_EmployeeManagement
{
    public partial class createDeduction : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        payrollList frmlist;

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

            public EmployeePayroll(int id, string payrollId, string employeeId, string firstName, string lastName, string position, DateTime dateFrom, DateTime dateTo, float dailyRate, float undertimeAmount,float overtimeAmount, float deductions, float witholdings, float bonus, float grossPay, float netPay)
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

        public createDeduction(payrollList flist)
        {
            cn = new SqlConnection(dbcon.MyConnection());
            InitializeComponent();
            frmlist = flist;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            float value = float.Parse(tb_amount.Text);
            double roundedValue = Math.Round(value, 2);

            List<EmployeePayroll> employeePayrolls = new List<EmployeePayroll>();
            try
            {
                DateTime date = dtp_date.Value;

                if (MessageBox.Show("Are you sure you want to save this deduction?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //working
                  
                        cn.Open();
                        cm = new SqlCommand("INSERT INTO tbl_deductionInfo(employeeId,deductionId, firstName, lastName, position, date, amount, reason)" +
                            "VALUES(@employeeId, @deductionId, @firstName, @lastName, @position, @date, @amount, @reason)", cn);
                        cm.Parameters.AddWithValue("@employeeId", tb_employeeId.Text);
                        cm.Parameters.AddWithValue("@firstName", tb_firstName.Text);
                        cm.Parameters.AddWithValue("@lastName", tb_lastName.Text);
                        cm.Parameters.AddWithValue("@position", tb_position.Text);
                        cm.Parameters.AddWithValue("@date", date);
                        cm.Parameters.AddWithValue("@amount", roundedValue);
                        cm.Parameters.AddWithValue("@reason", rtb_reason.Text);
                        cm.Parameters.AddWithValue("@deductionId", tb_deductionId.Text);
                        cm.ExecuteNonQuery();
                        MessageBox.Show("Record has been successfully saved");

                    cn.Close();


                    
                    try
                    {
                       
                        cn.Open();
                        cm = new SqlCommand("SELECT * FROM tbl_employeePayroll where employeeId like '" + tb_employeeId.Text + "'", cn);
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
                        else
                        {
                            MessageBox.Show("No rows found in the result.");
                        }
                     
                        
                        dr.Close();
                        cn.Close();


                        EmployeePayroll[] employeePayrollsArray = employeePayrolls.ToArray();

                        int id = 0;
                        float deductions = 0;
                        float grossPay  = 0;
                        float netPay = 0;
                        float withholdings = 0;
                        float overtimePay = 0;
                        float undertimePay = 0;
                        float bonus = 0;
                        foreach (EmployeePayroll record in employeePayrollsArray)
                        {
                            // Add your code here to process each record
                            // Example: Print the fields of each record
                            if(date >= record.DateFrom && date <= record.DateTo)
                            {
                                id = record.Id;
                                deductions = record.Deductions;
                                grossPay = record.GrossPay;
                                netPay = record.NetPay;
                                withholdings = record.Witholdings;
                                overtimePay = record.OvertimeAmount;
                                undertimePay = record.UndertimeAmount;
                                bonus = record.Bonus;
                              
                                break;
                            }
                        }

                        deductions = deductions + float.Parse(roundedValue.ToString());
                        netPay = netPay - (deductions);
                


                        if (id > 0)
                        {

                            cn.Open();
                            cm = new SqlCommand("update tbl_employeePayroll set deductions = @deductions, grossPay = @grossPay, netPay = @netPay where id like '" + id + "'", cn);
                            cm.Parameters.AddWithValue("@deductions", deductions);
                            cm.Parameters.AddWithValue("@grossPay", grossPay);
                            cm.Parameters.AddWithValue("@netPay", netPay);
                            cm.ExecuteNonQuery();
                            cn.Close();
                            this.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("SQL did not save");
                        }
                        frmlist.LoadData();
                          Close();
                        Clear();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        

    }

        private void createDeduction_Load(object sender, EventArgs e)
        {
            dtp_date.Value = DateTime.Now;
            int i = 0;
            cn.Open();
            cm = new SqlCommand("select * from tbl_deductionInfo order by id asc", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
            }
            i++;
            dr.Close();
            cn.Close();
            tb_deductionId.Text = i.ToString();



        }
        private void Clear()
        {
            tb_employeeId.Clear();
            tb_firstName.Clear();
            tb_lastName.Clear();
            tb_position.Clear();
            tb_amount.Clear();
            tb_deductionId.Clear();
            rtb_reason.Clear();
            dtp_date.Value = DateTime.Now;


        }

        public void LoadData()
        {

            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("select * from tbl_deductionInfo  WHERE employeeId = @employeeId", cn);
            cm.Parameters.AddWithValue("@employeeId", tb_employeeId.Text);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr["deductionId"].ToString(),dr["amount"].ToString(), $"{dr["date"].ToString().Split(" ")[0]}", dr["reason"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                cn.Open();

                using (cm = new SqlCommand("SELECT * FROM tbl_employeesInfo WHERE employeeId = @employeeId", cn))
                {
                    cm.Parameters.AddWithValue("@employeeId", tb_employeeId.Text);
                    using (dr = cm.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                // Do something with the data

                                tb_firstName.Text = dr["firstName"].ToString();
                                tb_lastName.Text = dr["lastName"].ToString();
                                tb_position.Text = dr["position"].ToString();

                            }
                        }
                        else
                        {
                            MessageBox.Show("No Employee found for the search ID: " + tb_employeeId.Text, "Search Employee", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void tb_deductionId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
