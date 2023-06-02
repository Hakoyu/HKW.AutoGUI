using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using HKW.AutoGUI.Native;

namespace HKW.AutoGUI;

/// <summary>
/// 键盘模拟接口
/// </summary>
public interface IKeyboardSimulator : IInputDelay<IKeyboardSimulator>
{
    /// <summary>
    /// 鼠标
    /// </summary>
    public IMouseSimulator Mouse { get; }

    /// <summary>
    /// 按下的按键
    /// </summary>
    public IReadOnlySet<VirtualKeyCode> DownedKeys { get; }

    /// <summary>
    /// 按下按键
    /// </summary>
    /// <param name="keyCode">键码</param>
    /// <param name="record">若为 <see langword="true"/> 则 <paramref name="keyCode"/> 会被添加至 <see cref="DownedKeys"/> 中</param>
    public IKeyboardSimulator KeyDown(VirtualKeyCode keyCode, bool record = true);

    /// <summary>
    /// 按下多个按键 (默认添加至 <see cref="DownedKeys"/> 中)
    /// </summary>
    /// <param name="keyCodes">多个键码</param>
    public IKeyboardSimulator KeyDown(params VirtualKeyCode[] keyCodes);

    /// <summary>
    /// 按下多个按键
    /// </summary>
    /// <param name="keyCodes">多个键码</param>
    /// <param name="record">若为 <see langword="true"/> 则 <paramref name="keyCodes"/> 会被添加至 <see cref="DownedKeys"/> 中</param>
    public IKeyboardSimulator KeyDown(bool record, params VirtualKeyCode[] keyCodes);

    /// <summary>
    /// 释放在 <see cref="DownedKeys"/> 中被记录的按键, 并清空记录
    /// </summary>
    public IKeyboardSimulator KeyUp();

    /// <summary>
    /// 释放按键
    /// </summary>
    /// <param name="keyCode">键码 <para>会从 <see cref="DownedKeys"/> 中删除</para></param>
    public IKeyboardSimulator KeyUp(VirtualKeyCode keyCode);

    /// <summary>
    /// 释放多个按键
    /// </summary>
    /// <param name="keyCodes">多个键码  <para>会从 <see cref="DownedKeys"/> 中删除</para></param>
    public IKeyboardSimulator KeyUp(params VirtualKeyCode[] keyCodes);

    /// <summary>
    /// 按键点击
    /// </summary>
    /// <param name="keyCode">键码</param>
    public IKeyboardSimulator KeyPress(VirtualKeyCode keyCode);

    /// <summary>
    /// 点击多个按键
    /// </summary>
    /// <param name="keyCodes">多个键码</param>
    public IKeyboardSimulator KeyPress(params VirtualKeyCode[] keyCodes);

    /// <summary>
    /// 模拟输入文本
    /// </summary>
    /// <param name="text">文本</param>
    public IKeyboardSimulator TextEntry(string text);

    /// <summary>
    /// 模拟输入字符
    /// </summary>
    /// <param name="character">字符</param>
    public IKeyboardSimulator TextEntry(char character);
}
