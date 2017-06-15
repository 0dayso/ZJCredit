// Decompiled with JetBrains decompiler
// Type: System.Enum
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System
{
  /// <summary>为枚举提供基类。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class Enum : ValueType, IComparable, IFormattable, IConvertible
  {
    private static readonly char[] enumSeperatorCharArray = new char[1]{ ',' };
    private const string enumSeperator = ", ";

    /// <summary>初始化 <see cref="T:System.Enum" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    protected Enum()
    {
    }

    [SecuritySafeCritical]
    private static Enum.ValuesAndNames GetCachedValuesAndNames(RuntimeType enumType, bool getNames)
    {
      Enum.ValuesAndNames valuesAndNames = enumType.GenericCache as Enum.ValuesAndNames;
      if (valuesAndNames == null || getNames && valuesAndNames.Names == null)
      {
        ulong[] o1 = (ulong[]) null;
        string[] o2 = (string[]) null;
        Enum.GetEnumValuesAndNames(enumType.GetTypeHandleInternal(), JitHelpers.GetObjectHandleOnStack<ulong[]>(ref o1), JitHelpers.GetObjectHandleOnStack<string[]>(ref o2), getNames);
        valuesAndNames = new Enum.ValuesAndNames(o1, o2);
        enumType.GenericCache = (object) valuesAndNames;
      }
      return valuesAndNames;
    }

    private static string InternalFormattedHexString(object value)
    {
      switch (Convert.GetTypeCode(value))
      {
        case TypeCode.Boolean:
          return Convert.ToByte((bool) value).ToString("X2", (IFormatProvider) null);
        case TypeCode.Char:
          return ((ushort) (char) value).ToString("X4", (IFormatProvider) null);
        case TypeCode.SByte:
          return ((byte) (sbyte) value).ToString("X2", (IFormatProvider) null);
        case TypeCode.Byte:
          return ((byte) value).ToString("X2", (IFormatProvider) null);
        case TypeCode.Int16:
          return ((ushort) (short) value).ToString("X4", (IFormatProvider) null);
        case TypeCode.UInt16:
          return ((ushort) value).ToString("X4", (IFormatProvider) null);
        case TypeCode.Int32:
          return ((uint) (int) value).ToString("X8", (IFormatProvider) null);
        case TypeCode.UInt32:
          return ((uint) value).ToString("X8", (IFormatProvider) null);
        case TypeCode.Int64:
          return ((ulong) (long) value).ToString("X16", (IFormatProvider) null);
        case TypeCode.UInt64:
          return ((ulong) value).ToString("X16", (IFormatProvider) null);
        default:
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_UnknownEnumType"));
      }
    }

    private static string InternalFormat(RuntimeType eT, object value)
    {
      if (!eT.IsDefined(typeof (FlagsAttribute), false))
        return Enum.GetName((Type) eT, value) ?? value.ToString();
      return Enum.InternalFlagsFormat(eT, value);
    }

    private static string InternalFlagsFormat(RuntimeType eT, object value)
    {
      ulong uint64 = Enum.ToUInt64(value);
      Enum.ValuesAndNames cachedValuesAndNames = Enum.GetCachedValuesAndNames(eT, true);
      string[] strArray = cachedValuesAndNames.Names;
      ulong[] numArray = cachedValuesAndNames.Values;
      int index = numArray.Length - 1;
      StringBuilder stringBuilder = new StringBuilder();
      bool flag = true;
      ulong num = uint64;
      for (; index >= 0 && (index != 0 || (long) numArray[index] != 0L); --index)
      {
        if (((long) uint64 & (long) numArray[index]) == (long) numArray[index])
        {
          uint64 -= numArray[index];
          if (!flag)
            stringBuilder.Insert(0, ", ");
          stringBuilder.Insert(0, strArray[index]);
          flag = false;
        }
      }
      if ((long) uint64 != 0L)
        return value.ToString();
      if ((long) num != 0L)
        return stringBuilder.ToString();
      if (numArray.Length != 0 && (long) numArray[0] == 0L)
        return strArray[0];
      return "0";
    }

    internal static ulong ToUInt64(object value)
    {
      switch (Convert.GetTypeCode(value))
      {
        case TypeCode.Boolean:
        case TypeCode.Char:
        case TypeCode.Byte:
        case TypeCode.UInt16:
        case TypeCode.UInt32:
        case TypeCode.UInt64:
          return Convert.ToUInt64(value, (IFormatProvider) CultureInfo.InvariantCulture);
        case TypeCode.SByte:
        case TypeCode.Int16:
        case TypeCode.Int32:
        case TypeCode.Int64:
          return (ulong) Convert.ToInt64(value, (IFormatProvider) CultureInfo.InvariantCulture);
        default:
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_UnknownEnumType"));
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int InternalCompareTo(object o1, object o2);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeType InternalGetUnderlyingType(RuntimeType enumType);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetEnumValuesAndNames(RuntimeTypeHandle enumType, ObjectHandleOnStack values, ObjectHandleOnStack names, bool getNames);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern object InternalBoxEnum(RuntimeType enumType, long value);

    /// <summary>将一个或多个枚举常数的名称或数字值的字符串表示转换成等效的枚举对象。用于指示转换是否成功的返回值。</summary>
    /// <returns>如果 <paramref name="value" /> 参数成功转换，则为 true；否则为 false。</returns>
    /// <param name="value">要转换的枚举名称或基础值的字符串表示形式。</param>
    /// <param name="result">当此方法返回时，如果分析操作成功，<paramref name="result" /> 将包含值由 <paramref name="value" /> 表示的 <paramref name="TEnum" /> 类型的对象。如果分析操作失败，<paramref name="result" /> 将包含 <paramref name="TEnum" />的基础类型的默认值。请注意，此值不需要是 <paramref name="TEnum" /> 枚举的成员。此参数未经初始化即被传递。</param>
    /// <typeparam name="TEnum">要将 <paramref name="value" /> 转换为的枚举类型。</typeparam>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="TEnum" /> is not an enumeration type.</exception>
    [__DynamicallyInvokable]
    public static bool TryParse<TEnum>(string value, out TEnum result) where TEnum : struct
    {
      return Enum.TryParse<TEnum>(value, false, out result);
    }

    /// <summary>将一个或多个枚举常数的名称或数字值的字符串表示转换成等效的枚举对象。一个参数指定该操作是否区分大小写。用于指示转换是否成功的返回值。</summary>
    /// <returns>如果 <paramref name="value" /> 参数成功转换，则为 true；否则为 false。</returns>
    /// <param name="value">要转换的枚举名称或基础值的字符串表示形式。</param>
    /// <param name="ignoreCase">true 表示不区分大小写；false 表示区分大小写。</param>
    /// <param name="result">当此方法返回时，如果分析操作成功，<paramref name="result" /> 将包含值由 <paramref name="value" /> 表示的 <paramref name="TEnum" /> 类型的对象。如果分析操作失败，<paramref name="result" /> 将包含 <paramref name="TEnum" />的基础类型的默认值。请注意，此值不需要是 <paramref name="TEnum" /> 枚举的成员。此参数未经初始化即被传递。</param>
    /// <typeparam name="TEnum">要将 <paramref name="value" /> 转换为的枚举类型。</typeparam>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="TEnum" /> is not an enumeration type.</exception>
    [__DynamicallyInvokable]
    public static bool TryParse<TEnum>(string value, bool ignoreCase, out TEnum result) where TEnum : struct
    {
      result = default (TEnum);
      Enum.EnumResult parseResult = new Enum.EnumResult();
      parseResult.Init(false);
      int num = Enum.TryParseEnum(typeof (TEnum), value, ignoreCase, ref parseResult) ? 1 : 0;
      if (num == 0)
        return num != 0;
      result = (TEnum) parseResult.parsedEnum;
      return num != 0;
    }

    /// <summary>将一个或多个枚举常数的名称或数字值的字符串表示转换成等效的枚举对象。</summary>
    /// <returns>
    /// <paramref name="enumType" /> 类型的对象，其值由 <paramref name="value" /> 表示。</returns>
    /// <param name="enumType">枚举类型。</param>
    /// <param name="value">包含要转换的值或名称的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="enumType" /> or <paramref name="value" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.-or- <paramref name="value" /> is either an empty string or only contains white space.-or- <paramref name="value" /> is a name, but not one of the named constants defined for the enumeration. </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> is outside the range of the underlying type of <paramref name="enumType" />.</exception>
    /// <filterpriority>1</filterpriority>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public static object Parse(Type enumType, string value)
    {
      return Enum.Parse(enumType, value, false);
    }

    /// <summary>将一个或多个枚举常数的名称或数字值的字符串表示转换成等效的枚举对象。一个参数指定该操作是否不区分大小写。</summary>
    /// <returns>
    /// <paramref name="enumType" /> 类型的对象，其值由 <paramref name="value" /> 表示。</returns>
    /// <param name="enumType">枚举类型。</param>
    /// <param name="value">包含要转换的值或名称的字符串。 </param>
    /// <param name="ignoreCase">true 为忽略大小写；false 为考虑大小写。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="enumType" /> or <paramref name="value" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.-or- <paramref name="value" /> is either an empty string ("") or only contains white space.-or- <paramref name="value" /> is a name, but not one of the named constants defined for the enumeration. </exception>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="value" /> is outside the range of the underlying type of <paramref name="enumType" />.</exception>
    /// <filterpriority>1</filterpriority>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public static object Parse(Type enumType, string value, bool ignoreCase)
    {
      Enum.EnumResult parseResult = new Enum.EnumResult();
      parseResult.Init(true);
      if (Enum.TryParseEnum(enumType, value, ignoreCase, ref parseResult))
        return parseResult.parsedEnum;
      throw parseResult.GetEnumParseException();
    }

    private static bool TryParseEnum(Type enumType, string value, bool ignoreCase, ref Enum.EnumResult parseResult)
    {
      if (enumType == (Type) null)
        throw new ArgumentNullException("enumType");
      RuntimeType enumType1 = enumType as RuntimeType;
      if (enumType1 == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "enumType");
      if (!enumType.IsEnum)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
      if (value == null)
      {
        parseResult.SetFailure(Enum.ParseFailureKind.ArgumentNull, "value");
        return false;
      }
      value = value.Trim();
      if (value.Length == 0)
      {
        parseResult.SetFailure(Enum.ParseFailureKind.Argument, "Arg_MustContainEnumInfo", (object) null);
        return false;
      }
      ulong num1 = 0;
      if (char.IsDigit(value[0]) || (int) value[0] == 45 || (int) value[0] == 43)
      {
        Type underlyingType = Enum.GetUnderlyingType(enumType);
        try
        {
          object obj = Convert.ChangeType((object) value, underlyingType, (IFormatProvider) CultureInfo.InvariantCulture);
          parseResult.parsedEnum = Enum.ToObject(enumType, obj);
          return true;
        }
        catch (FormatException ex)
        {
        }
        catch (Exception ex)
        {
          if (parseResult.canThrow)
          {
            throw;
          }
          else
          {
            parseResult.SetFailure(ex);
            return false;
          }
        }
      }
      string[] strArray1 = value.Split(Enum.enumSeperatorCharArray);
      Enum.ValuesAndNames cachedValuesAndNames = Enum.GetCachedValuesAndNames(enumType1, true);
      string[] strArray2 = cachedValuesAndNames.Names;
      ulong[] numArray = cachedValuesAndNames.Values;
      for (int index1 = 0; index1 < strArray1.Length; ++index1)
      {
        strArray1[index1] = strArray1[index1].Trim();
        bool flag = false;
        for (int index2 = 0; index2 < strArray2.Length; ++index2)
        {
          if (ignoreCase)
          {
            if (string.Compare(strArray2[index2], strArray1[index1], StringComparison.OrdinalIgnoreCase) != 0)
              continue;
          }
          else if (!strArray2[index2].Equals(strArray1[index1]))
            continue;
          ulong num2 = numArray[index2];
          num1 |= num2;
          flag = true;
          break;
        }
        if (!flag)
        {
          parseResult.SetFailure(Enum.ParseFailureKind.ArgumentWithParameter, "Arg_EnumValueNotFound", (object) value);
          return false;
        }
      }
      try
      {
        parseResult.parsedEnum = Enum.ToObject(enumType, num1);
        return true;
      }
      catch (Exception ex)
      {
        if (parseResult.canThrow)
        {
          throw;
        }
        else
        {
          parseResult.SetFailure(ex);
          return false;
        }
      }
    }

    /// <summary>返回指定枚举的基础类型。</summary>
    /// <returns>
    /// <paramref name="enumType" /> 的基础类型。</returns>
    /// <param name="enumType">基础类型将被检索的枚举。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="enumType" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
    /// <filterpriority>1</filterpriority>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public static Type GetUnderlyingType(Type enumType)
    {
      if (enumType == (Type) null)
        throw new ArgumentNullException("enumType");
      return enumType.GetEnumUnderlyingType();
    }

    /// <summary>检索指定枚举中常数值的数组。</summary>
    /// <returns>一个数组，其中包含 <paramref name="enumType" /> 中常数的值。</returns>
    /// <param name="enumType">枚举类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="enumType" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
    /// <exception cref="T:System.InvalidOperationException">The method is invoked by reflection in a reflection-only context, -or-<paramref name="enumType" /> is a type from an assembly loaded in a reflection-only context.</exception>
    /// <filterpriority>1</filterpriority>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public static Array GetValues(Type enumType)
    {
      if (enumType == (Type) null)
        throw new ArgumentNullException("enumType");
      return enumType.GetEnumValues();
    }

    internal static ulong[] InternalGetValues(RuntimeType enumType)
    {
      return Enum.GetCachedValuesAndNames(enumType, false).Values;
    }

    /// <summary>在指定枚举中检索具有指定值的常数的名称。</summary>
    /// <returns>一个字符串，其中包含 <paramref name="enumType" /> 中值为 <paramref name="value" /> 的枚举常数的名称；如果没有找到这样的常数，则为 null。</returns>
    /// <param name="enumType">枚举类型。</param>
    /// <param name="value">特定枚举常数的值（根据其基础类型）。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="enumType" /> or <paramref name="value" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.-or- <paramref name="value" /> is neither of type <paramref name="enumType" /> nor does it have the same underlying type as <paramref name="enumType" />. </exception>
    /// <filterpriority>1</filterpriority>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public static string GetName(Type enumType, object value)
    {
      if (enumType == (Type) null)
        throw new ArgumentNullException("enumType");
      return enumType.GetEnumName(value);
    }

    /// <summary>检索指定枚举中常数名称的数组。</summary>
    /// <returns>
    /// <paramref name="enumType" /> 的常数名称的字符串数组。</returns>
    /// <param name="enumType">枚举类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="enumType" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="enumType" /> parameter is not an <see cref="T:System.Enum" />. </exception>
    /// <filterpriority>1</filterpriority>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public static string[] GetNames(Type enumType)
    {
      if (enumType == (Type) null)
        throw new ArgumentNullException("enumType");
      return enumType.GetEnumNames();
    }

    internal static string[] InternalGetNames(RuntimeType enumType)
    {
      return Enum.GetCachedValuesAndNames(enumType, true).Names;
    }

    /// <summary>将具有整数值的指定对象转换为枚举成员。</summary>
    /// <returns>值为 <paramref name="value" /> 的枚举对象。</returns>
    /// <param name="enumType">要返回的枚举类型。</param>
    /// <param name="value">要转换为枚举成员的值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="enumType" /> or <paramref name="value" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.-or- <paramref name="value" /> is not type <see cref="T:System.SByte" />, <see cref="T:System.Int16" />, <see cref="T:System.Int32" />, <see cref="T:System.Int64" />, <see cref="T:System.Byte" />, <see cref="T:System.UInt16" />, <see cref="T:System.UInt32" />, or <see cref="T:System.UInt64" />. </exception>
    /// <filterpriority>1</filterpriority>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public static object ToObject(Type enumType, object value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      TypeCode typeCode = Convert.GetTypeCode(value);
      if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8 && (typeCode == TypeCode.Boolean || typeCode == TypeCode.Char))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnumBaseTypeOrEnum"), "value");
      switch (typeCode)
      {
        case TypeCode.Boolean:
          return Enum.ToObject(enumType, (bool) value);
        case TypeCode.Char:
          return Enum.ToObject(enumType, (char) value);
        case TypeCode.SByte:
          return Enum.ToObject(enumType, (sbyte) value);
        case TypeCode.Byte:
          return Enum.ToObject(enumType, (byte) value);
        case TypeCode.Int16:
          return Enum.ToObject(enumType, (short) value);
        case TypeCode.UInt16:
          return Enum.ToObject(enumType, (ushort) value);
        case TypeCode.Int32:
          return Enum.ToObject(enumType, (int) value);
        case TypeCode.UInt32:
          return Enum.ToObject(enumType, (uint) value);
        case TypeCode.Int64:
          return Enum.ToObject(enumType, (long) value);
        case TypeCode.UInt64:
          return Enum.ToObject(enumType, (ulong) value);
        default:
          throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnumBaseTypeOrEnum"), "value");
      }
    }

    /// <summary>返回指定枚举中是否存在具有指定值的常数的指示。</summary>
    /// <returns>如果 <paramref name="enumType" /> 的某个常数具有等于 <paramref name="value" /> 的值，则为 true；否则为 false。</returns>
    /// <param name="enumType">枚举类型。</param>
    /// <param name="value">
    /// <paramref name="enumType" /> 的常数的值或名称。 </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="enumType" /> or <paramref name="value" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="enumType" /> is not an Enum.-or- The type of <paramref name="value" /> is an enumeration, but it is not an enumeration of type <paramref name="enumType" />.-or- The type of <paramref name="value" /> is not an underlying type of <paramref name="enumType" />. </exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="value" /> is not type <see cref="T:System.SByte" />, <see cref="T:System.Int16" />, <see cref="T:System.Int32" />, <see cref="T:System.Int64" />, <see cref="T:System.Byte" />, <see cref="T:System.UInt16" />, <see cref="T:System.UInt32" />, or <see cref="T:System.UInt64" />, or <see cref="T:System.String" />. </exception>
    /// <filterpriority>1</filterpriority>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public static bool IsDefined(Type enumType, object value)
    {
      if (enumType == (Type) null)
        throw new ArgumentNullException("enumType");
      return enumType.IsEnumDefined(value);
    }

    /// <summary>根据指定格式将指定枚举类型的指定值转换为其等效的字符串表示形式。</summary>
    /// <returns>
    /// <paramref name="value" /> 的字符串表示形式。</returns>
    /// <param name="enumType">要转换的值的枚举类型。</param>
    /// <param name="value">要转换的值。</param>
    /// <param name="format">要使用的输出格式。</param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="enumType" />, <paramref name="value" />, or <paramref name="format" /> parameter is null. </exception>
    /// <exception cref="T:System.ArgumentException">The <paramref name="enumType" /> parameter is not an <see cref="T:System.Enum" /> type.-or- The <paramref name="value" /> is from an enumeration that differs in type from <paramref name="enumType" />.-or- The type of <paramref name="value" /> is not an underlying type of <paramref name="enumType" />. </exception>
    /// <exception cref="T:System.FormatException">The <paramref name="format" /> parameter contains an invalid value. </exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="format" /> equals "X", but the enumeration type is unknown.</exception>
    /// <filterpriority>1</filterpriority>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public static string Format(Type enumType, object value, string format)
    {
      if (enumType == (Type) null)
        throw new ArgumentNullException("enumType");
      if (!enumType.IsEnum)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
      if (value == null)
        throw new ArgumentNullException("value");
      if (format == null)
        throw new ArgumentNullException("format");
      RuntimeType eT = enumType as RuntimeType;
      if (eT == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "enumType");
      Type type = value.GetType();
      Type underlyingType = Enum.GetUnderlyingType(enumType);
      if (type.IsEnum)
      {
        Enum.GetUnderlyingType(type);
        if (!type.IsEquivalentTo(enumType))
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumAndObjectMustBeSameType", (object) type.ToString(), (object) enumType.ToString()));
        value = ((Enum) value).GetValue();
      }
      else if (type != underlyingType)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumFormatUnderlyingTypeAndObjectMustBeSameType", (object) type.ToString(), (object) underlyingType.ToString()));
      if (format.Length != 1)
        throw new FormatException(Environment.GetResourceString("Format_InvalidEnumFormatSpecification"));
      switch (format[0])
      {
        case 'D':
        case 'd':
          return value.ToString();
        case 'X':
        case 'x':
          return Enum.InternalFormattedHexString(value);
        case 'G':
        case 'g':
          return Enum.InternalFormat(eT, value);
        case 'F':
        case 'f':
          return Enum.InternalFlagsFormat(eT, value);
        default:
          throw new FormatException(Environment.GetResourceString("Format_InvalidEnumFormatSpecification"));
      }
    }

    [SecuritySafeCritical]
    internal unsafe object GetValue()
    {
      fixed (byte* numPtr = &JitHelpers.GetPinningHelper((object) this).m_data)
      {
        switch (this.InternalGetCorElementType())
        {
          case CorElementType.Boolean:
            return (object) (bool) *numPtr;
          case CorElementType.Char:
            return (object) (char) *(ushort*) numPtr;
          case CorElementType.I1:
            return (object) *(sbyte*) numPtr;
          case CorElementType.U1:
            return (object) *numPtr;
          case CorElementType.I2:
            return (object) *(short*) numPtr;
          case CorElementType.U2:
            return (object) *(ushort*) numPtr;
          case CorElementType.I4:
            return (object) *(int*) numPtr;
          case CorElementType.U4:
            return (object) *(uint*) numPtr;
          case CorElementType.I8:
            return (object) *(long*) numPtr;
          case CorElementType.U8:
            return (object) (ulong) *(long*) numPtr;
          case CorElementType.R4:
            return (object) *(float*) numPtr;
          case CorElementType.R8:
            return (object) *(double*) numPtr;
          case CorElementType.I:
            return (object) *(IntPtr*) numPtr;
          case CorElementType.U:
            return (object) (UIntPtr) *(IntPtr*) numPtr;
          default:
            return (object) null;
        }
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private bool InternalHasFlag(Enum flags);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private CorElementType InternalGetCorElementType();

    /// <summary>返回一个值，该值指示此实例是否等于指定的对象。</summary>
    /// <returns>如果 <paramref name="obj" /> 是相同类型的枚举值并且使用的基础值与此实例相同，则为 true；否则为 false。</returns>
    /// <param name="obj">与此实例进行比较的对象，或为 null。 </param>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public override bool Equals(object obj);

    /// <summary>返回该实例的值的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override unsafe int GetHashCode()
    {
      fixed (byte* numPtr = &JitHelpers.GetPinningHelper((object) this).m_data)
      {
        switch (this.InternalGetCorElementType())
        {
          case CorElementType.Boolean:
            return ((bool*) numPtr)->GetHashCode();
          case CorElementType.Char:
            return ((char*) numPtr)->GetHashCode();
          case CorElementType.I1:
            return ((sbyte*) numPtr)->GetHashCode();
          case CorElementType.U1:
            return numPtr->GetHashCode();
          case CorElementType.I2:
            return ((short*) numPtr)->GetHashCode();
          case CorElementType.U2:
            return ((ushort*) numPtr)->GetHashCode();
          case CorElementType.I4:
            return ((int*) numPtr)->GetHashCode();
          case CorElementType.U4:
            return ((uint*) numPtr)->GetHashCode();
          case CorElementType.I8:
            return ((long*) numPtr)->GetHashCode();
          case CorElementType.U8:
            return ((ulong*) numPtr)->GetHashCode();
          case CorElementType.R4:
            return ((float*) numPtr)->GetHashCode();
          case CorElementType.R8:
            return ((double*) numPtr)->GetHashCode();
          case CorElementType.I:
            return numPtr->GetHashCode();
          case CorElementType.U:
            return numPtr->GetHashCode();
          default:
            return 0;
        }
      }
    }

    /// <summary>将此实例的值转换为其等效的字符串表示形式。</summary>
    /// <returns>此实例的值的字符串表示形式。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return Enum.InternalFormat((RuntimeType) this.GetType(), this.GetValue());
    }

    /// <summary>此方法重载已过时；请使用 <see cref="M:System.Enum.ToString(System.String)" />。</summary>
    /// <returns>此实例的值的字符串表示形式，由 <paramref name="format" /> 指定。</returns>
    /// <param name="format">格式规范。</param>
    /// <param name="provider">（已过时。）</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> does not contain a valid format specification. </exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="format" /> equals "X", but the enumeration type is unknown.</exception>
    /// <filterpriority>2</filterpriority>
    [Obsolete("The provider argument is not used. Please use ToString(String).")]
    public string ToString(string format, IFormatProvider provider)
    {
      return this.ToString(format);
    }

    /// <summary>将此实例与指定对象进行比较并返回一个对二者的相对值的指示。</summary>
    /// <returns>一个有符号数字，用于指示此实例和 <paramref name="target" /> 的相对值。值含义小于零此实例的值小于 <paramref name="target" /> 的值。零此实例的值等于 <paramref name="target" /> 的值。大于零此实例的值大于 <paramref name="target" /> 的值。- 或 - <paramref name="target" /> 为 null。</returns>
    /// <param name="target">要比较的对象，或为 null。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> and this instance are not the same type. </exception>
    /// <exception cref="T:System.InvalidOperationException">This instance is not type <see cref="T:System.SByte" />, <see cref="T:System.Int16" />, <see cref="T:System.Int32" />, <see cref="T:System.Int64" />, <see cref="T:System.Byte" />, <see cref="T:System.UInt16" />, <see cref="T:System.UInt32" />, or <see cref="T:System.UInt64" />. </exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public int CompareTo(object target)
    {
      if (this == null)
        throw new NullReferenceException();
      int num = Enum.InternalCompareTo((object) this, target);
      if (num < 2)
        return num;
      if (num == 2)
      {
        Type type = this.GetType();
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumAndObjectMustBeSameType", (object) target.GetType().ToString(), (object) type.ToString()));
      }
      throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_UnknownEnumType"));
    }

    /// <summary>使用指定格式将此实例的值转换成其等效的字符串表示。</summary>
    /// <returns>此实例的值的字符串表示形式，由 <paramref name="format" /> 指定。</returns>
    /// <param name="format">一个格式字符串。</param>
    /// <exception cref="T:System.FormatException">
    /// <paramref name="format" /> contains an invalid specification. </exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="format" /> equals "X", but the enumeration type is unknown.</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public string ToString(string format)
    {
      if (format == null || format.Length == 0)
        format = "G";
      if (string.Compare(format, "G", StringComparison.OrdinalIgnoreCase) == 0)
        return this.ToString();
      if (string.Compare(format, "D", StringComparison.OrdinalIgnoreCase) == 0)
        return this.GetValue().ToString();
      if (string.Compare(format, "X", StringComparison.OrdinalIgnoreCase) == 0)
        return Enum.InternalFormattedHexString(this.GetValue());
      if (string.Compare(format, "F", StringComparison.OrdinalIgnoreCase) == 0)
        return Enum.InternalFlagsFormat((RuntimeType) this.GetType(), this.GetValue());
      throw new FormatException(Environment.GetResourceString("Format_InvalidEnumFormatSpecification"));
    }

    /// <summary>此方法重载已过时；请使用 <see cref="M:System.Enum.ToString" />。</summary>
    /// <returns>此实例的值的字符串表示形式。</returns>
    /// <param name="provider">（已过时）</param>
    /// <filterpriority>2</filterpriority>
    [Obsolete("The provider argument is not used. Please use ToString().")]
    public string ToString(IFormatProvider provider)
    {
      return this.ToString();
    }

    /// <summary>确定当前实例中是否设置了一个或多个位域。</summary>
    /// <returns>如果在 <paramref name="flag" /> 中设置的位域也在当前实例中进行了设置，则为 true；否则为 false。</returns>
    /// <param name="flag">一个枚举值。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="flag" /> is a different type than the current instance.</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public bool HasFlag(Enum flag)
    {
      if (flag == null)
        throw new ArgumentNullException("flag");
      if (!this.GetType().IsEquivalentTo(flag.GetType()))
        throw new ArgumentException(Environment.GetResourceString("Argument_EnumTypeDoesNotMatch", (object) flag.GetType(), (object) this.GetType()));
      return this.InternalHasFlag(flag);
    }

    /// <summary>返回此实例的基础 <see cref="T:System.TypeCode" />。</summary>
    /// <returns>此实例的类型。</returns>
    /// <exception cref="T:System.InvalidOperationException">The enumeration type is unknown.</exception>
    /// <filterpriority>2</filterpriority>
    public TypeCode GetTypeCode()
    {
      Type underlyingType = Enum.GetUnderlyingType(this.GetType());
      if (underlyingType == typeof (int))
        return TypeCode.Int32;
      if (underlyingType == typeof (sbyte))
        return TypeCode.SByte;
      if (underlyingType == typeof (short))
        return TypeCode.Int16;
      if (underlyingType == typeof (long))
        return TypeCode.Int64;
      if (underlyingType == typeof (uint))
        return TypeCode.UInt32;
      if (underlyingType == typeof (byte))
        return TypeCode.Byte;
      if (underlyingType == typeof (ushort))
        return TypeCode.UInt16;
      if (underlyingType == typeof (ulong))
        return TypeCode.UInt64;
      if (underlyingType == typeof (bool))
        return TypeCode.Boolean;
      if (underlyingType == typeof (char))
        return TypeCode.Char;
      throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_UnknownEnumType"));
    }

    [__DynamicallyInvokable]
    bool IConvertible.ToBoolean(IFormatProvider provider)
    {
      return Convert.ToBoolean(this.GetValue(), (IFormatProvider) CultureInfo.CurrentCulture);
    }

    [__DynamicallyInvokable]
    char IConvertible.ToChar(IFormatProvider provider)
    {
      return Convert.ToChar(this.GetValue(), (IFormatProvider) CultureInfo.CurrentCulture);
    }

    [__DynamicallyInvokable]
    sbyte IConvertible.ToSByte(IFormatProvider provider)
    {
      return Convert.ToSByte(this.GetValue(), (IFormatProvider) CultureInfo.CurrentCulture);
    }

    [__DynamicallyInvokable]
    byte IConvertible.ToByte(IFormatProvider provider)
    {
      return Convert.ToByte(this.GetValue(), (IFormatProvider) CultureInfo.CurrentCulture);
    }

    [__DynamicallyInvokable]
    short IConvertible.ToInt16(IFormatProvider provider)
    {
      return Convert.ToInt16(this.GetValue(), (IFormatProvider) CultureInfo.CurrentCulture);
    }

    [__DynamicallyInvokable]
    ushort IConvertible.ToUInt16(IFormatProvider provider)
    {
      return Convert.ToUInt16(this.GetValue(), (IFormatProvider) CultureInfo.CurrentCulture);
    }

    [__DynamicallyInvokable]
    int IConvertible.ToInt32(IFormatProvider provider)
    {
      return Convert.ToInt32(this.GetValue(), (IFormatProvider) CultureInfo.CurrentCulture);
    }

    [__DynamicallyInvokable]
    uint IConvertible.ToUInt32(IFormatProvider provider)
    {
      return Convert.ToUInt32(this.GetValue(), (IFormatProvider) CultureInfo.CurrentCulture);
    }

    [__DynamicallyInvokable]
    long IConvertible.ToInt64(IFormatProvider provider)
    {
      return Convert.ToInt64(this.GetValue(), (IFormatProvider) CultureInfo.CurrentCulture);
    }

    [__DynamicallyInvokable]
    ulong IConvertible.ToUInt64(IFormatProvider provider)
    {
      return Convert.ToUInt64(this.GetValue(), (IFormatProvider) CultureInfo.CurrentCulture);
    }

    [__DynamicallyInvokable]
    float IConvertible.ToSingle(IFormatProvider provider)
    {
      return Convert.ToSingle(this.GetValue(), (IFormatProvider) CultureInfo.CurrentCulture);
    }

    [__DynamicallyInvokable]
    double IConvertible.ToDouble(IFormatProvider provider)
    {
      return Convert.ToDouble(this.GetValue(), (IFormatProvider) CultureInfo.CurrentCulture);
    }

    [__DynamicallyInvokable]
    Decimal IConvertible.ToDecimal(IFormatProvider provider)
    {
      return Convert.ToDecimal(this.GetValue(), (IFormatProvider) CultureInfo.CurrentCulture);
    }

    [__DynamicallyInvokable]
    DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
      throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", (object) "Enum", (object) "DateTime"));
    }

    [__DynamicallyInvokable]
    object IConvertible.ToType(Type type, IFormatProvider provider)
    {
      return Convert.DefaultToType((IConvertible) this, type, provider);
    }

    /// <summary>将指定的 8 位有符号整数值转换为枚举成员。</summary>
    /// <returns>设置为 <paramref name="value" /> 的枚举的实例。</returns>
    /// <param name="enumType">要返回的枚举类型。</param>
    /// <param name="value">要转换为枚举成员的值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="enumType" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    [ComVisible(true)]
    public static object ToObject(Type enumType, sbyte value)
    {
      if (enumType == (Type) null)
        throw new ArgumentNullException("enumType");
      if (!enumType.IsEnum)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
      RuntimeType enumType1 = enumType as RuntimeType;
      // ISSUE: variable of the null type
      __Null local = null;
      if (enumType1 == (RuntimeType) local)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "enumType");
      long num = (long) value;
      return Enum.InternalBoxEnum(enumType1, num);
    }

    /// <summary>将指定的 16 位有符号整数转换为枚举成员。</summary>
    /// <returns>设置为 <paramref name="value" /> 的枚举的实例。</returns>
    /// <param name="enumType">要返回的枚举类型。</param>
    /// <param name="value">要转换为枚举成员的值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="enumType" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public static object ToObject(Type enumType, short value)
    {
      if (enumType == (Type) null)
        throw new ArgumentNullException("enumType");
      if (!enumType.IsEnum)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
      RuntimeType enumType1 = enumType as RuntimeType;
      // ISSUE: variable of the null type
      __Null local = null;
      if (enumType1 == (RuntimeType) local)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "enumType");
      long num = (long) value;
      return Enum.InternalBoxEnum(enumType1, num);
    }

    /// <summary>将指定的 32 位有符号整数转换为枚举成员。</summary>
    /// <returns>设置为 <paramref name="value" /> 的枚举的实例。</returns>
    /// <param name="enumType">要返回的枚举类型。</param>
    /// <param name="value">要转换为枚举成员的值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="enumType" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public static object ToObject(Type enumType, int value)
    {
      if (enumType == (Type) null)
        throw new ArgumentNullException("enumType");
      if (!enumType.IsEnum)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
      RuntimeType enumType1 = enumType as RuntimeType;
      // ISSUE: variable of the null type
      __Null local = null;
      if (enumType1 == (RuntimeType) local)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "enumType");
      long num = (long) value;
      return Enum.InternalBoxEnum(enumType1, num);
    }

    /// <summary>将指定的 8 位无符号整数转换为枚举成员。</summary>
    /// <returns>设置为 <paramref name="value" /> 的枚举的实例。</returns>
    /// <param name="enumType">要返回的枚举类型。</param>
    /// <param name="value">要转换为枚举成员的值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="enumType" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public static object ToObject(Type enumType, byte value)
    {
      if (enumType == (Type) null)
        throw new ArgumentNullException("enumType");
      if (!enumType.IsEnum)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
      RuntimeType enumType1 = enumType as RuntimeType;
      // ISSUE: variable of the null type
      __Null local = null;
      if (enumType1 == (RuntimeType) local)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "enumType");
      long num = (long) value;
      return Enum.InternalBoxEnum(enumType1, num);
    }

    /// <summary>将指定的 16 位无符号整数值转换为枚举成员。</summary>
    /// <returns>设置为 <paramref name="value" /> 的枚举的实例。</returns>
    /// <param name="enumType">要返回的枚举类型。</param>
    /// <param name="value">要转换为枚举成员的值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="enumType" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    [ComVisible(true)]
    public static object ToObject(Type enumType, ushort value)
    {
      if (enumType == (Type) null)
        throw new ArgumentNullException("enumType");
      if (!enumType.IsEnum)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
      RuntimeType enumType1 = enumType as RuntimeType;
      // ISSUE: variable of the null type
      __Null local = null;
      if (enumType1 == (RuntimeType) local)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "enumType");
      long num = (long) value;
      return Enum.InternalBoxEnum(enumType1, num);
    }

    /// <summary>将指定的 32 位无符号整数值转换为枚举成员。</summary>
    /// <returns>设置为 <paramref name="value" /> 的枚举的实例。</returns>
    /// <param name="enumType">要返回的枚举类型。</param>
    /// <param name="value">要转换为枚举成员的值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="enumType" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    [ComVisible(true)]
    public static object ToObject(Type enumType, uint value)
    {
      if (enumType == (Type) null)
        throw new ArgumentNullException("enumType");
      if (!enumType.IsEnum)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
      RuntimeType enumType1 = enumType as RuntimeType;
      // ISSUE: variable of the null type
      __Null local = null;
      if (enumType1 == (RuntimeType) local)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "enumType");
      long num = (long) value;
      return Enum.InternalBoxEnum(enumType1, num);
    }

    /// <summary>将指定的 64 位有符号整数转换为枚举成员。</summary>
    /// <returns>设置为 <paramref name="value" /> 的枚举的实例。</returns>
    /// <param name="enumType">要返回的枚举类型。</param>
    /// <param name="value">要转换为枚举成员的值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="enumType" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public static object ToObject(Type enumType, long value)
    {
      if (enumType == (Type) null)
        throw new ArgumentNullException("enumType");
      if (!enumType.IsEnum)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
      RuntimeType enumType1 = enumType as RuntimeType;
      // ISSUE: variable of the null type
      __Null local = null;
      if (enumType1 == (RuntimeType) local)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "enumType");
      long num = value;
      return Enum.InternalBoxEnum(enumType1, num);
    }

    /// <summary>将指定的 64 位无符号整数值转换为枚举成员。</summary>
    /// <returns>设置为 <paramref name="value" /> 的枚举的实例。</returns>
    /// <param name="enumType">要返回的枚举类型。</param>
    /// <param name="value">要转换为枚举成员的值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="enumType" /> is null. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="enumType" /> is not an <see cref="T:System.Enum" />. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    [ComVisible(true)]
    public static object ToObject(Type enumType, ulong value)
    {
      if (enumType == (Type) null)
        throw new ArgumentNullException("enumType");
      if (!enumType.IsEnum)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
      RuntimeType enumType1 = enumType as RuntimeType;
      // ISSUE: variable of the null type
      __Null local = null;
      if (enumType1 == (RuntimeType) local)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "enumType");
      long num = (long) value;
      return Enum.InternalBoxEnum(enumType1, num);
    }

    [SecuritySafeCritical]
    private static object ToObject(Type enumType, char value)
    {
      if (enumType == (Type) null)
        throw new ArgumentNullException("enumType");
      if (!enumType.IsEnum)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
      RuntimeType enumType1 = enumType as RuntimeType;
      // ISSUE: variable of the null type
      __Null local = null;
      if (enumType1 == (RuntimeType) local)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "enumType");
      long num = (long) value;
      return Enum.InternalBoxEnum(enumType1, num);
    }

    [SecuritySafeCritical]
    private static object ToObject(Type enumType, bool value)
    {
      if (enumType == (Type) null)
        throw new ArgumentNullException("enumType");
      if (!enumType.IsEnum)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
      RuntimeType enumType1 = enumType as RuntimeType;
      // ISSUE: variable of the null type
      __Null local = null;
      if (enumType1 == (RuntimeType) local)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "enumType");
      long num = value ? 1L : 0L;
      return Enum.InternalBoxEnum(enumType1, num);
    }

    private enum ParseFailureKind
    {
      None,
      Argument,
      ArgumentNull,
      ArgumentWithParameter,
      UnhandledException,
    }

    private struct EnumResult
    {
      internal object parsedEnum;
      internal bool canThrow;
      internal Enum.ParseFailureKind m_failure;
      internal string m_failureMessageID;
      internal string m_failureParameter;
      internal object m_failureMessageFormatArgument;
      internal Exception m_innerException;

      internal void Init(bool canMethodThrow)
      {
        this.parsedEnum = (object) 0;
        this.canThrow = canMethodThrow;
      }

      internal void SetFailure(Exception unhandledException)
      {
        this.m_failure = Enum.ParseFailureKind.UnhandledException;
        this.m_innerException = unhandledException;
      }

      internal void SetFailure(Enum.ParseFailureKind failure, string failureParameter)
      {
        this.m_failure = failure;
        this.m_failureParameter = failureParameter;
        if (this.canThrow)
          throw this.GetEnumParseException();
      }

      internal void SetFailure(Enum.ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument)
      {
        this.m_failure = failure;
        this.m_failureMessageID = failureMessageID;
        this.m_failureMessageFormatArgument = failureMessageFormatArgument;
        if (this.canThrow)
          throw this.GetEnumParseException();
      }

      internal Exception GetEnumParseException()
      {
        switch (this.m_failure)
        {
          case Enum.ParseFailureKind.Argument:
            return (Exception) new ArgumentException(Environment.GetResourceString(this.m_failureMessageID));
          case Enum.ParseFailureKind.ArgumentNull:
            return (Exception) new ArgumentNullException(this.m_failureParameter);
          case Enum.ParseFailureKind.ArgumentWithParameter:
            return (Exception) new ArgumentException(Environment.GetResourceString(this.m_failureMessageID, this.m_failureMessageFormatArgument));
          case Enum.ParseFailureKind.UnhandledException:
            return this.m_innerException;
          default:
            return (Exception) new ArgumentException(Environment.GetResourceString("Arg_EnumValueNotFound"));
        }
      }
    }

    private class ValuesAndNames
    {
      public ulong[] Values;
      public string[] Names;

      public ValuesAndNames(ulong[] values, string[] names)
      {
        this.Values = values;
        this.Names = names;
      }
    }
  }
}
