// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.VARDESC
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>描述变量、常数或数据成员。</summary>
  [__DynamicallyInvokable]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct VARDESC
  {
    /// <summary>指示变量的成员 ID。</summary>
    [__DynamicallyInvokable]
    public int memid;
    /// <summary>保留此字段供将来使用。</summary>
    [__DynamicallyInvokable]
    public string lpstrSchema;
    /// <summary>包含有关变量的信息。</summary>
    [__DynamicallyInvokable]
    public VARDESC.DESCUNION desc;
    /// <summary>包含变量类型。</summary>
    [__DynamicallyInvokable]
    public ELEMDESC elemdescVar;
    /// <summary>定义变量的属性。</summary>
    [__DynamicallyInvokable]
    public short wVarFlags;
    /// <summary>定义如何封送变量。</summary>
    [__DynamicallyInvokable]
    public VARKIND varkind;

    /// <summary>包含有关变量的信息。</summary>
    [__DynamicallyInvokable]
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    public struct DESCUNION
    {
      /// <summary>指示此变量在该实例中的偏移量。</summary>
      [__DynamicallyInvokable]
      [FieldOffset(0)]
      public int oInst;
      /// <summary>描述符号常数。</summary>
      [FieldOffset(0)]
      public IntPtr lpvarValue;
    }
  }
}
