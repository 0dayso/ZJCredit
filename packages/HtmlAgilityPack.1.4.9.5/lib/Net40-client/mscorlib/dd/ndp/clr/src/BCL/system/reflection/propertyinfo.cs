// Decompiled with JetBrains decompiler
// Type: System.Reflection.PropertyInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection
{
  /// <summary>发现属性 (Property) 的属性 (Attribute) 并提供对属性 (Property) 元数据的访问。</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_PropertyInfo))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
  public abstract class PropertyInfo : MemberInfo, _PropertyInfo
  {
    /// <summary>获取一个 <see cref="T:System.Reflection.MemberTypes" /> 值，该值指示此成员是属性。</summary>
    /// <returns>指示此成员是属性的 <see cref="T:System.Reflection.MemberTypes" /> 值。</returns>
    public override MemberTypes MemberType
    {
      get
      {
        return MemberTypes.Property;
      }
    }

    /// <summary>获取此属性的类型。</summary>
    /// <returns>此属性的类型。</returns>
    [__DynamicallyInvokable]
    public abstract Type PropertyType { [__DynamicallyInvokable] get; }

    /// <summary>获取此属性 (Property) 的属性 (Attribute)。</summary>
    /// <returns>此属性 (Property) 的属性 (Attribute)。</returns>
    [__DynamicallyInvokable]
    public abstract PropertyAttributes Attributes { [__DynamicallyInvokable] get; }

    /// <summary>获取一个值，该值指示此属性是否可读。</summary>
    /// <returns>如果此属性可读，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public abstract bool CanRead { [__DynamicallyInvokable] get; }

    /// <summary>获取一个值，该值指示此属性是否可写。</summary>
    /// <returns>如果此属性可写，则为 true；否则，为 false。</returns>
    [__DynamicallyInvokable]
    public abstract bool CanWrite { [__DynamicallyInvokable] get; }

    /// <summary>获取此属性的 get 访问器。</summary>
    /// <returns>此属性的 get 访问器。</returns>
    [__DynamicallyInvokable]
    public virtual MethodInfo GetMethod
    {
      [__DynamicallyInvokable] get
      {
        return this.GetGetMethod(true);
      }
    }

    /// <summary>获取此属性的 set 访问器。</summary>
    /// <returns>set取值函数，该属性，或null如果属性是只读的。</returns>
    [__DynamicallyInvokable]
    public virtual MethodInfo SetMethod
    {
      [__DynamicallyInvokable] get
      {
        return this.GetSetMethod(true);
      }
    }

    /// <summary>获取一个值，该值指示此属性是否是特殊名称。</summary>
    /// <returns>如果此属性是特殊名称，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsSpecialName
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & PropertyAttributes.SpecialName) > 0U;
      }
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.PropertyInfo" /> 对象是否相等。</summary>
    /// <returns>true if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, false.</returns>
    /// <param name="left">要比较的第一个对象。</param>
    /// <param name="right">要比较的第二个对象。</param>
    [__DynamicallyInvokable]
    public static bool operator ==(PropertyInfo left, PropertyInfo right)
    {
      if (left == right)
        return true;
      if (left == null || right == null || (left is RuntimePropertyInfo || right is RuntimePropertyInfo))
        return false;
      return left.Equals((object) right);
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.PropertyInfo" /> 对象是否不相等。</summary>
    /// <returns>如果 <paramref name="left" /> 不等于 <paramref name="right" />，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个对象。</param>
    /// <param name="right">要比较的第二个对象。</param>
    [__DynamicallyInvokable]
    public static bool operator !=(PropertyInfo left, PropertyInfo right)
    {
      return !(left == right);
    }

    /// <summary>返回一个值，该值指示此实例是否与指定的对象相等。</summary>
    /// <returns>如果 <paramref name="obj" /> 等于此实例的类型和值，则为 true；否则为 false。</returns>
    /// <param name="obj">与此实例进行比较的 object，或 null。</param>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    /// <summary>由编译器返回与属性关联的文本值。</summary>
    /// <returns>一个 <see cref="T:System.Object" />，它包含与此属性关联的文本值。如果文本值是一个元素值为零的类类型，则返回值为 null。</returns>
    /// <exception cref="T:System.InvalidOperationException">非托管元数据中的常数表不包含当前属性的常数值。</exception>
    /// <exception cref="T:System.FormatException">值的类型不是公共语言规范 (CLS) 许可的类型。请参见“ECMA Partition II”（ECMA 第二部分）规范中的“Metadata”（元数据）。</exception>
    [__DynamicallyInvokable]
    public virtual object GetConstantValue()
    {
      throw new NotImplementedException();
    }

    /// <summary>由编译器返回与属性关联的文本值。</summary>
    /// <returns>一个 <see cref="T:System.Object" />，它包含与此属性关联的文本值。如果文本值是一个元素值为零的类类型，则返回值为 null。</returns>
    /// <exception cref="T:System.InvalidOperationException">非托管元数据中的常数表不包含当前属性的常数值。</exception>
    /// <exception cref="T:System.FormatException">值的类型不是公共语言规范 (CLS) 许可的类型。请参见“ECMA Partition II”（ECMA 第二部分）规范中的“Metadata Logical Format: Other Structures, Element Types used in Signatures”（元数据逻辑格式：其他结构，在签名中使用的元素类型）。</exception>
    public virtual object GetRawConstantValue()
    {
      throw new NotImplementedException();
    }

    /// <summary>当在派生类中重写时，为具有指定绑定、索引和区域性特定信息的指定对象设置属性值。</summary>
    /// <param name="obj">将设置其属性值的对象。</param>
    /// <param name="value">新的属性值。</param>
    /// <param name="invokeAttr">以下指定该调用特性的枚举成员的按位组合: InvokeMethod、 CreateInstance、 Static、 GetField、 SetField、 GetProperty和 SetProperty。必须指定合适的调用属性。例如，为了调用静态成员，设置 Static 标志。</param>
    /// <param name="binder">一个对象，它启用绑定、对参数类型的强制、对成员的调用，以及通过反射对 <see cref="T:System.Reflection.MemberInfo" /> 对象的检索。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="index">索引化属性的可选索引值。对于非索引化属性，该值应为 null。</param>
    /// <param name="culture">要为其本地化资源的区域性。请注意，如果没有为此区域性本地化该资源，则在搜索匹配项的过程中将继续调用 <see cref="P:System.Globalization.CultureInfo.Parent" /> 属性。如果该值为 null，则从 <see cref="P:System.Globalization.CultureInfo.CurrentUICulture" /> 属性获取区域性的特定信息。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 数组不包含所需类型的参数。- 或 -未找到该属性的 set 访问器。- 或 -<paramref name="value" />不能转换为的类型<see cref="P:System.Reflection.PropertyInfo.PropertyType" />。</exception>
    /// <exception cref="T:System.Reflection.TargetException">该对象与目标类型不匹配，或者某属性是实例属性但 <paramref name="obj" /> 为 null。</exception>
    /// <exception cref="T:System.Reflection.TargetParameterCountException">
    /// <paramref name="index" /> 中参数的数目与已编制索引的属性所采用的参数的数目不相符。</exception>
    /// <exception cref="T:System.MethodAccessException">尝试非法访问某类中私有或受保护的方法。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">设置属性值时出错。例如，为索引属性指定的索引值超出范围。<see cref="P:System.Exception.InnerException" /> 属性指示错误的原因。</exception>
    public abstract void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

    /// <summary>返回一个数组，其元素反射了当前实例反射的属性的公共及非公共（如果指定）get、set 以及其他访问器。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.MethodInfo" /> 对象的数组，其元素反射了由当前实例反射的属性的 get、set 及其他访问器。如果 <paramref name="nonPublic" /> 为 true，则此数组包含公共及非公共 get、set 及其他访问器。如果 <paramref name="nonPublic" /> 为 false，则此数组仅包含公共 get、set 及其他访问器。如果没有找到具有指定可见性的访问器，则此方法将返回包含零 (0) 个元素的数组。</returns>
    /// <param name="nonPublic">指示非公共方法是否应在 MethodInfo 数组中返回。如果要包括非公共方法，则为 true；否则为 false。</param>
    [__DynamicallyInvokable]
    public abstract MethodInfo[] GetAccessors(bool nonPublic);

    /// <summary>当在派生类中重写时，返回此属性的公共或非公共 get 访问器。</summary>
    /// <returns>如果 <paramref name="nonPublic" /> 为 true，则返回表示该属性的 get 访问器的 MethodInfo 对象。如果 <paramref name="nonPublic" /> 为 false 且 get 访问器是非公共的，或者如果 <paramref name="nonPublic" /> 为 true 但不存在 get 访问器，则返回 null。</returns>
    /// <param name="nonPublic">指示是否应返回非公共 get 取值函数。如果将要返回非公共访问器，则为 true；否则为 false。</param>
    /// <exception cref="T:System.Security.SecurityException">请求的方法为非公共的，并且调用方不具有对该非公共方法进行反射的 <see cref="T:System.Security.Permissions.ReflectionPermission" />。</exception>
    [__DynamicallyInvokable]
    public abstract MethodInfo GetGetMethod(bool nonPublic);

    /// <summary>当在派生类中重写时，返回此属性的 set 访问器。</summary>
    /// <returns>值条件表示此属性的 Set 方法的 <see cref="T:System.Reflection.MethodInfo" /> 对象。set 访问器是公共的。- 或 - <paramref name="nonPublic" /> 为 true，set 访问器是非公共的。 null<paramref name="nonPublic" /> 为 true，但属性是只读的。- 或 - <paramref name="nonPublic" /> 为 false，set 访问器是非公共的。- 或 -不存在 set 访问器。</returns>
    /// <param name="nonPublic">指示在取值函数是非公共的情况下是否应将其返回。如果将要返回非公共访问器，则为 true；否则为 false。</param>
    /// <exception cref="T:System.Security.SecurityException">请求的方法为非公共的，并且调用方不具有对该非公共方法进行反射的 <see cref="T:System.Security.Permissions.ReflectionPermission" />。</exception>
    [__DynamicallyInvokable]
    public abstract MethodInfo GetSetMethod(bool nonPublic);

    /// <summary>当在派生类中重写时，返回此属性的所有索引参数的数组。</summary>
    /// <returns>ParameterInfo 类型的数组，它包含索引的参数。如果未为该属性编制索引，则数组包含 0（零）个元素。</returns>
    [__DynamicallyInvokable]
    public abstract ParameterInfo[] GetIndexParameters();

    /// <summary>返回指定对象的属性值。</summary>
    /// <returns>指定对象的属性值。</returns>
    /// <param name="obj">将返回其属性值的对象。</param>
    [DebuggerStepThrough]
    [DebuggerHidden]
    [__DynamicallyInvokable]
    public object GetValue(object obj)
    {
      return this.GetValue(obj, (object[]) null);
    }

    /// <summary>用索引化属性的可选索引值返回指定对象的该属性值。</summary>
    /// <returns>指定对象的属性值。</returns>
    /// <param name="obj">将返回其属性值的对象。</param>
    /// <param name="index">索引化属性的可选索引值。索引化属性的索引从零开始。对于非索引化属性，该值应为 null。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 数组不包含所需类型的参数。- 或 -未找到该属性的 get 访问器。</exception>
    /// <exception cref="T:System.Reflection.TargetException">在 .NET for Windows Store 应用程序 或 可移植类库 中，请改为捕获 <see cref="T:System.Exception" />。该对象与目标类型不匹配，或者某属性是实例属性但 <paramref name="obj" /> 为 null。</exception>
    /// <exception cref="T:System.Reflection.TargetParameterCountException">
    /// <paramref name="index" /> 中参数的数目与已编制索引的属性所采用的参数的数目不相符。</exception>
    /// <exception cref="T:System.MethodAccessException">在 .NET for Windows Store 应用程序 或 可移植类库 中，请改为捕获基类异常 <see cref="T:System.MemberAccessException" />。尝试非法访问某类中私有或受保护的方法。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">检索属性值时出错。例如，为索引属性指定的索引值超出范围。<see cref="P:System.Exception.InnerException" /> 属性指示错误的原因。</exception>
    [DebuggerStepThrough]
    [DebuggerHidden]
    [__DynamicallyInvokable]
    public virtual object GetValue(object obj, object[] index)
    {
      return this.GetValue(obj, BindingFlags.Default, (Binder) null, index, (CultureInfo) null);
    }

    /// <summary>当在派生类中重写时，将返回具有指定绑定、索引和区域性特定信息的指定对象的属性值。</summary>
    /// <returns>指定对象的属性值。</returns>
    /// <param name="obj">将返回其属性值的对象。</param>
    /// <param name="invokeAttr">以下指定该调用特性的枚举成员的按位组合: InvokeMethod、 CreateInstance、 Static、 GetField、 SetField、 GetProperty和 SetProperty。必须指定合适的调用属性。例如，为了调用静态成员，设置 Static 标志。</param>
    /// <param name="binder">一个对象，它启用绑定、对参数类型的强制、对成员的调用，以及通过反射对 <see cref="T:System.Reflection.MemberInfo" /> 对象的检索。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="index">索引化属性的可选索引值。对于非索引化属性，该值应为 null。</param>
    /// <param name="culture">要为其本地化资源的区域性。请注意，如果没有为此区域性本地化该资源，则在搜索匹配项的过程中将继续调用 <see cref="P:System.Globalization.CultureInfo.Parent" /> 属性。如果该值为 null，则从 <see cref="P:System.Globalization.CultureInfo.CurrentUICulture" /> 属性获取区域性的特定信息。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 数组不包含所需类型的参数。- 或 -未找到该属性的 get 访问器。</exception>
    /// <exception cref="T:System.Reflection.TargetException">该对象与目标类型不匹配，或者某属性是实例属性但 <paramref name="obj" /> 为 null。</exception>
    /// <exception cref="T:System.Reflection.TargetParameterCountException">
    /// <paramref name="index" /> 中参数的数目与已编制索引的属性所采用的参数的数目不相符。</exception>
    /// <exception cref="T:System.MethodAccessException">尝试非法访问某类中私有或受保护的方法。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">检索属性值时出错。例如，为索引属性指定的索引值超出范围。<see cref="P:System.Exception.InnerException" /> 属性指示错误的原因。</exception>
    public abstract object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

    /// <summary>设置指定对象的属性值。</summary>
    /// <param name="obj">将设置其属性值的对象。</param>
    /// <param name="value">新的属性值。</param>
    /// <exception cref="T:System.ArgumentException">未找到该属性的 set 访问器。- 或 -<paramref name="value" />不能转换为的类型<see cref="P:System.Reflection.PropertyInfo.PropertyType" />。</exception>
    /// <exception cref="T:System.Reflection.TargetException">在 .NET for Windows Store 应用程序 或 可移植类库 中，请改为捕获 <see cref="T:System.Exception" />。一种<paramref name="obj" />与目标类型不匹配或某个属性是实例属性，但<paramref name="obj" />是null。</exception>
    /// <exception cref="T:System.MethodAccessException">在 .NET for Windows Store 应用程序 或 可移植类库 中，请改为捕获基类异常 <see cref="T:System.MemberAccessException" />。尝试非法访问某类中私有或受保护的方法。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">设置属性值时出错。<see cref="P:System.Exception.InnerException" /> 属性指示错误的原因。</exception>
    [DebuggerStepThrough]
    [DebuggerHidden]
    [__DynamicallyInvokable]
    public void SetValue(object obj, object value)
    {
      this.SetValue(obj, value, (object[]) null);
    }

    /// <summary>用索引化属性的可选索引值设置指定对象的该属性值。</summary>
    /// <param name="obj">将设置其属性值的对象。</param>
    /// <param name="value">新的属性值。</param>
    /// <param name="index">索引化属性的可选索引值。对于非索引化属性，该值应为 null。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="index" /> 数组不包含所需类型的参数。- 或 -未找到该属性的 set 访问器。- 或 -<paramref name="value" />不能转换为的类型<see cref="P:System.Reflection.PropertyInfo.PropertyType" />。</exception>
    /// <exception cref="T:System.Reflection.TargetException">在 .NET for Windows Store 应用程序 或 可移植类库 中，请改为捕获 <see cref="T:System.Exception" />。该对象与目标类型不匹配，或者某属性是实例属性但 <paramref name="obj" /> 为 null。</exception>
    /// <exception cref="T:System.Reflection.TargetParameterCountException">
    /// <paramref name="index" /> 中参数的数目与已编制索引的属性所采用的参数的数目不相符。</exception>
    /// <exception cref="T:System.MethodAccessException">在 .NET for Windows Store 应用程序 或 可移植类库 中，请改为捕获基类异常 <see cref="T:System.MemberAccessException" />。尝试非法访问某类中私有或受保护的方法。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">设置属性值时出错。例如，为索引属性指定的索引值超出范围。<see cref="P:System.Exception.InnerException" /> 属性指示错误的原因。</exception>
    [DebuggerStepThrough]
    [DebuggerHidden]
    [__DynamicallyInvokable]
    public virtual void SetValue(object obj, object value, object[] index)
    {
      this.SetValue(obj, value, BindingFlags.Default, (Binder) null, index, (CultureInfo) null);
    }

    /// <summary>返回一个类型数组，其中的类型表示属性所必需的自定义修饰符。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 对象的数组，标识当前属性（例如 <see cref="T:System.Runtime.CompilerServices.IsConst" /> 或 <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />）需要的自定义修饰符。</returns>
    public virtual Type[] GetRequiredCustomModifiers()
    {
      return EmptyArray<Type>.Value;
    }

    /// <summary>返回一个类型数组，其中的类型表示属性的可选自定义修饰符。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 对象的数组，这些对象标识当前属性的可选自定义修饰符（例如 <see cref="T:System.Runtime.CompilerServices.IsConst" /> 或 <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />）。</returns>
    public virtual Type[] GetOptionalCustomModifiers()
    {
      return EmptyArray<Type>.Value;
    }

    /// <summary>返回一个数组，其元素反射了由当前实例反射的属性的公共 get、set 以及其他访问器。</summary>
    /// <returns>如果找到访问器，此方法将返回一个 <see cref="T:System.Reflection.MethodInfo" /> 对象的数组，这些对象反射了由当前实例反射的属性的公共 get、set 以及其他访问器；如果没有找到，此方法将返回包含零 (0) 个元素的数组。</returns>
    [__DynamicallyInvokable]
    public MethodInfo[] GetAccessors()
    {
      return this.GetAccessors(false);
    }

    /// <summary>返回此属性的公共 get 访问器。</summary>
    /// <returns>MethodInfo 对象，表示此属性的公共 get 访问器；如果 get 访问器是非公共的或不存在，则为 null。</returns>
    [__DynamicallyInvokable]
    public MethodInfo GetGetMethod()
    {
      return this.GetGetMethod(false);
    }

    /// <summary>返回此属性的公共 set 访问器。</summary>
    /// <returns>如果 set 访问器是公共的，则为表示此属性的 Set 方法的 MethodInfo 对象；如果 set 访问器是非公共的，则为 null。</returns>
    [__DynamicallyInvokable]
    public MethodInfo GetSetMethod()
    {
      return this.GetSetMethod(false);
    }

    Type _PropertyInfo.GetType()
    {
      return this.GetType();
    }

    void _PropertyInfo.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _PropertyInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _PropertyInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _PropertyInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
