namespace Capshot
{
    partial class ColorPreviewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorPreviewForm));
            this.colorP = new System.Windows.Forms.Panel();
            this.rgbTB = new System.Windows.Forms.TextBox();
            this.hexTB = new System.Windows.Forms.TextBox();
            this.copyRgbB = new System.Windows.Forms.Button();
            this.copyHexB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // colorP
            // 
            this.colorP.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.colorP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorP.Location = new System.Drawing.Point(12, 14);
            this.colorP.Name = "colorP";
            this.colorP.Size = new System.Drawing.Size(70, 70);
            this.colorP.TabIndex = 0;
            // 
            // rgbTB
            // 
            this.rgbTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rgbTB.Location = new System.Drawing.Point(89, 21);
            this.rgbTB.Name = "rgbTB";
            this.rgbTB.ReadOnly = true;
            this.rgbTB.Size = new System.Drawing.Size(104, 20);
            this.rgbTB.TabIndex = 1;
            this.rgbTB.Click += new System.EventHandler(this.textBox_Click);
            // 
            // hexTB
            // 
            this.hexTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.hexTB.Location = new System.Drawing.Point(89, 59);
            this.hexTB.Name = "hexTB";
            this.hexTB.ReadOnly = true;
            this.hexTB.Size = new System.Drawing.Size(104, 20);
            this.hexTB.TabIndex = 3;
            this.hexTB.Click += new System.EventHandler(this.textBox_Click);
            // 
            // copyRgbB
            // 
            this.copyRgbB.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.copyRgbB.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("copyRgbB.BackgroundImage")));
            this.copyRgbB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.copyRgbB.FlatAppearance.BorderSize = 0;
            this.copyRgbB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.copyRgbB.Location = new System.Drawing.Point(200, 14);
            this.copyRgbB.Name = "copyRgbB";
            this.copyRgbB.Size = new System.Drawing.Size(32, 32);
            this.copyRgbB.TabIndex = 4;
            this.copyRgbB.UseVisualStyleBackColor = true;
            this.copyRgbB.Click += new System.EventHandler(this.copyRgbB_Click);
            // 
            // copyHexB
            // 
            this.copyHexB.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.copyHexB.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("copyHexB.BackgroundImage")));
            this.copyHexB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.copyHexB.FlatAppearance.BorderSize = 0;
            this.copyHexB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.copyHexB.Location = new System.Drawing.Point(200, 52);
            this.copyHexB.Name = "copyHexB";
            this.copyHexB.Size = new System.Drawing.Size(32, 32);
            this.copyHexB.TabIndex = 5;
            this.copyHexB.UseVisualStyleBackColor = true;
            this.copyHexB.Click += new System.EventHandler(this.copyHexB_Click);
            // 
            // ColorPreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 99);
            this.Controls.Add(this.copyHexB);
            this.Controls.Add(this.copyRgbB);
            this.Controls.Add(this.hexTB);
            this.Controls.Add(this.rgbTB);
            this.Controls.Add(this.colorP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColorPreviewForm";
            this.Text = "Color seleccionado";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel colorP;
        private System.Windows.Forms.TextBox rgbTB;
        private System.Windows.Forms.TextBox hexTB;
        private System.Windows.Forms.Button copyRgbB;
        private System.Windows.Forms.Button copyHexB;
    }
}