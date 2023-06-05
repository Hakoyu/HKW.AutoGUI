using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using DesktopCapture;
using HKW.AutoGUI.Native.Windows;

namespace HKW.AutoGUI.Screen;

/// <summary>
/// Windows屏幕工具
/// </summary>
[SupportedOSPlatform(nameof(OSPlatform.Windows))]
public class WindowsScreenUtils : IScreenUtils
{
    private readonly DesktopDuplicator r_dd = new();

    /// <inheritdoc/>
    public ScreenResolution Size { get; }

    ///// <inheritdoc/>
    //public ScreenResolution[] Sizes { get; }

    /// <inheritdoc/>
    public WindowsScreenUtils()
    {
        Size = new ScreenResolution(
            WindowsNativeMethods.GetSystemMetrics((int)SystemMetricsIndex.SM_CXSCREEN),
            WindowsNativeMethods.GetSystemMetrics((int)SystemMetricsIndex.SM_CYSCREEN)
        );
    }

    /// <inheritdoc/>
    public Image Screenshot()
    {
        return r_dd.GetLatestFrame(out _).ToImageSharp();
    }

    /// <inheritdoc/>
    public LocateData? LocateOnScreen(string path)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public LocateData? LocateOnScreen(Image image)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public LocateData? LocateOnScreen(string path, int x, int y, int width, int height)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public LocateData? LocateOnScreen(Image image, int x, int y, int width, int height)
    {
        throw new NotImplementedException();
    }
}
