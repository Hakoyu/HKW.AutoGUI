using System.Collections;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.UI.Input.KeyboardAndMouse;

namespace HKW.AutoGUI.Windows;

/// <summary>
/// 输入信息构造器
/// </summary>
internal class InputBuilder : IList<INPUT>
{
    /// <summary>
    /// 原始列表
    /// </summary>
    private readonly List<INPUT> _inputList;

    #region IList
    /// <inheritdoc/>
    public int Count => ((ICollection<INPUT>)_inputList).Count;

    /// <inheritdoc/>
    public bool IsReadOnly => ((ICollection<INPUT>)_inputList).IsReadOnly;

    /// <inheritdoc/>
    INPUT IList<INPUT>.this[int index]
    {
        get => ((IList<INPUT>)_inputList)[index];
        set => ((IList<INPUT>)_inputList)[index] = value;
    }

    /// <inheritdoc/>
    public InputBuilder()
    {
        _inputList = [];
    }

    /// <inheritdoc/>
    public IEnumerator<INPUT> GetEnumerator()
    {
        return _inputList.GetEnumerator();
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc/>
    public int IndexOf(INPUT item)
    {
        return ((IList<INPUT>)_inputList).IndexOf(item);
    }

    /// <inheritdoc/>
    public void Insert(int index, INPUT item)
    {
        ((IList<INPUT>)_inputList).Insert(index, item);
    }

    /// <inheritdoc/>
    public void RemoveAt(int index)
    {
        ((IList<INPUT>)_inputList).RemoveAt(index);
    }

    /// <inheritdoc/>
    public void Add(INPUT item)
    {
        ((ICollection<INPUT>)_inputList).Add(item);
    }

    /// <inheritdoc/>
    public void Clear()
    {
        ((ICollection<INPUT>)_inputList).Clear();
    }

    /// <inheritdoc/>
    public bool Contains(INPUT item)
    {
        return ((ICollection<INPUT>)_inputList).Contains(item);
    }

    /// <inheritdoc/>
    public void CopyTo(INPUT[] array, int arrayIndex)
    {
        ((ICollection<INPUT>)_inputList).CopyTo(array, arrayIndex);
    }

    /// <inheritdoc/>
    public bool Remove(INPUT item)
    {
        return ((ICollection<INPUT>)_inputList).Remove(item);
    }
    #endregion
    #region Keyboard
    /// <summary>
    /// 确认是扩展键
    /// <para>详情查看: <a href="https://learn.microsoft.com/zh-cn/windows/win32/inputdev/about-keyboard-input#extended-key-flag">MSDN</a></para>
    /// </summary>
    /// <param name="keyCode">键码</param>
    /// <returns>是扩展键为 <see langword="true"/> 否则为 <see langword="false"/></returns>
    public static bool IsExtendedKey(VIRTUAL_KEY keyCode)
    {
        if (
            keyCode == VIRTUAL_KEY.VK_MENU
            || keyCode == VIRTUAL_KEY.VK_RMENU
            || keyCode == VIRTUAL_KEY.VK_CONTROL
            || keyCode == VIRTUAL_KEY.VK_RCONTROL
            || keyCode == VIRTUAL_KEY.VK_INSERT
            || keyCode == VIRTUAL_KEY.VK_DELETE
            || keyCode == VIRTUAL_KEY.VK_HOME
            || keyCode == VIRTUAL_KEY.VK_END
            || keyCode == VIRTUAL_KEY.VK_PRIOR
            || keyCode == VIRTUAL_KEY.VK_NEXT
            || keyCode == VIRTUAL_KEY.VK_RIGHT
            || keyCode == VIRTUAL_KEY.VK_UP
            || keyCode == VIRTUAL_KEY.VK_LEFT
            || keyCode == VIRTUAL_KEY.VK_DOWN
            || keyCode == VIRTUAL_KEY.VK_NUMLOCK
            || keyCode == VIRTUAL_KEY.VK_CANCEL
            || keyCode == VIRTUAL_KEY.VK_SNAPSHOT
            || keyCode == VIRTUAL_KEY.VK_DIVIDE
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
    public void AddKeyDown(VIRTUAL_KEY keyCode)
    {
        var down = new INPUT
        {
            type = INPUT_TYPE.INPUT_KEYBOARD,
            Anonymous =
            {
                ki =
                {
                    wVk = keyCode,
                    wScan = (ushort)(PInvoke.MapVirtualKey((uint)keyCode, 0) & 0xFFU),
                    dwFlags = IsExtendedKey(keyCode) ? KEYBD_EVENT_FLAGS.KEYEVENTF_EXTENDEDKEY : 0,
                    dwExtraInfo = (UIntPtr)PInvoke.GetMessageExtraInfo().Value
                }
            }
        };

        _inputList.Add(down);
    }

    /// <summary>
    /// 添加按键释放
    /// </summary>
    /// <param name="keyCode">键码</param>
    public void AddKeyUp(VIRTUAL_KEY keyCode)
    {
        var up = new INPUT
        {
            type = INPUT_TYPE.INPUT_KEYBOARD,
            Anonymous =
            {
                ki =
                {
                    wVk = keyCode,
                    wScan = (ushort)(PInvoke.MapVirtualKey((uint)keyCode, 0) & 0xFFU),
                    dwFlags = IsExtendedKey(keyCode)
                        ? KEYBD_EVENT_FLAGS.KEYEVENTF_KEYUP
                            | KEYBD_EVENT_FLAGS.KEYEVENTF_EXTENDEDKEY
                        : KEYBD_EVENT_FLAGS.KEYEVENTF_KEYUP,
                }
            }
        };

        _inputList.Add(up);
    }

    /// <summary>
    /// 添加按键按压
    /// </summary>
    /// <param name="keyCode">键码</param>
    public void AddKeyPress(VIRTUAL_KEY keyCode)
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
        var down = new INPUT
        {
            type = INPUT_TYPE.INPUT_KEYBOARD,
            Anonymous =
            {
                ki =
                {
                    wVk = 0,
                    wScan = character,
                    dwFlags = KEYBD_EVENT_FLAGS.KEYEVENTF_UNICODE,
                }
            }
        };

        var up = new INPUT
        {
            type = INPUT_TYPE.INPUT_KEYBOARD,
            Anonymous =
            {
                ki =
                {
                    wVk = 0,
                    wScan = character,
                    dwFlags =
                        KEYBD_EVENT_FLAGS.KEYEVENTF_KEYUP | KEYBD_EVENT_FLAGS.KEYEVENTF_UNICODE,
                }
            }
        };

        // 处理扩展键：
        // 如果扫描代码前面有一个值为 0xE0（224）的前缀字节、
        // 则需要在 Flags 属性中加入 KEYEVENTF_EXTENDEDKEY 标志。
        if ((character & 0xFF00) == 0xE000)
        {
            down.Anonymous.ki.dwFlags |= KEYBD_EVENT_FLAGS.KEYEVENTF_EXTENDEDKEY;
            up.Anonymous.ki.dwFlags |= KEYBD_EVENT_FLAGS.KEYEVENTF_EXTENDEDKEY;
        }

        _inputList.Add(down);
        _inputList.Add(up);
    }

    /// <summary>
    /// 添加字符串
    /// </summary>
    /// <param name="characters">字符串消息</param>
    public void AddCharacters(ReadOnlySpan<char> characters)
    {
        foreach (var c in characters)
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
        var movement = new INPUT
        {
            type = INPUT_TYPE.INPUT_MOUSE,
            Anonymous =
            {
                mi =
                {
                    dwFlags = MOUSE_EVENT_FLAGS.MOUSEEVENTF_MOVE,
                    dx = xMovePixelSize,
                    dy = yMovePixelSize
                }
            }
        };

        _inputList.Add(movement);
    }

    /// <summary>
    /// 添加鼠标绝对值移动
    /// <para>左上 <see langword="(0,0)"/> 右下 <see langword="(65535,65535)"/></para>
    /// </summary>
    /// <param name="absoluteX">范围: <see langword="0"/> ~ <see langword="65535"/></param>
    /// <param name="absoluteY">范围: <see langword="0"/> ~ <see langword="65535"/></param>
    public void AddAbsoluteMouseMovement(int absoluteX, int absoluteY)
    {
        var movement = new INPUT
        {
            type = INPUT_TYPE.INPUT_MOUSE,
            Anonymous =
            {
                mi =
                {
                    dwFlags =
                        MOUSE_EVENT_FLAGS.MOUSEEVENTF_MOVE | MOUSE_EVENT_FLAGS.MOUSEEVENTF_ABSOLUTE,
                    dx = absoluteX,
                    dy = absoluteY
                }
            }
        };

        _inputList.Add(movement);
    }

    /// <summary>
    /// 添加鼠标在虚拟桌面的绝对值移动
    /// <para>左上 <see langword="(0,0)"/> 右下 <see langword="(65535,65535)"/></para>
    /// </summary>
    /// <param name="absoluteX">范围: <see langword="0"/> ~ <see langword="65535"/></param>
    /// <param name="absoluteY">范围: <see langword="0"/> ~ <see langword="65535"/></param>
    public void AddAbsoluteMouseMovementOnVirtualDesktop(int absoluteX, int absoluteY)
    {
        var movement = new INPUT
        {
            type = INPUT_TYPE.INPUT_MOUSE,
            Anonymous =
            {
                mi =
                {
                    dwFlags =
                        MOUSE_EVENT_FLAGS.MOUSEEVENTF_MOVE
                        | MOUSE_EVENT_FLAGS.MOUSEEVENTF_ABSOLUTE
                        | MOUSE_EVENT_FLAGS.MOUSEEVENTF_VIRTUALDESK,
                    dx = absoluteX,
                    dy = absoluteY
                }
            }
        };

        _inputList.Add(movement);
    }

    /// <summary>
    /// 添加鼠标按键按下
    /// </summary>
    /// <param name="button">按键</param>
    public void AddMouseButtonDown(MouseButton button)
    {
        var dowm = new INPUT
        {
            type = INPUT_TYPE.INPUT_MOUSE,
            Anonymous = { mi = { dwFlags = ToMouseButtonDownFlag(button) } }
        };
        _inputList.Add(dowm);
    }

    /// <summary>
    /// 添加鼠标X按键按下
    /// </summary>
    /// <param name="xButton">X按键</param>
    public void AddMouseXButtonDown(XButton xButton)
    {
        var down = new INPUT
        {
            type = INPUT_TYPE.INPUT_MOUSE,
            Anonymous =
            {
                mi = { dwFlags = MOUSE_EVENT_FLAGS.MOUSEEVENTF_XDOWN, mouseData = (uint)xButton }
            }
        };
        _inputList.Add(down);
    }

    /// <summary>
    /// 添加鼠标按键释放
    /// </summary>
    /// <param name="button">按键</param>
    public void AddMouseButtonUp(MouseButton button)
    {
        var up = new INPUT
        {
            type = INPUT_TYPE.INPUT_MOUSE,
            Anonymous = { mi = { dwFlags = ToMouseButtonUpFlag(button) } }
        };
        _inputList.Add(up);
    }

    /// <summary>
    /// 添加鼠标X按键释放
    /// </summary>
    /// <param name="xButton">X按键</param>
    public void AddMouseXButtonUp(XButton xButton)
    {
        var up = new INPUT
        {
            type = INPUT_TYPE.INPUT_MOUSE,
            Anonymous =
            {
                mi = { dwFlags = MOUSE_EVENT_FLAGS.MOUSEEVENTF_XUP, mouseData = (uint)xButton }
            }
        };
        _inputList.Add(up);
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
        var scroll = new INPUT
        {
            type = INPUT_TYPE.INPUT_MOUSE,
            Anonymous =
            {
                mi =
                {
                    dwFlags = MOUSE_EVENT_FLAGS.MOUSEEVENTF_WHEEL,
                    mouseData = (uint)scrollAmount
                }
            }
        };
        _inputList.Add(scroll);
    }

    /// <summary>
    /// 添加鼠标滚轮水平滚动
    /// </summary>
    /// <param name="scrollAmount">滚动量</param>
    public void AddMouseHorizontalWheelScroll(int scrollAmount)
    {
        var scroll = new INPUT
        {
            type = INPUT_TYPE.INPUT_MOUSE,
            Anonymous =
            {
                mi =
                {
                    dwFlags = MOUSE_EVENT_FLAGS.MOUSEEVENTF_HWHEEL,
                    mouseData = (uint)scrollAmount
                }
            }
        };

        _inputList.Add(scroll);
    }

    public Span<INPUT> AsSpan()
    {
        return CollectionsMarshal.AsSpan(_inputList);
    }

    private static MOUSE_EVENT_FLAGS ToMouseButtonDownFlag(MouseButton button)
    {
        return button switch
        {
            MouseButton.Left => MOUSE_EVENT_FLAGS.MOUSEEVENTF_LEFTDOWN,
            MouseButton.Middle => MOUSE_EVENT_FLAGS.MOUSEEVENTF_MIDDLEDOWN,
            MouseButton.Right => MOUSE_EVENT_FLAGS.MOUSEEVENTF_RIGHTDOWN,
            _ => MOUSE_EVENT_FLAGS.MOUSEEVENTF_LEFTDOWN,
        };
    }

    private static MOUSE_EVENT_FLAGS ToMouseButtonUpFlag(MouseButton button)
    {
        return button switch
        {
            MouseButton.Left => MOUSE_EVENT_FLAGS.MOUSEEVENTF_LEFTUP,
            MouseButton.Middle => MOUSE_EVENT_FLAGS.MOUSEEVENTF_MIDDLEUP,
            MouseButton.Right => MOUSE_EVENT_FLAGS.MOUSEEVENTF_RIGHTUP,
            _ => MOUSE_EVENT_FLAGS.MOUSEEVENTF_LEFTUP,
        };
    }
    #endregion
}
