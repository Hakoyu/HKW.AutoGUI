namespace HKW.AutoGUI.Native
{
    /// <summary>
    /// Windows输入设备状态适配器
    /// </summary>
    public class WindowsInputDeviceStateAdaptor : IInputDeviceStateAdaptor
    {
        /// <inheritdoc/>
        public bool IsKeyDown(VirtualKeyCode keyCode)
        {
            var result = NativeMethods.GetKeyState(keyCode);
            return result < 0;
        }

        /// <inheritdoc/>
        public bool IsKeyUp(VirtualKeyCode keyCode)
        {
            return !IsKeyDown(keyCode);
        }

        /// <inheritdoc/>
        public bool IsHardwareKeyDown(VirtualKeyCode keyCode)
        {
            var result = NativeMethods.GetAsyncKeyState(keyCode);
            return result < 0;
        }

        /// <inheritdoc/>
        public bool IsHardwareKeyUp(VirtualKeyCode keyCode)
        {
            return !IsHardwareKeyDown(keyCode);
        }

        /// <inheritdoc/>
        public bool IsTogglingKeyInEffect(VirtualKeyCode keyCode)
        {
            var result = NativeMethods.GetKeyState(keyCode);
            return (result & 0x01) == 0x01;
        }
    }
}
