// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.ITypeInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>提供组件自动化 ITypeInfo 接口的托管定义。</summary>
  [Guid("00020401-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [__DynamicallyInvokable]
  [ComImport]
  public interface ITypeInfo
  {
    /// <summary>检索包含类型说明的特性的 <see cref="T:System.Runtime.InteropServices.TYPEATTR" /> 结构。</summary>
    /// <param name="ppTypeAttr">此方法返回时，包含对包含此类型说明的特性的结构的引用。该参数未经初始化即被传递。</param>
    void GetTypeAttr(out IntPtr ppTypeAttr);

    /// <summary>检索类型说明的 ITypeComp 接口，此接口使客户端编译器可以绑定到类型说明的成员。</summary>
    /// <param name="ppTComp">此方法返回时，包含对包含类型库的 ITypeComp 接口的引用。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void GetTypeComp(out ITypeComp ppTComp);

    /// <summary>检索包含有关指定函数的信息的 <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> 结构。</summary>
    /// <param name="index">要返回的函数说明的索引。</param>
    /// <param name="ppFuncDesc">此方法返回时，包含对描述指定函数的 FUNCDESC 结构的引用。该参数未经初始化即被传递。</param>
    void GetFuncDesc(int index, out IntPtr ppFuncDesc);

    /// <summary>检索描述指定变量的 VARDESC 结构。</summary>
    /// <param name="index">要返回的变量说明的索引。</param>
    /// <param name="ppVarDesc">此方法返回时，包含对描述指定变量的 VARDESC 结构的引用。该参数未经初始化即被传递。</param>
    void GetVarDesc(int index, out IntPtr ppVarDesc);

    /// <summary>检索具有与指定函数 ID 相对应的指定成员 ID（或者属性或方法的名称及其参数）的变量。</summary>
    /// <param name="memid">要返回其名称的成员的 ID。</param>
    /// <param name="rgBstrNames">此方法返回时，包含与成员相关联的名称。该参数未经初始化即被传递。</param>
    /// <param name="cMaxNames">
    /// <paramref name="rgBstrNames" /> 数组的长度。</param>
    /// <param name="pcNames">此方法返回时，包含 <paramref name="rgBstrNames" /> 数组中的名称数。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void GetNames(int memid, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2), Out] string[] rgBstrNames, int cMaxNames, out int pcNames);

    /// <summary>检索实现的接口类型的类型说明（如果类型说明描述 COM 类）。</summary>
    /// <param name="index">返回其句柄的已实现类型的索引。</param>
    /// <param name="href">此方法返回时，包含对已实现接口的句柄的引用。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void GetRefTypeOfImplType(int index, out int href);

    /// <summary>在类型说明中检索某个已实现的接口或基接口的 <see cref="T:System.Runtime.InteropServices.IMPLTYPEFLAGS" /> 值。</summary>
    /// <param name="index">已实现的接口或基接口的索引。</param>
    /// <param name="pImplTypeFlags">此方法返回时，包含对 IMPLTYPEFLAGS 枚举的引用。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void GetImplTypeFlags(int index, out IMPLTYPEFLAGS pImplTypeFlags);

    /// <summary>在成员名和成员 ID 之间以及参数名和参数 ID 之间映射。</summary>
    /// <param name="rgszNames">要映射的名称数组。</param>
    /// <param name="cNames">要映射的名称计数。</param>
    /// <param name="pMemId">此方法返回时，包含对在其中放置名称映射的数组的引用。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void GetIDsOfNames([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1, ArraySubType = UnmanagedType.LPWStr), In] string[] rgszNames, int cNames, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1), Out] int[] pMemId);

    /// <summary>调用对象的方法或访问对象的属性，该方法或属性实现由类型说明描述的接口。</summary>
    /// <param name="pvInstance">对由此类型说明描述的接口的引用。</param>
    /// <param name="memid">用于标识接口成员的值。</param>
    /// <param name="wFlags">描述 Invoke 调用的上下文的标志。</param>
    /// <param name="pDispParams">对结构的引用，该结构包含一个参数数组、一个命名参数的 DISPID 数组和每个数组中元素数的计数。</param>
    /// <param name="pVarResult">对用于存储结果的位置的引用。如果 <paramref name="wFlags" /> 指定 DISPATCH_PROPERTYPUT 或 DISPATCH_PROPERTYPUTREF，则忽略 <paramref name="pVarResult" />。如果不需要任何结果，则设置为 null。</param>
    /// <param name="pExcepInfo">指向异常信息结构的指针，该结构仅在返回 DISP_E_EXCEPTION 时才被填充。</param>
    /// <param name="puArgErr">如果 Invoke 返回 DISP_E_TYPEMISMATCH，则 <paramref name="puArgErr" /> 指示具有错误类型的参数的 <paramref name="rgvarg" /> 中的索引。如果多个参数返回错误，则 <paramref name="puArgErr" /> 仅指示第一个具有错误的参数。该参数未经初始化即被传递。</param>
    void Invoke([MarshalAs(UnmanagedType.IUnknown)] object pvInstance, int memid, short wFlags, ref DISPPARAMS pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, out int puArgErr);

    /// <summary>从文档字符串、完整的帮助文件名和路径以及帮助主题的上下文 ID 中检索指定的类型说明。</summary>
    /// <param name="index">要返回其文档的成员的 ID。</param>
    /// <param name="strName">此方法返回时，包含项方法的名称。该参数未经初始化即被传递。</param>
    /// <param name="strDocString">此方法返回时，包含指定项的文档字符串。该参数未经初始化即被传递。</param>
    /// <param name="dwHelpContext">此方法返回时，包含对与指定项相关联的帮助上下文的引用。该参数未经初始化即被传递。</param>
    /// <param name="strHelpFile">此方法返回时，包含帮助文件的完全限定名。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

    /// <summary>检索 DLL 中函数的入口点的说明或规范。</summary>
    /// <param name="memid">成员函数的 ID，要返回该成员函数的 DLL 入口说明。</param>
    /// <param name="invKind">指定由 <paramref name="memid" /> 标识的成员种类的 <see cref="T:System.Runtime.InteropServices.ComTypes.INVOKEKIND" /> 值之一。</param>
    /// <param name="pBstrDllName">如果不为 null，则此函数将 <paramref name="pBstrDllName" /> 设置为包含 DLL 名称的 BSTR。</param>
    /// <param name="pBstrName">如果不为 null，则此函数将 <paramref name="lpbstrName" /> 设置为包含入口点名称的 BSTR。</param>
    /// <param name="pwOrdinal">如果不为 null，并且此函数是按序号定义的，则 <paramref name="lpwOrdinal" /> 被设置为指向该序号。</param>
    void GetDllEntry(int memid, INVOKEKIND invKind, IntPtr pBstrDllName, IntPtr pBstrName, IntPtr pwOrdinal);

    /// <summary>检索被引用的类型说明（如果该类型说明引用其他类型说明）。</summary>
    /// <param name="hRef">要返回的被引用类型说明的句柄。</param>
    /// <param name="ppTI">此方法返回时，包含被引用的类型说明。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void GetRefTypeInfo(int hRef, out ITypeInfo ppTI);

    /// <summary>检索静态函数或变量（如那些在 DLL 中定义的静态函数或变量）的地址。</summary>
    /// <param name="memid">要检索的 static 成员地址的成员 ID。</param>
    /// <param name="invKind">指定该成员是否为属性（如果是，还将指定它属于哪种属性）的 <see cref="T:System.Runtime.InteropServices.ComTypes.INVOKEKIND" /> 值之一。</param>
    /// <param name="ppv">此方法返回时，包含对 static 成员的引用。该参数未经初始化即被传递。</param>
    void AddressOfMember(int memid, INVOKEKIND invKind, out IntPtr ppv);

    /// <summary>创建描述组件类 (coclass) 的类型的新实例。</summary>
    /// <param name="pUnkOuter">作为控制 IUnknown 的对象。</param>
    /// <param name="riid">接口的 IID，调用方将使用该接口与结果对象进行通信。</param>
    /// <param name="ppvObj">此方法返回时，包含对已创建对象的引用。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void CreateInstance([MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In] ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvObj);

    /// <summary>检索封送处理信息。</summary>
    /// <param name="memid">成员 ID，它指示需要哪些封送处理信息。</param>
    /// <param name="pBstrMops">此方法返回时，包含对 opcode 字符串的引用，该字符串用于封送处理由引用类型说明描述的结构的字段；如果没有要返回的信息，则返回 null。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void GetMops(int memid, out string pBstrMops);

    /// <summary>检索类型库，该类型库包含此类型说明和它在该类型库中的索引。</summary>
    /// <param name="ppTLB">此方法返回时，包含对包含类型库的引用。该参数未经初始化即被传递。</param>
    /// <param name="pIndex">此方法返回时，包含对包含类型库中的类型说明的索引的引用。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void GetContainingTypeLib(out ITypeLib ppTLB, out int pIndex);

    /// <summary>释放先前由 <see cref="M:System.Runtime.InteropServices.ComTypes.ITypeInfo.GetTypeAttr(System.IntPtr@)" /> 方法返回的一个 <see cref="T:System.Runtime.InteropServices.TYPEATTR" /> 结构。</summary>
    /// <param name="pTypeAttr">对要释放的 TYPEATTR 结构的引用。</param>
    [MethodImpl(MethodImplOptions.PreserveSig)]
    void ReleaseTypeAttr(IntPtr pTypeAttr);

    /// <summary>释放先前由 <see cref="M:System.Runtime.InteropServices.ComTypes.ITypeInfo.GetFuncDesc(System.Int32,System.IntPtr@)" /> 方法返回的一个 <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> 结构。</summary>
    /// <param name="pFuncDesc">对要释放的 FUNCDESC 结构的引用。</param>
    [MethodImpl(MethodImplOptions.PreserveSig)]
    void ReleaseFuncDesc(IntPtr pFuncDesc);

    /// <summary>释放先前由 <see cref="M:System.Runtime.InteropServices.ComTypes.ITypeInfo.GetVarDesc(System.Int32,System.IntPtr@)" /> 方法返回的一个 VARDESC 结构。</summary>
    /// <param name="pVarDesc">对要释放的 VARDESC 结构的引用。</param>
    [MethodImpl(MethodImplOptions.PreserveSig)]
    void ReleaseVarDesc(IntPtr pVarDesc);
  }
}
