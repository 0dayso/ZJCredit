// Decompiled with JetBrains decompiler
// Type: System.TypedReference
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
  /// <summary>描述既包含指向某位置的托管指针，也包含该位置可能存储的类型的运行时表示形式的对象。</summary>
  /// <filterpriority>2</filterpriority>
  [CLSCompliant(false)]
  [ComVisible(true)]
  [NonVersionable]
  public struct TypedReference
  {
    private IntPtr Value;
    private IntPtr Type;

    internal bool IsNull
    {
      get
      {
        if (this.Value.IsNull())
          return this.Type.IsNull();
        return false;
      }
    }

    /// <summary>为由指定对象和字段说明列表标识的字段生成 TypedReference。</summary>
    /// <returns>由 <paramref name="flds" /> 的最后一个元素说明的字段的 <see cref="T:System.TypedReference" />。</returns>
    /// <param name="target">包含由 <paramref name="flds" /> 的第一个元素说明的字段的对象。</param>
    /// <param name="flds">字段说明列表，其中每个元素说明的字段均包含了由后续元素说明的字段。每个说明的字段都必须是值类型。字段说明必须是类型系统所提供的 RuntimeFieldInfo 对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="target" /> 或 <paramref name="flds" /> 为 null。- 或 - <paramref name="flds" /> 的一个元素为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="flds" /> 数组没有元素。- 或 - 元素的 <paramref name="flds" /> 不是 RuntimeFieldInfo 对象。- 或 - <see cref="P:System.Reflection.FieldInfo.IsInitOnly" /> 元素的 <see cref="P:System.Reflection.FieldInfo.IsStatic" /> 或 <paramref name="flds" /> 属性为 true。</exception>
    /// <exception cref="T:System.MissingMemberException">参数 <paramref name="target" /> 不包含下面这样的元素所说明的字段：<paramref name="flds" /> 的第一个元素；或者 <paramref name="flds" /> 的这样一种元素：它说明了一个字段，而 <paramref name="flds" /> 的后续元素说明的字段中不包含该字段。- 或 - 由 <paramref name="flds" /> 的元素说明的字段不是值类型。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [SecurityCritical]
    [CLSCompliant(false)]
    public static unsafe TypedReference MakeTypedReference(object target, FieldInfo[] flds)
    {
      if (target == null)
        throw new ArgumentNullException("target");
      if (flds == null)
        throw new ArgumentNullException("flds");
      if (flds.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Arg_ArrayZeroError"));
      IntPtr[] flds1 = new IntPtr[flds.Length];
      RuntimeType lastFieldType = (RuntimeType) target.GetType();
      for (int index = 0; index < flds.Length; ++index)
      {
        RuntimeFieldInfo runtimeFieldInfo = flds[index] as RuntimeFieldInfo;
        if ((FieldInfo) runtimeFieldInfo == (FieldInfo) null)
          throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeFieldInfo"));
        if (runtimeFieldInfo.IsInitOnly || runtimeFieldInfo.IsStatic)
          throw new ArgumentException(Environment.GetResourceString("Argument_TypedReferenceInvalidField"));
        if (lastFieldType != runtimeFieldInfo.GetDeclaringTypeInternal() && !lastFieldType.IsSubclassOf((System.Type) runtimeFieldInfo.GetDeclaringTypeInternal()))
          throw new MissingMemberException(Environment.GetResourceString("MissingMemberTypeRef"));
        RuntimeType runtimeType = (RuntimeType) runtimeFieldInfo.FieldType;
        if (runtimeType.IsPrimitive)
          throw new ArgumentException(Environment.GetResourceString("Arg_TypeRefPrimitve"));
        if (index < flds.Length - 1 && !runtimeType.IsValueType)
          throw new MissingMemberException(Environment.GetResourceString("MissingMemberNestErr"));
        flds1[index] = runtimeFieldInfo.FieldHandle.Value;
        lastFieldType = runtimeType;
      }
      TypedReference typedReference = new TypedReference();
      TypedReference.InternalMakeTypedReference((void*) &typedReference, target, flds1, lastFieldType);
      return typedReference;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe void InternalMakeTypedReference(void* result, object target, IntPtr[] flds, RuntimeType lastFieldType);

    /// <summary>返回此对象的哈希代码。</summary>
    /// <returns>此对象的哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    public override int GetHashCode()
    {
      if (this.Type == IntPtr.Zero)
        return 0;
      return __reftype (this).GetHashCode();
    }

    /// <summary>检查该对象是否等于指定对象。</summary>
    /// <returns>如果该对象等于指定对象，则为 true；否则为 false。</returns>
    /// <param name="o">用于和当前对象进行比较的对象。</param>
    /// <exception cref="T:System.NotSupportedException">此方法未实现。</exception>
    /// <filterpriority>2</filterpriority>
    public override bool Equals(object o)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_NYI"));
    }

    /// <summary>将指定 TypedReference 转换为 Object。</summary>
    /// <returns>
    /// <see cref="T:System.Object" />，转换自 TypedReference。</returns>
    /// <param name="value">要转换的 TypedReference。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public static unsafe object ToObject(TypedReference value)
    {
      return TypedReference.InternalToObject((void*) &value);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern unsafe object InternalToObject(void* value);

    /// <summary>返回指定 TypedReference 的目标类型。</summary>
    /// <returns>返回指定 TypedReference 的目标类型。</returns>
    /// <param name="value">要返回其目标类型的值。</param>
    /// <filterpriority>1</filterpriority>
    public static System.Type GetTargetType(TypedReference value)
    {
      return __reftype (value);
    }

    /// <summary>返回指定 TypedReference 的内部元数据类型句柄。</summary>
    /// <returns>指定 TypedReference 的内部元数据类型句柄。</returns>
    /// <param name="value">请求该类型句柄的 TypedReference。</param>
    /// <filterpriority>1</filterpriority>
    public static RuntimeTypeHandle TargetTypeToken(TypedReference value)
    {
      return __reftype (value).TypeHandle;
    }

    /// <summary>将指定值转换为 TypedReference。不支持此方法。</summary>
    /// <param name="target">转换的目标。</param>
    /// <param name="value">要转换的值。</param>
    /// <exception cref="T:System.NotSupportedException">在所有情况下。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    public static unsafe void SetTypedReference(TypedReference target, object value)
    {
      TypedReference.InternalSetTypedReference((void*) &target, value);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern unsafe void InternalSetTypedReference(void* target, object value);
  }
}
