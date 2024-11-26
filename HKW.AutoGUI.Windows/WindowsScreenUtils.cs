//using System.Runtime.InteropServices;
//using HKW.AutoGUI.Native.Windows;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using HKW.HKWUtils.Extensions;
using HPPH;
using ScreenCapture.NET;
using SixLabors.ImageSharp.PixelFormats;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;

namespace HKW.AutoGUI.Windows;

/// <summary>
/// 屏幕工具
/// </summary>
public partial class WindowsScreenUtils : IScreenUtils
{
    /// <inheritdoc/>
    public WindowsScreenUtils()
    {
        ScreenInfos = new(GetScreensInfos());
        ScreenCaptureService = new DX11ScreenCaptureService();
        GraphicsCards = new(ScreenCaptureService.GetGraphicsCards().ToList());
        Displays = new(ScreenCaptureService.GetDisplays(GraphicsCards.First()).ToList());
        ScreenCapture = ScreenCaptureService.GetScreenCapture(Displays.First());
    }

    /// <inheritdoc/>
    public ReadOnlyCollection<IScreenInfo> ScreenInfos { get; }

    /// <summary>
    /// 屏幕截取服务
    /// </summary>
    public DX11ScreenCaptureService ScreenCaptureService { get; }

    /// <summary>
    /// 显示卡
    /// </summary>
    public ReadOnlyCollection<GraphicsCard> GraphicsCards { get; }

    /// <summary>
    /// 显示器
    /// </summary>
    public ReadOnlyCollection<Display> Displays { get; }

    /// <summary>
    /// 屏幕截取
    /// </summary>
    public DX11ScreenCapture ScreenCapture { get; }

    /// <inheritdoc/>
    public IImage<ColorBGRA>? Screenshot()
    {
        var fullscreen = ScreenCapture.RegisterCaptureZone(0, 0, 100, 100);
        // 停顿一下确保成功
        Thread.Sleep(1);
        var r = ScreenCapture.CaptureScreen();
        if (r is false)
            return null;
        var image = fullscreen.Image;
        ScreenCapture.UnregisterCaptureZone(fullscreen);
        return image;
    }

    /// <inheritdoc/>
    public IImage<ColorBGRA>? Screenshot(int x, int y, int width, int height)
    {
        var rectangle = ScreenCapture.RegisterCaptureZone(x, y, width, height);
        // 停顿一下确保成功
        Thread.Sleep(1);
        var r = ScreenCapture.CaptureScreen();
        if (r is false)
            return null;
        var image = rectangle.Image;
        ScreenCapture.UnregisterCaptureZone(rectangle);
        return image;
    }

    public SixLabors.ImageSharp.Rectangle? LocateOnScreen(SixLabors.ImageSharp.Image image)
    {
        throw new NotImplementedException();
    }

    public SixLabors.ImageSharp.Rectangle? LocateOnScreen(
        SixLabors.ImageSharp.Image image,
        int x,
        int y,
        int width,
        int height
    )
    {
        throw new NotImplementedException();
    }

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool EnumDisplayMonitors(
        IntPtr hdc,
        IntPtr lprcClip,
        MonitorEnumProc lpfnEnum,
        IntPtr dwData
    );

    internal delegate bool MonitorEnumProc(
        IntPtr hMonitor,
        IntPtr hdcMonitor,
        ref Rectangle lprcMonitor,
        IntPtr dwData
    );

    /// <summary>
    /// 获取全部屏幕的信息
    /// </summary>
    /// <param name="screenCount">最大屏幕数量</param>
    /// <returns>屏幕信息</returns>
    public static List<IScreenInfo> GetScreensInfos(int screenCount = 10)
    {
        var infos = new List<WindowsScreenInfo>();
        EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, MonitorEnum, IntPtr.Zero);

        // 获取屏幕驱动
        for (uint i = 0, s = 0; i < screenCount; i++)
        {
            var dd = new DISPLAY_DEVICEW();
            dd.cb = (uint)Marshal.SizeOf(dd);
            var r = PInvoke.EnumDisplayDevices(null, i, ref dd, 1);
            if (r == 0)
                continue;
            var ddt = new DISPLAY_DEVICEW();
            // 二次验证
            r = PInvoke.EnumDisplayDevices(dd.DeviceName.ToString(), 0, ref ddt, 1);
            if (r == 0)
                continue;
            infos[(int)s++].SetDEVICEW(dd);
        }

        for (var i = 0; i < infos.Count; i++)
        {
            var info = infos[i];
            var dm = new DEVMODEW();
            var r = PInvoke.EnumDisplaySettings(
                info.DeviceName.ToString(),
                ENUM_DISPLAY_SETTINGS_MODE.ENUM_CURRENT_SETTINGS,
                ref dm
            );
            if (r == 0)
                continue;
            info.SetDEVMODEW(dm);
        }
        return infos.Cast<IScreenInfo>().ToList();

        bool MonitorEnum(
            IntPtr hMonitor,
            IntPtr hdcMonitor,
            ref Rectangle lprcMonitor,
            IntPtr dwData
        )
        {
            infos.Add(new(new HMONITOR(hMonitor), lprcMonitor));
            return true;
        }
    }
}
