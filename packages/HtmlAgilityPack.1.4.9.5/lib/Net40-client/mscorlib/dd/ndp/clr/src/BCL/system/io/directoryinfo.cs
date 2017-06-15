// Decompiled with JetBrains decompiler
// Type: System.IO.DirectoryInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;

namespace System.IO
{
  /// <summary>公开用于通过目录和子目录进行创建、移动和枚举的实例方法。此类不能被继承。若要浏览此类型的.NET Framework 源代码，请参阅
  ///     引用源.
  /// </summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public sealed class DirectoryInfo : FileSystemInfo
  {
    private string[] demandDir;

    /// <summary>获取此名称
    /// <see cref="T:System.IO.DirectoryInfo" />实例。
    ///                             </summary>
    /// <returns>目录名称。</returns>
    /// <filterpriority>1</filterpriority>
    public override string Name
    {
      get
      {
        return DirectoryInfo.GetDirName(this.FullPath);
      }
    }

    /// <summary>获取指定的子目录的父目录。</summary>
    /// <returns>父目录中，或
    /// null如果该路径为 null，或文件路径表示根 （例如"\"，"c:"或 *"\\server\share"）。
    ///                             </returns>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public DirectoryInfo Parent
    {
      [SecuritySafeCritical] get
      {
        string path = this.FullPath;
        if (path.Length > 3 && path.EndsWith(Path.DirectorySeparatorChar))
          path = this.FullPath.Substring(0, this.FullPath.Length - 1);
        string directoryName = Path.GetDirectoryName(path);
        if (directoryName == null)
          return (DirectoryInfo) null;
        DirectoryInfo directoryInfo = new DirectoryInfo(directoryName, false);
        new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery, directoryInfo.demandDir, false, false).Demand();
        return directoryInfo;
      }
    }

