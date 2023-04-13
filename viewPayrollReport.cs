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
    public partial class viewPayrollReport : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        double total = 0;
        public viewPayrollReport()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            LoadData();
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

           


        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void viewPayrollReport_Load(object sender, EventArgs e)
        {

        }
    }
}
