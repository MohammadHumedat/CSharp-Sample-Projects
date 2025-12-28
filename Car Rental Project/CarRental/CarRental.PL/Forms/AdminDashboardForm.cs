using CarRental.CarRental.BLL.DTOs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace CarRental.CarRental.PL.Forms
{
    public partial class AdminDashboardForm : Form
    {
        private readonly IServiceProvider _serviceProvider; 
        private UserDto _currentUser;

        public AdminDashboardForm(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider; 
            InitializeComponent();
        }

        private void AdminDashboardForm_Load(object sender, EventArgs e)
        {
        }

        public void SetCurrentUser(UserDto user)
        {
            _currentUser = user;
            label1.Text = $"Welcome, {user.UserName} (Admin)";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var manageCarForm = _serviceProvider.GetRequiredService<ManageCarsForm>();
            manageCarForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var manageSupplierForm = _serviceProvider.GetRequiredService<ManageSuppliersForm>();
            manageSupplierForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var manageRentalAgentForm = _serviceProvider.GetRequiredService<ManageRentalAgentsForm>();
            manageRentalAgentForm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var form = _serviceProvider.GetRequiredService<ReportsForm>();
            form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}