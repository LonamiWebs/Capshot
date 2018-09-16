namespace Capshot
{
    partial class EditForm
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
            this.clipboardTSB = new System.Windows.Forms.ToolStripButton();
            this.saveTSB = new System.Windows.Forms.ToolStripButton();
            this.saveAsTSB = new System.Windows.Forms.ToolStripButton();
            this.tss1 = new System.Windows.Forms.ToolStripSeparator();
            this.textTSB = new System.Windows.Forms.ToolStripButton();
            this.resizeTSB = new System.Windows.Forms.ToolStripButton();
            this.rotateLeftTSB = new System.Windows.Forms.ToolStripButton();
            this.rotateRight = new System.Windows.Forms.ToolStripButton();
            this.mirrorTSB = new System.Windows.Forms.ToolStripButton();
            this.tss2 = new System.Windows.Forms.ToolStripSeparator();
            this.undoTSB = new System.Windows.Forms.ToolStripButton();
            this.redoTSB = new System.Windows.Forms.ToolStripButton();
            this.clearTSB = new System.Windows.Forms.ToolStripButton();
            this.toolsTS = new System.Windows.Forms.ToolStrip();
            this.penTSB = new System.Windows.Forms.ToolStripButton();
            this.markerTSB = new System.Windows.Forms.ToolStripButton();
            this.eraserTSB = new System.Windows.Forms.ToolStripButton();
            this.blurTSB = new System.Windows.Forms.ToolStripButton();
            this.pixelateTSB = new System.Windows.Forms.ToolStripButton();
            this.colorpickerTSB = new System.Windows.Forms.ToolStripButton();
            this.editControl = new Capshot.EditControl();
            this.actionsTS.SuspendLayout();
            this.toolsTS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editControl)).BeginInit();
            this.SuspendLayout();
            // 
            // actionsTS
            // 
            this.actionsTS.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.actionsTS.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.actionsTS.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.actionsTS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clipboardTSB,
            this.saveTSB,
            this.saveAsTSB,
            this.tss1,
            this.textTSB,
            this.resizeTSB,
            this.rotateLeftTSB,
            this.rotateRight,
            this.mirrorTSB,
            this.tss2,
            this.undoTSB,
            this.redoTSB,
            this.clearTSB});
            this.actionsTS.Location = new System.Drawing.Point(0, 0);
            this.actionsTS.Name = "actionsTS";
            this.actionsTS.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.actionsTS.Size = new System.Drawing.Size(457, 31);
            this.actionsTS.TabIndex = 0;
            // 
            // clipboardTSB
            // 
            this.clipboardTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clipboardTSB.Image = global::Capshot.Properties.Resources.clipboard;
            this.clipboardTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clipboardTSB.Name = "clipboardTSB";
            this.clipboardTSB.Size = new System.Drawing.Size(28, 28);
            this.clipboardTSB.ToolTipText = "Copy to clipboard (Ctrl+C)";
            this.clipboardTSB.Click += new System.EventHandler(this.clipboardTSB_Click);
            // 
            // saveTSB
            // 
            this.saveTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveTSB.Image = global::Capshot.Properties.Resources.save;
            this.saveTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveTSB.Name = "saveTSB";
            this.saveTSB.Size = new System.Drawing.Size(28, 28);
            this.saveTSB.ToolTipText = "Save (Ctrl+S)";
            this.saveTSB.Click += new System.EventHandler(this.saveTSB_Click);
            // 
            // saveAsTSB
            // 
            this.saveAsTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveAsTSB.Image = global::Capshot.Properties.Resources.saveas;
            this.saveAsTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveAsTSB.Name = "saveAsTSB";
            this.saveAsTSB.Size = new System.Drawing.Size(28, 28);
            this.saveAsTSB.ToolTipText = "Save as (Ctrl+Shift+S)";
            this.saveAsTSB.Click += new System.EventHandler(this.saveAsTSB_Click);
            // 
            // tss1
            // 
            this.tss1.Name = "tss1";
            this.tss1.Size = new System.Drawing.Size(6, 31);
            // 
            // textTSB
            // 
            this.textTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.textTSB.Image = global::Capshot.Properties.Resources.text;
            this.textTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.textTSB.Name = "textTSB";
            this.textTSB.Size = new System.Drawing.Size(28, 28);
            this.textTSB.ToolTipText = "Add text";
            this.textTSB.Click += new System.EventHandler(this.textTSB_Click);
            // 
            // resizeTSB
            // 
            this.resizeTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.resizeTSB.Image = global::Capshot.Properties.Resources.resize;
            this.resizeTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.resizeTSB.Name = "resizeTSB";
            this.resizeTSB.Size = new System.Drawing.Size(28, 28);
            this.resizeTSB.ToolTipText = "Change size";
            this.resizeTSB.Click += new System.EventHandler(this.resizeTSB_Click);
            // 
            // rotateLeftTSB
            // 
            this.rotateLeftTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rotateLeftTSB.Image = global::Capshot.Properties.Resources.rotateLeft;
            this.rotateLeftTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rotateLeftTSB.Name = "rotateLeftTSB";
            this.rotateLeftTSB.Size = new System.Drawing.Size(28, 28);
            this.rotateLeftTSB.ToolTipText = "Rotate left";
            this.rotateLeftTSB.Click += new System.EventHandler(this.rotateLeftTSB_Click);
            // 
            // rotateRight
            // 
            this.rotateRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rotateRight.Image = global::Capshot.Properties.Resources.rotateRight;
            this.rotateRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rotateRight.Name = "rotateRight";
            this.rotateRight.Size = new System.Drawing.Size(28, 28);
            this.rotateRight.ToolTipText = "Rotate right";
            this.rotateRight.Click += new System.EventHandler(this.rotateRight_Click);
            // 
            // mirrorTSB
            // 
            this.mirrorTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mirrorTSB.Image = global::Capshot.Properties.Resources.mirror;
            this.mirrorTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mirrorTSB.Name = "mirrorTSB";
            this.mirrorTSB.Size = new System.Drawing.Size(28, 28);
            this.mirrorTSB.ToolTipText = "Mirror";
            this.mirrorTSB.Click += new System.EventHandler(this.mirrorTSB_Click);
            // 
            // tss2
            // 
            this.tss2.Name = "tss2";
            this.tss2.Size = new System.Drawing.Size(6, 31);
            // 
            // undoTSB
            // 
            this.undoTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.undoTSB.Enabled = false;
            this.undoTSB.Image = global::Capshot.Properties.Resources.undo;
            this.undoTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undoTSB.Name = "undoTSB";
            this.undoTSB.Size = new System.Drawing.Size(28, 28);
            this.undoTSB.ToolTipText = "Undo (Ctrl+Z)";
            this.undoTSB.Click += new System.EventHandler(this.undoTSB_Click);
            // 
            // redoTSB
            // 
            this.redoTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.redoTSB.Enabled = false;
            this.redoTSB.Image = global::Capshot.Properties.Resources.redo;
            this.redoTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.redoTSB.Name = "redoTSB";
            this.redoTSB.Size = new System.Drawing.Size(28, 28);
            this.redoTSB.ToolTipText = "Redo (Ctrl+Y)";
            this.redoTSB.Click += new System.EventHandler(this.redoTSB_Click);
            // 
            // clearTSB
            // 
            this.clearTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearTSB.Enabled = false;
            this.clearTSB.Image = global::Capshot.Properties.Resources.clear;
            this.clearTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearTSB.Name = "clearTSB";
            this.clearTSB.Size = new System.Drawing.Size(28, 28);
            this.clearTSB.ToolTipText = "Clear";
            this.clearTSB.Click += new System.EventHandler(this.clearTSB_Click);
            // 
            // toolsTS
            // 
            this.toolsTS.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolsTS.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolsTS.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolsTS.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolsTS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.penTSB,
            this.markerTSB,
            this.eraserTSB,
            this.blurTSB,
            this.pixelateTSB,
            this.colorpickerTSB});
            this.toolsTS.Location = new System.Drawing.Point(0, 31);
            this.toolsTS.Name = "toolsTS";
            this.toolsTS.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolsTS.ShowItemToolTips = false;
            this.toolsTS.Size = new System.Drawing.Size(37, 333);
            this.toolsTS.TabIndex = 1;
            // 
            // penTSB
            // 
            this.penTSB.Checked = true;
            this.penTSB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.penTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.penTSB.Image = global::Capshot.Properties.Resources.pen;
            this.penTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.penTSB.Name = "penTSB";
            this.penTSB.Size = new System.Drawing.Size(34, 36);
            this.penTSB.Click += new System.EventHandler(this.penTSB_Click);
            this.penTSB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.penTSB_MouseDown);
            this.penTSB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.penTSB_MouseUp);
            // 
            // markerTSB
            // 
            this.markerTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.markerTSB.Image = global::Capshot.Properties.Resources.marker;
            this.markerTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.markerTSB.Name = "markerTSB";
            this.markerTSB.Size = new System.Drawing.Size(34, 36);
            this.markerTSB.Click += new System.EventHandler(this.markerTSB_Click);
            this.markerTSB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.markerTSB_MouseDown);
            this.markerTSB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.markerTSB_MouseUp);
            // 
            // eraserTSB
            // 
            this.eraserTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.eraserTSB.Image = global::Capshot.Properties.Resources.eraser;
            this.eraserTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.eraserTSB.Name = "eraserTSB";
            this.eraserTSB.Size = new System.Drawing.Size(34, 36);
            this.eraserTSB.Click += new System.EventHandler(this.eraserTSB_Click);
            // 
            // blurTSB
            // 
            this.blurTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.blurTSB.Image = global::Capshot.Properties.Resources.blur;
            this.blurTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.blurTSB.Name = "blurTSB";
            this.blurTSB.Size = new System.Drawing.Size(34, 36);
            this.blurTSB.Click += new System.EventHandler(this.blurTSB_Click);
            // 
            // pixelateTSB
            // 
            this.pixelateTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pixelateTSB.Image = global::Capshot.Properties.Resources.pixelate;
            this.pixelateTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pixelateTSB.Name = "pixelateTSB";
            this.pixelateTSB.Size = new System.Drawing.Size(34, 36);
            this.pixelateTSB.Click += new System.EventHandler(this.pixelateTSB_Click);
            // 
            // colorpickerTSB
            // 
            this.colorpickerTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.colorpickerTSB.Image = global::Capshot.Properties.Resources.colorpicker;
            this.colorpickerTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.colorpickerTSB.Name = "colorpickerTSB";
            this.colorpickerTSB.Size = new System.Drawing.Size(34, 36);
            this.colorpickerTSB.Click += new System.EventHandler(this.colorpickerTSB_Click);
            // 
            // editControl
            // 
            this.editControl.BackColor = System.Drawing.SystemColors.Control;
            this.editControl.CursorShown = true;
            this.editControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editControl.Location = new System.Drawing.Point(37, 31);
            this.editControl.Name = "editControl";
            this.editControl.Size = new System.Drawing.Size(420, 333);
            this.editControl.TabIndex = 2;
            this.editControl.TabStop = false;
            this.editControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.editControl_MouseUp);
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(457, 364);
            this.Controls.Add(this.editControl);
            this.Controls.Add(this.toolsTS);
            this.Controls.Add(this.actionsTS);
            this.KeyPreview = true;
            this.Name = "EditForm";
            this.Text = "Editar captura";
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.EditForm_PreviewKeyDown);
            this.actionsTS.ResumeLayout(false);
            this.actionsTS.PerformLayout();
            this.toolsTS.ResumeLayout(false);
            this.toolsTS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        System.Windows.Forms.ToolStrip actionsTS;
        System.Windows.Forms.ToolStripButton clipboardTSB;
        System.Windows.Forms.ToolStripButton saveTSB;
        System.Windows.Forms.ToolStripButton saveAsTSB;
        Capshot.EditControl editControl;
        System.Windows.Forms.ToolStripButton undoTSB;
        System.Windows.Forms.ToolStripButton redoTSB;
        System.Windows.Forms.ToolStripButton penTSB;
        System.Windows.Forms.ToolStripButton markerTSB;
        System.Windows.Forms.ToolStripButton eraserTSB;
        System.Windows.Forms.ToolStrip toolsTS;
        System.Windows.Forms.ToolStripButton blurTSB;
        System.Windows.Forms.ToolStripButton pixelateTSB;
        System.Windows.Forms.ToolStripButton clearTSB;
        System.Windows.Forms.ToolStripSeparator tss1;
        System.Windows.Forms.ToolStripButton resizeTSB;
        System.Windows.Forms.ToolStripButton rotateLeftTSB;
        System.Windows.Forms.ToolStripButton rotateRight;
        System.Windows.Forms.ToolStripSeparator tss2;
        System.Windows.Forms.ToolStripButton mirrorTSB;
        private System.Windows.Forms.ToolStripButton textTSB;
        private System.Windows.Forms.ToolStripButton colorpickerTSB;
    }
}