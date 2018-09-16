using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System;

namespace Capshot
{
    public partial class EditForm : Form
    {
        #region Variables

        // The timer (delay) to show the colors
        Timer sec1Timer = new Timer() { Interval = 300 };
        Timer infoTimer = new Timer() { Interval = 1000 };
        string toolName;
        string screenshotName;

        ToolStripButton latestSelected;

        #endregion

        #region Constructors

        // Testing EditForm
        public EditForm()
        {
            MessageBox.Show(@"You're about to try Capshot's editor.


Capshot's editor is useful to highlight things, draw notes, or pixelate and blur things to hide (such as phone numbers or passwords).

· To move the image, use right (or middle) click and drag the image.
· You can undo and redo infinitely, resize the image, rotate the screenshot...

If you still have questions, don't think twice before visiting author's website to ask him!",
                "Welcome to Capshot's editor", MessageBoxButtons.OK, MessageBoxIcon.Information);

            BeginInit();

            var testingBmp = new Bitmap(500, 500);
            using (var g = Graphics.FromImage(testingBmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                g.FillRectangle(new LinearGradientBrush(new Point(250, 0), new Point(250, 500),
                    Color.FromArgb(250, 250, 250), Color.FromArgb(230, 230, 230)), 0, 0, 500, 500);

                var icon = Properties.Resources.icon;
                g.DrawImage(icon, 250 - icon.Width / 2, 250 - icon.Height / 2);

                StringFormat sf = new StringFormat() {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Center
                };
                
                g.DrawString("Capshot", new Font(FontFamily.GenericSansSerif, 30),
                    Brushes.DarkBlue, 250, 250 + icon.Height, sf);

                g.DrawString("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin cursus condimentum sapien a sagittis. Nunc nec nisl tellus. Etiam in egestas sapien, eget finibus nibh. Mauris molestie urna lorem, at fringilla dolor dignissim in. Etiam elementum eu nisl eget luctus. Ut nec urna pulvinar, viverra elit ut, lobortis nulla. Praesent ac orci nunc. Sed vel nulla dui. Nulla vel odio neque. Vestibulum elementum eu quam vel vehicula.",
                    new Font(FontFamily.GenericSansSerif, 10), Brushes.DarkGoldenrod,
                    new Rectangle(10, 250 + icon.Height + 50, 490, 60), sf);
            }
            editControl.BaseBmp = testingBmp;

            EndInit();
        }

        public EditForm(Bitmap img)
        {
            BeginInit();
            editControl.BaseBmp = img;
            EndInit();
        }

        void BeginInit()
        {
            InitializeComponent();
            Icon = Properties.Resources.appiconsmall;
            Text = screenshotName = ActionForm.GetAutoFormat();

            sec1Timer.Tick += ShowColorPalette;
            infoTimer.Tick += InfoTimer_Tick;

            editControl.Operation += EditControl_Operation;
        }

        void EndInit()
        {
            Width = editControl.BaseBmp.Width + 80; // Some extra padding
            Height = editControl.BaseBmp.Height + 100; //   for the image

            // If it is bigger than the 90% of the screen, maximize
            if (Width > Screen.PrimaryScreen.Bounds.Width * 0.9f ||
                Height > Screen.PrimaryScreen.Bounds.Height * 0.9f)
                WindowState = FormWindowState.Maximized;
        }

        #endregion

        #region Overrides

        protected override void OnResize(EventArgs e)
        {
            editControl.Invalidate();
            base.OnResize(e);
        }


        protected override void OnClosed(EventArgs e)
        {
            editControl.Dispose();
            base.OnClosed(e);
        }

        #endregion

        #region Tools tool strip

        // Pen
        void penTSB_Click(object sender, EventArgs e)
        {
            toolName = "pen";
            editControl.SelectedTool = EditTools.Pen;
        }
        void penTSB_MouseDown(object sender, MouseEventArgs e)
        {
            penTSB_Click(null, null);
            sec1Timer.Enabled = true;
            UncheckToolbar((ToolStripButton)sender);
        }
        void penTSB_MouseUp(object sender, MouseEventArgs e) { sec1Timer.Enabled = false; }

        // Marker
        void markerTSB_Click(object sender, EventArgs e)
        {
            toolName = "marker";
            editControl.SelectedTool = EditTools.Marker;
        }
        void markerTSB_MouseDown(object sender, MouseEventArgs e)
        {
            markerTSB_Click(null, null);
            sec1Timer.Enabled = true;
            UncheckToolbar((ToolStripButton)sender);
        }
        void markerTSB_MouseUp(object sender, MouseEventArgs e) { sec1Timer.Enabled = false; }

        // Eraser
        void eraserTSB_Click(object sender, EventArgs e) {
            editControl.SelectedTool = EditTools.Eraser;
            UncheckToolbar((ToolStripButton)sender);
        }

        // Blur
        void blurTSB_Click(object sender, EventArgs e) {
            editControl.SelectedTool = EditTools.Blur;
            UncheckToolbar((ToolStripButton)sender);
        }

        // Pixelate
        void pixelateTSB_Click(object sender, EventArgs e) {
            editControl.SelectedTool = EditTools.Pixelate;
            UncheckToolbar((ToolStripButton)sender);
        }

        void colorpickerTSB_Click(object sender, EventArgs e) {
            editControl.SelectedTool = EditTools.PickColor;
            UncheckToolbar((ToolStripButton)sender);
        }

        // Uncheck all avoiding one
        void UncheckToolbar(ToolStripButton avoid) {
            foreach (ToolStripButton tsb in toolsTS.Items)
                tsb.Checked = tsb.Name == avoid.Name;
        }

        // Uncheck all and return the one that was checked
        ToolStripButton UncheckToolbar() {
            foreach (ToolStripButton tsb in toolsTS.Items)
                if (tsb.Checked)
                {
                    tsb.Checked = false;
                    return tsb;
                }
            return null;
        }

        // Color palette (for pen and marker)
        void ShowColorPalette(object sender, EventArgs e)
        {
            sec1Timer.Enabled = false;
            new ColorPaletteForm(toolName).Show();
        }

        #endregion

        #region Actions tool strip

        // Copy or save
        void clipboardTSB_Click(object sender, EventArgs e)
        {
            ActionForm.PerformAction(editControl.GetFinalResult(), ActionForm.Action.Clipboard);
            Text = "Copied to clipboard";
            infoTimer.Enabled = false; infoTimer.Enabled = true;
        }

        void saveTSB_Click(object sender, EventArgs e)
        {
            Text = "File saved to " +
                ActionForm.PerformAction(editControl.GetFinalResult(), ActionForm.Action.Save); ;
            infoTimer.Enabled = false; infoTimer.Enabled = true;
        }

        void saveAsTSB_Click(object sender, EventArgs e)
        {
            Text = "Saving file...";
            Text = ActionForm.PerformAction(editControl.GetFinalResult(), ActionForm.Action.SaveAs);
            infoTimer.Enabled = false; infoTimer.Enabled = true;
        }

        // Image transformations
        void resizeTSB_Click(object sender, EventArgs e) {
            editControl.ResizeImage(ResizeForm.Show(editControl.BaseBmp.Size));
        }

        void rotateLeftTSB_Click(object sender, EventArgs e) {
            editControl.RotateLeft();
        }

        void rotateRight_Click(object sender, EventArgs e) {
            editControl.RotateRight();
        }

        void mirrorTSB_Click(object sender, EventArgs e) {
            editControl.Mirror();
        }

        void textTSB_Click(object sender, EventArgs e)
        {
            var tff = TextForm.Show();
            if (tff.Text.Length > 0)
            {
                latestSelected = UncheckToolbar();
                editControl.SelectedTextFontFamily = tff;
                editControl.SelectedTool = EditTools.Text;
            }
        }

        #endregion

        #region Shortcuts

        void EditForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (!e.Control)
                return;

            switch (e.KeyCode)
            {
                case Keys.C:
                    clipboardTSB.PerformClick();
                    break;

                case Keys.S:
                    if (e.Shift)
                        saveAsTSB.PerformClick();
                    else
                        saveTSB.PerformClick();
                    break;

                case Keys.Z:
                    undoTSB.PerformClick();
                    break;

                case Keys.Y:
                    redoTSB.PerformClick();
                    break;

                case Keys.Add:
                case Keys.Oemplus:
                    editControl.ToolSize++;
                    break;

                case Keys.Subtract:
                case Keys.OemMinus:
                    editControl.ToolSize--;
                    break;
            }
        }

