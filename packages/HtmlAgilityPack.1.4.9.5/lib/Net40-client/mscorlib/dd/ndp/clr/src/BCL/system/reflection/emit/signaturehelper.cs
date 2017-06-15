// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.SignatureHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.Reflection.Emit
{
  /// <summary>提供生成签名的方法。</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_SignatureHelper))]
  [ComVisible(true)]
  public sealed class SignatureHelper : _SignatureHelper
  {
    private const int NO_SIZE_IN_SIG = -1;
    private byte[] m_signature;
    private int m_currSig;
    private int m_sizeLoc;
    private ModuleBuilder m_module;
    private bool m_sigDone;
    private int m_argCount;

    internal int ArgumentCount
    {
      get
      {
        return this.m_argCount;
      }
    }

    private SignatureHelper(Module mod, System.Reflection.MdSigCallingConvention callingConvention)
    {
      this.Init(mod, callingConvention);
    }

    [SecurityCritical]
    private SignatureHelper(Module mod, System.Reflection.MdSigCallingConvention callingConvention, int cGenericParameters, Type returnType, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers)
    {
      this.Init(mod, callingConvention, cGenericParameters);
      if (callingConvention == System.Reflection.MdSigCallingConvention.Field)
        throw new ArgumentException(Environment.GetResourceString("Argument_BadFieldSig"));
      this.AddOneArgTypeHelper(returnType, requiredCustomModifiers, optionalCustomModifiers);
    }

    [SecurityCritical]
    private SignatureHelper(Module mod, System.Reflection.MdSigCallingConvention callingConvention, Type returnType, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers)
      : this(mod, callingConvention, 0, returnType, requiredCustomModifiers, optionalCustomModifiers)
    {
    }

    [SecurityCritical]
    private SignatureHelper(Module mod, Type type)
    {
      this.Init(mod);
      this.AddOneArgTypeHelper(type);
    }

    /// <summary>在已知方法的模块、返回类型和参数类型的情况下，返回具有标准调用约定的方法的签名帮助器。</summary>
    /// <returns>方法的 SignatureHelper 对象。</returns>
    /// <param name="mod">包含为其请求 SignatureHelper 的方法的 <see cref="T:System.Reflection.Emit.ModuleBuilder" />。</param>
    /// <param name="returnType">方法的返回类型，对于 void 返回类型为 null（在 Visual Basic 中为 Sub 过程）。</param>
    /// <param name="parameterTypes">方法的参数类型，如果方法没有参数，则为 null。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="mod" /> 为 null。- 或 -<paramref name="parameterTypes" /> 的一个元素为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="mod" /> 不是 <see cref="T:System.Reflection.Emit.ModuleBuilder" />。</exception>
    [SecuritySafeCritical]
    public static SignatureHelper GetMethodSigHelper(Module mod, Type returnType, Type[] parameterTypes)
    {
      return SignatureHelper.GetMethodSigHelper(mod, CallingConventions.Standard, returnType, (Type[]) null, (Type[]) null, parameterTypes, (Type[][]) null, (Type[][]) null);
    }

    [SecurityCritical]
    internal static SignatureHelper GetMethodSigHelper(Module mod, CallingConventions callingConvention, Type returnType, int cGenericParam)
    {
      return SignatureHelper.GetMethodSigHelper(mod, callingConvention, cGenericParam, returnType, (Type[]) null, (Type[]) null, (Type[]) null, (Type[][]) null, (Type[][]) null);
    }

    /// <summary>已知方法的模块、调用约定和返回类型，返回方法的签名帮助器。</summary>
    /// <returns>方法的 SignatureHelper 对象。</returns>
    /// <param name="mod">包含为其请求 SignatureHelper 的方法的 <see cref="T:System.Reflection.Emit.ModuleBuilder" />。</param>
    /// <param name="callingConvention">该方法的调用约定。</param>
    /// <param name="returnType">方法的返回类型，对于 void 返回类型为 null（在 Visual Basic 中为 Sub 过程）。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="mod" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="mod" /> 不是 <see cref="T:System.Reflection.Emit.ModuleBuilder" />。</exception>
    [SecuritySafeCritical]
    public static SignatureHelper GetMethodSigHelper(Module mod, CallingConventions callingConvention, Type returnType)
    {
      return SignatureHelper.GetMethodSigHelper(mod, callingConvention, returnType, (Type[]) null, (Type[]) null, (Type[]) null, (Type[][]) null, (Type[][]) null);
    }

    internal static SignatureHelper GetMethodSpecSigHelper(Module scope, Type[] inst)
    {
      SignatureHelper signatureHelper = new SignatureHelper(scope, System.Reflection.MdSigCallingConvention.GenericInst);
      signatureHelper.AddData(inst.Length);
      foreach (Type clsArgument in inst)
        signatureHelper.AddArgument(clsArgument);
      return signatureHelper;
    }

    [SecurityCritical]
    internal static SignatureHelper GetMethodSigHelper(Module scope, CallingConventions callingConvention, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
    {
      return SignatureHelper.GetMethodSigHelper(scope, callingConvention, 0, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers, parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
    }

    [SecurityCritical]
    internal static SignatureHelper GetMethodSigHelper(Module scope, CallingConventions callingConvention, int cGenericParam, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
    {
      if (returnType == (Type) null)
        returnType = typeof (void);
      System.Reflection.MdSigCallingConvention callingConvention1 = System.Reflection.MdSigCallingConvention.Default;
      if ((callingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs)
        callingConvention1 = System.Reflection.MdSigCallingConvention.Vararg;
      if (cGenericParam > 0)
        callingConvention1 |= System.Reflection.MdSigCallingConvention.Generic;
      if ((callingConvention & CallingConventions.HasThis) == CallingConventions.HasThis)
        callingConvention1 |= System.Reflection.MdSigCallingConvention.HasThis;
      SignatureHelper signatureHelper = new SignatureHelper(scope, callingConvention1, cGenericParam, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers);
      Type[] arguments = parameterTypes;
      Type[][] requiredCustomModifiers = requiredParameterTypeCustomModifiers;
      Type[][] optionalCustomModifiers = optionalParameterTypeCustomModifiers;
      signatureHelper.AddArguments(arguments, requiredCustomModifiers, optionalCustomModifiers);
      return signatureHelper;
    }

    /// <summary>已知方法的模块、非托管调用约定和返回类型，返回方法的签名帮助器。</summary>
    /// <returns>方法的 SignatureHelper 对象。</returns>
    /// <param name="mod">包含为其请求 SignatureHelper 的方法的 <see cref="T:System.Reflection.Emit.ModuleBuilder" />。</param>
    /// <param name="unmanagedCallConv">此方法的非托管调用约定。</param>
    /// <param name="returnType">方法的返回类型，对于 void 返回类型为 null（在 Visual Basic 中为 Sub 过程）。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="mod" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="mod" /> 不是 <see cref="T:System.Reflection.Emit.ModuleBuilder" />。- 或 -<paramref name="unmanagedCallConv" /> 是未知的非托管调用约定。</exception>
    [SecuritySafeCritical]
    public static SignatureHelper GetMethodSigHelper(Module mod, CallingConvention unmanagedCallConv, Type returnType)
    {
      if (returnType == (Type) null)
        returnType = typeof (void);
      System.Reflection.MdSigCallingConvention callingConvention;
      if (unmanagedCallConv == CallingConvention.Cdecl)
        callingConvention = System.Reflection.MdSigCallingConvention.C;
      else if (unmanagedCallConv == CallingConvention.StdCall || unmanagedCallConv == CallingConvention.Winapi)
        callingConvention = System.Reflection.MdSigCallingConvention.StdCall;
      else if (unmanagedCallConv == CallingConvention.ThisCall)
      {
        callingConvention = System.Reflection.MdSigCallingConvention.ThisCall;
      }
      else
      {
        if (unmanagedCallConv != CallingConvention.FastCall)
          throw new ArgumentException(Environment.GetResourceString("Argument_UnknownUnmanagedCallConv"), "unmanagedCallConv");
        callingConvention = System.Reflection.MdSigCallingConvention.FastCall;
      }
      return new SignatureHelper(mod, callingConvention, returnType, (Type[]) null, (Type[]) null);
    }

    /// <summary>返回局部变量的签名帮助器。</summary>
    /// <returns>用于局部变量的 <see cref="T:System.Reflection.Emit.SignatureHelper" />。</returns>
    public static SignatureHelper GetLocalVarSigHelper()
    {
      return SignatureHelper.GetLocalVarSigHelper((Module) null);
    }

    /// <summary>已知方法的调用约定和返回类型，返回方法的签名帮助器。</summary>
    /// <returns>方法的 SignatureHelper 对象。</returns>
    /// <param name="callingConvention">该方法的调用约定。</param>
    /// <param name="returnType">方法的返回类型，对于 void 返回类型为 null（在 Visual Basic 中为 Sub 过程）。</param>
    public static SignatureHelper GetMethodSigHelper(CallingConventions callingConvention, Type returnType)
    {
      return SignatureHelper.GetMethodSigHelper((Module) null, callingConvention, returnType);
    }

    /// <summary>已知方法的非托管调用约定和返回类型，返回方法的签名帮助器。</summary>
    /// <returns>方法的 SignatureHelper 对象。</returns>
    /// <param name="unmanagedCallingConvention">此方法的非托管调用约定。</param>
    /// <param name="returnType">方法的返回类型，对于 void 返回类型为 null（在 Visual Basic 中为 Sub 过程）。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="unmanagedCallConv" /> 是未知的非托管调用约定。</exception>
    public static SignatureHelper GetMethodSigHelper(CallingConvention unmanagedCallingConvention, Type returnType)
    {
      return SignatureHelper.GetMethodSigHelper((Module) null, unmanagedCallingConvention, returnType);
    }

    /// <summary>返回局部变量的签名帮助器。</summary>
    /// <returns>局部变量的 SignatureHelper 对象。</returns>
    /// <param name="mod">包含为其请求 SignatureHelper 的局部变量的动态模块。</param>
    public static SignatureHelper GetLocalVarSigHelper(Module mod)
    {
      return new SignatureHelper(mod, System.Reflection.MdSigCallingConvention.LocalSig);
    }

    /// <summary>返回字段的签名帮助器。</summary>
    /// <returns>字段的 SignatureHelper 对象。</returns>
    /// <param name="mod">包含为其请求 SignatureHelper 的字段的动态模块。</param>
    public static SignatureHelper GetFieldSigHelper(Module mod)
    {
      return new SignatureHelper(mod, System.Reflection.MdSigCallingConvention.Field);
    }

    /// <summary>在已知包含属性、属性类型和属性参数的动态模块的情况下，返回属性的签名帮助器。</summary>
    /// <returns>属性的 <see cref="T:System.Reflection.Emit.SignatureHelper" /> 对象。</returns>
    /// <param name="mod">
    /// <see cref="T:System.Reflection.Emit.ModuleBuilder" />，其中包含为其请求 <see cref="T:System.Reflection.Emit.SignatureHelper" /> 的属性。</param>
    /// <param name="returnType">属性类型。</param>
    /// <param name="parameterTypes">参数类型，如果属性没有参数，则为 null。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="mod" /> 为 null。- 或 -<paramref name="parameterTypes" /> 的一个元素为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="mod" /> 不是 <see cref="T:System.Reflection.Emit.ModuleBuilder" />。</exception>
    public static SignatureHelper GetPropertySigHelper(Module mod, Type returnType, Type[] parameterTypes)
    {
      return SignatureHelper.GetPropertySigHelper(mod, returnType, (Type[]) null, (Type[]) null, parameterTypes, (Type[][]) null, (Type[][]) null);
    }

    /// <summary>在已知包含属性、属性类型、属性参数以及返回类型和参数的自定义修饰符的动态模块的情况下，返回属性的签名帮助器。</summary>
    /// <returns>属性的 <see cref="T:System.Reflection.Emit.SignatureHelper" /> 对象。</returns>
    /// <param name="mod">
    /// <see cref="T:System.Reflection.Emit.ModuleBuilder" />，其中包含为其请求 <see cref="T:System.Reflection.Emit.SignatureHelper" /> 的属性。</param>
    /// <param name="returnType">属性类型。</param>
    /// <param name="requiredReturnTypeCustomModifiers">一个表示返回类型必需的自定义修饰符的类型数组，例如 <see cref="T:System.Runtime.CompilerServices.IsConst" /> 或 <see cref="T:System.Runtime.CompilerServices.IsBoxed" />。如果返回类型没有必需的自定义修饰符，请指定 null。</param>
    /// <param name="optionalReturnTypeCustomModifiers">一个表示返回类型的可选自定义修饰符的类型数组，例如 <see cref="T:System.Runtime.CompilerServices.IsConst" /> 或 <see cref="T:System.Runtime.CompilerServices.IsBoxed" />。如果返回类型没有可选的自定义修饰符，请指定 null。</param>
    /// <param name="parameterTypes">属性的参数类型，如果属性没有参数，则为 null。</param>
    /// <param name="requiredParameterTypeCustomModifiers">由类型数组组成的数组。每个类型数组均表示属性的相应参数所必需的自定义修饰符。如果某个特定参数没有必需的自定义修饰符，请指定 null，而不要指定类型数组。如果属性没有参数，或者所有参数都没有必需的自定义修饰符，请指定 null，而不要指定由数组组成的数组。</param>
    /// <param name="optionalParameterTypeCustomModifiers">由类型数组组成的数组。每个类型数组均表示属性的相应参数的可选自定义修饰符。如果某个特定参数没有可选的自定义修饰符，请指定 null，而不要指定类型数组。如果属性没有参数，或者所有参数都没有可选的自定义修饰符，请指定 null，而不要指定由数组组成的数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="mod" /> 为 null。- 或 -<paramref name="parameterTypes" /> 的一个元素为 null。- 或 -其中一个指定的自定义修饰符为 null。（但是，可以为任何参数的自定义修饰符的数组指定 null。）</exception>
    /// <exception cref="T:System.ArgumentException">已完成签名- 或 -<paramref name="mod" /> 不是 <see cref="T:System.Reflection.Emit.ModuleBuilder" />。- 或 -指定的自定义修饰符之一为数组类型。- 或 -指定的自定义修饰符之一为开放泛型类型。也就是说，<see cref="P:System.Type.ContainsGenericParameters" /> 属性对于自定义修饰符为 true。- 或 -<paramref name="requiredParameterTypeCustomModifiers" /> 或 <paramref name="optionalParameterTypeCustomModifiers" /> 的大小与 <paramref name="parameterTypes" /> 的大小不相等。</exception>
    public static SignatureHelper GetPropertySigHelper(Module mod, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
    {
      return SignatureHelper.GetPropertySigHelper(mod, (CallingConventions) 0, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers, parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
    }

    /// <summary>在已知包含属性、调用约定、属性类型、属性参数以及返回类型和参数的自定义修饰符的动态模块的情况下，返回属性的签名帮助器。</summary>
    /// <returns>属性的 <see cref="T:System.Reflection.Emit.SignatureHelper" /> 对象。</returns>
    /// <param name="mod">
    /// <see cref="T:System.Reflection.Emit.ModuleBuilder" />，其中包含为其请求 <see cref="T:System.Reflection.Emit.SignatureHelper" /> 的属性。</param>
    /// <param name="callingConvention">属性访问器的调用约定。</param>
    /// <param name="returnType">属性类型。</param>
    /// <param name="requiredReturnTypeCustomModifiers">一个表示返回类型必需的自定义修饰符的类型数组，例如 <see cref="T:System.Runtime.CompilerServices.IsConst" /> 或 <see cref="T:System.Runtime.CompilerServices.IsBoxed" />。如果返回类型没有必需的自定义修饰符，请指定 null。</param>
    /// <param name="optionalReturnTypeCustomModifiers">一个表示返回类型的可选自定义修饰符的类型数组，例如 <see cref="T:System.Runtime.CompilerServices.IsConst" /> 或 <see cref="T:System.Runtime.CompilerServices.IsBoxed" />。如果返回类型没有可选的自定义修饰符，请指定 null。</param>
    /// <param name="parameterTypes">属性的参数类型，如果属性没有参数，则为 null。</param>
    /// <param name="requiredParameterTypeCustomModifiers">由类型数组组成的数组。每个类型数组均表示属性的相应参数所必需的自定义修饰符。如果某个特定参数没有必需的自定义修饰符，请指定 null，而不要指定类型数组。如果属性没有参数，或者所有参数都没有必需的自定义修饰符，请指定 null，而不要指定由数组组成的数组。</param>
    /// <param name="optionalParameterTypeCustomModifiers">由类型数组组成的数组。每个类型数组均表示属性的相应参数的可选自定义修饰符。如果某个特定参数没有可选的自定义修饰符，请指定 null，而不要指定类型数组。如果属性没有参数，或者所有参数都没有可选的自定义修饰符，请指定 null，而不要指定由数组组成的数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="mod" /> 为 null。- 或 -<paramref name="parameterTypes" /> 的一个元素为 null。- 或 -其中一个指定的自定义修饰符为 null。（但是，可以为任何参数的自定义修饰符的数组指定 null。）</exception>
    /// <exception cref="T:System.ArgumentException">已完成签名- 或 -<paramref name="mod" /> 不是 <see cref="T:System.Reflection.Emit.ModuleBuilder" />。- 或 -指定的自定义修饰符之一为数组类型。- 或 -指定的自定义修饰符之一为开放泛型类型。也就是说，<see cref="P:System.Type.ContainsGenericParameters" /> 属性对于自定义修饰符为 true。- 或 -<paramref name="requiredParameterTypeCustomModifiers" /> 或 <paramref name="optionalParameterTypeCustomModifiers" /> 的大小与 <paramref name="parameterTypes" /> 的大小不相等。</exception>
    [SecuritySafeCritical]
    public static SignatureHelper GetPropertySigHelper(Module mod, CallingConventions callingConvention, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
    {
      if (returnType == (Type) null)
        returnType = typeof (void);
      System.Reflection.MdSigCallingConvention callingConvention1 = System.Reflection.MdSigCallingConvention.Property;
      if ((callingConvention & CallingConventions.HasThis) == CallingConventions.HasThis)
        callingConvention1 |= System.Reflection.MdSigCallingConvention.HasThis;
      SignatureHelper signatureHelper = new SignatureHelper(mod, callingConvention1, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers);
      Type[] arguments = parameterTypes;
      Type[][] requiredCustomModifiers = requiredParameterTypeCustomModifiers;
      Type[][] optionalCustomModifiers = optionalParameterTypeCustomModifiers;
      signatureHelper.AddArguments(arguments, requiredCustomModifiers, optionalCustomModifiers);
      return signatureHelper;
    }

    [SecurityCritical]
    internal static SignatureHelper GetTypeSigToken(Module mod, Type type)
    {
      if (mod == (Module) null)
        throw new ArgumentNullException("module");
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      return new SignatureHelper(mod, type);
    }

    private void Init(Module mod)
    {
      this.m_signature = new byte[32];
      this.m_currSig = 0;
      this.m_module = mod as ModuleBuilder;
      this.m_argCount = 0;
      this.m_sigDone = false;
      this.m_sizeLoc = -1;
      if ((Module) this.m_module == (Module) null && mod != (Module) null)
        throw new ArgumentException(Environment.GetResourceString("NotSupported_MustBeModuleBuilder"));
    }

    private void Init(Module mod, System.Reflection.MdSigCallingConvention callingConvention)
    {
      this.Init(mod, callingConvention, 0);
    }

    private void Init(Module mod, System.Reflection.MdSigCallingConvention callingConvention, int cGenericParam)
    {
      this.Init(mod);
      this.AddData((int) callingConvention);
      if (callingConvention == System.Reflection.MdSigCallingConvention.Field || callingConvention == System.Reflection.MdSigCallingConvention.GenericInst)
      {
        this.m_sizeLoc = -1;
      }
      else
      {
        if (cGenericParam > 0)
          this.AddData(cGenericParam);
        int num = this.m_currSig;
        this.m_currSig = num + 1;
        this.m_sizeLoc = num;
      }
    }

    [SecurityCritical]
    private void AddOneArgTypeHelper(Type argument, bool pinned)
    {
      if (pinned)
        this.AddElementType(CorElementType.Pinned);
      this.AddOneArgTypeHelper(argument);
    }

    [SecurityCritical]
    private void AddOneArgTypeHelper(Type clsArgument, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers)
    {
      TypeToken typeToken;
      if (optionalCustomModifiers != null)
      {
        for (int index = 0; index < optionalCustomModifiers.Length; ++index)
        {
          Type type = optionalCustomModifiers[index];
          if (type == (Type) null)
            throw new ArgumentNullException("optionalCustomModifiers");
          if (type.HasElementType)
            throw new ArgumentException(Environment.GetResourceString("Argument_ArraysInvalid"), "optionalCustomModifiers");
          if (type.ContainsGenericParameters)
            throw new ArgumentException(Environment.GetResourceString("Argument_GenericsInvalid"), "optionalCustomModifiers");
          this.AddElementType(CorElementType.CModOpt);
          typeToken = this.m_module.GetTypeToken(type);
          this.AddToken(typeToken.Token);
        }
      }
      if (requiredCustomModifiers != null)
      {
        for (int index = 0; index < requiredCustomModifiers.Length; ++index)
        {
          Type type = requiredCustomModifiers[index];
          if (type == (Type) null)
            throw new ArgumentNullException("requiredCustomModifiers");
          if (type.HasElementType)
            throw new ArgumentException(Environment.GetResourceString("Argument_ArraysInvalid"), "requiredCustomModifiers");
          if (type.ContainsGenericParameters)
            throw new ArgumentException(Environment.GetResourceString("Argument_GenericsInvalid"), "requiredCustomModifiers");
          this.AddElementType(CorElementType.CModReqd);
          typeToken = this.m_module.GetTypeToken(type);
          this.AddToken(typeToken.Token);
        }
      }
      this.AddOneArgTypeHelper(clsArgument);
    }

    [SecurityCritical]
    private void AddOneArgTypeHelper(Type clsArgument)
    {
      this.AddOneArgTypeHelperWorker(clsArgument, false);
    }

    [SecurityCritical]
    private void AddOneArgTypeHelperWorker(Type clsArgument, bool lastWasGenericInst)
    {
      if (clsArgument.IsGenericParameter)
      {
        if (clsArgument.DeclaringMethod != (MethodBase) null)
          this.AddElementType(CorElementType.MVar);
        else
          this.AddElementType(CorElementType.Var);
        this.AddData(clsArgument.GenericParameterPosition);
      }
      else if (clsArgument.IsGenericType && (!clsArgument.IsGenericTypeDefinition || !lastWasGenericInst))
      {
        this.AddElementType(CorElementType.GenericInst);
        this.AddOneArgTypeHelperWorker(clsArgument.GetGenericTypeDefinition(), true);
        Type[] genericArguments = clsArgument.GetGenericArguments();
        this.AddData(genericArguments.Length);
        foreach (Type clsArgument1 in genericArguments)
          this.AddOneArgTypeHelper(clsArgument1);
      }
      else if (clsArgument is TypeBuilder)
      {
        TypeBuilder typeBuilder = (TypeBuilder) clsArgument;
        TypeToken clsToken = !typeBuilder.Module.Equals((object) this.m_module) ? this.m_module.GetTypeToken(clsArgument) : typeBuilder.TypeToken;
        if (clsArgument.IsValueType)
          this.InternalAddTypeToken(clsToken, CorElementType.ValueType);
        else
          this.InternalAddTypeToken(clsToken, CorElementType.Class);
      }
      else if (clsArgument is EnumBuilder)
      {
        TypeBuilder typeBuilder = ((EnumBuilder) clsArgument).m_typeBuilder;
        TypeToken clsToken = !typeBuilder.Module.Equals((object) this.m_module) ? this.m_module.GetTypeToken(clsArgument) : typeBuilder.TypeToken;
        if (clsArgument.IsValueType)
          this.InternalAddTypeToken(clsToken, CorElementType.ValueType);
        else
          this.InternalAddTypeToken(clsToken, CorElementType.Class);
      }
      else if (clsArgument.IsByRef)
      {
        this.AddElementType(CorElementType.ByRef);
        clsArgument = clsArgument.GetElementType();
        this.AddOneArgTypeHelper(clsArgument);
      }
      else if (clsArgument.IsPointer)
      {
        this.AddElementType(CorElementType.Ptr);
        this.AddOneArgTypeHelper(clsArgument.GetElementType());
      }
      else if (clsArgument.IsArray)
      {
        if (clsArgument.IsSzArray)
        {
          this.AddElementType(CorElementType.SzArray);
          this.AddOneArgTypeHelper(clsArgument.GetElementType());
        }
        else
        {
          this.AddElementType(CorElementType.Array);
          this.AddOneArgTypeHelper(clsArgument.GetElementType());
          int arrayRank = clsArgument.GetArrayRank();
          this.AddData(arrayRank);
          this.AddData(0);
          this.AddData(arrayRank);
          for (int index = 0; index < arrayRank; ++index)
            this.AddData(0);
        }
      }
      else
      {
        CorElementType corElementType = CorElementType.Max;
        if (clsArgument is RuntimeType)
        {
          corElementType = RuntimeTypeHandle.GetCorElementType((RuntimeType) clsArgument);
          if (corElementType == CorElementType.Class)
          {
            if (clsArgument == typeof (object))
              corElementType = CorElementType.Object;
            else if (clsArgument == typeof (string))
              corElementType = CorElementType.String;
          }
        }
        if (SignatureHelper.IsSimpleType(corElementType))
          this.AddElementType(corElementType);
        else if ((Module) this.m_module == (Module) null)
          this.InternalAddRuntimeType(clsArgument);
        else if (clsArgument.IsValueType)
          this.InternalAddTypeToken(this.m_module.GetTypeToken(clsArgument), CorElementType.ValueType);
        else
          this.InternalAddTypeToken(this.m_module.GetTypeToken(clsArgument), CorElementType.Class);
      }
    }

    private void AddData(int data)
    {
      if (this.m_currSig + 4 > this.m_signature.Length)
        this.m_signature = this.ExpandArray(this.m_signature);
      if (data <= (int) sbyte.MaxValue)
      {
        byte[] numArray = this.m_signature;
        int num1 = this.m_currSig;
        this.m_currSig = num1 + 1;
        int index = num1;
        int num2 = (int) (byte) (data & (int) byte.MaxValue);
        numArray[index] = (byte) num2;
      }
      else if (data <= 16383)
      {
        byte[] numArray1 = this.m_signature;
        int num1 = this.m_currSig;
        this.m_currSig = num1 + 1;
        int index1 = num1;
        int num2 = (int) (byte) (data >> 8 | 128);
        numArray1[index1] = (byte) num2;
        byte[] numArray2 = this.m_signature;
        int num3 = this.m_currSig;
        this.m_currSig = num3 + 1;
        int index2 = num3;
        int num4 = (int) (byte) (data & (int) byte.MaxValue);
        numArray2[index2] = (byte) num4;
      }
      else
      {
        if (data > 536870911)
          throw new ArgumentException(Environment.GetResourceString("Argument_LargeInteger"));
        byte[] numArray1 = this.m_signature;
        int num1 = this.m_currSig;
        this.m_currSig = num1 + 1;
        int index1 = num1;
        int num2 = (int) (byte) (data >> 24 | 192);
        numArray1[index1] = (byte) num2;
        byte[] numArray2 = this.m_signature;
        int num3 = this.m_currSig;
        this.m_currSig = num3 + 1;
        int index2 = num3;
        int num4 = (int) (byte) (data >> 16 & (int) byte.MaxValue);
        numArray2[index2] = (byte) num4;
        byte[] numArray3 = this.m_signature;
        int num5 = this.m_currSig;
        this.m_currSig = num5 + 1;
        int index3 = num5;
        int num6 = (int) (byte) (data >> 8 & (int) byte.MaxValue);
        numArray3[index3] = (byte) num6;
        byte[] numArray4 = this.m_signature;
        int num7 = this.m_currSig;
        this.m_currSig = num7 + 1;
        int index4 = num7;
        int num8 = (int) (byte) (data & (int) byte.MaxValue);
        numArray4[index4] = (byte) num8;
      }
    }

    private void AddData(uint data)
    {
      if (this.m_currSig + 4 > this.m_signature.Length)
        this.m_signature = this.ExpandArray(this.m_signature);
      byte[] numArray1 = this.m_signature;
      int num1 = this.m_currSig;
      this.m_currSig = num1 + 1;
      int index1 = num1;
      int num2 = (int) (byte) (data & (uint) byte.MaxValue);
      numArray1[index1] = (byte) num2;
      byte[] numArray2 = this.m_signature;
      int num3 = this.m_currSig;
      this.m_currSig = num3 + 1;
      int index2 = num3;
      int num4 = (int) (byte) (data >> 8 & (uint) byte.MaxValue);
      numArray2[index2] = (byte) num4;
      byte[] numArray3 = this.m_signature;
      int num5 = this.m_currSig;
      this.m_currSig = num5 + 1;
      int index3 = num5;
      int num6 = (int) (byte) (data >> 16 & (uint) byte.MaxValue);
      numArray3[index3] = (byte) num6;
      byte[] numArray4 = this.m_signature;
      int num7 = this.m_currSig;
      this.m_currSig = num7 + 1;
      int index4 = num7;
      int num8 = (int) (byte) (data >> 24 & (uint) byte.MaxValue);
      numArray4[index4] = (byte) num8;
    }

    private void AddData(ulong data)
    {
      if (this.m_currSig + 8 > this.m_signature.Length)
        this.m_signature = this.ExpandArray(this.m_signature);
      byte[] numArray1 = this.m_signature;
      int num1 = this.m_currSig;
      this.m_currSig = num1 + 1;
      int index1 = num1;
      int num2 = (int) (byte) (data & (ulong) byte.MaxValue);
      numArray1[index1] = (byte) num2;
      byte[] numArray2 = this.m_signature;
      int num3 = this.m_currSig;
      this.m_currSig = num3 + 1;
      int index2 = num3;
      int num4 = (int) (byte) (data >> 8 & (ulong) byte.MaxValue);
      numArray2[index2] = (byte) num4;
      byte[] numArray3 = this.m_signature;
      int num5 = this.m_currSig;
      this.m_currSig = num5 + 1;
      int index3 = num5;
      int num6 = (int) (byte) (data >> 16 & (ulong) byte.MaxValue);
      numArray3[index3] = (byte) num6;
      byte[] numArray4 = this.m_signature;
      int num7 = this.m_currSig;
      this.m_currSig = num7 + 1;
      int index4 = num7;
      int num8 = (int) (byte) (data >> 24 & (ulong) byte.MaxValue);
      numArray4[index4] = (byte) num8;
      byte[] numArray5 = this.m_signature;
      int num9 = this.m_currSig;
      this.m_currSig = num9 + 1;
      int index5 = num9;
      int num10 = (int) (byte) (data >> 32 & (ulong) byte.MaxValue);
      numArray5[index5] = (byte) num10;
      byte[] numArray6 = this.m_signature;
      int num11 = this.m_currSig;
      this.m_currSig = num11 + 1;
      int index6 = num11;
      int num12 = (int) (byte) (data >> 40 & (ulong) byte.MaxValue);
      numArray6[index6] = (byte) num12;
      byte[] numArray7 = this.m_signature;
      int num13 = this.m_currSig;
      this.m_currSig = num13 + 1;
      int index7 = num13;
      int num14 = (int) (byte) (data >> 48 & (ulong) byte.MaxValue);
      numArray7[index7] = (byte) num14;
      byte[] numArray8 = this.m_signature;
      int num15 = this.m_currSig;
      this.m_currSig = num15 + 1;
      int index8 = num15;
      int num16 = (int) (byte) (data >> 56 & (ulong) byte.MaxValue);
      numArray8[index8] = (byte) num16;
    }

    private void AddElementType(CorElementType cvt)
    {
      if (this.m_currSig + 1 > this.m_signature.Length)
        this.m_signature = this.ExpandArray(this.m_signature);
      byte[] numArray = this.m_signature;
      int num1 = this.m_currSig;
      this.m_currSig = num1 + 1;
      int index = num1;
      int num2 = (int) cvt;
      numArray[index] = (byte) num2;
    }

    private void AddToken(int token)
    {
      int num = token & 16777215;
      MetadataTokenType metadataTokenType = (MetadataTokenType) (token & -16777216);
      if (num > 67108863)
        throw new ArgumentException(Environment.GetResourceString("Argument_LargeInteger"));
      int data = num << 2;
      if (metadataTokenType == MetadataTokenType.TypeRef)
        data |= 1;
      else if (metadataTokenType == MetadataTokenType.TypeSpec)
        data |= 2;
      this.AddData(data);
    }

    private void InternalAddTypeToken(TypeToken clsToken, CorElementType CorType)
    {
      this.AddElementType(CorType);
      this.AddToken(clsToken.Token);
    }

    [SecurityCritical]
    private unsafe void InternalAddRuntimeType(Type type)
    {
      this.AddElementType(CorElementType.Internal);
      IntPtr num1 = type.GetTypeHandleInternal().Value;
      if (this.m_currSig + sizeof (void*) > this.m_signature.Length)
        this.m_signature = this.ExpandArray(this.m_signature);
      byte* numPtr = (byte*) &num1;
      for (int index1 = 0; index1 < sizeof (void*); ++index1)
      {
        byte[] numArray = this.m_signature;
        int num2 = this.m_currSig;
        this.m_currSig = num2 + 1;
        int index2 = num2;
        int num3 = (int) numPtr[index1];
        numArray[index2] = (byte) num3;
      }
    }

    private byte[] ExpandArray(byte[] inArray)
    {
      byte[] inArray1 = inArray;
      int requiredLength = inArray1.Length * 2;
      return this.ExpandArray(inArray1, requiredLength);
    }

    private byte[] ExpandArray(byte[] inArray, int requiredLength)
    {
      if (requiredLength < inArray.Length)
        requiredLength = inArray.Length * 2;
      byte[] numArray = new byte[requiredLength];
      Array.Copy((Array) inArray, (Array) numArray, inArray.Length);
      return numArray;
    }

    private void IncrementArgCounts()
    {
      if (this.m_sizeLoc == -1)
        return;
      this.m_argCount = this.m_argCount + 1;
    }

    private void SetNumberOfSignatureElements(bool forceCopy)
    {
      int num1 = this.m_currSig;
      if (this.m_sizeLoc == -1)
        return;
      if (this.m_argCount < 128 && !forceCopy)
      {
        this.m_signature[this.m_sizeLoc] = (byte) this.m_argCount;
      }
      else
      {
        int num2 = this.m_argCount >= 128 ? (this.m_argCount >= 16384 ? 4 : 2) : 1;
        byte[] numArray = new byte[this.m_currSig + num2 - 1];
        numArray[0] = this.m_signature[0];
        Array.Copy((Array) this.m_signature, this.m_sizeLoc + 1, (Array) numArray, this.m_sizeLoc + num2, num1 - (this.m_sizeLoc + 1));
        this.m_signature = numArray;
        this.m_currSig = this.m_sizeLoc;
        this.AddData(this.m_argCount);
        this.m_currSig = num1 + (num2 - 1);
      }
    }

    internal static bool IsSimpleType(CorElementType type)
    {
      return type <= CorElementType.String || type == CorElementType.TypedByRef || (type == CorElementType.I || type == CorElementType.U) || type == CorElementType.Object;
    }

    internal byte[] InternalGetSignature(out int length)
    {
      if (!this.m_sigDone)
      {
        this.m_sigDone = true;
        this.SetNumberOfSignatureElements(false);
      }
      length = this.m_currSig;
      return this.m_signature;
    }

    internal byte[] InternalGetSignatureArray()
    {
      int num1 = this.m_argCount;
      int num2 = this.m_currSig;
      int num3 = num2;
      int length = num1 >= (int) sbyte.MaxValue ? (num1 >= 16383 ? num3 + 4 : num3 + 2) : num3 + 1;
      byte[] numArray1 = new byte[length];
      int num4 = 0;
      byte[] numArray2 = numArray1;
      int index1 = num4;
      int num5 = 1;
      int num6 = index1 + num5;
      int num7 = (int) this.m_signature[0];
      numArray2[index1] = (byte) num7;
      int destinationIndex;
      if (num1 <= (int) sbyte.MaxValue)
      {
        byte[] numArray3 = numArray1;
        int index2 = num6;
        int num8 = 1;
        destinationIndex = index2 + num8;
        int num9 = (int) (byte) (num1 & (int) byte.MaxValue);
        numArray3[index2] = (byte) num9;
      }
      else if (num1 <= 16383)
      {
        byte[] numArray3 = numArray1;
        int index2 = num6;
        int num8 = 1;
        int num9 = index2 + num8;
        int num10 = (int) (byte) (num1 >> 8 | 128);
        numArray3[index2] = (byte) num10;
        byte[] numArray4 = numArray1;
        int index3 = num9;
        int num11 = 1;
        destinationIndex = index3 + num11;
        int num12 = (int) (byte) (num1 & (int) byte.MaxValue);
        numArray4[index3] = (byte) num12;
      }
      else
      {
        if (num1 > 536870911)
          throw new ArgumentException(Environment.GetResourceString("Argument_LargeInteger"));
        byte[] numArray3 = numArray1;
        int index2 = num6;
        int num8 = 1;
        int num9 = index2 + num8;
        int num10 = (int) (byte) (num1 >> 24 | 192);
        numArray3[index2] = (byte) num10;
        byte[] numArray4 = numArray1;
        int index3 = num9;
        int num11 = 1;
        int num12 = index3 + num11;
        int num13 = (int) (byte) (num1 >> 16 & (int) byte.MaxValue);
        numArray4[index3] = (byte) num13;
        byte[] numArray5 = numArray1;
        int index4 = num12;
        int num14 = 1;
        int num15 = index4 + num14;
        int num16 = (int) (byte) (num1 >> 8 & (int) byte.MaxValue);
        numArray5[index4] = (byte) num16;
        byte[] numArray6 = numArray1;
        int index5 = num15;
        int num17 = 1;
        destinationIndex = index5 + num17;
        int num18 = (int) (byte) (num1 & (int) byte.MaxValue);
        numArray6[index5] = (byte) num18;
      }
      Array.Copy((Array) this.m_signature, 2, (Array) numArray1, destinationIndex, num2 - 2);
      numArray1[length - 1] = (byte) 0;
      return numArray1;
    }

    /// <summary>为签名添加参数。</summary>
    /// <param name="clsArgument">参数类型。</param>
    /// <exception cref="T:System.ArgumentException">已完成签名</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="clsArgument" /> 为 null。</exception>
    public void AddArgument(Type clsArgument)
    {
      this.AddArgument(clsArgument, (Type[]) null, (Type[]) null);
    }

    /// <summary>向签名添加指定类型的参数，指定该参数是否固定。</summary>
    /// <param name="argument">参数类型。</param>
    /// <param name="pinned">如果参数固定，则为 true；否则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="argument" /> 为 null。</exception>
    [SecuritySafeCritical]
    public void AddArgument(Type argument, bool pinned)
    {
      if (argument == (Type) null)
        throw new ArgumentNullException("argument");
      this.IncrementArgCounts();
      this.AddOneArgTypeHelper(argument, pinned);
    }

    /// <summary>向签名添加具有指定自定义修饰符的一组参数。</summary>
    /// <param name="arguments">要添加的参数的类型。</param>
    /// <param name="requiredCustomModifiers">由类型数组组成的数组。每个类型数组均表示相应参数所必需的自定义修饰符，例如 <see cref="T:System.Runtime.CompilerServices.IsConst" /> 或 <see cref="T:System.Runtime.CompilerServices.IsBoxed" />。如果某个特定参数没有必需的自定义修饰符，请指定 null，而不要指定类型数组。如果所有参数都没有必需的自定义修饰符，请指定 null，而不要指定由数组组成的数组。</param>
    /// <param name="optionalCustomModifiers">由类型数组组成的数组。每个类型数组均表示相应参数的可选自定义修饰符，如 <see cref="T:System.Runtime.CompilerServices.IsConst" /> 或 <see cref="T:System.Runtime.CompilerServices.IsBoxed" />。如果某个特定参数没有可选的自定义修饰符，请指定 null，而不要指定类型数组。如果所有参数都没有可选的自定义修饰符，请指定 null，而不要指定由数组组成的数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="arguments" /> 的一个元素为 null。- 或 -其中一个指定的自定义修饰符为 null。（但是，可以为任何参数的自定义修饰符的数组指定 null。）</exception>
    /// <exception cref="T:System.ArgumentException">已完成签名- 或 -指定的自定义修饰符之一为数组类型。- 或 -指定的自定义修饰符之一为开放泛型类型。也就是说，<see cref="P:System.Type.ContainsGenericParameters" /> 属性对于自定义修饰符为 true。- 或 -<paramref name="requiredCustomModifiers" /> 或 <paramref name="optionalCustomModifiers" /> 的大小与 <paramref name="arguments" /> 的大小不相等。</exception>
    public void AddArguments(Type[] arguments, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers)
    {
      if (requiredCustomModifiers != null && (arguments == null || requiredCustomModifiers.Length != arguments.Length))
        throw new ArgumentException(Environment.GetResourceString("Argument_MismatchedArrays", (object) "requiredCustomModifiers", (object) "arguments"));
      if (optionalCustomModifiers != null && (arguments == null || optionalCustomModifiers.Length != arguments.Length))
        throw new ArgumentException(Environment.GetResourceString("Argument_MismatchedArrays", (object) "optionalCustomModifiers", (object) "arguments"));
      if (arguments == null)
        return;
      for (int index = 0; index < arguments.Length; ++index)
        this.AddArgument(arguments[index], requiredCustomModifiers == null ? (Type[]) null : requiredCustomModifiers[index], optionalCustomModifiers == null ? (Type[]) null : optionalCustomModifiers[index]);
    }

    /// <summary>向签名添加具有指定自定义修饰符的参数。</summary>
    /// <param name="argument">参数类型。</param>
    /// <param name="requiredCustomModifiers">一个表示参数必需的自定义修饰符的类型数组，例如 <see cref="T:System.Runtime.CompilerServices.IsConst" /> 或 <see cref="T:System.Runtime.CompilerServices.IsBoxed" />。如果参数没有必需的自定义修饰符，请指定 null。</param>
    /// <param name="optionalCustomModifiers">一个表示参数的可选自定义修饰符的类型数组，例如 <see cref="T:System.Runtime.CompilerServices.IsConst" /> 或 <see cref="T:System.Runtime.CompilerServices.IsBoxed" />。如果参数没有可选的自定义修饰符，请指定 null。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="argument" /> 为 null。- 或 -<paramref name="requiredCustomModifiers" /> 或 <paramref name="optionalCustomModifiers" /> 的一个元素为 null。</exception>
    /// <exception cref="T:System.ArgumentException">已完成签名- 或 -指定的自定义修饰符之一为数组类型。- 或 -指定的自定义修饰符之一为开放泛型类型。也就是说，<see cref="P:System.Type.ContainsGenericParameters" /> 属性对于自定义修饰符为 true。</exception>
    [SecuritySafeCritical]
    public void AddArgument(Type argument, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers)
    {
      if (this.m_sigDone)
        throw new ArgumentException(Environment.GetResourceString("Argument_SigIsFinalized"));
      if (argument == (Type) null)
        throw new ArgumentNullException("argument");
      this.IncrementArgCounts();
      this.AddOneArgTypeHelper(argument, requiredCustomModifiers, optionalCustomModifiers);
    }

    /// <summary>标记 vararg 固定部分的结尾。这仅在调用方创建 vararg 签名调用站点时使用。</summary>
    public void AddSentinel()
    {
      this.AddElementType(CorElementType.Sentinel);
    }

    /// <summary>检查该实例是否等于给定对象。</summary>
    /// <returns>如果给定对象是 SignatureHelper 并且表示同一签名，则为 true；否则为 false。</returns>
    /// <param name="obj">应与此实例进行比较的对象。</param>
    public override bool Equals(object obj)
    {
      if (!(obj is SignatureHelper))
        return false;
      SignatureHelper signatureHelper = (SignatureHelper) obj;
      if (!signatureHelper.m_module.Equals((object) this.m_module) || signatureHelper.m_currSig != this.m_currSig || (signatureHelper.m_sizeLoc != this.m_sizeLoc || signatureHelper.m_sigDone != this.m_sigDone))
        return false;
      for (int index = 0; index < this.m_currSig; ++index)
      {
        if ((int) this.m_signature[index] != (int) signatureHelper.m_signature[index])
          return false;
      }
      return true;
    }

    /// <summary>创建并返回此实例的哈希代码。</summary>
    /// <returns>返回基于名称的哈希代码。</returns>
    public override int GetHashCode()
    {
      int num = this.m_module.GetHashCode() + this.m_currSig + this.m_sizeLoc;
      if (this.m_sigDone)
        ++num;
      for (int index = 0; index < this.m_currSig; ++index)
        num += this.m_signature[index].GetHashCode();
      return num;
    }

    /// <summary>在签名中添加结束标记并将签名标记为已完成，以便不能再添加更多的标记。</summary>
    /// <returns>返回由完整签名组成的字节数组。</returns>
    public byte[] GetSignature()
    {
      return this.GetSignature(false);
    }

    internal byte[] GetSignature(bool appendEndOfSig)
    {
      if (!this.m_sigDone)
      {
        if (appendEndOfSig)
          this.AddElementType(CorElementType.End);
        this.SetNumberOfSignatureElements(true);
        this.m_sigDone = true;
      }
      if (this.m_signature.Length > this.m_currSig)
      {
        byte[] numArray = new byte[this.m_currSig];
        Array.Copy((Array) this.m_signature, (Array) numArray, this.m_currSig);
        this.m_signature = numArray;
      }
      return this.m_signature;
    }

    /// <summary>返回表示签名参数的字符串。</summary>
    /// <returns>返回表示该签名的参数的字符串。</returns>
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("Length: " + (object) this.m_currSig + Environment.NewLine);
      if (this.m_sizeLoc != -1)
        stringBuilder.Append("Arguments: " + (object) this.m_signature[this.m_sizeLoc] + Environment.NewLine);
      else
        stringBuilder.Append("Field Signature" + Environment.NewLine);
      stringBuilder.Append("Signature: " + Environment.NewLine);
      for (int index = 0; index <= this.m_currSig; ++index)
        stringBuilder.Append(((int) this.m_signature[index]).ToString() + "  ");
      stringBuilder.Append(Environment.NewLine);
      return stringBuilder.ToString();
    }

    void _SignatureHelper.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _SignatureHelper.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _SignatureHelper.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _SignatureHelper.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
