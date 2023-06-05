namespace HKW.AutoGUI.Mouse;

/// <summary>
/// 鼠标标识
/// <para>详情查看: <a href="https://learn.microsoft.com/zh-cn/windows/win32/api/winuser/ns-winuser-mouseinput">MSDN</a></para>
/// </summary>
[Flags]
internal enum MouseFlag : uint // UInt32
{
    /// <summary>
    /// 移动
    /// </summary>
    Move = 0x0001,

    /// <summary>
    /// 左键按下
    /// </summary>
    LeftDown = 0x0002,

    /// <summary>
    /// 左键释放
    /// </summary>
    LeftUp = 0x0004,

    /// <summary>
    /// 右键按下
    /// </summary>
    RightDown = 0x0008,

    /// <summary>
    /// 右键释放
    /// </summary>
    RightUp = 0x0010,

    /// <summary>
    /// 中键按下
    /// </summary>
    MiddleDown = 0x0020,

    /// <summary>
    /// 中键释放
    /// </summary>
    MiddleUp = 0x0040,

    /// <summary>
    /// X键按下
    /// </summary>
    XDown = 0x0080,

    /// <summary>
    /// X键释放
    /// </summary>
    XUp = 0x0100,

    /// <summary>
    /// 滚轮垂直移动
    /// </summary>
    VerticalWheel = 0x0800,

    /// <summary>
    /// 滚轮水平移动
    /// </summary>
    HorizontalWheel = 0x1000,

    /// <summary>
    /// 将坐标映射到整个桌面, 与 <see cref="Absolute"/> 一起使用
    /// </summary>
    VirtualDesk = 0x4000,

    /// <summary>
    /// 使用绝对值坐标 范围: <see langword="0"/> ~ <see langword="65535"/>
    /// </summary>
    Absolute = 0x8000,
}
