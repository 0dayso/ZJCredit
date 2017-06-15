// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.ISymbolWriter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>表示托管代码的符号编写器。</summary>
  [ComVisible(true)]
  public interface ISymbolWriter
  {
    /// <summary>设置元数据发射器接口以便与编写器关联。</summary>
    /// <param name="emitter">元数据发射器接口。</param>
    /// <param name="filename">向其中写入调试符号的文件名。某些编写器要求文件名，而其他编写器却不要求。如果为不使用文件名的编写器指定文件名，则忽略此参数。</param>
    /// <param name="fFullBuild">true 指示这是完全重新生成的；false 指示这是增量编译。</param>
    void Initialize(IntPtr emitter, string filename, bool fFullBuild);

    /// <summary>定义源文档。</summary>
    /// <returns>表示文档的对象。</returns>
    /// <param name="url">标识文档的 URL。</param>
    /// <param name="language">文档语言。此参数可以为 <see cref="F:System.Guid.Empty" />。</param>
    /// <param name="languageVendor">文档语言的供应商标识。此参数可以为 <see cref="F:System.Guid.Empty" />。</param>
    /// <param name="documentType">文档的类型。此参数可以为 <see cref="F:System.Guid.Empty" />。</param>
    ISymbolDocumentWriter DefineDocument(string url, Guid language, Guid languageVendor, Guid documentType);

    /// <summary>将用户定义的方法标识为当前模块的入口点。</summary>
    /// <param name="entryMethod">方法的元数据标记，它是用户入口点。</param>
    void SetUserEntryPoint(SymbolToken entryMethod);

    /// <summary>打开要向其中放入符号信息的方法。</summary>
    /// <param name="method">要打开的方法的元数据标记。</param>
    void OpenMethod(SymbolToken method);

    /// <summary>关闭当前方法。</summary>
    void CloseMethod();

    /// <summary>在当前方法内定义一组序列点。</summary>
    /// <param name="document">正在为其定义序列点的文档对象。</param>
    /// <param name="offsets">从方法开始测量的序列点偏移量。</param>
    /// <param name="lines">序列点的文档行。</param>
    /// <param name="columns">序列点的文档位置。</param>
    /// <param name="endLines">序列点的文档结束行。</param>
    /// <param name="endColumns">序列点的文档结束位置。</param>
    void DefineSequencePoints(ISymbolDocumentWriter document, int[] offsets, int[] lines, int[] columns, int[] endLines, int[] endColumns);

    /// <summary>在当前方法中打开新的词法范围。</summary>
    /// <returns>一个不透明的范围标识符，它以后可以与 <see cref="M:System.Diagnostics.SymbolStore.ISymbolWriter.SetScopeRange(System.Int32,System.Int32,System.Int32)" /> 一起使用，以定义范围的起始和结束偏移量。在这种情况下，忽略传递到 <see cref="M:System.Diagnostics.SymbolStore.ISymbolWriter.OpenScope(System.Int32)" /> 和 <see cref="M:System.Diagnostics.SymbolStore.ISymbolWriter.CloseScope(System.Int32)" /> 的偏移量。范围标识符只在当前方法中有效。</returns>
    /// <param name="startOffset">从方法的开始处到词法范围内第一条指令的偏移量，以字节数表示。</param>
    int OpenScope(int startOffset);

    /// <summary>关闭当前词法范围。</summary>
    /// <param name="endOffset">越过范围中最后一个指令的点。</param>
    void CloseScope(int endOffset);

    /// <summary>定义指定词法范围的偏移量范围。</summary>
    /// <param name="scopeID">词法范围的标识符。</param>
    /// <param name="startOffset">词法范围的开始的字节偏移量。</param>
    /// <param name="endOffset">词法范围的结尾的字节偏移量。</param>
    void SetScopeRange(int scopeID, int startOffset, int endOffset);

    /// <summary>在当前词法范围内定义单个变量。</summary>
    /// <param name="name">局部变量名称。</param>
    /// <param name="attributes">局部变量特性的按位组合。</param>
    /// <param name="signature">局部变量签名。</param>
    /// <param name="addrKind">
    /// <paramref name="addr1" />、<paramref name="addr2" /> 和 <paramref name="addr3" /> 的地址类型。</param>
    /// <param name="addr1">局部变量规格的第一个地址。</param>
    /// <param name="addr2">局部变量规格的第二个地址。</param>
    /// <param name="addr3">局部变量规格的第三个地址。</param>
    /// <param name="startOffset">变量的起始偏移量。如果此参数为零，则忽略此参数，并在整个范围内定义该变量。如果此参数为非 0，则该变量将位于当前范围的偏移量之内。</param>
    /// <param name="endOffset">变量的结束偏移量。如果此参数为零，则忽略此参数，并在整个范围内定义该变量。如果此参数为非 0，则该变量将位于当前范围的偏移量之内。</param>
    void DefineLocalVariable(string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3, int startOffset, int endOffset);

    /// <summary>在当前方法中定义单个参数。每个参数的类型从它在方法的签名内的位置获取。</summary>
    /// <param name="name">参数名。</param>
    /// <param name="attributes">参数特性的按位组合。</param>
    /// <param name="sequence">参数签名。</param>
    /// <param name="addrKind">
    /// <paramref name="addr1" />、<paramref name="addr2" /> 和 <paramref name="addr3" /> 的地址类型。</param>
    /// <param name="addr1">参数规格的第一个地址。</param>
    /// <param name="addr2">参数规格的第二个地址。</param>
    /// <param name="addr3">参数规格的第三个地址。</param>
    void DefineParameter(string name, ParameterAttributes attributes, int sequence, SymAddressKind addrKind, int addr1, int addr2, int addr3);

    /// <summary>在类型或全局字段中定义字段。</summary>
    /// <param name="parent">元数据类型或方法标记。</param>
    /// <param name="name">字段名。</param>
    /// <param name="attributes">字段特性的按位组合。</param>
    /// <param name="signature">字段签名。</param>
    /// <param name="addrKind">
    /// <paramref name="addr1" /> 和 <paramref name="addr2" /> 的地址类型。</param>
    /// <param name="addr1">字段规格的第一个地址。</param>
    /// <param name="addr2">字段规格的第二个地址。</param>
    /// <param name="addr3">字段规格的第三个地址。</param>
    void DefineField(SymbolToken parent, string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3);

    /// <summary>定义单个全局变量。</summary>
    /// <param name="name">全局变量名称。</param>
    /// <param name="attributes">全局变量特性的按位组合。</param>
    /// <param name="signature">全局变量签名。</param>
    /// <param name="addrKind">
    /// <paramref name="addr1" />、<paramref name="addr2" /> 和 <paramref name="addr3" /> 的地址类型。</param>
    /// <param name="addr1">全局变量规格的第一个地址。</param>
    /// <param name="addr2">全局变量规格的第二个地址。</param>
    /// <param name="addr3">全局变量规格的第三个地址。</param>
    void DefineGlobalVariable(string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3);

    /// <summary>关闭 <see cref="T:System.Diagnostics.SymbolStore.ISymbolWriter" />，并将符号提交到符号存储区。</summary>
    void Close();

    /// <summary>在给定特性名称和特性值的情况下，定义特性。</summary>
    /// <param name="parent">正在为其定义特性的元数据标记。</param>
    /// <param name="name">特性名称。</param>
    /// <param name="data">特性值。</param>
    void SetSymAttribute(SymbolToken parent, string name, byte[] data);

    /// <summary>打开一个新的命名空间。</summary>
    /// <param name="name">新命名空间的名称。</param>
    void OpenNamespace(string name);

    /// <summary>关闭最近的命名空间。</summary>
    void CloseNamespace();

    /// <summary>指定在打开的词法范围内使用给定的、完全限定的命名空间名称。</summary>
    /// <param name="fullName">命名空间的完全限定名称。</param>
    void UsingNamespace(string fullName);

    /// <summary>指定源文件内方法的真正开始和结尾。使用 <see cref="M:System.Diagnostics.SymbolStore.ISymbolWriter.SetMethodSourceRange(System.Diagnostics.SymbolStore.ISymbolDocumentWriter,System.Int32,System.Int32,System.Diagnostics.SymbolStore.ISymbolDocumentWriter,System.Int32,System.Int32)" /> 指定方法的作用域，独立于方法内存在的序列点。</summary>
    /// <param name="startDoc">包含起始位置的文档。</param>
    /// <param name="startLine">起始行号。</param>
    /// <param name="startColumn">起始列。</param>
    /// <param name="endDoc">包含结束位置的文档。</param>
    /// <param name="endLine">结束行号。</param>
    /// <param name="endColumn">结束列号。</param>
    void SetMethodSourceRange(ISymbolDocumentWriter startDoc, int startLine, int startColumn, ISymbolDocumentWriter endDoc, int endLine, int endColumn);

    /// <summary>设置基础 ISymUnmanagedWriter（对应的非托管接口），托管 <see cref="T:System.Diagnostics.SymbolStore.ISymbolWriter" /> 使用它来发出符号。</summary>
    /// <param name="underlyingWriter">指向表示此基础编写器的代码的指针。</param>
    void SetUnderlyingWriter(IntPtr underlyingWriter);
  }
}
