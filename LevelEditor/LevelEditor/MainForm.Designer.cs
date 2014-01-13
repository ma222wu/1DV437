namespace LevelEditor
{
    partial class MainForm
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
            this.xnaRenderWindow = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nudSnapGridY = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nudSnapGridX = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbToolTile = new System.Windows.Forms.RadioButton();
            this.rbToolTrigger = new System.Windows.Forms.RadioButton();
            this.rbToolEnemy = new System.Windows.Forms.RadioButton();
            this.rbToolClip = new System.Windows.Forms.RadioButton();
            this.rbToolSelect = new System.Windows.Forms.RadioButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnLoad = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tbTriggerData = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lbCurrentTexture = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.chbTriggersVisible = new System.Windows.Forms.CheckBox();
            this.chbClipsVisible = new System.Windows.Forms.CheckBox();
            this.chbTilesVisible = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nudObjPosY = new System.Windows.Forms.NumericUpDown();
            this.nudObjPosX = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nudObjHeight = new System.Windows.Forms.NumericUpDown();
            this.nudObjWidth = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.nudMasterValue = new System.Windows.Forms.NumericUpDown();
            this.btnObjXMinus = new System.Windows.Forms.Button();
            this.btnObjYMinus = new System.Windows.Forms.Button();
            this.btnObjWidthMinus = new System.Windows.Forms.Button();
            this.btnObjHeightMinus = new System.Windows.Forms.Button();
            this.btnObjXPlus = new System.Windows.Forms.Button();
            this.btnObjYPlus = new System.Windows.Forms.Button();
            this.btnObjWidthPlus = new System.Windows.Forms.Button();
            this.btnObjHeightPlus = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.btnClipsToBrickTiles = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.xnaRenderWindow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSnapGridY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSnapGridX)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjPosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjPosX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMasterValue)).BeginInit();
            this.SuspendLayout();
            // 
            // xnaRenderWindow
            // 
            this.xnaRenderWindow.Location = new System.Drawing.Point(0, 0);
            this.xnaRenderWindow.Name = "xnaRenderWindow";
            this.xnaRenderWindow.Size = new System.Drawing.Size(1024, 574);
            this.xnaRenderWindow.TabIndex = 3;
            this.xnaRenderWindow.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(217, 619);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 13);
            this.label8.TabIndex = 33;
            this.label8.Text = "Y:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(217, 593);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "X:";
            // 
            // nudSnapGridY
            // 
            this.nudSnapGridY.Location = new System.Drawing.Point(240, 617);
            this.nudSnapGridY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudSnapGridY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSnapGridY.Name = "nudSnapGridY";
            this.nudSnapGridY.Size = new System.Drawing.Size(69, 20);
            this.nudSnapGridY.TabIndex = 31;
            this.nudSnapGridY.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(237, 575);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Grid snap";
            // 
            // nudSnapGridX
            // 
            this.nudSnapGridX.Location = new System.Drawing.Point(240, 591);
            this.nudSnapGridX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudSnapGridX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSnapGridX.Name = "nudSnapGridX";
            this.nudSnapGridX.Size = new System.Drawing.Size(69, 20);
            this.nudSnapGridX.TabIndex = 29;
            this.nudSnapGridX.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 591);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 49);
            this.btnSave.TabIndex = 34;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.button1_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbToolTile);
            this.groupBox1.Controls.Add(this.rbToolTrigger);
            this.groupBox1.Controls.Add(this.rbToolEnemy);
            this.groupBox1.Controls.Add(this.rbToolClip);
            this.groupBox1.Controls.Add(this.rbToolSelect);
            this.groupBox1.Location = new System.Drawing.Point(1030, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(183, 153);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tools";
            // 
            // rbToolTile
            // 
            this.rbToolTile.AutoSize = true;
            this.rbToolTile.Location = new System.Drawing.Point(6, 111);
            this.rbToolTile.Name = "rbToolTile";
            this.rbToolTile.Size = new System.Drawing.Size(42, 17);
            this.rbToolTile.TabIndex = 4;
            this.rbToolTile.TabStop = true;
            this.rbToolTile.Text = "Tile";
            this.rbToolTile.UseVisualStyleBackColor = true;
            this.rbToolTile.CheckedChanged += new System.EventHandler(this.rbToolTile_CheckedChanged);
            // 
            // rbToolTrigger
            // 
            this.rbToolTrigger.AutoSize = true;
            this.rbToolTrigger.Location = new System.Drawing.Point(6, 88);
            this.rbToolTrigger.Name = "rbToolTrigger";
            this.rbToolTrigger.Size = new System.Drawing.Size(58, 17);
            this.rbToolTrigger.TabIndex = 3;
            this.rbToolTrigger.TabStop = true;
            this.rbToolTrigger.Text = "Trigger";
            this.rbToolTrigger.UseVisualStyleBackColor = true;
            this.rbToolTrigger.CheckedChanged += new System.EventHandler(this.rbToolTrigger_CheckedChanged);
            // 
            // rbToolEnemy
            // 
            this.rbToolEnemy.AutoSize = true;
            this.rbToolEnemy.Location = new System.Drawing.Point(6, 65);
            this.rbToolEnemy.Name = "rbToolEnemy";
            this.rbToolEnemy.Size = new System.Drawing.Size(57, 17);
            this.rbToolEnemy.TabIndex = 2;
            this.rbToolEnemy.TabStop = true;
            this.rbToolEnemy.Text = "Enemy";
            this.rbToolEnemy.UseVisualStyleBackColor = true;
            this.rbToolEnemy.CheckedChanged += new System.EventHandler(this.rbToolEnemy_CheckedChanged);
            // 
            // rbToolClip
            // 
            this.rbToolClip.AutoSize = true;
            this.rbToolClip.Location = new System.Drawing.Point(6, 42);
            this.rbToolClip.Name = "rbToolClip";
            this.rbToolClip.Size = new System.Drawing.Size(42, 17);
            this.rbToolClip.TabIndex = 1;
            this.rbToolClip.TabStop = true;
            this.rbToolClip.Text = "Clip";
            this.rbToolClip.UseVisualStyleBackColor = true;
            this.rbToolClip.CheckedChanged += new System.EventHandler(this.rbToolClip_CheckedChanged);
            // 
            // rbToolSelect
            // 
            this.rbToolSelect.AutoSize = true;
            this.rbToolSelect.Checked = true;
            this.rbToolSelect.Location = new System.Drawing.Point(6, 19);
            this.rbToolSelect.Name = "rbToolSelect";
            this.rbToolSelect.Size = new System.Drawing.Size(55, 17);
            this.rbToolSelect.TabIndex = 0;
            this.rbToolSelect.TabStop = true;
            this.rbToolSelect.Text = "Select";
            this.rbToolSelect.UseVisualStyleBackColor = true;
            this.rbToolSelect.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(113, 591);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(93, 49);
            this.btnLoad.TabIndex = 36;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "label2";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(1036, 171);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(177, 378);
            this.tabControl1.TabIndex = 38;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbTriggerData);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(169, 352);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Trigger data";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // tbTriggerData
            // 
            this.tbTriggerData.Location = new System.Drawing.Point(8, 17);
            this.tbTriggerData.Name = "tbTriggerData";
            this.tbTriggerData.Size = new System.Drawing.Size(130, 20);
            this.tbTriggerData.TabIndex = 0;
            this.tbTriggerData.TextChanged += new System.EventHandler(this.tbTriggerData_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lbCurrentTexture);
            this.tabPage2.Controls.Add(this.pictureBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(169, 352);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tiles";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lbCurrentTexture
            // 
            this.lbCurrentTexture.FormattingEnabled = true;
            this.lbCurrentTexture.Items.AddRange(new object[] {
            "enemy",
            "GroundTop",
            "GroundTopLeft",
            "GroundTopRight",
            "lavatop",
            "LavaTopLeft",
            "LavaTopRight",
            "bush1",
            "brickwall",
            "topBrickwall",
            "leftBrickwall",
            "ADkeys",
            "arrowdown",
            "arrowleft",
            "arrowright",
            "arrowup",
            "backspacekey",
            "spacekey",
            "enterkey",
            "esckey",
            "mouse1",
            "mouse2"});
            this.lbCurrentTexture.Location = new System.Drawing.Point(6, 13);
            this.lbCurrentTexture.Name = "lbCurrentTexture";
            this.lbCurrentTexture.Size = new System.Drawing.Size(157, 173);
            this.lbCurrentTexture.TabIndex = 1;
            this.lbCurrentTexture.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(6, 202);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(157, 144);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.checkBox5);
            this.tabPage3.Controls.Add(this.checkBox4);
            this.tabPage3.Controls.Add(this.chbTriggersVisible);
            this.tabPage3.Controls.Add(this.chbClipsVisible);
            this.tabPage3.Controls.Add(this.chbTilesVisible);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(169, 352);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Visibility";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(12, 106);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(80, 17);
            this.checkBox5.TabIndex = 4;
            this.checkBox5.Text = "checkBox5";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(12, 83);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(80, 17);
            this.checkBox4.TabIndex = 3;
            this.checkBox4.Text = "checkBox4";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // chbTriggersVisible
            // 
            this.chbTriggersVisible.AutoSize = true;
            this.chbTriggersVisible.Checked = true;
            this.chbTriggersVisible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbTriggersVisible.Location = new System.Drawing.Point(12, 60);
            this.chbTriggersVisible.Name = "chbTriggersVisible";
            this.chbTriggersVisible.Size = new System.Drawing.Size(64, 17);
            this.chbTriggersVisible.TabIndex = 2;
            this.chbTriggersVisible.Text = "Triggers";
            this.chbTriggersVisible.UseVisualStyleBackColor = true;
            // 
            // chbClipsVisible
            // 
            this.chbClipsVisible.AutoSize = true;
            this.chbClipsVisible.Checked = true;
            this.chbClipsVisible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbClipsVisible.Location = new System.Drawing.Point(12, 37);
            this.chbClipsVisible.Name = "chbClipsVisible";
            this.chbClipsVisible.Size = new System.Drawing.Size(48, 17);
            this.chbClipsVisible.TabIndex = 1;
            this.chbClipsVisible.Text = "Clips";
            this.chbClipsVisible.UseVisualStyleBackColor = true;
            // 
            // chbTilesVisible
            // 
            this.chbTilesVisible.AutoSize = true;
            this.chbTilesVisible.Checked = true;
            this.chbTilesVisible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbTilesVisible.Location = new System.Drawing.Point(12, 14);
            this.chbTilesVisible.Name = "chbTilesVisible";
            this.chbTilesVisible.Size = new System.Drawing.Size(48, 17);
            this.chbTilesVisible.TabIndex = 0;
            this.chbTilesVisible.Text = "Tiles";
            this.chbTilesVisible.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(427, 663);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 42;
            this.label3.Text = "Y:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(427, 637);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 41;
            this.label4.Text = "X:";
            // 
            // nudObjPosY
            // 
            this.nudObjPosY.Location = new System.Drawing.Point(450, 661);
            this.nudObjPosY.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudObjPosY.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.nudObjPosY.Name = "nudObjPosY";
            this.nudObjPosY.Size = new System.Drawing.Size(69, 20);
            this.nudObjPosY.TabIndex = 40;
            this.nudObjPosY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudObjPosX
            // 
            this.nudObjPosX.Location = new System.Drawing.Point(450, 635);
            this.nudObjPosX.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudObjPosX.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.nudObjPosX.Name = "nudObjPosX";
            this.nudObjPosX.Size = new System.Drawing.Size(69, 20);
            this.nudObjPosX.TabIndex = 39;
            this.nudObjPosX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(595, 663);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 46;
            this.label5.Text = "Height:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(595, 637);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 45;
            this.label6.Text = "Width:";
            // 
            // nudObjHeight
            // 
            this.nudObjHeight.Location = new System.Drawing.Point(639, 661);
            this.nudObjHeight.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudObjHeight.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.nudObjHeight.Name = "nudObjHeight";
            this.nudObjHeight.Size = new System.Drawing.Size(69, 20);
            this.nudObjHeight.TabIndex = 44;
            this.nudObjHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudObjWidth
            // 
            this.nudObjWidth.Location = new System.Drawing.Point(639, 635);
            this.nudObjWidth.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudObjWidth.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.nudObjWidth.Name = "nudObjWidth";
            this.nudObjWidth.Size = new System.Drawing.Size(69, 20);
            this.nudObjWidth.TabIndex = 43;
            this.nudObjWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(447, 619);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 13);
            this.label9.TabIndex = 47;
            this.label9.Text = "Obj Position";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(636, 619);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 13);
            this.label10.TabIndex = 48;
            this.label10.Text = "Obj Size";
            // 
            // nudMasterValue
            // 
            this.nudMasterValue.Location = new System.Drawing.Point(480, 596);
            this.nudMasterValue.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudMasterValue.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.nudMasterValue.Name = "nudMasterValue";
            this.nudMasterValue.Size = new System.Drawing.Size(142, 20);
            this.nudMasterValue.TabIndex = 49;
            this.nudMasterValue.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            // 
            // btnObjXMinus
            // 
            this.btnObjXMinus.Location = new System.Drawing.Point(396, 632);
            this.btnObjXMinus.Name = "btnObjXMinus";
            this.btnObjXMinus.Size = new System.Drawing.Size(30, 20);
            this.btnObjXMinus.TabIndex = 50;
            this.btnObjXMinus.Text = "-";
            this.btnObjXMinus.UseVisualStyleBackColor = true;
            this.btnObjXMinus.Click += new System.EventHandler(this.btnObjXMinus_Click);
            // 
            // btnObjYMinus
            // 
            this.btnObjYMinus.Location = new System.Drawing.Point(396, 660);
            this.btnObjYMinus.Name = "btnObjYMinus";
            this.btnObjYMinus.Size = new System.Drawing.Size(30, 20);
            this.btnObjYMinus.TabIndex = 51;
            this.btnObjYMinus.Text = "-";
            this.btnObjYMinus.UseVisualStyleBackColor = true;
            this.btnObjYMinus.Click += new System.EventHandler(this.btnObjYMinus_Click);
            // 
            // btnObjWidthMinus
            // 
            this.btnObjWidthMinus.Location = new System.Drawing.Point(561, 632);
            this.btnObjWidthMinus.Name = "btnObjWidthMinus";
            this.btnObjWidthMinus.Size = new System.Drawing.Size(30, 20);
            this.btnObjWidthMinus.TabIndex = 50;
            this.btnObjWidthMinus.Text = "-";
            this.btnObjWidthMinus.UseVisualStyleBackColor = true;
            this.btnObjWidthMinus.Click += new System.EventHandler(this.btnObjWidthMinus_Click);
            // 
            // btnObjHeightMinus
            // 
            this.btnObjHeightMinus.Location = new System.Drawing.Point(561, 660);
            this.btnObjHeightMinus.Name = "btnObjHeightMinus";
            this.btnObjHeightMinus.Size = new System.Drawing.Size(30, 20);
            this.btnObjHeightMinus.TabIndex = 51;
            this.btnObjHeightMinus.Text = "-";
            this.btnObjHeightMinus.UseVisualStyleBackColor = true;
            this.btnObjHeightMinus.Click += new System.EventHandler(this.btnObjHeightMinus_Click);
            // 
            // btnObjXPlus
            // 
            this.btnObjXPlus.Location = new System.Drawing.Point(525, 632);
            this.btnObjXPlus.Name = "btnObjXPlus";
            this.btnObjXPlus.Size = new System.Drawing.Size(30, 20);
            this.btnObjXPlus.TabIndex = 50;
            this.btnObjXPlus.Text = "+";
            this.btnObjXPlus.UseVisualStyleBackColor = true;
            this.btnObjXPlus.Click += new System.EventHandler(this.btnObjXPlus_Click);
            // 
            // btnObjYPlus
            // 
            this.btnObjYPlus.Location = new System.Drawing.Point(525, 660);
            this.btnObjYPlus.Name = "btnObjYPlus";
            this.btnObjYPlus.Size = new System.Drawing.Size(30, 20);
            this.btnObjYPlus.TabIndex = 51;
            this.btnObjYPlus.Text = "+";
            this.btnObjYPlus.UseVisualStyleBackColor = true;
            this.btnObjYPlus.Click += new System.EventHandler(this.btnObjYPlus_Click);
            // 
            // btnObjWidthPlus
            // 
            this.btnObjWidthPlus.Location = new System.Drawing.Point(714, 632);
            this.btnObjWidthPlus.Name = "btnObjWidthPlus";
            this.btnObjWidthPlus.Size = new System.Drawing.Size(30, 20);
            this.btnObjWidthPlus.TabIndex = 50;
            this.btnObjWidthPlus.Text = "+";
            this.btnObjWidthPlus.UseVisualStyleBackColor = true;
            this.btnObjWidthPlus.Click += new System.EventHandler(this.btnObjWidthPlus_Click);
            // 
            // btnObjHeightPlus
            // 
            this.btnObjHeightPlus.Location = new System.Drawing.Point(714, 660);
            this.btnObjHeightPlus.Name = "btnObjHeightPlus";
            this.btnObjHeightPlus.Size = new System.Drawing.Size(30, 20);
            this.btnObjHeightPlus.TabIndex = 51;
            this.btnObjHeightPlus.Text = "+";
            this.btnObjHeightPlus.UseVisualStyleBackColor = true;
            this.btnObjHeightPlus.Click += new System.EventHandler(this.btnObjHeightPlus_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(513, 577);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 13);
            this.label11.TabIndex = 52;
            this.label11.Text = "Master value";
            // 
            // btnClipsToBrickTiles
            // 
            this.btnClipsToBrickTiles.Location = new System.Drawing.Point(820, 608);
            this.btnClipsToBrickTiles.Name = "btnClipsToBrickTiles";
            this.btnClipsToBrickTiles.Size = new System.Drawing.Size(109, 71);
            this.btnClipsToBrickTiles.TabIndex = 53;
            this.btnClipsToBrickTiles.Text = "Add clips to all brick tiles";
            this.btnClipsToBrickTiles.UseVisualStyleBackColor = true;
            this.btnClipsToBrickTiles.Click += new System.EventHandler(this.btnClipsToBrickTiles_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1225, 695);
            this.Controls.Add(this.btnClipsToBrickTiles);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnObjHeightMinus);
            this.Controls.Add(this.btnObjHeightPlus);
            this.Controls.Add(this.btnObjYPlus);
            this.Controls.Add(this.btnObjYMinus);
            this.Controls.Add(this.btnObjWidthMinus);
            this.Controls.Add(this.btnObjWidthPlus);
            this.Controls.Add(this.btnObjXPlus);
            this.Controls.Add(this.btnObjXMinus);
            this.Controls.Add(this.nudMasterValue);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.nudObjHeight);
            this.Controls.Add(this.nudObjWidth);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nudObjPosY);
            this.Controls.Add(this.nudObjPosX);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.nudSnapGridY);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudSnapGridX);
            this.Controls.Add(this.xnaRenderWindow);
            this.Name = "MainForm";
            this.Text = "Potholy level editor";
            ((System.ComponentModel.ISupportInitialize)(this.xnaRenderWindow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSnapGridY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSnapGridX)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjPosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjPosX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMasterValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox xnaRenderWindow;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.NumericUpDown nudSnapGridY;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown nudSnapGridX;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbToolEnemy;
        private System.Windows.Forms.RadioButton rbToolClip;
        private System.Windows.Forms.RadioButton rbToolSelect;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnLoad;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbToolTrigger;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.TextBox tbTriggerData;
        public System.Windows.Forms.ListBox lbCurrentTexture;
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.RadioButton rbToolTile;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox4;
        public System.Windows.Forms.CheckBox chbTriggersVisible;
        public System.Windows.Forms.CheckBox chbClipsVisible;
        public System.Windows.Forms.CheckBox chbTilesVisible;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.NumericUpDown nudObjPosY;
        public System.Windows.Forms.NumericUpDown nudObjPosX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.NumericUpDown nudObjHeight;
        public System.Windows.Forms.NumericUpDown nudObjWidth;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudMasterValue;
        private System.Windows.Forms.Button btnObjXMinus;
        private System.Windows.Forms.Button btnObjYMinus;
        private System.Windows.Forms.Button btnObjWidthMinus;
        private System.Windows.Forms.Button btnObjHeightMinus;
        private System.Windows.Forms.Button btnObjXPlus;
        private System.Windows.Forms.Button btnObjYPlus;
        private System.Windows.Forms.Button btnObjWidthPlus;
        private System.Windows.Forms.Button btnObjHeightPlus;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnClipsToBrickTiles;
    }
}