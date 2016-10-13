namespace Capshot
{
    partial class GifForm
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
        void InitializeComponent()
        {
            this.controlP = new System.Windows.Forms.Panel();
            this.stopB = new System.Windows.Forms.Button();
            this.playPauseB = new System.Windows.Forms.Button();
            this.timeL = new System.Windows.Forms.Label();
            this.controlP.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlP
            // 
            this.controlP.BackColor = System.Drawing.Color.LightGray;
            this.controlP.Controls.Add(this.stopB);
            this.controlP.Controls.Add(this.playPauseB);
            this.controlP.Controls.Add(this.timeL);
            this.controlP.Location = new System.Drawing.Point(0, 0);
            this.controlP.Name = "controlP";
            this.controlP.Size = new System.Drawing.Size(120, 24);
            this.controlP.TabIndex = 0;
            this.controlP.Visible = false;
            // 
            // stopB
            // 
            this.stopB.FlatAppearance.BorderSize = 0;
            this.stopB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopB.Image = global::Capshot.Properties.Resources.stop;
            this.stopB.Location = new System.Drawing.Point(96, 0);
            this.stopB.Name = "stopB";
            this.stopB.Size = new System.Drawing.Size(24, 24);
            this.stopB.TabIndex = 2;
            this.stopB.UseVisualStyleBackColor = true;
            this.stopB.Click += new System.EventHandler(this.stopB_Click);
            // 
            // playPauseB
            // 
            this.playPauseB.FlatAppearance.BorderSize = 0;
            this.playPauseB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playPauseB.Image = global::Capshot.Properties.Resources.pause;
            this.playPauseB.Location = new System.Drawing.Point(72, 0);
            this.playPauseB.Name = "playPauseB";
            this.playPauseB.Size = new System.Drawing.Size(24, 24);
            this.playPauseB.TabIndex = 1;
            this.playPauseB.UseVisualStyleBackColor = true;
            this.playPauseB.Click += new System.EventHandler(this.playPauseB_Click);
            // 
            // timeL
            // 
            this.timeL.AutoSize = true;
            this.timeL.Location = new System.Drawing.Point(0, 6);
            this.timeL.Name = "timeL";
            this.timeL.Size = new System.Drawing.Size(45, 13);
            this.timeL.TabIndex = 0;
            this.timeL.Text = "0.0/x.xs";
            // 
            // GifForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(120, 24);
            this.Controls.Add(this.controlP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GifForm";
            this.ShowInTaskbar = false;
            this.Text = "GifForm";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.White;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.controlP.ResumeLayout(false);
            this.controlP.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel controlP;
        private System.Windows.Forms.Button stopB;
        private System.Windows.Forms.Button playPauseB;
        private System.Windows.Forms.Label timeL;
    }
}