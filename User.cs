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
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
        }

        String connectionString = "Data Source=DESKTOP-6L3FHIT;Initial Catalog=borderMgtSystem_db;Integrated Security=True";


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //Add button click
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkUserExists())
            {
                MessageBox.Show("User with this ID " + txtUserID.Text.Trim() + " already  exists, add new user!");
            }
            else
            {
                addNewUser();
            }
            
        }

        //USER DEFINED METHODS
        void clearControl()
        {
            txtUserID.Text = "";
            txtTelephone.Text = "";
            txtLname.Text = "";
            txtFname.Text = "";
            txtEmail.Text = "";
            cbxGender.SelectedIndex = -1;
        }

        void searchUser()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    MessageBox.Show("Enter user ID and search", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
               

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    //selectQuery
                    string selectQuery = "SELECT * FROM user_tb WHERE user_id = @user_id";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@user_id", txtSearch.Text);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {

                            if (dr.Read())
                            {
                                txtUserID.Text = dr["user_id"].ToString();
                                txtFname.Text = dr["first_name"].ToString();
                                txtLname.Text = dr["lname"].ToString();
                                txtEmail.Text = dr["email"].ToString();
                                cbxGender.Text = dr["gender"].ToString();
                                txtTelephone.Text = dr["telephone"].ToString();

                            }
                            else
                            {
                                MessageBox.Show("Invalid User or User Not Found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtUserID.Text = "";
                                txtFname.Text = "";
                                txtLname.Text = "";
                                txtEmail.Text = "";
                                cbxGender.Text = "";
                                txtTelephone.Text = "";
                            }
                          
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void updateUser()
        {
            if (string.IsNullOrWhiteSpace(txtUserID.Text))
            {
               MessageBox.Show("Please enter user ID you want to delete!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    //updatetQuery
                    String updateQuery = "UPDATE user_tb SET first_name = @first_name, lname = @lname, gender = @gender, email = @email, telephone = @telephone WHERE user_id = @user_id";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@user_id", txtUserID.Text);
                        cmd.Parameters.AddWithValue("@first_name", txtFname.Text);
                        cmd.Parameters.AddWithValue("@lname", txtLname.Text);
                        cmd.Parameters.AddWithValue("@gender", cbxGender.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@telephone", txtTelephone.Text.Trim());
                    

                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    MessageBox.Show("This User With The ID " +txtUserID.Text+" Has Been Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearControl();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!:" + ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void deleteUser()
        {
            try 
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    //deleteQuery
                    string deleteQuery = "DELETE FROM user_tb WHERE user_id = @user_id";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@user_id", txtUserID.Text.Trim());

                        cmd.ExecuteNonQuery();
                        con.Close();

                    }
                    MessageBox.Show("User with this ID " + txtUserID.Text + " has been deleted!");
                    clearControl();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool checkUserExists()
        {
            try 
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    //selectQuery
                    string selectQuery = "SELECT * FROM user_tb WHERE user_id = '" + txtUserID.Text.Trim() + "'";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count >= 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Error:" + ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
           
        }
        void addNewUser()
        {
            if (string.IsNullOrWhiteSpace(txtUserID.Text) || string.IsNullOrWhiteSpace(txtFname.Text) || string.IsNullOrWhiteSpace(txtLname.Text) || cbxGender.SelectedItem == null || string.IsNullOrWhiteSpace(txtTelephone.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please Fill Required Fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    //insertQuery
                    String insertQuery = "INSERT INTO user_tb(user_id, first_name, lname, gender, email, telephone) VALUES(@user_id, @first_name, @lname, @gender, @email, @telephone)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@user_id", txtUserID.Text);
                        cmd.Parameters.AddWithValue("@first_name", txtFname.Text);
                        cmd.Parameters.AddWithValue("@lname", txtLname.Text);
                        cmd.Parameters.AddWithValue("@gender", cbxGender.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@telephone", txtTelephone.Text.Trim());
                      

                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    MessageBox.Show("New user has beed added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearControl();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!:" + ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        //DELETE BUTTON
        private void button3_Click(object sender, EventArgs e)
        {
            if (checkUserExists())
            {
                deleteUser();
                clearControl();
            }
            else
            {
                MessageBox.Show("User does not exist!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                clearControl();
            }
            
        }
        
        //update button
        private void button2_Click(object sender, EventArgs e)
        {
            if (checkUserExists())
            {
                updateUser();
                clearControl();
            }
            else
            {
                MessageBox.Show("User does not exist!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                clearControl();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clearControl();
        }
        // EXIT BUTTON
        private void button7_Click(object sender, EventArgs e)
        {
            //ExitQuery
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    //selectQuery
                   string selectQuery = "SELECT * FROM user_tb";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            searchUser();
        }

        private void backToDashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dashboard db = new Dashboard();
            this.Hide();
            db.Show();
        }
    }
}
