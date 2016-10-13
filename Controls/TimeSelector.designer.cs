partial class TimeSelector
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.trackBarLine = new System.Windows.Forms.Panel();
        this.leftThumb = new System.Windows.Forms.Button();
        this.rightThumb = new System.Windows.Forms.Button();
        this.moveCheckTimer = new System.Windows.Forms.Timer(this.components);
        this.SuspendLayout();
        // 
        // trackBarLine
        // 
        this.trackBarLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
        this.trackBarLine.Location = new System.Drawing.Point(0, 11);
        this.trackBarLine.Name = "trackBarLine";
        this.trackBarLine.Size = new System.Drawing.Size(150, 2);
        this.trackBarLine.TabIndex = 3;
        this.trackBarLine.Paint += new System.Windows.Forms.PaintEventHandler(this.trackBarLine_Paint);
        // 
        // leftThumb
        // 
        this.leftThumb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left)));
        this.leftThumb.Location = new System.Drawing.Point(0, 0);
        this.leftThumb.Name = "leftThumb";
        this.leftThumb.Size = new System.Drawing.Size(12, 24);
        this.leftThumb.TabIndex = 4;
        this.leftThumb.UseVisualStyleBackColor = true;
        this.leftThumb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.leftThumb_MouseDown);
        this.leftThumb.MouseUp += new System.Windows.Forms.MouseEventHandler(this.leftThumb_MouseUp);
        // 
        // rightThumb
        // 
        this.rightThumb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Right)));
        this.rightThumb.Location = new System.Drawing.Point(138, 0);
        this.rightThumb.Name = "rightThumb";
        this.rightThumb.Size = new System.Drawing.Size(12, 24);
        this.rightThumb.TabIndex = 5;
        this.rightThumb.UseVisualStyleBackColor = true;
        this.rightThumb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rightThumb_MouseDown);
        this.rightThumb.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rightThumb_MouseUp);
        // 
        // moveCheckTimer
        // 
        this.moveCheckTimer.Interval = 15;
        this.moveCheckTimer.Tick += new System.EventHandler(this.moveCheckTimer_Tick);
        // 
        // TimeSelector
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.rightThumb);
        this.Controls.Add(this.leftThumb);
        this.Controls.Add(this.trackBarLine);
        this.Name = "TimeSelector";
        this.Size = new System.Drawing.Size(150, 24);
        this.Resize += new System.EventHandler(this.TimeSelector_Resize);
        this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Panel trackBarLine;
    private System.Windows.Forms.Button leftThumb;
    private System.Windows.Forms.Button rightThumb;
    private System.Windows.Forms.Timer moveCheckTimer;
}