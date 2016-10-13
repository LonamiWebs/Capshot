using System.Windows.Forms;

namespace Capshot
{
    class TimeSelectorItem : ToolStripControlHost
    {
        #region Properties

        /// <summary>
        /// Gets or sets the minimum possible value for both thumbs
        /// </summary>
        public int Minimum
        {
            get { return timeSelector.Minimum; }
            set { timeSelector.Minimum = value; }
        }

        /// <summary>
        /// Gets or sets the maximum possible value for both thumbs
        /// </summary>
        public int Maximum
        {
            get { return timeSelector.Maximum; }
            set { timeSelector.Maximum = value; }
        }

        /// <summary>
        /// Gets or sets the value for the left thumb (start value)
        /// </summary>
        public int StartValue
        {
            get { return timeSelector.StartValue; }
            set { timeSelector.StartValue = value; }
        }

        /// <summary>
        /// Gets or sets the value for the left thumb (start value)
        /// </summary>
        public int EndValue
        {
            get { return timeSelector.EndValue; }
            set { timeSelector.EndValue = value; }
        }

        /// <summary>
        /// Gets or sets the minimum distance between the sliders
        /// </summary>
        public int MinimumDistance
        {
            get { return timeSelector.MinimumDistance; }
            set { timeSelector.MinimumDistance = value; }
        }

        #endregion

        public event ScrollEventHandler Scroll;
        void onScroll(ScrollEventArgs e)
        {
            if (Scroll != null)
                Scroll(this, e);
        }

        TimeSelector timeSelector;

        public TimeSelectorItem() : base(new TimeSelector())
        {
            timeSelector = (TimeSelector)Control;
            timeSelector.Scroll += TimeSelector_Scroll;
        }

        void TimeSelector_Scroll(object sender, ScrollEventArgs e)
        {
            onScroll(e);
        }
    }
}
