// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ELEMDESC
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.ELEMDESC" />。</summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.ELEMDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct ELEMDESC
  {
    /// <summary>标识元素的类型。</summary>
    public TYPEDESC tdesc;
    /// <summary>包含有关元素的信息。</summary>
    public ELEMDESC.DESCUNION desc;

    /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.ELEMDESC.DESCUNION" />。</summary>
    [ComVisible(false)]
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    public struct DESCUNION
    {
      /// <summary>包含有关远程处理该元素的信息。</summary>
      [FieldOffset(0)]
      public IDLDESC idldesc;
      /// <summary>包含有关参数的信息。</summary>
      [FieldOffset(0)]
      public PARAMDESC paramdesc;
    }
  }
}
