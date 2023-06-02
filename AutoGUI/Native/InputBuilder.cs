using System;
using System.Collections;
using System.Collections.Generic;
using HKW.AutoGUI.Mouse;

namespace HKW.AutoGUI.Native;

/// <summary>
/// 输入信息构造器
/// </summary>
internal class InputBuilder : IList<InputTypeMessage>
{
    /// <summary>
    /// 原始列表
    /// </summary>
    private readonly List<InputTypeMessage> r_inputList;

    #region IList
    /// <inheritdoc/>
    public int Count => ((ICollection<InputTypeMessage>)r_inputList).Count;

    /// <inheritdoc/>
    public bool IsReadOnly => ((ICollection<InputTypeMessage>)r_inputList).IsReadOnly;

    /// <inheritdoc/>
    InputTypeMessage IList<InputTypeMessage>.this[int index]
    {
        get => ((IList<InputTypeMessage>)r_inputList)[index];
        set => ((IList<InputTypeMessage>)r_inputList)[index] = value;
    }

    /// <inheritdoc/>
    public InputBuilder()
    {
        r_inputList = new List<InputTypeMessage>();
    }

    /// <inheritdoc/>
    public IEnumerator<InputTypeMessage> GetEnumerator()
    {
        return r_inputList.GetEnumerator();
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc/>
    public int IndexOf(InputTypeMessage item)
    {
        return ((IList<InputTypeMessage>)r_inputList).IndexOf(item);
    }

    /// <inheritdoc/>
    public void Insert(int index, InputTypeMessage item)
    {
        ((IList<InputTypeMessage>)r_inputList).Insert(index, item);
    }

    /// <inheritdoc/>
    public void RemoveAt(int index)
    {
        ((IList<InputTypeMessage>)r_inputList).RemoveAt(index);
    }

    /// <inheritdoc/>
    public void Add(InputTypeMessage item)
    {
        ((ICollection<InputTypeMessage>)r_inputList).Add(item);
    }

    /// <inheritdoc/>
    public void Clear()
    {
        ((ICollection<InputTypeMessage>)r_inputList).Clear();
    }

    /// <inheritdoc/>
    public bool Contains(InputTypeMessage item)
    {
        return ((ICollection<InputTypeMessage>)r_inputList).Contains(item);
    }

    /// <inheritdoc/>
    public void CopyTo(InputTypeMessage[] array, int arrayIndex)
    {
        ((ICollection<InputTypeMessage>)r_inputList).CopyTo(array, arrayIndex);
    }

    /// <inheritdoc/>
    public bool Remove(InputTypeMessage item)
    {
        return ((ICollection<InputTypeMessage>)r_inputList).Remove(item);
    }
    #endregion
    #region Keyboard
    /// <summary>
    /// 确认是扩展键
    /// <para>详情查看: <a href="https://learn.microsoft.com/zh-cn/windows/win32/inputdev/about-keyboard-input#extended-key-flag">MSDN</a></para>
    /// </summary>
    /// <param name="keyCode">键码</param>
    /// <returns>是扩展键为 <see langword="true"/> 否则为 <see langword="false"/></returns>
    public static bool IsExtendedKey(VirtualKeyCode keyCode)
    {
        if (
            keyCode == VirtualKeyCode.MENU
            || keyCode == VirtualKeyCode.RMENU
            || keyCode == VirtualKeyCode.CONTROL
            || keyCode == VirtualKeyCode.RCONTROL
            || keyCode == VirtualKeyCode.INSERT
            || keyCode == VirtualKeyCode.DELETE
            || keyCode == VirtualKeyCode.HOME
            || keyCode == VirtualKeyCode.END
            || keyCode == VirtualKeyCode.PRIOR
            || keyCode == VirtualKeyCode.NEXT
            || keyCode == VirtualKeyCode.RIGHT
            || keyCode == VirtualKeyCode.UP
            || keyCode == VirtualKeyCode.LEFT
            || keyCode == VirtualKeyCode.DOWN
            || keyCode == VirtualKeyCode.NUMLOCK
            || keyCode == VirtualKeyCode.CANCEL
            || keyCode == VirtualKeyCode.SNAPSHOT
            || keyCode == VirtualKeyCode.DIVIDE
        )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 添加按键按下
    /// </summary>
    /// <param name="keyCode">键码</param>
    public void AddKeyDown(VirtualKeyCode keyCode)
    {
        var down = new InputTypeMessage
        {
            Type = InputType.Keyboard,
            Data =
            {
                Keyboard = new KeybdInput
                {
                    KeyCode = (ushort)keyCode,
                    Scan = (ushort)(NativeMethods.MapVirtualKey((uint)keyCode, 0) & 0xFFU),
                    Flags = IsExtendedKey(keyCode) ? KeyboardFlag.ExtendedKey : 0,
                    Time = 0,
                    ExtraInfo = IntPtr.Zero
                }
            }
        };

        r_inputList.Add(down);
    }

    /// <summary>
    /// 添加按键释放
    /// </summary>
    /// <param name="keyCode">键码</param>
    public void AddKeyUp(VirtualKeyCode keyCode)
    {
        var up = new InputTypeMessage
        {
            Type = InputType.Keyboard,
            Data =
            {
                Keyboard = new KeybdInput
                {
                    KeyCode = (ushort)keyCode,
                    Scan = (ushort)(NativeMethods.MapVirtualKey((uint)keyCode, 0) & 0xFFU),
                    Flags = IsExtendedKey(keyCode)
                        ? KeyboardFlag.KeyUp | KeyboardFlag.ExtendedKey
                        : KeyboardFlag.KeyUp,
                    Time = 0,
                    ExtraInfo = IntPtr.Zero
                }
            }
        };

        r_inputList.Add(up);
    }

    /// <summary>
    /// 添加按键按压
    /// </summary>
    /// <param name="keyCode">键码</param>
    public void AddKeyPress(VirtualKeyCode keyCode)
    {
        AddKeyDown(keyCode);
        AddKeyUp(keyCode);
    }

    /// <summary>
    /// 添加字符
    /// </summary>
    /// <param name="character">字符消息</param>
    public void AddCharacter(char character)
    {
        ushort scanCode = character;

        var down = new InputTypeMessage
        {
            Type = InputType.Keyboard,
            Data =
            {
                Keyboard = new KeybdInput
                {
                    KeyCode = 0,
                    Scan = scanCode,
                    Flags = KeyboardFlag.Unicode,
                    Time = 0,
                    ExtraInfo = IntPtr.Zero
                }
            }
        };

        var up = new InputTypeMessage
        {
            Type = InputType.Keyboard,
            Data =
            {
                Keyboard = new KeybdInput
                {
                    KeyCode = 0,
                    Scan = scanCode,
                    Flags = KeyboardFlag.KeyUp | KeyboardFlag.Unicode,
                    Time = 0,
                    ExtraInfo = IntPtr.Zero
                }
            }
        };

        // 处理扩展键：
        // 如果扫描代码前面有一个值为 0xE0（224）的前缀字节、
        // 则需要在 Flags 属性中加入 KEYEVENTF_EXTENDEDKEY 标志。
        if ((scanCode & 0xFF00) == 0xE000)
        {
            down.Data.Keyboard.Flags |= KeyboardFlag.ExtendedKey;
            up.Data.Keyboard.Flags |= KeyboardFlag.ExtendedKey;
        }

        r_inputList.Add(down);
        r_inputList.Add(up);
    }

    /// <summary>
    /// 添加字符串
    /// </summary>
    /// <param name="characters">字符串消息</param>
    public void AddCharacters(string characters)
    {
        foreach (var c in characters.AsSpan())
            AddCharacter(c);
    }
    #endregion
    #region Mouse
    /// <summary>
    /// 添加鼠标相对移动 (单位为像素)
    /// </summary>
    /// <param name="xMovePixelSize">X轴移动的像素值</param>
    /// <param name="yMovePixelSize">Y轴移动的像素值</param>
    public void AddRelativeMouseMovement(int xMovePixelSize, int yMovePixelSize)
    {
        var movement = new InputTypeMessage { Type = InputType.Mouse };
        movement.Data.Mouse.Flags = MouseFlag.Move;
        movement.Data.Mouse.X = xMovePixelSize;
        movement.Data.Mouse.Y = yMovePixelSize;

        r_inputList.Add(movement);
    }

    /// <summary>
    /// 添加鼠标绝对值移动
    /// <para>左上 <see langword="(0,0)"/> 右下 <see langword="(65535,65535)"/></para>
    /// </summary>
    /// <param name="absoluteX">范围: <see langword="0"/> ~ <see langword="65535"/></param>
    /// <param name="absoluteY">范围: <see langword="0"/> ~ <see langword="65535"/></param>
    public void AddAbsoluteMouseMovement(int absoluteX, int absoluteY)
    {
        var movement = new InputTypeMessage { Type = InputType.Mouse };
        movement.Data.Mouse.Flags = MouseFlag.Move | MouseFlag.Absolute;
        movement.Data.Mouse.X = absoluteX;
        movement.Data.Mouse.Y = absoluteY;

        r_inputList.Add(movement);
    }

    /// <summary>
    /// 添加鼠标在虚拟桌面的绝对值移动
    /// <para>左上 <see langword="(0,0)"/> 右下 <see langword="(65535,65535)"/></para>
    /// </summary>
    /// <param name="absoluteX">范围: <see langword="0"/> ~ <see langword="65535"/></param>
    /// <param name="absoluteY">范围: <see langword="0"/> ~ <see langword="65535"/></param>
    public void AddAbsoluteMouseMovementOnVirtualDesktop(int absoluteX, int absoluteY)
    {
        var movement = new InputTypeMessage { Type = InputType.Mouse };
        movement.Data.Mouse.Flags = MouseFlag.Move | MouseFlag.Absolute | MouseFlag.VirtualDesk;
        movement.Data.Mouse.X = absoluteX;
        movement.Data.Mouse.Y = absoluteY;

        r_inputList.Add(movement);
    }

    /// <summary>
    /// 添加鼠标按键按下
    /// </summary>
    /// <param name="button">按键</param>
    public void AddMouseButtonDown(MouseButton button)
    {
        var buttonDown = new InputTypeMessage { Type = InputType.Mouse };
        buttonDown.Data.Mouse.Flags = ToMouseButtonDownFlag(button);

        r_inputList.Add(buttonDown);
    }

    /// <summary>
    /// 添加鼠标X按键按下
    /// </summary>
    /// <param name="xButton">X按键</param>
    public void AddMouseXButtonDown(XButton xButton)
    {
        var buttonDown = new InputTypeMessage { Type = (uint)InputType.Mouse };
        buttonDown.Data.Mouse.Flags = MouseFlag.XDown;
        buttonDown.Data.Mouse.MouseData = (uint)xButton;
        r_inputList.Add(buttonDown);
    }

    /// <summary>
    /// 添加鼠标按键释放
    /// </summary>
    /// <param name="button">按键</param>
    public void AddMouseButtonUp(MouseButton button)
    {
        var buttonUp = new InputTypeMessage { Type = (uint)InputType.Mouse };
        buttonUp.Data.Mouse.Flags = ToMouseButtonUpFlag(button);
        r_inputList.Add(buttonUp);
    }

    /// <summary>
    /// 添加鼠标X按键释放
    /// </summary>
    /// <param name="xButton">X按键</param>
    public void AddMouseXButtonUp(XButton xButton)
    {
        var buttonUp = new InputTypeMessage { Type = (uint)InputType.Mouse };
        buttonUp.Data.Mouse.Flags = MouseFlag.XUp;
        buttonUp.Data.Mouse.MouseData = (uint)xButton;
        r_inputList.Add(buttonUp);
    }

    /// <summary>
    /// 添加鼠标按键点击
    /// </summary>
    /// <param name="button">按键</param>
    public void AddMouseButtonClick(MouseButton button)
    {
        AddMouseButtonDown(button);
        AddMouseButtonUp(button);
    }

    /// <summary>
    /// 添加鼠标X按键点击
    /// </summary>
    /// <param name="xButton">X按键</param>
    public void AddMouseXButtonClick(XButton xButton)
    {
        AddMouseXButtonDown(xButton);
        AddMouseXButtonUp(xButton);
    }

    /// <summary>
    /// 添加鼠标按键双击
    /// </summary>
    /// <param name="button">按键</param>
    public void AddMouseButtonDoubleClick(MouseButton button)
    {
        AddMouseButtonClick(button);
        AddMouseButtonClick(button);
    }

    /// <summary>
    /// 添加鼠标X按键双击
    /// </summary>
    /// <param name="xButton">X按键</param>
    public void AddMouseXButtonDoubleClick(XButton xButton)
    {
        AddMouseXButtonClick(xButton);
        AddMouseXButtonClick(xButton);
    }

    /// <summary>
    /// 添加鼠标滚轮垂直滚动
    /// </summary>
    /// <param name="scrollAmount">滚动量</param>
    public void AddMouseVerticalWheelScroll(int scrollAmount)
    {
        var scroll = new InputTypeMessage { Type = (uint)InputType.Mouse };
        scroll.Data.Mouse.Flags = MouseFlag.VerticalWheel;
        scroll.Data.Mouse.MouseData = (uint)scrollAmount;

        r_inputList.Add(scroll);
    }

    /// <summary>
    /// 添加鼠标滚轮水平滚动
    /// </summary>
    /// <param name="scrollAmount">滚动量</param>
    public void AddMouseHorizontalWheelScroll(int scrollAmount)
    {
        var scroll = new InputTypeMessage { Type = (uint)InputType.Mouse };
        scroll.Data.Mouse.Flags = MouseFlag.HorizontalWheel;
        scroll.Data.Mouse.MouseData = (uint)scrollAmount;

        r_inputList.Add(scroll);
    }

    private static MouseFlag ToMouseButtonDownFlag(MouseButton button)
    {
        return button switch
        {
            MouseButton.Left => MouseFlag.LeftDown,
            MouseButton.Middle => MouseFlag.MiddleDown,
            MouseButton.Right => MouseFlag.RightDown,
            _ => MouseFlag.LeftDown,
        };
    }

    private static MouseFlag ToMouseButtonUpFlag(MouseButton button)
    {
        return button switch
        {
            MouseButton.Left => MouseFlag.LeftUp,
            MouseButton.Middle => MouseFlag.MiddleUp,
            MouseButton.Right => MouseFlag.RightUp,
            _ => MouseFlag.LeftUp,
        };
    }
    #endregion
}
