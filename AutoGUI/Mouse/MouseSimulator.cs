using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using HKW.AutoGUI.Mouse;
using HKW.AutoGUI.Native;

namespace HKW.AutoGUI;

/// <summary>
/// 鼠标模拟
/// </summary>
public class MouseSimulator : IMouseSimulator
{
    /// <inheritdoc/>
    public MousePoint Position => GetMousePosition();

    /// <inheritdoc/>
    public int MouseWheelClickSize { get; set; } = 120;

    private double _abstractXRatio;
    private double _abstractYRatio;

    private readonly IAutoGUI r_iAutoGUI;

    /// <inheritdoc/>
    public IKeyboardSimulator Keyboard => r_iAutoGUI.Keyboard;

    /// <summary>
    /// 消息分配器
    /// </summary>
    private readonly IInputMessageDispatcher r_messageDispatcher;

    /// <summary>
    /// 构造鼠标模拟器, 使用默认消息分配器
    /// </summary>
    /// <param name="iAutoGUI">自动GUI接口</param>
    /// <exception cref="ArgumentNullException">若 <paramref name="iAutoGUI"/> 为 <see langword="null"/></exception>
    public MouseSimulator(IAutoGUI iAutoGUI)
    {
        r_iAutoGUI = iAutoGUI ?? throw new ArgumentNullException(nameof(iAutoGUI));
        r_messageDispatcher = new WindowsInputMessageDispatcher();
        InitializeAbstractRatio();
    }

    /// <summary>
    /// 构造鼠标模拟器, 使用自定义消息分配器
    /// </summary>
    /// <param name="iAutoGUI">自动GUI接口</param>
    /// <param name="messageDispatcher">消息分配器</param>
    /// <exception cref="ArgumentNullException">若 <paramref name="iAutoGUI"/> 或 <paramref name="messageDispatcher"/> 为 <see langword="null"/></exception>
    internal MouseSimulator(IAutoGUI iAutoGUI, IInputMessageDispatcher messageDispatcher)
    {
        r_iAutoGUI = iAutoGUI ?? throw new ArgumentNullException(nameof(iAutoGUI));
        r_messageDispatcher =
            messageDispatcher
            ?? throw new ArgumentNullException(
                nameof(messageDispatcher),
                string.Format(
                    "The {0} cannot operate with a null {1}. Please provide a valid {1} instance to use for dispatching {2} messages.",
                    typeof(MouseSimulator).Name,
                    typeof(IInputMessageDispatcher).Name,
                    typeof(InputTypeMessage).Name
                )
            );
        InitializeAbstractRatio();
    }

    /// <summary>
    /// 初始化绝对值比例
    /// </summary>
    private void InitializeAbstractRatio()
    {
        _abstractXRatio = 65535.0 / SystemMetrics.ScreenWidth;
        _abstractYRatio = 65535.0 / SystemMetrics.ScreenHeight;
    }

    /// <summary>
    /// 发送模拟输入
    /// </summary>
    /// <param name="input">输入构造器</param>
    private void SendSimulatedInput(InputBuilder input)
    {
        r_messageDispatcher.DispatchInput(r_iAutoGUI.Pause, input);
    }

    private void IMoveTo(int pixelX, int pixelY)
    {
        var inputList = new InputBuilder();
        inputList.AddAbsoluteMouseMovement(
            (int)Math.Truncate((pixelX + 1) * _abstractXRatio),
            (int)Math.Truncate((pixelY + 1) * _abstractYRatio)
        );
        SendSimulatedInput(inputList);
    }

    private void IAbsoluteMoveTo(int absoluteX, int absoluteY)
    {
        var inputList = new InputBuilder();
        inputList.AddAbsoluteMouseMovement(absoluteX, absoluteY);
        SendSimulatedInput(inputList);
    }

    private void IAddAbsoluteMouseMovementOnVirtualDesktop(int absoluteX, int absoluteY)
    {
        var inputList = new InputBuilder();
        inputList.AddAbsoluteMouseMovementOnVirtualDesktop(absoluteX, absoluteY);
        SendSimulatedInput(inputList);
    }

