// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.FUNCDESC
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.FUNCDESC" />。</summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.FUNCDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  public struct FUNCDESC
  {
    /// <summary>标识函数成员 ID。</summary>
    public int memid;
    /// <summary>存储函数可在 16 位系统中返回的错误的计数。</summary>
    public IntPtr lprgscode;
    /// <summary>指示 <see cref="F:System.Runtime.InteropServices.FUNCDESC.cParams" /> 的大小。</summary>
    public IntPtr lprgelemdescParam;
    /// <summary>指定函数是虚拟的、静态的还是仅支持调度的。</summary>
    public FUNCKIND funckind;
    /// <summary>指定属性函数的类型。</summary>
    public INVOKEKIND invkind;
    /// <summary>指定函数的调用约定。</summary>
    public CALLCONV callconv;
    /// <summary>计算参数的总数。</summary>
    public short cParams;
    /// <summary>计算可选参数。</summary>
    public short cParamsOpt;
    /// <summary>指定 <see cref="F:System.Runtime.InteropServices.FUNCKIND.FUNC_VIRTUAL" /> 在 VTBL 中的偏移量。</summary>
    public short oVft;
    /// <summary>计算允许的返回值。</summary>
    public short cScodes;
    /// <summary>包含函数的返回类型。</summary>
    public ELEMDESC elemdescFunc;
    /// <summary>指示函数的 <see cref="T:System.Runtime.InteropServices.FUNCFLAGS" />。</summary>
    public short wFuncFlags;
  }
}
