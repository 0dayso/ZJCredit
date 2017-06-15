// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.ISymbolDocumentWriter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>表示由符号存储引用的文档。</summary>
  [ComVisible(true)]
  public interface ISymbolDocumentWriter
  {
    /// <summary>将文档的原始源存储在符号存储区中。</summary>
    /// <param name="source">表示为无符号字节的文档源。</param>
    void SetSource(byte[] source);

    /// <summary>设置校验和信息。</summary>
    /// <param name="algorithmId">表示算法 ID 的 GUID。</param>
    /// <param name="checkSum">校验和。</param>
    void SetCheckSum(Guid algorithmId, byte[] checkSum);
  }
}
