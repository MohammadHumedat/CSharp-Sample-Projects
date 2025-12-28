using StudentAssistent.Business_Logic_Layer;
using StudentAssistent.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StudentAssistent
{
    public partial class MainForm : Form
    {
        private StudentService service;
        private System.Windows.Forms.Timer taskCheckTimer;
        public MainForm(StudentService service)
        {
            InitializeComponent();
            this.service = service;
            // subscribtion in event
            service.TaskDueSoon += OnTaskDueSoon;
            taskCheckTimer = new System.Windows.Forms.Timer();
            taskCheckTimer.Interval = 3600000; // 1 hour = 3600000 ms
            taskCheckTimer.Tick += TaskCheckTimer_Tick;
            taskCheckTimer.Start();
            LoadData();
        }
        private void TaskCheckTimer_Tick(object sender, EventArgs e)
        {
            //check the tasks each hour
            service.CheckDueTasks();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        // Event Handler when task is soon
        private void OnTaskDueSoon(object sender, TaskDueEventArgs e)
        {
            
            if (InvokeRequired)
            {
                Invoke(new TaskDueHandler(OnTaskDueSoon), sender, e);
                return;
            }

            MessageBox.Show(
                $" Task '{e.Task.Title}' is due soon!\n\n" +
                $"End Date: {e.Task.EndDate:yyyy-MM-dd}\n" +
                $"Status: {e.Task.Status}",
                "Task Alert",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }
        private void LoadData()
        {
            var user = service.GetCurrentUser();
            if (user != null)
            {
                this.Text = $"Student Assistant - {user.FullName}";
            }

            RefreshTasks();
            RefreshNotes();
            RefreshContacts();
            // Initialize progress bar
            UpdateProgressBar();
        }
        //  Tasks 
        protected override void OnFormClosing(FormClosingEventArgs e) // When close the form
        {
            taskCheckTimer?.Stop();
            taskCheckTimer?.Dispose();
            service.TaskDueSoon -= OnTaskDueSoon;
            service.Logout();
            base.OnFormClosing(e);
        }

        private void RefreshTasks()
        {
            listView1.Items.Clear();
            foreach (var task in service.GetTasks())
            {
                var item = new ListViewItem(task.Title);
                item.SubItems.Add(task.StartDate.ToShortDateString());
                item.SubItems.Add(task.EndDate.ToShortDateString());
                item.SubItems.Add(task.Status.ToString());
                item.Tag = task;

                // colors for tasks
                double daysLeft = (task.EndDate - DateTime.Now).TotalDays;
                if (daysLeft <= 1 && daysLeft >= 0 && task.Status != models.TaskStatus.Completed)
                {
                    item.BackColor = System.Drawing.Color.Orange;
                }

                listView1.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e) // Add Task
        {

            if (service.GetTotalItemCount() >= 10)
            {
                MessageBox.Show("Free trial limit reached! You can only create 10 items total.",
                    "Trial Limitation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 
            using (TaskDialog dialog = new TaskDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    service.AddTask(dialog.Task);
                    RefreshTasks();
                    UpdateProgressBar();
                    service.CheckDueTasks();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)  // Delete Task
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var task = (models.Task)listView1.SelectedItems[0].Tag;
                service.DeleteTask(task.Id);
                RefreshTasks();
                UpdateProgressBar();  // Update when deleting
            }
        }
        // Notes 

        private void RefreshNotes()
        {
            listView2.Items.Clear();
            foreach (var note in service.GetNotes())
            {
                var item = new ListViewItem(note.Title);
                item.SubItems.Add(note.Content.Length > 50 ?
                    note.Content.Substring(0, 50) + "..." : note.Content);
                item.Tag = note;
                listView2.Items.Add(item);
            }
        }

        private void button3_Click(object sender, EventArgs e)  // Add Note
        {
            if (service.GetTotalItemCount() >= 10)
            {
                MessageBox.Show("Free trial limit reached! You can only create 10 items total.",
                    "Trial Limitation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string title = PromptInput("Enter note title:");
            if (!string.IsNullOrEmpty(title))
            {
                var note = new Note
                {
                    Title = title,
                    Content = ""
                };
                service.AddNote(note);
                RefreshNotes();
                UpdateProgressBar();
            }
        }

        private void button4_Click(object sender, EventArgs e)  // Delete Note
        {
            if (listView2.SelectedItems.Count > 0)
            {
                var note = (models.Note)listView2.SelectedItems[0].Tag;
                service.DeleteNote(note.Id);
                RefreshNotes();
                UpdateProgressBar();  //  Update when deleting
            }
        }
        //  Contacts 

        private void RefreshContacts()
        {
            listView3.Items.Clear();
            foreach (var contact in service.GetContacts())
            {
                var item = new ListViewItem(contact.Name);
                item.SubItems.Add(contact.Phone);
                item.SubItems.Add(contact.Email);
                item.Tag = contact;
                listView3.Items.Add(item);
            }
        }

        private void button5_Click(object sender, EventArgs e)  // Add Contact
        {
            if (service.GetTotalItemCount() >= 10)
            {
                MessageBox.Show("Free trial limit reached! You can only create 10 items total.",
                    "Trial Limitation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string name = PromptInput("Enter contact name:");
            if (!string.IsNullOrEmpty(name))
            {
                var contact = new Contact
                {
                    Name = name,
                    Phone = "",
                    Email = ""
                };
                service.AddContact(contact);
                RefreshContacts();
                UpdateProgressBar();
            }
        }

        private void button6_Click(object sender, EventArgs e)  // Delete Contact
        {
            if (listView3.SelectedItems.Count > 0)
            {
                var contact = (Contact)listView3.SelectedItems[0].Tag;
                service.DeleteContact(contact.Id);
                RefreshContacts();
                UpdateProgressBar();  //  Update when deleting
            }
        }
        // Helper 

        private string PromptInput(string message)
        {
            using (Form prompt = new Form())
            {
                prompt.Width = 400;
                prompt.Height = 150;
                prompt.Text = "Input";
                prompt.StartPosition = FormStartPosition.CenterParent;

                Label label = new Label() { Left = 20, Top = 20, Text = message, Width = 350 };
                TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 350 };
                Button confirmation = new Button() { Text = "OK", Left = 200, Width = 80, Top = 80, DialogResult = DialogResult.OK };
                Button cancel = new Button() { Text = "Cancel", Left = 290, Width = 80, Top = 80, DialogResult = DialogResult.Cancel };

                confirmation.Click += (s, ev) => { prompt.Close(); };
                cancel.Click += (s, ev) => { prompt.Close(); };

                prompt.Controls.Add(label);
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(cancel);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }
        }
        private void UpdateProgressBar()
        {
            int totalItems = service.GetTotalItemCount();
            toolStripProgressBar1.Maximum = 10;
            toolStripProgressBar1.Value = totalItems;

            toolStripProgressBar1.Text = $"Items: {totalItems}/10";

            if (totalItems >= 10)
            {
                toolStripProgressBar1.ForeColor = Color.Red;
                toolStripProgressBar1.Text = "TRIAL LIMIT REACHED (10/10)";
            }
            else if (totalItems >= 8)
            {
                toolStripProgressBar1.ForeColor = Color.Orange;
                toolStripProgressBar1.Text = $"Items: {totalItems}/10 (Near Limit!)";
            }
            else
            {
                toolStripProgressBar1.ForeColor = Color.Black;
                toolStripProgressBar1.Text = $"Items: {totalItems}/10 (Trial Version)";
            }
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var task = (models.Task)listView1.SelectedItems[0].Tag;

                richTextBox1.Clear();
                richTextBox1.SelectionFont = new Font("Arial", 10, FontStyle.Bold);
                richTextBox1.AppendText($"Task: {task.Title}\n\n");

                richTextBox1.SelectionFont = new Font("Arial", 9, FontStyle.Regular);
                richTextBox1.AppendText($"Description:\n{task.Description}\n\n");
                richTextBox1.AppendText($"Start Date: {task.StartDate:yyyy-MM-dd}\n");
                richTextBox1.AppendText($"End Date: {task.EndDate:yyyy-MM-dd}\n");

                // calculate remining days
                double daysLeft = (task.EndDate - DateTime.Now).TotalDays;
                string daysText = daysLeft < 0 ? "OVERDUE!" : $"{Math.Ceiling(daysLeft)} days left";

                richTextBox1.AppendText($"Time Remaining: {daysText}\n");
                richTextBox1.AppendText($"Status: {task.Status}");

                //Colors according to status
                if (daysLeft <= 1 && task.Status != models.TaskStatus.Completed)
                {
                    richTextBox1.BackColor = Color.LightSalmon;
                }
                else if (task.Status == models.TaskStatus.Completed)
                {
                    richTextBox1.BackColor = Color.LightGreen;
                }
                else
                {
                    richTextBox1.BackColor = Color.White;
                }
            }
            else
            {
                richTextBox1.Clear();
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void copleteTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var task = (models.Task)listView1.SelectedItems[0].Tag;

                using (TaskDialog dialog = new TaskDialog(task))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        // Keep the same ID
                        dialog.Task.Id = task.Id;
                        service.UpdateTask(dialog.Task);
                        RefreshTasks();
                        service.CheckDueTasks();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a task to edit!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var task = (models.Task)listView1.SelectedItems[0].Tag;
                task.Status = models.TaskStatus.Completed;
                service.UpdateTask(task);
                RefreshTasks();

                MessageBox.Show($"Task '{task.Title}' marked as completed!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a task first!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are You sure??", "Yes",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                this.Close();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e) // Delete all tasks
        {
            // Check before deleting
            var result = MessageBox.Show(
                "Are you sure you want to delete ALL tasks?\n\nThis action cannot be undone!",
                "Confirm Clear All Tasks",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                // get a list of tasks,to avoid editing while repetion
                
                var tasks = service.GetTasks().ToList();

                // delete each task
                foreach (var task in tasks)
                {
                    service.DeleteTask(task.Id);
                }

                //refresh interface
                RefreshTasks();
                UpdateProgressBar();

                MessageBox.Show(
                    $"All {tasks.Count} tasks have been deleted.",
                    "Tasks Cleared",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }
    }
}
