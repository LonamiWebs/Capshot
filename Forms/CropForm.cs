using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Capshot
{
    public partial class CropForm : Form
    {
        #region Variables
        
        public Rectangle CroppedRectangle;

        readonly Bitmap OriginalImage;
        readonly Pen BlackDotsPen = new Pen(Color.Black, 0.1f) { DashStyle = DashStyle.Dot };
        readonly Pen RectanglePen = new Pen(Color.FromArgb(0, 150, 205), 0.1f);
        readonly Brush RectangleBrush = new SolidBrush(Color.FromArgb(20, 0, 200, 255));

        static readonly Point defaultPoint = new Point(-1, -1);
        Point StartPoint = defaultPoint;

        bool OnlySelectionRectangle; // If we don't need to perform any action to the cropped image

        #endregion

        #region CropForm show

        /// <summary>
        /// Shows the crop form and returns the cropped rectangle
        /// </summary>
        /// <param name="screenshot">The original screenshot</param>
        /// <returns>The cropped rectangle</returns>
        public static Rectangle ShowRectangle(Bitmap screenshot)
        {
            using (var cf = new CropForm(screenshot, true))
            {
                cf.ShowDialog();
                GC.Collect();
                return cf.CroppedRectangle;
            }
        }

        #endregion

        #region Constructor and load

        public CropForm(Bitmap originalImage, bool rectangle = false)
        {
            InitializeComponent();
            Icon = Properties.Resources.appiconsmall;

            OriginalImage = originalImage;
            OnlySelectionRectangle = rectangle;
        }

        void CropForm_Load(object sender, EventArgs e)
        {
            Activate();
            Focus();
        }

        #endregion

        #region Destruction and cleaning
        
        void CropForm_FormClosed(object sender, FormClosedEventArgs e) {
            OriginalImage.Dispose();
        }

        #endregion

        #region OnPaint override

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(OriginalImage, 0, 0);

            if (StartPoint != defaultPoint)
            {
                CroppedRectangle = GetCurrentRectangle(StartPoint);

                e.Graphics.FillRectangle(RectangleBrush, CroppedRectangle);

                e.Graphics.DrawRectangle(RectanglePen, CroppedRectangle);

                var fsize = CroppedRectangle.Height / 3;
                if (fsize > 40)
                    fsize = 40;

                if (fsize > 0)
                {
                    string text = CroppedRectangle.Width + "x" + CroppedRectangle.Height;
                    var font = new Font(FontFamily.GenericSansSerif, fsize);

                    while (e.Graphics.MeasureString(text, font).Width > CroppedRectangle.Width && fsize > 1)
                        font = new Font(FontFamily.GenericSansSerif, --fsize);

                    StringFormat sf = new StringFormat()
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Center
                    };

                    e.Graphics.DrawString(text, font, Brushes.DarkBlue, CroppedRectangle, sf);
                }

            }

            else
            {
                e.Graphics.DrawLine(BlackDotsPen, MousePosition.X, 0, MousePosition.X, Screen.PrimaryScreen.Bounds.Height);
                e.Graphics.DrawLine(BlackDotsPen, 0, MousePosition.Y, Screen.PrimaryScreen.Bounds.Width, MousePosition.Y);
            }
        }

        #endregion

        #region Retrieve current selected rectangle

        // As Rectangle doesn't accept negative sizes, we need this to know which point is upper and which is left
        public static Rectangle GetCurrentRectangle(Point startPoint)
        {
            Point start = new Point(), end = new Point();
            if (startPoint.X < MousePosition.X)
            {
                start.X = startPoint.X;
                end.X = MousePosition.X;
            }
            else
            {
                start.X = MousePosition.X;
                end.X = startPoint.X;
            }
            if (startPoint.Y < MousePosition.Y)
            {
                start.Y = startPoint.Y;
                end.Y = MousePosition.Y;
            }
            else
            {
                start.Y = MousePosition.Y;
                end.Y = startPoint.Y;
            }

            return new Rectangle(start.X, start.Y, end.X - start.X, end.Y - start.Y);
        }

        #endregion

        #region Mouse and keyboard events

        void CutForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        void CutForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                Close();
            else
                StartPoint = MousePosition;
        }

        void CutForm_MouseMove(object sender, MouseEventArgs e) { Invalidate(); }

        void CutForm_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateCropped();
            Close();
        }

        #endregion

        #region Cropped image

        // Updates the cropped image with the final result
        void UpdateCropped()
        {
            if (CroppedRectangle.Width == 0 || CroppedRectangle.Height == 0 || OnlySelectionRectangle)
                return;

            var cropped = new Bitmap(CroppedRectangle.Width, CroppedRectangle.Height);

            using (Graphics g = Graphics.FromImage(cropped))
            {
                g.DrawImage(
                    OriginalImage,
                    new Rectangle(0, 0, cropped.Width, cropped.Height),
                    CroppedRectangle,
                    GraphicsUnit.Pixel);
            }
            
            ActionForm.PerformAction(cropped);
        }

        #endregion
    }
}
