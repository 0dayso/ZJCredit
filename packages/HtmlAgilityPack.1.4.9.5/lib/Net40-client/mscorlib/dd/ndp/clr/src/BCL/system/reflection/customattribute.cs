// Decompiled with JetBrains decompiler
// Type: System.Reflection.CustomAttributeData
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
  /// <summary>提供对加载到只反射上下文中的程序集、模块、类型、成员和参数的自定义特性数据的访问。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class CustomAttributeData
  {
    private ConstructorInfo m_ctor;
    private RuntimeModule m_scope;
    private MemberInfo[] m_members;
    private CustomAttributeCtorParameter[] m_ctorParams;
    private CustomAttributeNamedParameter[] m_namedParams;
    private IList<CustomAttributeTypedArgument> m_typedCtorArgs;
    private IList<CustomAttributeNamedArgument> m_namedArgs;

    /// <summary>键入该特性的类型。</summary>
    /// <returns>属性的类型。</returns>
    [__DynamicallyInvokable]
    public Type AttributeType
    {
      [__DynamicallyInvokable] get
      {
        return this.Constructor.DeclaringType;
      }
    }

    /// <summary>获取一个 <see cref="T:System.Reflection.ConstructorInfo" /> 对象，该对象表示可能已初始化自定义特性的构造函数。</summary>
    /// <returns>一个表示构造函数的对象，该构造函数可能已初始化 <see cref="T:System.Reflection.CustomAttributeData" /> 类的当前实例所表示的自定义特性。</returns>
    [ComVisible(true)]
    public virtual ConstructorInfo Constructor
    {
      get
      {
        return this.m_ctor;
      }
    }

    /// <summary>获取为由 <see cref="T:System.Reflection.CustomAttributeData" /> 对象表示的特性实例指定的位置参数列表。</summary>
    /// <returns>一个结构的集合，表示为自定义特性实例指定的位置参数。</returns>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public virtual IList<CustomAttributeTypedArgument> ConstructorArguments
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_typedCtorArgs == null)
        {
          CustomAttributeTypedArgument[] array = new CustomAttributeTypedArgument[this.m_ctorParams.Length];
          for (int index = 0; index < array.Length; ++index)
          {
            CustomAttributeEncodedArgument attributeEncodedArgument = this.m_ctorParams[index].CustomAttributeEncodedArgument;
            array[index] = new CustomAttributeTypedArgument(this.m_scope, this.m_ctorParams[index].CustomAttributeEncodedArgument);
          }
          this.m_typedCtorArgs = (IList<CustomAttributeTypedArgument>) Array.AsReadOnly<CustomAttributeTypedArgument>(array);
        }
        return this.m_typedCtorArgs;
      }
    }

    /// <summary>获取为由 <see cref="T:System.Reflection.CustomAttributeData" /> 对象表示的特性实例指定的命名参数列表。</summary>
    /// <returns>一个结构的集合，表示为自定义特性实例指定的命名参数。</returns>
    [__DynamicallyInvokable]
    public virtual IList<CustomAttributeNamedArgument> NamedArguments
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_namedArgs == null)
        {
          if (this.m_namedParams == null)
            return (IList<CustomAttributeNamedArgument>) null;
          int length = 0;
          CustomAttributeType customAttributeType;
          for (int index = 0; index < this.m_namedParams.Length; ++index)
          {
            customAttributeType = this.m_namedParams[index].EncodedArgument.CustomAttributeType;
            if (customAttributeType.EncodedType != CustomAttributeEncoding.Undefined)
              ++length;
          }
          CustomAttributeNamedArgument[] array = new CustomAttributeNamedArgument[length];
          int index1 = 0;
          int num = 0;
          for (; index1 < this.m_namedParams.Length; ++index1)
          {
            customAttributeType = this.m_namedParams[index1].EncodedArgument.CustomAttributeType;
            if (customAttributeType.EncodedType != CustomAttributeEncoding.Undefined)
              array[num++] = new CustomAttributeNamedArgument(this.m_members[index1], new CustomAttributeTypedArgument(this.m_scope, this.m_namedParams[index1].EncodedArgument));
          }
          this.m_namedArgs = (IList<CustomAttributeNamedArgument>) Array.AsReadOnly<CustomAttributeNamedArgument>(array);
        }
        return this.m_namedArgs;
      }
    }

    /// <summary>初始化 <see cref="T:System.Reflection.CustomAttributeData" /> 类的新实例。</summary>
    protected CustomAttributeData()
    {
    }

    [SecuritySafeCritical]
    private CustomAttributeData(RuntimeModule scope, CustomAttributeRecord caRecord)
    {
      this.m_scope = scope;
      this.m_ctor = (ConstructorInfo) RuntimeType.GetMethodBase(scope, (int) caRecord.tkCtor);
      ParameterInfo[] parametersNoCopy = this.m_ctor.GetParametersNoCopy();
      this.m_ctorParams = new CustomAttributeCtorParameter[parametersNoCopy.Length];
      for (int index = 0; index < parametersNoCopy.Length; ++index)
        this.m_ctorParams[index] = new CustomAttributeCtorParameter(CustomAttributeData.InitCustomAttributeType((RuntimeType) parametersNoCopy[index].ParameterType));
      FieldInfo[] fields = this.m_ctor.DeclaringType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      PropertyInfo[] properties = this.m_ctor.DeclaringType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      this.m_namedParams = new CustomAttributeNamedParameter[properties.Length + fields.Length];
      for (int index = 0; index < fields.Length; ++index)
        this.m_namedParams[index] = new CustomAttributeNamedParameter(fields[index].Name, CustomAttributeEncoding.Field, CustomAttributeData.InitCustomAttributeType((RuntimeType) fields[index].FieldType));
      for (int index = 0; index < properties.Length; ++index)
        this.m_namedParams[index + fields.Length] = new CustomAttributeNamedParameter(properties[index].Name, CustomAttributeEncoding.Property, CustomAttributeData.InitCustomAttributeType((RuntimeType) properties[index].PropertyType));
      this.m_members = new MemberInfo[fields.Length + properties.Length];
      fields.CopyTo((Array) this.m_members, 0);
      properties.CopyTo((Array) this.m_members, fields.Length);
      CustomAttributeEncodedArgument.ParseAttributeArguments(caRecord.blob, ref this.m_ctorParams, ref this.m_namedParams, this.m_scope);
    }

    internal CustomAttributeData(Attribute attribute)
    {
      if (attribute is DllImportAttribute)
        this.Init((DllImportAttribute) attribute);
      else if (attribute is FieldOffsetAttribute)
        this.Init((FieldOffsetAttribute) attribute);
      else if (attribute is MarshalAsAttribute)
        this.Init((MarshalAsAttribute) attribute);
      else if (attribute is TypeForwardedToAttribute)
        this.Init((TypeForwardedToAttribute) attribute);
      else
        this.Init((object) attribute);
    }

    /// <summary>返回一个 <see cref="T:System.Reflection.CustomAttributeData" /> 对象列表，这些对象表示有关已应用于目标成员的特性的数据。</summary>
    /// <returns>一个对象列表，表示有关已应用于目标成员的特性的数据。</returns>
    /// <param name="target">特性数据待检索的成员。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="target" /> 为 null。</exception>
    public static IList<CustomAttributeData> GetCustomAttributes(MemberInfo target)
    {
      if (target == (MemberInfo) null)
        throw new ArgumentNullException("target");
      return target.GetCustomAttributesData();
    }

    /// <summary>返回一个 <see cref="T:System.Reflection.CustomAttributeData" /> 对象列表，这些对象表示有关已应用于目标模块的特性的数据。</summary>
    /// <returns>一个对象列表，表示有关已应用于目标模块的特性的数据。</returns>
    /// <param name="target">自定义特性数据待检索的模块。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="target" /> 为 null。</exception>
    public static IList<CustomAttributeData> GetCustomAttributes(Module target)
    {
      if (target == (Module) null)
        throw new ArgumentNullException("target");
      return target.GetCustomAttributesData();
    }

    /// <summary>返回一个 <see cref="T:System.Reflection.CustomAttributeData" /> 对象列表，这些对象表示有关已应用于目标程序集的特性的数据。</summary>
    /// <returns>一个对象列表，表示有关已应用于目标程序集的特性的数据。</returns>
    /// <param name="target">自定义特性数据待检索的程序集。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="target" /> 为 null。</exception>
    public static IList<CustomAttributeData> GetCustomAttributes(Assembly target)
    {
      if (target == (Assembly) null)
        throw new ArgumentNullException("target");
      return target.GetCustomAttributesData();
    }

    /// <summary>返回一个 <see cref="T:System.Reflection.CustomAttributeData" /> 对象列表，这些对象表示有关已应用于目标参数的特性的数据。</summary>
    /// <returns>一个对象列表，表示有关已应用于目标参数的特性的数据。</returns>
    /// <param name="target">特性数据待检索的参数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="target" /> 为 null。</exception>
    public static IList<CustomAttributeData> GetCustomAttributes(ParameterInfo target)
    {
      if (target == null)
        throw new ArgumentNullException("target");
      return target.GetCustomAttributesData();
    }

    [SecuritySafeCritical]
    internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeType target)
    {
      IList<CustomAttributeData> customAttributes1 = CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
      int count = 0;
      Attribute[] customAttributes2 = PseudoCustomAttribute.GetCustomAttributes(target, typeof (object) as RuntimeType, true, out count);
      if (count == 0)
        return customAttributes1;
      CustomAttributeData[] array = new CustomAttributeData[customAttributes1.Count + count];
      customAttributes1.CopyTo(array, count);
      for (int index = 0; index < count; ++index)
        array[index] = new CustomAttributeData(customAttributes2[index]);
      return (IList<CustomAttributeData>) Array.AsReadOnly<CustomAttributeData>(array);
    }

    [SecuritySafeCritical]
    internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeFieldInfo target)
    {
      IList<CustomAttributeData> customAttributes1 = CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
      int count = 0;
      Attribute[] customAttributes2 = PseudoCustomAttribute.GetCustomAttributes(target, typeof (object) as RuntimeType, out count);
      if (count == 0)
        return customAttributes1;
      CustomAttributeData[] array = new CustomAttributeData[customAttributes1.Count + count];
      customAttributes1.CopyTo(array, count);
      for (int index = 0; index < count; ++index)
        array[index] = new CustomAttributeData(customAttributes2[index]);
      return (IList<CustomAttributeData>) Array.AsReadOnly<CustomAttributeData>(array);
    }

    [SecuritySafeCritical]
    internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeMethodInfo target)
    {
      IList<CustomAttributeData> customAttributes1 = CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
      int count = 0;
      Attribute[] customAttributes2 = PseudoCustomAttribute.GetCustomAttributes(target, typeof (object) as RuntimeType, true, out count);
      if (count == 0)
        return customAttributes1;
      CustomAttributeData[] array = new CustomAttributeData[customAttributes1.Count + count];
      customAttributes1.CopyTo(array, count);
      for (int index = 0; index < count; ++index)
        array[index] = new CustomAttributeData(customAttributes2[index]);
      return (IList<CustomAttributeData>) Array.AsReadOnly<CustomAttributeData>(array);
    }

    [SecuritySafeCritical]
    internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeConstructorInfo target)
    {
      return CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
    }

    [SecuritySafeCritical]
    internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeEventInfo target)
    {
      return CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
    }

    [SecuritySafeCritical]
    internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimePropertyInfo target)
    {
      return CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
    }

    [SecuritySafeCritical]
    internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeModule target)
    {
      if (target.IsResource())
        return (IList<CustomAttributeData>) new List<CustomAttributeData>();
      RuntimeModule module = target;
      int metadataToken = module.MetadataToken;
      return CustomAttributeData.GetCustomAttributes(module, metadataToken);
    }

    [SecuritySafeCritical]
    internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeAssembly target)
    {
      IList<CustomAttributeData> customAttributes1 = CustomAttributeData.GetCustomAttributes((RuntimeModule) target.ManifestModule, RuntimeAssembly.GetToken(target.GetNativeHandle()));
      int count = 0;
      Attribute[] customAttributes2 = PseudoCustomAttribute.GetCustomAttributes(target, typeof (object) as RuntimeType, false, out count);
      if (count == 0)
        return customAttributes1;
      CustomAttributeData[] array = new CustomAttributeData[customAttributes1.Count + count];
      customAttributes1.CopyTo(array, count);
      for (int index = 0; index < count; ++index)
        array[index] = new CustomAttributeData(customAttributes2[index]);
      return (IList<CustomAttributeData>) Array.AsReadOnly<CustomAttributeData>(array);
    }

    [SecuritySafeCritical]
    internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeParameterInfo target)
    {
      IList<CustomAttributeData> customAttributes1 = CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
      int count = 0;
      Attribute[] customAttributes2 = PseudoCustomAttribute.GetCustomAttributes(target, typeof (object) as RuntimeType, out count);
      if (count == 0)
        return customAttributes1;
      CustomAttributeData[] array = new CustomAttributeData[customAttributes1.Count + count];
      customAttributes1.CopyTo(array, count);
      for (int index = 0; index < count; ++index)
        array[index] = new CustomAttributeData(customAttributes2[index]);
      return (IList<CustomAttributeData>) Array.AsReadOnly<CustomAttributeData>(array);
    }

    private static CustomAttributeEncoding TypeToCustomAttributeEncoding(RuntimeType type)
    {
      if (type == (RuntimeType) typeof (int))
        return CustomAttributeEncoding.Int32;
      if (type.IsEnum)
        return CustomAttributeEncoding.Enum;
      if (type == (RuntimeType) typeof (string))
        return CustomAttributeEncoding.String;
      if (type == (RuntimeType) typeof (Type))
        return CustomAttributeEncoding.Type;
      if (type == (RuntimeType) typeof (object))
        return CustomAttributeEncoding.Object;
      if (type.IsArray)
        return CustomAttributeEncoding.Array;
      if (type == (RuntimeType) typeof (char))
        return CustomAttributeEncoding.Char;
      if (type == (RuntimeType) typeof (bool))
        return CustomAttributeEncoding.Boolean;
      if (type == (RuntimeType) typeof (byte))
        return CustomAttributeEncoding.Byte;
      if (type == (RuntimeType) typeof (sbyte))
        return CustomAttributeEncoding.SByte;
      if (type == (RuntimeType) typeof (short))
        return CustomAttributeEncoding.Int16;
      if (type == (RuntimeType) typeof (ushort))
        return CustomAttributeEncoding.UInt16;
      if (type == (RuntimeType) typeof (uint))
        return CustomAttributeEncoding.UInt32;
      if (type == (RuntimeType) typeof (long))
        return CustomAttributeEncoding.Int64;
      if (type == (RuntimeType) typeof (ulong))
        return CustomAttributeEncoding.UInt64;
      if (type == (RuntimeType) typeof (float))
        return CustomAttributeEncoding.Float;
      if (type == (RuntimeType) typeof (double))
        return CustomAttributeEncoding.Double;
      if (type == (RuntimeType) typeof (Enum) || type.IsClass || type.IsInterface)
        return CustomAttributeEncoding.Object;
      if (type.IsValueType)
        return CustomAttributeEncoding.Undefined;
      throw new ArgumentException(Environment.GetResourceString("Argument_InvalidKindOfTypeForCA"), "type");
    }

    private static CustomAttributeType InitCustomAttributeType(RuntimeType parameterType)
    {
      int num1 = (int) CustomAttributeData.TypeToCustomAttributeEncoding(parameterType);
      CustomAttributeEncoding attributeEncoding1 = CustomAttributeEncoding.Undefined;
      CustomAttributeEncoding attributeEncoding2 = CustomAttributeEncoding.Undefined;
      string str = (string) null;
      int num2 = 29;
      if (num1 == num2)
      {
        parameterType = (RuntimeType) parameterType.GetElementType();
        attributeEncoding1 = CustomAttributeData.TypeToCustomAttributeEncoding(parameterType);
      }
      int num3 = 85;
      if (num1 == num3 || attributeEncoding1 == CustomAttributeEncoding.Enum)
      {
        attributeEncoding2 = CustomAttributeData.TypeToCustomAttributeEncoding((RuntimeType) Enum.GetUnderlyingType((Type) parameterType));
        str = parameterType.AssemblyQualifiedName;
      }
      int num4 = (int) attributeEncoding1;
      int num5 = (int) attributeEncoding2;
      string enumName = str;
      return new CustomAttributeType((CustomAttributeEncoding) num1, (CustomAttributeEncoding) num4, (CustomAttributeEncoding) num5, enumName);
    }

    [SecurityCritical]
    private static IList<CustomAttributeData> GetCustomAttributes(RuntimeModule module, int tkTarget)
    {
      CustomAttributeRecord[] attributeRecords = CustomAttributeData.GetCustomAttributeRecords(module, tkTarget);
      CustomAttributeData[] array = new CustomAttributeData[attributeRecords.Length];
      for (int index = 0; index < attributeRecords.Length; ++index)
        array[index] = new CustomAttributeData(module, attributeRecords[index]);
      return (IList<CustomAttributeData>) Array.AsReadOnly<CustomAttributeData>(array);
    }

    [SecurityCritical]
    internal static CustomAttributeRecord[] GetCustomAttributeRecords(RuntimeModule module, int targetToken)
    {
      MetadataImport metadataImport = module.MetadataImport;
      MetadataEnumResult result;
      metadataImport.EnumCustomAttributes(targetToken, out result);
      CustomAttributeRecord[] customAttributeRecordArray = new CustomAttributeRecord[result.Length];
      for (int index = 0; index < customAttributeRecordArray.Length; ++index)
        metadataImport.GetCustomAttributeProps(result[index], out customAttributeRecordArray[index].tkCtor.Value, out customAttributeRecordArray[index].blob);
      return customAttributeRecordArray;
    }

    internal static CustomAttributeTypedArgument Filter(IList<CustomAttributeData> attrs, Type caType, int parameter)
    {
      for (int index = 0; index < attrs.Count; ++index)
      {
        if (attrs[index].Constructor.DeclaringType == caType)
          return attrs[index].ConstructorArguments[parameter];
      }
      return new CustomAttributeTypedArgument();
    }

    private void Init(DllImportAttribute dllImport)
    {
      Type type = typeof (DllImportAttribute);
      this.m_ctor = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
      this.m_typedCtorArgs = (IList<CustomAttributeTypedArgument>) Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[1]
      {
        new CustomAttributeTypedArgument((object) dllImport.Value)
      });
      this.m_namedArgs = (IList<CustomAttributeNamedArgument>) Array.AsReadOnly<CustomAttributeNamedArgument>(new CustomAttributeNamedArgument[8]
      {
        new CustomAttributeNamedArgument((MemberInfo) type.GetField("EntryPoint"), (object) dllImport.EntryPoint),
        new CustomAttributeNamedArgument((MemberInfo) type.GetField("CharSet"), (object) dllImport.CharSet),
        new CustomAttributeNamedArgument((MemberInfo) type.GetField("ExactSpelling"), (object) dllImport.ExactSpelling),
        new CustomAttributeNamedArgument((MemberInfo) type.GetField("SetLastError"), (object) dllImport.SetLastError),
        new CustomAttributeNamedArgument((MemberInfo) type.GetField("PreserveSig"), (object) dllImport.PreserveSig),
        new CustomAttributeNamedArgument((MemberInfo) type.GetField("CallingConvention"), (object) dllImport.CallingConvention),
        new CustomAttributeNamedArgument((MemberInfo) type.GetField("BestFitMapping"), (object) dllImport.BestFitMapping),
        new CustomAttributeNamedArgument((MemberInfo) type.GetField("ThrowOnUnmappableChar"), (object) dllImport.ThrowOnUnmappableChar)
      });
    }

    private void Init(FieldOffsetAttribute fieldOffset)
    {
      this.m_ctor = typeof (FieldOffsetAttribute).GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
      this.m_typedCtorArgs = (IList<CustomAttributeTypedArgument>) Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[1]
      {
        new CustomAttributeTypedArgument((object) fieldOffset.Value)
      });
      this.m_namedArgs = (IList<CustomAttributeNamedArgument>) Array.AsReadOnly<CustomAttributeNamedArgument>(new CustomAttributeNamedArgument[0]);
    }

    private void Init(MarshalAsAttribute marshalAs)
    {
      Type type = typeof (MarshalAsAttribute);
      this.m_ctor = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
      this.m_typedCtorArgs = (IList<CustomAttributeTypedArgument>) Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[1]
      {
        new CustomAttributeTypedArgument((object) marshalAs.Value)
      });
      int num1 = 3;
      if (marshalAs.MarshalType != null)
        ++num1;
      if (marshalAs.MarshalTypeRef != (Type) null)
        ++num1;
      if (marshalAs.MarshalCookie != null)
        ++num1;
      int length = num1 + 1 + 1;
      if (marshalAs.SafeArrayUserDefinedSubType != (Type) null)
        ++length;
      CustomAttributeNamedArgument[] array = new CustomAttributeNamedArgument[length];
      int num2 = 0;
      CustomAttributeNamedArgument[] attributeNamedArgumentArray1 = array;
      int index1 = num2;
      int num3 = 1;
      int num4 = index1 + num3;
      CustomAttributeNamedArgument attributeNamedArgument1 = new CustomAttributeNamedArgument((MemberInfo) type.GetField("ArraySubType"), (object) marshalAs.ArraySubType);
      attributeNamedArgumentArray1[index1] = attributeNamedArgument1;
      CustomAttributeNamedArgument[] attributeNamedArgumentArray2 = array;
      int index2 = num4;
      int num5 = 1;
      int num6 = index2 + num5;
      CustomAttributeNamedArgument attributeNamedArgument2 = new CustomAttributeNamedArgument((MemberInfo) type.GetField("SizeParamIndex"), (object) marshalAs.SizeParamIndex);
      attributeNamedArgumentArray2[index2] = attributeNamedArgument2;
      CustomAttributeNamedArgument[] attributeNamedArgumentArray3 = array;
      int index3 = num6;
      int num7 = 1;
      int num8 = index3 + num7;
      CustomAttributeNamedArgument attributeNamedArgument3 = new CustomAttributeNamedArgument((MemberInfo) type.GetField("SizeConst"), (object) marshalAs.SizeConst);
      attributeNamedArgumentArray3[index3] = attributeNamedArgument3;
      CustomAttributeNamedArgument[] attributeNamedArgumentArray4 = array;
      int index4 = num8;
      int num9 = 1;
      int num10 = index4 + num9;
      CustomAttributeNamedArgument attributeNamedArgument4 = new CustomAttributeNamedArgument((MemberInfo) type.GetField("IidParameterIndex"), (object) marshalAs.IidParameterIndex);
      attributeNamedArgumentArray4[index4] = attributeNamedArgument4;
      CustomAttributeNamedArgument[] attributeNamedArgumentArray5 = array;
      int index5 = num10;
      int num11 = 1;
      int num12 = index5 + num11;
      CustomAttributeNamedArgument attributeNamedArgument5 = new CustomAttributeNamedArgument((MemberInfo) type.GetField("SafeArraySubType"), (object) marshalAs.SafeArraySubType);
      attributeNamedArgumentArray5[index5] = attributeNamedArgument5;
      if (marshalAs.MarshalType != null)
        array[num12++] = new CustomAttributeNamedArgument((MemberInfo) type.GetField("MarshalType"), (object) marshalAs.MarshalType);
      if (marshalAs.MarshalTypeRef != (Type) null)
        array[num12++] = new CustomAttributeNamedArgument((MemberInfo) type.GetField("MarshalTypeRef"), (object) marshalAs.MarshalTypeRef);
      if (marshalAs.MarshalCookie != null)
        array[num12++] = new CustomAttributeNamedArgument((MemberInfo) type.GetField("MarshalCookie"), (object) marshalAs.MarshalCookie);
      if (marshalAs.SafeArrayUserDefinedSubType != (Type) null)
      {
        CustomAttributeNamedArgument[] attributeNamedArgumentArray6 = array;
        int index6 = num12;
        int num13 = 1;
        int num14 = index6 + num13;
        CustomAttributeNamedArgument attributeNamedArgument6 = new CustomAttributeNamedArgument((MemberInfo) type.GetField("SafeArrayUserDefinedSubType"), (object) marshalAs.SafeArrayUserDefinedSubType);
        attributeNamedArgumentArray6[index6] = attributeNamedArgument6;
      }
      this.m_namedArgs = (IList<CustomAttributeNamedArgument>) Array.AsReadOnly<CustomAttributeNamedArgument>(array);
    }

    private void Init(TypeForwardedToAttribute forwardedTo)
    {
      this.m_ctor = typeof (TypeForwardedToAttribute).GetConstructor(BindingFlags.Instance | BindingFlags.Public, (Binder) null, new Type[1]{ typeof (Type) }, (ParameterModifier[]) null);
      this.m_typedCtorArgs = (IList<CustomAttributeTypedArgument>) Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[1]
      {
        new CustomAttributeTypedArgument(typeof (Type), (object) forwardedTo.Destination)
      });
      this.m_namedArgs = (IList<CustomAttributeNamedArgument>) Array.AsReadOnly<CustomAttributeNamedArgument>(new CustomAttributeNamedArgument[0]);
    }

    private void Init(object pca)
    {
      this.m_ctor = pca.GetType().GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
      this.m_typedCtorArgs = (IList<CustomAttributeTypedArgument>) Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[0]);
      this.m_namedArgs = (IList<CustomAttributeNamedArgument>) Array.AsReadOnly<CustomAttributeNamedArgument>(new CustomAttributeNamedArgument[0]);
    }

    /// <summary>返回自定义特性的字符串表示形式。</summary>
    /// <returns>一个表示自定义特性的字符串值。</returns>
    public override string ToString()
    {
      string str1 = "";
      for (int index = 0; index < this.ConstructorArguments.Count; ++index)
        str1 += string.Format((IFormatProvider) CultureInfo.CurrentCulture, index == 0 ? "{0}" : ", {0}", (object) this.ConstructorArguments[index]);
      string str2 = "";
      for (int index = 0; index < this.NamedArguments.Count; ++index)
        str2 += string.Format((IFormatProvider) CultureInfo.CurrentCulture, index != 0 || str1.Length != 0 ? ", {0}" : "{0}", (object) this.NamedArguments[index]);
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "[{0}({1}{2})]", (object) this.Constructor.DeclaringType.FullName, (object) str1, (object) str2);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    /// <summary>返回一个值，该值指示此实例是否与指定的对象相等。</summary>
    /// <returns>如果 <paramref name="obj" /> 等于当前的实例，则为 true；否则为 false。</returns>
    /// <param name="obj">与此实例进行比较的 object，或 null。</param>
    public override bool Equals(object obj)
    {
      return obj == this;
    }
  }
}
