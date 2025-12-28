using CarRental.CarRental.BLL.DTOs;
using CarRental.CarRental.BLL.Interfaces;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CarRental.CarRental.PL.Forms
{
    public partial class ManageRentalAgentsForm : Form
    {
        private readonly IRentalAgentService _rentalAgentService;
        private int _selectedAgentId = 0;

        public ManageRentalAgentsForm(IRentalAgentService rentalAgentService)
        {
            _rentalAgentService = rentalAgentService;
            InitializeComponent();
            LoadAgents();
        }

        private void ManageRentalAgentsForm_Load(object sender, EventArgs e)
        {
        }

        private void LoadAgents()
        {
            var agents = _rentalAgentService.GetAllAgents().ToList();
            dataGridView1.DataSource = agents;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];
                _selectedAgentId = Convert.ToInt32(row.Cells["UserId"].Value);

                
                textBox1.Text = row.Cells["UserName"].Value?.ToString();
                textBox2.Clear(); 
                checkBox1.Checked = Convert.ToBoolean(row.Cells["IsActive"].Value);
            }
        }

        private void button1_Click(object sender, EventArgs e) 
        {
           
                if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Please enter username and password",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var agentDTO = new RentalAgentDto
                {
                    UserName = textBox1.Text,
                    Password = textBox2.Text,
                    IsActive = checkBox1.Checked
                };

                _rentalAgentService.AddRentalAgent(agentDTO);

                MessageBox.Show("Rental agent added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadAgents();
                ClearFields();
            }
         
       

        private void button2_Click(object sender, EventArgs e) 
        {
            try
            {
                if (_selectedAgentId == 0)
                {
                    MessageBox.Show("Please select an agent to update", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Please enter username", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var agentDto = new RentalAgentDto
                {
                    UserId = _selectedAgentId,
                    UserName = textBox1.Text,
                    Password = textBox2.Text,
                    IsActive = checkBox1.Checked
                };

                _rentalAgentService.UpdateRentalAgent(agentDto);
                MessageBox.Show("Agent updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadAgents();
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
                if (_selectedAgentId == 0)
                {
                    MessageBox.Show("Please select an agent", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool newStatus = !checkBox1.Checked;
                string action = newStatus ? "activate" : "deactivate";

                var result = MessageBox.Show($"Are you sure you want to {action} this agent?",
                    "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _rentalAgentService.ActivateDeactivateAgent(_selectedAgentId, newStatus);
                    MessageBox.Show($"Agent {action}d successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadAgents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e) 
        {
            LoadAgents();
            ClearFields();
        }

        private void ClearFields()
        {
            _selectedAgentId = 0;
            
            textBox1.Clear();
            textBox2.Clear();
            checkBox1.Checked = true;
        }
    }
}