// Decompiled with JetBrains decompiler
// Type: System.IO.SearchResult
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Security;

namespace System.IO
{
  internal sealed class SearchResult
  {
    private string fullPath;
    private string userPath;
    [SecurityCritical]
    private Win32Native.WIN32_FIND_DATA findData;

    internal string FullPath
    {
      get
      {
        return this.fullPath;
      }
    }

    internal string UserPath
    {
      get
      {
        return this.userPath;
      }
    }

    internal Win32Native.WIN32_FIND_DATA FindData
    {
      [SecurityCritical] get
      {
        return this.findData;
      }
    }

    [SecurityCritical]
    internal SearchResult(string fullPath, string userPath, Win32Native.WIN32_FIND_DATA findData)
    {
      this.fullPath = fullPath;
      this.userPath = userPath;
      this.findData = findData;
    }
  }
}
