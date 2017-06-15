// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.FieldBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
  /// <summary>定义并表示字段。此类不能被继承。</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_FieldBuilder))]
  [ComVisible(true)]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public sealed class FieldBuilder : FieldInfo, _FieldBuilder
  {
    private int m_fieldTok;
    private FieldToken m_tkField;
    private TypeBuilder m_typeBuilder;
    private string m_fieldName;
    private FieldAttributes m_Attributes;
    private Type m_fieldType;

    internal int MetadataTokenInternal
    {
      get
      {
        return this.m_fieldTok;
      }
    }

    /// <summary>获取在其中定义包含此字段的类型的模块。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.Module" />，表示在其中定义此字段的动态模块。</returns>
    public override Module Module
    {
      get
      {
        return this.m_typeBuilder.Module;
      }
    }

    /// <summary>指示该字段的名称。此属性为只读。</summary>
    /// <returns>包含该字段的名称的 <see cref="T:System.String" />。</returns>
    public override string Name
    {
      get
      {
        return this.m_fieldName;
      }
    }

    /// <summary>指示对声明该字段的类型的 <see cref="T:System.Type" /> 对象的引用。此属性为只读。</summary>
    /// <returns>对声明该字段的类型的 <see cref="T:System.Type" /> 对象的引用。</returns>
    public override Type DeclaringType
    {
      get
      {
        if (this.m_typeBuilder.m_isHiddenGlobalType)
          return (Type) null;
        return (Type) this.m_typeBuilder;
      }
    }

    /// <summary>指示对从中获取此对象的 <see cref="T:System.Type" /> 对象的引用。此属性为只读。</summary>
    /// <returns>对从中获取该实例的 <see cref="T:System.Type" /> 对象的引用。</returns>
    public override Type ReflectedType
    {
      get
      {
        if (this.m_typeBuilder.m_isHiddenGlobalType)
          return (Type) null;
        return (Type) this.m_typeBuilder;
      }
    }

    /// <summary>指示表示该字段的类型的 <see cref="T:System.Type" /> 对象。此属性为只读。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 对象，表示该字段的类型。</returns>
    public override Type FieldType
    {
      get
      {
        return this.m_fieldType;
      }
    }

    /// <summary>指示该字段的内部元数据句柄。此属性为只读。</summary>
    /// <returns>该字段的内部元数据句柄。</returns>
    /// <exception cref="T:System.NotSupportedException">此方法不受支持。</exception>
    public override RuntimeFieldHandle FieldHandle
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
      }
    }

    /// <summary>指示该字段的特性。此属性为只读。</summary>
    /// <returns>该字段的属性。</returns>
    public override FieldAttributes Attributes
    {
      get
      {
        return this.m_Attributes;
      }
    }

    [SecurityCritical]
    internal FieldBuilder(TypeBuilder typeBuilder, string fieldName, Type type, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers, FieldAttributes attributes)
    {
      if (fieldName == null)
        throw new ArgumentNullException("fieldName");
      if (fieldName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "fieldName");
      if ((int) fieldName[0] == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_IllegalName"), "fieldName");
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      if (type == typeof (void))
        throw new ArgumentException(Environment.GetResourceString("Argument_BadFieldType"));
      this.m_fieldName = fieldName;
      this.m_typeBuilder = typeBuilder;
      this.m_fieldType = type;
      this.m_Attributes = attributes & ~FieldAttributes.ReservedMask;
      SignatureHelper fieldSigHelper = SignatureHelper.GetFieldSigHelper(this.m_typeBuilder.Module);
      Type type1 = type;
      Type[] requiredCustomModifiers1 = requiredCustomModifiers;
      Type[] optionalCustomModifiers1 = optionalCustomModifiers;
      fieldSigHelper.AddArgument(type1, requiredCustomModifiers1, optionalCustomModifiers1);
      int sigLength;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      int& length = @sigLength;
      byte[] signature = fieldSigHelper.InternalGetSignature(length);
      this.m_fieldTok = TypeBuilder.DefineField(this.m_typeBuilder.GetModuleBuilder().GetNativeHandle(), typeBuilder.TypeToken.Token, fieldName, signature, sigLength, this.m_Attributes);
      this.m_tkField = new FieldToken(this.m_fieldTok, type);
    }

    [SecurityCritical]
    internal void SetData(byte[] data, int size)
    {
      ModuleBuilder.SetFieldRVAContent(this.m_typeBuilder.GetModuleBuilder().GetNativeHandle(), this.m_tkField.Token, data, size);
    }

    internal TypeBuilder GetTypeBuilder()
    {
      return this.m_typeBuilder;
    }

    /// <summary>检索给定对象支持的字段值。</summary>
    /// <returns>包含此实例反映的字段值的 <see cref="T:System.Object" />。</returns>
    /// <param name="obj">在其上访问该字段的对象。</param>
    /// <exception cref="T:System.NotSupportedException">此方法不受支持。</exception>
    public override object GetValue(object obj)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>设置给定对象支持的字段值。</summary>
    /// <param name="obj">在其上访问该字段的对象。</param>
    /// <param name="val">分配给字段的值。</param>
    /// <param name="invokeAttr">指定所需绑定类型的 IBinder 的成员（例如，IBinder.CreateInstance、IBinder.ExactBinding）。</param>
    /// <param name="binder">一组使用反射启用绑定、参数类型强制和成员调用的属性。如果联编程序为 null，则使用 IBinder.DefaultBinding。</param>
    /// <param name="culture">特定区域性的软件首选项。</param>
    /// <exception cref="T:System.NotSupportedException">此方法不受支持。</exception>
    public override void SetValue(object obj, object val, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>返回为该字段定义的所有自定义属性。</summary>
    /// <returns>类型 <see cref="T:System.Object" /> 的数组，该类型对象表示由此 <see cref="T:System.Reflection.Emit.FieldBuilder" /> 实例表示的构造函数的所有自定义属性。</returns>
    /// <param name="inherit">控制来自基类的自定义属性的继承性。</param>
    /// <exception cref="T:System.NotSupportedException">此方法不受支持。</exception>
    public override object[] GetCustomAttributes(bool inherit)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>返回为该字段定义的、由给定类型标识的所有自定义属性。</summary>
    /// <returns>类型 <see cref="T:System.Object" /> 的数组，该类型对象表示由此 <see cref="T:System.Reflection.Emit.FieldBuilder" /> 实例表示的构造函数的所有自定义属性。</returns>
    /// <param name="attributeType">自定义特性类型。</param>
    /// <param name="inherit">控制来自基类的自定义属性的继承性。</param>
    /// <exception cref="T:System.NotSupportedException">此方法不受支持。</exception>
    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>指示是否在字段上定义了具有指定类型的特性。</summary>
    /// <returns>如果该字段上定义了一个或多个 <paramref name="attributeType" /> 实例，则为 true；否则为 false。</returns>
    /// <param name="attributeType">属性的类型。</param>
    /// <param name="inherit">控制来自基类的自定义属性的继承性。</param>
    /// <exception cref="T:System.NotSupportedException">目前不支持此方法。使用 <see cref="M:System.Type.GetField(System.String,System.Reflection.BindingFlags)" /> 检索该字段，并且对返回的 <see cref="T:System.Reflection.FieldInfo" /> 调用 <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" />。</exception>
    public override bool IsDefined(Type attributeType, bool inherit)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
    }

    /// <summary>返回表示该字段的标记。</summary>
    /// <returns>返回表示该字段的标记的 <see cref="T:System.Reflection.Emit.FieldToken" /> 对象。</returns>
    public FieldToken GetToken()
    {
      return this.m_tkField;
    }

    /// <summary>指定字段布局。</summary>
    /// <param name="iOffset">包含该字段的类型内的字段偏移量。</param>
    /// <exception cref="T:System.InvalidOperationException">已经使用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建了该包含类型。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="iOffset" /> 小于零。</exception>
    [SecuritySafeCritical]
    public void SetOffset(int iOffset)
    {
      this.m_typeBuilder.ThrowIfCreated();
      TypeBuilder.SetFieldLayoutOffset(this.m_typeBuilder.GetModuleBuilder().GetNativeHandle(), this.GetToken().Token, iOffset);
    }

    /// <summary>描述该字段的本机封送处理。</summary>
    /// <param name="unmanagedMarshal">指定该字段的本机封送处理的说明符。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="unmanagedMarshal" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">已经使用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建了该包含类型。</exception>
    [SecuritySafeCritical]
    [Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    public void SetMarshal(UnmanagedMarshal unmanagedMarshal)
    {
      if (unmanagedMarshal == null)
        throw new ArgumentNullException("unmanagedMarshal");
      this.m_typeBuilder.ThrowIfCreated();
      byte[] bytes = unmanagedMarshal.InternalGetBytes();
      RuntimeModule nativeHandle = this.m_typeBuilder.GetModuleBuilder().GetNativeHandle();
      int token = this.GetToken().Token;
      byte[] ubMarshal = bytes;
      int length = ubMarshal.Length;
      TypeBuilder.SetFieldMarshal(nativeHandle, token, ubMarshal, length);
    }

    /// <summary>设置该字段的默认值。</summary>
    /// <param name="defaultValue">该字段的新默认值。</param>
    /// <exception cref="T:System.InvalidOperationException">已经使用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> 创建了该包含类型。</exception>
    /// <exception cref="T:System.ArgumentException">该字段不是受支持类型之一。- 或 -<paramref name="defaultValue" /> 类型与该字段类型不匹配。- 或 -该字段的类型为 <see cref="T:System.Object" /> 或其他引用类型，并且 <paramref name="defaultValue" /> 不是 null，该值无法赋给引用类型。</exception>
    [SecuritySafeCritical]
    public void SetConstant(object defaultValue)
    {
      this.m_typeBuilder.ThrowIfCreated();
      TypeBuilder.SetConstantValue(this.m_typeBuilder.GetModuleBuilder(), this.GetToken().Token, this.m_fieldType, defaultValue);
    }

    /// <summary>使用指定的自定义属性 Blob 设置自定义属性。</summary>
    /// <param name="con">自定义属性的构造函数。</param>
    /// <param name="binaryAttribute">表示属性的字节 Blob。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="con" /> 或 <paramref name="binaryAttribute" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">此字段的父类型是完整的。</exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
    {
      if (con == (ConstructorInfo) null)
        throw new ArgumentNullException("con");
      if (binaryAttribute == null)
        throw new ArgumentNullException("binaryAttribute");
      ModuleBuilder module = this.m_typeBuilder.Module as ModuleBuilder;
      this.m_typeBuilder.ThrowIfCreated();
      TypeBuilder.DefineCustomAttribute(module, this.m_tkField.Token, module.GetConstructorToken(con).Token, binaryAttribute, false, false);
    }

    /// <summary>使用自定义属性生成器设置自定义属性。</summary>
    /// <param name="customBuilder">定义自定义属性的帮助器类的实例。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="con" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">此字段的父类型是完整的。</exception>
    [SecuritySafeCritical]
    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
      if (customBuilder == null)
        throw new ArgumentNullException("customBuilder");
      this.m_typeBuilder.ThrowIfCreated();
      ModuleBuilder mod = this.m_typeBuilder.Module as ModuleBuilder;
      customBuilder.CreateCustomAttribute(mod, this.m_tkField.Token);
    }

    void _FieldBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _FieldBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _FieldBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _FieldBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
