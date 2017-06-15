// Decompiled with JetBrains decompiler
// Type: System.CharEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System
{
  /// <summary>支持循环访问 <see cref="T:System.String" /> 对象并读取它的各个字符。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public sealed class CharEnumerator : IEnumerator, ICloneable, IEnumerator<char>, IDisposable
  {
    private string str;
    private int index;
    private char currentElement;

    object IEnumerator.Current
    {
      get
      {
        if (this.index == -1)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
        if (this.index >= this.str.Length)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
        return (object) this.currentElement;
      }
    }

    /// <summary>获取由此 <see cref="T:System.CharEnumerator" /> 对象枚举的字符串中当前引用的字符。</summary>
    /// <returns>当前由此 <see cref="T:System.CharEnumerator" /> 对象引用的 Unicode 字符。</returns>
    /// <exception cref="T:System.InvalidOperationException">该索引无效；即它位于枚举字符串的第一个字符之前或最后一个字符之后。</exception>
    /// <filterpriority>2</filterpriority>
    public char Current
    {
      get
      {
        if (this.index == -1)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
        if (this.index >= this.str.Length)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
        return this.currentElement;
      }
    }

    internal CharEnumerator(string str)
    {
      this.str = str;
      this.index = -1;
    }

    /// <summary>创建当前 <see cref="T:System.CharEnumerator" /> 对象的副本。</summary>
    /// <returns>
    /// <see cref="T:System.Object" />，是当前 <see cref="T:System.CharEnumerator" /> 对象的副本。</returns>
    /// <filterpriority>2</filterpriority>
    public object Clone()
    {
      return this.MemberwiseClone();
    }

    /// <summary>递增当前 <see cref="T:System.CharEnumerator" /> 对象的内部索引使其指向枚举的字符串的下一个字符。</summary>
    /// <returns>如果索引递增成功并且在枚举字符串内，则为 true；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public bool MoveNext()
    {
      if (this.index < this.str.Length - 1)
      {
        this.index = this.index + 1;
        this.currentElement = this.str[this.index];
        return true;
      }
      this.index = this.str.Length;
      return false;
    }

    /// <summary>释放由 <see cref="T:System.CharEnumerator" /> 类的当前实例占用的所有资源。</summary>
    public void Dispose()
    {
      if (this.str != null)
        this.index = this.str.Length;
      this.str = (string) null;
    }

    /// <summary>将索引初始化为逻辑上位于枚举字符串的第一个字符之前的位置。</summary>
    /// <filterpriority>2</filterpriority>
    public void Reset()
    {
      this.currentElement = char.MinValue;
      this.index = -1;
    }
  }
}
