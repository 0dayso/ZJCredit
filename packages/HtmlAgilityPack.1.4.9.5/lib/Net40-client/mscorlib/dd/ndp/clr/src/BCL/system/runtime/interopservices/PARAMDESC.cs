// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.PARAMDESC
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.PARAMDESC" />。</summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.PARAMDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct PARAMDESC
  {
    /// <summary>表示指向正在进程之间传递的值的指针。</summary>
    public IntPtr lpVarValue;
    /// <summary>表示描述结构元素、参数或返回值的位屏蔽值。</summary>
    public PARAMFLAG wParamFlags;
  }
}
