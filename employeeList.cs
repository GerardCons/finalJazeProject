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
    public partial class employeeList : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        public employeeList()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            createEmployee frm = new createEmployee(this);
            frm.ShowDialog();
        }

        public void LoadData()
        {

            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("select * from tbl_employeesInfo order by id asc", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr["id"].ToString(), dr["employeeId"].ToString(), $"{dr["firstName"]} {dr["lastName"]}", dr["position"].ToString(), dr["gender"].ToString(), dr["contanctNumber"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string firstName = "";
            string lastName = "";
            string employeeId = "";
            string position = "";
            string id = "";
            string birthDate = "";
            string gender = "";
            string address = "";
            string status = "";
            string contactNumber = "";
            string emailAddress = "";
            string philHealthId = "";
            string sssId = "";
            string pagibigId = "";
            string basicSalary = "";
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "edit")
            {
                cn.Open();
                cm = new SqlCommand("select * from tbl_employeesInfo where id = @id", cn);
                cm.Parameters.AddWithValue("@id", dataGridView1[0, e.RowIndex].Value.ToString());
                dr = cm.ExecuteReader();
                while (dr.Read())
                {

                    id = dr["id"].ToString();
                    employeeId = dr["employeeId"].ToString();
                    firstName = dr["firstName"].ToString();
                    lastName = dr["lastName"].ToString();
                    position = dr["position"].ToString();
                    birthDate = dr["birthDate"].ToString();
                    gender = dr["gender"].ToString();
                    address = dr["address"].ToString();
                    status = dr["status"].ToString();
                    contactNumber = dr["contanctNumber"].ToString();
                    emailAddress = dr["emailAddress"].ToString();
                    philHealthId = dr["philHealthId"].ToString();
                    sssId = dr["sssId"].ToString();
                    pagibigId = dr["pagibigId"].ToString();
                    basicSalary = dr["basicSalary"].ToString();

                }
                dr.Close();
                cn.Close();

                updateEmployee frm = new updateEmployee(this);

                frm.lblId.Text = id;
                frm.tb_EmployeeId.Text = employeeId;
                frm.tb_firstName.Text = firstName;
                frm.tb_lastName.Text = lastName;
                frm.tb_position.Text = position;
                frm.tb_birthDate.Text = birthDate;
                frm.tb_gender.Text = gender;
                frm.tb_address.Text = address;
                frm.tb_status.Text = status;
                frm.tb_contactNumber.Text = contactNumber;
                frm.tb_emailAddress.Text = emailAddress;
                frm.tb_phiHealth.Text = philHealthId;
                frm.tb_sss.Text = sssId;
                frm.tbt_pagIbigId.Text = pagibigId;
                frm.tb_basicSalary.Text = basicSalary;
                frm.ShowDialog();
            } else if(colName == "delete")
            {
                if(MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cn.Open();
                    cm = new SqlCommand("delete from tbl_employeesInfo where id = @id", cn);
                    cm.Parameters.AddWithValue("@id", dataGridView1[0, e.RowIndex].Value.ToString());
                    cm.ExecuteNonQuery();   
                    cn.Close();
                    LoadData();
                    MessageBox.Show("Record was successfully deleted");
                }
            }
        }

        private void employeeList_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear ();
                cn.Open();

                using (cm = new SqlCommand("SELECT * FROM tbl_employeesInfo WHERE employeeId = @employeeId", cn))
                {
                    cm.Parameters.AddWithValue("@employeeId", tbt_search.Text);
                    using (dr = cm.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                // Do something with the data

                                dataGridView1.Rows.Add(dr["employeeId"].ToString(), $"{dr["firstName"]} {dr["lastName"]}", dr["position"].ToString(), dr["gender"].ToString(), dr["contanctNumber"].ToString());

                            }
                        }
                        else
                        {
                            MessageBox.Show("No Employee found for the search ID: " + tbt_search.Text, "Search Employee", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void tbt_search_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
