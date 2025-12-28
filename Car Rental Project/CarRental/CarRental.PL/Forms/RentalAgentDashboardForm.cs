using CarRental.CarRental.BLL.DTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace CarRental.CarRental.PL.Forms
{
    public partial class RentalAgentDashboardForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private UserDto _currentUser;

        public RentalAgentDashboardForm(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            InitializeComponent();
        }

        private void RentalAgentDashboardForm_Load(object sender, EventArgs e)
        {
        }

        public void SetCurrentUser(UserDto user)
        {
            _currentUser = user;
            label1.Text = $"Welcome, {user.UserName} (Rental Agent)";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = _serviceProvider.GetRequiredService<ManageCustomersForm>();
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var form = _serviceProvider.GetRequiredService<ManageCarsForm>();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form = _serviceProvider.GetRequiredService<CreateRentalForm>();
            form.SetCurrentUser(_currentUser);
            form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var form = _serviceProvider.GetRequiredService<ReturnCarForm>();
            form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Are you sure you want to logout?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}