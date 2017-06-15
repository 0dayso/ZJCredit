// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.RuntimeHelpers
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System.Runtime.CompilerServices
{
  /// <summary>提供一组为编译器提供支持的静态方法和属性。此类不能被继承。</summary>
  [__DynamicallyInvokable]
  public static class RuntimeHelpers
  {
    /// <summary>获取给定字符串中数据的偏移量（以字节为单位）。</summary>
    /// <returns>字节偏移量，从 <see cref="T:System.String" /> 对象的起始位置到字符串中的第一个字符。</returns>
    [__DynamicallyInvokable]
    public static int OffsetToStringData
    {
      [NonVersionable, __DynamicallyInvokable] get
      {
        return 8;
      }
    }

    /// <summary>提供从存储在模块中的数据初始化数组的快速方法。</summary>
    /// <param name="array">要初始化的数组。</param>
    /// <param name="fldHandle">一个字段句柄，它指定用于初始化数组的数据的位置。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void InitializeArray(Array array, RuntimeFieldHandle fldHandle);

    /// <summary>将值类型装箱。</summary>
    /// <returns>如果 <paramref name="obj" /> 是一个值类，则返回其装箱的副本；否则返回 <paramref name="obj" /> 本身。</returns>
    /// <param name="obj">要装箱的值类型。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern object GetObjectValue(object obj);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _RunClassConstructor(RuntimeType type);

    /// <summary>运行指定的类构造函数方法。</summary>
    /// <param name="type">一个用于指定要运行的类构造函数方法的类型句柄。</param>
    /// <exception cref="T:System.TypeInitializationException">在类初始值设定项引发异常。</exception>
    [__DynamicallyInvokable]
    public static void RunClassConstructor(RuntimeTypeHandle type)
    {
      RuntimeHelpers._RunClassConstructor(type.GetRuntimeType());
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _RunModuleConstructor(RuntimeModule module);

    /// <summary>运行指定的模块构造函数方法。</summary>
    /// <param name="module">一个用于指定要运行的模块构造函数方法的句柄。</param>
    /// <exception cref="T:System.TypeInitializationException">模块构造函数引发了一个异常。</exception>
    public static void RunModuleConstructor(ModuleHandle module)
    {
      RuntimeHelpers._RunModuleConstructor(module.GetRuntimeModule());
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe void _PrepareMethod(IRuntimeMethodInfo method, IntPtr* pInstantiation, int cInstantiation);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void _CompileMethod(IRuntimeMethodInfo method);

    /// <summary>准备一个要包含在受约束的执行区域 (CER) 中的方法。</summary>
    /// <param name="method">要准备的方法的句柄。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static unsafe void PrepareMethod(RuntimeMethodHandle method)
    {
      RuntimeHelpers._PrepareMethod(method.GetMethodInfo(), (IntPtr*) null, 0);
    }

    /// <summary>准备一个要包含在受约束的执行区域 (CER) 中的具有指定实例化的方法。</summary>
    /// <param name="method">要准备的方法的句柄。</param>
    /// <param name="instantiation">要传递给该方法的实例化。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    public static unsafe void PrepareMethod(RuntimeMethodHandle method, RuntimeTypeHandle[] instantiation)
    {
      int length;
      fixed (IntPtr* pInstantiation = RuntimeTypeHandle.CopyRuntimeTypeHandles(instantiation, out length))
      {
        RuntimeHelpers._PrepareMethod(method.GetMethodInfo(), pInstantiation, length);
        GC.KeepAlive((object) instantiation);
      }
    }

    /// <summary>指示应准备指定委托以包含在受约束的执行区域 (CER) 中。</summary>
    /// <param name="d">要准备的委托类型。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void PrepareDelegate(Delegate d);

    /// <summary>提供应用程序用来动态准备 <see cref="T:System.AppDomain" /> 事件委托的方法。</summary>
    /// <param name="d">要准备的事件委托。</param>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void PrepareContractedDelegate(Delegate d);

    /// <summary>用作特定对象的哈希函数，适合在使用哈希代码的算法和数据结构（如哈希表）中使用。</summary>
    /// <returns>
    /// <paramref name="o" /> 参数标识的对象的哈希代码。</returns>
    /// <param name="o">要检索其哈希代码的对象。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int GetHashCode(object o);

    /// <summary>确定指定的 <see cref="T:System.Object" /> 实例是否被视为相等。</summary>
    /// <returns>如果 <paramref name="o1" /> 参数与 <paramref name="o2" /> 参数是同一个实例，或者二者均为 null，或者 o1.Equals(o2) 返回 true，则为 true；否则为 false。</returns>
    /// <param name="o1">要比较的第一个对象。</param>
    /// <param name="o2">要比较的第二个对象。</param>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public new static extern bool Equals(object o1, object o2);

    /// <summary>确保剩余的堆栈空间足够大，可以执行一般的 .NET Framework 函数。</summary>
    /// <exception cref="T:System.InsufficientExecutionStackException">可用堆栈空间足以执行一般的 .NET Framework 函数。</exception>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void EnsureSufficientExecutionStack();

    /// <summary>探测某个数量的堆栈空间，以确保不会在后续的代码块内发生堆栈溢出（假设您的代码仅使用有限适中的堆栈空间）。建议使用受约束的执行区域 (CER)，而不使用此方法。</summary>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void ProbeForSufficientStack();

    /// <summary>将代码体指定为受约束的执行区域 (CER)。</summary>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public static void PrepareConstrainedRegions()
    {
      RuntimeHelpers.ProbeForSufficientStack();
    }

    /// <summary>指定代码体为受约束的执行区域 (CER)，而不执行任何探测。</summary>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public static void PrepareConstrainedRegionsNoOP()
    {
    }

    /// <summary>使用一个 <see cref="T:System.Delegate" /> 执行代码，同时使用另一个 <see cref="T:System.Delegate" /> 在异常情况下执行附加代码。</summary>
    /// <param name="code">要尝试的代码的委托。</param>
    /// <param name="backoutCode">在发生异常时要运行的代码的委托。</param>
    /// <param name="userData">要传递给 <paramref name="code" /> 和 <paramref name="backoutCode" /> 的数据。</param>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void ExecuteCodeWithGuaranteedCleanup(RuntimeHelpers.TryCode code, RuntimeHelpers.CleanupCode backoutCode, object userData);

    [PrePrepareMethod]
    internal static void ExecuteBackoutCodeHelper(object backoutCode, object userData, bool exceptionThrown)
    {
      ((RuntimeHelpers.CleanupCode) backoutCode)(userData, exceptionThrown);
    }

    /// <summary>表示应该在 try 块中运行的代码的委托。</summary>
    /// <param name="userData">要传递给委托的数据。</param>
    public delegate void TryCode(object userData);

    /// <summary>表示在发生异常时要运行的方法。</summary>
    /// <param name="userData">要传递给委托的数据。</param>
    /// <param name="exceptionThrown">true 表示引发了异常；如果未引发异常，则为 false。</param>
    public delegate void CleanupCode(object userData, bool exceptionThrown);
  }
}
