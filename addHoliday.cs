using System;
using System.Collections;
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
    public partial class addHoliday : Form

    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        leaveList  frmlist;
        public addHoliday(leaveList frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            frmlist = frm;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void addHoliday_Load(object sender, EventArgs e)
        {
        
        }

        private void button4_Click(object sender, EventArgs e)
        {
        

            try
            {
                DateTime date = dtp_date.Value.Date;

                if (MessageBox.Show("Are you sure you want to save this holiday?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cn.Open();
                    cm = new SqlCommand("select * from tbl_employeesInfo order by id asc", cn);

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "employees");

                    foreach (DataRow dr in ds.Tables["employees"].Rows)
                    {
                        string employeeId = dr["employeeId"].ToString();
                        string firstName = dr["firstName"].ToString();
                        string lastName = dr["lastName"].ToString();
                        string position = dr["position"].ToString();

                        cm = new SqlCommand("INSERT INTO tbl_employeeLeaveInfo(employeeId, firstName, lastName, position, dateTo, dateFrom, leaveType, status, comment)" +
                           "VALUES(@employeeId, @firstName, @lastName, @position, @dateTo, @dateFrom, @leaveType, @status, @comment)", cn);

                        cm.Parameters.AddWithValue("@employeeId", employeeId);
                        cm.Parameters.AddWithValue("@firstName", firstName);
                        cm.Parameters.AddWithValue("@lastName", lastName);
                        cm.Parameters.AddWithValue("@position", position);
                        cm.Parameters.AddWithValue("@dateTo", dtp_date.Value);
                        cm.Parameters.AddWithValue("@dateFrom", dtp_date.Value);
                        cm.Parameters.AddWithValue("@leaveType", "Holiday");
                        cm.Parameters.AddWithValue("@status", "Approved");
                        cm.Parameters.AddWithValue("@comment", tb_holiday.Text);
                        cm.ExecuteNonQuery();

                   
                        cm = new SqlCommand("INSERT INTO tbl_attendancesInfo(employeeId, firstName, lastName, position, date, timeIn, timeOut, status, comment, overTime, underTime)" +
                    "VALUES(@employeeId, @firstName, @lastName, @position, @date, @timeIn, @timeOut, @status, @comment, @overTime, @underTime)", cn);
                        cm.Parameters.AddWithValue("@employeeId", employeeId);
                        cm.Parameters.AddWithValue("@firstName", firstName);
                        cm.Parameters.AddWithValue("@lastName", lastName);
                        cm.Parameters.AddWithValue("@position", position);
                        cm.Parameters.AddWithValue("@date", dtp_date.Value);
                        cm.Parameters.AddWithValue("@timeIn", "not applicable");
                        cm.Parameters.AddWithValue("@timeOut", "not applicable");
                        cm.Parameters.AddWithValue("@status", "Holiday");
                        cm.Parameters.AddWithValue("@comment", tb_holiday.Text);
                        cm.Parameters.AddWithValue("@overTime", 0);
                        cm.Parameters.AddWithValue("@underTime", 0);
                        cm.ExecuteNonQuery();

                    }

                    cn.Close();

             

                    frmlist.LoadData();
                    MessageBox.Show("Record has been successfully saved");
                    
                    Close();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
