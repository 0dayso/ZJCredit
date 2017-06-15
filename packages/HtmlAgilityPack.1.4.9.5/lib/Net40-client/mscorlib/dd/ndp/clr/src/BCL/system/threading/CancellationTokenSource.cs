// Decompiled with JetBrains decompiler
// Type: System.Threading.CancellationTokenSource
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>向应该被取消的 <see cref="T:System.Threading.CancellationToken" /> 发送信号。</summary>
  [ComVisible(false)]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public class CancellationTokenSource : IDisposable
  {
    private static readonly CancellationTokenSource _staticSource_Set = new CancellationTokenSource(true);
    private static readonly CancellationTokenSource _staticSource_NotCancelable = new CancellationTokenSource(false);
    private static readonly int s_nLists = PlatformHelper.ProcessorCount > 24 ? 24 : PlatformHelper.ProcessorCount;
    private static readonly Action<object> s_LinkedTokenCancelDelegate = new Action<object>(CancellationTokenSource.LinkedTokenCancelDelegate);
    private static readonly TimerCallback s_timerCallback = new TimerCallback(CancellationTokenSource.TimerCallbackLogic);
    private volatile int m_threadIDExecutingCallbacks = -1;
    private volatile ManualResetEvent m_kernelEvent;
    private volatile SparselyPopulatedArray<CancellationCallbackInfo>[] m_registeredCallbacksLists;
    private const int CANNOT_BE_CANCELED = 0;
    private const int NOT_CANCELED = 1;
    private const int NOTIFYING = 2;
    private const int NOTIFYINGCOMPLETE = 3;
    private volatile int m_state;
    private bool m_disposed;
    private CancellationTokenRegistration[] m_linkingRegistrations;
    private volatile CancellationCallbackInfo m_executingCallback;
    private volatile Timer m_timer;

    /// <summary>获取是否已请求取消此 <see cref="T:System.Threading.CancellationTokenSource" />。</summary>
    /// <returns>如果已请求取消此 <see cref="T:System.Threading.CancellationTokenSource" />，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsCancellationRequested
    {
      [__DynamicallyInvokable] get
      {
        return this.m_state >= 2;
      }
    }

    internal bool IsCancellationCompleted
    {
      get
      {
        return this.m_state == 3;
      }
    }

    internal bool IsDisposed
    {
      get
      {
        return this.m_disposed;
      }
    }

    internal int ThreadIDExecutingCallbacks
    {
      get
      {
        return this.m_threadIDExecutingCallbacks;
      }
      set
      {
        this.m_threadIDExecutingCallbacks = value;
      }
    }

    /// <summary>获取与此 <see cref="T:System.Threading.CancellationToken" /> 关联的 <see cref="T:System.Threading.CancellationTokenSource" />。</summary>
    /// <returns>与此 <see cref="T:System.Threading.CancellationToken" /> 关联的 <see cref="T:System.Threading.CancellationTokenSource" />。</returns>
    /// <exception cref="T:System.ObjectDisposedException">The token source has been disposed.</exception>
    [__DynamicallyInvokable]
    public CancellationToken Token
    {
      [__DynamicallyInvokable] get
      {
        this.ThrowIfDisposed();
        return new CancellationToken(this);
      }
    }

    internal bool CanBeCanceled
    {
      get
      {
        return (uint) this.m_state > 0U;
      }
    }

    internal WaitHandle WaitHandle
    {
      get
      {
        this.ThrowIfDisposed();
        if (this.m_kernelEvent != null)
          return (WaitHandle) this.m_kernelEvent;
        ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        if (Interlocked.CompareExchange<ManualResetEvent>(ref this.m_kernelEvent, manualResetEvent, (ManualResetEvent) null) != null)
          manualResetEvent.Dispose();
        if (this.IsCancellationRequested)
          this.m_kernelEvent.Set();
        return (WaitHandle) this.m_kernelEvent;
      }
    }

    internal CancellationCallbackInfo ExecutingCallback
    {
      get
      {
        return this.m_executingCallback;
      }
    }

    /// <summary>初始化 <see cref="T:System.Threading.CancellationTokenSource" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public CancellationTokenSource()
    {
      this.m_state = 1;
    }

    private CancellationTokenSource(bool set)
    {
      this.m_state = set ? 3 : 0;
    }

    /// <summary>初始化 <see cref="T:System.Threading.CancellationTokenSource" /> 类的新实例，在指定的时间跨度后将被取消。</summary>
    /// <param name="delay">取消此 <see cref="T:System.Threading.CancellationTokenSource" /> 前等待的时间间隔。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="delay" />.<see cref="P:System.TimeSpan.TotalMilliseconds" /> is less than -1 or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
    [__DynamicallyInvokable]
    public CancellationTokenSource(TimeSpan delay)
    {
      long num = (long) delay.TotalMilliseconds;
      if (num < -1L || num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("delay");
      this.InitializeWithTimer((int) num);
    }

    /// <summary>初始化 <see cref="T:System.Threading.CancellationTokenSource" /> 类的新实例，在指定的延迟（以毫秒为单位）后将被取消。</summary>
    /// <param name="millisecondsDelay">取消此 <see cref="T:System.Threading.CancellationTokenSource" /> 前等待的时间间隔（以毫秒为单位）。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsDelay" /> is less than -1. </exception>
    [__DynamicallyInvokable]
    public CancellationTokenSource(int millisecondsDelay)
    {
      if (millisecondsDelay < -1)
        throw new ArgumentOutOfRangeException("millisecondsDelay");
      this.InitializeWithTimer(millisecondsDelay);
    }

    private static void LinkedTokenCancelDelegate(object source)
    {
      (source as CancellationTokenSource).Cancel();
    }

    private void InitializeWithTimer(int millisecondsDelay)
    {
      this.m_state = 1;
      this.m_timer = new Timer(CancellationTokenSource.s_timerCallback, (object) this, millisecondsDelay, -1);
    }

    /// <summary>传达取消请求。</summary>
    /// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
    /// <exception cref="T:System.AggregateException">An aggregate exception containing all the exceptions thrown by the registered callbacks on the associated <see cref="T:System.Threading.CancellationToken" />.</exception>
    [__DynamicallyInvokable]
    public void Cancel()
    {
      this.Cancel(false);
    }

    /// <summary>传达对取消的请求，并指定是否应处理其余回调和可取消操作。</summary>
    /// <param name="throwOnFirstException">如果可以立即传播异常，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
    /// <exception cref="T:System.AggregateException">An aggregate exception containing all the exceptions thrown by the registered callbacks on the associated <see cref="T:System.Threading.CancellationToken" />.</exception>
    [__DynamicallyInvokable]
    public void Cancel(bool throwOnFirstException)
    {
      this.ThrowIfDisposed();
      this.NotifyCancellation(throwOnFirstException);
    }

    /// <summary>在指定的时间跨度后计划对此 <see cref="T:System.Threading.CancellationTokenSource" /> 的取消操作。</summary>
    /// <param name="delay">取消此 <see cref="T:System.Threading.CancellationTokenSource" /> 前等待的时间范围。</param>
    /// <exception cref="T:System.ObjectDisposedException">The exception thrown when this <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when <paramref name="delay" /> is less than -1 or greater than Int32.MaxValue.</exception>
    [__DynamicallyInvokable]
    public void CancelAfter(TimeSpan delay)
    {
      long num = (long) delay.TotalMilliseconds;
      if (num < -1L || num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("delay");
      this.CancelAfter((int) num);
    }

    /// <summary>在指定的毫秒数后计划对此 <see cref="T:System.Threading.CancellationTokenSource" /> 的取消操作。</summary>
    /// <param name="millisecondsDelay">取消此 <see cref="T:System.Threading.CancellationTokenSource" /> 前等待的时间范围。</param>
    /// <exception cref="T:System.ObjectDisposedException">The exception thrown when this <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The exception thrown when <paramref name="millisecondsDelay" /> is less than -1.</exception>
    [__DynamicallyInvokable]
    public void CancelAfter(int millisecondsDelay)
    {
      this.ThrowIfDisposed();
      if (millisecondsDelay < -1)
        throw new ArgumentOutOfRangeException("millisecondsDelay");
      if (this.IsCancellationRequested)
        return;
      if (this.m_timer == null)
      {
        Timer timer = new Timer(CancellationTokenSource.s_timerCallback, (object) this, -1, -1);
        if (Interlocked.CompareExchange<Timer>(ref this.m_timer, timer, (Timer) null) != null)
          timer.Dispose();
      }
      try
      {
        this.m_timer.Change(millisecondsDelay, -1);
      }
      catch (ObjectDisposedException ex)
      {
      }
    }

    private static void TimerCallbackLogic(object obj)
    {
      CancellationTokenSource cancellationTokenSource = (CancellationTokenSource) obj;
      if (cancellationTokenSource.IsDisposed)
        return;
      try
      {
        cancellationTokenSource.Cancel();
      }
      catch (ObjectDisposedException ex)
      {
        if (cancellationTokenSource.IsDisposed)
          return;
        throw;
      }
    }

    /// <summary>释放 <see cref="T:System.Threading.CancellationTokenSource" /> 类的当前实例所使用的所有资源。</summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>释放 <see cref="T:System.Threading.CancellationTokenSource" /> 类使用的非托管资源，并可以选择释放托管资源。</summary>
    /// <param name="disposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。</param>
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool disposing)
    {
      if (!disposing || this.m_disposed)
        return;
      if (this.m_timer != null)
        this.m_timer.Dispose();
      CancellationTokenRegistration[] tokenRegistrationArray = this.m_linkingRegistrations;
      if (tokenRegistrationArray != null)
      {
        this.m_linkingRegistrations = (CancellationTokenRegistration[]) null;
        for (int index = 0; index < tokenRegistrationArray.Length; ++index)
          tokenRegistrationArray[index].Dispose();
      }
      this.m_registeredCallbacksLists = (SparselyPopulatedArray<CancellationCallbackInfo>[]) null;
      if (this.m_kernelEvent != null)
      {
        this.m_kernelEvent.Close();
        this.m_kernelEvent = (ManualResetEvent) null;
      }
      this.m_disposed = true;
    }

    internal void ThrowIfDisposed()
    {
      if (!this.m_disposed)
        return;
      CancellationTokenSource.ThrowObjectDisposedException();
    }

    private static void ThrowObjectDisposedException()
    {
      throw new ObjectDisposedException((string) null, Environment.GetResourceString("CancellationTokenSource_Disposed"));
    }

    internal static CancellationTokenSource InternalGetStaticSource(bool set)
    {
      if (!set)
        return CancellationTokenSource._staticSource_NotCancelable;
      return CancellationTokenSource._staticSource_Set;
    }

    internal CancellationTokenRegistration InternalRegister(Action<object> callback, object stateForCallback, SynchronizationContext targetSyncContext, ExecutionContext executionContext)
    {
      if (AppContextSwitches.ThrowExceptionIfDisposedCancellationTokenSource)
        this.ThrowIfDisposed();
      if (!this.IsCancellationRequested)
      {
        if (this.m_disposed && !AppContextSwitches.ThrowExceptionIfDisposedCancellationTokenSource)
          return new CancellationTokenRegistration();
        int index = Thread.CurrentThread.ManagedThreadId % CancellationTokenSource.s_nLists;
        CancellationCallbackInfo cancellationCallbackInfo = new CancellationCallbackInfo(callback, stateForCallback, targetSyncContext, executionContext, this);
        SparselyPopulatedArray<CancellationCallbackInfo>[] sparselyPopulatedArrayArray1 = this.m_registeredCallbacksLists;
        if (sparselyPopulatedArrayArray1 == null)
        {
          SparselyPopulatedArray<CancellationCallbackInfo>[] sparselyPopulatedArrayArray2 = new SparselyPopulatedArray<CancellationCallbackInfo>[CancellationTokenSource.s_nLists];
          sparselyPopulatedArrayArray1 = Interlocked.CompareExchange<SparselyPopulatedArray<CancellationCallbackInfo>[]>(ref this.m_registeredCallbacksLists, sparselyPopulatedArrayArray2, (SparselyPopulatedArray<CancellationCallbackInfo>[]) null) ?? sparselyPopulatedArrayArray2;
        }
        SparselyPopulatedArray<CancellationCallbackInfo> sparselyPopulatedArray1 = Volatile.Read<SparselyPopulatedArray<CancellationCallbackInfo>>(ref sparselyPopulatedArrayArray1[index]);
        if (sparselyPopulatedArray1 == null)
        {
          SparselyPopulatedArray<CancellationCallbackInfo> sparselyPopulatedArray2 = new SparselyPopulatedArray<CancellationCallbackInfo>(4);
          Interlocked.CompareExchange<SparselyPopulatedArray<CancellationCallbackInfo>>(ref sparselyPopulatedArrayArray1[index], sparselyPopulatedArray2, (SparselyPopulatedArray<CancellationCallbackInfo>) null);
          sparselyPopulatedArray1 = sparselyPopulatedArrayArray1[index];
        }
        SparselyPopulatedArrayAddInfo<CancellationCallbackInfo> registrationInfo = sparselyPopulatedArray1.Add(cancellationCallbackInfo);
        CancellationTokenRegistration tokenRegistration = new CancellationTokenRegistration(cancellationCallbackInfo, registrationInfo);
        if (!this.IsCancellationRequested || !tokenRegistration.TryDeregister())
          return tokenRegistration;
      }
      callback(stateForCallback);
      return new CancellationTokenRegistration();
    }

    private void NotifyCancellation(bool throwOnFirstException)
    {
      if (this.IsCancellationRequested || Interlocked.CompareExchange(ref this.m_state, 2, 1) != 1)
        return;
      Timer timer = this.m_timer;
      if (timer != null)
        timer.Dispose();
      this.ThreadIDExecutingCallbacks = Thread.CurrentThread.ManagedThreadId;
      if (this.m_kernelEvent != null)
        this.m_kernelEvent.Set();
      this.ExecuteCallbackHandlers(throwOnFirstException);
    }

    private void ExecuteCallbackHandlers(bool throwOnFirstException)
    {
      List<Exception> exceptionList = (List<Exception>) null;
      SparselyPopulatedArray<CancellationCallbackInfo>[] sparselyPopulatedArrayArray = this.m_registeredCallbacksLists;
      if (sparselyPopulatedArrayArray == null)
      {
        Interlocked.Exchange(ref this.m_state, 3);
      }
      else
      {
        try
        {
          for (int index = 0; index < sparselyPopulatedArrayArray.Length; ++index)
          {
            SparselyPopulatedArray<CancellationCallbackInfo> sparselyPopulatedArray = Volatile.Read<SparselyPopulatedArray<CancellationCallbackInfo>>(ref sparselyPopulatedArrayArray[index]);
            if (sparselyPopulatedArray != null)
            {
              for (SparselyPopulatedArrayFragment<CancellationCallbackInfo> currArrayFragment = sparselyPopulatedArray.Tail; currArrayFragment != null; currArrayFragment = currArrayFragment.Prev)
              {
                for (int currArrayIndex = currArrayFragment.Length - 1; currArrayIndex >= 0; --currArrayIndex)
                {
                  this.m_executingCallback = currArrayFragment[currArrayIndex];
                  if (this.m_executingCallback != null)
                  {
                    CancellationCallbackCoreWorkArguments args = new CancellationCallbackCoreWorkArguments(currArrayFragment, currArrayIndex);
                    try
                    {
                      if (this.m_executingCallback.TargetSyncContext != null)
                      {
                        this.m_executingCallback.TargetSyncContext.Send(new SendOrPostCallback(this.CancellationCallbackCoreWork_OnSyncContext), (object) args);
                        this.ThreadIDExecutingCallbacks = Thread.CurrentThread.ManagedThreadId;
                      }
                      else
                        this.CancellationCallbackCoreWork(args);
                    }
                    catch (Exception ex)
                    {
                      if (throwOnFirstException)
                      {
                        throw;
                      }
                      else
                      {
                        if (exceptionList == null)
                          exceptionList = new List<Exception>();
                        exceptionList.Add(ex);
                      }
                    }
                  }
                }
              }
            }
          }
        }
        finally
        {
          this.m_state = 3;
          this.m_executingCallback = (CancellationCallbackInfo) null;
          Thread.MemoryBarrier();
        }
        if (exceptionList != null)
          throw new AggregateException((IEnumerable<Exception>) exceptionList);
      }
    }

    private void CancellationCallbackCoreWork_OnSyncContext(object obj)
    {
      this.CancellationCallbackCoreWork((CancellationCallbackCoreWorkArguments) obj);
    }

    private void CancellationCallbackCoreWork(CancellationCallbackCoreWorkArguments args)
    {
      CancellationCallbackInfo cancellationCallbackInfo = args.m_currArrayFragment.SafeAtomicRemove(args.m_currArrayIndex, this.m_executingCallback);
      if (cancellationCallbackInfo != this.m_executingCallback)
        return;
      if (cancellationCallbackInfo.TargetExecutionContext != null)
        cancellationCallbackInfo.CancellationTokenSource.ThreadIDExecutingCallbacks = Thread.CurrentThread.ManagedThreadId;
      cancellationCallbackInfo.ExecuteCallback();
    }

    /// <summary>创建一个将在任何源标记处于取消状态时处于取消状态的 <see cref="T:System.Threading.CancellationTokenSource" />。</summary>
    /// <returns>一个链接到源标记的 <see cref="T:System.Threading.CancellationTokenSource" />。</returns>
    /// <param name="token1">要观察的第一个取消标记。</param>
    /// <param name="token2">要观察的第二个取消标记。</param>
    /// <exception cref="T:System.ObjectDisposedException">A <see cref="T:System.Threading.CancellationTokenSource" /> associated with one of the source tokens has been disposed.</exception>
    [__DynamicallyInvokable]
    public static CancellationTokenSource CreateLinkedTokenSource(CancellationToken token1, CancellationToken token2)
    {
      CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
      bool canBeCanceled = token2.CanBeCanceled;
      if (token1.CanBeCanceled)
      {
        cancellationTokenSource.m_linkingRegistrations = new CancellationTokenRegistration[canBeCanceled ? 2 : 1];
        cancellationTokenSource.m_linkingRegistrations[0] = token1.InternalRegisterWithoutEC(CancellationTokenSource.s_LinkedTokenCancelDelegate, (object) cancellationTokenSource);
      }
      if (canBeCanceled)
      {
        int index = 1;
        if (cancellationTokenSource.m_linkingRegistrations == null)
        {
          cancellationTokenSource.m_linkingRegistrations = new CancellationTokenRegistration[1];
          index = 0;
        }
        cancellationTokenSource.m_linkingRegistrations[index] = token2.InternalRegisterWithoutEC(CancellationTokenSource.s_LinkedTokenCancelDelegate, (object) cancellationTokenSource);
      }
      return cancellationTokenSource;
    }

    /// <summary>创建一个将在在指定的数组中任何源标记处于取消状态时处于取消状态的 <see cref="T:System.Threading.CancellationTokenSource" />。</summary>
    /// <returns>一个链接到源标记的 <see cref="T:System.Threading.CancellationTokenSource" />。</returns>
    /// <param name="tokens">包含要观察的取消标记实例的数组。</param>
    /// <exception cref="T:System.ObjectDisposedException">A <see cref="T:System.Threading.CancellationTokenSource" /> associated with one of the source tokens has been disposed.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="tokens" /> is null.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="tokens" /> is empty.</exception>
    [__DynamicallyInvokable]
    public static CancellationTokenSource CreateLinkedTokenSource(params CancellationToken[] tokens)
    {
      if (tokens == null)
        throw new ArgumentNullException("tokens");
      if (tokens.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("CancellationToken_CreateLinkedToken_TokensIsEmpty"));
      CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
      cancellationTokenSource.m_linkingRegistrations = new CancellationTokenRegistration[tokens.Length];
      for (int index = 0; index < tokens.Length; ++index)
      {
        if (tokens[index].CanBeCanceled)
          cancellationTokenSource.m_linkingRegistrations[index] = tokens[index].InternalRegisterWithoutEC(CancellationTokenSource.s_LinkedTokenCancelDelegate, (object) cancellationTokenSource);
      }
      return cancellationTokenSource;
    }

    internal void WaitForCallbackToComplete(CancellationCallbackInfo callbackInfo)
    {
      SpinWait spinWait = new SpinWait();
      while (this.ExecutingCallback == callbackInfo)
        spinWait.SpinOnce();
    }
  }
}
