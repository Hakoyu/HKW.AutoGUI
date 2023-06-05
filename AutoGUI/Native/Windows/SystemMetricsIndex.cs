namespace HKW.AutoGUI.Native.Windows;

#pragma warning disable CA1069 // 不应复制枚举值
/// <summary>
/// 系统度量索引列表
/// </summary>
public enum SystemMetricsIndex
{
    /// <summary>
    /// 指定系统如何排列最小化窗口的标志。
    /// </summary>
    SM_ARRANGE = 56,

    /// <summary>
    /// 指定系统启动方式的 值：
    /// <para>0 正常启动</para>
    /// <para>1 故障安全启动</para>
    /// <para>2 通过网络启动实现故障安全</para>
    /// <para>故障安全启动 (也称为 SafeBoot、安全模式或干净启动) 会绕过用户启动文件。</para>
    /// </summary>
    SM_CLEANBOOT = 67,

    /// <summary>
    /// 桌面上的显示监视器数。
    /// </summary>
    SM_CMONITORS = 80,

    /// <summary>
    /// 鼠标上的按钮数;如果未安装鼠标, 则为零。
    /// </summary>
    SM_CMOUSEBUTTONS = 43,

    /// <summary>
    /// 反映笔记本电脑或平板模式的状态, 0 表示板模式, 否则为非零。
    /// <para>当此系统指标发生更改时, 系统会通过 LPARAM 中带有“ConvertibleSlateMode” 的 WM_SETTINGCHANGE 发送广播消息。</para>
    /// <para>请注意, 此系统指标不适用于台式电脑。 在这种情况下, 请使用 GetAutoRotationState。</para>
    /// </summary>
    SM_CONVERTIBLESLATEMODE = 0x2003,

    /// <summary>
    /// 窗口边框的宽度（以像素为单位）。
    /// <para>这等效于具有 3D 外观的窗口的 <see cref="SM_CXEDGE"/> 值。</para>
    /// </summary>
    SM_CXBORDER = 5,

    /// <summary>
    /// 光标的标称宽度（以像素为单位）。
    /// </summary>
    SM_CXCURSOR = 13,

    /// <summary>
    /// 此值与 SM_CXFIXEDFRAME 相同。
    /// </summary>
    SM_CXDLGFRAME = 7,

    /// <summary>
    /// 矩形围绕双击序列中第一次单击的位置的宽度（以像素为单位）。
    /// <para>第二次单击必须在由 <see cref="SM_CXDOUBLECLK"/> 和 <see cref="SM_CYDOUBLECLK"/> 定义的矩形内发生, 系统才能将两次单击视为双击。</para>
    /// <para>两次单击也必须在指定时间内发生。</para>
    /// <para>若要设置双击矩形的宽度, 请使用 SPI_SETDOUBLECLKWIDTH 调用 SystemParametersInfo。</para>
    /// </summary>
    SM_CXDOUBLECLK = 36,

    /// <summary>
    /// 鼠标指针在拖动操作开始之前可以移动的鼠标向下点任一侧的像素数。
    /// <para>这允许用户轻松单击并释放鼠标按钮, 而不会无意中启动拖动操作。</para>
    /// <para>如果此值为负值, 则从鼠标向下点的左侧减去该值, 并将其添加到其右侧。</para>
    /// </summary>
    SM_CXDRAG = 68,

    /// <summary>
    /// 三维边框的宽度（以像素为单位）。
    /// <para>此指标是 <see cref="SM_CXBORDER"/> 的三维对应指标。</para>
    /// </summary>
    SM_CXEDGE = 45,

    /// <summary>
    /// 窗口周围具有描述文字但不是相当大的（以像素为单位）的框架的粗细。
    /// <para> <see cref="SM_CXFIXEDFRAME"/> 是水平边框的高度, <see cref="SM_CYFIXEDFRAME"/> 是垂直边框的宽度。</para>
    /// <para>此值与 <see cref="SM_CXDLGFRAME"/> 相同。</para>
    /// </summary>

    SM_CXFIXEDFRAME = 7,

    /// <summary>
    /// DrawFocusRect 绘制的焦点矩形的左边缘和右边缘的宽度。
    /// <para>此值以像素为单位。</para>
    /// <para>Windows 2000： 不支持此值。</para>
    /// </summary>
    SM_CXFOCUSBORDER = 83,

