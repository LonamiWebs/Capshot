using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Drawing.Drawing2D;

namespace Capshot
{
    public partial class AdvancedSettingsForm : Form
    {
        #region Variables

        Panel[] panels;

        bool changingPalette;
        int selectedSize = 0;

        // I honestly hate using .resx files...
        const string startingCapshotLText = @"Welcome to Capshot! Capshot is a simple program that will allow you to take screenshots and gifs.
Capshot has a builtin editor to hide or highlight parts of your screenshots.
To select a region, press Print Screen in your keyboard. Ctrl+Print Screen will capture the entire screen, Alt+Print Screen the active window, and Shift+Print Screen will record an animated gif.

If you still have questions, visit author's website. He's a nice dude and will be glad to help!";
        const string editorLText = @"To select a colour from the editor's palette, hold the mouse in the tool for a third of a second.

To change the size of a tool from within the editor, press Ctrl++ o Ctrl+-, or use the mouse wheel.";

        #endregion

        #region Constructor, loading and menu

        public AdvancedSettingsForm()
        {
            InitializeComponent();
            Icon = Properties.Resources.appiconsmall;
            startingCapshotL.Text = startingCapshotLText;
            editorL.Text = editorLText;

            panels = new Panel[5];
            panels[0] = helpP;
            panels[1] = editorOptionsP;
            panels[2] = autosaveP;
            panels[3] = startupP;
            panels[4] = gifOptionsP;

            foreach (var gb in panels)
                gb.Dock = DockStyle.Fill;

            LoadSettings();
        }

        void AdvancedSettingsForm_Load_1(object sender, EventArgs e)
        {
            menuListBox.SelectedIndex = 0;
        }

        void menuListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
                panels[i].Visible = i == menuListBox.SelectedIndex;

            Text = "Advanced settings - ";
            switch (menuListBox.SelectedIndex)
            {
                case 0:
                    Text += "Help";
                    break;
                case 1:
                    Text += "Editor settings";
                    break;
                case 2:
                    Text += "Autosave settings";
                    break;
                case 3:
                    Text += "Startup settings";
                    break;
                case 4:
                    Text += "GIF settings";
                    break;
            }
        }

        #endregion

        #region Settings

        // Load settings
        void LoadSettings()
        {
            windowsStartupCB.Checked = GetStartup();

            try
            {
                // Autosave
                autosaveFolderTB.Text = Settings.GetValue<string>("autosaveFolder");
                autosaveFormatTB.Text = Settings.GetValue<string>("autosaveFormat");
                autosaveFormatCB.Text = Settings.GetValue<string>("autosaveExtension");

                // Start mode
                startMinimizedCB.Checked = Settings.GetValue<bool>("startMinimized");

                // Editor
                foreach (RadioButton rb in paletteP.Controls)
                    rb.BackColor = Settings.GetValue<SerializableColor>("color" + rb.Name[rb.Name.Length - 1]);
                penSizeNUD.Value = Settings.GetValue<int>("penSize");
                markerSizeNUD.Value = Settings.GetValue<int>("markerSize");
                eraserSizeNUD.Value = Settings.GetValue<int>("eraserSize");
                blurSizeNUD.Value = Settings.GetValue<int>("blurSize");
                pixelateSizeNUD.Value = Settings.GetValue<int>("pixelateSize");
                paletteRB1.Checked = false;
                paletteRB1.Checked = true;

                // Gif
                gifMaxTimeNUD.Value = (decimal)Settings.GetValue<float>("gifMax");
                gifFpsNUD.Value = (decimal)Settings.GetValue<float>("gifFps");
                gifQualityNUD.Value = Settings.GetValue<int>("gifQuality");
                gifRepeatNUD.Value = Settings.GetValue<int>("gifRepeat");
            }
            catch
            {
                MessageBox.Show("There was a problem loading settings so they will be reset",
                    "Corrupt settings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RestartSettings();
            }
        }

        // Restart settings
        void restartSettingsB_Click(object sender, EventArgs e)
        {
            RestartSettings();
        }

        void RestartSettings()
        {
            Settings.Reset();
            SetStartup(false);

            LoadSettings();

            MessageBox.Show("The settings were reset", "Settings reset",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region Autosave

        // Autosave folder select
        void autosaveFolderB_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() == DialogResult.OK)
                autosaveFolderTB.Text = fbd.SelectedPath;
        }

        // Autosave folder
        void autosaveFolderTB_TextChanged(object sender, EventArgs e)
        {
            if (ValidFilePath(autosaveFolderTB.Text))
                Settings.SetValue("autosaveFolder", autosaveFolderTB.Text);
        }

        // Autosave format
        void autosaveFormatTB_TextChanged(object sender, EventArgs e)
        {
            if (autosaveFormatTB.Text.Length > 0)
                Settings.SetValue("autosaveFormat", autosaveFormatTB.Text);
        }

        // Autosave extension
        void autosaveFormatCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.SetValue("autosaveExtension", autosaveFormatCB.Text);
        }

