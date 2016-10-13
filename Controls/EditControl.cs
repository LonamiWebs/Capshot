using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using BitmapExtensions;
using System.Drawing.Text;

namespace Capshot
{
    public enum EditTools { Image, Pen, Marker, Eraser, Blur, Pixelate, Text, PickColor }

    public struct TextFontFamily
    {
        public string Text;
        public FontFamily Family;

        public TextFontFamily(string text, FontFamily family)
        {
            Text = text;
            Family = family;
        }
    }

    class EditControl : PictureBox
    {
        #region Events

        public delegate void OperationDelegate(string status);
        public event OperationDelegate Operation;

        void OnOperation(string status)
        {
            if (Operation != null)
                Operation(status);
        }

        #endregion

        #region Public variables

        // WARNING: Set b4 use
        public Bitmap BaseBmp;

        public TextFontFamily SelectedTextFontFamily;

        #endregion

        #region Variables

        List<Point> points = new List<Point>();
        List<Bitmap> steps = new List<Bitmap>();

        bool Painting;

        // This is a clear image ready to use for the eraser tool
        Bitmap clearImage = new Bitmap(1, 1); // TODO CHANGE THIS

        const int showPickColorSize = 4; // the rendered circle (normal cursor)
        const int pickColorSize = 24; // the preview circle
        public float ToolSize
        {
            get
            {
                switch (SelectedTool)
                {
                    case EditTools.Pen: return Settings.GetValue<int>("penSize");
                    case EditTools.Marker: return Settings.GetValue<int>("markerSize");
                    case EditTools.Eraser: return Settings.GetValue<int>("eraserSize");
                    case EditTools.Pixelate: return Settings.GetValue<int>("pixelateSize");
                    case EditTools.Blur: return Settings.GetValue<int>("blurSize");
                    case EditTools.PickColor: return showPickColorSize;
                }
                return -1;
            }
            set
            {
                switch (SelectedTool)
                {
                    case EditTools.Pen: Settings.SetValue("penSize", Clamp((int)value)); break;
                    case EditTools.Marker: Settings.SetValue("markerSize", Clamp((int)value)); break;
                    case EditTools.Eraser: Settings.SetValue("eraserSize", Clamp((int)value)); break;

                    // these have different clamp values due to the time it takes to render
                    case EditTools.Pixelate: Settings.SetValue("pixelateSize", Clamp((int)value, 1, 20)); break;
                    case EditTools.Blur: Settings.SetValue("blurSize", Clamp((int)value, 1, 10)); break;
                }
                Invalidate();
            }
        }

        int currentStep = -1;

        float minDistance = 5;

        int panX, panY;

        bool panning;
        Point lastPoint;

        #endregion

        #region Properties

        // We don't need an Image, We need a BaseLayer.
        new Image Image { get; set; }

        bool CanUseStep { get { return steps.Count > 0 && currentStep > -1; } }

        public bool CanUndo { get { return currentStep > -1; } }
        public bool CanRedo { get { return currentStep < steps.Count - 1; } }

        public EditTools SelectedTool = EditTools.Pen;

        bool _CursorShown = true;
        public bool CursorShown
        {
            get { return _CursorShown; }
            set
            {
                if (value == _CursorShown)
                    return;

                if (value)
                    Cursor.Show();
                else
                    Cursor.Hide();

                _CursorShown = value;
            }
        }

        #endregion

        #region Overrides