    /// <summary>
    /// 此值与 <see cref="SM_CXSIZEFRAME"/> 相同。
    /// </summary>
    SM_CXFRAME = 32,

    /// <summary>
    /// 主显示器上全屏窗口的工作区宽度（以像素为单位）。
    /// <para>若要获取系统任务栏或应用程序桌面工具栏未遮挡的屏幕部分的坐标, 请使用 SPI_GETWORKAREA 值调用 SystemParametersInfo 函数。</para>
    /// </summary>
    SM_CXFULLSCREEN = 16,

    /// <summary>
    /// 水平滚动条上箭头位图的宽度（以像素为单位）。
    /// </summary>
    SM_CXHSCROLL = 21,

    /// <summary>
    /// 水平滚动条中拇指框的宽度（以像素为单位）。
    /// </summary>
    SM_CXHTHUMB = 10,

    /// <summary>
    /// 图标的系统大宽度（以像素为单位）。
    /// <para>LoadIcon 函数只能加载具有 <see cref="SM_CXICON"/> 和 <see cref="SM_CYICON"/> 指定尺寸的图标。</para>
    /// </summary>
    SM_CXICON = 11,

    /// <summary>
    /// 大图标视图中项的网格单元格的宽度（以像素为单位）。
    /// <para>每个项都适合在排列时按 <see cref="SM_CYICONSPACING"/> <see cref="SM_CXICONSPACING"/> 大小的矩形。</para>
    /// <para>此值始终大于或等于 <see cref="SM_CXICON"/>。</para>
    /// </summary>
    SM_CXICONSPACING = 38,

    /// <summary>
    /// 主显示监视器上最大化的顶级窗口的默认宽度（以像素为单位）。
    /// </summary>
    SM_CXMAXIMIZED = 61,

    /// <summary>
    /// 具有描述文字和大小调整边框（以像素为单位）的窗口的默认最大宽度。
    /// <para>此指标是指整个桌面。</para>
    /// <para>用户无法将窗口框架拖动到大于这些尺寸的大小。</para>
    /// <para>窗口可以通过处理 WM_GETMINMAXINFO 消息来替代此值。</para>
    /// </summary>
    SM_CXMAXTRACK = 59,

    /// <summary>
    /// 默认菜单的宽度检查标记位图（以像素为单位）。
    /// </summary>
    SM_CXMENUCHECK = 71,

    /// <summary>
    /// 菜单栏按钮的宽度, 例如在多个文档界面中使用的子窗口关闭按钮（以像素为单位）。
    /// </summary>
    SM_CXMENUSIZE = 54,

    /// <summary>
    /// 窗口的最小宽度（以像素为单位）。
    /// </summary>
    SM_CXMIN = 28,

    /// <summary>
    /// 最小化窗口的宽度（以像素为单位）。
    /// </summary>
    SM_CXMINIMIZED = 57,

    /// <summary>
    /// 最小化窗口的网格单元格的宽度（以像素为单位）。
    /// <para>每个最小化窗口在排列时适合此大小的矩形。</para>
    /// <para>此值始终大于或等于 <see cref="SM_CXMINIMIZED"/>。</para>
    /// </summary>
    SM_CXMINSPACING = 47,

    /// <summary>
    /// 窗口的最小跟踪宽度（以像素为单位）。
    /// <para>用户无法将窗口框架拖动到小于这些尺寸的大小。</para>
    /// <para>窗口可以通过处理 WM_GETMINMAXINFO 消息来替代此值。</para>
    /// </summary>
    SM_CXMINTRACK = 34,

    /// <summary>
    /// 带字幕窗口的边框填充量（以像素为单位）。
    /// <para>Windows XP/2000： 不支持此值。</para>
    /// </summary>
    SM_CXPADDEDBORDER = 92,

    /// <summary>
    /// 主显示器的屏幕宽度（以像素为单位）。
    /// <para>这是通过调用 GetDeviceCaps 获取的相同值, 如下所示：GetDeviceCaps(hdcPrimaryMonitor, HORZRES)。</para>
    /// </summary>
    SM_CXSCREEN = 0,

    /// <summary>
    /// 窗口中按钮的宽度描述文字或标题栏（以像素为单位）。
    /// </summary>
    SM_CXSIZE = 30,

