using CarRental.CarRental.BLL.Interfaces;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CarRental.CarRental.PL.Forms
{
    public partial class ReportsForm : Form
    {
        private readonly IReportService _reportService;

        public ReportsForm(IReportService reportService)
        {
            _reportService = reportService;
            InitializeComponent();
            LoadReports();
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            for (int year = DateTime.Now.Year; year >= 2020; year--)
            {
                comboBox1.Items.Add(year);
            }
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;

            for (int month = 1; month <= 12; month++)
            {
                comboBox2.Items.Add(new DateTime(2000, month, 1).ToString("MMMM"));
            }
            if (comboBox2.Items.Count > 0)
                comboBox2.SelectedIndex = DateTime.Now.Month - 1;
        }

        private void LoadReports()
        {
            LoadInventoryStats();
            LoadPopularCars();
            CalculateIncome();
        }

        private void LoadInventoryStats()
        {
            try
            {
                int totalCars = _reportService.GetTotalCarsCount();
                int availableCars = _reportService.GetAvailableCarsCount();
                int rentedCars = _reportService.GetRentedCarsCount();

                label3.Text = totalCars.ToString();
                label5.Text = availableCars.ToString();
                label7.Text = rentedCars.ToString();

                var rentedCarsList = _reportService.GetRentedCarsList().ToList();
                listBox1.Items.Clear();

                if (rentedCarsList.Count == 0)
                {
                    listBox1.Items.Add("No cars currently rented");
                }
                else
                {
                    foreach (var car in rentedCarsList)
                    {
                        listBox1.Items.Add(car);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory stats: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPopularCars() // popular cars inside data grid view when you refresh or open the report
        {
            try
            {
                
                DataGridView dgv = FindDataGridView();

                if (dgv == null)
                {
                    Console.WriteLine("DataGridView not found - skipping LoadPopularCars");
                    return;
                }

                var popularCars = _reportService.GetMostRentedCarModels();

                if (popularCars == null || popularCars.Count == 0)
                {
                    dgv.DataSource = new[]
                    {
                new { Rank = 0, CarModel = "No rental data available", TimesRented = 0 }
            }.ToList();
                    return;
                }

                var dataSource = popularCars.Select((kv, index) => new
                {
                    Rank = index + 1,
                    CarModel = kv.Key,
                    TimesRented = kv.Value
                }).ToList();

                dgv.DataSource = dataSource;

                if (dgv.Columns.Count > 0)
                {
                    if (dgv.Columns.Contains("Rank"))
                        dgv.Columns["Rank"].Width = 80;

                    if (dgv.Columns.Contains("CarModel"))
                        dgv.Columns["CarModel"].Width = 400;

                    if (dgv.Columns.Contains("TimesRented"))
                    {
                        dgv.Columns["TimesRented"].Width = 150;
                        dgv.Columns["TimesRented"].DefaultCellStyle.Alignment =
                            DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading popular cars: {ex.Message}");
                
            }
        }

        
        private DataGridView FindDataGridView() // i am using this to solve the bug of load data into grid view.
        {
          
            if (dataGridView1 != null)
                return dataGridView1;

            
            if (groupBox3 != null)
            {
                foreach (Control control in groupBox3.Controls)
                {
                    if (control is DataGridView dgv)
                    {
                        Console.WriteLine($"Found DataGridView: {dgv.Name}");
                        return dgv;
                    }
                }
            }

            
            foreach (Control control in this.Controls)
            {
                if (control is DataGridView dgv)
                {
                    Console.WriteLine($"Found DataGridView in form: {dgv.Name}");
                    return dgv;
                }

               
                if (control is GroupBox gb)
                {
                    foreach (Control innerControl in gb.Controls)
                    {
                        if (innerControl is DataGridView innerDgv)
                        {
                            Console.WriteLine($"Found DataGridView in GroupBox: {innerDgv.Name}");
                            return innerDgv;
                        }
                    }
                }
            }

            Console.WriteLine("DataGridView not found anywhere!");
            return null;
        }
        private void CalculateIncome() // calculate the incume of income report [ year and month]
        {
            if (comboBox1.SelectedItem == null || comboBox2.SelectedIndex < 0)
                return;

            int year = (int)comboBox1.SelectedItem;
            int month = comboBox2.SelectedIndex + 1;

            decimal monthlyIncome = _reportService.GetMonthlyIncome(year, month);
            decimal annualIncome = _reportService.GetAnnualIncome(year);

            label10.Text = $"${monthlyIncome:N2}";
            label13.Text = $"${annualIncome:N2}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CalculateIncome();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadReports();
            MessageBox.Show("Reports refreshed successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // query 1: retrieve how many cars have extra fees
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int count = _reportService.GetCarsWithExtraFeesCount();

                MessageBox.Show(
                    $" Number of cars with extra fees: {count}",
                    "Query 1: Cars with Extra Fees",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //  Query 2: For a given customer, retrieve the model of the cars he rented
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                //  ValidationtextBox1
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show(
                        "Please enter a Customer ID in the text box!",
                        "Validation Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    textBox1.Focus();
                    return;
                }

                //  Parse Customer ID 
                if (!int.TryParse(textBox1.Text, out int customerId))
                {
                    MessageBox.Show(
                        "Invalid Customer ID! Please enter a valid number.",
                        "Validation Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    textBox1.Focus();
                    textBox1.SelectAll();
                    return;
                }

                //  Get car models
                var carModels = _reportService.GetCarModelsRentedByCustomer(customerId).ToList();

                if (carModels.Count == 0)
                {
                    MessageBox.Show(
                        $"No rental history found for Customer ID: {customerId}",
                        "Query 2: Customer's Rented Cars",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    string result = $"🚗 Customer ID {customerId} rented:\n\n";
                    for (int i = 0; i < carModels.Count; i++)
                    {
                        result += $"{i + 1}. {carModels[i]}\n";
                    }

                    MessageBox.Show(
                        result,
                        "Query 2: Customer's Rented Cars",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }

                //  Clear textBox
                textBox1.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //  Query 3: How many customers delayed in returning the rented cars
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int delayedCount = _reportService.GetDelayedCustomersCount();

                if (delayedCount == 0)
                {
                    MessageBox.Show(
                        " Great! No customers have delayed in returning cars.",
                        "Query 3: Delayed Customers",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    MessageBox.Show(
                        $" Number of customers who delayed in returning cars: {delayedCount}",
                        "Query 3: Delayed Customers",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Query 4: Retrieve the gold customer (most expensive contract)
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
               
                string goldCustomer = _reportService.GetGoldCustomer();

                MessageBox.Show(
                    $"Gold Customer (Most Expensive Contract):\n\n{goldCustomer}",
                    "Query 4: Gold Customer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {
        }

        private void label11_Click(object sender, EventArgs e)
        {
        }

        private void label13_Click(object sender, EventArgs e)
        {
        }
    }
}