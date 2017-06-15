// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices._EventInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.InteropServices
{
  /// <summary>向非托管代码公开 <see cref="T:System.Reflection.EventInfo" /> 类的公共成员。</summary>
  [Guid("9DE59C64-D889-35A1-B897-587D74469E5B")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [CLSCompliant(false)]
  [TypeLibImportClass(typeof (EventInfo))]
  [ComVisible(true)]
  public interface _EventInfo
  {
    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.EventInfo.MemberType" /> 属性的版本无关的访问。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.MemberTypes" /> 值，指示此成员是事件。</returns>
    MemberTypes MemberType { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MemberInfo.Name" /> 属性的版本无关的访问。</summary>
    /// <returns>包含此成员的名称的 <see cref="T:System.String" /> 对象。</returns>
    string Name { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MemberInfo.DeclaringType" /> 属性的版本无关的访问。</summary>
    /// <returns>声明该成员的类的 Type 对象。</returns>
    Type DeclaringType { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MemberInfo.ReflectedType" /> 属性的版本无关的访问。</summary>
    /// <returns>用于获取此对象的 <see cref="T:System.Type" /> 对象。</returns>
    Type ReflectedType { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.EventInfo.Attributes" /> 属性的版本无关的访问。</summary>
    /// <returns>此事件的只读特性。</returns>
    EventAttributes Attributes { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.EventInfo.EventHandlerType" /> 属性的版本无关的访问。</summary>
    /// <returns>表示委托事件处理程序的只读 <see cref="T:System.Type" /> 对象。</returns>
    Type EventHandlerType { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.EventInfo.IsSpecialName" /> 属性的版本无关的访问。</summary>
    /// <returns>如果此事件具有一个特殊名称，则为 true；否则为 false。</returns>
    bool IsSpecialName { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.EventInfo.IsMulticast" /> 属性的版本无关的访问。</summary>
    /// <returns>如果该委托是多路广播委托的实例，则为 true；否则为 false。</returns>
    bool IsMulticast { get; }

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
    /// <param name="inherit">搜索此成员的继承链以查找这些属性，则为 true；否则为 false。</param>
    object[] GetCustomAttributes(Type attributeType, bool inherit);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>一个包含所有自定义特性的数组，在未定义任何特性时为包含零 (0) 个元素的数组。</returns>
    /// <param name="inherit">true 表示搜索成员的继承链以查找这些特性；否则为 false。</param>
    object[] GetCustomAttributes(bool inherit);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>如果此成员应用了一个或多个 <paramref name="attributeType" /> 参数的实例，则为 true；否则为 false。</returns>
    /// <param name="attributeType">自定义属性应用于的 Type 对象。</param>
    /// <param name="inherit">搜索此成员的继承链以查找这些属性，则为 true；否则为 false。</param>
    bool IsDefined(Type attributeType, bool inherit);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.EventInfo.GetAddMethod(System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.MethodInfo" /> 对象，它表示用于将事件处理程序委托添加到事件源的方法。</returns>
    /// <param name="nonPublic">true 表示返回非公共方法；否则为 false。</param>
    MethodInfo GetAddMethod(bool nonPublic);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.EventInfo.GetRemoveMethod(System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.MethodInfo" /> 对象，它表示用于从事件源中移除事件处理程序委托的方法。</returns>
    /// <param name="nonPublic">true 表示返回非公共方法；否则为 false。</param>
    MethodInfo GetRemoveMethod(bool nonPublic);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.EventInfo.GetRaiseMethod(System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>在引发该事件时调用的 <see cref="T:System.Reflection.MethodInfo" /> 对象。</returns>
    /// <param name="nonPublic">true 表示返回非公共方法；否则为 false。</param>
    MethodInfo GetRaiseMethod(bool nonPublic);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.EventInfo.GetAddMethod" /> 方法的版本无关的访问。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.MethodInfo" /> 对象，它表示用于将事件处理程序委托添加到事件源的方法。</returns>
    MethodInfo GetAddMethod();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.EventInfo.GetRemoveMethod" /> 方法的版本无关的访问。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.MethodInfo" /> 对象，它表示用于从事件源中移除事件处理程序委托的方法。</returns>
    MethodInfo GetRemoveMethod();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.EventInfo.GetRaiseMethod" /> 方法的版本无关的访问。</summary>
    /// <returns>引发该事件时所调用的方法。</returns>
    MethodInfo GetRaiseMethod();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.EventInfo.AddEventHandler(System.Object,System.Delegate)" /> 方法的版本无关的访问。</summary>
    /// <param name="target">事件源。</param>
    /// <param name="handler">目标引发事件时将调用的方法。</param>
    void AddEventHandler(object target, Delegate handler);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.EventInfo.RemoveEventHandler(System.Object,System.Delegate)" /> 方法的版本无关的访问。</summary>
    /// <param name="target">事件源。</param>
    /// <param name="handler">将解除与由目标引发的事件的关联的委托。</param>
    void RemoveEventHandler(object target, Delegate handler);
  }
}
