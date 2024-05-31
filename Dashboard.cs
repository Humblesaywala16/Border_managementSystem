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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            totalUsers();
            populateDate();
            cbxDate.SelectedIndexChanged += cbxDate_SelectedIndexChanged;
        }

        String connectionString = "Data Source=DESKTOP-6L3FHIT;Initial Catalog=borderMgtSystem_db;Integrated Security=True";


        private void cbxDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            totalAmount();
            totalCustomer();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            User us = new User();
            this.Hide();
            us.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            User us = new User();
            this.Hide();
            us.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string selectQuery = "SELECT * FROM People_tbl";
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
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string selectQuery = "SELECT * FROM Payment_tbl";
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
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void totalUsers()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string selectQuery = "SELECT COUNT(*) FROM user_tb";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        int userCount = (int)cmd.ExecuteScalar();
                        txtUsers.Text = userCount.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void totalAmount()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string selectQuery = "SELECT SUM(amount) FROM Payment_tbl WHERE CAST(paymentDate AS DATE) = @paymentDate";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        if (cbxDate.SelectedItem != null)
                        {
                            DateTime selectedDate = DateTime.Parse(cbxDate.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@paymentDate", selectedDate);

                            object result = cmd.ExecuteScalar();
                            if (result != DBNull.Value)
                            {
                                decimal totalAmount = (decimal)result;
                                txtPayment.Text = totalAmount.ToString("C");
                            }
                            else
                            {
                                txtPayment.Text = "0";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


       void populateDate()
{
    try
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();

            string selectQuery = "SELECT DISTINCT paymentDate FROM Payment_tbl";
            using (SqlCommand cmd = new SqlCommand(selectQuery, con))
            {
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DateTime paymentDate = reader.GetDateTime(0);
                    string formattedDate = paymentDate.ToString("yyyy-MM-dd"); // Ensure consistent date format
                    cbxDate.Items.Add(formattedDate);
                }
                reader.Close();
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

        }


       void totalCustomer()
       {
           try
           {
               using (SqlConnection con = new SqlConnection(connectionString))
               {
                   con.Open();

                   string selectQuery = "SELECT COUNT(*) FROM Payment_tbl WHERE CAST(paymentDate AS DATE) = @paymentDate";
                   using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                   {
                       if (cbxDate.SelectedItem != null)
                       {
                           DateTime selectedDate = DateTime.Parse(cbxDate.SelectedItem.ToString());
                           cmd.Parameters.AddWithValue("@paymentDate", selectedDate);

                           object result = cmd.ExecuteScalar();
                           if (result != DBNull.Value)
                           {
                               int totalCustomer = (int)result;
                               txtCustomer.Text = totalCustomer.ToString();
                           }
                           else
                           {
                               txtCustomer.Text = "0";
                           }
                       }
                   }
               }
           }
           catch (Exception ex)
           {
               MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
       }

       private void button12_Click(object sender, EventArgs e)
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
               MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
       }
    }
}
