namespace FLSAM.Forms
{
    partial class CharLocation
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.textNick = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkSpace = new System.Windows.Forms.CheckBox();
            this.checkNick = new System.Windows.Forms.CheckBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboSystem = new System.Windows.Forms.ComboBox();
            this.systemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gameInfoSet = new FLSAM.GD.DB.GameInfoSet();
            this.groupDocked = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBase = new System.Windows.Forms.ComboBox();
            this.basesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupSpace = new System.Windows.Forms.GroupBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.numZ = new System.Windows.Forms.NumericUpDown();
            this.numY = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numX = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.systemsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameInfoSet)).BeginInit();
            this.groupDocked.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.basesBindingSource)).BeginInit();
            this.groupSpace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nickname";
            // 
            // textNick
            // 
            this.textNick.Enabled = false;
            this.textNick.Location = new System.Drawing.Point(67, 13);
            this.textNick.Name = "textNick";
            this.textNick.Size = new System.Drawing.Size(284, 20);
            this.textNick.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkSpace);
            this.groupBox1.Controls.Add(this.checkNick);
            this.groupBox1.Controls.Add(this.buttonSave);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboSystem);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textNick);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(363, 110);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // checkSpace
            // 
            this.checkSpace.AutoSize = true;
            this.checkSpace.Location = new System.Drawing.Point(67, 67);
            this.checkSpace.Name = "checkSpace";
            this.checkSpace.Size = new System.Drawing.Size(67, 17);
            this.checkSpace.TabIndex = 8;
            this.checkSpace.Text = "In space";
            this.checkSpace.UseVisualStyleBackColor = true;
            this.checkSpace.CheckedChanged += new System.EventHandler(this.checkSpace_CheckedChanged);
            // 
            // checkNick
            // 
            this.checkNick.AutoSize = true;
            this.checkNick.Location = new System.Drawing.Point(67, 89);
            this.checkNick.Name = "checkNick";
            this.checkNick.Size = new System.Drawing.Size(94, 17);
            this.checkNick.TabIndex = 7;
            this.checkNick.Text = "Use nickname";
            this.checkNick.UseVisualStyleBackColor = true;
            this.checkNick.CheckedChanged += new System.EventHandler(this.checkNick_CheckedChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSave.Location = new System.Drawing.Point(276, 63);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "System";
            // 
            // comboSystem
            // 
            this.comboSystem.DataSource = this.systemsBindingSource;
            this.comboSystem.DisplayMember = "Name";
            this.comboSystem.FormattingEnabled = true;
            this.comboSystem.Location = new System.Drawing.Point(67, 39);
            this.comboSystem.Name = "comboSystem";
            this.comboSystem.Size = new System.Drawing.Size(284, 21);
            this.comboSystem.TabIndex = 4;
            this.comboSystem.ValueMember = "Nickname";
            this.comboSystem.SelectionChangeCommitted += new System.EventHandler(this.comboSystem_SelectionChangeCommitted);
            // 
            // systemsBindingSource
            // 
            this.systemsBindingSource.DataMember = "Systems";
            this.systemsBindingSource.DataSource = this.gameInfoSet;
            // 
            // gameInfoSet
            // 
            this.gameInfoSet.DataSetName = "GameInfoSet";
            this.gameInfoSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // groupDocked
            // 
            this.groupDocked.Controls.Add(this.label3);
            this.groupDocked.Controls.Add(this.comboBase);
            this.groupDocked.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupDocked.Location = new System.Drawing.Point(0, 110);
            this.groupDocked.Name = "groupDocked";
            this.groupDocked.Size = new System.Drawing.Size(363, 60);
            this.groupDocked.TabIndex = 3;
            this.groupDocked.TabStop = false;
            this.groupDocked.Text = "Base\\LastBase";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Base";
            // 
            // comboBase
            // 
            this.comboBase.DataSource = this.basesBindingSource;
            this.comboBase.DisplayMember = "Name";
            this.comboBase.FormattingEnabled = true;
            this.comboBase.Location = new System.Drawing.Point(67, 19);
            this.comboBase.Name = "comboBase";
            this.comboBase.Size = new System.Drawing.Size(284, 21);
            this.comboBase.TabIndex = 0;
            this.comboBase.ValueMember = "Nickname";
            // 
            // basesBindingSource
            // 
            this.basesBindingSource.DataMember = "Bases";
            this.basesBindingSource.DataSource = this.gameInfoSet;
            // 
            // groupSpace
            // 
            this.groupSpace.Controls.Add(this.buttonCancel);
            this.groupSpace.Controls.Add(this.numZ);
            this.groupSpace.Controls.Add(this.numY);
            this.groupSpace.Controls.Add(this.label4);
            this.groupSpace.Controls.Add(this.numX);
            this.groupSpace.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupSpace.Location = new System.Drawing.Point(0, 170);
            this.groupSpace.Name = "groupSpace";
            this.groupSpace.Size = new System.Drawing.Size(363, 59);
            this.groupSpace.TabIndex = 4;
            this.groupSpace.TabStop = false;
            this.groupSpace.Text = "In space";
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(291, 45);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(72, 14);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "button1";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Visible = false;
            // 
            // numZ
            // 
            this.numZ.DecimalPlaces = 4;
            this.numZ.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numZ.Location = new System.Drawing.Point(258, 19);
            this.numZ.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numZ.Minimum = new decimal(new int[] {
            1410065408,
            2,
            0,
            -2147483648});
            this.numZ.Name = "numZ";
            this.numZ.Size = new System.Drawing.Size(93, 20);
            this.numZ.TabIndex = 3;
            // 
            // numY
            // 
            this.numY.DecimalPlaces = 4;
            this.numY.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numY.Location = new System.Drawing.Point(159, 19);
            this.numY.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numY.Minimum = new decimal(new int[] {
            1410065408,
            2,
            0,
            -2147483648});
            this.numY.Name = "numY";
            this.numY.Size = new System.Drawing.Size(93, 20);
            this.numY.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Pos";
            // 
            // numX
            // 
            this.numX.DecimalPlaces = 4;
            this.numX.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numX.Location = new System.Drawing.Point(60, 19);
            this.numX.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numX.Minimum = new decimal(new int[] {
            1410065408,
            2,
            0,
            -2147483648});
            this.numX.Name = "numX";
            this.numX.Size = new System.Drawing.Size(93, 20);
            this.numX.TabIndex = 0;
            // 
            // CharLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(363, 229);
            this.Controls.Add(this.groupSpace);
            this.Controls.Add(this.groupDocked);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CharLocation";
            this.Text = "Change Location";
            this.Shown += new System.EventHandler(this.CharLocation_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.systemsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameInfoSet)).EndInit();
            this.groupDocked.ResumeLayout(false);
            this.groupDocked.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.basesBindingSource)).EndInit();
            this.groupSpace.ResumeLayout(false);
            this.groupSpace.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textNick;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboSystem;
        private System.Windows.Forms.GroupBox groupDocked;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBase;
        private System.Windows.Forms.GroupBox groupSpace;
        private System.Windows.Forms.NumericUpDown numZ;
        private System.Windows.Forms.NumericUpDown numY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numX;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.BindingSource systemsBindingSource;
        private GD.DB.GameInfoSet gameInfoSet;
        private System.Windows.Forms.BindingSource basesBindingSource;
        private System.Windows.Forms.CheckBox checkNick;
        private System.Windows.Forms.CheckBox checkSpace;
    }
}