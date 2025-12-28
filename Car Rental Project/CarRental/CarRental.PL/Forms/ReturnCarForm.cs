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
    public partial class ReturnCarForm : Form
    {
        private readonly IRentalService _rentalService;
        private int _selectedContractId = 0;
        public ReturnCarForm(IRentalService rentalService)
        {
            _rentalService = rentalService;
            InitializeComponent();
            LoadActiveRentals();
        }
        private void LoadActiveRentals()
        {
            var activeRentals = _rentalService.GetActiveRentals();
            dataGridView1.DataSource = activeRentals.ToList();
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];
                _selectedContractId = Convert.ToInt32(row.Cells["ContractId"].Value);

                var customerName = row.Cells["CustomerName"].Value?.ToString();
                var carInfo = $"{row.Cells["CarBrand"].Value} {row.Cells["CarModel"].Value}";
                var startDate = Convert.ToDateTime(row.Cells["StartDate"].Value).ToShortDateString();
                var endDate = Convert.ToDateTime(row.Cells["EndDate"].Value).ToShortDateString();
                var totalPrice = Convert.ToDecimal(row.Cells["TotalPrice"].Value);

                label2.Text = $"Contract #{_selectedContractId} | Customer: {customerName} | Car: {carInfo} | Period: {startDate} - {endDate}";
                textBox1.Text = totalPrice.ToString("F2");
                textBox2.Text = "0.00";
                textBox3.Text = totalPrice.ToString("F2");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(textBox1.Text, out decimal totalPrice) &&
                    decimal.TryParse(textBox2.Text, out decimal extraFees))
            {
                textBox3.Text = (totalPrice + extraFees).ToString("F2");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_selectedContractId == 0)
            {
                MessageBox.Show("Please select a rental contract", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Process car return?\nFinal Amount: ${textBox3.Text}",
                "Confirm Return",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var returnDto = new ReturnCarDto
                {
                    ContractId = _selectedContractId,
                    ActualReturnDate = dateTimePicker1.Value,
                    ExtraFees = decimal.Parse(textBox2.Text)
                };

                _rentalService.ReturnCar(returnDto);

                MessageBox.Show("Car returned successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadActiveRentals();
                ClearFields();
            }
        }
        private void ClearFields()
        {
            _selectedContractId = 0;
            label2.Text = "Contract ID: Not Selected";
            textBox1.Clear();
            textBox2.Text = "0.00";
            textBox3.Clear();
        }

        private void ReturnCarForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadActiveRentals();
            ClearFields();
        }
    }
}
