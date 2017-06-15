// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices._FieldInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
  /// <summary>向非托管代码公开 <see cref="T:System.Reflection.FieldInfo" /> 类的公共成员。</summary>
  [Guid("8A7C1442-A9FB-366B-80D8-4939FFA6DBE0")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [CLSCompliant(false)]
  [TypeLibImportClass(typeof (FieldInfo))]
  [ComVisible(true)]
  public interface _FieldInfo
  {
    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.FieldInfo.MemberType" /> 属性的版本无关的访问。</summary>
    /// <returns>指示此成员是字段的 <see cref="T:System.Reflection.MemberTypes" /> 值。</returns>
    MemberTypes MemberType { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MemberInfo.Name" /> 属性的版本无关的访问。</summary>
    /// <returns>包含此成员名称的 <see cref="T:System.String" />。</returns>
    string Name { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MemberInfo.DeclaringType" /> 属性的版本无关的访问。</summary>
    /// <returns>声明该成员的类的 <see cref="T:System.Type" /> 对象。</returns>
    Type DeclaringType { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MemberInfo.ReflectedType" /> 属性的版本无关的访问。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 对象，通过它获取了此对象。</returns>
    Type ReflectedType { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.FieldInfo.FieldType" /> 属性的版本无关的访问。</summary>
    /// <returns>此字段对象的类型。</returns>
    Type FieldType { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.FieldInfo.FieldHandle" /> 属性的版本无关的访问。</summary>
    /// <returns>某个字段的内部元数据表示形式的句柄。</returns>
    RuntimeFieldHandle FieldHandle { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.FieldInfo.Attributes" /> 属性的版本无关的访问。</summary>
    /// <returns>此字段的 <see cref="T:System.Reflection.FieldAttributes" />。</returns>
    FieldAttributes Attributes { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.FieldInfo.IsPublic" /> 属性的版本无关的访问。</summary>
    /// <returns>如果此字段为公共字段，则为 true；否则为 false。</returns>
    bool IsPublic { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.FieldInfo.IsPrivate" /> 属性的版本无关的访问。</summary>
    /// <returns>如果此字段为私有字段，则为 true；否则为 false。</returns>
    bool IsPrivate { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.FieldInfo.IsFamily" /> 属性的版本无关的访问。</summary>
    /// <returns>如果字段设置了 Family 属性，则为 true；否则为 false。</returns>
    bool IsFamily { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.FieldInfo.IsAssembly" /> 属性的版本无关的访问。</summary>
    /// <returns>如果字段设置了 Assembly 属性，则为 true；否则为 false。</returns>
    bool IsAssembly { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.FieldInfo.IsFamilyAndAssembly" /> 属性的版本无关的访问。</summary>
    /// <returns>如果字段设置了 FamANDAssem 属性，则为 true；否则为 false。</returns>
    bool IsFamilyAndAssembly { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.FieldInfo.IsFamilyOrAssembly" /> 属性的版本无关的访问。</summary>
    /// <returns>如果字段设置了 FamORAssem 属性，则为 true；否则为 false。</returns>
    bool IsFamilyOrAssembly { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.FieldInfo.IsStatic" /> 属性的版本无关的访问。</summary>
    /// <returns>如果此字段为静态字段，则为 true；否则为 false。</returns>
    bool IsStatic { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.FieldInfo.IsInitOnly" /> 属性的版本无关的访问。</summary>
    /// <returns>如果字段设置了 InitOnly 属性，则为 true；否则为 false。</returns>
    bool IsInitOnly { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.FieldInfo.IsLiteral" /> 属性的版本无关的访问。</summary>
    /// <returns>如果字段设置了 Literal 属性，则为 true；否则为 false。</returns>
    bool IsLiteral { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.FieldInfo.IsNotSerialized" /> 属性的版本无关的访问。</summary>
    /// <returns>如果字段设置了 NotSerialized 属性，则为 true；否则为 false。</returns>
    bool IsNotSerialized { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.FieldInfo.IsSpecialName" /> 属性的版本无关的访问。</summary>
    /// <returns>如果在 <see cref="T:System.Reflection.FieldAttributes" /> 中设置了 SpecialName 特性，则为 true；否则为 false。</returns>
    bool IsSpecialName { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.FieldInfo.IsPinvokeImpl" /> 属性的版本无关的访问。</summary>
    /// <returns>如果在 <see cref="T:System.Reflection.FieldAttributes" /> 中设置了 PinvokeImpl 特性，则为 true；否则为 false。</returns>
    bool IsPinvokeImpl { get; }

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

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Type,System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>应用于此成员的自定义特性的数组；如果未应用任何特性，则为包含零 (0) 个元素的数组。</returns>
    /// <param name="attributeType">要搜索的特性类型。只返回可分配给此类型的属性。</param>
    /// <param name="inherit">指定是否搜索该成员的继承链以查找这些属性。</param>
    object[] GetCustomAttributes(Type attributeType, bool inherit);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>一个包含所有自定义特性的数组，在未定义任何特性时为包含零个元素的数组。</returns>
    /// <param name="inherit">指定是否搜索该成员的继承链以查找这些属性。</param>
    object[] GetCustomAttributes(bool inherit);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>如果此成员应用了一个或多个 <paramref name="attributeType" /> 实例，则为 true；否则为 false。</returns>
    /// <param name="attributeType">自定义属性应用于的 <see cref="T:System.Type" /> 对象。</param>
    /// <param name="inherit">指定是否搜索该成员的继承链以查找这些属性。</param>
    bool IsDefined(Type attributeType, bool inherit);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.FieldInfo.GetValue(System.Object)" /> 方法的版本无关的访问。</summary>
    /// <returns>包含此实例反映的字段值的对象。</returns>
    /// <param name="obj">其字段值将返回的对象。</param>
    object GetValue(object obj);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.FieldInfo.GetValueDirect(System.TypedReference)" /> 方法的版本无关的访问。</summary>
    /// <returns>包含字段值的 <see cref="T:System.Object" />。</returns>
    /// <param name="obj">
    /// <see cref="T:System.TypedReference" /> 结构，封装指向某个位置的托管指针和可能存储在该位置的类型的运行时表示形式。</param>
    object GetValueDirect(TypedReference obj);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.PropertyInfo.SetValue(System.Object,System.Object,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo)" /> 方法的版本无关的访问。</summary>
    /// <param name="obj">将设置其字段值的对象。</param>
    /// <param name="value">分配给字段的值。</param>
    /// <param name="invokeAttr">指定所需的绑定类型（例如，Binder.CreateInstance 或 Binder.ExactBinding）的 <see cref="T:System.Reflection.Binder" /> 的字段。</param>
    /// <param name="binder">一组通过反射启用绑定、参数类型强制和成员调用的属性。如果 <paramref name="binder" /> 为 null，则使用 Binder.DefaultBinding。</param>
    /// <param name="culture">特定区域性的软件首选项。</param>
    void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.FieldInfo.SetValueDirect(System.TypedReference,System.Object)" /> 方法的版本无关的访问。</summary>
    /// <param name="obj">将设置其字段值的对象。</param>
    /// <param name="value">分配给字段的值。</param>
    void SetValueDirect(TypedReference obj, object value);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.FieldInfo.SetValue(System.Object,System.Object)" /> 方法的版本无关的访问。</summary>
    /// <param name="obj">将设置其字段值的对象。</param>
    /// <param name="value">分配给字段的值。</param>
    void SetValue(object obj, object value);
  }
}
