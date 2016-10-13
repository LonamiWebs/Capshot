using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

/* Usage to create an animated gif:
 * var age = new AnimatedGifEncoder();
 * age.Start(outputFile);
 * age.SetDelay(ms);
 * age.SetRepeat(repeat); // -1: no repeat, 0: always repeat, n: repeat n times
 * age.AddFrame(frame_n);
 * age.Finish();
 *
/* Usage to decode an animated gif:
 * var gd = new GifDecoder();
 * gd.Read(gifPath);
 * for: gd.GetFrameCount(); -> gif.GetFrame(n);
 */

// TODO I'm not sure if this is able to create TRANSPARENT ANIMATED GIFS, if it's not,
// GetPixels(...) should be done the same way SetPixels(...) is done

// Made 19th of month 9 of 2015.

// ============================ LZWEncoder ==============================
// = Adapted from Jef Poskanzer's Java port by way of J. M. G. Elliott. =
// =                           K Weiner 12/00                           =
// ======================================================================
// GIFCOMPR.C       - GIF Image compression routines
//
// Lempel-Ziv compression based on 'compress'. GIF modifications by
// David Rowley (mgardi@watdcsu.waterloo.edu)
// GIF Image compression - modified 'compress'
//
// Based on: compress.c - File compression ala IEEE Computer, June 1984.
//
// By Authors:  Spencer W. Thomas      (decvax!harpo!utah-cs!utah-gr!thomas)
//              Jim McKie              (decvax!mcvax!jim)
//              Steve Davies           (decvax!vax135!petsd!peora!srd)
//              Ken Turkowski          (decvax!decwrl!turtlevax!ken)
//              James A. Woods         (decvax!ihnp4!ames!jaw)
//              Joe Orost              (decvax!vax135!petsd!joe)

// ==================== NeuQuant Neural-Net Quantization Algorithm =======================
// = Copyright (c) 1994 Anthony Dekker                                                   =
// = NEUQUANT Neural-Net quantization algorithm by Anthony Dekker, 1994.                 =
// = See "Kohonen neural networks for optimal colour quantization"                       =
// = in "Network: Computation in Neural Systems" Vol. 5 (1994) pp 351-367.               =
// = for a discussion of the algorithm.                                                  =
// =                                                                                     =
// = Any party obtaining a copy of these files from the author, directly or              =
// = indirectly, is granted, free of charge, a full and unrestricted irrevocable,        =
// = world-wide, paid up, royalty-free, nonexclusive right and license to deal           =
// = in this software and documentation files (the "Software"), including without        =
// = limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, =
// = and/or sell copies of the Software, and to permit persons who receive               =
// = copies from any such party to do so, with the only requirement being                =
// = that this copyright notice remain intact.                                           =
// =======================================================================================

public class AnimatedGifEncoder
{
    protected int width; // image size
    protected int height;
    protected Color transparent = Color.Empty; // transparent color if given
    protected int transIndex; // transparent index in color table
    protected int repeat = -1; // no repeat
    protected int delay = 0; // frame delay (hundredths)
    protected bool started = false; // ready to output frames
                                    //	protected BinaryWriter bw;
    protected FileStream fs;

    protected Image image; // current frame
    protected byte[] pixels; // BGR byte array from frame
    protected byte[] indexedPixels; // converted frame indexed to palette
    protected int colorDepth; // number of bit planes
    protected byte[] colorTab; // RGB palette
    protected bool[] usedEntry = new bool[256]; // active palette entries
    protected int palSize = 7; // color table size (bits-1)
    protected int dispose = -1; // disposal code (-1 = use default)
    protected bool closeStream = false; // close stream when finished
    protected bool firstFrame = true;
    protected bool sizeSet = false; // if false, get size from first frame
    protected int sample = 10; // default sample interval for quantizer

    /// <summary>
    /// Sets the delay time between each frame, or changes it
    /// for subsequent frames (applies to last frame added)
    /// </summary>
    /// <param name="ms">int delay time in milliseconds</param>
    public void SetDelay(int ms) {
        delay = (int)Math.Round(ms / 10.0f);
    }

    /// <summary>
    /// Sets the GIF frame disposal code for the last added frame
    /// and any subsequent frames. Default is 0 if no transparent
    /// color has been set, otherwise 2
    /// </summary>
    /// <param name="code">int disposal code</param>
    public void SetDispose(int code)
    {
        if (code >= 0)
            dispose = code;
    }
    
    /// <summary>
    /// Sets the number of times the set of GIF frames
    /// should be played. Default is 1; 0 means play
    /// indefinitely. Must be invoked before the first
    /// image is added
    /// </summary>
    /// <param name="iter">int number of iterations</param>
    public void SetRepeat(int iter)
    {
        if (iter >= 0)
            repeat = iter;
    }
    
    /// <summary>
    /// Sets the transparent color for the last added frame
    /// and any subsequent frames.
    /// Since all colors are subject to modification
    /// in the quantization process, the color in the final
    /// palette for each frame closest to the given color
    /// becomes the transparent color for that frame.
    /// May be set to null to indicate no transparent color
    /// </summary>
    /// <param name="c">Color to be treated as transparent on display</param>
    public void SetTransparent(Color c) {
        transparent = c;
    }

    /// <summary>
    /// Adds next GIF frame. The frame is not written immediately, but is
    /// actually deferred until the next frame is received so that timing
    /// data can be inserted. Invoking <code>finish()</code> flushes all
    /// frames. If <code>setSize</code> was not invoked, the size of the
    /// first image is used for all subsequent frames
    /// </summary>
    /// <param name="im">BufferedImage containing frame to write</param>
    /// <returns>true if successful</returns>
    public bool AddFrame(Image im)
    {
        if ((im == null) || !started)
            return false;

        bool ok = true;
        try
        {
            if (!sizeSet) // use first frame's size
                SetSize(im.Width, im.Height);
            image = im;
            GetImagePixels(); // convert to correct format if necessary
            AnalyzePixels(); // build color table & map pixels
            if (firstFrame)
            {
                WriteLSD(); // logical screen descriptior
                WritePalette(); // global color table
                if (repeat >= 0) // use NS app extension to indicate reps
                    WriteNetscapeExt();
            }
            WriteGraphicCtrlExt(); // write graphic control extension
            WriteImageDesc(); // image descriptor
            if (!firstFrame) // local color table
                WritePalette();
            WritePixels(); // encode and write pixel data
            firstFrame = false;
        }
        catch (IOException) { ok = false; }

        return ok;
    }

    
    /// Flushes any pending data and closes output file.
    /// If writing to an OutputStream, the stream is not closed
    /// </summary>
    /// <returns>true if successful</returns>
    public bool Finish()
    {
        if (!started) return false;
        bool ok = true;
        started = false;
        try
        {
            fs.WriteByte(0x3b); // gif trailer
            fs.Flush();
            if (closeStream)
                fs.Close();
        }
        catch (IOException) { ok = false; }

        // reset for subsequent use
        transIndex = 0;
        fs = null;
        image.Dispose();
        image = null;
        pixels = null;
        indexedPixels = null;
        colorTab = null;
        closeStream = false;
        firstFrame = true;

        return ok;
    }

    /// <summary>
    /// Sets frame rate in frames per second. Equivalent to
    /// <code>setDelay(1000/fps)</code>
    /// </summary>
    /// <param name="fps">@param fps float frame rate (frames per second)</param>
    public void SetFrameRate(float fps)
    {
        if (fps != 0f)
            delay = (int)Math.Round(100f / fps);
    }

