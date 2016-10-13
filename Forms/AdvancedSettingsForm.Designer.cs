namespace Capshot
{
    partial class AdvancedSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        System.ComponentModel.IContainer components = null;

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
        void InitializeComponent()
        {
            this.restartSettingsB = new System.Windows.Forms.Button();
            this.visitWebB = new System.Windows.Forms.Button();
            this.autosaveFormatCB = new System.Windows.Forms.ComboBox();
            this.autosaveFolderB = new System.Windows.Forms.Button();
            this.autosaveFormatL = new System.Windows.Forms.Label();
            this.autosaveFormatTB = new System.Windows.Forms.TextBox();
            this.autosaveFolderL = new System.Windows.Forms.Label();
            this.autosaveFolderTB = new System.Windows.Forms.TextBox();
            this.startMinimizedCB = new System.Windows.Forms.CheckBox();
            this.windowsStartupCB = new System.Windows.Forms.CheckBox();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.autosaveP = new System.Windows.Forms.Panel();
            this.autosaveFormatLL = new System.Windows.Forms.LinkLabel();
            this.startupP = new System.Windows.Forms.Panel();
            this.editorOptionsP = new System.Windows.Forms.Panel();
            this.blurSizeNUD = new System.Windows.Forms.NumericUpDown();
            this.pixelateSizeNUD = new System.Windows.Forms.NumericUpDown();
            this.blurSizeL = new System.Windows.Forms.Label();
            this.pixelateSizeL = new System.Windows.Forms.Label();
            this.eraserSizeNUD = new System.Windows.Forms.NumericUpDown();
            this.markerSizeNUD = new System.Windows.Forms.NumericUpDown();
            this.penSizeNUD = new System.Windows.Forms.NumericUpDown();
            this.eraserSizeL = new System.Windows.Forms.Label();
            this.markerSizeL = new System.Windows.Forms.Label();
            this.paletteP = new System.Windows.Forms.Panel();
            this.paletteRB1 = new System.Windows.Forms.RadioButton();
            this.paletteRB2 = new System.Windows.Forms.RadioButton();
            this.paletteRB3 = new System.Windows.Forms.RadioButton();
            this.paletteRB4 = new System.Windows.Forms.RadioButton();
            this.paletteRB8 = new System.Windows.Forms.RadioButton();
            this.paletteRB5 = new System.Windows.Forms.RadioButton();
            this.paletteRB7 = new System.Windows.Forms.RadioButton();
            this.paletteRB6 = new System.Windows.Forms.RadioButton();
            this.colorPickerControl = new Capshot.ColorPickerControl();
            this.previewPB = new System.Windows.Forms.PictureBox();
            this.penSizeL = new System.Windows.Forms.Label();
            this.coloursPaletteL = new System.Windows.Forms.Label();
            this.editorL = new System.Windows.Forms.Label();
            this.panel = new System.Windows.Forms.Panel();
            this.helpP = new System.Windows.Forms.Panel();
            this.startingCapshotTL = new System.Windows.Forms.Label();
            this.startingCapshotL = new System.Windows.Forms.Label();
            this.editorTL = new System.Windows.Forms.Label();
            this.gifOptionsP = new System.Windows.Forms.Panel();
            this.gifMaxTimeNUD = new System.Windows.Forms.NumericUpDown();
            this.gifRepeatNUD = new System.Windows.Forms.NumericUpDown();
            this.gifQualityNUD = new System.Windows.Forms.NumericUpDown();
            this.gifFpsNUD = new System.Windows.Forms.NumericUpDown();
            this.gifRepeatLL = new System.Windows.Forms.LinkLabel();
            this.gifMaxTimeL = new System.Windows.Forms.Label();
            this.gifRepeatL = new System.Windows.Forms.Label();
            this.gifQualityL = new System.Windows.Forms.Label();
            this.gifFpsL = new System.Windows.Forms.Label();
            this.backB = new System.Windows.Forms.Button();
            this.tryEditorB = new System.Windows.Forms.Button();
            this.menuListBox = new Capshot.MenuListBox();
            this.autosaveP.SuspendLayout();
            this.startupP.SuspendLayout();
            this.editorOptionsP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blurSizeNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pixelateSizeNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eraserSizeNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.markerSizeNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.penSizeNUD)).BeginInit();
            this.paletteP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewPB)).BeginInit();
            this.panel.SuspendLayout();
            this.helpP.SuspendLayout();
            this.gifOptionsP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gifMaxTimeNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gifRepeatNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gifQualityNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gifFpsNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // restartSettingsB
            // 
            this.restartSettingsB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.restartSettingsB.FlatAppearance.BorderSize = 0;
            this.restartSettingsB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.restartSettingsB.Location = new System.Drawing.Point(41, 330);
            this.restartSettingsB.Margin = new System.Windows.Forms.Padding(4);
            this.restartSettingsB.Name = "restartSettingsB";
            this.restartSettingsB.Size = new System.Drawing.Size(178, 41);
            this.restartSettingsB.TabIndex = 14;
            this.restartSettingsB.Text = "Reiniciar ajustes";
            this.restartSettingsB.UseVisualStyleBackColor = true;
            this.restartSettingsB.Click += new System.EventHandler(this.restartSettingsB_Click);
            // 
            // visitWebB
            // 
            this.visitWebB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.visitWebB.FlatAppearance.BorderSize = 0;
            this.visitWebB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.visitWebB.Location = new System.Drawing.Point(335, 330);
            this.visitWebB.Margin = new System.Windows.Forms.Padding(4);
            this.visitWebB.Name = "visitWebB";
            this.visitWebB.Size = new System.Drawing.Size(200, 41);
            this.visitWebB.TabIndex = 16;
            this.visitWebB.Text = "Visitar página web del autor";
            this.visitWebB.UseVisualStyleBackColor = true;
            this.visitWebB.Click += new System.EventHandler(this.visitWebB_Click);
            // 
            // autosaveFormatCB
            // 
            this.autosaveFormatCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.autosaveFormatCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.autosaveFormatCB.FormattingEnabled = true;
            this.autosaveFormatCB.Items.AddRange(new object[] {
            ".png",
            ".jpg",
            ".gif"});
            this.autosaveFormatCB.Location = new System.Drawing.Point(319, 101);
            this.autosaveFormatCB.Margin = new System.Windows.Forms.Padding(4);
            this.autosaveFormatCB.Name = "autosaveFormatCB";
            this.autosaveFormatCB.Size = new System.Drawing.Size(72, 24);
            this.autosaveFormatCB.TabIndex = 24;
            this.autosaveFormatCB.SelectedIndexChanged += new System.EventHandler(this.autosaveFormatCB_SelectedIndexChanged);
            this.autosaveFormatCB.TextChanged += new System.EventHandler(this.autosaveFormatCB_SelectedIndexChanged);
            // 
            // autosaveFolderB
            // 
            this.autosaveFolderB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.autosaveFolderB.FlatAppearance.BorderSize = 0;
            this.autosaveFolderB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.autosaveFolderB.Location = new System.Drawing.Point(358, 33);
            this.autosaveFolderB.Margin = new System.Windows.Forms.Padding(4);
            this.autosaveFolderB.Name = "autosaveFolderB";
            this.autosaveFolderB.Size = new System.Drawing.Size(33, 31);
            this.autosaveFolderB.TabIndex = 23;
            this.autosaveFolderB.Text = "...";
            this.autosaveFolderB.UseVisualStyleBackColor = true;
            this.autosaveFolderB.Click += new System.EventHandler(this.autosaveFolderB_Click);
            // 
            // autosaveFormatL
            // 
            this.autosaveFormatL.AutoSize = true;
            this.autosaveFormatL.Location = new System.Drawing.Point(31, 82);
            this.autosaveFormatL.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.autosaveFormatL.Name = "autosaveFormatL";
            this.autosaveFormatL.Size = new System.Drawing.Size(154, 16);
            this.autosaveFormatL.TabIndex = 22;
            this.autosaveFormatL.Text = "Con el siguiente formato:";
            // 
            // autosaveFormatTB
            // 
            this.autosaveFormatTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.autosaveFormatTB.BackColor = System.Drawing.SystemColors.Window;
            this.autosaveFormatTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autosaveFormatTB.Location = new System.Drawing.Point(10, 102);
            this.autosaveFormatTB.Margin = new System.Windows.Forms.Padding(4);
            this.autosaveFormatTB.Name = "autosaveFormatTB";
            this.autosaveFormatTB.Size = new System.Drawing.Size(300, 22);
            this.autosaveFormatTB.TabIndex = 21;
            this.autosaveFormatTB.TextChanged += new System.EventHandler(this.autosaveFormatTB_TextChanged);
            // 
            // autosaveFolderL
            // 
            this.autosaveFolderL.AutoSize = true;
            this.autosaveFolderL.Location = new System.Drawing.Point(6, 11);
            this.autosaveFolderL.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.autosaveFolderL.Name = "autosaveFolderL";
            this.autosaveFolderL.Size = new System.Drawing.Size(195, 16);
            this.autosaveFolderL.TabIndex = 20;
            this.autosaveFolderL.Text = "Guardar en la siguiente carpeta";
            // 
            // autosaveFolderTB
            // 
            this.autosaveFolderTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.autosaveFolderTB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.autosaveFolderTB.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.autosaveFolderTB.BackColor = System.Drawing.SystemColors.Window;
            this.autosaveFolderTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autosaveFolderTB.Location = new System.Drawing.Point(10, 38);
            this.autosaveFolderTB.Margin = new System.Windows.Forms.Padding(4);
            this.autosaveFolderTB.Name = "autosaveFolderTB";
            this.autosaveFolderTB.Size = new System.Drawing.Size(340, 22);
            this.autosaveFolderTB.TabIndex = 19;
            this.autosaveFolderTB.TextChanged += new System.EventHandler(this.autosaveFolderTB_TextChanged);
            // 
            // startMinimizedCB
            // 
            this.startMinimizedCB.AutoSize = true;
            this.startMinimizedCB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startMinimizedCB.Location = new System.Drawing.Point(6, 4);
            this.startMinimizedCB.Margin = new System.Windows.Forms.Padding(4);
            this.startMinimizedCB.Name = "startMinimizedCB";
            this.startMinimizedCB.Size = new System.Drawing.Size(199, 20);
            this.startMinimizedCB.TabIndex = 26;
            this.startMinimizedCB.Text = "Abrir el programa minimizado";
            this.startMinimizedCB.UseVisualStyleBackColor = true;
            this.startMinimizedCB.CheckedChanged += new System.EventHandler(this.startMinimizedCB_CheckedChanged);
            // 
            // windowsStartupCB
            // 
            this.windowsStartupCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.windowsStartupCB.AutoSize = true;
            this.windowsStartupCB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.windowsStartupCB.Location = new System.Drawing.Point(249, 3);
            this.windowsStartupCB.Margin = new System.Windows.Forms.Padding(4);
            this.windowsStartupCB.Name = "windowsStartupCB";
            this.windowsStartupCB.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.windowsStartupCB.Size = new System.Drawing.Size(142, 20);
            this.windowsStartupCB.TabIndex = 25;
            this.windowsStartupCB.Text = "Iniciar con Windows";
            this.windowsStartupCB.UseVisualStyleBackColor = true;
            this.windowsStartupCB.CheckedChanged += new System.EventHandler(this.windowsStartupCB_CheckedChanged);
            // 
            // autosaveP
            // 
            this.autosaveP.Controls.Add(this.autosaveFormatLL);
            this.autosaveP.Controls.Add(this.autosaveFolderL);
            this.autosaveP.Controls.Add(this.autosaveFolderTB);
            this.autosaveP.Controls.Add(this.autosaveFormatTB);
            this.autosaveP.Controls.Add(this.autosaveFormatCB);
            this.autosaveP.Controls.Add(this.autosaveFormatL);
            this.autosaveP.Controls.Add(this.autosaveFolderB);
            this.autosaveP.Location = new System.Drawing.Point(435, 195);
            this.autosaveP.Name = "autosaveP";
            this.autosaveP.Size = new System.Drawing.Size(404, 132);
            this.autosaveP.TabIndex = 28;
            this.autosaveP.Text = "Ajustes del guardado automático";
            this.autosaveP.Visible = false;
            // 
            // autosaveFormatLL
            // 
            this.autosaveFormatLL.AutoSize = true;
            this.autosaveFormatLL.Location = new System.Drawing.Point(6, 82);
            this.autosaveFormatLL.Name = "autosaveFormatLL";
            this.autosaveFormatLL.Size = new System.Drawing.Size(23, 16);
            this.autosaveFormatLL.TabIndex = 25;
            this.autosaveFormatLL.TabStop = true;
            this.autosaveFormatLL.Text = "(?)";
            this.autosaveFormatLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.autosaveFormatLL_LinkClicked);
            // 
            // startupP
            // 
            this.startupP.Controls.Add(this.windowsStartupCB);
            this.startupP.Controls.Add(this.startMinimizedCB);
            this.startupP.Location = new System.Drawing.Point(435, 333);
            this.startupP.Name = "startupP";
            this.startupP.Size = new System.Drawing.Size(404, 27);
            this.startupP.TabIndex = 29;
            this.startupP.Text = "Inicio del programa";
            this.startupP.Visible = false;
            // 
            // editorOptionsP
            // 
            this.editorOptionsP.Controls.Add(this.blurSizeNUD);
            this.editorOptionsP.Controls.Add(this.pixelateSizeNUD);
            this.editorOptionsP.Controls.Add(this.blurSizeL);
            this.editorOptionsP.Controls.Add(this.pixelateSizeL);
            this.editorOptionsP.Controls.Add(this.eraserSizeNUD);
            this.editorOptionsP.Controls.Add(this.markerSizeNUD);
            this.editorOptionsP.Controls.Add(this.penSizeNUD);
            this.editorOptionsP.Controls.Add(this.eraserSizeL);
            this.editorOptionsP.Controls.Add(this.markerSizeL);
            this.editorOptionsP.Controls.Add(this.paletteP);
            this.editorOptionsP.Controls.Add(this.colorPickerControl);
            this.editorOptionsP.Controls.Add(this.previewPB);
            this.editorOptionsP.Controls.Add(this.penSizeL);
            this.editorOptionsP.Controls.Add(this.coloursPaletteL);
            this.editorOptionsP.Location = new System.Drawing.Point(25, 195);
            this.editorOptionsP.Name = "editorOptionsP";
            this.editorOptionsP.Size = new System.Drawing.Size(404, 280);
            this.editorOptionsP.TabIndex = 30;
            this.editorOptionsP.Text = "Opciones del editor";
            this.editorOptionsP.Visible = false;
            // 
            // blurSizeNUD
            // 
            this.blurSizeNUD.Location = new System.Drawing.Point(190, 218);
            this.blurSizeNUD.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.blurSizeNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.blurSizeNUD.Name = "blurSizeNUD";
            this.blurSizeNUD.Size = new System.Drawing.Size(59, 22);
            this.blurSizeNUD.TabIndex = 34;
            this.blurSizeNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.blurSizeNUD.ValueChanged += new System.EventHandler(this.blurSizeNUD_ValueChanged);
            // 
            // pixelateSizeNUD
            // 
            this.pixelateSizeNUD.Location = new System.Drawing.Point(190, 246);
            this.pixelateSizeNUD.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.pixelateSizeNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pixelateSizeNUD.Name = "pixelateSizeNUD";
            this.pixelateSizeNUD.Size = new System.Drawing.Size(59, 22);
            this.pixelateSizeNUD.TabIndex = 33;
            this.pixelateSizeNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pixelateSizeNUD.ValueChanged += new System.EventHandler(this.pixelateSizeNUD_ValueChanged);
            // 
            // blurSizeL
            // 
            this.blurSizeL.AutoSize = true;
            this.blurSizeL.Location = new System.Drawing.Point(25, 220);
            this.blurSizeL.Name = "blurSizeL";
            this.blurSizeL.Size = new System.Drawing.Size(159, 16);
            this.blurSizeL.TabIndex = 32;
            this.blurSizeL.Text = "Tamaño del desenfoque:";
            // 
            // pixelateSizeL
            // 
            this.pixelateSizeL.AutoSize = true;
            this.pixelateSizeL.Location = new System.Drawing.Point(25, 248);
            this.pixelateSizeL.Name = "pixelateSizeL";
            this.pixelateSizeL.Size = new System.Drawing.Size(139, 16);
            this.pixelateSizeL.TabIndex = 31;
            this.pixelateSizeL.Text = "Tamaño del pixelado:";
            // 
            // eraserSizeNUD
            // 
            this.eraserSizeNUD.Location = new System.Drawing.Point(190, 190);
            this.eraserSizeNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.eraserSizeNUD.Name = "eraserSizeNUD";
            this.eraserSizeNUD.Size = new System.Drawing.Size(59, 22);
            this.eraserSizeNUD.TabIndex = 30;
            this.eraserSizeNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.eraserSizeNUD.ValueChanged += new System.EventHandler(this.eraserSizeNUD_ValueChanged);
            this.eraserSizeNUD.Click += new System.EventHandler(this.sizeNUD_Click);
            this.eraserSizeNUD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sizeNUD_KeyDown);
            // 
            // markerSizeNUD
            // 
            this.markerSizeNUD.Location = new System.Drawing.Point(190, 160);
            this.markerSizeNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.markerSizeNUD.Name = "markerSizeNUD";
            this.markerSizeNUD.Size = new System.Drawing.Size(59, 22);
            this.markerSizeNUD.TabIndex = 29;
            this.markerSizeNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.markerSizeNUD.ValueChanged += new System.EventHandler(this.markerSizeNUD_ValueChanged);
            this.markerSizeNUD.Click += new System.EventHandler(this.sizeNUD_Click);
            this.markerSizeNUD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sizeNUD_KeyDown);
            // 
            // penSizeNUD
            // 
            this.penSizeNUD.Location = new System.Drawing.Point(190, 127);
            this.penSizeNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.penSizeNUD.Name = "penSizeNUD";
            this.penSizeNUD.Size = new System.Drawing.Size(59, 22);
            this.penSizeNUD.TabIndex = 28;
            this.penSizeNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.penSizeNUD.ValueChanged += new System.EventHandler(this.penSizeNUD_ValueChanged);
            this.penSizeNUD.Click += new System.EventHandler(this.sizeNUD_Click);
            this.penSizeNUD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sizeNUD_KeyDown);
            // 
            // eraserSizeL
            // 
            this.eraserSizeL.AutoSize = true;
            this.eraserSizeL.Location = new System.Drawing.Point(25, 192);
            this.eraserSizeL.Name = "eraserSizeL";
            this.eraserSizeL.Size = new System.Drawing.Size(139, 16);
            this.eraserSizeL.TabIndex = 27;
            this.eraserSizeL.Text = "Tamaño del borrador:";
            // 
            // markerSizeL
            // 
            this.markerSizeL.AutoSize = true;
            this.markerSizeL.Location = new System.Drawing.Point(25, 162);
            this.markerSizeL.Name = "markerSizeL";
            this.markerSizeL.Size = new System.Drawing.Size(145, 16);
            this.markerSizeL.TabIndex = 26;
            this.markerSizeL.Text = "Tamaño del marcador:";
            // 
            // paletteP
            // 
            this.paletteP.Controls.Add(this.paletteRB1);
            this.paletteP.Controls.Add(this.paletteRB2);
            this.paletteP.Controls.Add(this.paletteRB3);
            this.paletteP.Controls.Add(this.paletteRB4);
            this.paletteP.Controls.Add(this.paletteRB8);
            this.paletteP.Controls.Add(this.paletteRB5);
            this.paletteP.Controls.Add(this.paletteRB7);
            this.paletteP.Controls.Add(this.paletteRB6);
            this.paletteP.Location = new System.Drawing.Point(15, 30);
            this.paletteP.Name = "paletteP";
            this.paletteP.Size = new System.Drawing.Size(146, 70);
            this.paletteP.TabIndex = 25;
            // 
            // paletteRB1
            // 
            this.paletteRB1.FlatAppearance.BorderSize = 0;
            this.paletteRB1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.paletteRB1.Location = new System.Drawing.Point(0, 0);
            this.paletteRB1.Name = "paletteRB1";
            this.paletteRB1.Size = new System.Drawing.Size(32, 32);
            this.paletteRB1.TabIndex = 14;
            this.paletteRB1.TabStop = true;
            this.paletteRB1.CheckedChanged += new System.EventHandler(this.paletteRB_CheckedChanged);
            // 
            // paletteRB2
            // 
            this.paletteRB2.FlatAppearance.BorderSize = 0;
            this.paletteRB2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.paletteRB2.Location = new System.Drawing.Point(38, 0);
            this.paletteRB2.Name = "paletteRB2";
            this.paletteRB2.Size = new System.Drawing.Size(32, 32);
            this.paletteRB2.TabIndex = 15;
            this.paletteRB2.TabStop = true;
            this.paletteRB2.CheckedChanged += new System.EventHandler(this.paletteRB_CheckedChanged);
            // 
            // paletteRB3
            // 
            this.paletteRB3.FlatAppearance.BorderSize = 0;
            this.paletteRB3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.paletteRB3.Location = new System.Drawing.Point(76, 0);
            this.paletteRB3.Name = "paletteRB3";
            this.paletteRB3.Size = new System.Drawing.Size(32, 32);
            this.paletteRB3.TabIndex = 16;
            this.paletteRB3.TabStop = true;
            this.paletteRB3.CheckedChanged += new System.EventHandler(this.paletteRB_CheckedChanged);
            // 
            // paletteRB4
            // 
            this.paletteRB4.FlatAppearance.BorderSize = 0;
            this.paletteRB4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.paletteRB4.Location = new System.Drawing.Point(114, 0);
            this.paletteRB4.Name = "paletteRB4";
            this.paletteRB4.Size = new System.Drawing.Size(32, 32);
            this.paletteRB4.TabIndex = 17;
            this.paletteRB4.TabStop = true;
            this.paletteRB4.CheckedChanged += new System.EventHandler(this.paletteRB_CheckedChanged);
            // 
            // paletteRB8
            // 
            this.paletteRB8.FlatAppearance.BorderSize = 0;
            this.paletteRB8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.paletteRB8.Location = new System.Drawing.Point(114, 38);
            this.paletteRB8.Name = "paletteRB8";
            this.paletteRB8.Size = new System.Drawing.Size(32, 32);
            this.paletteRB8.TabIndex = 21;
            this.paletteRB8.TabStop = true;
            this.paletteRB8.CheckedChanged += new System.EventHandler(this.paletteRB_CheckedChanged);
            // 
            // paletteRB5
            // 
            this.paletteRB5.FlatAppearance.BorderSize = 0;
            this.paletteRB5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.paletteRB5.Location = new System.Drawing.Point(0, 38);
            this.paletteRB5.Name = "paletteRB5";
            this.paletteRB5.Size = new System.Drawing.Size(32, 32);
            this.paletteRB5.TabIndex = 18;
            this.paletteRB5.TabStop = true;
            this.paletteRB5.CheckedChanged += new System.EventHandler(this.paletteRB_CheckedChanged);
            // 
            // paletteRB7
            // 
            this.paletteRB7.FlatAppearance.BorderSize = 0;
            this.paletteRB7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.paletteRB7.Location = new System.Drawing.Point(76, 38);
            this.paletteRB7.Name = "paletteRB7";
            this.paletteRB7.Size = new System.Drawing.Size(32, 32);
            this.paletteRB7.TabIndex = 20;
            this.paletteRB7.TabStop = true;
            this.paletteRB7.CheckedChanged += new System.EventHandler(this.paletteRB_CheckedChanged);
            // 
            // paletteRB6
            // 
            this.paletteRB6.FlatAppearance.BorderSize = 0;
            this.paletteRB6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.paletteRB6.Location = new System.Drawing.Point(38, 38);
            this.paletteRB6.Name = "paletteRB6";
            this.paletteRB6.Size = new System.Drawing.Size(32, 32);
            this.paletteRB6.TabIndex = 19;
            this.paletteRB6.TabStop = true;
            this.paletteRB6.CheckedChanged += new System.EventHandler(this.paletteRB_CheckedChanged);
            // 
            // colorPickerControl
            // 
            this.colorPickerControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.colorPickerControl.Location = new System.Drawing.Point(171, 35);
            this.colorPickerControl.Name = "colorPickerControl";
            this.colorPickerControl.SelectedColor = System.Drawing.Color.Empty;
            this.colorPickerControl.Size = new System.Drawing.Size(224, 69);
            this.colorPickerControl.TabIndex = 12;
            this.colorPickerControl.ValueChanged += new Capshot.ColorPickerControl.ValueDelegate(this.colorPickerControl_ValueChanged);
            // 
            // previewPB
            // 
            this.previewPB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.previewPB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewPB.Location = new System.Drawing.Point(282, 115);
            this.previewPB.Name = "previewPB";
            this.previewPB.Size = new System.Drawing.Size(110, 110);
            this.previewPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.previewPB.TabIndex = 4;
            this.previewPB.TabStop = false;
            // 
            // penSizeL
            // 
            this.penSizeL.AutoSize = true;
            this.penSizeL.Location = new System.Drawing.Point(25, 129);
            this.penSizeL.Name = "penSizeL";
            this.penSizeL.Size = new System.Drawing.Size(140, 16);
            this.penSizeL.TabIndex = 1;
            this.penSizeL.Text = "Tamaño del bolígrafo:";
            // 
            // coloursPaletteL
            // 
            this.coloursPaletteL.AutoSize = true;
            this.coloursPaletteL.Location = new System.Drawing.Point(12, 11);
            this.coloursPaletteL.Name = "coloursPaletteL";
            this.coloursPaletteL.Size = new System.Drawing.Size(117, 16);
            this.coloursPaletteL.TabIndex = 0;
            this.coloursPaletteL.Text = "Paleta de colores:";
            // 
            // editorL
            // 
            this.editorL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editorL.Location = new System.Drawing.Point(15, 234);
            this.editorL.Name = "editorL";
            this.editorL.Size = new System.Drawing.Size(380, 85);
            this.editorL.TabIndex = 31;
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.AutoScroll = true;
            this.panel.BackColor = System.Drawing.Color.White;
            this.panel.Controls.Add(this.helpP);
            this.panel.Controls.Add(this.gifOptionsP);
            this.panel.Controls.Add(this.editorOptionsP);
            this.panel.Controls.Add(this.autosaveP);
            this.panel.Controls.Add(this.startupP);
            this.panel.Location = new System.Drawing.Point(120, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(415, 323);
            this.panel.TabIndex = 31;
            // 
            // helpP
            // 
            this.helpP.Controls.Add(this.startingCapshotTL);
            this.helpP.Controls.Add(this.startingCapshotL);
            this.helpP.Controls.Add(this.editorTL);
            this.helpP.Controls.Add(this.editorL);
            this.helpP.Location = new System.Drawing.Point(25, 481);
            this.helpP.Name = "helpP";
            this.helpP.Size = new System.Drawing.Size(404, 348);
            this.helpP.TabIndex = 30;
            this.helpP.Text = "Opciones del GIF";
            this.helpP.Visible = false;
            // 
            // startingCapshotTL
            // 
            this.startingCapshotTL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.startingCapshotTL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startingCapshotTL.Location = new System.Drawing.Point(0, 4);
            this.startingCapshotTL.Name = "startingCapshotTL";
            this.startingCapshotTL.Size = new System.Drawing.Size(401, 21);
            this.startingCapshotTL.TabIndex = 35;
            this.startingCapshotTL.Text = "Comenzando con Capshot";
            // 
            // startingCapshotL
            // 
            this.startingCapshotL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.startingCapshotL.Location = new System.Drawing.Point(12, 25);
            this.startingCapshotL.Name = "startingCapshotL";
            this.startingCapshotL.Size = new System.Drawing.Size(380, 188);
            this.startingCapshotL.TabIndex = 34;
            // 
            // editorTL
            // 
            this.editorTL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editorTL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editorTL.Location = new System.Drawing.Point(3, 213);
            this.editorTL.Name = "editorTL";
            this.editorTL.Size = new System.Drawing.Size(398, 21);
            this.editorTL.TabIndex = 33;
            this.editorTL.Text = "Sobre el editor";
            // 
            // gifOptionsP
            // 
            this.gifOptionsP.Controls.Add(this.gifMaxTimeNUD);
            this.gifOptionsP.Controls.Add(this.gifRepeatNUD);
            this.gifOptionsP.Controls.Add(this.gifQualityNUD);
            this.gifOptionsP.Controls.Add(this.gifFpsNUD);
            this.gifOptionsP.Controls.Add(this.gifRepeatLL);
            this.gifOptionsP.Controls.Add(this.gifMaxTimeL);
            this.gifOptionsP.Controls.Add(this.gifRepeatL);
            this.gifOptionsP.Controls.Add(this.gifQualityL);
            this.gifOptionsP.Controls.Add(this.gifFpsL);
            this.gifOptionsP.Location = new System.Drawing.Point(435, 366);
            this.gifOptionsP.Name = "gifOptionsP";
            this.gifOptionsP.Size = new System.Drawing.Size(404, 96);
            this.gifOptionsP.TabIndex = 29;
            this.gifOptionsP.Text = "Opciones del GIF";
            this.gifOptionsP.Visible = false;
            // 
            // gifMaxTimeNUD
            // 
            this.gifMaxTimeNUD.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.gifMaxTimeNUD.Location = new System.Drawing.Point(215, 56);
            this.gifMaxTimeNUD.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.gifMaxTimeNUD.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.gifMaxTimeNUD.Name = "gifMaxTimeNUD";
            this.gifMaxTimeNUD.Size = new System.Drawing.Size(48, 22);
            this.gifMaxTimeNUD.TabIndex = 34;
            this.gifMaxTimeNUD.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.gifMaxTimeNUD.ValueChanged += new System.EventHandler(this.gifMaxTimeNUD_ValueChanged);
            // 
            // gifRepeatNUD
            // 
            this.gifRepeatNUD.Location = new System.Drawing.Point(358, 13);
            this.gifRepeatNUD.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.gifRepeatNUD.Name = "gifRepeatNUD";
            this.gifRepeatNUD.Size = new System.Drawing.Size(39, 22);
            this.gifRepeatNUD.TabIndex = 33;
            this.gifRepeatNUD.ValueChanged += new System.EventHandler(this.gifRepeatNUD_ValueChanged);
            // 
            // gifQualityNUD
            // 
            this.gifQualityNUD.Location = new System.Drawing.Point(178, 13);
            this.gifQualityNUD.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.gifQualityNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.gifQualityNUD.Name = "gifQualityNUD";
            this.gifQualityNUD.Size = new System.Drawing.Size(32, 22);
            this.gifQualityNUD.TabIndex = 32;
            this.gifQualityNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.gifQualityNUD.ValueChanged += new System.EventHandler(this.gifQualityNUD_ValueChanged);
            // 
            // gifFpsNUD
            // 
            this.gifFpsNUD.Location = new System.Drawing.Point(49, 13);
            this.gifFpsNUD.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.gifFpsNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.gifFpsNUD.Name = "gifFpsNUD";
            this.gifFpsNUD.Size = new System.Drawing.Size(39, 22);
            this.gifFpsNUD.TabIndex = 31;
            this.gifFpsNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.gifFpsNUD.ValueChanged += new System.EventHandler(this.gifFpsNUD_ValueChanged);
            // 
            // gifRepeatLL
            // 
            this.gifRepeatLL.AutoSize = true;
            this.gifRepeatLL.Location = new System.Drawing.Point(240, 15);
            this.gifRepeatLL.Name = "gifRepeatLL";
            this.gifRepeatLL.Size = new System.Drawing.Size(23, 16);
            this.gifRepeatLL.TabIndex = 26;
            this.gifRepeatLL.TabStop = true;
            this.gifRepeatLL.Text = "(?)";
            this.gifRepeatLL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.gifRepeatLL_LinkClicked);
            // 
            // gifMaxTimeL
            // 
            this.gifMaxTimeL.AutoSize = true;
            this.gifMaxTimeL.Location = new System.Drawing.Point(7, 58);
            this.gifMaxTimeL.Name = "gifMaxTimeL";
            this.gifMaxTimeL.Size = new System.Drawing.Size(202, 16);
            this.gifMaxTimeL.TabIndex = 3;
            this.gifMaxTimeL.Text = "Máxima duración (en segundos):";
            // 
            // gifRepeatL
            // 
            this.gifRepeatL.AutoSize = true;
            this.gifRepeatL.Location = new System.Drawing.Point(269, 15);
            this.gifRepeatL.Name = "gifRepeatL";
            this.gifRepeatL.Size = new System.Drawing.Size(83, 16);
            this.gifRepeatL.TabIndex = 2;
            this.gifRepeatL.Text = "¿Repetir gif?";
            // 
            // gifQualityL
            // 
            this.gifQualityL.AutoSize = true;
            this.gifQualityL.Location = new System.Drawing.Point(114, 15);
            this.gifQualityL.Name = "gifQualityL";
            this.gifQualityL.Size = new System.Drawing.Size(58, 16);
            this.gifQualityL.TabIndex = 1;
            this.gifQualityL.Text = "Calidad:";
            // 
            // gifFpsL
            // 
            this.gifFpsL.AutoSize = true;
            this.gifFpsL.Location = new System.Drawing.Point(6, 15);
            this.gifFpsL.Name = "gifFpsL";
            this.gifFpsL.Size = new System.Drawing.Size(37, 16);
            this.gifFpsL.TabIndex = 0;
            this.gifFpsL.Text = "FPS:";
            // 
            // backB
            // 
            this.backB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.backB.FlatAppearance.BorderSize = 0;
            this.backB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backB.Location = new System.Drawing.Point(0, 330);
            this.backB.Margin = new System.Windows.Forms.Padding(4);
            this.backB.Name = "backB";
            this.backB.Size = new System.Drawing.Size(41, 41);
            this.backB.TabIndex = 32;
            this.backB.Text = "<";
            this.backB.UseVisualStyleBackColor = true;
            this.backB.Click += new System.EventHandler(this.backB_Click);
            // 
            // tryEditorB
            // 
            this.tryEditorB.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tryEditorB.FlatAppearance.BorderSize = 0;
            this.tryEditorB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tryEditorB.Location = new System.Drawing.Point(217, 330);
            this.tryEditorB.Margin = new System.Windows.Forms.Padding(4);
            this.tryEditorB.Name = "tryEditorB";
            this.tryEditorB.Size = new System.Drawing.Size(121, 41);
            this.tryEditorB.TabIndex = 33;
            this.tryEditorB.Text = "Probar editor";
            this.tryEditorB.Click += new System.EventHandler(this.tryEditorB_Click);
            // 
            // menuListBox
            // 
            this.menuListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.menuListBox.BackColor = System.Drawing.SystemColors.Control;
            this.menuListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.menuListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.menuListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuListBox.FormattingEnabled = true;
            this.menuListBox.ItemHeight = 40;
            this.menuListBox.Items.AddRange(new object[] {
            "Ayuda",
            "Editor",
            "Autoguardado",
            "Inicio",
            "GIF"});
            this.menuListBox.Location = new System.Drawing.Point(0, 0);
            this.menuListBox.Name = "menuListBox";
            this.menuListBox.Size = new System.Drawing.Size(120, 320);
            this.menuListBox.TabIndex = 34;
            this.menuListBox.SelectedIndexChanged += new System.EventHandler(this.menuListBox_SelectedIndexChanged);
            // 
            // AdvancedSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 371);
            this.Controls.Add(this.visitWebB);
            this.Controls.Add(this.menuListBox);
            this.Controls.Add(this.backB);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.restartSettingsB);
            this.Controls.Add(this.tryEditorB);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(550, 410);
            this.Name = "AdvancedSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ajustes avanzados";
            this.Load += new System.EventHandler(this.AdvancedSettingsForm_Load_1);
            this.autosaveP.ResumeLayout(false);
            this.autosaveP.PerformLayout();
            this.startupP.ResumeLayout(false);
            this.startupP.PerformLayout();
            this.editorOptionsP.ResumeLayout(false);
            this.editorOptionsP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blurSizeNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pixelateSizeNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eraserSizeNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.markerSizeNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.penSizeNUD)).EndInit();
            this.paletteP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.previewPB)).EndInit();
            this.panel.ResumeLayout(false);
            this.helpP.ResumeLayout(false);
            this.gifOptionsP.ResumeLayout(false);
            this.gifOptionsP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gifMaxTimeNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gifRepeatNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gifQualityNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gifFpsNUD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        System.Windows.Forms.Button restartSettingsB;
        System.Windows.Forms.Button visitWebB;
        System.Windows.Forms.ComboBox autosaveFormatCB;
        System.Windows.Forms.Button autosaveFolderB;
        System.Windows.Forms.Label autosaveFormatL;
        System.Windows.Forms.TextBox autosaveFormatTB;
        System.Windows.Forms.Label autosaveFolderL;
        System.Windows.Forms.TextBox autosaveFolderTB;
        System.Windows.Forms.CheckBox startMinimizedCB;
        System.Windows.Forms.CheckBox windowsStartupCB;
        System.Windows.Forms.FolderBrowserDialog fbd;
        System.Windows.Forms.Panel autosaveP;
        System.Windows.Forms.Panel startupP;
        System.Windows.Forms.Panel editorOptionsP;
        System.Windows.Forms.Label coloursPaletteL;
        System.Windows.Forms.Label penSizeL;
        System.Windows.Forms.PictureBox previewPB;
        ColorPickerControl colorPickerControl;
        System.Windows.Forms.Panel panel;
        System.Windows.Forms.RadioButton paletteRB8;
        System.Windows.Forms.RadioButton paletteRB7;
        System.Windows.Forms.RadioButton paletteRB6;
        System.Windows.Forms.RadioButton paletteRB5;
        System.Windows.Forms.RadioButton paletteRB4;
        System.Windows.Forms.RadioButton paletteRB3;
        System.Windows.Forms.RadioButton paletteRB2;
        System.Windows.Forms.RadioButton paletteRB1;
        System.Windows.Forms.Panel paletteP;
        System.Windows.Forms.Label markerSizeL;
        System.Windows.Forms.Label eraserSizeL;
        System.Windows.Forms.NumericUpDown penSizeNUD;
        System.Windows.Forms.NumericUpDown markerSizeNUD;
        System.Windows.Forms.NumericUpDown eraserSizeNUD;
        System.Windows.Forms.Button backB;
        private System.Windows.Forms.Button tryEditorB;
        private System.Windows.Forms.LinkLabel autosaveFormatLL;
        private System.Windows.Forms.Panel gifOptionsP;
        private MenuListBox menuListBox;
        private System.Windows.Forms.Label gifFpsL;
        private System.Windows.Forms.Label gifQualityL;
        private System.Windows.Forms.Label gifRepeatL;
        private System.Windows.Forms.LinkLabel gifRepeatLL;
        private System.Windows.Forms.Label gifMaxTimeL;
        private System.Windows.Forms.Panel helpP;
        private System.Windows.Forms.Label editorTL;
        private System.Windows.Forms.Label editorL;
        private System.Windows.Forms.Label startingCapshotTL;
        private System.Windows.Forms.Label startingCapshotL;
        private System.Windows.Forms.NumericUpDown gifFpsNUD;
        private System.Windows.Forms.NumericUpDown gifQualityNUD;
        private System.Windows.Forms.NumericUpDown gifRepeatNUD;
        private System.Windows.Forms.NumericUpDown gifMaxTimeNUD;
        private System.Windows.Forms.NumericUpDown blurSizeNUD;
        private System.Windows.Forms.NumericUpDown pixelateSizeNUD;
        private System.Windows.Forms.Label blurSizeL;
        private System.Windows.Forms.Label pixelateSizeL;
    }
}