    /// <inheritdoc/>
    public IMouseSimulator MoveBy(int pixelDeltaX, int pixelDeltaY, int duration = 0)
    {
        var position = Position;
        if (duration > 1)
        {
            // 循环结束后会再移动一次 所以少一次
            duration -= 1;
            // 计算每毫秒的移动比例
            var x = (double)position.X;
            var y = (double)position.Y;
            var incrementX = (double)pixelDeltaX / duration;
            var incrementY = (double)pixelDeltaY / duration;
            // 计时, 以保证总时长准确性
            Stopwatch stopWatch = new();
            stopWatch.Start();
            for (int i = 0; i < duration; i++)
            {
                IMoveTo((int)Math.Truncate(x += incrementX), (int)Math.Truncate(y += incrementY));
                // 空载以确保以一毫秒运行一次循环
                while (stopWatch.ElapsedMilliseconds < i)
                    ;
            }
            stopWatch.Stop();
        }
        IMoveTo(position.X + pixelDeltaX, position.Y + pixelDeltaY);
        return this;
    }

    /// <inheritdoc/>
    public IMouseSimulator MoveTo(int pixelX, int pixelY, int duration = 0)
    {
        if (duration > 1)
        {
            // 循环结束后会再移动一次 所以少一次
            duration -= 1;
            // 计算每毫秒的移动比例
            var position = Position;
            var x = (double)position.X;
            var y = (double)position.Y;
            var incrementX = (pixelX - x) / duration;
            var incrementY = (pixelY - y) / duration;
            // 计时, 以保证总时长准确性
            Stopwatch stopWatch = new();
            stopWatch.Start();
            for (int i = 0; i < duration; i++)
            {
                IMoveTo((int)Math.Truncate(x += incrementX), (int)Math.Truncate(y += incrementY));
                // 空载以确保以一毫秒运行一次循环
                while (stopWatch.ElapsedMilliseconds < i)
                    ;
            }
            stopWatch.Stop();
        }
        IMoveTo(pixelX, pixelY);
        return this;
    }

    /// <inheritdoc/>
    public IMouseSimulator AbsoluteMoveTo(int absoluteX, int absoluteY, int duration = 0)
    {
        if (duration > 1)
        {
            // 循环结束后会再移动一次 所以少一次
            duration -= 1;
            // 计算每毫秒的移动比例
            var position = Position;
            var x = position.X * _abstractXRatio;
            var y = position.Y * _abstractYRatio;
            var incrementX = (absoluteX - x) / duration;
            var incrementY = (absoluteY - y) / duration;
            // 计时, 以保证总时长准确性
            Stopwatch stopWatch = new();
            stopWatch.Start();
            for (int i = 0; i < duration; i++)
            {
                IAbsoluteMoveTo(
                    (int)Math.Truncate(x += incrementX),
                    (int)Math.Truncate(y += incrementY)
                );
                // 空载以确保以一毫秒运行一次循环
                while (stopWatch.ElapsedMilliseconds < i)
                    ;
            }
            stopWatch.Stop();
        }
        IAbsoluteMoveTo(absoluteX, absoluteY);
        return this;
    }

    /// <inheritdoc/>
    public IMouseSimulator AbsoluteMoveToPositionOnVirtualDesktop(
        int absoluteX,
        int absoluteY,
        int duration = 0
    )
    {
        if (duration > 1)
        {
            // 循环结束后会再移动一次 所以少一次
            duration -= 1;
            // 计算每毫秒的移动比例
            var position = Position;
            var x = position.X * _abstractXRatio;
            var y = position.Y * _abstractYRatio;
            var incrementX = (absoluteX - x) / duration;
            var incrementY = (absoluteY - y) / duration;
            // 计时, 以保证总时长准确性
            Stopwatch stopWatch = new();
            stopWatch.Start();
            for (int i = 0; i < duration; i++)
            {
                IAddAbsoluteMouseMovementOnVirtualDesktop(
                    (int)Math.Truncate(x += incrementX),
                    (int)Math.Truncate(y += incrementY)
                );
                // 空载以确保以一毫秒运行一次循环
                while (stopWatch.ElapsedMilliseconds < i)
                    ;
            }
            stopWatch.Stop();
        }
        IAddAbsoluteMouseMovementOnVirtualDesktop(absoluteX, absoluteY);
        return this;
    }

