// Decompiled with JetBrains decompiler
// Type: System.IO.DriveType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.IO
{
  /// <summary>定义驱动器类型常数，包括 CDRom、Fixed、Network、NoRootDirectory、Ram、Removable 和 Unknown。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public enum DriveType
  {
    Unknown,
    NoRootDirectory,
    Removable,
    Fixed,
    Network,
    CDRom,
    Ram,
  }
}
