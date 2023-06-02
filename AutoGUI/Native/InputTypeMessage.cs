using System;
using HKW.AutoGUI.Native;

namespace HKW.AutoGUI;

/// <summary>
/// 输入类型信息
/// </summary>
internal struct InputTypeMessage
{
    /// <summary>
    /// 输入类型
    /// </summary>
    public InputType Type;

    /// <summary>
    /// 输入消息
    /// </summary>
    public InputMessage Data;
}
