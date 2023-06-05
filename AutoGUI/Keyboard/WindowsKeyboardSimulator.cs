using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using HKW.AutoGUI.AutoGUI;
using HKW.AutoGUI.InputDeviceState;
using HKW.AutoGUI.Native.Windows;

namespace HKW.AutoGUI.Keyboard;

/// <summary>
/// 键盘模拟
/// </summary>
[SupportedOSPlatform(nameof(OSPlatform.Windows))]
[DebuggerDisplay("DownedKeysCount = {DownedKeys.Count}")]
public class WindowsKeyboardSimulator : IKeyboardSimulator
{
    /// <inheritdoc/>
    public IReadOnlySet<VirtualKeyCode> DownedKeys => r_downedKeys;

    private readonly HashSet<VirtualKeyCode> r_downedKeys = new();

    private readonly IAutoGUI _iAutoGUI;

    /// <summary>
    /// 消息适配器
    /// </summary>
    private readonly IInputMessageDispatcher r_messageDispatcher;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="iAutoGUI">自动GUI接口</param>
    /// <exception cref="ArgumentNullException">若 <paramref name="iAutoGUI"/> 为 <see langword="null"/></exception>
    public WindowsKeyboardSimulator(IAutoGUI iAutoGUI)
    {
        _iAutoGUI = iAutoGUI ?? throw new ArgumentNullException(nameof(iAutoGUI));
        r_messageDispatcher = new WindowsInputMessageDispatcher();
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="iAutoGUI">自动GUI接口</param>
    /// <param name="messageDispatcher">消息适配器</param>
    /// <exception cref="ArgumentNullException">若 <paramref name="iAutoGUI"/> 或 <paramref name="messageDispatcher"/> 为 <see langword="null"/></exception>
    internal WindowsKeyboardSimulator(IAutoGUI iAutoGUI, IInputMessageDispatcher messageDispatcher)
    {
        _iAutoGUI = iAutoGUI ?? throw new ArgumentNullException(nameof(iAutoGUI));
        r_messageDispatcher =
            messageDispatcher
            ?? throw new ArgumentNullException(
                nameof(messageDispatcher),
                string.Format(
                    "The {0} cannot operate with a null {1}. Please provide a valid {1} instance to use for dispatching {2} messages.",
                    typeof(WindowsKeyboardSimulator).Name,
                    typeof(IInputMessageDispatcher).Name,
                    typeof(InputTypeMessage).Name
                )
            );
    }

    /// <summary>
    /// 添加多个按键按下
    /// </summary>
    /// <param name="builder">构造器</param>
    /// <param name="keyCodes">多个键码</param>

    private static void ModifiersDown(InputBuilder builder, IEnumerable<VirtualKeyCode> keyCodes)
    {
        if (keyCodes == null)
            return;
        foreach (var key in keyCodes)
            builder.AddKeyDown(key);
    }

    /// <summary>
    /// 添加多个按键释放
    /// </summary>
    /// <param name="builder">构造器</param>
    /// <param name="keyCodes">多个键码</param>
    private static void ModifiersUp(InputBuilder builder, IEnumerable<VirtualKeyCode> keyCodes)
    {
        if (keyCodes == null)
            return;
        foreach (var code in keyCodes.Reverse())
            builder.AddKeyUp(code);
    }

    /// <summary>
    /// 多个按键点击
    /// </summary>
    /// <param name="builder">构造器</param>
    /// <param name="keyCodes">多个键码</param>
    private static void KeysPress(InputBuilder builder, IEnumerable<VirtualKeyCode> keyCodes)
    {
        if (keyCodes == null)
            return;
        foreach (var key in keyCodes)
            builder.AddKeyPress(key);
    }

    #region IKeyboardSimulator
    /// <summary>
    /// 发送模拟输入
    /// </summary>
    /// <param name="input">输入构造器</param>
    private void SendSimulatedInput(InputBuilder input)
    {
        r_messageDispatcher.DispatchInput(_iAutoGUI.Pause, input);
    }

    /// <inheritdoc/>
    public IKeyboardSimulator KeyDown(VirtualKeyCode keyCode, bool record = true)
    {
        var inputList = new InputBuilder();
        inputList.AddKeyDown(keyCode);
        if (record)
            r_downedKeys.Add(keyCode);
        SendSimulatedInput(inputList);
        return this;
    }

    /// <inheritdoc/>
    public IKeyboardSimulator KeyDown(params VirtualKeyCode[] keyCodes)
    {
        return KeyDown(true, keyCodes);
    }

    /// <inheritdoc/>
    public IKeyboardSimulator KeyDown(bool record, params VirtualKeyCode[] keyCodes)
    {
        var builder = new InputBuilder();
        ModifiersDown(builder, keyCodes);
        if (record)
            r_downedKeys.UnionWith(keyCodes);
        SendSimulatedInput(builder);
        return this;
    }

    /// <inheritdoc/>
    public IKeyboardSimulator KeyUp()
    {
        if (r_downedKeys.Count > 0)
        {
            var builder = new InputBuilder();
            ModifiersUp(builder, r_downedKeys);
            r_downedKeys.Clear();
            SendSimulatedInput(builder);
        }
        return this;
    }

    /// <inheritdoc/>
    public IKeyboardSimulator KeyUp(VirtualKeyCode keyCode)
    {
        var inputList = new InputBuilder();
        inputList.AddKeyUp(keyCode);
        if (r_downedKeys.Count > 0)
            r_downedKeys.Remove(keyCode);
        SendSimulatedInput(inputList);
        return this;
    }

    /// <inheritdoc/>
    public IKeyboardSimulator KeyUp(params VirtualKeyCode[] keyCodes)
    {
        var builder = new InputBuilder();
        ModifiersUp(builder, keyCodes);
        if (r_downedKeys.Count > 0)
            r_downedKeys.ExceptWith(keyCodes);
        SendSimulatedInput(builder);
        return this;
    }

    /// <inheritdoc/>
    public IKeyboardSimulator KeyPress(VirtualKeyCode keyCode)
    {
        var inputList = new InputBuilder();
        inputList.AddKeyPress(keyCode);
        SendSimulatedInput(inputList);
        return this;
    }

    /// <inheritdoc/>
    public IKeyboardSimulator KeyPress(params VirtualKeyCode[] keyCodes)
    {
        var builder = new InputBuilder();
        KeysPress(builder, keyCodes);
        SendSimulatedInput(builder);
        return this;
    }

    /// <inheritdoc/>
    public IKeyboardSimulator TextEntry(string text)
    {
        if (text.Length > uint.MaxValue / 2)
            throw new ArgumentException(
                string.Format(
                    "The text parameter is too long. It must be less than {0} characters.",
                    uint.MaxValue / 2
                ),
                nameof(text)
            );
        var inputList = new InputBuilder();
        inputList.AddCharacters(text);
        SendSimulatedInput(inputList);
        return this;
    }

    /// <inheritdoc/>
    public IKeyboardSimulator TextEntry(char character)
    {
        var inputList = new InputBuilder();
        inputList.AddCharacter(character);
        SendSimulatedInput(inputList);
        return this;
    }
    #endregion
    #region IInputDelay
    /// <inheritdoc/>
    public IKeyboardSimulator Sleep(int millsecondsTimeout)
    {
        Thread.Sleep(millsecondsTimeout);
        return this;
    }

    /// <inheritdoc/>
    public IKeyboardSimulator Sleep(TimeSpan timeout)
    {
        Thread.Sleep(timeout);
        return this;
    }

    /// <inheritdoc/>
    public async Task<IKeyboardSimulator> Delay(int milliseconds)
    {
        await Task.Delay(milliseconds);
        return this;
    }

    /// <inheritdoc/>
    public async Task<IKeyboardSimulator> Delay(TimeSpan timeout)
    {
        await Task.Delay(timeout);
        return this;
    }
    #endregion
}
