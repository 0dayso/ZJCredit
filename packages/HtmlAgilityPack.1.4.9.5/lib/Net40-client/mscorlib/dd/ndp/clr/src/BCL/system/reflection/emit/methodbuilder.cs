// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.MethodBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Reflection.Emit
{
  /// <summary>定义并表示动态类的方法（或构造函数）。</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_MethodBuilder))]
  [ComVisible(true)]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public sealed class MethodBuilder : MethodInfo, _MethodBuilder
  {
    private int m_maxStack = 16;
    internal string m_strName;
    private MethodToken m_tkMethod;
    private ModuleBuilder m_module;
    internal TypeBuilder m_containingType;
    private int[] m_mdMethodFixups;
    private byte[] m_localSignature;
    internal LocalSymInfo m_localSymInfo;
    internal ILGenerator m_ilGenerator;
    private byte[] m_ubBody;
    private ExceptionHandler[] m_exceptions;
    private const int DefaultMaxStack = 16;
    internal bool m_bIsBaked;
    private bool m_bIsGlobalMethod;
    private bool m_fInitLocals;
    private MethodAttributes m_iAttributes;
    private CallingConventions m_callingConvention;
    private MethodImplAttributes m_dwMethodImplFlags;
    private SignatureHelper m_signature;
    internal Type[] m_parameterTypes;
    private ParameterBuilder m_retParam;
    private Type m_returnType;
    private Type[] m_returnTypeRequiredCustomModifiers;
    private Type[] m_returnTypeOptionalCustomModifiers;
    private Type[][] m_parameterTypeRequiredCustomModifiers;
    private Type[][] m_parameterTypeOptionalCustomModifiers;
    private GenericTypeParameterBuilder[] m_inst;
    private bool m_bIsGenMethDef;
    private List<MethodBuilder.SymCustomAttr> m_symCustomAttrs;
    internal bool m_canBeRuntimeImpl;
    internal bool m_isDllImport;

    internal int ExceptionHandlerCount
    {
      get
      {
        if (this.m_exceptions == null)
          return 0;
        return this.m_exceptions.Length;
      }
    }

    /// <summary>检索此方法的名称。</summary>
    /// <returns>只读。检索包含此方法的简单名称的字符串。</returns>
    public override string Name
    {
      get
      {
        return this.m_strName;
      }
    }

    internal int MetadataTokenInternal
    {
      get
      {
        return this.GetToken().Token;
      }
    }

    /// <summary>获取当前方法正在其中定义的模块。</summary>
    /// <returns>正在定义的当前 <see cref="T:System.Reflection.MemberInfo" /> 所表示的成员所在的 <see cref="T:System.Reflection.Module" />。</returns>
    public override Module Module
    {
      get
      {
        return this.m_containingType.Module;
      }
    }

    /// <summary>返回声明此方法的类型。</summary>
    /// <returns>只读。声明此方法的类型。</returns>
    public override Type DeclaringType
    {
      get
      {
        if (this.m_containingType.m_isHiddenGlobalType)
          return (Type) null;
        return (Type) this.m_containingType;
      }
    }

    /// <summary>返回此方法的返回类型的自定义属性。</summary>
    /// <returns>只读。此方法的返回类型的自定义属性。</returns>
    public override ICustomAttributeProvider ReturnTypeCustomAttributes
    {
      get
      {
        return (ICustomAttributeProvider) null;
      }
    }

    /// <summary>检索在反射中用于获取此对象的类。</summary>
    /// <returns>只读。用于获取此方法的类型。</returns>
    public override Type ReflectedType
    {
      get
      {
        return this.DeclaringType;
      }
    }

    /// <summary>检索此方法的特性。</summary>
    /// <returns>只读。检索此方法的 MethodAttributes。</returns>
    public override MethodAttributes Attributes
    {
      get
      {
        return this.m_iAttributes;
      }
    }

    /// <summary>返回此方法的调用约定。</summary>
    /// <returns>只读。该方法的调用约定。</returns>
    public override CallingConventions CallingConvention
    {
      get
      {
        return this.m_callingConvention;
      }
    }

    /// <summary>检索此方法的内部句柄。使用此句柄访问基础元数据句柄。</summary>
    /// <returns>只读。此方法的内部句柄。使用此句柄访问基础元数据句柄。</returns>
    /// <exception cref="T:System.NotSupportedException">目前不支持此方法。使用 <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> 检索此方法，并且对返回的 <see cref="T:System.Reflection.MethodInfo" /> 调用 <see cref="P:System.Reflection.MethodBase.MethodHandle" />。</exception>
    public override RuntimeMethodHandle MethodHandle
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
      }
    }

    /// <summary>在所有情况下均引发 <see cref="T:System.NotSupportedException" />。</summary>
    /// <returns>在所有情况下均引发 <see cref="T:System.NotSupportedException" />。</returns>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。此属性在动态程序集中不受支持。请参阅“备注”。</exception>
    public override bool IsSecurityCritical
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
      }
    }

    /// <summary>在所有情况下均引发 <see cref="T:System.NotSupportedException" />。</summary>
    /// <returns>在所有情况下均引发 <see cref="T:System.NotSupportedException" />。</returns>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。此属性在动态程序集中不受支持。请参阅“备注”。</exception>
    public override bool IsSecuritySafeCritical
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
      }
    }

    /// <summary>在所有情况下均引发 <see cref="T:System.NotSupportedException" />。</summary>
    /// <returns>在所有情况下均引发 <see cref="T:System.NotSupportedException" />。</returns>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。此属性在动态程序集中不受支持。请参阅“备注”。</exception>
    public override bool IsSecurityTransparent
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
      }
    }

    /// <summary>获取由此 <see cref="T:System.Reflection.Emit.MethodBuilder" /> 表示的方法的返回类型。</summary>
    /// <returns>该方法的返回类型。</returns>
    public override Type ReturnType
    {
      get
      {
        return this.m_returnType;
      }
    }

    /// <summary>获取一个 <see cref="T:System.Reflection.ParameterInfo" /> 对象，该对象包含有关方法的返回类型的信息（例如返回类型是否具有自定义修饰符）。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.ParameterInfo" /> 对象，包含有关返回类型的信息。</returns>
    /// <exception cref="T:System.InvalidOperationException">声明类型尚未创建。</exception>
    public override ParameterInfo ReturnParameter
    {
      get
      {
        if (!this.m_bIsBaked || (Type) this.m_containingType == (Type) null || this.m_containingType.BakedRuntimeType == (RuntimeType) null)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TypeNotCreated"));
        return this.m_containingType.GetMethod(this.m_strName, this.m_parameterTypes).ReturnParameter;
      }
    }

    /// <summary>获取一个值，该值指示当前 <see cref="T:System.Reflection.Emit.MethodBuilder" /> 对象是否表示泛型方法的定义。</summary>
    /// <returns>如果当前 <see cref="T:System.Reflection.Emit.MethodBuilder" /> 对象表示泛型方法的定义，则为 true；否则为 false。</returns>
    public override bool IsGenericMethodDefinition
    {
      get
      {
        return this.m_bIsGenMethDef;
      }
    }

    /// <summary>不支持此类型。</summary>
    /// <returns>不支持。</returns>
    /// <exception cref="T:System.NotSupportedException">基类不支持所调用的方法。</exception>
    public override bool ContainsGenericParameters
    {
      get
      {
        throw new NotSupportedException();
      }
    }

    /// <summary>获取指示该方法是否为泛型方法的值。</summary>
    /// <returns>如果该方法是泛型，则为 true；否则为 false。</returns>
    public override bool IsGenericMethod
    {
      get
      {
        return this.m_inst != null;
      }
    }

    /// <summary>获取或设置一个布尔值，该值指定此方法中的局部变量是否初始化为零。此属性的默认值为 true。</summary>
    /// <returns>如果应将此方法中的局部变量初始化为零，则为 true；否则为 false。</returns>
    /// <exception cref="T:System.InvalidOperationException">对于当前方法，<see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> 属性为 true，而 <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> 属性为 false。（获取或设置。）</exception>
    public bool InitLocals
    {
      get
      {
        this.ThrowIfGeneric();
        return this.m_fInitLocals;
      }
      set
      {
        this.ThrowIfGeneric();
        this.m_fInitLocals = value;
      }
    }

    /// <summary>检索方法的签名。</summary>
    /// <returns>只读。一个字符串，包含此 MethodBase 实例反映的方法的签名。</returns>
    public string Signature
    {
      [SecuritySafeCritical] get
      {
        return this.GetMethodSignature().ToString();
      }
    }

    internal MethodBuilder(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, ModuleBuilder mod, TypeBuilder type, bool bIsGlobalMethod)
    {
      this.Init(name, attributes, callingConvention, returnType, (Type[]) null, (Type[]) null, parameterTypes, (Type[][]) null, (Type[][]) null, mod, type, bIsGlobalMethod);
    }

    internal MethodBuilder(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, ModuleBuilder mod, TypeBuilder type, bool bIsGlobalMethod)
    {
      this.Init(name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, mod, type, bIsGlobalMethod);
    }

    private void Init(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, ModuleBuilder mod, TypeBuilder type, bool bIsGlobalMethod)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
      if ((int) name[0] == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_IllegalName"), "name");
      if ((Module) mod == (Module) null)
        throw new ArgumentNullException("mod");
      if (parameterTypes != null)
      {
        foreach (Type parameterType in parameterTypes)
        {
          if (parameterType == (Type) null)
            throw new ArgumentNullException("parameterTypes");
        }
      }
      this.m_strName = name;
      this.m_module = mod;
      this.m_containingType = type;
      this.m_returnType = returnType;
      if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
        callingConvention |= CallingConventions.HasThis;
      else if ((attributes & MethodAttributes.Virtual) != MethodAttributes.PrivateScope)
        throw new ArgumentException(Environment.GetResourceString("Arg_NoStaticVirtual"));
      if ((attributes & MethodAttributes.SpecialName) != MethodAttributes.SpecialName && (type.Attributes & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask && ((attributes & (MethodAttributes.Virtual | MethodAttributes.Abstract)) != (MethodAttributes.Virtual | MethodAttributes.Abstract) && (attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope))
        throw new ArgumentException(Environment.GetResourceString("Argument_BadAttributeOnInterfaceMethod"));
      this.m_callingConvention = callingConvention;
      if (parameterTypes != null)
      {
        this.m_parameterTypes = new Type[parameterTypes.Length];
        Array.Copy((Array) parameterTypes, (Array) this.m_parameterTypes, parameterTypes.Length);
      }
      else
        this.m_parameterTypes = (Type[]) null;
      this.m_returnTypeRequiredCustomModifiers = returnTypeRequiredCustomModifiers;
      this.m_returnTypeOptionalCustomModifiers = returnTypeOptionalCustomModifiers;
      this.m_parameterTypeRequiredCustomModifiers = parameterTypeRequiredCustomModifiers;
      this.m_parameterTypeOptionalCustomModifiers = parameterTypeOptionalCustomModifiers;
      this.m_iAttributes = attributes;
      this.m_bIsGlobalMethod = bIsGlobalMethod;
      this.m_bIsBaked = false;
      this.m_fInitLocals = true;
      this.m_localSymInfo = new LocalSymInfo();
      this.m_ubBody = (byte[]) null;
      this.m_ilGenerator = (ILGenerator) null;
      this.m_dwMethodImplFlags = MethodImplAttributes.IL;
    }

    internal void CheckContext(params Type[][] typess)
    {
      this.m_module.CheckContext(typess);
    }

    internal void CheckContext(params Type[] types)
    {
      this.m_module.CheckContext(types);
    }

    [SecurityCritical]
    internal void CreateMethodBodyHelper(ILGenerator il)
    {
      if (il == null)
        throw new ArgumentNullException("il");
      int num = 0;
      ModuleBuilder moduleBuilder = this.m_module;
      this.m_containingType.ThrowIfCreated();
      if (this.m_bIsBaked)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MethodHasBody"));
      if (il.m_methodBuilder != (MethodInfo) this && il.m_methodBuilder != (MethodInfo) null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadILGeneratorUsage"));
      this.ThrowIfShouldNotHaveBody();
      if (il.m_ScopeTree.m_iOpenScopeCount != 0)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OpenLocalVariableScope"));
      this.m_ubBody = il.BakeByteArray();
      this.m_mdMethodFixups = il.GetTokenFixups();
      __ExceptionInfo[] exceptions = il.GetExceptions();
      int numberOfExceptions = this.CalculateNumberOfExceptions(exceptions);
      if (numberOfExceptions > 0)
      {
        this.m_exceptions = new ExceptionHandler[numberOfExceptions];
        for (int index1 = 0; index1 < exceptions.Length; ++index1)
        {
          int[] filterAddresses = exceptions[index1].GetFilterAddresses();
          int[] catchAddresses = exceptions[index1].GetCatchAddresses();
          int[] catchEndAddresses = exceptions[index1].GetCatchEndAddresses();
          Type[] catchClass = exceptions[index1].GetCatchClass();
          int numberOfCatches = exceptions[index1].GetNumberOfCatches();
          int startAddress = exceptions[index1].GetStartAddress();
          int endAddress = exceptions[index1].GetEndAddress();
          int[] exceptionTypes = exceptions[index1].GetExceptionTypes();
          for (int index2 = 0; index2 < numberOfCatches; ++index2)
          {
            int exceptionTypeToken = 0;
            if (catchClass[index2] != (Type) null)
              exceptionTypeToken = moduleBuilder.GetTypeTokenInternal(catchClass[index2]).Token;
            switch (exceptionTypes[index2])
            {
              case 0:
              case 1:
              case 4:
                this.m_exceptions[num++] = new ExceptionHandler(startAddress, endAddress, filterAddresses[index2], catchAddresses[index2], catchEndAddresses[index2], exceptionTypes[index2], exceptionTypeToken);
                break;
              case 2:
                this.m_exceptions[num++] = new ExceptionHandler(startAddress, exceptions[index1].GetFinallyEndAddress(), filterAddresses[index2], catchAddresses[index2], catchEndAddresses[index2], exceptionTypes[index2], exceptionTypeToken);
                break;
            }
          }
        }
      }
      this.m_bIsBaked = true;
      if (moduleBuilder.GetSymWriter() == null)
        return;
      SymbolToken method = new SymbolToken(this.MetadataTokenInternal);
      ISymbolWriter symWriter = moduleBuilder.GetSymWriter();
      symWriter.OpenMethod(method);
      symWriter.OpenScope(0);
      if (this.m_symCustomAttrs != null)
      {
        foreach (MethodBuilder.SymCustomAttr mSymCustomAttr in this.m_symCustomAttrs)
          moduleBuilder.GetSymWriter().SetSymAttribute(new SymbolToken(this.MetadataTokenInternal), mSymCustomAttr.m_name, mSymCustomAttr.m_data);
      }
      if (this.m_localSymInfo != null)
        this.m_localSymInfo.EmitLocalSymInfo(symWriter);
      il.m_ScopeTree.EmitScopeTree(symWriter);
      il.m_LineNumberInfo.EmitLineNumberInfo(symWriter);
      symWriter.CloseScope(il.ILOffset);
      symWriter.CloseMethod();
    }

    internal void ReleaseBakedStructures()
    {
      if (!this.m_bIsBaked)
        return;
      this.m_ubBody = (byte[]) null;
      this.m_localSymInfo = (LocalSymInfo) null;
      this.m_mdMethodFixups = (int[]) null;
      this.m_localSignature = (byte[]) null;
      this.m_exceptions = (ExceptionHandler[]) null;
    }

    internal override Type[] GetParameterTypes()
    {
      if (this.m_parameterTypes == null)
        this.m_parameterTypes = EmptyArray<Type>.Value;
      return this.m_parameterTypes;
    }

    internal static Type GetMethodBaseReturnType(MethodBase method)
    {
      MethodInfo methodInfo;
      if ((methodInfo = method as MethodInfo) != (MethodInfo) null)
        return methodInfo.ReturnType;
      ConstructorInfo constructorInfo;
      if ((constructorInfo = method as ConstructorInfo) != (ConstructorInfo) null)
        return constructorInfo.GetReturnType();
      return (Type) null;
    }

    internal void SetToken(MethodToken token)
    {
      this.m_tkMethod = token;
    }

    internal byte[] GetBody()
    {
      return this.m_ubBody;
    }

    internal int[] GetTokenFixups()
    {
      return this.m_mdMethodFixups;
    }

    [SecurityCritical]
    internal SignatureHelper GetMethodSignature()
    {
      if (this.m_parameterTypes == null)
        this.m_parameterTypes = EmptyArray<Type>.Value;
      this.m_signature = SignatureHelper.GetMethodSigHelper((Module) this.m_module, this.m_callingConvention, this.m_inst != null ? this.m_inst.Length : 0, this.m_returnType == (Type) null ? typeof (void) : this.m_returnType, this.m_returnTypeRequiredCustomModifiers, this.m_returnTypeOptionalCustomModifiers, this.m_parameterTypes, this.m_parameterTypeRequiredCustomModifiers, this.m_parameterTypeOptionalCustomModifiers);
      return this.m_signature;
    }

    internal byte[] GetLocalSignature(out int signatureLength)
    {
      if (this.m_localSignature != null)
      {
        signatureLength = this.m_localSignature.Length;
        return this.m_localSignature;
      }
      if (this.m_ilGenerator != null && this.m_ilGenerator.m_localCount != 0)
        return this.m_ilGenerator.m_localSignature.InternalGetSignature(out signatureLength);
      return SignatureHelper.GetLocalVarSigHelper((Module) this.m_module).InternalGetSignature(out signatureLength);
    }

    internal int GetMaxStack()
    {
      if (this.m_ilGenerator != null)
        return this.m_ilGenerator.GetMaxStackSize() + this.ExceptionHandlerCount;
      return this.m_maxStack;
    }

    internal ExceptionHandler[] GetExceptionHandlers()
    {
      return this.m_exceptions;
    }

    internal int CalculateNumberOfExceptions(__ExceptionInfo[] excp)
    {
      int num = 0;
      if (excp == null)
        return 0;
      for (int index = 0; index < excp.Length; ++index)
        num += excp[index].GetNumberOfCatches();
      return num;
    }

    internal bool IsTypeCreated()
    {
      if ((Type) this.m_containingType != (Type) null)
        return this.m_containingType.IsCreated();
      return false;
    }

    internal TypeBuilder GetTypeBuilder()
    {
      return this.m_containingType;
    }

    internal ModuleBuilder GetModuleBuilder()
    {
      return this.m_module;
    }

    /// <summary>确定给定对象是否等于该实例。</summary>
    /// <returns>如果 <paramref name="obj" /> 为 MethodBuilder 的实例并且等于此对象，则为 true；否则为 false。</returns>
    /// <param name="obj">与此 MethodBuilder 实例进行比较的对象。</param>
    [SecuritySafeCritical]
    public override bool Equals(object obj)
    {
      return obj is MethodBuilder && this.m_strName.Equals(((MethodBuilder) obj).m_strName) && (this.m_iAttributes == ((MethodBuilder) obj).m_iAttributes && ((MethodBuilder) obj).GetMethodSignature().Equals((object) this.GetMethodSignature()));
    }

    /// <summary>获取此方法的哈希代码。</summary>
    /// <returns>此方法的哈希代码。</returns>
    public override int GetHashCode()
    {
      return this.m_strName.GetHashCode();
    }

    /// <summary>以字符串形式返回此 MethodBuilder 实例。</summary>
    /// <returns>返回一个字符串，它包含此方法的名称、特性、方法签名、异常和本地签名，后跟当前 Microsoft 中间语言 (MSIL) 流。</returns>
    [SecuritySafeCritical]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder(1000);
      string str1 = "Name: " + this.m_strName + " " + Environment.NewLine;
      stringBuilder.Append(str1);
      string str2 = "Attributes: " + (object) this.m_iAttributes + Environment.NewLine;
      stringBuilder.Append(str2);
      string str3 = "Method Signature: " + (object) this.GetMethodSignature() + Environment.NewLine;
      stringBuilder.Append(str3);
      string newLine = Environment.NewLine;
      stringBuilder.Append(newLine);
      return stringBuilder.ToString();
    }

    /// <summary>在给定联编程序的约束下，对给定的对象动态调用此实例所反射的方法，并传递指定的参数。</summary>
    /// <returns>返回包含被调用方法的返回值的对象。</returns>
    /// <param name="obj">对其调用指定方法的对象。如果此方法是静态的，则忽略此参数。</param>
    /// <param name="invokeAttr">这必须是来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志：InvokeMethod、NonPublic 等。</param>
    /// <param name="binder">一个启用绑定、参数类型强制、成员调用以及通过反射进行 MemberInfo 对象检索的对象。如果 binder 为 null，则使用默认联编程序。有关更多详细信息，请参见 <see cref="T:System.Reflection.Binder" />。</param>
    /// <param name="parameters">参数列表。这是一个参数数组，这些参数与要调用的方法的参数具有相同的数目、顺序和类型。如果没有参数，则它应为 null。</param>
    /// <param name="culture">用于控制类型强制的 <see cref="T:System.Globalization.CultureInfo" /> 的实例。如果这是 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。（请注意，这对于某些转换很必要，例如，从表示 1000 的 <see cref="T:System.String" /> 转换为 <see cref="T:System.Double" /> 值，因为不同的区域性以不同的形式表示 1000。）</param>
    /// <exception cref="T:System.NotSupportedException">目前不支持此方法。使用 <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> 检索此方法，并且对返回的 <see cref="T:System.Reflection.MethodInfo" /> 调用 <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" />。</exception>
    public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>返回此方法的实现标志。</summary>
    /// <returns>返回此方法的实现标志。</returns>
    public override MethodImplAttributes GetMethodImplementationFlags()
    {
      return this.m_dwMethodImplFlags;
    }

    /// <summary>返回方法的基实现。</summary>
    /// <returns>此方法的基实现。</returns>
    public override MethodInfo GetBaseDefinition()
    {
      return (MethodInfo) this;
    }

    /// <summary>返回此方法的参数。</summary>
    /// <returns>表示此方法的参数的 ParameterInfo 对象数组。</returns>
    /// <exception cref="T:System.NotSupportedException">目前不支持此方法。使用 <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> 检索此方法，并且对返回的 <see cref="T:System.Reflection.MethodInfo" /> 调用 GetParameters。</exception>
    public override ParameterInfo[] GetParameters()
    {
      if (!this.m_bIsBaked || (Type) this.m_containingType == (Type) null || this.m_containingType.BakedRuntimeType == (RuntimeType) null)
        throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_TypeNotCreated"));
      return this.m_containingType.GetMethod(this.m_strName, this.m_parameterTypes).GetParameters();
    }

    /// <summary>返回为此方法定义的所有自定义属性。</summary>
    /// <returns>返回表示此方法的所有自定义特性的对象数组。</returns>
    /// <param name="inherit">指定是否搜索此成员的继承链以查找自定义属性。</param>
    /// <exception cref="T:System.NotSupportedException">目前不支持此方法。使用 <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> 检索此方法，并且对返回的 <see cref="T:System.Reflection.MethodInfo" /> 调用 <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" />。</exception>
    public override object[] GetCustomAttributes(bool inherit)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>返回由给定类型标识的自定义属性。</summary>
    /// <returns>返回一个数组，该数组包含表示此方法的特性的类型为 <paramref name="attributeType" /> 的对象。</returns>
    /// <param name="attributeType">自定义特性类型。</param>
    /// <param name="inherit">指定是否搜索此成员的继承链以查找自定义属性。</param>
    /// <exception cref="T:System.NotSupportedException">目前不支持此方法。使用 <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> 检索此方法，并且对返回的 <see cref="T:System.Reflection.MethodInfo" /> 调用 <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" />。</exception>
    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>检查是否定义了指定的自定义特性类型。</summary>
    /// <returns>如果定义了指定的自定义特性类型，则为 true；否则为 false。</returns>
    /// <param name="attributeType">自定义特性类型。</param>
    /// <param name="inherit">指定是否搜索此成员的继承链以查找自定义属性。</param>
    /// <exception cref="T:System.NotSupportedException">目前不支持此方法。使用 <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> 检索此方法，并且对返回的 <see cref="T:System.Reflection.MethodInfo" /> 调用 <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" />。</exception>
    public override bool IsDefined(Type attributeType, bool inherit)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>返回此方法。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.Emit.MethodBuilder" /> 的当前实例。</returns>
    /// <exception cref="T:System.InvalidOperationException">当前方法不是泛型。即，<see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> 属性返回 false。</exception>
    public override MethodInfo GetGenericMethodDefinition()
    {
      if (!this.IsGenericMethod)
        throw new InvalidOperationException();
      return (MethodInfo) this;
    }

    /// <summary>返回一个 <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> 对象的数组，这些对象表示方法的类型参数（如果该方法是泛型方法）。</summary>
    /// <returns>如果该方法为泛型方法，则为表示类型参数的 <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> 对象的数组；如果该方法不是泛型，则为 null。</returns>
    public override Type[] GetGenericArguments()
    {
      return (Type[]) this.m_inst;
    }

    /// <summary>返回一个使用指定的泛型类型参数从当前泛型方法定义构造的泛型方法。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.MethodInfo" />，表示使用指定的泛型类型参数从当前泛型方法定义构造的泛型方法。</returns>
    /// <param name="typeArguments">一个 <see cref="T:System.Type" /> 对象的数组，这些对象表示泛型方法的类型参数。</param>
    public override MethodInfo MakeGenericMethod(params Type[] typeArguments)
    {
      return MethodBuilderInstantiation.MakeGenericMethod((MethodInfo) this, typeArguments);
    }

    /// <summary>设置当前方法的泛型类型参数的数目，指定这些参数的名称，并返回一个 <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> 对象的数组，这些对象可用于定义这些参数的约束。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> 对象的数组，这些对象表示泛型方法的类型参数。</returns>
    /// <param name="names">一个字符串数组，这些字符串表示泛型类型参数的名称。</param>
    /// <exception cref="T:System.InvalidOperationException">已为此方法定义了泛型类型参数。- 或 -该方法已经完成。- 或 -已为当前方法调用了 <see cref="M:System.Reflection.Emit.MethodBuilder.SetImplementationFlags(System.Reflection.MethodImplAttributes)" /> 方法。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="names" /> 为 null。- 或 -<paramref name="names" /> 的一个元素为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="names" /> 为空数组。</exception>
    public GenericTypeParameterBuilder[] DefineGenericParameters(params string[] names)
    {
      if (names == null)
        throw new ArgumentNullException("names");
      if (names.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Arg_EmptyArray"), "names");
      if (this.m_inst != null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_GenericParametersAlreadySet"));
      for (int index = 0; index < names.Length; ++index)
      {
        if (names[index] == null)
          throw new ArgumentNullException("names");
      }
      if (this.m_tkMethod.Token != 0)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MethodBuilderBaked"));
      this.m_bIsGenMethDef = true;
      this.m_inst = new GenericTypeParameterBuilder[names.Length];
      for (int genParamPos = 0; genParamPos < names.Length; ++genParamPos)
        this.m_inst[genParamPos] = new GenericTypeParameterBuilder(new TypeBuilder(names[genParamPos], genParamPos, this));
      return this.m_inst;
    }

    internal void ThrowIfGeneric()
    {
      if (this.IsGenericMethod && !this.IsGenericMethodDefinition)
        throw new InvalidOperationException();
    }

    /// <summary>返回表示此方法的标记的 MethodToken。</summary>
    /// <returns>返回此方法的 MethodToken。</returns>
    [SecuritySafeCritical]
    public MethodToken GetToken()
    {
      if (this.m_tkMethod.Token != 0)
        return this.m_tkMethod;
      MethodToken methodToken = new MethodToken(0);
      lock (this.m_containingType.m_listMethods)
      {
        if (this.m_tkMethod.Token != 0)
          return this.m_tkMethod;
        int local_1;
        for (local_1 = this.m_containingType.m_lastTokenizedMethod + 1; local_1 < this.m_containingType.m_listMethods.Count; ++local_1)
        {
          MethodBuilder temp_28 = this.m_containingType.m_listMethods[local_1];
          methodToken = temp_28.GetTokenNoLock();
          if ((MethodInfo) temp_28 == (MethodInfo) this)
            break;
        }
        this.m_containingType.m_lastTokenizedMethod = local_1;
      }
      return methodToken;
    }

    [SecurityCritical]
    private MethodToken GetTokenNoLock()
    {
      int length;
      int num = TypeBuilder.DefineMethod(this.m_module.GetNativeHandle(), this.m_containingType.MetadataTokenInternal, this.m_strName, this.GetMethodSignature().InternalGetSignature(out length), length, this.Attributes);
      this.m_tkMethod = new MethodToken(num);
      if (this.m_inst != null)
      {
        foreach (GenericTypeParameterBuilder parameterBuilder in this.m_inst)
        {
          if (!parameterBuilder.m_type.IsCreated())
            parameterBuilder.m_type.CreateType();
        }
      }
      TypeBuilder.SetMethodImpl(this.m_module.GetNativeHandle(), num, this.m_dwMethodImplFlags);
      return this.m_tkMethod;
    }

    /// <summary>为方法设置参数的数目和类型。</summary>
    /// <param name="parameterTypes">表示参数类型的 <see cref="T:System.Type" /> 对象的数组。</param>
    /// <exception cref="T:System.InvalidOperationException">当前方法是泛型方法，但不是泛型方法定义。即，<see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> 属性为 true，但 <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> 属性为 false。</exception>
    public void SetParameters(params Type[] parameterTypes)
    {
      this.CheckContext(parameterTypes);
      this.SetSignature((Type) null, (Type[]) null, (Type[]) null, parameterTypes, (Type[][]) null, (Type[][]) null);
    }

    /// <summary>设置该方法的返回类型。</summary>
    /// <param name="returnType">表示该方法的返回类型的 <see cref="T:System.Type" /> 对象。</param>
    /// <exception cref="T:System.InvalidOperationException">当前方法是泛型方法，但不是泛型方法定义。即，<see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> 属性为 true，但 <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> 属性为 false。</exception>
    public void SetReturnType(Type returnType)
    {
      this.CheckContext(new Type[1]{ returnType });
      this.SetSignature(returnType, (Type[]) null, (Type[]) null, (Type[]) null, (Type[][]) null, (Type[][]) null);
    }

    /// <summary>设置方法的签名，包括返回类型、参数类型以及该返回类型和参数类型的必需的和可选的自定义修饰符。</summary>
    /// <param name="returnType">该方法的返回类型。</param>
    /// <param name="returnTypeRequiredCustomModifiers">一个类型数组，表示该方法的返回类型的必需的自定义修饰符（如，<see cref="T:System.Runtime.CompilerServices.IsConst" />）。如果返回类型没有必需的自定义修饰符，请指定 null。</param>
    /// <param name="returnTypeOptionalCustomModifiers">一个类型数组，表示该方法的返回类型的可选自定义修饰符（例如，<see cref="T:System.Runtime.CompilerServices.IsConst" />）。如果返回类型没有可选的自定义修饰符，请指定 null。</param>
    /// <param name="parameterTypes">该方法的参数的类型。</param>
    /// <param name="parameterTypeRequiredCustomModifiers">由类型数组组成的数组。每个类型数组均表示相应参数所必需的自定义修饰符，如 <see cref="T:System.Runtime.CompilerServices.IsConst" />。如果某个特定参数没有必需的自定义修饰符，请指定 null，而不指定类型数组。如果没有参数具有必需的自定义修饰符，请指定 null，而不指定由数组构成的数组。</param>
    /// <param name="parameterTypeOptionalCustomModifiers">由类型数组组成的数组。每个类型数组均表示相应参数的可选自定义修饰符，如 <see cref="T:System.Runtime.CompilerServices.IsConst" />。如果某个特定参数没有可选的自定义修饰符，请指定 null，而不指定类型数组。如果没有参数具有可选的自定义修饰符，请指定 null，而不指定由数组构成的数组。</param>
    /// <exception cref="T:System.InvalidOperationException">当前方法是泛型方法，但不是泛型方法定义。即，<see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> 属性为 true，但 <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> 属性为 false。</exception>
    public void SetSignature(Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
    {
      if (this.m_tkMethod.Token != 0)
        return;
      this.CheckContext(new Type[1]{ returnType });
      this.CheckContext(returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes);
      this.CheckContext(parameterTypeRequiredCustomModifiers);
      this.CheckContext(parameterTypeOptionalCustomModifiers);
      this.ThrowIfGeneric();
      if (returnType != (Type) null)
        this.m_returnType = returnType;
      if (parameterTypes != null)
      {
        this.m_parameterTypes = new Type[parameterTypes.Length];
        Array.Copy((Array) parameterTypes, (Array) this.m_parameterTypes, parameterTypes.Length);
      }
      this.m_returnTypeRequiredCustomModifiers = returnTypeRequiredCustomModifiers;
      this.m_returnTypeOptionalCustomModifiers = returnTypeOptionalCustomModifiers;
      this.m_parameterTypeRequiredCustomModifiers = parameterTypeRequiredCustomModifiers;
      this.m_parameterTypeOptionalCustomModifiers = parameterTypeOptionalCustomModifiers;
    }

    /// <summary>设置参数属性以及此方法的参数名称或此方法返回值的名称。返回可用于应用自定义属性的 ParameterBuilder。</summary>
    /// <returns>返回一个 ParameterBuilder 对象，该对象表示此方法的参数或此方法的返回值。</returns>
    /// <param name="position">该参数在参数列表中的位置。为参数编索引，第一个参数从数字 1 开始；数字 0 表示方法的返回值。</param>
    /// <param name="attributes">参数的参数属性。</param>
    /// <param name="strParamName">参数名。名称可以为 null 字符串。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">此方法没有参数。- 或 -<paramref name="position" /> 小于零。- 或 -<paramref name="position" /> 大于此方法的参数数目。</exception>
    /// <exception cref="T:System.InvalidOperationException">该包含类型是以前使用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建的。- 或 -对于当前方法，<see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> 属性为 true，而 <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> 属性为 false。</exception>
    [SecuritySafeCritical]
    public ParameterBuilder DefineParameter(int position, ParameterAttributes attributes, string strParamName)
    {
      if (position < 0)
        throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_ParamSequence"));
      this.ThrowIfGeneric();
      this.m_containingType.ThrowIfCreated();
      if (position > 0 && (this.m_parameterTypes == null || position > this.m_parameterTypes.Length))
        throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_ParamSequence"));
      attributes &= ~ParameterAttributes.ReservedMask;
      return new ParameterBuilder(this, position, attributes, strParamName);
    }

    /// <summary>设置此方法的返回类型的封送处理信息。</summary>
    /// <param name="unmanagedMarshal">此方法的返回类型的封送处理信息。</param>
    /// <exception cref="T:System.InvalidOperationException">该包含类型是以前使用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建的。- 或 -对于当前方法，<see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> 属性为 true，而 <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> 属性为 false。</exception>
    [SecuritySafeCritical]
    [Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    public void SetMarshal(UnmanagedMarshal unmanagedMarshal)
    {
      this.ThrowIfGeneric();
      this.m_containingType.ThrowIfCreated();
      if (this.m_retParam == null)
        this.m_retParam = new ParameterBuilder(this, 0, ParameterAttributes.None, (string) null);
      this.m_retParam.SetMarshal(unmanagedMarshal);
    }

    /// <summary>使用 Blob 设置符号化自定义特性。</summary>
    /// <param name="name">符号自定义特性的名称。</param>
    /// <param name="data">表示符号自定义特性值的字节 Blob。</param>
    /// <exception cref="T:System.InvalidOperationException">该包含类型是以前使用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建的。- 或 -包含此方法的模块不是调试模块。- 或 -对于当前方法，<see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> 属性为 true，而 <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> 属性为 false。</exception>
    public void SetSymCustomAttribute(string name, byte[] data)
    {
      this.ThrowIfGeneric();
      this.m_containingType.ThrowIfCreated();
      if (this.m_module.GetSymWriter() == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotADebugModule"));
      if (this.m_symCustomAttrs == null)
        this.m_symCustomAttrs = new List<MethodBuilder.SymCustomAttr>();
      this.m_symCustomAttrs.Add(new MethodBuilder.SymCustomAttr(name, data));
    }

    /// <summary>为此方法添加声明性安全。</summary>
    /// <param name="action">要执行的安全操作（Demand、Assert 等）。</param>
    /// <param name="pset">操作应用于的权限集。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="action" /> 无效（RequestMinimum、RequestOptional 和 RequestRefuse 无效）。</exception>
    /// <exception cref="T:System.InvalidOperationException">已经使用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建了该包含类型。- 或 -权限集 <paramref name="pset" /> 包含以前由 <see cref="M:System.Reflection.Emit.MethodBuilder.AddDeclarativeSecurity(System.Security.Permissions.SecurityAction,System.Security.PermissionSet)" /> 添加的操作。- 或 -对于当前方法，<see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> 属性为 true，而 <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> 属性为 false。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pset" /> 为 null。</exception>
    [SecuritySafeCritical]
    public void AddDeclarativeSecurity(SecurityAction action, PermissionSet pset)
    {
      if (pset == null)
        throw new ArgumentNullException("pset");
      this.ThrowIfGeneric();
      if (!Enum.IsDefined(typeof (SecurityAction), (object) action) || action == SecurityAction.RequestMinimum || (action == SecurityAction.RequestOptional || action == SecurityAction.RequestRefuse))
        throw new ArgumentOutOfRangeException("action");
      this.m_containingType.ThrowIfCreated();
      byte[] blob = (byte[]) null;
      int cb = 0;
      if (!pset.IsEmpty())
      {
        blob = pset.EncodeXml();
        cb = blob.Length;
      }
      TypeBuilder.AddDeclarativeSecurity(this.m_module.GetNativeHandle(), this.MetadataTokenInternal, action, blob, cb);
    }

    /// <summary>使用指定的 Microsoft 中间语言 (MSIL) 指令的字节数组创建方法体。</summary>
    /// <param name="il">包含有效 MSIL 指令的数组。</param>
    /// <param name="maxStack">最大堆栈计算深度。</param>
    /// <param name="localSignature">包含序列化的本地变量结构的字节数组。如果方法没有局部变量，请指定 null。</param>
    /// <param name="exceptionHandlers">包含方法的异常处理程序的集合。如果方法没有异常管理器，请指定 null。</param>
    /// <param name="tokenFixups">表示 <paramref name="il" /> 中偏移量的值的集合，每个偏移量都指定了可以修改的标记的开头部分。如果方法没有必须修改的标记，请指定 null。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="il" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="maxStack" /> 为负数。- 或 -<paramref name="exceptionHandlers" /> 的一个指定偏移超出 <paramref name="il" /> 。- 或 -<paramref name="tokenFixups" /> 的一个指定偏移 <paramref name="il" /> 之外。</exception>
    /// <exception cref="T:System.InvalidOperationException">该包含类型是以前使用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 方法创建的。- 或 -此方法之前在此 <see cref="T:System.Reflection.Emit.MethodBuilder" /> 对象上已调用。</exception>
    public void SetMethodBody(byte[] il, int maxStack, byte[] localSignature, IEnumerable<ExceptionHandler> exceptionHandlers, IEnumerable<int> tokenFixups)
    {
      if (il == null)
        throw new ArgumentNullException("il", Environment.GetResourceString("ArgumentNull_Array"));
      if (maxStack < 0)
        throw new ArgumentOutOfRangeException("maxStack", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (this.m_bIsBaked)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MethodBaked"));
      this.m_containingType.ThrowIfCreated();
      this.ThrowIfGeneric();
      byte[] numArray1 = (byte[]) null;
      ExceptionHandler[] exceptionHandlers1 = (ExceptionHandler[]) null;
      int[] numArray2 = (int[]) null;
      byte[] numArray3 = (byte[]) il.Clone();
      if (localSignature != null)
        numArray1 = (byte[]) localSignature.Clone();
      if (exceptionHandlers != null)
      {
        exceptionHandlers1 = MethodBuilder.ToArray<ExceptionHandler>(exceptionHandlers);
        MethodBuilder.CheckExceptionHandlerRanges(exceptionHandlers1, numArray3.Length);
      }
      if (tokenFixups != null)
      {
        numArray2 = MethodBuilder.ToArray<int>(tokenFixups);
        int num = numArray3.Length - 4;
        for (int index = 0; index < numArray2.Length; ++index)
        {
          if (numArray2[index] < 0 || numArray2[index] > num)
            throw new ArgumentOutOfRangeException("tokenFixups[" + (object) index + "]", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 0, (object) num));
        }
      }
      this.m_ubBody = numArray3;
      this.m_localSignature = numArray1;
      this.m_exceptions = exceptionHandlers1;
      this.m_mdMethodFixups = numArray2;
      this.m_maxStack = maxStack;
      this.m_ilGenerator = (ILGenerator) null;
      this.m_bIsBaked = true;
    }

    private static T[] ToArray<T>(IEnumerable<T> sequence)
    {
      T[] objArray = sequence as T[];
      if (objArray != null)
        return (T[]) objArray.Clone();
      return new List<T>(sequence).ToArray();
    }

    private static void CheckExceptionHandlerRanges(ExceptionHandler[] exceptionHandlers, int maxOffset)
    {
      for (int index = 0; index < exceptionHandlers.Length; ++index)
      {
        ExceptionHandler exceptionHandler = exceptionHandlers[index];
        if (exceptionHandler.m_filterOffset > maxOffset || exceptionHandler.m_tryEndOffset > maxOffset || exceptionHandler.m_handlerEndOffset > maxOffset)
          throw new ArgumentOutOfRangeException("exceptionHandlers[" + (object) index + "]", Environment.GetResourceString("ArgumentOutOfRange_Range", (object) 0, (object) maxOffset));
        if (exceptionHandler.Kind == ExceptionHandlingClauseOptions.Clause && exceptionHandler.ExceptionTypeToken == 0)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidTypeToken", (object) exceptionHandler.ExceptionTypeToken), "exceptionHandlers[" + (object) index + "]");
      }
    }

    /// <summary>使用所提供的 Microsoft 中间语言 (MSIL) 指令的字节数组创建方法体。</summary>
    /// <param name="il">包含有效 MSIL 指令的数组。如果该参数为 null，则将清除此方法体。</param>
    /// <param name="count">MSIL 数组中的有效字节数。如果 MSIL 为 null，则忽略该值。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="count" /> 不在提供的 MSIL 指令数组的索引范围内，并且 <paramref name="il" /> 不为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">该包含类型是以前使用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建的。- 或 -此方法先前曾以一个非 null 的 <paramref name="il" /> 参数对此 MethodBuilder 调用过。- 或 -对于当前方法，<see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> 属性为 true，而 <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> 属性为 false。</exception>
    public void CreateMethodBody(byte[] il, int count)
    {
      this.ThrowIfGeneric();
      if (this.m_bIsBaked)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MethodBaked"));
      this.m_containingType.ThrowIfCreated();
      if (il != null && (count < 0 || count > il.Length))
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (il == null)
      {
        this.m_ubBody = (byte[]) null;
      }
      else
      {
        this.m_ubBody = new byte[count];
        Array.Copy((Array) il, (Array) this.m_ubBody, count);
        this.m_localSignature = (byte[]) null;
        this.m_exceptions = (ExceptionHandler[]) null;
        this.m_mdMethodFixups = (int[]) null;
        this.m_maxStack = 16;
        this.m_bIsBaked = true;
      }
    }

    /// <summary>设置此方法的实现标志。</summary>
    /// <param name="attributes">要设置的实现标志。</param>
    /// <exception cref="T:System.InvalidOperationException">该包含类型是以前使用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建的。- 或 -对于当前方法，<see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> 属性为 true，而 <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> 属性为 false。</exception>
    [SecuritySafeCritical]
    public void SetImplementationFlags(MethodImplAttributes attributes)
    {
      this.ThrowIfGeneric();
      this.m_containingType.ThrowIfCreated();
      this.m_dwMethodImplFlags = attributes;
      this.m_canBeRuntimeImpl = true;
      TypeBuilder.SetMethodImpl(this.m_module.GetNativeHandle(), this.MetadataTokenInternal, attributes);
    }

    /// <summary>为此方法返回具有 64 字节大小的默认 Microsoft 中间语言 (MSIL) 流的 ILGenerator。</summary>
    /// <returns>返回此方法的 ILGenerator 对象。</returns>
    /// <exception cref="T:System.InvalidOperationException">此方法不应有主体，这是由其 <see cref="T:System.Reflection.MethodAttributes" /> 或 <see cref="T:System.Reflection.MethodImplAttributes" /> 标志决定的，例如，它具有 <see cref="F:System.Reflection.MethodAttributes.PinvokeImpl" /> 标志。- 或 -此方法是泛型方法，但不是泛型方法定义。即，<see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> 属性为 true，但 <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> 属性为 false。</exception>
    public ILGenerator GetILGenerator()
    {
      this.ThrowIfGeneric();
      this.ThrowIfShouldNotHaveBody();
      if (this.m_ilGenerator == null)
        this.m_ilGenerator = new ILGenerator((MethodInfo) this);
      return this.m_ilGenerator;
    }

    /// <summary>为此方法返回具有指定 Microsoft 中间语言 (MSIL) 流大小的 ILGenerator。</summary>
    /// <returns>返回此方法的 ILGenerator 对象。</returns>
    /// <param name="size">MSIL 流的大小，以字节为单位。</param>
    /// <exception cref="T:System.InvalidOperationException">此方法不应有主体，这是由其 <see cref="T:System.Reflection.MethodAttributes" /> 或 <see cref="T:System.Reflection.MethodImplAttributes" /> 标志决定的，例如，它具有 <see cref="F:System.Reflection.MethodAttributes.PinvokeImpl" /> 标志。- 或 -此方法是泛型方法，但不是泛型方法定义。即，<see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> 属性为 true，但 <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> 属性为 false。</exception>
    public ILGenerator GetILGenerator(int size)
    {
      this.ThrowIfGeneric();
      this.ThrowIfShouldNotHaveBody();
      if (this.m_ilGenerator == null)
        this.m_ilGenerator = new ILGenerator((MethodInfo) this, size);
      return this.m_ilGenerator;
    }

    private void ThrowIfShouldNotHaveBody()
    {
      if ((this.m_dwMethodImplFlags & MethodImplAttributes.CodeTypeMask) != MethodImplAttributes.IL || (this.m_dwMethodImplFlags & MethodImplAttributes.ManagedMask) != MethodImplAttributes.IL || ((this.m_iAttributes & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope || this.m_isDllImport))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ShouldNotHaveMethodBody"));
    }

    /// <summary>返回对包含此方法的模块的引用。</summary>
    /// <returns>返回对包含此方法的模块的引用。</returns>
    public Module GetModule()
    {
      return (Module) this.GetModuleBuilder();
    }

    /// <summary>使用指定的自定义属性 Blob 设置自定义属性。</summary>
    /// <param name="con">自定义属性的构造函数。</param>
    /// <param name="binaryAttribute">表示属性的字节 Blob。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="con" /> 或 <paramref name="binaryAttribute" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">对于当前方法，<see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> 属性为 true，而 <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> 属性为 false。</exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
    {
      if (con == (ConstructorInfo) null)
        throw new ArgumentNullException("con");
      if (binaryAttribute == null)
        throw new ArgumentNullException("binaryAttribute");
      this.ThrowIfGeneric();
      TypeBuilder.DefineCustomAttribute(this.m_module, this.MetadataTokenInternal, this.m_module.GetConstructorToken(con).Token, binaryAttribute, false, false);
      if (!this.IsKnownCA(con))
        return;
      this.ParseCA(con, binaryAttribute);
    }

    /// <summary>使用自定义属性生成器设置自定义属性。</summary>
    /// <param name="customBuilder">对自定义属性进行描述的帮助器类的实例。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="customBuilder" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">对于当前方法，<see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> 属性为 true，而 <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> 属性为 false。</exception>
    [SecuritySafeCritical]
    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
      if (customBuilder == null)
        throw new ArgumentNullException("customBuilder");
      this.ThrowIfGeneric();
      customBuilder.CreateCustomAttribute(this.m_module, this.MetadataTokenInternal);
      if (!this.IsKnownCA(customBuilder.m_con))
        return;
      this.ParseCA(customBuilder.m_con, customBuilder.m_blob);
    }

    private bool IsKnownCA(ConstructorInfo con)
    {
      Type declaringType = con.DeclaringType;
      return declaringType == typeof (MethodImplAttribute) || declaringType == typeof (DllImportAttribute);
    }

    private void ParseCA(ConstructorInfo con, byte[] blob)
    {
      Type declaringType = con.DeclaringType;
      if (declaringType == typeof (MethodImplAttribute))
      {
        this.m_canBeRuntimeImpl = true;
      }
      else
      {
        if (!(declaringType == typeof (DllImportAttribute)))
          return;
        this.m_canBeRuntimeImpl = true;
        this.m_isDllImport = true;
      }
    }

    void _MethodBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _MethodBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _MethodBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _MethodBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }

    private struct SymCustomAttr
    {
      public string m_name;
      public byte[] m_data;

      public SymCustomAttr(string name, byte[] data)
      {
        this.m_name = name;
        this.m_data = data;
      }
    }
  }
}
