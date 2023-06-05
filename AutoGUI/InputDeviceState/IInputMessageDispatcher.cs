using HKW.AutoGUI.Native.Windows;

namespace HKW.AutoGUI.InputDeviceState;

/// <summary>
/// 消息适配器
/// </summary>
internal interface IInputMessageDispatcher
{
    /// <summary>
    /// 消息适配器
    /// </summary>
    /// <param name="pause">停顿</param>
    /// <param name="input">消息构造器</param>
    void DispatchInput(int pause, InputBuilder input);
}
