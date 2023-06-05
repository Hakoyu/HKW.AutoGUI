using System.Buffers;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using HKW.AutoGUI.AutoGUI;
using HKW.AutoGUI.Native.Windows;
using SixLabors.ImageSharp.Advanced;

namespace HKW.AutoGUI;

internal class Program
{
    public static void Main(string[] args)
    {
#if DEBUG
        //_ = Parallel.For(
        //    0,
        //    100,
        //    y =>
        //    {
        //        var array = ArrayPool<int>.Shared.Rent(10);
        //        Console.WriteLine(y);
        //        ArrayPool<int>.Shared.Return(array);
        //    }
        //);
        //HKWAutoGUI.Default.Mouse.AbsoluteMoveTo(30000, 30000, 1000);
        //string file = @"C:\Users\HKW\Desktop\1.bmp";
        //System.Diagnostics.Stopwatch stopWatch = new();
        //stopWatch.Start();
        //Console.WriteLine(HKWAutoGUI.Default.Mouse.MoveTo(99, 99).OnScreen(100, 100));
        //Console.WriteLine(HKWAutoGUI.Default.Mouse.MoveTo(99, 99).OnScreen(100, 100, 100, 100));
        //Console.WriteLine(HKWAutoGUI.Default.Mouse.MoveTo(201, 201).OnScreen(100, 100, 100, 100));
        //stopWatch.Stop();
        //Console.WriteLine($"\nSTOP {stopWatch.ElapsedMilliseconds:f4}ms");
        string file = @"C:\Users\HKW\Desktop\1.bmp";
        WinGDI32.CaptureScreen().ToImageSharp().Save(file);
        //HKWAutoGUI.Default.Screen.Screenshot().Save(file);
        //DesktopDuplicator dd = new();
        //var bmp = dd.GetLatestFrame(out _);
        //SystemDrawingBridge.From32bppArgbSystemDrawingBitmap<Bgr24>(bmp).Save(file);
        //using MemoryStream ms = new();
        //bmp.Save(ms, ImageFormat.Bmp);
        //SixLabors.ImageSharp.Image.Load(ms.ToArray()).SaveAsBmp(file);
        //Image image = Image.Load(bmp.LockBits().);

#endif
    }

#if DEBUG

#endif
}

#if DEBUG
internal static class TestExtension { }
#endif
