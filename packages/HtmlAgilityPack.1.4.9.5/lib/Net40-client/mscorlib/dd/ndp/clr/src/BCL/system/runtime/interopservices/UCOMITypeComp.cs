// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UCOMITypeComp
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.ITypeComp" />。</summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.ITypeComp instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Guid("00020403-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface UCOMITypeComp
  {
    /// <summary>将名称映射到类型的成员，或者绑定类型库中包含的全局变量和函数。</summary>
    /// <param name="szName">要绑定的名称。</param>
    /// <param name="lHashVal">由 LHashValOfNameSys 计算的 <paramref name="szName" /> 的哈希值。</param>
    /// <param name="wFlags">标志字，包含一个或多个在 INVOKEKIND 枚举中定义的调用标志。</param>
    /// <param name="ppTInfo">成功返回时，如果返回了 FUNCDESC 或 VARDESC，则是对类型说明（它包含将其绑定到的项）的引用。</param>
    /// <param name="pDescKind">对 DESCKIND 枚举数的引用，该枚举数指示绑定到的名称是 VARDESC、FUNCDESC 还是 TYPECOMP。</param>
    /// <param name="pBindPtr">对绑定到的 VARDESC、FUNCDESC 或 ITypeComp 接口的引用。</param>
    void Bind([MarshalAs(UnmanagedType.LPWStr)] string szName, int lHashVal, short wFlags, out UCOMITypeInfo ppTInfo, out DESCKIND pDescKind, out BINDPTR pBindPtr);

    /// <summary>绑定到包含在类型库中的类型说明。</summary>
    /// <param name="szName">要绑定的名称。</param>
    /// <param name="lHashVal">由 LHashValOfNameSys 确定的 <paramref name="szName" /> 的哈希值。</param>
    /// <param name="ppTInfo">成功返回时，对将 <paramref name="szName" /> 绑定到的类型的 ITypeInfo 的引用。</param>
    /// <param name="ppTComp">成功返回时，对 ITypeComp 变量的引用。</param>
    void BindType([MarshalAs(UnmanagedType.LPWStr)] string szName, int lHashVal, out UCOMITypeInfo ppTInfo, out UCOMITypeComp ppTComp);
  }
}
