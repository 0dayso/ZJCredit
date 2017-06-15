// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.FieldToken
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>FieldToken 结构是表示字段的标记的对象表示形式。</summary>
  [ComVisible(true)]
  [Serializable]
  public struct FieldToken
  {
    /// <summary>
    /// <see cref="P:System.Reflection.Emit.FieldToken.Token" /> 值为 0 的默认 FieldToken。</summary>
    public static readonly FieldToken Empty;
    internal int m_fieldTok;
    internal object m_class;

    /// <summary>检索该字段的元数据标记。</summary>
    /// <returns>只读。检索该字段的元数据标记。</returns>
    public int Token
    {
      get
      {
        return this.m_fieldTok;
      }
    }

    internal FieldToken(int field, Type fieldClass)
    {
      this.m_fieldTok = field;
      this.m_class = (object) fieldClass;
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.Emit.FieldToken" /> 结构是否等同。</summary>
    /// <returns>如果 <paramref name="a" /> 等于 <paramref name="b" />，则为 true；否则为 false。</returns>
    /// <param name="a">要与 <paramref name="b" /> 进行比较的 <see cref="T:System.Reflection.Emit.FieldToken" />。</param>
    /// <param name="b">要与 <paramref name="a" /> 进行比较的 <see cref="T:System.Reflection.Emit.FieldToken" />。</param>
    public static bool operator ==(FieldToken a, FieldToken b)
    {
      return a.Equals(b);
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.Emit.FieldToken" /> 结构是否不相等。</summary>
    /// <returns>如果 <paramref name="a" /> 不等于 <paramref name="b" />，则为 true；否则为 false。</returns>
    /// <param name="a">要与 <paramref name="b" /> 进行比较的 <see cref="T:System.Reflection.Emit.FieldToken" />。</param>
    /// <param name="b">要与 <paramref name="a" /> 进行比较的 <see cref="T:System.Reflection.Emit.FieldToken" />。</param>
    public static bool operator !=(FieldToken a, FieldToken b)
    {
      return !(a == b);
    }

    /// <summary>生成此字段的哈希代码。</summary>
    /// <returns>返回此实例的哈希代码。</returns>
    public override int GetHashCode()
    {
      return this.m_fieldTok;
    }

    /// <summary>确定对象是否是 FieldToken 的实例和是否等于该实例。</summary>
    /// <returns>如果 <paramref name="obj" /> 是 FieldToken 的实例并等于此对象，则返回 true；否则返回 false。</returns>
    /// <param name="obj">与此 FieldToken 进行比较的对象。</param>
    public override bool Equals(object obj)
    {
      if (obj is FieldToken)
        return this.Equals((FieldToken) obj);
      return false;
    }

    /// <summary>指示当前实例是否等于指定的 <see cref="T:System.Reflection.Emit.FieldToken" />。</summary>
    /// <returns>如果 <paramref name="obj" /> 的值等于当前实例的值，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前实例进行比较的 <see cref="T:System.Reflection.Emit.FieldToken" />。</param>
    public bool Equals(FieldToken obj)
    {
      if (obj.m_fieldTok == this.m_fieldTok)
        return obj.m_class == this.m_class;
      return false;
    }
  }
}