    /// <summary>
    /// Sets quality of color quantization (conversion of images
    /// to the maximum 256 colors allowed by the GIF specification).
    /// Lower values (minimum = 1) produce better colors, but slow
    /// processing significantly. 10 is the default, and produces
    /// good color mapping at reasonable speeds. Values greater
    /// than 20 do not yield significant improvements in speed
    /// </summary>
    /// <param name="quality">int greater than 0</param>
    public void SetQuality(int quality)
    {
        if (quality < 1) quality = 1;
        sample = quality;
    }

    /// <summary>
    /// Sets the GIF frame size. The default size is the
    /// size of the first frame added if this method is
    /// not invoked
    /// </summary>
    /// <param name="w">int frame width</param>
    /// <param name="h">int frame height</param>
    public void SetSize(int w, int h)
    {
        if (started && !firstFrame) return;
        width = w;
        height = h;
        if (width < 1) width = 320;
        if (height < 1) height = 240;
        sizeSet = true;
    }

    /// <summary>
    /// Initiates GIF file creation on the given stream. The stream
    /// is not closed automatically.
    /// </summary>
    /// <param name="os">OutputStream on which GIF images are written</param>
    /// <returns>false if initial write failed</returns>
    public bool Start(FileStream os)
    {
        if (os == null) return false;
        bool ok = true;
        closeStream = false;
        fs = os;
        try
        {
            WriteString("GIF89a"); // header
        }
        catch (IOException)
        {
            ok = false;
        }
        return started = ok;
    }

    /// <summary>
    /// Initiates writing of a GIF file with the specified name.
    /// </summary>
    /// <param name="file">String containing output file name</param>
    /// <returns>false if open or initial write failed</returns>
    public bool Start(string file)
    {
        bool ok = true;
        try
        {
            //			bw = new BinaryWriter( new FileStream( file, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None ) );
            fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            ok = Start(fs);
            closeStream = true;
        }
        catch (IOException)
        {
            ok = false;
        }
        return started = ok;
    }

    /// <summary>
    /// Analyzes image colors and creates color map.
    /// </summary>
    protected void AnalyzePixels()
    {
        int len = pixels.Length;
        int nPix = len / 3;
        indexedPixels = new byte[nPix];
        NeuQuant nq = new NeuQuant(pixels, len, sample);
        // initialize quantizer
        colorTab = nq.Process(); // create reduced palette
                                    // convert map from BGR to RGB
                                    //			for (int i = 0; i < colorTab.Length; i += 3) 
                                    //			{
                                    //				byte temp = colorTab[i];
                                    //				colorTab[i] = colorTab[i + 2];
                                    //				colorTab[i + 2] = temp;
                                    //				usedEntry[i / 3] = false;
                                    //			}
                                    // map image pixels to new palette
        int k = 0;
        for (int i = 0; i < nPix; i++)
        {
            int index =
                nq.Map(pixels[k++] & 0xff,
                pixels[k++] & 0xff,
                pixels[k++] & 0xff);
            usedEntry[index] = true;
            indexedPixels[i] = (byte)index;
        }
        pixels = null;
        colorDepth = 8;
        palSize = 7;
        // get closest match to transparent color if specified
        if (transparent != Color.Empty)
        {
            transIndex = FindClosest(transparent);
        }
    }
    
	/// <summary>
    /// Returns index of palette color closest to c
    /// </summary>
    /// <param name="c">The original colour</param>
    /// <returns>The most similar colour index</returns>
    protected int FindClosest(Color c)
    {
        if (colorTab == null) return -1;
        int r = c.R;
        int g = c.G;
        int b = c.B;
        int minpos = 0;
        int dmin = 256 * 256 * 256;
        int len = colorTab.Length;
        for (int i = 0; i < len;)
        {
            int dr = r - (colorTab[i++] & 0xff);
            int dg = g - (colorTab[i++] & 0xff);
            int db = b - (colorTab[i] & 0xff);
            int d = dr * dr + dg * dg + db * db;
            int index = i / 3;
            if (usedEntry[index] && (d < dmin))
            {
                dmin = d;
                minpos = index;
            }
            i++;
        }
        return minpos;
    }

    /// <summary>
    /// Extracts image pixels into byte array "pixels"
    /// </summary>
    protected void GetImagePixels()
    {
        int w = image.Width;
        int h = image.Height;
        //		int type = image.GetType().;
        if ((w != width)
            || (h != height)
            )
        {
            // create new image with right size/format
            Image temp =
                new Bitmap(width, height);
            Graphics g = Graphics.FromImage(temp);
            g.DrawImage(image, 0, 0);
            image = temp;
            g.Dispose();
        }
        pixels = new Byte[3 * image.Width * image.Height];
        int count = 0;
        using (var bmp = new Bitmap(image)) // Temp Bitmap
        {
            // Lock the image
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly, bmp.PixelFormat);

            // Create a variable to store the locked bytes of the bitmap
            byte[] bytes = new byte[Math.Abs(data.Stride) * bmp.Height];

            // Get a pointer to the start of our bitmap in the memory
            IntPtr scan = data.Scan0;

            // Copy the bytes from the memory to our byte array
            Marshal.Copy(scan, bytes, 0, bytes.Length);

            // Calculate how many bytes there are per pixel and others variables to reduce calculations
            int bytesPerPixel = Image.GetPixelFormatSize(bmp.PixelFormat) / 8;
            int widthInBytes = data.Width * bytesPerPixel; // The total image width in bytes
            int yyMax = bmp.Height; // The maximum Y coordinate given by the area rectangle
            int xxMax = bmp.Width * bytesPerPixel; // The maximum X coordinate given by the area rectangle

            // Loop through the bitmap rows
            for (int yy = 0; yy < yyMax; yy++)
            {
                // Loop through the bitmap pixels in the row
                for (int xx = 0; xx < xxMax; xx += bytesPerPixel)
                {
                    // CurrentIndex    Get the row    Get the column
                    int ci =        yy * data.Stride + xx;
                    
                    pixels[count++] = bytes[ci + 2]; // Red
                    pixels[count++] = bytes[ci + 1]; // Green
                    pixels[count++] = bytes[ci    ]; // Blue
                }
            }

            // Unlock the bits of the image
            bmp.UnlockBits(data);
        }
    }

    /// <summary>
    /// Writes Graphic Control Extension
    /// </summary>
    protected void WriteGraphicCtrlExt()
    {
        fs.WriteByte(0x21); // extension introducer
        fs.WriteByte(0xf9); // GCE label
        fs.WriteByte(4); // data block size
        int transp, disp;
        if (transparent == Color.Empty)
        {
            transp = 0;
            disp = 0; // dispose = no action
        }
        else
        {
            transp = 1;
            disp = 2; // force clear if using transparent color
        }
        if (dispose >= 0)
        {
            disp = dispose & 7; // user override
        }
        disp <<= 2;

        // packed fields
        fs.WriteByte(Convert.ToByte(0 | // 1:3 reserved
            disp | // 4:6 disposal
            0 | // 7   user input - 0 = none
            transp)); // 8   transparency flag

        WriteShort(delay); // delay x 1/100 sec
        fs.WriteByte(Convert.ToByte(transIndex)); // transparent color index
        fs.WriteByte(0); // block terminator
    }
    
    /// <summary>
    /// Writes Image Descriptor
    /// </summary>
    protected void WriteImageDesc()
    {
        fs.WriteByte(0x2c); // image separator
        WriteShort(0); // image position x,y = 0,0
        WriteShort(0);
        WriteShort(width); // image size
        WriteShort(height);
        // packed fields
        if (firstFrame)
        {
            // no LCT  - GCT is used for first (or only) frame
            fs.WriteByte(0);
        }
        else
        {
            // specify normal LCT
            fs.WriteByte(Convert.ToByte(0x80 | // 1 local color table  1=yes
                0 | // 2 interlace - 0=no
                0 | // 3 sorted - 0=no
                0 | // 4-5 reserved
                palSize)); // 6-8 size of color table
        }
    }

    /// <summary>
    /// Writes Logical Screen Descriptor
    /// </summary>
    protected void WriteLSD()
    {
        // logical screen size
        WriteShort(width);
        WriteShort(height);
        // packed fields
        fs.WriteByte(Convert.ToByte(0x80 | // 1   : global color table flag = 1 (gct used)
            0x70 | // 2-4 : color resolution = 7
            0x00 | // 5   : gct sort flag = 0
            palSize)); // 6-8 : gct size

        fs.WriteByte(0); // background color index
        fs.WriteByte(0); // pixel aspect ratio - assume 1:1
    }

    
    /// <summary>
    /// Writes Netscape application extension to define
    /// repeat count
    /// </summary>
    protected void WriteNetscapeExt()
    {
        fs.WriteByte(0x21); // extension introducer
        fs.WriteByte(0xff); // app extension label
        fs.WriteByte(11); // block size
        WriteString("NETSCAPE" + "2.0"); // app id + auth code
        fs.WriteByte(3); // sub-block size
        fs.WriteByte(1); // loop sub-block id
        WriteShort(repeat); // loop count (extra iterations, 0=repeat forever)
        fs.WriteByte(0); // block terminator
    }

    
    /// <summary>
    /// Writes color table
    /// </summary>
    protected void WritePalette()
    {
        fs.Write(colorTab, 0, colorTab.Length);
        int n = (3 * 256) - colorTab.Length;
        for (int i = 0; i < n; i++)
        {
            fs.WriteByte(0);
        }
    }



    /// <summary>
    /// Encodes and writes pixel data
    /// </summary>
    protected void WritePixels()
    {
        LZWEncoder encoder =
            new LZWEncoder(width, height, indexedPixels, colorDepth);
        encoder.Encode(fs);
    }



    /// <summary>
    /// Write 16-bit value to output stream, LSB first
    /// </summary>
    /// <param name="value">The short to write</param>
    protected void WriteShort(int value)
    {
        fs.WriteByte(Convert.ToByte(value & 0xff));
        fs.WriteByte(Convert.ToByte((value >> 8) & 0xff));
    }

    
    /// <summary>
    /// Writes string to output stream
    /// </summary>
    /// <param name="s">The string to write</param>
    protected void WriteString(String s)
    {
        char[] chars = s.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            fs.WriteByte((byte)chars[i]);
        }
    }
}