        bool leftWasDown;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                panning = true;
                lastPoint = e.Location;
                Cursor = Cursors.NoMove2D;
            }
            else if (SelectedTool == EditTools.PickColor)
            {
                var color = GetColorUnderMouse();
                if (color.HasValue)
                    new ColorPreviewForm(color.Value).ShowDialog();
            }
            else
            {
                leftWasDown = true;
                Painting = true;

                using (var clearImage = new Bitmap(Width, Height))
                using (var g = Graphics.FromImage(clearImage))
                {
                    g.Clear(BackColor);
                    g.DrawImage(BaseBmp, TopLeftCorner());
                }

                DoMouseAction(e.Location);
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            CursorShown = ShowCursor(e.Location);
            if (panning)
            {
                panX += e.Location.X - lastPoint.X;
                panY += e.Location.Y - lastPoint.Y;
                lastPoint = e.Location;
                Invalidate();
            }
            else
                DoMouseAction(e.Location);
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (leftWasDown && e != null && e.Button == MouseButtons.Left)
                leftWasDown = false;

            if (panning)
            {
                panning = false;
                Cursor = Cursors.Arrow;
            }

            FinishMouseAction();
            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            CursorShown = true;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            ToolSize += e.Delta / 120;
            base.OnMouseWheel(e);
        }

        protected override void Dispose(bool disposing)
        {
            foreach (var step in steps)
                step.Dispose();
            if (clearImage != null)
                clearImage.Dispose();
            BaseBmp.Dispose();

            base.Dispose(disposing);
        }

        #endregion

        #region Perform mouse actions

        // Do mouse action when it's moved over the control
        void DoMouseAction(Point mouseLocation)
        {
            if (Painting)
                switch (SelectedTool)
                {
                    case EditTools.Pen:
                    case EditTools.Marker:
                    case EditTools.Eraser:

                        points.Add(mouseLocation);
                        break;

                    case EditTools.Blur:
                    case EditTools.Pixelate:
                    case EditTools.Text:

                        if (points.Count == 2)
                            points.RemoveAt(1);

                        points.Add(mouseLocation);
                        break;
                }

            Invalidate();
        }

        // Returns the current color under the mouse
        Color? GetColorUnderMouse()
        {
            // Get point relative to the control
            var point = PointToClient(Cursor.Position);
            if (!GetImageRectangle().Contains(point))
                return null;

            using (var bmp = new Bitmap(1, 1))
            using (var g = Graphics.FromImage(bmp))
            {
                // Translate the point to be relative to the image
                point = CalculatePointToImage(point);

                // Add the base color
                g.FillRectangle(new SolidBrush(BaseBmp.GetPixel(point.X, point.Y)), 0, 0, 1, 1);

                // If available, add the last step color
                if (CanUseStep && !ModifierKeys.HasFlag(Keys.Control))
                    g.FillRectangle(new SolidBrush(steps[currentStep].GetPixel(point.X, point.Y)), 0, 0, 1, 1);

                return bmp.GetPixel(0, 0);
            }
        }

        // Finish the mouse action after it's released from the control
        void FinishMouseAction()
        {
            Painting = false;

            if (SelectedTool != EditTools.PickColor)
            {
                DiscardPostSteps();

                // If there's any previous step, use it, otherwise create a new empty step
                var step = CanUseStep ?
                    (Bitmap)steps[currentStep].Clone()
                    : new Bitmap(BaseBmp.Width, BaseBmp.Height);

                // Translate the points to use them in the bitmap
                for (int i = 0; i < points.Count; i++)
                    points[i] = CalculatePointToImage(points[i]);

                var g = Graphics.FromImage(step);
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Do the painting operations on the layer
                DoPaint(g, SelectedTool, points, false);

                g.Dispose();

                // Add the steps and clear the points
                steps.Add(step);
                currentStep++;

                points.Clear();

                Invalidate();
            }
        }

        #endregion

        #region Painting

