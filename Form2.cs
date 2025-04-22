using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pabdproject
{
    public partial class Form2: Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create a new instance of Form2
            Form1 form1 = new Form1();

            // Show Form2
            form1.Show();

            // Optionally, you can hide Form1 if you don't want it visible after opening Form2
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Create a new instance of Form4
            Form4 form4 = new Form4();

            // Show Form4
            form4.Show();

            // Optionally, you can hide Form2 if you don't want it visible after opening Form4
            this.Hide();

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
