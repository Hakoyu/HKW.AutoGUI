using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Windows.Win32;
using Windows.Win32.UI.Input.KeyboardAndMouse;

namespace HKW.AutoGUI.Windows;

/// <summary>
/// 键盘模拟
/// </summary>
[SupportedOSPlatform(nameof(OSPlatform.Windows))]
[DebuggerDisplay("DownedKeysCount = {DownedKeys.Count}")]
public class WindowsKeyboardSimulator : IKeyboardSimulator<WindowsKeyboardSimulator, VIRTUAL_KEY>
{
    /// <inheritdoc/>
    public IReadOnlySet<VIRTUAL_KEY> DownedKeys => _downedKeys;

    private readonly HashSet<VIRTUAL_KEY> _downedKeys = [];

    /// <summary>
    /// 消息适配器
    /// </summary>
    public IInputMessageDispatcher MessageDispatcher { get; set; }

    /// <summary>
    /// 构造
    /// </summary>
    public WindowsKeyboardSimulator()
    {
        MessageDispatcher = new InputMessageDispatcher();
    }

    #region IKeyboardSimulator
    /// <summary>
    /// 发送模拟输入
    /// </summary>
    /// <param name="input">输入构造器</param>
    private void SendSimulatedInput(InputBuilder input)
    {
        MessageDispatcher.DispatchInput(input);
    }

    /// <inheritdoc/>
    public WindowsKeyboardSimulator KeyDown(VIRTUAL_KEY keyCode, bool record = true)
    {
        var inputList = new InputBuilder();
        inputList.AddKeyDown(keyCode);
        if (record)
            _downedKeys.Add(keyCode);
        SendSimulatedInput(inputList);
        return this;
    }

    /// <inheritdoc/>
    public WindowsKeyboardSimulator KeyDown(params VIRTUAL_KEY[] keyCodes)
    {
        return KeyDown(true, keyCodes);
    }

    /// <inheritdoc/>
    public WindowsKeyboardSimulator KeyDown(bool record, params VIRTUAL_KEY[] keyCodes)
    {
        var builder = new InputBuilder();
        foreach (var key in keyCodes)
            builder.AddKeyDown(key);
        if (record)
            _downedKeys.UnionWith(keyCodes);
        SendSimulatedInput(builder);
        return this;
    }

    /// <inheritdoc/>
    public WindowsKeyboardSimulator KeyUp()
    {
        if (_downedKeys.Count > 0)
        {
            var builder = new InputBuilder();
            foreach (var code in DownedKeys.Reverse())
                builder.AddKeyUp(code);
            _downedKeys.Clear();
            SendSimulatedInput(builder);
        }
        return this;
    }

    /// <inheritdoc/>
    public WindowsKeyboardSimulator KeyUp(VIRTUAL_KEY keyCode)
    {
        var inputList = new InputBuilder();
        inputList.AddKeyUp(keyCode);
        if (_downedKeys.Count > 0)
            _downedKeys.Remove(keyCode);
        SendSimulatedInput(inputList);
        return this;
    }

    /// <inheritdoc/>
    public WindowsKeyboardSimulator KeyUp(params VIRTUAL_KEY[] keyCodes)
    {
        var builder = new InputBuilder();
        foreach (var code in keyCodes.Reverse())
            builder.AddKeyUp(code);
        if (_downedKeys.Count > 0)
            _downedKeys.ExceptWith(keyCodes);
        SendSimulatedInput(builder);
        return this;
    }

    /// <inheritdoc/>
    public WindowsKeyboardSimulator KeyPress(VIRTUAL_KEY keyCode)
    {
        var inputList = new InputBuilder();
        inputList.AddKeyPress(keyCode);
        SendSimulatedInput(inputList);
        return this;
    }

    /// <inheritdoc/>
    public WindowsKeyboardSimulator KeyPress(params VIRTUAL_KEY[] keyCodes)
    {
        var builder = new InputBuilder();
        foreach (var key in keyCodes)
            builder.AddKeyPress(key);
        SendSimulatedInput(builder);
        return this;
    }

    /// <inheritdoc/>
    public WindowsKeyboardSimulator TextEntry(ReadOnlySpan<char> text)
    {
        if (text.Length > int.MaxValue)
            throw new ArgumentException(
                string.Format(
                    "The text parameter is too long. It must be less than {0} characters.",
                    int.MaxValue
                ),
                nameof(text)
            );
        var inputList = new InputBuilder();
        inputList.AddCharacters(text);
        SendSimulatedInput(inputList);
        return this;
    }

    /// <inheritdoc/>
    public WindowsKeyboardSimulator TextEntry(char character)
    {
        var inputList = new InputBuilder();
        inputList.AddCharacter(character);
        SendSimulatedInput(inputList);
        return this;
    }
    #endregion
}
