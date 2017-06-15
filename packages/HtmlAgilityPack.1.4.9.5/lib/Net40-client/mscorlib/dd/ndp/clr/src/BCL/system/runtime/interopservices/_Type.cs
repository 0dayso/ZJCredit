// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices._Type
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
  /// <summary>向非托管代码公开 <see cref="T:System.Type" /> 类的公共成员。</summary>
  [Guid("BCA8B44D-AAD6-3A86-8AB7-03349F4F2DA2")]
  [CLSCompliant(false)]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [TypeLibImportClass(typeof (Type))]
  [ComVisible(true)]
  public interface _Type
  {
    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.MemberType" /> 属性的版本无关的访问。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.MemberTypes" /> 值，指示此成员是类型还是嵌套类型。</returns>
    MemberTypes MemberType { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MemberInfo.Name" /> 属性的版本无关的访问。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 的名称。</returns>
    string Name { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.DeclaringType" /> 属性的版本无关的访问。</summary>
    /// <returns>声明该成员的类的 <see cref="T:System.Type" /> 对象。如果该类型是嵌套类型，则该属性返回封闭类型。</returns>
    Type DeclaringType { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.ReflectedType" /> 属性的版本无关的访问。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 对象，通过它获取了该 <see cref="T:System.Reflection.MemberInfo" /> 对象。</returns>
    Type ReflectedType { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.GUID" /> 属性的版本无关的访问。</summary>
    /// <returns>与 <see cref="T:System.Type" /> 关联的 GUID。</returns>
    Guid GUID { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.Module" /> 属性的版本无关的访问。</summary>
    /// <returns>在其中定义当前 <see cref="T:System.Type" /> 的模块的名称。</returns>
    Module Module { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.Assembly" /> 属性的版本无关的访问。</summary>
    /// <returns>描述包含当前类型的程序集的 <see cref="T:System.Reflection.Assembly" /> 实例。</returns>
    Assembly Assembly { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.TypeHandle" /> 属性的版本无关的访问。</summary>
    /// <returns>当前 <see cref="T:System.Type" /> 的句柄。</returns>
    RuntimeTypeHandle TypeHandle { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.FullName" /> 属性的版本无关的访问。</summary>
    /// <returns>包含 <see cref="T:System.Type" /> 的完全限定名（包括 <see cref="T:System.Type" /> 的命名空间，但不包括程序集）的字符串。</returns>
    string FullName { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.Namespace" /> 属性的版本无关的访问。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 的命名空间。</returns>
    string Namespace { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.AssemblyQualifiedName" /> 属性的版本无关的访问。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 的程序集限定名，包括从中加载 <see cref="T:System.Type" /> 的程序集的名称。</returns>
    string AssemblyQualifiedName { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.BaseType" /> 属性的版本无关的访问。</summary>
    /// <returns>当前 <see cref="T:System.Type" /> 直接继承的 <see cref="T:System.Type" />；如果当前 Type 表示 <see cref="T:System.Object" /> 类，则为 null。</returns>
    Type BaseType { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.UnderlyingSystemType" /> 属性的版本无关的访问。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 的基础系统类型。</returns>
    Type UnderlyingSystemType { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.TypeInitializer" /> 属性的版本无关的访问。</summary>
    /// <returns>包含 <see cref="T:System.Type" /> 的类构造函数的名称的 <see cref="T:System.Reflection.ConstructorInfo" />。</returns>
    ConstructorInfo TypeInitializer { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.Attributes" /> 属性的版本无关的访问。</summary>
    /// <returns>表示 <see cref="T:System.Type" /> 的属性集的 <see cref="T:System.Reflection.TypeAttributes" /> 对象，除非 <see cref="T:System.Type" /> 表示泛型类型形参，在此情况下该值未指定。</returns>
    TypeAttributes Attributes { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsNotPublic" /> 属性的版本无关的访问。</summary>
    /// <returns>如果顶级 <see cref="T:System.Type" /> 不是声明为公共的，则为 true；否则为 false。</returns>
    bool IsNotPublic { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsPublic" /> 属性的版本无关的访问。</summary>
    /// <returns>如果顶级 <see cref="T:System.Type" /> 声明为公共的，则为 true；否则为 false。</returns>
    bool IsPublic { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsNestedPublic" /> 属性的版本无关的访问。</summary>
    /// <returns>如果类是嵌套的并且声明为公共的，则为 true；否则为 false。</returns>
    bool IsNestedPublic { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsNestedPrivate" /> 属性的版本无关的访问。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 是嵌套的并声明为私有的，则为 true；否则为 false。</returns>
    bool IsNestedPrivate { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsNestedFamily" /> 属性的版本无关的访问。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 是嵌套的并且仅在它自己的家族中可见，则为 true；否则为 false。</returns>
    bool IsNestedFamily { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsNestedAssembly" /> 属性的版本无关的访问。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 是嵌套的并且仅在它自己的程序集中可见，则为 true；否则为 false。</returns>
    bool IsNestedAssembly { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsNestedFamANDAssem" /> 属性的版本无关的访问。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 是嵌套的并且只对同时属于它自己的家族和它自己的程序集的类可见，则为 true；否则为 false。</returns>
    bool IsNestedFamANDAssem { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsNestedFamORAssem" /> 属性的版本无关的访问。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 是嵌套的并且只对属于它自己的家族或属于它自己的程序集的类可见，则为 true；否则为 false。</returns>
    bool IsNestedFamORAssem { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsAutoLayout" /> 属性的版本无关的访问。</summary>
    /// <returns>如果为 <see cref="T:System.Type" /> 选定了类布局属性 AutoLayout，则为 true；否则为 false。</returns>
    bool IsAutoLayout { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsLayoutSequential" /> 属性的版本无关的访问。</summary>
    /// <returns>如果为 <see cref="T:System.Type" /> 选定了类布局属性 SequentialLayout，则为 true；否则为 false。</returns>
    bool IsLayoutSequential { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsExplicitLayout" /> 属性的版本无关的访问。</summary>
    /// <returns>如果为 <see cref="T:System.Type" /> 选定了类布局属性 ExplicitLayout，则为 true；否则为 false。</returns>
    bool IsExplicitLayout { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsClass" /> 属性的版本无关的访问。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 是类，则为 true；否则为 false。</returns>
    bool IsClass { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsInterface" /> 属性的版本无关的访问。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 是接口，则为 true；否则为 false。</returns>
    bool IsInterface { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsValueType" /> 属性的版本无关的访问。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 是值类型，则为 true；否则为 false。</returns>
    bool IsValueType { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsAbstract" /> 属性的版本无关的访问。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 是抽象的，则为 true；否则为 false。</returns>
    bool IsAbstract { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsSealed" /> 属性的版本无关的访问。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 被声明为密封的，则为 true；否则为 false。</returns>
    bool IsSealed { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsEnum" /> 属性的版本无关的访问。</summary>
    /// <returns>如果当前 <see cref="T:System.Type" /> 表示枚举，则为 true；否则为 false。</returns>
    bool IsEnum { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsSpecialName" /> 属性的版本无关的访问。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 具有需要特殊处理的名称，则为 true；否则为 false。</returns>
    bool IsSpecialName { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsImport" /> 属性的版本无关的访问。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 具有 <see cref="T:System.Runtime.InteropServices.ComImportAttribute" />，则为 true；否则为 false。</returns>
    bool IsImport { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsSerializable" /> 属性的版本无关的访问。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 是可序列化的，则为 true；否则为 false。</returns>
    bool IsSerializable { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsAnsiClass" /> 属性的版本无关的访问。</summary>
    /// <returns>如果为 <see cref="T:System.Type" /> 选择了字符串格式属性 AnsiClass，则为 true；否则为 false。</returns>
    bool IsAnsiClass { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsUnicodeClass" /> 属性的版本无关的访问。</summary>
    /// <returns>如果为 <see cref="T:System.Type" /> 选择了字符串格式属性 UnicodeClass，则为 true；否则为 false。</returns>
    bool IsUnicodeClass { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsAutoClass" /> 属性的版本无关的访问。</summary>
    /// <returns>如果为 <see cref="T:System.Type" /> 选择了字符串格式属性 AutoClass，则为 true；否则为 false。</returns>
    bool IsAutoClass { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsArray" /> 属性的版本无关的访问。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 是数组，则为 true；否则为 false。</returns>
    bool IsArray { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsByRef" /> 属性的版本无关的访问。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 按引用传递，则为 true；否则为 false。</returns>
    bool IsByRef { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsPointer" /> 属性的版本无关的访问。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 是指针，则为 true；否则为 false。</returns>
    bool IsPointer { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsPrimitive" /> 属性的版本无关的访问。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 为基元类型之一，则为 true；否则为 false。</returns>
    bool IsPrimitive { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsCOMObject" /> 属性的版本无关的访问。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 为 COM 对象，则为 true；否则为 false。</returns>
    bool IsCOMObject { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.HasElementType" /> 属性的版本无关的访问。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 为数组、指针或按引用传递，则为 true；否则为 false。</returns>
    bool HasElementType { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsContextful" /> 属性的版本无关的访问。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 能够在某个上下文中承载，则为 true；否则为 false。</returns>
    bool IsContextful { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Type.IsMarshalByRef" /> 属性的版本无关的访问。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 是由引用封送的，则为 true；否则为 false。</returns>
    bool IsMarshalByRef { get; }

    /// <summary>检索对象提供的类型信息接口的数量（0 或 1）。</summary>
    /// <param name="pcTInfo">指向一个位置，该位置接收对象提供的类型信息接口的数量。</param>
    void GetTypeInfoCount(out uint pcTInfo);

    /// <summary>检索对象的类型信息，然后可以使用该信息获取接口的类型信息。</summary>
    /// <param name="iTInfo">要返回的类型信息。</param>
    /// <param name="lcid">类型信息的区域设置标识符。</param>
    /// <param name="ppTInfo">接收一个指针，指向请求的类型信息对象。</param>
    void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

    /// <summary>将一组名称映射为对应的一组调度标识符。</summary>
    /// <param name="riid">保留供将来使用。必须为 IID_NULL。</param>
    /// <param name="rgszNames">要映射的名称的传入数组。</param>
    /// <param name="cNames">要映射的名称的计数。</param>
    /// <param name="lcid">要在其中解释名称的区域设置上下文。</param>
    /// <param name="rgDispId">调用方分配的数组，用于接收与名称对应的 ID。</param>
    void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

    /// <summary>提供对某一对象公开的属性和方法的访问。</summary>
    /// <param name="dispIdMember">标识成员。</param>
    /// <param name="riid">保留供将来使用。必须为 IID_NULL。</param>
    /// <param name="lcid">要在其中解释参数的区域设置上下文。</param>
    /// <param name="wFlags">描述调用的上下文的标志。</param>
    /// <param name="pDispParams">指向一个结构的指针，该结构包含一个参数数组、一个命名参数的 DISPID 参数数组和数组中元素数的计数。</param>
    /// <param name="pVarResult">指向要存储结果的位置的指针。</param>
    /// <param name="pExcepInfo">指向一个包含异常信息的结构的指针。</param>
    /// <param name="puArgErr">第一个出错参数的索引。</param>
    void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.ToString" /> 方法的版本无关的访问。</summary>
    /// <returns>表示当前 <see cref="T:System.Type" /> 的名称的 <see cref="T:System.String" />。</returns>
    string ToString();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.Equals(System.Object)" /> 方法的版本无关的访问。</summary>
    /// <returns>如果 <paramref name="o" /> 的基础系统类型与当前 <see cref="T:System.Type" /> 的基础系统类型相同，则为 true；否则为 false。</returns>
    /// <param name="other">
    /// <see cref="T:System.Object" />，其基础系统类型将与当前 <see cref="T:System.Type" /> 的基础系统类型相比较。</param>
    bool Equals(object other);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetHashCode" /> 方法的版本无关的访问。</summary>
    /// <returns>包含此实例的哈希代码的 <see cref="T:System.Int32" />。</returns>
    int GetHashCode();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetType" /> 方法的版本无关的访问。</summary>
    /// <returns>当前 <see cref="T:System.Type" />。</returns>
    Type GetType();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Type,System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>应用于此成员的自定义特性的数组；如果未应用任何特性，则为包含零 (0) 个元素的数组。</returns>
    /// <param name="attributeType">要搜索的特性类型。只返回可分配给此类型的属性。</param>
    /// <param name="inherit">指定是否搜索该成员的继承链以查找这些属性。</param>
    object[] GetCustomAttributes(Type attributeType, bool inherit);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetCustomAttributes(System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>应用于此成员的自定义特性的数组；如果未应用任何特性，则为包含零 (0) 个元素的数组。</returns>
    /// <param name="inherit">指定是否搜索该成员的继承链以查找这些属性。</param>
    object[] GetCustomAttributes(bool inherit);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>如果此成员应用了一个或多个 <paramref name="attributeType" /> 实例，则为 true；否则为 false。</returns>
    /// <param name="attributeType">自定义属性应用于的 Type 对象。</param>
    /// <param name="inherit">指定是否搜索该成员的继承链以查找这些属性。</param>
    bool IsDefined(Type attributeType, bool inherit);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetArrayRank" /> 方法的版本无关的访问。</summary>
    /// <returns>包含当前 <see cref="T:System.Type" /> 中维数的 <see cref="T:System.Int32" />。</returns>
    int GetArrayRank();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetConstructors(System.Reflection.BindingFlags)" /> 方法的版本无关的访问。</summary>
    /// <returns>表示为当前 <see cref="T:System.Type" /> 定义的匹配指定绑定约束的所有构造函数的 <see cref="T:System.Reflection.ConstructorInfo" /> 对象数组，包括类型初始值设定项（如果定义的话）。如果当前 <see cref="T:System.Type" /> 没有定义构造函数，或者定义的构造函数都不符合绑定约束，或者当前 <see cref="T:System.Type" /> 表示泛型类型或方法定义的类型参数，则返回 <see cref="T:System.Reflection.ConstructorInfo" /> 类型的空数组。</returns>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 -零，以返回 null。</param>
    ConstructorInfo[] GetConstructors(BindingFlags bindingAttr);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetInterface(System.String,System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>表示具有指定名称且由当前的 <see cref="T:System.Type" /> 实现或继承的接口的 <see cref="T:System.Type" /> 对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的接口名称的 <see cref="T:System.String" />。对于泛型接口，这是重整名称。</param>
    /// <param name="ignoreCase">true，表示对 <paramref name="name" /> 执行不区分大小写的搜索。- 或 -false，表示对 <paramref name="name" /> 执行区分大小写的搜索。</param>
    Type GetInterface(string name, bool ignoreCase);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetInterfaces" /> 方法的版本无关的访问。</summary>
    /// <returns>表示由当前 <see cref="T:System.Type" /> 实现或继承的所有接口的 <see cref="T:System.Type" /> 对象数组。- 或 -如果没有由当前 <see cref="T:System.Type" /> 实现或继承的接口，则为 <see cref="T:System.Type" /> 类型的空数组。</returns>
    Type[] GetInterfaces();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.FindInterfaces(System.Reflection.TypeFilter,System.Object)" /> 方法的版本无关的访问。</summary>
    /// <returns>表示接口（由当前 <see cref="T:System.Type" /> 所实现或继承）的筛选列表的 <see cref="T:System.Type" /> 对象数组。- 或 -如果没有匹配筛选器的接口由当前 <see cref="T:System.Type" /> 实现或继承，则为 <see cref="T:System.Type" /> 类型的空数组。</returns>
    /// <param name="filter">对照 <paramref name="filterCriteria" /> 比较接口的 <see cref="T:System.Reflection.TypeFilter" /> 委托。</param>
    /// <param name="filterCriteria">确定接口是否应包括在返回数组中的搜索判据。</param>
    Type[] FindInterfaces(TypeFilter filter, object filterCriteria);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetEvent(System.String,System.Reflection.BindingFlags)" /> 方法的版本无关的访问。</summary>
    /// <returns>如果找到，则为 <see cref="T:System.Reflection.EventInfo" /> 对象，该对象表示当前 <see cref="T:System.Type" /> 所声明或继承的指定事件；否则为 null。</returns>
    /// <param name="name">包含事件名称的 <see cref="T:System.String" />，该事件是由当前 <see cref="T:System.Type" /> 声明或继承的。</param>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 -零，以返回 null。</param>
    EventInfo GetEvent(string name, BindingFlags bindingAttr);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetEvents" /> 方法的版本无关的访问。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.EventInfo" /> 对象数组，表示当前 <see cref="T:System.Type" /> 所声明或继承的所有公共事件。- 或 -如果当前 <see cref="T:System.Type" /> 没有公共事件，则为 <see cref="T:System.Reflection.EventInfo" /> 类型的空数组。</returns>
    EventInfo[] GetEvents();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetEvents(System.Reflection.BindingFlags)" /> 方法的版本无关的访问。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.EventInfo" /> 对象数组，表示当前 <see cref="T:System.Type" /> 所声明或继承的与指定绑定约束匹配的所有事件。- 或 -如果当前 <see cref="T:System.Type" /> 没有事件，或者如果没有一个事件匹配绑定约束，则为 <see cref="T:System.Reflection.EventInfo" /> 类型的空数组。</returns>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 -零，以返回 null。</param>
    EventInfo[] GetEvents(BindingFlags bindingAttr);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetNestedTypes(System.Reflection.BindingFlags)" /> 方法的版本无关的访问，并使用指定绑定约束搜索嵌套在当前 <see cref="T:System.Type" /> 中的类型。</summary>
    /// <returns>表示嵌套在当前 <see cref="T:System.Type" /> 中的匹配指定绑定约束的所有类型的 <see cref="T:System.Type" /> 对象数组。- 或 -如果没有嵌套在当前 <see cref="T:System.Type" /> 中的类型，或者如果没有一个嵌套类型匹配绑定约束，则为 <see cref="T:System.Type" /> 类型的空数组。</returns>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 -零，以返回 null。</param>
    Type[] GetNestedTypes(BindingFlags bindingAttr);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetNestedType(System.String,System.Reflection.BindingFlags)" /> 方法的版本无关的访问。</summary>
    /// <returns>表示符合指定要求的嵌套类型的 <see cref="T:System.Type" /> 对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的嵌套类型的名称的字符串。</param>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 -零，以返回 null。</param>
    Type GetNestedType(string name, BindingFlags bindingAttr);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetMember(System.String,System.Reflection.MemberTypes,System.Reflection.BindingFlags)" /> 方法的版本无关的访问。</summary>
    /// <returns>一个表示具有指定名称的公共成员的 <see cref="T:System.Reflection.MemberInfo" /> 对象数组（如果找到的话）；否则为空数组。</returns>
    /// <param name="name">包含要获取的成员的名称的 <see cref="T:System.String" />。</param>
    /// <param name="type">要搜索的 <see cref="T:System.Reflection.MemberTypes" /> 值。</param>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 -零，返回空数组。</param>
    MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetDefaultMembers" /> 方法的版本无关的访问。</summary>
    /// <returns>表示当前 <see cref="T:System.Type" /> 的所有默认成员的 <see cref="T:System.Reflection.MemberInfo" /> 对象数组。- 或 -如果当前 <see cref="T:System.Type" /> 没有默认成员，则为 <see cref="T:System.Reflection.MemberInfo" /> 类型的空数组。</returns>
    MemberInfo[] GetDefaultMembers();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.FindMembers(System.Reflection.MemberTypes,System.Reflection.BindingFlags,System.Reflection.MemberFilter,System.Object)" /> 方法的版本无关的访问。</summary>
    /// <returns>指定成员类型的 <see cref="T:System.Reflection.MemberInfo" /> 对象的筛选数组。- 或 -如果当前 <see cref="T:System.Type" /> 没有匹配筛选判据的 <paramref name="memberType" /> 类型成员，则为 <see cref="T:System.Reflection.MemberInfo" /> 类型的空数组。</returns>
    /// <param name="memberType">指示要搜索的成员类型的 MemberTypes 对象。</param>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 -零，以返回 null。</param>
    /// <param name="filter">执行比较的委托，如果当前被检查的成员匹配 <paramref name="filterCriteria" />，则返回 true；否则返回 false。可以使用该类提供的 FilterAttribute、FilterName 和 FilterNameIgnoreCase 委托。第一个委托使用 FieldAttributes、MethodAttributes 和 MethodImplAttributes 的字段作为搜索判据，另两个委托使用 String 对象作为搜索判据。</param>
    /// <param name="filterCriteria">确定成员是否在 MemberInfo 对象数组中返回的搜索判据。FieldAttributes、MethodAttributes 和 MethodImplAttributes 的字段可以和该类提供的 FilterAttribute 委托一起使用。</param>
    MemberInfo[] FindMembers(MemberTypes memberType, BindingFlags bindingAttr, MemberFilter filter, object filterCriteria);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetElementType" /> 方法的版本无关的访问。</summary>
    /// <returns>当前数组、指针或引用类型包含的或引用的对象的 <see cref="T:System.Type" />。- 或 -如果当前 <see cref="T:System.Type" /> 不是数组或指针，或者不是通过引用传递的，或者表示泛型类型，或者表示泛型类型或方法定义的类型参数，则为 null。</returns>
    Type GetElementType();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.IsSubclassOf(System.Type)" /> 方法的版本无关的访问。</summary>
    /// <returns>如果 <see cref="T:System.Type" /> 由 <paramref name="c" /> 参数表示并且当前的 <see cref="T:System.Type" /> 表示类，并且当前的 <paramref name="c" /> 所表示的类是从 <see cref="T:System.Type" /> 所表示的类派生的，则为 true；否则为 false。如果 <paramref name="c" /> 和当前的 <see cref="T:System.Type" /> 表示相同的类，则此方法还返回 false。</returns>
    /// <param name="c">与当前的 <see cref="T:System.Type" /> 进行比较的 <see cref="T:System.Type" />。</param>
    bool IsSubclassOf(Type c);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.IsInstanceOfType(System.Object)" /> 方法的版本无关的访问。</summary>
    /// <returns>如果满足下列任一条件，则为 true：当前 <see cref="T:System.Type" /> 位于由 <paramref name="o" /> 表示的对象的继承层次结构中；当前 <see cref="T:System.Type" /> 是 <paramref name="o" /> 支持的接口。如果不属于其中任一种情况，或者 <paramref name="o" /> 为 null 或者当前 <see cref="T:System.Type" /> 为开放式泛型类型（即 <see cref="P:System.Type.ContainsGenericParameters" /> 返回 true），则为 false。</returns>
    /// <param name="o">将与当前 <see cref="T:System.Type" /> 进行比较的对象。</param>
    bool IsInstanceOfType(object o);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.IsAssignableFrom(System.Type)" /> 方法的版本无关的访问。</summary>
    /// <returns>如果满足下列任一条件，则为 true：<paramref name="c" /> 和当前 <see cref="T:System.Type" /> 表示同一类型；当前 <see cref="T:System.Type" /> 位于 <paramref name="c" /> 的继承层次结构中；当前 <see cref="T:System.Type" /> 是 <paramref name="c" /> 实现的接口；<paramref name="c" /> 是泛型类型参数且当前 <see cref="T:System.Type" /> 表示 <paramref name="c" /> 的约束之一。如果这些条件都不成立，或者 <paramref name="c" /> 为 null，则为 false。</returns>
    /// <param name="c">与当前的 <see cref="T:System.Type" /> 进行比较的 <see cref="T:System.Type" />。</param>
    bool IsAssignableFrom(Type c);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetInterfaceMap(System.Type)" /> 方法的版本无关的访问。</summary>
    /// <returns>表示 <paramref name="interfaceType" /> 的接口映射的 <see cref="T:System.Reflection.InterfaceMapping" /> 对象。</returns>
    /// <param name="interfaceType">要检索其映射的接口的 <see cref="T:System.Type" />。</param>
    InterfaceMapping GetInterfaceMap(Type interfaceType);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Type[],System.Reflection.ParameterModifier[])" /> 方法的版本无关的访问。</summary>
    /// <returns>表示符合指定要求的方法的 <see cref="T:System.Reflection.MethodInfo" /> 对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的方法名称的 <see cref="T:System.String" />。</param>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 -零，以返回 null。</param>
    /// <param name="binder">一个 <see cref="T:System.Reflection.Binder" /> 对象，该对象定义一组属性并启用绑定，而绑定可能涉及选择重载方法、强制参数类型和通过反射调用成员。- 或 -若为 null，则使用 <see cref="P:System.Type.DefaultBinder" />。</param>
    /// <param name="types">表示此方法要获取的参数的个数、顺序和类型的 <see cref="T:System.Type" /> 对象数组。- 或 -一个类型为 <see cref="T:System.Type" />（即 Type[] types = new Type[0]）的空数组，用于获取一个不带参数的方法。</param>
    /// <param name="modifiers">
    /// <see cref="T:System.Reflection.ParameterModifier" /> 对象数组，表示与 <paramref name="types" /> 数组中的相应元素关联的属性。默认的联编程序不处理此参数。</param>
    MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags)" /> 方法的版本无关的访问。</summary>
    /// <returns>表示符合指定要求的方法的 <see cref="T:System.Reflection.MethodInfo" /> 对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的方法名称的 <see cref="T:System.String" />。</param>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 -零，以返回 null。</param>
    MethodInfo GetMethod(string name, BindingFlags bindingAttr);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetMethods(System.Reflection.BindingFlags)" /> 方法的版本无关的访问。</summary>
    /// <returns>表示为当前 <see cref="T:System.Type" /> 定义的匹配指定绑定约束的所有方法的 <see cref="T:System.Reflection.MethodInfo" /> 对象数组。- 或 -如果没有为当前 <see cref="T:System.Type" /> 定义的方法，或者如果没有一个定义的方法匹配绑定约束，则为 <see cref="T:System.Reflection.MethodInfo" /> 类型的空数组。</returns>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 -零，以返回 null。</param>
    MethodInfo[] GetMethods(BindingFlags bindingAttr);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetField(System.String,System.Reflection.BindingFlags)" /> 方法的版本无关的访问。</summary>
    /// <returns>表示符合指定要求的字段的 <see cref="T:System.Reflection.FieldInfo" /> 对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的数据字段的名称的 <see cref="T:System.String" />。</param>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 -零，以返回 null。</param>
    FieldInfo GetField(string name, BindingFlags bindingAttr);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetFields(System.Reflection.BindingFlags)" /> 方法的版本无关的访问。</summary>
    /// <returns>表示为当前 <see cref="T:System.Type" /> 定义的匹配指定绑定约束的所有字段的 <see cref="T:System.Reflection.FieldInfo" /> 对象数组。- 或 -如果没有为当前 <see cref="T:System.Type" /> 定义的字段，或者如果没有一个定义的字段匹配绑定约束，则为 <see cref="T:System.Reflection.FieldInfo" /> 类型的空数组。</returns>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 -零，以返回 null。</param>
    FieldInfo[] GetFields(BindingFlags bindingAttr);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetProperty(System.String,System.Reflection.BindingFlags)" /> 方法的版本无关的访问。</summary>
    /// <returns>表示符合指定要求的属性的 <see cref="T:System.Reflection.PropertyInfo" /> 对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的属性名的 <see cref="T:System.String" />。</param>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 -零，以返回 null。</param>
    PropertyInfo GetProperty(string name, BindingFlags bindingAttr);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetProperty(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Type,System.Type[],System.Reflection.ParameterModifier[])" /> 方法的版本无关的访问。</summary>
    /// <returns>表示符合指定要求的属性的 <see cref="T:System.Reflection.PropertyInfo" /> 对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的属性名的 <see cref="T:System.String" />。</param>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 -零，以返回 null。</param>
    /// <param name="binder">一个 <see cref="T:System.Reflection.Binder" /> 对象，该对象定义一组属性并启用绑定，而绑定可能涉及选择重载方法、强制参数类型和通过反射调用成员。- 或 -若为 null，则使用 <see cref="P:System.Type.DefaultBinder" />。</param>
    /// <param name="returnType">属性的返回类型。</param>
    /// <param name="types">一个 <see cref="T:System.Type" /> 对象数组，表示要获取的索引属性的参数的数目、顺序和类型。- 或 -获取未被索引的属性的 <see cref="T:System.Type" /> 类型的空数组（即 Type[] types = new Type[0]）。</param>
    /// <param name="modifiers">
    /// <see cref="T:System.Reflection.ParameterModifier" /> 对象数组，表示与 <paramref name="types" /> 数组中的相应元素关联的属性。默认的联编程序不处理此参数。</param>
    PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetProperties(System.Reflection.BindingFlags)" /> 方法的版本无关的访问。</summary>
    /// <returns>表示当前 <see cref="T:System.Type" /> 的匹配指定绑定约束的所有属性的 <see cref="T:System.Reflection.PropertyInfo" /> 对象数组。- 或 -如果当前 <see cref="T:System.Type" /> 没有属性，或者如果没有一个属性匹配绑定约束，则为 <see cref="T:System.Reflection.PropertyInfo" /> 类型的空数组。</returns>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 -零，以返回 null。</param>
    PropertyInfo[] GetProperties(BindingFlags bindingAttr);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetMember(System.String,System.Reflection.BindingFlags)" /> 方法的版本无关的访问。</summary>
    /// <returns>一个表示具有指定名称的公共成员的 <see cref="T:System.Reflection.MemberInfo" /> 对象数组（如果找到的话）；否则为空数组。</returns>
    /// <param name="name">包含要获取的成员的名称的 <see cref="T:System.String" />。</param>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 -零，返回空数组。</param>
    MemberInfo[] GetMember(string name, BindingFlags bindingAttr);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetMembers(System.Reflection.BindingFlags)" /> 方法的版本无关的访问。</summary>
    /// <returns>表示为当前 <see cref="T:System.Type" /> 定义的匹配指定绑定约束的所有成员的 <see cref="T:System.Reflection.MemberInfo" /> 对象数组。- 或 -如果没有为当前 <see cref="T:System.Type" /> 定义的成员，或者如果没有一个定义的成员匹配绑定约束，则为 <see cref="T:System.Reflection.MemberInfo" /> 类型的空数组。</returns>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 -零，以返回 null。</param>
    MemberInfo[] GetMembers(BindingFlags bindingAttr);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" /> 方法的版本无关的访问。</summary>
    /// <returns>表示被调用成员的返回值的 <see cref="T:System.Object" />。</returns>
    /// <param name="name">
    /// <see cref="T:System.String" />，它包含要调用的构造函数、方法、属性或字段成员的名称。- 或 -空字符串 ("")，表示调用默认成员。- 或 -对于 IDispatch 成员，一个表示 DispID 的字符串，例如“[DispID=3]”。</param>
    /// <param name="invokeAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。访问可以是 BindingFlags 之一，如 Public、NonPublic、Private、InvokeMethod 和 GetField 等。查找类型无需指定。如果省略查找的类型，则将使用 BindingFlags.Public   |BindingFlags.Instance 将应用。</param>
    /// <param name="binder">一个 <see cref="T:System.Reflection.Binder" /> 对象，该对象定义一组属性并启用绑定，而绑定可能涉及选择重载方法、强制参数类型和通过反射调用成员。- 或 -若为 null，则使用 <see cref="P:System.Type.DefaultBinder" />。</param>
    /// <param name="target">要在其上调用指定成员的 <see cref="T:System.Object" />。</param>
    /// <param name="args">包含传递给要调用的成员的参数的数组。</param>
    /// <param name="modifiers">
    /// <see cref="T:System.Reflection.ParameterModifier" /> 对象数组，表示与 <paramref name="args" /> 数组中的相应元素关联的属性。参数的关联的属性存储在成员的签名中。默认的联编程序不处理此参数。</param>
    /// <param name="culture">表示要使用的全局化区域设置的 <see cref="T:System.Globalization.CultureInfo" /> 对象，它对区域设置特定的转换可能是必需的，比如将数字 String 转换为 Double。- 或 -null，表示使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    /// <param name="namedParameters">包含参数名称的数组，<paramref name="args" /> 数组中的值将传递给这些参数。</param>
    object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Globalization.CultureInfo)" /> 方法的版本无关的访问。</summary>
    /// <returns>表示被调用成员的返回值的 <see cref="T:System.Object" />。</returns>
    /// <param name="name">
    /// <see cref="T:System.String" />，它包含要调用的构造函数、方法、属性或字段成员的名称。- 或 -空字符串 ("")，表示调用默认成员。- 或 -对于 IDispatch 成员，一个表示 DispID 的字符串，例如“[DispID=3]”。</param>
    /// <param name="invokeAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。访问可以是 BindingFlags 之一，如 Public、NonPublic、Private、InvokeMethod 和 GetField 等。查找类型无需指定。如果省略查找的类型，则将使用 BindingFlags.Public   |BindingFlags.Instance 将应用。</param>
    /// <param name="binder">一个 <see cref="T:System.Reflection.Binder" /> 对象，该对象定义一组属性并启用绑定，而绑定可能涉及选择重载方法、强制参数类型和通过反射调用成员。- 或 -若为 null，则使用 <see cref="P:System.Type.DefaultBinder" />。</param>
    /// <param name="target">要在其上调用指定成员的 <see cref="T:System.Object" />。</param>
    /// <param name="args">包含传递给要调用的成员的参数的数组。</param>
    /// <param name="culture">表示要使用的全局化区域设置的 <see cref="T:System.Globalization.CultureInfo" /> 对象，它对区域设置特定的转换可能是必需的，比如将数字 String 转换为 Double。- 或 -null，表示使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, CultureInfo culture);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[])" /> 方法的版本无关的访问。</summary>
    /// <returns>表示被调用成员的返回值的 <see cref="T:System.Object" />。</returns>
    /// <param name="name">
    /// <see cref="T:System.String" />，它包含要调用的构造函数、方法、属性或字段成员的名称。- 或 -空字符串 ("")，表示调用默认成员。- 或 -对于 IDispatch 成员，一个表示 DispID 的字符串，例如“[DispID=3]”。</param>
    /// <param name="invokeAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。访问可以是 BindingFlags 之一，如 Public、NonPublic、Private、InvokeMethod 和 GetField 等。查找类型无需指定。如果省略查找的类型，则将使用 BindingFlags.Public   |BindingFlags.Instance 将应用。</param>
    /// <param name="binder">一个 <see cref="T:System.Reflection.Binder" /> 对象，该对象定义一组属性并启用绑定，而绑定可能涉及选择重载方法、强制参数类型和通过反射调用成员。- 或 -若为 null，则使用 <see cref="P:System.Type.DefaultBinder" />。</param>
    /// <param name="target">要在其上调用指定成员的 <see cref="T:System.Object" />。</param>
    /// <param name="args">包含传递给要调用的成员的参数的数组。</param>
    object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetConstructor(System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> 方法的版本无关的访问。</summary>
    /// <returns>表示符合指定要求的构造函数的 <see cref="T:System.Reflection.ConstructorInfo" /> 对象（如果找到的话）；否则为 null。</returns>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 -零，以返回 null。</param>
    /// <param name="binder">一个 <see cref="T:System.Reflection.Binder" /> 对象，该对象定义一组属性并启用绑定，而绑定可能涉及选择重载方法、强制参数类型和通过反射调用成员。- 或 -若为 null，则使用 <see cref="P:System.Type.DefaultBinder" />。</param>
    /// <param name="callConvention">
    /// <see cref="T:System.Reflection.CallingConventions" /> 对象，用于指定要使用的一套规则，这些规则涉及参数的顺序和布局、传递返回值的方式、用于参数的寄存器和清理堆栈的方式。</param>
    /// <param name="types">
    /// <see cref="T:System.Type" /> 对象数组，表示构造函数要获取的参数的个数、顺序和类型。- 或 -获取不使用参数的构造函数的 <see cref="T:System.Type" /> 类型的空数组（即 Type[] types = new Type[0]）。</param>
    /// <param name="modifiers">
    /// <see cref="T:System.Reflection.ParameterModifier" /> 对象数组，表示与 <paramref name="types" /> 数组中的相应元素关联的属性。默认的联编程序不处理此参数。</param>
    ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetConstructor(System.Reflection.BindingFlags,System.Reflection.Binder,System.Type[],System.Reflection.ParameterModifier[])" /> 方法的版本无关的访问。</summary>
    /// <returns>表示符合指定要求的构造函数的 <see cref="T:System.Reflection.ConstructorInfo" /> 对象（如果找到的话）；否则为 null。</returns>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 -零，以返回 null。</param>
    /// <param name="binder">一个 <see cref="T:System.Reflection.Binder" /> 对象，该对象定义一组属性并启用绑定，而绑定可能涉及选择重载方法、强制参数类型和通过反射调用成员。- 或 -若为 null，则使用 <see cref="P:System.Type.DefaultBinder" />。</param>
    /// <param name="types">
    /// <see cref="T:System.Type" /> 对象数组，表示构造函数要获取的参数的个数、顺序和类型。- 或 -获取不使用参数的构造函数的 <see cref="T:System.Type" /> 类型的空数组（即 Type[] types = new Type[0]）。- 或 -<see cref="F:System.Type.EmptyTypes" />.</param>
    /// <param name="modifiers">
    /// <see cref="T:System.Reflection.ParameterModifier" /> 对象数组，表示与参数类型数组中的相应元素关联的属性。默认的联编程序不处理此参数。</param>
    ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetConstructor(System.Type[])" /> 方法的版本无关的访问。</summary>
    /// <returns>为表示某个公共实例构造函数（该构造函数的参数与参数类型数组中的类型匹配）的 <see cref="T:System.Reflection.ConstructorInfo" /> 对象（如果找到的话）；否则为 null。</returns>
    /// <param name="types">表示需要的构造函数的参数个数、顺序和类型的 <see cref="T:System.Type" /> 对象的数组。- 或 -<see cref="T:System.Type" /> 对象的空数组，用于获取不带参数的构造函数。这样的空数组由 static 字段 <see cref="F:System.Type.EmptyTypes" /> 提供。</param>
    ConstructorInfo GetConstructor(Type[] types);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetConstructors" /> 方法的版本无关的访问。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.ConstructorInfo" /> 对象数组，表示当前 <see cref="T:System.Type" /> 定义的所有公共实例构造函数，但不包括类型初始值设定项（静态构造函数）。如果当前 <see cref="T:System.Type" /> 没有定义公共实例构造函数，或者当前 <see cref="T:System.Type" /> 表示泛型类型或方法定义的类型参数，则返回 <see cref="T:System.Reflection.ConstructorInfo" /> 类型的空数组。</returns>
    ConstructorInfo[] GetConstructors();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> 方法的版本无关的访问。</summary>
    /// <returns>表示符合指定要求的方法的 <see cref="T:System.Reflection.MethodInfo" /> 对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的方法名称的 <see cref="T:System.String" />。</param>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 -零，以返回 null。</param>
    /// <param name="binder">一个 <see cref="T:System.Reflection.Binder" /> 对象，该对象定义一组属性并启用绑定，而绑定可能涉及选择重载方法、强制参数类型和通过反射调用成员。- 或 -若为 null，则使用 <see cref="P:System.Type.DefaultBinder" />。</param>
    /// <param name="callConvention">
    /// <see cref="T:System.Reflection.CallingConventions" /> 对象，用于指定要使用的一套规则，这些规则涉及参数的顺序和布局、传递返回值的方式、用于参数的寄存器和清理堆栈的方式。</param>
    /// <param name="types">表示此方法要获取的参数的个数、顺序和类型的 <see cref="T:System.Type" /> 对象数组。- 或 -一个类型为 <see cref="T:System.Type" />（即 Type[] types = new Type[0]）的空数组，用于获取一个不带参数的方法。</param>
    /// <param name="modifiers">
    /// <see cref="T:System.Reflection.ParameterModifier" /> 对象数组，表示与 <paramref name="types" /> 数组中的相应元素关联的属性。默认的联编程序不处理此参数。</param>
    MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetMethod(System.String,System.Type[],System.Reflection.ParameterModifier[])" /> 方法的版本无关的访问。</summary>
    /// <returns>表示符合指定要求的公共方法的 <see cref="T:System.Reflection.MethodInfo" /> 对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的公共方法的名称的 <see cref="T:System.String" />。</param>
    /// <param name="types">表示此方法要获取的参数的个数、顺序和类型的 <see cref="T:System.Type" /> 对象数组。- 或 -一个类型为 <see cref="T:System.Type" />（即 Type[] types = new Type[0]）的空数组，用于获取一个不带参数的方法。</param>
    /// <param name="modifiers">
    /// <see cref="T:System.Reflection.ParameterModifier" /> 对象数组，表示与 <paramref name="types" /> 数组中的相应元素关联的属性。默认的联编程序不处理此参数。</param>
    MethodInfo GetMethod(string name, Type[] types, ParameterModifier[] modifiers);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetMethod(System.String,System.Type[])" /> 方法的版本无关的访问。</summary>
    /// <returns>表示其参数与指定参数类型匹配的公共方法的 <see cref="T:System.Reflection.MethodInfo" /> 对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的公共方法的名称的 <see cref="T:System.String" />。</param>
    /// <param name="types">表示此方法要获取的参数的个数、顺序和类型的 <see cref="T:System.Type" /> 对象数组。- 或 -一个类型为 <see cref="T:System.Type" />（即 Type[] types = new Type[0]）的空数组，用于获取一个不带参数的方法。</param>
    MethodInfo GetMethod(string name, Type[] types);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetMethod(System.String)" /> 方法的版本无关的访问。</summary>
    /// <returns>表示具有指定名称的公共方法的 <see cref="T:System.Reflection.MethodInfo" /> 对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的公共方法的名称的 <see cref="T:System.String" />。</param>
    MethodInfo GetMethod(string name);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetMethods" /> 方法的版本无关的访问。</summary>
    /// <returns>表示为当前 <see cref="T:System.Type" /> 定义的所有公共方法的 <see cref="T:System.Reflection.MethodInfo" /> 对象数组。- 或 -如果没有为当前 <see cref="T:System.Type" /> 定义的公共方法，则为 <see cref="T:System.Reflection.MethodInfo" /> 类型的空数组。</returns>
    MethodInfo[] GetMethods();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetField(System.String)" /> 方法的版本无关的访问。</summary>
    /// <returns>如找到，则为表示具有指定名称的公共字段的 <see cref="T:System.Reflection.FieldInfo" /> 对象；否则为 null。</returns>
    /// <param name="name">包含要获取的数据字段的名称的 <see cref="T:System.String" />。</param>
    FieldInfo GetField(string name);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetFields" /> 方法的版本无关的访问。</summary>
    /// <returns>表示为当前 <see cref="T:System.Type" /> 定义的所有公共字段的 <see cref="T:System.Reflection.FieldInfo" /> 对象数组。- 或 -如果没有为当前 <see cref="T:System.Type" /> 定义的公共字段，则为 <see cref="T:System.Reflection.FieldInfo" /> 类型的空数组。</returns>
    FieldInfo[] GetFields();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetInterface(System.String)" /> 方法的版本无关的访问。</summary>
    /// <returns>表示具有指定名称且由当前的 <see cref="T:System.Type" /> 实现或继承的接口的 <see cref="T:System.Type" /> 对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的接口名称的 <see cref="T:System.String" />。对于泛型接口，这是重整名称。</param>
    Type GetInterface(string name);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetEvent(System.String)" /> 方法的版本无关的访问。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.EventInfo" /> 对象数组，表示当前 <see cref="T:System.Type" /> 所声明或继承的与指定绑定约束匹配的所有事件。- 或 -如果当前 <see cref="T:System.Type" /> 没有事件，或者如果没有一个事件匹配绑定约束，则为 <see cref="T:System.Reflection.EventInfo" /> 类型的空数组。</returns>
    /// <param name="name">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 -零，以返回 null。</param>
    EventInfo GetEvent(string name);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetProperty(System.String,System.Type,System.Type[],System.Reflection.ParameterModifier[])" /> 方法的版本无关的访问。</summary>
    /// <returns>表示符合指定要求的公共属性的 <see cref="T:System.Reflection.PropertyInfo" /> 对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的公共属性名的 <see cref="T:System.String" />。</param>
    /// <param name="returnType">属性的返回类型。</param>
    /// <param name="types">一个 <see cref="T:System.Type" /> 对象数组，表示要获取的索引属性的参数的数目、顺序和类型。- 或 -获取未被索引的属性的 <see cref="T:System.Type" /> 类型的空数组（即 Type[] types = new Type[0]）。</param>
    /// <param name="modifiers">
    /// <see cref="T:System.Reflection.ParameterModifier" /> 对象数组，表示与 <paramref name="types" /> 数组中的相应元素关联的属性。默认的联编程序不处理此参数。</param>
    PropertyInfo GetProperty(string name, Type returnType, Type[] types, ParameterModifier[] modifiers);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetProperty(System.String,System.Type,System.Type[])" /> 方法的版本无关的访问。</summary>
    /// <returns>表示其参数与指定参数类型匹配的公共属性的 <see cref="T:System.Reflection.PropertyInfo" /> 对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的公共属性名的 <see cref="T:System.String" />。</param>
    /// <param name="returnType">属性的返回类型。</param>
    /// <param name="types">一个 <see cref="T:System.Type" /> 对象数组，表示要获取的索引属性的参数的数目、顺序和类型。- 或 -获取未被索引的属性的 <see cref="T:System.Type" /> 类型的空数组（即 Type[] types = new Type[0]）。</param>
    PropertyInfo GetProperty(string name, Type returnType, Type[] types);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetProperty(System.String,System.Type[])" /> 方法的版本无关的访问。</summary>
    /// <returns>表示其参数与指定参数类型匹配的公共属性的 <see cref="T:System.Reflection.PropertyInfo" /> 对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的公共属性名的 <see cref="T:System.String" />。</param>
    /// <param name="types">一个 <see cref="T:System.Type" /> 对象数组，表示要获取的索引属性的参数的数目、顺序和类型。- 或 -获取未被索引的属性的 <see cref="T:System.Type" /> 类型的空数组（即 Type[] types = new Type[0]）。</param>
    PropertyInfo GetProperty(string name, Type[] types);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetProperty(System.String,System.Type)" /> 方法的版本无关的访问。</summary>
    /// <returns>表示具有指定名称的公共属性的 <see cref="T:System.Reflection.PropertyInfo" /> 对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的公共属性名的 <see cref="T:System.String" />。</param>
    /// <param name="returnType">属性的返回类型。</param>
    PropertyInfo GetProperty(string name, Type returnType);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetProperty(System.String)" /> 方法的版本无关的访问。</summary>
    /// <returns>表示具有指定名称的公共属性的 <see cref="T:System.Reflection.PropertyInfo" /> 对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的公共属性名的 <see cref="T:System.String" />。</param>
    PropertyInfo GetProperty(string name);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetProperties" /> 方法的版本无关的访问。</summary>
    /// <returns>表示当前 <see cref="T:System.Type" /> 的所有公共属性的 <see cref="T:System.Reflection.PropertyInfo" /> 对象数组。- 或 -如果当前 <see cref="T:System.Type" /> 没有公共属性，则为 <see cref="T:System.Reflection.PropertyInfo" /> 类型的空数组。</returns>
    PropertyInfo[] GetProperties();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetNestedTypes" /> 方法的版本无关的访问。</summary>
    /// <returns>表示嵌套在当前 <see cref="T:System.Type" /> 中的所有类型的 <see cref="T:System.Type" /> 对象数组。- 或 -如果没有嵌套在当前 <see cref="T:System.Type" /> 中的类型，则为 <see cref="T:System.Type" /> 类型的空数组。</returns>
    Type[] GetNestedTypes();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetNestedType(System.String)" /> 方法的版本无关的访问。</summary>
    /// <returns>如找到，则为表示具有指定名称的公共嵌套类型的 <see cref="T:System.Type" /> 对象；否则为 null。</returns>
    /// <param name="name">包含要获取的嵌套类型的名称的字符串。</param>
    Type GetNestedType(string name);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetMember(System.String)" /> 方法的版本无关的访问。</summary>
    /// <returns>一个表示具有指定名称的公共成员的 <see cref="T:System.Reflection.MemberInfo" /> 对象数组（如果找到的话）；否则为空数组。</returns>
    /// <param name="name">包含要获取的公共成员名称的 <see cref="T:System.String" />。</param>
    MemberInfo[] GetMember(string name);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetMembers" /> 方法的版本无关的访问。</summary>
    /// <returns>表示当前 <see cref="T:System.Type" /> 的所有公共成员的 <see cref="T:System.Reflection.MemberInfo" /> 对象数组。- 或 -如果当前 <see cref="T:System.Type" /> 没有公共成员，则为 <see cref="T:System.Reflection.MemberInfo" /> 类型的空数组。</returns>
    MemberInfo[] GetMembers();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.Equals(System.Type)" /> 方法的版本无关的访问。</summary>
    /// <returns>如果 <paramref name="o" /> 的基础系统类型与当前 <see cref="T:System.Type" /> 的基础系统类型相同，则为 true；否则为 false。</returns>
    /// <param name="o">
    /// <see cref="T:System.Type" />，其基础系统类型将与当前 <see cref="T:System.Type" /> 的基础系统类型相比较。</param>
    bool Equals(Type o);
  }
}
