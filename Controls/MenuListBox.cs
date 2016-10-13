using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capshot
{
    class MenuListBox : ListBox
    {
        public MenuListBox()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            BorderStyle = BorderStyle.None;
            BackColor = SystemColors.Control;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= Items.Count) return;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e = new DrawItemEventArgs(e.Graphics,
                                          e.Font,
                                          e.Bounds,
                                          e.Index,
                                          e.State ^ DrawItemState.Selected,
                                          Color.Black,
                                          Color.White); //Choose the color

            e.DrawBackground();
            if (e.State == DrawItemState.Focus) e.DrawFocusRectangle();
            string text = (Items[e.Index] == null) ? "(null)" : Items[e.Index].ToString();
            using (var brush = new SolidBrush(e.ForeColor))
            {
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                e.Graphics.DrawString(text, e.Font, brush, e.Bounds);
            }
        }
    }
}
