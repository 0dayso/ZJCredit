// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.ParameterToken
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>ParameterToken 结构是由元数据返回以表示参数的标记的不透明表示形式。</summary>
  [ComVisible(true)]
  [Serializable]
  public struct ParameterToken
  {
    /// <summary>
    /// <see cref="P:System.Reflection.Emit.ParameterToken.Token" /> 值为 0 的默认 ParameterToken。</summary>
    public static readonly ParameterToken Empty;
    internal int m_tkParameter;

    /// <summary>检索此参数的元数据标记。</summary>
    /// <returns>只读。检索此参数的元数据标记。</returns>
    public int Token
    {
      get
      {
        return this.m_tkParameter;
      }
    }

    internal ParameterToken(int tkParam)
    {
      this.m_tkParameter = tkParam;
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.Emit.ParameterToken" /> 结构是否等同。</summary>
    /// <returns>如果 <paramref name="a" /> 等于 <paramref name="b" />，则为 true；否则为 false。</returns>
    /// <param name="a">要与 <paramref name="b" /> 进行比较的 <see cref="T:System.Reflection.Emit.ParameterToken" />。</param>
    /// <param name="b">要与 <paramref name="a" /> 进行比较的 <see cref="T:System.Reflection.Emit.ParameterToken" />。</param>
    public static bool operator ==(ParameterToken a, ParameterToken b)
    {
      return a.Equals(b);
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.Emit.ParameterToken" /> 结构是否不相等。</summary>
    /// <returns>如果 <paramref name="a" /> 不等于 <paramref name="b" />，则为 true；否则为 false。</returns>
    /// <param name="a">要与 <paramref name="b" /> 进行比较的 <see cref="T:System.Reflection.Emit.ParameterToken" />。</param>
    /// <param name="b">要与 <paramref name="a" /> 进行比较的 <see cref="T:System.Reflection.Emit.ParameterToken" />。</param>
    public static bool operator !=(ParameterToken a, ParameterToken b)
    {
      return !(a == b);
    }

    /// <summary>生成该参数的哈希代码。</summary>
    /// <returns>返回该参数的哈希代码。</returns>
    public override int GetHashCode()
    {
      return this.m_tkParameter;
    }

    /// <summary>检查给定对象是否为 ParameterToken 的实例和是否等于此实例。</summary>
    /// <returns>如果 <paramref name="obj" /> 为 ParameterToken 的实例并等于当前实例，则为 true；否则，为 false。</returns>
    /// <param name="obj">要与此对象比较的对象。</param>
    public override bool Equals(object obj)
    {
      if (obj is ParameterToken)
        return this.Equals((ParameterToken) obj);
      return false;
    }

    /// <summary>指示当前实例是否与指定的 <see cref="T:System.Reflection.Emit.ParameterToken" /> 相等。</summary>
    /// <returns>如果 <paramref name="obj" /> 的值等于当前实例的值，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前实例进行比较的 <see cref="T:System.Reflection.Emit.ParameterToken" />。</param>
    public bool Equals(ParameterToken obj)
    {
      return obj.m_tkParameter == this.m_tkParameter;
    }
  }
}
