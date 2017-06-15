// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.PARAMFLAG
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>描述如何在进程之间传输结构元素、参数或函数返回值。</summary>
  [Flags]
  [__DynamicallyInvokable]
  [Serializable]
  public enum PARAMFLAG : short
  {
    [__DynamicallyInvokable] PARAMFLAG_NONE = 0,
    [__DynamicallyInvokable] PARAMFLAG_FIN = 1,
    [__DynamicallyInvokable] PARAMFLAG_FOUT = 2,
    [__DynamicallyInvokable] PARAMFLAG_FLCID = 4,
    [__DynamicallyInvokable] PARAMFLAG_FRETVAL = 8,
    [__DynamicallyInvokable] PARAMFLAG_FOPT = 16,
    [__DynamicallyInvokable] PARAMFLAG_FHASDEFAULT = 32,
    [__DynamicallyInvokable] PARAMFLAG_FHASCUSTDATA = 64,
  }
}
