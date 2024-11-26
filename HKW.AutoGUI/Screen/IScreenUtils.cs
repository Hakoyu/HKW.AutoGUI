using System.Collections.ObjectModel;
using HPPH;
using SixLabors.ImageSharp;

namespace HKW.AutoGUI;

/// <summary>
/// 屏幕工具
/// </summary>
public interface IScreenUtils
{
    /// <summary>
    /// 屏幕大小
    /// </summary>
    public ReadOnlyCollection<IScreenInfo> ScreenInfos { get; }

    /// <summary>
    /// 全屏幕截屏
    /// </summary>
    /// <returns>截屏图片</returns>
    public IImage<ColorBGRA>? Screenshot();

    /// <summary>
    /// 区域截屏
    /// </summary>
    /// <param name="x">坐标X</param>
    /// <param name="y">坐标Y</param>
    /// <param name="width">宽</param>
    /// <param name="height">高</param>
    /// <returns>截屏图片</returns>
    public IImage<ColorBGRA>? Screenshot(int x, int y, int width, int height);

    /// <summary>
    /// 从屏幕定位图像
    /// </summary>
    /// <param name="image">图片</param>
    /// <returns>定位数据</returns>
    public Rectangle? LocateOnScreen(Image image);

    /// <summary>
    /// 从屏幕指定位置定位图像
    /// </summary>
    /// <param name="image">图片</param>
    /// <param name="x">X坐标</param>
    /// <param name="y">Y坐标</param>
    /// <param name="width">宽</param>
    /// <param name="height">高</param>
    /// <returns>定位数据</returns>
    public Rectangle? LocateOnScreen(Image image, int x, int y, int width, int height);
}
