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
            this.output_box = new System.Windows.Forms.ListBox();
            this.lines_indicator = new System.Windows.Forms.RichTextBox();
            this.flagclbl = new System.Windows.Forms.Label();
            this.flagzlbl = new System.Windows.Forms.Label();
            this.flagslbl = new System.Windows.Forms.Label();
            this.flagalbl = new System.Windows.Forms.Label();
            this.flagplbl = new System.Windows.Forms.Label();
            this.flagp = new System.Windows.Forms.Label();
            this.flaga = new System.Windows.Forms.Label();
            this.flags = new System.Windows.Forms.Label();
            this.flagz = new System.Windows.Forms.Label();
            this.flagc = new System.Windows.Forms.Label();
            this.registers_box = new System.Windows.Forms.GroupBox();
            this.flag_box = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.register_layout = new System.Windows.Forms.TableLayoutPanel();
            this.flag_layout = new System.Windows.Forms.TableLayoutPanel();
            this.main_layout = new System.Windows.Forms.TableLayoutPanel();
            this.status_group_layout = new System.Windows.Forms.TableLayoutPanel();
            this.code_input_layout = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.registers_box.SuspendLayout();
            this.flag_box.SuspendLayout();
            this.register_layout.SuspendLayout();
            this.flag_layout.SuspendLayout();
            this.main_layout.SuspendLayout();
            this.status_group_layout.SuspendLayout();
            this.code_input_layout.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(954, 3);
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
            this.albl.BackColor = System.Drawing.SystemColors.Control;
            this.albl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.albl.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.albl.Location = new System.Drawing.Point(3, 0);
            this.albl.Name = "albl";
            this.albl.Size = new System.Drawing.Size(42, 26);
            this.albl.TabIndex = 2;
            this.albl.Text = "A";
            this.albl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // blbl
            // 
            this.blbl.AutoSize = true;
            this.blbl.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.blbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blbl.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blbl.Location = new System.Drawing.Point(3, 26);
            this.blbl.Name = "blbl";
            this.blbl.Size = new System.Drawing.Size(42, 26);
            this.blbl.TabIndex = 3;
            this.blbl.Text = "B";
            this.blbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // clbl
            // 
            this.clbl.AutoSize = true;
            this.clbl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.clbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbl.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbl.Location = new System.Drawing.Point(51, 26);
            this.clbl.Name = "clbl";
            this.clbl.Size = new System.Drawing.Size(43, 26);
            this.clbl.TabIndex = 4;
            this.clbl.Text = "C";
            this.clbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dlbl
            // 
            this.dlbl.AutoSize = true;
            this.dlbl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dlbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dlbl.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dlbl.Location = new System.Drawing.Point(3, 78);
            this.dlbl.Name = "dlbl";
            this.dlbl.Size = new System.Drawing.Size(42, 26);
            this.dlbl.TabIndex = 5;
            this.dlbl.Text = "D";
            this.dlbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // elbl
            // 
            this.elbl.AutoSize = true;
            this.elbl.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.elbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elbl.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.elbl.Location = new System.Drawing.Point(51, 78);
            this.elbl.Name = "elbl";
            this.elbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.elbl.Size = new System.Drawing.Size(43, 26);
            this.elbl.TabIndex = 6;
            this.elbl.Text = "E";
            this.elbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ereg
            // 
            this.ereg.AutoSize = true;
            this.ereg.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ereg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ereg.Location = new System.Drawing.Point(51, 104);
            this.ereg.Name = "ereg";
            this.ereg.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ereg.Size = new System.Drawing.Size(43, 26);
            this.ereg.TabIndex = 11;
            this.ereg.Text = "0";
            this.ereg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dreg
            // 
            this.dreg.AutoSize = true;
            this.dreg.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dreg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dreg.Location = new System.Drawing.Point(3, 104);
            this.dreg.Name = "dreg";
            this.dreg.Size = new System.Drawing.Size(42, 26);
            this.dreg.TabIndex = 10;
            this.dreg.Text = "0";
            this.dreg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // creg
            // 
            this.creg.AutoSize = true;
            this.creg.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.creg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.creg.Location = new System.Drawing.Point(51, 52);
            this.creg.Name = "creg";
            this.creg.Size = new System.Drawing.Size(43, 26);
            this.creg.TabIndex = 9;
            this.creg.Text = "0";
            this.creg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // breg
            // 
            this.breg.AutoSize = true;
            this.breg.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.breg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.breg.Location = new System.Drawing.Point(3, 52);
            this.breg.Name = "breg";
            this.breg.Size = new System.Drawing.Size(42, 26);
            this.breg.TabIndex = 8;
            this.breg.Text = "0";
            this.breg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // areg
            // 
            this.areg.AutoSize = true;
            this.areg.BackColor = System.Drawing.SystemColors.Control;
            this.areg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.areg.Location = new System.Drawing.Point(51, 0);
            this.areg.Name = "areg";
            this.areg.Size = new System.Drawing.Size(43, 26);
            this.areg.TabIndex = 7;
            this.areg.Text = "0";
            this.areg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // codeEditor
            // 
            this.codeEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.codeEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeEditor.Location = new System.Drawing.Point(41, 3);
            this.codeEditor.Name = "codeEditor";
            this.codeEditor.Size = new System.Drawing.Size(689, 587);
            this.codeEditor.TabIndex = 12;
            this.codeEditor.Text = "";
            this.codeEditor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.code_editor_key_press);
            // 
            // output_box
            // 
            this.output_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.output_box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.output_box.FormattingEnabled = true;
            this.output_box.ItemHeight = 23;
            this.output_box.Location = new System.Drawing.Point(3, 602);
            this.output_box.Name = "output_box";
            this.output_box.Size = new System.Drawing.Size(733, 144);
            this.output_box.TabIndex = 13;
            // 
            // lines_indicator
            // 
            this.lines_indicator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lines_indicator.Location = new System.Drawing.Point(3, 3);
            this.lines_indicator.Name = "lines_indicator";
            this.lines_indicator.ReadOnly = true;
            this.lines_indicator.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lines_indicator.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.lines_indicator.Size = new System.Drawing.Size(32, 587);
            this.lines_indicator.TabIndex = 14;
            this.lines_indicator.Text = "";
            // 
            // flagclbl
            // 
            this.flagclbl.AutoSize = true;
            this.flagclbl.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.flagclbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flagclbl.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flagclbl.Location = new System.Drawing.Point(3, 0);
            this.flagclbl.Name = "flagclbl";
            this.flagclbl.Size = new System.Drawing.Size(41, 24);
            this.flagclbl.TabIndex = 15;
            this.flagclbl.Text = "C";
            this.flagclbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flagzlbl
            // 
            this.flagzlbl.AutoSize = true;
            this.flagzlbl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flagzlbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flagzlbl.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flagzlbl.Location = new System.Drawing.Point(3, 24);
            this.flagzlbl.Name = "flagzlbl";
            this.flagzlbl.Size = new System.Drawing.Size(41, 24);
            this.flagzlbl.TabIndex = 16;
            this.flagzlbl.Text = "Z";
            this.flagzlbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flagslbl
            // 
            this.flagslbl.AutoSize = true;
            this.flagslbl.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.flagslbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flagslbl.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flagslbl.Location = new System.Drawing.Point(3, 48);
            this.flagslbl.Name = "flagslbl";
            this.flagslbl.Size = new System.Drawing.Size(41, 24);
            this.flagslbl.TabIndex = 17;
            this.flagslbl.Text = "S";
            this.flagslbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flagalbl
            // 
            this.flagalbl.AutoSize = true;
            this.flagalbl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flagalbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flagalbl.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flagalbl.Location = new System.Drawing.Point(3, 72);
            this.flagalbl.Name = "flagalbl";
            this.flagalbl.Size = new System.Drawing.Size(41, 24);
            this.flagalbl.TabIndex = 18;
            this.flagalbl.Text = "A";
            this.flagalbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flagplbl
            // 
            this.flagplbl.AutoSize = true;
            this.flagplbl.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.flagplbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flagplbl.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flagplbl.Location = new System.Drawing.Point(3, 96);
            this.flagplbl.Name = "flagplbl";
            this.flagplbl.Size = new System.Drawing.Size(41, 27);
            this.flagplbl.TabIndex = 19;
            this.flagplbl.Text = "P";
            this.flagplbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flagp
            // 
            this.flagp.AutoSize = true;
            this.flagp.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.flagp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flagp.Location = new System.Drawing.Point(50, 96);
            this.flagp.Name = "flagp";
            this.flagp.Size = new System.Drawing.Size(41, 27);
            this.flagp.TabIndex = 24;
            this.flagp.Text = "0";
            this.flagp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flaga
            // 
            this.flaga.AutoSize = true;
            this.flaga.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flaga.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flaga.Location = new System.Drawing.Point(50, 72);
            this.flaga.Name = "flaga";
            this.flaga.Size = new System.Drawing.Size(41, 24);
            this.flaga.TabIndex = 23;
            this.flaga.Text = "0";
            this.flaga.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flags
            // 
            this.flags.AutoSize = true;
            this.flags.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.flags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flags.Location = new System.Drawing.Point(50, 48);
            this.flags.Name = "flags";
            this.flags.Size = new System.Drawing.Size(41, 24);
            this.flags.TabIndex = 22;
            this.flags.Text = "0";
            this.flags.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flagz
            // 
            this.flagz.AutoSize = true;
            this.flagz.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flagz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flagz.Location = new System.Drawing.Point(50, 24);
            this.flagz.Name = "flagz";
            this.flagz.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.flagz.Size = new System.Drawing.Size(41, 24);
            this.flagz.TabIndex = 21;
            this.flagz.Text = "0";
            this.flagz.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flagc
            // 
            this.flagc.AutoSize = true;
            this.flagc.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.flagc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flagc.Location = new System.Drawing.Point(50, 0);
            this.flagc.Name = "flagc";
            this.flagc.Size = new System.Drawing.Size(41, 24);
            this.flagc.TabIndex = 20;
            this.flagc.Text = "0";
            this.flagc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // registers_box
            // 
            this.registers_box.Controls.Add(this.register_layout);
            this.registers_box.Location = new System.Drawing.Point(3, 3);
            this.registers_box.Name = "registers_box";
            this.registers_box.Size = new System.Drawing.Size(103, 218);
            this.registers_box.TabIndex = 25;
            this.registers_box.TabStop = false;
            this.registers_box.Text = "Registers";
            // 
            // flag_box
            // 
            this.flag_box.Controls.Add(this.flag_layout);
            this.flag_box.Location = new System.Drawing.Point(3, 227);
            this.flag_box.Name = "flag_box";
            this.flag_box.Size = new System.Drawing.Size(100, 152);
            this.flag_box.TabIndex = 26;
            this.flag_box.TabStop = false;
            this.flag_box.Text = "Flags";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 130);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(42, 26);
            this.label1.TabIndex = 12;
            this.label1.Text = "H";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 156);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(42, 33);
            this.label2.TabIndex = 13;
            this.label2.Text = "0";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(51, 130);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(43, 26);
            this.label3.TabIndex = 14;
            this.label3.Text = "L";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(51, 156);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(43, 33);
            this.label4.TabIndex = 15;
            this.label4.Text = "0";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // register_layout
            // 
            this.register_layout.ColumnCount = 2;
            this.register_layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.register_layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.register_layout.Controls.Add(this.albl, 0, 0);
            this.register_layout.Controls.Add(this.label2, 0, 6);
            this.register_layout.Controls.Add(this.label4, 1, 6);
            this.register_layout.Controls.Add(this.label3, 1, 5);
            this.register_layout.Controls.Add(this.areg, 1, 0);
            this.register_layout.Controls.Add(this.blbl, 0, 1);
            this.register_layout.Controls.Add(this.label1, 0, 5);
            this.register_layout.Controls.Add(this.clbl, 1, 1);
            this.register_layout.Controls.Add(this.breg, 0, 2);
            this.register_layout.Controls.Add(this.ereg, 1, 4);
            this.register_layout.Controls.Add(this.dreg, 0, 4);
            this.register_layout.Controls.Add(this.elbl, 1, 3);
            this.register_layout.Controls.Add(this.dlbl, 0, 3);
            this.register_layout.Controls.Add(this.creg, 1, 2);
            this.register_layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.register_layout.Location = new System.Drawing.Point(3, 26);
            this.register_layout.Name = "register_layout";
            this.register_layout.RowCount = 7;
            this.register_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.register_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.register_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.register_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.register_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.register_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.register_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.register_layout.Size = new System.Drawing.Size(97, 189);
            this.register_layout.TabIndex = 16;
            // 
            // flag_layout
            // 
            this.flag_layout.ColumnCount = 2;
            this.flag_layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.flag_layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.flag_layout.Controls.Add(this.flagp, 1, 4);
            this.flag_layout.Controls.Add(this.flagc, 1, 0);
            this.flag_layout.Controls.Add(this.flaga, 1, 3);
            this.flag_layout.Controls.Add(this.flagclbl, 0, 0);
            this.flag_layout.Controls.Add(this.flags, 1, 2);
            this.flag_layout.Controls.Add(this.flagzlbl, 0, 1);
            this.flag_layout.Controls.Add(this.flagz, 1, 1);
            this.flag_layout.Controls.Add(this.flagslbl, 0, 2);
            this.flag_layout.Controls.Add(this.flagalbl, 0, 3);
            this.flag_layout.Controls.Add(this.flagplbl, 0, 4);
            this.flag_layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flag_layout.Location = new System.Drawing.Point(3, 26);
            this.flag_layout.Name = "flag_layout";
            this.flag_layout.RowCount = 5;
            this.flag_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.flag_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.flag_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.flag_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.flag_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.flag_layout.Size = new System.Drawing.Size(94, 123);
            this.flag_layout.TabIndex = 27;
            // 
            // main_layout
            // 
            this.main_layout.ColumnCount = 3;
            this.main_layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.main_layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.98229F));
            this.main_layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.01771F));
            this.main_layout.Controls.Add(this.code_input_layout, 1, 0);
            this.main_layout.Controls.Add(this.status_group_layout, 0, 0);
            this.main_layout.Controls.Add(this.button1, 2, 0);
            this.main_layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main_layout.Location = new System.Drawing.Point(0, 0);
            this.main_layout.Name = "main_layout";
            this.main_layout.RowCount = 1;
            this.main_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.main_layout.Size = new System.Drawing.Size(1174, 755);
            this.main_layout.TabIndex = 27;
            // 
            // status_group_layout
            // 
            this.status_group_layout.ColumnCount = 1;
            this.status_group_layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.status_group_layout.Controls.Add(this.registers_box, 0, 0);
            this.status_group_layout.Controls.Add(this.flag_box, 0, 1);
            this.status_group_layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.status_group_layout.Location = new System.Drawing.Point(3, 3);
            this.status_group_layout.Name = "status_group_layout";
            this.status_group_layout.RowCount = 2;
            this.status_group_layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.status_group_layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.status_group_layout.Size = new System.Drawing.Size(200, 749);
            this.status_group_layout.TabIndex = 0;
            // 
            // code_input_layout
            // 
            this.code_input_layout.ColumnCount = 1;
            this.code_input_layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.code_input_layout.Controls.Add(this.output_box, 0, 1);
            this.code_input_layout.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.code_input_layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.code_input_layout.Location = new System.Drawing.Point(209, 3);
            this.code_input_layout.Name = "code_input_layout";
            this.code_input_layout.RowCount = 2;
            this.code_input_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.code_input_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.code_input_layout.Size = new System.Drawing.Size(739, 749);
            this.code_input_layout.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.21327F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 94.78673F));
            this.tableLayoutPanel1.Controls.Add(this.lines_indicator, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.codeEditor, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(733, 593);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 755);
            this.Controls.Add(this.main_layout);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "main";
            this.Text = "8085 Simulator";
            this.registers_box.ResumeLayout(false);
            this.flag_box.ResumeLayout(false);
            this.register_layout.ResumeLayout(false);
            this.register_layout.PerformLayout();
            this.flag_layout.ResumeLayout(false);
            this.flag_layout.PerformLayout();
            this.main_layout.ResumeLayout(false);
            this.status_group_layout.ResumeLayout(false);
            this.code_input_layout.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.ListBox output_box;
        private System.Windows.Forms.RichTextBox lines_indicator;
        private System.Windows.Forms.Label flagclbl;
        private System.Windows.Forms.Label flagzlbl;
        private System.Windows.Forms.Label flagslbl;
        private System.Windows.Forms.Label flagalbl;
        private System.Windows.Forms.Label flagplbl;
        private System.Windows.Forms.Label flagp;
        private System.Windows.Forms.Label flaga;
        private System.Windows.Forms.Label flags;
        private System.Windows.Forms.Label flagz;
        private System.Windows.Forms.Label flagc;
        private System.Windows.Forms.GroupBox registers_box;
        private System.Windows.Forms.GroupBox flag_box;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel register_layout;
        private System.Windows.Forms.TableLayoutPanel flag_layout;
        private System.Windows.Forms.TableLayoutPanel main_layout;
        private System.Windows.Forms.TableLayoutPanel code_input_layout;
        private System.Windows.Forms.TableLayoutPanel status_group_layout;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

