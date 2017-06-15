// Decompiled with JetBrains decompiler
// Type: System.Threading.CancellationToken
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>传播有关应取消操作的通知。</summary>
  [ComVisible(false)]
  [DebuggerDisplay("IsCancellationRequested = {IsCancellationRequested}")]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public struct CancellationToken
  {
    private static readonly Action<object> s_ActionToActionObjShunt = new Action<object>(CancellationToken.ActionToActionObjShunt);
    private CancellationTokenSource m_source;

    /// <summary>返回一个空 <see cref="T:System.Threading.CancellationToken" /> 值。</summary>
    /// <returns>一个空取消标记。</returns>
    [__DynamicallyInvokable]
    public static CancellationToken None
    {
      [__DynamicallyInvokable] get
      {
        return new CancellationToken();
      }
    }

    /// <summary>获取是否已请求取消此标记。</summary>
    /// <returns>如果已请求取消此标记，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsCancellationRequested
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_source != null)
          return this.m_source.IsCancellationRequested;
        return false;
      }
    }

    /// <summary>获取此标记是否能处于已取消状态。</summary>
    /// <returns>如果此标记能处于已取消状态，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool CanBeCanceled
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_source != null)
          return this.m_source.CanBeCanceled;
        return false;
      }
    }

    /// <summary>获取在取消标记时收到信号的 <see cref="T:System.Threading.WaitHandle" />。</summary>
    /// <returns>在取消标记时收到信号的 <see cref="T:System.Threading.WaitHandle" />。</returns>
    /// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
    [__DynamicallyInvokable]
    public WaitHandle WaitHandle
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_source == null)
          this.InitializeDefaultSource();
        return this.m_source.WaitHandle;
      }
    }

    internal CancellationToken(CancellationTokenSource source)
    {
      this.m_source = source;
    }

    /// <summary>初始化 <see cref="T:System.Threading.CancellationToken" />。</summary>
    /// <param name="canceled">标记的已取消状态。</param>
    [__DynamicallyInvokable]
    public CancellationToken(bool canceled)
    {
      this = new CancellationToken();
      if (!canceled)
        return;
      this.m_source = CancellationTokenSource.InternalGetStaticSource(canceled);
    }

    /// <summary>确定两个 <see cref="T:System.Threading.CancellationToken" /> 实例是否相等。</summary>
    /// <returns>如果两个实例相等，则为 true；否则为 false。</returns>
    /// <param name="left">第一个实例。</param>
    /// <param name="right">第二个实例。</param>
    /// <exception cref="T:System.ObjectDisposedException">An associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
    [__DynamicallyInvokable]
    public static bool operator ==(CancellationToken left, CancellationToken right)
    {
      return left.Equals(right);
    }

    /// <summary>确定两个 <see cref="T:System.Threading.CancellationToken" /> 实例是否不相等。</summary>
    /// <returns>如果实例不相等，则为 true；否则为 false。</returns>
    /// <param name="left">第一个实例。</param>
    /// <param name="right">第二个实例。</param>
    /// <exception cref="T:System.ObjectDisposedException">An associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
    [__DynamicallyInvokable]
    public static bool operator !=(CancellationToken left, CancellationToken right)
    {
      return !left.Equals(right);
    }

    private static void ActionToActionObjShunt(object obj)
    {
      (obj as Action)();
    }

    /// <summary>注册一个将在取消此 <see cref="T:System.Threading.CancellationToken" /> 时调用的委托。</summary>
    /// <returns>可用于取消注册回调的 <see cref="T:System.Threading.CancellationTokenRegistration" /> 实例。</returns>
    /// <param name="callback">要在取消 <see cref="T:System.Threading.CancellationToken" /> 时执行的委托。</param>
    /// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="callback" /> is null.</exception>
    [__DynamicallyInvokable]
    public CancellationTokenRegistration Register(Action callback)
    {
      if (callback == null)
        throw new ArgumentNullException("callback");
      return this.Register(CancellationToken.s_ActionToActionObjShunt, (object) callback, false, true);
    }

    /// <summary>注册一个将在取消此 <see cref="T:System.Threading.CancellationToken" /> 时调用的委托。</summary>
    /// <returns>可用于取消注册回调的 <see cref="T:System.Threading.CancellationTokenRegistration" /> 实例。</returns>
    /// <param name="callback">要在取消 <see cref="T:System.Threading.CancellationToken" /> 时执行的委托。</param>
    /// <param name="useSynchronizationContext">一个布尔值，该值指示是否捕获当前 <see cref="T:System.Threading.SynchronizationContext" /> 并在调用 <paramref name="callback" /> 时使用它。</param>
    /// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="callback" /> is null.</exception>
    [__DynamicallyInvokable]
    public CancellationTokenRegistration Register(Action callback, bool useSynchronizationContext)
    {
      if (callback == null)
        throw new ArgumentNullException("callback");
      return this.Register(CancellationToken.s_ActionToActionObjShunt, (object) callback, useSynchronizationContext, true);
    }

    /// <summary>注册一个将在取消此 <see cref="T:System.Threading.CancellationToken" /> 时调用的委托。</summary>
    /// <returns>可用于取消注册回调的 <see cref="T:System.Threading.CancellationTokenRegistration" /> 实例。</returns>
    /// <param name="callback">要在取消 <see cref="T:System.Threading.CancellationToken" /> 时执行的委托。</param>
    /// <param name="state">要在调用委托时传递给 <paramref name="callback" /> 的状态。这可能为 null。</param>
    /// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="callback" /> is null.</exception>
    [__DynamicallyInvokable]
    public CancellationTokenRegistration Register(Action<object> callback, object state)
    {
      if (callback == null)
        throw new ArgumentNullException("callback");
      return this.Register(callback, state, false, true);
    }

    /// <summary>注册一个将在取消此 <see cref="T:System.Threading.CancellationToken" /> 时调用的委托。</summary>
    /// <returns>可用于取消注册回调的 <see cref="T:System.Threading.CancellationTokenRegistration" /> 实例。</returns>
    /// <param name="callback">要在取消 <see cref="T:System.Threading.CancellationToken" /> 时执行的委托。</param>
    /// <param name="state">要在调用委托时传递给 <paramref name="callback" /> 的状态。这可能为 null。</param>
    /// <param name="useSynchronizationContext">一个布尔值，该值指示是否捕获当前 <see cref="T:System.Threading.SynchronizationContext" /> 并在调用 <paramref name="callback" /> 时使用它。</param>
    /// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="callback" /> is null.</exception>
    [__DynamicallyInvokable]
    public CancellationTokenRegistration Register(Action<object> callback, object state, bool useSynchronizationContext)
    {
      return this.Register(callback, state, useSynchronizationContext, true);
    }

    internal CancellationTokenRegistration InternalRegisterWithoutEC(Action<object> callback, object state)
    {
      return this.Register(callback, state, false, false);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private CancellationTokenRegistration Register(Action<object> callback, object state, bool useSynchronizationContext, bool useExecutionContext)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      if (callback == null)
        throw new ArgumentNullException("callback");
      if (!this.CanBeCanceled)
        return new CancellationTokenRegistration();
      SynchronizationContext targetSyncContext = (SynchronizationContext) null;
      ExecutionContext executionContext = (ExecutionContext) null;
      if (!this.IsCancellationRequested)
      {
        if (useSynchronizationContext)
          targetSyncContext = SynchronizationContext.Current;
        if (useExecutionContext)
          executionContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.OptimizeDefaultCase);
      }
      return this.m_source.InternalRegister(callback, state, targetSyncContext, executionContext);
    }

    /// <summary>确定当前的 <see cref="T:System.Threading.CancellationToken" /> 实例是否等于指定的标记。</summary>
    /// <returns>如果两个实例相等，则为 true；否则为 false。如果两个标记与同一 <see cref="T:System.Threading.CancellationTokenSource" /> 关联，或者它们均是根据公共 CancellationToken 构造函数构造并且其 <see cref="P:System.Threading.CancellationToken.IsCancellationRequested" /> 值相等，则两个标记相等。</returns>
    /// <param name="other">要与此实例进行比较的另一个 <see cref="T:System.Threading.CancellationToken" />。</param>
    [__DynamicallyInvokable]
    public bool Equals(CancellationToken other)
    {
      if (this.m_source == null && other.m_source == null)
        return true;
      if (this.m_source == null)
        return other.m_source == CancellationTokenSource.InternalGetStaticSource(false);
      if (other.m_source == null)
        return this.m_source == CancellationTokenSource.InternalGetStaticSource(false);
      return this.m_source == other.m_source;
    }

    /// <summary>确定当前的 <see cref="T:System.Threading.CancellationToken" /> 实例是否等于指定的 <see cref="T:System.Object" />。</summary>
    /// <returns>如果 <paramref name="other" /> 为 <see cref="T:System.Threading.CancellationToken" /> 并且两个实例相等，则为 true；否则为 false。如果两个标记与同一 <see cref="T:System.Threading.CancellationTokenSource" /> 关联，或者它们均是根据公共 CancellationToken 构造函数构造并且其 <see cref="P:System.Threading.CancellationToken.IsCancellationRequested" /> 值相等，则两个标记相等。</returns>
    /// <param name="other">要与此实例进行比较的其他对象。</param>
    /// <exception cref="T:System.ObjectDisposedException">An associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
    [__DynamicallyInvokable]
    public override bool Equals(object other)
    {
      if (other is CancellationToken)
        return this.Equals((CancellationToken) other);
      return false;
    }

    /// <summary>作为 <see cref="T:System.Threading.CancellationToken" /> 的哈希函数。</summary>
    /// <returns>当前 <see cref="T:System.Threading.CancellationToken" /> 实例的哈希代码。</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      if (this.m_source == null)
        return CancellationTokenSource.InternalGetStaticSource(false).GetHashCode();
      return this.m_source.GetHashCode();
    }

    /// <summary>如果已请求取消此标记，则引发 <see cref="T:System.OperationCanceledException" />。</summary>
    /// <exception cref="T:System.OperationCanceledException">The token has had cancellation requested.</exception>
    /// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
    [__DynamicallyInvokable]
    public void ThrowIfCancellationRequested()
    {
      if (!this.IsCancellationRequested)
        return;
      this.ThrowOperationCanceledException();
    }

    internal void ThrowIfSourceDisposed()
    {
      if (this.m_source == null || !this.m_source.IsDisposed)
        return;
      CancellationToken.ThrowObjectDisposedException();
    }

    private void ThrowOperationCanceledException()
    {
      throw new OperationCanceledException(Environment.GetResourceString("OperationCanceled"), this);
    }

    private static void ThrowObjectDisposedException()
    {
      throw new ObjectDisposedException((string) null, Environment.GetResourceString("CancellationToken_SourceDisposed"));
    }

    private void InitializeDefaultSource()
    {
      this.m_source = CancellationTokenSource.InternalGetStaticSource(false);
    }
  }
}
