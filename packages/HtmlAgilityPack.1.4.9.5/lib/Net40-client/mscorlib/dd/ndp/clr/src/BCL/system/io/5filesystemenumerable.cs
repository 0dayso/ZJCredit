// Decompiled with JetBrains decompiler
// Type: System.IO.DirectoryInfoResultHandler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Security;
using System.Security.Permissions;

namespace System.IO
{
  internal class DirectoryInfoResultHandler : SearchResultHandler<DirectoryInfo>
  {
    [SecurityCritical]
    internal override bool IsResultIncluded(SearchResult result)
    {
      return FileSystemEnumerableHelpers.IsDir(result.FindData);
    }

    [SecurityCritical]
    internal override DirectoryInfo CreateObject(SearchResult result)
    {
      string fullPath = result.FullPath;
      new FileIOPermission(FileIOPermissionAccess.Read, new string[1]
      {
        fullPath + "\\."
      }, false, false).Demand();
      DirectoryInfo directoryInfo = new DirectoryInfo(fullPath, false);
      Win32Native.WIN32_FIND_DATA findData = result.FindData;
      directoryInfo.InitializeFrom(findData);
      return directoryInfo;
    }
  }
}
