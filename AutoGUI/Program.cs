#if DEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
#endif
namespace HKW.AutoGUI;

internal class Program
{
    public static void Main(string[] args)
    {
#if DEBUG
        HKWAutoGUI.Default.Mouse.AbsoluteMoveTo(30000, 30000, 1000);
        //string file = @"C:\Users\HKW\Desktop\1.bmp";
        //System.Diagnostics.Stopwatch stopWatch = new();
        //stopWatch.Start();
        //HKWAutoGUI.Default.Mouse
        //    .MoveTo(100, 100)
        //    .RandomMotion(10)
        //    .ButtonClick(MouseButton.Left)
        //    .Keyboard.KeyDown(VirtualKeyCode.CONTROL)
        //    .KeyPress(VirtualKeyCode.VK_A)
        //    .KeyPress(VirtualKeyCode.VK_C)
        //    .KeyPress(VirtualKeyCode.VK_V)
        //    .KeyUp();
        //stopWatch.Stop();
        //Console.WriteLine($"\nSTOP {stopWatch.ElapsedMilliseconds:f4}ms");
#endif
    }
}

#if DEBUG

#endif
