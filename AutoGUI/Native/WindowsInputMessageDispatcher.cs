using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace HKW.AutoGUI.Native;

/// <summary>
/// Windows消息适配器
/// </summary>
internal class WindowsInputMessageDispatcher : IInputMessageDispatcher
{
    private static readonly int sr_lnputSize = Marshal.SizeOf(typeof(InputTypeMessage));

    /// <inheritdoc/>
    /// <exception cref="ArgumentException">若 <paramref name="input"/> 为空</exception>
    /// <exception cref="ArgumentNullException">若 <paramref name="input"/> 为 <see langword="null"/></exception>
    /// <exception cref="Exception">消息发送失败</exception>
    public void DispatchInput(int pause, InputBuilder input)
    {
        if (input == null)
            throw new ArgumentNullException(nameof(input));
        var inputArray = input.ToArray();
        if (inputArray.Length == 0)
            throw new ArgumentException("The input array was empty", nameof(input));
        var successful = NativeMethods.SendInput((uint)inputArray.Length, inputArray, sr_lnputSize);
        if (successful != inputArray.Length)
            throw new Exception(
                "Some simulated input commands were not sent successfully. The most common reason for this happening are the security features of Windows including User Interface Privacy Isolation (UIPI). Your application can only send commands to applications of the same or lower elevation. Similarly certain commands are restricted to Accessibility/UIAutomation applications. Refer to the project home page and the code samples for more information."
            );
        if (pause > 0)
            Thread.Sleep(pause);
    }
}
