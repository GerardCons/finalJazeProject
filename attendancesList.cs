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
    public partial class attendancesList : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        public attendancesList()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addAttendance frm = new addAttendance(this);
            frm.ShowDialog();
        }
        public void LoadData()
        {

            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("select * from tbl_attendancesInfo order by id asc", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr["id"].ToString(), dr["employeeId"].ToString(), $"{dr["firstName"]} {dr["lastName"]}", dr["position"].ToString(), dr["date"].ToString().Split(" ")[0], dr["timeIn"].ToString(), dr["timeOut"].ToString(), dr["status"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                cn.Open();

                using (cm = new SqlCommand("SELECT * FROM tbl_attendancesInfo WHERE employeeId = @employeeId", cn))
                {
                    cm.Parameters.AddWithValue("@employeeId", tbt_searchId.Text);
                    using (dr = cm.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                // Do something with the data

                                dataGridView1.Rows.Add(dr["employeeId"].ToString(), $"{dr["firstName"]} {dr["lastName"]}", dr["position"].ToString(), dr["date"].ToString().Split(" ")[0], dr["timeIn"].ToString(), dr["timeOut"].ToString(), dr["status"].ToString());

                            }
                        }
                        else
                        {
                            MessageBox.Show("No Employee found for the search ID: " + tbt_searchId.Text, "Search Employee", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            String id = "";
            String employeeId = "";
            String firstName = "";
            String lastName = "";
            String position = "";
            String status = "";
            String comment = "";
            DateTime attendaceDate = DateTime.Now;
            String timeIn = "";
            String timeOut = "";
            if (colName == "edit")
            {



                cn.Open();
                cm = new SqlCommand("select * from tbl_attendancesInfo where id = @id", cn);
                cm.Parameters.AddWithValue("@id", dataGridView1[0, e.RowIndex].Value.ToString());
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
      
                    id = dr["id"].ToString();
                    employeeId = dr["employeeId"].ToString();
                    firstName = dr["firstName"].ToString();
                    lastName = dr["lastName"].ToString();
                    position = dr["position"].ToString();
                    status = dr["status"].ToString();
                    comment = dr["comment"].ToString();
                    attendaceDate = DateTime.Parse(dr["date"].ToString());
                    timeIn = dr["timeIn"].ToString();
                    timeOut = dr["timeOut"].ToString();

                }
                dr.Close();
                cn.Close();


                updateAttendance frm = new updateAttendance(this);
                frm.lblId.Text = id;
                frm.tbt_employeeId.Text = employeeId;
                frm.tbt_firstName.Text = firstName;
                frm.tbt_lastName.Text = lastName;
                frm.tbt_position.Text = position;
                frm.dtp_date.Value = attendaceDate;
                frm.lbl_timeIn.Text = timeIn;
                frm.lbl_timeOut.Text = timeOut;
                frm.cb_status.Text = status;
                frm.rtbt_comment.Text = comment;
                frm.ShowDialog();
            }
            else if (colName == "delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cn.Open();
                    cm = new SqlCommand("delete from tbl_attendancesInfo where id = @id", cn);
                    cm.Parameters.AddWithValue("@id", dataGridView1[0, e.RowIndex].Value.ToString());
                    cm.ExecuteNonQuery();
                    cn.Close();
                    LoadData();
                    MessageBox.Show("Record was successfully deleted");
                }
            }
        }

        private void attendancesList_Load(object sender, EventArgs e)
        {

        }
    }
}
