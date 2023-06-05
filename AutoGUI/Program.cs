using System.Buffers;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using HKW.AutoGUI.AutoGUI;
using HKW.AutoGUI.Native.Windows;
using HKW.AutoGUI.Screen;
using SixLabors.ImageSharp.Advanced;

namespace HKW.AutoGUI;

internal class Program
{
    public static void Main(string[] args)
    {
#if DEBUG
        //HKWAutoGUI.Default.Mouse.AbsoluteMoveTo(30000, 30000, 1000);
        //string file = @"C:\Users\HKW\Desktop\1.bmp";
        //System.Diagnostics.Stopwatch stopWatch = new();
        //stopWatch.Start();
        //Console.WriteLine(HKWAutoGUI.Default.Mouse.MoveTo(99, 99).OnScreen(100, 100));
        //Console.WriteLine(HKWAutoGUI.Default.Mouse.MoveTo(99, 99).OnScreen(100, 100, 100, 100));
        //Console.WriteLine(HKWAutoGUI.Default.Mouse.MoveTo(201, 201).OnScreen(100, 100, 100, 100));
        //stopWatch.Stop();
        //Console.WriteLine($"\nSTOP {stopWatch.ElapsedMilliseconds:f4}ms");
        //string file = @"C:\Users\HKW\Desktop\1.bmp";
        //string file = @"C:\Users\HKW\Desktop\TestPicture\test1.bmp";
        //DesktopCapture.DesktopDuplicator dd = new();
        //var bitmap = dd.GetLatestFrame(out _);
        //foreach (int i in Enumerable.Range(0, 1000))
        //{
        //    using var image = bitmap.ToImageSharp();
        //    Console.WriteLine(image.Width);
        //    Thread.Sleep(10);
        //}
        //WinGDI32.CaptureScreen().ToImageSharp().Save(file);
        //HKWAutoGUI.Default.Screen.Screenshot().Save(file);
        //DesktopDuplicator dd = new();
        //var bmp = dd.GetLatestFrame(out _);
        //SystemDrawingBridge.From32bppArgbSystemDrawingBitmap<Bgr24>(bmp).Save(file);
        //using MemoryStream ms = new();
        //bmp.Save(ms, ImageFormat.Bmp);
        //SixLabors.ImageSharp.Image.Load(ms.ToArray()).SaveAsBmp(file);
        //Image image = Image.Load(bmp.LockBits().);
        string locateSoureImageFile = @"C:\Users\HKW\Desktop\TestPicture\LocateSoureImage1.png";
        string locateDestImageFile = @"C:\Users\HKW\Desktop\TestPicture\LocateDestImage.png";
        using var soureImage = Image.Load(locateSoureImageFile);
        using var destImage = Image.Load(locateDestImageFile);
        var locate = LocateOn(soureImage, destImage);
        Console.WriteLine(locate);
#endif
    }

#if DEBUG
    public static LocateData? LocateOn(Image sourceImage, Image destImage)
    {
        if (sourceImage.Width < destImage.Width || sourceImage.Height < destImage.Height)
            throw new ArgumentException(
                $"{nameof(sourceImage)} must be larger than {nameof(destImage)}"
            );
        var sourceGRAYImage = sourceImage.CloneAs<Abgr32>();
        var destGRAYImage = destImage.CloneAs<Abgr32>();
        sourceGRAYImage.Mutate(p => p.Grayscale());
        destGRAYImage.Mutate(p => p.Grayscale());
        var destY = 0;
        var destRows = destGRAYImage.DangerousGetPixelRowMemory(destY).Span;
        int locateX = -1;
        int locateY = -1;
        for (int y = 0; y < sourceGRAYImage.Height; y++)
        {
            var sourceRows = sourceGRAYImage.DangerousGetPixelRowMemory(y).Span;
            int x = 0;
            for (; x < sourceGRAYImage.Width; x++)
            {
                bool getOut = false;
                int dx = 0;
                if (locateX > -1 && destGRAYImage.Height <= sourceGRAYImage.Height - x)
                {
                    destRows = destGRAYImage.DangerousGetPixelRowMemory(++destY).Span;
                    x = locateX;
                    for (; dx < destGRAYImage.Width; dx++, x++)
                    {
                        if (sourceRows[x] != destRows[dx])
                            break;
                    }
                    if (dx < destGRAYImage.Width)
                    {
                        x = locateX;
                        y = locateY;
                        locateX = -1;
                        locateY = -1;
                        destRows = destGRAYImage.DangerousGetPixelRowMemory(destY = 0).Span;
                        getOut = true;
                    }
                    else
                        getOut = true;
                    if (destY == destGRAYImage.Height - 1)
                        return new(locateX, locateY, destGRAYImage.Width, destGRAYImage.Height);
                }
                else if (
                    locateX == -1
                    && sourceRows[x] == destRows[0]
                    && destGRAYImage.Width <= sourceGRAYImage.Width - x
                )
                {
                    locateX = x;
                    locateY = y;
                    for (; dx < destGRAYImage.Width; dx++, x++)
                    {
                        if (sourceRows[x] != destRows[dx])
                            break;
                    }
                    if (dx < destGRAYImage.Width)
                    {
                        x = locateX;
                        locateX = -1;
                        locateY = -1;
                    }
                    else
                        getOut = true;
                }
                if (getOut)
                    break;
            }
        }
        return null;
    }
#endif
}

#if DEBUG
internal static class TestExtension { }
#endif
