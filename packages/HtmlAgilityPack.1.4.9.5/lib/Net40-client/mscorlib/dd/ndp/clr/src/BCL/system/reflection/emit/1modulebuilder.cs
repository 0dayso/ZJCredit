// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.ModuleBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
  /// <summary>定义和表示动态程序集中的模块。</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_ModuleBuilder))]
  [ComVisible(true)]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public class ModuleBuilder : Module, _ModuleBuilder
  {
    private Dictionary<string, Type> m_TypeBuilderDict;
    private ISymbolWriter m_iSymWriter;
    internal ModuleBuilderData m_moduleData;
    private MethodToken m_EntryPoint;
    internal InternalModuleBuilder m_internalModuleBuilder;
    private AssemblyBuilder m_assemblyBuilder;

    internal AssemblyBuilder ContainingAssemblyBuilder
    {
      get
      {
        return this.m_assemblyBuilder;
      }
    }

    internal object SyncRoot
    {
      get
      {
        return this.ContainingAssemblyBuilder.SyncRoot;
      }
    }

    internal InternalModuleBuilder InternalModule
    {
      get
      {
        return this.m_internalModuleBuilder;
      }
    }

    /// <summary>获取表示此模块的完全限定名和路径的 String。</summary>
    /// <returns>完全限定的模块名。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public override string FullyQualifiedName
    {
      [SecuritySafeCritical] get
      {
        string str = this.m_moduleData.m_strFileName;
        if (str == null)
          return (string) null;
        if (this.ContainingAssemblyBuilder.m_assemblyData.m_strDir != null)
          str = Path.UnsafeGetFullPath(Path.Combine(this.ContainingAssemblyBuilder.m_assemblyData.m_strDir, str));
        if (this.ContainingAssemblyBuilder.m_assemblyData.m_strDir != null && str != null)
          new FileIOPermission(FileIOPermissionAccess.PathDiscovery, str).Demand();
        return str;
      }
    }

    /// <summary>获取元数据流版本。</summary>
    /// <returns>表示元数据流版本的 32 位整数。高序位的两个字节表示主版本号，低序位的两个字节表示次版本号。</returns>
    public override int MDStreamVersion
    {
      get
      {
        return this.InternalModule.MDStreamVersion;
      }
    }

    /// <summary>获取一个通用唯一标识符 (UUID)，该标识符可用于区分一个模块的两个版本。</summary>
    /// <returns>一个 <see cref="T:System.Guid" />，可用于区分一个模块的两个版本。</returns>
    public override Guid ModuleVersionId
    {
      get
      {
        return this.InternalModule.ModuleVersionId;
      }
    }

    /// <summary>获取一个标记，该标记用于标识元数据中的当前动态模块。</summary>
    /// <returns>一个整数标记，用于标识元数据中的当前模块。</returns>
    public override int MetadataToken
    {
      get
      {
        return this.InternalModule.MetadataToken;
      }
    }

    /// <summary>获取表示动态模块的名称的字符串。</summary>
    /// <returns>该动态模块的名称。</returns>
    public override string ScopeName
    {
      get
      {
        return this.InternalModule.ScopeName;
      }
    }

    /// <summary>一个字符串，指示这是内存中的模块。</summary>
    /// <returns>指示这是内存中的模块的文本。</returns>
    public override string Name
    {
      get
      {
        return this.InternalModule.Name;
      }
    }

    /// <summary>获取定义此 <see cref="T:System.Reflection.Emit.ModuleBuilder" /> 实例的动态程序集。</summary>
    /// <returns>定义了当前动态模块的动态程序集。</returns>
    public override Assembly Assembly
    {
      get
      {
        return (Assembly) this.m_assemblyBuilder;
      }
    }

    internal ModuleBuilder(AssemblyBuilder assemblyBuilder, InternalModuleBuilder internalModuleBuilder)
    {
      this.m_internalModuleBuilder = internalModuleBuilder;
      this.m_assemblyBuilder = assemblyBuilder;
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr nCreateISymWriterForDynamicModule(Module module, string filename);

    internal static string UnmangleTypeName(string typeName)
    {
      int startIndex = typeName.Length - 1;
      int num1;
      while (true)
      {
        num1 = typeName.LastIndexOf('+', startIndex);
        if (num1 != -1)
        {
          bool flag = true;
          int num2 = num1;
          while ((int) typeName[--num2] == 92)
            flag = !flag;
          if (!flag)
            startIndex = num2;
          else
            break;
        }
        else
          break;
      }
      return typeName.Substring(num1 + 1);
    }

    internal void AddType(string name, Type type)
    {
      this.m_TypeBuilderDict.Add(name, type);
    }

    internal void CheckTypeNameConflict(string strTypeName, Type enclosingType)
    {
      Type type = (Type) null;
      if (this.m_TypeBuilderDict.TryGetValue(strTypeName, out type) && type.DeclaringType == enclosingType)
        throw new ArgumentException(Environment.GetResourceString("Argument_DuplicateTypeName"));
    }

    private Type GetType(string strFormat, Type baseType)
    {
      if (strFormat == null || strFormat.Equals(string.Empty))
        return baseType;
      return SymbolType.FormCompoundType(strFormat.ToCharArray(), baseType, 0);
    }

    internal void CheckContext(params Type[][] typess)
    {
      this.ContainingAssemblyBuilder.CheckContext(typess);
    }

    internal void CheckContext(params Type[] types)
    {
      this.ContainingAssemblyBuilder.CheckContext(types);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int GetTypeRef(RuntimeModule module, string strFullName, RuntimeModule refedModule, string strRefedModuleFileName, int tkResolution);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int GetMemberRef(RuntimeModule module, RuntimeModule refedModule, int tr, int defToken);

    [SecurityCritical]
    private int GetMemberRef(Module refedModule, int tr, int defToken)
    {
      return ModuleBuilder.GetMemberRef(this.GetNativeHandle(), ModuleBuilder.GetRuntimeModuleFromModule(refedModule).GetNativeHandle(), tr, defToken);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int GetMemberRefFromSignature(RuntimeModule module, int tr, string methodName, byte[] signature, int length);

    [SecurityCritical]
    private int GetMemberRefFromSignature(int tr, string methodName, byte[] signature, int length)
    {
      return ModuleBuilder.GetMemberRefFromSignature(this.GetNativeHandle(), tr, methodName, signature, length);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int GetMemberRefOfMethodInfo(RuntimeModule module, int tr, IRuntimeMethodInfo method);

    [SecurityCritical]
    private int GetMemberRefOfMethodInfo(int tr, RuntimeMethodInfo method)
    {
      if (this.ContainingAssemblyBuilder.ProfileAPICheck && (method.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) method.FullName));
      return ModuleBuilder.GetMemberRefOfMethodInfo(this.GetNativeHandle(), tr, (IRuntimeMethodInfo) method);
    }

    [SecurityCritical]
    private int GetMemberRefOfMethodInfo(int tr, RuntimeConstructorInfo method)
    {
      if (this.ContainingAssemblyBuilder.ProfileAPICheck && (method.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) method.FullName));
      return ModuleBuilder.GetMemberRefOfMethodInfo(this.GetNativeHandle(), tr, (IRuntimeMethodInfo) method);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int GetMemberRefOfFieldInfo(RuntimeModule module, int tkType, RuntimeTypeHandle declaringType, int tkField);

    [SecurityCritical]
    private int GetMemberRefOfFieldInfo(int tkType, RuntimeTypeHandle declaringType, RuntimeFieldInfo runtimeField)
    {
      if (this.ContainingAssemblyBuilder.ProfileAPICheck)
      {
        RtFieldInfo rtFieldInfo = runtimeField as RtFieldInfo;
        if ((FieldInfo) rtFieldInfo != (FieldInfo) null && (rtFieldInfo.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) rtFieldInfo.FullName));
      }
      return ModuleBuilder.GetMemberRefOfFieldInfo(this.GetNativeHandle(), tkType, declaringType, runtimeField.MetadataToken);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int GetTokenFromTypeSpec(RuntimeModule pModule, byte[] signature, int length);

    [SecurityCritical]
    private int GetTokenFromTypeSpec(byte[] signature, int length)
    {
      return ModuleBuilder.GetTokenFromTypeSpec(this.GetNativeHandle(), signature, length);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int GetArrayMethodToken(RuntimeModule module, int tkTypeSpec, string methodName, byte[] signature, int sigLength);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int GetStringConstant(RuntimeModule module, string str, int length);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void PreSavePEFile(RuntimeModule module, int portableExecutableKind, int imageFileMachine);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void SavePEFile(RuntimeModule module, string fileName, int entryPoint, int isExe, bool isManifestFile);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void AddResource(RuntimeModule module, string strName, byte[] resBytes, int resByteCount, int tkFile, int attribute, int portableExecutableKind, int imageFileMachine);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void SetModuleName(RuntimeModule module, string strModuleName);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void SetFieldRVAContent(RuntimeModule module, int fdToken, byte[] data, int length);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void DefineNativeResourceFile(RuntimeModule module, string strFilename, int portableExecutableKind, int ImageFileMachine);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void DefineNativeResourceBytes(RuntimeModule module, byte[] pbResource, int cbResource, int portableExecutableKind, int imageFileMachine);

    [SecurityCritical]
    internal void DefineNativeResource(PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
    {
      string strFilename = this.m_moduleData.m_strResourceFileName;
      byte[] numArray = this.m_moduleData.m_resourceBytes;
      if (strFilename != null)
      {
        ModuleBuilder.DefineNativeResourceFile(this.GetNativeHandle(), strFilename, (int) portableExecutableKind, (int) imageFileMachine);
      }
      else
      {
        if (numArray == null)
          return;
        RuntimeModule nativeHandle = this.GetNativeHandle();
        byte[] pbResource = numArray;
        int length = pbResource.Length;
        int portableExecutableKind1 = (int) portableExecutableKind;
        int imageFileMachine1 = (int) imageFileMachine;
        ModuleBuilder.DefineNativeResourceBytes(nativeHandle, pbResource, length, portableExecutableKind1, imageFileMachine1);
      }
    }

    internal virtual Type FindTypeBuilderWithName(string strTypeName, bool ignoreCase)
    {
      if (ignoreCase)
      {
        foreach (string key in this.m_TypeBuilderDict.Keys)
        {
          if (string.Compare(key, strTypeName, StringComparison.OrdinalIgnoreCase) == 0)
            return this.m_TypeBuilderDict[key];
        }
      }
      else
      {
        Type type;
        if (this.m_TypeBuilderDict.TryGetValue(strTypeName, out type))
          return type;
      }
      return (Type) null;
    }

    internal void SetEntryPoint(MethodToken entryPoint)
    {
      this.m_EntryPoint = entryPoint;
    }

    [SecurityCritical]
    internal void PreSave(string fileName, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
    {
      if (this.m_moduleData.m_isSaved)
        throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("InvalidOperation_ModuleHasBeenSaved"), (object) this.m_moduleData.m_strModuleName));
      if (!this.m_moduleData.m_fGlobalBeenCreated && this.m_moduleData.m_fHasGlobal)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_GlobalFunctionNotBaked"));
      foreach (Type type in this.m_TypeBuilderDict.Values)
      {
        TypeBuilder typeBuilder = !(type is TypeBuilder) ? ((EnumBuilder) type).m_typeBuilder : (TypeBuilder) type;
        if (!typeBuilder.IsCreated())
          throw new NotSupportedException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("NotSupported_NotAllTypesAreBaked"), (object) typeBuilder.FullName));
      }
      ModuleBuilder.PreSavePEFile(this.GetNativeHandle(), (int) portableExecutableKind, (int) imageFileMachine);
    }

    [SecurityCritical]
    internal void Save(string fileName, bool isAssemblyFile, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
    {
      if (this.m_moduleData.m_embeddedRes != null)
      {
        for (ResWriterData resWriterData = this.m_moduleData.m_embeddedRes; resWriterData != null; resWriterData = resWriterData.m_nextResWriter)
        {
          if (resWriterData.m_resWriter != null)
            resWriterData.m_resWriter.Generate();
          byte[] buffer = new byte[resWriterData.m_memoryStream.Length];
          resWriterData.m_memoryStream.Flush();
          resWriterData.m_memoryStream.Position = 0L;
          resWriterData.m_memoryStream.Read(buffer, 0, buffer.Length);
          RuntimeModule nativeHandle = this.GetNativeHandle();
          string strName = resWriterData.m_strName;
          byte[] resBytes = buffer;
          int length = resBytes.Length;
          int fileToken = this.m_moduleData.FileToken;
          int attribute = (int) resWriterData.m_attribute;
          int portableExecutableKind1 = (int) portableExecutableKind;
          int imageFileMachine1 = (int) imageFileMachine;
          ModuleBuilder.AddResource(nativeHandle, strName, resBytes, length, fileToken, attribute, portableExecutableKind1, imageFileMachine1);
        }
      }
      this.DefineNativeResource(portableExecutableKind, imageFileMachine);
      PEFileKinds peFileKinds = isAssemblyFile ? this.ContainingAssemblyBuilder.m_assemblyData.m_peFileKind : PEFileKinds.Dll;
      ModuleBuilder.SavePEFile(this.GetNativeHandle(), fileName, this.m_EntryPoint.Token, (int) peFileKinds, isAssemblyFile);
      this.m_moduleData.m_isSaved = true;
    }

    [SecurityCritical]
    private int GetTypeRefNested(Type type, Module refedModule, string strRefedModuleFileName)
    {
      Type declaringType = type.DeclaringType;
      int tkResolution = 0;
      string str = type.FullName;
      if (declaringType != (Type) null)
      {
        tkResolution = this.GetTypeRefNested(declaringType, refedModule, strRefedModuleFileName);
        str = ModuleBuilder.UnmangleTypeName(str);
      }
      if (this.ContainingAssemblyBuilder.ProfileAPICheck)
      {
        RuntimeType runtimeType = type as RuntimeType;
        if (runtimeType != (RuntimeType) null && (runtimeType.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) runtimeType.FullName));
      }
      return ModuleBuilder.GetTypeRef(this.GetNativeHandle(), str, ModuleBuilder.GetRuntimeModuleFromModule(refedModule).GetNativeHandle(), strRefedModuleFileName, tkResolution);
    }

    [SecurityCritical]
    internal MethodToken InternalGetConstructorToken(ConstructorInfo con, bool usingRef)
    {
      if (con == (ConstructorInfo) null)
        throw new ArgumentNullException("con");
      ConstructorBuilder constructorBuilder;
      int str;
      if ((ConstructorInfo) (constructorBuilder = con as ConstructorBuilder) != (ConstructorInfo) null)
      {
        if (!usingRef && constructorBuilder.Module.Equals((object) this))
          return constructorBuilder.GetToken();
        int token = this.GetTypeTokenInternal(con.ReflectedType).Token;
        str = this.GetMemberRef(con.ReflectedType.Module, token, constructorBuilder.GetToken().Token);
      }
      else
      {
        ConstructorOnTypeBuilderInstantiation builderInstantiation;
        if ((ConstructorInfo) (builderInstantiation = con as ConstructorOnTypeBuilderInstantiation) != (ConstructorInfo) null)
        {
          if (usingRef)
            throw new InvalidOperationException();
          int token = this.GetTypeTokenInternal(con.DeclaringType).Token;
          str = this.GetMemberRef(con.DeclaringType.Module, token, builderInstantiation.MetadataTokenInternal);
        }
        else
        {
          RuntimeConstructorInfo method;
          if ((ConstructorInfo) (method = con as RuntimeConstructorInfo) != (ConstructorInfo) null && !con.ReflectedType.IsArray)
          {
            str = this.GetMemberRefOfMethodInfo(this.GetTypeTokenInternal(con.ReflectedType).Token, method);
          }
          else
          {
            ParameterInfo[] parameters = con.GetParameters();
            if (parameters == null)
              throw new ArgumentException(Environment.GetResourceString("Argument_InvalidConstructorInfo"));
            int length1 = parameters.Length;
            Type[] parameterTypes = new Type[length1];
            Type[][] requiredParameterTypeCustomModifiers = new Type[length1][];
            Type[][] optionalParameterTypeCustomModifiers = new Type[length1][];
            for (int index = 0; index < length1; ++index)
            {
              if (parameters[index] == null)
                throw new ArgumentException(Environment.GetResourceString("Argument_InvalidConstructorInfo"));
              parameterTypes[index] = parameters[index].ParameterType;
              requiredParameterTypeCustomModifiers[index] = parameters[index].GetRequiredCustomModifiers();
              optionalParameterTypeCustomModifiers[index] = parameters[index].GetOptionalCustomModifiers();
            }
            int token = this.GetTypeTokenInternal(con.ReflectedType).Token;
            int length2;
            byte[] signature = SignatureHelper.GetMethodSigHelper((Module) this, con.CallingConvention, (Type) null, (Type[]) null, (Type[]) null, parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers).InternalGetSignature(out length2);
            str = this.GetMemberRefFromSignature(token, con.Name, signature, length2);
          }
        }
      }
      return new MethodToken(str);
    }

    [SecurityCritical]
    internal void Init(string strModuleName, string strFileName, int tkFile)
    {
      this.m_moduleData = new ModuleBuilderData(this, strModuleName, strFileName, tkFile);
      this.m_TypeBuilderDict = new Dictionary<string, Type>();
    }

    [SecurityCritical]
    internal void ModifyModuleName(string name)
    {
      this.m_moduleData.ModifyModuleName(name);
      ModuleBuilder.SetModuleName(this.GetNativeHandle(), name);
    }

    internal void SetSymWriter(ISymbolWriter writer)
    {
      this.m_iSymWriter = writer;
    }

    internal override ModuleHandle GetModuleHandle()
    {
      return new ModuleHandle(this.GetNativeHandle());
    }

    internal RuntimeModule GetNativeHandle()
    {
      return this.InternalModule.GetNativeHandle();
    }

    private static RuntimeModule GetRuntimeModuleFromModule(Module m)
    {
      ModuleBuilder moduleBuilder = m as ModuleBuilder;
      if ((Module) moduleBuilder != (Module) null)
        return (RuntimeModule) moduleBuilder.InternalModule;
      return m as RuntimeModule;
    }

    [SecurityCritical]
    private int GetMemberRefToken(MethodBase method, IEnumerable<Type> optionalParameterTypes)
    {
      int cGenericParameters = 0;
      if (method.IsGenericMethod)
      {
        if (!method.IsGenericMethodDefinition)
          throw new InvalidOperationException();
        cGenericParameters = method.GetGenericArguments().Length;
      }
      if (optionalParameterTypes != null && (method.CallingConvention & CallingConventions.VarArgs) == (CallingConventions) 0)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAVarArgCallingConvention"));
      MethodInfo method1 = method as MethodInfo;
      Type[] parameterTypes;
      Type methodBaseReturnType;
      if (method.DeclaringType.IsGenericType)
      {
        MethodOnTypeBuilderInstantiation builderInstantiation1;
        MethodBase method2;
        if ((MethodInfo) (builderInstantiation1 = method as MethodOnTypeBuilderInstantiation) != (MethodInfo) null)
        {
          method2 = (MethodBase) builderInstantiation1.m_method;
        }
        else
        {
          ConstructorOnTypeBuilderInstantiation builderInstantiation2;
          if ((ConstructorInfo) (builderInstantiation2 = method as ConstructorOnTypeBuilderInstantiation) != (ConstructorInfo) null)
            method2 = (MethodBase) builderInstantiation2.m_ctor;
          else if (method is MethodBuilder || method is ConstructorBuilder)
            method2 = method;
          else if (method.IsGenericMethod)
          {
            MethodBase methodBase = (MethodBase) method1.GetGenericMethodDefinition();
            method2 = methodBase.Module.ResolveMethod(method.MetadataToken, methodBase.DeclaringType != (Type) null ? methodBase.DeclaringType.GetGenericArguments() : (Type[]) null, methodBase.GetGenericArguments());
          }
          else
            method2 = method.Module.ResolveMethod(method.MetadataToken, method.DeclaringType != (Type) null ? method.DeclaringType.GetGenericArguments() : (Type[]) null, (Type[]) null);
        }
        parameterTypes = method2.GetParameterTypes();
        methodBaseReturnType = MethodBuilder.GetMethodBaseReturnType(method2);
      }
      else
      {
        parameterTypes = method.GetParameterTypes();
        methodBaseReturnType = MethodBuilder.GetMethodBaseReturnType(method);
      }
      int length1;
      byte[] signature = this.GetMemberRefSignature(method.CallingConvention, methodBaseReturnType, parameterTypes, optionalParameterTypes, cGenericParameters).InternalGetSignature(out length1);
      int length2;
      return this.GetMemberRefFromSignature(!method.DeclaringType.IsGenericType ? (method.Module.Equals((object) this) ? (!(method1 != (MethodInfo) null) ? this.GetConstructorToken(method as ConstructorInfo).Token : this.GetMethodToken(method1).Token) : this.GetTypeToken(method.DeclaringType).Token) : this.GetTokenFromTypeSpec(SignatureHelper.GetTypeSigToken((Module) this, method.DeclaringType).InternalGetSignature(out length2), length2), method.Name, signature, length1);
    }

    [SecurityCritical]
    internal SignatureHelper GetMemberRefSignature(CallingConventions call, Type returnType, Type[] parameterTypes, IEnumerable<Type> optionalParameterTypes, int cGenericParameters)
    {
      int num1 = parameterTypes == null ? 0 : parameterTypes.Length;
      SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper((Module) this, call, returnType, cGenericParameters);
      for (int index = 0; index < num1; ++index)
        methodSigHelper.AddArgument(parameterTypes[index]);
      if (optionalParameterTypes != null)
      {
        int num2 = 0;
        foreach (Type optionalParameterType in optionalParameterTypes)
        {
          if (num2 == 0)
            methodSigHelper.AddSentinel();
          methodSigHelper.AddArgument(optionalParameterType);
          ++num2;
        }
      }
      return methodSigHelper;
    }

    /// <summary>返回一个值，该值指示此实例是否与指定的对象相等。</summary>
    /// <returns>如果 <paramref name="obj" /> 等于此实例的类型和值，则为 true；否则为 false。</returns>
    /// <param name="obj">与此实例进行比较的 object，或 null。</param>
    public override bool Equals(object obj)
    {
      return this.InternalModule.Equals(obj);
    }

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    public override int GetHashCode()
    {
      return this.InternalModule.GetHashCode();
    }

    /// <summary>返回已应用于当前 <see cref="T:System.Reflection.Emit.ModuleBuilder" /> 的所有自定义特性。</summary>
    /// <returns>一个数组，其中包含自定义特性；如果没有特性，则该数组为空。</returns>
    /// <param name="inherit">对于该类型的对象，该参数被忽略。</param>
    public override object[] GetCustomAttributes(bool inherit)
    {
      return this.InternalModule.GetCustomAttributes(inherit);
    }

    /// <summary>返回已应用于当前 <see cref="T:System.Reflection.Emit.ModuleBuilder" /> 且派生自指定的特性类型的所有自定义特性。</summary>
    /// <returns>一个数组，其中包含从 <paramref name="attributeType" /> 以任何级别派生的自定义特性；如果没有这样的特性，则该数组为空。</returns>
    /// <param name="attributeType">从中派生特性的基类。</param>
    /// <param name="inherit">对于该类型的对象，该参数被忽略。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不是一个由运行库提供的 <see cref="T:System.Type" /> 对象。例如，<paramref name="attributeType" /> 是一个 <see cref="T:System.Reflection.Emit.TypeBuilder" /> 对象。</exception>
    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      return this.InternalModule.GetCustomAttributes(attributeType, inherit);
    }

    /// <summary>返回一个值，该值指示是否已将指定的特性类型应用于此模块。</summary>
    /// <returns>如果一个或多个 <paramref name="attributeType" /> 实例已应用于此模块，则为 true；否则为 false。</returns>
    /// <param name="attributeType">要测试的自定义特性的类型。</param>
    /// <param name="inherit">对于该类型的对象，该参数被忽略。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不是一个由运行库提供的 <see cref="T:System.Type" /> 对象。例如，<paramref name="attributeType" /> 是一个 <see cref="T:System.Reflection.Emit.TypeBuilder" /> 对象。</exception>
    public override bool IsDefined(Type attributeType, bool inherit)
    {
      return this.InternalModule.IsDefined(attributeType, inherit);
    }

    /// <summary>返回有关已应用于当前 <see cref="T:System.Reflection.Emit.ModuleBuilder" />（表示为 <see cref="T:System.Reflection.CustomAttributeData" /> 对象）的特性的信息。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.CustomAttributeData" /> 对象泛型列表，这些对象表示已应用于当前模块的特性的有关数据。</returns>
    public override IList<CustomAttributeData> GetCustomAttributesData()
    {
      return this.InternalModule.GetCustomAttributesData();
    }

    /// <summary>返回在此模块内定义的所有类。</summary>
    /// <returns>一个数组，包含在此实例反射的模块内定义的类型。</returns>
    /// <exception cref="T:System.Reflection.ReflectionTypeLoadException">未能加载模块中的一个或多个类。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public override Type[] GetTypes()
    {
      lock (this.SyncRoot)
        return this.GetTypesNoLock();
    }

    internal Type[] GetTypesNoLock()
    {
      int count = this.m_TypeBuilderDict.Count;
      Type[] typeArray = new Type[this.m_TypeBuilderDict.Count];
      int num = 0;
      foreach (Type type in this.m_TypeBuilderDict.Values)
      {
        EnumBuilder enumBuilder = type as EnumBuilder;
        TypeBuilder typeBuilder = !((Type) enumBuilder != (Type) null) ? (TypeBuilder) type : enumBuilder.m_typeBuilder;
        typeArray[num++] = !typeBuilder.IsCreated() ? type : typeBuilder.UnderlyingSystemType;
      }
      return typeArray;
    }

    /// <summary>获取模块中定义的命名类型。</summary>
    /// <returns>如果已在此模块中定义了请求的类型，则为此类型；否则为 null。</returns>
    /// <param name="className">要获取的 <see cref="T:System.Type" /> 的名称。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="className" /> 的长度为零或大于 1023。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="className" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">请求的 <see cref="T:System.Type" /> 是非公共的，且调用方没有将非公共对象反射到当前程序集外部的 <see cref="T:System.Security.Permissions.ReflectionPermission" />。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">调用类初始值设定项并引发异常。</exception>
    /// <exception cref="T:System.TypeLoadException">加载 <see cref="T:System.Type" /> 时遇到错误。</exception>
    [ComVisible(true)]
    public override Type GetType(string className)
    {
      return this.GetType(className, false, false);
    }

    /// <summary>获取模块中定义的命名类型，可以忽略类型名称的大小写。</summary>
    /// <returns>如果已在此模块中定义了请求的类型，则为此类型；否则为 null。</returns>
    /// <param name="className">要获取的 <see cref="T:System.Type" /> 的名称。</param>
    /// <param name="ignoreCase">如果为 true，则搜索不区分大小写。如果为 false，则搜索区分大小写。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="className" /> 的长度为零或大于 1023。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="className" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">请求的 <see cref="T:System.Type" /> 是非公共的，且调用方没有将非公共对象反射到当前程序集外部的 <see cref="T:System.Security.Permissions.ReflectionPermission" />。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">调用类初始值设定项并引发异常。</exception>
    [ComVisible(true)]
    public override Type GetType(string className, bool ignoreCase)
    {
      return this.GetType(className, false, ignoreCase);
    }

    /// <summary>获取模块中定义的命名类型，可以忽略类型名称的大小写。如果未找到该类型，则可选择引发异常。</summary>
    /// <returns>如果指定类型已在此模块中声明，则为该类型；否则为 null。</returns>
    /// <param name="className">要获取的 <see cref="T:System.Type" /> 的名称。</param>
    /// <param name="throwOnError">如果为 true，则在找不到该类型时引发异常；如果为 false，则返回 null。</param>
    /// <param name="ignoreCase">如果为 true，则搜索不区分大小写。如果为 false，则搜索区分大小写。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="className" /> 的长度为零或大于 1023。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="className" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">请求的 <see cref="T:System.Type" /> 是非公共的，且调用方没有将非公共对象反射到当前程序集外部的 <see cref="T:System.Security.Permissions.ReflectionPermission" />。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">调用类初始值设定项并引发异常。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="throwOnError" /> 为 true，未找到指定的类型。</exception>
    [ComVisible(true)]
    public override Type GetType(string className, bool throwOnError, bool ignoreCase)
    {
      lock (this.SyncRoot)
        return this.GetTypeNoLock(className, throwOnError, ignoreCase);
    }

    private Type GetTypeNoLock(string className, bool throwOnError, bool ignoreCase)
    {
      Type baseType = this.InternalModule.GetType(className, throwOnError, ignoreCase);
      if (baseType != (Type) null)
        return baseType;
      string str1 = (string) null;
      string strFormat = (string) null;
      int num1;
      for (int startIndex = 0; startIndex <= className.Length; startIndex = num1 + 1)
      {
        num1 = className.IndexOfAny(new char[3]{ '[', '*', '&' }, startIndex);
        if (num1 == -1)
        {
          str1 = className;
          strFormat = (string) null;
          break;
        }
        int num2 = 0;
        for (int index = num1 - 1; index >= 0 && (int) className[index] == 92; --index)
          ++num2;
        if (num2 % 2 != 1)
        {
          str1 = className.Substring(0, num1);
          strFormat = className.Substring(num1);
          break;
        }
      }
      if (str1 == null)
      {
        str1 = className;
        strFormat = (string) null;
      }
      string str2 = str1.Replace("\\\\", "\\").Replace("\\[", "[").Replace("\\*", "*").Replace("\\&", "&");
      if (strFormat != null)
        baseType = this.InternalModule.GetType(str2, false, ignoreCase);
      if (baseType == (Type) null)
      {
        baseType = this.FindTypeBuilderWithName(str2, ignoreCase);
        if (baseType == (Type) null && this.Assembly is AssemblyBuilder)
        {
          List<ModuleBuilder> moduleBuilderList = this.ContainingAssemblyBuilder.m_assemblyData.m_moduleBuilderList;
          int count = moduleBuilderList.Count;
          for (int index = 0; index < count && baseType == (Type) null; ++index)
            baseType = moduleBuilderList[index].FindTypeBuilderWithName(str2, ignoreCase);
        }
        if (baseType == (Type) null)
          return (Type) null;
      }
      if (strFormat == null)
        return baseType;
      return this.GetType(strFormat, baseType);
    }

    /// <summary>返回由元数据标记标识的签名 Blob。</summary>
    /// <returns>一个字节数组，表示签名 Blob。</returns>
    /// <param name="metadataToken">一个元数据标记，用于标识模块中的一个签名。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的有效 MemberRef、MethodDef、TypeSpec、签名或 FieldDef 标记。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的有效标记。</exception>
    public override byte[] ResolveSignature(int metadataToken)
    {
      return this.InternalModule.ResolveSignature(metadataToken);
    }

    /// <summary>在由指定的泛型类型参数定义的上下文中，返回由指定的元数据标记标识的方法或构造函数。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.MethodBase" /> 对象，表示由指定的元数据标记标识的方法。</returns>
    /// <param name="metadataToken">一个元数据标记，用于标识模块中的方法或构造函数。</param>
    /// <param name="genericTypeArguments">一个 <see cref="T:System.Type" /> 对象的数组，这些对象表示以下类型的泛型类型参数，在该类型中，标记在范围内；如果该类型不是泛型类型，则为 null。</param>
    /// <param name="genericMethodArguments">一个 <see cref="T:System.Type" /> 对象的数组，这些对象表示以下方法的泛型类型参数，在该方法中，标记在范围内；如果该方法不是泛型方法，则为 null。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的方法或构造函数的标记。- 或 -<paramref name="metadataToken" /> 是一个 MethodSpec，其签名包含元素类型 var（泛型类型的类型参数）或 mvar（泛型方法的类型参数），并且没有为 <paramref name="genericTypeArguments" /> 和（或）<paramref name="genericMethodArguments" /> 提供必要的泛型类型参数。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的有效标记。</exception>
    public override MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      return this.InternalModule.ResolveMethod(metadataToken, genericTypeArguments, genericMethodArguments);
    }

    /// <summary>在由指定的泛型类型参数定义的上下文中，返回由指定的元数据标记标识的字段。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.FieldInfo" /> 对象，表示由指定的元数据标记标识的字段。</returns>
    /// <param name="metadataToken">一个元数据标记，用于标识模块中的一个字段。</param>
    /// <param name="genericTypeArguments">一个 <see cref="T:System.Type" /> 对象的数组，这些对象表示以下类型的泛型类型参数，在该类型中，标记在范围内；如果该类型不是泛型类型，则为 null。</param>
    /// <param name="genericMethodArguments">一个 <see cref="T:System.Type" /> 对象的数组，这些对象表示以下方法的泛型类型参数，在该方法中，标记在范围内；如果该方法不是泛型方法，则为 null。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的字段的标记。- 或 -<paramref name="metadataToken" /> 标识一个字段，该字段的父 TypeSpec 具有一个包含元素类型 var（泛型类型的类型参数）或 mvar（泛型方法的类型参数）的签名，并且没有为 <paramref name="genericTypeArguments" /> 和（或）<paramref name="genericMethodArguments" /> 提供必要的泛型类型参数。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的有效标记。</exception>
    public override FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      return this.InternalModule.ResolveField(metadataToken, genericTypeArguments, genericMethodArguments);
    }

    /// <summary>在由指定的泛型类型参数定义的上下文中，返回由指定的元数据标记标识的类型。</summary>
    /// <returns>一个 <see cref="T:System.Type" /> 对象，表示由指定的元数据标记标识的类型。</returns>
    /// <param name="metadataToken">一个元数据标记，用于标识模块中的一个类型。</param>
    /// <param name="genericTypeArguments">一个 <see cref="T:System.Type" /> 对象的数组，这些对象表示以下类型的泛型类型参数，在该类型中，标记在范围内；如果该类型不是泛型类型，则为 null。</param>
    /// <param name="genericMethodArguments">一个 <see cref="T:System.Type" /> 对象的数组，这些对象表示以下方法的泛型类型参数，在该方法中，标记在范围内；如果该方法不是泛型方法，则为 null。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的类型的标记。- 或 -<paramref name="metadataToken" /> 是一个 TypeSpec，其签名包含元素类型 var（泛型类型的类型参数）或 mvar（泛型方法的类型参数），并且没有为 <paramref name="genericTypeArguments" /> 和（或）<paramref name="genericMethodArguments" /> 提供必要的泛型类型参数。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的有效标记。</exception>
    public override Type ResolveType(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      return this.InternalModule.ResolveType(metadataToken, genericTypeArguments, genericMethodArguments);
    }

    /// <summary>在由指定的泛型类型参数定义的上下文中，返回由指定的元数据标记标识的类型或成员。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.MemberInfo" /> 对象，表示由指定的元数据标记标识的类型或成员。</returns>
    /// <param name="metadataToken">一个元数据标记，用于标识模块中的类型或成员。</param>
    /// <param name="genericTypeArguments">一个 <see cref="T:System.Type" /> 对象的数组，这些对象表示以下类型的泛型类型参数，在该类型中，标记在范围内；如果该类型不是泛型类型，则为 null。</param>
    /// <param name="genericMethodArguments">一个 <see cref="T:System.Type" /> 对象的数组，这些对象表示以下方法的泛型类型参数，在该方法中，标记在范围内；如果该方法不是泛型方法，则为 null。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的类型或成员的标记。- 或 -<paramref name="metadataToken" /> 是一个 MethodSpec 或 TypeSpec，其签名包含元素类型 var（泛型类型的类型参数）或 mvar（泛型方法的类型参数），并且没有为 <paramref name="genericTypeArguments" /> 和（或）<paramref name="genericMethodArguments" /> 提供必要的泛型类型参数。- 或 -<paramref name="metadataToken" /> 标识一个属性或事件。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的有效标记。</exception>
    public override MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      return this.InternalModule.ResolveMember(metadataToken, genericTypeArguments, genericMethodArguments);
    }

    /// <summary>返回由指定元数据标记标识的字符串。</summary>
    /// <returns>一个 <see cref="T:System.String" />，包含来自元数据字符串堆的一个字符串值。</returns>
    /// <param name="metadataToken">一个元数据标记，用于标识模块的字符串堆中的一个字符串。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的字符串的标记。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的有效标记。</exception>
    public override string ResolveString(int metadataToken)
    {
      return this.InternalModule.ResolveString(metadataToken);
    }

    /// <summary>获取一对值，这一对值指示某个模块中代码的性质和该模块的目标平台。</summary>
    /// <param name="peKind">当此方法返回时，为 <see cref="T:System.Reflection.PortableExecutableKinds" /> 值的组合，用于指示模块中代码的性质。</param>
    /// <param name="machine">当此方法返回时，为 <see cref="T:System.Reflection.ImageFileMachine" /> 值中的一个，用于指示模块的目标平台。</param>
    public override void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
    {
      this.InternalModule.GetPEKind(out peKind, out machine);
    }

    /// <summary>获取一个值，该值指示此对象是否是资源。</summary>
    /// <returns>如果此对象是资源，则为 true；否则为 false。</returns>
    public override bool IsResource()
    {
      return this.InternalModule.IsResource();
    }

    /// <summary>返回在可移植可执行 (PE) 文件的 .sdata 区域中定义的、与指定绑定标志匹配的所有字段。</summary>
    /// <returns>与指定标志匹配的字段的数组；如果不存在这样的字段，则数组为空。</returns>
    /// <param name="bindingFlags">用于控制搜索的 BindingFlags 位标志的组合。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    public override FieldInfo[] GetFields(BindingFlags bindingFlags)
    {
      return this.InternalModule.GetFields(bindingFlags);
    }

    /// <summary>返回在可移植可执行 (PE) 文件的 .sdata 区域中定义的、具有指定名称和绑定特性的模块级字段。</summary>
    /// <returns>一个具有指定名称及绑定特性的字段；或者如果该字段不存在，则为 null。</returns>
    /// <param name="name">字段名。</param>
    /// <param name="bindingAttr">用于控制搜索的 BindingFlags 位标志的组合。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    public override FieldInfo GetField(string name, BindingFlags bindingAttr)
    {
      return this.InternalModule.GetField(name, bindingAttr);
    }

    /// <summary>返回已在模块级别上为当前 <see cref="T:System.Reflection.Emit.ModuleBuilder" /> 定义并与指定的绑定标志匹配的所有方法。</summary>
    /// <returns>一个数组，包含与 <paramref name="bindingFlags" /> 匹配的所有模块级方法。</returns>
    /// <param name="bindingFlags">用于控制搜索的 BindingFlags 位标志的组合。</param>
    public override MethodInfo[] GetMethods(BindingFlags bindingFlags)
    {
      return this.InternalModule.GetMethods(bindingFlags);
    }

    /// <summary>返回符合指定条件的模块级方法。</summary>
    /// <returns>在模块级定义并符合指定条件的方法；如果不存在这样的方法，则为 null。</returns>
    /// <param name="name">方法名。</param>
    /// <param name="bindingAttr">用于控制搜索的 BindingFlags 位标志的组合。</param>
    /// <param name="binder">实现 Binder 的对象，它包含与此方法相关的属性。</param>
    /// <param name="callConvention">该方法的调用约定。</param>
    /// <param name="types">该方法的参数类型。</param>
    /// <param name="modifiers">参数修饰符数组，用来与参数签名进行绑定，这些参数签名中的类型已经被修改。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null，<paramref name="types" /> 为 null，或者 <paramref name="types" /> 的某个元素为 null。</exception>
    protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      return this.InternalModule.GetMethodInternal(name, bindingAttr, binder, callConvention, types, modifiers);
    }

    /// <summary>返回与证书（包括在此模块所属的程序集的 Authenticode 签名中）对应的 <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> 对象。如果此程序集没有 Authenticode 签名，则返回 null。</summary>
    /// <returns>一个证书；如果此模块所属的程序集没有 Authenticode 签名，则为 null。</returns>
    public override X509Certificate GetSignerCertificate()
    {
      return this.InternalModule.GetSignerCertificate();
    }

    /// <summary>在此模块中用指定的名称为私有类型构造 TypeBuilder。</summary>
    /// <returns>具有指定名称的私有类型。</returns>
    /// <param name="name">类型的完整路径，其中包括命名空间。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <exception cref="T:System.ArgumentException">具有给定名称的类型存在于此模块的父程序集中。- 或 -在未嵌套的类型上设置嵌套类型属性。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    [SecuritySafeCritical]
    public TypeBuilder DefineType(string name)
    {
      lock (this.SyncRoot)
        return this.DefineTypeNoLock(name, TypeAttributes.NotPublic, (Type) null, (Type[]) null, PackingSize.Unspecified, 0);
    }

    /// <summary>在给定类型名称和类型特性的情况下，构造 TypeBuilder。</summary>
    /// <returns>用所有请求的特性创建的 TypeBuilder。</returns>
    /// <param name="name">类型的完整路径。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="attr">已定义类型的属性。</param>
    /// <exception cref="T:System.ArgumentException">具有给定名称的类型存在于此模块的父程序集中。- 或 -在未嵌套的类型上设置嵌套类型属性。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    [SecuritySafeCritical]
    public TypeBuilder DefineType(string name, TypeAttributes attr)
    {
      lock (this.SyncRoot)
        return this.DefineTypeNoLock(name, attr, (Type) null, (Type[]) null, PackingSize.Unspecified, 0);
    }

    /// <summary>在给定类型名称、类型特性和已定义类型扩展的类型的情况下，构造 TypeBuilder。</summary>
    /// <returns>用所有请求的特性创建的 TypeBuilder。</returns>
    /// <param name="name">类型的完整路径。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="attr">与类型关联的属性。</param>
    /// <param name="parent">已定义类型扩展的类型。</param>
    /// <exception cref="T:System.ArgumentException">具有给定名称的类型存在于此模块的父程序集中。- 或 -在未嵌套的类型上设置嵌套类型属性。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    [SecuritySafeCritical]
    public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent)
    {
      lock (this.SyncRoot)
      {
        this.CheckContext(new Type[1]{ parent });
        return this.DefineTypeNoLock(name, attr, parent, (Type[]) null, PackingSize.Unspecified, 0);
      }
    }

    /// <summary>在给定类型名称、特性、已定义类型扩展的类型和类型的总大小的情况下，构造 TypeBuilder。</summary>
    /// <returns>一个 TypeBuilder 对象。</returns>
    /// <param name="name">类型的完整路径。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="attr">已定义类型的属性。</param>
    /// <param name="parent">已定义类型扩展的类型。</param>
    /// <param name="typesize">类型的总大小。</param>
    /// <exception cref="T:System.ArgumentException">具有给定名称的类型存在于此模块的父程序集中。- 或 -在未嵌套的类型上设置嵌套类型属性。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    [SecuritySafeCritical]
    public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, int typesize)
    {
      lock (this.SyncRoot)
        return this.DefineTypeNoLock(name, attr, parent, (Type[]) null, PackingSize.Unspecified, typesize);
    }

    /// <summary>在给定类型名称、特性、已定义类型扩展的类型，已定义类型的封装大小和已定义类型的总大小的情况下，构造 TypeBuilder。</summary>
    /// <returns>用所有请求的特性创建的 TypeBuilder。</returns>
    /// <param name="name">类型的完整路径。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="attr">已定义类型的属性。</param>
    /// <param name="parent">已定义类型扩展的类型。</param>
    /// <param name="packingSize">该类型的封装大小。</param>
    /// <param name="typesize">类型的总大小。</param>
    /// <exception cref="T:System.ArgumentException">具有给定名称的类型存在于此模块的父程序集中。- 或 -在未嵌套的类型上设置嵌套类型属性。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    [SecuritySafeCritical]
    public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, PackingSize packingSize, int typesize)
    {
      lock (this.SyncRoot)
        return this.DefineTypeNoLock(name, attr, parent, (Type[]) null, packingSize, typesize);
    }

    /// <summary>在给定类型名称、特性、已定义类型扩展的类型和已定义类型实现的接口的情况下，构造 TypeBuilder。</summary>
    /// <returns>用所有请求的特性创建的 TypeBuilder。</returns>
    /// <param name="name">类型的完整路径。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="attr">与类型关联的特性。</param>
    /// <param name="parent">已定义类型扩展的类型。</param>
    /// <param name="interfaces">类型实现的接口列表。</param>
    /// <exception cref="T:System.ArgumentException">具有给定名称的类型存在于此模块的父程序集中。- 或 -在未嵌套的类型上设置嵌套类型属性。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, Type[] interfaces)
    {
      lock (this.SyncRoot)
        return this.DefineTypeNoLock(name, attr, parent, interfaces, PackingSize.Unspecified, 0);
    }

    [SecurityCritical]
    private TypeBuilder DefineTypeNoLock(string name, TypeAttributes attr, Type parent, Type[] interfaces, PackingSize packingSize, int typesize)
    {
      return new TypeBuilder(name, attr, parent, interfaces, this, packingSize, typesize, (TypeBuilder) null);
    }

    /// <summary>在给定类型名称、特性、已定义类型扩展的类型和类型的封装大小的情况下，构造 TypeBuilder。</summary>
    /// <returns>一个 TypeBuilder 对象。</returns>
    /// <param name="name">类型的完整路径。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="attr">已定义类型的属性。</param>
    /// <param name="parent">已定义类型扩展的类型。</param>
    /// <param name="packsize">该类型的封装大小。</param>
    /// <exception cref="T:System.ArgumentException">具有给定名称的类型存在于此模块的父程序集中。- 或 -在未嵌套的类型上设置嵌套类型属性。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    [SecuritySafeCritical]
    public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, PackingSize packsize)
    {
      lock (this.SyncRoot)
        return this.DefineTypeNoLock(name, attr, parent, packsize);
    }

    [SecurityCritical]
    private TypeBuilder DefineTypeNoLock(string name, TypeAttributes attr, Type parent, PackingSize packsize)
    {
      return new TypeBuilder(name, attr, parent, (Type[]) null, this, packsize, 0, (TypeBuilder) null);
    }

    /// <summary>用指定类型的单个非静态字段（称为 <paramref name="value__" />）定义属于值类型的枚举类型。</summary>
    /// <returns>已定义的枚举。</returns>
    /// <param name="name">枚举类型的完整路径。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="visibility">枚举的类型特性。这些特性是由 <see cref="F:System.Reflection.TypeAttributes.VisibilityMask" /> 定义的任何位。</param>
    /// <param name="underlyingType">枚举的基础类型。此类型必须是一种内置的整数类型。</param>
    /// <exception cref="T:System.ArgumentException">提供的属性不是可见性属性。- 或 -具有给定名称的枚举存在于此模块的父程序集中。- 或 -可见性属性与该枚举的范围不匹配。例如，将 <paramref name="visibility" /> 指定为 <see cref="F:System.Reflection.TypeAttributes.NestedPublic" />，但是枚举不是嵌套类型。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    [SecuritySafeCritical]
    public EnumBuilder DefineEnum(string name, TypeAttributes visibility, Type underlyingType)
    {
      this.CheckContext(new Type[1]{ underlyingType });
      lock (this.SyncRoot)
      {
        EnumBuilder local_2 = this.DefineEnumNoLock(name, visibility, underlyingType);
        this.m_TypeBuilderDict[name] = (Type) local_2;
        return local_2;
      }
    }

    [SecurityCritical]
    private EnumBuilder DefineEnumNoLock(string name, TypeAttributes visibility, Type underlyingType)
    {
      return new EnumBuilder(name, underlyingType, visibility, this);
    }

    /// <summary>定义要存储在此模块中的已命名托管嵌入资源。</summary>
    /// <returns>已定义资源的资源编写器。</returns>
    /// <param name="name">资源的名称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="description">该资源的说明。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 的长度为零。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">此模块是瞬态的。- 或 -包含程序集不是持久的。</exception>
    public IResourceWriter DefineResource(string name, string description)
    {
      return this.DefineResource(name, description, ResourceAttributes.Public);
    }

    /// <summary>用给定的特性定义存储在此模块中的已命名托管嵌入资源。</summary>
    /// <returns>已定义资源的资源编写器。</returns>
    /// <param name="name">资源的名称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="description">该资源的说明。</param>
    /// <param name="attribute">资源属性。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 的长度为零。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">此模块是瞬态的。- 或 -包含程序集不是持久的。</exception>
    public IResourceWriter DefineResource(string name, string description, ResourceAttributes attribute)
    {
      lock (this.SyncRoot)
        return this.DefineResourceNoLock(name, description, attribute);
    }

    private IResourceWriter DefineResourceNoLock(string name, string description, ResourceAttributes attribute)
    {
      if (this.IsTransient())
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadResourceContainer"));
      if (name == null)
        throw new ArgumentNullException("name");
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
      if (!this.m_assemblyBuilder.IsPersistable())
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadResourceContainer"));
      this.m_assemblyBuilder.m_assemblyData.CheckResNameConflict(name);
      MemoryStream memoryStream1 = new MemoryStream();
      ResourceWriter resWriter = new ResourceWriter((Stream) memoryStream1);
      MemoryStream memoryStream2 = memoryStream1;
      string strName = name;
      string strFileName = string.Empty;
      string strFullFileName = string.Empty;
      int num = (int) attribute;
      this.m_moduleData.m_embeddedRes = new ResWriterData(resWriter, (Stream) memoryStream2, strName, strFileName, strFullFileName, (ResourceAttributes) num)
      {
        m_nextResWriter = this.m_moduleData.m_embeddedRes
      };
      return (IResourceWriter) resWriter;
    }

    /// <summary>定义表示要在动态程序集中嵌入的清单资源的二进制大对象 (BLOB)。</summary>
    /// <param name="name">资源的区分大小写的名称。</param>
    /// <param name="stream">包含资源字节的流。</param>
    /// <param name="attribute">一个枚举值，用于指定资源是公共资源还是私有资源。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。- 或 -<paramref name="stream" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 是零长度字符串。</exception>
    /// <exception cref="T:System.InvalidOperationException">包含当前模块的动态程序集是瞬态的；也就是说，调用 <see cref="M:System.Reflection.Emit.AssemblyBuilder.DefineDynamicModule(System.String,System.String)" /> 时没有指定任何文件名。</exception>
    public void DefineManifestResource(string name, Stream stream, ResourceAttributes attribute)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (stream == null)
        throw new ArgumentNullException("stream");
      lock (this.SyncRoot)
        this.DefineManifestResourceNoLock(name, stream, attribute);
    }

    private void DefineManifestResourceNoLock(string name, Stream stream, ResourceAttributes attribute)
    {
      if (this.IsTransient())
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadResourceContainer"));
      if (name == null)
        throw new ArgumentNullException("name");
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
      if (!this.m_assemblyBuilder.IsPersistable())
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadResourceContainer"));
      this.m_assemblyBuilder.m_assemblyData.CheckResNameConflict(name);
      this.m_moduleData.m_embeddedRes = new ResWriterData((ResourceWriter) null, stream, name, string.Empty, string.Empty, attribute)
      {
        m_nextResWriter = this.m_moduleData.m_embeddedRes
      };
    }

    /// <summary>已知不透明的字节二进制大对象 (BLOB)，定义非托管嵌入资源。</summary>
    /// <param name="resource">表示非托管资源的不透明 BLOB</param>
    /// <exception cref="T:System.ArgumentException">已经在模块的程序集中定义了一个非托管资源。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="resource" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public void DefineUnmanagedResource(byte[] resource)
    {
      lock (this.SyncRoot)
        this.DefineUnmanagedResourceInternalNoLock(resource);
    }

    internal void DefineUnmanagedResourceInternalNoLock(byte[] resource)
    {
      if (resource == null)
        throw new ArgumentNullException("resource");
      if (this.m_moduleData.m_strResourceFileName != null || this.m_moduleData.m_resourceBytes != null)
        throw new ArgumentException(Environment.GetResourceString("Argument_NativeResourceAlreadyDefined"));
      this.m_moduleData.m_resourceBytes = new byte[resource.Length];
      Array.Copy((Array) resource, (Array) this.m_moduleData.m_resourceBytes, resource.Length);
    }

    /// <summary>在给定 Win32 资源文件名称的情况下，定义非托管资源。</summary>
    /// <param name="resourceFileName">非托管资源文件的名称。</param>
    /// <exception cref="T:System.ArgumentException">已经在模块的程序集中定义了一个非托管资源。- 或 -<paramref name="resourceFileName" /> 是空字符串 ("")。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="resourceFileName" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到 <paramref name="resourceFileName" />。- 或 -<paramref name="resourceFileName" /> 是一个目录。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public void DefineUnmanagedResource(string resourceFileName)
    {
      lock (this.SyncRoot)
        this.DefineUnmanagedResourceFileInternalNoLock(resourceFileName);
    }

    [SecurityCritical]
    internal void DefineUnmanagedResourceFileInternalNoLock(string resourceFileName)
    {
      if (resourceFileName == null)
        throw new ArgumentNullException("resourceFileName");
      if (this.m_moduleData.m_resourceBytes != null || this.m_moduleData.m_strResourceFileName != null)
        throw new ArgumentException(Environment.GetResourceString("Argument_NativeResourceAlreadyDefined"));
      string fullPath = Path.UnsafeGetFullPath(resourceFileName);
      new FileIOPermission(FileIOPermissionAccess.Read, fullPath).Demand();
      new EnvironmentPermission(PermissionState.Unrestricted).Assert();
      try
      {
        if (!File.UnsafeExists(resourceFileName))
          throw new FileNotFoundException(Environment.GetResourceString("IO.FileNotFound_FileName", (object) resourceFileName), resourceFileName);
      }
      finally
      {
        CodeAccessPermission.RevertAssert();
      }
      this.m_moduleData.m_strResourceFileName = fullPath;
    }

    /// <summary>使用指定的名称、属性、返回类型和参数类型定义一个全局方法。</summary>
    /// <returns>已定义的全局方法。</returns>
    /// <param name="name">方法的名称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="attributes">该方法的特性。<paramref name="attributes" /> 必须包括 <see cref="F:System.Reflection.MethodAttributes.Static" />。</param>
    /// <param name="returnType">该方法的返回类型。</param>
    /// <param name="parameterTypes">方法参数的类型。</param>
    /// <exception cref="T:System.ArgumentException">该方法不是静态的。也就是说，<paramref name="attributes" /> 不包括 <see cref="F:System.Reflection.MethodAttributes.Static" />。- 或 -<paramref name="name" /> 的长度为零。- 或 -<see cref="T:System.Type" /> 数组中的一个元素为 null。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">以前调用过 <see cref="M:System.Reflection.Emit.ModuleBuilder.CreateGlobalFunctions" />。</exception>
    public MethodBuilder DefineGlobalMethod(string name, MethodAttributes attributes, Type returnType, Type[] parameterTypes)
    {
      return this.DefineGlobalMethod(name, attributes, CallingConventions.Standard, returnType, parameterTypes);
    }

    /// <summary>定义一个具有指定名称、属性、调用约定、返回类型和参数类型的全局方法。</summary>
    /// <returns>已定义的全局方法。</returns>
    /// <param name="name">方法的名称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="attributes">该方法的特性。<paramref name="attributes" /> 必须包括 <see cref="F:System.Reflection.MethodAttributes.Static" />。</param>
    /// <param name="callingConvention">该方法的调用约定。</param>
    /// <param name="returnType">该方法的返回类型。</param>
    /// <param name="parameterTypes">方法参数的类型。</param>
    /// <exception cref="T:System.ArgumentException">该方法不是静态的。也就是说，<paramref name="attributes" /> 不包括 <see cref="F:System.Reflection.MethodAttributes.Static" />。- 或 -<see cref="T:System.Type" /> 数组中的一个元素为 null。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">以前调用过 <see cref="M:System.Reflection.Emit.ModuleBuilder.CreateGlobalFunctions" />。</exception>
    public MethodBuilder DefineGlobalMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
    {
      return this.DefineGlobalMethod(name, attributes, callingConvention, returnType, (Type[]) null, (Type[]) null, parameterTypes, (Type[][]) null, (Type[][]) null);
    }

    /// <summary>使用指定的名称、属性、调用约定、返回类型、返回类型的自定义修饰符、参数类型以及参数类型的自定义修饰符定义一个全局方法。</summary>
    /// <returns>已定义的全局方法。</returns>
    /// <param name="name">方法的名称。<paramref name="name" /> 不能包含嵌入的 null 字符。</param>
    /// <param name="attributes">该方法的特性。<paramref name="attributes" /> 必须包括 <see cref="F:System.Reflection.MethodAttributes.Static" />。</param>
    /// <param name="callingConvention">该方法的调用约定。</param>
    /// <param name="returnType">该方法的返回类型。</param>
    /// <param name="requiredReturnTypeCustomModifiers">一个表示返回类型必需的自定义修饰符的类型数组，例如 <see cref="T:System.Runtime.CompilerServices.IsConst" /> 或 <see cref="T:System.Runtime.CompilerServices.IsBoxed" />。如果返回类型没有必需的自定义修饰符，请指定 null。</param>
    /// <param name="optionalReturnTypeCustomModifiers">一个表示返回类型的可选自定义修饰符的类型数组，例如 <see cref="T:System.Runtime.CompilerServices.IsConst" /> 或 <see cref="T:System.Runtime.CompilerServices.IsBoxed" />。如果返回类型没有可选的自定义修饰符，请指定 null。</param>
    /// <param name="parameterTypes">方法参数的类型。</param>
    /// <param name="requiredParameterTypeCustomModifiers">由类型数组组成的数组。每个类型数组均表示全局方法的相应参数所必需的自定义修饰符。如果某个特定参数没有必需的自定义修饰符，请指定 null，而不要指定类型数组。如果全局方法没有参数，或者所有参数都没有必需的自定义修饰符，请指定 null，而不要指定由数组组成的数组。</param>
    /// <param name="optionalParameterTypeCustomModifiers">由类型数组组成的数组。每个类型数组均表示相应参数的可选自定义修饰符。如果某个特定参数没有可选的自定义修饰符，请指定 null，而不要指定类型数组。如果全局方法没有参数，或者所有参数都没有可选的自定义修饰符，请指定 null，而不要指定由数组组成的数组。</param>
    /// <exception cref="T:System.ArgumentException">该方法不是静态的。也就是说，<paramref name="attributes" /> 不包括 <see cref="F:System.Reflection.MethodAttributes.Static" />。- 或 -<see cref="T:System.Type" /> 数组中的一个元素为 null。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">此前已调用 <see cref="M:System.Reflection.Emit.ModuleBuilder.CreateGlobalFunctions" /> 方法。</exception>
    public MethodBuilder DefineGlobalMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
    {
      lock (this.SyncRoot)
        return this.DefineGlobalMethodNoLock(name, attributes, callingConvention, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers, parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
    }

    private MethodBuilder DefineGlobalMethodNoLock(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
    {
      if (this.m_moduleData.m_fGlobalBeenCreated)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_GlobalsHaveBeenCreated"));
      if (name == null)
        throw new ArgumentNullException("name");
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
      if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
        throw new ArgumentException(Environment.GetResourceString("Argument_GlobalFunctionHasToBeStatic"));
      this.CheckContext(new Type[1]{ returnType });
      this.CheckContext(requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers, parameterTypes);
      this.CheckContext(requiredParameterTypeCustomModifiers);
      this.CheckContext(optionalParameterTypeCustomModifiers);
      this.m_moduleData.m_fHasGlobal = true;
      return this.m_moduleData.m_globalTypeBuilder.DefineMethod(name, attributes, callingConvention, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers, parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
    }

    /// <summary>使用指定的名称、定义方法的 DLL 的名称、方法的属性、方法的调用约定、方法的返回类型、方法的参数类型以及 PInvoke 标志定义一个 PInvoke 方法。</summary>
    /// <returns>已定义的 PInvoke 方法。</returns>
    /// <param name="name">PInvoke 方法的名称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="dllName">在其中定义 PInvoke 方法的 DLL 的名称。</param>
    /// <param name="attributes">该方法的特性。</param>
    /// <param name="callingConvention">该方法的调用约定。</param>
    /// <param name="returnType">该方法的返回类型。</param>
    /// <param name="parameterTypes">方法参数的类型。</param>
    /// <param name="nativeCallConv">本机调用约定。</param>
    /// <param name="nativeCharSet">该方法的本机字符集。</param>
    /// <exception cref="T:System.ArgumentException">此方法不是静态的，或者如果包含类型是接口。- 或 -这种方法是抽象的方法。- 或 -该方法是以前定义的。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 或 <paramref name="dllName" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">以前已使用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建了该包含类型。</exception>
    public MethodBuilder DefinePInvokeMethod(string name, string dllName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
    {
      return this.DefinePInvokeMethod(name, dllName, name, attributes, callingConvention, returnType, parameterTypes, nativeCallConv, nativeCharSet);
    }

    /// <summary>使用指定的名称、定义方法的 DLL 的名称、方法的属性、方法的调用约定、方法的返回类型、方法的参数类型以及 PInvoke 标志定义一个 PInvoke 方法。</summary>
    /// <returns>已定义的 PInvoke 方法。</returns>
    /// <param name="name">PInvoke 方法的名称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="dllName">在其中定义 PInvoke 方法的 DLL 的名称。</param>
    /// <param name="entryName">DLL 中的入口点名称。</param>
    /// <param name="attributes">该方法的特性。</param>
    /// <param name="callingConvention">该方法的调用约定。</param>
    /// <param name="returnType">该方法的返回类型。</param>
    /// <param name="parameterTypes">方法参数的类型。</param>
    /// <param name="nativeCallConv">本机调用约定。</param>
    /// <param name="nativeCharSet">该方法的本机字符集。</param>
    /// <exception cref="T:System.ArgumentException">此方法不是静态的，或者如果包含类型是接口，或者如果此方法是抽象的，或者如果此方法以前定义过。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 或 <paramref name="dllName" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">以前已使用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建了该包含类型。</exception>
    public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
    {
      lock (this.SyncRoot)
        return this.DefinePInvokeMethodNoLock(name, dllName, entryName, attributes, callingConvention, returnType, parameterTypes, nativeCallConv, nativeCharSet);
    }

    private MethodBuilder DefinePInvokeMethodNoLock(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
    {
      if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
        throw new ArgumentException(Environment.GetResourceString("Argument_GlobalFunctionHasToBeStatic"));
      this.CheckContext(new Type[1]{ returnType });
      this.CheckContext(parameterTypes);
      this.m_moduleData.m_fHasGlobal = true;
      return this.m_moduleData.m_globalTypeBuilder.DefinePInvokeMethod(name, dllName, entryName, attributes, callingConvention, returnType, parameterTypes, nativeCallConv, nativeCharSet);
    }

    /// <summary>完成此动态模块的全局函数定义和全局数据定义。</summary>
    /// <exception cref="T:System.InvalidOperationException">以前调用过此方法。</exception>
    public void CreateGlobalFunctions()
    {
      lock (this.SyncRoot)
        this.CreateGlobalFunctionsNoLock();
    }

    private void CreateGlobalFunctionsNoLock()
    {
      if (this.m_moduleData.m_fGlobalBeenCreated)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotADebugModule"));
      this.m_moduleData.m_globalTypeBuilder.CreateType();
      this.m_moduleData.m_fGlobalBeenCreated = true;
    }

    /// <summary>在可移植可执行 (PE) 文件的 .sdata 部分定义已初始化的数据字段。</summary>
    /// <returns>引用这些数据的字段。</returns>
    /// <param name="name">用于引用数据的名称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="data">数据的二进制大对象 (BLOB)。</param>
    /// <param name="attributes">该字段的特性。默认值为 Static。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 的长度为零。- 或 -<paramref name="data" /> 的大小小于等于零，或者大于等于 0x3f0000。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 或 <paramref name="data" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">以前调用过 <see cref="M:System.Reflection.Emit.ModuleBuilder.CreateGlobalFunctions" />。</exception>
    public FieldBuilder DefineInitializedData(string name, byte[] data, FieldAttributes attributes)
    {
      lock (this.SyncRoot)
        return this.DefineInitializedDataNoLock(name, data, attributes);
    }

    private FieldBuilder DefineInitializedDataNoLock(string name, byte[] data, FieldAttributes attributes)
    {
      if (this.m_moduleData.m_fGlobalBeenCreated)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_GlobalsHaveBeenCreated"));
      this.m_moduleData.m_fHasGlobal = true;
      return this.m_moduleData.m_globalTypeBuilder.DefineInitializedData(name, data, attributes);
    }

    /// <summary>在可移植可执行 (PE) 文件的 .sdata 部分定义未初始化的数据字段。</summary>
    /// <returns>引用这些数据的字段。</returns>
    /// <param name="name">用于引用数据的名称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="size">该数据字段的大小。</param>
    /// <param name="attributes">该字段的特性。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 的长度为零。- 或 -<paramref name="size" /> 小于或等于零，或者大于或等于 0x003f0000。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">以前调用过 <see cref="M:System.Reflection.Emit.ModuleBuilder.CreateGlobalFunctions" />。</exception>
    public FieldBuilder DefineUninitializedData(string name, int size, FieldAttributes attributes)
    {
      lock (this.SyncRoot)
        return this.DefineUninitializedDataNoLock(name, size, attributes);
    }

    private FieldBuilder DefineUninitializedDataNoLock(string name, int size, FieldAttributes attributes)
    {
      if (this.m_moduleData.m_fGlobalBeenCreated)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_GlobalsHaveBeenCreated"));
      this.m_moduleData.m_fHasGlobal = true;
      return this.m_moduleData.m_globalTypeBuilder.DefineUninitializedData(name, size, attributes);
    }

    [SecurityCritical]
    internal TypeToken GetTypeTokenInternal(Type type)
    {
      return this.GetTypeTokenInternal(type, false);
    }

    [SecurityCritical]
    private TypeToken GetTypeTokenInternal(Type type, bool getGenericDefinition)
    {
      lock (this.SyncRoot)
        return this.GetTypeTokenWorkerNoLock(type, getGenericDefinition);
    }

    /// <summary>返回用于标识此模块内的指定类型的标记。</summary>
    /// <returns>用于标识此模块内给定类型的标记。</returns>
    /// <param name="type">表示类类型的类型对象。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="type" /> 是一种 ByRef 类型。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">这是一个引用瞬态模块的非瞬态模块。</exception>
    [SecuritySafeCritical]
    public TypeToken GetTypeToken(Type type)
    {
      return this.GetTypeTokenInternal(type, true);
    }

    [SecurityCritical]
    private TypeToken GetTypeTokenWorkerNoLock(Type type, bool getGenericDefinition)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      this.CheckContext(new Type[1]{ type });
      if (type.IsByRef)
        throw new ArgumentException(Environment.GetResourceString("Argument_CannotGetTypeTokenForByRef"));
      if (type.IsGenericType && (!type.IsGenericTypeDefinition || !getGenericDefinition) || (type.IsGenericParameter || type.IsArray || type.IsPointer))
      {
        int length;
        return new TypeToken(this.GetTokenFromTypeSpec(SignatureHelper.GetTypeSigToken((Module) this, type).InternalGetSignature(out length), length));
      }
      Module module = type.Module;
      if (module.Equals((object) this))
      {
        EnumBuilder enumBuilder = type as EnumBuilder;
        TypeBuilder typeBuilder = !((Type) enumBuilder != (Type) null) ? type as TypeBuilder : enumBuilder.m_typeBuilder;
        if ((Type) typeBuilder != (Type) null)
          return typeBuilder.TypeToken;
        GenericTypeParameterBuilder parameterBuilder;
        if ((Type) (parameterBuilder = type as GenericTypeParameterBuilder) != (Type) null)
          return new TypeToken(parameterBuilder.MetadataTokenInternal);
        return new TypeToken(this.GetTypeRefNested(type, (Module) this, string.Empty));
      }
      ModuleBuilder moduleBuilder = module as ModuleBuilder;
      if (!this.IsTransient() & ((Module) moduleBuilder != (Module) null ? moduleBuilder.IsTransient() : ((RuntimeModule) module).IsTransientInternal()))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadTransientModuleReference"));
      string strRefedModuleFileName = string.Empty;
      if (module.Assembly.Equals((object) this.Assembly))
      {
        if ((Module) moduleBuilder == (Module) null)
          moduleBuilder = this.ContainingAssemblyBuilder.GetModuleBuilder((InternalModuleBuilder) module);
        strRefedModuleFileName = moduleBuilder.m_moduleData.m_strFileName;
      }
      return new TypeToken(this.GetTypeRefNested(type, module, strRefedModuleFileName));
    }

    /// <summary>返回用于标识具有指定名称的类型的标记。</summary>
    /// <returns>用于标识此模块内具有指定名称的类型的标记。</returns>
    /// <param name="name">类的名称，包括命名空间。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 是空字符串 ("")。- 或 -<paramref name="name" /> 表示一种 ByRef 类型。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。- 或 -无法找到 <paramref name="name" /> 指定的类型。</exception>
    /// <exception cref="T:System.InvalidOperationException">这是一个引用瞬态模块的非瞬态模块。</exception>
    public TypeToken GetTypeToken(string name)
    {
      return this.GetTypeToken(this.InternalModule.GetType(name, false, true));
    }

    /// <summary>返回用于标识此模块内指定方法的标记。</summary>
    /// <returns>用于标识此模块内的指定方法的标记。</returns>
    /// <param name="method">要为其获取标记的方法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="method" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">此方法的声明类型不在此模块内。</exception>
    [SecuritySafeCritical]
    public MethodToken GetMethodToken(MethodInfo method)
    {
      lock (this.SyncRoot)
        return this.GetMethodTokenNoLock(method, true);
    }

    [SecurityCritical]
    internal MethodToken GetMethodTokenInternal(MethodInfo method)
    {
      lock (this.SyncRoot)
        return this.GetMethodTokenNoLock(method, false);
    }

    [SecurityCritical]
    private MethodToken GetMethodTokenNoLock(MethodInfo method, bool getGenericTypeDefinition)
    {
      if (method == (MethodInfo) null)
        throw new ArgumentNullException("method");
      MethodBuilder methodBuilder;
      int str;
      if ((MethodInfo) (methodBuilder = method as MethodBuilder) != (MethodInfo) null)
      {
        int metadataTokenInternal = methodBuilder.MetadataTokenInternal;
        if (method.Module.Equals((object) this))
          return new MethodToken(metadataTokenInternal);
        if (method.DeclaringType == (Type) null)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotImportGlobalFromDifferentModule"));
        int tr = getGenericTypeDefinition ? this.GetTypeToken(method.DeclaringType).Token : this.GetTypeTokenInternal(method.DeclaringType).Token;
        str = this.GetMemberRef(method.DeclaringType.Module, tr, metadataTokenInternal);
      }
      else
      {
        if (method is MethodOnTypeBuilderInstantiation)
          return new MethodToken(this.GetMemberRefToken((MethodBase) method, (IEnumerable<Type>) null));
        SymbolMethod symbolMethod;
        if ((MethodInfo) (symbolMethod = method as SymbolMethod) != (MethodInfo) null)
        {
          if (symbolMethod.GetModule() == (Module) this)
            return symbolMethod.GetToken();
          return symbolMethod.GetToken(this);
        }
        Type declaringType = method.DeclaringType;
        if (declaringType == (Type) null)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotImportGlobalFromDifferentModule"));
        if (declaringType.IsArray)
        {
          ParameterInfo[] parameters = method.GetParameters();
          Type[] parameterTypes = new Type[parameters.Length];
          for (int index = 0; index < parameters.Length; ++index)
            parameterTypes[index] = parameters[index].ParameterType;
          return this.GetArrayMethodToken(declaringType, method.Name, method.CallingConvention, method.ReturnType, parameterTypes);
        }
        RuntimeMethodInfo method1;
        if ((MethodInfo) (method1 = method as RuntimeMethodInfo) != (MethodInfo) null)
        {
          str = this.GetMemberRefOfMethodInfo(getGenericTypeDefinition ? this.GetTypeToken(method.DeclaringType).Token : this.GetTypeTokenInternal(method.DeclaringType).Token, method1);
        }
        else
        {
          ParameterInfo[] parameters = method.GetParameters();
          Type[] parameterTypes = new Type[parameters.Length];
          Type[][] requiredParameterTypeCustomModifiers = new Type[parameterTypes.Length][];
          Type[][] optionalParameterTypeCustomModifiers = new Type[parameterTypes.Length][];
          for (int index = 0; index < parameters.Length; ++index)
          {
            parameterTypes[index] = parameters[index].ParameterType;
            requiredParameterTypeCustomModifiers[index] = parameters[index].GetRequiredCustomModifiers();
            optionalParameterTypeCustomModifiers[index] = parameters[index].GetOptionalCustomModifiers();
          }
          int tr = getGenericTypeDefinition ? this.GetTypeToken(method.DeclaringType).Token : this.GetTypeTokenInternal(method.DeclaringType).Token;
          SignatureHelper methodSigHelper;
          try
          {
            methodSigHelper = SignatureHelper.GetMethodSigHelper((Module) this, method.CallingConvention, method.ReturnType, method.ReturnParameter.GetRequiredCustomModifiers(), method.ReturnParameter.GetOptionalCustomModifiers(), parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
          }
          catch (NotImplementedException ex)
          {
            methodSigHelper = SignatureHelper.GetMethodSigHelper((Module) this, method.ReturnType, parameterTypes);
          }
          int length;
          byte[] signature = methodSigHelper.InternalGetSignature(out length);
          str = this.GetMemberRefFromSignature(tr, method.Name, signature, length);
        }
      }
      return new MethodToken(str);
    }

    /// <summary>返回在此模块用于标识具有指定的特性和参数类型的构造函数的标记。</summary>
    /// <returns>用于标识此模块内的指定构造函数的标记。</returns>
    /// <param name="constructor">要为其获取标记的构造函数。</param>
    /// <param name="optionalParameterTypes">构造函数的可选参数类型的集合。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="constructor" /> 为 null。</exception>
    [SecuritySafeCritical]
    public MethodToken GetConstructorToken(ConstructorInfo constructor, IEnumerable<Type> optionalParameterTypes)
    {
      if (constructor == (ConstructorInfo) null)
        throw new ArgumentNullException("constructor");
      lock (this.SyncRoot)
        return new MethodToken(this.GetMethodTokenInternal((MethodBase) constructor, optionalParameterTypes, false));
    }

    /// <summary>返回在此模块用于标识具有指定的特性和参数类型的方法的标记。</summary>
    /// <returns>用于标识此模块内的指定方法的标记。</returns>
    /// <param name="method">要为其获取标记的方法。</param>
    /// <param name="optionalParameterTypes">方法的可选参数类型的集合。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="method" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">此方法的声明类型不在此模块内。</exception>
    [SecuritySafeCritical]
    public MethodToken GetMethodToken(MethodInfo method, IEnumerable<Type> optionalParameterTypes)
    {
      if (method == (MethodInfo) null)
        throw new ArgumentNullException("method");
      lock (this.SyncRoot)
        return new MethodToken(this.GetMethodTokenInternal((MethodBase) method, optionalParameterTypes, true));
    }

    [SecurityCritical]
    internal int GetMethodTokenInternal(MethodBase method, IEnumerable<Type> optionalParameterTypes, bool useMethodDef)
    {
      MethodInfo method1 = method as MethodInfo;
      int num1;
      if (method.IsGenericMethod)
      {
        MethodInfo method2 = method1;
        int num2 = method1.IsGenericMethodDefinition ? 1 : 0;
        if (num2 == 0)
          method2 = method1.GetGenericMethodDefinition();
        int tkParent = !this.Equals((object) method2.Module) || method2.DeclaringType != (Type) null && method2.DeclaringType.IsGenericType ? this.GetMemberRefToken((MethodBase) method2, (IEnumerable<Type>) null) : this.GetMethodTokenInternal(method2).Token;
        int num3 = useMethodDef ? 1 : 0;
        if ((num2 & num3) != 0)
          return tkParent;
        int length;
        byte[] signature = SignatureHelper.GetMethodSpecSigHelper((Module) this, method1.GetGenericArguments()).InternalGetSignature(out length);
        num1 = TypeBuilder.DefineMethodSpec(this.GetNativeHandle(), tkParent, signature, length);
      }
      else
        num1 = (method.CallingConvention & CallingConventions.VarArgs) != (CallingConventions) 0 || !(method.DeclaringType == (Type) null) && method.DeclaringType.IsGenericType ? this.GetMemberRefToken(method, optionalParameterTypes) : (!(method1 != (MethodInfo) null) ? this.GetConstructorToken(method as ConstructorInfo).Token : this.GetMethodTokenInternal(method1).Token);
      return num1;
    }

    /// <summary>返回数组类上的命名方法的标记。</summary>
    /// <returns>数组类上的命名方法的标记。</returns>
    /// <param name="arrayClass">数组的对象。</param>
    /// <param name="methodName">包含方法名称的字符串。</param>
    /// <param name="callingConvention">该方法的调用约定。</param>
    /// <param name="returnType">该方法的返回类型。</param>
    /// <param name="parameterTypes">该方法的参数的类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="arrayClass" /> 不是数组。- 或 -<paramref name="methodName" /> 的长度为零。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="arrayClass" /> 或 <paramref name="methodName" /> 为 null。</exception>
    [SecuritySafeCritical]
    public MethodToken GetArrayMethodToken(Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
    {
      lock (this.SyncRoot)
        return this.GetArrayMethodTokenNoLock(arrayClass, methodName, callingConvention, returnType, parameterTypes);
    }

    [SecurityCritical]
    private MethodToken GetArrayMethodTokenNoLock(Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
    {
      if (arrayClass == (Type) null)
        throw new ArgumentNullException("arrayClass");
      if (methodName == null)
        throw new ArgumentNullException("methodName");
      if (methodName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "methodName");
      if (!arrayClass.IsArray)
        throw new ArgumentException(Environment.GetResourceString("Argument_HasToBeArrayClass"));
      this.CheckContext(returnType, arrayClass);
      this.CheckContext(parameterTypes);
      int length;
      byte[] signature = SignatureHelper.GetMethodSigHelper((Module) this, callingConvention, returnType, (Type[]) null, (Type[]) null, parameterTypes, (Type[][]) null, (Type[][]) null).InternalGetSignature(out length);
      return new MethodToken(ModuleBuilder.GetArrayMethodToken(this.GetNativeHandle(), this.GetTypeTokenInternal(arrayClass).Token, methodName, signature, length));
    }

    /// <summary>返回数组类上的命名方法。</summary>
    /// <returns>数组类上的命名方法。</returns>
    /// <param name="arrayClass">数组类。</param>
    /// <param name="methodName">数组类上的方法的名称。</param>
    /// <param name="callingConvention">该方法的调用约定。</param>
    /// <param name="returnType">该方法的返回类型。</param>
    /// <param name="parameterTypes">方法参数的类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="arrayClass" /> 不是数组。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="arrayClass" /> 或 <paramref name="methodName" /> 为 null。</exception>
    [SecuritySafeCritical]
    public MethodInfo GetArrayMethod(Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
    {
      this.CheckContext(returnType, arrayClass);
      this.CheckContext(parameterTypes);
      return (MethodInfo) new SymbolMethod(this, this.GetArrayMethodToken(arrayClass, methodName, callingConvention, returnType, parameterTypes), arrayClass, methodName, callingConvention, returnType, parameterTypes);
    }

    /// <summary>返回用于标识此模块内的指定构造函数的标记。</summary>
    /// <returns>用于标识此模块内的指定构造函数的标记。</returns>
    /// <param name="con">要为其获取标记的构造函数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="con" /> 为 null。</exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public MethodToken GetConstructorToken(ConstructorInfo con)
    {
      return this.InternalGetConstructorToken(con, false);
    }

    /// <summary>返回用于标识此模块内的指定字段的标记。</summary>
    /// <returns>用于标识此模块内的指定字段的标记。</returns>
    /// <param name="field">要为其获取标记的字段。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="field" /> 为 null。</exception>
    [SecuritySafeCritical]
    public FieldToken GetFieldToken(FieldInfo field)
    {
      lock (this.SyncRoot)
        return this.GetFieldTokenNoLock(field);
    }

    [SecurityCritical]
    private FieldToken GetFieldTokenNoLock(FieldInfo field)
    {
      if (field == (FieldInfo) null)
        throw new ArgumentNullException("con");
      FieldBuilder fieldBuilder;
      int field1;
      if ((FieldInfo) (fieldBuilder = field as FieldBuilder) != (FieldInfo) null)
      {
        if (field.DeclaringType != (Type) null && field.DeclaringType.IsGenericType)
        {
          int length;
          field1 = this.GetMemberRef((Module) this, this.GetTokenFromTypeSpec(SignatureHelper.GetTypeSigToken((Module) this, field.DeclaringType).InternalGetSignature(out length), length), fieldBuilder.GetToken().Token);
        }
        else
        {
          if (fieldBuilder.Module.Equals((object) this))
            return fieldBuilder.GetToken();
          if (field.DeclaringType == (Type) null)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotImportGlobalFromDifferentModule"));
          int token = this.GetTypeTokenInternal(field.DeclaringType).Token;
          field1 = this.GetMemberRef(field.ReflectedType.Module, token, fieldBuilder.GetToken().Token);
        }
      }
      else
      {
        RuntimeFieldInfo runtimeField;
        if ((FieldInfo) (runtimeField = field as RuntimeFieldInfo) != (FieldInfo) null)
        {
          if (field.DeclaringType == (Type) null)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotImportGlobalFromDifferentModule"));
          int length;
          field1 = !(field.DeclaringType != (Type) null) || !field.DeclaringType.IsGenericType ? this.GetMemberRefOfFieldInfo(this.GetTypeTokenInternal(field.DeclaringType).Token, field.DeclaringType.GetTypeHandleInternal(), runtimeField) : this.GetMemberRefOfFieldInfo(this.GetTokenFromTypeSpec(SignatureHelper.GetTypeSigToken((Module) this, field.DeclaringType).InternalGetSignature(out length), length), field.DeclaringType.GetTypeHandleInternal(), runtimeField);
        }
        else
        {
          FieldOnTypeBuilderInstantiation builderInstantiation;
          if ((FieldInfo) (builderInstantiation = field as FieldOnTypeBuilderInstantiation) != (FieldInfo) null)
          {
            int length;
            field1 = this.GetMemberRef(builderInstantiation.FieldInfo.ReflectedType.Module, this.GetTokenFromTypeSpec(SignatureHelper.GetTypeSigToken((Module) this, field.DeclaringType).InternalGetSignature(out length), length), builderInstantiation.MetadataTokenInternal);
          }
          else
          {
            int token = this.GetTypeTokenInternal(field.ReflectedType).Token;
            SignatureHelper fieldSigHelper = SignatureHelper.GetFieldSigHelper((Module) this);
            Type fieldType = field.FieldType;
            Type[] requiredCustomModifiers = field.GetRequiredCustomModifiers();
            Type[] optionalCustomModifiers = field.GetOptionalCustomModifiers();
            fieldSigHelper.AddArgument(fieldType, requiredCustomModifiers, optionalCustomModifiers);
            int length1;
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            int& length2 = @length1;
            byte[] signature = fieldSigHelper.InternalGetSignature(length2);
            field1 = this.GetMemberRefFromSignature(token, field.Name, signature, length1);
          }
        }
      }
      return new FieldToken(field1, field.GetType());
    }

    /// <summary>返回模块常量池中给定字符串的标记。</summary>
    /// <returns>常量池中字符串的标记。</returns>
    /// <param name="str">要添加到模块常数池中的字符串。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="str" /> 为 null。</exception>
    [SecuritySafeCritical]
    public StringToken GetStringConstant(string str)
    {
      if (str == null)
        throw new ArgumentNullException("str");
      RuntimeModule nativeHandle = this.GetNativeHandle();
      string str1 = str;
      int length = str1.Length;
      return new StringToken(ModuleBuilder.GetStringConstant(nativeHandle, str1, length));
    }

    /// <summary>为由指定的 <see cref="T:System.Reflection.Emit.SignatureHelper" /> 定义的签名定义标记。</summary>
    /// <returns>已定义签名的标记。</returns>
    /// <param name="sigHelper">签名。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sigHelper" /> 为 null。</exception>
    [SecuritySafeCritical]
    public SignatureToken GetSignatureToken(SignatureHelper sigHelper)
    {
      if (sigHelper == null)
        throw new ArgumentNullException("sigHelper");
      int length;
      return new SignatureToken(TypeBuilder.GetTokenFromSig(this.GetNativeHandle(), sigHelper.InternalGetSignature(out length), length), this);
    }

    /// <summary>为具有指定字符数组和签名长度的签名定义标记。</summary>
    /// <returns>指定签名的标记。</returns>
    /// <param name="sigBytes">签名二进制大对象 (BLOB)。</param>
    /// <param name="sigLength">签名 BLOB 的长度。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sigBytes" /> 为 null。</exception>
    [SecuritySafeCritical]
    public SignatureToken GetSignatureToken(byte[] sigBytes, int sigLength)
    {
      if (sigBytes == null)
        throw new ArgumentNullException("sigBytes");
      byte[] signature = new byte[sigBytes.Length];
      Array.Copy((Array) sigBytes, (Array) signature, sigBytes.Length);
      return new SignatureToken(TypeBuilder.GetTokenFromSig(this.GetNativeHandle(), signature, sigLength), this);
    }

    /// <summary>使用表示自定义属性的指定二进制大对象 (BLOB) 向此模块应用该属性。</summary>
    /// <param name="con">自定义属性的构造函数。</param>
    /// <param name="binaryAttribute">表示属性的字节 BLOB。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="con" /> 或 <paramref name="binaryAttribute" /> 为 null。</exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
    {
      if (con == (ConstructorInfo) null)
        throw new ArgumentNullException("con");
      if (binaryAttribute == null)
        throw new ArgumentNullException("binaryAttribute");
      TypeBuilder.DefineCustomAttribute(this, 1, this.GetConstructorToken(con).Token, binaryAttribute, false, false);
    }

    /// <summary>使用自定义属性生成器向此模块应用自定义属性。</summary>
    /// <param name="customBuilder">帮助器类的实例，指定要应用的自定义属性。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="customBuilder" /> 为 null。</exception>
    [SecuritySafeCritical]
    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
      if (customBuilder == null)
        throw new ArgumentNullException("customBuilder");
      customBuilder.CreateCustomAttribute(this, 1);
    }

    /// <summary>返回与此动态模块关联的符号编写器。</summary>
    /// <returns>与此动态模块关联的符号编写器。</returns>
    public ISymbolWriter GetSymWriter()
    {
      return this.m_iSymWriter;
    }

    /// <summary>定义源的文档。</summary>
    /// <returns>已定义的文档。</returns>
    /// <param name="url">文档的 URL。</param>
    /// <param name="language">标识文档语言的 GUID。它可以是 <see cref="F:System.Guid.Empty" />。</param>
    /// <param name="languageVendor">标识文档语言供应商的 GUID。它可以是 <see cref="F:System.Guid.Empty" />。</param>
    /// <param name="documentType">标识文档类型的 GUID。它可以是 <see cref="F:System.Guid.Empty" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="url" /> 为 null。这是对早期版本的 .NET Framework 的更改。</exception>
    /// <exception cref="T:System.InvalidOperationException">对不是调试模块的动态模块调用此方法。</exception>
    public ISymbolDocumentWriter DefineDocument(string url, Guid language, Guid languageVendor, Guid documentType)
    {
      if (url == null)
        throw new ArgumentNullException("url");
      lock (this.SyncRoot)
        return this.DefineDocumentNoLock(url, language, languageVendor, documentType);
    }

    private ISymbolDocumentWriter DefineDocumentNoLock(string url, Guid language, Guid languageVendor, Guid documentType)
    {
      if (this.m_iSymWriter == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotADebugModule"));
      return this.m_iSymWriter.DefineDocument(url, language, languageVendor, documentType);
    }

    /// <summary>设置用户入口点。</summary>
    /// <param name="entryPoint">用户入口点。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="entryPoint" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">对不是调试模块的动态模块调用此方法。- 或 -此动态模块中不包含 <paramref name="entryPoint" />。</exception>
    [SecuritySafeCritical]
    public void SetUserEntryPoint(MethodInfo entryPoint)
    {
      lock (this.SyncRoot)
        this.SetUserEntryPointNoLock(entryPoint);
    }

    [SecurityCritical]
    private void SetUserEntryPointNoLock(MethodInfo entryPoint)
    {
      if (entryPoint == (MethodInfo) null)
        throw new ArgumentNullException("entryPoint");
      if (this.m_iSymWriter == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotADebugModule"));
      if (entryPoint.DeclaringType != (Type) null)
      {
        if (!entryPoint.Module.Equals((object) this))
          throw new InvalidOperationException(Environment.GetResourceString("Argument_NotInTheSameModuleBuilder"));
      }
      else
      {
        MethodBuilder methodBuilder = entryPoint as MethodBuilder;
        if ((MethodInfo) methodBuilder != (MethodInfo) null && (Module) methodBuilder.GetModuleBuilder() != (Module) this)
          throw new InvalidOperationException(Environment.GetResourceString("Argument_NotInTheSameModuleBuilder"));
      }
      this.m_iSymWriter.SetUserEntryPoint(new SymbolToken(this.GetMethodTokenInternal(entryPoint).Token));
    }

    /// <summary>此方法不执行任何操作。</summary>
    /// <param name="name">自定义特性的名称。</param>
    /// <param name="data">不透明的字节二进制大对象 (BLOB)，表示自定义特性的值。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="url" /> 为 null。</exception>
    public void SetSymCustomAttribute(string name, byte[] data)
    {
      lock (this.SyncRoot)
        this.SetSymCustomAttributeNoLock(name, data);
    }

    private void SetSymCustomAttributeNoLock(string name, byte[] data)
    {
      if (this.m_iSymWriter == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotADebugModule"));
    }

    /// <summary>返回一个值，该值指示此动态模块是否为瞬态的。</summary>
    /// <returns>如果此动态模块是瞬态的，则为 true；否则为 false。</returns>
    public bool IsTransient()
    {
      return this.InternalModule.IsTransientInternal();
    }

    void _ModuleBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _ModuleBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _ModuleBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _ModuleBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
