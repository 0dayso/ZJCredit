// Decompiled with JetBrains decompiler
// Type: System.Collections.BitArray
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections
{
  /// <summary>管理位值的压缩数组，该值表示为布尔值，其中 true 表示位是打开的 (1)，false 表示位是关闭的 (0)。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class BitArray : ICollection, IEnumerable, ICloneable
  {
    private const int BitsPerInt32 = 32;
    private const int BytesPerInt32 = 4;
    private const int BitsPerByte = 8;
    private int[] m_array;
    private int m_length;
    private int _version;
    [NonSerialized]
    private object _syncRoot;
    private const int _ShrinkThreshold = 256;

    /// <summary>获取或设置 <see cref="T:System.Collections.BitArray" /> 中特定位置处的位值。</summary>
    /// <returns>在 <paramref name="index" /> 位置处的位的值。</returns>
    /// <param name="index">要获取或设置的值的从零开始索引。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.BitArray.Count" />. </exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool this[int index]
    {
      [__DynamicallyInvokable] get
      {
        return this.Get(index);
      }
      [__DynamicallyInvokable] set
      {
        this.Set(index, value);
      }
    }

    /// <summary>获取或设置 <see cref="T:System.Collections.BitArray" /> 中的元素数。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.BitArray" /> 中元素的数目。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The property is set to a value that is less than zero. </exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public int Length
    {
      [__DynamicallyInvokable] get
      {
        return this.m_length;
      }
      [__DynamicallyInvokable] set
      {
        if (value < 0)
          throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
        int arrayLength = BitArray.GetArrayLength(value, 32);
        if (arrayLength > this.m_array.Length || arrayLength + 256 < this.m_array.Length)
        {
          int[] numArray = new int[arrayLength];
          Array.Copy((Array) this.m_array, (Array) numArray, arrayLength > this.m_array.Length ? this.m_array.Length : arrayLength);
          this.m_array = numArray;
        }
        if (value > this.m_length)
        {
          int index = BitArray.GetArrayLength(this.m_length, 32) - 1;
          int num = this.m_length % 32;
          if (num > 0)
            this.m_array[index] &= (1 << num) - 1;
          Array.Clear((Array) this.m_array, index + 1, arrayLength - index - 1);
        }
        this.m_length = value;
        this._version = this._version + 1;
      }
    }

    /// <summary>获取 <see cref="T:System.Collections.BitArray" /> 中包含的元素数。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.BitArray" /> 中包含的元素数。</returns>
    /// <filterpriority>2</filterpriority>
    public int Count
    {
      get
      {
        return this.m_length;
      }
    }

    /// <summary>获取可用于同步对 <see cref="T:System.Collections.BitArray" /> 的访问的对象。</summary>
    /// <returns>可用于同步对 <see cref="T:System.Collections.BitArray" /> 的访问的对象。</returns>
    /// <filterpriority>2</filterpriority>
    public object SyncRoot
    {
      get
      {
        if (this._syncRoot == null)
          Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), (object) null);
        return this._syncRoot;
      }
    }

    /// <summary>获取一个值，该值指示 <see cref="T:System.Collections.BitArray" /> 是否为只读。</summary>
    /// <returns>此属性恒为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    /// <summary>获取一个值，该值指示是否同步对 <see cref="T:System.Collections.BitArray" /> 的访问（线程安全）。</summary>
    /// <returns>此属性恒为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public bool IsSynchronized
    {
      get
      {
        return false;
      }
    }

    private BitArray()
    {
    }

    /// <summary>初始化 <see cref="T:System.Collections.BitArray" /> 类的新实例，该实例可拥有指定数目的位值，位值最初设置为 false。</summary>
    /// <param name="length">新 <see cref="T:System.Collections.BitArray" /> 中位值的数目。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="length" /> is less than zero. </exception>
    [__DynamicallyInvokable]
    public BitArray(int length)
      : this(length, false)
    {
    }

    /// <summary>初始化 <see cref="T:System.Collections.BitArray" /> 类的新实例，该实例可拥有指定数目的位值，位值最初设置为指定值。</summary>
    /// <param name="length">新 <see cref="T:System.Collections.BitArray" /> 中位值的数目。</param>
    /// <param name="defaultValue">要分配给每个比特位的布尔值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="length" /> is less than zero. </exception>
    [__DynamicallyInvokable]
    public BitArray(int length, bool defaultValue)
    {
      if (length < 0)
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      this.m_array = new int[BitArray.GetArrayLength(length, 32)];
      this.m_length = length;
      int num = defaultValue ? -1 : 0;
      for (int index = 0; index < this.m_array.Length; ++index)
        this.m_array[index] = num;
      this._version = 0;
    }

    /// <summary>初始化 <see cref="T:System.Collections.BitArray" /> 类的新实例，该实例包含从指定的字节数组复制的位值。</summary>
    /// <param name="bytes">字节数组包含要复制的值，在这里每个字节代表八个连续位。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bytes" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">The length of <paramref name="bytes" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
    [__DynamicallyInvokable]
    public BitArray(byte[] bytes)
    {
      if (bytes == null)
        throw new ArgumentNullException("bytes");
      if (bytes.Length > 268435455)
        throw new ArgumentException(Environment.GetResourceString("Argument_ArrayTooLarge", (object) 8), "bytes");
      this.m_array = new int[BitArray.GetArrayLength(bytes.Length, 4)];
      this.m_length = bytes.Length * 8;
      int index1 = 0;
      int index2 = 0;
      while (bytes.Length - index2 >= 4)
      {
        this.m_array[index1++] = (int) bytes[index2] & (int) byte.MaxValue | ((int) bytes[index2 + 1] & (int) byte.MaxValue) << 8 | ((int) bytes[index2 + 2] & (int) byte.MaxValue) << 16 | ((int) bytes[index2 + 3] & (int) byte.MaxValue) << 24;
        index2 += 4;
      }
      switch (bytes.Length - index2)
      {
        case 1:
          this.m_array[index1] |= (int) bytes[index2] & (int) byte.MaxValue;
          break;
        case 2:
          this.m_array[index1] |= ((int) bytes[index2 + 1] & (int) byte.MaxValue) << 8;
          goto case 1;
        case 3:
          this.m_array[index1] = ((int) bytes[index2 + 2] & (int) byte.MaxValue) << 16;
          goto case 2;
      }
      this._version = 0;
    }

    /// <summary>初始化 <see cref="T:System.Collections.BitArray" /> 类的新实例，该实例包含从布尔值指定数组复制的位值。</summary>
    /// <param name="values">要复制的布尔值数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="values" /> is null. </exception>
    [__DynamicallyInvokable]
    public BitArray(bool[] values)
    {
      if (values == null)
        throw new ArgumentNullException("values");
      this.m_array = new int[BitArray.GetArrayLength(values.Length, 32)];
      this.m_length = values.Length;
      for (int index = 0; index < values.Length; ++index)
      {
        if (values[index])
          this.m_array[index / 32] |= 1 << index % 32;
      }
      this._version = 0;
    }

    /// <summary>初始化 <see cref="T:System.Collections.BitArray" /> 类的新实例，该实例包含从指定的 32 位整数数组复制的位值。</summary>
    /// <param name="values">整数数组包含要复制的值，在这里每个整数代表 32 个连续位。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="values" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">The length of <paramref name="values" /> is greater than <see cref="F:System.Int32.MaxValue" /></exception>
    [__DynamicallyInvokable]
    public BitArray(int[] values)
    {
      if (values == null)
        throw new ArgumentNullException("values");
      if (values.Length > 67108863)
        throw new ArgumentException(Environment.GetResourceString("Argument_ArrayTooLarge", (object) 32), "values");
      this.m_array = new int[values.Length];
      this.m_length = values.Length * 32;
      Array.Copy((Array) values, (Array) this.m_array, values.Length);
      this._version = 0;
    }

    /// <summary>初始化 <see cref="T:System.Collections.BitArray" /> 类的新实例，该实例包含从指定 <see cref="T:System.Collections.BitArray" /> 复制的位值。</summary>
    /// <param name="bits">要复制的 <see cref="T:System.Collections.BitArray" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bits" /> is null. </exception>
    [__DynamicallyInvokable]
    public BitArray(BitArray bits)
    {
      if (bits == null)
        throw new ArgumentNullException("bits");
      int arrayLength = BitArray.GetArrayLength(bits.m_length, 32);
      this.m_array = new int[arrayLength];
      this.m_length = bits.m_length;
      Array.Copy((Array) bits.m_array, (Array) this.m_array, arrayLength);
      this._version = bits._version;
    }

    /// <summary>获取 <see cref="T:System.Collections.BitArray" /> 中特定位置处的位值。</summary>
    /// <returns>在 <paramref name="index" /> 位置处的位的值。</returns>
    /// <param name="index">要获取的值的从零开始索引。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is greater than or equal to the number of elements in the <see cref="T:System.Collections.BitArray" />. </exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool Get(int index)
    {
      if (index < 0 || index >= this.Length)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      return (uint) (this.m_array[index / 32] & 1 << index % 32) > 0U;
    }

    /// <summary>将 <see cref="T:System.Collections.BitArray" /> 中特定位置处的位设置为指定值。</summary>
    /// <param name="index">要设置的位的从零开始索引。</param>
    /// <param name="value">要分配给比特位的布尔值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is greater than or equal to the number of elements in the <see cref="T:System.Collections.BitArray" />. </exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public void Set(int index, bool value)
    {
      if (index < 0 || index >= this.Length)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (value)
        this.m_array[index / 32] |= 1 << index % 32;
      else
        this.m_array[index / 32] &= ~(1 << index % 32);
      this._version = this._version + 1;
    }

    /// <summary>将 <see cref="T:System.Collections.BitArray" /> 中所有位设置为指定值。</summary>
    /// <param name="value">要分配给所有位的布尔值。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public void SetAll(bool value)
    {
      int num = value ? -1 : 0;
      int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
      for (int index = 0; index < arrayLength; ++index)
        this.m_array[index] = num;
      this._version = this._version + 1;
    }

    /// <summary>针对指定的 <see cref="T:System.Collections.BitArray" /> 中的相应元素对当前 <see cref="T:System.Collections.BitArray" /> 中的元素执行按位“与”运算。</summary>
    /// <returns>当前实例包含针对指定的 <see cref="T:System.Collections.BitArray" /> 中的相应元素对当前 <see cref="T:System.Collections.BitArray" /> 中的元素执行按位“与”运算的结果。</returns>
    /// <param name="value">用其执行按位“与”运算的 <see cref="T:System.Collections.BitArray" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="value" /> and the current <see cref="T:System.Collections.BitArray" /> do not have the same number of elements. </exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public BitArray And(BitArray value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (this.Length != value.Length)
        throw new ArgumentException(Environment.GetResourceString("Arg_ArrayLengthsDiffer"));
      int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
      for (int index = 0; index < arrayLength; ++index)
        this.m_array[index] &= value.m_array[index];
      this._version = this._version + 1;
      return this;
    }

    /// <summary>针对指定的 <see cref="T:System.Collections.BitArray" /> 中的相应元素对当前 <see cref="T:System.Collections.BitArray" /> 中的元素执行按位“或”运算。</summary>
    /// <returns>当前实例包含针对指定的 <see cref="T:System.Collections.BitArray" /> 中的相应元素对当前 <see cref="T:System.Collections.BitArray" /> 中的元素执行按位“或”运算的结果。</returns>
    /// <param name="value">用其执行按位“或”运算的 <see cref="T:System.Collections.BitArray" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="value" /> and the current <see cref="T:System.Collections.BitArray" /> do not have the same number of elements. </exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public BitArray Or(BitArray value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (this.Length != value.Length)
        throw new ArgumentException(Environment.GetResourceString("Arg_ArrayLengthsDiffer"));
      int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
      for (int index = 0; index < arrayLength; ++index)
        this.m_array[index] |= value.m_array[index];
      this._version = this._version + 1;
      return this;
    }

    /// <summary>针对指定的 <see cref="T:System.Collections.BitArray" /> 中的相应元素对当前 <see cref="T:System.Collections.BitArray" /> 中的元素执行按位“异或”运算。</summary>
    /// <returns>当前实例包含针对指定的 <see cref="T:System.Collections.BitArray" /> 中的相应元素对当前 <see cref="T:System.Collections.BitArray" /> 中的元素执行按位“异或”运算的结果。</returns>
    /// <param name="value">用其执行按位“异或”运算的 <see cref="T:System.Collections.BitArray" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="value" /> and the current <see cref="T:System.Collections.BitArray" /> do not have the same number of elements. </exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public BitArray Xor(BitArray value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (this.Length != value.Length)
        throw new ArgumentException(Environment.GetResourceString("Arg_ArrayLengthsDiffer"));
      int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
      for (int index = 0; index < arrayLength; ++index)
        this.m_array[index] ^= value.m_array[index];
      this._version = this._version + 1;
      return this;
    }

    /// <summary>反转当前 <see cref="T:System.Collections.BitArray" /> 中的所有位值，以便将设置为 true 的元素更改为 false；将设置为 false 的元素更改为 true。</summary>
    /// <returns>具有已反转的位值的当前实例。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public BitArray Not()
    {
      int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
      for (int index = 0; index < arrayLength; ++index)
        this.m_array[index] = ~this.m_array[index];
      this._version = this._version + 1;
      return this;
    }

    /// <summary>从目标数组的指定索引处开始将整个 <see cref="T:System.Collections.BitArray" /> 复制到兼容的一维 <see cref="T:System.Array" />。</summary>
    /// <param name="array">一维 <see cref="T:System.Array" />，它是从 <see cref="T:System.Collections.BitArray" /> 复制的元素的目标。<see cref="T:System.Array" /> 必须具有从零开始的索引。</param>
    /// <param name="index">
    /// <paramref name="array" /> 中从零开始的索引，从此索引处开始进行复制。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> is null. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> is less than zero. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.BitArray" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
    /// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.BitArray" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
    /// <filterpriority>2</filterpriority>
    public void CopyTo(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (index < 0)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (array.Rank != 1)
        throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
      if (array is int[])
        Array.Copy((Array) this.m_array, 0, array, index, BitArray.GetArrayLength(this.m_length, 32));
      else if (array is byte[])
      {
        int arrayLength = BitArray.GetArrayLength(this.m_length, 8);
        if (array.Length - index < arrayLength)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        byte[] numArray = (byte[]) array;
        for (int index1 = 0; index1 < arrayLength; ++index1)
          numArray[index + index1] = (byte) (this.m_array[index1 / 4] >> index1 % 4 * 8 & (int) byte.MaxValue);
      }
      else
      {
        if (!(array is bool[]))
          throw new ArgumentException(Environment.GetResourceString("Arg_BitArrayTypeUnsupported"));
        if (array.Length - index < this.m_length)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
        bool[] flagArray = (bool[]) array;
        for (int index1 = 0; index1 < this.m_length; ++index1)
          flagArray[index + index1] = (uint) (this.m_array[index1 / 32] >> index1 % 32 & 1) > 0U;
      }
    }

    /// <summary>创建 <see cref="T:System.Collections.BitArray" /> 的浅表副本。</summary>
    /// <returns>
    /// <see cref="T:System.Collections.BitArray" /> 的浅表复制。</returns>
    /// <filterpriority>2</filterpriority>
    public object Clone()
    {
      return (object) new BitArray(this.m_array) { _version = this._version, m_length = this.m_length };
    }

    /// <summary>返回循环访问 <see cref="T:System.Collections.BitArray" /> 的枚举数。</summary>
    /// <returns>一个用于整个 <see cref="T:System.Collections.BitArray" /> 的 <see cref="T:System.Collections.IEnumerator" />。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public IEnumerator GetEnumerator()
    {
      return (IEnumerator) new BitArray.BitArrayEnumeratorSimple(this);
    }

    private static int GetArrayLength(int n, int div)
    {
      if (n <= 0)
        return 0;
      return (n - 1) / div + 1;
    }

    [Serializable]
    private class BitArrayEnumeratorSimple : IEnumerator, ICloneable
    {
      private BitArray bitarray;
      private int index;
      private int version;
      private bool currentElement;

      public virtual object Current
      {
        get
        {
          if (this.index == -1)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
          if (this.index >= this.bitarray.Count)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
          return (object) this.currentElement;
        }
      }

      internal BitArrayEnumeratorSimple(BitArray bitarray)
      {
        this.bitarray = bitarray;
        this.index = -1;
        this.version = bitarray._version;
      }

      public object Clone()
      {
        return this.MemberwiseClone();
      }

      public virtual bool MoveNext()
      {
        if (this.version != this.bitarray._version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        if (this.index < this.bitarray.Count - 1)
        {
          this.index = this.index + 1;
          this.currentElement = this.bitarray.Get(this.index);
          return true;
        }
        this.index = this.bitarray.Count;
        return false;
      }

      public void Reset()
      {
        if (this.version != this.bitarray._version)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        this.index = -1;
      }
    }
  }
}