public class GifDecoder : IDisposable
{

    // File read status: No errors.
    public static readonly int STATUS_OK = 0;

    // File read status: Error decoding file (may be partially decoded)
    public static readonly int STATUS_FORMAT_ERROR = 1;

    // File read status: Unable to open source.
    public static readonly int STATUS_OPEN_ERROR = 2;

    protected Stream inStream;
    protected int status;

    protected int width; // full image width
    protected int height; // full image height
    protected bool gctFlag; // global color table used
    protected int gctSize; // size of global color table
    protected int loopCount = 1; // iterations; 0 = repeat forever

    protected int[] gct; // global color table
    protected int[] lct; // local color table
    protected int[] act; // active color table

    protected int bgIndex; // background color index
    protected int bgColor; // background color
    protected int lastBgColor; // previous bg color
    protected int pixelAspect; // pixel aspect ratio

    protected bool lctFlag; // local color table flag
    protected bool interlace; // interlace flag
    protected int lctSize; // local color table size

    protected int ix, iy, iw, ih; // current image rectangle
    protected Rectangle lastRect; // last image rect
    protected Image image; // current frame
    protected Bitmap bitmap;
    protected Image lastImage; // previous frame

    protected byte[] block = new byte[256]; // current data block
    protected int blockSize = 0; // block size

    // last graphic control extension info
    protected int dispose = 0;
    // 0=no action; 1=leave in place; 2=restore to bg; 3=restore to prev
    protected int lastDispose = 0;
    protected bool transparency = false; // use transparent color
    protected int delay = 0; // delay in milliseconds
    protected int transIndex; // transparent color index

    protected static readonly int MaxStackSize = 4096;
    // max decoder pixel stack size

    // LZW decoder working arrays
    protected short[] prefix;
    protected byte[] suffix;
    protected byte[] pixelStack;
    protected byte[] pixels;

    protected ArrayList frames; // frames read from current file
    protected int frameCount;

    public class GifFrame
    {
        public GifFrame(Image im, int del)
        {
            image = im;
            delay = del;
        }
        public Image image;
        public int delay;
    }

    /// <summary>
    /// Gets display duration for specified frame
    /// </summary>
    /// <param name="n">int index of frame</param>
    /// <returns>delay in milliseconds</returns>
    public int GetDelay(int n)
    {
        //
        delay = -1;
        if ((n >= 0) && (n < frameCount))
        {
            delay = ((GifFrame)frames[n]).delay;
        }
        return delay;
    }

    /// <summary>
    /// Gets the number of frames read from file
    /// </summary>
    /// <returns>frame count</returns>
    public int GetFrameCount() {
        return frameCount;
    }

    /// <summary>
    /// Gets the first (or only) image read
    /// </summary>
    /// <returns>BufferedImage containing first frame, or null if none</returns>
    public Image GetImage() {
        return GetFrame(0);
    }

    /// <summary>
    /// Gets the "Netscape" iteration count, if any.
    /// A count of 0 means repeat indefinitiely.
    /// </summary>
    /// <returns>Iteration count if one was specified, else 1</returns>
    public int GetLoopCount() {
        return loopCount;
    }
    
	/// <summary>
    /// Creates new frame image from current data (and previous
    /// frames as specified by their disposition codes)
    /// </summary>
    /// <param name="bitmap">Current bitmap data</param>
    /// <returns>Pixels array</returns>
    int[] GetPixels(Bitmap bitmap)
    {
        int[] pixels = new int[3 * image.Width * image.Height];
        int count = 0;

        // Lock the image
        BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
            ImageLockMode.ReadOnly, bitmap.PixelFormat);

        // Create a variable to store the locked bytes of the bitmap
        byte[] bytes = new byte[Math.Abs(data.Stride) * bitmap.Height];

        // Get a pointer to the start of our bitmap in the memory
        IntPtr scan = data.Scan0;

        // Copy the bytes from the memory to our byte array
        Marshal.Copy(scan, bytes, 0, bytes.Length);

        // Calculate how many bytes there are per pixel and others variables to reduce calculations
        int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
        int widthInBytes = data.Width * bytesPerPixel; // The total image width in bytes
        int yyMax = bitmap.Height; // The maximum Y coordinate given by the area rectangle
        int xxMax = bitmap.Width * bytesPerPixel; // The maximum X coordinate given by the area rectangle

        // Loop through the bitmap rows
        for (int yy = 0; yy < yyMax; yy++)
        {
            // Loop through the bitmap pixels in the row
            for (int xx = 0; xx < xxMax; xx += bytesPerPixel)
            {
                // CurrentIndex    Get the row    Get the column
                int ci = yy * data.Stride + xx;

                pixels[count++] = bytes[ci + 2]; // Red
                pixels[count++] = bytes[ci + 1]; // Green
                pixels[count++] = bytes[ci]; // Blue
            }
        }

        // Unlock the bits of the image
        bitmap.UnlockBits(data);

