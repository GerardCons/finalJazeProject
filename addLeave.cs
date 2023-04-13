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
    public partial class addLeave : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        leaveList frmlist;
        public addLeave(leaveList flist)
        {
            cn = new SqlConnection(dbcon.MyConnection());
            InitializeComponent();
            frmlist= flist;

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void addLeave_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
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

        private void Clear()
        {
            tb_employeeId.Clear();
            tb_firstName.Clear();
            tb_lastName.Clear();
            tb_position.Clear();
            tbt_leaveType.Clear();
            dtp_from.Value = DateTime.Now;
            dtp_to.Value = DateTime.Now;
            rtb_comment.Clear();


        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime now = DateTime.Now;
                DateTime dateTo = dtp_to.Value.Date;
                DateTime dateFrom = dtp_from.Value;
                TimeSpan difference = dateTo - dateFrom;
               
                int totalDifference = difference.Days + 2;
                if (MessageBox.Show("Are you sure you want to save this leave?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if(difference.Days  + 1 > 0)
                    {
                        cn.Open();
                        cm = new SqlCommand("INSERT INTO tbl_employeeLeaveInfo(employeeId, firstName, lastName, position, dateTo, dateFrom, leaveType, status, comment)" +
                            "VALUES(@employeeId, @firstName, @lastName, @position, @dateTo, @dateFrom, @leaveType, @status, @comment)", cn);
                        cm.Parameters.AddWithValue("@employeeId", tb_employeeId.Text);
                        cm.Parameters.AddWithValue("@firstName", tb_firstName.Text);
                        cm.Parameters.AddWithValue("@lastName", tb_lastName.Text);
                        cm.Parameters.AddWithValue("@position", tb_position.Text);
                        cm.Parameters.AddWithValue("@dateTo", dateTo);
                        cm.Parameters.AddWithValue("@dateFrom", dateFrom);
                        cm.Parameters.AddWithValue("@leaveType", tbt_leaveType.Text);
                        cm.Parameters.AddWithValue("@status", "Pending");
                        cm.Parameters.AddWithValue("@comment", rtb_comment.Text);
                        cm.ExecuteNonQuery();
                     
                        cn.Close();
                        MessageBox.Show("Record has been successfully saved");
                        Clear();
                        frmlist.LoadData();
                        Close();
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
}
