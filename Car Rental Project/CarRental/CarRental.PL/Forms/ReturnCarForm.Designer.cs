namespace CarRental.CarRental.PL.Forms
{
    partial class ReturnCarForm
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
            dataGridView1 = new DataGridView();
            groupBox1 = new GroupBox();
            button2 = new Button();
            button1 = new Button();
            label7 = new Label();
            textBox3 = new TextBox();
            label6 = new Label();
            textBox2 = new TextBox();
            label5 = new Label();
            textBox1 = new TextBox();
            label4 = new Label();
            dateTimePicker1 = new DateTimePicker();
            label3 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(39, 30);
            label1.Name = "label1";
            label1.Size = new Size(233, 24);
            label1.TabIndex = 0;
            label1.Text = "Active Rental Contracts";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(39, 69);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(950, 240);
            dataGridView1.TabIndex = 1;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(textBox3);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(dateTimePicker1);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(39, 349);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1014, 323);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Return Details";
            // 
            // button2
            // 
            button2.BackColor = Color.LightSalmon;
            button2.Location = new Point(694, 179);
            button2.Name = "button2";
            button2.Size = new Size(168, 51);
            button2.TabIndex = 11;
            button2.Text = "Refresh";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.LightGreen;
            button1.Location = new Point(443, 179);
            button1.Name = "button1";
            button1.Size = new Size(168, 51);
            button1.TabIndex = 10;
            button1.Text = "Process Return";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(443, 94);
            label7.Name = "label7";
            label7.Size = new Size(565, 21);
            label7.TabIndex = 9;
            label7.Text = "Note: Add extra fees for late return, damage, or excess mileage";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(217, 264);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(150, 28);
            textBox3.TabIndex = 8;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(52, 267);
            label6.Name = "label6";
            label6.Size = new Size(159, 21);
            label6.TabIndex = 7;
            label6.Text = "Final Amount ($):";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(217, 200);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(150, 28);
            textBox2.TabIndex = 6;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(97, 200);
            label5.Name = "label5";
            label5.Size = new Size(104, 21);
            label5.TabIndex = 5;
            label5.Text = "Extra Fees";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(217, 141);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(150, 28);
            textBox1.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(49, 144);
            label4.Name = "label4";
            label4.Size = new Size(162, 21);
            label4.TabIndex = 3;
            label4.Text = "Original Price ($):";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(217, 88);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(159, 28);
            dateTimePicker1.TabIndex = 2;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(30, 94);
            label3.Name = "label3";
            label3.Size = new Size(181, 21);
            label3.TabIndex = 1;
            label3.Text = "Actual Return Date:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 41);
            label2.Name = "label2";
            label2.Size = new Size(231, 21);
            label2.TabIndex = 0;
            label2.Text = "Contract ID: Not Selected";
            // 
            // ReturnCarForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1081, 714);
            Controls.Add(groupBox1);
            Controls.Add(dataGridView1);
            Controls.Add(label1);
            Name = "ReturnCarForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Process Car Return";
            Load += ReturnCarForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView dataGridView1;
        private GroupBox groupBox1;
        private Button button1;
        private Label label7;
        private TextBox textBox3;
        private Label label6;
        private TextBox textBox2;
        private Label label5;
        private TextBox textBox1;
        private Label label4;
        private DateTimePicker dateTimePicker1;
        private Label label3;
        private Label label2;
        private Button button2;
    }
}