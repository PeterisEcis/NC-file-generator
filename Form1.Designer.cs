using System.Windows.Forms;

namespace NC_file_generator
{
    partial class Form1
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
            this.source = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.generate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.target = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // source
            // 
            this.source.Location = new System.Drawing.Point(311, 26);
            this.source.Name = "source";
            this.source.Size = new System.Drawing.Size(33, 27);
            this.source.TabIndex = 0;
            this.source.Text = "...";
            this.source.UseVisualStyleBackColor = true;
            this.source.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(34, 31);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(262, 22);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // generate
            // 
            this.generate.Location = new System.Drawing.Point(142, 309);
            this.generate.Name = "generate";
            this.generate.Size = new System.Drawing.Size(84, 23);
            this.generate.TabIndex = 2;
            this.generate.Text = "Generate";
            this.generate.UseVisualStyleBackColor = true;
            this.generate.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select folder with .nc files";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(34, 276);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(262, 22);
            this.textBox2.TabIndex = 4;
            // 
            // target
            // 
            this.target.Location = new System.Drawing.Point(311, 271);
            this.target.Name = "target";
            this.target.Size = new System.Drawing.Size(33, 27);
            this.target.TabIndex = 5;
            this.target.Text = "...";
            this.target.UseVisualStyleBackColor = true;
            this.target.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 256);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Select target folder (optional)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Found valid files:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(34, 86);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox3.Size = new System.Drawing.Size(262, 159);
            this.textBox3.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 344);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.target);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.generate);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.source);
            this.Name = "Form1";
            this.Text = "NC File Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button source;
        private TextBox textBox1;
        private Button generate;
        private Label label1;
        private TextBox textBox2;
        private Button target;
        private Label label2;
        private Label label3;
        private TextBox textBox3;
    }
}

