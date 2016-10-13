/* Made by Lonami   Exo
 * Date:  30 / 8 / 2015
 * (C) lonamiwebs.github.io */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Capshot
{
    [DefaultEvent("ValueChanged")]
    public class ColorPickerControl : UserControl
    {
        // Delegates and events to notify the color changed
        public delegate void ValueDelegate(Color color);
        
        [Browsable(true),Category("Property Changed"),Description("Occurs when the selected color changed")]
        public event ValueDelegate ValueChanged;

        Color _SelectedColor;
        [Browsable(true),Category("Data"),Description("The currently selected color")]
        // Get or set the selected colour in the control
        public Color SelectedColor {
            get {
                return _SelectedColor;
            }
            set {
                _SelectedColor = value;
                OnValueChanged(value);
            }
        }

        // Declare our sliders for the three color channels: Red, Green and Blue
        ColorSliderControl cscR = new ColorSliderControl(ColorSliderControl.Channel.R);
        ColorSliderControl cscG = new ColorSliderControl(ColorSliderControl.Channel.G);
        ColorSliderControl cscB = new ColorSliderControl(ColorSliderControl.Channel.B);

        public ColorPickerControl()
        {
            SuspendLayout();

            // This control size shouldn't be affected by the parent control font
            //AutoScaleMode = AutoScaleMode.None;

            // Add our RGB sliders to the base control
            Controls.Add(cscR);
            Controls.Add(cscG);
            Controls.Add(cscB);

            // Suscribe to the sliders OnValueChanged event
            cscR.ValueChanged += OnValueChanged;
            cscG.ValueChanged += OnValueChanged;
            cscB.ValueChanged += OnValueChanged;

            RecalculatePositions();

            ResumeLayout();
        }

        // Some value changed, update all the sliders with the new current colour
        void OnValueChanged(Color color)
        {
            SuspendLayout();

            _SelectedColor = color;
            cscR.SetCurrentColor(color);
            cscG.SetCurrentColor(color);
            cscB.SetCurrentColor(color);

            if (ValueChanged != null)
                ValueChanged(color);

            ResumeLayout();
        }

        // On resize, recalculate the positions
        protected override void OnResize(EventArgs e)
        {
            SuspendLayout();
            RecalculatePositions();
            ResumeLayout();

            base.OnResize(e);
        }

        // Recalculate the slide bars positions
        void RecalculatePositions()
        {
            int height = Height / 3;

            cscR.Width = cscG.Width = cscB.Width = Width;
            cscR.Height = cscG.Height = cscB.Height = height;

            cscG.Location = new Point(0, height);
            cscB.Location = new Point(0, height * 2);

            cscR.Invalidate();
            cscG.Invalidate();
            cscB.Invalidate();
        }
    }


    // It is a PictureBox so it renders better
    class ColorSliderControl : PictureBox
    {
        // Are we selecting the color?
        bool selecting;

        // The pen to indicate the current selected value in the slider
        Pen linePen = new Pen(Color.Black, 1f);

        // An event to notify the value changed
        public event ColorPickerControl.ValueDelegate ValueChanged;

        // The currently selected color, with it's get and set methods
        Color CurrentColor = Color.Black;
        Color GetCurrentColor(byte value)
        {
            switch (SelectedChannel)
            {
                case Channel.R: return Color.FromArgb(value, CurrentColor.G, CurrentColor.B);
                case Channel.G: return Color.FromArgb(CurrentColor.R, value, CurrentColor.B);
                case Channel.B: return Color.FromArgb(CurrentColor.R, CurrentColor.G, value);
                default: return Color.Empty;
            }
        }
        public void SetCurrentColor(Color color)
        {
            CurrentColor = color;
            Invalidate();
        }
        // Get the current byte value for the selected channel
        byte GetCurrentColorValue()
        {
            switch (SelectedChannel)
            {
                case Channel.R: return CurrentColor.R;
                case Channel.G: return CurrentColor.G;
                case Channel.B: return CurrentColor.B;
                default: return 0;
            }
        }

        // The possible channels and the currently selected channel
        public enum Channel { R, G, B }
        Channel SelectedChannel;

        // We always require a channel to be selected
        public ColorSliderControl(Channel channel)
        {
            SelectedChannel = channel;
            BorderStyle = BorderStyle.FixedSingle;
        }

        // Custom control paint
        protected override void OnPaint(PaintEventArgs e)
        {
            // Store the graphics in the g variable for easier manipulation
            var g = e.Graphics;

            // Get the rectangle area of the control
            var rect = new Rectangle(0, 0, Width, Height);
            // Draw with a linear gradient brush, from the CurrentColor to the max value of the current channel
            g.FillRectangle(new LinearGradientBrush(rect,
                GetCurrentColor(0), GetCurrentColor(byte.MaxValue), 0f), rect);

            // Draw the line to indicate the selected value
            var x = GetLineXIndicator();
            g.DrawLine(linePen, x, 0, x, Height);
        }

        // Mouse events
        protected override void OnMouseDown(MouseEventArgs e) {
            selecting = true;
            OnValueChanged(e.Location);
        }
        protected override void OnMouseMove(MouseEventArgs e) {
            OnValueChanged(e.Location);
        }
        protected override void OnMouseUp(MouseEventArgs e) {
            OnValueChanged(e.Location);
            selecting = false;
        }

        // Value changed by a mouse event, so we have to notify it
        void OnValueChanged(Point pt)
        {
            if (!selecting)
                return;

            if (pt.X < 0)
                ValueChanged(GetCurrentColor(0));
            else if (pt.X > Width)
                ValueChanged(GetCurrentColor(byte.MaxValue));
            else
                ValueChanged(GetCurrentColor((byte)((float)pt.X / (float)Width * (float)byte.MaxValue)));
        }

        // Gets the X position of that thin line that represents the current value for the slider
        float GetLineXIndicator()
        { return (float)GetCurrentColorValue() / (float)byte.MaxValue * (float)Width; }
    }
}
