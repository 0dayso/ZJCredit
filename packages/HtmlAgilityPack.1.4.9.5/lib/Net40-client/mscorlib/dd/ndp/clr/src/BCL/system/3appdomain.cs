// Decompiled with JetBrains decompiler
// Type: System.AppDomain
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Deployment.Internal.Isolation.Manifest;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Principal;
using System.Security.Util;
using System.Text;
using System.Threading;

namespace System
{
  /// <summary>表示应用程序域，它是一个应用程序在其中执行的独立环境。此类不能被继承。</summary>
  /// <filterpriority>1</filterpriority>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_AppDomain))]
  [ComVisible(true)]
  public sealed class AppDomain : MarshalByRefObject, _AppDomain, IEvidenceFactory
  {
    [SecurityCritical]
    private AppDomainManager _domainManager;
    private Dictionary<string, object[]> _LocalStore;
    private AppDomainSetup _FusionStore;
    private Evidence _SecurityIdentity;
    private object[] _Policies;
    [SecurityCritical]
    private ResolveEventHandler _TypeResolve;
    [SecurityCritical]
    private ResolveEventHandler _ResourceResolve;
    [SecurityCritical]
    private ResolveEventHandler _AssemblyResolve;
    private Context _DefaultContext;
    private ActivationContext _activationContext;
    private ApplicationIdentity _applicationIdentity;
    private ApplicationTrust _applicationTrust;
    private IPrincipal _DefaultPrincipal;
    private DomainSpecificRemotingData _RemotingData;
    private EventHandler _processExit;
    private EventHandler _domainUnload;
    private UnhandledExceptionEventHandler _unhandledException;
    private string[] _aptcaVisibleAssemblies;
    private Dictionary<string, object> _compatFlags;
    private EventHandler<FirstChanceExceptionEventArgs> _firstChanceException;
    private IntPtr _pDomain;
    private PrincipalPolicy _PrincipalPolicy;
    private bool _HasSetPolicy;
    private bool _IsFastFullTrustDomain;
    private bool _compatFlagsInitialized;
    internal const string TargetFrameworkNameAppCompatSetting = "TargetFrameworkName";
    private static AppDomain.APPX_FLAGS s_flags;
    internal const int DefaultADID = 1;

    private static AppDomain.APPX_FLAGS Flags
    {
      [SecuritySafeCritical] get
      {
        if (AppDomain.s_flags == (AppDomain.APPX_FLAGS) 0)
          AppDomain.s_flags = AppDomain.nGetAppXFlags();
        return AppDomain.s_flags;
      }
    }

    internal static bool ProfileAPICheck
    {
      [SecuritySafeCritical] get
      {
        return (uint) (AppDomain.Flags & AppDomain.APPX_FLAGS.APPX_FLAGS_API_CHECK) > 0U;
      }
    }

    internal static bool IsAppXNGen
    {
      [SecuritySafeCritical] get
      {
        return (uint) (AppDomain.Flags & AppDomain.APPX_FLAGS.APPX_FLAGS_APPX_NGEN) > 0U;
      }
    }

    internal string[] PartialTrustVisibleAssemblies
    {
      get
      {
        return this._aptcaVisibleAssemblies;
      }
      [SecuritySafeCritical] set
      {
        this._aptcaVisibleAssemblies = value;
        string canonicalList = (string) null;
        if (value != null)
        {
          StringBuilder sb = StringBuilderCache.Acquire(16);
          for (int index = 0; index < value.Length; ++index)
          {
            if (value[index] != null)
            {
              sb.Append(value[index].ToUpperInvariant());
              if (index != value.Length - 1)
                sb.Append(';');
            }
          }
          canonicalList = StringBuilderCache.GetStringAndRelease(sb);
        }
        this.SetCanonicalConditionalAptcaList(canonicalList);
      }
    }

    /// <summary>获得初始化应用程序域时主机提供的域管理器。</summary>
    /// <returns>一个对象，表示初始化应用程序域时主机提供的域管理器；或者如果没有提供域管理器，则返回 null。</returns>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlDomainPolicy" />
    /// </PermissionSet>
    public AppDomainManager DomainManager
    {
      [SecurityCritical] get
      {
        return this._domainManager;
      }
    }

    internal HostSecurityManager HostSecurityManager
    {
      [SecurityCritical] get
      {
        HostSecurityManager hostSecurityManager = (HostSecurityManager) null;
        AppDomainManager domainManager = AppDomain.CurrentDomain.DomainManager;
        if (domainManager != null)
          hostSecurityManager = domainManager.HostSecurityManager;
        if (hostSecurityManager == null)
          hostSecurityManager = new HostSecurityManager();
        return hostSecurityManager;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Threading.Thread" /> 的当前应用程序域。</summary>
    /// <returns>当前应用程序域。</returns>
    /// <filterpriority>1</filterpriority>
    public static AppDomain CurrentDomain
    {
      get
      {
        return Thread.GetDomain();
      }
    }

    /// <summary>获取与该应用程序域关联的 <see cref="T:System.Security.Policy.Evidence" />。</summary>
    /// <returns>与该应用程序域关联的证据。</returns>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    public Evidence Evidence
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, ControlEvidence = true)] get
      {
        return this.EvidenceNoDemand;
      }
    }

    internal Evidence EvidenceNoDemand
    {
      [SecurityCritical] get
      {
        if (this._SecurityIdentity != null)
          return this._SecurityIdentity.Clone();
        if (!this.IsDefaultAppDomain() && this.nIsDefaultAppDomainForEvidence())
          return AppDomain.GetDefaultDomain().Evidence;
        return new Evidence((IRuntimeEvidenceFactory) new AppDomainEvidenceFactory(this));
      }
    }

    internal Evidence InternalEvidence
    {
      get
      {
        return this._SecurityIdentity;
      }
    }

    /// <summary>获取此应用程序域的友好名称。</summary>
    /// <returns>此应用程序域的友好名称。</returns>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    public string FriendlyName
    {
      [SecuritySafeCritical] get
      {
        return this.nGetFriendlyName();
      }
    }

    /// <summary>获取基目录，它由程序集冲突解决程序用来探测程序集。</summary>
    /// <returns>基目录，由程序集冲突解决程序用来探测程序集。</returns>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public string BaseDirectory
    {
      get
      {
        return this.FusionStore.ApplicationBase;
      }
    }

    /// <summary>获取基目录下的路径，在此程序集冲突解决程序应探测专用程序集。</summary>
    /// <returns>基目录下的路径，在此程序集冲突解决程序应探测专用程序集。</returns>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public string RelativeSearchPath
    {
      get
      {
        return this.FusionStore.PrivateBinPath;
      }
    }

    /// <summary>获取应用程序域是否配置为影像副本文件的指示。</summary>
    /// <returns>如果应用程序域配置为卷影副本文件，则为 true；否则为 false。</returns>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    public bool ShadowCopyFiles
    {
      get
      {
        string shadowCopyFiles = this.FusionStore.ShadowCopyFiles;
        return shadowCopyFiles != null && string.Compare(shadowCopyFiles, "true", StringComparison.OrdinalIgnoreCase) == 0;
      }
    }

    /// <summary>获取当前应用程序域的激活上下文。</summary>
    /// <returns>一个对象，表示当前应用程序域的激活上下文；或者如果域没有激活上下文，则返回 null。</returns>
    /// <filterpriority>1</filterpriority>
    public ActivationContext ActivationContext
    {
      [SecurityCritical] get
      {
        return this._activationContext;
      }
    }

    /// <summary>获得应用程序域中的应用程序标识。</summary>
    /// <returns>标识应用程序域中应用程序的对象。</returns>
    public ApplicationIdentity ApplicationIdentity
    {
      [SecurityCritical] get
      {
        return this._applicationIdentity;
      }
    }

    /// <summary>获取说明授予应用程序的权限以及应用程序是否拥有允许其运行的信任级别的信息。</summary>
    /// <returns>封装应用程序域中应用程序的权限及信任信息的对象。</returns>
    public ApplicationTrust ApplicationTrust
    {
      [SecurityCritical] get
      {
        if (this._applicationTrust == null && this._IsFastFullTrustDomain)
          this._applicationTrust = new ApplicationTrust(new PermissionSet(PermissionState.Unrestricted));
        return this._applicationTrust;
      }
    }

    /// <summary>获取目录，它由程序集冲突解决程序用来探测动态创建的程序集。</summary>
    /// <returns>目录，它由程序集冲突解决程序用来探测动态创建的程序集。</returns>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public string DynamicDirectory
    {
      [SecuritySafeCritical] get
      {
        string dynamicDir = this.GetDynamicDir();
        if (dynamicDir != null)
          new FileIOPermission(FileIOPermissionAccess.PathDiscovery, dynamicDir).Demand();
        return dynamicDir;
      }
    }

    internal DomainSpecificRemotingData RemotingData
    {
      get
      {
        if (this._RemotingData == null)
          this.CreateRemotingData();
        return this._RemotingData;
      }
    }

    internal AppDomainSetup FusionStore
    {
      get
      {
        return this._FusionStore;
      }
    }

    private Dictionary<string, object[]> LocalStore
    {
      get
      {
        if (this._LocalStore != null)
          return this._LocalStore;
        this._LocalStore = new Dictionary<string, object[]>();
        return this._LocalStore;
      }
    }

    /// <summary>获取此实例的应用程序域配置信息。</summary>
    /// <returns>应用程序域初始化信息。</returns>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    public AppDomainSetup SetupInformation
    {
      get
      {
        return new AppDomainSetup(this.FusionStore, true);
      }
    }

    /// <summary>获取沙盒应用程序域的权限集。</summary>
    /// <returns>沙盒应用程序域的权限集。</returns>
    public PermissionSet PermissionSet
    {
      [SecurityCritical] get
      {
        PermissionSet o = (PermissionSet) null;
        AppDomain.GetGrantSet(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<PermissionSet>(ref o));
        if (o != null)
          return o.Copy();
        return new PermissionSet(PermissionState.Unrestricted);
      }
    }

    /// <summary>获取一个值，该值指示加载到当前应用程序域的程序集是否是以完全信任方式执行的。</summary>
    /// <returns>如果加载到当前应用程序域的程序集是以完全信任方式执行的，则为 true；否则为 false。</returns>
    public bool IsFullyTrusted
    {
      [SecuritySafeCritical] get
      {
        PermissionSet o = (PermissionSet) null;
        AppDomain.GetGrantSet(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<PermissionSet>(ref o));
        if (o != null)
          return o.IsUnrestricted();
        return true;
      }
    }

    /// <summary>获取一个值，该值指示当前应用程序域是否拥有一个为加载到该应用程序域的所有程序集授予的权限集。</summary>
    /// <returns>如果当前应用程序域具有一组同构权限，则为 true；否则，为 false。</returns>
    public bool IsHomogenous
    {
      get
      {
        if (!this._IsFastFullTrustDomain)
          return this._applicationTrust != null;
        return true;
      }
    }

    internal bool IsLegacyCasPolicyEnabled
    {
      [SecuritySafeCritical] get
      {
        return AppDomain.GetIsLegacyCasPolicyEnabled(this.GetNativeHandle());
      }
    }