        return pixels;
    }

    void SetPixels(int[] pixels)
    {
        int count = 0;

        // Lock the image
        BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
            ImageLockMode.ReadOnly, bitmap.PixelFormat);

        // Create a variable to store the locked bytes of the bitmap
        byte[] bytes = new byte[Math.Abs(data.Stride) * bitmap.Height];

        // Get a pointer to the start of our bitmap in the memory
        IntPtr scan = data.Scan0;

        // Copy the bytes from the memory to our byte array
        Marshal.Copy(scan, bytes, 0, bytes.Length);

        // Calculate how many bytes there are per pixel and others variables to reduce calculations
        int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
        int widthInBytes = data.Width * bytesPerPixel; // The total image width in bytes
        int yyMax = bitmap.Height; // The maximum Y coordinate given by the area rectangle
        int xxMax = bitmap.Width * bytesPerPixel; // The maximum X coordinate given by the area rectangle

        // Loop through the bitmap rows
        for (int yy = 0; yy < yyMax; yy++)
        {
            // Loop through the bitmap pixels in the row
            for (int xx = 0; xx < xxMax; xx += bytesPerPixel)
            {
                Color color = Color.FromArgb(pixels[count++]);

                // CurrentIndex    Get the row    Get the column
                int ci =        yy * data.Stride + xx;
                bytes[ci + 3] = color.A; // Alpha?
                bytes[ci + 2] = color.R; // Red
                bytes[ci + 1] = color.G; // Green
                bytes[ci    ] = color.B; // Blue
            }
        }

        // Copy back from our destination bytes array to the dst bitmap in the memory
        Marshal.Copy(bytes, 0, scan, bytes.Length);

        // Unlock the bits of the image
        bitmap.UnlockBits(data);
    }

    protected void SetPixels()
    {
        // expose destination image's pixels as int array
        int[] dest = GetPixels(bitmap);

        // fill in starting image contents based on last image's dispose code
        if (lastDispose > 0)
        {
            if (lastDispose == 3)
            {
                // use image before last
                int n = frameCount - 2;
                if (n > 0)
                    lastImage = GetFrame(n - 1);
                else
                    lastImage = null;
            }

            if (lastImage != null)
            {
                //				int[] prev =
                //					((DataBufferInt) lastImage.getRaster().getDataBuffer()).getData();
                int[] prev = GetPixels(new Bitmap(lastImage));
                Array.Copy(prev, 0, dest, 0, width * height);
                // copy pixels

                if (lastDispose == 2)
                {
                    // fill last image rect area with background color
                    Graphics g = Graphics.FromImage(image);
                    Color c = Color.Empty;
                    if (transparency)
                        c = Color.FromArgb(0, 0, 0, 0);  // assume background is transparent
                    else
                        c = Color.FromArgb(lastBgColor); // use given background color

                    Brush brush = new SolidBrush(c);
                    g.FillRectangle(brush, lastRect);
                    brush.Dispose();
                    g.Dispose();
                }
            }
        }

        // copy each source line to the appropriate place in the destination
        int pass = 1;
        int inc = 8;
        int iline = 0;
        for (int i = 0; i < ih; i++)
        {
            int line = i;
            if (interlace)
            {
                if (iline >= ih)
                {
                    pass++;
                    switch (pass)
                    {
                        case 2:
                            iline = 4;
                            break;
                        case 3:
                            iline = 2;
                            inc = 4;
                            break;
                        case 4:
                            iline = 1;
                            inc = 2;
                            break;
                    }
                }
                line = iline;
                iline += inc;
            }
            line += iy;
            if (line < height)
            {
                int k = line * width;
                int dx = k + ix; // start of line in dest
                int dlim = dx + iw; // end of dest line
                if ((k + width) < dlim)
                {
                    dlim = k + width; // past dest edge
                }
                int sx = i * iw; // start of line in source
                while (dx < dlim)
                {
                    // map color and insert in destination
                    int index = ((int)pixels[sx++]) & 0xff;
                    int c = act[index];
                    if (c != 0)
                    {
                        dest[dx] = c;
                    }
                    dx++;
                }
            }
        }
        SetPixels(dest);
    }
    
    /// <summary>
    /// Gets the image contents of frame n
    /// </summary>
    /// <param name="n">The n'th frame</param>
    /// <returns>BufferedImage representation of frame, or null if n is invalid</returns>
    public Image GetFrame(int n)
    {
        Image im = null;
        if ((n >= 0) && (n < frameCount))
            im = ((GifFrame)frames[n]).image;

        return im;
    }

    /// <summary>
    /// Gets image size
    /// </summary>
    /// <returns>GIF image dimensions</returns>
    public Size GetFrameSize() {
        return new Size(width, height);
    }
    
    /// <summary>
    /// Reads GIF image from stream
    /// </summary>
    /// <param name="inStream">BufferedInputStream containing GIF file</param>
    /// <returns>read status code (0 = no errors)</returns>
    public int Read(Stream inStream)
    {
        Init();
        if (inStream != null)
        {
            this.inStream = inStream;
            ReadHeader();
            if (!Error())
            {
                ReadContents();
                if (frameCount < 0)
                    status = STATUS_FORMAT_ERROR;
            }
            inStream.Close();
        }
        else
            status = STATUS_OPEN_ERROR;

        return status;
    }
    
    /// <summary>
    /// Reads GIF file from specified file/URL source  
    /// (URL assumed if name contains ":/" or "file:")
    /// </summary>
    /// <param name="name">String containing source</param>
    /// <returns>read status code (0 = no errors)</returns>
    public int Read(String name)
    {
        status = STATUS_OK;
        try
        {
            name = name.Trim().ToLower();
            status = Read(new FileInfo(name).OpenRead());
        }
        catch (IOException)
        {
            status = STATUS_OPEN_ERROR;
        }

        return status;
    }

    /// <summary>
    /// Decodes LZW image data into pixel array.
    /// Adapted from John Cristy's ImageMagick
    /// </summary>
    protected void DecodeImageData()
    {
        int NullCode = -1;
        int npix = iw * ih;
        int available,
            clear,
            code_mask,
            code_size,
            end_of_information,
            in_code,
            old_code,
            bits,
            code,
            count,
            i,
            datum,
            data_size,
            first,
            top,
            bi,
            pi;

        if ((pixels == null) || (pixels.Length < npix))
        {
            pixels = new byte[npix]; // allocate new pixel array
        }
        if (prefix == null) prefix = new short[MaxStackSize];
        if (suffix == null) suffix = new byte[MaxStackSize];
        if (pixelStack == null) pixelStack = new byte[MaxStackSize + 1];

        //  Initialize GIF data stream decoder.

        data_size = Read();
        clear = 1 << data_size;
        end_of_information = clear + 1;
        available = clear + 2;
        old_code = NullCode;
        code_size = data_size + 1;
        code_mask = (1 << code_size) - 1;
        for (code = 0; code < clear; code++)
        {
            prefix[code] = 0;
            suffix[code] = (byte)code;
        }

        //  Decode GIF pixel stream.

        datum = bits = count = first = top = pi = bi = 0;

        for (i = 0; i < npix;)
        {
            if (top == 0)
            {
                if (bits < code_size)
                {
                    //  Load bytes until there are enough bits for a code.
                    if (count == 0)
                    {
                        // Read a new data block.
                        count = ReadBlock();
                        if (count <= 0)
                            break;
                        bi = 0;
                    }
                    datum += (((int)block[bi]) & 0xff) << bits;
                    bits += 8;
                    bi++;
                    count--;
                    continue;
                }

                //  Get the next code.

                code = datum & code_mask;
                datum >>= code_size;
                bits -= code_size;

                //  Interpret the code

                if ((code > available) || (code == end_of_information))
                    break;
                if (code == clear)
                {
                    //  Reset decoder.
                    code_size = data_size + 1;
                    code_mask = (1 << code_size) - 1;
                    available = clear + 2;
                    old_code = NullCode;
                    continue;
                }
                if (old_code == NullCode)
                {
                    pixelStack[top++] = suffix[code];
                    old_code = code;
                    first = code;
                    continue;
                }
                in_code = code;
                if (code == available)
                {
                    pixelStack[top++] = (byte)first;
                    code = old_code;
                }
                while (code > clear)
                {
                    pixelStack[top++] = suffix[code];
                    code = prefix[code];
                }
                first = ((int)suffix[code]) & 0xff;

                //  Add a new string to the string table,

                if (available >= MaxStackSize)
                    break;
                pixelStack[top++] = (byte)first;
                prefix[available] = (short)old_code;
                suffix[available] = (byte)first;
                available++;
                if (((available & code_mask) == 0)
                    && (available < MaxStackSize))
                {
                    code_size++;
                    code_mask += available;
                }
                old_code = in_code;
            }

            //  Pop a pixel off the pixel stack.

            top--;
            pixels[pi++] = pixelStack[top];
            i++;
        }

        for (i = pi; i < npix; i++)
        {
            pixels[i] = 0; // clear missing pixels
        }

    }
    
    /// <summary>
    /// Returns true if an error was encountered during reading/decoding
    /// </summary>
    /// <returns>true if an error occured</returns>
    protected bool Error()
    {
        return status != STATUS_OK;
    }

    /// <summary>
    /// Initializes or re-initializes reader
    /// </summary>
    protected void Init()
    {
        status = STATUS_OK;
        frameCount = 0;
        frames = new ArrayList();
        gct = null;
        lct = null;
    }
    
    /// <summary>
    /// Reads a single byte from the input stream.
    /// </summary>
    /// <returns>The byte read</returns>
    protected int Read()
    {
        int curByte = 0;
        try
        {
            curByte = inStream.ReadByte();
        }
        catch (IOException)
        {
            status = STATUS_FORMAT_ERROR;
        }
        return curByte;
    }

    /// <summary>
    /// Reads next variable length block from input.
    /// </summary>
    /// <returns>number of bytes stored in "buffer"</returns>
    protected int ReadBlock()
    {
        blockSize = Read();
        int n = 0;
        if (blockSize > 0)
        {
            try
            {
                int count = 0;
                while (n < blockSize)
                {
                    count = inStream.Read(block, n, blockSize - n);
                    if (count == -1)
                        break;
                    n += count;
                }
            }
            catch (IOException)
            {
            }

            if (n < blockSize)
            {
                status = STATUS_FORMAT_ERROR;
            }
        }
        return n;
    }
    
    /// <summary>
    /// Reads color table as 256 RGB integer values
    /// </summary>
    /// <param name="ncolors">int number of colors to read</param>
    /// <returns>int array containing 256 colors (packed ARGB with full alpha)</returns>
    protected int[] ReadColorTable(int ncolors)
    {
        int nbytes = 3 * ncolors;
        int[] tab = null;
        byte[] c = new byte[nbytes];
        int n = 0;
        try
        {
            n = inStream.Read(c, 0, c.Length);
        }
        catch (IOException)
        {
        }
        if (n < nbytes)
        {
            status = STATUS_FORMAT_ERROR;
        }
        else
        {
            tab = new int[256]; // max size to avoid bounds checks
            int i = 0;
            int j = 0;
            while (i < ncolors)
            {
                int r = ((int)c[j++]) & 0xff;
                int g = ((int)c[j++]) & 0xff;
                int b = ((int)c[j++]) & 0xff;
                tab[i++] = (int)(0xff000000 | (r << 16) | (g << 8) | b);
            }
        }
        return tab;
    }
    
    /// <summary>
    /// Main file parser. Reads GIF content blocks
    /// </summary>
    protected void ReadContents()
    {
        // read GIF file content blocks
        bool done = false;
        while (!(done || Error()))
        {
            int code = Read();
            switch (code)
            {

                case 0x2C: // image separator
                    ReadImage();
                    break;

                case 0x21: // extension
                    code = Read();
                    switch (code)
                    {
                        case 0xf9: // graphics control extension
                            ReadGraphicControlExt();
                            break;

                        case 0xff: // application extension
                            ReadBlock();
                            String app = "";
                            for (int i = 0; i < 11; i++)
                                app += (char)block[i];

                            if (app.Equals("NETSCAPE2.0"))
                                ReadNetscapeExt();
                            else
                                Skip(); // don't care
                            break;

                        default: // uninteresting extension
                            Skip();
                            break;
                    }
                    break;

                case 0x3b: // terminator
                    done = true;
                    break;

                case 0x00: // bad byte, but keep going and see what happens
                    break;

                default:
                    status = STATUS_FORMAT_ERROR;
                    break;
            }
        }
    }

    /// <summary>
    /// Reads Graphics Control Extension values
    /// </summary>
    protected void ReadGraphicControlExt()
    {
        Read(); // block size
        int packed = Read(); // packed fields
        dispose = (packed & 0x1c) >> 2; // disposal method
        if (dispose == 0)
            dispose = 1; // elect to keep old image if discretionary

        transparency = (packed & 1) != 0;
        delay = ReadShort() * 10; // delay in milliseconds
        transIndex = Read(); // transparent color index
        Read(); // block terminator
    }

    /// <summary>
    /// Reads GIF file header information
    /// </summary>
    protected void ReadHeader()
    {
        String id = "";
        for (int i = 0; i < 6; i++)
            id += (char)Read();

        if (!id.StartsWith("GIF"))
        {
            status = STATUS_FORMAT_ERROR;
            return;
        }

        ReadLSD();
        if (gctFlag && !Error())
        {
            gct = ReadColorTable(gctSize);
            bgColor = gct[bgIndex];
        }
    }
    
    /// <summary>
    /// Reads next frame image
    /// </summary>
    protected void ReadImage()
    {
        ix = ReadShort(); // (sub)image position & size
        iy = ReadShort();
        iw = ReadShort();
        ih = ReadShort();

        int packed = Read();
        lctFlag = (packed & 0x80) != 0; // 1 - local color table flag
        interlace = (packed & 0x40) != 0; // 2 - interlace flag
                                            // 3 - sort flag
                                            // 4-5 - reserved
        lctSize = 2 << (packed & 7); // 6-8 - local color table size

        if (lctFlag)
        {
            lct = ReadColorTable(lctSize); // read table
            act = lct; // make local table active
        }
        else
        {
            act = gct; // make global table active
            if (bgIndex == transIndex)
                bgColor = 0;
        }
        int save = 0;
        if (transparency)
        {
            save = act[transIndex];
            act[transIndex] = 0; // set transparent color if specified
        }

        if (act == null)
            status = STATUS_FORMAT_ERROR; // no color table defined

        if (Error()) return;

        DecodeImageData(); // decode pixel data
        Skip();

        if (Error()) return;

        frameCount++;

        // create new image to receive frame data
        //		image =
        //			new BufferedImage(width, height, BufferedImage.TYPE_INT_ARGB_PRE);

        bitmap = new Bitmap(width, height);
        image = bitmap;
        SetPixels(); // transfer pixel data to image

        frames.Add(new GifFrame(bitmap, delay)); // add image to frame list

        if (transparency)
            act[transIndex] = save;

        ResetFrame();

    }

    /// <summary>
    /// Reads Logical Screen Descriptor
    /// </summary>
    protected void ReadLSD()
    {

        // logical screen size
        width = ReadShort();
        height = ReadShort();

        // packed fields
        int packed = Read();
        gctFlag = (packed & 0x80) != 0; // 1   : global color table flag
                                        // 2-4 : color resolution
                                        // 5   : gct sort flag
        gctSize = 2 << (packed & 7); // 6-8 : gct size

        bgIndex = Read(); // background color index
        pixelAspect = Read(); // pixel aspect ratio
    }

    /// <summary>
    /// Reads Netscape extenstion to obtain iteration count
    /// </summary>
    protected void ReadNetscapeExt()
    {
        do
        {
            ReadBlock();
            if (block[0] == 1)
            {
                // loop count sub-block
                int b1 = ((int)block[1]) & 0xff;
                int b2 = ((int)block[2]) & 0xff;
                loopCount = (b2 << 8) | b1;
            }
        } while ((blockSize > 0) && !Error());
    }
    
    /// <summary>
    /// Reads next 16-bit value, LSB first
    /// </summary>
    /// <returns>short read</returns>
    protected int ReadShort()
    {
        // read 16-bit value, LSB first
        return Read() | (Read() << 8);
    }

    /// <summary>
    /// Resets frame state for reading next image
    /// </summary>
    protected void ResetFrame()
    {
        lastDispose = dispose;
        lastRect = new Rectangle(ix, iy, iw, ih);
        lastImage = image;
        lastBgColor = bgColor;
        transparency = false;
        delay = 0;
        lct = null;
    }
    
        /// <summary>
        /// Skips variable length blocks up to and including
        /// next zero length block
        /// </summary>
    protected void Skip()
    {
        do
        {
            ReadBlock();
        } while ((blockSize > 0) && !Error());
    }

    public void Dispose()
    {
        image.Dispose();
        bitmap.Dispose();
        lastImage.Dispose();
    }
}