        #endregion

        #region Startup

        // Start minimized
        void startMinimizedCB_CheckedChanged(object sender, EventArgs e)
        {
            Settings.SetValue("startMinimized", startMinimizedCB.Checked);
        }

        // Windows startup
        void windowsStartupCB_CheckedChanged(object sender, EventArgs e)
        {
            SetStartup(windowsStartupCB.Checked);
        }

        // Utils
        void SetStartup(bool enabled)
        {
            var registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (enabled)
                registryKey.SetValue("Capshot", Application.ExecutablePath);

            else if (registryKey.GetValue("Capshot") != null)
                registryKey.DeleteValue("Capshot");
        }

        bool GetStartup()
        {
            return Registry.CurrentUser
                .OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run")
                .GetValue("Capshot") != null;
        }

        #endregion

        #region Editor

        void paletteRB_CheckedChanged(object sender, EventArgs e)
        {
            var rb = (RadioButton)sender;

            if (rb.Checked)
            {
                changingPalette = true;
                colorPickerControl.SelectedColor = rb.BackColor;
                RefreshPreview();
                changingPalette = false;
            }
        }

        void colorPickerControl_ValueChanged(Color color)
        {
            selectedSize = 0;
            if (changingPalette)
                return;

            char rp = '0'; // selected Radio Palette
            foreach (RadioButton rb in paletteP.Controls)
                if (rb.Checked)
                {
                    rb.BackColor = color;
                    rp = rb.Name[rb.Name.Length - 1];
                    break;
                }

            Settings.SetValue("color" + rp, (SerializableColor)color);
            RefreshPreview();
        }


        void penSizeNUD_ValueChanged(object sender, EventArgs e)
        {
            selectedSize = (int)penSizeNUD.Value;
            Settings.SetValue("penSize", selectedSize);

            RefreshPreview();
        }

        void markerSizeNUD_ValueChanged(object sender, EventArgs e)
        {
            selectedSize = (int)markerSizeNUD.Value;
            Settings.SetValue("markerSize", selectedSize);

            RefreshPreview();
        }

        void eraserSizeNUD_ValueChanged(object sender, EventArgs e)
        {
            selectedSize = (int)eraserSizeNUD.Value;
            Settings.SetValue("eraserSize", selectedSize);

            RefreshPreview();
        }

        void blurSizeNUD_ValueChanged(object sender, EventArgs e)
        {
            selectedSize = (int)blurSizeNUD.Value;
            Settings.SetValue("blurSize", selectedSize);

            RefreshPreview();
        }

        void pixelateSizeNUD_ValueChanged(object sender, EventArgs e)
        {
            selectedSize = (int)pixelateSizeNUD.Value;
            Settings.SetValue("pixelateSize", selectedSize);

            RefreshPreview();
        }

        void sizeNUD_Click(object sender, EventArgs e)
        {
            selectedSize = (int)((NumericUpDown)sender).Value;
            RefreshPreview();
        }

