// Decompiled with JetBrains decompiler
// Type: System.IO.IsolatedStorage.IsolatedStorage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System.IO.IsolatedStorage
{
  /// <summary>表示所有独立存储实现都必须从中派生的抽象基类。</summary>
  [ComVisible(true)]
  public abstract class IsolatedStorage : MarshalByRefObject
  {
    internal const IsolatedStorageScope c_Assembly = IsolatedStorageScope.User | IsolatedStorageScope.Assembly;
    internal const IsolatedStorageScope c_Domain = IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly;
    internal const IsolatedStorageScope c_AssemblyRoaming = IsolatedStorageScope.User | IsolatedStorageScope.Assembly | IsolatedStorageScope.Roaming;
    internal const IsolatedStorageScope c_DomainRoaming = IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Roaming;
    internal const IsolatedStorageScope c_MachineAssembly = IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine;
    internal const IsolatedStorageScope c_MachineDomain = IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine;
    internal const IsolatedStorageScope c_AppUser = IsolatedStorageScope.User | IsolatedStorageScope.Application;
    internal const IsolatedStorageScope c_AppMachine = IsolatedStorageScope.Machine | IsolatedStorageScope.Application;
    internal const IsolatedStorageScope c_AppUserRoaming = IsolatedStorageScope.User | IsolatedStorageScope.Roaming | IsolatedStorageScope.Application;
    private const string s_Publisher = "Publisher";
    private const string s_StrongName = "StrongName";
    private const string s_Site = "Site";
    private const string s_Url = "Url";
    private const string s_Zone = "Zone";
    private ulong m_Quota;
    private bool m_ValidQuota;
    private object m_DomainIdentity;
    private object m_AssemIdentity;
    private object m_AppIdentity;
    private string m_DomainName;
    private string m_AssemName;
    private string m_AppName;
    private IsolatedStorageScope m_Scope;
    private static volatile IsolatedStorageFilePermission s_PermDomain;
    private static volatile IsolatedStorageFilePermission s_PermMachineDomain;
    private static volatile IsolatedStorageFilePermission s_PermDomainRoaming;
    private static volatile IsolatedStorageFilePermission s_PermAssem;
    private static volatile IsolatedStorageFilePermission s_PermMachineAssem;
    private static volatile IsolatedStorageFilePermission s_PermAssemRoaming;
    private static volatile IsolatedStorageFilePermission s_PermAppUser;
    private static volatile IsolatedStorageFilePermission s_PermAppMachine;
    private static volatile IsolatedStorageFilePermission s_PermAppUserRoaming;
    private static volatile SecurityPermission s_PermControlEvidence;
    private static volatile PermissionSet s_PermUnrestricted;

    /// <summary>获取可在目录字符串中使用的反斜杠字符。当在派生类中重写后，可能会返回另一个字符。</summary>
    /// <returns>默认实现返回“\”（反斜杠）字符。</returns>
    protected virtual char SeparatorExternal
    {
      get
      {
        return '\\';
      }
    }

    /// <summary>获取可在目录字符串中使用的句点字符。当在派生类中重写后，可能会返回另一个字符。</summary>
    /// <returns>默认实现返回“.”（句点）字符。</returns>
    protected virtual char SeparatorInternal
    {
      get
      {
        return '.';
      }
    }

    /// <summary>获取一个值，该值表示独立存储的最大可用空间数量。当在派生类中重写时，该值可以采用不同的度量单位。</summary>
    /// <returns>独立存储空间的最大数量（以字节为单位）。派生类可以返回不同单位的值。</returns>
    /// <exception cref="T:System.InvalidOperationException">尚未定义配额。</exception>
    [CLSCompliant(false)]
    [Obsolete("IsolatedStorage.MaximumSize has been deprecated because it is not CLS Compliant.  To get the maximum size use IsolatedStorage.Quota")]
    public virtual ulong MaximumSize
    {
      get
      {
        if (this.m_ValidQuota)
          return this.m_Quota;
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_QuotaIsUndefined", (object) "MaximumSize"));
      }
    }

    /// <summary>获取表示独立存储当前大小的值。</summary>
    /// <returns>该独立存储范围内当前使用的存储单元的数目。</returns>
    /// <exception cref="T:System.InvalidOperationException">独立存储区的当前大小未定义。</exception>
    [CLSCompliant(false)]
    [Obsolete("IsolatedStorage.CurrentSize has been deprecated because it is not CLS Compliant.  To get the current size use IsolatedStorage.UsedSize")]
    public virtual ulong CurrentSize
    {
      get
      {
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_CurrentSizeUndefined", (object) "CurrentSize"));
      }
    }

    /// <summary>在派生类中重写时，获取一个值，该值表示用于独立存储的空间量。</summary>
    /// <returns>独立存储的已用空间量，以字节为单位。</returns>
    /// <exception cref="T:System.InvalidOperationException">执行需要访问 <see cref="P:System.IO.IsolatedStorage.IsolatedStorage.UsedSize" /> 的操作，但是未为此存储区定义该属性。通过使用枚举获得存储没有定义完善的 <see cref="P:System.IO.IsolatedStorage.IsolatedStorage.UsedSize" /> 属性，因为使用部分证据来打开存储区。</exception>
    [ComVisible(false)]
    public virtual long UsedSize
    {
      get
      {
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_CurrentSizeUndefined", (object) "UsedSize"));
      }
    }

    /// <summary>在派生类中重写时，获取一个值，该值表示可用于独立存储的最大空间量。</summary>
    /// <returns>独立存储空间的限制，以字节为单位。</returns>
    /// <exception cref="T:System.InvalidOperationException">执行需要访问 <see cref="P:System.IO.IsolatedStorage.IsolatedStorage.Quota" /> 的操作，但是未为此存储区定义该属性。通过使用枚举获得存储没有定义完善的 <see cref="P:System.IO.IsolatedStorage.IsolatedStorage.Quota" /> 属性，因为使用部分证据来打开存储区。</exception>
    [ComVisible(false)]
    public virtual long Quota
    {
      get
      {
        if (this.m_ValidQuota)
          return (long) this.m_Quota;
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_QuotaIsUndefined", (object) "Quota"));
      }
      internal set
      {
        this.m_Quota = (ulong) value;
        this.m_ValidQuota = true;
      }
    }

    /// <summary>在派生类中重写时，获取独立存储的可用空间（以字节为单位）。</summary>
    /// <returns>独立存储的可用空间，以字节为单位。</returns>
    /// <exception cref="T:System.InvalidOperationException">执行需要访问 <see cref="P:System.IO.IsolatedStorage.IsolatedStorage.AvailableFreeSpace" /> 的操作，但是未为此存储区定义该属性。通过使用枚举获得存储没有定义完善的 <see cref="P:System.IO.IsolatedStorage.IsolatedStorage.AvailableFreeSpace" /> 属性，因为使用部分证据来打开存储区。</exception>
    [ComVisible(false)]
    public virtual long AvailableFreeSpace
    {
      get
      {
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_QuotaIsUndefined", (object) "AvailableFreeSpace"));
      }
    }

    /// <summary>获取用于确定独立存储范围的域标识。</summary>
    /// <returns>表示 <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" /> 标识的 <see cref="T:System.Object" />。</returns>
    /// <exception cref="T:System.Security.SecurityException">该代码缺少访问此对象所需的 <see cref="T:System.Security.Permissions.SecurityPermission" />。这些权限由运行时基于安全策略授予。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> 对象没有被域 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> 隔离。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPolicy" />
    /// </PermissionSet>
    public object DomainIdentity
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)] get
      {
        if (this.IsDomain())
          return this.m_DomainIdentity;
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_DomainUndefined"));
      }
    }

    /// <summary>获取用于确定独立存储范围的应用程序标识。</summary>
    /// <returns>表示 <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" /> 标识的 <see cref="T:System.Object" />。</returns>
    /// <exception cref="T:System.Security.SecurityException">该代码缺少访问此对象所需的 <see cref="T:System.Security.Permissions.SecurityPermission" />。这些权限由运行时基于安全策略授予。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> 对象没有被应用程序 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> 隔离。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPolicy" />
    /// </PermissionSet>
    [ComVisible(false)]
    public object ApplicationIdentity
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)] get
      {
        if (this.IsApp())
          return this.m_AppIdentity;
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_ApplicationUndefined"));
      }
    }

    /// <summary>获取用于确定独立存储范围的程序集标识。</summary>
    /// <returns>表示 <see cref="T:System.Reflection.Assembly" /> 标识的 <see cref="T:System.Object" />。</returns>
    /// <exception cref="T:System.Security.SecurityException">该代码缺少访问此对象所需的 <see cref="T:System.Security.Permissions.SecurityPermission" />。</exception>
    /// <exception cref="T:System.InvalidOperationException">未定义程序集。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPolicy" />
    /// </PermissionSet>
    public object AssemblyIdentity
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)] get
      {
        if (this.IsAssembly())
          return this.m_AssemIdentity;
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_AssemblyUndefined"));
      }
    }

    /// <summary>获取指定用于隔离存储区的范围的 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> 枚举值。</summary>
    /// <returns>指定用于隔离存储区的范围的 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> 值的按位组合。</returns>
    public IsolatedStorageScope Scope
    {
      get
      {
        return this.m_Scope;
      }
    }

    internal string DomainName
    {
      get
      {
        if (this.IsDomain())
          return this.m_DomainName;
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_DomainUndefined"));
      }
    }

    internal string AssemName
    {
      get
      {
        if (this.IsAssembly())
          return this.m_AssemName;
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_AssemblyUndefined"));
      }
    }

    internal string AppName
    {
      get
      {
        if (this.IsApp())
          return this.m_AppName;
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_ApplicationUndefined"));
      }
    }

    internal static bool IsRoaming(IsolatedStorageScope scope)
    {
      return (uint) (scope & IsolatedStorageScope.Roaming) > 0U;
    }

    internal bool IsRoaming()
    {
      return (uint) (this.m_Scope & IsolatedStorageScope.Roaming) > 0U;
    }

    internal static bool IsDomain(IsolatedStorageScope scope)
    {
      return (uint) (scope & IsolatedStorageScope.Domain) > 0U;
    }

    internal bool IsDomain()
    {
      return (uint) (this.m_Scope & IsolatedStorageScope.Domain) > 0U;
    }

    internal static bool IsMachine(IsolatedStorageScope scope)
    {
      return (uint) (scope & IsolatedStorageScope.Machine) > 0U;
    }

    internal bool IsAssembly()
    {
      return (uint) (this.m_Scope & IsolatedStorageScope.Assembly) > 0U;
    }

    internal static bool IsApp(IsolatedStorageScope scope)
    {
      return (uint) (scope & IsolatedStorageScope.Application) > 0U;
    }

    internal bool IsApp()
    {
      return (uint) (this.m_Scope & IsolatedStorageScope.Application) > 0U;
    }

    private string GetNameFromID(string typeID, string instanceID)
    {
      return typeID + this.SeparatorInternal.ToString() + instanceID;
    }

    private static string GetPredefinedTypeName(object o)
    {
      if (o is Publisher)
        return "Publisher";
      if (o is StrongName)
        return "StrongName";
      if (o is Url)
        return "Url";
      if (o is Site)
        return "Site";
      if (o is Zone)
        return "Zone";
      return (string) null;
    }

    internal static string GetHash(Stream s)
    {
      using (SHA1 shA1 = (SHA1) new SHA1CryptoServiceProvider())
        return Path.ToBase32StringSuitableForDirName(shA1.ComputeHash(s));
    }

    private static bool IsValidName(string s)
    {
      for (int index = 0; index < s.Length; ++index)
      {
        if (!char.IsLetter(s[index]) && !char.IsDigit(s[index]))
          return false;
      }
      return true;
    }

    private static SecurityPermission GetControlEvidencePermission()
    {
      if (System.IO.IsolatedStorage.IsolatedStorage.s_PermControlEvidence == null)
        System.IO.IsolatedStorage.IsolatedStorage.s_PermControlEvidence = new SecurityPermission(SecurityPermissionFlag.ControlEvidence);
      return System.IO.IsolatedStorage.IsolatedStorage.s_PermControlEvidence;
    }

    private static PermissionSet GetUnrestricted()
    {
      if (System.IO.IsolatedStorage.IsolatedStorage.s_PermUnrestricted == null)
        System.IO.IsolatedStorage.IsolatedStorage.s_PermUnrestricted = new PermissionSet(PermissionState.Unrestricted);
      return System.IO.IsolatedStorage.IsolatedStorage.s_PermUnrestricted;
    }

    /// <summary>在派生类中重写时，将提示用户批准独立存储的更大配额大小（以字节为单位）。</summary>
    /// <returns>所有情况下均为 false。</returns>
    /// <param name="newQuotaSize">请求的新配额大小（以字节为单位），有待用户批准。</param>
    [ComVisible(false)]
    public virtual bool IncreaseQuotaTo(long newQuotaSize)
    {
      return false;
    }

    [SecurityCritical]
    internal MemoryStream GetIdentityStream(IsolatedStorageScope scope)
    {
      System.IO.IsolatedStorage.IsolatedStorage.GetUnrestricted().Assert();
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      MemoryStream memoryStream = new MemoryStream();
      object graph = !System.IO.IsolatedStorage.IsolatedStorage.IsApp(scope) ? (!System.IO.IsolatedStorage.IsolatedStorage.IsDomain(scope) ? this.m_AssemIdentity : this.m_DomainIdentity) : this.m_AppIdentity;
      if (graph != null)
        binaryFormatter.Serialize((Stream) memoryStream, graph);
      memoryStream.Position = 0L;
      return memoryStream;
    }

    /// <summary>初始化新的 <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> 对象。</summary>
    /// <param name="scope">
    /// <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> 值的按位组合。</param>
    /// <param name="domainEvidenceType">可从调用应用程序的域内存在的 <see cref="T:System.Security.Policy.Evidence" /> 列表中选择的 <see cref="T:System.Security.Policy.Evidence" /> 的类型。如果为 null，则允许 <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> 对象选择证据。</param>
    /// <param name="assemblyEvidenceType">可从调用应用程序的程序集内存在的 <see cref="T:System.Security.Policy.Evidence" /> 列表中选择的 <see cref="T:System.Security.Policy.Evidence" /> 的类型。如果为 null，则允许 <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> 对象选择证据。</param>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">指定的程序集没有足够的权限创建独立存储区。</exception>
    [SecuritySafeCritical]
    protected void InitStore(IsolatedStorageScope scope, Type domainEvidenceType, Type assemblyEvidenceType)
    {
      PermissionSet newGrant = (PermissionSet) null;
      PermissionSet newDenied = (PermissionSet) null;
      RuntimeAssembly caller = System.IO.IsolatedStorage.IsolatedStorage.GetCaller();
      System.IO.IsolatedStorage.IsolatedStorage.GetControlEvidencePermission().Assert();
      if (System.IO.IsolatedStorage.IsolatedStorage.IsDomain(scope))
      {
        AppDomain domain = Thread.GetDomain();
        if (!System.IO.IsolatedStorage.IsolatedStorage.IsRoaming(scope))
        {
          newGrant = domain.PermissionSet;
          if (newGrant == null)
            throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DomainGrantSet"));
        }
        this._InitStore(scope, domain.Evidence, domainEvidenceType, caller.Evidence, assemblyEvidenceType, (Evidence) null, (Type) null);
      }
      else
      {
        if (!System.IO.IsolatedStorage.IsolatedStorage.IsRoaming(scope))
        {
          caller.GetGrantSet(out newGrant, out newDenied);
          if (newGrant == null)
            throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_AssemblyGrantSet"));
        }
        this._InitStore(scope, (Evidence) null, (Type) null, caller.Evidence, assemblyEvidenceType, (Evidence) null, (Type) null);
      }
      this.SetQuota(newGrant, newDenied);
    }

    /// <summary>初始化新的 <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> 对象。</summary>
    /// <param name="scope">
    /// <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> 值的按位组合。</param>
    /// <param name="appEvidenceType">可从调用应用程序的 <see cref="T:System.Security.Policy.Evidence" /> 列表中选择的 <see cref="T:System.Security.Policy.Evidence" /> 的类型。如果为 null，则允许 <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> 对象选择证据。</param>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">指定的程序集没有足够的权限创建独立存储区。</exception>
    [SecuritySafeCritical]
    protected void InitStore(IsolatedStorageScope scope, Type appEvidenceType)
    {
      PermissionSet psAllowed = (PermissionSet) null;
      PermissionSet psDenied = (PermissionSet) null;
      System.IO.IsolatedStorage.IsolatedStorage.GetCaller();
      System.IO.IsolatedStorage.IsolatedStorage.GetControlEvidencePermission().Assert();
      if (System.IO.IsolatedStorage.IsolatedStorage.IsApp(scope))
      {
        AppDomain domain = Thread.GetDomain();
        if (!System.IO.IsolatedStorage.IsolatedStorage.IsRoaming(scope))
        {
          psAllowed = domain.PermissionSet;
          if (psAllowed == null)
            throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DomainGrantSet"));
        }
        ActivationContext activationContext = AppDomain.CurrentDomain.ActivationContext;
        if (activationContext == null)
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_ApplicationMissingIdentity"));
        ApplicationSecurityInfo applicationSecurityInfo = new ApplicationSecurityInfo(activationContext);
        this._InitStore(scope, (Evidence) null, (Type) null, (Evidence) null, (Type) null, applicationSecurityInfo.ApplicationEvidence, appEvidenceType);
      }
      this.SetQuota(psAllowed, psDenied);
    }

    [SecuritySafeCritical]
    internal void InitStore(IsolatedStorageScope scope, object domain, object assem, object app)
    {
      PermissionSet psAllowed = (PermissionSet) null;
      PermissionSet psDenied = (PermissionSet) null;
      Evidence domainEv = (Evidence) null;
      Evidence assemEv = (Evidence) null;
      Evidence appEv = (Evidence) null;
      if (System.IO.IsolatedStorage.IsolatedStorage.IsApp(scope))
      {
        EvidenceBase evidence = app as EvidenceBase ?? (EvidenceBase) new LegacyEvidenceWrapper(app);
        appEv = new Evidence();
        appEv.AddHostEvidence<EvidenceBase>(evidence);
      }
      else
      {
        EvidenceBase evidence1 = assem as EvidenceBase ?? (EvidenceBase) new LegacyEvidenceWrapper(assem);
        assemEv = new Evidence();
        assemEv.AddHostEvidence<EvidenceBase>(evidence1);
        if (System.IO.IsolatedStorage.IsolatedStorage.IsDomain(scope))
        {
          EvidenceBase evidence2 = domain as EvidenceBase ?? (EvidenceBase) new LegacyEvidenceWrapper(domain);
          domainEv = new Evidence();
          domainEv.AddHostEvidence<EvidenceBase>(evidence2);
        }
      }
      this._InitStore(scope, domainEv, (Type) null, assemEv, (Type) null, appEv, (Type) null);
      if (!System.IO.IsolatedStorage.IsolatedStorage.IsRoaming(scope))
      {
        RuntimeAssembly caller = System.IO.IsolatedStorage.IsolatedStorage.GetCaller();
        System.IO.IsolatedStorage.IsolatedStorage.GetControlEvidencePermission().Assert();
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        PermissionSet& newGrant = @psAllowed;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        PermissionSet& newDenied = @psDenied;
        caller.GetGrantSet(newGrant, newDenied);
        if (psAllowed == null)
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_AssemblyGrantSet"));
      }
      this.SetQuota(psAllowed, psDenied);
    }

    [SecurityCritical]
    internal void InitStore(IsolatedStorageScope scope, Evidence domainEv, Type domainEvidenceType, Evidence assemEv, Type assemEvidenceType, Evidence appEv, Type appEvidenceType)
    {
      PermissionSet psAllowed = (PermissionSet) null;
      if (!System.IO.IsolatedStorage.IsolatedStorage.IsRoaming(scope))
        psAllowed = !System.IO.IsolatedStorage.IsolatedStorage.IsApp(scope) ? (!System.IO.IsolatedStorage.IsolatedStorage.IsDomain(scope) ? SecurityManager.GetStandardSandbox(assemEv) : SecurityManager.GetStandardSandbox(domainEv)) : SecurityManager.GetStandardSandbox(appEv);
      this._InitStore(scope, domainEv, domainEvidenceType, assemEv, assemEvidenceType, appEv, appEvidenceType);
      this.SetQuota(psAllowed, (PermissionSet) null);
    }

    [SecuritySafeCritical]
    internal bool InitStore(IsolatedStorageScope scope, Stream domain, Stream assem, Stream app, string domainName, string assemName, string appName)
    {
      try
      {
        System.IO.IsolatedStorage.IsolatedStorage.GetUnrestricted().Assert();
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        if (System.IO.IsolatedStorage.IsolatedStorage.IsApp(scope))
        {
          this.m_AppIdentity = binaryFormatter.Deserialize(app);
          this.m_AppName = appName;
        }
        else
        {
          this.m_AssemIdentity = binaryFormatter.Deserialize(assem);
          this.m_AssemName = assemName;
          if (System.IO.IsolatedStorage.IsolatedStorage.IsDomain(scope))
          {
            this.m_DomainIdentity = binaryFormatter.Deserialize(domain);
            this.m_DomainName = domainName;
          }
        }
      }
      catch
      {
        return false;
      }
      this.m_Scope = scope;
      return true;
    }

    [SecurityCritical]
    private void _InitStore(IsolatedStorageScope scope, Evidence domainEv, Type domainEvidenceType, Evidence assemEv, Type assemblyEvidenceType, Evidence appEv, Type appEvidenceType)
    {
      System.IO.IsolatedStorage.IsolatedStorage.VerifyScope(scope);
      if (System.IO.IsolatedStorage.IsolatedStorage.IsApp(scope))
      {
        if (appEv == null)
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_ApplicationMissingIdentity"));
      }
      else
      {
        if (assemEv == null)
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_AssemblyMissingIdentity"));
        if (System.IO.IsolatedStorage.IsolatedStorage.IsDomain(scope) && domainEv == null)
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DomainMissingIdentity"));
      }
      System.IO.IsolatedStorage.IsolatedStorage.DemandPermission(scope);
      string typeName = (string) null;
      string instanceName = (string) null;
      if (System.IO.IsolatedStorage.IsolatedStorage.IsApp(scope))
      {
        this.m_AppIdentity = System.IO.IsolatedStorage.IsolatedStorage.GetAccountingInfo(appEv, appEvidenceType, IsolatedStorageScope.Application, out typeName, out instanceName);
        this.m_AppName = this.GetNameFromID(typeName, instanceName);
      }
      else
      {
        this.m_AssemIdentity = System.IO.IsolatedStorage.IsolatedStorage.GetAccountingInfo(assemEv, assemblyEvidenceType, IsolatedStorageScope.Assembly, out typeName, out instanceName);
        this.m_AssemName = this.GetNameFromID(typeName, instanceName);
        if (System.IO.IsolatedStorage.IsolatedStorage.IsDomain(scope))
        {
          this.m_DomainIdentity = System.IO.IsolatedStorage.IsolatedStorage.GetAccountingInfo(domainEv, domainEvidenceType, IsolatedStorageScope.Domain, out typeName, out instanceName);
          this.m_DomainName = this.GetNameFromID(typeName, instanceName);
        }
      }
      this.m_Scope = scope;
    }

    [SecurityCritical]
    private static object GetAccountingInfo(Evidence evidence, Type evidenceType, IsolatedStorageScope fAssmDomApp, out string typeName, out string instanceName)
    {
      object oNormalized = (object) null;
      object accountingInfo = System.IO.IsolatedStorage.IsolatedStorage._GetAccountingInfo(evidence, evidenceType, fAssmDomApp, out oNormalized);
      typeName = System.IO.IsolatedStorage.IsolatedStorage.GetPredefinedTypeName(accountingInfo);
      if (typeName == null)
      {
        System.IO.IsolatedStorage.IsolatedStorage.GetUnrestricted().Assert();
        MemoryStream memoryStream = new MemoryStream();
        new BinaryFormatter().Serialize((Stream) memoryStream, (object) accountingInfo.GetType());
        memoryStream.Position = 0L;
        typeName = System.IO.IsolatedStorage.IsolatedStorage.GetHash((Stream) memoryStream);
        CodeAccessPermission.RevertAssert();
      }
      instanceName = (string) null;
      if (oNormalized != null)
      {
        if (oNormalized is Stream)
          instanceName = System.IO.IsolatedStorage.IsolatedStorage.GetHash((Stream) oNormalized);
        else if (oNormalized is string)
        {
          if (System.IO.IsolatedStorage.IsolatedStorage.IsValidName((string) oNormalized))
          {
            instanceName = (string) oNormalized;
          }
          else
          {
            MemoryStream memoryStream = new MemoryStream();
            new BinaryWriter((Stream) memoryStream).Write((string) oNormalized);
            memoryStream.Position = 0L;
            instanceName = System.IO.IsolatedStorage.IsolatedStorage.GetHash((Stream) memoryStream);
          }
        }
      }
      else
        oNormalized = accountingInfo;
      if (instanceName == null)
      {
        System.IO.IsolatedStorage.IsolatedStorage.GetUnrestricted().Assert();
        MemoryStream memoryStream = new MemoryStream();
        new BinaryFormatter().Serialize((Stream) memoryStream, oNormalized);
        memoryStream.Position = 0L;
        instanceName = System.IO.IsolatedStorage.IsolatedStorage.GetHash((Stream) memoryStream);
        CodeAccessPermission.RevertAssert();
      }
      return accountingInfo;
    }

    private static object _GetAccountingInfo(Evidence evidence, Type evidenceType, IsolatedStorageScope fAssmDomApp, out object oNormalized)
    {
      object obj;
      if (evidenceType == (Type) null)
      {
        obj = ((((object) evidence.GetHostEvidence<Publisher>() ?? (object) evidence.GetHostEvidence<StrongName>()) ?? (object) evidence.GetHostEvidence<Url>()) ?? (object) evidence.GetHostEvidence<Site>()) ?? (object) evidence.GetHostEvidence<Zone>();
        if (obj == null)
        {
          if (fAssmDomApp == IsolatedStorageScope.Domain)
            throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DomainNoEvidence"));
          if (fAssmDomApp == IsolatedStorageScope.Application)
            throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_ApplicationNoEvidence"));
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_AssemblyNoEvidence"));
        }
      }
      else
      {
        obj = (object) evidence.GetHostEvidence(evidenceType);
        if (obj == null)
        {
          if (fAssmDomApp == IsolatedStorageScope.Domain)
            throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DomainNoEvidence"));
          if (fAssmDomApp == IsolatedStorageScope.Application)
            throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_ApplicationNoEvidence"));
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_AssemblyNoEvidence"));
        }
      }
      oNormalized = !(obj is INormalizeForIsolatedStorage) ? (!(obj is Publisher) ? (!(obj is StrongName) ? (!(obj is Url) ? (!(obj is Site) ? (!(obj is Zone) ? (object) null : ((Zone) obj).Normalize()) : ((Site) obj).Normalize()) : ((Url) obj).Normalize()) : ((StrongName) obj).Normalize()) : ((Publisher) obj).Normalize()) : ((INormalizeForIsolatedStorage) obj).Normalize();
      return obj;
    }

    [SecurityCritical]
    private static void DemandPermission(IsolatedStorageScope scope)
    {
      IsolatedStorageFilePermission storageFilePermission = (IsolatedStorageFilePermission) null;
      if (scope <= (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Roaming))
      {
        if (scope <= (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly))
        {
          if (scope != (IsolatedStorageScope.User | IsolatedStorageScope.Assembly))
          {
            if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly))
            {
              if (System.IO.IsolatedStorage.IsolatedStorage.s_PermDomain == null)
                System.IO.IsolatedStorage.IsolatedStorage.s_PermDomain = new IsolatedStorageFilePermission(IsolatedStorageContainment.DomainIsolationByUser, 0L, false);
              storageFilePermission = System.IO.IsolatedStorage.IsolatedStorage.s_PermDomain;
            }
          }
          else
          {
            if (System.IO.IsolatedStorage.IsolatedStorage.s_PermAssem == null)
              System.IO.IsolatedStorage.IsolatedStorage.s_PermAssem = new IsolatedStorageFilePermission(IsolatedStorageContainment.AssemblyIsolationByUser, 0L, false);
            storageFilePermission = System.IO.IsolatedStorage.IsolatedStorage.s_PermAssem;
          }
        }
        else if (scope != (IsolatedStorageScope.User | IsolatedStorageScope.Assembly | IsolatedStorageScope.Roaming))
        {
          if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Roaming))
          {
            if (System.IO.IsolatedStorage.IsolatedStorage.s_PermDomainRoaming == null)
              System.IO.IsolatedStorage.IsolatedStorage.s_PermDomainRoaming = new IsolatedStorageFilePermission(IsolatedStorageContainment.DomainIsolationByRoamingUser, 0L, false);
            storageFilePermission = System.IO.IsolatedStorage.IsolatedStorage.s_PermDomainRoaming;
          }
        }
        else
        {
          if (System.IO.IsolatedStorage.IsolatedStorage.s_PermAssemRoaming == null)
            System.IO.IsolatedStorage.IsolatedStorage.s_PermAssemRoaming = new IsolatedStorageFilePermission(IsolatedStorageContainment.AssemblyIsolationByRoamingUser, 0L, false);
          storageFilePermission = System.IO.IsolatedStorage.IsolatedStorage.s_PermAssemRoaming;
        }
      }
      else if (scope <= (IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine))
      {
        if (scope != (IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine))
        {
          if (scope == (IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine))
          {
            if (System.IO.IsolatedStorage.IsolatedStorage.s_PermMachineDomain == null)
              System.IO.IsolatedStorage.IsolatedStorage.s_PermMachineDomain = new IsolatedStorageFilePermission(IsolatedStorageContainment.DomainIsolationByMachine, 0L, false);
            storageFilePermission = System.IO.IsolatedStorage.IsolatedStorage.s_PermMachineDomain;
          }
        }
        else
        {
          if (System.IO.IsolatedStorage.IsolatedStorage.s_PermMachineAssem == null)
            System.IO.IsolatedStorage.IsolatedStorage.s_PermMachineAssem = new IsolatedStorageFilePermission(IsolatedStorageContainment.AssemblyIsolationByMachine, 0L, false);
          storageFilePermission = System.IO.IsolatedStorage.IsolatedStorage.s_PermMachineAssem;
        }
      }
      else if (scope != (IsolatedStorageScope.User | IsolatedStorageScope.Application))
      {
        if (scope != (IsolatedStorageScope.User | IsolatedStorageScope.Roaming | IsolatedStorageScope.Application))
        {
          if (scope == (IsolatedStorageScope.Machine | IsolatedStorageScope.Application))
          {
            if (System.IO.IsolatedStorage.IsolatedStorage.s_PermAppMachine == null)
              System.IO.IsolatedStorage.IsolatedStorage.s_PermAppMachine = new IsolatedStorageFilePermission(IsolatedStorageContainment.ApplicationIsolationByMachine, 0L, false);
            storageFilePermission = System.IO.IsolatedStorage.IsolatedStorage.s_PermAppMachine;
          }
        }
        else
        {
          if (System.IO.IsolatedStorage.IsolatedStorage.s_PermAppUserRoaming == null)
            System.IO.IsolatedStorage.IsolatedStorage.s_PermAppUserRoaming = new IsolatedStorageFilePermission(IsolatedStorageContainment.ApplicationIsolationByRoamingUser, 0L, false);
          storageFilePermission = System.IO.IsolatedStorage.IsolatedStorage.s_PermAppUserRoaming;
        }
      }
      else
      {
        if (System.IO.IsolatedStorage.IsolatedStorage.s_PermAppUser == null)
          System.IO.IsolatedStorage.IsolatedStorage.s_PermAppUser = new IsolatedStorageFilePermission(IsolatedStorageContainment.ApplicationIsolationByUser, 0L, false);
        storageFilePermission = System.IO.IsolatedStorage.IsolatedStorage.s_PermAppUser;
      }
      storageFilePermission.Demand();
    }

    internal static void VerifyScope(IsolatedStorageScope scope)
    {
      if (scope != (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly) && scope != (IsolatedStorageScope.User | IsolatedStorageScope.Assembly) && (scope != (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Roaming) && scope != (IsolatedStorageScope.User | IsolatedStorageScope.Assembly | IsolatedStorageScope.Roaming)) && (scope != (IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine) && scope != (IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine) && (scope != (IsolatedStorageScope.User | IsolatedStorageScope.Application) && scope != (IsolatedStorageScope.Machine | IsolatedStorageScope.Application))) && scope != (IsolatedStorageScope.User | IsolatedStorageScope.Roaming | IsolatedStorageScope.Application))
        throw new ArgumentException(Environment.GetResourceString("IsolatedStorage_Scope_Invalid"));
    }

    [SecurityCritical]
    internal virtual void SetQuota(PermissionSet psAllowed, PermissionSet psDenied)
    {
      IsolatedStoragePermission permission1 = this.GetPermission(psAllowed);
      this.m_Quota = 0UL;
      if (permission1 != null)
        this.m_Quota = !permission1.IsUnrestricted() ? (ulong) permission1.UserQuota : 9223372036854775807UL;
      if (psDenied != null)
      {
        IsolatedStoragePermission permission2 = this.GetPermission(psDenied);
        if (permission2 != null)
        {
          if (permission2.IsUnrestricted())
          {
            this.m_Quota = 0UL;
          }
          else
          {
            ulong num = (ulong) permission2.UserQuota;
            this.m_Quota = num <= this.m_Quota ? this.m_Quota - num : 0UL;
          }
        }
      }
      this.m_ValidQuota = true;
    }

    /// <summary>当在派生类中重写时，移除个别独立存储区和包含的所有数据。</summary>
    public abstract void Remove();

    /// <summary>当由派生类实现时，从权限集中返回表示对独立存储的访问权限的权限。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Permissions.IsolatedStoragePermission" /> 对象。</returns>
    /// <param name="ps">
    /// <see cref="T:System.Security.PermissionSet" />，它包含授予尝试使用独立存储的代码的权限集。</param>
    protected abstract IsolatedStoragePermission GetPermission(PermissionSet ps);

    [SecuritySafeCritical]
    internal static RuntimeAssembly GetCaller()
    {
      RuntimeAssembly o = (RuntimeAssembly) null;
      System.IO.IsolatedStorage.IsolatedStorage.GetCaller(JitHelpers.GetObjectHandleOnStack<RuntimeAssembly>(ref o));
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetCaller(ObjectHandleOnStack retAssembly);
  }
}
