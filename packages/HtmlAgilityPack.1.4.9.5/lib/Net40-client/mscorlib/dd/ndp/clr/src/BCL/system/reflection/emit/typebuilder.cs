// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.TypeBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
  /// <summary>在运行时定义并创建类的新实例。</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_TypeBuilder))]
  [ComVisible(true)]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public sealed class TypeBuilder : TypeInfo, _TypeBuilder
  {
    /// <summary>表示不指定此类型的总大小。</summary>
    public const int UnspecifiedTypeSize = 0;
    private List<TypeBuilder.CustAttr> m_ca;
    private TypeToken m_tdType;
    private ModuleBuilder m_module;
    private string m_strName;
    private string m_strNameSpace;
    private string m_strFullQualName;
    private Type m_typeParent;
    private List<Type> m_typeInterfaces;
    private TypeAttributes m_iAttr;
    private GenericParameterAttributes m_genParamAttributes;
    internal List<MethodBuilder> m_listMethods;
    internal int m_lastTokenizedMethod;
    private int m_constructorCount;
    private int m_iTypeSize;
    private PackingSize m_iPackingSize;
    private TypeBuilder m_DeclaringType;
    private Type m_enumUnderlyingType;
    internal bool m_isHiddenGlobalType;
    private bool m_hasBeenCreated;
    private RuntimeType m_bakedRuntimeType;
    private int m_genParamPos;
    private GenericTypeParameterBuilder[] m_inst;
    private bool m_bIsGenParam;
    private MethodBuilder m_declMeth;
    private TypeBuilder m_genTypeDef;

    internal object SyncRoot
    {
      get
      {
        return this.m_module.SyncRoot;
      }
    }

    internal RuntimeType BakedRuntimeType
    {
      get
      {
        return this.m_bakedRuntimeType;
      }
    }

    /// <summary>返回声明此类型的类型。</summary>
    /// <returns>只读。声明此类型的类型。</returns>
    public override Type DeclaringType
    {
      get
      {
        return (Type) this.m_DeclaringType;
      }
    }

    /// <summary>返回用于获取此类型的类型。</summary>
    /// <returns>只读。用于获取此类型的类型。</returns>
    public override Type ReflectedType
    {
      get
      {
        return (Type) this.m_DeclaringType;
      }
    }

    /// <summary>检索此类型的名称。</summary>
    /// <returns>只读。检索此类型的 <see cref="T:System.String" /> 名称。</returns>
    public override string Name
    {
      get
      {
        return this.m_strName;
      }
    }

    /// <summary>检索包含此类型定义的动态模块。</summary>
    /// <returns>只读。检索包含此类型定义的动态模块。</returns>
    public override Module Module
    {
      get
      {
        return (Module) this.GetModuleBuilder();
      }
    }

    internal int MetadataTokenInternal
    {
      get
      {
        return this.m_tdType.Token;
      }
    }

    /// <summary>检索此类型的 GUID。</summary>
    /// <returns>只读。检索此类型的 GUID</returns>
    /// <exception cref="T:System.NotSupportedException">对于不完整类型，目前不支持此方法。</exception>
    public override Guid GUID
    {
      get
      {
        if (!this.IsCreated())
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
        return this.m_bakedRuntimeType.GUID;
      }
    }

    /// <summary>检索包含此类型定义的动态程序集。</summary>
    /// <returns>只读。检索包含此类型定义的动态程序集。</returns>
    public override Assembly Assembly
    {
      get
      {
        return this.m_module.Assembly;
      }
    }

    /// <summary>在动态模块中不支持。</summary>
    /// <returns>只读。</returns>
    /// <exception cref="T:System.NotSupportedException">在动态模块中不支持。</exception>
    public override RuntimeTypeHandle TypeHandle
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
      }
    }

    /// <summary>检索此类型的完整路径。</summary>
    /// <returns>只读。检索此类型的完整路径。</returns>
    public override string FullName
    {
      get
      {
        if (this.m_strFullQualName == null)
          this.m_strFullQualName = TypeNameBuilder.ToString((Type) this, TypeNameBuilder.Format.FullName);
        return this.m_strFullQualName;
      }
    }

    /// <summary>检索定义此 TypeBuilder 的命名空间。</summary>
    /// <returns>只读。检索定义此 TypeBuilder 的命名空间。</returns>
    public override string Namespace
    {
      get
      {
        return this.m_strNameSpace;
      }
    }

    /// <summary>返回由程序集的显示名称限定的此类型的完整名称。</summary>
    /// <returns>只读。由程序集的显示名称限定的此类型的完整名称。</returns>
    public override string AssemblyQualifiedName
    {
      get
      {
        return TypeNameBuilder.ToString((Type) this, TypeNameBuilder.Format.AssemblyQualifiedName);
      }
    }

    /// <summary>检索此类型的基类型。</summary>
    /// <returns>只读。检索此类型的基类型。</returns>
    public override Type BaseType
    {
      get
      {
        return this.m_typeParent;
      }
    }

    /// <summary>获取一个值，该值指示当前类型是安全关键的还是安全可靠关键的，因此可以执行关键操作。</summary>
    /// <returns>如果当前类型是安全关键的或安全可靠关键的，则为 true；如果它是透明的，则为 false。</returns>
    /// <exception cref="T:System.NotSupportedException">当前动态类型尚未通过调用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 方法来创建。</exception>
    public override bool IsSecurityCritical
    {
      get
      {
        if (!this.IsCreated())
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
        return this.m_bakedRuntimeType.IsSecurityCritical;
      }
    }

    /// <summary>获取一个值，该值指示当前类型是否是安全可靠关键的；即它是否可以执行关键操作并可以由透明代码访问。</summary>
    /// <returns>如果当前类型是安全可靠关键的，则为 true；如果它是安全关键的或透明的，则为 false。</returns>
    /// <exception cref="T:System.NotSupportedException">当前动态类型尚未通过调用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 方法来创建。</exception>
    public override bool IsSecuritySafeCritical
    {
      get
      {
        if (!this.IsCreated())
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
        return this.m_bakedRuntimeType.IsSecuritySafeCritical;
      }
    }

    /// <summary>获取一个值，该值指示当前类型是否是透明的，因此无法执行关键操作。</summary>
    /// <returns>如果类型是安全透明的，则为 true；否则为 false。</returns>
    /// <exception cref="T:System.NotSupportedException">当前动态类型尚未通过调用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 方法来创建。</exception>
    public override bool IsSecurityTransparent
    {
      get
      {
        if (!this.IsCreated())
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
        return this.m_bakedRuntimeType.IsSecurityTransparent;
      }
    }

    /// <summary>返回此 TypeBuilder 的基础系统类型。</summary>
    /// <returns>只读。返回基础系统类型。</returns>
    /// <exception cref="T:System.InvalidOperationException">此类型是枚举，但是没有基础系统类型。</exception>
    public override Type UnderlyingSystemType
    {
      get
      {
        if (this.m_bakedRuntimeType != (RuntimeType) null)
          return (Type) this.m_bakedRuntimeType;
        if (!this.IsEnum)
          return (Type) this;
        if (this.m_enumUnderlyingType == (Type) null)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NoUnderlyingTypeOnEnum"));
        return this.m_enumUnderlyingType;
      }
    }

    /// <summary>获取一个值，该值指示当前泛型类型参数的协方差和特殊约束。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.GenericParameterAttributes" /> 值的按位组合，用于描述当前泛型类型参数的协变和特殊约束。</returns>
    public override GenericParameterAttributes GenericParameterAttributes
    {
      get
      {
        return this.m_genParamAttributes;
      }
    }

    /// <summary>获取一个值，该值指示当前 <see cref="T:System.Reflection.Emit.TypeBuilder" /> 是否表示一个泛型类型定义，可以根据该定义构造其他的泛型类型。</summary>
    /// <returns>如果此 <see cref="T:System.Reflection.Emit.TypeBuilder" /> 对象表示泛型类型定义，则为 true；否则为 false。</returns>
    public override bool IsGenericTypeDefinition
    {
      get
      {
        return this.IsGenericType;
      }
    }

    /// <summary>获取一个值，该值指示当前类型是否是泛型类型。</summary>
    /// <returns>如果当前的 <see cref="T:System.Reflection.Emit.TypeBuilder" /> 对象表示的类型为泛型，则为true；否则为 false。</returns>
    public override bool IsGenericType
    {
      get
      {
        return this.m_inst != null;
      }
    }

    /// <summary>获取一个值，该值指示当前类型是否为泛型类型参数。</summary>
    /// <returns>如果当前 <see cref="T:System.Reflection.Emit.TypeBuilder" /> 对象表示泛型类型参数，则为 true；否则为 false。</returns>
    public override bool IsGenericParameter
    {
      get
      {
        return this.m_bIsGenParam;
      }
    }

    /// <summary>获取指示此对象是否表示构造的泛型类型的值。</summary>
    /// <returns>如果此对象表示构造泛型类型，则为 true；否则为 false。</returns>
    public override bool IsConstructedGenericType
    {
      get
      {
        return false;
      }
    }

    /// <summary>获取某个类型参数在类型参数列表中的位置，该列表具有声明该参数的泛型类型。</summary>
    /// <returns>如果当前的 <see cref="T:System.Reflection.Emit.TypeBuilder" /> 对象表示某个泛型类型参数，则为该类型参数在类型参数列表中的位置，该列表具有声明该参数的泛型类型；否则为未定义。</returns>
    public override int GenericParameterPosition
    {
      get
      {
        return this.m_genParamPos;
      }
    }

    /// <summary>获取当前泛型类型参数的声明方法。</summary>
    /// <returns>如果当前类型是泛型类型参数，则为 <see cref="T:System.Reflection.MethodBase" />，表示当前类型的声明方法；否则为 null。</returns>
    public override MethodBase DeclaringMethod
    {
      get
      {
        return (MethodBase) this.m_declMeth;
      }
    }

    /// <summary>检索此类型的总大小。</summary>
    /// <returns>只读。检索此类型的总大小。</returns>
    public int Size
    {
      get
      {
        return this.m_iTypeSize;
      }
    }

    /// <summary>检索此类型的封装大小。</summary>
    /// <returns>只读。检索此类型的封装大小。</returns>
    public PackingSize PackingSize
    {
      get
      {
        return this.m_iPackingSize;
      }
    }

    /// <summary>返回此类型的类型标记。</summary>
    /// <returns>只读。返回此类型的 TypeToken。</returns>
    /// <exception cref="T:System.InvalidOperationException">该类型是以前用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建的。</exception>
    public TypeToken TypeToken
    {
      get
      {
        if (this.IsGenericParameter)
          this.ThrowIfCreated();
        return this.m_tdType;
      }
    }

    internal TypeBuilder(ModuleBuilder module)
    {
      this.m_tdType = new TypeToken(33554432);
      this.m_isHiddenGlobalType = true;
      this.m_module = module;
      this.m_listMethods = new List<MethodBuilder>();
      this.m_lastTokenizedMethod = -1;
    }

    internal TypeBuilder(string szName, int genParamPos, MethodBuilder declMeth)
    {
      this.m_declMeth = declMeth;
      this.m_DeclaringType = this.m_declMeth.GetTypeBuilder();
      this.m_module = declMeth.GetModuleBuilder();
      this.InitAsGenericParam(szName, genParamPos);
    }

    private TypeBuilder(string szName, int genParamPos, TypeBuilder declType)
    {
      this.m_DeclaringType = declType;
      this.m_module = declType.GetModuleBuilder();
      this.InitAsGenericParam(szName, genParamPos);
    }

    [SecurityCritical]
    internal TypeBuilder(string name, TypeAttributes attr, Type parent, Type[] interfaces, ModuleBuilder module, PackingSize iPackingSize, int iTypeSize, TypeBuilder enclosingType)
    {
      this.Init(name, attr, parent, interfaces, module, iPackingSize, iTypeSize, enclosingType);
    }

    /// <summary>获取一个值，该值指示指定的 <see cref="T:System.Reflection.TypeInfo" /> 对象是否可以分配给这个对象。</summary>
    /// <returns>如果 <paramref name="typeInfo" />可分配给此对象，则为 true；否则为 false。</returns>
    /// <param name="typeInfo">要测试的对象。</param>
    public override bool IsAssignableFrom(TypeInfo typeInfo)
    {
      if ((Type) typeInfo == (Type) null)
        return false;
      return this.IsAssignableFrom(typeInfo.AsType());
    }

    /// <summary>返回指定的构造泛型类型的方法，该方法对应于泛型类型定义的指定字段。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.MethodInfo" /> 对象表示 <paramref name="type" /> 的方法，该方法对应于 <paramref name="method" />，用于指定属于泛型类型定义 <paramref name="type" /> 的一个方法。</returns>
    /// <param name="type">返回其方法的构造泛型类型。</param>
    /// <param name="method">泛型类型定义 <paramref name="type" /> 中的一个方法，用于指定要返回 <paramref name="type" /> 的哪一个方法。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="method" /> 是非泛型方法定义的泛型方法。- 或 -<paramref name="type" /> 不表示泛型类型。- 或 -<paramref name="type" /> 并不属于类型 <see cref="T:System.Reflection.Emit.TypeBuilder" />。- 或 -<paramref name="method" /> 的声明类型不是泛型类型定义。- 或 -<paramref name="method" /> 的声明类型不是 <paramref name="type" /> 的泛型类型定义。</exception>
    public static MethodInfo GetMethod(Type type, MethodInfo method)
    {
      if (!(type is TypeBuilder) && !(type is TypeBuilderInstantiation))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeTypeBuilder"));
      if (method.IsGenericMethod && !method.IsGenericMethodDefinition)
        throw new ArgumentException(Environment.GetResourceString("Argument_NeedGenericMethodDefinition"), "method");
      if (method.DeclaringType == (Type) null || !method.DeclaringType.IsGenericTypeDefinition)
        throw new ArgumentException(Environment.GetResourceString("Argument_MethodNeedGenericDeclaringType"), "method");
      if (type.GetGenericTypeDefinition() != method.DeclaringType)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidMethodDeclaringType"), "type");
      if (type.IsGenericTypeDefinition)
      {
        Type type1 = type;
        Type[] genericArguments = type1.GetGenericArguments();
        type = type1.MakeGenericType(genericArguments);
      }
      if (!(type is TypeBuilderInstantiation))
        throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), "type");
      return MethodOnTypeBuilderInstantiation.GetMethod(method, type as TypeBuilderInstantiation);
    }

    /// <summary>返回指定的构造泛型类型的构造函数，该函数对应于泛型类型定义的指定构造函数。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.ConstructorInfo" /> 对象表示 <paramref name="type" /> 的构造函数，该函数对应于 <paramref name="constructor" />，用于指定属于泛型类型定义 <paramref name="type" /> 的一个构造函数。</returns>
    /// <param name="type">返回其构造函数的构造泛型类型。</param>
    /// <param name="constructor">泛型类型定义 <paramref name="type" /> 中的一个构造函数，用于指定要返回 <paramref name="type" /> 的哪一个构造函数。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="type" /> 不表示泛型类型。- 或 -<paramref name="type" /> 并不属于类型 <see cref="T:System.Reflection.Emit.TypeBuilder" />。- 或 -<paramref name="constructor" /> 的声明类型不是泛型类型定义。- 或 -<paramref name="constructor" /> 的声明类型不是 <paramref name="type" /> 的泛型类型定义。</exception>
    public static ConstructorInfo GetConstructor(Type type, ConstructorInfo constructor)
    {
      if (!(type is TypeBuilder) && !(type is TypeBuilderInstantiation))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeTypeBuilder"));
      if (!constructor.DeclaringType.IsGenericTypeDefinition)
        throw new ArgumentException(Environment.GetResourceString("Argument_ConstructorNeedGenericDeclaringType"), "constructor");
      if (!(type is TypeBuilderInstantiation))
        throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), "type");
      if (type is TypeBuilder && type.IsGenericTypeDefinition)
      {
        Type type1 = type;
        Type[] genericArguments = type1.GetGenericArguments();
        type = type1.MakeGenericType(genericArguments);
      }
      if (type.GetGenericTypeDefinition() != constructor.DeclaringType)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidConstructorDeclaringType"), "type");
      return ConstructorOnTypeBuilderInstantiation.GetConstructor(constructor, type as TypeBuilderInstantiation);
    }

    /// <summary>返回指定的构造泛型类型的字段，该字段对应于泛型类型定义的指定字段。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.FieldInfo" /> 对象表示 <paramref name="type" /> 的字段，该字段对应于 <paramref name="field" />，用于指定属于泛型类型定义 <paramref name="type" /> 的一个字段。</returns>
    /// <param name="type">返回其字段的构造泛型类型。</param>
    /// <param name="field">泛型类型定义 <paramref name="type" /> 中的一个字段，用于指定要返回 <paramref name="type" /> 的哪一个字段。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="type" /> 不表示泛型类型。- 或 -<paramref name="type" /> 并不属于类型 <see cref="T:System.Reflection.Emit.TypeBuilder" />。- 或 -<paramref name="field" /> 的声明类型不是泛型类型定义。- 或 -<paramref name="field" /> 的声明类型不是 <paramref name="type" /> 的泛型类型定义。</exception>
    public static FieldInfo GetField(Type type, FieldInfo field)
    {
      if (!(type is TypeBuilder) && !(type is TypeBuilderInstantiation))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeTypeBuilder"));
      if (!field.DeclaringType.IsGenericTypeDefinition)
        throw new ArgumentException(Environment.GetResourceString("Argument_FieldNeedGenericDeclaringType"), "field");
      if (!(type is TypeBuilderInstantiation))
        throw new ArgumentException(Environment.GetResourceString("Argument_NeedNonGenericType"), "type");
      if (type is TypeBuilder && type.IsGenericTypeDefinition)
      {
        Type type1 = type;
        Type[] genericArguments = type1.GetGenericArguments();
        type = type1.MakeGenericType(genericArguments);
      }
      if (type.GetGenericTypeDefinition() != field.DeclaringType)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFieldDeclaringType"), "type");
      return FieldOnTypeBuilderInstantiation.GetField(field, type as TypeBuilderInstantiation);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void SetParentType(RuntimeModule module, int tdTypeDef, int tkParent);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void AddInterfaceImpl(RuntimeModule module, int tdTypeDef, int tkInterface);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern int DefineMethod(RuntimeModule module, int tkParent, string name, byte[] signature, int sigLength, MethodAttributes attributes);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern int DefineMethodSpec(RuntimeModule module, int tkParent, byte[] signature, int sigLength);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern int DefineField(RuntimeModule module, int tkParent, string name, byte[] signature, int sigLength, FieldAttributes attributes);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void SetMethodIL(RuntimeModule module, int tk, bool isInitLocals, byte[] body, int bodyLength, byte[] LocalSig, int sigLength, int maxStackSize, ExceptionHandler[] exceptions, int numExceptions, int[] tokenFixups, int numTokenFixups);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void DefineCustomAttribute(RuntimeModule module, int tkAssociate, int tkConstructor, byte[] attr, int attrLength, bool toDisk, bool updateCompilerFlags);

    [SecurityCritical]
    internal static void DefineCustomAttribute(ModuleBuilder module, int tkAssociate, int tkConstructor, byte[] attr, bool toDisk, bool updateCompilerFlags)
    {
      byte[] numArray = (byte[]) null;
      if (attr != null)
      {
        numArray = new byte[attr.Length];
        Array.Copy((Array) attr, (Array) numArray, attr.Length);
      }
      RuntimeModule nativeHandle = module.GetNativeHandle();
      int tkAssociate1 = tkAssociate;
      int tkConstructor1 = tkConstructor;
      byte[] attr1 = numArray;
      int attrLength = attr1 != null ? numArray.Length : 0;
      int num1 = toDisk ? 1 : 0;
      int num2 = updateCompilerFlags ? 1 : 0;
      TypeBuilder.DefineCustomAttribute(nativeHandle, tkAssociate1, tkConstructor1, attr1, attrLength, num1 != 0, num2 != 0);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void SetPInvokeData(RuntimeModule module, string DllName, string name, int token, int linkFlags);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern int DefineProperty(RuntimeModule module, int tkParent, string name, PropertyAttributes attributes, byte[] signature, int sigLength);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern int DefineEvent(RuntimeModule module, int tkParent, string name, EventAttributes attributes, int tkEventType);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void DefineMethodSemantics(RuntimeModule module, int tkAssociation, MethodSemanticsAttributes semantics, int tkMethod);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void DefineMethodImpl(RuntimeModule module, int tkType, int tkBody, int tkDecl);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void SetMethodImpl(RuntimeModule module, int tkMethod, MethodImplAttributes MethodImplAttributes);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern int SetParamInfo(RuntimeModule module, int tkMethod, int iSequence, ParameterAttributes iParamAttributes, string strParamName);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern int GetTokenFromSig(RuntimeModule module, byte[] signature, int sigLength);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void SetFieldLayoutOffset(RuntimeModule module, int fdToken, int iOffset);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void SetClassLayout(RuntimeModule module, int tk, PackingSize iPackingSize, int iTypeSize);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void SetFieldMarshal(RuntimeModule module, int tk, byte[] ubMarshal, int ubSize);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern unsafe void SetConstantValue(RuntimeModule module, int tk, int corType, void* pValue);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void AddDeclarativeSecurity(RuntimeModule module, int parent, SecurityAction action, byte[] blob, int cb);

    private static bool IsPublicComType(Type type)
    {
      Type declaringType = type.DeclaringType;
      if (declaringType != (Type) null)
      {
        if (TypeBuilder.IsPublicComType(declaringType) && (type.Attributes & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPublic)
          return true;
      }
      else if ((type.Attributes & TypeAttributes.VisibilityMask) == TypeAttributes.Public)
        return true;
      return false;
    }

    internal static bool IsTypeEqual(Type t1, Type t2)
    {
      if (t1 == t2)
        return true;
      TypeBuilder typeBuilder1 = (TypeBuilder) null;
      TypeBuilder typeBuilder2 = (TypeBuilder) null;
      Type type1;
      if (t1 is TypeBuilder)
      {
        typeBuilder1 = (TypeBuilder) t1;
        type1 = (Type) typeBuilder1.m_bakedRuntimeType;
      }
      else
        type1 = t1;
      Type type2;
      if (t2 is TypeBuilder)
      {
        typeBuilder2 = (TypeBuilder) t2;
        type2 = (Type) typeBuilder2.m_bakedRuntimeType;
      }
      else
        type2 = t2;
      return (Type) typeBuilder1 != (Type) null && (Type) typeBuilder2 != (Type) null && typeBuilder1 == typeBuilder2 || type1 != (Type) null && type2 != (Type) null && type1 == type2;
    }

    [SecurityCritical]
    internal static unsafe void SetConstantValue(ModuleBuilder module, int tk, Type destType, object value)
    {
      if (value != null)
      {
        Type c = value.GetType();
        if (destType.IsByRef)
          destType = destType.GetElementType();
        if (destType.IsEnum)
        {
          EnumBuilder enumBuilder;
          Type type;
          if ((Type) (enumBuilder = destType as EnumBuilder) != (Type) null)
          {
            type = enumBuilder.GetEnumUnderlyingType();
            if (c != (Type) enumBuilder.m_typeBuilder.m_bakedRuntimeType && c != type)
              throw new ArgumentException(Environment.GetResourceString("Argument_ConstantDoesntMatch"));
          }
          else
          {
            TypeBuilder typeBuilder;
            if ((Type) (typeBuilder = destType as TypeBuilder) != (Type) null)
            {
              type = typeBuilder.m_enumUnderlyingType;
              if (type == (Type) null || c != typeBuilder.UnderlyingSystemType && c != type)
                throw new ArgumentException(Environment.GetResourceString("Argument_ConstantDoesntMatch"));
            }
            else
            {
              type = Enum.GetUnderlyingType(destType);
              if (c != destType && c != type)
                throw new ArgumentException(Environment.GetResourceString("Argument_ConstantDoesntMatch"));
            }
          }
          c = type;
        }
        else if (!destType.IsAssignableFrom(c))
          throw new ArgumentException(Environment.GetResourceString("Argument_ConstantDoesntMatch"));
        CorElementType corElementType = RuntimeTypeHandle.GetCorElementType((RuntimeType) c);
        switch (corElementType)
        {
          case CorElementType.Boolean:
          case CorElementType.Char:
          case CorElementType.I1:
          case CorElementType.U1:
          case CorElementType.I2:
          case CorElementType.U2:
          case CorElementType.I4:
          case CorElementType.U4:
          case CorElementType.I8:
          case CorElementType.U8:
          case CorElementType.R4:
          case CorElementType.R8:
            fixed (byte* numPtr = &JitHelpers.GetPinningHelper(value).m_data)
              TypeBuilder.SetConstantValue(module.GetNativeHandle(), tk, (int) corElementType, (void*) numPtr);
            break;
          default:
            if (c == typeof (string))
            {
              string str = (string) value;
              char* chPtr = (char*) str;
              if ((IntPtr) chPtr != IntPtr.Zero)
                chPtr += RuntimeHelpers.OffsetToStringData;
              TypeBuilder.SetConstantValue(module.GetNativeHandle(), tk, 14, (void*) chPtr);
              str = (string) null;
              break;
            }
            if (c == typeof (DateTime))
            {
              long ticks = ((DateTime) value).Ticks;
              TypeBuilder.SetConstantValue(module.GetNativeHandle(), tk, 10, (void*) &ticks);
              break;
            }
            throw new ArgumentException(Environment.GetResourceString("Argument_ConstantNotSupported", (object) c.ToString()));
        }
      }
      else
      {
        if (destType.IsValueType && (!destType.IsGenericType || !(destType.GetGenericTypeDefinition() == typeof (Nullable<>))))
          throw new ArgumentException(Environment.GetResourceString("Argument_ConstantNull"));
        TypeBuilder.SetConstantValue(module.GetNativeHandle(), tk, 18, (void*) null);
      }
    }

    private void InitAsGenericParam(string szName, int genParamPos)
    {
      this.m_strName = szName;
      this.m_genParamPos = genParamPos;
      this.m_bIsGenParam = true;
      this.m_typeInterfaces = new List<Type>();
    }

    [SecurityCritical]
    private void Init(string fullname, TypeAttributes attr, Type parent, Type[] interfaces, ModuleBuilder module, PackingSize iPackingSize, int iTypeSize, TypeBuilder enclosingType)
    {
      if (fullname == null)
        throw new ArgumentNullException("fullname");
      if (fullname.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "fullname");
      if ((int) fullname[0] == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_IllegalName"), "fullname");
      if (fullname.Length > 1023)
        throw new ArgumentException(Environment.GetResourceString("Argument_TypeNameTooLong"), "fullname");
      this.m_module = module;
      this.m_DeclaringType = enclosingType;
      AssemblyBuilder containingAssemblyBuilder = this.m_module.ContainingAssemblyBuilder;
      containingAssemblyBuilder.m_assemblyData.CheckTypeNameConflict(fullname, enclosingType);
      if ((Type) enclosingType != (Type) null && ((attr & TypeAttributes.VisibilityMask) == TypeAttributes.Public || (attr & TypeAttributes.VisibilityMask) == TypeAttributes.NotPublic))
        throw new ArgumentException(Environment.GetResourceString("Argument_BadNestedTypeFlags"), "attr");
      int[] interfaceTokens = (int[]) null;
      if (interfaces != null)
      {
        for (int index = 0; index < interfaces.Length; ++index)
        {
          if (interfaces[index] == (Type) null)
            throw new ArgumentNullException("interfaces");
        }
        interfaceTokens = new int[interfaces.Length + 1];
        for (int index = 0; index < interfaces.Length; ++index)
          interfaceTokens[index] = this.m_module.GetTypeTokenInternal(interfaces[index]).Token;
      }
      int length = fullname.LastIndexOf('.');
      switch (length)
      {
        case -1:
        case 0:
          this.m_strNameSpace = string.Empty;
          this.m_strName = fullname;
          break;
        default:
          this.m_strNameSpace = fullname.Substring(0, length);
          this.m_strName = fullname.Substring(length + 1);
          break;
      }
      this.VerifyTypeAttributes(attr);
      this.m_iAttr = attr;
      this.SetParent(parent);
      this.m_listMethods = new List<MethodBuilder>();
      this.m_lastTokenizedMethod = -1;
      this.SetInterfaces(interfaces);
      int tkParent = 0;
      if (this.m_typeParent != (Type) null)
        tkParent = this.m_module.GetTypeTokenInternal(this.m_typeParent).Token;
      int tkEnclosingType = 0;
      if ((Type) enclosingType != (Type) null)
        tkEnclosingType = enclosingType.m_tdType.Token;
      this.m_tdType = new TypeToken(TypeBuilder.DefineType(this.m_module.GetNativeHandle(), fullname, tkParent, this.m_iAttr, tkEnclosingType, interfaceTokens));
      this.m_iPackingSize = iPackingSize;
      this.m_iTypeSize = iTypeSize;
      if (this.m_iPackingSize != PackingSize.Unspecified || this.m_iTypeSize != 0)
        TypeBuilder.SetClassLayout(this.GetModuleBuilder().GetNativeHandle(), this.m_tdType.Token, this.m_iPackingSize, this.m_iTypeSize);
      if (TypeBuilder.IsPublicComType((Type) this))
      {
        if (containingAssemblyBuilder.IsPersistable() && !this.m_module.IsTransient())
          containingAssemblyBuilder.m_assemblyData.AddPublicComType((Type) this);
        if (!this.m_module.Equals((object) containingAssemblyBuilder.ManifestModule))
          containingAssemblyBuilder.DefineExportedTypeInMemory((Type) this, this.m_module.m_moduleData.FileToken, this.m_tdType.Token);
      }
      this.m_module.AddType(this.FullName, (Type) this);
    }

    [SecurityCritical]
    private MethodBuilder DefinePInvokeMethodHelper(string name, string dllName, string importName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, CallingConvention nativeCallConv, CharSet nativeCharSet)
    {
      this.CheckContext(new Type[1]{ returnType });
      this.CheckContext(returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes);
      this.CheckContext(parameterTypeRequiredCustomModifiers);
      this.CheckContext(parameterTypeOptionalCustomModifiers);
      AppDomain.CheckDefinePInvokeSupported();
      lock (this.SyncRoot)
        return this.DefinePInvokeMethodHelperNoLock(name, dllName, importName, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, nativeCallConv, nativeCharSet);
    }

    [SecurityCritical]
    private MethodBuilder DefinePInvokeMethodHelperNoLock(string name, string dllName, string importName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, CallingConvention nativeCallConv, CharSet nativeCharSet)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
      if (dllName == null)
        throw new ArgumentNullException("dllName");
      if (dllName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "dllName");
      if (importName == null)
        throw new ArgumentNullException("importName");
      if (importName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "importName");
      if ((attributes & MethodAttributes.Abstract) != MethodAttributes.PrivateScope)
        throw new ArgumentException(Environment.GetResourceString("Argument_BadPInvokeMethod"));
      if ((this.m_iAttr & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask)
        throw new ArgumentException(Environment.GetResourceString("Argument_BadPInvokeOnInterface"));
      this.ThrowIfCreated();
      attributes |= MethodAttributes.PinvokeImpl;
      MethodBuilder methodBuilder = new MethodBuilder(name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, this.m_module, this, false);
      int length;
      methodBuilder.GetMethodSignature().InternalGetSignature(out length);
      if (this.m_listMethods.Contains(methodBuilder))
        throw new ArgumentException(Environment.GetResourceString("Argument_MethodRedefined"));
      this.m_listMethods.Add(methodBuilder);
      MethodToken token = methodBuilder.GetToken();
      int linkFlags = 0;
      switch (nativeCallConv)
      {
        case CallingConvention.Winapi:
          linkFlags = 256;
          break;
        case CallingConvention.Cdecl:
          linkFlags = 512;
          break;
        case CallingConvention.StdCall:
          linkFlags = 768;
          break;
        case CallingConvention.ThisCall:
          linkFlags = 1024;
          break;
        case CallingConvention.FastCall:
          linkFlags = 1280;
          break;
      }
      switch (nativeCharSet)
      {
        case CharSet.None:
          linkFlags |= 0;
          break;
        case CharSet.Ansi:
          linkFlags |= 2;
          break;
        case CharSet.Unicode:
          linkFlags |= 4;
          break;
        case CharSet.Auto:
          linkFlags |= 6;
          break;
      }
      TypeBuilder.SetPInvokeData(this.m_module.GetNativeHandle(), dllName, importName, token.Token, linkFlags);
      methodBuilder.SetToken(token);
      return methodBuilder;
    }

    [SecurityCritical]
    private FieldBuilder DefineDataHelper(string name, byte[] data, int size, FieldAttributes attributes)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
      if (size <= 0 || size >= 4128768)
        throw new ArgumentException(Environment.GetResourceString("Argument_BadSizeForData"));
      this.ThrowIfCreated();
      string str = "$ArrayType$" + (object) size;
      TypeBuilder typeBuilder = this.m_module.FindTypeBuilderWithName(str, false) as TypeBuilder;
      if ((Type) typeBuilder == (Type) null)
      {
        TypeAttributes attr = TypeAttributes.Public | TypeAttributes.ExplicitLayout | TypeAttributes.Sealed;
        typeBuilder = this.m_module.DefineType(str, attr, typeof (ValueType), PackingSize.Size1, size);
        typeBuilder.CreateType();
      }
      FieldBuilder fieldBuilder = this.DefineField(name, (Type) typeBuilder, attributes | FieldAttributes.Static);
      byte[] data1 = data;
      int size1 = size;
      fieldBuilder.SetData(data1, size1);
      return fieldBuilder;
    }

    private void VerifyTypeAttributes(TypeAttributes attr)
    {
      if (this.DeclaringType == (Type) null)
      {
        if ((attr & TypeAttributes.VisibilityMask) != TypeAttributes.NotPublic && (attr & TypeAttributes.VisibilityMask) != TypeAttributes.Public)
          throw new ArgumentException(Environment.GetResourceString("Argument_BadTypeAttrNestedVisibilityOnNonNestedType"));
      }
      else if ((attr & TypeAttributes.VisibilityMask) == TypeAttributes.NotPublic || (attr & TypeAttributes.VisibilityMask) == TypeAttributes.Public)
        throw new ArgumentException(Environment.GetResourceString("Argument_BadTypeAttrNonNestedVisibilityNestedType"));
      if ((attr & TypeAttributes.LayoutMask) != TypeAttributes.NotPublic && (attr & TypeAttributes.LayoutMask) != TypeAttributes.SequentialLayout && (attr & TypeAttributes.LayoutMask) != TypeAttributes.ExplicitLayout)
        throw new ArgumentException(Environment.GetResourceString("Argument_BadTypeAttrInvalidLayout"));
      if ((attr & TypeAttributes.ReservedMask) != TypeAttributes.NotPublic)
        throw new ArgumentException(Environment.GetResourceString("Argument_BadTypeAttrReservedBitsSet"));
    }

    /// <summary>返回一个值，该值指示是否已创建当前动态类型。</summary>
    /// <returns>如果已调用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 方法，则为 true；否则为 false。</returns>
    public bool IsCreated()
    {
      return this.m_hasBeenCreated;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int DefineType(RuntimeModule module, string fullname, int tkParent, TypeAttributes attributes, int tkEnclosingType, int[] interfaceTokens);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int DefineGenericParam(RuntimeModule module, string name, int tkParent, GenericParameterAttributes attributes, int position, int[] constraints);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void TermCreateClass(RuntimeModule module, int tk, ObjectHandleOnStack type);

    internal void ThrowIfCreated()
    {
      if (this.IsCreated())
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TypeHasBeenCreated"));
    }

    internal ModuleBuilder GetModuleBuilder()
    {
      return this.m_module;
    }

    internal void SetGenParamAttributes(GenericParameterAttributes genericParameterAttributes)
    {
      this.m_genParamAttributes = genericParameterAttributes;
    }

    internal void SetGenParamCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
    {
      TypeBuilder.CustAttr ca = new TypeBuilder.CustAttr(con, binaryAttribute);
      lock (this.SyncRoot)
        this.SetGenParamCustomAttributeNoLock(ca);
    }

    internal void SetGenParamCustomAttribute(CustomAttributeBuilder customBuilder)
    {
      TypeBuilder.CustAttr ca = new TypeBuilder.CustAttr(customBuilder);
      lock (this.SyncRoot)
        this.SetGenParamCustomAttributeNoLock(ca);
    }

    private void SetGenParamCustomAttributeNoLock(TypeBuilder.CustAttr ca)
    {
      if (this.m_ca == null)
        this.m_ca = new List<TypeBuilder.CustAttr>();
      this.m_ca.Add(ca);
    }

    /// <summary>返回不包括命名空间的类型名称。</summary>
    /// <returns>只读。不包括命名空间的类型名称。</returns>
    public override string ToString()
    {
      return TypeNameBuilder.ToString((Type) this, TypeNameBuilder.Format.ToString);
    }

    /// <summary>调用指定的成员。在指定联编程序和调用属性的约束下，要调用的方法必须是可访问的，而且提供与指定参数列表最精确的匹配。</summary>
    /// <returns>返回被调用成员的返回值。</returns>
    /// <param name="name">要调用的成员名。它可以是构造函数、方法、属性或字段。必须指定合适的调用属性。请注意，可以通过将空字符串作为成员名称传递来调用类的默认成员。</param>
    /// <param name="invokeAttr">调用属性。这必须是来自 BindingFlags 的位标志。</param>
    /// <param name="binder">一个对象，它使用反射启用绑定、参数类型的强制、成员的调用和 MemberInfo 对象的检索。如果 binder 为 null，则使用默认联编程序。请参见<see cref="T:System.Reflection.Binder" />。</param>
    /// <param name="target">对其调用指定成员的对象。如果该成员是静态的，则忽略此参数。</param>
    /// <param name="args">参数列表。这是一个对象数组，包含要调用的成员的参数的数目、顺序和类型。如果没有参数，则它应为 null。</param>
    /// <param name="modifiers">与 <paramref name="args" /> 长度相同的数组，其元素表示与要调用的成员的参数相关联的属性。参数在元数据中有关联的属性。它们由各种交互操作服务使用。有关更多详细信息，请参见元数据规范。</param>
    /// <param name="culture">用于控制类型强制的 CultureInfo 的实例。如果这是 null，则使用当前线程的 CultureInfo。（注意，这对于某些转换是必要的，例如，将表示 1000 的 String 转换为 Double 值，因为不同区域性的 1000 表示形式不同。）</param>
    /// <param name="namedParameters">
    /// <paramref name="namedParameters" /> 数组中的每一个参数获取 <paramref name="args" /> 数组中相应元素中的值。如果 <paramref name="args" /> 的长度大于 <paramref name="namedParameters" /> 的长度，则按顺序传递剩余的参数值。</param>
    /// <exception cref="T:System.NotSupportedException">对于不完整类型，目前不支持此方法。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
    }

    protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
    }

    /// <summary>按照指定，返回 <see cref="T:System.Reflection.ConstructorInfo" /> 对象的数组，这些对象表示为此类定义的公共和非公共构造函数。</summary>
    /// <returns>返回 <see cref="T:System.Reflection.ConstructorInfo" /> 对象的数组，这些对象表示为此类定义的指定构造函数。如果未定义任何构造函数，则返回空数组。</returns>
    /// <param name="bindingAttr">这必须是来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志，与 InvokeMethod、NonPublic 等中的一样。</param>
    /// <exception cref="T:System.NotSupportedException">对于不完整的类型，不实现此方法。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [ComVisible(true)]
    public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetConstructors(bindingAttr);
    }

    protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      if (types == null)
        return this.m_bakedRuntimeType.GetMethod(name, bindingAttr);
      return this.m_bakedRuntimeType.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
    }

    /// <summary>按照指定，返回此类型声明或继承的所有公共和非公共方法。</summary>
    /// <returns>如果使用 <paramref name="nonPublic" />，则返回 <see cref="T:System.Reflection.MethodInfo" /> 对象的数组，这些对象表示在此类型上定义的公共和非公共方法；否则，仅返回公共方法。</returns>
    /// <param name="bindingAttr">这必须是来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志，与 InvokeMethod、NonPublic 等中的一样。</param>
    /// <exception cref="T:System.NotSupportedException">对于不完整的类型，不实现此方法。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetMethods(bindingAttr);
    }

    /// <summary>返回由给定名称指定的字段。</summary>
    /// <returns>返回 <see cref="T:System.Reflection.FieldInfo" /> 对象，该对象表示此类型声明或继承且具有指定名称和公共或非公共修饰符的字段。如果没有匹配项，则返回 null。</returns>
    /// <param name="name">要获取的字段的名称。</param>
    /// <param name="bindingAttr">这必须是来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志，与 InvokeMethod、NonPublic 等中的一样。</param>
    /// <exception cref="T:System.NotSupportedException">对于不完整的类型，不实现此方法。</exception>
    public override FieldInfo GetField(string name, BindingFlags bindingAttr)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetField(name, bindingAttr);
    }

    /// <summary>返回此类型声明的公共和非公共字段。</summary>
    /// <returns>返回 <see cref="T:System.Reflection.FieldInfo" /> 对象的数组，这些对象表示此类型声明或继承的公共和非公共字段。按照指定，如果没有任何字段，则返回空数组。</returns>
    /// <param name="bindingAttr">这必须是来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志：InvokeMethod、NonPublic 等。</param>
    /// <exception cref="T:System.NotSupportedException">对于不完整的类型，不实现此方法。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override FieldInfo[] GetFields(BindingFlags bindingAttr)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetFields(bindingAttr);
    }

    /// <summary>返回由此类直接或间接实现的接口，该接口具有与给定接口名匹配的完全限定名。</summary>
    /// <returns>返回 <see cref="T:System.Type" /> 对象，该对象表示实现的接口。如果未找到名称匹配的接口，则返回空。</returns>
    /// <param name="name">接口名。</param>
    /// <param name="ignoreCase">如果为 true，则搜索不区分大小写。如果为 false，则搜索区分大小写。</param>
    /// <exception cref="T:System.NotSupportedException">对于不完整的类型，不实现此方法。</exception>
    public override Type GetInterface(string name, bool ignoreCase)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetInterface(name, ignoreCase);
    }

    /// <summary>返回在此类型及其基类型上实现的所有接口的数组。</summary>
    /// <returns>返回 <see cref="T:System.Type" /> 对象的数组，这些对象表示实现的接口。如果一个都没有定义，则返回空数组。</returns>
    public override Type[] GetInterfaces()
    {
      if (this.m_bakedRuntimeType != (RuntimeType) null)
        return this.m_bakedRuntimeType.GetInterfaces();
      if (this.m_typeInterfaces == null)
        return EmptyArray<Type>.Value;
      return this.m_typeInterfaces.ToArray();
    }

    /// <summary>返回具有指定名称的事件。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.EventInfo" /> 对象，表示这个指定名称的类型声明或继承的事件；如果没有匹配项，则为 null。</returns>
    /// <param name="name">要搜索的事件名称。</param>
    /// <param name="bindingAttr">用于限制搜索的 <see cref="T:System.Reflection.BindingFlags" /> 值的按位组合。</param>
    /// <exception cref="T:System.NotSupportedException">对于不完整的类型，不实现此方法。</exception>
    public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetEvent(name, bindingAttr);
    }

    /// <summary>返回此类型声明或继承的公共事件。</summary>
    /// <returns>返回 <see cref="T:System.Reflection.EventInfo" /> 对象的数组，这些对象表示此类型声明或继承的公共事件。如果没有公共事件，则返回空数组。</returns>
    /// <exception cref="T:System.NotSupportedException">对于不完整的类型，不实现此方法。</exception>
    public override EventInfo[] GetEvents()
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetEvents();
    }

    protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>按照指定，返回此类型声明或继承的所有公共和非公共属性。</summary>
    /// <returns>如果使用 <paramref name="nonPublic" />，则返回 PropertyInfo 对象的数组，这些对象表示在此类型上定义的公共和非公共属性；否则，仅返回公共属性。</returns>
    /// <param name="bindingAttr">此调用属性。这必须是来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志：InvokeMethod、NonPublic 等。</param>
    /// <exception cref="T:System.NotSupportedException">对于不完整的类型，不实现此方法。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetProperties(bindingAttr);
    }

    /// <summary>返回此类型声明或继承的公共和非公共嵌套类型。</summary>
    /// <returns>表示嵌套在当前 <see cref="T:System.Type" /> 中的匹配指定绑定约束的所有类型的 <see cref="T:System.Type" /> 对象数组。如果没有嵌套在当前 <see cref="T:System.Type" /> 中的类型，或者如果没有一个嵌套类型匹配绑定约束，则为 <see cref="T:System.Type" /> 类型的空数组。</returns>
    /// <param name="bindingAttr">这必须是来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志，与 InvokeMethod、NonPublic 等中的一样。</param>
    /// <exception cref="T:System.NotSupportedException">对于不完整的类型，不实现此方法。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override Type[] GetNestedTypes(BindingFlags bindingAttr)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetNestedTypes(bindingAttr);
    }

    /// <summary>返回此类型声明的公共和非公共嵌套类型。</summary>
    /// <returns>表示符合指定要求的嵌套类型的 <see cref="T:System.Type" /> 对象（如果找到的话）；否则为 null。</returns>
    /// <param name="name">包含要获取的嵌套类型的名称的 <see cref="T:System.String" />。</param>
    /// <param name="bindingAttr">一个位屏蔽，由一个或多个指定搜索执行方式的 <see cref="T:System.Reflection.BindingFlags" /> 组成。- 或 -零，表示对公共方法执行区分大小写的搜索。</param>
    /// <exception cref="T:System.NotSupportedException">对于不完整的类型，不实现此方法。</exception>
    public override Type GetNestedType(string name, BindingFlags bindingAttr)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetNestedType(name, bindingAttr);
    }

    /// <summary>按照指定，返回此类型声明或继承的所有公共和非公共成员。</summary>
    /// <returns>如果使用 <paramref name="nonPublic" />，则返回 <see cref="T:System.Reflection.MemberInfo" /> 对象的数组，这些对象表示在此类型上定义的公共和非公共成员；否则，仅返回公共成员。</returns>
    /// <param name="name">成员的名称。</param>
    /// <param name="type">返回的成员的类型。</param>
    /// <param name="bindingAttr">这必须是来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志，与 InvokeMethod、NonPublic 等中的一样。</param>
    /// <exception cref="T:System.NotSupportedException">对于不完整的类型，不实现此方法。</exception>
    public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetMember(name, type, bindingAttr);
    }

    /// <summary>返回请求的接口的接口映射。</summary>
    /// <returns>返回请求的接口映射。</returns>
    /// <param name="interfaceType">为其检索映射的接口的 <see cref="T:System.Type" />。</param>
    /// <exception cref="T:System.NotSupportedException">对于不完整的类型，不实现此方法。</exception>
    [ComVisible(true)]
    public override InterfaceMapping GetInterfaceMap(Type interfaceType)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetInterfaceMap(interfaceType);
    }

    /// <summary>返回此类型声明的公共和非公共事件。</summary>
    /// <returns>返回 <see cref="T:System.Reflection.EventInfo" /> 对象的数组，这些对象表示此类型声明或继承的事件，而这些事件与指定的绑定标志匹配。如果没有匹配的事件，则返回空数组。</returns>
    /// <param name="bindingAttr">用于限制搜索的 <see cref="T:System.Reflection.BindingFlags" /> 值的按位组合。</param>
    /// <exception cref="T:System.NotSupportedException">对于不完整的类型，不实现此方法。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override EventInfo[] GetEvents(BindingFlags bindingAttr)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetEvents(bindingAttr);
    }

    /// <summary>返回由此类型声明或继承的公共和非公共成员的成员。</summary>
    /// <returns>返回 <see cref="T:System.Reflection.MemberInfo" /> 对象的数组，这些对象表示此类型声明或继承的公共和非公共成员。如果没有匹配的成员，则返回空数组。</returns>
    /// <param name="bindingAttr">这必须是来自 <see cref="T:System.Reflection.BindingFlags" /> 的位标志，如 InvokeMethod、NonPublic 等。</param>
    /// <exception cref="T:System.NotSupportedException">对于不完整的类型，不实现此方法。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return this.m_bakedRuntimeType.GetMembers(bindingAttr);
    }

    /// <summary>获取一个值，该值指示指定的 <see cref="T:System.Type" /> 是否可以分配给这个对象。</summary>
    /// <returns>如果 <paramref name="c" /> 参数和当前类型表示同一类型，或者当前类型位于 <paramref name="c" /> 的继承层次结构中，或者当前类型是 <paramref name="c" /> 支持的接口，则为 true。如果不满足上述任何一个条件或者 <paramref name="c" /> 为 null，则为 false。</returns>
    /// <param name="c">要测试的对象。</param>
    public override bool IsAssignableFrom(Type c)
    {
      if (TypeBuilder.IsTypeEqual(c, (Type) this))
        return true;
      TypeBuilder typeBuilder = c as TypeBuilder;
      Type c1 = !((Type) typeBuilder != (Type) null) ? c : (Type) typeBuilder.m_bakedRuntimeType;
      if (c1 != (Type) null && c1 is RuntimeType)
      {
        if (this.m_bakedRuntimeType == (RuntimeType) null)
          return false;
        return this.m_bakedRuntimeType.IsAssignableFrom(c1);
      }
      if ((Type) typeBuilder == (Type) null)
        return false;
      if (typeBuilder.IsSubclassOf((Type) this))
        return true;
      if (!this.IsInterface)
        return false;
      Type[] interfaces = typeBuilder.GetInterfaces();
      for (int index = 0; index < interfaces.Length; ++index)
      {
        if (TypeBuilder.IsTypeEqual(interfaces[index], (Type) this) || interfaces[index].IsSubclassOf((Type) this))
          return true;
      }
      return false;
    }

    protected override TypeAttributes GetAttributeFlagsImpl()
    {
      return this.m_iAttr;
    }

    protected override bool IsArrayImpl()
    {
      return false;
    }

    protected override bool IsByRefImpl()
    {
      return false;
    }

    protected override bool IsPointerImpl()
    {
      return false;
    }

    protected override bool IsPrimitiveImpl()
    {
      return false;
    }

    protected override bool IsCOMObjectImpl()
    {
      return (this.GetAttributeFlagsImpl() & TypeAttributes.Import) != TypeAttributes.NotPublic;
    }

    /// <summary>调用此方法始终引发 <see cref="T:System.NotSupportedException" />。</summary>
    /// <returns>此方法不受支持。不返回任何值。</returns>
    /// <exception cref="T:System.NotSupportedException">此方法不受支持。</exception>
    public override Type GetElementType()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    protected override bool HasElementTypeImpl()
    {
      return false;
    }

    /// <summary>确定此类型是否从指定的类型派生而来。</summary>
    /// <returns>只读。如果此类型与 <paramref name="c" /> 类型相同，或是 <paramref name="c" /> 类型的子类型，则返回 true；否则返回 false。</returns>
    /// <param name="c">要检查的 <see cref="T:System.Type" />。</param>
    [ComVisible(true)]
    public override bool IsSubclassOf(Type c)
    {
      Type t1 = (Type) this;
      if (TypeBuilder.IsTypeEqual(t1, c))
        return false;
      for (Type baseType = t1.BaseType; baseType != (Type) null; baseType = baseType.BaseType)
      {
        if (TypeBuilder.IsTypeEqual(baseType, c))
          return true;
      }
      return false;
    }

    /// <summary>返回一个 <see cref="T:System.Type" /> 对象，该对象表示指向当前类型的非托管指针的类型。</summary>
    /// <returns>一个 <see cref="T:System.Type" /> 对象，表示指向当前类型的非托管指针的类型。</returns>
    public override Type MakePointerType()
    {
      return SymbolType.FormCompoundType("*".ToCharArray(), (Type) this, 0);
    }

    /// <summary>返回一个 <see cref="T:System.Type" /> 对象，该对象表示作为 ref（在 Visual Basic 中为 ByRef）参数传递的当前类型。</summary>
    /// <returns>一个 <see cref="T:System.Type" /> 对象，表示作为 ref（在 Visual Basic 中为 ByRef）参数传递的当前类型。</returns>
    public override Type MakeByRefType()
    {
      return SymbolType.FormCompoundType("&".ToCharArray(), (Type) this, 0);
    }

    /// <summary>返回 <see cref="T:System.Type" /> 对象，该对象表示一个当前类型的一维数组，其下限为零。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 对象表示一个一维数组类型，其元素类型为当前类型，其下限为零。</returns>
    public override Type MakeArrayType()
    {
      return SymbolType.FormCompoundType("[]".ToCharArray(), (Type) this, 0);
    }

    /// <summary>返回 <see cref="T:System.Type" /> 对象，该对象表示一个具有指定维数的当前类型的数组。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 对象表示一个当前类型的一维数组。</returns>
    /// <param name="rank">数组的维数。</param>
    /// <exception cref="T:System.IndexOutOfRangeException">
    /// <paramref name="rank" /> 不是有效的数组维数。</exception>
    public override Type MakeArrayType(int rank)
    {
      if (rank <= 0)
        throw new IndexOutOfRangeException();
      string str = "";
      if (rank == 1)
      {
        str = "*";
      }
      else
      {
        for (int index = 1; index < rank; ++index)
          str += ",";
      }
      return SymbolType.FormCompoundType(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "[{0}]", (object) str).ToCharArray(), (Type) this, 0);
    }

    /// <summary>返回为此类型定义的所有自定义属性。</summary>
    /// <returns>返回对象的数组，这些对象表示此类型的所有自定义特性。</returns>
    /// <param name="inherit">指定是否搜索该成员的继承链以查找这些属性。</param>
    /// <exception cref="T:System.NotSupportedException">对于不完整类型，目前不支持此方法。使用 <see cref="M:System.Type.GetType" /> 检索类型，并对返回的 <see cref="T:System.Type" /> 调用 <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" />。</exception>
    [SecuritySafeCritical]
    public override object[] GetCustomAttributes(bool inherit)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      return CustomAttribute.GetCustomAttributes(this.m_bakedRuntimeType, typeof (object) as RuntimeType, inherit);
    }

    /// <summary>返回当前类型的所有自定义属性，可以将这些属性分配给指定类型。</summary>
    /// <returns>针对当前类型定义的自定义属性的数组。</returns>
    /// <param name="attributeType">要搜索的特性类型。只返回可分配给此类型的属性。</param>
    /// <param name="inherit">指定是否搜索该成员的继承链以查找这些属性。</param>
    /// <exception cref="T:System.NotSupportedException">对于不完整类型，目前不支持此方法。使用 <see cref="M:System.Type.GetType" /> 检索类型，并对返回的 <see cref="T:System.Type" /> 调用 <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">该类型必须是由基础运行时系统提供的类型。</exception>
    [SecuritySafeCritical]
    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      if (attributeType == (Type) null)
        throw new ArgumentNullException("attributeType");
      RuntimeType caType = attributeType.UnderlyingSystemType as RuntimeType;
      if (caType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
      return CustomAttribute.GetCustomAttributes(this.m_bakedRuntimeType, caType, inherit);
    }

    /// <summary>确定是否将自定义属性应用于当前类型。</summary>
    /// <returns>如果针对此类型定义了 <paramref name="attributeType" /> 的一个或多个实例，或定义了一个从 <paramref name="attributeType" /> 派生的属性，则为 true；否则为 false。</returns>
    /// <param name="attributeType">要搜索的特性类型。只返回可分配给此类型的属性。</param>
    /// <param name="inherit">指定是否搜索该成员的继承链以查找这些属性。</param>
    /// <exception cref="T:System.NotSupportedException">对于不完整类型，目前不支持此方法。使用 <see cref="M:System.Type.GetType" /> 检索类型，并对返回的 <see cref="T:System.Type" /> 调用 <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 未定义。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="attributeType" /> 为 null。</exception>
    [SecuritySafeCritical]
    public override bool IsDefined(Type attributeType, bool inherit)
    {
      if (!this.IsCreated())
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_TypeNotYetCreated"));
      if (attributeType == (Type) null)
        throw new ArgumentNullException("attributeType");
      RuntimeType caType = attributeType.UnderlyingSystemType as RuntimeType;
      if (caType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "caType");
      return CustomAttribute.IsDefined(this.m_bakedRuntimeType, caType, inherit);
    }

    internal void SetInterfaces(params Type[] interfaces)
    {
      this.ThrowIfCreated();
      this.m_typeInterfaces = new List<Type>();
      if (interfaces == null)
        return;
      this.m_typeInterfaces.AddRange((IEnumerable<Type>) interfaces);
    }

    /// <summary>为当前类型定义泛型类型参数，指定参数的个数和名称，并返回一个 <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> 对象的数组，这些对象可用于设置参数的约束。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> 对象的数组，这些对象可用于为当前类型定义泛型类型参数的约束。</returns>
    /// <param name="names">泛型类型参数的名称数组。</param>
    /// <exception cref="T:System.InvalidOperationException">已为此类型定义了泛型类型参数。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="names" /> 为 null。- 或 -<paramref name="names" /> 的一个元素为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="names" /> 为空数组。</exception>
    public GenericTypeParameterBuilder[] DefineGenericParameters(params string[] names)
    {
      if (names == null)
        throw new ArgumentNullException("names");
      if (names.Length == 0)
        throw new ArgumentException();
      for (int index = 0; index < names.Length; ++index)
      {
        if (names[index] == null)
          throw new ArgumentNullException("names");
      }
      if (this.m_inst != null)
        throw new InvalidOperationException();
      this.m_inst = new GenericTypeParameterBuilder[names.Length];
      for (int genParamPos = 0; genParamPos < names.Length; ++genParamPos)
        this.m_inst[genParamPos] = new GenericTypeParameterBuilder(new TypeBuilder(names[genParamPos], genParamPos, this));
      return this.m_inst;
    }

    /// <summary>用一个类型数组的元素取代当前泛型类型定义的类型参数，然后返回结果构造类型。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 表示的构造类型通过以下方式形成：用 <paramref name="typeArguments" /> 的元素取代当前泛型类型的类型参数。</returns>
    /// <param name="typeArguments">一个类型数组，用于取代当前泛型类型定义的类型参数。</param>
    /// <exception cref="T:System.InvalidOperationException">当前类型不表示泛型类型的定义。即 <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> 返回 false。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeArguments" /> 为 null。- 或 -<paramref name="typeArguments" /> 的所有元素均为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="typeArguments" /> 的所有元素都不满足为当前泛型类型的对应类型参数指定的约束。</exception>
    public override Type MakeGenericType(params Type[] typeArguments)
    {
      this.CheckContext(typeArguments);
      return TypeBuilderInstantiation.MakeGenericType((Type) this, typeArguments);
    }

    /// <summary>返回一个 <see cref="T:System.Type" /> 对象的数组，这些对象表示泛型类型的类型变量或泛型类型定义的类型参数。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 对象的数组。该数组的元素表示泛型类型的类型变量或泛型类型定义的类型参数。</returns>
    public override Type[] GetGenericArguments()
    {
      return (Type[]) this.m_inst;
    }

    /// <summary>返回的 <see cref="T:System.Type" /> 对象表示一个泛型类型定义，可以从该定义中获取当前类型。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 对象表示一个泛型类型定义，可以从该定义中获取当前类型。</returns>
    /// <exception cref="T:System.InvalidOperationException">当前类型不是泛型类型。即，<see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> 返回 false。</exception>
    public override Type GetGenericTypeDefinition()
    {
      if (this.IsGenericTypeDefinition)
        return (Type) this;
      if ((Type) this.m_genTypeDef == (Type) null)
        throw new InvalidOperationException();
      return (Type) this.m_genTypeDef;
    }

    /// <summary>指定实现给定方法声明的给定方法体，可能使用不同名称。</summary>
    /// <param name="methodInfoBody">要使用的方法体。应该是 MethodBuilder 对象。</param>
    /// <param name="methodInfoDeclaration">要使用其声明的方法。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="methodInfoBody" /> 不属于此类。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="methodInfoBody" /> 或 <paramref name="methodInfoDeclaration" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">该类型是以前用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建的。- 或 -<paramref name="methodInfoBody" /> 的声明类型不是由此 <see cref="T:System.Reflection.Emit.TypeBuilder" /> 表示的类型。</exception>
    [SecuritySafeCritical]
    public void DefineMethodOverride(MethodInfo methodInfoBody, MethodInfo methodInfoDeclaration)
    {
      lock (this.SyncRoot)
        this.DefineMethodOverrideNoLock(methodInfoBody, methodInfoDeclaration);
    }

    [SecurityCritical]
    private void DefineMethodOverrideNoLock(MethodInfo methodInfoBody, MethodInfo methodInfoDeclaration)
    {
      if (methodInfoBody == (MethodInfo) null)
        throw new ArgumentNullException("methodInfoBody");
      if (methodInfoDeclaration == (MethodInfo) null)
        throw new ArgumentNullException("methodInfoDeclaration");
      this.ThrowIfCreated();
      if (methodInfoBody.DeclaringType != this)
        throw new ArgumentException(Environment.GetResourceString("ArgumentException_BadMethodImplBody"));
      TypeBuilder.DefineMethodImpl(this.m_module.GetNativeHandle(), this.m_tdType.Token, this.m_module.GetMethodTokenInternal(methodInfoBody).Token, this.m_module.GetMethodTokenInternal(methodInfoDeclaration).Token);
    }

    /// <summary>使用指定的名称、方法属性和调用约定向类型中添加新方法。</summary>
    /// <returns>已定义的方法。</returns>
    /// <param name="name">方法的名称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="attributes">该方法的特性。</param>
    /// <param name="returnType">该方法的返回类型。</param>
    /// <param name="parameterTypes">该方法的参数的类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 的长度为零。- 或 -此方法的父级类型是一个接口，而且此方法不是虚拟的（Visual Basic 中为 Overridable）。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">该类型是以前用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建的。- 或 -对于当前动态类型，<see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> 属性为 true，而 <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> 属性为 false。</exception>
    public MethodBuilder DefineMethod(string name, MethodAttributes attributes, Type returnType, Type[] parameterTypes)
    {
      return this.DefineMethod(name, attributes, CallingConventions.Standard, returnType, parameterTypes);
    }

    /// <summary>使用指定的名称和方法属性向类型中添加新方法。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.Emit.MethodBuilder" />，它表示新定义的方法。</returns>
    /// <param name="name">方法的名称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="attributes">该方法的特性。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 的长度为零。- 或 -此方法的父级类型是一个接口，而且此方法不是虚拟的（Visual Basic 中为 Overridable）。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">该类型是以前用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建的。- 或 -对于当前动态类型，<see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> 属性为 true，而 <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> 属性为 false。</exception>
    public MethodBuilder DefineMethod(string name, MethodAttributes attributes)
    {
      return this.DefineMethod(name, attributes, CallingConventions.Standard, (Type) null, (Type[]) null);
    }

    /// <summary>使用指定名称、方法属性和调用约定向类型中添加新方法。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.Emit.MethodBuilder" />，它表示新定义的方法。</returns>
    /// <param name="name">方法的名称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="attributes">该方法的特性。</param>
    /// <param name="callingConvention">该方法的调用约定。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 的长度为零。- 或 -此方法的父级类型是一个接口，而且此方法不是虚拟的（Visual Basic 中为 Overridable）。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">该类型是以前用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建的。- 或 -对于当前动态类型，<see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> 属性为 true，而 <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> 属性为 false。</exception>
    public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention)
    {
      return this.DefineMethod(name, attributes, callingConvention, (Type) null, (Type[]) null);
    }

    /// <summary>使用指定的名称、方法属性、调用约定和方法签名向类型中添加新方法。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.Emit.MethodBuilder" />，它表示新定义的方法。</returns>
    /// <param name="name">方法的名称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="attributes">该方法的特性。</param>
    /// <param name="callingConvention">该方法的调用约定。</param>
    /// <param name="returnType">该方法的返回类型。</param>
    /// <param name="parameterTypes">该方法的参数的类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 的长度为零。- 或 -此方法的父级类型是一个接口，而且此方法不是虚拟的（Visual Basic 中为 Overridable）。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">该类型是以前用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建的。- 或 -对于当前动态类型，<see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> 属性为 true，而 <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> 属性为 false。</exception>
    public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
    {
      return this.DefineMethod(name, attributes, callingConvention, returnType, (Type[]) null, (Type[]) null, parameterTypes, (Type[][]) null, (Type[][]) null);
    }

    /// <summary>使用指定的名称、方法属性、调用约定、方法签名和自定义修饰符向类型中添加新方法。</summary>
    /// <returns>一个表示新添加方法的 <see cref="T:System.Reflection.Emit.MethodBuilder" /> 对象。</returns>
    /// <param name="name">方法的名称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="attributes">该方法的特性。</param>
    /// <param name="callingConvention">该方法的调用约定。</param>
    /// <param name="returnType">该方法的返回类型。</param>
    /// <param name="returnTypeRequiredCustomModifiers">一个类型数组，表示该方法的返回类型的必需的自定义修饰符（如，<see cref="T:System.Runtime.CompilerServices.IsConst" />）。如果返回类型没有必需的自定义修饰符，请指定 null。</param>
    /// <param name="returnTypeOptionalCustomModifiers">一个类型数组，表示该方法的返回类型的可选自定义修饰符（例如，<see cref="T:System.Runtime.CompilerServices.IsConst" />）。如果返回类型没有可选的自定义修饰符，请指定 null。</param>
    /// <param name="parameterTypes">该方法的参数的类型。</param>
    /// <param name="parameterTypeRequiredCustomModifiers">由类型数组组成的数组。每个类型数组均表示相应参数所必需的自定义修饰符，如 <see cref="T:System.Runtime.CompilerServices.IsConst" />。如果某个特定参数没有必需的自定义修饰符，请指定 null，而不指定类型数组。如果没有参数具有必需的自定义修饰符，请指定 null，而不指定由数组构成的数组。</param>
    /// <param name="parameterTypeOptionalCustomModifiers">由类型数组组成的数组。每个类型数组均表示相应参数的可选自定义修饰符，如 <see cref="T:System.Runtime.CompilerServices.IsConst" />。如果某个特定参数没有可选的自定义修饰符，请指定 null，而不指定类型数组。如果没有参数具有可选的自定义修饰符，请指定 null，而不指定由数组构成的数组。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 的长度为零。- 或 -此方法的父级类型是一个接口，而且此方法不是虚拟的（Visual Basic 中为 Overridable）。- 或 -<paramref name="parameterTypeRequiredCustomModifiers" /> 或 <paramref name="parameterTypeOptionalCustomModifiers" /> 的大小不等于 <paramref name="parameterTypes" /> 的大小。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">该类型是以前用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建的。- 或 -对于当前动态类型，<see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> 属性为 true，而 <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> 属性为 false。</exception>
    public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
    {
      lock (this.SyncRoot)
        return this.DefineMethodNoLock(name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
    }

    private MethodBuilder DefineMethodNoLock(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
      this.CheckContext(new Type[1]{ returnType });
      this.CheckContext(returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes);
      this.CheckContext(parameterTypeRequiredCustomModifiers);
      this.CheckContext(parameterTypeOptionalCustomModifiers);
      if (parameterTypes != null)
      {
        if (parameterTypeOptionalCustomModifiers != null && parameterTypeOptionalCustomModifiers.Length != parameterTypes.Length)
          throw new ArgumentException(Environment.GetResourceString("Argument_MismatchedArrays", (object) "parameterTypeOptionalCustomModifiers", (object) "parameterTypes"));
        if (parameterTypeRequiredCustomModifiers != null && parameterTypeRequiredCustomModifiers.Length != parameterTypes.Length)
          throw new ArgumentException(Environment.GetResourceString("Argument_MismatchedArrays", (object) "parameterTypeRequiredCustomModifiers", (object) "parameterTypes"));
      }
      this.ThrowIfCreated();
      if (!this.m_isHiddenGlobalType && (this.m_iAttr & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask && ((attributes & MethodAttributes.Abstract) == MethodAttributes.PrivateScope && (attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope))
        throw new ArgumentException(Environment.GetResourceString("Argument_BadAttributeOnInterfaceMethod"));
      MethodBuilder methodBuilder = new MethodBuilder(name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, this.m_module, this, false);
      if (!this.m_isHiddenGlobalType && (methodBuilder.Attributes & MethodAttributes.SpecialName) != MethodAttributes.PrivateScope && methodBuilder.Name.Equals(ConstructorInfo.ConstructorName))
        this.m_constructorCount = this.m_constructorCount + 1;
      this.m_listMethods.Add(methodBuilder);
      return methodBuilder;
    }

    /// <summary>为此类型定义初始值设定项。</summary>
    /// <returns>返回类型初始值设定项。</returns>
    /// <exception cref="T:System.InvalidOperationException">以前已使用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建了包含类型。</exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public ConstructorBuilder DefineTypeInitializer()
    {
      lock (this.SyncRoot)
        return this.DefineTypeInitializerNoLock();
    }

    [SecurityCritical]
    private ConstructorBuilder DefineTypeInitializerNoLock()
    {
      this.ThrowIfCreated();
      MethodAttributes attributes = MethodAttributes.Private | MethodAttributes.Static | MethodAttributes.SpecialName;
      return new ConstructorBuilder(ConstructorInfo.TypeConstructorName, attributes, CallingConventions.Standard, (Type[]) null, this.m_module, this);
    }

    /// <summary>定义默认的构造函数。这里定义的构造函数只调用父类的默认构造函数。</summary>
    /// <returns>返回该构造函数。</returns>
    /// <param name="attributes">MethodAttributes 对象，表示应用于构造函数的属性。</param>
    /// <exception cref="T:System.NotSupportedException">父类型（基类型）没有默认构造函数。</exception>
    /// <exception cref="T:System.InvalidOperationException">该类型是以前用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建的。- 或 -对于当前动态类型，<see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> 属性为 true，而 <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> 属性为 false。</exception>
    [ComVisible(true)]
    public ConstructorBuilder DefineDefaultConstructor(MethodAttributes attributes)
    {
      if ((this.m_iAttr & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ConstructorNotAllowedOnInterface"));
      lock (this.SyncRoot)
        return this.DefineDefaultConstructorNoLock(attributes);
    }

    private ConstructorBuilder DefineDefaultConstructorNoLock(MethodAttributes attributes)
    {
      ConstructorInfo constructorInfo = (ConstructorInfo) null;
      if (this.m_typeParent is TypeBuilderInstantiation)
      {
        Type type1 = this.m_typeParent.GetGenericTypeDefinition();
        if (type1 is TypeBuilder)
          type1 = (Type) ((TypeBuilder) type1).m_bakedRuntimeType;
        if (type1 == (Type) null)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
        Type type2 = type1.MakeGenericType(this.m_typeParent.GetGenericArguments());
        constructorInfo = !(type2 is TypeBuilderInstantiation) ? type2.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, Type.EmptyTypes, (ParameterModifier[]) null) : TypeBuilder.GetConstructor(type2, type1.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, Type.EmptyTypes, (ParameterModifier[]) null));
      }
      if (constructorInfo == (ConstructorInfo) null)
        constructorInfo = this.m_typeParent.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, Type.EmptyTypes, (ParameterModifier[]) null);
      if (constructorInfo == (ConstructorInfo) null)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_NoParentDefaultConstructor"));
      ConstructorBuilder constructorBuilder = this.DefineConstructor(attributes, CallingConventions.Standard, (Type[]) null);
      this.m_constructorCount = this.m_constructorCount + 1;
      ILGenerator ilGenerator = constructorBuilder.GetILGenerator();
      OpCode opcode1 = OpCodes.Ldarg_0;
      ilGenerator.Emit(opcode1);
      OpCode opcode2 = OpCodes.Call;
      ConstructorInfo con = constructorInfo;
      ilGenerator.Emit(opcode2, con);
      OpCode opcode3 = OpCodes.Ret;
      ilGenerator.Emit(opcode3);
      int num = 1;
      constructorBuilder.m_isDefaultConstructor = num != 0;
      return constructorBuilder;
    }

    /// <summary>用给定的属性和签名，向类型中添加新的构造函数。</summary>
    /// <returns>已定义的构造函数。</returns>
    /// <param name="attributes">构造函数的属性。</param>
    /// <param name="callingConvention">构造函数的调用约定。</param>
    /// <param name="parameterTypes">构造函数的参数类型。</param>
    /// <exception cref="T:System.InvalidOperationException">该类型是以前用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建的。</exception>
    [ComVisible(true)]
    public ConstructorBuilder DefineConstructor(MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes)
    {
      return this.DefineConstructor(attributes, callingConvention, parameterTypes, (Type[][]) null, (Type[][]) null);
    }

    /// <summary>用给定的属性、签名和自定义修饰符，向类型中添加新的构造函数。</summary>
    /// <returns>已定义的构造函数。</returns>
    /// <param name="attributes">构造函数的属性。</param>
    /// <param name="callingConvention">构造函数的调用约定。</param>
    /// <param name="parameterTypes">构造函数的参数类型。</param>
    /// <param name="requiredCustomModifiers">由类型数组组成的数组。每个类型数组均表示相应参数所必需的自定义修饰符，如 <see cref="T:System.Runtime.CompilerServices.IsConst" />。如果某个特定参数没有必需的自定义修饰符，请指定 null，而不指定类型数组。如果没有参数具有必需的自定义修饰符，请指定 null，而不指定由数组构成的数组。</param>
    /// <param name="optionalCustomModifiers">由类型数组组成的数组。每个类型数组均表示相应参数的可选自定义修饰符，如 <see cref="T:System.Runtime.CompilerServices.IsConst" />。如果某个特定参数没有可选的自定义修饰符，请指定 null，而不指定类型数组。如果没有参数具有可选的自定义修饰符，请指定 null，而不指定由数组构成的数组。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="requiredCustomModifiers" /> 或 <paramref name="optionalCustomModifiers" /> 的大小与 <paramref name="parameterTypes" /> 的大小不相等。</exception>
    /// <exception cref="T:System.InvalidOperationException">该类型是以前用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建的。- 或 -对于当前动态类型，<see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> 属性为 true，而 <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> 属性为 false。</exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public ConstructorBuilder DefineConstructor(MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers)
    {
      if ((this.m_iAttr & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask && (attributes & MethodAttributes.Static) != MethodAttributes.Static)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ConstructorNotAllowedOnInterface"));
      lock (this.SyncRoot)
        return this.DefineConstructorNoLock(attributes, callingConvention, parameterTypes, requiredCustomModifiers, optionalCustomModifiers);
    }

    [SecurityCritical]
    private ConstructorBuilder DefineConstructorNoLock(MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers)
    {
      this.CheckContext(parameterTypes);
      this.CheckContext(requiredCustomModifiers);
      this.CheckContext(optionalCustomModifiers);
      this.ThrowIfCreated();
      string name = (attributes & MethodAttributes.Static) != MethodAttributes.PrivateScope ? ConstructorInfo.TypeConstructorName : ConstructorInfo.ConstructorName;
      attributes |= MethodAttributes.SpecialName;
      ConstructorBuilder constructorBuilder = new ConstructorBuilder(name, attributes, callingConvention, parameterTypes, requiredCustomModifiers, optionalCustomModifiers, this.m_module, this);
      this.m_constructorCount = this.m_constructorCount + 1;
      return constructorBuilder;
    }

    /// <summary>已知 PInvoke 方法的名称、定义该方法的 DLL 的名称、该方法的属性、该方法的调用约定、该方法的返回类型、该方法的参数类型以及 PInvoke 标志，定义该方法。</summary>
    /// <returns>已定义的 PInvoke 方法。</returns>
    /// <param name="name">PInvoke 方法的名称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="dllName">在其中定义 PInvoke 方法的 DLL 的名称。</param>
    /// <param name="attributes">该方法的特性。</param>
    /// <param name="callingConvention">该方法的调用约定。</param>
    /// <param name="returnType">该方法的返回类型。</param>
    /// <param name="parameterTypes">方法参数的类型。</param>
    /// <param name="nativeCallConv">本机调用约定。</param>
    /// <param name="nativeCharSet">该方法的本机字符集。</param>
    /// <exception cref="T:System.ArgumentException">该方法不是静态的。- 或 -父类型是接口。- 或 -这种方法是抽象的方法。- 或 -该方法是以前定义的。- 或 -<paramref name="name" /> 或 <paramref name="dllName" /> 的长度为零。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 或 <paramref name="dllName" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">以前已使用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建了包含类型。</exception>
    [SecuritySafeCritical]
    public MethodBuilder DefinePInvokeMethod(string name, string dllName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
    {
      return this.DefinePInvokeMethodHelper(name, dllName, name, attributes, callingConvention, returnType, (Type[]) null, (Type[]) null, parameterTypes, (Type[][]) null, (Type[][]) null, nativeCallConv, nativeCharSet);
    }

    /// <summary>在已知 PInvoke 方法的名称、定义该方法的 DLL 的名称、入口点的名称、该方法的属性、该方法的调用约定、该方法的返回类型、该方法的参数类型以及 PInvoke 标志的情况下，定义该方法。</summary>
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
    /// <exception cref="T:System.ArgumentException">该方法不是静态的。- 或 -父类型是接口。- 或 -这种方法是抽象的方法。- 或 -该方法是以前定义的。- 或 -<paramref name="name" />、<paramref name="dllName" /> 或 <paramref name="entryName" /> 的长度为零。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" />、<paramref name="dllName" /> 或 <paramref name="entryName" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">以前已使用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建了包含类型。</exception>
    [SecuritySafeCritical]
    public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
    {
      return this.DefinePInvokeMethodHelper(name, dllName, entryName, attributes, callingConvention, returnType, (Type[]) null, (Type[]) null, parameterTypes, (Type[][]) null, (Type[][]) null, nativeCallConv, nativeCharSet);
    }

    /// <summary>要定义 PInvoke 方法，需要给出以下各项：该方法的名称，在其中定义该方法的 DLL 的名称，入口点的名称，该方法的属性、调用约定、返回类型、参数类型，PInvoke 标志，参数和返回类型的自定义修饰符。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.Emit.MethodBuilder" />，表示定义的 PInvoke 方法。</returns>
    /// <param name="name">PInvoke 方法的名称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="dllName">在其中定义 PInvoke 方法的 DLL 的名称。</param>
    /// <param name="entryName">DLL 中的入口点名称。</param>
    /// <param name="attributes">该方法的特性。</param>
    /// <param name="callingConvention">该方法的调用约定。</param>
    /// <param name="returnType">该方法的返回类型。</param>
    /// <param name="returnTypeRequiredCustomModifiers">一个类型数组，表示该方法的返回类型的必需的自定义修饰符（如，<see cref="T:System.Runtime.CompilerServices.IsConst" />）。如果返回类型没有必需的自定义修饰符，请指定 null。</param>
    /// <param name="returnTypeOptionalCustomModifiers">一个类型数组，表示该方法的返回类型的可选自定义修饰符（例如，<see cref="T:System.Runtime.CompilerServices.IsConst" />）。如果返回类型没有可选的自定义修饰符，请指定 null。</param>
    /// <param name="parameterTypes">方法参数的类型。</param>
    /// <param name="parameterTypeRequiredCustomModifiers">由类型数组组成的数组。每个类型数组均表示相应参数所必需的自定义修饰符，如 <see cref="T:System.Runtime.CompilerServices.IsConst" />。如果某个特定参数没有必需的自定义修饰符，请指定 null，而不指定类型数组。如果没有参数具有必需的自定义修饰符，请指定 null，而不指定由数组构成的数组。</param>
    /// <param name="parameterTypeOptionalCustomModifiers">由类型数组组成的数组。每个类型数组均表示相应参数的可选自定义修饰符，如 <see cref="T:System.Runtime.CompilerServices.IsConst" />。如果某个特定参数没有可选的自定义修饰符，请指定 null，而不指定类型数组。如果没有参数具有可选的自定义修饰符，请指定 null，而不指定由数组构成的数组。</param>
    /// <param name="nativeCallConv">本机调用约定。</param>
    /// <param name="nativeCharSet">该方法的本机字符集。</param>
    /// <exception cref="T:System.ArgumentException">该方法不是静态的。- 或 -父类型是接口。- 或 -这种方法是抽象的方法。- 或 -该方法是以前定义的。- 或 -<paramref name="name" />、<paramref name="dllName" /> 或 <paramref name="entryName" /> 的长度为零。- 或 -<paramref name="parameterTypeRequiredCustomModifiers" /> 或 <paramref name="parameterTypeOptionalCustomModifiers" /> 的大小不等于 <paramref name="parameterTypes" /> 的大小。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" />、<paramref name="dllName" /> 或 <paramref name="entryName" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">该类型是以前用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建的。- 或 -对于当前动态类型，<see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> 属性为 true，而 <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> 属性为 false。</exception>
    [SecuritySafeCritical]
    public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, CallingConvention nativeCallConv, CharSet nativeCharSet)
    {
      return this.DefinePInvokeMethodHelper(name, dllName, entryName, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, nativeCallConv, nativeCharSet);
    }

    /// <summary>已知名称，定义嵌套类型。</summary>
    /// <returns>已定义的嵌套类型。</returns>
    /// <param name="name">类型的简称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 的长度为零或大于 1023。- 或 -此操作将在当前程序集中用重复的 <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> 创建类型。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    [SecuritySafeCritical]
    public TypeBuilder DefineNestedType(string name)
    {
      lock (this.SyncRoot)
        return this.DefineNestedTypeNoLock(name, TypeAttributes.NestedPrivate, (Type) null, (Type[]) null, PackingSize.Unspecified, 0);
    }

    /// <summary>已知嵌套类型的名称、属性、它扩展的类型和它实现的接口，定义嵌套类型。</summary>
    /// <returns>已定义的嵌套类型。</returns>
    /// <param name="name">类型的简称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="attr">该类型的属性。</param>
    /// <param name="parent">嵌套类型扩展的类型。</param>
    /// <param name="interfaces">嵌套类型实现的接口。</param>
    /// <exception cref="T:System.ArgumentException">未指定嵌套属性。- 或 -此类型是密封的。- 或 -此类型是数组。- 或 -此类型是接口，但嵌套类型不是接口。- 或 -<paramref name="name" /> 的长度为零或大于 1023。- 或 -此操作将在当前程序集中用重复的 <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> 创建类型。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。- 或 -<paramref name="interfaces" /> 数组的一个元素为 null。</exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, Type[] interfaces)
    {
      lock (this.SyncRoot)
      {
        this.CheckContext(new Type[1]{ parent });
        this.CheckContext(interfaces);
        return this.DefineNestedTypeNoLock(name, attr, parent, interfaces, PackingSize.Unspecified, 0);
      }
    }

    /// <summary>已知嵌套类型的名称、属性和它扩展的类型，定义嵌套类型。</summary>
    /// <returns>已定义的嵌套类型。</returns>
    /// <param name="name">类型的简称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="attr">该类型的属性。</param>
    /// <param name="parent">嵌套类型扩展的类型。</param>
    /// <exception cref="T:System.ArgumentException">未指定嵌套属性。- 或 -此类型是密封的。- 或 -此类型是数组。- 或 -此类型是接口，但嵌套类型不是接口。- 或 -<paramref name="name" /> 的长度为零或大于 1023。- 或 -此操作将在当前程序集中用重复的 <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> 创建类型。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    [SecuritySafeCritical]
    public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent)
    {
      lock (this.SyncRoot)
        return this.DefineNestedTypeNoLock(name, attr, parent, (Type[]) null, PackingSize.Unspecified, 0);
    }

    /// <summary>已知名称和属性，定义嵌套类型。</summary>
    /// <returns>已定义的嵌套类型。</returns>
    /// <param name="name">类型的简称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="attr">该类型的属性。</param>
    /// <exception cref="T:System.ArgumentException">未指定嵌套属性。- 或 -此类型是密封的。- 或 -此类型是数组。- 或 -此类型是接口，但嵌套类型不是接口。- 或 -<paramref name="name" /> 的长度为零或大于 1023。- 或 -此操作将在当前程序集中用重复的 <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> 创建类型。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    [SecuritySafeCritical]
    public TypeBuilder DefineNestedType(string name, TypeAttributes attr)
    {
      lock (this.SyncRoot)
        return this.DefineNestedTypeNoLock(name, attr, (Type) null, (Type[]) null, PackingSize.Unspecified, 0);
    }

    /// <summary>已知嵌套类型的名称、属性、类型的总大小和它扩展的类型，定义嵌套类型。</summary>
    /// <returns>已定义的嵌套类型。</returns>
    /// <param name="name">类型的简称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="attr">该类型的属性。</param>
    /// <param name="parent">嵌套类型扩展的类型。</param>
    /// <param name="typeSize">类型的总大小。</param>
    /// <exception cref="T:System.ArgumentException">未指定嵌套属性。- 或 -此类型是密封的。- 或 -此类型是数组。- 或 -此类型是接口，但嵌套类型不是接口。- 或 -<paramref name="name" /> 的长度为零或大于 1023。- 或 -此操作将在当前程序集中用重复的 <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> 创建类型。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    [SecuritySafeCritical]
    public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, int typeSize)
    {
      lock (this.SyncRoot)
        return this.DefineNestedTypeNoLock(name, attr, parent, (Type[]) null, PackingSize.Unspecified, typeSize);
    }

    /// <summary>已知嵌套类型的名称、属性、它扩展的类型和包装大小，定义嵌套类型。</summary>
    /// <returns>已定义的嵌套类型。</returns>
    /// <param name="name">类型的简称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="attr">该类型的属性。</param>
    /// <param name="parent">嵌套类型扩展的类型。</param>
    /// <param name="packSize">该类型的封装大小。</param>
    /// <exception cref="T:System.ArgumentException">未指定嵌套属性。- 或 -此类型是密封的。- 或 -此类型是数组。- 或 -此类型是接口，但嵌套类型不是接口。- 或 -<paramref name="name" /> 的长度为零或大于 1023。- 或 -此操作将在当前程序集中用重复的 <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> 创建类型。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    [SecuritySafeCritical]
    public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, PackingSize packSize)
    {
      lock (this.SyncRoot)
        return this.DefineNestedTypeNoLock(name, attr, parent, (Type[]) null, packSize, 0);
    }

    /// <summary>已知嵌套类型的名称、属性、尺寸和它扩展的类型，定义嵌套类型。</summary>
    /// <returns>已定义的嵌套类型。</returns>
    /// <param name="name">类型的简称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="attr">该类型的属性。</param>
    /// <param name="parent">嵌套类型扩展的类型。</param>
    /// <param name="packSize">该类型的封装大小。</param>
    /// <param name="typeSize">类型的总大小。</param>
    [SecuritySafeCritical]
    public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, PackingSize packSize, int typeSize)
    {
      lock (this.SyncRoot)
        return this.DefineNestedTypeNoLock(name, attr, parent, (Type[]) null, packSize, typeSize);
    }

    [SecurityCritical]
    private TypeBuilder DefineNestedTypeNoLock(string name, TypeAttributes attr, Type parent, Type[] interfaces, PackingSize packSize, int typeSize)
    {
      return new TypeBuilder(name, attr, parent, interfaces, this.m_module, packSize, typeSize, this);
    }

    /// <summary>用给定的名称、属性和字段类型，向类型中添加新字段。</summary>
    /// <returns>定义的字段。</returns>
    /// <param name="fieldName">字段名。<paramref name="fieldName" /> 不能包含嵌入的 null 值。</param>
    /// <param name="type">字段的类型</param>
    /// <param name="attributes">字段的属性。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="fieldName" /> 的长度为零。- 或 -<paramref name="type" /> 是 System.Void。- 或 -为该字段的父类指定了总大小。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="fieldName" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">该类型是以前用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建的。</exception>
    public FieldBuilder DefineField(string fieldName, Type type, FieldAttributes attributes)
    {
      return this.DefineField(fieldName, type, (Type[]) null, (Type[]) null, attributes);
    }

    /// <summary>用给定的名称、属性、字段类型和自定义修饰符，向类型中添加新字段。</summary>
    /// <returns>定义的字段。</returns>
    /// <param name="fieldName">字段名。<paramref name="fieldName" /> 不能包含嵌入的 null 值。</param>
    /// <param name="type">字段的类型</param>
    /// <param name="requiredCustomModifiers">一个表示字段所必需的自定义修饰符的类型数组，如 <see cref="T:Microsoft.VisualC.IsConstModifier" />。</param>
    /// <param name="optionalCustomModifiers">一个表示字段的可选自定义修饰符的类型数组，如 <see cref="T:Microsoft.VisualC.IsConstModifier" />。</param>
    /// <param name="attributes">字段的属性。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="fieldName" /> 的长度为零。- 或 -<paramref name="type" /> 是 System.Void。- 或 -为该字段的父类指定了总大小。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="fieldName" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">该类型是以前用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建的。</exception>
    [SecuritySafeCritical]
    public FieldBuilder DefineField(string fieldName, Type type, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers, FieldAttributes attributes)
    {
      lock (this.SyncRoot)
        return this.DefineFieldNoLock(fieldName, type, requiredCustomModifiers, optionalCustomModifiers, attributes);
    }

    [SecurityCritical]
    private FieldBuilder DefineFieldNoLock(string fieldName, Type type, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers, FieldAttributes attributes)
    {
      this.ThrowIfCreated();
      this.CheckContext(new Type[1]{ type });
      this.CheckContext(requiredCustomModifiers);
      if (this.m_enumUnderlyingType == (Type) null && this.IsEnum && (attributes & FieldAttributes.Static) == FieldAttributes.PrivateScope)
        this.m_enumUnderlyingType = type;
      return new FieldBuilder(this, fieldName, type, requiredCustomModifiers, optionalCustomModifiers, attributes);
    }

    /// <summary>在可移植可执行 (PE) 文件的 .sdata 部分定义初始化的数据字段。</summary>
    /// <returns>引用这些数据的字段。</returns>
    /// <param name="name">用于引用数据的名称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="data">数据的 Blob。</param>
    /// <param name="attributes">该字段的特性。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 的长度为零。- 或 -数据的大小小于等于 0，或者大于等于 0x3f0000。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 或 <paramref name="data" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">以前调用过 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />。</exception>
    [SecuritySafeCritical]
    public FieldBuilder DefineInitializedData(string name, byte[] data, FieldAttributes attributes)
    {
      lock (this.SyncRoot)
        return this.DefineInitializedDataNoLock(name, data, attributes);
    }

    [SecurityCritical]
    private FieldBuilder DefineInitializedDataNoLock(string name, byte[] data, FieldAttributes attributes)
    {
      if (data == null)
        throw new ArgumentNullException("data");
      string name1 = name;
      byte[] data1 = data;
      int length = data1.Length;
      int num = (int) attributes;
      return this.DefineDataHelper(name1, data1, length, (FieldAttributes) num);
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
    /// <exception cref="T:System.InvalidOperationException">该类型是以前用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建的。</exception>
    [SecuritySafeCritical]
    public FieldBuilder DefineUninitializedData(string name, int size, FieldAttributes attributes)
    {
      lock (this.SyncRoot)
        return this.DefineUninitializedDataNoLock(name, size, attributes);
    }

    [SecurityCritical]
    private FieldBuilder DefineUninitializedDataNoLock(string name, int size, FieldAttributes attributes)
    {
      return this.DefineDataHelper(name, (byte[]) null, size, attributes);
    }

    /// <summary>用给定的名称和属性签名，向类型中添加新属性。</summary>
    /// <returns>已定义的属性。</returns>
    /// <param name="name">属性的名称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="attributes">属性 (Property) 的属性 (Attribute)。</param>
    /// <param name="returnType">属性的返回类型。</param>
    /// <param name="parameterTypes">属性的参数类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 的长度为零。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。- 或 -<paramref name="parameterTypes" /> 数组中有任何元素为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">该类型是以前用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建的。</exception>
    public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, Type returnType, Type[] parameterTypes)
    {
      return this.DefineProperty(name, attributes, returnType, (Type[]) null, (Type[]) null, parameterTypes, (Type[][]) null, (Type[][]) null);
    }

    /// <summary>用给定的名称、特性、调用约定和属性签名，向类型中添加新属性。</summary>
    /// <returns>已定义的属性。</returns>
    /// <param name="name">属性的名称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="attributes">属性 (Property) 的属性 (Attribute)。</param>
    /// <param name="callingConvention">属性访问器的调用约定。</param>
    /// <param name="returnType">属性的返回类型。</param>
    /// <param name="parameterTypes">属性的参数类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 的长度为零。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。- 或 -<paramref name="parameterTypes" /> 数组中有任何元素为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">该类型是以前用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建的。</exception>
    public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
    {
      return this.DefineProperty(name, attributes, callingConvention, returnType, (Type[]) null, (Type[]) null, parameterTypes, (Type[][]) null, (Type[][]) null);
    }

    /// <summary>用给定的名称、属性签名和自定义修饰符，向类型中添加新属性。</summary>
    /// <returns>已定义的属性。</returns>
    /// <param name="name">属性的名称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="attributes">属性 (Property) 的属性 (Attribute)。</param>
    /// <param name="returnType">属性的返回类型。</param>
    /// <param name="returnTypeRequiredCustomModifiers">一个类型数组，表示属性的返回类型所必需的自定义修饰符，如 <see cref="T:System.Runtime.CompilerServices.IsConst" />。如果返回类型没有必需的自定义修饰符，请指定 null。</param>
    /// <param name="returnTypeOptionalCustomModifiers">一个类型数组，表示属性的返回类型的可选自定义修饰符，如 <see cref="T:System.Runtime.CompilerServices.IsConst" />。如果返回类型没有可选的自定义修饰符，请指定 null。</param>
    /// <param name="parameterTypes">属性的参数类型。</param>
    /// <param name="parameterTypeRequiredCustomModifiers">由类型数组组成的数组。每个类型数组均表示相应参数所必需的自定义修饰符，如 <see cref="T:System.Runtime.CompilerServices.IsConst" />。如果某个特定参数没有必需的自定义修饰符，请指定 null，而不指定类型数组。如果没有参数具有必需的自定义修饰符，请指定 null，而不指定由数组构成的数组。</param>
    /// <param name="parameterTypeOptionalCustomModifiers">由类型数组组成的数组。每个类型数组均表示相应参数的可选自定义修饰符，如 <see cref="T:System.Runtime.CompilerServices.IsConst" />。如果某个特定参数没有可选的自定义修饰符，请指定 null，而不指定类型数组。如果没有参数具有可选的自定义修饰符，请指定 null，而不指定由数组构成的数组。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 的长度为零。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null- 或 -<paramref name="parameterTypes" /> 数组中有任何元素为 null</exception>
    /// <exception cref="T:System.InvalidOperationException">该类型是以前用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建的。</exception>
    public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
    {
      return this.DefineProperty(name, attributes, (CallingConventions) 0, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
    }

    /// <summary>用给定的名称、调用约定、属性签名和自定义修饰符，向类型中添加新属性。</summary>
    /// <returns>已定义的属性。</returns>
    /// <param name="name">属性的名称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="attributes">属性 (Property) 的属性 (Attribute)。</param>
    /// <param name="callingConvention">属性访问器的调用约定。</param>
    /// <param name="returnType">属性的返回类型。</param>
    /// <param name="returnTypeRequiredCustomModifiers">一个类型数组，表示属性的返回类型所必需的自定义修饰符，如 <see cref="T:System.Runtime.CompilerServices.IsConst" />。如果返回类型没有必需的自定义修饰符，请指定 null。</param>
    /// <param name="returnTypeOptionalCustomModifiers">一个类型数组，表示属性的返回类型的可选自定义修饰符，如 <see cref="T:System.Runtime.CompilerServices.IsConst" />。如果返回类型没有可选的自定义修饰符，请指定 null。</param>
    /// <param name="parameterTypes">属性的参数类型。</param>
    /// <param name="parameterTypeRequiredCustomModifiers">由类型数组组成的数组。每个类型数组均表示相应参数所必需的自定义修饰符，如 <see cref="T:System.Runtime.CompilerServices.IsConst" />。如果某个特定参数没有必需的自定义修饰符，请指定 null，而不指定类型数组。如果没有参数具有必需的自定义修饰符，请指定 null，而不指定由数组构成的数组。</param>
    /// <param name="parameterTypeOptionalCustomModifiers">由类型数组组成的数组。每个类型数组均表示相应参数的可选自定义修饰符，如 <see cref="T:System.Runtime.CompilerServices.IsConst" />。如果某个特定参数没有可选的自定义修饰符，请指定 null，而不指定类型数组。如果没有参数具有可选的自定义修饰符，请指定 null，而不指定由数组构成的数组。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 的长度为零。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。- 或 -<paramref name="parameterTypes" /> 数组中有任何元素为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">该类型是以前用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建的。</exception>
    [SecuritySafeCritical]
    public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
    {
      lock (this.SyncRoot)
        return this.DefinePropertyNoLock(name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
    }

    [SecurityCritical]
    private PropertyBuilder DefinePropertyNoLock(string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
      this.CheckContext(new Type[1]{ returnType });
      this.CheckContext(returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes);
      this.CheckContext(parameterTypeRequiredCustomModifiers);
      this.CheckContext(parameterTypeOptionalCustomModifiers);
      this.ThrowIfCreated();
      SignatureHelper propertySigHelper = SignatureHelper.GetPropertySigHelper((Module) this.m_module, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
      int length;
      byte[] signature = propertySigHelper.InternalGetSignature(out length);
      PropertyToken prToken = new PropertyToken(TypeBuilder.DefineProperty(this.m_module.GetNativeHandle(), this.m_tdType.Token, name, attributes, signature, length));
      return new PropertyBuilder(this.m_module, name, propertySigHelper, attributes, returnType, prToken, this);
    }

    /// <summary>用给定的名称、属性和事件类型，向类型中添加新事件。</summary>
    /// <returns>已定义的事件。</returns>
    /// <param name="name">事件的名称。<paramref name="name" /> 不能包含嵌入的 null 值。</param>
    /// <param name="attributes">事件的属性。</param>
    /// <param name="eventtype">事件的类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 的长度为零。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。- 或 -<paramref name="eventtype" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">该类型是以前用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建的。</exception>
    [SecuritySafeCritical]
    public EventBuilder DefineEvent(string name, EventAttributes attributes, Type eventtype)
    {
      lock (this.SyncRoot)
        return this.DefineEventNoLock(name, attributes, eventtype);
    }

    [SecurityCritical]
    private EventBuilder DefineEventNoLock(string name, EventAttributes attributes, Type eventtype)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
      if ((int) name[0] == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_IllegalName"), "name");
      this.CheckContext(new Type[1]{ eventtype });
      this.ThrowIfCreated();
      int token = this.m_module.GetTypeTokenInternal(eventtype).Token;
      EventToken evToken = new EventToken(TypeBuilder.DefineEvent(this.m_module.GetNativeHandle(), this.m_tdType.Token, name, attributes, token));
      return new EventBuilder(this.m_module, name, attributes, this, evToken);
    }

    /// <summary>获取表示此类型的 <see cref="T:System.Reflection.TypeInfo" /> 对象。</summary>
    /// <returns>一个表示此类型的对象。</returns>
    [SecuritySafeCritical]
    public TypeInfo CreateTypeInfo()
    {
      lock (this.SyncRoot)
        return this.CreateTypeNoLock();
    }

    /// <summary>创建类的 <see cref="T:System.Type" /> 对象。定义了类的字段和方法后，调用 CreateType 以加载其 Type 对象。</summary>
    /// <returns>返回此类的新 <see cref="T:System.Type" /> 对象。</returns>
    /// <exception cref="T:System.InvalidOperationException">尚未创建封闭类型。- 或 -此类型是非抽象的并且包含抽象方法。- 或 -此类型不是抽象类或接口，并且包含不带方法体的方法。</exception>
    /// <exception cref="T:System.NotSupportedException">此类型包含无效的 Microsoft 中间语言 (MSIL) 代码。- 或 -分支目标是使用 1 字节的偏移量指定的，但是该目标与分支之间的距离大于 127 字节。</exception>
    /// <exception cref="T:System.TypeLoadException">无法加载该类型。例如，它包含具有调用约定 <see cref="F:System.Reflection.CallingConventions.HasThis" /> 的 static 方法。</exception>
    [SecuritySafeCritical]
    public Type CreateType()
    {
      lock (this.SyncRoot)
        return (Type) this.CreateTypeNoLock();
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
    private TypeInfo CreateTypeNoLock()
    {
      if (this.IsCreated())
        return (TypeInfo) this.m_bakedRuntimeType;
      this.ThrowIfCreated();
      if (this.m_typeInterfaces == null)
        this.m_typeInterfaces = new List<Type>();
      int[] numArray1 = new int[this.m_typeInterfaces.Count];
      TypeToken typeTokenInternal;
      for (int index1 = 0; index1 < this.m_typeInterfaces.Count; ++index1)
      {
        int[] numArray2 = numArray1;
        int index2 = index1;
        typeTokenInternal = this.m_module.GetTypeTokenInternal(this.m_typeInterfaces[index1]);
        int token = typeTokenInternal.Token;
        numArray2[index2] = token;
      }
      int tkParent = 0;
      if (this.m_typeParent != (Type) null)
      {
        typeTokenInternal = this.m_module.GetTypeTokenInternal(this.m_typeParent);
        tkParent = typeTokenInternal.Token;
      }
      if (this.IsGenericParameter)
      {
        int[] constraints;
        if (this.m_typeParent != (Type) null)
        {
          constraints = new int[this.m_typeInterfaces.Count + 2];
          int[] numArray2 = constraints;
          int index = numArray2.Length - 2;
          int num = tkParent;
          numArray2[index] = num;
        }
        else
          constraints = new int[this.m_typeInterfaces.Count + 1];
        for (int index1 = 0; index1 < this.m_typeInterfaces.Count; ++index1)
        {
          int[] numArray2 = constraints;
          int index2 = index1;
          typeTokenInternal = this.m_module.GetTypeTokenInternal(this.m_typeInterfaces[index1]);
          int token = typeTokenInternal.Token;
          numArray2[index2] = token;
        }
        this.m_tdType = new TypeToken(TypeBuilder.DefineGenericParam(this.m_module.GetNativeHandle(), this.m_strName, (MethodInfo) this.m_declMeth == (MethodInfo) null ? this.m_DeclaringType.m_tdType.Token : this.m_declMeth.GetToken().Token, this.m_genParamAttributes, this.m_genParamPos, constraints));
        if (this.m_ca != null)
        {
          foreach (TypeBuilder.CustAttr custAttr in this.m_ca)
            custAttr.Bake(this.m_module, this.MetadataTokenInternal);
        }
        this.m_hasBeenCreated = true;
        return (TypeInfo) this;
      }
      if ((this.m_tdType.Token & 16777215) != 0 && (tkParent & 16777215) != 0)
        TypeBuilder.SetParentType(this.m_module.GetNativeHandle(), this.m_tdType.Token, tkParent);
      if (this.m_inst != null)
      {
        foreach (Type type in this.m_inst)
        {
          if (type is GenericTypeParameterBuilder)
            ((GenericTypeParameterBuilder) type).m_type.CreateType();
        }
      }
      if (!this.m_isHiddenGlobalType && this.m_constructorCount == 0 && ((this.m_iAttr & TypeAttributes.ClassSemanticsMask) == TypeAttributes.NotPublic && !this.IsValueType) && (this.m_iAttr & (TypeAttributes.Abstract | TypeAttributes.Sealed)) != (TypeAttributes.Abstract | TypeAttributes.Sealed))
        this.DefineDefaultConstructor(MethodAttributes.Public);
      int count = this.m_listMethods.Count;
      for (int index = 0; index < count; ++index)
      {
        MethodBuilder methodBuilder1 = this.m_listMethods[index];
        if (methodBuilder1.IsGenericMethodDefinition)
          methodBuilder1.GetToken();
        MethodAttributes attributes = methodBuilder1.Attributes;
        if ((methodBuilder1.GetMethodImplementationFlags() & (MethodImplAttributes.CodeTypeMask | MethodImplAttributes.ManagedMask | MethodImplAttributes.PreserveSig)) == MethodImplAttributes.IL && (attributes & MethodAttributes.PinvokeImpl) == MethodAttributes.PrivateScope)
        {
          int signatureLength;
          byte[] localSignature = methodBuilder1.GetLocalSignature(out signatureLength);
          if ((attributes & MethodAttributes.Abstract) != MethodAttributes.PrivateScope && (this.m_iAttr & TypeAttributes.Abstract) == TypeAttributes.NotPublic)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadTypeAttributesNotAbstract"));
          byte[] body1 = methodBuilder1.GetBody();
          if ((attributes & MethodAttributes.Abstract) != MethodAttributes.PrivateScope)
          {
            if (body1 != null)
              throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadMethodBody"));
          }
          else if (body1 == null || body1.Length == 0)
          {
            if (methodBuilder1.m_ilGenerator != null)
            {
              MethodBuilder methodBuilder2 = methodBuilder1;
              ILGenerator ilGenerator = methodBuilder2.GetILGenerator();
              methodBuilder2.CreateMethodBodyHelper(ilGenerator);
            }
            body1 = methodBuilder1.GetBody();
            if ((body1 == null || body1.Length == 0) && !methodBuilder1.m_canBeRuntimeImpl)
              throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadEmptyMethodBody", (object) methodBuilder1.Name));
          }
          int maxStack = methodBuilder1.GetMaxStack();
          ExceptionHandler[] exceptionHandlers = methodBuilder1.GetExceptionHandlers();
          int[] tokenFixups1 = methodBuilder1.GetTokenFixups();
          RuntimeModule nativeHandle = this.m_module.GetNativeHandle();
          int token = methodBuilder1.GetToken().Token;
          int num = methodBuilder1.InitLocals ? 1 : 0;
          byte[] body2 = body1;
          int bodyLength = body2 != null ? body1.Length : 0;
          byte[] LocalSig = localSignature;
          int sigLength = signatureLength;
          int maxStackSize = maxStack;
          ExceptionHandler[] exceptions = exceptionHandlers;
          int numExceptions = exceptions != null ? exceptionHandlers.Length : 0;
          int[] tokenFixups2 = tokenFixups1;
          int numTokenFixups = tokenFixups2 != null ? tokenFixups1.Length : 0;
          TypeBuilder.SetMethodIL(nativeHandle, token, num != 0, body2, bodyLength, LocalSig, sigLength, maxStackSize, exceptions, numExceptions, tokenFixups2, numTokenFixups);
          if (this.m_module.ContainingAssemblyBuilder.m_assemblyData.m_access == AssemblyBuilderAccess.Run)
            methodBuilder1.ReleaseBakedStructures();
        }
      }
      this.m_hasBeenCreated = true;
      RuntimeType o = (RuntimeType) null;
      TypeBuilder.TermCreateClass(this.m_module.GetNativeHandle(), this.m_tdType.Token, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref o));
      if (this.m_isHiddenGlobalType)
        return (TypeInfo) null;
      this.m_bakedRuntimeType = o;
      if ((Type) this.m_DeclaringType != (Type) null && this.m_DeclaringType.m_bakedRuntimeType != (RuntimeType) null)
        this.m_DeclaringType.m_bakedRuntimeType.InvalidateCachedNestedType();
      return (TypeInfo) o;
    }

    /// <summary>为当前构造中的类型设置基类型。</summary>
    /// <param name="parent">新的基类型。</param>
    /// <exception cref="T:System.InvalidOperationException">该类型是以前用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建的。- 或 -<paramref name="parent" /> 为 null，当前的实例表示一个接口，该接口的属性不包括 <see cref="F:System.Reflection.TypeAttributes.Abstract" />。- 或 -对于当前动态类型，<see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> 属性为 true，而 <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> 属性为 false。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="parent" /> 是一个接口。此异常条件是 .NET Framework 2.0 版中新增的。</exception>
    public void SetParent(Type parent)
    {
      this.ThrowIfCreated();
      if (parent != (Type) null)
      {
        this.CheckContext(new Type[1]{ parent });
        if (parent.IsInterface)
          throw new ArgumentException(Environment.GetResourceString("Argument_CannotSetParentToInterface"));
        this.m_typeParent = parent;
      }
      else if ((this.m_iAttr & TypeAttributes.ClassSemanticsMask) != TypeAttributes.ClassSemanticsMask)
      {
        this.m_typeParent = typeof (object);
      }
      else
      {
        if ((this.m_iAttr & TypeAttributes.Abstract) == TypeAttributes.NotPublic)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadInterfaceNotAbstract"));
        this.m_typeParent = (Type) null;
      }
    }

    /// <summary>添加此类型实现的接口。</summary>
    /// <param name="interfaceType">此类型实现的接口。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="interfaceType" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">该类型是以前用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建的。</exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public void AddInterfaceImplementation(Type interfaceType)
    {
      if (interfaceType == (Type) null)
        throw new ArgumentNullException("interfaceType");
      this.CheckContext(new Type[1]{ interfaceType });
      this.ThrowIfCreated();
      TypeBuilder.AddInterfaceImpl(this.m_module.GetNativeHandle(), this.m_tdType.Token, this.m_module.GetTypeTokenInternal(interfaceType).Token);
      this.m_typeInterfaces.Add(interfaceType);
    }

    /// <summary>为此类型添加声明性安全。</summary>
    /// <param name="action">要执行的安全操作，如 Demand、Assert 等。</param>
    /// <param name="pset">操作应用于的权限集。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="action" /> 无效（RequestMinimum、RequestOptional 和 RequestRefuse 无效）。</exception>
    /// <exception cref="T:System.InvalidOperationException">已经使用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建了该包含类型。- 或 -权限集 <paramref name="pset" /> 包含以前由 AddDeclarativeSecurity 添加的操作。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pset" /> 为 null。</exception>
    [SecuritySafeCritical]
    public void AddDeclarativeSecurity(SecurityAction action, PermissionSet pset)
    {
      lock (this.SyncRoot)
        this.AddDeclarativeSecurityNoLock(action, pset);
    }

    [SecurityCritical]
    private void AddDeclarativeSecurityNoLock(SecurityAction action, PermissionSet pset)
    {
      if (pset == null)
        throw new ArgumentNullException("pset");
      if (!Enum.IsDefined(typeof (SecurityAction), (object) action) || action == SecurityAction.RequestMinimum || (action == SecurityAction.RequestOptional || action == SecurityAction.RequestRefuse))
        throw new ArgumentOutOfRangeException("action");
      this.ThrowIfCreated();
      byte[] blob = (byte[]) null;
      int cb = 0;
      if (!pset.IsEmpty())
      {
        blob = pset.EncodeXml();
        cb = blob.Length;
      }
      TypeBuilder.AddDeclarativeSecurity(this.m_module.GetNativeHandle(), this.m_tdType.Token, action, blob, cb);
    }

    /// <summary>使用指定的自定义属性 Blob 设置自定义属性。</summary>
    /// <param name="con">自定义属性的构造函数。</param>
    /// <param name="binaryAttribute">表示属性的字节 Blob。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="con" /> 或 <paramref name="binaryAttribute" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">对于当前动态类型，<see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> 属性为 true，而 <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> 属性为 false。</exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
    {
      if (con == (ConstructorInfo) null)
        throw new ArgumentNullException("con");
      if (binaryAttribute == null)
        throw new ArgumentNullException("binaryAttribute");
      TypeBuilder.DefineCustomAttribute(this.m_module, this.m_tdType.Token, this.m_module.GetConstructorToken(con).Token, binaryAttribute, false, false);
    }

    /// <summary>使用自定义属性生成器设置自定义属性。</summary>
    /// <param name="customBuilder">定义自定义属性的帮助器类的实例。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="customBuilder" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">对于当前动态类型，<see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> 属性为 true，而 <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> 属性为 false。</exception>
    [SecuritySafeCritical]
    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
      if (customBuilder == null)
        throw new ArgumentNullException("customBuilder");
      customBuilder.CreateCustomAttribute(this.m_module, this.m_tdType.Token);
    }

    void _TypeBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _TypeBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _TypeBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _TypeBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }

    private class CustAttr
    {
      private ConstructorInfo m_con;
      private byte[] m_binaryAttribute;
      private CustomAttributeBuilder m_customBuilder;

      public CustAttr(ConstructorInfo con, byte[] binaryAttribute)
      {
        if (con == (ConstructorInfo) null)
          throw new ArgumentNullException("con");
        if (binaryAttribute == null)
          throw new ArgumentNullException("binaryAttribute");
        this.m_con = con;
        this.m_binaryAttribute = binaryAttribute;
      }

      public CustAttr(CustomAttributeBuilder customBuilder)
      {
        if (customBuilder == null)
          throw new ArgumentNullException("customBuilder");
        this.m_customBuilder = customBuilder;
      }

      [SecurityCritical]
      public void Bake(ModuleBuilder module, int token)
      {
        if (this.m_customBuilder == null)
          TypeBuilder.DefineCustomAttribute(module, token, module.GetConstructorToken(this.m_con).Token, this.m_binaryAttribute, false, false);
        else
          this.m_customBuilder.CreateCustomAttribute(module, token);
      }
    }
  }
}
