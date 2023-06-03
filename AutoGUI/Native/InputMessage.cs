using System.Runtime.InteropServices;

namespace HKW.AutoGUI;

/// <summary>
/// <see cref="NativeMethods.SendInput(uint, InputTypeMessage[], int)"/> 用于存储用于合成输入事件的信息，例如击键、鼠标移动和鼠标单击。
/// <para>详情: <a href="https://learn.microsoft.com/zh-cn/windows/win32/api/winuser/ns-winuser-input">MSDN</a></para>
/// </summary>
[StructLayout(LayoutKind.Explicit)]
internal struct InputMessage
{
    /// <summary>
    /// 鼠标输入结构
    /// </summary>
    [FieldOffset(0)]
    public MouseInput Mouse;

    /// <summary>
    /// 键盘输入结构
    /// </summary>
    [FieldOffset(0)]
    public KeybdInput Keyboard;

    /// <summary>
    /// 硬件输入结构
    /// </summary>
    [FieldOffset(0)]
    public HardwareInput Hardware;
}
