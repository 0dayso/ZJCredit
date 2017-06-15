// Decompiled with JetBrains decompiler
// Type: System.Globalization.TextElementEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Globalization
{
  /// <summary>枚举字符串的文本元素。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class TextElementEnumerator : IEnumerator
  {
    private string str;
    private int index;
    private int startIndex;
    [NonSerialized]
    private int strLen;
    [NonSerialized]
    private int currTextElementLen;
    [OptionalField(VersionAdded = 2)]
    private UnicodeCategory uc;
    [OptionalField(VersionAdded = 2)]
    private int charLen;
    private int endIndex;
    private int nextTextElementLen;

    /// <summary>获取字符串中的当前文本元素。</summary>
    /// <returns>包含字符串中当前文本元素的对象。</returns>
    /// <exception cref="T:System.InvalidOperationException">枚举数位于字符串的第一个文本元素之前或最后一个文本元素之后。</exception>
    [__DynamicallyInvokable]
    public object Current
    {
      [__DynamicallyInvokable] get
      {
        return (object) this.GetTextElement();
      }
    }

    /// <summary>获取枚举数当前置于其上的文本元素的索引。</summary>
    /// <returns>枚举数当前置于其上的文本元素的索引。</returns>
    /// <exception cref="T:System.InvalidOperationException">枚举数位于字符串的第一个文本元素之前或最后一个文本元素之后。</exception>
    [__DynamicallyInvokable]
    public int ElementIndex
    {
      [__DynamicallyInvokable] get
      {
        if (this.index == this.startIndex)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
        return this.index - this.currTextElementLen;
      }
    }

    internal TextElementEnumerator(string str, int startIndex, int strLen)
    {
      this.str = str;
      this.startIndex = startIndex;
      this.strLen = strLen;
      this.Reset();
    }

    [OnDeserializing]
    private void OnDeserializing(StreamingContext ctx)
    {
      this.charLen = -1;
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      this.strLen = this.endIndex + 1;
      this.currTextElementLen = this.nextTextElementLen;
      if (this.charLen != -1)
        return;
      this.uc = CharUnicodeInfo.InternalGetUnicodeCategory(this.str, this.index, out this.charLen);
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      this.endIndex = this.strLen - 1;
      this.nextTextElementLen = this.currTextElementLen;
    }

    /// <summary>将枚举数前移到字符串的下一个文本元素。</summary>
    /// <returns>如果枚举数成功前移到下一个文本元素，则为 true；如果枚举数已超过字符串的结尾，则为 false。</returns>
    [__DynamicallyInvokable]
    public bool MoveNext()
    {
      if (this.index >= this.strLen)
      {
        this.index = this.strLen + 1;
        return false;
      }
      this.currTextElementLen = StringInfo.GetCurrentTextElementLen(this.str, this.index, this.strLen, ref this.uc, ref this.charLen);
      this.index = this.index + this.currTextElementLen;
      return true;
    }

    /// <summary>获取字符串中的当前文本元素。</summary>
    /// <returns>一个包含所读取的字符串中当前文本元素的新字符串。</returns>
    /// <exception cref="T:System.InvalidOperationException">枚举数位于字符串的第一个文本元素之前或最后一个文本元素之后。</exception>
    [__DynamicallyInvokable]
    public string GetTextElement()
    {
      if (this.index == this.startIndex)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
      if (this.index > this.strLen)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
      return this.str.Substring(this.index - this.currTextElementLen, this.currTextElementLen);
    }

    /// <summary>将枚举数设置为其初始位置，该位置位于字符串中第一个文本元素之前。</summary>
    [__DynamicallyInvokable]
    public void Reset()
    {
      this.index = this.startIndex;
      if (this.index >= this.strLen)
        return;
      this.uc = CharUnicodeInfo.InternalGetUnicodeCategory(this.str, this.index, out this.charLen);
    }
  }
}
