using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.UI.Input.KeyboardAndMouse;
using Windows.Win32.UI.WindowsAndMessaging;

namespace HKW.AutoGUI.Windows;

/// <summary>
/// 鼠标模拟
/// </summary>
[SupportedOSPlatform(nameof(OSPlatform.Windows))]
public class WindowsMouseSimulator : IMouseSimulator<WindowsMouseSimulator>
{
    /// <summary>
    /// 默认鼠标滚轮滚动量
    /// </summary>
    public const int DefaultMouseWheelScrollAmount = 120;

    /// <inheritdoc/>
    public int MouseWheelScrollAmount { get; set; } = DefaultMouseWheelScrollAmount;

    /// <summary>
    /// X洲绝对值比例
    /// </summary>
    public double AbstractXRatio { get; }

    /// <summary>
    /// Y轴绝对值比例
    /// </summary>
    public double AbstractYRatio { get; }

    /// <summary>
    /// 消息分配器
    /// </summary>
    public IInputMessageDispatcher MessageDispatcher { get; set; }

    /// <inheritdoc/>
    public float DPIScaling { get; set; }

    /// <inheritdoc/>
    /// <param name="mainScreenInfo">主屏幕信息</param>
    public WindowsMouseSimulator(IScreenInfo mainScreenInfo)
    {
        DPIScaling = mainScreenInfo.DPIScaling;
        AbstractXRatio = 65535.0 / mainScreenInfo.RealResolution.Width;
        AbstractYRatio = 65535.0 / mainScreenInfo.RealResolution.Height;
        MessageDispatcher = new InputMessageDispatcher();
    }

    /// <summary>
    /// 获取鼠标坐标
    /// </summary>
    /// <returns>鼠标坐标</returns>
    public Point GetPosition()
    {
        PInvoke.GetCursorPos(out var p);
        return new()
        {
            X = (int)Math.Round(p.X * DPIScaling),
            Y = (int)Math.Round(p.Y * DPIScaling)
        };
    }

    /// <summary>
    /// 发送模拟输入
    /// </summary>
    /// <param name="input">输入构造器</param>
    private void SendSimulatedInput(InputBuilder input)
    {
        MessageDispatcher.DispatchInput(input);
    }

    private void MoveTo(int pixelX, int pixelY)
    {
        var inputList = new InputBuilder();
        inputList.AddAbsoluteMouseMovement(
            (int)Math.Round(pixelX * AbstractXRatio),
            (int)Math.Round(pixelY * AbstractYRatio)
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

    #region IMouseSimulator
    /// <inheritdoc/>
    public WindowsMouseSimulator MoveBy(int pixelDeltaX, int pixelDeltaY, int duration = 0)
    {
        var position = GetPosition();
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
                MoveTo((int)Math.Round(x += incrementX), (int)Math.Round(y += incrementY));
                // 空载以确保以一毫秒运行一次循环
                while (stopWatch.ElapsedMilliseconds < i)
                    ;
            }
            stopWatch.Stop();
        }
        MoveTo(position.X + pixelDeltaX, position.Y + pixelDeltaY);
        return this;
    }

    /// <inheritdoc/>
    public WindowsMouseSimulator MoveTo(int pixelX, int pixelY, int duration = 0)
    {
        if (duration > 1)
        {
            // 循环结束后会再移动一次 所以少一次
            duration -= 1;
            // 计算每毫秒的移动比例
            var position = GetPosition();
            var x = (double)position.X;
            var y = (double)position.Y;
            var incrementX = (pixelX - x) / duration;
            var incrementY = (pixelY - y) / duration;
            // 计时, 以保证总时长准确性
            Stopwatch stopWatch = new();
            stopWatch.Start();
            for (int i = 0; i < duration; i++)
            {
                MoveTo((int)Math.Round(x += incrementX), (int)Math.Round(y += incrementY));
                // 空载以确保以一毫秒运行一次循环
                while (stopWatch.ElapsedMilliseconds < i)
                    ;
            }
            stopWatch.Stop();
        }
        MoveTo(pixelX, pixelY);
        return this;
    }

