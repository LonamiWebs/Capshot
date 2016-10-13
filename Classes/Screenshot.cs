using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Capshot
{
    public static class Screenshot
    {
        #region Capture cursor

        // Captures the cursor as Bitmap and gives the coordinates of it's location on the screen
        static Bitmap CaptureCursor(out int x, out int y)
        {
            x = -1; y = -1;

            WinApi.CURSORINFO cursorInfo = new WinApi.CURSORINFO();
            cursorInfo.cbSize = Marshal.SizeOf(cursorInfo);
            if (!WinApi.GetCursorInfo(out cursorInfo))
                return null;

            if (cursorInfo.flags != WinApi.CURSOR_SHOWING)
                return null;

            IntPtr hicon = WinApi.CopyIcon(cursorInfo.hCursor);
            if (hicon == IntPtr.Zero)
                return null;

            WinApi.ICONINFO iconInfo;
            if (!WinApi.GetIconInfo(hicon, out iconInfo))
                return null;
            
            x = cursorInfo.ptScreenPos.X - iconInfo.xHotspot;
            y = cursorInfo.ptScreenPos.Y - iconInfo.yHotspot;

            using (Bitmap maskBitmap = Bitmap.FromHbitmap(iconInfo.hbmMask))
            {
                // Is this a monochrome cursor?
                if (maskBitmap.Height == maskBitmap.Width * 2)
                {
                    Bitmap resultBitmap = new Bitmap(maskBitmap.Width, maskBitmap.Width);

                    Graphics desktopGraphics = Graphics.FromHwnd(WinApi.GetDesktopWindow());
                    IntPtr desktopHdc = desktopGraphics.GetHdc();

                    IntPtr maskHdc = WinApi.CreateCompatibleDC(desktopHdc);
                    IntPtr oldPtr = WinApi.SelectObject(maskHdc, maskBitmap.GetHbitmap());

                    using (Graphics resultGraphics = Graphics.FromImage(resultBitmap))
                    {
                        IntPtr resultHdc = resultGraphics.GetHdc();

                        // These two operation will result in a black cursor over a white background.
                        // Later in the code, a call to MakeTransparent() will get rid of the white background.
                        WinApi.BitBlt(resultHdc, 0, 0, 32, 32, maskHdc, 0, 32, WinApi.TernaryRasterOperations.SRCCOPY);
                        WinApi.BitBlt(resultHdc, 0, 0, 32, 32, maskHdc, 0, 0, WinApi.TernaryRasterOperations.SRCINVERT);

                        resultGraphics.ReleaseHdc(resultHdc);
                    }

                    IntPtr newPtr = WinApi.SelectObject(maskHdc, oldPtr);
                    WinApi.DeleteDC(newPtr);
                    WinApi.DeleteDC(maskHdc);
                    desktopGraphics.ReleaseHdc(desktopHdc);

                    // Remove the white background from the BitBlt calls,
                    // resulting in a black cursor over a transparent background.
                    resultBitmap.MakeTransparent(Color.White);
                    return resultBitmap;
                }
            }

            Icon icon = Icon.FromHandle(hicon);
            return icon.ToBitmap();
        }

        #endregion

        #region Capture screen

        // Captures the whole screen
        public static Bitmap CaptureScreen(bool captureCursor) {
            return CopyScreen(Screen.PrimaryScreen.Bounds, captureCursor);
        }

        // Captures a region
        public static Bitmap CaptureRegion(Rectangle location, bool captureCursor) {
            return CopyScreen(location, captureCursor);
        }

        // Captures a region and returns a byte array
        public static byte[] CaptureRegionAsBytes(Rectangle location, bool captureCursor)
        {
            using (var ms = new MemoryStream())
            {
                var img = CopyScreen(location, captureCursor);
                img.Save(ms, ImageFormat.Png);
                img.Dispose();
                return ms.ToArray();
            }
        }

        // Captures the given rectangle of the screen
        static Bitmap CopyScreen(Rectangle rect, bool captureCursor)
        {
            if (rect.Width <= 0 || rect.Height <= 0)
                return null;

            var screenshot = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);

            using (var g = Graphics.FromImage(screenshot))
            {
                g.FillRectangle(Brushes.Black, 0, 0, screenshot.Width, screenshot.Height); // fix black -> transparent
                g.CopyFromScreen(rect.Location, Point.Empty, rect.Size, CopyPixelOperation.SourceCopy);

                if (captureCursor)
                {
                    int cx, cy;
                    var cursor = CaptureCursor(out cx, out cy);
                    
                    if (cursor != null)
                    {
                        // Translate the screen mouse position to the relative captured rectangle
                        cx -= rect.X;
                        cy -= rect.Y;
                        
                        g.DrawImage(cursor, cx, cy);
                    }
                }

                return screenshot;
            }
        }

        #endregion

        #region Capture windows

        public static Bitmap CaptureActiveWindow(bool captureCursor) {
            return CopyScreen(GetActiveWindowRect(), captureCursor);
        }

        #region Extract window info

        public static Rectangle GetActiveWindowRect()
        { return GetWindowRectangle(WinApi.GetForegroundWindow()); }

        static Rectangle GetWindowRectangle(IntPtr handle)
        {
            WinApi.WINDOWINFO wi;
            WinApi.RECT windowRect;

            WinApi.GetWindowRect(handle, out windowRect);
            WinApi.GetWindowInfo(handle, out wi);

            return new WinApi.RECT(windowRect.Left + (int)wi.cxWindowBorders, windowRect.Top,
                windowRect.Right - (int)wi.cxWindowBorders, windowRect.Bottom - (int)wi.cyWindowBorders);
        }

        #endregion

        #endregion
    }
}
