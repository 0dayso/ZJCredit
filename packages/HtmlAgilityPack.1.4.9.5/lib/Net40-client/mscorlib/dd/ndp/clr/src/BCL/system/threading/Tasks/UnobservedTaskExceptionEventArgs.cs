// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.UnobservedTaskExceptionEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  /// <summary>为在出错的 <see cref="T:System.Threading.Tasks.Task" /> 的异常未观察到时引发的事件提供数据。</summary>
  [__DynamicallyInvokable]
  public class UnobservedTaskExceptionEventArgs : EventArgs
  {
    private AggregateException m_exception;
    internal bool m_observed;

    /// <summary>获取此异常是否已标记为“已观察到”。</summary>
    /// <returns>如果此异常已标记为“已观察到”，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool Observed
    {
      [__DynamicallyInvokable] get
      {
        return this.m_observed;
      }
    }

    /// <summary>未观察到的异常。</summary>
    /// <returns>未观察到的异常。</returns>
    [__DynamicallyInvokable]
    public AggregateException Exception
    {
      [__DynamicallyInvokable] get
      {
        return this.m_exception;
      }
    }

    /// <summary>使用未观察到的异常初始化 <see cref="T:System.Threading.Tasks.UnobservedTaskExceptionEventArgs" /> 类的新实例。</summary>
    /// <param name="exception">未观察到的异常。</param>
    [__DynamicallyInvokable]
    public UnobservedTaskExceptionEventArgs(AggregateException exception)
    {
      this.m_exception = exception;
    }

    /// <summary>将 <see cref="P:System.Threading.Tasks.UnobservedTaskExceptionEventArgs.Exception" /> 标记为“已观察到”，这样将阻止该异常触发默认情况下会终止进程的异常升级策略。</summary>
    [__DynamicallyInvokable]
    public void SetObserved()
    {
      this.m_observed = true;
    }
  }
}
