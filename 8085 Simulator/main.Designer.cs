namespace _8085_Simulator
{
    partial class main
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
            this.button1 = new System.Windows.Forms.Button();
            this.albl = new System.Windows.Forms.Label();
            this.blbl = new System.Windows.Forms.Label();
            this.clbl = new System.Windows.Forms.Label();
            this.dlbl = new System.Windows.Forms.Label();
            this.elbl = new System.Windows.Forms.Label();
            this.ereg = new System.Windows.Forms.Label();
            this.dreg = new System.Windows.Forms.Label();
            this.creg = new System.Windows.Forms.Label();
            this.breg = new System.Windows.Forms.Label();
            this.areg = new System.Windows.Forms.Label();
            this.codeEditor = new System.Windows.Forms.RichTextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.lines_indicator = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(637, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "run code";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // albl
            // 
            this.albl.AutoSize = true;
            this.albl.Location = new System.Drawing.Point(12, 9);
            this.albl.Name = "albl";
            this.albl.Size = new System.Drawing.Size(33, 23);
            this.albl.TabIndex = 2;
            this.albl.Text = "a : ";
            // 
            // blbl
            // 
            this.blbl.AutoSize = true;
            this.blbl.Location = new System.Drawing.Point(12, 32);
            this.blbl.Name = "blbl";
            this.blbl.Size = new System.Drawing.Size(29, 23);
            this.blbl.TabIndex = 3;
            this.blbl.Text = "b :";
            // 
            // clbl
            // 
            this.clbl.AutoSize = true;
            this.clbl.Location = new System.Drawing.Point(12, 55);
            this.clbl.Name = "clbl";
            this.clbl.Size = new System.Drawing.Size(27, 23);
            this.clbl.TabIndex = 4;
            this.clbl.Text = "c :";
            // 
            // dlbl
            // 
            this.dlbl.AutoSize = true;
            this.dlbl.Location = new System.Drawing.Point(12, 78);
            this.dlbl.Name = "dlbl";
            this.dlbl.Size = new System.Drawing.Size(29, 23);
            this.dlbl.TabIndex = 5;
            this.dlbl.Text = "d :";
            // 
            // elbl
            // 
            this.elbl.AutoSize = true;
            this.elbl.Location = new System.Drawing.Point(12, 101);
            this.elbl.Name = "elbl";
            this.elbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.elbl.Size = new System.Drawing.Size(28, 23);
            this.elbl.TabIndex = 6;
            this.elbl.Text = "e :";
            // 
            // ereg
            // 
            this.ereg.AutoSize = true;
            this.ereg.Location = new System.Drawing.Point(86, 101);
            this.ereg.Name = "ereg";
            this.ereg.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ereg.Size = new System.Drawing.Size(19, 23);
            this.ereg.TabIndex = 11;
            this.ereg.Text = "0";
            // 
            // dreg
            // 
            this.dreg.AutoSize = true;
            this.dreg.Location = new System.Drawing.Point(86, 78);
            this.dreg.Name = "dreg";
            this.dreg.Size = new System.Drawing.Size(19, 23);
            this.dreg.TabIndex = 10;
            this.dreg.Text = "0";
            // 
            // creg
            // 
            this.creg.AutoSize = true;
            this.creg.Location = new System.Drawing.Point(86, 55);
            this.creg.Name = "creg";
            this.creg.Size = new System.Drawing.Size(19, 23);
            this.creg.TabIndex = 9;
            this.creg.Text = "0";
            // 
            // breg
            // 
            this.breg.AutoSize = true;
            this.breg.Location = new System.Drawing.Point(86, 32);
            this.breg.Name = "breg";
            this.breg.Size = new System.Drawing.Size(19, 23);
            this.breg.TabIndex = 8;
            this.breg.Text = "0";
            // 
            // areg
            // 
            this.areg.AutoSize = true;
            this.areg.Location = new System.Drawing.Point(86, 9);
            this.areg.Name = "areg";
            this.areg.Size = new System.Drawing.Size(19, 23);
            this.areg.TabIndex = 7;
            this.areg.Text = "0";
            // 
            // codeEditor
            // 
            this.codeEditor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.codeEditor.Location = new System.Drawing.Point(154, 12);
            this.codeEditor.Name = "codeEditor";
            this.codeEditor.Size = new System.Drawing.Size(477, 308);
            this.codeEditor.TabIndex = 12;
            this.codeEditor.Text = "";
            this.codeEditor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.code_editor_key_press);
            // 
            // listBox1
            // 
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 23;
            this.listBox1.Location = new System.Drawing.Point(154, 326);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(477, 92);
            this.listBox1.TabIndex = 13;
            // 
            // lines_indicator
            // 
            this.lines_indicator.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lines_indicator.Location = new System.Drawing.Point(111, 12);
            this.lines_indicator.Name = "lines_indicator";
            this.lines_indicator.ReadOnly = true;
            this.lines_indicator.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lines_indicator.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.lines_indicator.Size = new System.Drawing.Size(37, 308);
            this.lines_indicator.TabIndex = 14;
            this.lines_indicator.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 460);
            this.Controls.Add(this.lines_indicator);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.codeEditor);
            this.Controls.Add(this.ereg);
            this.Controls.Add(this.dreg);
            this.Controls.Add(this.creg);
            this.Controls.Add(this.breg);
            this.Controls.Add(this.areg);
            this.Controls.Add(this.elbl);
            this.Controls.Add(this.dlbl);
            this.Controls.Add(this.clbl);
            this.Controls.Add(this.blbl);
            this.Controls.Add(this.albl);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "8085 Simulator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label albl;
        private System.Windows.Forms.Label blbl;
        private System.Windows.Forms.Label clbl;
        private System.Windows.Forms.Label dlbl;
        private System.Windows.Forms.Label elbl;
        private System.Windows.Forms.Label ereg;
        private System.Windows.Forms.Label dreg;
        private System.Windows.Forms.Label creg;
        private System.Windows.Forms.Label breg;
        private System.Windows.Forms.Label areg;
        private System.Windows.Forms.RichTextBox codeEditor;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.RichTextBox lines_indicator;
    }
}

