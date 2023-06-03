using System;
using HKW.AutoGUI;

namespace HKW.AutoGUI;

/// <summary>
/// GUI自动化模块
/// </summary>
public class HKWAutoGUI : IAutoGUI
{
    /// <inheritdoc/>
    public int Pause { get; set; } = 0;

    /// <inheritdoc/>
    public IKeyboardSimulator Keyboard { get; }

    /// <inheritdoc/>
    public IMouseSimulator Mouse { get; }

    /// <inheritdoc/>
    public IInputDeviceStateAdaptor InputDeviceState { get; }

    /// <inheritdoc/>
    public IScreenUtils Screen { get; }

    /// <summary>
    /// 默认实例
    /// </summary>
    public static HKWAutoGUI Default { get; } = new();

    /// <inheritdoc/>
    /// <param name="mouseSimulator">鼠标模拟</param>
    /// <param name="keyboardSimulator">键盘模拟</param>
    /// <param name="inputDeviceStateAdaptor">设备输入状态</param>
    /// <param name="screenUtils">屏幕工具</param>
    public HKWAutoGUI(
        IMouseSimulator mouseSimulator,
        IKeyboardSimulator keyboardSimulator,
        IInputDeviceStateAdaptor inputDeviceStateAdaptor,
        IScreenUtils screenUtils
    )
    {
        Screen = screenUtils;
        Mouse = mouseSimulator;
        Keyboard = keyboardSimulator;
        InputDeviceState = inputDeviceStateAdaptor;
    }

    /// <inheritdoc/>
    public HKWAutoGUI()
    {
        Screen = new WindowsScreenUtils();
        Keyboard = new WindowsKeyboardSimulator(this);
        Mouse = new WindowsMouseSimulator(this);
        InputDeviceState = new WindowsInputDeviceStateAdaptor();
    }
}
