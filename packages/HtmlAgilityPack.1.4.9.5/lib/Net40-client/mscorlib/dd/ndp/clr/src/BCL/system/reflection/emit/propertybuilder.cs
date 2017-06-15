// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.PropertyBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
  /// <summary>定义类型的属性。</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_PropertyBuilder))]
  [ComVisible(true)]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public sealed class PropertyBuilder : PropertyInfo, _PropertyBuilder
  {
    private string m_name;
    private PropertyToken m_prToken;
    private int m_tkProperty;
    private ModuleBuilder m_moduleBuilder;
    private SignatureHelper m_signature;
    private PropertyAttributes m_attributes;
    private Type m_returnType;
    private MethodInfo m_getMethod;
    private MethodInfo m_setMethod;
    private TypeBuilder m_containingType;

    /// <summary>检索此属性的标记。</summary>
    /// <returns>只读。检索此属性的标记。</returns>
    public PropertyToken PropertyToken
    {
      get
      {
        return this.m_prToken;
      }
    }

    internal int MetadataTokenInternal
    {
      get
      {
        return this.m_tkProperty;
      }
    }

    /// <summary>获取在其中定义了特定类型的模块，该类型即为声明当前属性 (Property) 的类型。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.Module" />，在该模块中定义了声明当前属性的类型。</returns>
    public override Module Module
    {
      get
      {
        return this.m_containingType.Module;
      }
    }

    /// <summary>获取此属性的字段类型。</summary>
    /// <returns>此属性的类型。</returns>
    public override Type PropertyType
    {
      get
      {
        return this.m_returnType;
      }
    }

    /// <summary>获取此属性 (Property) 的属性 (Attribute)。</summary>
    /// <returns>此属性 (Property) 的属性 (Attribute)。</returns>
    public override PropertyAttributes Attributes
    {
      get
      {
        return this.m_attributes;
      }
    }

    /// <summary>获取一个值，该值指示此属性是否可读。</summary>
    /// <returns>如果此属性可读，则为 true；否则为 false。</returns>
    public override bool CanRead
    {
      get
      {
        return this.m_getMethod != (MethodInfo) null;
      }
    }

    /// <summary>获取一个值，该值指示此属性是否可写。</summary>
    /// <returns>如果此属性可写，则为 true；否则，为 false。</returns>
    public override bool CanWrite
    {
      get
      {
        return this.m_setMethod != (MethodInfo) null;
      }
    }

    /// <summary>获取此成员的名称。</summary>
    /// <returns>包含此成员名称的 <see cref="T:System.String" />。</returns>
    public override string Name
    {
      get
      {
        return this.m_name;
      }
    }

    /// <summary>获取声明该成员的类。</summary>
    /// <returns>声明该成员的类的 Type 对象。</returns>
    public override Type DeclaringType
    {
      get
      {
        return (Type) this.m_containingType;
      }
    }

    /// <summary>获取用于获取 MemberInfo 的此实例的类对象。</summary>
    /// <returns>Type 对象，通过它获取了该 MemberInfo 对象。</returns>
    public override Type ReflectedType
    {
      get
      {
        return (Type) this.m_containingType;
      }
    }

    private PropertyBuilder()
    {
    }

    internal PropertyBuilder(ModuleBuilder mod, string name, SignatureHelper sig, PropertyAttributes attr, Type returnType, PropertyToken prToken, TypeBuilder containingType)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
      if ((int) name[0] == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_IllegalName"), "name");
      this.m_name = name;
      this.m_moduleBuilder = mod;
      this.m_signature = sig;
      this.m_attributes = attr;
      this.m_returnType = returnType;
      this.m_prToken = prToken;
      this.m_tkProperty = prToken.Token;
      this.m_containingType = containingType;
    }

    /// <summary>设置该属性 (Property) 的默认值。</summary>
    /// <param name="defaultValue">该属性 (Property) 的默认值。</param>
    /// <exception cref="T:System.InvalidOperationException">已对封闭类型调用了 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />。</exception>
    /// <exception cref="T:System.ArgumentException">该属性不是受支持类型之一。- 或 -<paramref name="defaultValue" /> 类型与该属性类型不匹配。- 或 -该属性的类型为 <see cref="T:System.Object" /> 或其他引用类型，并且 <paramref name="defaultValue" /> 不是 null，该值无法赋给引用类型。</exception>
    [SecuritySafeCritical]
    public void SetConstant(object defaultValue)
    {
      this.m_containingType.ThrowIfCreated();
      TypeBuilder.SetConstantValue(this.m_moduleBuilder, this.m_prToken.Token, this.m_returnType, defaultValue);
    }

    [SecurityCritical]
    private void SetMethodSemantics(MethodBuilder mdBuilder, MethodSemanticsAttributes semantics)
    {
      if ((MethodInfo) mdBuilder == (MethodInfo) null)
        throw new ArgumentNullException("mdBuilder");
      this.m_containingType.ThrowIfCreated();
      TypeBuilder.DefineMethodSemantics(this.m_moduleBuilder.GetNativeHandle(), this.m_prToken.Token, semantics, mdBuilder.GetToken().Token);
    }

    /// <summary>设置获取属性值的方法。</summary>
    /// <param name="mdBuilder">MethodBuilder 对象，表示获取属性值的方法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="mdBuilder" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">已对封闭类型调用了 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />。</exception>
    [SecuritySafeCritical]
    public void SetGetMethod(MethodBuilder mdBuilder)
    {
      this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Getter);
      this.m_getMethod = (MethodInfo) mdBuilder;
    }

    /// <summary>设置用于设置属性值的方法。</summary>
    /// <param name="mdBuilder">MethodBuilder 对象，表示设置属性值的方法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="mdBuilder" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">已对封闭类型调用了 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />。</exception>
    [SecuritySafeCritical]
    public void SetSetMethod(MethodBuilder mdBuilder)
    {
      this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Setter);
      this.m_setMethod = (MethodInfo) mdBuilder;
    }

    /// <summary>添加与此属性关联的其他方法之一。</summary>
    /// <param name="mdBuilder">一个表示另一个方法的 MethodBuilder 对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="mdBuilder" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">已对封闭类型调用了 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />。</exception>
    [SecuritySafeCritical]
    public void AddOtherMethod(MethodBuilder mdBuilder)
    {
      this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Other);
    }

    /// <summary>使用指定的自定义属性 Blob 设置自定义属性。</summary>
    /// <param name="con">自定义属性的构造函数。</param>
    /// <param name="binaryAttribute">表示属性的字节 Blob。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="con" /> 或 <paramref name="binaryAttribute" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">已对封闭类型调用了 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />。</exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
    {
      if (con == (ConstructorInfo) null)
        throw new ArgumentNullException("con");
      if (binaryAttribute == null)
        throw new ArgumentNullException("binaryAttribute");
      this.m_containingType.ThrowIfCreated();
      TypeBuilder.DefineCustomAttribute(this.m_moduleBuilder, this.m_prToken.Token, this.m_moduleBuilder.GetConstructorToken(con).Token, binaryAttribute, false, false);
    }

    /// <summary>使用自定义属性生成器设置自定义属性。</summary>
    /// <param name="customBuilder">定义自定义属性的帮助器类的实例。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="customBuilder" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">如果已对封闭类型调用了 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />。</exception>
    [SecuritySafeCritical]
    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
      if (customBuilder == null)
        throw new ArgumentNullException("customBuilder");
      this.m_containingType.ThrowIfCreated();
      customBuilder.CreateCustomAttribute(this.m_moduleBuilder, this.m_prToken.Token);
    }

    /// <summary>通过调用索引化属性 (Property) 的 getter 方法来获取该属性 (Property) 的值。</summary>
    /// <returns>指定的索引化属性 (Property) 的值。</returns>
    /// <param name="obj">将返回其属性值的对象。</param>
    /// <param name="index">索引化属性的可选索引值。对于非索引化属性，该值应为 null。</param>
    /// <exception cref="T:System.NotSupportedException">此方法不受支持。</exception>
    public override object GetValue(object obj, object[] index)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>获取属性的值，该属性具有指定的绑定、索引和 CultureInfo。</summary>
    /// <returns>
    /// <paramref name="obj" /> 的属性 (Property) 值。</returns>
    /// <param name="obj">将返回其属性值的对象。</param>
    /// <param name="invokeAttr">调用属性。这必须是 BindingFlags 中的位标志：InvokeMethod、CreateInstance、Static、GetField、SetField、GetProperty 或 SetProperty。必须指定合适的调用属性。如果要调用静态成员，则必须设置 BindingFlags 的 Static 标志。</param>
    /// <param name="binder">一个对象，它使用反射启用绑定、参数类型的强制、成员的调用和 MemberInfo 对象的检索。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="index">索引化属性的可选索引值。对于非索引化属性，该值应为 null。</param>
    /// <param name="culture">表示要为其本地化资源的区域性的 CultureInfo 对象。请注意，如果没有为此区域性本地化该资源，则在搜索匹配项的过程中将继续调用 CultureInfo.Parent 方法。如果此值为 null，则从 CultureInfo.CurrentUICulture 属性获得 CultureInfo。</param>
    /// <exception cref="T:System.NotSupportedException">此方法不受支持。</exception>
    public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>用索引属性的可选索引值设置该属性的值。</summary>
    /// <param name="obj">将设置其属性值的对象。</param>
    /// <param name="value">此属性的新值。</param>
    /// <param name="index">索引化属性的可选索引值。对于非索引化属性，该值应为 null。</param>
    /// <exception cref="T:System.NotSupportedException">此方法不受支持。</exception>
    public override void SetValue(object obj, object value, object[] index)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>将给定对象的属性值设置为给定值。</summary>
    /// <param name="obj">将返回其属性值的对象。</param>
    /// <param name="value">此属性的新值。</param>
    /// <param name="invokeAttr">调用属性。这必须是 BindingFlags 中的位标志：InvokeMethod、CreateInstance、Static、GetField、SetField、GetProperty 或 SetProperty。必须指定合适的调用属性。如果要调用静态成员，则必须设置 BindingFlags 的 Static 标志。</param>
    /// <param name="binder">一个对象，它使用反射启用绑定、参数类型的强制、成员的调用和 MemberInfo 对象的检索。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="index">索引化属性的可选索引值。对于非索引化属性，该值应为 null。</param>
    /// <param name="culture">表示要为其本地化资源的区域性的 CultureInfo 对象。请注意，如果没有为此区域性本地化该资源，则在搜索匹配项的过程中将继续调用 CultureInfo.Parent 方法。如果此值为 null，则从 CultureInfo.CurrentUICulture 属性获得 CultureInfo。</param>
    /// <exception cref="T:System.NotSupportedException">此方法不受支持。</exception>
    public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>返回此属性 (Property) 的公共和非公共 get 和 set 的数组。</summary>
    /// <returns>MethodInfo 类型的数组，它包含匹配的公共或非公共访问器，或者如果在此属性上不存在匹配访问器，则为空数组。</returns>
    /// <param name="nonPublic">指示非公共方法是否应在 MethodInfo 数组中返回。如果要包括非公共方法，则为 true；否则为 false。</param>
    /// <exception cref="T:System.NotSupportedException">此方法不受支持。</exception>
    public override MethodInfo[] GetAccessors(bool nonPublic)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>返回该属性的公共和非公共 get 访问器。</summary>
    /// <returns>如果 <paramref name="nonPublic" /> 为 true，则返回表示该属性的 get 访问器的 MethodInfo 对象。如果 <paramref name="nonPublic" /> 为 false 且 get 访问器是非公共的，或者如果 <paramref name="nonPublic" /> 为 true 但不存在 get 访问器，则返回 null。</returns>
    /// <param name="nonPublic">指示是否应返回非公共 get 取值函数。如果要包括非公共方法，则为 true；否则为 false。</param>
    public override MethodInfo GetGetMethod(bool nonPublic)
    {
      if (nonPublic || this.m_getMethod == (MethodInfo) null || (this.m_getMethod.Attributes & MethodAttributes.Public) == MethodAttributes.Public)
        return this.m_getMethod;
      return (MethodInfo) null;
    }

    /// <summary>返回此属性的 set 访问器。</summary>
    /// <returns>值条件表示此属性的 Set 方法的 <see cref="T:System.Reflection.MethodInfo" /> 对象。Set 访问器是公共的。<paramref name="nonPublic" /> 为 true 并且可以返回非公共的方法。null<paramref name="nonPublic" /> 为 true，但是该属性是只读的。<paramref name="nonPublic" /> 为 false，且 set 访问器是非公共的。</returns>
    /// <param name="nonPublic">指示在取值函数是非公共的情况下是否应将其返回。如果要包括非公共方法，则为 true；否则为 false。</param>
    public override MethodInfo GetSetMethod(bool nonPublic)
    {
      if (nonPublic || this.m_setMethod == (MethodInfo) null || (this.m_setMethod.Attributes & MethodAttributes.Public) == MethodAttributes.Public)
        return this.m_setMethod;
      return (MethodInfo) null;
    }

    /// <summary>返回此属性 (Property) 的所有索引参数的数组。</summary>
    /// <returns>ParameterInfo 类型的数组，它包含索引的参数。</returns>
    /// <exception cref="T:System.NotSupportedException">此方法不受支持。</exception>
    public override ParameterInfo[] GetIndexParameters()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>返回此属性 (Property) 的所有自定义属性 (Attribute) 的数组。</summary>
    /// <returns>一个包含所有自定义属性 (Attribute) 的数组。</returns>
    /// <param name="inherit">如果为 true，则遍历此属性的继承链以查找自定义特性</param>
    /// <exception cref="T:System.NotSupportedException">此方法不受支持。</exception>
    public override object[] GetCustomAttributes(bool inherit)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>返回由 <see cref="T:System.Type" /> 标识的自定义属性 (Attribute) 数组。</summary>
    /// <returns>在该反映成员上定义的自定义属性 (Attribute) 的数组，如果未在该成员上定义任何属性 (Attribute)，则为 null。</returns>
    /// <param name="attributeType">由类型标识的自定义属性数组。</param>
    /// <param name="inherit">如果为 true，则遍历此属性的继承链以查找自定义特性。</param>
    /// <exception cref="T:System.NotSupportedException">此方法不受支持。</exception>
    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>指示是否在此属性上定义一个或多个 <paramref name="attributeType" /> 的实例。</summary>
    /// <returns>如果在此属性上定义一个或多个 <paramref name="attributeType" /> 实例，则为 true；否则为 false。</returns>
    /// <param name="attributeType">自定义属性应用于的 Type 对象。</param>
    /// <param name="inherit">指定是否遍历属性的继承链以查找自定义特性。</param>
    /// <exception cref="T:System.NotSupportedException">此方法不受支持。</exception>
    public override bool IsDefined(Type attributeType, bool inherit)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    void _PropertyBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _PropertyBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _PropertyBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _PropertyBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
