namespace GUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            NumOfItemsText = new TextBox();
            label1 = new Label();
            SeedText = new TextBox();
            CapacityText = new TextBox();
            label2 = new Label();
            label3 = new Label();
            ResultTextBox = new TextBox();
            InstanceTextBox = new TextBox();
            label4 = new Label();
            label5 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(287, 938);
            button1.Name = "button1";
            button1.Size = new Size(150, 46);
            button1.TabIndex = 0;
            button1.Text = "Run";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // NumOfItemsText
            // 
            NumOfItemsText.ForeColor = SystemColors.WindowText;
            NumOfItemsText.Location = new Point(97, 79);
            NumOfItemsText.Name = "NumOfItemsText";
            NumOfItemsText.RightToLeft = RightToLeft.No;
            NumOfItemsText.Size = new Size(200, 39);
            NumOfItemsText.TabIndex = 1;
            NumOfItemsText.TextAlign = HorizontalAlignment.Right;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(95, 44);
            label1.Name = "label1";
            label1.Size = new Size(196, 32);
            label1.TabIndex = 2;
            label1.Text = "Number of items";
            // 
            // SeedText
            // 
            SeedText.Location = new Point(97, 179);
            SeedText.Name = "SeedText";
            SeedText.RightToLeft = RightToLeft.No;
            SeedText.Size = new Size(200, 39);
            SeedText.TabIndex = 3;
            SeedText.TextAlign = HorizontalAlignment.Right;
            // 
            // CapacityText
            // 
            CapacityText.Location = new Point(97, 276);
            CapacityText.Name = "CapacityText";
            CapacityText.Size = new Size(200, 39);
            CapacityText.TabIndex = 4;
            CapacityText.TextAlign = HorizontalAlignment.Right;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(95, 150);
            label2.Name = "label2";
            label2.Size = new Size(67, 32);
            label2.TabIndex = 5;
            label2.Text = "Seed";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(99, 246);
            label3.Name = "label3";
            label3.Size = new Size(104, 32);
            label3.TabIndex = 6;
            label3.Text = "Capacity";
            // 
            // ResultTextBox
            // 
            ResultTextBox.Location = new Point(55, 432);
            ResultTextBox.Multiline = true;
            ResultTextBox.Name = "ResultTextBox";
            ResultTextBox.ReadOnly = true;
            ResultTextBox.Size = new Size(220, 352);
            ResultTextBox.TabIndex = 7;
            // 
            // InstanceTextBox
            // 
            InstanceTextBox.Location = new Point(439, 131);
            InstanceTextBox.Multiline = true;
            InstanceTextBox.Name = "InstanceTextBox";
            InstanceTextBox.ReadOnly = true;
            InstanceTextBox.Size = new Size(336, 664);
            InstanceTextBox.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(55, 397);
            label4.Name = "label4";
            label4.Size = new Size(78, 32);
            label4.TabIndex = 10;
            label4.Text = "Result";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(441, 100);
            label5.Name = "label5";
            label5.Size = new Size(102, 32);
            label5.TabIndex = 11;
            label5.Text = "Instance";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(808, 1098);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(InstanceTextBox);
            Controls.Add(ResultTextBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(CapacityText);
            Controls.Add(SeedText);
            Controls.Add(label1);
            Controls.Add(NumOfItemsText);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Knapsack";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox NumOfItemsText;
        private Label label1;
        private TextBox SeedText;
        private TextBox CapacityText;
        private Label label2;
        private Label label3;
        private TextBox ResultTextBox;
        private TextBox InstanceTextBox;
        private Label label4;
        private Label label5;
    }
}
