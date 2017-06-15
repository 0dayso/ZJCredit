// Decompiled with JetBrains decompiler
// Type: System.Decimal
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
  /// <summary>表示十进制数。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [NonVersionable]
  [__DynamicallyInvokable]
  [Serializable]
  public struct Decimal : IFormattable, IComparable, IConvertible, IDeserializationCallback, IComparable<Decimal>, IEquatable<Decimal>
  {
    private static uint[] Powers10 = new uint[10]
    {
      1U,
      10U,
      100U,
      1000U,
      10000U,
      100000U,
      1000000U,
      10000000U,
      100000000U,
      1000000000U
    };
    /// <summary>表示数字零 (0)。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public const Decimal Zero = new Decimal(0);
    /// <summary>表示数字一 (1)。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public const Decimal One = new Decimal(1);
    /// <summary>表示数字负一 (-1)。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public const Decimal MinusOne = new Decimal(-1);
    /// <summary>表示 <see cref="T:System.Decimal" /> 的最大可能值。该字段是常数且为只读。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public const Decimal MaxValue = new Decimal(-1, -1, -1, false, (byte) 0);
    /// <summary>表示 <see cref="T:System.Decimal" /> 的最小可能值。该字段是常数且为只读。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public const Decimal MinValue = new Decimal(-1, -1, -1, true, (byte) 0);
    private const Decimal NearNegativeZero = new Decimal(1, 0, 0, true, (byte) 27);
    private const Decimal NearPositiveZero = new Decimal(1, 0, 0, false, (byte) 27);
    private const int SignMask = -2147483648;
    private const byte DECIMAL_NEG = 128;
    private const byte DECIMAL_ADD = 0;
    private const int ScaleMask = 16711680;
    private const int ScaleShift = 16;
    private const int MaxInt32Scale = 9;
    private int flags;
    private int hi;
    private int lo;
    private int mid;

    /// <summary>将 <see cref="T:System.Decimal" /> 的新实例初始化为指定的 32 位有符号整数的值。</summary>
    /// <param name="value">要表示为 <see cref="T:System.Decimal" /> 的值。</param>
    [__DynamicallyInvokable]
    public Decimal(int value)
    {
      int num = value;
      if (num >= 0)
      {
        this.flags = 0;
      }
      else
      {
        this.flags = int.MinValue;
        num = -num;
      }
      this.lo = num;
      this.mid = 0;
      this.hi = 0;
    }

    /// <summary>将 <see cref="T:System.Decimal" /> 的新实例初始化为指定的 32 位无符号整数的值。</summary>
    /// <param name="value">要表示为 <see cref="T:System.Decimal" /> 的值。</param>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public Decimal(uint value)
    {
      this.flags = 0;
      this.lo = (int) value;
      this.mid = 0;
      this.hi = 0;
    }

    /// <summary>将 <see cref="T:System.Decimal" /> 的新实例初始化为指定的 64 位有符号整数的值。</summary>
    /// <param name="value">要表示为 <see cref="T:System.Decimal" /> 的值。</param>
    [__DynamicallyInvokable]
    public Decimal(long value)
    {
      long num = value;
      if (num >= 0L)
      {
        this.flags = 0;
      }
      else
      {
        this.flags = int.MinValue;
        num = -num;
      }
      this.lo = (int) num;
      this.mid = (int) (num >> 32);
      this.hi = 0;
    }

    /// <summary>将 <see cref="T:System.Decimal" /> 的新实例初始化为指定的 64 位无符号整数的值。</summary>
    /// <param name="value">要表示为 <see cref="T:System.Decimal" /> 的值。</param>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public Decimal(ulong value)
    {
      this.flags = 0;
      this.lo = (int) value;
      this.mid = (int) (value >> 32);
      this.hi = 0;
    }

    /// <summary>将 <see cref="T:System.Decimal" /> 的新实例初始化为指定的单精度浮点数的值。</summary>
    /// <param name="value">要表示为 <see cref="T:System.Decimal" /> 的值。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> is greater than <see cref="F:System.Decimal.MaxValue" /> or less than <see cref="F:System.Decimal.MinValue" />.-or- <paramref name="value" /> is <see cref="F:System.Single.NaN" />, <see cref="F:System.Single.PositiveInfinity" />, or <see cref="F:System.Single.NegativeInfinity" />. </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public Decimal(float value);

    /// <summary>将 <see cref="T:System.Decimal" /> 的新实例初始化为指定的双精度浮点数的值。</summary>
    /// <param name="value">要表示为 <see cref="T:System.Decimal" /> 的值。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> is greater than <see cref="F:System.Decimal.MaxValue" /> or less than <see cref="F:System.Decimal.MinValue" />.-or- <paramref name="value" /> is <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.PositiveInfinity" />, or <see cref="F:System.Double.NegativeInfinity" />. </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public Decimal(double value);

    internal Decimal(Currency value)
    {
      Decimal @decimal = Currency.ToDecimal(value);
      this.lo = @decimal.lo;
      this.mid = @decimal.mid;
      this.hi = @decimal.hi;
      this.flags = @decimal.flags;
    }

    /// <summary>将 <see cref="T:System.Decimal" /> 的新实例初始化为以二进制表示的、包含在指定数组中的十进制值。</summary>
    /// <param name="bits">包含十进制值表示形式的 32 位有符号整数的数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="bits" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">The length of the <paramref name="bits" /> is not 4.-or- The representation of the decimal value in <paramref name="bits" /> is not valid. </exception>
    [__DynamicallyInvokable]
    public Decimal(int[] bits)
    {
      this.lo = 0;
      this.mid = 0;
      this.hi = 0;
      this.flags = 0;
      this.SetBits(bits);
    }

    /// <summary>用指定实例构成部分的参数来初始化 <see cref="T:System.Decimal" /> 的新实例。</summary>
    /// <param name="lo">96 位整数的低 32 位。</param>
    /// <param name="mid">96 位整数的中间 32 位。</param>
    /// <param name="hi">96 位整数的高 32 位。</param>
    /// <param name="isNegative">若要指示负数，则为 true；若要指示正数，则为 false。</param>
    /// <param name="scale">10 的指数（0 到 28 之间）。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="scale" /> is greater than 28. </exception>
    [__DynamicallyInvokable]
    public Decimal(int lo, int mid, int hi, bool isNegative, byte scale)
    {
      if ((int) scale > 28)
        throw new ArgumentOutOfRangeException("scale", Environment.GetResourceString("ArgumentOutOfRange_DecimalScale"));
      this.lo = lo;
      this.mid = mid;
      this.hi = hi;
      this.flags = (int) scale << 16;
      if (!isNegative)
        return;
      this.flags = this.flags | int.MinValue;
    }

    private Decimal(int lo, int mid, int hi, int flags)
    {
      if ((flags & 2130771967) != 0 || (flags & 16711680) > 1835008)
        throw new ArgumentException(Environment.GetResourceString("Arg_DecBitCtor"));
      this.lo = lo;
      this.mid = mid;
      this.hi = hi;
      this.flags = flags;
    }

    /// <summary>定义从 8 位无符号整数到 <see cref="T:System.Decimal" /> 的显式转换。</summary>
    /// <returns>已转换的 8 位无符号整数。</returns>
    /// <param name="value">要转换的 8 位无符号整数。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static implicit operator Decimal(byte value)
    {
      return new Decimal((int) value);
    }

    /// <summary>定义从 8 位有符号整数到 <see cref="T:System.Decimal" /> 的显式转换。</summary>
    /// <returns>转换后的 8 位有符号整数。</returns>
    /// <param name="value">要转换的 8 位带符号整数。</param>
    /// <filterpriority>3</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static implicit operator Decimal(sbyte value)
    {
      return new Decimal((int) value);
    }

    /// <summary>定义从 16 位有符号整数到 <see cref="T:System.Decimal" /> 的显式转换。</summary>
    /// <returns>转换后的 16 位有符号整数。</returns>
    /// <param name="value">要转换的 16 位有符号整数。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static implicit operator Decimal(short value)
    {
      return new Decimal((int) value);
    }

    /// <summary>定义从 16 位无符号整数到 <see cref="T:System.Decimal" /> 的显式转换。</summary>
    /// <returns>已转换的 16 位无符号整数。</returns>
    /// <param name="value">要转换的 16 位无符号整数。</param>
    /// <filterpriority>3</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static implicit operator Decimal(ushort value)
    {
      return new Decimal((int) value);
    }

    /// <summary>定义从 Unicode 字符到 <see cref="T:System.Decimal" /> 的显式转换。</summary>
    /// <returns>转换后的 Unicode 字符。</returns>
    /// <param name="value">要转换的 Unicode 字符。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static implicit operator Decimal(char value)
    {
      return new Decimal((int) value);
    }

    /// <summary>定义从 32 位有符号整数到 <see cref="T:System.Decimal" /> 的显式转换。</summary>
    /// <returns>转换后的 32 位有符号整数。</returns>
    /// <param name="value">要转换的 32 位带符号整数。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static implicit operator Decimal(int value)
    {
      return new Decimal(value);
    }

    /// <summary>定义从 32 位无符号整数到 <see cref="T:System.Decimal" /> 的显式转换。</summary>
    /// <returns>已转换的 32 位无符号整数。</returns>
    /// <param name="value">要转换的 32 位无符号整数。</param>
    /// <filterpriority>3</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static implicit operator Decimal(uint value)
    {
      return new Decimal(value);
    }

    /// <summary>定义从 64 位有符号整数到 <see cref="T:System.Decimal" /> 的显式转换。</summary>
    /// <returns>转换后的 64 位有符号整数。</returns>
    /// <param name="value">要转换的 64 位带符号整数。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static implicit operator Decimal(long value)
    {
      return new Decimal(value);
    }

    /// <summary>定义从 64 位无符号整数到 <see cref="T:System.Decimal" /> 的显式转换。</summary>
    /// <returns>已转换的 64 位无符号整数。</returns>
    /// <param name="value">要转换的 64 位无符号整数。</param>
    /// <filterpriority>3</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static implicit operator Decimal(ulong value)
    {
      return new Decimal(value);
    }

    /// <summary>定义从单精度浮点数到 <see cref="T:System.Decimal" /> 的显式转换。</summary>
    /// <returns>已转换的单精度浮点数。</returns>
    /// <param name="value">要转换的单精度浮点数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.-or- <paramref name="value" /> is <see cref="F:System.Single.NaN" />, <see cref="F:System.Single.PositiveInfinity" />, or <see cref="F:System.Single.NegativeInfinity" />. </exception>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static explicit operator Decimal(float value)
    {
      return new Decimal(value);
    }

    /// <summary>定义从双精度浮点数到 <see cref="T:System.Decimal" /> 的显式转换。</summary>
    /// <returns>已转换的双精度浮点数。</returns>
    /// <param name="value">要转换的双精度浮点数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.-or- <paramref name="value" /> is <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.PositiveInfinity" />, or <see cref="F:System.Double.NegativeInfinity" />. </exception>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static explicit operator Decimal(double value)
    {
      return new Decimal(value);
    }

    /// <summary>定义从 <see cref="T:System.Decimal" /> 到 8 位无符号整数的显式转换。</summary>
    /// <returns>8 位无符号整数，它表示转换后的 <see cref="T:System.Decimal" />。</returns>
    /// <param name="value">要转换的值。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. </exception>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static explicit operator byte(Decimal value)
    {
      return Decimal.ToByte(value);
    }

    /// <summary>定义从 <see cref="T:System.Decimal" /> 到 8 位有符号整数的显式转换。</summary>
    /// <returns>8 位有符号整数，它表示转换后的 <see cref="T:System.Decimal" />。</returns>
    /// <param name="value">要转换的值。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />. </exception>
    /// <filterpriority>3</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static explicit operator sbyte(Decimal value)
    {
      return Decimal.ToSByte(value);
    }

    /// <summary>定义从 Unicode 字符到 <see cref="T:System.Decimal" /> 的显式转换。</summary>
    /// <returns>表示转换后的 <see cref="T:System.Decimal" /> 的 Unicode 字符。</returns>
    /// <param name="value">要转换的值。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> is less than <see cref="F:System.Char.MinValue" /> or greater than <see cref="F:System.Char.MaxValue" />. </exception>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static explicit operator char(Decimal value)
    {
      try
      {
        return (char) Decimal.ToUInt16(value);
      }
      catch (OverflowException ex)
      {
        throw new OverflowException(Environment.GetResourceString("Overflow_Char"), (Exception) ex);
      }
    }

    /// <summary>定义从 <see cref="T:System.Decimal" /> 到 16 位有符号整数的显式转换。</summary>
    /// <returns>16 位有符号整数，它表示转换后的 <see cref="T:System.Decimal" />。</returns>
    /// <param name="value">要转换的值。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />. </exception>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static explicit operator short(Decimal value)
    {
      return Decimal.ToInt16(value);
    }

    /// <summary>定义从 <see cref="T:System.Decimal" /> 到 16 位无符号整数的显式转换。</summary>
    /// <returns>16 位无符号整数，它表示转换后的 <see cref="T:System.Decimal" />。</returns>
    /// <param name="value">要转换的值。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> is greater than <see cref="F:System.UInt16.MaxValue" /> or less than <see cref="F:System.UInt16.MinValue" />. </exception>
    /// <filterpriority>3</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static explicit operator ushort(Decimal value)
    {
      return Decimal.ToUInt16(value);
    }

    /// <summary>定义从 <see cref="T:System.Decimal" /> 到 32 位有符号整数的显式转换。</summary>
    /// <returns>32 位有符号整数，它表示转换后的 <see cref="T:System.Decimal" />。</returns>
    /// <param name="value">要转换的值。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static explicit operator int(Decimal value)
    {
      return Decimal.ToInt32(value);
    }

    /// <summary>定义从 <see cref="T:System.Decimal" /> 到 32 位无符号整数的显式转换。</summary>
    /// <returns>32 位无符号整数，它表示转换后的 <see cref="T:System.Decimal" />。</returns>
    /// <param name="value">要转换的值。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> is negative or greater than <see cref="F:System.UInt32.MaxValue" />. </exception>
    /// <filterpriority>3</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static explicit operator uint(Decimal value)
    {
      return Decimal.ToUInt32(value);
    }

    /// <summary>定义从 <see cref="T:System.Decimal" /> 到 64 位有符号整数的显式转换。</summary>
    /// <returns>64 位有符号整数，它表示转换后的 <see cref="T:System.Decimal" />。</returns>
    /// <param name="value">要转换的值。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />. </exception>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static explicit operator long(Decimal value)
    {
      return Decimal.ToInt64(value);
    }

    /// <summary>定义从 <see cref="T:System.Decimal" /> 到 64 位无符号整数的显式转换。</summary>
    /// <returns>64 位无符号整数，它表示转换后的 <see cref="T:System.Decimal" />。</returns>
    /// <param name="value">要转换的值。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> is negative or greater than <see cref="F:System.UInt64.MaxValue" />. </exception>
    /// <filterpriority>3</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static explicit operator ulong(Decimal value)
    {
      return Decimal.ToUInt64(value);
    }

    /// <summary>定义从 <see cref="T:System.Decimal" /> 到单精度浮点数的显式转换。</summary>
    /// <returns>单精度浮点数，它表示转换后的 <see cref="T:System.Decimal" />。</returns>
    /// <param name="value">要转换的值。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static explicit operator float(Decimal value)
    {
      return Decimal.ToSingle(value);
    }

    /// <summary>定义从 <see cref="T:System.Decimal" /> 到双精度浮点数的显式转换。</summary>
    /// <returns>双精度浮点数，它表示转换后的 <see cref="T:System.Decimal" />。</returns>
    /// <param name="value">要转换的值。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static explicit operator double(Decimal value)
    {
      return Decimal.ToDouble(value);
    }

    /// <summary>返回 <see cref="T:System.Decimal" /> 操作数的值（操作数符号不变）。</summary>
    /// <returns>操作数 <paramref name="d" /> 的值。</returns>
    /// <param name="d">要返回的操作数。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal operator +(Decimal d)
    {
      return d;
    }

    /// <summary>对指定 <see cref="T:System.Decimal" /> 操作数的值求反。</summary>
    /// <returns>
    /// <paramref name="d" /> 乘以负一 (-1) 的结果。</returns>
    /// <param name="d">要求反的值。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal operator -(Decimal d)
    {
      return Decimal.Negate(d);
    }

    /// <summary>将 <see cref="T:System.Decimal" /> 操作数增加 1。</summary>
    /// <returns>
    /// <paramref name="d" /> 增加 1 后的值。</returns>
    /// <param name="d">要递增的值。</param>
    /// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal operator ++(Decimal d)
    {
      return Decimal.Add(d, Decimal.One);
    }

    /// <summary>
    /// <see cref="T:System.Decimal" /> 操作数减 1。</summary>
    /// <returns>
    /// <paramref name="d" /> 减 1 所得的值。</returns>
    /// <param name="d">要递减的值。</param>
    /// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal operator --(Decimal d)
    {
      return Decimal.Subtract(d, Decimal.One);
    }

    /// <summary>将两个指定的 <see cref="T:System.Decimal" /> 值相加。</summary>
    /// <returns>
    /// <paramref name="d1" /> 与 <paramref name="d2" /> 相加的结果。</returns>
    /// <param name="d1">要相加的第一个值。</param>
    /// <param name="d2">要相加的第二个值。</param>
    /// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
    /// <filterpriority>3</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Decimal operator +(Decimal d1, Decimal d2)
    {
      Decimal.FCallAddSub(ref d1, ref d2, (byte) 0);
      return d1;
    }

    /// <summary>将两个指定的 <see cref="T:System.Decimal" /> 值相减。</summary>
    /// <returns>
    /// <paramref name="d2" /> 减 <paramref name="d1" /> 所得的结果。</returns>
    /// <param name="d1">被减数。</param>
    /// <param name="d2">减数。</param>
    /// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
    /// <filterpriority>3</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Decimal operator -(Decimal d1, Decimal d2)
    {
      Decimal.FCallAddSub(ref d1, ref d2, (byte) 128);
      return d1;
    }

    /// <summary>两个指定的 <see cref="T:System.Decimal" /> 值相乘。</summary>
    /// <returns>
    /// <paramref name="d1" /> 与 <paramref name="d2" /> 相乘的结果。</returns>
    /// <param name="d1">要相乘的第一个值。</param>
    /// <param name="d2">要相乘的第二个值。</param>
    /// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
    /// <filterpriority>3</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Decimal operator *(Decimal d1, Decimal d2)
    {
      Decimal.FCallMultiply(ref d1, ref d2);
      return d1;
    }

    /// <summary>将两个指定的 <see cref="T:System.Decimal" /> 值相除。</summary>
    /// <returns>
    /// <paramref name="d1" /> 除以 <paramref name="d2" /> 的结果。</returns>
    /// <param name="d1">被除数。</param>
    /// <param name="d2">除数。</param>
    /// <exception cref="T:System.DivideByZeroException">
    /// <paramref name="d2" /> is zero. </exception>
    /// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
    /// <filterpriority>3</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Decimal operator /(Decimal d1, Decimal d2)
    {
      Decimal.FCallDivide(ref d1, ref d2);
      return d1;
    }

    /// <summary>返回两个指定 <see cref="T:System.Decimal" /> 值相除所得的余数。</summary>
    /// <returns>该余数是由 <paramref name="d1" /> 除以 <paramref name="d2" /> 所得。</returns>
    /// <param name="d1">被除数。</param>
    /// <param name="d2">除数。</param>
    /// <exception cref="T:System.DivideByZeroException">
    /// <paramref name="d2" /> is zero. </exception>
    /// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal operator %(Decimal d1, Decimal d2)
    {
      return Decimal.Remainder(d1, d2);
    }

    /// <summary>返回一个值，该值指示两个 <see cref="T:System.Decimal" /> 值是否相等。</summary>
    /// <returns>如果 true 和 <paramref name="d1" /> 相等，则为 <paramref name="d2" />；否则为 false。</returns>
    /// <param name="d1">要比较的第一个值。</param>
    /// <param name="d2">要比较的第二个值。</param>
    /// <filterpriority>3</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static bool operator ==(Decimal d1, Decimal d2)
    {
      return Decimal.FCallCompare(ref d1, ref d2) == 0;
    }

    /// <summary>返回一个值，该值指示两个 <see cref="T:System.Decimal" /> 对象是否具有不同的值。</summary>
    /// <returns>如果 true 和 <paramref name="d1" /> 不相等，则为 <paramref name="d2" />；否则为 false。</returns>
    /// <param name="d1">要比较的第一个值。</param>
    /// <param name="d2">要比较的第二个值。</param>
    /// <filterpriority>3</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static bool operator !=(Decimal d1, Decimal d2)
    {
      return (uint) Decimal.FCallCompare(ref d1, ref d2) > 0U;
    }

    /// <summary>返回一个值，该值指示指定的 <see cref="T:System.Decimal" /> 是否小于另一个指定的 <see cref="T:System.Decimal" />。</summary>
    /// <returns>如果 <paramref name="d1" /> 小于 <paramref name="d2" />，则为 true；否则为 false。</returns>
    /// <param name="d1">要比较的第一个值。</param>
    /// <param name="d2">要比较的第二个值。</param>
    /// <filterpriority>3</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static bool operator <(Decimal d1, Decimal d2)
    {
      return Decimal.FCallCompare(ref d1, ref d2) < 0;
    }

    /// <summary>返回一个值，该值指示指定的 <see cref="T:System.Decimal" /> 是小于还是等于另一个指定的 <see cref="T:System.Decimal" />。</summary>
    /// <returns>如果 <paramref name="d1" /> 小于等于 <paramref name="d2" />，则为 true；否则为 false。</returns>
    /// <param name="d1">要比较的第一个值。</param>
    /// <param name="d2">要比较的第二个值。</param>
    /// <filterpriority>3</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static bool operator <=(Decimal d1, Decimal d2)
    {
      return Decimal.FCallCompare(ref d1, ref d2) <= 0;
    }

    /// <summary>返回一个值，该值指示指定的 <see cref="T:System.Decimal" /> 是否大于另一个指定的 <see cref="T:System.Decimal" />。</summary>
    /// <returns>如果 true 大于 <paramref name="d1" />，则为 <paramref name="d2" />；否则为 false。</returns>
    /// <param name="d1">要比较的第一个值。</param>
    /// <param name="d2">要比较的第二个值。</param>
    /// <filterpriority>3</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static bool operator >(Decimal d1, Decimal d2)
    {
      return Decimal.FCallCompare(ref d1, ref d2) > 0;
    }

    /// <summary>返回一个值，该值指示指定的 <see cref="T:System.Decimal" /> 是否大于等于另一个指定的 <see cref="T:System.Decimal" />。</summary>
    /// <returns>如果 <paramref name="d1" /> 大于等于 <paramref name="d2" />，则为 true；否则为 false。</returns>
    /// <param name="d1">要比较的第一个值。</param>
    /// <param name="d2">要比较的第二个值。</param>
    /// <filterpriority>3</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static bool operator >=(Decimal d1, Decimal d2)
    {
      return Decimal.FCallCompare(ref d1, ref d2) >= 0;
    }

    /// <summary>将指定的 <see cref="T:System.Decimal" /> 值转换为等效的 OLE 自动化货币值，该值包含在一个 64 位有符号整数中。</summary>
    /// <returns>包含 <paramref name="value" /> 的 OLE 自动化等效值的 64 位有符号整数。</returns>
    /// <param name="value">要转换的十进制数。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public static long ToOACurrency(Decimal value)
    {
      return new Currency(value).ToOACurrency();
    }

    /// <summary>将指定的 64 位有符号整数（它包含 OLE 自动化货币值）转换为等效的 <see cref="T:System.Decimal" /> 值。</summary>
    /// <returns>包含 <paramref name="cy" /> 的等效数的 <see cref="T:System.Decimal" />。</returns>
    /// <param name="cy">一个 OLE 自动化货币值。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal FromOACurrency(long cy)
    {
      return Currency.ToDecimal(Currency.FromOACurrency(cy));
    }

    private void SetBits(int[] bits)
    {
      if (bits == null)
        throw new ArgumentNullException("bits");
      if (bits.Length == 4)
      {
        int num = bits[3];
        if ((num & 2130771967) == 0 && (num & 16711680) <= 1835008)
        {
          this.lo = bits[0];
          this.mid = bits[1];
          this.hi = bits[2];
          this.flags = num;
          return;
        }
      }
      throw new ArgumentException(Environment.GetResourceString("Arg_DecBitCtor"));
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      try
      {
        this.SetBits(Decimal.GetBits(this));
      }
      catch (ArgumentException ex)
      {
        throw new SerializationException(Environment.GetResourceString("Overflow_Decimal"), (Exception) ex);
      }
    }

    void IDeserializationCallback.OnDeserialization(object sender)
    {
      try
      {
        this.SetBits(Decimal.GetBits(this));
      }
      catch (ArgumentException ex)
      {
        throw new SerializationException(Environment.GetResourceString("Overflow_Decimal"), (Exception) ex);
      }
    }

    internal static Decimal Abs(Decimal d)
    {
      return new Decimal(d.lo, d.mid, d.hi, d.flags & int.MaxValue);
    }

    /// <summary>将两个指定的 <see cref="T:System.Decimal" /> 值相加。</summary>
    /// <returns>
    /// <paramref name="d1" /> 与 <paramref name="d2" /> 的和。</returns>
    /// <param name="d1">要相加的第一个值。</param>
    /// <param name="d2">要相加的第二个值。</param>
    /// <exception cref="T:System.OverflowException">The sum of <paramref name="d1" /> and <paramref name="d2" /> is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Decimal Add(Decimal d1, Decimal d2)
    {
      Decimal.FCallAddSub(ref d1, ref d2, (byte) 0);
      return d1;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallAddSub(ref Decimal d1, ref Decimal d2, byte bSign);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallAddSubOverflowed(ref Decimal d1, ref Decimal d2, byte bSign, ref bool overflowed);

    /// <summary>返回大于或等于指定的十进制数的最小整数值。</summary>
    /// <returns>大于或等于 <paramref name="d" /> 参数的最小整数值。请注意，此方法返回 <see cref="T:System.Decimal" />，而不是整数类型。</returns>
    /// <param name="d">十进制数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal Ceiling(Decimal d)
    {
      return -Decimal.Floor(-d);
    }

    /// <summary>比较两个指定的 <see cref="T:System.Decimal" /> 值。</summary>
    /// <returns>有符号数字，指示 <paramref name="d1" /> 和 <paramref name="d2" /> 的相对值。Return value Meaning Less than zero <paramref name="d1" /> is less than <paramref name="d2" />. Zero <paramref name="d1" /> and <paramref name="d2" /> are equal. Greater than zero <paramref name="d1" /> is greater than <paramref name="d2" />. </returns>
    /// <param name="d1">要比较的第一个值。</param>
    /// <param name="d2">要比较的第二个值。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static int Compare(Decimal d1, Decimal d2)
    {
      return Decimal.FCallCompare(ref d1, ref d2);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int FCallCompare(ref Decimal d1, ref Decimal d2);

    /// <summary>将此实例与指定对象进行比较并返回一个对二者的相对值的比较。</summary>
    /// <returns>一个带符号数字，指示此实例和 <paramref name="value" /> 的相对值。Return value Meaning Less than zero This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. Greater than zero This instance is greater than <paramref name="value" />.-or- <paramref name="value" /> is null. </returns>
    /// <param name="value">要与此实例进行比较的对象，或 null。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="value" /> is not a <see cref="T:System.Decimal" />. </exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public int CompareTo(object value)
    {
      if (value == null)
        return 1;
      if (!(value is Decimal))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDecimal"));
      Decimal d2 = (Decimal) value;
      return Decimal.FCallCompare(ref this, ref d2);
    }

    /// <summary>将此实例与指定的 <see cref="T:System.Decimal" /> 对象进行比较并返回一个对二者的相对值的比较。</summary>
    /// <returns>一个带符号数字，指示此实例和 <paramref name="value" /> 的相对值。Return value Meaning Less than zero This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. Greater than zero This instance is greater than <paramref name="value" />. </returns>
    /// <param name="value">与该实例进行比较的对象。</param>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public int CompareTo(Decimal value)
    {
      return Decimal.FCallCompare(ref this, ref value);
    }

    /// <summary>将两个指定的 <see cref="T:System.Decimal" /> 值相除。</summary>
    /// <returns>
    /// <paramref name="d1" /> 除以 <paramref name="d2" /> 的结果。</returns>
    /// <param name="d1">被除数。</param>
    /// <param name="d2">除数。</param>
    /// <exception cref="T:System.DivideByZeroException">
    /// <paramref name="d2" /> is zero. </exception>
    /// <exception cref="T:System.OverflowException">The return value (that is, the quotient) is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Decimal Divide(Decimal d1, Decimal d2)
    {
      Decimal.FCallDivide(ref d1, ref d2);
      return d1;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallDivide(ref Decimal d1, ref Decimal d2);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallDivideOverflowed(ref Decimal d1, ref Decimal d2, ref bool overflowed);

    /// <summary>返回一个值，该值指示此实例和指定的 <see cref="T:System.Object" /> 是否表示相同的类型和值。</summary>
    /// <returns>如果 <paramref name="value" /> 是一个 <see cref="T:System.Decimal" /> 且与此实例相等，则为 true；否则为 false。</returns>
    /// <param name="value">与该实例进行比较的对象。</param>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override bool Equals(object value)
    {
      if (!(value is Decimal))
        return false;
      Decimal d2 = (Decimal) value;
      return Decimal.FCallCompare(ref this, ref d2) == 0;
    }

    /// <summary>返回一个值，该值指示此实例和指定的 <see cref="T:System.Decimal" /> 对象是否表示相同的值。</summary>
    /// <returns>如果 true 与此实例相等，则为 <paramref name="value" />；否则为 false。</returns>
    /// <param name="value">要与此实例进行比较的对象。</param>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public bool Equals(Decimal value)
    {
      return Decimal.FCallCompare(ref this, ref value) == 0;
    }

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public override int GetHashCode();

    /// <summary>返回一个值，该值指示 <see cref="T:System.Decimal" /> 的两个指定实例是否表示同一个值。</summary>
    /// <returns>如果 true 和 <paramref name="d1" /> 相等，则为 <paramref name="d2" />；否则为 false。</returns>
    /// <param name="d1">要比较的第一个值。</param>
    /// <param name="d2">要比较的第二个值。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static bool Equals(Decimal d1, Decimal d2)
    {
      return Decimal.FCallCompare(ref d1, ref d2) == 0;
    }

    /// <summary>将指定的 <see cref="T:System.Decimal" /> 数字向负无穷方向舍入为最接近的整数。</summary>
    /// <returns>如果 <paramref name="d" /> 有小数部分，则为负无穷方向上小于 <paramref name="d" /> 的下一个整 <see cref="T:System.Decimal" /> 数字。- 或 - 如果 <paramref name="d" /> 没有小数部分，则 <paramref name="d" /> 原样返回。请注意，该方法将返回 <see cref="T:System.Decimal" /> 类型的整数值。</returns>
    /// <param name="d">要舍入的值。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Decimal Floor(Decimal d)
    {
      Decimal.FCallFloor(ref d);
      return d;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallFloor(ref Decimal d);

    /// <summary>将此实例的数值转换为其等效的字符串表示形式。</summary>
    /// <returns>表示此实例的值的字符串。</returns>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return Number.FormatDecimal(this, (string) null, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>使用指定的格式，将此实例的数值转换为它的等效字符串表示形式。</summary>
    /// <returns>此实例的值的字符串表示形式，由 <paramref name="format" /> 指定。</returns>
    /// <param name="format">标准或自定义的数值格式字符串（请参见“备注”）。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> is invalid. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string ToString(string format)
    {
      return Number.FormatDecimal(this, format, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>使用指定的区域性特定格式信息，将此实例的数值转换为它的等效字符串表示形式。</summary>
    /// <returns>此实例的值的字符串表示形式，由 <paramref name="provider" /> 指定。</returns>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string ToString(IFormatProvider provider)
    {
      return Number.FormatDecimal(this, (string) null, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>使用指定的格式和区域性特定格式信息，将此实例的数值转换为它的等效字符串表示形式。</summary>
    /// <returns>此实例的值的字符串表示形式，由 <paramref name="format" /> 和 <paramref name="provider" /> 指定。</returns>
    /// <param name="format">数值格式字符串（请参见“备注”）。</param>
    /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> is invalid. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public string ToString(string format, IFormatProvider provider)
    {
      return Number.FormatDecimal(this, format, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>将数字的字符串表示形式转换为它的等效 <see cref="T:System.Decimal" /> 表示形式。</summary>
    /// <returns>
    /// <paramref name="s" /> 中包含的数字的等效值。</returns>
    /// <param name="s">要转换的数字的字符串表示形式。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> is null. </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="s" /> is not in the correct format. </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="s" /> represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal Parse(string s)
    {
      return Number.ParseDecimal(s, NumberStyles.Number, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>将指定样式的数字的字符串表示形式转换为它的等效 <see cref="T:System.Decimal" />。</summary>
    /// <returns>
    /// <see cref="T:System.Decimal" /> 数，它与 <paramref name="style" /> 所指定的 <paramref name="s" /> 中包含的数字等效。</returns>
    /// <param name="s">要转换的数字的字符串表示形式。</param>
    /// <param name="style">
    /// <see cref="T:System.Globalization.NumberStyles" /> 值的按位组合，指示可出现在 <paramref name="s" /> 中的样式元素。要指定的一个典型值为 <see cref="F:System.Globalization.NumberStyles.Number" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> is null.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="s" /> is not in the correct format. </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="s" /> represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" /></exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal Parse(string s, NumberStyles style)
    {
      NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
      return Number.ParseDecimal(s, style, NumberFormatInfo.CurrentInfo);
    }

    /// <summary>使用指定的区域性特定格式信息将数字的字符串表示形式转换为其 <see cref="T:System.Decimal" /> 等效项。</summary>
    /// <returns>
    /// <see cref="T:System.Decimal" /> 数，它与由 <paramref name="provider" /> 所指定的 <paramref name="s" /> 中包含的数字等效。</returns>
    /// <param name="s">要转换的数字的字符串表示形式。</param>
    /// <param name="provider">一个 <see cref="T:System.IFormatProvider" />，它提供有关 <paramref name="s" /> 的区域性特定分析信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> is null. </exception>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="s" /> is not of the correct format </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="s" /> represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" /></exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal Parse(string s, IFormatProvider provider)
    {
      return Number.ParseDecimal(s, NumberStyles.Number, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>使用指定样式和区域性特定格式将数字的字符串表示形式转换为其 <see cref="T:System.Decimal" /> 等效项。</summary>
    /// <returns>
    /// <see cref="T:System.Decimal" /> 数，它与 <paramref name="style" /> 和 <paramref name="provider" /> 所指定的 <paramref name="s" /> 中包含的数字等效。</returns>
    /// <param name="s">要转换的数字的字符串表示形式。</param>
    /// <param name="style">
    /// <see cref="T:System.Globalization.NumberStyles" /> 值的按位组合，指示可出现在 <paramref name="s" /> 中的样式元素。要指定的一个典型值为 <see cref="F:System.Globalization.NumberStyles.Number" />。</param>
    /// <param name="provider">一个 <see cref="T:System.IFormatProvider" /> 对象，用于提供有关 <paramref name="s" /> 格式的区域性特定信息。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="s" /> is not in the correct format. </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="s" /> represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="s" /> is null.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal Parse(string s, NumberStyles style, IFormatProvider provider)
    {
      NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
      return Number.ParseDecimal(s, style, NumberFormatInfo.GetInstance(provider));
    }

    /// <summary>将数字的字符串表示形式转换为它的等效 <see cref="T:System.Decimal" /> 表示形式。一个指示转换是否成功的返回值。</summary>
    /// <returns>如果 true 成功转换，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">要转换的数字的字符串表示形式。</param>
    /// <param name="result">当此方法返回时，如果转换成功，则包含与 <paramref name="s" /> 中所包含数值等效的 <see cref="T:System.Decimal" /> 数；如果转换失败，则为零。如果 <paramref name="s" /> 参数为 null 或 <see cref="F:System.String.Empty" />、不是有效格式的数字，或者表示的数字小于 <see cref="F:System.Decimal.MinValue" /> 或大于 <see cref="F:System.Decimal.MaxValue" />，则转换失败。此参数未经初始化即进行传递；最初在 <paramref name="result" /> 中提供的任何值都会被覆盖。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool TryParse(string s, out Decimal result)
    {
      return Number.TryParseDecimal(s, NumberStyles.Number, NumberFormatInfo.CurrentInfo, out result);
    }

    /// <summary>使用指定样式和区域性特定格式将数字的字符串表示形式转换为其 <see cref="T:System.Decimal" /> 等效项。一个指示转换是否成功的返回值。</summary>
    /// <returns>如果 true 成功转换，则为 <paramref name="s" />；否则为 false。</returns>
    /// <param name="s">要转换的数字的字符串表示形式。</param>
    /// <param name="style">枚举值的一个按位组合，指示 <paramref name="s" /> 所允许的格式。要指定的一个典型值为 <see cref="F:System.Globalization.NumberStyles.Number" />。</param>
    /// <param name="provider">一个对象，它提供有关 <paramref name="s" /> 的区域性特定分析信息。</param>
    /// <param name="result">当此方法返回时，如果转换成功，则包含与 <paramref name="s" /> 中所包含数值等效的 <see cref="T:System.Decimal" /> 数；如果转换失败，则为零。如果 <paramref name="s" /> 参数为 null 或 <see cref="F:System.String.Empty" />、格式不符合 <paramref name="style" />，或者表示的数字小于 <see cref="F:System.Decimal.MinValue" /> 或大于 <see cref="F:System.Decimal.MaxValue" />，则转换失败。此参数未经初始化即进行传递；最初在 <paramref name="result" /> 中提供的任何值都会被覆盖。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out Decimal result)
    {
      NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
      return Number.TryParseDecimal(s, style, NumberFormatInfo.GetInstance(provider), out result);
    }

    /// <summary>将 <see cref="T:System.Decimal" /> 的指定实例的值转换为其等效的二进制表示形式。</summary>
    /// <returns>包含 <paramref name="d" /> 二进制表示形式、由四个元素组成的 32 位有符号整数数组。</returns>
    /// <param name="d">要转换的值。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int[] GetBits(Decimal d)
    {
      return new int[4]
      {
        d.lo,
        d.mid,
        d.hi,
        d.flags
      };
    }

    internal static void GetBytes(Decimal d, byte[] buffer)
    {
      buffer[0] = (byte) d.lo;
      buffer[1] = (byte) (d.lo >> 8);
      buffer[2] = (byte) (d.lo >> 16);
      buffer[3] = (byte) (d.lo >> 24);
      buffer[4] = (byte) d.mid;
      buffer[5] = (byte) (d.mid >> 8);
      buffer[6] = (byte) (d.mid >> 16);
      buffer[7] = (byte) (d.mid >> 24);
      buffer[8] = (byte) d.hi;
      buffer[9] = (byte) (d.hi >> 8);
      buffer[10] = (byte) (d.hi >> 16);
      buffer[11] = (byte) (d.hi >> 24);
      buffer[12] = (byte) d.flags;
      buffer[13] = (byte) (d.flags >> 8);
      buffer[14] = (byte) (d.flags >> 16);
      buffer[15] = (byte) (d.flags >> 24);
    }

    internal static Decimal ToDecimal(byte[] buffer)
    {
      int lo = (int) buffer[0] | (int) buffer[1] << 8 | (int) buffer[2] << 16 | (int) buffer[3] << 24;
      int num1 = (int) buffer[4] | (int) buffer[5] << 8 | (int) buffer[6] << 16 | (int) buffer[7] << 24;
      int num2 = (int) buffer[8] | (int) buffer[9] << 8 | (int) buffer[10] << 16 | (int) buffer[11] << 24;
      int num3 = (int) buffer[12] | (int) buffer[13] << 8 | (int) buffer[14] << 16 | (int) buffer[15] << 24;
      int mid = num1;
      int hi = num2;
      int flags = num3;
      return new Decimal(lo, mid, hi, flags);
    }

    private static void InternalAddUInt32RawUnchecked(ref Decimal value, uint i)
    {
      uint num1 = (uint) value.lo;
      uint num2 = num1 + i;
      value.lo = (int) num2;
      if (num2 >= num1 && num2 >= i)
        return;
      uint num3 = (uint) value.mid;
      uint num4 = num3 + 1U;
      value.mid = (int) num4;
      if (num4 >= num3 && num4 >= 1U)
        return;
      ++value.hi;
    }

    private static uint InternalDivRemUInt32(ref Decimal value, uint divisor)
    {
      uint num1 = 0;
      if (value.hi != 0)
      {
        ulong num2 = (ulong) (uint) value.hi;
        value.hi = (int) (uint) (num2 / (ulong) divisor);
        num1 = (uint) (num2 % (ulong) divisor);
      }
      if (value.mid != 0 || (int) num1 != 0)
      {
        ulong num2 = (ulong) num1 << 32 | (ulong) (uint) value.mid;
        value.mid = (int) (uint) (num2 / (ulong) divisor);
        num1 = (uint) (num2 % (ulong) divisor);
      }
      if (value.lo != 0 || (int) num1 != 0)
      {
        ulong num2 = (ulong) num1 << 32 | (ulong) (uint) value.lo;
        value.lo = (int) (uint) (num2 / (ulong) divisor);
        num1 = (uint) (num2 % (ulong) divisor);
      }
      return num1;
    }

    private static void InternalRoundFromZero(ref Decimal d, int decimalCount)
    {
      int num1 = ((d.flags & 16711680) >> 16) - decimalCount;
      if (num1 <= 0)
        return;
      uint divisor;
      uint num2;
      do
      {
        int index = num1 > 9 ? 9 : num1;
        divisor = Decimal.Powers10[index];
        num2 = Decimal.InternalDivRemUInt32(ref d, divisor);
        num1 -= index;
      }
      while (num1 > 0);
      if (num2 >= divisor >> 1)
        Decimal.InternalAddUInt32RawUnchecked(ref d, 1U);
      d.flags = decimalCount << 16 & 16711680 | d.flags & int.MinValue;
    }

    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal static Decimal Max(Decimal d1, Decimal d2)
    {
      if (Decimal.FCallCompare(ref d1, ref d2) < 0)
        return d2;
      return d1;
    }

    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal static Decimal Min(Decimal d1, Decimal d2)
    {
      if (Decimal.FCallCompare(ref d1, ref d2) >= 0)
        return d2;
      return d1;
    }

    /// <summary>计算两个 <see cref="T:System.Decimal" /> 值相除后的余数。</summary>
    /// <returns>将 <paramref name="d1" /> 除以 <paramref name="d2" /> 后的余数。</returns>
    /// <param name="d1">被除数。</param>
    /// <param name="d2">除数。</param>
    /// <exception cref="T:System.DivideByZeroException">
    /// <paramref name="d2" /> is zero. </exception>
    /// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal Remainder(Decimal d1, Decimal d2)
    {
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Decimal& local1 = @d2;
      // ISSUE: explicit reference operation
      int num1 = (^local1).flags & int.MaxValue | d1.flags & int.MinValue;
      // ISSUE: explicit reference operation
      (^local1).flags = num1;
      if (Decimal.Abs(d1) < Decimal.Abs(d2))
        return d1;
      d1 -= d2;
      if (d1 == Decimal.Zero)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Decimal& local2 = @d1;
        // ISSUE: explicit reference operation
        int num2 = (^local2).flags & int.MaxValue | d2.flags & int.MinValue;
        // ISSUE: explicit reference operation
        (^local2).flags = num2;
      }
      Decimal num3 = Decimal.Truncate(d1 / d2) * d2;
      Decimal num4 = d1 - num3;
      if ((d1.flags & int.MinValue) != (num4.flags & int.MinValue))
      {
        if (new Decimal(1, 0, 0, true, (byte) 27) <= num4 && num4 <= new Decimal(1, 0, 0, false, (byte) 27))
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Decimal& local2 = @num4;
          // ISSUE: explicit reference operation
          int num2 = (^local2).flags & int.MaxValue | d1.flags & int.MinValue;
          // ISSUE: explicit reference operation
          (^local2).flags = num2;
        }
        else
          num4 += d2;
      }
      return num4;
    }

    /// <summary>两个指定的 <see cref="T:System.Decimal" /> 值相乘。</summary>
    /// <returns>
    /// <paramref name="d1" /> 与 <paramref name="d2" /> 相乘的结果。</returns>
    /// <param name="d1">被乘数。</param>
    /// <param name="d2">乘数。</param>
    /// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Decimal Multiply(Decimal d1, Decimal d2)
    {
      Decimal.FCallMultiply(ref d1, ref d2);
      return d1;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallMultiply(ref Decimal d1, ref Decimal d2);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallMultiplyOverflowed(ref Decimal d1, ref Decimal d2, ref bool overflowed);

    /// <summary>返回指定的 <see cref="T:System.Decimal" /> 值乘以 -1 的结果。</summary>
    /// <returns>具有 <paramref name="d" /> 的值，但符号相反的十进制数。- 或 - 如果 <paramref name="d" /> 为零，则为零。</returns>
    /// <param name="d">要求反的值。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal Negate(Decimal d)
    {
      return new Decimal(d.lo, d.mid, d.hi, d.flags ^ int.MinValue);
    }

    /// <summary>将小数值舍入到最接近的整数。</summary>
    /// <returns>最接近 <paramref name="d" /> 参数的整数。如果 <paramref name="d" /> 正好处于两个整数中间，其中一个整数为偶数，另一个整数为奇数，则返回偶数。</returns>
    /// <param name="d">要舍入的小数。</param>
    /// <exception cref="T:System.OverflowException">The result is outside the range of a <see cref="T:System.Decimal" /> object.</exception>
    /// <filterpriority>1</filterpriority>
    public static Decimal Round(Decimal d)
    {
      return Decimal.Round(d, 0);
    }

    /// <summary>将 <see cref="T:System.Decimal" /> 值舍入到指定的小数位数。</summary>
    /// <returns>舍入到 <paramref name="decimals" /> 的小数位数等于 <paramref name="d" /> 的小数位数。</returns>
    /// <param name="d">要舍入的小数。</param>
    /// <param name="decimals">指定数字要舍入到的小数位数的值，范围从 0 到 28。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="decimals" /> is not a value from 0 to 28. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Decimal Round(Decimal d, int decimals)
    {
      Decimal.FCallRound(ref d, decimals);
      return d;
    }

    /// <summary>将小数值舍入到最接近的整数。一个参数，指定当一个值正好处于另两个数中间时如何舍入这个值。</summary>
    /// <returns>最接近 <paramref name="d" /> 参数的整数。如果 <paramref name="d" /> 位于两个数字的中间，其中一个为偶数，另一个为奇数，则 <paramref name="mode" /> 参数确定返回这两个数字中的哪一个。</returns>
    /// <param name="d">要舍入的小数。</param>
    /// <param name="mode">一个值，指定当 <paramref name="d" /> 正好处于另两个数字中间时如何舍入。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="mode" /> is not a <see cref="T:System.MidpointRounding" /> value.</exception>
    /// <exception cref="T:System.OverflowException">The result is outside the range of a <see cref="T:System.Decimal" /> object.</exception>
    /// <filterpriority>1</filterpriority>
    public static Decimal Round(Decimal d, MidpointRounding mode)
    {
      return Decimal.Round(d, 0, mode);
    }

    /// <summary>将小数值舍入到指定精度。一个参数，指定当一个值正好处于另两个数中间时如何舍入这个值。</summary>
    /// <returns>最接近 <paramref name="d" /> 参数的数字，其精度等于 <paramref name="decimals" /> 参数。如果 <paramref name="d" /> 位于两个数字的中间，其中一个为偶数，另一个为奇数，则 <paramref name="mode" /> 参数确定返回这两个数字中的哪一个。如果 <paramref name="d" /> 的精度小于 <paramref name="decimals" />，则原样返回 <paramref name="d" />。</returns>
    /// <param name="d">要舍入的小数。</param>
    /// <param name="decimals">返回值中的有效小数位数（精度）。</param>
    /// <param name="mode">一个值，指定当 <paramref name="d" /> 正好处于另两个数字中间时如何舍入。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="decimals" /> is less than 0 or greater than 28. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="mode" /> is not a <see cref="T:System.MidpointRounding" /> value.</exception>
    /// <exception cref="T:System.OverflowException">The result is outside the range of a <see cref="T:System.Decimal" /> object.</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public static Decimal Round(Decimal d, int decimals, MidpointRounding mode)
    {
      if (decimals < 0 || decimals > 28)
        throw new ArgumentOutOfRangeException("decimals", Environment.GetResourceString("ArgumentOutOfRange_DecimalRound"));
      if (mode < MidpointRounding.ToEven || mode > MidpointRounding.AwayFromZero)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidEnumValue", (object) mode, (object) "MidpointRounding"), "mode");
      if (mode == MidpointRounding.ToEven)
        Decimal.FCallRound(ref d, decimals);
      else
        Decimal.InternalRoundFromZero(ref d, decimals);
      return d;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallRound(ref Decimal d, int decimals);

    /// <summary>从一个 <see cref="T:System.Decimal" /> 值中减去指定的另一个值。</summary>
    /// <returns>
    /// <paramref name="d2" /> 减 <paramref name="d1" /> 所得的结果。</returns>
    /// <param name="d1">被减数。</param>
    /// <param name="d2">减数。</param>
    /// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Decimal Subtract(Decimal d1, Decimal d2)
    {
      Decimal.FCallAddSub(ref d1, ref d2, (byte) 128);
      return d1;
    }

    /// <summary>将指定的 <see cref="T:System.Decimal" /> 的值转换为等效的 8 位无符号整数。</summary>
    /// <returns>等效于 <paramref name="value" /> 的 8 位无符号整数。</returns>
    /// <param name="value">要转换的十进制数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. </exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static byte ToByte(Decimal value)
    {
      uint uint32;
      try
      {
        uint32 = Decimal.ToUInt32(value);
      }
      catch (OverflowException ex)
      {
        throw new OverflowException(Environment.GetResourceString("Overflow_Byte"), (Exception) ex);
      }
      if (uint32 < 0U || uint32 > (uint) byte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
      return (byte) uint32;
    }

    /// <summary>将指定的 <see cref="T:System.Decimal" /> 值转换为等效的 8 位有符号整数。</summary>
    /// <returns>等效于 <paramref name="value" /> 的 8 位有符号整数。</returns>
    /// <param name="value">要转换的十进制数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />. </exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte ToSByte(Decimal value)
    {
      int int32;
      try
      {
        int32 = Decimal.ToInt32(value);
      }
      catch (OverflowException ex)
      {
        throw new OverflowException(Environment.GetResourceString("Overflow_SByte"), (Exception) ex);
      }
      if (int32 < (int) sbyte.MinValue || int32 > (int) sbyte.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
      return (sbyte) int32;
    }

    /// <summary>将指定的 <see cref="T:System.Decimal" /> 值转换为等效的 16 位有符号整数。</summary>
    /// <returns>等效于 <paramref name="value" /> 的 16 位有符号整数。</returns>
    /// <param name="value">要转换的十进制数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />. </exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static short ToInt16(Decimal value)
    {
      int int32;
      try
      {
        int32 = Decimal.ToInt32(value);
      }
      catch (OverflowException ex)
      {
        throw new OverflowException(Environment.GetResourceString("Overflow_Int16"), (Exception) ex);
      }
      if (int32 < (int) short.MinValue || int32 > (int) short.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
      return (short) int32;
    }

    [SecuritySafeCritical]
    internal static Currency ToCurrency(Decimal d)
    {
      Currency result = new Currency();
      Decimal.FCallToCurrency(ref result, d);
      return result;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallToCurrency(ref Currency result, Decimal d);

    /// <summary>将指定的 <see cref="T:System.Decimal" /> 的值转换为等效的双精度浮点数。</summary>
    /// <returns>与 <paramref name="d" /> 等效的双精度浮点数。</returns>
    /// <param name="d">要转换的十进制数。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double ToDouble(Decimal d);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int FCallToInt32(Decimal d);

    /// <summary>将指定的 <see cref="T:System.Decimal" /> 值转换为等效的 32 位有符号整数。</summary>
    /// <returns>与 <paramref name="d" /> 的值等效的 32 位有符号整数。</returns>
    /// <param name="d">要转换的十进制数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="d" /> is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static int ToInt32(Decimal d)
    {
      if ((d.flags & 16711680) != 0)
        Decimal.FCallTruncate(ref d);
      if (d.hi == 0 && d.mid == 0)
      {
        int num1 = d.lo;
        if (d.flags >= 0)
        {
          if (num1 >= 0)
            return num1;
        }
        else
        {
          int num2 = -num1;
          if (num2 <= 0)
            return num2;
        }
      }
      throw new OverflowException(Environment.GetResourceString("Overflow_Int32"));
    }

    /// <summary>将指定的 <see cref="T:System.Decimal" /> 值转换为等效的 64 位有符号整数。</summary>
    /// <returns>与 <paramref name="d" /> 的值等效的 64 位有符号整数。</returns>
    /// <param name="d">要转换的十进制数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="d" /> is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static long ToInt64(Decimal d)
    {
      if ((d.flags & 16711680) != 0)
        Decimal.FCallTruncate(ref d);
      if (d.hi == 0)
      {
        long num1 = (long) d.lo & (long) uint.MaxValue | (long) d.mid << 32;
        if (d.flags >= 0)
        {
          if (num1 >= 0L)
            return num1;
        }
        else
        {
          long num2 = -num1;
          if (num2 <= 0L)
            return num2;
        }
      }
      throw new OverflowException(Environment.GetResourceString("Overflow_Int64"));
    }

    /// <summary>将指定的 <see cref="T:System.Decimal" /> 的值转换为等效的 16 位无符号整数。</summary>
    /// <returns>与 <paramref name="value" /> 的值等效的 16 位无符号整数。</returns>
    /// <param name="value">要转换的十进制数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> is greater than <see cref="F:System.UInt16.MaxValue" /> or less than <see cref="F:System.UInt16.MinValue" />. </exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort ToUInt16(Decimal value)
    {
      uint uint32;
      try
      {
        uint32 = Decimal.ToUInt32(value);
      }
      catch (OverflowException ex)
      {
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"), (Exception) ex);
      }
      if (uint32 < 0U || uint32 > (uint) ushort.MaxValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
      return (ushort) uint32;
    }

    /// <summary>将指定的 <see cref="T:System.Decimal" /> 的值转换为等效的 32 位无符号整数。</summary>
    /// <returns>与 <paramref name="d" /> 的值等效的 32 位无符号整数。</returns>
    /// <param name="d">要转换的十进制数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="d" /> is negative or greater than <see cref="F:System.UInt32.MaxValue" />. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint ToUInt32(Decimal d)
    {
      if ((d.flags & 16711680) != 0)
        Decimal.FCallTruncate(ref d);
      if (d.hi == 0 && d.mid == 0)
      {
        uint num = (uint) d.lo;
        if (d.flags >= 0 || (int) num == 0)
          return num;
      }
      throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
    }

    /// <summary>将指定的 <see cref="T:System.Decimal" /> 的值转换为等效的 64 位无符号整数。</summary>
    /// <returns>与 <paramref name="d" /> 的值等效的 64 位无符号整数。</returns>
    /// <param name="d">要转换的十进制数。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="d" /> is negative or greater than <see cref="F:System.UInt64.MaxValue" />. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ulong ToUInt64(Decimal d)
    {
      if ((d.flags & 16711680) != 0)
        Decimal.FCallTruncate(ref d);
      if (d.hi == 0)
      {
        ulong num = (ulong) (uint) d.lo | (ulong) (uint) d.mid << 32;
        if (d.flags >= 0 || (long) num == 0L)
          return num;
      }
      throw new OverflowException(Environment.GetResourceString("Overflow_UInt64"));
    }

    /// <summary>将指定的 <see cref="T:System.Decimal" /> 的值转换为等效的单精度浮点数。</summary>
    /// <returns>等效于 <paramref name="d" /> 的值的单精度浮点数字。</returns>
    /// <param name="d">要转换的十进制数。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern float ToSingle(Decimal d);

    /// <summary>返回指定的 <see cref="T:System.Decimal" /> 的整数位，所有小数位均被放弃。</summary>
    /// <returns>
    /// <paramref name="d" /> 向零舍入为最接近的整数后的结果。</returns>
    /// <param name="d">要截断的十进制数。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Decimal Truncate(Decimal d)
    {
      Decimal.FCallTruncate(ref d);
      return d;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void FCallTruncate(ref Decimal d);

    /// <summary>返回值类型 <see cref="T:System.TypeCode" /> 的 <see cref="T:System.Decimal" />。</summary>
    /// <returns>枚举常数 <see cref="F:System.TypeCode.Decimal" />。</returns>
    /// <filterpriority>2</filterpriority>
    public TypeCode GetTypeCode()
    {
      return TypeCode.Decimal;
    }

    [__DynamicallyInvokable]
    bool IConvertible.ToBoolean(IFormatProvider provider)
    {
      return Convert.ToBoolean(this);
    }

    [__DynamicallyInvokable]
    char IConvertible.ToChar(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "Decimal", (object) "Char"));
    }

    [__DynamicallyInvokable]
    sbyte IConvertible.ToSByte(IFormatProvider provider)
    {
      return Convert.ToSByte(this);
    }

    [__DynamicallyInvokable]
    byte IConvertible.ToByte(IFormatProvider provider)
    {
      return Convert.ToByte(this);
    }

    [__DynamicallyInvokable]
    short IConvertible.ToInt16(IFormatProvider provider)
    {
      return Convert.ToInt16(this);
    }

    [__DynamicallyInvokable]
    ushort IConvertible.ToUInt16(IFormatProvider provider)
    {
      return Convert.ToUInt16(this);
    }

    [__DynamicallyInvokable]
    int IConvertible.ToInt32(IFormatProvider provider)
    {
      return Convert.ToInt32(this);
    }

    [__DynamicallyInvokable]
    uint IConvertible.ToUInt32(IFormatProvider provider)
    {
      return Convert.ToUInt32(this);
    }

    [__DynamicallyInvokable]
    long IConvertible.ToInt64(IFormatProvider provider)
    {
      return Convert.ToInt64(this);
    }

    [__DynamicallyInvokable]
    ulong IConvertible.ToUInt64(IFormatProvider provider)
    {
      return Convert.ToUInt64(this);
    }

    [__DynamicallyInvokable]
    float IConvertible.ToSingle(IFormatProvider provider)
    {
      return Convert.ToSingle(this);
    }

    [__DynamicallyInvokable]
    double IConvertible.ToDouble(IFormatProvider provider)
    {
      return Convert.ToDouble(this);
    }

    [__DynamicallyInvokable]
    Decimal IConvertible.ToDecimal(IFormatProvider provider)
    {
      return this;
    }

    [__DynamicallyInvokable]
    DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "Decimal", (object) "DateTime"));
    }

    [__DynamicallyInvokable]
    object IConvertible.ToType(Type type, IFormatProvider provider)
    {
      return Convert.DefaultToType((IConvertible) this, type, provider);
    }
  }
}
