// Decompiled with JetBrains decompiler
// Type: System.RuntimeFieldHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>使用内部元数据标记表示一个字段。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct RuntimeFieldHandle : ISerializable
  {
    private IRuntimeFieldInfo m_ptr;

    /// <summary>获取当前实例所表示的字段的句柄。</summary>
    /// <returns>
    /// <see cref="T:System.IntPtr" />，包含当前实例所表示的字段的句柄。</returns>
    /// <filterpriority>2</filterpriority>
    public IntPtr Value
    {
      [SecurityCritical] get
      {
        if (this.m_ptr == null)
          return IntPtr.Zero;
        return this.m_ptr.Value.Value;
      }
    }

    internal RuntimeFieldHandle(IRuntimeFieldInfo fieldInfo)
    {
      this.m_ptr = fieldInfo;
    }

    [SecurityCritical]
    private RuntimeFieldHandle(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      FieldInfo fieldInfo = (FieldInfo) info.GetValue("FieldObj", typeof (RuntimeFieldInfo));
      if (fieldInfo == (FieldInfo) null)
        throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
      this.m_ptr = fieldInfo.FieldHandle.m_ptr;
      if (this.m_ptr == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
    }

    /// <summary>指示两个 <see cref="T:System.RuntimeFieldHandle" /> 结构是否等同。</summary>
    /// <returns>如果 <paramref name="left" /> 等于 <paramref name="right" />，则为 true；否则为 false。</returns>
    /// <param name="left">要与 <paramref name="right" /> 进行比较的 <see cref="T:System.RuntimeFieldHandle" />。</param>
    /// <param name="right">要与 <paramref name="left" /> 进行比较的 <see cref="T:System.RuntimeFieldHandle" />。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator ==(RuntimeFieldHandle left, RuntimeFieldHandle right)
    {
      return left.Equals(right);
    }

    /// <summary>指示两个 <see cref="T:System.RuntimeFieldHandle" /> 结构是否不相等。</summary>
    /// <returns>如果 <paramref name="left" /> 不等于 <paramref name="right" />，则为 true；否则为 false。</returns>
    /// <param name="left">要与 <paramref name="right" /> 进行比较的 <see cref="T:System.RuntimeFieldHandle" />。</param>
    /// <param name="right">要与 <paramref name="left" /> 进行比较的 <see cref="T:System.RuntimeFieldHandle" />。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator !=(RuntimeFieldHandle left, RuntimeFieldHandle right)
    {
      return !left.Equals(right);
    }

    internal RuntimeFieldHandle GetNativeHandle()
    {
      IRuntimeFieldInfo fieldInfo = this.m_ptr;
      if (fieldInfo == null)
        throw new ArgumentNullException((string) null, Environment.GetResourceString("Arg_InvalidHandle"));
      return new RuntimeFieldHandle(fieldInfo);
    }

    internal IRuntimeFieldInfo GetRuntimeFieldInfo()
    {
      return this.m_ptr;
    }

    internal bool IsNullHandle()
    {
      return this.m_ptr == null;
    }

    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return ValueType.GetHashCodeOfPtr(this.Value);
    }

    /// <summary>指示当前实例是否等于指定的对象。</summary>
    /// <returns>如果 <paramref name="obj" /> 为 <see cref="T:System.RuntimeFieldHandle" /> 且与当前实例的值相等，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前实例进行比较的对象。</param>
    /// <filterpriority>2</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (!(obj is RuntimeFieldHandle))
        return false;
      return ((RuntimeFieldHandle) obj).Value == this.Value;
    }

    /// <summary>指示当前实例是否等于指定的 <see cref="T:System.RuntimeFieldHandle" />。</summary>
    /// <returns>如果 <paramref name="handle" /> 的值等于当前实例的值，则为 true；否则为 false。</returns>
    /// <param name="handle">要与当前实例进行比较的 <see cref="T:System.RuntimeFieldHandle" />。</param>
    /// <filterpriority>2</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public bool Equals(RuntimeFieldHandle handle)
    {
      return handle.Value == this.Value;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string GetName(RtFieldInfo field);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe void* _GetUtf8Name(RuntimeFieldHandleInternal field);

    [SecuritySafeCritical]
    internal static unsafe Utf8String GetUtf8Name(RuntimeFieldHandleInternal field)
    {
      return new Utf8String(RuntimeFieldHandle._GetUtf8Name(field));
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool MatchesNameHash(RuntimeFieldHandleInternal handle, uint hash);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern FieldAttributes GetAttributes(RuntimeFieldHandleInternal field);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeType GetApproxDeclaringType(RuntimeFieldHandleInternal field);

    [SecurityCritical]
    internal static RuntimeType GetApproxDeclaringType(IRuntimeFieldInfo field)
    {
      RuntimeType approxDeclaringType = RuntimeFieldHandle.GetApproxDeclaringType(field.Value);
      GC.KeepAlive((object) field);
      return approxDeclaringType;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetToken(RtFieldInfo field);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object GetValue(RtFieldInfo field, object instance, RuntimeType fieldType, RuntimeType declaringType, ref bool domainInitialized);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern unsafe object GetValueDirect(RtFieldInfo field, RuntimeType fieldType, void* pTypedRef, RuntimeType contextType);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void SetValue(RtFieldInfo field, object obj, object value, RuntimeType fieldType, FieldAttributes fieldAttr, RuntimeType declaringType, ref bool domainInitialized);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern unsafe void SetValueDirect(RtFieldInfo field, RuntimeType fieldType, void* pTypedRef, object value, RuntimeType contextType);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeFieldHandleInternal GetStaticFieldForGenericType(RuntimeFieldHandleInternal field, RuntimeType declaringType);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool AcquiresContextFromThis(RuntimeFieldHandleInternal field);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsSecurityCritical(RuntimeFieldHandle fieldHandle);

    [SecuritySafeCritical]
    internal bool IsSecurityCritical()
    {
      return RuntimeFieldHandle.IsSecurityCritical(this.GetNativeHandle());
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsSecuritySafeCritical(RuntimeFieldHandle fieldHandle);

    [SecuritySafeCritical]
    internal bool IsSecuritySafeCritical()
    {
      return RuntimeFieldHandle.IsSecuritySafeCritical(this.GetNativeHandle());
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsSecurityTransparent(RuntimeFieldHandle fieldHandle);

    [SecuritySafeCritical]
    internal bool IsSecurityTransparent()
    {
      return RuntimeFieldHandle.IsSecurityTransparent(this.GetNativeHandle());
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void CheckAttributeAccess(RuntimeFieldHandle fieldHandle, RuntimeModule decoratedTarget);

    /// <summary>使用反序列化当前实例所表示的字段所需的数据填充 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。</summary>
    /// <param name="info">要用序列化信息填充的 <see cref="T:System.Runtime.Serialization.SerializationInfo" /> 对象。</param>
    /// <param name="context">（保留）存储和检索序列化数据的地方。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">当前实例的 <see cref="P:System.RuntimeFieldHandle.Value" /> 属性不是有效句柄。</exception>
    /// <filterpriority>2</filterpriority>
    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      if (this.m_ptr == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidFieldState"));
      RuntimeFieldInfo runtimeFieldInfo = (RuntimeFieldInfo) RuntimeType.GetFieldInfo(this.GetRuntimeFieldInfo());
      info.AddValue("FieldObj", (object) runtimeFieldInfo, typeof (RuntimeFieldInfo));
    }
  }
}
