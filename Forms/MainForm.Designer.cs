namespace Capshot
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.clipboardCB = new System.Windows.Forms.CheckBox();
            this.saveCB = new System.Windows.Forms.CheckBox();
            this.saveAsCB = new System.Windows.Forms.CheckBox();
            this.destinationL = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.editCB = new System.Windows.Forms.CheckBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyCMS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.settingsTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.closeTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.captureCursorCB = new System.Windows.Forms.CheckBox();
            this.advancedSettingsB = new System.Windows.Forms.Button();
            this.minimizeB = new System.Windows.Forms.Button();
            this.delayL = new System.Windows.Forms.Label();
            this.delayNUD = new System.Windows.Forms.NumericUpDown();
            this.notifyCMS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.delayNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // clipboardCB
            // 
            this.clipboardCB.AutoSize = true;
            this.clipboardCB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clipboardCB.Image = global::Capshot.Properties.Resources.clipboard;
            this.clipboardCB.Location = new System.Drawing.Point(13, 40);
            this.clipboardCB.Margin = new System.Windows.Forms.Padding(4);
            this.clipboardCB.Name = "clipboardCB";
            this.clipboardCB.Size = new System.Drawing.Size(76, 64);
            this.clipboardCB.TabIndex = 1;
            this.toolTip.SetToolTip(this.clipboardCB, "Copiar al portapapeles");
            this.clipboardCB.UseVisualStyleBackColor = false;
            this.clipboardCB.CheckedChanged += new System.EventHandler(this.clipboardCB_CheckedChanged);
            // 
            // saveCB
            // 
            this.saveCB.AutoSize = true;
            this.saveCB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveCB.Image = global::Capshot.Properties.Resources.save;
            this.saveCB.Location = new System.Drawing.Point(121, 40);
            this.saveCB.Margin = new System.Windows.Forms.Padding(4);
            this.saveCB.Name = "saveCB";
            this.saveCB.Size = new System.Drawing.Size(76, 64);
            this.saveCB.TabIndex = 2;
            this.toolTip.SetToolTip(this.saveCB, "Guardar automáticamente");
            this.saveCB.UseVisualStyleBackColor = false;
            this.saveCB.CheckedChanged += new System.EventHandler(this.saveCB_CheckedChanged);
            // 
            // saveAsCB
            // 
            this.saveAsCB.AutoSize = true;
            this.saveAsCB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveAsCB.Image = global::Capshot.Properties.Resources.saveas;
            this.saveAsCB.Location = new System.Drawing.Point(231, 40);
            this.saveAsCB.Margin = new System.Windows.Forms.Padding(4);
            this.saveAsCB.Name = "saveAsCB";
            this.saveAsCB.Size = new System.Drawing.Size(76, 64);
            this.saveAsCB.TabIndex = 3;
            this.toolTip.SetToolTip(this.saveAsCB, "Guardarla en un directorio elegido");
            this.saveAsCB.UseVisualStyleBackColor = false;
            this.saveAsCB.CheckedChanged += new System.EventHandler(this.saveAsCB_CheckedChanged);
            // 
            // destinationL
            // 
            this.destinationL.AutoSize = true;
            this.destinationL.Location = new System.Drawing.Point(13, 9);
            this.destinationL.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.destinationL.Name = "destinationL";
            this.destinationL.Size = new System.Drawing.Size(57, 16);
            this.destinationL.TabIndex = 4;
            this.destinationL.Text = "Destination:";
            // 
            // editCB
            // 
            this.editCB.AutoSize = true;
            this.editCB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editCB.Image = global::Capshot.Properties.Resources.icon;
            this.editCB.Location = new System.Drawing.Point(334, 40);
            this.editCB.Margin = new System.Windows.Forms.Padding(4);
            this.editCB.Name = "editCB";
            this.editCB.Size = new System.Drawing.Size(76, 64);
            this.editCB.TabIndex = 5;
            this.toolTip.SetToolTip(this.editCB, "Abrir imagen en el editor");
            this.editCB.UseVisualStyleBackColor = false;
            this.editCB.CheckedChanged += new System.EventHandler(this.editCB_CheckedChanged);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "Capshot will remain in the system tray\r\nTo exit, right click " +
    "in the system tray's icon";
            this.notifyIcon.BalloonTipTitle = "Capshot is running";
            this.notifyIcon.ContextMenuStrip = this.notifyCMS;
            this.notifyIcon.Text = "Capshot";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // notifyCMS
            // 
            this.notifyCMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsTSMI,
            this.closeTSMI});
            this.notifyCMS.Name = "notifyCMS";
            this.notifyCMS.Size = new System.Drawing.Size(113, 48);
            // 
            // settingsTSMI
            // 
            this.settingsTSMI.Image = global::Capshot.Properties.Resources.icon;
            this.settingsTSMI.Name = "settingsTSMI";
            this.settingsTSMI.Size = new System.Drawing.Size(112, 22);
            this.settingsTSMI.Text = "Settings";
            this.settingsTSMI.Click += new System.EventHandler(this.settingsTSMI_Click);
            // 
            // closeTSMI
            // 
            this.closeTSMI.Image = global::Capshot.Properties.Resources.clear;
            this.closeTSMI.Name = "closeTSMI";
            this.closeTSMI.Size = new System.Drawing.Size(112, 22);
            this.closeTSMI.Text = "Close";
            this.closeTSMI.Click += new System.EventHandler(this.closeTSMI_Click);
            // 
            // captureCursorCB
            // 
            this.captureCursorCB.AutoSize = true;
            this.captureCursorCB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.captureCursorCB.Location = new System.Drawing.Point(13, 128);
            this.captureCursorCB.Name = "captureCursorCB";
            this.captureCursorCB.Size = new System.Drawing.Size(115, 20);
            this.captureCursorCB.TabIndex = 17;
            this.captureCursorCB.Text = "Capture mouse";
            this.captureCursorCB.UseVisualStyleBackColor = true;
            this.captureCursorCB.CheckedChanged += new System.EventHandler(this.captureCursorCB_CheckedChanged);
            // 
            // advancedSettingsB
            // 
            this.advancedSettingsB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.advancedSettingsB.FlatAppearance.BorderSize = 0;
            this.advancedSettingsB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.advancedSettingsB.Location = new System.Drawing.Point(253, 169);
            this.advancedSettingsB.Name = "advancedSettingsB";
            this.advancedSettingsB.Size = new System.Drawing.Size(177, 41);
            this.advancedSettingsB.TabIndex = 19;
            this.advancedSettingsB.Text = "Advanced settings";
            this.advancedSettingsB.UseVisualStyleBackColor = true;
            this.advancedSettingsB.Click += new System.EventHandler(this.advancedSettingsB_Click);
            // 
            // minimizeB
            // 
            this.minimizeB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizeB.FlatAppearance.BorderSize = 0;
            this.minimizeB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimizeB.Location = new System.Drawing.Point(0, 169);
            this.minimizeB.Name = "minimizeB";
            this.minimizeB.Size = new System.Drawing.Size(177, 41);
            this.minimizeB.TabIndex = 20;
            this.minimizeB.Text = "Minimize Capshot";
            this.minimizeB.UseVisualStyleBackColor = true;
            this.minimizeB.Click += new System.EventHandler(this.minimizeB_Click);
            // 
            // delayL
            // 
            this.delayL.AutoSize = true;
            this.delayL.Location = new System.Drawing.Point(186, 130);
            this.delayL.Name = "delayL";
            this.delayL.Size = new System.Drawing.Size(148, 16);
            this.delayL.TabIndex = 21;
            this.delayL.Text = "Delay (in seconds):";
            // 
            // delayNUD
            // 
            this.delayNUD.DecimalPlaces = 1;
            this.delayNUD.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.delayNUD.Location = new System.Drawing.Point(340, 128);
            this.delayNUD.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.delayNUD.Name = "delayNUD";
            this.delayNUD.Size = new System.Drawing.Size(64, 22);
            this.delayNUD.TabIndex = 22;
            this.delayNUD.ValueChanged += new System.EventHandler(this.delayNUD_ValueChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(430, 210);
            this.Controls.Add(this.delayNUD);
            this.Controls.Add(this.delayL);
            this.Controls.Add(this.minimizeB);
            this.Controls.Add(this.advancedSettingsB);
            this.Controls.Add(this.captureCursorCB);
            this.Controls.Add(this.editCB);
            this.Controls.Add(this.destinationL);
            this.Controls.Add(this.saveAsCB);
            this.Controls.Add(this.saveCB);
            this.Controls.Add(this.clipboardCB);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = " Capshot";
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.notifyCMS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.delayNUD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        System.Windows.Forms.CheckBox clipboardCB;
        System.Windows.Forms.CheckBox saveCB;
        System.Windows.Forms.CheckBox saveAsCB;
        System.Windows.Forms.Label destinationL;
        System.Windows.Forms.ToolTip toolTip;
        System.Windows.Forms.CheckBox editCB;
        System.Windows.Forms.NotifyIcon notifyIcon;
        System.Windows.Forms.ContextMenuStrip notifyCMS;
        System.Windows.Forms.ToolStripMenuItem settingsTSMI;
        System.Windows.Forms.ToolStripMenuItem closeTSMI;
        System.Windows.Forms.CheckBox captureCursorCB;
        System.Windows.Forms.Button advancedSettingsB;
        System.Windows.Forms.Button minimizeB;
        System.Windows.Forms.Label delayL;
        System.Windows.Forms.NumericUpDown delayNUD;
    }
}

