// Decompiled with JetBrains decompiler
// Type: System.Reflection.TypeDelegator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>包装 <see cref="T:System.Type" /> 对象并将所有方法委托给该 Type。</summary>
  [ComVisible(true)]
  [Serializable]
  public class TypeDelegator : TypeInfo
  {
    /// <summary>一个指示类型信息的值。</summary>
    protected Type typeImpl;

    /// <summary>获取已实现类型的 GUID（全局唯一标识符）。</summary>
    /// <returns>一个 GUID。</returns>
    public override Guid GUID
    {
      get
      {
        return this.typeImpl.GUID;
      }
    }

    /// <summary>获取一个标识元数据中的此实体的值。</summary>
    /// <returns>一个值，与模块一起来唯一标识元数据中的此实体。</returns>
    public override int MetadataToken
    {
      get
      {
        return this.typeImpl.MetadataToken;
      }
    }

    /// <summary>获取包含已实现类型的模块。</summary>
    /// <returns>表示已实现类型的模块的 <see cref="T:System.Reflection.Module" /> 对象。</returns>
    public override Module Module
    {
      get
      {
        return this.typeImpl.Module;
      }
    }

    /// <summary>获取已实现类型的程序集。</summary>
    /// <returns>表示已实现类型的程序集的 <see cref="T:System.Reflection.Assembly" /> 对象。</returns>
    public override Assembly Assembly
    {
      get
      {
        return this.typeImpl.Assembly;
      }
    }

    /// <summary>获取已实现类型的内部元数据表示形式的句柄。</summary>
    /// <returns>一个 RuntimeTypeHandle 对象。</returns>
    public override RuntimeTypeHandle TypeHandle
    {
      get
      {
        return this.typeImpl.TypeHandle;
      }
    }

    /// <summary>获取移除了路径的已实现类型的名称。</summary>
    /// <returns>包含类型的非限定名的 String。</returns>
    public override string Name
    {
      get
      {
        return this.typeImpl.Name;
      }
    }

    /// <summary>获取已实现类型的完全限定名。</summary>
    /// <returns>包含类型的完全限定名的 String。</returns>
    public override string FullName
    {
      get
      {
        return this.typeImpl.FullName;
      }
    }

    /// <summary>获取已实现类型的命名空间。</summary>
    /// <returns>包含类型的命名空间的 String。</returns>
    public override string Namespace
    {
      get
      {
        return this.typeImpl.Namespace;
      }
    }

    /// <summary>获取程序集的完全限定名。</summary>
    /// <returns>包含程序集的完全限定名的 String。</returns>
    public override string AssemblyQualifiedName
    {
      get
      {
        return this.typeImpl.AssemblyQualifiedName;
      }
    }

    /// <summary>获取当前类型的基类型。</summary>
    /// <returns>类型的基类型。</returns>
    public override Type BaseType
    {
      get
      {
        return this.typeImpl.BaseType;
      }
    }

    /// <summary>获取指示此对象是否表示构造的泛型类型的值。</summary>
    /// <returns>如果此对象表示构造泛型类型，则为 true；否则为 false。</returns>
    public override bool IsConstructedGenericType
    {
      get
      {
        return this.typeImpl.IsConstructedGenericType;
      }
    }

    /// <summary>获取表示已实现类型的基础 <see cref="T:System.Type" />。</summary>
    /// <returns>基础类型。</returns>
    public override Type UnderlyingSystemType
    {
      get
      {
        return this.typeImpl.UnderlyingSystemType;
      }
    }

    /// <summary>使用默认属性初始化 <see cref="T:System.Reflection.TypeDelegator" /> 类的新实例。</summary>
    protected TypeDelegator()
    {
    }

    /// <summary>在指定封装实例的情况下，初始化 <see cref="T:System.Reflection.TypeDelegator" /> 类的新实例。</summary>
    /// <param name="delegatingType">
    /// <see cref="T:System.Type" /> 类的实例，它封装对对象方法的调用。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="delegatingType" /> 为 null。</exception>
    public TypeDelegator(Type delegatingType)
    {
      if (delegatingType == (Type) null)
        throw new ArgumentNullException("delegatingType");
      this.typeImpl = delegatingType;
    }

    /// <summary>返回一个值，该值指示指定类型是否可分配给此类型。</summary>
    /// <returns>如果可以将指定类型分配给此类型，则为 true；否则为 false。</returns>
    /// <param name="typeInfo">要检查的类型。</param>
    public override bool IsAssignableFrom(TypeInfo typeInfo)
    {
      if ((Type) typeInfo == (Type) null)
        return false;
      return this.IsAssignableFrom(typeInfo.AsType());
    }

    /// <summary>调用指定的成员。在指定联编程序和调用属性的约束下，要调用的方法必须是可访问的，而且提供与指定参数列表最精确的匹配。</summary>
    /// <returns>表示被调用成员的返回值的 Object。</returns>
    /// <param name="name">要调用的成员名。这可能是一个构造函数、方法、属性或字段。如果传递了空字符串 ("")，则调用默认成员。</param>
    /// <param name="invokeAttr">调用属性。这必须是下列之一：<see cref="T:System.Reflection.BindingFlags" />、InvokeMethod、CreateInstance、Static、GetField、SetField、GetProperty 或 SetProperty。必须指定合适的调用属性。如果要调用静态成员，则必须设置 Static 标志。</param>
    /// <param name="binder">一个启用绑定、参数类型强制、成员调用以及通过反射进行 MemberInfo 对象检索的对象。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。请参见<see cref="T:System.Reflection.Binder" />。</param>
    /// <param name="target">对其调用指定成员的对象。</param>
    /// <param name="args">Object 类型的数组，包含要调用的成员参数的数目、顺序和类型。如果 <paramref name="args" /> 包含未初始化的 Object，则它被视为空，用默认联编程序可将它扩展为 0、0.0 或一个字符串。</param>
    /// <param name="modifiers">ParameterModifer 类型的数组，其长度与 <paramref name="args" /> 相同，其元素表示与要调用的成员参数关联的属性。参数在成员的签名中有与其关联的属性。对于 ByRef，请使用 ParameterModifer.ByRef；对于空，请使用 ParameterModifer.None。默认联编程序执行与这些内容的精确匹配。In 和 InOut 这样的属性不用于绑定，可以使用 ParameterInfo 查看它们。</param>
    /// <param name="culture">用于控制类型强制的 CultureInfo 的实例。类型强制在某些情况下是必要的，例如将表示 1000 的字符串转换为 Double 值，因为不同的区域性表示 1000 的方式不同。如果 <paramref name="culture" /> 为 null，则使用当前线程的 CultureInfo 的 CultureInfo。</param>
    /// <param name="namedParameters">String 类型的数组，包含与 <paramref name="args" /> 数组匹配的参数名（从元素零开始）。数组中不能有空缺。如果为 <paramref name="args" />，Length 大于 <paramref name="namedParameters" />。Length，则按顺序填充剩余的参数。</param>
    public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
    {
      return this.typeImpl.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
    }

    /// <summary>获取实现 TypeDelegator 的构造函数。</summary>
    /// <returns>此方法的匹配指定判据的 ConstructorInfo 对象；如果无法找到匹配项，则为 null。</returns>
    /// <param name="bindingAttr">影响执行搜索的方式的位屏蔽。该值是零个或多个来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志的组合。</param>
    /// <param name="binder">一个对象，它使用反射启用绑定、参数类型的强制、成员的调用和 MemberInfo 对象的检索。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="callConvention">调用约定。</param>
    /// <param name="types">Type 类型的数组，包含参数数量、顺序和类型的列表。类型不能为 null；使用相应的 GetMethod 方法或空数组搜索不带参数的方法。</param>
    /// <param name="modifiers">ParameterModifier 类型的数组，它与 <paramref name="types" /> 数组的长度相同，后者的元素表示与要获取的方法的参数关联的属性。</param>
    protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      return this.typeImpl.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
    }

    /// <summary>返回 <see cref="T:System.Reflection.ConstructorInfo" /> 对象的数组，这些对象表示为当前 <see cref="T:System.Reflection.TypeDelegator" /> 包装的类型定义的构造函数。</summary>
    /// <returns>ConstructorInfo 类型的数组，包含为此类定义的指定的构造函数。如果未定义任何构造函数，则返回空数组。根据指定参数的值，只返回公共构造函数或同时返回公共和非公共构造函数。</returns>
    /// <param name="bindingAttr">影响执行搜索的方式的位屏蔽。该值是零个或多个来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志的组合。</param>
    [ComVisible(true)]
    public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
    {
      return this.typeImpl.GetConstructors(bindingAttr);
    }

    /// <summary>用指定的绑定约束和指定的调用约定，搜索参数与指定的参数类型及修饰符相匹配的指定方法。</summary>
    /// <returns>匹配指定条件的实现方法的 MethodInfoInfo 对象；如果无法找到匹配项，则为 null。</returns>
    /// <param name="name">方法名。</param>
    /// <param name="bindingAttr">影响执行搜索的方式的位屏蔽。该值是零个或多个来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志的组合。</param>
    /// <param name="binder">一个对象，它使用反射启用绑定、参数类型的强制、成员的调用和 MemberInfo 对象的检索。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="callConvention">调用约定。</param>
    /// <param name="types">Type 类型的数组，包含参数数量、顺序和类型的列表。类型不能为 null；使用相应的 GetMethod 方法或空数组搜索不带参数的方法。</param>
    /// <param name="modifiers">ParameterModifier 类型的数组，它与 <paramref name="types" /> 数组的长度相同，后者的元素表示与要获取的方法的参数关联的属性。</param>
    protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      if (types == null)
        return this.typeImpl.GetMethod(name, bindingAttr);
      return this.typeImpl.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
    }

    /// <summary>返回 <see cref="T:System.Reflection.MethodInfo" /> 对象的数组，这些对象表示由当前 <see cref="T:System.Reflection.TypeDelegator" /> 包装的类型的指定方法。</summary>
    /// <returns>MethodInfo 对象的数组，表示在此 TypeDelegator 上定义的方法。</returns>
    /// <param name="bindingAttr">影响执行搜索的方式的位屏蔽。该值是零个或多个来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志的组合。</param>
    public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
    {
      return this.typeImpl.GetMethods(bindingAttr);
    }

    /// <summary>返回 <see cref="T:System.Reflection.FieldInfo" /> 对象，该对象表示具有指定名称的字段。</summary>
    /// <returns>FieldInfo 对象，表示由此 TypeDelegator 声明或继承的具有指定名称的字段。如果未找到这样的字段，则返回 null。</returns>
    /// <param name="name">要查找的字段的名称。</param>
    /// <param name="bindingAttr">影响执行搜索的方式的位屏蔽。该值是零个或多个来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志的组合。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    public override FieldInfo GetField(string name, BindingFlags bindingAttr)
    {
      return this.typeImpl.GetField(name, bindingAttr);
    }

    /// <summary>返回 <see cref="T:System.Reflection.FieldInfo" /> 对象的数组，这些对象表示为当前 <see cref="T:System.Reflection.TypeDelegator" /> 包装的类型定义的数据字段。</summary>
    /// <returns>FieldInfo 类型的数组，包含由当前 TypeDelegator 声明或继承的字段。如果没有匹配的字段，则返回空数组。</returns>
    /// <param name="bindingAttr">影响执行搜索的方式的位屏蔽。该值是零个或多个来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志的组合。</param>
    public override FieldInfo[] GetFields(BindingFlags bindingAttr)
    {
      return this.typeImpl.GetFields(bindingAttr);
    }

    /// <summary>返回由当前 <see cref="T:System.Reflection.TypeDelegator" /> 包装的类型实现的指定接口。</summary>
    /// <returns>Type 对象，表示由当前类（直接或间接）实现的具有匹配指定名称的完全限定名的接口。如果未找到匹配名称的接口，则返回 null。</returns>
    /// <param name="name">由当前类实现的接口的完全限定名。</param>
    /// <param name="ignoreCase">如果忽略大小写，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    public override Type GetInterface(string name, bool ignoreCase)
    {
      return this.typeImpl.GetInterface(name, ignoreCase);
    }

    /// <summary>返回在当前类及其基类上实现的所有接口。</summary>
    /// <returns>Type 类型的数组，包含在当前类及其基类上实现的所有接口。如果一个都没有定义，则返回空数组。</returns>
    public override Type[] GetInterfaces()
    {
      return this.typeImpl.GetInterfaces();
    }

    /// <summary>返回指定事件。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.EventInfo" /> 对象，表示由此类型声明或继承的具有指定名称的事件。如果未找到此类事件，则此方法返回 null。</returns>
    /// <param name="name">要获取的事件的名称。</param>
    /// <param name="bindingAttr">影响执行搜索的方式的位屏蔽。该值是零个或多个来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志的组合。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
    {
      return this.typeImpl.GetEvent(name, bindingAttr);
    }

    /// <summary>返回 <see cref="T:System.Reflection.EventInfo" /> 对象的数组，这些对象表示由当前 TypeDelegator 声明或继承的所有公共事件。</summary>
    /// <returns>返回 EventInfo 类型的数组，该数组包含由当前类型声明或继承的所有事件。如果没有事件，则返回空数组。</returns>
    public override EventInfo[] GetEvents()
    {
      return this.typeImpl.GetEvents();
    }

    /// <summary>当在派生类中重写时，使用指定的绑定约束搜索其参数与指定的参数类型和修饰符匹配的指定属性。</summary>
    /// <returns>此属性的匹配指定条件的 <see cref="T:System.Reflection.PropertyInfo" /> 对象；如果无法找到匹配，则为 null。</returns>
    /// <param name="name">要获取的属性。</param>
    /// <param name="bindingAttr">影响执行搜索的方式的位屏蔽。该值是零个或多个来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志的组合。</param>
    /// <param name="binder">一个启用绑定、参数类型强制、成员调用以及通过反射进行 MemberInfo 对象检索的对象。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。请参见<see cref="T:System.Reflection.Binder" />。</param>
    /// <param name="returnType">属性的返回类型。</param>
    /// <param name="types">参数类型的列表。此列表表示参数的数目、顺序和类型。类型不能为 null；使用相应的 GetMethod 方法或空数组搜索不带参数的方法。</param>
    /// <param name="modifiers">长度与 types 相同的数组，其元素表示与要获取的方法参数关联的属性。</param>
    protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
    {
      if (returnType == (Type) null && types == null)
        return this.typeImpl.GetProperty(name, bindingAttr);
      return this.typeImpl.GetProperty(name, bindingAttr, binder, returnType, types, modifiers);
    }

    /// <summary>返回 <see cref="T:System.Reflection.PropertyInfo" /> 对象的数组，这些对象表示由当前 <see cref="T:System.Reflection.TypeDelegator" /> 包装的类型的属性。</summary>
    /// <returns>PropertyInfo 对象的数组，表示在此 TypeDelegator 上定义的属性。</returns>
    /// <param name="bindingAttr">影响执行搜索的方式的位屏蔽。该值是零个或多个来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志的组合。</param>
    public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
    {
      return this.typeImpl.GetProperties(bindingAttr);
    }

    /// <summary>返回 <paramref name="bindingAttr" /> 中指定的由当前 TypeDelegator 声明或继承的事件。</summary>
    /// <returns>EventInfo 类型的数组，包含 <paramref name="bindingAttr" /> 中指定的事件。如果没有事件，则返回空数组。</returns>
    /// <param name="bindingAttr">影响执行搜索的方式的位屏蔽。该值是零个或多个来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志的组合。</param>
    public override EventInfo[] GetEvents(BindingFlags bindingAttr)
    {
      return this.typeImpl.GetEvents(bindingAttr);
    }

    /// <summary>返回嵌套类型，这些嵌套类型是在 <paramref name="bindingAttr" /> 中指定的，并且由当前 <see cref="T:System.Reflection.TypeDelegator" /> 包装的类型来声明或继承。</summary>
    /// <returns>包含嵌套类型的 Type 类型数组。</returns>
    /// <param name="bindingAttr">影响执行搜索的方式的位屏蔽。该值是零个或多个来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志的组合。</param>
    public override Type[] GetNestedTypes(BindingFlags bindingAttr)
    {
      return this.typeImpl.GetNestedTypes(bindingAttr);
    }

    /// <summary>返回由 <paramref name="name" /> 指定的嵌套类型，并且该嵌套类型是在 <paramref name="bindingAttr" /> 中指定的、由当前 <see cref="T:System.Reflection.TypeDelegator" /> 表示的类型来声明或继承。</summary>
    /// <returns>表示嵌套类型的 Type 对象。</returns>
    /// <param name="name">嵌套类型的名称。</param>
    /// <param name="bindingAttr">影响执行搜索的方式的位屏蔽。该值是零个或多个来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志的组合。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    public override Type GetNestedType(string name, BindingFlags bindingAttr)
    {
      return this.typeImpl.GetNestedType(name, bindingAttr);
    }

    /// <summary>返回由给定的 <paramref name="name" />、<paramref name="type" /> 和 <paramref name="bindingAttr" /> 指定的成员（属性、方法、构造函数、字段、事件和嵌套类型）。</summary>
    /// <returns>MemberInfo 类型的数组，包含当前类及其基类的符合指定条件的所有成员。</returns>
    /// <param name="name">要获取的成员名称。</param>
    /// <param name="type">影响执行搜索的方式的位屏蔽。该值是零个或多个来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志的组合。</param>
    /// <param name="bindingAttr">要获取的成员类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
    {
      return this.typeImpl.GetMember(name, type, bindingAttr);
    }

    /// <summary>返回由 <paramref name="bindingAttr" /> 指定的成员。</summary>
    /// <returns>MemberInfo 类型的数组，包含当前类及其基类的符合 <paramref name="bindingAttr" /> 筛选器的所有成员。</returns>
    /// <param name="bindingAttr">影响执行搜索的方式的位屏蔽。该值是零个或多个来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志的组合。</param>
    public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
    {
      return this.typeImpl.GetMembers(bindingAttr);
    }

    /// <summary>获取分配给 TypeDelegator 的属性。</summary>
    /// <returns>表示实现属性标志的 TypeAttributes 对象。</returns>
    protected override TypeAttributes GetAttributeFlagsImpl()
    {
      return this.typeImpl.Attributes;
    }

    /// <summary>返回一个值，该值指示 <see cref="T:System.Type" /> 是否为数组。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 是数组，则为 true；否则为 false。</returns>
    protected override bool IsArrayImpl()
    {
      return this.typeImpl.IsArray;
    }

    /// <summary>返回一个值，该值指示 <see cref="T:System.Type" /> 是否为基元类型之一。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 为基元类型之一，则为 true；否则为 false。</returns>
    protected override bool IsPrimitiveImpl()
    {
      return this.typeImpl.IsPrimitive;
    }

    /// <summary>返回指示 <see cref="T:System.Type" /> 是否由引用传递的值。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 按引用传递，则为 true；否则为 false。</returns>
    protected override bool IsByRefImpl()
    {
      return this.typeImpl.IsByRef;
    }

    /// <summary>返回一个值，该值指示 <see cref="T:System.Type" /> 是否为指针。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 是指针，则为 true；否则为 false。</returns>
    protected override bool IsPointerImpl()
    {
      return this.typeImpl.IsPointer;
    }

    /// <summary>返回一个值，该值指示此类型是否为值类型（即不是类或接口）。</summary>
    /// <returns>如果该类型是值类型，则为 true；否则为 false。</returns>
    protected override bool IsValueTypeImpl()
    {
      return this.typeImpl.IsValueType;
    }

    /// <summary>返回一个值，该值指示 <see cref="T:System.Type" /> 是否为 COM 对象。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 为 COM 对象，则为 true；否则为 false。</returns>
    protected override bool IsCOMObjectImpl()
    {
      return this.typeImpl.IsCOMObject;
    }

    /// <summary>返回由当前数组、指针或 ByRef 包含或引用的对象的 <see cref="T:System.Type" />。</summary>
    /// <returns>由当前数组、指针或 ByRef 包含或引用的对象的 <see cref="T:System.Type" />；如果当前 <see cref="T:System.Type" /> 不是数组、指针或 ByRef，则为 null。</returns>
    public override Type GetElementType()
    {
      return this.typeImpl.GetElementType();
    }

    /// <summary>获取一个值，该值指示当前 <see cref="T:System.Type" /> 是否包含或引用其他类型，即当前 <see cref="T:System.Type" /> 是数组、指针还是 ByRef。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 是数组、指针或 ByRef，则为 true；否则为 false。</returns>
    protected override bool HasElementTypeImpl()
    {
      return this.typeImpl.HasElementType;
    }

    /// <summary>返回为此类型定义的所有自定义属性，并指定是否搜索此类型的继承链。</summary>
    /// <returns>一个包含为此类型定义的所有自定义属性的对象数组。</returns>
    /// <param name="inherit">指定是否搜索此类型的继承链以查找这些属性。</param>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    public override object[] GetCustomAttributes(bool inherit)
    {
      return this.typeImpl.GetCustomAttributes(inherit);
    }

    /// <summary>返回由类型标识的自定义属性数组。</summary>
    /// <returns>一个对象数组，包含此类型中定义的与 <paramref name="attributeType" /> 参数匹配的自定义属性，并指定是否搜索此类型的继承链；如果在此类型上未定义自定义属性，则为 null。</returns>
    /// <param name="attributeType">由类型标识的自定义属性数组。</param>
    /// <param name="inherit">指定是否搜索此类型的继承链以查找这些属性。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载自定义特性类型。</exception>
    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      return this.typeImpl.GetCustomAttributes(attributeType, inherit);
    }

    /// <summary>指示是否定义由 <paramref name="attributeType" /> 标识的自定义属性。</summary>
    /// <returns>如果定义由 <paramref name="attributeType" /> 标识的自定义属性，则为 true；否则为 false。</returns>
    /// <param name="attributeType">指定是否搜索此类型的继承链以查找这些属性。</param>
    /// <param name="inherit">由类型标识的自定义属性数组。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.Reflection.ReflectionTypeLoadException">无法加载自定义特性类型。</exception>
    public override bool IsDefined(Type attributeType, bool inherit)
    {
      return this.typeImpl.IsDefined(attributeType, inherit);
    }

    /// <summary>返回指定接口类型的接口映射。</summary>
    /// <returns>表示 <paramref name="interfaceType" /> 的接口映射的 <see cref="T:System.Reflection.InterfaceMapping" /> 对象。</returns>
    /// <param name="interfaceType">要检索其映射的接口的 <see cref="T:System.Type" />。</param>
    [ComVisible(true)]
    public override InterfaceMapping GetInterfaceMap(Type interfaceType)
    {
      return this.typeImpl.GetInterfaceMap(interfaceType);
    }
  }
}
