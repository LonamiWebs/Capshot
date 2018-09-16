using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capshot
{
    public partial class GifForm : Form
    {
        #region Variables

        Rectangle selectedRectangle;
        Rectangle oversizedRect;

        bool captureCursor;
        float maxTime;
        float curTime;
        int quality;
        int repeat;

        bool stopped = false;

        // store the captures as bytes to use less memory
        List<byte[]> frames = new List<byte[]>();
        Timer captureT = new Timer();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new GifForm
        /// </summary>
        /// <param name="captureCursor">Should the gif capture the cursor?</param>
        /// <param name="rct">The rectangle to capture</param>
        public GifForm(bool captureCursor, Rectangle rct)
        {
            InitializeComponent();
            Icon = Properties.Resources.appiconsmall;

            this.captureCursor = captureCursor;
            maxTime = Settings.GetValue<float>("gifMax");
            repeat = Settings.GetValue<int>("gifRepeat");

            // 9 max = 1 (more quality); 1 min = 18 (less quality)
            quality = ((int)((9 - Settings.GetValue<int>("gifQuality")) / 9f * 20f) + 1);

            selectedRectangle = rct;
            oversizedRect = new Rectangle(rct.X - 1, rct.Y - 1, rct.Width + 1, rct.Height + 1);

            LocateControlPanel();

            captureT.Interval = (int)(1000f / Settings.GetValue<float>("gifFps"));
            captureT.Tick += captureT_Tick;
            captureT.Enabled = true;
        }

        #endregion

        #region Paint

        protected override void OnPaint(PaintEventArgs e)
        {
            if (oversizedRect != Rectangle.Empty) // Recording
                e.Graphics.DrawRectangle(Pens.DarkBlue, oversizedRect);
        }

        #endregion

        #region Controls

        void playPauseB_Click(object sender, EventArgs e)
        {
            if (captureT.Enabled) // Recording, pause
            {
                playPauseB.Image = Properties.Resources.play;
                captureT.Enabled = false;
            }
            else // Paused, record
            {
                playPauseB.Image = Properties.Resources.pause;
                captureT.Enabled = true;
            }
        }

        async void stopB_Click(object sender, EventArgs e)
        {
            await Stop();
        }

        async Task Stop()
        {
            if (stopped) return;
            stopped = true;

            selectedRectangle = oversizedRect = Rectangle.Empty;
            captureT.Enabled = false;

            playPauseB.Visible = stopB.Visible = false;
            timeL.Text = "";
            Invalidate();
            await SaveAsync(ActionForm.GetGifSaveLocation());
        }

        async Task SaveAsync(string location)
        {
            if (ActionForm.SelectedEdit)
            {
                Close();
                EditGifForm.EditGif(frames, captureT.Interval, quality, repeat);
                return;
            }
            if (string.IsNullOrEmpty(location))
            {
                FreeFramesMemory(frames);
                Close();
                return;
            }

            timeL.Text = "Guardando...";
            var progress = new Progress<float>();
            progress.ProgressChanged += (s, f) => timeL.Text = String.Format("Guardando {0}...", f.ToString("P"));
            
            await EncodeGifBytes(frames, captureT.Interval, quality, repeat, location, progress);
            FreeFramesMemory(frames);

            Close();
        }

        // Locate the control panel to it's position
        void LocateControlPanel()
        {
            var bottomCenter = new Point(selectedRectangle.X + selectedRectangle.Width / 2,
                                         selectedRectangle.Y + selectedRectangle.Height);
            int padding = 5;
            var position = new Point(bottomCenter.X - controlP.Width / 2, bottomCenter.Y + padding);

            if (position.Y + controlP.Height > Screen.PrimaryScreen.WorkingArea.Bottom)
                position.Y -= (position.Y + controlP.Height) - Screen.PrimaryScreen.WorkingArea.Bottom;

            controlP.Location = position;
            controlP.Visible = true;
        }

        #endregion

        // TODO organise
        /// <summary>
        /// Encodes a compressed byte[] list containing image data as a gif to the desired location
        /// </summary>
        /// <param name="frames">Compressed byte array defining an image</param>
        /// <param name="delay">The delay between frames</param>
        /// <param name="quality">The quality of the gif</param>
        /// <param name="repeat">Repeat mode</param>
        /// <param name="location">Where the gif will be saved</param>
        /// <param name="percentageProgress">Interface used to determine the current task progress</param>
        /// <param name="fromIndex">From which index will the list of frames start to be encoded</param>
        /// <param name="toIndex">Until which index will the list of frames be encoded</param>
        /// <param name="frameIndexIncremet">Used to skip frames if <see cref="frameIndexIncremet"/> > 1</param>
        public static async Task EncodeGifBytes(
            List<byte[]> frames,
            int delay,
            int quality,
            int repeat,
            string location,
            IProgress<float> percentageProgress,
            int fromIndex = 0,
            int toIndex = -1,
            int frameIndexIncremet = 1)
        {
            if (toIndex < 0)
                toIndex = frames.Count;

            await Task.Run(() =>
            {
                var age = new AnimatedGifEncoder();
                age.Start(location);
                age.SetDelay(delay);
                age.SetQuality(quality);
                age.SetRepeat(repeat);
                for (int i = fromIndex; i < toIndex; i += frameIndexIncremet)
                {
                    if (percentageProgress != null)
                        percentageProgress.Report((float)(i - fromIndex) / (float)(toIndex - fromIndex));

                    age.AddFrame(BytesToImage(frames[i]));
                }
                age.Finish();
            });
        }

        public static void FreeFramesMemory(List<byte[]> frames)
        {
            for (int i = 0; i < frames.Count; i++)
                frames[i] = new byte[0];
            frames.Clear();

            GC.Collect();
        }

        public static Image BytesToImage(byte[] imageBytes)
        {
            using (var ms = new MemoryStream(imageBytes))
                return Image.FromStream(ms);
        }

        async void captureT_Tick(object sender, EventArgs e)
        {
            curTime += captureT.Interval / 1000f; // We need seconds
            if (curTime > maxTime)
            {
                timeL.Text = maxTime.ToString("0.0") + "/" + maxTime.ToString("0.0") + "s";
                await Stop();
                return;
            }

            timeL.Text = curTime.ToString("0.0") + "/" + maxTime.ToString("0.0") + "s";
            frames.Add(Screenshot.CaptureRegionAsBytes(selectedRectangle, captureCursor));
        }
    }
}