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
using System.Xml.Linq;

namespace CTHardware_EmployeeManagement
{

    public partial class createEmployee : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader drr;
        employeeList frmlist;
        public createEmployee(employeeList flist)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            frmlist= flist;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void createEmployee_Load(object sender, EventArgs e)
        {
            int i = 0;
            cn.Open();
            cm = new SqlCommand("select * from tbl_employeesInfo order by id asc", cn);
            drr = cm.ExecuteReader();
            while (drr.Read())
            {
                i++;
            }
            i++;
            drr.Close();
            cn.Close();
            tbt_employeeId.Text = i.ToString();
        }
        private void Clear()
        {
            tbt_employeeId.Clear();
            tbt_firstName.Clear();
            tbt_lastName.Clear();
            tbt_address.Clear();
            tbt_contactNum.Clear();
            tbt_email.Clear();
            tbt_philHealth.Clear();
            tbt_sss.Clear();
            tbt_salary.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double philHealthTax = 0;
            double sssTax = 0;
            double pagIbigTax = 0;
            double salaryValue = float.Parse(tbt_salary.Text);
            double totalTax = sssTax + pagIbigTax + philHealthTax;
            try
            {
                if (MessageBox.Show("Are you sure you want to save this information?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO tbl_employeesInfo(employeeId, firstName, lastName, position, birthDate, gender, address, status, contanctNumber, emailAddress, philHealthId, sssId, pagIbigId, basicSalary)" +
                        "VALUES(@employeeId, @firstName, @lastName, @position, @birthDate, @gender, @address, @status, @contanctNumber, @emailAddress, @philHealthId, @sssId, @pagIbigId, @basicSalary)", cn);
                    cm.Parameters.AddWithValue("@employeeId", tbt_employeeId.Text);
                    cm.Parameters.AddWithValue("@firstName", tbt_firstName.Text);
                    cm.Parameters.AddWithValue("@lastName", tbt_lastName.Text);
                    cm.Parameters.AddWithValue("@position", tbt_position.Text);
                    cm.Parameters.AddWithValue("@birthDate", tbt_birthDate.Text);
                    cm.Parameters.AddWithValue("@gender", tbt_gender.Text);
                    cm.Parameters.AddWithValue("@address", tbt_address.Text);
                    cm.Parameters.AddWithValue("@status", tbt_status.Text);
                    cm.Parameters.AddWithValue("@contanctNumber", tbt_contactNum.Text);
                    cm.Parameters.AddWithValue("@emailAddress", tbt_email.Text);
                    cm.Parameters.AddWithValue("@philHealthId", tbt_philHealth.Text);
                    cm.Parameters.AddWithValue("@sssId", tbt_sss.Text);
                    cm.Parameters.AddWithValue("@pagIbigId", tbt_pagIbigId.Text);
                    cm.Parameters.AddWithValue("@basicSalary", tbt_salary.Text);
                    cm.ExecuteNonQuery();

                    cm = new SqlCommand("INSERT INTO tbl_employeeWitholding(employeeId, philHealthId, sssId, pagIbigId, philHealthAmount, sssAmount, pagIbigAmount, salaryAmount, totalTax)" +
                    "VALUES(@employeeId, @philHealthId, @sssId, @pagIbigId, @philHealthAmount, @sssAmount, @pagIbigAmount, @salaryAmount, @totalTax)", cn);
                    cm.Parameters.AddWithValue("@employeeId", tbt_employeeId.Text);
                    cm.Parameters.AddWithValue("@philHealthId", tbt_philHealth.Text);
                    cm.Parameters.AddWithValue("@sssId", tbt_sss.Text);
                    cm.Parameters.AddWithValue("@pagIbigId", tbt_pagIbigId.Text);
                    cm.Parameters.AddWithValue("@philHealthAmount", philHealthTax);
                    cm.Parameters.AddWithValue("@sssAmount", sssTax);
                    cm.Parameters.AddWithValue("@pagIbigAmount", pagIbigTax);
                    cm.Parameters.AddWithValue("@salaryAmount", salaryValue);
                    cm.Parameters.AddWithValue("@totalTax", totalTax);
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

        private void tbt_position_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
