namespace _8085_Simulator
{
    partial class value_editbox
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
            this.label1 = new System.Windows.Forms.Label();
            this.value = new System.Windows.Forms.TextBox();
            this.type_box = new System.Windows.Forms.GroupBox();
            this.dec_flag = new System.Windows.Forms.RadioButton();
            this.hex_flag = new System.Windows.Forms.RadioButton();
            this.bin_flag = new System.Windows.Forms.RadioButton();
            this.set_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.type_box.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Set value as : ";
            // 
            // value
            // 
            this.value.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value.Location = new System.Drawing.Point(12, 40);
            this.value.MaxLength = 2;
            this.value.Name = "value";
            this.value.Size = new System.Drawing.Size(325, 51);
            this.value.TabIndex = 1;
            this.value.Text = "00";
            this.value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.value.TextChanged += new System.EventHandler(this.value_changed);
            this.value.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_keypress);
            // 
            // type_box
            // 
            this.type_box.Controls.Add(this.dec_flag);
            this.type_box.Controls.Add(this.hex_flag);
            this.type_box.Controls.Add(this.bin_flag);
            this.type_box.Location = new System.Drawing.Point(12, 145);
            this.type_box.Name = "type_box";
            this.type_box.Size = new System.Drawing.Size(325, 78);
            this.type_box.TabIndex = 2;
            this.type_box.TabStop = false;
            this.type_box.Text = "Type";
            // 
            // dec_flag
            // 
            this.dec_flag.AutoSize = true;
            this.dec_flag.Location = new System.Drawing.Point(211, 33);
            this.dec_flag.Name = "dec_flag";
            this.dec_flag.Size = new System.Drawing.Size(108, 32);
            this.dec_flag.TabIndex = 2;
            this.dec_flag.TabStop = true;
            this.dec_flag.Text = "DEC (10)";
            this.dec_flag.UseVisualStyleBackColor = true;
            this.dec_flag.CheckedChanged += new System.EventHandler(this.dec_flag_CheckedChanged);
            // 
            // hex_flag
            // 
            this.hex_flag.AutoSize = true;
            this.hex_flag.Checked = true;
            this.hex_flag.Location = new System.Drawing.Point(104, 33);
            this.hex_flag.Name = "hex_flag";
            this.hex_flag.Size = new System.Drawing.Size(108, 32);
            this.hex_flag.TabIndex = 1;
            this.hex_flag.TabStop = true;
            this.hex_flag.Text = "HEX (16)";
            this.hex_flag.UseVisualStyleBackColor = true;
            this.hex_flag.CheckedChanged += new System.EventHandler(this.hex_flag_CheckedChanged);
            // 
            // bin_flag
            // 
            this.bin_flag.AutoSize = true;
            this.bin_flag.Location = new System.Drawing.Point(6, 33);
            this.bin_flag.Name = "bin_flag";
            this.bin_flag.Size = new System.Drawing.Size(92, 32);
            this.bin_flag.TabIndex = 0;
            this.bin_flag.TabStop = true;
            this.bin_flag.Text = "BIN (2)";
            this.bin_flag.UseVisualStyleBackColor = true;
            this.bin_flag.CheckedChanged += new System.EventHandler(this.bin_flag_CheckedChanged);
            // 
            // set_button
            // 
            this.set_button.Location = new System.Drawing.Point(12, 97);
            this.set_button.Name = "set_button";
            this.set_button.Size = new System.Drawing.Size(107, 42);
            this.set_button.TabIndex = 3;
            this.set_button.Text = "Set";
            this.set_button.UseVisualStyleBackColor = true;
            this.set_button.Click += new System.EventHandler(this.set_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel_button.Location = new System.Drawing.Point(230, 97);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(107, 42);
            this.cancel_button.TabIndex = 4;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            // 
            // value_editbox
            // 
            this.AcceptButton = this.set_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel_button;
            this.ClientSize = new System.Drawing.Size(349, 233);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.set_button);
            this.Controls.Add(this.type_box);
            this.Controls.Add(this.value);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "value_editbox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "value_editbox";
            this.type_box.ResumeLayout(false);
            this.type_box.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox type_box;
        private System.Windows.Forms.RadioButton bin_flag;
        private System.Windows.Forms.Button set_button;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.RadioButton hex_flag;
        private System.Windows.Forms.RadioButton dec_flag;
        public System.Windows.Forms.TextBox value;
    }
}