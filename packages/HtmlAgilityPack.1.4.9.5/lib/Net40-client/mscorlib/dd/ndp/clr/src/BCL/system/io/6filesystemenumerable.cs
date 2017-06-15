// Decompiled with JetBrains decompiler
// Type: System.IO.FileSystemInfoResultHandler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Security;
using System.Security.Permissions;

namespace System.IO
{
  internal class FileSystemInfoResultHandler : SearchResultHandler<FileSystemInfo>
  {
    [SecurityCritical]
    internal override bool IsResultIncluded(SearchResult result)
    {
      bool flag = FileSystemEnumerableHelpers.IsFile(result.FindData);
      return FileSystemEnumerableHelpers.IsDir(result.FindData) | flag;
    }

    [SecurityCritical]
    internal override FileSystemInfo CreateObject(SearchResult result)
    {
      FileSystemEnumerableHelpers.IsFile(result.FindData);
      if (FileSystemEnumerableHelpers.IsDir(result.FindData))
      {
        string fullPath = result.FullPath;
        new FileIOPermission(FileIOPermissionAccess.Read, new string[1]
        {
          fullPath + "\\."
        }, false, false).Demand();
        DirectoryInfo directoryInfo = new DirectoryInfo(fullPath, false);
        Win32Native.WIN32_FIND_DATA findData = result.FindData;
        directoryInfo.InitializeFrom(findData);
        return (FileSystemInfo) directoryInfo;
      }
      string fullPath1 = result.FullPath;
      new FileIOPermission(FileIOPermissionAccess.Read, new string[1]{ fullPath1 }, false, false).Demand();
      FileInfo fileInfo = new FileInfo(fullPath1, false);
      Win32Native.WIN32_FIND_DATA findData1 = result.FindData;
      fileInfo.InitializeFrom(findData1);
      return (FileSystemInfo) fileInfo;
    }
  }
}
