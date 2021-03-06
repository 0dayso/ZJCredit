﻿// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.VARDESC
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.VARDESC" />。</summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.VARDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct VARDESC
  {
    /// <summary>指示变量的成员 ID。</summary>
    public int memid;
    /// <summary>保留此字段供将来使用。</summary>
    public string lpstrSchema;
    /// <summary>包含变量类型。</summary>
    public ELEMDESC elemdescVar;
    /// <summary>定义变量的属性。</summary>
    public short wVarFlags;
    /// <summary>定义应如何对变量进行封送处理。</summary>
    public VarEnum varkind;

    /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.VARDESC.DESCUNION" />。</summary>
    [ComVisible(false)]
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    public struct DESCUNION
    {
      /// <summary>指示此变量在该实例中的偏移量。</summary>
      [FieldOffset(0)]
      public int oInst;
      /// <summary>描述符号常数。</summary>
      [FieldOffset(0)]
      public IntPtr lpvarValue;
    }
  }
}
