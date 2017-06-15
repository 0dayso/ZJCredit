// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.StringToken
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>表示用于表示字符串的标记。</summary>
  [ComVisible(true)]
  [Serializable]
  public struct StringToken
  {
    internal int m_string;

    /// <summary>检索该字符串的元数据标记。</summary>
    /// <returns>只读。检索该字符串的元数据标记。</returns>
    public int Token
    {
      get
      {
        return this.m_string;
      }
    }

    internal StringToken(int str)
    {
      this.m_string = str;
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.Emit.StringToken" /> 结构是否等同。</summary>
    /// <returns>如果 <paramref name="a" /> 等于 <paramref name="b" />，则为 true；否则为 false。</returns>
    /// <param name="a">要与 <paramref name="b" /> 进行比较的 <see cref="T:System.Reflection.Emit.StringToken" />。</param>
    /// <param name="b">要与 <paramref name="a" /> 进行比较的 <see cref="T:System.Reflection.Emit.StringToken" />。</param>
    public static bool operator ==(StringToken a, StringToken b)
    {
      return a.Equals(b);
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.Emit.StringToken" /> 结构是否不相等。</summary>
    /// <returns>如果 <paramref name="a" /> 不等于 <paramref name="b" />，则为 true；否则为 false。</returns>
    /// <param name="a">要与 <paramref name="b" /> 进行比较的 <see cref="T:System.Reflection.Emit.StringToken" />。</param>
    /// <param name="b">要与 <paramref name="a" /> 进行比较的 <see cref="T:System.Reflection.Emit.StringToken" />。</param>
    public static bool operator !=(StringToken a, StringToken b)
    {
      return !(a == b);
    }

    /// <summary>返回该字符串的哈希代码。</summary>
    /// <returns>返回基础字符串标记。</returns>
    public override int GetHashCode()
    {
      return this.m_string;
    }

    /// <summary>检查给定对象是否为 StringToken 的实例和是否等于此实例。</summary>
    /// <returns>如果 <paramref name="obj" /> 为 StringToken 的实例并且等于此对象，则为 true；否则为 false。</returns>
    /// <param name="obj">与此 StringToken 进行比较的对象。</param>
    public override bool Equals(object obj)
    {
      if (obj is StringToken)
        return this.Equals((StringToken) obj);
      return false;
    }

    /// <summary>指定当前实例是否等于指定的 <see cref="T:System.Reflection.Emit.StringToken" />。</summary>
    /// <returns>如果 <paramref name="obj" /> 的值等于当前实例的值，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前实例进行比较的 <see cref="T:System.Reflection.Emit.StringToken" />。</param>
    public bool Equals(StringToken obj)
    {
      return obj.m_string == this.m_string;
    }
  }
}
