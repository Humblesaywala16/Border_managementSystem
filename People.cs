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
    public partial class People : Form
    {
        public People()
        {
            InitializeComponent();
        }

        String connectionString = "Data Source=DESKTOP-6L3FHIT;Initial Catalog=borderMgtSystem_db;Integrated Security=True";


        // EXIT BUTTON
        private void button7_Click(object sender, EventArgs e)
        {//ExitQuery
            this. Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        // ADD BUTTON
        private void button1_Click(object sender, EventArgs e)
        {
            if (!checkPersonExists())
            {
                MessageBox.Show("This Person Already Exists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                addNewPerson();
            }
            
        }

        //UPDATE BUTTON
        private void button2_Click(object sender, EventArgs e)
        {
            if (checkPersonExists())
            {
                updatePerson();
            }
            else
            {
                MessageBox.Show("This Person Does Not Exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // DELETE BUTTON
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    //insertQuery
                    String insertQuery = "Delete from people_tbl where person_ID = @person_ID ";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@person_ID", txtid.Text);
                        


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

        // CLEAR BUTTON
        private void button3_Click(object sender, EventArgs e)
        {
            clearControl();
        }

        // VIEW BUTTON
        private void button5_Click(object sender, EventArgs e)
        {
            try 
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    //selectQuery
                    String selectQuery = "SELECT * FROM people_tbl";
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
                MessageBox.Show("Error" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // SEARCH BUTTON
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    //selectQuery
                    String selectQuery = "SELECT * FROM people_tbl WHERE person_ID = @person_ID";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@person_ID", txtsearch.Text);

                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            txtid.Text = dr["person_id"].ToString();
                            txtfname.Text = dr["first_name"].ToString();
                            txtlname.Text = dr["last_name"].ToString();
                            txtphone.Text = dr["telephone"].ToString();
                            txtemail.Text = dr["email"].ToString();
                            txtbirth.Text = dr["date_ofBirth"].ToString();
                            txtdate.Text = dr["date_ofTravel"].ToString();
                            cbdestination.Text = dr["destination"].ToString();
                            cbfrom.Text = dr["pfrom"].ToString();
                            cbduration.Text = dr["duration"].ToString();
                            cbgender.Text = dr["gender"].ToString();
                            cbpurpose.Text = dr["purpose"].ToString();
                            cbnationality.Text = dr["nationality"].ToString();
                            cbtdocument.Text = dr["travel_Document"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Not Search Found or Invalid Person ID", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtid.Text = "";
                            txtfname.Text = "";
                            txtlname.Text = "";
                            txtphone.Text = "";
                            txtemail.Text = "";
                            txtbirth.Text = "";
                            txtdate.Text = "";
                            cbdestination.Text = "";
                            cbfrom.Text = "";
                            cbduration.Text = "";
                            cbgender.Text = "";
                            cbpurpose.Text = "";
                            cbnationality.Text = "";
                            cbtdocument.Text = "";
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        //User defined methods
        bool checkPersonExists()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    //selectQuery
                    String selectQuery = "SELECT * FROM people_tbl ";
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
                MessageBox.Show("Error!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
          
        }
        void clearControl()
        {
            txtid.Text = "";
            txtfname.Text = "";
            txtlname.Text = "";
            txtphone.Text = "";
            txtemail.Text = "";
            txtbirth.Text = "";
            txtdate.Text = "";
            cbdestination.SelectedIndex = -1;
            cbduration. SelectedIndex = -1;
            cbfrom.SelectedIndex = -1;
            cbgender.SelectedIndex = -1;
            cbnationality.SelectedIndex = -1;
            cbpurpose.SelectedIndex = -1;
            cbtdocument.SelectedIndex = -1;
            
        }

        void updatePerson()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    //updateQuery
                    String updateQuery = "UPDATE people_tbl SET first_name = @first_name, last_name =  @last_name, telephone =  @telephone, email =  @email, gender = @gender, pfrom = @pfrom, date_ofBirth =  @date_ofBirth, destination =  @destination, nationality = @nationality, date_ofTravel = @date_ofTravel, travel_Document = @travel_Document, duration =  @duration, purpose = @purpose WHERE person_ID = @person_ID";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@person_ID", txtid.Text);
                        cmd.Parameters.AddWithValue("@first_name", txtfname.Text);
                        cmd.Parameters.AddWithValue("@last_name", txtlname.Text);
                        cmd.Parameters.AddWithValue("@telephone", txtphone.Text.Trim());
                        cmd.Parameters.AddWithValue("@email", txtemail.Text);
                        cmd.Parameters.AddWithValue("@gender", cbgender.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@pfrom", cbfrom.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@date_ofBirth", txtbirth.Value);
                        cmd.Parameters.AddWithValue("@destination", cbdestination.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@nationality", cbnationality.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@date_ofTravel", txtdate.Value);
                        cmd.Parameters.AddWithValue("@travel_Document", cbtdocument.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@duration", cbduration.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@purpose", cbpurpose.SelectedItem.ToString());



                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    MessageBox.Show("This person with the ID " +txtid.Text+ " has been updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearControl();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!:" + ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void addNewPerson()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    //insertQuery
                    String insertQuery = "INSERT INTO people_tbl(person_ID, first_name, last_name, telephone, email, gender, pfrom, date_ofBirth, destination, nationality, date_ofTravel, travel_Document, duration, purpose) VALUES(@person_ID, @first_name, @last_name, @telephone, @email, @gender, @pfrom, @date_ofBirth, @destination, @nationality, @date_ofTravel, @travel_Document, @duration, @purpose )";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@person_ID", txtid.Text);
                        cmd.Parameters.AddWithValue("@first_name", txtfname.Text);
                        cmd.Parameters.AddWithValue("@last_name", txtlname.Text);
                        cmd.Parameters.AddWithValue("@telephone", txtphone.Text.Trim());
                        cmd.Parameters.AddWithValue("@email", txtemail.Text);
                        cmd.Parameters.AddWithValue("@gender", cbgender.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@pfrom", cbfrom.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@date_ofBirth",txtbirth.Value);
                        cmd.Parameters.AddWithValue("@destination", cbdestination.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@nationality", cbnationality.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@date_ofTravel", txtdate.Value);
                        cmd.Parameters.AddWithValue("@travel_Document", cbtdocument.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@duration", cbduration.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@purpose", cbpurpose.SelectedItem.ToString());
                        
                       

                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    MessageBox.Show("New user has been added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearControl();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!:" + ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
