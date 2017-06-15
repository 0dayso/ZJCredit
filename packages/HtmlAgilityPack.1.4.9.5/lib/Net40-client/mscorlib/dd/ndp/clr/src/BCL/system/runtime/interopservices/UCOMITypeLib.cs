// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UCOMITypeLib
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
  /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.ITypeLib" />。</summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.ITypeLib instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Guid("00020402-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface UCOMITypeLib
  {
    /// <summary>返回类型库中的类型说明的数量。</summary>
    /// <returns>类型库中的类型说明的数量。</returns>
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetTypeInfoCount();

    /// <summary>在库中检索指定的类型说明。</summary>
    /// <param name="index">要返回的 UCOMITypeInfo 接口的索引。</param>
    /// <param name="ppTI">成功返回时，描述被 <paramref name="index" /> 引用的类型的 UCOMITypeInfo。</param>
    void GetTypeInfo(int index, out UCOMITypeInfo ppTI);

    /// <summary>检索类型说明的类型。</summary>
    /// <param name="index">类型库中类型说明的索引。</param>
    /// <param name="pTKind">对此类型说明的 TYPEKIND 枚举的引用。</param>
    void GetTypeInfoType(int index, out TYPEKIND pTKind);

    /// <summary>检索与指定的 GUID 相对应的类型说明。</summary>
    /// <param name="guid">请求其类型信息的类的 CLSID 的接口的 IID。</param>
    /// <param name="ppTInfo">成功返回时所请求的 ITypeInfo 接口。</param>
    void GetTypeInfoOfGuid(ref Guid guid, out UCOMITypeInfo ppTInfo);

    /// <summary>检索包含库的特性的结构。</summary>
    /// <param name="ppTLibAttr">成功返回时包含库的特性的结构。</param>
    void GetLibAttr(out IntPtr ppTLibAttr);

    /// <summary>使客户端编译器能够绑定到库的类型、变量、常数和全局函数。</summary>
    /// <param name="ppTComp">成功返回时，此 ITypeLib 的 UCOMITypeComp 实例的实例。</param>
    void GetTypeComp(out UCOMITypeComp ppTComp);

    /// <summary>检索库的文档字符串、完整的帮助文件名和路径以及帮助文件中的库帮助主题的上下文标识符。</summary>
    /// <param name="index">要返回其文档的类型说明的索引。</param>
    /// <param name="strName">返回包含指定项的名称的字符串。</param>
    /// <param name="strDocString">返回包含指定项的文档字符串的字符串。</param>
    /// <param name="dwHelpContext">返回与指定项关联的帮助上下文标识符。</param>
    /// <param name="strHelpFile">返回包含帮助文件的完全限定名的字符串。</param>
    void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

    /// <summary>指示传入的字符串是否包含库中描述的类型或成员的名称。</summary>
    /// <returns>如果在类型库中找到 <paramref name="szNameBuf" /> 则为 true；否则为 false。</returns>
    /// <param name="szNameBuf">要测试的字符串。</param>
    /// <param name="lHashVal">
    /// <paramref name="szNameBuf" /> 的哈希值。</param>
    [return: MarshalAs(UnmanagedType.Bool)]
    bool IsName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal);

    /// <summary>在类型库中查找类型说明的匹配项。</summary>
    /// <param name="szNameBuf">要搜索的名称。</param>
    /// <param name="lHashVal">一个用于加快搜索速度的哈希值，由 LHashValOfNameSys 函数计算。如果 <paramref name="lHashVal" /> 为 0，则计算一个值。</param>
    /// <param name="ppTInfo">成功返回时，指向包含在 <paramref name="szNameBuf" /> 中指定的名称的类型说明的指针的数组。</param>
    /// <param name="rgMemId">所找到的项的 MEMBERID 数组；<paramref name="rgMemId" />[i] 是由 <paramref name="ppTInfo" />[i] 指定的类型说明中建立索引的 MEMBERID。不能为 null。</param>
    /// <param name="pcFound">在进入时指示要查找的实例数。例如，可以调用 <paramref name="pcFound" />= 1 以查找第一个匹配项。当找到一个实例时停止搜索。在退出时指示找到的实例数。如果 <paramref name="pcFound" /> 的 in 和 out 值完全相同，则可能存在其他包含此名称的类型说明。</param>
    void FindName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal, [MarshalAs(UnmanagedType.LPArray), Out] UCOMITypeInfo[] ppTInfo, [MarshalAs(UnmanagedType.LPArray), Out] int[] rgMemId, ref short pcFound);

    /// <summary>释放最初从 <see cref="M:System.Runtime.InteropServices.UCOMITypeLib.GetLibAttr(System.IntPtr@)" /> 获取的 <see cref="T:System.Runtime.InteropServices.TYPELIBATTR" />。</summary>
    /// <param name="pTLibAttr">要释放的 TLIBATTR。</param>
    [MethodImpl(MethodImplOptions.PreserveSig)]
    void ReleaseTLibAttr(IntPtr pTLibAttr);
  }
}
