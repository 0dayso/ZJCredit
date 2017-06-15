// Decompiled with JetBrains decompiler
// Type: System.Reflection.ConstructorInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection
{
  /// <summary>发现类构造函数的属性并提供对构造函数元数据的访问权。</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_ConstructorInfo))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
  public abstract class ConstructorInfo : MethodBase, _ConstructorInfo
  {
    /// <summary>表示存储在元数据中的类构造函数方法的名称。该名称始终为“.ctor”。此字段为只读。</summary>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public static readonly string ConstructorName = ".ctor";
    /// <summary>表示存储于元数据中的类型构造函数方法的名称。该名称始终为“.cctor”。此属性为只读。</summary>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public static readonly string TypeConstructorName = ".cctor";

    /// <summary>获取一个 <see cref="T:System.Reflection.MemberTypes" /> 值，该值指示此成员是构造函数。</summary>
    /// <returns>指示此成员是构造函数的 <see cref="T:System.Reflection.MemberTypes" /> 对象。</returns>
    [ComVisible(true)]
    public override MemberTypes MemberType
    {
      get
      {
        return MemberTypes.Constructor;
      }
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.ConstructorInfo" /> 对象是否相等。</summary>
    /// <returns>如果 <paramref name="left" /> 等于 <paramref name="right" />，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个 <see cref="T:System.Reflection.ConstructorInfo" />。</param>
    /// <param name="right">要比较的第二个 <see cref="T:System.Reflection.ConstructorInfo" />。</param>
    [__DynamicallyInvokable]
    public static bool operator ==(ConstructorInfo left, ConstructorInfo right)
    {
      if (left == right)
        return true;
      if (left == null || right == null || (left is RuntimeConstructorInfo || right is RuntimeConstructorInfo))
        return false;
      return left.Equals((object) right);
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.ConstructorInfo" /> 对象是否不相等。</summary>
    /// <returns>如果 <paramref name="left" /> 不等于 <paramref name="right" />，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个 <see cref="T:System.Reflection.ConstructorInfo" />。</param>
    /// <param name="right">要比较的第二个 <see cref="T:System.Reflection.ConstructorInfo" />。</param>
    [__DynamicallyInvokable]
    public static bool operator !=(ConstructorInfo left, ConstructorInfo right)
    {
      return !(left == right);
    }

    /// <summary>返回一个值，该值指示此实例是否与指定的对象相等。</summary>
    /// <returns>如果 <paramref name="obj" /> 等于此实例的类型和值，则为 true；否则为 false。</returns>
    /// <param name="obj">与此实例进行比较的 object，或 null。</param>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    internal virtual Type GetReturnType()
    {
      throw new NotImplementedException();
    }

    /// <summary>在派生类中实现时，在指定 Binder 的约束下调用具有指定参数的此 ConstructorInfo 所反映的构造函数。</summary>
    /// <returns>与构造函数关联的类的实例。</returns>
    /// <param name="invokeAttr">指定绑定类型的 BindingFlags 值之一。</param>
    /// <param name="binder">一个 Binder，它定义一组属性并通过反映来启用绑定、参数类型强制和成员调用。如果 <paramref name="binder" /> 为 null，则使用 Binder.DefaultBinding。</param>
    /// <param name="parameters">一组 Object 类型，该类型用于在 <paramref name="binder" /> 的约束下匹配此构造函数的参数的个数、顺序和类型。如果此构造函数不需要参数，则像 Object[] parameters = new Object[0] 中那样传递一个包含零元素的数组。如果此数组中的对象未用值来显式初始化，则该对象将包含该对象类型的默认值。对于引用类型的元素，该值为 null。对于值类型的元素，该值为 0、0.0 或 false，具体取决于特定的元素类型。</param>
    /// <param name="culture">用于控制类型强制的 <see cref="T:System.Globalization.CultureInfo" />。如果这是 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    /// <exception cref="T:System.ArgumentException">在 <paramref name="binder" /> 的约束下，<paramref name="parameters" /> 数组不包含与此构造函数所接受的类型相匹配的值。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">调用的构造函数引发异常。</exception>
    /// <exception cref="T:System.Reflection.TargetParameterCountException">传递的参数个数不正确。</exception>
    /// <exception cref="T:System.NotSupportedException">不支持创建 <see cref="T:System.TypedReference" />、<see cref="T:System.ArgIterator" /> 和 <see cref="T:System.RuntimeArgumentHandle" /> 类型。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方不具有所需的代码访问权限。</exception>
    /// <exception cref="T:System.MemberAccessException">此类是抽象类。- 或 -构造函数是类初始值设定项。</exception>
    /// <exception cref="T:System.MethodAccessException">构造函数是私有的或受保护的，而且调用方不具有 <see cref="F:System.Security.Permissions.ReflectionPermissionFlag.MemberAccess" />。</exception>
    public abstract object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

    /// <summary>调用具有指定参数的实例所反映的构造函数，并为不常用的参数提供默认值。</summary>
    /// <returns>与构造函数关联的类的实例。</returns>
    /// <param name="parameters">与此构造函数的参数的个数、顺序和类型（受默认联编程序的约束）相匹配的值数组。如果此构造函数没有参数，则像 Object[] parameters = new Object[0] 中那样，使用包含零元素或 null 的数组。如果此数组中的对象未用值来显式初始化，则该对象将包含该对象类型的默认值。对于引用类型的元素，该值为 null。对于值类型的元素，该值为 0、0.0 或 false，具体取决于特定的元素类型。</param>
    /// <exception cref="T:System.MemberAccessException">此类是抽象类。- 或 -构造函数是类初始值设定项。</exception>
    /// <exception cref="T:System.MethodAccessException">在 .NET for Windows Store 应用程序 或 可移植类库 中，请改为捕获基类异常 <see cref="T:System.MemberAccessException" />。构造函数是私有的或受保护的，而且调用方不具有 <see cref="F:System.Security.Permissions.ReflectionPermissionFlag.MemberAccess" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="parameters" /> 数组不包含与此构造函数所接受的类型相匹配的值。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">调用的构造函数引发异常。</exception>
    /// <exception cref="T:System.Reflection.TargetParameterCountException">传递的参数个数不正确。</exception>
    /// <exception cref="T:System.NotSupportedException">不支持创建 <see cref="T:System.TypedReference" />、<see cref="T:System.ArgIterator" /> 和 <see cref="T:System.RuntimeArgumentHandle" /> 类型。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方不具有所需的代码访问权限。</exception>
    [DebuggerStepThrough]
    [DebuggerHidden]
    [__DynamicallyInvokable]
    public object Invoke(object[] parameters)
    {
      return this.Invoke(BindingFlags.Default, (Binder) null, parameters, (CultureInfo) null);
    }

    Type _ConstructorInfo.GetType()
    {
      return this.GetType();
    }

    object _ConstructorInfo.Invoke_2(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
    {
      return this.Invoke(obj, invokeAttr, binder, parameters, culture);
    }

    object _ConstructorInfo.Invoke_3(object obj, object[] parameters)
    {
      return this.Invoke(obj, parameters);
    }

    object _ConstructorInfo.Invoke_4(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
    {
      return this.Invoke(invokeAttr, binder, parameters, culture);
    }

    object _ConstructorInfo.Invoke_5(object[] parameters)
    {
      return this.Invoke(parameters);
    }

    void _ConstructorInfo.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _ConstructorInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _ConstructorInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _ConstructorInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
