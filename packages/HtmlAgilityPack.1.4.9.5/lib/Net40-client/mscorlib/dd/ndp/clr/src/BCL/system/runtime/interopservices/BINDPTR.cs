// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.BINDPTR
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.BINDPTR" />。</summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.BINDPTR instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
  public struct BINDPTR
  {
    /// <summary>表示指向 <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> 结构的指针。</summary>
    [FieldOffset(0)]
    public IntPtr lpfuncdesc;
    /// <summary>表示指向 <see cref="T:System.Runtime.InteropServices.VARDESC" /> 结构的指针。</summary>
    [FieldOffset(0)]
    public IntPtr lpvardesc;
    /// <summary>表示指向 <see cref="F:System.Runtime.InteropServices.BINDPTR.lptcomp" /> 接口的指针。</summary>
    [FieldOffset(0)]
    public IntPtr lptcomp;
  }
}