public class LZWEncoder 
{
	static readonly int EOF = -1;

	int imgW, imgH;
	byte[] pixAry;
	int initCodeSize;
	int remaining;
	int curPixel;
	// General DEFINEs
	static readonly int BITS = 12;

	static readonly int HSIZE = 5003; // 80% occupancy

	int n_bits; // number of bits/code
	int maxbits = BITS; // user settable max # bits/code
	int maxcode; // maximum code, given n_bits
	int maxmaxcode = 1 << BITS; // should NEVER generate this code

	int[] htab = new int[HSIZE];
	int[] codetab = new int[HSIZE];

	int hsize = HSIZE; // for dynamic table sizing

	int free_ent = 0; // first unused entry

	// block compression parameters -- after all codes are used up,
	// and compression rate changes, start over.
	bool clear_flg = false;

	// Algorithm:  use open addressing double hashing (no chaining) on the
	// prefix code / next character combination. We do a variant of Knuth's
	// algorithm D (vol. 3, sec. 6.4) along with G. Knott's relatively-prime
	// secondary probe. Here, the modular division first probe is gives way
	// to a faster exclusive-or manipulation. Also do block compression with
	// an adaptive reset, whereby the code table is cleared when the compression
	// ratio decreases, but after the table fills. The variable-length output
	// codes are re-sized at this point, and a special CLEAR code is generated
	// for the decompressor. Late addition:  construct the table according to
	// file size for noticeable speed improvement on small files. Please direct
	// questions about this implementation to ames!jaw.

