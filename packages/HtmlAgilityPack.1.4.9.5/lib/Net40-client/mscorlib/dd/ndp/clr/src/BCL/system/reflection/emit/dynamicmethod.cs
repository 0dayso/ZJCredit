// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.DynamicMethod
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System.Reflection.Emit
{
  /// <summary>定义并表示一种可编译、执行和丢弃的动态方法。丢弃的方法可用于垃圾回收。</summary>
  [ComVisible(true)]
  public sealed class DynamicMethod : MethodInfo
  {
    private static readonly object s_anonymouslyHostedDynamicMethodsModuleLock = new object();
    private RuntimeType[] m_parameterTypes;
    internal IRuntimeMethodInfo m_methodHandle;
    private RuntimeType m_returnType;
    private DynamicILGenerator m_ilGenerator;
    private DynamicILInfo m_DynamicILInfo;
    private bool m_fInitLocals;
    private RuntimeModule m_module;
    internal bool m_skipVisibility;
    internal RuntimeType m_typeOwner;
    private DynamicMethod.RTDynamicMethod m_dynMethod;
    internal DynamicResolver m_resolver;
    private bool m_profileAPICheck;
    private RuntimeAssembly m_creatorAssembly;
    internal bool m_restrictedSkipVisibility;
    internal CompressedStack m_creationContext;
    private static volatile InternalModuleBuilder s_anonymouslyHostedDynamicMethodsModule;

    internal bool ProfileAPICheck
    {
      get
      {
        return this.m_profileAPICheck;
      }
      [FriendAccessAllowed] set
      {
        this.m_profileAPICheck = value;
      }
    }

    /// <summary>获取动态方法的名称。</summary>
    /// <returns>方法的简称。</returns>
    public override string Name
    {
      get
      {
        return this.m_dynMethod.Name;
      }
    }

    /// <summary>获取声明方法的类型，对于动态方法，类型始终为 null。</summary>
    /// <returns>始终为 null。</returns>
    public override Type DeclaringType
    {
      get
      {
        return this.m_dynMethod.DeclaringType;
      }
    }

    /// <summary>获取在反射中用于获取方法的类。</summary>
    /// <returns>始终为 null。</returns>
    public override Type ReflectedType
    {
      get
      {
        return this.m_dynMethod.ReflectedType;
      }
    }

    /// <summary>获取动态方法逻辑关联的模块。</summary>
    /// <returns>动态方法当前关联的 <see cref="T:System.Reflection.Module" />。</returns>
    public override Module Module
    {
      get
      {
        return this.m_dynMethod.Module;
      }
    }

    /// <summary>动态方法不支持。</summary>
    /// <returns>动态方法不支持。</returns>
    /// <exception cref="T:System.InvalidOperationException">动态方法不允许。</exception>
    public override RuntimeMethodHandle MethodHandle
    {
      get
      {
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod"));
      }
    }

    /// <summary>获取创建动态方法时指定的属性。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.MethodAttributes" /> 值的按位组合，表示方法的属性。</returns>
    public override MethodAttributes Attributes
    {
      get
      {
        return this.m_dynMethod.Attributes;
      }
    }

    /// <summary>获取创建动态方法时指定的调用约定。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.CallingConventions" /> 值，它指示方法的调用约定。</returns>
    public override CallingConventions CallingConvention
    {
      get
      {
        return this.m_dynMethod.CallingConvention;
      }
    }

    /// <summary>获取一个值，该值指示当前动态方法是安全关键的还是安全可靠关键的，因此可以执行关键操作。</summary>
    /// <returns>如果当前动态方法是安全关键的或安全可靠关键的，则为 true；如果它是透明的，则为 false。</returns>
    /// <exception cref="T:System.InvalidOperationException">动态方法没有方法体。</exception>
    public override bool IsSecurityCritical
    {
      [SecuritySafeCritical] get
      {
        if (this.m_methodHandle != null)
          return RuntimeMethodHandle.IsSecurityCritical(this.m_methodHandle);
        if (this.m_typeOwner != (RuntimeType) null)
          return (this.m_typeOwner.Assembly as RuntimeAssembly).IsAllSecurityCritical();
        return (this.m_module.Assembly as RuntimeAssembly).IsAllSecurityCritical();
      }
    }

    /// <summary>获取一个值，该值指示当前动态方法在当前信任级别上是否是安全可靠关键的；即它是否可以执行关键操作并可以由透明代码访问。</summary>
    /// <returns>如果动态方法在当前信任级别上是安全可靠关键的，则为 true；如果它是安全关键的或透明的，则为 false。</returns>
    /// <exception cref="T:System.InvalidOperationException">动态方法没有方法体。</exception>
    public override bool IsSecuritySafeCritical
    {
      [SecuritySafeCritical] get
      {
        if (this.m_methodHandle != null)
          return RuntimeMethodHandle.IsSecuritySafeCritical(this.m_methodHandle);
        if (this.m_typeOwner != (RuntimeType) null)
          return (this.m_typeOwner.Assembly as RuntimeAssembly).IsAllPublicAreaSecuritySafeCritical();
        return (this.m_module.Assembly as RuntimeAssembly).IsAllSecuritySafeCritical();
      }
    }

    /// <summary>获取一个值，该值指示动态方法在当前信任级别上是透明的，因此无法执行关键操作。</summary>
    /// <returns>如果动态方法在当前信任级别上是安全透明的，则为 true；否则为 false。</returns>
    /// <exception cref="T:System.InvalidOperationException">动态方法没有方法体。</exception>
    public override bool IsSecurityTransparent
    {
      [SecuritySafeCritical] get
      {
        if (this.m_methodHandle != null)
          return RuntimeMethodHandle.IsSecurityTransparent(this.m_methodHandle);
        if (this.m_typeOwner != (RuntimeType) null)
          return !(this.m_typeOwner.Assembly as RuntimeAssembly).IsAllSecurityCritical();
        return !(this.m_module.Assembly as RuntimeAssembly).IsAllSecurityCritical();
      }
    }

    /// <summary>获取动态方法的返回值的类型。</summary>
    /// <returns>一个 <see cref="T:System.Type" />，表示当前方法的返回值的类型；如果该方法没有返回类型，则为 <see cref="T:System.Void" />。</returns>
    public override Type ReturnType
    {
      get
      {
        return this.m_dynMethod.ReturnType;
      }
    }

    /// <summary>获取动态方法的返回参数。</summary>
    /// <returns>始终为 null。</returns>
    public override ParameterInfo ReturnParameter
    {
      get
      {
        return this.m_dynMethod.ReturnParameter;
      }
    }

    /// <summary>获取动态方法的返回类型的自定义属性。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.ICustomAttributeProvider" />，表示动态方法的返回类型的自定义属性。</returns>
    public override ICustomAttributeProvider ReturnTypeCustomAttributes
    {
      get
      {
        return this.m_dynMethod.ReturnTypeCustomAttributes;
      }
    }

    /// <summary>获取或设置一个值，该值指示方法中的局部变量是否初始化为零。</summary>
    /// <returns>如果方法中的局部变量初始化为零，则为 true；否则为 false。默认值为 true。</returns>
    public bool InitLocals
    {
      get
      {
        return this.m_fInitLocals;
      }
      set
      {
        this.m_fInitLocals = value;
      }
    }

    private DynamicMethod()
    {
    }

    /// <summary>初始化匿名承载的动态方法，指定方法名称、返回类型和参数类型。</summary>
    /// <param name="name">动态方法的名称。可以是长度为零的字符串，但不能为 null。</param>
    /// <param name="returnType">一个 <see cref="T:System.Type" /> 对象，它指定动态方法的返回类型；如果方法没有返回类型，则为 null。</param>
    /// <param name="parameterTypes">一个 <see cref="T:System.Type" /> 对象数组，它指定动态方法的参数的类型；如果方法没有参数，则为 null。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="parameterTypes" /> 的一个元素为 null 或 <see cref="T:System.Void" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="returnType" /> 为 <see cref="P:System.Type.IsByRef" /> 返回 true 的类型。</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public DynamicMethod(string name, Type returnType, Type[] parameterTypes)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.Init(name, MethodAttributes.Public | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, (Type) null, (Module) null, false, true, ref stackMark);
    }

    /// <summary>初始化匿名承载的动态方法，指定方法名称、返回类型、参数类型，并指定动态方法的 Microsoft 中间语言 (MSIL) 访问的类型和成员是否应跳过实时 (JIT) 可见性检查。</summary>
    /// <param name="name">动态方法的名称。可以是长度为零的字符串，但不能为 null。</param>
    /// <param name="returnType">一个 <see cref="T:System.Type" /> 对象，它指定动态方法的返回类型；如果方法没有返回类型，则为 null。</param>
    /// <param name="parameterTypes">一个 <see cref="T:System.Type" /> 对象数组，它指定动态方法的参数的类型；如果方法没有参数，则为 null。</param>
    /// <param name="restrictedSkipVisibility">若要跳过对动态方法的 MSIL 访问的类型和方法的 JIT 可见性检查，则为 true，前提是：包含这些类型和成员的程序集的信任级别必须等于或低于发出动态方法的调用堆栈的信任级别；否则为 false。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="parameterTypes" /> 的一个元素为 null 或 <see cref="T:System.Void" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="returnType" /> 为 <see cref="P:System.Type.IsByRef" /> 返回 true 的类型。</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public DynamicMethod(string name, Type returnType, Type[] parameterTypes, bool restrictedSkipVisibility)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.Init(name, MethodAttributes.Public | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, (Type) null, (Module) null, restrictedSkipVisibility, true, ref stackMark);
    }

    /// <summary>创建一个对模块全局有效的动态方法，指定方法名称、返回类型、参数类型和模块。</summary>
    /// <param name="name">动态方法的名称。可以是长度为零的字符串，但不能为 null。</param>
    /// <param name="returnType">一个 <see cref="T:System.Type" /> 对象，它指定动态方法的返回类型；如果方法没有返回类型，则为 null。</param>
    /// <param name="parameterTypes">一个 <see cref="T:System.Type" /> 对象数组，它指定动态方法的参数的类型；如果方法没有参数，则为 null。</param>
    /// <param name="m">一个 <see cref="T:System.Reflection.Module" />，表示动态方法将与之逻辑关联的模块。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="parameterTypes" /> 的一个元素为 null 或 <see cref="T:System.Void" />。- 或 -<paramref name="m" /> 是为动态方法提供匿名承载的模块。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。- 或 -<paramref name="m" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="returnType" /> 为 <see cref="P:System.Type.IsByRef" /> 返回 true 的类型。</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public DynamicMethod(string name, Type returnType, Type[] parameterTypes, Module m)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PerformSecurityCheck(m, ref stackMark, false);
      this.Init(name, MethodAttributes.Public | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, (Type) null, m, false, false, ref stackMark);
    }

    /// <summary>创建一个对模块全局有效的动态方法，指定方法名称、返回类型、参数类型和模块，并指定动态方法的 Microsoft 中间语言 (MSIL) 访问的类型和成员是否应跳过实时 (JIT) 可见性检查。</summary>
    /// <param name="name">动态方法的名称。可以是长度为零的字符串，但不能为 null。</param>
    /// <param name="returnType">一个 <see cref="T:System.Type" /> 对象，它指定动态方法的返回类型；如果方法没有返回类型，则为 null。</param>
    /// <param name="parameterTypes">一个 <see cref="T:System.Type" /> 对象数组，它指定动态方法的参数的类型；如果方法没有参数，则为 null。</param>
    /// <param name="m">一个 <see cref="T:System.Reflection.Module" />，表示动态方法将与之逻辑关联的模块。</param>
    /// <param name="skipVisibility">要跳过对动态方法的 MSIL 访问的类型和成员的 JIT 可见性检查，则为 true。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="parameterTypes" /> 的一个元素为 null 或 <see cref="T:System.Void" />。- 或 -<paramref name="m" /> 是为动态方法提供匿名承载的模块。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。- 或 -<paramref name="m" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="returnType" /> 为 <see cref="P:System.Type.IsByRef" /> 返回 true 的类型。</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public DynamicMethod(string name, Type returnType, Type[] parameterTypes, Module m, bool skipVisibility)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PerformSecurityCheck(m, ref stackMark, skipVisibility);
      this.Init(name, MethodAttributes.Public | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, (Type) null, m, skipVisibility, false, ref stackMark);
    }

    /// <summary>创建一个对模块全局有效的动态方法，指定方法名称、属性、调用约定、返回类型、参数类型和模块，并指定动态方法的 Microsoft 中间语言 (MSIL) 访问的类型和成员是否应跳过实时 (JIT) 可见性检查。</summary>
    /// <param name="name">动态方法的名称。可以是长度为零的字符串，但不能为 null。</param>
    /// <param name="attributes">
    /// <see cref="T:System.Reflection.MethodAttributes" /> 值的按位组合，指定动态方法的属性。允许的唯一组合为 <see cref="F:System.Reflection.MethodAttributes.Public" /> 和 <see cref="F:System.Reflection.MethodAttributes.Static" />。</param>
    /// <param name="callingConvention">动态方法的调用约定。必须为 <see cref="F:System.Reflection.CallingConventions.Standard" />。</param>
    /// <param name="returnType">一个 <see cref="T:System.Type" /> 对象，它指定动态方法的返回类型；如果方法没有返回类型，则为 null。</param>
    /// <param name="parameterTypes">一个 <see cref="T:System.Type" /> 对象数组，它指定动态方法的参数的类型；如果方法没有参数，则为 null。</param>
    /// <param name="m">一个 <see cref="T:System.Reflection.Module" />，表示动态方法将与之逻辑关联的模块。</param>
    /// <param name="skipVisibility">要跳过动态方法的 MSIL 访问的类型和成员的 JIT 可见性检查，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="parameterTypes" /> 的一个元素为 null 或 <see cref="T:System.Void" />。- 或 -<paramref name="m" /> 是为动态方法提供匿名承载的模块。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。- 或 -<paramref name="m" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="attributes" /> 是标志的组合，而不是 <see cref="F:System.Reflection.MethodAttributes.Public" /> 和 <see cref="F:System.Reflection.MethodAttributes.Static" /> 的组合。- 或 -<paramref name="callingConvention" /> 不是 <see cref="F:System.Reflection.CallingConventions.Standard" />。- 或 -<paramref name="returnType" /> 为 <see cref="P:System.Type.IsByRef" /> 返回 true 的类型。</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public DynamicMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Module m, bool skipVisibility)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PerformSecurityCheck(m, ref stackMark, skipVisibility);
      this.Init(name, attributes, callingConvention, returnType, parameterTypes, (Type) null, m, skipVisibility, false, ref stackMark);
    }

    /// <summary>创建一个动态方法，指定方法名称、返回类型、参数类型和动态方法逻辑关联的类型。</summary>
    /// <param name="name">动态方法的名称。可以是长度为零的字符串，但不能为 null。</param>
    /// <param name="returnType">一个 <see cref="T:System.Type" /> 对象，它指定动态方法的返回类型；如果方法没有返回类型，则为 null。</param>
    /// <param name="parameterTypes">一个 <see cref="T:System.Type" /> 对象数组，它指定动态方法的参数的类型；如果方法没有参数，则为 null。</param>
    /// <param name="owner">一个 <see cref="T:System.Type" />，动态方法与其逻辑关联。动态方法可以访问类型的所有成员。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="parameterTypes" /> 的一个元素为 null 或 <see cref="T:System.Void" />。- 或 -<paramref name="owner" /> 是一个接口、一个数组、一个开放式泛型类型或一个泛型类型或方法的类型参数。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。- 或 -<paramref name="owner" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="returnType" /> 为 null，或者为 <see cref="P:System.Type.IsByRef" /> 返回 true 的类型。</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public DynamicMethod(string name, Type returnType, Type[] parameterTypes, Type owner)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PerformSecurityCheck(owner, ref stackMark, false);
      this.Init(name, MethodAttributes.Public | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, owner, (Module) null, false, false, ref stackMark);
    }

    /// <summary>创建一个动态方法，指定方法名称、返回类型、参数类型、动态方法逻辑关联的类型，并指定动态方法的 Microsoft 中间语言 (MSIL) 访问的类型和成员是否应跳过实时 (JIT) 可见性检查。</summary>
    /// <param name="name">动态方法的名称。可以是长度为零的字符串，但不能为 null。</param>
    /// <param name="returnType">一个 <see cref="T:System.Type" /> 对象，它指定动态方法的返回类型；如果方法没有返回类型，则为 null。</param>
    /// <param name="parameterTypes">一个 <see cref="T:System.Type" /> 对象数组，它指定动态方法的参数的类型；如果方法没有参数，则为 null。</param>
    /// <param name="owner">一个 <see cref="T:System.Type" />，动态方法与其逻辑关联。动态方法可以访问类型的所有成员。</param>
    /// <param name="skipVisibility">要跳过动态方法的 MSIL 访问的类型和成员的 JIT 可见性检查，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="parameterTypes" /> 的一个元素为 null 或 <see cref="T:System.Void" />。- 或 -<paramref name="owner" /> 是一个接口、一个数组、一个开放式泛型类型或一个泛型类型或方法的类型参数。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。- 或 -<paramref name="owner" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="returnType" /> 为 null，或者为 <see cref="P:System.Type.IsByRef" /> 返回 true 的类型。</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public DynamicMethod(string name, Type returnType, Type[] parameterTypes, Type owner, bool skipVisibility)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PerformSecurityCheck(owner, ref stackMark, skipVisibility);
      this.Init(name, MethodAttributes.Public | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, owner, (Module) null, skipVisibility, false, ref stackMark);
    }

    /// <summary>创建一个动态方法，指定方法名称、特性、调用约定、返回类型、参数类型、动态方法逻辑关联的类型，并指定动态方法的 Microsoft 中间语言 (MSIL) 访问的类型和成员是否应跳过实时 (JIT) 可见性检查。</summary>
    /// <param name="name">动态方法的名称。可以是长度为零的字符串，但不能为 null。</param>
    /// <param name="attributes">
    /// <see cref="T:System.Reflection.MethodAttributes" /> 值的按位组合，指定动态方法的属性。允许的唯一组合为 <see cref="F:System.Reflection.MethodAttributes.Public" /> 和 <see cref="F:System.Reflection.MethodAttributes.Static" />。</param>
    /// <param name="callingConvention">动态方法的调用约定。必须为 <see cref="F:System.Reflection.CallingConventions.Standard" />。</param>
    /// <param name="returnType">一个 <see cref="T:System.Type" /> 对象，它指定动态方法的返回类型；如果方法没有返回类型，则为 null。</param>
    /// <param name="parameterTypes">一个 <see cref="T:System.Type" /> 对象数组，它指定动态方法的参数的类型；如果方法没有参数，则为 null。</param>
    /// <param name="owner">一个 <see cref="T:System.Type" />，动态方法与其逻辑关联。动态方法可以访问类型的所有成员。</param>
    /// <param name="skipVisibility">要跳过动态方法的 MSIL 访问的类型和成员的 JIT 可见性检查，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="parameterTypes" /> 的一个元素为 null 或 <see cref="T:System.Void" />。- 或 -<paramref name="owner" /> 是一个接口、一个数组、一个开放式泛型类型或一个泛型类型或方法的类型参数。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。- 或 -<paramref name="owner" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="attributes" /> 是标志的组合，而不是 <see cref="F:System.Reflection.MethodAttributes.Public" /> 和 <see cref="F:System.Reflection.MethodAttributes.Static" /> 的组合。- 或 -<paramref name="callingConvention" /> 不是 <see cref="F:System.Reflection.CallingConventions.Standard" />。- 或 -<paramref name="returnType" /> 为 <see cref="P:System.Type.IsByRef" /> 返回 true 的类型。</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public DynamicMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type owner, bool skipVisibility)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      this.PerformSecurityCheck(owner, ref stackMark, skipVisibility);
      this.Init(name, attributes, callingConvention, returnType, parameterTypes, owner, (Module) null, skipVisibility, false, ref stackMark);
    }

    private static void CheckConsistency(MethodAttributes attributes, CallingConventions callingConvention)
    {
      if ((attributes & ~MethodAttributes.MemberAccessMask) != MethodAttributes.Static)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicMethodFlags"));
      if ((attributes & MethodAttributes.MemberAccessMask) != MethodAttributes.Public)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicMethodFlags"));
      if (callingConvention != CallingConventions.Standard && callingConvention != CallingConventions.VarArgs)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicMethodFlags"));
      if (callingConvention == CallingConventions.VarArgs)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicMethodFlags"));
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static RuntimeModule GetDynamicMethodsModule()
    {
      if ((Module) DynamicMethod.s_anonymouslyHostedDynamicMethodsModule != (Module) null)
        return (RuntimeModule) DynamicMethod.s_anonymouslyHostedDynamicMethodsModule;
      lock (DynamicMethod.s_anonymouslyHostedDynamicMethodsModuleLock)
      {
        if ((Module) DynamicMethod.s_anonymouslyHostedDynamicMethodsModule != (Module) null)
          return (RuntimeModule) DynamicMethod.s_anonymouslyHostedDynamicMethodsModule;
        CustomAttributeBuilder local_2 = new CustomAttributeBuilder(typeof (SecurityTransparentAttribute).GetConstructor(Type.EmptyTypes), EmptyArray<object>.Value);
        List<CustomAttributeBuilder> local_3 = new List<CustomAttributeBuilder>();
        local_3.Add(local_2);
        CustomAttributeBuilder local_4 = new CustomAttributeBuilder(typeof (SecurityRulesAttribute).GetConstructor(new Type[1]{ typeof (SecurityRuleSet) }), new object[1]{ (object) SecurityRuleSet.Level1 });
        local_3.Add(local_4);
        AssemblyName temp_36 = new AssemblyName("Anonymously Hosted DynamicMethods Assembly");
        StackCrawlMark local_5 = StackCrawlMark.LookForMe;
        int temp_38 = 1;
        // ISSUE: variable of the null type
        __Null temp_39 = null;
        // ISSUE: variable of the null type
        __Null temp_40 = null;
        // ISSUE: variable of the null type
        __Null temp_41 = null;
        // ISSUE: variable of the null type
        __Null temp_42 = null;
        // ISSUE: variable of the null type
        __Null temp_43 = null;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        StackCrawlMark& temp_44 = @local_5;
        List<CustomAttributeBuilder> temp_45 = local_3;
        int temp_46 = 1;
        AssemblyBuilder temp_47 = AssemblyBuilder.InternalDefineDynamicAssembly(temp_36, (AssemblyBuilderAccess) temp_38, (string) temp_39, (Evidence) temp_40, (PermissionSet) temp_41, (PermissionSet) temp_42, (PermissionSet) temp_43, temp_44, (IEnumerable<CustomAttributeBuilder>) temp_45, (SecurityContextSource) temp_46);
        AppDomain.PublishAnonymouslyHostedDynamicMethodsAssembly(temp_47.GetNativeHandle());
        DynamicMethod.s_anonymouslyHostedDynamicMethodsModule = (InternalModuleBuilder) temp_47.ManifestModule;
      }
      return (RuntimeModule) DynamicMethod.s_anonymouslyHostedDynamicMethodsModule;
    }

    [SecurityCritical]
    private void Init(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] signature, Type owner, Module m, bool skipVisibility, bool transparentMethod, ref StackCrawlMark stackMark)
    {
      DynamicMethod.CheckConsistency(attributes, callingConvention);
      if (signature != null)
      {
        this.m_parameterTypes = new RuntimeType[signature.Length];
        for (int index = 0; index < signature.Length; ++index)
        {
          if (signature[index] == (Type) null)
            throw new ArgumentException(Environment.GetResourceString("Arg_InvalidTypeInSignature"));
          this.m_parameterTypes[index] = signature[index].UnderlyingSystemType as RuntimeType;
          if (this.m_parameterTypes[index] == (RuntimeType) null || this.m_parameterTypes[index] == null || this.m_parameterTypes[index] == (RuntimeType) typeof (void))
            throw new ArgumentException(Environment.GetResourceString("Arg_InvalidTypeInSignature"));
        }
      }
      else
        this.m_parameterTypes = new RuntimeType[0];
      this.m_returnType = returnType == (Type) null ? (RuntimeType) typeof (void) : returnType.UnderlyingSystemType as RuntimeType;
      if (this.m_returnType == (RuntimeType) null || this.m_returnType == null || this.m_returnType.IsByRef)
        throw new NotSupportedException(Environment.GetResourceString("Arg_InvalidTypeInRetType"));
      if (transparentMethod)
      {
        this.m_module = DynamicMethod.GetDynamicMethodsModule();
        if (skipVisibility)
          this.m_restrictedSkipVisibility = true;
        this.m_creationContext = CompressedStack.Capture();
      }
      else
      {
        if (m != (Module) null)
        {
          this.m_module = m.ModuleHandle.GetRuntimeModule();
        }
        else
        {
          RuntimeType runtimeType = (RuntimeType) null;
          if (owner != (Type) null)
            runtimeType = owner.UnderlyingSystemType as RuntimeType;
          if (runtimeType != (RuntimeType) null)
          {
            if (runtimeType.HasElementType || runtimeType.ContainsGenericParameters || (runtimeType.IsGenericParameter || runtimeType.IsInterface))
              throw new ArgumentException(Environment.GetResourceString("Argument_InvalidTypeForDynamicMethod"));
            this.m_typeOwner = runtimeType;
            this.m_module = runtimeType.GetRuntimeModule();
          }
        }
        this.m_skipVisibility = skipVisibility;
      }
      this.m_ilGenerator = (DynamicILGenerator) null;
      this.m_fInitLocals = true;
      this.m_methodHandle = (IRuntimeMethodInfo) null;
      if (name == null)
        throw new ArgumentNullException("name");
      if (AppDomain.ProfileAPICheck)
      {
        if ((Assembly) this.m_creatorAssembly == (Assembly) null)
          this.m_creatorAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackMark);
        if ((Assembly) this.m_creatorAssembly != (Assembly) null && !this.m_creatorAssembly.IsFrameworkAssembly())
          this.m_profileAPICheck = true;
      }
      this.m_dynMethod = new DynamicMethod.RTDynamicMethod(this, name, attributes, callingConvention);
    }

    [SecurityCritical]
    private void PerformSecurityCheck(Module m, ref StackCrawlMark stackMark, bool skipVisibility)
    {
      if (m == (Module) null)
        throw new ArgumentNullException("m");
      ModuleBuilder moduleBuilder = m as ModuleBuilder;
      RuntimeModule runtimeModule = !((Module) moduleBuilder != (Module) null) ? m as RuntimeModule : (RuntimeModule) moduleBuilder.InternalModule;
      if ((Module) runtimeModule == (Module) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeModule"), "m");
      if ((Module) runtimeModule == (Module) DynamicMethod.s_anonymouslyHostedDynamicMethodsModule)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"), "m");
      if (skipVisibility)
        new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Demand();
      this.m_creatorAssembly = RuntimeMethodHandle.GetCallerType(ref stackMark).GetRuntimeAssembly();
      if (!(m.Assembly != (Assembly) this.m_creatorAssembly))
        return;
      CodeAccessSecurityEngine.ReflectionTargetDemandHelper(PermissionType.SecurityControlEvidence, m.Assembly.PermissionSet);
    }

    [SecurityCritical]
    private void PerformSecurityCheck(Type owner, ref StackCrawlMark stackMark, bool skipVisibility)
    {
      if (owner == (Type) null)
        throw new ArgumentNullException("owner");
      RuntimeType runtimeType = owner as RuntimeType;
      if (runtimeType == (RuntimeType) null)
        runtimeType = owner.UnderlyingSystemType as RuntimeType;
      if (runtimeType == (RuntimeType) null)
        throw new ArgumentNullException("owner", Environment.GetResourceString("Argument_MustBeRuntimeType"));
      RuntimeType callerType = RuntimeMethodHandle.GetCallerType(ref stackMark);
      if (skipVisibility)
        new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Demand();
      else if (callerType != runtimeType)
        new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Demand();
      this.m_creatorAssembly = callerType.GetRuntimeAssembly();
      if (!(runtimeType.Assembly != (Assembly) this.m_creatorAssembly))
        return;
      CodeAccessSecurityEngine.ReflectionTargetDemandHelper(PermissionType.SecurityControlEvidence, owner.Assembly.PermissionSet);
    }

    /// <summary>完成动态方法并创建一个可用于执行该方法的委托。</summary>
    /// <returns>一个指定类型的委托，可用于执行动态方法。</returns>
    /// <param name="delegateType">一个签名与动态方法的签名匹配的委托类型。</param>
    /// <exception cref="T:System.InvalidOperationException">动态方法没有方法体。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="delegateType" /> 的参数数量不正确，或者参数类型不正确。</exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public override sealed Delegate CreateDelegate(Type delegateType)
    {
      if (this.m_restrictedSkipVisibility)
      {
        this.GetMethodDescriptor();
        RuntimeHelpers._CompileMethod(this.m_methodHandle);
      }
      MulticastDelegate multicastDelegate = (MulticastDelegate) Delegate.CreateDelegateNoSecurityCheck(delegateType, (object) null, this.GetMethodDescriptor());
      MethodInfo methodInfo = this.GetMethodInfo();
      multicastDelegate.StoreDynamicMethod(methodInfo);
      return (Delegate) multicastDelegate;
    }

    /// <summary>完成动态方法并创建一个可用于执行该方法的委托，指定委托类型和委托绑定到的对象。</summary>
    /// <returns>一个指定类型的委托，可用于对指定的目标对象执行动态方法。</returns>
    /// <param name="delegateType">一个签名与动态方法的签名匹配的委托类型，不包括第一个参数。</param>
    /// <param name="target">委托绑定到的对象。其类型必须与动态方法的第一个参数的类型相同。</param>
    /// <exception cref="T:System.InvalidOperationException">动态方法没有方法体。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 的类型与动态方法的第一个参数的类型不同，不能赋值给该类型。- 或 -<paramref name="delegateType" /> 的参数数量不正确，或者参数类型不正确。</exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public override sealed Delegate CreateDelegate(Type delegateType, object target)
    {
      if (this.m_restrictedSkipVisibility)
      {
        this.GetMethodDescriptor();
        RuntimeHelpers._CompileMethod(this.m_methodHandle);
      }
      MulticastDelegate multicastDelegate = (MulticastDelegate) Delegate.CreateDelegateNoSecurityCheck(delegateType, target, this.GetMethodDescriptor());
      MethodInfo methodInfo = this.GetMethodInfo();
      multicastDelegate.StoreDynamicMethod(methodInfo);
      return (Delegate) multicastDelegate;
    }

    [SecurityCritical]
    internal RuntimeMethodHandle GetMethodDescriptor()
    {
      if (this.m_methodHandle == null)
      {
        lock (this)
        {
          if (this.m_methodHandle == null)
          {
            if (this.m_DynamicILInfo != null)
            {
              this.m_DynamicILInfo.GetCallableMethod(this.m_module, this);
            }
            else
            {
              if (this.m_ilGenerator == null || this.m_ilGenerator.ILOffset == 0)
                throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadEmptyMethodBody", (object) this.Name));
              this.m_ilGenerator.GetCallableMethod(this.m_module, this);
            }
          }
        }
      }
      return new RuntimeMethodHandle(this.m_methodHandle);
    }

    /// <summary>返回方法的签名，以字符串表示。</summary>
    /// <returns>表示方法签名的字符串。</returns>
    public override string ToString()
    {
      return this.m_dynMethod.ToString();
    }

    /// <summary>返回方法的基实现。</summary>
    /// <returns>方法的基实现。</returns>
    public override MethodInfo GetBaseDefinition()
    {
      return (MethodInfo) this;
    }

    /// <summary>返回动态方法的参数。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.ParameterInfo" /> 对象数组，表示动态方法的参数。</returns>
    public override ParameterInfo[] GetParameters()
    {
      return this.m_dynMethod.GetParameters();
    }

    /// <summary>返回此方法的实现标志。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.MethodImplAttributes" /> 值的按位组合，表示此方法的实现标志。</returns>
    public override MethodImplAttributes GetMethodImplementationFlags()
    {
      return this.m_dynMethod.GetMethodImplementationFlags();
    }

    /// <summary>使用指定的参数，在指定的联编程序的约束下，使用指定的区域性信息调用动态方法。</summary>
    /// <returns>一个 <see cref="T:System.Object" />，包含调用的方法的返回值。</returns>
    /// <param name="obj">因为动态方法是静态的，所以对于动态方法，忽略此参数。指定 null。</param>
    /// <param name="invokeAttr">
    /// <see cref="T:System.Reflection.BindingFlags" /> 值的按位组合。</param>
    /// <param name="binder">一个 <see cref="T:System.Reflection.Binder" /> 对象，该对象通过反射来启用绑定、参数类型强制、成员调用以及对 <see cref="T:System.Reflection.MemberInfo" /> 对象的检索。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。有关更多详细信息，请参见 <see cref="T:System.Reflection.Binder" />。</param>
    /// <param name="parameters">参数列表。这是一个参数数组，这些参数与要调用的方法的参数具有相同的数目、顺序和类型。如果没有任何参数，则此参数应为 null。</param>
    /// <param name="culture">用于控制类型强制的 <see cref="T:System.Globalization.CultureInfo" /> 的实例。如果这是 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。例如，将表示 1000 的 <see cref="T:System.String" /> 正确转换为 <see cref="T:System.Double" /> 值时需要此信息，因为不同的区域性使用不同的形式表示 1000。</param>
    /// <exception cref="T:System.NotSupportedException">不支持 <see cref="F:System.Reflection.CallingConventions.VarArgs" /> 调用约定。</exception>
    /// <exception cref="T:System.Reflection.TargetParameterCountException">
    /// <paramref name="parameters" /> 中的元素数量与动态方法中的参数数量不匹配。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="parameters" /> 的一个或多个元素的类型与动态方法的相应参数的类型不匹配。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">该动态方法与模块关联，并且不是匿名承载的，而是在 <paramref name="skipVisibility" /> 设置为 false 的情况下构造的，但是该动态方法可以访问不是 public 或 internal（在 Visual Basic 中为 Friend）的成员。- 或 -该动态方法是匿名承载的，并且是在 <paramref name="skipVisibility" /> 设置为 false 的情况下构造的，但是它可以访问不是 public 的成员。- 或 -动态方法包含不可验证的代码。请参见 <see cref="T:System.Reflection.Emit.DynamicMethod" /> 的备注中的“验证”一节。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
    {
      if ((this.CallingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_CallToVarArg"));
      this.GetMethodDescriptor();
      Signature sig = new Signature(this.m_methodHandle, this.m_parameterTypes, this.m_returnType, this.CallingConvention);
      int length = sig.Arguments.Length;
      int num1 = parameters != null ? parameters.Length : 0;
      int num2 = num1;
      if (length != num2)
        throw new TargetParameterCountException(Environment.GetResourceString("Arg_ParmCnt"));
      object obj1;
      if (num1 > 0)
      {
        object[] arguments = this.CheckArguments(parameters, binder, invokeAttr, culture, sig);
        obj1 = RuntimeMethodHandle.InvokeMethod((object) null, arguments, sig, false);
        for (int index = 0; index < arguments.Length; ++index)
          parameters[index] = arguments[index];
      }
      else
        obj1 = RuntimeMethodHandle.InvokeMethod((object) null, (object[]) null, sig, false);
      GC.KeepAlive((object) this);
      return obj1;
    }

    /// <summary>返回应用于此方法的指定类型的自定义属性。</summary>
    /// <returns>一个对象数组，表示此方法的属性为 <paramref name="attributeType" /> 类型或派生自 <paramref name="attributeType" /> 类型。</returns>
    /// <param name="attributeType">一个 <see cref="T:System.Type" />，表示要返回的自定义特性类型。</param>
    /// <param name="inherit">为 true 则搜索方法的继承链以查找自定义属性；为 false 则仅检查当前方法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="attributeType" /> 为 null。</exception>
    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      return this.m_dynMethod.GetCustomAttributes(attributeType, inherit);
    }

    /// <summary>返回为此方法定义的所有自定义属性。</summary>
    /// <returns>表示此方法的所有自定义属性的对象数组。</returns>
    /// <param name="inherit">为 true 则搜索方法的继承链以查找自定义属性；为 false 则仅检查当前方法。</param>
    public override object[] GetCustomAttributes(bool inherit)
    {
      return this.m_dynMethod.GetCustomAttributes(inherit);
    }

    /// <summary>指示是否定义了指定的自定义特性类型。</summary>
    /// <returns>如果定义了指定的自定义特性类型，则为 true；否则为 false。</returns>
    /// <param name="attributeType">一个 <see cref="T:System.Type" />，表示要搜索的自定义特性类型。</param>
    /// <param name="inherit">为 true 则搜索方法的继承链以查找自定义属性；为 false 则仅检查当前方法。</param>
    public override bool IsDefined(Type attributeType, bool inherit)
    {
      return this.m_dynMethod.IsDefined(attributeType, inherit);
    }

    /// <summary>定义动态方法的参数。</summary>
    /// <returns>始终返回 null。</returns>
    /// <param name="position">该参数在参数列表中的位置。为参数编索引，第一个参数从数字 1 开始。</param>
    /// <param name="attributes">
    /// <see cref="T:System.Reflection.ParameterAttributes" /> 值的按位组合，用于指定参数的属性。</param>
    /// <param name="parameterName">参数名。名称可以为零长度字符串。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">此方法没有参数。- 或 -<paramref name="position" /> 小于 0。- 或 -<paramref name="position" /> 大于此方法的参数数目。</exception>
    public ParameterBuilder DefineParameter(int position, ParameterAttributes attributes, string parameterName)
    {
      if (position < 0 || position > this.m_parameterTypes.Length)
        throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_ParamSequence"));
      --position;
      if (position >= 0)
      {
        ParameterInfo[] parameterInfoArray = this.m_dynMethod.LoadParameters();
        int index1 = position;
        parameterInfoArray[index1].SetName(parameterName);
        int index2 = position;
        parameterInfoArray[index2].SetAttributes(attributes);
      }
      return (ParameterBuilder) null;
    }

    /// <summary>返回一个 <see cref="T:System.Reflection.Emit.DynamicILInfo" /> 对象，该对象可用于从元数据标记、范围和 Microsoft 中间语言 (MSIL) 流生成方法体。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.Emit.DynamicILInfo" /> 对象，可用于从元数据标记、范围和 MSIL 流生成方法体。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public DynamicILInfo GetDynamicILInfo()
    {
      new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
      if (this.m_DynamicILInfo != null)
        return this.m_DynamicILInfo;
      return this.GetDynamicILInfo(new DynamicScope());
    }

    [SecurityCritical]
    internal DynamicILInfo GetDynamicILInfo(DynamicScope scope)
    {
      if (this.m_DynamicILInfo == null)
      {
        byte[] signature = SignatureHelper.GetMethodSigHelper((Module) null, this.CallingConvention, this.ReturnType, (Type[]) null, (Type[]) null, (Type[]) this.m_parameterTypes, (Type[][]) null, (Type[][]) null).GetSignature(true);
        this.m_DynamicILInfo = new DynamicILInfo(scope, this, signature);
      }
      return this.m_DynamicILInfo;
    }

    /// <summary>为该方法返回一个具有默认 MSIL 流大小（64 字节）的 Microsoft 中间语言 (MSIL) 生成器。</summary>
    /// <returns>该方法的 <see cref="T:System.Reflection.Emit.ILGenerator" /> 对象。</returns>
    public ILGenerator GetILGenerator()
    {
      return this.GetILGenerator(64);
    }

    /// <summary>为方法返回一个具有指定 MSIL 流大小的 Microsoft 中间语言 (MSIL) 生成器。</summary>
    /// <returns>方法的 <see cref="T:System.Reflection.Emit.ILGenerator" /> 对象，具有指定的 MSIL 流大小。</returns>
    /// <param name="streamSize">MSIL 流的大小，以字节为单位。</param>
    [SecuritySafeCritical]
    public ILGenerator GetILGenerator(int streamSize)
    {
      if (this.m_ilGenerator == null)
        this.m_ilGenerator = new DynamicILGenerator(this, SignatureHelper.GetMethodSigHelper((Module) null, this.CallingConvention, this.ReturnType, (Type[]) null, (Type[]) null, (Type[]) this.m_parameterTypes, (Type[][]) null, (Type[][]) null).GetSignature(true), streamSize);
      return (ILGenerator) this.m_ilGenerator;
    }

    internal MethodInfo GetMethodInfo()
    {
      return (MethodInfo) this.m_dynMethod;
    }

    internal class RTDynamicMethod : MethodInfo
    {
      internal DynamicMethod m_owner;
      private ParameterInfo[] m_parameters;
      private string m_name;
      private MethodAttributes m_attributes;
      private CallingConventions m_callingConvention;

      public override string Name
      {
        get
        {
          return this.m_name;
        }
      }

      public override Type DeclaringType
      {
        get
        {
          return (Type) null;
        }
      }

      public override Type ReflectedType
      {
        get
        {
          return (Type) null;
        }
      }

      public override Module Module
      {
        get
        {
          return (Module) this.m_owner.m_module;
        }
      }

      public override RuntimeMethodHandle MethodHandle
      {
        get
        {
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod"));
        }
      }

      public override MethodAttributes Attributes
      {
        get
        {
          return this.m_attributes;
        }
      }

      public override CallingConventions CallingConvention
      {
        get
        {
          return this.m_callingConvention;
        }
      }

      public override bool IsSecurityCritical
      {
        get
        {
          return this.m_owner.IsSecurityCritical;
        }
      }

      public override bool IsSecuritySafeCritical
      {
        get
        {
          return this.m_owner.IsSecuritySafeCritical;
        }
      }

      public override bool IsSecurityTransparent
      {
        get
        {
          return this.m_owner.IsSecurityTransparent;
        }
      }

      public override Type ReturnType
      {
        get
        {
          return (Type) this.m_owner.m_returnType;
        }
      }

      public override ParameterInfo ReturnParameter
      {
        get
        {
          return (ParameterInfo) null;
        }
      }

      public override ICustomAttributeProvider ReturnTypeCustomAttributes
      {
        get
        {
          return this.GetEmptyCAHolder();
        }
      }

      private RTDynamicMethod()
      {
      }

      internal RTDynamicMethod(DynamicMethod owner, string name, MethodAttributes attributes, CallingConventions callingConvention)
      {
        this.m_owner = owner;
        this.m_name = name;
        this.m_attributes = attributes;
        this.m_callingConvention = callingConvention;
      }

      public override string ToString()
      {
        return this.ReturnType.FormatTypeName() + " " + this.FormatNameAndSig();
      }

      public override MethodInfo GetBaseDefinition()
      {
        return (MethodInfo) this;
      }

      public override ParameterInfo[] GetParameters()
      {
        ParameterInfo[] parameterInfoArray1 = this.LoadParameters();
        ParameterInfo[] parameterInfoArray2 = new ParameterInfo[parameterInfoArray1.Length];
        Array.Copy((Array) parameterInfoArray1, (Array) parameterInfoArray2, parameterInfoArray1.Length);
        return parameterInfoArray2;
      }

      public override MethodImplAttributes GetMethodImplementationFlags()
      {
        return MethodImplAttributes.NoInlining;
      }

      public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
      {
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), "this");
      }

      public override object[] GetCustomAttributes(Type attributeType, bool inherit)
      {
        if (attributeType == (Type) null)
          throw new ArgumentNullException("attributeType");
        if (!attributeType.IsAssignableFrom(typeof (MethodImplAttribute)))
          return EmptyArray<object>.Value;
        return new object[1]{ (object) new MethodImplAttribute(this.GetMethodImplementationFlags()) };
      }

      public override object[] GetCustomAttributes(bool inherit)
      {
        return new object[1]{ (object) new MethodImplAttribute(this.GetMethodImplementationFlags()) };
      }

      public override bool IsDefined(Type attributeType, bool inherit)
      {
        if (attributeType == (Type) null)
          throw new ArgumentNullException("attributeType");
        return attributeType.IsAssignableFrom(typeof (MethodImplAttribute));
      }

      internal ParameterInfo[] LoadParameters()
      {
        if (this.m_parameters == null)
        {
          Type[] typeArray = (Type[]) this.m_owner.m_parameterTypes;
          ParameterInfo[] parameterInfoArray = new ParameterInfo[typeArray.Length];
          for (int position = 0; position < typeArray.Length; ++position)
            parameterInfoArray[position] = (ParameterInfo) new RuntimeParameterInfo((MethodInfo) this, (string) null, typeArray[position], position);
          if (this.m_parameters == null)
            this.m_parameters = parameterInfoArray;
        }
        return this.m_parameters;
      }

      private ICustomAttributeProvider GetEmptyCAHolder()
      {
        return (ICustomAttributeProvider) new DynamicMethod.RTDynamicMethod.EmptyCAHolder();
      }

      private class EmptyCAHolder : ICustomAttributeProvider
      {
        internal EmptyCAHolder()
        {
        }

        object[] ICustomAttributeProvider.GetCustomAttributes(Type attributeType, bool inherit)
        {
          return EmptyArray<object>.Value;
        }

        object[] ICustomAttributeProvider.GetCustomAttributes(bool inherit)
        {
          return EmptyArray<object>.Value;
        }

        bool ICustomAttributeProvider.IsDefined(Type attributeType, bool inherit)
        {
          return false;
        }
      }
    }
  }
}
