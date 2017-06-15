// Decompiled with JetBrains decompiler
// Type: System.Text.StringBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
  /// <summary>表示可变字符字符串。此类不能被继承。若要浏览此类型的.NET Framework 源代码，请参阅参考源。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class StringBuilder : ISerializable
  {
    internal char[] m_ChunkChars;
    internal StringBuilder m_ChunkPrevious;
    internal int m_ChunkLength;
    internal int m_ChunkOffset;
    internal int m_MaxCapacity;
    internal const int DefaultCapacity = 16;
    private const string CapacityField = "Capacity";
    private const string MaxCapacityField = "m_MaxCapacity";
    private const string StringValueField = "m_StringValue";
    private const string ThreadIDField = "m_currentThread";
    internal const int MaxChunkSize = 8000;

    /// <summary>获取或设置可包含在当前实例所分配的内存中的最大字符数。</summary>
    /// <returns>可包含在当前实例所分配的内存中的最大字符数。其值可以介于<see cref="P:System.Text.StringBuilder.Length" />到<see cref="P:System.Text.StringBuilder.MaxCapacity" />。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">为设置操作指定的值小于此实例的当前长度。- 或 -为设置操作指定的值大于最大容量。 </exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public int Capacity
    {
      [__DynamicallyInvokable] get
      {
        return this.m_ChunkChars.Length + this.m_ChunkOffset;
      }
      [__DynamicallyInvokable] set
      {
        if (value < 0)
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NegativeCapacity"));
        if (value > this.MaxCapacity)
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_Capacity"));
        if (value < this.Length)
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
        if (this.Capacity == value)
          return;
        char[] chArray = new char[value - this.m_ChunkOffset];
        Array.Copy((Array) this.m_ChunkChars, (Array) chArray, this.m_ChunkLength);
        this.m_ChunkChars = chArray;
      }
    }

    /// <summary>获取此实例的最大容量。</summary>
    /// <returns>此实例可容纳的最大字符数。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public int MaxCapacity
    {
      [__DynamicallyInvokable] get
      {
        return this.m_MaxCapacity;
      }
    }

    /// <summary>获取或设置当前 <see cref="T:System.Text.StringBuilder" /> 对象的长度。</summary>
    /// <returns>此实例的长度。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">为设置操作指定的值小于零或大于 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public int Length
    {
      [__DynamicallyInvokable] get
      {
        return this.m_ChunkOffset + this.m_ChunkLength;
      }
      [__DynamicallyInvokable] set
      {
        if (value < 0)
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
        if (value > this.MaxCapacity)
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
        int capacity = this.Capacity;
        if (value == 0 && this.m_ChunkPrevious == null)
        {
          this.m_ChunkLength = 0;
          this.m_ChunkOffset = 0;
        }
        else
        {
          int repeatCount = value - this.Length;
          if (repeatCount > 0)
          {
            this.Append(char.MinValue, repeatCount);
          }
          else
          {
            StringBuilder chunkForIndex = this.FindChunkForIndex(value);
            if (chunkForIndex != this)
            {
              char[] chArray = new char[capacity - chunkForIndex.m_ChunkOffset];
              Array.Copy((Array) chunkForIndex.m_ChunkChars, (Array) chArray, chunkForIndex.m_ChunkLength);
              this.m_ChunkChars = chArray;
              this.m_ChunkPrevious = chunkForIndex.m_ChunkPrevious;
              this.m_ChunkOffset = chunkForIndex.m_ChunkOffset;
            }
            this.m_ChunkLength = value - chunkForIndex.m_ChunkOffset;
          }
        }
      }
    }

    /// <summary>获取或设置此实例中指定字符位置处的字符。</summary>
    /// <returns>
    /// <paramref name="index" /> 位置处的 Unicode 字符。</returns>
    /// <param name="index">字符的位置。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">在设置一个字符时 <paramref name="index" /> 在此实例的范围之外。 </exception>
    /// <exception cref="T:System.IndexOutOfRangeException">在获取一个字符时 <paramref name="index" /> 在此实例的范围之外。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    [IndexerName("Chars")]
    public char this[int index]
    {
      [__DynamicallyInvokable] get
      {
        StringBuilder stringBuilder = this;
        do
        {
          int index1 = index - stringBuilder.m_ChunkOffset;
          if (index1 >= 0)
          {
            if (index1 >= stringBuilder.m_ChunkLength)
              throw new IndexOutOfRangeException();
            return stringBuilder.m_ChunkChars[index1];
          }
          stringBuilder = stringBuilder.m_ChunkPrevious;
        }
        while (stringBuilder != null);
        throw new IndexOutOfRangeException();
      }
      [__DynamicallyInvokable] set
      {
        StringBuilder stringBuilder = this;
        do
        {
          int index1 = index - stringBuilder.m_ChunkOffset;
          if (index1 >= 0)
          {
            if (index1 >= stringBuilder.m_ChunkLength)
              throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
            stringBuilder.m_ChunkChars[index1] = value;
            return;
          }
          stringBuilder = stringBuilder.m_ChunkPrevious;
        }
        while (stringBuilder != null);
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      }
    }

    /// <summary>初始化 <see cref="T:System.Text.StringBuilder" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public StringBuilder()
      : this(16)
    {
    }

    /// <summary>使用指定的容量初始化 <see cref="T:System.Text.StringBuilder" /> 类的新实例。</summary>
    /// <param name="capacity">此实例的建议起始大小。 </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="capacity" /> 小于零。</exception>
    [__DynamicallyInvokable]
    public StringBuilder(int capacity)
      : this(string.Empty, capacity)
    {
    }

    /// <summary>使用指定的字符串初始化 <see cref="T:System.Text.StringBuilder" /> 类的新实例。</summary>
    /// <param name="value">用于初始化实例值的字符串。如果 <paramref name="value" /> 为 null，则新的 <see cref="T:System.Text.StringBuilder" /> 将包含空字符串（即包含 <see cref="F:System.String.Empty" />）。</param>
    [__DynamicallyInvokable]
    public StringBuilder(string value)
      : this(value, 16)
    {
    }

    /// <summary>使用指定的字符串和容量初始化 <see cref="T:System.Text.StringBuilder" /> 类的新实例。</summary>
    /// <param name="value">用于初始化实例值的字符串。如果 <paramref name="value" /> 为 null，则新的 <see cref="T:System.Text.StringBuilder" /> 将包含空字符串（即包含 <see cref="F:System.String.Empty" />）。</param>
    /// <param name="capacity">
    /// <see cref="T:System.Text.StringBuilder" /> 的建议起始大小。 </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="capacity" /> 小于零。</exception>
    [__DynamicallyInvokable]
    public StringBuilder(string value, int capacity)
      : this(value, 0, value != null ? value.Length : 0, capacity)
    {
    }

    /// <summary>用指定的子字符串和容量初始化 <see cref="T:System.Text.StringBuilder" /> 类的新实例。</summary>
    /// <param name="value">字符串包含用于初始化此实例值的子字符串。如果 <paramref name="value" /> 为 null，则新的 <see cref="T:System.Text.StringBuilder" /> 将包含空字符串（即包含 <see cref="F:System.String.Empty" />）。</param>
    /// <param name="startIndex">
    /// <paramref name="value" /> 中子字符串开始的位置。</param>
    /// <param name="length">子字符串中的字符数。</param>
    /// <param name="capacity">
    /// <see cref="T:System.Text.StringBuilder" /> 的建议起始大小。 </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="capacity" /> 小于零。- 或 - <paramref name="startIndex" /> 加上 <paramref name="length" /> 不是 <paramref name="value" /> 中的位置。</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder(string value, int startIndex, int length, int capacity)
    {
      if (capacity < 0)
        throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_MustBePositive", (object) "capacity"));
      if (length < 0)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_MustBeNonNegNum", (object) "length"));
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (value == null)
        value = string.Empty;
      if (startIndex > value.Length - length)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_IndexLength"));
      this.m_MaxCapacity = int.MaxValue;
      if (capacity == 0)
        capacity = 16;
      if (capacity < length)
        capacity = length;
      this.m_ChunkChars = new char[capacity];
      this.m_ChunkLength = length;
      string str = value;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      StringBuilder.ThreadSafeCopy(chPtr + startIndex, this.m_ChunkChars, 0, length);
      str = (string) null;
    }

    /// <summary>初始化 <see cref="T:System.Text.StringBuilder" /> 类的新实例，该类起始于指定容量并且可增长到指定的最大容量。</summary>
    /// <param name="capacity">
    /// <see cref="T:System.Text.StringBuilder" /> 的建议起始大小。</param>
    /// <param name="maxCapacity">当前字符串可包含的最大字符数。 </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="maxCapacity" /> 小于一，<paramref name="capacity" /> 小于零，或 <paramref name="capacity" /> 大于 <paramref name="maxCapacity" />。</exception>
    [__DynamicallyInvokable]
    public StringBuilder(int capacity, int maxCapacity)
    {
      if (capacity > maxCapacity)
        throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_Capacity"));
      if (maxCapacity < 1)
        throw new ArgumentOutOfRangeException("maxCapacity", Environment.GetResourceString("ArgumentOutOfRange_SmallMaxCapacity"));
      if (capacity < 0)
        throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_MustBePositive", (object) "capacity"));
      if (capacity == 0)
        capacity = Math.Min(16, maxCapacity);
      this.m_MaxCapacity = maxCapacity;
      this.m_ChunkChars = new char[capacity];
    }

    [SecurityCritical]
    private StringBuilder(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      int length = 0;
      string str = (string) null;
      int num = int.MaxValue;
      bool flag = false;
      SerializationInfoEnumerator enumerator = info.GetEnumerator();
      while (enumerator.MoveNext())
      {
        string name = enumerator.Name;
        if (!(name == "m_MaxCapacity"))
        {
          if (!(name == "m_StringValue"))
          {
            if (name == "Capacity")
            {
              length = info.GetInt32("Capacity");
              flag = true;
            }
          }
          else
            str = info.GetString("m_StringValue");
        }
        else
          num = info.GetInt32("m_MaxCapacity");
      }
      if (str == null)
        str = string.Empty;
      if (num < 1 || str.Length > num)
        throw new SerializationException(Environment.GetResourceString("Serialization_StringBuilderMaxCapacity"));
      if (!flag)
      {
        length = 16;
        if (length < str.Length)
          length = str.Length;
        if (length > num)
          length = num;
      }
      if (length < 0 || length < str.Length || length > num)
        throw new SerializationException(Environment.GetResourceString("Serialization_StringBuilderCapacity"));
      this.m_MaxCapacity = num;
      this.m_ChunkChars = new char[length];
      str.CopyTo(0, this.m_ChunkChars, 0, str.Length);
      this.m_ChunkLength = str.Length;
      this.m_ChunkPrevious = (StringBuilder) null;
    }

    private StringBuilder(StringBuilder from)
    {
      this.m_ChunkLength = from.m_ChunkLength;
      this.m_ChunkOffset = from.m_ChunkOffset;
      this.m_ChunkChars = from.m_ChunkChars;
      this.m_ChunkPrevious = from.m_ChunkPrevious;
      this.m_MaxCapacity = from.m_MaxCapacity;
    }

    private StringBuilder(int size, int maxCapacity, StringBuilder previousBlock)
    {
      this.m_ChunkChars = new char[size];
      this.m_MaxCapacity = maxCapacity;
      this.m_ChunkPrevious = previousBlock;
      if (previousBlock == null)
        return;
      this.m_ChunkOffset = previousBlock.m_ChunkOffset + previousBlock.m_ChunkLength;
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      info.AddValue("m_MaxCapacity", this.m_MaxCapacity);
      info.AddValue("Capacity", this.Capacity);
      info.AddValue("m_StringValue", (object) this.ToString());
      info.AddValue("m_currentThread", 0);
    }

    [Conditional("_DEBUG")]
    private void VerifyClassInvariant()
    {
      StringBuilder stringBuilder1 = this;
      int num = this.m_MaxCapacity;
      while (true)
      {
        StringBuilder stringBuilder2 = stringBuilder1.m_ChunkPrevious;
        if (stringBuilder2 != null)
          stringBuilder1 = stringBuilder2;
        else
          break;
      }
    }

    /// <summary>确保 <see cref="T:System.Text.StringBuilder" /> 的此实例的容量至少是指定值。</summary>
    /// <returns>此实例的新容量。</returns>
    /// <param name="capacity">要确保的最小容量。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="capacity" /> 小于零。- 或 -增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public int EnsureCapacity(int capacity)
    {
      if (capacity < 0)
        throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_NegativeCapacity"));
      if (this.Capacity < capacity)
        this.Capacity = capacity;
      return this.Capacity;
    }

    /// <summary>将此实例的值转换为 <see cref="T:System.String" />。</summary>
    /// <returns>其值与此实例相同的字符串。</returns>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe string ToString()
    {
      if (this.Length == 0)
        return string.Empty;
      string str1 = string.FastAllocateString(this.Length);
      StringBuilder stringBuilder = this;
      string str2 = str1;
      char* chPtr = (char*) str2;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      do
      {
        if (stringBuilder.m_ChunkLength > 0)
        {
          char[] chArray = stringBuilder.m_ChunkChars;
          int num = stringBuilder.m_ChunkOffset;
          int charCount = stringBuilder.m_ChunkLength;
          if ((long) (uint) (charCount + num) > (long) str1.Length || (uint) charCount > (uint) chArray.Length)
            throw new ArgumentOutOfRangeException("chunkLength", Environment.GetResourceString("ArgumentOutOfRange_Index"));
          fixed (char* smem = chArray)
            string.wstrcpy(chPtr + num, smem, charCount);
        }
        stringBuilder = stringBuilder.m_ChunkPrevious;
      }
      while (stringBuilder != null);
      str2 = (string) null;
      return str1;
    }

    /// <summary>将此实例中子字符串的值转换为 <see cref="T:System.String" />。</summary>
    /// <returns>一个字符串，其值与此实例的指定子字符串相同。</returns>
    /// <param name="startIndex">此实例内子字符串的起始位置。</param>
    /// <param name="length">子字符串的长度。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 或 <paramref name="length" /> 小于零。- 或 -<paramref name="startIndex" /> 与 <paramref name="length" /> 的和大于当前实例的长度。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe string ToString(int startIndex, int length)
    {
      int length1 = this.Length;
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (startIndex > length1)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndexLargerThanLength"));
      if (length < 0)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
      if (startIndex > length1 - length)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_IndexLength"));
      StringBuilder stringBuilder = this;
      int num1 = startIndex + length;
      string str1 = string.FastAllocateString(length);
      int num2 = length;
      string str2 = str1;
      char* chPtr = (char*) str2;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      while (num2 > 0)
      {
        int num3 = num1 - stringBuilder.m_ChunkOffset;
        if (num3 >= 0)
        {
          if (num3 > stringBuilder.m_ChunkLength)
            num3 = stringBuilder.m_ChunkLength;
          int num4 = num2;
          int charCount = num4;
          int index = num3 - num4;
          if (index < 0)
          {
            charCount += index;
            index = 0;
          }
          num2 -= charCount;
          if (charCount > 0)
          {
            char[] chArray = stringBuilder.m_ChunkChars;
            if ((long) (uint) (charCount + num2) > (long) length || (uint) (charCount + index) > (uint) chArray.Length)
              throw new ArgumentOutOfRangeException("chunkCount", Environment.GetResourceString("ArgumentOutOfRange_Index"));
            fixed (char* smem = &chArray[index])
              string.wstrcpy(chPtr + num2, smem, charCount);
          }
        }
        stringBuilder = stringBuilder.m_ChunkPrevious;
      }
      str2 = (string) null;
      return str1;
    }

    /// <summary>从当前 <see cref="T:System.Text.StringBuilder" /> 实例中移除所有字符。</summary>
    /// <returns>其 <see cref="P:System.Text.StringBuilder.Length" /> 为 0（零）的对象。</returns>
    [__DynamicallyInvokable]
    public StringBuilder Clear()
    {
      this.Length = 0;
      return this;
    }

    /// <summary>向此实例追加 Unicode 字符的字符串表示形式指定数目的副本。</summary>
    /// <returns>完成追加操作后对此实例的引用。</returns>
    /// <param name="value">要追加的字符。</param>
    /// <param name="repeatCount">追加 <paramref name="value" /> 的次数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="repeatCount" /> 小于零。- 或 -增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <exception cref="T:System.OutOfMemoryException">内存不足。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Append(char value, int repeatCount)
    {
      if (repeatCount < 0)
        throw new ArgumentOutOfRangeException("repeatCount", Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
      if (repeatCount == 0)
        return this;
      int num = this.m_ChunkLength;
      while (repeatCount > 0)
      {
        if (num < this.m_ChunkChars.Length)
        {
          this.m_ChunkChars[num++] = value;
          --repeatCount;
        }
        else
        {
          this.m_ChunkLength = num;
          this.ExpandByABlock(repeatCount);
          num = 0;
        }
      }
      this.m_ChunkLength = num;
      return this;
    }

    /// <summary>向此实例追加指定的 Unicode 字符子数组的字符串表示形式。</summary>
    /// <returns>完成追加操作后对此实例的引用。</returns>
    /// <param name="value">字符数组。</param>
    /// <param name="startIndex">
    /// <paramref name="value" /> 中的起始位置。</param>
    /// <param name="charCount">要追加的字符数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null，并且 <paramref name="startIndex" /> 和 <paramref name="charCount" /> 不为零。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="charCount" /> 小于零。- 或 - <paramref name="startIndex" /> 小于零。- 或 - <paramref name="startIndex" /> + <paramref name="charCount" /> 大于 <paramref name="value" /> 的长度。- 或 -增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder Append(char[] value, int startIndex, int charCount)
    {
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      if (charCount < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8 && charCount == 0)
        return this;
      if (value == null)
      {
        if (startIndex == 0 && charCount == 0)
          return this;
        throw new ArgumentNullException("value");
      }
      if (charCount > value.Length - startIndex)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (charCount == 0)
        return this;
      fixed (char* chPtr = &value[startIndex])
        this.Append(chPtr, charCount);
      return this;
    }

    /// <summary>向此实例追加指定字符串的副本。</summary>
    /// <returns>完成追加操作后对此实例的引用。</returns>
    /// <param name="value">要追加的字符串。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder Append(string value)
    {
      if (value != null)
      {
        char[] chArray = this.m_ChunkChars;
        int index = this.m_ChunkLength;
        int length = value.Length;
        int num = index + length;
        if (num < chArray.Length)
        {
          if (length <= 2)
          {
            if (length > 0)
              chArray[index] = value[0];
            if (length > 1)
              chArray[index + 1] = value[1];
          }
          else
          {
            string str = value;
            char* smem = (char*) str;
            if ((IntPtr) smem != IntPtr.Zero)
              smem += RuntimeHelpers.OffsetToStringData;
            fixed (char* dmem = &chArray[index])
              string.wstrcpy(dmem, smem, length);
            str = (string) null;
          }
          this.m_ChunkLength = num;
        }
        else
          this.AppendHelper(value);
      }
      return this;
    }

    [SecuritySafeCritical]
    private unsafe void AppendHelper(string value)
    {
      string str = value;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      this.Append(chPtr, value.Length);
      str = (string) null;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal unsafe void ReplaceBufferInternal(char* newBuffer, int newLength);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal unsafe void ReplaceBufferAnsiInternal(sbyte* newBuffer, int newLength);

    /// <summary>向此实例追加指定子字符串的副本。</summary>
    /// <returns>完成追加操作后对此实例的引用。</returns>
    /// <param name="value">包含要追加的子字符串的字符串。</param>
    /// <param name="startIndex">
    /// <paramref name="value" /> 中子字符串开始的位置。</param>
    /// <param name="count">
    /// <paramref name="value" /> 中要追加的字符数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null，并且 <paramref name="startIndex" /> 和 <paramref name="count" /> 不为零。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 小于零。- 或 - <paramref name="startIndex" /> 小于零。- 或 - <paramref name="startIndex" /> + <paramref name="count" /> 大于 <paramref name="value" /> 的长度。- 或 -增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder Append(string value, int startIndex, int count)
    {
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8 && count == 0)
        return this;
      if (value == null)
      {
        if (startIndex == 0 && count == 0)
          return this;
        throw new ArgumentNullException("value");
      }
      if (count == 0)
        return this;
      if (startIndex > value.Length - count)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      string str = value;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      this.Append(chPtr + startIndex, count);
      str = (string) null;
      return this;
    }

    /// <summary>将默认的行终止符追加到当前 <see cref="T:System.Text.StringBuilder" /> 对象的末尾。</summary>
    /// <returns>完成追加操作后对此实例的引用。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public StringBuilder AppendLine()
    {
      return this.Append(Environment.NewLine);
    }

    /// <summary>将后面跟有默认行终止符的指定字符串的副本追加到当前 <see cref="T:System.Text.StringBuilder" /> 对象的末尾。</summary>
    /// <returns>完成追加操作后对此实例的引用。</returns>
    /// <param name="value">要追加的字符串。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public StringBuilder AppendLine(string value)
    {
      this.Append(value);
      return this.Append(Environment.NewLine);
    }

    /// <summary>将此实例的指定段中的字符复制到目标 <see cref="T:System.Char" /> 数组的指定段中。</summary>
    /// <param name="sourceIndex">此实例中开始复制字符的位置。索引是从零开始的。</param>
    /// <param name="destination">将从中复制字符的数组。</param>
    /// <param name="destinationIndex">
    /// <paramref name="destination" /> 中将从其开始复制字符的起始位置。索引是从零开始的。</param>
    /// <param name="count">要复制的字符数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destination" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="sourceIndex" />、<paramref name="destinationIndex" /> 或 <paramref name="count" />，小于零。- 或 -<paramref name="sourceIndex" /> 大于此实例的长度。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="sourceIndex" /> + <paramref name="count" /> 大于此实例的长度。- 或 -<paramref name="destinationIndex" /> + <paramref name="count" /> 大于 <paramref name="destination" /> 的长度。</exception>
    /// <filterpriority>1</filterpriority>
    [ComVisible(false)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
    {
      if (destination == null)
        throw new ArgumentNullException("destination");
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Arg_NegativeArgCount"));
      if (destinationIndex < 0)
        throw new ArgumentOutOfRangeException("destinationIndex", Environment.GetResourceString("ArgumentOutOfRange_MustBeNonNegNum", (object) "destinationIndex"));
      if (destinationIndex > destination.Length - count)
        throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_OffsetOut"));
      if ((uint) sourceIndex > (uint) this.Length)
        throw new ArgumentOutOfRangeException("sourceIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (sourceIndex > this.Length - count)
        throw new ArgumentException(Environment.GetResourceString("Arg_LongerThanSrcString"));
      StringBuilder stringBuilder = this;
      int num1 = sourceIndex + count;
      int destinationIndex1 = destinationIndex + count;
      while (count > 0)
      {
        int num2 = num1 - stringBuilder.m_ChunkOffset;
        if (num2 >= 0)
        {
          if (num2 > stringBuilder.m_ChunkLength)
            num2 = stringBuilder.m_ChunkLength;
          int count1 = count;
          int sourceIndex1 = num2 - count;
          if (sourceIndex1 < 0)
          {
            count1 += sourceIndex1;
            sourceIndex1 = 0;
          }
          destinationIndex1 -= count1;
          count -= count1;
          StringBuilder.ThreadSafeCopy(stringBuilder.m_ChunkChars, sourceIndex1, destination, destinationIndex1, count1);
        }
        stringBuilder = stringBuilder.m_ChunkPrevious;
      }
    }

    /// <summary>将指定字符串的一个或更多副本插入到此实例中的指定字符位置。</summary>
    /// <returns>完成插入后对此实例的引用。</returns>
    /// <param name="index">此实例中开始插入的位置。</param>
    /// <param name="value">要插入的字符串。</param>
    /// <param name="count">插入 <paramref name="value" /> 的次数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于此实例的当前长度。- 或 - <paramref name="count" /> 小于零。</exception>
    /// <exception cref="T:System.OutOfMemoryException">此 <see cref="T:System.Text.StringBuilder" /> 对象的当前长度加上 <paramref name="value" /> 的长度乘以 <paramref name="count" /> 的值超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder Insert(int index, string value, int count)
    {
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      int length = this.Length;
      if ((uint) index > (uint) length)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (value == null || value.Length == 0 || count == 0)
        return this;
      long num = (long) value.Length * (long) count;
      if (num > (long) (this.MaxCapacity - this.Length))
        throw new OutOfMemoryException();
      StringBuilder chunk;
      int indexInChunk;
      this.MakeRoom(index, (int) num, out chunk, out indexInChunk, false);
      string str = value;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      for (; count > 0; --count)
        this.ReplaceInPlaceAtChunk(ref chunk, ref indexInChunk, chPtr, value.Length);
      str = (string) null;
      return this;
    }

    /// <summary>将指定范围的字符从此实例中移除。</summary>
    /// <returns>切除操作完成后对此实例的引用。</returns>
    /// <param name="startIndex">此实例中开始移除操作的从零开始的位置。</param>
    /// <param name="length">要删除的字符数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">如果 <paramref name="startIndex" /> 或 <paramref name="length" /> 小于零，或者 <paramref name="startIndex" />+<paramref name="length" /> 大于此实例的长度。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Remove(int startIndex, int length)
    {
      if (length < 0)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (length > this.Length - startIndex)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (this.Length == length && startIndex == 0)
      {
        this.Length = 0;
        return this;
      }
      if (length > 0)
      {
        StringBuilder chunk;
        int indexInChunk;
        this.Remove(startIndex, length, out chunk, out indexInChunk);
      }
      return this;
    }

    /// <summary>向此实例追加指定的布尔值的字符串表示形式。</summary>
    /// <returns>完成追加操作后对此实例的引用。</returns>
    /// <param name="value">要追加的布尔值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Append(bool value)
    {
      return this.Append(value.ToString());
    }

    /// <summary>向此实例追加指定的 8 位有符号整数的字符串表示形式。</summary>
    /// <returns>完成追加操作后对此实例的引用。</returns>
    /// <param name="value">要追加的值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public StringBuilder Append(sbyte value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>向此实例追加指定的 8 位无符号整数的字符串表示形式。</summary>
    /// <returns>完成追加操作后对此实例的引用。</returns>
    /// <param name="value">要追加的值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Append(byte value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>向此实例追加指定 Unicode 字符的字符串表示形式。</summary>
    /// <returns>完成追加操作后对此实例的引用。</returns>
    /// <param name="value">要追加的 Unicode 字符。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Append(char value)
    {
      if (this.m_ChunkLength < this.m_ChunkChars.Length)
      {
        char[] chArray = this.m_ChunkChars;
        int num1 = this.m_ChunkLength;
        this.m_ChunkLength = num1 + 1;
        int index = num1;
        int num2 = (int) value;
        chArray[index] = (char) num2;
      }
      else
        this.Append(value, 1);
      return this;
    }

    /// <summary>向此实例追加指定的 16 位有符号整数的字符串表示形式。</summary>
    /// <returns>完成追加操作后对此实例的引用。</returns>
    /// <param name="value">要追加的值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Append(short value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>向此实例追加指定的 32 位有符号整数的字符串表示形式。</summary>
    /// <returns>完成追加操作后对此实例的引用。</returns>
    /// <param name="value">要追加的值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Append(int value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>向此实例追加指定的 64 位有符号整数的字符串表示形式。</summary>
    /// <returns>完成追加操作后对此实例的引用。</returns>
    /// <param name="value">要追加的值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Append(long value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>向此实例追加指定的单精度浮点数的字符串表示形式。</summary>
    /// <returns>完成追加操作后对此实例的引用。</returns>
    /// <param name="value">要追加的值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Append(float value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>向此实例追加指定的双精度浮点数的字符串表示形式。</summary>
    /// <returns>完成追加操作后对此实例的引用。</returns>
    /// <param name="value">要追加的值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Append(double value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>向此实例追加指定的十进制数的字符串表示形式。</summary>
    /// <returns>完成追加操作后对此实例的引用。</returns>
    /// <param name="value">要追加的值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Append(Decimal value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>向此实例追加指定的 16 位无符号整数的字符串表示形式。</summary>
    /// <returns>完成追加操作后对此实例的引用。</returns>
    /// <param name="value">要追加的值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public StringBuilder Append(ushort value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>向此实例追加指定的 32 位无符号整数的字符串表示形式。</summary>
    /// <returns>完成追加操作后对此实例的引用。</returns>
    /// <param name="value">要追加的值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public StringBuilder Append(uint value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>向此实例追加指定的 64 位无符号整数的字符串表示形式。</summary>
    /// <returns>完成追加操作后对此实例的引用。</returns>
    /// <param name="value">要追加的值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public StringBuilder Append(ulong value)
    {
      return this.Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    /// <summary>向此实例追加指定对象的字符串表示形式。</summary>
    /// <returns>完成追加操作后对此实例的引用。</returns>
    /// <param name="value">要追加的对象。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Append(object value)
    {
      if (value == null)
        return this;
      return this.Append(value.ToString());
    }

    /// <summary>向此实例追加指定数组中的 Unicode 字符的字符串表示形式。</summary>
    /// <returns>完成追加操作后对此实例的引用。</returns>
    /// <param name="value">要追加的字符数组。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder Append(char[] value)
    {
      if (value != null && value.Length != 0)
      {
        fixed (char* chPtr = &value[0])
          this.Append(chPtr, value.Length);
      }
      return this;
    }

    /// <summary>将字符串插入到此实例中的指定字符位置。</summary>
    /// <returns>完成插入操作后对此实例的引用。</returns>
    /// <param name="index">此实例中开始插入的位置。</param>
    /// <param name="value">要插入的字符串。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于此实例的当前长度。- 或 -此 <see cref="T:System.Text.StringBuilder" /> 对象的当前长度加上 <paramref name="value" /> 的长度超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder Insert(int index, string value)
    {
      if ((uint) index > (uint) this.Length)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (value != null)
      {
        string str = value;
        char* chPtr = (char*) str;
        if ((IntPtr) chPtr != IntPtr.Zero)
          chPtr += RuntimeHelpers.OffsetToStringData;
        this.Insert(index, chPtr, value.Length);
        str = (string) null;
      }
      return this;
    }

    /// <summary>将布尔值的字符串表示形式插入到此实例中的指定字符位置。</summary>
    /// <returns>完成插入操作后对此实例的引用。</returns>
    /// <param name="index">此实例中开始插入的位置。</param>
    /// <param name="value">要插入的值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于此实例的长度。</exception>
    /// <exception cref="T:System.OutOfMemoryException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, bool value)
    {
      return this.Insert(index, value.ToString(), 1);
    }

    /// <summary>将指定的 8 位带符号整数的字符串表示形式插入到此实例中的指定字符位置。</summary>
    /// <returns>完成插入操作后对此实例的引用。</returns>
    /// <param name="index">此实例中开始插入的位置。</param>
    /// <param name="value">要插入的值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于此实例的长度。</exception>
    /// <exception cref="T:System.OutOfMemoryException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, sbyte value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>将指定的 8 位无符号整数的字符串表示形式插入到此实例中的指定字符位置。</summary>
    /// <returns>完成插入操作后对此实例的引用。</returns>
    /// <param name="index">此实例中开始插入的位置。</param>
    /// <param name="value">要插入的值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于此实例的长度。</exception>
    /// <exception cref="T:System.OutOfMemoryException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, byte value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>将指定的 16 位带符号整数的字符串表示形式插入到此实例中的指定字符位置。</summary>
    /// <returns>完成插入操作后对此实例的引用。</returns>
    /// <param name="index">此实例中开始插入的位置。</param>
    /// <param name="value">要插入的值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于此实例的长度。</exception>
    /// <exception cref="T:System.OutOfMemoryException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, short value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>将指定的 Unicode 字符的字符串表示形式插入到此实例中的指定位置。</summary>
    /// <returns>完成插入操作后对此实例的引用。</returns>
    /// <param name="index">此实例中开始插入的位置。</param>
    /// <param name="value">要插入的值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于此实例的长度。- 或 -增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。 </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder Insert(int index, char value)
    {
      this.Insert(index, &value, 1);
      return this;
    }

    /// <summary>将指定的 Unicode 字符数组的字符串表示形式插入到此实例中的指定字符位置。</summary>
    /// <returns>完成插入操作后对此实例的引用。</returns>
    /// <param name="index">此实例中开始插入的位置。</param>
    /// <param name="value">要插入的字符数组。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于此实例的长度。- 或 -增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, char[] value)
    {
      if ((uint) index > (uint) this.Length)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (value != null)
        this.Insert(index, value, 0, value.Length);
      return this;
    }

    /// <summary>将指定的 Unicode 字符子数组的字符串表示形式插入到此实例中的指定字符位置。</summary>
    /// <returns>完成插入操作后对此实例的引用。</returns>
    /// <param name="index">此实例中开始插入的位置。</param>
    /// <param name="value">字符数组。</param>
    /// <param name="startIndex">
    /// <paramref name="value" /> 内的起始索引。</param>
    /// <param name="charCount">要插入的字符数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null，并且 <paramref name="startIndex" /> 和 <paramref name="charCount" /> 不为零。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" />、<paramref name="startIndex" /> 或 <paramref name="charCount" /> 小于零。- 或 - <paramref name="index" /> 大于此实例的长度。- 或 - <paramref name="startIndex" /> 加上 <paramref name="charCount" /> 不是 <paramref name="value" /> 中的位置。- 或 -增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe StringBuilder Insert(int index, char[] value, int startIndex, int charCount)
    {
      int length = this.Length;
      if ((uint) index > (uint) length)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (value == null)
      {
        if (startIndex == 0 && charCount == 0)
          return this;
        throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_String"));
      }
      if (startIndex < 0)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      if (charCount < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      if (startIndex > value.Length - charCount)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (charCount > 0)
      {
        fixed (char* chPtr = &value[startIndex])
          this.Insert(index, chPtr, charCount);
      }
      return this;
    }

    /// <summary>将指定的 32 位带符号整数的字符串表示形式插入到此实例中的指定字符位置。</summary>
    /// <returns>完成插入操作后对此实例的引用。</returns>
    /// <param name="index">此实例中开始插入的位置。</param>
    /// <param name="value">要插入的值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于此实例的长度。</exception>
    /// <exception cref="T:System.OutOfMemoryException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, int value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>将 64 位带符号整数的字符串表示形式插入到此实例中的指定字符位置。</summary>
    /// <returns>完成插入操作后对此实例的引用。</returns>
    /// <param name="index">此实例中开始插入的位置。</param>
    /// <param name="value">要插入的值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于此实例的长度。</exception>
    /// <exception cref="T:System.OutOfMemoryException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, long value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>将单精度浮点数的字符串表示形式插入到此实例中的指定字符位置。</summary>
    /// <returns>完成插入操作后对此实例的引用。</returns>
    /// <param name="index">此实例中开始插入的位置。</param>
    /// <param name="value">要插入的值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于此实例的长度。</exception>
    /// <exception cref="T:System.OutOfMemoryException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, float value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>将双精度浮点数的字符串表示形式插入到此实例中的指定字符位置。</summary>
    /// <returns>完成插入操作后对此实例的引用。</returns>
    /// <param name="index">此实例中开始插入的位置。</param>
    /// <param name="value">要插入的值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于此实例的长度。</exception>
    /// <exception cref="T:System.OutOfMemoryException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, double value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>将十进制数的字符串表示形式插入到此实例中的指定字符位置。</summary>
    /// <returns>完成插入操作后对此实例的引用。</returns>
    /// <param name="index">此实例中开始插入的位置。</param>
    /// <param name="value">要插入的值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于此实例的长度。</exception>
    /// <exception cref="T:System.OutOfMemoryException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, Decimal value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>将 16 位无符号整数的字符串表示形式插入到此实例中的指定字符位置。</summary>
    /// <returns>完成插入操作后对此实例的引用。</returns>
    /// <param name="index">此实例中开始插入的位置。</param>
    /// <param name="value">要插入的值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于此实例的长度。</exception>
    /// <exception cref="T:System.OutOfMemoryException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, ushort value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>将 32 位无符号整数的字符串表示形式插入到此实例中的指定字符位置。</summary>
    /// <returns>完成插入操作后对此实例的引用。</returns>
    /// <param name="index">此实例中开始插入的位置。</param>
    /// <param name="value">要插入的值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于此实例的长度。</exception>
    /// <exception cref="T:System.OutOfMemoryException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, uint value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>将 64 位无符号整数的字符串表示形式插入到此实例中的指定字符位置。</summary>
    /// <returns>完成插入操作后对此实例的引用。</returns>
    /// <param name="index">此实例中开始插入的位置。</param>
    /// <param name="value">要插入的值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于此实例的长度。</exception>
    /// <exception cref="T:System.OutOfMemoryException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, ulong value)
    {
      return this.Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
    }

    /// <summary>将对象的字符串表示形式插入到此实例中的指定字符位置。</summary>
    /// <returns>完成插入操作后对此实例的引用。</returns>
    /// <param name="index">此实例中开始插入的位置。</param>
    /// <param name="value">要插入的对象或 null。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于零或大于此实例的长度。</exception>
    /// <exception cref="T:System.OutOfMemoryException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Insert(int index, object value)
    {
      if (value == null)
        return this;
      return this.Insert(index, value.ToString(), 1);
    }

    /// <summary>向此实例追加通过处理复合格式字符串（包含零个或更多格式项）而返回的字符串。每个格式项都替换为一个参数的字符串表示形式。</summary>
    /// <returns>对追加了 <paramref name="format" /> 的此实例的引用。<paramref name="format" /> 中的每个格式项都替换为 <paramref name="arg0" /> 的字符串表示形式。</returns>
    /// <param name="format">复合格式字符串（请参见“备注”）。</param>
    /// <param name="arg0">要设置其格式的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 为 null。 </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 无效。- 或 -格式项的索引小于 0（零），或大于或等于 1。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">扩展字符串的长度将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder AppendFormat(string format, object arg0)
    {
      return this.AppendFormatHelper((IFormatProvider) null, format, new ParamsArray(arg0));
    }

    /// <summary>向此实例追加通过处理复合格式字符串（包含零个或更多格式项）而返回的字符串。每个格式项都替换为这两个参数中任意一个参数的字符串表示形式。</summary>
    /// <returns>对追加了 <paramref name="format" /> 的此实例的引用。<paramref name="format" /> 中的每个格式项都由相应的对象参数的字符串表示形式替换。</returns>
    /// <param name="format">复合格式字符串（请参见“备注”）。</param>
    /// <param name="arg0">要设置格式的第一个对象。</param>
    /// <param name="arg1">要设置格式的第二个对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 为 null。 </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 无效。- 或 -格式项的索引小于 0（零），或大于或等于 2。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">扩展字符串的长度将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder AppendFormat(string format, object arg0, object arg1)
    {
      return this.AppendFormatHelper((IFormatProvider) null, format, new ParamsArray(arg0, arg1));
    }

    /// <summary>向此实例追加通过处理复合格式字符串（包含零个或更多格式项）而返回的字符串。每个格式项都替换为这三个参数中任意一个参数的字符串表示形式。</summary>
    /// <returns>对追加了 <paramref name="format" /> 的此实例的引用。<paramref name="format" /> 中的每个格式项都由相应的对象参数的字符串表示形式替换。</returns>
    /// <param name="format">复合格式字符串（请参见“备注”）。</param>
    /// <param name="arg0">要设置格式的第一个对象。</param>
    /// <param name="arg1">要设置格式的第二个对象。</param>
    /// <param name="arg2">要设置格式的第三个对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 为 null。 </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 无效。- 或 -格式项的索引小于 0（零），或大于或等于 3。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">扩展字符串的长度将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder AppendFormat(string format, object arg0, object arg1, object arg2)
    {
      return this.AppendFormatHelper((IFormatProvider) null, format, new ParamsArray(arg0, arg1, arg2));
    }

    /// <summary>向此实例追加通过处理复合格式字符串（包含零个或更多格式项）而返回的字符串。每个格式项都由参数数组中相应参数的字符串表示形式替换。</summary>
    /// <returns>对追加了 <paramref name="format" /> 的此实例的引用。<paramref name="format" /> 中的每个格式项都由相应的对象参数的字符串表示形式替换。</returns>
    /// <param name="format">复合格式字符串（请参见“备注”）。</param>
    /// <param name="args">要设置其格式的对象的数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 或 <paramref name="args" /> 为 null。 </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 无效。- 或 -格式项的索引小于 0（零）或大于等于 <paramref name="args" /> 数组的长度。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">扩展字符串的长度将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder AppendFormat(string format, params object[] args)
    {
      if (args == null)
        throw new ArgumentNullException(format == null ? "format" : "args");
      return this.AppendFormatHelper((IFormatProvider) null, format, new ParamsArray(args));
    }

    /// <summary>向此实例追加通过处理复合格式字符串（包含零个或更多格式项）而返回的字符串。每个格式项都使用指定的格式提供程序替换为单个参数的字符串表示形式。</summary>
    /// <returns>完成追加操作后对此实例的引用。追加操作后，此实例包含执行操作，加上后缀的副本之前已存在的任何数据<paramref name="format" />规范由字符串表示形式中的任何格式的替换<paramref name="arg0" />。</returns>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <param name="format">复合格式字符串（请参见“备注”）。</param>
    /// <param name="arg0">要设置格式的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 为 null。 </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 无效。- 或 -格式项的索引小于 0 （零），或者大于或等于一 (1)。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">扩展字符串的长度将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    [__DynamicallyInvokable]
    public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0)
    {
      return this.AppendFormatHelper(provider, format, new ParamsArray(arg0));
    }

    /// <summary>向此实例追加通过处理复合格式字符串（包含零个或更多格式项）而返回的字符串。每个格式项都使用指定的格式提供程序替换为两个参数中任一个的字符串表示形式。</summary>
    /// <returns>完成追加操作后对此实例的引用。完成追加操作后，此实例包含执行该操作之前已存在的任何数据，并且有一个 <paramref name="format" /> 的副本作为后缀，其中任何格式规范都由相应对象参数的字符串表示形式替换。</returns>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <param name="format">复合格式字符串（请参见“备注”）。</param>
    /// <param name="arg0">要设置格式的第一个对象。</param>
    /// <param name="arg1">要设置格式的第二个对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 为 null。 </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 无效。- 或 -格式项的索引小于 0 （零），或者大于或等于 2 （两个）。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">扩展字符串的长度将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    [__DynamicallyInvokable]
    public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0, object arg1)
    {
      return this.AppendFormatHelper(provider, format, new ParamsArray(arg0, arg1));
    }

    /// <summary>向此实例追加通过处理复合格式字符串（包含零个或更多格式项）而返回的字符串。每个格式项都使用指定的格式提供程序替换为三个参数中任一个的字符串表示形式。</summary>
    /// <returns>完成追加操作后对此实例的引用。完成追加操作后，此实例包含执行该操作之前已存在的任何数据，并且有一个 <paramref name="format" /> 的副本作为后缀，其中任何格式规范都由相应对象参数的字符串表示形式替换。</returns>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <param name="format">复合格式字符串（请参见“备注”）。</param>
    /// <param name="arg0">要设置格式的第一个对象。</param>
    /// <param name="arg1">要设置格式的第二个对象。</param>
    /// <param name="arg2">要设置格式的第三个对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 为 null。 </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 无效。- 或 -格式项的索引小于 0 （零），或者大于或等于 3 （三个）。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">扩展字符串的长度将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    [__DynamicallyInvokable]
    public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0, object arg1, object arg2)
    {
      return this.AppendFormatHelper(provider, format, new ParamsArray(arg0, arg1, arg2));
    }

    /// <summary>向此实例追加通过处理复合格式字符串（包含零个或更多格式项）而返回的字符串。每个格式项都使用指定的格式提供程序由参数数组中相应参数的字符串表示形式替换。</summary>
    /// <returns>完成追加操作后对此实例的引用。完成追加操作后，此实例包含执行该操作之前已存在的任何数据，并且有一个 <paramref name="format" /> 的副本作为后缀，其中任何格式规范都由相应对象参数的字符串表示形式替换。</returns>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <param name="format">复合格式字符串（请参见“备注”）。</param>
    /// <param name="args">要设置其格式的对象的数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="format" /> 为 null。 </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> 无效。- 或 -格式项的索引小于 0（零）或大于等于 <paramref name="args" /> 数组的长度。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">扩展字符串的长度将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder AppendFormat(IFormatProvider provider, string format, params object[] args)
    {
      if (args == null)
        throw new ArgumentNullException(format == null ? "format" : "args");
      return this.AppendFormatHelper(provider, format, new ParamsArray(args));
    }

    private static void FormatError()
    {
      throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
    }

    internal StringBuilder AppendFormatHelper(IFormatProvider provider, string format, ParamsArray args)
    {
      if (format == null)
        throw new ArgumentNullException("format");
      int index1 = 0;
      int length = format.Length;
      char ch = char.MinValue;
      ICustomFormatter customFormatter = (ICustomFormatter) null;
      if (provider != null)
        customFormatter = (ICustomFormatter) provider.GetFormat(typeof (ICustomFormatter));
      while (true)
      {
        bool flag;
        int repeatCount;
        do
        {
          if (index1 < length)
          {
            ch = format[index1];
            ++index1;
            if ((int) ch == 125)
            {
              if (index1 < length && (int) format[index1] == 125)
                ++index1;
              else
                StringBuilder.FormatError();
            }
            if ((int) ch == 123)
            {
              if (index1 >= length || (int) format[index1] != 123)
                --index1;
              else
                goto label_10;
            }
            else
              goto label_12;
          }
          if (index1 != length)
          {
            int index2 = index1 + 1;
            if (index2 == length || (int) (ch = format[index2]) < 48 || (int) ch > 57)
              StringBuilder.FormatError();
            int index3 = 0;
            do
            {
              index3 = index3 * 10 + (int) ch - 48;
              ++index2;
              if (index2 == length)
                StringBuilder.FormatError();
              ch = format[index2];
            }
            while ((int) ch >= 48 && (int) ch <= 57 && index3 < 1000000);
            if (index3 >= args.Length)
              throw new FormatException(Environment.GetResourceString("Format_IndexOutOfRange"));
            while (index2 < length && (int) (ch = format[index2]) == 32)
              ++index2;
            flag = false;
            int num = 0;
            if ((int) ch == 44)
            {
              ++index2;
              while (index2 < length && (int) format[index2] == 32)
                ++index2;
              if (index2 == length)
                StringBuilder.FormatError();
              ch = format[index2];
              if ((int) ch == 45)
              {
                flag = true;
                ++index2;
                if (index2 == length)
                  StringBuilder.FormatError();
                ch = format[index2];
              }
              if ((int) ch < 48 || (int) ch > 57)
                StringBuilder.FormatError();
              do
              {
                num = num * 10 + (int) ch - 48;
                ++index2;
                if (index2 == length)
                  StringBuilder.FormatError();
                ch = format[index2];
              }
              while ((int) ch >= 48 && (int) ch <= 57 && num < 1000000);
            }
            while (index2 < length && (int) (ch = format[index2]) == 32)
              ++index2;
            object obj = args[index3];
            StringBuilder stringBuilder = (StringBuilder) null;
            if ((int) ch == 58)
            {
              int index4 = index2 + 1;
              while (true)
              {
                if (index4 == length)
                  StringBuilder.FormatError();
                ch = format[index4];
                ++index4;
                if ((int) ch == 123)
                {
                  if (index4 < length && (int) format[index4] == 123)
                    ++index4;
                  else
                    StringBuilder.FormatError();
                }
                else if ((int) ch == 125)
                {
                  if (index4 < length && (int) format[index4] == 125)
                    ++index4;
                  else
                    break;
                }
                if (stringBuilder == null)
                  stringBuilder = new StringBuilder();
                stringBuilder.Append(ch);
              }
              index2 = index4 - 1;
            }
            if ((int) ch != 125)
              StringBuilder.FormatError();
            index1 = index2 + 1;
            string format1 = (string) null;
            string str = (string) null;
            if (customFormatter != null)
            {
              if (stringBuilder != null)
                format1 = stringBuilder.ToString();
              str = customFormatter.Format(format1, obj, provider);
            }
            if (str == null)
            {
              IFormattable formattable = obj as IFormattable;
              if (formattable != null)
              {
                if (format1 == null && stringBuilder != null)
                  format1 = stringBuilder.ToString();
                str = formattable.ToString(format1, provider);
              }
              else if (obj != null)
                str = obj.ToString();
            }
            if (str == null)
              str = string.Empty;
            repeatCount = num - str.Length;
            if (!flag && repeatCount > 0)
              this.Append(' ', repeatCount);
            this.Append(str);
          }
          else
            goto label_76;
        }
        while (!flag || repeatCount <= 0);
        goto label_75;
label_10:
        ++index1;
label_12:
        this.Append(ch);
        continue;
label_75:
        this.Append(' ', repeatCount);
      }
label_76:
      return this;
    }

    /// <summary>将此实例中出现的所有指定字符串的替换为其他指定字符串。</summary>
    /// <returns>对此实例的引用，其中 <paramref name="oldValue" /> 的所有实例被 <paramref name="newValue" /> 替换。</returns>
    /// <param name="oldValue">要替换的字符串。</param>
    /// <param name="newValue">替换 <paramref name="oldValue" /> 的字符串或 null。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="oldValue" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="oldValue" /> 的长度为零。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Replace(string oldValue, string newValue)
    {
      return this.Replace(oldValue, newValue, 0, this.Length);
    }

    /// <summary>返回一个值，该值指示此实例是否等于指定的对象。</summary>
    /// <returns>如果此实例和 <paramref name="sb" /> 具有相等的字符串、<see cref="P:System.Text.StringBuilder.Capacity" /> 和 <see cref="P:System.Text.StringBuilder.MaxCapacity" /> 值，则为 true；否则，为 false。</returns>
    /// <param name="sb">与此实例进行比较的 object，或 null。 </param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool Equals(StringBuilder sb)
    {
      if (sb == null || this.Capacity != sb.Capacity || (this.MaxCapacity != sb.MaxCapacity || this.Length != sb.Length))
        return false;
      if (sb == this)
        return true;
      StringBuilder stringBuilder1 = this;
      int index1 = stringBuilder1.m_ChunkLength;
      StringBuilder stringBuilder2 = sb;
      int index2 = stringBuilder2.m_ChunkLength;
      do
      {
        --index1;
        --index2;
        for (; index1 < 0; index1 = stringBuilder1.m_ChunkLength + index1)
        {
          stringBuilder1 = stringBuilder1.m_ChunkPrevious;
          if (stringBuilder1 == null)
            break;
        }
        for (; index2 < 0; index2 = stringBuilder2.m_ChunkLength + index2)
        {
          stringBuilder2 = stringBuilder2.m_ChunkPrevious;
          if (stringBuilder2 == null)
            break;
        }
        if (index1 < 0)
          return index2 < 0;
      }
      while (index2 >= 0 && (int) stringBuilder1.m_ChunkChars[index1] == (int) stringBuilder2.m_ChunkChars[index2]);
      return false;
    }

    /// <summary>将此实例的子字符串中出现的所有指定字符串替换为其他指定字符串。</summary>
    /// <returns>对此实例的引用，其中从 <paramref name="startIndex" /> 到 <paramref name="startIndex" />+<paramref name="count" />- 1 的范围内 <paramref name="oldValue" /> 的所有实例被 <paramref name="newValue" /> 替换。</returns>
    /// <param name="oldValue">要替换的字符串。</param>
    /// <param name="newValue">替换 <paramref name="oldValue" /> 的字符串或 null。</param>
    /// <param name="startIndex">此实例中子字符串开始的位置。</param>
    /// <param name="count">子字符串的长度。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="oldValue" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="oldValue" /> 的长度为零。 </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" /> 或 <paramref name="count" /> 小于零。- 或 - <paramref name="startIndex" /> 与 <paramref name="count" /> 之和指示一个不在此实例内的字符位置。- 或 -增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Replace(string oldValue, string newValue, int startIndex, int count)
    {
      int length1 = this.Length;
      if ((uint) startIndex > (uint) length1)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0 || startIndex > length1 - count)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (oldValue == null)
        throw new ArgumentNullException("oldValue");
      if (oldValue.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "oldValue");
      if (newValue == null)
        newValue = "";
      int length2 = newValue.Length;
      int length3 = oldValue.Length;
      int[] replacements = (int[]) null;
      int replacementsCount = 0;
      StringBuilder chunkForIndex = this.FindChunkForIndex(startIndex);
      int indexInChunk = startIndex - chunkForIndex.m_ChunkOffset;
      while (count > 0)
      {
        if (this.StartsWith(chunkForIndex, indexInChunk, count, oldValue))
        {
          if (replacements == null)
            replacements = new int[5];
          else if (replacementsCount >= replacements.Length)
          {
            int[] numArray = new int[replacements.Length * 3 / 2 + 4];
            Array.Copy((Array) replacements, (Array) numArray, replacements.Length);
            replacements = numArray;
          }
          replacements[replacementsCount++] = indexInChunk;
          indexInChunk += oldValue.Length;
          count -= oldValue.Length;
        }
        else
        {
          ++indexInChunk;
          --count;
        }
        if (indexInChunk >= chunkForIndex.m_ChunkLength || count == 0)
        {
          int num = indexInChunk + chunkForIndex.m_ChunkOffset;
          this.ReplaceAllInChunk(replacements, replacementsCount, chunkForIndex, oldValue.Length, newValue);
          int index = num + (newValue.Length - oldValue.Length) * replacementsCount;
          replacementsCount = 0;
          chunkForIndex = this.FindChunkForIndex(index);
          indexInChunk = index - chunkForIndex.m_ChunkOffset;
        }
      }
      return this;
    }

    /// <summary>将此实例中出现的所有指定字符替换为其他指定字符。</summary>
    /// <returns>对此实例的引用，其中 <paramref name="oldChar" /> 被 <paramref name="newChar" /> 替换。</returns>
    /// <param name="oldChar">要替换的字符。</param>
    /// <param name="newChar">替换 <paramref name="oldChar" /> 的字符。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Replace(char oldChar, char newChar)
    {
      return this.Replace(oldChar, newChar, 0, this.Length);
    }

    /// <summary>将此实例的子字符串中出现的所有指定字符替换为其他指定字符。</summary>
    /// <returns>对此实例的引用，其中从 <paramref name="startIndex" /> 到 <paramref name="startIndex" /> + <paramref name="count" /> -1 范围内的 <paramref name="oldChar" /> 被 <paramref name="newChar" /> 替换。</returns>
    /// <param name="oldChar">要替换的字符。</param>
    /// <param name="newChar">替换 <paramref name="oldChar" /> 的字符。</param>
    /// <param name="startIndex">此实例中子字符串开始的位置。</param>
    /// <param name="count">子字符串的长度。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="startIndex" />+<paramref name="count" /> 大于此实例值的长度。- 或 - <paramref name="startIndex" /> 或 <paramref name="count" /> 小于零。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public StringBuilder Replace(char oldChar, char newChar, int startIndex, int count)
    {
      int length = this.Length;
      if ((uint) startIndex > (uint) length)
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (count < 0 || startIndex > length - count)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      int num = startIndex + count;
      StringBuilder stringBuilder = this;
      while (true)
      {
        int val2 = num - stringBuilder.m_ChunkOffset;
        int val1 = startIndex - stringBuilder.m_ChunkOffset;
        if (val2 >= 0)
        {
          int index1 = Math.Max(val1, 0);
          for (int index2 = Math.Min(stringBuilder.m_ChunkLength, val2); index1 < index2; ++index1)
          {
            if ((int) stringBuilder.m_ChunkChars[index1] == (int) oldChar)
              stringBuilder.m_ChunkChars[index1] = newChar;
          }
        }
        if (val1 < 0)
          stringBuilder = stringBuilder.m_ChunkPrevious;
        else
          break;
      }
      return this;
    }

    /// <summary>将追加到此实例的指定地址处开始的 Unicode 字符的数组。</summary>
    /// <returns>完成追加操作后对此实例的引用。 </returns>
    /// <param name="value">指向一个字符数组的指针。</param>
    /// <param name="valueCount">数组中的字符数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="valueCount" /> 小于零。- 或 -增大此实例的值将超过 <see cref="P:System.Text.StringBuilder.MaxCapacity" />。 </exception>
    /// <exception cref="T:System.NullReferenceException">
    /// <paramref name="value" />为 null 指针。</exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    public unsafe StringBuilder Append(char* value, int valueCount)
    {
      if (valueCount < 0)
        throw new ArgumentOutOfRangeException("valueCount", Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
      int num1 = valueCount + this.m_ChunkLength;
      if (num1 <= this.m_ChunkChars.Length)
      {
        StringBuilder.ThreadSafeCopy(value, this.m_ChunkChars, this.m_ChunkLength, valueCount);
        this.m_ChunkLength = num1;
      }
      else
      {
        int count = this.m_ChunkChars.Length - this.m_ChunkLength;
        if (count > 0)
        {
          StringBuilder.ThreadSafeCopy(value, this.m_ChunkChars, this.m_ChunkLength, count);
          this.m_ChunkLength = this.m_ChunkChars.Length;
        }
        int num2 = valueCount - count;
        this.ExpandByABlock(num2);
        StringBuilder.ThreadSafeCopy(value + count, this.m_ChunkChars, 0, num2);
        this.m_ChunkLength = num2;
      }
      return this;
    }

    [SecurityCritical]
    private unsafe void Insert(int index, char* value, int valueCount)
    {
      if ((uint) index > (uint) this.Length)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (valueCount <= 0)
        return;
      StringBuilder chunk;
      int indexInChunk;
      this.MakeRoom(index, valueCount, out chunk, out indexInChunk, false);
      this.ReplaceInPlaceAtChunk(ref chunk, ref indexInChunk, value, valueCount);
    }

    [SecuritySafeCritical]
    private unsafe void ReplaceAllInChunk(int[] replacements, int replacementsCount, StringBuilder sourceChunk, int removeCount, string value)
    {
      if (replacementsCount <= 0)
        return;
      string str = value;
      char* chPtr1 = (char*) str;
      if ((IntPtr) chPtr1 != IntPtr.Zero)
        chPtr1 += RuntimeHelpers.OffsetToStringData;
      int count = (value.Length - removeCount) * replacementsCount;
      StringBuilder chunk = sourceChunk;
      int indexInChunk = replacements[0];
      if (count > 0)
        this.MakeRoom(chunk.m_ChunkOffset + indexInChunk, count, out chunk, out indexInChunk, true);
      int index1 = 0;
      while (true)
      {
        this.ReplaceInPlaceAtChunk(ref chunk, ref indexInChunk, chPtr1, value.Length);
        int index2 = replacements[index1] + removeCount;
        ++index1;
        if (index1 < replacementsCount)
        {
          int num = replacements[index1];
          if (count != 0)
          {
            fixed (char* chPtr2 = &sourceChunk.m_ChunkChars[index2])
              this.ReplaceInPlaceAtChunk(ref chunk, ref indexInChunk, chPtr2, num - index2);
          }
          else
            indexInChunk += num - index2;
        }
        else
          break;
      }
      if (count < 0)
        this.Remove(chunk.m_ChunkOffset + indexInChunk, -count, out chunk, out indexInChunk);
      str = (string) null;
    }

    private bool StartsWith(StringBuilder chunk, int indexInChunk, int count, string value)
    {
      for (int index = 0; index < value.Length; ++index)
      {
        if (count == 0)
          return false;
        if (indexInChunk >= chunk.m_ChunkLength)
        {
          chunk = this.Next(chunk);
          if (chunk == null)
            return false;
          indexInChunk = 0;
        }
        if ((int) value[index] != (int) chunk.m_ChunkChars[indexInChunk])
          return false;
        ++indexInChunk;
        --count;
      }
      return true;
    }

    [SecurityCritical]
    private unsafe void ReplaceInPlaceAtChunk(ref StringBuilder chunk, ref int indexInChunk, char* value, int count)
    {
      if (count == 0)
        return;
      while (true)
      {
        int count1 = Math.Min(chunk.m_ChunkLength - indexInChunk, count);
        StringBuilder.ThreadSafeCopy(value, chunk.m_ChunkChars, indexInChunk, count1);
        indexInChunk += count1;
        if (indexInChunk >= chunk.m_ChunkLength)
        {
          chunk = this.Next(chunk);
          indexInChunk = 0;
        }
        count -= count1;
        if (count != 0)
          value += count1;
        else
          break;
      }
    }

    [SecurityCritical]
    private static unsafe void ThreadSafeCopy(char* sourcePtr, char[] destination, int destinationIndex, int count)
    {
      if (count <= 0)
        return;
      if ((uint) destinationIndex > (uint) destination.Length || destinationIndex + count > destination.Length)
        throw new ArgumentOutOfRangeException("destinationIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      fixed (char* dmem = &destination[destinationIndex])
        string.wstrcpy(dmem, sourcePtr, count);
    }

    [SecurityCritical]
    private static unsafe void ThreadSafeCopy(char[] source, int sourceIndex, char[] destination, int destinationIndex, int count)
    {
      if (count <= 0)
        return;
      if ((uint) sourceIndex > (uint) source.Length || sourceIndex + count > source.Length)
        throw new ArgumentOutOfRangeException("sourceIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      fixed (char* sourcePtr = &source[sourceIndex])
        StringBuilder.ThreadSafeCopy(sourcePtr, destination, destinationIndex, count);
    }

    [SecurityCritical]
    internal unsafe void InternalCopy(IntPtr dest, int len)
    {
      if (len == 0)
        return;
      bool flag = true;
      byte* numPtr = (byte*) dest.ToPointer();
      StringBuilder stringBuilder = this.FindChunkForByte(len);
      do
      {
        int num = stringBuilder.m_ChunkOffset * 2;
        int len1 = stringBuilder.m_ChunkLength * 2;
        fixed (char* chPtr = &stringBuilder.m_ChunkChars[0])
        {
          if (flag)
          {
            flag = false;
            Buffer.Memcpy(numPtr + num, (byte*) chPtr, len - num);
          }
          else
            Buffer.Memcpy(numPtr + num, (byte*) chPtr, len1);
        }
        stringBuilder = stringBuilder.m_ChunkPrevious;
      }
      while (stringBuilder != null);
    }

    private StringBuilder FindChunkForIndex(int index)
    {
      StringBuilder stringBuilder = this;
      while (stringBuilder.m_ChunkOffset > index)
        stringBuilder = stringBuilder.m_ChunkPrevious;
      return stringBuilder;
    }

    private StringBuilder FindChunkForByte(int byteIndex)
    {
      StringBuilder stringBuilder = this;
      while (stringBuilder.m_ChunkOffset * 2 > byteIndex)
        stringBuilder = stringBuilder.m_ChunkPrevious;
      return stringBuilder;
    }

    private StringBuilder Next(StringBuilder chunk)
    {
      if (chunk == this)
        return (StringBuilder) null;
      return this.FindChunkForIndex(chunk.m_ChunkOffset + chunk.m_ChunkLength);
    }

    private void ExpandByABlock(int minBlockCharCount)
    {
      if (minBlockCharCount + this.Length > this.m_MaxCapacity)
        throw new ArgumentOutOfRangeException("requiredLength", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
      int length = Math.Max(minBlockCharCount, Math.Min(this.Length, 8000));
      this.m_ChunkPrevious = new StringBuilder(this);
      this.m_ChunkOffset = this.m_ChunkOffset + this.m_ChunkLength;
      this.m_ChunkLength = 0;
      if (this.m_ChunkOffset + length < length)
      {
        this.m_ChunkChars = (char[]) null;
        throw new OutOfMemoryException();
      }
      this.m_ChunkChars = new char[length];
    }

    [SecuritySafeCritical]
    private unsafe void MakeRoom(int index, int count, out StringBuilder chunk, out int indexInChunk, bool doneMoveFollowingChars)
    {
      if (count + this.Length > this.m_MaxCapacity)
        throw new ArgumentOutOfRangeException("requiredLength", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
      chunk = this;
      while (chunk.m_ChunkOffset > index)
      {
        chunk.m_ChunkOffset += count;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        StringBuilder& local = @chunk;
        // ISSUE: explicit reference operation
        StringBuilder stringBuilder = (^local).m_ChunkPrevious;
        // ISSUE: explicit reference operation
        ^local = stringBuilder;
      }
      indexInChunk = index - chunk.m_ChunkOffset;
      if (!doneMoveFollowingChars && chunk.m_ChunkLength <= 32 && chunk.m_ChunkChars.Length - chunk.m_ChunkLength >= count)
      {
        int index1 = chunk.m_ChunkLength;
        while (index1 > indexInChunk)
        {
          --index1;
          chunk.m_ChunkChars[index1 + count] = chunk.m_ChunkChars[index1];
        }
        chunk.m_ChunkLength += count;
      }
      else
      {
        StringBuilder stringBuilder = new StringBuilder(Math.Max(count, 16), chunk.m_MaxCapacity, chunk.m_ChunkPrevious);
        stringBuilder.m_ChunkLength = count;
        int count1 = Math.Min(count, indexInChunk);
        if (count1 > 0)
        {
          fixed (char* sourcePtr = chunk.m_ChunkChars)
          {
            StringBuilder.ThreadSafeCopy(sourcePtr, stringBuilder.m_ChunkChars, 0, count1);
            int count2 = indexInChunk - count1;
            if (count2 >= 0)
            {
              StringBuilder.ThreadSafeCopy(sourcePtr + count1, chunk.m_ChunkChars, 0, count2);
              indexInChunk = count2;
            }
          }
        }
        chunk.m_ChunkPrevious = stringBuilder;
        chunk.m_ChunkOffset += count;
        if (count1 >= count)
          return;
        chunk = stringBuilder;
        indexInChunk = count1;
      }
    }

    [SecuritySafeCritical]
    private void Remove(int startIndex, int count, out StringBuilder chunk, out int indexInChunk)
    {
      int num = startIndex + count;
      chunk = this;
      StringBuilder stringBuilder1 = (StringBuilder) null;
      int sourceIndex = 0;
      while (true)
      {
        if (num - chunk.m_ChunkOffset >= 0)
        {
          if (stringBuilder1 == null)
          {
            stringBuilder1 = chunk;
            sourceIndex = num - stringBuilder1.m_ChunkOffset;
          }
          if (startIndex - chunk.m_ChunkOffset >= 0)
            break;
        }
        else
          chunk.m_ChunkOffset -= count;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        StringBuilder& local = @chunk;
        // ISSUE: explicit reference operation
        StringBuilder stringBuilder2 = (^local).m_ChunkPrevious;
        // ISSUE: explicit reference operation
        ^local = stringBuilder2;
      }
      indexInChunk = startIndex - chunk.m_ChunkOffset;
      int destinationIndex = indexInChunk;
      int count1 = stringBuilder1.m_ChunkLength - sourceIndex;
      if (stringBuilder1 != chunk)
      {
        destinationIndex = 0;
        chunk.m_ChunkLength = indexInChunk;
        stringBuilder1.m_ChunkPrevious = chunk;
        stringBuilder1.m_ChunkOffset = chunk.m_ChunkOffset + chunk.m_ChunkLength;
        if (indexInChunk == 0)
        {
          stringBuilder1.m_ChunkPrevious = chunk.m_ChunkPrevious;
          chunk = stringBuilder1;
        }
      }
      stringBuilder1.m_ChunkLength -= sourceIndex - destinationIndex;
      if (destinationIndex == sourceIndex)
        return;
      StringBuilder.ThreadSafeCopy(stringBuilder1.m_ChunkChars, sourceIndex, stringBuilder1.m_ChunkChars, destinationIndex, count1);
    }
  }
}
