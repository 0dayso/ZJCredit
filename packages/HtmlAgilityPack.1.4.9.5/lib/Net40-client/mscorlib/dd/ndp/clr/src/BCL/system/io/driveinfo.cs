// Decompiled with JetBrains decompiler
// Type: System.IO.DriveInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.IO
{
  /// <summary>提供对有关驱动器的信息的访问。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public sealed class DriveInfo : ISerializable
  {
    private string _name;
    private const string NameField = "_name";

    /// <summary>获取驱动器的名称，如 C:\。</summary>
    /// <returns>驱动器的名称。</returns>
    /// <filterpriority>1</filterpriority>
    public string Name
    {
      get
      {
        return this._name;
      }
    }

    /// <summary>获取驱动器类型，如 CD-ROM、可移动、网络或固定。</summary>
    /// <returns>指定驱动器类型的枚举值之一。</returns>
    /// <filterpriority>1</filterpriority>
    public DriveType DriveType
    {
      [SecuritySafeCritical] get
      {
        return (DriveType) Win32Native.GetDriveType(this.Name);
      }
    }

    /// <summary>获取文件系统的名称，例如 NTFS 或 FAT32。</summary>
    /// <returns>指定驱动器上文件系统的名称。</returns>
    /// <exception cref="T:System.UnauthorizedAccessException">拒绝访问驱动器信息。</exception>
    /// <exception cref="T:System.IO.DriveNotFoundException">该驱动器不存在或未映射。</exception>
    /// <exception cref="T:System.IO.IOException">发生了 I/O 错误（例如，磁盘错误或驱动器未准备好）。</exception>
    /// <filterpriority>1</filterpriority>
    public string DriveFormat
    {
      [SecuritySafeCritical] get
      {
        StringBuilder volumeName = new StringBuilder(50);
        StringBuilder fileSystemName = new StringBuilder(50);
        int newMode = Win32Native.SetErrorMode(1);
        try
        {
          int volSerialNumber;
          int maxFileNameLen;
          int fileSystemFlags;
          if (!Win32Native.GetVolumeInformation(this.Name, volumeName, 50, out volSerialNumber, out maxFileNameLen, out fileSystemFlags, fileSystemName, 50))
            __Error.WinIODriveError(this.Name, Marshal.GetLastWin32Error());
        }
        finally
        {
          Win32Native.SetErrorMode(newMode);
        }
        return fileSystemName.ToString();
      }
    }

    /// <summary>获取一个指示驱动器是否已准备好的值。</summary>
    /// <returns>如果驱动器已准备好，则为 true；如果驱动器未准备好，则为 false。</returns>
    /// <filterpriority>1</filterpriority>
    public bool IsReady
    {
      [SecuritySafeCritical] get
      {
        return Directory.InternalExists(this.Name);
      }
    }

    /// <summary>指示驱动器上的可用空闲空间总量（以字节为单位）。</summary>
    /// <returns>驱动器上的可用空闲空间量（以字节为单位）。</returns>
    /// <exception cref="T:System.UnauthorizedAccessException">拒绝访问驱动器信息。</exception>
    /// <exception cref="T:System.IO.IOException">发生了 I/O 错误（例如，磁盘错误或驱动器未准备好）。</exception>
    /// <filterpriority>1</filterpriority>
    public long AvailableFreeSpace
    {
      [SecuritySafeCritical] get
      {
        int newMode = Win32Native.SetErrorMode(1);
        long freeBytesForUser;
        try
        {
          long totalBytes;
          long freeBytes;
          if (!Win32Native.GetDiskFreeSpaceEx(this.Name, out freeBytesForUser, out totalBytes, out freeBytes))
            __Error.WinIODriveError(this.Name);
        }
        finally
        {
          Win32Native.SetErrorMode(newMode);
        }
        return freeBytesForUser;
      }
    }

    /// <summary>获取驱动器上的可用空闲空间总量（以字节为单位）。</summary>
    /// <returns>驱动器上的可用空闲空间总量（以字节为单位）。</returns>
    /// <exception cref="T:System.UnauthorizedAccessException">拒绝访问驱动器信息。</exception>
    /// <exception cref="T:System.IO.DriveNotFoundException">该驱动器未映射或不存在。</exception>
    /// <exception cref="T:System.IO.IOException">发生了 I/O 错误（例如，磁盘错误或驱动器未准备好）。</exception>
    /// <filterpriority>1</filterpriority>
    public long TotalFreeSpace
    {
      [SecuritySafeCritical] get
      {
        int newMode = Win32Native.SetErrorMode(1);
        long freeBytes;
        try
        {
          long freeBytesForUser;
          long totalBytes;
          if (!Win32Native.GetDiskFreeSpaceEx(this.Name, out freeBytesForUser, out totalBytes, out freeBytes))
            __Error.WinIODriveError(this.Name);
        }
        finally
        {
          Win32Native.SetErrorMode(newMode);
        }
        return freeBytes;
      }
    }

    /// <summary>获取驱动器上存储空间的总大小（以字节为单位）。</summary>
    /// <returns>驱动器的总大小（以字节为单位）。</returns>
    /// <exception cref="T:System.UnauthorizedAccessException">拒绝访问驱动器信息。</exception>
    /// <exception cref="T:System.IO.DriveNotFoundException">该驱动器未映射或不存在。</exception>
    /// <exception cref="T:System.IO.IOException">发生了 I/O 错误（例如，磁盘错误或驱动器未准备好）。</exception>
    /// <filterpriority>1</filterpriority>
    public long TotalSize
    {
      [SecuritySafeCritical] get
      {
        int newMode = Win32Native.SetErrorMode(1);
        long totalBytes;
        try
        {
          long freeBytesForUser;
          long freeBytes;
          if (!Win32Native.GetDiskFreeSpaceEx(this.Name, out freeBytesForUser, out totalBytes, out freeBytes))
            __Error.WinIODriveError(this.Name);
        }
        finally
        {
          Win32Native.SetErrorMode(newMode);
        }
        return totalBytes;
      }
    }

    /// <summary>获取驱动器的根目录。</summary>
    /// <returns>包含驱动器根目录的对象。</returns>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public DirectoryInfo RootDirectory
    {
      get
      {
        return new DirectoryInfo(this.Name);
      }
    }

    /// <summary>获取或设置驱动器的卷标。</summary>
    /// <returns>卷标。</returns>
    /// <exception cref="T:System.IO.IOException">发生了 I/O 错误（例如，磁盘错误或驱动器未准备好）。</exception>
    /// <exception cref="T:System.IO.DriveNotFoundException">该驱动器未映射或不存在。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">正在网络或 CD-ROM 驱动器上设置卷标。- 或 -拒绝访问驱动器信息。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public string VolumeLabel
    {
      [SecuritySafeCritical] get
      {
        StringBuilder volumeName = new StringBuilder(50);
        StringBuilder fileSystemName = new StringBuilder(50);
        int newMode = Win32Native.SetErrorMode(1);
        try
        {
          int volSerialNumber;
          int maxFileNameLen;
          int fileSystemFlags;
          if (!Win32Native.GetVolumeInformation(this.Name, volumeName, 50, out volSerialNumber, out maxFileNameLen, out fileSystemFlags, fileSystemName, 50))
          {
            int errorCode = Marshal.GetLastWin32Error();
            if (errorCode == 13)
              errorCode = 15;
            __Error.WinIODriveError(this.Name, errorCode);
          }
        }
        finally
        {
          Win32Native.SetErrorMode(newMode);
        }
        return volumeName.ToString();
      }
      [SecuritySafeCritical] set
      {
        new FileIOPermission(FileIOPermissionAccess.Write, this._name + ".").Demand();
        int newMode = Win32Native.SetErrorMode(1);
        try
        {
          if (Win32Native.SetVolumeLabel(this.Name, value))
            return;
          int lastWin32Error = Marshal.GetLastWin32Error();
          if (lastWin32Error == 5)
            throw new UnauthorizedAccessException(Environment.GetResourceString("InvalidOperation_SetVolumeLabelFailed"));
          __Error.WinIODriveError(this.Name, lastWin32Error);
        }
        finally
        {
          Win32Native.SetErrorMode(newMode);
        }
      }
    }

    /// <summary>提供对有关指定驱动器的信息的访问。</summary>
    /// <param name="driveName">有效驱动器路径或驱动器号。它可以是从“a”到“z”的大写或小写字母。Null 值无效。</param>
    /// <exception cref="T:System.ArgumentNullException">驱动器号不能为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="driveName" /> 的第一个字母不能是从“a”到“z”的大写或小写字母。- 或 -<paramref name="driveName" /> 未引用有效的驱动器。</exception>
    [SecuritySafeCritical]
    public DriveInfo(string driveName)
    {
      if (driveName == null)
        throw new ArgumentNullException("driveName");
      if (driveName.Length == 1)
      {
        this._name = driveName + ":\\";
      }
      else
      {
        Path.CheckInvalidPathChars(driveName, false);
        this._name = Path.GetPathRoot(driveName);
        if (this._name == null || this._name.Length == 0 || this._name.StartsWith("\\\\", StringComparison.Ordinal))
          throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDriveLetterOrRootDir"));
      }
      if (this._name.Length == 2 && (int) this._name[1] == 58)
        this._name = this._name + "\\";
      char ch = driveName[0];
      if (((int) ch < 65 || (int) ch > 90) && ((int) ch < 97 || (int) ch > 122))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDriveLetterOrRootDir"));
      new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this._name + ".").Demand();
    }

    [SecurityCritical]
    private DriveInfo(SerializationInfo info, StreamingContext context)
    {
      this._name = (string) info.GetValue("_name", typeof (string));
      new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this._name + ".").Demand();
    }

    /// <summary>检索计算机上的所有逻辑驱动器的驱动器名称。</summary>
    /// <returns>
    /// <see cref="T:System.IO.DriveInfo" /> 类型的数组，表示计算机上的逻辑驱动器。</returns>
    /// <exception cref="T:System.IO.IOException">发生了 I/O 错误（例如，磁盘错误或驱动器未准备好）。</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">调用方没有所要求的权限。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static DriveInfo[] GetDrives()
    {
      string[] logicalDrives = Directory.GetLogicalDrives();
      DriveInfo[] driveInfoArray = new DriveInfo[logicalDrives.Length];
      for (int index = 0; index < logicalDrives.Length; ++index)
        driveInfoArray[index] = new DriveInfo(logicalDrives[index]);
      return driveInfoArray;
    }

    /// <summary>将驱动器名称作为字符串返回。</summary>
    /// <returns>驱动器的名称。</returns>
    /// <filterpriority>1</filterpriority>
    public override string ToString()
    {
      return this.Name;
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info.AddValue("_name", (object) this._name, typeof (string));
    }
  }
}
