// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.MarshalAsAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>指示如何在托管代码和非托管代码之间封送数据。</summary>
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class MarshalAsAttribute : Attribute
  {
    internal UnmanagedType _val;
    /// <summary>指示 <see cref="F:System.Runtime.InteropServices.UnmanagedType.SafeArray" /> 的元素类型。</summary>
    [__DynamicallyInvokable]
    public VarEnum SafeArraySubType;
    /// <summary>指示用户定义的 <see cref="F:System.Runtime.InteropServices.UnmanagedType.SafeArray" /> 元素类型。</summary>
    [__DynamicallyInvokable]
    public Type SafeArrayUserDefinedSubType;
    /// <summary>指定 COM 使用的非托管 iid_is 属性的参数索引。</summary>
    [__DynamicallyInvokable]
    public int IidParameterIndex;
    /// <summary>指定非托管 <see cref="F:System.Runtime.InteropServices.UnmanagedType.LPArray" /> 或 <see cref="F:System.Runtime.InteropServices.UnmanagedType.ByValArray" /> 的元素类型。</summary>
    [__DynamicallyInvokable]
    public UnmanagedType ArraySubType;
    /// <summary>指示从零开始的参数，该参数包含数组元素的计数，与 COM 中的 size_is 类似。</summary>
    [__DynamicallyInvokable]
    public short SizeParamIndex;
    /// <summary>指示固定长度数组中的元素数，或要导入的字符串中的字符（不是字节）数。</summary>
    [__DynamicallyInvokable]
    public int SizeConst;
    /// <summary>指定自定义封送拆收器的完全限定名。</summary>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public string MarshalType;
    /// <summary>将 <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.MarshalType" /> 作为类型实现。</summary>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public Type MarshalTypeRef;
    /// <summary>向自定义封送拆收器提供附加信息。</summary>
    [__DynamicallyInvokable]
    public string MarshalCookie;

    /// <summary>获取 <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> 值，数据将被作为该值封送。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> 值，数据将被作为该值封送。</returns>
    [__DynamicallyInvokable]
    public UnmanagedType Value
    {
      [__DynamicallyInvokable] get
      {
        return this._val;
      }
    }

    internal MarshalAsAttribute(UnmanagedType val, VarEnum safeArraySubType, RuntimeType safeArrayUserDefinedSubType, UnmanagedType arraySubType, short sizeParamIndex, int sizeConst, string marshalType, RuntimeType marshalTypeRef, string marshalCookie, int iidParamIndex)
    {
      this._val = val;
      this.SafeArraySubType = safeArraySubType;
      this.SafeArrayUserDefinedSubType = (Type) safeArrayUserDefinedSubType;
      this.IidParameterIndex = iidParamIndex;
      this.ArraySubType = arraySubType;
      this.SizeParamIndex = sizeParamIndex;
      this.SizeConst = sizeConst;
      this.MarshalType = marshalType;
      this.MarshalTypeRef = (Type) marshalTypeRef;
      this.MarshalCookie = marshalCookie;
    }

    /// <summary>使用指定的 <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> 枚举成员初始化 <see cref="T:System.Runtime.InteropServices.MarshalAsAttribute" /> 类的新实例。</summary>
    /// <param name="unmanagedType">数据将封送为的值。</param>
    [__DynamicallyInvokable]
    public MarshalAsAttribute(UnmanagedType unmanagedType)
    {
      this._val = unmanagedType;
    }

    /// <summary>使用指定的 <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> 值初始化 <see cref="T:System.Runtime.InteropServices.MarshalAsAttribute" /> 类的新实例。</summary>
    /// <param name="unmanagedType">数据将封送为的值。</param>
    [__DynamicallyInvokable]
    public MarshalAsAttribute(short unmanagedType)
    {
      this._val = (UnmanagedType) unmanagedType;
    }

    [SecurityCritical]
    internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
    {
      return MarshalAsAttribute.GetCustomAttribute(parameter.MetadataToken, parameter.GetRuntimeModule());
    }

    [SecurityCritical]
    internal static bool IsDefined(RuntimeParameterInfo parameter)
    {
      return MarshalAsAttribute.GetCustomAttribute(parameter) != null;
    }

    [SecurityCritical]
    internal static Attribute GetCustomAttribute(RuntimeFieldInfo field)
    {
      return MarshalAsAttribute.GetCustomAttribute(field.MetadataToken, field.GetRuntimeModule());
    }

    [SecurityCritical]
    internal static bool IsDefined(RuntimeFieldInfo field)
    {
      return MarshalAsAttribute.GetCustomAttribute(field) != null;
    }

    [SecurityCritical]
    internal static Attribute GetCustomAttribute(int token, RuntimeModule scope)
    {
      int sizeParamIndex = 0;
      int sizeConst = 0;
      string marshalType = (string) null;
      string marshalCookie = (string) null;
      string safeArrayUserDefinedSubType1 = (string) null;
      int iidParamIndex = 0;
      ConstArray fieldMarshal = ModuleHandle.GetMetadataImport(scope.GetNativeHandle()).GetFieldMarshal(token);
      if (fieldMarshal.Length == 0)
        return (Attribute) null;
      UnmanagedType unmanagedType;
      VarEnum safeArraySubType;
      UnmanagedType arraySubType;
      MetadataImport.GetMarshalAs(fieldMarshal, out unmanagedType, out safeArraySubType, out safeArrayUserDefinedSubType1, out arraySubType, out sizeParamIndex, out sizeConst, out marshalType, out marshalCookie, out iidParamIndex);
      RuntimeType safeArrayUserDefinedSubType2 = safeArrayUserDefinedSubType1 == null || safeArrayUserDefinedSubType1.Length == 0 ? (RuntimeType) null : RuntimeTypeHandle.GetTypeByNameUsingCARules(safeArrayUserDefinedSubType1, scope);
      RuntimeType marshalTypeRef = (RuntimeType) null;
      try
      {
        marshalTypeRef = marshalType == null ? (RuntimeType) null : RuntimeTypeHandle.GetTypeByNameUsingCARules(marshalType, scope);
      }
      catch (TypeLoadException ex)
      {
      }
      return (Attribute) new MarshalAsAttribute(unmanagedType, safeArraySubType, safeArrayUserDefinedSubType2, arraySubType, (short) sizeParamIndex, sizeConst, marshalType, marshalTypeRef, marshalCookie, iidParamIndex);
    }
  }
}
