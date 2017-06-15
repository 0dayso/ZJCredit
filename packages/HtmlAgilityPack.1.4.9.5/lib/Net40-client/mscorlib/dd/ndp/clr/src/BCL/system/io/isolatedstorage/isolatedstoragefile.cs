// Decompiled with JetBrains decompiler
// Type: System.IO.IsolatedStorage.IsolatedStorageFile
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading;

namespace System.IO.IsolatedStorage
{
  /// <summary>表示包含文件和目录的独立存储区。</summary>
  [ComVisible(true)]
  public sealed class IsolatedStorageFile : System.IO.IsolatedStorage.IsolatedStorage, IDisposable
  {
    private object m_internalLock = new object();
    private const int s_BlockSize = 1024;
    private const int s_DirSize = 1024;
    private const string s_name = "file.store";
    internal const string s_Files = "Files";
    internal const string s_AssemFiles = "AssemFiles";
    internal const string s_AppFiles = "AppFiles";
    internal const string s_IDFile = "identity.dat";
    internal const string s_InfoFile = "info.dat";
    internal const string s_AppInfoFile = "appinfo.dat";
    private static volatile string s_RootDirUser;
    private static volatile string s_RootDirMachine;
    private static volatile string s_RootDirRoaming;
    private static volatile string s_appDataDir;
    private static volatile FileIOPermission s_PermUser;
    private static volatile FileIOPermission s_PermMachine;
    private static volatile FileIOPermission s_PermRoaming;
    private static volatile IsolatedStorageFilePermission s_PermAdminUser;
    private FileIOPermission m_fiop;
    private string m_RootDir;
    private string m_InfoFile;
    private string m_SyncObjectName;
    [SecurityCritical]
    private SafeIsolatedStorageFileHandle m_handle;
    private bool m_closed;
    private bool m_bDisposed;
    private IsolatedStorageScope m_StoreScope;

    /// <summary>获取一个值，该值表示用于独立存储的空间量。</summary>
    /// <returns>已用的独立存储空间，以字节为单位。</returns>
    /// <exception cref="T:System.InvalidOperationException">独立存储区已被关闭。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">独立存储区已被移除。</exception>
    /// <exception cref="T:System.ObjectDisposedException">独立存储区已被释放。</exception>
    public override long UsedSize
    {
      [SecuritySafeCritical] get
      {
        if (this.IsRoaming())
          throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_CurrentSizeUndefined"));
        lock (this.m_internalLock)
        {
          if (this.m_bDisposed)
            throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
          if (this.m_closed)
            throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
          if (this.InvalidFileHandle)
            this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
          return (long) IsolatedStorageFile.GetUsage(this.m_handle);
        }
      }
    }

    /// <summary>获取独立存储的当前大小。</summary>
    /// <returns>独立存储范围中当前正在使用的存储区的总字节数。</returns>
    /// <exception cref="T:System.InvalidOperationException">该属性不可用。当前存储区有漫游范围或者没有打开。</exception>
    /// <exception cref="T:System.ObjectDisposedException">未定义当前对象的大小。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [CLSCompliant(false)]
    [Obsolete("IsolatedStorageFile.CurrentSize has been deprecated because it is not CLS Compliant.  To get the current size use IsolatedStorageFile.UsedSize")]
    public override ulong CurrentSize
    {
      [SecuritySafeCritical] get
      {
        if (this.IsRoaming())
          throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_CurrentSizeUndefined"));
        lock (this.m_internalLock)
        {
          if (this.m_bDisposed)
            throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
          if (this.m_closed)
            throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
          if (this.InvalidFileHandle)
            this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
          return IsolatedStorageFile.GetUsage(this.m_handle);
        }
      }
    }

    /// <summary>获取一个值，该值表示独立存储的可用空间量。</summary>
    /// <returns>独立存储的可用空间，以字节为单位。</returns>
    /// <exception cref="T:System.InvalidOperationException">独立存储区已关闭。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">独立存储区已被移除。- 或 -独立存储被禁用。</exception>
    /// <exception cref="T:System.ObjectDisposedException">独立存储区已被释放。</exception>
    [ComVisible(false)]
    public override long AvailableFreeSpace
    {
      [SecuritySafeCritical] get
      {
        if (this.IsRoaming())
          return long.MaxValue;
        long num;
        lock (this.m_internalLock)
        {
          if (this.m_bDisposed)
            throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
          if (this.m_closed)
            throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
          if (this.InvalidFileHandle)
            this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
          num = (long) IsolatedStorageFile.GetUsage(this.m_handle);
        }
        return this.Quota - num;
      }
    }

    /// <summary>获取一个值，该值表示独立存储的最大可用空间量。</summary>
    /// <returns>独立存储空间的限制，以字节为单位。</returns>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">独立存储区已被移除。- 或 -独立存储被禁用。</exception>
    /// <exception cref="T:System.ObjectDisposedException">独立存储区已被释放。</exception>
    [ComVisible(false)]
    public override long Quota
    {
      get
      {
        if (this.IsRoaming())
          return long.MaxValue;
        return base.Quota;
      }
      [SecuritySafeCritical] internal set
      {
        bool locked = false;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
          this.Lock(ref locked);
          lock (this.m_internalLock)
          {
            if (this.InvalidFileHandle)
              this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
            IsolatedStorageFile.SetQuota(this.m_handle, value);
          }
        }
        finally
        {
          if (locked)
            this.Unlock();
        }
        base.Quota = value;
      }
    }

    /// <summary>获取一个值，该值指示是否启用了独立存储。</summary>
    /// <returns>任何情况下都为 true。</returns>
    [ComVisible(false)]
    public static bool IsEnabled
    {
      get
      {
        return true;
      }
    }

    /// <summary>获取一个值，该值表示在由配额设定的限制内独立存储的最大可用空间。</summary>
    /// <returns>独立存储空间的限制，以字节为单位。</returns>
    /// <exception cref="T:System.InvalidOperationException">该属性不可用。如果没有来自程序集创建过程的证据，则无法确定 <see cref="P:System.IO.IsolatedStorage.IsolatedStorageFile.MaximumSize" />。当创建该对象时未能确定证据。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">发生独立存储错误。</exception>
    [CLSCompliant(false)]
    [Obsolete("IsolatedStorageFile.MaximumSize has been deprecated because it is not CLS Compliant.  To get the maximum size use IsolatedStorageFile.Quota")]
    public override ulong MaximumSize
    {
      get
      {
        if (this.IsRoaming())
          return 9223372036854775807;
        return base.MaximumSize;
      }
    }

    internal string RootDirectory
    {
      get
      {
        return this.m_RootDir;
      }
    }

    internal bool Disposed
    {
      get
      {
        return this.m_bDisposed;
      }
    }

    private bool InvalidFileHandle
    {
      [SecuritySafeCritical] get
      {
        if (this.m_handle != null && !this.m_handle.IsClosed)
          return this.m_handle.IsInvalid;
        return true;
      }
    }

    internal IsolatedStorageFile()
    {
    }

    ~IsolatedStorageFile()
    {
      this.Dispose();
    }

