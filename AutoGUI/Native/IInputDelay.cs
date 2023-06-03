using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HKW.AutoGUI;

/// <summary>
/// 输入延迟接口
/// </summary>
/// <typeparam name="TInputSimulator">模拟基类</typeparam>
public interface IInputDelay<TInputSimulator>
    where TInputSimulator : IInputDelay<TInputSimulator>
{
    /// <summary>
    /// 执行睡眠, 产生延迟
    /// </summary>
    /// <param name="millseconds">延迟时间(毫秒)</param>
    /// <inheritdoc cref="Thread.Sleep(int)"/>
    ///
    public TInputSimulator Sleep(int millseconds);

    /// <summary>
    /// 执行睡眠, 产生延迟
    /// </summary>
    /// <param name="timeout">延迟时间</param>
    /// <inheritdoc cref="Thread.Sleep(TimeSpan)"/>
    public TInputSimulator Sleep(TimeSpan timeout);

    /// <summary>
    /// 执行异步延迟
    /// </summary>
    /// <param name="milliseconds">延迟时间(毫秒)</param>
    /// <inheritdoc cref="Task.Delay(int)"/>
    public Task<TInputSimulator> Delay(int milliseconds);

    /// <summary>
    /// 执行异步延迟
    /// </summary>
    /// <param name="timeout">延迟时间</param>
    /// <inheritdoc cref="Task.Delay(TimeSpan)"/>
    public Task<TInputSimulator> Delay(TimeSpan timeout);
}
