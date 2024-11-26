using System.Runtime.InteropServices;
using Windows.Win32.UI.Input.KeyboardAndMouse;

namespace HKW.AutoGUI.Windows;

/// <summary>
/// GUI自动化模块
/// </summary>
public class WindowsAutoGUI
    : IAutoGUI<WindowsMouseSimulator, WindowsKeyboardSimulator, WindowsScreenUtils, VIRTUAL_KEY>
{
    private static WindowsAutoGUI? _default;

    /// <summary>
    /// 默认实例
    /// </summary>
    public static WindowsAutoGUI Default => _default ??= new();

    /// <inheritdoc/>
    public WindowsAutoGUI()
    {
        ScreenUtils = new();
        Mouse = new(ScreenUtils.ScreenInfos.First());
        Keyboard = new();
        InputDeviceState = new WindowsInputDeviceStateAdaptor();
    }

    /// <inheritdoc/>
    public WindowsMouseSimulator Mouse { get; }

    /// <inheritdoc/>
    public WindowsKeyboardSimulator Keyboard { get; }

    /// <inheritdoc/>
    public IInputDeviceStateAdaptor<VIRTUAL_KEY> InputDeviceState { get; }

    /// <inheritdoc/>
    public WindowsScreenUtils ScreenUtils { get; }
}
