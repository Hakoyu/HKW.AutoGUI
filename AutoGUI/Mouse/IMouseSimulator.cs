using HKW.AutoGUI.InputDeviceState;

namespace HKW.AutoGUI.Mouse;

/// <summary>
/// 鼠标模拟接口
/// </summary>
public interface IMouseSimulator : IMouseOnScreen, IInputDelay<IMouseSimulator>
{
    /// <summary>
    /// 位置
    /// </summary>
    public MousePoint Position { get; }

    /// <summary>
    /// 获取或设置每次点击的鼠标滚轮滚动量。
    /// <para>默认为 <see langword="120"/> 不同的值可能导致一些应用程序对滚动的解释与预期不同。</para>
    /// </summary>
    public int MouseWheelClickSize { get; set; }

    /// <summary>
    /// 相对移动至 (单位为像素)
    /// </summary>
    /// <param name="pixelX">X坐标</param>
    /// <param name="pixelY">Y坐标</param>
    /// <param name="duration">持续时间 (单位为毫秒) 默认为: <see langword="0"/></param>
    public IMouseSimulator MoveBy(int pixelX, int pixelY, int duration = 0);

    /// <summary>
    /// 移动至基于分辨率的指定位置 (单位为像素)
    /// </summary>
    /// <param name="pixelX">X坐标</param>
    /// <param name="pixelY">Y坐标</param>
    /// <param name="duration">持续时间 (单位为毫秒) 默认为: <see langword="0"/></param>
    public IMouseSimulator MoveTo(int pixelX, int pixelY, int duration = 0);

    /// <summary>
    /// 移动至绝对值位置
    /// <para>左上 <see langword="(0,0)"/> 右下 <see langword="(65535,65535)"/></para>
    /// </summary>
    /// <param name="absoluteX">绝对值X 范围: <see langword="0"/> ~ <see langword="65535"/></param>
    /// <param name="absoluteY">绝对值Y 范围: <see langword="0"/> ~ <see langword="65535"/></param>
    /// <param name="duration">持续时间 (单位为毫秒) 默认为: <see langword="0"/></param>
    public IMouseSimulator AbsoluteMoveTo(int absoluteX, int absoluteY, int duration = 0);

    /// <summary>
    /// 移动到虚拟桌面上的指定绝对值位置, 包括所有活动的显示器。
    /// <para>左上 <see langword="(0,0)"/> 右下 <see langword="(65535,65535)"/></para>
    /// </summary>
    /// <param name="absoluteX">绝对值X 范围: <see langword="0"/> ~ <see langword="65535"/></param>
    /// <param name="absoluteY">绝对值Y 范围: <see langword="0"/> ~ <see langword="65535"/></param>
    /// <param name="duration"> 持续时间(单位为毫秒) 默认为: <see langword="0"/></param>
    public IMouseSimulator AbsoluteMoveToPositionOnVirtualDesktop(
        int absoluteX,
        int absoluteY,
        int duration = 0
    );

    /// <summary>
    /// 鼠标位置随机运动
    /// <para>例如: 当 x = 0, y = 0, randomThreshold = 5 时, 随机的区域为 (-5, -5) ~ (5, 5)</para>
    /// </summary>
    /// <param name="randomThreshold">随机阈值</param>
    public IMouseSimulator RandomMotion(int randomThreshold);

    /// <summary>
    /// 鼠标位置随机运动
    /// <para>例如: 当 x = 0, y = 0, randomThresholdX = 5, randomThresholdY = 3 时, 随机的区域为 (-5, -3) ~ (5, 3)</para>
    /// </summary>
    /// <param name="randomThresholdX">随机阈值X</param>
    /// <param name="randomThresholdY">随机阈值Y</param>
    public IMouseSimulator RandomMotion(int randomThresholdX, int randomThresholdY);

    /// <summary>
    /// 按下指定按键
    /// </summary>
    /// <param name="button">按键</param>
    public IMouseSimulator ButtonDown(MouseButton button);

    /// <summary>
    /// 释放指定按键
    /// </summary>
    /// <param name="button">按键</param>
    public IMouseSimulator ButtonUp(MouseButton button);

    /// <summary>
    /// 指定按键单击
    /// </summary>
    /// <param name="button">按键</param>
    public IMouseSimulator ButtonClick(MouseButton button);

    /// <summary>
    /// 指定按键双击
    /// </summary>
    /// <param name="button">按键</param>
    public IMouseSimulator ButtonDoubleClick(MouseButton button);

    /// <summary>
    /// 按下指定X键
    /// </summary>
    /// <param name="xButton">X按钮</param>
    public IMouseSimulator XButtonDown(XButton xButton);

    /// <summary>
    /// 释放指定X键
    /// </summary>
    /// <param name="xButton">X按钮</param>
    public IMouseSimulator XButtonUp(XButton xButton);

    /// <summary>
    /// 指定X键单击
    /// </summary>
    /// <param name="xButton">X按钮</param>
    public IMouseSimulator XButtonClick(XButton xButton);

    /// <summary>
    /// 指定X键双击
    /// </summary>
    /// <param name="xButton">X按钮</param>
    public IMouseSimulator XButtonDoubleClick(XButton xButton);

    /// <summary>
    /// 垂直滚动
    /// </summary>
    /// <param name="scrollAmountInClicks">以次数为单位的滚动量, 正值向前远离用户, 负值向后朝向用户</param>
    public IMouseSimulator VerticalScroll(int scrollAmountInClicks);

    /// <summary>
    /// 水平滚动
    /// </summary>
    /// <param name="scrollAmountInClicks">以次数为单位的滚动量, 正值向前远离用户, 负值向后朝向用户</param>
    public IMouseSimulator HorizontalScroll(int scrollAmountInClicks);
}
