// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.SymbolToken
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>
  /// <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> 结构是表示符号信息的标记的对象表示形式。</summary>
  [ComVisible(true)]
  public struct SymbolToken
  {
    internal int m_token;

    /// <summary>在给定值的情况下，初始化 <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> 结构的新实例。</summary>
    /// <param name="val">要用于标记的值。</param>
    public SymbolToken(int val)
    {
      this.m_token = val;
    }

    /// <summary>返回一个值，该值指示两个 <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> 对象是否相等。</summary>
    /// <returns>如果 <paramref name="a" /> 和 <paramref name="b" /> 相等，则为 true；否则为 false。</returns>
    /// <param name="a">一个 <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> 结构。</param>
    /// <param name="b">一个 <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> 结构。</param>
    public static bool operator ==(SymbolToken a, SymbolToken b)
    {
      return a.Equals(b);
    }

    /// <summary>返回一个值，该值指示两个 <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> 对象是否不相等。</summary>
    /// <returns>如果 <paramref name="a" /> 和 <paramref name="b" /> 不相等，则为 true；否则为 false。</returns>
    /// <param name="a">一个 <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> 结构。</param>
    /// <param name="b">一个 <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> 结构。</param>
    public static bool operator !=(SymbolToken a, SymbolToken b)
    {
      return !(a == b);
    }

    /// <summary>获取当前标记的值。</summary>
    /// <returns>当前标记的值。</returns>
    public int GetToken()
    {
      return this.m_token;
    }

    /// <summary>生成当前标记的哈希代码。</summary>
    /// <returns>当前标记的哈希代码。</returns>
    public override int GetHashCode()
    {
      return this.m_token;
    }

    /// <summary>确定 <paramref name="obj" /> 是否为 <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> 的实例以及是否等于此实例。</summary>
    /// <returns>如果 <paramref name="obj" /> 是 <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> 的一个实例并且等于此实例，则为 true；否则，为 false。</returns>
    /// <param name="obj">要检查的对象。</param>
    public override bool Equals(object obj)
    {
      if (obj is SymbolToken)
        return this.Equals((SymbolToken) obj);
      return false;
    }

    /// <summary>确定 <paramref name="obj" /> 是否等于此实例。</summary>
    /// <returns>如果 <paramref name="obj" /> 等于此实例，则为 true；否则为 false。</returns>
    /// <param name="obj">要检查的 <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" />。</param>
    public bool Equals(SymbolToken obj)
    {
      return obj.m_token == this.m_token;
    }
  }
}
