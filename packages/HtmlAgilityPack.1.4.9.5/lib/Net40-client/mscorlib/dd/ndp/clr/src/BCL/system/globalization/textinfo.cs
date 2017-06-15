// Decompiled with JetBrains decompiler
// Type: System.Globalization.TextInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace System.Globalization
{
  /// <summary>定义特定于书写系统的文本属性和行为（如大小写）。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class TextInfo : ICloneable, IDeserializationCallback
  {
    [OptionalField(VersionAdded = 2)]
    private string m_listSeparator;
    [OptionalField(VersionAdded = 2)]
    private bool m_isReadOnly;
    [OptionalField(VersionAdded = 3)]
    private string m_cultureName;
    [NonSerialized]
    private CultureData m_cultureData;
    [NonSerialized]
    private string m_textInfoName;
    [NonSerialized]
    private IntPtr m_dataHandle;
    [NonSerialized]
    private IntPtr m_handleOrigin;
    [NonSerialized]
    private bool? m_IsAsciiCasingSameAsInvariant;
    internal static volatile TextInfo s_Invariant;
    [OptionalField(VersionAdded = 2)]
    private string customCultureName;
    [OptionalField(VersionAdded = 1)]
    internal int m_nDataItem;
    [OptionalField(VersionAdded = 1)]
    internal bool m_useUserOverride;
    [OptionalField(VersionAdded = 1)]
    internal int m_win32LangID;
    private const int wordSeparatorMask = 536672256;

    internal static TextInfo Invariant
    {
      get
      {
        if (TextInfo.s_Invariant == null)
          TextInfo.s_Invariant = new TextInfo(CultureData.Invariant);
        return TextInfo.s_Invariant;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Globalization.TextInfo" /> 所表示书写系统使用的“美国国家标准学会”（ANSI) 代码页。</summary>
    /// <returns>当前 <see cref="T:System.Globalization.TextInfo" /> 所表示书写系统使用的 ANSI 代码页。</returns>
    public virtual int ANSICodePage
    {
      get
      {
        return this.m_cultureData.IDEFAULTANSICODEPAGE;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Globalization.TextInfo" /> 所表示书写系统使用的原始设备制造商 (OEM) 代码页。</summary>
    /// <returns>当前 <see cref="T:System.Globalization.TextInfo" /> 所表示书写系统所使用的 OEM 代码页。</returns>
    public virtual int OEMCodePage
    {
      get
      {
        return this.m_cultureData.IDEFAULTOEMCODEPAGE;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Globalization.TextInfo" /> 所表示书写系统使用的 Macintosh 代码页。</summary>
    /// <returns>当前 <see cref="T:System.Globalization.TextInfo" /> 所表示书写系统使用的 Macintosh 代码页。</returns>
    public virtual int MacCodePage
    {
      get
      {
        return this.m_cultureData.IDEFAULTMACCODEPAGE;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Globalization.TextInfo" /> 所表示书写系统使用的扩充的二进制编码的十进制交换码 (EBCDIC) 代码页。</summary>
    /// <returns>当前 <see cref="T:System.Globalization.TextInfo" /> 所表示书写系统使用的 EBCDIC 代码页。</returns>
    public virtual int EBCDICCodePage
    {
      get
      {
        return this.m_cultureData.IDEFAULTEBCDICCODEPAGE;
      }
    }

    /// <summary>获取与当前 <see cref="T:System.Globalization.TextInfo" /> 对象关联的区域性的区域性标识符。</summary>
    /// <returns>一个标识创建当前 <see cref="T:System.Globalization.TextInfo" /> 对象所使用的区域性标识符的数字。</returns>
    [ComVisible(false)]
    public int LCID
    {
      get
      {
        return CultureInfo.GetCultureInfo(this.m_textInfoName).LCID;
      }
    }

    /// <summary>获取与当前 <see cref="T:System.Globalization.TextInfo" /> 对象关联的区域性的名称。</summary>
    /// <returns>区域性的名称。</returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public string CultureName
    {
      [__DynamicallyInvokable] get
      {
        return this.m_textInfoName;
      }
    }

    /// <summary>获取一个值，该值指示当前 <see cref="T:System.Globalization.TextInfo" /> 对象是否为只读。</summary>
    /// <returns>如果当前 <see cref="T:System.Globalization.TextInfo" /> 对象为只读，则为 true；否则为 false。</returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public bool IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return this.m_isReadOnly;
      }
    }

    /// <summary>获取或设置在列表中分隔项的字符串。</summary>
    /// <returns>在列表中分隔项的字符串。</returns>
    /// <exception cref="T:System.ArgumentNullException">The value in a set operation is null.</exception>
    /// <exception cref="T:System.InvalidOperationException">In a set operation, the current <see cref="T:System.Globalization.TextInfo" /> object is read-only.</exception>
    [__DynamicallyInvokable]
    public virtual string ListSeparator
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        if (this.m_listSeparator == null)
          this.m_listSeparator = this.m_cultureData.SLIST;
        return this.m_listSeparator;
      }
      [ComVisible(false), __DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_String"));
        this.VerifyWritable();
        this.m_listSeparator = value;
      }
    }

    private bool IsAsciiCasingSameAsInvariant
    {
      get
      {
        if (!this.m_IsAsciiCasingSameAsInvariant.HasValue)
          this.m_IsAsciiCasingSameAsInvariant = new bool?(CultureInfo.GetCultureInfo(this.m_textInfoName).CompareInfo.Compare("abcdefghijklmnopqrstuvwxyz", "ABCDEFGHIJKLMNOPQRSTUVWXYZ", CompareOptions.IgnoreCase) == 0);
        return this.m_IsAsciiCasingSameAsInvariant.Value;
      }
    }

    /// <summary>获取一个值，该值指示当前 <see cref="T:System.Globalization.TextInfo" /> 对象是否表示文本从右到左书写的书写系统。</summary>
    /// <returns>如果文本从右到左书写，则为 true；否则为 false。</returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public bool IsRightToLeft
    {
      [__DynamicallyInvokable] get
      {
        return this.m_cultureData.IsRightToLeft;
      }
    }

    internal TextInfo(CultureData cultureData)
    {
      this.m_cultureData = cultureData;
      this.m_cultureName = this.m_cultureData.CultureName;
      this.m_textInfoName = this.m_cultureData.STEXTINFO;
      IntPtr handleOrigin;
      this.m_dataHandle = CompareInfo.InternalInitSortHandle(this.m_textInfoName, out handleOrigin);
      this.m_handleOrigin = handleOrigin;
    }

    [OnDeserializing]
    private void OnDeserializing(StreamingContext ctx)
    {
      this.m_cultureData = (CultureData) null;
      this.m_cultureName = (string) null;
    }

    private void OnDeserialized()
    {
      if (this.m_cultureData != null)
        return;
      if (this.m_cultureName == null)
        this.m_cultureName = this.customCultureName == null ? (this.m_win32LangID != 0 ? CultureInfo.GetCultureInfo(this.m_win32LangID).m_cultureData.CultureName : "ar-SA") : this.customCultureName;
      this.m_cultureData = CultureInfo.GetCultureInfo(this.m_cultureName).m_cultureData;
      this.m_textInfoName = this.m_cultureData.STEXTINFO;
      IntPtr handleOrigin;
      this.m_dataHandle = CompareInfo.InternalInitSortHandle(this.m_textInfoName, out handleOrigin);
      this.m_handleOrigin = handleOrigin;
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      this.OnDeserialized();
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      this.m_useUserOverride = false;
      this.customCultureName = this.m_cultureName;
      this.m_win32LangID = CultureInfo.GetCultureInfo(this.m_cultureName).LCID;
    }

    internal static int GetHashCodeOrdinalIgnoreCase(string s)
    {
      return TextInfo.GetHashCodeOrdinalIgnoreCase(s, false, 0L);
    }

    internal static int GetHashCodeOrdinalIgnoreCase(string s, bool forceRandomizedHashing, long additionalEntropy)
    {
      return TextInfo.Invariant.GetCaseInsensitiveHashCode(s, forceRandomizedHashing, additionalEntropy);
    }

    [SecuritySafeCritical]
    internal static bool TryFastFindStringOrdinalIgnoreCase(int searchFlags, string source, int startIndex, string value, int count, ref int foundIndex)
    {
      int searchFlags1 = searchFlags;
      string source1 = source;
      int sourceCount = count;
      int startIndex1 = startIndex;
      string target = value;
      int length = target.Length;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      int& foundIndex1 = @foundIndex;
      return TextInfo.InternalTryFindStringOrdinalIgnoreCase(searchFlags1, source1, sourceCount, startIndex1, target, length, foundIndex1);
    }

    [SecuritySafeCritical]
    internal static int CompareOrdinalIgnoreCase(string str1, string str2)
    {
      return TextInfo.InternalCompareStringOrdinalIgnoreCase(str1, 0, str2, 0, str1.Length, str2.Length);
    }

    [SecuritySafeCritical]
    internal static int CompareOrdinalIgnoreCaseEx(string strA, int indexA, string strB, int indexB, int lengthA, int lengthB)
    {
      return TextInfo.InternalCompareStringOrdinalIgnoreCase(strA, indexA, strB, indexB, lengthA, lengthB);
    }

    internal static int IndexOfStringOrdinalIgnoreCase(string source, string value, int startIndex, int count)
    {
      if (source.Length == 0 && value.Length == 0)
        return 0;
      int foundIndex = -1;
      if (TextInfo.TryFastFindStringOrdinalIgnoreCase(4194304, source, startIndex, value, count, ref foundIndex))
        return foundIndex;
      for (int index = startIndex + count - value.Length; startIndex <= index; ++startIndex)
      {
        if (TextInfo.CompareOrdinalIgnoreCaseEx(source, startIndex, value, 0, value.Length, value.Length) == 0)
          return startIndex;
      }
      return -1;
    }

    internal static int LastIndexOfStringOrdinalIgnoreCase(string source, string value, int startIndex, int count)
    {
      if (value.Length == 0)
        return startIndex;
      int foundIndex = -1;
      if (TextInfo.TryFastFindStringOrdinalIgnoreCase(8388608, source, startIndex, value, count, ref foundIndex))
        return foundIndex;
      int num = startIndex - count + 1;
      if (value.Length > 0)
        startIndex -= value.Length - 1;
      for (; startIndex >= num; --startIndex)
      {
        if (TextInfo.CompareOrdinalIgnoreCaseEx(source, startIndex, value, 0, value.Length, value.Length) == 0)
          return startIndex;
      }
      return -1;
    }

    /// <summary>创建作为当前 <see cref="T:System.Globalization.TextInfo" /> 对象的副本的新对象。</summary>
    /// <returns>一个新的 <see cref="T:System.Object" /> 实例，它是当前 <see cref="T:System.Globalization.TextInfo" /> 对象的成员副本。</returns>
    [ComVisible(false)]
    public virtual object Clone()
    {
      object obj;
      ((TextInfo) (obj = this.MemberwiseClone())).SetReadOnlyState(false);
      return obj;
    }

    /// <summary>返回指定的 <see cref="T:System.Globalization.TextInfo" /> 对象的只读版本。</summary>
    /// <returns>由 <paramref name="textInfo" /> 参数指定的 <see cref="T:System.Globalization.TextInfo" /> 对象（如果 <paramref name="textInfo" /> 是只读的）。- 或 -由 <paramref name="textInfo" /> 指定的 <see cref="T:System.Globalization.TextInfo" /> 对象的只读成员副本（如果 <paramref name="textInfo" /> 不是只读的）。</returns>
    /// <param name="textInfo">一个 <see cref="T:System.Globalization.TextInfo" /> 对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="textInfo" /> is null.</exception>
    [ComVisible(false)]
    public static TextInfo ReadOnly(TextInfo textInfo)
    {
      if (textInfo == null)
        throw new ArgumentNullException("textInfo");
      if (textInfo.IsReadOnly)
        return textInfo;
      TextInfo textInfo1 = (TextInfo) textInfo.MemberwiseClone();
      int num = 1;
      textInfo1.SetReadOnlyState(num != 0);
      return textInfo1;
    }

    private void VerifyWritable()
    {
      if (this.m_isReadOnly)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
    }

    internal void SetReadOnlyState(bool readOnly)
    {
      this.m_isReadOnly = readOnly;
    }

    /// <summary>将指定的字符转换为小写。</summary>
    /// <returns>转换为小写的指定字符。</returns>
    /// <param name="c">要转换为小写的字符。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual char ToLower(char c)
    {
      if (TextInfo.IsAscii(c) && this.IsAsciiCasingSameAsInvariant)
        return TextInfo.ToLowerAsciiInvariant(c);
      return TextInfo.InternalChangeCaseChar(this.m_dataHandle, this.m_handleOrigin, this.m_textInfoName, c, false);
    }

    /// <summary>将指定的字符串转换为小写。</summary>
    /// <returns>转换为小写的指定字符串。</returns>
    /// <param name="str">要转换为小写的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="str" /> is null. </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual string ToLower(string str)
    {
      if (str == null)
        throw new ArgumentNullException("str");
      return TextInfo.InternalChangeCaseString(this.m_dataHandle, this.m_handleOrigin, this.m_textInfoName, str, false);
    }

    private static char ToLowerAsciiInvariant(char c)
    {
      if (65 <= (int) c && (int) c <= 90)
        c |= ' ';
      return c;
    }

    /// <summary>将指定的字符转换为大写。</summary>
    /// <returns>转换为大写的指定字符。</returns>
    /// <param name="c">要转换为大写的字符。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual char ToUpper(char c)
    {
      if (TextInfo.IsAscii(c) && this.IsAsciiCasingSameAsInvariant)
        return TextInfo.ToUpperAsciiInvariant(c);
      return TextInfo.InternalChangeCaseChar(this.m_dataHandle, this.m_handleOrigin, this.m_textInfoName, c, true);
    }

    /// <summary>将指定的字符串转换为大写。</summary>
    /// <returns>转换为大写的指定字符串。</returns>
    /// <param name="str">要转换为大写的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="str" /> is null. </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public virtual string ToUpper(string str)
    {
      if (str == null)
        throw new ArgumentNullException("str");
      return TextInfo.InternalChangeCaseString(this.m_dataHandle, this.m_handleOrigin, this.m_textInfoName, str, true);
    }

    private static char ToUpperAsciiInvariant(char c)
    {
      if (97 <= (int) c && (int) c <= 122)
        c &= '\xFFDF';
      return c;
    }

    private static bool IsAscii(char c)
    {
      return (int) c < 128;
    }

    /// <summary>确定指定的对象是否与当前 <see cref="T:System.Globalization.TextInfo" /> 对象表示同一书写体系。</summary>
    /// <returns>如果 <paramref name="obj" /> 与当前 <see cref="T:System.Globalization.TextInfo" /> 表示同一书写系统，则为 true；否则为 false。</returns>
    /// <param name="obj">将与当前 <see cref="T:System.Globalization.TextInfo" /> 进行比较的对象。 </param>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      TextInfo textInfo = obj as TextInfo;
      if (textInfo != null)
        return this.CultureName.Equals(textInfo.CultureName);
      return false;
    }

    /// <summary>用作当前 <see cref="T:System.Globalization.TextInfo" /> 的哈希函数，适合用在哈希算法和数据结构（如哈希表）中。</summary>
    /// <returns>当前 <see cref="T:System.Globalization.TextInfo" /> 的哈希代码。</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.CultureName.GetHashCode();
    }

    /// <summary>返回表示当前 <see cref="T:System.Globalization.TextInfo" /> 的字符串。</summary>
    /// <returns>表示当前 <see cref="T:System.Globalization.TextInfo" /> 的字符串。</returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return "TextInfo - " + this.m_cultureData.CultureName;
    }

    /// <summary>将指定字符串转换为标题大写（全部大写将被视为首字母缩写的词不包含在内）。</summary>
    /// <returns>转换为标题大写的指定字符串。</returns>
    /// <param name="str">转换为标题大写的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="str" /> is null. </exception>
    public string ToTitleCase(string str)
    {
      if (str == null)
        throw new ArgumentNullException("str");
      if (str.Length == 0)
        return str;
      StringBuilder result = new StringBuilder();
      string str1 = (string) null;
      int index1;
      for (int index2 = 0; index2 < str.Length; index2 = index1 + 1)
      {
        int charLength;
        UnicodeCategory unicodeCategory1 = CharUnicodeInfo.InternalGetUnicodeCategory(str, index2, out charLength);
        if (char.CheckLetter(unicodeCategory1))
        {
          index1 = this.AddTitlecaseLetter(ref result, ref str, index2, charLength) + 1;
          int startIndex = index1;
          bool flag = unicodeCategory1 == UnicodeCategory.LowercaseLetter;
          while (index1 < str.Length)
          {
            UnicodeCategory unicodeCategory2 = CharUnicodeInfo.InternalGetUnicodeCategory(str, index1, out charLength);
            if (TextInfo.IsLetterCategory(unicodeCategory2))
            {
              if (unicodeCategory2 == UnicodeCategory.LowercaseLetter)
                flag = true;
              index1 += charLength;
            }
            else if ((int) str[index1] == 39)
            {
              ++index1;
              if (flag)
              {
                if (str1 == null)
                  str1 = this.ToLower(str);
                result.Append(str1, startIndex, index1 - startIndex);
              }
              else
                result.Append(str, startIndex, index1 - startIndex);
              startIndex = index1;
              flag = true;
            }
            else if (!TextInfo.IsWordSeparator(unicodeCategory2))
              index1 += charLength;
            else
              break;
          }
          int count = index1 - startIndex;
          if (count > 0)
          {
            if (flag)
            {
              if (str1 == null)
                str1 = this.ToLower(str);
              result.Append(str1, startIndex, count);
            }
            else
              result.Append(str, startIndex, count);
          }
          if (index1 < str.Length)
            index1 = TextInfo.AddNonLetter(ref result, ref str, index1, charLength);
        }
        else
          index1 = TextInfo.AddNonLetter(ref result, ref str, index2, charLength);
      }
      return result.ToString();
    }

    private static int AddNonLetter(ref StringBuilder result, ref string input, int inputIndex, int charLen)
    {
      if (charLen == 2)
      {
        result.Append(input[inputIndex++]);
        result.Append(input[inputIndex]);
      }
      else
        result.Append(input[inputIndex]);
      return inputIndex;
    }

    private int AddTitlecaseLetter(ref StringBuilder result, ref string input, int inputIndex, int charLen)
    {
      if (charLen == 2)
      {
        result.Append(this.ToUpper(input.Substring(inputIndex, charLen)));
        ++inputIndex;
      }
      else
      {
        switch (input[inputIndex])
        {
          case 'Ǆ':
          case 'ǅ':
          case 'ǆ':
            result.Append('ǅ');
            break;
          case 'Ǉ':
          case 'ǈ':
          case 'ǉ':
            result.Append('ǈ');
            break;
          case 'Ǌ':
          case 'ǋ':
          case 'ǌ':
            result.Append('ǋ');
            break;
          case 'Ǳ':
          case 'ǲ':
          case 'ǳ':
            result.Append('ǲ');
            break;
          default:
            result.Append(this.ToUpper(input[inputIndex]));
            break;
        }
      }
      return inputIndex;
    }

    private static bool IsWordSeparator(UnicodeCategory category)
    {
      return (uint) (536672256 & 1 << (int) (category & (UnicodeCategory.Format | UnicodeCategory.Surrogate))) > 0U;
    }

    private static bool IsLetterCategory(UnicodeCategory uc)
    {
      if (uc != UnicodeCategory.UppercaseLetter && uc != UnicodeCategory.LowercaseLetter && (uc != UnicodeCategory.TitlecaseLetter && uc != UnicodeCategory.ModifierLetter))
        return uc == UnicodeCategory.OtherLetter;
      return true;
    }

    void IDeserializationCallback.OnDeserialization(object sender)
    {
      this.OnDeserialized();
    }

    [SecuritySafeCritical]
    internal int GetCaseInsensitiveHashCode(string str)
    {
      return this.GetCaseInsensitiveHashCode(str, false, 0L);
    }

    [SecuritySafeCritical]
    internal int GetCaseInsensitiveHashCode(string str, bool forceRandomizedHashing, long additionalEntropy)
    {
      if (str == null)
        throw new ArgumentNullException("str");
      return TextInfo.InternalGetCaseInsHash(this.m_dataHandle, this.m_handleOrigin, this.m_textInfoName, str, forceRandomizedHashing, additionalEntropy);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern char InternalChangeCaseChar(IntPtr handle, IntPtr handleOrigin, string localeName, char ch, bool isToUpper);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern string InternalChangeCaseString(IntPtr handle, IntPtr handleOrigin, string localeName, string str, bool isToUpper);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int InternalGetCaseInsHash(IntPtr handle, IntPtr handleOrigin, string localeName, string str, bool forceRandomizedHashing, long additionalEntropy);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int InternalCompareStringOrdinalIgnoreCase(string string1, int index1, string string2, int index2, int length1, int length2);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool InternalTryFindStringOrdinalIgnoreCase(int searchFlags, string source, int sourceCount, int startIndex, string target, int targetCount, ref int foundIndex);
  }
}
