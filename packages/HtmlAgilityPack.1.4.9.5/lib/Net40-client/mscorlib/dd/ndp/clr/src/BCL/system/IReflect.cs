// Decompiled with JetBrains decompiler
// Type: System.Reflection.IReflect
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>与 IDispatch 接口进行互操作。</summary>
  [Guid("AFBF15E5-C37C-11d2-B88E-00A0C9B471B8")]
  [ComVisible(true)]
  public interface IReflect
  {
    /// <summary>获取表示 <see cref="T:System.Reflection.IReflect" /> 对象的基础类型。</summary>
    /// <returns>表示 <see cref="T:System.Reflection.IReflect" /> 对象的基础类型。</returns>
    Type UnderlyingSystemType { get; }

    /// <summary>检索与指定方法对应的 <see cref="T:System.Reflection.MethodInfo" /> 对象，使用 <see cref="T:System.Type" /> 数组从重载方法中进行选择。</summary>
    /// <returns>匹配所有指定参数的请求的方法。</returns>
    /// <param name="name">要查找的成员的名称。</param>
    /// <param name="bindingAttr">用于控制搜索的绑定属性。</param>
    /// <param name="binder">实现 <see cref="T:System.Reflection.Binder" /> 的对象，它包含与此方法相关的属性。</param>
    /// <param name="types">用于从重载方法中进行选择的数组。</param>
    /// <param name="modifiers">参数修饰符数组，用来与参数签名进行绑定，这些参数签名中的类型已经被修改。</param>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">该对象实现同名的多个方法。</exception>
    MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

    /// <summary>在指定的搜索约束下检索与指定方法对应的 <see cref="T:System.Reflection.MethodInfo" /> 对象。</summary>
    /// <returns>包含方法信息的 <see cref="T:System.Reflection.MethodInfo" /> 对象，匹配基于方法名和 <paramref name="bindingAttr" /> 中指定的搜索约束。</returns>
    /// <param name="name">要查找的成员的名称。</param>
    /// <param name="bindingAttr">用于控制搜索的绑定属性。</param>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">该对象实现同名的多个方法。</exception>
    MethodInfo GetMethod(string name, BindingFlags bindingAttr);

    /// <summary>检索与所有公共方法或当前类的所有方法相关的 <see cref="T:System.Reflection.MethodInfo" /> 对象的数组。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.MethodInfo" /> 对象的数组，包含为此反射对象定义的符合 <paramref name="bindingAttr" /> 中指定的搜索约束的所有方法。</returns>
    /// <param name="bindingAttr">用于控制搜索的绑定属性。</param>
    MethodInfo[] GetMethods(BindingFlags bindingAttr);

    /// <summary>返回与指定字段和绑定标志对应的 <see cref="T:System.Reflection.FieldInfo" /> 对象。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.FieldInfo" /> 对象，包含命名对象的符合 <paramref name="bindingAttr" /> 中指定的搜索约束的字段信息。</returns>
    /// <param name="name">要查找的字段的名称。</param>
    /// <param name="bindingAttr">用于控制搜索的绑定属性。</param>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">该对象实现同名的多个字段。</exception>
    FieldInfo GetField(string name, BindingFlags bindingAttr);

    /// <summary>返回与当前类的所有字段对应的 <see cref="T:System.Reflection.FieldInfo" /> 对象的数组。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.FieldInfo" /> 对象的数组，包含此反射对象的符合 <paramref name="bindingAttr" /> 中指定的搜索约束的所有字段信息。</returns>
    /// <param name="bindingAttr">用于控制搜索的绑定属性。</param>
    FieldInfo[] GetFields(BindingFlags bindingAttr);

    /// <summary>在指定搜索约束下检索与指定属性对应的 <see cref="T:System.Reflection.PropertyInfo" /> 对象。</summary>
    /// <returns>找到的与 <paramref name="bindingAttr" /> 中指定的搜索约束相符的属性的 <see cref="T:System.Reflection.PropertyInfo" /> 对象；如果没有找到此属性，则为 null。</returns>
    /// <param name="name">要查找的属性的名称。</param>
    /// <param name="bindingAttr">用于控制搜索的绑定属性。</param>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">该对象实现同名的多个字段。</exception>
    PropertyInfo GetProperty(string name, BindingFlags bindingAttr);

    /// <summary>在指定的搜索约束下检索与指定属性对应的 <see cref="T:System.Reflection.PropertyInfo" /> 对象。</summary>
    /// <returns>如果在此反射对象中找到了具有指定名称的属性，则为所找到的属性的 <see cref="T:System.Reflection.PropertyInfo" /> 对象；如果没有找到此属性，则为 null。</returns>
    /// <param name="name">要查找的成员的名称。</param>
    /// <param name="bindingAttr">用于控制搜索的绑定属性。</param>
    /// <param name="binder">实现 <see cref="T:System.Reflection.Binder" /> 的对象，它包含与此方法相关的属性。</param>
    /// <param name="returnType">属性的类型。</param>
    /// <param name="types">用于从同名的重载方法中进行选择的数组。</param>
    /// <param name="modifiers">用于选择参数修饰符的数组。</param>
    PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

    /// <summary>检索与所有公共属性或当前类的所有属性对应的 <see cref="T:System.Reflection.PropertyInfo" /> 对象的数组。</summary>
    /// <returns>在反射对象上定义的所有属性的 <see cref="T:System.Reflection.PropertyInfo" /> 对象数组。</returns>
    /// <param name="bindingAttr">用于控制搜索的绑定属性。</param>
    PropertyInfo[] GetProperties(BindingFlags bindingAttr);

    /// <summary>检索与所有公共成员对应或者与匹配指定名称的所有成员对应的 <see cref="T:System.Reflection.MemberInfo" /> 对象的数组。</summary>
    /// <returns>与 <paramref name="name" /> 参数匹配的 <see cref="T:System.Reflection.MemberInfo" /> 对象的数组。</returns>
    /// <param name="name">要查找的成员的名称。</param>
    /// <param name="bindingAttr">用于控制搜索的绑定属性。</param>
    MemberInfo[] GetMember(string name, BindingFlags bindingAttr);

    /// <summary>检索与所有公共成员或当前类的所有成员对应的 <see cref="T:System.Reflection.MemberInfo" /> 对象的数组。</summary>
    /// <returns>包含此反射对象的所有成员信息的 <see cref="T:System.Reflection.MemberInfo" /> 对象数组。</returns>
    /// <param name="bindingAttr">用于控制搜索的绑定属性。</param>
    MemberInfo[] GetMembers(BindingFlags bindingAttr);

    /// <summary>调用指定的成员。</summary>
    /// <returns>指定的成员。</returns>
    /// <param name="name">要查找的成员的名称。</param>
    /// <param name="invokeAttr">
    /// <see cref="T:System.Reflection.BindingFlags" /> 调用特性之一。<paramref name="invokeAttr" /> 参数可以是构造函数、方法、属性或字段。必须指定合适的调用属性。通过将空字符串 ("") 作为成员的名称传递来调用类的默认成员。</param>
    /// <param name="binder">
    /// <see cref="T:System.Reflection.BindingFlags" /> 位标志之一。实现 <see cref="T:System.Reflection.Binder" />，包含与此方法相关的属性。</param>
    /// <param name="target">对其调用指定成员的对象。对于静态成员，此参数被忽略。</param>
    /// <param name="args">包含要调用的成员的参数数目、顺序和类型的对象数组。如果没有参数，则这是一个空数组。</param>
    /// <param name="modifiers">
    /// <see cref="T:System.Reflection.ParameterModifier" /> 对象的数组。此数组与表示元数据中被调用成员的参数属性的 <paramref name="args" /> 参数具有相同的长度。参数可以有下列属性：pdIn、pdOut、pdRetval、pdOptional 和 pdHasDefault。这些属性分别表示 [In]、[Out]、[retval]、[optional] 和默认参数。这些属性由不同的互操作性服务使用。</param>
    /// <param name="culture">用于控制类型强制的 <see cref="T:System.Globalization.CultureInfo" /> 的实例。例如，<paramref name="culture" /> 将表示 1000 的 String 转换为 Double 值，因为不同的区域性以不同的方式表示 1000。如果此参数为 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    /// <param name="namedParameters">参数的 String 数组。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="invokeAttr" /> 是 <see cref="F:System.Reflection.BindingFlags.CreateInstance" /> 并且还设置了另一个位标志。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="invokeAttr" /> 不是 <see cref="F:System.Reflection.BindingFlags.CreateInstance" />，且 <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="invokeAttr" /> 不是来自 <see cref="T:System.Reflection.BindingFlags" /> 的调用属性。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="invokeAttr" /> 为属性或字段同时指定 get 和 set。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="invokeAttr" /> 同时指定字段 set 和 Invoke 方法。为字段 get 提供了 <paramref name="args" />。</exception>
    /// <exception cref="T:System.ArgumentException">为字段 set 指定了多个参数。</exception>
    /// <exception cref="T:System.MissingFieldException">找不到该字段或属性。</exception>
    /// <exception cref="T:System.MissingMethodException">找不到此方法。</exception>
    /// <exception cref="T:System.Security.SecurityException">在没有所需 <see cref="T:System.Security.Permissions.ReflectionPermission" /> 的情况下调用私有成员。</exception>
    object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);
  }
}
