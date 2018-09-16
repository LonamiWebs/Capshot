// Made by Lonami Exo | 28-03-2016
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

[DefaultEvent("Scroll")]
public partial class TimeSelector : UserControl
{
    #region Private fields and properties

    // minimum location of where the thumbs can be located in the control
    int minLocation { get { return 0; } }
    // maximum location of where the thumbs can be located in the control
    int maxLocation { get { return Width - rightThumb.Width; } }

    // determines which thumb is moving: -1, 0 or 1 (left, none or right)
    int thumbMoving;

    // private fields for properties
    int _Minimum = 0;
    int _Maximum = 100;
    int _StartValue = 0;
    int _EndValue = 100;
    int _MinimumDistance = 0;

    #endregion

    #region Public properties

    /// <summary>
    /// Gets or sets the minimum possible value for both thumbs
    /// </summary>
    public int Minimum
    {
        get { return _Minimum; }
        set
        {
            if (_Minimum != value)
            {
                _Minimum = value;
                if (_StartValue < _Minimum)
                    _StartValue = _Minimum;

                checkMinimumDistance();
                checkSliderLocations();
            }
        }
    }

    /// <summary>
    /// Gets or sets the maximum possible value for both thumbs
    /// </summary>
    public int Maximum
    {
        get { return _Maximum; }
        set
        {
            if (_Maximum != value)
            {
                _Maximum = value;
                if (_EndValue > _Maximum)
                    _EndValue = _Maximum;

                checkMinimumDistance();
                checkSliderLocations();
            }
        }
    }

    /// <summary>
    /// Gets or sets the value for the left thumb (start value)
    /// </summary>
    public int StartValue
    {
        get { return _StartValue; }
        set
        {
            if (_StartValue != value)
            {
                var oldStartValue = _StartValue;
                setStartValue(value);
                OnScroll(new ScrollEventArgs(
                    ScrollEventType.ThumbTrack,
                    oldStartValue,
                    _StartValue,
                    ScrollOrientation.HorizontalScroll));
            }
        }
    }

    /// <summary>
    /// Gets or sets the value for the left thumb (start value)
    /// </summary>
    public int EndValue
    {
        get { return _EndValue; }
        set
        {
            if (_EndValue != value)
            {
                var oldEndValue = _EndValue;
                setEndValue(value);
                OnScroll(new ScrollEventArgs(
                    ScrollEventType.ThumbTrack,
                    oldEndValue,
                    _EndValue,
                    ScrollOrientation.HorizontalScroll));
            }
        }
    }

    /// <summary>
    /// Gets or sets the minimum distance between the sliders
    /// </summary>
    public int MinimumDistance
    {
        get { return _MinimumDistance; }
        set
        {

            if (_MinimumDistance != value)
            {
                _MinimumDistance = value;
                checkMinimumDistance();
            }
        }
    }

    #endregion

    void setStartValue(int value)
    {
        _StartValue = value < _Minimum ? _Minimum : value;
        checkPushRightThumb();
        checkSliderLocations();
    }

    void setEndValue(int value)
    {
        _EndValue = value > _Maximum ? _Maximum : value;
        checkPushLeftThumb();
        checkSliderLocations();
    }

    #region Checks

    // check pushes
    void checkMinimumDistance()
    {
        if (_Maximum - _Minimum < _MinimumDistance)
            throw new ArgumentOutOfRangeException("MinimumDistance",
                "The value can't be less than the distance between the maximum and the minimum");

        checkPushLeftThumb();
        checkPushRightThumb();
    }

    void checkPushLeftThumb()
    {
        if (_StartValue > _EndValue - _MinimumDistance)
            setStartValue(_EndValue - _MinimumDistance);
    }

    void checkPushRightThumb()
    {
        if (_EndValue < _StartValue + _MinimumDistance)
            setEndValue(_StartValue + _MinimumDistance);
    }

    // update their location on screen
    void checkSliderLocations()
    {
        leftThumb.Location = new Point((int)map(StartValue, _Minimum, _Maximum, minLocation, maxLocation), 0);
        rightThumb.Location = new Point((int)map(EndValue, _Minimum, _Maximum, minLocation, maxLocation), 0);
    }

    // set whether a thumb is moving or not
    void enableMovingCheck(int thumb, MouseEventArgs e)
    {
        OnMouseDown(e);

        thumbMoving = thumb;
        moveCheckTimer.Enabled = true;
    }
    void disableMovingCheck(int thumb, MouseEventArgs e)
    {
        OnMouseUp(e);

        thumbMoving = 0;
        moveCheckTimer.Enabled = false;

        if (thumb < 0)
            OnScroll(new ScrollEventArgs(ScrollEventType.EndScroll, _StartValue));
        else
            OnScroll(new ScrollEventArgs(ScrollEventType.EndScroll, _EndValue));

    }

    #endregion

    #region Constructor

    public TimeSelector()
    {
        InitializeComponent();
    }

    #endregion

    #region Events

    // painting related
    void TimeSelector_Resize(object sender, EventArgs e)
    {
        checkSliderLocations();
    }

    void trackBarLine_Paint(object sender, PaintEventArgs e)
    {
        using (SolidBrush brush = new SolidBrush(SystemColors.ControlDark))
            e.Graphics.FillRectangle(brush, ClientRectangle);
    }

    // moving related
    void leftThumb_MouseDown(object sender, MouseEventArgs e) { enableMovingCheck(-1, e); }
	void rightThumb_MouseDown(object sender, MouseEventArgs e) { enableMovingCheck(1, e); }

	void leftThumb_MouseUp(object sender, MouseEventArgs e) { disableMovingCheck(-1, e); }
	void rightThumb_MouseUp(object sender, MouseEventArgs e) { disableMovingCheck(1, e); }

    // update the current position
    void moveCheckTimer_Tick(object sender, EventArgs e)
    {
        int x;
        switch (thumbMoving)
        {
            case -1:
                x = clamp(PointToClient(MousePosition).X - leftThumb.Width / 2, minLocation, maxLocation);
                StartValue = map(x, minLocation, maxLocation, _Minimum, _Maximum);
                break;

            case 1:
                x = clamp(PointToClient(MousePosition).X - rightThumb.Width / 2, minLocation, maxLocation);
                EndValue = map(x, minLocation, maxLocation, _Minimum, _Maximum);
                break;
        }
    }

    #endregion

    #region Utils

    static int clamp(int value, int min, int max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }

    // maps a value from a start range to a new range
    static int map(double value, double start1, double stop1, double start2, double stop2)
    {
        return (int)(start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1)));
    }

    #endregion
}