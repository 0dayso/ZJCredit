// Decompiled with JetBrains decompiler
// Type: System.IO.FileSystemInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.IO
{
  /// <summary>为 <see cref="T:System.IO.FileInfo" /> 和 <see cref="T:System.IO.DirectoryInfo" /> 对象提供基类。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  [FileIOPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
  public abstract class FileSystemInfo : MarshalByRefObject, ISerializable
  {
    internal int _dataInitialised = -1;
    private string _displayPath = "";
    [SecurityCritical]
    internal Win32Native.WIN32_FILE_ATTRIBUTE_DATA _data;
    private const int ERROR_INVALID_PARAMETER = 87;
    internal const int ERROR_ACCESS_DENIED = 5;
    /// <summary>表示目录或文件的完全限定目录。</summary>
    /// <exception cref="T:System.IO.PathTooLongException">完全限定路径为 260 或更多字符。</exception>
    protected string FullPath;
    /// <summary>最初由用户指定的目录（不论是相对目录还是绝对目录）。</summary>
    protected string OriginalPath;

    /// <summary>获取目录或文件的完整目录。</summary>
    /// <returns>包含完整目录的字符串。</returns>
    /// <exception cref="T:System.IO.PathTooLongException">完全限定路径或文件名为 260 或更多字符。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public virtual string FullName
    {
      [SecuritySafeCritical] get
      {
        new FileIOPermission(FileIOPermissionAccess.PathDiscovery, !(this is DirectoryInfo) ? this.FullPath : Directory.GetDemandDir(this.FullPath, true)).Demand();
        return this.FullPath;
      }
    }

    internal virtual string UnsafeGetFullName
    {
      [SecurityCritical] get
      {
        new FileIOPermission(FileIOPermissionAccess.PathDiscovery, !(this is DirectoryInfo) ? this.FullPath : Directory.GetDemandDir(this.FullPath, true)).Demand();
        return this.FullPath;
      }
    }

    /// <summary>获取表示文件扩展名部分的字符串。</summary>
    /// <returns>包含 <see cref="T:System.IO.FileSystemInfo" /> 扩展名的字符串。</returns>
    /// <filterpriority>1</filterpriority>
    public string Extension
    {
      get
      {
        int length = this.FullPath.Length;
        int startIndex = length;
        while (--startIndex >= 0)
        {
          char ch = this.FullPath[startIndex];
          if ((int) ch == 46)
            return this.FullPath.Substring(startIndex, length - startIndex);
          if ((int) ch == (int) Path.DirectorySeparatorChar || (int) ch == (int) Path.AltDirectorySeparatorChar || (int) ch == (int) Path.VolumeSeparatorChar)
            break;
        }
        return string.Empty;
      }
    }

    /// <summary>对于文件，获取该文件的名称。对于目录，如果存在层次结构，则获取层次结构中最后一个目录的名称。否则，Name 属性获取该目录的名称。</summary>
    /// <returns>一个字符串，它是父目录的名称、层次结构中最后一个目录的名称或文件的名称（包括文件扩展名）。</returns>
    /// <filterpriority>1</filterpriority>
    public abstract string Name { get; }

    /// <summary>获取指示文件或目录是否存在的值。</summary>
    /// <returns>如果文件或目录存在，则为 true；否则为 false。</returns>
    /// <filterpriority>1</filterpriority>
    public abstract bool Exists { get; }

    /// <summary>获取或设置当前文件或目录的创建时间。</summary>
    /// <returns>当前 <see cref="T:System.IO.FileSystemInfo" /> 对象的创建日期和时间。</returns>
    /// <exception cref="T:System.IO.IOException">
    /// <see cref="M:System.IO.FileSystemInfo.Refresh" /> 不能初始化数据。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效：例如，它位于未映射的驱动器上。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Windows NT 或更高版本。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">调用方试图设置无效的创建时间。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public DateTime CreationTime
    {
      get
      {
        return this.CreationTimeUtc.ToLocalTime();
      }
      set
      {
        this.CreationTimeUtc = value.ToUniversalTime();
      }
    }

    /// <summary>获取或设置当前文件或目录的创建时间，其格式为协调世界时 (UTC)。</summary>
    /// <returns>当前 <see cref="T:System.IO.FileSystemInfo" /> 对象的创建日期及时间（UTC 格式）。</returns>
    /// <exception cref="T:System.IO.IOException">
    /// <see cref="M:System.IO.FileSystemInfo.Refresh" /> 不能初始化数据。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效：例如，它位于未映射的驱动器上。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Windows NT 或更高版本。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">调用方试图设置无效的访问时间。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [ComVisible(false)]
    public DateTime CreationTimeUtc
    {
      [SecuritySafeCritical] get
      {
        if (this._dataInitialised == -1)
        {
          this._data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
          this.Refresh();
        }
        if (this._dataInitialised != 0)
          __Error.WinIOError(this._dataInitialised, this.DisplayPath);
        return DateTime.FromFileTimeUtc((long) this._data.ftCreationTimeHigh << 32 | (long) this._data.ftCreationTimeLow);
      }
      set
      {
        if (this is DirectoryInfo)
          Directory.SetCreationTimeUtc(this.FullPath, value);
        else
          File.SetCreationTimeUtc(this.FullPath, value);
        this._dataInitialised = -1;
      }
    }

    /// <summary>获取或设置上次访问当前文件或目录的时间。</summary>
    /// <returns>上次访问当前文件或目录的时间。</returns>
    /// <exception cref="T:System.IO.IOException">
    /// <see cref="M:System.IO.FileSystemInfo.Refresh" /> 不能初始化数据。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Windows NT 或更高版本。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">调用方试图设置无效的访问时间</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public DateTime LastAccessTime
    {
      get
      {
        return this.LastAccessTimeUtc.ToLocalTime();
      }
      set
      {
        this.LastAccessTimeUtc = value.ToUniversalTime();
      }
    }

    /// <summary>获取或设置上次访问当前文件或目录的时间，其格式为协调世界时 (UTC)。</summary>
    /// <returns>上次访问当前文件或目录的 UTC 时间。</returns>
    /// <exception cref="T:System.IO.IOException">
    /// <see cref="M:System.IO.FileSystemInfo.Refresh" /> 不能初始化数据。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Windows NT 或更高版本。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">调用方试图设置无效的访问时间。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [ComVisible(false)]
    public DateTime LastAccessTimeUtc
    {
      [SecuritySafeCritical] get
      {
        if (this._dataInitialised == -1)
        {
          this._data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
          this.Refresh();
        }
        if (this._dataInitialised != 0)
          __Error.WinIOError(this._dataInitialised, this.DisplayPath);
        return DateTime.FromFileTimeUtc((long) this._data.ftLastAccessTimeHigh << 32 | (long) this._data.ftLastAccessTimeLow);
      }
      set
      {
        if (this is DirectoryInfo)
          Directory.SetLastAccessTimeUtc(this.FullPath, value);
        else
          File.SetLastAccessTimeUtc(this.FullPath, value);
        this._dataInitialised = -1;
      }
    }

    /// <summary>获取或设置上次写入当前文件或目录的时间。</summary>
    /// <returns>上次写入当前文件的时间。</returns>
    /// <exception cref="T:System.IO.IOException">
    /// <see cref="M:System.IO.FileSystemInfo.Refresh" /> 不能初始化数据。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Windows NT 或更高版本。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">调用方试图设置无效的写入时间。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public DateTime LastWriteTime
    {
      get
      {
        return this.LastWriteTimeUtc.ToLocalTime();
      }
      set
      {
        this.LastWriteTimeUtc = value.ToUniversalTime();
      }
    }

    /// <summary>获取或设置上次写入当前文件或目录的时间，其格式为协调世界时 (UTC)。</summary>
    /// <returns>上次写入当前文件的 UTC 时间。</returns>
    /// <exception cref="T:System.IO.IOException">
    /// <see cref="M:System.IO.FileSystemInfo.Refresh" /> 不能初始化数据。</exception>
    /// <exception cref="T:System.PlatformNotSupportedException">当前操作系统不是 Windows NT 或更高版本。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">调用方试图设置无效的写入时间。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [ComVisible(false)]
    public DateTime LastWriteTimeUtc
    {
      [SecuritySafeCritical] get
      {
        if (this._dataInitialised == -1)
        {
          this._data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
          this.Refresh();
        }
        if (this._dataInitialised != 0)
          __Error.WinIOError(this._dataInitialised, this.DisplayPath);
        return DateTime.FromFileTimeUtc((long) this._data.ftLastWriteTimeHigh << 32 | (long) this._data.ftLastWriteTimeLow);
      }
      set
      {
        if (this is DirectoryInfo)
          Directory.SetLastWriteTimeUtc(this.FullPath, value);
        else
          File.SetLastWriteTimeUtc(this.FullPath, value);
        this._dataInitialised = -1;
      }
    }

    /// <summary>获取或设置当前文件或目录的特性。</summary>
    /// <returns>当前 <see cref="T:System.IO.FileSystemInfo" /> 的 <see cref="T:System.IO.FileAttributes" />。</returns>
    /// <exception cref="T:System.IO.FileNotFoundException">指定的文件不存在。</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效：例如，它位于未映射的驱动器上。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.ArgumentException">调用方试图设置无效的文件属性。- 或 -用户尝试设置属性值，但没有写入权限。</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <see cref="M:System.IO.FileSystemInfo.Refresh" /> 不能初始化数据。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public FileAttributes Attributes
    {
      [SecuritySafeCritical] get
      {
        if (this._dataInitialised == -1)
        {
          this._data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
          this.Refresh();
        }
        if (this._dataInitialised != 0)
          __Error.WinIOError(this._dataInitialised, this.DisplayPath);
        return (FileAttributes) this._data.fileAttributes;
      }
      [SecuritySafeCritical] set
      {
        new FileIOPermission(FileIOPermissionAccess.Write, this.FullPath).Demand();
        if (!Win32Native.SetFileAttributes(this.FullPath, (int) value))
        {
          int lastWin32Error = Marshal.GetLastWin32Error();
          int num1 = 87;
          if (lastWin32Error == num1)
            throw new ArgumentException(Environment.GetResourceString("Arg_InvalidFileAttrs"));
          int num2 = 5;
          if (lastWin32Error == num2)
            throw new ArgumentException(Environment.GetResourceString("UnauthorizedAccess_IODenied_NoPathName"));
          string displayPath = this.DisplayPath;
          __Error.WinIOError(lastWin32Error, displayPath);
        }
        this._dataInitialised = -1;
      }
    }

    internal string DisplayPath
    {
      get
      {
        return this._displayPath;
      }
      set
      {
        this._displayPath = value;
      }
    }

    /// <summary>初始化 <see cref="T:System.IO.FileSystemInfo" /> 类的新实例。</summary>
    protected FileSystemInfo()
    {
    }

    /// <summary>使用序列化数据初始化 <see cref="T:System.IO.FileSystemInfo" /> 类的新实例。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" />，它保存关于所引发异常的序列化对象数据。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" />，它包含关于源或目标的上下文信息。</param>
    /// <exception cref="T:System.ArgumentNullException">指定的 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 为空。</exception>
    protected FileSystemInfo(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      this.FullPath = Path.GetFullPathInternal(info.GetString("FullPath"));
      this.OriginalPath = info.GetString("OriginalPath");
      this._dataInitialised = -1;
    }

    [SecurityCritical]
    internal void InitializeFrom(Win32Native.WIN32_FIND_DATA findData)
    {
      this._data = new Win32Native.WIN32_FILE_ATTRIBUTE_DATA();
      this._data.PopulateFrom(findData);
      this._dataInitialised = 0;
    }

    /// <summary>删除文件或目录。</summary>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">指定的路径无效：例如，它位于未映射的驱动器上。</exception>
    /// <exception cref="T:System.IO.IOException">对于文件或目录有打开句柄，并且操作系统是 Windows XP 或更早版本。此打开句柄可能是由于枚举目录和文件导致的。有关详细信息，请参阅如何：枚举目录和文件。</exception>
    /// <filterpriority>2</filterpriority>
    public abstract void Delete();

    /// <summary>刷新对象的状态。</summary>
    /// <exception cref="T:System.IO.IOException">设备（如磁盘驱动器）未准备好。 </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public void Refresh()
    {
      this._dataInitialised = File.FillAttributeInfo(this.FullPath, ref this._data, false, false);
    }

    /// <summary>设置带有文件名和附加异常信息的 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象。</summary>
    /// <param name="info">
    /// <see cref="T:System.Runtime.Serialization.SerializationInfo" />，它保存关于所引发异常的序列化对象数据。</param>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" />，它包含关于源或目标的上下文信息。</param>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
    /// </PermissionSet>
    [SecurityCritical]
    [ComVisible(false)]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.FullPath).Demand();
      info.AddValue("OriginalPath", (object) this.OriginalPath, typeof (string));
      info.AddValue("FullPath", (object) this.FullPath, typeof (string));
    }
  }
}
