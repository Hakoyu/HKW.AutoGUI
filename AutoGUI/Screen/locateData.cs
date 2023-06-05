using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.AutoGUI.Screen;

/// <summary>
/// 定位数据
/// </summary>
public struct LocateData
{
    /// <summary>
    /// X轴坐标
    /// </summary>
    public int X { get; }

    /// <summary>
    /// Y轴坐标
    /// </summary>
    public int Y { get; }

    /// <summary>
    /// 宽
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// 高
    /// </summary>
    public int Height { get; }
}
