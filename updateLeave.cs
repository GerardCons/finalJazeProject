using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CTHardware_EmployeeManagement
{
    public partial class updateLeave : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        leaveList frmlist;
        public updateLeave(leaveList frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            frmlist = frm;
       
        }

        private void updateLeave_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dateTo = dtp_to.Value.Date;
                DateTime dateFrom = dtp_from.Value.Date;

                if (MessageBox.Show("Are you sure you want to update this data?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("update tbl_employeeLeaveInfo set leaveType = @leaveType, comment = @comment, dateTo = @dateTo, dateFrom = @dateFrom where id like '" + lblId.Text + "'", cn);
                    cm.Parameters.AddWithValue("@leaveType", tbt_leaveType.Text);
                    cm.Parameters.AddWithValue("@comment", rtb_comment.Text);
                    cm.Parameters.AddWithValue("@dateTo", dateTo);
                    cm.Parameters.AddWithValue("@dateFrom", dateFrom);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    MessageBox.Show("Leave Data has been updated successfully");
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
