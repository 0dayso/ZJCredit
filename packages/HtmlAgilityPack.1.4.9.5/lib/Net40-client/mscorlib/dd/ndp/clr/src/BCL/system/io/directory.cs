// Decompiled with JetBrains decompiler
// Type: System.IO.Directory
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;

namespace System.IO
{
  /// <summary>公开用于通过目录和子目录进行创建、移动和枚举的静态方法。此类不能被继承。若要浏览此类型的.NET Framework 源代码，请参阅参考源。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  public static class Directory
  {
    private const int FILE_ATTRIBUTE_DIRECTORY = 16;
    private const int GENERIC_WRITE = 1073741824;
    private const int FILE_SHARE_WRITE = 2;
    private const int FILE_SHARE_DELETE = 4;
    private const int OPEN_EXISTING = 3;
    private const int FILE_FLAG_BACKUP_SEMANTICS = 33554432;

    /// <summary>检索指定路径的父目录，包括绝对路径和相对路径。</summary>
    /// <returns>父目录；或者如果 <paramref name="path" /> 是根目录，包括 UNC 服务器或共享名的根，则为 null。</returns>
    /// <param name="path">用于检索父目录的路径。</param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 指定的目录是只读的。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">未找到指定的路径。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static DirectoryInfo GetParent(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_PathEmpty"), "path");
      string directoryName = Path.GetDirectoryName(Path.GetFullPathInternal(path));
      if (directoryName == null)
        return (DirectoryInfo) null;
      return new DirectoryInfo(directoryName);
    }

