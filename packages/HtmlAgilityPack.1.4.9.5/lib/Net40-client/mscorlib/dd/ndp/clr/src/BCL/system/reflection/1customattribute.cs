// Decompiled with JetBrains decompiler
// Type: System.Reflection.CustomAttributeNamedArgument
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>表示只反射上下文中自定义特性的命名参数。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct CustomAttributeNamedArgument
  {
    private MemberInfo m_memberInfo;
    private CustomAttributeTypedArgument m_value;

    internal Type ArgumentType
    {
      get
      {
        if (!(this.m_memberInfo is FieldInfo))
          return ((PropertyInfo) this.m_memberInfo).PropertyType;
        return ((FieldInfo) this.m_memberInfo).FieldType;
      }
    }

    /// <summary>获取将用于设置命名参数的特性成员。</summary>
    /// <returns>将用于设置命名参数的特性成员。</returns>
    public MemberInfo MemberInfo
    {
      get
      {
        return this.m_memberInfo;
      }
    }

    /// <summary>获取一个 <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> 结构，该结构可用于获取当前命名参数的类型和值。</summary>
    /// <returns>一个结构，可用于获取当前命名参数的类型和值。</returns>
    [__DynamicallyInvokable]
    public CustomAttributeTypedArgument TypedValue
    {
      [__DynamicallyInvokable] get
      {
        return this.m_value;
      }
    }

    /// <summary>获取将用于设置命名参数的特性成员名称。</summary>
    /// <returns>用于设置命名参数的特性成员的名称。</returns>
    [__DynamicallyInvokable]
    public string MemberName
    {
      [__DynamicallyInvokable] get
      {
        return this.MemberInfo.Name;
      }
    }

    /// <summary>获取一个值，该值指示命名参数是否是一个字段。</summary>
    /// <returns>如果命名参数为字段，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsField
    {
      [__DynamicallyInvokable] get
      {
        return this.MemberInfo is FieldInfo;
      }
    }

    /// <summary>初始化 <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> 类（它表示自定义特性的指定字段或属性）的新实例，并指定字段或属性的值。</summary>
    /// <param name="memberInfo">自定义特性的字段或属性。新的 <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> 对象表示此成员及其值。</param>
    /// <param name="value">自定义特性的字段或属性的值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="memberInfo" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="memberInfo" /> 不是自定义特性的字段或属性。</exception>
    public CustomAttributeNamedArgument(MemberInfo memberInfo, object value)
    {
      if (memberInfo == (MemberInfo) null)
        throw new ArgumentNullException("memberInfo");
      FieldInfo fieldInfo = memberInfo as FieldInfo;
      PropertyInfo propertyInfo = memberInfo as PropertyInfo;
      Type argumentType;
      if (fieldInfo != (FieldInfo) null)
      {
        argumentType = fieldInfo.FieldType;
      }
      else
      {
        if (!(propertyInfo != (PropertyInfo) null))
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidMemberForNamedArgument"));
        argumentType = propertyInfo.PropertyType;
      }
      this.m_memberInfo = memberInfo;
      this.m_value = new CustomAttributeTypedArgument(argumentType, value);
    }

    /// <summary>初始化 <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> 类（它表示自定义特性的指定字段或属性）的新实例，并指定描述字段或属性的类型和值的 <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> 对象。</summary>
    /// <param name="memberInfo">自定义特性的字段或属性。新的 <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> 对象表示此成员及其值。</param>
    /// <param name="typedArgument">一个描述字段或属性的类型和值的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="memberInfo" /> 为 null。</exception>
    public CustomAttributeNamedArgument(MemberInfo memberInfo, CustomAttributeTypedArgument typedArgument)
    {
      if (memberInfo == (MemberInfo) null)
        throw new ArgumentNullException("memberInfo");
      this.m_memberInfo = memberInfo;
      this.m_value = typedArgument;
    }

    /// <summary>测试两个 <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> 结构是否相等。</summary>
    /// <returns>如果两个 <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> 结构相等，则为 true；否则为 false。</returns>
    /// <param name="left">相等运算符左侧的结构。</param>
    /// <param name="right">相等运算符右侧的结构。</param>
    public static bool operator ==(CustomAttributeNamedArgument left, CustomAttributeNamedArgument right)
    {
      return left.Equals((object) right);
    }

    /// <summary>测试两个 <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> 结构是否不同。</summary>
    /// <returns>如果两个 <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> 结构不同，则为 true；否则为 false。</returns>
    /// <param name="left">不等运算符左侧的结构。</param>
    /// <param name="right">不等运算符右侧的结构。</param>
    public static bool operator !=(CustomAttributeNamedArgument left, CustomAttributeNamedArgument right)
    {
      return !left.Equals((object) right);
    }

    /// <summary>返回一个由参数名称、等号和参数值的字符串表示形式组成的字符串。</summary>
    /// <returns>一个由参数名称、等号和参数值的字符串表示形式组成的字符串。</returns>
    public override string ToString()
    {
      if (this.m_memberInfo == (MemberInfo) null)
        return base.ToString();
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "{0} = {1}", (object) this.MemberInfo.Name, (object) this.TypedValue.ToString(this.ArgumentType != typeof (object)));
    }

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    /// <summary>返回一个值，该值指示此实例是否与指定的对象相等。</summary>
    /// <returns>如果 <paramref name="obj" /> 等于此实例的类型和值，则为 true；否则为 false。</returns>
    /// <param name="obj">与此实例进行比较的 object，或 null。</param>
    public override bool Equals(object obj)
    {
      return obj == (ValueType) this;
    }
  }
}
