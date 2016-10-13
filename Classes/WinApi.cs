using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Capshot
{
    static class WinApi
    {
        #region Const values

        public const int CURSOR_SHOWING = 0x00000001;

        #endregion

        #region Structures

        [StructLayout(LayoutKind.Sequential)]
        public struct CURSORINFO
        {
            public int cbSize;
            public int flags;
            public IntPtr hCursor;
            public Point ptScreenPos;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWINFO
        {
            public uint cbSize;
            public RECT rcWindow;
            public RECT rcClient;
            public uint dwStyle;
            public uint dwExStyle;
            public uint dwWindowStatus;
            public uint cxWindowBorders;
            public uint cyWindowBorders;
            public ushort atomWindowType;
            public ushort wCreatorVersion;

            public WINDOWINFO(Boolean? filler) : this() // Allows automatic initialization of "cbSize" with "new WINDOWINFO(null/true/false)".
            {
                cbSize = (UInt32)(Marshal.SizeOf(typeof(WINDOWINFO)));
            }

        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ICONINFO
        {
            public bool fIcon;

            public int xHotspot;
            public int yHotspot;

            public IntPtr hbmMask;

            public IntPtr hbmColor;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            int _Left;
            int _Top;
            int _Right;
            int _Bottom;

            public RECT(RECT Rectangle)
                : this(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom) { }

            public RECT(int Left, int Top, int Right, int Bottom) {
                _Left = Left;
                _Top = Top;
                _Right = Right;
                _Bottom = Bottom;
            }

            public int X {
                get { return _Left; }
                set { _Left = value; }
            }
            public int Y {
                get { return _Top; }
                set { _Top = value; }
            }

            public int Left {
                get { return _Left; }
                set { _Left = value; }
            }
            public int Top {
                get { return _Top; }
                set { _Top = value; }
            }
            public int Right {
                get { return _Right; }
                set { _Right = value; }
            }
            public int Bottom {
                get { return _Bottom; }
                set { _Bottom = value; }
            }
            
            public int Height {
                get { return _Bottom - _Top; }
                set { _Bottom = value + _Top; }
            }
            public int Width {
                get { return _Right - _Left; }
                set { _Right = value + _Left; }
            }

            public Point Location {
                get { return new Point(Left, Top); }
                set
                {
                    _Left = value.X;
                    _Top = value.Y;
                }
            }

            public Size Size {
                get { return new Size(Width, Height); }
                set
                {
                    _Right = value.Width + _Left;
                    _Bottom = value.Height + _Top;
                }
            }

            public static implicit operator Rectangle(RECT Rectangle) {
                return new Rectangle(Rectangle.Left, Rectangle.Top, Rectangle.Width, Rectangle.Height);
            }

            public static implicit operator RECT(Rectangle Rectangle) {
                return new RECT(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom);
            }

            public static bool operator ==(RECT Rectangle1, RECT Rectangle2) {
                return Rectangle1.Equals(Rectangle2);
            }

            public static bool operator !=(RECT Rectangle1, RECT Rectangle2) {
                return !Rectangle1.Equals(Rectangle2);
            }

            public override string ToString() {
                return "{Left: " + _Left + "; " + "Top: " + _Top +
                    "; Right: " + _Right + "; Bottom: " + _Bottom + "}";
            }

            public override int GetHashCode() {
                return ToString().GetHashCode();
            }

            public bool Equals(RECT Rectangle)
            {
                return Rectangle.Left == _Left && Rectangle.Top ==
                    _Top && Rectangle.Right == _Right && Rectangle.Bottom == _Bottom;
            }

            public override bool Equals(object Object)
            {
                if (Object is RECT)
                    return Equals((RECT)Object);

                else if (Object is Rectangle)
                    return Equals(new RECT((Rectangle)Object));

                return false;
            }
        }

        #endregion

        #region Enumerations
        
        public enum TernaryRasterOperations : uint
        {
            SRCCOPY = 0x00CC0020,
            SRCPAINT = 0x00EE0086,
            SRCAND = 0x008800C6,
            SRCINVERT = 0x00660046,
            SRCERASE = 0x00440328,
            NOTSRCCOPY = 0x00330008,
            NOTSRCERASE = 0x001100A6,
            MERGECOPY = 0x00C000CA,
            MERGEPAINT = 0x00BB0226,
            PATCOPY = 0x00F00021,
            PATPAINT = 0x00FB0A09,
            PATINVERT = 0x005A0049,
            DSTINVERT = 0x00550009,
            BLACKNESS = 0x00000042,
            WHITENESS = 0x00FF0062,
            CAPTUREBLT = 0x40000000
        }

        #endregion

        #region user32 methods

        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT rect);

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT rect);

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hwnd, out Point lpPoint);

        [DllImport("user32.dll")]
        public static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO piconinfo);

        [DllImport("user32.dll")]
        public static extern bool GetCursorInfo(out CURSORINFO pci);

        [DllImport("user32.dll")]
        public static extern IntPtr CopyIcon(IntPtr hIcon);

        [DllImport("user32.dll", SetLastError = false)]
        public static extern IntPtr GetDesktopWindow();


        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowInfo(IntPtr hWnd, out WINDOWINFO pwi);

        #endregion

        #region gdi32 methods

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC", SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC([In] IntPtr hdc);

        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        public static extern IntPtr SelectObject([In] IntPtr hdc, [In] IntPtr hgdiobj);

        [DllImport("gdi32.dll", EntryPoint = "BitBlt", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BitBlt([In] IntPtr hdc, int nXDest, int nYDest,
            int nWidth, int nHeight, [In] IntPtr hdcSrc, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);
        
        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        public static extern bool DeleteDC([In] IntPtr hdc);

        #endregion

    }
}
