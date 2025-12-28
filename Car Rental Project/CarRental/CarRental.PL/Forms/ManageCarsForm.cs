using CarRental.CarRental.BLL.DTOs;
using CarRental.CarRental.BLL.Interfaces;
using CarRental.CarRental.DAL.Entities;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CarRental.CarRental.PL.Forms
{
    public partial class ManageCarsForm : Form
    {
        private readonly ICarService _carService;
        private readonly ISupplierService _supplierService;
        private int _selectedCarId = 0;

        public ManageCarsForm(ICarService carService, ISupplierService supplierService)
        {
            _carService = carService;
            _supplierService = supplierService;
            InitializeComponent();
            LoadCars();
            LoadSuppliers();
        }

        private void ManageCarsForm_Load(object sender, EventArgs e)
        {
        }

        private void LoadCars()
        {
            var cars = _carService.GetAllCars().ToList();
            dataGridView1.DataSource = cars;
        }

        private void LoadSuppliers()
        {
            var suppliers = _supplierService.GetAllSuppliers().ToList();
            comboBox2.DataSource = suppliers;
            comboBox2.DisplayMember = "SupplierName";
            comboBox2.ValueMember = "SupplierId";
        }

        private void ClearFields()
        {
            _selectedCarId = 0;
            label4.Text = "Car ID: Not Selected";
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();

           
            if (comboBox2.Items.Count > 0)
                comboBox2.SelectedIndex = 0;
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e) // Show data from database

        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];
                _selectedCarId = Convert.ToInt32(row.Cells["CarId"].Value);

                label4.Text = $"Car ID: {_selectedCarId}";
                textBox4.Text = row.Cells["Brand"].Value?.ToString();
                textBox5.Text = row.Cells["Model"].Value?.ToString();
                textBox6.Text = row.Cells["Year"].Value?.ToString();
                textBox7.Text = row.Cells["Color"].Value?.ToString();
                textBox8.Text = row.Cells["Mileage"].Value?.ToString();
                textBox9.Text = row.Cells["PurchaseCost"].Value?.ToString();

               
                var supplierId = Convert.ToInt32(row.Cells["SupplierId"].Value);
                comboBox2.SelectedValue = supplierId;

                
                var status = row.Cells["Status"].Value?.ToString();
                if (!string.IsNullOrEmpty(status))
                {
                    int statusIndex = comboBox1.FindStringExact(status);
                    if (statusIndex >= 0)
                        comboBox1.SelectedIndex = statusIndex;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e) // Add new Car
        {
            try
            {
               
                if (string.IsNullOrWhiteSpace(textBox4.Text) ||
                    string.IsNullOrWhiteSpace(textBox5.Text) ||
                    comboBox2.SelectedValue == null)
                {
                    MessageBox.Show("Please fill all required fields", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var carDto = new CarDto
                {
                    Brand = textBox4.Text,
                    Model = textBox5.Text,
                    Year = int.Parse(textBox6.Text),
                    Color = textBox7.Text,
                    Mileage = double.Parse(textBox8.Text),
                    PurchaseCost = decimal.Parse(textBox9.Text),
                    SupplierId = (int)comboBox2.SelectedValue
                };

                _carService.AddCar(carDto);
                MessageBox.Show("Car added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadCars();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e) // Updata car
        {
            try
            {
                if (_selectedCarId == 0)
                {
                    MessageBox.Show("Please select a car to update", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (comboBox2.SelectedValue == null)
                {
                    MessageBox.Show("Please select a supplier", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var carDto = new CarDto
                {
                    CarId = _selectedCarId,
                    Brand = textBox4.Text,
                    Model = textBox5.Text,
                    Year = int.Parse(textBox6.Text),
                    Color = textBox7.Text,
                    Mileage = double.Parse(textBox8.Text),
                    PurchaseCost = decimal.Parse(textBox9.Text),
                    SupplierId = (int)comboBox2.SelectedValue
                };

                _carService.UpdateCar(carDto);
                MessageBox.Show("Car updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadCars();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e) // Delete car
        {
            try
            {
                if (_selectedCarId == 0)
                {
                    MessageBox.Show("Please select a car to delete", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show(
                    "Are you sure you want to delete this car?",
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                    return;

                _carService.DeleteCar(_selectedCarId);
                MessageBox.Show("Car deleted successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadCars();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e) // search for car
        {
            try
            {
                int? year = string.IsNullOrEmpty(textBox3.Text) ?
                    null : (int?)int.Parse(textBox3.Text);

                var cars = _carService.SearchCars(textBox1.Text, textBox2.Text, year).ToList();
                dataGridView1.DataSource = cars;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e) // clear data from input feilds
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            LoadCars();
        }

        private void button6_Click(object sender, EventArgs e) // refresh data feilds and data grid view
        {
            LoadCars();
            ClearFields();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void label9_Click(object sender, EventArgs e)
        {
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}