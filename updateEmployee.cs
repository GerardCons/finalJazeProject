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
    public partial class updateEmployee : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        employeeList frmlist;
        public updateEmployee(employeeList frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            frmlist = frm;
        }

        private void updateEmployee_Load(object sender, EventArgs e)
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
   

                if (MessageBox.Show("Are you sure you want to update this data?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("update tbl_employeesInfo set employeeId = @employeeId, firstName = @firstName, lastName = @lastName, position = @position, birthDate = @birthDate, gender = @gender, address = @address" +
                        ", status = @status, contanctNumber = @contanctNumber, emailAddress = @emailAddress, philHealthId = @philHealthId, sssId = @sssId, pagIbigId = @pagIbigId, basicSalary = @basicSalary " +
                        "where id like '" + lblId.Text + "'", cn);

                    cm.Parameters.AddWithValue("@employeeId", tb_EmployeeId.Text);
                    cm.Parameters.AddWithValue("@firstName", tb_firstName.Text);
                    cm.Parameters.AddWithValue("@lastName", tb_lastName.Text);
                    cm.Parameters.AddWithValue("@position", tb_position.Text);
                    cm.Parameters.AddWithValue("@birthDate", tb_birthDate.Text);
                    cm.Parameters.AddWithValue("@gender", tb_gender.Text);
                    cm.Parameters.AddWithValue("@address", tb_address.Text);
                    cm.Parameters.AddWithValue("@status", tb_status.Text);
                    cm.Parameters.AddWithValue("@contanctNumber", tb_contactNumber.Text);
                    cm.Parameters.AddWithValue("@emailAddress", tb_emailAddress.Text);
                    cm.Parameters.AddWithValue("@philHealthId", tb_phiHealth.Text);
                    cm.Parameters.AddWithValue("@sssId", tb_sss.Text);
                    cm.Parameters.AddWithValue("@pagIbigId", tbt_pagIbigId.Text);
                    cm.Parameters.AddWithValue("@basicSalary", tb_basicSalary.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    cn.Open();
                    cm = new SqlCommand("update tbl_employeeWitholding set employeeId = @employeeId, salaryAmount = @salaryAmount, " +
                        "philHealthId = @philHealthId, sssId = @sssId, pagIbigId = @pagIbigId where employeeId like '" + lblId.Text + "'", cn);
                    cm.Parameters.AddWithValue("@employeeId", tb_EmployeeId.Text);
                    cm.Parameters.AddWithValue("@salaryAmount", tb_basicSalary.Text);
                    cm.Parameters.AddWithValue("@philHealthId", tb_phiHealth.Text);
                    cm.Parameters.AddWithValue("@sssId", tb_sss.Text);
                    cm.Parameters.AddWithValue("@pagIbigId", tbt_pagIbigId.Text);
                    cm.ExecuteNonQuery();


                    cn.Close();

                    MessageBox.Show("Employeee Data has been updated successfully");
                    frmlist.LoadData();
                    this.Dispose();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    }

