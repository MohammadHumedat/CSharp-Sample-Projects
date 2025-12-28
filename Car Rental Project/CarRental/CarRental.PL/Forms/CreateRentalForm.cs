using CarRental.CarRental.BLL.DTOs;
using CarRental.CarRental.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarRental.CarRental.PL.Forms
{
    public partial class CreateRentalForm : Form
    {
        private readonly ICustomerService _customerService;
        private readonly ICarService _carService;
        private readonly IRentalService _rentalService;
        private UserDto _currentUser;
        public CreateRentalForm(ICustomerService customerService, ICarService carService,
            IRentalService rentalService)
        {
            _customerService = customerService;
            _carService = carService;
            _rentalService = rentalService;
            InitializeComponent();
            LoadCustomers();
            LoadAvailableCars();
        }
        public void SetCurrentUser(UserDto user)
        {
            _currentUser = user;
        }

        private void LoadCustomers()
        {
            var customers = _customerService.GetAllCustomers().ToList();
            comboBox1.DataSource = customers;
            comboBox1.DisplayMember = "CustomerName";
            comboBox1.ValueMember = "CustomerId";
        }
        private void LoadAvailableCars()
        {
            var cars = _carService.GetAvailableCars().ToList();
            var carDisplay = cars.Select(c => new
            {
                c.CarId,
                Display = $"{c.Brand} {c.Model} ({c.Year}) - {c.Color}"
            }).ToList();

            comboBox2.DataSource = carDisplay;
            comboBox2.DisplayMember = "Display";
            comboBox2.ValueMember = "CarId";
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            var days = (dateTimePicker2.Value - dateTimePicker1.Value).Days;
            label5.Text = $"Rental Days: {days}";
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            var days = (dateTimePicker2.Value - dateTimePicker1.Value).Days;
            label5.Text = $"Rental Days: {days}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(dateTimePicker2.Value <= dateTimePicker1.Value)
                {
                MessageBox.Show("End date must be after start date", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var days = (dateTimePicker2.Value - dateTimePicker1.Value).Days;
            var dailyRate = decimal.Parse(textBox1.Text);
            var totalPrice = days * dailyRate;

            textBox2.Text = totalPrice.ToString("F2");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null || comboBox2.SelectedValue == null)
            {
                MessageBox.Show("Please select customer and car", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dateTimePicker2.Value <= dateTimePicker1.Value)
            {
                MessageBox.Show("End date must be after start date", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var rentDto = new RentCarDto
            {
                CustomerId = (int)comboBox1.SelectedValue,
                CarId = (int)comboBox2.SelectedValue,
                RentalAgentId = _currentUser.UserId,
                StartDate = dateTimePicker1.Value,
                EndDate = dateTimePicker2.Value,
                DailyRate = decimal.Parse(textBox1.Text)
            };

            var totalPrice = _rentalService.CreateRental(rentDto);

            MessageBox.Show($"Rental contract created successfully!\nTotal Price: ${totalPrice:F2}",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadAvailableCars();
            textBox2.Clear();
            textBox1.Text = "50.00";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now.AddDays(7);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadCustomers();
            LoadAvailableCars();
        }

        private void CreateRentalForm_Load(object sender, EventArgs e)
        {

        }
    }
}
