// Decompiled with JetBrains decompiler
// Type: System.Reflection.MethodBase
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.Reflection
{
  /// <summary>提供有关方法和构造函数的信息。</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_MethodBase))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
  public abstract class MethodBase : MemberInfo, _MethodBase
  {
    internal virtual bool IsDynamicallyInvokable
    {
      [__DynamicallyInvokable] get
      {
        return true;
      }
    }

    /// <summary>获取指定方法实现特性的 <see cref="T:System.Reflection.MethodImplAttributes" /> 标志。</summary>
    /// <returns>方法实现标志。</returns>
    [__DynamicallyInvokable]
    public virtual MethodImplAttributes MethodImplementationFlags
    {
      [__DynamicallyInvokable] get
      {
        return this.GetMethodImplementationFlags();
      }
    }

    /// <summary>获取方法的内部元数据表示形式的句柄。</summary>
    /// <returns>
    /// <see cref="T:System.RuntimeMethodHandle" /> 对象。</returns>
    [__DynamicallyInvokable]
    public abstract RuntimeMethodHandle MethodHandle { [__DynamicallyInvokable] get; }

    /// <summary>获取与此方法关联的属性。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.MethodAttributes" /> 值之一。</returns>
    [__DynamicallyInvokable]
    public abstract MethodAttributes Attributes { [__DynamicallyInvokable] get; }

    /// <summary>获取一个值，该值指示此方法的调用约定。</summary>
    /// <returns>此方法的 <see cref="T:System.Reflection.CallingConventions" />。</returns>
    [__DynamicallyInvokable]
    public virtual CallingConventions CallingConvention
    {
      [__DynamicallyInvokable] get
      {
        return CallingConventions.Standard;
      }
    }

    /// <summary>获取一个值，该值指示方法是否为泛型方法定义。</summary>
    /// <returns>如果当前 <see cref="T:System.Reflection.MethodBase" /> 对象表示泛型方法的定义，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public virtual bool IsGenericMethodDefinition
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    /// <summary>获取一个值，该值指示泛型方法是否包含未赋值的泛型类型参数。</summary>
    /// <returns>如果当前 <see cref="T:System.Reflection.MethodBase" /> 对象表示的泛型方法包含未赋值的泛型类型参数，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public virtual bool ContainsGenericParameters
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    /// <summary>获取一个值，该值指示方法是否为泛型方法。</summary>
    /// <returns>如果当前 <see cref="T:System.Reflection.MethodBase" /> 表示泛型方法，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public virtual bool IsGenericMethod
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    /// <summary>获取一个值，该值指示当前方法或构造函数在当前信任级别上是安全关键的还是安全可靠关键的，因此可以执行关键操作。</summary>
    /// <returns>如果当前方法或构造函数在当前信任级别上是安全关键的或安全可靠关键的，则为 true；如果它是透明的，则为 false。</returns>
    public virtual bool IsSecurityCritical
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>获取一个值，该值指示当前方法或构造函数在当前信任级别上是安全可靠关键的；即它是否可以执行关键操作并可以由透明代码访问。</summary>
    /// <returns>如果方法或构造函数在当前信任级别上是安全可靠关键的，则为 true；如果它是安全关键的或透明的，则为 false。</returns>
    public virtual bool IsSecuritySafeCritical
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>获取一个值，该值指示当前方法或构造函数在当前信任级别上是透明的，因此无法执行关键操作。</summary>
    /// <returns>如果方法或构造函数在当前信任级别上是安全透明的，则为 true；否则为 false。</returns>
    public virtual bool IsSecurityTransparent
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>获取一个值，该值指示这是否是一个公共方法。</summary>
    /// <returns>如果此方法是公共的，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsPublic
    {
      [__DynamicallyInvokable] get
      {
        return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public;
      }
    }

    /// <summary>获取一个值，该值指示此成员是否是私有的。</summary>
    /// <returns>如果对此方法的访问只限于该类本身的其他成员，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsPrivate
    {
      [__DynamicallyInvokable] get
      {
        return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Private;
      }
    }

    /// <summary>获取一个值，该值指示此方法或构造函数的可见性是否由 <see cref="F:System.Reflection.MethodAttributes.Family" /> 描述；也就是说，此方法或构造函数仅在其类和派生类内可见。</summary>
    /// <returns>如果对此方法或构造函数的访问由 <see cref="F:System.Reflection.MethodAttributes.Family" /> 准确描述，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsFamily
    {
      [__DynamicallyInvokable] get
      {
        return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Family;
      }
    }

    /// <summary>获取一个值，该值指示此方法或构造函数的潜在可见性是否由 <see cref="F:System.Reflection.MethodAttributes.Assembly" /> 描述；也就是说，此方法或构造函数只对同一程序集中的其他类型可见，而对该程序集以外的派生类型则不可见。</summary>
    /// <returns>如果此方法或构造函数的可见性由 <see cref="F:System.Reflection.MethodAttributes.Assembly" /> 准确描述，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsAssembly
    {
      [__DynamicallyInvokable] get
      {
        return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Assembly;
      }
    }

    /// <summary>获取一个值，该值指示此方法或构造函数的可见性是否由 <see cref="F:System.Reflection.MethodAttributes.FamANDAssem" /> 描述；也就是说，此方法或构造函数可由派生类调用，但仅当这些派生类在同一程序集中时。</summary>
    /// <returns>如果对此方法或构造函数的访问由 <see cref="F:System.Reflection.MethodAttributes.FamANDAssem" /> 准确描述，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsFamilyAndAssembly
    {
      [__DynamicallyInvokable] get
      {
        return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.FamANDAssem;
      }
    }

    /// <summary>获取一个值，该值指示此方法或构造函数的潜在可见性是否由 <see cref="F:System.Reflection.MethodAttributes.FamORAssem" /> 描述；也就是说，此方法或构造函数可由派生类（无论其位置如何）和同一程序集中的类调用。</summary>
    /// <returns>如果对此方法或构造函数的访问由 <see cref="F:System.Reflection.MethodAttributes.FamORAssem" /> 准确描述，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsFamilyOrAssembly
    {
      [__DynamicallyInvokable] get
      {
        return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.FamORAssem;
      }
    }

    /// <summary>获取一个值，该值指示方法是否为 static。</summary>
    /// <returns>如果此方法为 static，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsStatic
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & MethodAttributes.Static) > 0U;
      }
    }

    /// <summary>获取一个值，该值指示此方法是否为 final。</summary>
    /// <returns>如果此方法是 final 方法，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsFinal
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & MethodAttributes.Final) > 0U;
      }
    }

    /// <summary>获取一个值，该值指示方法是否为 virtual。</summary>
    /// <returns>如果此方法为 virtual，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsVirtual
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & MethodAttributes.Virtual) > 0U;
      }
    }

    /// <summary>获取一个值，该值指示是否只有一个签名完全相同的同一种类的成员在派生类中是隐藏的。</summary>
    /// <returns>如果此成员被签名隐藏，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsHideBySig
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & MethodAttributes.HideBySig) > 0U;
      }
    }

    /// <summary>获取一个值，该值指示此方法是否为抽象方法。</summary>
    /// <returns>如果该方法是抽象的，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsAbstract
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & MethodAttributes.Abstract) > 0U;
      }
    }

    /// <summary>获取一个值，该值指示此方法是否具有特殊名称。</summary>
    /// <returns>如果此方法具有特殊名称，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsSpecialName
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & MethodAttributes.SpecialName) > 0U;
      }
    }

    /// <summary>获取一个值，该值指示此方法是否为构造函数。</summary>
    /// <returns>如果此方法是 <see cref="T:System.Reflection.ConstructorInfo" /> 对象（参见"备注"中有关 <see cref="T:System.Reflection.Emit.ConstructorBuilder" /> 对象的说明）所表示的构造函数，则为 true；否则为 false。</returns>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public bool IsConstructor
    {
      [__DynamicallyInvokable] get
      {
        if (this is ConstructorInfo && !this.IsStatic)
          return (this.Attributes & MethodAttributes.RTSpecialName) == MethodAttributes.RTSpecialName;
        return false;
      }
    }

    internal string FullName
    {
      get
      {
        return string.Format("{0}.{1}", (object) this.DeclaringType.FullName, (object) this.FormatNameAndSig());
      }
    }

    bool _MethodBase.IsPublic
    {
      get
      {
        return this.IsPublic;
      }
    }

    bool _MethodBase.IsPrivate
    {
      get
      {
        return this.IsPrivate;
      }
    }

    bool _MethodBase.IsFamily
    {
      get
      {
        return this.IsFamily;
      }
    }

    bool _MethodBase.IsAssembly
    {
      get
      {
        return this.IsAssembly;
      }
    }

    bool _MethodBase.IsFamilyAndAssembly
    {
      get
      {
        return this.IsFamilyAndAssembly;
      }
    }

    bool _MethodBase.IsFamilyOrAssembly
    {
      get
      {
        return this.IsFamilyOrAssembly;
      }
    }

    bool _MethodBase.IsStatic
    {
      get
      {
        return this.IsStatic;
      }
    }

    bool _MethodBase.IsFinal
    {
      get
      {
        return this.IsFinal;
      }
    }

    bool _MethodBase.IsVirtual
    {
      get
      {
        return this.IsVirtual;
      }
    }

    bool _MethodBase.IsHideBySig
    {
      get
      {
        return this.IsHideBySig;
      }
    }

    bool _MethodBase.IsAbstract
    {
      get
      {
        return this.IsAbstract;
      }
    }

    bool _MethodBase.IsSpecialName
    {
      get
      {
        return this.IsSpecialName;
      }
    }

    bool _MethodBase.IsConstructor
    {
      get
      {
        return this.IsConstructor;
      }
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.MethodBase" /> 对象是否相等。</summary>
    /// <returns>如果 <paramref name="left" /> 等于 <paramref name="right" />，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个对象。</param>
    /// <param name="right">要比较的第二个对象。</param>
    [__DynamicallyInvokable]
    public static bool operator ==(MethodBase left, MethodBase right)
    {
      if (left == right)
        return true;
      if (left == null || right == null)
        return false;
      MethodInfo methodInfo1;
      MethodInfo methodInfo2;
      if ((methodInfo1 = left as MethodInfo) != (MethodInfo) null && (methodInfo2 = right as MethodInfo) != (MethodInfo) null)
        return methodInfo1 == methodInfo2;
      ConstructorInfo constructorInfo1;
      ConstructorInfo constructorInfo2;
      if ((constructorInfo1 = left as ConstructorInfo) != (ConstructorInfo) null && (constructorInfo2 = right as ConstructorInfo) != (ConstructorInfo) null)
        return constructorInfo1 == constructorInfo2;
      return false;
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.MethodBase" /> 对象是否不相等。</summary>
    /// <returns>如果 <paramref name="left" /> 不等于 <paramref name="right" />，则为 true；否则为 false。</returns>
    /// <param name="left">要比较的第一个对象。</param>
    /// <param name="right">要比较的第二个对象。</param>
    [__DynamicallyInvokable]
    public static bool operator !=(MethodBase left, MethodBase right)
    {
      return !(left == right);
    }

    /// <summary>通过使用方法的内部元数据表示形式（句柄）获取方法信息。</summary>
    /// <returns>MethodBase，包含方法的有关信息。</returns>
    /// <param name="handle">方法的句柄。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="handle" /> 无效。</exception>
    [__DynamicallyInvokable]
    public static MethodBase GetMethodFromHandle(RuntimeMethodHandle handle)
    {
      if (handle.IsNullHandle())
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHandle"));
      MethodBase methodBase = RuntimeType.GetMethodBase(handle.GetMethodInfo());
      Type declaringType = methodBase.DeclaringType;
      if (declaringType != (Type) null && declaringType.IsGenericType)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_MethodDeclaringTypeGeneric"), (object) methodBase, (object) declaringType.GetGenericTypeDefinition()));
      return methodBase;
    }

    /// <summary>以指定泛型类型，获取指定句柄所表示的构造函数或方法的 <see cref="T:System.Reflection.MethodBase" /> 对象。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.MethodBase" /> 对象，表示由 <paramref name="handle" /> 指定的方法或构造函数，为由 <paramref name="declaringType" /> 指定的泛型类型。</returns>
    /// <param name="handle">构造函数或方法的内部元数据表示形式的句柄。</param>
    /// <param name="declaringType">定义构造函数或方法的泛型类型的句柄。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="handle" /> 无效。</exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public static MethodBase GetMethodFromHandle(RuntimeMethodHandle handle, RuntimeTypeHandle declaringType)
    {
      if (handle.IsNullHandle())
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHandle"));
      return RuntimeType.GetMethodBase(declaringType.GetRuntimeType(), handle.GetMethodInfo());
    }

    /// <summary>返回表示当前正在执行的方法的 MethodBase 对象。</summary>
    /// <returns>表示当前正在执行的方法的 MethodBase 对象。</returns>
    /// <exception cref="T:System.Reflection.TargetException">用后期绑定机制调用该成员。</exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static MethodBase GetCurrentMethod()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return RuntimeMethodInfo.InternalGetCurrentMethod(ref stackMark);
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

    [SecurityCritical]
    private IntPtr GetMethodDesc()
    {
      return this.MethodHandle.Value;
    }

    internal virtual ParameterInfo[] GetParametersNoCopy()
    {
      return this.GetParameters();
    }

    /// <summary>当在派生类中重写时，获取指定的方法或构造函数的参数。</summary>
    /// <returns>ParameterInfo 类型的数组，包含与此 MethodBase 实例所反射的方法（或构造函数）的签名匹配的信息。</returns>
    [__DynamicallyInvokable]
    public abstract ParameterInfo[] GetParameters();

    /// <summary>当在派生类中重写时，返回 <see cref="T:System.Reflection.MethodImplAttributes" /> 标志。</summary>
    /// <returns>MethodImplAttributes 标志。</returns>
    public abstract MethodImplAttributes GetMethodImplementationFlags();

    /// <summary>当在派生类中重写时，调用具有给定参数的反射的方法或构造函数。</summary>
    /// <returns>Object，包含被调用方法的返回值；如果调用的是构造函数，则为 null；如果方法的返回类型是 void，则为 null。在调用方法或构造函数之前，Invoke 检查用户是否有访问权限并验证参数是否有效。警告也可以修改表示用 ref 或 out 关键字声明的参数的 <paramref name="parameters" /> 数组元素。</returns>
    /// <param name="obj">对其调用方法或构造函数的对象。如果方法是静态的，则忽略此参数。如果构造函数是静态的，则此参数必须为 null 或定义该构造函数的类的实例。</param>
    /// <param name="invokeAttr">位屏蔽，它是 <see cref="T:System.Reflection.BindingFlags" /> 的 0 个或多个位标志的组合。如果 <paramref name="binder" /> 为 null，则此参数赋值为 <see cref="F:System.Reflection.BindingFlags.Default" />；因此，传入的任何值都被忽略。</param>
    /// <param name="binder">一个启用绑定、参数类型强制、成员调用以及通过反射进行 MemberInfo 对象检索的对象。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="parameters">调用的方法或构造函数的参数列表。这是一个对象数组，这些对象与要调用的方法或构造函数的参数具有相同的数量、顺序和类型。如果没有参数，则此应为 null。如果此实例表示的方法或构造函数采用 ByRef 参数，那么使用此函数调用该方法或构造函数时，对于该参数不需要特殊的特性。如果此数组中的对象未用值来显式初始化，则该对象将包含该对象类型的默认值。对于引用类型的元素，该值为 null。对于值类型的元素，该值为 0、0.0 或 false，具体取决于特定的元素类型。</param>
    /// <param name="culture">用于控制类型强制的 CultureInfo 的实例。如果这是 null，则使用当前线程的 CultureInfo。（例如，这对于将表示 1000 的 String 转换为 Double 值是必需的，因为不同的区域性以不同的方式表示 1000。）</param>
    /// <exception cref="T:System.Reflection.TargetException">
    /// <paramref name="obj" /> 参数为 null 并且此方法不是静态的。- 或 -<paramref name="obj" /> 的类既不声明也不继承此方法。- 或 -调用了静态构造函数，并且 <paramref name="obj" /> 既不是 null 也不是声明该构造函数的类的实例。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="parameters" /> 参数的类型与此实例所反射的方法或构造函数的签名不匹配。</exception>
    /// <exception cref="T:System.Reflection.TargetParameterCountException">
    /// <paramref name="parameters" /> 数组的参数数目不正确。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">调用的方法或构造函数引发异常。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方无权执行由当前实例表示的方法或构造函数。</exception>
    /// <exception cref="T:System.InvalidOperationException">声明此方法的类型是开放式泛型类型。即，<see cref="P:System.Type.ContainsGenericParameters" /> 属性为声明类型返回 true。</exception>
    public abstract object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

    /// <summary>返回 <see cref="T:System.Type" /> 对象的数组，这些对象表示泛型方法的类型实参或泛型方法定义的类型形参。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 对象的数组，这些对象表示泛型方法的类型变量或泛型方法定义的类型参数。如果当前方法不是泛型方法，则返回空数组。</returns>
    /// <exception cref="T:System.NotSupportedException">当前对象是 <see cref="T:System.Reflection.ConstructorInfo" />。.NET Framework 2.0 版不支持泛型构造函数。如果派生类未重写此方法，此异常即为默认行为。</exception>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public virtual Type[] GetGenericArguments()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    /// <summary>使用指定的参数调用当前实例所表示的方法或构造函数。</summary>
    /// <returns>一个对象，包含被调用方法的返回值，如果调用的是构造函数，则为 null。警告也可以修改表示用 ref 或 out 关键字声明的参数的 <paramref name="parameters" /> 数组元素。</returns>
    /// <param name="obj">对其调用方法或构造函数的对象。如果方法是静态的，则忽略此参数。如果构造函数是静态的，则此参数必须为 null 或定义该构造函数的类的实例。</param>
    /// <param name="parameters">调用的方法或构造函数的参数列表。这是一个对象数组，这些对象与要调用的方法或构造函数的参数具有相同的数量、顺序和类型。如果没有任何参数，则 <paramref name="parameters" /> 应为 null。如果此实例所表示的方法或构造函数采用 ref 参数（在 Visual Basic 中为 ByRef），使用此函数调用该方法或构造函数时，该参数不需要任何特殊属性。如果此数组中的对象未用值来显式初始化，则该对象将包含该对象类型的默认值。对于引用类型的元素，该值为 null。对于值类型的元素，该值为 0、0.0 或 false，具体取决于特定的元素类型。</param>
    /// <exception cref="T:System.Reflection.TargetException">在 .NET for Windows Store 应用程序 或 可移植类库 中，请改为捕获 <see cref="T:System.Exception" />。<paramref name="obj" /> 参数为 null 并且此方法不是静态的。- 或 -<paramref name="obj" /> 的类既不声明也不继承此方法。- 或 -调用了静态构造函数，并且 <paramref name="obj" /> 既不是 null 也不是声明该构造函数的类的实例。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="parameters" /> 数组的元素与此实例所反射的方法或构造函数的签名不匹配。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">调用的方法或构造函数引发异常。- 或 -当前实例是包含不可验证代码的 <see cref="T:System.Reflection.Emit.DynamicMethod" />。请参见 <see cref="T:System.Reflection.Emit.DynamicMethod" /> 的备注中的“验证”一节。</exception>
    /// <exception cref="T:System.Reflection.TargetParameterCountException">
    /// <paramref name="parameters" /> 数组的参数数目不正确。</exception>
    /// <exception cref="T:System.MethodAccessException">在 .NET for Windows Store 应用程序 或 可移植类库 中，请改为捕获基类异常 <see cref="T:System.MemberAccessException" />。调用方无权执行由当前实例表示的方法或构造函数。</exception>
    /// <exception cref="T:System.InvalidOperationException">声明此方法的类型是开放式泛型类型。即，<see cref="P:System.Type.ContainsGenericParameters" /> 属性为声明类型返回 true。</exception>
    /// <exception cref="T:System.NotSupportedException">当前实例等于 <see cref="T:System.Reflection.Emit.MethodBuilder" />。</exception>
    [DebuggerStepThrough]
    [DebuggerHidden]
    [__DynamicallyInvokable]
    public object Invoke(object obj, object[] parameters)
    {
      return this.Invoke(obj, BindingFlags.Default, (Binder) null, parameters, (CultureInfo) null);
    }

    /// <summary>在派生类中重写后，获取 <see cref="T:System.Reflection.MethodBody" /> 对象，该对象提供对 MSIL 流、局部变量和当前方法的异常的访问。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.MethodBody" /> 对象，提供对 MSIL 流、局部变量和当前方法的异常的访问。</returns>
    /// <exception cref="T:System.InvalidOperationException">除非在派生类中重写，否则此方法无效。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [ReflectionPermission(SecurityAction.Demand, Flags = ReflectionPermissionFlag.MemberAccess)]
    public virtual MethodBody GetMethodBody()
    {
      throw new InvalidOperationException();
    }

    internal static string ConstructParameters(Type[] parameterTypes, CallingConventions callingConvention, bool serialization)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string str1 = "";
      for (int index = 0; index < parameterTypes.Length; ++index)
      {
        Type type = parameterTypes[index];
        stringBuilder.Append(str1);
        int num = serialization ? 1 : 0;
        string str2 = type.FormatTypeName(num != 0);
        if (type.IsByRef && !serialization)
        {
          stringBuilder.Append(str2.TrimEnd('&'));
          stringBuilder.Append(" ByRef");
        }
        else
          stringBuilder.Append(str2);
        str1 = ", ";
      }
      if ((callingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs)
      {
        stringBuilder.Append(str1);
        stringBuilder.Append("...");
      }
      return stringBuilder.ToString();
    }

    internal string FormatNameAndSig()
    {
      return this.FormatNameAndSig(false);
    }

    internal virtual string FormatNameAndSig(bool serialization)
    {
      StringBuilder stringBuilder = new StringBuilder(this.Name);
      string str1 = "(";
      stringBuilder.Append(str1);
      string str2 = MethodBase.ConstructParameters(this.GetParameterTypes(), this.CallingConvention, serialization);
      stringBuilder.Append(str2);
      string str3 = ")";
      stringBuilder.Append(str3);
      return stringBuilder.ToString();
    }

    internal virtual Type[] GetParameterTypes()
    {
      ParameterInfo[] parametersNoCopy = this.GetParametersNoCopy();
      Type[] typeArray = new Type[parametersNoCopy.Length];
      for (int index = 0; index < parametersNoCopy.Length; ++index)
        typeArray[index] = parametersNoCopy[index].ParameterType;
      return typeArray;
    }

    [SecuritySafeCritical]
    internal object[] CheckArguments(object[] parameters, Binder binder, BindingFlags invokeAttr, CultureInfo culture, Signature sig)
    {
      object[] objArray = new object[parameters.Length];
      ParameterInfo[] parameterInfoArray = (ParameterInfo[]) null;
      for (int index = 0; index < parameters.Length; ++index)
      {
        object obj = parameters[index];
        RuntimeType runtimeType = sig.Arguments[index];
        if (obj == Type.Missing)
        {
          if (parameterInfoArray == null)
            parameterInfoArray = this.GetParametersNoCopy();
          if (parameterInfoArray[index].DefaultValue == DBNull.Value)
            throw new ArgumentException(Environment.GetResourceString("Arg_VarMissNull"), "parameters");
          obj = parameterInfoArray[index].DefaultValue;
        }
        objArray[index] = runtimeType.CheckValue(obj, binder, culture, invokeAttr);
      }
      return objArray;
    }

    Type _MethodBase.GetType()
    {
      return this.GetType();
    }

    void _MethodBase.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _MethodBase.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _MethodBase.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _MethodBase.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
