// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.IDLDESC
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.IDLDESC" />。</summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.IDLDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct IDLDESC
  {
    /// <summary>保留；设置为 null。</summary>
    public int dwReserved;
    /// <summary>指示描述类型的 <see cref="T:System.Runtime.InteropServices.IDLFLAG" /> 值。</summary>
    public IDLFLAG wIDLFlags;
  }
}
