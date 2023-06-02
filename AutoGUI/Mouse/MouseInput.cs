using System;

namespace HKW.AutoGUI;

/// <summary>
/// 鼠标输入结构
/// 详情查看: <a href="https://learn.microsoft.com/zh-cn/windows/win32/api/winuser/ns-winuser-mouseinput">MSDN</a>
/// </summary>
internal struct MouseInput
{
    /// <summary>
    /// 绝对值坐标 范围: <see langword="0"/> ~ <see langword="65535"/>
    /// <para>或是相对运动量 (单位为像素)</para>
    /// </summary>
    public int X;

    /// <summary>
    /// 绝对值坐标 范围: <see langword="0"/> ~ <see langword="65535"/>
    /// <para>或是相对运动量 (单位为像素)</para>
    /// </summary>
    public int Y;

    /// <summary>
    /// 如果 <see cref="Flags"/> 包含 <see cref="MouseFlag.VerticalWheel"/>, 则指定为滚轮移动量。
    /// 正值表示滚轮向前旋转, 远离用户;负值表示滚轮向后旋转, 朝向用户方向旋转。
    /// 一次滚轮点击定义为 <see cref="IMouseSimulator.MouseWheelClickSize"/>, 即 <see langword="120"/>。
    /// <para>Windows Vista：如果 Flags 包含 <see cref="MouseFlag.HorizontalWheel"/>, 则指定为滚轮移动量。
    /// 正值表示车轮向右旋转;负值表示滚轮向左旋转。一次滚轮点击定义为 <see cref="IMouseSimulator.MouseWheelClickSize"/>, 即 <see langword="120"/>。</para>
    /// <para>如果 <see cref="Flags"/> 不包含 <see cref="MouseFlag.VerticalWheel"/>、<see cref="MouseFlag.XDown"/> 或 <see cref="MouseFlag.XUp"/>, 则应为零。</para>
    /// <para>如果 <see cref="Flags"/> 包含 <see cref="MouseFlag.XDown"/> 或 <see cref="MouseFlag.XUp"/>, 则指定为按下或释放了哪些 X 按钮。
    /// 此值可以是 <see cref="MouseFlag.XDown"/>, <see cref="MouseFlag.XUp"/> 的任意组合。</para>
    /// </summary>
    public uint MouseData;

    /// <summary>
    /// 一组位标志, 用于指定鼠标移动和按钮单击的各个方面。
    /// 此成员中的位可以是 <see cref="MouseFlag"/> 的任意合理组合。
    /// <para>
    /// 指定鼠标按钮状态的位标志设置为指示状态更改, 而不是正在进行的条件。
    /// 例如, 如果按住鼠标左键, 则在首次按下左键时设置 <see cref="MouseFlag.LeftDown"/>, 但不为后续动作设置。
    /// 同样, 仅在首次释放按钮时设置 <see cref="MouseFlag.LeftUp"/>。
    /// </para>
    /// <para>
    /// 不能同时指定 <see cref="MouseFlag.VerticalWheel"/> 和 <see cref="MouseFlag.XDown"/> 或 <see cref="MouseFlag.XUp"/> 标志, 因为它们都需要使用 <see cref="MouseData"/> 字段。
    /// </para>
    /// </summary>
    public MouseFlag Flags;

    /// <summary>
    /// 事件的时间戳 (以毫秒为单位)
    /// 如果此参数为 0, 系统将提供自己的时间戳。
    /// </summary>
    public uint Time;

    /// <summary>
    /// 与鼠标事件关联的附加值。
    /// 应用程序调用 GetMessageExtraInfo 来获取此额外信息。
    /// </summary>
    public IntPtr ExtraInfo;
}
