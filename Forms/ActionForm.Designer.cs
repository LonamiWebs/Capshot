namespace Capshot
{
    partial class ActionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActionForm));
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.clipboardB = new System.Windows.Forms.Button();
            this.saveB = new System.Windows.Forms.Button();
            this.saveAsB = new System.Windows.Forms.Button();
            this.editB = new System.Windows.Forms.Button();
            this.flowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.AutoSize = true;
            this.flowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel.Controls.Add(this.clipboardB);
            this.flowLayoutPanel.Controls.Add(this.saveB);
            this.flowLayoutPanel.Controls.Add(this.saveAsB);
            this.flowLayoutPanel.Controls.Add(this.editB);
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(280, 70);
            this.flowLayoutPanel.TabIndex = 0;
            // 
            // clipboardB
            // 
            this.clipboardB.FlatAppearance.BorderSize = 0;
            this.clipboardB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clipboardB.Image = ((System.Drawing.Image)(resources.GetObject("clipboardB.Image")));
            this.clipboardB.Location = new System.Drawing.Point(3, 3);
            this.clipboardB.Name = "clipboardB";
            this.clipboardB.Size = new System.Drawing.Size(64, 64);
            this.clipboardB.TabIndex = 0;
            this.clipboardB.UseVisualStyleBackColor = true;
            this.clipboardB.Visible = false;
            this.clipboardB.Click += new System.EventHandler(this.clipboardB_Click);
            // 
            // saveB
            // 
            this.saveB.FlatAppearance.BorderSize = 0;
            this.saveB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveB.Image = ((System.Drawing.Image)(resources.GetObject("saveB.Image")));
            this.saveB.Location = new System.Drawing.Point(73, 3);
            this.saveB.Name = "saveB";
            this.saveB.Size = new System.Drawing.Size(64, 64);
            this.saveB.TabIndex = 1;
            this.saveB.UseVisualStyleBackColor = true;
            this.saveB.Visible = false;
            this.saveB.Click += new System.EventHandler(this.saveB_Click);
            // 
            // saveAsB
            // 
            this.saveAsB.FlatAppearance.BorderSize = 0;
            this.saveAsB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveAsB.Image = ((System.Drawing.Image)(resources.GetObject("saveAsB.Image")));
            this.saveAsB.Location = new System.Drawing.Point(143, 3);
            this.saveAsB.Name = "saveAsB";
            this.saveAsB.Size = new System.Drawing.Size(64, 64);
            this.saveAsB.TabIndex = 2;
            this.saveAsB.UseVisualStyleBackColor = true;
            this.saveAsB.Visible = false;
            this.saveAsB.Click += new System.EventHandler(this.saveAsB_Click);
            // 
            // editB
            // 
            this.editB.FlatAppearance.BorderSize = 0;
            this.editB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editB.Image = global::Capshot.Properties.Resources.icon;
            this.editB.Location = new System.Drawing.Point(213, 3);
            this.editB.Name = "editB";
            this.editB.Size = new System.Drawing.Size(64, 64);
            this.editB.TabIndex = 3;
            this.editB.UseVisualStyleBackColor = true;
            this.editB.Visible = false;
            this.editB.Click += new System.EventHandler(this.editB_Click);
            // 
            // ActionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(302, 88);
            this.Controls.Add(this.flowLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ActionForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Actions";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ActionForm_Load);
            this.flowLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        System.Windows.Forms.Button clipboardB;
        System.Windows.Forms.Button saveB;
        System.Windows.Forms.Button saveAsB;
        System.Windows.Forms.Button editB;
    }
}