//using System.Runtime.InteropServices;
//using HKW.AutoGUI.Native.Windows;
using System.Diagnostics;
using System.Drawing;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;

namespace HKW.AutoGUI.Windows;

/// <summary>
/// 屏幕信息
/// </summary>
[DebuggerDisplay(
    "HMONITOR = {HMONITOR}, RealResolution = {RealResolution}, VirtualRectangle = {VirtualRectangle}"
)]
public class WindowsScreenInfo : IScreenInfo
{
    /// <inheritdoc/>
    public WindowsScreenInfo(HMONITOR hMONITOR, Rectangle rect)
    {
        HMONITOR = hMONITOR;
        VirtualResolution = rect.Size;
        VirtualRectangle = rect;
    }

    /// <summary>
    /// 设置驱动信息
    /// </summary>
    /// <param name="dd">显示器驱动</param>
    public void SetDEVICEW(DISPLAY_DEVICEW dd)
    {
        DISPLAY_DEVICEW = dd;
        DeviceName = string.Intern(dd.DeviceName.ToString());
    }

    /// <summary>
    /// 设置模式
    /// </summary>
    /// <param name="d">显示器模式</param>
    public void SetDEVMODEW(DEVMODEW d)
    {
        DEVMODEW = d;
        RealResolution = new((int)d.dmPelsWidth, (int)d.dmPelsHeight);
        DPIScaling = (float)RealResolution.Width / (float)VirtualRectangle.Width;
    }

    /// <inheritdoc/>
    public IntPtr ID => HMONITOR;

    /// <inheritdoc/>
    public float DPIScaling { get; set; }

    /// <inheritdoc/>
    public string DeviceName { get; set; } = string.Empty;

    /// <inheritdoc cref="Windows.Win32.Graphics.Gdi.HMONITOR"/>
    public HMONITOR HMONITOR { get; set; }

    /// <inheritdoc/>
    public Size RealResolution { get; set; }

    /// <inheritdoc/>
    public Size VirtualResolution { get; set; }

    /// <inheritdoc/>
    public Rectangle VirtualRectangle { get; set; }

    /// <inheritdoc cref="Windows.Win32.Graphics.Gdi.DISPLAY_DEVICEW"/>
    public DISPLAY_DEVICEW DISPLAY_DEVICEW { get; set; }

    /// <inheritdoc cref="Windows.Win32.Graphics.Gdi.DEVMODEW"/>
    public DEVMODEW DEVMODEW { get; set; }
}
