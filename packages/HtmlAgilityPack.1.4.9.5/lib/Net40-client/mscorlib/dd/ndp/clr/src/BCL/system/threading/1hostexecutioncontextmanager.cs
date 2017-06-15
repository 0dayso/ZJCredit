// Decompiled with JetBrains decompiler
// Type: System.Threading.HostExecutionContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Threading
{
  /// <summary>在线程之间封装并传播宿主执行上下文。</summary>
  /// <filterpriority>2</filterpriority>
  public class HostExecutionContext : IDisposable
  {
    private object state;

    /// <summary>获取或设置宿主执行上下文的状态。</summary>
    /// <returns>一个表示宿主执行上下文状态的对象。</returns>
    protected internal object State
    {
      get
      {
        return this.state;
      }
      set
      {
        this.state = value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Threading.HostExecutionContext" /> 类的新实例。</summary>
    public HostExecutionContext()
    {
    }

    /// <summary>使用指定的状态初始化 <see cref="T:System.Threading.HostExecutionContext" /> 类的新实例。</summary>
    /// <param name="state">一个表示宿主执行上下文状态的对象。</param>
    public HostExecutionContext(object state)
    {
      this.state = state;
    }

    /// <summary>创建当前宿主执行上下文的副本。</summary>
    /// <returns>一个 <see cref="T:System.Threading.HostExecutionContext" /> 对象，表示当前线程的宿主上下文。</returns>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public virtual HostExecutionContext CreateCopy()
    {
      object obj = this.state;
      if (this.state is IUnknownSafeHandle)
        ((IUnknownSafeHandle) this.state).Clone();
      return new HostExecutionContext(this.state);
    }

    /// <summary>释放由 <see cref="T:System.Threading.HostExecutionContext" /> 类的当前实例占用的所有资源。</summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>在派生类中被重写时，释放由 <see cref="T:System.Threading.WaitHandle" /> 使用的非托管资源，也可以根据需要释放托管资源。</summary>
    /// <param name="disposing">true 表示释放托管资源和非托管资源；false 表示仅释放非托管资源。</param>
    public virtual void Dispose(bool disposing)
    {
    }
  }
}
