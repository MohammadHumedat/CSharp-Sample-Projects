using CarRental.CarRental.BLL.DTOs;
using CarRental.CarRental.BLL.Interfaces;
using CarRental.CarRental.BLL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarRental.CarRental.PL.Forms
{
    public partial class ManageCustomersForm : Form
    {
        private readonly ICustomerService _customerService;
        private readonly IRentalService _rentalService;
        private int _selectedCustomerId = 0;
        public ManageCustomersForm(ICustomerService customerService, IRentalService rentalService)
        {
            _customerService = customerService;
            _rentalService = rentalService;
            InitializeComponent();
            LoadCustomers();
        }
        private void LoadCustomers()
        {
            var customers = _customerService.GetAllCustomers();
            dataGridView1.DataSource = customers.ToList();
        }
        private void ManageCustomersForm_Load(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];
                _selectedCustomerId = Convert.ToInt32(row.Cells["CustomerId"].Value);

                label3.Text = $"Customer ID: {_selectedCustomerId}";
                textBox3.Text = row.Cells["CustomerName"].Value?.ToString();
                textBox4.Text = row.Cells["LicenseNumber"].Value?.ToString();
                textBox5.Text = row.Cells["Phone"].Value?.ToString();
                textBox6.Text = row.Cells["Address"].Value?.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                var customer = _customerService.GetCustomerByLicense(textBox2.Text);
                if (customer != null)
                    dataGridView1.DataSource = new[] { customer }.ToList();
                else
                    MessageBox.Show("Customer not found", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (!string.IsNullOrEmpty(textBox1.Text))
            {
                var customers = _customerService.SearchCustomers(textBox1.Text);
                dataGridView1.DataSource = customers.ToList();
            }
            else
            {
                LoadCustomers();
            }
        }

        private void button2_Click(object sender, EventArgs e)// add customer
        {
            try
            {
                var customerDto = new CustomerDto
                {
                    CustomerName = textBox3.Text,
                    LicenseNumber = textBox4.Text,
                    Phone = textBox5.Text,
                    Address = textBox6.Text
                };

                _customerService.AddCustomer(customerDto);
                MessageBox.Show("Customer registered successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCustomers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedCustomerId == 0)
                {
                    MessageBox.Show("Please select a customer to update", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var customerDto = new CustomerDto
                {
                    CustomerId = _selectedCustomerId,
                    CustomerName = textBox3.Text,
                    LicenseNumber = textBox4.Text,
                    Phone = textBox5.Text,
                    Address = textBox6.Text
                };

                _customerService.UpdateCustomer(customerDto);
                MessageBox.Show("Customer updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCustomers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedCustomerId == 0)
                {
                    MessageBox.Show("Please select a customer to delete", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show("Are you sure you want to delete this customer?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _customerService.DeleteCustomer(_selectedCustomerId);
                    MessageBox.Show("Customer deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCustomers();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e) // view history
        {
            if (_selectedCustomerId == 0)
            {
                MessageBox.Show("Please select a customer", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var rentals = _rentalService.GetCustomerRentals(_selectedCustomerId).ToList();

            if (rentals.Count == 0)
            {
                MessageBox.Show("No rental history found for this customer", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

           
            var historyForm = new Form
            {
                Text = "Customer Rental History",
                Size = new Size(900, 500),
                StartPosition = FormStartPosition.CenterParent
            };

            var dgvHistory = new DataGridView
            {
                Location = new Point(10, 10),
                Size = new Size(860, 430),
                DataSource = rentals,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true
            };

            historyForm.Controls.Add(dgvHistory);
            historyForm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoadCustomers();
            ClearFields();
            textBox1.Clear();
            textBox2.Clear();
        }
        private void ClearFields()
        {
            _selectedCustomerId = 0;
            label3.Text = "Customer ID: Not Selected";
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }
    }
}
