using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using HKW.AutoGUI.Native.Windows;

namespace HKW.AutoGUI.InputDeviceState;

/// <summary>
/// Windows输入设备状态适配器
/// </summary>
[SupportedOSPlatform(nameof(OSPlatform.Windows))]
public class WindowsInputDeviceStateAdaptor : IInputDeviceStateAdaptor
{
    /// <inheritdoc/>
    public bool IsKeyDown(VirtualKeyCode keyCode)
    {
        var result = WindowsNativeMethods.GetKeyState(keyCode);
        return result < 0;
    }

    /// <inheritdoc/>
    public bool IsKeyUp(VirtualKeyCode keyCode)
    {
        return !IsKeyDown(keyCode);
    }

    /// <inheritdoc/>
    public bool IsHardwareKeyDown(VirtualKeyCode keyCode)
    {
        var result = WindowsNativeMethods.GetAsyncKeyState(keyCode);
        return result < 0;
    }

    /// <inheritdoc/>
    public bool IsHardwareKeyUp(VirtualKeyCode keyCode)
    {
        return !IsHardwareKeyDown(keyCode);
    }

    /// <inheritdoc/>
    public bool IsTogglingKeyInEffect(VirtualKeyCode keyCode)
    {
        var result = WindowsNativeMethods.GetKeyState(keyCode);
        return (result & 0x01) == 0x01;
    }
}
