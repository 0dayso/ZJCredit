// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.MethodToken
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>MethodToken 结构是表示方法的标记的对象表示形式。</summary>
  [ComVisible(true)]
  [Serializable]
  public struct MethodToken
  {
    /// <summary>
    /// <see cref="P:System.Reflection.Emit.MethodToken.Token" /> 值为 0 的默认 MethodToken。</summary>
    public static readonly MethodToken Empty;
    internal int m_method;

    /// <summary>返回此方法的元数据标记。</summary>
    /// <returns>只读。返回此方法的元数据标记。</returns>
    public int Token
    {
      get
      {
        return this.m_method;
      }
    }

    internal MethodToken(int str)
    {
      this.m_method = str;
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.Emit.MethodToken" /> 结构是否等同。</summary>
    /// <returns>如果 <paramref name="a" /> 等于 <paramref name="b" />，则为 true；否则为 false。</returns>
    /// <param name="a">要与 <paramref name="b" /> 进行比较的 <see cref="T:System.Reflection.Emit.MethodToken" />。</param>
    /// <param name="b">要与 <paramref name="a" /> 进行比较的 <see cref="T:System.Reflection.Emit.MethodToken" />。</param>
    public static bool operator ==(MethodToken a, MethodToken b)
    {
      return a.Equals(b);
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.Emit.MethodToken" /> 结构是否不相等。</summary>
    /// <returns>如果 <paramref name="a" /> 不等于 <paramref name="b" />，则为 true；否则为 false。</returns>
    /// <param name="a">要与 <paramref name="b" /> 进行比较的 <see cref="T:System.Reflection.Emit.MethodToken" />。</param>
    /// <param name="b">要与 <paramref name="a" /> 进行比较的 <see cref="T:System.Reflection.Emit.MethodToken" />。</param>
    public static bool operator !=(MethodToken a, MethodToken b)
    {
      return !(a == b);
    }

    /// <summary>返回为此方法生成的哈希代码。</summary>
    /// <returns>返回此实例的哈希代码。</returns>
    public override int GetHashCode()
    {
      return this.m_method;
    }

    /// <summary>测试给定对象是否等于此 MethodToken 对象。</summary>
    /// <returns>如果 <paramref name="obj" /> 为 MethodToken 的实例并且等于此对象，则为 true；否则为 false。</returns>
    /// <param name="obj">要与此对象比较的对象。</param>
    public override bool Equals(object obj)
    {
      if (obj is MethodToken)
        return this.Equals((MethodToken) obj);
      return false;
    }

    /// <summary>指示当前实例是否等于指定的 <see cref="T:System.Reflection.Emit.MethodToken" />。</summary>
    /// <returns>如果 <paramref name="obj" /> 的值等于当前实例的值，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前实例进行比较的 <see cref="T:System.Reflection.Emit.MethodToken" />。</param>
    public bool Equals(MethodToken obj)
    {
      return obj.m_method == this.m_method;
    }
  }
}
