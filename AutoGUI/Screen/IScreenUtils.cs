namespace HKW.AutoGUI.Screen;

/// <summary>
/// 屏幕工具
/// </summary>
public interface IScreenUtils
{
    /// <summary>
    /// 屏幕大小
    /// </summary>
    public ScreenResolution Size { get; }

    /// <summary>
    /// 截屏
    /// </summary>
    /// <returns>截屏图片</returns>
    public Image Screenshot();

    /// <summary>
    /// 从屏幕定位图像
    /// </summary>
    /// <param name="path">图片文件</param>
    /// <returns>定位数据</returns>
    public LocateData? LocateOnScreen(string path);

    /// <summary>
    /// 从屏幕定位图像
    /// </summary>
    /// <param name="image">图片</param>
    /// <returns>定位数据</returns>
    public LocateData? LocateOnScreen(Image image);

    /// <summary>
    /// 从屏幕指定位置定位图像
    /// </summary>
    /// <param name="path">图片文件</param>
    /// <param name="x">X坐标</param>
    /// <param name="y">Y坐标</param>
    /// <param name="width">宽</param>
    /// <param name="height">高</param>
    /// <returns>定位数据</returns>
    public LocateData? LocateOnScreen(string path, int x, int y, int width, int height);

    /// <summary>
    /// 从屏幕指定位置定位图像
    /// </summary>
    /// <param name="image">图片</param>
    /// <param name="x">X坐标</param>
    /// <param name="y">Y坐标</param>
    /// <param name="width">宽</param>
    /// <param name="height">高</param>
    /// <returns>定位数据</returns>
    public LocateData? LocateOnScreen(Image image, int x, int y, int width, int height);
}
