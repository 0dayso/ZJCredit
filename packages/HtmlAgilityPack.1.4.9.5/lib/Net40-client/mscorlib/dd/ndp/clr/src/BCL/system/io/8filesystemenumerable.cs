// Decompiled with JetBrains decompiler
// Type: System.IO.FileSystemEnumerableHelpers
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Security;

namespace System.IO
{
  internal static class FileSystemEnumerableHelpers
  {
    [SecurityCritical]
    internal static bool IsDir(Win32Native.WIN32_FIND_DATA data)
    {
      if ((data.dwFileAttributes & 16) != 0 && !data.cFileName.Equals("."))
        return !data.cFileName.Equals("..");
      return false;
    }

    [SecurityCritical]
    internal static bool IsFile(Win32Native.WIN32_FIND_DATA data)
    {
      return (data.dwFileAttributes & 16) == 0;
    }
  }
}
