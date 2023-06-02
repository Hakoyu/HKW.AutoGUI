using System;

namespace HKW.AutoGUI;

/// <summary>
/// 键盘标识符
/// </summary>
[Flags]
internal enum KeyboardFlag : uint
{
    /// <summary>
    /// 如果指定， 则 <see cref="KeybdInput.Scan"/> 扫描代码由两个字节组成的序列组成，其中第一个字节的值为 0xE0。
    /// </summary>
    ExtendedKey = 0x0001,

    /// <summary>
    /// 如果指定，则释放按键。如果未指定，则按下按键。
    /// </summary>
    KeyUp = 0x0002,

    /// <summary>
    /// 如果指定， <see cref="KeybdInput.Scan"/> 将标识密钥，并忽略 <see cref="KeybdInput.KeyCode"/>
    /// </summary>
    Unicode = 0x0004,

    /// <summary>
    /// 如果指定，系统会合成 <see cref="VirtualKeyCode.PACKET"/> 击键。 <see cref="KeybdInput.KeyCode"/> 参数必须为零。
    /// <para>此标志只能与 <see cref="KeyUp"/> 标志组合使用。</para>
    /// </summary>
    ScanCode = 0x0008,
}
