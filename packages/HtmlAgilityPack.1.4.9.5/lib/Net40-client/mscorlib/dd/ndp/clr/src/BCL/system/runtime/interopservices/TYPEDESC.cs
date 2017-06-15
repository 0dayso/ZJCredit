// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TYPEDESC
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.TYPEDESC" />。</summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.TYPEDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct TYPEDESC
  {
    /// <summary>如果变量为 VT_SAFEARRAY 或 VT_PTR，则 <see cref="F:System.Runtime.InteropServices.TYPEDESC.lpValue" /> 字段包含指向指定元素类型的 TYPEDESC 的指针。</summary>
    public IntPtr lpValue;
    /// <summary>指示由此 TYPEDESC 描述的项的 Variant 类型。</summary>
    public short vt;
  }
}
