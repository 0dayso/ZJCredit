// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.DISPPARAMS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.DISPPARAMS" />。</summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.DISPPARAMS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct DISPPARAMS
  {
    /// <summary>表示对参数数组的引用。</summary>
    public IntPtr rgvarg;
    /// <summary>表示命名参数的调度 ID。</summary>
    public IntPtr rgdispidNamedArgs;
    /// <summary>表示参数的计数。</summary>
    public int cArgs;
    /// <summary>表示命名参数的计数。</summary>
    public int cNamedArgs;
  }
}
