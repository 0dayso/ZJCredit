// Decompiled with JetBrains decompiler
// Type: System.IO.File
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
  /// <summary>提供用于创建、复制、删除、移动和打开单一文件的静态方法，并协助创建 <see cref="T:System.IO.FileStream" /> 对象。若要浏览此类型的.NET Framework 源代码，请参阅 Reference Source。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  public static class File
  {
    private const int GetFileExInfoStandard = 0;
    private const int ERROR_INVALID_PARAMETER = 87;
    private const int ERROR_ACCESS_DENIED = 5;

    /// <summary>打开现有 UTF-8 编码文本文件以进行读取。</summary>
    /// <returns>指定路径上的 <see cref="T:System.IO.StreamReader" />。</returns>
    /// <param name="path">要打开以进行读取的文件。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到 <paramref name="path" /> 中指定的文件。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static StreamReader OpenText(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      return new StreamReader(path);
    }

    /// <summary>创建或打开用于写入 UTF-8 编码文本的文件。</summary>
    /// <returns>一个 <see cref="T:System.IO.StreamWriter" />，它使用 UTF-8 编码写入到指定的文件。</returns>
    /// <param name="path">要打开以进行写入的文件。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static StreamWriter CreateText(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      return new StreamWriter(path, false);
    }

    /// <summary>创建一个 <see cref="T:System.IO.StreamWriter" />，它将 UTF-8 编码文本追加到现有文件或新文件（如果指定文件不存在）。</summary>
    /// <returns>一个流写入器，它将 UTF-8 编码文本追加到指定文件或新文件。</returns>
    /// <param name="path">要向其中追加内容的文件的路径。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定路径无效（例如，目录不存在或位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static StreamWriter AppendText(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      return new StreamWriter(path, true);
    }

    /// <summary>将现有文件复制到新文件。不允许覆盖同名的文件。</summary>
    /// <param name="sourceFileName">要复制的文件。</param>
    /// <param name="destFileName">目标文件的名称。它不能是一个目录或现有文件。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="sourceFileName" /> 或 <paramref name="destFileName" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。- 或 - <paramref name="sourceFileName" /> 或 <paramref name="destFileName" /> 指定目录。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sourceFileName" /> 或 <paramref name="destFileName" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">在 <paramref name="sourceFileName" /> 或 <paramref name="destFileName" /> 中指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="sourceFileName" />。</exception>
    /// <exception cref="T:System.IO.IOException">已存在 <paramref name="destFileName" />。- 或 - 出现 I/O 错误。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="sourceFileName" /> 或 <paramref name="destFileName" /> 的格式无效。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static void Copy(string sourceFileName, string destFileName)
    {
      if (sourceFileName == null)
        throw new ArgumentNullException("sourceFileName", Environment.GetResourceString("ArgumentNull_FileName"));
      if (destFileName == null)
        throw new ArgumentNullException("destFileName", Environment.GetResourceString("ArgumentNull_FileName"));
      if (sourceFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "sourceFileName");
      if (destFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destFileName");
      File.InternalCopy(sourceFileName, destFileName, false, true);
    }

    /// <summary>将现有文件复制到新文件。允许覆盖同名的文件。</summary>
    /// <param name="sourceFileName">要复制的文件。</param>
    /// <param name="destFileName">目标文件的名称。不能是目录。</param>
    /// <param name="overwrite">如果可以覆盖目标文件，则为 true；否则为 false。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。- 或 -<paramref name="destFileName" /> 为只读。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="sourceFileName" /> 或 <paramref name="destFileName" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。- 或 - <paramref name="sourceFileName" /> 或 <paramref name="destFileName" /> 指定目录。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sourceFileName" /> 或 <paramref name="destFileName" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">在 <paramref name="sourceFileName" /> 或 <paramref name="destFileName" /> 中指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="sourceFileName" />。</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="destFileName" /> 存在并且 <paramref name="overwrite" /> 为 false.- 或 - 出现 I/O 错误。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="sourceFileName" /> 或 <paramref name="destFileName" /> 的格式无效。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static void Copy(string sourceFileName, string destFileName, bool overwrite)
    {
      if (sourceFileName == null)
        throw new ArgumentNullException("sourceFileName", Environment.GetResourceString("ArgumentNull_FileName"));
      if (destFileName == null)
        throw new ArgumentNullException("destFileName", Environment.GetResourceString("ArgumentNull_FileName"));
      if (sourceFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "sourceFileName");
      if (destFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destFileName");
      File.InternalCopy(sourceFileName, destFileName, overwrite, true);
    }

    [SecurityCritical]
    internal static void UnsafeCopy(string sourceFileName, string destFileName, bool overwrite)
    {
      if (sourceFileName == null)
        throw new ArgumentNullException("sourceFileName", Environment.GetResourceString("ArgumentNull_FileName"));
      if (destFileName == null)
        throw new ArgumentNullException("destFileName", Environment.GetResourceString("ArgumentNull_FileName"));
      if (sourceFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "sourceFileName");
      if (destFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destFileName");
      File.InternalCopy(sourceFileName, destFileName, overwrite, false);
    }

    [SecuritySafeCritical]
    internal static string InternalCopy(string sourceFileName, string destFileName, bool overwrite, bool checkHost)
    {
      string fullPathInternal1 = Path.GetFullPathInternal(sourceFileName);
      string fullPathInternal2 = Path.GetFullPathInternal(destFileName);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, fullPathInternal1, false, false);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, fullPathInternal2, false, false);
      if (!Win32Native.CopyFile(fullPathInternal1, fullPathInternal2, !overwrite))
      {
        int lastWin32Error = Marshal.GetLastWin32Error();
        string maybeFullPath = destFileName;
        if (lastWin32Error != 80)
        {
          using (SafeFileHandle file = Win32Native.UnsafeCreateFile(fullPathInternal1, int.MinValue, FileShare.Read, (Win32Native.SECURITY_ATTRIBUTES) null, FileMode.Open, 0, IntPtr.Zero))
          {
            if (file.IsInvalid)
              maybeFullPath = sourceFileName;
          }
          if (lastWin32Error == 5 && Directory.InternalExists(fullPathInternal2))
            throw new IOException(Environment.GetResourceString("Arg_FileIsDirectory_Name", (object) destFileName), 5, fullPathInternal2);
        }
        __Error.WinIOError(lastWin32Error, maybeFullPath);
      }
      return fullPathInternal2;
    }

    /// <summary>在指定路径中创建或覆盖文件。</summary>
    /// <returns>一个 <see cref="T:System.IO.FileStream" />，它提供对 <paramref name="path" /> 中指定的文件的读/写访问。</returns>
    /// <param name="path">要创建的文件的路径及名称。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。- 或 - <paramref name="path" /> 指定了一个只读文件。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.IOException">创建文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static FileStream Create(string path)
    {
      return File.Create(path, 4096);
    }

    /// <summary>创建或覆盖指定的文件。</summary>
    /// <returns>一个具有指定缓冲区大小的 <see cref="T:System.IO.FileStream" />，它提供对 <paramref name="path" /> 中指定的文件的读/写访问。</returns>
    /// <param name="path">文件的名称。</param>
    /// <param name="bufferSize">用于读取和写入到文件的已放入缓冲区的字节数。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。- 或 - <paramref name="path" /> 指定了一个只读文件。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.IOException">创建文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static FileStream Create(string path, int bufferSize)
    {
      return new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, bufferSize);
    }

    /// <summary>创建或覆盖指定的文件，指定缓冲区大小和一个描述如何创建或覆盖该文件的 <see cref="T:System.IO.FileOptions" /> 值。</summary>
    /// <returns>具有指定缓冲区大小的新文件。</returns>
    /// <param name="path">文件的名称。</param>
    /// <param name="bufferSize">用于读取和写入到文件的已放入缓冲区的字节数。</param>
    /// <param name="options">
    /// <see cref="T:System.IO.FileOptions" /> 值之一，它描述如何创建或覆盖该文件。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。- 或 - <paramref name="path" /> 指定了一个只读文件。- 或 -为 <see cref="F:System.IO.FileOptions.Encrypted" /> 指定了 <paramref name="options" />，但当前平台不支持文件加密。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.IOException">创建文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。- 或 - <paramref name="path" /> 指定了一个只读文件。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。- 或 - <paramref name="path" /> 指定了一个只读文件。</exception>
    public static FileStream Create(string path, int bufferSize, FileOptions options)
    {
      return new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, bufferSize, options);
    }

    /// <summary>创建或覆盖具有指定的缓冲区大小、文件选项和文件安全性的指定文件。</summary>
    /// <returns>具有指定的缓冲区大小、文件选项和文件安全性的新文件。</returns>
    /// <param name="path">文件的名称。</param>
    /// <param name="bufferSize">用于读取和写入到文件的已放入缓冲区的字节数。</param>
    /// <param name="options">
    /// <see cref="T:System.IO.FileOptions" /> 值之一，它描述如何创建或覆盖该文件。</param>
    /// <param name="fileSecurity">
    /// <see cref="T:System.Security.AccessControl.FileSecurity" /> 值之一，它确定文件的访问控制和审核安全性。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。- 或 - <paramref name="path" /> 指定了一个只读文件。- 或 -为 <see cref="F:System.IO.FileOptions.Encrypted" /> 指定了 <paramref name="options" />，但当前平台不支持文件加密。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.IOException">创建文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。- 或 - <paramref name="path" /> 指定了一个只读文件。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。- 或 - <paramref name="path" /> 指定了一个只读文件。</exception>
    public static FileStream Create(string path, int bufferSize, FileOptions options, FileSecurity fileSecurity)
    {
      return new FileStream(path, FileMode.Create, FileSystemRights.Read | FileSystemRights.Write, FileShare.None, bufferSize, options, fileSecurity);
    }

    /// <summary>删除指定的文件。</summary>
    /// <param name="path">要删除的文件的名称。该指令不支持通配符。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.IOException">指定的文件正在使用中。- 或 -对于文件有打开句柄，并且操作系统是 Windows XP 或更早版本。此打开句柄可能是由于枚举目录和文件导致的。有关详细信息，请参阅如何：枚举目录和文件。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。- 或 - 该文件是正在使用的可执行文件。- 或 - <paramref name="path" /> 是一个目录。- 或 - <paramref name="path" /> 指定一个只读文件。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static void Delete(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      File.InternalDelete(path, true);
    }

    [SecurityCritical]
    internal static void UnsafeDelete(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      File.InternalDelete(path, false);
    }

    [SecurityCritical]
    internal static void InternalDelete(string path, bool checkHost)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, fullPathInternal, false, false);
      if (Win32Native.DeleteFile(fullPathInternal))
        return;
      int lastWin32Error = Marshal.GetLastWin32Error();
      if (lastWin32Error == 2)
        return;
      __Error.WinIOError(lastWin32Error, fullPathInternal);
    }

    /// <summary>使用 <see cref="M:System.IO.File.Encrypt(System.String)" /> 方法解密由当前帐户加密的文件。</summary>
    /// <param name="path">描述要解密的文件的路径。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 参数是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 参数为 null。</exception>
    /// <exception cref="T:System.IO.DriveNotFoundException">指定了无效的驱动器。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到 <paramref name="path" /> 参数所描述的文件。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。例如，加密的文件已经打开。- 或 -当前平台不支持此操作。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Windows NT 或更高版本。</exception>
    /// <exception cref="T:System.NotSupportedException">文件系统不是 NTFS。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 参数指定了一个只读文件。- 或 - 当前平台不支持此操作。- 或 - <paramref name="path" /> 参数指定了一个目录。- 或 - 调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static void Decrypt(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      string fullPathInternal = Path.GetFullPathInternal(path);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, fullPathInternal, false, false);
      if (Win32Native.DecryptFile(fullPathInternal, 0))
        return;
      int lastWin32Error = Marshal.GetLastWin32Error();
      int num = 5;
      if (lastWin32Error == num && !string.Equals("NTFS", new DriveInfo(Path.GetPathRoot(fullPathInternal)).DriveFormat))
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_EncryptionNeedsNTFS"));
      string maybeFullPath = fullPathInternal;
      __Error.WinIOError(lastWin32Error, maybeFullPath);
    }

    /// <summary>将某个文件加密，使得只有加密该文件的帐户才能将其解密。</summary>
    /// <param name="path">描述要加密的文件的路径。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 参数是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 参数为 null。</exception>
    /// <exception cref="T:System.IO.DriveNotFoundException">指定了无效的驱动器。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到 <paramref name="path" /> 参数所描述的文件。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。- 或 -当前平台不支持此操作。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Windows NT 或更高版本。</exception>
    /// <exception cref="T:System.NotSupportedException">文件系统不是 NTFS。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 参数指定了一个只读文件。- 或 - 当前平台不支持此操作。- 或 - <paramref name="path" /> 参数指定了一个目录。- 或 - 调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static void Encrypt(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      string fullPathInternal = Path.GetFullPathInternal(path);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, fullPathInternal, false, false);
      if (Win32Native.EncryptFile(fullPathInternal))
        return;
      int lastWin32Error = Marshal.GetLastWin32Error();
      int num = 5;
      if (lastWin32Error == num && !string.Equals("NTFS", new DriveInfo(Path.GetPathRoot(fullPathInternal)).DriveFormat))
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_EncryptionNeedsNTFS"));
      string maybeFullPath = fullPathInternal;
      __Error.WinIOError(lastWin32Error, maybeFullPath);
    }

    /// <summary>确定指定的文件是否存在。</summary>
    /// <returns>如果调用方具有要求的权限并且 true 包含现有文件的名称，则为 <paramref name="path" />；否则为 false。如果 false 为 <paramref name="path" />（一个无效路径或零长度字符串）,则此方法也将返回 null。如果调用方不具有读取指定文件所需的足够权限，则不引发异常并且该方法返回 false，这与 <paramref name="path" /> 是否存在无关。</returns>
    /// <param name="path">要检查的文件。</param>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static bool Exists(string path)
    {
      return File.InternalExistsHelper(path, true);
    }

    [SecurityCritical]
    internal static bool UnsafeExists(string path)
    {
      return File.InternalExistsHelper(path, false);
    }

    [SecurityCritical]
    private static bool InternalExistsHelper(string path, bool checkHost)
    {
      try
      {
        if (path == null || path.Length == 0)
          return false;
        path = Path.GetFullPathInternal(path);
        if (path.Length > 0)
        {
          string str = path;
          int index = str.Length - 1;
          if (Path.IsDirectorySeparator(str[index]))
            return false;
        }
        FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, path, false, false);
        return File.InternalExists(path);
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
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      if (File.FillAttributeInfo(path, ref data, false, true) == 0 && data.fileAttributes != -1)
        return (data.fileAttributes & 16) == 0;
      return false;
    }

    /// <summary>以读/写访问权限打开指定路径上的 <see cref="T:System.IO.FileStream" />。</summary>
    /// <returns>以读/写访问与不共享权限打开的指定模式和路径上的 <see cref="T:System.IO.FileStream" />。</returns>
    /// <param name="path">要打开的文件。</param>
    /// <param name="mode">
    /// <see cref="T:System.IO.FileMode" /> 值，用于指定在文件不存在时是否创建该文件，并确定是保留还是覆盖现有文件的内容。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 指定了一个只读文件。- 或 - 当前平台不支持此操作。- 或 - <paramref name="path" /> 指定了一个目录。- 或 - 调用方没有所要求的权限。- 或 -<paramref name="mode" /> 为 <see cref="F:System.IO.FileMode.Create" />，指定文件为隐藏文件。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="mode" /> 指定了一个无效值。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到 <paramref name="path" /> 中指定的文件。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static FileStream Open(string path, FileMode mode)
    {
      string path1 = path;
      int num1 = (int) mode;
      int num2 = 6;
      int num3 = num1 == num2 ? 2 : 3;
      int num4 = 0;
      return File.Open(path1, (FileMode) num1, (FileAccess) num3, (FileShare) num4);
    }

    /// <summary>以指定的模式和访问权限打开指定路径上的 <see cref="T:System.IO.FileStream" />。</summary>
    /// <returns>一个非共享的 <see cref="T:System.IO.FileStream" />，它提供对指定文件的访问，并且具有指定的模式和访问权限。</returns>
    /// <param name="path">要打开的文件。</param>
    /// <param name="mode">
    /// <see cref="T:System.IO.FileMode" /> 值，用于指定在文件不存在时是否创建该文件，并确定是保留还是覆盖现有文件的内容。</param>
    /// <param name="access">一个 <see cref="T:System.IO.FileAccess" /> 值，它指定可以对文件执行的操作。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。- 或 - <paramref name="access" /> 指定了 Read，而 <paramref name="mode" /> 指定了 Create、CreateNew、Truncate 或 Append。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 指定了一个只读文件，而 <paramref name="access" /> 不为 Read。- 或 - <paramref name="path" /> 指定了一个目录。- 或 - 调用方没有所要求的权限。- 或 -<paramref name="mode" /> 为 <see cref="F:System.IO.FileMode.Create" />，指定文件为隐藏文件。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="mode" /> 或 <paramref name="access" /> 指定了一个无效值。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到 <paramref name="path" /> 中指定的文件。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static FileStream Open(string path, FileMode mode, FileAccess access)
    {
      return File.Open(path, mode, access, FileShare.None);
    }

    /// <summary>打开指定路径上的 <see cref="T:System.IO.FileStream" />，具有带读、写或读/写访问的指定模式和指定的共享选项。</summary>
    /// <returns>指定路径上的 <see cref="T:System.IO.FileStream" />，具有带读、写或读/写访问的指定模式以及指定的共享选项。</returns>
    /// <param name="path">要打开的文件。</param>
    /// <param name="mode">
    /// <see cref="T:System.IO.FileMode" /> 值，用于指定在文件不存在时是否创建该文件，并确定是保留还是覆盖现有文件的内容。</param>
    /// <param name="access">一个 <see cref="T:System.IO.FileAccess" /> 值，它指定可以对文件执行的操作。</param>
    /// <param name="share">一个 <see cref="T:System.IO.FileShare" /> 值，它指定其他线程所具有的对该文件的访问类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。- 或 - <paramref name="access" /> 指定了 Read，而 <paramref name="mode" /> 指定了 Create、CreateNew、Truncate 或 Append。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 指定了一个只读文件，而 <paramref name="access" /> 不为 Read。- 或 - <paramref name="path" /> 指定了一个目录。- 或 - 调用方没有所要求的权限。- 或 -<paramref name="mode" /> 为 <see cref="F:System.IO.FileMode.Create" />，指定文件为隐藏文件。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="mode" />、<paramref name="access" /> 或 <paramref name="share" /> 指定了一个无效值。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到 <paramref name="path" /> 中指定的文件。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static FileStream Open(string path, FileMode mode, FileAccess access, FileShare share)
    {
      return new FileStream(path, mode, access, share);
    }

    /// <summary>设置创建该文件的日期和时间。</summary>
    /// <param name="path">要设置其创建日期和时间信息的文件。</param>
    /// <param name="creationTime">一个 <see cref="T:System.DateTime" />，它包含要为 <paramref name="path" /> 的创建日期和时间设置的值。该值用本地时间表示。</param>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到指定的路径。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.IOException">执行操作时发生 I/O 错误。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationTime" /> 指定的值超出了该操作所允许的日期范围或时间范围，或同时超出了日期范围和时间范围。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static void SetCreationTime(string path, DateTime creationTime)
    {
      File.SetCreationTimeUtc(path, creationTime.ToUniversalTime());
    }

    /// <summary>设置文件创建的日期和时间，其格式为协调通用时 (UTC)。</summary>
    /// <param name="path">要设置其创建日期和时间信息的文件。</param>
    /// <param name="creationTimeUtc">一个 <see cref="T:System.DateTime" />，它包含要为 <paramref name="path" /> 的创建日期和时间设置的值。该值用 UTC 时间表示。</param>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到指定的路径。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.IOException">执行操作时发生 I/O 错误。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="creationTime" /> 指定的值超出了该操作所允许的日期范围或时间范围，或同时超出了日期范围和时间范围。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static unsafe void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
    {
      SafeFileHandle handle;
      using (File.OpenFile(path, FileAccess.Write, out handle))
      {
        Win32Native.FILE_TIME fileTime = new Win32Native.FILE_TIME(creationTimeUtc.ToFileTimeUtc());
        if (Win32Native.SetFileTime(handle, &fileTime, (Win32Native.FILE_TIME*) null, (Win32Native.FILE_TIME*) null))
          return;
        __Error.WinIOError(Marshal.GetLastWin32Error(), path);
      }
    }

    /// <summary>返回指定文件或目录的创建日期和时间。</summary>
    /// <returns>一个 <see cref="T:System.DateTime" /> 结构，它被设置为指定文件或目录的创建日期和时间。该值用本地时间表示。</returns>
    /// <param name="path">要获取其创建日期和时间信息的文件或目录。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static DateTime GetCreationTime(string path)
    {
      return File.InternalGetCreationTimeUtc(path, true).ToLocalTime();
    }

    /// <summary>返回指定的文件或目录的创建日期及时间，其格式为协调通用时 (UTC)。</summary>
    /// <returns>一个 <see cref="T:System.DateTime" /> 结构，它被设置为指定文件或目录的创建日期和时间。该值用 UTC 时间表示。</returns>
    /// <param name="path">要获取其创建日期和时间信息的文件或目录。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static DateTime GetCreationTimeUtc(string path)
    {
      return File.InternalGetCreationTimeUtc(path, false);
    }

    [SecurityCritical]
    private static DateTime InternalGetCreationTimeUtc(string path, bool checkHost)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, fullPathInternal, false, false);
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      int errorCode = File.FillAttributeInfo(fullPathInternal, ref data, false, false);
      if (errorCode != 0)
        __Error.WinIOError(errorCode, fullPathInternal);
      return DateTime.FromFileTimeUtc((long) data.ftCreationTimeHigh << 32 | (long) data.ftCreationTimeLow);
    }

    /// <summary>设置上次访问指定文件的日期和时间。</summary>
    /// <param name="path">要设置其访问日期和时间信息的文件。</param>
    /// <param name="lastAccessTime">一个 <see cref="T:System.DateTime" />，它包含要为 <paramref name="path" /> 的上次访问日期和时间设置的值。该值用本地时间表示。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到指定的路径。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="lastAccessTime" /> 指定超出该操作允许的日期或时间范围的值。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static void SetLastAccessTime(string path, DateTime lastAccessTime)
    {
      File.SetLastAccessTimeUtc(path, lastAccessTime.ToUniversalTime());
    }

    /// <summary>设置上次访问指定的文件的日期和时间，其格式为协调通用时 (UTC)。</summary>
    /// <param name="path">要设置其访问日期和时间信息的文件。</param>
    /// <param name="lastAccessTimeUtc">一个 <see cref="T:System.DateTime" />，它包含要为 <paramref name="path" /> 的上次访问日期和时间设置的值。该值用 UTC 时间表示。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到指定的路径。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="lastAccessTimeUtc" /> 指定超出该操作允许的日期或时间范围的值。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static unsafe void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
    {
      SafeFileHandle handle;
      using (File.OpenFile(path, FileAccess.Write, out handle))
      {
        Win32Native.FILE_TIME fileTime = new Win32Native.FILE_TIME(lastAccessTimeUtc.ToFileTimeUtc());
        if (Win32Native.SetFileTime(handle, (Win32Native.FILE_TIME*) null, &fileTime, (Win32Native.FILE_TIME*) null))
          return;
        __Error.WinIOError(Marshal.GetLastWin32Error(), path);
      }
    }

    /// <summary>返回上次访问指定文件或目录的日期和时间。</summary>
    /// <returns>一个 <see cref="T:System.DateTime" /> 结构，它被设置为上次访问指定文件或目录的日期和时间。该值用本地时间表示。</returns>
    /// <param name="path">要获取其访问日期和时间信息的文件或目录。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static DateTime GetLastAccessTime(string path)
    {
      return File.InternalGetLastAccessTimeUtc(path, true).ToLocalTime();
    }

    /// <summary>返回上次访问指定的文件或目录的日期及时间，其格式为协调通用时 (UTC)。</summary>
    /// <returns>一个 <see cref="T:System.DateTime" /> 结构，它被设置为上次访问指定文件或目录的日期和时间。该值用 UTC 时间表示。</returns>
    /// <param name="path">要获取其访问日期和时间信息的文件或目录。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static DateTime GetLastAccessTimeUtc(string path)
    {
      return File.InternalGetLastAccessTimeUtc(path, false);
    }

    [SecurityCritical]
    private static DateTime InternalGetLastAccessTimeUtc(string path, bool checkHost)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, fullPathInternal, false, false);
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      int errorCode = File.FillAttributeInfo(fullPathInternal, ref data, false, false);
      if (errorCode != 0)
        __Error.WinIOError(errorCode, fullPathInternal);
      return DateTime.FromFileTimeUtc((long) data.ftLastAccessTimeHigh << 32 | (long) data.ftLastAccessTimeLow);
    }

    /// <summary>设置上次写入指定文件的日期和时间。</summary>
    /// <param name="path">要设置其日期和时间信息的文件。</param>
    /// <param name="lastWriteTime">一个 <see cref="T:System.DateTime" />，它包含要为 <paramref name="path" /> 的上次写入日期和时间设置的值。该值用本地时间表示。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到指定的路径。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="lastWriteTime" /> 指定超出该操作允许的日期或时间范围的值。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static void SetLastWriteTime(string path, DateTime lastWriteTime)
    {
      File.SetLastWriteTimeUtc(path, lastWriteTime.ToUniversalTime());
    }

    /// <summary>设置上次写入指定的文件的日期和时间，其格式为协调通用时 (UTC)。</summary>
    /// <param name="path">要设置其日期和时间信息的文件。</param>
    /// <param name="lastWriteTimeUtc">一个 <see cref="T:System.DateTime" />，它包含要为 <paramref name="path" /> 的上次写入日期和时间设置的值。该值用 UTC 时间表示。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到指定的路径。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="lastWriteTimeUtc" /> 指定超出该操作允许的日期或时间范围的值。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static unsafe void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
    {
      SafeFileHandle handle;
      using (File.OpenFile(path, FileAccess.Write, out handle))
      {
        Win32Native.FILE_TIME fileTime = new Win32Native.FILE_TIME(lastWriteTimeUtc.ToFileTimeUtc());
        if (Win32Native.SetFileTime(handle, (Win32Native.FILE_TIME*) null, (Win32Native.FILE_TIME*) null, &fileTime))
          return;
        __Error.WinIOError(Marshal.GetLastWin32Error(), path);
      }
    }

    /// <summary>返回上次写入指定文件或目录的日期和时间。</summary>
    /// <returns>一个 <see cref="T:System.DateTime" /> 结构，它被设置为上次写入指定文件或目录的日期和时间。该值用本地时间表示。</returns>
    /// <param name="path">要获取其写入日期和时间信息的文件或目录。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static DateTime GetLastWriteTime(string path)
    {
      return File.InternalGetLastWriteTimeUtc(path, true).ToLocalTime();
    }

    /// <summary>返回上次写入指定的文件或目录的日期和时间，其格式为协调通用时 (UTC)。</summary>
    /// <returns>一个 <see cref="T:System.DateTime" /> 结构，它被设置为上次写入指定文件或目录的日期和时间。该值用 UTC 时间表示。</returns>
    /// <param name="path">要获取其写入日期和时间信息的文件或目录。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static DateTime GetLastWriteTimeUtc(string path)
    {
      return File.InternalGetLastWriteTimeUtc(path, false);
    }

    [SecurityCritical]
    private static DateTime InternalGetLastWriteTimeUtc(string path, bool checkHost)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, fullPathInternal, false, false);
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      int errorCode = File.FillAttributeInfo(fullPathInternal, ref data, false, false);
      if (errorCode != 0)
        __Error.WinIOError(errorCode, fullPathInternal);
      return DateTime.FromFileTimeUtc((long) data.ftLastWriteTimeHigh << 32 | (long) data.ftLastWriteTimeLow);
    }

    /// <summary>获取在此路径上的文件的 <see cref="T:System.IO.FileAttributes" />。</summary>
    /// <returns>路径上文件的 <see cref="T:System.IO.FileAttributes" />。</returns>
    /// <param name="path">文件的路径。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 为空，仅包含空白，或包含无效字符。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="path" /> 表示一个文件且它是无效的，例如，位于未映射的驱动器上或无法找到文件。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="path" /> 表示一个目录且它是无效的，例如，位于未映射的驱动器上或无法找到目录。</exception>
    /// <exception cref="T:System.IO.IOException">此文件正由另一个进程使用。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static FileAttributes GetAttributes(string path)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, fullPathInternal, false, false);
      Win32Native.WIN32_FILE_ATTRIBUTE_DATA data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      int errorCode = File.FillAttributeInfo(fullPathInternal, ref data, false, true);
      if (errorCode != 0)
        __Error.WinIOError(errorCode, fullPathInternal);
      return (FileAttributes) data.fileAttributes;
    }

    /// <summary>获取指定路径上的文件的指定 <see cref="T:System.IO.FileAttributes" />。</summary>
    /// <param name="path">文件的路径。</param>
    /// <param name="fileAttributes">枚举值的按位组合。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 为空、只包含空白、包含无效字符或文件属性无效。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">无法找到该文件。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 指定了一个只读文件。- 或 - 当前平台不支持此操作。- 或 - <paramref name="path" /> 指定了一个目录。- 或 - 调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static void SetAttributes(string path, FileAttributes fileAttributes)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, fullPathInternal, false, false);
      if (Win32Native.SetFileAttributes(fullPathInternal, (int) fileAttributes))
        return;
      int lastWin32Error = Marshal.GetLastWin32Error();
      int num = 87;
      if (lastWin32Error == num)
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidFileAttrs"));
      string maybeFullPath = fullPathInternal;
      __Error.WinIOError(lastWin32Error, maybeFullPath);
    }

    /// <summary>获取一个 <see cref="T:System.Security.AccessControl.FileSecurity" /> 对象，它封装指定文件的访问控制列表 (ACL) 条目。</summary>
    /// <returns>一个 <see cref="T:System.Security.AccessControl.FileSecurity" /> 对象，它封装由 <paramref name="path" /> 参数描述的文件的访问控制规则。    </returns>
    /// <param name="path">一个文件的路径，该文件包含描述文件的访问控制列表 (ACL) 信息的 <see cref="T:System.Security.AccessControl.FileSecurity" /> 对象。</param>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.Runtime.InteropServices.SEHException">
    /// <paramref name="path" /> 参数为 null。</exception>
    /// <exception cref="T:System.SystemException">找不到文件。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 参数指定了一个只读文件。- 或 - 当前平台不支持此操作。- 或 - <paramref name="path" /> 参数指定了一个目录。- 或 - 调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static FileSecurity GetAccessControl(string path)
    {
      return File.GetAccessControl(path, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
    }

    /// <summary>获取一个 <see cref="T:System.Security.AccessControl.FileSecurity" /> 对象，封装特定文件的指定类型的访问控制列表 (ACL) 项。</summary>
    /// <returns>一个 <see cref="T:System.Security.AccessControl.FileSecurity" /> 对象，它封装由 <paramref name="path" /> 参数描述的文件的访问控制规则。    </returns>
    /// <param name="path">一个文件的路径，该文件包含描述文件的访问控制列表 (ACL) 信息的 <see cref="T:System.Security.AccessControl.FileSecurity" /> 对象。</param>
    /// <param name="includeSections">
    /// <see cref="T:System.Security.AccessControl.AccessControlSections" /> 值之一，它指定要接收的访问控制列表 (ACL) 信息的类型。</param>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.Runtime.InteropServices.SEHException">
    /// <paramref name="path" /> 参数为 null。</exception>
    /// <exception cref="T:System.SystemException">找不到文件。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 参数指定了一个只读文件。- 或 - 当前平台不支持此操作。- 或 - <paramref name="path" /> 参数指定了一个目录。- 或 - 调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static FileSecurity GetAccessControl(string path, AccessControlSections includeSections)
    {
      return new FileSecurity(path, includeSections);
    }

    /// <summary>将 <see cref="T:System.Security.AccessControl.FileSecurity" /> 对象描述的访问控制列表 (ACL) 项应用于指定的文件。</summary>
    /// <param name="path">从其中添加或移除访问控制列表 (ACL) 项的文件。</param>
    /// <param name="fileSecurity">一个 <see cref="T:System.Security.AccessControl.FileSecurity" /> 对象，描述要应用于 <paramref name="path" /> 参数所描述的文件的 ACL 项。</param>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.Runtime.InteropServices.SEHException">
    /// <paramref name="path" /> 参数为 null。</exception>
    /// <exception cref="T:System.SystemException">找不到文件。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 参数指定了一个只读文件。- 或 - 当前平台不支持此操作。- 或 - <paramref name="path" /> 参数指定了一个目录。- 或 - 调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="fileSecurity" /> 参数为 null。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static void SetAccessControl(string path, FileSecurity fileSecurity)
    {
      if (fileSecurity == null)
        throw new ArgumentNullException("fileSecurity");
      string fullPathInternal = Path.GetFullPathInternal(path);
      fileSecurity.Persist(fullPathInternal);
    }

    /// <summary>打开现有文件以进行读取。</summary>
    /// <returns>指定路径上的只读 <see cref="T:System.IO.FileStream" />。</returns>
    /// <param name="path">要打开以进行读取的文件。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 指定了一个目录。- 或 - 调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到 <paramref name="path" /> 中指定的文件。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static FileStream OpenRead(string path)
    {
      return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
    }

    /// <summary>打开一个现有文件或创建一个新文件以进行写入。</summary>
    /// <returns>指定路径上具有 <see cref="T:System.IO.FileStream" /> 访问权限的非共享的 <see cref="F:System.IO.FileAccess.Write" /> 对象。</returns>
    /// <param name="path">要打开以进行写入的文件。</param>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。- 或 - <paramref name="path" /> 指定了只读文件或目录。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static FileStream OpenWrite(string path)
    {
      return new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
    }

    /// <summary>打开一个文本文件，读取文件的所有行，然后关闭该文件。</summary>
    /// <returns>包含文件所有行的字符串。</returns>
    /// <param name="path">要打开以进行读取的文件。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 指定了一个只读文件。- 或 - 当前平台不支持此操作。- 或 - <paramref name="path" /> 指定了一个目录。- 或 - 调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到 <paramref name="path" /> 中指定的文件。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static string ReadAllText(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      return File.InternalReadAllText(path, Encoding.UTF8, true);
    }

    /// <summary>打开一个文件，使用指定的编码读取文件的所有行，然后关闭该文件。</summary>
    /// <returns>包含文件所有行的字符串。</returns>
    /// <param name="path">要打开以进行读取的文件。</param>
    /// <param name="encoding">应用到文件内容的编码。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 指定了一个只读文件。- 或 - 当前平台不支持此操作。- 或 - <paramref name="path" /> 指定了一个目录。- 或 - 调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到 <paramref name="path" /> 中指定的文件。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static string ReadAllText(string path, Encoding encoding)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (encoding == null)
        throw new ArgumentNullException("encoding");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      return File.InternalReadAllText(path, encoding, true);
    }

    [SecurityCritical]
    internal static string UnsafeReadAllText(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      return File.InternalReadAllText(path, Encoding.UTF8, false);
    }

    [SecurityCritical]
    private static string InternalReadAllText(string path, Encoding encoding, bool checkHost)
    {
      using (StreamReader streamReader = new StreamReader(path, encoding, true, StreamReader.DefaultBufferSize, checkHost))
        return streamReader.ReadToEnd();
    }

    /// <summary>创建一个新文件，向其中写入指定的字符串，然后关闭文件。如果目标文件已存在，则覆盖该文件。</summary>
    /// <param name="path">要写入的文件。</param>
    /// <param name="contents">要写入文件的字符串。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null 或 <paramref name="contents" /> 为空。 </exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 指定了一个只读文件。- 或 - 当前平台不支持此操作。- 或 - <paramref name="path" /> 指定了一个目录。- 或 - 调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static void WriteAllText(string path, string contents)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllText(path, contents, StreamWriter.UTF8NoBOM, true);
    }

    /// <summary>创建一个新文件，使用指定编码向其中写入指定的字符串，然后关闭文件。如果目标文件已存在，则覆盖该文件。</summary>
    /// <param name="path">要写入的文件。</param>
    /// <param name="contents">要写入文件的字符串。</param>
    /// <param name="encoding">应用于字符串的编码。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null 或 <paramref name="contents" /> 为空。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 指定了一个只读文件。- 或 - 当前平台不支持此操作。- 或 - <paramref name="path" /> 指定了一个目录。- 或 - 调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static void WriteAllText(string path, string contents, Encoding encoding)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (encoding == null)
        throw new ArgumentNullException("encoding");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllText(path, contents, encoding, true);
    }

    [SecurityCritical]
    internal static void UnsafeWriteAllText(string path, string contents)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllText(path, contents, StreamWriter.UTF8NoBOM, false);
    }

    [SecurityCritical]
    private static void InternalWriteAllText(string path, string contents, Encoding encoding, bool checkHost)
    {
      using (StreamWriter streamWriter = new StreamWriter(path, false, encoding, 1024, checkHost))
        streamWriter.Write(contents);
    }

    /// <summary>打开一个二进制文件，将文件的内容读入一个字节数组，然后关闭该文件。</summary>
    /// <returns>包含文件内容的字节数组。</returns>
    /// <param name="path">要打开以进行读取的文件。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">当前平台不支持此操作。- 或 - <paramref name="path" /> 指定了一个目录。- 或 - 调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到 <paramref name="path" /> 中指定的文件。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static byte[] ReadAllBytes(string path)
    {
      return File.InternalReadAllBytes(path, true);
    }

    [SecurityCritical]
    internal static byte[] UnsafeReadAllBytes(string path)
    {
      return File.InternalReadAllBytes(path, false);
    }

    [SecurityCritical]
    private static byte[] InternalReadAllBytes(string path, bool checkHost)
    {
      byte[] buffer;
      using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.None, Path.GetFileName(path), false, false, checkHost))
      {
        int offset = 0;
        long length = fileStream.Length;
        long num1 = (long) int.MaxValue;
        if (length > num1)
          throw new IOException(Environment.GetResourceString("IO.IO_FileTooLong2GB"));
        int count = (int) length;
        buffer = new byte[count];
        while (count > 0)
        {
          int num2 = fileStream.Read(buffer, offset, count);
          if (num2 == 0)
            __Error.EndOfFile();
          offset += num2;
          count -= num2;
        }
      }
      return buffer;
    }

    /// <summary>创建一个新文件，在其中写入指定的字节数组，然后关闭该文件。如果目标文件已存在，则覆盖该文件。</summary>
    /// <param name="path">要写入的文件。</param>
    /// <param name="bytes">要写入文件的字节。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null 或字节数组为空。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 指定了一个只读文件。- 或 - 当前平台不支持此操作。- 或 - <paramref name="path" /> 指定了一个目录。- 或 - 调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static void WriteAllBytes(string path, byte[] bytes)
    {
      if (path == null)
        throw new ArgumentNullException("path", Environment.GetResourceString("ArgumentNull_Path"));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      if (bytes == null)
        throw new ArgumentNullException("bytes");
      File.InternalWriteAllBytes(path, bytes, true);
    }

    [SecurityCritical]
    internal static void UnsafeWriteAllBytes(string path, byte[] bytes)
    {
      if (path == null)
        throw new ArgumentNullException("path", Environment.GetResourceString("ArgumentNull_Path"));
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      if (bytes == null)
        throw new ArgumentNullException("bytes");
      File.InternalWriteAllBytes(path, bytes, false);
    }

    [SecurityCritical]
    private static void InternalWriteAllBytes(string path, byte[] bytes, bool checkHost)
    {
      using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read, 4096, FileOptions.None, Path.GetFileName(path), false, false, checkHost))
        fileStream.Write(bytes, 0, bytes.Length);
    }

    /// <summary>打开一个文本文件，读取文件的所有行，然后关闭该文件。</summary>
    /// <returns>包含文件所有行的字符串数组。</returns>
    /// <param name="path">要打开以进行读取的文件。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 指定了一个只读文件。- 或 - 当前平台不支持此操作。- 或 - <paramref name="path" /> 指定了一个目录。- 或 - 调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到 <paramref name="path" /> 中指定的文件。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static string[] ReadAllLines(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      return File.InternalReadAllLines(path, Encoding.UTF8);
    }

    /// <summary>打开一个文件，使用指定的编码读取文件的所有行，然后关闭该文件。</summary>
    /// <returns>包含文件所有行的字符串数组。</returns>
    /// <param name="path">要打开以进行读取的文件。</param>
    /// <param name="encoding">应用到文件内容的编码。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 指定了一个只读文件。- 或 - 当前平台不支持此操作。- 或 - <paramref name="path" /> 指定了一个目录。- 或 - 调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到 <paramref name="path" /> 中指定的文件。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static string[] ReadAllLines(string path, Encoding encoding)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (encoding == null)
        throw new ArgumentNullException("encoding");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      return File.InternalReadAllLines(path, encoding);
    }

    private static string[] InternalReadAllLines(string path, Encoding encoding)
    {
      List<string> stringList = new List<string>();
      using (StreamReader streamReader = new StreamReader(path, encoding))
      {
        string str;
        while ((str = streamReader.ReadLine()) != null)
          stringList.Add(str);
      }
      return stringList.ToArray();
    }

    /// <summary>读取文件的行。</summary>
    /// <returns>该文件的所有行或查询结果所示的行。</returns>
    /// <param name="path">要读取的文件。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白，或者包含由定义的一个或多个无效字符 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="path" /> 无效（例如，在未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到 <paramref name="path" /> 指定的文件。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    /// <paramref name="path" /> 超过了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 指定了一个只读文件。- 或 -当前平台不支持此操作。- 或 -<paramref name="path" /> 是一个目录。- 或 -调用方没有所要求的权限。</exception>
    public static IEnumerable<string> ReadLines(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "path");
      return (IEnumerable<string>) ReadLinesIterator.CreateIterator(path, Encoding.UTF8);
    }

    /// <summary>读取具有指定编码的文件的行。</summary>
    /// <returns>该文件的所有行或查询结果所示的行。</returns>
    /// <param name="path">要读取的文件。</param>
    /// <param name="encoding">应用到文件内容的编码。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法中定义的一个或多个无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="path" /> 无效（例如，在未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到 <paramref name="path" /> 指定的文件。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    /// <paramref name="path" /> 超过了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 指定了一个只读文件。- 或 -当前平台不支持此操作。- 或 -<paramref name="path" /> 是一个目录。- 或 -调用方没有所要求的权限。</exception>
    public static IEnumerable<string> ReadLines(string path, Encoding encoding)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (encoding == null)
        throw new ArgumentNullException("encoding");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "path");
      return (IEnumerable<string>) ReadLinesIterator.CreateIterator(path, encoding);
    }

    /// <summary>创建一个新文件，在其中写入指定的字节数组，然后关闭该文件。</summary>
    /// <param name="path">要写入的文件。</param>
    /// <param name="contents">要写入文件的字符串数组。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 或 <paramref name="contents" /> 为 null。 </exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 指定了一个只读文件。- 或 - 当前平台不支持此操作。- 或 - <paramref name="path" /> 指定了一个目录。- 或 - 调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static void WriteAllLines(string path, string[] contents)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (contents == null)
        throw new ArgumentNullException("contents");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllLines((TextWriter) new StreamWriter(path, false, StreamWriter.UTF8NoBOM), (IEnumerable<string>) contents);
    }

    /// <summary>创建一个新文件，使用指定编码在其中写入指定的字符串数组，然后关闭该文件。</summary>
    /// <param name="path">要写入的文件。</param>
    /// <param name="contents">要写入文件的字符串数组。</param>
    /// <param name="encoding">一个 <see cref="T:System.Text.Encoding" /> 对象，它表示应用于字符串数组的字符编码。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 或 <paramref name="contents" /> 为 null。 </exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 指定了一个只读文件。- 或 - 当前平台不支持此操作。- 或 - <paramref name="path" /> 指定了一个目录。- 或 - 调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static void WriteAllLines(string path, string[] contents, Encoding encoding)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (contents == null)
        throw new ArgumentNullException("contents");
      if (encoding == null)
        throw new ArgumentNullException("encoding");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllLines((TextWriter) new StreamWriter(path, false, encoding), (IEnumerable<string>) contents);
    }

    /// <summary>创建一个新文件，向其中写入一个字符串集合，然后关闭该文件。</summary>
    /// <param name="path">要写入的文件。</param>
    /// <param name="contents">要写入到文件中的行。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白，或者包含由定义的一个或多个无效字符 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法。</exception>
    /// <exception cref="T:System.ArgumentNullException">要么<paramref name=" path " />或 <paramref name="contents" /> 是 null。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="path" /> 无效（例如，在未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    /// <paramref name="path" /> 超过了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 指定了一个只读文件。- 或 -当前平台不支持此操作。- 或 -<paramref name="path" /> 是一个目录。- 或 -调用方没有所要求的权限。</exception>
    public static void WriteAllLines(string path, IEnumerable<string> contents)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (contents == null)
        throw new ArgumentNullException("contents");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllLines((TextWriter) new StreamWriter(path, false, StreamWriter.UTF8NoBOM), contents);
    }

    /// <summary>使用指定的编码创建一个新文件，向其中写入一个字符串集合，然后关闭该文件。</summary>
    /// <param name="path">要写入的文件。</param>
    /// <param name="contents">要写入到文件中的行。</param>
    /// <param name="encoding">要使用的字符编码。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白，或者包含由定义的一个或多个无效字符 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法。</exception>
    /// <exception cref="T:System.ArgumentNullException">要么<paramref name=" path" />,，<paramref name=" contents" />, ，或 <paramref name="encoding" /> 是 null。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="path" /> 无效（例如，在未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    /// <paramref name="path" /> 超过了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 指定了一个只读文件。- 或 -当前平台不支持此操作。- 或 -<paramref name="path" /> 是一个目录。- 或 -调用方没有所要求的权限。</exception>
    public static void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (contents == null)
        throw new ArgumentNullException("contents");
      if (encoding == null)
        throw new ArgumentNullException("encoding");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllLines((TextWriter) new StreamWriter(path, false, encoding), contents);
    }

    private static void InternalWriteAllLines(TextWriter writer, IEnumerable<string> contents)
    {
      using (writer)
      {
        foreach (string content in contents)
          writer.WriteLine(content);
      }
    }

    /// <summary>打开一个文件，向其中追加指定的字符串，然后关闭该文件。如果文件不存在，此方法将创建一个文件，将指定的字符串写入文件，然后关闭该文件。</summary>
    /// <param name="path">要将指定的字符串追加到的文件。</param>
    /// <param name="contents">要追加到文件中的字符串。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定路径无效（例如，目录不存在或位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 指定了一个只读文件。- 或 - 当前平台不支持此操作。- 或 - <paramref name="path" /> 指定了一个目录。- 或 - 调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static void AppendAllText(string path, string contents)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalAppendAllText(path, contents, StreamWriter.UTF8NoBOM);
    }

    /// <summary>将指定的字符串追加到文件中，如果文件还不存在则创建该文件。</summary>
    /// <param name="path">要将指定的字符串追加到的文件。</param>
    /// <param name="contents">要追加到文件中的字符串。</param>
    /// <param name="encoding">要使用的字符编码。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含一个或多个由 <see cref="F:System.IO.Path.InvalidPathChars" /> 定义的无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 为 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定路径无效（例如，目录不存在或位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 指定了一个只读文件。- 或 - 当前平台不支持此操作。- 或 - <paramref name="path" /> 指定了一个目录。- 或 - 调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static void AppendAllText(string path, string contents, Encoding encoding)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (encoding == null)
        throw new ArgumentNullException("encoding");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalAppendAllText(path, contents, encoding);
    }

    private static void InternalAppendAllText(string path, string contents, Encoding encoding)
    {
      using (StreamWriter streamWriter = new StreamWriter(path, true, encoding))
        streamWriter.Write(contents);
    }

    /// <summary>向一个文件中追加行，然后关闭该文件。如果指定文件不存在，此方法会创建一个文件，向其中写入指定的行，然后关闭该文件。</summary>
    /// <param name="path">要向其中追加行的文件。如果文件尚不存在，则创建该文件。</param>
    /// <param name="contents">要追加到文件中的行。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法中已定义的一个或多个无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">要么<paramref name=" path " />或 <paramref name="contents" /> 是 null。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="path" /> 无效（例如，目录不存在或位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到 <paramref name="path" /> 指定的文件。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    /// <paramref name="path" /> 超过了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有写入到文件的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 指定了一个只读文件。- 或 -当前平台不支持此操作。- 或 -<paramref name="path" /> 是一个目录。</exception>
    public static void AppendAllLines(string path, IEnumerable<string> contents)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (contents == null)
        throw new ArgumentNullException("contents");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllLines((TextWriter) new StreamWriter(path, true, StreamWriter.UTF8NoBOM), contents);
    }

    /// <summary>使用指定的编码向一个文件中追加行，然后关闭该文件。如果指定文件不存在，此方法会创建一个文件，向其中写入指定的行，然后关闭该文件。</summary>
    /// <param name="path">要向其中追加行的文件。如果文件尚不存在，则创建该文件。</param>
    /// <param name="contents">要追加到文件中的行。</param>
    /// <param name="encoding">要使用的字符编码。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 是一个零长度字符串，仅包含空白或者包含 <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法中已定义的一个或多个无效字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name=" path" />、<paramref name="contents" /> 或 <paramref name="encoding" /> 为 null。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="path" /> 无效（例如，目录不存在或位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到 <paramref name="path" /> 指定的文件。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">
    /// <paramref name="path" /> 超过了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 的格式无效。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 指定了一个只读文件。- 或 -当前平台不支持此操作。- 或 -<paramref name="path" /> 是一个目录。- 或 -调用方没有所要求的权限。</exception>
    public static void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (contents == null)
        throw new ArgumentNullException("contents");
      if (encoding == null)
        throw new ArgumentNullException("encoding");
      if (path.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      File.InternalWriteAllLines((TextWriter) new StreamWriter(path, true, encoding), contents);
    }

    /// <summary>将指定文件移到新位置，提供要指定新文件名的选项。</summary>
    /// <param name="sourceFileName">要移动的文件的名称。可以包括相对或绝对路径。</param>
    /// <param name="destFileName">文件的新路径和名称。</param>
    /// <exception cref="T:System.IO.IOException">目标文件已经存在。- 或 -<paramref name="sourceFileName" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sourceFileName" /> 或 <paramref name="destFileName" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="sourceFileName" /> 或 <paramref name="destFileName" /> 是零长度字符串、只包含空白或者包含在 <see cref="F:System.IO.Path.InvalidPathChars" /> 中定义的无效字符。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="sourceFileName" /> 或 <paramref name="destFileName" /> 中指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="sourceFileName" /> 或 <paramref name="destFileName" /> 的格式无效。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static void Move(string sourceFileName, string destFileName)
    {
      File.InternalMove(sourceFileName, destFileName, true);
    }

    [SecurityCritical]
    internal static void UnsafeMove(string sourceFileName, string destFileName)
    {
      File.InternalMove(sourceFileName, destFileName, false);
    }

    [SecurityCritical]
    private static void InternalMove(string sourceFileName, string destFileName, bool checkHost)
    {
      if (sourceFileName == null)
        throw new ArgumentNullException("sourceFileName", Environment.GetResourceString("ArgumentNull_FileName"));
      if (destFileName == null)
        throw new ArgumentNullException("destFileName", Environment.GetResourceString("ArgumentNull_FileName"));
      if (sourceFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "sourceFileName");
      if (destFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destFileName");
      string fullPathInternal1 = Path.GetFullPathInternal(sourceFileName);
      string fullPathInternal2 = Path.GetFullPathInternal(destFileName);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, fullPathInternal1, false, false);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, fullPathInternal2, false, false);
      if (!File.InternalExists(fullPathInternal1))
        __Error.WinIOError(2, fullPathInternal1);
      if (Win32Native.MoveFile(fullPathInternal1, fullPathInternal2))
        return;
      __Error.WinIOError();
    }

    /// <summary>使用其他文件的内容替换指定文件的内容，这一过程将删除原始文件，并创建被替换文件的备份。</summary>
    /// <param name="sourceFileName">替换由 <paramref name="destinationFileName" /> 指定的文件的文件名。</param>
    /// <param name="destinationFileName">被替换文件的名称。</param>
    /// <param name="destinationBackupFileName">备份文件的名称。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="destinationFileName" /> 参数描述的路径不是合法的格式。- 或 -<paramref name="destinationBackupFileName" /> 参数描述的路径不是合法的格式。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destinationFileName" /> 参数为 null。</exception>
    /// <exception cref="T:System.IO.DriveNotFoundException">指定了无效的驱动器。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到当前 <see cref="T:System.IO.FileInfo" /> 对象所描述的文件。- 或 -找不到 <paramref name="destinationBackupFileName" /> 参数所描述的文件。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。- 或 -<paramref name="sourceFileName" /> 和 <paramref name="destinationFileName" /> 参数指定了相同的文件。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">操作系统是 Windows 98 Second Edition 或更低版本，且文件系统不是 NTFS。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="sourceFileName" /> 或 <paramref name="destinationFileName" /> 参数指定了一个只读文件。- 或 - 当前平台不支持此操作。- 或 - 源参数或目标参数指定的是目录，而不是文件。- 或 - 调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName)
    {
      if (sourceFileName == null)
        throw new ArgumentNullException("sourceFileName");
      if (destinationFileName == null)
        throw new ArgumentNullException("destinationFileName");
      File.InternalReplace(sourceFileName, destinationFileName, destinationBackupFileName, false);
    }

    /// <summary>用其他文件的内容替换指定文件的内容，这一过程将删除原始文件，并创建被替换文件的备份，还可以忽略合并错误。</summary>
    /// <param name="sourceFileName">替换由 <paramref name="destinationFileName" /> 指定的文件的文件名。</param>
    /// <param name="destinationFileName">被替换文件的名称。</param>
    /// <param name="destinationBackupFileName">备份文件的名称。</param>
    /// <param name="ignoreMetadataErrors">如果忽略从被替换文件到替换文件的合并错误（如特性和访问控制列表 (ACL)），则为 true，否则为 false。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="destinationFileName" /> 参数描述的路径不是合法的格式。- 或 -<paramref name="destinationBackupFileName" /> 参数描述的路径不是合法的格式。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destinationFileName" /> 参数为 null。</exception>
    /// <exception cref="T:System.IO.DriveNotFoundException">指定了无效的驱动器。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到当前 <see cref="T:System.IO.FileInfo" /> 对象所描述的文件。- 或 -找不到 <paramref name="destinationBackupFileName" /> 参数所描述的文件。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。- 或 -<paramref name="sourceFileName" /> 和 <paramref name="destinationFileName" /> 参数指定了相同的文件。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">操作系统是 Windows 98 Second Edition 或更低版本，且文件系统不是 NTFS。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="sourceFileName" /> 或 <paramref name="destinationFileName" /> 参数指定了一个只读文件。- 或 - 当前平台不支持此操作。- 或 - 源参数或目标参数指定的是目录，而不是文件。- 或 - 调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
    {
      if (sourceFileName == null)
        throw new ArgumentNullException("sourceFileName");
      if (destinationFileName == null)
        throw new ArgumentNullException("destinationFileName");
      File.InternalReplace(sourceFileName, destinationFileName, destinationBackupFileName, ignoreMetadataErrors);
    }

    [SecuritySafeCritical]
    private static void InternalReplace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
    {
      string fullPathInternal1 = Path.GetFullPathInternal(sourceFileName);
      string fullPathInternal2 = Path.GetFullPathInternal(destinationFileName);
      string str = (string) null;
      if (destinationBackupFileName != null)
        str = Path.GetFullPathInternal(destinationBackupFileName);
      FileIOPermission fileIoPermission = new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, new string[2]{ fullPathInternal1, fullPathInternal2 });
      if (destinationBackupFileName != null)
        fileIoPermission.AddPathList(FileIOPermissionAccess.Write, str);
      fileIoPermission.Demand();
      int dwReplaceFlags = 1;
      if (ignoreMetadataErrors)
        dwReplaceFlags |= 2;
      if (Win32Native.ReplaceFile(fullPathInternal2, fullPathInternal1, str, dwReplaceFlags, IntPtr.Zero, IntPtr.Zero))
        return;
      __Error.WinIOError();
    }

    [SecurityCritical]
    internal static int FillAttributeInfo(string path, ref Win32Native.WIN32_FILE_ATTRIBUTE_DATA data, bool tryagain, bool returnErrorOnNotFound)
    {
      int num = 0;
      if (tryagain)
      {
        Win32Native.WIN32_FIND_DATA wiN32FindData = new Win32Native.WIN32_FIND_DATA();
        string fileName = path.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        int newMode = Win32Native.SetErrorMode(1);
        try
        {
          bool flag = false;
          SafeFindHandle firstFile = Win32Native.FindFirstFile(fileName, wiN32FindData);
          try
          {
            if (firstFile.IsInvalid)
            {
              flag = true;
              num = Marshal.GetLastWin32Error();
              switch (num)
              {
                case 2:
                case 3:
                case 21:
                  if (!returnErrorOnNotFound)
                  {
                    num = 0;
                    data.fileAttributes = -1;
                    break;
                  }
                  break;
              }
              return num;
            }
          }
          finally
          {
            try
            {
              firstFile.Close();
            }
            catch
            {
              if (!flag)
                __Error.WinIOError();
            }
          }
        }
        finally
        {
          Win32Native.SetErrorMode(newMode);
        }
        data.PopulateFrom(wiN32FindData);
      }
      else
      {
        bool flag = false;
        int newMode = Win32Native.SetErrorMode(1);
        try
        {
          flag = Win32Native.GetFileAttributesEx(path, 0, ref data);
        }
        finally
        {
          Win32Native.SetErrorMode(newMode);
        }
        if (!flag)
        {
          num = Marshal.GetLastWin32Error();
          switch (num)
          {
            case 2:
            case 3:
            case 21:
              if (!returnErrorOnNotFound)
              {
                num = 0;
                data.fileAttributes = -1;
                break;
              }
              break;
            default:
              return File.FillAttributeInfo(path, ref data, true, returnErrorOnNotFound);
          }
        }
      }
      return num;
    }

    [SecurityCritical]
    private static FileStream OpenFile(string path, FileAccess access, out SafeFileHandle handle)
    {
      FileStream fileStream = new FileStream(path, FileMode.Open, access, FileShare.ReadWrite, 1);
      handle = fileStream.SafeFileHandle;
      if (handle.IsInvalid)
      {
        int errorCode = Marshal.GetLastWin32Error();
        string fullPathInternal = Path.GetFullPathInternal(path);
        if (errorCode == 3 && fullPathInternal.Equals(Directory.GetDirectoryRoot(fullPathInternal)))
          errorCode = 5;
        __Error.WinIOError(errorCode, path);
      }
      return fileStream;
    }
  }
}
