namespace HKW.AutoGUI;

/// <summary>
/// 自动化GUI接口
/// </summary>
public interface IAutoGUI<TMouseSimulator, TKeyboardSimulator, TScreenUtils, TKeyCode>
    where TMouseSimulator : IMouseSimulator<TMouseSimulator>
    where TKeyboardSimulator : IKeyboardSimulator<TKeyboardSimulator, TKeyCode>
    where TScreenUtils : IScreenUtils
{
    /// <summary>
    /// 用于模拟鼠标
    /// </summary>
    public TMouseSimulator Mouse { get; }

    /// <summary>
    /// 用于模拟键盘
    /// </summary>
    public TKeyboardSimulator Keyboard { get; }

    /// <summary>
    /// 用于获取输入设备状态
    /// </summary>
    public IInputDeviceStateAdaptor<TKeyCode> InputDeviceState { get; }

    /// <summary>
    /// 屏幕工具
    /// </summary>
    public TScreenUtils ScreenUtils { get; }
}
