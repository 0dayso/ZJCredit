// Decompiled with JetBrains decompiler
// Type: System.Globalization.StringInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Globalization
{
  /// <summary>提供功能将字符串拆分为文本元素并循环访问这些文本元素。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class StringInfo
  {
    [OptionalField(VersionAdded = 2)]
    private string m_str;
    [NonSerialized]
    private int[] m_indexes;

    private int[] Indexes
    {
      get
      {
        if (this.m_indexes == null && 0 < this.String.Length)
          this.m_indexes = StringInfo.ParseCombiningCharacters(this.String);
        return this.m_indexes;
      }
    }

    /// <summary>获取或设置当前 <see cref="T:System.Globalization.StringInfo" /> 对象的值。</summary>
    /// <returns>作为当前 <see cref="T:System.Globalization.StringInfo" /> 对象的值的字符串。</returns>
    /// <exception cref="T:System.ArgumentNullException">设置操作中的值为 null。</exception>
    [__DynamicallyInvokable]
    public string String
    {
      [__DynamicallyInvokable] get
      {
        return this.m_str;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException("String", Environment.GetResourceString("ArgumentNull_String"));
        this.m_str = value;
        this.m_indexes = (int[]) null;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Globalization.StringInfo" /> 对象中的文本元素的数目。</summary>
    /// <returns>此 <see cref="T:System.Globalization.StringInfo" /> 对象中的基本字符、代理项对和组合字符序列的数目。</returns>
    [__DynamicallyInvokable]
    public int LengthInTextElements
    {
      [__DynamicallyInvokable] get
      {
        if (this.Indexes == null)
          return 0;
        return this.Indexes.Length;
      }
    }

    /// <summary>初始化 <see cref="T:System.Globalization.StringInfo" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public StringInfo()
      : this("")
    {
    }

    /// <summary>将 <see cref="T:System.Globalization.StringInfo" /> 类的新实例初始化为指定的字符串。</summary>
    /// <param name="value">用于初始化此 <see cref="T:System.Globalization.StringInfo" /> 对象的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public StringInfo(string value)
    {
      this.String = value;
    }

    [OnDeserializing]
    private void OnDeserializing(StreamingContext ctx)
    {
      this.m_str = string.Empty;
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      if (this.m_str.Length != 0)
        return;
      this.m_indexes = (int[]) null;
    }

    /// <summary>指示当前 <see cref="T:System.Globalization.StringInfo" /> 对象是否与指定的对象相等。</summary>
    /// <returns>如果 <paramref name="value" /> 参数是 <see cref="T:System.Globalization.StringInfo" /> 对象并且其 <see cref="P:System.Globalization.StringInfo.String" /> 属性等同于此 <see cref="T:System.Globalization.StringInfo" /> 对象的 <see cref="P:System.Globalization.StringInfo.String" /> 属性，则为 true；否则，为 false。</returns>
    /// <param name="value">一个对象。</param>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      StringInfo stringInfo = value as StringInfo;
      if (stringInfo != null)
        return this.m_str.Equals(stringInfo.m_str);
      return false;
    }

    /// <summary>计算当前 <see cref="T:System.Globalization.StringInfo" /> 对象的值的哈希代码。</summary>
    /// <returns>基于此 <see cref="T:System.Globalization.StringInfo" /> 对象的字符串值的 32 位有符号整数哈希代码。</returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.m_str.GetHashCode();
    }

    /// <summary>从当前的 <see cref="T:System.Globalization.StringInfo" /> 对象检索文本元素的子字符串（从指定的文本元素开始，一直到最后一个文本元素）。</summary>
    /// <returns>此 <see cref="T:System.Globalization.StringInfo" /> 对象中的文本元素的子字符串（从 <paramref name="startingTextElement" /> 参数指定的文本元素索引开始，一直到此对象中的最后一个文本元素）。</returns>
    /// <param name="startingTextElement">此 <see cref="T:System.Globalization.StringInfo" /> 对象中文本元素的零始索引。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startingTextElement" /> 小于零。- 或 -当前 <see cref="T:System.Globalization.StringInfo" /> 对象的值字符串是空字符串 ("")。</exception>
    public string SubstringByTextElements(int startingTextElement)
    {
      if (this.Indexes != null)
        return this.SubstringByTextElements(startingTextElement, this.Indexes.Length - startingTextElement);
      if (startingTextElement < 0)
        throw new ArgumentOutOfRangeException("startingTextElement", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      throw new ArgumentOutOfRangeException("startingTextElement", Environment.GetResourceString("Arg_ArgumentOutOfRangeException"));
    }

    /// <summary>从当前的 <see cref="T:System.Globalization.StringInfo" /> 对象中检索文本元素的子字符串（从指定文本元素开始，一直到指定数目的文本元素）。</summary>
    /// <returns>此 <see cref="T:System.Globalization.StringInfo" /> 对象中的文本元素的子字符串。子字符串包含个数由 <paramref name="lengthInTextElements" /> 参数指定的文本元素，并从 <paramref name="startingTextElement" /> 参数指定的文本元素索引开始。</returns>
    /// <param name="startingTextElement">此 <see cref="T:System.Globalization.StringInfo" /> 对象中文本元素的零始索引。</param>
    /// <param name="lengthInTextElements">要检索的文本元素的数目。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startingTextElement" /> 小于零。- 或 -<paramref name="startingTextElement" /> 大于或等于当前 <see cref="T:System.Globalization.StringInfo" /> 对象的值字符串的长度。- 或 -<paramref name="lengthInTextElements" /> 小于零。- 或 -当前 <see cref="T:System.Globalization.StringInfo" /> 对象的值字符串是空字符串 ("")。- 或 -<paramref name="startingTextElement" /> 和 <paramref name="lengthInTextElements" /> 指定了大于此 <see cref="T:System.Globalization.StringInfo" /> 对象中的文本元素数目的索引。</exception>
    public string SubstringByTextElements(int startingTextElement, int lengthInTextElements)
    {
      if (startingTextElement < 0)
        throw new ArgumentOutOfRangeException("startingTextElement", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      if (this.String.Length == 0 || startingTextElement >= this.Indexes.Length)
        throw new ArgumentOutOfRangeException("startingTextElement", Environment.GetResourceString("Arg_ArgumentOutOfRangeException"));
      if (lengthInTextElements < 0)
        throw new ArgumentOutOfRangeException("lengthInTextElements", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      if (startingTextElement > this.Indexes.Length - lengthInTextElements)
        throw new ArgumentOutOfRangeException("lengthInTextElements", Environment.GetResourceString("Arg_ArgumentOutOfRangeException"));
      int startIndex = this.Indexes[startingTextElement];
      if (startingTextElement + lengthInTextElements == this.Indexes.Length)
        return this.String.Substring(startIndex);
      return this.String.Substring(startIndex, this.Indexes[lengthInTextElements + startingTextElement] - startIndex);
    }

    /// <summary>获取指定字符串中的第一个文本元素。</summary>
    /// <returns>包含指定字符串中的第一个文本元素的字符串。</returns>
    /// <param name="str">要从其获取文本元素的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="str" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public static string GetNextTextElement(string str)
    {
      return StringInfo.GetNextTextElement(str, 0);
    }

    internal static int GetCurrentTextElementLen(string str, int index, int len, ref UnicodeCategory ucCurrent, ref int currentCharCount)
    {
      if (index + currentCharCount == len)
        return currentCharCount;
      int charLength;
      UnicodeCategory unicodeCategory1 = CharUnicodeInfo.InternalGetUnicodeCategory(str, index + currentCharCount, out charLength);
      if (CharUnicodeInfo.IsCombiningCategory(unicodeCategory1) && !CharUnicodeInfo.IsCombiningCategory(ucCurrent) && (ucCurrent != UnicodeCategory.Format && ucCurrent != UnicodeCategory.Control) && (ucCurrent != UnicodeCategory.OtherNotAssigned && ucCurrent != UnicodeCategory.Surrogate))
      {
        int num = index;
        index += currentCharCount + charLength;
        while (index < len)
        {
          UnicodeCategory unicodeCategory2 = CharUnicodeInfo.InternalGetUnicodeCategory(str, index, out charLength);
          if (!CharUnicodeInfo.IsCombiningCategory(unicodeCategory2))
          {
            ucCurrent = unicodeCategory2;
            currentCharCount = charLength;
            break;
          }
          index += charLength;
        }
        return index - num;
      }
      int num1 = currentCharCount;
      ucCurrent = unicodeCategory1;
      currentCharCount = charLength;
      return num1;
    }

    /// <summary>获取指定字符串中指定索引处的文本元素。</summary>
    /// <returns>包含指定字符串中指定索引处的文本元素的字符串。</returns>
    /// <param name="str">要从其获取文本元素的字符串。</param>
    /// <param name="index">文本元素开始位置的从零开始的索引。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="str" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 超出了 <paramref name="str" /> 的有效索引范围。</exception>
    [__DynamicallyInvokable]
    public static string GetNextTextElement(string str, int index)
    {
      if (str == null)
        throw new ArgumentNullException("str");
      int length = str.Length;
      if (index < 0 || index >= length)
      {
        if (index == length)
          return string.Empty;
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      }
      int charLength;
      UnicodeCategory unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, index, out charLength);
      return str.Substring(index, StringInfo.GetCurrentTextElementLen(str, index, length, ref unicodeCategory, ref charLength));
    }

    /// <summary>返回一个循环访问整个字符串的文本元素的枚举数。</summary>
    /// <returns>整个字符串的 <see cref="T:System.Globalization.TextElementEnumerator" />。</returns>
    /// <param name="str">要循环访问的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="str" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public static TextElementEnumerator GetTextElementEnumerator(string str)
    {
      return StringInfo.GetTextElementEnumerator(str, 0);
    }

    /// <summary>返回一个枚举数，它循环访问字符串的文本元素并从指定索引处开始。</summary>
    /// <returns>在 <paramref name="index" /> 处开始的字符串的 <see cref="T:System.Globalization.TextElementEnumerator" />。</returns>
    /// <param name="str">要循环访问的字符串。</param>
    /// <param name="index">开始迭代处的从零开始的索引。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="str" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 超出了 <paramref name="str" /> 的有效索引范围。</exception>
    [__DynamicallyInvokable]
    public static TextElementEnumerator GetTextElementEnumerator(string str, int index)
    {
      if (str == null)
        throw new ArgumentNullException("str");
      int length = str.Length;
      if (index < 0 || index > length)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      return new TextElementEnumerator(str, index, length);
    }

    /// <summary>返回指定字符串中每个基字符、高代理项或控制字符的索引。</summary>
    /// <returns>一个整数数组，它包含指定字符串中每个基字符、高代理项或控制字符的索引（从零开始）。</returns>
    /// <param name="str">要搜索的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="str" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public static int[] ParseCombiningCharacters(string str)
    {
      if (str == null)
        throw new ArgumentNullException("str");
      int length1 = str.Length;
      int[] numArray1 = new int[length1];
      if (length1 == 0)
        return numArray1;
      int length2 = 0;
      int index = 0;
      int charLength;
      UnicodeCategory unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, 0, out charLength);
      while (index < length1)
      {
        numArray1[length2++] = index;
        index += StringInfo.GetCurrentTextElementLen(str, index, length1, ref unicodeCategory, ref charLength);
      }
      if (length2 >= length1)
        return numArray1;
      int[] numArray2 = new int[length2];
      Array.Copy((Array) numArray1, (Array) numArray2, length2);
      return numArray2;
    }
  }
}
