// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.Label
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>表示指令流中的标签。Label 与 <see cref="T:System.Reflection.Emit.ILGenerator" /> 类一起使用。</summary>
  [ComVisible(true)]
  [Serializable]
  public struct Label
  {
    internal int m_label;

    internal Label(int label)
    {
      this.m_label = label;
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.Emit.Label" /> 结构是否等同。</summary>
    /// <returns>如果 <paramref name="a" /> 等于 <paramref name="b" />，则为 true；否则为 false。</returns>
    /// <param name="a">要与 <paramref name="b" /> 进行比较的 <see cref="T:System.Reflection.Emit.Label" />。</param>
    /// <param name="b">要与 <paramref name="a" /> 进行比较的 <see cref="T:System.Reflection.Emit.Label" />。</param>
    public static bool operator ==(Label a, Label b)
    {
      return a.Equals(b);
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.Emit.Label" /> 结构是否不相等。</summary>
    /// <returns>如果 <paramref name="a" /> 不等于 <paramref name="b" />，则为 true；否则为 false。</returns>
    /// <param name="a">要与 <paramref name="b" /> 进行比较的 <see cref="T:System.Reflection.Emit.Label" />。</param>
    /// <param name="b">要与 <paramref name="a" /> 进行比较的 <see cref="T:System.Reflection.Emit.Label" />。</param>
    public static bool operator !=(Label a, Label b)
    {
      return !(a == b);
    }

    internal int GetLabelValue()
    {
      return this.m_label;
    }

    /// <summary>生成此实例的哈希代码。</summary>
    /// <returns>返回此实例的哈希代码。</returns>
    public override int GetHashCode()
    {
      return this.m_label;
    }

    /// <summary>检查给定对象是否为 Label 的实例和是否等于此实例。</summary>
    /// <returns>如果 <paramref name="obj" /> 是 Label 的实例并等于此对象，则返回 true；否则返回 false。</returns>
    /// <param name="obj">与此 Label 实例进行比较的对象。</param>
    public override bool Equals(object obj)
    {
      if (obj is Label)
        return this.Equals((Label) obj);
      return false;
    }

    /// <summary>指示当前实例是否等于指定的 <see cref="T:System.Reflection.Emit.Label" />。</summary>
    /// <returns>如果 <paramref name="obj" /> 的值等于当前实例的值，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前实例进行比较的 <see cref="T:System.Reflection.Emit.Label" />。</param>
    public bool Equals(Label obj)
    {
      return obj.m_label == this.m_label;
    }
  }
}
