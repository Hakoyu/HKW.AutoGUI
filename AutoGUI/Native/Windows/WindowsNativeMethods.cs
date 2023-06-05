using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using HKW.AutoGUI.Mouse;

namespace HKW.AutoGUI.Native.Windows;

/// <summary>
/// 使用WindowsAPI的本地方法
/// </summary>
[SupportedOSPlatform(nameof(OSPlatform.Windows))]
internal static class WindowsNativeMethods
{
    /// <summary>
    /// 获取按键状态
    /// <para>详情查看: <a href="https://learn.microsoft.com/zh-cn/windows/win32/api/winuser/nf-winuser-getasynckeystate">MSDN</a></para>
    /// </summary>
    /// <param name="virtualKeyCode">虚拟键码</param>
    /// <returns>小于 0 则表示被按下</returns>
    [DllImport("user32.dll", SetLastError = true)]
    public static extern short GetAsyncKeyState(VirtualKeyCode virtualKeyCode);

    /// <summary>
    /// 获取按键的状态
    /// </summary>
    /// <param name="virtualKeyCode">虚拟键码</param>
    /// <returns>
    /// 如果高位为 <see langword="1"/> (负数) 时, 则键为关闭状态, 反之它被按下
    /// <para>如果低位为 <see langword="1"/> , 则为状态切换模式, 适用于部分按键 (如大写键), 反之不是切换状态模式</para>
    /// </returns>
    [DllImport("user32.dll", SetLastError = true)]
    public static extern short GetKeyState(VirtualKeyCode virtualKeyCode);

    /// <summary>
    /// 发送输入信息
    /// <para>如: 按键或鼠标, 按钮点击</para>
    /// </summary>
    /// <param name="sizeOfInputs">结构长度</param>
    /// <param name="inputs">结构数组</param>
    /// <param name="sizeOfInputStructure">输入结构的大小</param>
    /// <returns>The function returns the number of events that it successfully inserted into the keyboard or mouse input stream. If the function returns zero, the input was already blocked by another thread. To get extended error information, call GetLastError.Microsoft Windows Vista. This function fails when it is blocked by User Interface Privilege Isolation (UIPI). Note that neither GetLastError nor the return value will indicate the failure was caused by UIPI blocking.</returns>
    [DllImport("user32.dll", SetLastError = true)]
    public static extern uint SendInput(
        uint sizeOfInputs,
        InputTypeMessage[] inputs,
        int sizeOfInputStructure
    );

    /// <summary>
    /// The GetMessageExtraInfo function retrieves the extra message information for the current thread. Extra message information is an application- or driver-defined value associated with the current thread's message queue.
    /// </summary>
    /// <returns></returns>
    /// <remarks>To set a thread's extra message information, use the SetMessageExtraInfo function. </remarks>
    [DllImport("user32.dll")]
    public static extern IntPtr GetMessageExtraInfo();

    /// <summary>
    /// Used to find the keyboard input scan code for single key input. Some applications do not receive the input when scan is not set.
    /// </summary>
    /// <param name="uCode"></param>
    /// <param name="uMapType"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern uint MapVirtualKey(uint uCode, uint uMapType);

    [DllImport("user32.dll")]
    public static extern int GetSystemMetrics(int nIndex);

    [DllImport("user32.dll")]
    public static extern IntPtr SetCursorPos(int x, int y);

    [DllImport("user32.dll")]
    public static extern IntPtr GetCursorPos(out MousePoint lpPoint);
}
