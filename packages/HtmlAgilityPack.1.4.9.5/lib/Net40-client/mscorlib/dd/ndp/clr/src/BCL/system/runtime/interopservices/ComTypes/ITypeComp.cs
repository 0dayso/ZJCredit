// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.ITypeComp
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>提供 ITypeComp 接口的托管定义。</summary>
  [Guid("00020403-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [__DynamicallyInvokable]
  [ComImport]
  public interface ITypeComp
  {
    /// <summary>将名称映射到类型的成员，或者绑定类型库中包含的全局变量和函数。</summary>
    /// <param name="szName">要绑定的名称。</param>
    /// <param name="lHashVal">由 LHashValOfNameSys 计算的 <paramref name="szName" /> 的哈希值。</param>
    /// <param name="wFlags">标志字，包含一个或多个在 INVOKEKIND 枚举中定义的调用标志。</param>
    /// <param name="ppTInfo">此方法返回时，包含对类型说明（包含将其绑定到的项）的引用（如果返回了 FUNCDESC 或 VARDESC）。该参数未经初始化即被传递。</param>
    /// <param name="pDescKind">此方法返回时，包含对 DESCKIND 枚举数的引用，该枚举数指示绑定到的名称是 VARDESC、FUNCDESC 还是 TYPECOMP。该参数未经初始化即被传递。</param>
    /// <param name="pBindPtr">此方法返回时，包含对绑定到的 VARDESC、FUNCDESC 或 ITypeComp 接口的引用。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void Bind([MarshalAs(UnmanagedType.LPWStr)] string szName, int lHashVal, short wFlags, out ITypeInfo ppTInfo, out DESCKIND pDescKind, out BINDPTR pBindPtr);

    /// <summary>绑定到包含在类型库中的类型说明。</summary>
    /// <param name="szName">要绑定的名称。</param>
    /// <param name="lHashVal">由 LHashValOfNameSys 确定的 <paramref name="szName" /> 的哈希值。</param>
    /// <param name="ppTInfo">此方法返回时，包含对将 <paramref name="szName" /> 绑定到的类型的 ITypeInfo 的引用。该参数未经初始化即被传递。</param>
    /// <param name="ppTComp">此方法返回时，包含对 ITypeComp 变量的引用。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void BindType([MarshalAs(UnmanagedType.LPWStr)] string szName, int lHashVal, out ITypeInfo ppTInfo, out ITypeComp ppTComp);
  }
}
