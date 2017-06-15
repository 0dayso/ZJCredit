// Decompiled with JetBrains decompiler
// Type: System.RuntimeTypeHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System
{
  /// <summary>表示使用内部元数据标记的类型。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct RuntimeTypeHandle : ISerializable
  {
    private RuntimeType m_type;

    internal static RuntimeTypeHandle EmptyHandle
    {
      get
      {
        return new RuntimeTypeHandle((RuntimeType) null);
      }
    }

    /// <summary>获取此实例所表示的类型的句柄。</summary>
    /// <returns>此实例所表示的类型的句柄。</returns>
    /// <filterpriority>2</filterpriority>
    public IntPtr Value
    {
      [SecurityCritical] get
      {
        if (!(this.m_type != (RuntimeType) null))
          return IntPtr.Zero;
        return this.m_type.m_handle;
      }
    }

    internal RuntimeTypeHandle(RuntimeType type)
    {
      this.m_type = type;
    }

    [SecurityCritical]
    private RuntimeTypeHandle(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      this.m_type = (RuntimeType) info.GetValue("TypeObj", typeof (RuntimeType));
      if (this.m_type == (RuntimeType) null)
        throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
    }

    /// <summary>指示 <see cref="T:System.RuntimeTypeHandle" /> 结构与某个对象是否相等。</summary>
    /// <returns>如果 <paramref name="right" /> 是 <see cref="T:System.RuntimeTypeHandle" /> 并且与 <paramref name="left" /> 相等，则为 true；否则为 false。</returns>
    /// <param name="left">要与 <paramref name="right" /> 比较的 <see cref="T:System.RuntimeTypeHandle" /> 结构。</param>
    /// <param name="right">要与 <paramref name="left" /> 比较的对象。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator ==(RuntimeTypeHandle left, object right)
    {
      return left.Equals(right);
    }

    /// <summary>指示某个对象与 <see cref="T:System.RuntimeTypeHandle" /> 结构是否相等。</summary>
    /// <returns>如果 <paramref name="left" /> 是 <see cref="T:System.RuntimeTypeHandle" /> 结构并且与 <paramref name="right" /> 相等，则为 true；否则为 false。</returns>
    /// <param name="left">要与 <paramref name="right" /> 比较的对象。</param>
    /// <param name="right">要与 <paramref name="left" /> 比较的 <see cref="T:System.RuntimeTypeHandle" /> 结构。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator ==(object left, RuntimeTypeHandle right)
    {
      return right.Equals(left);
    }

    /// <summary>指示 <see cref="T:System.RuntimeTypeHandle" /> 结构与某个对象是否不相等。</summary>
    /// <returns>如果 <paramref name="right" /> 是 <see cref="T:System.RuntimeTypeHandle" /> 结构并且它与 <paramref name="left" /> 不相等，则为 true；否则为 false。</returns>
    /// <param name="left">要与 <paramref name="right" /> 比较的 <see cref="T:System.RuntimeTypeHandle" /> 结构。</param>
    /// <param name="right">要与 <paramref name="left" /> 比较的对象。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator !=(RuntimeTypeHandle left, object right)
    {
      return !left.Equals(right);
    }

    /// <summary>指示某个对象与 <see cref="T:System.RuntimeTypeHandle" /> 结构是否不相等。</summary>
    /// <returns>如果 <paramref name="left" /> 是 <see cref="T:System.RuntimeTypeHandle" /> 并且与 <paramref name="right" /> 相等，则为 true；否则为 false。</returns>
    /// <param name="left">要与 <paramref name="right" /> 比较的对象。</param>
    /// <param name="right">要与 <paramref name="left" /> 比较的 <see cref="T:System.RuntimeTypeHandle" /> 结构。</param>
    /// <filterpriority>3</filterpriority>
    [__DynamicallyInvokable]
    public static bool operator !=(object left, RuntimeTypeHandle right)
    {
      return !right.Equals(left);
    }

    internal RuntimeTypeHandle GetNativeHandle()
    {
      RuntimeType type = this.m_type;
      // ISSUE: variable of the null type
      __Null local = null;
      if (type == (RuntimeType) local)
        throw new ArgumentNullException((string) null, Environment.GetResourceString("Arg_InvalidHandle"));
      return new RuntimeTypeHandle(type);
    }

    internal RuntimeType GetTypeChecked()
    {
      RuntimeType runtimeType = this.m_type;
      // ISSUE: variable of the null type
      __Null local = null;
      if (!(runtimeType == (RuntimeType) local))
        return runtimeType;
      throw new ArgumentNullException((string) null, Environment.GetResourceString("Arg_InvalidHandle"));
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsInstanceOfType(RuntimeType type, object o);

    [SecuritySafeCritical]
    internal static unsafe Type GetTypeHelper(Type typeStart, Type[] genericArgs, IntPtr pModifiers, int cModifiers)
    {
      Type type = typeStart;
      if (genericArgs != null)
        type = type.MakeGenericType(genericArgs);
      if (cModifiers > 0)
      {
        int* numPtr = (int*) pModifiers.ToPointer();
        for (int index = 0; index < cModifiers; ++index)
          type = (int) (byte) Marshal.ReadInt32((IntPtr) ((void*) numPtr), index * 4) != 15 ? ((int) (byte) Marshal.ReadInt32((IntPtr) ((void*) numPtr), index * 4) != 16 ? ((int) (byte) Marshal.ReadInt32((IntPtr) ((void*) numPtr), index * 4) != 29 ? type.MakeArrayType(Marshal.ReadInt32((IntPtr) ((void*) numPtr), ++index * 4)) : type.MakeArrayType()) : type.MakeByRefType()) : type.MakePointerType();
      }
      return type;
    }

    /// <summary>返回当前实例的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      if (!(this.m_type != (RuntimeType) null))
        return 0;
      return this.m_type.GetHashCode();
    }

    /// <summary>指示指定的对象是否等于当前的 <see cref="T:System.RuntimeTypeHandle" /> 结构。</summary>
    /// <returns>如果 <paramref name="obj" /> 是 <see cref="T:System.RuntimeTypeHandle" /> 结构并且与此实例的值相等，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前实例进行比较的对象。</param>
    /// <filterpriority>2</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (!(obj is RuntimeTypeHandle))
        return false;
      return ((RuntimeTypeHandle) obj).m_type == this.m_type;
    }

    /// <summary>指示指定的 <see cref="T:System.RuntimeTypeHandle" /> 结构是否等于当前的 <see cref="T:System.RuntimeTypeHandle" /> 结构。</summary>
    /// <returns>如果 <paramref name="handle" /> 的值等于此实例的值，则为 true；否则为 false。</returns>
    /// <param name="handle">要与当前实例进行比较的 <see cref="T:System.RuntimeTypeHandle" /> 结构。</param>
    /// <filterpriority>2</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public bool Equals(RuntimeTypeHandle handle)
    {
      return handle.m_type == this.m_type;
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr GetValueInternal(RuntimeTypeHandle handle);

    internal bool IsNullHandle()
    {
      return this.m_type == (RuntimeType) null;
    }

    [SecuritySafeCritical]
    internal static bool IsPrimitive(RuntimeType type)
    {
      CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
      if ((corElementType < CorElementType.Boolean || corElementType > CorElementType.R8) && corElementType != CorElementType.I)
        return corElementType == CorElementType.U;
      return true;
    }

    [SecuritySafeCritical]
    internal static bool IsByRef(RuntimeType type)
    {
      return RuntimeTypeHandle.GetCorElementType(type) == CorElementType.ByRef;
    }

    [SecuritySafeCritical]
    internal static bool IsPointer(RuntimeType type)
    {
      return RuntimeTypeHandle.GetCorElementType(type) == CorElementType.Ptr;
    }

    [SecuritySafeCritical]
    internal static bool IsArray(RuntimeType type)
    {
      CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
      if (corElementType != CorElementType.Array)
        return corElementType == CorElementType.SzArray;
      return true;
    }

    [SecuritySafeCritical]
    internal static bool IsSzArray(RuntimeType type)
    {
      return RuntimeTypeHandle.GetCorElementType(type) == CorElementType.SzArray;
    }

    [SecuritySafeCritical]
    internal static bool HasElementType(RuntimeType type)
    {
      CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
      switch (corElementType)
      {
        case CorElementType.Array:
        case CorElementType.SzArray:
        case CorElementType.Ptr:
          return true;
        default:
          return corElementType == CorElementType.ByRef;
      }
    }

    [SecurityCritical]
    internal static IntPtr[] CopyRuntimeTypeHandles(RuntimeTypeHandle[] inHandles, out int length)
    {
      if (inHandles == null || inHandles.Length == 0)
      {
        length = 0;
        return (IntPtr[]) null;
      }
      IntPtr[] numArray = new IntPtr[inHandles.Length];
      for (int index = 0; index < inHandles.Length; ++index)
        numArray[index] = inHandles[index].Value;
      length = numArray.Length;
      return numArray;
    }

    [SecurityCritical]
    internal static IntPtr[] CopyRuntimeTypeHandles(Type[] inHandles, out int length)
    {
      if (inHandles == null || inHandles.Length == 0)
      {
        length = 0;
        return (IntPtr[]) null;
      }
      IntPtr[] numArray = new IntPtr[inHandles.Length];
      for (int index = 0; index < inHandles.Length; ++index)
        numArray[index] = inHandles[index].GetTypeHandleInternal().Value;
      length = numArray.Length;
      return numArray;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object CreateInstance(RuntimeType type, bool publicOnly, bool noCheck, ref bool canBeCached, ref RuntimeMethodHandleInternal ctor, ref bool bNeedSecurityCheck);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object CreateCaInstance(RuntimeType type, IRuntimeMethodInfo ctor);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object Allocate(RuntimeType type);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object CreateInstanceForAnotherGenericParameter(RuntimeType type, RuntimeType genericParameter);

    internal RuntimeType GetRuntimeType()
    {
      return this.m_type;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern CorElementType GetCorElementType(RuntimeType type);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeAssembly GetAssembly(RuntimeType type);

    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeModule GetModule(RuntimeType type);

    /// <summary>获取包含当前实例所表示类型的模块的句柄。</summary>
    /// <returns>
    /// <see cref="T:System.ModuleHandle" /> 结构，表示包含当前实例所表示类型的模块的句柄。</returns>
    /// <filterpriority>1</filterpriority>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public ModuleHandle GetModuleHandle()
    {
      return new ModuleHandle(RuntimeTypeHandle.GetModule(this.m_type));
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeType GetBaseType(RuntimeType type);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern TypeAttributes GetAttributes(RuntimeType type);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeType GetElementType(RuntimeType type);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool CompareCanonicalHandles(RuntimeType left, RuntimeType right);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetArrayRank(RuntimeType type);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetToken(RuntimeType type);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeMethodHandleInternal GetMethodAt(RuntimeType type, int slot);

    internal static RuntimeTypeHandle.IntroducedMethodEnumerator GetIntroducedMethods(RuntimeType type)
    {
      return new RuntimeTypeHandle.IntroducedMethodEnumerator(type);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern RuntimeMethodHandleInternal GetFirstIntroducedMethod(RuntimeType type);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void GetNextIntroducedMethod(ref RuntimeMethodHandleInternal method);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern unsafe bool GetFields(RuntimeType type, IntPtr* result, int* count);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern Type[] GetInterfaces(RuntimeType type);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetConstraints(RuntimeTypeHandle handle, ObjectHandleOnStack types);

    [SecuritySafeCritical]
    internal Type[] GetConstraints()
    {
      Type[] o = (Type[]) null;
      RuntimeTypeHandle.GetConstraints(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<Type[]>(ref o));
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern IntPtr GetGCHandle(RuntimeTypeHandle handle, GCHandleType type);

    [SecurityCritical]
    internal IntPtr GetGCHandle(GCHandleType type)
    {
      return RuntimeTypeHandle.GetGCHandle(this.GetNativeHandle(), type);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetNumVirtuals(RuntimeType type);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void VerifyInterfaceIsImplemented(RuntimeTypeHandle handle, RuntimeTypeHandle interfaceHandle);

    [SecuritySafeCritical]
    internal void VerifyInterfaceIsImplemented(RuntimeTypeHandle interfaceHandle)
    {
      RuntimeTypeHandle.VerifyInterfaceIsImplemented(this.GetNativeHandle(), interfaceHandle.GetNativeHandle());
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int GetInterfaceMethodImplementationSlot(RuntimeTypeHandle handle, RuntimeTypeHandle interfaceHandle, RuntimeMethodHandleInternal interfaceMethodHandle);

    [SecuritySafeCritical]
    internal int GetInterfaceMethodImplementationSlot(RuntimeTypeHandle interfaceHandle, RuntimeMethodHandleInternal interfaceMethodHandle)
    {
      return RuntimeTypeHandle.GetInterfaceMethodImplementationSlot(this.GetNativeHandle(), interfaceHandle.GetNativeHandle(), interfaceMethodHandle);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsComObject(RuntimeType type, bool isGenericCOM);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsContextful(RuntimeType type);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsInterface(RuntimeType type);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool _IsVisible(RuntimeTypeHandle typeHandle);

    [SecuritySafeCritical]
    internal static bool IsVisible(RuntimeType type)
    {
      return RuntimeTypeHandle._IsVisible(new RuntimeTypeHandle(type));
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsSecurityCritical(RuntimeTypeHandle typeHandle);

    [SecuritySafeCritical]
    internal bool IsSecurityCritical()
    {
      return RuntimeTypeHandle.IsSecurityCritical(this.GetNativeHandle());
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsSecuritySafeCritical(RuntimeTypeHandle typeHandle);

    [SecuritySafeCritical]
    internal bool IsSecuritySafeCritical()
    {
      return RuntimeTypeHandle.IsSecuritySafeCritical(this.GetNativeHandle());
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsSecurityTransparent(RuntimeTypeHandle typeHandle);

    [SecuritySafeCritical]
    internal bool IsSecurityTransparent()
    {
      return RuntimeTypeHandle.IsSecurityTransparent(this.GetNativeHandle());
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool HasProxyAttribute(RuntimeType type);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsValueType(RuntimeType type);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void ConstructName(RuntimeTypeHandle handle, TypeNameFormatFlags formatFlags, StringHandleOnStack retString);

    [SecuritySafeCritical]
    internal string ConstructName(TypeNameFormatFlags formatFlags)
    {
      string s = (string) null;
      RuntimeTypeHandle.ConstructName(this.GetNativeHandle(), formatFlags, JitHelpers.GetStringHandleOnStack(ref s));
      return s;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe void* _GetUtf8Name(RuntimeType type);

    [SecuritySafeCritical]
    internal static unsafe Utf8String GetUtf8Name(RuntimeType type)
    {
      return new Utf8String(RuntimeTypeHandle._GetUtf8Name(type));
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool CanCastTo(RuntimeType type, RuntimeType target);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeType GetDeclaringType(RuntimeType type);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IRuntimeMethodInfo GetDeclaringMethod(RuntimeType type);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetDefaultConstructor(RuntimeTypeHandle handle, ObjectHandleOnStack method);

    [SecuritySafeCritical]
    internal IRuntimeMethodInfo GetDefaultConstructor()
    {
      IRuntimeMethodInfo o = (IRuntimeMethodInfo) null;
      RuntimeTypeHandle.GetDefaultConstructor(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<IRuntimeMethodInfo>(ref o));
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetTypeByName(string name, bool throwOnError, bool ignoreCase, bool reflectionOnly, StackCrawlMarkHandle stackMark, IntPtr pPrivHostBinder, bool loadTypeFromPartialName, ObjectHandleOnStack type);

    internal static RuntimeType GetTypeByName(string name, bool throwOnError, bool ignoreCase, bool reflectionOnly, ref StackCrawlMark stackMark, bool loadTypeFromPartialName)
    {
      return RuntimeTypeHandle.GetTypeByName(name, throwOnError, ignoreCase, reflectionOnly, ref stackMark, IntPtr.Zero, loadTypeFromPartialName);
    }

    [SecuritySafeCritical]
    internal static RuntimeType GetTypeByName(string name, bool throwOnError, bool ignoreCase, bool reflectionOnly, ref StackCrawlMark stackMark, IntPtr pPrivHostBinder, bool loadTypeFromPartialName)
    {
      if (name == null || name.Length == 0)
      {
        if (throwOnError)
          throw new TypeLoadException(Environment.GetResourceString("Arg_TypeLoadNullStr"));
        return (RuntimeType) null;
      }
      RuntimeType o = (RuntimeType) null;
      RuntimeTypeHandle.GetTypeByName(name, throwOnError, ignoreCase, reflectionOnly, JitHelpers.GetStackCrawlMarkHandle(ref stackMark), pPrivHostBinder, loadTypeFromPartialName, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref o));
      return o;
    }

    internal static Type GetTypeByName(string name, ref StackCrawlMark stackMark)
    {
      return (Type) RuntimeTypeHandle.GetTypeByName(name, false, false, false, ref stackMark, false);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetTypeByNameUsingCARules(string name, RuntimeModule scope, ObjectHandleOnStack type);

    [SecuritySafeCritical]
    internal static RuntimeType GetTypeByNameUsingCARules(string name, RuntimeModule scope)
    {
      if (name == null || name.Length == 0)
        throw new ArgumentException("name");
      RuntimeType o = (RuntimeType) null;
      RuntimeTypeHandle.GetTypeByNameUsingCARules(name, scope.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref o));
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void GetInstantiation(RuntimeTypeHandle type, ObjectHandleOnStack types, bool fAsRuntimeTypeArray);

    [SecuritySafeCritical]
    internal RuntimeType[] GetInstantiationInternal()
    {
      RuntimeType[] o = (RuntimeType[]) null;
      RuntimeTypeHandle.GetInstantiation(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType[]>(ref o), true);
      return o;
    }

    [SecuritySafeCritical]
    internal Type[] GetInstantiationPublic()
    {
      Type[] o = (Type[]) null;
      RuntimeTypeHandle.GetInstantiation(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<Type[]>(ref o), false);
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern unsafe void Instantiate(RuntimeTypeHandle handle, IntPtr* pInst, int numGenericArgs, ObjectHandleOnStack type);

    [SecurityCritical]
    internal unsafe RuntimeType Instantiate(Type[] inst)
    {
      int length;
      fixed (IntPtr* pInst = RuntimeTypeHandle.CopyRuntimeTypeHandles(inst, out length))
      {
        RuntimeType o = (RuntimeType) null;
        RuntimeTypeHandle.Instantiate(this.GetNativeHandle(), pInst, length, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref o));
        GC.KeepAlive((object) inst);
        return o;
      }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void MakeArray(RuntimeTypeHandle handle, int rank, ObjectHandleOnStack type);

    [SecuritySafeCritical]
    internal RuntimeType MakeArray(int rank)
    {
      RuntimeType o = (RuntimeType) null;
      RuntimeTypeHandle.MakeArray(this.GetNativeHandle(), rank, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref o));
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void MakeSZArray(RuntimeTypeHandle handle, ObjectHandleOnStack type);

    [SecuritySafeCritical]
    internal RuntimeType MakeSZArray()
    {
      RuntimeType o = (RuntimeType) null;
      RuntimeTypeHandle.MakeSZArray(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref o));
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void MakeByRef(RuntimeTypeHandle handle, ObjectHandleOnStack type);

    [SecuritySafeCritical]
    internal RuntimeType MakeByRef()
    {
      RuntimeType o = (RuntimeType) null;
      RuntimeTypeHandle.MakeByRef(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref o));
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void MakePointer(RuntimeTypeHandle handle, ObjectHandleOnStack type);

    [SecurityCritical]
    internal RuntimeType MakePointer()
    {
      RuntimeType o = (RuntimeType) null;
      RuntimeTypeHandle.MakePointer(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref o));
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern bool IsCollectible(RuntimeTypeHandle handle);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool HasInstantiation(RuntimeType type);

    internal bool HasInstantiation()
    {
      return RuntimeTypeHandle.HasInstantiation(this.GetTypeChecked());
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetGenericTypeDefinition(RuntimeTypeHandle type, ObjectHandleOnStack retType);

    [SecuritySafeCritical]
    internal static RuntimeType GetGenericTypeDefinition(RuntimeType type)
    {
      RuntimeType o = type;
      if (RuntimeTypeHandle.HasInstantiation(o) && !RuntimeTypeHandle.IsGenericTypeDefinition(o))
        RuntimeTypeHandle.GetGenericTypeDefinition(o.GetTypeHandleInternal(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref o));
      return o;
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsGenericTypeDefinition(RuntimeType type);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsGenericVariable(RuntimeType type);

    internal bool IsGenericVariable()
    {
      return RuntimeTypeHandle.IsGenericVariable(this.GetTypeChecked());
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int GetGenericVariableIndex(RuntimeType type);

    [SecuritySafeCritical]
    internal int GetGenericVariableIndex()
    {
      RuntimeType typeChecked = this.GetTypeChecked();
      if (!RuntimeTypeHandle.IsGenericVariable(typeChecked))
        throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericParameter"));
      return RuntimeTypeHandle.GetGenericVariableIndex(typeChecked);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool ContainsGenericVariables(RuntimeType handle);

    [SecuritySafeCritical]
    internal bool ContainsGenericVariables()
    {
      return RuntimeTypeHandle.ContainsGenericVariables(this.GetTypeChecked());
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe bool SatisfiesConstraints(RuntimeType paramType, IntPtr* pTypeContext, int typeContextLength, IntPtr* pMethodContext, int methodContextLength, RuntimeType toType);

    [SecurityCritical]
    internal static unsafe bool SatisfiesConstraints(RuntimeType paramType, RuntimeType[] typeContext, RuntimeType[] methodContext, RuntimeType toType)
    {
      int length1;
      IntPtr[] numArray1 = RuntimeTypeHandle.CopyRuntimeTypeHandles((Type[]) typeContext, out length1);
      int length2;
      IntPtr[] numArray2 = RuntimeTypeHandle.CopyRuntimeTypeHandles((Type[]) methodContext, out length2);
      IntPtr[] numArray3 = numArray1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      fixed (IntPtr* pTypeContext = &^(numArray1 == null || numArray3.Length == 0 ? (IntPtr&) IntPtr.Zero : @numArray3[0]))
        fixed (IntPtr* pMethodContext = numArray2)
        {
          int num = RuntimeTypeHandle.SatisfiesConstraints(paramType, pTypeContext, length1, pMethodContext, length2, toType) ? 1 : 0;
          GC.KeepAlive((object) typeContext);
          GC.KeepAlive((object) methodContext);
          return num != 0;
        }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern IntPtr _GetMetadataImport(RuntimeType type);

    [SecurityCritical]
    internal static MetadataImport GetMetadataImport(RuntimeType type)
    {
      return new MetadataImport(RuntimeTypeHandle._GetMetadataImport(type), (object) type);
    }

    /// <summary>用反序列化当前实例表示的类型所必需的数据填充 <see cref="T:System.Runtime.Serialization.SerializationInfo" />。</summary>
    /// <param name="info">用序列化信息填充的对象。</param>
    /// <param name="context">（保留）存储和检索序列化数据的位置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    /// <see cref="P:System.RuntimeTypeHandle.Value" /> 无效。</exception>
    /// <filterpriority>2</filterpriority>
    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      if (this.m_type == (RuntimeType) null)
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidFieldState"));
      info.AddValue("TypeObj", (object) this.m_type, typeof (RuntimeType));
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsEquivalentTo(RuntimeType rtType1, RuntimeType rtType2);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsEquivalentType(RuntimeType type);

    internal struct IntroducedMethodEnumerator
    {
      private bool _firstCall;
      private RuntimeMethodHandleInternal _handle;

      public RuntimeMethodHandleInternal Current
      {
        get
        {
          return this._handle;
        }
      }

      [SecuritySafeCritical]
      internal IntroducedMethodEnumerator(RuntimeType type)
      {
        this._handle = RuntimeTypeHandle.GetFirstIntroducedMethod(type);
        this._firstCall = true;
      }

      [SecuritySafeCritical]
      public bool MoveNext()
      {
        if (this._firstCall)
          this._firstCall = false;
        else if (this._handle.Value != IntPtr.Zero)
          RuntimeTypeHandle.GetNextIntroducedMethod(ref this._handle);
        return !(this._handle.Value == IntPtr.Zero);
      }

      public RuntimeTypeHandle.IntroducedMethodEnumerator GetEnumerator()
      {
        return this;
      }
    }
  }
}