	int g_init_bits;

	int ClearCode;
	int EOFCode;

	// output
	//
	// Output the given code.
	// Inputs:
	//      code:   A n_bits-bit integer. If == -1, then EOF. This assumes
	//              that n_bits =< wordsize - 1.
	// Outputs:
	//      Outputs code to the file.
	// Assumptions:
	//      Chars are 8 bits long.
	// Algorithm:
	//      Maintain a BITS character long buffer (so that 8 codes will
	// fit in it exactly). Use the VAX insv instruction to insert each
	// code in turn. When the buffer fills up empty it and start over.

	int cur_accum = 0;
	int cur_bits = 0;

	int [] masks =
	{
		0x0000,
		0x0001,
		0x0003,
		0x0007,
		0x000F,
		0x001F,
		0x003F,
		0x007F,
		0x00FF,
		0x01FF,
		0x03FF,
		0x07FF,
		0x0FFF,
		0x1FFF,
		0x3FFF,
		0x7FFF,
		0xFFFF };

	// Number of characters so far in this 'packet'
	int a_count;

	// Define the storage for the packet accumulator
	byte[] accum = new byte[256];

	//----------------------------------------------------------------------------
	public LZWEncoder(int width, int height, byte[] pixels, int color_depth) 
	{
		imgW = width;
		imgH = height;
		pixAry = pixels;
		initCodeSize = Math.Max(2, color_depth);
	}
	
	// Add a character to the end of the current packet, and if it is 254
	// characters, flush the packet to disk.
	void Add(byte c, Stream outs)
	{
		accum[a_count++] = c;
		if (a_count >= 254)
			Flush(outs);
	}
	
	// Clear out the hash table

	// table clear for block compress
	void ClearTable(Stream outs)
	{
		ResetCodeTable(hsize);
		free_ent = ClearCode + 2;
		clear_flg = true;

		Output(ClearCode, outs);
	}
	
	// reset code table
	void ResetCodeTable(int hsize) 
	{
		for (int i = 0; i < hsize; ++i)
			htab[i] = -1;
	}
	
	void Compress(int init_bits, Stream outs)
	{
		int fcode;
		int i;
		int c;
		int ent;
		int disp;
		int hsize_reg;
		int hshift;

		// Set up the globals:  g_init_bits - initial number of bits
		g_init_bits = init_bits;

		// Set up the necessary values
		clear_flg = false;
		n_bits = g_init_bits;
		maxcode = MaxCode(n_bits);

		ClearCode = 1 << (init_bits - 1);
		EOFCode = ClearCode + 1;
		free_ent = ClearCode + 2;

		a_count = 0; // clear packet

		ent = NextPixel();

		hshift = 0;
		for (fcode = hsize; fcode < 65536; fcode *= 2)
			++hshift;
		hshift = 8 - hshift; // set hash code range bound

		hsize_reg = hsize;
		ResetCodeTable(hsize_reg); // clear hash table

		Output(ClearCode, outs);

		outer_loop:
        while ((c = NextPixel()) != EOF) 
		{
			fcode = (c << maxbits) + ent;
			i = (c << hshift) ^ ent; // xor hashing

			if (htab[i] == fcode) 
			{
				ent = codetab[i];
				continue;
			} 
			else if (htab[i] >= 0) // non-empty slot
			{
				disp = hsize_reg - i; // secondary hash (after G. Knott)
				if (i == 0)
					disp = 1;
				do 
				{
					if ((i -= disp) < 0)
						i += hsize_reg;

					if (htab[i] == fcode) 
					{
						ent = codetab[i];
						goto outer_loop;
					}
				} while (htab[i] >= 0);
			}
			Output(ent, outs);
			ent = c;
			if (free_ent < maxmaxcode) 
			{
				codetab[i] = free_ent++; // code -> hashtable
				htab[i] = fcode;
			} 
			else
			ClearTable(outs);
		}
		// Put out the final code.
		Output(ent, outs);
		Output(EOFCode, outs);
	}
	
	//----------------------------------------------------------------------------
	public void Encode( Stream os)
	{
		os.WriteByte( Convert.ToByte( initCodeSize) ); // write "initial code size" byte

		remaining = imgW * imgH; // reset navigation variables
		curPixel = 0;

		Compress(initCodeSize + 1, os); // compress and write the pixel data

		os.WriteByte(0); // write block terminator
	}
	
