// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.ISymbolScope
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>表示 <see cref="T:System.Diagnostics.SymbolStore.ISymbolMethod" /> 内的词法范围，提供对范围及其子范围和父范围的起始和结束偏移量的访问。</summary>
  [ComVisible(true)]
  public interface ISymbolScope
  {
    /// <summary>获取包含当前词法范围的方法。</summary>
    /// <returns>包含当前词法范围的方法。</returns>
    ISymbolMethod Method { get; }

    /// <summary>获取当前范围的父词法范围。</summary>
    /// <returns>当前范围的父词法范围。</returns>
    ISymbolScope Parent { get; }

    /// <summary>获取当前词法范围的起始偏移量。</summary>
    /// <returns>当前词法范围的起始偏移量。</returns>
    int StartOffset { get; }

    /// <summary>获取当前词法范围的结束偏移量。</summary>
    /// <returns>当前词法范围的结束偏移量。</returns>
    int EndOffset { get; }

    /// <summary>获取当前词法范围的子词法范围。</summary>
    /// <returns>当前词法范围的子词法范围。</returns>
    ISymbolScope[] GetChildren();

    /// <summary>获取当前词法范围内的局部变量。</summary>
    /// <returns>当前词法范围内的局部变量。</returns>
    ISymbolVariable[] GetLocals();

    /// <summary>获取在当前范围内使用的命名空间。</summary>
    /// <returns>在当前范围内使用的命名空间。</returns>
    ISymbolNamespace[] GetNamespaces();
  }
}
