// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.GenericTypeParameterBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>为动态定义的泛型类型与方法定义和创建泛型类型参数。此类不能被继承。</summary>
  [ComVisible(true)]
  public sealed class GenericTypeParameterBuilder : TypeInfo
  {
    internal TypeBuilder m_type;

    /// <summary>获取泛型类型参数所属的泛型类型定义或泛型方法定义。</summary>
    /// <returns>如果类型参数属于某个泛型类型，则为表示该泛型类型的 <see cref="T:System.Type" /> 对象；如果类型参数属于某个泛型方法，则为表示声明该泛型方法的类型的 <see cref="T:System.Type" /> 对象。</returns>
    public override Type DeclaringType
    {
      get
      {
        return this.m_type.DeclaringType;
      }
    }

    /// <summary>获取用于获取 <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> 的 <see cref="T:System.Type" /> 对象。</summary>
    /// <returns>用于获取 <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> 的 <see cref="T:System.Type" /> 对象。</returns>
    public override Type ReflectedType
    {
      get
      {
        return this.m_type.ReflectedType;
      }
    }

    /// <summary>获取泛型类型参数的名称。</summary>
    /// <returns>泛型类型参数的名称。</returns>
    public override string Name
    {
      get
      {
        return this.m_type.Name;
      }
    }

    /// <summary>获取包含泛型类型参数的动态模块。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.Module" /> 对象，该对象表示包含泛型类型参数的动态模块。</returns>
    public override Module Module
    {
      get
      {
        return this.m_type.Module;
      }
    }

    internal int MetadataTokenInternal
    {
      get
      {
        return this.m_type.MetadataTokenInternal;
      }
    }

    /// <summary>对于不完整的泛型类型参数不支持。</summary>
    /// <returns>对于不完整的泛型类型参数不支持。</returns>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。</exception>
    public override Guid GUID
    {
      get
      {
        throw new NotSupportedException();
      }
    }

    /// <summary>获取一个表示动态程序集的 <see cref="T:System.Reflection.Assembly" /> 对象，该动态程序集包含当前类型参数所属的泛型类型定义。</summary>
    /// <returns>一个表示动态程序集的 <see cref="T:System.Reflection.Assembly" /> 对象，该动态程序集包含当前类型参数所属的泛型类型定义。</returns>
    public override Assembly Assembly
    {
      get
      {
        return this.m_type.Assembly;
      }
    }

    /// <summary>对于不完整的泛型类型参数不支持。</summary>
    /// <returns>对于不完整的泛型类型参数不支持。</returns>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。</exception>
    public override RuntimeTypeHandle TypeHandle
    {
      get
      {
        throw new NotSupportedException();
      }
    }

    /// <summary>在所有情况下均获取 null。</summary>
    /// <returns>在所有情况下均为空引用（在 Visual Basic 中为 Nothing）。</returns>
    public override string FullName
    {
      get
      {
        return (string) null;
      }
    }

    /// <summary>在所有情况下均获取 null。</summary>
    /// <returns>在所有情况下均为空引用（在 Visual Basic 中为 Nothing）。</returns>
    public override string Namespace
    {
      get
      {
        return (string) null;
      }
    }

    /// <summary>在所有情况下均获取 null。</summary>
    /// <returns>在所有情况下均为空引用（在 Visual Basic 中为 Nothing）。</returns>
    public override string AssemblyQualifiedName
    {
      get
      {
        return (string) null;
      }
    }

    /// <summary>获取当前泛型类型参数的基类型约束。</summary>
    /// <returns>为一个表示泛型类型参数的基类型约束的 <see cref="T:System.Type" /> 对象，或者为 null（如果类型参数没有基类型约束）。</returns>
    public override Type BaseType
    {
      get
      {
        return this.m_type.BaseType;
      }
    }

    /// <summary>获取当前泛型类型参数。</summary>
    /// <returns>当前的 <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> 对象。</returns>
    public override Type UnderlyingSystemType
    {
      get
      {
        return (Type) this;
      }
    }

    /// <summary>在所有情况下均获取 false。</summary>
    /// <returns>所有情况下均为 false。</returns>
    public override bool IsGenericTypeDefinition
    {
      get
      {
        return false;
      }
    }

    /// <summary>在所有情况下均返回 false。</summary>
    /// <returns>所有情况下均为 false。</returns>
    public override bool IsGenericType
    {
      get
      {
        return false;
      }
    }

    /// <summary>在所有情况下均获取 true。</summary>
    /// <returns>任何情况下都为 true。</returns>
    public override bool IsGenericParameter
    {
      get
      {
        return true;
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

    /// <summary>获取类型参数在声明该参数的泛型类型或方法的类型参数列表中的位置。</summary>
    /// <returns>类型参数在声明该参数的泛型类型或方法的类型参数列表中的位置。</returns>
    public override int GenericParameterPosition
    {
      get
      {
        return this.m_type.GenericParameterPosition;
      }
    }

    /// <summary>在所有情况下均获取 true。</summary>
    /// <returns>任何情况下都为 true。</returns>
    public override bool ContainsGenericParameters
    {
      get
      {
        return this.m_type.ContainsGenericParameters;
      }
    }

    public override GenericParameterAttributes GenericParameterAttributes
    {
      get
      {
        return this.m_type.GenericParameterAttributes;
      }
    }

    /// <summary>获取一个表示声明方法的 <see cref="T:System.Reflection.MethodInfo" />（如果当前 <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> 表示泛型方法的一个类型参数）。</summary>
    /// <returns>如果当前 <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> 表示泛型方法的一个类型参数，则为一个表示声明方法的 <see cref="T:System.Reflection.MethodInfo" />；否则为 null。</returns>
    public override MethodBase DeclaringMethod
    {
      get
      {
        return this.m_type.DeclaringMethod;
      }
    }

    internal GenericTypeParameterBuilder(TypeBuilder type)
    {
      this.m_type = type;
    }

    /// <summary>任何情况下均引发 <see cref="T:System.NotSupportedException" /> 异常。</summary>
    /// <returns>任何情况下均引发 <see cref="T:System.NotSupportedException" /> 异常。</returns>
    /// <param name="typeInfo">要测试的对象。</param>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。</exception>
    public override bool IsAssignableFrom(TypeInfo typeInfo)
    {
      if ((Type) typeInfo == (Type) null)
        return false;
      return this.IsAssignableFrom(typeInfo.AsType());
    }

    /// <summary>返回当前泛型类型参数的字符串表示形式。</summary>
    /// <returns>包含泛型类型参数名称的字符串。</returns>
    public override string ToString()
    {
      return this.m_type.Name;
    }

    /// <summary>测试给定的对象是否为 EventToken 的实例，并检查它是否与当前实例相等。</summary>
    /// <returns>如果 <paramref name="o" /> 为 EventToken 的实例并等于当前实例，则返回 true；否则返回 false。</returns>
    /// <param name="o">要与当前实例进行比较的对象。</param>
    public override bool Equals(object o)
    {
      GenericTypeParameterBuilder parameterBuilder = o as GenericTypeParameterBuilder;
      if ((Type) parameterBuilder == (Type) null)
        return false;
      return parameterBuilder.m_type == this.m_type;
    }

    /// <summary>返回当前实例的 32 位整数哈希代码。</summary>
    /// <returns>32 位整数哈希代码。</returns>
    public override int GetHashCode()
    {
      return this.m_type.GetHashCode();
    }

    /// <summary>返回一个 <see cref="T:System.Type" /> 对象，该对象表示指向当前泛型类型参数的指针。</summary>
    /// <returns>一个 <see cref="T:System.Type" /> 对象，表示指向当前泛型类型参数的指针。</returns>
    public override Type MakePointerType()
    {
      return SymbolType.FormCompoundType("*".ToCharArray(), (Type) this, 0);
    }

    /// <summary>返回一个表示当前泛型类型参数的 <see cref="T:System.Type" /> 对象（作为引用参数传递时）。</summary>
    /// <returns>一个表示当前泛型类型参数的 <see cref="T:System.Type" /> 对象（作为引用参数传递时）。</returns>
    public override Type MakeByRefType()
    {
      return SymbolType.FormCompoundType("&".ToCharArray(), (Type) this, 0);
    }

    /// <summary>返回元素类型为泛型类型参数的一维数组的类型。</summary>
    /// <returns>一个表示元素类型为泛型类型参数的一维数组类型的 <see cref="T:System.Type" /> 对象。</returns>
    public override Type MakeArrayType()
    {
      return SymbolType.FormCompoundType("[]".ToCharArray(), (Type) this, 0);
    }

    /// <summary>返回数组的类型，该数组的元素类型为泛型类型参数，且具有指定维数。</summary>
    /// <returns>一个表示数组类型的 <see cref="T:System.Type" /> 对象，该数组的元素类型为泛型类型参数，且具有指定维数。</returns>
    /// <param name="rank">数组的维数。</param>
    /// <exception cref="T:System.IndexOutOfRangeException">
    /// <paramref name="rank" /> 不是有效的维数。例如，其值小于 1。</exception>
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
      return (Type) (SymbolType.FormCompoundType(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "[{0}]", (object) str).ToCharArray(), (Type) this, 0) as SymbolType);
    }

    /// <summary>对于不完整的泛型类型参数不支持。</summary>
    /// <returns>对于不完整的泛型类型参数不支持。</returns>
    /// <param name="name">不支持。</param>
    /// <param name="invokeAttr">不支持。</param>
    /// <param name="binder">不支持。</param>
    /// <param name="target">不支持。</param>
    /// <param name="args">不支持。</param>
    /// <param name="modifiers">不支持。</param>
    /// <param name="culture">不支持。</param>
    /// <param name="namedParameters">不支持。</param>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。</exception>
    public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
    {
      throw new NotSupportedException();
    }

    protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      throw new NotSupportedException();
    }

    /// <summary>对于不完整的泛型类型参数不支持。</summary>
    /// <returns>对于不完整的泛型类型参数不支持。</returns>
    /// <param name="bindingAttr">不支持。</param>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。</exception>
    [ComVisible(true)]
    public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      throw new NotSupportedException();
    }

    /// <summary>对于不完整的泛型类型参数不支持。</summary>
    /// <returns>对于不完整的泛型类型参数不支持。</returns>
    /// <param name="bindingAttr">不支持。</param>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。</exception>
    public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    /// <summary>对于不完整的泛型类型参数不支持。</summary>
    /// <returns>对于不完整的泛型类型参数不支持。</returns>
    /// <param name="name">不支持。</param>
    /// <param name="bindingAttr">不支持。</param>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。</exception>
    public override FieldInfo GetField(string name, BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    /// <summary>对于不完整的泛型类型参数不支持。</summary>
    /// <returns>对于不完整的泛型类型参数不支持。</returns>
    /// <param name="bindingAttr">不支持。</param>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。</exception>
    public override FieldInfo[] GetFields(BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    /// <summary>对于不完整的泛型类型参数不支持。</summary>
    /// <returns>对于不完整的泛型类型参数不支持。</returns>
    /// <param name="name">接口名。</param>
    /// <param name="ignoreCase">如果为 true，则搜索时不考虑大小写；如果为 false，则搜索时区分大小写。</param>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。</exception>
    public override Type GetInterface(string name, bool ignoreCase)
    {
      throw new NotSupportedException();
    }

    /// <summary>对于不完整的泛型类型参数不支持。</summary>
    /// <returns>对于不完整的泛型类型参数不支持。</returns>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。</exception>
    public override Type[] GetInterfaces()
    {
      throw new NotSupportedException();
    }

    /// <summary>对于不完整的泛型类型参数不支持。</summary>
    /// <returns>对于不完整的泛型类型参数不支持。</returns>
    /// <param name="name">不支持。</param>
    /// <param name="bindingAttr">不支持。</param>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。</exception>
    public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    /// <summary>对于不完整的泛型类型参数不支持。</summary>
    /// <returns>对于不完整的泛型类型参数不支持。</returns>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。</exception>
    public override EventInfo[] GetEvents()
    {
      throw new NotSupportedException();
    }

    protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
    {
      throw new NotSupportedException();
    }

    /// <summary>对于不完整的泛型类型参数不支持。</summary>
    /// <returns>对于不完整的泛型类型参数不支持。</returns>
    /// <param name="bindingAttr">不支持。</param>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。</exception>
    public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    /// <summary>对于不完整的泛型类型参数不支持。</summary>
    /// <returns>对于不完整的泛型类型参数不支持。</returns>
    /// <param name="bindingAttr">不支持。</param>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。</exception>
    public override Type[] GetNestedTypes(BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    /// <summary>对于不完整的泛型类型参数不支持。</summary>
    /// <returns>对于不完整的泛型类型参数不支持。</returns>
    /// <param name="name">不支持。</param>
    /// <param name="bindingAttr">不支持。</param>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。</exception>
    public override Type GetNestedType(string name, BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    /// <summary>对于不完整的泛型类型参数不支持。</summary>
    /// <returns>对于不完整的泛型类型参数不支持。</returns>
    /// <param name="name">不支持。</param>
    /// <param name="type">不支持。</param>
    /// <param name="bindingAttr">不支持。</param>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。</exception>
    public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    /// <summary>对于不完整的泛型类型参数不支持。</summary>
    /// <returns>对于不完整的泛型类型参数不支持。</returns>
    /// <param name="interfaceType">一个表示接口类型（针对该接口类型检索映射）的 <see cref="T:System.Type" /> 对象。</param>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。</exception>
    [ComVisible(true)]
    public override InterfaceMapping GetInterfaceMap(Type interfaceType)
    {
      throw new NotSupportedException();
    }

    /// <summary>对于不完整的泛型类型参数不支持。</summary>
    /// <returns>对于不完整的泛型类型参数不支持。</returns>
    /// <param name="bindingAttr">不支持。</param>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。</exception>
    public override EventInfo[] GetEvents(BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    /// <summary>对于不完整的泛型类型参数不支持。</summary>
    /// <returns>对于不完整的泛型类型参数不支持。</returns>
    /// <param name="bindingAttr">不支持。</param>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。</exception>
    public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
    {
      throw new NotSupportedException();
    }

    protected override TypeAttributes GetAttributeFlagsImpl()
    {
      return TypeAttributes.Public;
    }

    protected override bool IsArrayImpl()
    {
      return false;
    }

    protected override bool IsByRefImpl()
    {
      return false;
    }

    protected override bool IsPointerImpl()
    {
      return false;
    }

    protected override bool IsPrimitiveImpl()
    {
      return false;
    }

    protected override bool IsCOMObjectImpl()
    {
      return false;
    }

    /// <summary>在所有情况下均引发 <see cref="T:System.NotSupportedException" />。</summary>
    /// <returns>当前数组类型、指针类型引用的类型，或者为 ByRef 类型；如果当前类型不为数组类型或指针类型，并且不由引用传递，则为 null。</returns>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。</exception>
    public override Type GetElementType()
    {
      throw new NotSupportedException();
    }

    protected override bool HasElementTypeImpl()
    {
      return false;
    }

    /// <summary>对于泛型类型参数无效。</summary>
    /// <returns>对于泛型类型参数无效。</returns>
    /// <exception cref="T:System.InvalidOperationException">在所有情况下。</exception>
    public override Type[] GetGenericArguments()
    {
      throw new InvalidOperationException();
    }

    /// <summary>对于泛型类型参数无效。</summary>
    /// <returns>对于泛型类型参数无效。</returns>
    /// <exception cref="T:System.InvalidOperationException">在所有情况下。</exception>
    public override Type GetGenericTypeDefinition()
    {
      throw new InvalidOperationException();
    }

    /// <summary>对于不完整的泛型类型参数无效。</summary>
    /// <returns>此方法对不完整的泛型类型参数无效。</returns>
    /// <param name="typeArguments">类型参数数组。</param>
    /// <exception cref="T:System.InvalidOperationException">在所有情况下。</exception>
    public override Type MakeGenericType(params Type[] typeArguments)
    {
      throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericTypeDefinition"));
    }

    protected override bool IsValueTypeImpl()
    {
      return false;
    }

    /// <summary>任何情况下均引发 <see cref="T:System.NotSupportedException" /> 异常。</summary>
    /// <returns>任何情况下均引发 <see cref="T:System.NotSupportedException" /> 异常。</returns>
    /// <param name="c">要测试的对象。</param>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。</exception>
    public override bool IsAssignableFrom(Type c)
    {
      throw new NotSupportedException();
    }

    /// <summary>对于不完整的泛型类型参数不支持。</summary>
    /// <returns>对于不完整的泛型类型参数不支持。</returns>
    /// <param name="c">不支持。</param>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。</exception>
    [ComVisible(true)]
    public override bool IsSubclassOf(Type c)
    {
      throw new NotSupportedException();
    }

    /// <summary>对于不完整的泛型类型参数不支持。</summary>
    /// <returns>对于不完整的泛型类型参数不支持。</returns>
    /// <param name="inherit">指定是否搜索该成员的继承链以查找这些属性。</param>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。</exception>
    public override object[] GetCustomAttributes(bool inherit)
    {
      throw new NotSupportedException();
    }

    /// <summary>对于不完整的泛型类型参数不支持。</summary>
    /// <returns>对于不完整的泛型类型参数不支持。</returns>
    /// <param name="attributeType">要搜索的特性类型。只返回可分配给此类型的属性。</param>
    /// <param name="inherit">指定是否搜索该成员的继承链以查找这些属性。</param>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。</exception>
    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      throw new NotSupportedException();
    }

    /// <summary>对于不完整的泛型类型参数不支持。</summary>
    /// <returns>对于不完整的泛型类型参数不支持。</returns>
    /// <param name="attributeType">不支持。</param>
    /// <param name="inherit">不支持。</param>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。</exception>
    public override bool IsDefined(Type attributeType, bool inherit)
    {
      throw new NotSupportedException();
    }

    /// <summary>使用指定的自定义属性 Blob 设置自定义属性。</summary>
    /// <param name="con">自定义属性的构造函数。</param>
    /// <param name="binaryAttribute">表示属性的字节 blob。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="con" /> 为 null。- 或 -<paramref name="binaryAttribute" /> 为 null 引用。</exception>
    public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
    {
      this.m_type.SetGenParamCustomAttribute(con, binaryAttribute);
    }

    /// <summary>使用自定义属性生成器设置自定义属性。</summary>
    /// <param name="customBuilder">定义自定义属性的帮助器类的实例。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="customBuilder" /> 为 null。</exception>
    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
      this.m_type.SetGenParamCustomAttribute(customBuilder);
    }

    /// <summary>设置某类型为了替换为类型参数而必须继承的基类型。</summary>
    /// <param name="baseTypeConstraint">任何将替换为类型参数的类型必须继承的 <see cref="T:System.Type" />。</param>
    public void SetBaseTypeConstraint(Type baseTypeConstraint)
    {
      this.m_type.CheckContext(new Type[1]
      {
        baseTypeConstraint
      });
      this.m_type.SetParent(baseTypeConstraint);
    }

    /// <summary>设置某类型为了替换为类型参数而必须实现的接口。</summary>
    /// <param name="interfaceConstraints">一个 <see cref="T:System.Type" /> 对象的数组，这些对象表示某类型为了替换为类型参数而必须实现的接口。</param>
    [ComVisible(true)]
    public void SetInterfaceConstraints(params Type[] interfaceConstraints)
    {
      this.m_type.CheckContext(interfaceConstraints);
      this.m_type.SetInterfaces(interfaceConstraints);
    }

    /// <summary>设置泛型参数的方差特征和特殊约束（例如，无参数构造函数约束）。</summary>
    /// <param name="genericParameterAttributes">一个表示泛型类型参数的方差特征和特殊约束的 <see cref="T:System.Reflection.GenericParameterAttributes" /> 值的按位组合。</param>
    public void SetGenericParameterAttributes(GenericParameterAttributes genericParameterAttributes)
    {
      this.m_type.SetGenParamAttributes(genericParameterAttributes);
    }
  }
}
