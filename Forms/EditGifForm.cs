using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capshot
{
    public partial class EditGifForm : Form
    {
        const string baseFrameText = "Frame {0}/{1}";

        Timer playT = new Timer();

        // store the frames as bytes so they occupy less 
        List<byte[]> frames;
        int delay;
        int quality;
        int repeat;

        string gifName;

        public static void EditGif(List<byte[]> frames, int delay, int quality, int repeat)
        {
            using (var egf = new EditGifForm())
            {
                egf.frames = frames;
                egf.delay = delay;
                egf.quality = quality;
                egf.repeat = repeat;

                egf.ShowDialog();
            }
        }

        EditGifForm()
        {
            InitializeComponent();
            Icon = Properties.Resources.appiconsmall;
            Text = gifName = ActionForm.GetAutoFormat();
        }
        
        async void saveTSB_Click(object sender, EventArgs e)
        {
            var file = ActionForm.GetGifSaveLocation(ActionForm.Action.Save);
            if (string.IsNullOrEmpty(file))
                return;

            await doSave(file);
        }

        async void saveAsTSB_Click(object sender, EventArgs e)
        {
            var file = ActionForm.GetGifSaveLocation(ActionForm.Action.SaveAs);
            if (string.IsNullOrEmpty(file))
                return;

            await doSave(file);
        }

        async Task doSave(string file)
        {
            var progress = new Progress<float>();
            progress.ProgressChanged += progressChanged;

            // start saving
            string tmpText = Text;
            Text = "Saving...";
            Cursor = Cursors.WaitCursor;
            Enabled = false;
            progressBar.Visible = true;
            playT.Enabled = false;

            // save
            await GifForm.EncodeGifBytes(
                frames,
                getEncodeDelay(), 
                quality,
                repeat,
                file,
                progress,
                timeSelector.StartValue - 1,
                timeSelector.EndValue - 1,
                getEncodeFrameIncrement());

            // end saving
            Text = tmpText;
            Cursor = Cursors.Arrow;
            Enabled = true;
            progressBar.Visible = false;
            playT.Enabled = true;
        }

        void progressChanged(object sender, float progress)
        {
            var value = (int)(progress * progressBar.Maximum);
            if (value < 0)
                value = 0;
            if (value > progressBar.Maximum)
                value = progressBar.Maximum;

            Text = "Saving " + progress.ToString("P") + "...";
            progressBar.Value = value;
        }

        void EditGifForm_Load(object sender, EventArgs e)
        {
            timeSelector.StartValue = timeSelector.Minimum = 1;
            timeSelector.EndValue = timeSelector.Maximum = frames.Count;

            playT.Interval = delay;
            playT.Tick += PlayT_Tick;
            playT.Enabled = true;
        }
        
        int _currentFrame = -1;
        int currentFrame
        {
            get { return _currentFrame; }
            set
            {
                var start = timeSelector.StartValue - 1;
                var end = timeSelector.EndValue;
                
                _currentFrame = start + ((value - start) % (end - start));

                frameTSL.Text = string.Format(baseFrameText, _currentFrame + 1, frames.Count);
                pictureBox.Image = GifForm.BytesToImage(frames[_currentFrame]);
            }
        }

        private void PlayT_Tick(object sender, EventArgs e)
        {
            if (!isSelectingTime)
                ++currentFrame;
        }

        #region Constructor

        public EditGifForm(Bitmap img)
        {
            BeginInit();
            //editControl.BaseBmp = img;
            EndInit();
        }

        void BeginInit()
        {
            InitializeComponent();
            Icon = Properties.Resources.appiconsmall;
            Text = gifName = ActionForm.GetAutoFormat();
        }

        void EndInit()
        {
            //Width = editControl.BaseBmp.Width + 80; // Some extra padding
            //Height = editControl.BaseBmp.Height + 100; //   for the image

            // If it is bigger than the 90% of the screen, maximize
            if (Width > Screen.PrimaryScreen.Bounds.Width * 0.9f ||
                Height > Screen.PrimaryScreen.Bounds.Height * 0.9f)
                WindowState = FormWindowState.Maximized;
        }

        #endregion

        #region Shortcuts

        void EditForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (!e.Control)
                return;

            switch (e.KeyCode)
            {
                case Keys.S:
                    if (e.Shift)
                        saveAsTSB.PerformClick();
                    else
                        saveTSB.PerformClick();
                    break;
            }
        }

        #endregion

        bool isSelectingTime;
        void timeSelector_MouseDown(object sender, MouseEventArgs e)
        {
            isSelectingTime = true;
        }

        void timeSelector_MouseUp(object sender, MouseEventArgs e)
        {
            isSelectingTime = false;
        }

        void timeSelector_Scroll(object sender, ScrollEventArgs e)
        {
            currentFrame = e.NewValue - 1; // it has to be index 0 based
        }

        void speedTSMI_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem tsi in speedsTSDDB.DropDownItems)
                tsi.Checked = false;

            ((ToolStripMenuItem)sender).Checked = true;
            updateSpeed();
        }

        void updateSpeed()
        {
            double multiplier = 1;

            if (speedQuarterTSMI.Checked)
                multiplier = 0.25;

            else if (speedHalfTSMI.Checked)
                multiplier = 0.5;

            else if (speedDoubleTSMI.Checked)
                multiplier = 2;

            else if (speedQuadrupleTSMI.Checked)
                multiplier = 4;
            
            // perform the inverse operation, as more interval = slower
            playT.Interval = (int)(delay / multiplier);
        }

        int getEncodeDelay()
        {
            if (speedQuarterTSMI.Checked)
                return delay * 4;

            else if (speedHalfTSMI.Checked)
                return delay * 2;

            return delay;
        }

        int getEncodeFrameIncrement()
        {
            if (speedDoubleTSMI.Checked)
                return 2;

            else if (speedQuadrupleTSMI.Checked)
                return 4;

            return 1;
        }
    }
}
