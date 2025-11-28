using StudentAssistent.Business_Logic_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace StudentAssistent
{
    public partial class LogIn : Form
    {
        private StudentService service;
        public LogIn(StudentService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            this.Text = "Student Assistant - Login"; // Form name
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter username and password!",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (service.Login(username, password))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password!",
                    "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register registerForm = new Register(service);
            registerForm.ShowDialog();
            this.Show();
        }
    }
}
