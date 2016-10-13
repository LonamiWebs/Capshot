using System;
using System.Windows.Forms;

namespace Capshot
{
    public partial class ColorPaletteForm : Form
    {
        string toolName;

        public ColorPaletteForm(string toolName)
        {
            InitializeComponent();
            Icon = Properties.Resources.appiconsmall;

            Left = Cursor.Position.X;
            Top = Cursor.Position.Y;

            this.toolName = toolName;

            foreach (Button b in Controls)
                b.BackColor = Settings.GetValue<SerializableColor>("color" + b.Name[b.Name.Length - 1]);

            hoverTimer.Tick += HoverTimer_Tick;
            hoverTimer.Enabled = true;
        }

        void HoverTimer_Tick(object sender, EventArgs e)
        {
            foreach (Button b in Controls)
            {
                if (b.ClientRectangle.Contains(b.PointToClient(Cursor.Position)))
                    b.FlatAppearance.BorderSize = 1;
                else
                    b.FlatAppearance.BorderSize = 0;
            }
        }

        Timer hoverTimer = new Timer { Interval = 30 };

        void ColorPaletteForm_Deactivate(object sender, EventArgs e)
        {
            End();
        }

        void colorB_MouseEnter(object sender, EventArgs e)
        {
            var b = (Button)sender;

            Settings.SetValue(toolName + "Color", (SerializableColor)b.BackColor);

            End();
        }

        void End()
        {
            hoverTimer.Tick -= HoverTimer_Tick;
            Close();
        }
    }
}
