using HKW.AutoGUI.Native;

namespace HKW.AutoGUI;

/// <summary>
/// GUI自动化模块接口
/// </summary>
public interface IAutoGUI
{
    /// <summary>
    /// 动作间停顿 单位为毫秒 默认为 <see langword="0"/>
    /// </summary>
    public int Pause { get; set; }

    /// <summary>
    /// 用于模拟键盘
    /// </summary>
    public IKeyboardSimulator Keyboard { get; }

    /// <summary>
    /// 用于模拟鼠标
    /// </summary>
    public IMouseSimulator Mouse { get; }

    /// <summary>
    /// 用于获取输入设备状态
    /// </summary>
    public IInputDeviceStateAdaptor InputDeviceState { get; }
}
