﻿// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.BINDPTR
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>包含指向绑定到 <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> 结构、<see cref="T:System.Runtime.InteropServices.VARDESC" /> 结构或 ITypeComp 接口的指针。</summary>
  [__DynamicallyInvokable]
  [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
  public struct BINDPTR
  {
    /// <summary>表示指向 <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> 结构的指针。</summary>
    [FieldOffset(0)]
    public IntPtr lpfuncdesc;
    /// <summary>表示指向 <see cref="T:System.Runtime.InteropServices.VARDESC" /> 结构的指针。</summary>
    [FieldOffset(0)]
    public IntPtr lpvardesc;
    /// <summary>表示指向 <see cref="T:System.Runtime.InteropServices.ComTypes.ITypeComp" /> 接口的指针。</summary>
    [FieldOffset(0)]
    public IntPtr lptcomp;
  }
}
