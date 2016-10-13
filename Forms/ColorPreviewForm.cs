using System;
using System.Drawing;
using System.Windows.Forms;

namespace Capshot
{
    public partial class ColorPreviewForm : Form
    {
        public ColorPreviewForm(Color color)
        {
            InitializeComponent();

            colorP.BackColor = color;
            rgbTB.Text = string.Format("rgb({0},{1},{2})", color.R, color.G, color.B);
            hexTB.Text = ColorTranslator.ToHtml(color);
        }

        void copyRgbB_Click(object sender, EventArgs e) => copyClose(rgbTB.Text);
        void copyHexB_Click(object sender, EventArgs e) => copyClose(hexTB.Text);
        void copyClose(string text)
        {
            Clipboard.SetText(text);
            Close();
        }

        void textBox_Click(object sender, EventArgs e) => ((TextBox)sender).SelectAll();
    }
}
