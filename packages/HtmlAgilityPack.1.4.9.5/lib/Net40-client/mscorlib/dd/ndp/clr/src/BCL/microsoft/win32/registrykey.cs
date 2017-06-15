// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.RegistryKey
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;

namespace Microsoft.Win32
{
  /// <summary>表示 Windows 注册表中的项级节点。此类是注册表封装。</summary>
  [ComVisible(true)]
  public sealed class RegistryKey : MarshalByRefObject, IDisposable
  {
    internal static readonly IntPtr HKEY_CLASSES_ROOT = new IntPtr(int.MinValue);
    internal static readonly IntPtr HKEY_CURRENT_USER = new IntPtr(-2147483647);
    internal static readonly IntPtr HKEY_LOCAL_MACHINE = new IntPtr(-2147483646);
    internal static readonly IntPtr HKEY_USERS = new IntPtr(-2147483645);
    internal static readonly IntPtr HKEY_PERFORMANCE_DATA = new IntPtr(-2147483644);
    internal static readonly IntPtr HKEY_CURRENT_CONFIG = new IntPtr(-2147483643);
    internal static readonly IntPtr HKEY_DYN_DATA = new IntPtr(-2147483642);
    private static readonly string[] hkeyNames = new string[7]
    {
      "HKEY_CLASSES_ROOT",
      "HKEY_CURRENT_USER",
      "HKEY_LOCAL_MACHINE",
      "HKEY_USERS",
      "HKEY_PERFORMANCE_DATA",
      "HKEY_CURRENT_CONFIG",
      "HKEY_DYN_DATA"
    };
    private const int STATE_DIRTY = 1;
    private const int STATE_SYSTEMKEY = 2;
    private const int STATE_WRITEACCESS = 4;
    private const int STATE_PERF_DATA = 8;
    private const int MaxKeyLength = 255;
    private const int MaxValueLength = 16383;
    [SecurityCritical]
    private volatile SafeRegistryHandle hkey;
    private volatile int state;
    private volatile string keyName;
    private volatile bool remoteKey;
    private volatile RegistryKeyPermissionCheck checkMode;
    private volatile RegistryView regView;
    private const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;
    private const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;
    private const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192;

    /// <summary>检索当前项的子项计数。</summary>
    /// <returns>当前项的子项的数目。</returns>
    /// <exception cref="T:System.Security.SecurityException">用户没有该项的读取权限。</exception>
    /// <exception cref="T:System.ObjectDisposedException">要操作的 <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问关闭的项）。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">用户没有必需的注册表权限。</exception>
    /// <exception cref="T:System.IO.IOException">发生系统错误，例如，当前项已被删除。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public int SubKeyCount
    {
      [SecuritySafeCritical] get
      {
        this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckKeyReadPermission, (string) null, false, RegistryKeyPermissionCheck.Default);
        return this.InternalSubKeyCount();
      }
    }

    /// <summary>获取用于创建注册表项的视图。</summary>
    /// <returns>用于创建注册表项的视图。- 或 -如果未使用视图，则为 <see cref="F:Microsoft.Win32.RegistryView.Default" />。</returns>
    [ComVisible(false)]
    public RegistryView View
    {
      [SecuritySafeCritical] get
      {
        this.EnsureNotDisposed();
        return this.regView;
      }
    }

    /// <summary>获取一个 <see cref="T:Microsoft.Win32.SafeHandles.SafeRegistryHandle" /> 对象，该对象表示当前 <see cref="T:Microsoft.Win32.RegistryKey" /> 对象封装的注册表项。</summary>
    /// <returns>注册表项的句柄。</returns>
    /// <exception cref="T:System.ObjectDisposedException">注册表项已关闭。无法访问已关闭的项。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">用户没有必需的注册表权限。</exception>
    /// <exception cref="T:System.IO.IOException">发生系统错误，例如，删除了当前项。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有读取注册表项所需的权限。</exception>
    [ComVisible(false)]
    public SafeRegistryHandle Handle
    {
      [SecurityCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        this.EnsureNotDisposed();
        int errorCode = 6;
        if (!this.IsSystemKey())
          return this.hkey;
        IntPtr hKey = (IntPtr) 0;
        string s = this.keyName;
        uint stringHash = \u003CPrivateImplementationDetails\u003E.ComputeStringHash(s);
        if (stringHash <= 1097425318U)
        {
          if ((int) stringHash != 126972219)
          {
            if ((int) stringHash != 457190004)
            {
              if ((int) stringHash == 1097425318 && s == "HKEY_CLASSES_ROOT")
              {
                hKey = RegistryKey.HKEY_CLASSES_ROOT;
                goto label_22;
              }
            }
            else if (s == "HKEY_LOCAL_MACHINE")
            {
              hKey = RegistryKey.HKEY_LOCAL_MACHINE;
              goto label_22;
            }
          }
          else if (s == "HKEY_CURRENT_CONFIG")
          {
            hKey = RegistryKey.HKEY_CURRENT_CONFIG;
            goto label_22;
          }
        }
        else if (stringHash <= 1568329430U)
        {
          if ((int) stringHash != 1198714601)
          {
            if ((int) stringHash == 1568329430 && s == "HKEY_CURRENT_USER")
            {
              hKey = RegistryKey.HKEY_CURRENT_USER;
              goto label_22;
            }
          }
          else if (s == "HKEY_USERS")
          {
            hKey = RegistryKey.HKEY_USERS;
            goto label_22;
          }
        }
        else if ((int) stringHash != -1471101685)
        {
          if ((int) stringHash == -739976840 && s == "HKEY_PERFORMANCE_DATA")
          {
            hKey = RegistryKey.HKEY_PERFORMANCE_DATA;
            goto label_22;
          }
        }
        else if (s == "HKEY_DYN_DATA")
        {
          hKey = RegistryKey.HKEY_DYN_DATA;
          goto label_22;
        }
        this.Win32Error(errorCode, (string) null);
label_22:
        SafeRegistryHandle hkResult;
        int num = Win32Native.RegOpenKeyEx(hKey, (string) null, 0, (int) ((RegistryView) RegistryKey.GetRegistryKeyAccess(this.IsWritable()) | this.regView), out hkResult);
        if (num == 0 && !hkResult.IsInvalid)
          return hkResult;
        this.Win32Error(num, (string) null);
        throw new IOException(Win32Native.GetMessage(num), num);
      }
    }

