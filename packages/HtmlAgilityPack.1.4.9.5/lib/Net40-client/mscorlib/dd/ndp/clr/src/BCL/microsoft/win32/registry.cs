// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.Registry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32
{
  /// <summary>提供表示 Windows 注册表中的根项的 <see cref="T:Microsoft.Win32.RegistryKey" /> 对象，并提供访问项/值对的 static 方法。</summary>
  [ComVisible(true)]
  public static class Registry
  {
    /// <summary>包含有关当前用户首选项的信息。该字段读取 Windows 注册表基项 HKEY_CURRENT_USER</summary>
    public static readonly RegistryKey CurrentUser = RegistryKey.GetBaseKey(RegistryKey.HKEY_CURRENT_USER);
    /// <summary>包含本地计算机的配置数据。该字段读取 Windows 注册表基项 HKEY_LOCAL_MACHINE。</summary>
    public static readonly RegistryKey LocalMachine = RegistryKey.GetBaseKey(RegistryKey.HKEY_LOCAL_MACHINE);
    /// <summary>定义文档的类型（或类）以及与那些类型关联的属性。该字段读取 Windows 注册表基项 HKEY_CLASSES_ROOT。</summary>
    public static readonly RegistryKey ClassesRoot = RegistryKey.GetBaseKey(RegistryKey.HKEY_CLASSES_ROOT);
    /// <summary>包含有关默认用户配置的信息。该字段读取 Windows 注册表基项 HKEY_USERS。</summary>
    public static readonly RegistryKey Users = RegistryKey.GetBaseKey(RegistryKey.HKEY_USERS);
    /// <summary>包含软件组件的性能信息。该字段读取 Windows 注册表基项 HKEY_PERFORMANCE_DATA。</summary>
    public static readonly RegistryKey PerformanceData = RegistryKey.GetBaseKey(RegistryKey.HKEY_PERFORMANCE_DATA);
    /// <summary>包含有关非用户特定的硬件的配置信息。该字段读取 Windows 注册表基项 HKEY_CURRENT_CONFIG。</summary>
    public static readonly RegistryKey CurrentConfig = RegistryKey.GetBaseKey(RegistryKey.HKEY_CURRENT_CONFIG);
    /// <summary>包含动态注册表数据。该字段读取 Windows 注册表基项 HKEY_DYN_DATA。</summary>
    /// <exception cref="T:System.ObjectDisposedException">操作系统不支持动态数据，即操作系统非 Windows 98、Windows 98 Second Edition 或 Windows Millennium Edition。</exception>
    [Obsolete("The DynData registry key only works on Win9x, which is no longer supported by the CLR.  On NT-based operating systems, use the PerformanceData registry key instead.")]
    public static readonly RegistryKey DynData = RegistryKey.GetBaseKey(RegistryKey.HKEY_DYN_DATA);

    [SecuritySafeCritical]
    static Registry()
    {
    }

    [SecurityCritical]
    private static RegistryKey GetBaseKeyFromKeyName(string keyName, out string subKeyName)
    {
      if (keyName == null)
        throw new ArgumentNullException("keyName");
      int length = keyName.IndexOf('\\');
      string s = length == -1 ? keyName.ToUpper(CultureInfo.InvariantCulture) : keyName.Substring(0, length).ToUpper(CultureInfo.InvariantCulture);
      // ISSUE: reference to a compiler-generated method
      uint stringHash = \u003CPrivateImplementationDetails\u003E.ComputeStringHash(s);
      RegistryKey registryKey;
      if (stringHash <= 1097425318U)
      {
        if ((int) stringHash != 126972219)
        {
          if ((int) stringHash != 457190004)
          {
            if ((int) stringHash == 1097425318 && s == "HKEY_CLASSES_ROOT")
            {
              registryKey = Registry.ClassesRoot;
              goto label_23;
            }
          }
          else if (s == "HKEY_LOCAL_MACHINE")
          {
            registryKey = Registry.LocalMachine;
            goto label_23;
          }
        }
        else if (s == "HKEY_CURRENT_CONFIG")
        {
          registryKey = Registry.CurrentConfig;
          goto label_23;
        }
      }
      else if (stringHash <= 1568329430U)
      {
        if ((int) stringHash != 1198714601)
        {
          if ((int) stringHash == 1568329430 && s == "HKEY_CURRENT_USER")
          {
            registryKey = Registry.CurrentUser;
            goto label_23;
          }
        }
        else if (s == "HKEY_USERS")
        {
          registryKey = Registry.Users;
          goto label_23;
        }
      }
      else if ((int) stringHash != -1471101685)
      {
        if ((int) stringHash == -739976840 && s == "HKEY_PERFORMANCE_DATA")
        {
          registryKey = Registry.PerformanceData;
          goto label_23;
        }
      }
      else if (s == "HKEY_DYN_DATA")
      {
        registryKey = RegistryKey.GetBaseKey(RegistryKey.HKEY_DYN_DATA);
        goto label_23;
      }
      throw new ArgumentException(Environment.GetResourceString("Arg_RegInvalidKeyName", (object) "keyName"));
label_23:
      subKeyName = length == -1 || length == keyName.Length ? string.Empty : keyName.Substring(length + 1, keyName.Length - length - 1);
      return registryKey;
    }

    /// <summary>检索与指定的注册表项中的指定名称关联的值。如果在指定的项中未找到该名称，则返回您提供的默认值；或者，如果指定的项不存在，则返回 null。</summary>
    /// <returns>如果由 <paramref name="keyName" /> 指定的子项不存在，则返回 null；否则，返回与 <paramref name="valueName" /> 关联的值；或者，如果未找到 <paramref name="valueName" />，则返回 <paramref name="defaultValue" />。</returns>
    /// <param name="keyName">以有效注册表根（如“HKEY_CURRENT_USER”）开头的键的完整注册表路径。</param>
    /// <param name="valueName">名称/值对的名称。</param>
    /// <param name="defaultValue">当 <paramref name="valueName" /> 不存在时返回的值。</param>
    /// <exception cref="T:System.Security.SecurityException">该用户没有读取注册表项所需的权限。</exception>
    /// <exception cref="T:System.IO.IOException">包含指定值的 <see cref="T:Microsoft.Win32.RegistryKey" /> 已标记为删除。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="keyName" /> 未以有效注册表根开头。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static object GetValue(string keyName, string valueName, object defaultValue)
    {
      string subKeyName;
      RegistryKey registryKey = Registry.GetBaseKeyFromKeyName(keyName, out subKeyName).OpenSubKey(subKeyName);
      if (registryKey == null)
        return (object) null;
      try
      {
        return registryKey.GetValue(valueName, defaultValue);
      }
      finally
      {
        registryKey.Close();
      }
    }

    /// <summary>设置指定的注册表项的指定名称/值对。如果指定的项不存在，则创建该项。</summary>
    /// <param name="keyName">以有效注册表根（如“HKEY_CURRENT_USER”）开头的键的完整注册表路径。</param>
    /// <param name="valueName">名称/值对的名称。</param>
    /// <param name="value">要存储的值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="keyName" /> 未以有效注册表根开头。- 或 -<paramref name="keyName" /> 的长度超过了允许的最大长度（255 个字符）。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <see cref="T:Microsoft.Win32.RegistryKey" /> 是只读的，因此无法对其写入；例如，它是根级节点。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有创建或修改注册表项所需的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static void SetValue(string keyName, string valueName, object value)
    {
      Registry.SetValue(keyName, valueName, value, RegistryValueKind.Unknown);
    }

    /// <summary>通过使用指定的注册表数据类型，设置该指定的注册表项的名称/值对。如果指定的项不存在，则创建该项。</summary>
    /// <param name="keyName">以有效注册表根（如“HKEY_CURRENT_USER”）开头的键的完整注册表路径。</param>
    /// <param name="valueName">名称/值对的名称。</param>
    /// <param name="value">要存储的值。</param>
    /// <param name="valueKind">在存储数据时使用的注册表数据类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="keyName" /> 未以有效注册表根开头。- 或 -<paramref name="keyName" /> 的长度超过了允许的最大长度（255 个字符）。- 或 -<paramref name="value" /> 的类型与 <paramref name="valueKind" /> 指定的注册表数据类型不匹配，因此，未能正确转换该数据。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <see cref="T:Microsoft.Win32.RegistryKey" /> 是只读的，因此无法对其写入（例如，它是根级节点，或者未用写访问权限打开该项）。</exception>
    /// <exception cref="T:System.Security.SecurityException">用户没有创建或修改注册表项所需的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static void SetValue(string keyName, string valueName, object value, RegistryValueKind valueKind)
    {
      string subKeyName;
      RegistryKey subKey = Registry.GetBaseKeyFromKeyName(keyName, out subKeyName).CreateSubKey(subKeyName);
      try
      {
        subKey.SetValue(valueName, value, valueKind);
      }
      finally
      {
        subKey.Close();
      }
    }
  }
}