    /// <summary>获取指示目录是否存在的值。</summary>
    /// <returns>true如果该目录存在） ；否则为
    ///     false.
    /// </returns>
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
          return this._data.fileAttributes != -1 && (uint) (this._data.fileAttributes & 16) > 0U;
        }
        catch
        {
          return false;
        }
      }
    }

    /// <summary>获取目录的根部分。</summary>
    /// <returns>一个表示目录的根目录的对象。</returns>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public DirectoryInfo Root
    {
      [SecuritySafeCritical] get
      {
        string str = this.FullPath.Substring(0, Path.GetRootLength(this.FullPath));
        new FileIOPermission(FileIOPermissionAccess.PathDiscovery, new string[1]
        {
          Directory.GetDemandDir(str, true)
        }, 0 != 0, 0 != 0).Demand();
        return new DirectoryInfo(str);
      }
    }

    /// <summary>初始化
    /// <see cref="T:System.IO.DirectoryInfo" />指定路径上的类。
    ///                             </summary>
    /// <param name="path">一个字符串，指定在其上创建路径
    ///     DirectoryInfo.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" />是
    ///                                 null.
    ///                             </exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 包含无效字符，如 "、&lt;、&gt; 或 |.
    ///                             </exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。指定的路径或文件名太长，或者两者都太长。</exception>
    [SecuritySafeCritical]
    public DirectoryInfo(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      this.Init(path, true);
    }

    internal DirectoryInfo(string fullPath, bool junk)
    {
      this.OriginalPath = Path.GetFileName(fullPath);
      this.FullPath = fullPath;
      this.DisplayPath = DirectoryInfo.GetDisplayName(this.OriginalPath, this.FullPath);
      this.demandDir = new string[1]
      {
        Directory.GetDemandDir(fullPath, true)
      };
    }

    [SecurityCritical]
    private DirectoryInfo(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.demandDir = new string[1]
      {
        Directory.GetDemandDir(this.FullPath, true)
      };
      new FileIOPermission(FileIOPermissionAccess.Read, this.demandDir, false, false).Demand();
      this.DisplayPath = DirectoryInfo.GetDisplayName(this.OriginalPath, this.FullPath);
    }

    [SecurityCritical]
    private void Init(string path, bool checkHost)
    {
      if (path.Length == 2 && (int) path[1] == 58)
        this.OriginalPath = ".";
      else
        this.OriginalPath = path;
      string fullPathInternal = Path.GetFullPathInternal(path);
      this.demandDir = new string[1]
      {
        Directory.GetDemandDir(fullPathInternal, true)
      };
      new FileIOPermission(FileIOPermissionAccess.Read, this.demandDir, false, false).Demand();
      this.FullPath = fullPathInternal;
      this.DisplayPath = DirectoryInfo.GetDisplayName(this.OriginalPath, this.FullPath);
    }

    /// <summary>在指定路径上创建一个或多个子目录。指定的路径可以是相对于此实例
    /// <see cref="T:System.IO.DirectoryInfo" />类。
    ///                         </summary>
    /// <returns>中指定的最后一个目录
    ///     <paramref name="path" />.
    /// </returns>
    /// <param name="path">指定的路径。它不能是另一个磁盘卷或通用命名约定 (UNC) 名称。</param>
    /// <exception cref="T:System.ArgumentException">
    ///         <paramref name="path" />未指定有效的文件路径或包含无效
    /// DirectoryInfo字符。
    ///                                     </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" />是
    ///                                 null.
    ///                             </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效，比如在未映射的驱动器上。</exception>
    /// <exception cref="T:System.IO.IOException">不能创建子目录。- 或 -文件或目录已具有指定的名称
    ///     <paramref name="path" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。指定的路径或文件名太长，或者两者都太长。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方不具有创建目录的代码访问权限。- 或 -调用方没有代码访问权限以读取所描述的返回目录
    /// <see cref="T:System.IO.DirectoryInfo" /> 对象。
    ///                                 发生这种情况时
    /// <paramref name="path" />参数描述的现有目录。
    ///                                 </exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 包含一个冒号字符 (:)，该冒号字符不是驱动器标签（“C:\”）的一部分。
    ///                             </exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public DirectoryInfo CreateSubdirectory(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      return this.CreateSubdirectory(path, (DirectorySecurity) null);
    }

    /// <summary>使用指定的安全性在指定的路径上创建一个或多个子目录。指定的路径可以是相对于此实例
    /// <see cref="T:System.IO.DirectoryInfo" />类。
    ///                         </summary>
    /// <returns>中指定的最后一个目录
    ///     <paramref name="path" />.
    /// </returns>
    /// <param name="path">指定的路径。它不能是另一个磁盘卷或通用命名约定 (UNC) 名称。</param>
    /// <param name="directorySecurity">要应用的安全性。</param>
    /// <exception cref="T:System.ArgumentException">
    ///         <paramref name="path" />未指定有效的文件路径或包含无效
    /// DirectoryInfo字符。
    ///                                     </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" />是
    ///                                 null.
    ///                             </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效，比如在未映射的驱动器上。</exception>
    /// <exception cref="T:System.IO.IOException">不能创建子目录。- 或 -文件或目录已具有指定的名称
    ///     <paramref name="path" />.
    /// </exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。指定的路径或文件名太长，或者两者都太长。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方不具有创建目录的代码访问权限。- 或 -调用方没有代码访问权限以读取所描述的返回目录
    /// <see cref="T:System.IO.DirectoryInfo" /> 对象。
    ///                                 发生这种情况时
    /// <paramref name="path" />参数描述的现有目录。
    ///                                 </exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 包含一个冒号字符 (:)，该冒号字符不是驱动器标签（“C:\”）的一部分。
    ///                             </exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public DirectoryInfo CreateSubdirectory(string path, DirectorySecurity directorySecurity)
    {
      return this.CreateSubdirectoryHelper(path, (object) directorySecurity);
    }

    [SecurityCritical]
    private DirectoryInfo CreateSubdirectoryHelper(string path, object directorySecurity)
    {
      string fullPathInternal = Path.GetFullPathInternal(Path.InternalCombine(this.FullPath, path));
      if (string.Compare(this.FullPath, 0, fullPathInternal, 0, this.FullPath.Length, StringComparison.OrdinalIgnoreCase) != 0)
      {
        string displayablePath = __Error.GetDisplayablePath(this.DisplayPath, false);
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSubPath", (object) path, (object) displayablePath));
      }
      new FileIOPermission(FileIOPermissionAccess.Write, new string[1]
      {
        Directory.GetDemandDir(fullPathInternal, true)
      }, 0 != 0, 0 != 0).Demand();
      Directory.InternalCreateDirectory(fullPathInternal, path, directorySecurity);
      return new DirectoryInfo(fullPathInternal);
    }

    /// <summary>创建目录。</summary>
    /// <exception cref="T:System.IO.IOException">不能创建该目录。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public void Create()
    {
      Directory.InternalCreateDirectory(this.FullPath, this.OriginalPath, (object) null, true);
    }

    /// <summary>创建一个目录使用
    /// <see cref="T:System.Security.AccessControl.DirectorySecurity" /> 对象。
    ///                             </summary>
    /// <param name="directorySecurity">要应用于此目录的访问控制。</param>
    /// <exception cref="T:System.IO.IOException">指定的目录
    /// <paramref name="path" />是只读的或不为空。
    ///                                     </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" />是一个长度为零的字符串、 仅包含空白，或由定义包含一个或多个无效字符
    ///                                 <see cref="F:System.IO.Path.InvalidPathChars" />.
    ///                             </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" />是
    ///                                 null.
    ///                             </exception>
    /// <exception cref="T:System.IO.PathTooLongException">指定的路径、文件名或者两者都超出了系统定义的最大长度。例如，在基于 Windows 的平台上，路径必须小于 248 个字符，文件名必须小于 260 个字符。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效，比如在未映射的驱动器上。</exception>
    /// <exception cref="T:System.NotSupportedException">试图仅通过冒号 (:) 字符创建目录。</exception>
    /// <exception cref="T:System.IO.IOException">指定的目录
    /// <paramref name="path" />是只读的或不为空。
    ///                                     </exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public void Create(DirectorySecurity directorySecurity)
    {
      Directory.InternalCreateDirectory(this.FullPath, this.OriginalPath, (object) directorySecurity, true);
    }

    /// <summary>获取
    /// <see cref="T:System.Security.AccessControl.DirectorySecurity" />封装所描述的当前目录的访问控制列表 (ACL) 项的对象
    /// <see cref="T:System.IO.DirectoryInfo" /> 对象。
    ///                             </summary>
    /// <returns>包含当前请求的 URL 的
    /// <see cref="T:System.Security.AccessControl.DirectorySecurity" />封装的目录的访问控制规则的对象。
    ///                             </returns>
    /// <exception cref="T:System.SystemException">未能找到或修改该目录。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">当前进程不具有打开该目录的权限。</exception>
    /// <exception cref="T:System.IO.IOException">打开目录时发生 I/O 错误。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Microsoft Windows 2000 或更高版本。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">目录为只读。- 或 -当前平台不支持此操作。- 或 -调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public DirectorySecurity GetAccessControl()
    {
      return Directory.GetAccessControl(this.FullPath, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
    }

    /// <summary>获取
    /// <see cref="T:System.Security.AccessControl.DirectorySecurity" />封装由当前所描述的目录的访问控制列表 (ACL) 项的指定的类型的对象
    /// <see cref="T:System.IO.DirectoryInfo" /> 对象。
    ///                             </summary>
    /// <returns>包含当前请求的 URL 的
    /// <see cref="T:System.Security.AccessControl.DirectorySecurity" />对象，它封装的访问控制规则所描述的文件
    /// <paramref name="path" />参数。
    ///                             异常异常类型条件<see cref="T:System.SystemException" />未能找到或修改该目录。<see cref="T:System.UnauthorizedAccessException" />当前进程不具有打开该目录的权限。<see cref="T:System.IO.IOException" />打开目录时发生 I/O 错误。<see cref="T:System.PlatformNotSupportedException" />当前操作系统不是 Microsoft Windows 2000 或更高版本。<see cref="T:System.UnauthorizedAccessException" />目录为只读。- 或 -当前平台不支持此操作。- 或 -调用方没有所要求的权限。</returns>
    /// <param name="includeSections">之一
    /// <see cref="T:System.Security.AccessControl.AccessControlSections" />指定要接收的访问控制列表 (ACL) 信息的类型的值。
    ///                                 </param>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public DirectorySecurity GetAccessControl(AccessControlSections includeSections)
    {
      return Directory.GetAccessControl(this.FullPath, includeSections);
    }

    /// <summary>将应用所描述的访问控制列表 (ACL) 项
    /// <see cref="T:System.Security.AccessControl.DirectorySecurity" />所描述的当前目录对象
    /// <see cref="T:System.IO.DirectoryInfo" /> 对象。
    ///                             </summary>
    /// <param name="directorySecurity">描述将应用于所描述的目录的 ACL 项的对象
    /// <paramref name="path" />参数。
    ///                                 </param>
    /// <exception cref="T:System.ArgumentNullException">新任务将观察的
    /// <paramref name="directorySecurity" />参数是
    ///                                         null.
    ///                                     </exception>
    /// <exception cref="T:System.SystemException">未能找到或修改文件。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">当前进程不具有打开该文件的权限。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Microsoft Windows 2000 或更高版本。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public void SetAccessControl(DirectorySecurity directorySecurity)
    {
      Directory.SetAccessControl(this.FullPath, directorySecurity);
    }

    /// <summary>返回当前目录中与给定的搜索模式匹配的文件列表。</summary>
    /// <returns>类型的数组
    ///     <see cref="T:System.IO.FileInfo" />.
    /// </returns>
    /// <param name="searchPattern">要与文件名匹配的搜索字符串。此参数可以包含有效文本路径和通配符（* 和 ?）的组合（请参见“备注”），但不支持正则表达式。默认模式为“*”，该模式返回所有文件。</param>
    /// <exception cref="T:System.ArgumentException">
    ///         <paramref name="searchPattern" />包含由定义的一个或多个无效字符
    /// <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法。
    ///                                     </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="searchPattern" />是
    ///                                 null.
    ///                             </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">路径无效（例如，在未映射的驱动器上）。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public FileInfo[] GetFiles(string searchPattern)
    {
      if (searchPattern == null)
        throw new ArgumentNullException("searchPattern");
      return this.InternalGetFiles(searchPattern, SearchOption.TopDirectoryOnly);
    }

    /// <summary>返回当前目录的文件列表，该列表与给定的搜索模式匹配并且使用某个值确定是否搜索子目录。</summary>
    /// <returns>类型的数组
    ///     <see cref="T:System.IO.FileInfo" />.
    /// </returns>
    /// <param name="searchPattern">要与文件名匹配的搜索字符串。此参数可以包含有效文本路径和通配符（* 和 ?）的组合（请参见“备注”），但不支持正则表达式。默认模式为“*”，该模式返回所有文件。</param>
    /// <param name="searchOption">指定搜索操作是应仅包含当前目录还是应包含所有子目录的枚举值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    ///         <paramref name="searchPattern" />包含由定义的一个或多个无效字符
    /// <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法。
    ///                                     </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="searchPattern" />是
    ///                                 null.
    ///                             </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///         <paramref name="searchOption" />不是有效
    /// <see cref="T:System.IO.SearchOption" />值。
    ///                                     </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">路径无效（例如，在未映射的驱动器上）。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public FileInfo[] GetFiles(string searchPattern, SearchOption searchOption)
    {
      if (searchPattern == null)
        throw new ArgumentNullException("searchPattern");
      if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
        throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      return this.InternalGetFiles(searchPattern, searchOption);
    }

    private FileInfo[] InternalGetFiles(string searchPattern, SearchOption searchOption)
    {
      return new List<FileInfo>(FileSystemEnumerableFactory.CreateFileInfoIterator(this.FullPath, this.OriginalPath, searchPattern, searchOption)).ToArray();
    }

    /// <summary>返回当前目录的文件列表。</summary>
    /// <returns>类型的数组
    ///     <see cref="T:System.IO.FileInfo" />.
    /// </returns>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">该路径无效，比如在未映射的驱动器上。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public FileInfo[] GetFiles()
    {
      return this.InternalGetFiles("*", SearchOption.TopDirectoryOnly);
    }

    /// <summary>返回当前目录的子目录。</summary>
    /// <returns>一个数组
    /// <see cref="T:System.IO.DirectoryInfo" /> 对象。
    ///                             </returns>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">该路径封装在
    /// <see cref="T:System.IO.DirectoryInfo" />对象是无效的例如，位于未映射的驱动器上。
    ///                                     </exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public DirectoryInfo[] GetDirectories()
    {
      return this.InternalGetDirectories("*", SearchOption.TopDirectoryOnly);
    }

    /// <summary>检索数组的强类型化
    /// <see cref="T:System.IO.FileSystemInfo" />对象表示的文件和与指定的搜索条件匹配的子目录。
    ///                             </summary>
    /// <returns>强类型的数组
    /// FileSystemInfo与搜索条件匹配的对象。
    ///                             </returns>
    /// <param name="searchPattern">要与目录和文件的名称匹配的搜索字符串。此参数可以包含有效文本路径和通配符（* 和 ?）的组合（请参见“备注”），但不支持正则表达式。默认模式为“*”，该模式返回所有文件。</param>
    /// <exception cref="T:System.ArgumentException">
    ///         <paramref name="searchPattern" />包含由定义的一个或多个无效字符
    /// <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法。
    ///                                     </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="searchPattern" />是
    ///                                 null.
    ///                             </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public FileSystemInfo[] GetFileSystemInfos(string searchPattern)
    {
      if (searchPattern == null)
        throw new ArgumentNullException("searchPattern");
      return this.InternalGetFileSystemInfos(searchPattern, SearchOption.TopDirectoryOnly);
    }

    /// <summary>检索的数组
    /// <see cref="T:System.IO.FileSystemInfo" />表示的文件和子目录的指定的搜索条件匹配的对象。
    ///                             </summary>
    /// <returns>与搜索条件匹配的文件系统项的数组。</returns>
    /// <param name="searchPattern">要与目录和文件的名称匹配的搜索字符串。此参数可以包含有效文本路径和通配符（* 和 ?）的组合（请参见“备注”），但不支持正则表达式。默认模式为“*”，该模式返回所有文件。</param>
    /// <param name="searchOption">指定搜索操作是应仅包含当前目录还是应包含所有子目录的枚举值之一。默认值是
    ///     <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///         <paramref name="searchPattern" />包含由定义的一个或多个无效字符
    /// <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法。
    ///                                     </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="searchPattern" />是
    ///                                 null.
    ///                             </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///         <paramref name="searchOption" />不是有效
    /// <see cref="T:System.IO.SearchOption" />值。
    ///                                     </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效（例如，它位于未映射的驱动器上）。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public FileSystemInfo[] GetFileSystemInfos(string searchPattern, SearchOption searchOption)
    {
      if (searchPattern == null)
        throw new ArgumentNullException("searchPattern");
      if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
        throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      return this.InternalGetFileSystemInfos(searchPattern, searchOption);
    }

    private FileSystemInfo[] InternalGetFileSystemInfos(string searchPattern, SearchOption searchOption)
    {
      return new List<FileSystemInfo>(FileSystemEnumerableFactory.CreateFileSystemInfoIterator(this.FullPath, this.OriginalPath, searchPattern, searchOption)).ToArray();
    }

    /// <summary>返回强类型的数组
    /// <see cref="T:System.IO.FileSystemInfo" />表示所有文件和目录的子目录中的项。
    ///                             </summary>
    /// <returns>强类型的数组
    /// <see cref="T:System.IO.FileSystemInfo" />条目。
    ///                             </returns>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">路径无效（例如，在未映射的驱动器上）。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public FileSystemInfo[] GetFileSystemInfos()
    {
      return this.InternalGetFileSystemInfos("*", SearchOption.TopDirectoryOnly);
    }

    /// <summary>返回一个数组中当前的目录
    /// <see cref="T:System.IO.DirectoryInfo" />匹配给定的搜索条件。
    ///                             </summary>
    /// <returns>类型的数组
    /// DirectoryInfo匹配
    ///                                 <paramref name="searchPattern" />.
    ///                             </returns>
    /// <param name="searchPattern">要与目录名匹配的搜索字符串。此参数可以包含有效文本路径和通配符（* 和 ?）的组合（请参见“备注”），但不支持正则表达式。默认模式为“*”，该模式返回所有文件。</param>
    /// <exception cref="T:System.ArgumentException">
    ///         <paramref name="searchPattern" />包含由定义的一个或多个无效字符
    /// <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法。
    ///                                     </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="searchPattern" />是
    ///                                 null.
    ///                             </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">该路径封装在
    /// DirectoryInfo对象无效 （例如，它位于未映射的驱动器上）。
    ///                                     </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public DirectoryInfo[] GetDirectories(string searchPattern)
    {
      if (searchPattern == null)
        throw new ArgumentNullException("searchPattern");
      return this.InternalGetDirectories(searchPattern, SearchOption.TopDirectoryOnly);
    }

    /// <summary>返回一个数组中当前的目录
    /// <see cref="T:System.IO.DirectoryInfo" />与给定的搜索条件相匹配，并使用一个值以确定是否搜索子目录。
    ///                             </summary>
    /// <returns>类型的数组
    /// DirectoryInfo匹配
    ///                                 <paramref name="searchPattern" />.
    ///                             </returns>
    /// <param name="searchPattern">要与目录名匹配的搜索字符串。此参数可以包含有效文本路径和通配符（* 和 ?）的组合（请参见“备注”），但不支持正则表达式。默认模式为“*”，该模式返回所有文件。</param>
    /// <param name="searchOption">指定搜索操作是应仅包含当前目录还是应包含所有子目录的枚举值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    ///         <paramref name="searchPattern" />包含由定义的一个或多个无效字符
    /// <see cref="M:System.IO.Path.GetInvalidPathChars" /> 方法。
    ///                                     </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="searchPattern" />是
    ///                                 null.
    ///                             </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///         <paramref name="searchOption" />不是有效
    /// <see cref="T:System.IO.SearchOption" />值。
    ///                                     </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">该路径封装在
    /// DirectoryInfo对象无效 （例如，它位于未映射的驱动器上）。
    ///                                     </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    public DirectoryInfo[] GetDirectories(string searchPattern, SearchOption searchOption)
    {
      if (searchPattern == null)
        throw new ArgumentNullException("searchPattern");
      if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
        throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      return this.InternalGetDirectories(searchPattern, searchOption);
    }

    private DirectoryInfo[] InternalGetDirectories(string searchPattern, SearchOption searchOption)
    {
      return new List<DirectoryInfo>(FileSystemEnumerableFactory.CreateDirectoryInfoIterator(this.FullPath, this.OriginalPath, searchPattern, searchOption)).ToArray();
    }

    /// <summary>返回当前目录中目录信息的可枚举集合。</summary>
    /// <returns>当前目录中目录的可枚举集合。</returns>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">该路径封装在
    /// <see cref="T:System.IO.DirectoryInfo" />对象无效 （例如，它位于未映射的驱动器上）。
    ///                                     </exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public IEnumerable<DirectoryInfo> EnumerateDirectories()
    {
      return this.InternalEnumerateDirectories("*", SearchOption.TopDirectoryOnly);
    }

    /// <summary>返回与指定的搜索模式匹配的目录信息的可枚举集合。</summary>
    /// <returns>匹配的目录的可枚举集合
    ///     <paramref name="searchPattern" />.
    /// </returns>
    /// <param name="searchPattern">要与目录名匹配的搜索字符串。此参数可以包含有效文本路径和通配符（* 和 ?）的组合（请参见“备注”），但不支持正则表达式。默认模式为“*”，该模式返回所有文件。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="searchPattern" />是
    ///                                 null.
    ///                             </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">该路径封装在
    /// <see cref="T:System.IO.DirectoryInfo" />对象无效 （例如，它位于未映射的驱动器上）。
    ///                                     </exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public IEnumerable<DirectoryInfo> EnumerateDirectories(string searchPattern)
    {
      if (searchPattern == null)
        throw new ArgumentNullException("searchPattern");
      return this.InternalEnumerateDirectories(searchPattern, SearchOption.TopDirectoryOnly);
    }

    /// <summary>返回与指定的搜索模式和搜索子目录选项匹配的目录信息的可枚举集合。</summary>
    /// <returns>匹配的目录的可枚举集合
    /// <paramref name="searchPattern" />和
    ///                                 <paramref name="searchOption" />.
    ///                             </returns>
    /// <param name="searchPattern">要与目录名匹配的搜索字符串。此参数可以包含有效文本路径和通配符（* 和 ?）的组合（请参见“备注”），但不支持正则表达式。默认模式为“*”，该模式返回所有文件。</param>
    /// <param name="searchOption">指定搜索操作是应仅包含当前目录还是应包含所有子目录的枚举值之一。默认值是
    ///     <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="searchPattern" />是
    ///                                 null.
    ///                             </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///         <paramref name="searchOption" />不是有效
    /// <see cref="T:System.IO.SearchOption" />值。
    ///                                     </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">该路径封装在
    /// <see cref="T:System.IO.DirectoryInfo" />对象无效 （例如，它位于未映射的驱动器上）。
    ///                                     </exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public IEnumerable<DirectoryInfo> EnumerateDirectories(string searchPattern, SearchOption searchOption)
    {
      if (searchPattern == null)
        throw new ArgumentNullException("searchPattern");
      if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
        throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      return this.InternalEnumerateDirectories(searchPattern, searchOption);
    }

    private IEnumerable<DirectoryInfo> InternalEnumerateDirectories(string searchPattern, SearchOption searchOption)
    {
      return FileSystemEnumerableFactory.CreateDirectoryInfoIterator(this.FullPath, this.OriginalPath, searchPattern, searchOption);
    }

    /// <summary>返回当前目录中的文件信息的可枚举集合。</summary>
    /// <returns>当前目录中的文件的可枚举集合。</returns>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">该路径封装在
    /// <see cref="T:System.IO.DirectoryInfo" />对象无效 （例如，它位于未映射的驱动器上）。
    ///                                     </exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public IEnumerable<FileInfo> EnumerateFiles()
    {
      return this.InternalEnumerateFiles("*", SearchOption.TopDirectoryOnly);
    }

    /// <summary>返回与搜索模式匹配的文件信息的可枚举集合。</summary>
    /// <returns>匹配的文件的可枚举集合
    ///     <paramref name="searchPattern" />.
    /// </returns>
    /// <param name="searchPattern">要与文件名匹配的搜索字符串。此参数可以包含有效文本路径和通配符（* 和 ?）的组合（请参见“备注”），但不支持正则表达式。默认模式为“*”，该模式返回所有文件。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="searchPattern" />是
    ///                                 null.
    ///                             </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">该路径封装在
    /// <see cref="T:System.IO.DirectoryInfo" />对象无效，（例如，它位于未映射的驱动器上）。
    ///                                     </exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public IEnumerable<FileInfo> EnumerateFiles(string searchPattern)
    {
      if (searchPattern == null)
        throw new ArgumentNullException("searchPattern");
      return this.InternalEnumerateFiles(searchPattern, SearchOption.TopDirectoryOnly);
    }

    /// <summary>返回与指定的搜索模式和搜索子目录选项匹配的文件信息的可枚举集合。</summary>
    /// <returns>匹配的文件的可枚举集合
    /// <paramref name="searchPattern" />和
    ///                                 <paramref name="searchOption" />.
    ///                             </returns>
    /// <param name="searchPattern">要与文件名匹配的搜索字符串。此参数可以包含有效文本路径和通配符（* 和 ?）的组合（请参见“备注”），但不支持正则表达式。默认模式为“*”，该模式返回所有文件。</param>
    /// <param name="searchOption">指定搜索操作是应仅包含当前目录还是应包含所有子目录的枚举值之一。默认值是
    ///     <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="searchPattern" />是
    ///                                 null.
    ///                             </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///         <paramref name="searchOption" />不是有效
    /// <see cref="T:System.IO.SearchOption" />值。
    ///                                     </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">该路径封装在
    /// <see cref="T:System.IO.DirectoryInfo" />对象无效 （例如，它位于未映射的驱动器上）。
    ///                                     </exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public IEnumerable<FileInfo> EnumerateFiles(string searchPattern, SearchOption searchOption)
    {
      if (searchPattern == null)
        throw new ArgumentNullException("searchPattern");
      if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
        throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      return this.InternalEnumerateFiles(searchPattern, searchOption);
    }

    private IEnumerable<FileInfo> InternalEnumerateFiles(string searchPattern, SearchOption searchOption)
    {
      return FileSystemEnumerableFactory.CreateFileInfoIterator(this.FullPath, this.OriginalPath, searchPattern, searchOption);
    }

    /// <summary>返回当前目录中的文件系统信息的可枚举集合。</summary>
    /// <returns>当前目录中的文件系统信息的可枚举集合。</returns>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">该路径封装在
    /// <see cref="T:System.IO.DirectoryInfo" />对象无效 （例如，它位于未映射的驱动器上）。
    ///                                     </exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos()
    {
      return this.InternalEnumerateFileSystemInfos("*", SearchOption.TopDirectoryOnly);
    }

    /// <summary>返回与指定的搜索模式匹配的文件系统信息的可枚举集合。</summary>
    /// <returns>匹配的文件系统信息对象的可枚举集合
    ///     <paramref name="searchPattern" />.
    /// </returns>
    /// <param name="searchPattern">要与目录名匹配的搜索字符串。此参数可以包含有效文本路径和通配符（* 和 ?）的组合（请参见“备注”），但不支持正则表达式。默认模式为“*”，该模式返回所有文件。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="searchPattern" />是
    ///                                 null.
    ///                             </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">该路径封装在
    /// <see cref="T:System.IO.DirectoryInfo" />对象无效 （例如，它位于未映射的驱动器上）。
    ///                                     </exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos(string searchPattern)
    {
      if (searchPattern == null)
        throw new ArgumentNullException("searchPattern");
      return this.InternalEnumerateFileSystemInfos(searchPattern, SearchOption.TopDirectoryOnly);
    }

    /// <summary>返回与指定的搜索模式和搜索子目录选项匹配的文件系统信息的可枚举集合。</summary>
    /// <returns>匹配的文件系统信息对象的可枚举集合
    /// <paramref name="searchPattern" />和
    ///                                 <paramref name="searchOption" />.
    ///                             </returns>
    /// <param name="searchPattern">要与目录名匹配的搜索字符串。此参数可以包含有效文本路径和通配符（* 和 ?）的组合（请参见“备注”），但不支持正则表达式。默认模式为“*”，该模式返回所有文件。</param>
    /// <param name="searchOption">指定搜索操作是应仅包含当前目录还是应包含所有子目录的枚举值之一。默认值是
    ///     <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="searchPattern" />是
    ///                                 null.
    ///                             </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///         <paramref name="searchOption" />不是有效
    /// <see cref="T:System.IO.SearchOption" />值。
    ///                                     </exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">该路径封装在
    /// <see cref="T:System.IO.DirectoryInfo" />对象无效 （例如，它位于未映射的驱动器上）。
    ///                                     </exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos(string searchPattern, SearchOption searchOption)
    {
      if (searchPattern == null)
        throw new ArgumentNullException("searchPattern");
      if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
        throw new ArgumentOutOfRangeException("searchOption", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      return this.InternalEnumerateFileSystemInfos(searchPattern, searchOption);
    }

    private IEnumerable<FileSystemInfo> InternalEnumerateFileSystemInfos(string searchPattern, SearchOption searchOption)
    {
      return FileSystemEnumerableFactory.CreateFileSystemInfoIterator(this.FullPath, this.OriginalPath, searchPattern, searchOption);
    }

    /// <summary>将移动
    /// <see cref="T:System.IO.DirectoryInfo" />实例，并且其内容进行新的路径。
    ///                             </summary>
    /// <param name="destDirName">要将此目录移动到的目标位置的名称和路径。目标不能是另一个具有相同名称的磁盘卷或目录。它可以是你要将此目录作为子目录添加到某个现有目录。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destDirName" />是
    ///                                 null.
    ///                             </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="destDirName" /> 是空字符串 ("")。
    ///                             </exception>
    /// <exception cref="T:System.IO.IOException">试图将一个目录移到不同的卷。- 或 -<paramref name="destDirName" /> 已存在。
    /// - 或 -您无权访问此路径。- 或 -被移动的目录与目标目录同名。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">找不到此目标目录。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public void MoveTo(string destDirName)
    {
      if (destDirName == null)
        throw new ArgumentNullException("destDirName");
      if (destDirName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destDirName");
      new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, this.demandDir, false, false).Demand();
      string fullPathInternal = Path.GetFullPathInternal(destDirName);
      if (!fullPathInternal.EndsWith(Path.DirectorySeparatorChar))
        fullPathInternal += Path.DirectorySeparatorChar.ToString();
      new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, fullPathInternal + ".").Demand();
      string str = !this.FullPath.EndsWith(Path.DirectorySeparatorChar) ? this.FullPath + Path.DirectorySeparatorChar.ToString() : this.FullPath;
      if (string.Compare(str, fullPathInternal, StringComparison.OrdinalIgnoreCase) == 0)
        throw new IOException(Environment.GetResourceString("IO.IO_SourceDestMustBeDifferent"));
      if (string.Compare(Path.GetPathRoot(str), Path.GetPathRoot(fullPathInternal), StringComparison.OrdinalIgnoreCase) != 0)
        throw new IOException(Environment.GetResourceString("IO.IO_SourceDestMustHaveSameRoot"));
      if (!Win32Native.MoveFile(this.FullPath, destDirName))
      {
        int errorCode = Marshal.GetLastWin32Error();
        if (errorCode == 2)
        {
          errorCode = 3;
          __Error.WinIOError(errorCode, this.DisplayPath);
        }
        if (errorCode == 5)
          throw new IOException(Environment.GetResourceString("UnauthorizedAccess_IODenied_Path", (object) this.DisplayPath));
        __Error.WinIOError(errorCode, string.Empty);
      }
      this.FullPath = fullPathInternal;
      this.OriginalPath = destDirName;
      this.DisplayPath = DirectoryInfo.GetDisplayName(this.OriginalPath, this.FullPath);
      this.demandDir = new string[1]
      {
        Directory.GetDemandDir(this.FullPath, true)
      };
      this._dataInitialised = -1;
    }

    /// <summary>这将删除
    /// <see cref="T:System.IO.DirectoryInfo" />如果它为空。
    ///                             </summary>
    /// <exception cref="T:System.UnauthorizedAccessException">目录中包含一个只读文件。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">此描述的目录
    /// <see cref="T:System.IO.DirectoryInfo" />对象不存在或找不到。
    ///                                     </exception>
    /// <exception cref="T:System.IO.IOException">目录不为空。- 或 -该目录为应用程序的当前工作目录。- 或 -对于目录有打开句柄，并且操作系统是 Windows XP 或更早版本。此打开句柄可能是由于枚举目录导致的。有关详细信息，请参阅
    ///     如何：枚举目录和文件.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override void Delete()
    {
      Directory.Delete(this.FullPath, this.OriginalPath, false, true);
    }

    /// <summary>删除此实例
    /// <see cref="T:System.IO.DirectoryInfo" />指定是否删除子目录和文件。
    ///                             </summary>
    /// <param name="recursive">true若要删除此目录、 及其子目录中，和所有文件 ；否则为
    ///     false.
    /// </param>
    /// <exception cref="T:System.UnauthorizedAccessException">目录中包含一个只读文件。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">此描述的目录
    /// <see cref="T:System.IO.DirectoryInfo" />对象不存在或找不到。
    ///                                     </exception>
    /// <exception cref="T:System.IO.IOException">目录为只读。- 或 -该目录包含一个或多个文件或子目录和
    /// <paramref name="recursive" />是
    ///                                         false.
    ///                                     - 或 -该目录为应用程序的当前工作目录。- 或 -对于目录或其文件之一有打开句柄，并且操作系统是 Windows XP 或更早版本。此打开句柄可能是由于枚举目录和文件导致的。有关详细信息，请参阅
    ///                                     如何：枚举目录和文件.
    ///                                 </exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public void Delete(bool recursive)
    {
      Directory.Delete(this.FullPath, this.OriginalPath, recursive, true);
    }

    /// <summary>返回用户所传递的原始路径。</summary>
    /// <returns>返回用户所传递的原始路径。</returns>
    /// <filterpriority>2</filterpriority>
    public override string ToString()
    {
      return this.DisplayPath;
    }

    private static string GetDisplayName(string originalPath, string fullPath)
    {
      return originalPath.Length != 2 || (int) originalPath[1] != 58 ? originalPath : ".";
    }

    private static string GetDirName(string fullPath)
    {
      string str;
      if (fullPath.Length > 3)
      {
        string path = fullPath;
        if (fullPath.EndsWith(Path.DirectorySeparatorChar))
          path = fullPath.Substring(0, fullPath.Length - 1);
        str = Path.GetFileName(path);
      }
      else
        str = fullPath;
      return str;
    }
  }
}
