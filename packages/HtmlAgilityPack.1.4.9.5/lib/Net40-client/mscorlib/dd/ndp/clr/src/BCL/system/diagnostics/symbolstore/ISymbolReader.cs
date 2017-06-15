// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.ISymbolReader
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>表示托管代码的符号读取器。</summary>
  [ComVisible(true)]
  public interface ISymbolReader
  {
    /// <summary>获取方法的元数据标记，它指定为模块的用户入口点（如果有的话）。</summary>
    /// <returns>方法的元数据标记，它是模块的用户入口点。</returns>
    SymbolToken UserEntryPoint { get; }

    /// <summary>获取按语言、供应商和类型指定的文档。</summary>
    /// <returns>指定的文档。</returns>
    /// <param name="url">标识文档的 URL。</param>
    /// <param name="language">文档语言。可以将此参数指定为 <see cref="F:System.Guid.Empty" />。</param>
    /// <param name="languageVendor">文档语言的供应商标识。可以将此参数指定为 <see cref="F:System.Guid.Empty" />。</param>
    /// <param name="documentType">文档的类型。可以将此参数指定为 <see cref="F:System.Guid.Empty" />。</param>
    ISymbolDocument GetDocument(string url, Guid language, Guid languageVendor, Guid documentType);

    /// <summary>获取在符号存储区中定义的所有文档的数组。</summary>
    /// <returns>在符号存储区中定义的所有文档的数组。</returns>
    ISymbolDocument[] GetDocuments();

    /// <summary>在给定方法的标识符的情况下，获取符号读取器方法对象。</summary>
    /// <returns>指定方法标识符表示的符号读取器方法对象。</returns>
    /// <param name="method">方法的元数据标记。</param>
    ISymbolMethod GetMethod(SymbolToken method);

    /// <summary>在给定方法的标识符及其编辑和连续版本的情况下，获取符号读取器方法对象。</summary>
    /// <returns>指定方法标识符表示的符号读取器方法对象。</returns>
    /// <param name="method">方法的元数据标记。</param>
    /// <param name="version">方法的编辑和连续版本。</param>
    ISymbolMethod GetMethod(SymbolToken method, int version);

    /// <summary>在给定父级的情况下，获取非局部变量。</summary>
    /// <returns>父级的变量的数组。</returns>
    /// <param name="parent">为其请求变量的类型的元数据标记。</param>
    ISymbolVariable[] GetVariables(SymbolToken parent);

    /// <summary>获取模块中的所有全局变量。</summary>
    /// <returns>模块中所有变量的数组。</returns>
    ISymbolVariable[] GetGlobalVariables();

    /// <summary>获取包含文档中的指定位置的符号读取器方法对象。</summary>
    /// <returns>文档中的指定位置的读取器方法对象。</returns>
    /// <param name="document">方法所在的文档。</param>
    /// <param name="line">文档内行的位置。行带有编号，从 1 开始。</param>
    /// <param name="column">文档内列的位置。列带有编号，从 1 开始。</param>
    ISymbolMethod GetMethodFromDocumentPosition(ISymbolDocument document, int line, int column);

    /// <summary>在给定特性名称的情况下，获取特性值。</summary>
    /// <returns>属性的值。</returns>
    /// <param name="parent">为其请求特性的对象的元数据标记。</param>
    /// <param name="name">特性名称。</param>
    byte[] GetSymAttribute(SymbolToken parent, string name);

    /// <summary>获取在当前符号存储区的全局范围内定义的命名空间。</summary>
    /// <returns>在当前符号存储区的全局范围内定义的命名空间。</returns>
    ISymbolNamespace[] GetNamespaces();
  }
}