	// Flush the packet to disk, and reset the accumulator
	void Flush(Stream outs)
	{
		if (a_count > 0) 
		{
			outs.WriteByte( Convert.ToByte( a_count ));
			outs.Write(accum, 0, a_count);
			a_count = 0;
		}
	}
	
	int MaxCode(int n_bits) 
	{
		return (1 << n_bits) - 1;
	}
	
	//----------------------------------------------------------------------------
	// Return the next pixel from the image
	//----------------------------------------------------------------------------
	int NextPixel() 
	{
		if (remaining == 0)
			return EOF;

		--remaining;

		int temp = curPixel + 1;
		if ( temp < pixAry.GetUpperBound( 0 ))
		{
			byte pix = pixAry[curPixel++];

			return pix & 0xff;
		}
		return 0xff;
	}
	
	void Output(int code, Stream outs)
	{
		cur_accum &= masks[cur_bits];

		if (cur_bits > 0)
			cur_accum |= (code << cur_bits);
		else
			cur_accum = code;

		cur_bits += n_bits;

		while (cur_bits >= 8) 
		{
			Add((byte) (cur_accum & 0xff), outs);
			cur_accum >>= 8;
			cur_bits -= 8;
		}

		// If the next entry is going to be too big for the code size,
		// then increase it, if possible.
		if (free_ent > maxcode || clear_flg) 
		{
			if (clear_flg) 
			{
				maxcode = MaxCode(n_bits = g_init_bits);
				clear_flg = false;
			} 
			else 
			{
				++n_bits;
				if (n_bits == maxbits)
					maxcode = maxmaxcode;
				else
					maxcode = MaxCode(n_bits);
			}
		}

		if (code == EOFCode) 
		{
			// At EOF, write the rest of the buffer.
			while (cur_bits > 0) 
			{
				Add((byte) (cur_accum & 0xff), outs);
				cur_accum >>= 8;
				cur_bits -= 8;
			}

			Flush(outs);
		}
	}
}

public class NeuQuant
{
    protected static readonly int netsize = 256; // number of colours used 
                                                    // four primes near 500 - assume no image has a length so large 
                                                    // that it is divisible by all four primes 
    protected static readonly int prime1 = 499;
    protected static readonly int prime2 = 491;
    protected static readonly int prime3 = 487;
    protected static readonly int prime4 = 503;
    protected static readonly int minpicturebytes = (3 * prime4);
    // minimum size for input image 
    // Program Skeleton
	/*	[select samplefac in range 1..30]
		[read image from input file]
		pic = (unsigned char*) malloc(3*width*height);
		initnet(pic,3*width*height,samplefac);
		learn();
		unbiasnet();
		[write output image header, using writecolourmap(f)]
		inxbuild();
		write output image using inxsearch(b,g,r) */

    // Network Definitions
    protected static readonly int maxnetpos = (netsize - 1);
    protected static readonly int netbiasshift = 4; // bias for colour values 
    protected static readonly int ncycles = 100; // no. of learning cycles 

    // defs for freq and bias */
    protected static readonly int intbiasshift = 16; // bias for fractions 
    protected static readonly int intbias = (((int)1) << intbiasshift);
    protected static readonly int gammashift = 10; // gamma = 1024 
    protected static readonly int gamma = (((int)1) << gammashift);
    protected static readonly int betashift = 10;
    protected static readonly int beta = (intbias >> betashift); // beta = 1/1024 
    protected static readonly int betagamma =
        (intbias << (gammashift - betashift));

    // defs for decreasing radius factor 
    protected static readonly int initrad = (netsize >> 3); // for 256 cols, radius starts 
    protected static readonly int radiusbiasshift = 6; // at 32.0 biased by 6 bits 
    protected static readonly int radiusbias = (((int)1) << radiusbiasshift);
    protected static readonly int initradius = (initrad * radiusbias); // and decreases by a 
    protected static readonly int radiusdec = 30; // factor of 1/30 each cycle 

    // defs for decreasing alpha factor 
    protected static readonly int alphabiasshift = 10; // alpha starts at 1.0 
    protected static readonly int initalpha = (((int)1) << alphabiasshift);

    protected int alphadec; // biased by 10 bits 

    // radbias and alpharadbias used for radpower calculation 
    protected static readonly int radbiasshift = 8;
    protected static readonly int radbias = (((int)1) << radbiasshift);
    protected static readonly int alpharadbshift = (alphabiasshift + radbiasshift);
    protected static readonly int alpharadbias = (((int)1) << alpharadbshift);

    // Types and Global Variables

    protected byte[] thepicture; // the input image itself 
    protected int lengthcount; // lengthcount = H*W*3 

    protected int samplefac; // sampling factor 1..30 

    //   typedef int pixel[4];                // BGRc 
    protected int[][] network; // the network itself - [netsize][4] 

    protected int[] netindex = new int[256];
    // for network lookup - really 256 

    protected int[] bias = new int[netsize];
    // bias and freq arrays for learning 
    protected int[] freq = new int[netsize];
    protected int[] radpower = new int[initrad];
    // radpower for precomputation 

    // Initialise network in range (0,0,0) to (255,255,255) and set parameters
    public NeuQuant(byte[] thepic, int len, int sample)
    {

        int i;
        int[] p;

        thepicture = thepic;
        lengthcount = len;
        samplefac = sample;

        network = new int[netsize][];
        for (i = 0; i < netsize; i++)
        {
            network[i] = new int[4];
            p = network[i];
            p[0] = p[1] = p[2] = (i << (netbiasshift + 8)) / netsize;
            freq[i] = intbias / netsize; // 1/netsize 
            bias[i] = 0;
        }
    }

    public byte[] ColorMap()
    {
        byte[] map = new byte[3 * netsize];
        int[] index = new int[netsize];
        for (int i = 0; i < netsize; i++)
            index[network[i][3]] = i;
        int k = 0;
        for (int i = 0; i < netsize; i++)
        {
            int j = index[i];
            map[k++] = (byte)(network[j][0]);
            map[k++] = (byte)(network[j][1]);
            map[k++] = (byte)(network[j][2]);
        }
        return map;
    }

    // Insertion sort of network and building of netindex[0..255] (to do after unbias)
    public void Inxbuild()
    {

        int i, j, smallpos, smallval;
        int[] p;
        int[] q;
        int previouscol, startpos;

        previouscol = 0;
        startpos = 0;
        for (i = 0; i < netsize; i++)
        {
            p = network[i];
            smallpos = i;
            smallval = p[1]; // index on g 
                                // find smallest in i..netsize-1 
            for (j = i + 1; j < netsize; j++)
            {
                q = network[j];
                if (q[1] < smallval)
                { // index on g 
                    smallpos = j;
                    smallval = q[1]; // index on g 
                }
            }
            q = network[smallpos];
            // swap p (i) and q (smallpos) entries 
            if (i != smallpos)
            {
                j = q[0];
                q[0] = p[0];
                p[0] = j;
                j = q[1];
                q[1] = p[1];
                p[1] = j;
                j = q[2];
                q[2] = p[2];
                p[2] = j;
                j = q[3];
                q[3] = p[3];
                p[3] = j;
            }
            // smallval entry is now in position i 
            if (smallval != previouscol)
            {
                netindex[previouscol] = (startpos + i) >> 1;
                for (j = previouscol + 1; j < smallval; j++)
                    netindex[j] = i;
                previouscol = smallval;
                startpos = i;
            }
        }
        netindex[previouscol] = (startpos + maxnetpos) >> 1;
        for (j = previouscol + 1; j < 256; j++)
            netindex[j] = maxnetpos; // really 256 
    }

