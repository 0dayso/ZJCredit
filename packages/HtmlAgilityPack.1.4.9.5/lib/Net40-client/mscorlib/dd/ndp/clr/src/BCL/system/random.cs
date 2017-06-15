// Decompiled with JetBrains decompiler
// Type: System.Random
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>表示伪随机数生成器，这是一种能够产生满足某些随机性统计要求的数字序列的设备。若要浏览此类型的 .NET Framework 源代码，请参阅引用源。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class Random
  {
    private int[] SeedArray = new int[56];
    private const int MBIG = 2147483647;
    private const int MSEED = 161803398;
    private const int MZ = 0;
    private int inext;
    private int inextp;

    /// <summary>使用与时间相关的默认种子值，初始化 <see cref="T:System.Random" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public Random()
      : this(Environment.TickCount)
    {
    }

    /// <summary>使用指定的种子值初始化 <see cref="T:System.Random" /> 类的新实例。</summary>
    /// <param name="Seed">用来计算伪随机数序列起始值的数字。如果指定的是负数，则使用其绝对值。</param>
    [__DynamicallyInvokable]
    public Random(int Seed)
    {
      int num1 = 161803398 - (Seed == int.MinValue ? int.MaxValue : Math.Abs(Seed));
      this.SeedArray[55] = num1;
      int num2 = 1;
      for (int index1 = 1; index1 < 55; ++index1)
      {
        int index2 = 21 * index1 % 55;
        this.SeedArray[index2] = num2;
        num2 = num1 - num2;
        if (num2 < 0)
          num2 += int.MaxValue;
        num1 = this.SeedArray[index2];
      }
      for (int index1 = 1; index1 < 5; ++index1)
      {
        for (int index2 = 1; index2 < 56; ++index2)
        {
          this.SeedArray[index2] -= this.SeedArray[1 + (index2 + 30) % 55];
          if (this.SeedArray[index2] < 0)
            this.SeedArray[index2] += int.MaxValue;
        }
      }
      this.inext = 0;
      this.inextp = 21;
      Seed = 1;
    }

    /// <summary>返回一个介于 0.0 和 1.0 之间的随机浮点数。</summary>
    /// <returns>大于或等于 0.0 且小于 1.0 的双精度浮点数。</returns>
    [__DynamicallyInvokable]
    protected virtual double Sample()
    {
      return (double) this.InternalSample() * 4.6566128752458E-10;
    }

    private int InternalSample()
    {
      int num1 = this.inext;
      int num2 = this.inextp;
      int index1;
      if ((index1 = num1 + 1) >= 56)
        index1 = 1;
      int index2;
      if ((index2 = num2 + 1) >= 56)
        index2 = 1;
      int num3 = this.SeedArray[index1] - this.SeedArray[index2];
      if (num3 == int.MaxValue)
        --num3;
      if (num3 < 0)
        num3 += int.MaxValue;
      this.SeedArray[index1] = num3;
      this.inext = index1;
      this.inextp = index2;
      return num3;
    }

    /// <summary>返回一个非负随机整数。</summary>
    /// <returns>大于等于零且小于 <see cref="F:System.Int32.MaxValue" /> 的 32 位带符号整数。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual int Next()
    {
      return this.InternalSample();
    }

    private double GetSampleForLargeRange()
    {
      int num = this.InternalSample();
      if ((this.InternalSample() % 2 == 0 ? 1 : 0) != 0)
        num = -num;
      return ((double) num + 2147483646.0) / 4294967293.0;
    }

    /// <summary>返回在指定范围内的任意整数。</summary>
    /// <returns>大于等于 <paramref name="minValue" /> 且小于 <paramref name="maxValue" /> 的 32 位带符号整数，即：返回值的范围包括 <paramref name="minValue" /> 但不包括 <paramref name="maxValue" />。如果 <paramref name="minValue" /> 等于 <paramref name="maxValue" />，则返回 <paramref name="minValue" />。</returns>
    /// <param name="minValue">返回的随机数的下界（随机数可取该下界值）。</param>
    /// <param name="maxValue">返回的随机数的上限（随机数不能取该上限值）。<paramref name="maxValue" /> 必须大于等于 <paramref name="minValue" />。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="minValue" /> is greater than <paramref name="maxValue" />. </exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual int Next(int minValue, int maxValue)
    {
      if (minValue > maxValue)
        throw new ArgumentOutOfRangeException("minValue", Environment.GetResourceString("Argument_MinMaxValue", (object) "minValue", (object) "maxValue"));
      long num = (long) maxValue - (long) minValue;
      if (num <= (long) int.MaxValue)
        return (int) (this.Sample() * (double) num) + minValue;
      return (int) ((long) (this.GetSampleForLargeRange() * (double) num) + (long) minValue);
    }

    /// <summary>返回一个小于所指定最大值的非负随机整数。</summary>
    /// <returns>大于等于零且小于 <paramref name="maxValue" /> 的 32 位带符号整数，即：返回值的范围通常包括零但不包括 <paramref name="maxValue" />。但是，如果 <paramref name="maxValue" /> 等于 0，则返回 <paramref name="maxValue" />。</returns>
    /// <param name="maxValue">要生成的随机数的上限（随机数不能取该上限值）。<paramref name="maxValue" /> 必须大于或等于 0。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="maxValue" /> is less than 0. </exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual int Next(int maxValue)
    {
      if (maxValue < 0)
        throw new ArgumentOutOfRangeException("maxValue", Environment.GetResourceString("ArgumentOutOfRange_MustBePositive", (object) "maxValue"));
      return (int) (this.Sample() * (double) maxValue);
    }

    /// <summary>返回一个大于或等于 0.0 且小于 1.0 的随机浮点数。</summary>
    /// <returns>大于或等于 0.0 且小于 1.0 的双精度浮点数。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual double NextDouble()
    {
      return this.Sample();
    }

    /// <summary>用随机数填充指定字节数组的元素。</summary>
    /// <param name="buffer">包含随机数的字节数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> is null. </exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual void NextBytes(byte[] buffer)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer");
      for (int index = 0; index < buffer.Length; ++index)
        buffer[index] = (byte) (this.InternalSample() % 256);
    }
  }
}
