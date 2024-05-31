using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Border_managementSystem
{
    public partial class UserDashboard : Form
    {
        public UserDashboard()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Luggage lug = new Luggage();
            this.Hide();
            lug.Show();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Luggage lu = new Luggage();
            this.Hide();
            lu.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Payment pym = new Payment();
            this.Hide();
            pym.Show();
        }
    }
}
