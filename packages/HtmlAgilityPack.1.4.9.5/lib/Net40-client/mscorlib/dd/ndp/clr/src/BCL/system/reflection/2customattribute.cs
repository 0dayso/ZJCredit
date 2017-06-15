// Decompiled with JetBrains decompiler
// Type: System.Reflection.CustomAttributeTypedArgument
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
  /// <summary>表示只反射上下文中的自定义特性的参数，或数组参数的元素。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct CustomAttributeTypedArgument
  {
    private object m_value;
    private Type m_argumentType;

    /// <summary>获取参数或数组参数元素的类型。</summary>
    /// <returns>一个 <see cref="T:System.Type" /> 对象，表示参数或数组元素的类型。</returns>
    [__DynamicallyInvokable]
    public Type ArgumentType
    {
      [__DynamicallyInvokable] get
      {
        return this.m_argumentType;
      }
    }

    /// <summary>获取简单参数或数组参数的元素的参数值；获取数组参数的值的集合。</summary>
    /// <returns>一个表示参数或元素的值的对象，或表示数组类型参数的值的 <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> 对象的一个泛型 <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />。</returns>
    [__DynamicallyInvokable]
    public object Value
    {
      [__DynamicallyInvokable] get
      {
        return this.m_value;
      }
    }

    /// <summary>用指定的类型和值初始化 <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> 类的新实例。</summary>
    /// <param name="argumentType">自定义特性参数的类型。</param>
    /// <param name="value">自定义特性参数的值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="argumentType" /> 为 null。</exception>
    public CustomAttributeTypedArgument(Type argumentType, object value)
    {
      if (argumentType == (Type) null)
        throw new ArgumentNullException("argumentType");
      this.m_value = value == null ? (object) null : CustomAttributeTypedArgument.CanonicalizeValue(value);
      this.m_argumentType = argumentType;
    }

    /// <summary>使用指定的值初始化 <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> 类的新实例。</summary>
    /// <param name="value">自定义特性参数的值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    public CustomAttributeTypedArgument(object value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      this.m_value = CustomAttributeTypedArgument.CanonicalizeValue(value);
      this.m_argumentType = value.GetType();
    }

    internal CustomAttributeTypedArgument(RuntimeModule scope, CustomAttributeEncodedArgument encodedArg)
    {
      CustomAttributeEncoding encodedType = encodedArg.CustomAttributeType.EncodedType;
      switch (encodedType)
      {
        case CustomAttributeEncoding.Undefined:
          throw new ArgumentException("encodedArg");
        case CustomAttributeEncoding.Enum:
          RuntimeModule scope1 = scope;
          CustomAttributeType customAttributeType = encodedArg.CustomAttributeType;
          string enumName = customAttributeType.EnumName;
          this.m_argumentType = (Type) CustomAttributeTypedArgument.ResolveType(scope1, enumName);
          long primitiveValue = encodedArg.PrimitiveValue;
          customAttributeType = encodedArg.CustomAttributeType;
          int num = (int) customAttributeType.EncodedEnumType;
          this.m_value = CustomAttributeTypedArgument.EncodedValueToRawValue(primitiveValue, (CustomAttributeEncoding) num);
          break;
        case CustomAttributeEncoding.String:
          this.m_argumentType = typeof (string);
          this.m_value = (object) encodedArg.StringValue;
          break;
        case CustomAttributeEncoding.Type:
          this.m_argumentType = typeof (Type);
          this.m_value = (object) null;
          if (encodedArg.StringValue == null)
            break;
          this.m_value = (object) CustomAttributeTypedArgument.ResolveType(scope, encodedArg.StringValue);
          break;
        case CustomAttributeEncoding.Array:
          CustomAttributeEncoding encodedArrayType = encodedArg.CustomAttributeType.EncodedArrayType;
          this.m_argumentType = (encodedArrayType != CustomAttributeEncoding.Enum ? CustomAttributeTypedArgument.CustomAttributeEncodingToType(encodedArrayType) : (Type) CustomAttributeTypedArgument.ResolveType(scope, encodedArg.CustomAttributeType.EnumName)).MakeArrayType();
          if (encodedArg.ArrayValue == null)
          {
            this.m_value = (object) null;
            break;
          }
          CustomAttributeTypedArgument[] array = new CustomAttributeTypedArgument[encodedArg.ArrayValue.Length];
          for (int index = 0; index < array.Length; ++index)
            array[index] = new CustomAttributeTypedArgument(scope, encodedArg.ArrayValue[index]);
          this.m_value = (object) Array.AsReadOnly<CustomAttributeTypedArgument>(array);
          break;
        default:
          this.m_argumentType = CustomAttributeTypedArgument.CustomAttributeEncodingToType(encodedType);
          this.m_value = CustomAttributeTypedArgument.EncodedValueToRawValue(encodedArg.PrimitiveValue, encodedType);
          break;
      }
    }

    /// <summary>测试两个 <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> 结构是否相等。</summary>
    /// <returns>如果两个 <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> 结构相等，则为 true；否则为 false。</returns>
    /// <param name="left">相等运算符左侧的 <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> 结构。</param>
    /// <param name="right">相等运算符右侧的 <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> 结构。</param>
    public static bool operator ==(CustomAttributeTypedArgument left, CustomAttributeTypedArgument right)
    {
      return left.Equals((object) right);
    }

    /// <summary>测试两个 <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> 结构是否不同。</summary>
    /// <returns>如果两个 <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> 结构不同，则为 true；否则为 false。</returns>
    /// <param name="left">不等运算符左侧的 <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> 结构。</param>
    /// <param name="right">不等运算符右侧的 <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> 结构。</param>
    public static bool operator !=(CustomAttributeTypedArgument left, CustomAttributeTypedArgument right)
    {
      return !left.Equals((object) right);
    }

    private static Type CustomAttributeEncodingToType(CustomAttributeEncoding encodedType)
    {
      switch (encodedType)
      {
        case CustomAttributeEncoding.Type:
          return typeof (Type);
        case CustomAttributeEncoding.Object:
          return typeof (object);
        case CustomAttributeEncoding.Enum:
          return typeof (Enum);
        case CustomAttributeEncoding.Boolean:
          return typeof (bool);
        case CustomAttributeEncoding.Char:
          return typeof (char);
        case CustomAttributeEncoding.SByte:
          return typeof (sbyte);
        case CustomAttributeEncoding.Byte:
          return typeof (byte);
        case CustomAttributeEncoding.Int16:
          return typeof (short);
        case CustomAttributeEncoding.UInt16:
          return typeof (ushort);
        case CustomAttributeEncoding.Int32:
          return typeof (int);
        case CustomAttributeEncoding.UInt32:
          return typeof (uint);
        case CustomAttributeEncoding.Int64:
          return typeof (long);
        case CustomAttributeEncoding.UInt64:
          return typeof (ulong);
        case CustomAttributeEncoding.Float:
          return typeof (float);
        case CustomAttributeEncoding.Double:
          return typeof (double);
        case CustomAttributeEncoding.String:
          return typeof (string);
        case CustomAttributeEncoding.Array:
          return typeof (Array);
        default:
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) encodedType), "encodedType");
      }
    }

    [SecuritySafeCritical]
    private static unsafe object EncodedValueToRawValue(long val, CustomAttributeEncoding encodedType)
    {
      switch (encodedType)
      {
        case CustomAttributeEncoding.Boolean:
          return (object) ((uint) (byte) val > 0U);
        case CustomAttributeEncoding.Char:
          return (object) (char) val;
        case CustomAttributeEncoding.SByte:
          return (object) (sbyte) val;
        case CustomAttributeEncoding.Byte:
          return (object) (byte) val;
        case CustomAttributeEncoding.Int16:
          return (object) (short) val;
        case CustomAttributeEncoding.UInt16:
          return (object) (ushort) val;
        case CustomAttributeEncoding.Int32:
          return (object) (int) val;
        case CustomAttributeEncoding.UInt32:
          return (object) (uint) val;
        case CustomAttributeEncoding.Int64:
          return (object) val;
        case CustomAttributeEncoding.UInt64:
          return (object) (ulong) val;
        case CustomAttributeEncoding.Float:
          return (object) *(float*) &val;
        case CustomAttributeEncoding.Double:
          return (object) *(double*) &val;
        default:
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) (int) val), "val");
      }
    }

    private static RuntimeType ResolveType(RuntimeModule scope, string typeName)
    {
      RuntimeType nameUsingCaRules = RuntimeTypeHandle.GetTypeByNameUsingCARules(typeName, scope);
      // ISSUE: variable of the null type
      __Null local = null;
      if (!(nameUsingCaRules == (RuntimeType) local))
        return nameUsingCaRules;
      throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, Environment.GetResourceString("Arg_CATypeResolutionFailed"), (object) typeName));
    }

    private static object CanonicalizeValue(object value)
    {
      if (value.GetType().IsEnum)
        return ((Enum) value).GetValue();
      return value;
    }

    /// <summary>返回一个由参数名称、等号和参数值的字符串表示形式组成的字符串。</summary>
    /// <returns>一个由参数名称、等号和参数值的字符串表示形式组成的字符串。</returns>
    public override string ToString()
    {
      return this.ToString(false);
    }

    internal string ToString(bool typed)
    {
      if (this.m_argumentType == (Type) null)
        return base.ToString();
      if (this.ArgumentType.IsEnum)
        return string.Format((IFormatProvider) CultureInfo.CurrentCulture, typed ? "{0}" : "({1}){0}", this.Value, (object) this.ArgumentType.FullName);
      if (this.Value == null)
        return string.Format((IFormatProvider) CultureInfo.CurrentCulture, typed ? "null" : "({0})null", (object) this.ArgumentType.Name);
      if (this.ArgumentType == typeof (string))
        return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "\"{0}\"", this.Value);
      if (this.ArgumentType == typeof (char))
        return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "'{0}'", this.Value);
      if (this.ArgumentType == typeof (Type))
        return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "typeof({0})", (object) ((Type) this.Value).FullName);
      if (!this.ArgumentType.IsArray)
        return string.Format((IFormatProvider) CultureInfo.CurrentCulture, typed ? "{0}" : "({1}){0}", this.Value, (object) this.ArgumentType.Name);
      string str1 = (string) null;
      IList<CustomAttributeTypedArgument> attributeTypedArgumentList = this.Value as IList<CustomAttributeTypedArgument>;
      Type elementType = this.ArgumentType.GetElementType();
      string str2 = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "new {0}[{1}] {{ ", elementType.IsEnum ? (object) elementType.FullName : (object) elementType.Name, (object) attributeTypedArgumentList.Count);
      for (int index = 0; index < attributeTypedArgumentList.Count; ++index)
        str2 += string.Format((IFormatProvider) CultureInfo.CurrentCulture, index == 0 ? "{0}" : ", {0}", (object) attributeTypedArgumentList[index].ToString(elementType != typeof (object)));
      return str1 = str2 + " }";
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    /// <returns>如果 <paramref name="obj" /> 和该实例具有相同的类型并表示相同的值，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前实例进行比较的对象。</param>
    public override bool Equals(object obj)
    {
      return obj == (ValueType) this;
    }
  }
}
