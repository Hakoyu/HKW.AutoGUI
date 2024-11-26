using System.Buffers;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using HKW.AutoGUI.Windows;
//using HPPH;
using ScreenCapture.NET;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;

namespace HKW.AutoGUI;

internal class Program
{
    public static void Main(string[] args)
    {
#if DEBUG

        //var screenCaptureService = new DX11ScreenCaptureService();
        //IEnumerable<GraphicsCard> graphicsCards = screenCaptureService.GetGraphicsCards();
        //IEnumerable<Display> displays = screenCaptureService.GetDisplays(graphicsCards.First());
        //DX11ScreenCapture screenCapture = screenCaptureService.GetScreenCapture(displays.First());

        //CaptureZone<HPPH.ColorBGRA> fullscreen = screenCapture.RegisterCaptureZone(0, 0, 100, 100);
        //Thread.Sleep(1);
        //var r = screenCapture.CaptureScreen();
        var screenImage = WindowsAutoGUI.Default.ScreenUtils.Screenshot();

        var image = SixLabors.ImageSharp.Image.LoadPixelData<Bgra32>(
            screenImage.ToRawArray(),
            screenImage.Width,
            screenImage.Height
        );

        image.SaveAsPng(@"C:\Users\HKW\Desktop\ScreenImage.png");

#endif
    }
}
