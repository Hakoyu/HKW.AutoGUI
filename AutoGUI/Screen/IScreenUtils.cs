using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.AutoGUI;

/// <summary>
/// 屏幕工具
/// </summary>
public interface IScreenUtils
{
    /// <summary>
    /// 屏幕大小
    /// </summary>
    public ScreenResolution Size { get; }
}
