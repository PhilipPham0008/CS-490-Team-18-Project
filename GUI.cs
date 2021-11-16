using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gui
{
    public partial class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();
        }

        // The GUI initializes.
        private void Form1_Load(object sender, EventArgs e)
        {
            startSystemButton.Text = "Start System";
            pauseSystemButton.Text = "Pause System";
            systemStatusLabel.Text = "Waiting...";
            oneTimeUnit.Text = "1 time unit = ";
            msLabel.Text = "ms";
            waitingProcessQueue1.Text = "Waiting Process Queue";
        }

        public delegate void GuiEventHandler(int num);
        public event GuiEventHandler startProcessorsEvent;

        // The Start System button begins/resumes the CPUs' operations.
        private void startSystemButton_Click(object sender, EventArgs e)
        {
            systemStatusLabel.Text = "System Running";
            startProcessorsEvent(1);
            // send 
        }

        // The Pause System button stops the CPUs' operations until the
        // Start System button is pressed.
        private void pauseSystemButton_Click(object sender, EventArgs e)
        {
            systemStatusLabel.Text = "System Paused";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void millisecondsTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
