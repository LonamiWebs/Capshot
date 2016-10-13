using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capshot
{
    public partial class ResizeForm : Form
    {
        const int code0 = 48;
        const int code9 = 57;
        const int backCd = 8;

        static Size SelectedSize;

        public static Size Show(Size size)
        {
            new ResizeForm(size).ShowDialog();
            return SelectedSize;
        }

        ResizeForm(Size size)
        {
            InitializeComponent();
            Icon = Properties.Resources.appiconsmall;
            ActiveControl = widthTB;
            SelectedSize = Size.Empty;

            beforeL.Text += size.Width + "x" + size.Height;
            widthTB.Text = size.Width.ToString();
            heightTB.Text = size.Height.ToString();
        }

        void onlyNumbers_KeyPress(object sender, KeyPressEventArgs e)
        {
            int kc = (int)e.KeyChar;
            if (kc != backCd && (kc < code0 || kc > code9))
                e.Handled = true;
        }

        void cancelB_Click(object sender, EventArgs e)
        {
            Close();
        }

        void acceptB_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrWhiteSpace(widthTB.Text) || string.IsNullOrWhiteSpace(heightTB.Text)))
                SelectedSize = new Size(int.Parse(widthTB.Text), int.Parse(heightTB.Text));

            Close();
        }

        void widthTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = e.SuppressKeyPress = true;
                heightTB.Focus();
                heightTB.SelectAll();
            }
        }

        void heightTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = e.SuppressKeyPress = true;
                acceptB.PerformClick();
            }
        }
    }
}
