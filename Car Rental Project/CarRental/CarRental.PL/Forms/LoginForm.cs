using CarRental.CarRental.BLL.DTOs;
using CarRental.CarRental.BLL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace CarRental.CarRental.PL.Forms
{
    public partial class LoginForm : Form
    {
        private readonly IAuthService authService;
        private readonly IServiceProvider serviceProvider;

        public LoginForm(IAuthService authService, IServiceProvider serviceProvider)
        {
            this.authService = authService;
            this.serviceProvider = serviceProvider;
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Please enter your Username and Password",
                        "Validation Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                UserDto user = authService.Login(textBox1.Text, textBox2.Text);

                MessageBox.Show($"Welcome {user.UserName} to Car Rental System",
                    "Login Successful",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                
                if (user.Role == "Admin")
                {
                    var adminDashboard = serviceProvider.GetRequiredService<AdminDashboardForm>();
                    adminDashboard.SetCurrentUser(user);
                    this.Hide();
                    adminDashboard.ShowDialog();
                    this.Close();
                }
                else if (user.Role == "Agent")
                {
                    var agentDashboard = serviceProvider.GetRequiredService<RentalAgentDashboardForm>();
                    agentDashboard.SetCurrentUser(user);
                    this.Hide();
                    agentDashboard.ShowDialog();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login Failed: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}