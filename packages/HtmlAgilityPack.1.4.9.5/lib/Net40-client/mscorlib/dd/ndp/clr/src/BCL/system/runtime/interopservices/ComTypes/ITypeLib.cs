// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.ITypeLib
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>提供 ITypeLib 接口的托管定义。</summary>
  [Guid("00020402-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [__DynamicallyInvokable]
  [ComImport]
  public interface ITypeLib
  {
    /// <summary>返回类型库中的类型说明的数量。</summary>
    /// <returns>类型库中的类型说明的数量。</returns>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetTypeInfoCount();

    /// <summary>在库中检索指定的类型说明。</summary>
    /// <param name="index">要返回的 ITypeInfo 接口的索引。</param>
    /// <param name="ppTI">此方法返回时，包含一个 ITypeInfo，它描述 <paramref name="index" /> 引用的类型。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void GetTypeInfo(int index, out ITypeInfo ppTI);

    /// <summary>检索类型说明的类型。</summary>
    /// <param name="index">类型库中类型说明的索引。</param>
    /// <param name="pTKind">此方法返回时，包含对用于类型说明的 TYPEKIND 枚举的引用。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void GetTypeInfoType(int index, out TYPEKIND pTKind);

    /// <summary>检索与指定的 GUID 相对应的类型说明。</summary>
    /// <param name="guid">请求其类型信息的类的接口的 IID 或 CLSID。</param>
    /// <param name="ppTInfo">此方法返回时，包含请求的 ITypeInfo 接口。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void GetTypeInfoOfGuid(ref Guid guid, out ITypeInfo ppTInfo);

    /// <summary>检索包含库的特性的结构。</summary>
    /// <param name="ppTLibAttr">此方法返回时，包含一个结构，该结构包含库的特性。该参数未经初始化即被传递。</param>
    void GetLibAttr(out IntPtr ppTLibAttr);

    /// <summary>使客户端编译器能够绑定到库的类型、变量、常数和全局函数。</summary>
    /// <param name="ppTComp">此方法返回时，包含此 ITypeLib 的 ITypeComp 实例的一个实例。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void GetTypeComp(out ITypeComp ppTComp);

    /// <summary>检索库的文档字符串、完整的帮助文件名和路径以及帮助文件中的库帮助主题的上下文标识符。</summary>
    /// <param name="index">要返回其文档的类型说明的索引。</param>
    /// <param name="strName">此方法返回时，包含一个表示指定项的名称的字符串。该参数未经初始化即被传递。</param>
    /// <param name="strDocString">此方法返回时，包含一个表示指定项的文档字符串的字符串。该参数未经初始化即被传递。</param>
    /// <param name="dwHelpContext">此方法返回时，包含与指定项关联的帮助上下文标识符。该参数未经初始化即被传递。</param>
    /// <param name="strHelpFile">此方法返回时，包含一个表示帮助文件的完全限定名的字符串。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

    /// <summary>指示传入的字符串是否包含库中描述的类型或成员的名称。</summary>
    /// <returns>如果在类型库中找到 <paramref name="szNameBuf" />，则为 true；否则为 false。</returns>
    /// <param name="szNameBuf">要测试的字符串。这是一个输入/输出参数。</param>
    /// <param name="lHashVal">
    /// <paramref name="szNameBuf" /> 的哈希值。</param>
    [__DynamicallyInvokable]
    [return: MarshalAs(UnmanagedType.Bool)]
    bool IsName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal);

    /// <summary>在类型库中查找类型说明的匹配项。</summary>
    /// <param name="szNameBuf">要搜索的名称。这是一个输入/输出参数。</param>
    /// <param name="lHashVal">一个用于加快搜索速度的哈希值，由 LHashValOfNameSys 函数计算。如果 <paramref name="lHashVal" /> 为 0，则计算一个值。</param>
    /// <param name="ppTInfo">此方法返回时，包含一个指向类型说明的指针数组，这些类型说明中包含 <paramref name="szNameBuf" /> 中指定的名称。该参数未经初始化即被传递。</param>
    /// <param name="rgMemId">所找到的项的 MEMBERID 数组；<paramref name="rgMemId" />[i] 是由 <paramref name="ppTInfo" />[i] 指定的类型说明中建立索引的 MEMBERID。不能为 null。</param>
    /// <param name="pcFound">在进入时指示要查找的实例数。例如，可以调用 <paramref name="pcFound" />= 1 以查找第一个匹配项。当找到一个实例时停止搜索。在退出时指示找到的实例数。如果 <paramref name="pcFound" /> 的 in 和 out 值完全相同，则可能存在其他包含此名称的类型说明。</param>
    [__DynamicallyInvokable]
    void FindName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal, [MarshalAs(UnmanagedType.LPArray), Out] ITypeInfo[] ppTInfo, [MarshalAs(UnmanagedType.LPArray), Out] int[] rgMemId, ref short pcFound);

    /// <summary>释放最初通过 <see cref="M:System.Runtime.InteropServices.ComTypes.ITypeLib.GetLibAttr(System.IntPtr@)" /> 方法获取的 <see cref="T:System.Runtime.InteropServices.TYPELIBATTR" /> 结构。</summary>
    /// <param name="pTLibAttr">要释放的 TLIBATTR 结构。</param>
    [MethodImpl(MethodImplOptions.PreserveSig)]
    void ReleaseTLibAttr(IntPtr pTLibAttr);
  }
}