    // Main Learning Loop
    public void Learn()
    {

        int i, j, b, g, r;
        int radius, rad, alpha, step, delta, samplepixels;
        byte[] p;
        int pix, lim;

        if (lengthcount < minpicturebytes)
            samplefac = 1;
        alphadec = 30 + ((samplefac - 1) / 3);
        p = thepicture;
        pix = 0;
        lim = lengthcount;
        samplepixels = lengthcount / (3 * samplefac);
        delta = samplepixels / ncycles;
        alpha = initalpha;
        radius = initradius;

        rad = radius >> radiusbiasshift;
        if (rad <= 1)
            rad = 0;
        for (i = 0; i < rad; i++)
            radpower[i] =
                alpha * (((rad * rad - i * i) * radbias) / (rad * rad));

        //fprintf(stderr,"beginning 1D learning: initial radius=%d\n", rad);

        if (lengthcount < minpicturebytes)
            step = 3;
        else if ((lengthcount % prime1) != 0)
            step = 3 * prime1;
        else
        {
            if ((lengthcount % prime2) != 0)
                step = 3 * prime2;
            else
            {
                if ((lengthcount % prime3) != 0)
                    step = 3 * prime3;
                else
                    step = 3 * prime4;
            }
        }

        i = 0;
        while (i < samplepixels)
        {
            b = (p[pix + 0] & 0xff) << netbiasshift;
            g = (p[pix + 1] & 0xff) << netbiasshift;
            r = (p[pix + 2] & 0xff) << netbiasshift;
            j = Contest(b, g, r);

            Altersingle(alpha, j, b, g, r);
            if (rad != 0)
                Alterneigh(rad, j, b, g, r); // alter neighbours 

            pix += step;
            if (pix >= lim)
                pix -= lengthcount;

            i++;
            if (delta == 0)
                delta = 1;
            if (i % delta == 0)
            {
                alpha -= alpha / alphadec;
                radius -= radius / radiusdec;
                rad = radius >> radiusbiasshift;
                if (rad <= 1)
                    rad = 0;
                for (j = 0; j < rad; j++)
                    radpower[j] =
                        alpha * (((rad * rad - j * j) * radbias) / (rad * rad));
            }
        }
        //fprintf(stderr,"finished 1D learning: readonly alpha=%f !\n",((float)alpha)/initalpha);
    }

    // Search for BGR values 0..255 (after net is unbiased) and return colour index
    public int Map(int b, int g, int r)
    {

        int i, j, dist, a, bestd;
        int[] p;
        int best;

        bestd = 1000; // biggest possible dist is 256*3 
        best = -1;
        i = netindex[g]; // index on g 
        j = i - 1; // start at netindex[g] and work outwards 

        while ((i < netsize) || (j >= 0))
        {
            if (i < netsize)
            {
                p = network[i];
                dist = p[1] - g; // inx key 
                if (dist >= bestd)
                    i = netsize; // stop iter 
                else
                {
                    i++;
                    if (dist < 0)
                        dist = -dist;
                    a = p[0] - b;
                    if (a < 0)
                        a = -a;
                    dist += a;
                    if (dist < bestd)
                    {
                        a = p[2] - r;
                        if (a < 0)
                            a = -a;
                        dist += a;
                        if (dist < bestd)
                        {
                            bestd = dist;
                            best = p[3];
                        }
                    }
                }
            }
            if (j >= 0)
            {
                p = network[j];
                dist = g - p[1]; // inx key - reverse dif 
                if (dist >= bestd)
                    j = -1; // stop iter 
                else
                {
                    j--;
                    if (dist < 0)
                        dist = -dist;
                    a = p[0] - b;
                    if (a < 0)
                        a = -a;
                    dist += a;
                    if (dist < bestd)
                    {
                        a = p[2] - r;
                        if (a < 0)
                            a = -a;
                        dist += a;
                        if (dist < bestd)
                        {
                            bestd = dist;
                            best = p[3];
                        }
                    }
                }
            }
        }
        return (best);
    }
    public byte[] Process()
    {
        Learn();
        Unbiasnet();
        Inxbuild();
        return ColorMap();
    }

    // Unbias network to give byte values 0..255 and record position i to prepare for sort
    public void Unbiasnet()
    {
        int i;

        for (i = 0; i < netsize; i++)
        {
            network[i][0] >>= netbiasshift;
            network[i][1] >>= netbiasshift;
            network[i][2] >>= netbiasshift;
            network[i][3] = i; // record colour no 
        }
    }

    // Move adjacent neurons by precomputed alpha*(1-((i-j)^2/[r]^2)) in radpower[|i-j|]
    protected void Alterneigh(int rad, int i, int b, int g, int r)
    {

        int j, k, lo, hi, a, m;
        int[] p;

        lo = i - rad;
        if (lo < -1)
            lo = -1;
        hi = i + rad;
        if (hi > netsize)
            hi = netsize;

        j = i + 1;
        k = i - 1;
        m = 1;
        while ((j < hi) || (k > lo))
        {
            a = radpower[m++];
            if (j < hi)
            {
                p = network[j++];
                try
                {
                    p[0] -= (a * (p[0] - b)) / alpharadbias;
                    p[1] -= (a * (p[1] - g)) / alpharadbias;
                    p[2] -= (a * (p[2] - r)) / alpharadbias;
                }
                catch { } // prevents 1.3 miscompilation
            }
            if (k > lo)
            {
                p = network[k--];
                try
                {
                    p[0] -= (a * (p[0] - b)) / alpharadbias;
                    p[1] -= (a * (p[1] - g)) / alpharadbias;
                    p[2] -= (a * (p[2] - r)) / alpharadbias;
                }
                catch { }
            }
        }
    }

    // Move neuron i towards biased (b,g,r) by factor alpha
    protected void Altersingle(int alpha, int i, int b, int g, int r)
    {

        // alter hit neuron 
        int[] n = network[i];
        n[0] -= (alpha * (n[0] - b)) / initalpha;
        n[1] -= (alpha * (n[1] - g)) / initalpha;
        n[2] -= (alpha * (n[2] - r)) / initalpha;
    }

    // Search for biased BGR values
    protected int Contest(int b, int g, int r)
    {

        // finds closest neuron (min dist) and updates freq 
        // finds best neuron (min dist-bias) and returns position 
        // for frequently chosen neurons, freq[i] is high and bias[i] is negative 
        // bias[i] = gamma*((1/netsize)-freq[i]) 

        int i, dist, a, biasdist, betafreq;
        int bestpos, bestbiaspos, bestd, bestbiasd;
        int[] n;

        bestd = ~(((int)1) << 31);
        bestbiasd = bestd;
        bestpos = -1;
        bestbiaspos = bestpos;

        for (i = 0; i < netsize; i++)
        {
            n = network[i];
            dist = n[0] - b;
            if (dist < 0)
                dist = -dist;
            a = n[1] - g;
            if (a < 0)
                a = -a;
            dist += a;
            a = n[2] - r;
            if (a < 0)
                a = -a;
            dist += a;
            if (dist < bestd)
            {
                bestd = dist;
                bestpos = i;
            }
            biasdist = dist - ((bias[i]) >> (intbiasshift - netbiasshift));
            if (biasdist < bestbiasd)
            {
                bestbiasd = biasdist;
                bestbiaspos = i;
            }
            betafreq = (freq[i] >> betashift);
            freq[i] -= betafreq;
            bias[i] += (betafreq << gammashift);
        }
        freq[bestpos] += beta;
        bias[bestpos] -= betagamma;
        return (bestbiaspos);
    }
}
