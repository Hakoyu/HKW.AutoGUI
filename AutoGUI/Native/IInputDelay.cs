using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HKW.AutoGUI.Native;

/// <summary>
/// 输入延迟接口
/// </summary>
/// <typeparam name="T">模拟基类</typeparam>
public interface IInputDelay<T>
    where T : class
{
    /// <summary>
    /// 执行睡眠, 产生延迟
    /// </summary>
    /// <param name="millseconds">延迟时间(毫秒)</param>
    /// <inheritdoc cref="Thread.Sleep(int)"/>
    ///
    public T Sleep(int millseconds);

    /// <summary>
    /// 执行睡眠, 产生延迟
    /// </summary>
    /// <param name="timeout">延迟时间</param>
    /// <inheritdoc cref="Thread.Sleep(TimeSpan)"/>
    public T Sleep(TimeSpan timeout);

    /// <summary>
    /// 执行异步延迟
    /// </summary>
    /// <param name="milliseconds">延迟时间(毫秒)</param>
    /// <inheritdoc cref="Task.Delay(int)"/>
    public Task<T> Delay(int milliseconds);

    /// <summary>
    /// 执行异步延迟
    /// </summary>
    /// <param name="timeout">延迟时间</param>
    /// <inheritdoc cref="Task.Delay(TimeSpan)"/>
    public Task<T> Delay(TimeSpan timeout);
}
