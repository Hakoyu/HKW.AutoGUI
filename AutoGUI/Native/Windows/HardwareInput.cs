namespace HKW.AutoGUI.Native.Windows;

/// <summary>
/// 硬件输入结构
/// <para>详情查看: <a href="https://learn.microsoft.com/zh-cn/windows/win32/api/winuser/ns-winuser-hardwareinput">MSDN</a></para>
/// </summary>
internal struct HardwareInput
{
    /// <summary>
    /// 硬件消息
    /// </summary>
    public uint Msg;

    /// <summary>
    /// <see cref="Msg"/> 的 lParam 参数的低序单词.
    /// </summary>
    public ushort ParamL;

    /// <summary>
    /// <see cref="Msg"/> 的 lParam 参数的高序单词.
    /// </summary>
    public ushort ParamH;
}
