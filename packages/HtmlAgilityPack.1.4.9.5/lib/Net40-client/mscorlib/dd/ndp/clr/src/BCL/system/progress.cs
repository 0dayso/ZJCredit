// Decompiled with JetBrains decompiler
// Type: System.Progress`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Threading;

namespace System
{
  /// <summary>提供调用每个报告进度的值的回调的 <see cref="T:System.IProgress`1" /> 。</summary>
  /// <typeparam name="T">指定进度报表值的类型。</typeparam>
  [__DynamicallyInvokable]
  public class Progress<T> : IProgress<T>
  {
    private readonly SynchronizationContext m_synchronizationContext;
    private readonly Action<T> m_handler;
    private readonly SendOrPostCallback m_invokeHandlers;

    /// <summary>为每个报告进度的值引发。</summary>
    [__DynamicallyInvokable]
    public event EventHandler<T> ProgressChanged;

    /// <summary>初始化 <see cref="T:System.Progress`1" /> 对象。</summary>
    [__DynamicallyInvokable]
    public Progress()
    {
      this.m_synchronizationContext = SynchronizationContext.CurrentNoFlow ?? ProgressStatics.DefaultContext;
      this.m_invokeHandlers = new SendOrPostCallback(this.InvokeHandlers);
    }

    /// <summary>用指定的回调初始化 <see cref="T:System.Progress`1" /> 对象。</summary>
    /// <param name="handler">为每个报告的进度值调用处理程序。该处理程序会调用除了任何委托 <see cref="E:System.Progress`1.ProgressChanged" /> 事件注册。根据 <see cref="T:System.Threading.SynchronizationContext" /> 实例， <see cref="T:System.Progress`1" /> 在构造时所捕获的实例, 该处理程序实例很有可能同时调用自身。</param>
    [__DynamicallyInvokable]
    public Progress(Action<T> handler)
      : this()
    {
      if (handler == null)
        throw new ArgumentNullException("handler");
      this.m_handler = handler;
    }

    /// <summary>报告进度更改。</summary>
    /// <param name="value">进度更新之后的值。</param>
    [__DynamicallyInvokable]
    protected virtual void OnReport(T value)
    {
      // ISSUE: reference to a compiler-generated field
      if (this.m_handler == null && this.ProgressChanged == null)
        return;
      this.m_synchronizationContext.Post(this.m_invokeHandlers, (object) value);
    }

    [__DynamicallyInvokable]
    void IProgress<T>.Report(T value)
    {
      this.OnReport(value);
    }

    private void InvokeHandlers(object state)
    {
      T e = (T) state;
      Action<T> action = this.m_handler;
      // ISSUE: reference to a compiler-generated field
      EventHandler<T> eventHandler = this.ProgressChanged;
      if (action != null)
        action(e);
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }
  }
}
