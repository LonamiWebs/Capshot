namespace Capshot
{
    partial class ResizeForm
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
            this.widthTB = new System.Windows.Forms.TextBox();
            this.beforeL = new System.Windows.Forms.Label();
            this.widthL = new System.Windows.Forms.Label();
            this.heightL = new System.Windows.Forms.Label();
            this.heightTB = new System.Windows.Forms.TextBox();
            this.cancelB = new System.Windows.Forms.Button();
            this.acceptB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // widthTB
            // 
            this.widthTB.Location = new System.Drawing.Point(59, 43);
            this.widthTB.MaxLength = 4;
            this.widthTB.Name = "widthTB";
            this.widthTB.Size = new System.Drawing.Size(57, 20);
            this.widthTB.TabIndex = 0;
            this.widthTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.widthTB_KeyDown);
            this.widthTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumbers_KeyPress);
            // 
            // beforeL
            // 
            this.beforeL.AutoSize = true;
            this.beforeL.Location = new System.Drawing.Point(12, 9);
            this.beforeL.Name = "beforeL";
            this.beforeL.Size = new System.Drawing.Size(40, 13);
            this.beforeL.TabIndex = 1;
            this.beforeL.Text = "Antes: ";
            // 
            // widthL
            // 
            this.widthL.AutoSize = true;
            this.widthL.Location = new System.Drawing.Point(12, 46);
            this.widthL.Name = "widthL";
            this.widthL.Size = new System.Drawing.Size(41, 13);
            this.widthL.TabIndex = 2;
            this.widthL.Text = "Ancho:";
            // 
            // heightL
            // 
            this.heightL.AutoSize = true;
            this.heightL.Location = new System.Drawing.Point(136, 46);
            this.heightL.Name = "heightL";
            this.heightL.Size = new System.Drawing.Size(28, 13);
            this.heightL.TabIndex = 3;
            this.heightL.Text = "Alto:";
            // 
            // heightTB
            // 
            this.heightTB.Location = new System.Drawing.Point(170, 43);
            this.heightTB.MaxLength = 4;
            this.heightTB.Name = "heightTB";
            this.heightTB.Size = new System.Drawing.Size(57, 20);
            this.heightTB.TabIndex = 4;
            this.heightTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.heightTB_KeyDown);
            this.heightTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumbers_KeyPress);
            // 
            // cancelB
            // 
            this.cancelB.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelB.FlatAppearance.BorderSize = 0;
            this.cancelB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelB.Location = new System.Drawing.Point(0, 79);
            this.cancelB.Name = "cancelB";
            this.cancelB.Size = new System.Drawing.Size(110, 30);
            this.cancelB.TabIndex = 5;
            this.cancelB.Text = "Cancelar";
            this.cancelB.UseVisualStyleBackColor = true;
            this.cancelB.Click += new System.EventHandler(this.cancelB_Click);
            // 
            // acceptB
            // 
            this.acceptB.FlatAppearance.BorderSize = 0;
            this.acceptB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.acceptB.Location = new System.Drawing.Point(122, 79);
            this.acceptB.Name = "acceptB";
            this.acceptB.Size = new System.Drawing.Size(110, 30);
            this.acceptB.TabIndex = 6;
            this.acceptB.Text = "Aceptar";
            this.acceptB.UseVisualStyleBackColor = true;
            this.acceptB.Click += new System.EventHandler(this.acceptB_Click);
            // 
            // ResizeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelB;
            this.ClientSize = new System.Drawing.Size(234, 111);
            this.Controls.Add(this.acceptB);
            this.Controls.Add(this.cancelB);
            this.Controls.Add(this.heightTB);
            this.Controls.Add(this.heightL);
            this.Controls.Add(this.widthL);
            this.Controls.Add(this.beforeL);
            this.Controls.Add(this.widthTB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ResizeForm";
            this.Text = "Cambiar tamaño";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        System.Windows.Forms.TextBox widthTB;
        System.Windows.Forms.Label beforeL;
        System.Windows.Forms.Label widthL;
        System.Windows.Forms.Label heightL;
        System.Windows.Forms.TextBox heightTB;
        System.Windows.Forms.Button cancelB;
        System.Windows.Forms.Button acceptB;
    }
}