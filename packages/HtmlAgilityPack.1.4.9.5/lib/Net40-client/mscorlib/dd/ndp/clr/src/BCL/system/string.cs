// Decompiled with JetBrains decompiler
// Type: System.String
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;

namespace System
{
  /// <summary>将文本表示为一系列 Unicode 字符。若要浏览此类型的.NET Framework 源代码，请参阅 Reference Source。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class String : IComparable, ICloneable, IConvertible, IEnumerable, IComparable<string>, IEnumerable<char>, IEquatable<string>
  {
    [NonSerialized]
    private int m_stringLength;
    [NonSerialized]
    private char m_firstChar;
    private const int TrimHead = 0;
    private const int TrimTail = 1;
    private const int TrimBoth = 2;
    /// <summary>表示空字符串。此字段为只读。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static readonly string Empty;
    private const int charPtrAlignConst = 1;
    private const int alignConst = 3;

    internal char FirstChar
    {
      get
      {
        return this.m_firstChar;
      }
    }

    /// <summary>获取当前 <see cref="T:System.Char" /> 对象中位于指定位置的 <see cref="T:System.String" /> 对象。</summary>
    /// <returns>位于 <paramref name="index" /> 位置的对象。</returns>
    /// <param name="index">当前的字符串中的位置。</param>
    /// <exception cref="T:System.IndexOutOfRangeException">
    /// <paramref name="index" /> 大于或等于此对象的长度小于等于零。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    [IndexerName("Chars")]
    public char this[int index] { [SecuritySafeCritical, __DynamicallyInvokable, MethodImpl(MethodImplOptions.InternalCall)] get; }

    /// <summary>获取当前 <see cref="T:System.String" /> 对象中的字符数。</summary>
    /// <returns>当前字符串中字符的数量。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int Length { [SecuritySafeCritical, __DynamicallyInvokable, MethodImpl(MethodImplOptions.InternalCall)] get; }

    /// <summary>将 <see cref="T:System.String" /> 类的新实例初始化为由指向 Unicode 字符数组的指定指针指示的值。</summary>
    /// <param name="value">指向以 null 终止的 Unicode 字符数组的指针。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">当前进程不具有对寻址的所有字符读取访问权限。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="value" /> 指定一个数组，包含一个无效的 Unicode 字符，或 <paramref name="value" /> 指定小于 64000 的地址。</exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public unsafe String(char* value);

    /// <summary>将 <see cref="T:System.String" /> 类的新实例初始化为由指向 Unicode 字符数组的指定指针指示的值、该数组内的起始字符位置和一个长度指示的值。</summary>
    /// <param name="value">指向 Unicode 字符数组的指针。</param>
    /// <param name="startIndex">
    /// <paramref name="value" /> 内的起始位置。</param>
    /// <param name="length">要使用的 <paramref name="value" /> 内的字符数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 或 <paramref name="length" /> 小于零， <paramref name="value" /> + <paramref name="startIndex" /> 导致指针溢出，或当前进程不具有对寻址的所有字符读取访问权限。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="value" /> 指定一个数组，包含一个无效的 Unicode 字符，或 <paramref name="value" /> + <paramref name="startIndex" /> 指定小于 64000 的地址。</exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public unsafe String(char* value, int startIndex, int length);

    /// <summary>将 <see cref="T:System.String" /> 类的新实例初始化为由指向 8 位有符号整数数组的指针指示的值。</summary>
    /// <param name="value">一个指针，指向以 null 结尾的 8 位带符号整数数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">新实例 <see cref="T:System.String" /> 无法使用初始化 <paramref name="value" />, 假定 <paramref name="value" /> 用 ANSI 编码。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">由的 null 终止字符决定要初始化的新字符串的长度 <paramref name="value" />, ，是太大，无法分配。</exception>
    /// <exception cref="T:System.AccessViolationException">
    /// <paramref name="value" /> 指定了无效的地址。</exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public unsafe String(sbyte* value);

    /// <summary>将 <see cref="T:System.String" /> 类的新实例初始化为由指向 8 位有符号整数数组的指定指针、该数组内的起始位置和一个长度指示的值。</summary>
    /// <param name="value">指向 8 位带符号整数数组的指针。</param>
    /// <param name="startIndex">
    /// <paramref name="value" /> 内的起始位置。</param>
    /// <param name="length">要使用的 <paramref name="value" /> 内的字符数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 或 <paramref name="length" /> 小于零。- 或 -指定的地址 <paramref name="value" /> + <paramref name="startIndex" /> 当前平台中 ； 对于而言太大，即地址计算溢出。- 或 -要初始化的新字符串的长度是太大，无法分配。</exception>
    /// <exception cref="T:System.ArgumentException">指定的地址 <paramref name="value" /> + <paramref name="startIndex" /> 为小于 64 K。- 或 - 新实例 <see cref="T:System.String" /> 无法使用初始化 <paramref name="value" />, 假定 <paramref name="value" /> 用 ANSI 编码。</exception>
    /// <exception cref="T:System.AccessViolationException">
    /// <paramref name="value" />, <paramref name="startIndex" />, ，和 <paramref name="length" /> 共同指定了无效的地址。</exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public unsafe String(sbyte* value, int startIndex, int length);

    /// <summary>将 <see cref="T:System.String" /> 的新实例初始化为由指向 8 位有符号整数数组的指定指针、该数组内的起始位置、长度以及 <see cref="T:System.Text.Encoding" /> 对象指示的值。</summary>
    /// <param name="value">指向 8 位带符号整数数组的指针。</param>
    /// <param name="startIndex">
    /// <paramref name="value" /> 内的起始位置。</param>
    /// <param name="length">要使用的 <paramref name="value" /> 内的字符数。</param>
    /// <param name="enc">一个对象，用于指定如何对 <paramref name="value" /> 所引用的数组进行编码。如果 <paramref name="enc" /> 为 null，则假定以 ANSI 编码。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 或 <paramref name="length" /> 小于零。- 或 -指定的地址 <paramref name="value" /> + <paramref name="startIndex" /> 当前平台中 ； 对于而言太大，即地址计算溢出。- 或 -要初始化的新字符串的长度是太大，无法分配。</exception>
    /// <exception cref="T:System.ArgumentException">指定的地址 <paramref name="value" /> + <paramref name="startIndex" /> 为小于 64 K。- 或 - 新实例 <see cref="T:System.String" /> 无法使用初始化 <paramref name="value" />, 假定 <paramref name="value" /> 进行编码所指定的 <paramref name="enc" />。</exception>
    /// <exception cref="T:System.AccessViolationException">
    /// <paramref name="value" />, <paramref name="startIndex" />, ，和 <paramref name="length" /> 共同指定了无效的地址。</exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public unsafe String(sbyte* value, int startIndex, int length, Encoding enc);

    /// <summary>将 <see cref="T:System.String" /> 类的新实例初始化为由 Unicode 字符数组、该数组内的起始字符位置和一个长度指示的值。</summary>
    /// <param name="value">Unicode 字符的数组。</param>
    /// <param name="startIndex">
    /// <paramref name="value" /> 内的起始位置。</param>
    /// <param name="length">要使用的 <paramref name="value" /> 内的字符数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 或 <paramref name="length" /> 小于零。- 或 - 总和 <paramref name="startIndex" /> 和 <paramref name="length" /> 中的元素数大于 <paramref name="value" />。</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public String(char[] value, int startIndex, int length);

    /// <summary>将 <see cref="T:System.String" /> 类的新实例初始化为由 Unicode 字符数组指示的值。</summary>
    /// <param name="value">Unicode 字符的数组。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public String(char[] value);

    /// <summary>将 <see cref="T:System.String" /> 类的新实例初始化为由重复指定次数的指定 Unicode 字符指示的值。</summary>
    /// <param name="c">一个 Unicode 字符。</param>
    /// <param name="count">
    /// <paramref name="c" /> 出现的次数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 小于零。</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public String(char c, int count);

    /// <summary>确定两个指定的字符串是否具有相同的值。</summary>
    /// <returns>如果 true 的值与 <paramref name="a" /> 的值相同，则为 <paramref name="b" />；否则为 false。</returns>
    /// <param name="a">要比较的第一个字符串，或 null。</param>
    /// <param name="b">要比较的第二个字符串，或 null。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator ==(string a, string b)
    {
      return string.Equals(a, b);
    }

    /// <summary>确定两个指定的字符串是否具有不同的值。</summary>
    /// <returns>如果 true 的值与 <paramref name="a" /> 的值不同，则为 <paramref name="b" />；否则为 false。</returns>
    /// <param name="a">要比较的第一个字符串，或 null。</param>
    /// <param name="b">要比较的第二个字符串，或 null。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator !=(string a, string b)
    {
      return !string.Equals(a, b);
    }

    /// <summary>串联字符串数组的所有元素，其中在每个元素之间使用指定的分隔符。</summary>
    /// <returns>一个由 <paramref name="value" /> 中的元素组成的字符串，这些元素以 <paramref name="separator" /> 字符串分隔。如果 <paramref name="value" /> 为空数组，该方法将返回 <see cref="F:System.String.Empty" />。</returns>
    /// <param name="separator">要用作分隔符的字符串。只有在 <paramref name="separator" /> 具有多个元素时，<paramref name="value" /> 才包括在返回的字符串中。</param>
    /// <param name="value">一个数组，其中包含要连接的元素。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string Join(string separator, params string[] value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      return string.Join(separator, value, 0, value.Length);
    }

    /// <summary>串联对象数组的各个元素，其中在每个元素之间使用指定的分隔符。</summary>
    /// <returns>一个由 <paramref name="values" /> 的元素组成的字符串，这些元素以 <paramref name="separator" /> 字符串分隔。如果 <paramref name="values" /> 为空数组，该方法将返回 <see cref="F:System.String.Empty" />。</returns>
    /// <param name="separator">要用作分隔符的字符串。只有在 <paramref name="separator" /> 具有多个元素时，<paramref name="values" /> 才包括在返回的字符串中。</param>
    /// <param name="values">一个数组，其中包含要连接的元素。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="values" /> 为 null。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public static string Join(string separator, params object[] values)
    {
      if (values == null)
        throw new ArgumentNullException("values");
      if (values.Length == 0 || values[0] == null)
        return string.Empty;
      if (separator == null)
        separator = string.Empty;
      StringBuilder sb = StringBuilderCache.Acquire(16);
      string string1 = values[0].ToString();
      if (string1 != null)
        sb.Append(string1);
      for (int index = 1; index < values.Length; ++index)
      {
        sb.Append(separator);
        if (values[index] != null)
        {
          string string2 = values[index].ToString();
          if (string2 != null)
            sb.Append(string2);
        }
      }
      return StringBuilderCache.GetStringAndRelease(sb);
    }

    /// <summary>串联集合的成员，其中在每个成员之间使用指定的分隔符。</summary>
    /// <returns>一个由 <paramref name="values" /> 的成员组成的字符串，这些成员以 <paramref name="separator" /> 字符串分隔。如果 <paramref name="values" /> 没有成员，则该方法返回 <see cref="F:System.String.Empty" />。</returns>
    /// <param name="separator">要用作分隔符的字符串。只有在 <paramref name="separator" /> 具有多个元素时，<paramref name="values" /> 才包括在返回的字符串中。</param>
    /// <param name="values">一个包含要串联的对象的集合。</param>
    /// <typeparam name="T">
    /// <paramref name="values" /> 成员的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="values" /> 为 null。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public static string Join<T>(string separator, IEnumerable<T> values)
    {
      if (values == null)
        throw new ArgumentNullException("values");
      if (separator == null)
        separator = string.Empty;
      using (IEnumerator<T> enumerator = values.GetEnumerator())
      {
        if (!enumerator.MoveNext())
          return string.Empty;
        StringBuilder sb = StringBuilderCache.Acquire(16);
        if ((object) enumerator.Current != null)
        {
          string @string = enumerator.Current.ToString();
          if (@string != null)
            sb.Append(@string);
        }
        while (enumerator.MoveNext())
        {
          sb.Append(separator);
          if ((object) enumerator.Current != null)
          {
            string @string = enumerator.Current.ToString();
            if (@string != null)
              sb.Append(@string);
          }
        }
        return StringBuilderCache.GetStringAndRelease(sb);
      }
    }

    /// <summary>串联类型为 <see cref="T:System.Collections.Generic.IEnumerable`1" /> 的 <see cref="T:System.String" /> 构造集合的成员，其中在每个成员之间使用指定的分隔符。</summary>
    /// <returns>一个由 <paramref name="values" /> 的成员组成的字符串，这些成员以 <paramref name="separator" /> 字符串分隔。如果 <paramref name="values" /> 没有成员，则该方法返回 <see cref="F:System.String.Empty" />。</returns>
    /// <param name="separator">要用作分隔符的字符串。只有在 <paramref name="separator" /> 具有多个元素时，<paramref name="values" /> 才包括在返回的字符串中。</param>
    /// <param name="values">一个包含要串联的字符串的集合。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="values" /> 为 null。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public static string Join(string separator, IEnumerable<string> values)
    {
      if (values == null)
        throw new ArgumentNullException("values");
      if (separator == null)
        separator = string.Empty;
      using (IEnumerator<string> enumerator = values.GetEnumerator())
      {
        if (!enumerator.MoveNext())
          return string.Empty;
        StringBuilder sb = StringBuilderCache.Acquire(16);
        if (enumerator.Current != null)
          sb.Append(enumerator.Current);
        while (enumerator.MoveNext())
        {
          sb.Append(separator);
          if (enumerator.Current != null)
            sb.Append(enumerator.Current);
        }
        return StringBuilderCache.GetStringAndRelease(sb);
      }
    }