    /// <summary>
    /// 可调整大小的窗口周边的大小边框的粗细（以像素为单位）。
    /// <para><see cref="SM_CXSIZEFRAME"/> 是水平边框的宽度, <see cref="SM_CYSIZEFRAME"/> 是垂直边框的高度。</para>
    /// <para>此值与 <see cref="SM_CXFRAME"/> 相同。</para>
    /// </summary>
    SM_CXSIZEFRAME = 32,

    /// <summary>
    /// 图标的系统小宽度（以像素为单位）。
    /// <para>小图标通常显示在窗口标题和小图标视图中。</para>
    /// </summary>
    SM_CXSMICON = 49,

    /// <summary>
    /// 小描述文字按钮的宽度（以像素为单位）。
    /// </summary>
    SM_CXSMSIZE = 52,

    /// <summary>
    /// 虚拟屏幕的宽度（以像素为单位）。
    /// <para>虚拟屏幕是所有显示监视器的边框。</para>
    /// <para><see cref="SM_XVIRTUALSCREEN"/> 指标是虚拟屏幕左侧的坐标。</para>
    /// </summary>
    SM_CXVIRTUALSCREEN = 78,

    /// <summary>
    /// 垂直滚动条的宽度（以像素为单位）。
    /// </summary>
    SM_CXVSCROLL = 2,

    /// <summary>
    /// 窗口边框的高度（以像素为单位）。
    /// <para>这等效于具有 3D 外观的窗口的 <see cref="SM_CYEDGE"/> 值。</para>
    /// </summary>
    SM_CYBORDER = 6,

    /// <summary>
    /// 描述文字区域的高度（以像素为单位）。
    /// </summary>
    SM_CYCAPTION = 4,

    /// <summary>
    /// 光标的标称高度（以像素为单位）。
    /// </summary>
    SM_CYCURSOR = 14,

    /// <summary>
    /// 此值与 <see cref="SM_CYFIXEDFRAME"/> 相同。
    /// </summary>
    SM_CYDLGFRAME = 8,

    /// <summary>
    /// 矩形围绕双击序列中第一次单击的位置的高度（以像素为单位）。
    /// <para>第二次单击必须在由 <see cref="SM_CXDOUBLECLK"/> 定义的矩形内发生, <see cref="SM_CYDOUBLECLK"/> 系统会将两次单击视为双击。</para>
    /// <para>两次单击也必须在指定时间内发生。</para>
    /// <para>若要设置双击矩形的高度, 请使用SPI_SETDOUBLECLKHEIGHT调用 SystemParametersInfo 。</para>
    /// </summary>
    SM_CYDOUBLECLK = 37,

    /// <summary>
    /// 鼠标指针在拖动操作开始之前可以移动的鼠标向下点上方和下方的像素数。
    /// <para>这允许用户轻松单击并释放鼠标按钮, 而不会无意中启动拖动操作。</para>
    /// <para>如果此值为负值, 则从鼠标向下点上方减去该值, 并将其添加到其下方。</para>
    /// </summary>
    SM_CYDRAG = 69,

    /// <summary>
    /// 三维边框的高度（以像素为单位）。
    /// <para>这是 <see cref="SM_CYBORDER"/> 的三维对应项。</para>
    /// </summary>
    SM_CYEDGE = 46,

    /// <summary>
    /// 窗口周围具有描述文字但不是相当大的（以像素为单位）的框架的粗细。
    /// <see cref="SM_CXFIXEDFRAME"/> 是水平边框的高度, <see cref="SM_CYFIXEDFRAME"/> 是垂直边框的宽度。
    /// <para>此值与 <see cref="SM_CYDLGFRAME"/> 相同。</para>
    /// </summary>
    SM_CYFIXEDFRAME = 8,

    /// <summary>
    /// DrawFocusRect绘制的焦点矩形的上边缘和下边缘的高度。
    /// <para>此值以像素为单位。</para>
    /// <para>Windows 2000： 不支持此值。</para>
    /// </summary>
    SM_CYFOCUSBORDER = 84,

    /// <summary>
    /// 此值与 <see cref="SM_CYSIZEFRAME"/> 相同。
    /// </summary>
    SM_CYFRAME = 33,

    /// <summary>
    /// 主显示器上全屏窗口的工作区高度（以像素为单位）。
    /// 若要获取系统任务栏或应用程序桌面工具栏未遮挡的屏幕部分的坐标, 请使用 SPI_GETWORKAREA 值调用 SystemParametersInfo 函数。
    /// </summary>
    SM_CYFULLSCREEN = 17,

