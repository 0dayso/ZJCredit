// Decompiled with JetBrains decompiler
// Type: System.Reflection.PortableExecutableKinds
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>标识可执行文件中代码的特性。</summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum PortableExecutableKinds
  {
    NotAPortableExecutableImage = 0,
    ILOnly = 1,
    Required32Bit = 2,
    PE32Plus = 4,
    Unmanaged32Bit = 8,
    [ComVisible(false)] Preferred32Bit = 16,
  }
}
