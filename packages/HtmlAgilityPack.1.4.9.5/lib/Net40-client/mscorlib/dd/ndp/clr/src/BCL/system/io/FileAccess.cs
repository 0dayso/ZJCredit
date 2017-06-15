// Decompiled with JetBrains decompiler
// Type: System.IO.FileAccess
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.IO
{
  /// <summary>定义用于文件读取、写入或读取/写入访问权限的常数。</summary>
  /// <filterpriority>2</filterpriority>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum FileAccess
  {
    Read = 1,
    Write = 2,
    ReadWrite = Write | Read,
  }
}
