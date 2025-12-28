using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace StudentAssistent
{
    public partial class TaskDialog : Form
    {
        public models.Task Task { get; private set; }
        public TaskDialog()
        {
            InitializeComponent();
            // Set default dates
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now.AddDays(7);
        }
        public TaskDialog(models.Task existingTask) : this()
        {
            // For editing existing task
            textBox1.Text = existingTask.Title;
            richTextBox1.Text = existingTask.Description;
            dateTimePicker1.Value = existingTask.StartDate;
            dateTimePicker2.Value = existingTask.EndDate;
            comboBox1.SelectedIndex = (int)existingTask.Status;
        }

        private void TaskDialog_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter a task title!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dateTimePicker2.Value < dateTimePicker1.Value)
            {
                MessageBox.Show("End date must be after start date!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Task = new models.Task
            {
                Id = Guid.NewGuid(),
                Title = textBox1.Text.Trim(),
                Description = richTextBox1.Text.Trim(),
                StartDate = dateTimePicker1.Value,
                EndDate = dateTimePicker2.Value,
                Status = (models.TaskStatus)(TaskStatus)comboBox1.SelectedIndex
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
