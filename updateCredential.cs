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
    public partial class updateCredential : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        credentialList frmlist;
        public updateCredential(credentialList frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            frmlist = frm;
        }

        private void updateCredential_Load(object sender, EventArgs e)
        {

            cn.Open();
            cm = new SqlCommand("select * from tbl_credentials where id = @id", cn);
            cm.Parameters.AddWithValue("@id", lbl_id.Text);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
               tbt_displayName.Text = dr["displayName"].ToString();
               tbt_firstName.Text = dr["firstName"].ToString();
                tbt_lastName.Text = dr["lastName"].ToString();
                tbt_username.Text = dr["username"].ToString();
                tbt_password.Text = dr["password"].ToString();

            }
            dr.Close();
            cn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {


                if (MessageBox.Show("Are you sure you want to update this data?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("update tbl_credentials set username = @username, password = @password, displayName = @displayName, firstName = @firstName, lastName = @lastName" +
                        " where id like '" + lbl_id.Text + "'", cn);

                    cm.Parameters.AddWithValue("@username", tbt_username.Text);
                    cm.Parameters.AddWithValue("@password", tbt_password.Text);
                    cm.Parameters.AddWithValue("@displayName", tbt_displayName.Text);
                    cm.Parameters.AddWithValue("@firstName", tbt_firstName.Text);
                    cm.Parameters.AddWithValue("@lastName", tbt_lastName.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Credentials has been updated successfully");
                    frmlist.LoadData();
                    this.Dispose();

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
