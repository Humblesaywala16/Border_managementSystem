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

namespace Border_managementSystem
{
    public partial class userLogin : Form
    {
        public userLogin()
        {
            InitializeComponent();
           // populateUser();
        }

        String connectionString = "Data Source=DESKTOP-6L3FHIT;Initial Catalog=borderMgtSystem_db;Integrated Security=True";
        private void button1_Click(object sender, EventArgs e)
        {
            People pp = new People();
            this.Hide();
            pp.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            User pp = new User();
            this.Hide();
            pp.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserDashboard pp = new UserDashboard();
            this.Hide();
            pp.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dashboard pp = new Dashboard();
            this.Hide();
            pp.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Luggage pp = new Luggage();
            this.Hide();
            pp.Show ();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Payment pp = new Payment();
            this.Hide();
            pp.Show ();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            usersLogin();
        }

    
        void usersLogin()
        {
            try
            {
                // Check if any required field is empty
                if (string.IsNullOrWhiteSpace(txtPassword.Text) ||
                    string.IsNullOrWhiteSpace(txtUsername.Text) ||
                    cbxUsertype.SelectedItem == null)
                {
                    MessageBox.Show("All Fields Are Required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string selectQuery = "SELECT username, password, user_type FROM userLogin_tbl WHERE username = @username AND password = @password AND user_type = @user_type";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                        cmd.Parameters.AddWithValue("@user_type", cbxUsertype.SelectedItem.ToString());

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count >= 1)
                        {
                            MessageBox.Show("Login Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            if (cbxUsertype.Text == "admin")
                            {
                                Dashboard db = new Dashboard();
                                db.Show();
                            }
                            else if (cbxUsertype.Text == "staff")
                            {
                                UserDashboard ud = new UserDashboard();
                                this.Hide();
                                ud.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid User Credentials", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void populateUser()
        {
            try
            {
                
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string selectQuery = "SELECT user_type FROM userLogin_tbl";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            cbxUsertype.Items.Add(reader["user_type"].ToString());
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
