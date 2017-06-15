// Decompiled with JetBrains decompiler
// Type: System.Threading.SpinWait
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>提供对基于自旋的等待的支持。</summary>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public struct SpinWait
  {
    internal const int YIELD_THRESHOLD = 10;
    internal const int SLEEP_0_EVERY_HOW_MANY_TIMES = 5;
    internal const int SLEEP_1_EVERY_HOW_MANY_TIMES = 20;
    private int m_count;

    /// <summary>获取已对此实例调用 <see cref="M:System.Threading.SpinWait.SpinOnce" /> 的次数。</summary>
    /// <returns>返回一个整数，该整数表示已对此实例调用 <see cref="M:System.Threading.SpinWait.SpinOnce" /> 的次数。</returns>
    [__DynamicallyInvokable]
    public int Count
    {
      [__DynamicallyInvokable] get
      {
        return this.m_count;
      }
    }

    /// <summary>获取对 <see cref="M:System.Threading.SpinWait.SpinOnce" /> 的下一次调用是否将产生处理器，同时触发强制上下文切换。</summary>
    /// <returns>对 <see cref="M:System.Threading.SpinWait.SpinOnce" /> 的下一次调用是否将产生处理器，同时触发强制上下文切换。</returns>
    [__DynamicallyInvokable]
    public bool NextSpinWillYield
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_count <= 10)
          return PlatformHelper.IsSingleProcessor;
        return true;
      }
    }

    /// <summary>执行单一自旋。</summary>
    [__DynamicallyInvokable]
    public void SpinOnce()
    {
      if (this.NextSpinWillYield)
      {
        CdsSyncEtwBCLProvider.Log.SpinWait_NextSpinWillYield();
        int num = this.m_count >= 10 ? this.m_count - 10 : this.m_count;
        if (num % 20 == 19)
          Thread.Sleep(1);
        else if (num % 5 == 4)
          Thread.Sleep(0);
        else
          Thread.Yield();
      }
      else
        Thread.SpinWait(4 << this.m_count);
      this.m_count = this.m_count == int.MaxValue ? 10 : this.m_count + 1;
    }

    /// <summary>重置自旋计数器。</summary>
    [__DynamicallyInvokable]
    public void Reset()
    {
      this.m_count = 0;
    }

    /// <summary>在指定条件得到满足之前自旋。</summary>
    /// <param name="condition">在返回 true 之前重复执行的委托。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="condition" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    public static void SpinUntil(Func<bool> condition)
    {
      SpinWait.SpinUntil(condition, -1);
    }

    /// <summary>在指定条件得到满足或指定超时过期之前自旋。</summary>
    /// <returns>如果条件在超时时间内得到满足，则为 true；否则为 false</returns>
    /// <param name="condition">在返回 true 之前重复执行的委托。</param>
    /// <param name="timeout">一个 <see cref="T:System.TimeSpan" />，表示等待的毫秒数；或者一个 TimeSpan，表示 -1 毫秒（无限期等待）。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="condition" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="timeout" /> 是 -1 毫秒之外的负数，表示无限超时或者超时大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public static bool SpinUntil(Func<bool> condition, TimeSpan timeout)
    {
      long num = (long) timeout.TotalMilliseconds;
      if (num < -1L || num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("timeout", (object) timeout, Environment.GetResourceString("SpinWait_SpinUntil_TimeoutWrong"));
      return SpinWait.SpinUntil(condition, (int) timeout.TotalMilliseconds);
    }

    /// <summary>在指定条件得到满足或指定超时过期之前自旋。</summary>
    /// <returns>如果条件在超时时间内得到满足，则为 true；否则为 false</returns>
    /// <param name="condition">在返回 true 之前重复执行的委托。</param>
    /// <param name="millisecondsTimeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="condition" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> 是一个非 -1 的负数，而 -1 表示无限期超时。</exception>
    [__DynamicallyInvokable]
    public static bool SpinUntil(Func<bool> condition, int millisecondsTimeout)
    {
      if (millisecondsTimeout < -1)
        throw new ArgumentOutOfRangeException("millisecondsTimeout", (object) millisecondsTimeout, Environment.GetResourceString("SpinWait_SpinUntil_TimeoutWrong"));
      if (condition == null)
        throw new ArgumentNullException("condition", Environment.GetResourceString("SpinWait_SpinUntil_ArgumentNull"));
      uint num = 0;
      if (millisecondsTimeout != 0 && millisecondsTimeout != -1)
        num = TimeoutHelper.GetTime();
      SpinWait spinWait = new SpinWait();
      while (!condition())
      {
        if (millisecondsTimeout == 0)
          return false;
        spinWait.SpinOnce();
        if (millisecondsTimeout != -1 && spinWait.NextSpinWillYield && (long) millisecondsTimeout <= (long) (TimeoutHelper.GetTime() - num))
          return false;
      }
      return true;
    }
  }
}
