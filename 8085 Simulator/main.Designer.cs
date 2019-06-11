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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
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
            this.output_box = new System.Windows.Forms.ListBox();
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
            this.register_layout = new System.Windows.Forms.TableLayoutPanel();
            this.hreg = new System.Windows.Forms.Label();
            this.lreg = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flag_box = new System.Windows.Forms.GroupBox();
            this.flag_layout = new System.Windows.Forms.TableLayoutPanel();
            this.main_layout = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.address_to_find = new System.Windows.Forms.TextBox();
            this.find_address = new System.Windows.Forms.Button();
            this.data_tabs = new System.Windows.Forms.TabControl();
            this.memoryTab = new System.Windows.Forms.TabPage();
            this.memorybox = new System.Windows.Forms.ListView();
            this.address = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.memoryData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.stackTab = new System.Windows.Forms.TabPage();
            this.stackbox = new System.Windows.Forms.ListView();
            this.index = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.stackData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.portTab = new System.Windows.Forms.TabPage();
            this.portbox = new System.Windows.Forms.ListView();
            this.portAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.portData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.code_input_layout = new System.Windows.Forms.TableLayoutPanel();
            this.code_editors = new System.Windows.Forms.TableLayoutPanel();
            this.codeEditor = new ScintillaNET.Scintilla();
            this.status_group_layout = new System.Windows.Forms.TableLayoutPanel();
            this.pcandsp = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.s = new System.Windows.Forms.Label();
            this.spreg = new System.Windows.Forms.Label();
            this.pcreg = new System.Windows.Forms.Label();
            this.mlbl = new System.Windows.Forms.Label();
            this.mreg = new System.Windows.Forms.Label();
            this.pswlbl = new System.Windows.Forms.Label();
            this.pswreg = new System.Windows.Forms.Label();
            this.conv_group_box = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.reg_name = new System.Windows.Forms.Label();
            this.conv_lbl = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newASMFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openASMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearMemoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearRegistersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepNextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.runToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.checkErrorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.play_button = new System.Windows.Forms.ToolStripButton();
            this.step_button = new System.Windows.Forms.ToolStripButton();
            this.stop_button = new System.Windows.Forms.ToolStripButton();
            this.registers_box.SuspendLayout();
            this.register_layout.SuspendLayout();
            this.flag_box.SuspendLayout();
            this.flag_layout.SuspendLayout();
            this.main_layout.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.data_tabs.SuspendLayout();
            this.memoryTab.SuspendLayout();
            this.stackTab.SuspendLayout();
            this.portTab.SuspendLayout();
            this.code_input_layout.SuspendLayout();
            this.code_editors.SuspendLayout();
            this.status_group_layout.SuspendLayout();
            this.pcandsp.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.conv_group_box.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // albl
            // 
            this.albl.AutoSize = true;
            this.albl.BackColor = System.Drawing.SystemColors.Control;
            this.albl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.albl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.albl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.albl.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.albl.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.albl.Location = new System.Drawing.Point(3, 0);
            this.albl.Name = "albl";
            this.albl.Size = new System.Drawing.Size(88, 26);
            this.albl.TabIndex = 2;
            this.albl.Text = "A";
            this.albl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // blbl
            // 
            this.blbl.AutoSize = true;
            this.blbl.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.blbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.blbl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.blbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blbl.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.blbl.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blbl.Location = new System.Drawing.Point(3, 26);
            this.blbl.Name = "blbl";
            this.blbl.Size = new System.Drawing.Size(88, 26);
            this.blbl.TabIndex = 3;
            this.blbl.Text = "B";
            this.blbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // clbl
            // 
            this.clbl.AutoSize = true;
            this.clbl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.clbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clbl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.clbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbl.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.clbl.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbl.Location = new System.Drawing.Point(97, 26);
            this.clbl.Name = "clbl";
            this.clbl.Size = new System.Drawing.Size(88, 26);
            this.clbl.TabIndex = 4;
            this.clbl.Text = "C";
            this.clbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dlbl
            // 
            this.dlbl.AutoSize = true;
            this.dlbl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dlbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dlbl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dlbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dlbl.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.dlbl.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dlbl.Location = new System.Drawing.Point(3, 78);
            this.dlbl.Name = "dlbl";
            this.dlbl.Size = new System.Drawing.Size(88, 26);
            this.dlbl.TabIndex = 5;
            this.dlbl.Text = "D";
            this.dlbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // elbl
            // 
            this.elbl.AutoSize = true;
            this.elbl.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.elbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.elbl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.elbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elbl.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.elbl.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.elbl.Location = new System.Drawing.Point(97, 78);
            this.elbl.Name = "elbl";
            this.elbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.elbl.Size = new System.Drawing.Size(88, 26);
            this.elbl.TabIndex = 6;
            this.elbl.Text = "E";
            this.elbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ereg
            // 
            this.ereg.AutoSize = true;
            this.ereg.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ereg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ereg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ereg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ereg.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ereg.Location = new System.Drawing.Point(97, 104);
            this.ereg.Name = "ereg";
            this.ereg.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ereg.Size = new System.Drawing.Size(88, 26);
            this.ereg.TabIndex = 11;
            this.ereg.Tag = "E Register";
            this.ereg.Text = "0";
            this.ereg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ereg.MouseHover += new System.EventHandler(this.show_conv_tooltip);
            // 
            // dreg
            // 
            this.dreg.AutoSize = true;
            this.dreg.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dreg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dreg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dreg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dreg.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.dreg.Location = new System.Drawing.Point(3, 104);
            this.dreg.Name = "dreg";
            this.dreg.Size = new System.Drawing.Size(88, 26);
            this.dreg.TabIndex = 10;
            this.dreg.Tag = "D Register";
            this.dreg.Text = "0";
            this.dreg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dreg.MouseHover += new System.EventHandler(this.show_conv_tooltip);
            // 
            // creg
            // 
            this.creg.AutoSize = true;
            this.creg.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.creg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.creg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.creg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.creg.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.creg.Location = new System.Drawing.Point(97, 52);
            this.creg.Name = "creg";
            this.creg.Size = new System.Drawing.Size(88, 26);
            this.creg.TabIndex = 9;
            this.creg.Tag = "C Register";
            this.creg.Text = "0";
            this.creg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.creg.MouseHover += new System.EventHandler(this.show_conv_tooltip);
            // 
            // breg
            // 
            this.breg.AutoSize = true;
            this.breg.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.breg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.breg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.breg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.breg.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.breg.Location = new System.Drawing.Point(3, 52);
            this.breg.Name = "breg";
            this.breg.Size = new System.Drawing.Size(88, 26);
            this.breg.TabIndex = 8;
            this.breg.Tag = "B Register";
            this.breg.Text = "0";
            this.breg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.breg.MouseHover += new System.EventHandler(this.show_conv_tooltip);
            // 
            // areg
            // 
            this.areg.AutoSize = true;
            this.areg.BackColor = System.Drawing.SystemColors.Control;
            this.areg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.areg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.areg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.areg.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.areg.Location = new System.Drawing.Point(97, 0);
            this.areg.Name = "areg";
            this.areg.Size = new System.Drawing.Size(88, 26);
            this.areg.TabIndex = 7;
            this.areg.Tag = "A Register";
            this.areg.Text = "0";
            this.areg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.areg.MouseHover += new System.EventHandler(this.show_conv_tooltip);
            // 
            // output_box
            // 
            this.output_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.output_box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.output_box.FormattingEnabled = true;
            this.output_box.ItemHeight = 23;
            this.output_box.Location = new System.Drawing.Point(3, 535);
            this.output_box.Name = "output_box";
            this.output_box.Size = new System.Drawing.Size(739, 128);
            this.output_box.TabIndex = 13;
            // 
            // flagclbl
            // 
            this.flagclbl.AutoSize = true;
            this.flagclbl.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.flagclbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flagclbl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flagclbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flagclbl.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.flagclbl.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flagclbl.Location = new System.Drawing.Point(3, 96);
            this.flagclbl.Name = "flagclbl";
            this.flagclbl.Size = new System.Drawing.Size(88, 27);
            this.flagclbl.TabIndex = 15;
            this.flagclbl.Text = "C";
            this.flagclbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flagzlbl
            // 
            this.flagzlbl.AutoSize = true;
            this.flagzlbl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flagzlbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flagzlbl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flagzlbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flagzlbl.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.flagzlbl.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flagzlbl.Location = new System.Drawing.Point(3, 24);
            this.flagzlbl.Name = "flagzlbl";
            this.flagzlbl.Size = new System.Drawing.Size(88, 24);
            this.flagzlbl.TabIndex = 16;
            this.flagzlbl.Text = "Z";
            this.flagzlbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flagslbl
            // 
            this.flagslbl.AutoSize = true;
            this.flagslbl.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.flagslbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flagslbl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flagslbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flagslbl.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.flagslbl.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flagslbl.Location = new System.Drawing.Point(3, 0);
            this.flagslbl.Name = "flagslbl";
            this.flagslbl.Size = new System.Drawing.Size(88, 24);
            this.flagslbl.TabIndex = 17;
            this.flagslbl.Text = "S";
            this.flagslbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flagalbl
            // 
            this.flagalbl.AutoSize = true;
            this.flagalbl.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.flagalbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flagalbl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flagalbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flagalbl.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.flagalbl.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flagalbl.Location = new System.Drawing.Point(3, 48);
            this.flagalbl.Name = "flagalbl";
            this.flagalbl.Size = new System.Drawing.Size(88, 24);
            this.flagalbl.TabIndex = 18;
            this.flagalbl.Text = "A";
            this.flagalbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flagplbl
            // 
            this.flagplbl.AutoSize = true;
            this.flagplbl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flagplbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flagplbl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flagplbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flagplbl.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.flagplbl.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flagplbl.Location = new System.Drawing.Point(3, 72);
            this.flagplbl.Name = "flagplbl";
            this.flagplbl.Size = new System.Drawing.Size(88, 24);
            this.flagplbl.TabIndex = 19;
            this.flagplbl.Text = "P";
            this.flagplbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flagp
            // 
            this.flagp.AutoSize = true;
            this.flagp.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flagp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flagp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flagp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flagp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.flagp.Location = new System.Drawing.Point(97, 72);
            this.flagp.Name = "flagp";
            this.flagp.Size = new System.Drawing.Size(88, 24);
            this.flagp.TabIndex = 24;
            this.flagp.Text = "0";
            this.flagp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flaga
            // 
            this.flaga.AutoSize = true;
            this.flaga.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.flaga.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flaga.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flaga.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flaga.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.flaga.Location = new System.Drawing.Point(97, 48);
            this.flaga.Name = "flaga";
            this.flaga.Size = new System.Drawing.Size(88, 24);
            this.flaga.TabIndex = 23;
            this.flaga.Text = "0";
            this.flaga.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flags
            // 
            this.flags.AutoSize = true;
            this.flags.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.flags.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flags.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flags.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.flags.Location = new System.Drawing.Point(97, 0);
            this.flags.Name = "flags";
            this.flags.Size = new System.Drawing.Size(88, 24);
            this.flags.TabIndex = 22;
            this.flags.Text = "0";
            this.flags.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flagz
            // 
            this.flagz.AutoSize = true;
            this.flagz.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flagz.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flagz.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flagz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flagz.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.flagz.Location = new System.Drawing.Point(97, 24);
            this.flagz.Name = "flagz";
            this.flagz.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.flagz.Size = new System.Drawing.Size(88, 24);
            this.flagz.TabIndex = 21;
            this.flagz.Text = "0";
            this.flagz.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flagc
            // 
            this.flagc.AutoSize = true;
            this.flagc.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.flagc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flagc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flagc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flagc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.flagc.Location = new System.Drawing.Point(97, 96);
            this.flagc.Name = "flagc";
            this.flagc.Size = new System.Drawing.Size(88, 27);
            this.flagc.TabIndex = 20;
            this.flagc.Text = "0";
            this.flagc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // registers_box
            // 
            this.registers_box.Controls.Add(this.register_layout);
            this.registers_box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.registers_box.Location = new System.Drawing.Point(3, 3);
            this.registers_box.Name = "registers_box";
            this.registers_box.Size = new System.Drawing.Size(194, 218);
            this.registers_box.TabIndex = 25;
            this.registers_box.TabStop = false;
            this.registers_box.Text = "Registers";
            // 
            // register_layout
            // 
            this.register_layout.ColumnCount = 2;
            this.register_layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.register_layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.register_layout.Controls.Add(this.albl, 0, 0);
            this.register_layout.Controls.Add(this.hreg, 0, 6);
            this.register_layout.Controls.Add(this.lreg, 1, 6);
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
            this.register_layout.Size = new System.Drawing.Size(188, 189);
            this.register_layout.TabIndex = 16;
            // 
            // hreg
            // 
            this.hreg.AutoSize = true;
            this.hreg.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.hreg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hreg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hreg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hreg.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.hreg.Location = new System.Drawing.Point(3, 156);
            this.hreg.Name = "hreg";
            this.hreg.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.hreg.Size = new System.Drawing.Size(88, 33);
            this.hreg.TabIndex = 13;
            this.hreg.Tag = "H Register";
            this.hreg.Text = "0";
            this.hreg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.hreg.MouseHover += new System.EventHandler(this.show_conv_tooltip);
            // 
            // lreg
            // 
            this.lreg.AutoSize = true;
            this.lreg.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lreg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lreg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lreg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lreg.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lreg.Location = new System.Drawing.Point(97, 156);
            this.lreg.Name = "lreg";
            this.lreg.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lreg.Size = new System.Drawing.Size(88, 33);
            this.lreg.TabIndex = 15;
            this.lreg.Tag = "L Register";
            this.lreg.Text = "0";
            this.lreg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lreg.MouseHover += new System.EventHandler(this.show_conv_tooltip);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(97, 130);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(88, 26);
            this.label3.TabIndex = 14;
            this.label3.Text = "L";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 130);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(88, 26);
            this.label1.TabIndex = 12;
            this.label1.Text = "H";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flag_box
            // 
            this.flag_box.Controls.Add(this.flag_layout);
            this.flag_box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flag_box.Location = new System.Drawing.Point(3, 227);
            this.flag_box.Name = "flag_box";
            this.flag_box.Size = new System.Drawing.Size(194, 152);
            this.flag_box.TabIndex = 26;
            this.flag_box.TabStop = false;
            this.flag_box.Text = "Flags";
            // 
            // flag_layout
            // 
            this.flag_layout.ColumnCount = 2;
            this.flag_layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.flag_layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.flag_layout.Controls.Add(this.flagzlbl, 0, 1);
            this.flag_layout.Controls.Add(this.flagc, 1, 4);
            this.flag_layout.Controls.Add(this.flagz, 1, 1);
            this.flag_layout.Controls.Add(this.flagclbl, 0, 4);
            this.flag_layout.Controls.Add(this.flagslbl, 0, 0);
            this.flag_layout.Controls.Add(this.flags, 1, 0);
            this.flag_layout.Controls.Add(this.flagalbl, 0, 2);
            this.flag_layout.Controls.Add(this.flaga, 1, 2);
            this.flag_layout.Controls.Add(this.flagplbl, 0, 3);
            this.flag_layout.Controls.Add(this.flagp, 1, 3);
            this.flag_layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flag_layout.Location = new System.Drawing.Point(3, 26);
            this.flag_layout.Name = "flag_layout";
            this.flag_layout.RowCount = 5;
            this.flag_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.flag_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.flag_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.flag_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.flag_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.flag_layout.Size = new System.Drawing.Size(188, 123);
            this.flag_layout.TabIndex = 27;
            // 
            // main_layout
            // 
            this.main_layout.AutoSize = true;
            this.main_layout.ColumnCount = 3;
            this.main_layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.main_layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.2683F));
            this.main_layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.73171F));
            this.main_layout.Controls.Add(this.tableLayoutPanel3, 2, 0);
            this.main_layout.Controls.Add(this.code_input_layout, 1, 0);
            this.main_layout.Controls.Add(this.status_group_layout, 0, 0);
            this.main_layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main_layout.Location = new System.Drawing.Point(0, 28);
            this.main_layout.Name = "main_layout";
            this.main_layout.RowCount = 1;
            this.main_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.main_layout.Size = new System.Drawing.Size(1179, 672);
            this.main_layout.TabIndex = 27;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.data_tabs, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(960, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(216, 666);
            this.tableLayoutPanel3.TabIndex = 28;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.35714F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.64286F));
            this.tableLayoutPanel4.Controls.Add(this.address_to_find, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.find_address, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(210, 41);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // address_to_find
            // 
            this.address_to_find.Dock = System.Windows.Forms.DockStyle.Fill;
            this.address_to_find.Location = new System.Drawing.Point(3, 3);
            this.address_to_find.Multiline = true;
            this.address_to_find.Name = "address_to_find";
            this.address_to_find.Size = new System.Drawing.Size(120, 35);
            this.address_to_find.TabIndex = 0;
            this.address_to_find.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.find_text_keypress);
            // 
            // find_address
            // 
            this.find_address.Dock = System.Windows.Forms.DockStyle.Fill;
            this.find_address.Location = new System.Drawing.Point(129, 3);
            this.find_address.Name = "find_address";
            this.find_address.Size = new System.Drawing.Size(78, 35);
            this.find_address.TabIndex = 1;
            this.find_address.Text = "Find";
            this.find_address.UseVisualStyleBackColor = true;
            this.find_address.Click += new System.EventHandler(this.find_address_Click);
            // 
            // data_tabs
            // 
            this.data_tabs.Controls.Add(this.memoryTab);
            this.data_tabs.Controls.Add(this.stackTab);
            this.data_tabs.Controls.Add(this.portTab);
            this.data_tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.data_tabs.Location = new System.Drawing.Point(3, 50);
            this.data_tabs.Name = "data_tabs";
            this.data_tabs.SelectedIndex = 0;
            this.data_tabs.Size = new System.Drawing.Size(210, 613);
            this.data_tabs.TabIndex = 4;
            // 
            // memoryTab
            // 
            this.memoryTab.Controls.Add(this.memorybox);
            this.memoryTab.Location = new System.Drawing.Point(4, 32);
            this.memoryTab.Name = "memoryTab";
            this.memoryTab.Padding = new System.Windows.Forms.Padding(3);
            this.memoryTab.Size = new System.Drawing.Size(202, 577);
            this.memoryTab.TabIndex = 0;
            this.memoryTab.Text = "Memory";
            this.memoryTab.UseVisualStyleBackColor = true;
            // 
            // memorybox
            // 
            this.memorybox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.memorybox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.address,
            this.memoryData});
            this.memorybox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memorybox.HideSelection = false;
            this.memorybox.Location = new System.Drawing.Point(3, 3);
            this.memorybox.Name = "memorybox";
            this.memorybox.Size = new System.Drawing.Size(196, 571);
            this.memorybox.TabIndex = 2;
            this.memorybox.UseCompatibleStateImageBehavior = false;
            this.memorybox.View = System.Windows.Forms.View.Details;
            this.memorybox.VirtualListSize = 255;
            this.memorybox.VirtualMode = true;
            this.memorybox.CacheVirtualItems += new System.Windows.Forms.CacheVirtualItemsEventHandler(this.memory_cache_items);
            this.memorybox.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.memory_retrieve_items);
            this.memorybox.SearchForVirtualItem += new System.Windows.Forms.SearchForVirtualItemEventHandler(this.memory_search_item);
            this.memorybox.DoubleClick += new System.EventHandler(this.memory_double_click);
            // 
            // address
            // 
            this.address.Text = "Address";
            this.address.Width = 88;
            // 
            // memoryData
            // 
            this.memoryData.Text = "Data";
            this.memoryData.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.memoryData.Width = 100;
            // 
            // stackTab
            // 
            this.stackTab.Controls.Add(this.stackbox);
            this.stackTab.Location = new System.Drawing.Point(4, 32);
            this.stackTab.Name = "stackTab";
            this.stackTab.Padding = new System.Windows.Forms.Padding(3);
            this.stackTab.Size = new System.Drawing.Size(202, 577);
            this.stackTab.TabIndex = 1;
            this.stackTab.Text = "Stack";
            this.stackTab.UseVisualStyleBackColor = true;
            // 
            // stackbox
            // 
            this.stackbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.stackbox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.index,
            this.stackData});
            this.stackbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stackbox.HideSelection = false;
            this.stackbox.Location = new System.Drawing.Point(3, 3);
            this.stackbox.Name = "stackbox";
            this.stackbox.Size = new System.Drawing.Size(196, 571);
            this.stackbox.TabIndex = 0;
            this.stackbox.UseCompatibleStateImageBehavior = false;
            this.stackbox.View = System.Windows.Forms.View.Details;
            this.stackbox.VirtualListSize = 255;
            this.stackbox.VirtualMode = true;
            this.stackbox.CacheVirtualItems += new System.Windows.Forms.CacheVirtualItemsEventHandler(this.stack_cache_items);
            this.stackbox.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.stack_retrieve_items);
            // 
            // index
            // 
            this.index.Text = "Index";
            this.index.Width = 86;
            // 
            // stackData
            // 
            this.stackData.Text = "Data";
            this.stackData.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.stackData.Width = 100;
            // 
            // portTab
            // 
            this.portTab.Controls.Add(this.portbox);
            this.portTab.Location = new System.Drawing.Point(4, 32);
            this.portTab.Name = "portTab";
            this.portTab.Size = new System.Drawing.Size(202, 577);
            this.portTab.TabIndex = 2;
            this.portTab.Text = "Port";
            this.portTab.UseVisualStyleBackColor = true;
            // 
            // portbox
            // 
            this.portbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.portbox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.portAddress,
            this.portData});
            this.portbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.portbox.HideSelection = false;
            this.portbox.Location = new System.Drawing.Point(0, 0);
            this.portbox.Name = "portbox";
            this.portbox.Size = new System.Drawing.Size(202, 577);
            this.portbox.TabIndex = 0;
            this.portbox.UseCompatibleStateImageBehavior = false;
            this.portbox.View = System.Windows.Forms.View.Details;
            this.portbox.VirtualListSize = 255;
            this.portbox.VirtualMode = true;
            this.portbox.CacheVirtualItems += new System.Windows.Forms.CacheVirtualItemsEventHandler(this.port_cache_items);
            this.portbox.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.port_retrieve_items);
            // 
            // portAddress
            // 
            this.portAddress.Text = "Address";
            this.portAddress.Width = 90;
            // 
            // portData
            // 
            this.portData.Text = "Data";
            this.portData.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.portData.Width = 100;
            // 
            // code_input_layout
            // 
            this.code_input_layout.ColumnCount = 1;
            this.code_input_layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.code_input_layout.Controls.Add(this.output_box, 0, 1);
            this.code_input_layout.Controls.Add(this.code_editors, 0, 0);
            this.code_input_layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.code_input_layout.Location = new System.Drawing.Point(209, 3);
            this.code_input_layout.Name = "code_input_layout";
            this.code_input_layout.RowCount = 2;
            this.code_input_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.code_input_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.code_input_layout.Size = new System.Drawing.Size(745, 666);
            this.code_input_layout.TabIndex = 1;
            // 
            // code_editors
            // 
            this.code_editors.ColumnCount = 1;
            this.code_editors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.code_editors.Controls.Add(this.codeEditor, 0, 0);
            this.code_editors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.code_editors.Location = new System.Drawing.Point(3, 3);
            this.code_editors.Name = "code_editors";
            this.code_editors.RowCount = 1;
            this.code_editors.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.code_editors.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.code_editors.Size = new System.Drawing.Size(739, 526);
            this.code_editors.TabIndex = 14;
            // 
            // codeEditor
            // 
            this.codeEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.codeEditor.CaretWidth = 2;
            this.codeEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeEditor.EdgeMode = ScintillaNET.EdgeMode.Line;
            this.codeEditor.Location = new System.Drawing.Point(3, 3);
            this.codeEditor.Name = "codeEditor";
            this.codeEditor.Size = new System.Drawing.Size(733, 520);
            this.codeEditor.TabIndex = 30;
            this.codeEditor.Text = ";8085 Simulator ALPHA state!\r\n;Developed by Vishal Solanki\r\n\r\njmp start\r\n\r\n\r\nstar" +
    "t: nop\r\n\r\n\r\n\r\nhlt";
            this.codeEditor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.code_editor_keypress);
            // 
            // status_group_layout
            // 
            this.status_group_layout.AutoSize = true;
            this.status_group_layout.ColumnCount = 1;
            this.status_group_layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.status_group_layout.Controls.Add(this.registers_box, 0, 0);
            this.status_group_layout.Controls.Add(this.flag_box, 0, 1);
            this.status_group_layout.Controls.Add(this.pcandsp, 0, 2);
            this.status_group_layout.Controls.Add(this.conv_group_box, 0, 3);
            this.status_group_layout.Dock = System.Windows.Forms.DockStyle.Top;
            this.status_group_layout.Location = new System.Drawing.Point(3, 3);
            this.status_group_layout.Name = "status_group_layout";
            this.status_group_layout.RowCount = 4;
            this.status_group_layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.status_group_layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.status_group_layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.status_group_layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.status_group_layout.Size = new System.Drawing.Size(200, 666);
            this.status_group_layout.TabIndex = 0;
            // 
            // pcandsp
            // 
            this.pcandsp.Controls.Add(this.tableLayoutPanel2);
            this.pcandsp.Dock = System.Windows.Forms.DockStyle.Top;
            this.pcandsp.Location = new System.Drawing.Point(3, 385);
            this.pcandsp.Name = "pcandsp";
            this.pcandsp.Size = new System.Drawing.Size(194, 160);
            this.pcandsp.TabIndex = 27;
            this.pcandsp.TabStop = false;
            this.pcandsp.Text = "16 bit Registers";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.s, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.spreg, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.pcreg, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.mlbl, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.mreg, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.pswlbl, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.pswreg, 1, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 26);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(188, 131);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 32);
            this.label2.TabIndex = 0;
            this.label2.Text = "PC";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // s
            // 
            this.s.AutoSize = true;
            this.s.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.s.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.s.Cursor = System.Windows.Forms.Cursors.Hand;
            this.s.Dock = System.Windows.Forms.DockStyle.Fill;
            this.s.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.s.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.s.Location = new System.Drawing.Point(3, 32);
            this.s.Name = "s";
            this.s.Size = new System.Drawing.Size(88, 32);
            this.s.TabIndex = 1;
            this.s.Text = "SP";
            this.s.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // spreg
            // 
            this.spreg.AutoSize = true;
            this.spreg.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.spreg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spreg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.spreg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spreg.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.spreg.Location = new System.Drawing.Point(97, 32);
            this.spreg.Name = "spreg";
            this.spreg.Size = new System.Drawing.Size(88, 32);
            this.spreg.TabIndex = 2;
            this.spreg.Tag = "Stack";
            this.spreg.Text = "0";
            this.spreg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pcreg
            // 
            this.pcreg.AutoSize = true;
            this.pcreg.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pcreg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pcreg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pcreg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcreg.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.pcreg.Location = new System.Drawing.Point(97, 0);
            this.pcreg.Name = "pcreg";
            this.pcreg.Size = new System.Drawing.Size(88, 32);
            this.pcreg.TabIndex = 3;
            this.pcreg.Tag = "Program Counter";
            this.pcreg.Text = "0";
            this.pcreg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mlbl
            // 
            this.mlbl.AutoSize = true;
            this.mlbl.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.mlbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mlbl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mlbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mlbl.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.mlbl.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlbl.Location = new System.Drawing.Point(3, 64);
            this.mlbl.Name = "mlbl";
            this.mlbl.Size = new System.Drawing.Size(88, 32);
            this.mlbl.TabIndex = 4;
            this.mlbl.Text = "M";
            this.mlbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mreg
            // 
            this.mreg.AutoSize = true;
            this.mreg.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.mreg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mreg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mreg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mreg.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.mreg.Location = new System.Drawing.Point(97, 64);
            this.mreg.Name = "mreg";
            this.mreg.Size = new System.Drawing.Size(88, 32);
            this.mreg.TabIndex = 5;
            this.mreg.Tag = "M Register";
            this.mreg.Text = "0";
            this.mreg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pswlbl
            // 
            this.pswlbl.AutoSize = true;
            this.pswlbl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pswlbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pswlbl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pswlbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pswlbl.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.pswlbl.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pswlbl.Location = new System.Drawing.Point(3, 96);
            this.pswlbl.Name = "pswlbl";
            this.pswlbl.Size = new System.Drawing.Size(88, 35);
            this.pswlbl.TabIndex = 6;
            this.pswlbl.Text = "PSW";
            this.pswlbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pswreg
            // 
            this.pswreg.AutoSize = true;
            this.pswreg.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pswreg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pswreg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pswreg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pswreg.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.pswreg.Location = new System.Drawing.Point(97, 96);
            this.pswreg.Name = "pswreg";
            this.pswreg.Size = new System.Drawing.Size(88, 35);
            this.pswreg.TabIndex = 7;
            this.pswreg.Tag = "Processor Status Word";
            this.pswreg.Text = "0";
            this.pswreg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // conv_group_box
            // 
            this.conv_group_box.AutoSize = true;
            this.conv_group_box.Controls.Add(this.flowLayoutPanel1);
            this.conv_group_box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conv_group_box.Location = new System.Drawing.Point(3, 551);
            this.conv_group_box.Name = "conv_group_box";
            this.conv_group_box.Size = new System.Drawing.Size(194, 112);
            this.conv_group_box.TabIndex = 28;
            this.conv_group_box.TabStop = false;
            this.conv_group_box.Text = "Conversion";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.reg_name);
            this.flowLayoutPanel1.Controls.Add(this.conv_lbl);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 26);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(188, 83);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // reg_name
            // 
            this.reg_name.AutoSize = true;
            this.reg_name.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reg_name.Location = new System.Drawing.Point(3, 0);
            this.reg_name.Name = "reg_name";
            this.reg_name.Size = new System.Drawing.Size(100, 23);
            this.reg_name.TabIndex = 0;
            this.reg_name.Text = "<Register>";
            // 
            // conv_lbl
            // 
            this.conv_lbl.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.conv_lbl, true);
            this.conv_lbl.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conv_lbl.Location = new System.Drawing.Point(3, 23);
            this.conv_lbl.Name = "conv_lbl";
            this.conv_lbl.Size = new System.Drawing.Size(126, 60);
            this.conv_lbl.TabIndex = 1;
            this.conv_lbl.Text = "BIN : <value>\r\nHEX : <value>\r\nDEC : <value>";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolToolStripMenuItem,
            this.runToolStripMenuItem,
            this.formatToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1179, 28);
            this.menuStrip1.TabIndex = 28;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newASMFileToolStripMenuItem,
            this.openASMToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newASMFileToolStripMenuItem
            // 
            this.newASMFileToolStripMenuItem.Name = "newASMFileToolStripMenuItem";
            this.newASMFileToolStripMenuItem.Size = new System.Drawing.Size(155, 26);
            this.newASMFileToolStripMenuItem.Text = "New ASM";
            // 
            // openASMToolStripMenuItem
            // 
            this.openASMToolStripMenuItem.Name = "openASMToolStripMenuItem";
            this.openASMToolStripMenuItem.Size = new System.Drawing.Size(155, 26);
            this.openASMToolStripMenuItem.Text = "Open ASM";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(152, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(155, 26);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(155, 26);
            this.saveAsToolStripMenuItem.Text = "Save As";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(152, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(155, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // toolToolStripMenuItem
            // 
            this.toolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearMemoryToolStripMenuItem,
            this.clearRegistersToolStripMenuItem,
            this.clearEditorToolStripMenuItem});
            this.toolToolStripMenuItem.Name = "toolToolStripMenuItem";
            this.toolToolStripMenuItem.Size = new System.Drawing.Size(56, 24);
            this.toolToolStripMenuItem.Text = "Tools";
            // 
            // clearMemoryToolStripMenuItem
            // 
            this.clearMemoryToolStripMenuItem.Name = "clearMemoryToolStripMenuItem";
            this.clearMemoryToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.clearMemoryToolStripMenuItem.Text = "Clear Memory";
            this.clearMemoryToolStripMenuItem.Click += new System.EventHandler(this.clearMemoryToolStripMenuItem_Click);
            // 
            // clearRegistersToolStripMenuItem
            // 
            this.clearRegistersToolStripMenuItem.Name = "clearRegistersToolStripMenuItem";
            this.clearRegistersToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.clearRegistersToolStripMenuItem.Text = "Clear Registers";
            this.clearRegistersToolStripMenuItem.Click += new System.EventHandler(this.clearRegistersToolStripMenuItem_Click);
            // 
            // clearEditorToolStripMenuItem
            // 
            this.clearEditorToolStripMenuItem.Name = "clearEditorToolStripMenuItem";
            this.clearEditorToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.clearEditorToolStripMenuItem.Text = "Clear Editor";
            this.clearEditorToolStripMenuItem.Click += new System.EventHandler(this.clearEditorToolStripMenuItem_Click);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stepNextToolStripMenuItem,
            this.toolStripSeparator3,
            this.runToolStripMenuItem1,
            this.checkErrorsToolStripMenuItem});
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(78, 24);
            this.runToolStripMenuItem.Text = "Program";
            // 
            // stepNextToolStripMenuItem
            // 
            this.stepNextToolStripMenuItem.Name = "stepNextToolStripMenuItem";
            this.stepNextToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.stepNextToolStripMenuItem.Text = "Step Next";
            this.stepNextToolStripMenuItem.Click += new System.EventHandler(this.stepNextToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(186, 6);
            // 
            // runToolStripMenuItem1
            // 
            this.runToolStripMenuItem1.Name = "runToolStripMenuItem1";
            this.runToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.runToolStripMenuItem1.Size = new System.Drawing.Size(189, 26);
            this.runToolStripMenuItem1.Text = "Run";
            this.runToolStripMenuItem1.Click += new System.EventHandler(this.runToolStripMenuItem1_Click);
            // 
            // checkErrorsToolStripMenuItem
            // 
            this.checkErrorsToolStripMenuItem.Name = "checkErrorsToolStripMenuItem";
            this.checkErrorsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.checkErrorsToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.checkErrorsToolStripMenuItem.Text = "Check Errors";
            this.checkErrorsToolStripMenuItem.Click += new System.EventHandler(this.checkErrorsToolStripMenuItem_Click);
            // 
            // formatToolStripMenuItem
            // 
            this.formatToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fontToolStripMenuItem});
            this.formatToolStripMenuItem.Name = "formatToolStripMenuItem";
            this.formatToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
            this.formatToolStripMenuItem.Text = "Format";
            // 
            // fontToolStripMenuItem
            // 
            this.fontToolStripMenuItem.Name = "fontToolStripMenuItem";
            this.fontToolStripMenuItem.Size = new System.Drawing.Size(113, 26);
            this.fontToolStripMenuItem.Text = "Font";
            this.fontToolStripMenuItem.Click += new System.EventHandler(this.fontToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(125, 26);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label4.Location = new System.Drawing.Point(925, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(251, 23);
            this.label4.TabIndex = 29;
            this.label4.Text = "Instruction coding still in works!\r\n";
            this.label4.Click += new System.EventHandler(this.warning_click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.play_button,
            this.step_button,
            this.stop_button});
            this.toolStrip1.Location = new System.Drawing.Point(452, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(170, 27);
            this.toolStrip1.TabIndex = 30;
            this.toolStrip1.Text = "tool_strip";
            // 
            // play_button
            // 
            this.play_button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.play_button.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.play_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.play_button.Name = "play_button";
            this.play_button.Size = new System.Drawing.Size(49, 24);
            this.play_button.Text = "Run";
            this.play_button.ToolTipText = "Compile and Run whole program to the end!";
            this.play_button.Click += new System.EventHandler(this.run_program);
            // 
            // step_button
            // 
            this.step_button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.step_button.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.step_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.step_button.Name = "step_button";
            this.step_button.Size = new System.Drawing.Size(52, 24);
            this.step_button.Text = "Step";
            this.step_button.ToolTipText = "Compile and run program wth Stepping!";
            this.step_button.Click += new System.EventHandler(this.step_program);
            // 
            // stop_button
            // 
            this.stop_button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stop_button.Enabled = false;
            this.stop_button.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stop_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stop_button.Name = "stop_button";
            this.stop_button.Size = new System.Drawing.Size(53, 24);
            this.stop_button.Text = "Stop";
            this.stop_button.ToolTipText = "Stop running program! Useful if program goes into infinite loop!";
            this.stop_button.Click += new System.EventHandler(this.stop_program);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 700);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.main_layout);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(1197, 747);
            this.Name = "main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "8085 Simulator And Assembler";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.closing);
            this.Load += new System.EventHandler(this.loading);
            this.registers_box.ResumeLayout(false);
            this.register_layout.ResumeLayout(false);
            this.register_layout.PerformLayout();
            this.flag_box.ResumeLayout(false);
            this.flag_layout.ResumeLayout(false);
            this.flag_layout.PerformLayout();
            this.main_layout.ResumeLayout(false);
            this.main_layout.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.data_tabs.ResumeLayout(false);
            this.memoryTab.ResumeLayout(false);
            this.stackTab.ResumeLayout(false);
            this.portTab.ResumeLayout(false);
            this.code_input_layout.ResumeLayout(false);
            this.code_editors.ResumeLayout(false);
            this.status_group_layout.ResumeLayout(false);
            this.status_group_layout.PerformLayout();
            this.pcandsp.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.conv_group_box.ResumeLayout(false);
            this.conv_group_box.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private System.Windows.Forms.ListBox output_box;
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
        private System.Windows.Forms.Label lreg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label hreg;
        private System.Windows.Forms.TableLayoutPanel register_layout;
        private System.Windows.Forms.TableLayoutPanel flag_layout;
        private System.Windows.Forms.TableLayoutPanel main_layout;
        private System.Windows.Forms.TableLayoutPanel code_input_layout;
        private System.Windows.Forms.TableLayoutPanel status_group_layout;
        private System.Windows.Forms.TableLayoutPanel code_editors;
        private System.Windows.Forms.GroupBox pcandsp;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label s;
        private System.Windows.Forms.Label spreg;
        private System.Windows.Forms.Label pcreg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ListView memorybox;
        private System.Windows.Forms.ColumnHeader address;
        private System.Windows.Forms.ColumnHeader memoryData;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TextBox address_to_find;
        private System.Windows.Forms.Button find_address;
        private System.Windows.Forms.ToolStripMenuItem newASMFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openASMToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label mlbl;
        private System.Windows.Forms.Label mreg;
        private System.Windows.Forms.Label pswlbl;
        private System.Windows.Forms.Label pswreg;
        private System.Windows.Forms.ToolStripMenuItem clearMemoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearRegistersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stepNextToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl data_tabs;
        private System.Windows.Forms.TabPage memoryTab;
        private System.Windows.Forms.TabPage stackTab;
        private System.Windows.Forms.TabPage portTab;
        private System.Windows.Forms.ListView stackbox;
        private System.Windows.Forms.ColumnHeader index;
        private System.Windows.Forms.ColumnHeader stackData;
        private System.Windows.Forms.ListView portbox;
        private System.Windows.Forms.ColumnHeader portAddress;
        private System.Windows.Forms.ColumnHeader portData;
        private System.Windows.Forms.GroupBox conv_group_box;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label reg_name;
        private System.Windows.Forms.Label conv_lbl;
        private System.Windows.Forms.ToolStripMenuItem checkErrorsToolStripMenuItem;
        private ScintillaNET.Scintilla codeEditor;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton play_button;
        private System.Windows.Forms.ToolStripButton step_button;
        private System.Windows.Forms.ToolStripButton stop_button;
    }
}