    /// <summary>
    /// 水平滚动条的高度（以像素为单位）。
    /// </summary>
    SM_CYHSCROLL = 3,

    /// <summary>
    /// 图标的系统高度（以像素为单位）。
    /// <para>LoadIcon 函数只能加载具有 <see cref="SM_CXICON"/> 和 <see cref="SM_CYICON"/> 指定尺寸的图标。</para>
    /// </summary>
    SM_CYICON = 12,

    /// <summary>
    /// 大图标视图中项的网格单元格的高度（以像素为单位）。
    /// <para>每个项都适合在排列时按 <see cref="SM_CYICONSPACING"/> <see cref="SM_CXICONSPACING"/> 大小的矩形。</para>
    /// <para>此值始终大于或等于 <see cref="SM_CYICON"/>。</para>
    /// </summary>
    SM_CYICONSPACING = 39,

    /// <summary>
    /// 对于系统的双字节字符集版本, 这是屏幕底部的汉字窗口的高度（以像素为单位）。
    /// </summary>
    SM_CYKANJIWINDOW = 18,

    /// <summary>
    /// 主显示监视器上最大化的顶级窗口的默认高度（以像素为单位）。
    /// </summary>
    SM_CYMAXIMIZED = 62,

    /// <summary>
    /// 具有描述文字和大小调整边框的窗口的默认最大高度（以像素为单位）。
    /// <para>此指标是指整个桌面。 用户无法将窗口框架拖动到大于这些尺寸的大小。</para>
    /// <para>窗口可以通过处理 WM_GETMINMAXINFO 消息来替代此值。</para>
    /// </summary>
    SM_CYMAXTRACK = 60,

    /// <summary>
    /// 单行菜单栏的高度（以像素为单位）。
    /// </summary>
    SM_CYMENU = 15,

    /// <summary>
    /// 默认菜单的高度检查标记位图（以像素为单位）。
    /// </summary>
    SM_CYMENUCHECK = 72,

    /// <summary>
    /// 菜单栏按钮（例如在多个文档界面中使用的子窗口关闭按钮）的高度（以像素为单位）。
    /// </summary>
    SM_CYMENUSIZE = 55,

    /// <summary>
    /// 窗口的最小高度（以像素为单位）。
    /// </summary>
    SM_CYMIN = 29,

    /// <summary>
    /// 最小化窗口的高度（以像素为单位）。
    /// </summary>
    SM_CYMINIMIZED = 58,

    /// <summary>
    /// 最小化窗口的网格单元格的高度（以像素为单位）。
    /// <para>每个最小化窗口在排列时适合此大小的矩形。</para>
    /// <para>此值始终大于或等于 <see cref="SM_CYMINIMIZED"/>。</para>
    /// </summary>
    SM_CYMINSPACING = 48,

    /// <summary>
    /// 窗口的最小跟踪高度（以像素为单位）。
    /// <para>用户无法将窗口框架拖动到小于这些尺寸的大小。</para>
    /// <para>口可以通过处理 WM_GETMINMAXINFO 消息来替代此值。</para>
    /// </summary>
    SM_CYMINTRACK = 35,

    /// <summary>
    /// 主显示器的屏幕高度（以像素为单位）。
    /// <para>这是通过调用 GetDeviceCaps 获取的相同值, 如下所示：GetDeviceCaps(hdcPrimaryMonitor, VERTRES)。</para>
    /// </summary>
    SM_CYSCREEN = 1,

    /// <summary>
    /// 窗口中按钮的高度描述文字或标题栏（以像素为单位）。
    /// </summary>
    SM_CYSIZE = 31,

    /// <summary>
    /// 可调整大小的窗口周边的大小边框的粗细（以像素为单位）。
    /// <para><see cref="SM_CXSIZEFRAME"/> 是水平边框的宽度, <see cref="SM_CYSIZEFRAME"/> 是垂直边框的高度。</para>
    /// <para>此值与 <see cref="SM_CYFRAME"/> 相同。</para>
    /// </summary>
    SM_CYSIZEFRAME = 33,

    /// <summary>
    /// 小描述文字的高度（以像素为单位）。
    /// </summary>
    SM_CYSMCAPTION = 51,

