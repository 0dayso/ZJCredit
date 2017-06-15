// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices._MemberInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.InteropServices
{
  /// <summary>向非托管代码公开 <see cref="T:System.Reflection.MemberInfo" /> 类的公共成员。</summary>
  [Guid("f7102fa9-cabb-3a74-a6da-b4567ef1b079")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [TypeLibImportClass(typeof (MemberInfo))]
  [CLSCompliant(false)]
  [ComVisible(true)]
  public interface _MemberInfo
  {
    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MemberInfo.MemberType" /> 属性的版本无关的访问。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.MemberTypes" /> 值之一，指示成员的类型。</returns>
    MemberTypes MemberType { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MemberInfo.Name" /> 属性的版本无关的访问。</summary>
    /// <returns>包含此成员的名称的 <see cref="T:System.String" /> 对象。</returns>
    string Name { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MemberInfo.DeclaringType" /> 属性的版本无关的访问。</summary>
    /// <returns>声明该成员的类的 <see cref="T:System.Type" /> 对象。</returns>
    Type DeclaringType { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.MemberInfo.ReflectedType" /> 属性的版本无关的访问。</summary>
    /// <returns>用于获取此 <see cref="T:System.Reflection.MemberInfo" /> 对象的 <see cref="T:System.Type" /> 对象。</returns>
    Type ReflectedType { get; }

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

    /// <summary>为 COM 对象提供对 <see cref="M:System.Type.GetType" /> 方法的版本无关的访问。</summary>
    /// <returns>一个 <see cref="T:System.Type" /> 对象。</returns>
    Type GetType();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetCustomAttributes(System.Type,System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>应用于此成员的自定义特性的数组；如果未应用任何特性，则为包含零 (0) 个元素的数组。</returns>
    /// <param name="attributeType">要搜索的特性类型。只返回可分配给此类型的属性。</param>
    /// <param name="inherit">搜索此成员的继承链以查找这些属性，则为 true；否则为 false。</param>
    object[] GetCustomAttributes(Type attributeType, bool inherit);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>一个包含所有自定义特性的数组，在未定义任何特性时为包含零 (0) 个元素的数组。</returns>
    /// <param name="inherit">搜索此成员的继承链以查找这些属性，则为 true；否则为 false。</param>
    object[] GetCustomAttributes(bool inherit);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>如果此成员应用了一个或多个 <paramref name="attributeType" /> 参数的实例，则为 true；否则为 false。</returns>
    /// <param name="attributeType">自定义属性应用于的 <see cref="T:System.Type" /> 对象。</param>
    /// <param name="inherit">搜索此成员的继承链以查找这些属性，则为 true；否则为 false。</param>
    bool IsDefined(Type attributeType, bool inherit);
  }
}
