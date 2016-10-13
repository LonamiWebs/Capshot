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
        const string startingCapshotLText = @"¡Bienvenido a Capshot! Capshot es un sencillo programa que te permite capturar áreas específicas de tu pantalla, así como gifs animados.
Capshot integra un editor para que puedas ocultar o resaltar partes de tus capturas.
Para seleccionar una región, presiona la tecla Impr Pant en tu teclado. Ctrl+Impr Pant capturará toda la pantalla, Alt+Impr Pant la ventana activa, y Shift+Impr Pant capturará un gif animado.

Si todavía tienes dudas, visita la página web del autor. ¡Es un tío majo y te las aclarará todas!";
        const string editorLText = @"Para elegir un color de la paleta en el editor, mantén pulsado el ratón sobre la herramienta durante un tercio de segundo.

Para cambiar el tamaño desde el editor selecciona una herramienta y pulsa Ctrl++ o Ctrl+-, o utiliza la rueda del ratón.";

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

            Text = "Ajustes avanzados - ";
            switch (menuListBox.SelectedIndex)
            {
                case 0:
                    Text += "Ayuda";
                    break;
                case 1:
                    Text += "Opciones del editor";
                    break;
                case 2:
                    Text += "Ajustes del autoguardado";
                    break;
                case 3:
                    Text += "Ajustes del inicio";
                    break;
                case 4:
                    Text += "Ajustes del GIF";
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
                MessageBox.Show("Hubo un problema al cargar los ajustes por lo que serán reiniciados",
                    "Ajustes corruptos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            MessageBox.Show("Se han reiniciado los ajustes", "Ajustes reiniciados",
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
            MessageBox.Show(@"Puedes personalizar con el formato que se guardan las capturas. Por defecto tienen el siguiente formato:
'Captura {dia}-{mes}-{año}_{hora}-{minuto}-{segundo}'
Los campos encapsulados entre corchete se reemplazarán por el día, mes, etc. actual.

Otros ejemplo válido sería:
'Captura tomada un {nombredia} de {nombremes}'
Dará como resultado (por ejemplo) 'Captura tomada un martes de junio'",

"Ayuda sobre el formato (1/2)", MessageBoxButtons.OK, MessageBoxIcon.Information);

            MessageBox.Show(@"Los campos válidos son los siguientes:

{dia} - Se reemplazará por el día actual en formato numérico (p.ej. 17)
{nombredia} - Se reemplazará por el día actual (p.ej. lunes)
{mes} - Se reemplazará por el mes actual en formato numérico (p.ej. 4)
{nombremes} - Se reemplazará por el mes actual (p.ej. febrero)
{año} - Se reemplazará por el año actual (p.ej. 2015)
{hora} - Se reemplazará por la hora actual (p.ej. 13)
{minuto} - Se reemplazará por el minuto actual (p.ej. 47)
{segundo} - Se reemplazará por el segundo actual (p.ej. 02)",

"Ayuda sobre el formato (2/2)", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void gifRepeatLL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Si ajustas las repeticiones a 0, el gif se repetirá indefinidamente. " +
                "Si por el contrario ajustas un número específico, el gif se repetirá ese número de veces " +
                "(nótese que igual el resto de programas ignoran esto)", "Ayuda sobre los GIFs");
        }

        #endregion
    }
}
