// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.ISymbolVariable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>表示符号存储区内的变量。</summary>
  [ComVisible(true)]
  public interface ISymbolVariable
  {
    /// <summary>获取变量名。</summary>
    /// <returns>变量的名称。</returns>
    string Name { get; }

    /// <summary>获取变量的特性。</summary>
    /// <returns>变量特性。</returns>
    object Attributes { get; }

    /// <summary>获取 <see cref="T:System.Diagnostics.SymbolStore.SymAddressKind" /> 描述地址类型的值。</summary>
    /// <returns>地址类型。<see cref="T:System.Diagnostics.SymbolStore.SymAddressKind" /> 值之一。</returns>
    SymAddressKind AddressKind { get; }

    /// <summary>获取变量的第一个地址。</summary>
    /// <returns>变量的第一个地址。</returns>
    int AddressField1 { get; }

    /// <summary>获取变量的第二个地址。</summary>
    /// <returns>变量的第二个地址。</returns>
    int AddressField2 { get; }

    /// <summary>获取变量的第三个地址。</summary>
    /// <returns>变量的第三个地址。</returns>
    int AddressField3 { get; }

    /// <summary>获取变量范围内的变量的起始偏移量。</summary>
    /// <returns>变量的起始偏移量。</returns>
    int StartOffset { get; }

    /// <summary>获取变量范围内的变量的结束偏移量。</summary>
    /// <returns>变量的结束偏移量。</returns>
    int EndOffset { get; }

    /// <summary>获取变量签名。</summary>
    /// <returns>作为不透明 Blob 的变量签名。</returns>
    byte[] GetSignature();
  }
}
