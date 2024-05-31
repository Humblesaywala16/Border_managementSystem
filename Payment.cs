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
    public partial class Payment : Form
    {
        public Payment()
        {
            InitializeComponent();
        }

        String connectionString = "Data Source=DESKTOP-6L3FHIT;Initial Catalog=borderMgtSystem_db;Integrated Security=True";
        private void button7_Click(object sender, EventArgs e)
        {
            calculatePayment();


        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string selectQuery = "SELECT luggageID, firstName, lastName, LuggageWeight FROM Luggage_tbl WHERE luggageID = @luggageID";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@luggageID", txtLuggage.Text);

                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            txtLuggage.Text = dr["luggageID"].ToString();
                            txtFname.Text = dr["firstName"].ToString();
                            txtLname.Text = dr["lastName"].ToString();
                            txtWeight.Text = dr["LuggageWeight"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No Record Found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtLuggage.Text = "";
                            txtFname.Text = "";
                            txtLname.Text = "";
                            txtWeight.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            payment();
            updateLuggagetbl();
        }

        void calculatePayment()
        {
            try
            {
                // Try to parse the weight input from the text box
                int weight;
                if (int.TryParse(txtWeight.Text, out weight))
                {
                    // Perform the calculation
                    double amount = weight * 250;

                    // Display the result in the amount text box
                    txtAmount.Text = amount.ToString();
                }
                else
                {
                    // Handle the case where the input is not a valid integer
                    MessageBox.Show("Please enter a valid integer weight.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Handle any other unexpected exceptions
                MessageBox.Show("An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void payment()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string insertQuery = "INSERT INTO payment_tbl (luggageID, firstName, lastName, amount, kilograms,paymentDate) VALUES (@luggageID, @firstName, @lastName, @amount, @kilograms, @paymentDate)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@luggageID", txtLuggage.Text);
                        cmd.Parameters.AddWithValue("@firstName", txtFname.Text);
                        cmd.Parameters.AddWithValue("@lastName", txtLname.Text);
                        cmd.Parameters.AddWithValue("@amount", txtAmount.Text);
                        cmd.Parameters.AddWithValue("@kilograms", txtWeight.Text);
                        cmd.Parameters.AddWithValue("@paymentDate", txtPaymentDate.Value);

                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    MessageBox.Show("Payment Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearControl();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void updateLuggagetbl()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string updateQuery = "Update Luggage_tbl Set payment_status = 'paid' WHERE luggageID = @luggageID";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@luggageID", txtLuggage.Text);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    MessageBox.Show("Payment Status Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clearControl();
        }
        void clearControl()
        { 
            txtLuggage.Text = "";
            txtFname.Text = "";
            txtLname.Text = "";
            txtAmount.Text = "";
            txtWeight.Text = "";
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string select = "SELECT * FROM Payment_tbl";
                    using (SqlCommand cmd = new SqlCommand(select, con))
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

        private void button4_Click(object sender, EventArgs e)
        {
            //*exit *//
            this.Close();
        }

        private void backToCustomersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            People ppl = new People();
            this.Hide();
            ppl.Show();
        }
    }
}
