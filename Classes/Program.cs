using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using System.Diagnostics;

namespace Capshot
{
    static class Program
    {
        public static MainForm mainForm;

        // Gets the window location so the mouse is in the center of the form, and the form inside the bounds
        public static Point GetTopLeftLocationCenterMouse(Size size)
        {
            // Calculate the cursor positio
            var left = Cursor.Position.X - size.Width / 2;
            var top = Cursor.Position.Y - size.Height / 2;

            var scrX = Screen.PrimaryScreen.WorkingArea.X;
            var scrY = Screen.PrimaryScreen.WorkingArea.Y;
            var scrS = Screen.PrimaryScreen.WorkingArea.Size;

            // And check it isn't outside the screen
            if (left < 0)
                left = 0;
            else if (left + size.Width > scrS.Width)
                left -= (left + size.Width) - scrS.Width;

            if (top < 0)
                top = 0;
            else if (top + size.Height > scrS.Height)
                top -= (top + size.Height) - scrS.Height;

            return new Point(left, top);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (SingleInstance.IsRunning)
                return;

            Settings.Init("LonamiWebs\\Capshot", new Dictionary<string, object>
            {
                // Quick settings
                { "destinations", (int)(ActionForm.Action.Clipboard | ActionForm.Action.SaveAs) },
                { "captureCursor", true },

                // Colors and sizes
                { "color1", (SerializableColor)Color.Black }, { "color2", (SerializableColor)Color.White },
                { "color3", (SerializableColor)Color.Blue }, { "color4",  (SerializableColor)Color.Lime },
                { "color5", (SerializableColor)Color.Red }, { "color6",   (SerializableColor)Color.Yellow },
                { "color7", (SerializableColor)Color.Pink }, { "color8",  (SerializableColor)Color.Orange },

                { "penSize", 3 }, { "penColor", (SerializableColor)Color.Black },
                { "markerSize", 16 }, { "markerColor", (SerializableColor)Color.Yellow },
                { "eraserSize", 24 },
                { "blurSize", 4 },
                { "pixelateSize", 6 },
                
                // Autosave location
                { "autosaveFolder", Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                    "Capturas de pantalla") },

                // Autosave format
                { "autosaveFormat", "Screenshot_{day}-{month}-{year}_{hour}-{minute}-{second}" },
                { "autosaveExtension", ".png" },

                // Start minimized?
                { "startMinimized", false },

                // Screenshot delay
                { "delay", 0 },

                // Gif maximum time and FPS
                { "gifMax", 10f },
                { "gifFps", 10f },
                { "gifRepeat", 0 },
                { "gifQuality", 5 },

                // First time
                { "firstTime", true },
            });

            mainForm = new MainForm(args.Length > 0 && args[0] == "-min" ? true : false);
            Application.Run(mainForm);
        }
    }
}
