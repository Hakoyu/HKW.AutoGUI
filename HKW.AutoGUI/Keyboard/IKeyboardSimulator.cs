namespace HKW.AutoGUI;

/// <summary>
/// 键盘模拟接口
/// </summary>
public interface IKeyboardSimulator<TKeyboardSimulator, TKeyCode>
    where TKeyboardSimulator : IKeyboardSimulator<TKeyboardSimulator, TKeyCode>
{
    /// <summary>
    /// 按下的按键
    /// </summary>
    public IReadOnlySet<TKeyCode> DownedKeys { get; }

    /// <summary>
    /// 按下按键
    /// </summary>
    /// <param name="keyCode">键码</param>
    /// <param name="record">若为 <see langword="true"/> 则 <paramref name="keyCode"/> 会被添加至 <see cref="DownedKeys"/> 中</param>
    public TKeyboardSimulator KeyDown(TKeyCode keyCode, bool record = true);

    /// <summary>
    /// 按下多个按键 (默认添加至 <see cref="DownedKeys"/> 中)
    /// </summary>
    /// <param name="keyCodes">多个键码</param>
    public TKeyboardSimulator KeyDown(params TKeyCode[] keyCodes);

    /// <summary>
    /// 按下多个按键
    /// </summary>
    /// <param name="record">记录按键, 按下的按键会被添加至 <see cref="DownedKeys"/> 中</param>
    /// <param name="keyCodes">多个键码</param>
    public TKeyboardSimulator KeyDown(bool record, params TKeyCode[] keyCodes);

    /// <summary>
    /// 释放在 <see cref="DownedKeys"/> 中被记录的按键, 并清空记录
    /// </summary>
    public TKeyboardSimulator KeyUp();

    /// <summary>
    /// 释放按键
    /// </summary>
    /// <param name="keyCode">键码 <para>会从 <see cref="DownedKeys"/> 中删除</para></param>
    public TKeyboardSimulator KeyUp(TKeyCode keyCode);

    /// <summary>
    /// 释放多个按键
    /// </summary>
    /// <param name="keyCodes">多个键码  <para>会从 <see cref="DownedKeys"/> 中删除</para></param>
    public TKeyboardSimulator KeyUp(params TKeyCode[] keyCodes);

    /// <summary>
    /// 按键点击
    /// </summary>
    /// <param name="keyCode">键码</param>
    public TKeyboardSimulator KeyPress(TKeyCode keyCode);

    /// <summary>
    /// 点击多个按键
    /// </summary>
    /// <param name="keyCodes">多个键码</param>
    public TKeyboardSimulator KeyPress(params TKeyCode[] keyCodes);

    /// <summary>
    /// 模拟输入文本
    /// </summary>
    /// <param name="text">文本</param>
    public TKeyboardSimulator TextEntry(ReadOnlySpan<char> text);

    /// <summary>
    /// 模拟输入字符
    /// </summary>
    /// <param name="character">字符</param>
    public TKeyboardSimulator TextEntry(char character);
}
