namespace Automation.ConfigMaker.GUI
{
    partial class PrimaryForm
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
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.scriptGroupBox = new System.Windows.Forms.GroupBox();
            this.scriptModifyButton = new System.Windows.Forms.Button();
            this.scriptRemoveButton = new System.Windows.Forms.Button();
            this.scriptAddButton = new System.Windows.Forms.Button();
            this.scriptListBox = new System.Windows.Forms.ListBox();
            this.keyGroupBox = new System.Windows.Forms.GroupBox();
            this.keyModifyButton = new System.Windows.Forms.Button();
            this.keyRemoveButton = new System.Windows.Forms.Button();
            this.keyAddButton = new System.Windows.Forms.Button();
            this.keyListBox = new System.Windows.Forms.ListBox();
            this.jobGroupBox = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.jobNameTextBox = new System.Windows.Forms.TextBox();
            this.portOverrideCheckBox = new System.Windows.Forms.CheckBox();
            this.portNumeric = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.jobCategoryComboBox = new System.Windows.Forms.ComboBox();
            this.jobRemoveButton = new System.Windows.Forms.Button();
            this.jobAddButton = new System.Windows.Forms.Button();
            this.jobListBox = new System.Windows.Forms.ListBox();
            this.metadataGroupBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.authorEmailTextBox = new System.Windows.Forms.TextBox();
            this.authorNameTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.revisionTextBox = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.hostGroupBox = new System.Windows.Forms.GroupBox();
            this.hostModifyButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.hostRemoveButton = new System.Windows.Forms.Button();
            this.hostAddButton = new System.Windows.Forms.Button();
            this.hostTextBox = new System.Windows.Forms.TextBox();
            this.hostListBox = new System.Windows.Forms.ListBox();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.scriptGroupBox.SuspendLayout();
            this.keyGroupBox.SuspendLayout();
            this.jobGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portNumeric)).BeginInit();
            this.metadataGroupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.hostGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.RestoreDirectory = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.RestoreDirectory = true;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(981, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.saveFileToolStripMenuItem,
            this.toolStripSeparator1,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.openFileToolStripMenuItem.Text = "Open file";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.saveFileToolStripMenuItem.Text = "Save file";
            this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(119, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatusLabel1});
            this.statusStrip.Location = new System.Drawing.Point(0, 497);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(981, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.scriptGroupBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.keyGroupBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.jobGroupBox, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.metadataGroupBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.hostGroupBox, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 27);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(957, 467);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // scriptGroupBox
            // 
            this.scriptGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scriptGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.scriptGroupBox.Controls.Add(this.scriptModifyButton);
            this.scriptGroupBox.Controls.Add(this.scriptRemoveButton);
            this.scriptGroupBox.Controls.Add(this.scriptAddButton);
            this.scriptGroupBox.Controls.Add(this.scriptListBox);
            this.scriptGroupBox.Location = new System.Drawing.Point(336, 183);
            this.scriptGroupBox.Name = "scriptGroupBox";
            this.scriptGroupBox.Size = new System.Drawing.Size(327, 281);
            this.scriptGroupBox.TabIndex = 8;
            this.scriptGroupBox.TabStop = false;
            this.scriptGroupBox.Text = "Scripts";
            // 
            // scriptModifyButton
            // 
            this.scriptModifyButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.scriptModifyButton.Location = new System.Drawing.Point(130, 252);
            this.scriptModifyButton.Name = "scriptModifyButton";
            this.scriptModifyButton.Size = new System.Drawing.Size(60, 23);
            this.scriptModifyButton.TabIndex = 9;
            this.scriptModifyButton.Text = "Modify";
            this.scriptModifyButton.UseVisualStyleBackColor = true;
            // 
            // scriptRemoveButton
            // 
            this.scriptRemoveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.scriptRemoveButton.Location = new System.Drawing.Point(261, 252);
            this.scriptRemoveButton.Name = "scriptRemoveButton";
            this.scriptRemoveButton.Size = new System.Drawing.Size(60, 23);
            this.scriptRemoveButton.TabIndex = 8;
            this.scriptRemoveButton.Text = "Remove";
            this.scriptRemoveButton.UseVisualStyleBackColor = true;
            this.scriptRemoveButton.Click += new System.EventHandler(this.scriptRemoveButton_Click);
            // 
            // scriptAddButton
            // 
            this.scriptAddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.scriptAddButton.Location = new System.Drawing.Point(6, 252);
            this.scriptAddButton.Name = "scriptAddButton";
            this.scriptAddButton.Size = new System.Drawing.Size(60, 23);
            this.scriptAddButton.TabIndex = 7;
            this.scriptAddButton.Text = "Add";
            this.scriptAddButton.UseVisualStyleBackColor = true;
            // 
            // scriptListBox
            // 
            this.scriptListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scriptListBox.FormattingEnabled = true;
            this.scriptListBox.Location = new System.Drawing.Point(7, 20);
            this.scriptListBox.Name = "scriptListBox";
            this.scriptListBox.Size = new System.Drawing.Size(314, 225);
            this.scriptListBox.TabIndex = 0;
            // 
            // keyGroupBox
            // 
            this.keyGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keyGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.keyGroupBox.Controls.Add(this.keyModifyButton);
            this.keyGroupBox.Controls.Add(this.keyRemoveButton);
            this.keyGroupBox.Controls.Add(this.keyAddButton);
            this.keyGroupBox.Controls.Add(this.keyListBox);
            this.keyGroupBox.Location = new System.Drawing.Point(3, 183);
            this.keyGroupBox.Name = "keyGroupBox";
            this.keyGroupBox.Size = new System.Drawing.Size(327, 281);
            this.keyGroupBox.TabIndex = 7;
            this.keyGroupBox.TabStop = false;
            this.keyGroupBox.Text = "Keys";
            // 
            // keyModifyButton
            // 
            this.keyModifyButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.keyModifyButton.Location = new System.Drawing.Point(130, 252);
            this.keyModifyButton.Name = "keyModifyButton";
            this.keyModifyButton.Size = new System.Drawing.Size(60, 23);
            this.keyModifyButton.TabIndex = 6;
            this.keyModifyButton.Text = "Modify";
            this.keyModifyButton.UseVisualStyleBackColor = true;
            // 
            // keyRemoveButton
            // 
            this.keyRemoveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.keyRemoveButton.Location = new System.Drawing.Point(261, 252);
            this.keyRemoveButton.Name = "keyRemoveButton";
            this.keyRemoveButton.Size = new System.Drawing.Size(60, 23);
            this.keyRemoveButton.TabIndex = 5;
            this.keyRemoveButton.Text = "Remove";
            this.keyRemoveButton.UseVisualStyleBackColor = true;
            this.keyRemoveButton.Click += new System.EventHandler(this.keyRemoveButton_Click);
            // 
            // keyAddButton
            // 
            this.keyAddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.keyAddButton.Location = new System.Drawing.Point(6, 252);
            this.keyAddButton.Name = "keyAddButton";
            this.keyAddButton.Size = new System.Drawing.Size(60, 23);
            this.keyAddButton.TabIndex = 4;
            this.keyAddButton.Text = "Add";
            this.keyAddButton.UseVisualStyleBackColor = true;
            // 
            // keyListBox
            // 
            this.keyListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keyListBox.FormattingEnabled = true;
            this.keyListBox.Location = new System.Drawing.Point(7, 20);
            this.keyListBox.Name = "keyListBox";
            this.keyListBox.Size = new System.Drawing.Size(314, 225);
            this.keyListBox.TabIndex = 0;
            // 
            // jobGroupBox
            // 
            this.jobGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.jobGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.jobGroupBox.Controls.Add(this.label9);
            this.jobGroupBox.Controls.Add(this.jobNameTextBox);
            this.jobGroupBox.Controls.Add(this.portOverrideCheckBox);
            this.jobGroupBox.Controls.Add(this.portNumeric);
            this.jobGroupBox.Controls.Add(this.label8);
            this.jobGroupBox.Controls.Add(this.label7);
            this.jobGroupBox.Controls.Add(this.jobCategoryComboBox);
            this.jobGroupBox.Controls.Add(this.jobRemoveButton);
            this.jobGroupBox.Controls.Add(this.jobAddButton);
            this.jobGroupBox.Controls.Add(this.jobListBox);
            this.jobGroupBox.Location = new System.Drawing.Point(669, 3);
            this.jobGroupBox.Name = "jobGroupBox";
            this.tableLayoutPanel1.SetRowSpan(this.jobGroupBox, 2);
            this.jobGroupBox.Size = new System.Drawing.Size(285, 461);
            this.jobGroupBox.TabIndex = 9;
            this.jobGroupBox.TabStop = false;
            this.jobGroupBox.Text = "Jobs";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 130);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Name:";
            // 
            // jobNameTextBox
            // 
            this.jobNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.jobNameTextBox.Location = new System.Drawing.Point(64, 127);
            this.jobNameTextBox.Name = "jobNameTextBox";
            this.jobNameTextBox.Size = new System.Drawing.Size(215, 20);
            this.jobNameTextBox.TabIndex = 12;
            this.jobNameTextBox.Leave += new System.EventHandler(this.jobNameTextBox_Leave);
            // 
            // portOverrideCheckBox
            // 
            this.portOverrideCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.portOverrideCheckBox.AutoSize = true;
            this.portOverrideCheckBox.Location = new System.Drawing.Point(137, 183);
            this.portOverrideCheckBox.Name = "portOverrideCheckBox";
            this.portOverrideCheckBox.Size = new System.Drawing.Size(66, 17);
            this.portOverrideCheckBox.TabIndex = 11;
            this.portOverrideCheckBox.Text = "Override";
            this.portOverrideCheckBox.UseVisualStyleBackColor = true;
            this.portOverrideCheckBox.CheckedChanged += new System.EventHandler(this.portOverrideCheckBox_CheckedChanged);
            // 
            // portNumeric
            // 
            this.portNumeric.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.portNumeric.Location = new System.Drawing.Point(64, 181);
            this.portNumeric.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.portNumeric.Name = "portNumeric";
            this.portNumeric.ReadOnly = true;
            this.portNumeric.Size = new System.Drawing.Size(66, 20);
            this.portNumeric.TabIndex = 10;
            this.portNumeric.Value = new decimal(new int[] {
            443,
            0,
            0,
            0});
            this.portNumeric.ValueChanged += new System.EventHandler(this.portNumeric_ValueChanged);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 184);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Port: ";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 156);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Category:";
            // 
            // jobCategoryComboBox
            // 
            this.jobCategoryComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.jobCategoryComboBox.FormattingEnabled = true;
            this.jobCategoryComboBox.Location = new System.Drawing.Point(64, 153);
            this.jobCategoryComboBox.Name = "jobCategoryComboBox";
            this.jobCategoryComboBox.Size = new System.Drawing.Size(215, 21);
            this.jobCategoryComboBox.TabIndex = 7;
            this.jobCategoryComboBox.SelectedIndexChanged += new System.EventHandler(this.jobCategoryComboBox_SelectedIndexChanged);
            // 
            // jobRemoveButton
            // 
            this.jobRemoveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.jobRemoveButton.Location = new System.Drawing.Point(219, 48);
            this.jobRemoveButton.Name = "jobRemoveButton";
            this.jobRemoveButton.Size = new System.Drawing.Size(60, 23);
            this.jobRemoveButton.TabIndex = 6;
            this.jobRemoveButton.Text = "Remove";
            this.jobRemoveButton.UseVisualStyleBackColor = true;
            this.jobRemoveButton.Click += new System.EventHandler(this.jobRemoveButton_Click);
            // 
            // jobAddButton
            // 
            this.jobAddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.jobAddButton.Location = new System.Drawing.Point(219, 19);
            this.jobAddButton.Name = "jobAddButton";
            this.jobAddButton.Size = new System.Drawing.Size(60, 23);
            this.jobAddButton.TabIndex = 5;
            this.jobAddButton.Text = "Add";
            this.jobAddButton.UseVisualStyleBackColor = true;
            this.jobAddButton.Click += new System.EventHandler(this.jobAddButton_Click);
            // 
            // jobListBox
            // 
            this.jobListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.jobListBox.FormattingEnabled = true;
            this.jobListBox.Location = new System.Drawing.Point(6, 19);
            this.jobListBox.Name = "jobListBox";
            this.jobListBox.Size = new System.Drawing.Size(206, 82);
            this.jobListBox.TabIndex = 0;
            this.jobListBox.SelectedIndexChanged += new System.EventHandler(this.jobListBox_SelectedIndexChanged);
            // 
            // metadataGroupBox
            // 
            this.metadataGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metadataGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.metadataGroupBox.Controls.Add(this.label3);
            this.metadataGroupBox.Controls.Add(this.groupBox2);
            this.metadataGroupBox.Controls.Add(this.revisionTextBox);
            this.metadataGroupBox.Controls.Add(this.descriptionTextBox);
            this.metadataGroupBox.Controls.Add(this.label2);
            this.metadataGroupBox.Controls.Add(this.label1);
            this.metadataGroupBox.Controls.Add(this.titleTextBox);
            this.metadataGroupBox.Location = new System.Drawing.Point(3, 3);
            this.metadataGroupBox.Name = "metadataGroupBox";
            this.metadataGroupBox.Size = new System.Drawing.Size(327, 174);
            this.metadataGroupBox.TabIndex = 5;
            this.metadataGroupBox.TabStop = false;
            this.metadataGroupBox.Text = "Metadata";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Revision: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.authorEmailTextBox);
            this.groupBox2.Controls.Add(this.authorNameTextBox);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(6, 101);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(315, 67);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Author";
            // 
            // authorEmailTextBox
            // 
            this.authorEmailTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.authorEmailTextBox.Location = new System.Drawing.Point(74, 39);
            this.authorEmailTextBox.Name = "authorEmailTextBox";
            this.authorEmailTextBox.Size = new System.Drawing.Size(235, 20);
            this.authorEmailTextBox.TabIndex = 3;
            this.authorEmailTextBox.Leave += new System.EventHandler(this.authorEmailTextBox_Leave);
            // 
            // authorNameTextBox
            // 
            this.authorNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.authorNameTextBox.Location = new System.Drawing.Point(74, 13);
            this.authorNameTextBox.Name = "authorNameTextBox";
            this.authorNameTextBox.Size = new System.Drawing.Size(235, 20);
            this.authorNameTextBox.TabIndex = 2;
            this.authorNameTextBox.Leave += new System.EventHandler(this.authorNameTextBox_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "E-mail: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Name: ";
            // 
            // revisionTextBox
            // 
            this.revisionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.revisionTextBox.Location = new System.Drawing.Point(80, 39);
            this.revisionTextBox.Name = "revisionTextBox";
            this.revisionTextBox.ReadOnly = true;
            this.revisionTextBox.Size = new System.Drawing.Size(241, 20);
            this.revisionTextBox.TabIndex = 4;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionTextBox.Location = new System.Drawing.Point(80, 65);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(241, 30);
            this.descriptionTextBox.TabIndex = 3;
            this.descriptionTextBox.Leave += new System.EventHandler(this.descriptionTextBox_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Title: ";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleTextBox.Location = new System.Drawing.Point(80, 13);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(241, 20);
            this.titleTextBox.TabIndex = 0;
            this.titleTextBox.Leave += new System.EventHandler(this.titleTextBox_Leave);
            // 
            // hostGroupBox
            // 
            this.hostGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hostGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.hostGroupBox.Controls.Add(this.hostModifyButton);
            this.hostGroupBox.Controls.Add(this.label6);
            this.hostGroupBox.Controls.Add(this.hostRemoveButton);
            this.hostGroupBox.Controls.Add(this.hostAddButton);
            this.hostGroupBox.Controls.Add(this.hostTextBox);
            this.hostGroupBox.Controls.Add(this.hostListBox);
            this.hostGroupBox.Location = new System.Drawing.Point(336, 3);
            this.hostGroupBox.Name = "hostGroupBox";
            this.hostGroupBox.Size = new System.Drawing.Size(327, 174);
            this.hostGroupBox.TabIndex = 6;
            this.hostGroupBox.TabStop = false;
            this.hostGroupBox.Text = "Hosts";
            // 
            // hostModifyButton
            // 
            this.hostModifyButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.hostModifyButton.Location = new System.Drawing.Point(130, 145);
            this.hostModifyButton.Name = "hostModifyButton";
            this.hostModifyButton.Size = new System.Drawing.Size(60, 23);
            this.hostModifyButton.TabIndex = 5;
            this.hostModifyButton.Text = "Modify";
            this.hostModifyButton.UseVisualStyleBackColor = true;
            this.hostModifyButton.Click += new System.EventHandler(this.hostModifyButton_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Hostname/IP:";
            // 
            // hostRemoveButton
            // 
            this.hostRemoveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.hostRemoveButton.Location = new System.Drawing.Point(261, 145);
            this.hostRemoveButton.Name = "hostRemoveButton";
            this.hostRemoveButton.Size = new System.Drawing.Size(60, 23);
            this.hostRemoveButton.TabIndex = 3;
            this.hostRemoveButton.Text = "Remove";
            this.hostRemoveButton.UseVisualStyleBackColor = true;
            this.hostRemoveButton.Click += new System.EventHandler(this.hostRemoveButton_Click);
            // 
            // hostAddButton
            // 
            this.hostAddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.hostAddButton.Location = new System.Drawing.Point(6, 145);
            this.hostAddButton.Name = "hostAddButton";
            this.hostAddButton.Size = new System.Drawing.Size(60, 23);
            this.hostAddButton.TabIndex = 2;
            this.hostAddButton.Text = "Add";
            this.hostAddButton.UseVisualStyleBackColor = true;
            this.hostAddButton.Click += new System.EventHandler(this.hostAddButton_Click);
            // 
            // hostTextBox
            // 
            this.hostTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hostTextBox.Location = new System.Drawing.Point(80, 120);
            this.hostTextBox.Name = "hostTextBox";
            this.hostTextBox.Size = new System.Drawing.Size(241, 20);
            this.hostTextBox.TabIndex = 1;
            // 
            // hostListBox
            // 
            this.hostListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hostListBox.FormattingEnabled = true;
            this.hostListBox.Location = new System.Drawing.Point(6, 19);
            this.hostListBox.Name = "hostListBox";
            this.hostListBox.Size = new System.Drawing.Size(315, 82);
            this.hostListBox.TabIndex = 0;
            this.hostListBox.SelectedIndexChanged += new System.EventHandler(this.hostListBox_SelectedIndexChanged);
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar.Value = 100;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(35, 17);
            this.toolStripStatusLabel1.Text = "Done";
            // 
            // PrimaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(981, 519);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(500, 500);
            this.Name = "PrimaryForm";
            this.Text = "ConfigMaker";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.scriptGroupBox.ResumeLayout(false);
            this.keyGroupBox.ResumeLayout(false);
            this.jobGroupBox.ResumeLayout(false);
            this.jobGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portNumeric)).EndInit();
            this.metadataGroupBox.ResumeLayout(false);
            this.metadataGroupBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.hostGroupBox.ResumeLayout(false);
            this.hostGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox hostGroupBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button hostRemoveButton;
        private System.Windows.Forms.Button hostAddButton;
        private System.Windows.Forms.TextBox hostTextBox;
        private System.Windows.Forms.ListBox hostListBox;
        private System.Windows.Forms.GroupBox metadataGroupBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox authorEmailTextBox;
        private System.Windows.Forms.TextBox authorNameTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox revisionTextBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.GroupBox jobGroupBox;
        private System.Windows.Forms.GroupBox scriptGroupBox;
        private System.Windows.Forms.GroupBox keyGroupBox;
        private System.Windows.Forms.ListBox scriptListBox;
        private System.Windows.Forms.ListBox keyListBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox jobCategoryComboBox;
        private System.Windows.Forms.Button jobRemoveButton;
        private System.Windows.Forms.Button jobAddButton;
        private System.Windows.Forms.ListBox jobListBox;
        private System.Windows.Forms.Button scriptModifyButton;
        private System.Windows.Forms.Button scriptRemoveButton;
        private System.Windows.Forms.Button scriptAddButton;
        private System.Windows.Forms.Button keyModifyButton;
        private System.Windows.Forms.Button keyRemoveButton;
        private System.Windows.Forms.Button keyAddButton;
        private System.Windows.Forms.Button hostModifyButton;
        private System.Windows.Forms.CheckBox portOverrideCheckBox;
        private System.Windows.Forms.NumericUpDown portNumeric;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox jobNameTextBox;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}

