using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.AutoGUI.Screen;

/// <summary>
/// 定位数据
/// </summary>
[DebuggerDisplay("X = {X}, Y = {Y}, Width = {Width}, Height = {Height}")]
public readonly struct LocateData
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

    /// <inheritdoc/>
    /// <param name="x">X轴坐标</param>
    /// <param name="y">Y轴坐标</param>
    /// <param name="width">宽</param>
    /// <param name="height">高</param>
    public LocateData(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    /// <summary>
    /// 转换为字符串信息
    /// </summary>
    /// <returns>字符串信息</returns>
    public override string ToString()
    {
        return $"X = {X}, Y = {Y}, Width = {Width}, Height = {Height}";
    }
}
