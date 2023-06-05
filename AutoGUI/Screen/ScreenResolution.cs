using System.Diagnostics;

namespace HKW.AutoGUI.Screen;

/// <summary>
/// 屏幕分辨率 (像素为单位)
/// </summary>
[DebuggerDisplay("Width = {Width}, Height = {Height}")]
public readonly struct ScreenResolution
{
    /// <summary>
    /// 宽
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// 高
    /// </summary>
    public int Height { get; }

    /// <inheritdoc/>
    /// <param name="width">宽</param>
    /// <param name="height">高</param>
    public ScreenResolution(int width, int height)
    {
        Width = width;
        Height = height;
    }

    /// <summary>
    /// 屏幕分辨率信息
    /// </summary>
    /// <returns>屏幕分辨率信息</returns>
    public override string ToString()
    {
        return string.Format("Width = {0}, Height = {1}", Width, Height);
    }
}