        // Called after Invalidate();
        protected override void OnPaint(PaintEventArgs pe)
        {
            if (leftWasDown && MouseButtons != MouseButtons.Left) // something went wrong!, force MouseUp call
            {
                leftWasDown = false;
                OnMouseUp(null);
                return;
            }

            var mouseLoc = PointToClient(Cursor.Position);

            var g = pe.Graphics;
            g.SmoothingMode = Painting ? SmoothingMode.None : SmoothingMode.AntiAlias;

            DoPaint(g, EditTools.Image, BaseBmp);

            // If there is any changes, draw the latest
            if (CanUseStep)
                DoPaint(g, EditTools.Image, steps[currentStep]);

            if (Painting)
                DoPaint(g, SelectedTool, points);

            else if (ToolSize > 0 && !CursorShown)
            {
                g.DrawEllipse(new Pen(new LinearGradientBrush(new Point(0, 0),
                    new Point((int)ToolSize, (int)ToolSize), Color.White, Color.Black), 0.1f),
                    mouseLoc.X - ToolSize / 2, mouseLoc.Y - ToolSize / 2, ToolSize, ToolSize);

                g.FillEllipse(new LinearGradientBrush(new Point(0, 0),
                    new Point(2, 2), Color.White, Color.Black), mouseLoc.X - 1, mouseLoc.Y - 1, 2, 2);

                // perform special painting
                if (SelectedTool == EditTools.PickColor)
                {
                    var color = GetColorUnderMouse();
                    if (color.HasValue)
                    {
                        g.FillEllipse(
                            new SolidBrush(color.Value),
                            mouseLoc.X,
                            mouseLoc.Y - pickColorSize,
                            pickColorSize,
                            pickColorSize);

                        g.DrawEllipse(
                            new Pen(new LinearGradientBrush(
                                new Point(0, 0),
                                new Point(pickColorSize, pickColorSize),
                                Color.White,
                                Color.Black),
                                1f),
                            mouseLoc.X,
                            mouseLoc.Y - pickColorSize,
                            pickColorSize,
                            pickColorSize);
                    }
                }
            }
        }

        // Do the painting operations with the given Graphics item
        // Enable useClearImage if it's rendering over all the control, set to false if only for the final result
        void DoPaint(Graphics g, EditTools tool, object info, bool useClearImage = true)
        {
            if (info == null)
                return;

            bool dispose = true;
            Bitmap bmp = null;
            List<Point> pts;

            switch (tool)
            {
                case EditTools.Image:
                    // All images will have the same size, so we always use the size of the BaseLayer
                    g.DrawImage((Bitmap)info, TopLeftCorner());
                    break;

                case EditTools.Pen:
                    PaintPoints(ref g, new Pen(Settings.GetValue<SerializableColor>("penColor"), ToolSize), (List<Point>)info);
                    break;

                case EditTools.Marker:
                    if (g.SmoothingMode == SmoothingMode.AntiAlias) // Do it right. This could (and SHOULD) be optimized
                    {
                        bmp = GetFinalResult();

                        var newbmp = new Bitmap(bmp.Width, bmp.Height);

                        var gr = Graphics.FromImage(newbmp);
                        PaintPoints(ref gr, new Pen(Settings.GetValue<SerializableColor>("markerColor"), ToolSize),
                            (List<Point>)info);
                        gr.Dispose();

                        newbmp.MergeMultiply(bmp);

                        g.DrawImage(newbmp, 0, 0);
                    }
                    else // Do it quick
                    {
                        PaintPoints(ref g,
                            new Pen(Color.FromArgb(100, Settings.GetValue<SerializableColor>("markerColor")), ToolSize),
                            (List<Point>)info);
                    }
                    break;

                case EditTools.Eraser:

                    dispose = false;

                    bmp = useClearImage ? clearImage : BaseBmp;
                    if (bmp == null)
                        break;

                    var tb = new TextureBrush(bmp);
                    var pn = new Pen(tb, ToolSize);
                    PaintPoints(ref g,
                        new Pen(new TextureBrush(bmp), ToolSize), (List<Point>)info, true);

                    break;

                case EditTools.Blur:
                    pts = (List<Point>)info;
                    if (pts.Count < 2)
                        break;

                    if (g.SmoothingMode == SmoothingMode.AntiAlias) // Do it right
                    {
                        CursorShown = true;
                        OnOperation("Aplicando desenfoque...");
                        bmp = GetFinalResult();
                        g.DrawImage(bmp.Blur(GetRectangle(pts[0], pts[1]), (int)ToolSize), 0, 0);
                        OnOperation("");
                        CursorShown = false;
                    }
                    else // Do it quick
                    {
                        g.DrawRectangle(Pens.Black, GetRectangle(pts[0], pts[1]));
                    }
                    break;

                case EditTools.Pixelate:

                    pts = (List<Point>)info;
                    if (pts.Count < 2)
                        break;

                    if (g.SmoothingMode == SmoothingMode.AntiAlias) // Do it right
                    {
                        CursorShown = true;
                        OnOperation("Aplicando pixelado...");
                        bmp = GetFinalResult();
                        g.DrawImage(bmp.Pixelate(GetRectangle(pts[0], pts[1]), (int)ToolSize), 0, 0);
                        OnOperation("");
                        CursorShown = false;
                    }
                    else // Do it quick
                    {
                        g.DrawRectangle(Pens.Black, GetRectangle(pts[0], pts[1]));
                    }
                    break;

                case EditTools.Text:

                    pts = (List<Point>)info;
                    if (pts.Count < 2)
                        break;

                    var rct = GetRectangle(pts[0], pts[1]);

                    g.TextRenderingHint = TextRenderingHint.AntiAlias;

                    if (g.SmoothingMode != SmoothingMode.AntiAlias)
                        g.DrawRectangle(Pens.Black, rct);

                    g.DrawString(
                        SelectedTextFontFamily.Text,
                        GetBestFontSize(ref g, rct),
                        new SolidBrush(Settings.GetValue<SerializableColor>("penColor")),
                        rct);

                    break;
            }

            if (dispose && bmp != null)
                bmp.Dispose();
        }

