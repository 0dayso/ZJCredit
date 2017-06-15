// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.ISymbolDocument
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>表示由符号存储引用的文档。</summary>
  [ComVisible(true)]
  public interface ISymbolDocument
  {
    /// <summary>获取当前文档的 URL。</summary>
    /// <returns>当前文档的 URL。</returns>
    string URL { get; }

    /// <summary>获取当前文档的类型。</summary>
    /// <returns>当前文档的类型。</returns>
    Guid DocumentType { get; }

    /// <summary>获取当前文档的语言。</summary>
    /// <returns>当前文档的语言。</returns>
    Guid Language { get; }

    /// <summary>获取当前文档的语言供应商。</summary>
    /// <returns>当前文档的语言供应商。</returns>
    Guid LanguageVendor { get; }

    /// <summary>获取校验和算法标识符。</summary>
    /// <returns>标识校验和算法的 GUID。如果没有校验和，值为全零。</returns>
    Guid CheckSumAlgorithmId { get; }

    /// <summary>检查当前文档是否存储在符号存储区中。</summary>
    /// <returns>如果当前文档存储在符号存储区中，则为 true；否则为 false。</returns>
    bool HasEmbeddedSource { get; }

    /// <summary>获取嵌入源的长度（以字节表示）。</summary>
    /// <returns>当前文档的源长度。</returns>
    int SourceLength { get; }

    /// <summary>获取校验和。</summary>
    /// <returns>校验和。</returns>
    byte[] GetCheckSum();

    /// <summary>在当前文档中的一行不一定是序列点的情况下，返回作为序列点的最近的一行。</summary>
    /// <returns>作为序列点的最近的一行。</returns>
    /// <param name="line">文档中的指定行。</param>
    int FindClosestLine(int line);

    /// <summary>获取指定范围内的嵌入文档源。</summary>
    /// <returns>指定范围内的文档源。</returns>
    /// <param name="startLine">当前文档中的起始行。</param>
    /// <param name="startColumn">当前文档中的起始列。</param>
    /// <param name="endLine">当前文档中的结束行。</param>
    /// <param name="endColumn">当前文档中的结束列。</param>
    byte[] GetSourceRange(int startLine, int startColumn, int endLine, int endColumn);
  }
}
