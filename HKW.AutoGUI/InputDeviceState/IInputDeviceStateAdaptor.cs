namespace HKW.AutoGUI;

/// <summary>
/// 输入设备状态适配器接口
/// </summary>
public interface IInputDeviceStateAdaptor<TKeyCode>
{
    /// <summary>
    /// 检查按键按下
    /// </summary>
    /// <param name="keyCode">键码</param>
    /// <returns>按下为 <see langword="true"/> 否则为 <see langword="false"/></returns>
    public bool CheckKeyDown(TKeyCode keyCode);

    /// <summary>
    /// 检查按键释放
    /// </summary>
    /// <param name="keyCode">键码</param>
    /// <returns>释放为 <see langword="true"/> 否则为 <see langword="false"/></returns>
    public bool CheckKeyUp(TKeyCode keyCode);

    /// <summary>
    /// 检查物理按键是否被按下
    /// </summary>
    /// <param name="keyCode">键码</param>
    /// <returns>按下为 <see langword="true"/> 否则为 <see langword="false"/></returns>
    public bool CheckHardwareKeyDown(TKeyCode keyCode);

    /// <summary>
    /// 检查物理按键释放被释放
    /// </summary>
    /// <param name="keyCode">键码</param>
    /// <returns>释放为 <see langword="true"/> 否则为 <see langword="false"/></returns>
    public bool CheckHardwareKeyUp(TKeyCode keyCode);

    /// <summary>
    /// 检查切换键是否生效
    /// <para>例如: <see langword="CAPITAL"/>, <see langword="NUMLOCK"/></para>
    /// </summary>
    /// <param name="keyCode">键码</param>
    /// <returns>生效为 <see langword="true"/> 否则为 <see langword="false"/></returns>
    public bool CheckTogglingKeyInEffect(TKeyCode keyCode);
}
