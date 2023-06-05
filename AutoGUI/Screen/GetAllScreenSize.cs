using System.Runtime.InteropServices;
using HKW.AutoGUI.Native.Windows;

namespace HKW.AutoGUI.Screen;

internal class GetAllScreenSize
{
    [DllImport("user32.dll")]
    public static extern bool EnumDisplayMonitors(
        IntPtr hdc,
        IntPtr lprcClip,
        MonitorEnumProc lpfnEnum,
        IntPtr dwData
    );

    public delegate bool MonitorEnumProc(
        IntPtr hMonitor,
        IntPtr hdcMonitor,
        ref RECT lprcMonitor,
        IntPtr dwData
    );

    static bool MonitorEnum(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData)
    {
        screens.Add(lprcMonitor);
        return true;
    }

    private static List<RECT> screens = new();

    public static List<RECT> Get()
    {
        screens.Clear();
        EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, MonitorEnum, IntPtr.Zero);

        foreach (RECT screen in screens)
        {
            Console.WriteLine("Width: " + (screen.Right - screen.Left));
            Console.WriteLine("Height: " + (screen.Bottom - screen.Top));
        }
        return screens;
    }
}
