using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CTHardware_EmployeeManagement
{
    public partial class credentialList : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();

        public credentialList()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            LoadData();
        }

        public void LoadData()
        {

            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("select * from tbl_credentials order by id asc", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr["id"].ToString(), $"{dr["firstName"]} {dr["lastName"]}", dr["username"].ToString(), dr["password"].ToString(), dr["displayName"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void credentialList_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            createCredential frm = new createCredential(this);
            frm.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "edit")
            {
                string id = "";

                cn.Open();
                cm = new SqlCommand("select * from tbl_credentials where id = @id", cn);
                cm.Parameters.AddWithValue("@id", dataGridView1[0, e.RowIndex].Value.ToString());
                dr = cm.ExecuteReader();
                while (dr.Read())
                {

                    id = dr["id"].ToString();

                }
                dr.Close();
                cn.Close();

                updateCredential frm = new updateCredential(this);

                frm.lbl_id.Text = id;
           
                frm.ShowDialog();
            }
            else if (colName == "delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cn.Open();
                    cm = new SqlCommand("delete from tbl_credentials where id = @id", cn);
                    cm.Parameters.AddWithValue("@id", dataGridView1[0, e.RowIndex].Value.ToString());
                    cm.ExecuteNonQuery();
                    cn.Close();
                    LoadData();
                    MessageBox.Show("Record was successfully deleted");
                }
            }
        }
    }
}
