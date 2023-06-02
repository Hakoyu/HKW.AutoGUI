using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.AutoGUI.Screen;

/// <summary>
/// 屏幕模拟
/// </summary>
public interface IScreen
{
    public ScreenResolution Size { get; }
}
