// Decompiled with JetBrains decompiler
// Type: System.RuntimeMethodHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System
{
  /// <summary>
  /// <see cref="T:System.RuntimeMethodHandle" /> 是方法的内部元数据表示形式的句柄。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct RuntimeMethodHandle : ISerializable
  {
    private IRuntimeMethodInfo m_value;

    internal static RuntimeMethodHandle EmptyHandle
    {
      get
      {
        return new RuntimeMethodHandle();
      }
    }

    /// <summary>获得此实例的值。</summary>
    /// <returns>
    /// <see cref="T:System.RuntimeMethodHandle" />，它是方法的内部元数据表示形式。</returns>
    /// <filterpriority>2</filterpriority>
    public IntPtr Value
    {
      [SecurityCritical] get
      {
        if (this.m_value == null)
          return IntPtr.Zero;
        return this.m_value.Value.Value;
      }
    }

    internal RuntimeMethodHandle(IRuntimeMethodInfo method)
    {
      this.m_value = method;
    }

    [SecurityCritical]
    private RuntimeMethodHandle(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      this.m_value = ((MethodBase) info.GetValue("MethodObj", typeof (MethodBase))).MethodHandle.m_value;
      if (this.m_value == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
    }

    /// <summary>指示两个 <see cref="T:System.RuntimeMethodHandle" /> 实例是否相等。</summary>
    /// <returns>如果 <paramref name="left" /> 的值等于 <paramref name="right" /> 的值，则为 true；否则为 false。</returns>
    /// <param name="left">要与 <paramref name="right" /> 进行比较的 <see cref="T:System.RuntimeMethodHandle" />。</param>
    /// <param name="right">要与 <paramref name="left" /> 进行比较的 <see cref="T:System.RuntimeMethodHandle" />。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator ==(RuntimeMethodHandle left, RuntimeMethodHandle right)
    {
      return left.Equals(right);
    }

    /// <summary>指示两个 <see cref="T:System.RuntimeMethodHandle" /> 实例是否不相等。</summary>
    /// <returns>如果 <paramref name="left" /> 的值与 <paramref name="right" /> 的值不相等，则为 true；否则为 false。</returns>
    /// <param name="left">要与 <paramref name="right" /> 进行比较的 <see cref="T:System.RuntimeMethodHandle" />。</param>
    /// <param name="right">要与 <paramref name="left" /> 进行比较的 <see cref="T:System.RuntimeMethodHandle" />。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator !=(RuntimeMethodHandle left, RuntimeMethodHandle right)
    {
      return !left.Equals(right);
    }

    internal static IRuntimeMethodInfo EnsureNonNullMethodInfo(IRuntimeMethodInfo method)
    {
      if (method == null)
        throw new ArgumentNullException((string) null, Environment.GetResourceString("Arg_InvalidHandle"));
      return method;
    }

    internal IRuntimeMethodInfo GetMethodInfo()
    {
      return this.m_value;
    }

    [SecurityCritical]
    private static IntPtr GetValueInternal(RuntimeMethodHandle rmh)
    {
      return rmh.Value;
    }

    /// <summary>用反序列化此实例所表示的字段所必需的数据填充 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。</summary>
    /// <param name="info">要用序列化信息填充的对象。</param>
    /// <param name="context">（保留）存储和检索序列化数据的地方。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    /// <see cref="P:System.RuntimeMethodHandle.Value" /> 无效。</exception>
    /// <filterpriority>2</filterpriority>
    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      if (this.m_value == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidFieldState"));
      MethodBase methodBase = RuntimeType.GetMethodBase(this.m_value);
      info.AddValue("MethodObj", (object) methodBase, typeof (MethodBase));
    }

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return ValueType.GetHashCodeOfPtr(this.Value);
    }

    /// <summary>指示此实例是否与指定对象相等。</summary>
    /// <returns>如果 <paramref name="obj" /> 为 <see cref="T:System.RuntimeMethodHandle" /> 且与此实例的值相等，则为 true；否则为 false。</returns>
    /// <param name="obj">要与此实例进行比较的 <see cref="T:System.Object" />。</param>
    /// <filterpriority>2</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (!(obj is RuntimeMethodHandle))
        return false;
      return ((RuntimeMethodHandle) obj).Value == this.Value;
    }

    /// <summary>指示此实例是否与指定的 <see cref="T:System.RuntimeMethodHandle" /> 相等。</summary>
    /// <returns>如果 <paramref name="handle" /> 等于此实例的值，则为 true；否则，为 false。</returns>
    /// <param name="handle">要与此实例进行比较的 <see cref="T:System.RuntimeMethodHandle" />。</param>
    /// <filterpriority>2</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public bool Equals(RuntimeMethodHandle handle)
    {
      return handle.Value == this.Value;
    }

    internal bool IsNullHandle()
    {
      return this.m_value == null;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern IntPtr GetFunctionPointer(RuntimeMethodHandleInternal handle);

    /// <summary>获取指向此实例所表示方法的指针。</summary>
    /// <returns>指向此实例所表示方法的指针。</returns>
    /// <exception cref="T:System.Security.SecurityException">调用方不具有执行此操作所需的权限。</exception>
    /// <filterpriority>2</filterpriority>
    [SecurityCritical]
    public IntPtr GetFunctionPointer()
    {
      IntPtr functionPointer = RuntimeMethodHandle.GetFunctionPointer(RuntimeMethodHandle.EnsureNonNullMethodInfo(this.m_value).Value);
      GC.KeepAlive((object) this.m_value);
      return functionPointer;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void CheckLinktimeDemands(IRuntimeMethodInfo method, RuntimeModule module, bool isDecoratedTargetSecurityTransparent);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern bool IsCAVisibleFromDecoratedType(RuntimeTypeHandle attrTypeHandle, IRuntimeMethodInfo attrCtor, RuntimeTypeHandle sourceTypeHandle, RuntimeModule sourceModule);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern IRuntimeMethodInfo _GetCurrentMethod(ref StackCrawlMark stackMark);

    [SecuritySafeCritical]
    internal static IRuntimeMethodInfo GetCurrentMethod(ref StackCrawlMark stackMark)
    {
      return RuntimeMethodHandle._GetCurrentMethod(ref stackMark);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern MethodAttributes GetAttributes(RuntimeMethodHandleInternal method);

    [SecurityCritical]
    internal static MethodAttributes GetAttributes(IRuntimeMethodInfo method)
    {
      int num = (int) RuntimeMethodHandle.GetAttributes(method.Value);
      GC.KeepAlive((object) method);
      return (MethodAttributes) num;
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern MethodImplAttributes GetImplAttributes(IRuntimeMethodInfo method);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void ConstructInstantiation(IRuntimeMethodInfo method, TypeNameFormatFlags format, StringHandleOnStack retString);

    [SecuritySafeCritical]
    internal static string ConstructInstantiation(IRuntimeMethodInfo method, TypeNameFormatFlags format)
    {
      string s = (string) null;
      RuntimeMethodHandle.ConstructInstantiation(RuntimeMethodHandle.EnsureNonNullMethodInfo(method), format, JitHelpers.GetStringHandleOnStack(ref s));
      return s;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeType GetDeclaringType(RuntimeMethodHandleInternal method);

    [SecuritySafeCritical]
    internal static RuntimeType GetDeclaringType(IRuntimeMethodInfo method)
    {
      RuntimeType declaringType = RuntimeMethodHandle.GetDeclaringType(method.Value);
      GC.KeepAlive((object) method);
      return declaringType;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetSlot(RuntimeMethodHandleInternal method);

    [SecurityCritical]
    internal static int GetSlot(IRuntimeMethodInfo method)
    {
      int slot = RuntimeMethodHandle.GetSlot(method.Value);
      GC.KeepAlive((object) method);
      return slot;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetMethodDef(IRuntimeMethodInfo method);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string GetName(RuntimeMethodHandleInternal method);

    [SecurityCritical]
    internal static string GetName(IRuntimeMethodInfo method)
    {
      string name = RuntimeMethodHandle.GetName(method.Value);
      GC.KeepAlive((object) method);
      return name;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe void* _GetUtf8Name(RuntimeMethodHandleInternal method);

    [SecurityCritical]
    internal static unsafe Utf8String GetUtf8Name(RuntimeMethodHandleInternal method)
    {
      return new Utf8String(RuntimeMethodHandle._GetUtf8Name(method));
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool MatchesNameHash(RuntimeMethodHandleInternal method, uint hash);

    [SecuritySafeCritical]
    [DebuggerStepThrough]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object InvokeMethod(object target, object[] arguments, Signature sig, bool constructor);

    [SecurityCritical]
    internal static INVOCATION_FLAGS GetSecurityFlags(IRuntimeMethodInfo handle)
    {
      return (INVOCATION_FLAGS) RuntimeMethodHandle.GetSpecialSecurityFlags(handle);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern uint GetSpecialSecurityFlags(IRuntimeMethodInfo method);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void PerformSecurityCheck(object obj, RuntimeMethodHandleInternal method, RuntimeType parent, uint invocationFlags);

    [SecurityCritical]
    internal static void PerformSecurityCheck(object obj, IRuntimeMethodInfo method, RuntimeType parent, uint invocationFlags)
    {
      RuntimeMethodHandle.PerformSecurityCheck(obj, method.Value, parent, invocationFlags);
      GC.KeepAlive((object) method);
    }

    [SecuritySafeCritical]
    [DebuggerStepThrough]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void SerializationInvoke(IRuntimeMethodInfo method, object target, SerializationInfo info, ref StreamingContext context);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool _IsTokenSecurityTransparent(RuntimeModule module, int metaDataToken);

    [SecurityCritical]
    internal static bool IsTokenSecurityTransparent(Module module, int metaDataToken)
    {
      return RuntimeMethodHandle._IsTokenSecurityTransparent(module.ModuleHandle.GetRuntimeModule(), metaDataToken);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool _IsSecurityCritical(IRuntimeMethodInfo method);

    [SecuritySafeCritical]
    internal static bool IsSecurityCritical(IRuntimeMethodInfo method)
    {
      return RuntimeMethodHandle._IsSecurityCritical(method);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool _IsSecuritySafeCritical(IRuntimeMethodInfo method);

    [SecuritySafeCritical]
    internal static bool IsSecuritySafeCritical(IRuntimeMethodInfo method)
    {
      return RuntimeMethodHandle._IsSecuritySafeCritical(method);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool _IsSecurityTransparent(IRuntimeMethodInfo method);

    [SecuritySafeCritical]
    internal static bool IsSecurityTransparent(IRuntimeMethodInfo method)
    {
      return RuntimeMethodHandle._IsSecurityTransparent(method);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetMethodInstantiation(RuntimeMethodHandleInternal method, ObjectHandleOnStack types, bool fAsRuntimeTypeArray);

    [SecuritySafeCritical]
    internal static RuntimeType[] GetMethodInstantiationInternal(IRuntimeMethodInfo method)
    {
      RuntimeType[] o = (RuntimeType[]) null;
      RuntimeMethodHandle.GetMethodInstantiation(RuntimeMethodHandle.EnsureNonNullMethodInfo(method).Value, JitHelpers.GetObjectHandleOnStack<RuntimeType[]>(ref o), true);
      GC.KeepAlive((object) method);
      return o;
    }

    [SecuritySafeCritical]
    internal static RuntimeType[] GetMethodInstantiationInternal(RuntimeMethodHandleInternal method)
    {
      RuntimeType[] o = (RuntimeType[]) null;
      RuntimeMethodHandle.GetMethodInstantiation(method, JitHelpers.GetObjectHandleOnStack<RuntimeType[]>(ref o), true);
      return o;
    }

    [SecuritySafeCritical]
    internal static Type[] GetMethodInstantiationPublic(IRuntimeMethodInfo method)
    {
      RuntimeType[] o = (RuntimeType[]) null;
      RuntimeMethodHandle.GetMethodInstantiation(RuntimeMethodHandle.EnsureNonNullMethodInfo(method).Value, JitHelpers.GetObjectHandleOnStack<RuntimeType[]>(ref o), false);
      GC.KeepAlive((object) method);
      return (Type[]) o;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool HasMethodInstantiation(RuntimeMethodHandleInternal method);

    [SecuritySafeCritical]
    internal static bool HasMethodInstantiation(IRuntimeMethodInfo method)
    {
      int num = RuntimeMethodHandle.HasMethodInstantiation(method.Value) ? 1 : 0;
      GC.KeepAlive((object) method);
      return num != 0;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeMethodHandleInternal GetStubIfNeeded(RuntimeMethodHandleInternal method, RuntimeType declaringType, RuntimeType[] methodInstantiation);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeMethodHandleInternal GetMethodFromCanonical(RuntimeMethodHandleInternal method, RuntimeType declaringType);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsGenericMethodDefinition(RuntimeMethodHandleInternal method);

    [SecuritySafeCritical]
    internal static bool IsGenericMethodDefinition(IRuntimeMethodInfo method)
    {
      int num = RuntimeMethodHandle.IsGenericMethodDefinition(method.Value) ? 1 : 0;
      GC.KeepAlive((object) method);
      return num != 0;
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsTypicalMethodDefinition(IRuntimeMethodInfo method);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetTypicalMethodDefinition(IRuntimeMethodInfo method, ObjectHandleOnStack outMethod);

    [SecuritySafeCritical]
    internal static IRuntimeMethodInfo GetTypicalMethodDefinition(IRuntimeMethodInfo method)
    {
      if (!RuntimeMethodHandle.IsTypicalMethodDefinition(method))
        RuntimeMethodHandle.GetTypicalMethodDefinition(method, JitHelpers.GetObjectHandleOnStack<IRuntimeMethodInfo>(ref method));
      return method;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void StripMethodInstantiation(IRuntimeMethodInfo method, ObjectHandleOnStack outMethod);

    [SecuritySafeCritical]
    internal static IRuntimeMethodInfo StripMethodInstantiation(IRuntimeMethodInfo method)
    {
      IRuntimeMethodInfo o = method;
      RuntimeMethodHandle.StripMethodInstantiation(method, JitHelpers.GetObjectHandleOnStack<IRuntimeMethodInfo>(ref o));
      return o;
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsDynamicMethod(RuntimeMethodHandleInternal method);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void Destroy(RuntimeMethodHandleInternal method);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern Resolver GetResolver(RuntimeMethodHandleInternal method);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetCallerType(StackCrawlMarkHandle stackMark, ObjectHandleOnStack retType);

    [SecuritySafeCritical]
    internal static RuntimeType GetCallerType(ref StackCrawlMark stackMark)
    {
      RuntimeType o = (RuntimeType) null;
      RuntimeMethodHandle.GetCallerType(JitHelpers.GetStackCrawlMarkHandle(ref stackMark), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref o));
      return o;
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern MethodBody GetMethodBody(IRuntimeMethodInfo method, RuntimeType declaringType);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsConstructor(RuntimeMethodHandleInternal method);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern LoaderAllocator GetLoaderAllocator(RuntimeMethodHandleInternal method);
  }
}
