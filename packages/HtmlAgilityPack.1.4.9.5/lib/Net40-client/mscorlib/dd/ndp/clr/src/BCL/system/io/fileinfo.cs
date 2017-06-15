// Decompiled with JetBrains decompiler
// Type: System.IO.FileInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;

namespace System.IO
{
  /// <summary>提供用于创建、复制、删除、移动和打开文件的属性和实例方法，并且帮助创建 <see cref="T:System.IO.FileStream" /> 对象。此类不能被继承。若要浏览此类型的.NET Framework 源代码，请参阅 Reference Source。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public sealed class FileInfo : FileSystemInfo
  {
    private string _name;

    /// <summary>获取文件名。</summary>
    /// <returns>文件的名称。</returns>
    /// <filterpriority>1</filterpriority>
    public override string Name
    {
      get
      {
        return this._name;
      }
    }

    /// <summary>获取当前文件的大小（以字节为单位）。</summary>
    /// <returns>当前文件的大小（以字节为单位）。</returns>
    /// <exception cref="T:System.IO.IOException">
    /// <see cref="M:System.IO.FileSystemInfo.Refresh" /> 无法更新文件或目录的状态。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">文件不存在。- 或 - 为一个目录调用 Length 属性。</exception>
    /// <filterpriority>1</filterpriority>
    public long Length
    {
      [SecuritySafeCritical] get
      {
        if (this._dataInitialised == -1)
          this.Refresh();
        if (this._dataInitialised != 0)
          __Error.WinIOError(this._dataInitialised, this.DisplayPath);
        if ((this._data.fileAttributes & 16) != 0)
          __Error.WinIOError(2, this.DisplayPath);
        return (long) this._data.fileSizeHigh << 32 | (long) this._data.fileSizeLow & (long) uint.MaxValue;
      }
    }

    /// <summary>获取表示目录的完整路径的字符串。</summary>
    /// <returns>表示目录的完整路径的字符串。</returns>
    /// <exception cref="T:System.ArgumentNullException">为目录名传入 null。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">完全限定路径为 260 或更多字符。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public string DirectoryName
    {
      [SecuritySafeCritical] get
      {
        string directoryName = Path.GetDirectoryName(this.FullPath);
        if (directoryName != null)
          new FileIOPermission(FileIOPermissionAccess.PathDiscovery, new string[1]{ directoryName }, 0 != 0, 0 != 0).Demand();
        return directoryName;
      }
    }

    /// <summary>获取父目录的实例。</summary>
    /// <returns>表示此文件父目录的 <see cref="T:System.IO.DirectoryInfo" /> 对象。</returns>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效，比如在未映射的驱动器上。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public DirectoryInfo Directory
    {
      get
      {
        string directoryName = this.DirectoryName;
        if (directoryName == null)
          return (DirectoryInfo) null;
        return new DirectoryInfo(directoryName);
      }
    }

    /// <summary>获取或设置确定当前文件是否为只读的值。</summary>
    /// <returns>如果当前文件为只读，则为 true；否则为 false。</returns>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到当前 <see cref="T:System.IO.FileInfo" /> 对象所描述的文件。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">当前平台不支持此操作。- 或 - 调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.ArgumentException">用户没有写入权限，但尝试设置属性为 false。</exception>
    /// <filterpriority>1</filterpriority>
    public bool IsReadOnly
    {
      get
      {
        return (uint) (this.Attributes & FileAttributes.ReadOnly) > 0U;
      }
      set
      {
        if (value)
          this.Attributes = this.Attributes | FileAttributes.ReadOnly;
        else
          this.Attributes = this.Attributes & ~FileAttributes.ReadOnly;
      }
    }

    /// <summary>获取指示文件是否存在的值。</summary>
    /// <returns>如果该文件存在，则为 true；如果文件不存在或文件即是目录，则为 false。</returns>
    /// <filterpriority>1</filterpriority>
    public override bool Exists
    {
      [SecuritySafeCritical] get
      {
        try
        {
          if (this._dataInitialised == -1)
            this.Refresh();
          if (this._dataInitialised != 0)
            return false;
          return (this._data.fileAttributes & 16) == 0;
        }
        catch
        {
          return false;
        }
      }
    }

    /// <summary>初始化作为文件路径的包装的 <see cref="T:System.IO.FileInfo" /> 类的新实例。</summary>
    /// <param name="fileName">新文件的完全限定名或相对文件名。路径不要以目录分隔符结尾。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="fileName" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.ArgumentException">文件名为空，只包含空白，或包含无效字符。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">对 <paramref name="fileName" /> 的访问被拒绝。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="fileName" /> 字符串中间有一个冒号 (:)。</exception>
    [SecuritySafeCritical]
    public FileInfo(string fileName)
    {
      if (fileName == null)
        throw new ArgumentNullException("fileName");
      this.Init(fileName, true);
    }

    [SecurityCritical]
    private FileInfo(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      new FileIOPermission(FileIOPermissionAccess.Read, new string[1]{ this.FullPath }, 0 != 0, 0 != 0).Demand();
      this._name = Path.GetFileName(this.OriginalPath);
      this.DisplayPath = this.GetDisplayPath(this.OriginalPath);
    }

    internal FileInfo(string fullPath, bool ignoreThis)
    {
      this._name = Path.GetFileName(fullPath);
      this.OriginalPath = this._name;
      this.FullPath = fullPath;
      this.DisplayPath = this._name;
    }

    [SecurityCritical]
    private void Init(string fileName, bool checkHost)
    {
      this.OriginalPath = fileName;
      string fullPathInternal = Path.GetFullPathInternal(fileName);
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, fullPathInternal, false, false);
      this._name = Path.GetFileName(fileName);
      this.FullPath = fullPathInternal;
      this.DisplayPath = this.GetDisplayPath(fileName);
    }

