using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HKW.AutoGUI;

/// <summary>
/// 鼠标位置 基于分辨率
/// </summary>
[DebuggerDisplay("X = {X}, Y = {Y}")]
public readonly struct MousePoint
{
    /// <summary>
    /// X轴
    /// </summary>
    public int X { get; }

    /// <summary>
    /// Y轴
    /// </summary>
    public int Y { get; }

    /// <inheritdoc/>
    /// <param name="x">坐标X</param>
    /// <param name="y">坐标Y</param>
    public MousePoint(int x, int y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// 坐标信息
    /// </summary>
    /// <returns>"X = {X}, Y = {Y}"</returns>
    public override string ToString() => $"X = {X}, Y = {Y}";
}
