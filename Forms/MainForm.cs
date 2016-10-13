using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Capshot
{
    public partial class MainForm : Form
    {
        #region Variables

        readonly GlobalKeyboardHook gkh = new GlobalKeyboardHook();

        bool NotifyClosing;

        #endregion

        #region Constructor

        public MainForm(bool minimized)
        {
            InitializeComponent();
            Icon = notifyIcon.Icon = Properties.Resources.appiconsmall;

            ToggleVisible(!(minimized || Settings.GetValue<bool>("startMinimized")));

            LoadSettings();

            gkh.HookedKeys.Add(Keys.PrintScreen);
            gkh.KeyDown += Gkh_KeyDown;
        }

        #endregion

        #region Settings

        // Load
        void LoadSettings()
        {
            Settings.Autosave = false;

            clipboardCB.Checked = saveCB.Checked = saveAsCB.Checked = editCB.Checked = false;

            captureCursorCB.Checked = Settings.GetValue<bool>("captureCursor");
            delayNUD.Value = Settings.GetValue<int>("delay") / 1000m;

            var destinations = (ActionForm.Action)Settings.GetValue<int>("destinations");

            if (destinations.HasFlag(ActionForm.Action.Clipboard))
                clipboardCB.Checked = true;
            if (destinations.HasFlag(ActionForm.Action.Save))
                saveCB.Checked = true;
            if (destinations.HasFlag(ActionForm.Action.SaveAs))
                saveAsCB.Checked = true;
            if (destinations.HasFlag(ActionForm.Action.Edit))
                editCB.Checked = true;

            Settings.Autosave = true;
        }

        // Save
        void UpdateSettings()
        {
            if (!Settings.Autosave)
                return;

            ActionForm.Action destinations = 0;

            if (clipboardCB.Checked)
                destinations |= ActionForm.Action.Clipboard;
            if (saveCB.Checked)
                destinations |= ActionForm.Action.Save;
            if (saveAsCB.Checked)
                destinations |= ActionForm.Action.SaveAs;
            if (editCB.Checked)
                destinations |= ActionForm.Action.Edit;

            Settings.SetValue("destinations", (int)destinations);
        }

        // Destination
        void clipboardCB_CheckedChanged(object sender, EventArgs e) { UpdateSettings(); }
        void saveCB_CheckedChanged(object sender, EventArgs e) { UpdateSettings(); }
        void saveAsCB_CheckedChanged(object sender, EventArgs e) { UpdateSettings(); }
        void editCB_CheckedChanged(object sender, EventArgs e) { UpdateSettings(); }

        // Capture cursor
        void captureCursorCB_CheckedChanged(object sender, EventArgs e)
        { Settings.SetValue("captureCursor", captureCursorCB.Checked); }

        // Delay
        void delayNUD_ValueChanged(object sender, EventArgs e)
        { Settings.SetValue("delay", (int)(delayNUD.Value * 1000)); }

        #endregion

        #region Hook
        
        void Gkh_KeyDown(object sender, KeyEventArgs e)
        {
            Thread.Sleep(Settings.GetValue<int>("delay"));
            bool captureCursor = Settings.GetValue<bool>("captureCursor");
            
            if (!e.Shift) // Screenshot
            {
                if (e.Control)
                    ActionForm.PerformAction(Screenshot.CaptureScreen(captureCursor));
                else if (e.Alt)
                    ActionForm.PerformAction(Screenshot.CaptureActiveWindow(captureCursor));
                else
                    new CropForm(Screenshot.CaptureScreen(captureCursor)).Show();
            }
            else // Gif capture
            {
                if (e.Control)
                    new GifForm(captureCursor, Screen.PrimaryScreen.Bounds).Show();
                else if (e.Alt)
                    new GifForm(captureCursor, Screenshot.GetActiveWindowRect()).Show();
                else
                {
                    var rectangle = CropForm.ShowRectangle(Screenshot.CaptureScreen(captureCursor));
                    if (rectangle.Width > 0 && rectangle.Height > 0)
                        new GifForm(captureCursor, rectangle).Show();
                }
            }
        }

        #endregion

        #region Form closing and visibility

        // Hide (or close)
        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !NotifyClosing)
            {
                e.Cancel = true;
                ToggleVisible(false);
            }
            else
                gkh.KeyDown -= Gkh_KeyDown;
        }
        
        void minimizeB_Click(object sender, EventArgs e)
        {
            ToggleVisible(false);
        }

        void closeTSMI_Click(object sender, EventArgs e)
        {
            NotifyClosing = true;
            Close();
        }

        void MainForm_Deactivate(object sender, EventArgs e)
        {
            ToggleVisible(false);
        }

        // Open
        void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                ToggleVisible(true);
        }

        void settingsTSMI_Click(object sender, EventArgs e)
        {
            ToggleVisible(true);
        }

        // Both
        public void ToggleVisible(bool visible)
        {
            Visible = ShowInTaskbar = visible;
            WindowState = visible ? FormWindowState.Normal : FormWindowState.Minimized;

            if (visible)
            {
                LoadSettings();

                Location = Program.GetTopLeftLocationCenterMouse(Size);
                Activate();
            }
            else
            {
                if (Settings.GetValue<bool>("firstTime"))
                {
                    notifyIcon.ShowBalloonTip(5000);
                    Settings.SetValue("firstTime", false);
                }
            }
        }

        #endregion

        #region Advanced settings

        void advancedSettingsB_Click(object sender, EventArgs e)
        {
            var f = (AdvancedSettingsForm)Application.OpenForms["AdvancedSettingsForm"];
            if (f != null)
            {
                f.Show();
                f.Activate();
            }
            else
                new AdvancedSettingsForm().Show();
        }

        #endregion
    }
}
