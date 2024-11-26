//using System.Runtime.InteropServices;
//using HKW.AutoGUI.Native.Windows;
using System.Drawing;

namespace HKW.AutoGUI;

/// <summary>
/// 屏幕信息接口
/// </summary>
public interface IScreenInfo
{
    /// <summary>
    /// ID
    /// </summary>
    public IntPtr ID { get; }

    /// <summary>
    /// 屏幕名称
    /// </summary>
    string DeviceName { get; set; }

    /// <summary>
    /// DPI缩放比例
    /// </summary>
    float DPIScaling { get; set; }

    /// <summary>
    /// 真实分辨率
    /// </summary>
    Size RealResolution { get; set; }

    /// <summary>
    /// 虚拟分辨率 (受缩放比例影响)
    /// </summary>
    Rectangle VirtualRectangle { get; set; }

    /// <summary>
    /// 虚拟范围 (受缩放比例影响)
    /// </summary>
    Size VirtualResolution { get; set; }
}
