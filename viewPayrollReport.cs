using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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
        payrollList frm;
        public viewPayrollReport(payrollList frmlist)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            LoadData();
            frm = frmlist;
        }
        public void LoadData()
        {

            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("select * from tbl_employeePayroll order by id asc", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                total = total + double.Parse(dr["netPay"].ToString());
                dataGridView1.Rows.Add(dr["id"].ToString(), $"{dr["firstName"]} {dr["lastName"]}", dr["grossPay"].ToString(), dr["dailyRate"].ToString(), dr["deductions"].ToString(),  dr["netPay"].ToString());
            }
            lbl_total.Text = total.ToString();
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
