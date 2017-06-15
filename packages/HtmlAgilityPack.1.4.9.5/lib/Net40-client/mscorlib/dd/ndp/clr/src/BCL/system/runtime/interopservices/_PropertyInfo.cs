// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices._PropertyInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
  /// <summary>向非托管代码公开 <see cref="T:System.Reflection.PropertyInfo" /> 类的公共成员。</summary>
  [Guid("F59ED4E4-E68F-3218-BD77-061AA82824BF")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [CLSCompliant(false)]
  [TypeLibImportClass(typeof (PropertyInfo))]
  [ComVisible(true)]
  public interface _PropertyInfo
  {
    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.PropertyInfo.MemberType" /> 属性的版本无关的访问。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.MemberTypes" /> 值之一，指示此成员是一个属性。</returns>
    MemberTypes MemberType { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MemberInfo.Name" /> 属性的版本无关的访问。</summary>
    /// <returns>包含此成员的名称的 <see cref="T:System.String" /> 对象。</returns>
    string Name { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MemberInfo.DeclaringType" /> 属性的版本无关的访问。</summary>
    /// <returns>声明该成员的类的 Type 对象。</returns>
    Type DeclaringType { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MemberInfo.ReflectedType" /> 属性的版本无关的访问。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 对象，通过它获取了该 <see cref="T:System.Reflection.MemberInfo" /> 对象。</returns>
    Type ReflectedType { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.PropertyInfo.PropertyType" /> 属性的版本无关的访问。</summary>
    /// <returns>此属性的类型。</returns>
    Type PropertyType { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.PropertyInfo.Attributes" /> 属性的版本无关的访问。</summary>
    /// <returns>此属性的特性。</returns>
    PropertyAttributes Attributes { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.PropertyInfo.CanRead" /> 属性的版本无关的访问。</summary>
    /// <returns>如果此属性可读，则为 true；否则为 false。</returns>
    bool CanRead { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.PropertyInfo.CanWrite" /> 属性的版本无关的访问。</summary>
    /// <returns>如果此属性可写，则为 true；否则，为 false。</returns>
    bool CanWrite { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.PropertyInfo.IsSpecialName" /> 属性的版本无关的访问。</summary>
    /// <returns>如果此属性是特殊名称，则为 true；否则为 false。</returns>
    bool IsSpecialName { get; }

    /// <summary>检索对象提供的类型信息接口的数量（0 或 1）。</summary>
    /// <param name="pcTInfo">此方法返回时包含一个用于接收对象提供的类型信息接口数量的位置指针。该参数未经初始化即被传递。</param>
    void GetTypeInfoCount(out uint pcTInfo);

    /// <summary>检索对象的类型信息，然后可以使用该信息获取接口的类型信息。</summary>
    /// <param name="iTInfo">要返回的类型信息。</param>
    /// <param name="lcid">类型信息的区域设置标识符。</param>
    /// <param name="ppTInfo">指向请求的类型信息对象的指针。</param>
    void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

    /// <summary>将一组名称映射为对应的一组调度标识符。</summary>
    /// <param name="riid">保留供将来使用。必须为 IID_NULL。</param>
    /// <param name="rgszNames">要映射的名称的数组。</param>
    /// <param name="cNames">要映射的名称的计数。</param>
    /// <param name="lcid">要在其中解释名称的区域设置上下文。</param>
    /// <param name="rgDispId">调用方分配的数组，接收对应于这些名称的标识符。</param>
    void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

    /// <summary>提供对某一对象公开的属性和方法的访问。</summary>
    /// <param name="dispIdMember">成员的标识符。</param>
    /// <param name="riid">保留供将来使用。必须为 IID_NULL。</param>
    /// <param name="lcid">要在其中解释参数的区域设置上下文。</param>
    /// <param name="wFlags">描述调用的上下文的标志。</param>
    /// <param name="pDispParams">指向一个结构的指针，该结构包含一个参数数组、一个命名参数的 DISPID 参数数组和数组元素的计数。</param>
    /// <param name="pVarResult">指向一个将存储结果的位置的指针。</param>
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
    /// <param name="inherit">true 表示搜索此成员的继承链以查找这些特性；否则为 false。</param>
    object[] GetCustomAttributes(Type attributeType, bool inherit);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>一个包含所有自定义特性的数组，在未定义任何特性时为包含零个元素的数组。</returns>
    /// <param name="inherit">true 表示搜索此成员的继承链以查找这些特性；否则为 false。</param>
    object[] GetCustomAttributes(bool inherit);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>如果对此成员应用了 <paramref name="attributeType" /> 参数的一个或多个实例，则为 true；否则为 false。</returns>
    /// <param name="attributeType">自定义属性应用于的 <see cref="T:System.Type" /> 对象。</param>
    /// <param name="inherit">true 表示搜索此成员的继承链以查找这些特性；否则为 false。</param>
    bool IsDefined(Type attributeType, bool inherit);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.PropertyInfo.GetValue(System.Object,System.Object[])" /> 方法的版本无关的访问。</summary>
    /// <returns>
    /// <paramref name="obj" /> 参数的属性值。</returns>
    /// <param name="obj">将返回其属性值的对象。</param>
    /// <param name="index">索引化属性的可选索引值。对于非索引化属性，该值应为 null。</param>
    object GetValue(object obj, object[] index);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.PropertyInfo.GetValue(System.Object,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo)" /> 方法的版本无关的访问。</summary>
    /// <returns>
    /// <paramref name="obj" /> 参数的属性值。</returns>
    /// <param name="obj">将返回其属性值的对象。</param>
    /// <param name="invokeAttr">调用属性。这必须是 BindingFlags 中的位标志：InvokeMethod、CreateInstance、Static、GetField、SetField、GetProperty 或 SetProperty。必须指定合适的调用属性。如果要调用静态成员，则必须设置 BindingFlags 的 Static 标志。</param>
    /// <param name="binder">一个对象，它启用绑定、对参数类型的强制、对成员的调用，以及通过反射对 MemberInfo 对象的检索。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="index">索引化属性的可选索引值。对于非索引化属性，该值应为 null。</param>
    /// <param name="culture">CultureInfo 对象，它表示资源将针对哪个区域性进行本地化。请注意，如果没有为此区域性本地化该资源，则在搜索匹配项的过程中将继续调用 CultureInfo.Parent 方法。如果此值为 null，则从 CultureInfo.CurrentUICulture 属性获得 CultureInfo。</param>
    object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.PropertyInfo.SetValue(System.Object,System.Object,System.Object[])" /> 方法的版本无关的访问。</summary>
    /// <param name="obj">将设置其属性值的对象。</param>
    /// <param name="value">此属性的新值。</param>
    /// <param name="index">索引化属性的可选索引值。对于非索引化属性，该值应为 null。</param>
    void SetValue(object obj, object value, object[] index);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.FieldInfo.SetValue(System.Object,System.Object,System.Reflection.BindingFlags,System.Reflection.Binder,System.Globalization.CultureInfo)" /> 方法的版本无关的访问。</summary>
    /// <param name="obj">将返回其属性值的对象。</param>
    /// <param name="value">此属性的新值。</param>
    /// <param name="invokeAttr">调用属性。这必须是 <see cref="T:System.Reflection.BindingFlags" /> 中的位标志：InvokeMethod、CreateInstance、Static、GetField、SetField、GetProperty 或 SetProperty。必须指定合适的调用属性。如果要调用静态成员，则必须设置 BindingFlags 的 Static 标志。</param>
    /// <param name="binder">一个对象，它启用绑定、对参数类型的强制、对成员的调用，以及通过反射对 <see cref="T:System.Reflection.MemberInfo" /> 对象的检索。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="index">索引化属性的可选索引值。对于非索引化属性，该值应为 null。</param>
    /// <param name="culture">
    /// <see cref="T:System.Globalization.CultureInfo" /> 对象，它表示资源将针对哪个区域性进行本地化。请注意，如果没有为此区域性本地化该资源，则在搜索匹配项的过程中将继续调用 CultureInfo.Parent 方法。如果此值为 null，则从 CultureInfo.CurrentUICulture 属性获得 CultureInfo。</param>
    void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.PropertyInfo.GetAccessors(System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.MethodInfo" /> 对象的数组，其元素反射了由当前实例反射的属性的 get、set 及其他访问器。如果 <paramref name="nonPublic" /> 参数为 true，则此数组包含公共及非公共 get、set 及其他访问器。如果 <paramref name="nonPublic" /> 为 false，则此数组仅包含公共 get、set 及其他访问器。如果没有找到具有指定可见性的访问器，则此方法将返回包含零 (0) 个元素的数组。</returns>
    /// <param name="nonPublic">true 表示在返回的 MethodInfo 数组中包含非公共方法；否则，为 false。</param>
    MethodInfo[] GetAccessors(bool nonPublic);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.PropertyInfo.GetGetMethod(System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>如果 <paramref name="nonPublic" /> 参数为 true，则返回表示此属性的 get 访问器的 <see cref="T:System.Reflection.MethodInfo" /> 对象。如果 <paramref name="nonPublic" /> 为 false 且 get 访问器是非公共的，或者 <paramref name="nonPublic" /> 为 true 但是不存在 get 访问器，则返回 null。</returns>
    /// <param name="nonPublic">true 表示返回非公共的 get 访问器；否则，为 false。</param>
    MethodInfo GetGetMethod(bool nonPublic);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.PropertyInfo.GetSetMethod(System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>下表中的值之一。值含义表示此属性的 Set 方法的 <see cref="T:System.Reflection.MethodInfo" /> 对象。set 访问器是公共的。- 或 -<paramref name="nonPublic" /> 参数为 true，且 set 访问器是非公共的。null<paramref name="nonPublic" /> 参数为 true，但属性是只读的。- 或 -<paramref name="nonPublic" /> 参数为 false，且 set 访问器是非公共的。- 或 -不存在 set 访问器。</returns>
    /// <param name="nonPublic">true 表示返回非公共访问器；否则，为 false。</param>
    MethodInfo GetSetMethod(bool nonPublic);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.PropertyInfo.GetIndexParameters" /> 方法的版本无关的访问。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.ParameterInfo" /> 类型的数组，它包含索引的参数。</returns>
    ParameterInfo[] GetIndexParameters();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.PropertyInfo.GetAccessors" /> 方法的版本无关的访问。</summary>
    /// <returns>如果找到访问器，则返回一个 <see cref="T:System.Reflection.MethodInfo" /> 对象的数组，这些对象反射了由当前实例反射的属性的公共 get、set 以及其他访问器；如果没有找到访问器，则返回包含零 (0) 个元素的数组。</returns>
    MethodInfo[] GetAccessors();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.PropertyInfo.GetGetMethod" /> 方法的版本无关的访问。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.MethodInfo" /> 对象，表示此属性的公共 get 访问器；如果 get 访问器是非公共的或不存在，则为 null。</returns>
    MethodInfo GetGetMethod();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.PropertyInfo.GetSetMethod" /> 方法的版本无关的访问。</summary>
    /// <returns>如果 set 访问器是公共的，则为表示此属性的 Set 方法的 <see cref="T:System.Reflection.MethodInfo" /> 对象；如果 set 访问器是非公共的，则为 null。</returns>
    MethodInfo GetSetMethod();
  }
}
