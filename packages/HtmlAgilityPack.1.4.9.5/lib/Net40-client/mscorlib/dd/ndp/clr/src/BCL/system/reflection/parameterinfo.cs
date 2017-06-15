// Decompiled with JetBrains decompiler
// Type: System.Reflection.ParameterInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
  /// <summary>发现参数属性并提供对参数元数据的访问。</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_ParameterInfo))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class ParameterInfo : _ParameterInfo, ICustomAttributeProvider, IObjectReference
  {
    /// <summary>参数名。</summary>
    protected string NameImpl;
    /// <summary>参数的 Type。</summary>
    protected Type ClassImpl;
    /// <summary>参数列表中参数从零开始的位置。</summary>
    protected int PositionImpl;
    /// <summary>参数的属性。</summary>
    protected ParameterAttributes AttrsImpl;
    /// <summary>参数的默认值。</summary>
    protected object DefaultValueImpl;
    /// <summary>在其中实现该字段的成员。</summary>
    protected MemberInfo MemberImpl;
    [OptionalField]
    private IntPtr _importer;
    [OptionalField]
    private int _token;
    [OptionalField]
    private bool bExtraConstChecked;

    /// <summary>获取该参数的 Type。</summary>
    /// <returns>表示该参数 Type 的 Type 对象。</returns>
    [__DynamicallyInvokable]
    public virtual Type ParameterType
    {
      [__DynamicallyInvokable] get
      {
        return this.ClassImpl;
      }
    }

    /// <summary>获取参数名。</summary>
    /// <returns>此参数的简单名称。</returns>
    [__DynamicallyInvokable]
    public virtual string Name
    {
      [__DynamicallyInvokable] get
      {
        return this.NameImpl;
      }
    }

    /// <summary>获取一个值，指示此参数是否有默认值。</summary>
    /// <returns>如果此参数有一默认值，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public virtual bool HasDefaultValue
    {
      [__DynamicallyInvokable] get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>如果此参数有默认值，则获取指示此参数的默认值的值。</summary>
    /// <returns>此参数的默认值；如果此参数没有默认值，则为 <see cref="F:System.DBNull.Value" />。</returns>
    [__DynamicallyInvokable]
    public virtual object DefaultValue
    {
      [__DynamicallyInvokable] get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>如果此参数有默认值，则获取指示此参数的默认值的值。</summary>
    /// <returns>此参数的默认值；如果此参数没有默认值，则为 <see cref="F:System.DBNull.Value" />。</returns>
    public virtual object RawDefaultValue
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>获取参数在形参表中的位置（从零开始）。</summary>
    /// <returns>表示该参数在参数列表中所占位置的整数。</returns>
    [__DynamicallyInvokable]
    public virtual int Position
    {
      [__DynamicallyInvokable] get
      {
        return this.PositionImpl;
      }
    }

    /// <summary>获取该参数的属性。</summary>
    /// <returns>表示该参数的特性的 ParameterAttributes 对象。</returns>
    [__DynamicallyInvokable]
    public virtual ParameterAttributes Attributes
    {
      [__DynamicallyInvokable] get
      {
        return this.AttrsImpl;
      }
    }

    /// <summary>获取一个值，通过该值指示实现此参数的成员。</summary>
    /// <returns>植入由此 <see cref="T:System.Reflection.ParameterInfo" /> 表示的参数的成员。</returns>
    [__DynamicallyInvokable]
    public virtual MemberInfo Member
    {
      [__DynamicallyInvokable] get
      {
        return this.MemberImpl;
      }
    }

    /// <summary>获取一个值，通过该值指示这是否为输入参数。</summary>
    /// <returns>如果此参数是输入参数，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsIn
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & ParameterAttributes.In) > 0U;
      }
    }

    /// <summary>获取一个值，通过该值指示这是否为输出参数。</summary>
    /// <returns>如果此参数是输出参数，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsOut
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & ParameterAttributes.Out) > 0U;
      }
    }

    /// <summary>获取一个值，通过该值指示该参数是否为区域设置标识符 (lcid)。</summary>
    /// <returns>如果此参数是区域设置标识符，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsLcid
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & ParameterAttributes.Lcid) > 0U;
      }
    }

    /// <summary>获取一个值，通过该值指示这是否为 Retval 参数。</summary>
    /// <returns>如果此参数是 Retval，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsRetval
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & ParameterAttributes.Retval) > 0U;
      }
    }

    /// <summary>获取一个值，通过该值指示该参数是否可选。</summary>
    /// <returns>如果此参数是可选的，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool IsOptional
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & ParameterAttributes.Optional) > 0U;
      }
    }

    /// <summary>获取在元数据中标识此参数的值。</summary>
    /// <returns>与模块一起使用的值，可在元数据中唯一标识此参数。</returns>
    public virtual int MetadataToken
    {
      get
      {
        RuntimeParameterInfo runtimeParameterInfo = this as RuntimeParameterInfo;
        if (runtimeParameterInfo != null)
          return runtimeParameterInfo.MetadataToken;
        return 134217728;
      }
    }

    /// <summary>获取包含此参数自定义特性的集合。</summary>
    /// <returns>包含此参数自定义特性的集合。</returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<CustomAttributeData> CustomAttributes
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<CustomAttributeData>) this.GetCustomAttributesData();
      }
    }

    /// <summary>初始化 ParameterInfo 类的新实例。</summary>
    protected ParameterInfo()
    {
    }

    internal void SetName(string name)
    {
      this.NameImpl = name;
    }

    internal void SetAttributes(ParameterAttributes attributes)
    {
      this.AttrsImpl = attributes;
    }

    /// <summary>获取参数的必需自定义修饰符。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 对象的一个数组，用于标识当前参数所需的自定义修饰符（例如 <see cref="T:System.Runtime.CompilerServices.IsConst" /> 或 <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />）。</returns>
    public virtual Type[] GetRequiredCustomModifiers()
    {
      return EmptyArray<Type>.Value;
    }

    /// <summary>获取参数的可选自定义修饰符。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 对象的一个数组，用于标识当前参数的可选自定义修饰符（例如 <see cref="T:System.Runtime.CompilerServices.IsConst" /> 或 <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />）。</returns>
    public virtual Type[] GetOptionalCustomModifiers()
    {
      return EmptyArray<Type>.Value;
    }

    /// <summary>获取表示为字符串的参数类型和名称。</summary>
    /// <returns>包含参数的类型和名称的字符串。</returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.ParameterType.FormatTypeName() + " " + this.Name;
    }

    /// <summary>获取该参数上定义的所有自定义属性。</summary>
    /// <returns>包含应用于此参数的所有自定义特性的数组。</returns>
    /// <param name="inherit">对于该类型的对象，该参数被忽略。请参阅“备注”。</param>
    /// <exception cref="T:System.TypeLoadException">未能加载自定义特性类型。</exception>
    [__DynamicallyInvokable]
    public virtual object[] GetCustomAttributes(bool inherit)
    {
      return EmptyArray<object>.Value;
    }

    /// <summary>获取应用于此参数的指定类型或其派生类型的自定义特性。</summary>
    /// <returns>一个包含指定类型或其派生类型的自定义特性的数组。</returns>
    /// <param name="attributeType">由类型标识的自定义属性。</param>
    /// <param name="inherit">对于该类型的对象，该参数被忽略。请参阅“备注”。</param>
    /// <exception cref="T:System.ArgumentException">该类型必须是由基础运行时系统提供的类型。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.TypeLoadException">未能加载自定义特性类型。</exception>
    [__DynamicallyInvokable]
    public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      if (attributeType == (Type) null)
        throw new ArgumentNullException("attributeType");
      return EmptyArray<object>.Value;
    }

    /// <summary>确定指定类型或其派生类型的自定义特性是否应用于此参数。</summary>
    /// <returns>如果将 <paramref name="attributeType" /> 或其派生类型的一个或多个实例应用于此参数，则为 true；否则为 false。</returns>
    /// <param name="attributeType">要搜索的 Type 对象。</param>
    /// <param name="inherit">对于该类型的对象，该参数被忽略。请参阅“备注”。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不是一个由公共语言运行时提供的 <see cref="T:System.Type" /> 对象。</exception>
    [__DynamicallyInvokable]
    public virtual bool IsDefined(Type attributeType, bool inherit)
    {
      if (attributeType == (Type) null)
        throw new ArgumentNullException("attributeType");
      return false;
    }

    /// <summary>返回当前参数的 <see cref="T:System.Reflection.CustomAttributeData" /> 对象列表，这些对象可以在只反射上下文中使用。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.CustomAttributeData" /> 对象的泛型列表，这些对象表示有关已应用于当前参数的特性的数据。</returns>
    public virtual IList<CustomAttributeData> GetCustomAttributesData()
    {
      throw new NotImplementedException();
    }

    void _ParameterInfo.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _ParameterInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _ParameterInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _ParameterInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }

    /// <summary>返回应进行反序列化的实际对象，而不是序列化流指定的对象。</summary>
    /// <returns>放入图形中的实际对象。</returns>
    /// <param name="context">从中对当前对象进行反序列化的序列化流。</param>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">参数在其关联成员的参数列表中的位置对于该成员的类型无效。</exception>
    [SecurityCritical]
    public object GetRealObject(StreamingContext context)
    {
      if (this.MemberImpl == (MemberInfo) null)
        throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
      switch (this.MemberImpl.MemberType)
      {
        case MemberTypes.Constructor:
        case MemberTypes.Method:
          if (this.PositionImpl == -1)
          {
            if (this.MemberImpl.MemberType == MemberTypes.Method)
              return (object) ((MethodInfo) this.MemberImpl).ReturnParameter;
            throw new SerializationException(Environment.GetResourceString("Serialization_BadParameterInfo"));
          }
          ParameterInfo[] parametersNoCopy1 = ((MethodBase) this.MemberImpl).GetParametersNoCopy();
          if (parametersNoCopy1 != null && this.PositionImpl < parametersNoCopy1.Length)
            return (object) parametersNoCopy1[this.PositionImpl];
          throw new SerializationException(Environment.GetResourceString("Serialization_BadParameterInfo"));
        case MemberTypes.Property:
          ParameterInfo[] parametersNoCopy2 = ((RuntimePropertyInfo) this.MemberImpl).GetIndexParametersNoCopy();
          if (parametersNoCopy2 != null && this.PositionImpl > -1 && this.PositionImpl < parametersNoCopy2.Length)
            return (object) parametersNoCopy2[this.PositionImpl];
          throw new SerializationException(Environment.GetResourceString("Serialization_BadParameterInfo"));
        default:
          throw new SerializationException(Environment.GetResourceString("Serialization_NoParameterInfo"));
      }
    }
  }
}