        #endregion

        #region Undo and redo

        void editControl_MouseUp(object sender, MouseEventArgs e)
        {
            RecheckUndoRedo();
            if (latestSelected != null)
            {
                latestSelected.PerformClick();
                latestSelected = null;
            }
        }

        void RecheckUndoRedo()
        {
            undoTSB.Enabled = clearTSB.Enabled = editControl.CanUndo;
            redoTSB.Enabled = editControl.CanRedo;
        }

        void undoTSB_Click(object sender, EventArgs e)
        {
            editControl.Undo();
            RecheckUndoRedo();
        }

        void redoTSB_Click(object sender, EventArgs e)
        {
            editControl.Redo();
            RecheckUndoRedo();
        }

        void clearTSB_Click(object sender, EventArgs e)
        {
            editControl.Clear();
            RecheckUndoRedo();
        }

        #endregion
        
        #region Info timer and events

        void InfoTimer_Tick(object sender, EventArgs e)
        {
            Text = screenshotName;
            infoTimer.Enabled = false;
        }

        void EditControl_Operation(string status)
        {
            if (status.Length > 0)
            {
                Text = status;
                Cursor = Cursors.WaitCursor;
            }
            else
            {
                Text = screenshotName;
                Cursor = Cursors.Arrow;
            }
        }

        #endregion
    }
}