    /// <summary>检索项中值的计数。</summary>
    /// <returns>项中的名称/值对的数目。</returns>
    /// <exception cref="T:System.Security.SecurityException">用户没有该项的读取权限。</exception>
    /// <exception cref="T:System.ObjectDisposedException">要操作的 <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问关闭的项）。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">用户没有必需的注册表权限。</exception>
    /// <exception cref="T:System.IO.IOException">发生系统错误，例如，当前项已被删除。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public int ValueCount
    {
      [SecuritySafeCritical] get
      {
        this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckKeyReadPermission, (string) null, false, RegistryKeyPermissionCheck.Default);
        return this.InternalValueCount();
      }
    }

    /// <summary>检索项的名称。</summary>
    /// <returns>项的绝对（限定）名称。</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问已关闭的项）。</exception>
    public string Name
    {
      [SecuritySafeCritical] get
      {
        this.EnsureNotDisposed();
        return this.keyName;
      }
    }

    [SecurityCritical]
    private RegistryKey(SafeRegistryHandle hkey, bool writable, RegistryView view)
      : this(hkey, writable, false, false, false, view)
    {
    }

    [SecurityCritical]
    private RegistryKey(SafeRegistryHandle hkey, bool writable, bool systemkey, bool remoteKey, bool isPerfData, RegistryView view)
    {
      this.hkey = hkey;
      this.keyName = "";
      this.remoteKey = remoteKey;
      this.regView = view;
      if (systemkey)
        this.state = this.state | 2;
      if (writable)
        this.state = this.state | 4;
      if (isPerfData)
        this.state = this.state | 8;
      RegistryKey.ValidateKeyView(view);
    }

    /// <summary>关闭该项，如果其内容已修改，则将其刷新到磁盘。</summary>
    public void Close()
    {
      this.Dispose(true);
    }

    [SecuritySafeCritical]
    private void Dispose(bool disposing)
    {
      if (this.hkey == null)
        return;
      if (!this.IsSystemKey())
      {
        try
        {
          this.hkey.Dispose();
        }
        catch (IOException ex)
        {
        }
        finally
        {
          this.hkey = (SafeRegistryHandle) null;
        }
      }
      else
      {
        if (!disposing || !this.IsPerfDataKey())
          return;
        SafeRegistryHandle.RegCloseKey(RegistryKey.HKEY_PERFORMANCE_DATA);
      }
    }

    /// <summary>将指定的打开注册表项的全部特性写到注册表中。</summary>
    [SecuritySafeCritical]
    public void Flush()
    {
      if (this.hkey == null || !this.IsDirty())
        return;
      Win32Native.RegFlushKey(this.hkey);
    }

    /// <summary>释放由 <see cref="T:Microsoft.Win32.RegistryKey" /> 类的当前实例占用的所有资源。</summary>
    public void Dispose()
    {
      this.Dispose(true);
    }

    /// <summary>创建一个新子项或打开一个现有子项以进行写访问。</summary>
    /// <returns>新创建的子项，如果操作失败，则为 null。如果为 <paramref name="subkey" /> 指定了零长度字符串，则返回当前的 <see cref="T:Microsoft.Win32.RegistryKey" /> 对象。</returns>
    /// <param name="subkey">要创建或打开的子项的名称或路径。此字符串不区分大小写。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="subkey" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有创建或打开该注册表项所需的权限。</exception>
    /// <exception cref="T:System.ObjectDisposedException">调用此方法时所针对的 <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问已关闭的项）。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">无法写入 <see cref="T:Microsoft.Win32.RegistryKey" />；例如，它不是作为可写入项打开的，或者用户没有必需的访问权限。</exception>
    /// <exception cref="T:System.IO.IOException">嵌套级别超过 510。- 或 -发生系统错误，例如，删除了项，或者尝试在 <see cref="F:Microsoft.Win32.Registry.LocalMachine" /> 根中创建项。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public RegistryKey CreateSubKey(string subkey)
    {
      return this.CreateSubKey(subkey, this.checkMode);
    }

    /// <summary>使用指定的权限检查选项创建一个新子项或打开一个现有子项以进行写访问。</summary>
    /// <returns>新创建的子项，如果操作失败，则为 null。如果为 <paramref name="subkey" /> 指定了零长度字符串，则返回当前的 <see cref="T:Microsoft.Win32.RegistryKey" /> 对象。</returns>
    /// <param name="subkey">要创建或打开的子项的名称或路径。此字符串不区分大小写。</param>
    /// <param name="permissionCheck">用于指定打开该项是进行读取还是读取/写入访问的枚举值之一。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="subkey" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有创建或打开该注册表项所需的权限。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="permissionCheck" /> 包含无效值。</exception>
    /// <exception cref="T:System.ObjectDisposedException">调用此方法时所针对的 <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问已关闭的项）。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">无法写入 <see cref="T:Microsoft.Win32.RegistryKey" />；例如，它不是作为可写入项打开的，或者用户没有必需的访问权限。</exception>
    /// <exception cref="T:System.IO.IOException">嵌套级别超过 510。- 或 -发生系统错误，例如，删除了项，或者尝试在 <see cref="F:Microsoft.Win32.Registry.LocalMachine" /> 根中创建项。</exception>
    [ComVisible(false)]
    public RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck)
    {
      return this.CreateSubKeyInternal(subkey, permissionCheck, (object) null, RegistryOptions.None);
    }

    /// <summary>使用指定的权限检查和注册表选项，创建或打开一个用于写访问的子项。</summary>
    /// <returns>新创建的子项，如果操作失败，则为 null。</returns>
    /// <param name="subkey">要创建或打开的子项的名称或路径。</param>
    /// <param name="permissionCheck">用于指定打开该项是进行读取还是读取/写入访问的枚举值之一。</param>
    /// <param name="options">要使用的注册表选项；例如，用于创建可变键的注册表选项。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="subkey " /> 为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">当前的 <see cref="T:Microsoft.Win32.RegistryKey" /> 对象已关闭（无法访问关闭的项）。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">无法写入当前 <see cref="T:Microsoft.Win32.RegistryKey" /> 对象；例如，它未作为可写入项打开，或用户不具有所需的访问权限。</exception>
    /// <exception cref="T:System.IO.IOException">嵌套级别超过 510。- 或 -发生系统错误，例如，删除了项，或者尝试在 <see cref="F:Microsoft.Win32.Registry.LocalMachine" /> 根中创建项。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有创建或打开该注册表项所需的权限。</exception>
    [ComVisible(false)]
    public RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck, RegistryOptions options)
    {
      return this.CreateSubKeyInternal(subkey, permissionCheck, (object) null, options);
    }

    /// <summary>创建一个新的子项或用指定的访问权限打开一个现有子项。从开始，提供.NET Framework 2015</summary>
    /// <returns>新创建的子项，如果操作失败，则为 null。如果为 <paramref name="subkey" /> 指定了零长度字符串，则返回当前的 <see cref="T:Microsoft.Win32.RegistryKey" /> 对象。</returns>
    /// <param name="subkey">要创建或打开的子项的名称或路径。此字符串不区分大小写。</param>
    /// <param name="writable">true若要指示新的子项是可写 ；否则为false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="subkey" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有创建或打开该注册表项所需的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">无法写入当前 <see cref="T:Microsoft.Win32.RegistryKey" />；例如，它未作为可写入项打开，或用户不具有必要的访问权限。</exception>
    /// <exception cref="T:System.IO.IOException">嵌套级别超过 510。- 或 -发生系统错误，例如，删除了项，或者尝试在 <see cref="F:Microsoft.Win32.Registry.LocalMachine" /> 根中创建项。</exception>
    [ComVisible(false)]
    public RegistryKey CreateSubKey(string subkey, bool writable)
    {
      return this.CreateSubKeyInternal(subkey, writable ? RegistryKeyPermissionCheck.ReadWriteSubTree : RegistryKeyPermissionCheck.ReadSubTree, (object) null, RegistryOptions.None);
    }

    /// <summary>创建一个新的子项或用指定的访问权限打开一个现有子项。从开始，提供.NET Framework 2015</summary>
    /// <returns>新创建的子项，如果操作失败，则为 null。如果为 <paramref name="subkey" /> 指定了零长度字符串，则返回当前的 <see cref="T:Microsoft.Win32.RegistryKey" /> 对象。</returns>
    /// <param name="subkey">要创建或打开的子项的名称或路径。此字符串不区分大小写。</param>
    /// <param name="writable">true若要指示新的子项是可写 ；否则为false。</param>
    /// <param name="options">要使用的注册表选项。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="subkey" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" />未指定有效的选项</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有创建或打开该注册表项所需的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">无法写入当前 <see cref="T:Microsoft.Win32.RegistryKey" />；例如，它未作为可写入项打开，或用户不具有必要的访问权限。</exception>
    /// <exception cref="T:System.IO.IOException">嵌套级别超过 510。- 或 -发生系统错误，例如，删除了项，或者尝试在 <see cref="F:Microsoft.Win32.Registry.LocalMachine" /> 根中创建项。</exception>
    [ComVisible(false)]
    public RegistryKey CreateSubKey(string subkey, bool writable, RegistryOptions options)
    {
      return this.CreateSubKeyInternal(subkey, writable ? RegistryKeyPermissionCheck.ReadWriteSubTree : RegistryKeyPermissionCheck.ReadSubTree, (object) null, options);
    }

    /// <summary>使用指定的权限检查选项和注册表安全性创建一个新子项或打开一个现有子项以进行写访问。</summary>
    /// <returns>新创建的子项，如果操作失败，则为 null。如果为 <paramref name="subkey" /> 指定了零长度字符串，则返回当前的 <see cref="T:Microsoft.Win32.RegistryKey" /> 对象。</returns>
    /// <param name="subkey">要创建或打开的子项的名称或路径。此字符串不区分大小写。</param>
    /// <param name="permissionCheck">用于指定打开该项是进行读取还是读取/写入访问的枚举值之一。</param>
    /// <param name="registrySecurity">新项的访问控制安全性。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="subkey" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有创建或打开该注册表项所需的权限。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="permissionCheck" /> 包含无效值。</exception>
    /// <exception cref="T:System.ObjectDisposedException">调用此方法时所针对的 <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问已关闭的项）。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">无法写入当前 <see cref="T:Microsoft.Win32.RegistryKey" />；例如，它未作为可写入项打开，或用户不具有必要的访问权限。</exception>
    /// <exception cref="T:System.IO.IOException">嵌套级别超过 510。- 或 -发生系统错误，例如，删除了项，或者尝试在 <see cref="F:Microsoft.Win32.Registry.LocalMachine" /> 根中创建项。</exception>
    [ComVisible(false)]
    public RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck, RegistrySecurity registrySecurity)
    {
      return this.CreateSubKeyInternal(subkey, permissionCheck, (object) registrySecurity, RegistryOptions.None);
    }

    /// <summary>使用指定的权限检查选项、注册表选项和注册表安全性，创建或打开一个用于写访问的子项。</summary>
    /// <returns>新创建的子项，如果操作失败，则为 null。  </returns>
    /// <param name="subkey">要创建或打开的子项的名称或路径。</param>
    /// <param name="permissionCheck">用于指定打开该项是进行读取还是读取/写入访问的枚举值之一。</param>
    /// <param name="registryOptions">要使用的注册表选项。</param>
    /// <param name="registrySecurity">新子项的访问控制安全性。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="subkey " /> 为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">当前的 <see cref="T:Microsoft.Win32.RegistryKey" /> 对象被关闭。无法访问已关闭的项。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">无法写入当前 <see cref="T:Microsoft.Win32.RegistryKey" /> 对象；例如，它未作为可写入项打开，或用户不具有所需的访问权限。</exception>
    /// <exception cref="T:System.IO.IOException">嵌套级别超过 510。- 或 -发生系统错误，例如，删除了项，或者尝试在 <see cref="F:Microsoft.Win32.Registry.LocalMachine" /> 根中创建项。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有创建或打开该注册表项所需的权限。</exception>
    [ComVisible(false)]
    public RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck, RegistryOptions registryOptions, RegistrySecurity registrySecurity)
    {
      return this.CreateSubKeyInternal(subkey, permissionCheck, (object) registrySecurity, registryOptions);
    }

    [SecuritySafeCritical]
    [ComVisible(false)]
    private unsafe RegistryKey CreateSubKeyInternal(string subkey, RegistryKeyPermissionCheck permissionCheck, object registrySecurityObj, RegistryOptions registryOptions)
    {
      RegistryKey.ValidateKeyOptions(registryOptions);
      RegistryKey.ValidateKeyName(subkey);
      RegistryKey.ValidateKeyMode(permissionCheck);
      this.EnsureWriteable();
      subkey = RegistryKey.FixupName(subkey);
      if (!this.remoteKey)
      {
        RegistryKey registryKey = this.InternalOpenSubKey(subkey, permissionCheck != RegistryKeyPermissionCheck.ReadSubTree);
        if (registryKey != null)
        {
          this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckSubKeyWritePermission, subkey, false, RegistryKeyPermissionCheck.Default);
          this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckSubTreePermission, subkey, false, permissionCheck);
          registryKey.checkMode = permissionCheck;
          return registryKey;
        }
      }
      this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckSubKeyCreatePermission, subkey, false, RegistryKeyPermissionCheck.Default);
      Win32Native.SECURITY_ATTRIBUTES securityAttributes = (Win32Native.SECURITY_ATTRIBUTES) null;
      RegistrySecurity registrySecurity = (RegistrySecurity) registrySecurityObj;
      if (registrySecurity != null)
      {
        securityAttributes = new Win32Native.SECURITY_ATTRIBUTES();
        securityAttributes.nLength = Marshal.SizeOf<Win32Native.SECURITY_ATTRIBUTES>(securityAttributes);
        byte[] descriptorBinaryForm = registrySecurity.GetSecurityDescriptorBinaryForm();
        byte* pDest = stackalloc byte[descriptorBinaryForm.Length];
        Buffer.Memcpy(pDest, 0, descriptorBinaryForm, 0, descriptorBinaryForm.Length);
        securityAttributes.pSecurityDescriptor = pDest;
      }
      int lpdwDisposition = 0;
      SafeRegistryHandle hkResult = (SafeRegistryHandle) null;
      int keyEx = Win32Native.RegCreateKeyEx(this.hkey, subkey, 0, (string) null, (int) registryOptions, (int) ((RegistryView) RegistryKey.GetRegistryKeyAccess(permissionCheck != RegistryKeyPermissionCheck.ReadSubTree) | this.regView), securityAttributes, out hkResult, out lpdwDisposition);
      if (keyEx == 0 && !hkResult.IsInvalid)
      {
        RegistryKey registryKey = new RegistryKey(hkResult, permissionCheck != RegistryKeyPermissionCheck.ReadSubTree, false, this.remoteKey, false, this.regView);
        this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckSubTreePermission, subkey, false, permissionCheck);
        registryKey.checkMode = permissionCheck;
        registryKey.keyName = subkey.Length != 0 ? this.keyName + "\\" + subkey : this.keyName;
        return registryKey;
      }
      if (keyEx != 0)
        this.Win32Error(keyEx, this.keyName + "\\" + subkey);
      return (RegistryKey) null;
    }

    /// <summary>删除指定子项。</summary>
    /// <param name="subkey">要删除的子项的名称。此字符串不区分大小写。</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="subkey" /> 有子级子项</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="subkey" /> 参数未指定有效的注册表项 </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="subkey" /> 为null</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有删除该项所需的权限。</exception>
    /// <exception cref="T:System.ObjectDisposedException">要操作的 <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问关闭的项）。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">用户没有必需的注册表权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public void DeleteSubKey(string subkey)
    {
      this.DeleteSubKey(subkey, true);
    }

    /// <summary>删除指定的子项，并指定在找不到该子项时是否引发异常。</summary>
    /// <param name="subkey">要删除的子项的名称。此字符串不区分大小写。</param>
    /// <param name="throwOnMissingSubKey">指示在找不到指定子项的情况下是否引发异常。如果该参数为 true，并且指定的子项不存在，则引发异常。如果该参数为 false，并且指定的子项不存在，则不执行任何操作。</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="subkey" /> 有子级子项。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="subkey" /> 未指定有效的注册表项，并且 <paramref name="throwOnMissingSubKey" /> 为 true。 </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="subkey" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有删除该项所需的权限。</exception>
    /// <exception cref="T:System.ObjectDisposedException">要操作的 <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问关闭的项）。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">用户没有必需的注册表权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public void DeleteSubKey(string subkey, bool throwOnMissingSubKey)
    {
      RegistryKey.ValidateKeyName(subkey);
      this.EnsureWriteable();
      subkey = RegistryKey.FixupName(subkey);
      this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckSubKeyWritePermission, subkey, false, RegistryKeyPermissionCheck.Default);
      RegistryKey registryKey = this.InternalOpenSubKey(subkey, false);
      if (registryKey != null)
      {
        try
        {
          if (registryKey.InternalSubKeyCount() > 0)
            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_RegRemoveSubKey);
        }
        finally
        {
          registryKey.Close();
        }
        int errorCode;
        try
        {
          errorCode = Win32Native.RegDeleteKeyEx(this.hkey, subkey, (int) this.regView, 0);
        }
        catch (EntryPointNotFoundException ex)
        {
          errorCode = Win32Native.RegDeleteKey(this.hkey, subkey);
        }
        if (errorCode == 0)
          return;
        if (errorCode == 2)
        {
          if (!throwOnMissingSubKey)
            return;
          ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSubKeyAbsent);
        }
        else
          this.Win32Error(errorCode, (string) null);
      }
      else
      {
        if (!throwOnMissingSubKey)
          return;
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSubKeyAbsent);
      }
    }

    /// <summary>递归删除子项和任何子级子项。</summary>
    /// <param name="subkey">要删除的子项。此字符串不区分大小写。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="subkey" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">尝试删除根配置单元。- 或 -<paramref name="subkey" /> 未指定有效的注册表子项。</exception>
    /// <exception cref="T:System.IO.IOException">出现 I/O 错误。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有删除该项所需的权限。</exception>
    /// <exception cref="T:System.ObjectDisposedException">要操作的 <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问关闭的项）。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">用户没有必需的注册表权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public void DeleteSubKeyTree(string subkey)
    {
      this.DeleteSubKeyTree(subkey, true);
    }

    /// <summary>以递归方式删除指定的子项和任何子级子项，并指定在找不到子项时是否引发异常。</summary>
    /// <param name="subkey">要删除的子项的名称。此字符串不区分大小写。</param>
    /// <param name="throwOnMissingSubKey">指示在找不到指定子项的情况下是否引发异常。如果该参数为 true，并且指定的子项不存在，则引发异常。如果该参数为 false，并且指定的子项不存在，则不执行任何操作。</param>
    /// <exception cref="T:System.ArgumentException">尝试删除树的根配置单元。- 或 -<paramref name="subkey" /> 未指定有效的注册表子项，并且 <paramref name="throwOnMissingSubKey" /> 为 true。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="subkey" /> 为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问已关闭的项）。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">用户没有必需的注册表权限。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有删除该项所需的权限。</exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public void DeleteSubKeyTree(string subkey, bool throwOnMissingSubKey)
    {
      RegistryKey.ValidateKeyName(subkey);
      if (subkey.Length == 0 && this.IsSystemKey())
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegKeyDelHive);
      this.EnsureWriteable();
      subkey = RegistryKey.FixupName(subkey);
      this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckSubTreeWritePermission, subkey, false, RegistryKeyPermissionCheck.Default);
      RegistryKey registryKey = this.InternalOpenSubKey(subkey, true);
      if (registryKey != null)
      {
        try
        {
          if (registryKey.InternalSubKeyCount() > 0)
          {
            foreach (string subKeyName in registryKey.InternalGetSubKeyNames())
              registryKey.DeleteSubKeyTreeInternal(subKeyName);
          }
        }
        finally
        {
          registryKey.Close();
        }
        int errorCode;
        try
        {
          errorCode = Win32Native.RegDeleteKeyEx(this.hkey, subkey, (int) this.regView, 0);
        }
        catch (EntryPointNotFoundException ex)
        {
          errorCode = Win32Native.RegDeleteKey(this.hkey, subkey);
        }
        if (errorCode == 0)
          return;
        this.Win32Error(errorCode, (string) null);
      }
      else
      {
        if (!throwOnMissingSubKey)
          return;
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSubKeyAbsent);
      }
    }

    [SecurityCritical]
    private void DeleteSubKeyTreeInternal(string subkey)
    {
      RegistryKey registryKey = this.InternalOpenSubKey(subkey, true);
      if (registryKey != null)
      {
        try
        {
          if (registryKey.InternalSubKeyCount() > 0)
          {
            foreach (string subKeyName in registryKey.InternalGetSubKeyNames())
              registryKey.DeleteSubKeyTreeInternal(subKeyName);
          }
        }
        finally
        {
          registryKey.Close();
        }
        int errorCode;
        try
        {
          errorCode = Win32Native.RegDeleteKeyEx(this.hkey, subkey, (int) this.regView, 0);
        }
        catch (EntryPointNotFoundException ex)
        {
          errorCode = Win32Native.RegDeleteKey(this.hkey, subkey);
        }
        if (errorCode == 0)
          return;
        this.Win32Error(errorCode, (string) null);
      }
      else
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSubKeyAbsent);
    }

    /// <summary>从此项中删除指定值。</summary>
    /// <param name="name">要删除的值的名称。 </param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 不是对值的有效引用。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有删除该值所需的权限。</exception>
    /// <exception cref="T:System.ObjectDisposedException">要操作的 <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问关闭的项）。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">正在处理的 <see cref="T:Microsoft.Win32.RegistryKey" /> 为只读。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public void DeleteValue(string name)
    {
      this.DeleteValue(name, true);
    }

    /// <summary>从此项中删除指定的值，并指定在找不到该值时是否引发异常。</summary>
    /// <param name="name">要删除的值的名称。</param>
    /// <param name="throwOnMissingValue">指示在找不到指定值的情况下是否引发异常。如果该参数为 true，并且指定的值不存在，则引发异常。如果该参数为 false，并且指定的值不存在，则不执行任何操作。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 不是对值的有效引用，并且 <paramref name="throwOnMissingValue" /> 为 true。- 或 - <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有删除该值所需的权限。</exception>
    /// <exception cref="T:System.ObjectDisposedException">要操作的 <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问关闭的项）。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">正在处理的 <see cref="T:Microsoft.Win32.RegistryKey" /> 为只读。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public void DeleteValue(string name, bool throwOnMissingValue)
    {
      this.EnsureWriteable();
      this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckValueWritePermission, name, false, RegistryKeyPermissionCheck.Default);
      switch (Win32Native.RegDeleteValue(this.hkey, name))
      {
        case 2:
        case 206:
          if (!throwOnMissingValue)
            break;
          ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSubKeyValueAbsent);
          break;
      }
    }

    [SecurityCritical]
    internal static RegistryKey GetBaseKey(IntPtr hKey)
    {
      return RegistryKey.GetBaseKey(hKey, RegistryView.Default);
    }

    [SecurityCritical]
    internal static RegistryKey GetBaseKey(IntPtr hKey, RegistryView view)
    {
      int index = (int) hKey & 268435455;
      bool flag = hKey == RegistryKey.HKEY_PERFORMANCE_DATA;
      RegistryKey registryKey = new RegistryKey(new SafeRegistryHandle(hKey, flag), true, true, false, flag, view);
      registryKey.checkMode = RegistryKeyPermissionCheck.Default;
      string str = RegistryKey.hkeyNames[index];
      registryKey.keyName = str;
      return registryKey;
    }

    /// <summary>打开一个新的 <see cref="T:Microsoft.Win32.RegistryKey" />，它使用指定的视图在本地计算机上表示请求的项。</summary>
    /// <returns>请求的注册表项。</returns>
    /// <param name="hKey">要打开的 HKEY。</param>
    /// <param name="view">要使用的注册表视图。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="hKey" /> 或 <paramref name="view" /> 无效。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">用户没有必需的注册表权限。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有执行此操作所需的权限。</exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public static RegistryKey OpenBaseKey(RegistryHive hKey, RegistryView view)
    {
      RegistryKey.ValidateKeyView(view);
      RegistryKey.CheckUnmanagedCodePermission();
      return RegistryKey.GetBaseKey((IntPtr) ((int) hKey), view);
    }

    /// <summary>打开一个新的 <see cref="T:Microsoft.Win32.RegistryKey" />，它表示远程计算机上的请求的项。</summary>
    /// <returns>请求的注册表项。</returns>
    /// <param name="hKey">要从 <see cref="T:Microsoft.Win32.RegistryHive" /> 枚举中打开的 HKEY。</param>
    /// <param name="machineName">远程计算机。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="hKey" /> 无效。</exception>
    /// <exception cref="T:System.IO.IOException">未找到 <paramref name="machineName" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="machineName" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户不具有执行该操作的适当权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">用户没有必需的注册表权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static RegistryKey OpenRemoteBaseKey(RegistryHive hKey, string machineName)
    {
      return RegistryKey.OpenRemoteBaseKey(hKey, machineName, RegistryView.Default);
    }

    /// <summary>打开一个新的注册表项，它使用指定的视图在远程计算机上表示请求的项。</summary>
    /// <returns>请求的注册表项。</returns>
    /// <param name="hKey">要从 <see cref="T:Microsoft.Win32.RegistryHive" /> 枚举中打开的 HKEY。</param>
    /// <param name="machineName">远程计算机。</param>
    /// <param name="view">要使用的注册表视图。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="hKey" /> 或 <paramref name="view" /> 无效。</exception>
    /// <exception cref="T:System.IO.IOException">未找到 <paramref name="machineName" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="machineName" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="machineName" /> 为 null。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">用户没有必需的注册表权限。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户不具有执行该操作所需的权限。</exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public static RegistryKey OpenRemoteBaseKey(RegistryHive hKey, string machineName, RegistryView view)
    {
      if (machineName == null)
        throw new ArgumentNullException("machineName");
      int index = (int) (hKey & (RegistryHive) 268435455);
      if (index < 0 || index >= RegistryKey.hkeyNames.Length || ((long) hKey & 4294967280L) != 2147483648L)
        throw new ArgumentException(Environment.GetResourceString("Arg_RegKeyOutOfRange"));
      RegistryKey.ValidateKeyView(view);
      RegistryKey.CheckUnmanagedCodePermission();
      SafeRegistryHandle result = (SafeRegistryHandle) null;
      int errorCode = Win32Native.RegConnectRegistry(machineName, new SafeRegistryHandle(new IntPtr((int) hKey), false), out result);
      switch (errorCode)
      {
        case 1114:
          throw new ArgumentException(Environment.GetResourceString("Arg_DllInitFailure"));
        case 0:
          if (result.IsInvalid)
            throw new ArgumentException(Environment.GetResourceString("Arg_RegKeyNoRemoteConnect", (object) machineName));
          RegistryKey registryKey = new RegistryKey(result, true, false, true, (IntPtr) ((int) hKey) == RegistryKey.HKEY_PERFORMANCE_DATA, view);
          registryKey.checkMode = RegistryKeyPermissionCheck.Default;
          string str = RegistryKey.hkeyNames[index];
          registryKey.keyName = str;
          return registryKey;
        default:
          RegistryKey.Win32ErrorStatic(errorCode, (string) null);
          goto case 0;
      }
    }

    /// <summary>检索指定的子项，并指定是否将写访问权限应用于该项。</summary>
    /// <returns>请求的子项；如果操作失败，则为 null。</returns>
    /// <param name="name">要打开的子项的名称或路径。</param>
    /// <param name="writable">如果需要项的写访问权限，则设置为 true。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问已关闭的项）。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有在指定模式下访问注册表项所需的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public RegistryKey OpenSubKey(string name, bool writable)
    {
      RegistryKey.ValidateKeyName(name);
      this.EnsureNotDisposed();
      name = RegistryKey.FixupName(name);
      this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckOpenSubKeyWithWritablePermission, name, writable, RegistryKeyPermissionCheck.Default);
      SafeRegistryHandle hkResult = (SafeRegistryHandle) null;
      int num = Win32Native.RegOpenKeyEx(this.hkey, name, 0, (int) ((RegistryView) RegistryKey.GetRegistryKeyAccess(writable) | this.regView), out hkResult);
      if (num == 0 && !hkResult.IsInvalid)
        return new RegistryKey(hkResult, writable, false, this.remoteKey, false, this.regView)
        {
          checkMode = this.GetSubKeyPermissonCheck(writable),
          keyName = this.keyName + "\\" + name
        };
      if (num == 5 || num == 1346)
        ThrowHelper.ThrowSecurityException(ExceptionResource.Security_RegistryPermission);
      return (RegistryKey) null;
    }

    /// <summary>检索指定的子项以进行读取或读/写访问。</summary>
    /// <returns>请求的子项；如果操作失败，则为 null。</returns>
    /// <param name="name">要创建或打开的子项的名称或路径。</param>
    /// <param name="permissionCheck">用于指定打开该项是进行读取还是读取/写入访问的枚举值之一。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="permissionCheck" /> 包含无效值。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问已关闭的项）。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有读取注册表项所需的权限。</exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public RegistryKey OpenSubKey(string name, RegistryKeyPermissionCheck permissionCheck)
    {
      RegistryKey.ValidateKeyMode(permissionCheck);
      return this.InternalOpenSubKey(name, permissionCheck, RegistryKey.GetRegistryKeyAccess(permissionCheck));
    }

    /// <summary>检索具有指定名称的子项。从开始，提供.NET Framework 2015</summary>
    /// <returns>请求的子项；如果操作失败，则为 null。</returns>
    /// <param name="name">要创建或打开的子项的名称或路径。</param>
    /// <param name="rights">注册表项的权限。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问已关闭的项）。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有在指定模式下访问注册表项所需的权限。</exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public RegistryKey OpenSubKey(string name, RegistryRights rights)
    {
      return this.InternalOpenSubKey(name, this.checkMode, (int) rights);
    }

    /// <summary>检索指定的子项以进行读取或读/写访问，请求指定的访问权限。</summary>
    /// <returns>请求的子项；如果操作失败，则为 null。</returns>
    /// <param name="name">要创建或打开的子项的名称或路径。</param>
    /// <param name="permissionCheck">用于指定打开该项是进行读取还是读取/写入访问的枚举值之一。</param>
    /// <param name="rights">枚举值的按位组合，它指定所需的安全访问。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="permissionCheck" /> 包含无效值。</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问已关闭的项）。 </exception>
    /// <exception cref="T:System.Security.SecurityException">
    /// <paramref name="rights" /> 包含无效的注册表权限值。- 或 -用户没有所要求的权限。</exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public RegistryKey OpenSubKey(string name, RegistryKeyPermissionCheck permissionCheck, RegistryRights rights)
    {
      return this.InternalOpenSubKey(name, permissionCheck, (int) rights);
    }

    [SecurityCritical]
    private RegistryKey InternalOpenSubKey(string name, RegistryKeyPermissionCheck permissionCheck, int rights)
    {
      RegistryKey.ValidateKeyName(name);
      RegistryKey.ValidateKeyMode(permissionCheck);
      RegistryKey.ValidateKeyRights(rights);
      this.EnsureNotDisposed();
      name = RegistryKey.FixupName(name);
      this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckOpenSubKeyPermission, name, false, permissionCheck);
      this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckSubTreePermission, name, false, permissionCheck);
      SafeRegistryHandle hkResult = (SafeRegistryHandle) null;
      int num = Win32Native.RegOpenKeyEx(this.hkey, name, 0, (int) ((RegistryView) rights | this.regView), out hkResult);
      if (num == 0 && !hkResult.IsInvalid)
        return new RegistryKey(hkResult, permissionCheck == RegistryKeyPermissionCheck.ReadWriteSubTree, false, this.remoteKey, false, this.regView)
        {
          keyName = this.keyName + "\\" + name,
          checkMode = permissionCheck
        };
      if (num == 5 || num == 1346)
        ThrowHelper.ThrowSecurityException(ExceptionResource.Security_RegistryPermission);
      return (RegistryKey) null;
    }

    [SecurityCritical]
    internal RegistryKey InternalOpenSubKey(string name, bool writable)
    {
      RegistryKey.ValidateKeyName(name);
      this.EnsureNotDisposed();
      SafeRegistryHandle hkResult = (SafeRegistryHandle) null;
      if (Win32Native.RegOpenKeyEx(this.hkey, name, 0, (int) ((RegistryView) RegistryKey.GetRegistryKeyAccess(writable) | this.regView), out hkResult) != 0 || hkResult.IsInvalid)
        return (RegistryKey) null;
      return new RegistryKey(hkResult, writable, false, this.remoteKey, false, this.regView)
      {
        keyName = this.keyName + "\\" + name
      };
    }

    /// <summary>以只读方式检索子项。</summary>
    /// <returns>请求的子项；如果操作失败，则为 null。</returns>
    /// <param name="name">要以只读方式打开的子项的名称或路径。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为null</exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问已关闭的项）。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有读取注册表项所需的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
    /// </PermissionSet>
    public RegistryKey OpenSubKey(string name)
    {
      return this.OpenSubKey(name, false);
    }

    /// <summary>根据指定的句柄创建注册表项。</summary>
    /// <returns>注册表项。</returns>
    /// <param name="handle">注册表项的句柄。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="handle" /> 为 null。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">用户没有必需的注册表权限。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有执行此操作所需的权限。</exception>
    [SecurityCritical]
    [ComVisible(false)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public static RegistryKey FromHandle(SafeRegistryHandle handle)
    {
      return RegistryKey.FromHandle(handle, RegistryView.Default);
    }

    /// <summary>利用指定的句柄和注册表视图设置创建注册表项。</summary>
    /// <returns>注册表项。</returns>
    /// <param name="handle">注册表项的句柄。</param>
    /// <param name="view">要使用的注册表视图。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="view" /> 无效。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="handle" /> 为 null。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">用户没有必需的注册表权限。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有执行此操作所需的权限。</exception>
    [SecurityCritical]
    [ComVisible(false)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public static RegistryKey FromHandle(SafeRegistryHandle handle, RegistryView view)
    {
      if (handle == null)
        throw new ArgumentNullException("handle");
      RegistryKey.ValidateKeyView(view);
      return new RegistryKey(handle, true, view);
    }

    [SecurityCritical]
    internal int InternalSubKeyCount()
    {
      this.EnsureNotDisposed();
      int lpcSubKeys = 0;
      int lpcValues = 0;
      int errorCode = Win32Native.RegQueryInfoKey(this.hkey, (StringBuilder) null, (int[]) null, IntPtr.Zero, ref lpcSubKeys, (int[]) null, (int[]) null, ref lpcValues, (int[]) null, (int[]) null, (int[]) null, (int[]) null);
      if (errorCode != 0)
        this.Win32Error(errorCode, (string) null);
      return lpcSubKeys;
    }

    /// <summary>检索包含所有子项名称的字符串数组。</summary>
    /// <returns>包含当前项的子项名称的字符串数组。</returns>
    /// <exception cref="T:System.Security.SecurityException">用户没有读取该项所需的权限。</exception>
    /// <exception cref="T:System.ObjectDisposedException">要操作的 <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问关闭的项）。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">用户没有必需的注册表权限。</exception>
    /// <exception cref="T:System.IO.IOException">发生系统错误，例如，当前项已被删除。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public string[] GetSubKeyNames()
    {
      this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckKeyReadPermission, (string) null, false, RegistryKeyPermissionCheck.Default);
      return this.InternalGetSubKeyNames();
    }

    [SecurityCritical]
    internal unsafe string[] InternalGetSubKeyNames()
    {
      this.EnsureNotDisposed();
      int length1 = this.InternalSubKeyCount();
      string[] strArray = new string[length1];
      if (length1 > 0)
      {
        char[] chArray = new char[256];
        fixed (char* lpName = &chArray[0])
        {
          for (int dwIndex = 0; dwIndex < length1; ++dwIndex)
          {
            int length2 = chArray.Length;
            int errorCode = Win32Native.RegEnumKeyEx(this.hkey, dwIndex, lpName, ref length2, (int[]) null, (StringBuilder) null, (int[]) null, (long[]) null);
            if (errorCode != 0)
              this.Win32Error(errorCode, (string) null);
            strArray[dwIndex] = new string(lpName);
          }
        }
      }
      return strArray;
    }

    [SecurityCritical]
    internal int InternalValueCount()
    {
      this.EnsureNotDisposed();
      int lpcValues = 0;
      int lpcSubKeys = 0;
      int errorCode = Win32Native.RegQueryInfoKey(this.hkey, (StringBuilder) null, (int[]) null, IntPtr.Zero, ref lpcSubKeys, (int[]) null, (int[]) null, ref lpcValues, (int[]) null, (int[]) null, (int[]) null, (int[]) null);
      if (errorCode != 0)
        this.Win32Error(errorCode, (string) null);
      return lpcValues;
    }

    /// <summary>检索包含与此项关联的所有值名称的字符串数组。</summary>
    /// <returns>包含当前项的值名称的字符串数组。</returns>
    /// <exception cref="T:System.Security.SecurityException">该用户没有读取注册表项所需的权限。</exception>
    /// <exception cref="T:System.ObjectDisposedException">要操作的 <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问关闭的项）。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">用户没有必需的注册表权限。</exception>
    /// <exception cref="T:System.IO.IOException">发生系统错误；例如，当前项已被删除。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public unsafe string[] GetValueNames()
    {
      this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckKeyReadPermission, (string) null, false, RegistryKeyPermissionCheck.Default);
      this.EnsureNotDisposed();
      int length1 = this.InternalValueCount();
      string[] strArray = new string[length1];
      if (length1 > 0)
      {
        char[] chArray = new char[16384];
        fixed (char* lpValueName = &chArray[0])
        {
          for (int dwIndex = 0; dwIndex < length1; ++dwIndex)
          {
            int length2 = chArray.Length;
            int errorCode = Win32Native.RegEnumValue(this.hkey, dwIndex, lpValueName, ref length2, IntPtr.Zero, (int[]) null, (byte[]) null, (int[]) null);
            if (errorCode != 0 && (!this.IsPerfDataKey() || errorCode != 234))
              this.Win32Error(errorCode, (string) null);
            strArray[dwIndex] = new string(lpValueName);
          }
        }
      }
      return strArray;
    }

    /// <summary>检索与指定名称关联的值。如果注册表中不存在名称/值对，则返回 null。</summary>
    /// <returns>与 <paramref name="name" /> 关联的值；如果未找到 <paramref name="name" />，则为 null。</returns>
    /// <param name="name">要检索的值的名称。此字符串不区分大小写。</param>
    /// <exception cref="T:System.Security.SecurityException">该用户没有读取注册表项所需的权限。</exception>
    /// <exception cref="T:System.ObjectDisposedException">包含指定值的 <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问关闭的项）。</exception>
    /// <exception cref="T:System.IO.IOException">包含指定值的 <see cref="T:Microsoft.Win32.RegistryKey" /> 已标记为删除。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">用户没有必需的注册表权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public object GetValue(string name)
    {
      this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckValueReadPermission, name, false, RegistryKeyPermissionCheck.Default);
      return this.InternalGetValue(name, (object) null, false, true);
    }

    /// <summary>检索与指定名称关联的值。如果未找到名称，则返回你提供的默认值。</summary>
    /// <returns>与 <paramref name="name" /> 关联的值，不展开嵌入的任何环境变量；如果未找到 <paramref name="name" />，则为 <paramref name="defaultValue" />。</returns>
    /// <param name="name">要检索的值的名称。此字符串不区分大小写。</param>
    /// <param name="defaultValue">当 <paramref name="name" /> 不存在时返回的值。</param>
    /// <exception cref="T:System.Security.SecurityException">该用户没有读取注册表项所需的权限。</exception>
    /// <exception cref="T:System.ObjectDisposedException">包含指定值的 <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问关闭的项）。</exception>
    /// <exception cref="T:System.IO.IOException">包含指定值的 <see cref="T:Microsoft.Win32.RegistryKey" /> 已标记为删除。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">用户没有必需的注册表权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public object GetValue(string name, object defaultValue)
    {
      this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckValueReadPermission, name, false, RegistryKeyPermissionCheck.Default);
      return this.InternalGetValue(name, defaultValue, false, true);
    }

    /// <summary>检索与指定的名称和检索选项关联的值。如果未找到名称，则返回你提供的默认值。</summary>
    /// <returns>与 <paramref name="name" /> 关联的值，按指定的 <paramref name="options" /> 对其进行处理；如果未找到 <paramref name="name" />，则为 <paramref name="defaultValue" />。</returns>
    /// <param name="name">要检索的值的名称。此字符串不区分大小写。</param>
    /// <param name="defaultValue">当 <paramref name="name" /> 不存在时返回的值。</param>
    /// <param name="options">枚举值之一，它指定对所检索值的可选处理方式。</param>
    /// <exception cref="T:System.Security.SecurityException">该用户没有读取注册表项所需的权限。</exception>
    /// <exception cref="T:System.ObjectDisposedException">包含指定值的 <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问关闭的项）。</exception>
    /// <exception cref="T:System.IO.IOException">包含指定值的 <see cref="T:Microsoft.Win32.RegistryKey" /> 已标记为删除。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 不是有效的 <see cref="T:Microsoft.Win32.RegistryValueOptions" /> 值；例如，无效值将强制转换为 <see cref="T:Microsoft.Win32.RegistryValueOptions" />。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">用户没有必需的注册表权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public object GetValue(string name, object defaultValue, RegistryValueOptions options)
    {
      if (options < RegistryValueOptions.None || options > RegistryValueOptions.DoNotExpandEnvironmentNames)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) options), "options");
      bool doNotExpand = options == RegistryValueOptions.DoNotExpandEnvironmentNames;
      this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckValueReadPermission, name, false, RegistryKeyPermissionCheck.Default);
      return this.InternalGetValue(name, defaultValue, doNotExpand, true);
    }

    [SecurityCritical]
    internal object InternalGetValue(string name, object defaultValue, bool doNotExpand, bool checkSecurity)
    {
      if (checkSecurity)
        this.EnsureNotDisposed();
      object obj = defaultValue;
      int lpType = 0;
      int lpcbData1 = 0;
      int num1 = Win32Native.RegQueryValueEx(this.hkey, name, (int[]) null, ref lpType, (byte[]) null, ref lpcbData1);
      if (num1 != 0)
      {
        if (this.IsPerfDataKey())
        {
          int length = 65000;
          int lpcbData2 = length;
          byte[] lpData;
          int errorCode;
          for (lpData = new byte[length]; 234 == (errorCode = Win32Native.RegQueryValueEx(this.hkey, name, (int[]) null, ref lpType, lpData, ref lpcbData2)); lpData = new byte[length])
          {
            if (length == int.MaxValue)
              this.Win32Error(errorCode, name);
            else if (length > 1073741823)
              length = int.MaxValue;
            else
              length *= 2;
            lpcbData2 = length;
          }
          if (errorCode != 0)
            this.Win32Error(errorCode, name);
          return (object) lpData;
        }
        if (num1 != 234)
          return obj;
      }
      if (lpcbData1 < 0)
        lpcbData1 = 0;
      int num2;
      switch (lpType)
      {
        case 0:
        case 3:
        case 5:
          byte[] lpData1 = new byte[lpcbData1];
          num2 = Win32Native.RegQueryValueEx(this.hkey, name, (int[]) null, ref lpType, lpData1, ref lpcbData1);
          obj = (object) lpData1;
          break;
        case 1:
          if (lpcbData1 % 2 == 1)
          {
            try
            {
              checked { ++lpcbData1; }
            }
            catch (OverflowException ex)
            {
              throw new IOException(Environment.GetResourceString("Arg_RegGetOverflowBug"), (Exception) ex);
            }
          }
          char[] lpData2 = new char[lpcbData1 / 2];
          num2 = Win32Native.RegQueryValueEx(this.hkey, name, (int[]) null, ref lpType, lpData2, ref lpcbData1);
          if (lpData2.Length != 0)
          {
            char[] chArray = lpData2;
            int index = chArray.Length - 1;
            if ((int) chArray[index] == 0)
            {
              obj = (object) new string(lpData2, 0, lpData2.Length - 1);
              break;
            }
          }
          obj = (object) new string(lpData2);
          break;
        case 2:
          if (lpcbData1 % 2 == 1)
          {
            try
            {
              checked { ++lpcbData1; }
            }
            catch (OverflowException ex)
            {
              throw new IOException(Environment.GetResourceString("Arg_RegGetOverflowBug"), (Exception) ex);
            }
          }
          char[] lpData3 = new char[lpcbData1 / 2];
          num2 = Win32Native.RegQueryValueEx(this.hkey, name, (int[]) null, ref lpType, lpData3, ref lpcbData1);
          if (lpData3.Length != 0)
          {
            char[] chArray = lpData3;
            int index = chArray.Length - 1;
            if ((int) chArray[index] == 0)
            {
              obj = (object) new string(lpData3, 0, lpData3.Length - 1);
              goto label_39;
            }
          }
          obj = (object) new string(lpData3);
label_39:
          if (!doNotExpand)
          {
            obj = (object) Environment.ExpandEnvironmentVariables((string) obj);
            break;
          }
          break;
        case 4:
          if (lpcbData1 <= 4)
          {
            int lpData4 = 0;
            num2 = Win32Native.RegQueryValueEx(this.hkey, name, (int[]) null, ref lpType, ref lpData4, ref lpcbData1);
            obj = (object) lpData4;
            break;
          }
          goto case 11;
        case 7:
          if (lpcbData1 % 2 == 1)
          {
            try
            {
              checked { ++lpcbData1; }
            }
            catch (OverflowException ex)
            {
              throw new IOException(Environment.GetResourceString("Arg_RegGetOverflowBug"), (Exception) ex);
            }
          }
          char[] lpData5 = new char[lpcbData1 / 2];
          int num3 = Win32Native.RegQueryValueEx(this.hkey, name, (int[]) null, ref lpType, lpData5, ref lpcbData1);
          if (lpData5.Length != 0)
          {
            char[] chArray1 = lpData5;
            int index1 = chArray1.Length - 1;
            if ((int) chArray1[index1] != 0)
            {
              try
              {
                char[] chArray2 = new char[checked (lpData5.Length + 1)];
                for (int index2 = 0; index2 < lpData5.Length; ++index2)
                  chArray2[index2] = lpData5[index2];
                char[] chArray3 = chArray2;
                int index3 = chArray3.Length - 1;
                int num4 = 0;
                chArray3[index3] = (char) num4;
                lpData5 = chArray2;
              }
              catch (OverflowException ex)
              {
                throw new IOException(Environment.GetResourceString("Arg_RegGetOverflowBug"), (Exception) ex);
              }
              char[] chArray4 = lpData5;
              int index4 = chArray4.Length - 1;
              int num5 = 0;
              chArray4[index4] = (char) num5;
            }
          }
          IList<string> stringList = (IList<string>) new List<string>();
          int startIndex = 0;
          int index5;
          for (int length = lpData5.Length; num3 == 0 && startIndex < length; startIndex = index5 + 1)
          {
            index5 = startIndex;
            while (index5 < length && (int) lpData5[index5] != 0)
              ++index5;
            if (index5 < length)
            {
              if (index5 - startIndex > 0)
                stringList.Add(new string(lpData5, startIndex, index5 - startIndex));
              else if (index5 != length - 1)
                stringList.Add(string.Empty);
            }
            else
              stringList.Add(new string(lpData5, startIndex, length - startIndex));
          }
          obj = (object) new string[stringList.Count];
          stringList.CopyTo((string[]) obj, 0);
          break;
        case 11:
          if (lpcbData1 <= 8)
          {
            long lpData4 = 0;
            num2 = Win32Native.RegQueryValueEx(this.hkey, name, (int[]) null, ref lpType, ref lpData4, ref lpcbData1);
            obj = (object) lpData4;
            break;
          }
          goto case 0;
      }
      return obj;
    }

    /// <summary>检索与指定名称关联的值的注册表数据类型。</summary>
    /// <returns>与 <paramref name="name" /> 关联的值的注册表数据类型。</returns>
    /// <param name="name">要检索其注册表数据类型的值的名称。此字符串不区分大小写。</param>
    /// <exception cref="T:System.Security.SecurityException">该用户没有读取注册表项所需的权限。</exception>
    /// <exception cref="T:System.ObjectDisposedException">包含指定值的 <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问关闭的项）。</exception>
    /// <exception cref="T:System.IO.IOException">包含指定值的子项不存在。- 或 -由 <paramref name="name" /> 指定的名称/值对不存在。在 Windows 95、Windows 98 或 Windows Millennium Edition 中不引发此异常。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">用户没有必需的注册表权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public RegistryValueKind GetValueKind(string name)
    {
      this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckValueReadPermission, name, false, RegistryKeyPermissionCheck.Default);
      this.EnsureNotDisposed();
      int lpType = 0;
      int lpcbData = 0;
      int errorCode = Win32Native.RegQueryValueEx(this.hkey, name, (int[]) null, ref lpType, (byte[]) null, ref lpcbData);
      if (errorCode != 0)
        this.Win32Error(errorCode, (string) null);
      if (lpType == 0)
        return RegistryValueKind.None;
      if (!Enum.IsDefined(typeof (RegistryValueKind), (object) lpType))
        return RegistryValueKind.Unknown;
      return (RegistryValueKind) lpType;
    }

    private bool IsDirty()
    {
      return (uint) (this.state & 1) > 0U;
    }

    private bool IsSystemKey()
    {
      return (uint) (this.state & 2) > 0U;
    }

    private bool IsWritable()
    {
      return (uint) (this.state & 4) > 0U;
    }

    private bool IsPerfDataKey()
    {
      return (uint) (this.state & 8) > 0U;
    }

    private void SetDirty()
    {
      this.state = this.state | 1;
    }

    /// <summary>设置指定的名称/值对。</summary>
    /// <param name="name">要存储的值的名称。</param>
    /// <param name="value">要存储的数据。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="value" /> 不是受支持的数据类型。</exception>
    /// <exception cref="T:System.ObjectDisposedException">包含指定值的 <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问关闭的项）。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <see cref="T:Microsoft.Win32.RegistryKey" /> 只读，因此无法写入；例如，项不是用写访问权限打开的。- 或 -<see cref="T:Microsoft.Win32.RegistryKey" /> 对象表示根级别节点，操作系统为 Windows Millennium Edition 或 Windows 98。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有创建或修改注册表项所需的权限。</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <see cref="T:Microsoft.Win32.RegistryKey" /> 对象表示根级别节点，操作系统为 Windows 2000、Windows XP 或 Windows Server 2003。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public void SetValue(string name, object value)
    {
      this.SetValue(name, value, RegistryValueKind.Unknown);
    }

    /// <summary>使用指定的注册表数据类型设置注册表项中的名称/值对的值。</summary>
    /// <param name="name">要存储的值的名称。</param>
    /// <param name="value">要存储的数据。</param>
    /// <param name="valueKind">在存储数据时要使用的注册表数据类型。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="value" /> 的类型与 <paramref name="valueKind" /> 指定的注册表数据类型不匹配，因此，未能正确转换该数据。</exception>
    /// <exception cref="T:System.ObjectDisposedException">包含指定值的 <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问关闭的项）。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <see cref="T:Microsoft.Win32.RegistryKey" /> 只读，因此无法写入；例如，项不是用写访问权限打开的。- 或 -<see cref="T:Microsoft.Win32.RegistryKey" /> 对象表示根级别节点，操作系统为 Windows Millennium Edition 或 Windows 98。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有创建或修改注册表项所需的权限。</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <see cref="T:Microsoft.Win32.RegistryKey" /> 对象表示根级别节点，操作系统为 Windows 2000、Windows XP 或 Windows Server 2003。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public unsafe void SetValue(string name, object value, RegistryValueKind valueKind)
    {
      if (value == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
      if (name != null && name.Length > 16383)
        throw new ArgumentException(Environment.GetResourceString("Arg_RegValStrLenBug"));
      if (!Enum.IsDefined(typeof (RegistryValueKind), (object) valueKind))
        throw new ArgumentException(Environment.GetResourceString("Arg_RegBadKeyKind"), "valueKind");
      this.EnsureWriteable();
      if (!this.remoteKey && this.ContainsRegistryValue(name))
        this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckValueWritePermission, name, false, RegistryKeyPermissionCheck.Default);
      else
        this.CheckPermission(RegistryKey.RegistryInternalCheck.CheckValueCreatePermission, name, false, RegistryKeyPermissionCheck.Default);
      if (valueKind == RegistryValueKind.Unknown)
        valueKind = this.CalculateValueKind(value);
      int errorCode = 0;
      try
      {
        switch (valueKind)
        {
          case RegistryValueKind.None:
          case RegistryValueKind.Binary:
            byte[] numArray = (byte[]) value;
            SafeRegistryHandle hKey1 = this.hkey;
            string lpValueName1 = name;
            int Reserved1 = 0;
            int num1 = valueKind == RegistryValueKind.None ? 0 : 3;
            byte[] lpData1 = numArray;
            int length = lpData1.Length;
            errorCode = Win32Native.RegSetValueEx(hKey1, lpValueName1, Reserved1, (RegistryValueKind) num1, lpData1, length);
            break;
          case RegistryValueKind.String:
          case RegistryValueKind.ExpandString:
            string @string = value.ToString();
            SafeRegistryHandle hKey2 = this.hkey;
            string lpValueName2 = name;
            int Reserved2 = 0;
            int num2 = (int) valueKind;
            string lpData2 = @string;
            int cbData1 = checked (lpData2.Length * 2 + 2);
            errorCode = Win32Native.RegSetValueEx(hKey2, lpValueName2, Reserved2, (RegistryValueKind) num2, lpData2, cbData1);
            break;
          case RegistryValueKind.DWord:
            int int32 = Convert.ToInt32(value, (IFormatProvider) CultureInfo.InvariantCulture);
            errorCode = Win32Native.RegSetValueEx(this.hkey, name, 0, RegistryValueKind.DWord, ref int32, 4);
            break;
          case RegistryValueKind.MultiString:
            string[] strArray = (string[]) ((Array) value).Clone();
            int num3 = 0;
            for (int index = 0; index < strArray.Length; ++index)
            {
              if (strArray[index] == null)
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSetStrArrNull);
              checked { num3 += (strArray[index].Length + 1) * 2; }
            }
            int cbData2 = checked (num3 + 2);
            byte[] lpData3 = new byte[cbData2];
            fixed (byte* numPtr = lpData3)
            {
              IntPtr dest = new IntPtr((void*) numPtr);
              for (int index = 0; index < strArray.Length; ++index)
              {
                string.InternalCopy(strArray[index], dest, checked (strArray[index].Length * 2));
                dest = new IntPtr((long) dest + (long) checked (strArray[index].Length * 2));
                *(short*) dest.ToPointer() = (short) 0;
                dest = new IntPtr((long) dest + 2L);
              }
              *(short*) dest.ToPointer() = (short) 0;
              dest = new IntPtr((long) dest + 2L);
              errorCode = Win32Native.RegSetValueEx(this.hkey, name, 0, RegistryValueKind.MultiString, lpData3, cbData2);
              break;
            }
          case RegistryValueKind.QWord:
            long int64 = Convert.ToInt64(value, (IFormatProvider) CultureInfo.InvariantCulture);
            errorCode = Win32Native.RegSetValueEx(this.hkey, name, 0, RegistryValueKind.QWord, ref int64, 8);
            break;
        }
      }
      catch (OverflowException ex)
      {
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSetMismatchedKind);
      }
      catch (InvalidOperationException ex)
      {
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSetMismatchedKind);
      }
      catch (FormatException ex)
      {
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSetMismatchedKind);
      }
      catch (InvalidCastException ex)
      {
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSetMismatchedKind);
      }
      if (errorCode == 0)
        this.SetDirty();
      else
        this.Win32Error(errorCode, (string) null);
    }

    private RegistryValueKind CalculateValueKind(object value)
    {
      if (value is int)
        return RegistryValueKind.DWord;
      if (!(value is Array))
        return RegistryValueKind.String;
      if (value is byte[])
        return RegistryValueKind.Binary;
      if (value is string[])
        return RegistryValueKind.MultiString;
      throw new ArgumentException(Environment.GetResourceString("Arg_RegSetBadArrType", (object) value.GetType().Name));
    }

    /// <summary>检索此项的字符串表示形式。</summary>
    /// <returns>表示此项的字符串。如果指定的项无效（找不到），则返回 null。</returns>
    /// <exception cref="T:System.ObjectDisposedException">访问的 <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问关闭的项）。</exception>
    [SecuritySafeCritical]
    public override string ToString()
    {
      this.EnsureNotDisposed();
      return this.keyName;
    }

    /// <summary>返回当前注册表项的访问控制安全性。</summary>
    /// <returns>一个对象，该对象描述针对由当前 <see cref="T:Microsoft.Win32.RegistryKey" /> 表示的注册表项的访问控制权限。</returns>
    /// <exception cref="T:System.Security.SecurityException">用户没有必要的权限。</exception>
    /// <exception cref="T:System.ObjectDisposedException">要操作的 <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问关闭的项）。</exception>
    /// <exception cref="T:System.InvalidOperationException">当前项已被删除。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public RegistrySecurity GetAccessControl()
    {
      return this.GetAccessControl(AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
    }

    /// <summary>返回当前注册表项的访问控制安全性的指定部分。</summary>
    /// <returns>一个对象，该对象描述针对由当前 <see cref="T:Microsoft.Win32.RegistryKey" /> 表示的注册表项的访问控制权限。</returns>
    /// <param name="includeSections">枚举值的按位组合，它指定要获取的安全信息类型。</param>
    /// <exception cref="T:System.Security.SecurityException">用户没有必要的权限。</exception>
    /// <exception cref="T:System.ObjectDisposedException">要操作的 <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问关闭的项）。</exception>
    /// <exception cref="T:System.InvalidOperationException">当前项已被删除。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public RegistrySecurity GetAccessControl(AccessControlSections includeSections)
    {
      this.EnsureNotDisposed();
      return new RegistrySecurity(this.hkey, this.keyName, includeSections);
    }

    /// <summary>向现有注册表项应用 Windows 访问控制安全性。</summary>
    /// <param name="registrySecurity">要应用于当前子项的访问控制安全性。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">当前的 <see cref="T:Microsoft.Win32.RegistryKey" /> 对象表示一个具有访问控制安全性的项，并且调用方没有 <see cref="F:System.Security.AccessControl.RegistryRights.ChangePermissions" /> 权限。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="registrySecurity" /> 为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">要操作的 <see cref="T:Microsoft.Win32.RegistryKey" /> 已关闭（无法访问关闭的项）。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public void SetAccessControl(RegistrySecurity registrySecurity)
    {
      this.EnsureWriteable();
      if (registrySecurity == null)
        throw new ArgumentNullException("registrySecurity");
      registrySecurity.Persist(this.hkey, this.keyName);
    }

    [SecuritySafeCritical]
    internal void Win32Error(int errorCode, string str)
    {
      switch (errorCode)
      {
        case 2:
          throw new IOException(Environment.GetResourceString("Arg_RegKeyNotFound"), errorCode);
        case 5:
          if (str != null)
            throw new UnauthorizedAccessException(Environment.GetResourceString("UnauthorizedAccess_RegistryKeyGeneric_Key", (object) str));
          throw new UnauthorizedAccessException();
        case 6:
          if (!this.IsPerfDataKey())
          {
            this.hkey.SetHandleAsInvalid();
            this.hkey = (SafeRegistryHandle) null;
            break;
          }
          break;
      }
      throw new IOException(Win32Native.GetMessage(errorCode), errorCode);
    }

    [SecuritySafeCritical]
    internal static void Win32ErrorStatic(int errorCode, string str)
    {
      if (errorCode != 5)
        throw new IOException(Win32Native.GetMessage(errorCode), errorCode);
      if (str != null)
        throw new UnauthorizedAccessException(Environment.GetResourceString("UnauthorizedAccess_RegistryKeyGeneric_Key", (object) str));
      throw new UnauthorizedAccessException();
    }

    internal static string FixupName(string name)
    {
      if (name.IndexOf('\\') == -1)
        return name;
      StringBuilder path = new StringBuilder(name);
      RegistryKey.FixupPath(path);
      int index = path.Length - 1;
      if (index >= 0 && (int) path[index] == 92)
        path.Length = index;
      return path.ToString();
    }

    private static void FixupPath(StringBuilder path)
    {
      int length = path.Length;
      bool flag = false;
      char ch = char.MaxValue;
      for (int index = 1; index < length - 1; ++index)
      {
        if ((int) path[index] == 92)
        {
          ++index;
          while (index < length && (int) path[index] == 92)
          {
            path[index] = ch;
            ++index;
            flag = true;
          }
        }
      }
      if (!flag)
        return;
      int index1 = 0;
      int index2 = 0;
      while (index1 < length)
      {
        if ((int) path[index1] == (int) ch)
        {
          ++index1;
        }
        else
        {
          path[index2] = path[index1];
          ++index1;
          ++index2;
        }
      }
      path.Length += index2 - index1;
    }

    private void GetSubKeyReadPermission(string subkeyName, out RegistryPermissionAccess access, out string path)
    {
      access = RegistryPermissionAccess.Read;
      path = this.keyName + "\\" + subkeyName + "\\.";
    }

    private void GetSubKeyWritePermission(string subkeyName, out RegistryPermissionAccess access, out string path)
    {
      access = RegistryPermissionAccess.Write;
      path = this.keyName + "\\" + subkeyName + "\\.";
    }

    private void GetSubKeyCreatePermission(string subkeyName, out RegistryPermissionAccess access, out string path)
    {
      access = RegistryPermissionAccess.Create;
      path = this.keyName + "\\" + subkeyName + "\\.";
    }

    private void GetSubTreeReadPermission(string subkeyName, out RegistryPermissionAccess access, out string path)
    {
      access = RegistryPermissionAccess.Read;
      path = this.keyName + "\\" + subkeyName + "\\";
    }

    private void GetSubTreeWritePermission(string subkeyName, out RegistryPermissionAccess access, out string path)
    {
      access = RegistryPermissionAccess.Write;
      path = this.keyName + "\\" + subkeyName + "\\";
    }

    private void GetSubTreeReadWritePermission(string subkeyName, out RegistryPermissionAccess access, out string path)
    {
      access = RegistryPermissionAccess.Read | RegistryPermissionAccess.Write;
      path = this.keyName + "\\" + subkeyName;
    }

    private void GetValueReadPermission(string valueName, out RegistryPermissionAccess access, out string path)
    {
      access = RegistryPermissionAccess.Read;
      path = this.keyName + "\\" + valueName;
    }

    private void GetValueWritePermission(string valueName, out RegistryPermissionAccess access, out string path)
    {
      access = RegistryPermissionAccess.Write;
      path = this.keyName + "\\" + valueName;
    }

    private void GetValueCreatePermission(string valueName, out RegistryPermissionAccess access, out string path)
    {
      access = RegistryPermissionAccess.Create;
      path = this.keyName + "\\" + valueName;
    }

    private void GetKeyReadPermission(out RegistryPermissionAccess access, out string path)
    {
      access = RegistryPermissionAccess.Read;
      path = this.keyName + "\\.";
    }

    [SecurityCritical]
    private void CheckPermission(RegistryKey.RegistryInternalCheck check, string item, bool subKeyWritable, RegistryKeyPermissionCheck subKeyCheck)
    {
      bool flag = false;
      RegistryPermissionAccess access = RegistryPermissionAccess.NoAccess;
      string path = (string) null;
      if (CodeAccessSecurityEngine.QuickCheckForAllDemands())
        return;
      switch (check)
      {
        case RegistryKey.RegistryInternalCheck.CheckSubKeyWritePermission:
          if (this.remoteKey)
          {
            RegistryKey.CheckUnmanagedCodePermission();
            break;
          }
          if (this.checkMode == RegistryKeyPermissionCheck.Default)
          {
            flag = true;
            this.GetSubKeyWritePermission(item, out access, out path);
            break;
          }
          break;
        case RegistryKey.RegistryInternalCheck.CheckSubKeyReadPermission:
          if (this.remoteKey)
          {
            RegistryKey.CheckUnmanagedCodePermission();
            break;
          }
          flag = true;
          this.GetSubKeyReadPermission(item, out access, out path);
          break;
        case RegistryKey.RegistryInternalCheck.CheckSubKeyCreatePermission:
          if (this.remoteKey)
          {
            RegistryKey.CheckUnmanagedCodePermission();
            break;
          }
          if (this.checkMode == RegistryKeyPermissionCheck.Default)
          {
            flag = true;
            this.GetSubKeyCreatePermission(item, out access, out path);
            break;
          }
          break;
        case RegistryKey.RegistryInternalCheck.CheckSubTreeReadPermission:
          if (this.remoteKey)
          {
            RegistryKey.CheckUnmanagedCodePermission();
            break;
          }
          if (this.checkMode == RegistryKeyPermissionCheck.Default)
          {
            flag = true;
            this.GetSubTreeReadPermission(item, out access, out path);
            break;
          }
          break;
        case RegistryKey.RegistryInternalCheck.CheckSubTreeWritePermission:
          if (this.remoteKey)
          {
            RegistryKey.CheckUnmanagedCodePermission();
            break;
          }
          if (this.checkMode == RegistryKeyPermissionCheck.Default)
          {
            flag = true;
            this.GetSubTreeWritePermission(item, out access, out path);
            break;
          }
          break;
        case RegistryKey.RegistryInternalCheck.CheckSubTreeReadWritePermission:
          if (this.remoteKey)
          {
            RegistryKey.CheckUnmanagedCodePermission();
            break;
          }
          flag = true;
          this.GetSubTreeReadWritePermission(item, out access, out path);
          break;
        case RegistryKey.RegistryInternalCheck.CheckValueWritePermission:
          if (this.remoteKey)
          {
            RegistryKey.CheckUnmanagedCodePermission();
            break;
          }
          if (this.checkMode == RegistryKeyPermissionCheck.Default)
          {
            flag = true;
            this.GetValueWritePermission(item, out access, out path);
            break;
          }
          break;
        case RegistryKey.RegistryInternalCheck.CheckValueCreatePermission:
          if (this.remoteKey)
          {
            RegistryKey.CheckUnmanagedCodePermission();
            break;
          }
          if (this.checkMode == RegistryKeyPermissionCheck.Default)
          {
            flag = true;
            this.GetValueCreatePermission(item, out access, out path);
            break;
          }
          break;
        case RegistryKey.RegistryInternalCheck.CheckValueReadPermission:
          if (this.checkMode == RegistryKeyPermissionCheck.Default)
          {
            flag = true;
            this.GetValueReadPermission(item, out access, out path);
            break;
          }
          break;
        case RegistryKey.RegistryInternalCheck.CheckKeyReadPermission:
          if (this.checkMode == RegistryKeyPermissionCheck.Default)
          {
            flag = true;
            this.GetKeyReadPermission(out access, out path);
            break;
          }
          break;
        case RegistryKey.RegistryInternalCheck.CheckSubTreePermission:
          if (subKeyCheck == RegistryKeyPermissionCheck.ReadSubTree)
          {
            if (this.checkMode == RegistryKeyPermissionCheck.Default)
            {
              if (this.remoteKey)
              {
                RegistryKey.CheckUnmanagedCodePermission();
                break;
              }
              flag = true;
              this.GetSubTreeReadPermission(item, out access, out path);
              break;
            }
            break;
          }
          if (subKeyCheck == RegistryKeyPermissionCheck.ReadWriteSubTree && this.checkMode != RegistryKeyPermissionCheck.ReadWriteSubTree)
          {
            if (this.remoteKey)
            {
              RegistryKey.CheckUnmanagedCodePermission();
              break;
            }
            flag = true;
            this.GetSubTreeReadWritePermission(item, out access, out path);
            break;
          }
          break;
        case RegistryKey.RegistryInternalCheck.CheckOpenSubKeyWithWritablePermission:
          if (this.checkMode == RegistryKeyPermissionCheck.Default)
          {
            if (this.remoteKey)
            {
              RegistryKey.CheckUnmanagedCodePermission();
              break;
            }
            flag = true;
            this.GetSubKeyReadPermission(item, out access, out path);
            break;
          }
          if (subKeyWritable && this.checkMode == RegistryKeyPermissionCheck.ReadSubTree)
          {
            if (this.remoteKey)
            {
              RegistryKey.CheckUnmanagedCodePermission();
              break;
            }
            flag = true;
            this.GetSubTreeReadWritePermission(item, out access, out path);
            break;
          }
          break;
        case RegistryKey.RegistryInternalCheck.CheckOpenSubKeyPermission:
          if (subKeyCheck == RegistryKeyPermissionCheck.Default && this.checkMode == RegistryKeyPermissionCheck.Default)
          {
            if (this.remoteKey)
            {
              RegistryKey.CheckUnmanagedCodePermission();
              break;
            }
            flag = true;
            this.GetSubKeyReadPermission(item, out access, out path);
            break;
          }
          break;
      }
      if (!flag)
        return;
      new RegistryPermission(access, path).Demand();
    }

    [SecurityCritical]
    private static void CheckUnmanagedCodePermission()
    {
      new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
    }

    [SecurityCritical]
    private bool ContainsRegistryValue(string name)
    {
      int lpType = 0;
      int lpcbData = 0;
      return Win32Native.RegQueryValueEx(this.hkey, name, (int[]) null, ref lpType, (byte[]) null, ref lpcbData) == 0;
    }

    [SecurityCritical]
    private void EnsureNotDisposed()
    {
      if (this.hkey != null)
        return;
      ThrowHelper.ThrowObjectDisposedException(this.keyName, ExceptionResource.ObjectDisposed_RegKeyClosed);
    }

    [SecurityCritical]
    private void EnsureWriteable()
    {
      this.EnsureNotDisposed();
      if (this.IsWritable())
        return;
      ThrowHelper.ThrowUnauthorizedAccessException(ExceptionResource.UnauthorizedAccess_RegistryNoWrite);
    }

    private static int GetRegistryKeyAccess(bool isWritable)
    {
      return isWritable ? 131103 : 131097;
    }

    private static int GetRegistryKeyAccess(RegistryKeyPermissionCheck mode)
    {
      int num = 0;
      switch (mode)
      {
        case RegistryKeyPermissionCheck.Default:
        case RegistryKeyPermissionCheck.ReadSubTree:
          num = 131097;
          break;
        case RegistryKeyPermissionCheck.ReadWriteSubTree:
          num = 131103;
          break;
      }
      return num;
    }

    private RegistryKeyPermissionCheck GetSubKeyPermissonCheck(bool subkeyWritable)
    {
      if (this.checkMode == RegistryKeyPermissionCheck.Default)
        return this.checkMode;
      return subkeyWritable ? RegistryKeyPermissionCheck.ReadWriteSubTree : RegistryKeyPermissionCheck.ReadSubTree;
    }

    private static void ValidateKeyName(string name)
    {
      if (name == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.name);
      int num = name.IndexOf("\\", StringComparison.OrdinalIgnoreCase);
      int startIndex = 0;
      for (; num != -1; num = name.IndexOf("\\", startIndex, StringComparison.OrdinalIgnoreCase))
      {
        if (num - startIndex > (int) byte.MaxValue)
          ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegKeyStrLenBug);
        startIndex = num + 1;
      }
      if (name.Length - startIndex <= (int) byte.MaxValue)
        return;
      ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegKeyStrLenBug);
    }

    private static void ValidateKeyMode(RegistryKeyPermissionCheck mode)
    {
      if (mode >= RegistryKeyPermissionCheck.Default && mode <= RegistryKeyPermissionCheck.ReadWriteSubTree)
        return;
      ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidRegistryKeyPermissionCheck, ExceptionArgument.mode);
    }

    private static void ValidateKeyOptions(RegistryOptions options)
    {
      if (options >= RegistryOptions.None && options <= RegistryOptions.Volatile)
        return;
      ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidRegistryOptionsCheck, ExceptionArgument.options);
    }

    private static void ValidateKeyView(RegistryView view)
    {
      if (view == RegistryView.Default || view == RegistryView.Registry32 || view == RegistryView.Registry64)
        return;
      ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidRegistryViewCheck, ExceptionArgument.view);
    }

    private static void ValidateKeyRights(int rights)
    {
      if ((rights & -983104) == 0)
        return;
      ThrowHelper.ThrowSecurityException(ExceptionResource.Security_RegistryPermission);
    }

    private enum RegistryInternalCheck
    {
      CheckSubKeyWritePermission,
      CheckSubKeyReadPermission,
      CheckSubKeyCreatePermission,
      CheckSubTreeReadPermission,
      CheckSubTreeWritePermission,
      CheckSubTreeReadWritePermission,
      CheckValueWritePermission,
      CheckValueCreatePermission,
      CheckValueReadPermission,
      CheckKeyReadPermission,
      CheckSubTreePermission,
      CheckOpenSubKeyWithWritablePermission,
      CheckOpenSubKeyPermission,
    }
  }
}
