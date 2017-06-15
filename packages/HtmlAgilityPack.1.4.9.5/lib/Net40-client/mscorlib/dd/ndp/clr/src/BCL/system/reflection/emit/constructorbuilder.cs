// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.ConstructorBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
  /// <summary>定义并表示动态类的构造函数。</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_ConstructorBuilder))]
  [ComVisible(true)]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public sealed class ConstructorBuilder : ConstructorInfo, _ConstructorBuilder
  {
    private readonly MethodBuilder m_methodBuilder;
    internal bool m_isDefaultConstructor;

    internal int MetadataTokenInternal
    {
      get
      {
        return this.m_methodBuilder.MetadataTokenInternal;
      }
    }

    /// <summary>获取定义此构造函数的动态模块。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.Module" /> 对象，表示定义此构造函数的动态模块。</returns>
    public override Module Module
    {
      get
      {
        return this.m_methodBuilder.Module;
      }
    }

    /// <summary>保存对从中获取该对象的 <see cref="T:System.Type" /> 对象的引用。</summary>
    /// <returns>返回从中获取该对象的 Type 对象。</returns>
    public override Type ReflectedType
    {
      get
      {
        return this.m_methodBuilder.ReflectedType;
      }
    }

    /// <summary>检索对声明此成员的类型的 <see cref="T:System.Type" /> 对象的引用。</summary>
    /// <returns>返回声明此成员的类型的 <see cref="T:System.Type" /> 对象。</returns>
    public override Type DeclaringType
    {
      get
      {
        return this.m_methodBuilder.DeclaringType;
      }
    }

    /// <summary>检索此构造函数的名称。</summary>
    /// <returns>返回此构造函数的名称。</returns>
    public override string Name
    {
      get
      {
        return this.m_methodBuilder.Name;
      }
    }

    /// <summary>检索此构造函数的特性。</summary>
    /// <returns>返回此构造函数的特性。</returns>
    public override MethodAttributes Attributes
    {
      get
      {
        return this.m_methodBuilder.Attributes;
      }
    }

    /// <summary>检索此方法的内部句柄。使用此句柄访问基础元数据句柄。</summary>
    /// <returns>返回此方法的内部句柄。使用此句柄访问基础元数据句柄。</returns>
    /// <exception cref="T:System.NotSupportedException">在该类上不支持此属性。</exception>
    public override RuntimeMethodHandle MethodHandle
    {
      get
      {
        return this.m_methodBuilder.MethodHandle;
      }
    }

    /// <summary>获取一个 <see cref="T:System.Reflection.CallingConventions" /> 值，该值取决于声明类型是否为泛型。</summary>
    /// <returns>如果声明类型为泛型，则为 <see cref="F:System.Reflection.CallingConventions.HasThis" />；否则为 <see cref="F:System.Reflection.CallingConventions.Standard" />。</returns>
    public override CallingConventions CallingConvention
    {
      get
      {
        return this.DeclaringType.IsGenericType ? CallingConventions.HasThis : CallingConventions.Standard;
      }
    }

    /// <summary>获取 null。</summary>
    /// <returns>返回 null。</returns>
    [Obsolete("This property has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
    public Type ReturnType
    {
      get
      {
        return this.GetReturnType();
      }
    }

    /// <summary>检索字符串形式的字段的签名。</summary>
    /// <returns>返回字段的签名。</returns>
    public string Signature
    {
      get
      {
        return this.m_methodBuilder.Signature;
      }
    }

    /// <summary>获取或设置此构造函数中的局部变量是否应初始化为零。</summary>
    /// <returns>读/写。获取或设置此构造函数中的局部变量是否应初始化为零。</returns>
    public bool InitLocals
    {
      get
      {
        return this.m_methodBuilder.InitLocals;
      }
      set
      {
        this.m_methodBuilder.InitLocals = value;
      }
    }

    private ConstructorBuilder()
    {
    }

    [SecurityCritical]
    internal ConstructorBuilder(string name, MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers, ModuleBuilder mod, TypeBuilder type)
    {
      this.m_methodBuilder = new MethodBuilder(name, attributes, callingConvention, (Type) null, (Type[]) null, (Type[]) null, parameterTypes, requiredCustomModifiers, optionalCustomModifiers, mod, type, false);
      type.m_listMethods.Add(this.m_methodBuilder);
      int length;
      this.m_methodBuilder.GetMethodSignature().InternalGetSignature(out length);
      this.m_methodBuilder.GetToken();
    }

    [SecurityCritical]
    internal ConstructorBuilder(string name, MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, ModuleBuilder mod, TypeBuilder type)
      : this(name, attributes, callingConvention, parameterTypes, (Type[][]) null, (Type[][]) null, mod, type)
    {
    }

    internal override Type[] GetParameterTypes()
    {
      return this.m_methodBuilder.GetParameterTypes();
    }

    private TypeBuilder GetTypeBuilder()
    {
      return this.m_methodBuilder.GetTypeBuilder();
    }

    internal ModuleBuilder GetModuleBuilder()
    {
      return this.GetTypeBuilder().GetModuleBuilder();
    }

    /// <summary>以 <see cref="T:System.String" /> 形式返回此 <see cref="T:System.Reflection.Emit.ConstructorBuilder" /> 实例。</summary>
    /// <returns>返回 <see cref="T:System.String" />，它包含此构造函数的名称、特性和异常，后跟当前 Microsoft 中间语言 (MSIL) 流。</returns>
    public override string ToString()
    {
      return this.m_methodBuilder.ToString();
    }

    /// <summary>在指定 Binder 的约束下，用指定的参数动态调用此实例反映的构造函数。</summary>
    /// <returns>与构造函数关联的类的实例。</returns>
    /// <param name="obj">需要重新初始化的对象。</param>
    /// <param name="invokeAttr">指定所需绑定类型的 BindingFlags 值之一。</param>
    /// <param name="binder">一个 Binder，它定义一组属性并通过反映来启用绑定、参数类型强制和成员调用。如果 <paramref name="binder" /> 为 null，则使用 Binder.DefaultBinding。</param>
    /// <param name="parameters">参数列表。这是一个参数数组，这些参数与要调用的构造函数的参数具有相同的数量、顺序和类型。如果没有参数，则应为一个 null 引用（在 Visual Basic 中为 Nothing）。</param>
    /// <param name="culture">用于控制类型强制的 <see cref="T:System.Globalization.CultureInfo" />。如果这是 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    /// <exception cref="T:System.NotSupportedException">目前不支持此方法。可以使用 <see cref="M:System.Type.GetConstructor(System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> 检索构造函数，并对返回的 <see cref="T:System.Reflection.ConstructorInfo" /> 调用 <see cref="M:System.Reflection.ConstructorInfo.Invoke(System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo)" />。</exception>
    public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>返回此构造函数的参数。</summary>
    /// <returns>返回表示此构造函数的参数的 <see cref="T:System.Reflection.ParameterInfo" /> 对象数组。</returns>
    /// <exception cref="T:System.InvalidOperationException">在 .NET Framework 1.0 版和 1.1 版中，没有对此构造函数的类型调用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />。</exception>
    /// <exception cref="T:System.NotSupportedException">在 .NET Framework 2.0 版中，没有对此构造函数的类型调用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />。</exception>
    public override ParameterInfo[] GetParameters()
    {
      return this.GetTypeBuilder().GetConstructor(this.m_methodBuilder.m_parameterTypes).GetParameters();
    }

    /// <summary>返回此构造函数的方法实现标志。</summary>
    /// <returns>此构造函数的方法实现标志。</returns>
    public override MethodImplAttributes GetMethodImplementationFlags()
    {
      return this.m_methodBuilder.GetMethodImplementationFlags();
    }

    /// <summary>在给定联编程序的约束下，对给定的对象动态调用此实例所反射的构造函数，并传递指定的参数。</summary>
    /// <returns>返回 <see cref="T:System.Object" />，它是已调用的构造函数的返回值。</returns>
    /// <param name="invokeAttr">这必须是来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志，如 InvokeMethod、NonPublic 等。</param>
    /// <param name="binder">一个对象，它使用反射启用绑定、参数类型的强制、成员的调用和 MemberInfo 对象的检索。如果 binder 为 null，则使用默认联编程序。请参见<see cref="T:System.Reflection.Binder" />。</param>
    /// <param name="parameters">参数列表。这是一个参数数组，这些参数与要调用的构造函数的参数具有相同的数量、顺序和类型。如果没有参数，则它应为 null。</param>
    /// <param name="culture">用于控制类型强制的 <see cref="T:System.Globalization.CultureInfo" /> 的实例。如果这是 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。（这对于某些转换很必要，例如，将表示 1000 的 <see cref="T:System.String" /> 转换为 <see cref="T:System.Double" /> 值，因为不同的区域性以不同的形式表示 1000。）</param>
    /// <exception cref="T:System.NotSupportedException">目前不支持此方法。可以使用 <see cref="M:System.Type.GetConstructor(System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> 检索构造函数，并对返回的 <see cref="T:System.Reflection.ConstructorInfo" /> 调用 <see cref="M:System.Reflection.ConstructorInfo.Invoke(System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo)" />。</exception>
    public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>返回为此构造函数定义的所有自定义属性。</summary>
    /// <returns>返回对象的数组，这些对象表示由此 <see cref="T:System.Reflection.Emit.ConstructorBuilder" /> 实例表示的构造函数的所有自定义属性。</returns>
    /// <param name="inherit">控制来自基类的自定义属性的继承性。忽略此参数。</param>
    /// <exception cref="T:System.NotSupportedException">目前不支持此方法。</exception>
    public override object[] GetCustomAttributes(bool inherit)
    {
      return this.m_methodBuilder.GetCustomAttributes(inherit);
    }

    /// <summary>返回由给定类型标识的自定义属性。</summary>
    /// <returns>返回表示此构造函数的特性的 <see cref="T:System.Object" /> 类型数组。</returns>
    /// <param name="attributeType">自定义特性类型。</param>
    /// <param name="inherit">控制来自基类的自定义属性的继承性。忽略此参数。</param>
    /// <exception cref="T:System.NotSupportedException">目前不支持此方法。</exception>
    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      return this.m_methodBuilder.GetCustomAttributes(attributeType, inherit);
    }

    /// <summary>检查是否定义了指定的自定义特性类型。</summary>
    /// <returns>如果定义了指定的自定义特性类型，则为 true；否则为 false。</returns>
    /// <param name="attributeType">自定义特性类型。</param>
    /// <param name="inherit">控制来自基类的自定义属性的继承性。忽略此参数。</param>
    /// <exception cref="T:System.NotSupportedException">目前不支持此方法。可以使用 <see cref="M:System.Type.GetConstructor(System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> 检索构造函数，并对返回的 <see cref="T:System.Reflection.ConstructorInfo" /> 调用 <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" />。</exception>
    public override bool IsDefined(Type attributeType, bool inherit)
    {
      return this.m_methodBuilder.IsDefined(attributeType, inherit);
    }

    /// <summary>返回表示此构造函数的标记的 <see cref="T:System.Reflection.Emit.MethodToken" />。</summary>
    /// <returns>返回此构造函数的 <see cref="T:System.Reflection.Emit.MethodToken" />。</returns>
    public MethodToken GetToken()
    {
      return this.m_methodBuilder.GetToken();
    }

    /// <summary>定义此构造函数的参数。</summary>
    /// <returns>返回表示此构造函数的新参数的 ParameterBuilder 对象。</returns>
    /// <param name="iSequence">该参数在参数列表中的位置。为参数编索引，第一个参数从数字 1 开始。</param>
    /// <param name="attributes">参数的属性。</param>
    /// <param name="strParamName">参数名。名称可以为 null 字符串。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="iSequence" /> 小于 0（零），或者大于构造函数的参数数目。</exception>
    /// <exception cref="T:System.InvalidOperationException">已经使用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建了该包含类型。</exception>
    public ParameterBuilder DefineParameter(int iSequence, ParameterAttributes attributes, string strParamName)
    {
      attributes &= ~ParameterAttributes.ReservedMask;
      return this.m_methodBuilder.DefineParameter(iSequence, attributes, strParamName);
    }

    /// <summary>设置与符号信息关联的此构造函数的自定义特性。</summary>
    /// <param name="name">自定义特性的名称。</param>
    /// <param name="data">自定义特性的值。</param>
    /// <exception cref="T:System.InvalidOperationException">已经使用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建了该包含类型。- 或 -模块没有定义的符号编写器。例如，模块不是调试模块。</exception>
    public void SetSymCustomAttribute(string name, byte[] data)
    {
      this.m_methodBuilder.SetSymCustomAttribute(name, data);
    }

    /// <summary>获取此构造函数的 <see cref="T:System.Reflection.Emit.ILGenerator" />。</summary>
    /// <returns>返回此构造函数的 <see cref="T:System.Reflection.Emit.ILGenerator" /> 对象。</returns>
    /// <exception cref="T:System.InvalidOperationException">该构造函数为默认构造函数。- 或 -该构造函数具有 <see cref="T:System.Reflection.MethodAttributes" /> 或 <see cref="T:System.Reflection.MethodImplAttributes" /> 标记，指示其不能包含方法体。</exception>
    public ILGenerator GetILGenerator()
    {
      if (this.m_isDefaultConstructor)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DefaultConstructorILGen"));
      return this.m_methodBuilder.GetILGenerator();
    }

    /// <summary>获取有指定 MSIL 流大小的 <see cref="T:System.Reflection.Emit.ILGenerator" /> 对象，它可以用来生成此构造函数的方法体。</summary>
    /// <returns>此构造函数的 <see cref="T:System.Reflection.Emit.ILGenerator" />。</returns>
    /// <param name="streamSize">MSIL 流的大小，以字节为单位。</param>
    /// <exception cref="T:System.InvalidOperationException">该构造函数为默认构造函数。- 或 -该构造函数具有 <see cref="T:System.Reflection.MethodAttributes" /> 或 <see cref="T:System.Reflection.MethodImplAttributes" /> 标记，指示其不能包含方法体。</exception>
    public ILGenerator GetILGenerator(int streamSize)
    {
      if (this.m_isDefaultConstructor)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DefaultConstructorILGen"));
      return this.m_methodBuilder.GetILGenerator(streamSize);
    }

    /// <summary>使用指定的 Microsoft 中间语言 (MSIL) 指令的字节数组创建构造函数体。</summary>
    /// <param name="il">包含有效 MSIL 指令的数组。</param>
    /// <param name="maxStack">最大堆栈计算深度。</param>
    /// <param name="localSignature">包含序列化的本地变量结构的字节数组。如果构造函数没有局部变量，请指定 null。</param>
    /// <param name="exceptionHandlers">包含构造函数的异常处理程序的集合。如果构造函数没有异常管理器，请指定 null。</param>
    /// <param name="tokenFixups">表示 <paramref name="il" /> 中偏移量的值的集合，每个偏移量都指定了可以修改的标记的开头部分。如果构造函数没有必须修改的标记，请指定 null。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="il" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="maxStack" /> 为负数。- 或 -<paramref name="exceptionHandlers" /> 的一个指定偏移超出 <paramref name="il" /> 。- 或 -<paramref name="tokenFixups" /> 的一个指定偏移 <paramref name="il" /> 之外。</exception>
    /// <exception cref="T:System.InvalidOperationException">该包含类型是以前使用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 方法创建的。- 或 -此方法之前在此 <see cref="T:System.Reflection.Emit.ConstructorBuilder" /> 对象上已调用。</exception>
    public void SetMethodBody(byte[] il, int maxStack, byte[] localSignature, IEnumerable<ExceptionHandler> exceptionHandlers, IEnumerable<int> tokenFixups)
    {
      if (this.m_isDefaultConstructor)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DefaultConstructorDefineBody"));
      this.m_methodBuilder.SetMethodBody(il, maxStack, localSignature, exceptionHandlers, tokenFixups);
    }

    /// <summary>向此构造函数添加声明性安全。</summary>
    /// <param name="action">要执行的安全操作，如 Demand、Assert 等。</param>
    /// <param name="pset">操作应用于的权限集。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="action" /> 无效（RequestMinimum、RequestOptional 和 RequestRefuse 无效）。</exception>
    /// <exception cref="T:System.InvalidOperationException">以前已使用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建了包含类型。- 或 -权限集 <paramref name="pset" /> 包含以前由 AddDeclarativeSecurity 添加的操作。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pset" /> 为 null。</exception>
    [SecuritySafeCritical]
    public void AddDeclarativeSecurity(SecurityAction action, PermissionSet pset)
    {
      if (pset == null)
        throw new ArgumentNullException("pset");
      if (!Enum.IsDefined(typeof (SecurityAction), (object) action) || action == SecurityAction.RequestMinimum || (action == SecurityAction.RequestOptional || action == SecurityAction.RequestRefuse))
        throw new ArgumentOutOfRangeException("action");
      if (this.m_methodBuilder.IsTypeCreated())
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TypeHasBeenCreated"));
      byte[] numArray = pset.EncodeXml();
      RuntimeModule nativeHandle = this.GetModuleBuilder().GetNativeHandle();
      int token = this.GetToken().Token;
      int num = (int) action;
      byte[] blob = numArray;
      int length = blob.Length;
      TypeBuilder.AddDeclarativeSecurity(nativeHandle, token, (SecurityAction) num, blob, length);
    }

    /// <summary>返回对包含此构造函数的模块的引用。</summary>
    /// <returns>包含此构造函数的模块。</returns>
    public Module GetModule()
    {
      return this.m_methodBuilder.GetModule();
    }

    internal override Type GetReturnType()
    {
      return this.m_methodBuilder.ReturnType;
    }

    /// <summary>使用指定的自定义属性 Blob 设置自定义属性。</summary>
    /// <param name="con">自定义属性的构造函数。</param>
    /// <param name="binaryAttribute">表示属性的字节 Blob。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="con" /> 或 <paramref name="binaryAttribute" /> 为 null。</exception>
    [ComVisible(true)]
    public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
    {
      this.m_methodBuilder.SetCustomAttribute(con, binaryAttribute);
    }

    /// <summary>使用自定义属性生成器设置自定义属性。</summary>
    /// <param name="customBuilder">定义自定义属性的帮助器类的实例。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="customBuilder" /> 为 null。</exception>
    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
      this.m_methodBuilder.SetCustomAttribute(customBuilder);
    }

    /// <summary>设置此构造函数的方法实现标志。</summary>
    /// <param name="attributes">方法实现标志。</param>
    /// <exception cref="T:System.InvalidOperationException">已经使用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建了该包含类型。</exception>
    public void SetImplementationFlags(MethodImplAttributes attributes)
    {
      this.m_methodBuilder.SetImplementationFlags(attributes);
    }

    void _ConstructorBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _ConstructorBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _ConstructorBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _ConstructorBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
