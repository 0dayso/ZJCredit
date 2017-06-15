// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices._ConstructorInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
  /// <summary>向非托管代码公开 <see cref="T:System.Reflection.ConstructorInfo" /> 类的公共成员。</summary>
  [Guid("E9A19478-9646-3679-9B10-8411AE1FD57D")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [CLSCompliant(false)]
  [TypeLibImportClass(typeof (ConstructorInfo))]
  [ComVisible(true)]
  public interface _ConstructorInfo
  {
    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.ConstructorInfo.MemberType" /> 属性的版本无关的访问。</summary>
    /// <returns>指示成员类型的 <see cref="T:System.Reflection.MemberTypes" /> 值。</returns>
    MemberTypes MemberType { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MemberInfo.Name" /> 属性的版本无关的访问。</summary>
    /// <returns>包含此成员名称的 <see cref="T:System.String" />。</returns>
    string Name { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MemberInfo.DeclaringType" /> 属性的版本无关的访问。</summary>
    /// <returns>声明该成员的类的 <see cref="T:System.Type" /> 对象。</returns>
    Type DeclaringType { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MemberInfo.ReflectedType" /> 属性的版本无关的访问。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 对象，通过它获取了该 <see cref="T:System.Reflection.MemberInfo" /> 对象。</returns>
    Type ReflectedType { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MethodBase.MethodHandle" /> 属性的版本无关的访问。</summary>
    /// <returns>一个 <see cref="T:System.RuntimeMethodHandle" /> 对象。</returns>
    RuntimeMethodHandle MethodHandle { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MethodBase.Attributes" /> 属性的版本无关的访问。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.MethodAttributes" /> 值之一。</returns>
    MethodAttributes Attributes { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MethodBase.CallingConvention" /> 属性的版本无关的访问。</summary>
    /// <returns>此方法的 <see cref="T:System.Reflection.CallingConventions" />。</returns>
    CallingConventions CallingConvention { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MethodBase.IsPublic" /> 属性的版本无关的访问。</summary>
    /// <returns>如果此方法是公共的，则为 true；否则为 false。</returns>
    bool IsPublic { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MethodBase.IsPrivate" /> 属性的版本无关的访问。</summary>
    /// <returns>如果对此方法的访问只限于该类本身的其他成员，则为 true；否则为 false。</returns>
    bool IsPrivate { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MethodBase.IsFamily" /> 属性的版本无关的访问。</summary>
    /// <returns>如果对此类的访问只限于此类本身的成员和它的派生类的成员，则为 true；否则为 false。</returns>
    bool IsFamily { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MethodBase.IsAssembly" /> 属性的版本无关的访问。</summary>
    /// <returns>如果此方法可以由同一程序集中的其他类调用，则为 true；否则为 false。</returns>
    bool IsAssembly { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MethodBase.IsFamilyAndAssembly" /> 属性的版本无关的访问。</summary>
    /// <returns>如果对此方法的访问只限于此类本身的成员和同一程序集中的派生类的成员，则为 true；否则为 false。</returns>
    bool IsFamilyAndAssembly { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MethodBase.IsFamilyOrAssembly" /> 属性的版本无关的访问。</summary>
    /// <returns>如果对此方法的访问只限于该类本身的成员、派生类的成员（与它们的位置无关）以及同一程序集中其他类的成员，则为 true；否则为 false。</returns>
    bool IsFamilyOrAssembly { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MethodBase.IsStatic" /> 属性的版本无关的访问。</summary>
    /// <returns>如果此方法为 static，则为 true；否则为 false。</returns>
    bool IsStatic { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MethodBase.IsFinal" /> 属性的版本无关的访问。</summary>
    /// <returns>如果此方法是 final 方法，则为 true；否则为 false。</returns>
    bool IsFinal { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MethodBase.IsVirtual" /> 属性的版本无关的访问。</summary>
    /// <returns>如果此方法为 virtual，则为 true；否则为 false。</returns>
    bool IsVirtual { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MethodBase.IsHideBySig" /> 属性的版本无关的访问。</summary>
    /// <returns>如果此成员被签名隐藏，则为 true；否则为 false。</returns>
    bool IsHideBySig { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MethodBase.IsAbstract" /> 属性的版本无关的访问。</summary>
    /// <returns>如果该方法是抽象的，则为 true；否则为 false。</returns>
    bool IsAbstract { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MethodBase.IsSpecialName" /> 属性的版本无关的访问。</summary>
    /// <returns>如果此方法具有特殊名称，则为 true；否则为 false。</returns>
    bool IsSpecialName { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MethodBase.IsConstructor" /> 属性的版本无关的访问。</summary>
    /// <returns>如果此方法为构造函数，则为 true；否则为 false。</returns>
    bool IsConstructor { get; }

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

    /// <summary>为 COM 对象提供对 <see cref="M:System.Object.ToString" /> 方法的版本无关的访问。</summary>
    /// <returns>表示当前 <see cref="T:System.Object" /> 的字符串。</returns>
    string ToString();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Object.Equals(System.Object)" /> 方法的版本无关的访问。</summary>
    /// <returns>如果指定的 <see cref="T:System.Object" /> 等于当前的 <see cref="T:System.Object" />，则为 true；否则为 false。</returns>
    /// <param name="other">与当前的 <see cref="T:System.Object" /> 进行比较的 <see cref="T:System.Object" />。</param>
    bool Equals(object other);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Object.GetHashCode" /> 方法的版本无关的访问。</summary>
    /// <returns>当前实例的哈希代码。</returns>
    int GetHashCode();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Object.GetType" /> 方法的版本无关的访问。</summary>
    /// <returns>一个 <see cref="T:System.Type" /> 对象。</returns>
    Type GetType();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Emit.MethodBuilder.GetCustomAttributes(System.Type,System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>应用于此成员的自定义特性的数组；如果未应用任何特性，则为包含零 (0) 个元素的数组。</returns>
    /// <param name="attributeType">要搜索的特性类型。只返回可分配给此类型的属性。</param>
    /// <param name="inherit">指定是否搜索该成员的继承链以查找这些属性。</param>
    object[] GetCustomAttributes(Type attributeType, bool inherit);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>一个包含所有自定义特性的数组，在未定义任何特性时为包含零个元素的数组。</returns>
    /// <param name="inherit">指定是否搜索该成员的继承链以查找这些属性。</param>
    object[] GetCustomAttributes(bool inherit);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> 成员的版本无关的访问。</summary>
    /// <returns>如果此成员应用了一个或多个 <paramref name="attributeType" /> 实例，则为 true；否则为 false。</returns>
    /// <param name="attributeType">自定义属性应用于的 Type 对象。</param>
    /// <param name="inherit">指定是否搜索该成员的继承链以查找这些属性。</param>
    bool IsDefined(Type attributeType, bool inherit);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.MethodBase.GetParameters" /> 方法的版本无关的访问。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.ParameterInfo" /> 类型的数组，包含与此实例所反射的方法（或构造函数）的签名相匹配的信息。</returns>
    ParameterInfo[] GetParameters();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.MethodBase.GetMethodImplementationFlags" /> 成员的版本无关的访问。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.MethodImplAttributes" /> 标志。</returns>
    MethodImplAttributes GetMethodImplementationFlags();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.MethodBase.Invoke(System.Object,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo)" /> 方法的版本无关的访问。</summary>
    /// <returns>与构造函数关联的类的实例。</returns>
    /// <param name="obj">创建了此方法的实例。</param>
    /// <param name="invokeAttr">指定绑定类型的 BindingFlags 值之一。</param>
    /// <param name="binder">一个 Binder，它定义一组属性并通过反映来启用绑定、参数类型强制和成员调用。如果 <paramref name="binder" /> 为 null，则使用 Binder.DefaultBinding。</param>
    /// <param name="parameters">Object 类型的数组，该类型用于在 <paramref name="binder" /> 的约束下匹配此构造函数的参数的个数、顺序和类型。如果此构造函数不需要参数，则像 Object[] parameters = new Object[0] 中那样传递一个包含零元素的数组。如果此数组中的对象未用值来显式初始化，则该对象将包含该对象类型的默认值。对于引用类型的元素，该值为 null。对于值类型的元素，该值为 0、0.0 或 false，具体取决于特定的元素类型。</param>
    /// <param name="culture">用于控制类型强制的 <see cref="T:System.Globalization.CultureInfo" />。如果这是 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    object Invoke_2(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.MethodBase.Invoke(System.Object,System.Object[])" /> 方法的版本无关的访问。</summary>
    /// <returns>与构造函数关联的类的实例。</returns>
    /// <param name="obj">创建了此方法的实例。</param>
    /// <param name="parameters">调用的方法或构造函数的参数列表。这是一个对象数组，这些对象与要调用的方法或构造函数的参数具有相同的数量、顺序和类型。如果没有任何参数，则 <paramref name="parameters" /> 应为 null。如果此实例所表示的方法或构造函数采用 ref 参数（在 Visual Basic 中为 ByRef），使用此函数调用该方法或构造函数时，该参数不需要任何特殊属性。如果此数组中的对象未用值来显式初始化，则该对象将包含该对象类型的默认值。对于引用类型的元素，该值为 null。对于值类型的元素，该值为 0、0.0 或 false，具体取决于特定的元素类型。</param>
    object Invoke_3(object obj, object[] parameters);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.ConstructorInfo.Invoke(System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo)" /> 方法的版本无关的访问。</summary>
    /// <returns>与构造函数关联的类的实例。</returns>
    /// <param name="invokeAttr">指定绑定类型的 BindingFlags 值之一。</param>
    /// <param name="binder">一个 Binder，它定义一组属性并通过反映来启用绑定、参数类型强制和成员调用。如果 <paramref name="binder" /> 为 null，则使用 Binder.DefaultBinding。</param>
    /// <param name="parameters">Object 类型的数组，该类型用于在 <paramref name="binder" /> 的约束下匹配此构造函数的参数的个数、顺序和类型。如果此构造函数不需要参数，则像 Object[] parameters = new Object[0] 中那样传递一个包含零元素的数组。如果此数组中的对象未用值来显式初始化，则该对象将包含该对象类型的默认值。对于引用类型的元素，该值为 null。对于值类型的元素，该值为 0、0.0 或 false，具体取决于特定的元素类型。</param>
    /// <param name="culture">用于控制类型强制的 <see cref="T:System.Globalization.CultureInfo" />。如果这是 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    object Invoke_4(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.ConstructorInfo.Invoke(System.Object[])" /> 方法的版本无关的访问。</summary>
    /// <returns>与构造函数关联的类的实例。</returns>
    /// <param name="parameters">与此构造函数的参数的个数、顺序和类型（受默认联编程序的约束）相匹配的值的数组。如果此构造函数没有参数，则像 Object[] parameters = new Object[0] 中那样，使用包含零元素或 null 的数组。如果此数组中的对象未用值来显式初始化，则该对象将包含该对象类型的默认值。对于引用类型的元素，该值为 null。对于值类型的元素，该值为 0、0.0 或 false，具体取决于特定的元素类型。</param>
    object Invoke_5(object[] parameters);
  }
}
