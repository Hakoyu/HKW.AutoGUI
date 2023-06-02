namespace HKW.AutoGUI.Native;

/// <summary>
/// 输入设备状态适配器接口
/// </summary>
public interface IInputDeviceStateAdaptor
{
    /// <summary>
    /// 确认按键按下
    /// </summary>
    /// <param name="keyCode">键码</param>
    /// <returns>按下为 <see langword="true"/> 否则为 <see langword="false"/></returns>
    bool IsKeyDown(VirtualKeyCode keyCode);

    /// <summary>
    /// 确认按键释放
    /// </summary>
    /// <param name="keyCode">键码</param>
    /// <returns>释放为 <see langword="true"/> 否则为 <see langword="false"/></returns>
    bool IsKeyUp(VirtualKeyCode keyCode);

    /// <summary>
    /// 确认物理按键是否被按下
    /// </summary>
    /// <param name="keyCode">键码</param>
    /// <returns>按下为 <see langword="true"/> 否则为 <see langword="false"/></returns>
    bool IsHardwareKeyDown(VirtualKeyCode keyCode);

    /// <summary>
    /// 确认物理按键释放被释放
    /// </summary>
    /// <param name="keyCode">键码</param>
    /// <returns>释放为 <see langword="true"/> 否则为 <see langword="false"/></returns>
    bool IsHardwareKeyUp(VirtualKeyCode keyCode);

    /// <summary>
    /// 确认切换键是否生效
    /// <para>例如: <see cref="VirtualKeyCode.CAPITAL"/>, <see cref="VirtualKeyCode.NUMLOCK"/></para>
    /// </summary>
    /// <param name="keyCode">键码</param>
    /// <returns>生效为 <see langword="true"/> 否则为 <see langword="false"/></returns>
    bool IsTogglingKeyInEffect(VirtualKeyCode keyCode);
}
