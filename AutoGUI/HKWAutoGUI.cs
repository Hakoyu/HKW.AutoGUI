using System;
using HKW.AutoGUI.Native;

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

    /// <summary>
    /// 默认实例
    /// </summary>
    public static HKWAutoGUI Default { get; } = new();

    /// <inheritdoc/>
    /// <param name="keyboardSimulator">用于模拟键盘输入的 <see cref="IKeyboardSimulator"/> 实例。</param>
    /// <param name="mouseSimulator">用于模拟鼠标输入的 <see cref="IMouseSimulator"/> 实例。</param>
    /// <param name="inputDeviceStateAdaptor"><see cref="IInputDeviceStateAdaptor"/>实例, 用于解释输入设备的状态。</param>
    public HKWAutoGUI(
        IKeyboardSimulator keyboardSimulator,
        IMouseSimulator mouseSimulator,
        IInputDeviceStateAdaptor inputDeviceStateAdaptor
    )
    {
        Keyboard = keyboardSimulator;
        Mouse = mouseSimulator;
        InputDeviceState = inputDeviceStateAdaptor;
    }

    /// <inheritdoc/>
    public HKWAutoGUI()
    {
        Keyboard = new KeyboardSimulator(this);
        Mouse = new MouseSimulator(this);
        InputDeviceState = new WindowsInputDeviceStateAdaptor();
    }
}
