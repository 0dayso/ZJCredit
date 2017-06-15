// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ArrayWithOffset
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>在指定的数组中封装数组和偏移量。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct ArrayWithOffset
  {
    private object m_array;
    private int m_offset;
    private int m_count;

    /// <summary>初始化 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 结构的新实例。</summary>
    /// <param name="array">托管数组。</param>
    /// <param name="offset">要通过平台调用传递的元素的偏移量（以字节为单位）。</param>
    /// <exception cref="T:System.ArgumentException">数组大于 2 GB。</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public ArrayWithOffset(object array, int offset)
    {
      this.m_array = array;
      this.m_offset = offset;
      this.m_count = 0;
      this.m_count = this.CalculateCount();
    }

    /// <summary>确定两个指定的 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 对象是否具有相同的值。</summary>
    /// <returns>如果 <paramref name="a" /> 的值与 <paramref name="b" /> 的值相同，则为 true；否则为 false。</returns>
    /// <param name="a">与 <paramref name="b" /> 参数进行比较的 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 对象。</param>
    /// <param name="b">与 <paramref name="a" /> 参数进行比较的 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 对象。</param>
    [__DynamicallyInvokable]
    public static bool operator ==(ArrayWithOffset a, ArrayWithOffset b)
    {
      return a.Equals(b);
    }

    /// <summary>确定两个指定的 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 对象是否具有不同值。</summary>
    /// <returns>如果 <paramref name="a" /> 的值与 <paramref name="b" /> 的值不同，则为 true；否则为 false。</returns>
    /// <param name="a">与 <paramref name="b" /> 参数进行比较的 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 对象。</param>
    /// <param name="b">与 <paramref name="a" /> 参数进行比较的 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 对象。</param>
    [__DynamicallyInvokable]
    public static bool operator !=(ArrayWithOffset a, ArrayWithOffset b)
    {
      return !(a == b);
    }

    /// <summary>返回此 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 引用的托管数组。</summary>
    /// <returns>此实例引用的托管数组。</returns>
    [__DynamicallyInvokable]
    public object GetArray()
    {
      return this.m_array;
    }

    /// <summary>返回当构造此 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 时提供的偏移量。</summary>
    /// <returns>此实例的偏移量。</returns>
    [__DynamicallyInvokable]
    public int GetOffset()
    {
      return this.m_offset;
    }

    /// <summary>返回此值类型的哈希代码。</summary>
    /// <returns>此实例的哈希代码。</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.m_count + this.m_offset;
    }

    /// <summary>指示指定的对象是否与当前的 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 对象匹配。</summary>
    /// <returns>如果对象与此 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 匹配，则为 true；否则，为 false。</returns>
    /// <param name="obj">要与该实例进行比较的对象。</param>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (obj is ArrayWithOffset)
        return this.Equals((ArrayWithOffset) obj);
      return false;
    }

    /// <summary>指示指定的 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 对象是否与当前实例匹配。</summary>
    /// <returns>如果指定的 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 对象与当前实例相匹配，则为 true；否则为 false。</returns>
    /// <param name="obj">与此实例进行比较的 <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> 对象。</param>
    [__DynamicallyInvokable]
    public bool Equals(ArrayWithOffset obj)
    {
      if (obj.m_array == this.m_array && obj.m_offset == this.m_offset)
        return obj.m_count == this.m_count;
      return false;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private int CalculateCount();
  }
}
