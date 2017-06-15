// Decompiled with JetBrains decompiler
// Type: System.PlatformID
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>标识程序集所支持的操作系统（或平台）。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public enum PlatformID
  {
    Win32S,
    Win32Windows,
    Win32NT,
    WinCE,
    Unix,
    Xbox,
    MacOSX,
  }
}
