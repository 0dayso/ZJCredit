// Decompiled with JetBrains decompiler
// Type: System.Math
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
  /// <summary>为三角函数、对数函数和其他通用数学函数提供常数和静态方法。若要浏览此类型的.NET Framework 源代码，请参阅 Reference Source。</summary>
  /// <filterpriority>1</filterpriority>
  [__DynamicallyInvokable]
  public static class Math
  {
    private static double doubleRoundLimit = 1E+16;
    private static double[] roundPower10Double = new double[16]{ 1.0, 10.0, 100.0, 1000.0, 10000.0, 100000.0, 1000000.0, 10000000.0, 100000000.0, 1000000000.0, 10000000000.0, 100000000000.0, 1000000000000.0, 10000000000000.0, 100000000000000.0, 1E+15 };
    private const int maxRoundingDigits = 15;
    /// <summary>表示圆的周长与其直径的比值，由常数 π 指定。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public const double PI = 3.14159265358979;
    /// <summary>表示自然对数的底，它由常数 e 指定。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public const double E = 2.71828182845905;

    /// <summary>返回余弦值为指定数字的角度。</summary>
    /// <returns>角度 θ，以弧度为单位，满足 0 ≤θ≤π- 或 - 如果 <paramref name="d" /> &lt; -1 或 <paramref name="d" /> &gt; 1 或 <paramref name="d" /> 等于 <see cref="F:System.Double.NaN" />，则为 <see cref="F:System.Double.NaN" />。</returns>
    /// <param name="d">一个表示余弦值的数字，其中 <paramref name="d" /> 必须大于或等于 -1 但小于或等于 1。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Acos(double d);

    /// <summary>返回正弦值为指定数字的角度。</summary>
    /// <returns>角度 θ，以弧度为单位，满足 π/2 ≤θ≤π/2 - 或 - 如果 <paramref name="d" /> &lt; -1 或 <paramref name="d" /> &gt; 1 或 <paramref name="d" /> 等于 <see cref="F:System.Double.NaN" />，则为 <see cref="F:System.Double.NaN" />。</returns>
    /// <param name="d">一个表示正弦值的数字，其中 <paramref name="d" /> 必须大于或等于 -1 但小于或等于 1。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Asin(double d);

    /// <summary>返回正切值为指定数字的角度。</summary>
    /// <returns>角度 θ，以弧度为单位，满足 -π/2 ≤θ≤π/2。- 或 - 如果 <paramref name="d" /> 等于 <see cref="F:System.Double.NaN" />，则为 <see cref="F:System.Double.NaN" />；如果 <paramref name="d" /> 等于 <see cref="F:System.Double.NegativeInfinity" />，则为舍入为双精度值 (-1.5707963267949) 的 -π/2；或者如果 <paramref name="d" /> 等于 <see cref="F:System.Double.PositiveInfinity" />，则为舍入为双精度值 (1.5707963267949) 的 π/2。</returns>
    /// <param name="d">表示正切值的数字。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Atan(double d);

    /// <summary>返回正切值为两个指定数字的商的角度。</summary>
    /// <returns>角度 θ，以弧度为单位，满足 -π≤θ≤π，且 tan(θ) = <paramref name="y" /> / <paramref name="x" />，其中 (<paramref name="x" />, <paramref name="y" />) 是笛卡尔平面中的点。请看下面：对于 (<paramref name="x" />, ，<paramref name="y" />) 在象限 1、 0 &lt; θ &lt; π/2。对于 (<paramref name="x" />, ，<paramref name="y" />) 在象限中 2， π/2 &lt; θ≤π。对于 (<paramref name="x" />, ，<paramref name="y" />) 在象限中 3，-π &lt; θ &lt;-π/2。对于 (<paramref name="x" />, ，<paramref name="y" />) 在象限中 4 中，-π/2 &lt; θ &lt; 0。如果点在象限的边界上，则返回值如下：如果 y 为 0 并且 x 不为负值，则 θ = 0。如果 y 为 0 并且 x 为负值，则 θ = π。如果 y 为正值并且 x 为 0，则 θ = π/2。如果 y 为负值并且 x 为 0，则 θ = -π/2。如果 y 为 0 并且 x 为 0，则 θ = 0。如果 <paramref name="x" /> 或 <paramref name="y" /> 为 <see cref="F:System.Double.NaN" />，或者如果 <paramref name="x" /> 和 <paramref name="y" /> 为 <see cref="F:System.Double.PositiveInfinity" /> 或 <see cref="F:System.Double.NegativeInfinity" />，则该方法返回 <see cref="F:System.Double.NaN" />。</returns>
    /// <param name="y">点的 y 坐标。</param>
    /// <param name="x">点的 x 坐标。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Atan2(double y, double x);

    /// <summary>返回大于或等于指定的十进制数的最小整数值。</summary>
    /// <returns>大于或等于 <paramref name="d" /> 的最小整数值。请注意，此方法返回 <see cref="T:System.Decimal" />，而不是整数类型。</returns>
    /// <param name="d">十进制数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal Ceiling(Decimal d)
    {
      return Decimal.Ceiling(d);
    }

    /// <summary>返回大于或等于指定的双精度浮点数的最小整数值。</summary>
    /// <returns>大于或等于 <paramref name="a" /> 的最小整数值。如果 <paramref name="a" /> 等于 <see cref="F:System.Double.NaN" />、<see cref="F:System.Double.NegativeInfinity" /> 或 <see cref="F:System.Double.PositiveInfinity" />，则返回该值。请注意，此方法返回 <see cref="T:System.Double" />，而不是整数类型。</returns>
    /// <param name="a">一个双精度浮点数。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Ceiling(double a);

    /// <summary>返回指定角度的余弦值。</summary>
    /// <returns>
    /// <paramref name="d" /> 的余弦值。如果 <paramref name="d" /> 等于 <see cref="F:System.Double.NaN" />、<see cref="F:System.Double.NegativeInfinity" /> 或 <see cref="F:System.Double.PositiveInfinity" />，此方法将返回 <see cref="F:System.Double.NaN" />。</returns>
    /// <param name="d">以弧度计量的角度。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Cos(double d);

    /// <summary>返回指定角度的双曲余弦值。</summary>
    /// <returns>
    /// <paramref name="value" /> 的双曲余弦值。如果 <paramref name="value" /> 等于 <see cref="F:System.Double.NegativeInfinity" /> 或 <see cref="F:System.Double.PositiveInfinity" />，则返回 <see cref="F:System.Double.PositiveInfinity" />。如果 <paramref name="value" /> 等于 <see cref="F:System.Double.NaN" />，则返回 <see cref="F:System.Double.NaN" />。</returns>
    /// <param name="value">以弧度计量的角度。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Cosh(double value);

    /// <summary>返回小于或等于指定小数的最大整数。</summary>
    /// <returns>小于或等于 <paramref name="d" /> 的最大整数。请注意，该方法将返回 <see cref="T:System.Math" /> 类型的整数值。</returns>
    /// <param name="d">十进制数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal Floor(Decimal d)
    {
      return Decimal.Floor(d);
    }

    /// <summary>返回小于或等于指定双精度浮点数的最大整数。</summary>
    /// <returns>小于或等于 <paramref name="d" /> 的最大整数。如果 <paramref name="d" /> 等于 <see cref="F:System.Double.NaN" />、<see cref="F:System.Double.NegativeInfinity" /> 或 <see cref="F:System.Double.PositiveInfinity" />，则返回该值。</returns>
    /// <param name="d">一个双精度浮点数。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Floor(double d);

    [SecuritySafeCritical]
    private static unsafe double InternalRound(double value, int digits, MidpointRounding mode)
    {
      if (Math.Abs(value) < Math.doubleRoundLimit)
      {
        double num1 = Math.roundPower10Double[digits];
        value *= num1;
        if (mode == MidpointRounding.AwayFromZero)
        {
          double num2 = Math.SplitFractionDouble(&value);
          if (Math.Abs(num2) >= 0.5)
            value += (double) Math.Sign(num2);
        }
        else
          value = Math.Round(value);
        value /= num1;
      }
      return value;
    }

    [SecuritySafeCritical]
    private static unsafe double InternalTruncate(double d)
    {
      Math.SplitFractionDouble(&d);
      return d;
    }

    /// <summary>返回指定角度的正弦值。</summary>
    /// <returns>
    /// <paramref name="a" /> 的正弦值。如果 <paramref name="a" /> 等于 <see cref="F:System.Double.NaN" />、<see cref="F:System.Double.NegativeInfinity" /> 或 <see cref="F:System.Double.PositiveInfinity" />，此方法将返回 <see cref="F:System.Double.NaN" />。</returns>
    /// <param name="a">以弧度计量的角度。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Sin(double a);

    /// <summary>返回指定角度的正切值。</summary>
    /// <returns>
    /// <paramref name="a" /> 的正切值。如果 <paramref name="a" /> 等于 <see cref="F:System.Double.NaN" />、<see cref="F:System.Double.NegativeInfinity" /> 或 <see cref="F:System.Double.PositiveInfinity" />，此方法将返回 <see cref="F:System.Double.NaN" />。</returns>
    /// <param name="a">以弧度计量的角度。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Tan(double a);

    /// <summary>返回指定角度的双曲正弦值。</summary>
    /// <returns>
    /// <paramref name="value" /> 的双曲正弦值。如果 <paramref name="value" /> 等于 <see cref="F:System.Double.NegativeInfinity" />、<see cref="F:System.Double.PositiveInfinity" /> 或 <see cref="F:System.Double.NaN" />，则此方法返回等于 <paramref name="value" /> 的 <see cref="T:System.Double" />。</returns>
    /// <param name="value">以弧度计量的角度。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Sinh(double value);

    /// <summary>返回指定角度的双曲正切值。</summary>
    /// <returns>
    /// <paramref name="value" /> 的双曲正切值。如果 <paramref name="value" /> 等于 <see cref="F:System.Double.NegativeInfinity" />，则此方法返回 -1。如果值等于 <see cref="F:System.Double.PositiveInfinity" />，则此方法返回 1。如果 <paramref name="value" /> 等于 <see cref="F:System.Double.NaN" />，则此方法返回 <see cref="F:System.Double.NaN" />。</returns>
    /// <param name="value">以弧度计量的角度。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Tanh(double value);

    /// <summary>将双精度浮点值舍入为最接近的整数值。</summary>
    /// <returns>最接近 <paramref name="a" /> 的整数。如果 <paramref name="a" /> 的小数部分正好处于两个整数中间，其中一个整数为偶数，另一个整数为奇数，则返回偶数。请注意，此方法返回 <see cref="T:System.Double" />，而不是整数类型。</returns>
    /// <param name="a">要舍入的双精度浮点数。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Round(double a);

    /// <summary>将双精度浮点值按指定的小数位数舍入。</summary>
    /// <returns>最接近 <paramref name="value" /> 的 <paramref name="digits" /> 位小数的数字。</returns>
    /// <param name="value">要舍入的双精度浮点数。</param>
    /// <param name="digits">返回值中的小数数字。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="digits" /> 为小于 0 或大于 15。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static double Round(double value, int digits)
    {
      if (digits < 0 || digits > 15)
        throw new ArgumentOutOfRangeException("digits", Environment.GetResourceString("ArgumentOutOfRange_RoundingDigits"));
      return Math.InternalRound(value, digits, MidpointRounding.ToEven);
    }

    /// <summary>将双精度浮点值舍入为最接近的整数。一个参数，指定当一个值正好处于两个数中间时如何舍入这个值。</summary>
    /// <returns>最接近 <paramref name="value" /> 的整数。如果 <paramref name="value" /> 是两个整数的中值，这两个整数一个为偶数，另一个为奇数，则 <paramref name="mode" /> 确定返回两个整数中的哪一个。</returns>
    /// <param name="value">要舍入的双精度浮点数。</param>
    /// <param name="mode">在两个数字之间时如何舍入 <paramref name="value" /> 的规范。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="mode" /> 不是有效的 <see cref="T:System.MidpointRounding" /> 值。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static double Round(double value, MidpointRounding mode)
    {
      return Math.Round(value, 0, mode);
    }

    /// <summary>将双精度浮点值按指定的小数位数舍入。一个参数，指定当一个值正好处于两个数中间时如何舍入这个值。</summary>
    /// <returns>最接近 <paramref name="value" /> 的 <paramref name="digits" /> 位小数的数字。如果 <paramref name="value" /> 比 <paramref name="digits" /> 少部分数字，<paramref name="value" /> 原样返回。</returns>
    /// <param name="value">要舍入的双精度浮点数。</param>
    /// <param name="digits">返回值中的小数数字。</param>
    /// <param name="mode">在两个数字之间时如何舍入 <paramref name="value" /> 的规范。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="digits" /> 为小于 0 或大于 15。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="mode" /> 不是有效的 <see cref="T:System.MidpointRounding" /> 值。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static double Round(double value, int digits, MidpointRounding mode)
    {
      if (digits < 0 || digits > 15)
        throw new ArgumentOutOfRangeException("digits", Environment.GetResourceString("ArgumentOutOfRange_RoundingDigits"));
      if (mode < MidpointRounding.ToEven || mode > MidpointRounding.AwayFromZero)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidEnumValue", (object) mode, (object) "MidpointRounding"), "mode");
      return Math.InternalRound(value, digits, mode);
    }

    /// <summary>将小数值舍入到最接近的整数值。</summary>
    /// <returns>最接近参数 <paramref name="d" /> 的整数。如果 <paramref name="d" /> 的小数部分正好处于两个整数中间，其中一个整数为偶数，另一个整数为奇数，则返回偶数。请注意，此方法返回 <see cref="T:System.Decimal" />，而不是整数类型。</returns>
    /// <param name="d">要舍入的小数。</param>
    /// <exception cref="T:System.OverflowException">结果超出了 <see cref="T:System.Decimal" /> 的范围。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal Round(Decimal d)
    {
      return Decimal.Round(d, 0);
    }

    /// <summary>将小数值按指定的小数位数舍入。</summary>
    /// <returns>最接近 <paramref name="d" /> 的 <paramref name="decimals" /> 位小数的数字。</returns>
    /// <param name="d">要舍入的小数。</param>
    /// <param name="decimals">返回值中的小数位数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="decimals" /> 为小于 0 或大于 28。</exception>
    /// <exception cref="T:System.OverflowException">结果超出了 <see cref="T:System.Decimal" /> 的范围。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal Round(Decimal d, int decimals)
    {
      return Decimal.Round(d, decimals);
    }

    /// <summary>将小数值舍入到最接近的整数。一个参数，指定当一个值正好处于两个数中间时如何舍入这个值。</summary>
    /// <returns>最接近 <paramref name="d" /> 的整数。如果 <paramref name="d" /> 是两个数字的中值，这两个数字一个为偶数，另一个为奇数，则 <paramref name="mode" /> 确定返回两个数字中的哪一个。</returns>
    /// <param name="d">要舍入的小数。</param>
    /// <param name="mode">在两个数字之间时如何舍入 <paramref name="d" /> 的规范。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="mode" /> 不是有效的 <see cref="T:System.MidpointRounding" /> 值。</exception>
    /// <exception cref="T:System.OverflowException">结果超出了 <see cref="T:System.Decimal" /> 的范围。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal Round(Decimal d, MidpointRounding mode)
    {
      return Decimal.Round(d, 0, mode);
    }

    /// <summary>将小数值按指定的小数位数舍入。一个参数，指定当一个值正好处于两个数中间时如何舍入这个值。</summary>
    /// <returns>最接近 <paramref name="d" /> 的 <paramref name="decimals" /> 位小数的数字。如果 <paramref name="d" /> 比 <paramref name="decimals" /> 少部分数字，<paramref name="d" /> 原样返回。</returns>
    /// <param name="d">要舍入的小数。</param>
    /// <param name="decimals">返回值中的小数位数。</param>
    /// <param name="mode">在两个数字之间时如何舍入 <paramref name="d" /> 的规范。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="decimals" /> 为小于 0 或大于 28。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="mode" /> 不是有效的 <see cref="T:System.MidpointRounding" /> 值。</exception>
    /// <exception cref="T:System.OverflowException">结果超出了 <see cref="T:System.Decimal" /> 的范围。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal Round(Decimal d, int decimals, MidpointRounding mode)
    {
      return Decimal.Round(d, decimals, mode);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe double SplitFractionDouble(double* value);

    /// <summary>计算一个数字的整数部分。</summary>
    /// <returns>
    /// <paramref name="d" /> 的整数部分（即舍弃小数位后剩余的数）。</returns>
    /// <param name="d">要截断的数字。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal Truncate(Decimal d)
    {
      return Decimal.Truncate(d);
    }

    /// <summary>计算指定双精度浮点数的整数部分。</summary>
    /// <returns>
    /// <paramref name="d" /> 的整数部分（即舍弃小数位后剩余的数或下表所列出的值之一）。<paramref name="d" />返回值<see cref="F:System.Double.NaN" /><see cref="F:System.Double.NaN" /><see cref="F:System.Double.NegativeInfinity" /><see cref="F:System.Double.NegativeInfinity" /><see cref="F:System.Double.PositiveInfinity" /><see cref="F:System.Double.PositiveInfinity" /></returns>
    /// <param name="d">要截断的数字。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static double Truncate(double d)
    {
      return Math.InternalTruncate(d);
    }

    /// <summary>返回指定数字的平方根。</summary>
    /// <returns>下表中的值之一。<paramref name="d" /> 参数 返回值 零或正数 <paramref name="d" /> 的正平方根。负数 <see cref="F:System.Double.NaN" />等于 <see cref="F:System.Double.NaN" /><see cref="F:System.Double.NaN" />等于 <see cref="F:System.Double.PositiveInfinity" /><see cref="F:System.Double.PositiveInfinity" /></returns>
    /// <param name="d">将查找其平方根的数字。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Sqrt(double d);

    /// <summary>返回指定数字的自然对数（底为 e）。</summary>
    /// <returns>下表中的值之一。<paramref name="d" /> 参数返回值 正 自然对数的 <paramref name="d" />； 也就是说，ln <paramref name="d" />, ，或日志 e<paramref name="d" />零 <see cref="F:System.Double.NegativeInfinity" />负数 <see cref="F:System.Double.NaN" />等于 <see cref="F:System.Double.NaN" /><see cref="F:System.Double.NaN" />等于 <see cref="F:System.Double.PositiveInfinity" /><see cref="F:System.Double.PositiveInfinity" /></returns>
    /// <param name="d">要查找其对数的数字。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Log(double d);

    /// <summary>返回指定数字以 10 为底的对数。</summary>
    /// <returns>下表中的值之一。<paramref name="d" /> 参数 返回值 正 基准的 10 个日志的 <paramref name="d" />； 也就是说，记录 10<paramref name="d" />。零 <see cref="F:System.Double.NegativeInfinity" />负数 <see cref="F:System.Double.NaN" />等于 <see cref="F:System.Double.NaN" /><see cref="F:System.Double.NaN" />等于 <see cref="F:System.Double.PositiveInfinity" /><see cref="F:System.Double.PositiveInfinity" /></returns>
    /// <param name="d">要查找其对数的数字。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Log10(double d);

    /// <summary>返回 e 的指定次幂。</summary>
    /// <returns>数字 e 的 <paramref name="d" /> 次幂。如果 <paramref name="d" /> 等于 <see cref="F:System.Double.NaN" /> 或 <see cref="F:System.Double.PositiveInfinity" />，则返回该值。如果 <paramref name="d" /> 等于 <see cref="F:System.Double.NegativeInfinity" />，则返回 0。</returns>
    /// <param name="d">指定幂的数字。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Exp(double d);

    /// <summary>返回指定数字的指定次幂。</summary>
    /// <returns>数字 <paramref name="x" /> 的 <paramref name="y" /> 次幂。</returns>
    /// <param name="x">要乘幂的双精度浮点数。</param>
    /// <param name="y">指定幂的双精度浮点数。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Pow(double x, double y);

    /// <summary>返回一指定数字被另一指定数字相除的余数。</summary>
    /// <returns>一个数等于 <paramref name="x" /> - (<paramref name="y" /> Q)，其中 Q 是 <paramref name="x" /> / <paramref name="y" /> 的商的最接近整数（如果 <paramref name="x" /> / <paramref name="y" /> 在两个整数中间，则返回偶数）。如果 <paramref name="x" /> - (<paramref name="y" /> Q) 为零，则在 <paramref name="x" /> 为正时返回值 +0，而在 <paramref name="x" /> 为负时返回 -0。如果 <paramref name="y" /> = 0，则返回 <see cref="F:System.Double.NaN" />。</returns>
    /// <param name="x">被除数。</param>
    /// <param name="y">除数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static double IEEERemainder(double x, double y)
    {
      if (double.IsNaN(x))
        return x;
      if (double.IsNaN(y))
        return y;
      double d = x % y;
      if (double.IsNaN(d))
        return double.NaN;
      if (d == 0.0 && double.IsNegative(x))
        return double.NegativeZero;
      double num = d - Math.Abs(y) * (double) Math.Sign(x);
      if (Math.Abs(num) == Math.Abs(d))
      {
        double a = x / y;
        if (Math.Abs(Math.Round(a)) > Math.Abs(a))
          return num;
        return d;
      }
      if (Math.Abs(num) < Math.Abs(d))
        return num;
      return d;
    }

    /// <summary>返回 8 位有符号整数的绝对值。</summary>
    /// <returns>8 位有符号整数 x，满足 0 ≤ x ≤<see cref="F:System.SByte.MaxValue" />。</returns>
    /// <param name="value">一个大于 <see cref="F:System.SByte.MinValue" /> 但小于或等于 <see cref="F:System.SByte.MaxValue" /> 的数字。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 等于 <see cref="F:System.SByte.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte Abs(sbyte value)
    {
      if ((int) value >= 0)
        return value;
      return Math.AbsHelper(value);
    }

    private static sbyte AbsHelper(sbyte value)
    {
      if ((int) value == (int) sbyte.MinValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_NegateTwosCompNum"));
      return -value;
    }

    /// <summary>返回 16 位有符号整数的绝对值。</summary>
    /// <returns>16 位带符号整数 x，满足 0 ≤ x ≤<see cref="F:System.Int16.MaxValue" />。</returns>
    /// <param name="value">一个大于 <see cref="F:System.Int16.MinValue" /> 但小于或等于 <see cref="F:System.Int16.MaxValue" /> 的数字。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 等于 <see cref="F:System.Int16.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static short Abs(short value)
    {
      if ((int) value >= 0)
        return value;
      return Math.AbsHelper(value);
    }

    private static short AbsHelper(short value)
    {
      if ((int) value == (int) short.MinValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_NegateTwosCompNum"));
      return -value;
    }

    /// <summary>返回 32 位有符号整数的绝对值。</summary>
    /// <returns>32 位带符号整数 x，满足 0 ≤ x ≤<see cref="F:System.Int32.MaxValue" />。</returns>
    /// <param name="value">一个大于 <see cref="F:System.Int32.MinValue" /> 但小于或等于 <see cref="F:System.Int32.MaxValue" /> 的数字。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 等于 <see cref="F:System.Int32.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int Abs(int value)
    {
      if (value >= 0)
        return value;
      return Math.AbsHelper(value);
    }

    private static int AbsHelper(int value)
    {
      if (value == int.MinValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_NegateTwosCompNum"));
      return -value;
    }

    /// <summary>返回 64 位有符号整数的绝对值。</summary>
    /// <returns>64 位带符号整数 x，满足 0 ≤ x ≤<see cref="F:System.Int64.MaxValue" />。</returns>
    /// <param name="value">一个大于 <see cref="F:System.Int64.MinValue" /> 但小于或等于 <see cref="F:System.Int64.MaxValue" /> 的数字。</param>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> 等于 <see cref="F:System.Int64.MinValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static long Abs(long value)
    {
      if (value >= 0L)
        return value;
      return Math.AbsHelper(value);
    }

    private static long AbsHelper(long value)
    {
      if (value == long.MinValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_NegateTwosCompNum"));
      return -value;
    }

    /// <summary>返回单精度浮点数字的绝对值。</summary>
    /// <returns>一个单精度浮点数 x，满足 0 ≤ x ≤<see cref="F:System.Single.MaxValue" />。</returns>
    /// <param name="value">一个大于或等于 <see cref="F:System.Single.MinValue" /> 但小于或等于 <see cref="F:System.Single.MaxValue" /> 的数字。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern float Abs(float value);

    /// <summary>返回双精度浮点数字的绝对值。</summary>
    /// <returns>一个双精度浮点数 x，满足 0 ≤ x ≤<see cref="F:System.Double.MaxValue" />。</returns>
    /// <param name="value">一个大于或等于 <see cref="F:System.Double.MinValue" /> 但小于或等于 <see cref="F:System.Double.MaxValue" /> 的数字。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Abs(double value);

    /// <summary>返回 <see cref="T:System.Decimal" /> 数字的绝对值。</summary>
    /// <returns>十进制数 x，使其满足 0 ≤ x ≤<see cref="F:System.Decimal.MaxValue" />。</returns>
    /// <param name="value">一个大于或等于 <see cref="F:System.Decimal.MinValue" /> 但小于或等于 <see cref="F:System.Decimal.MaxValue" /> 的数字。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static Decimal Abs(Decimal value)
    {
      return Decimal.Abs(value);
    }

    /// <summary>返回两个 8 位有符号的整数中较大的一个。</summary>
    /// <returns>
    /// <paramref name="val1" /> 或 <paramref name="val2" /> 参数中较大的一个。</returns>
    /// <param name="val1">要比较的两个 8 位有符号整数中的第一个。</param>
    /// <param name="val2">要比较的两个 8 位有符号整数中的第二个。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static sbyte Max(sbyte val1, sbyte val2)
    {
      if ((int) val1 < (int) val2)
        return val2;
      return val1;
    }

    /// <summary>返回两个 8 位无符号整数中较大的一个。</summary>
    /// <returns>
    /// <paramref name="val1" /> 或 <paramref name="val2" /> 参数中较大的一个。</returns>
    /// <param name="val1">要比较的两个 8 位无符号整数中的第一个。</param>
    /// <param name="val2">要比较的两个 8 位无符号整数中的第二个。</param>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static byte Max(byte val1, byte val2)
    {
      if ((int) val1 < (int) val2)
        return val2;
      return val1;
    }

    /// <summary>返回两个 16 位有符号的整数中较大的一个。</summary>
    /// <returns>
    /// <paramref name="val1" /> 或 <paramref name="val2" /> 参数中较大的一个。</returns>
    /// <param name="val1">要比较的两个 16 位有符号整数中的第一个。</param>
    /// <param name="val2">要比较的两个 16 位有符号整数中的第二个。</param>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static short Max(short val1, short val2)
    {
      if ((int) val1 < (int) val2)
        return val2;
      return val1;
    }

    /// <summary>返回两个 16 位无符号整数中较大的一个。</summary>
    /// <returns>
    /// <paramref name="val1" /> 或 <paramref name="val2" /> 参数中较大的一个。</returns>
    /// <param name="val1">要比较的两个 16 位无符号整数中的第一个。</param>
    /// <param name="val2">要比较的两个 16 位无符号整数中的第二个。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static ushort Max(ushort val1, ushort val2)
    {
      if ((int) val1 < (int) val2)
        return val2;
      return val1;
    }

    /// <summary>返回两个 32 位有符号的整数中较大的一个。</summary>
    /// <returns>
    /// <paramref name="val1" /> 或 <paramref name="val2" /> 参数中较大的一个。</returns>
    /// <param name="val1">要比较的两个 32 位有符号整数中的第一个。</param>
    /// <param name="val2">要比较的两个 32 位有符号整数中的第二个。</param>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static int Max(int val1, int val2)
    {
      if (val1 < val2)
        return val2;
      return val1;
    }

    /// <summary>返回两个 32 位无符号整数中较大的一个。</summary>
    /// <returns>
    /// <paramref name="val1" /> 或 <paramref name="val2" /> 参数中较大的一个。</returns>
    /// <param name="val1">要比较的两个 32 位无符号整数中的第一个。</param>
    /// <param name="val2">要比较的两个 32 位无符号整数中的第二个。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static uint Max(uint val1, uint val2)
    {
      if (val1 < val2)
        return val2;
      return val1;
    }

    /// <summary>返回两个 64 位有符号的整数中较大的一个。</summary>
    /// <returns>
    /// <paramref name="val1" /> 或 <paramref name="val2" /> 参数中较大的一个。</returns>
    /// <param name="val1">要比较的两个 64 位有符号整数中的第一个。</param>
    /// <param name="val2">要比较的两个 64 位有符号整数中的第二个。</param>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static long Max(long val1, long val2)
    {
      if (val1 < val2)
        return val2;
      return val1;
    }

    /// <summary>返回两个 64 位无符号整数中较大的一个。</summary>
    /// <returns>
    /// <paramref name="val1" /> 或 <paramref name="val2" /> 参数中较大的一个。</returns>
    /// <param name="val1">要比较的两个 64 位无符号整数中的第一个。</param>
    /// <param name="val2">要比较的两个 64 位无符号整数中的第二个。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static ulong Max(ulong val1, ulong val2)
    {
      if (val1 < val2)
        return val2;
      return val1;
    }

    /// <summary>返回两个单精度浮点数字中较大的一个。</summary>
    /// <returns>
    /// <paramref name="val1" /> 或 <paramref name="val2" /> 参数中较大的一个。如果 <paramref name="val1" /> 或 <paramref name="val2" /> 或者 <paramref name="val1" /> 和 <paramref name="val2" /> 都等于 <see cref="F:System.Single.NaN" />，则返回 <see cref="F:System.Single.NaN" />。</returns>
    /// <param name="val1">要比较的两个单精度浮点数中的第一个。</param>
    /// <param name="val2">要比较的两个单精度浮点数中的第二个。</param>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static float Max(float val1, float val2)
    {
      if ((double) val1 > (double) val2 || float.IsNaN(val1))
        return val1;
      return val2;
    }

    /// <summary>返回两个双精度浮点数字中较大的一个。</summary>
    /// <returns>
    /// <paramref name="val1" /> 或 <paramref name="val2" /> 参数中较大的一个。如果 <paramref name="val1" /> 或 <paramref name="val2" /> 或者 <paramref name="val1" /> 和 <paramref name="val2" /> 都等于 <see cref="F:System.Double.NaN" />，则返回 <see cref="F:System.Double.NaN" />。</returns>
    /// <param name="val1">要比较的两个双精度浮点数中的第一个。</param>
    /// <param name="val2">要比较的两个双精度浮点数中的第二个</param>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static double Max(double val1, double val2)
    {
      if (val1 > val2 || double.IsNaN(val1))
        return val1;
      return val2;
    }

    /// <summary>返回两个十进制数中较大的一个。</summary>
    /// <returns>
    /// <paramref name="val1" /> 或 <paramref name="val2" /> 参数中较大的一个。</returns>
    /// <param name="val1">要比较的两个十进制数字中的第一个。</param>
    /// <param name="val2">要比较的两个十进制数字中的第二个。</param>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static Decimal Max(Decimal val1, Decimal val2)
    {
      return Decimal.Max(val1, val2);
    }

    /// <summary>返回两个 8 位有符号整数中较小的一个。</summary>
    /// <returns>
    /// <paramref name="val1" /> 或 <paramref name="val2" /> 参数中较小的一个。</returns>
    /// <param name="val1">要比较的两个 8 位有符号整数中的第一个。</param>
    /// <param name="val2">要比较的两个 8 位有符号整数中的第二个。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static sbyte Min(sbyte val1, sbyte val2)
    {
      if ((int) val1 > (int) val2)
        return val2;
      return val1;
    }

    /// <summary>返回两个 8 位无符号整数中较小的一个。</summary>
    /// <returns>
    /// <paramref name="val1" /> 或 <paramref name="val2" /> 参数中较小的一个。</returns>
    /// <param name="val1">要比较的两个 8 位无符号整数中的第一个。</param>
    /// <param name="val2">要比较的两个 8 位无符号整数中的第二个。</param>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static byte Min(byte val1, byte val2)
    {
      if ((int) val1 > (int) val2)
        return val2;
      return val1;
    }

    /// <summary>返回两个 16 位有符号整数中较小的一个。</summary>
    /// <returns>
    /// <paramref name="val1" /> 或 <paramref name="val2" /> 参数中较小的一个。</returns>
    /// <param name="val1">要比较的两个 16 位有符号整数中的第一个。</param>
    /// <param name="val2">要比较的两个 16 位有符号整数中的第二个。</param>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static short Min(short val1, short val2)
    {
      if ((int) val1 > (int) val2)
        return val2;
      return val1;
    }

    /// <summary>返回两个 16 位无符号整数中较小的一个。</summary>
    /// <returns>
    /// <paramref name="val1" /> 或 <paramref name="val2" /> 参数中较小的一个。</returns>
    /// <param name="val1">要比较的两个 16 位无符号整数中的第一个。</param>
    /// <param name="val2">要比较的两个 16 位无符号整数中的第二个。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static ushort Min(ushort val1, ushort val2)
    {
      if ((int) val1 > (int) val2)
        return val2;
      return val1;
    }

    /// <summary>返回两个 32 位有符号整数中较小的一个。</summary>
    /// <returns>
    /// <paramref name="val1" /> 或 <paramref name="val2" /> 参数中较小的一个。</returns>
    /// <param name="val1">要比较的两个 32 位有符号整数中的第一个。</param>
    /// <param name="val2">要比较的两个 32 位有符号整数中的第二个。</param>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static int Min(int val1, int val2)
    {
      if (val1 > val2)
        return val2;
      return val1;
    }

    /// <summary>返回两个 32 位无符号整数中较小的一个。</summary>
    /// <returns>
    /// <paramref name="val1" /> 或 <paramref name="val2" /> 参数中较小的一个。</returns>
    /// <param name="val1">要比较的两个 32 位无符号整数中的第一个。</param>
    /// <param name="val2">要比较的两个 32 位无符号整数中的第二个。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static uint Min(uint val1, uint val2)
    {
      if (val1 > val2)
        return val2;
      return val1;
    }

    /// <summary>返回两个 64 位有符号整数中较小的一个。</summary>
    /// <returns>
    /// <paramref name="val1" /> 或 <paramref name="val2" /> 参数中较小的一个。</returns>
    /// <param name="val1">要比较的两个 64 位有符号整数中的第一个。</param>
    /// <param name="val2">要比较的两个 64 位有符号整数中的第二个。</param>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static long Min(long val1, long val2)
    {
      if (val1 > val2)
        return val2;
      return val1;
    }

    /// <summary>返回两个 64 位无符号整数中较小的一个。</summary>
    /// <returns>
    /// <paramref name="val1" /> 或 <paramref name="val2" /> 参数中较小的一个。</returns>
    /// <param name="val1">要比较的两个 64 位无符号整数中的第一个。</param>
    /// <param name="val2">要比较的两个 64 位无符号整数中的第二个。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static ulong Min(ulong val1, ulong val2)
    {
      if (val1 > val2)
        return val2;
      return val1;
    }

    /// <summary>返回两个单精度浮点数字中较小的一个。</summary>
    /// <returns>
    /// <paramref name="val1" /> 或 <paramref name="val2" /> 参数中较小的一个。如果 <paramref name="val1" /> 或 <paramref name="val2" /> 或者 <paramref name="val1" /> 和 <paramref name="val2" /> 都等于 <see cref="F:System.Single.NaN" />，则返回 <see cref="F:System.Single.NaN" />。</returns>
    /// <param name="val1">要比较的两个单精度浮点数中的第一个。</param>
    /// <param name="val2">要比较的两个单精度浮点数中的第二个。</param>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static float Min(float val1, float val2)
    {
      if ((double) val1 < (double) val2 || float.IsNaN(val1))
        return val1;
      return val2;
    }

    /// <summary>返回两个双精度浮点数字中较小的一个。</summary>
    /// <returns>
    /// <paramref name="val1" /> 或 <paramref name="val2" /> 参数中较小的一个。如果 <paramref name="val1" /> 或 <paramref name="val2" /> 或者 <paramref name="val1" /> 和 <paramref name="val2" /> 都等于 <see cref="F:System.Double.NaN" />，则返回 <see cref="F:System.Double.NaN" />。</returns>
    /// <param name="val1">要比较的两个双精度浮点数中的第一个。</param>
    /// <param name="val2">要比较的两个双精度浮点数中的第二个</param>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static double Min(double val1, double val2)
    {
      if (val1 < val2 || double.IsNaN(val1))
        return val1;
      return val2;
    }

    /// <summary>返回两个十进制数中较小的一个。</summary>
    /// <returns>
    /// <paramref name="val1" /> 或 <paramref name="val2" /> 参数中较小的一个。</returns>
    /// <param name="val1">要比较的两个十进制数字中的第一个。</param>
    /// <param name="val2">要比较的两个十进制数字中的第二个。</param>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static Decimal Min(Decimal val1, Decimal val2)
    {
      return Decimal.Min(val1, val2);
    }

    /// <summary>返回指定数字在使用指定底时的对数。</summary>
    /// <returns>下表中的值之一。（+Infinity 表示 <see cref="F:System.Double.PositiveInfinity" />，-Infinity 表示 <see cref="F:System.Double.NegativeInfinity" />，NaN 表示 <see cref="F:System.Double.NaN" />。）<paramref name="a" /><paramref name="newBase" />返回值<paramref name="a" />&gt; 0(0 &lt;<paramref name="newBase" />&lt; 1) 或 (<paramref name="newBase" />&gt; 1)lognewBase(a)<paramref name="a" />&lt; 0（任意值）NaN（任意值）<paramref name="newBase" />&lt; 0NaN<paramref name="a" /> != 1<paramref name="newBase" /> = 0NaN<paramref name="a" /> != 1<paramref name="newBase" /> = +InfinityNaN<paramref name="a" /> = NaN（任意值）NaN（任意值）<paramref name="newBase" /> = NaNNaN（任意值）<paramref name="newBase" /> = 1NaN<paramref name="a" /> = 00 &lt;<paramref name="newBase" />&lt; 1 +Infinity<paramref name="a" /> = 0<paramref name="newBase" />&gt; 1-Infinity<paramref name="a" /> = + 无穷大0 &lt;<paramref name="newBase" />&lt; 1-Infinity<paramref name="a" /> = + 无穷大<paramref name="newBase" />&gt; 1+Infinity<paramref name="a" /> = 1<paramref name="newBase" /> = 00<paramref name="a" /> = 1<paramref name="newBase" /> = +Infinity0</returns>
    /// <param name="a">要查找其对数的数字。</param>
    /// <param name="newBase">对数的底。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static double Log(double a, double newBase)
    {
      if (double.IsNaN(a))
        return a;
      if (double.IsNaN(newBase))
        return newBase;
      if (newBase == 1.0 || a != 1.0 && (newBase == 0.0 || double.IsPositiveInfinity(newBase)))
        return double.NaN;
      return Math.Log(a) / Math.Log(newBase);
    }

    /// <summary>返回表示 8 位有符号整数的符号的值。</summary>
    /// <returns>一个指示 <paramref name="value" /> 的符号的数字，如下表所示。返回值 含义 -1 <paramref name="value" /> 小于零。0 <paramref name="value" /> 等于零。1 <paramref name="value" /> 大于零。</returns>
    /// <param name="value">有符号的数字。</param>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static int Sign(sbyte value)
    {
      if ((int) value < 0)
        return -1;
      return (int) value > 0 ? 1 : 0;
    }

    /// <summary>返回表示 16 位有符号整数的符号的值。</summary>
    /// <returns>一个指示 <paramref name="value" /> 的符号的数字，如下表所示。返回值 含义 -1 <paramref name="value" /> 小于零。0 <paramref name="value" /> 等于零。1 <paramref name="value" /> 大于零。</returns>
    /// <param name="value">有符号的数字。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int Sign(short value)
    {
      if ((int) value < 0)
        return -1;
      return (int) value > 0 ? 1 : 0;
    }

    /// <summary>返回表示 32 位有符号整数的符号的值。</summary>
    /// <returns>一个指示 <paramref name="value" /> 的符号的数字，如下表所示。返回值 含义 -1 <paramref name="value" /> 小于零。0 <paramref name="value" /> 等于零。1 <paramref name="value" /> 大于零。</returns>
    /// <param name="value">有符号的数字。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int Sign(int value)
    {
      if (value < 0)
        return -1;
      return value > 0 ? 1 : 0;
    }

    /// <summary>返回表示 64 位有符号整数的符号的值。</summary>
    /// <returns>一个指示 <paramref name="value" /> 的符号的数字，如下表所示。返回值 含义 -1 <paramref name="value" /> 小于零。0 <paramref name="value" /> 等于零。1 <paramref name="value" /> 大于零。</returns>
    /// <param name="value">有符号的数字。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int Sign(long value)
    {
      if (value < 0L)
        return -1;
      return value > 0L ? 1 : 0;
    }

    /// <summary>返回表示单精度浮点数字的符号的值。</summary>
    /// <returns>一个指示 <paramref name="value" /> 的符号的数字，如下表所示。返回值 含义 -1 <paramref name="value" /> 小于零。0 <paramref name="value" /> 等于零。1 <paramref name="value" /> 大于零。</returns>
    /// <param name="value">有符号的数字。</param>
    /// <exception cref="T:System.ArithmeticException">
    /// <paramref name="value" /> 等于 <see cref="F:System.Single.NaN" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int Sign(float value)
    {
      if ((double) value < 0.0)
        return -1;
      if ((double) value > 0.0)
        return 1;
      if ((double) value == 0.0)
        return 0;
      throw new ArithmeticException(Environment.GetResourceString("Arithmetic_NaN"));
    }

    /// <summary>返回表示双精度浮点数字的符号的值。</summary>
    /// <returns>一个指示 <paramref name="value" /> 的符号的数字，如下表所示。返回值 含义 -1 <paramref name="value" /> 小于零。0 <paramref name="value" /> 等于零。1 <paramref name="value" /> 大于零。</returns>
    /// <param name="value">有符号的数字。</param>
    /// <exception cref="T:System.ArithmeticException">
    /// <paramref name="value" /> 等于 <see cref="F:System.Double.NaN" />。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int Sign(double value)
    {
      if (value < 0.0)
        return -1;
      if (value > 0.0)
        return 1;
      if (value == 0.0)
        return 0;
      throw new ArithmeticException(Environment.GetResourceString("Arithmetic_NaN"));
    }

    /// <summary>返回表示十进制数符号的值。</summary>
    /// <returns>一个指示 <paramref name="value" /> 的符号的数字，如下表所示。返回值 含义 -1 <paramref name="value" /> 小于零。0 <paramref name="value" /> 等于零。1 <paramref name="value" /> 大于零。</returns>
    /// <param name="value">已签名的十进制数。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int Sign(Decimal value)
    {
      if (value < Decimal.Zero)
        return -1;
      return value > Decimal.Zero ? 1 : 0;
    }

    /// <summary>生成两个 32 位数字的完整乘积。</summary>
    /// <returns>包含指定数字乘积的数。</returns>
    /// <param name="a">要相乘的第一个数。</param>
    /// <param name="b">要相乘的第二个数。</param>
    /// <filterpriority>1</filterpriority>
    public static long BigMul(int a, int b)
    {
      return (long) a * (long) b;
    }

    /// <summary>计算两个 32 位有符号整数的商，并通过输出参数返回余数。</summary>
    /// <returns>指定数字的商。</returns>
    /// <param name="a">被除数。</param>
    /// <param name="b">除数。</param>
    /// <param name="result">余数。</param>
    /// <exception cref="T:System.DivideByZeroException">
    /// <paramref name="b" /> 是零。</exception>
    /// <filterpriority>1</filterpriority>
    public static int DivRem(int a, int b, out int result)
    {
      result = a % b;
      return a / b;
    }

    /// <summary>计算两个 64 位有符号整数的商，并通过输出参数返回余数。</summary>
    /// <returns>指定数字的商。</returns>
    /// <param name="a">被除数。</param>
    /// <param name="b">除数。</param>
    /// <param name="result">余数。</param>
    /// <exception cref="T:System.DivideByZeroException">
    /// <paramref name="b" /> 是零。</exception>
    /// <filterpriority>1</filterpriority>
    public static long DivRem(long a, long b, out long result)
    {
      result = a % b;
      return a / b;
    }
  }
}
