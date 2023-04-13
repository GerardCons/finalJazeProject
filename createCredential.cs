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
    public partial class createCredential : Form
    {
        credentialList frmlist;
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader drr;

        public createCredential(credentialList flist)
        {
            InitializeComponent();
            frmlist= flist;
            cn = new SqlConnection(dbcon.MyConnection());
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void createCredential_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this information?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO tbl_credentials(username, password, displayName, firstName, lastName)" +
                        "VALUES(@username, @password, @displayName, @firstName, @lastName)", cn);
                    cm.Parameters.AddWithValue("@username", tbt_username.Text);
                    cm.Parameters.AddWithValue("@password", tbt_password.Text);
                    cm.Parameters.AddWithValue("@displayName", tbt_displayName.Text);
                    cm.Parameters.AddWithValue("@firstName", tbt_firstName.Text);
                    cm.Parameters.AddWithValue("@lastName", tbt_lastName.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been successfully saved");
                    frmlist.LoadData();
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
