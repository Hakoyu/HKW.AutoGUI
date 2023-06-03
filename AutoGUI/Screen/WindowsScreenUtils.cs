using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.AutoGUI;

/// <summary>
/// Windows屏幕工具
/// </summary>
public class WindowsScreenUtils : IScreenUtils
{
    /// <inheritdoc/>
    public ScreenResolution Size { get; }

    ///// <inheritdoc/>
    //public ScreenResolution[] Sizes { get; }

    /// <inheritdoc/>
    public WindowsScreenUtils()
    {
        Size = new ScreenResolution(
            NativeMethods.GetSystemMetrics((int)SystemMetricsIndex.SM_CXSCREEN),
            NativeMethods.GetSystemMetrics((int)SystemMetricsIndex.SM_CYSCREEN)
        );
    }
}
