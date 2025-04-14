namespace ImageProcessing
{
    partial class ImageProcessing
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
            openFileDialog1 = new OpenFileDialog();
            button1 = new Button();
            OriginalPicture = new PictureBox();
            button2 = new Button();
            GreyScale = new PictureBox();
            Threshold = new PictureBox();
            Negative = new PictureBox();
            Mirroring = new PictureBox();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            textBox5 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)OriginalPicture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GreyScale).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Threshold).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Negative).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Mirroring).BeginInit();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Filter = "jpg files (*.jpg)|*.jpg";
            // 
            // button1
            // 
            button1.Location = new Point(83, 104);
            button1.Name = "button1";
            button1.Size = new Size(150, 46);
            button1.TabIndex = 0;
            button1.Text = "Load File";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // OriginalPicture
            // 
            OriginalPicture.Location = new Point(83, 220);
            OriginalPicture.Name = "OriginalPicture";
            OriginalPicture.Size = new Size(982, 752);
            OriginalPicture.SizeMode = PictureBoxSizeMode.AutoSize;
            OriginalPicture.TabIndex = 1;
            OriginalPicture.TabStop = false;
            // 
            // button2
            // 
            button2.Location = new Point(1075, 536);
            button2.Name = "button2";
            button2.Size = new Size(164, 148);
            button2.TabIndex = 2;
            button2.Text = "Parallel Process";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // GreyScale
            // 
            GreyScale.Location = new Point(1245, 88);
            GreyScale.Name = "GreyScale";
            GreyScale.Size = new Size(500, 500);
            GreyScale.SizeMode = PictureBoxSizeMode.StretchImage;
            GreyScale.TabIndex = 3;
            GreyScale.TabStop = false;
            // 
            // Threshold
            // 
            Threshold.Location = new Point(1245, 642);
            Threshold.Name = "Threshold";
            Threshold.Size = new Size(500, 500);
            Threshold.SizeMode = PictureBoxSizeMode.StretchImage;
            Threshold.TabIndex = 4;
            Threshold.TabStop = false;
            // 
            // Negative
            // 
            Negative.Location = new Point(1965, 88);
            Negative.Name = "Negative";
            Negative.Size = new Size(500, 500);
            Negative.SizeMode = PictureBoxSizeMode.StretchImage;
            Negative.TabIndex = 5;
            Negative.TabStop = false;
            // 
            // Mirroring
            // 
            Mirroring.Location = new Point(1965, 642);
            Mirroring.Name = "Mirroring";
            Mirroring.Size = new Size(500, 500);
            Mirroring.SizeMode = PictureBoxSizeMode.StretchImage;
            Mirroring.TabIndex = 6;
            Mirroring.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(83, 174);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(200, 39);
            textBox1.TabIndex = 7;
            textBox1.Text = "Original Picture";
            textBox1.Visible = false;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(1245, 43);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(200, 39);
            textBox2.TabIndex = 8;
            textBox2.Text = "GreyScale";
            textBox2.Visible = false;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(1245, 597);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(200, 39);
            textBox3.TabIndex = 9;
            textBox3.Text = "Thresholding";
            textBox3.Visible = false;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(1965, 597);
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.Size = new Size(200, 39);
            textBox4.TabIndex = 10;
            textBox4.Text = "Mirroring";
            textBox4.Visible = false;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(1965, 43);
            textBox5.Name = "textBox5";
            textBox5.ReadOnly = true;
            textBox5.Size = new Size(200, 39);
            textBox5.TabIndex = 11;
            textBox5.Text = "Negative";
            textBox5.Visible = false;
            // 
            // ImageProcessing
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2556, 1210);
            Controls.Add(textBox5);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(Mirroring);
            Controls.Add(Negative);
            Controls.Add(Threshold);
            Controls.Add(GreyScale);
            Controls.Add(button2);
            Controls.Add(OriginalPicture);
            Controls.Add(button1);
            Name = "ImageProcessing";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)OriginalPicture).EndInit();
            ((System.ComponentModel.ISupportInitialize)GreyScale).EndInit();
            ((System.ComponentModel.ISupportInitialize)Threshold).EndInit();
            ((System.ComponentModel.ISupportInitialize)Negative).EndInit();
            ((System.ComponentModel.ISupportInitialize)Mirroring).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OpenFileDialog openFileDialog1;
        private Button button1;
        private PictureBox OriginalPicture;
        private Button button2;
        private PictureBox GreyScale;
        private PictureBox Threshold;
        private PictureBox Negative;
        private PictureBox Mirroring;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;
    }
}
