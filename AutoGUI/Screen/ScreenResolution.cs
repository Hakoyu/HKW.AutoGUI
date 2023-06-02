using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.AutoGUI;

/// <summary>
/// 屏幕分辨率 (像素为单位)
/// </summary>
[DebuggerDisplay("Width = {Width}, Height = {Height}")]
public ref struct ScreenResolution
{
    /// <summary>
    /// 宽
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// 高
    /// </summary>
    public int Height { get; set; }
}