        void sizeNUD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                e.Handled = e.SuppressKeyPress = true;
        }

        // Preview and utils
        void RefreshPreview()
        {
            var bmp = new Bitmap(previewPB.Width, previewPB.Height); // Extra padding
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                if (selectedSize > 0)
                {
                    g.Clear(WhitePreferrable(colorPickerControl.SelectedColor) ? Color.White : Color.Black);
                    g.FillEllipse(new SolidBrush(colorPickerControl.SelectedColor),
                        (bmp.Width - selectedSize) / 2f, (bmp.Height - selectedSize) / 2f,
                        selectedSize, selectedSize);
                }
                else
                    g.Clear(colorPickerControl.SelectedColor);
            }
            previewPB.Image = bmp;
        }

        bool WhitePreferrable(Color color)
        {
            return
                color.R * 0.299 +
                color.G * 0.587 +
                color.B * 0.114 < 186;
        }

        #endregion

        #region Gif

        // Frames per second
        void gifFpsNUD_ValueChanged(object sender, EventArgs e)
        {
            Settings.SetValue("gifFps", (float)gifFpsNUD.Value);
        }

        // Gif quality?
        void gifQualityNUD_ValueChanged(object sender, EventArgs e)
        {
            Settings.SetValue("gifQuality", (int)gifQualityNUD.Value);
        }

        // Repeat infinite or n times
        void gifRepeatNUD_ValueChanged(object sender, EventArgs e)
        {
            Settings.SetValue("gifRepeat", (int)gifRepeatNUD.Value);
        }

        // Maximum recording time
        void gifMaxTimeNUD_ValueChanged(object sender, EventArgs e)
        {
            Settings.SetValue("gifMax", (float)gifMaxTimeNUD.Value);
        }

        #endregion

        #endregion

        #region Utils

        public static bool ValidFilePath(string path)
        {
            List<char> invalidChars = Path.GetInvalidFileNameChars().ToList();
            if (path.Contains("\\") && path.Count(c => c == ':') == 1)
            {
                invalidChars.Remove('\\');
                invalidChars.Remove(':');
                return path.Trim('\\') == path && path.IndexOfAny(invalidChars.ToArray()) < 0;
            }
            return !String.IsNullOrWhiteSpace(path) && path.IndexOfAny(Path.GetInvalidFileNameChars()) < 0;
        }

        #endregion

        #region Spam and controls

        void visitWebB_Click(object sender, EventArgs e)
        { Process.Start("http://lonamiwebs.github.io"); }

        void tryEditorB_Click(object sender, EventArgs e)
        { new EditForm().Show(); }

        void backB_Click(object sender, EventArgs e)
        {
            Program.mainForm.ToggleVisible(true);
            Close();
        }

        #endregion

        #region Help (?)

        void autosaveFormatLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(@"You can customize the format in which screenshots are saved. By default they have the following format:
'Screenshot_{day}-{month}-{year}_{hour}-{minute}-{second}'
The fields between braces will be replaced with the current day, month, etc.

Another valid example is:
'Screenshot taken a {dayname} of {daymonth}'
Will for example show 'Screenshot taken a Tuesday of June'",

"Format help (1/2)", MessageBoxButtons.OK, MessageBoxIcon.Information);

            MessageBox.Show(@"Valid fields are:

{day} - Current day as a number (e.g. 17)
{dayname} - Current day name (e.g. lunes)
{month} - Current month as a number (e.g. 4)
{monthname} - Current month name (e.g. febrero)
{year} - Current year (e.g. 2015)
{hour} - Current hour (e.g. 13)
{minute} - Current minute (e.g. 47)
{second} - Current second (e.g. 02)",

"Format help (2/2)", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void gifRepeatLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("If you set the repetitions to 0, gif will repeat forever. " +
                "If you set a non-zero number, gif will repeat those many times " +
                "(note that programs are free to ignore this)", "GIFs help");
        }

        #endregion
    }
}
