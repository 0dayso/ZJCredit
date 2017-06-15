// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.IDLFLAG
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>描述如何在进程之间传输结构元素、参数或函数返回值。</summary>
  [Flags]
  [__DynamicallyInvokable]
  [Serializable]
  public enum IDLFLAG : short
  {
    [__DynamicallyInvokable] IDLFLAG_NONE = 0,
    [__DynamicallyInvokable] IDLFLAG_FIN = 1,
    [__DynamicallyInvokable] IDLFLAG_FOUT = 2,
    [__DynamicallyInvokable] IDLFLAG_FLCID = 4,
    [__DynamicallyInvokable] IDLFLAG_FRETVAL = 8,
  }
}
