// Decompiled with JetBrains decompiler
// Type: System.Globalization.NumberFormatInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Globalization
{
  /// <summary>提供用于对数字值进行格式设置和分析的区域性特定信息。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class NumberFormatInfo : ICloneable, IFormatProvider
  {
    internal int[] numberGroupSizes = new int[1]{ 3 };
    internal int[] currencyGroupSizes = new int[1]{ 3 };
    internal int[] percentGroupSizes = new int[1]{ 3 };
    internal string positiveSign = "+";
    internal string negativeSign = "-";
    internal string numberDecimalSeparator = ".";
    internal string numberGroupSeparator = ",";
    internal string currencyGroupSeparator = ",";
    internal string currencyDecimalSeparator = ".";
    internal string currencySymbol = "¤";
    internal string nanSymbol = "NaN";
    internal string positiveInfinitySymbol = "Infinity";
    internal string negativeInfinitySymbol = "-Infinity";
    internal string percentDecimalSeparator = ".";
    internal string percentGroupSeparator = ",";
    internal string percentSymbol = "%";
    internal string perMilleSymbol = "‰";
    [OptionalField(VersionAdded = 2)]
    internal string[] nativeDigits = new string[10]{ "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
    internal int numberDecimalDigits = 2;
    internal int currencyDecimalDigits = 2;
    internal int numberNegativePattern = 1;
    internal int percentDecimalDigits = 2;
    [OptionalField(VersionAdded = 2)]
    internal int digitSubstitution = 1;
    [OptionalField(VersionAdded = 1)]
    internal bool validForParseAsNumber = true;
    [OptionalField(VersionAdded = 1)]
    internal bool validForParseAsCurrency = true;
    private static volatile NumberFormatInfo invariantInfo;
    internal string ansiCurrencySymbol;
    [OptionalField(VersionAdded = 1)]
    internal int m_dataItem;
    internal int currencyPositivePattern;
    internal int currencyNegativePattern;
    internal int percentPositivePattern;
    internal int percentNegativePattern;
    internal bool isReadOnly;
    [OptionalField(VersionAdded = 1)]
    internal bool m_useUserOverride;
    [OptionalField(VersionAdded = 2)]
    internal bool m_isInvariant;
    private const NumberStyles InvalidNumberStyles = ~(NumberStyles.Any | NumberStyles.AllowHexSpecifier);

    /// <summary>获取不依赖于区域性的（固定）只读的 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象。</summary>
    /// <returns>不依赖于区域性的（固定的）默认只读对象。</returns>
    [__DynamicallyInvokable]
    public static NumberFormatInfo InvariantInfo
    {
      [__DynamicallyInvokable] get
      {
        if (NumberFormatInfo.invariantInfo == null)
          NumberFormatInfo.invariantInfo = NumberFormatInfo.ReadOnly(new NumberFormatInfo()
          {
            m_isInvariant = true
          });
        return NumberFormatInfo.invariantInfo;
      }
    }

    /// <summary>获取或设置在货币值中使用的小数位数。</summary>
    /// <returns>要在货币值中使用的小数位数。<see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> 的默认值为 2。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">该属性被设置为小于 0 或大于 99 的值。</exception>
    /// <exception cref="T:System.InvalidOperationException">此属性设置与 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    [__DynamicallyInvokable]
    public int CurrencyDecimalDigits
    {
      [__DynamicallyInvokable] get
      {
        return this.currencyDecimalDigits;
      }
      [__DynamicallyInvokable] set
      {
        if (value < 0 || value > 99)
          throw new ArgumentOutOfRangeException("CurrencyDecimalDigits", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 0, (object) 99));
        this.VerifyWritable();
        this.currencyDecimalDigits = value;
      }
    }

    /// <summary>获取或设置要在货币值中用作小数分隔符的字符串。</summary>
    /// <returns>要在货币值中用作小数分隔符的字符串。<see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> 默认为“.”。</returns>
    /// <exception cref="T:System.ArgumentNullException">该属性设置为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">此属性设置与 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    /// <exception cref="T:System.ArgumentException">该属性被设置为空字符串。</exception>
    [__DynamicallyInvokable]
    public string CurrencyDecimalSeparator
    {
      [__DynamicallyInvokable] get
      {
        return this.currencyDecimalSeparator;
      }
      [__DynamicallyInvokable] set
      {
        this.VerifyWritable();
        NumberFormatInfo.VerifyDecimalSeparator(value, "CurrencyDecimalSeparator");
        this.currencyDecimalSeparator = value;
      }
    }

    /// <summary>获取一个值，该值指示 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是否为只读。</summary>
    /// <returns>如果 <see cref="T:System.Globalization.NumberFormatInfo" /> 是只读的，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsReadOnly
    {
      [__DynamicallyInvokable] get
      {
        return this.isReadOnly;
      }
    }

    /// <summary>获取或设置货币值中小数点左边每一组的位数。</summary>
    /// <returns>货币值中小数点左边每一组的位数。<see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> 的默认值是一个一维数组，该数组只包含一个设置为 3 的元素。</returns>
    /// <exception cref="T:System.ArgumentNullException">该属性设置为 null。</exception>
    /// <exception cref="T:System.ArgumentException">此属性设置，该数组包含一个小于 0 或大于 9 的条目。- 或 - 此属性设置，该数组包含除最后一项，设置为 0 之外的条目。</exception>
    /// <exception cref="T:System.InvalidOperationException">此属性设置与 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    [__DynamicallyInvokable]
    public int[] CurrencyGroupSizes
    {
      [__DynamicallyInvokable] get
      {
        return (int[]) this.currencyGroupSizes.Clone();
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException("CurrencyGroupSizes", Environment.GetResourceString("ArgumentNull_Obj"));
        this.VerifyWritable();
        int[] groupSize = (int[]) value.Clone();
        NumberFormatInfo.CheckGroupSize("CurrencyGroupSizes", groupSize);
        this.currencyGroupSizes = groupSize;
      }
    }

    /// <summary>获取或设置数值中小数点左边每一组的位数。</summary>
    /// <returns>数值中小数点左边每一组的位数。<see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> 的默认值是一个一维数组，该数组只包含一个设置为 3 的元素。</returns>
    /// <exception cref="T:System.ArgumentNullException">该属性设置为 null。</exception>
    /// <exception cref="T:System.ArgumentException">此属性设置，该数组包含一个小于 0 或大于 9 的条目。- 或 - 此属性设置，该数组包含除最后一项，设置为 0 之外的条目。</exception>
    /// <exception cref="T:System.InvalidOperationException">此属性设置与 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    [__DynamicallyInvokable]
    public int[] NumberGroupSizes
    {
      [__DynamicallyInvokable] get
      {
        return (int[]) this.numberGroupSizes.Clone();
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException("NumberGroupSizes", Environment.GetResourceString("ArgumentNull_Obj"));
        this.VerifyWritable();
        int[] groupSize = (int[]) value.Clone();
        NumberFormatInfo.CheckGroupSize("NumberGroupSizes", groupSize);
        this.numberGroupSizes = groupSize;
      }
    }

    /// <summary>获取或设置在百分比值中小数点左边每一组的位数。</summary>
    /// <returns>百分比值中小数点左边的每一组的位数。<see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> 的默认值是一个一维数组，该数组只包含一个设置为 3 的元素。</returns>
    /// <exception cref="T:System.ArgumentNullException">该属性设置为 null。</exception>
    /// <exception cref="T:System.ArgumentException">此属性设置，该数组包含一个小于 0 或大于 9 的条目。- 或 - 此属性设置，该数组包含除最后一项，设置为 0 之外的条目。</exception>
    /// <exception cref="T:System.InvalidOperationException">此属性设置与 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    [__DynamicallyInvokable]
    public int[] PercentGroupSizes
    {
      [__DynamicallyInvokable] get
      {
        return (int[]) this.percentGroupSizes.Clone();
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException("PercentGroupSizes", Environment.GetResourceString("ArgumentNull_Obj"));
        this.VerifyWritable();
        int[] groupSize = (int[]) value.Clone();
        NumberFormatInfo.CheckGroupSize("PercentGroupSizes", groupSize);
        this.percentGroupSizes = groupSize;
      }
    }

    /// <summary>获取或设置在货币值中隔开小数点左边的位数组的字符串。</summary>
    /// <returns>在货币值中隔开小数点左边的位数组的字符串。<see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> 默认为“,”。</returns>
    /// <exception cref="T:System.ArgumentNullException">该属性设置为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">此属性设置与 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    [__DynamicallyInvokable]
    public string CurrencyGroupSeparator
    {
      [__DynamicallyInvokable] get
      {
        return this.currencyGroupSeparator;
      }
      [__DynamicallyInvokable] set
      {
        this.VerifyWritable();
        NumberFormatInfo.VerifyGroupSeparator(value, "CurrencyGroupSeparator");
        this.currencyGroupSeparator = value;
      }
    }

    /// <summary>获取或设置用作货币符号的字符串。</summary>
    /// <returns>用作货币符号的字符串。<see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> 默认为“¤”。</returns>
    /// <exception cref="T:System.ArgumentNullException">该属性设置为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">此属性设置与 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    [__DynamicallyInvokable]
    public string CurrencySymbol
    {
      [__DynamicallyInvokable] get
      {
        return this.currencySymbol;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException("CurrencySymbol", Environment.GetResourceString("ArgumentNull_String"));
        this.VerifyWritable();
        this.currencySymbol = value;
      }
    }

    /// <summary>获取基于当前区域性对值进行格式设置的只读的 <see cref="T:System.Globalization.NumberFormatInfo" />。</summary>
    /// <returns>基于当前线程的区域性的只读的 <see cref="T:System.Globalization.NumberFormatInfo" />。</returns>
    [__DynamicallyInvokable]
    public static NumberFormatInfo CurrentInfo
    {
      [__DynamicallyInvokable] get
      {
        CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
        if (!currentCulture.m_isInherited)
        {
          NumberFormatInfo numberFormatInfo = currentCulture.numInfo;
          if (numberFormatInfo != null)
            return numberFormatInfo;
        }
        return (NumberFormatInfo) currentCulture.GetFormat(typeof (NumberFormatInfo));
      }
    }

    /// <summary>获取或设置表示 IEEE NaN（非数字）值的字符串。</summary>
    /// <returns>表示 IEEE NaN（非数字）值的字符串。<see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> 默认为“NaN”。</returns>
    /// <exception cref="T:System.ArgumentNullException">该属性设置为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">此属性设置与 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    [__DynamicallyInvokable]
    public string NaNSymbol
    {
      [__DynamicallyInvokable] get
      {
        return this.nanSymbol;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException("NaNSymbol", Environment.GetResourceString("ArgumentNull_String"));
        this.VerifyWritable();
        this.nanSymbol = value;
      }
    }

    /// <summary>获取或设置负货币值的格式模式。</summary>
    /// <returns>负货币值的格式模式。<see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> 的默认值是 0，它表示“($n)”，其中“$”是 <see cref="P:System.Globalization.NumberFormatInfo.CurrencySymbol" />，<paramref name="n" /> 是一个数字。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">属性被设置为小于 0 或大于 15 的值。</exception>
    /// <exception cref="T:System.InvalidOperationException">此属性设置与 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    [__DynamicallyInvokable]
    public int CurrencyNegativePattern
    {
      [__DynamicallyInvokable] get
      {
        return this.currencyNegativePattern;
      }
      [__DynamicallyInvokable] set
      {
        if (value < 0 || value > 15)
          throw new ArgumentOutOfRangeException("CurrencyNegativePattern", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 0, (object) 15));
        this.VerifyWritable();
        this.currencyNegativePattern = value;
      }
    }

    /// <summary>获取或设置负数值的格式模式。</summary>
    /// <returns>负数值的格式模式。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">该属性被设置为小于 0 或大于 4 的值。</exception>
    /// <exception cref="T:System.InvalidOperationException">此属性设置与 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    [__DynamicallyInvokable]
    public int NumberNegativePattern
    {
      [__DynamicallyInvokable] get
      {
        return this.numberNegativePattern;
      }
      [__DynamicallyInvokable] set
      {
        if (value < 0 || value > 4)
          throw new ArgumentOutOfRangeException("NumberNegativePattern", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 0, (object) 4));
        this.VerifyWritable();
        this.numberNegativePattern = value;
      }
    }

    /// <summary>获取或设置正百分比值的格式模式。</summary>
    /// <returns>正百分比值的格式模式。<see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> 的默认值是 0，它表示“n %”，其中“%”是 <see cref="P:System.Globalization.NumberFormatInfo.PercentSymbol" />，<paramref name="n" /> 是一个数字。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">该属性被设置为小于 0 或大于 3 的值。</exception>
    /// <exception cref="T:System.InvalidOperationException">此属性设置与 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    [__DynamicallyInvokable]
    public int PercentPositivePattern
    {
      [__DynamicallyInvokable] get
      {
        return this.percentPositivePattern;
      }
      [__DynamicallyInvokable] set
      {
        if (value < 0 || value > 3)
          throw new ArgumentOutOfRangeException("PercentPositivePattern", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 0, (object) 3));
        this.VerifyWritable();
        this.percentPositivePattern = value;
      }
    }

    /// <summary>获取或设置负百分比值的格式模式。</summary>
    /// <returns>负百分比值的格式模式。<see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> 的默认值是 0，它表示“-n %”，其中“%”是 <see cref="P:System.Globalization.NumberFormatInfo.PercentSymbol" />，<paramref name="n" /> 是一个数字。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">属性被设置为小于 0 或大于 11 的值。</exception>
    /// <exception cref="T:System.InvalidOperationException">此属性设置与 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    [__DynamicallyInvokable]
    public int PercentNegativePattern
    {
      [__DynamicallyInvokable] get
      {
        return this.percentNegativePattern;
      }
      [__DynamicallyInvokable] set
      {
        if (value < 0 || value > 11)
          throw new ArgumentOutOfRangeException("PercentNegativePattern", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 0, (object) 11));
        this.VerifyWritable();
        this.percentNegativePattern = value;
      }
    }

    /// <summary>获取或设置表示负无穷大的字符串。</summary>
    /// <returns>表示负无穷大的字符串。<see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> 默认为“Infinity”。</returns>
    /// <exception cref="T:System.ArgumentNullException">该属性设置为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">此属性设置与 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    [__DynamicallyInvokable]
    public string NegativeInfinitySymbol
    {
      [__DynamicallyInvokable] get
      {
        return this.negativeInfinitySymbol;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException("NegativeInfinitySymbol", Environment.GetResourceString("ArgumentNull_String"));
        this.VerifyWritable();
        this.negativeInfinitySymbol = value;
      }
    }

    /// <summary>获取或设置表示关联数字是负值的字符串。</summary>
    /// <returns>表示关联数字是负值的字符串。<see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> 默认为“-”。</returns>
    /// <exception cref="T:System.ArgumentNullException">该属性设置为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">此属性设置与 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    [__DynamicallyInvokable]
    public string NegativeSign
    {
      [__DynamicallyInvokable] get
      {
        return this.negativeSign;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException("NegativeSign", Environment.GetResourceString("ArgumentNull_String"));
        this.VerifyWritable();
        this.negativeSign = value;
      }
    }

    /// <summary>获取或设置在数值中使用的小数位数。</summary>
    /// <returns>在数值中使用的小数位数。<see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> 的默认值为 2。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">该属性被设置为小于 0 或大于 99 的值。</exception>
    /// <exception cref="T:System.InvalidOperationException">此属性设置与 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    [__DynamicallyInvokable]
    public int NumberDecimalDigits
    {
      [__DynamicallyInvokable] get
      {
        return this.numberDecimalDigits;
      }
      [__DynamicallyInvokable] set
      {
        if (value < 0 || value > 99)
          throw new ArgumentOutOfRangeException("NumberDecimalDigits", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 0, (object) 99));
        this.VerifyWritable();
        this.numberDecimalDigits = value;
      }
    }

    /// <summary>获取或设置在数值中用作小数分隔符的字符串。</summary>
    /// <returns>在数值中用作小数分隔符的字符串。<see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> 默认为“.”。</returns>
    /// <exception cref="T:System.ArgumentNullException">该属性设置为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">此属性设置与 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    /// <exception cref="T:System.ArgumentException">该属性被设置为空字符串。</exception>
    [__DynamicallyInvokable]
    public string NumberDecimalSeparator
    {
      [__DynamicallyInvokable] get
      {
        return this.numberDecimalSeparator;
      }
      [__DynamicallyInvokable] set
      {
        this.VerifyWritable();
        NumberFormatInfo.VerifyDecimalSeparator(value, "NumberDecimalSeparator");
        this.numberDecimalSeparator = value;
      }
    }

    /// <summary>获取或设置在数值中隔开小数点左边的位数组的字符串。</summary>
    /// <returns>在数值中隔开小数点左边的位数组的字符串。<see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> 默认为“,”。</returns>
    /// <exception cref="T:System.ArgumentNullException">该属性设置为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">此属性设置与 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    [__DynamicallyInvokable]
    public string NumberGroupSeparator
    {
      [__DynamicallyInvokable] get
      {
        return this.numberGroupSeparator;
      }
      [__DynamicallyInvokable] set
      {
        this.VerifyWritable();
        NumberFormatInfo.VerifyGroupSeparator(value, "NumberGroupSeparator");
        this.numberGroupSeparator = value;
      }
    }

    /// <summary>获取或设置正货币值的格式模式。</summary>
    /// <returns>正货币值的格式模式。<see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> 的默认值是 0，它表示“$n”，其中“$”是 <see cref="P:System.Globalization.NumberFormatInfo.CurrencySymbol" />，<paramref name="n" /> 是一个数字。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">该属性被设置为小于 0 或大于 3 的值。</exception>
    /// <exception cref="T:System.InvalidOperationException">此属性设置与 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    [__DynamicallyInvokable]
    public int CurrencyPositivePattern
    {
      [__DynamicallyInvokable] get
      {
        return this.currencyPositivePattern;
      }
      [__DynamicallyInvokable] set
      {
        if (value < 0 || value > 3)
          throw new ArgumentOutOfRangeException("CurrencyPositivePattern", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 0, (object) 3));
        this.VerifyWritable();
        this.currencyPositivePattern = value;
      }
    }

    /// <summary>获取或设置表示正无穷大的字符串。</summary>
    /// <returns>表示正无穷大的字符串。<see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> 默认为“Infinity”。</returns>
    /// <exception cref="T:System.ArgumentNullException">该属性设置为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">此属性设置与 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    [__DynamicallyInvokable]
    public string PositiveInfinitySymbol
    {
      [__DynamicallyInvokable] get
      {
        return this.positiveInfinitySymbol;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException("PositiveInfinitySymbol", Environment.GetResourceString("ArgumentNull_String"));
        this.VerifyWritable();
        this.positiveInfinitySymbol = value;
      }
    }

    /// <summary>获取或设置指示关联数字是正值的字符串。</summary>
    /// <returns>指示关联数字是正值的字符串。<see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> 默认为“+”。</returns>
    /// <exception cref="T:System.ArgumentNullException">在设置操作中，要分配的值是 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">此属性设置与 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    [__DynamicallyInvokable]
    public string PositiveSign
    {
      [__DynamicallyInvokable] get
      {
        return this.positiveSign;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException("PositiveSign", Environment.GetResourceString("ArgumentNull_String"));
        this.VerifyWritable();
        this.positiveSign = value;
      }
    }

    /// <summary>获取或设置在百分比值中使用的小数位数。</summary>
    /// <returns>要在百分比值中使用的小数位数。<see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> 的默认值为 2。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">该属性被设置为小于 0 或大于 99 的值。</exception>
    /// <exception cref="T:System.InvalidOperationException">此属性设置与 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    [__DynamicallyInvokable]
    public int PercentDecimalDigits
    {
      [__DynamicallyInvokable] get
      {
        return this.percentDecimalDigits;
      }
      [__DynamicallyInvokable] set
      {
        if (value < 0 || value > 99)
          throw new ArgumentOutOfRangeException("PercentDecimalDigits", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), (object) 0, (object) 99));
        this.VerifyWritable();
        this.percentDecimalDigits = value;
      }
    }

    /// <summary>获取或设置在百分比值中用作小数点分隔符的字符串。</summary>
    /// <returns>在百分比值中用作小数分隔符的字符串。<see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> 默认为“.”。</returns>
    /// <exception cref="T:System.ArgumentNullException">该属性设置为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">此属性设置与 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    /// <exception cref="T:System.ArgumentException">该属性被设置为空字符串。</exception>
    [__DynamicallyInvokable]
    public string PercentDecimalSeparator
    {
      [__DynamicallyInvokable] get
      {
        return this.percentDecimalSeparator;
      }
      [__DynamicallyInvokable] set
      {
        this.VerifyWritable();
        NumberFormatInfo.VerifyDecimalSeparator(value, "PercentDecimalSeparator");
        this.percentDecimalSeparator = value;
      }
    }

    /// <summary>获取或设置在百分比值中隔离小数点左边数字组的字符串。</summary>
    /// <returns>在百分比值中隔开小数点左边的位数组的字符串。<see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> 默认为“,”。</returns>
    /// <exception cref="T:System.ArgumentNullException">该属性设置为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">此属性设置与 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    [__DynamicallyInvokable]
    public string PercentGroupSeparator
    {
      [__DynamicallyInvokable] get
      {
        return this.percentGroupSeparator;
      }
      [__DynamicallyInvokable] set
      {
        this.VerifyWritable();
        NumberFormatInfo.VerifyGroupSeparator(value, "PercentGroupSeparator");
        this.percentGroupSeparator = value;
      }
    }

    /// <summary>获取或设置用作百分比符号的字符串。</summary>
    /// <returns>用作百分比符号的字符串。<see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> 默认为“%”。</returns>
    /// <exception cref="T:System.ArgumentNullException">该属性设置为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">此属性设置与 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    [__DynamicallyInvokable]
    public string PercentSymbol
    {
      [__DynamicallyInvokable] get
      {
        return this.percentSymbol;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException("PercentSymbol", Environment.GetResourceString("ArgumentNull_String"));
        this.VerifyWritable();
        this.percentSymbol = value;
      }
    }

    /// <summary>获取或设置用作千分比符号的字符串。</summary>
    /// <returns>用作千分比符号的字符串。<see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> 默认为“‰”，它是 Unicode 字符 U+2030。</returns>
    /// <exception cref="T:System.ArgumentNullException">该属性设置为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">此属性设置与 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    [__DynamicallyInvokable]
    public string PerMilleSymbol
    {
      [__DynamicallyInvokable] get
      {
        return this.perMilleSymbol;
      }
      [__DynamicallyInvokable] set
      {
        if (value == null)
          throw new ArgumentNullException("PerMilleSymbol", Environment.GetResourceString("ArgumentNull_String"));
        this.VerifyWritable();
        this.perMilleSymbol = value;
      }
    }

    /// <summary>获取或设置与西文数字 0 到 9 等同的本机数字的字符串数组。</summary>
    /// <returns>包含与西文数字 0 到 9 等同的本机数字的字符串数组。默认值是包含元素“0”、“1”、“2”、“3”、“4”、“5”、“6”、“7”、“8”和“9”的一个数组。</returns>
    /// <exception cref="T:System.InvalidOperationException">当前 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    /// <exception cref="T:System.ArgumentNullException">在设置操作中，值是 null。- 或 -在设置操作中，值数组的元素是 null。</exception>
    /// <exception cref="T:System.ArgumentException">在设置操作中，值数组不包含 10 个元素。- 或 -在设置操作中，值数组的元素不包含单个 <see cref="T:System.Char" /> 对象或一对 <see cref="T:System.Char" /> 包含代理项对的对象。- 或 -在设置操作中，值数组的元素不是数字的数字由定义 Unicode Standard。也就是说，数组元素中的数字不具有 Unicode Number, Decimal Digit (Nd) 常规类别值。- 或 -在设置操作中，值数组中的元素的数值不对应的元素为数组中的位置。也就是说，位于索引 0，即数组的第一个元素处元素不具有值为 0，数字或位于索引 1 处的元素不具有数字值为 1。</exception>
    [ComVisible(false)]
    public string[] NativeDigits
    {
      get
      {
        return (string[]) this.nativeDigits.Clone();
      }
      set
      {
        this.VerifyWritable();
        NumberFormatInfo.VerifyNativeDigits(value, "NativeDigits");
        this.nativeDigits = value;
      }
    }

    /// <summary>获取或设置指定图形用户界面如何显示数字形状的值。</summary>
    /// <returns>指定区域性特定的数字形状的枚举值之一。</returns>
    /// <exception cref="T:System.InvalidOperationException">当前 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象是只读的。</exception>
    /// <exception cref="T:System.ArgumentException">一个集运算中的值不是有效 <see cref="T:System.Globalization.DigitShapes" /> 值。</exception>
    [ComVisible(false)]
    public DigitShapes DigitSubstitution
    {
      get
      {
        return (DigitShapes) this.digitSubstitution;
      }
      set
      {
        this.VerifyWritable();
        NumberFormatInfo.VerifyDigitSubstitution(value, "DigitSubstitution");
        this.digitSubstitution = (int) value;
      }
    }

    /// <summary>初始化不依赖于区域性的（固定的）<see cref="T:System.Globalization.NumberFormatInfo" /> 类的新可写实例。</summary>
    [__DynamicallyInvokable]
    public NumberFormatInfo()
      : this((CultureData) null)
    {
    }

    [SecuritySafeCritical]
    internal NumberFormatInfo(CultureData cultureData)
    {
      if (cultureData == null)
        return;
      cultureData.GetNFIValues(this);
      if (!cultureData.IsInvariantCulture)
        return;
      this.m_isInvariant = true;
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      this.validForParseAsNumber = this.numberDecimalSeparator != this.numberGroupSeparator;
      if (this.numberDecimalSeparator != this.numberGroupSeparator && this.numberDecimalSeparator != this.currencyGroupSeparator && (this.currencyDecimalSeparator != this.numberGroupSeparator && this.currencyDecimalSeparator != this.currencyGroupSeparator))
        this.validForParseAsCurrency = true;
      else
        this.validForParseAsCurrency = false;
    }

    [OnDeserializing]
    private void OnDeserializing(StreamingContext ctx)
    {
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
    }

    private static void VerifyDecimalSeparator(string decSep, string propertyName)
    {
      if (decSep == null)
        throw new ArgumentNullException(propertyName, Environment.GetResourceString("ArgumentNull_String"));
      if (decSep.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyDecString"));
    }

    private static void VerifyGroupSeparator(string groupSep, string propertyName)
    {
      if (groupSep == null)
        throw new ArgumentNullException(propertyName, Environment.GetResourceString("ArgumentNull_String"));
    }

    private static void VerifyNativeDigits(string[] nativeDig, string propertyName)
    {
      if (nativeDig == null)
        throw new ArgumentNullException(propertyName, Environment.GetResourceString("ArgumentNull_Array"));
      if (nativeDig.Length != 10)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNativeDigitCount"), propertyName);
      for (int index = 0; index < nativeDig.Length; ++index)
      {
        if (nativeDig[index] == null)
          throw new ArgumentNullException(propertyName, Environment.GetResourceString("ArgumentNull_ArrayValue"));
        if (nativeDig[index].Length != 1)
        {
          if (nativeDig[index].Length != 2)
            throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNativeDigitValue"), propertyName);
          if (!char.IsSurrogatePair(nativeDig[index][0], nativeDig[index][1]))
            throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNativeDigitValue"), propertyName);
        }
        if (CharUnicodeInfo.GetDecimalDigitValue(nativeDig[index], 0) != index && CharUnicodeInfo.GetUnicodeCategory(nativeDig[index], 0) != UnicodeCategory.PrivateUse)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNativeDigitValue"), propertyName);
      }
    }

    private static void VerifyDigitSubstitution(DigitShapes digitSub, string propertyName)
    {
      switch (digitSub)
      {
        case DigitShapes.Context:
          break;
        case DigitShapes.None:
          break;
        case DigitShapes.NativeNational:
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDigitSubstitution"), propertyName);
      }
    }

    private void VerifyWritable()
    {
      if (this.isReadOnly)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
    }

    /// <summary>获取与指定 <see cref="T:System.Globalization.NumberFormatInfo" /> 关联的 <see cref="T:System.IFormatProvider" />。</summary>
    /// <returns>与指定 <see cref="T:System.Globalization.NumberFormatInfo" /> 关联的 <see cref="T:System.IFormatProvider" />。</returns>
    /// <param name="formatProvider">用于获取 <see cref="T:System.Globalization.NumberFormatInfo" /> 的 <see cref="T:System.IFormatProvider" />。- 或 - 要获取 <see cref="P:System.Globalization.NumberFormatInfo.CurrentInfo" /> 的 null。</param>
    [__DynamicallyInvokable]
    public static NumberFormatInfo GetInstance(IFormatProvider formatProvider)
    {
      CultureInfo cultureInfo = formatProvider as CultureInfo;
      if (cultureInfo != null && !cultureInfo.m_isInherited)
        return cultureInfo.numInfo ?? cultureInfo.NumberFormat;
      NumberFormatInfo numberFormatInfo1 = formatProvider as NumberFormatInfo;
      if (numberFormatInfo1 != null)
        return numberFormatInfo1;
      if (formatProvider != null)
      {
        NumberFormatInfo numberFormatInfo2 = formatProvider.GetFormat(typeof (NumberFormatInfo)) as NumberFormatInfo;
        if (numberFormatInfo2 != null)
          return numberFormatInfo2;
      }
      return NumberFormatInfo.CurrentInfo;
    }

    /// <summary>创建 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象的浅表副本。</summary>
    /// <returns>从原始 <see cref="T:System.Globalization.NumberFormatInfo" /> 对象复制的新对象。</returns>
    [__DynamicallyInvokable]
    public object Clone()
    {
      NumberFormatInfo numberFormatInfo = (NumberFormatInfo) this.MemberwiseClone();
      int num = 0;
      numberFormatInfo.isReadOnly = num != 0;
      return (object) numberFormatInfo;
    }

    internal static void CheckGroupSize(string propName, int[] groupSize)
    {
      for (int index = 0; index < groupSize.Length; ++index)
      {
        if (groupSize[index] < 1)
        {
          if (index == groupSize.Length - 1 && groupSize[index] == 0)
            break;
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidGroupSize"), propName);
        }
        if (groupSize[index] > 9)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidGroupSize"), propName);
      }
    }

    /// <summary>获取提供数字格式化服务的指定类型的对象。</summary>
    /// <returns>如果 <paramref name="formatType" /> 与当前 <see cref="T:System.Globalization.NumberFormatInfo" /> 的类型相同，则为当前 <see cref="T:System.Globalization.NumberFormatInfo" />；否则为 null。</returns>
    /// <param name="formatType">所需格式化服务的 <see cref="T:System.Type" />。</param>
    [__DynamicallyInvokable]
    public object GetFormat(Type formatType)
    {
      if (!(formatType == typeof (NumberFormatInfo)))
        return (object) null;
      return (object) this;
    }

    /// <summary>返回只读的 <see cref="T:System.Globalization.NumberFormatInfo" /> 包装。</summary>
    /// <returns>
    /// <paramref name="nfi" /> 周围的只读 <see cref="T:System.Globalization.NumberFormatInfo" /> 包装。</returns>
    /// <param name="nfi">要包装的 <see cref="T:System.Globalization.NumberFormatInfo" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="nfi" /> 为 null。</exception>
    [__DynamicallyInvokable]
    public static NumberFormatInfo ReadOnly(NumberFormatInfo nfi)
    {
      if (nfi == null)
        throw new ArgumentNullException("nfi");
      if (nfi.IsReadOnly)
        return nfi;
      NumberFormatInfo numberFormatInfo = (NumberFormatInfo) nfi.MemberwiseClone();
      int num = 1;
      numberFormatInfo.isReadOnly = num != 0;
      return numberFormatInfo;
    }

    internal static void ValidateParseStyleInteger(NumberStyles style)
    {
      if ((style & ~(NumberStyles.Any | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNumberStyles"), "style");
      if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None && (style & ~NumberStyles.HexNumber) != NumberStyles.None)
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidHexStyle"));
    }

    internal static void ValidateParseStyleFloatingPoint(NumberStyles style)
    {
      if ((style & ~(NumberStyles.Any | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNumberStyles"), "style");
      if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
        throw new ArgumentException(Environment.GetResourceString("Arg_HexStyleNotSupported"));
    }
  }
}
