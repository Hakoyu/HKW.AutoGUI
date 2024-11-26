namespace HKW.AutoGUI;

/// <summary>
/// 消息适配器
/// </summary>
public interface IInputMessageDispatcher
{
    /// <summary>
    /// 消息适配器
    /// </summary>
    /// <param name="inputMessage">消息</param>
    public void DispatchInput(object inputMessage);
}
