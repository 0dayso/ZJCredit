// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.PARAMDESC
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>包含有关如何在进程之间传输结构元素、参数或函数返回值的信息。</summary>
  [__DynamicallyInvokable]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct PARAMDESC
  {
    /// <summary>表示指向正在进程之间传递的值的指针。</summary>
    public IntPtr lpVarValue;
    /// <summary>表示描述结构元素、参数或返回值的位屏蔽值。</summary>
    [__DynamicallyInvokable]
    public PARAMFLAG wParamFlags;
  }
}