    /// <summary>在指定路径中创建所有目录和子目录，除非它们已经存在。</summary>
    /// <returns>一个表示在指定路径的目录的对象。无论指定路径的目录是否已经存在，都将返回此对象。</returns>
    /// <param name="path">要创建的目录。</param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 指定的目录是个文件。- 或 -网络名称未知。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。- 或 -<paramref name="path" /> 带有冒号字符 (:) 前缀，或仅包含一个冒号字符 (:)。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。 </exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 包含一个冒号字符 (:)，该冒号字符不是驱动器标签（“C:\”）的一部分。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static DirectoryInfo CreateDirectory(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_PathEmpty"));
      return Directory.InternalCreateDirectoryHelper(path, true);
    }

    [SecurityCritical]
    internal static DirectoryInfo UnsafeCreateDirectory(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_PathEmpty"));
      return Directory.InternalCreateDirectoryHelper(path, false);
    }

    [SecurityCritical]
    internal static DirectoryInfo InternalCreateDirectoryHelper(string path, bool checkHost)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      int num1 = 1;
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, Directory.GetDemandDir(fullPathInternal, num1 != 0), false, false);
      string path1 = path;
      // ISSUE: variable of the null type
      __Null local = null;
      int num2 = checkHost ? 1 : 0;
      Directory.InternalCreateDirectory(fullPathInternal, path1, (object) local, num2 != 0);
      int num3 = 0;
      return new DirectoryInfo(fullPathInternal, num3 != 0);
    }

    /// <summary>在指定路径中创建所有目录（除非已存在），并应用指定的 Windows 安全性。</summary>
    /// <returns>一个表示在指定路径的目录的对象。无论指定路径的目录是否已经存在，都将返回此对象。</returns>
    /// <param name="path">要创建的目录。</param>
    /// <param name="directorySecurity">要应用于此目录的访问控制。</param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 指定的目录是个文件。- 或 -网络名称未知。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。- 或 -<paramref name="path" /> 带有冒号字符 (:) 前缀，或仅包含一个冒号字符 (:)。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。 </exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 包含一个冒号字符 (:)，该冒号字符不是驱动器标签（“C:\”）的一部分。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static DirectoryInfo CreateDirectory(string path, DirectorySecurity directorySecurity)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_PathEmpty"));
      string fullPathInternal = Path.GetFullPathInternal(path);
      int num1 = 1;
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, Directory.GetDemandDir(fullPathInternal, num1 != 0), false, false);
      string path1 = path;
      DirectorySecurity directorySecurity1 = directorySecurity;
      Directory.InternalCreateDirectory(fullPathInternal, path1, (object) directorySecurity1);
      int num2 = 0;
      return new DirectoryInfo(fullPathInternal, num2 != 0);
    }

    internal static string GetDemandDir(string fullPath, bool thisDirOnly)
    {
      return !thisDirOnly ? (fullPath.EndsWith(Path.DirectorySeparatorChar) || fullPath.EndsWith(Path.AltDirectorySeparatorChar) ? fullPath : fullPath + "\\") : (fullPath.EndsWith(Path.DirectorySeparatorChar) || fullPath.EndsWith(Path.AltDirectorySeparatorChar) ? fullPath + "." : fullPath + "\\.");
    }

    internal static void InternalCreateDirectory(string fullPath, string path, object dirSecurityObj)
    {
      Directory.InternalCreateDirectory(fullPath, path, dirSecurityObj, false);
    }

    [SecuritySafeCritical]
    internal static unsafe void InternalCreateDirectory(string fullPath, string path, object dirSecurityObj, bool checkHost)
    {
      DirectorySecurity directorySecurity = (DirectorySecurity) dirSecurityObj;
      int length = fullPath.Length;
      if (length >= 2 && Path.IsDirectorySeparator(fullPath[length - 1]))
        --length;
      int rootLength = Path.GetRootLength(fullPath);
      if (length == 2 && Path.IsDirectorySeparator(fullPath[1]))
        throw new IOException(Environment.GetResourceString("IO.IO_CannotCreateDirectory", (object) path));
      if (Directory.InternalExists(fullPath))
        return;
      List<string> stringList1 = new List<string>();
      bool flag1 = false;
      if (length > rootLength)
      {
        for (int index = length - 1; index >= rootLength && !flag1; --index)
        {
          string path1 = fullPath.Substring(0, index + 1);
          if (!Directory.InternalExists(path1))
            stringList1.Add(path1);
          else
            flag1 = true;
          while (index > rootLength && (int) fullPath[index] != (int) Path.DirectorySeparatorChar && (int) fullPath[index] != (int) Path.AltDirectorySeparatorChar)
            --index;
        }
      }
      int count = stringList1.Count;
      if (stringList1.Count != 0)
      {
        string[] strArray = new string[stringList1.Count];
        stringList1.CopyTo(strArray, 0);
        for (int index = 0; index < strArray.Length; ++index)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          string& local = @strArray[index];
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          ^local = ^local + "\\.";
        }
        new FileIOPermission(FileIOPermissionAccess.Write, directorySecurity == null ? AccessControlActions.None : AccessControlActions.Change, strArray, false, false).Demand();
      }
      Win32Native.SECURITY_ATTRIBUTES securityAttributes = (Win32Native.SECURITY_ATTRIBUTES) null;
      if (directorySecurity != null)
      {
        securityAttributes = new Win32Native.SECURITY_ATTRIBUTES();
        securityAttributes.nLength = Marshal.SizeOf<Win32Native.SECURITY_ATTRIBUTES>(securityAttributes);
        byte[] descriptorBinaryForm = directorySecurity.GetSecurityDescriptorBinaryForm();
        byte* pDest = stackalloc byte[descriptorBinaryForm.Length];
        Buffer.Memcpy(pDest, 0, descriptorBinaryForm, 0, descriptorBinaryForm.Length);
        securityAttributes.pSecurityDescriptor = pDest;
      }
      bool flag2 = true;
      int errorCode = 0;
      string maybeFullPath = path;
      while (stringList1.Count > 0)
      {
        List<string> stringList2 = stringList1;
        int index1 = stringList2.Count - 1;
        string str = stringList2[index1];
        List<string> stringList3 = stringList1;
        int index2 = stringList3.Count - 1;
        stringList3.RemoveAt(index2);
        if (str.Length >= 248)
          throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
        flag2 = Win32Native.CreateDirectory(str, securityAttributes);
        if (!flag2 && errorCode == 0)
        {
          int lastError = Marshal.GetLastWin32Error();
          if (lastError != 183)
            errorCode = lastError;
          else if (File.InternalExists(str) || !Directory.InternalExists(str, out lastError) && lastError == 5)
          {
            errorCode = lastError;
            try
            {
              new FileIOPermission(FileIOPermissionAccess.PathDiscovery, Directory.GetDemandDir(str, true)).Demand();
              maybeFullPath = str;
            }
            catch (SecurityException ex)
            {
            }
          }
        }
      }
      if (count == 0 && !flag1)
      {
        if (Directory.InternalExists(Directory.InternalGetDirectoryRoot(fullPath)))
          return;
        __Error.WinIOError(3, Directory.InternalGetDirectoryRoot(path));
      }
      else
      {
        if (flag2 || errorCode == 0)
          return;
        __Error.WinIOError(errorCode, maybeFullPath);
      }
    }

    /// <summary>确定给定路径是否引用磁盘上的现有目录。</summary>
    /// <returns>如果 <paramref name="path" /> 指向现有目录，则为 true；如果该目录不存在或者在确定指定文件是否存在时出错，则为 false。</returns>
    /// <param name="path">要测试的路径。 </param>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static bool Exists(string path)
    {
      return Directory.InternalExistsHelper(path, true);
    }

    [SecurityCritical]
    internal static bool UnsafeExists(string path)
    {
      return Directory.InternalExistsHelper(path, false);
    }

    [SecurityCritical]
    internal static bool InternalExistsHelper(string path, bool checkHost)
    {
      try
      {
        if (path == null || path.Length == 0)
          return false;
        string fullPathInternal = Path.GetFullPathInternal(path);
        int num = 1;
        FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, Directory.GetDemandDir(fullPathInternal, num != 0), false, false);
        return Directory.InternalExists(fullPathInternal);
      }
      catch (ArgumentException ex)
      {
      }
      catch (NotSupportedException ex)
      {
      }
      catch (SecurityException ex)
      {
      }
      catch (IOException ex)
      {
      }
      catch (UnauthorizedAccessException ex)
      {
      }
      return false;
    }

    [SecurityCritical]
    internal static bool InternalExists(string path)
    {
      int lastError = 0;
      return Directory.InternalExists(path, out lastError);
    }

    [SecurityCritical]
    internal static bool InternalExists(string path, out int lastError)
    {
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      lastError = File.FillAttributeInfo(path, ref data, false, true);
      if (lastError == 0 && data.fileAttributes != -1)
        return (uint) (data.fileAttributes & 16) > 0U;
      return false;
    }

    /// <summary>为指定的文件或目录设置创建日期和时间。</summary>
    /// <param name="path">要设置其创建日期和时间信息的文件或目录。</param>
    /// <param name="creationTime">上次写入到文件或目录的日期和时间。该值用本地时间表示。</param>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到指定的路径。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationTime" /> 指定超出该操作允许的日期或时间范围的值。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Windows NT 或更高版本。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static void SetCreationTime(string path, DateTime creationTime)
    {
      Directory.SetCreationTimeUtc(path, creationTime.ToUniversalTime());
    }

    /// <summary>设置指定文件或目录的创建日期和时间，其格式为协调通用时 (UTC)。</summary>
    /// <param name="path">要设置其创建日期和时间信息的文件或目录。</param>
    /// <param name="creationTimeUtc">目录或文件的创建日期和时间。该值用本地时间表示。</param>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到指定的路径。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationTime" /> 指定超出该操作允许的日期或时间范围的值。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Windows NT 或更高版本。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static unsafe void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
    {
      using (SafeFileHandle hFile = Directory.OpenHandle(path))
      {
        Win32Native.FILE_TIME fileTime = new Win32Native.FILE_TIME(creationTimeUtc.ToFileTimeUtc());
        if (Win32Native.SetFileTime(hFile, &fileTime, (Win32Native.FILE_TIME*) null, (Win32Native.FILE_TIME*) null))
          return;
        __Error.WinIOError(Marshal.GetLastWin32Error(), path);
      }
    }

    /// <summary>获取目录的创建日期和时间。</summary>
    /// <returns>一个设置为指定目录的创建日期和时间的结构。该值用本地时间表示。</returns>
    /// <param name="path">目录的路径。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static DateTime GetCreationTime(string path)
    {
      return File.GetCreationTime(path);
    }

    /// <summary>获取目录创建的日期和时间，其格式为协调通用时 (UTC)。</summary>
    /// <returns>一个设置为指定目录的创建日期和时间的结构。该值用 UTC 时间表示。</returns>
    /// <param name="path">目录的路径。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static DateTime GetCreationTimeUtc(string path)
    {
      return File.GetCreationTimeUtc(path);
    }

    /// <summary>设置上次写入目录的日期和时间。</summary>
    /// <param name="path">目录的路径。</param>
    /// <param name="lastWriteTime">上次写入目录的日期和时间。该值用本地时间表示。</param>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到指定的路径。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Windows NT 或更高版本。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="lastWriteTime" /> 指定超出该操作允许的日期或时间范围的值。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static void SetLastWriteTime(string path, DateTime lastWriteTime)
    {
      Directory.SetLastWriteTimeUtc(path, lastWriteTime.ToUniversalTime());
    }

    /// <summary>设置上次写入某个目录的日期和时间，其格式为协调通用时 (UTC)。</summary>
    /// <param name="path">目录的路径。</param>
    /// <param name="lastWriteTimeUtc">上次写入目录的日期和时间。该值用 UTC 时间表示。</param>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到指定的路径。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Windows NT 或更高版本。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="lastWriteTimeUtc" /> 指定超出该操作允许的日期或时间范围的值。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static unsafe void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
    {
      using (SafeFileHandle hFile = Directory.OpenHandle(path))
      {
        Win32Native.FILE_TIME fileTime = new Win32Native.FILE_TIME(lastWriteTimeUtc.ToFileTimeUtc());
        if (Win32Native.SetFileTime(hFile, (Win32Native.FILE_TIME*) null, (Win32Native.FILE_TIME*) null, &fileTime))
          return;
        __Error.WinIOError(Marshal.GetLastWin32Error(), path);
      }
    }

    /// <summary>返回上次写入指定文件或目录的日期和时间。</summary>
    /// <returns>一个结构，它被设置为上次写入指定文件或目录的日期和时间。该值用本地时间表示。</returns>
    /// <param name="path">要获取其修改日期和时间信息的文件或目录。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static DateTime GetLastWriteTime(string path)
    {
      return File.GetLastWriteTime(path);
    }

    /// <summary>返回上次写入指定文件或目录的日期和时间，其格式为协调通用时 (UTC)。</summary>
    /// <returns>一个结构，它被设置为上次写入指定文件或目录的日期和时间。该值用 UTC 时间表示。</returns>
    /// <param name="path">要获取其修改日期和时间信息的文件或目录。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static DateTime GetLastWriteTimeUtc(string path)
    {
      return File.GetLastWriteTimeUtc(path);
    }

    /// <summary>设置上次访问指定文件或目录的日期和时间。</summary>
    /// <param name="path">要设置其访问日期和时间信息的文件或目录。</param>
    /// <param name="lastAccessTime">一个对象，它包含要为 <paramref name="path" /> 的访问日期和时间设置的值。该值用本地时间表示。</param>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到指定的路径。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Windows NT 或更高版本。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="lastAccessTime" /> 指定超出该操作允许的日期或时间范围的值。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static void SetLastAccessTime(string path, DateTime lastAccessTime)
    {
      Directory.SetLastAccessTimeUtc(path, lastAccessTime.ToUniversalTime());
    }

    /// <summary>设置上次访问指定文件或目录的日期和时间，其格式为协调通用时 (UTC)。</summary>
    /// <param name="path">要设置其访问日期和时间信息的文件或目录。</param>
    /// <param name="lastAccessTimeUtc">一个对象，它包含要为 <paramref name="path" /> 的访问日期和时间设置的值。该值用 UTC 时间表示。</param>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到指定的路径。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Windows NT 或更高版本。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="lastAccessTimeUtc" /> 指定超出该操作允许的日期或时间范围的值。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static unsafe void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
    {
      using (SafeFileHandle hFile = Directory.OpenHandle(path))
      {
        Win32Native.FILE_TIME fileTime = new Win32Native.FILE_TIME(lastAccessTimeUtc.ToFileTimeUtc());
        if (Win32Native.SetFileTime(hFile, (Win32Native.FILE_TIME*) null, &fileTime, (Win32Native.FILE_TIME*) null))
          return;
        __Error.WinIOError(Marshal.GetLastWin32Error(), path);
      }
    }

    /// <summary>返回上次访问指定文件或目录的日期和时间。</summary>
    /// <returns>一个结构，它被设置为上次访问指定文件或目录的日期和时间。该值用本地时间表示。</returns>
    /// <param name="path">要获取其访问日期和时间信息的文件或目录。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 参数的格式无效。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static DateTime GetLastAccessTime(string path)
    {
      return File.GetLastAccessTime(path);
    }

    /// <summary>返回上次访问指定文件或目录的日期和时间，其格式为协调通用时 (UTC)。</summary>
    /// <returns>一个结构，它被设置为上次访问指定文件或目录的日期和时间。该值用 UTC 时间表示。</returns>
    /// <param name="path">要获取其访问日期和时间信息的文件或目录。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 参数的格式无效。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static DateTime GetLastAccessTimeUtc(string path)
    {
      return File.GetLastAccessTimeUtc(path);
    }

    /// <summary>获取一个 <see cref="T:System.Security.AccessControl.DirectorySecurity" /> 对象，该对象封装指定目录的访问控制列表 (ACL) 项。</summary>
    /// <returns>一个封装由 <paramref name="path" /> 参数描述的文件的访问控制规则的对象。</returns>
    /// <param name="path">包含 <see cref="T:System.Security.AccessControl.DirectorySecurity" /> 对象的目录的路径，该对象描述文件的访问控制列表 (ACL) 信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 参数为 null。</exception>
    /// <exception cref="T:System.IO.IOException">打开目录时发生 I/O 错误。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Windows 2000 或更高版本。</exception>
    /// <exception cref="T:System.SystemException">出现的一个系统级错误，如未能找到目录。特定异常可能是 <see cref="T:System.SystemException" /> 的子类。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 参数指定了一个只读目录。- 或 -当前平台不支持此操作。- 或 -调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static DirectorySecurity GetAccessControl(string path)
    {
      return new DirectorySecurity(path, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
    }

    /// <summary>获取一个 <see cref="T:System.Security.AccessControl.DirectorySecurity" /> 对象，它封装指定目录的指定类型的访问控制列表 (ACL) 项。</summary>
    /// <returns>一个封装由 <paramref name="path" /> 参数描述的文件的访问控制规则的对象。</returns>
    /// <param name="path">包含 <see cref="T:System.Security.AccessControl.DirectorySecurity" /> 对象的目录的路径，该对象描述文件的访问控制列表 (ACL) 信息。</param>
    /// <param name="includeSections">
    /// <see cref="T:System.Security.AccessControl.AccessControlSections" /> 值之一，它指定要接收的访问控制列表 (ACL) 信息的类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 参数为 null。</exception>
    /// <exception cref="T:System.IO.IOException">打开目录时发生 I/O 错误。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Windows 2000 或更高版本。</exception>
    /// <exception cref="T:System.SystemException">出现的一个系统级错误，如未能找到目录。特定异常可能是 <see cref="T:System.SystemException" /> 的子类。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 参数指定了一个只读目录。- 或 -当前平台不支持此操作。- 或 -调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static DirectorySecurity GetAccessControl(string path, AccessControlSections includeSections)
    {
      return new DirectorySecurity(path, includeSections);
    }

    /// <summary>将 <see cref="T:System.Security.AccessControl.DirectorySecurity" /> 对象描述的访问控制列表 (ACL) 项应用于指定的目录。</summary>
    /// <param name="path">要从中添加或移除访问控制列表 (ACL) 项的目录。</param>
    /// <param name="directorySecurity">一个 <see cref="T:System.Security.AccessControl.DirectorySecurity" /> 对象，该对象描述要应用于 <paramref name="path" /> 参数所描述的目录的 ACL 项。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="directorySecurity" /> 参数为 null。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">未能找到目录。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 无效。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">当前进程不具有访问由 <paramref name="path" /> 指定的目录的权限。- 或 -当前进程没有足够的特权设置 ACL 项。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Windows 2000 或更高版本。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static void SetAccessControl(string path, DirectorySecurity directorySecurity)
    {
      if (directorySecurity == null)
        throw new ArgumentNullException("directorySecurity");
      string fullPathInternal = Path.GetFullPathInternal(path);
      directorySecurity.Persist(fullPathInternal);
    }

    /// <summary>返回指定目录中文件的名称（包括其路径）。</summary>
    /// <returns>一个包含指定目录中的文件的完整名称（包含路径）的数组，如果未找到任何文件，则为空数组。</returns>
    /// <param name="path">要搜索的目录的相对或绝对路径。此字符串不区分大小写。</param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 是一个文件名。- 或 -发生了网络错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径找不到或者无效（例如，它位于未映射的驱动器上）。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static string[] GetFiles(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      return Directory.InternalGetFiles(path, "*", SearchOption.TopDirectoryOnly);
    }

    /// <summary>返回指定目录中与指定的搜索模式匹配的文件的名称（包含其路径）。</summary>
    /// <returns>指定目录中与指定的搜索模式匹配的文件的完整名称（包含路径）的数组；如果未找到任何文件，则为空数组。</returns>
    /// <param name="path">要搜索的目录的相对或绝对路径。此字符串不区分大小写。</param>
    /// <param name="searchPattern">要与 <paramref name="path" /> 中的文件名匹配的搜索字符串。此参数可以包含有效文本路径和通配符（* 和 ?）的组合（请参见“备注”），但不支持正则表达式。</param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 是一个文件名。- 或 -发生了网络错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 来查询无效字符。- 或 - <paramref name="searchPattern" /> 不包含有效模式。 </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 或 <paramref name="searchPattern" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径找不到或者无效（例如，它位于未映射的驱动器上）。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static string[] GetFiles(string path, string searchPattern)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (searchPattern == null)
        throw new ArgumentNullException("searchPattern");
      return Directory.InternalGetFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
    }

    /// <summary>返回指定目录中与指定的搜索模式匹配的文件的名称（包含其路径），使用某个值确定是否要搜索子目录。</summary>
    /// <returns>指定目录中与指定的搜索模式和选项匹配的文件的完整名称（包含路径）的数组；如果未找到任何文件，则为空数组。</returns>
    /// <param name="path">要搜索的目录的相对或绝对路径。此字符串不区分大小写。</param>
    /// <param name="searchPattern">要与 <paramref name="path" /> 中的文件名匹配的搜索字符串。此参数可以包含有效文本路径和通配符（* 和 ?）的组合（请参见“备注”），但不支持正则表达式。</param>
    /// <param name="searchOption">用于指定搜索操作是应包含所有子目录还是仅包含当前目录的枚举值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。- 或 - <paramref name="searchPattern" /> 不包含有效模式。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 或 <paramref name="searchpattern" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="searchOption" /> 不是有效的 <see cref="T:System.IO.SearchOption" /> 值。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径找不到或者无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 是一个文件名。- 或 -发生了网络错误。</exception>
    public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (searchPattern == null)
        throw new ArgumentNullException("searchPattern");
      if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
        throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      return Directory.InternalGetFiles(path, searchPattern, searchOption);
    }

    private static string[] InternalGetFiles(string path, string searchPattern, SearchOption searchOption)
    {
      string str = path;
      string searchPattern1 = searchPattern;
      int num1 = 1;
      int num2 = 0;
      int num3 = (int) searchOption;
      int num4 = 1;
      return Directory.InternalGetFileDirectoryNames(str, str, searchPattern1, num1 != 0, num2 != 0, (SearchOption) num3, num4 != 0);
    }

    [SecurityCritical]
    internal static string[] UnsafeGetFiles(string path, string searchPattern, SearchOption searchOption)
    {
      string str = path;
      string searchPattern1 = searchPattern;
      int num1 = 1;
      int num2 = 0;
      int num3 = (int) searchOption;
      int num4 = 0;
      return Directory.InternalGetFileDirectoryNames(str, str, searchPattern1, num1 != 0, num2 != 0, (SearchOption) num3, num4 != 0);
    }

    /// <summary>返回指定目录中的子目录的名称（包括其路径）。</summary>
    /// <returns>指定路径中子目录的完整名称（包含路径）的数组；如果未找到任何目录，则为空数组。</returns>
    /// <param name="path">要搜索的目录的相对或绝对路径。此字符串不区分大小写。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 是一个文件名。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static string[] GetDirectories(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      return Directory.InternalGetDirectories(path, "*", SearchOption.TopDirectoryOnly);
    }

    /// <summary>返回指定目录中与指定的搜索模式匹配的子目录的名称（包括其路径）。</summary>
    /// <returns>指定目录中与搜索模式匹配的子目录的完整名称（包含路径）的数组；如果未找到任何文件，则为空数组。</returns>
    /// <param name="path">要搜索的目录的相对或绝对路径。此字符串不区分大小写。</param>
    /// <param name="searchPattern">要与 <paramref name="path" /> 中的子目录的名称匹配的搜索字符串。此参数可以包含有效文本和通配符的组合（请参见“备注”），但不支持正则表达式。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 来查询无效字符。- 或 - <paramref name="searchPattern" /> 不包含有效模式。 </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 或 <paramref name="searchPattern" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 是一个文件名。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static string[] GetDirectories(string path, string searchPattern)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (searchPattern == null)
        throw new ArgumentNullException("searchPattern");
      return Directory.InternalGetDirectories(path, searchPattern, SearchOption.TopDirectoryOnly);
    }

    /// <summary>返回与在指定目录中的指定搜索模式匹配的子目录的名称（包括其路径），还可以选择地搜索子目录。</summary>
    /// <returns>与指定条件匹配的子目录的完整名称（包含路径）的数组；如果未找到任何目录，则为空数组。</returns>
    /// <param name="path">要搜索的目录的相对或绝对路径。此字符串不区分大小写。</param>
    /// <param name="searchPattern">要与 <paramref name="path" /> 中的子目录的名称匹配的搜索字符串。此参数可以包含有效文本和通配符的组合（请参见“备注”），但不支持正则表达式。</param>
    /// <param name="searchOption">用于指定搜索操作是应包含所有子目录还是仅包含当前目录的枚举值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。- 或 - <paramref name="searchPattern" /> 不包含有效模式。 </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 或 <paramref name="searchPattern" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="searchOption" /> 不是有效的 <see cref="T:System.IO.SearchOption" /> 值。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 是一个文件名。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    public static string[] GetDirectories(string path, string searchPattern, SearchOption searchOption)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (searchPattern == null)
        throw new ArgumentNullException("searchPattern");
      if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
        throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      return Directory.InternalGetDirectories(path, searchPattern, searchOption);
    }

    private static string[] InternalGetDirectories(string path, string searchPattern, SearchOption searchOption)
    {
      string str = path;
      string searchPattern1 = searchPattern;
      int num1 = 0;
      int num2 = 1;
      int num3 = (int) searchOption;
      int num4 = 1;
      return Directory.InternalGetFileDirectoryNames(str, str, searchPattern1, num1 != 0, num2 != 0, (SearchOption) num3, num4 != 0);
    }

    [SecurityCritical]
    internal static string[] UnsafeGetDirectories(string path, string searchPattern, SearchOption searchOption)
    {
      string str = path;
      string searchPattern1 = searchPattern;
      int num1 = 0;
      int num2 = 1;
      int num3 = (int) searchOption;
      int num4 = 0;
      return Directory.InternalGetFileDirectoryNames(str, str, searchPattern1, num1 != 0, num2 != 0, (SearchOption) num3, num4 != 0);
    }

    /// <summary>返回指定路径中的所有文件和子目录的名称。</summary>
    /// <returns>指定目录中的文件和子目录的名称的数组；如果找不到任何文件或子目录，则为空数组。</returns>
    /// <param name="path">要搜索的目录的相对或绝对路径。此字符串不区分大小写。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 来查询无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 是一个文件名。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static string[] GetFileSystemEntries(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      return Directory.InternalGetFileSystemEntries(path, "*", SearchOption.TopDirectoryOnly);
    }

    /// <summary>返回与指定路径中搜索模式匹配的文件名和目录名的数组。</summary>
    /// <returns>与指定的搜索条件匹配的文件名和目录名的数组；如果找不到任何文件或目录，则为空数组。</returns>
    /// <param name="path">要搜索的目录的相对或绝对路径。此字符串不区分大小写。</param>
    /// <param name="searchPattern">要与 <paramref name="path" /> 中的文件和目录的名称匹配的搜索字符串。此参数可以包含有效文本路径和通配符（* 和 ?）的组合（请参见“备注”），但不支持正则表达式。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。- 或 - <paramref name="searchPattern" /> 不包含有效模式。 </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 或 <paramref name="searchPattern" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 是一个文件名。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static string[] GetFileSystemEntries(string path, string searchPattern)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (searchPattern == null)
        throw new ArgumentNullException("searchPattern");
      return Directory.InternalGetFileSystemEntries(path, searchPattern, SearchOption.TopDirectoryOnly);
    }

    /// <summary>返回指定路径中与搜索模式匹配的所有文件名和目录名的数组，还可以搜索子目录。</summary>
    /// <returns>与指定的搜索条件匹配的文件名和目录名的数组；如果找不到任何文件或目录，则为空数组。</returns>
    /// <param name="path">要搜索的目录的相对或绝对路径。此字符串不区分大小写。</param>
    /// <param name="searchPattern">要与 <paramref name="path" /> 中的文件和目录的名称匹配的搜索字符串。此参数可以包含有效文本路径和通配符（* 和 ?）的组合（请参见“备注”），但不支持正则表达式。</param>
    /// <param name="searchOption">指定搜索操作是应仅包含当前目录还是应包含所有子目录的枚举值之一。默认值为 <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path " />是一个零长度字符串、仅包含空白或者包含无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。- 或 -<paramref name="searchPattern" /> 不包含有效模式。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。- 或 -<paramref name="searchPattern" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="searchOption" /> 不是有效的 <see cref="T:System.IO.SearchOption" /> 值。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="path" /> 无效，比如引用未映射的驱动器。 </exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 是一个文件名。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    public static string[] GetFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (searchPattern == null)
        throw new ArgumentNullException("searchPattern");
      if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
        throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      return Directory.InternalGetFileSystemEntries(path, searchPattern, searchOption);
    }

    private static string[] InternalGetFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
    {
      string str = path;
      string searchPattern1 = searchPattern;
      int num1 = 1;
      int num2 = 1;
      int num3 = (int) searchOption;
      int num4 = 1;
      return Directory.InternalGetFileDirectoryNames(str, str, searchPattern1, num1 != 0, num2 != 0, (SearchOption) num3, num4 != 0);
    }

    internal static string[] InternalGetFileDirectoryNames(string path, string userPathOriginal, string searchPattern, bool includeFiles, bool includeDirs, SearchOption searchOption, bool checkHost)
    {
      return new List<string>(FileSystemEnumerableFactory.CreateFileNameIterator(path, userPathOriginal, searchPattern, includeFiles, includeDirs, searchOption, checkHost)).ToArray();
    }

    /// <summary>返回指定路径中的目录名的可枚举集合。</summary>
    /// <returns>一个可枚举集合，它包含 <paramref name="path" /> 指定的目录中的目录的完整名称（包括路径）。</returns>
    /// <param name="path">要搜索的目录的相对或绝对路径。此字符串不区分大小写。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path " />是一个零长度字符串、仅包含空白或者包含无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。 </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="path" /> 无效，比如引用未映射的驱动器。 </exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 是一个文件名。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    public static IEnumerable<string> EnumerateDirectories(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      return Directory.InternalEnumerateDirectories(path, "*", SearchOption.TopDirectoryOnly);
    }

    /// <summary>返回指定路径中与搜索模式匹配的目录名的可枚举集合。</summary>
    /// <returns>一个可枚举集合，它包含 <paramref name="path" /> 指定的目录中与指定的搜索模式匹配的目录的完整名称（包括路径）。</returns>
    /// <param name="path">要搜索的目录的相对或绝对路径。此字符串不区分大小写。</param>
    /// <param name="searchPattern">要与 <paramref name="path" /> 中的目录名称匹配的搜索字符串。此参数可以包含有效文本路径和通配符（* 和 ?）的组合（请参见“备注”），但不支持正则表达式。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path " />是一个零长度字符串、仅包含空白或者包含无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。- 或 -<paramref name="searchPattern" /> 不包含有效模式。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。- 或 -<paramref name="searchPattern" /> 为 null。 </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="path" /> 无效，比如引用未映射的驱动器。 </exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 是一个文件名。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (searchPattern == null)
        throw new ArgumentNullException("searchPattern");
      return Directory.InternalEnumerateDirectories(path, searchPattern, SearchOption.TopDirectoryOnly);
    }

    /// <summary>返回指定路径中与搜索模式匹配的目录名称的可枚举集合，还可以搜索子目录。</summary>
    /// <returns>一个可枚举集合，它包含 <paramref name="path" /> 指定的目录中与指定的搜索模式和选项匹配的目录的完整名称（包括路径）。</returns>
    /// <param name="path">要搜索的目录的相对或绝对路径。此字符串不区分大小写。</param>
    /// <param name="searchPattern">要与 <paramref name="path" /> 中的目录名称匹配的搜索字符串。此参数可以包含有效文本路径和通配符（* 和 ?）的组合（请参见“备注”），但不支持正则表达式。</param>
    /// <param name="searchOption">指定搜索操作是应仅包含当前目录还是应包含所有子目录的枚举值之一。默认值为 <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path " />是一个零长度字符串、仅包含空白或者包含无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。- 或 -<paramref name="searchPattern" /> 不包含有效模式。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。- 或 -<paramref name="searchPattern" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="searchOption" /> 不是有效的 <see cref="T:System.IO.SearchOption" /> 值。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="path" /> 无效，比如引用未映射的驱动器。 </exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 是一个文件名。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (searchPattern == null)
        throw new ArgumentNullException("searchPattern");
      if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
        throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      return Directory.InternalEnumerateDirectories(path, searchPattern, searchOption);
    }

    private static IEnumerable<string> InternalEnumerateDirectories(string path, string searchPattern, SearchOption searchOption)
    {
      return Directory.EnumerateFileSystemNames(path, searchPattern, searchOption, false, true);
    }

    /// <summary>返回指定路径中的文件名的可枚举集合。</summary>
    /// <returns>一个可枚举集合，它包含 <paramref name="path" /> 指定的目录中的文件的完整名称（包括路径）。</returns>
    /// <param name="path">要搜索的目录的相对或绝对路径。此字符串不区分大小写。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path " />是一个零长度字符串、仅包含空白或者包含无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。 </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="path" /> 无效，比如引用未映射的驱动器。 </exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 是一个文件名。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    public static IEnumerable<string> EnumerateFiles(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      return Directory.InternalEnumerateFiles(path, "*", SearchOption.TopDirectoryOnly);
    }

    /// <summary>返回指定路径中与搜索模式匹配的文件名称的可枚举集合。</summary>
    /// <returns>一个可枚举集合，它包含 <paramref name="path" /> 指定的目录中与指定的搜索模式匹配的文件的完整名称（包括路径）。</returns>
    /// <param name="path">要搜索的目录的相对或绝对路径。此字符串不区分大小写。</param>
    /// <param name="searchPattern">要与 <paramref name="path" /> 中的文件名匹配的搜索字符串。此参数可以包含有效文本路径和通配符（* 和 ?）的组合（请参见“备注”），但不支持正则表达式。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path " />是一个零长度字符串、仅包含空白或者包含无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。- 或 -<paramref name="searchPattern" /> 不包含有效模式。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。- 或 -<paramref name="searchPattern" /> 为 null。 </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="path" /> 无效，比如引用未映射的驱动器。 </exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 是一个文件名。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    public static IEnumerable<string> EnumerateFiles(string path, string searchPattern)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (searchPattern == null)
        throw new ArgumentNullException("searchPattern");
      return Directory.InternalEnumerateFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
    }

    /// <summary>返回指定路径中与搜索模式匹配的文件名称的可枚举集合，还可以搜索子目录。</summary>
    /// <returns>一个可枚举集合，它包含 <paramref name="path" /> 指定的目录中与指定的搜索模式和选项匹配的文件的完整名称（包括路径）。</returns>
    /// <param name="path">要搜索的目录的相对或绝对路径。此字符串不区分大小写。</param>
    /// <param name="searchPattern">要与 <paramref name="path" /> 中的文件名匹配的搜索字符串。此参数可以包含有效文本路径和通配符（* 和 ?）的组合（请参见“备注”），但不支持正则表达式。</param>
    /// <param name="searchOption">指定搜索操作是应仅包含当前目录还是应包含所有子目录的枚举值之一。默认值为 <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path " />是一个零长度字符串、仅包含空白或者包含无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。- 或 -<paramref name="searchPattern" /> 不包含有效模式。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。- 或 -<paramref name="searchPattern" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="searchOption" /> 不是有效的 <see cref="T:System.IO.SearchOption" /> 值。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="path" /> 无效，比如引用未映射的驱动器。 </exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 是一个文件名。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    public static IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (searchPattern == null)
        throw new ArgumentNullException("searchPattern");
      if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
        throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      return Directory.InternalEnumerateFiles(path, searchPattern, searchOption);
    }

    private static IEnumerable<string> InternalEnumerateFiles(string path, string searchPattern, SearchOption searchOption)
    {
      return Directory.EnumerateFileSystemNames(path, searchPattern, searchOption, true, false);
    }

    /// <summary>返回指定路径中的文件名和目录名的可枚举集合。</summary>
    /// <returns>由 <paramref name="path" /> 指定的目录中的文件系统项的可枚举集合。</returns>
    /// <param name="path">要搜索的目录的相对或绝对路径。此字符串不区分大小写。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path " />是一个零长度字符串、仅包含空白或者包含无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。 </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="path" /> 无效，比如引用未映射的驱动器。 </exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 是一个文件名。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    public static IEnumerable<string> EnumerateFileSystemEntries(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      return Directory.InternalEnumerateFileSystemEntries(path, "*", SearchOption.TopDirectoryOnly);
    }

    /// <summary>返回指定路径中与搜索模式匹配的文件名和目录名的可枚举集合。</summary>
    /// <returns>由 <paramref name="path" /> 指定的目录中与指定搜索模式匹配的文件系统项的可枚举集合。</returns>
    /// <param name="path">要搜索的目录的相对或绝对路径。此字符串不区分大小写。</param>
    /// <param name="searchPattern">要与 <paramref name="path" /> 中的文件系统项的名称匹配的搜索字符串。此参数可以包含有效文本路径和通配符（* 和 ?）的组合（请参见“备注”），但不支持正则表达式。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path " />是一个零长度字符串、仅包含空白或者包含无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。- 或 -<paramref name="searchPattern" /> 不包含有效模式。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。- 或 -<paramref name="searchPattern" /> 为 null。 </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="path" /> 无效，比如引用未映射的驱动器。 </exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 是一个文件名。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    public static IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (searchPattern == null)
        throw new ArgumentNullException("searchPattern");
      return Directory.InternalEnumerateFileSystemEntries(path, searchPattern, SearchOption.TopDirectoryOnly);
    }

    /// <summary>返回指定路径中与搜索模式匹配的文件名称和目录名的可枚举集合，还可以搜索子目录。</summary>
    /// <returns>由 <paramref name="path" /> 指定的目录中与指定搜索模式和选项匹配的文件系统项的可枚举集合。</returns>
    /// <param name="path">要搜索的目录的相对或绝对路径。此字符串不区分大小写。</param>
    /// <param name="searchPattern">要与 <paramref name="path" /> 中的文件系统项匹配的搜索字符串。此参数可以包含有效文本路径和通配符（* 和 ?）的组合（请参见“备注”），但不支持正则表达式。</param>
    /// <param name="searchOption">指定搜索操作是应仅包含当前目录还是应包含所有子目录的枚举值之一。默认值为 <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path " />是一个零长度字符串、仅包含空白或者包含无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。- 或 -<paramref name="searchPattern" /> 不包含有效模式。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。- 或 -<paramref name="searchPattern" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="searchOption" /> 不是有效的 <see cref="T:System.IO.SearchOption" /> 值。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="path" /> 无效，比如引用未映射的驱动器。 </exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="path" /> 是一个文件名。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    public static IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (searchPattern == null)
        throw new ArgumentNullException("searchPattern");
      if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
        throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      return Directory.InternalEnumerateFileSystemEntries(path, searchPattern, searchOption);
    }

    private static IEnumerable<string> InternalEnumerateFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
    {
      return Directory.EnumerateFileSystemNames(path, searchPattern, searchOption, true, true);
    }

    private static IEnumerable<string> EnumerateFileSystemNames(string path, string searchPattern, SearchOption searchOption, bool includeFiles, bool includeDirs)
    {
      string str = path;
      string searchPattern1 = searchPattern;
      int num1 = includeFiles ? 1 : 0;
      int num2 = includeDirs ? 1 : 0;
      int num3 = (int) searchOption;
      int num4 = 1;
      return FileSystemEnumerableFactory.CreateFileNameIterator(str, str, searchPattern1, num1 != 0, num2 != 0, (SearchOption) num3, num4 != 0);
    }

    /// <summary>检索此计算机上格式为“&lt;驱动器号&gt;:\”的逻辑驱动器的名称。</summary>
    /// <returns>此计算机上的逻辑驱动器。</returns>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误（例如，磁盘错误）。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static string[] GetLogicalDrives()
    {
      new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
      int logicalDrives = Win32Native.GetLogicalDrives();
      if (logicalDrives == 0)
        __Error.WinIOError();
      uint num1 = (uint) logicalDrives;
      int length = 0;
      while ((int) num1 != 0)
      {
        if (((int) num1 & 1) != 0)
          ++length;
        num1 >>= 1;
      }
      string[] strArray = new string[length];
      char[] chArray = new char[3]{ 'A', ':', '\\' };
      uint num2 = (uint) logicalDrives;
      int num3 = 0;
      while ((int) num2 != 0)
      {
        if (((int) num2 & 1) != 0)
          strArray[num3++] = new string(chArray);
        num2 >>= 1;
        ++chArray[0];
      }
      return strArray;
    }

    /// <summary>返回指定路径的卷信息、根信息或两者同时返回。</summary>
    /// <returns>包含指定路径的卷信息、根信息或同时包括这两者的字符串。</returns>
    /// <param name="path">文件或目录的路径。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 来查询无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static string GetDirectoryRoot(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      string fullPathInternal = Path.GetFullPathInternal(path);
      string fullPath = fullPathInternal.Substring(0, Path.GetRootLength(fullPathInternal));
      int num = 1;
      FileIOPermission.QuickDemand(FileIOPermissionAccess.PathDiscovery, Directory.GetDemandDir(fullPath, num != 0), false, false);
      return fullPath;
    }

    internal static string InternalGetDirectoryRoot(string path)
    {
      if (path == null)
        return (string) null;
      return path.Substring(0, Path.GetRootLength(path));
    }

    /// <summary>获取应用程序的当前工作目录。</summary>
    /// <returns>包含当前工作目录的路径且不以反斜杠 (\) 结尾的字符串。</returns>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.NotSupportedException">操作系统为 Windows CE，该系统不具有当前目录功能。此方法在 .NET Compact Framework 中可用，但是当前并不支持。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static string GetCurrentDirectory()
    {
      return Directory.InternalGetCurrentDirectory(true);
    }

    [SecurityCritical]
    internal static string UnsafeGetCurrentDirectory()
    {
      return Directory.InternalGetCurrentDirectory(false);
    }

    [SecurityCritical]
    private static string InternalGetCurrentDirectory(bool checkHost)
    {
      StringBuilder stringBuilder = StringBuilderCache.Acquire(261);
      if (Win32Native.GetCurrentDirectory(stringBuilder.Capacity, stringBuilder) == 0)
        __Error.WinIOError();
      string @string = stringBuilder.ToString();
      if (@string.IndexOf('~') >= 0)
      {
        string path = @string;
        StringBuilder longPathBuffer = stringBuilder;
        int capacity = longPathBuffer.Capacity;
        int longPathName = Win32Native.GetLongPathName(path, longPathBuffer, capacity);
        if (longPathName == 0 || longPathName >= 260)
        {
          int errorCode = Marshal.GetLastWin32Error();
          if (longPathName >= 260)
            errorCode = 206;
          if (errorCode != 2 && errorCode != 3 && (errorCode != 1 && errorCode != 5))
            __Error.WinIOError(errorCode, string.Empty);
        }
        @string = stringBuilder.ToString();
      }
      StringBuilderCache.Release(stringBuilder);
      new FileIOPermission(FileIOPermissionAccess.PathDiscovery, new string[1]
      {
        Directory.GetDemandDir(@string, true)
      }, 0 != 0, 0 != 0).Demand();
      return @string;
    }

    /// <summary>将应用程序的当前工作目录设置为指定的目录。</summary>
    /// <param name="path">设置为当前工作目录的路径。</param>
    /// <exception cref="T:System.IO.IOException">发生了 I/O 错误。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有访问未委托的代码所需的权限。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到指定的路径。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">未找到指定的目录。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static void SetCurrentDirectory(string path)
    {
      if (path == null)
        throw new ArgumentNullException("value");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_PathEmpty"));
      if (path.Length >= 260)
        throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
      new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
      string fullPathInternal = Path.GetFullPathInternal(path);
      if (Win32Native.SetCurrentDirectory(fullPathInternal))
        return;
      int errorCode = Marshal.GetLastWin32Error();
      if (errorCode == 2)
        errorCode = 3;
      __Error.WinIOError(errorCode, fullPathInternal);
    }

    /// <summary>将文件或目录及其内容移到新位置。</summary>
    /// <param name="sourceDirName">要移动的文件或目录的路径。</param>
    /// <param name="destDirName">指向 <paramref name="sourceDirName" /> 的新位置的路径。如果 <paramref name="sourceDirName" /> 是一个文件，则 <paramref name="destDirName" /> 也必须是一个文件名。</param>
    /// <exception cref="T:System.IO.IOException">试图将一个目录移到不同的卷。- 或 - <paramref name="destDirName" /> 已存在。- 或 -<paramref name="sourceDirName" /> 和 <paramref name="destDirName" /> 参数引用相同的文件或目录。- 或 -另一个进程正在使用的目录或在其中一个文件。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="sourceDirName" /> or <paramref name="destDirName" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sourceDirName" /> 或 <paramref name="destDirName" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">由 <paramref name="sourceDirName" /> 指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static void Move(string sourceDirName, string destDirName)
    {
      Directory.InternalMove(sourceDirName, destDirName, true);
    }

    [SecurityCritical]
    internal static void UnsafeMove(string sourceDirName, string destDirName)
    {
      Directory.InternalMove(sourceDirName, destDirName, false);
    }

    [SecurityCritical]
    private static void InternalMove(string sourceDirName, string destDirName, bool checkHost)
    {
      if (sourceDirName == null)
        throw new ArgumentNullException("sourceDirName");
      if (sourceDirName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "sourceDirName");
      if (destDirName == null)
        throw new ArgumentNullException("destDirName");
      if (destDirName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destDirName");
      string fullPathInternal = Path.GetFullPathInternal(sourceDirName);
      string demandDir1 = Directory.GetDemandDir(fullPathInternal, false);
      if (demandDir1.Length >= 248)
        throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
      string demandDir2 = Directory.GetDemandDir(Path.GetFullPathInternal(destDirName), false);
      if (demandDir2.Length >= 248)
        throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, demandDir1, false, false);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, demandDir2, false, false);
      if (string.Compare(demandDir1, demandDir2, StringComparison.OrdinalIgnoreCase) == 0)
        throw new IOException(Environment.GetResourceString("IO.IO_SourceDestMustBeDifferent"));
      if (string.Compare(Path.GetPathRoot(demandDir1), Path.GetPathRoot(demandDir2), StringComparison.OrdinalIgnoreCase) != 0)
        throw new IOException(Environment.GetResourceString("IO.IO_SourceDestMustHaveSameRoot"));
      if (Win32Native.MoveFile(sourceDirName, destDirName))
        return;
      int errorCode = Marshal.GetLastWin32Error();
      if (errorCode == 2)
      {
        errorCode = 3;
        __Error.WinIOError(errorCode, fullPathInternal);
      }
      if (errorCode == 5)
        throw new IOException(Environment.GetResourceString("UnauthorizedAccess_IODenied_Path", (object) sourceDirName), Win32Native.MakeHRFromErrorCode(errorCode));
      __Error.WinIOError(errorCode, string.Empty);
    }

    /// <summary>从指定路径删除空目录。</summary>
    /// <param name="path">要移除的空目录的名称。此目录必须可写且为空。</param>
    /// <exception cref="T:System.IO.IOException">存在具有相同名称和 <paramref name="path" /> 指定的位置的文件。- 或 -该目录为应用程序的当前工作目录。- 或 -由 <paramref name="path" /> 指定的目录不为空。- 或 -该目录是只读的，或者包含只读文件。- 或 -该目录正被另一个进程使用。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。 </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="path" /> 不存在或未能找到。- 或 -指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static void Delete(string path)
    {
      Directory.Delete(Path.GetFullPathInternal(path), path, false, true);
    }

    /// <summary>删除指定的目录并（如果指示）删除该目录中的所有子目录和文件。</summary>
    /// <param name="path">要移除的目录的名称。 </param>
    /// <param name="recursive">若要移除 <paramref name="path" /> 中的目录、子目录和文件，则为 true；否则为 false。</param>
    /// <exception cref="T:System.IO.IOException">存在具有相同名称和 <paramref name="path" /> 指定的位置的文件。- 或 -<paramref name="path" /> 指定的目录是只读的，或者 <paramref name="recursive" /> 是 false 并且 <paramref name="path" /> 不是空目录。- 或 -该目录为应用程序的当前工作目录。- 或 -目录中包含一个只读文件。- 或 -该目录正被另一个进程使用。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个无效字符。可以使用 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法来查询无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="path" /> 不存在或未能找到。- 或 -指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static void Delete(string path, bool recursive)
    {
      Directory.Delete(Path.GetFullPathInternal(path), path, recursive, true);
    }

    [SecurityCritical]
    internal static void UnsafeDelete(string path, bool recursive)
    {
      Directory.Delete(Path.GetFullPathInternal(path), path, recursive, false);
    }

    [SecurityCritical]
    internal static void Delete(string fullPath, string userPath, bool recursive, bool checkHost)
    {
      new FileIOPermission(FileIOPermissionAccess.Write, new string[1]
      {
        Directory.GetDemandDir(fullPath, !recursive)
      }, 0 != 0, 0 != 0).Demand();
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      int errorCode = File.FillAttributeInfo(fullPath, ref data, false, true);
      switch (errorCode)
      {
        case 0:
          if ((data.fileAttributes & 1024) != 0)
            recursive = false;
          Directory.DeleteHelper(fullPath, userPath, recursive, true);
          return;
        case 2:
          errorCode = 3;
          break;
      }
      __Error.WinIOError(errorCode, fullPath);
      goto case 0;
    }

    [SecurityCritical]
    private static void DeleteHelper(string fullPath, string userPath, bool recursive, bool throwOnTopLevelDirectoryNotFound)
    {
      Exception exception = (Exception) null;
      if (recursive)
      {
        Win32Native.WIN32_FIND_DATA wiN32FindData = new Win32Native.WIN32_FIND_DATA();
        int lastWin32Error;
        using (SafeFindHandle firstFile = Win32Native.FindFirstFile(fullPath + "\\*", wiN32FindData))
        {
          if (firstFile.IsInvalid)
          {
            lastWin32Error = Marshal.GetLastWin32Error();
            __Error.WinIOError(lastWin32Error, fullPath);
          }
          do
          {
            if ((uint) (wiN32FindData.dwFileAttributes & 16) > 0U)
            {
              if (!wiN32FindData.cFileName.Equals(".") && !wiN32FindData.cFileName.Equals(".."))
              {
                if ((wiN32FindData.dwFileAttributes & 1024) == 0)
                {
                  string fullPath1 = Path.InternalCombine(fullPath, wiN32FindData.cFileName);
                  string userPath1 = Path.InternalCombine(userPath, wiN32FindData.cFileName);
                  try
                  {
                    Directory.DeleteHelper(fullPath1, userPath1, recursive, false);
                  }
                  catch (Exception ex)
                  {
                    if (exception == null)
                      exception = ex;
                  }
                }
                else
                {
                  if (wiN32FindData.dwReserved0 == -1610612733 && !Win32Native.DeleteVolumeMountPoint(Path.InternalCombine(fullPath, wiN32FindData.cFileName + Path.DirectorySeparatorChar.ToString())))
                  {
                    lastWin32Error = Marshal.GetLastWin32Error();
                    if (lastWin32Error != 3)
                    {
                      try
                      {
                        __Error.WinIOError(lastWin32Error, wiN32FindData.cFileName);
                      }
                      catch (Exception ex)
                      {
                        if (exception == null)
                          exception = ex;
                      }
                    }
                  }
                  if (!Win32Native.RemoveDirectory(Path.InternalCombine(fullPath, wiN32FindData.cFileName)))
                  {
                    lastWin32Error = Marshal.GetLastWin32Error();
                    if (lastWin32Error != 3)
                    {
                      try
                      {
                        __Error.WinIOError(lastWin32Error, wiN32FindData.cFileName);
                      }
                      catch (Exception ex)
                      {
                        if (exception == null)
                          exception = ex;
                      }
                    }
                  }
                }
              }
            }
            else if (!Win32Native.DeleteFile(Path.InternalCombine(fullPath, wiN32FindData.cFileName)))
            {
              lastWin32Error = Marshal.GetLastWin32Error();
              if (lastWin32Error != 2)
              {
                try
                {
                  __Error.WinIOError(lastWin32Error, wiN32FindData.cFileName);
                }
                catch (Exception ex)
                {
                  if (exception == null)
                    exception = ex;
                }
              }
            }
          }
          while (Win32Native.FindNextFile(firstFile, wiN32FindData));
          lastWin32Error = Marshal.GetLastWin32Error();
        }
        if (exception != null)
          throw exception;
        if (lastWin32Error != 0 && lastWin32Error != 18)
          __Error.WinIOError(lastWin32Error, userPath);
      }
      if (Win32Native.RemoveDirectory(fullPath))
        return;
      int errorCode = Marshal.GetLastWin32Error();
      if (errorCode == 2)
        errorCode = 3;
      if (errorCode == 5)
        throw new IOException(Environment.GetResourceString("UnauthorizedAccess_IODenied_Path", (object) userPath));
      if (errorCode == 3 && !throwOnTopLevelDirectoryNotFound)
        return;
      __Error.WinIOError(errorCode, fullPath);
    }

    [SecurityCritical]
    private static SafeFileHandle OpenHandle(string path)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      string pathRoot = Path.GetPathRoot(fullPathInternal);
      if (pathRoot == fullPathInternal && (int) pathRoot[1] == (int) Path.VolumeSeparatorChar)
        throw new ArgumentException(Environment.GetResourceString("Arg_PathIsVolume"));
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, Directory.GetDemandDir(fullPathInternal, true), false, false);
      SafeFileHandle file = Win32Native.SafeCreateFile(fullPathInternal, 1073741824, FileShare.Write | FileShare.Delete, (Win32Native.SECURITY_ATTRIBUTES) null, FileMode.Open, 33554432, IntPtr.Zero);
      if (!file.IsInvalid)
        return file;
      __Error.WinIOError(Marshal.GetLastWin32Error(), fullPathInternal);
      return file;
    }

    internal sealed class SearchData
    {
      public readonly string fullPath;
      public readonly string userPath;
      public readonly SearchOption searchOption;

      public SearchData(string fullPath, string userPath, SearchOption searchOption)
      {
        this.fullPath = fullPath;
        this.userPath = userPath;
        this.searchOption = searchOption;
      }
    }
  }
}
