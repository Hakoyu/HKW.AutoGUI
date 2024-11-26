using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Windows.Win32;
using Windows.Win32.UI.Input.KeyboardAndMouse;

namespace HKW.AutoGUI.Windows;

/// <summary>
/// Windows消息适配器
/// </summary>
[SupportedOSPlatform(nameof(OSPlatform.Windows))]
internal class InputMessageDispatcher : IInputMessageDispatcher
{
    private static readonly int _inputStructureSize = Marshal.SizeOf(typeof(INPUT));

    /// <inheritdoc/>
    /// <exception cref="ArgumentException">若 <paramref name="input"/> 为空</exception>
    /// <exception cref="ArgumentNullException">若 <paramref name="input"/> 为 <see langword="null"/></exception>
    /// <exception cref="Exception">消息发送失败</exception>
    public void DispatchInput(InputBuilder input)
    {
        ArgumentNullException.ThrowIfNull(input);
        if (input.Count == 0)
            throw new ArgumentException("The input array was empty", nameof(input));
        var successful = PInvoke.SendInput(input.AsSpan(), _inputStructureSize);
        if (successful != input.Count)
            throw new Exception(
                "Some simulated input commands were not sent successfully. The most common reason for this happening are the security features of Windows including User Interface Privacy Isolation (UIPI). Your application can only send commands to applications of the same or lower elevation. Similarly certain commands are restricted to Accessibility/UIAutomation applications. Refer to the project home page and the code samples for more information."
            );
    }

    public void DispatchInput(object inputMessage)
    {
        DispatchInput((InputBuilder)inputMessage);
    }
}
