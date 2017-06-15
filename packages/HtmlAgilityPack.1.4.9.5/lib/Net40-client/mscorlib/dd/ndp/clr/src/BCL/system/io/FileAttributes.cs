// Decompiled with JetBrains decompiler
// Type: System.IO.FileAttributes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.IO
{
  /// <summary>提供文件和目录的属性。</summary>
  /// <filterpriority>2</filterpriority>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum FileAttributes
  {
    ReadOnly = 1,
    Hidden = 2,
    System = 4,
    Directory = 16,
    Archive = 32,
    Device = 64,
    Normal = 128,
    Temporary = 256,
    SparseFile = 512,
    ReparsePoint = 1024,
    Compressed = 2048,
    Offline = 4096,
    NotContentIndexed = 8192,
    Encrypted = 16384,
    [ComVisible(false)] IntegrityStream = 32768,
    [ComVisible(false)] NoScrubData = 131072,
  }
}
