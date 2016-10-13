using System;
using System.Drawing;
using System.Windows.Forms;

namespace Capshot
{
    public partial class TextForm : Form
    {
        static TextFontFamily tff;
        
        public TextForm()
        {
            InitializeComponent();
            Icon = Properties.Resources.appiconsmall;

            tff = new TextFontFamily("", FontFamily.GenericSansSerif);

            fontCB.BeginUpdate();

            foreach (FontFamily font in FontFamily.Families)
                fontCB.Items.Add(font.Name);
            fontCB.SelectedItem = tff.Family.Name;

            fontCB.EndUpdate();

        }

        public new static TextFontFamily Show()
        {
            new TextForm().ShowDialog();
            return tff;
        }

        void cancelB_Click(object sender, EventArgs e)
        {
            Close();
        }

        void acceptB_Click(object sender, EventArgs e)
        {
            tff.Text = textTB.Text;
            tff.Family = new FontFamily(fontCB.SelectedItem.ToString());

            Close();
        }

        void fontCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            textTB.Font = new Font(new FontFamily(fontCB.SelectedItem.ToString()), textTB.Font.Size);
        }

        void textTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                e.Handled = e.SuppressKeyPress = true;
                acceptB.PerformClick();
            }
        }
    }
}
