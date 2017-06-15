// Decompiled with JetBrains decompiler
// Type: System.Type
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System
{
  /// <summary>表示类型声明：类类型、接口类型、数组类型、值类型、枚举类型、类型参数、泛型类型定义，以及开放或封闭构造的泛型类型。若要浏览此类型的.NET Framework 源代码，请参阅 Reference Source。</summary>
  /// <filterpriority>1</filterpriority>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_Type))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class Type : MemberInfo, _Type, IReflect
  {
    /// <summary>表示用在特性上的成员筛选器。此字段为只读。</summary>
    /// <filterpriority>1</filterpriority>
    public static readonly MemberFilter FilterAttribute = new MemberFilter(__Filters.Instance.FilterAttribute);
    /// <summary>表示用于名称的区分大小写的成员筛选器。此字段为只读。</summary>
    /// <filterpriority>1</filterpriority>
    public static readonly MemberFilter FilterName = new MemberFilter(__Filters.Instance.FilterName);
    /// <summary>表示用于名称的不区分大小写的成员筛选器。此字段为只读。</summary>
    /// <filterpriority>1</filterpriority>
    public static readonly MemberFilter FilterNameIgnoreCase = new MemberFilter(__Filters.Instance.FilterIgnoreCase);
    /// <summary>表示 <see cref="T:System.Type" /> 信息中的缺少值。此字段为只读。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static readonly object Missing = (object) System.Reflection.Missing.Value;
    /// <summary>分隔 <see cref="T:System.Type" /> 的命名空间中的名称。此字段为只读。</summary>
    /// <filterpriority>1</filterpriority>
    public static readonly char Delimiter = '.';
    /// <summary>表示 <see cref="T:System.Type" /> 类型的空数组。此字段为只读。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static readonly Type[] EmptyTypes = EmptyArray<Type>.Value;
    private static Binder defaultBinder;
    private const BindingFlags DefaultLookup = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
    internal const BindingFlags DeclaredOnlyLookup = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

    /// <summary>获取一个指示此成员是类型还是嵌套类型的 <see cref="T:System.Reflection.MemberTypes" /> 值。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.MemberTypes" /> 值，指示此成员是类型还是嵌套类型。</returns>
    /// <filterpriority>1</filterpriority>
    public override MemberTypes MemberType
    {
      get
      {
        return MemberTypes.TypeInfo;
      }
    }

    /// <summary>获取用来声明当前的嵌套类型或泛型类型参数的类型。</summary>
    /// <returns>如果当前的类型是嵌套类型，则为表示封闭类型的 <see cref="T:System.Type" /> 对象；如果当前的类型是泛型类型的类型参数，则为泛型类型的定义；如果当前的类型是泛型方法的类型参数，则为用来声明泛型方法的类型；否则为 null。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override Type DeclaringType
    {
      [__DynamicallyInvokable] get
      {
        return (Type) null;
      }
    }

    /// <summary>获取一个表示声明方法的 <see cref="T:System.Reflection.MethodBase" />（如果当前 <see cref="T:System.Type" /> 表示泛型方法的一个类型参数）。</summary>
    /// <returns>如果当前 <see cref="T:System.Type" /> 表示泛型方法的一个类型参数，则为一个表示声明方法的 <see cref="T:System.Reflection.MethodBase" />；否则为 null。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual MethodBase DeclaringMethod
    {
      [__DynamicallyInvokable] get
      {
        return (MethodBase) null;
      }
    }

    /// <summary>获取用于获取该成员的类对象。</summary>
    /// <returns>Type 对象，通过它获取了此 <see cref="T:System.Type" /> 对象。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public override Type ReflectedType
    {
      [__DynamicallyInvokable] get
      {
        return (Type) null;
      }
    }

    /// <summary>获取一个描述当前类型的布局的 <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" />。</summary>
    /// <returns>获取一个描述当前类型的大致布局特性的 <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" />。</returns>
    /// <exception cref="T:System.NotSupportedException">在基类中不支持调用的方法。</exception>
    /// <filterpriority>1</filterpriority>
    public virtual StructLayoutAttribute StructLayoutAttribute
    {
      get
      {
        throw new NotSupportedException();
      }
    }

    /// <summary>获取与 <see cref="T:System.Type" /> 关联的 GUID。</summary>
    /// <returns>获取与 <see cref="T:System.Type" /> 关联的 GUID。</returns>
    /// <filterpriority>1</filterpriority>
    public abstract Guid GUID { get; }

    /// <summary>获取默认联编程序的引用，该程序实现的内部规则用于选择由 <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" /> 调用的合适成员。</summary>
    /// <returns>系统使用的默认联编程序的引用。</returns>
    /// <filterpriority>1</filterpriority>
    public static Binder DefaultBinder
    {
      get
      {
        if (Type.defaultBinder == null)
          Type.CreateBinder();
        return Type.defaultBinder;
      }
    }

    /// <summary>获取在其中定义当前 <see cref="T:System.Type" /> 的模块 (DLL)。</summary>
    /// <returns>在其中定义当前 <see cref="T:System.Type" /> 的模块。</returns>
    /// <filterpriority>1</filterpriority>
    public new abstract Module Module { get; }

    /// <summary>获取在其中声明该类型的 <see cref="T:System.Reflection.Assembly" />。对于泛型类型，则获取在其中定义该泛型类型的 <see cref="T:System.Reflection.Assembly" />。</summary>
    /// <returns>描述包含当前类型的程序集的 <see cref="T:System.Reflection.Assembly" /> 实例。对于泛型类型，该实例描述包含泛型类型定义的程序集，而不是创建和使用特定构造类型的程序集。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public abstract Assembly Assembly { [__DynamicallyInvokable] get; }

    /// <summary>获取当前 <see cref="T:System.Type" /> 的句柄。</summary>
    /// <returns>当前 <see cref="T:System.Type" /> 的句柄。</returns>
    /// <exception cref="T:System.NotSupportedException">.NET Compact Framework 当前不支持此属性。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual RuntimeTypeHandle TypeHandle
    {
      [__DynamicallyInvokable] get
      {
        throw new NotSupportedException();
      }
    }

    /// <summary>获取该类型的完全限定名称，包括其命名空间，但不包括程序集。</summary>
    /// <returns>该类型的完全限定名，包括其命名空间，但不包括程序集；如果当前实例表示泛型类型参数、数组类型、指针类型或基于类型参数的 null 类型，或表示不属于泛型类型定义但包含无法解析的类型参数的泛型类型，则为 byref。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public abstract string FullName { [__DynamicallyInvokable] get; }

    /// <summary>获取 <see cref="T:System.Type" /> 的命名空间。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 的命名空间；如果当前实例没有命名空间或表示泛型参数，则为 null。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract string Namespace { [__DynamicallyInvokable] get; }

    /// <summary>获取类型的程序集限定名，其中包括从中加载 <see cref="T:System.Type" /> 的程序集的名称。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 的程序集限定名，其中包括从中加载 <see cref="T:System.Type" /> 的程序集的名称；或者为 null（如果当前实例表示泛型类型参数）。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract string AssemblyQualifiedName { [__DynamicallyInvokable] get; }

    /// <summary>获取当前 <see cref="T:System.Type" /> 直接从中继承的类型。</summary>
    /// <returns>当前 <see cref="T:System.Type" /> 直接从中继承的 <see cref="T:System.Type" />；或者如果当前 null 表示 Type 类或一个接口，则为 <see cref="T:System.Object" />。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract Type BaseType { [__DynamicallyInvokable] get; }

    /// <summary>获取该类型的初始值设定项。</summary>
    /// <returns>包含 <see cref="T:System.Type" /> 的类构造函数的名称的对象。</returns>
    /// <filterpriority>2</filterpriority>
    [ComVisible(true)]
    public ConstructorInfo TypeInitializer
    {
      get
      {
        return this.GetConstructorImpl(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, CallingConventions.Any, Type.EmptyTypes, (ParameterModifier[]) null);
      }
    }

    /// <summary>获取一个指示当前 <see cref="T:System.Type" /> 对象是否表示其定义嵌套在另一个类型的定义之内的类型的值。</summary>
    /// <returns>如果 true 嵌套在另一个类型内，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsNested
    {
      [__DynamicallyInvokable] get
      {
        return this.DeclaringType != (Type) null;
      }
    }

    /// <summary>获取与 <see cref="T:System.Type" /> 关联的属性。</summary>
    /// <returns>表示 <see cref="T:System.Reflection.TypeAttributes" /> 的属性集的 <see cref="T:System.Type" /> 对象，除非 <see cref="T:System.Type" /> 表示泛型类型形参，在此情况下该值未指定。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public TypeAttributes Attributes
    {
      [__DynamicallyInvokable] get
      {
        return this.GetAttributeFlagsImpl();
      }
    }

    /// <summary>获取描述当前泛型类型参数的协变和特殊约束的 <see cref="T:System.Reflection.GenericParameterAttributes" /> 标志。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.GenericParameterAttributes" /> 值的按位组合，用于描述当前泛型类型参数的协变和特殊约束。</returns>
    /// <exception cref="T:System.InvalidOperationException">当前 <see cref="T:System.Type" /> 对象不是泛型类型参数。也就是说， <see cref="P:System.Type.IsGenericParameter" /> 属性将返回 false。</exception>
    /// <exception cref="T:System.NotSupportedException">在基类中不支持调用的方法。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual GenericParameterAttributes GenericParameterAttributes
    {
      [__DynamicallyInvokable] get
      {
        throw new NotSupportedException();
      }
    }

    /// <summary>获取一个指示 <see cref="T:System.Type" /> 是否可由程序集之外的代码访问的值。</summary>
    /// <returns>如果当前 true 是公共类型或公共嵌套类型从而使所有封闭类型都是公共类型，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsVisible
    {
      [__DynamicallyInvokable] get
      {
        RuntimeType type1 = this as RuntimeType;
        if (type1 != (RuntimeType) null)
          return RuntimeTypeHandle.IsVisible(type1);
        if (this.IsGenericParameter)
          return true;
        if (this.HasElementType)
          return this.GetElementType().IsVisible;
        Type type2;
        for (type2 = this; type2.IsNested; type2 = type2.DeclaringType)
        {
          if (!type2.IsNestedPublic)
            return false;
        }
        if (!type2.IsPublic)
          return false;
        if (this.IsGenericType && !this.IsGenericTypeDefinition)
        {
          foreach (Type genericArgument in this.GetGenericArguments())
          {
            if (!genericArgument.IsVisible)
              return false;
          }
        }
        return true;
      }
    }

    /// <summary>获取一个值，该值指示 <see cref="T:System.Type" /> 是否声明为公共类型。</summary>
    /// <returns>如果 true 未声明为公共类型且不是嵌套类型，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsNotPublic
    {
      [__DynamicallyInvokable] get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NotPublic;
      }
    }

    /// <summary>获取一个值，该值指示 <see cref="T:System.Type" /> 是否声明为公共类型。</summary>
    /// <returns>如果 true 声明为公共类型且不是嵌套类型，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsPublic
    {
      [__DynamicallyInvokable] get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.Public;
      }
    }

    /// <summary>获取一个值，通过该值指示类是否是嵌套的并且声明为公共的。</summary>
    /// <returns>如果类是嵌套的并且声明为公共的，则为 true；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsNestedPublic
    {
      [__DynamicallyInvokable] get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPublic;
      }
    }

    /// <summary>获取一个值，通过该值指示 <see cref="T:System.Type" /> 是否是嵌套的并声明为私有。</summary>
    /// <returns>如果 true 是嵌套的并声明为私有，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsNestedPrivate
    {
      [__DynamicallyInvokable] get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPrivate;
      }
    }

    /// <summary>获取一个值，通过该值指示 <see cref="T:System.Type" /> 是否是嵌套的并且只能在它自己的家族内可见。</summary>
    /// <returns>如果 true 是嵌套的并且仅在它自己的家族中可见，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsNestedFamily
    {
      [__DynamicallyInvokable] get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedFamily;
      }
    }

    /// <summary>获取一个值，通过该值指示 <see cref="T:System.Type" /> 是否是嵌套的并且只能在它自己的程序集内可见。</summary>
    /// <returns>如果 true 是嵌套的并且仅在它自己的程序集中可见，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsNestedAssembly
    {
      [__DynamicallyInvokable] get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedAssembly;
      }
    }

    /// <summary>获取一个值，通过该值指示 <see cref="T:System.Type" /> 是否是嵌套的并且只对同时属于自己家族和自己程序集的类可见。</summary>
    /// <returns>如果 true 是嵌套的并且只对同时属于它自己的家族和它自己的程序集的类可见，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsNestedFamANDAssem
    {
      [__DynamicallyInvokable] get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedFamANDAssem;
      }
    }

    /// <summary>获取一个值，通过该值指示 <see cref="T:System.Type" /> 是否是嵌套的并且只对属于它自己的家族或属于它自己的程序集的类可见。</summary>
    /// <returns>如果 true 是嵌套的并且只对属于它自己的家族或属于它自己的程序集的类可见，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsNestedFamORAssem
    {
      [__DynamicallyInvokable] get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.VisibilityMask;
      }
    }

    /// <summary>获取指示当前类型的字段是否由公共语言运行时自动放置的值。</summary>
    /// <returns>如果当前类型的 true 属性包括 <see cref="P:System.Type.Attributes" />，则为 <see cref="F:System.Reflection.TypeAttributes.AutoLayout" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public bool IsAutoLayout
    {
      get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.LayoutMask) == TypeAttributes.NotPublic;
      }
    }

    /// <summary>获取指示当前类型的字段是否按顺序（定义顺序或发送到元数据的顺序）放置的值。</summary>
    /// <returns>如果当前类型的 true 属性包括 <see cref="P:System.Type.Attributes" />，则为 <see cref="F:System.Reflection.TypeAttributes.SequentialLayout" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public bool IsLayoutSequential
    {
      get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.LayoutMask) == TypeAttributes.SequentialLayout;
      }
    }

    /// <summary>获取指示当前类型的字段是否放置在显式指定的偏移量处的值。</summary>
    /// <returns>如果当前类型的 true 属性包括 <see cref="P:System.Type.Attributes" />，则为 <see cref="F:System.Reflection.TypeAttributes.ExplicitLayout" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public bool IsExplicitLayout
    {
      get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.LayoutMask) == TypeAttributes.ExplicitLayout;
      }
    }

    /// <summary>获取一个值，通过该值指示 <see cref="T:System.Type" /> 是否是一个类或委托；即，不是值类型或接口。</summary>
    /// <returns>如果 true 是类，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsClass
    {
      [__DynamicallyInvokable] get
      {
        if ((this.GetAttributeFlagsImpl() & TypeAttributes.ClassSemanticsMask) == TypeAttributes.NotPublic)
          return !this.IsValueType;
        return false;
      }
    }

    /// <summary>获取一个值，通过该值指示 <see cref="T:System.Type" /> 是否是一个接口；即，不是类或值类型。</summary>
    /// <returns>如果 true 是接口，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsInterface
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        RuntimeType type = this as RuntimeType;
        if (type != (RuntimeType) null)
          return RuntimeTypeHandle.IsInterface(type);
        return (this.GetAttributeFlagsImpl() & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask;
      }
    }

    /// <summary>获取一个值，通过该值指示 <see cref="T:System.Type" /> 是否为值类型。</summary>
    /// <returns>如果 true 是值类型，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsValueType
    {
      [__DynamicallyInvokable] get
      {
        return this.IsValueTypeImpl();
      }
    }

    /// <summary>获取一个值，通过该值指示 <see cref="T:System.Type" /> 是否为抽象的并且必须被重写。</summary>
    /// <returns>如果 true 是抽象的，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsAbstract
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.GetAttributeFlagsImpl() & TypeAttributes.Abstract) > 0U;
      }
    }

    /// <summary>获取一个值，该值指示 <see cref="T:System.Type" /> 是否声明为密封的。</summary>
    /// <returns>如果 true 被声明为密封的，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsSealed
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.GetAttributeFlagsImpl() & TypeAttributes.Sealed) > 0U;
      }
    }

    /// <summary>获取一个值，该值指示当前的 <see cref="T:System.Type" /> 是否表示枚举。</summary>
    /// <returns>如果当前 true 表示枚举，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual bool IsEnum
    {
      [__DynamicallyInvokable] get
      {
        return this.IsSubclassOf((Type) RuntimeType.EnumType);
      }
    }

    /// <summary>获取一个值，该值指示该类型是否具有需要特殊处理的名称。</summary>
    /// <returns>如果该类型具有需要特殊处理的名称，则为 true；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsSpecialName
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.GetAttributeFlagsImpl() & TypeAttributes.SpecialName) > 0U;
      }
    }

    /// <summary>获取一个值，该值指示 <see cref="T:System.Type" /> 是否应用了 <see cref="T:System.Runtime.InteropServices.ComImportAttribute" /> 属性，如果应用了该属性，则表示它是从 COM 类型库导入的。</summary>
    /// <returns>如果 true 具有 <see cref="T:System.Type" />，则为 <see cref="T:System.Runtime.InteropServices.ComImportAttribute" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public bool IsImport
    {
      get
      {
        return (uint) (this.GetAttributeFlagsImpl() & TypeAttributes.Import) > 0U;
      }
    }

    /// <summary>获取一个值，通过该值指示 <see cref="T:System.Type" /> 是否为可序列化的。</summary>
    /// <returns>如果 true 是可序列化的，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public virtual bool IsSerializable
    {
      [__DynamicallyInvokable] get
      {
        if ((this.GetAttributeFlagsImpl() & TypeAttributes.Serializable) != TypeAttributes.NotPublic)
          return true;
        RuntimeType runtimeType = this.UnderlyingSystemType as RuntimeType;
        if (runtimeType != (RuntimeType) null)
          return runtimeType.IsSpecialSerializableType();
        return false;
      }
    }

    /// <summary>获取一个值，该值指示是否为 AnsiClass 选择了字符串格式属性 <see cref="T:System.Type" />。</summary>
    /// <returns>如果为 true 选择了字符串格式属性 AnsiClass，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public bool IsAnsiClass
    {
      get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.StringFormatMask) == TypeAttributes.NotPublic;
      }
    }

    /// <summary>获取一个值，该值指示是否为 UnicodeClass 选择了字符串格式属性 <see cref="T:System.Type" />。</summary>
    /// <returns>如果为 true 选择了字符串格式属性 UnicodeClass，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public bool IsUnicodeClass
    {
      get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.StringFormatMask) == TypeAttributes.UnicodeClass;
      }
    }

    /// <summary>获取一个值，该值指示是否为 AutoClass 选择了字符串格式属性 <see cref="T:System.Type" />。</summary>
    /// <returns>如果为 true 选择了字符串格式属性 AutoClass，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public bool IsAutoClass
    {
      get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.StringFormatMask) == TypeAttributes.AutoClass;
      }
    }

    /// <summary>获取一个值，该值指示类型是否为数组。</summary>
    /// <returns>如果当前类型是数组，则为 true；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsArray
    {
      [__DynamicallyInvokable] get
      {
        return this.IsArrayImpl();
      }
    }

    internal virtual bool IsSzArray
    {
      get
      {
        return false;
      }
    }

    /// <summary>获取一个值，该值指示当前类型是否是泛型类型。</summary>
    /// <returns>如果当前类型是泛型类型，则为 true；否则为  false。</returns>
    [__DynamicallyInvokable]
    public virtual bool IsGenericType
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    /// <summary>获取一个值，该值指示当前 <see cref="T:System.Type" /> 是否表示可以用来构造其他泛型类型的泛型类型定义。</summary>
    /// <returns>如果此 true 对象表示泛型类型定义，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual bool IsGenericTypeDefinition
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    /// <summary>获取指示此对象是否表示构造的泛型类型的值。你可以创建构造型泛型类型的实例。</summary>
    /// <returns>如果此对象表示构造泛型类型，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public virtual bool IsConstructedGenericType
    {
      [__DynamicallyInvokable] get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>获取一个值，该值指示当前 <see cref="T:System.Type" /> 是否表示泛型类型或方法的定义中的类型参数。</summary>
    /// <returns>如果 true 对象表示泛型类型定义或泛型方法定义的类型参数，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual bool IsGenericParameter
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    /// <summary>当 <see cref="T:System.Type" /> 对象表示泛型类型或泛型方法的类型参数时，获取类型参数在声明它的泛型类型或方法的类型参数列表中的位置。</summary>
    /// <returns>类型参数在定义它的泛型类型或方法的类型参数列表中的位置。位置编号从 0 开始。</returns>
    /// <exception cref="T:System.InvalidOperationException">当前的类型不表示类型参数。也就是说， <see cref="P:System.Type.IsGenericParameter" /> 返回 false。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual int GenericParameterPosition
    {
      [__DynamicallyInvokable] get
      {
        throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericParameter"));
      }
    }

    /// <summary>获取一个值，该值指示当前 <see cref="T:System.Type" /> 对象是否具有尚未被特定类型替代的类型参数。</summary>
    /// <returns>如果 true 对象本身是泛型类型形参或者具有尚未提供特定类型的类型形参，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual bool ContainsGenericParameters
    {
      [__DynamicallyInvokable] get
      {
        if (this.HasElementType)
          return this.GetRootElementType().ContainsGenericParameters;
        if (this.IsGenericParameter)
          return true;
        if (!this.IsGenericType)
          return false;
        foreach (Type genericArgument in this.GetGenericArguments())
        {
          if (genericArgument.ContainsGenericParameters)
            return true;
        }
        return false;
      }
    }

    /// <summary>获取一个值，该值指示 <see cref="T:System.Type" /> 是否由引用传递。</summary>
    /// <returns>如果 true 按引用传递，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsByRef
    {
      [__DynamicallyInvokable] get
      {
        return this.IsByRefImpl();
      }
    }

    /// <summary>获取一个值，通过该值指示 <see cref="T:System.Type" /> 是否为指针。</summary>
    /// <returns>如果 true 是指针，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsPointer
    {
      [__DynamicallyInvokable] get
      {
        return this.IsPointerImpl();
      }
    }

    /// <summary>获取一个值，通过该值指示 <see cref="T:System.Type" /> 是否为基元类型之一。</summary>
    /// <returns>如果 true 为基元类型之一，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool IsPrimitive
    {
      [__DynamicallyInvokable] get
      {
        return this.IsPrimitiveImpl();
      }
    }

    /// <summary>获取一个值，通过该值指示 <see cref="T:System.Type" /> 是否为 COM 对象。</summary>
    /// <returns>如果 true 为 COM 对象，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public bool IsCOMObject
    {
      get
      {
        return this.IsCOMObjectImpl();
      }
    }

    internal bool IsWindowsRuntimeObject
    {
      get
      {
        return this.IsWindowsRuntimeObjectImpl();
      }
    }

    internal bool IsExportedToWindowsRuntime
    {
      get
      {
        return this.IsExportedToWindowsRuntimeImpl();
      }
    }

    /// <summary>获取一个值，通过该值指示当前 <see cref="T:System.Type" /> 是包含还是引用另一类型，即当前 <see cref="T:System.Type" /> 是数组、指针还是通过引用传递。</summary>
    /// <returns>如果 true 为数组、指针或按引用传递，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public bool HasElementType
    {
      [__DynamicallyInvokable] get
      {
        return this.HasElementTypeImpl();
      }
    }

    /// <summary>获取一个值，通过该值指示 <see cref="T:System.Type" /> 在上下文中是否可以被承载。</summary>
    /// <returns>如果 true 能够在某个上下文中承载，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public bool IsContextful
    {
      get
      {
        return this.IsContextfulImpl();
      }
    }

    /// <summary>获取一个值，该值指示 <see cref="T:System.Type" /> 是否按引用进行封送。</summary>
    /// <returns>如果 true 是由引用封送的，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public bool IsMarshalByRef
    {
      get
      {
        return this.IsMarshalByRefImpl();
      }
    }

    internal bool HasProxyAttribute
    {
      get
      {
        return this.HasProxyAttributeImpl();
      }
    }

    /// <summary>获取此类型泛型类型参数的数组。</summary>
    /// <returns>此类型的泛型类型参数的数组。</returns>
    [__DynamicallyInvokable]
    public virtual Type[] GenericTypeArguments
    {
      [__DynamicallyInvokable] get
      {
        if (this.IsGenericType && !this.IsGenericTypeDefinition)
          return this.GetGenericArguments();
        return Type.EmptyTypes;
      }
    }

    /// <summary>获取一个值，该值指示当前的类型在当前信任级别上是安全关键的还是安全可靠关键的，并因此可以执行关键操作。</summary>
    /// <returns>如果当前类型在当前信任级别上是安全关键的或安全可靠关键的，则为 true；如果它是透明的，则为 false。</returns>
    public virtual bool IsSecurityCritical
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>获取一个值，该值指示当前类型在当前信任级别上是否是安全可靠关键的；即它是否可以执行关键操作并可以由透明代码访问。</summary>
    /// <returns>如果当前类型在当前信任级别上是安全可靠关键的，则为 true；如果它是安全关键的或透明的，则为 false。</returns>
    public virtual bool IsSecuritySafeCritical
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>获取一个值，该值指示当前类型在当前信任级别上是否是透明的而无法执行关键操作。</summary>
    /// <returns>如果该类型在当前信任级别上是安全透明的，则为 true；否则为 false。</returns>
    public virtual bool IsSecurityTransparent
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    internal bool NeedsReflectionSecurityCheck
    {
      get
      {
        if (!this.IsVisible || this.IsSecurityCritical && !this.IsSecuritySafeCritical)
          return true;
        if (this.IsGenericType)
        {
          foreach (Type genericArgument in this.GetGenericArguments())
          {
            if (genericArgument.NeedsReflectionSecurityCheck)
              return true;
          }
        }
        else if (this.IsArray || this.IsPointer)
          return this.GetElementType().NeedsReflectionSecurityCheck;
        return false;
      }
    }

    /// <summary>指示表示该类型的公共语言运行时提供的类型。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 的基础系统类型。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract Type UnderlyingSystemType { [__DynamicallyInvokable] get; }

    /// <summary>指示两个 <see cref="T:System.Type" /> 对象是否相等。</summary>
    /// <returns>如果 true 等于 <paramref name="left" />，则为 <paramref name="right" />；否则为 false。</returns>
    /// <param name="left">要比较的第一个对象。</param>
    /// <param name="right">要比较的第二个对象。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern bool operator ==(Type left, Type right);

    /// <summary>指示两个 <see cref="T:System.Type" /> 对象是否不相等。</summary>
    /// <returns>如果 true 不等于 <paramref name="left" />，则为 <paramref name="right" />；否则为 false。</returns>
    /// <param name="left">要比较的第一个对象。</param>
    /// <param name="right">要比较的第二个对象。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern bool operator !=(Type left, Type right);

    /// <summary>获取具有指定名称的 <see cref="T:System.Type" />，指定是否执行区分大小写的搜索，以及在找不到类型时是否引发异常。</summary>
    /// <returns>具有指定名称的类型。如果找不到该类型，则 <paramref name="throwOnError" /> 参数指定是返回 null 还是引发异常。在某些情况下，将引发异常，而不考虑 <paramref name="throwOnError" /> 的值。请参见“异常”部分。</returns>
    /// <param name="typeName">要获取的类型的程序集限定名称。请参阅<see cref="P:System.Type.AssemblyQualifiedName" />。如果该类型位于当前正在执行的程序集中或者 Mscorlib.dll 中，则提供由命名空间限定的类型名称就足够了。</param>
    /// <param name="throwOnError">true 则引发异常（如果找不到类型）；false 则返回 null.Specifying false，也抑制了其他一些异常情况，但不是所有异常。请参见“异常”部分。</param>
    /// <param name="ignoreCase">对 true 执行的搜索不区分大小写，则为 <paramref name="typeName" />；对 false 执行的搜索区分大小写，则为 <paramref name="typeName" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">类初始值设定项将调用，并且会引发异常。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="throwOnError" /> 是 true 但找不到该类型。- 或 -<paramref name="throwOnError" /> 是 true 和 <paramref name="typeName" /> 包含无效字符如嵌入的选项卡。- 或 -<paramref name="throwOnError" /> 是 true 和 <paramref name="typeName" /> 为空字符串。- 或 -<paramref name="throwOnError" /> 是 true 和 <paramref name="typeName" /> 表示具有无效大小的数组类型。- 或 -<paramref name="typeName" /> 表示一个数组的 <see cref="T:System.TypedReference" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="throwOnError" /> 是 true 和 <paramref name="typeName" /> 包含无效的语法。例如，"MyType [，*，]"。- 或 - <paramref name="typeName" /> 表示具有指针类型，泛型类型 ByRef 类型，或 <see cref="T:System.Void" /> 作为其类型参数之一。- 或 -<paramref name="typeName" /> 表示具有数目不正确的类型参数的泛型类型。- 或 -<paramref name="typeName" /> 表示泛型类型，并且其类型参数之一不满足相应的约束类型参数。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="throwOnError" /> 是 true 并且未找到该程序集或其依赖项之一。</exception>
    /// <exception cref="T:System.IO.FileLoadException">该程序集或其依赖项之一找到，但无法加载。</exception>
    /// <exception cref="T:System.BadImageFormatException">该程序集或其依赖项之一无效。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时，并具有更高版本编译的程序集。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Type GetType(string typeName, bool throwOnError, bool ignoreCase)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Type) RuntimeType.GetType(typeName, throwOnError, ignoreCase, false, ref stackMark);
    }

    /// <summary>获取具有指定名称的 <see cref="T:System.Type" />，指定是否执行区分大小写的搜索，以及在找不到类型时是否引发异常。</summary>
    /// <returns>具有指定名称的类型。如果找不到该类型，则 <paramref name="throwOnError" /> 参数指定是返回 null 还是引发异常。在某些情况下，将引发异常，而不考虑 <paramref name="throwOnError" /> 的值。请参见“异常”部分。</returns>
    /// <param name="typeName">要获取的类型的程序集限定名称。请参阅<see cref="P:System.Type.AssemblyQualifiedName" />。如果该类型位于当前正在执行的程序集中或者 Mscorlib.dll 中，则提供由命名空间限定的类型名称就足够了。</param>
    /// <param name="throwOnError">如果为 true，则在找不到该类型时引发异常；如果为 false，则返回 null。指定 false 还会取消某些其他异常条件，但并不取消所有条件。请参见“异常”部分。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">类初始值设定项将调用，并且会引发异常。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="throwOnError" /> 是 true 但找不到该类型。- 或 -<paramref name="throwOnError" /> 是 true 和 <paramref name="typeName" /> 包含无效字符如嵌入的选项卡。- 或 -<paramref name="throwOnError" /> 是 true 和 <paramref name="typeName" /> 为空字符串。- 或 -<paramref name="throwOnError" /> 是 true 和 <paramref name="typeName" /> 表示具有无效大小的数组类型。- 或 -<paramref name="typeName" /> 表示一个数组的 <see cref="T:System.TypedReference" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="throwOnError" /> 是 true 和 <paramref name="typeName" /> 包含无效的语法。例如，"MyType [，*，]"。- 或 - <paramref name="typeName" /> 表示具有指针类型，泛型类型 ByRef 类型，或 <see cref="T:System.Void" /> 作为其类型参数之一。- 或 -<paramref name="typeName" /> 表示具有数目不正确的类型参数的泛型类型。- 或 -<paramref name="typeName" /> 表示泛型类型，并且其类型参数之一不满足相应的约束类型参数。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="throwOnError" /> 是 true 并且未找到该程序集或其依赖项之一。</exception>
    /// <exception cref="T:System.IO.FileLoadException">在 .NET for Windows Store apps 或 可移植类库, ，捕获该基类异常， <see cref="T:System.IO.IOException" />, 、 相反。该程序集或其依赖项之一找到，但无法加载。</exception>
    /// <exception cref="T:System.BadImageFormatException">该程序集或其依赖项之一无效。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时，并具有更高版本编译的程序集。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Type GetType(string typeName, bool throwOnError)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Type) RuntimeType.GetType(typeName, throwOnError, false, false, ref stackMark);
    }

    /// <summary>获取具有指定名称的 <see cref="T:System.Type" />，执行区分大小写的搜索。</summary>
    /// <returns>具有指定名称的类型（如果找到的话）；否则为 null。</returns>
    /// <param name="typeName">要获取的类型的程序集限定名称。请参阅<see cref="P:System.Type.AssemblyQualifiedName" />。如果该类型位于当前正在执行的程序集中或者 Mscorlib.dll 中，则提供由命名空间限定的类型名称就足够了。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">类初始值设定项将调用，并且会引发异常。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="typeName" /> 表示具有指针类型，泛型类型 ByRef 类型，或 <see cref="T:System.Void" /> 作为其类型参数之一。- 或 -<paramref name="typeName" /> 表示具有数目不正确的类型参数的泛型类型。- 或 -<paramref name="typeName" /> 表示泛型类型，并且其类型参数之一不满足相应的约束类型参数。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typeName" /> 表示一个数组的 <see cref="T:System.TypedReference" />。</exception>
    /// <exception cref="T:System.IO.FileLoadException">在 .NET for Windows Store apps 或 可移植类库, ，捕获该基类异常， <see cref="T:System.IO.IOException" />, 、 相反。该程序集或其依赖项之一找到，但无法加载。</exception>
    /// <exception cref="T:System.BadImageFormatException">该程序集或其依赖项之一无效。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时，并具有更高版本编译的程序集。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Type GetType(string typeName)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Type) RuntimeType.GetType(typeName, false, false, false, ref stackMark);
    }

    /// <summary>获取具有指定名称的类型，（可选）提供自定义方法以解析程序集和该类型。</summary>
    /// <returns>具有指定名称的类型，如果未找到该类型，则返回 null。</returns>
    /// <param name="typeName">要获取的类型的名称。如果提供了 <paramref name="typeResolver" /> 参数，则类型名称可以为 <paramref name="typeResolver" /> 能够解析的任何字符串。如果提供了 <paramref name="assemblyResolver" /> 参数，或者使用了标准类型解析，则除非该类型位于当前正在执行的程序集或 Mscorlib.dll 中（在这种情况下足以提供其命名空间所限定的类型名称），否则 <paramref name="typeName" /> 必须为程序集限定的名称（请参见 <see cref="P:System.Type.AssemblyQualifiedName" />）。</param>
    /// <param name="assemblyResolver">一个方法，它定位并返回 <paramref name="typeName" /> 中指定的程序集。以 <paramref name="assemblyResolver" /> 对象形式传递给 <see cref="T:System.Reflection.AssemblyName" /> 的程序集名称。如果 <paramref name="typeName" /> 不包含程序集的名称，则不调用 <paramref name="assemblyResolver" />。如果未提供 <paramref name="assemblyResolver" />，则执行标准程序集解析。警告   不要通过未知的或不受信任的调用方传递方法。此操作可能会导致恶意代码特权提升。仅使用你提供或者熟悉的方法。</param>
    /// <param name="typeResolver">一个方法，它在由 <paramref name="typeName" /> 或标准程序集解析返回的程序集中定位并返回 <paramref name="assemblyResolver" /> 所指定的类型。如果未提供任何程序集，则 <paramref name="typeResolver" /> 方法可以提供一个程序集。该方法还采用一个参数以指定是否执行不区分大小写的搜索；false 传递给该参数。警告   不要通过未知的或不受信任的调用方传递方法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">类初始值设定项将调用，并且会引发异常。</exception>
    /// <exception cref="T:System.ArgumentException">发生错误时 <paramref name="typeName" /> 分析为类型名称和程序集名称 （例如，当简单类型名称包括转义的特殊字符时）。- 或 -<paramref name="typeName" /> 表示具有指针类型，泛型类型 ByRef 类型，或 <see cref="T:System.Void" /> 作为其类型参数之一。- 或 -<paramref name="typeName" /> 表示具有数目不正确的类型参数的泛型类型。- 或 -<paramref name="typeName" /> 表示泛型类型，并且其类型参数之一不满足相应的约束类型参数。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typeName" /> 表示一个数组的 <see cref="T:System.TypedReference" />。</exception>
    /// <exception cref="T:System.IO.FileLoadException">该程序集或其依赖项之一找到，但无法加载。- 或 -<paramref name="typeName" /> 包含无效的程序集名称。- 或 -<paramref name="typeName" /> 是一个有效的程序集名称不包含类型名称。</exception>
    /// <exception cref="T:System.BadImageFormatException">该程序集或其依赖项之一无效。- 或 -该程序集是具有比当前加载的版本更高版本的公共语言运行时编译的。</exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Type GetType(string typeName, Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TypeNameParser.GetType(typeName, assemblyResolver, typeResolver, false, false, ref stackMark);
    }

    /// <summary>获取具有指定名称的类型，指定在找不到该类型时是否引发异常，（可选）提供自定义方法以解析程序集和该类型。</summary>
    /// <returns>具有指定名称的类型。如果找不到该类型，则 <paramref name="throwOnError" /> 参数指定是返回 null 还是引发异常。在某些情况下，将引发异常，而不考虑 <paramref name="throwOnError" /> 的值。请参见“异常”部分。</returns>
    /// <param name="typeName">要获取的类型的名称。如果提供了 <paramref name="typeResolver" /> 参数，则类型名称可以为 <paramref name="typeResolver" /> 能够解析的任何字符串。如果提供了 <paramref name="assemblyResolver" /> 参数，或者使用了标准类型解析，则除非该类型位于当前正在执行的程序集或 Mscorlib.dll 中（在这种情况下足以提供其命名空间所限定的类型名称），否则 <paramref name="typeName" /> 必须为程序集限定的名称（请参见 <see cref="P:System.Type.AssemblyQualifiedName" />）。</param>
    /// <param name="assemblyResolver">一个方法，它定位并返回 <paramref name="typeName" /> 中指定的程序集。以 <paramref name="assemblyResolver" /> 对象形式传递给 <see cref="T:System.Reflection.AssemblyName" /> 的程序集名称。如果 <paramref name="typeName" /> 不包含程序集的名称，则不调用 <paramref name="assemblyResolver" />。如果未提供 <paramref name="assemblyResolver" />，则执行标准程序集解析。警告   不要通过未知的或不受信任的调用方传递方法。此操作可能会导致恶意代码特权提升。仅使用你提供或者熟悉的方法。</param>
    /// <param name="typeResolver">一个方法，它在由 <paramref name="typeName" /> 或标准程序集解析返回的程序集中定位并返回 <paramref name="assemblyResolver" /> 所指定的类型。如果未提供任何程序集，则该方法可以提供一个程序集。该方法还采用一个参数以指定是否执行不区分大小写的搜索；false 传递给该参数。警告   不要通过未知的或不受信任的调用方传递方法。</param>
    /// <param name="throwOnError">如果为 true，则在找不到该类型时引发异常；如果为 false，则返回 null。指定 false 还会取消某些其他异常条件，但并不取消所有条件。请参见“异常”部分。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">类初始值设定项将调用，并且会引发异常。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="throwOnError" /> 是 true 但找不到该类型。- 或 -<paramref name="throwOnError" /> 是 true 和 <paramref name="typeName" /> 包含无效字符如嵌入的选项卡。- 或 -<paramref name="throwOnError" /> 是 true 和 <paramref name="typeName" /> 为空字符串。- 或 -<paramref name="throwOnError" /> 是 true 和 <paramref name="typeName" /> 表示具有无效大小的数组类型。- 或 -<paramref name="typeName" /> 表示一个数组的 <see cref="T:System.TypedReference" />。</exception>
    /// <exception cref="T:System.ArgumentException">发生错误时 <paramref name="typeName" /> 分析为类型名称和程序集名称 （例如，当简单类型名称包括转义的特殊字符时）。- 或 -<paramref name="throwOnError" /> 是 true 和 <paramref name="typeName" /> 包含无效的语法 （例如，"MyType[,*,]")。- 或 - <paramref name="typeName" /> 表示具有指针类型，泛型类型 ByRef 类型，或 <see cref="T:System.Void" /> 作为其类型参数之一。- 或 -<paramref name="typeName" /> 表示具有数目不正确的类型参数的泛型类型。- 或 -<paramref name="typeName" /> 表示泛型类型，并且其类型参数之一不满足相应的约束类型参数。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="throwOnError" /> 是 true 并且未找到该程序集或其依赖项之一。- 或 -<paramref name="typeName" /> 包含无效的程序集名称。- 或 -<paramref name="typeName" /> 是一个有效的程序集名称不包含类型名称。</exception>
    /// <exception cref="T:System.IO.FileLoadException">该程序集或其依赖项之一找到，但无法加载。</exception>
    /// <exception cref="T:System.BadImageFormatException">该程序集或其依赖项之一无效。- 或 -该程序集是具有比当前加载的版本更高版本的公共语言运行时编译的。</exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Type GetType(string typeName, Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver, bool throwOnError)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TypeNameParser.GetType(typeName, assemblyResolver, typeResolver, throwOnError, false, ref stackMark);
    }

    /// <summary>获取具有指定名称的类型，指定是否执行区分大小写的搜索，在找不到类型时是否引发异常，（可选）提供自定义方法以解析程序集和该类型。</summary>
    /// <returns>具有指定名称的类型。如果找不到该类型，则 <paramref name="throwOnError" /> 参数指定是返回 null 还是引发异常。在某些情况下，将引发异常，而不考虑 <paramref name="throwOnError" /> 的值。请参见“异常”部分。</returns>
    /// <param name="typeName">要获取的类型的名称。如果提供了 <paramref name="typeResolver" /> 参数，则类型名称可以为 <paramref name="typeResolver" /> 能够解析的任何字符串。如果提供了 <paramref name="assemblyResolver" /> 参数，或者使用了标准类型解析，则除非该类型位于当前正在执行的程序集或 Mscorlib.dll 中（在这种情况下足以提供其命名空间所限定的类型名称），否则 <paramref name="typeName" /> 必须为程序集限定的名称（请参见 <see cref="P:System.Type.AssemblyQualifiedName" />）。</param>
    /// <param name="assemblyResolver">一个方法，它定位并返回 <paramref name="typeName" /> 中指定的程序集。以 <paramref name="assemblyResolver" /> 对象形式传递给 <see cref="T:System.Reflection.AssemblyName" /> 的程序集名称。如果 <paramref name="typeName" /> 不包含程序集的名称，则不调用 <paramref name="assemblyResolver" />。如果未提供 <paramref name="assemblyResolver" />，则执行标准程序集解析。警告   不要通过未知的或不受信任的调用方传递方法。此操作可能会导致恶意代码特权提升。仅使用你提供或者熟悉的方法。</param>
    /// <param name="typeResolver">一个方法，它在由 <paramref name="typeName" /> 或标准程序集解析返回的程序集中定位并返回 <paramref name="assemblyResolver" /> 所指定的类型。如果未提供任何程序集，则该方法可以提供一个程序集。该方法还采用一个参数以指定是否执行不区分大小写的搜索；<paramref name="ignoreCase" /> 的值传递给该参数。警告   不要通过未知的或不受信任的调用方传递方法。</param>
    /// <param name="throwOnError">如果为 true，则在找不到该类型时引发异常；如果为 false，则返回 null。指定 false 还会取消某些其他异常条件，但并不取消所有条件。请参见“异常”部分。</param>
    /// <param name="ignoreCase">对 true 执行的搜索不区分大小写，则为 <paramref name="typeName" />；对 false 执行的搜索区分大小写，则为 <paramref name="typeName" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">类初始值设定项将调用，并且会引发异常。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="throwOnError" /> 是 true 但找不到该类型。- 或 -<paramref name="throwOnError" /> 是 true 和 <paramref name="typeName" /> 包含无效字符如嵌入的选项卡。- 或 -<paramref name="throwOnError" /> 是 true 和 <paramref name="typeName" /> 为空字符串。- 或 -<paramref name="throwOnError" /> 是 true 和 <paramref name="typeName" /> 表示具有无效大小的数组类型。- 或 -<paramref name="typeName" /> 表示一个数组的 <see cref="T:System.TypedReference" />。</exception>
    /// <exception cref="T:System.ArgumentException">发生错误时 <paramref name="typeName" /> 分析为类型名称和程序集名称 （例如，当简单类型名称包括转义的特殊字符时）。- 或 -<paramref name="throwOnError" /> 是 true 和 <paramref name="typeName" /> 包含无效的语法 （例如，"MyType[,*,]")。- 或 - <paramref name="typeName" /> 表示具有指针类型，泛型类型 ByRef 类型，或 <see cref="T:System.Void" /> 作为其类型参数之一。- 或 -<paramref name="typeName" /> 表示具有数目不正确的类型参数的泛型类型。- 或 -<paramref name="typeName" /> 表示泛型类型，并且其类型参数之一不满足相应的约束类型参数。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="throwOnError" /> 是 true 并且未找到该程序集或其依赖项之一。</exception>
    /// <exception cref="T:System.IO.FileLoadException">该程序集或其依赖项之一找到，但无法加载。- 或 -<paramref name="typeName" /> 包含无效的程序集名称。- 或 -<paramref name="typeName" /> 是一个有效的程序集名称不包含类型名称。</exception>
    /// <exception cref="T:System.BadImageFormatException">该程序集或其依赖项之一无效。- 或 -该程序集是具有比当前加载的版本更高版本的公共语言运行时编译的。</exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Type GetType(string typeName, Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver, bool throwOnError, bool ignoreCase)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TypeNameParser.GetType(typeName, assemblyResolver, typeResolver, throwOnError, ignoreCase, ref stackMark);
    }

    /// <summary>获取具有指定名称的 <see cref="T:System.Type" />，指定是否执行区分大小写的搜索，以及在找不到类型时是否引发异常。该类型只为反射加载，而不为执行加载。</summary>
    /// <returns>具有指定名称的类型（如果找到的话）；否则为 null。如果找不到该类型，则 <paramref name="throwIfNotFound" /> 参数指定是返回 null 还是引发异常。在某些情况下，将引发异常，而不考虑 <paramref name="throwIfNotFound" /> 的值。请参见“异常”部分。</returns>
    /// <param name="typeName">要获取的 <see cref="T:System.Type" /> 的程序集限定名称。</param>
    /// <param name="throwIfNotFound">如果为 true，则会在找不到该类型时引发 <see cref="T:System.TypeLoadException" />；如果为 false，则在找不到该类型时返回 null。指定 false 还会取消某些其他异常条件，但并不取消所有条件。请参见“异常”部分。</param>
    /// <param name="ignoreCase">如果为 true，则执行不区分大小写的 <paramref name="typeName" /> 搜索；如果为 false，则执行区分大小写的 <paramref name="typeName" /> 搜索。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">类初始值设定项将调用，并且会引发异常。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="throwIfNotFound" /> 是 true 但找不到该类型。- 或 -<paramref name="throwIfNotFound" /> 是 true 和 <paramref name="typeName" /> 包含无效字符如嵌入的选项卡。- 或 -<paramref name="throwIfNotFound" /> 是 true 和 <paramref name="typeName" /> 为空字符串。- 或 -<paramref name="throwIfNotFound" /> 是 true 和 <paramref name="typeName" /> 表示具有无效大小的数组类型。- 或 -<paramref name="typeName" /> 表示一个数组的 <see cref="T:System.TypedReference" /> 对象。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="typeName" /> 不包括程序集名称。- 或 -<paramref name="throwIfNotFound" /> 是 true 和 <paramref name="typeName" /> 包含无效的语法 ； 例如，"MyType [，*，]"。- 或 -<paramref name="typeName" /> 表示具有指针类型，泛型类型 ByRef 类型，或 <see cref="T:System.Void" /> 作为其类型参数之一。- 或 -<paramref name="typeName" /> 表示具有数目不正确的类型参数的泛型类型。- 或 -<paramref name="typeName" /> 表示泛型类型，并且其类型参数之一不满足相应的约束类型参数。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="throwIfNotFound" /> 是 true 并且未找到该程序集或其依赖项之一。</exception>
    /// <exception cref="T:System.IO.FileLoadException">该程序集或其依赖项之一找到，但无法加载。</exception>
    /// <exception cref="T:System.BadImageFormatException">该程序集或其依赖项之一无效。- 或 -该程序集是具有比当前加载的版本更高版本的公共语言运行时编译的。</exception>
    /// <filterpriority>1</filterpriority>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Type ReflectionOnlyGetType(string typeName, bool throwIfNotFound, bool ignoreCase)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Type) RuntimeType.GetType(typeName, throwIfNotFound, ignoreCase, true, ref stackMark);
    }

    /// <summary>返回表示指向当前类型的指针的 <see cref="T:System.Type" /> 对象。</summary>
    /// <returns>表示指向当前类型的指针的 <see cref="T:System.Type" /> 对象。</returns>
    /// <exception cref="T:System.NotSupportedException">在基类中不支持调用的方法。</exception>
    /// <exception cref="T:System.TypeLoadException">当前的类型是 <see cref="T:System.TypedReference" />。- 或 -当前的类型是 ByRef 类型。也就是说， <see cref="P:System.Type.IsByRef" /> 返回 true。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual Type MakePointerType()
    {
      throw new NotSupportedException();
    }

    /// <summary>返回表示作为 <see cref="T:System.Type" /> 参数（在 Visual Basic 中为 ref 参数）传递时的当前类型的 ByRef 对象。</summary>
    /// <returns>表示作为 <see cref="T:System.Type" /> 参数（在 Visual Basic 中为 ref 参数）传递时的当前类型的 ByRef 对象。</returns>
    /// <exception cref="T:System.NotSupportedException">在基类中不支持调用的方法。</exception>
    /// <exception cref="T:System.TypeLoadException">当前的类型是 <see cref="T:System.TypedReference" />。- 或 -当前的类型是 ByRef 类型。也就是说， <see cref="P:System.Type.IsByRef" /> 返回 true。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual Type MakeByRefType()
    {
      throw new NotSupportedException();
    }

    /// <summary>返回 <see cref="T:System.Type" /> 对象，该对象表示当前类型的一维数组（下限为零）。</summary>
    /// <returns>返回一个表示当前类型的一维数组（下限为零）的 <see cref="T:System.Type" /> 对象。</returns>
    /// <exception cref="T:System.NotSupportedException">在基类中不支持调用的方法。派生类必须提供一个实现。</exception>
    /// <exception cref="T:System.TypeLoadException">当前的类型是 <see cref="T:System.TypedReference" />。- 或 -当前的类型是 ByRef 类型。也就是说， <see cref="P:System.Type.IsByRef" /> 返回 true。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual Type MakeArrayType()
    {
      throw new NotSupportedException();
    }

    /// <summary>返回 <see cref="T:System.Type" /> 对象，该对象表示一个具有指定维数的当前类型的数组。</summary>
    /// <returns>表示当前类型的指定维数的数组的对象。</returns>
    /// <param name="rank">数组的维数。此数字必须小于或等于 32。</param>
    /// <exception cref="T:System.IndexOutOfRangeException">
    /// <paramref name="rank" /> 无效。例如，0 或负值。</exception>
    /// <exception cref="T:System.NotSupportedException">在基类中不支持调用的方法。</exception>
    /// <exception cref="T:System.TypeLoadException">当前的类型是 <see cref="T:System.TypedReference" />。- 或 -当前的类型是 ByRef 类型。也就是说， <see cref="P:System.Type.IsByRef" /> 返回 true。- 或 -<paramref name="rank" /> 大于 32。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual Type MakeArrayType(int rank)
    {
      throw new NotSupportedException();
    }

    /// <summary>获取与指定程序标识符 (ProgID) 关联的类型，如果在加载 <see cref="T:System.Type" /> 时遇到错误，则返回空值。</summary>
    /// <returns>如果 <paramref name="progID" /> 是注册表中的有效项，并且有与之关联的类型，则为与指定 ProgID 关联的类型；否则为 null。</returns>
    /// <param name="progID">要获取的类型的 ProgID。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="progID" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static Type GetTypeFromProgID(string progID)
    {
      return RuntimeType.GetTypeFromProgIDImpl(progID, (string) null, false);
    }

    /// <summary>获取与指定程序标识符 (ProgID) 关联的类型，指定如果在加载该类型时发生错误是否引发异常。</summary>
    /// <returns>如果 <paramref name="progID" /> 是注册表中的有效项且有与之关联的类型，则为与指定程序标识符 (ProgID) 关联的类型；否则为 null。</returns>
    /// <param name="progID">要获取的类型的 ProgID。</param>
    /// <param name="throwOnError">true 将引发所发生的任何异常。- 或 - false 将忽略所发生的任何异常。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="progID" /> 为 null。</exception>
    /// <exception cref="T:System.Runtime.InteropServices.COMException">未注册指定的 ProgID。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static Type GetTypeFromProgID(string progID, bool throwOnError)
    {
      return RuntimeType.GetTypeFromProgIDImpl(progID, (string) null, throwOnError);
    }

    /// <summary>从指定服务器获取与指定程序标识符 (progID) 关联的类型，如果在加载该类型时遇到错误则返回空值。</summary>
    /// <returns>如果 <paramref name="progID" /> 是注册表中的有效项且有与之关联的类型，则为与指定程序标识符 (ProgID) 关联的类型；否则为 null。</returns>
    /// <param name="progID">要获取的类型的 ProgID。</param>
    /// <param name="server">用于从中加载该类型的服务器。如果服务器名称为 null，则此方法会自动恢复到本地计算机上。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="prodID" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static Type GetTypeFromProgID(string progID, string server)
    {
      return RuntimeType.GetTypeFromProgIDImpl(progID, server, false);
    }

    /// <summary>从指定服务器获取与指定程序标识符 (progID) 关联的类型，指定如果在加载该类型时发生错误是否引发异常。</summary>
    /// <returns>如果 <paramref name="progID" /> 是注册表中的有效项且有与之关联的类型，则为与指定程序标识符 (ProgID) 关联的类型；否则为 null。</returns>
    /// <param name="progID">要获取的 <see cref="T:System.Type" /> 的 progID。</param>
    /// <param name="server">用于从中加载该类型的服务器。如果服务器名称为 null，则此方法会自动恢复到本地计算机上。</param>
    /// <param name="throwOnError">true 将引发所发生的任何异常。- 或 - false 将忽略所发生的任何异常。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="progID" /> 为 null。</exception>
    /// <exception cref="T:System.Runtime.InteropServices.COMException">指定 progID 未注册。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static Type GetTypeFromProgID(string progID, string server, bool throwOnError)
    {
      return RuntimeType.GetTypeFromProgIDImpl(progID, server, throwOnError);
    }

    /// <summary>获取与指定类标识符 (CLSID) 关联的类型。</summary>
    /// <returns>System.__ComObject，无论 CLSID 是否有效。</returns>
    /// <param name="clsid">要获取的类型的 CLSID。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Type GetTypeFromCLSID(Guid clsid)
    {
      return RuntimeType.GetTypeFromCLSIDImpl(clsid, (string) null, false);
    }

    /// <summary>获取与指定类标识符 (CLSID) 关联的类型，指定在加载该类型时如果发生错误是否引发异常。</summary>
    /// <returns>System.__ComObject，无论 CLSID 是否有效。</returns>
    /// <param name="clsid">要获取的类型的 CLSID。</param>
    /// <param name="throwOnError">true 将引发所发生的任何异常。- 或 - false 将忽略所发生的任何异常。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public static Type GetTypeFromCLSID(Guid clsid, bool throwOnError)
    {
      return RuntimeType.GetTypeFromCLSIDImpl(clsid, (string) null, throwOnError);
    }

    /// <summary>从指定服务器获取与指定类标识符 (CLSID) 关联的类型。</summary>
    /// <returns>System.__ComObject，无论 CLSID 是否有效。</returns>
    /// <param name="clsid">要获取的类型的 CLSID。</param>
    /// <param name="server">用于从中加载该类型的服务器。如果服务器名称为 null，则此方法会自动恢复到本地计算机上。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public static Type GetTypeFromCLSID(Guid clsid, string server)
    {
      return RuntimeType.GetTypeFromCLSIDImpl(clsid, server, false);
    }

    /// <summary>从指定服务器获取与指定类标识符 (CLSID) 关联的类型，指定在加载该类型时如果发生错误是否引发异常。</summary>
    /// <returns>System.__ComObject，无论 CLSID 是否有效。</returns>
    /// <param name="clsid">要获取的类型的 CLSID。</param>
    /// <param name="server">用于从中加载该类型的服务器。如果服务器名称为 null，则此方法会自动恢复到本地计算机上。</param>
    /// <param name="throwOnError">true 将引发所发生的任何异常。- 或 - false 将忽略所发生的任何异常。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public static Type GetTypeFromCLSID(Guid clsid, string server, bool throwOnError)
    {
      return RuntimeType.GetTypeFromCLSIDImpl(clsid, server, throwOnError);
    }

    /// <summary>获取指定 <see cref="T:System.Type" /> 的基础类型代码。</summary>
    /// <returns>如果 <see cref="F:System.TypeCode.Empty" /> 为 <paramref name="type" />，则为基础类型代码或 null。</returns>
    /// <param name="type">要获取其基础代码的类型。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static TypeCode GetTypeCode(Type type)
    {
      if (type == (Type) null)
        return TypeCode.Empty;
      return type.GetTypeCodeImpl();
    }

    /// <summary>返回指定 <see cref="T:System.Type" /> 的基础类型代码。</summary>
    /// <returns>基础类型的代码。</returns>
    protected virtual TypeCode GetTypeCodeImpl()
    {
      if (this != this.UnderlyingSystemType && this.UnderlyingSystemType != (Type) null)
        return Type.GetTypeCode(this.UnderlyingSystemType);
      return TypeCode.Object;
    }

    private static void CreateBinder()
    {
      if (Type.defaultBinder != null)
        return;
      System.DefaultBinder defaultBinder = new System.DefaultBinder();
      Interlocked.CompareExchange<Binder>(ref Type.defaultBinder, (Binder) defaultBinder, (Binder) null);
    }

    /// <summary>当在派生类中重写时，使用指定的绑定约束并匹配指定的参数列表、修饰符和区域性，调用指定成员。</summary>
    /// <returns>一个对象，表示被调用成员的返回值。</returns>
    /// <param name="name">字符串，它包含要调用的构造函数、方法、属性或字段成员的名称。- 或 - 空字符串 ("")，表示调用默认成员。- 或 -对于 IDispatch 成员，则为一个表示 DispID 的字符串，例如"[DispID=3]"。</param>
    /// <param name="invokeAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。访问可以是 BindingFlags 之一，如 Public、NonPublic、Private、InvokeMethod 和 GetField 等。查找类型无需指定。如果省略查找的类型，则将使用 BindingFlags.Public   | BindingFlags.Instance | 使用 BindingFlags.Static。</param>
    /// <param name="binder">一个对象，该对象定义一组属性并启用绑定，而绑定可能涉及选择重载方法、强制参数类型和通过反射调用成员。- 或 - 要使用 <see cref="P:System.Type.DefaultBinder" /> 的空引用（在 Visual Basic 中为 Nothing）。请注意，为了成功地使用变量参数来调用方法重载，可能必须显式定义 <see cref="T:System.Reflection.Binder" /> 对象。</param>
    /// <param name="target">对其调用指定成员的对象。</param>
    /// <param name="args">包含传递给要调用的成员的参数的数组。</param>
    /// <param name="modifiers">
    /// <see cref="T:System.Reflection.ParameterModifier" /> 对象的数组，表示与 <paramref name="args" /> 数组中的相应元素关联的特性。参数的关联的属性存储在成员的签名中。只有在调用 COM 组件时，默认联编程序才处理此参数。</param>
    /// <param name="culture">表示要使用的全局化区域设置的 <see cref="T:System.Globalization.CultureInfo" /> 对象，它对区域设置特定的转换可能是必需的，比如将数字 String 转换为 Double。- 或 - 要使用当前线程的 Nothing 的空引用（在 Visual Basic 中为 <see cref="T:System.Globalization.CultureInfo" />）。</param>
    /// <param name="namedParameters">包含参数名称的数组，<paramref name="args" /> 数组中的值将传递给这些参数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="invokeAttr" /> 不包含 CreateInstance 和 <paramref name="name" /> 是 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="args" /> 和 <paramref name="modifiers" /> 不具有相同的长度。- 或 - <paramref name="invokeAttr" /> 不是有效 <see cref="T:System.Reflection.BindingFlags" /> 属性。- 或 - <paramref name="invokeAttr" /> 不包含以下绑定标志： InvokeMethod, ，CreateInstance, ，GetField, ，SetField, ，GetProperty, ，或 SetProperty。- 或 - <paramref name="invokeAttr" /> 包含 CreateInstance 结合 InvokeMethod, ，GetField, ，SetField, ，GetProperty, ，或 SetProperty。- 或 - <paramref name="invokeAttr" /> 同时包含 GetField 和 SetField。- 或 - <paramref name="invokeAttr" /> 同时包含 GetProperty 和 SetProperty。- 或 - <paramref name="invokeAttr" /> 包含 InvokeMethod 结合 SetField 或 SetProperty。- 或 - <paramref name="invokeAttr" /> 包含 SetField 和 <paramref name="args" /> 具有多个元素。- 或 - 命名的参数数组大于参数数组。- 或 - 对 COM 对象调用此方法，而且中未传递以下绑定标志之一： BindingFlags.InvokeMethod, ，BindingFlags.GetProperty, ，BindingFlags.SetProperty, ，BindingFlags.PutDispProperty, ，或 BindingFlags.PutRefDispProperty。- 或 - 其中一个命名的参数数组包含一个字符串，它 null。</exception>
    /// <exception cref="T:System.MethodAccessException">指定的成员是类初始值设定项。</exception>
    /// <exception cref="T:System.MissingFieldException">找不到的字段或属性。</exception>
    /// <exception cref="T:System.MissingMethodException">没有方法找不到程序中的参数匹配 <paramref name="args" />。- 或 - 可以找到具有中提供的参数名称的任何成员 <paramref name="namedParameters" />。- 或 - 当前 <see cref="T:System.Type" /> 对象都表示一个类型，包含开放类型参数，即 <see cref="P:System.Type.ContainsGenericParameters" /> 返回 true。</exception>
    /// <exception cref="T:System.Reflection.TargetException">指定的成员不能在调用 <paramref name="target" />。</exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">多个方法匹配绑定条件。</exception>
    /// <exception cref="T:System.InvalidOperationException">所表示的方法 <paramref name="name" /> 具有一个或多个未指定的泛型类型参数。也就是说，该方法的 <see cref="P:System.Reflection.MethodInfo.ContainsGenericParameters" /> 属性将返回 true。</exception>
    /// <filterpriority>2</filterpriority>
    public abstract object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

    /// <summary>使用指定的绑定约束和匹配的指定参数列表及区域性来调用指定成员。</summary>
    /// <returns>一个对象，表示被调用成员的返回值。</returns>
    /// <param name="name">字符串，它包含要调用的构造函数、方法、属性或字段成员的名称。- 或 - 空字符串 ("")，表示调用默认成员。- 或 -对于 IDispatch 成员，则为一个表示 DispID 的字符串，例如"[DispID=3]"。</param>
    /// <param name="invokeAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。访问可以是 BindingFlags 之一，如 Public、NonPublic、Private、InvokeMethod 和 GetField 等。查找类型无需指定。如果省略查找的类型，则将使用 BindingFlags.Public   | BindingFlags.Instance | 使用 BindingFlags.Static。</param>
    /// <param name="binder">一个对象，该对象定义一组属性并启用绑定，而绑定可能涉及选择重载方法、强制参数类型和通过反射调用成员。- 或 - 要使用 Nothing 的空引用（在 Visual Basic 中为 <see cref="P:System.Type.DefaultBinder" />）。请注意，为了成功地使用变量参数来调用方法重载，可能必须显式定义 <see cref="T:System.Reflection.Binder" /> 对象。</param>
    /// <param name="target">对其调用指定成员的对象。</param>
    /// <param name="args">包含传递给要调用的成员的参数的数组。</param>
    /// <param name="culture">表示要使用的全局化区域设置的对象，它对区域设置特定的转换可能是必需的，比如将数字 <see cref="T:System.String" /> 转换为 <see cref="T:System.Double" />。- 或 - 要使用当前线程的 Nothing 的空引用（在 Visual Basic 中为 <see cref="T:System.Globalization.CultureInfo" />）。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="invokeAttr" /> 不包含 CreateInstance 和 <paramref name="name" /> 是 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="invokeAttr" /> 不是有效 <see cref="T:System.Reflection.BindingFlags" /> 属性。- 或 - <paramref name="invokeAttr" /> 不包含以下绑定标志： InvokeMethod, ，CreateInstance, ，GetField, ，SetField, ，GetProperty, ，或 SetProperty。- 或 - <paramref name="invokeAttr" /> 包含 CreateInstance 结合 InvokeMethod, ，GetField, ，SetField, ，GetProperty, ，或 SetProperty。- 或 - <paramref name="invokeAttr" /> 同时包含 GetField 和 SetField。- 或 - <paramref name="invokeAttr" /> 同时包含 GetProperty 和 SetProperty。- 或 - <paramref name="invokeAttr" /> 包含 InvokeMethod 结合 SetField 或 SetProperty。- 或 - <paramref name="invokeAttr" /> 包含 SetField 和 <paramref name="args" /> 具有多个元素。- 或 - 对 COM 对象调用此方法，而且中未传递以下绑定标志之一： BindingFlags.InvokeMethod, ，BindingFlags.GetProperty, ，BindingFlags.SetProperty, ，BindingFlags.PutDispProperty, ，或 BindingFlags.PutRefDispProperty。- 或 - 其中一个命名的参数数组包含一个字符串，它 null。</exception>
    /// <exception cref="T:System.MethodAccessException">指定的成员是类初始值设定项。</exception>
    /// <exception cref="T:System.MissingFieldException">找不到的字段或属性。</exception>
    /// <exception cref="T:System.MissingMethodException">没有方法找不到程序中的参数匹配 <paramref name="args" />。- 或 - 当前 <see cref="T:System.Type" /> 对象都表示一个类型，包含开放类型参数，即 <see cref="P:System.Type.ContainsGenericParameters" /> 返回 true。</exception>
    /// <exception cref="T:System.Reflection.TargetException">指定的成员不能在调用 <paramref name="target" />。</exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">多个方法匹配绑定条件。</exception>
    /// <exception cref="T:System.InvalidOperationException">所表示的方法 <paramref name="name" /> 具有一个或多个未指定的泛型类型参数。也就是说，该方法的 <see cref="P:System.Reflection.MethodInfo.ContainsGenericParameters" /> 属性将返回 true。</exception>
    /// <filterpriority>2</filterpriority>
    [DebuggerStepThrough]
    [DebuggerHidden]
    public object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, CultureInfo culture)
    {
      return this.InvokeMember(name, invokeAttr, binder, target, args, (ParameterModifier[]) null, culture, (string[]) null);
    }

    /// <summary>使用指定的绑定约束并匹配指定的参数列表，调用指定成员。</summary>
    /// <returns>一个对象，表示被调用成员的返回值。</returns>
    /// <param name="name">字符串，它包含要调用的构造函数、方法、属性或字段成员的名称。- 或 - 空字符串 ("")，表示调用默认成员。- 或 -对于 IDispatch 成员，则为一个表示 DispID 的字符串，例如"[DispID=3]"。</param>
    /// <param name="invokeAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。访问可以是 BindingFlags 之一，如 Public、NonPublic、Private、InvokeMethod 和 GetField 等。查找类型无需指定。如果省略查找的类型，则将使用 BindingFlags.Public   | BindingFlags.Instance | 使用 BindingFlags.Static。</param>
    /// <param name="binder">一个对象，该对象定义一组属性并启用绑定，而绑定可能涉及选择重载方法、强制参数类型和通过反射调用成员。- 或 - 要使用 Nothing 的空引用（在 Visual Basic 中为 <see cref="P:System.Type.DefaultBinder" />）。请注意，为了成功地使用变量参数来调用方法重载，可能必须显式定义 <see cref="T:System.Reflection.Binder" /> 对象。</param>
    /// <param name="target">对其调用指定成员的对象。</param>
    /// <param name="args">包含传递给要调用的成员的参数的数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="invokeAttr" /> 不包含 CreateInstance 和 <paramref name="name" /> 是 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="invokeAttr" /> 不是有效 <see cref="T:System.Reflection.BindingFlags" /> 属性。- 或 - <paramref name="invokeAttr" /> 不包含以下绑定标志： InvokeMethod, ，CreateInstance, ，GetField, ，SetField, ，GetProperty, ，或 SetProperty。- 或 - <paramref name="invokeAttr" /> 包含 CreateInstance 结合 InvokeMethod, ，GetField, ，SetField, ，GetProperty, ，或 SetProperty。- 或 - <paramref name="invokeAttr" /> 同时包含 GetField 和 SetField。- 或 - <paramref name="invokeAttr" /> 同时包含 GetProperty 和 SetProperty。- 或 - <paramref name="invokeAttr" /> 包含 InvokeMethod 结合 SetField 或 SetProperty。- 或 - <paramref name="invokeAttr" /> 包含 SetField 和 <paramref name="args" /> 具有多个元素。- 或 - 对 COM 对象调用此方法，而且中未传递以下绑定标志之一： BindingFlags.InvokeMethod, ，BindingFlags.GetProperty, ，BindingFlags.SetProperty, ，BindingFlags.PutDispProperty, ，或 BindingFlags.PutRefDispProperty。- 或 - 其中一个命名的参数数组包含一个字符串，它 null。</exception>
    /// <exception cref="T:System.MethodAccessException">指定的成员是类初始值设定项。</exception>
    /// <exception cref="T:System.MissingFieldException">找不到的字段或属性。</exception>
    /// <exception cref="T:System.MissingMethodException">没有方法找不到程序中的参数匹配 <paramref name="args" />。- 或 - 当前 <see cref="T:System.Type" /> 对象都表示一个类型，包含开放类型参数，即 <see cref="P:System.Type.ContainsGenericParameters" /> 返回 true。</exception>
    /// <exception cref="T:System.Reflection.TargetException">指定的成员不能在调用 <paramref name="target" />。</exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">多个方法匹配绑定条件。</exception>
    /// <exception cref="T:System.NotSupportedException">.NET Compact Framework 当前不支持此方法。</exception>
    /// <exception cref="T:System.InvalidOperationException">所表示的方法 <paramref name="name" /> 具有一个或多个未指定的泛型类型参数。也就是说，该方法的 <see cref="P:System.Reflection.MethodInfo.ContainsGenericParameters" /> 属性将返回 true。</exception>
    /// <filterpriority>2</filterpriority>
    [DebuggerStepThrough]
    [DebuggerHidden]
    public object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args)
    {
      return this.InvokeMember(name, invokeAttr, binder, target, args, (ParameterModifier[]) null, (CultureInfo) null, (string[]) null);
    }

    internal virtual RuntimeTypeHandle GetTypeHandleInternal()
    {
      return this.TypeHandle;
    }

    /// <summary>获取指定对象的 <see cref="T:System.Type" /> 的句柄。</summary>
    /// <returns>指定 <see cref="T:System.Type" /> 的 <see cref="T:System.Object" /> 的句柄。</returns>
    /// <param name="o">要获取类型句柄的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="o" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static RuntimeTypeHandle GetTypeHandle(object o)
    {
      if (o == null)
        throw new ArgumentNullException((string) null, Environment.GetResourceString("Arg_InvalidHandle"));
      return new RuntimeTypeHandle((RuntimeType) o.GetType());
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeType GetTypeFromHandleUnsafe(IntPtr handle);

    /// <summary>获取由指定类型句柄引用的类型。</summary>
    /// <returns>如果 <see cref="T:System.RuntimeTypeHandle" /> 的 null 属性为 <see cref="P:System.RuntimeTypeHandle.Value" />，则为由指定的 <paramref name="handle" /> 引用的类型，或者为 null。</returns>
    /// <param name="handle">引用类型的对象。</param>
    /// <exception cref="T:System.Reflection.TargetInvocationException">类初始值设定项将调用，并且会引发异常。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern Type GetTypeFromHandle(RuntimeTypeHandle handle);

    /// <summary>获取数组中的维数。</summary>
    /// <returns>包含当前类型中维数的整数。</returns>
    /// <exception cref="T:System.NotSupportedException">此方法的功能在基类中不受支持，并且必须改为在派生类中实现。</exception>
    /// <exception cref="T:System.ArgumentException">当前的类型不是数组。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual int GetArrayRank()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    /// <summary>用指定绑定约束和指定调用约定，搜索其参数与指定参数类型及修饰符匹配的构造函数。</summary>
    /// <returns>表示符合指定要求的构造函数的对象（如果找到的话）；否则为 null。</returns>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 - 零，以返回 null。</param>
    /// <param name="binder">一个对象，该对象定义一组属性并启用绑定，而绑定可能涉及选择重载方法、强制参数类型和通过反射调用成员。- 或 - 要使用 Nothing 的空引用（在 Visual Basic 中为 <see cref="P:System.Type.DefaultBinder" />）。</param>
    /// <param name="callConvention">对象，用于指定要使用的一套规则，这些规则涉及参数的顺序和布局、传递返回值的方式、用于参数的寄存器和清理堆栈的方式。</param>
    /// <param name="types">
    /// <see cref="T:System.Type" /> 对象的数组，表示构造函数要获取的参数的个数、顺序和类型。- 或 - 获取不使用参数的构造函数的 <see cref="T:System.Type" /> 类型的空数组（即 Type[] types = new Type[0]）。</param>
    /// <param name="modifiers">
    /// <see cref="T:System.Reflection.ParameterModifier" /> 对象的数组，表示与 <paramref name="types" /> 数组中的相应元素关联的特性。默认的联编程序不处理此参数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="types" /> 为 null。- 或 - 中的元素之一 <paramref name="types" /> 是 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="types" /> 是多维的。- 或 - <paramref name="modifiers" /> 是多维的。- 或 - <paramref name="types" /> 和 <paramref name="modifiers" /> 不具有相同的长度。</exception>
    /// <filterpriority>2</filterpriority>
    [ComVisible(true)]
    public ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      if (types == null)
        throw new ArgumentNullException("types");
      for (int index = 0; index < types.Length; ++index)
      {
        if (types[index] == (Type) null)
          throw new ArgumentNullException("types");
      }
      return this.GetConstructorImpl(bindingAttr, binder, callConvention, types, modifiers);
    }

    /// <summary>使用指定绑定约束搜索其参数与指定参数类型和修饰符匹配的构造函数。</summary>
    /// <returns>表示符合指定要求的构造函数的 <see cref="T:System.Reflection.ConstructorInfo" /> 对象（如果找到的话）；否则为 null。</returns>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 - 零，以返回 null。</param>
    /// <param name="binder">一个对象，该对象定义一组属性并启用绑定，而绑定可能涉及选择重载方法、强制参数类型和通过反射调用成员。- 或 - 要使用 Nothing 的空引用（在 Visual Basic 中为 <see cref="P:System.Type.DefaultBinder" />）。</param>
    /// <param name="types">
    /// <see cref="T:System.Type" /> 对象的数组，表示构造函数要获取的参数的个数、顺序和类型。- 或 - 获取不使用参数的构造函数的 <see cref="T:System.Type" /> 类型的空数组（即 Type[] types = new Type[0]）。- 或 - <see cref="F:System.Type.EmptyTypes" />.</param>
    /// <param name="modifiers">
    /// <see cref="T:System.Reflection.ParameterModifier" /> 对象的数组，表示与参数类型数组中的相应元素关联的特性。默认的联编程序不处理此参数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="types" /> 为 null。- 或 - 中的元素之一 <paramref name="types" /> 是 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="types" /> 是多维的。- 或 - <paramref name="modifiers" /> 是多维的。- 或 - <paramref name="types" /> 和 <paramref name="modifiers" /> 不具有相同的长度。</exception>
    /// <filterpriority>2</filterpriority>
    [ComVisible(true)]
    public ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
    {
      if (types == null)
        throw new ArgumentNullException("types");
      for (int index = 0; index < types.Length; ++index)
      {
        if (types[index] == (Type) null)
          throw new ArgumentNullException("types");
      }
      return this.GetConstructorImpl(bindingAttr, binder, CallingConventions.Any, types, modifiers);
    }

    /// <summary>搜索其参数与指定数组中的类型匹配的公共实例构造函数。</summary>
    /// <returns>为表示某个公共实例构造函数（该构造函数的参数与参数类型数组中的类型匹配）的对象（如果找到的话）；否则为 null。</returns>
    /// <param name="types">表示需要的构造函数的参数个数、顺序和类型的 <see cref="T:System.Type" /> 对象的数组。- 或 - <see cref="T:System.Type" /> 对象的空数组，用于获取不带参数的构造函数。这样的空数组由 static 字段 <see cref="F:System.Type.EmptyTypes" /> 提供。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="types" /> 为 null。- 或 - 中的元素之一 <paramref name="types" /> 是 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="types" /> 是多维的。</exception>
    /// <filterpriority>2</filterpriority>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public ConstructorInfo GetConstructor(Type[] types)
    {
      return this.GetConstructor(BindingFlags.Instance | BindingFlags.Public, (Binder) null, types, (ParameterModifier[]) null);
    }

    /// <summary>当在派生类中重写时，使用指定的绑定约束和指定的调用约定搜索其参数与指定的参数类型和修饰符匹配的构造函数。</summary>
    /// <returns>表示符合指定要求的构造函数的 <see cref="T:System.Reflection.ConstructorInfo" /> 对象（如果找到的话）；否则为 null。</returns>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 - 零，以返回 null。</param>
    /// <param name="binder">一个对象，该对象定义一组属性并启用绑定，而绑定可能涉及选择重载方法、强制参数类型和通过反射调用成员。- 或 - 要使用 Nothing 的空引用（在 Visual Basic 中为 <see cref="P:System.Type.DefaultBinder" />）。</param>
    /// <param name="callConvention">对象，用于指定要使用的一套规则，这些规则涉及参数的顺序和布局、传递返回值的方式、用于参数的寄存器和清理堆栈的方式。</param>
    /// <param name="types">
    /// <see cref="T:System.Type" /> 对象的数组，表示构造函数要获取的参数的个数、顺序和类型。- 或 - 获取不使用参数的构造函数的 <see cref="T:System.Type" /> 类型的空数组（即 Type[] types = new Type[0]）。</param>
    /// <param name="modifiers">
    /// <see cref="T:System.Reflection.ParameterModifier" /> 对象的数组，表示与 <paramref name="types" /> 数组中的相应元素关联的特性。默认的联编程序不处理此参数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="types" /> 为 null。- 或 - 中的元素之一 <paramref name="types" /> 是 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="types" /> 是多维的。- 或 - <paramref name="modifiers" /> 是多维的。- 或 - <paramref name="types" /> 和 <paramref name="modifiers" /> 不具有相同的长度。</exception>
    /// <exception cref="T:System.NotSupportedException">当前的类型是 <see cref="T:System.Reflection.Emit.TypeBuilder" /> 或 <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />。</exception>
    protected abstract ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

    /// <summary>返回为当前 <see cref="T:System.Type" /> 定义的所有公共构造函数。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.ConstructorInfo" /> 对象的数组，表示当前 <see cref="T:System.Type" /> 定义的所有公共实例构造函数，但不包括类型初始值设定项（静态构造函数）。如果没有为当前 <see cref="T:System.Type" /> 定义公共实例构造函数，或者当前 <see cref="T:System.Type" /> 表示泛型类型或泛型方法的定义中的类型参数，则返回 <see cref="T:System.Reflection.ConstructorInfo" /> 类型的空数组。</returns>
    /// <filterpriority>2</filterpriority>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public ConstructorInfo[] GetConstructors()
    {
      return this.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
    }

    /// <summary>当在派生类中重写时，使用指定 <see cref="T:System.Type" /> 搜索为当前 BindingFlags 定义的构造函数。</summary>
    /// <returns>表示为当前 <see cref="T:System.Reflection.ConstructorInfo" /> 定义的匹配指定绑定约束的所有构造函数的 <see cref="T:System.Type" /> 对象数组，包括类型初始值设定项（如果定义的话）。如果当前 <see cref="T:System.Reflection.ConstructorInfo" /> 没有定义构造函数，或者定义的构造函数都不符合绑定约束，或者当前 <see cref="T:System.Type" /> 表示泛型类型或泛型方法定义的类型参数，则返回 <see cref="T:System.Type" /> 类型的空数组。</returns>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 - 零，以返回 null。</param>
    /// <filterpriority>2</filterpriority>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public abstract ConstructorInfo[] GetConstructors(BindingFlags bindingAttr);

    /// <summary>用指定的绑定约束和指定的调用约定，搜索参数与指定的参数类型及修饰符相匹配的指定方法。</summary>
    /// <returns>表示符合指定要求的方法的对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的方法名称的字符串。</param>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 - 零，以返回 null。</param>
    /// <param name="binder">一个对象，该对象定义一组属性并启用绑定，而绑定可能涉及选择重载方法、强制参数类型和通过反射调用成员。- 或 - 要使用 Nothing 的空引用（在 Visual Basic 中为 <see cref="P:System.Type.DefaultBinder" />）。</param>
    /// <param name="callConvention">该对象用于指定要使用的一套规则，这些规则涉及参数的顺序和布局、传递返回值的方式、用于参数的寄存器和清理堆栈的方式。</param>
    /// <param name="types">表示此方法要获取的参数的个数、顺序和类型的 <see cref="T:System.Type" /> 对象数组。- 或 - 空的 <see cref="T:System.Type" /> 对象数组（由 <see cref="F:System.Type.EmptyTypes" /> 字段提供），用来获取不采用参数的方法。</param>
    /// <param name="modifiers">
    /// <see cref="T:System.Reflection.ParameterModifier" /> 对象的数组，表示与 <paramref name="types" /> 数组中的相应元素关联的特性。仅当通过 COM 互操作进行调用时才使用，而且仅处理通过引用传递的参数。默认的联编程序不处理此参数。</param>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">多个方法找到具有指定名称和匹配指定的绑定约束。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。- 或 - <paramref name="types" /> 为 null。- 或 - 中的元素之一 <paramref name="types" /> 是 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="types" /> 是多维的。- 或 - <paramref name="modifiers" /> 是多维的。</exception>
    /// <filterpriority>2</filterpriority>
    public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (types == null)
        throw new ArgumentNullException("types");
      for (int index = 0; index < types.Length; ++index)
      {
        if (types[index] == (Type) null)
          throw new ArgumentNullException("types");
      }
      return this.GetMethodImpl(name, bindingAttr, binder, callConvention, types, modifiers);
    }

    /// <summary>使用指定绑定约束，搜索其参数与指定参数类型及修饰符匹配的指定方法。</summary>
    /// <returns>表示符合指定要求的方法的对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的方法名称的字符串。</param>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 - 零，以返回 null。</param>
    /// <param name="binder">一个对象，该对象定义一组属性并启用绑定，而绑定可能涉及选择重载方法、强制参数类型和通过反射调用成员。- 或 - 要使用 Nothing 的空引用（在 Visual Basic 中为 <see cref="P:System.Type.DefaultBinder" />）。</param>
    /// <param name="types">表示此方法要获取的参数的个数、顺序和类型的 <see cref="T:System.Type" /> 对象数组。- 或 - 空的 <see cref="T:System.Type" /> 对象数组（由 <see cref="F:System.Type.EmptyTypes" /> 字段提供），用来获取不采用参数的方法。</param>
    /// <param name="modifiers">
    /// <see cref="T:System.Reflection.ParameterModifier" /> 对象的数组，表示与 <paramref name="types" /> 数组中的相应元素关联的特性。仅当通过 COM 互操作进行调用时才使用，而且仅处理通过引用传递的参数。默认的联编程序不处理此参数。</param>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">多个方法找到具有指定名称和匹配指定的绑定约束。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。- 或 - <paramref name="types" /> 为 null。- 或 - 中的元素之一 <paramref name="types" /> 是 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="types" /> 是多维的。- 或 - <paramref name="modifiers" /> 是多维的。</exception>
    /// <filterpriority>2</filterpriority>
    public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (types == null)
        throw new ArgumentNullException("types");
      for (int index = 0; index < types.Length; ++index)
      {
        if (types[index] == (Type) null)
          throw new ArgumentNullException("types");
      }
      return this.GetMethodImpl(name, bindingAttr, binder, CallingConventions.Any, types, modifiers);
    }

    /// <summary>搜索其参数与指定参数类型及修饰符匹配的指定公共方法。</summary>
    /// <returns>表示符合指定要求的公共方法的对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的公共方法的名称的字符串。</param>
    /// <param name="types">表示此方法要获取的参数的个数、顺序和类型的 <see cref="T:System.Type" /> 对象数组。- 或 - 空的 <see cref="T:System.Type" /> 对象数组（由 <see cref="F:System.Type.EmptyTypes" /> 字段提供），用来获取不采用参数的方法。</param>
    /// <param name="modifiers">
    /// <see cref="T:System.Reflection.ParameterModifier" /> 对象的数组，表示与 <paramref name="types" /> 数组中的相应元素关联的特性。仅当通过 COM 互操作进行调用时才使用，而且仅处理通过引用传递的参数。默认的联编程序不处理此参数。</param>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">多个方法找到具有指定名称和指定的参数。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。- 或 - <paramref name="types" /> 为 null。- 或 - 中的元素之一 <paramref name="types" /> 是 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="types" /> 是多维的。- 或 - <paramref name="modifiers" /> 是多维的。</exception>
    /// <filterpriority>2</filterpriority>
    public MethodInfo GetMethod(string name, Type[] types, ParameterModifier[] modifiers)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (types == null)
        throw new ArgumentNullException("types");
      for (int index = 0; index < types.Length; ++index)
      {
        if (types[index] == (Type) null)
          throw new ArgumentNullException("types");
      }
      return this.GetMethodImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) null, CallingConventions.Any, types, modifiers);
    }

    /// <summary>搜索其参数与指定参数类型匹配的指定公共方法。</summary>
    /// <returns>表示其参数与指定参数类型匹配的公共方法的对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的公共方法的名称的字符串。</param>
    /// <param name="types">表示此方法要获取的参数的个数、顺序和类型的 <see cref="T:System.Type" /> 对象数组。- 或 - 空的 <see cref="T:System.Type" /> 对象数组（由 <see cref="F:System.Type.EmptyTypes" /> 字段提供），用来获取不采用参数的方法。</param>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">多个方法找到具有指定名称和指定的参数。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。- 或 - <paramref name="types" /> 为 null。- 或 - 中的元素之一 <paramref name="types" /> 是 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="types" /> 是多维的。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public MethodInfo GetMethod(string name, Type[] types)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (types == null)
        throw new ArgumentNullException("types");
      for (int index = 0; index < types.Length; ++index)
      {
        if (types[index] == (Type) null)
          throw new ArgumentNullException("types");
      }
      return this.GetMethodImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) null, CallingConventions.Any, types, (ParameterModifier[]) null);
    }

    /// <summary>使用指定绑定约束搜索指定方法。</summary>
    /// <returns>表示符合指定要求的方法的对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的方法名称的字符串。</param>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 - 零，以返回 null。</param>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">多个方法找到具有指定名称和匹配指定的绑定约束。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public MethodInfo GetMethod(string name, BindingFlags bindingAttr)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      return this.GetMethodImpl(name, bindingAttr, (Binder) null, CallingConventions.Any, (Type[]) null, (ParameterModifier[]) null);
    }

    /// <summary>搜索具有指定名称的公共方法。</summary>
    /// <returns>表示具有指定名称的公共方法的对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的公共方法的名称的字符串。</param>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">多个方法是找到具有指定名称。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public MethodInfo GetMethod(string name)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      return this.GetMethodImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) null, CallingConventions.Any, (Type[]) null, (ParameterModifier[]) null);
    }

    /// <summary>当在派生类中重写时，使用指定的绑定约束和指定的调用约定搜索其参数与指定的参数类型和修饰符匹配的指定方法。</summary>
    /// <returns>表示符合指定要求的方法的对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的方法名称的字符串。</param>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 - 零，以返回 null。</param>
    /// <param name="binder">一个对象，该对象定义一组属性并启用绑定，而绑定可能涉及选择重载方法、强制参数类型和通过反射调用成员。- 或 - 要使用 Nothing 的空引用（在 Visual Basic 中为 <see cref="P:System.Type.DefaultBinder" />）。</param>
    /// <param name="callConvention">该对象，用于指定要使用的一套规则，这些规则涉及参数的顺序和布局、传递返回值的方式、用于参数的寄存器以及哪个进程清理堆栈。</param>
    /// <param name="types">表示此方法要获取的参数的个数、顺序和类型的 <see cref="T:System.Type" /> 对象数组。- 或 - 一个类型为 <see cref="T:System.Type" />（即 Type[] types = new Type[0]）的空数组，用于获取一个不带参数的方法。- 或 - null.如果 <paramref name="types" /> 为 null，则参数不匹配。</param>
    /// <param name="modifiers">
    /// <see cref="T:System.Reflection.ParameterModifier" /> 对象的数组，表示与 <paramref name="types" /> 数组中的相应元素关联的特性。默认的联编程序不处理此参数。</param>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">多个方法找到具有指定名称和匹配指定的绑定约束。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="types" /> 是多维的。- 或 - <paramref name="modifiers" /> 是多维的。- 或 - <paramref name="types" /> 和 <paramref name="modifiers" /> 不具有相同的长度。</exception>
    /// <exception cref="T:System.NotSupportedException">当前的类型是 <see cref="T:System.Reflection.Emit.TypeBuilder" /> 或 <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />。</exception>
    protected abstract MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

    /// <summary>返回为当前 <see cref="T:System.Type" /> 的所有公共方法。</summary>
    /// <returns>表示为当前 <see cref="T:System.Reflection.MethodInfo" /> 定义的所有公共方法的 <see cref="T:System.Type" /> 对象数组。- 或 - 如果没有为当前 <see cref="T:System.Reflection.MethodInfo" /> 定义的公共方法，则为 <see cref="T:System.Type" /> 类型的空数组。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public MethodInfo[] GetMethods()
    {
      return this.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    /// <summary>当在派生类中重写时，使用指定绑定约束，搜索为当前 <see cref="T:System.Type" /> 定义的方法。</summary>
    /// <returns>表示为当前 <see cref="T:System.Reflection.MethodInfo" /> 定义的匹配指定绑定约束的所有方法的 <see cref="T:System.Type" /> 对象数组。- 或 - 如果没有为当前 <see cref="T:System.Reflection.MethodInfo" /> 定义的方法，或者如果没有一个定义的方法匹配绑定约束，则为 <see cref="T:System.Type" /> 类型的空数组。</returns>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 - 零，以返回 null。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract MethodInfo[] GetMethods(BindingFlags bindingAttr);

    /// <summary>使用指定绑定约束搜索指定字段。</summary>
    /// <returns>表示符合指定要求的字段的对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的数据字段的名称的字符串。</param>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 - 零，以返回 null。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract FieldInfo GetField(string name, BindingFlags bindingAttr);

    /// <summary>搜索具有指定名称的公共字段。</summary>
    /// <returns>如找到，则为表示具有指定名称的公共字段的对象；否则为 null。</returns>
    /// <param name="name">包含要获取的数据字段的名称的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">这 <see cref="T:System.Type" /> 对象是 <see cref="T:System.Reflection.Emit.TypeBuilder" /> 其 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 尚未调用方法。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public FieldInfo GetField(string name)
    {
      return this.GetField(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    /// <summary>返回当前 <see cref="T:System.Type" /> 的所有公共字段。</summary>
    /// <returns>表示为当前 <see cref="T:System.Reflection.FieldInfo" /> 定义的所有公共字段的 <see cref="T:System.Type" /> 对象数组。- 或 - 如果没有为当前 <see cref="T:System.Reflection.FieldInfo" /> 定义的公共字段，则为 <see cref="T:System.Type" /> 类型的空数组。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public FieldInfo[] GetFields()
    {
      return this.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    /// <summary>当在派生类中重写时，使用指定绑定约束，搜索为当前 <see cref="T:System.Type" /> 定义的字段。</summary>
    /// <returns>表示为当前 <see cref="T:System.Reflection.FieldInfo" /> 定义的匹配指定绑定约束的所有字段的 <see cref="T:System.Type" /> 对象数组。- 或 - 如果没有为当前 <see cref="T:System.Reflection.FieldInfo" /> 定义的字段，或者如果没有一个定义的字段匹配绑定约束，则为 <see cref="T:System.Type" /> 类型的空数组。</returns>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 - 零，以返回 null。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract FieldInfo[] GetFields(BindingFlags bindingAttr);

    /// <summary>搜索具有指定名称的接口。</summary>
    /// <returns>表示具有指定名称且由当前的 <see cref="T:System.Type" /> 实现或继承的接口的对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的接口名称的字符串。对于泛型接口，这是重整名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">当前 <see cref="T:System.Type" /> 表示实现使用不同类型的参数相同的泛型接口的类型。</exception>
    /// <filterpriority>2</filterpriority>
    public Type GetInterface(string name)
    {
      return this.GetInterface(name, false);
    }

    /// <summary>当在派生类中重写时，搜索指定的接口，指定是否要对接口名称执行不区分大小写的搜索。</summary>
    /// <returns>表示具有指定名称且由当前的 <see cref="T:System.Type" /> 实现或继承的接口的对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的接口名称的字符串。对于泛型接口，这是重整名称。</param>
    /// <param name="ignoreCase">true 表示对于用来指定简单接口名称的 <paramref name="name" /> 部分（该部分指定命名空间大小写必须正确）忽略其大小写。- 或 - false 表示对 <paramref name="name" /> 的所有部分执行区分大小写的搜索。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">当前 <see cref="T:System.Type" /> 表示实现使用不同类型的参数相同的泛型接口的类型。</exception>
    /// <filterpriority>2</filterpriority>
    public abstract Type GetInterface(string name, bool ignoreCase);

    /// <summary>当在派生类中重写时，获取由当前 <see cref="T:System.Type" /> 实现或继承的所有接口。</summary>
    /// <returns>表示由当前 <see cref="T:System.Type" /> 实现或继承的所有接口的 <see cref="T:System.Type" /> 对象数组。- 或 - 如果没有由当前 <see cref="T:System.Type" /> 实现或继承的接口，则为 <see cref="T:System.Type" /> 类型的空数组。</returns>
    /// <exception cref="T:System.Reflection.TargetInvocationException">静态初始值设定项将调用，并且会引发异常。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract Type[] GetInterfaces();

    /// <summary>返回表示接口（由当前 <see cref="T:System.Type" /> 所实现或继承）的筛选列表的 <see cref="T:System.Type" /> 对象数组。</summary>
    /// <returns>一个表示当前 <see cref="T:System.Type" /> 实现或继承的接口的筛选列表的 <see cref="T:System.Type" /> 对象数组，或者类型 <see cref="T:System.Type" /> 的空数组（如果当前 <see cref="T:System.Type" /> 没有实现或继承匹配筛选器的接口）。</returns>
    /// <param name="filter">对照 <paramref name="filterCriteria" /> 比较接口的委托。</param>
    /// <param name="filterCriteria">确定接口是否应包括在返回数组中的搜索判据。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="filter" /> 为 null。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">静态初始值设定项将调用，并且会引发异常。</exception>
    /// <filterpriority>2</filterpriority>
    public virtual Type[] FindInterfaces(TypeFilter filter, object filterCriteria)
    {
      if (filter == null)
        throw new ArgumentNullException("filter");
      Type[] interfaces = this.GetInterfaces();
      int length = 0;
      for (int index = 0; index < interfaces.Length; ++index)
      {
        if (!filter(interfaces[index], filterCriteria))
          interfaces[index] = (Type) null;
        else
          ++length;
      }
      if (length == interfaces.Length)
        return interfaces;
      Type[] typeArray = new Type[length];
      int num = 0;
      for (int index = 0; index < interfaces.Length; ++index)
      {
        if (interfaces[index] != (Type) null)
          typeArray[num++] = interfaces[index];
      }
      return typeArray;
    }

    /// <summary>返回表示指定的公共事件的 <see cref="T:System.Reflection.EventInfo" /> 对象。</summary>
    /// <returns>如找到，则为表示由当前 <see cref="T:System.Type" /> 声明或继承的指定公共事件的对象；否则为 null。</returns>
    /// <param name="name">该字符串包含事件名称，该事件是由当前 <see cref="T:System.Type" /> 声明或继承的。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public EventInfo GetEvent(string name)
    {
      return this.GetEvent(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    /// <summary>当在派生类中重写时，使用指定绑定约束，返回表示指定事件的 <see cref="T:System.Reflection.EventInfo" /> 对象。</summary>
    /// <returns>如找到，则为表示由当前 <see cref="T:System.Type" /> 声明或继承的指定公共事件的对象；否则为 null。</returns>
    /// <param name="name">字符串包含由当前的 <see cref="T:System.Type" /> 声明或继承的事件的名称。</param>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 - 零，以返回 null。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract EventInfo GetEvent(string name, BindingFlags bindingAttr);

    /// <summary>返回由当前 <see cref="T:System.Type" /> 声明或继承的所有公共事件。</summary>
    /// <returns>表示由当前 <see cref="T:System.Reflection.EventInfo" /> 声明或继承的所有公共事件的 <see cref="T:System.Type" /> 对象数组。- 或 - 如果当前 <see cref="T:System.Reflection.EventInfo" /> 没有默认成员，则为 <see cref="T:System.Type" /> 类型的空数组。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual EventInfo[] GetEvents()
    {
      return this.GetEvents(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    /// <summary>当在派生类中重写时，使用指定绑定约束，搜索由当前 <see cref="T:System.Type" /> 声明或继承的事件。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.EventInfo" /> 对象的数组，表示当前 <see cref="T:System.Type" /> 所声明或继承的与指定绑定约束匹配的所有事件。- 或 - 如果当前 <see cref="T:System.Reflection.EventInfo" /> 没有事件，或者如果没有一个事件匹配绑定约束，则为 <see cref="T:System.Type" /> 类型的空数组。</returns>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 - 零，以返回 null。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract EventInfo[] GetEvents(BindingFlags bindingAttr);

    /// <summary>使用指定的绑定约束，搜索参数与指定的参数类型及修饰符匹配的指定属性。</summary>
    /// <returns>表示符合指定要求的属性的对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的属性名的字符串。</param>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 - 零，以返回 null。</param>
    /// <param name="binder">一个对象，该对象定义一组属性并启用绑定，而绑定可能涉及选择重载方法、强制参数类型和通过反射调用成员。- 或 - 要使用 Nothing 的空引用（在 Visual Basic 中为 <see cref="P:System.Type.DefaultBinder" />）。</param>
    /// <param name="returnType">属性的返回类型。</param>
    /// <param name="types">一个 <see cref="T:System.Type" /> 对象数组，表示要获取的索引属性的参数的数目、顺序和类型。- 或 - 获取未被索引的属性的 <see cref="T:System.Type" /> 类型的空数组（即 Type[] types = new Type[0]）。</param>
    /// <param name="modifiers">
    /// <see cref="T:System.Reflection.ParameterModifier" /> 对象的数组，表示与 <paramref name="types" /> 数组中的相应元素关联的特性。默认的联编程序不处理此参数。</param>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">多个属性找到具有指定名称和匹配指定的绑定约束。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。- 或 - <paramref name="types" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="types" /> 是多维的。- 或 - <paramref name="modifiers" /> 是多维的。- 或 - <paramref name="types" /> 和 <paramref name="modifiers" /> 不具有相同的长度。</exception>
    /// <exception cref="T:System.NullReferenceException">元素的 <paramref name="types" /> 是 null。</exception>
    /// <filterpriority>2</filterpriority>
    public PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (types == null)
        throw new ArgumentNullException("types");
      return this.GetPropertyImpl(name, bindingAttr, binder, returnType, types, modifiers);
    }

    /// <summary>搜索其参数与指定参数类型及修饰符匹配的指定公共属性。</summary>
    /// <returns>表示符合指定要求的公共属性的对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的公共属性名的字符串。</param>
    /// <param name="returnType">属性的返回类型。</param>
    /// <param name="types">一个 <see cref="T:System.Type" /> 对象数组，表示要获取的索引属性的参数的数目、顺序和类型。- 或 - 获取未被索引的属性的 <see cref="T:System.Type" /> 类型的空数组（即 Type[] types = new Type[0]）。</param>
    /// <param name="modifiers">
    /// <see cref="T:System.Reflection.ParameterModifier" /> 对象的数组，表示与 <paramref name="types" /> 数组中的相应元素关联的特性。默认的联编程序不处理此参数。</param>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">多个属性找到具有指定名称和匹配指定的参数类型和修饰符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。- 或 - <paramref name="types" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="types" /> 是多维的。- 或 - <paramref name="modifiers" /> 是多维的。- 或 - <paramref name="types" /> 和 <paramref name="modifiers" /> 不具有相同的长度。</exception>
    /// <exception cref="T:System.NullReferenceException">元素的 <paramref name="types" /> 是 null。</exception>
    /// <filterpriority>2</filterpriority>
    public PropertyInfo GetProperty(string name, Type returnType, Type[] types, ParameterModifier[] modifiers)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (types == null)
        throw new ArgumentNullException("types");
      return this.GetPropertyImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) null, returnType, types, modifiers);
    }

    /// <summary>使用指定的绑定约束搜索指定属性。</summary>
    /// <returns>表示符合指定要求的属性的对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的属性名的字符串。</param>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 - 零，以返回 null。</param>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">多个属性找到具有指定名称和匹配指定的绑定约束。请参阅“备注”。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public PropertyInfo GetProperty(string name, BindingFlags bindingAttr)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      return this.GetPropertyImpl(name, bindingAttr, (Binder) null, (Type) null, (Type[]) null, (ParameterModifier[]) null);
    }

    /// <summary>搜索其参数与指定参数类型匹配的指定公共属性。</summary>
    /// <returns>表示其参数与指定参数类型匹配的公共属性的对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的公共属性名的字符串。</param>
    /// <param name="returnType">属性的返回类型。</param>
    /// <param name="types">一个 <see cref="T:System.Type" /> 对象数组，表示要获取的索引属性的参数的数目、顺序和类型。- 或 - 获取未被索引的属性的 <see cref="T:System.Type" /> 类型的空数组（即 Type[] types = new Type[0]）。</param>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">多个属性找到具有指定名称和匹配指定的参数类型。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。- 或 - <paramref name="types" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="types" /> 是多维的。</exception>
    /// <exception cref="T:System.NullReferenceException">元素的 <paramref name="types" /> 是 null。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public PropertyInfo GetProperty(string name, Type returnType, Type[] types)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (types == null)
        throw new ArgumentNullException("types");
      return this.GetPropertyImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) null, returnType, types, (ParameterModifier[]) null);
    }

    /// <summary>搜索其参数与指定参数类型匹配的指定公共属性。</summary>
    /// <returns>表示其参数与指定参数类型匹配的公共属性的对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的公共属性名的字符串。</param>
    /// <param name="types">一个 <see cref="T:System.Type" /> 对象数组，表示要获取的索引属性的参数的数目、顺序和类型。- 或 - 获取未被索引的属性的 <see cref="T:System.Type" /> 类型的空数组（即 Type[] types = new Type[0]）。</param>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">多个属性找到具有指定名称和匹配指定的参数类型。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。- 或 - <paramref name="types" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="types" /> 是多维的。</exception>
    /// <exception cref="T:System.NullReferenceException">元素的 <paramref name="types" /> 是 null。</exception>
    /// <filterpriority>2</filterpriority>
    public PropertyInfo GetProperty(string name, Type[] types)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (types == null)
        throw new ArgumentNullException("types");
      return this.GetPropertyImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) null, (Type) null, types, (ParameterModifier[]) null);
    }

    /// <summary>搜索具有指定名称和返回类型的公共属性。</summary>
    /// <returns>表示具有指定名称的公共属性的对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的公共属性名的字符串。</param>
    /// <param name="returnType">属性的返回类型。</param>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">具有指定名称找到多个属性。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 是 null, ，或 <paramref name="returnType" /> 是 null。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public PropertyInfo GetProperty(string name, Type returnType)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (returnType == (Type) null)
        throw new ArgumentNullException("returnType");
      return this.GetPropertyImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) null, returnType, (Type[]) null, (ParameterModifier[]) null);
    }

    internal PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Type returnType)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (returnType == (Type) null)
        throw new ArgumentNullException("returnType");
      return this.GetPropertyImpl(name, bindingAttr, (Binder) null, returnType, (Type[]) null, (ParameterModifier[]) null);
    }

    /// <summary>搜索具有指定名称的公共属性。</summary>
    /// <returns>表示具有指定名称的公共属性的对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的公共属性名的字符串。</param>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">具有指定名称找到多个属性。请参阅“备注”。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public PropertyInfo GetProperty(string name)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      return this.GetPropertyImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) null, (Type) null, (Type[]) null, (ParameterModifier[]) null);
    }

    /// <summary>当在派生类中重写时，使用指定的绑定约束搜索其参数与指定的参数类型和修饰符匹配的指定属性。</summary>
    /// <returns>表示符合指定要求的属性的对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的属性名的字符串。</param>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 - 零，以返回 null。</param>
    /// <param name="binder">一个对象，该对象定义一组属性并启用绑定，而绑定可能涉及选择重载成员、强制参数类型和通过反射调用成员。- 或 - 要使用 Nothing 的空引用（在 Visual Basic 中为 <see cref="P:System.Type.DefaultBinder" />）。</param>
    /// <param name="returnType">属性的返回类型。</param>
    /// <param name="types">一个 <see cref="T:System.Type" /> 对象数组，表示要获取的索引属性的参数的数目、顺序和类型。- 或 - 获取未被索引的属性的 <see cref="T:System.Type" /> 类型的空数组（即 Type[] types = new Type[0]）。</param>
    /// <param name="modifiers">
    /// <see cref="T:System.Reflection.ParameterModifier" /> 对象的数组，表示与 <paramref name="types" /> 数组中的相应元素关联的特性。默认的联编程序不处理此参数。</param>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">多个属性找到具有指定名称和匹配指定的绑定约束。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。- 或 - <paramref name="types" /> 为 null。- 或 - 中的元素之一 <paramref name="types" /> 是 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="types" /> 是多维的。- 或 - <paramref name="modifiers" /> 是多维的。- 或 - <paramref name="types" /> 和 <paramref name="modifiers" /> 不具有相同的长度。</exception>
    /// <exception cref="T:System.NotSupportedException">当前的类型是 <see cref="T:System.Reflection.Emit.TypeBuilder" />, ，<see cref="T:System.Reflection.Emit.EnumBuilder" />, ，或 <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />。</exception>
    protected abstract PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

    /// <summary>当在派生类中重写时，使用指定绑定约束，搜索当前 <see cref="T:System.Type" /> 的属性。</summary>
    /// <returns>表示当前 <see cref="T:System.Reflection.PropertyInfo" /> 的匹配指定绑定约束的所有属性的 <see cref="T:System.Type" /> 对象数组。- 或 - 如果当前 <see cref="T:System.Reflection.PropertyInfo" /> 没有属性，或者如果没有一个属性匹配绑定约束，则为 <see cref="T:System.Type" /> 类型的空数组。</returns>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 - 零，以返回 null。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract PropertyInfo[] GetProperties(BindingFlags bindingAttr);

    /// <summary>返回为当前 <see cref="T:System.Type" /> 的所有公共属性。</summary>
    /// <returns>表示当前 <see cref="T:System.Reflection.PropertyInfo" /> 的所有公共属性的 <see cref="T:System.Type" /> 对象数组。- 或 - 如果当前 <see cref="T:System.Reflection.PropertyInfo" /> 没有公共属性，则为 <see cref="T:System.Type" /> 类型的空数组。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public PropertyInfo[] GetProperties()
    {
      return this.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    /// <summary>返回嵌套在当前的 <see cref="T:System.Type" /> 中的公共类型。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 对象的数组，这些对象表示嵌套在当前 <see cref="T:System.Type" /> 中的公共类型（搜索是非递归的）；如果当前的 <see cref="T:System.Type" /> 中没有嵌套公共类型，则为 <see cref="T:System.Type" /> 类型的空数组。</returns>
    /// <filterpriority>2</filterpriority>
    public Type[] GetNestedTypes()
    {
      return this.GetNestedTypes(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    /// <summary>当在派生类中重写时，使用指定绑定约束搜索嵌套在当前 <see cref="T:System.Type" /> 中的类型。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 对象数组，这些对象表示嵌套在当前 <see cref="T:System.Type" /> 中的所有与指定的绑定约束匹配的类型（搜索是非递归的）；如果没有找到与绑定约束匹配的嵌套类型，则为 <see cref="T:System.Type" /> 类型的空数组。</returns>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 - 零，以返回 null。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract Type[] GetNestedTypes(BindingFlags bindingAttr);

    /// <summary>搜索具有指定名称的公共嵌套类型。</summary>
    /// <returns>如找到，则为表示具有指定名称的公共嵌套类型的对象；否则为 null。</returns>
    /// <param name="name">包含要获取的嵌套类型的名称的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    public Type GetNestedType(string name)
    {
      return this.GetNestedType(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    /// <summary>当在派生类中重写时，使用指定绑定约束搜索指定嵌套类型。</summary>
    /// <returns>表示符合指定要求的嵌套类型的对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的嵌套类型的名称的字符串。</param>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 - 零，以返回 null。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract Type GetNestedType(string name, BindingFlags bindingAttr);

    /// <summary>搜索具有指定名称的公共成员。</summary>
    /// <returns>一个表示具有指定名称的公共成员的 <see cref="T:System.Reflection.MemberInfo" /> 对象数组（如果找到的话）；否则为空数组。</returns>
    /// <param name="name">包含要获取的公共成员名称的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public MemberInfo[] GetMember(string name)
    {
      return this.GetMember(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    /// <summary>使用指定绑定约束搜索指定成员。</summary>
    /// <returns>一个表示具有指定名称的公共成员的 <see cref="T:System.Reflection.MemberInfo" /> 对象数组（如果找到的话）；否则为空数组。</returns>
    /// <param name="name">包含要获取的成员的名称的字符串。</param>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 - 零，返回空数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual MemberInfo[] GetMember(string name, BindingFlags bindingAttr)
    {
      return this.GetMember(name, MemberTypes.All, bindingAttr);
    }

    /// <summary>使用指定绑定约束搜索指定成员类型的指定成员。</summary>
    /// <returns>一个表示具有指定名称的公共成员的 <see cref="T:System.Reflection.MemberInfo" /> 对象数组（如果找到的话）；否则为空数组。</returns>
    /// <param name="name">包含要获取的成员的名称的字符串。</param>
    /// <param name="type">要搜索的值。</param>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 - 零，返回空数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">在派生的类必须提供实现。</exception>
    /// <filterpriority>2</filterpriority>
    public virtual MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    /// <summary>返回为当前 <see cref="T:System.Type" /> 的所有公共成员。</summary>
    /// <returns>表示当前 <see cref="T:System.Reflection.MemberInfo" /> 的所有公共成员的 <see cref="T:System.Type" /> 对象数组。- 或 - 如果当前 <see cref="T:System.Reflection.MemberInfo" /> 没有公共成员，则为 <see cref="T:System.Type" /> 类型的空数组。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public MemberInfo[] GetMembers()
    {
      return this.GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    /// <summary>当在派生类中重写时，使用指定绑定约束，搜索为当前 <see cref="T:System.Type" /> 定义的成员。</summary>
    /// <returns>表示为当前 <see cref="T:System.Reflection.MemberInfo" /> 定义的匹配指定绑定约束的所有成员的 <see cref="T:System.Type" /> 对象数组。- 或 - 如果没有为当前 <see cref="T:System.Reflection.MemberInfo" /> 定义的成员，或者如果没有一个定义的成员匹配绑定约束，则为 <see cref="T:System.Type" /> 类型的空数组。</returns>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 - 如果为零 (<see cref="F:System.Reflection.BindingFlags.Default" />)，则返回空数组。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract MemberInfo[] GetMembers(BindingFlags bindingAttr);

    /// <summary>搜索为设置了 <see cref="T:System.Type" /> 的当前 <see cref="T:System.Reflection.DefaultMemberAttribute" /> 定义的成员。</summary>
    /// <returns>表示当前 <see cref="T:System.Reflection.MemberInfo" /> 的所有默认成员的 <see cref="T:System.Type" /> 对象数组。- 或 - 如果当前 <see cref="T:System.Reflection.MemberInfo" /> 没有默认成员，则为 <see cref="T:System.Type" /> 类型的空数组。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual MemberInfo[] GetDefaultMembers()
    {
      throw new NotImplementedException();
    }

    /// <summary>返回指定成员类型的 <see cref="T:System.Reflection.MemberInfo" /> 对象的筛选数组。</summary>
    /// <returns>指定成员类型的 <see cref="T:System.Reflection.MemberInfo" /> 对象的筛选数组。- 或 - 如果当前 <see cref="T:System.Reflection.MemberInfo" /> 没有匹配筛选判据的 <see cref="T:System.Type" /> 类型成员，则为 <paramref name="memberType" /> 类型的空数组。</returns>
    /// <param name="memberType">指示要搜索的成员类型的对象。</param>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 - 零，以返回 null。</param>
    /// <param name="filter">执行比较的委托，如果当前被检查的成员匹配 true，则返回 <paramref name="filterCriteria" />；否则返回 false。可以使用该类提供的 FilterAttribute、FilterName 和 FilterNameIgnoreCase 委托。第一个委托使用 FieldAttributes、MethodAttributes 和 MethodImplAttributes 的字段作为搜索判据，另两个委托使用 String 对象作为搜索判据。</param>
    /// <param name="filterCriteria">确定成员是否在 MemberInfo 对象数组中返回的搜索判据。FieldAttributes、MethodAttributes 和 MethodImplAttributes 的字段可以和该类提供的 FilterAttribute 委托一起使用。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="filter" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    public virtual MemberInfo[] FindMembers(MemberTypes memberType, BindingFlags bindingAttr, MemberFilter filter, object filterCriteria)
    {
      MethodInfo[] methodInfoArray = (MethodInfo[]) null;
      ConstructorInfo[] constructorInfoArray = (ConstructorInfo[]) null;
      FieldInfo[] fieldInfoArray = (FieldInfo[]) null;
      PropertyInfo[] propertyInfoArray = (PropertyInfo[]) null;
      EventInfo[] eventInfoArray = (EventInfo[]) null;
      Type[] typeArray = (Type[]) null;
      int length = 0;
      if ((memberType & MemberTypes.Method) != (MemberTypes) 0)
      {
        methodInfoArray = this.GetMethods(bindingAttr);
        if (filter != null)
        {
          for (int index = 0; index < methodInfoArray.Length; ++index)
          {
            if (!filter((MemberInfo) methodInfoArray[index], filterCriteria))
              methodInfoArray[index] = (MethodInfo) null;
            else
              ++length;
          }
        }
        else
          length += methodInfoArray.Length;
      }
      if ((memberType & MemberTypes.Constructor) != (MemberTypes) 0)
      {
        constructorInfoArray = this.GetConstructors(bindingAttr);
        if (filter != null)
        {
          for (int index = 0; index < constructorInfoArray.Length; ++index)
          {
            if (!filter((MemberInfo) constructorInfoArray[index], filterCriteria))
              constructorInfoArray[index] = (ConstructorInfo) null;
            else
              ++length;
          }
        }
        else
          length += constructorInfoArray.Length;
      }
      if ((memberType & MemberTypes.Field) != (MemberTypes) 0)
      {
        fieldInfoArray = this.GetFields(bindingAttr);
        if (filter != null)
        {
          for (int index = 0; index < fieldInfoArray.Length; ++index)
          {
            if (!filter((MemberInfo) fieldInfoArray[index], filterCriteria))
              fieldInfoArray[index] = (FieldInfo) null;
            else
              ++length;
          }
        }
        else
          length += fieldInfoArray.Length;
      }
      if ((memberType & MemberTypes.Property) != (MemberTypes) 0)
      {
        propertyInfoArray = this.GetProperties(bindingAttr);
        if (filter != null)
        {
          for (int index = 0; index < propertyInfoArray.Length; ++index)
          {
            if (!filter((MemberInfo) propertyInfoArray[index], filterCriteria))
              propertyInfoArray[index] = (PropertyInfo) null;
            else
              ++length;
          }
        }
        else
          length += propertyInfoArray.Length;
      }
      if ((memberType & MemberTypes.Event) != (MemberTypes) 0)
      {
        eventInfoArray = this.GetEvents(bindingAttr);
        if (filter != null)
        {
          for (int index = 0; index < eventInfoArray.Length; ++index)
          {
            if (!filter((MemberInfo) eventInfoArray[index], filterCriteria))
              eventInfoArray[index] = (EventInfo) null;
            else
              ++length;
          }
        }
        else
          length += eventInfoArray.Length;
      }
      if ((memberType & MemberTypes.NestedType) != (MemberTypes) 0)
      {
        typeArray = this.GetNestedTypes(bindingAttr);
        if (filter != null)
        {
          for (int index = 0; index < typeArray.Length; ++index)
          {
            if (!filter((MemberInfo) typeArray[index], filterCriteria))
              typeArray[index] = (Type) null;
            else
              ++length;
          }
        }
        else
          length += typeArray.Length;
      }
      MemberInfo[] memberInfoArray = new MemberInfo[length];
      int num = 0;
      if (methodInfoArray != null)
      {
        for (int index = 0; index < methodInfoArray.Length; ++index)
        {
          if (methodInfoArray[index] != (MethodInfo) null)
            memberInfoArray[num++] = (MemberInfo) methodInfoArray[index];
        }
      }
      if (constructorInfoArray != null)
      {
        for (int index = 0; index < constructorInfoArray.Length; ++index)
        {
          if (constructorInfoArray[index] != (ConstructorInfo) null)
            memberInfoArray[num++] = (MemberInfo) constructorInfoArray[index];
        }
      }
      if (fieldInfoArray != null)
      {
        for (int index = 0; index < fieldInfoArray.Length; ++index)
        {
          if (fieldInfoArray[index] != (FieldInfo) null)
            memberInfoArray[num++] = (MemberInfo) fieldInfoArray[index];
        }
      }
      if (propertyInfoArray != null)
      {
        for (int index = 0; index < propertyInfoArray.Length; ++index)
        {
          if (propertyInfoArray[index] != (PropertyInfo) null)
            memberInfoArray[num++] = (MemberInfo) propertyInfoArray[index];
        }
      }
      if (eventInfoArray != null)
      {
        for (int index = 0; index < eventInfoArray.Length; ++index)
        {
          if (eventInfoArray[index] != (EventInfo) null)
            memberInfoArray[num++] = (MemberInfo) eventInfoArray[index];
        }
      }
      if (typeArray != null)
      {
        for (int index = 0; index < typeArray.Length; ++index)
        {
          if (typeArray[index] != (Type) null)
            memberInfoArray[num++] = (MemberInfo) typeArray[index];
        }
      }
      return memberInfoArray;
    }

    /// <summary>返回表示当前泛型类型参数约束的 <see cref="T:System.Type" /> 对象的数组。</summary>
    /// <returns>表示当前泛型类型参数上的约束的 <see cref="T:System.Type" /> 对象的数组。</returns>
    /// <exception cref="T:System.InvalidOperationException">当前 <see cref="T:System.Type" /> 对象不是泛型类型参数。也就是说， <see cref="P:System.Type.IsGenericParameter" /> 属性将返回 false。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public virtual Type[] GetGenericParameterConstraints()
    {
      if (!this.IsGenericParameter)
        throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericParameter"));
      throw new InvalidOperationException();
    }

    /// <summary>实现 <see cref="P:System.Type.IsValueType" /> 属性并确定 <see cref="T:System.Type" /> 是否是值类型；即，它不是值类或接口。</summary>
    /// <returns>如果 true 是值类型，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    [__DynamicallyInvokable]
    protected virtual bool IsValueTypeImpl()
    {
      return this.IsSubclassOf((Type) RuntimeType.ValueType);
    }

    /// <summary>当在派生类中重写时，实现 <see cref="P:System.Type.Attributes" /> 属性 (Property)，并获取指示与 <see cref="T:System.Type" /> 关联的属性 (Attribute) 的位屏蔽。</summary>
    /// <returns>表示 <see cref="T:System.Reflection.TypeAttributes" /> 的属性集的 <see cref="T:System.Type" /> 对象。</returns>
    protected abstract TypeAttributes GetAttributeFlagsImpl();

    /// <summary>在派生类中重写时，实现 <see cref="P:System.Type.IsArray" /> 属性并确定 <see cref="T:System.Type" /> 是否为数组。</summary>
    /// <returns>如果 true 是数组，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    [__DynamicallyInvokable]
    protected abstract bool IsArrayImpl();

    /// <summary>在派生类中重写时，实现 <see cref="P:System.Type.IsByRef" /> 属性并确定<see cref="T:System.Type" /> 是否通过引用传递。</summary>
    /// <returns>如果 true 按引用传递，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    [__DynamicallyInvokable]
    protected abstract bool IsByRefImpl();

    /// <summary>在派生类中重写时，实现 <see cref="P:System.Type.IsPointer" /> 属性并确定 <see cref="T:System.Type" /> 是否为指针。</summary>
    /// <returns>如果 true 是指针，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    [__DynamicallyInvokable]
    protected abstract bool IsPointerImpl();

    /// <summary>在派生类中重写时，实现 <see cref="P:System.Type.IsPrimitive" /> 属性并确定 <see cref="T:System.Type" /> 是否为基元类型之一。</summary>
    /// <returns>如果 true 为基元类型之一，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    [__DynamicallyInvokable]
    protected abstract bool IsPrimitiveImpl();

    /// <summary>当在派生类中重写时，实现 <see cref="P:System.Type.IsCOMObject" /> 属性并确定 <see cref="T:System.Type" /> 是否为 COM 对象。</summary>
    /// <returns>如果 true 为 COM 对象，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    protected abstract bool IsCOMObjectImpl();

    internal virtual bool IsWindowsRuntimeObjectImpl()
    {
      throw new NotImplementedException();
    }

    internal virtual bool IsExportedToWindowsRuntimeImpl()
    {
      throw new NotImplementedException();
    }

    /// <summary>替代由当前泛型类型定义的类型参数组成的类型数组的元素，并返回表示结果构造类型的 <see cref="T:System.Type" /> 对象。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 表示的构造类型通过以下方式形成：用 <paramref name="typeArguments" /> 的元素取代当前泛型类型的类型参数。</returns>
    /// <param name="typeArguments">将代替当前泛型类型的类型参数的类型数组。</param>
    /// <exception cref="T:System.InvalidOperationException">当前的类型不表示泛型类型定义。也就是说， <see cref="P:System.Type.IsGenericTypeDefinition" /> 返回 false。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeArguments" /> 为 null。- 或 - 任何元素 <paramref name="typeArguments" /> 是 null。</exception>
    /// <exception cref="T:System.ArgumentException">中的元素数 <paramref name="typeArguments" /> 不是当前的泛型类型定义中的类型参数的编号相同。- 或 - 任何元素 <paramref name="typeArguments" /> 不满足当前的泛型类型的相应类型参数指定的约束。- 或 - <paramref name="typeArguments" /> 包含的元素，是指针类型 (<see cref="P:System.Type.IsPointer" /> 返回 true），通过 ref 类型 (<see cref="P:System.Type.IsByRef" /> 返回 true)，或 <see cref="T:System.Void" />。</exception>
    /// <exception cref="T:System.NotSupportedException">在基类中不支持调用的方法。派生类必须提供一个实现。</exception>
    [__DynamicallyInvokable]
    public virtual Type MakeGenericType(params Type[] typeArguments)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    /// <summary>实现 <see cref="P:System.Type.IsContextful" /> 属性并确定 <see cref="T:System.Type" /> 在上下文中是否可以被承载。</summary>
    /// <returns>如果 true 能够在某个上下文中承载，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    protected virtual bool IsContextfulImpl()
    {
      return typeof (ContextBoundObject).IsAssignableFrom(this);
    }

    /// <summary>实现 <see cref="P:System.Type.IsMarshalByRef" /> 属性并确定 <see cref="T:System.Type" /> 是否按引用来进行封送。</summary>
    /// <returns>如果 true 是由引用封送的，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    protected virtual bool IsMarshalByRefImpl()
    {
      return typeof (MarshalByRefObject).IsAssignableFrom(this);
    }

    internal virtual bool HasProxyAttributeImpl()
    {
      return false;
    }

    /// <summary>当在派生类中重写时，返回当前数组、指针或引用类型包含的或引用的对象的 <see cref="T:System.Type" />。</summary>
    /// <returns>当前数组、指针或引用类型包含或引用的对象的 <see cref="T:System.Type" />；如果当前 null 不是数组或指针，不是按引用传递，或者表示泛型类型或泛型方法的定义中的泛型类型或类型参数，则为 <see cref="T:System.Type" />。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public abstract Type GetElementType();

    /// <summary>返回表示封闭式泛型类型的类型参数或泛型类型定义的类型参数的 <see cref="T:System.Type" /> 对象的数组。</summary>
    /// <returns>表示泛型类型的类型实参的 <see cref="T:System.Type" /> 对象的数组。如果当前类型不是泛型类型，则返回一个空数组。</returns>
    /// <exception cref="T:System.NotSupportedException">在基类中不支持调用的方法。派生类必须提供一个实现。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual Type[] GetGenericArguments()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    /// <summary>返回一个表示可用于构造当前泛型类型的泛型类型定义的 <see cref="T:System.Type" /> 对象。</summary>
    /// <returns>表示可用于构造当前类型的泛型类型的 <see cref="T:System.Type" /> 对象。</returns>
    /// <exception cref="T:System.InvalidOperationException">当前的类型不是泛型类型。也就是说， <see cref="P:System.Type.IsGenericType" /> 返回 false。</exception>
    /// <exception cref="T:System.NotSupportedException">在基类中不支持调用的方法。派生类必须提供一个实现。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual Type GetGenericTypeDefinition()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    /// <summary>当在派生类中重写时，实现 <see cref="P:System.Type.HasElementType" /> 属性，确定当前 <see cref="T:System.Type" /> 是否包含另一类型或对其引用；即，当前 <see cref="T:System.Type" /> 是否是数组、指针或由引用传递。</summary>
    /// <returns>如果 true 为数组、指针或按引用传递，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    [__DynamicallyInvokable]
    protected abstract bool HasElementTypeImpl();

    internal Type GetRootElementType()
    {
      Type type = this;
      while (type.HasElementType)
        type = type.GetElementType();
      return type;
    }

    /// <summary>返回当前枚举类型中各个成员的名称。</summary>
    /// <returns>一个数组，其中包含枚举中各个成员的名称。</returns>
    /// <exception cref="T:System.ArgumentException">当前的类型不是一个枚举。</exception>
    public virtual string[] GetEnumNames()
    {
      if (!this.IsEnum)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
      string[] enumNames;
      Array enumValues;
      this.GetEnumData(out enumNames, out enumValues);
      return enumNames;
    }

    /// <summary>返回当前枚举类型中各个常数的值组成的数组。</summary>
    /// <returns>包含值的数组。该数组的元素按枚举常量的二进制值（无符号值）排序。</returns>
    /// <exception cref="T:System.ArgumentException">当前的类型不是一个枚举。</exception>
    public virtual Array GetEnumValues()
    {
      if (!this.IsEnum)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
      throw new NotImplementedException();
    }

    private Array GetEnumRawConstantValues()
    {
      string[] enumNames;
      Array enumValues;
      this.GetEnumData(out enumNames, out enumValues);
      return enumValues;
    }

    private void GetEnumData(out string[] enumNames, out Array enumValues)
    {
      FieldInfo[] fields = this.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      object[] objArray = new object[fields.Length];
      string[] strArray = new string[fields.Length];
      for (int index = 0; index < fields.Length; ++index)
      {
        strArray[index] = fields[index].Name;
        objArray[index] = fields[index].GetRawConstantValue();
      }
      IComparer comparer = (IComparer) Comparer.Default;
      for (int index1 = 1; index1 < objArray.Length; ++index1)
      {
        int index2 = index1;
        string str = strArray[index1];
        object y = objArray[index1];
        bool flag = false;
        while (comparer.Compare(objArray[index2 - 1], y) > 0)
        {
          strArray[index2] = strArray[index2 - 1];
          objArray[index2] = objArray[index2 - 1];
          --index2;
          flag = true;
          if (index2 == 0)
            break;
        }
        if (flag)
        {
          strArray[index2] = str;
          objArray[index2] = y;
        }
      }
      enumNames = strArray;
      enumValues = (Array) objArray;
    }

    /// <summary>返回当前枚举类型的基础类型。</summary>
    /// <returns>当前枚举的基础类型。</returns>
    /// <exception cref="T:System.ArgumentException">当前的类型不是一个枚举。- 或 -枚举类型无效，因为它包含多个实例字段。</exception>
    public virtual Type GetEnumUnderlyingType()
    {
      if (!this.IsEnum)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
      FieldInfo[] fields = this.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      if (fields == null || fields.Length != 1)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidEnum"), "enumType");
      return fields[0].FieldType;
    }

    /// <summary>返回一个值，该值指示当前的枚举类型中是否存在指定的值。</summary>
    /// <returns>如果指定的值是当前枚举类型的成员，则为 true；否则为 false。</returns>
    /// <param name="value">要测试的值。</param>
    /// <exception cref="T:System.ArgumentException">当前的类型不是一个枚举。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="value" /> 是不能为一个枚举的基础类型的类型。</exception>
    public virtual bool IsEnumDefined(object value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (!this.IsEnum)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
      Type t = value.GetType();
      if (t.IsEnum)
      {
        if (!t.IsEquivalentTo(this))
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumAndObjectMustBeSameType", (object) t.ToString(), (object) this.ToString()));
        t = t.GetEnumUnderlyingType();
      }
      if (t == typeof (string))
        return Array.IndexOf<object>((object[]) this.GetEnumNames(), value) >= 0;
      if (Type.IsIntegerType(t))
      {
        Type enumUnderlyingType = this.GetEnumUnderlyingType();
        if (enumUnderlyingType.GetTypeCodeImpl() != t.GetTypeCodeImpl())
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumUnderlyingTypeAndObjectMustBeSameType", (object) t.ToString(), (object) enumUnderlyingType.ToString()));
        return Type.BinarySearch(this.GetEnumRawConstantValues(), value) >= 0;
      }
      if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumUnderlyingTypeAndObjectMustBeSameType", (object) t.ToString(), (object) this.GetEnumUnderlyingType()));
      throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_UnknownEnumType"));
    }

    /// <summary>返回当前枚举类型中具有指定值的常数的名称。</summary>
    /// <returns>当前枚举类型中具有指定值的成员的名称；如果未找到这样的常数，则为 null。</returns>
    /// <param name="value">要检索其名称的值。</param>
    /// <exception cref="T:System.ArgumentException">当前的类型不是一个枚举。- 或 -<paramref name="value" /> 既不属于当前类型也不具有与当前类型的基础类型相同。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> 为 null。</exception>
    public virtual string GetEnumName(object value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (!this.IsEnum)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
      Type type = value.GetType();
      if (!type.IsEnum && !Type.IsIntegerType(type))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnumBaseTypeOrEnum"), "value");
      int index = Type.BinarySearch(this.GetEnumRawConstantValues(), value);
      if (index >= 0)
        return this.GetEnumNames()[index];
      return (string) null;
    }

    private static int BinarySearch(Array array, object value)
    {
      ulong[] array1 = new ulong[array.Length];
      for (int index = 0; index < array.Length; ++index)
        array1[index] = Enum.ToUInt64(array.GetValue(index));
      ulong uint64 = Enum.ToUInt64(value);
      return Array.BinarySearch<ulong>(array1, uint64);
    }

    internal static bool IsIntegerType(Type t)
    {
      if (!(t == typeof (int)) && !(t == typeof (short)) && (!(t == typeof (ushort)) && !(t == typeof (byte))) && (!(t == typeof (sbyte)) && !(t == typeof (uint)) && (!(t == typeof (long)) && !(t == typeof (ulong)))) && !(t == typeof (char)))
        return t == typeof (bool);
      return true;
    }

    /// <summary>确定当前 <see cref="T:System.Type" /> 是否派生自指定的 <see cref="T:System.Type" />。</summary>
    /// <returns>如果当前 true 派生于 Type，则为 <paramref name="c" />；否则为 false。如果 false 和当前 <paramref name="c" /> 相等，此方法也返回 Type。</returns>
    /// <param name="c">要与当前类型进行比较的类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="c" /> 为 null。</exception>
    /// <filterpriority>2</filterpriority>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public virtual bool IsSubclassOf(Type c)
    {
      Type type = this;
      if (type == c)
        return false;
      for (; type != (Type) null; type = type.BaseType)
      {
        if (type == c)
          return true;
      }
      return false;
    }

    /// <summary>确定指定的对象是否是当前 <see cref="T:System.Type" /> 的实例。</summary>
    /// <returns>如果满足下列任一条件，则为 true：当前 Type 位于由 <paramref name="o" /> 表示的对象的继承层次结构中；当前 Type 是 <paramref name="o" /> 实现的接口。如果不属于其中任一种情况，<paramref name="o" /> 为 null，或者当前 Type 为开放式泛型类型（即 <see cref="P:System.Type.ContainsGenericParameters" /> 返回 true），则为 false。</returns>
    /// <param name="o">要与当前类型进行比较的对象。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual bool IsInstanceOfType(object o)
    {
      if (o == null)
        return false;
      return this.IsAssignableFrom(o.GetType());
    }

    /// <summary>确定指定类型的实例是否能分配给当前类型实例。</summary>
    /// <returns>如果满足下列任一条件，则为 true：  <paramref name="c" /> 且当前实例表示相同类型。<paramref name="c" /> 是从当前实例直接或间接派生的。当前实例是一个 <paramref name="c" /> 实现的接口。<paramref name="c" /> 是一个泛型类型参数，并且当前实例表示 <paramref name="c" /> 的约束之一。<paramref name="c" /> 表示一个值类型，并且当前实例表示表示 Nullable&lt;c&gt;（在 Visual Basic 中为 Nullable(Of c)）。如果不满足上述任何一个条件或者 false 为 <paramref name="c" />，则为 null。</returns>
    /// <param name="c">要与当前类型进行比较的类型。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual bool IsAssignableFrom(Type c)
    {
      if (c == (Type) null)
        return false;
      if (this == c)
        return true;
      RuntimeType runtimeType = this.UnderlyingSystemType as RuntimeType;
      if (runtimeType != (RuntimeType) null)
        return runtimeType.IsAssignableFrom(c);
      if (c.IsSubclassOf(this))
        return true;
      if (this.IsInterface)
        return c.ImplementInterface(this);
      if (!this.IsGenericParameter)
        return false;
      foreach (Type parameterConstraint in this.GetGenericParameterConstraints())
      {
        if (!parameterConstraint.IsAssignableFrom(c))
          return false;
      }
      return true;
    }

    /// <summary>确定两个 COM 类型是否具有相同的标识，以及是否符合类型等效的条件。</summary>
    /// <returns>如果 COM 类型等效，则为 true；否则为 false。如果一个类型位于为执行加载的程序集中，而另一个类型位于已加载到仅限反射上下文的程序集中，则此方法也返回 false。</returns>
    /// <param name="other">要测试是否与当前类型等效的 COM 类型。</param>
    public virtual bool IsEquivalentTo(Type other)
    {
      return this == other;
    }

    internal bool ImplementInterface(Type ifaceType)
    {
      for (Type type = this; type != (Type) null; type = type.BaseType)
      {
        Type[] interfaces = type.GetInterfaces();
        if (interfaces != null)
        {
          for (int index = 0; index < interfaces.Length; ++index)
          {
            if (interfaces[index] == ifaceType || interfaces[index] != (Type) null && interfaces[index].ImplementInterface(ifaceType))
              return true;
          }
        }
      }
      return false;
    }

    internal string FormatTypeName()
    {
      return this.FormatTypeName(false);
    }

    internal virtual string FormatTypeName(bool serialization)
    {
      throw new NotImplementedException();
    }

    /// <summary>返回表示当前 String 的名称的 Type。</summary>
    /// <returns>表示当前 <see cref="T:System.String" /> 的名称的 <see cref="T:System.Type" />。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return "Type: " + this.Name;
    }

    /// <summary>获取指定数组中对象的类型。</summary>
    /// <returns>表示 <see cref="T:System.Type" /> 中相应元素的类型的 <paramref name="args" /> 对象数组。</returns>
    /// <param name="args">要确定其类型的对象数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="args" /> 为 null。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">调用类初始值设定项，并且至少一个引发异常。</exception>
    /// <filterpriority>1</filterpriority>
    public static Type[] GetTypeArray(object[] args)
    {
      if (args == null)
        throw new ArgumentNullException("args");
      Type[] typeArray = new Type[args.Length];
      for (int index = 0; index < typeArray.Length; ++index)
      {
        if (args[index] == null)
          throw new ArgumentNullException();
        typeArray[index] = args[index].GetType();
      }
      return typeArray;
    }

    /// <summary>确定当前 <see cref="T:System.Type" /> 的基础系统类型是否与指定 <see cref="T:System.Object" /> 的基础系统类型相同。</summary>
    /// <returns>如果 true 的基础系统类型与当前 <paramref name="o" /> 的基础系统类型相同，则为 <see cref="T:System.Type" />；否则为 false。如果 false 参数指定的对象不是 <paramref name="o" />，此方法也会返回 Type。</returns>
    /// <param name="o">该对象，其基础系统类型将与当前 <see cref="T:System.Type" /> 的基础系统类型相比较。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override bool Equals(object o)
    {
      if (o == null)
        return false;
      return this.Equals(o as Type);
    }

    /// <summary>确定当前 <see cref="T:System.Type" /> 的基础系统类型是否与指定 <see cref="T:System.Type" /> 的基础系统类型相同。</summary>
    /// <returns>如果 true 的基础系统类型与当前 <paramref name="o" /> 的基础系统类型相同，则为 <see cref="T:System.Type" />；否则为 false。</returns>
    /// <param name="o">该对象，其基础系统类型将与当前 <see cref="T:System.Type" /> 的基础系统类型相比较。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public virtual bool Equals(Type o)
    {
      if (o == null)
        return false;
      return this.UnderlyingSystemType == o.UnderlyingSystemType;
    }

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>此实例的哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      Type underlyingSystemType = this.UnderlyingSystemType;
      if (underlyingSystemType != this)
        return underlyingSystemType.GetHashCode();
      return base.GetHashCode();
    }

    /// <summary>返回指定接口类型的接口映射。</summary>
    /// <returns>表示 <paramref name="interfaceType" /> 的接口映射的对象。</returns>
    /// <param name="interfaceType">要检索其映射的接口类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="interfaceType" /> 未实现由当前类型。- 或 -<paramref name="interfaceType" /> 参数不是指一个接口。- 或 -<paramref name="interfaceType" /> 是一个泛型接口和当前的类型是数组类型。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="interfaceType" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">当前 <see cref="T:System.Type" /> 都表示一个泛型类型参数 ； 也就是说， <see cref="P:System.Type.IsGenericParameter" /> 是 true。</exception>
    /// <exception cref="T:System.NotSupportedException">在基类中不支持调用的方法。派生类必须提供一个实现。</exception>
    /// <filterpriority>2</filterpriority>
    [ComVisible(true)]
    public virtual InterfaceMapping GetInterfaceMap(Type interfaceType)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    /// <summary>获取当前 <see cref="T:System.Type" />。</summary>
    /// <returns>当前的 <see cref="T:System.Type" />。</returns>
    /// <exception cref="T:System.Reflection.TargetInvocationException">类初始值设定项将调用，并且会引发异常。</exception>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public new Type GetType()
    {
      return base.GetType();
    }

    void _Type.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _Type.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _Type.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _Type.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
