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
    public partial class leaveList : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();

        public leaveList()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addLeave frm = new addLeave(this);
            frm.ShowDialog();
        }

        public void LoadData()
        {

            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("select * from tbl_employeeLeaveInfo order by id asc", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
           
                dataGridView1.Rows.Add(dr["id"].ToString(), dr["employeeId"].ToString(), $"{dr["firstName"]} {dr["lastName"]}", dr["leaveType"].ToString(), $"{dr["dateFrom"].ToString().Split(" ")[0]}-{dr["dateTo"].ToString().Split(" ")[0]}", dr["status"].ToString(), dr["comment"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            DateTime dateTo = DateTime.Now;
            DateTime dateFrom = DateTime.Now;
            int totalDifference = 0;
            String employeeId = "";
            String firstName = "";
            String lastName = "";
            String position = "";
            String comment = "";
            String id = "";
            String status = "";
            String leaveType = "";
            if (colName == "approve")
            {
      

                cn.Open();
                cm = new SqlCommand("select * from tbl_employeeLeaveInfo where id = @id", cn);
                cm.Parameters.AddWithValue("@id", dataGridView1[0, e.RowIndex].Value.ToString());
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                     dateTo = DateTime.Parse(dr["dateTo"].ToString());
                     dateFrom = DateTime.Parse(dr["dateFrom"].ToString());
                     TimeSpan difference = dateTo - dateFrom;
                     totalDifference = difference.Days + 2;
                    id= dr["id"].ToString();
                    employeeId = dr["employeeId"].ToString();
                        firstName = dr["firstName"].ToString();
                    lastName = dr["lastName"].ToString();
                    position = dr["position"].ToString();
                    comment = dr["comment"].ToString();
                    status = dr["status"].ToString();

                }
                dr.Close();
                cn.Close();

              

                if(status == "Approve")
                {
                    MessageBox.Show("The Leave Request has already been approved!!");
                }
                else
                {
                    cn.Open();
                    cm = new SqlCommand("UPDATE  tbl_employeeLeaveInfo set status = @newvalue where id = @id", cn);
                    cm.Parameters.AddWithValue("@id", dataGridView1[0, e.RowIndex].Value.ToString());
                    cm.Parameters.AddWithValue("@newvalue", "Approved");
                    cm.ExecuteNonQuery();
                    cn.Close();


                    cn.Open();
                    for (int i = 0; i < totalDifference; i++)
                    {
                        cm = new SqlCommand("INSERT INTO tbl_attendancesInfo(employeeId, firstName, lastName, position, date, timeIn, timeOut, status, comment, overTime, underTime)" +
                    "VALUES(@employeeId, @firstName, @lastName, @position, @date, @timeIn, @timeOut, @status, @comment, @overTime, @underTime)", cn);
                        cm.Parameters.AddWithValue("@employeeId", employeeId);
                        cm.Parameters.AddWithValue("@firstName", firstName);
                        cm.Parameters.AddWithValue("@lastName", lastName);
                        cm.Parameters.AddWithValue("@position", position);
                        cm.Parameters.AddWithValue("@date", dateFrom.AddDays(i).Date);
                        cm.Parameters.AddWithValue("@timeIn", "not applicable");
                        cm.Parameters.AddWithValue("@timeOut", "not applicable");
                        cm.Parameters.AddWithValue("@status", "Leave");
                        cm.Parameters.AddWithValue("@comment", comment);
                        cm.Parameters.AddWithValue("@overTime", 0);
                        cm.Parameters.AddWithValue("@underTime", 0);
                        cm.ExecuteNonQuery();
                    }
                    MessageBox.Show("Record has been successfully updated");
                    cn.Close();
                    LoadData();

                }

               
            }
            else if (colName == "edit")
            {
                cn.Open();
                cm = new SqlCommand("select * from tbl_employeeLeaveInfo where id = @id", cn);
                cm.Parameters.AddWithValue("@id", dataGridView1[0, e.RowIndex].Value.ToString());
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    dateTo = DateTime.Parse(dr["dateTo"].ToString());
                    dateFrom = DateTime.Parse(dr["dateFrom"].ToString());
                    TimeSpan difference = dateTo - dateFrom;
                    totalDifference = difference.Days + 2;
                    id = dr["id"].ToString();
                    employeeId = dr["employeeId"].ToString();
                    firstName = dr["firstName"].ToString();
                    lastName = dr["lastName"].ToString();
                    position = dr["position"].ToString();
                    comment = dr["comment"].ToString();
                    leaveType = dr["leaveType"].ToString();

                }
                dr.Close();
                cn.Close();

                updateLeave frm = new updateLeave(this);
                frm.lblId.Text = id;
                frm.tb_employeeId.Text = employeeId;
                frm.tb_firstName.Text = firstName;
                frm.tb_lastName.Text = lastName;
                frm.tb_position .Text = position;
                frm.tbt_leaveType.Text = leaveType;
                frm.rtb_comment.Text = comment;
                frm.dtp_to.Value = dateTo;
                frm.dtp_from.Value = dateFrom;
                frm.ShowDialog();

            }
            else if (colName == "delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cn.Open();
                    cm = new SqlCommand("delete from tbl_employeeLeaveInfo where id = @id", cn);
                    cm.Parameters.AddWithValue("@id", dataGridView1[0, e.RowIndex].Value.ToString());
                    cm.ExecuteNonQuery();
                    cn.Close();
                    LoadData();
                    MessageBox.Show("Record was successfully deleted");
                }
            }
        }

        private void leaveList_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                cn.Open();

                using (cm = new SqlCommand("SELECT * FROM tbl_employeeLeaveInfo WHERE employeeId = @employeeId", cn))
                {
                    cm.Parameters.AddWithValue("@employeeId", tbt_searchEmployee.Text);
                    using (dr = cm.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                // Do something with the data

                                dataGridView1.Rows.Add(dr["employeeId"].ToString(), $"{dr["firstName"]} {dr["lastName"]}", dr["leaveType"].ToString(), $"{dr["dateFrom"].ToString().Split(" ")[0]}-{dr["dateTo"].ToString().Split(" ")[0]}", dr["status"].ToString(), dr["comment"].ToString());

                            }
                        }
                        else
                        {
                            MessageBox.Show("No Employee found for the search ID: " + tbt_searchEmployee.Text, "Search Employee", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void button3_Click(object sender, EventArgs e)
        {
            addHoliday frm = new addHoliday(this);
            frm.ShowDialog();
        }
    }
}
