// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.RegistryValueKind
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
  /// <summary>指定在注册表中存储值时所用的数据类型，或标识注册表中某个值的数据类型。</summary>
  [ComVisible(true)]
  public enum RegistryValueKind
  {
    [ComVisible(false)] None = -1,
    Unknown = 0,
    String = 1,
    ExpandString = 2,
    Binary = 3,
    DWord = 4,
    MultiString = 7,
    QWord = 11,
  }
}
