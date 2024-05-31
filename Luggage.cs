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
    public partial class Luggage : Form
    {
        public Luggage()
        {
            InitializeComponent();
        }

        String connectionString = "Data Source=DESKTOP-6L3FHIT;Initial Catalog=borderMgtSystem_db;Integrated Security=True";
        // EXIT BUTTON
        private void button7_Click(object sender, EventArgs e)
        {
            //ExitQuery
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            try 
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    //selectQuery
                    string selectQuery = "SELECT person_ID, first_name, last_name, pfrom, destination FROM People_tbl WHERE person_ID = @person_ID";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@person_ID", txtSearch.Text);

                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            txtPersonID.Text = dr["person_ID"].ToString();
                            txtFname.Text = dr["first_name"].ToString();
                            txtLname.Text = dr["last_name"].ToString();
                            txtFrom.Text = dr["pfrom"].ToString();
                            txtDestination.Text = dr["destination"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No Matching Record Found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtPersonID.Text = "";
                            txtFname.Text = "";
                            txtLname.Text = "";
                            txtFrom.Text =  "";
                            txtDestination.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string selectQuery = "SELECT  Person_ID, first_name, last_name, pfrom, destination FROM People_tbl";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con)){
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dataGridView1.DataSource = dt;
                    }
                 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtLuggageID.Text) || string.IsNullOrWhiteSpace(txtPersonID.Text) || string.IsNullOrWhiteSpace(txtFname.Text) || string.IsNullOrWhiteSpace(txtDestination.Text) || string.IsNullOrWhiteSpace(txtLname.Text) || string.IsNullOrWhiteSpace(txtlweight.Text))
                {
                    MessageBox.Show("All Fields Are Required", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string insertQuery = "INSERT INTO Luggage_tbl (luggageID, person_ID, firstName, lastName, lfrom, destination, added_date, LuggageWeight, payment_status)" +
                                          "VALUES (@luggageID, @person_ID, @firstName, @lastName, @lfrom, @destination, @added_date, @LuggageWeight, 'pending')";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@luggageID", txtLuggageID.Text);
                        cmd.Parameters.AddWithValue("@person_ID", txtPersonID.Text);
                        cmd.Parameters.AddWithValue("@firstName", txtFname.Text);
                        cmd.Parameters.AddWithValue("@lastName", txtLname.Text);
                        cmd.Parameters.AddWithValue("@lfrom", txtFrom.Text);
                        cmd.Parameters.AddWithValue("@destination", txtDestination.Text);
                        cmd.Parameters.AddWithValue("@added_date", txtDate.Value);
                        cmd.Parameters.AddWithValue("@LuggageWeight", txtlweight.Text);

                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Luggage Information Has Been Recorded", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearControl();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtLuggageID.Text) || string.IsNullOrWhiteSpace(txtPersonID.Text) || string.IsNullOrWhiteSpace(txtFname.Text) || string.IsNullOrWhiteSpace(txtDestination.Text) || string.IsNullOrWhiteSpace(txtLname.Text) || string.IsNullOrWhiteSpace(txtlweight.Text))
                {
                    MessageBox.Show("All Fields Are Required", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string updageQuery = "UPDATE Luggage_tbl SET person_ID = @person_ID, firstName = @firstName, lastName = @lastName, lfrom = @lfrom, destination = @destination, added_date = @added_date, LuggageWeight =  @LuggageWeight, payment_status = 'pending' WHERE luggageID = @luggageID";
                    using (SqlCommand cmd = new SqlCommand(updageQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@luggageID", txtLuggageID.Text);
                        cmd.Parameters.AddWithValue("@person_ID", txtPersonID.Text);
                        cmd.Parameters.AddWithValue("@firstName", txtFname.Text);
                        cmd.Parameters.AddWithValue("@lastName", txtLname.Text);
                        cmd.Parameters.AddWithValue("@lfrom", txtFrom.Text);
                        cmd.Parameters.AddWithValue("@destination", txtDestination.Text);
                        cmd.Parameters.AddWithValue("@added_date", txtDate.Value);
                        cmd.Parameters.AddWithValue("@LuggageWeight", txtlweight.Text);

                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Luggage Information Has Been Recorded", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearControl();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    //selectQuery
                    string selectQuery = "SELECT luggageID, LuggageWeight FROM Luggage_tbl WHERE luggageID = @luggageID";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@luggageID", txtSearch.Text);

                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            txtLuggageID.Text = dr["luggageID"].ToString();
                            txtlweight.Text = dr["LuggageWeight"].ToString();
                           
                        }
                        else
                        {
                            MessageBox.Show("No Matching Record Found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtLuggageID.Text = "";
                            txtlweight.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    //insertQuery
                    String insertQuery = "Delete from Luggage_tbl where luggageID = @luggageID ";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@luggageID", txtLuggageID.Text);



                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    MessageBox.Show("Person has been Deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearControl();
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!:" + ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            clearControl();
        }

        void clearControl()
        {
            txtlweight.Text = "";
            txtLuggageID.Text = "";
            txtPersonID.Text = "";
            txtFname.Text = "";
            txtLname.Text = "";
            txtFrom.Text = "";
            txtDestination.Text = "";
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string selectQuery = "SELECT * FROM Luggage_tbl";
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
                MessageBox.Show("Error!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
