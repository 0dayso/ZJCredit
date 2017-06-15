// Decompiled with JetBrains decompiler
// Type: System.IO.FileShare
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.IO
{
  /// <summary>包含用于控制其他 <see cref="T:System.IO.FileStream" /> 对象对同一文件可以具有的访问类型的常数。</summary>
  /// <filterpriority>2</filterpriority>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum FileShare
  {
    None = 0,
    Read = 1,
    Write = 2,
    ReadWrite = Write | Read,
    Delete = 4,
    Inheritable = 16,
  }
}