    /// <summary>
    /// 图标的系统小高度（以像素为单位）。
    /// <para>小图标通常显示在窗口标题和小图标视图中。</para>
    /// </summary>
    SM_CYSMICON = 50,

    /// <summary>
    /// 小描述文字按钮的高度（以像素为单位）。
    /// </summary>
    SM_CYSMSIZE = 53,

    /// <summary>
    /// 虚拟屏幕的高度（以像素为单位）。
    /// <para>虚拟屏幕是所有显示监视器的边框。</para>
    /// <para>SM_YVIRTUALSCREEN 指标是虚拟屏幕顶部的坐标。</para>
    /// </summary>
    SM_CYVIRTUALSCREEN = 79,

    /// <summary>
    /// 垂直滚动条上箭头位图的高度（以像素为单位）。
    /// </summary>
    SM_CYVSCROLL = 20,

    /// <summary>
    /// 垂直滚动条中拇指框的高度（以像素为单位）。
    /// </summary>
    SM_CYVTHUMB = 9,

    /// <summary>
    /// 如果User32.dll支持 DBCS, 则为非零值;否则为 0。
    /// </summary>
    SM_DBCSENABLED = 42,

    /// <summary>
    /// 如果安装了User.exe的调试版本, 则为非零;否则为 0。
    /// </summary>
    SM_DEBUG = 22,

    /// <summary>
    /// 如果当前操作系统是 Windows 7 或 Windows Server 2008 R2 并且平板电脑输入服务已启动, 则为非零;否则为 0。
    /// <para>返回值是一个位掩码, 用于指定设备支持的数字化器输入的类型。</para>
    /// <para>Windows Server 2008、Windows Vista 和 Windows XP/2000： 不支持此值。</para>
    /// </summary>
    SM_DIGITIZER = 94,

    /// <summary>
    /// 如果启用了输入法管理器/输入法编辑器功能, 则为非零值;否则为 0。
    /// <para>SM_IMMENABLED指示系统是否已准备好在 Unicode 应用程序上使用基于 Unicode 的 IME。</para>
    /// <para>若要确保依赖于语言的 IME 正常工作, 检查 SM_DBCSENABLED和系统 ANSI 代码页。</para>
    /// <para>否则, ANSI 到 Unicode 的转换可能无法正确执行, 或者某些组件（如字体或注册表设置）可能不存在。</para>
    /// </summary>
    SM_IMMENABLED = 82,

    /// <summary>
    /// 如果系统中存在数字化器, 则为非零值;否则为 0。
    /// <para>SM_MAXIMUMTOUCHES返回系统中每个数字化器支持的最大接触数的聚合最大值。 </para>
    /// <para>如果系统只有单点触控数字化器, 则返回值为 1。 如果系统具有多点触控数字化器, 则返回值是硬件可以提供的同时触点数。</para>
    /// <para>Windows Server 2008、Windows Vista 和 Windows XP/2000： 不支持此值。</para>
    /// </summary>
    SM_MAXIMUMTOUCHES = 95,

    /// <summary>
    /// 如果当前操作系统是 Windows XP, 则为非零, Media Center Edition 为 0（如果不是）。
    /// </summary>
    SM_MEDIACENTER = 87,

    /// <summary>
    /// 如果下拉菜单与相应的菜单栏项右对齐, 则为非零值;如果菜单左对齐, 则为 0。
    /// </summary>
    SM_MENUDROPALIGNMENT = 40,

    /// <summary>
    /// 如果为希伯来语和阿拉伯语启用系统, 则为非零值;否则为 0。
    /// </summary>
    SM_MIDEASTENABLED = 74,

    /// <summary>
    /// 如果安装了鼠标, 则为非零值;否则为 0。
    /// <para>此值很少为零, 因为支持虚拟鼠标, 并且某些系统检测到端口的存在, 而不是鼠标的存在。</para>
    /// </summary>
    SM_MOUSEPRESENT = 19,

    /// <summary>
    /// 如果安装了水平滚轮的鼠标, 则为非零值;否则为 0。
    /// </summary>
    SM_MOUSEHORIZONTALWHEELPRESENT = 91,

    /// <summary>
    /// 如果安装了具有垂直滚轮的鼠标, 则为非零值;否则为 0。
    /// </summary>
    SM_MOUSEWHEELPRESENT = 75,

