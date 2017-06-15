// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.ParameterBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection.Emit
{
  /// <summary>创建或关联参数信息。</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_ParameterBuilder))]
  [ComVisible(true)]
  public class ParameterBuilder : _ParameterBuilder
  {
    private string m_strParamName;
    private int m_iPosition;
    private ParameterAttributes m_attributes;
    private MethodBuilder m_methodBuilder;
    private ParameterToken m_pdToken;

    internal int MetadataTokenInternal
    {
      get
      {
        return this.m_pdToken.Token;
      }
    }

    /// <summary>检索此参数的名称。</summary>
    /// <returns>只读。检索此参数的名称。</returns>
    public virtual string Name
    {
      get
      {
        return this.m_strParamName;
      }
    }

    /// <summary>检索此参数的签名位置。</summary>
    /// <returns>只读。检索此参数的签名位置。</returns>
    public virtual int Position
    {
      get
      {
        return this.m_iPosition;
      }
    }

    /// <summary>检索此参数的属性。</summary>
    /// <returns>只读。检索此参数的属性。</returns>
    public virtual int Attributes
    {
      get
      {
        return (int) this.m_attributes;
      }
    }

    /// <summary>检索这是否为输入参数。</summary>
    /// <returns>只读。检索这是否为输入参数。</returns>
    public bool IsIn
    {
      get
      {
        return (uint) (this.m_attributes & ParameterAttributes.In) > 0U;
      }
    }

    /// <summary>检索此参数是否为输出参数。</summary>
    /// <returns>只读。检索此参数是否为输出参数。</returns>
    public bool IsOut
    {
      get
      {
        return (uint) (this.m_attributes & ParameterAttributes.Out) > 0U;
      }
    }

    /// <summary>检索此参数是否为可选的。</summary>
    /// <returns>只读。指定此参数是否为可选的。</returns>
    public bool IsOptional
    {
      get
      {
        return (uint) (this.m_attributes & ParameterAttributes.Optional) > 0U;
      }
    }

    private ParameterBuilder()
    {
    }

    [SecurityCritical]
    internal ParameterBuilder(MethodBuilder methodBuilder, int sequence, ParameterAttributes attributes, string strParamName)
    {
      this.m_iPosition = sequence;
      this.m_strParamName = strParamName;
      this.m_methodBuilder = methodBuilder;
      this.m_strParamName = strParamName;
      this.m_attributes = attributes;
      this.m_pdToken = new ParameterToken(TypeBuilder.SetParamInfo(this.m_methodBuilder.GetModuleBuilder().GetNativeHandle(), this.m_methodBuilder.GetToken().Token, sequence, attributes, strParamName));
    }

    /// <summary>为该参数指定封送处理。</summary>
    /// <param name="unmanagedMarshal">此参数的封送处理信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="unmanagedMarshal" /> 为 null。</exception>
    [SecuritySafeCritical]
    [Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    public virtual void SetMarshal(UnmanagedMarshal unmanagedMarshal)
    {
      if (unmanagedMarshal == null)
        throw new ArgumentNullException("unmanagedMarshal");
      byte[] bytes = unmanagedMarshal.InternalGetBytes();
      RuntimeModule nativeHandle = this.m_methodBuilder.GetModuleBuilder().GetNativeHandle();
      int token = this.m_pdToken.Token;
      byte[] ubMarshal = bytes;
      int length = ubMarshal.Length;
      TypeBuilder.SetFieldMarshal(nativeHandle, token, ubMarshal, length);
    }

    /// <summary>设置该参数的默认值。</summary>
    /// <param name="defaultValue">该参数的默认值。</param>
    /// <exception cref="T:System.ArgumentException">该参数不是受支持的类型之一。- 或 -<paramref name="defaultValue" /> 的类型与该参数的类型不匹配。- 或 -该参数的类型为 <see cref="T:System.Object" /> 或其他引用类型，并且 <paramref name="defaultValue" /> 不是 null，该值无法赋给引用类型。</exception>
    [SecuritySafeCritical]
    public virtual void SetConstant(object defaultValue)
    {
      TypeBuilder.SetConstantValue(this.m_methodBuilder.GetModuleBuilder(), this.m_pdToken.Token, this.m_iPosition == 0 ? this.m_methodBuilder.ReturnType : this.m_methodBuilder.m_parameterTypes[this.m_iPosition - 1], defaultValue);
    }

    /// <summary>使用指定的自定义属性 Blob 设置自定义属性。</summary>
    /// <param name="con">自定义属性的构造函数。</param>
    /// <param name="binaryAttribute">表示属性的字节 Blob。</param>
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
      TypeBuilder.DefineCustomAttribute(this.m_methodBuilder.GetModuleBuilder(), this.m_pdToken.Token, ((ModuleBuilder) this.m_methodBuilder.GetModule()).GetConstructorToken(con).Token, binaryAttribute, false, false);
    }

    /// <summary>使用自定义属性生成器设置自定义属性。</summary>
    /// <param name="customBuilder">定义自定义属性的帮助器类的实例。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="con" /> 为 null。</exception>
    [SecuritySafeCritical]
    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
      if (customBuilder == null)
        throw new ArgumentNullException("customBuilder");
      customBuilder.CreateCustomAttribute((ModuleBuilder) this.m_methodBuilder.GetModule(), this.m_pdToken.Token);
    }

    /// <summary>检索此参数的标记。</summary>
    /// <returns>返回此参数的标记。</returns>
    public virtual ParameterToken GetToken()
    {
      return this.m_pdToken;
    }

    void _ParameterBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _ParameterBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _ParameterBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _ParameterBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
