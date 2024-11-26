using System.Drawing;

namespace HKW.AutoGUI;

/// <summary>
/// 鼠标模拟接口
/// </summary>
public interface IMouseSimulator<TMouseSimulator> : IMouseOnScreen
    where TMouseSimulator : IMouseSimulator<TMouseSimulator>
{
    /// <summary>
    /// 鼠标滚轮滚动量
    /// </summary>
    public int MouseWheelScrollAmount { get; set; }

    /// <summary>
    /// 获取位置
    /// </summary>
    public Point GetPosition();

    /// <summary>
    /// 相对移动至 (单位为像素)
    /// </summary>
    /// <param name="pixelX">X坐标</param>
    /// <param name="pixelY">Y坐标</param>
    /// <param name="duration">持续时间 (单位为毫秒) 默认为: <see langword="0"/></param>
    public TMouseSimulator MoveBy(int pixelX, int pixelY, int duration = 0);

    /// <summary>
    /// 移动至基于分辨率的指定位置 (单位为像素)
    /// </summary>
    /// <param name="pixelX">X坐标</param>
    /// <param name="pixelY">Y坐标</param>
    /// <param name="duration">持续时间 (单位为毫秒) 默认为: <see langword="0"/></param>
    public TMouseSimulator MoveTo(int pixelX, int pixelY, int duration = 0);

    /// <summary>
    /// 移动至绝对值位置
    /// <para>左上 <see langword="(0,0)"/> 右下 <see langword="(65535,65535)"/></para>
    /// </summary>
    /// <param name="absoluteX">绝对值X 范围: <see langword="0"/> ~ <see langword="65535"/></param>
    /// <param name="absoluteY">绝对值Y 范围: <see langword="0"/> ~ <see langword="65535"/></param>
    /// <param name="duration">持续时间 (单位为毫秒) 默认为: <see langword="0"/></param>
    public TMouseSimulator AbsoluteMoveTo(int absoluteX, int absoluteY, int duration = 0);

    /// <summary>
    /// 移动到虚拟桌面上的指定绝对值位置, 包括所有活动的显示器。
    /// <para>左上 <see langword="(0,0)"/> 右下 <see langword="(65535,65535)"/></para>
    /// </summary>
    /// <param name="absoluteX">绝对值X 范围: <see langword="0"/> ~ <see langword="65535"/></param>
    /// <param name="absoluteY">绝对值Y 范围: <see langword="0"/> ~ <see langword="65535"/></param>
    /// <param name="duration"> 持续时间(单位为毫秒) 默认为: <see langword="0"/></param>
    public TMouseSimulator AbsoluteMoveToOnVirtualDesktop(
        int absoluteX,
        int absoluteY,
        int duration = 0
    );

    /// <summary>
    /// 鼠标位置随机运动
    /// <para>例如: 当 x = 0, y = 0, randomThreshold = 5 时, 随机的区域为 (-5, -5) ~ (5, 5)</para>
    /// </summary>
    /// <param name="randomThreshold">随机阈值</param>
    public TMouseSimulator RandomMotion(int randomThreshold);

    /// <summary>
    /// 鼠标位置随机运动
    /// <para>例如: 当 x = 0, y = 0, randomThresholdX = 5, randomThresholdY = 3 时, 随机的区域为 (-5, -3) ~ (5, 3)</para>
    /// </summary>
    /// <param name="randomThresholdX">随机阈值X</param>
    /// <param name="randomThresholdY">随机阈值Y</param>
    public TMouseSimulator RandomMotion(int randomThresholdX, int randomThresholdY);

    /// <summary>
    /// 按下指定按键
    /// </summary>
    /// <param name="button">按键</param>
    public TMouseSimulator ButtonDown(MouseButton button);

    /// <summary>
    /// 释放指定按键
    /// </summary>
    /// <param name="button">按键</param>
    public TMouseSimulator ButtonUp(MouseButton button);

    /// <summary>
    /// 指定按键单击
    /// </summary>
    /// <param name="button">按键</param>
    public TMouseSimulator ButtonClick(MouseButton button);

    /// <summary>
    /// 指定按键双击
    /// </summary>
    /// <param name="button">按键</param>
    public TMouseSimulator ButtonDoubleClick(MouseButton button);

    /// <summary>
    /// 按下指定X键
    /// </summary>
    /// <param name="xButton">X按钮</param>
    public TMouseSimulator XButtonDown(XButton xButton);

    /// <summary>
    /// 释放指定X键
    /// </summary>
    /// <param name="xButton">X按钮</param>
    public TMouseSimulator XButtonUp(XButton xButton);

    /// <summary>
    /// 指定X键单击
    /// </summary>
    /// <param name="xButton">X按钮</param>
    public TMouseSimulator XButtonClick(XButton xButton);

    /// <summary>
    /// 指定X键双击
    /// </summary>
    /// <param name="xButton">X按钮</param>
    public TMouseSimulator XButtonDoubleClick(XButton xButton);

    /// <summary>
    /// 垂直滚动
    /// </summary>
    /// <param name="scrollAmount">以次数为单位的滚动量, 正值向上, 负值向下</param>
    public TMouseSimulator VerticalScroll(int scrollAmount);

    /// <summary>
    /// 水平滚动
    /// </summary>
    /// <param name="scrollAmount">以次数为单位的滚动量, 正值向左, 负值向右</param>
    public TMouseSimulator HorizontalScroll(int scrollAmount);
}
