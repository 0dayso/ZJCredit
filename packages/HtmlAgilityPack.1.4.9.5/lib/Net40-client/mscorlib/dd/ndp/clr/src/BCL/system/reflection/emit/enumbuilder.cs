// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.EnumBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
  /// <summary>说明并表示枚举类型。</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_EnumBuilder))]
  [ComVisible(true)]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public sealed class EnumBuilder : TypeInfo, _EnumBuilder
  {
    internal TypeBuilder m_typeBuilder;
    private FieldBuilder m_underlyingField;

    /// <summary>返回该枚举的内部元数据类型标记。</summary>
    /// <returns>只读。该枚举的类型标记。</returns>
    public TypeToken TypeToken
    {
      get
      {
        return this.m_typeBuilder.TypeToken;
      }
    }

    /// <summary>返回该枚举的基础字段。</summary>
    /// <returns>只读。该枚举的基础字段。</returns>
    public FieldBuilder UnderlyingField
    {
      get
      {
        return this.m_underlyingField;
      }
    }

    /// <summary>返回该枚举的名称。</summary>
    /// <returns>只读。该枚举的名称。</returns>
    public override string Name
    {
      get
      {
        return this.m_typeBuilder.Name;
      }
    }

    /// <summary>返回此枚举的 GUID。</summary>
    /// <returns>只读。此枚举的 GUID。</returns>
    /// <exception cref="T:System.NotSupportedException">在不完整类型中目前不支持此方法。</exception>
    public override Guid GUID
    {
      get
      {
        return this.m_typeBuilder.GUID;
      }
    }

    /// <summary>检索包含此 <see cref="T:System.Reflection.Emit.EnumBuilder" /> 定义的动态模块。</summary>
    /// <returns>只读。包含此 <see cref="T:System.Reflection.Emit.EnumBuilder" /> 定义的动态模块。</returns>
    public override Module Module
    {
      get
      {
        return this.m_typeBuilder.Module;
      }
    }

    /// <summary>检索包含此枚举定义的动态程序集。</summary>
    /// <returns>只读。包含此枚举定义的动态程序集。</returns>
    public override Assembly Assembly
    {
      get
      {
        return this.m_typeBuilder.Assembly;
      }
    }

    /// <summary>检索该枚举的内部句柄。</summary>
    /// <returns>只读。该枚举的内部句柄。</returns>
    /// <exception cref="T:System.NotSupportedException">目前不支持此属性。</exception>
    public override RuntimeTypeHandle TypeHandle
    {
      get
      {
        return this.m_typeBuilder.TypeHandle;
      }
    }

    /// <summary>返回此枚举的完整路径。</summary>
    /// <returns>只读。此枚举的完整路径。</returns>
    public override string FullName
    {
      get
      {
        return this.m_typeBuilder.FullName;
      }
    }

    /// <summary>返回由父程序集的显示名称完全限定的此枚举的完整路径。</summary>
    /// <returns>只读。由父程序集的显示名称完全限定的此枚举的完整路径。</returns>
    /// <exception cref="T:System.NotSupportedException">如果 <see cref="M:System.Reflection.Emit.EnumBuilder.CreateType" /> 以前未被调用过。</exception>
    public override string AssemblyQualifiedName
    {
      get
      {
        return this.m_typeBuilder.AssemblyQualifiedName;
      }
    }

    /// <summary>返回该枚举的命名空间。</summary>
    /// <returns>只读。该枚举的命名空间。</returns>
    public override string Namespace
    {
      get
      {
        return this.m_typeBuilder.Namespace;
      }
    }

    /// <summary>返回此类型的父 <see cref="T:System.Type" />，它始终为 <see cref="T:System.Enum" />。</summary>
    /// <returns>只读。该类型的父 <see cref="T:System.Type" />。</returns>
    public override Type BaseType
    {
      get
      {
        return this.m_typeBuilder.BaseType;
      }
    }

    /// <summary>获取指示此对象是否表示构造的泛型类型的值。</summary>
    /// <returns>如果此对象表示构造泛型类型，则为 true；否则为 false。</returns>
    public override bool IsConstructedGenericType
    {
      get
      {
        return false;
      }
    }

    /// <summary>返回该枚举的基础系统类型。</summary>
    /// <returns>只读。返回基础系统类型。</returns>
    public override Type UnderlyingSystemType
    {
      get
      {
        return this.GetEnumUnderlyingType();
      }
    }

    /// <summary>返回声明该 <see cref="T:System.Reflection.Emit.EnumBuilder" /> 的类型。</summary>
    /// <returns>只读。声明该 <see cref="T:System.Reflection.Emit.EnumBuilder" /> 的类型。</returns>
    public override Type DeclaringType
    {
      get
      {
        return this.m_typeBuilder.DeclaringType;
      }
    }

    /// <summary>返回用于获取该 <see cref="T:System.Reflection.Emit.EnumBuilder" /> 的类型。</summary>
    /// <returns>只读。用于获取该 <see cref="T:System.Reflection.Emit.EnumBuilder" /> 的类型。</returns>
    public override Type ReflectedType
    {
      get
      {
        return this.m_typeBuilder.ReflectedType;
      }
    }

    internal int MetadataTokenInternal
    {
      get
      {
        return this.m_typeBuilder.MetadataTokenInternal;
      }
    }

    private EnumBuilder()
    {
    }

    [SecurityCritical]
    internal EnumBuilder(string name, Type underlyingType, TypeAttributes visibility, ModuleBuilder module)
    {
      if ((visibility & ~TypeAttributes.VisibilityMask) != TypeAttributes.NotPublic)
        throw new ArgumentException(Environment.GetResourceString("Argument_ShouldOnlySetVisibilityFlags"), "name");
      this.m_typeBuilder = new TypeBuilder(name, visibility | TypeAttributes.Sealed, typeof (Enum), (Type[]) null, module, PackingSize.Unspecified, 0, (TypeBuilder) null);
      this.m_underlyingField = this.m_typeBuilder.DefineField("value__", underlyingType, FieldAttributes.Public | FieldAttributes.SpecialName | FieldAttributes.RTSpecialName);
    }

    /// <summary>获取一个值，该值指示指定的 <see cref="T:System.Reflection.TypeInfo" /> 对象是否可以分配给这个对象。</summary>
    /// <returns>如果 <paramref name="typeInfo" />可分配给此对象，则为 true；否则为 false。</returns>
    /// <param name="typeInfo">要测试的对象。</param>
    public override bool IsAssignableFrom(TypeInfo typeInfo)
    {
      if ((Type) typeInfo == (Type) null)
        return false;
      return this.IsAssignableFrom(typeInfo.AsType());
    }

    /// <summary>用指定的常数值定义枚举类型中已命名的静态字段。</summary>
    /// <returns>定义的字段。</returns>
    /// <param name="literalName">静态字段的名称。</param>
    /// <param name="literalValue">Literal 的常数值。</param>
    public FieldBuilder DefineLiteral(string literalName, object literalValue)
    {
      FieldBuilder fieldBuilder = this.m_typeBuilder.DefineField(literalName, (Type) this, FieldAttributes.Public | FieldAttributes.Static | FieldAttributes.Literal);
      object defaultValue = literalValue;
      fieldBuilder.SetConstant(defaultValue);
      return fieldBuilder;
    }

    /// <summary>获取表示此枚举的 <see cref="T:System.Reflection.TypeInfo" /> 对象。</summary>
    /// <returns>一个对象，表示此枚举。</returns>
    public TypeInfo CreateTypeInfo()
    {
      return this.m_typeBuilder.CreateTypeInfo();
    }

    /// <summary>创建该枚举的 <see cref="T:System.Type" /> 对象。</summary>
    /// <returns>该枚举的 <see cref="T:System.Type" /> 对象。</returns>
    /// <exception cref="T:System.InvalidOperationException">以前创建过此类型。- 或 -尚未创建封闭类型。</exception>
    public Type CreateType()
    {
      return this.m_typeBuilder.CreateType();
    }

    /// <summary>调用指定的成员。在指定的联编程序和调用属性的约束下，要调用的方法必须是可访问的，并且必须提供与指定参数列表相符的最精确的匹配项。</summary>
    /// <returns>返回被调用成员的返回值。</returns>
    /// <param name="name">要调用的成员名。它可以是构造函数、方法、属性或字段。必须指定合适的调用属性。请注意，可以通过将空字符串作为成员名称传递来调用类的默认成员。</param>
    /// <param name="invokeAttr">调用属性。这必须是来自 BindingFlags 的位标志。</param>
    /// <param name="binder">一个对象，它使用反射启用绑定、参数类型的强制、成员的调用和 MemberInfo 对象的检索。如果 binder 为 null，则使用默认联编程序。请参见<see cref="T:System.Reflection.Binder" />。</param>
    /// <param name="target">对其调用指定成员的对象。如果该成员是静态的，则忽略此参数。</param>
    /// <param name="args">参数列表。这是一个对象数组，包含要调用的成员的参数的数目、顺序和类型。如果没有参数，则它应为 null。</param>
    /// <param name="modifiers">与 <paramref name="args" /> 长度相同的数组，其元素表示与要调用的成员的参数相关联的属性。参数在元数据中有关联的属性。它们由各种交互操作服务使用。有关这些说明的详细信息，请参见元数据规范。</param>
    /// <param name="culture">用于控制类型强制的 CultureInfo 的实例。如果这是 null，则使用当前线程的 CultureInfo。（注意，这对于某些转换是必要的，例如，将表示 1000 的 String 转换为 Double 值，因为不同区域性的 1000 表示形式不同。）</param>
    /// <param name="namedParameters">
    /// <paramref name="namedParameters" /> 数组中的每一个参数获取 <paramref name="args" /> 数组中相应元素中的值。如果 <paramref name="args" /> 的长度大于 <paramref name="namedParameters" /> 的长度，则按顺序传递剩余的参数值。</param>
    /// <exception cref="T:System.NotSupportedException">在不完整类型中目前不支持此方法。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
    {
      return this.m_typeBuilder.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
    }

    protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      return this.m_typeBuilder.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
    }

    /// <summary>按照指定，返回 <see cref="T:System.Reflection.ConstructorInfo" /> 对象的数组，这些对象表示为此类定义的公共和非公共构造函数。</summary>
    /// <returns>返回 <see cref="T:System.Reflection.ConstructorInfo" /> 对象的数组，这些对象表示为此类定义的指定构造函数。如果未定义任何构造函数，则返回空数组。</returns>
    /// <param name="bindingAttr">这必须是来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志：InvokeMethod、NonPublic 等。</param>
    /// <exception cref="T:System.NotSupportedException">在不完整类型中目前不支持此方法。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [ComVisible(true)]
    public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
    {
      return this.m_typeBuilder.GetConstructors(bindingAttr);
    }

    protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      if (types == null)
        return this.m_typeBuilder.GetMethod(name, bindingAttr);
      return this.m_typeBuilder.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
    }

    /// <summary>按照指定，返回此类型声明或继承的所有公共和非公共方法。</summary>
    /// <returns>如果使用 <paramref name="nonPublic" />，则返回 <see cref="T:System.Reflection.MethodInfo" /> 对象的数组，这些对象表示在此类型上定义的公共和非公共方法；否则，仅返回公共方法。</returns>
    /// <param name="bindingAttr">这必须是来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志，如 InvokeMethod、NonPublic 等。</param>
    /// <exception cref="T:System.NotSupportedException">在不完整类型中目前不支持此方法。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
    {
      return this.m_typeBuilder.GetMethods(bindingAttr);
    }

    /// <summary>返回由给定名称指定的字段。</summary>
    /// <returns>返回 <see cref="T:System.Reflection.FieldInfo" /> 对象，该对象表示此类型声明或继承且具有指定名称和公共或非公共修饰符的字段。如果没有匹配项，则返回空。</returns>
    /// <param name="name">要获取的字段的名称。</param>
    /// <param name="bindingAttr">这必须是来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志：InvokeMethod、NonPublic 等。</param>
    /// <exception cref="T:System.NotSupportedException">在不完整类型中目前不支持此方法。</exception>
    public override FieldInfo GetField(string name, BindingFlags bindingAttr)
    {
      return this.m_typeBuilder.GetField(name, bindingAttr);
    }

    /// <summary>返回此类型声明的公共和非公共字段。</summary>
    /// <returns>返回 <see cref="T:System.Reflection.FieldInfo" /> 对象的数组，这些对象表示此类型声明或继承的公共和非公共字段。按照指定，如果没有任何字段，则返回空数组。</returns>
    /// <param name="bindingAttr">这必须是来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志，如 InvokeMethod、NonPublic 等。</param>
    /// <exception cref="T:System.NotSupportedException">在不完整类型中目前不支持此方法。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override FieldInfo[] GetFields(BindingFlags bindingAttr)
    {
      return this.m_typeBuilder.GetFields(bindingAttr);
    }

    /// <summary>使用指定的完全限定名返回此类型（直接或间接）实现的接口。</summary>
    /// <returns>返回 <see cref="T:System.Type" /> 对象，该对象表示实现的接口。如果未找到名称匹配的接口，则返回空。</returns>
    /// <param name="name">接口名。</param>
    /// <param name="ignoreCase">如果为 true，则搜索不区分大小写。如果为 false，则搜索区分大小写。</param>
    /// <exception cref="T:System.NotSupportedException">在不完整类型中目前不支持此方法。</exception>
    public override Type GetInterface(string name, bool ignoreCase)
    {
      return this.m_typeBuilder.GetInterface(name, ignoreCase);
    }

    /// <summary>返回在此类及其基类上实现的所有接口的数组。</summary>
    /// <returns>返回 <see cref="T:System.Type" /> 对象的数组，这些对象表示实现的接口。如果一个都没有定义，则返回空数组。</returns>
    public override Type[] GetInterfaces()
    {
      return this.m_typeBuilder.GetInterfaces();
    }

    /// <summary>返回具有指定名称的事件。</summary>
    /// <returns>返回 <see cref="T:System.Reflection.EventInfo" /> 对象，该对象表示此类型声明或继承的具有指定名称的事件。如果没有匹配项，则返回 null。</returns>
    /// <param name="name">要获取的事件的名称。</param>
    /// <param name="bindingAttr">此调用属性。这必须是来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志：InvokeMethod、NonPublic 等。</param>
    /// <exception cref="T:System.NotSupportedException">在不完整类型中目前不支持此方法。</exception>
    public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
    {
      return this.m_typeBuilder.GetEvent(name, bindingAttr);
    }

    /// <summary>返回此类型声明或继承的公共事件的事件。</summary>
    /// <returns>返回 <see cref="T:System.Reflection.EventInfo" /> 对象的数组，这些对象表示此类型声明或继承的公共事件。如果没有公共事件，则返回空数组。</returns>
    /// <exception cref="T:System.NotSupportedException">在不完整类型中目前不支持此方法。</exception>
    public override EventInfo[] GetEvents()
    {
      return this.m_typeBuilder.GetEvents();
    }

    protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>按照指定，返回此类型声明或继承的所有公共和非公共属性。</summary>
    /// <returns>如果使用 <paramref name="nonPublic" />，则返回 <see cref="T:System.Reflection.PropertyInfo" /> 对象的数组，这些对象表示在此类型上定义的公共和非公共属性；否则，仅返回公共属性。</returns>
    /// <param name="bindingAttr">此调用属性。这必须是来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志：InvokeMethod、NonPublic 等。</param>
    /// <exception cref="T:System.NotSupportedException">在不完整类型中目前不支持此方法。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
    {
      return this.m_typeBuilder.GetProperties(bindingAttr);
    }

    /// <summary>返回此类型声明或继承的公共和非公共嵌套类型。</summary>
    /// <returns>表示嵌套在当前 <see cref="T:System.Type" /> 中的匹配指定绑定约束的所有类型的 <see cref="T:System.Type" /> 对象数组。如果没有嵌套在当前 <see cref="T:System.Type" /> 中的类型，或者如果没有一个嵌套类型匹配绑定约束，则为 <see cref="T:System.Type" /> 类型的空数组。</returns>
    /// <param name="bindingAttr">这必须是来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志，如 InvokeMethod、NonPublic 等。</param>
    /// <exception cref="T:System.NotSupportedException">在不完整类型中目前不支持此方法。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override Type[] GetNestedTypes(BindingFlags bindingAttr)
    {
      return this.m_typeBuilder.GetNestedTypes(bindingAttr);
    }

    /// <summary>返回此类型声明的指定嵌套类型。</summary>
    /// <returns>表示符合指定要求的嵌套类型的 <see cref="T:System.Type" /> 对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的嵌套类型的名称的 <see cref="T:System.String" />。</param>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 -零，表示对公共方法执行区分大小写的搜索。</param>
    /// <exception cref="T:System.NotSupportedException">在不完整类型中目前不支持此方法。</exception>
    public override Type GetNestedType(string name, BindingFlags bindingAttr)
    {
      return this.m_typeBuilder.GetNestedType(name, bindingAttr);
    }

    /// <summary>返回具有指定名称、类型并由此类型声明或继承其绑定的所有成员。</summary>
    /// <returns>如果使用 <paramref name="nonPublic" />，则返回 <see cref="T:System.Reflection.MemberInfo" /> 对象的数组，这些对象表示在此类型上定义的公共和非公共成员；否则，仅返回公共成员。</returns>
    /// <param name="name">成员的名称。</param>
    /// <param name="type">要返回的成员的类型。</param>
    /// <param name="bindingAttr">这必须是来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志：InvokeMethod、NonPublic 等。</param>
    /// <exception cref="T:System.NotSupportedException">在不完整类型中目前不支持此方法。</exception>
    public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
    {
      return this.m_typeBuilder.GetMember(name, type, bindingAttr);
    }

    /// <summary>返回由此类型声明或继承的指定成员。</summary>
    /// <returns>返回 <see cref="T:System.Reflection.MemberInfo" /> 对象的数组，这些对象表示此类型声明或继承的公共和非公共成员。如果没有匹配的成员，则返回空数组。</returns>
    /// <param name="bindingAttr">这必须是来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志：InvokeMethod、NonPublic 等。</param>
    /// <exception cref="T:System.NotSupportedException">在不完整类型中目前不支持此方法。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
    {
      return this.m_typeBuilder.GetMembers(bindingAttr);
    }

    /// <summary>返回请求的接口的接口映射。</summary>
    /// <returns>请求的接口映射。</returns>
    /// <param name="interfaceType">要为其检索接口映射的接口类型。</param>
    /// <exception cref="T:System.ArgumentException">该类型未实现接口。</exception>
    [ComVisible(true)]
    public override InterfaceMapping GetInterfaceMap(Type interfaceType)
    {
      return this.m_typeBuilder.GetInterfaceMap(interfaceType);
    }

    /// <summary>返回此类型声明的公共和非公共事件。</summary>
    /// <returns>返回 <see cref="T:System.Reflection.EventInfo" /> 对象的数组，这些对象表示此类型声明或继承的公共和非公共事件。按照指定，如果没有任何事件，则返回空数组。</returns>
    /// <param name="bindingAttr">这必须是来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志，如 InvokeMethod、NonPublic 等。</param>
    /// <exception cref="T:System.NotSupportedException">在不完整类型中目前不支持此方法。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override EventInfo[] GetEvents(BindingFlags bindingAttr)
    {
      return this.m_typeBuilder.GetEvents(bindingAttr);
    }

    protected override TypeAttributes GetAttributeFlagsImpl()
    {
      return this.m_typeBuilder.Attributes;
    }

    protected override bool IsArrayImpl()
    {
      return false;
    }

    protected override bool IsPrimitiveImpl()
    {
      return false;
    }

    protected override bool IsValueTypeImpl()
    {
      return true;
    }

    protected override bool IsByRefImpl()
    {
      return false;
    }

    protected override bool IsPointerImpl()
    {
      return false;
    }

    protected override bool IsCOMObjectImpl()
    {
      return false;
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.NotSupportedException" />。</summary>
    /// <returns>此方法不受支持。不返回任何值。</returns>
    /// <exception cref="T:System.NotSupportedException">目前不支持此方法。</exception>
    public override Type GetElementType()
    {
      return this.m_typeBuilder.GetElementType();
    }

    protected override bool HasElementTypeImpl()
    {
      return this.m_typeBuilder.HasElementType;
    }

    /// <summary>返回当前枚举的基础整数类型，在定义枚举生成器时设置该类型。</summary>
    /// <returns>基础类型。</returns>
    public override Type GetEnumUnderlyingType()
    {
      return this.m_underlyingField.FieldType;
    }

    /// <summary>返回为此构造函数定义的所有自定义属性。</summary>
    /// <returns>返回对象的数组，这些对象表示由此 <see cref="T:System.Reflection.Emit.ConstructorBuilder" /> 实例表示的构造函数的所有自定义属性。</returns>
    /// <param name="inherit">指定是否搜索该成员的继承链以查找这些属性。</param>
    /// <exception cref="T:System.NotSupportedException">在不完整类型中目前不支持此方法。</exception>
    public override object[] GetCustomAttributes(bool inherit)
    {
      return this.m_typeBuilder.GetCustomAttributes(inherit);
    }

    /// <summary>返回由给定类型标识的自定义属性。</summary>
    /// <returns>返回表示该构造函数的属性（这些属性属于 <see cref="T:System.Type" /><paramref name="attributeType" />）的对象的数组。</returns>
    /// <param name="attributeType">自定义属性应用于的 Type 对象。</param>
    /// <param name="inherit">指定是否搜索该成员的继承链以查找这些属性。</param>
    /// <exception cref="T:System.NotSupportedException">在不完整类型中目前不支持此方法。</exception>
    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      return this.m_typeBuilder.GetCustomAttributes(attributeType, inherit);
    }

    /// <summary>使用指定的自定义属性 Blob 设置自定义属性。</summary>
    /// <param name="con">自定义属性的构造函数。</param>
    /// <param name="binaryAttribute">表示属性的字节 Blob。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="con" /> 或 <paramref name="binaryAttribute" /> 为 null。</exception>
    [ComVisible(true)]
    public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
    {
      this.m_typeBuilder.SetCustomAttribute(con, binaryAttribute);
    }

    /// <summary>使用自定义属性生成器设置自定义属性。</summary>
    /// <param name="customBuilder">定义自定义属性的帮助器类的实例。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="con" /> 为 null。</exception>
    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
      this.m_typeBuilder.SetCustomAttribute(customBuilder);
    }

    /// <summary>检查是否定义了指定的自定义特性类型。</summary>
    /// <returns>如果该成员上定义了一个或多个 <paramref name="attributeType" /> 实例，则为 true；否则为 false。</returns>
    /// <param name="attributeType">自定义属性应用于的 Type 对象。</param>
    /// <param name="inherit">指定是否搜索该成员的继承链以查找这些属性。</param>
    /// <exception cref="T:System.NotSupportedException">在不完整类型中目前不支持此方法。</exception>
    public override bool IsDefined(Type attributeType, bool inherit)
    {
      return this.m_typeBuilder.IsDefined(attributeType, inherit);
    }

    public override Type MakePointerType()
    {
      return SymbolType.FormCompoundType("*".ToCharArray(), (Type) this, 0);
    }

    public override Type MakeByRefType()
    {
      return SymbolType.FormCompoundType("&".ToCharArray(), (Type) this, 0);
    }

    public override Type MakeArrayType()
    {
      return SymbolType.FormCompoundType("[]".ToCharArray(), (Type) this, 0);
    }

    /// <exception cref="T:System.IndexOutOfRangeException">
    /// <paramref name="rank" /> 小于 1。</exception>
    public override Type MakeArrayType(int rank)
    {
      if (rank <= 0)
        throw new IndexOutOfRangeException();
      string str = "";
      if (rank == 1)
      {
        str = "*";
      }
      else
      {
        for (int index = 1; index < rank; ++index)
          str += ",";
      }
      return SymbolType.FormCompoundType(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "[{0}]", (object) str).ToCharArray(), (Type) this, 0);
    }

    void _EnumBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _EnumBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _EnumBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _EnumBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
