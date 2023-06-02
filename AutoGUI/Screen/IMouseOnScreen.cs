﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKW.AutoGUI;

/// <summary>
/// 鼠标在屏幕中接口
/// </summary>
public interface IMouseOnScreen
{
    /// <summary>
    /// 判断是否在屏幕中
    /// </summary>
    /// <returns>存在为 <see langword="true"/> 不存在为 <see langword="false"/></returns>
    public bool OnScreen();

    /// <summary>
    /// 判断是否在屏幕中
    /// <para>以(x, y)为左上角,至屏幕右下角</para>
    /// </summary>
    /// <param name="x">X坐标</param>
    /// <param name="y">Y坐标</param>
    /// <returns><see langword="true"/> 不存在为 <see langword="false"/></returns>
    public bool OnScreen(int x, int y);

    /// <summary>
    /// 判断是否在屏幕中
    /// <para>以(x, y)为左上角,至(x + widthSize, y + heightSize)</para>
    /// </summary>
    /// <param name="x">X坐标</param>
    /// <param name="y">Y坐标</param>
    /// <param name="widthSize">范围宽度</param>
    /// <param name="heightSize">范围高度</param>
    /// <returns><see langword="true"/> 不存在为 <see langword="false"/></returns>
    public bool OnScreen(int x, int y, int widthSize, int heightSize);
}
