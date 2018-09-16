namespace Capshot
{
    partial class TextForm
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
            this.fontCB = new System.Windows.Forms.ComboBox();
            this.textTB = new System.Windows.Forms.TextBox();
            this.fontL = new System.Windows.Forms.Label();
            this.textL = new System.Windows.Forms.Label();
            this.cancelB = new System.Windows.Forms.Button();
            this.acceptB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fontCB
            // 
            this.fontCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fontCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fontCB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fontCB.FormattingEnabled = true;
            this.fontCB.Location = new System.Drawing.Point(81, 196);
            this.fontCB.Margin = new System.Windows.Forms.Padding(4);
            this.fontCB.Name = "fontCB";
            this.fontCB.Size = new System.Drawing.Size(133, 24);
            this.fontCB.TabIndex = 1;
            this.fontCB.SelectedIndexChanged += new System.EventHandler(this.fontCB_SelectedIndexChanged);
            // 
            // textTB
            // 
            this.textTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textTB.Location = new System.Drawing.Point(16, 43);
            this.textTB.Margin = new System.Windows.Forms.Padding(4);
            this.textTB.Multiline = true;
            this.textTB.Name = "textTB";
            this.textTB.Size = new System.Drawing.Size(401, 131);
            this.textTB.TabIndex = 0;
            this.textTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textTB_KeyDown);
            // 
            // fontL
            // 
            this.fontL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.fontL.AutoSize = true;
            this.fontL.Location = new System.Drawing.Point(16, 199);
            this.fontL.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.fontL.Name = "fontL";
            this.fontL.Size = new System.Drawing.Size(52, 16);
            this.fontL.TabIndex = 2;
            this.fontL.Text = "Font:";
            // 
            // textL
            // 
            this.textL.AutoSize = true;
            this.textL.Location = new System.Drawing.Point(12, 11);
            this.textL.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.textL.Name = "textL";
            this.textL.Size = new System.Drawing.Size(45, 16);
            this.textL.TabIndex = 3;
            this.textL.Text = "Text:";
            // 
            // cancelB
            // 
            this.cancelB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelB.FlatAppearance.BorderSize = 0;
            this.cancelB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelB.Location = new System.Drawing.Point(226, 182);
            this.cancelB.Margin = new System.Windows.Forms.Padding(4);
            this.cancelB.Name = "cancelB";
            this.cancelB.Size = new System.Drawing.Size(100, 49);
            this.cancelB.TabIndex = 4;
            this.cancelB.Text = "Cancel";
            this.cancelB.UseVisualStyleBackColor = true;
            this.cancelB.Click += new System.EventHandler(this.cancelB_Click);
            // 
            // acceptB
            // 
            this.acceptB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.acceptB.FlatAppearance.BorderSize = 0;
            this.acceptB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.acceptB.Location = new System.Drawing.Point(334, 182);
            this.acceptB.Margin = new System.Windows.Forms.Padding(4);
            this.acceptB.Name = "acceptB";
            this.acceptB.Size = new System.Drawing.Size(100, 49);
            this.acceptB.TabIndex = 5;
            this.acceptB.Text = "Accept";
            this.acceptB.UseVisualStyleBackColor = true;
            this.acceptB.Click += new System.EventHandler(this.acceptB_Click);
            // 
            // TextForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 231);
            this.Controls.Add(this.acceptB);
            this.Controls.Add(this.cancelB);
            this.Controls.Add(this.textL);
            this.Controls.Add(this.fontL);
            this.Controls.Add(this.textTB);
            this.Controls.Add(this.fontCB);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(450, 270);
            this.Name = "TextForm";
            this.Text = "Add text";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox fontCB;
        private System.Windows.Forms.TextBox textTB;
        private System.Windows.Forms.Label fontL;
        private System.Windows.Forms.Label textL;
        private System.Windows.Forms.Button cancelB;
        private System.Windows.Forms.Button acceptB;
    }
}