// Decompiled with JetBrains decompiler
// Type: System.Threading.Thread
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Threading
{
  /// <summary>创建和控制线程，设置其优先级并获取其状态。若要浏览此类型的.NET Framework 源代码，请参阅 Reference Source。</summary>
  /// <filterpriority>1</filterpriority>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_Thread))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class Thread : CriticalFinalizerObject, _Thread
  {
    private Context m_Context;
    private ExecutionContext m_ExecutionContext;
    private string m_Name;
    private Delegate m_Delegate;
    private CultureInfo m_CurrentCulture;
    private CultureInfo m_CurrentUICulture;
    private object m_ThreadStartArg;
    private IntPtr DONT_USE_InternalThread;
    private int m_Priority;
    private int m_ManagedThreadId;
    private bool m_ExecutionContextBelongsToOuterScope;
    private static LocalDataStoreMgr s_LocalDataStoreMgr;
    [ThreadStatic]
    private static LocalDataStoreHolder s_LocalDataStore;
    private static AsyncLocal<CultureInfo> s_asyncLocalCurrentCulture;
    private static AsyncLocal<CultureInfo> s_asyncLocalCurrentUICulture;

    /// <summary>获取当前托管线程的唯一标识符。</summary>
    /// <returns>一个整数，表示此托管线程的唯一标识符。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int ManagedThreadId { [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), SecuritySafeCritical, __DynamicallyInvokable, MethodImpl(MethodImplOptions.InternalCall)] get; }

    internal bool ExecutionContextBelongsToCurrentScope
    {
      get
      {
        return !this.m_ExecutionContextBelongsToOuterScope;
      }
      set
      {
        this.m_ExecutionContextBelongsToOuterScope = !value;
      }
    }

    /// <summary>获取 <see cref="T:System.Threading.ExecutionContext" /> 对象，该对象包含有关当前线程的各种上下文的信息。</summary>
    /// <returns>一个 <see cref="T:System.Threading.ExecutionContext" /> 对象，包含当前线程的上下文信息。</returns>
    /// <filterpriority>2</filterpriority>
    public ExecutionContext ExecutionContext
    {
      [SecuritySafeCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)] get
      {
        return this != Thread.CurrentThread ? this.m_ExecutionContext : this.GetMutableExecutionContext();
      }
    }

    /// <summary>获取或设置指示线程的调度优先级的值。</summary>
    /// <returns>
    /// <see cref="T:System.Threading.ThreadPriority" /> 值之一。默认值为 <see cref="F:System.Threading.ThreadPriority.Normal" />。</returns>
    /// <exception cref="T:System.Threading.ThreadStateException">线程已到达最终状态，如 <see cref="F:System.Threading.ThreadState.Aborted" />。</exception>
    /// <exception cref="T:System.ArgumentException">指定为一个集运算不是有效的值 <see cref="T:System.Threading.ThreadPriority" /> 值。</exception>
    /// <filterpriority>1</filterpriority>
    public ThreadPriority Priority
    {
      [SecuritySafeCritical] get
      {
        return (ThreadPriority) this.GetPriorityNative();
      }
      [SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, SelfAffectingThreading = true)] set
      {
        this.SetPriorityNative((int) value);
      }
    }

    /// <summary>获取指示当前线程的执行状态的值。</summary>
    /// <returns>如果此线程已经开始但尚未正常终止或中止，则为 true，否则为 false。</returns>
    /// <filterpriority>1</filterpriority>
    public bool IsAlive { [SecuritySafeCritical, MethodImpl(MethodImplOptions.InternalCall)] get; }

    /// <summary>获取指示线程是否属于托管线程池的值。</summary>
    /// <returns>如果此线程属于托管线程池，则为 true；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public bool IsThreadPoolThread { [SecuritySafeCritical, MethodImpl(MethodImplOptions.InternalCall)] get; }

    /// <summary>获取当前正在运行的线程。</summary>
    /// <returns>
    /// <see cref="T:System.Threading.Thread" />，表示当前正在运行的线程。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Thread CurrentThread
    {
      [SecuritySafeCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail), __DynamicallyInvokable] get
      {
        return Thread.GetCurrentThreadNative();
      }
    }

    /// <summary>获取或设置一个值，该值指示某个线程是否为后台线程。</summary>
    /// <returns>如果此线程为或将成为后台线程，则为 true；否则为 false。</returns>
    /// <exception cref="T:System.Threading.ThreadStateException">该线程是死。</exception>
    /// <filterpriority>1</filterpriority>
    public bool IsBackground
    {
      [SecuritySafeCritical] get
      {
        return this.IsBackgroundNative();
      }
      [SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, SelfAffectingThreading = true)] set
      {
        this.SetBackgroundNative(value);
      }
    }

    /// <summary>获取一个值，该值包含当前线程的状态。</summary>
    /// <returns>其中一个表示当前线程的状态的 <see cref="T:System.Threading.ThreadState" /> 值。初始值为 Unstarted。</returns>
    /// <filterpriority>2</filterpriority>
    public ThreadState ThreadState
    {
      [SecuritySafeCritical] get
      {
        return (ThreadState) this.GetThreadStateNative();
      }
    }

    /// <summary>获取或设置此线程的单元状态。</summary>
    /// <returns>
    /// <see cref="T:System.Threading.ApartmentState" /> 值之一。初始值为 Unknown。</returns>
    /// <exception cref="T:System.ArgumentException">尝试将此属性设置为不是有效的单元状态的状态 （单线程单元以外的状态 （STA） 或多线程的单元 (MTA)）。</exception>
    /// <filterpriority>2</filterpriority>
    [Obsolete("The ApartmentState property has been deprecated.  Use GetApartmentState, SetApartmentState or TrySetApartmentState instead.", false)]
    public ApartmentState ApartmentState
    {
      [SecuritySafeCritical] get
      {
        return (ApartmentState) this.GetApartmentStateNative();
      }
      [SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, SelfAffectingThreading = true, Synchronization = true)] set
      {
        this.SetApartmentStateNative((int) value, true);
      }
    }

    /// <summary>获取或设置资源管理器使用的当前区域性以便在运行时查找区域性特定的资源。</summary>
    /// <returns>表示当前区域性的对象。</returns>
    /// <exception cref="T:System.ArgumentNullException">该属性设置为 null。</exception>
    /// <exception cref="T:System.ArgumentException">该属性设置为不能用于定位资源文件的区域性名称。资源文件名必须包含字母、 数字、 连字符或下划线。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public CultureInfo CurrentUICulture
    {
      [__DynamicallyInvokable] get
      {
        if (AppDomain.IsAppXModel())
          return CultureInfo.GetCultureInfoForUserPreferredLanguageInAppX() ?? this.GetCurrentUICultureNoAppX();
        return this.GetCurrentUICultureNoAppX();
      }
      [SecuritySafeCritical, __DynamicallyInvokable, HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)] set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        CultureInfo.VerifyCultureName(value, true);
        if (!Thread.nativeSetThreadUILocale(value.SortName))
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidResourceCultureName", (object) value.Name));
        value.StartCrossDomainTracking();
        if (!AppContextSwitches.NoAsyncCurrentCulture)
        {
          if (Thread.s_asyncLocalCurrentUICulture == null)
            Interlocked.CompareExchange<AsyncLocal<CultureInfo>>(ref Thread.s_asyncLocalCurrentUICulture, new AsyncLocal<CultureInfo>(new Action<AsyncLocalValueChangedArgs<CultureInfo>>(Thread.AsyncLocalSetCurrentUICulture)), (AsyncLocal<CultureInfo>) null);
          Thread.s_asyncLocalCurrentUICulture.Value = value;
        }
        else
          this.m_CurrentUICulture = value;
      }
    }

    /// <summary>获取或设置当前线程的区域性。</summary>
    /// <returns>表示当前线程的区域性的对象。</returns>
    /// <exception cref="T:System.ArgumentNullException">该属性设置为 null。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public CultureInfo CurrentCulture
    {
      [__DynamicallyInvokable] get
      {
        if (AppDomain.IsAppXModel())
          return CultureInfo.GetCultureInfoForUserPreferredLanguageInAppX() ?? this.GetCurrentCultureNoAppX();
        return this.GetCurrentCultureNoAppX();
      }
      [SecuritySafeCritical, __DynamicallyInvokable, SecurityPermission(SecurityAction.Demand, ControlThread = true)] set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        CultureInfo.nativeSetThreadLocale(value.SortName);
        value.StartCrossDomainTracking();
        if (!AppContextSwitches.NoAsyncCurrentCulture)
        {
          if (Thread.s_asyncLocalCurrentCulture == null)
            Interlocked.CompareExchange<AsyncLocal<CultureInfo>>(ref Thread.s_asyncLocalCurrentCulture, new AsyncLocal<CultureInfo>(new Action<AsyncLocalValueChangedArgs<CultureInfo>>(Thread.AsyncLocalSetCurrentCulture)), (AsyncLocal<CultureInfo>) null);
          Thread.s_asyncLocalCurrentCulture.Value = value;
        }
        else
          this.m_CurrentCulture = value;
      }
    }

    /// <summary>获取线程正在其中执行的当前上下文。</summary>
    /// <returns>表示当前线程上下文的 <see cref="T:System.Runtime.Remoting.Contexts.Context" />。</returns>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public static Context CurrentContext
    {
      [SecurityCritical] get
      {
        return Thread.CurrentThread.GetCurrentContextInternal();
      }
    }

    /// <summary>获取或设置线程的当前负责人（对基于角色的安全性而言）。</summary>
    /// <returns>表示安全上下文的 <see cref="T:System.Security.Principal.IPrincipal" /> 值。</returns>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所需设置主体的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
    /// </PermissionSet>
    public static IPrincipal CurrentPrincipal
    {
      [SecuritySafeCritical] get
      {
        lock (Thread.CurrentThread)
        {
          IPrincipal local_2 = CallContext.Principal;
          if (local_2 == null)
          {
            local_2 = Thread.GetDomain().GetThreadPrincipal();
            CallContext.Principal = local_2;
          }
          return local_2;
        }
      }
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)] set
      {
        CallContext.Principal = value;
      }
    }

    /// <summary>获取或设置线程的名称。</summary>
    /// <returns>包含线程名称的字符串或 null（如果未设置名称）。</returns>
    /// <exception cref="T:System.InvalidOperationException">请求设置操作，但 Name 已设置属性。</exception>
    /// <filterpriority>1</filterpriority>
    public string Name
    {
      get
      {
        return this.m_Name;
      }
      [SecuritySafeCritical, HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)] set
      {
        lock (this)
        {
          if (this.m_Name != null)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WriteOnce"));
          this.m_Name = value;
          ThreadHandle temp_9 = this.GetNativeHandle();
          string temp_10 = value;
          int temp_12 = temp_10 != null ? value.Length : 0;
          Thread.InformThreadNameChange(temp_9, temp_10, temp_12);
        }
      }
    }

    internal object AbortReason
    {
      [SecurityCritical] get
      {
        try
        {
          return this.GetAbortReason();
        }
        catch (Exception ex)
        {
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ExceptionStateCrossAppDomain"), ex);
        }
      }
      [SecurityCritical] set
      {
        this.SetAbortReason(value);
      }
    }

    private static LocalDataStoreMgr LocalDataStoreManager
    {
      get
      {
        if (Thread.s_LocalDataStoreMgr == null)
          Interlocked.CompareExchange<LocalDataStoreMgr>(ref Thread.s_LocalDataStoreMgr, new LocalDataStoreMgr(), (LocalDataStoreMgr) null);
        return Thread.s_LocalDataStoreMgr;
      }
    }

    /// <summary>初始化 <see cref="T:System.Threading.Thread" /> 类的新实例。</summary>
    /// <param name="start">表示开始执行此线程时要调用的方法的 <see cref="T:System.Threading.ThreadStart" /> 委托。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="start" /> 参数为 null。</exception>
    [SecuritySafeCritical]
    public Thread(ThreadStart start)
    {
      if (start == null)
        throw new ArgumentNullException("start");
      this.SetStartHelper((Delegate) start, 0);
    }

    /// <summary>初始化 <see cref="T:System.Threading.Thread" /> 类的新实例，指定线程的最大堆栈大小。</summary>
    /// <param name="start">表示开始执行此线程时要调用的方法的 <see cref="T:System.Threading.ThreadStart" /> 委托。</param>
    /// <param name="maxStackSize">线程要使用的最大堆栈大小（以字节为单位）；如果为 0，则使用可执行文件的文件头中指定的默认最大堆栈大小。重要事项：对于部分受信任的代码，如果 <paramref name="maxStackSize" /> 大于默认堆栈大小，则将其忽略。不引发异常。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="start" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="maxStackSize" /> 小于零。</exception>
    [SecuritySafeCritical]
    public Thread(ThreadStart start, int maxStackSize)
    {
      if (start == null)
        throw new ArgumentNullException("start");
      if (0 > maxStackSize)
        throw new ArgumentOutOfRangeException("maxStackSize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this.SetStartHelper((Delegate) start, maxStackSize);
    }

    /// <summary>初始化 <see cref="T:System.Threading.Thread" /> 类的新实例，指定允许对象在线程启动时传递给线程的委托。</summary>
    /// <param name="start">一个委托，它表示此线程开始执行时要调用的方法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="start" /> 为 null。</exception>
    [SecuritySafeCritical]
    public Thread(ParameterizedThreadStart start)
    {
      if (start == null)
        throw new ArgumentNullException("start");
      this.SetStartHelper((Delegate) start, 0);
    }

    /// <summary>初始化 <see cref="T:System.Threading.Thread" /> 类的新实例，指定允许对象在线程启动时传递给线程的委托，并指定线程的最大堆栈大小。。</summary>
    /// <param name="start">表示开始执行此线程时要调用的方法的 <see cref="T:System.Threading.ParameterizedThreadStart" /> 委托。</param>
    /// <param name="maxStackSize">线程要使用的最大堆栈大小（以字节为单位）；如果为 0，则使用可执行文件的文件头中指定的默认最大堆栈大小。重要事项：对于部分受信任的代码，如果 <paramref name="maxStackSize" /> 大于默认堆栈大小，则将其忽略。不引发异常。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="start" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="maxStackSize" /> 小于零。</exception>
    [SecuritySafeCritical]
    public Thread(ParameterizedThreadStart start, int maxStackSize)
    {
      if (start == null)
        throw new ArgumentNullException("start");
      if (0 > maxStackSize)
        throw new ArgumentOutOfRangeException("maxStackSize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this.SetStartHelper((Delegate) start, maxStackSize);
    }

    /// <summary>确保垃圾回收器回收 <see cref="T:System.Threading.Thread" /> 对象时释放资源并执行其他清理操作。</summary>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    ~Thread()
    {
      this.InternalFinalize();
    }

    private static void AsyncLocalSetCurrentCulture(AsyncLocalValueChangedArgs<CultureInfo> args)
    {
      Thread.CurrentThread.m_CurrentCulture = args.CurrentValue;
    }

    private static void AsyncLocalSetCurrentUICulture(AsyncLocalValueChangedArgs<CultureInfo> args)
    {
      Thread.CurrentThread.m_CurrentUICulture = args.CurrentValue;
    }

    /// <summary>返回当前线程的哈希代码。</summary>
    /// <returns>整数哈希代码值。</returns>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    public override int GetHashCode()
    {
      return this.m_ManagedThreadId;
    }

    internal ThreadHandle GetNativeHandle()
    {
      IntPtr pThread = this.DONT_USE_InternalThread;
      if (pThread.IsNull())
        throw new ArgumentException((string) null, Environment.GetResourceString("Argument_InvalidHandle"));
      return new ThreadHandle(pThread);
    }

    /// <summary>导致操作系统将当前实例的状态更改为 <see cref="F:System.Threading.ThreadState.Running" />。</summary>
    /// <exception cref="T:System.Threading.ThreadStateException">该线程已启动。</exception>
    /// <exception cref="T:System.OutOfMemoryException">没有足够内存可用于启动此线程。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    public void Start()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.Start(ref stackMark);
    }

    /// <summary>导致操作系统将当前实例的状态更改为 <see cref="F:System.Threading.ThreadState.Running" />，并选择提供包含线程执行的方法要使用的数据的对象。</summary>
    /// <param name="parameter">一个对象，包含线程执行的方法要使用的数据。</param>
    /// <exception cref="T:System.Threading.ThreadStateException">该线程已启动。</exception>
    /// <exception cref="T:System.OutOfMemoryException">没有足够内存可用于启动此线程。</exception>
    /// <exception cref="T:System.InvalidOperationException">该线程的创建使用 <see cref="T:System.Threading.ThreadStart" /> 委托，而不是 <see cref="T:System.Threading.ParameterizedThreadStart" /> 委托。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    public void Start(object parameter)
    {
      if (this.m_Delegate is ThreadStart)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ThreadWrongThreadStart"));
      this.m_ThreadStartArg = parameter;
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.Start(ref stackMark);
    }

    [SecuritySafeCritical]
    private void Start(ref StackCrawlMark stackMark)
    {
      this.StartupSetApartmentStateInternal();
      if (this.m_Delegate != null)
        ((ThreadHelper) this.m_Delegate.Target).SetExecutionContextHelper(ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx));
      this.StartInternal(CallContext.Principal, ref stackMark);
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal ExecutionContext.Reader GetExecutionContextReader()
    {
      return new ExecutionContext.Reader(this.m_ExecutionContext);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    internal ExecutionContext GetMutableExecutionContext()
    {
      if (this.m_ExecutionContext == null)
        this.m_ExecutionContext = new ExecutionContext();
      else if (!this.ExecutionContextBelongsToCurrentScope)
        this.m_ExecutionContext = this.m_ExecutionContext.CreateMutableCopy();
      this.ExecutionContextBelongsToCurrentScope = true;
      return this.m_ExecutionContext;
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal void SetExecutionContext(ExecutionContext value, bool belongsToCurrentScope)
    {
      this.m_ExecutionContext = value;
      this.ExecutionContextBelongsToCurrentScope = belongsToCurrentScope;
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal void SetExecutionContext(ExecutionContext.Reader value, bool belongsToCurrentScope)
    {
      this.m_ExecutionContext = value.DangerousGetRawExecutionContext();
      this.ExecutionContextBelongsToCurrentScope = belongsToCurrentScope;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void StartInternal(IPrincipal principal, ref StackCrawlMark stackMark);

    /// <summary>将捕获的 <see cref="T:System.Threading.CompressedStack" /> 应用到当前线程。</summary>
    /// <param name="stack">将被应用到当前线程的 <see cref="T:System.Threading.CompressedStack" /> 对象。</param>
    /// <exception cref="T:System.InvalidOperationException">在所有情况下。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    ///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="00000000000000000400000000000000" />
    /// </PermissionSet>
    [SecurityCritical]
    [Obsolete("Thread.SetCompressedStack is no longer supported. Please use the System.Threading.CompressedStack class")]
    public void SetCompressedStack(CompressedStack stack)
    {
      throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ThreadAPIsNotSupported"));
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal IntPtr SetAppDomainStack(SafeCompressedStackHandle csHandle);

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal void RestoreAppDomainStack(IntPtr appDomainStack);

    /// <summary>返回 <see cref="T:System.Threading.CompressedStack" /> 对象，此对象可用于获取当前线程的堆栈。</summary>
    /// <returns>无。</returns>
    /// <exception cref="T:System.InvalidOperationException">在所有情况下。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    ///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="00000000000000000400000000000000" />
    /// </PermissionSet>
    [SecurityCritical]
    [Obsolete("Thread.GetCompressedStack is no longer supported. Please use the System.Threading.CompressedStack class")]
    public CompressedStack GetCompressedStack()
    {
      throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ThreadAPIsNotSupported"));
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr InternalGetCurrentThread();

    /// <summary>引发在其上调用的线程中的 <see cref="T:System.Threading.ThreadAbortException" /> 以开始处理终止线程，同时提供有关线程终止的异常信息。调用此方法通常会终止线程。</summary>
    /// <param name="stateInfo">一个对象，它包含应用程序特定的信息（如状态），该信息可供正被中止的线程使用。</param>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.Threading.ThreadStateException">正在中止该线程当前已挂起。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, ControlThread = true)]
    public void Abort(object stateInfo)
    {
      this.AbortReason = stateInfo;
      this.AbortInternal();
    }

    /// <summary>在调用此方法的线程上引发 <see cref="T:System.Threading.ThreadAbortException" />，以开始终止此线程的过程。调用此方法通常会终止线程。</summary>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.Threading.ThreadStateException">正在中止该线程当前已挂起。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, ControlThread = true)]
    public void Abort()
    {
      this.AbortInternal();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void AbortInternal();

    /// <summary>取消当前线程所请求的 <see cref="M:System.Threading.Thread.Abort(System.Object)" />。</summary>
    /// <exception cref="T:System.Threading.ThreadStateException">Abort 不在当前线程上调用。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有当前线程的所需的安全权限。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, ControlThread = true)]
    public static void ResetAbort()
    {
      Thread currentThread = Thread.CurrentThread;
      if ((currentThread.ThreadState & ThreadState.AbortRequested) == ThreadState.Running)
        throw new ThreadStateException(Environment.GetResourceString("ThreadState_NoAbortRequested"));
      currentThread.ResetAbortNative();
      currentThread.ClearAbortReason();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void ResetAbortNative();

    /// <summary>挂起线程，或者如果线程已挂起，则不起作用。</summary>
    /// <exception cref="T:System.Threading.ThreadStateException">该线程尚未启动，或该退位了。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方不具有适当 <see cref="T:System.Security.Permissions.SecurityPermission" />。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("Thread.Suspend has been deprecated.  Please use other classes in System.Threading, such as Monitor, Mutex, Event, and Semaphore, to synchronize Threads or protect resources.  http://go.microsoft.com/fwlink/?linkid=14202", false)]
    [SecurityPermission(SecurityAction.Demand, ControlThread = true)]
    [SecurityPermission(SecurityAction.Demand, ControlThread = true)]
    public void Suspend()
    {
      this.SuspendInternal();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void SuspendInternal();

    /// <summary>继续已挂起的线程。</summary>
    /// <exception cref="T:System.Threading.ThreadStateException">该线程尚未启动、 已退位，或未处于挂起状态。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方不具有适当 <see cref="T:System.Security.Permissions.SecurityPermission" />。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("Thread.Resume has been deprecated.  Please use other classes in System.Threading, such as Monitor, Mutex, Event, and Semaphore, to synchronize Threads or protect resources.  http://go.microsoft.com/fwlink/?linkid=14202", false)]
    [SecurityPermission(SecurityAction.Demand, ControlThread = true)]
    public void Resume()
    {
      this.ResumeInternal();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void ResumeInternal();

    /// <summary>中断处于 WaitSleepJoin 线程状态的线程。</summary>
    /// <exception cref="T:System.Security.SecurityException">调用方不具有适当 <see cref="T:System.Security.Permissions.SecurityPermission" />。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, ControlThread = true)]
    public void Interrupt()
    {
      this.InterruptInternal();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void InterruptInternal();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private int GetPriorityNative();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void SetPriorityNative(int priority);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private bool JoinInternal(int millisecondsTimeout);

    /// <summary>阻止调用线程直到线程终止，同时继续执行标准的 COM 和 SendMessage 传送。</summary>
    /// <exception cref="T:System.Threading.ThreadStateException">调用方尝试加入处于一个线程 <see cref="F:System.Threading.ThreadState.Unstarted" /> 状态。</exception>
    /// <exception cref="T:System.Threading.ThreadInterruptedException">该线程在等待时中断。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    public void Join()
    {
      this.JoinInternal(-1);
    }

    /// <summary>在继续执行标准的 COM 和 SendMessage 消息泵处理期间，阻止调用线程，直到某个线程终止或经过了指定时间为止。</summary>
    /// <returns>如果线程已终止，则为 true；如果 false 参数指定的时间量已过之后还未终止线程，则为 <paramref name="millisecondsTimeout" />。</returns>
    /// <param name="millisecondsTimeout">等待线程终止的毫秒数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">值 <paramref name="millisecondsTimeout" /> 为负，且不等于 <see cref="F:System.Threading.Timeout.Infinite" /> 以毫秒为单位。</exception>
    /// <exception cref="T:System.Threading.ThreadStateException">该线程尚未启动。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    public bool Join(int millisecondsTimeout)
    {
      return this.JoinInternal(millisecondsTimeout);
    }

    /// <summary>在继续执行标准的 COM 和 SendMessage 消息泵处理期间，阻止调用线程，直到某个线程终止或经过了指定时间为止。</summary>
    /// <returns>如果线程已终止，则为 true；如果 false 参数指定的时间量已过之后还未终止线程，则为 <paramref name="timeout" />。</returns>
    /// <param name="timeout">设置等待线程终止的时间量的 <see cref="T:System.TimeSpan" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">值 <paramref name="timeout" /> 为负，且不等于 <see cref="F:System.Threading.Timeout.Infinite" /> 以毫秒为单位，或者大于 <see cref="F:System.Int32.MaxValue" /> 毫秒为单位）。</exception>
    /// <exception cref="T:System.Threading.ThreadStateException">调用方尝试加入处于一个线程 <see cref="F:System.Threading.ThreadState.Unstarted" /> 状态。</exception>
    /// <filterpriority>1</filterpriority>
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    public bool Join(TimeSpan timeout)
    {
      long num = (long) timeout.TotalMilliseconds;
      if (num < -1L || num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      return this.Join((int) num);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void SleepInternal(int millisecondsTimeout);

    /// <summary>将当前线程挂起指定的毫秒数。</summary>
    /// <param name="millisecondsTimeout">挂起线程的毫秒数。如果 <paramref name="millisecondsTimeout" /> 参数的值为零，则该线程会将其时间片的剩余部分让给任何已经准备好运行的、具有同等优先级的线程。如果没有其他已经准备好运行的、具有同等优先级的线程，则不会挂起当前线程的执行。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">超时值为负，且不等于 <see cref="F:System.Threading.Timeout.Infinite" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public static void Sleep(int millisecondsTimeout)
    {
      Thread.SleepInternal(millisecondsTimeout);
      if (!AppDomainPauseManager.IsPaused)
        return;
      AppDomainPauseManager.ResumeEvent.WaitOneWithoutFAS();
    }

    /// <summary>将当前线程挂起指定的时间。</summary>
    /// <param name="timeout">挂起线程的时间量。如果 <paramref name="millisecondsTimeout" /> 参数的值为 <see cref="F:System.TimeSpan.Zero" />，则该线程会将其时间片的剩余部分让给任何已经准备好运行的、具有同等优先级的线程。如果没有其他已经准备好运行的、具有同等优先级的线程，则不会挂起当前线程的执行。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">值 <paramref name="timeout" /> 为负，且不等于 <see cref="F:System.Threading.Timeout.Infinite" /> 以毫秒为单位，或者大于 <see cref="F:System.Int32.MaxValue" /> 毫秒为单位）。</exception>
    /// <filterpriority>1</filterpriority>
    public static void Sleep(TimeSpan timeout)
    {
      long num = (long) timeout.TotalMilliseconds;
      if (num < -1L || num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      Thread.Sleep((int) num);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    private static extern void SpinWaitInternal(int iterations);

    /// <summary>导致线程等待由 <paramref name="iterations" /> 参数定义的时间量。</summary>
    /// <param name="iterations">定义线程等待的时间长短的 32 位有符号整数。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    public static void SpinWait(int iterations)
    {
      Thread.SpinWaitInternal(iterations);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    private static extern bool YieldInternal();

    /// <summary>导致调用线程执行准备好在当前处理器上运行的另一个线程。由操作系统选择要执行的线程。</summary>
    /// <returns>如果操作系统转而执行另一个线程，则为 true；否则为 false。</returns>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    public static bool Yield()
    {
      return Thread.YieldInternal();
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern Thread GetCurrentThreadNative();

    [SecurityCritical]
    private void SetStartHelper(Delegate start, int maxStackSize)
    {
      ulong defaultStackSize = Thread.GetProcessDefaultStackSize();
      if ((ulong) (uint) maxStackSize > defaultStackSize)
      {
        try
        {
          CodeAccessPermission.Demand(PermissionType.FullTrust);
        }
        catch (SecurityException ex)
        {
          maxStackSize = (int) Math.Min(defaultStackSize, (ulong) int.MaxValue);
        }
      }
      ThreadHelper threadHelper = new ThreadHelper(start);
      if (start is ThreadStart)
        this.SetStart((Delegate) new ThreadStart(threadHelper.ThreadStart), maxStackSize);
      else
        this.SetStart((Delegate) new ParameterizedThreadStart(threadHelper.ThreadStart), maxStackSize);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern ulong GetProcessDefaultStackSize();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void SetStart(Delegate start, int maxStackSize);

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void InternalFinalize();

    /// <summary>对于当前线程关闭运行时可调用包装 (RCW) 的自动清理。</summary>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public void DisableComObjectEagerCleanup();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private bool IsBackgroundNative();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void SetBackgroundNative(bool isBackground);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private int GetThreadStateNative();

    /// <summary>返回表示单元状态的 <see cref="T:System.Threading.ApartmentState" /> 值。</summary>
    /// <returns>其中一个表示托管线程的单元状态的 <see cref="T:System.Threading.ApartmentState" /> 值。默认值为 <see cref="F:System.Threading.ApartmentState.Unknown" />。</returns>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public ApartmentState GetApartmentState()
    {
      return (ApartmentState) this.GetApartmentStateNative();
    }

    /// <summary>在线程启动前设置其单元状态。</summary>
    /// <returns>如果设置了单元状态，则为 true；否则为 false。</returns>
    /// <param name="state">新的单元状态。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="state" /> 不是有效的单元状态。</exception>
    /// <exception cref="T:System.Threading.ThreadStateException">该线程已启动。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, SelfAffectingThreading = true, Synchronization = true)]
    public bool TrySetApartmentState(ApartmentState state)
    {
      return this.SetApartmentStateHelper(state, false);
    }

    /// <summary>在线程启动前设置其单元状态。</summary>
    /// <param name="state">新的单元状态。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="state" /> 不是有效的单元状态。</exception>
    /// <exception cref="T:System.Threading.ThreadStateException">该线程已启动。</exception>
    /// <exception cref="T:System.InvalidOperationException">已初始化的单元状态。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, SelfAffectingThreading = true, Synchronization = true)]
    public void SetApartmentState(ApartmentState state)
    {
      if (!this.SetApartmentStateHelper(state, true))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ApartmentStateSwitchFailed"));
    }

    [SecurityCritical]
    private bool SetApartmentStateHelper(ApartmentState state, bool fireMDAOnMismatch)
    {
      ApartmentState apartmentState = (ApartmentState) this.SetApartmentStateNative((int) state, fireMDAOnMismatch);
      return state == ApartmentState.Unknown && apartmentState == ApartmentState.MTA || apartmentState == state;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private int GetApartmentStateNative();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private int SetApartmentStateNative(int state, bool fireMDAOnMismatch);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void StartupSetApartmentStateInternal();

    /// <summary>在所有线程上分配未命名的数据槽。为了获得更好的性能，请改用以 <see cref="T:System.ThreadStaticAttribute" /> 特性标记的字段。</summary>
    /// <returns>所有线程上已分配的命名数据槽。</returns>
    /// <filterpriority>2</filterpriority>
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, SharedState = true)]
    public static LocalDataStoreSlot AllocateDataSlot()
    {
      return Thread.LocalDataStoreManager.AllocateDataSlot();
    }

    /// <summary>在所有线程上分配已命名的数据槽。为了获得更好的性能，请改用以 <see cref="T:System.ThreadStaticAttribute" /> 特性标记的字段。</summary>
    /// <returns>所有线程上已分配的命名数据槽。</returns>
    /// <param name="name">要分配的数据槽的名称。</param>
    /// <exception cref="T:System.ArgumentException">已存在具有指定名称的命名的数据槽。</exception>
    /// <filterpriority>2</filterpriority>
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, SharedState = true)]
    public static LocalDataStoreSlot AllocateNamedDataSlot(string name)
    {
      return Thread.LocalDataStoreManager.AllocateNamedDataSlot(name);
    }

    /// <summary>查找命名的数据槽。为了获得更好的性能，请改用以 <see cref="T:System.ThreadStaticAttribute" /> 特性标记的字段。</summary>
    /// <returns>为此线程分配的 <see cref="T:System.LocalDataStoreSlot" />。</returns>
    /// <param name="name">本地数据槽的名称。</param>
    /// <filterpriority>2</filterpriority>
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, SharedState = true)]
    public static LocalDataStoreSlot GetNamedDataSlot(string name)
    {
      return Thread.LocalDataStoreManager.GetNamedDataSlot(name);
    }

    /// <summary>为进程中的所有线程消除名称与槽之间的关联。为了获得更好的性能，请改用以 <see cref="T:System.ThreadStaticAttribute" /> 特性标记的字段。</summary>
    /// <param name="name">要释放的数据槽的名称。</param>
    /// <filterpriority>2</filterpriority>
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, SharedState = true)]
    public static void FreeNamedDataSlot(string name)
    {
      Thread.LocalDataStoreManager.FreeNamedDataSlot(name);
    }

    /// <summary>在当前线程的当前域中从当前线程上指定的槽中检索值。为了获得更好的性能，请改用以 <see cref="T:System.ThreadStaticAttribute" /> 特性标记的字段。</summary>
    /// <returns>检索到的值。</returns>
    /// <param name="slot">要从其获取值的 <see cref="T:System.LocalDataStoreSlot" />。</param>
    /// <filterpriority>2</filterpriority>
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, SharedState = true)]
    public static object GetData(LocalDataStoreSlot slot)
    {
      LocalDataStoreHolder localDataStoreHolder = Thread.s_LocalDataStore;
      if (localDataStoreHolder != null)
        return localDataStoreHolder.Store.GetData(slot);
      Thread.LocalDataStoreManager.ValidateSlot(slot);
      return (object) null;
    }

    /// <summary>在当前正在运行的线程上为此线程的当前域在指定槽中设置数据。为了提高性能，请改用用 <see cref="T:System.ThreadStaticAttribute" /> 属性标记的字段。</summary>
    /// <param name="slot">在其中设置值的 <see cref="T:System.LocalDataStoreSlot" />。</param>
    /// <param name="data">要设置的值。</param>
    /// <filterpriority>1</filterpriority>
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, SharedState = true)]
    public static void SetData(LocalDataStoreSlot slot, object data)
    {
      LocalDataStoreHolder localDataStoreHolder = Thread.s_LocalDataStore;
      if (localDataStoreHolder == null)
      {
        localDataStoreHolder = Thread.LocalDataStoreManager.CreateLocalDataStore();
        Thread.s_LocalDataStore = localDataStoreHolder;
      }
      localDataStoreHolder.Store.SetData(slot, data);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool nativeGetSafeCulture(Thread t, int appDomainId, bool isUI, ref CultureInfo safeCulture);

    [SecuritySafeCritical]
    internal CultureInfo GetCurrentUICultureNoAppX()
    {
      if (this.m_CurrentUICulture == null)
        return CultureInfo.DefaultThreadCurrentUICulture ?? CultureInfo.UserDefaultUICulture;
      CultureInfo safeCulture = (CultureInfo) null;
      if (!Thread.nativeGetSafeCulture(this, Thread.GetDomainID(), true, ref safeCulture) || safeCulture == null)
        return CultureInfo.UserDefaultUICulture;
      return safeCulture;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool nativeSetThreadUILocale(string locale);

    [SecuritySafeCritical]
    private CultureInfo GetCurrentCultureNoAppX()
    {
      if (this.m_CurrentCulture == null)
        return CultureInfo.DefaultThreadCurrentCulture ?? CultureInfo.UserDefaultCulture;
      CultureInfo safeCulture = (CultureInfo) null;
      if (!Thread.nativeGetSafeCulture(this, Thread.GetDomainID(), false, ref safeCulture) || safeCulture == null)
        return CultureInfo.UserDefaultCulture;
      return safeCulture;
    }

    [SecurityCritical]
    internal Context GetCurrentContextInternal()
    {
      if (this.m_Context == null)
        this.m_Context = Context.DefaultContext;
      return this.m_Context;
    }

    [SecurityCritical]
    private void SetPrincipalInternal(IPrincipal principal)
    {
      this.GetMutableExecutionContext().LogicalCallContext.SecurityData.Principal = principal;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern Context GetContextInternal(IntPtr id);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal object InternalCrossContextCallback(Context ctx, IntPtr ctxID, int appDomainID, InternalCrossContextDelegate ftnToCall, object[] args);

    [SecurityCritical]
    internal object InternalCrossContextCallback(Context ctx, InternalCrossContextDelegate ftnToCall, object[] args)
    {
      Context ctx1 = ctx;
      IntPtr internalContextId = ctx1.InternalContextID;
      int appDomainID = 0;
      InternalCrossContextDelegate ftnToCall1 = ftnToCall;
      object[] args1 = args;
      return this.InternalCrossContextCallback(ctx1, internalContextId, appDomainID, ftnToCall1, args1);
    }

    private static object CompleteCrossContextCallback(InternalCrossContextDelegate ftnToCall, object[] args)
    {
      return ftnToCall(args);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern AppDomain GetDomainInternal();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern AppDomain GetFastDomainInternal();

    /// <summary>返回当前线程正在其中运行的当前域。</summary>
    /// <returns>表示正在运行的线程的当前应用程序域的 <see cref="T:System.AppDomain" />。</returns>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public static AppDomain GetDomain()
    {
      return Thread.GetFastDomainInternal() ?? Thread.GetDomainInternal();
    }

    /// <summary>返回唯一的应用程序域标识符。</summary>
    /// <returns>唯一标识应用程序域的 32 位有符号整数。</returns>
    /// <filterpriority>2</filterpriority>
    public static int GetDomainID()
    {
      return Thread.GetDomain().GetId();
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void InformThreadNameChange(ThreadHandle t, string name, int len);

    /// <summary>通知宿主执行将要进入一个代码区域，在该代码区域内线程中止或未经处理异常的影响可能会危害应用程序域中的其他任务。</summary>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    public static extern void BeginCriticalRegion();

    /// <summary>通知宿主执行将要进入一个代码区域，在该代码区域内线程中止或未经处理异常的影响限于当前任务。</summary>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    public static extern void EndCriticalRegion();

    /// <summary>通知宿主托管代码将要执行依赖于当前物理操作系统线程的标识的指令。</summary>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
    /// </PermissionSet>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void BeginThreadAffinity();

    /// <summary>通知宿主托管代码已执行完依赖于当前物理操作系统线程的标识的指令。</summary>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
    /// </PermissionSet>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void EndThreadAffinity();

    /// <summary>读取字段值。无论处理器的数目或处理器缓存的状态如何，该值都是由计算机的任何处理器写入的最新值。</summary>
    /// <returns>由任何处理器写入字段的最新值。</returns>
    /// <param name="address">要读取的字段。</param>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static byte VolatileRead(ref byte address)
    {
      int num = (int) address;
      Thread.MemoryBarrier();
      return (byte) num;
    }

    /// <summary>读取字段值。无论处理器的数目或处理器缓存的状态如何，该值都是由计算机的任何处理器写入的最新值。</summary>
    /// <returns>由任何处理器写入字段的最新值。</returns>
    /// <param name="address">要读取的字段。</param>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static short VolatileRead(ref short address)
    {
      int num = (int) address;
      Thread.MemoryBarrier();
      return (short) num;
    }

    /// <summary>读取字段值。无论处理器的数目或处理器缓存的状态如何，该值都是由计算机的任何处理器写入的最新值。</summary>
    /// <returns>由任何处理器写入字段的最新值。</returns>
    /// <param name="address">要读取的字段。</param>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int VolatileRead(ref int address)
    {
      int num = address;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>读取字段值。无论处理器的数目或处理器缓存的状态如何，该值都是由计算机的任何处理器写入的最新值。</summary>
    /// <returns>由任何处理器写入字段的最新值。</returns>
    /// <param name="address">要读取的字段。</param>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static long VolatileRead(ref long address)
    {
      long num = address;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>读取字段值。无论处理器的数目或处理器缓存的状态如何，该值都是由计算机的任何处理器写入的最新值。</summary>
    /// <returns>由任何处理器写入字段的最新值。</returns>
    /// <param name="address">要读取的字段。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static sbyte VolatileRead(ref sbyte address)
    {
      int num = (int) address;
      Thread.MemoryBarrier();
      return (sbyte) num;
    }

    /// <summary>读取字段值。无论处理器的数目或处理器缓存的状态如何，该值都是由计算机的任何处理器写入的最新值。</summary>
    /// <returns>由任何处理器写入字段的最新值。</returns>
    /// <param name="address">要读取的字段。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static ushort VolatileRead(ref ushort address)
    {
      int num = (int) address;
      Thread.MemoryBarrier();
      return (ushort) num;
    }

    /// <summary>读取字段值。无论处理器的数目或处理器缓存的状态如何，该值都是由计算机的任何处理器写入的最新值。</summary>
    /// <returns>由任何处理器写入字段的最新值。</returns>
    /// <param name="address">要读取的字段。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static uint VolatileRead(ref uint address)
    {
      int num = (int) address;
      Thread.MemoryBarrier();
      return (uint) num;
    }

    /// <summary>读取字段值。无论处理器的数目或处理器缓存的状态如何，该值都是由计算机的任何处理器写入的最新值。</summary>
    /// <returns>由任何处理器写入字段的最新值。</returns>
    /// <param name="address">要读取的字段。</param>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static IntPtr VolatileRead(ref IntPtr address)
    {
      IntPtr num = address;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>读取字段值。无论处理器的数目或处理器缓存的状态如何，该值都是由计算机的任何处理器写入的最新值。</summary>
    /// <returns>由任何处理器写入字段的最新值。</returns>
    /// <param name="address">要读取的字段。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static UIntPtr VolatileRead(ref UIntPtr address)
    {
      IntPtr num = (IntPtr) address;
      Thread.MemoryBarrier();
      return (UIntPtr) num;
    }

    /// <summary>读取字段值。无论处理器的数目或处理器缓存的状态如何，该值都是由计算机的任何处理器写入的最新值。</summary>
    /// <returns>由任何处理器写入字段的最新值。</returns>
    /// <param name="address">要读取的字段。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static ulong VolatileRead(ref ulong address)
    {
      long num = (long) address;
      Thread.MemoryBarrier();
      return (ulong) num;
    }

    /// <summary>读取字段值。无论处理器的数目或处理器缓存的状态如何，该值都是由计算机的任何处理器写入的最新值。</summary>
    /// <returns>由任何处理器写入字段的最新值。</returns>
    /// <param name="address">要读取的字段。</param>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static float VolatileRead(ref float address)
    {
      double num = (double) address;
      Thread.MemoryBarrier();
      return (float) num;
    }

    /// <summary>读取字段值。无论处理器的数目或处理器缓存的状态如何，该值都是由计算机的任何处理器写入的最新值。</summary>
    /// <returns>由任何处理器写入字段的最新值。</returns>
    /// <param name="address">要读取的字段。</param>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static double VolatileRead(ref double address)
    {
      double num = address;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>读取字段值。无论处理器的数目或处理器缓存的状态如何，该值都是由计算机的任何处理器写入的最新值。</summary>
    /// <returns>由任何处理器写入字段的最新值。</returns>
    /// <param name="address">要读取的字段。</param>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static object VolatileRead(ref object address)
    {
      object obj = address;
      Thread.MemoryBarrier();
      return obj;
    }

    /// <summary>立即向字段写入一个值，以使该值对计算机中的所有处理器都可见。</summary>
    /// <param name="address">将向其中写入值的字段。</param>
    /// <param name="value">要写入的值。</param>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void VolatileWrite(ref byte address, byte value)
    {
      Thread.MemoryBarrier();
      address = value;
    }

    /// <summary>立即向字段写入一个值，以使该值对计算机中的所有处理器都可见。</summary>
    /// <param name="address">将向其中写入值的字段。</param>
    /// <param name="value">要写入的值。</param>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void VolatileWrite(ref short address, short value)
    {
      Thread.MemoryBarrier();
      address = value;
    }

    /// <summary>立即向字段写入一个值，以使该值对计算机中的所有处理器都可见。</summary>
    /// <param name="address">将向其中写入值的字段。</param>
    /// <param name="value">要写入的值。</param>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void VolatileWrite(ref int address, int value)
    {
      Thread.MemoryBarrier();
      address = value;
    }

    /// <summary>立即向字段写入一个值，以使该值对计算机中的所有处理器都可见。</summary>
    /// <param name="address">将向其中写入值的字段。</param>
    /// <param name="value">要写入的值。</param>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void VolatileWrite(ref long address, long value)
    {
      Thread.MemoryBarrier();
      address = value;
    }

    /// <summary>立即向字段写入一个值，以使该值对计算机中的所有处理器都可见。</summary>
    /// <param name="address">将向其中写入值的字段。</param>
    /// <param name="value">要写入的值。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void VolatileWrite(ref sbyte address, sbyte value)
    {
      Thread.MemoryBarrier();
      address = value;
    }

    /// <summary>立即向字段写入一个值，以使该值对计算机中的所有处理器都可见。</summary>
    /// <param name="address">将向其中写入值的字段。</param>
    /// <param name="value">要写入的值。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void VolatileWrite(ref ushort address, ushort value)
    {
      Thread.MemoryBarrier();
      address = value;
    }

    /// <summary>立即向字段写入一个值，以使该值对计算机中的所有处理器都可见。</summary>
    /// <param name="address">将向其中写入值的字段。</param>
    /// <param name="value">要写入的值。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void VolatileWrite(ref uint address, uint value)
    {
      Thread.MemoryBarrier();
      address = value;
    }

    /// <summary>立即向字段写入一个值，以使该值对计算机中的所有处理器都可见。</summary>
    /// <param name="address">将向其中写入值的字段。</param>
    /// <param name="value">要写入的值。</param>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void VolatileWrite(ref IntPtr address, IntPtr value)
    {
      Thread.MemoryBarrier();
      address = value;
    }

    /// <summary>立即向字段写入一个值，以使该值对计算机中的所有处理器都可见。</summary>
    /// <param name="address">将向其中写入值的字段。</param>
    /// <param name="value">要写入的值。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void VolatileWrite(ref UIntPtr address, UIntPtr value)
    {
      Thread.MemoryBarrier();
      address = value;
    }

    /// <summary>立即向字段写入一个值，以使该值对计算机中的所有处理器都可见。</summary>
    /// <param name="address">将向其中写入值的字段。</param>
    /// <param name="value">要写入的值。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void VolatileWrite(ref ulong address, ulong value)
    {
      Thread.MemoryBarrier();
      address = value;
    }

    /// <summary>立即向字段写入一个值，以使该值对计算机中的所有处理器都可见。</summary>
    /// <param name="address">将向其中写入值的字段。</param>
    /// <param name="value">要写入的值。</param>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void VolatileWrite(ref float address, float value)
    {
      Thread.MemoryBarrier();
      address = value;
    }

    /// <summary>立即向字段写入一个值，以使该值对计算机中的所有处理器都可见。</summary>
    /// <param name="address">将向其中写入值的字段。</param>
    /// <param name="value">要写入的值。</param>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void VolatileWrite(ref double address, double value)
    {
      Thread.MemoryBarrier();
      address = value;
    }

    /// <summary>立即向字段写入一个值，以使该值对计算机中的所有处理器都可见。</summary>
    /// <param name="address">将向其中写入值的字段。</param>
    /// <param name="value">要写入的值。</param>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void VolatileWrite(ref object address, object value)
    {
      Thread.MemoryBarrier();
      address = value;
    }

    /// <summary>按如下方式同步内存访问：执行当前线程的处理器在对指令重新排序时，不能采用先执行 <see cref="M:System.Threading.Thread.MemoryBarrier" /> 调用之后的内存存取，再执行 <see cref="M:System.Threading.Thread.MemoryBarrier" /> 调用之前的内存存取的方式。</summary>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void MemoryBarrier();

    void _Thread.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _Thread.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _Thread.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _Thread.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal void SetAbortReason(object o);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal object GetAbortReason();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal void ClearAbortReason();
  }
}
