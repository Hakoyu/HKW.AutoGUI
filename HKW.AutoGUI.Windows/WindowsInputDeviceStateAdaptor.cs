using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Windows.Win32;
using Windows.Win32.UI.Input.KeyboardAndMouse;

namespace HKW.AutoGUI.Windows;

/// <summary>
/// Windows输入设备状态适配器
/// </summary>
[SupportedOSPlatform(nameof(OSPlatform.Windows))]
public class WindowsInputDeviceStateAdaptor : IInputDeviceStateAdaptor<VIRTUAL_KEY>
{
    /// <inheritdoc/>
    public bool CheckKeyDown(VIRTUAL_KEY keyCode)
    {
        var result = PInvoke.GetKeyState((int)keyCode);
        return result < 0;
    }

    /// <inheritdoc/>
    public bool CheckKeyUp(VIRTUAL_KEY keyCode)
    {
        return CheckKeyDown(keyCode) is not true;
    }

    /// <inheritdoc/>
    public bool CheckHardwareKeyDown(VIRTUAL_KEY keyCode)
    {
        var result = PInvoke.GetAsyncKeyState((int)keyCode);
        return result < 0;
    }

    /// <inheritdoc/>
    public bool CheckHardwareKeyUp(VIRTUAL_KEY keyCode)
    {
        return CheckHardwareKeyDown(keyCode) is not true;
    }

    /// <inheritdoc/>
    public bool CheckTogglingKeyInEffect(VIRTUAL_KEY keyCode)
    {
        var result = PInvoke.GetKeyState((int)keyCode);
        return (result & 0x01) == 0x01;
    }
}
