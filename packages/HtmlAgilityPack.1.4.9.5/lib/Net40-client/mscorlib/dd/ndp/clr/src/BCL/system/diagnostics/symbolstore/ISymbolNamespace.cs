// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.ISymbolNamespace
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>表示符号存储区内的命名空间。</summary>
  [ComVisible(true)]
  public interface ISymbolNamespace
  {
    /// <summary>获取当前命名空间。</summary>
    /// <returns>当前命名空间。</returns>
    string Name { get; }

    /// <summary>获取当前命名空间的子成员。</summary>
    /// <returns>当前命名空间的子成员。</returns>
    ISymbolNamespace[] GetNamespaces();

    /// <summary>获取在当前命名空间的全局范围内定义的所有变量。</summary>
    /// <returns>在当前命名空间的全局范围内定义的变量。</returns>
    ISymbolVariable[] GetVariables();
  }
}
