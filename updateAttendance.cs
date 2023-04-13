using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CTHardware_EmployeeManagement
{
    public partial class updateAttendance : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        attendancesList frmlist;
        public updateAttendance(attendancesList frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            frmlist = frm;
        }

        private void updateAttendance_Load(object sender, EventArgs e)
        {
            if (lbl_timeIn.Text == "not applicable")
            {
                dtp_timeIn.Value = DateTime.Now;
                dtp_timeOut.Value = DateTime.Now;
                dtp_timeIn.Format = DateTimePickerFormat.Time;
                dtp_timeOut.Format = DateTimePickerFormat.Time;
                dtp_timeOut.Enabled= false;
                dtp_timeIn.Enabled= false;
                cb_status.Enabled= false;
            }
            else
            {
                dtp_timeIn.Value = DateTime.Parse(lbl_timeIn.Text);
                dtp_timeOut.Value = DateTime.Parse(lbl_timeOut.Text);
                dtp_timeIn.Format = DateTimePickerFormat.Time;
                dtp_timeOut.Format = DateTimePickerFormat.Time;
                dtp_timeOut.Enabled = true;
                dtp_timeIn.Enabled = true;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                String checkStatusTimeIn = "";
                String checkStatusTimeOut = "";
                DateTime attendanceDate = dtp_date.Value.Date;
                DateTime timeOutValue = dtp_timeOut.Value;
                DateTime timeInValue = dtp_timeIn.Value;
                DateTime dateTimeIn = new DateTime(2022, 12, 25, 8, 00, 0);
                DateTime dateTimeOut = new DateTime(2022, 12, 25, 17, 00, 0);
                TimeSpan timeInFixed = dateTimeIn.TimeOfDay;
                TimeSpan timeOutFixed = dateTimeOut.TimeOfDay;
                TimeSpan timeIn = timeInValue.TimeOfDay;
                TimeSpan timeOut = timeOutValue.TimeOfDay;
                TimeSpan timeInDifference = timeInFixed - timeIn;
                TimeSpan timeOutDifference = timeOutFixed - timeOut;
                string[] timeInDifferenceValues = timeInDifference.ToString().Split(':');
                string[] timeOutDifferenceValues = timeOutDifference.ToString().Split(':');
                int timeOutHour = Int32.Parse(timeOutDifferenceValues[0]) * -1;
                int timeOutMins = Int32.Parse(timeOutDifferenceValues[1]);
                int timeInHour = Int32.Parse(timeInDifferenceValues[0]) * -1;
                int timeInMins = Int32.Parse(timeInDifferenceValues[1]);
                int totalUndertime = timeInHour * 60 + timeInMins;
                int totalOvertime = timeOutHour * 60 + timeOutMins;


                if (MessageBox.Show("Are you sure you want to update this data?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("update tbl_attendancesInfo set employeeId = @employeeId, firstName = @firstName, lastName = @lastName, position = @position, date = @date, timeIn = @timeIn, timeOut = @timeOut" +
                        ", status = @status, comment = @comment, underTime = @underTime, overTime = @overTime " +
                        "where id like '" + lblId.Text + "'", cn);

                    cm.Parameters.AddWithValue("@employeeId", tbt_employeeId.Text);
                    cm.Parameters.AddWithValue("@firstName", tbt_firstName.Text);
                    cm.Parameters.AddWithValue("@lastName", tbt_position.Text);
                    cm.Parameters.AddWithValue("@position", tbt_position.Text);
                    cm.Parameters.AddWithValue("@date", attendanceDate);
                    if (lbl_timeIn.Text == "not applicable")
                    {
                        cm.Parameters.AddWithValue("@timeIn", "not applicable");
                    }
                    else
                    {
                        cm.Parameters.AddWithValue("@timeIn", checkStatusTimeIn);
                    }
                    if (lbl_timeOut.Text == "not applicable")
                    {
                        cm.Parameters.AddWithValue("@timeOut", "not applicable");
                    }
                    else
                    {
                        cm.Parameters.AddWithValue("@timeOut", checkStatusTimeOut);
                    }
                    cm.Parameters.AddWithValue("@status", cb_status.Text);
                    cm.Parameters.AddWithValue("@comment", rtbt_comment.Text);
                    if (lbl_timeOut.Text == "not applicable")
                    {
                        cm.Parameters.AddWithValue("@overTime", 0);
                    }
                    else
                    {
                        cm.Parameters.AddWithValue("@overTime", totalOvertime);
                    }
                    if (lbl_timeOut.Text == "not applicable")
                    {
                        cm.Parameters.AddWithValue("@underTime", 0);
                    }
                    else
                    {
                        cm.Parameters.AddWithValue("@underTime", totalUndertime);
                    }
                    cm.ExecuteNonQuery();
                    cn.Close();

                    MessageBox.Show("Attendance Data has been updated successfully");
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
