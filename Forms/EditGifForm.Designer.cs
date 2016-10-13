namespace Capshot
{
    partial class EditGifForm
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
            this.actionsTS = new System.Windows.Forms.ToolStrip();
            this.saveTSB = new System.Windows.Forms.ToolStripButton();
            this.saveAsTSB = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.timeLabel = new System.Windows.Forms.ToolStripLabel();
            this.timeSelector = new Capshot.TimeSelectorItem();
            this.speedsTSDDB = new System.Windows.Forms.ToolStripDropDownButton();
            this.speedQuarterTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.speedHalfTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.speedNormalTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.speedDoubleTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.speedQuadrupleTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.frameTSL = new System.Windows.Forms.ToolStripLabel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.actionsTS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // actionsTS
            // 
            this.actionsTS.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.actionsTS.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.actionsTS.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.actionsTS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveTSB,
            this.saveAsTSB,
            this.toolStripSeparator1,
            this.timeLabel,
            this.timeSelector,
            this.speedsTSDDB,
            this.frameTSL});
            this.actionsTS.Location = new System.Drawing.Point(0, 0);
            this.actionsTS.Name = "actionsTS";
            this.actionsTS.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.actionsTS.Size = new System.Drawing.Size(600, 31);
            this.actionsTS.TabIndex = 0;
            // 
            // saveTSB
            // 
            this.saveTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveTSB.Image = global::Capshot.Properties.Resources.save;
            this.saveTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveTSB.Name = "saveTSB";
            this.saveTSB.Size = new System.Drawing.Size(28, 28);
            this.saveTSB.ToolTipText = "Guardar automáticamente (Ctrl+S)";
            this.saveTSB.Click += new System.EventHandler(this.saveTSB_Click);
            // 
            // saveAsTSB
            // 
            this.saveAsTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveAsTSB.Image = global::Capshot.Properties.Resources.saveas;
            this.saveAsTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveAsTSB.Name = "saveAsTSB";
            this.saveAsTSB.Size = new System.Drawing.Size(28, 28);
            this.saveAsTSB.ToolTipText = "Guardar en un directorio elegido (Ctrl+Shift+S)";
            this.saveAsTSB.Click += new System.EventHandler(this.saveAsTSB_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // timeLabel
            // 
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(113, 28);
            this.timeLabel.Text = "Intervalo de tiempo:";
            // 
            // timeSelector
            // 
            this.timeSelector.AutoSize = false;
            this.timeSelector.EndValue = 100;
            this.timeSelector.Maximum = 100;
            this.timeSelector.Minimum = 0;
            this.timeSelector.MinimumDistance = 1;
            this.timeSelector.Name = "timeSelector";
            this.timeSelector.Size = new System.Drawing.Size(120, 28);
            this.timeSelector.StartValue = 0;
            this.timeSelector.Scroll += new System.Windows.Forms.ScrollEventHandler(this.timeSelector_Scroll);
            this.timeSelector.MouseDown += new System.Windows.Forms.MouseEventHandler(this.timeSelector_MouseDown);
            this.timeSelector.MouseUp += new System.Windows.Forms.MouseEventHandler(this.timeSelector_MouseUp);
            // 
            // speedsTSDDB
            // 
            this.speedsTSDDB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.speedsTSDDB.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.speedQuarterTSMI,
            this.speedHalfTSMI,
            this.speedNormalTSMI,
            this.speedDoubleTSMI,
            this.speedQuadrupleTSMI});
            this.speedsTSDDB.Image = global::Capshot.Properties.Resources.speed;
            this.speedsTSDDB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.speedsTSDDB.Name = "speedsTSDDB";
            this.speedsTSDDB.Size = new System.Drawing.Size(37, 28);
            this.speedsTSDDB.Text = "toolStripDropDownButton1";
            // 
            // speedQuarterTSMI
            // 
            this.speedQuarterTSMI.Name = "speedQuarterTSMI";
            this.speedQuarterTSMI.Size = new System.Drawing.Size(103, 22);
            this.speedQuarterTSMI.Text = "x 0.25";
            this.speedQuarterTSMI.Click += new System.EventHandler(this.speedTSMI_Click);
            // 
            // speedHalfTSMI
            // 
            this.speedHalfTSMI.Name = "speedHalfTSMI";
            this.speedHalfTSMI.Size = new System.Drawing.Size(103, 22);
            this.speedHalfTSMI.Text = "x 0.5";
            this.speedHalfTSMI.Click += new System.EventHandler(this.speedTSMI_Click);
            // 
            // speedNormalTSMI
            // 
            this.speedNormalTSMI.Checked = true;
            this.speedNormalTSMI.CheckState = System.Windows.Forms.CheckState.Checked;
            this.speedNormalTSMI.Name = "speedNormalTSMI";
            this.speedNormalTSMI.Size = new System.Drawing.Size(103, 22);
            this.speedNormalTSMI.Text = "x 1";
            this.speedNormalTSMI.Click += new System.EventHandler(this.speedTSMI_Click);
            // 
            // speedDoubleTSMI
            // 
            this.speedDoubleTSMI.Name = "speedDoubleTSMI";
            this.speedDoubleTSMI.Size = new System.Drawing.Size(103, 22);
            this.speedDoubleTSMI.Text = "x 2";
            this.speedDoubleTSMI.Click += new System.EventHandler(this.speedTSMI_Click);
            // 
            // speedQuadrupleTSMI
            // 
            this.speedQuadrupleTSMI.Name = "speedQuadrupleTSMI";
            this.speedQuadrupleTSMI.Size = new System.Drawing.Size(103, 22);
            this.speedQuadrupleTSMI.Text = "x 4";
            this.speedQuadrupleTSMI.Click += new System.EventHandler(this.speedTSMI_Click);
            // 
            // frameTSL
            // 
            this.frameTSL.Name = "frameTSL";
            this.frameTSL.Size = new System.Drawing.Size(60, 28);
            this.frameTSL.Text = "Frame 0/0";
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(600, 400);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.BackColor = System.Drawing.SystemColors.Control;
            this.progressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.progressBar.Location = new System.Drawing.Point(0, 388);
            this.progressBar.Maximum = 1000;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(600, 12);
            this.progressBar.TabIndex = 0;
            this.progressBar.Visible = false;
            // 
            // EditGifForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.actionsTS);
            this.Controls.Add(this.pictureBox);
            this.KeyPreview = true;
            this.Name = "EditGifForm";
            this.Text = "Editar gif";
            this.Load += new System.EventHandler(this.EditGifForm_Load);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.EditForm_PreviewKeyDown);
            this.actionsTS.ResumeLayout(false);
            this.actionsTS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        System.Windows.Forms.ToolStrip actionsTS;
        System.Windows.Forms.ToolStripButton saveTSB;
        System.Windows.Forms.ToolStripButton saveAsTSB;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private TimeSelectorItem timeSelector;
        private System.Windows.Forms.ToolStripDropDownButton speedsTSDDB;
        private System.Windows.Forms.ToolStripMenuItem speedQuarterTSMI;
        private System.Windows.Forms.ToolStripMenuItem speedHalfTSMI;
        private System.Windows.Forms.ToolStripMenuItem speedNormalTSMI;
        private System.Windows.Forms.ToolStripMenuItem speedDoubleTSMI;
        private System.Windows.Forms.ToolStripMenuItem speedQuadrupleTSMI;
        private System.Windows.Forms.ToolStripLabel frameTSL;
        private System.Windows.Forms.ToolStripLabel timeLabel;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}