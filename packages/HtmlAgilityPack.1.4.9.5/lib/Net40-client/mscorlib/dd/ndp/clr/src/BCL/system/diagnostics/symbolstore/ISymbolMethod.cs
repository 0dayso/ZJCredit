// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.ISymbolMethod
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>表示符号存储区内的方法。</summary>
  [ComVisible(true)]
  public interface ISymbolMethod
  {
    /// <summary>获取 <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" />，它包含当前方法的元数据。</summary>
    /// <returns>当前方法的元数据标记。</returns>
    SymbolToken Token { get; }

    /// <summary>获取方法中序列点的计数。</summary>
    /// <returns>方法中序列点的计数。</returns>
    int SequencePointCount { get; }

    /// <summary>获取当前方法的根词法范围。此范围包括整个方法。</summary>
    /// <returns>包括整个方法的根词法范围。</returns>
    ISymbolScope RootScope { get; }

    /// <summary>获取当前方法的序列点。</summary>
    /// <param name="offsets">序列点从方法开始的字节偏移量的数组。</param>
    /// <param name="documents">序列点所在的文档的数组。</param>
    /// <param name="lines">序列点所在的文档中的行的数组。</param>
    /// <param name="columns">序列点所在的文档中的列的数组。</param>
    /// <param name="endLines">序列点结束的文档中的行的数组。</param>
    /// <param name="endColumns">序列点结束的文档中的列的数组。</param>
    void GetSequencePoints(int[] offsets, ISymbolDocument[] documents, int[] lines, int[] columns, int[] endLines, int[] endColumns);

    /// <summary>在给定方法内的一个偏移量的情况下，返回最封闭的词法范围。</summary>
    /// <returns>方法内给定字节偏移量的最封闭的词法范围。</returns>
    /// <param name="offset">词法范围的方法内的字节偏移量。</param>
    ISymbolScope GetScope(int offset);

    /// <summary>获取与指定位置对应的方法内的 Microsoft 中间语言 (MSIL) 偏移量。</summary>
    /// <returns>指定文档内的偏移量。</returns>
    /// <param name="document">为其请求偏移量的文档。</param>
    /// <param name="line">与偏移量对应的文档行。</param>
    /// <param name="column">与偏移量对应的文档列。</param>
    int GetOffset(ISymbolDocument document, int line, int column);

    /// <summary>获取与 Microsoft 中间语言 (MSIL) 的范围对应的起始和结束偏移量对的数组，给定位置在此方法内包括该数组。</summary>
    /// <returns>起始和结束偏移量对的数组。</returns>
    /// <param name="document">为其请求偏移量的文档。</param>
    /// <param name="line">与范围对应的文档行。</param>
    /// <param name="column">与范围对应的文档列。</param>
    int[] GetRanges(ISymbolDocument document, int line, int column);

    /// <summary>获取当前方法的参数。</summary>
    /// <returns>当前方法的参数数组。</returns>
    ISymbolVariable[] GetParameters();

    /// <summary>获取在其中定义当前方法的命名空间。</summary>
    /// <returns>在其中定义当前方法的命名空间。</returns>
    ISymbolNamespace GetNamespace();

    /// <summary>获取当前方法的源的起始和结束位置。</summary>
    /// <returns>如果定义了位置，则为 true；否则为 false。</returns>
    /// <param name="docs">起始和结束源文档。</param>
    /// <param name="lines">对应的源文档中的起始和结束行。</param>
    /// <param name="columns">对应的源文档中的起始和结束列。</param>
    bool GetSourceStartEnd(ISymbolDocument[] docs, int[] lines, int[] columns);
  }
}