    /// <summary>获得一个整数，该整数唯一标识进程中的应用程序域。</summary>
    /// <returns>标识应用程序域的整数。</returns>
    /// <filterpriority>2</filterpriority>
    public int Id
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get
      {
        return this.GetId();
      }
    }

    /// <summary>获取或设置一个值，该值指示是否对当前进程启用应用程序域的 CPU 和内存监视。一旦对进程启用了监视，则无法将其禁用。</summary>
    /// <returns>如果启用监视，则为 true；否则为 false。</returns>
    /// <exception cref="T:System.ArgumentException">当前进程尝试将值分配 false 给此属性。</exception>
    public static bool MonitoringIsEnabled
    {
      [SecurityCritical] get
      {
        return AppDomain.nMonitoringIsEnabled();
      }
      [SecurityCritical] set
      {
        if (!value)
          throw new ArgumentException(Environment.GetResourceString("Arg_MustBeTrue"));
        AppDomain.nEnableMonitoring();
      }
    }

    /// <summary>获取自从进程启动后所有线程在当前应用程序域中执行时所使用的总处理器时间。</summary>
    /// <returns>当前应用程序域的总处理器时间。</returns>
    /// <exception cref="T:System.InvalidOperationException">static (Shared 在 Visual Basic 中） <see cref="P:System.AppDomain.MonitoringIsEnabled" /> 属性设置为 false。</exception>
    public TimeSpan MonitoringTotalProcessorTime
    {
      [SecurityCritical] get
      {
        long totalProcessorTime = this.nGetTotalProcessorTime();
        long num = -1;
        if (totalProcessorTime == num)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WithoutARM"));
        return new TimeSpan(totalProcessorTime);
      }
    }

    /// <summary>获取自从创建应用程序域后由应用程序域进行的所有内存分配的总大小（以字节为单位，不扣除已回收的内存）。</summary>
    /// <returns>所有内存分配的总大小。</returns>
    /// <exception cref="T:System.InvalidOperationException">static (Shared 在 Visual Basic 中） <see cref="P:System.AppDomain.MonitoringIsEnabled" /> 属性设置为 false。</exception>
    public long MonitoringTotalAllocatedMemorySize
    {
      [SecurityCritical] get
      {
        long allocatedMemorySize = this.nGetTotalAllocatedMemorySize();
        long num = -1;
        if (allocatedMemorySize != num)
          return allocatedMemorySize;
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WithoutARM"));
      }
    }

    /// <summary>获取上次回收后保留下来的，已知由当前应用程序域引用的字节数。</summary>
    /// <returns>保留下来的字节数。</returns>
    /// <exception cref="T:System.InvalidOperationException">static (Shared 在 Visual Basic 中） <see cref="P:System.AppDomain.MonitoringIsEnabled" /> 属性设置为 false。</exception>
    public long MonitoringSurvivedMemorySize
    {
      [SecurityCritical] get
      {
        long survivedMemorySize = this.nGetLastSurvivedMemorySize();
        long num = -1;
        if (survivedMemorySize != num)
          return survivedMemorySize;
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WithoutARM"));
      }
    }

    /// <summary>获取进程中所有应用程序域的上次回收后保留下来的总字节数。</summary>
    /// <returns>进程的保留下来的总字节数。</returns>
    /// <exception cref="T:System.InvalidOperationException">static (Shared 在 Visual Basic 中） <see cref="P:System.AppDomain.MonitoringIsEnabled" /> 属性设置为 false。</exception>
    public static long MonitoringSurvivedProcessMemorySize
    {
      [SecurityCritical] get
      {
        long processMemorySize = AppDomain.nGetLastSurvivedProcessMemorySize();
        long num = -1;
        if (processMemorySize != num)
          return processMemorySize;
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WithoutARM"));
      }
    }

    /// <summary>在加载程序集时发生。</summary>
    /// <filterpriority>2</filterpriority>
    public event AssemblyLoadEventHandler AssemblyLoad;

    /// <summary>在对类型的解析失败时发生。</summary>
    /// <filterpriority>2</filterpriority>
    public event ResolveEventHandler TypeResolve
    {
      [SecurityCritical] add
      {
        lock (this)
          this._TypeResolve = this._TypeResolve + value;
      }
      [SecurityCritical] remove
      {
        lock (this)
          this._TypeResolve = this._TypeResolve - value;
      }
    }

    /// <summary>当资源解析因资源不是程序集中的有效链接资源或嵌入资源而失败时发生。</summary>
    /// <filterpriority>2</filterpriority>
    public event ResolveEventHandler ResourceResolve
    {
      [SecurityCritical] add
      {
        lock (this)
          this._ResourceResolve = this._ResourceResolve + value;
      }
      [SecurityCritical] remove
      {
        lock (this)
          this._ResourceResolve = this._ResourceResolve - value;
      }
    }

    /// <summary>在对程序集的解析失败时发生。</summary>
    /// <filterpriority>2</filterpriority>
    public event ResolveEventHandler AssemblyResolve
    {
      [SecurityCritical] add
      {
        lock (this)
          this._AssemblyResolve = this._AssemblyResolve + value;
      }
      [SecurityCritical] remove
      {
        lock (this)
          this._AssemblyResolve = this._AssemblyResolve - value;
      }
    }

    /// <summary>当程序集的解析在仅限反射的上下文中失败时发生。</summary>
    public event ResolveEventHandler ReflectionOnlyAssemblyResolve;

    /// <summary>当默认应用程序域的父进程存在时发生。</summary>
    /// <filterpriority>2</filterpriority>
    public event EventHandler ProcessExit
    {
      [SecuritySafeCritical] add
      {
        if (value == null)
          return;
        RuntimeHelpers.PrepareContractedDelegate((Delegate) value);
        lock (this)
          this._processExit = this._processExit + value;
      }
      remove
      {
        lock (this)
          this._processExit = this._processExit - value;
      }
    }

    /// <summary>在即将卸载 <see cref="T:System.AppDomain" /> 时发生。</summary>
    /// <filterpriority>2</filterpriority>
    public event EventHandler DomainUnload
    {
      [SecuritySafeCritical] add
      {
        if (value == null)
          return;
        RuntimeHelpers.PrepareContractedDelegate((Delegate) value);
        lock (this)
          this._domainUnload = this._domainUnload + value;
      }
      remove
      {
        lock (this)
          this._domainUnload = this._domainUnload - value;
      }
    }

    /// <summary>当某个异常未被捕获时出现。</summary>
    /// <filterpriority>2</filterpriority>
    public event UnhandledExceptionEventHandler UnhandledException
    {
      [SecurityCritical] add
      {
        if (value == null)
          return;
        RuntimeHelpers.PrepareContractedDelegate((Delegate) value);
        lock (this)
          this._unhandledException = this._unhandledException + value;
      }
      [SecurityCritical] remove
      {
        lock (this)
          this._unhandledException = this._unhandledException - value;
      }
    }

    /// <summary>当托管代码抛出异常时发生，在运行时在调用堆栈中搜索应用程序域中的异常处理程序之前。</summary>
    public event EventHandler<FirstChanceExceptionEventArgs> FirstChanceException
    {
      [SecurityCritical] add
      {
        if (value == null)
          return;
        RuntimeHelpers.PrepareContractedDelegate((Delegate) value);
        lock (this)
          this._firstChanceException = this._firstChanceException + value;
      }
      [SecurityCritical] remove
      {
        lock (this)
          this._firstChanceException = this._firstChanceException - value;
      }
    }

    private AppDomain()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_Constructor"));
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DisableFusionUpdatesFromADManager(AppDomainHandle domain);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.I4)]
    private static extern AppDomain.APPX_FLAGS nGetAppXFlags();

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetAppDomainManagerType(AppDomainHandle domain, StringHandleOnStack retAssembly, StringHandleOnStack retType);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void SetAppDomainManagerType(AppDomainHandle domain, string assembly, string type);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void nSetHostSecurityManagerFlags(HostSecurityManagerOptions flags);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void SetSecurityHomogeneousFlag(AppDomainHandle domain, [MarshalAs(UnmanagedType.Bool)] bool runtimeSuppliedHomogenousGrantSet);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void SetLegacyCasPolicyEnabled(AppDomainHandle domain);

    [SecurityCritical]
    private void SetLegacyCasPolicyEnabled()
    {
      AppDomain.SetLegacyCasPolicyEnabled(this.GetNativeHandle());
    }

    internal AppDomainHandle GetNativeHandle()
    {
      if (this._pDomain.IsNull())
        throw new InvalidOperationException(Environment.GetResourceString("Argument_InvalidHandle"));
      return new AppDomainHandle(this._pDomain);
    }

    [SecuritySafeCritical]
    private void CreateAppDomainManager()
    {
      AppDomainSetup fusionStore = this.FusionStore;
      string assembly;
      string type;
      this.GetAppDomainManagerType(out assembly, out type);
      if (assembly != null)
      {
        if (type != null)
        {
          try
          {
            new PermissionSet(PermissionState.Unrestricted).Assert();
            this._domainManager = this.CreateInstanceAndUnwrap(assembly, type) as AppDomainManager;
            CodeAccessPermission.RevertAssert();
          }
          catch (FileNotFoundException ex)
          {
            throw new TypeLoadException(Environment.GetResourceString("Argument_NoDomainManager"), (Exception) ex);
          }
          catch (SecurityException ex)
          {
            throw new TypeLoadException(Environment.GetResourceString("Argument_NoDomainManager"), (Exception) ex);
          }
          catch (TypeLoadException ex)
          {
            throw new TypeLoadException(Environment.GetResourceString("Argument_NoDomainManager"), (Exception) ex);
          }
          if (this._domainManager == null)
            throw new TypeLoadException(Environment.GetResourceString("Argument_NoDomainManager"));
          this.FusionStore.AppDomainManagerAssembly = assembly;
          this.FusionStore.AppDomainManagerType = type;
          int num = !(this._domainManager.GetType() != typeof (AppDomainManager)) ? 0 : (!this.DisableFusionUpdatesFromADManager() ? 1 : 0);
          AppDomainSetup oldInfo = (AppDomainSetup) null;
          if (num != 0)
            oldInfo = new AppDomainSetup(this.FusionStore, true);
          this._domainManager.InitializeNewDomain(this.FusionStore);
          if (num != 0)
            this.SetupFusionStore(this._FusionStore, oldInfo);
          if ((this._domainManager.InitializationFlags & AppDomainManagerInitializationOptions.RegisterWithHost) == AppDomainManagerInitializationOptions.RegisterWithHost)
            this._domainManager.RegisterWithHost();
        }
      }
      this.InitializeCompatibilityFlags();
    }

    private void InitializeCompatibilityFlags()
    {
      AppDomainSetup fusionStore = this.FusionStore;
      if (fusionStore.GetCompatibilityFlags() != null)
        this._compatFlags = new Dictionary<string, object>((IDictionary<string, object>) fusionStore.GetCompatibilityFlags(), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      this._compatFlagsInitialized = true;
      CompatibilitySwitches.InitializeSwitches();
    }

    [SecuritySafeCritical]
    internal string GetTargetFrameworkName()
    {
      string str = this._FusionStore.TargetFrameworkName;
      if (str == null && this.IsDefaultAppDomain() && !this._FusionStore.CheckedForTargetFrameworkName)
      {
        Assembly entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly != (Assembly) null)
        {
          TargetFrameworkAttribute[] frameworkAttributeArray = (TargetFrameworkAttribute[]) entryAssembly.GetCustomAttributes(typeof (TargetFrameworkAttribute));
          if (frameworkAttributeArray != null && frameworkAttributeArray.Length != 0)
          {
            str = frameworkAttributeArray[0].FrameworkName;
            this._FusionStore.TargetFrameworkName = str;
          }
        }
        this._FusionStore.CheckedForTargetFrameworkName = true;
      }
      return str;
    }

    [SecuritySafeCritical]
    internal bool DisableFusionUpdatesFromADManager()
    {
      return AppDomain.DisableFusionUpdatesFromADManager(this.GetNativeHandle());
    }

    [SecuritySafeCritical]
    internal static bool IsAppXModel()
    {
      return (uint) (AppDomain.Flags & AppDomain.APPX_FLAGS.APPX_FLAGS_APPX_MODEL) > 0U;
    }

    [SecuritySafeCritical]
    internal static bool IsAppXDesignMode()
    {
      return (AppDomain.Flags & AppDomain.APPX_FLAGS.APPX_FLAGS_APPX_MASK) == (AppDomain.APPX_FLAGS.APPX_FLAGS_APPX_MODEL | AppDomain.APPX_FLAGS.APPX_FLAGS_APPX_DESIGN_MODE);
    }

    [SecuritySafeCritical]
    internal static void CheckLoadFromSupported()
    {
      if (AppDomain.IsAppXModel())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_AppX", (object) "Assembly.LoadFrom"));
    }

    [SecuritySafeCritical]
    internal static void CheckLoadFileSupported()
    {
      if (AppDomain.IsAppXModel())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_AppX", (object) "Assembly.LoadFile"));
    }

    [SecuritySafeCritical]
    internal static void CheckReflectionOnlyLoadSupported()
    {
      if (AppDomain.IsAppXModel())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_AppX", (object) "Assembly.ReflectionOnlyLoad"));
    }

    [SecuritySafeCritical]
    internal static void CheckLoadWithPartialNameSupported(StackCrawlMark stackMark)
    {
      if (!AppDomain.IsAppXModel())
        return;
      RuntimeAssembly executingAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackMark);
      if ((!((Assembly) executingAssembly != (Assembly) null) ? 0 : (executingAssembly.IsFrameworkAssembly() ? 1 : 0)) == 0)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_AppX", (object) "Assembly.LoadWithPartialName"));
    }

    [SecuritySafeCritical]
    internal static void CheckDefinePInvokeSupported()
    {
      if (AppDomain.IsAppXModel())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_AppX", (object) "DefinePInvokeMethod"));
    }

    [SecuritySafeCritical]
    internal static void CheckLoadByteArraySupported()
    {
      if (AppDomain.IsAppXModel())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_AppX", (object) "Assembly.Load(byte[], ...)"));
    }

    [SecuritySafeCritical]
    internal static void CheckCreateDomainSupported()
    {
      if (AppDomain.IsAppXModel() && !AppDomain.IsAppXDesignMode())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_AppX", (object) "AppDomain.CreateDomain"));
    }

    [SecuritySafeCritical]
    internal void GetAppDomainManagerType(out string assembly, out string type)
    {
      string s1 = (string) null;
      string s2 = (string) null;
      AppDomain.GetAppDomainManagerType(this.GetNativeHandle(), JitHelpers.GetStringHandleOnStack(ref s1), JitHelpers.GetStringHandleOnStack(ref s2));
      assembly = s1;
      type = s2;
    }

    [SecuritySafeCritical]
    private void SetAppDomainManagerType(string assembly, string type)
    {
      AppDomain.SetAppDomainManagerType(this.GetNativeHandle(), assembly, type);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void SetCanonicalConditionalAptcaList(AppDomainHandle appDomain, string canonicalList);

    [SecurityCritical]
    private void SetCanonicalConditionalAptcaList(string canonicalList)
    {
      AppDomain.SetCanonicalConditionalAptcaList(this.GetNativeHandle(), canonicalList);
    }

    private void SetupDefaultClickOnceDomain(string fullName, string[] manifestPaths, string[] activationData)
    {
      this.FusionStore.ActivationArguments = new ActivationArguments(fullName, manifestPaths, activationData);
    }

    [SecurityCritical]
    private void InitializeDomainSecurity(Evidence providedSecurityInfo, Evidence creatorsSecurityInfo, bool generateDefaultEvidence, IntPtr parentSecurityDescriptor, bool publishAppDomain)
    {
      AppDomainSetup fusionStore = this.FusionStore;
      if (CompatibilitySwitches.IsNetFx40LegacySecurityPolicy)
        this.SetLegacyCasPolicyEnabled();
      if (fusionStore.ActivationArguments != null)
      {
        ActivationContext activationContext = (ActivationContext) null;
        ApplicationIdentity applicationIdentity = (ApplicationIdentity) null;
        CmsUtils.CreateActivationContext(fusionStore.ActivationArguments.ApplicationFullName, fusionStore.ActivationArguments.ApplicationManifestPaths, fusionStore.ActivationArguments.UseFusionActivationContext, out applicationIdentity, out activationContext);
        string[] activationData = fusionStore.ActivationArguments.ActivationData;
        providedSecurityInfo = CmsUtils.MergeApplicationEvidence(providedSecurityInfo, applicationIdentity, activationContext, activationData, fusionStore.ApplicationTrust);
        this.SetupApplicationHelper(providedSecurityInfo, creatorsSecurityInfo, applicationIdentity, activationContext, activationData);
      }
      else
      {
        bool runtimeSuppliedHomogenousGrantSet = false;
        ApplicationTrust applicationTrust = fusionStore.ApplicationTrust;
        if (applicationTrust == null && !this.IsLegacyCasPolicyEnabled)
        {
          this._IsFastFullTrustDomain = true;
          runtimeSuppliedHomogenousGrantSet = true;
        }
        if (applicationTrust != null)
          this.SetupDomainSecurityForHomogeneousDomain(applicationTrust, runtimeSuppliedHomogenousGrantSet);
        else if (this._IsFastFullTrustDomain)
          AppDomain.SetSecurityHomogeneousFlag(this.GetNativeHandle(), runtimeSuppliedHomogenousGrantSet);
      }
      Evidence evidence = providedSecurityInfo != null ? providedSecurityInfo : creatorsSecurityInfo;
      if (evidence == null & generateDefaultEvidence)
        evidence = new Evidence((IRuntimeEvidenceFactory) new AppDomainEvidenceFactory(this));
      if (this._domainManager != null)
      {
        HostSecurityManager hostSecurityManager = this._domainManager.HostSecurityManager;
        if (hostSecurityManager != null)
        {
          AppDomain.nSetHostSecurityManagerFlags(hostSecurityManager.Flags);
          if ((hostSecurityManager.Flags & HostSecurityManagerOptions.HostAppDomainEvidence) == HostSecurityManagerOptions.HostAppDomainEvidence)
          {
            evidence = hostSecurityManager.ProvideAppDomainEvidence(evidence);
            if (evidence != null && evidence.Target == null)
              evidence.Target = (IRuntimeEvidenceFactory) new AppDomainEvidenceFactory(this);
          }
        }
      }
      this._SecurityIdentity = evidence;
      this.SetupDomainSecurity(evidence, parentSecurityDescriptor, publishAppDomain);
      if (this._domainManager == null)
        return;
      this.RunDomainManagerPostInitialization(this._domainManager);
    }

    [SecurityCritical]
    private void RunDomainManagerPostInitialization(AppDomainManager domainManager)
    {
      HostExecutionContextManager executionContextManager = domainManager.HostExecutionContextManager;
      if (!this.IsLegacyCasPolicyEnabled)
        return;
      HostSecurityManager hostSecurityManager = domainManager.HostSecurityManager;
      if (hostSecurityManager == null || (hostSecurityManager.Flags & HostSecurityManagerOptions.HostPolicyLevel) != HostSecurityManagerOptions.HostPolicyLevel)
        return;
      PolicyLevel domainPolicy = hostSecurityManager.DomainPolicy;
      if (domainPolicy == null)
        return;
      this.SetAppDomainPolicy(domainPolicy);
    }

    [SecurityCritical]
    private void SetupApplicationHelper(Evidence providedSecurityInfo, Evidence creatorsSecurityInfo, ApplicationIdentity appIdentity, ActivationContext activationContext, string[] activationData)
    {
      ApplicationTrust applicationTrust = AppDomain.CurrentDomain.HostSecurityManager.DetermineApplicationTrust(providedSecurityInfo, creatorsSecurityInfo, new TrustManagerContext());
      if (applicationTrust == null || !applicationTrust.IsApplicationTrustedToRun)
        throw new PolicyException(Environment.GetResourceString("Policy_NoExecutionPermission"), -2146233320, (Exception) null);
      if (activationContext != null)
        this.SetupDomainForApplication(activationContext, activationData);
      this.SetupDomainSecurityForApplication(appIdentity, applicationTrust);
    }

    [SecurityCritical]
    private void SetupDomainForApplication(ActivationContext activationContext, string[] activationData)
    {
      if (this.IsDefaultAppDomain())
      {
        AppDomainSetup fusionStore = this.FusionStore;
        fusionStore.ActivationArguments = new ActivationArguments(activationContext, activationData);
        string entryPointFullPath = CmsUtils.GetEntryPointFullPath(activationContext);
        if (!string.IsNullOrEmpty(entryPointFullPath))
          fusionStore.SetupDefaults(entryPointFullPath, false);
        else
          fusionStore.ApplicationBase = activationContext.ApplicationDirectory;
        this.SetupFusionStore(fusionStore, (AppDomainSetup) null);
      }
      activationContext.PrepareForExecution();
      int num1 = (int) activationContext.SetApplicationState(ActivationContext.ApplicationState.Starting);
      int num2 = (int) activationContext.SetApplicationState(ActivationContext.ApplicationState.Running);
      IPermission permission = (IPermission) null;
      string dataDirectory = activationContext.DataDirectory;
      if (dataDirectory != null && dataDirectory.Length > 0)
        permission = (IPermission) new FileIOPermission(FileIOPermissionAccess.PathDiscovery, dataDirectory);
      this.SetData("DataDirectory", (object) dataDirectory, permission);
      this._activationContext = activationContext;
    }

    [SecurityCritical]
    private void SetupDomainSecurityForApplication(ApplicationIdentity appIdentity, ApplicationTrust appTrust)
    {
      this._applicationIdentity = appIdentity;
      this.SetupDomainSecurityForHomogeneousDomain(appTrust, false);
    }

    [SecurityCritical]
    private void SetupDomainSecurityForHomogeneousDomain(ApplicationTrust appTrust, bool runtimeSuppliedHomogenousGrantSet)
    {
      if (runtimeSuppliedHomogenousGrantSet)
        this._FusionStore.ApplicationTrust = (ApplicationTrust) null;
      this._applicationTrust = appTrust;
      AppDomain.SetSecurityHomogeneousFlag(this.GetNativeHandle(), runtimeSuppliedHomogenousGrantSet);
    }

    [SecuritySafeCritical]
    private int ActivateApplication()
    {
      return (int) Activator.CreateInstance(AppDomain.CurrentDomain.ActivationContext).Unwrap();
    }

    private Assembly ResolveAssemblyForIntrospection(object sender, ResolveEventArgs args)
    {
      return Assembly.ReflectionOnlyLoad(this.ApplyPolicy(args.Name));
    }

    [SecuritySafeCritical]
    private void EnableResolveAssembliesForIntrospection(string verifiedFileDirectory)
    {
      AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += new ResolveEventHandler(this.ResolveAssemblyForIntrospection);
      string[] strArray = (string[]) null;
      if (verifiedFileDirectory != null)
        strArray = new string[1]
        {
          verifiedFileDirectory
        };
      WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve += new EventHandler<NamespaceResolveEventArgs>(new AppDomain.NamespaceResolverForIntrospection((IEnumerable<string>) strArray).ResolveNamespace);
    }

    /// <summary>以指定名称和访问模式定义动态程序集。</summary>
    /// <returns>含指定名称和访问模式的动态程序集。</returns>
    /// <param name="name">动态程序集的唯一标识。</param>
    /// <param name="access">动态程序集的访问模式。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">Name 属性 <paramref name="name" /> 是 null。- 或 - Name 属性 <paramref name="name" /> 开头空白区域，或包含向前或向后的反斜杠。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalDefineDynamicAssembly(name, access, (string) null, (Evidence) null, (PermissionSet) null, (PermissionSet) null, (PermissionSet) null, ref stackMark, (IEnumerable<CustomAttributeBuilder>) null, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>使用指定的名称、访问模式和自定义特性定义动态程序集。</summary>
    /// <returns>具有指定名称和功能的动态程序集。</returns>
    /// <param name="name">动态程序集的唯一标识。</param>
    /// <param name="access">动态程序集的访问模式。</param>
    /// <param name="assemblyAttributes">要应用于程序集的可枚举特性列表；如果无特性，则为 null。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">Name 属性 <paramref name="name" /> 是 null。- 或 - Name 属性 <paramref name="name" /> 开头为空白区域，或包含向前或向后的反斜杠。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, IEnumerable<CustomAttributeBuilder> assemblyAttributes)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalDefineDynamicAssembly(name, access, (string) null, (Evidence) null, (PermissionSet) null, (PermissionSet) null, (PermissionSet) null, ref stackMark, assemblyAttributes, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>定义具有指定名称、访问模式和自定义特性的动态程序集，并将指定源用于动态程序集的安全上下文。</summary>
    /// <returns>具有指定名称和功能的动态程序集。</returns>
    /// <param name="name">动态程序集的唯一标识。</param>
    /// <param name="access">动态程序集的访问模式。</param>
    /// <param name="assemblyAttributes">要应用于程序集的可枚举特性列表；如果无特性，则为 null。</param>
    /// <param name="securityContextSource">安全上下文的源。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">Name 属性 <paramref name="name" /> 是 null。- 或 - Name 属性 <paramref name="name" /> 开头为空白区域，或包含向前或向后的反斜杠。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">值 <paramref name="securityContextSource" /> 不是枚举值之一。</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, IEnumerable<CustomAttributeBuilder> assemblyAttributes, SecurityContextSource securityContextSource)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalDefineDynamicAssembly(name, access, (string) null, (Evidence) null, (PermissionSet) null, (PermissionSet) null, (PermissionSet) null, ref stackMark, assemblyAttributes, securityContextSource);
    }

    /// <summary>使用指定名称、访问模式和存储目录定义动态程序集。</summary>
    /// <returns>具有指定名称和功能的动态程序集。</returns>
    /// <param name="name">动态程序集的唯一标识。</param>
    /// <param name="access">访问动态程序集所采用的模式。</param>
    /// <param name="dir">保存程序集的目录的名称。如果 <paramref name="dir" /> 是 null，目录将默认为当前目录。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">Name 属性 <paramref name="name" /> 是 null。- 或 - Name 属性 <paramref name="name" /> 开头空白区域，或包含向前或向后的反斜杠。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalDefineDynamicAssembly(name, access, dir, (Evidence) null, (PermissionSet) null, (PermissionSet) null, (PermissionSet) null, ref stackMark, (IEnumerable<CustomAttributeBuilder>) null, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>使用指定名称、访问模式和证据定义动态程序集。</summary>
    /// <returns>具有指定名称和功能的动态程序集。</returns>
    /// <param name="name">动态程序集的唯一标识。</param>
    /// <param name="access">访问动态程序集所采用的模式。</param>
    /// <param name="evidence">为动态程序集提供的证据。该证据始终作为最后一组用于策略解析的证据来使用。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">Name 属性 <paramref name="name" /> 是 null。- 或 - Name 属性 <paramref name="name" /> 开头空白区域，或包含向前或向后的反斜杠。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("Assembly level declarative security is obsolete and is no longer enforced by the CLR by default.  See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, Evidence evidence)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalDefineDynamicAssembly(name, access, (string) null, evidence, (PermissionSet) null, (PermissionSet) null, (PermissionSet) null, ref stackMark, (IEnumerable<CustomAttributeBuilder>) null, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>使用指定名称、访问模式和权限请求定义动态程序集。</summary>
    /// <returns>具有指定名称和功能的动态程序集。</returns>
    /// <param name="name">动态程序集的唯一标识。</param>
    /// <param name="access">访问动态程序集所采用的模式。</param>
    /// <param name="requiredPermissions">必需的权限请求。</param>
    /// <param name="optionalPermissions">可选的权限请求。</param>
    /// <param name="refusedPermissions">被拒绝的权限请求。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">Name 属性 <paramref name="name" /> 是 null。- 或 - Name 属性 <paramref name="name" /> 开头空白区域，或包含向前或向后的反斜杠。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("Assembly level declarative security is obsolete and is no longer enforced by the CLR by default.  See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalDefineDynamicAssembly(name, access, (string) null, (Evidence) null, requiredPermissions, optionalPermissions, refusedPermissions, ref stackMark, (IEnumerable<CustomAttributeBuilder>) null, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>使用指定名称、访问模式、存储目录和证据定义动态程序集。</summary>
    /// <returns>具有指定名称和功能的动态程序集。</returns>
    /// <param name="name">动态程序集的唯一标识。</param>
    /// <param name="access">访问动态程序集所采用的模式。</param>
    /// <param name="dir">保存程序集的目录的名称。如果 <paramref name="dir" /> 是 null，目录将默认为当前目录。</param>
    /// <param name="evidence">为动态程序集提供的证据。该证据始终作为最后一组用于策略解析的证据来使用。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">Name 属性 <paramref name="name" /> 是 null。- 或 - Name 属性 <paramref name="name" /> 开头空白区域，或包含向前或向后的反斜杠。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of DefineDynamicAssembly which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkId=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalDefineDynamicAssembly(name, access, dir, evidence, (PermissionSet) null, (PermissionSet) null, (PermissionSet) null, ref stackMark, (IEnumerable<CustomAttributeBuilder>) null, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>使用指定名称、访问模式、存储目录和权限请求定义动态程序集。</summary>
    /// <returns>具有指定名称和功能的动态程序集。</returns>
    /// <param name="name">动态程序集的唯一标识。</param>
    /// <param name="access">访问动态程序集所采用的模式。</param>
    /// <param name="dir">保存程序集的目录的名称。如果 <paramref name="dir" /> 是 null，目录将默认为当前目录。</param>
    /// <param name="requiredPermissions">必需的权限请求。</param>
    /// <param name="optionalPermissions">可选的权限请求。</param>
    /// <param name="refusedPermissions">被拒绝的权限请求。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">Name 属性 <paramref name="name" /> 是 null。- 或 - Name 属性 <paramref name="name" /> 开头空白区域，或包含向前或向后的反斜杠。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("Assembly level declarative security is obsolete and is no longer enforced by the CLR by default. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalDefineDynamicAssembly(name, access, dir, (Evidence) null, requiredPermissions, optionalPermissions, refusedPermissions, ref stackMark, (IEnumerable<CustomAttributeBuilder>) null, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>使用指定名称、访问模式、证据和权限请求定义动态程序集。</summary>
    /// <returns>具有指定名称和功能的动态程序集。</returns>
    /// <param name="name">动态程序集的唯一标识。</param>
    /// <param name="access">访问动态程序集所采用的模式。</param>
    /// <param name="evidence">为动态程序集提供的证据。该证据始终作为最后一组用于策略解析的证据来使用。</param>
    /// <param name="requiredPermissions">必需的权限请求。</param>
    /// <param name="optionalPermissions">可选的权限请求。</param>
    /// <param name="refusedPermissions">被拒绝的权限请求。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">Name 属性 <paramref name="name" /> 是 null。- 或 - Name 属性 <paramref name="name" /> 开头空白区域，或包含向前或向后的反斜杠。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("Assembly level declarative security is obsolete and is no longer enforced by the CLR by default. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalDefineDynamicAssembly(name, access, (string) null, evidence, requiredPermissions, optionalPermissions, refusedPermissions, ref stackMark, (IEnumerable<CustomAttributeBuilder>) null, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>使用指定名称、访问模式、存储目录、证据和权限请求定义动态程序集。</summary>
    /// <returns>具有指定名称和功能的动态程序集。</returns>
    /// <param name="name">动态程序集的唯一标识。</param>
    /// <param name="access">访问动态程序集所采用的模式。</param>
    /// <param name="dir">保存程序集的目录的名称。如果 <paramref name="dir" /> 是 null，目录将默认为当前目录。</param>
    /// <param name="evidence">为动态程序集提供的证据。该证据始终作为最后一组用于策略解析的证据来使用。</param>
    /// <param name="requiredPermissions">必需的权限请求。</param>
    /// <param name="optionalPermissions">可选的权限请求。</param>
    /// <param name="refusedPermissions">被拒绝的权限请求。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">Name 属性 <paramref name="name" /> 是 null。- 或 - Name 属性 <paramref name="name" /> 开头空白区域，或包含向前或向后的反斜杠。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("Assembly level declarative security is obsolete and is no longer enforced by the CLR by default.  Please see http://go.microsoft.com/fwlink/?LinkId=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalDefineDynamicAssembly(name, access, dir, evidence, requiredPermissions, optionalPermissions, refusedPermissions, ref stackMark, (IEnumerable<CustomAttributeBuilder>) null, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>使用指定名称、访问模式、存储目录、证据、权限请求和同步选项定义动态程序集。</summary>
    /// <returns>具有指定名称和功能的动态程序集。</returns>
    /// <param name="name">动态程序集的唯一标识。</param>
    /// <param name="access">访问动态程序集所采用的模式。</param>
    /// <param name="dir">保存动态程序集的目录的名称。如果 <paramref name="dir" /> 是 null，目录将默认为当前目录。</param>
    /// <param name="evidence">为动态程序集提供的证据。该证据始终作为最后一组用于策略解析的证据来使用。</param>
    /// <param name="requiredPermissions">必需的权限请求。</param>
    /// <param name="optionalPermissions">可选的权限请求。</param>
    /// <param name="refusedPermissions">被拒绝的权限请求。</param>
    /// <param name="isSynchronized">若要在动态程序集中同步模块、类型和成员的创建，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">Name 属性 <paramref name="name" /> 是 null。- 或 - Name 属性 <paramref name="name" /> 开头空白区域，或包含向前或向后的反斜杠。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("Assembly level declarative security is obsolete and is no longer enforced by the CLR by default. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, bool isSynchronized)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalDefineDynamicAssembly(name, access, dir, evidence, requiredPermissions, optionalPermissions, refusedPermissions, ref stackMark, (IEnumerable<CustomAttributeBuilder>) null, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>使用指定的名称、访问模式、存储目录、证据、权限请求、同步选项和自定义特性定义动态程序集。</summary>
    /// <returns>具有指定名称和功能的动态程序集。</returns>
    /// <param name="name">动态程序集的唯一标识。</param>
    /// <param name="access">访问动态程序集所采用的模式。</param>
    /// <param name="dir">保存动态程序集的目录的名称。如果 <paramref name="dir" /> 为 null，则使用当前目录。</param>
    /// <param name="evidence">为动态程序集提供的证据。该证据始终作为最后一组用于策略解析的证据来使用。</param>
    /// <param name="requiredPermissions">必需的权限请求。</param>
    /// <param name="optionalPermissions">可选的权限请求。</param>
    /// <param name="refusedPermissions">被拒绝的权限请求。</param>
    /// <param name="isSynchronized">若要在动态程序集中同步模块、类型和成员的创建，则为 true；否则为 false。</param>
    /// <param name="assemblyAttributes">要应用于程序集的可枚举特性列表；如果无特性，则为 null。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">Name 属性 <paramref name="name" /> 是 null。- 或 - Name 属性 <paramref name="name" /> 开头为空白区域，或包含向前或向后的反斜杠。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    [SecuritySafeCritical]
    [Obsolete("Assembly level declarative security is obsolete and is no longer enforced by the CLR by default. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, bool isSynchronized, IEnumerable<CustomAttributeBuilder> assemblyAttributes)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalDefineDynamicAssembly(name, access, dir, evidence, requiredPermissions, optionalPermissions, refusedPermissions, ref stackMark, assemblyAttributes, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>使用指定名称、访问模式、存储目录和同步选项定义动态程序集。</summary>
    /// <returns>具有指定名称和功能的动态程序集。</returns>
    /// <param name="name">动态程序集的唯一标识。</param>
    /// <param name="access">访问动态程序集所采用的模式。</param>
    /// <param name="dir">保存动态程序集的目录的名称。如果 <paramref name="dir" /> 为 null，则使用当前目录。</param>
    /// <param name="isSynchronized">若要在动态程序集中同步模块、类型和成员的创建，则为 true；否则为 false。</param>
    /// <param name="assemblyAttributes">要应用于程序集的可枚举特性列表；如果无特性，则为 null。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">Name 属性 <paramref name="name" /> 是 null。- 或 - Name 属性 <paramref name="name" /> 开头为空白区域，或包含向前或向后的反斜杠。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, bool isSynchronized, IEnumerable<CustomAttributeBuilder> assemblyAttributes)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalDefineDynamicAssembly(name, access, dir, (Evidence) null, (PermissionSet) null, (PermissionSet) null, (PermissionSet) null, ref stackMark, assemblyAttributes, SecurityContextSource.CurrentAssembly);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private AssemblyBuilder InternalDefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, ref StackCrawlMark stackMark, IEnumerable<CustomAttributeBuilder> assemblyAttributes, SecurityContextSource securityContextSource)
    {
      return AssemblyBuilder.InternalDefineDynamicAssembly(name, access, dir, evidence, requiredPermissions, optionalPermissions, refusedPermissions, ref stackMark, assemblyAttributes, securityContextSource);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private string nApplyPolicy(AssemblyName an);

    /// <summary>返回应用策略后的程序集显示名称。</summary>
    /// <returns>包含应用策略后的程序集显示名称的字符串。</returns>
    /// <param name="assemblyName">程序集显示名称，采用 <see cref="P:System.Reflection.Assembly.FullName" /> 属性提供的格式。</param>
    /// <filterpriority>1</filterpriority>
    [ComVisible(false)]
    public string ApplyPolicy(string assemblyName)
    {
      AssemblyName an = new AssemblyName(assemblyName);
      byte[] numArray = an.GetPublicKeyToken() ?? an.GetPublicKey();
      if (numArray == null || numArray.Length == 0)
        return assemblyName;
      return this.nApplyPolicy(an);
    }

    /// <summary>创建在指定程序集中定义的指定类型的新实例。</summary>
    /// <returns>一个对象，该对象是 <paramref name="typeName" /> 指定的新实例的包装。返回值需要打开包装才能访问真实对象。</returns>
    /// <param name="assemblyName">程序集的显示名称。请参阅<see cref="P:System.Reflection.Assembly.FullName" />。</param>
    /// <param name="typeName">
    /// <see cref="P:System.Type.FullName" /> 属性返回的所请求类型的完全限定名称，包含命名空间而不是程序集。</param>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyName" /> 或 <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyName" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyName" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyName" /> 未找到。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的公共构造函数。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typename" /> 中未找到 <paramref name="assemblyName" />。</exception>
    /// <exception cref="T:System.NullReferenceException">此实例是 null。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public ObjectHandle CreateInstance(string assemblyName, string typeName)
    {
      if (this == null)
        throw new NullReferenceException();
      if (assemblyName == null)
        throw new ArgumentNullException("assemblyName");
      return Activator.CreateInstance(assemblyName, typeName);
    }

    [SecurityCritical]
    internal ObjectHandle InternalCreateInstanceWithNoSecurity(string assemblyName, string typeName)
    {
      PermissionSet.s_fullTrust.Assert();
      return this.CreateInstance(assemblyName, typeName);
    }

    /// <summary>创建在指定程序集文件中定义的指定类型的新实例。</summary>
    /// <returns>一个对象，它是新实例的包装，或者如果找不到 null，则为 <paramref name="typeName" />。返回值需要打开包装才能访问真实对象。</returns>
    /// <param name="assemblyFile">文件的名称（包括路径），该文件包含定义所请求类型的程序集。该程序集是使用 <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" /> 方法加载的。</param>
    /// <param name="typeName">
    /// <see cref="P:System.Type.FullName" /> 属性返回的所请求类型的完全限定名称，包含命名空间而不是程序集。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyFile" /> 为 null。- 或 - <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyFile" /> 未找到。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typeName" /> 中未找到 <paramref name="assemblyFile" />。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.MissingMethodException">发现没有无参数公共构造函数。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有足够的权限来调用此构造函数。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyFile" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <exception cref="T:System.NullReferenceException">此实例是 null。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName)
    {
      if (this == null)
        throw new NullReferenceException();
      return Activator.CreateInstanceFrom(assemblyFile, typeName);
    }

    [SecurityCritical]
    internal ObjectHandle InternalCreateInstanceFromWithNoSecurity(string assemblyName, string typeName)
    {
      PermissionSet.s_fullTrust.Assert();
      return this.CreateInstanceFrom(assemblyName, typeName);
    }

    /// <summary>创建指定 COM 类型的新实例。形参指定文件的名称，该文件包含含有类型和类型名称的程序集。</summary>
    /// <returns>一个对象，该对象是 <paramref name="typeName" /> 指定的新实例的包装。返回值需要打开包装才能访问真实对象。</returns>
    /// <param name="assemblyName">文件的名称，该文件包含定义所请求的类型的程序集。</param>
    /// <param name="typeName">所请求类型的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyName" /> 或 <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.TypeLoadException">不能加载的类型。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.MissingMethodException">发现没有公共无参数构造函数。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyName" /> 未找到。</exception>
    /// <exception cref="T:System.MemberAccessException">
    /// <paramref name="typeName" /> 是一个抽象类。- 或 -使用后期绑定机制调用了该成员。</exception>
    /// <exception cref="T:System.NotSupportedException">调用方不能提供一个对象，不会继承从激活特性 <see cref="T:System.MarshalByRefObject" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="assemblyName" /> 为空字符串 ("")。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyName" /> 不是有效的程序集。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <exception cref="T:System.NullReferenceException">所引用的 COM 对象是 null。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public ObjectHandle CreateComInstanceFrom(string assemblyName, string typeName)
    {
      if (this == null)
        throw new NullReferenceException();
      return Activator.CreateComInstanceFrom(assemblyName, typeName);
    }

    /// <summary>创建指定 COM 类型的新实例。形参指定文件的名称，该文件包含含有类型和类型名称的程序集。</summary>
    /// <returns>一个对象，该对象是 <paramref name="typeName" /> 指定的新实例的包装。返回值需要打开包装才能访问真实对象。</returns>
    /// <param name="assemblyFile">文件的名称，该文件包含定义所请求的类型的程序集。</param>
    /// <param name="typeName">所请求类型的名称。</param>
    /// <param name="hashValue">表示计算所得的哈希代码的值。</param>
    /// <param name="hashAlgorithm">表示程序集清单使用的哈希算法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyName" /> 或 <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.TypeLoadException">不能加载的类型。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.MissingMethodException">发现没有公共无参数构造函数。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyFile" /> 未找到。</exception>
    /// <exception cref="T:System.MemberAccessException">
    /// <paramref name="typeName" /> 是一个抽象类。- 或 -使用后期绑定机制调用了该成员。</exception>
    /// <exception cref="T:System.NotSupportedException">调用方不能提供一个对象，不会继承从激活特性 <see cref="T:System.MarshalByRefObject" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="assemblyFile" /> 为空字符串 ("")。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效的程序集。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <exception cref="T:System.NullReferenceException">所引用的 COM 对象是 null。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public ObjectHandle CreateComInstanceFrom(string assemblyFile, string typeName, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
    {
      if (this == null)
        throw new NullReferenceException();
      return Activator.CreateComInstanceFrom(assemblyFile, typeName, hashValue, hashAlgorithm);
    }

    /// <summary>创建在指定程序集中定义的指定类型的新实例。形参指定激活特性数组。</summary>
    /// <returns>一个对象，该对象是 <paramref name="typeName" /> 指定的新实例的包装。返回值需要打开包装才能访问真实对象。</returns>
    /// <param name="assemblyName">程序集的显示名称。请参阅<see cref="P:System.Reflection.Assembly.FullName" />。</param>
    /// <param name="typeName">
    /// <see cref="P:System.Type.FullName" /> 属性返回的所请求类型的完全限定名称，包含命名空间而不是程序集。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。通常，为包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组，该对象指定激活远程对象所需的 URL。此参数与客户端激活的对象相关。客户端激活是一项传统技术，保留用于向后兼容，但不建议用于新的开发。应改用 Windows Communication Foundation 来开发分布式应用程序。</param>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyName" /> 或 <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyName" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyName" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyName" /> 未找到。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的公共构造函数。</exception>
    /// <exception cref="T:System.NotSupportedException">调用方不能提供一个对象，不会继承从激活特性 <see cref="T:System.MarshalByRefObject" />。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typename" /> 中未找到 <paramref name="assemblyName" />。</exception>
    /// <exception cref="T:System.NullReferenceException">此实例是 null。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public ObjectHandle CreateInstance(string assemblyName, string typeName, object[] activationAttributes)
    {
      if (this == null)
        throw new NullReferenceException();
      if (assemblyName == null)
        throw new ArgumentNullException("assemblyName");
      return Activator.CreateInstance(assemblyName, typeName, activationAttributes);
    }

    /// <summary>创建在指定程序集文件中定义的指定类型的新实例。</summary>
    /// <returns>一个对象，它是新实例的包装，或者如果找不到 null，则为 <paramref name="typeName" />。返回值需要打开包装才能访问真实对象。</returns>
    /// <param name="assemblyFile">文件的名称（包括路径），该文件包含定义所请求类型的程序集。该程序集是使用 <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" /> 方法加载的。</param>
    /// <param name="typeName">
    /// <see cref="P:System.Type.FullName" /> 属性返回的所请求类型的完全限定名称，包含命名空间而不是程序集。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。通常，为包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组，该对象指定激活远程对象所需的 URL。此参数与客户端激活的对象相关。客户端激活是一项传统技术，保留用于向后兼容，但不建议用于新的开发。应改用 Windows Communication Foundation 来开发分布式应用程序。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyFile" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyFile" /> 未找到。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typeName" /> 中未找到 <paramref name="assemblyFile" />。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有足够的权限来调用此构造函数。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的公共构造函数。</exception>
    /// <exception cref="T:System.NotSupportedException">调用方不能提供一个对象，不会继承从激活特性 <see cref="T:System.MarshalByRefObject" />。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyFile" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <exception cref="T:System.NullReferenceException">此实例是 null。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, object[] activationAttributes)
    {
      if (this == null)
        throw new NullReferenceException();
      return Activator.CreateInstanceFrom(assemblyFile, typeName, activationAttributes);
    }

    /// <summary>创建在指定程序集中定义的指定类型的新实例。形参指定联编程序、绑定标志、构造函数实参、特定于区域性的信息，这些信息用于解释实参、激活特性和授权，以创建类型。</summary>
    /// <returns>一个对象，该对象是 <paramref name="typeName" /> 指定的新实例的包装。返回值需要打开包装才能访问真实对象。</returns>
    /// <param name="assemblyName">程序集的显示名称。请参阅<see cref="P:System.Reflection.Assembly.FullName" />。</param>
    /// <param name="typeName">
    /// <see cref="P:System.Type.FullName" /> 属性返回的所请求类型的完全限定名称，包含命名空间而不是程序集。</param>
    /// <param name="ignoreCase">一个布尔值，指示是否执行区分大小写的搜索。</param>
    /// <param name="bindingAttr">影响 <paramref name="typeName" /> 构造函数搜索的零个或多个位标志的组合。如果 <paramref name="bindingAttr" /> 为零，则对公共构造函数进行区分大小写的搜索。</param>
    /// <param name="binder">一个对象，它使用反射启用绑定、参数类型的强制、成员的调用和 <see cref="T:System.Reflection.MemberInfo" /> 对象的检索。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="args">要传递给构造函数的实参。此实参数组必须在数量、顺序和类型方面与要调用的构造函数的形参匹配。如果默认的构造函数是首选构造函数，则 <paramref name="args" /> 必须为空数组或 Null。</param>
    /// <param name="culture">区域性特定的信息，这些信息控制将 <paramref name="args" /> 强制转换为 <paramref name="typeName" /> 构造函数所声明的正式类型。如果 <paramref name="culture" /> 为 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。通常，为包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组，该对象指定激活远程对象所需的 URL。此参数与客户端激活的对象相关。客户端激活是一项传统技术，保留用于向后兼容，但不建议用于新的开发。应改用 Windows Communication Foundation 来开发分布式应用程序。</param>
    /// <param name="securityAttributes">用于授权创建 <paramref name="typeName" /> 的信息。</param>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyName" /> 或 <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyName" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyName" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyName" /> 未找到。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的构造函数。</exception>
    /// <exception cref="T:System.NotSupportedException">调用方不能提供一个对象，不会继承从激活特性 <see cref="T:System.MarshalByRefObject" />。- 或 -<paramref name="securityAttributes" /> 不是 null。当未启用旧版 CAS 策略时， <paramref name="securityAttributes" /> 应为 null.</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typename" /> 中未找到 <paramref name="assemblyName" />。</exception>
    /// <exception cref="T:System.NullReferenceException">此实例是 null。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstance which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
    {
      if (this == null)
        throw new NullReferenceException();
      if (assemblyName == null)
        throw new ArgumentNullException("assemblyName");
      if (securityAttributes != null && !this.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      return Activator.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
    }

    /// <summary>创建在指定程序集中定义的指定类型的新实例。形参指定联编程序、绑定标志、构造函数实参、用于解释实参的特定于区域性的信息，以及可选激活特性。</summary>
    /// <returns>一个对象，该对象是 <paramref name="typeName" /> 指定的新实例的包装。返回值需要打开包装才能访问真实对象。</returns>
    /// <param name="assemblyName">程序集的显示名称。请参阅<see cref="P:System.Reflection.Assembly.FullName" />。</param>
    /// <param name="typeName">
    /// <see cref="P:System.Type.FullName" /> 属性返回的所请求类型的完全限定名称，包含命名空间而不是程序集。</param>
    /// <param name="ignoreCase">一个布尔值，指示是否执行区分大小写的搜索。</param>
    /// <param name="bindingAttr">影响 <paramref name="typeName" /> 构造函数搜索的零个或多个位标志的组合。如果 <paramref name="bindingAttr" /> 为零，则对公共构造函数进行区分大小写的搜索。</param>
    /// <param name="binder">一个对象，它使用反射启用绑定、参数类型的强制、成员的调用和 <see cref="T:System.Reflection.MemberInfo" /> 对象的检索。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="args">要传递给构造函数的实参。此实参数组必须在数量、顺序和类型方面与要调用的构造函数的形参匹配。如果默认的构造函数是首选构造函数，则 <paramref name="args" /> 必须为空数组或 Null。</param>
    /// <param name="culture">区域性特定的信息，这些信息控制将 <paramref name="args" /> 强制转换为 <paramref name="typeName" /> 构造函数所声明的正式类型。如果 <paramref name="culture" /> 为 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。通常，为包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组，该对象指定激活远程对象所需的 URL。此参数与客户端激活的对象相关。客户端激活是一项传统技术，保留用于向后兼容，但不建议用于新的开发。应改用 Windows Communication Foundation 来开发分布式应用程序。</param>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyName" /> 或 <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyName" /> 不是有效的程序集。- 或 -<paramref name="assemblyName" /> 已使用比当前加载的版本更高版本的公共语言运行时编译。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyName" /> 未找到。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的构造函数。</exception>
    /// <exception cref="T:System.NotSupportedException">调用方不能提供一个对象，不会继承从激活特性 <see cref="T:System.MarshalByRefObject" />。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typename" /> 中未找到 <paramref name="assemblyName" />。</exception>
    /// <exception cref="T:System.NullReferenceException">此实例是 null。</exception>
    public ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
    {
      if (this == null)
        throw new NullReferenceException();
      if (assemblyName == null)
        throw new ArgumentNullException("assemblyName");
      return Activator.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes);
    }

    [SecurityCritical]
    internal ObjectHandle InternalCreateInstanceWithNoSecurity(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
    {
      PermissionSet.s_fullTrust.Assert();
      return this.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
    }

    /// <summary>创建在指定程序集文件中定义的指定类型的新实例。</summary>
    /// <returns>一个对象，它是新实例的包装，或者如果找不到 null，则为 <paramref name="typeName" />。返回值需要打开包装才能访问真实对象。</returns>
    /// <param name="assemblyFile">文件的名称（包括路径），该文件包含定义所请求类型的程序集。该程序集是使用 <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" /> 方法加载的。</param>
    /// <param name="typeName">
    /// <see cref="P:System.Type.FullName" /> 属性返回的所请求类型的完全限定名称，包含命名空间而不是程序集。</param>
    /// <param name="ignoreCase">一个布尔值，指示是否执行区分大小写的搜索。</param>
    /// <param name="bindingAttr">影响 <paramref name="typeName" /> 构造函数搜索的零个或多个位标志的组合。如果 <paramref name="bindingAttr" /> 为零，则对公共构造函数进行区分大小写的搜索。</param>
    /// <param name="binder">一个对象，它启用绑定、对参数类型的强制、对成员的调用，以及通过反射对 <see cref="T:System.Reflection.MemberInfo" /> 对象的检索。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="args">要传递给构造函数的实参。此实参数组必须在数量、顺序和类型方面与要调用的构造函数的形参匹配。如果默认的构造函数是首选构造函数，则 <paramref name="args" /> 必须为空数组或 Null。</param>
    /// <param name="culture">区域性特定的信息，这些信息控制将 <paramref name="args" /> 强制转换为 <paramref name="typeName" /> 构造函数所声明的正式类型。如果 <paramref name="culture" /> 为 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。通常，为包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组，该对象指定激活远程对象所需的 URL。此参数与客户端激活的对象相关。客户端激活是一项传统技术，保留用于向后兼容，但不建议用于新的开发。应改用 Windows Communication Foundation 来开发分布式应用程序。</param>
    /// <param name="securityAttributes">用于授权创建 <paramref name="typeName" /> 的信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyFile" /> 为 null。- 或 - <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">调用方不能提供一个对象，不会继承从激活特性 <see cref="T:System.MarshalByRefObject" />。- 或 -<paramref name="securityAttributes" /> 不是 null。当未启用旧版 CAS 策略时， <paramref name="securityAttributes" /> 应 null。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyFile" /> 未找到。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typeName" /> 中未找到 <paramref name="assemblyFile" />。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的公共构造函数。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有足够的权限来调用此构造函数。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyFile" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <exception cref="T:System.NullReferenceException">此实例是 null。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstanceFrom which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
    {
      if (this == null)
        throw new NullReferenceException();
      if (securityAttributes != null && !this.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      return Activator.CreateInstanceFrom(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
    }

    /// <summary>创建在指定程序集文件中定义的指定类型的新实例。</summary>
    /// <returns>一个对象，它是新实例的包装，或者如果找不到 null，则为 <paramref name="typeName" />。返回值需要打开包装才能访问真实对象。</returns>
    /// <param name="assemblyFile">文件的名称（包括路径），该文件包含定义所请求类型的程序集。该程序集是使用 <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" /> 方法加载的。</param>
    /// <param name="typeName">
    /// <see cref="P:System.Type.FullName" /> 属性返回的所请求类型的完全限定名称，包含命名空间而不是程序集。</param>
    /// <param name="ignoreCase">一个布尔值，指示是否执行区分大小写的搜索。</param>
    /// <param name="bindingAttr">影响 <paramref name="typeName" /> 构造函数搜索的零个或多个位标志的组合。如果 <paramref name="bindingAttr" /> 为零，则对公共构造函数进行区分大小写的搜索。</param>
    /// <param name="binder">一个对象，它启用绑定、对参数类型的强制、对成员的调用，以及通过反射对 <see cref="T:System.Reflection.MemberInfo" /> 对象的检索。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="args">要传递给构造函数的实参。此实参数组必须在数量、顺序和类型方面与要调用的构造函数的形参匹配。如果默认的构造函数是首选构造函数，则 <paramref name="args" /> 必须为空数组或 Null。</param>
    /// <param name="culture">区域性特定的信息，这些信息控制将 <paramref name="args" /> 强制转换为 <paramref name="typeName" /> 构造函数所声明的正式类型。如果 <paramref name="culture" /> 为 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。通常，为包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组，该对象指定激活远程对象所需的 URL。此参数与客户端激活的对象相关。客户端激活是一项传统技术，保留用于向后兼容，但不建议用于新的开发。应改用 Windows Communication Foundation 来开发分布式应用程序。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyFile" /> 为 null。- 或 - <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">调用方不能提供一个对象，不会继承从激活特性 <see cref="T:System.MarshalByRefObject" />。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyFile" /> 未找到。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typeName" /> 中未找到 <paramref name="assemblyFile" />。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的公共构造函数。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有足够的权限来调用此构造函数。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效的程序集。- 或 -<paramref name="assemblyFile" /> 已使用比当前加载的版本更高版本的公共语言运行时编译。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <exception cref="T:System.NullReferenceException">此实例是 null。</exception>
    public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
    {
      if (this == null)
        throw new NullReferenceException();
      return Activator.CreateInstanceFrom(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes);
    }

    [SecurityCritical]
    internal ObjectHandle InternalCreateInstanceFromWithNoSecurity(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
    {
      PermissionSet.s_fullTrust.Assert();
      return this.CreateInstanceFrom(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
    }

    /// <summary>在给定 <see cref="T:System.Reflection.AssemblyName" /> 的情况下加载 <see cref="T:System.Reflection.Assembly" />。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="assemblyRef">描述要加载的程序集的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyRef" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyRef" /> 未找到。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyRef" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyRef" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Assembly Load(AssemblyName assemblyRef)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoadAssemblyName(assemblyRef, (Evidence) null, (RuntimeAssembly) null, ref stackMark, true, false, false);
    }

    /// <summary>在给定其显示名称的情况下加载 <see cref="T:System.Reflection.Assembly" />。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="assemblyString">程序集的显示名称。请参阅<see cref="P:System.Reflection.Assembly.FullName" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyString" /> 为 null</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyString" /> 未找到。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyString" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyString" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Assembly Load(string assemblyString)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoad(assemblyString, (Evidence) null, ref stackMark, false);
    }

    /// <summary>加载带有基于通用对象文件格式 (COFF) 的图像的 <see cref="T:System.Reflection.Assembly" />，该图像包含已发出的 <see cref="T:System.Reflection.Assembly" />。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="rawAssembly">byte 类型的数组，它是包含已发出程序集的基于 COFF 的图像。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rawAssembly" /> 为 null。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="rawAssembly" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="rawAssembly" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Assembly Load(byte[] rawAssembly)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.nLoadImage(rawAssembly, (byte[]) null, (Evidence) null, ref stackMark, false, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>加载带有基于通用对象文件格式 (COFF) 的图像的 <see cref="T:System.Reflection.Assembly" />，该图像包含已发出的 <see cref="T:System.Reflection.Assembly" />。还加载表示 <see cref="T:System.Reflection.Assembly" /> 的符号的原始字节。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="rawAssembly">byte 类型的数组，它是包含已发出程序集的基于 COFF 的图像。</param>
    /// <param name="rawSymbolStore">byte 类型的数组，它包含表示程序集符号的原始字节。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rawAssembly" /> 为 null。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="rawAssembly" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="rawAssembly" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.nLoadImage(rawAssembly, rawSymbolStore, (Evidence) null, ref stackMark, false, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>加载带有基于通用对象文件格式 (COFF) 的图像的 <see cref="T:System.Reflection.Assembly" />，该图像包含已发出的 <see cref="T:System.Reflection.Assembly" />。还加载表示 <see cref="T:System.Reflection.Assembly" /> 的符号的原始字节。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="rawAssembly">byte 类型的数组，它是包含已发出程序集的基于 COFF 的图像。</param>
    /// <param name="rawSymbolStore">byte 类型的数组，它包含表示程序集符号的原始字节。</param>
    /// <param name="securityEvidence">用于加载程序集的证据。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rawAssembly" /> 为 null。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="rawAssembly" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="rawAssembly" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="securityEvidence" /> 不是 null。当未启用旧版 CAS 策略时， <paramref name="securityEvidence" /> 应 null。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of Load which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkId=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    [SecurityPermission(SecurityAction.Demand, ControlEvidence = true)]
    public Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence)
    {
      if (securityEvidence != null && !this.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.nLoadImage(rawAssembly, rawSymbolStore, securityEvidence, ref stackMark, false, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>在给定 <see cref="T:System.Reflection.AssemblyName" /> 的情况下加载 <see cref="T:System.Reflection.Assembly" />。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="assemblyRef">描述要加载的程序集的对象。</param>
    /// <param name="assemblySecurity">用于加载程序集的证据。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyRef" /> 为 null</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyRef" /> 未找到。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyRef" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyRef" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of Load which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Assembly Load(AssemblyName assemblyRef, Evidence assemblySecurity)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoadAssemblyName(assemblyRef, assemblySecurity, (RuntimeAssembly) null, ref stackMark, true, false, false);
    }

    /// <summary>在给定其显示名称的情况下加载 <see cref="T:System.Reflection.Assembly" />。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="assemblyString">程序集的显示名称。请参阅<see cref="P:System.Reflection.Assembly.FullName" />。</param>
    /// <param name="assemblySecurity">用于加载程序集的证据。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyString" /> 为 null</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyString" /> 未找到。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyString" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyString" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of Load which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Assembly Load(string assemblyString, Evidence assemblySecurity)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoad(assemblyString, assemblySecurity, ref stackMark, false);
    }

    /// <summary>执行指定文件中包含的程序集。</summary>
    /// <returns>程序集的入口点返回的值。</returns>
    /// <param name="assemblyFile">包含要执行程序集的文件的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyFile" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyFile" /> 未找到。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyFile" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <exception cref="T:System.MissingMethodException">指定的程序集具有不到入口点。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    public int ExecuteAssembly(string assemblyFile)
    {
      return this.ExecuteAssembly(assemblyFile, (string[]) null);
    }

    /// <summary>使用指定的证据执行指定文件中包含的程序集。</summary>
    /// <returns>程序集的入口点返回的值。</returns>
    /// <param name="assemblyFile">包含要执行程序集的文件的名称。</param>
    /// <param name="assemblySecurity">用于加载程序集的证据。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyFile" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyFile" /> 未找到。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyFile" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <exception cref="T:System.MissingMethodException">指定的程序集具有不到入口点。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of ExecuteAssembly which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity)
    {
      return this.ExecuteAssembly(assemblyFile, assemblySecurity, (string[]) null);
    }

    /// <summary>使用指定的证据和实参执行指定文件中包含的程序集。</summary>
    /// <returns>程序集的入口点返回的值。</returns>
    /// <param name="assemblyFile">包含要执行程序集的文件的名称。</param>
    /// <param name="assemblySecurity">为程序集提供的证据。</param>
    /// <param name="args">程序集的入口点的实参。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyFile" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyFile" /> 未找到。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyFile" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="assemblySecurity" /> 不是 null。当未启用旧版 CAS 策略时， <paramref name="assemblySecurity" /> 应 null。</exception>
    /// <exception cref="T:System.MissingMethodException">指定的程序集具有不到入口点。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of ExecuteAssembly which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity, string[] args)
    {
      if (assemblySecurity != null && !this.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      RuntimeAssembly assembly = (RuntimeAssembly) Assembly.LoadFrom(assemblyFile, assemblySecurity);
      if (args == null)
        args = new string[0];
      return this.nExecuteAssembly(assembly, args);
    }

    /// <summary>使用指定的参数执行指定文件中包含的程序集。</summary>
    /// <returns>程序集的入口点返回的值。</returns>
    /// <param name="assemblyFile">包含要执行程序集的文件的名称。</param>
    /// <param name="args">程序集的入口点的实参。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyFile" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyFile" /> 未找到。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效的程序集。- 或 -<paramref name="assemblyFile" /> 已使用比当前加载的版本更高版本的公共语言运行时编译。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <exception cref="T:System.MissingMethodException">指定的程序集具有不到入口点。</exception>
    public int ExecuteAssembly(string assemblyFile, string[] args)
    {
      RuntimeAssembly assembly = (RuntimeAssembly) Assembly.LoadFrom(assemblyFile);
      if (args == null)
        args = new string[0];
      return this.nExecuteAssembly(assembly, args);
    }

    /// <summary>使用指定的证据、参数、哈希值和哈希算法执行指定文件中包含的程序集。</summary>
    /// <returns>程序集的入口点返回的值。</returns>
    /// <param name="assemblyFile">包含要执行程序集的文件的名称。</param>
    /// <param name="assemblySecurity">为程序集提供的证据。</param>
    /// <param name="args">程序集的入口点的实参。</param>
    /// <param name="hashValue">表示计算所得的哈希代码的值。</param>
    /// <param name="hashAlgorithm">表示程序集清单使用的哈希算法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyFile" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyFile" /> 未找到。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyFile" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="assemblySecurity" /> 不是 null。当未启用旧版 CAS 策略时， <paramref name="assemblySecurity" /> 应 null。</exception>
    /// <exception cref="T:System.MissingMethodException">指定的程序集具有不到入口点。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of ExecuteAssembly which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity, string[] args, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
    {
      if (assemblySecurity != null && !this.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      RuntimeAssembly assembly = (RuntimeAssembly) Assembly.LoadFrom(assemblyFile, assemblySecurity, hashValue, hashAlgorithm);
      if (args == null)
        args = new string[0];
      return this.nExecuteAssembly(assembly, args);
    }

    /// <summary>使用指定的参数、哈希值和哈希算法执行指定文件中包含的程序集。</summary>
    /// <returns>程序集的入口点返回的值。</returns>
    /// <param name="assemblyFile">包含要执行程序集的文件的名称。</param>
    /// <param name="args">程序集的入口点的实参。</param>
    /// <param name="hashValue">表示计算所得的哈希代码的值。</param>
    /// <param name="hashAlgorithm">表示程序集清单使用的哈希算法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyFile" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyFile" /> 未找到。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效的程序集。- 或 -<paramref name="assemblyFile" /> 已使用比当前加载的版本更高版本的公共语言运行时编译。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <exception cref="T:System.MissingMethodException">指定的程序集具有不到入口点。</exception>
    public int ExecuteAssembly(string assemblyFile, string[] args, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
    {
      RuntimeAssembly assembly = (RuntimeAssembly) Assembly.LoadFrom(assemblyFile, hashValue, hashAlgorithm);
      if (args == null)
        args = new string[0];
      return this.nExecuteAssembly(assembly, args);
    }

    /// <summary>在给定其显示名称的情况下执行程序集。</summary>
    /// <returns>程序集的入口点返回的值。</returns>
    /// <param name="assemblyName">程序集的显示名称。请参阅<see cref="P:System.Reflection.Assembly.FullName" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyName" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">通过指定的程序集 <paramref name="assemblyName" /> 找不到。</exception>
    /// <exception cref="T:System.BadImageFormatException">通过指定的程序集 <paramref name="assemblyName" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyName" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.IO.FileLoadException">通过指定的程序集 <paramref name="assemblyName" /> 找到了，但无法加载。</exception>
    /// <exception cref="T:System.MissingMethodException">指定的程序集具有不到入口点。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    public int ExecuteAssemblyByName(string assemblyName)
    {
      return this.ExecuteAssemblyByName(assemblyName, (string[]) null);
    }

    /// <summary>在给定显示名称的情况下，使用指定证据执行程序集。</summary>
    /// <returns>程序集的入口点返回的值。</returns>
    /// <param name="assemblyName">程序集的显示名称。请参阅<see cref="P:System.Reflection.Assembly.FullName" />。</param>
    /// <param name="assemblySecurity">用于加载程序集的证据。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyName" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">通过指定的程序集 <paramref name="assemblyName" /> 找不到。</exception>
    /// <exception cref="T:System.IO.FileLoadException">通过指定的程序集 <paramref name="assemblyName" /> 找到了，但无法加载。</exception>
    /// <exception cref="T:System.BadImageFormatException">通过指定的程序集 <paramref name="assemblyName" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyName" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.MissingMethodException">指定的程序集具有不到入口点。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of ExecuteAssemblyByName which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public int ExecuteAssemblyByName(string assemblyName, Evidence assemblySecurity)
    {
      return this.ExecuteAssemblyByName(assemblyName, assemblySecurity, (string[]) null);
    }

    /// <summary>在给定其显示名称的情况下，使用指定证据和实参执行程序集。</summary>
    /// <returns>程序集的入口点返回的值。</returns>
    /// <param name="assemblyName">程序集的显示名称。请参阅<see cref="P:System.Reflection.Assembly.FullName" />。</param>
    /// <param name="assemblySecurity">用于加载程序集的证据。</param>
    /// <param name="args">启动该进程时传递的命令行参数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyName" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">通过指定的程序集 <paramref name="assemblyName" /> 找不到。</exception>
    /// <exception cref="T:System.IO.FileLoadException">通过指定的程序集 <paramref name="assemblyName" /> 找到了，但无法加载。</exception>
    /// <exception cref="T:System.BadImageFormatException">通过指定的程序集 <paramref name="assemblyName" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyName" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="assemblySecurity" /> 不是 null。当未启用旧版 CAS 策略时， <paramref name="assemblySecurity" /> 应 null。</exception>
    /// <exception cref="T:System.MissingMethodException">指定的程序集具有不到入口点。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of ExecuteAssemblyByName which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public int ExecuteAssemblyByName(string assemblyName, Evidence assemblySecurity, params string[] args)
    {
      if (assemblySecurity != null && !this.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      RuntimeAssembly assembly = (RuntimeAssembly) Assembly.Load(assemblyName, assemblySecurity);
      if (args == null)
        args = new string[0];
      return this.nExecuteAssembly(assembly, args);
    }

    /// <summary>在给定显示名称的情况下，使用指定参数执行程序集。</summary>
    /// <returns>程序集的入口点返回的值。</returns>
    /// <param name="assemblyName">程序集的显示名称。请参阅<see cref="P:System.Reflection.Assembly.FullName" />。</param>
    /// <param name="args">启动该进程时传递的命令行参数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyName" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">通过指定的程序集 <paramref name="assemblyName" /> 找不到。</exception>
    /// <exception cref="T:System.IO.FileLoadException">通过指定的程序集 <paramref name="assemblyName" /> 找到了，但无法加载。</exception>
    /// <exception cref="T:System.BadImageFormatException">通过指定的程序集 <paramref name="assemblyName" /> 不是有效的程序集。- 或 -<paramref name="assemblyName" /> 已使用比当前加载的版本更高版本的公共语言运行时编译。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.MissingMethodException">指定的程序集具有不到入口点。</exception>
    public int ExecuteAssemblyByName(string assemblyName, params string[] args)
    {
      RuntimeAssembly assembly = (RuntimeAssembly) Assembly.Load(assemblyName);
      if (args == null)
        args = new string[0];
      return this.nExecuteAssembly(assembly, args);
    }

    /// <summary>根据给定的 <see cref="T:System.Reflection.AssemblyName" /> 使用指定的证据和实参执行程序集。</summary>
    /// <returns>程序集的入口点返回的值。</returns>
    /// <param name="assemblyName">
    /// <see cref="T:System.Reflection.AssemblyName" /> 对象，表示程序集名称。</param>
    /// <param name="assemblySecurity">用于加载程序集的证据。</param>
    /// <param name="args">启动该进程时传递的命令行参数。</param>
    /// <exception cref="T:System.IO.FileNotFoundException">通过指定的程序集 <paramref name="assemblyName" /> 找不到。</exception>
    /// <exception cref="T:System.IO.FileLoadException">通过指定的程序集 <paramref name="assemblyName" /> 找到了，但无法加载。</exception>
    /// <exception cref="T:System.BadImageFormatException">通过指定的程序集 <paramref name="assemblyName" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyName" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="assemblySecurity" /> 不是 null。当未启用旧版 CAS 策略时， <paramref name="assemblySecurity" /> 应 null。</exception>
    /// <exception cref="T:System.MissingMethodException">指定的程序集具有不到入口点。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of ExecuteAssemblyByName which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public int ExecuteAssemblyByName(AssemblyName assemblyName, Evidence assemblySecurity, params string[] args)
    {
      if (assemblySecurity != null && !this.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      RuntimeAssembly assembly = (RuntimeAssembly) Assembly.Load(assemblyName, assemblySecurity);
      if (args == null)
        args = new string[0];
      return this.nExecuteAssembly(assembly, args);
    }

    /// <summary>根据给定的 <see cref="T:System.Reflection.AssemblyName" /> 使用指定的参数执行程序集。</summary>
    /// <returns>程序集的入口点返回的值。</returns>
    /// <param name="assemblyName">
    /// <see cref="T:System.Reflection.AssemblyName" /> 对象，表示程序集名称。</param>
    /// <param name="args">启动该进程时传递的命令行参数。</param>
    /// <exception cref="T:System.IO.FileNotFoundException">通过指定的程序集 <paramref name="assemblyName" /> 找不到。</exception>
    /// <exception cref="T:System.IO.FileLoadException">通过指定的程序集 <paramref name="assemblyName" /> 找到了，但无法加载。</exception>
    /// <exception cref="T:System.BadImageFormatException">通过指定的程序集 <paramref name="assemblyName" /> 不是有效的程序集。- 或 -<paramref name="assemblyName" /> 已使用比当前加载的版本更高版本的公共语言运行时编译。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.MissingMethodException">指定的程序集具有不到入口点。</exception>
    public int ExecuteAssemblyByName(AssemblyName assemblyName, params string[] args)
    {
      RuntimeAssembly assembly = (RuntimeAssembly) Assembly.Load(assemblyName);
      if (args == null)
        args = new string[0];
      return this.nExecuteAssembly(assembly, args);
    }

    internal EvidenceBase GetHostEvidence(Type type)
    {
      if (this._SecurityIdentity != null)
        return this._SecurityIdentity.GetHostEvidence(type);
      return new Evidence((IRuntimeEvidenceFactory) new AppDomainEvidenceFactory(this)).GetHostEvidence(type);
    }

    /// <summary>获取一个字符串表示，包含应用程序域友好名称和任意上下文策略。</summary>
    /// <returns>一个字符串，通过连接字符串“Name:”、应用程序域的友好名称以及上下文策略的字符串表示或字符串“There are no context policies”而成。 </returns>
    /// <exception cref="T:System.AppDomainUnloadedException">表示由当前的应用程序域 <see cref="T:System.AppDomain" /> 已被卸载。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public override string ToString()
    {
      StringBuilder sb = StringBuilderCache.Acquire(16);
      string friendlyName = this.nGetFriendlyName();
      if (friendlyName != null)
      {
        sb.Append(Environment.GetResourceString("Loader_Name") + friendlyName);
        sb.Append(Environment.NewLine);
      }
      if (this._Policies == null || this._Policies.Length == 0)
      {
        sb.Append(Environment.GetResourceString("Loader_NoContextPolicies") + Environment.NewLine);
      }
      else
      {
        sb.Append(Environment.GetResourceString("Loader_ContextPolicies") + Environment.NewLine);
        for (int index = 0; index < this._Policies.Length; ++index)
        {
          sb.Append(this._Policies[index]);
          sb.Append(Environment.NewLine);
        }
      }
      return StringBuilderCache.GetStringAndRelease(sb);
    }

    /// <summary>获取已加载到此应用程序域的执行上下文中的程序集。</summary>
    /// <returns>此应用程序域中的程序集的数组。</returns>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    public Assembly[] GetAssemblies()
    {
      return this.nGetAssemblies(false);
    }

    /// <summary>返回已加载到应用程序域的只反射上下文中的程序集。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.Assembly" /> 对象数组，表示加载到应用程序域的只反射上下文中的程序集。</returns>
    /// <exception cref="T:System.AppDomainUnloadedException">对已卸载的应用程序域中尝试进行操作。</exception>
    /// <filterpriority>2</filterpriority>
    public Assembly[] ReflectionOnlyGetAssemblies()
    {
      return this.nGetAssemblies(true);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private Assembly[] nGetAssemblies(bool forIntrospection);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal bool IsUnloadingForcedFinalize();

    /// <summary>指示此应用程序域是否正在卸载以及公共语言运行时是否正在终止该域包含的对象。</summary>
    /// <returns>如果此应用程序域正在卸载，并且公共语言运行时已开始调用终止程序，则为 true；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public bool IsFinalizingForUnload();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void PublishAnonymouslyHostedDynamicMethodsAssembly(RuntimeAssembly assemblyHandle);

    /// <summary>将指定的目录名追加到专用路径列表。</summary>
    /// <param name="path">要追加到专用路径的目录名称。</param>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
    /// </PermissionSet>
    [SecurityCritical]
    [Obsolete("AppDomain.AppendPrivatePath has been deprecated. Please investigate the use of AppDomainSetup.PrivateBinPath instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    public void AppendPrivatePath(string path)
    {
      if (path == null || path.Length == 0)
        return;
      string str1 = this.FusionStore.Value[5];
      StringBuilder sb = StringBuilderCache.Acquire(16);
      if (str1 != null && str1.Length > 0)
      {
        sb.Append(str1);
        string str2 = str1;
        int index = str2.Length - 1;
        if ((int) str2[index] != (int) Path.PathSeparator && (int) path[0] != (int) Path.PathSeparator)
          sb.Append(Path.PathSeparator);
      }
      sb.Append(path);
      this.InternalSetPrivateBinPath(StringBuilderCache.GetStringAndRelease(sb));
    }

    /// <summary>将指定专用程序集位置的路径重置为空字符串 ("")。</summary>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
    /// </PermissionSet>
    [SecurityCritical]
    [Obsolete("AppDomain.ClearPrivatePath has been deprecated. Please investigate the use of AppDomainSetup.PrivateBinPath instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    public void ClearPrivatePath()
    {
      this.InternalSetPrivateBinPath(string.Empty);
    }

    /// <summary>将包含影像复制的程序集的目录列表重置为空字符串 ("")。</summary>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
    /// </PermissionSet>
    [SecurityCritical]
    [Obsolete("AppDomain.ClearShadowCopyPath has been deprecated. Please investigate the use of AppDomainSetup.ShadowCopyDirectories instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    public void ClearShadowCopyPath()
    {
      this.InternalSetShadowCopyPath(string.Empty);
    }

    /// <summary>确定指定目录路径为对程序集进行影像复制的位置。</summary>
    /// <param name="path">到卷影副本位置的完全限定路径。</param>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
    /// </PermissionSet>
    [SecurityCritical]
    [Obsolete("AppDomain.SetCachePath has been deprecated. Please investigate the use of AppDomainSetup.CachePath instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    public void SetCachePath(string path)
    {
      this.InternalSetCachePath(path);
    }

    /// <summary>为指定的应用程序域属性分配指定值。</summary>
    /// <param name="name">要创建或更改的用户定义应用程序域属性的名称。</param>
    /// <param name="data">该属性的值。</param>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
    /// </PermissionSet>
    [SecurityCritical]
    public void SetData(string name, object data)
    {
      this.SetDataHelper(name, data, (IPermission) null);
    }

    /// <summary>将指定值分配给指定应用程序域属性，检索该属性时要求调用方具有指定权限。</summary>
    /// <param name="name">要创建或更改的用户定义应用程序域属性的名称。</param>
    /// <param name="data">该属性的值。</param>
    /// <param name="permission">检索属性时调用方需要具有的权限。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="name" /> 指定系统定义的属性字符串和 <paramref name="permission" /> 不是 null。</exception>
    [SecurityCritical]
    public void SetData(string name, object data, IPermission permission)
    {
      this.SetDataHelper(name, data, permission);
    }

    [SecurityCritical]
    private void SetDataHelper(string name, object data, IPermission permission)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (name.Equals("TargetFrameworkName"))
      {
        this._FusionStore.TargetFrameworkName = (string) data;
      }
      else
      {
        if (name.Equals("IgnoreSystemPolicy"))
        {
          lock (this)
          {
            if (!this._HasSetPolicy)
              throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_SetData"));
          }
          new PermissionSet(PermissionState.Unrestricted).Demand();
        }
        int index = AppDomainSetup.Locate(name);
        if (index == -1)
        {
          lock (((ICollection) this.LocalStore).SyncRoot)
            this.LocalStore[name] = new object[2]
            {
              data,
              (object) permission
            };
        }
        else
        {
          if (permission != null)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_SetData"));
          switch (index)
          {
            case 2:
              this.FusionStore.DynamicBase = (string) data;
              break;
            case 3:
              this.FusionStore.DeveloperPath = (string) data;
              break;
            case 7:
              this.FusionStore.ShadowCopyDirectories = (string) data;
              break;
            case 11:
              if (data != null)
              {
                this.FusionStore.DisallowPublisherPolicy = true;
                break;
              }
              this.FusionStore.DisallowPublisherPolicy = false;
              break;
            case 12:
              if (data != null)
              {
                this.FusionStore.DisallowCodeDownload = true;
                break;
              }
              this.FusionStore.DisallowCodeDownload = false;
              break;
            case 13:
              if (data != null)
              {
                this.FusionStore.DisallowBindingRedirects = true;
                break;
              }
              this.FusionStore.DisallowBindingRedirects = false;
              break;
            case 14:
              if (data != null)
              {
                this.FusionStore.DisallowApplicationBaseProbing = true;
                break;
              }
              this.FusionStore.DisallowApplicationBaseProbing = false;
              break;
            case 15:
              this.FusionStore.SetConfigurationBytes((byte[]) data);
              break;
            default:
              this.FusionStore.Value[index] = (string) data;
              break;
          }
        }
      }
    }

    /// <summary>为指定名称获取存储在当前应用程序域中的值。</summary>
    /// <returns>
    /// <paramref name="name" /> 属性的值，或 null（如果属性不存在）。</returns>
    /// <param name="name">预定义应用程序域属性的名称，或已定义的应用程序域属性的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public object GetData(string name)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      switch (AppDomainSetup.Locate(name))
      {
        case -1:
          if (name.Equals(AppDomainSetup.LoaderOptimizationKey))
            return (object) this.FusionStore.LoaderOptimization;
          object[] objArray;
          lock (((ICollection) this.LocalStore).SyncRoot)
            this.LocalStore.TryGetValue(name, out objArray);
          if (objArray == null)
            return (object) null;
          if (objArray[1] != null)
            ((IPermission) objArray[1]).Demand();
          return objArray[0];
        case 0:
          return (object) this.FusionStore.ApplicationBase;
        case 1:
          return (object) this.FusionStore.ConfigurationFile;
        case 2:
          return (object) this.FusionStore.DynamicBase;
        case 3:
          return (object) this.FusionStore.DeveloperPath;
        case 4:
          return (object) this.FusionStore.ApplicationName;
        case 5:
          return (object) this.FusionStore.PrivateBinPath;
        case 6:
          return (object) this.FusionStore.PrivateBinPathProbe;
        case 7:
          return (object) this.FusionStore.ShadowCopyDirectories;
        case 8:
          return (object) this.FusionStore.ShadowCopyFiles;
        case 9:
          return (object) this.FusionStore.CachePath;
        case 10:
          return (object) this.FusionStore.LicenseFile;
        case 11:
          return (object) this.FusionStore.DisallowPublisherPolicy;
        case 12:
          return (object) this.FusionStore.DisallowCodeDownload;
        case 13:
          return (object) this.FusionStore.DisallowBindingRedirects;
        case 14:
          return (object) this.FusionStore.DisallowApplicationBaseProbing;
        case 15:
          return (object) this.FusionStore.GetConfigurationBytes();
        default:
          return (object) null;
      }
    }

    /// <summary>获取可以为 null 的布尔值，该值指示是否设置了任何兼容性开关，如果已设置，则指定是否设置了指定的兼容性开关。</summary>
    /// <returns>如果未设置任何兼容性开关，则为 null 引用（Visual Basic 中的 Nothing）；否则，为布尔值，指示是否设置了由 <paramref name="value" /> 指定的兼容性开关。</returns>
    /// <param name="value">要测试的兼容性开关。</param>
    public bool? IsCompatibilitySwitchSet(string value)
    {
      return this._compatFlagsInitialized ? new bool?(this._compatFlags != null && this._compatFlags.ContainsKey(value)) : new bool?();
    }

    /// <summary>获取当前线程标识符。</summary>
    /// <returns>一个 32 位带符号整数，它是当前线程的标识符。</returns>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [Obsolete("AppDomain.GetCurrentThreadId has been deprecated because it does not provide a stable Id when managed threads are running on fibers (aka lightweight threads). To get a stable identifier for a managed thread, use the ManagedThreadId property on Thread.  http://go.microsoft.com/fwlink/?linkid=14202", false)]
    [DllImport("kernel32.dll")]
    public static extern int GetCurrentThreadId();

    /// <summary>卸载指定的应用程序域。</summary>
    /// <param name="domain">要卸载的应用程序域。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="domain" /> 为 null。</exception>
    /// <exception cref="T:System.CannotUnloadAppDomainException">
    /// <paramref name="domain" /> 不能被卸载。</exception>
    /// <exception cref="T:System.Exception">在卸载过程中出错。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.MayFail)]
    [SecurityPermission(SecurityAction.Demand, ControlAppDomain = true)]
    public static void Unload(AppDomain domain)
    {
      if (domain == null)
        throw new ArgumentNullException("domain");
      try
      {
        int idForUnload = AppDomain.GetIdForUnload(domain);
        if (idForUnload == 0)
          throw new CannotUnloadAppDomainException();
        AppDomain.nUnload(idForUnload);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>为此应用程序域确定安全策略级别。</summary>
    /// <param name="domainPolicy">安全策略级别。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="domainPolicy" /> 为 null。</exception>
    /// <exception cref="T:System.Security.Policy.PolicyException">已设置的安全策略级别。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlDomainPolicy" />
    /// </PermissionSet>
    [SecurityCritical]
    [Obsolete("AppDomain policy levels are obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public void SetAppDomainPolicy(PolicyLevel domainPolicy)
    {
      if (domainPolicy == null)
        throw new ArgumentNullException("domainPolicy");
      if (!this.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
      lock (this)
      {
        if (this._HasSetPolicy)
          throw new PolicyException(Environment.GetResourceString("Policy_PolicyAlreadySet"));
        this._HasSetPolicy = true;
        this.nChangeSecurityPolicy();
      }
      SecurityManager.PolicyManager.AddLevel(domainPolicy);
    }

    /// <summary>设置在以下情况下要附加到线程的默认主体对象，即当线程在此应用程序域中执行时，如果线程尝试绑定到主体这种情况。</summary>
    /// <param name="principal">要附加到线程的主体对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="principal" /> 为 null。</exception>
    /// <exception cref="T:System.Security.Policy.PolicyException">线程主体已设置。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
    public void SetThreadPrincipal(IPrincipal principal)
    {
      if (principal == null)
        throw new ArgumentNullException("principal");
      lock (this)
      {
        if (this._DefaultPrincipal != null)
          throw new PolicyException(Environment.GetResourceString("Policy_PrincipalTwice"));
        this._DefaultPrincipal = principal;
      }
    }

    /// <summary>指定在此应用程序域中执行时如果线程尝试绑定到用户，用户和标识对象应如何附加到该线程。</summary>
    /// <param name="policy">
    /// <see cref="T:System.Security.Principal.PrincipalPolicy" /> 值之一，指定要附加到线程的主体对象类型。</param>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
    public void SetPrincipalPolicy(PrincipalPolicy policy)
    {
      this._PrincipalPolicy = policy;
    }

    /// <summary>通过防止创建租约来给予 <see cref="T:System.AppDomain" /> 无限生存期。</summary>
    /// <returns>总是为 null。</returns>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    [SecurityCritical]
    public override object InitializeLifetimeService()
    {
      return (object) null;
    }

    /// <summary>在另一个应用程序域中执行代码，该应用程序域由指定的委托标识。</summary>
    /// <param name="callBackDelegate">指定要调用的方法的委托。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="callBackDelegate" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    public void DoCallBack(CrossAppDomainDelegate callBackDelegate)
    {
      if (callBackDelegate == null)
        throw new ArgumentNullException("callBackDelegate");
      callBackDelegate();
    }

    /// <summary>使用所提供的证据创建具有给定名称的新应用程序域。</summary>
    /// <returns>新创建的应用程序域。</returns>
    /// <param name="friendlyName">域的友好名称。此友好名称可在用户界面中显示以标识域。有关详细信息，请参阅<see cref="P:System.AppDomain.FriendlyName" />。</param>
    /// <param name="securityInfo">确定代码标识的证据，该代码在应用程序域中运行。传递 null 以使用当前应用程序域的证据。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="friendlyName" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlAppDomain" />
    /// </PermissionSet>
    public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo)
    {
      return AppDomain.CreateDomain(friendlyName, securityInfo, (AppDomainSetup) null);
    }

    /// <summary>使用证据、应用程序基路径、相对搜索路径和指定是否向应用程序域中加载程序集的影像副本的形参创建具有给定名称的新应用程序域。</summary>
    /// <returns>新创建的应用程序域。</returns>
    /// <param name="friendlyName">域的友好名称。此友好名称可在用户界面中显示以标识域。有关详细信息，请参阅<see cref="P:System.AppDomain.FriendlyName" />。</param>
    /// <param name="securityInfo">确定代码标识的证据，该代码在应用程序域中运行。传递 null 以使用当前应用程序域的证据。</param>
    /// <param name="appBasePath">基目录，由程序集冲突解决程序用来探测程序集。有关详细信息，请参阅<see cref="P:System.AppDomain.BaseDirectory" />。</param>
    /// <param name="appRelativeSearchPath">相对于基目录的路径，在此程序集冲突解决程序应探测专用程序集。有关详细信息，请参阅<see cref="P:System.AppDomain.RelativeSearchPath" />。</param>
    /// <param name="shadowCopyFiles">如果为 true，则向此应用程序域中加载程序集的卷影副本。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="friendlyName" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlAppDomain" />
    /// </PermissionSet>
    public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, string appBasePath, string appRelativeSearchPath, bool shadowCopyFiles)
    {
      AppDomainSetup info = new AppDomainSetup();
      info.ApplicationBase = appBasePath;
      info.PrivateBinPath = appRelativeSearchPath;
      if (shadowCopyFiles)
        info.ShadowCopyFiles = "true";
      return AppDomain.CreateDomain(friendlyName, securityInfo, info);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private string GetDynamicDir();

    /// <summary>使用指定的名称新建应用程序域。</summary>
    /// <returns>新创建的应用程序域。</returns>
    /// <param name="friendlyName">域的友好名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="friendlyName" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlAppDomain" />
    /// </PermissionSet>
    public static AppDomain CreateDomain(string friendlyName)
    {
      return AppDomain.CreateDomain(friendlyName, (Evidence) null, (AppDomainSetup) null);
    }

    [SecurityCritical]
    private static byte[] MarshalObject(object o)
    {
      CodeAccessPermission.Assert(true);
      return AppDomain.Serialize(o);
    }

    [SecurityCritical]
    private static byte[] MarshalObjects(object o1, object o2, out byte[] blob2)
    {
      CodeAccessPermission.Assert(true);
      byte[] numArray = AppDomain.Serialize(o1);
      blob2 = AppDomain.Serialize(o2);
      return numArray;
    }

    [SecurityCritical]
    private static object UnmarshalObject(byte[] blob)
    {
      CodeAccessPermission.Assert(true);
      return AppDomain.Deserialize(blob);
    }

    [SecurityCritical]
    private static object UnmarshalObjects(byte[] blob1, byte[] blob2, out object o2)
    {
      CodeAccessPermission.Assert(true);
      object obj = AppDomain.Deserialize(blob1);
      o2 = AppDomain.Deserialize(blob2);
      return obj;
    }

    [SecurityCritical]
    private static byte[] Serialize(object o)
    {
      if (o == null)
        return (byte[]) null;
      if (o is ISecurityEncodable)
      {
        SecurityElement xml = ((ISecurityEncodable) o).ToXml();
        MemoryStream memoryStream = new MemoryStream(4096);
        int num = 0;
        memoryStream.WriteByte((byte) num);
        Encoding utF8 = Encoding.UTF8;
        StreamWriter writer = new StreamWriter((Stream) memoryStream, utF8);
        xml.ToWriter(writer);
        writer.Flush();
        return memoryStream.ToArray();
      }
      MemoryStream stm = new MemoryStream();
      stm.WriteByte((byte) 1);
      CrossAppDomainSerializer.SerializeObject(o, stm);
      return stm.ToArray();
    }

    [SecurityCritical]
    private static object Deserialize(byte[] blob)
    {
      if (blob == null)
        return (object) null;
      if ((int) blob[0] == 0)
      {
        SecurityElement topElement = new Parser(blob, Tokenizer.ByteTokenEncoding.UTF8Tokens, 1).GetTopElement();
        if (topElement.Tag.Equals("IPermission") || topElement.Tag.Equals("Permission"))
        {
          IPermission permission = XMLUtil.CreatePermission(topElement, PermissionState.None, false);
          if (permission == null)
            return (object) null;
          permission.FromXml(topElement);
          return (object) permission;
        }
        if (topElement.Tag.Equals("PermissionSet"))
        {
          PermissionSet permissionSet = new PermissionSet();
          SecurityElement et = topElement;
          int num1 = 0;
          int num2 = 0;
          permissionSet.FromXml(et, num1 != 0, num2 != 0);
          return (object) permissionSet;
        }
        if (!topElement.Tag.Equals("PermissionToken"))
          return (object) null;
        PermissionToken permissionToken = new PermissionToken();
        SecurityElement elRoot = topElement;
        permissionToken.FromXml(elRoot);
        return (object) permissionToken;
      }
      using (MemoryStream stm = new MemoryStream(blob, 1, blob.Length - 1))
        return CrossAppDomainSerializer.DeserializeObject(stm);
    }

    [SecurityCritical]
    internal static void Pause()
    {
      AppDomainPauseManager.Instance.Pausing();
      AppDomainPauseManager.Instance.Paused();
    }

    [SecurityCritical]
    internal static void Resume()
    {
      if (!AppDomainPauseManager.IsPaused)
        return;
      AppDomainPauseManager.Instance.Resuming();
      AppDomainPauseManager.Instance.Resumed();
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private int _nExecuteAssembly(RuntimeAssembly assembly, string[] args);

    internal int nExecuteAssembly(RuntimeAssembly assembly, string[] args)
    {
      return this._nExecuteAssembly(assembly, args);
    }

    internal void CreateRemotingData()
    {
      lock (this)
      {
        if (this._RemotingData != null)
          return;
        this._RemotingData = new DomainSpecificRemotingData();
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private string nGetFriendlyName();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private bool nIsDefaultAppDomainForEvidence();

    private void OnAssemblyLoadEvent(RuntimeAssembly LoadedAssembly)
    {
      // ISSUE: reference to a compiler-generated field
      AssemblyLoadEventHandler loadEventHandler = this.AssemblyLoad;
      if (loadEventHandler == null)
        return;
      AssemblyLoadEventArgs args = new AssemblyLoadEventArgs((Assembly) LoadedAssembly);
      loadEventHandler((object) this, args);
    }

    [SecurityCritical]
    private RuntimeAssembly OnResourceResolveEvent(RuntimeAssembly assembly, string resourceName)
    {
      ResolveEventHandler resolveEventHandler = this._ResourceResolve;
      if (resolveEventHandler == null)
        return (RuntimeAssembly) null;
      Delegate[] invocationList = resolveEventHandler.GetInvocationList();
      int length = invocationList.Length;
      for (int index = 0; index < length; ++index)
      {
        RuntimeAssembly runtimeAssembly = AppDomain.GetRuntimeAssembly(((ResolveEventHandler) invocationList[index])((object) this, new ResolveEventArgs(resourceName, (Assembly) assembly)));
        if ((Assembly) runtimeAssembly != (Assembly) null)
          return runtimeAssembly;
      }
      return (RuntimeAssembly) null;
    }

    [SecurityCritical]
    private RuntimeAssembly OnTypeResolveEvent(RuntimeAssembly assembly, string typeName)
    {
      ResolveEventHandler resolveEventHandler = this._TypeResolve;
      if (resolveEventHandler == null)
        return (RuntimeAssembly) null;
      Delegate[] invocationList = resolveEventHandler.GetInvocationList();
      int length = invocationList.Length;
      for (int index = 0; index < length; ++index)
      {
        RuntimeAssembly runtimeAssembly = AppDomain.GetRuntimeAssembly(((ResolveEventHandler) invocationList[index])((object) this, new ResolveEventArgs(typeName, (Assembly) assembly)));
        if ((Assembly) runtimeAssembly != (Assembly) null)
          return runtimeAssembly;
      }
      return (RuntimeAssembly) null;
    }

    [SecurityCritical]
    private RuntimeAssembly OnAssemblyResolveEvent(RuntimeAssembly assembly, string assemblyFullName)
    {
      ResolveEventHandler resolveEventHandler = this._AssemblyResolve;
      if (resolveEventHandler == null)
        return (RuntimeAssembly) null;
      Delegate[] invocationList = resolveEventHandler.GetInvocationList();
      int length = invocationList.Length;
      for (int index = 0; index < length; ++index)
      {
        RuntimeAssembly runtimeAssembly = AppDomain.GetRuntimeAssembly(((ResolveEventHandler) invocationList[index])((object) this, new ResolveEventArgs(assemblyFullName, (Assembly) assembly)));
        if ((Assembly) runtimeAssembly != (Assembly) null)
          return runtimeAssembly;
      }
      return (RuntimeAssembly) null;
    }

    private RuntimeAssembly OnReflectionOnlyAssemblyResolveEvent(RuntimeAssembly assembly, string assemblyFullName)
    {
      // ISSUE: reference to a compiler-generated field
      ResolveEventHandler resolveEventHandler = this.ReflectionOnlyAssemblyResolve;
      if (resolveEventHandler != null)
      {
        Delegate[] invocationList = resolveEventHandler.GetInvocationList();
        int length = invocationList.Length;
        for (int index = 0; index < length; ++index)
        {
          RuntimeAssembly runtimeAssembly = AppDomain.GetRuntimeAssembly(((ResolveEventHandler) invocationList[index])((object) this, new ResolveEventArgs(assemblyFullName, (Assembly) assembly)));
          if ((Assembly) runtimeAssembly != (Assembly) null)
            return runtimeAssembly;
        }
      }
      return (RuntimeAssembly) null;
    }

    private RuntimeAssembly[] OnReflectionOnlyNamespaceResolveEvent(RuntimeAssembly assembly, string namespaceName)
    {
      return WindowsRuntimeMetadata.OnReflectionOnlyNamespaceResolveEvent(this, assembly, namespaceName);
    }

    private string[] OnDesignerNamespaceResolveEvent(string namespaceName)
    {
      return WindowsRuntimeMetadata.OnDesignerNamespaceResolveEvent(this, namespaceName);
    }

    internal static RuntimeAssembly GetRuntimeAssembly(Assembly asm)
    {
      if (asm == (Assembly) null)
        return (RuntimeAssembly) null;
      RuntimeAssembly runtimeAssembly = asm as RuntimeAssembly;
      if ((Assembly) runtimeAssembly != (Assembly) null)
        return runtimeAssembly;
      AssemblyBuilder assemblyBuilder = asm as AssemblyBuilder;
      if ((Assembly) assemblyBuilder != (Assembly) null)
        return (RuntimeAssembly) assemblyBuilder.InternalAssembly;
      return (RuntimeAssembly) null;
    }

    private void TurnOnBindingRedirects()
    {
      this._FusionStore.DisallowBindingRedirects = false;
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    internal static int GetIdForUnload(AppDomain domain)
    {
      if (RemotingServices.IsTransparentProxy((object) domain))
        return RemotingServices.GetServerDomainIdForProxy((object) domain);
      return domain.Id;
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsDomainIdValid(int id);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern AppDomain GetDefaultDomain();

    internal IPrincipal GetThreadPrincipal()
    {
      IPrincipal principal;
      if (this._DefaultPrincipal == null)
      {
        switch (this._PrincipalPolicy)
        {
          case PrincipalPolicy.UnauthenticatedPrincipal:
            principal = (IPrincipal) new GenericPrincipal((IIdentity) new GenericIdentity("", ""), new string[1]{ "" });
            break;
          case PrincipalPolicy.NoPrincipal:
            principal = (IPrincipal) null;
            break;
          case PrincipalPolicy.WindowsPrincipal:
            principal = (IPrincipal) new WindowsPrincipal(WindowsIdentity.GetCurrent());
            break;
          default:
            principal = (IPrincipal) null;
            break;
        }
      }
      else
        principal = this._DefaultPrincipal;
      return principal;
    }

    [SecurityCritical]
    internal void CreateDefaultContext()
    {
      lock (this)
      {
        if (this._DefaultContext != null)
          return;
        this._DefaultContext = Context.CreateDefaultContext();
      }
    }

    [SecurityCritical]
    internal Context GetDefaultContext()
    {
      if (this._DefaultContext == null)
        this.CreateDefaultContext();
      return this._DefaultContext;
    }

    [SecuritySafeCritical]
    internal static void CheckDomainCreationEvidence(AppDomainSetup creationDomainSetup, Evidence creationEvidence)
    {
      if (creationEvidence == null || AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled || creationDomainSetup != null && creationDomainSetup.ApplicationTrust != null)
        return;
      Zone hostEvidence1 = AppDomain.CurrentDomain.EvidenceNoDemand.GetHostEvidence<Zone>();
      SecurityZone securityZone = hostEvidence1 != null ? hostEvidence1.SecurityZone : SecurityZone.MyComputer;
      Zone hostEvidence2 = creationEvidence.GetHostEvidence<Zone>();
      if (hostEvidence2 != null && hostEvidence2.SecurityZone != securityZone && hostEvidence2.SecurityZone != SecurityZone.MyComputer)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
    }

    /// <summary>使用指定的名称、证据和应用程序域设置信息创建新的应用程序域。</summary>
    /// <returns>新创建的应用程序域。</returns>
    /// <param name="friendlyName">域的友好名称。此友好名称可在用户界面中显示以标识域。有关详细信息，请参阅<see cref="P:System.AppDomain.FriendlyName" />。</param>
    /// <param name="securityInfo">确定代码标识的证据，该代码在应用程序域中运行。传递 null 以使用当前应用程序域的证据。</param>
    /// <param name="info">包含应用程序域初始化信息的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="friendlyName" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlAppDomain" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, ControlAppDomain = true)]
    public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, AppDomainSetup info)
    {
      return AppDomain.InternalCreateDomain(friendlyName, securityInfo, info);
    }

    [SecurityCritical]
    internal static AppDomain InternalCreateDomain(string friendlyName, Evidence securityInfo, AppDomainSetup info)
    {
      if (friendlyName == null)
        throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_String"));
      AppDomain.CheckCreateDomainSupported();
      if (info == null)
        info = new AppDomainSetup();
      if (info.TargetFrameworkName == null)
        info.TargetFrameworkName = AppDomain.CurrentDomain.GetTargetFrameworkName();
      AppDomainManager domainManager = AppDomain.CurrentDomain.DomainManager;
      if (domainManager != null)
        return domainManager.CreateDomain(friendlyName, securityInfo, info);
      if (securityInfo != null)
      {
        new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
        AppDomain.CheckDomainCreationEvidence(info, securityInfo);
      }
      string friendlyName1 = friendlyName;
      AppDomainSetup setup = info;
      Evidence providedSecurityInfo = securityInfo;
      Evidence creatorsSecurityInfo = providedSecurityInfo == null ? AppDomain.CurrentDomain.InternalEvidence : (Evidence) null;
      IntPtr securityDescriptor = AppDomain.CurrentDomain.GetSecurityDescriptor();
      return AppDomain.nCreateDomain(friendlyName1, setup, providedSecurityInfo, creatorsSecurityInfo, securityDescriptor);
    }

    /// <summary>使用指定的名称、证据、应用程序域设置信息、默认权限集和一组完全受信任的程序集创建新的应用程序域。</summary>
    /// <returns>新创建的应用程序域。</returns>
    /// <param name="friendlyName">域的友好名称。此友好名称可在用户界面中显示以标识域。有关更多信息，请参见 <see cref="P:System.AppDomain.FriendlyName" /> 的说明。</param>
    /// <param name="securityInfo">确定代码标识的证据，该代码在应用程序域中运行。传递 null 以使用当前应用程序域的证据。</param>
    /// <param name="info">包含应用程序域初始化信息的对象。</param>
    /// <param name="grantSet">一个默认权限集，被授予加载到新应用程序域的所有无特定权限的程序集。</param>
    /// <param name="fullTrustAssemblies">一组强名称，表示在新应用程序域中被认为完全受信任的程序集。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="friendlyName" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">应用程序域是 null。- 或 -<see cref="P:System.AppDomainSetup.ApplicationBase" /> 上未设置属性 <see cref="T:System.AppDomainSetup" /> 为提供的对象 <paramref name="info" />。</exception>
    public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, AppDomainSetup info, PermissionSet grantSet, params StrongName[] fullTrustAssemblies)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      if (info.ApplicationBase == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AppDomainSandboxAPINeedsExplicitAppBase"));
      if (fullTrustAssemblies == null)
        fullTrustAssemblies = new StrongName[0];
      info.ApplicationTrust = new ApplicationTrust(grantSet, (IEnumerable<StrongName>) fullTrustAssemblies);
      return AppDomain.CreateDomain(friendlyName, securityInfo, info);
    }

    /// <summary>使用证据、应用程序基路径、相对搜索路径和指定是否向应用程序域中加载程序集的影像副本的形参创建具有给定名称的新应用程序域。指定在初始化应用程序域时调用的回调方法，以及传递回调方法的字符串实参数组。</summary>
    /// <returns>新创建的应用程序域。</returns>
    /// <param name="friendlyName">域的友好名称。此友好名称可在用户界面中显示以标识域。有关详细信息，请参阅<see cref="P:System.AppDomain.FriendlyName" />。</param>
    /// <param name="securityInfo">确定代码标识的证据，该代码在应用程序域中运行。传递 null 以使用当前应用程序域的证据。</param>
    /// <param name="appBasePath">基目录，由程序集冲突解决程序用来探测程序集。有关详细信息，请参阅<see cref="P:System.AppDomain.BaseDirectory" />。</param>
    /// <param name="appRelativeSearchPath">相对于基目录的路径，在此程序集冲突解决程序应探测专用程序集。有关详细信息，请参阅<see cref="P:System.AppDomain.RelativeSearchPath" />。</param>
    /// <param name="shadowCopyFiles">如果为 true，则将程序集的卷影副本加载到应用程序域中。</param>
    /// <param name="adInit">
    /// <see cref="T:System.AppDomainInitializer" /> 委托，表示初始化新的 <see cref="T:System.AppDomain" /> 对象时调用的回调方法。</param>
    /// <param name="adInitArgs">字符串实参数组，在初始化新的 <see cref="T:System.AppDomain" /> 对象时传递给由 <paramref name="adInit" /> 表示的回调。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="friendlyName" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlAppDomain" />
    /// </PermissionSet>
    public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, string appBasePath, string appRelativeSearchPath, bool shadowCopyFiles, AppDomainInitializer adInit, string[] adInitArgs)
    {
      AppDomainSetup info = new AppDomainSetup();
      info.ApplicationBase = appBasePath;
      info.PrivateBinPath = appRelativeSearchPath;
      info.AppDomainInitializer = adInit;
      info.AppDomainInitializerArguments = adInitArgs;
      if (shadowCopyFiles)
        info.ShadowCopyFiles = "true";
      return AppDomain.CreateDomain(friendlyName, securityInfo, info);
    }

    [SecurityCritical]
    private void SetupFusionStore(AppDomainSetup info, AppDomainSetup oldInfo)
    {
      if (oldInfo == null)
      {
        if (info.Value[0] == null || info.Value[1] == null)
        {
          AppDomain defaultDomain = AppDomain.GetDefaultDomain();
          if (this == defaultDomain)
          {
            info.SetupDefaults(RuntimeEnvironment.GetModuleFileName(), true);
          }
          else
          {
            if (info.Value[1] == null)
              info.ConfigurationFile = defaultDomain.FusionStore.Value[1];
            if (info.Value[0] == null)
              info.ApplicationBase = defaultDomain.FusionStore.Value[0];
            if (info.Value[4] == null)
              info.ApplicationName = defaultDomain.FusionStore.Value[4];
          }
        }
        if (info.Value[5] == null)
          info.PrivateBinPath = Environment.nativeGetEnvironmentVariable(AppDomainSetup.PrivateBinPathEnvironmentVariable);
        if (info.DeveloperPath == null)
          info.DeveloperPath = RuntimeEnvironment.GetDeveloperPath();
      }
      IntPtr fusionContext = this.GetFusionContext();
      info.SetupFusionContext(fusionContext, oldInfo);
      if (info.LoaderOptimization != LoaderOptimization.NotSpecified || oldInfo != null && info.LoaderOptimization != oldInfo.LoaderOptimization)
        this.UpdateLoaderOptimization(info.LoaderOptimization);
      this._FusionStore = info;
    }

    private static void RunInitializer(AppDomainSetup setup)
    {
      if (setup.AppDomainInitializer == null)
        return;
      string[] args = (string[]) null;
      if (setup.AppDomainInitializerArguments != null)
        args = (string[]) setup.AppDomainInitializerArguments.Clone();
      setup.AppDomainInitializer(args);
    }

    [SecurityCritical]
    private static object PrepareDataForSetup(string friendlyName, AppDomainSetup setup, Evidence providedSecurityInfo, Evidence creatorsSecurityInfo, IntPtr parentSecurityDescriptor, string sandboxName, string[] propertyNames, string[] propertyValues)
    {
      byte[] numArray = (byte[]) null;
      bool flag = false;
      AppDomain.EvidenceCollection evidenceCollection = (AppDomain.EvidenceCollection) null;
      if (providedSecurityInfo != null || creatorsSecurityInfo != null)
      {
        HostSecurityManager hostSecurityManager = AppDomain.CurrentDomain.DomainManager != null ? AppDomain.CurrentDomain.DomainManager.HostSecurityManager : (HostSecurityManager) null;
        if ((hostSecurityManager == null || !(hostSecurityManager.GetType() != typeof (HostSecurityManager)) ? 0 : ((hostSecurityManager.Flags & HostSecurityManagerOptions.HostAppDomainEvidence) == HostSecurityManagerOptions.HostAppDomainEvidence ? 1 : 0)) == 0)
        {
          if (providedSecurityInfo != null && providedSecurityInfo.IsUnmodified && (providedSecurityInfo.Target != null && providedSecurityInfo.Target is AppDomainEvidenceFactory))
          {
            providedSecurityInfo = (Evidence) null;
            flag = true;
          }
          if (creatorsSecurityInfo != null && creatorsSecurityInfo.IsUnmodified && (creatorsSecurityInfo.Target != null && creatorsSecurityInfo.Target is AppDomainEvidenceFactory))
          {
            creatorsSecurityInfo = (Evidence) null;
            flag = true;
          }
        }
      }
      if (providedSecurityInfo != null || creatorsSecurityInfo != null)
      {
        evidenceCollection = new AppDomain.EvidenceCollection();
        evidenceCollection.ProvidedSecurityInfo = providedSecurityInfo;
        evidenceCollection.CreatorsSecurityInfo = creatorsSecurityInfo;
      }
      if (evidenceCollection != null)
        numArray = CrossAppDomainSerializer.SerializeObject((object) evidenceCollection).GetBuffer();
      AppDomainInitializerInfo domainInitializerInfo = (AppDomainInitializerInfo) null;
      if (setup != null && setup.AppDomainInitializer != null)
        domainInitializerInfo = new AppDomainInitializerInfo(setup.AppDomainInitializer);
      AppDomainSetup appDomainSetup = new AppDomainSetup(setup, false);
      return (object) new object[9]
      {
        (object) friendlyName,
        (object) appDomainSetup,
        (object) parentSecurityDescriptor,
        (object) flag,
        (object) numArray,
        (object) domainInitializerInfo,
        (object) sandboxName,
        (object) propertyNames,
        (object) propertyValues
      };
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static object Setup(object arg)
    {
      object[] objArray = (object[]) arg;
      int index1 = 0;
      string friendlyName = (string) objArray[index1];
      int index2 = 1;
      AppDomainSetup copy = (AppDomainSetup) objArray[index2];
      int index3 = 2;
      IntPtr parentSecurityDescriptor = (IntPtr) objArray[index3];
      int index4 = 3;
      bool generateDefaultEvidence = (bool) objArray[index4];
      int index5 = 4;
      byte[] buffer = (byte[]) objArray[index5];
      int index6 = 5;
      AppDomainInitializerInfo domainInitializerInfo = (AppDomainInitializerInfo) objArray[index6];
      int index7 = 6;
      string str1 = (string) objArray[index7];
      int index8 = 7;
      string[] strArray1 = (string[]) objArray[index8];
      int index9 = 8;
      string[] strArray2 = (string[]) objArray[index9];
      Evidence providedSecurityInfo = (Evidence) null;
      Evidence creatorsSecurityInfo = (Evidence) null;
      AppDomain currentDomain = AppDomain.CurrentDomain;
      AppDomainSetup info = new AppDomainSetup(copy, false);
      if (strArray1 != null && strArray2 != null)
      {
        for (int index10 = 0; index10 < strArray1.Length; ++index10)
        {
          if (strArray1[index10] == "APPBASE")
          {
            if (strArray2[index10] == null)
              throw new ArgumentNullException("APPBASE");
            if (Path.IsRelative(strArray2[index10]))
              throw new ArgumentException(Environment.GetResourceString("Argument_AbsolutePathRequired"));
            info.ApplicationBase = Path.NormalizePath(strArray2[index10], true);
          }
          else if (strArray1[index10] == "LOCATION_URI" && providedSecurityInfo == null)
          {
            providedSecurityInfo = new Evidence();
            providedSecurityInfo.AddHostEvidence<Url>(new Url(strArray2[index10]));
            currentDomain.SetDataHelper(strArray1[index10], (object) strArray2[index10], (IPermission) null);
          }
          else if (strArray1[index10] == "LOADER_OPTIMIZATION")
          {
            if (strArray2[index10] == null)
              throw new ArgumentNullException("LOADER_OPTIMIZATION");
            string str2 = strArray2[index10];
            if (!(str2 == "SingleDomain"))
            {
              if (!(str2 == "MultiDomain"))
              {
                if (!(str2 == "MultiDomainHost"))
                {
                  if (!(str2 == "NotSpecified"))
                    throw new ArgumentException(Environment.GetResourceString("Argument_UnrecognizedLoaderOptimization"), "LOADER_OPTIMIZATION");
                  info.LoaderOptimization = LoaderOptimization.NotSpecified;
                }
                else
                  info.LoaderOptimization = LoaderOptimization.MultiDomainHost;
              }
              else
                info.LoaderOptimization = LoaderOptimization.MultiDomain;
            }
            else
              info.LoaderOptimization = LoaderOptimization.SingleDomain;
          }
        }
      }
      AppDomainSortingSetupInfo sortingSetupInfo = info._AppDomainSortingSetupInfo;
      if (sortingSetupInfo != null && (sortingSetupInfo._pfnIsNLSDefinedString == IntPtr.Zero || sortingSetupInfo._pfnCompareStringEx == IntPtr.Zero || (sortingSetupInfo._pfnLCMapStringEx == IntPtr.Zero || sortingSetupInfo._pfnFindNLSStringEx == IntPtr.Zero) || (sortingSetupInfo._pfnCompareStringOrdinal == IntPtr.Zero || sortingSetupInfo._pfnGetNLSVersionEx == IntPtr.Zero)) && (!(sortingSetupInfo._pfnIsNLSDefinedString == IntPtr.Zero) || !(sortingSetupInfo._pfnCompareStringEx == IntPtr.Zero) || (!(sortingSetupInfo._pfnLCMapStringEx == IntPtr.Zero) || !(sortingSetupInfo._pfnFindNLSStringEx == IntPtr.Zero)) || (!(sortingSetupInfo._pfnCompareStringOrdinal == IntPtr.Zero) || !(sortingSetupInfo._pfnGetNLSVersionEx == IntPtr.Zero))))
        throw new ArgumentException(Environment.GetResourceString("ArgumentException_NotAllCustomSortingFuncsDefined"));
      currentDomain.SetupFusionStore(info, (AppDomainSetup) null);
      AppDomainSetup fusionStore = currentDomain.FusionStore;
      if (buffer != null)
      {
        AppDomain.EvidenceCollection evidenceCollection = (AppDomain.EvidenceCollection) CrossAppDomainSerializer.DeserializeObject(new MemoryStream(buffer));
        providedSecurityInfo = evidenceCollection.ProvidedSecurityInfo;
        creatorsSecurityInfo = evidenceCollection.CreatorsSecurityInfo;
      }
      currentDomain.nSetupFriendlyName(friendlyName);
      if (copy != null && copy.SandboxInterop)
        currentDomain.nSetDisableInterfaceCache();
      if (fusionStore.AppDomainManagerAssembly != null && fusionStore.AppDomainManagerType != null)
        currentDomain.SetAppDomainManagerType(fusionStore.AppDomainManagerAssembly, fusionStore.AppDomainManagerType);
      currentDomain.PartialTrustVisibleAssemblies = fusionStore.PartialTrustVisibleAssemblies;
      currentDomain.CreateAppDomainManager();
      currentDomain.InitializeDomainSecurity(providedSecurityInfo, creatorsSecurityInfo, generateDefaultEvidence, parentSecurityDescriptor, true);
      if (domainInitializerInfo != null)
        fusionStore.AppDomainInitializer = domainInitializerInfo.Unwrap();
      AppDomain.RunInitializer(fusionStore);
      ObjectHandle objectHandle = (ObjectHandle) null;
      if (fusionStore.ActivationArguments != null && fusionStore.ActivationArguments.ActivateInstance)
        objectHandle = Activator.CreateInstance(currentDomain.ActivationContext);
      return (object) RemotingServices.MarshalInternal((MarshalByRefObject) objectHandle, (string) null, (Type) null);
    }

    [SecuritySafeCritical]
    [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
    private bool IsAssemblyOnAptcaVisibleList(RuntimeAssembly assembly)
    {
      if (this._aptcaVisibleAssemblies == null)
        return false;
      return Array.BinarySearch<string>(this._aptcaVisibleAssemblies, assembly.GetName().GetNameWithPublicKey().ToUpperInvariant(), (IComparer<string>) StringComparer.OrdinalIgnoreCase) >= 0;
    }

    [SecurityCritical]
    private unsafe bool IsAssemblyOnAptcaVisibleListRaw(char* namePtr, int nameLen, byte* keyTokenPtr, int keyTokenLen)
    {
      if (this._aptcaVisibleAssemblies == null)
        return false;
      string str = new string(namePtr, 0, nameLen);
      byte[] publicKeyToken = new byte[keyTokenLen];
      for (int index = 0; index < publicKeyToken.Length; ++index)
        publicKeyToken[index] = keyTokenPtr[index];
      AssemblyName assemblyName = new AssemblyName();
      assemblyName.Name = str;
      assemblyName.SetPublicKeyToken(publicKeyToken);
      try
      {
        return Array.BinarySearch((Array) this._aptcaVisibleAssemblies, (object) assemblyName, (IComparer) new AppDomain.CAPTCASearcher()) >= 0;
      }
      catch (InvalidOperationException ex)
      {
        return false;
      }
    }

    [SecurityCritical]
    private void SetupDomain(bool allowRedirects, string path, string configFile, string[] propertyNames, string[] propertyValues)
    {
      lock (this)
      {
        if (this._FusionStore != null)
          return;
        AppDomainSetup local_2 = new AppDomainSetup();
        local_2.SetupDefaults(RuntimeEnvironment.GetModuleFileName(), true);
        if (path != null)
          local_2.Value[0] = path;
        if (configFile != null)
          local_2.Value[1] = configFile;
        if (!allowRedirects)
          local_2.DisallowBindingRedirects = true;
        if (propertyNames != null)
        {
          for (int local_3 = 0; local_3 < propertyNames.Length; ++local_3)
          {
            if (string.Equals(propertyNames[local_3], "PARTIAL_TRUST_VISIBLE_ASSEMBLIES", StringComparison.Ordinal) && propertyValues[local_3] != null)
            {
              if (propertyValues[local_3].Length > 0)
                local_2.PartialTrustVisibleAssemblies = propertyValues[local_3].Split(';');
              else
                local_2.PartialTrustVisibleAssemblies = new string[0];
            }
          }
        }
        this.PartialTrustVisibleAssemblies = local_2.PartialTrustVisibleAssemblies;
        this.SetupFusionStore(local_2, (AppDomainSetup) null);
      }
    }

    [SecurityCritical]
    private void SetupLoaderOptimization(LoaderOptimization policy)
    {
      if (policy == LoaderOptimization.NotSpecified)
        return;
      this.FusionStore.LoaderOptimization = policy;
      this.UpdateLoaderOptimization(this.FusionStore.LoaderOptimization);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal IntPtr GetFusionContext();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal IntPtr GetSecurityDescriptor();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern AppDomain nCreateDomain(string friendlyName, AppDomainSetup setup, Evidence providedSecurityInfo, Evidence creatorsSecurityInfo, IntPtr parentSecurityDescriptor);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern ObjRef nCreateInstance(string friendlyName, AppDomainSetup setup, Evidence providedSecurityInfo, Evidence creatorsSecurityInfo, IntPtr parentSecurityDescriptor);

    [SecurityCritical]
    private void SetupDomainSecurity(Evidence appDomainEvidence, IntPtr creatorsSecurityDescriptor, bool publishAppDomain)
    {
      Evidence o = appDomainEvidence;
      AppDomain.SetupDomainSecurity(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<Evidence>(ref o), creatorsSecurityDescriptor, publishAppDomain);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void SetupDomainSecurity(AppDomainHandle appDomain, ObjectHandleOnStack appDomainEvidence, IntPtr creatorsSecurityDescriptor, [MarshalAs(UnmanagedType.Bool)] bool publishAppDomain);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void nSetupFriendlyName(string friendlyName);

    [MethodImpl(MethodImplOptions.InternalCall)]
    private void nSetDisableInterfaceCache();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal void UpdateLoaderOptimization(LoaderOptimization optimization);

    /// <summary>确定指定目录路径为要进行影像复制的程序集的位置。</summary>
    /// <param name="path">目录名列表，各名称用分号隔开。</param>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
    /// </PermissionSet>
    [SecurityCritical]
    [Obsolete("AppDomain.SetShadowCopyPath has been deprecated. Please investigate the use of AppDomainSetup.ShadowCopyDirectories instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    public void SetShadowCopyPath(string path)
    {
      this.InternalSetShadowCopyPath(path);
    }

    /// <summary>打开影像复制功能。</summary>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
    /// </PermissionSet>
    [SecurityCritical]
    [Obsolete("AppDomain.SetShadowCopyFiles has been deprecated. Please investigate the use of AppDomainSetup.ShadowCopyFiles instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    public void SetShadowCopyFiles()
    {
      this.InternalSetShadowCopyFiles();
    }

    /// <summary>建立指定的目录路径，作为存储和访问动态生成的文件的子目录的基目录。</summary>
    /// <param name="path">完全限定路径，是存储动态程序集的子目录的基目录。</param>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
    /// </PermissionSet>
    [SecurityCritical]
    [Obsolete("AppDomain.SetDynamicBase has been deprecated. Please investigate the use of AppDomainSetup.DynamicBase instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    public void SetDynamicBase(string path)
    {
      this.InternalSetDynamicBase(path);
    }

    [SecurityCritical]
    internal void InternalSetShadowCopyPath(string path)
    {
      if (path != null)
        AppDomainSetup.UpdateContextProperty(this.GetFusionContext(), AppDomainSetup.ShadowCopyDirectoriesKey, (object) path);
      this.FusionStore.ShadowCopyDirectories = path;
    }

    [SecurityCritical]
    internal void InternalSetShadowCopyFiles()
    {
      AppDomainSetup.UpdateContextProperty(this.GetFusionContext(), AppDomainSetup.ShadowCopyFilesKey, (object) "true");
      this.FusionStore.ShadowCopyFiles = "true";
    }

    [SecurityCritical]
    internal void InternalSetCachePath(string path)
    {
      this.FusionStore.CachePath = path;
      if (this.FusionStore.Value[9] == null)
        return;
      AppDomainSetup.UpdateContextProperty(this.GetFusionContext(), AppDomainSetup.CachePathKey, (object) this.FusionStore.Value[9]);
    }

    [SecurityCritical]
    internal void InternalSetPrivateBinPath(string path)
    {
      AppDomainSetup.UpdateContextProperty(this.GetFusionContext(), AppDomainSetup.PrivateBinPathKey, (object) path);
      this.FusionStore.PrivateBinPath = path;
    }

    [SecurityCritical]
    internal void InternalSetDynamicBase(string path)
    {
      this.FusionStore.DynamicBase = path;
      if (this.FusionStore.Value[2] == null)
        return;
      AppDomainSetup.UpdateContextProperty(this.GetFusionContext(), AppDomainSetup.DynamicBaseKey, (object) this.FusionStore.Value[2]);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal string IsStringInterned(string str);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal string GetOrInternString(string str);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetGrantSet(AppDomainHandle domain, ObjectHandleOnStack retGrantSet);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetIsLegacyCasPolicyEnabled(AppDomainHandle domain);

    [SecuritySafeCritical]
    internal PermissionSet GetHomogenousGrantSet(Evidence evidence)
    {
      if (this._IsFastFullTrustDomain)
        return new PermissionSet(PermissionState.Unrestricted);
      if (evidence.GetDelayEvaluatedHostEvidence<StrongName>() != null)
      {
        foreach (StrongName fullTrustAssembly in (IEnumerable<StrongName>) this.ApplicationTrust.FullTrustAssemblies)
        {
          StrongNameMembershipCondition membershipCondition = new StrongNameMembershipCondition(fullTrustAssembly.PublicKey, fullTrustAssembly.Name, fullTrustAssembly.Version);
          object obj = (object) null;
          Evidence evidence1 = evidence;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          object& usedEvidence = @obj;
          if (((IReportMatchMembershipCondition) membershipCondition).Check(evidence1, usedEvidence))
          {
            IDelayEvaluatedEvidence evaluatedEvidence = obj as IDelayEvaluatedEvidence;
            if (obj != null)
              evaluatedEvidence.MarkUsed();
            return new PermissionSet(PermissionState.Unrestricted);
          }
        }
      }
      return this.ApplicationTrust.DefaultGrantSet.PermissionSet.Copy();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void nChangeSecurityPolicy();

    [SecurityCritical]
    [ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.MayFail)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void nUnload(int domainInternal);

    /// <summary>创建指定类型的新实例。形参指定定义类型的程序集以及类型的名称。</summary>
    /// <returns>
    /// <paramref name="typeName" /> 所指定对象的实例。</returns>
    /// <param name="assemblyName">程序集的显示名称。请参阅<see cref="P:System.Reflection.Assembly.FullName" />。</param>
    /// <param name="typeName">
    /// <see cref="P:System.Type.FullName" /> 属性返回的所请求类型的完全限定名称，包含命名空间而不是程序集。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyName" /> 或 <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的公共构造函数。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typename" /> 中未找到 <paramref name="assemblyName" />。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyName" /> 未找到。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyName" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyName" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public object CreateInstanceAndUnwrap(string assemblyName, string typeName)
    {
      ObjectHandle instance = this.CreateInstance(assemblyName, typeName);
      if (instance == null)
        return (object) null;
      return instance.Unwrap();
    }

    /// <summary>创建指定类型的新实例。形参指定定义类型的程序集、类型的名称和激活特性的数组。</summary>
    /// <returns>
    /// <paramref name="typeName" /> 所指定对象的实例。</returns>
    /// <param name="assemblyName">程序集的显示名称。请参阅<see cref="P:System.Reflection.Assembly.FullName" />。</param>
    /// <param name="typeName">
    /// <see cref="P:System.Type.FullName" /> 属性返回的所请求类型的完全限定名称，包含命名空间而不是程序集。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。通常，为包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组，该对象指定激活远程对象所需的 URL。此参数与客户端激活的对象相关。客户端激活是一项传统技术，保留用于向后兼容，但不建议用于新的开发。应改用 Windows Communication Foundation 来开发分布式应用程序。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyName" /> 或 <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的公共构造函数。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typename" /> 中未找到 <paramref name="assemblyName" />。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyName" /> 未找到。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.NotSupportedException">调用方不能提供一个对象，不会继承从激活特性 <see cref="T:System.MarshalByRefObject" />。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyName" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyName" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public object CreateInstanceAndUnwrap(string assemblyName, string typeName, object[] activationAttributes)
    {
      ObjectHandle instance = this.CreateInstance(assemblyName, typeName, activationAttributes);
      if (instance == null)
        return (object) null;
      return instance.Unwrap();
    }

    /// <summary>创建指定类型的新实例。形参指定类型的名称以及查找和创建该类型的方式。</summary>
    /// <returns>
    /// <paramref name="typeName" /> 所指定对象的实例。</returns>
    /// <param name="assemblyName">程序集的显示名称。请参阅<see cref="P:System.Reflection.Assembly.FullName" />。</param>
    /// <param name="typeName">
    /// <see cref="P:System.Type.FullName" /> 属性返回的所请求类型的完全限定名称，包含命名空间而不是程序集。</param>
    /// <param name="ignoreCase">一个布尔值，指示是否执行区分大小写的搜索。</param>
    /// <param name="bindingAttr">影响 <paramref name="typeName" /> 构造函数搜索的零个或多个位标志的组合。如果 <paramref name="bindingAttr" /> 为零，则对公共构造函数进行区分大小写的搜索。</param>
    /// <param name="binder">一个对象，它使用反射启用绑定、参数类型的强制、成员的调用和 <see cref="T:System.Reflection.MemberInfo" /> 对象的检索。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="args">要传递给构造函数的实参。此实参数组必须在数量、顺序和类型方面与要调用的构造函数的形参匹配。如果默认的构造函数是首选构造函数，则 <paramref name="args" /> 必须为空数组或 Null。</param>
    /// <param name="culture">用于控制类型强制的特定于区域性的对象。如果 <paramref name="culture" /> 为 null，则使用当前线程的 CultureInfo。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。通常，为包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组，该对象指定激活远程对象所需的 URL。此参数与客户端激活的对象相关。客户端激活是一项传统技术，保留用于向后兼容，但不建议用于新的开发。应改用 Windows Communication Foundation 来开发分布式应用程序。</param>
    /// <param name="securityAttributes">用于授权创建 <paramref name="typeName" /> 的信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyName" /> 或 <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的构造函数。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typename" /> 中未找到 <paramref name="assemblyName" />。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyName" /> 未找到。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.NotSupportedException">调用方不能提供一个对象，不会继承从激活特性 <see cref="T:System.MarshalByRefObject" />。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyName" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyName" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstanceAndUnwrap which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public object CreateInstanceAndUnwrap(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
    {
      ObjectHandle instance = this.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
      if (instance == null)
        return (object) null;
      return instance.Unwrap();
    }

    /// <summary>创建在指定的程序集中定义的指定类型的新实例，指定是否忽略类型名称的大小写，并指定绑定特性和用于选择要创建的类型的联编程序、构造函数的参数、区域性以及激活特性。</summary>
    /// <returns>
    /// <paramref name="typeName" /> 所指定对象的实例。</returns>
    /// <param name="assemblyName">程序集的显示名称。请参阅<see cref="P:System.Reflection.Assembly.FullName" />。</param>
    /// <param name="typeName">
    /// <see cref="P:System.Type.FullName" /> 属性返回的所请求类型的完全限定名称，包含命名空间而不是程序集。</param>
    /// <param name="ignoreCase">一个布尔值，指示是否执行区分大小写的搜索。</param>
    /// <param name="bindingAttr">影响 <paramref name="typeName" /> 构造函数搜索的零个或多个位标志的组合。如果 <paramref name="bindingAttr" /> 为零，则对公共构造函数进行区分大小写的搜索。</param>
    /// <param name="binder">一个对象，它使用反射启用绑定、参数类型的强制、成员的调用和 <see cref="T:System.Reflection.MemberInfo" /> 对象的检索。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="args">要传递给构造函数的实参。此实参数组必须在数量、顺序和类型方面与要调用的构造函数的形参匹配。如果默认的构造函数是首选构造函数，则 <paramref name="args" /> 必须为空数组或 Null。</param>
    /// <param name="culture">用于控制类型强制的特定于区域性的对象。如果 <paramref name="culture" /> 为 null，则使用当前线程的 CultureInfo。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。通常是包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组。指定激活远程对象所需的 URL。此参数与客户端激活的对象相关。客户端激活是一项传统技术，保留用于向后兼容，但不建议用于新的开发。应改用 Windows Communication Foundation 来开发分布式应用程序。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyName" /> 或 <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的构造函数。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typename" /> 中未找到 <paramref name="assemblyName" />。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyName" /> 未找到。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.NotSupportedException">调用方不能提供一个对象，不会继承从激活特性 <see cref="T:System.MarshalByRefObject" />。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyName" /> 不是有效的程序集。- 或 -<paramref name="assemblyName" /> 已使用比当前加载的版本更高版本的公共语言运行时编译。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    public object CreateInstanceAndUnwrap(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
    {
      ObjectHandle instance = this.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes);
      if (instance == null)
        return (object) null;
      return instance.Unwrap();
    }

    /// <summary>创建在指定程序集文件中定义的指定类型的新实例。</summary>
    /// <returns>请求的对象，或者如果找不到 <paramref name="typeName" /> 则返回 null。</returns>
    /// <param name="assemblyName">定义所请求类型的程序集的文件名和路径。</param>
    /// <param name="typeName">
    /// <see cref="P:System.Type.FullName" /> 属性返回的所请求类型的完全限定名称，包含命名空间而不是程序集。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyName" /> 为 null。- 或 - <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyName" /> 未找到。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typeName" /> 中未找到 <paramref name="assemblyName" />。</exception>
    /// <exception cref="T:System.MissingMethodException">发现没有无参数公共构造函数。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有足够的权限来调用此构造函数。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyName" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyName" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public object CreateInstanceFromAndUnwrap(string assemblyName, string typeName)
    {
      ObjectHandle instanceFrom = this.CreateInstanceFrom(assemblyName, typeName);
      if (instanceFrom == null)
        return (object) null;
      return instanceFrom.Unwrap();
    }

    /// <summary>创建在指定程序集文件中定义的指定类型的新实例。</summary>
    /// <returns>请求的对象，或者如果找不到 <paramref name="typeName" /> 则返回 null。</returns>
    /// <param name="assemblyName">定义所请求类型的程序集的文件名和路径。</param>
    /// <param name="typeName">所请求类型的完全限定名，包括命名空间而不是程序集（请参见 <see cref="P:System.Type.FullName" /> 属性）。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。通常，为包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组，该对象指定激活远程对象所需的 URL。此参数与客户端激活的对象相关。客户端激活是一项传统技术，保留用于向后兼容，但不建议用于新的开发。应改用 Windows Communication Foundation 来开发分布式应用程序。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyName" /> 为 null。- 或 - <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">调用方不能提供一个对象，不会继承从激活特性 <see cref="T:System.MarshalByRefObject" />。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyName" /> 未找到。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typeName" /> 中未找到 <paramref name="assemblyName" />。</exception>
    /// <exception cref="T:System.MissingMethodException">发现没有无参数公共构造函数。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有足够的权限来调用此构造函数。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyName" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyName" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public object CreateInstanceFromAndUnwrap(string assemblyName, string typeName, object[] activationAttributes)
    {
      ObjectHandle instanceFrom = this.CreateInstanceFrom(assemblyName, typeName, activationAttributes);
      if (instanceFrom == null)
        return (object) null;
      return instanceFrom.Unwrap();
    }

    /// <summary>创建在指定程序集文件中定义的指定类型的新实例。</summary>
    /// <returns>请求的对象，或者如果找不到 <paramref name="typeName" /> 则返回 null。</returns>
    /// <param name="assemblyName">定义所请求类型的程序集的文件名和路径。</param>
    /// <param name="typeName">
    /// <see cref="P:System.Type.FullName" /> 属性返回的所请求类型的完全限定名称，包含命名空间而不是程序集。</param>
    /// <param name="ignoreCase">一个布尔值，指示是否执行区分大小写的搜索。</param>
    /// <param name="bindingAttr">影响 <paramref name="typeName" /> 构造函数搜索的零个或多个位标志的组合。如果 <paramref name="bindingAttr" /> 为零，则对公共构造函数进行区分大小写的搜索。</param>
    /// <param name="binder">一个对象，它启用绑定、对参数类型的强制、对成员的调用，以及通过反射对 <see cref="T:System.Reflection.MemberInfo" /> 对象的检索。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="args">要传递给构造函数的实参。此实参数组必须在数量、顺序和类型方面与要调用的构造函数的形参匹配。如果默认的构造函数是首选构造函数，则 <paramref name="args" /> 必须为空数组或 Null。</param>
    /// <param name="culture">区域性特定的信息，这些信息控制将 <paramref name="args" /> 强制转换为 <paramref name="typeName" /> 构造函数所声明的正式类型。如果 <paramref name="culture" /> 为 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。通常，为包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组，该对象指定激活远程对象所需的 URL。此参数与客户端激活的对象相关。客户端激活是一项传统技术，保留用于向后兼容，但不建议用于新的开发。应改用 Windows Communication Foundation 来开发分布式应用程序。</param>
    /// <param name="securityAttributes">用于授权创建 <paramref name="typeName" /> 的信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyName" /> 为 null。- 或 - <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">调用方不能提供一个对象，不会继承从激活特性 <see cref="T:System.MarshalByRefObject" />。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyName" /> 未找到。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typeName" /> 中未找到 <paramref name="assemblyName" />。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的公共构造函数。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有足够的权限来调用此构造函数。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyName" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyName" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstanceFromAndUnwrap which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public object CreateInstanceFromAndUnwrap(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
    {
      ObjectHandle instanceFrom = this.CreateInstanceFrom(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
      if (instanceFrom == null)
        return (object) null;
      return instanceFrom.Unwrap();
    }

    /// <summary>创建在指定的程序集文件中定义的指定类型的新实例，指定是否忽略类型名称的大小写，并指定绑定特性和用于选择要创建的类型的联编程序、构造函数的参数、区域性以及激活特性。</summary>
    /// <returns>请求的对象，或者如果找不到 <paramref name="typeName" /> 则返回 null。</returns>
    /// <param name="assemblyFile">定义所请求类型的程序集的文件名和路径。</param>
    /// <param name="typeName">
    /// <see cref="P:System.Type.FullName" /> 属性返回的所请求类型的完全限定名称，包含命名空间而不是程序集。</param>
    /// <param name="ignoreCase">一个布尔值，指示是否执行区分大小写的搜索。</param>
    /// <param name="bindingAttr">影响 <paramref name="typeName" /> 构造函数搜索的零个或多个位标志的组合。如果 <paramref name="bindingAttr" /> 为零，则对公共构造函数进行区分大小写的搜索。</param>
    /// <param name="binder">一个对象，它启用绑定、对参数类型的强制、对成员的调用，以及通过反射对 <see cref="T:System.Reflection.MemberInfo" /> 对象的检索。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="args">要传递给构造函数的实参。此实参数组必须在数量、顺序和类型方面与要调用的构造函数的形参匹配。如果默认的构造函数是首选构造函数，则 <paramref name="args" /> 必须为空数组或 Null。</param>
    /// <param name="culture">区域性特定的信息，这些信息控制将 <paramref name="args" /> 强制转换为 <paramref name="typeName" /> 构造函数所声明的正式类型。如果 <paramref name="culture" /> 为 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。通常，为包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组，该对象指定激活远程对象所需的 URL。此参数与客户端激活的对象相关。客户端激活是一项传统技术，保留用于向后兼容，但不建议用于新的开发。应改用 Windows Communication Foundation 来开发分布式应用程序。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyName" /> 为 null。- 或 - <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">调用方不能提供一个对象，不会继承从激活特性 <see cref="T:System.MarshalByRefObject" />。</exception>
    /// <exception cref="T:System.AppDomainUnloadedException">该操作尝试对已卸载的应用程序域中。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyName" /> 未找到。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typeName" /> 中未找到 <paramref name="assemblyName" />。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的公共构造函数。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有足够的权限来调用此构造函数。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyName" /> 不是有效的程序集。- 或 -<paramref name="assemblyName" /> 使用更高版本的公共语言运行时编译的版本，是当前加载。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    public object CreateInstanceFromAndUnwrap(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
    {
      ObjectHandle instanceFrom = this.CreateInstanceFrom(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes);
      if (instanceFrom == null)
        return (object) null;
      return instanceFrom.Unwrap();
    }

    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal int GetId();

    /// <summary>返回一个值，指示应用程序域是否是进程的默认应用程序域。</summary>
    /// <returns>如果当前 <see cref="T:System.AppDomain" /> 对象表示进程的默认应用程序域，则为 true；否则为 false。</returns>
    /// <filterpriority>1</filterpriority>
    public bool IsDefaultAppDomain()
    {
      return this.GetId() == 1;
    }

    private static AppDomainSetup InternalCreateDomainSetup(string imageLocation)
    {
      int num = imageLocation.LastIndexOf('\\');
      AppDomainSetup appDomainSetup = new AppDomainSetup();
      appDomainSetup.ApplicationBase = imageLocation.Substring(0, num + 1);
      StringBuilder stringBuilder = new StringBuilder(imageLocation.Substring(num + 1));
      stringBuilder.Append(AppDomainSetup.ConfigurationExtension);
      string @string = stringBuilder.ToString();
      appDomainSetup.ConfigurationFile = @string;
      return appDomainSetup;
    }

    private static AppDomain InternalCreateDomain(string imageLocation)
    {
      return AppDomain.CreateDomain("Validator", (Evidence) null, AppDomain.InternalCreateDomainSetup(imageLocation));
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void nEnableMonitoring();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool nMonitoringIsEnabled();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private long nGetTotalProcessorTime();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private long nGetTotalAllocatedMemorySize();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private long nGetLastSurvivedMemorySize();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern long nGetLastSurvivedProcessMemorySize();

    [SecurityCritical]
    private void InternalSetDomainContext(string imageLocation)
    {
      this.SetupFusionStore(AppDomain.InternalCreateDomainSetup(imageLocation), (AppDomainSetup) null);
    }

    /// <summary>获取当前实例的类型。</summary>
    /// <returns>当前实例的类型。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public new Type GetType()
    {
      return base.GetType();
    }

    void _AppDomain.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _AppDomain.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _AppDomain.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _AppDomain.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }

    [System.Flags]
    private enum APPX_FLAGS
    {
      APPX_FLAGS_INITIALIZED = 1,
      APPX_FLAGS_APPX_MODEL = 2,
      APPX_FLAGS_APPX_DESIGN_MODE = 4,
      APPX_FLAGS_APPX_NGEN = 8,
      APPX_FLAGS_APPX_MASK = APPX_FLAGS_APPX_NGEN | APPX_FLAGS_APPX_DESIGN_MODE | APPX_FLAGS_APPX_MODEL,
      APPX_FLAGS_API_CHECK = 16,
    }

    private class NamespaceResolverForIntrospection
    {
      private IEnumerable<string> _packageGraphFilePaths;

      public NamespaceResolverForIntrospection(IEnumerable<string> packageGraphFilePaths)
      {
        this._packageGraphFilePaths = packageGraphFilePaths;
      }

      [SecurityCritical]
      public void ResolveNamespace(object sender, NamespaceResolveEventArgs args)
      {
        foreach (string assemblyFile in WindowsRuntimeMetadata.ResolveNamespace(args.NamespaceName, (string) null, this._packageGraphFilePaths))
          args.ResolvedAssemblies.Add(Assembly.ReflectionOnlyLoadFrom(assemblyFile));
      }
    }

    [Serializable]
    private class EvidenceCollection
    {
      public Evidence ProvidedSecurityInfo;
      public Evidence CreatorsSecurityInfo;
    }

    private class CAPTCASearcher : IComparer
    {
      int IComparer.Compare(object lhs, object rhs)
      {
        AssemblyName assemblyName1 = new AssemblyName((string) lhs);
        AssemblyName assemblyName2 = (AssemblyName) rhs;
        int num1 = string.Compare(assemblyName1.Name, assemblyName2.Name, StringComparison.OrdinalIgnoreCase);
        if (num1 != 0)
          return num1;
        byte[] publicKeyToken1 = assemblyName1.GetPublicKeyToken();
        byte[] publicKeyToken2 = assemblyName2.GetPublicKeyToken();
        if (publicKeyToken1 == null)
          return -1;
        if (publicKeyToken2 == null)
          return 1;
        if (publicKeyToken1.Length < publicKeyToken2.Length)
          return -1;
        if (publicKeyToken1.Length > publicKeyToken2.Length)
          return 1;
        for (int index = 0; index < publicKeyToken1.Length; ++index)
        {
          byte num2 = publicKeyToken1[index];
          byte num3 = publicKeyToken2[index];
          if ((int) num2 < (int) num3)
            return -1;
          if ((int) num2 > (int) num3)
            return 1;
        }
        return 0;
      }
    }
  }
}
