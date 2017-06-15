// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.FileSecurity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Security.Permissions;

namespace System.Security.AccessControl
{
  /// <summary>表示文件的访问控制和审核安全。此类不能被继承。</summary>
  public sealed class FileSecurity : FileSystemSecurity
  {
    /// <summary>初始化 <see cref="T:System.Security.AccessControl.FileSecurity" /> 类的新实例。</summary>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Microsoft Windows 2000 或更高版本。</exception>
    [SecuritySafeCritical]
    public FileSecurity()
      : base(false)
    {
    }

    /// <summary>使用 <see cref="T:System.Security.AccessControl.AccessControlSections" /> 枚举的指定值从指定文件初始化 <see cref="T:System.Security.AccessControl.FileSecurity" /> 类的新实例。</summary>
    /// <param name="fileName">一个文件的位置，<see cref="T:System.Security.AccessControl.FileSecurity" /> 对象将从该文件创建。</param>
    /// <param name="includeSections">
    /// <see cref="T:System.Security.AccessControl.AccessControlSections" /> 值之一，该值指定要检索的访问控制列表 (ACL) 信息的类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="fileName" /> 参数是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到 <paramref name="fileName" /> 参数中所指定的文件。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.Runtime.InteropServices.SEHException">
    /// <paramref name="fileName" /> 参数为 null。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Microsoft Windows 2000 或更高版本。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.Security.AccessControl.PrivilegeNotHeldException">当前系统帐户没有管理特权。</exception>
    /// <exception cref="T:System.SystemException">找不到文件。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="fileName" /> 参数指定了一个只读文件。- 或 -在当前平台上不支持此操作。- 或 -<paramref name="fileName" /> 参数指定了一个目录。- 或 -调用方没有所要求的权限。</exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
    public FileSecurity(string fileName, AccessControlSections includeSections)
      : base(false, fileName, includeSections, false)
    {
      new FileIOPermission(FileIOPermissionAccess.NoAccess, AccessControlActions.View, Path.GetFullPathInternal(fileName)).Demand();
    }

    [SecurityCritical]
    [SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
    internal FileSecurity(SafeFileHandle handle, string fullPath, AccessControlSections includeSections)
      : base(false, handle, includeSections, false)
    {
      if (fullPath != null)
        new FileIOPermission(FileIOPermissionAccess.NoAccess, AccessControlActions.View, fullPath).Demand();
      else
        new FileIOPermission(PermissionState.Unrestricted).Demand();
    }
  }
}