    /// <summary>串联字符串数组的指定元素，其中在每个元素之间使用指定的分隔符。</summary>
    /// <returns>由 <paramref name="value" /> 中的字符串组成的字符串，这些字符串以 <paramref name="separator" /> 字符串分隔。- 或 -如果 <see cref="F:System.String.Empty" /> 为零，<paramref name="count" /> 没有元素，或 <paramref name="value" /> 以及 <paramref name="separator" /> 的全部元素均为 <paramref name="value" />，则为 <see cref="F:System.String.Empty" />。</returns>
    /// <param name="separator">要用作分隔符的字符串。只有在 <paramref name="separator" /> 具有多个元素时，<paramref name="value" /> 才包括在返回的字符串中。</param>
    /// <param name="value">一个数组，其中包含要连接的元素。</param>
    /// <param name="startIndex">
    /// <paramref name="value" /> 中要使用的第一个元素。</param>
    /// <param name="count">要使用的 <paramref name="value" /> 的元素数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 或 <paramref name="count" /> 小于 0。- 或 - <paramref name="startIndex" /> 加上 <paramref name="count" /> 中的元素数大于 <paramref name="value" />。</exception>
    /// <exception cref="T:System.OutOfMemoryException">内存不足。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe string Join(string separator, string[] value, int startIndex, int count)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
      if (startIndex > value.Length - count)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
      if (separator == null)
        separator = string.Empty;
      if (count == 0)
        return string.Empty;
      int num1 = 0;
      int num2 = startIndex + count - 1;
      for (int index = startIndex; index <= num2; ++index)
      {
        if (value[index] != null)
          num1 += value[index].Length;
      }
      int num3 = num1 + (count - 1) * separator.Length;
      if (num3 < 0 || num3 + 1 < 0)
        throw new OutOfMemoryException();
      if (num3 == 0)
        return string.Empty;
      string str = string.FastAllocateString(num3);
      fixed (char* buffer = &str.m_firstChar)
      {
        UnSafeCharBuffer unSafeCharBuffer = new UnSafeCharBuffer(buffer, num3);
        unSafeCharBuffer.AppendString(value[startIndex]);
        for (int index = startIndex + 1; index <= num2; ++index)
        {
          unSafeCharBuffer.AppendString(separator);
          unSafeCharBuffer.AppendString(value[index]);
        }
      }
      return str;
    }

    [SecuritySafeCritical]
    private static unsafe int CompareOrdinalIgnoreCaseHelper(string strA, string strB)
    {
      int num1 = Math.Min(strA.Length, strB.Length);
      fixed (char* chPtr1 = &strA.m_firstChar)
        fixed (char* chPtr2 = &strB.m_firstChar)
        {
          char* chPtr3 = chPtr1;
          char* chPtr4 = chPtr2;
          for (; num1 != 0; --num1)
          {
            int num2 = (int) *chPtr3;
            int num3 = (int) *chPtr4;
            if ((uint) (num2 - 97) <= 25U)
              num2 -= 32;
            if ((uint) (num3 - 97) <= 25U)
              num3 -= 32;
            if (num2 != num3)
              return num2 - num3;
            chPtr3 += 2;
            chPtr4 += 2;
          }
          return strA.Length - strB.Length;
        }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int nativeCompareOrdinalEx(string strA, int indexA, string strB, int indexB, int count);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern unsafe int nativeCompareOrdinalIgnoreCaseWC(string strA, sbyte* strBBytes);

    [SecuritySafeCritical]
    internal static unsafe string SmallCharToUpper(string strIn)
    {
      int length = strIn.Length;
      string str = string.FastAllocateString(length);
      fixed (char* chPtr1 = &strIn.m_firstChar)
        fixed (char* chPtr2 = &str.m_firstChar)
        {
          for (int index = 0; index < length; ++index)
          {
            int num = (int) chPtr1[index];
            if ((uint) (num - 97) <= 25U)
              num -= 32;
            chPtr2[index] = (char) num;
          }
        }
      return str;
    }

    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    private static unsafe bool EqualsHelper(string strA, string strB)
    {
      int length = strA.Length;
      fixed (char* chPtr1 = &strA.m_firstChar)
        fixed (char* chPtr2 = &strB.m_firstChar)
        {
          char* chPtr3 = chPtr1;
          char* chPtr4 = chPtr2;
          while (length >= 10)
          {
            if (*(int*) chPtr3 != *(int*) chPtr4 || *(int*) (chPtr3 + 2) != *(int*) (chPtr4 + 2) || (*(int*) (chPtr3 + 4) != *(int*) (chPtr4 + 4) || *(int*) (chPtr3 + 6) != *(int*) (chPtr4 + 6)) || *(int*) (chPtr3 + 8) != *(int*) (chPtr4 + 8))
              return false;
            chPtr3 += 10;
            chPtr4 += 10;
            length -= 10;
          }
          while (length > 0 && *(int*) chPtr3 == *(int*) chPtr4)
          {
            chPtr3 += 2;
            chPtr4 += 2;
            length -= 2;
          }
          return length <= 0;
        }
    }

    [SecuritySafeCritical]
    private static unsafe int CompareOrdinalHelper(string strA, string strB)
    {
      int num1 = Math.Min(strA.Length, strB.Length);
      int num2 = -1;
      fixed (char* chPtr1 = &strA.m_firstChar)
        fixed (char* chPtr2 = &strB.m_firstChar)
        {
          char* chPtr3 = chPtr1;
          char* chPtr4 = chPtr2;
          while (num1 >= 10)
          {
            if (*(int*) chPtr3 != *(int*) chPtr4)
            {
              num2 = 0;
              break;
            }
            if (*(int*) (chPtr3 + 2) != *(int*) (chPtr4 + 2))
            {
              num2 = 2;
              break;
            }
            if (*(int*) (chPtr3 + 4) != *(int*) (chPtr4 + 4))
            {
              num2 = 4;
              break;
            }
            if (*(int*) (chPtr3 + 6) != *(int*) (chPtr4 + 6))
            {
              num2 = 6;
              break;
            }
            if (*(int*) (chPtr3 + 8) != *(int*) (chPtr4 + 8))
            {
              num2 = 8;
              break;
            }
            chPtr3 += 10;
            chPtr4 += 10;
            num1 -= 10;
          }
          if (num2 != -1)
          {
            char* chPtr5 = chPtr3 + num2;
            char* chPtr6 = chPtr4 + num2;
            int num3;
            if ((num3 = (int) *chPtr5 - (int) *chPtr6) != 0)
              return num3;
            return (int) *(ushort*) ((IntPtr) chPtr5 + 2) - (int) *(ushort*) ((IntPtr) chPtr6 + 2);
          }
          while (num1 > 0 && *(int*) chPtr3 == *(int*) chPtr4)
          {
            chPtr3 += 2;
            chPtr4 += 2;
            num1 -= 2;
          }
          if (num1 <= 0)
            return strA.Length - strB.Length;
          int num4;
          if ((num4 = (int) *chPtr3 - (int) *chPtr4) != 0)
            return num4;
          return (int) *(ushort*) ((IntPtr) chPtr3 + 2) - (int) *(ushort*) ((IntPtr) chPtr4 + 2);
        }
    }

    /// <summary>确定此实例是否与指定的对象（也必须是 <see cref="T:System.String" /> 对象）具有相同的值。</summary>
    /// <returns>如果 true 是一个 <paramref name="obj" /> 且其值与此实例相等，则为 <see cref="T:System.String" />；否则为 false。如果 <paramref name="obj" /> 为 null，则此方法返回 false。</returns>
    /// <param name="obj">要与此实例进行比较的字符串。</param>
    /// <filterpriority>2</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (this == null)
        throw new NullReferenceException();
      string strB = obj as string;
      if (strB == null)
        return false;
      if (this == obj)
        return true;
      if (this.Length != strB.Length)
        return false;
      return string.EqualsHelper(this, strB);
    }

    /// <summary>确定此实例是否与另一个指定的 <see cref="T:System.String" /> 对象具有相同的值。</summary>
    /// <returns>如果 true 参数的值与此实例的值相同，则为 <paramref name="value" />；否则为 false。如果 <paramref name="value" /> 为 null，则此方法返回 false。</returns>
    /// <param name="value">要与此实例进行比较的字符串。</param>
    /// <filterpriority>2</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public bool Equals(string value)
    {
      if (this == null)
        throw new NullReferenceException();
      if (value == null)
        return false;
      if (this == value)
        return true;
      if (this.Length != value.Length)
        return false;
      return string.EqualsHelper(this, value);
    }

    /// <summary>确定此字符串是否与另一个指定的 <see cref="T:System.String" /> 对象具有相同的值。参数指定区域性、大小写以及比较所用的排序规则。</summary>
    /// <returns>如果 true 参数的值与此字符串相同，则为 <paramref name="value" />；否则为 false。</returns>
    /// <param name="value">要与此实例进行比较的字符串。</param>
    /// <param name="comparisonType">枚举值之一，用于指定如何比较字符串。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="comparisonType" /> 不是 <see cref="T:System.StringComparison" /> 值。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public bool Equals(string value, StringComparison comparisonType)
    {
      if (comparisonType < StringComparison.CurrentCulture || comparisonType > StringComparison.OrdinalIgnoreCase)
        throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
      if (this == value)
        return true;
      if (value == null)
        return false;
      switch (comparisonType)
      {
        case StringComparison.CurrentCulture:
          return CultureInfo.CurrentCulture.CompareInfo.Compare(this, value, CompareOptions.None) == 0;
        case StringComparison.CurrentCultureIgnoreCase:
          return CultureInfo.CurrentCulture.CompareInfo.Compare(this, value, CompareOptions.IgnoreCase) == 0;
        case StringComparison.InvariantCulture:
          return CultureInfo.InvariantCulture.CompareInfo.Compare(this, value, CompareOptions.None) == 0;
        case StringComparison.InvariantCultureIgnoreCase:
          return CultureInfo.InvariantCulture.CompareInfo.Compare(this, value, CompareOptions.IgnoreCase) == 0;
        case StringComparison.Ordinal:
          if (this.Length != value.Length)
            return false;
          return string.EqualsHelper(this, value);
        case StringComparison.OrdinalIgnoreCase:
          if (this.Length != value.Length)
            return false;
          if (this.IsAscii() && value.IsAscii())
            return string.CompareOrdinalIgnoreCaseHelper(this, value) == 0;
          return TextInfo.CompareOrdinalIgnoreCase(this, value) == 0;
        default:
          throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
      }
    }

    /// <summary>确定两个指定的 <see cref="T:System.String" /> 对象是否具有相同的值。</summary>
    /// <returns>如果 true 的值与 <paramref name="a" /> 的值相同，则为 <paramref name="b" />；否则为 false。如果 <paramref name="a" /> 和 <paramref name="b" /> 均为 null，此方法将返回 true。</returns>
    /// <param name="a">要比较的第一个字符串，或 null。</param>
    /// <param name="b">要比较的第二个字符串，或 null。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool Equals(string a, string b)
    {
      if (a == b)
        return true;
      if (a == null || b == null || a.Length != b.Length)
        return false;
      return string.EqualsHelper(a, b);
    }

    /// <summary>确定两个指定的 <see cref="T:System.String" /> 对象是否具有相同的值。参数指定区域性、大小写以及比较所用的排序规则。</summary>
    /// <returns>如果 true 参数的值与 <paramref name="a" /> 参数的值相同，则为 <paramref name="b" />；否则为 false。</returns>
    /// <param name="a">要比较的第一个字符串，或 null。</param>
    /// <param name="b">要比较的第二个字符串，或 null。</param>
    /// <param name="comparisonType">枚举值之一，用于指定比较的规则。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="comparisonType" /> 不是 <see cref="T:System.StringComparison" /> 值。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static bool Equals(string a, string b, StringComparison comparisonType)
    {
      if (comparisonType < StringComparison.CurrentCulture || comparisonType > StringComparison.OrdinalIgnoreCase)
        throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
      if (a == b)
        return true;
      if (a == null || b == null)
        return false;
      switch (comparisonType)
      {
        case StringComparison.CurrentCulture:
          return CultureInfo.CurrentCulture.CompareInfo.Compare(a, b, CompareOptions.None) == 0;
        case StringComparison.CurrentCultureIgnoreCase:
          return CultureInfo.CurrentCulture.CompareInfo.Compare(a, b, CompareOptions.IgnoreCase) == 0;
        case StringComparison.InvariantCulture:
          return CultureInfo.InvariantCulture.CompareInfo.Compare(a, b, CompareOptions.None) == 0;
        case StringComparison.InvariantCultureIgnoreCase:
          return CultureInfo.InvariantCulture.CompareInfo.Compare(a, b, CompareOptions.IgnoreCase) == 0;
        case StringComparison.Ordinal:
          if (a.Length != b.Length)
            return false;
          return string.EqualsHelper(a, b);
        case StringComparison.OrdinalIgnoreCase:
          if (a.Length != b.Length)
            return false;
          if (a.IsAscii() && b.IsAscii())
            return string.CompareOrdinalIgnoreCaseHelper(a, b) == 0;
          return TextInfo.CompareOrdinalIgnoreCase(a, b) == 0;
        default:
          throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
      }
    }

    /// <summary>将指定数目的字符从此实例中的指定位置复制到 Unicode 字符数组中的指定位置。</summary>
    /// <param name="sourceIndex">要复制的此实例中第一个字符的索引。</param>
    /// <param name="destination">此实例中的字符所复制到的 Unicode 字符数组。</param>
    /// <param name="destinationIndex">
    /// <paramref name="destination" /> 中的索引，在此处开始复制操作。</param>
    /// <param name="count">此实例中要复制到 <paramref name="destination" /> 的字符数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destination" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="sourceIndex" />, <paramref name="destinationIndex" />, ，或 <paramref name="count" /> 为负 - 或 - <paramref name="sourceIndex" /> 并不标识当前实例中的位置。- 或 -<paramref name="destinationIndex" /> 不能标识中的有效索引 <paramref name="destination" /> 数组。- 或 -<paramref name="count" /> 从子字符串的长度大于 <paramref name="startIndex" /> 到此实例的末尾 - 或 - <paramref name="count" /> 从子数组的长度大于 <paramref name="destinationIndex" /> 到末尾 <paramref name="destination" /> 数组。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
    {
      if (destination == null)
        throw new ArgumentNullException("destination");
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
      if (sourceIndex < 0)
        throw new ArgumentOutOfRangeException("sourceIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count > this.Length - sourceIndex)
        throw new ArgumentOutOfRangeException("sourceIndex", Environment.GetResourceString("ArgumentOutOfRange_IndexCount"));
      if (destinationIndex > destination.Length - count || destinationIndex < 0)
        throw new ArgumentOutOfRangeException("destinationIndex", Environment.GetResourceString("ArgumentOutOfRange_IndexCount"));
      if (count <= 0)
        return;
      fixed (char* chPtr1 = &this.m_firstChar)
        fixed (char* chPtr2 = destination)
          string.wstrcpy(chPtr2 + destinationIndex, chPtr1 + sourceIndex, count);
    }

    /// <summary>将此实例中的字符复制到 Unicode 字符数组。</summary>
    /// <returns>元素为此实例的各字符的 Unicode 字符数组。如果此实例是空字符串，则返回的数组为空且长度为零。</returns>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe char[] ToCharArray()
    {
      int length = this.Length;
      char[] chArray = new char[length];
      if (length > 0)
      {
        fixed (char* smem = &this.m_firstChar)
          fixed (char* dmem = chArray)
            string.wstrcpy(dmem, smem, length);
      }
      return chArray;
    }

    /// <summary>将此实例中的指定子字符串内的字符复制到 Unicode 字符数组。</summary>
    /// <returns>元素为此实例中从字符位置 <paramref name="length" /> 开始的 <paramref name="startIndex" /> 字符数的 Unicode 字符数组。</returns>
    /// <param name="startIndex">此实例内子字符串的起始位置。</param>
    /// <param name="length">此实例内子字符串的长度。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 或 <paramref name="length" /> 小于零。- 或 - <paramref name="startIndex" /> 加上 <paramref name="length" /> 大于此实例的长度。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe char[] ToCharArray(int startIndex, int length)
    {
      if (startIndex < 0 || startIndex > this.Length || startIndex > this.Length - length)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (length < 0)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      char[] chArray = new char[length];
      if (length > 0)
      {
        fixed (char* chPtr = &this.m_firstChar)
          fixed (char* dmem = chArray)
            string.wstrcpy(dmem, chPtr + startIndex, length);
      }
      return chArray;
    }

    /// <summary>指示指定的字符串是 null 还是 <see cref="F:System.String.Empty" /> 字符串。</summary>
    /// <returns>如果 true 参数为 <paramref name="value" /> 或空字符串 ("")，则为 null；否则为 false。</returns>
    /// <param name="value">要测试的字符串。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool IsNullOrEmpty(string value)
    {
      if (value != null)
        return value.Length == 0;
      return true;
    }

    /// <summary>指示指定的字符串是 null、空还是仅由空白字符组成。</summary>
    /// <returns>如果 true 参数为 <paramref name="value" /> 或 null，或者如果 <see cref="F:System.String.Empty" /> 仅由空白字符组成，则为 <paramref name="value" />。</returns>
    /// <param name="value">要测试的字符串。</param>
    [__DynamicallyInvokable]
    public static bool IsNullOrWhiteSpace(string value)
    {
      if (value == null)
        return true;
      for (int index = 0; index < value.Length; ++index)
      {
        if (!char.IsWhiteSpace(value[index]))
          return false;
      }
      return true;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int InternalMarvin32HashString(string s, int strLen, long additionalEntropy);

    [SecuritySafeCritical]
    internal static bool UseRandomizedHashing()
    {
      return string.InternalUseRandomizedHashing();
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern bool InternalUseRandomizedHashing();

    /// <summary>返回该字符串的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    public override unsafe int GetHashCode()
    {
      if (HashHelpers.s_UseRandomizedStringHashing)
        return string.InternalMarvin32HashString(this, this.Length, 0L);
      string str = this;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      int num1 = 352654597;
      int num2 = num1;
      int* numPtr = (int*) chPtr;
      int length = this.Length;
      while (length > 2)
      {
        num1 = (num1 << 5) + num1 + (num1 >> 27) ^ *numPtr;
        num2 = (num2 << 5) + num2 + (num2 >> 27) ^ *(int*) ((IntPtr) numPtr + 4);
        numPtr += 2;
        length -= 4;
      }
      if (length > 0)
        num1 = (num1 << 5) + num1 + (num1 >> 27) ^ *numPtr;
      return num1 + num2 * 1566083941;
    }

    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    internal unsafe int GetLegacyNonRandomizedHashCode()
    {
      string str = this;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      int num1 = 352654597;
      int num2 = num1;
      int* numPtr = (int*) chPtr;
      int length = this.Length;
      while (length > 2)
      {
        num1 = (num1 << 5) + num1 + (num1 >> 27) ^ *numPtr;
        num2 = (num2 << 5) + num2 + (num2 >> 27) ^ *(int*) ((IntPtr) numPtr + 4);
        numPtr += 2;
        length -= 4;
      }
      if (length > 0)
        num1 = (num1 << 5) + num1 + (num1 >> 27) ^ *numPtr;
      return num1 + num2 * 1566083941;
    }

    /// <summary>基于数组中的字符将字符串拆分为多个子字符串。</summary>
    /// <returns>一个数组，其元素包含此实例中的子字符串，这些子字符串由 <paramref name="separator" /> 中的一个或多个字符分隔。有关详细信息，请参阅“备注”部分。</returns>
    /// <param name="separator">分隔此字符串中子字符串的字符数组、不包含分隔符的空数组或 null。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public string[] Split(params char[] separator)
    {
      return this.SplitInternal(separator, int.MaxValue, StringSplitOptions.None);
    }

    /// <summary>基于数组中的字符将一个字符串拆分成最大数量的子字符串。也可指定要返回的子字符串的最大数量。</summary>
    /// <returns>一个数组，其元素包含此实例中的子字符串，这些子字符串由 <paramref name="separator" /> 中的一个或多个字符分隔。有关详细信息，请参阅“备注”部分。</returns>
    /// <param name="separator">分隔此字符串中子字符串的字符数组、不包含分隔符的空数组或 null。</param>
    /// <param name="count">要返回的子字符串的最大数量。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 为负。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public string[] Split(char[] separator, int count)
    {
      return this.SplitInternal(separator, count, StringSplitOptions.None);
    }

    /// <summary>基于数组中的字符将字符串拆分为多个子字符串。可以指定子字符串是否包含空数组元素。</summary>
    /// <returns>一个数组，其元素包含此字符串中的子字符串，这些子字符串由 <paramref name="separator" /> 中的一个或多个字符分隔。有关详细信息，请参阅“备注”部分。</returns>
    /// <param name="separator">分隔此字符串中子字符串的字符数组、不包含分隔符的空数组或 null。</param>
    /// <param name="options">要省略返回的数组中的空数组元素，则为 <see cref="F:System.StringSplitOptions.RemoveEmptyEntries" />；要包含返回的数组中的空数组元素，则为 <see cref="F:System.StringSplitOptions.None" />。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 不是之一 <see cref="T:System.StringSplitOptions" /> 值。</exception>
    /// <filterpriority>1</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public string[] Split(char[] separator, StringSplitOptions options)
    {
      return this.SplitInternal(separator, int.MaxValue, options);
    }

    /// <summary>基于数组中的字符将一个字符串拆分成最大数量的子字符串。 </summary>
    /// <returns>一个数组，其元素包含此字符串中的子字符串，这些子字符串由 <paramref name="separator" /> 中的一个或多个字符分隔。有关详细信息，请参阅“备注”部分。</returns>
    /// <param name="separator">分隔此字符串中子字符串的字符数组、不包含分隔符的空数组或 null。</param>
    /// <param name="count">要返回的子字符串的最大数量。</param>
    /// <param name="options">要省略返回的数组中的空数组元素，则为 <see cref="F:System.StringSplitOptions.RemoveEmptyEntries" />；要包含返回的数组中的空数组元素，则为 <see cref="F:System.StringSplitOptions.None" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 不是之一 <see cref="T:System.StringSplitOptions" /> 值。</exception>
    /// <filterpriority>1</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public string[] Split(char[] separator, int count, StringSplitOptions options)
    {
      return this.SplitInternal(separator, count, options);
    }

    [ComVisible(false)]
    internal string[] SplitInternal(char[] separator, int count, StringSplitOptions options)
    {
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
      if (options < StringSplitOptions.None || options > StringSplitOptions.RemoveEmptyEntries)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) options));
      bool flag = options == StringSplitOptions.RemoveEmptyEntries;
      if (count == 0 || flag && this.Length == 0)
        return new string[0];
      int[] sepList = new int[this.Length];
      int numReplaces = this.MakeSeparatorList(separator, ref sepList);
      if (numReplaces == 0 || count == 1)
        return new string[1]{ this };
      if (flag)
        return this.InternalSplitOmitEmptyEntries(sepList, (int[]) null, numReplaces, count);
      return this.InternalSplitKeepEmptyEntries(sepList, (int[]) null, numReplaces, count);
    }

    /// <summary>基于数组中的字符串将字符串拆分为多个子字符串。可以指定子字符串是否包含空数组元素。</summary>
    /// <returns>一个数组，其元素包含此字符串中的子字符串，这些子字符串由 <paramref name="separator" /> 中的一个或多个字符串分隔。有关详细信息，请参阅“备注”部分。</returns>
    /// <param name="separator">分隔此字符串中子字符串的字符串数组、不包含分隔符的空数组或 null。</param>
    /// <param name="options">要省略返回的数组中的空数组元素，则为 <see cref="F:System.StringSplitOptions.RemoveEmptyEntries" />；要包含返回的数组中的空数组元素，则为 <see cref="F:System.StringSplitOptions.None" />。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 不是之一 <see cref="T:System.StringSplitOptions" /> 值。</exception>
    /// <filterpriority>1</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public string[] Split(string[] separator, StringSplitOptions options)
    {
      return this.Split(separator, int.MaxValue, options);
    }

    /// <summary>基于数组中的字符串将一个字符串拆分成最大数量的子字符串。可以指定子字符串是否包含空数组元素。</summary>
    /// <returns>一个数组，其元素包含此字符串中的子字符串，这些子字符串由 <paramref name="separator" /> 中的一个或多个字符串分隔。有关详细信息，请参阅“备注”部分。</returns>
    /// <param name="separator">分隔此字符串中子字符串的字符串数组、不包含分隔符的空数组或 null。</param>
    /// <param name="count">要返回的子字符串的最大数量。</param>
    /// <param name="options">要省略返回的数组中的空数组元素，则为 <see cref="F:System.StringSplitOptions.RemoveEmptyEntries" />；要包含返回的数组中的空数组元素，则为 <see cref="F:System.StringSplitOptions.None" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 不是之一 <see cref="T:System.StringSplitOptions" /> 值。</exception>
    /// <filterpriority>1</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public string[] Split(string[] separator, int count, StringSplitOptions options)
    {
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
      if (options < StringSplitOptions.None || options > StringSplitOptions.RemoveEmptyEntries)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) options));
      bool flag = options == StringSplitOptions.RemoveEmptyEntries;
      if (separator == null || separator.Length == 0)
        return this.SplitInternal((char[]) null, count, options);
      if (count == 0 || flag && this.Length == 0)
        return new string[0];
      int[] sepList = new int[this.Length];
      int[] lengthList = new int[this.Length];
      int numReplaces = this.MakeSeparatorList(separator, ref sepList, ref lengthList);
      if (numReplaces == 0 || count == 1)
        return new string[1]{ this };
      if (flag)
        return this.InternalSplitOmitEmptyEntries(sepList, lengthList, numReplaces, count);
      return this.InternalSplitKeepEmptyEntries(sepList, lengthList, numReplaces, count);
    }

    private string[] InternalSplitKeepEmptyEntries(int[] sepList, int[] lengthList, int numReplaces, int count)
    {
      int startIndex = 0;
      int index1 = 0;
      --count;
      int num = numReplaces < count ? numReplaces : count;
      string[] strArray = new string[num + 1];
      for (int index2 = 0; index2 < num && startIndex < this.Length; ++index2)
      {
        strArray[index1++] = this.Substring(startIndex, sepList[index2] - startIndex);
        startIndex = sepList[index2] + (lengthList == null ? 1 : lengthList[index2]);
      }
      if (startIndex < this.Length && num >= 0)
        strArray[index1] = this.Substring(startIndex);
      else if (index1 == num)
        strArray[index1] = string.Empty;
      return strArray;
    }

    private string[] InternalSplitOmitEmptyEntries(int[] sepList, int[] lengthList, int numReplaces, int count)
    {
      int length1 = numReplaces < count ? numReplaces + 1 : count;
      string[] strArray1 = new string[length1];
      int startIndex = 0;
      int length2 = 0;
      for (int index = 0; index < numReplaces && startIndex < this.Length; ++index)
      {
        if (sepList[index] - startIndex > 0)
          strArray1[length2++] = this.Substring(startIndex, sepList[index] - startIndex);
        startIndex = sepList[index] + (lengthList == null ? 1 : lengthList[index]);
        if (length2 == count - 1)
        {
          while (index < numReplaces - 1 && startIndex == sepList[++index])
            startIndex += lengthList == null ? 1 : lengthList[index];
          break;
        }
      }
      if (startIndex < this.Length)
        strArray1[length2++] = this.Substring(startIndex);
      string[] strArray2 = strArray1;
      if (length2 != length1)
      {
        strArray2 = new string[length2];
        for (int index = 0; index < length2; ++index)
          strArray2[index] = strArray1[index];
      }
      return strArray2;
    }

    [SecuritySafeCritical]
    private unsafe int MakeSeparatorList(char[] separator, ref int[] sepList)
    {
      int num1 = 0;
      if (separator == null || separator.Length == 0)
      {
        fixed (char* chPtr = &this.m_firstChar)
        {
          for (int index = 0; index < this.Length && num1 < sepList.Length; ++index)
          {
            if (char.IsWhiteSpace(chPtr[index]))
              sepList[num1++] = index;
          }
        }
      }
      else
      {
        int length1 = sepList.Length;
        int length2 = separator.Length;
        fixed (char* chPtr1 = &this.m_firstChar)
          fixed (char* chPtr2 = separator)
          {
            for (int index = 0; index < this.Length && num1 < length1; ++index)
            {
              char* chPtr3 = chPtr2;
              int num2 = 0;
              while (num2 < length2)
              {
                if ((int) chPtr1[index] == (int) *chPtr3)
                {
                  sepList[num1++] = index;
                  break;
                }
                ++num2;
                chPtr3 += 2;
              }
            }
          }
      }
      return num1;
    }

    [SecuritySafeCritical]
    private unsafe int MakeSeparatorList(string[] separators, ref int[] sepList, ref int[] lengthList)
    {
      int index1 = 0;
      int length1 = sepList.Length;
      int length2 = separators.Length;
      fixed (char* chPtr = &this.m_firstChar)
      {
        for (int indexA = 0; indexA < this.Length && index1 < length1; ++indexA)
        {
          for (int index2 = 0; index2 < separators.Length; ++index2)
          {
            string strB = separators[index2];
            if (!string.IsNullOrEmpty(strB))
            {
              int length3 = strB.Length;
              if ((int) chPtr[indexA] == (int) strB[0] && length3 <= this.Length - indexA && (length3 == 1 || string.CompareOrdinal(this, indexA, strB, 0, length3) == 0))
              {
                sepList[index1] = indexA;
                lengthList[index1] = length3;
                ++index1;
                indexA += length3 - 1;
                break;
              }
            }
          }
        }
      }
      return index1;
    }

    /// <summary>从此实例检索子字符串。子字符串在指定的字符位置开始并一直到该字符串的末尾。</summary>
    /// <returns>与此实例中在 <paramref name="startIndex" /> 处开头的子字符串等效的一个字符串；如果 <see cref="F:System.String.Empty" /> 等于此实例的长度，则为 <paramref name="startIndex" />。</returns>
    /// <param name="startIndex">此实例中子字符串的起始字符位置（从零开始）。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 小于零或大于此实例的长度。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public string Substring(int startIndex)
    {
      return this.Substring(startIndex, this.Length - startIndex);
    }

    /// <summary>从此实例检索子字符串。子字符串从指定的字符位置开始且具有指定的长度。</summary>
    /// <returns>与此实例中在 <paramref name="length" /> 处开头、长度为 <paramref name="startIndex" /> 的子字符串等效的一个字符串；如果 <see cref="F:System.String.Empty" /> 等于此实例的长度且 <paramref name="startIndex" /> 为零，则为 <paramref name="length" />。</returns>
    /// <param name="startIndex">此实例中子字符串的起始字符位置（从零开始）。</param>
    /// <param name="length">子字符串中的字符数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 加上 <paramref name="length" /> 指示的位置不在此实例。- 或 - <paramref name="startIndex" /> 或 <paramref name="length" /> 小于零。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string Substring(int startIndex, int length)
    {
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (startIndex > this.Length)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndexLargerThanLength"));
      if (length < 0)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
      if (startIndex > this.Length - length)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_IndexLength"));
      if (length == 0)
        return string.Empty;
      if (startIndex == 0 && length == this.Length)
        return this;
      return this.InternalSubString(startIndex, length);
    }

    [SecurityCritical]
    private unsafe string InternalSubString(int startIndex, int length)
    {
      string str = string.FastAllocateString(length);
      fixed (char* dmem = &str.m_firstChar)
        fixed (char* chPtr = &this.m_firstChar)
          string.wstrcpy(dmem, chPtr + startIndex, length);
      return str;
    }

    /// <summary>从当前 <see cref="T:System.String" /> 对象移除数组中指定的一组字符的所有前导匹配项和尾部匹配项。</summary>
    /// <returns>从当前字符串的开头移除所出现的所有 <paramref name="trimChars" /> 参数中的字符后剩余的字符串。如果 <paramref name="trimChars" /> 为 null 或空数组，则改为移除空白字符。如果从当前实例无法删除字符，此方法返回未更改的当前实例。</returns>
    /// <param name="trimChars">要删除的 Unicode 字符的数组，或 null。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public string Trim(params char[] trimChars)
    {
      if (trimChars == null || trimChars.Length == 0)
        return this.TrimHelper(2);
      return this.TrimHelper(trimChars, 2);
    }

    /// <summary>从当前 <see cref="T:System.String" /> 对象移除数组中指定的一组字符的所有前导匹配项。</summary>
    /// <returns>从当前字符串的开头移除所出现的所有 <paramref name="trimChars" /> 参数中的字符后剩余的字符串。如果 <paramref name="trimChars" /> 为 null 或空数组，则改为移除空白字符。</returns>
    /// <param name="trimChars">要删除的 Unicode 字符的数组，或 null。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public string TrimStart(params char[] trimChars)
    {
      if (trimChars == null || trimChars.Length == 0)
        return this.TrimHelper(0);
      return this.TrimHelper(trimChars, 0);
    }

    /// <summary>从当前 <see cref="T:System.String" /> 对象移除数组中指定的一组字符的所有尾部匹配项。</summary>
    /// <returns>从当前字符串的开头移除所出现的所有 <paramref name="trimChars" /> 参数中的字符后剩余的字符串。如果 <paramref name="trimChars" /> 为 null 或空数组，则改为删除 Unicode 空白字符。如果从当前实例无法删除字符，此方法返回未更改的当前实例。</returns>
    /// <param name="trimChars">要删除的 Unicode 字符的数组，或 null。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public string TrimEnd(params char[] trimChars)
    {
      if (trimChars == null || trimChars.Length == 0)
        return this.TrimHelper(1);
      return this.TrimHelper(trimChars, 1);
    }

    [SecurityCritical]
    private static unsafe string CreateString(sbyte* value, int startIndex, int length, Encoding enc)
    {
      if (enc == null)
        return new string(value, startIndex, length);
      if (length < 0)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (value + startIndex < value)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_PartialWCHAR"));
      byte[] numArray = new byte[length];
      try
      {
        Buffer.Memcpy(numArray, 0, (byte*) value, startIndex, length);
      }
      catch (NullReferenceException ex)
      {
        throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_PartialWCHAR"));
      }
      return enc.GetString(numArray);
    }

    [SecurityCritical]
    internal static unsafe string CreateStringFromEncoding(byte* bytes, int byteLength, Encoding encoding)
    {
      int charCount = encoding.GetCharCount(bytes, byteLength, (DecoderNLS) null);
      if (charCount == 0)
        return string.Empty;
      string str = string.FastAllocateString(charCount);
      fixed (char* chars = &str.m_firstChar)
        encoding.GetChars(bytes, byteLength, chars, charCount, (DecoderNLS) null);
      return str;
    }

    [SecuritySafeCritical]
    internal unsafe int ConvertToAnsi(byte* pbNativeBuffer, int cbNativeBuffer, bool fBestFit, bool fThrowOnUnmappableChar)
    {
      uint flags = fBestFit ? 0U : 1024U;
      uint num = 0;
      int multiByte;
      fixed (char* pwzSource = &this.m_firstChar)
        multiByte = Win32Native.WideCharToMultiByte(0U, flags, pwzSource, this.Length, pbNativeBuffer, cbNativeBuffer, IntPtr.Zero, fThrowOnUnmappableChar ? new IntPtr((void*) &num) : IntPtr.Zero);
      if ((int) num != 0)
        throw new ArgumentException(Environment.GetResourceString("Interop_Marshal_Unmappable_Char"));
      pbNativeBuffer[multiByte] = (byte) 0;
      return multiByte;
    }

    /// <summary>指示此字符串是否符合 Unicode 范式 C。</summary>
    /// <returns>如果此字符串符合范式 C，则为 true；否则为 false。</returns>
    /// <exception cref="T:System.ArgumentException">当前实例包含无效的 Unicode 字符。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public bool IsNormalized()
    {
      return this.IsNormalized(NormalizationForm.FormC);
    }

    /// <summary>指示此字符串是否符合指定的 Unicode 范式。</summary>
    /// <returns>如果此字符串符合由 true 参数指定的范式，则为 <paramref name="normalizationForm" />；否则为 false。</returns>
    /// <param name="normalizationForm">一个 Unicode 范式。</param>
    /// <exception cref="T:System.ArgumentException">当前实例包含无效的 Unicode 字符。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public bool IsNormalized(NormalizationForm normalizationForm)
    {
      if (this.IsFastSort() && (normalizationForm == NormalizationForm.FormC || normalizationForm == NormalizationForm.FormKC || (normalizationForm == NormalizationForm.FormD || normalizationForm == NormalizationForm.FormKD)))
        return true;
      return Normalization.IsNormalized(this, normalizationForm);
    }

    /// <summary>返回一个新字符串，其文本值与此字符串相同，但其二进制表示形式符合 Unicode 范式 C。</summary>
    /// <returns>一个新的规范化字符串，其文本值与此字符串相同，但其二进制表示形式符合范式 C。</returns>
    /// <exception cref="T:System.ArgumentException">当前实例包含无效的 Unicode 字符。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public string Normalize()
    {
      return this.Normalize(NormalizationForm.FormC);
    }

    /// <summary>返回一个新字符串，其文本值与此字符串相同，但其二进制表示形式符合指定的 Unicode 范式。</summary>
    /// <returns>一个新字符串，其文本值与此字符串相同，但其二进制表示形式符合由 <paramref name="normalizationForm" /> 参数指定的范式。</returns>
    /// <param name="normalizationForm">一个 Unicode 范式。</param>
    /// <exception cref="T:System.ArgumentException">当前实例包含无效的 Unicode 字符。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public string Normalize(NormalizationForm normalizationForm)
    {
      if (this.IsAscii() && (normalizationForm == NormalizationForm.FormC || normalizationForm == NormalizationForm.FormKC || (normalizationForm == NormalizationForm.FormD || normalizationForm == NormalizationForm.FormKD)))
        return this;
      return Normalization.Normalize(this, normalizationForm);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string FastAllocateString(int length);

    [SecuritySafeCritical]
    private static unsafe void FillStringChecked(string dest, int destPos, string src)
    {
      if (src.Length > dest.Length - destPos)
        throw new IndexOutOfRangeException();
      fixed (char* chPtr = &dest.m_firstChar)
        fixed (char* smem = &src.m_firstChar)
          string.wstrcpy(chPtr + destPos, smem, src.Length);
    }

    [SecurityCritical]
    internal static unsafe void wstrcpy(char* dmem, char* smem, int charCount)
    {
      Buffer.Memcpy((byte*) dmem, (byte*) smem, charCount * 2);
    }

    [SecuritySafeCritical]
    private unsafe string CtorCharArray(char[] value)
    {
      if (value == null || value.Length == 0)
        return string.Empty;
      string str1;
      string str2 = str1 = string.FastAllocateString(value.Length);
      char* dmem = (char*) str2;
      if ((IntPtr) dmem != IntPtr.Zero)
        dmem += RuntimeHelpers.OffsetToStringData;
      fixed (char* smem = value)
      {
        string.wstrcpy(dmem, smem, value.Length);
        str2 = (string) null;
      }
      return str1;
    }

    [SecuritySafeCritical]
    private unsafe string CtorCharArrayStartLength(char[] value, int startIndex, int length)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (length < 0)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
      if (startIndex > value.Length - length)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (length <= 0)
        return string.Empty;
      string str1;
      string str2 = str1 = string.FastAllocateString(length);
      char* dmem = (char*) str2;
      if ((IntPtr) dmem != IntPtr.Zero)
        dmem += RuntimeHelpers.OffsetToStringData;
      fixed (char* chPtr = value)
      {
        string.wstrcpy(dmem, chPtr + startIndex, length);
        str2 = (string) null;
      }
      return str1;
    }

    [SecuritySafeCritical]
    private unsafe string CtorCharCount(char c, int count)
    {
      if (count > 0)
      {
        string str1 = string.FastAllocateString(count);
        if ((int) c != 0)
        {
          string str2 = str1;
          char* chPtr1 = (char*) str2;
          if ((IntPtr) chPtr1 != IntPtr.Zero)
            chPtr1 += RuntimeHelpers.OffsetToStringData;
          char* chPtr2;
          for (chPtr2 = chPtr1; ((int) (uint) chPtr2 & 3) != 0 && count > 0; --count)
            *chPtr2++ = c;
          uint num = (uint) c << 16 | (uint) c;
          if (count >= 4)
          {
            count -= 4;
            do
            {
              *(int*) chPtr2 = (int) num;
              *(int*) ((IntPtr) chPtr2 + 4) = (int) num;
              chPtr2 += 4;
              count -= 4;
            }
            while (count >= 0);
          }
          if ((count & 2) != 0)
          {
            *(int*) chPtr2 = (int) num;
            chPtr2 += 2;
          }
          if ((count & 1) != 0)
            *chPtr2 = c;
          str2 = (string) null;
        }
        return str1;
      }
      if (count == 0)
        return string.Empty;
      throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_MustBeNonNegNum", (object) "count"));
    }

    [SecurityCritical]
    private static unsafe int wcslen(char* ptr)
    {
      char* chPtr = ptr;
      while (((int) (uint) chPtr & 3) != 0 && (int) *chPtr != 0)
        chPtr += 2;
      if ((int) *chPtr != 0)
      {
        while (((int) *chPtr & (int) *(ushort*) ((IntPtr) chPtr + 2)) != 0 || (int) *chPtr != 0 && (int) *(ushort*) ((IntPtr) chPtr + 2) != 0)
          chPtr += 2;
      }
      while ((int) *chPtr != 0)
        chPtr += 2;
      return (int) (chPtr - ptr);
    }

    [SecurityCritical]
    private unsafe string CtorCharPtr(char* ptr)
    {
      if ((IntPtr) ptr == IntPtr.Zero)
        return string.Empty;
      if ((UIntPtr) ptr < new UIntPtr(64000))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeStringPtrNotAtom"));
      try
      {
        int num = string.wcslen(ptr);
        if (num == 0)
          return string.Empty;
        string str1 = string.FastAllocateString(num);
        string str2;
        try
        {
          str2 = str1;
          char* dmem = (char*) str2;
          if ((IntPtr) dmem != IntPtr.Zero)
            dmem += RuntimeHelpers.OffsetToStringData;
          string.wstrcpy(dmem, ptr, num);
        }
        finally
        {
          str2 = (string) null;
        }
        return str1;
      }
      catch (NullReferenceException ex)
      {
        throw new ArgumentOutOfRangeException("ptr", Environment.GetResourceString("ArgumentOutOfRange_PartialWCHAR"));
      }
    }

    [SecurityCritical]
    private unsafe string CtorCharPtrStartLength(char* ptr, int startIndex, int length)
    {
      if (length < 0)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      char* smem = ptr + startIndex;
      if (smem < ptr)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_PartialWCHAR"));
      if (length == 0)
        return string.Empty;
      string str1 = string.FastAllocateString(length);
      try
      {
        string str2;
        try
        {
          str2 = str1;
          char* dmem = (char*) str2;
          if ((IntPtr) dmem != IntPtr.Zero)
            dmem += RuntimeHelpers.OffsetToStringData;
          string.wstrcpy(dmem, smem, length);
        }
        finally
        {
          str2 = (string) null;
        }
        return str1;
      }
      catch (NullReferenceException ex)
      {
        throw new ArgumentOutOfRangeException("ptr", Environment.GetResourceString("ArgumentOutOfRange_PartialWCHAR"));
      }
    }

    /// <summary>比较两个指定的 <see cref="T:System.String" /> 对象，并返回一个指示二者在排序顺序中的相对位置的整数。</summary>
    /// <returns>一个 32 位带符号整数，指示两个比较数之间的词法关系。值 条件 小于零 <paramref name="strA" /> 在排序顺序中位于 <paramref name="strB" /> 之前。零 <paramref name="strA" /> 在相同的位置中出现 <paramref name="strB" /> 的排序顺序。大于零 <paramref name="strA" /> 在排序顺序中位于 <paramref name="strB" /> 之后。</returns>
    /// <param name="strA">要比较的第一个字符串。</param>
    /// <param name="strB">要比较的第二个字符串。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int Compare(string strA, string strB)
    {
      return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.None);
    }

    /// <summary>比较两个指定的 <see cref="T:System.String" /> 对象（其中忽略或考虑其大小写），并返回一个整数，指示二者在排序顺序中的相对位置。</summary>
    /// <returns>一个 32 位带符号整数，指示两个比较数之间的词法关系。值 条件 小于零 <paramref name="strA" /> 在排序顺序中位于 <paramref name="strB" /> 之前。零 <paramref name="strA" /> 在相同的位置中出现 <paramref name="strB" /> 的排序顺序。大于零 <paramref name="strA" /> 在排序顺序中位于 <paramref name="strB" /> 之后。</returns>
    /// <param name="strA">要比较的第一个字符串。</param>
    /// <param name="strB">要比较的第二个字符串。</param>
    /// <param name="ignoreCase">若要在比较过程中忽略大小写，则为 true；否则为 false。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int Compare(string strA, string strB, bool ignoreCase)
    {
      if (ignoreCase)
        return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase);
      return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.None);
    }

    /// <summary>使用指定的规则比较两个指定的 <see cref="T:System.String" /> 对象，并返回一个整数，指示二者在排序顺序中的相对位置。</summary>
    /// <returns>一个 32 位带符号整数，指示两个比较数之间的词法关系。值 条件 小于零 <paramref name="strA" /> 在排序顺序中位于 <paramref name="strB" /> 之前。零 <paramref name="strA" /> 处于相同的位置 <paramref name="strB" /> 的排序顺序。大于零 <paramref name="strA" /> 在排序顺序中位于 <paramref name="strB" /> 之后。</returns>
    /// <param name="strA">要比较的第一个字符串。</param>
    /// <param name="strB">要比较的第二个字符串。</param>
    /// <param name="comparisonType">一个枚举值，用于指定比较中要使用的规则。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="comparisonType" /> 不是 <see cref="T:System.StringComparison" /> 值。</exception>
    /// <exception cref="T:System.NotSupportedException">不支持 <see cref="T:System.StringComparison" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static int Compare(string strA, string strB, StringComparison comparisonType)
    {
      if ((uint) (comparisonType - 0) > 5U)
        throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
      if (strA == strB)
        return 0;
      if (strA == null)
        return -1;
      if (strB == null)
        return 1;
      switch (comparisonType)
      {
        case StringComparison.CurrentCulture:
          return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.None);
        case StringComparison.CurrentCultureIgnoreCase:
          return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase);
        case StringComparison.InvariantCulture:
          return CultureInfo.InvariantCulture.CompareInfo.Compare(strA, strB, CompareOptions.None);
        case StringComparison.InvariantCultureIgnoreCase:
          return CultureInfo.InvariantCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase);
        case StringComparison.Ordinal:
          if ((int) strA.m_firstChar - (int) strB.m_firstChar != 0)
            return (int) strA.m_firstChar - (int) strB.m_firstChar;
          return string.CompareOrdinalHelper(strA, strB);
        case StringComparison.OrdinalIgnoreCase:
          if (strA.IsAscii() && strB.IsAscii())
            return string.CompareOrdinalIgnoreCaseHelper(strA, strB);
          return TextInfo.CompareOrdinalIgnoreCase(strA, strB);
        default:
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_StringComparison"));
      }
    }

    /// <summary>对两个指定的 <see cref="T:System.String" /> 对象进行比较，使用指定的比较选项和区域性特定的信息来影响比较，并返回一个整数，该整数指示这两个字符串在排序顺序中的关系。</summary>
    /// <returns>一个 32 位带符号整数，该整数指示 <paramref name="strA" /> 与 <paramref name="strB" /> 之间的词法关系，如下表所示值条件小于零<paramref name="strA" /> 在排序顺序中位于 <paramref name="strB" /> 之前。零<paramref name="strA" /> 在相同的位置中出现 <paramref name="strB" /> 的排序顺序。大于零<paramref name="strA" /> 在排序顺序中位于 <paramref name="strB" /> 之后。</returns>
    /// <param name="strA">要比较的第一个字符串。 </param>
    /// <param name="strB">要比较的第二个字符串。</param>
    /// <param name="culture">提供区域性特定的比较信息的区域性。</param>
    /// <param name="options">要在执行比较时使用的选项（如忽略大小写或符号）。 </param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 不是 <see cref="T:System.Globalization.CompareOptions" /> 值。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="culture" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public static int Compare(string strA, string strB, CultureInfo culture, CompareOptions options)
    {
      if (culture == null)
        throw new ArgumentNullException("culture");
      return culture.CompareInfo.Compare(strA, strB, options);
    }

    /// <summary>比较两个指定的 <see cref="T:System.String" /> 对象（其中忽略或考虑其大小写，并使用区域性特定的信息干预比较），并返回一个整数，指示二者在排序顺序中的相对位置。</summary>
    /// <returns>一个 32 位带符号整数，指示两个比较数之间的词法关系。值 条件 小于零 <paramref name="strA" /> 在排序顺序中位于 <paramref name="strB" /> 之前。零 <paramref name="strA" /> 在相同的位置中出现 <paramref name="strB" /> 的排序顺序。大于零 <paramref name="strA" /> 在排序顺序中位于 <paramref name="strB" /> 之后。</returns>
    /// <param name="strA">要比较的第一个字符串。</param>
    /// <param name="strB">要比较的第二个字符串。</param>
    /// <param name="ignoreCase">若要在比较过程中忽略大小写，则为 true；否则为 false。</param>
    /// <param name="culture">一个对象，提供区域性特定的比较信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="culture" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    public static int Compare(string strA, string strB, bool ignoreCase, CultureInfo culture)
    {
      if (culture == null)
        throw new ArgumentNullException("culture");
      if (ignoreCase)
        return culture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase);
      return culture.CompareInfo.Compare(strA, strB, CompareOptions.None);
    }

    /// <summary>比较两个指定的 <see cref="T:System.String" /> 对象的子字符串，并返回一个指示二者在排序顺序中的相对位置的整数。</summary>
    /// <returns>一个 32 位有符号整数，指示两个比较数之间的词法关系。值 条件 小于零 中的子字符串 <paramref name="strA" /> 前面中的子字符串 <paramref name="strB" /> 的排序顺序。零 中的相同位置中的排序顺序，出现子字符串或 <paramref name="length" /> 为零。大于零 中的子字符串 <paramref name="strA" /> 遵循中的子字符串 <paramref name="strB" /> 的排序顺序。</returns>
    /// <param name="strA">要在比较中使用的第一个字符串。</param>
    /// <param name="indexA">
    /// <paramref name="strA" /> 中子字符串的位置。</param>
    /// <param name="strB">要在比较中使用的第二个字符串。</param>
    /// <param name="indexB">
    /// <paramref name="strB" /> 中子字符串的位置。</param>
    /// <param name="length">要比较的子字符串中字符的最大数量。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="indexA" /> 大于 <paramref name="strA" />。<see cref="P:System.String.Length" />。- 或 - <paramref name="indexB" /> 大于 <paramref name="strB" />。<see cref="P:System.String.Length" />。- 或 - <paramref name="indexA" />, <paramref name="indexB" />, ，或 <paramref name="length" /> 为负。- 或 -要么 <paramref name="indexA" /> 或 <paramref name="indexB" /> 是 null, ，和 <paramref name="length" /> 大于零。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int Compare(string strA, int indexA, string strB, int indexB, int length)
    {
      int length1 = length;
      int length2 = length;
      if (strA != null && strA.Length - indexA < length1)
        length1 = strA.Length - indexA;
      if (strB != null && strB.Length - indexB < length2)
        length2 = strB.Length - indexB;
      return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, length1, strB, indexB, length2, CompareOptions.None);
    }

    /// <summary>比较两个指定的 <see cref="T:System.String" /> 对象的子字符串（忽略或考虑其大小写），并返回一个整数，指示二者在排序顺序中的相对位置。</summary>
    /// <returns>一个 32 位带符号整数，指示两个比较数之间的词法关系。值条件 小于零 中的子字符串 <paramref name="strA" /> 前面中的子字符串 <paramref name="strB" /> 的排序顺序。零 中的相同位置中的排序顺序，出现子字符串或 <paramref name="length" /> 为零。大于零 中的子字符串 <paramref name="strA" /> 遵循中的子字符串 <paramref name="strB" /> 的排序顺序。</returns>
    /// <param name="strA">要在比较中使用的第一个字符串。</param>
    /// <param name="indexA">
    /// <paramref name="strA" /> 中子字符串的位置。</param>
    /// <param name="strB">要在比较中使用的第二个字符串。</param>
    /// <param name="indexB">
    /// <paramref name="strB" /> 中子字符串的位置。</param>
    /// <param name="length">要比较的子字符串中字符的最大数量。</param>
    /// <param name="ignoreCase">若要在比较过程中忽略大小写，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="indexA" /> 大于 <paramref name="strA" />。<see cref="P:System.String.Length" />。- 或 - <paramref name="indexB" /> 大于 <paramref name="strB" />。<see cref="P:System.String.Length" />。- 或 - <paramref name="indexA" />, <paramref name="indexB" />, ，或 <paramref name="length" /> 为负。- 或 -要么 <paramref name="indexA" /> 或 <paramref name="indexB" /> 是 null, ，和 <paramref name="length" /> 大于零。</exception>
    /// <filterpriority>1</filterpriority>
    public static int Compare(string strA, int indexA, string strB, int indexB, int length, bool ignoreCase)
    {
      int length1 = length;
      int length2 = length;
      if (strA != null && strA.Length - indexA < length1)
        length1 = strA.Length - indexA;
      if (strB != null && strB.Length - indexB < length2)
        length2 = strB.Length - indexB;
      if (ignoreCase)
        return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, length1, strB, indexB, length2, CompareOptions.IgnoreCase);
      return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, length1, strB, indexB, length2, CompareOptions.None);
    }

    /// <summary>比较两个指定的 <see cref="T:System.String" /> 对象（其中忽略或考虑其大小写，并使用区域性特定的信息干预比较），并返回一个整数，指示二者在排序顺序中的相对位置。</summary>
    /// <returns>一个整数，指示两个比较字之间的词法关系。值 条件 小于零 中的子字符串 <paramref name="strA" /> 前面中的子字符串 <paramref name="strB" /> 的排序顺序。零 中的相同位置中的排序顺序，出现子字符串或 <paramref name="length" /> 为零。大于零 中的子字符串 <paramref name="strA" /> 遵循中的子字符串 <paramref name="strB" /> 的排序顺序。</returns>
    /// <param name="strA">要在比较中使用的第一个字符串。</param>
    /// <param name="indexA">
    /// <paramref name="strA" /> 中子字符串的位置。</param>
    /// <param name="strB">要在比较中使用的第二个字符串。</param>
    /// <param name="indexB">
    /// <paramref name="strB" /> 中子字符串的位置。</param>
    /// <param name="length">要比较的子字符串中字符的最大数量。</param>
    /// <param name="ignoreCase">若要在比较过程中忽略大小写，则为 true；否则为 false。</param>
    /// <param name="culture">一个对象，提供区域性特定的比较信息。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="indexA" /> 大于 <paramref name="strA" />。<see cref="P:System.String.Length" />。- 或 - <paramref name="indexB" /> 大于 <paramref name="strB" />。<see cref="P:System.String.Length" />。- 或 - <paramref name="indexA" />, <paramref name="indexB" />, ，或 <paramref name="length" /> 为负。- 或 -要么 <paramref name="strA" /> 或 <paramref name="strB" /> 是 null, ，和 <paramref name="length" /> 大于零。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="culture" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    public static int Compare(string strA, int indexA, string strB, int indexB, int length, bool ignoreCase, CultureInfo culture)
    {
      if (culture == null)
        throw new ArgumentNullException("culture");
      int length1 = length;
      int length2 = length;
      if (strA != null && strA.Length - indexA < length1)
        length1 = strA.Length - indexA;
      if (strB != null && strB.Length - indexB < length2)
        length2 = strB.Length - indexB;
      if (ignoreCase)
        return culture.CompareInfo.Compare(strA, indexA, length1, strB, indexB, length2, CompareOptions.IgnoreCase);
      return culture.CompareInfo.Compare(strA, indexA, length1, strB, indexB, length2, CompareOptions.None);
    }

    /// <summary>对两个指定 <see cref="T:System.String" /> 对象的子字符串进行比较，使用指定的比较选项和区域性特定的信息来影响比较，并返回一个整数，该整数指示这两个子字符串在排序顺序中的关系。</summary>
    /// <returns>一个整数，该整数用于指示两个子字符串之间的词法关系，如下表所示。值条件小于零中的子字符串 <paramref name="strA" /> 前面中的子字符串 <paramref name="strB" /> 的排序顺序。零中的相同位置中的排序顺序，出现子字符串或 <paramref name="length" /> 为零。大于零中的子字符串 <paramref name="strA" /> 遵循中的子字符串 <paramref name="strB" /> 的排序顺序。</returns>
    /// <param name="strA">要在比较中使用的第一个字符串。  </param>
    /// <param name="indexA">
    /// <paramref name="strA" /> 中子字符串开始的位置。</param>
    /// <param name="strB">要在比较中使用的第二个字符串。</param>
    /// <param name="indexB">
    /// <paramref name="strB" /> 中子字符串开始的位置。</param>
    /// <param name="length">要比较的子字符串中字符的最大数量。</param>
    /// <param name="culture">一个对象，提供区域性特定的比较信息。</param>
    /// <param name="options">要在执行比较时使用的选项（如忽略大小写或符号）。 </param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="options" /> 不是 <see cref="T:System.Globalization.CompareOptions" /> 值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="indexA" /> 大于 <paramref name="strA" />.Length。- 或 -<paramref name="indexB" /> 大于 <paramref name="strB" />.Length。- 或 -<paramref name="indexA" />, <paramref name="indexB" />, ，或 <paramref name="length" /> 为负。- 或 -要么 <paramref name="strA" /> 或 <paramref name="strB" /> 是 null, ，和 <paramref name="length" /> 大于零。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="culture" /> 为 null。</exception>
    public static int Compare(string strA, int indexA, string strB, int indexB, int length, CultureInfo culture, CompareOptions options)
    {
      if (culture == null)
        throw new ArgumentNullException("culture");
      int length1 = length;
      int length2 = length;
      if (strA != null && strA.Length - indexA < length1)
        length1 = strA.Length - indexA;
      if (strB != null && strB.Length - indexB < length2)
        length2 = strB.Length - indexB;
      return culture.CompareInfo.Compare(strA, indexA, length1, strB, indexB, length2, options);
    }

    /// <summary>使用指定的规则比较两个指定的 <see cref="T:System.String" /> 对象的子字符串，并返回一个整数，指示二者在排序顺序中的相对位置。</summary>
    /// <returns>一个 32 位带符号整数，指示两个比较数之间的词法关系。值 条件 小于零 中的子字符串 <paramref name="strA" /> 前面中的子字符串 <paramref name="strB" /> 的排序顺序。零 中的相同位置中的排序顺序，出现子字符串或 <paramref name="length" /> 参数为零。大于零 中的子字符串 <paramref name="strA" /> follllows 子字符串在 <paramref name="strB" /> 的排序顺序。</returns>
    /// <param name="strA">要在比较中使用的第一个字符串。</param>
    /// <param name="indexA">
    /// <paramref name="strA" /> 中子字符串的位置。</param>
    /// <param name="strB">要在比较中使用的第二个字符串。</param>
    /// <param name="indexB">
    /// <paramref name="strB" /> 中子字符串的位置。</param>
    /// <param name="length">要比较的子字符串中字符的最大数量。</param>
    /// <param name="comparisonType">一个枚举值，用于指定比较中要使用的规则。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="indexA" /> 大于 <paramref name="strA" />。<see cref="P:System.String.Length" />。- 或 - <paramref name="indexB" /> 大于 <paramref name="strB" />。<see cref="P:System.String.Length" />。- 或 - <paramref name="indexA" />, <paramref name="indexB" />, ，或 <paramref name="length" /> 为负。- 或 -要么 <paramref name="indexA" /> 或 <paramref name="indexB" /> 是 null, ，和 <paramref name="length" /> 大于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="comparisonType" /> 不是 <see cref="T:System.StringComparison" /> 值。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static int Compare(string strA, int indexA, string strB, int indexB, int length, StringComparison comparisonType)
    {
      if (comparisonType < StringComparison.CurrentCulture || comparisonType > StringComparison.OrdinalIgnoreCase)
        throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
      if (strA == null || strB == null)
      {
        if (strA == strB)
          return 0;
        return strA != null ? 1 : -1;
      }
      if (length < 0)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
      if (indexA < 0)
        throw new ArgumentOutOfRangeException("indexA", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (indexB < 0)
        throw new ArgumentOutOfRangeException("indexB", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (strA.Length - indexA < 0)
        throw new ArgumentOutOfRangeException("indexA", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (strB.Length - indexB < 0)
        throw new ArgumentOutOfRangeException("indexB", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (length == 0 || strA == strB && indexA == indexB)
        return 0;
      int num1 = length;
      int num2 = length;
      if (strA != null && strA.Length - indexA < num1)
        num1 = strA.Length - indexA;
      if (strB != null && strB.Length - indexB < num2)
        num2 = strB.Length - indexB;
      switch (comparisonType)
      {
        case StringComparison.CurrentCulture:
          return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, num1, strB, indexB, num2, CompareOptions.None);
        case StringComparison.CurrentCultureIgnoreCase:
          return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, num1, strB, indexB, num2, CompareOptions.IgnoreCase);
        case StringComparison.InvariantCulture:
          return CultureInfo.InvariantCulture.CompareInfo.Compare(strA, indexA, num1, strB, indexB, num2, CompareOptions.None);
        case StringComparison.InvariantCultureIgnoreCase:
          return CultureInfo.InvariantCulture.CompareInfo.Compare(strA, indexA, num1, strB, indexB, num2, CompareOptions.IgnoreCase);
        case StringComparison.Ordinal:
          return string.nativeCompareOrdinalEx(strA, indexA, strB, indexB, length);
        case StringComparison.OrdinalIgnoreCase:
          return TextInfo.CompareOrdinalIgnoreCaseEx(strA, indexA, strB, indexB, num1, num2);
        default:
          throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"));
      }
    }

    /// <summary>将此实例与指定的 <see cref="T:System.Object" /> 进行比较，并指示此实例在排序顺序中是位于指定的 <see cref="T:System.Object" /> 之前、之后还是与其出现在同一位置。</summary>
    /// <returns>一个 32 位带符号整数，该整数指示此实例在排序顺序中是位于 <paramref name="value" /> 参数之前、之后还是与其出现在同一位置。值 条件 小于零 此实例位于 <paramref name="value" /> 之前。零 此实例在排序顺序中的位置与 <paramref name="value" /> 相同。大于零 此实例位于 <paramref name="value" /> 之后。- 或 - <paramref name="value" /> 为 null。</returns>
    /// <param name="value">一个对象，其计算结果为 <see cref="T:System.String" />。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="value" /> 不是 <see cref="T:System.String" />。</exception>
    /// <filterpriority>2</filterpriority>
    public int CompareTo(object value)
    {
      if (value == null)
        return 1;
      if (!(value is string))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeString"));
      return string.Compare(this, (string) value, StringComparison.CurrentCulture);
    }

    /// <summary>将此实例与指定的 <see cref="T:System.String" /> 对象进行比较，并指示此实例在排序顺序中是位于指定的字符串之前、之后还是与其出现在同一位置。</summary>
    /// <returns>一个 32 位带符号整数，该整数指示此实例在排序顺序中是位于 <paramref name="strB" /> 参数之前、之后还是与其出现在同一位置。值 条件 小于零 此实例位于 <paramref name="strB" /> 之前。零 此实例在排序顺序中的位置与 <paramref name="strB" /> 相同。大于零 此实例位于 <paramref name="strB" /> 之后。- 或 - <paramref name="strB" /> 为 null。</returns>
    /// <param name="strB">要与此实例进行比较的字符串。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public int CompareTo(string strB)
    {
      if (strB == null)
        return 1;
      return CultureInfo.CurrentCulture.CompareInfo.Compare(this, strB, CompareOptions.None);
    }

    /// <summary>通过计算每个字符串中相应 <see cref="T:System.String" /> 对象的数值来比较两个指定的 <see cref="T:System.Char" /> 对象。</summary>
    /// <returns>一个整数，指示两个比较字之间的词法关系。值条件 小于零 <paramref name="strA" /> 小于 <paramref name="strB" />。零 <paramref name="strA" /> 与 <paramref name="strB" /> 相等。大于零 <paramref name="strA" /> 大于 <paramref name="strB" />。</returns>
    /// <param name="strA">要比较的第一个字符串。</param>
    /// <param name="strB">要比较的第二个字符串。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public static int CompareOrdinal(string strA, string strB)
    {
      if (strA == strB)
        return 0;
      if (strA == null)
        return -1;
      if (strB == null)
        return 1;
      if ((int) strA.m_firstChar - (int) strB.m_firstChar != 0)
        return (int) strA.m_firstChar - (int) strB.m_firstChar;
      return string.CompareOrdinalHelper(strA, strB);
    }

    /// <summary>通过计算每个子字符串中相应 <see cref="T:System.String" /> 对象的数值来比较两个指定的 <see cref="T:System.Char" /> 对象的子字符串。</summary>
    /// <returns>一个 32 位带符号整数，指示两个比较数之间的词法关系。值条件 小于零 <paramref name="strA" /> 中的子字符串小于 <paramref name="strB" /> 中的子字符串。零 子字符串相等，或者 <paramref name="length" /> 为零。大于零 <paramref name="strA" /> 中的子字符串大于 <paramref name="strB" /> 中的子字符串。</returns>
    /// <param name="strA">要在比较中使用的第一个字符串。</param>
    /// <param name="indexA">
    /// <paramref name="strA" /> 中子字符串的起始索引。</param>
    /// <param name="strB">要在比较中使用的第二个字符串。</param>
    /// <param name="indexB">
    /// <paramref name="strB" /> 中子字符串的起始索引。</param>
    /// <param name="length">要比较的子字符串中字符的最大数量。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="strA" /> 不是 null 和 <paramref name="indexA" /> 大于 <paramref name="strA" />。<see cref="P:System.String.Length" />。- 或 - <paramref name="strB" /> 不是 null 和<paramref name="indexB" /> 大于 <paramref name="strB" />。<see cref="P:System.String.Length" />。- 或 - <paramref name="indexA" />, <paramref name="indexB" />, ，或 <paramref name="length" /> 为负。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static int CompareOrdinal(string strA, int indexA, string strB, int indexB, int length)
    {
      if (strA != null && strB != null)
        return string.nativeCompareOrdinalEx(strA, indexA, strB, indexB, length);
      if (strA == strB)
        return 0;
      return strA != null ? 1 : -1;
    }

    /// <summary>返回一个值，该值指示指定的子串是否出现在此字符串中。</summary>
    /// <returns>如果 true 参数出现在此字符串中，或者 <paramref name="value" /> 为空字符串 ("")，则为 <paramref name="value" />；否则为 false。</returns>
    /// <param name="value">要搜寻的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public bool Contains(string value)
    {
      return this.IndexOf(value, StringComparison.Ordinal) >= 0;
    }

    /// <summary>确定此字符串实例的结尾是否与指定的字符串匹配。</summary>
    /// <returns>如果 true 与此实例的末尾匹配，则为 <paramref name="value" />；否则为 false。</returns>
    /// <param name="value">要与此实例末尾的子字符串进行比较的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public bool EndsWith(string value)
    {
      return this.EndsWith(value, StringComparison.CurrentCulture);
    }

    /// <summary>确定使用指定的比较选项进行比较时此字符串实例的结尾是否与指定的字符串匹配。</summary>
    /// <returns>如果 true 参数与此字符串的末尾匹配，则为 <paramref name="value" />；否则为 false。</returns>
    /// <param name="value">要与此实例末尾的子字符串进行比较的字符串。</param>
    /// <param name="comparisonType">枚举值之一，用于确定如何比较此字符串与 <paramref name="value" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="comparisonType" /> 不是 <see cref="T:System.StringComparison" /> 值。</exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public bool EndsWith(string value, StringComparison comparisonType)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (comparisonType < StringComparison.CurrentCulture || comparisonType > StringComparison.OrdinalIgnoreCase)
        throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
      if (this == value || value.Length == 0)
        return true;
      switch (comparisonType)
      {
        case StringComparison.CurrentCulture:
          return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(this, value, CompareOptions.None);
        case StringComparison.CurrentCultureIgnoreCase:
          return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(this, value, CompareOptions.IgnoreCase);
        case StringComparison.InvariantCulture:
          return CultureInfo.InvariantCulture.CompareInfo.IsSuffix(this, value, CompareOptions.None);
        case StringComparison.InvariantCultureIgnoreCase:
          return CultureInfo.InvariantCulture.CompareInfo.IsSuffix(this, value, CompareOptions.IgnoreCase);
        case StringComparison.Ordinal:
          if (this.Length >= value.Length)
            return string.nativeCompareOrdinalEx(this, this.Length - value.Length, value, 0, value.Length) == 0;
          return false;
        case StringComparison.OrdinalIgnoreCase:
          if (this.Length >= value.Length)
            return TextInfo.CompareOrdinalIgnoreCaseEx(this, this.Length - value.Length, value, 0, value.Length, value.Length) == 0;
          return false;
        default:
          throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
      }
    }

    /// <summary>确定在使用指定的区域性进行比较时此字符串实例的结尾是否与指定的字符串匹配。</summary>
    /// <returns>如果 true 参数与此字符串的末尾匹配，则为 <paramref name="value" />；否则为 false。</returns>
    /// <param name="value">要与此实例末尾的子字符串进行比较的字符串。</param>
    /// <param name="ignoreCase">若要在比较过程中忽略大小写，则为 true；否则为 false。</param>
    /// <param name="culture">确定如何对此实例与 <paramref name="value" /> 进行比较的区域性信息。如果 <paramref name="culture" /> 为 null，则使用当前区域性。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    public bool EndsWith(string value, bool ignoreCase, CultureInfo culture)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (this == value)
        return true;
      return (culture != null ? culture : CultureInfo.CurrentCulture).CompareInfo.IsSuffix(this, value, ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
    }

    internal bool EndsWith(char value)
    {
      int length = this.Length;
      return length != 0 && (int) this[length - 1] == (int) value;
    }

    /// <summary>报告指定 Unicode 字符在此字符串中的第一个匹配项的从零开始的索引。</summary>
    /// <returns>如果找到该字符，则为 <paramref name="value" /> 的从零开始的索引位置；如果未找到，则为 -1。</returns>
    /// <param name="value">要查找的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int IndexOf(char value)
    {
      return this.IndexOf(value, 0, this.Length);
    }

    /// <summary>报告指定 Unicode 字符在此字符串中的第一个匹配项的从零开始的索引。该搜索从指定字符位置开始。</summary>
    /// <returns>如果找到该字符，则为 <paramref name="value" /> 的从零开始的索引位置；如果未找到，则为 -1。</returns>
    /// <param name="value">要查找的 Unicode 字符。</param>
    /// <param name="startIndex">搜索起始位置。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 小于 0 （零） 或大于字符串的长度。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int IndexOf(char value, int startIndex)
    {
      return this.IndexOf(value, startIndex, this.Length - startIndex);
    }

    /// <summary>报告指定字符在此实例中的第一个匹配项的从零开始的索引。搜索从指定字符位置开始，并检查指定数量的字符位置。</summary>
    /// <returns>如果找到该字符，则为 <paramref name="value" /> 的从零开始的索引位置；如果未找到，则为 -1。</returns>
    /// <param name="value">要查找的 Unicode 字符。</param>
    /// <param name="startIndex">搜索起始位置。</param>
    /// <param name="count">要检查的字符位置数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 或 <paramref name="startIndex" /> 为负。- 或 - <paramref name="startIndex" /> 大于此字符串的长度。- 或 -<paramref name="count" /> 大于减去此字符串的长度 <paramref name="startIndex" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public int IndexOf(char value, int startIndex, int count);

    /// <summary>报告指定 Unicode 字符数组中的任意字符在此实例中第一个匹配项的从零开始的索引。</summary>
    /// <returns>在此实例中第一次找到 <paramref name="anyOf" /> 中的任意字符的索引位置（从零开始）；如果未找到 <paramref name="anyOf" /> 中的字符，则为 -1。</returns>
    /// <param name="anyOf">Unicode 字符数组，包含一个或多个要查找的字符。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="anyOf" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public int IndexOfAny(char[] anyOf)
    {
      return this.IndexOfAny(anyOf, 0, this.Length);
    }

    /// <summary>报告指定 Unicode 字符数组中的任意字符在此实例中第一个匹配项的从零开始的索引。该搜索从指定字符位置开始。</summary>
    /// <returns>在此实例中第一次找到 <paramref name="anyOf" /> 中的任意字符的索引位置（从零开始）；如果未找到 <paramref name="anyOf" /> 中的字符，则为 -1。</returns>
    /// <param name="anyOf">Unicode 字符数组，包含一个或多个要查找的字符。</param>
    /// <param name="startIndex">搜索起始位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="anyOf" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 为负。- 或 - <paramref name="startIndex" /> 大于此实例中的字符数。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public int IndexOfAny(char[] anyOf, int startIndex)
    {
      return this.IndexOfAny(anyOf, startIndex, this.Length - startIndex);
    }

    /// <summary>报告指定 Unicode 字符数组中的任意字符在此实例中第一个匹配项的从零开始的索引。搜索从指定字符位置开始，并检查指定数量的字符位置。</summary>
    /// <returns>在此实例中第一次找到 <paramref name="anyOf" /> 中的任意字符的索引位置（从零开始）；如果未找到 <paramref name="anyOf" /> 中的字符，则为 -1。</returns>
    /// <param name="anyOf">Unicode 字符数组，包含一个或多个要查找的字符。</param>
    /// <param name="startIndex">搜索起始位置。</param>
    /// <param name="count">要检查的字符位置数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="anyOf" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 或 <paramref name="startIndex" /> 为负。- 或 - <paramref name="count" /> + <paramref name="startIndex" /> 大于此实例中的字符数。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public int IndexOfAny(char[] anyOf, int startIndex, int count);

    /// <summary>报告指定字符串在此实例中的第一个匹配项的从零开始的索引。</summary>
    /// <returns>如果找到该字符串，则为 <paramref name="value" /> 的从零开始的索引位置；如果未找到该字符串，则为 -1。如果 <paramref name="value" /> 为 <see cref="F:System.String.Empty" />，则返回值为 0。</returns>
    /// <param name="value">要搜寻的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int IndexOf(string value)
    {
      return this.IndexOf(value, StringComparison.CurrentCulture);
    }

    /// <summary>报告指定字符串在此实例中的第一个匹配项的从零开始的索引。该搜索从指定字符位置开始。</summary>
    /// <returns>如果找到该字符串，则为 <paramref name="value" /> 的从零开始的索引位置；如果未找到该字符串，则为 -1。如果 <paramref name="value" /> 为 <see cref="F:System.String.Empty" />，则返回值为 <paramref name="startIndex" />。</returns>
    /// <param name="value">要搜寻的字符串。</param>
    /// <param name="startIndex">搜索起始位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 小于 0 （零） 或大于此字符串的长度。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int IndexOf(string value, int startIndex)
    {
      return this.IndexOf(value, startIndex, StringComparison.CurrentCulture);
    }

    /// <summary>报告指定字符串在此实例中的第一个匹配项的从零开始的索引。搜索从指定字符位置开始，并检查指定数量的字符位置。</summary>
    /// <returns>如果找到该字符串，则为 <paramref name="value" /> 的从零开始的索引位置；如果未找到该字符串，则为 -1。如果 <paramref name="value" /> 为 <see cref="F:System.String.Empty" />，则返回值为 <paramref name="startIndex" />。</returns>
    /// <param name="value">要搜寻的字符串。</param>
    /// <param name="startIndex">搜索起始位置。</param>
    /// <param name="count">要检查的字符位置数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 或 <paramref name="startIndex" /> 为负。- 或 - <paramref name="startIndex" /> 大于此字符串的长度。- 或 -<paramref name="count" /> 大于减去此字符串的长度 <paramref name="startIndex" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int IndexOf(string value, int startIndex, int count)
    {
      if (startIndex < 0 || startIndex > this.Length)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0 || count > this.Length - startIndex)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
      return this.IndexOf(value, startIndex, count, StringComparison.CurrentCulture);
    }

    /// <summary>报告指定的字符串在当前 <see cref="T:System.String" /> 对象中的第一个匹配项的从零开始的索引。一个参数指定要用于指定字符串的搜索类型。</summary>
    /// <returns>如果找到该字符串，则为 <paramref name="value" /> 参数的索引位置；如果未找到该字符串，则为 -1。如果 <paramref name="value" /> 为 <see cref="F:System.String.Empty" />，则返回值为 0。</returns>
    /// <param name="value">要搜寻的字符串。</param>
    /// <param name="comparisonType">指定搜索规则的枚举值之一。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="comparisonType" /> 不是有效 <see cref="T:System.StringComparison" /> 值。</exception>
    [__DynamicallyInvokable]
    public int IndexOf(string value, StringComparison comparisonType)
    {
      return this.IndexOf(value, 0, this.Length, comparisonType);
    }

    /// <summary>报告指定的字符串在当前 <see cref="T:System.String" /> 对象中的第一个匹配项的从零开始的索引。参数指定当前字符串中的起始搜索位置以及用于指定字符串的搜索类型。</summary>
    /// <returns>如果找到该字符串，则为 <paramref name="value" /> 参数的从零开始的索引位置；如果未找到该字符串，则为 -1。如果 <paramref name="value" /> 为 <see cref="F:System.String.Empty" />，则返回值为 <paramref name="startIndex" />。</returns>
    /// <param name="value">要搜寻的字符串。</param>
    /// <param name="startIndex">搜索起始位置。</param>
    /// <param name="comparisonType">指定搜索规则的枚举值之一。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 小于 0 （零） 或大于此字符串的长度。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="comparisonType" /> 不是有效 <see cref="T:System.StringComparison" /> 值。</exception>
    [__DynamicallyInvokable]
    public int IndexOf(string value, int startIndex, StringComparison comparisonType)
    {
      return this.IndexOf(value, startIndex, this.Length - startIndex, comparisonType);
    }

    /// <summary>报告指定的字符串在当前 <see cref="T:System.String" /> 对象中的第一个匹配项的从零开始的索引。参数指定当前字符串中的起始搜索位置、要搜索的当前字符串中的字符数量，以及要用于指定字符串的搜索类型。</summary>
    /// <returns>如果找到该字符串，则为 <paramref name="value" /> 参数的从零开始的索引位置；如果未找到该字符串，则为 -1。如果 <paramref name="value" /> 为 <see cref="F:System.String.Empty" />，则返回值为 <paramref name="startIndex" />。</returns>
    /// <param name="value">要搜寻的字符串。</param>
    /// <param name="startIndex">搜索起始位置。</param>
    /// <param name="count">要检查的字符位置数。</param>
    /// <param name="comparisonType">指定搜索规则的枚举值之一。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 或 <paramref name="startIndex" /> 为负。- 或 - <paramref name="startIndex" /> 大于此实例的长度。- 或 -<paramref name="count" /> 大于减去此字符串的长度 <paramref name="startIndex" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="comparisonType" /> 不是有效 <see cref="T:System.StringComparison" /> 值。</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public int IndexOf(string value, int startIndex, int count, StringComparison comparisonType)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (startIndex < 0 || startIndex > this.Length)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0 || startIndex > this.Length - count)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
      switch (comparisonType)
      {
        case StringComparison.CurrentCulture:
          return CultureInfo.CurrentCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.None);
        case StringComparison.CurrentCultureIgnoreCase:
          return CultureInfo.CurrentCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
        case StringComparison.InvariantCulture:
          return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.None);
        case StringComparison.InvariantCultureIgnoreCase:
          return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
        case StringComparison.Ordinal:
          return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.Ordinal);
        case StringComparison.OrdinalIgnoreCase:
          if (value.IsAscii() && this.IsAscii())
            return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
          return TextInfo.IndexOfStringOrdinalIgnoreCase(this, value, startIndex, count);
        default:
          throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
      }
    }

    /// <summary>报告指定 Unicode 字符在此实例中的最后一个匹配项的从零开始的索引的位置。</summary>
    /// <returns>如果找到该字符，则为 <paramref name="value" /> 的从零开始的索引位置；如果未找到，则为 -1。</returns>
    /// <param name="value">要查找的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int LastIndexOf(char value)
    {
      return this.LastIndexOf(value, this.Length - 1, this.Length);
    }

    /// <summary>报告指定 Unicode 字符在此实例中的最后一个匹配项的从零开始的索引的位置。在指定的字符位置开始和在向后的右边该字符串的开头处理的搜索。</summary>
    /// <returns>如果找到该字符，则为 <paramref name="value" /> 的从零开始的索引位置；如果未找到该字符或当前实例等于 <see cref="F:System.String.Empty" />，则为 -1。</returns>
    /// <param name="value">要查找的 Unicode 字符。</param>
    /// <param name="startIndex">搜索的起始位置。从 <paramref name="startIndex" /> 此实例的开头开始搜索。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">当前实例不等于 <see cref="F:System.String.Empty" />, ，和 <paramref name="startIndex" /> 小于零或大于或等于此实例的长度。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int LastIndexOf(char value, int startIndex)
    {
      int num1 = (int) value;
      int startIndex1 = startIndex;
      int num2 = 1;
      int count = startIndex1 + num2;
      return this.LastIndexOf((char) num1, startIndex1, count);
    }

    /// <summary>报告指定的 Unicode 字符在此实例内的子字符串中的最后一个匹配项的从零开始的索引的位置。搜索在指定字符位置的数目的字符串开始时，开始指定字符和其后面的位置。</summary>
    /// <returns>如果找到该字符，则为 <paramref name="value" /> 的从零开始的索引位置；如果未找到该字符或当前实例等于 <see cref="F:System.String.Empty" />，则为 -1。</returns>
    /// <param name="value">要查找的 Unicode 字符。</param>
    /// <param name="startIndex">搜索的起始位置。从 <paramref name="startIndex" /> 此实例的开头开始搜索。</param>
    /// <param name="count">要检查的字符位置数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">当前实例不等于 <see cref="F:System.String.Empty" />, ，和 <paramref name="startIndex" /> 小于零或大于或等于此实例的长度。- 或 -当前实例不等于 <see cref="F:System.String.Empty" />, ，和 <paramref name="startIndex" /> - <paramref name="count" /> + 1 为小于零。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public int LastIndexOf(char value, int startIndex, int count);

    /// <summary>报告在 Unicode 数组中指定的一个或多个字符在此实例中的最后一个匹配项的从零开始的索引的位置。</summary>
    /// <returns>最后一次在此实例中找到 <paramref name="anyOf" /> 中的任意字符的索引位置；如果未找到 <paramref name="anyOf" /> 中的字符，则为 -1。</returns>
    /// <param name="anyOf">Unicode 字符数组，包含一个或多个要查找的字符。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="anyOf" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public int LastIndexOfAny(char[] anyOf)
    {
      return this.LastIndexOfAny(anyOf, this.Length - 1, this.Length);
    }

    /// <summary>报告在 Unicode 数组中指定的一个或多个字符在此实例中的最后一个匹配项的从零开始的索引的位置。在指定的字符位置开始和在向后的右边该字符串的开头处理的搜索。</summary>
    /// <returns>最后一次在此实例中找到 <paramref name="anyOf" /> 中的任意字符的索引位置；如果未找到 <paramref name="anyOf" /> 中的字符或者当前实例等于<see cref="F:System.String.Empty" />，则为 -1。</returns>
    /// <param name="anyOf">Unicode 字符数组，包含一个或多个要查找的字符。</param>
    /// <param name="startIndex">搜索起始位置。从 <paramref name="startIndex" /> 此实例的开头开始搜索。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="anyOf" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">当前实例不等于 <see cref="F:System.String.Empty" />, ，和 <paramref name="startIndex" /> 指定一个不在此实例的位置。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public int LastIndexOfAny(char[] anyOf, int startIndex)
    {
      char[] anyOf1 = anyOf;
      int startIndex1 = startIndex;
      int num = 1;
      int count = startIndex1 + num;
      return this.LastIndexOfAny(anyOf1, startIndex1, count);
    }

    /// <summary>报告在 Unicode 数组中指定的一个或多个字符在此实例中的最后一个匹配项的从零开始的索引的位置。搜索在指定字符位置的数目的字符串开始时，开始指定字符和其后面的位置。</summary>
    /// <returns>最后一次在此实例中找到 <paramref name="anyOf" /> 中的任意字符的索引位置；如果未找到 <paramref name="anyOf" /> 中的字符或者当前实例等于<see cref="F:System.String.Empty" />，则为 -1。</returns>
    /// <param name="anyOf">Unicode 字符数组，包含一个或多个要查找的字符。</param>
    /// <param name="startIndex">搜索起始位置。从 <paramref name="startIndex" /> 此实例的开头开始搜索。</param>
    /// <param name="count">要检查的字符位置数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="anyOf" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">当前实例不等于 <see cref="F:System.String.Empty" />, ，和 <paramref name="count" /> 或 <paramref name="startIndex" /> 为负。- 或 - 当前实例不等于 <see cref="F:System.String.Empty" />, ，和 <paramref name="startIndex" /> 减去 <paramref name="count" /> + 1 小于零。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public int LastIndexOfAny(char[] anyOf, int startIndex, int count);

    /// <summary>报告指定字符串在此实例中的最后一个匹配项的从零开始的索引的位置。</summary>
    /// <returns>如果找到该字符串，则为 <paramref name="value" /> 的从零开始的起始索引位置；如果未找到该字符串，则为 -1。如果 <paramref name="value" /> 为 <see cref="F:System.String.Empty" />，则返回值为此实例中的最后一个索引位置。</returns>
    /// <param name="value">要搜寻的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int LastIndexOf(string value)
    {
      return this.LastIndexOf(value, this.Length - 1, this.Length, StringComparison.CurrentCulture);
    }

    /// <summary>报告指定字符串在此实例中的最后一个匹配项的从零开始的索引的位置。在指定的字符位置开始和在向后的右边该字符串的开头处理的搜索。</summary>
    /// <returns>如果找到该字符串，则为 <paramref name="value" /> 的从零开始的起始索引位置；如果未找到该字符串或当前实例等于 <see cref="F:System.String.Empty" />，则为 -1。如果 <paramref name="value" /> 为 <see cref="F:System.String.Empty" />，则返回值为 <paramref name="startIndex" /> 和此实例中的最后一个索引位置中的较小者。</returns>
    /// <param name="value">要搜寻的字符串。</param>
    /// <param name="startIndex">搜索起始位置。从 <paramref name="startIndex" /> 此实例的开头开始搜索。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">当前实例不等于 <see cref="F:System.String.Empty" />, ，和 <paramref name="startIndex" /> 小于零或大于当前实例的长度。- 或 -当前实例等于 <see cref="F:System.String.Empty" />, ，和 <paramref name="startIndex" /> 小于-1 或大于零。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int LastIndexOf(string value, int startIndex)
    {
      string str = value;
      int startIndex1 = startIndex;
      int num1 = 1;
      int count = startIndex1 + num1;
      int num2 = 0;
      return this.LastIndexOf(str, startIndex1, count, (StringComparison) num2);
    }

    /// <summary>报告指定字符串在此实例中的最后一个匹配项的从零开始的索引的位置。搜索在指定字符位置的数目的字符串开始时，开始指定字符和其后面的位置。</summary>
    /// <returns>如果找到该字符串，则为 <paramref name="value" /> 的从零开始的起始索引位置；如果未找到该字符串或当前实例等于 <see cref="F:System.String.Empty" />，则为 -1。如果 <paramref name="value" /> 为 <see cref="F:System.String.Empty" />，则返回值为 <paramref name="startIndex" /> 和此实例中的最后一个索引位置中的较小者。</returns>
    /// <param name="value">要搜寻的字符串。</param>
    /// <param name="startIndex">搜索起始位置。从 <paramref name="startIndex" /> 此实例的开头开始搜索。</param>
    /// <param name="count">要检查的字符位置数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 为负。- 或 -当前实例不等于 <see cref="F:System.String.Empty" />, ，和  <paramref name="startIndex" /> 为负。- 或 - 当前实例不等于 <see cref="F:System.String.Empty" />, ，和 <paramref name="startIndex" /> 大于此实例的长度。- 或 -当前实例不等于 <see cref="F:System.String.Empty" />, ，和 <paramref name="startIndex" /> - <paramref name="count" /> + 1 表示指定的位置，则不在此实例。- 或 -当前实例等于 <see cref="F:System.String.Empty" /> 和 <paramref name="start" /> 小于-1 或大于零。- 或 -当前实例等于 <see cref="F:System.String.Empty" /> 和 <paramref name="count" /> 大于 1。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int LastIndexOf(string value, int startIndex, int count)
    {
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
      return this.LastIndexOf(value, startIndex, count, StringComparison.CurrentCulture);
    }

    /// <summary>报告指定字符串在当前 <see cref="T:System.String" /> 对象中最后一个匹配项的从零开始的索引。一个参数指定要用于指定字符串的搜索类型。</summary>
    /// <returns>如果找到该字符串，则为 <paramref name="value" /> 参数的从零开始的起始索引位置；如果未找到该字符串，则为 -1。如果 <paramref name="value" /> 为 <see cref="F:System.String.Empty" />，则返回值为此实例中的最后一个索引位置。</returns>
    /// <param name="value">要搜寻的字符串。</param>
    /// <param name="comparisonType">指定搜索规则的枚举值之一。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="comparisonType" /> 不是有效 <see cref="T:System.StringComparison" /> 值。</exception>
    [__DynamicallyInvokable]
    public int LastIndexOf(string value, StringComparison comparisonType)
    {
      return this.LastIndexOf(value, this.Length - 1, this.Length, comparisonType);
    }

    /// <summary>报告指定字符串在当前 <see cref="T:System.String" /> 对象中最后一个匹配项的从零开始的索引。在指定的字符位置开始和在向后的右边该字符串的开头处理的搜索。一个参数指定要执行搜索指定字符串的比较类型。</summary>
    /// <returns>如果找到该字符串，则为 <paramref name="value" /> 参数的从零开始的起始索引位置；如果未找到该字符串或当前实例等于 <see cref="F:System.String.Empty" />，则为 -1。如果 <paramref name="value" /> 为 <see cref="F:System.String.Empty" />，则返回值为 <paramref name="startIndex" /> 和此实例中的最后一个索引位置中的较小者。</returns>
    /// <param name="value">要搜寻的字符串。</param>
    /// <param name="startIndex">搜索起始位置。从 <paramref name="startIndex" /> 此实例的开头开始搜索。</param>
    /// <param name="comparisonType">指定搜索规则的枚举值之一。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">当前实例不等于 <see cref="F:System.String.Empty" />, ，和 <paramref name="startIndex" /> 小于零或大于当前实例的长度。- 或 -当前实例等于 <see cref="F:System.String.Empty" />, ，和 <paramref name="startIndex" /> 小于-1 或大于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="comparisonType" /> 不是有效 <see cref="T:System.StringComparison" /> 值。</exception>
    [__DynamicallyInvokable]
    public int LastIndexOf(string value, int startIndex, StringComparison comparisonType)
    {
      string str = value;
      int startIndex1 = startIndex;
      int num1 = 1;
      int count = startIndex1 + num1;
      int num2 = (int) comparisonType;
      return this.LastIndexOf(str, startIndex1, count, (StringComparison) num2);
    }

    /// <summary>报告指定字符串在此实例中的最后一个匹配项的从零开始的索引的位置。搜索在所指定的字符位置的数目的字符串开始时，开始指定字符和其后面的位置。一个参数指定要执行搜索指定字符串的比较类型。</summary>
    /// <returns>如果找到该字符串，则为 <paramref name="value" /> 参数的从零开始的起始索引位置；如果未找到该字符串或当前实例等于 <see cref="F:System.String.Empty" />，则为 -1。如果 <paramref name="value" /> 为 <see cref="F:System.String.Empty" />，则返回值为 <paramref name="startIndex" /> 和此实例中的最后一个索引位置中的较小者。</returns>
    /// <param name="value">要搜寻的字符串。</param>
    /// <param name="startIndex">搜索起始位置。从 <paramref name="startIndex" /> 此实例的开头开始搜索。</param>
    /// <param name="count">要检查的字符位置数。</param>
    /// <param name="comparisonType">指定搜索规则的枚举值之一。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 为负。- 或 -当前实例不等于 <see cref="F:System.String.Empty" />, ，和 <paramref name="startIndex" /> 为负。- 或 - 当前实例不等于 <see cref="F:System.String.Empty" />, ，和 <paramref name="startIndex" /> 大于此实例的长度。- 或 -当前实例不等于 <see cref="F:System.String.Empty" />, ，和 <paramref name="startIndex" /> + 1- <paramref name="count" /> 指定一个不在此实例的位置。- 或 -当前实例等于 <see cref="F:System.String.Empty" /> 和 <paramref name="start" /> 小于-1 或大于零。- 或 -当前实例等于 <see cref="F:System.String.Empty" /> 和 <paramref name="count" /> 大于 1。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="comparisonType" /> 不是有效 <see cref="T:System.StringComparison" /> 值。</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public int LastIndexOf(string value, int startIndex, int count, StringComparison comparisonType)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (this.Length == 0 && (startIndex == -1 || startIndex == 0))
        return value.Length != 0 ? -1 : 0;
      if (startIndex < 0 || startIndex > this.Length)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (startIndex == this.Length)
      {
        --startIndex;
        if (count > 0)
          --count;
        if (value.Length == 0 && count >= 0 && startIndex - count + 1 >= 0)
          return startIndex;
      }
      if (count < 0 || startIndex - count + 1 < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
      switch (comparisonType)
      {
        case StringComparison.CurrentCulture:
          return CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.None);
        case StringComparison.CurrentCultureIgnoreCase:
          return CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
        case StringComparison.InvariantCulture:
          return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.None);
        case StringComparison.InvariantCultureIgnoreCase:
          return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
        case StringComparison.Ordinal:
          return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.Ordinal);
        case StringComparison.OrdinalIgnoreCase:
          if (value.IsAscii() && this.IsAscii())
            return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
          return TextInfo.LastIndexOfStringOrdinalIgnoreCase(this, value, startIndex, count);
        default:
          throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
      }
    }

    /// <summary>返回一个新字符串，该字符串通过在此实例中的字符左侧填充空格来达到指定的总长度，从而实现右对齐。</summary>
    /// <returns>与此实例等效的一个新字符串，但该字符串为右对齐，因此，在左侧填充所需任意数量的空格，使长度达到 <paramref name="totalWidth" />。但是，如果 <paramref name="totalWidth" /> 小于此实例的长度，则此方法返回对现有实例的引用。如果 <paramref name="totalWidth" /> 等于此实例的长度，则此方法返回与此实例相同的新字符串。</returns>
    /// <param name="totalWidth">结果字符串中的字符数，等于原始字符数加上任何其他填充字符。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="totalWidth" /> 小于零。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public string PadLeft(int totalWidth)
    {
      return this.PadHelper(totalWidth, ' ', false);
    }

    /// <summary>返回一个新字符串，该字符串通过在此实例中的字符左侧填充指定的 Unicode 字符来达到指定的总长度，从而使这些字符右对齐。</summary>
    /// <returns>与此实例等效的一个新字符串，但该字符串为右对齐，因此，在左侧填充所需任意数量的 <paramref name="paddingChar" /> 字符，使长度达到 <paramref name="totalWidth" />。但是，如果 <paramref name="totalWidth" /> 小于此实例的长度，则此方法返回对现有实例的引用。如果 <paramref name="totalWidth" /> 等于此实例的长度，则此方法返回与此实例相同的新字符串。</returns>
    /// <param name="totalWidth">结果字符串中的字符数，等于原始字符数加上任何其他填充字符。</param>
    /// <param name="paddingChar">Unicode 填充字符。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="totalWidth" /> 小于零。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public string PadLeft(int totalWidth, char paddingChar)
    {
      return this.PadHelper(totalWidth, paddingChar, false);
    }

    /// <summary>返回一个新字符串，该字符串通过在此字符串中的字符右侧填充空格来达到指定的总长度，从而使这些字符左对齐。</summary>
    /// <returns>与此实例等效的一个新字符串，但该字符串为左对齐，因此，在右侧填充所需任意数量的空格，使长度达到 <paramref name="totalWidth" />。但是，如果 <paramref name="totalWidth" /> 小于此实例的长度，则此方法返回对现有实例的引用。如果 <paramref name="totalWidth" /> 等于此实例的长度，则此方法返回与此实例相同的新字符串。</returns>
    /// <param name="totalWidth">结果字符串中的字符数，等于原始字符数加上任何其他填充字符。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="totalWidth" /> 小于零。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public string PadRight(int totalWidth)
    {
      return this.PadHelper(totalWidth, ' ', true);
    }

    /// <summary>返回一个新字符串，该字符串通过在此字符串中的字符右侧填充指定的 Unicode 字符来达到指定的总长度，从而使这些字符左对齐。</summary>
    /// <returns>与此实例等效的一个新字符串，但该字符串为左对齐，因此，在右侧填充所需任意数量的 <paramref name="paddingChar" /> 字符，使长度达到 <paramref name="totalWidth" />。但是，如果 <paramref name="totalWidth" /> 小于此实例的长度，则此方法返回对现有实例的引用。如果 <paramref name="totalWidth" /> 等于此实例的长度，则此方法返回与此实例相同的新字符串。</returns>
    /// <param name="totalWidth">结果字符串中的字符数，等于原始字符数加上任何其他填充字符。</param>
    /// <param name="paddingChar">Unicode 填充字符。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="totalWidth" /> 小于零。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public string PadRight(int totalWidth, char paddingChar)
    {
      return this.PadHelper(totalWidth, paddingChar, true);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private string PadHelper(int totalWidth, char paddingChar, bool isRightPadded);

    /// <summary>确定此字符串实例的开头是否与指定的字符串匹配。</summary>
    /// <returns>如果 true 与此字符串的开头匹配，则为 <paramref name="value" />；否则为 false。</returns>
    /// <param name="value">要比较的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public bool StartsWith(string value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      return this.StartsWith(value, StringComparison.CurrentCulture);
    }

    /// <summary>确定在使用指定的比较选项进行比较时此字符串实例的开头是否与指定的字符串匹配。</summary>
    /// <returns>如果此实例以 true 开头，则为 <paramref name="value" />；否则为 false。</returns>
    /// <param name="value">要比较的字符串。</param>
    /// <param name="comparisonType">枚举值之一，用于确定如何比较此字符串与 <paramref name="value" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="comparisonType" /> 不是 <see cref="T:System.StringComparison" /> 值。</exception>
    [SecuritySafeCritical]
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public bool StartsWith(string value, StringComparison comparisonType)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (comparisonType < StringComparison.CurrentCulture || comparisonType > StringComparison.OrdinalIgnoreCase)
        throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
      if (this == value || value.Length == 0)
        return true;
      switch (comparisonType)
      {
        case StringComparison.CurrentCulture:
          return CultureInfo.CurrentCulture.CompareInfo.IsPrefix(this, value, CompareOptions.None);
        case StringComparison.CurrentCultureIgnoreCase:
          return CultureInfo.CurrentCulture.CompareInfo.IsPrefix(this, value, CompareOptions.IgnoreCase);
        case StringComparison.InvariantCulture:
          return CultureInfo.InvariantCulture.CompareInfo.IsPrefix(this, value, CompareOptions.None);
        case StringComparison.InvariantCultureIgnoreCase:
          return CultureInfo.InvariantCulture.CompareInfo.IsPrefix(this, value, CompareOptions.IgnoreCase);
        case StringComparison.Ordinal:
          if (this.Length < value.Length)
            return false;
          return string.nativeCompareOrdinalEx(this, 0, value, 0, value.Length) == 0;
        case StringComparison.OrdinalIgnoreCase:
          if (this.Length < value.Length)
            return false;
          return TextInfo.CompareOrdinalIgnoreCaseEx(this, 0, value, 0, value.Length, value.Length) == 0;
        default:
          throw new ArgumentException(Environment.GetResourceString("NotSupported_StringComparison"), "comparisonType");
      }
    }

    /// <summary>确定在使用指定的区域性进行比较时此字符串实例的开头是否与指定的字符串匹配。</summary>
    /// <returns>如果 true 参数与此字符串的开头匹配，则为 <paramref name="value" />；否则为 false。</returns>
    /// <param name="value">要比较的字符串。</param>
    /// <param name="ignoreCase">若要在比较过程中忽略大小写，则为 true；否则为 false。</param>
    /// <param name="culture">确定如何对此字符串与 <paramref name="value" /> 进行比较的区域性信息。如果 <paramref name="culture" /> 为 null，则使用当前区域性。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    public bool StartsWith(string value, bool ignoreCase, CultureInfo culture)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (this == value)
        return true;
      return (culture != null ? culture : CultureInfo.CurrentCulture).CompareInfo.IsPrefix(this, value, ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
    }

    /// <summary>返回此字符串转换为小写形式的副本。</summary>
    /// <returns>一个小写字符串。</returns>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public string ToLower()
    {
      return this.ToLower(CultureInfo.CurrentCulture);
    }

    /// <summary>根据指定区域性的大小写规则返回此字符串转换为小写形式的副本。</summary>
    /// <returns>当前字符串的等效小写形式。</returns>
    /// <param name="culture">一个对象，用于提供区域性特定的大小写规则。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="culture" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public string ToLower(CultureInfo culture)
    {
      if (culture == null)
        throw new ArgumentNullException("culture");
      return culture.TextInfo.ToLower(this);
    }

    /// <summary>返回此 <see cref="T:System.String" /> 对象的转换为小写形式的副本，返回时使用固定区域性的大小写规则。</summary>
    /// <returns>当前字符串的等效小写形式。</returns>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public string ToLowerInvariant()
    {
      return this.ToLower(CultureInfo.InvariantCulture);
    }

    /// <summary>返回此字符串转换为大写形式的副本。</summary>
    /// <returns>当前字符串的大写形式。</returns>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public string ToUpper()
    {
      return this.ToUpper(CultureInfo.CurrentCulture);
    }

    /// <summary>根据指定区域性的大小写规则返回此字符串转换为大写形式的副本。</summary>
    /// <returns>当前字符串的大写形式。</returns>
    /// <param name="culture">一个对象，用于提供区域性特定的大小写规则。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="culture" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public string ToUpper(CultureInfo culture)
    {
      if (culture == null)
        throw new ArgumentNullException("culture");
      return culture.TextInfo.ToUpper(this);
    }

    /// <summary>返回此 <see cref="T:System.String" /> 对象的转换为大写形式的副本，返回时使用固定区域性的大小写规则。</summary>
    /// <returns>当前字符串的大写形式。</returns>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public string ToUpperInvariant()
    {
      return this.ToUpper(CultureInfo.InvariantCulture);
    }

    /// <summary>返回 <see cref="T:System.String" /> 的此实例；不执行实际转换。</summary>
    /// <returns>当前的字符串。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this;
    }

    /// <summary>返回 <see cref="T:System.String" /> 的此实例；不执行实际转换。</summary>
    /// <returns>当前的字符串。</returns>
    /// <param name="provider">（保留）一个对象，用于提供区域性特定的格式设置信息。</param>
    /// <filterpriority>1</filterpriority>
    public string ToString(IFormatProvider provider)
    {
      return this;
    }

    /// <summary>返回对此 <see cref="T:System.String" /> 实例的引用。</summary>
    /// <returns>此 <see cref="T:System.String" /> 实例。</returns>
    /// <filterpriority>2</filterpriority>
    public object Clone()
    {
      return (object) this;
    }

    private static bool IsBOMWhitespace(char c)
    {
      return false;
    }

    /// <summary>从当前 <see cref="T:System.String" /> 对象移除所有前导空白字符和尾部空白字符。</summary>
    /// <returns>从当前字符串的开头和结尾删除所有空白字符后剩余的字符串。如果从当前实例无法删除字符，此方法返回未更改的当前实例。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public string Trim()
    {
      return this.TrimHelper(2);
    }

    [SecuritySafeCritical]
    private string TrimHelper(int trimType)
    {
      int end = this.Length - 1;
      int start = 0;
      if (trimType != 1)
      {
        start = 0;
        while (start < this.Length && (char.IsWhiteSpace(this[start]) || string.IsBOMWhitespace(this[start])))
          ++start;
      }
      if (trimType != 0)
      {
        end = this.Length - 1;
        while (end >= start && (char.IsWhiteSpace(this[end]) || string.IsBOMWhitespace(this[start])))
          --end;
      }
      return this.CreateTrimmedString(start, end);
    }

    [SecuritySafeCritical]
    private string TrimHelper(char[] trimChars, int trimType)
    {
      int end = this.Length - 1;
      int start = 0;
      if (trimType != 1)
      {
        for (start = 0; start < this.Length; ++start)
        {
          char ch = this[start];
          int index = 0;
          while (index < trimChars.Length && (int) trimChars[index] != (int) ch)
            ++index;
          if (index == trimChars.Length)
            break;
        }
      }
      if (trimType != 0)
      {
        for (end = this.Length - 1; end >= start; --end)
        {
          char ch = this[end];
          int index = 0;
          while (index < trimChars.Length && (int) trimChars[index] != (int) ch)
            ++index;
          if (index == trimChars.Length)
            break;
        }
      }
      return this.CreateTrimmedString(start, end);
    }

    [SecurityCritical]
    private string CreateTrimmedString(int start, int end)
    {
      int length = end - start + 1;
      if (length == this.Length)
        return this;
      if (length == 0)
        return string.Empty;
      return this.InternalSubString(start, length);
    }

    /// <summary>返回一个新的字符串，在此实例中的指定的索引位置插入指定的字符串。</summary>
    /// <returns>与此实例等效的一个新字符串，但在该字符串的 <paramref name="value" /> 位置处插入了 <paramref name="startIndex" />。</returns>
    /// <param name="startIndex">插入的从零开始的索引位置。</param>
    /// <param name="value">要插入的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 为负数或大于此实例的长度。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe string Insert(int startIndex, string value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (startIndex < 0 || startIndex > this.Length)
        throw new ArgumentOutOfRangeException("startIndex");
      int length1 = this.Length;
      int length2 = value.Length;
      int length3 = length1 + length2;
      if (length3 == 0)
        return string.Empty;
      string str = string.FastAllocateString(length3);
      fixed (char* smem1 = &this.m_firstChar)
        fixed (char* smem2 = &value.m_firstChar)
          fixed (char* dmem = &str.m_firstChar)
          {
            string.wstrcpy(dmem, smem1, startIndex);
            string.wstrcpy(dmem + startIndex, smem2, length2);
            string.wstrcpy(dmem + startIndex + length2, smem1 + startIndex, length1 - startIndex);
          }
      return str;
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private string ReplaceInternal(char oldChar, char newChar);

    /// <summary>返回一个新字符串，其中此实例中出现的所有指定 Unicode 字符都替换为另一个指定的 Unicode 字符。</summary>
    /// <returns>等效于此实例（除了 <paramref name="oldChar" /> 的所有实例都已替换为 <paramref name="newChar" /> 外）的字符串。如果在当前实例中找不到 <paramref name="oldChar" />，此方法返回未更改的当前实例。</returns>
    /// <param name="oldChar">要替换的 Unicode 字符。</param>
    /// <param name="newChar">要替换出现的所有 <paramref name="oldChar" /> 的 Unicode 字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public string Replace(char oldChar, char newChar)
    {
      return this.ReplaceInternal(oldChar, newChar);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private string ReplaceInternal(string oldValue, string newValue);

    /// <summary>返回一个新字符串，其中当前实例中出现的所有指定字符串都替换为另一个指定的字符串。</summary>
    /// <returns>等效于当前字符串（除了 <paramref name="oldValue" /> 的所有实例都已替换为 <paramref name="newValue" /> 外）的字符串。如果在当前实例中找不到 <paramref name="oldValue" />，此方法返回未更改的当前实例。</returns>
    /// <param name="oldValue">要替换的字符串。</param>
    /// <param name="newValue">要替换 <paramref name="oldValue" /> 的所有匹配项的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="oldValue" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="oldValue" /> 为空字符串 ("")。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public string Replace(string oldValue, string newValue)
    {
      if (oldValue == null)
        throw new ArgumentNullException("oldValue");
      return this.ReplaceInternal(oldValue, newValue);
    }

    /// <summary>返回指定数量字符在当前这个实例起始点在已删除的指定的位置的新字符串。</summary>
    /// <returns>一个新字符串，除所删除的字符之外，该字符串与此实例等效。</returns>
    /// <param name="startIndex">开始删除字符的从零开始的位置。</param>
    /// <param name="count">要删除的字符数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">要么 <paramref name="startIndex" /> 或 <paramref name="count" /> 小于零。- 或 - <paramref name="startIndex" /> 加上 <paramref name="count" /> 指定此实例外部的位置。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe string Remove(int startIndex, int count)
    {
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
      if (count > this.Length - startIndex)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_IndexCount"));
      int length = this.Length - count;
      if (length == 0)
        return string.Empty;
      string str = string.FastAllocateString(length);
      fixed (char* smem = &this.m_firstChar)
        fixed (char* dmem = &str.m_firstChar)
        {
          string.wstrcpy(dmem, smem, startIndex);
          string.wstrcpy(dmem + startIndex, smem + startIndex + count, length - startIndex);
        }
      return str;
    }

    /// <summary>返回当前实例中从指定位置到最后位置的所有以删除的字符的新字符串。</summary>
    /// <returns>一个新字符串，除所删除的字符之外，该字符串与此字符串等效。</returns>
    /// <param name="startIndex">开始删除字符的从零开始的位置。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 小于零。- 或 - <paramref name="startIndex" /> 指定的位置，则不在此字符串中。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public string Remove(int startIndex)
    {
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (startIndex >= this.Length)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndexLessThanLength"));
      return this.Substring(0, startIndex);
    }

    /// <summary>将指定字符串中的一个或多个格式项替换为指定对象的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="format" /> 的副本，其中的任何格式项均替换为 <paramref name="arg0" /> 的字符串表示形式。</returns>
    /// <param name="format">复合格式字符串。</param>
    /// <param name="arg0">要设置格式的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">中的格式项 <paramref name="format" /> 无效。- 或 - 格式项的索引不为零。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string Format(string format, object arg0)
    {
      return string.FormatHelper((IFormatProvider) null, format, new ParamsArray(arg0));
    }

    /// <summary>将指定字符串中的格式项替换为两个指定对象的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="format" /> 的副本，其中的格式项替换为 <paramref name="arg0" /> 和 <paramref name="arg1" /> 的字符串表示形式。</returns>
    /// <param name="format">复合格式字符串。</param>
    /// <param name="arg0">要设置格式的第一个对象。</param>
    /// <param name="arg1">要设置格式的第二个对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 无效。- 或 - 格式项的索引不是零个或一个。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string Format(string format, object arg0, object arg1)
    {
      return string.FormatHelper((IFormatProvider) null, format, new ParamsArray(arg0, arg1));
    }

    /// <summary>将指定字符串中的格式项替换为三个指定对象的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="format" /> 的副本，其中的格式项已替换为 <paramref name="arg0" />、<paramref name="arg1" /> 和 <paramref name="arg2" /> 的字符串表示形式。</returns>
    /// <param name="format">复合格式字符串。</param>
    /// <param name="arg0">要设置格式的第一个对象。</param>
    /// <param name="arg1">要设置格式的第二个对象。</param>
    /// <param name="arg2">要设置格式的第三个对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 无效。- 或 - 格式项的索引小于零，或两个以上。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string Format(string format, object arg0, object arg1, object arg2)
    {
      return string.FormatHelper((IFormatProvider) null, format, new ParamsArray(arg0, arg1, arg2));
    }

    /// <summary>将指定字符串中的格式项替换为指定数组中相应对象的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="format" /> 的副本，其中格式项已替换为 <paramref name="args" /> 中相应对象的字符串表示形式。</returns>
    /// <param name="format">复合格式字符串。</param>
    /// <param name="args">一个对象数组，其中包含零个或多个要设置格式的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 或 <paramref name="args" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 无效。- 或 - 格式项的索引小于零，或者大于或等于的长度 <paramref name="args" /> 数组。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string Format(string format, params object[] args)
    {
      if (args == null)
        throw new ArgumentNullException(format == null ? "format" : "args");
      return string.FormatHelper((IFormatProvider) null, format, new ParamsArray(args));
    }

    /// <summary>将指定字符串中的一个或多个格式项替换为对应对象的字符串表示形式。参数提供区域性特定的格式设置信息。</summary>
    /// <returns>
    /// <paramref name="format" /> 的副本，其中的一个或多个格式项已替换为 <paramref name="arg0" /> 的字符串表示形式。</returns>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <param name="format">复合格式字符串。</param>
    /// <param name="arg0">要设置格式的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 或 <paramref name="arg0" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 无效。- 或 - 格式项的索引小于零，或者大于或等于 1。</exception>
    [__DynamicallyInvokable]
    public static string Format(IFormatProvider provider, string format, object arg0)
    {
      return string.FormatHelper(provider, format, new ParamsArray(arg0));
    }

    /// <summary>将指定字符串中的格式项替换为两个指定对象的字符串表示形式。参数提供区域性特定的格式设置信息。</summary>
    /// <returns>
    /// <paramref name="format" /> 的副本，其中的格式项替换为 <paramref name="arg0" /> 和 <paramref name="arg1" /> 的字符串表示形式。</returns>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <param name="format">复合格式字符串。</param>
    /// <param name="arg0">要设置格式的第一个对象。</param>
    /// <param name="arg1">要设置格式的第二个对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" />, <paramref name="arg0" />, or <paramref name="arg1" /> is null.</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 无效。- 或 - 格式项的索引小于零，或者大于或等于两个。</exception>
    [__DynamicallyInvokable]
    public static string Format(IFormatProvider provider, string format, object arg0, object arg1)
    {
      return string.FormatHelper(provider, format, new ParamsArray(arg0, arg1));
    }

    /// <summary>将指定字符串中的格式项替换为三个指定对象的字符串表示形式。参数提供区域性特定的格式设置信息。</summary>
    /// <returns>
    /// <paramref name="format" /> 的副本，其中的格式项已替换为 <paramref name="arg0" />、<paramref name="arg1" /> 和 <paramref name="arg2" /> 的字符串表示形式。</returns>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <param name="format">复合格式字符串。</param>
    /// <param name="arg0">要设置格式的第一个对象。</param>
    /// <param name="arg1">要设置格式的第二个对象。</param>
    /// <param name="arg2">要设置格式的第三个对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" />, <paramref name="arg0" />, <paramref name="arg1" />, or <paramref name="arg2" /> is null.</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 无效。- 或 - 格式项的索引小于零，或者大于或等于为三个。</exception>
    [__DynamicallyInvokable]
    public static string Format(IFormatProvider provider, string format, object arg0, object arg1, object arg2)
    {
      return string.FormatHelper(provider, format, new ParamsArray(arg0, arg1, arg2));
    }

    /// <summary>将指定字符串中的格式项替换为指定数组中相应对象的字符串表示形式。参数提供区域性特定的格式设置信息。</summary>
    /// <returns>
    /// <paramref name="format" /> 的副本，其中格式项已替换为 <paramref name="args" /> 中相应对象的字符串表示形式。</returns>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <param name="format">复合格式字符串。</param>
    /// <param name="args">一个对象数组，其中包含零个或多个要设置格式的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 或 <paramref name="args" /> 为 null。</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 无效。- 或 - 格式项的索引小于零，或者大于或等于的长度 <paramref name="args" /> 数组。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string Format(IFormatProvider provider, string format, params object[] args)
    {
      if (args == null)
        throw new ArgumentNullException(format == null ? "format" : "args");
      return string.FormatHelper(provider, format, new ParamsArray(args));
    }

    private static string FormatHelper(IFormatProvider provider, string format, ParamsArray args)
    {
      if (format == null)
        throw new ArgumentNullException("format");
      return StringBuilderCache.GetStringAndRelease(StringBuilderCache.Acquire(format.Length + args.Length * 8).AppendFormatHelper(provider, format, args));
    }

    /// <summary>创建一个与指定的 <see cref="T:System.String" /> 具有相同值的 <see cref="T:System.String" /> 的新实例。</summary>
    /// <returns>值与 <paramref name="str" /> 相同的新字符串。</returns>
    /// <param name="str">要复制的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="str" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public static unsafe string Copy(string str)
    {
      if (str == null)
        throw new ArgumentNullException("str");
      int length = str.Length;
      string str1 = string.FastAllocateString(length);
      fixed (char* dmem = &str1.m_firstChar)
        fixed (char* smem = &str.m_firstChar)
          string.wstrcpy(dmem, smem, length);
      return str1;
    }

    /// <summary>创建指定对象的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="arg0" /> 的值的字符串表示形式，如果 <see cref="F:System.String.Empty" /> 为 <paramref name="arg0" />，则为 null。</returns>
    /// <param name="arg0">要表示的对象，或 null。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string Concat(object arg0)
    {
      if (arg0 == null)
        return string.Empty;
      return arg0.ToString();
    }

    /// <summary>连接两个指定对象的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="arg0" /> 和 <paramref name="arg1" /> 的值的串联字符串表示形式。</returns>
    /// <param name="arg0">要连接的第一个对象。</param>
    /// <param name="arg1">要连接的第二个对象。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string Concat(object arg0, object arg1)
    {
      if (arg0 == null)
        arg0 = (object) string.Empty;
      if (arg1 == null)
        arg1 = (object) string.Empty;
      return arg0.ToString() + arg1.ToString();
    }

    /// <summary>连接三个指定对象的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="arg0" />、<paramref name="arg1" /> 和 <paramref name="arg2" /> 的值的串联字符串表示形式。</returns>
    /// <param name="arg0">要连接的第一个对象。</param>
    /// <param name="arg1">要连接的第二个对象。</param>
    /// <param name="arg2">要连接的第三个对象。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string Concat(object arg0, object arg1, object arg2)
    {
      if (arg0 == null)
        arg0 = (object) string.Empty;
      if (arg1 == null)
        arg1 = (object) string.Empty;
      if (arg2 == null)
        arg2 = (object) string.Empty;
      return arg0.ToString() + arg1.ToString() + arg2.ToString();
    }

    [CLSCompliant(false)]
    public static string Concat(object arg0, object arg1, object arg2, object arg3, __arglist)
    {
      ArgIterator argIterator = new ArgIterator(__arglist);
      int length = argIterator.GetRemainingCount() + 4;
      object[] objArray = new object[length];
      objArray[0] = arg0;
      objArray[1] = arg1;
      objArray[2] = arg2;
      objArray[3] = arg3;
      for (int index = 4; index < length; ++index)
        objArray[index] = TypedReference.ToObject(argIterator.GetNextArg());
      return string.Concat(objArray);
    }

    /// <summary>连接指定 <see cref="T:System.Object" /> 数组中的元素的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="args" /> 中元素的值的串联字符串表示形式。</returns>
    /// <param name="args">一个对象数组，其中包含要连接的元素。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="args" /> 为 null。</exception>
    /// <exception cref="T:System.OutOfMemoryException">内存不足。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string Concat(params object[] args)
    {
      if (args == null)
        throw new ArgumentNullException("args");
      string[] values = new string[args.Length];
      int totalLength = 0;
      for (int index = 0; index < args.Length; ++index)
      {
        object obj = args[index];
        values[index] = obj == null ? string.Empty : obj.ToString();
        if (values[index] == null)
          values[index] = string.Empty;
        totalLength += values[index].Length;
        if (totalLength < 0)
          throw new OutOfMemoryException();
      }
      return string.ConcatArray(values, totalLength);
    }

    /// <summary>串联 <see cref="T:System.Collections.Generic.IEnumerable`1" /> 实现的成员。</summary>
    /// <returns>
    /// <paramref name="values" /> 中的串联成员。</returns>
    /// <param name="values">一个实现 <see cref="T:System.Collections.Generic.IEnumerable`1" /> 接口的集合对象。</param>
    /// <typeparam name="T">
    /// <paramref name="values" /> 成员的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="values" /> 为 null。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public static string Concat<T>(IEnumerable<T> values)
    {
      if (values == null)
        throw new ArgumentNullException("values");
      StringBuilder sb = StringBuilderCache.Acquire(16);
      using (IEnumerator<T> enumerator = values.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          if ((object) enumerator.Current != null)
          {
            string @string = enumerator.Current.ToString();
            if (@string != null)
              sb.Append(@string);
          }
        }
      }
      return StringBuilderCache.GetStringAndRelease(sb);
    }

    /// <summary>串联类型为 <see cref="T:System.Collections.Generic.IEnumerable`1" /> 的 <see cref="T:System.String" /> 构造集合的成员。</summary>
    /// <returns>
    /// <paramref name="values" /> 中的串联字符串。</returns>
    /// <param name="values">一个集合对象，该对象实现 <see cref="T:System.Collections.Generic.IEnumerable`1" />，且其泛型类型参数为 <see cref="T:System.String" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="values" /> 为 null。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public static string Concat(IEnumerable<string> values)
    {
      if (values == null)
        throw new ArgumentNullException("values");
      StringBuilder sb = StringBuilderCache.Acquire(16);
      using (IEnumerator<string> enumerator = values.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          if (enumerator.Current != null)
            sb.Append(enumerator.Current);
        }
      }
      return StringBuilderCache.GetStringAndRelease(sb);
    }

    /// <summary>连接 <see cref="T:System.String" /> 的两个指定实例。</summary>
    /// <returns>
    /// <paramref name="str0" /> 和 <paramref name="str1" /> 的串联。</returns>
    /// <param name="str0">要串联的第一个字符串。</param>
    /// <param name="str1">要串联的第二个字符串。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string Concat(string str0, string str1)
    {
      if (string.IsNullOrEmpty(str0))
      {
        if (string.IsNullOrEmpty(str1))
          return string.Empty;
        return str1;
      }
      if (string.IsNullOrEmpty(str1))
        return str0;
      int length = str0.Length;
      string dest = string.FastAllocateString(length + str1.Length);
      int destPos1 = 0;
      string src1 = str0;
      string.FillStringChecked(dest, destPos1, src1);
      int destPos2 = length;
      string src2 = str1;
      string.FillStringChecked(dest, destPos2, src2);
      return dest;
    }

    /// <summary>连接 <see cref="T:System.String" /> 的三个指定实例。</summary>
    /// <returns>
    /// <paramref name="str0" />、<paramref name="str1" /> 和 <paramref name="str2" /> 的串联。</returns>
    /// <param name="str0">要串联的第一个字符串。</param>
    /// <param name="str1">要串联的第二个字符串。</param>
    /// <param name="str2">要比较的第三个字符串。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string Concat(string str0, string str1, string str2)
    {
      if (str0 == null && str1 == null && str2 == null)
        return string.Empty;
      if (str0 == null)
        str0 = string.Empty;
      if (str1 == null)
        str1 = string.Empty;
      if (str2 == null)
        str2 = string.Empty;
      string dest = string.FastAllocateString(str0.Length + str1.Length + str2.Length);
      int destPos1 = 0;
      string src1 = str0;
      string.FillStringChecked(dest, destPos1, src1);
      int length = str0.Length;
      string src2 = str1;
      string.FillStringChecked(dest, length, src2);
      int destPos2 = str0.Length + str1.Length;
      string src3 = str2;
      string.FillStringChecked(dest, destPos2, src3);
      return dest;
    }

    /// <summary>连接 <see cref="T:System.String" /> 的四个指定实例。</summary>
    /// <returns>
    /// <paramref name="str0" />、<paramref name="str1" />、<paramref name="str2" /> 和 <paramref name="str3" /> 的串联。</returns>
    /// <param name="str0">要串联的第一个字符串。</param>
    /// <param name="str1">要串联的第二个字符串。</param>
    /// <param name="str2">要比较的第三个字符串。</param>
    /// <param name="str3">要比较的第四个字符串。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static string Concat(string str0, string str1, string str2, string str3)
    {
      if (str0 == null && str1 == null && (str2 == null && str3 == null))
        return string.Empty;
      if (str0 == null)
        str0 = string.Empty;
      if (str1 == null)
        str1 = string.Empty;
      if (str2 == null)
        str2 = string.Empty;
      if (str3 == null)
        str3 = string.Empty;
      string dest = string.FastAllocateString(str0.Length + str1.Length + str2.Length + str3.Length);
      int destPos1 = 0;
      string src1 = str0;
      string.FillStringChecked(dest, destPos1, src1);
      int length = str0.Length;
      string src2 = str1;
      string.FillStringChecked(dest, length, src2);
      int destPos2 = str0.Length + str1.Length;
      string src3 = str2;
      string.FillStringChecked(dest, destPos2, src3);
      int destPos3 = str0.Length + str1.Length + str2.Length;
      string src4 = str3;
      string.FillStringChecked(dest, destPos3, src4);
      return dest;
    }

    [SecuritySafeCritical]
    private static string ConcatArray(string[] values, int totalLength)
    {
      string dest = string.FastAllocateString(totalLength);
      int destPos = 0;
      for (int index = 0; index < values.Length; ++index)
      {
        string.FillStringChecked(dest, destPos, values[index]);
        destPos += values[index].Length;
      }
      return dest;
    }

    /// <summary>连接指定的 <see cref="T:System.String" /> 数组的元素。</summary>
    /// <returns>
    /// <paramref name="values" /> 的串联元素。</returns>
    /// <param name="values">字符串实例的数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="values" /> 为 null。</exception>
    /// <exception cref="T:System.OutOfMemoryException">内存不足。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static string Concat(params string[] values)
    {
      if (values == null)
        throw new ArgumentNullException("values");
      int totalLength = 0;
      string[] values1 = new string[values.Length];
      for (int index = 0; index < values.Length; ++index)
      {
        string str = values[index];
        values1[index] = str == null ? string.Empty : str;
        totalLength += values1[index].Length;
        if (totalLength < 0)
          throw new OutOfMemoryException();
      }
      return string.ConcatArray(values1, totalLength);
    }

    /// <summary>检索系统对指定 <see cref="T:System.String" /> 的引用。</summary>
    /// <returns>如果暂存了 <paramref name="str" />，则返回系统对其的引用；否则返回对值为 <paramref name="str" /> 的字符串的新引用。</returns>
    /// <param name="str">要在暂存池中搜索的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="str" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public static string Intern(string str)
    {
      if (str == null)
        throw new ArgumentNullException("str");
      return Thread.GetDomain().GetOrInternString(str);
    }

    /// <summary>检索对指定 <see cref="T:System.String" /> 的引用。</summary>
    /// <returns>如果 <paramref name="str" /> 在公共语言运行时的暂存池中，则返回对它的引用；否则返回 null。</returns>
    /// <param name="str">要在暂存池中搜索的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="str" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public static string IsInterned(string str)
    {
      if (str == null)
        throw new ArgumentNullException("str");
      return Thread.GetDomain().IsStringInterned(str);
    }

    /// <summary>返回类 <see cref="T:System.TypeCode" /> 的 <see cref="T:System.String" />。</summary>
    /// <returns>枚举常数 <see cref="F:System.TypeCode.String" />。</returns>
    /// <filterpriority>2</filterpriority>
    public TypeCode GetTypeCode()
    {
      return TypeCode.String;
    }

    [__DynamicallyInvokable]
    bool IConvertible.ToBoolean(IFormatProvider provider)
    {
      return Convert.ToBoolean(this, provider);
    }

    [__DynamicallyInvokable]
    char IConvertible.ToChar(IFormatProvider provider)
    {
      return Convert.ToChar(this, provider);
    }

    [__DynamicallyInvokable]
    sbyte IConvertible.ToSByte(IFormatProvider provider)
    {
      return Convert.ToSByte(this, provider);
    }

    [__DynamicallyInvokable]
    byte IConvertible.ToByte(IFormatProvider provider)
    {
      return Convert.ToByte(this, provider);
    }

    [__DynamicallyInvokable]
    short IConvertible.ToInt16(IFormatProvider provider)
    {
      return Convert.ToInt16(this, provider);
    }

    [__DynamicallyInvokable]
    ushort IConvertible.ToUInt16(IFormatProvider provider)
    {
      return Convert.ToUInt16(this, provider);
    }

    [__DynamicallyInvokable]
    int IConvertible.ToInt32(IFormatProvider provider)
    {
      return Convert.ToInt32(this, provider);
    }

    [__DynamicallyInvokable]
    uint IConvertible.ToUInt32(IFormatProvider provider)
    {
      return Convert.ToUInt32(this, provider);
    }

    [__DynamicallyInvokable]
    long IConvertible.ToInt64(IFormatProvider provider)
    {
      return Convert.ToInt64(this, provider);
    }

    [__DynamicallyInvokable]
    ulong IConvertible.ToUInt64(IFormatProvider provider)
    {
      return Convert.ToUInt64(this, provider);
    }

    [__DynamicallyInvokable]
    float IConvertible.ToSingle(IFormatProvider provider)
    {
      return Convert.ToSingle(this, provider);
    }

    [__DynamicallyInvokable]
    double IConvertible.ToDouble(IFormatProvider provider)
    {
      return Convert.ToDouble(this, provider);
    }

    [__DynamicallyInvokable]
    Decimal IConvertible.ToDecimal(IFormatProvider provider)
    {
      return Convert.ToDecimal(this, provider);
    }

    [__DynamicallyInvokable]
    DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
      return Convert.ToDateTime(this, provider);
    }

    [__DynamicallyInvokable]
    object IConvertible.ToType(Type type, IFormatProvider provider)
    {
      return Convert.DefaultToType((IConvertible) this, type, provider);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal bool IsFastSort();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal bool IsAscii();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal void SetTrailByte(byte data);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal bool TryGetTrailByte(out byte data);

    /// <summary>检索一个可以循环访问此字符串中的每个字符的对象。</summary>
    /// <returns>枚举器对象。</returns>
    /// <filterpriority>2</filterpriority>
    public CharEnumerator GetEnumerator()
    {
      return new CharEnumerator(this);
    }

    [__DynamicallyInvokable]
    IEnumerator<char> IEnumerable<char>.GetEnumerator()
    {
      return (IEnumerator<char>) new CharEnumerator(this);
    }

    [__DynamicallyInvokable]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new CharEnumerator(this);
    }

    [SecurityCritical]
    internal static unsafe void InternalCopy(string src, IntPtr dest, int len)
    {
      if (len == 0)
        return;
      fixed (char* chPtr = &src.m_firstChar)
        Buffer.Memcpy((byte*) (void*) dest, (byte*) chPtr, len);
    }
  }
}
