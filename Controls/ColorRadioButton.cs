using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capshot
{
    class ColorRadioButton : RadioButton
    {
        [Browsable(true),Category("Appearance")]
        public Color SelectedColor = Color.Red;

        public ColorRadioButton()
        {
            FlatAppearance.BorderSize = 0;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.FillRectangle(new SolidBrush(SelectedColor), 0, 0, Width, Height);
            base.OnPaint(pe);
        }
    }
}