    /// <summary>获取与应用程序域标识和程序集标识对应的用户范围的独立存储。</summary>
    /// <returns>与基于应用程序域标识和程序集标识组合的 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> 对应的对象。</returns>
    /// <exception cref="T:System.Security.SecurityException">未授予使用独立存储区的足够权限。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">存储区未能打开。- 或 -指定的程序集没有足够的权限创建独立存储区。- 或 -无法初始化独立的存储位置。- 或 -不能确定应用程序域的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.IsolatedStorageFilePermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static IsolatedStorageFile GetUserStoreForDomain()
    {
      return IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, (Type) null, (Type) null);
    }

    /// <summary>获取与调用代码的程序集标识对应的用户范围的独立存储。</summary>
    /// <returns>与基于调用代码的程序集标识的独立存储范围对应的对象。</returns>
    /// <exception cref="T:System.Security.SecurityException">未授予使用独立存储区的足够权限。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">无法初始化独立的存储位置。- 或 -不能确定调用程序集的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.IsolatedStorageFilePermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static IsolatedStorageFile GetUserStoreForAssembly()
    {
      return IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, (Type) null, (Type) null);
    }

    /// <summary>获取与调用代码的应用程序标识对应的用户范围的独立存储。</summary>
    /// <returns>与基于调用代码的程序集标识的独立存储范围对应的对象。</returns>
    /// <exception cref="T:System.Security.SecurityException">未授予使用独立存储区的足够权限。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">无法初始化独立的存储位置。- 或 -未能确定调用方的应用程序标识，因为 <see cref="P:System.AppDomain.ActivationContext" /> 属性返回了 null。- 或 -不能确定应用程序域的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.IsolatedStorageFilePermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static IsolatedStorageFile GetUserStoreForApplication()
    {
      return IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Application, (Type) null);
    }

    /// <summary>获取用户范围的独立存储，供虚拟主机域中的应用程序使用。</summary>
    /// <returns>与基于调用代码的应用程序标识的独立存储范围对应的独立存储文件。</returns>
    [ComVisible(false)]
    public static IsolatedStorageFile GetUserStoreForSite()
    {
      throw new NotSupportedException(Environment.GetResourceString("IsolatedStorage_NotValidOnDesktop"));
    }

    /// <summary>获取与应用程序域标识和程序集标识对应的计算机范围的独立存储。</summary>
    /// <returns>与基于应用程序域标识和程序集标识组合的 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> 对应的对象。</returns>
    /// <exception cref="T:System.Security.SecurityException">未授予使用独立存储区的足够权限。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">存储区未能打开。- 或 -指定的程序集没有足够的权限创建独立存储区。- 或 -不能确定应用程序域的权限。- 或 -无法初始化独立的存储位置。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.IsolatedStorageFilePermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static IsolatedStorageFile GetMachineStoreForDomain()
    {
      return IsolatedStorageFile.GetStore(IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine, (Type) null, (Type) null);
    }

    /// <summary>获取与调用代码的程序集标识对应的计算机范围的独立存储。</summary>
    /// <returns>与基于调用代码的程序集标识的独立存储范围对应的对象。</returns>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">无法初始化独立的存储位置。</exception>
    /// <exception cref="T:System.Security.SecurityException">未授予使用独立存储区的足够权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.IsolatedStorageFilePermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static IsolatedStorageFile GetMachineStoreForAssembly()
    {
      return IsolatedStorageFile.GetStore(IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine, (Type) null, (Type) null);
    }

    /// <summary>获取与调用代码的应用程序标识对应的计算机范围的独立存储。</summary>
    /// <returns>与基于调用代码的应用程序标识的独立存储范围对应的对象。</returns>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">未能确定调用方的应用程序标识。- 或 -未能确定为应用程序域授予的权限集。- 或 -无法初始化独立的存储位置。</exception>
    /// <exception cref="T:System.Security.SecurityException">未授予使用独立存储区的足够权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.IsolatedStorageFilePermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static IsolatedStorageFile GetMachineStoreForApplication()
    {
      return IsolatedStorageFile.GetStore(IsolatedStorageScope.Machine | IsolatedStorageScope.Application, (Type) null);
    }

    /// <summary>已知应用程序域和程序集证据类型，获取与独立存储范围对应的独立存储。</summary>
    /// <returns>表示该参数的对象。</returns>
    /// <param name="scope">枚举值的按位组合。</param>
    /// <param name="domainEvidenceType">可从调用应用程序的域内存在的 <see cref="T:System.Security.Policy.Evidence" /> 列表中选择的 <see cref="T:System.Security.Policy.Evidence" /> 的类型。如果为 null，则允许 <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> 对象选择证据。</param>
    /// <param name="assemblyEvidenceType">可从调用应用程序的域内存在的 <see cref="T:System.Security.Policy.Evidence" /> 列表中选择的 <see cref="T:System.Security.Policy.Evidence" /> 的类型。如果为 null，则允许 <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> 对象选择证据。</param>
    /// <exception cref="T:System.Security.SecurityException">未授予使用独立存储区的足够权限。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="scope" /> 无效。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">所提供的证据类型没有出现在程序集证据列表中。- 或 -无法初始化独立的存储位置。- 或 -<paramref name="scope" /> 包含枚举值 <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" />，但不能确定调用方的应用程序标识，因为当前应用程序域的 <see cref="P:System.AppDomain.ActivationContext" /> 返回了 null。- 或 -<paramref name="scope" /> 包含值 <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" />，但不能确定应用程序域的权限。- 或 -<paramref name="scope" /> 包含 <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Assembly" />，但不能决定调用程序集的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.IsolatedStorageFilePermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, Type domainEvidenceType, Type assemblyEvidenceType)
    {
      if (domainEvidenceType != (Type) null)
        IsolatedStorageFile.DemandAdminPermission();
      IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile();
      int num1 = (int) scope;
      Type domainEvidenceType1 = domainEvidenceType;
      Type assemblyEvidenceType1 = assemblyEvidenceType;
      isolatedStorageFile.InitStore((IsolatedStorageScope) num1, domainEvidenceType1, assemblyEvidenceType1);
      int num2 = (int) scope;
      isolatedStorageFile.Init((IsolatedStorageScope) num2);
      return isolatedStorageFile;
    }

    internal void EnsureStoreIsValid()
    {
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      if (this.m_closed)
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
    }

    /// <summary>获取与给定的应用程序域和程序集证据对象相对应的独立存储。</summary>
    /// <returns>表示该参数的对象。</returns>
    /// <param name="scope">枚举值的按位组合。</param>
    /// <param name="domainIdentity">包含应用程序域标识的证据的对象。</param>
    /// <param name="assemblyIdentity">包含代码程序集标识的证据的对象。</param>
    /// <exception cref="T:System.Security.SecurityException">未授予使用独立存储区的足够权限。</exception>
    /// <exception cref="T:System.ArgumentNullException">既不传递 <paramref name="domainIdentity" />，也不传递 <paramref name="assemblyIdentity" />。这将验证使用的是正确的构造函数。- 或 -<paramref name="domainIdentity" /> 或 <paramref name="assemblyIdentity" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="scope" /> 无效。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">无法初始化独立的存储位置。- 或 -<paramref name="scope" /> 包含枚举值 <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" />，但不能确定调用方的应用程序标识，因为当前应用程序域的 <see cref="P:System.AppDomain.ActivationContext" /> 返回了 null。- 或 -<paramref name="scope" /> 包含值 <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" />，但不能确定应用程序域的权限。- 或 -<paramref name="scope" /> 包含值 <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Assembly" />，但不能确定调用程序集的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.IsolatedStorageFilePermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, object domainIdentity, object assemblyIdentity)
    {
      if (assemblyIdentity == null)
        throw new ArgumentNullException("assemblyIdentity");
      if (System.IO.IsolatedStorage.IsolatedStorage.IsDomain(scope) && domainIdentity == null)
        throw new ArgumentNullException("domainIdentity");
      IsolatedStorageFile.DemandAdminPermission();
      IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile();
      int num1 = (int) scope;
      object domain = domainIdentity;
      object assem = assemblyIdentity;
      // ISSUE: variable of the null type
      __Null local = null;
      isolatedStorageFile.InitStore((IsolatedStorageScope) num1, domain, assem, (object) local);
      int num2 = (int) scope;
      isolatedStorageFile.Init((IsolatedStorageScope) num2);
      return isolatedStorageFile;
    }

    /// <summary>获取与给定的应用程序域和程序集证据对象及类型对应的独立存储。</summary>
    /// <returns>表示该参数的对象。</returns>
    /// <param name="scope">枚举值的按位组合。</param>
    /// <param name="domainEvidence">包含应用程序域标识的对象。</param>
    /// <param name="domainEvidenceType">要从应用程序域证据中选择的标识类型 。</param>
    /// <param name="assemblyEvidence">包含代码程序集标识的对象。</param>
    /// <param name="assemblyEvidenceType">要从应用程序代码程序集证据中选择的标识类型 。</param>
    /// <exception cref="T:System.Security.SecurityException">未授予使用独立存储区的足够权限。</exception>
    /// <exception cref="T:System.ArgumentNullException">尚未传入 <paramref name="domainEvidence" /> 或 <paramref name="assemblyEvidence" /> 标识。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="scope" /> 无效。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">无法初始化独立的存储位置。- 或 -<paramref name="scope" /> 包含枚举值 <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" />，但不能确定调用方的应用程序标识，因为当前应用程序域的 <see cref="P:System.AppDomain.ActivationContext" /> 返回了 null。- 或 -<paramref name="scope" /> 包含值 <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" />，但不能确定应用程序域的权限。- 或 -<paramref name="scope" /> 包含值 <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Assembly" />，但不能确定调用程序集的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.IsolatedStorageFilePermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, Evidence domainEvidence, Type domainEvidenceType, Evidence assemblyEvidence, Type assemblyEvidenceType)
    {
      if (assemblyEvidence == null)
        throw new ArgumentNullException("assemblyEvidence");
      if (System.IO.IsolatedStorage.IsolatedStorage.IsDomain(scope) && domainEvidence == null)
        throw new ArgumentNullException("domainEvidence");
      IsolatedStorageFile.DemandAdminPermission();
      IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile();
      int num1 = (int) scope;
      Evidence domainEv = domainEvidence;
      Type domainEvidenceType1 = domainEvidenceType;
      Evidence assemEv = assemblyEvidence;
      Type assemEvidenceType = assemblyEvidenceType;
      // ISSUE: variable of the null type
      __Null local1 = null;
      // ISSUE: variable of the null type
      __Null local2 = null;
      isolatedStorageFile.InitStore((IsolatedStorageScope) num1, domainEv, domainEvidenceType1, assemEv, assemEvidenceType, (Evidence) local1, (Type) local2);
      int num2 = (int) scope;
      isolatedStorageFile.Init((IsolatedStorageScope) num2);
      return isolatedStorageFile;
    }

    /// <summary>获取与隔离范围和应用程序标识对象对应的独立存储。</summary>
    /// <returns>表示该参数的对象。</returns>
    /// <param name="scope">枚举值的按位组合。</param>
    /// <param name="applicationEvidenceType">包含应用程序标识的对象。</param>
    /// <exception cref="T:System.Security.SecurityException">未授予使用独立存储区的足够权限。</exception>
    /// <exception cref="T:System.ArgumentNullException">尚未传入 <paramref name="applicationEvidence" /> 标识。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="scope" /> 无效。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">无法初始化独立的存储位置。- 或 -<paramref name="scope" /> 包含枚举值 <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" />，但不能确定调用方的应用程序标识，因为当前应用程序域的 <see cref="P:System.AppDomain.ActivationContext" /> 返回了 null。- 或 -<paramref name="scope" /> 包含值 <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" />，但不能确定应用程序域的权限。- 或 -<paramref name="scope" /> 包含值 <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Assembly" />，但不能确定调用程序集的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.IsolatedStorageFilePermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, Type applicationEvidenceType)
    {
      if (applicationEvidenceType != (Type) null)
        IsolatedStorageFile.DemandAdminPermission();
      IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile();
      int num1 = (int) scope;
      Type appEvidenceType = applicationEvidenceType;
      isolatedStorageFile.InitStore((IsolatedStorageScope) num1, appEvidenceType);
      int num2 = (int) scope;
      isolatedStorageFile.Init((IsolatedStorageScope) num2);
      return isolatedStorageFile;
    }

    /// <summary>获取与给定应用程序标识对应的独立存储。</summary>
    /// <returns>表示该参数的对象。</returns>
    /// <param name="scope">枚举值的按位组合。</param>
    /// <param name="applicationIdentity">包含应用程序标识证据的对象。</param>
    /// <exception cref="T:System.Security.SecurityException">未授予使用独立存储区的足够权限。</exception>
    /// <exception cref="T:System.ArgumentNullException">尚未传入 <paramref name="applicationIdentity" /> 标识。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="scope" /> 无效。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">无法初始化独立的存储位置。- 或 -<paramref name="scope" /> 包含枚举值 <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" />，但不能确定调用方的应用程序标识，因为当前应用程序域的 <see cref="P:System.AppDomain.ActivationContext" /> 返回了 null。- 或 -<paramref name="scope" /> 包含值 <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" />，但不能确定应用程序域的权限。- 或 -<paramref name="scope" /> 包含值 <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Assembly" />，但不能确定调用程序集的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.IsolatedStorageFilePermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, object applicationIdentity)
    {
      if (applicationIdentity == null)
        throw new ArgumentNullException("applicationIdentity");
      IsolatedStorageFile.DemandAdminPermission();
      IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile();
      int num1 = (int) scope;
      // ISSUE: variable of the null type
      __Null local1 = null;
      // ISSUE: variable of the null type
      __Null local2 = null;
      object app = applicationIdentity;
      isolatedStorageFile.InitStore((IsolatedStorageScope) num1, (object) local1, (object) local2, app);
      int num2 = (int) scope;
      isolatedStorageFile.Init((IsolatedStorageScope) num2);
      return isolatedStorageFile;
    }

    /// <summary>使应用程序显式请求一个更大的配额大小，以字节为单位。</summary>
    /// <returns>如果接受新配额，则为 true；否则为 false。</returns>
    /// <param name="newQuotaSize">请求的大小，以字节为单位。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="newQuotaSize" /> 小于当前配额大小。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="newQuotaSize" /> 小于零，或小于或等于当前配额大小。</exception>
    /// <exception cref="T:System.InvalidOperationException">独立存储区已被关闭。</exception>
    /// <exception cref="T:System.NotSupportedException">当前作用域不是针对应用程序用户的。</exception>
    /// <exception cref="T:System.ObjectDisposedException">独立存储区已被释放。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">独立存储区已被移除。- 或 -独立存储被禁用。</exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public override bool IncreaseQuotaTo(long newQuotaSize)
    {
      if (newQuotaSize <= this.Quota)
        throw new ArgumentException(Environment.GetResourceString("IsolatedStorage_OldQuotaLarger"));
      if (this.m_StoreScope != (IsolatedStorageScope.User | IsolatedStorageScope.Application))
        throw new NotSupportedException(Environment.GetResourceString("IsolatedStorage_OnlyIncreaseUserApplicationStore"));
      IsolatedStorageSecurityState quotaForApplication = IsolatedStorageSecurityState.CreateStateToIncreaseQuotaForApplication(newQuotaSize, this.Quota - this.AvailableFreeSpace);
      try
      {
        quotaForApplication.EnsureState();
      }
      catch (IsolatedStorageException ex)
      {
        return false;
      }
      this.Quota = newQuotaSize;
      return true;
    }

    [SecuritySafeCritical]
    internal void Reserve(ulong lReserve)
    {
      if (this.IsRoaming())
        return;
      ulong plQuota = (ulong) this.Quota;
      ulong plReserve = lReserve;
      lock (this.m_internalLock)
      {
        if (this.m_bDisposed)
          throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
        if (this.m_closed)
          throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
        if (this.InvalidFileHandle)
          this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
        IsolatedStorageFile.Reserve(this.m_handle, plQuota, plReserve, false);
      }
    }

    internal void Unreserve(ulong lFree)
    {
      if (this.IsRoaming())
        return;
      ulong quota = (ulong) this.Quota;
      this.Unreserve(lFree, quota);
    }

    [SecuritySafeCritical]
    internal void Unreserve(ulong lFree, ulong quota)
    {
      if (this.IsRoaming())
        return;
      ulong plReserve = lFree;
      lock (this.m_internalLock)
      {
        if (this.m_bDisposed)
          throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
        if (this.m_closed)
          throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
        if (this.InvalidFileHandle)
          this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
        IsolatedStorageFile.Reserve(this.m_handle, quota, plReserve, true);
      }
    }

    /// <summary>删除独立存储范围中的文件。</summary>
    /// <param name="file">要在独立存储范围中删除的文件的相对路径。</param>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">目标文件已打开或者路径不正确。</exception>
    /// <exception cref="T:System.ArgumentNullException">文件路径是 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public void DeleteFile(string file)
    {
      if (file == null)
        throw new ArgumentNullException("file");
      this.m_fiop.Assert();
      this.m_fiop.PermitOnly();
      bool locked = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.Lock(ref locked);
        long length;
        try
        {
          string fullPath = this.GetFullPath(file);
          length = LongPathFile.GetLength(fullPath);
          LongPathFile.Delete(fullPath);
        }
        catch
        {
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DeleteFile"));
        }
        this.Unreserve(IsolatedStorageFile.RoundToBlockSize((ulong) length));
      }
      finally
      {
        if (locked)
          this.Unlock();
      }
      CodeAccessPermission.RevertAll();
    }

    /// <summary>确定指定的路径是否指的是独立存储区中的现有文件。</summary>
    /// <returns>如果 <paramref name="path" /> 指的是独立存储区中的现有文件而不是 null，则为 true；否则为 false。</returns>
    /// <param name="path">要测试的路径和文件名。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">独立存储区已关闭。</exception>
    /// <exception cref="T:System.ObjectDisposedException">独立存储区已被释放。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">独立存储区已被移除。</exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public bool FileExists(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      if (this.m_closed)
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      this.m_fiop.Assert();
      this.m_fiop.PermitOnly();
      string path1 = LongPath.NormalizePath(this.GetFullPath(path));
      if (path.EndsWith(Path.DirectorySeparatorChar.ToString() + ".", StringComparison.Ordinal))
        path1 = !path1.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal) ? path1 + Path.DirectorySeparatorChar.ToString() + "." : path1 + ".";
      try
      {
        IsolatedStorageFile.Demand((CodeAccessPermission) new FileIOPermission(FileIOPermissionAccess.Read, new string[1]{ path1 }, 0 != 0, 0 != 0));
      }
      catch
      {
        return false;
      }
      int num = LongPathFile.Exists(path1) ? 1 : 0;
      CodeAccessPermission.RevertAll();
      return num != 0;
    }

    /// <summary>确定指定的路径是否指的是独立存储区中的现有目录。</summary>
    /// <returns>如果 <paramref name="path" /> 指的是独立存储区中的现有目录而不是 null，则为 true；否则为 false。</returns>
    /// <param name="path">要测试的路径。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">独立存储区已关闭。</exception>
    /// <exception cref="T:System.ObjectDisposedException">独立存储区已被释放。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">独立存储区已被移除。- 或 -独立存储被禁用。</exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public bool DirectoryExists(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      if (this.m_closed)
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      this.m_fiop.Assert();
      this.m_fiop.PermitOnly();
      string fullPath = this.GetFullPath(path);
      string path1 = LongPath.NormalizePath(fullPath);
      string str = Path.DirectorySeparatorChar.ToString() + ".";
      int num1 = 4;
      if (fullPath.EndsWith(str, (StringComparison) num1))
        path1 = !path1.EndsWith(Path.DirectorySeparatorChar) ? path1 + Path.DirectorySeparatorChar.ToString() + "." : path1 + ".";
      try
      {
        IsolatedStorageFile.Demand((CodeAccessPermission) new FileIOPermission(FileIOPermissionAccess.Read, new string[1]{ path1 }, 0 != 0, 0 != 0));
      }
      catch
      {
        return false;
      }
      int num2 = LongPathDirectory.Exists(path1) ? 1 : 0;
      CodeAccessPermission.RevertAll();
      return num2 != 0;
    }

    /// <summary>在独立存储范围中创建目录。</summary>
    /// <param name="dir">要在独立存储范围中创建的目录的相对路径。</param>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">当前代码没有足够的权限创建独立存储目录。</exception>
    /// <exception cref="T:System.ArgumentNullException">目录路径为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public void CreateDirectory(string dir)
    {
      if (dir == null)
        throw new ArgumentNullException("dir");
      this.m_fiop.Assert();
      this.m_fiop.PermitOnly();
      string fullPath1 = this.GetFullPath(dir);
      string fullPath2 = LongPath.NormalizePath(fullPath1);
      try
      {
        IsolatedStorageFile.Demand((CodeAccessPermission) new FileIOPermission(FileIOPermissionAccess.Read, new string[1]{ fullPath2 }, 0 != 0, 0 != 0));
      }
      catch
      {
        throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_CreateDirectory"));
      }
      string[] create = this.DirectoriesToCreate(fullPath2);
      if (create == null || create.Length == 0)
      {
        if (!LongPathDirectory.Exists(fullPath1))
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_CreateDirectory"));
      }
      else
      {
        this.Reserve(1024UL * (ulong) create.Length);
        try
        {
          string[] strArray = create;
          int index = strArray.Length - 1;
          LongPathDirectory.CreateDirectory(strArray[index]);
        }
        catch
        {
          this.Unreserve(1024UL * (ulong) create.Length);
          try
          {
            LongPathDirectory.Delete(create[0], true);
          }
          catch
          {
          }
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_CreateDirectory"));
        }
        CodeAccessPermission.RevertAll();
      }
    }

    /// <summary>返回指定文件或目录的创建日期和时间。</summary>
    /// <returns>指定的文件或目录的创建日期和时间。该值用本地时间表示。</returns>
    /// <param name="path">要获取其创建日期和时间信息的文件或目录的路径。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path " /> 是一个零长度字符串，仅包含空白或者包含 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法定义的一个或多个无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path " />为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">独立存储区已被关闭。</exception>
    /// <exception cref="T:System.ObjectDisposedException">独立存储区已被释放。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">独立存储区已被移除。- 或 -独立存储被禁用。</exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public DateTimeOffset GetCreationTime(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (path.Trim().Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "path");
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      if (this.m_closed)
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      this.m_fiop.Assert();
      this.m_fiop.PermitOnly();
      string path1 = LongPath.NormalizePath(this.GetFullPath(path));
      try
      {
        IsolatedStorageFile.Demand((CodeAccessPermission) new FileIOPermission(FileIOPermissionAccess.Read, new string[1]{ path1 }, 0 != 0, 0 != 0));
      }
      catch
      {
        DateTimeOffset dateTimeOffset = new DateTimeOffset(1601, 1, 1, 0, 0, 0, TimeSpan.Zero);
        dateTimeOffset = dateTimeOffset.ToLocalTime();
        return dateTimeOffset;
      }
      DateTimeOffset creationTime = LongPathFile.GetCreationTime(path1);
      CodeAccessPermission.RevertAll();
      return creationTime;
    }

    /// <summary>返回上次访问指定文件或目录的日期和时间。</summary>
    /// <returns>上次访问指定文件或目录的日期和时间。该值用本地时间表示。</returns>
    /// <param name="path">要获取其上次访问日期和时间信息的文件或目录的路径。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path " /> 是一个零长度字符串，仅包含空白或者包含 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法定义的一个或多个无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path " />为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">独立存储区已被关闭。</exception>
    /// <exception cref="T:System.ObjectDisposedException">独立存储区已被释放。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">独立存储区已被移除。- 或 -独立存储被禁用。</exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public DateTimeOffset GetLastAccessTime(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (path.Trim().Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "path");
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      if (this.m_closed)
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      this.m_fiop.Assert();
      this.m_fiop.PermitOnly();
      string path1 = LongPath.NormalizePath(this.GetFullPath(path));
      try
      {
        IsolatedStorageFile.Demand((CodeAccessPermission) new FileIOPermission(FileIOPermissionAccess.Read, new string[1]{ path1 }, 0 != 0, 0 != 0));
      }
      catch
      {
        DateTimeOffset dateTimeOffset = new DateTimeOffset(1601, 1, 1, 0, 0, 0, TimeSpan.Zero);
        dateTimeOffset = dateTimeOffset.ToLocalTime();
        return dateTimeOffset;
      }
      DateTimeOffset lastAccessTime = LongPathFile.GetLastAccessTime(path1);
      CodeAccessPermission.RevertAll();
      return lastAccessTime;
    }

    /// <summary>返回上次写入指定文件或目录的日期和时间。</summary>
    /// <returns>上次写入指定文件或目录的日期和时间。该值用本地时间表示。</returns>
    /// <param name="path">要获取其上次写入日期和时间信息的文件或目录的路径。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path " /> 是一个零长度字符串，仅包含空白或者包含 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法定义的一个或多个无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path " />为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">独立存储区已被关闭。</exception>
    /// <exception cref="T:System.ObjectDisposedException">独立存储区已被释放。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">独立存储区已被移除。- 或 -独立存储被禁用。</exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public DateTimeOffset GetLastWriteTime(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (path.Trim().Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "path");
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      if (this.m_closed)
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      this.m_fiop.Assert();
      this.m_fiop.PermitOnly();
      string path1 = LongPath.NormalizePath(this.GetFullPath(path));
      try
      {
        IsolatedStorageFile.Demand((CodeAccessPermission) new FileIOPermission(FileIOPermissionAccess.Read, new string[1]{ path1 }, 0 != 0, 0 != 0));
      }
      catch
      {
        DateTimeOffset dateTimeOffset = new DateTimeOffset(1601, 1, 1, 0, 0, 0, TimeSpan.Zero);
        dateTimeOffset = dateTimeOffset.ToLocalTime();
        return dateTimeOffset;
      }
      DateTimeOffset lastWriteTime = LongPathFile.GetLastWriteTime(path1);
      CodeAccessPermission.RevertAll();
      return lastWriteTime;
    }

    /// <summary>将现有文件复制到新文件。</summary>
    /// <param name="sourceFileName">要复制的文件的名称。</param>
    /// <param name="destinationFileName">目标文件的名称。它不能是一个目录或现有文件。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="sourceFileName " /> 或 <paramref name=" destinationFileName " /> 是一个零长度字符串，仅包含空白或者包含 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法定义的一个或多个无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sourceFileName " />或 <paramref name=" destinationFileName " />为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">独立存储区已被关闭。</exception>
    /// <exception cref="T:System.ObjectDisposedException">独立存储区已被释放。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="sourceFileName " />未找到。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="sourceFileName " />未找到。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">独立存储区已被移除。- 或 -独立存储被禁用。- 或 -已存在 <paramref name="destinationFileName" />。- 或 -出现 I/O 错误。</exception>
    [ComVisible(false)]
    public void CopyFile(string sourceFileName, string destinationFileName)
    {
      if (sourceFileName == null)
        throw new ArgumentNullException("sourceFileName");
      if (destinationFileName == null)
        throw new ArgumentNullException("destinationFileName");
      if (sourceFileName.Trim().Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "sourceFileName");
      if (destinationFileName.Trim().Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "destinationFileName");
      this.CopyFile(sourceFileName, destinationFileName, false);
    }

    /// <summary>将现有文件复制到新文件，还可以覆盖现有文件。</summary>
    /// <param name="sourceFileName">要复制的文件的名称。</param>
    /// <param name="destinationFileName">目标文件的名称。不能是目录。</param>
    /// <param name="overwrite">如果可以覆盖目标文件，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="sourceFileName " /> 或 <paramref name=" destinationFileName " /> 是一个零长度字符串，仅包含空白或者包含 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法定义的一个或多个无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sourceFileName " />或 <paramref name=" destinationFileName " />为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">独立存储区已被关闭。</exception>
    /// <exception cref="T:System.ObjectDisposedException">独立存储区已被释放。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="sourceFileName " />未找到。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="sourceFileName " />未找到。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">独立存储区已被移除。- 或 -独立存储被禁用。- 或 -出现 I/O 错误。</exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public void CopyFile(string sourceFileName, string destinationFileName, bool overwrite)
    {
      if (sourceFileName == null)
        throw new ArgumentNullException("sourceFileName");
      if (destinationFileName == null)
        throw new ArgumentNullException("destinationFileName");
      if (sourceFileName.Trim().Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "sourceFileName");
      if (destinationFileName.Trim().Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "destinationFileName");
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      if (this.m_closed)
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      this.m_fiop.Assert();
      this.m_fiop.PermitOnly();
      string str1 = LongPath.NormalizePath(this.GetFullPath(sourceFileName));
      string str2 = LongPath.NormalizePath(this.GetFullPath(destinationFileName));
      try
      {
        IsolatedStorageFile.Demand((CodeAccessPermission) new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, new string[1]{ str1 }, 0 != 0, 0 != 0));
        IsolatedStorageFile.Demand((CodeAccessPermission) new FileIOPermission(FileIOPermissionAccess.Write, new string[1]{ str2 }, 0 != 0, 0 != 0));
      }
      catch
      {
        throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
      }
      bool locked = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.Lock(ref locked);
        long length;
        try
        {
          length = LongPathFile.GetLength(str1);
        }
        catch (FileNotFoundException ex)
        {
          throw new FileNotFoundException(Environment.GetResourceString("IO.PathNotFound_Path", (object) sourceFileName));
        }
        catch (DirectoryNotFoundException ex)
        {
          throw new DirectoryNotFoundException(Environment.GetResourceString("IO.PathNotFound_Path", (object) sourceFileName));
        }
        catch
        {
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
        }
        long num = 0;
        if (LongPathFile.Exists(str2))
        {
          try
          {
            num = LongPathFile.GetLength(str2);
          }
          catch
          {
            throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
          }
        }
        if (num < length)
          this.Reserve(IsolatedStorageFile.RoundToBlockSize((ulong) (length - num)));
        try
        {
          LongPathFile.Copy(str1, str2, overwrite);
        }
        catch (FileNotFoundException ex)
        {
          if (num < length)
            this.Unreserve(IsolatedStorageFile.RoundToBlockSize((ulong) (length - num)));
          throw new FileNotFoundException(Environment.GetResourceString("IO.PathNotFound_Path", (object) sourceFileName));
        }
        catch
        {
          if (num < length)
            this.Unreserve(IsolatedStorageFile.RoundToBlockSize((ulong) (length - num)));
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
        }
        if (!(num > length & overwrite))
          return;
        this.Unreserve(IsolatedStorageFile.RoundToBlockSizeFloor((ulong) (num - length)));
      }
      finally
      {
        if (locked)
          this.Unlock();
      }
    }

    /// <summary>将指定文件移到新位置，还可以允许您指定新文件名。</summary>
    /// <param name="sourceFileName">要移动的文件的名称。</param>
    /// <param name="destinationFileName">指向文件的新位置的路径。如果包括文件名，则移动的文件将具有该名称。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="sourceFileName " /> 或 <paramref name=" destinationFileName " /> 是一个零长度字符串，仅包含空白或者包含 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法定义的一个或多个无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sourceFileName " />或 <paramref name=" destinationFileName " />为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">独立存储区已被关闭。</exception>
    /// <exception cref="T:System.ObjectDisposedException">独立存储区已被释放。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="sourceFileName" />。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">独立存储区已被移除。- 或 -独立存储被禁用。</exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public void MoveFile(string sourceFileName, string destinationFileName)
    {
      if (sourceFileName == null)
        throw new ArgumentNullException("sourceFileName");
      if (destinationFileName == null)
        throw new ArgumentNullException("destinationFileName");
      if (sourceFileName.Trim().Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "sourceFileName");
      if (destinationFileName.Trim().Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "destinationFileName");
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      if (this.m_closed)
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      this.m_fiop.Assert();
      this.m_fiop.PermitOnly();
      string sourceFileName1 = LongPath.NormalizePath(this.GetFullPath(sourceFileName));
      string destFileName = LongPath.NormalizePath(this.GetFullPath(destinationFileName));
      try
      {
        IsolatedStorageFile.Demand((CodeAccessPermission) new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, new string[1]
        {
          sourceFileName1
        }, 0 != 0, 0 != 0));
        IsolatedStorageFile.Demand((CodeAccessPermission) new FileIOPermission(FileIOPermissionAccess.Write, new string[1]{ destFileName }, 0 != 0, 0 != 0));
      }
      catch
      {
        throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
      }
      try
      {
        LongPathFile.Move(sourceFileName1, destFileName);
      }
      catch (FileNotFoundException ex)
      {
        throw new FileNotFoundException(Environment.GetResourceString("IO.PathNotFound_Path", (object) sourceFileName));
      }
      catch
      {
        throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
      }
      CodeAccessPermission.RevertAll();
    }

    /// <summary>将指定的目录及其内容移到新位置。</summary>
    /// <param name="sourceDirectoryName">要移动的目录的名称。</param>
    /// <param name="destinationDirectoryName">指向 <paramref name="sourceDirectoryName" /> 的新位置的路径。这不能是现有目录的路径。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="sourceFileName " /> 或 <paramref name=" destinationFileName " /> 是一个零长度字符串，仅包含空白或者包含 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法定义的一个或多个无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sourceFileName " />或 <paramref name=" destinationFileName " />为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">独立存储区已被关闭。</exception>
    /// <exception cref="T:System.ObjectDisposedException">独立存储区已被释放。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="sourceDirectoryName" />。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">独立存储区已被移除。- 或 -独立存储被禁用。- 或 -<paramref name="destinationDirectoryName" /> 已存在。- 或 -<paramref name="sourceDirectoryName" /> 和 <paramref name="destinationDirectoryName" /> 引用相同的目录。</exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public void MoveDirectory(string sourceDirectoryName, string destinationDirectoryName)
    {
      if (sourceDirectoryName == null)
        throw new ArgumentNullException("sourceDirectoryName");
      if (destinationDirectoryName == null)
        throw new ArgumentNullException("destinationDirectoryName");
      if (sourceDirectoryName.Trim().Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "sourceDirectoryName");
      if (destinationDirectoryName.Trim().Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "destinationDirectoryName");
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      if (this.m_closed)
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      this.m_fiop.Assert();
      this.m_fiop.PermitOnly();
      string sourceDirName = LongPath.NormalizePath(this.GetFullPath(sourceDirectoryName));
      string destDirName = LongPath.NormalizePath(this.GetFullPath(destinationDirectoryName));
      try
      {
        IsolatedStorageFile.Demand((CodeAccessPermission) new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, new string[1]{ sourceDirName }, 0 != 0, 0 != 0));
        IsolatedStorageFile.Demand((CodeAccessPermission) new FileIOPermission(FileIOPermissionAccess.Write, new string[1]{ destDirName }, 0 != 0, 0 != 0));
      }
      catch
      {
        throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
      }
      try
      {
        LongPathDirectory.Move(sourceDirName, destDirName);
      }
      catch (DirectoryNotFoundException ex)
      {
        throw new DirectoryNotFoundException(Environment.GetResourceString("IO.PathNotFound_Path", (object) sourceDirectoryName));
      }
      catch
      {
        throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
      }
      CodeAccessPermission.RevertAll();
    }

    [SecurityCritical]
    private string[] DirectoriesToCreate(string fullPath)
    {
      List<string> stringList = new List<string>();
      int length = fullPath.Length;
      if (length >= 2 && (int) fullPath[length - 1] == (int) this.SeparatorExternal)
        --length;
      int rootLength = LongPath.GetRootLength(fullPath);
      while (rootLength < length)
      {
        ++rootLength;
        while (rootLength < length && (int) fullPath[rootLength] != (int) this.SeparatorExternal)
          ++rootLength;
        string path = fullPath.Substring(0, rootLength);
        if (!LongPathDirectory.InternalExists(path))
          stringList.Add(path);
      }
      if (stringList.Count != 0)
        return stringList.ToArray();
      return (string[]) null;
    }

    /// <summary>删除独立存储范围中的目录。</summary>
    /// <param name="dir">要在独立存储范围中删除的目录的相对路径。</param>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">该目录未能删除。</exception>
    /// <exception cref="T:System.ArgumentNullException">目录路径是 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public void DeleteDirectory(string dir)
    {
      if (dir == null)
        throw new ArgumentNullException("dir");
      this.m_fiop.Assert();
      this.m_fiop.PermitOnly();
      bool locked = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.Lock(ref locked);
        try
        {
          string path = LongPath.NormalizePath(this.GetFullPath(dir));
          string str = LongPath.NormalizePath(this.GetFullPath("."));
          int num1 = 4;
          if (path.Equals(str, (StringComparison) num1))
            throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DeleteDirectory"));
          int num2 = 0;
          LongPathDirectory.Delete(path, num2 != 0);
        }
        catch
        {
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DeleteDirectory"));
        }
        this.Unreserve(1024UL);
      }
      finally
      {
        if (locked)
          this.Unlock();
      }
      CodeAccessPermission.RevertAll();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void Demand(CodeAccessPermission permission)
    {
      permission.Demand();
    }

    /// <summary>枚举独立存储根处的文件名。</summary>
    /// <returns>独立存储根处文件的相对路径的数组。零长度数组指定根处没有任何文件。</returns>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">独立存储区已被移除。</exception>
    /// <exception cref="T:System.ObjectDisposedException">独立存储区已被释放。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">无法确定来自独立存储区根目录的文件路径。</exception>
    [ComVisible(false)]
    public string[] GetFileNames()
    {
      return this.GetFileNames("*");
    }

    /// <summary>获取与搜索模式匹配的文件名。</summary>
    /// <returns>独立存储范围中与 <paramref name="searchPattern" /> 匹配的文件的相对路径的数组。零长度数组指定没有任何匹配的文件。</returns>
    /// <param name="searchPattern">搜索模式。单字符（"?"）和多字符（"*"）通配符都受支持。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="searchPattern" /> 为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">独立存储区已被释放。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">独立存储区已被移除。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">无法找到 <paramref name="searchPattern" /> 指定的文件路径。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public string[] GetFileNames(string searchPattern)
    {
      if (searchPattern == null)
        throw new ArgumentNullException("searchPattern");
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      if (this.m_closed)
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      this.m_fiop.Assert();
      this.m_fiop.PermitOnly();
      string[] fileDirectoryNames = IsolatedStorageFile.GetFileDirectoryNames(this.GetFullPath(searchPattern), searchPattern, true);
      CodeAccessPermission.RevertAll();
      return fileDirectoryNames;
    }

    /// <summary>枚举独立存储根处的目录。</summary>
    /// <returns>独立存储根处目录的相对路径的数组。零长度数组指定根处没有任何目录。</returns>
    /// <exception cref="T:System.ObjectDisposedException">独立存储区已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">独立存储区已关闭。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">独立存储区已被移除。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方不具有枚举目录的权限。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">未找到一个或多个目录。</exception>
    [ComVisible(false)]
    public string[] GetDirectoryNames()
    {
      return this.GetDirectoryNames("*");
    }

    /// <summary>枚举独立存储范围中与给定搜索模式匹配的目录。</summary>
    /// <returns>独立存储范围中与 <paramref name="searchPattern" /> 匹配的目录的相对路径的数组。零长度数组指定没有任何匹配的目录。</returns>
    /// <param name="searchPattern">搜索模式。单字符（"?"）和多字符（"*"）通配符都受支持。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="searchPattern" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">独立存储区已关闭。</exception>
    /// <exception cref="T:System.ObjectDisposedException">独立存储区已被释放。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方不具有枚举从 <paramref name="searchPattern" /> 解析的目录的权限。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">未找到 <paramref name="searchPattern" /> 指定的一个或多个目录。</exception>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">独立存储区已被移除。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public string[] GetDirectoryNames(string searchPattern)
    {
      if (searchPattern == null)
        throw new ArgumentNullException("searchPattern");
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      if (this.m_closed)
        throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
      this.m_fiop.Assert();
      this.m_fiop.PermitOnly();
      string[] fileDirectoryNames = IsolatedStorageFile.GetFileDirectoryNames(this.GetFullPath(searchPattern), searchPattern, false);
      CodeAccessPermission.RevertAll();
      return fileDirectoryNames;
    }

    private static string NormalizeSearchPattern(string searchPattern)
    {
      string searchPattern1 = searchPattern.TrimEnd(Path.TrimEndChars);
      Path.CheckSearchPattern(searchPattern1);
      return searchPattern1;
    }

    /// <summary>在指定的模式中打开文件。</summary>
    /// <returns>在指定模式下打开、具有读/写访问权限且不共享的文件。</returns>
    /// <param name="path">独立存储区内文件的相对路径。</param>
    /// <param name="mode">指定如何打开文件的枚举值之一。</param>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">独立存储区已被移除。- 或 -独立存储被禁用。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 的格式不正确。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="path" /> 中的目录不存在。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到文件，而 <paramref name="mode" /> 设置为 <see cref="F:System.IO.FileMode.Open" />。</exception>
    /// <exception cref="T:System.ObjectDisposedException">独立存储区已被释放。</exception>
    [ComVisible(false)]
    public IsolatedStorageFileStream OpenFile(string path, FileMode mode)
    {
      return new IsolatedStorageFileStream(path, mode, this);
    }

    /// <summary>用指定的读/写访问权限在指定模式下打开文件。</summary>
    /// <returns>用指定模式和访问权限打开且不共享的文件。</returns>
    /// <param name="path">独立存储区内文件的相对路径。</param>
    /// <param name="mode">指定如何打开文件的枚举值之一。</param>
    /// <param name="access">指定是否将用读、写或读/写访问权限打开文件的枚举值之一。</param>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">独立存储区已被移除。- 或 -独立存储被禁用。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 的格式不正确。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="path" /> 中的目录不存在。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到文件，而 <paramref name="mode" /> 设置为 <see cref="F:System.IO.FileMode.Open" />。</exception>
    /// <exception cref="T:System.ObjectDisposedException">独立存储区已被释放。</exception>
    [ComVisible(false)]
    public IsolatedStorageFileStream OpenFile(string path, FileMode mode, FileAccess access)
    {
      return new IsolatedStorageFileStream(path, mode, access, this);
    }

    /// <summary>用指定的读/写访问权限和共享权限在指定的模式下打开文件。</summary>
    /// <returns>用指定的模式和访问权限以及指定的共享选项打开的文件。</returns>
    /// <param name="path">独立存储区内文件的相对路径。</param>
    /// <param name="mode">指定如何打开或创建文件的枚举值之一。</param>
    /// <param name="access">指定是否将用读、写或读/写访问权限打开文件的枚举值之一</param>
    /// <param name="share">用于指定其他 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFileStream" /> 对象对此文件具有哪种访问权限的枚举值的按位组合。</param>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">独立存储区已被移除。- 或 -独立存储被禁用。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 的格式不正确。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="path" /> 中的目录不存在。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到文件，而 <paramref name="mode" /> 设置为 <see cref="M:System.IO.FileInfo.Open(System.IO.FileMode)" />。</exception>
    /// <exception cref="T:System.ObjectDisposedException">独立存储区已被释放。</exception>
    [ComVisible(false)]
    public IsolatedStorageFileStream OpenFile(string path, FileMode mode, FileAccess access, FileShare share)
    {
      return new IsolatedStorageFileStream(path, mode, access, share, this);
    }

    /// <summary>在独立存储区中创建文件。</summary>
    /// <returns>新的独立存储文件。</returns>
    /// <param name="path">要创建的文件的相对路径。</param>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">独立存储区已被移除。- 或 -独立存储被禁用。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 的格式不正确。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="path" /> 中的目录不存在。</exception>
    /// <exception cref="T:System.ObjectDisposedException">独立存储区已被释放。</exception>
    [ComVisible(false)]
    public IsolatedStorageFileStream CreateFile(string path)
    {
      return new IsolatedStorageFileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, this);
    }

    /// <summary>移除独立存储区范围及其所有内容。</summary>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">无法删除独立存储区。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override void Remove()
    {
      string str = (string) null;
      this.RemoveLogicalDir();
      this.Close();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(IsolatedStorageFile.GetRootDir(this.Scope));
      if (this.IsApp())
      {
        stringBuilder.Append(this.AppName);
        stringBuilder.Append(this.SeparatorExternal);
      }
      else
      {
        if (this.IsDomain())
        {
          stringBuilder.Append(this.DomainName);
          stringBuilder.Append(this.SeparatorExternal);
          str = stringBuilder.ToString();
        }
        stringBuilder.Append(this.AssemName);
        stringBuilder.Append(this.SeparatorExternal);
      }
      string @string = stringBuilder.ToString();
      new FileIOPermission(FileIOPermissionAccess.AllAccess, @string).Assert();
      if (this.ContainsUnknownFiles(@string))
        return;
      try
      {
        LongPathDirectory.Delete(@string, true);
      }
      catch
      {
        return;
      }
      if (!this.IsDomain())
        return;
      CodeAccessPermission.RevertAssert();
      new FileIOPermission(FileIOPermissionAccess.AllAccess, str).Assert();
      if (this.ContainsUnknownFiles(str))
        return;
      try
      {
        LongPathDirectory.Delete(str, true);
      }
      catch
      {
      }
    }

    [SecuritySafeCritical]
    private void RemoveLogicalDir()
    {
      this.m_fiop.Assert();
      bool locked = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.Lock(ref locked);
        if (!Directory.Exists(this.RootDirectory))
          return;
        ulong lFree = this.IsRoaming() ? 0UL : (ulong) (this.Quota - this.AvailableFreeSpace);
        ulong quota = (ulong) this.Quota;
        try
        {
          LongPathDirectory.Delete(this.RootDirectory, true);
        }
        catch
        {
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DeleteDirectories"));
        }
        this.Unreserve(lFree, quota);
      }
      finally
      {
        if (locked)
          this.Unlock();
      }
    }

    private bool ContainsUnknownFiles(string rootDir)
    {
      string[] fileDirectoryNames1;
      string[] fileDirectoryNames2;
      try
      {
        fileDirectoryNames1 = IsolatedStorageFile.GetFileDirectoryNames(rootDir + "*", "*", true);
        fileDirectoryNames2 = IsolatedStorageFile.GetFileDirectoryNames(rootDir + "*", "*", false);
      }
      catch
      {
        throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DeleteDirectories"));
      }
      if (fileDirectoryNames2 != null && fileDirectoryNames2.Length != 0)
      {
        if (fileDirectoryNames2.Length > 1)
          return true;
        if (this.IsApp())
        {
          if (IsolatedStorageFile.NotAppFilesDir(fileDirectoryNames2[0]))
            return true;
        }
        else if (this.IsDomain())
        {
          if (IsolatedStorageFile.NotFilesDir(fileDirectoryNames2[0]))
            return true;
        }
        else if (IsolatedStorageFile.NotAssemFilesDir(fileDirectoryNames2[0]))
          return true;
      }
      if (fileDirectoryNames1 == null || fileDirectoryNames1.Length == 0)
        return false;
      if (this.IsRoaming())
        return fileDirectoryNames1.Length > 1 || IsolatedStorageFile.NotIDFile(fileDirectoryNames1[0]);
      return fileDirectoryNames1.Length > 2 || IsolatedStorageFile.NotIDFile(fileDirectoryNames1[0]) && IsolatedStorageFile.NotInfoFile(fileDirectoryNames1[0]) || fileDirectoryNames1.Length == 2 && IsolatedStorageFile.NotIDFile(fileDirectoryNames1[1]) && IsolatedStorageFile.NotInfoFile(fileDirectoryNames1[1]);
    }

    /// <summary>关闭以前用 <see cref="M:System.IO.IsolatedStorage.IsolatedStorageFile.GetStore(System.IO.IsolatedStorage.IsolatedStorageScope,System.Type,System.Type)" />、<see cref="M:System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForAssembly" /> 或 <see cref="M:System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForDomain" /> 打开的存储区。</summary>
    [SecuritySafeCritical]
    public void Close()
    {
      if (this.IsRoaming())
        return;
      lock (this.m_internalLock)
      {
        if (this.m_closed)
          return;
        this.m_closed = true;
        if (this.m_handle != null)
          this.m_handle.Dispose();
        GC.SuppressFinalize((object) this);
      }
    }

    /// <summary>释放由 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" /> 使用的所有资源。</summary>
    public void Dispose()
    {
      this.Close();
      this.m_bDisposed = true;
    }

    private static bool NotIDFile(string file)
    {
      return (uint) string.Compare(file, "identity.dat", StringComparison.Ordinal) > 0U;
    }

    private static bool NotInfoFile(string file)
    {
      if (string.Compare(file, "info.dat", StringComparison.Ordinal) != 0)
        return (uint) string.Compare(file, "appinfo.dat", StringComparison.Ordinal) > 0U;
      return false;
    }

    private static bool NotFilesDir(string dir)
    {
      return (uint) string.Compare(dir, "Files", StringComparison.Ordinal) > 0U;
    }

    internal static bool NotAssemFilesDir(string dir)
    {
      return (uint) string.Compare(dir, "AssemFiles", StringComparison.Ordinal) > 0U;
    }

    internal static bool NotAppFilesDir(string dir)
    {
      return (uint) string.Compare(dir, "AppFiles", StringComparison.Ordinal) > 0U;
    }

    /// <summary>为所有标识移除指定的独立存储范围。</summary>
    /// <param name="scope">
    /// <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> 值的按位组合。</param>
    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">无法移除独立存储区。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.IsolatedStorageFilePermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static void Remove(IsolatedStorageScope scope)
    {
      IsolatedStorageFile.VerifyGlobalScope(scope);
      IsolatedStorageFile.DemandAdminPermission();
      string rootDir = IsolatedStorageFile.GetRootDir(scope);
      new FileIOPermission(FileIOPermissionAccess.Write, rootDir).Assert();
      try
      {
        LongPathDirectory.Delete(rootDir, true);
        LongPathDirectory.CreateDirectory(rootDir);
      }
      catch
      {
        throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DeleteDirectories"));
      }
    }

    /// <summary>获取独立存储范围中 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" /> 存储区的枚举器。</summary>
    /// <returns>指定独立存储范围中 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageFile" /> 存储区的枚举器。</returns>
    /// <param name="scope">表示为其返回独立存储区的 <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" />。User 和 User|Roaming 是仅有的受支持的 IsolatedStorageScope 组合。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.IsolatedStorageFilePermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static IEnumerator GetEnumerator(IsolatedStorageScope scope)
    {
      IsolatedStorageFile.VerifyGlobalScope(scope);
      IsolatedStorageFile.DemandAdminPermission();
      return (IEnumerator) new IsolatedStorageFileEnumerator(scope);
    }

    internal string GetFullPath(string path)
    {
      if (path == string.Empty)
        return this.RootDirectory;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(this.RootDirectory);
      if ((int) path[0] == (int) this.SeparatorExternal)
        stringBuilder.Append(path.Substring(1));
      else
        stringBuilder.Append(path);
      return stringBuilder.ToString();
    }

    [SecurityCritical]
    [SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
    private static string GetDataDirectoryFromActivationContext()
    {
      if (IsolatedStorageFile.s_appDataDir == null)
      {
        ActivationContext activationContext = AppDomain.CurrentDomain.ActivationContext;
        if (activationContext == null)
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_ApplicationMissingIdentity"));
        string dataDirectory = activationContext.DataDirectory;
        if (dataDirectory != null)
        {
          string str = dataDirectory;
          int index = str.Length - 1;
          if ((int) str[index] != 92)
            dataDirectory += "\\";
        }
        IsolatedStorageFile.s_appDataDir = dataDirectory;
      }
      return IsolatedStorageFile.s_appDataDir;
    }

    [SecuritySafeCritical]
    internal void Init(IsolatedStorageScope scope)
    {
      IsolatedStorageFile.GetGlobalFileIOPerm(scope).Assert();
      this.m_StoreScope = scope;
      StringBuilder stringBuilder = new StringBuilder();
      if (System.IO.IsolatedStorage.IsolatedStorage.IsApp(scope))
      {
        stringBuilder.Append(IsolatedStorageFile.GetRootDir(scope));
        if (IsolatedStorageFile.s_appDataDir == null)
        {
          stringBuilder.Append(this.AppName);
          stringBuilder.Append(this.SeparatorExternal);
        }
        try
        {
          LongPathDirectory.CreateDirectory(stringBuilder.ToString());
        }
        catch
        {
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
        }
        this.CreateIDFile(stringBuilder.ToString(), IsolatedStorageScope.Application);
        this.m_InfoFile = stringBuilder.ToString() + "appinfo.dat";
        stringBuilder.Append("AppFiles");
      }
      else
      {
        stringBuilder.Append(IsolatedStorageFile.GetRootDir(scope));
        if (System.IO.IsolatedStorage.IsolatedStorage.IsDomain(scope))
        {
          stringBuilder.Append(this.DomainName);
          stringBuilder.Append(this.SeparatorExternal);
          try
          {
            LongPathDirectory.CreateDirectory(stringBuilder.ToString());
            this.CreateIDFile(stringBuilder.ToString(), IsolatedStorageScope.Domain);
          }
          catch
          {
            throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
          }
          this.m_InfoFile = stringBuilder.ToString() + "info.dat";
        }
        stringBuilder.Append(this.AssemName);
        stringBuilder.Append(this.SeparatorExternal);
        try
        {
          LongPathDirectory.CreateDirectory(stringBuilder.ToString());
          this.CreateIDFile(stringBuilder.ToString(), IsolatedStorageScope.Assembly);
        }
        catch
        {
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
        }
        if (System.IO.IsolatedStorage.IsolatedStorage.IsDomain(scope))
        {
          stringBuilder.Append("Files");
        }
        else
        {
          this.m_InfoFile = stringBuilder.ToString() + "info.dat";
          stringBuilder.Append("AssemFiles");
        }
      }
      stringBuilder.Append(this.SeparatorExternal);
      string @string = stringBuilder.ToString();
      try
      {
        LongPathDirectory.CreateDirectory(@string);
      }
      catch
      {
        throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
      }
      this.m_RootDir = @string;
      this.m_fiop = new FileIOPermission(FileIOPermissionAccess.AllAccess, @string);
      if (scope != (IsolatedStorageScope.User | IsolatedStorageScope.Application))
        return;
      this.UpdateQuotaFromInfoFile();
    }

    [SecurityCritical]
    private void UpdateQuotaFromInfoFile()
    {
      bool locked = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        this.Lock(ref locked);
        lock (this.m_internalLock)
        {
          if (this.InvalidFileHandle)
            this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
          long local_3 = 0;
          if (!IsolatedStorageFile.GetQuota(this.m_handle, out local_3))
            return;
          base.Quota = local_3;
        }
      }
      finally
      {
        if (locked)
          this.Unlock();
      }
    }

    [SecuritySafeCritical]
    internal bool InitExistingStore(IsolatedStorageScope scope)
    {
      StringBuilder stringBuilder = new StringBuilder();
      this.m_StoreScope = scope;
      stringBuilder.Append(IsolatedStorageFile.GetRootDir(scope));
      if (System.IO.IsolatedStorage.IsolatedStorage.IsApp(scope))
      {
        stringBuilder.Append(this.AppName);
        stringBuilder.Append(this.SeparatorExternal);
        this.m_InfoFile = stringBuilder.ToString() + "appinfo.dat";
        stringBuilder.Append("AppFiles");
      }
      else
      {
        if (System.IO.IsolatedStorage.IsolatedStorage.IsDomain(scope))
        {
          stringBuilder.Append(this.DomainName);
          stringBuilder.Append(this.SeparatorExternal);
          this.m_InfoFile = stringBuilder.ToString() + "info.dat";
        }
        stringBuilder.Append(this.AssemName);
        stringBuilder.Append(this.SeparatorExternal);
        if (System.IO.IsolatedStorage.IsolatedStorage.IsDomain(scope))
        {
          stringBuilder.Append("Files");
        }
        else
        {
          this.m_InfoFile = stringBuilder.ToString() + "info.dat";
          stringBuilder.Append("AssemFiles");
        }
      }
      stringBuilder.Append(this.SeparatorExternal);
      FileIOPermission fileIoPermission = new FileIOPermission(FileIOPermissionAccess.AllAccess, stringBuilder.ToString());
      fileIoPermission.Assert();
      if (!LongPathDirectory.Exists(stringBuilder.ToString()))
        return false;
      this.m_RootDir = stringBuilder.ToString();
      this.m_fiop = fileIoPermission;
      if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Application))
        this.UpdateQuotaFromInfoFile();
      return true;
    }

    protected override IsolatedStoragePermission GetPermission(PermissionSet ps)
    {
      if (ps == null)
        return (IsolatedStoragePermission) null;
      if (ps.IsUnrestricted())
        return (IsolatedStoragePermission) new IsolatedStorageFilePermission(PermissionState.Unrestricted);
      return (IsolatedStoragePermission) ps.GetPermission(typeof (IsolatedStorageFilePermission));
    }

    internal void UndoReserveOperation(ulong oldLen, ulong newLen)
    {
      oldLen = IsolatedStorageFile.RoundToBlockSize(oldLen);
      if (newLen <= oldLen)
        return;
      this.Unreserve(IsolatedStorageFile.RoundToBlockSize(newLen - oldLen));
    }

    internal void Reserve(ulong oldLen, ulong newLen)
    {
      oldLen = IsolatedStorageFile.RoundToBlockSize(oldLen);
      if (newLen <= oldLen)
        return;
      this.Reserve(IsolatedStorageFile.RoundToBlockSize(newLen - oldLen));
    }

    internal void ReserveOneBlock()
    {
      this.Reserve(1024UL);
    }

    internal void UnreserveOneBlock()
    {
      this.Unreserve(1024UL);
    }

    internal static ulong RoundToBlockSize(ulong num)
    {
      if (num < 1024UL)
        return 1024;
      ulong num1 = num % 1024UL;
      if ((long) num1 != 0L)
        num += 1024UL - num1;
      return num;
    }

    internal static ulong RoundToBlockSizeFloor(ulong num)
    {
      if (num < 1024UL)
        return 0;
      ulong num1 = num % 1024UL;
      num -= num1;
      return num;
    }

    [SecurityCritical]
    internal static string GetRootDir(IsolatedStorageScope scope)
    {
      if (System.IO.IsolatedStorage.IsolatedStorage.IsRoaming(scope))
      {
        if (IsolatedStorageFile.s_RootDirRoaming == null)
        {
          string s = (string) null;
          IsolatedStorageFile.GetRootDir(scope, JitHelpers.GetStringHandleOnStack(ref s));
          IsolatedStorageFile.s_RootDirRoaming = s;
        }
        return IsolatedStorageFile.s_RootDirRoaming;
      }
      if (System.IO.IsolatedStorage.IsolatedStorage.IsMachine(scope))
      {
        if (IsolatedStorageFile.s_RootDirMachine == null)
          IsolatedStorageFile.InitGlobalsMachine(scope);
        return IsolatedStorageFile.s_RootDirMachine;
      }
      if (IsolatedStorageFile.s_RootDirUser == null)
        IsolatedStorageFile.InitGlobalsNonRoamingUser(scope);
      return IsolatedStorageFile.s_RootDirUser;
    }

    [SecuritySafeCritical]
    [HandleProcessCorruptedStateExceptions]
    private static void InitGlobalsMachine(IsolatedStorageScope scope)
    {
      string s = (string) null;
      IsolatedStorageFile.GetRootDir(scope, JitHelpers.GetStringHandleOnStack(ref s));
      new FileIOPermission(FileIOPermissionAccess.AllAccess, s).Assert();
      string str = IsolatedStorageFile.GetMachineRandomDirectory(s);
      if (str == null)
      {
        Mutex mutexNotOwned = IsolatedStorageFile.CreateMutexNotOwned(s);
        if (!mutexNotOwned.WaitOne())
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
        try
        {
          str = IsolatedStorageFile.GetMachineRandomDirectory(s);
          if (str == null)
          {
            string randomFileName1 = Path.GetRandomFileName();
            string randomFileName2 = Path.GetRandomFileName();
            try
            {
              IsolatedStorageFile.CreateDirectoryWithDacl(s + randomFileName1);
              IsolatedStorageFile.CreateDirectoryWithDacl(s + randomFileName1 + "\\" + randomFileName2);
            }
            catch
            {
              throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
            }
            str = randomFileName1 + "\\" + randomFileName2;
          }
        }
        finally
        {
          mutexNotOwned.ReleaseMutex();
        }
      }
      IsolatedStorageFile.s_RootDirMachine = s + str + "\\";
    }

    [SecuritySafeCritical]
    private static void InitGlobalsNonRoamingUser(IsolatedStorageScope scope)
    {
      string s = (string) null;
      if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Application))
      {
        s = IsolatedStorageFile.GetDataDirectoryFromActivationContext();
        if (s != null)
        {
          IsolatedStorageFile.s_RootDirUser = s;
          return;
        }
      }
      IsolatedStorageFile.GetRootDir(scope, JitHelpers.GetStringHandleOnStack(ref s));
      new FileIOPermission(FileIOPermissionAccess.AllAccess, s).Assert();
      bool bMigrateNeeded = false;
      string sOldStoreLocation = (string) null;
      string str = IsolatedStorageFile.GetRandomDirectory(s, out bMigrateNeeded, out sOldStoreLocation);
      if (str == null)
      {
        Mutex mutexNotOwned = IsolatedStorageFile.CreateMutexNotOwned(s);
        if (!mutexNotOwned.WaitOne())
          throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
        try
        {
          str = IsolatedStorageFile.GetRandomDirectory(s, out bMigrateNeeded, out sOldStoreLocation) ?? (!bMigrateNeeded ? IsolatedStorageFile.CreateRandomDirectory(s) : IsolatedStorageFile.MigrateOldIsoStoreDirectory(s, sOldStoreLocation));
        }
        finally
        {
          mutexNotOwned.ReleaseMutex();
        }
      }
      IsolatedStorageFile.s_RootDirUser = s + str + "\\";
    }

    [SecuritySafeCritical]
    [HandleProcessCorruptedStateExceptions]
    internal static string MigrateOldIsoStoreDirectory(string rootDir, string oldRandomDirectory)
    {
      string randomFileName1 = Path.GetRandomFileName();
      string randomFileName2 = Path.GetRandomFileName();
      string path = rootDir + randomFileName1;
      string destDirName = path + "\\" + randomFileName2;
      try
      {
        LongPathDirectory.CreateDirectory(path);
        LongPathDirectory.Move(rootDir + oldRandomDirectory, destDirName);
      }
      catch
      {
        throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
      }
      return randomFileName1 + "\\" + randomFileName2;
    }

    [SecuritySafeCritical]
    [HandleProcessCorruptedStateExceptions]
    internal static string CreateRandomDirectory(string rootDir)
    {
      string str;
      string path;
      do
      {
        str = Path.GetRandomFileName() + "\\" + Path.GetRandomFileName();
        path = rootDir + str;
      }
      while (LongPathDirectory.Exists(path));
      try
      {
        LongPathDirectory.CreateDirectory(path);
      }
      catch
      {
        throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Init"));
      }
      return str;
    }

    internal static string GetRandomDirectory(string rootDir, out bool bMigrateNeeded, out string sOldStoreLocation)
    {
      bMigrateNeeded = false;
      sOldStoreLocation = (string) null;
      string[] fileDirectoryNames1 = IsolatedStorageFile.GetFileDirectoryNames(rootDir + "*", "*", false);
      for (int index1 = 0; index1 < fileDirectoryNames1.Length; ++index1)
      {
        if (fileDirectoryNames1[index1].Length == 12)
        {
          string[] fileDirectoryNames2 = IsolatedStorageFile.GetFileDirectoryNames(rootDir + fileDirectoryNames1[index1] + "\\*", "*", false);
          for (int index2 = 0; index2 < fileDirectoryNames2.Length; ++index2)
          {
            if (fileDirectoryNames2[index2].Length == 12)
              return fileDirectoryNames1[index1] + "\\" + fileDirectoryNames2[index2];
          }
        }
      }
      for (int index = 0; index < fileDirectoryNames1.Length; ++index)
      {
        if (fileDirectoryNames1[index].Length == 24)
        {
          bMigrateNeeded = true;
          sOldStoreLocation = fileDirectoryNames1[index];
          return (string) null;
        }
      }
      return (string) null;
    }

    internal static string GetMachineRandomDirectory(string rootDir)
    {
      string[] fileDirectoryNames1 = IsolatedStorageFile.GetFileDirectoryNames(rootDir + "*", "*", false);
      for (int index1 = 0; index1 < fileDirectoryNames1.Length; ++index1)
      {
        if (fileDirectoryNames1[index1].Length == 12)
        {
          string[] fileDirectoryNames2 = IsolatedStorageFile.GetFileDirectoryNames(rootDir + fileDirectoryNames1[index1] + "\\*", "*", false);
          for (int index2 = 0; index2 < fileDirectoryNames2.Length; ++index2)
          {
            if (fileDirectoryNames2[index2].Length == 12)
              return fileDirectoryNames1[index1] + "\\" + fileDirectoryNames2[index2];
          }
        }
      }
      return (string) null;
    }

    [SecurityCritical]
    internal static Mutex CreateMutexNotOwned(string pathName)
    {
      return new Mutex(false, "Global\\" + IsolatedStorageFile.GetStrongHashSuitableForObjectName(pathName));
    }

    internal static string GetStrongHashSuitableForObjectName(string name)
    {
      MemoryStream memoryStream = new MemoryStream();
      new BinaryWriter((Stream) memoryStream).Write(name.ToUpper(CultureInfo.InvariantCulture));
      memoryStream.Position = 0L;
      return Path.ToBase32StringSuitableForDirName(new SHA1CryptoServiceProvider().ComputeHash((Stream) memoryStream));
    }

    private string GetSyncObjectName()
    {
      if (this.m_SyncObjectName == null)
        this.m_SyncObjectName = IsolatedStorageFile.GetStrongHashSuitableForObjectName(this.m_InfoFile);
      return this.m_SyncObjectName;
    }

    [SecuritySafeCritical]
    internal void Lock(ref bool locked)
    {
      locked = false;
      if (this.IsRoaming())
        return;
      lock (this.m_internalLock)
      {
        if (this.m_bDisposed)
          throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
        if (this.m_closed)
          throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
        if (this.InvalidFileHandle)
          this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
        locked = IsolatedStorageFile.Lock(this.m_handle, true);
      }
    }

    [SecuritySafeCritical]
    internal void Unlock()
    {
      if (this.IsRoaming())
        return;
      lock (this.m_internalLock)
      {
        if (this.m_bDisposed)
          throw new ObjectDisposedException((string) null, Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
        if (this.m_closed)
          throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_StoreNotOpen"));
        if (this.InvalidFileHandle)
          this.m_handle = IsolatedStorageFile.Open(this.m_InfoFile, this.GetSyncObjectName());
        IsolatedStorageFile.Lock(this.m_handle, false);
      }
    }

    [SecurityCritical]
    internal static FileIOPermission GetGlobalFileIOPerm(IsolatedStorageScope scope)
    {
      if (System.IO.IsolatedStorage.IsolatedStorage.IsRoaming(scope))
      {
        if (IsolatedStorageFile.s_PermRoaming == null)
          IsolatedStorageFile.s_PermRoaming = new FileIOPermission(FileIOPermissionAccess.AllAccess, IsolatedStorageFile.GetRootDir(scope));
        return IsolatedStorageFile.s_PermRoaming;
      }
      if (System.IO.IsolatedStorage.IsolatedStorage.IsMachine(scope))
      {
        if (IsolatedStorageFile.s_PermMachine == null)
          IsolatedStorageFile.s_PermMachine = new FileIOPermission(FileIOPermissionAccess.AllAccess, IsolatedStorageFile.GetRootDir(scope));
        return IsolatedStorageFile.s_PermMachine;
      }
      if (IsolatedStorageFile.s_PermUser == null)
        IsolatedStorageFile.s_PermUser = new FileIOPermission(FileIOPermissionAccess.AllAccess, IsolatedStorageFile.GetRootDir(scope));
      return IsolatedStorageFile.s_PermUser;
    }

    [SecurityCritical]
    private static void DemandAdminPermission()
    {
      if (IsolatedStorageFile.s_PermAdminUser == null)
        IsolatedStorageFile.s_PermAdminUser = new IsolatedStorageFilePermission(IsolatedStorageContainment.AdministerIsolatedStorageByUser, 0L, false);
      IsolatedStorageFile.s_PermAdminUser.Demand();
    }

    internal static void VerifyGlobalScope(IsolatedStorageScope scope)
    {
      if (scope != IsolatedStorageScope.User && scope != (IsolatedStorageScope.User | IsolatedStorageScope.Roaming) && scope != IsolatedStorageScope.Machine)
        throw new ArgumentException(Environment.GetResourceString("IsolatedStorage_Scope_U_R_M"));
    }

    [SecuritySafeCritical]
    internal void CreateIDFile(string path, IsolatedStorageScope scope)
    {
      try
      {
        using (FileStream fileStream = new FileStream(path + "identity.dat", FileMode.OpenOrCreate))
        {
          MemoryStream identityStream = this.GetIdentityStream(scope);
          byte[] buffer = identityStream.GetBuffer();
          fileStream.Write(buffer, 0, (int) identityStream.Length);
          identityStream.Close();
        }
      }
      catch
      {
      }
    }

    [SecuritySafeCritical]
    internal static string[] GetFileDirectoryNames(string path, string userSearchPattern, bool file)
    {
      if (path == null)
        throw new ArgumentNullException("path", Environment.GetResourceString("ArgumentNull_Path"));
      userSearchPattern = IsolatedStorageFile.NormalizeSearchPattern(userSearchPattern);
      if (userSearchPattern.Length == 0)
        return new string[0];
      bool flag1 = false;
      string str1 = path;
      int index1 = str1.Length - 1;
      char ch = str1[index1];
      if ((int) ch == (int) Path.DirectorySeparatorChar || (int) ch == (int) Path.AltDirectorySeparatorChar || (int) ch == 46)
        flag1 = true;
      string path1 = LongPath.NormalizePath(path);
      if (flag1)
      {
        string str2 = path1;
        int index2 = str2.Length - 1;
        if ((int) str2[index2] != (int) ch)
          path1 += "\\*";
      }
      string directoryName = LongPath.GetDirectoryName(path1);
      if (directoryName != null)
        directoryName += "\\";
      try
      {
        new FileIOPermission(FileIOPermissionAccess.Read, new string[1]
        {
          directoryName == null ? path1 : directoryName
        }, false, false).Demand();
      }
      catch
      {
        throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
      }
      string[] array = new string[10];
      int newSize = 0;
      Win32Native.WIN32_FIND_DATA wiN32FindData = new Win32Native.WIN32_FIND_DATA();
      SafeFindHandle firstFile = Win32Native.FindFirstFile(Path.AddLongPathPrefix(path1), wiN32FindData);
      if (firstFile.IsInvalid)
      {
        int lastWin32Error = Marshal.GetLastWin32Error();
        if (lastWin32Error == 2)
          return new string[0];
        __Error.WinIOError(lastWin32Error, userSearchPattern);
      }
      int num = 0;
      do
      {
        bool flag2;
        if (file)
        {
          flag2 = (wiN32FindData.dwFileAttributes & 16) == 0;
        }
        else
        {
          flag2 = (uint) (wiN32FindData.dwFileAttributes & 16) > 0U;
          if (flag2 && (wiN32FindData.cFileName.Equals(".") || wiN32FindData.cFileName.Equals("..")))
            flag2 = false;
        }
        if (flag2)
        {
          ++num;
          if (newSize == array.Length)
            Array.Resize<string>(ref array, 2 * array.Length);
          array[newSize++] = wiN32FindData.cFileName;
        }
      }
      while (Win32Native.FindNextFile(firstFile, wiN32FindData));
      int lastWin32Error1 = Marshal.GetLastWin32Error();
      firstFile.Close();
      if (lastWin32Error1 != 0 && lastWin32Error1 != 18)
        __Error.WinIOError(lastWin32Error1, userSearchPattern);
      if (!file && num == 1 && (wiN32FindData.dwFileAttributes & 16) != 0)
        return new string[1]{ wiN32FindData.cFileName };
      if (newSize == array.Length)
        return array;
      Array.Resize<string>(ref array, newSize);
      return array;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern ulong GetUsage(SafeIsolatedStorageFileHandle handle);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern SafeIsolatedStorageFileHandle Open(string infoFile, string syncName);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void Reserve(SafeIsolatedStorageFileHandle handle, ulong plQuota, ulong plReserve, [MarshalAs(UnmanagedType.Bool)] bool fFree);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void GetRootDir(IsolatedStorageScope scope, StringHandleOnStack retRootDir);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool Lock(SafeIsolatedStorageFileHandle handle, [MarshalAs(UnmanagedType.Bool)] bool fLock);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void CreateDirectoryWithDacl(string path);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool GetQuota(SafeIsolatedStorageFileHandle scope, out long quota);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern void SetQuota(SafeIsolatedStorageFileHandle scope, long quota);
  }
}
