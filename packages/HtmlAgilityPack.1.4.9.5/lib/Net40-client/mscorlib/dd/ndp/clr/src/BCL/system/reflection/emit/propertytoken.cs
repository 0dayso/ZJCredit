// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.PropertyToken
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>PropertyToken 结构是由元数据返回以表示属性的 Token 的不透明表示形式。</summary>
  [ComVisible(true)]
  [Serializable]
  public struct PropertyToken
  {
    /// <summary>
    /// <see cref="P:System.Reflection.Emit.PropertyToken.Token" /> 值为 0 的默认 PropertyToken。</summary>
    public static readonly PropertyToken Empty;
    internal int m_property;

    /// <summary>检索此属性的元数据标记。</summary>
    /// <returns>只读。检索此实例的元数据标记。</returns>
    public int Token
    {
      get
      {
        return this.m_property;
      }
    }

    internal PropertyToken(int str)
    {
      this.m_property = str;
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.Emit.PropertyToken" /> 结构是否等同。</summary>
    /// <returns>如果 <paramref name="a" /> 等于 <paramref name="b" />，则为 true；否则为 false。</returns>
    /// <param name="a">要与 <paramref name="b" /> 进行比较的 <see cref="T:System.Reflection.Emit.PropertyToken" />。</param>
    /// <param name="b">要与 <paramref name="a" /> 进行比较的 <see cref="T:System.Reflection.Emit.PropertyToken" />。</param>
    public static bool operator ==(PropertyToken a, PropertyToken b)
    {
      return a.Equals(b);
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.Emit.PropertyToken" /> 结构是否不相等。</summary>
    /// <returns>如果 <paramref name="a" /> 不等于 <paramref name="b" />，则为 true；否则为 false。</returns>
    /// <param name="a">要与 <paramref name="b" /> 进行比较的 <see cref="T:System.Reflection.Emit.PropertyToken" />。</param>
    /// <param name="b">要与 <paramref name="a" /> 进行比较的 <see cref="T:System.Reflection.Emit.PropertyToken" />。</param>
    public static bool operator !=(PropertyToken a, PropertyToken b)
    {
      return !(a == b);
    }

    /// <summary>生成此属性的哈希代码。</summary>
    /// <returns>返回此属性的哈希代码。</returns>
    public override int GetHashCode()
    {
      return this.m_property;
    }

    /// <summary>检查给定对象是否为 PropertyToken 的实例和是否等于此实例。</summary>
    /// <returns>如果 <paramref name="obj" /> 是 PropertyToken 的实例并等于当前实例，则为 true；否则，为 false。</returns>
    /// <param name="obj">该对象的对象。</param>
    public override bool Equals(object obj)
    {
      if (obj is PropertyToken)
        return this.Equals((PropertyToken) obj);
      return false;
    }

    /// <summary>指示当前实例是否等于指定的 <see cref="T:System.Reflection.Emit.PropertyToken" />。</summary>
    /// <returns>如果 <paramref name="obj" /> 的值等于当前实例的值，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前实例进行比较的 <see cref="T:System.Reflection.Emit.PropertyToken" />。</param>
    public bool Equals(PropertyToken obj)
    {
      return obj.m_property == this.m_property;
    }
  }
}