    private string GetDisplayPath(string originalPath)
    {
      return originalPath;
    }

    /// <summary>获取 <see cref="T:System.Security.AccessControl.FileSecurity" /> 对象，该对象封装当前 <see cref="T:System.IO.FileInfo" /> 对象所描述的文件的访问控制列表 (ACL) 项。</summary>
    /// <returns>一个 <see cref="T:System.Security.AccessControl.FileSecurity" /> 对象，该对象封装当前文件的访问控制规则。</returns>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Microsoft Windows 2000 或更高版本。</exception>
    /// <exception cref="T:System.Security.AccessControl.PrivilegeNotHeldException">当前系统帐户没有管理特权。</exception>
    /// <exception cref="T:System.SystemException">找不到文件。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">当前平台不支持此操作。- 或 - 调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public FileSecurity GetAccessControl()
    {
      return File.GetAccessControl(this.FullPath, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
    }

    /// <summary>获取一个 <see cref="T:System.Security.AccessControl.FileSecurity" /> 对象，该对象封装当前 <see cref="T:System.IO.FileInfo" /> 对象所描述的文件的指定类型的访问控制列表 (ACL) 项。</summary>
    /// <returns>一个 <see cref="T:System.Security.AccessControl.FileSecurity" /> 对象，该对象封装当前文件的访问控制规则。    </returns>
    /// <param name="includeSections">
    /// <see cref="T:System.Security.AccessControl.AccessControlSections" /> 值之一，该值指定要检索的访问控制项组。</param>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Microsoft Windows 2000 或更高版本。</exception>
    /// <exception cref="T:System.Security.AccessControl.PrivilegeNotHeldException">当前系统帐户没有管理特权。</exception>
    /// <exception cref="T:System.SystemException">找不到文件。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">当前平台不支持此操作。- 或 - 调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public FileSecurity GetAccessControl(AccessControlSections includeSections)
    {
      return File.GetAccessControl(this.FullPath, includeSections);
    }

    /// <summary>将 <see cref="T:System.Security.AccessControl.FileSecurity" /> 对象所描述的访问控制列表 (ACL) 项应用于当前 <see cref="T:System.IO.FileInfo" /> 对象所描述的文件。</summary>
    /// <param name="fileSecurity">一个 <see cref="T:System.Security.AccessControl.FileSecurity" /> 对象，该对象描述要应用于当前文件的访问控制列表 (ACL) 项。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="fileSecurity" /> 参数为 null。</exception>
    /// <exception cref="T:System.SystemException">未能找到或修改文件。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">当前进程不具有打开该文件的权限。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Microsoft Windows 2000 或更高版本。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public void SetAccessControl(FileSecurity fileSecurity)
    {
      File.SetAccessControl(this.FullPath, fileSecurity);
    }

    /// <summary>创建使用从现有文本文件中读取的 UTF8 编码的 <see cref="T:System.IO.StreamReader" />。</summary>
    /// <returns>使用 UTF8 编码的新的 StreamReader。</returns>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到该文件。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 为只读，或者是一个目录。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效，比如在未映射的驱动器上。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public StreamReader OpenText()
    {
      return new StreamReader(this.FullPath, Encoding.UTF8, true, StreamReader.DefaultBufferSize, false);
    }

    /// <summary>创建写入新文本文件的 <see cref="T:System.IO.StreamWriter" />。</summary>
    /// <returns>一个新的 StreamWriter。</returns>
    /// <exception cref="T:System.UnauthorizedAccessException">文件名为目录。</exception>
    /// <exception cref="T:System.IO.IOException">磁盘为只读。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public StreamWriter CreateText()
    {
      return new StreamWriter(this.FullPath, false);
    }

    /// <summary>创建一个 <see cref="T:System.IO.StreamWriter" />，它向 <see cref="T:System.IO.FileInfo" /> 的此实例表示的文件追加文本。</summary>
    /// <returns>一个新的 StreamWriter。</returns>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public StreamWriter AppendText()
    {
      return new StreamWriter(this.FullPath, true);
    }

    /// <summary>将现有文件复制到新文件，不允许覆盖现有文件。</summary>
    /// <returns>带有完全限定路径的新文件。</returns>
    /// <param name="destFileName">要复制到的新文件的名称。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="destFileName" /> 为空，仅包含空白，或包含无效字符。</exception>
    /// <exception cref="T:System.IO.IOException">发生错误或目标文件已经存在。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destFileName" /> 为 null。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">传入了一个目录路径，或者正在将文件移动到另一个驱动器。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="destFileName" /> 中指定的目录不存在。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="destFileName" /> 在字符串内包含一个冒号 (:)，但未指定卷。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public FileInfo CopyTo(string destFileName)
    {
      if (destFileName == null)
        throw new ArgumentNullException("destFileName", Environment.GetResourceString("ArgumentNull_FileName"));
      if (destFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destFileName");
      destFileName = File.InternalCopy(this.FullPath, destFileName, false, true);
      return new FileInfo(destFileName, false);
    }

    /// <summary>将现有文件复制到新文件，允许覆盖现有文件。</summary>
    /// <returns>为新文件；如果 <paramref name="overwrite" /> 是 true，则为现有文件的覆盖。如果文件存在且 <paramref name="overwrite" /> 为 false，则引发 <see cref="T:System.IO.IOException" />。</returns>
    /// <param name="destFileName">要复制到的新文件的名称。</param>
    /// <param name="overwrite">如果允许覆盖现有文件，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="destFileName" /> 为空，仅包含空白，或包含无效字符。</exception>
    /// <exception cref="T:System.IO.IOException">发生错误，或者目标文件已经存在，并且 <paramref name="overwrite" /> 为 false。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destFileName" /> 为 null。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">
    /// <paramref name="destFileName" /> 中指定的目录不存在。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">传入了一个目录路径，或者正在将文件移动到另一个驱动器。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="destFileName" /> 字符串中间有一个冒号 (:)。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public FileInfo CopyTo(string destFileName, bool overwrite)
    {
      if (destFileName == null)
        throw new ArgumentNullException("destFileName", Environment.GetResourceString("ArgumentNull_FileName"));
      if (destFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destFileName");
      destFileName = File.InternalCopy(this.FullPath, destFileName, overwrite, true);
      return new FileInfo(destFileName, false);
    }

    /// <summary>创建文件。</summary>
    /// <returns>新文件。</returns>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public FileStream Create()
    {
      return File.Create(this.FullPath);
    }

    /// <summary>永久删除文件。</summary>
    /// <exception cref="T:System.IO.IOException">目标文件已打开或内存映射到运行 Microsoft Windows NT 的计算机上。- 或 -对于文件有打开句柄，并且操作系统是 Windows XP 或更早版本。此打开句柄可能是由于枚举目录和文件导致的。有关详细信息，请参阅如何：枚举目录和文件。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">路径是目录。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override void Delete()
    {
      new FileIOPermission(FileIOPermissionAccess.Write, new string[1]{ this.FullPath }, 0 != 0, 0 != 0).Demand();
      if (Win32Native.DeleteFile(this.FullPath))
        return;
      int lastWin32Error = Marshal.GetLastWin32Error();
      if (lastWin32Error == 2)
        return;
      __Error.WinIOError(lastWin32Error, this.DisplayPath);
    }

    /// <summary>使用 <see cref="M:System.IO.FileInfo.Encrypt" /> 方法解密由当前帐户加密的文件。</summary>
    /// <exception cref="T:System.IO.DriveNotFoundException">指定了无效的驱动器。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到当前 <see cref="T:System.IO.FileInfo" /> 对象所描述的文件。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.NotSupportedException">文件系统不是 NTFS。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Microsoft Windows NT 或更高版本。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">当前 <see cref="T:System.IO.FileInfo" /> 对象描述的文件是只读文件。- 或 - 当前平台不支持此操作。- 或 - 调用方没有所要求的权限。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [ComVisible(false)]
    public void Decrypt()
    {
      File.Decrypt(this.FullPath);
    }

    /// <summary>将某个文件加密，使得只有加密该文件的帐户才能将其解密。</summary>
    /// <exception cref="T:System.IO.DriveNotFoundException">指定了无效的驱动器。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到当前 <see cref="T:System.IO.FileInfo" /> 对象所描述的文件。</exception>
    /// <exception cref="T:System.IO.IOException">打开文件时发生 I/O 错误。</exception>
    /// <exception cref="T:System.NotSupportedException">文件系统不是 NTFS。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Microsoft Windows NT 或更高版本。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">当前 <see cref="T:System.IO.FileInfo" /> 对象描述的文件是只读文件。- 或 - 当前平台不支持此操作。- 或 - 调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [ComVisible(false)]
    public void Encrypt()
    {
      File.Encrypt(this.FullPath);
    }

    /// <summary>在指定的模式中打开文件。</summary>
    /// <returns>在指定模式中打开、具有读/写访问权限且不共享的文件。</returns>
    /// <param name="mode">一个 <see cref="T:System.IO.FileMode" /> 常数，它指定打开文件所采用的模式（例如 Open 或 Append）。</param>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到该文件。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">此文件是只读文件，或者是一个目录。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效，比如在未映射的驱动器上。</exception>
    /// <exception cref="T:System.IO.IOException">文件已经处于打开状态。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public FileStream Open(FileMode mode)
    {
      return this.Open(mode, FileAccess.ReadWrite, FileShare.None);
    }

    /// <summary>用读、写或读/写访问权限在指定模式下打开文件。</summary>
    /// <returns>用指定模式和访问权限打开的且不共享的 <see cref="T:System.IO.FileStream" /> 对象。</returns>
    /// <param name="mode">一个 <see cref="T:System.IO.FileMode" /> 常数，它指定打开文件所采用的模式（例如 Open 或 Append）。</param>
    /// <param name="access">一个 <see cref="T:System.IO.FileAccess" /> 常数，它指定是使用 Read、Write 还是 ReadWrite 文件访问来打开文件。</param>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到该文件。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 为只读，或者是一个目录。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效，比如在未映射的驱动器上。</exception>
    /// <exception cref="T:System.IO.IOException">文件已经处于打开状态。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public FileStream Open(FileMode mode, FileAccess access)
    {
      return this.Open(mode, access, FileShare.None);
    }

    /// <summary>用读、写或读/写访问权限和指定的共享选项在指定的模式中打开文件。</summary>
    /// <returns>用指定的模式、访问权限和共享选项打开的 <see cref="T:System.IO.FileStream" /> 对象。</returns>
    /// <param name="mode">一个 <see cref="T:System.IO.FileMode" /> 常数，它指定打开文件所采用的模式（例如 Open 或 Append）。</param>
    /// <param name="access">一个 <see cref="T:System.IO.FileAccess" /> 常数，它指定是使用 Read、Write 还是 ReadWrite 文件访问来打开文件。</param>
    /// <param name="share">一个 <see cref="T:System.IO.FileShare" /> 常数，它指定其他 FileStream 对象对此文件拥有的访问类型。</param>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到该文件。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 为只读，或者是一个目录。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效，比如在未映射的驱动器上。</exception>
    /// <exception cref="T:System.IO.IOException">文件已经处于打开状态。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public FileStream Open(FileMode mode, FileAccess access, FileShare share)
    {
      return new FileStream(this.FullPath, mode, access, share);
    }

    /// <summary>创建一个只读的 <see cref="T:System.IO.FileStream" />。</summary>
    /// <returns>一个新的只读的 <see cref="T:System.IO.FileStream" /> 对象。</returns>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="path" /> 为只读，或者是一个目录。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效，比如在未映射的驱动器上。</exception>
    /// <exception cref="T:System.IO.IOException">文件已经处于打开状态。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public FileStream OpenRead()
    {
      return new FileStream(this.FullPath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, false);
    }

    /// <summary>创建一个只写的 <see cref="T:System.IO.FileStream" />。</summary>
    /// <returns>新的或现有文件的只写非共享的 <see cref="T:System.IO.FileStream" /> 对象。</returns>
    /// <exception cref="T:System.UnauthorizedAccessException">路径指定创建 <see cref="T:System.IO.FileInfo" /> 对象的实例何时是只读或者是目录。 </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">路径指定创建 <see cref="T:System.IO.FileInfo" /> 对象的实例何时无效，例如在未映射的驱动器上时。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public FileStream OpenWrite()
    {
      return new FileStream(this.FullPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
    }

    /// <summary>将指定文件移到新位置，提供要指定新文件名的选项。</summary>
    /// <param name="destFileName">要将文件移动到的路径，可以指定另一个文件名。</param>
    /// <exception cref="T:System.IO.IOException">发生 I/O 错误，如目标文件已经存在或目标设备未准备好。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destFileName" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="destFileName" /> 为空，仅包含空白，或包含无效字符。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    /// <paramref name="destFileName" /> 为只读，或者是一个目录。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到该文件。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效，比如在未映射的驱动器上。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="destFileName" /> 字符串中间有一个冒号 (:)。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public void MoveTo(string destFileName)
    {
      if (destFileName == null)
        throw new ArgumentNullException("destFileName");
      if (destFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destFileName");
      string fullPathInternal = Path.GetFullPathInternal(destFileName);
      new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, new string[1]{ this.FullPath }, 0 != 0, 0 != 0).Demand();
      FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, fullPathInternal, false, false);
      if (!Win32Native.MoveFile(this.FullPath, fullPathInternal))
        __Error.WinIOError();
      this.FullPath = fullPathInternal;
      this.OriginalPath = destFileName;
      this._name = Path.GetFileName(fullPathInternal);
      this.DisplayPath = this.GetDisplayPath(destFileName);
      this._dataInitialised = -1;
    }

    /// <summary>使用当前 <see cref="T:System.IO.FileInfo" /> 对象所描述的文件替换指定文件的内容，这一过程将删除原始文件，并创建被替换文件的备份。</summary>
    /// <returns>一个 <see cref="T:System.IO.FileInfo" /> 对象，该对象封装有关 <paramref name="destFileName" /> 参数所描述的文件的信息。</returns>
    /// <param name="destinationFileName">要替换为当前文件的文件的名称。</param>
    /// <param name="destinationBackupFileName">文件的名称，该文件用于创建 <paramref name="destFileName" /> 参数所描述的文件的备份。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="destFileName" /> 参数描述的路径不是合法的格式。- 或 -<paramref name="destBackupFileName" /> 参数描述的路径不是合法的格式。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destFileName" /> 参数为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到当前 <see cref="T:System.IO.FileInfo" /> 对象所描述的文件。- 或 -找不到 <paramref name="destinationFileName" /> 参数所描述的文件。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Microsoft Windows NT 或更高版本。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [ComVisible(false)]
    public FileInfo Replace(string destinationFileName, string destinationBackupFileName)
    {
      return this.Replace(destinationFileName, destinationBackupFileName, false);
    }

    /// <summary>使用当前 <see cref="T:System.IO.FileInfo" /> 对象所描述的文件替换指定文件的内容，这一过程将删除原始文件，并创建被替换文件的备份。还指定是否忽略合并错误。</summary>
    /// <returns>一个 <see cref="T:System.IO.FileInfo" /> 对象，该对象封装有关 <paramref name="destFileName" /> 参数所描述的文件的信息。</returns>
    /// <param name="destinationFileName">要替换为当前文件的文件的名称。</param>
    /// <param name="destinationBackupFileName">文件的名称，该文件用于创建 <paramref name="destFileName" /> 参数所描述的文件的备份。</param>
    /// <param name="ignoreMetadataErrors">若要忽略从被替换文件到替换文件的合并错误（例如特性和 ACL），请设置为 true；否则设置为 false。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="destFileName" /> 参数描述的路径不是合法的格式。- 或 -<paramref name="destBackupFileName" /> 参数描述的路径不是合法的格式。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destFileName" /> 参数为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到当前 <see cref="T:System.IO.FileInfo" /> 对象所描述的文件。- 或 -找不到 <paramref name="destinationFileName" /> 参数所描述的文件。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Microsoft Windows NT 或更高版本。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [ComVisible(false)]
    public FileInfo Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
    {
      File.Replace(this.FullPath, destinationFileName, destinationBackupFileName, ignoreMetadataErrors);
      return new FileInfo(destinationFileName);
    }

    /// <summary>以字符串形式返回路径。</summary>
    /// <returns>一个表示该路径的字符串。</returns>
    /// <filterpriority>1</filterpriority>
    public override string ToString()
    {
      return this.DisplayPath;
    }
  }
}
