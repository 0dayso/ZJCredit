// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.SerializationInfoEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>提供一种对格式化程序友好的机制，用于分析 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 中的数据。此类不能被继承。</summary>
  [ComVisible(true)]
  public sealed class SerializationInfoEnumerator : IEnumerator
  {
    private string[] m_members;
    private object[] m_data;
    private Type[] m_types;
    private int m_numItems;
    private int m_currItem;
    private bool m_current;

    object IEnumerator.Current
    {
      get
      {
        if (!this.m_current)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
        return (object) new SerializationEntry(this.m_members[this.m_currItem], this.m_data[this.m_currItem], this.m_types[this.m_currItem]);
      }
    }

    /// <summary>获取当前所检查的项。</summary>
    /// <returns>当前所检查的项。</returns>
    /// <exception cref="T:System.InvalidOperationException">枚举器还未开始枚举项或已到达枚举的结尾。</exception>
    public SerializationEntry Current
    {
      get
      {
        if (!this.m_current)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
        return new SerializationEntry(this.m_members[this.m_currItem], this.m_data[this.m_currItem], this.m_types[this.m_currItem]);
      }
    }

    /// <summary>获取当前所检查的项的名称。</summary>
    /// <returns>项名称。</returns>
    /// <exception cref="T:System.InvalidOperationException">枚举器还未开始枚举项或已到达枚举的结尾。</exception>
    public string Name
    {
      get
      {
        if (!this.m_current)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
        return this.m_members[this.m_currItem];
      }
    }

    /// <summary>获取当前所检查的项的值。</summary>
    /// <returns>当前所检查的项的值。</returns>
    /// <exception cref="T:System.InvalidOperationException">枚举器还未开始枚举项或已到达枚举的结尾。</exception>
    public object Value
    {
      get
      {
        if (!this.m_current)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
        return this.m_data[this.m_currItem];
      }
    }

    /// <summary>获取当前所检查的项的类型。</summary>
    /// <returns>当前所检查的项的类型。</returns>
    /// <exception cref="T:System.InvalidOperationException">枚举器还未开始枚举项或已到达枚举的结尾。</exception>
    public Type ObjectType
    {
      get
      {
        if (!this.m_current)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
        return this.m_types[this.m_currItem];
      }
    }

    internal SerializationInfoEnumerator(string[] members, object[] info, Type[] types, int numItems)
    {
      this.m_members = members;
      this.m_data = info;
      this.m_types = types;
      this.m_numItems = numItems - 1;
      this.m_currItem = -1;
      this.m_current = false;
    }

    /// <summary>将枚举数更新到下一项。</summary>
    /// <returns>如果找到新的元素，则为 true；否则为 false。</returns>
    public bool MoveNext()
    {
      if (this.m_currItem < this.m_numItems)
      {
        this.m_currItem = this.m_currItem + 1;
        this.m_current = true;
      }
      else
        this.m_current = false;
      return this.m_current;
    }

    /// <summary>将枚举数重置为第一项。</summary>
    public void Reset()
    {
      this.m_currItem = -1;
      this.m_current = false;
    }
  }
}
