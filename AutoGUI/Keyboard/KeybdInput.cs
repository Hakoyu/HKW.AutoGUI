namespace HKW.AutoGUI.Keyboard;

/// <summary>
/// 键盘输入结构
/// <para>详情查看 <a href="https://learn.microsoft.com/zh-cn/windows/win32/api/winuser/ns-winuser-keybdinput">MSDN</a></para>
/// </summary>
internal struct KeybdInput
{
    /// <summary>
    /// 键码
    /// </summary>
    public ushort KeyCode;

    /// <summary>
    /// 硬件扫描代码
    /// </summary>
    public ushort Scan;

    /// <summary>
    /// 键盘标识符
    /// </summary>
    public KeyboardFlag Flags;

    /// <summary>
    /// 事件的时间戳，单位是毫秒。如果这个参数为零，系统将提供自己的时间戳。
    /// </summary>
    public uint Time;

    /// <summary>
    /// 指定与击键相关的附加值。使用 <see langword="GetMessageExtraInfo"/> 函数来获得这些信息。
    /// </summary>
    public IntPtr ExtraInfo;
}