        Font GetBestFontSize(ref Graphics g, Rectangle rct)
        {
            var txt = SelectedTextFontFamily.Text;
            var fof = SelectedTextFontFamily.Family;
            int fsize = rct.Width / txt.Length;
            if (fsize == 0)
                return new Font(fof, 1);

            Font font = new Font(fof, fsize);

            while (g.MeasureString(txt, font).Width < rct.Width)
                font = new Font(fof, ++fsize);
            while (g.MeasureString(txt, font).Height < rct.Height)
                font = new Font(fof, ++fsize);

            while (g.MeasureString(txt, font).Height > rct.Height && fsize > 1)
                font = new Font(fof, --fsize);
            while (g.MeasureString(txt, font).Width > rct.Width && fsize > 1)
                font = new Font(fof, --fsize);

            return font;
        }

        Rectangle GetRectangle(Point p1, Point p2)
        {
            int x, y;
            int width, height;

            if (p1.X < p2.X)
            {
                x = p1.X;
                width = p2.X - p1.X;
            }
            else
            {
                x = p2.X;
                width = p1.X - p2.X;
            }

            if (p1.Y < p2.Y)
            {
                y = p1.Y;
                height = p2.Y - p1.Y;
            }
            else
            {
                y = p2.Y;
                height = p1.Y - p2.Y;
            }

            return new Rectangle(x, y, width, height);
        }

        void PaintPoints(ref Graphics g, Pen pen, List<Point> points, bool isEraser = false)
        {
            if (points.Count > 1)
                g.DrawCurve(pen, OptimizeList(points).ToArray());

            else if (points.Count == 1 && !isEraser)
                g.FillEllipse(new SolidBrush(pen.Color),
                    points[0].X - ToolSize / 2, points[0].Y - ToolSize / 2,
                    ToolSize, ToolSize);
        }

        #endregion

        #region Image transformations

        public void ResizeImage(Size newSize)
        {
            if (newSize == Size.Empty)
                return;

            BaseBmp = new Bitmap(BaseBmp, newSize);
            for (int i = 0; i < steps.Count; i++)
                steps[i] = new Bitmap(steps[i], newSize);

            Invalidate();
        }