    /// <summary>
    /// 如果存在网络, 则设置最小有效位;否则, 将清除它。 其他位保留供将来使用。
    /// </summary>
    SM_NETWORK = 63,

    /// <summary>
    /// 如果安装了 Microsoft Windows for Pen 计算扩展, 则为非零值;否则为零。
    /// </summary>
    SM_PENWINDOWS = 41,

    /// <summary>
    /// 此系统指标在终端服务环境中用于确定是否远程控制当前终端服务器会话。
    /// <para>如果远程控制当前会话, 则其值为非零值;否则为 0。</para>
    /// <para>可以使用终端服务管理工具（如终端服务管理器 (tsadmin.msc) 和shadow.exe）来控制远程会话。</para>
    /// <para>远程控制会话时, 另一个用户可以查看该会话的内容, 并可能与之交互。</para>
    /// </summary>
    SM_REMOTECONTROL = 0x2001,

    /// <summary>
    /// 此系统指标用于终端服务环境。 如果调用进程与终端服务客户端会话相关联, 则返回值为非零值。
    /// <para>如果调用进程与终端服务控制台会话相关联, 则返回值为 0。</para>
    /// <para>Windows Server 2003 和 Windows XP： 控制台会话不一定是物理控制台。</para>
    /// <para>有关详细信息, 请参阅 WTSGetActiveConsoleSessionId 。</para>
    /// </summary>
    SM_REMOTESESSION = 0x1000,

    /// <summary>
    /// 如果所有显示监视器具有相同的颜色格式, 则为非零值, 否则为 0。
    /// <para>两个显示器可以具有相同的位深度, 但颜色格式不同。</para>
    /// <para>例如, 红色、绿色和蓝色像素可以使用不同位数进行编码, 或者这些位可以位于像素颜色值的不同位置。</para>
    /// </summary>
    SM_SAMEDISPLAYFORMAT = 81,

    /// <summary>
    /// 应忽略此系统指标;它始终返回 0。
    /// </summary>
    SM_SECURE = 44,

    /// <summary>
    /// 系统为 Windows Server 2003 R2 时的内部版本号;否则为 0。
    /// </summary>
    SM_SERVERR2 = 89,

    /// <summary>
    /// 如果用户要求应用程序在仅以声音形式显示信息的情况下直观显示信息, 则为非零值;否则为 0。
    /// </summary>
    SM_SHOWSOUNDS = 70,

    /// <summary>
    /// 如果当前会话正在关闭, 则为非零;否则为 0。
    /// <para>Windows 2000： 不支持此值。</para>
    /// </summary>
    SM_SHUTTINGDOWN = 0x2000,

    /// <summary>
    /// 如果计算机具有低端 (慢) 处理器, 则为非零值;否则为 0。
    /// </summary>
    SM_SLOWMACHINE = 73,

    /// <summary>
    /// 如果当前操作系统为 Windows 7 简易版 Edition、Windows Vista 入门版 或 Windows XP Starter Edition, 则为非零;否则为 0。
    /// </summary>
    SM_STARTER = 88,

    /// <summary>
    /// 如果交换了鼠标左键和右键的含义, 则为非零值;否则为 0。
    /// </summary>
    SM_SWAPBUTTON = 23,

    /// <summary>
    /// 反映停靠模式的状态, 0 表示未停靠模式, 否则为非零。
    /// <para>当此系统指标发生更改时, 系统会通过 LPARAM 中带有“SystemDockMode” 的 WM_SETTINGCHANGE 发送广播消息。</para>
    /// </summary>
    SM_SYSTEMDOCKED = 0x2004,

    /// <summary>
    /// 如果当前操作系统是 Windows XP 平板电脑版本, 或者当前操作系统是 Windows Vista 或 Windows 7 并且平板电脑输入服务已启动, 则为非零值;否则为 0。
    /// <para>SM_DIGITIZER设置指示运行 Windows 7 或 Windows Server 2008 R2 的设备支持的数字化器输入类型。</para>
    /// </summary>
    SM_TABLETPC = 86,

    /// <summary>
    /// 虚拟屏幕左侧的坐标。
    /// <para>虚拟屏幕是所有显示监视器的边框。<see cref="SM_CXVIRTUALSCREEN"/> 指标是虚拟屏幕的宽度。</para>
    /// </summary>
    SM_XVIRTUALSCREEN = 76,
}
#pragma warning restore CA1069 // 不应复制枚举值
