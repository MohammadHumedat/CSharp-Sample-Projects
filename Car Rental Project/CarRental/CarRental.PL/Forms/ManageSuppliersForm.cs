using CarRental.CarRental.BLL.DTOs;
using CarRental.CarRental.BLL.Interfaces;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CarRental.CarRental.PL.Forms
{
    public partial class ManageSuppliersForm : Form
    {
        private readonly ISupplierService _supplierService;
        private int _selectedSupplierId = 0;

        public ManageSuppliersForm(ISupplierService supplierService)
        {
            _supplierService = supplierService;
            InitializeComponent();
            LoadSuppliers();
            ClearFields(); 
        }

        private void LoadSuppliers()
        {
            var suppliers = _supplierService.GetAllSuppliers();
            dataGridView1.DataSource = suppliers.ToList();
        }

        private void ManageSuppliersForm_Load(object sender, EventArgs e)
        {
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];
                _selectedSupplierId = Convert.ToInt32(row.Cells["SupplierId"].Value);

                label1.Text = $"Supplier ID: {_selectedSupplierId}";
                textBox1.Text = row.Cells["SupplierName"].Value?.ToString();
                textBox2.Text = row.Cells["Country"].Value?.ToString();
                textBox3.Text = row.Cells["Contact"].Value?.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e) // Add supplier
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Please enter supplier name", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var supplierDto = new SupplierDto
                {
                    SupplierName = textBox1.Text,
                    Country = textBox2.Text,
                    Contact = textBox3.Text
                };

                _supplierService.AddSupplier(supplierDto);
                MessageBox.Show("Supplier added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadSuppliers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e) // Update supplier
        {
            try
            {
                if (_selectedSupplierId == 0)
                {
                    MessageBox.Show("Please select a supplier to update", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Please enter supplier name", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var supplierDto = new SupplierDto
                {
                    SupplierId = _selectedSupplierId,
                    SupplierName = textBox1.Text,
                    Country = textBox2.Text,
                    Contact = textBox3.Text
                };

                _supplierService.UpdateSupplier(supplierDto);
                MessageBox.Show("Supplier updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadSuppliers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e) // Delete supplier
        {
            try
            {
                if (_selectedSupplierId == 0)
                {
                    MessageBox.Show("Please select a supplier to delete", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show("Are you sure you want to delete this supplier?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _supplierService.DeleteSupplier(_selectedSupplierId);
                    MessageBox.Show("Supplier deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadSuppliers();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e) // Refresh
        {
            LoadSuppliers();
            ClearFields();
        }

        private void ClearFields()
        {
            _selectedSupplierId = 0;
            label1.Text = "Supplier ID: Not Selected";
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }
    }
}