    /// <inheritdoc/>
    public WindowsMouseSimulator AbsoluteMoveTo(int absoluteX, int absoluteY, int duration = 0)
    {
        if (duration > 1)
        {
            // 循环结束后会再移动一次 所以少一次
            duration -= 1;
            // 计算每毫秒的移动比例
            var position = GetPosition();
            var x = position.X * AbstractXRatio;
            var y = position.Y * AbstractYRatio;
            var incrementX = (absoluteX - x) / duration;
            var incrementY = (absoluteY - y) / duration;
            // 计时, 以保证总时长准确性
            Stopwatch stopWatch = new();
            stopWatch.Start();
            for (int i = 0; i < duration; i++)
            {
                IAbsoluteMoveTo((int)Math.Round(x += incrementX), (int)Math.Round(y += incrementY));
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
    public WindowsMouseSimulator AbsoluteMoveToOnVirtualDesktop(
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
            var position = GetPosition();
            var x = position.X * AbstractXRatio;
            var y = position.Y * AbstractYRatio;
            var incrementX = (absoluteX - x) / duration;
            var incrementY = (absoluteY - y) / duration;
            // 计时, 以保证总时长准确性
            Stopwatch stopWatch = new();
            stopWatch.Start();
            for (int i = 0; i < duration; i++)
            {
                IAddAbsoluteMouseMovementOnVirtualDesktop(
                    (int)Math.Round(x += incrementX),
                    (int)Math.Round(y += incrementY)
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
    public WindowsMouseSimulator RandomMotion(int randomThreshold)
    {
        return RandomMotion(randomThreshold, randomThreshold);
    }

    /// <inheritdoc/>
    public WindowsMouseSimulator RandomMotion(int randomThresholdX, int randomThresholdY)
    {
        var position = GetPosition();
        MoveTo(GetRandom(position.X, randomThresholdX), GetRandom(position.Y, randomThresholdY));
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
    public WindowsMouseSimulator ButtonDown(MouseButton button)
    {
        var inputList = new InputBuilder();
        inputList.AddMouseButtonDown(button);
        SendSimulatedInput(inputList);
        return this;
    }

    /// <inheritdoc/>
    public WindowsMouseSimulator ButtonUp(MouseButton button)
    {
        var inputList = new InputBuilder();
        inputList.AddMouseButtonUp(button);
        SendSimulatedInput(inputList);
        return this;
    }

    /// <inheritdoc/>
    public WindowsMouseSimulator ButtonClick(MouseButton button)
    {
        var inputList = new InputBuilder();
        inputList.AddMouseButtonClick(button);
        SendSimulatedInput(inputList);
        return this;
    }

    /// <inheritdoc/>
    public WindowsMouseSimulator ButtonDoubleClick(MouseButton button)
    {
        var inputList = new InputBuilder();
        inputList.AddMouseButtonDoubleClick(button);
        SendSimulatedInput(inputList);
        return this;
    }

    /// <inheritdoc/>
    public WindowsMouseSimulator XButtonDown(XButton xButton)
    {
        var inputList = new InputBuilder();
        inputList.AddMouseXButtonDown(xButton);
        SendSimulatedInput(inputList);
        return this;
    }

    /// <inheritdoc/>
    public WindowsMouseSimulator XButtonUp(XButton xButton)
    {
        var inputList = new InputBuilder();
        inputList.AddMouseXButtonUp(xButton);
        SendSimulatedInput(inputList);
        return this;
    }

    /// <inheritdoc/>
    public WindowsMouseSimulator XButtonClick(XButton xButton)
    {
        var inputList = new InputBuilder();
        inputList.AddMouseXButtonClick(xButton);
        SendSimulatedInput(inputList);
        return this;
    }

    /// <inheritdoc/>
    public WindowsMouseSimulator XButtonDoubleClick(XButton xButton)
    {
        var inputList = new InputBuilder();
        inputList.AddMouseXButtonDoubleClick(xButton);
        SendSimulatedInput(inputList);
        return this;
    }

    /// <inheritdoc/>
    public WindowsMouseSimulator VerticalScroll(int scrollAmount)
    {
        var inputList = new InputBuilder();
        inputList.AddMouseVerticalWheelScroll(scrollAmount * MouseWheelScrollAmount);
        SendSimulatedInput(inputList);
        return this;
    }

    /// <inheritdoc/>
    public WindowsMouseSimulator HorizontalScroll(int scrollAmount)
    {
        var inputList = new InputBuilder();
        inputList.AddMouseHorizontalWheelScroll(scrollAmount * MouseWheelScrollAmount);
        SendSimulatedInput(inputList);
        return this;
    }
    #endregion
    #region IMouseOnScreen
    /// <inheritdoc/>
    public bool OnScreen()
    {
        return true;
    }

    /// <inheritdoc/>
    public bool OnScreen(int pixelX, int pixelY)
    {
        var position = GetPosition();
        if (position.X < pixelX || position.Y < pixelY)
            return false;
        return true;
    }

    /// <inheritdoc/>
    public bool OnScreen(int pixelX, int pixelY, int width, int height)
    {
        var position = GetPosition();
        if (
            position.X < pixelX
            || position.X > pixelX + width
            || position.Y < pixelY
            || position.Y > pixelY + height
        )
            return false;
        return true;
    }
    #endregion
}
