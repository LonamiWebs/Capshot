using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace BitmapExtensions
{
    public static class BitmapExtensions
    {
        struct Colour // This class is optimized for our methods!
        {
            public byte r, g, b;

            public Colour(byte b, byte g, byte r)
            {
                this.r = r;
                this.g = g;
                this.b = b;
            }
        }

        public static void MergeMultiply(this Bitmap srcA, Bitmap srcB)
        {
            BitmapData dataA = srcA.LockBits(new Rectangle(0, 0, srcA.Width,
              srcA.Height), ImageLockMode.ReadWrite, srcA.PixelFormat);

            // Height and width MUST be the same, we avoid checking it because this program does not need that check
            BitmapData dataB = srcB.LockBits(new Rectangle(0, 0, srcA.Width,
              srcA.Height), ImageLockMode.ReadWrite, srcA.PixelFormat);

            byte[] srcABytes = new byte[Math.Abs(dataA.Stride) * srcA.Height];
            byte[] srcBBytes = new byte[Math.Abs(dataB.Stride) * srcA.Height];
            IntPtr aScan = dataA.Scan0;
            IntPtr bScan = dataB.Scan0;

            Marshal.Copy(aScan, srcABytes, 0, srcABytes.Length);
            Marshal.Copy(bScan, srcBBytes, 0, srcBBytes.Length);

            int bytesPerPixel = Bitmap.GetPixelFormatSize(srcA.PixelFormat) / 8;

            for (int i = 0; i < srcABytes.Length; i += bytesPerPixel)
            {
                int blue = srcABytes[i] * srcBBytes[i] / 255;
                int green = srcABytes[i + 1] * srcBBytes[i + 1] / 255;
                int red = srcABytes[i + 2] * srcBBytes[i + 2] / 255;

                srcABytes[i] = blue > byte.MaxValue ? byte.MaxValue : (byte)blue;
                srcABytes[i + 1] = green > byte.MaxValue ? byte.MaxValue : (byte)green;
                srcABytes[i + 2] = red > byte.MaxValue ? byte.MaxValue : (byte)red;
            }

            Marshal.Copy(srcABytes, 0, aScan, srcABytes.Length);

            srcA.UnlockBits(dataA);
            srcB.UnlockBits(dataB);
        }


        public static Bitmap Pixelate(this Bitmap ori, Rectangle area, int pixelateSize)
        {
            // Keep in bounds
            if (area.Y < 0) {
                area.Height += area.Y;
                area.Y = 0;
            }
            if (area.X < 0) {
                area.Width += area.X;
                area.X = 0;
            }
            if (area.Y + area.Height > ori.Height)
                area.Height -= (area.Y + area.Height - ori.Height);
            if (area.X + area.Width > ori.Width)
                area.Width -= (area.X + area.Width - ori.Width);

            // We create a rectangle variable in order to lock all the bits of the bitmap without making two Rectangles
            var rct = new Rectangle(0, 0, ori.Width, ori.Height);

            // Create an empty copy of the original bitmap. Here we will save the pixelated pixels
            Bitmap dst = new Bitmap(rct.Width, rct.Height, ori.PixelFormat);

            // Lock both the original and the destination bitmap
            BitmapData dataOri = ori.LockBits(rct, ImageLockMode.ReadOnly, ori.PixelFormat);
            BitmapData dataDst = dst.LockBits(rct, ImageLockMode.ReadWrite, dst.PixelFormat);

            // Create a variable to store the locked bytes of our bitmaps so we can manipulate them
            byte[] oriBytes = new byte[Math.Abs(dataOri.Stride) * ori.Height];
            byte[] dstBytes = new byte[Math.Abs(dataDst.Stride) * dst.Height];

            // Get a pointer to the start of our bitmap in the memory
            IntPtr oriScan = dataOri.Scan0;
            IntPtr dstScan = dataDst.Scan0;

            // Copy the bytes from the memory to our byte array
            Marshal.Copy(oriScan, oriBytes, 0, oriBytes.Length);
            Marshal.Copy(oriScan, dstBytes, 0, dstBytes.Length);

            // Calculate how many bytes there are per pixel
            int bytesPerPixel = Image.GetPixelFormatSize(ori.PixelFormat) / 8;


            // Variables to reduce calculations
            int widthInBytes = dataOri.Width * bytesPerPixel; // The total image width in bytes
            int yyMax = area.Y + area.Height; // The maximum Y coordinate given by the area rectangle
            int xxMax = (area.X + area.Width) * bytesPerPixel; // The maximum X coordinate given by the area rectangle
            int bytesPerPixelated = bytesPerPixel * pixelateSize; // How many bytes we have to move per pixelation?

            // Loop through the bitmap rows
            for (int yy = area.Y; yy < yyMax; yy += pixelateSize)
            {
                // Loop through the bitmap pixels in the row
                for (int xx = area.X * bytesPerPixel; xx < xxMax; xx += bytesPerPixelated)
                {
                    // Calculate the offset for the pixelation
                    int offsetX = (pixelateSize / 2) * bytesPerPixel;
                    int offsetY = pixelateSize / 2;

                    // Keep it inside the bounds
                    while (xx + offsetX >= widthInBytes) offsetX -= bytesPerPixel;
                    while (yy + offsetY >= ori.Height) offsetY--;

                    // CurrentIndex     Get the row              Get the column
                    int ci = ((yy + offsetY) * dataOri.Stride) + (xx + offsetX);
                    Colour pixel = new Colour(oriBytes[ci], oriBytes[ci + 1], oriBytes[ci + 2]); // Blue, Green, Red

                    // Iterate through the pixelated pixels
                    for (int x = xx; x < xx + bytesPerPixelated && x < xxMax; x += bytesPerPixel)
                    {
                        for (int y = yy; y < yy + pixelateSize && y < yyMax; y++)
                        {
                            // DestIndex  Get the row     Get the column
                            int di = y * dataOri.Stride + x;

                            // Set them to the same colour to create the pixelation effect
                            dstBytes[di] = pixel.b;
                            dstBytes[di + 1] = pixel.g;
                            dstBytes[di + 2] = pixel.r;
                        }
                    }
                }
            }

            // Copy back from our destination bytes array to the dst bitmap in the memory
            Marshal.Copy(dstBytes, 0, dstScan, dstBytes.Length);

            // Unlock the bits for both the original image and the destination image
            ori.UnlockBits(dataOri);
            dst.UnlockBits(dataDst);

            // Return the destination image
            return dst;
        }

        public static Bitmap Blur(this Bitmap ori, Rectangle area, int blurSize)
        {
            // Keep in bounds
            if (area.Y < 0) {
                area.Height += area.Y;
                area.Y = 0;
            }
            if (area.X < 0) {
                area.Width += area.X;
                area.X = 0;
            }
            if (area.Y + area.Height > ori.Height)
                area.Height -= (area.Y + area.Height - ori.Height);
            if (area.X + area.Width > ori.Width)
                area.Width -= (area.X + area.Width - ori.Width);

            // We create a rectangle variable in order to lock all the bits of the bitmap without making two Rectangles
            var rct = new Rectangle(0, 0, ori.Width, ori.Height);

            // Create an empty copy of the original bitmap. Here we will save the pixelated pixels
            Bitmap dst = new Bitmap(rct.Width, rct.Height, ori.PixelFormat);

            // Lock both the original and the destination bitmap
            BitmapData dataOri = ori.LockBits(rct, ImageLockMode.ReadOnly, ori.PixelFormat);
            BitmapData dataDst = dst.LockBits(rct, ImageLockMode.ReadWrite, dst.PixelFormat);

            // Create a variable to store the locked bytes of our bitmaps so we can manipulate them
            byte[] oriBytes = new byte[Math.Abs(dataOri.Stride) * ori.Height];
            byte[] dstBytes = new byte[Math.Abs(dataDst.Stride) * dst.Height];

            // Get a pointer to the start of our bitmap in the memory
            IntPtr oriScan = dataOri.Scan0;
            IntPtr dstScan = dataDst.Scan0;

            // Copy the bytes from the memory to our byte array
            Marshal.Copy(oriScan, oriBytes, 0, oriBytes.Length);
            Marshal.Copy(oriScan, dstBytes, 0, dstBytes.Length);

            // Calculate how many bytes there are per pixel
            int bytesPerPixel = Image.GetPixelFormatSize(ori.PixelFormat) / 8;

            // Variables to reduce calculations
            int widthInBytes = ori.Width * bytesPerPixel;
            int yyMax = area.Y + area.Height;
            int xxMax = area.X * bytesPerPixel + area.Width * bytesPerPixel;
            int blurPerPixel = blurSize * bytesPerPixel;

            // Loop through the bitmap rows
            for (int yy = area.Y; yy < yyMax; yy++)
            {
                // Loop through the bitmap pixels in the row
                for (int xx = area.X * bytesPerPixel; xx < xxMax; xx += bytesPerPixel)
                {
                    // Some variables to keep track of the average colour in the blur area
                    int avgR = 0, avgG = 0, avgB = 0;
                    int blurPixelCount = 0;

                    // Average the colour of the red, green and blue for each pixel in the
                    // blur size while making sure you don't go outside the image boundaries
                    for (int x = xx; x < xx + blurPerPixel && x < widthInBytes; x += bytesPerPixel)
                    {
                        for (int y = yy; y < yy + blurSize && y < ori.Height; y++)
                        {
                            // Index  Get the row     Get the column
                            int i = y * dataOri.Stride + x;
                            avgB += oriBytes[i];
                            avgG += oriBytes[i + 1];
                            avgR += oriBytes[i + 2];

                            blurPixelCount++;
                        }
                    }

                    avgR /= blurPixelCount;
                    avgG /= blurPixelCount;
                    avgB /= blurPixelCount;

                    // Now that we know the average for the blur size, set each pixel to that color
                    for (int x = xx; x < xx + blurPerPixel && x < widthInBytes && x < xxMax; x += bytesPerPixel)
                    {
                        for (int y = yy; y < yy + blurSize && y < ori.Height && y < yyMax; y++)
                        {
                            // Index  Get the row     Get the column
                            int i = y * dataOri.Stride + x;

                            // Set them to the same average colour to create the blur effect
                            dstBytes[i] = (byte)avgB;
                            dstBytes[i + 1] = (byte)avgG;
                            dstBytes[i + 2] = (byte)avgR;
                        }
                    }
                }
            }

            // Copy back from our destination bytes array to the dst bitmap in the memory
            Marshal.Copy(dstBytes, 0, dstScan, dstBytes.Length);

            // Unlock the bits for both the original image and the destination image
            ori.UnlockBits(dataOri);
            dst.UnlockBits(dataDst);

            // Return the destination image
            return dst;
        }
    }
}
