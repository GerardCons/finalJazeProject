using System.Data.SqlClient;
namespace CTHardware_EmployeeManagement
{
    public partial class loginScreen : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        public loginScreen()
        {
            cn = new SqlConnection(dbcon.MyConnection());
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void loginScreen_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cn.Open();

                using (cm = new SqlCommand("SELECT * FROM tbl_credentials WHERE username = @username AND password = @password", cn))
                {
                    cm.Parameters.AddWithValue("@username", tb_username.Text);
                    cm.Parameters.AddWithValue("@password", tb_password.Text);
                    using (dr = cm.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                // Do something with the data
                                String displayName = dr["displayName"].ToString();
                                String firstName = dr["firstName"].ToString();
                                String lastName = dr["lastName"].ToString();
                                MainMenu frm = new MainMenu(this);
                                frm.lbl_displayName.Text = displayName;
                                frm.lbl_firstName.Text = firstName;
                                frm.lbl_lastName.Text = lastName;
                                frm.Show();



                            }
                        }
                        else
                        {
                            MessageBox.Show("The username or password is invalid, please try again", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}