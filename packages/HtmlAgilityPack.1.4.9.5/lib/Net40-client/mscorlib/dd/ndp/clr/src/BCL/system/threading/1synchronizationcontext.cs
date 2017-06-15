// Decompiled with JetBrains decompiler
// Type: System.Threading.SynchronizationContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>提供在各种同步模型中传播同步上下文的基本功能。</summary>
  /// <filterpriority>2</filterpriority>
  [__DynamicallyInvokable]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)]
  public class SynchronizationContext
  {
    private SynchronizationContextProperties _props;
    private static Type s_cachedPreparedType1;
    private static Type s_cachedPreparedType2;
    private static Type s_cachedPreparedType3;
    private static Type s_cachedPreparedType4;
    private static Type s_cachedPreparedType5;
    [SecurityCritical]
    private static WinRTSynchronizationContextFactoryBase s_winRTContextFactory;

    /// <summary>获取当前线程的同步上下文。</summary>
    /// <returns>一个 <see cref="T:System.Threading.SynchronizationContext" /> 对象，它表示当前同步上下文。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static SynchronizationContext Current
    {
      [__DynamicallyInvokable] get
      {
        return Thread.CurrentThread.GetExecutionContextReader().SynchronizationContext ?? SynchronizationContext.GetThreadLocalContext();
      }
    }

    internal static SynchronizationContext CurrentNoFlow
    {
      [FriendAccessAllowed] get
      {
        return Thread.CurrentThread.GetExecutionContextReader().SynchronizationContextNoFlow ?? SynchronizationContext.GetThreadLocalContext();
      }
    }

    /// <summary>创建 <see cref="T:System.Threading.SynchronizationContext" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public SynchronizationContext()
    {
    }

    /// <summary>设置指示需要等待通知的通知，并准备回调方法以使其在发生等待时可以更可靠地被调用。</summary>
    [SecuritySafeCritical]
    protected void SetWaitNotificationRequired()
    {
      Type type = this.GetType();
      if (SynchronizationContext.s_cachedPreparedType1 != type && SynchronizationContext.s_cachedPreparedType2 != type && (SynchronizationContext.s_cachedPreparedType3 != type && SynchronizationContext.s_cachedPreparedType4 != type) && SynchronizationContext.s_cachedPreparedType5 != type)
      {
        RuntimeHelpers.PrepareDelegate((Delegate) new SynchronizationContext.WaitDelegate(this.Wait));
        if (SynchronizationContext.s_cachedPreparedType1 == (Type) null)
          SynchronizationContext.s_cachedPreparedType1 = type;
        else if (SynchronizationContext.s_cachedPreparedType2 == (Type) null)
          SynchronizationContext.s_cachedPreparedType2 = type;
        else if (SynchronizationContext.s_cachedPreparedType3 == (Type) null)
          SynchronizationContext.s_cachedPreparedType3 = type;
        else if (SynchronizationContext.s_cachedPreparedType4 == (Type) null)
          SynchronizationContext.s_cachedPreparedType4 = type;
        else if (SynchronizationContext.s_cachedPreparedType5 == (Type) null)
          SynchronizationContext.s_cachedPreparedType5 = type;
      }
      this._props = this._props | SynchronizationContextProperties.RequireWaitNotification;
    }

    /// <summary>确定是否需要等待通知。</summary>
    /// <returns>如果需要等待通知，则为 true；否则为 false。</returns>
    public bool IsWaitNotificationRequired()
    {
      return (uint) (this._props & SynchronizationContextProperties.RequireWaitNotification) > 0U;
    }

    /// <summary>在派生类中重写时，将同步消息分派到同步上下文。</summary>
    /// <param name="d">要调用的 <see cref="T:System.Threading.SendOrPostCallback" /> 委托。</param>
    /// <param name="state">传递给委托的对象。</param>
    /// <exception cref="T:System.NotSupportedException">The method was called in a Windows Store app.The implementation of <see cref="T:System.Threading.SynchronizationContext" /> for Windows Store apps does not support the <see cref="M:System.Threading.SynchronizationContext.Send(System.Threading.SendOrPostCallback,System.Object)" /> method.</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Send(SendOrPostCallback d, object state)
    {
      d(state);
    }

    /// <summary>在派生类中重写时，将异步消息分派到同步上下文。</summary>
    /// <param name="d">要调用的 <see cref="T:System.Threading.SendOrPostCallback" /> 委托。</param>
    /// <param name="state">传递给委托的对象。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual void Post(SendOrPostCallback d, object state)
    {
      ThreadPool.QueueUserWorkItem(new WaitCallback(d.Invoke), state);
    }

    /// <summary>在派生类中重写时，响应操作已开始的通知。</summary>
    [__DynamicallyInvokable]
    public virtual void OperationStarted()
    {
    }

    /// <summary>在派生类中重写时，响应操作已完成的通知。</summary>
    [__DynamicallyInvokable]
    public virtual void OperationCompleted()
    {
    }

    /// <summary>等待指定数组中的任一元素或所有元素接收信号。</summary>
    /// <returns>满足等待的对象的数组索引。</returns>
    /// <param name="waitHandles">一个类型为 <see cref="T:System.IntPtr" /> 的数组，其中包含本机操作系统句柄。</param>
    /// <param name="waitAll">如果等待所有句柄，则为 true；如果等待任一句柄，则为 false。</param>
    /// <param name="millisecondsTimeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="waitHandles" /> is null.</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecurityCritical]
    [CLSCompliant(false)]
    [PrePrepareMethod]
    public virtual int Wait(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)
    {
      if (waitHandles == null)
        throw new ArgumentNullException("waitHandles");
      return SynchronizationContext.WaitHelper(waitHandles, waitAll, millisecondsTimeout);
    }

    /// <summary>用于等待指定数组中的任一元素或所有元素接收信号的 Helper 函数。</summary>
    /// <returns>满足等待的对象的数组索引。</returns>
    /// <param name="waitHandles">一个类型为 <see cref="T:System.IntPtr" /> 的数组，其中包含本机操作系统句柄。</param>
    /// <param name="waitAll">如果等待所有句柄，则为 true；如果等待任一句柄，则为 false。</param>
    /// <param name="millisecondsTimeout">等待的毫秒数，或为 <see cref="F:System.Threading.Timeout.Infinite" /> (-1)，表示无限期等待。</param>
    [SecurityCritical]
    [CLSCompliant(false)]
    [PrePrepareMethod]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    protected static extern int WaitHelper(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout);

    /// <summary>设置当前同步上下文。</summary>
    /// <param name="syncContext">要设置的 <see cref="T:System.Threading.SynchronizationContext" /> 对象。</param>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static void SetSynchronizationContext(SynchronizationContext syncContext)
    {
      ExecutionContext executionContext = Thread.CurrentThread.GetMutableExecutionContext();
      SynchronizationContext synchronizationContext1 = syncContext;
      executionContext.SynchronizationContext = synchronizationContext1;
      SynchronizationContext synchronizationContext2 = syncContext;
      executionContext.SynchronizationContextNoFlow = synchronizationContext2;
    }

    private static SynchronizationContext GetThreadLocalContext()
    {
      SynchronizationContext synchronizationContext = (SynchronizationContext) null;
      if (synchronizationContext == null && Environment.IsWinRTSupported)
        synchronizationContext = SynchronizationContext.GetWinRTContext();
      return synchronizationContext;
    }

    [SecuritySafeCritical]
    private static SynchronizationContext GetWinRTContext()
    {
      if (!AppDomain.IsAppXModel())
        return (SynchronizationContext) null;
      object forCurrentThread = SynchronizationContext.GetWinRTDispatcherForCurrentThread();
      if (forCurrentThread != null)
        return SynchronizationContext.GetWinRTSynchronizationContextFactory().Create(forCurrentThread);
      return (SynchronizationContext) null;
    }

    [SecurityCritical]
    private static WinRTSynchronizationContextFactoryBase GetWinRTSynchronizationContextFactory()
    {
      WinRTSynchronizationContextFactoryBase contextFactoryBase = SynchronizationContext.s_winRTContextFactory;
      if (contextFactoryBase == null)
        SynchronizationContext.s_winRTContextFactory = contextFactoryBase = (WinRTSynchronizationContextFactoryBase) Activator.CreateInstance(Type.GetType("System.Threading.WinRTSynchronizationContextFactory, System.Runtime.WindowsRuntime, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", true), true);
      return contextFactoryBase;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Interface)]
    private static extern object GetWinRTDispatcherForCurrentThread();

    /// <summary>在派生类中重写时，创建同步上下文的副本。 </summary>
    /// <returns>一个新 <see cref="T:System.Threading.SynchronizationContext" /> 对象。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual SynchronizationContext CreateCopy()
    {
      return new SynchronizationContext();
    }

    [SecurityCritical]
    private static int InvokeWaitMethodHelper(SynchronizationContext syncContext, IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)
    {
      return syncContext.Wait(waitHandles, waitAll, millisecondsTimeout);
    }

    private delegate int WaitDelegate(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout);
  }
}
