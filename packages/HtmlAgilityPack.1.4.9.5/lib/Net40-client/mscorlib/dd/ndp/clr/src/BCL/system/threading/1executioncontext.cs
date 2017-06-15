// Decompiled with JetBrains decompiler
// Type: System.Threading.AsyncFlowControl
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Threading
{
  /// <summary>提供功能以还原执行上下文在线程之间的迁移（或流动）。</summary>
  /// <filterpriority>2</filterpriority>
  public struct AsyncFlowControl : IDisposable
  {
    private bool useEC;
    private ExecutionContext _ec;
    private SecurityContext _sc;
    private Thread _thread;

    /// <summary>比较两个 <see cref="T:System.Threading.AsyncFlowControl" /> 结构以确定它们是否相等。</summary>
    /// <returns>如果两个结构相等，则为 true；否则为 false。</returns>
    /// <param name="a">一个 <see cref="T:System.Threading.AsyncFlowControl" /> 结构。</param>
    /// <param name="b">一个 <see cref="T:System.Threading.AsyncFlowControl" /> 结构。</param>
    public static bool operator ==(AsyncFlowControl a, AsyncFlowControl b)
    {
      return a.Equals(b);
    }

    /// <summary>比较两个 <see cref="T:System.Threading.AsyncFlowControl" /> 结构以确定它们是否相等。</summary>
    /// <returns>如果两个结构不相等，则为 true；否则为 false。</returns>
    /// <param name="a">一个 <see cref="T:System.Threading.AsyncFlowControl" /> 结构。</param>
    /// <param name="b">一个 <see cref="T:System.Threading.AsyncFlowControl" /> 结构。</param>
    public static bool operator !=(AsyncFlowControl a, AsyncFlowControl b)
    {
      return !(a == b);
    }

    [SecurityCritical]
    internal void Setup(SecurityContextDisableFlow flags)
    {
      this.useEC = false;
      Thread currentThread = Thread.CurrentThread;
      this._sc = currentThread.GetMutableExecutionContext().SecurityContext;
      this._sc._disableFlow = flags;
      this._thread = currentThread;
    }

    [SecurityCritical]
    internal void Setup()
    {
      this.useEC = true;
      Thread currentThread = Thread.CurrentThread;
      this._ec = currentThread.GetMutableExecutionContext();
      this._ec.isFlowSuppressed = true;
      this._thread = currentThread;
    }

    /// <summary>释放由 <see cref="T:System.Threading.AsyncFlowControl" /> 类的当前实例占用的所有资源。</summary>
    /// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Threading.AsyncFlowControl" /> structure is not used on the thread where it was created.-or-The <see cref="T:System.Threading.AsyncFlowControl" /> structure has already been used to call <see cref="M:System.Threading.AsyncFlowControl.Dispose" /> or <see cref="M:System.Threading.AsyncFlowControl.Undo" />.</exception>
    public void Dispose()
    {
      this.Undo();
    }

    /// <summary>还原执行上下文在线程之间的流动。</summary>
    /// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Threading.AsyncFlowControl" /> structure is not used on the thread where it was created.-or-The <see cref="T:System.Threading.AsyncFlowControl" /> structure has already been used to call <see cref="M:System.Threading.AsyncFlowControl.Dispose" /> or <see cref="M:System.Threading.AsyncFlowControl.Undo" />.</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public void Undo()
    {
      if (this._thread == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotUseAFCMultiple"));
      if (this._thread != Thread.CurrentThread)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotUseAFCOtherThread"));
      if (this.useEC)
      {
        if (Thread.CurrentThread.GetMutableExecutionContext() != this._ec)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AsyncFlowCtrlCtxMismatch"));
        ExecutionContext.RestoreFlow();
      }
      else
      {
        if (!Thread.CurrentThread.GetExecutionContextReader().SecurityContext.IsSame(this._sc))
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AsyncFlowCtrlCtxMismatch"));
        SecurityContext.RestoreFlow();
      }
      this._thread = (Thread) null;
    }

    /// <summary>获取当前 <see cref="T:System.Threading.AsyncFlowControl" /> 结构的哈希代码。</summary>
    /// <returns>当前 <see cref="T:System.Threading.AsyncFlowControl" /> 结构的哈希代码。</returns>
    public override int GetHashCode()
    {
      if (this._thread != null)
        return this._thread.GetHashCode();
      return this.ToString().GetHashCode();
    }

    /// <summary>确定指定的对象是否等于当前的 <see cref="T:System.Threading.AsyncFlowControl" /> 结构。</summary>
    /// <returns>如果 <paramref name="obj" /> 是一个 <see cref="T:System.Threading.AsyncFlowControl" /> 结构并且等于当前 <see cref="T:System.Threading.AsyncFlowControl" /> 结构，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前结构进行比较的对象。</param>
    public override bool Equals(object obj)
    {
      if (obj is AsyncFlowControl)
        return this.Equals((AsyncFlowControl) obj);
      return false;
    }

    /// <summary>确定指定的 <see cref="T:System.Threading.AsyncFlowControl" /> 结构是否等于当前的 <see cref="T:System.Threading.AsyncFlowControl" /> 结构。</summary>
    /// <returns>如果 <paramref name="obj" /> 等于当前的 <see cref="T:System.Threading.AsyncFlowControl" /> 结构，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前结构进行比较的 <see cref="T:System.Threading.AsyncFlowControl" /> 结构。</param>
    public bool Equals(AsyncFlowControl obj)
    {
      if (obj.useEC == this.useEC && obj._ec == this._ec && obj._sc == this._sc)
        return obj._thread == this._thread;
      return false;
    }
  }
}