    /// <inheritdoc/>
    public IMouseSimulator RandomMotion(int randomThreshold)
    {
        return RandomMotion(randomThreshold, randomThreshold);
    }

    /// <inheritdoc/>
    public IMouseSimulator RandomMotion(int randomThresholdX, int randomThresholdY)
    {
        var position = Position;
        IMoveTo(GetRandom(position.X, randomThresholdX), GetRandom(position.Y, randomThresholdY));
        return this;

        static int GetRandom(int pixel, int randomThreshold)
        {
            return new Random(Guid.NewGuid().GetHashCode()).Next(
                pixel - randomThreshold,
                pixel + randomThreshold
            );
        }
    }

    /// <inheritdoc/>
    public IMouseSimulator ButtonDown(MouseButton button)
    {
        var inputList = new InputBuilder();
        inputList.AddMouseButtonDown(button);
        SendSimulatedInput(inputList);
        return this;
    }

    /// <inheritdoc/>
    public IMouseSimulator ButtonUp(MouseButton button)
    {
        var inputList = new InputBuilder();
        inputList.AddMouseButtonUp(MouseButton.Left);
        SendSimulatedInput(inputList);
        return this;
    }

    /// <inheritdoc/>
    public IMouseSimulator ButtonClick(MouseButton button)
    {
        var inputList = new InputBuilder();
        inputList.AddMouseButtonClick(MouseButton.Left);
        SendSimulatedInput(inputList);
        return this;
    }

    /// <inheritdoc/>
    public IMouseSimulator ButtonDoubleClick(MouseButton button)
    {
        var inputList = new InputBuilder();
        inputList.AddMouseButtonDoubleClick(button);
        SendSimulatedInput(inputList);
        return this;
    }

    /// <inheritdoc/>
    public IMouseSimulator XButtonDown(XButton xButton)
    {
        var inputList = new InputBuilder();
        inputList.AddMouseXButtonDown(xButton);
        SendSimulatedInput(inputList);
        return this;
    }

    /// <inheritdoc/>
    public IMouseSimulator XButtonUp(XButton xButton)
    {
        var inputList = new InputBuilder();
        inputList.AddMouseXButtonUp(xButton);
        SendSimulatedInput(inputList);
        return this;
    }

    /// <inheritdoc/>
    public IMouseSimulator XButtonClick(XButton xButton)
    {
        var inputList = new InputBuilder();
        inputList.AddMouseXButtonClick(xButton);
        SendSimulatedInput(inputList);
        return this;
    }

    /// <inheritdoc/>
    public IMouseSimulator XButtonDoubleClick(XButton xButton)
    {
        var inputList = new InputBuilder();
        inputList.AddMouseXButtonDoubleClick(xButton);
        SendSimulatedInput(inputList);
        return this;
    }

    /// <inheritdoc/>
    public IMouseSimulator VerticalScroll(int scrollAmountInClicks)
    {
        var inputList = new InputBuilder();
        inputList.AddMouseVerticalWheelScroll(scrollAmountInClicks * MouseWheelClickSize);
        SendSimulatedInput(inputList);
        return this;
    }

    /// <inheritdoc/>
    public IMouseSimulator HorizontalScroll(int scrollAmountInClicks)
    {
        var inputList = new InputBuilder();
        inputList.AddMouseHorizontalWheelScroll(scrollAmountInClicks * MouseWheelClickSize);
        SendSimulatedInput(inputList);
        return this;
    }

    private static MousePoint GetMousePosition()
    {
        NativeMethods.GetCursorPos(out var point);
        return point;
    }

    /// <inheritdoc/>
    public IMouseSimulator Sleep(int millsecondsTimeout)
    {
        Thread.Sleep(millsecondsTimeout);
        return this;
    }

    /// <inheritdoc/>
    public IMouseSimulator Sleep(TimeSpan timeout)
    {
        Thread.Sleep(timeout);
        return this;
    }

    /// <inheritdoc/>
    public async Task<IMouseSimulator> Delay(int milliseconds)
    {
        await Task.Delay(milliseconds);
        return this;
    }

    /// <inheritdoc/>
    public async Task<IMouseSimulator> Delay(TimeSpan timeout)
    {
        await Task.Delay(timeout);
        return this;
    }
}
