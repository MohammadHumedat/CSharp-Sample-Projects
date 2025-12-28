namespace CarRental.CarRental.PL.Forms
{
    partial class RentalAgentDashboardForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            groupBox1 = new GroupBox();
            button5 = new Button();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.DarkBlue;
            label1.Location = new Point(30, 14);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(105, 22);
            label1.TabIndex = 0;
            label1.Text = "Welecome";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button5);
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button1);
            groupBox1.Font = new Font("Arial", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(30, 50);
            groupBox1.Margin = new Padding(2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(2);
            groupBox1.Size = new Size(389, 228);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Operations";
            // 
            // button5
            // 
            button5.BackColor = Color.IndianRed;
            button5.ForeColor = Color.Transparent;
            button5.Location = new Point(22, 164);
            button5.Margin = new Padding(2);
            button5.Name = "button5";
            button5.Size = new Size(78, 32);
            button5.TabIndex = 4;
            button5.Text = "Logout";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // button4
            // 
            button4.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button4.Location = new Point(204, 101);
            button4.Margin = new Padding(2);
            button4.Name = "button4";
            button4.Size = new Size(158, 42);
            button4.TabIndex = 3;
            button4.Text = "Return Car";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.Location = new Point(204, 34);
            button3.Margin = new Padding(2);
            button3.Name = "button3";
            button3.Size = new Size(158, 42);
            button3.TabIndex = 2;
            button3.Text = "View Available Cars";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.Location = new Point(22, 101);
            button2.Margin = new Padding(2);
            button2.Name = "button2";
            button2.Size = new Size(150, 42);
            button2.TabIndex = 1;
            button2.Text = "Create Rental";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(22, 34);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(150, 42);
            button1.TabIndex = 0;
            button1.Text = "ManageCustomers";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // RentalAgentDashboardForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(779, 409);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Margin = new Padding(2);
            Name = "RentalAgentDashboardForm";
            Text = "RentalAgentDashboardForm";
            Load += RentalAgentDashboardForm_Load;
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private GroupBox groupBox1;
        private Button button4;
        private Button button3;
        private Button button2;
        private Button button1;
        private Button button5;
    }
}