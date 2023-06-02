using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.AutoGUI.Native;

internal static class SystemMetrics
{
    public static int ScreenWidth { get; }
    public static int ScreenHeight { get; }

    static SystemMetrics()
    {
        ScreenWidth = NativeMethods.GetSystemMetrics((int)SystemMetricsIndex.SM_CXSCREEN);
        ScreenHeight = NativeMethods.GetSystemMetrics((int)SystemMetricsIndex.SM_CYSCREEN);
    }
}