        public void RotateLeft()
        {
            BaseBmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
            for (int i = 0; i < steps.Count; i++)
                steps[i].RotateFlip(RotateFlipType.Rotate270FlipNone);

            Invalidate();
        }

        public void RotateRight()
        {
            BaseBmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            for (int i = 0; i < steps.Count; i++)
                steps[i].RotateFlip(RotateFlipType.Rotate90FlipNone);

            Invalidate();
        }

        public void Mirror()
        {
            BaseBmp.RotateFlip(RotateFlipType.RotateNoneFlipX);
            for (int i = 0; i < steps.Count; i++)
                steps[i].RotateFlip(RotateFlipType.RotateNoneFlipX);

            Invalidate();
        }

        #endregion

        #region Image positioning utils

        // Find the top left corner location for the BaseBmp based on the current canvas size
        public Point TopLeftCorner()
        {
            if (BaseBmp == null)
                return Point.Empty;

            return new Point((int)(Width / 2f - BaseBmp.Width / 2f) + panX,
                             (int)(Height / 2f - BaseBmp.Height / 2f) + panY);
        }

        // Get the image rectangle
        public Rectangle GetImageRectangle()
        {
            if (BaseBmp == null)
                return Rectangle.Empty;

            return new Rectangle(TopLeftCorner(), new Size(BaseBmp.Width, BaseBmp.Height));
        }

        // Calculates the point from the control to the image
        Point CalculatePointToImage(Point pointFromControl)
        {
            var topLeftImage = TopLeftCorner();

            return new Point(pointFromControl.X - topLeftImage.X,
                             pointFromControl.Y - topLeftImage.Y);
        }

        #endregion

        #region Result generation

        public Bitmap GetFinalResult()
        {
            var finalResult = new Bitmap(BaseBmp.Width, BaseBmp.Height);

            using (var g = Graphics.FromImage(finalResult))
            {
                g.DrawImage(BaseBmp, 0, 0);
                if (CanUseStep)
                    g.DrawImage(steps[currentStep], 0, 0);
            }

            return finalResult;
        }

        #endregion

        #region Undo, redo and clear

        public void Undo()
        {
            if (currentStep > -1)
            {
                currentStep--;
                Invalidate();
            }
        }

        public void Redo()
        {
            if (currentStep < steps.Count - 1)
            {
                currentStep++;
                Invalidate();
            }
        }

        public void Clear()
        {
            currentStep = -1;
            Invalidate();
        }

        void DiscardPostSteps()
        {
            int discard = steps.Count - currentStep;

            if (--discard > 0) // There are steps to discard
            {
                for (int i = 0; i < discard; i++)
                    steps.RemoveAt(steps.Count - 1);

                currentStep = steps.Count - 1; // Set the step to the latest
            }
        }

        #endregion

        #region Optimization and others

        List<Point> OptimizeList(List<Point> points)
        {
            for (int i = 0; i < points.Count - 2; i++)
            {
                float dist = (float)Math.Sqrt(
                    Math.Pow(points[i].X - points[i + 1].X, 2) +
                    Math.Pow(points[i].Y - points[i + 1].Y, 2));

                if (dist < minDistance)
                    points.RemoveAt(i + 1);
            }

            return points;
        }

        // Shall the cursor be shown?
        bool ShowCursor(Point location)
        {
            if (panning)
                return true;

            if (SelectedTool == EditTools.Pen ||
                SelectedTool == EditTools.Marker ||
                SelectedTool == EditTools.Eraser ||
                SelectedTool == EditTools.Pixelate ||
                SelectedTool == EditTools.Blur ||
                SelectedTool == EditTools.PickColor)
                return !GetImageRectangle().Contains(location);

            return true;
        }

        int Clamp(int value, int min = 1, int max = 100)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }

        #endregion

    }
}
