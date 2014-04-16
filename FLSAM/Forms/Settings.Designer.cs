namespace FLSAM.Forms
{
    partial class Settings
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabConnection = new System.Windows.Forms.TabPage();
            this.comboBackendSelector = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabSQLite = new System.Windows.Forms.TabPage();
            this.buttonSQOpen = new System.Windows.Forms.Button();
            this.buttonClearSQL = new System.Windows.Forms.Button();
            this.buttonSQLPathSelector = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxSQAggressive = new System.Windows.Forms.CheckBox();
            this.textBoxSQLPath = new System.Windows.Forms.TextBox();
            this.tabFLDB = new System.Windows.Forms.TabPage();
            this.buttonFLDBPathSelector = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxFLDBPath = new System.Windows.Forms.TextBox();
            this.tabHook = new System.Windows.Forms.TabPage();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.saveFileSQLPicker = new System.Windows.Forms.SaveFileDialog();
            this.buttonApply = new System.Windows.Forms.Button();
            this.folderBrowserFLDBPath = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelCleanupBackup = new System.Windows.Forms.Panel();
            this.textCleanupBackup = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBoxCleanBackup = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.checkBoxCleanup = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonIonPath = new System.Windows.Forms.Button();
            this.buttonFLPath = new System.Windows.Forms.Button();
            this.textIonPath = new System.Windows.Forms.TextBox();
            this.textFLPath = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabTuning = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.openFileSQLPicker = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabConnection.SuspendLayout();
            this.tabSQLite.SuspendLayout();
            this.tabFLDB.SuspendLayout();
            this.tabHook.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tabControl2.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelCleanupBackup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabTuning.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabConnection);
            this.tabControl1.Controls.Add(this.tabSQLite);
            this.tabControl1.Controls.Add(this.tabFLDB);
            this.tabControl1.Controls.Add(this.tabHook);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Location = new System.Drawing.Point(0, 423);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(782, 69);
            this.tabControl1.TabIndex = 0;
            // 
            // tabConnection
            // 
            this.tabConnection.Controls.Add(this.comboBackendSelector);
            this.tabConnection.Controls.Add(this.label4);
            this.tabConnection.Location = new System.Drawing.Point(4, 22);
            this.tabConnection.Name = "tabConnection";
            this.tabConnection.Padding = new System.Windows.Forms.Padding(3);
            this.tabConnection.Size = new System.Drawing.Size(774, 43);
            this.tabConnection.TabIndex = 0;
            this.tabConnection.Text = "General connection";
            this.tabConnection.UseVisualStyleBackColor = true;
            // 
            // comboBackendSelector
            // 
            this.comboBackendSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBackendSelector.FormattingEnabled = true;
            this.comboBackendSelector.Items.AddRange(new object[] {
            "NoSQL + SQLite for metadata (Legacy)",
            "FOS SQLite"});
            this.comboBackendSelector.Location = new System.Drawing.Point(63, 9);
            this.comboBackendSelector.Name = "comboBackendSelector";
            this.comboBackendSelector.Size = new System.Drawing.Size(132, 21);
            this.comboBackendSelector.TabIndex = 1;
            this.comboBackendSelector.SelectedIndexChanged += new System.EventHandler(this.comboBackendSelector_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "DB Type";
            // 
            // tabSQLite
            // 
            this.tabSQLite.Controls.Add(this.buttonSQOpen);
            this.tabSQLite.Controls.Add(this.buttonClearSQL);
            this.tabSQLite.Controls.Add(this.buttonSQLPathSelector);
            this.tabSQLite.Controls.Add(this.label5);
            this.tabSQLite.Controls.Add(this.checkBoxSQAggressive);
            this.tabSQLite.Controls.Add(this.textBoxSQLPath);
            this.tabSQLite.Location = new System.Drawing.Point(4, 22);
            this.tabSQLite.Name = "tabSQLite";
            this.tabSQLite.Size = new System.Drawing.Size(774, 43);
            this.tabSQLite.TabIndex = 2;
            this.tabSQLite.Text = "SQLite";
            this.tabSQLite.UseVisualStyleBackColor = true;
            // 
            // buttonSQOpen
            // 
            this.buttonSQOpen.Location = new System.Drawing.Point(479, 10);
            this.buttonSQOpen.Name = "buttonSQOpen";
            this.buttonSQOpen.Size = new System.Drawing.Size(75, 20);
            this.buttonSQOpen.TabIndex = 5;
            this.buttonSQOpen.Text = "Open";
            this.buttonSQOpen.UseVisualStyleBackColor = true;
            this.buttonSQOpen.Click += new System.EventHandler(this.buttonSQOpen_Click);
            // 
            // buttonClearSQL
            // 
            this.buttonClearSQL.Location = new System.Drawing.Point(560, 10);
            this.buttonClearSQL.Name = "buttonClearSQL";
            this.buttonClearSQL.Size = new System.Drawing.Size(75, 20);
            this.buttonClearSQL.TabIndex = 3;
            this.buttonClearSQL.Text = "Clear the DB";
            this.buttonClearSQL.UseVisualStyleBackColor = true;
            this.buttonClearSQL.Click += new System.EventHandler(this.buttonClearSQL_Click);
            // 
            // buttonSQLPathSelector
            // 
            this.buttonSQLPathSelector.Location = new System.Drawing.Point(306, 10);
            this.buttonSQLPathSelector.Name = "buttonSQLPathSelector";
            this.buttonSQLPathSelector.Size = new System.Drawing.Size(89, 20);
            this.buttonSQLPathSelector.TabIndex = 2;
            this.buttonSQLPathSelector.Text = "Browse";
            this.buttonSQLPathSelector.UseVisualStyleBackColor = true;
            this.buttonSQLPathSelector.Click += new System.EventHandler(this.buttonSQLPathSelector_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Path to DB";
            // 
            // checkBoxSQAggressive
            // 
            this.checkBoxSQAggressive.AutoSize = true;
            this.checkBoxSQAggressive.Checked = global::FLSAM.Properties.Settings.Default.DBAggressiveScan;
            this.checkBoxSQAggressive.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::FLSAM.Properties.Settings.Default, "DBAggressiveScan", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxSQAggressive.Location = new System.Drawing.Point(401, 12);
            this.checkBoxSQAggressive.Name = "checkBoxSQAggressive";
            this.checkBoxSQAggressive.Size = new System.Drawing.Size(78, 17);
            this.checkBoxSQAggressive.TabIndex = 4;
            this.checkBoxSQAggressive.Text = "Aggressive";
            this.checkBoxSQAggressive.UseVisualStyleBackColor = true;
            // 
            // textBoxSQLPath
            // 
            this.textBoxSQLPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FLSAM.Properties.Settings.Default, "DBPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxSQLPath.Location = new System.Drawing.Point(76, 10);
            this.textBoxSQLPath.Name = "textBoxSQLPath";
            this.textBoxSQLPath.Size = new System.Drawing.Size(224, 20);
            this.textBoxSQLPath.TabIndex = 1;
            this.textBoxSQLPath.Text = global::FLSAM.Properties.Settings.Default.DBPath;
            // 
            // tabFLDB
            // 
            this.tabFLDB.Controls.Add(this.buttonFLDBPathSelector);
            this.tabFLDB.Controls.Add(this.label6);
            this.tabFLDB.Controls.Add(this.textBoxFLDBPath);
            this.tabFLDB.Location = new System.Drawing.Point(4, 22);
            this.tabFLDB.Name = "tabFLDB";
            this.tabFLDB.Size = new System.Drawing.Size(774, 43);
            this.tabFLDB.TabIndex = 3;
            this.tabFLDB.Text = "Freelancer DB";
            this.tabFLDB.UseVisualStyleBackColor = true;
            // 
            // buttonFLDBPathSelector
            // 
            this.buttonFLDBPathSelector.Location = new System.Drawing.Point(504, 13);
            this.buttonFLDBPathSelector.Name = "buttonFLDBPathSelector";
            this.buttonFLDBPathSelector.Size = new System.Drawing.Size(89, 20);
            this.buttonFLDBPathSelector.TabIndex = 3;
            this.buttonFLDBPathSelector.Text = "Browse";
            this.buttonFLDBPathSelector.UseVisualStyleBackColor = true;
            this.buttonFLDBPathSelector.Click += new System.EventHandler(this.buttonFLDBPathSelector_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "MultiPlayer directory";
            // 
            // textBoxFLDBPath
            // 
            this.textBoxFLDBPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FLSAM.Properties.Settings.Default, "FLDBPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxFLDBPath.Enabled = false;
            this.textBoxFLDBPath.Location = new System.Drawing.Point(115, 13);
            this.textBoxFLDBPath.Name = "textBoxFLDBPath";
            this.textBoxFLDBPath.Size = new System.Drawing.Size(383, 20);
            this.textBoxFLDBPath.TabIndex = 1;
            this.textBoxFLDBPath.Text = global::FLSAM.Properties.Settings.Default.FLDBPath;
            // 
            // tabHook
            // 
            this.tabHook.Controls.Add(this.numericUpDown1);
            this.tabHook.Controls.Add(this.textBox2);
            this.tabHook.Controls.Add(this.textBox1);
            this.tabHook.Controls.Add(this.label3);
            this.tabHook.Controls.Add(this.label2);
            this.tabHook.Controls.Add(this.label1);
            this.tabHook.Location = new System.Drawing.Point(4, 22);
            this.tabHook.Name = "tabHook";
            this.tabHook.Padding = new System.Windows.Forms.Padding(3);
            this.tabHook.Size = new System.Drawing.Size(774, 43);
            this.tabHook.TabIndex = 1;
            this.tabHook.Text = "FLHook";
            this.tabHook.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::FLSAM.Properties.Settings.Default, "HookPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown1.Location = new System.Drawing.Point(519, 15);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(63, 20);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.Value = global::FLSAM.Properties.Settings.Default.HookPort;
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FLSAM.Properties.Settings.Default, "HookPassword", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox2.Location = new System.Drawing.Point(276, 15);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(142, 20);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = global::FLSAM.Properties.Settings.Default.HookPassword;
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FLSAM.Properties.Settings.Default, "HookAddr", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox1.Location = new System.Drawing.Point(68, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(142, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = global::FLSAM.Properties.Settings.Default.HookAddr;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(216, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(424, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port (0 to disable)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Address";
            // 
            // saveFileSQLPicker
            // 
            this.saveFileSQLPicker.AddExtension = false;
            this.saveFileSQLPicker.DefaultExt = "db";
            this.saveFileSQLPicker.Filter = "SQLite Database (*.db)|*.db|All files|*.*";
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(645, 423);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(137, 69);
            this.buttonApply.TabIndex = 1;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // folderBrowserFLDBPath
            // 
            this.folderBrowserFLDBPath.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabGeneral);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Controls.Add(this.tabTuning);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(782, 423);
            this.tabControl2.TabIndex = 2;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.groupBox2);
            this.tabGeneral.Controls.Add(this.groupBox1);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(774, 397);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.checkBoxCleanup);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(768, 129);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Character deletion";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelCleanupBackup);
            this.panel1.Controls.Add(this.checkBoxCleanBackup);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.numericUpDown2);
            this.panel1.Controls.Add(this.label9);
            this.panel1.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", global::FLSAM.Properties.Settings.Default, "CleanupEnabled", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.panel1.Enabled = global::FLSAM.Properties.Settings.Default.CleanupEnabled;
            this.panel1.Location = new System.Drawing.Point(0, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(768, 82);
            this.panel1.TabIndex = 1;
            // 
            // panelCleanupBackup
            // 
            this.panelCleanupBackup.Controls.Add(this.textCleanupBackup);
            this.panelCleanupBackup.Controls.Add(this.button1);
            this.panelCleanupBackup.Enabled = false;
            this.panelCleanupBackup.Location = new System.Drawing.Point(117, 28);
            this.panelCleanupBackup.Name = "panelCleanupBackup";
            this.panelCleanupBackup.Size = new System.Drawing.Size(525, 35);
            this.panelCleanupBackup.TabIndex = 7;
            // 
            // textCleanupBackup
            // 
            this.textCleanupBackup.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FLSAM.Properties.Settings.Default, "CleanupBackupPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textCleanupBackup.Enabled = false;
            this.textCleanupBackup.Location = new System.Drawing.Point(0, 4);
            this.textCleanupBackup.Name = "textCleanupBackup";
            this.textCleanupBackup.Size = new System.Drawing.Size(423, 20);
            this.textCleanupBackup.TabIndex = 4;
            this.textCleanupBackup.Text = global::FLSAM.Properties.Settings.Default.CleanupBackupPath;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(429, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 20);
            this.button1.TabIndex = 6;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBoxCleanBackup
            // 
            this.checkBoxCleanBackup.AutoSize = true;
            this.checkBoxCleanBackup.Checked = global::FLSAM.Properties.Settings.Default.CleanupBackup;
            this.checkBoxCleanBackup.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::FLSAM.Properties.Settings.Default, "CleanupBackup", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxCleanBackup.Location = new System.Drawing.Point(14, 34);
            this.checkBoxCleanBackup.Name = "checkBoxCleanBackup";
            this.checkBoxCleanBackup.Size = new System.Drawing.Size(92, 17);
            this.checkBoxCleanBackup.TabIndex = 3;
            this.checkBoxCleanBackup.Text = "Back them up";
            this.checkBoxCleanBackup.UseVisualStyleBackColor = true;
            this.checkBoxCleanBackup.CheckedChanged += new System.EventHandler(this.checkBoxCleanBackup_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(150, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "days of inactivity";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(82, 8);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            1827,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(62, 20);
            this.numericUpDown2.TabIndex = 1;
            this.numericUpDown2.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Delete after:";
            // 
            // checkBoxCleanup
            // 
            this.checkBoxCleanup.AutoSize = true;
            this.checkBoxCleanup.Checked = global::FLSAM.Properties.Settings.Default.CleanupEnabled;
            this.checkBoxCleanup.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::FLSAM.Properties.Settings.Default, "CleanupEnabled", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxCleanup.Location = new System.Drawing.Point(14, 20);
            this.checkBoxCleanup.Name = "checkBoxCleanup";
            this.checkBoxCleanup.Size = new System.Drawing.Size(65, 17);
            this.checkBoxCleanup.TabIndex = 0;
            this.checkBoxCleanup.Text = "Enabled";
            this.checkBoxCleanup.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonIonPath);
            this.groupBox1.Controls.Add(this.buttonFLPath);
            this.groupBox1.Controls.Add(this.textIonPath);
            this.groupBox1.Controls.Add(this.textFLPath);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(768, 63);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Paths";
            // 
            // buttonIonPath
            // 
            this.buttonIonPath.Location = new System.Drawing.Point(546, 34);
            this.buttonIonPath.Name = "buttonIonPath";
            this.buttonIonPath.Size = new System.Drawing.Size(75, 20);
            this.buttonIonPath.TabIndex = 5;
            this.buttonIonPath.Text = "Browse";
            this.buttonIonPath.UseVisualStyleBackColor = true;
            this.buttonIonPath.Click += new System.EventHandler(this.buttonIonPath_Click);
            // 
            // buttonFLPath
            // 
            this.buttonFLPath.Location = new System.Drawing.Point(546, 13);
            this.buttonFLPath.Name = "buttonFLPath";
            this.buttonFLPath.Size = new System.Drawing.Size(75, 20);
            this.buttonFLPath.TabIndex = 4;
            this.buttonFLPath.Text = "Browse";
            this.buttonFLPath.UseVisualStyleBackColor = true;
            this.buttonFLPath.Click += new System.EventHandler(this.buttonFLPath_Click);
            // 
            // textIonPath
            // 
            this.textIonPath.Enabled = false;
            this.textIonPath.Location = new System.Drawing.Point(117, 34);
            this.textIonPath.Name = "textIonPath";
            this.textIonPath.Size = new System.Drawing.Size(423, 20);
            this.textIonPath.TabIndex = 3;
            // 
            // textFLPath
            // 
            this.textFLPath.Enabled = false;
            this.textFLPath.Location = new System.Drawing.Point(117, 13);
            this.textFLPath.Name = "textFLPath";
            this.textFLPath.Size = new System.Drawing.Size(423, 20);
            this.textFLPath.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 37);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "IONCROSS directory";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Freelancer directory";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(774, 397);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabTuning
            // 
            this.tabTuning.Controls.Add(this.groupBox3);
            this.tabTuning.Controls.Add(this.label11);
            this.tabTuning.Location = new System.Drawing.Point(4, 22);
            this.tabTuning.Name = "tabTuning";
            this.tabTuning.Size = new System.Drawing.Size(774, 397);
            this.tabTuning.TabIndex = 2;
            this.tabTuning.Text = "Fine-tuning";
            this.tabTuning.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.numericUpDown4);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.numericUpDown3);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Location = new System.Drawing.Point(11, 9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(616, 71);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "DB Queue";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(151, 47);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(20, 13);
            this.label14.TabIndex = 4;
            this.label14.Text = "ms";
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::FLSAM.Properties.Settings.Default, "TuneQTimer", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown4.Location = new System.Drawing.Point(67, 45);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            600000,
            0,
            0,
            0});
            this.numericUpDown4.Minimum = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(78, 20);
            this.numericUpDown4.TabIndex = 3;
            this.numericUpDown4.Value = global::FLSAM.Properties.Settings.Default.TuneQTimer;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(151, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(58, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "commands";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::FLSAM.Properties.Settings.Default, "TuneQThreshold", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown3.Location = new System.Drawing.Point(67, 18);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown3.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(78, 20);
            this.numericUpDown3.TabIndex = 1;
            this.numericUpDown3.Value = global::FLSAM.Properties.Settings.Default.TuneQThreshold;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Threshold";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(633, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(133, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "\"If it ain\'t broke, don\'t fix it\"";
            // 
            // openFileSQLPicker
            // 
            this.openFileSQLPicker.DefaultExt = "db";
            this.openFileSQLPicker.FileName = "openFileDialog1";
            this.openFileSQLPicker.Filter = "SQLite Database (*.db)|*.db|All files|*.*";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 492);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Settings";
            this.Text = "SettingsForm";
            this.tabControl1.ResumeLayout(false);
            this.tabConnection.ResumeLayout(false);
            this.tabConnection.PerformLayout();
            this.tabSQLite.ResumeLayout(false);
            this.tabSQLite.PerformLayout();
            this.tabFLDB.ResumeLayout(false);
            this.tabFLDB.PerformLayout();
            this.tabHook.ResumeLayout(false);
            this.tabHook.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tabControl2.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelCleanupBackup.ResumeLayout(false);
            this.panelCleanupBackup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabTuning.ResumeLayout(false);
            this.tabTuning.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabConnection;
        private System.Windows.Forms.ComboBox comboBackendSelector;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabSQLite;
        private System.Windows.Forms.Button buttonSQLPathSelector;
        private System.Windows.Forms.TextBox textBoxSQLPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabFLDB;
        private System.Windows.Forms.TabPage tabHook;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SaveFileDialog saveFileSQLPicker;
        private System.Windows.Forms.Button buttonClearSQL;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonFLDBPathSelector;
        private System.Windows.Forms.TextBox textBoxFLDBPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserFLDBPath;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonIonPath;
        private System.Windows.Forms.Button buttonFLPath;
        private System.Windows.Forms.TextBox textIonPath;
        private System.Windows.Forms.TextBox textFLPath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxCleanup;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textCleanupBackup;
        private System.Windows.Forms.CheckBox checkBoxCleanBackup;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panelCleanupBackup;
        private System.Windows.Forms.CheckBox checkBoxSQAggressive;
        private System.Windows.Forms.Button buttonSQOpen;
        private System.Windows.Forms.OpenFileDialog openFileSQLPicker;
        private System.Windows.Forms.TabPage tabTuning;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
    }
}