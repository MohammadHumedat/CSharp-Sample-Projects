using StudentAssistent.Business_Logic_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StudentAssistent
{
    public partial class Register : Form
    {
        private StudentService service;
        public Register(StudentService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void Register_Load(object sender, EventArgs e)
        {
            this.Text = "Student Assistant - Register";
        }

        private void button1_Click(object sender, EventArgs e)// create button
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();
            string fullName = textBox3.Text.Trim();
            string email = textBox4.Text.Trim();
            // checking
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username and Password are required!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (username.Length < 3)
            {
                MessageBox.Show("Username must be at least 3 characters!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Resistration
            if (service.Register(username, password, fullName, email))
            {
                MessageBox.Show("Registration successful! You can now login.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Username already exists!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e) // close button
        {
            this.Close();
        }
    }
}
