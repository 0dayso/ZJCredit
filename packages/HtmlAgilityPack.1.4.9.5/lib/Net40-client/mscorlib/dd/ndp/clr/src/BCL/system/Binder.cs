// Decompiled with JetBrains decompiler
// Type: System.Reflection.Binder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>从候选者列表中选择一个成员，并执行实参类型到形参类型的类型转换。</summary>
  [ClassInterface(ClassInterfaceType.AutoDual)]
  [ComVisible(true)]
  [Serializable]
  public abstract class Binder
  {
    /// <summary>基于提供的参数，从给定的方法集中选择要调用的方法。</summary>
    /// <returns>匹配的方法。</returns>
    /// <param name="bindingAttr">
    /// <see cref="T:System.Reflection.BindingFlags" /> 值的按位组合。</param>
    /// <param name="match">用于匹配的候选方法集。例如，当 <see cref="Overload:System.Type.InvokeMember" /> 使用 <see cref="T:System.Reflection.Binder" /> 对象时，此参数将指定反射已确定为可能匹配项的方法集，通常是因为它们有正确的成员名称。由 <see cref="P:System.Type.DefaultBinder" /> 提供的默认实现会更改此数组的顺序。</param>
    /// <param name="args">传入的参数。联编程序可以更改此数组中的参数的顺序；例如，如果 <paramref name="names" /> 参数用于指定位置顺序以外的顺序，则默认联编程序会更改参数的顺序。如果联编程序实现强制转换参数类型，则也可以更改参数的类型和值。</param>
    /// <param name="modifiers">使绑定能够处理在其中修改了类型的参数签名的参数修饰符数组。默认的联编程序实现不使用此参数。</param>
    /// <param name="culture">一个 <see cref="T:System.Globalization.CultureInfo" /> 实例，用于在强制类型的联编程序实现中控制数据类型强制。如果 <paramref name="culture" /> 为 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。注意   例如，如果联编程序实现允许将字符串值强制转换为数值类型，则此参数对于将表示 1000 的 String 转换为 Double 值是必需的，因为不同的区域性以不同的方式表示 1000。默认联编程序不进行此类字符串强制。</param>
    /// <param name="names">参数名（如果匹配时要考虑参数名）或 null（如果要将变量视为纯位置）。例如，如果没有按位置顺序提供变量，则必须使用参数名。</param>
    /// <param name="state">方法返回之后，<paramref name="state" /> 包含一个联编程序提供的对象，用于跟踪参数的重新排序。联编程序创建此对象，并且联编程序是此对象的唯一使用者。如果在返回 BindToMethod 时 <paramref name="state" /> 不为 null，若要将 <paramref name="args" /> 还原为其原始顺序，您必须将 <paramref name="state" /> 传递给 <see cref="M:System.Reflection.Binder.ReorderArgumentArray(System.Object[]@,System.Object)" /> 方法，以便可以检索 ref 参数（在 Visual Basic 中为 ByRef）的值。</param>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">对于默认联编程序，<paramref name="match" /> 包含多个与 <paramref name="args" /> 良好匹配且匹配程序相同的方法。例如，<paramref name="args" /> 包含一个实现 IMyClass 接口的 MyClass 对象，并且 <paramref name="match" /> 包含一个采用 MyClass 的方法和一个采用 IMyClass 的方法。</exception>
    /// <exception cref="T:System.MissingMethodException">对于默认联编程序，<paramref name="match" /> 不包含可以接受 <paramref name="args" /> 中提供的参数的任何方法。</exception>
    /// <exception cref="T:System.ArgumentException">对于默认联编程序，<paramref name="match" /> 为 null 或一个空数组。</exception>
    public abstract MethodBase BindToMethod(BindingFlags bindingAttr, MethodBase[] match, ref object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] names, out object state);

    /// <summary>基于指定的判据，从给定的字段集中选择一个字段。</summary>
    /// <returns>匹配的字段。</returns>
    /// <param name="bindingAttr">
    /// <see cref="T:System.Reflection.BindingFlags" /> 值的按位组合。</param>
    /// <param name="match">用于匹配的候选字段集。例如，当 <see cref="Overload:System.Type.InvokeMember" /> 使用 <see cref="T:System.Reflection.Binder" /> 对象时，此参数将指定反射已确定为可能匹配项的字段集，通常是因为它们有正确的成员名称。由 <see cref="P:System.Type.DefaultBinder" /> 提供的默认实现会更改此数组的顺序。</param>
    /// <param name="value">用于定位匹配字段的字段值。</param>
    /// <param name="culture">一个 <see cref="T:System.Globalization.CultureInfo" /> 实例，用于在强制类型的联编程序实现中控制数据类型强制。如果 <paramref name="culture" /> 为 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。注意   例如，如果联编程序实现允许将字符串值强制转换为数值类型，则此参数对于将表示 1000 的 String 转换为 Double 值是必需的，因为不同的区域性以不同的方式表示 1000。默认联编程序不进行此类字符串强制。</param>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">对于默认的联编程序，<paramref name="bindingAttr" /> 包括 <see cref="F:System.Reflection.BindingFlags.SetField" />，并且 <paramref name="match" /> 包含多个与 <paramref name="value" /> 良好匹配且匹配程度相同的字段。例如，<paramref name="value" /> 包含一个实现 IMyClass 接口的 MyClass 对象，并且 <paramref name="match" /> 包含一个类型为 MyClass 的字段和一个类型为 IMyClass 的字段。</exception>
    /// <exception cref="T:System.MissingFieldException">对于默认联编程序，<paramref name="bindingAttr" /> 包括 <see cref="F:System.Reflection.BindingFlags.SetField" />，并且 <paramref name="match" /> 不包含任何可接受 <paramref name="value" /> 的字段。</exception>
    /// <exception cref="T:System.NullReferenceException">对于默认联编程序，<paramref name="bindingAttr" /> 包括 <see cref="F:System.Reflection.BindingFlags.SetField" />，并且 <paramref name="match" /> 为 null 或一个空数组。- 或 -<paramref name="bindingAttr" /> 包括 <see cref="F:System.Reflection.BindingFlags.SetField" />，并且 <paramref name="value" /> 为 null。</exception>
    public abstract FieldInfo BindToField(BindingFlags bindingAttr, FieldInfo[] match, object value, CultureInfo culture);

    /// <summary>基于参数类型，从给定的方法集中选择一个方法。</summary>
    /// <returns>如果找到，则为匹配的方法；否则为 null。</returns>
    /// <param name="bindingAttr">
    /// <see cref="T:System.Reflection.BindingFlags" /> 值的按位组合。</param>
    /// <param name="match">用于匹配的候选方法集。例如，当 <see cref="Overload:System.Type.InvokeMember" /> 使用 <see cref="T:System.Reflection.Binder" /> 对象时，此参数将指定反射已确定为可能匹配项的方法集，通常是因为它们有正确的成员名称。由 <see cref="P:System.Type.DefaultBinder" /> 提供的默认实现会更改此数组的顺序。</param>
    /// <param name="types">用于定位匹配方法的参数类型。</param>
    /// <param name="modifiers">使绑定能够处理在其中修改了类型的参数签名的参数修饰符数组。</param>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">对于默认联编程序，<paramref name="match" /> 包含多个与由 <paramref name="types" /> 描述的参数类型良好匹配且匹配程序相同的方法。例如，<paramref name="types" /> 中的数组包含一个用于 MyClass 的 <see cref="T:System.Type" /> 对象，并且 <paramref name="match" /> 中的数组包含一个采用 MyClass 的基类的方法和一个采用 MyClass 实现的接口的方法。</exception>
    /// <exception cref="T:System.ArgumentException">对于默认联编程序，<paramref name="match" /> 为 null 或一个空数组。- 或 -<paramref name="types" /> 的元素从 <see cref="T:System.Type" /> 派生，但并不属于类型 RuntimeType。</exception>
    public abstract MethodBase SelectMethod(BindingFlags bindingAttr, MethodBase[] match, Type[] types, ParameterModifier[] modifiers);

    /// <summary>基于指定的判据，从给定的属性集中选择一个属性。</summary>
    /// <returns>匹配的属性。</returns>
    /// <param name="bindingAttr">
    /// <see cref="T:System.Reflection.BindingFlags" /> 值的按位组合。</param>
    /// <param name="match">用于匹配的候选属性集。例如，当 <see cref="Overload:System.Type.InvokeMember" /> 使用 <see cref="T:System.Reflection.Binder" /> 对象时，此参数将指定反射已确定为可能匹配项的属性集，通常是因为它们有正确的成员名称。由 <see cref="P:System.Type.DefaultBinder" /> 提供的默认实现会更改此数组的顺序。</param>
    /// <param name="returnType">匹配属性必须具有的返回值。</param>
    /// <param name="indexes">所搜索的属性的索引类型。用于索引属性，如类的索引器。</param>
    /// <param name="modifiers">使绑定能够处理在其中修改了类型的参数签名的参数修饰符数组。</param>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">对于默认联编程序，<paramref name="match" /> 包含多个与 <paramref name="returnType" /> 和 <paramref name="indexes" /> 良好匹配且匹配程序相同的属性。</exception>
    /// <exception cref="T:System.ArgumentException">对于默认联编程序，<paramref name="match" /> 为 null 或一个空数组。</exception>
    public abstract PropertyInfo SelectProperty(BindingFlags bindingAttr, PropertyInfo[] match, Type returnType, Type[] indexes, ParameterModifier[] modifiers);

    /// <summary>将给定 Object 的类型更改为给定 Type。</summary>
    /// <returns>一个包含作为新类型的给定值的对象。</returns>
    /// <param name="value">要更改为新 Type 的对象。</param>
    /// <param name="type">
    /// <paramref name="value" /> 将变成的新 Type。</param>
    /// <param name="culture">一个 <see cref="T:System.Globalization.CultureInfo" /> 实例，用于控制数据类型的强制转换。如果 <paramref name="culture" /> 为 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。注意   例如，此参数对于将表示 1000 的 String 转换为 Double 值是必需的，因为不同的区域性表示 1000 的形式不同。</param>
    public abstract object ChangeType(object value, Type type, CultureInfo culture);

    /// <summary>从 <see cref="M:System.Reflection.Binder.BindToMethod(System.Reflection.BindingFlags,System.Reflection.MethodBase[],System.Object[]@,System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[],System.Object@)" /> 返回后，将 <paramref name="args" /> 参数还原为从 BindToMethod 传入时的状态。</summary>
    /// <param name="args">传入的实参。参数的类型和值都可更改。</param>
    /// <param name="state">联编程序提供的对象，用于跟踪参数的重新排序。</param>
    public abstract void ReorderArgumentArray(ref object[] args, object state);
  }
}
