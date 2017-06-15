// Decompiled with JetBrains decompiler
// Type: System.ModuleHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
  /// <summary>表示模块的运行时句柄。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  public struct ModuleHandle
  {
    /// <summary>表示一个空模块句柄。</summary>
    /// <filterpriority>1</filterpriority>
    public static readonly ModuleHandle EmptyHandle = ModuleHandle.GetEmptyMH();
    private RuntimeModule m_ptr;

    /// <summary>获取元数据流版本。</summary>
    /// <returns>表示元数据流版本的 32 位整数。高序位的两个字节表示主版本号，低序位的两个字节表示次版本号。</returns>
    public int MDStreamVersion
    {
      [SecuritySafeCritical] get
      {
        return ModuleHandle.GetMDStreamVersion(this.GetRuntimeModule().GetNativeHandle());
      }
    }

    internal ModuleHandle(RuntimeModule module)
    {
      this.m_ptr = module;
    }

    /// <summary>测试两个 <see cref="T:System.ModuleHandle" /> 结构是否相等。</summary>
    /// <returns>如果 <see cref="T:System.ModuleHandle" /> 结构相等，则为 true；否则为 false。</returns>
    /// <param name="left">相等运算符左侧的 <see cref="T:System.ModuleHandle" /> 结构。</param>
    /// <param name="right">相等运算符右侧的 <see cref="T:System.ModuleHandle" /> 结构。</param>
    /// <filterpriority>3</filterpriority>
    public static bool operator ==(ModuleHandle left, ModuleHandle right)
    {
      return left.Equals(right);
    }

    /// <summary>测试两个 <see cref="T:System.ModuleHandle" /> 结构是否相等。</summary>
    /// <returns>如果 <see cref="T:System.ModuleHandle" /> 结构不相等，则为 true；否则为 false。</returns>
    /// <param name="left">不等运算符左侧的 <see cref="T:System.ModuleHandle" /> 结构。</param>
    /// <param name="right">不等运算符右侧的 <see cref="T:System.ModuleHandle" /> 结构。</param>
    /// <filterpriority>3</filterpriority>
    public static bool operator !=(ModuleHandle left, ModuleHandle right)
    {
      return !left.Equals(right);
    }

    private static ModuleHandle GetEmptyMH()
    {
      return new ModuleHandle();
    }

    internal RuntimeModule GetRuntimeModule()
    {
      return this.m_ptr;
    }

    internal bool IsNullHandle()
    {
      return (Module) this.m_ptr == (Module) null;
    }

    /// <filterpriority>2</filterpriority>
    public override int GetHashCode()
    {
      if (!((Module) this.m_ptr != (Module) null))
        return 0;
      return this.m_ptr.GetHashCode();
    }

    /// <summary>返回一个 <see cref="T:System.Boolean" /> 值，该值指示指定对象是否是一个 <see cref="T:System.ModuleHandle" /> 结构，以及是否等于当前的 <see cref="T:System.ModuleHandle" />。</summary>
    /// <returns>如果 <paramref name="obj" /> 是 <see cref="T:System.ModuleHandle" /> 结构，且等于当前 <see cref="T:System.ModuleHandle" /> 结构，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前 <see cref="T:System.ModuleHandle" /> 结构比较的对象。</param>
    /// <filterpriority>2</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public override bool Equals(object obj)
    {
      if (!(obj is ModuleHandle))
        return false;
      return (Module) ((ModuleHandle) obj).m_ptr == (Module) this.m_ptr;
    }

    /// <summary>返回一个 <see cref="T:System.Boolean" /> 值，该值指示指定的 <see cref="T:System.ModuleHandle" /> 结构是否等于当前的 <see cref="T:System.ModuleHandle" />。</summary>
    /// <returns>如果 <paramref name="handle" /> 等于当前的 <see cref="T:System.ModuleHandle" /> 结构，则为 true；否则为 false。</returns>
    /// <param name="handle">要与当前 <see cref="T:System.ModuleHandle" /> 比较的 <see cref="T:System.ModuleHandle" /> 结构。</param>
    /// <filterpriority>2</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public bool Equals(ModuleHandle handle)
    {
      return (Module) handle.m_ptr == (Module) this.m_ptr;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IRuntimeMethodInfo GetDynamicMethod(DynamicMethod method, RuntimeModule module, string name, byte[] sig, Resolver resolver);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetToken(RuntimeModule module);

    private static void ValidateModulePointer(RuntimeModule module)
    {
      if ((Module) module == (Module) null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullModuleHandle"));
    }

    /// <summary>返回由指定元数据标记标识的类型的运行时类型句柄。</summary>
    /// <returns>由 <paramref name="typeToken" /> 标识的类型的 <see cref="T:System.RuntimeTypeHandle" />。</returns>
    /// <param name="typeToken">一个元数据标记，用于标识模块中的一个类型。</param>
    /// <filterpriority>2</filterpriority>
    public RuntimeTypeHandle GetRuntimeTypeHandleFromMetadataToken(int typeToken)
    {
      return this.ResolveTypeHandle(typeToken);
    }

    /// <summary>返回由指定元数据标记标识的类型的运行时类型句柄。</summary>
    /// <returns>由 <paramref name="typeToken" /> 标识的类型的 <see cref="T:System.RuntimeTypeHandle" />。</returns>
    /// <param name="typeToken">一个元数据标记，用于标识模块中的一个类型。</param>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="typeToken" /> 不是当前模块中类型的有效元数据标记。- 或 -<paramref name="metadataToken" /> 不是当前模块范围内的类型的标记。- 或 -<paramref name="metadataToken" /> 是一个 TypeSpec，它的签名中包含元素类型 var 或 mvar。</exception>
    /// <exception cref="T:System.InvalidOperationException">该方法在空类型句柄上调用。</exception>
    /// <filterpriority>1</filterpriority>
    public RuntimeTypeHandle ResolveTypeHandle(int typeToken)
    {
      return new RuntimeTypeHandle(ModuleHandle.ResolveTypeHandleInternal(this.GetRuntimeModule(), typeToken, (RuntimeTypeHandle[]) null, (RuntimeTypeHandle[]) null));
    }

    /// <summary>返回由指定元数据标记标识的类型的运行时类型句柄，指定标记所在范围内的类型和方法的泛型类型参数。</summary>
    /// <returns>由 <paramref name="typeToken" /> 标识的类型的 <see cref="T:System.RuntimeTypeHandle" />。</returns>
    /// <param name="typeToken">一个元数据标记，用于标识模块中的一个类型。</param>
    /// <param name="typeInstantiationContext">
    /// <see cref="T:System.RuntimeTypeHandle" /> 结构的数组，表示标记所在范围内的类型的泛型类型参数，如果类型不是泛型，则为 null。</param>
    /// <param name="methodInstantiationContext">
    /// <see cref="T:System.RuntimeTypeHandle" /> 结构对象的数组，表示标记所在范围内的方法的泛型类型参数，如果方法不是泛型，则为 null。</param>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="typeToken" /> 不是当前模块中类型的有效元数据标记。- 或 -<paramref name="metadataToken" /> 不是当前模块范围内的类型的标记。- 或 -<paramref name="metadataToken" /> 是一个 TypeSpec，它的签名中包含元素类型 var 或 mvar。</exception>
    /// <exception cref="T:System.InvalidOperationException">该方法在空类型句柄上调用。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="typeToken " />不是有效的标记。</exception>
    public RuntimeTypeHandle ResolveTypeHandle(int typeToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
    {
      return new RuntimeTypeHandle(ModuleHandle.ResolveTypeHandleInternal(this.GetRuntimeModule(), typeToken, typeInstantiationContext, methodInstantiationContext));
    }

    [SecuritySafeCritical]
    internal static unsafe RuntimeType ResolveTypeHandleInternal(RuntimeModule module, int typeToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
    {
      ModuleHandle.ValidateModulePointer(module);
      if (!ModuleHandle.GetMetadataImport(module).IsValidToken(typeToken))
        throw new ArgumentOutOfRangeException("metadataToken", Environment.GetResourceString("Argument_InvalidToken", (object) typeToken, (object) new ModuleHandle(module)));
      int length1;
      IntPtr[] numArray1 = RuntimeTypeHandle.CopyRuntimeTypeHandles(typeInstantiationContext, out length1);
      int length2;
      IntPtr[] numArray2 = RuntimeTypeHandle.CopyRuntimeTypeHandles(methodInstantiationContext, out length2);
      IntPtr[] numArray3 = numArray1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      fixed (IntPtr* typeInstArgs = &^(numArray1 == null || numArray3.Length == 0 ? (IntPtr&) IntPtr.Zero : @numArray3[0]))
        fixed (IntPtr* methodInstArgs = numArray2)
        {
          RuntimeType o = (RuntimeType) null;
          ModuleHandle.ResolveType(module, typeToken, typeInstArgs, length1, methodInstArgs, length2, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref o));
          GC.KeepAlive((object) typeInstantiationContext);
          GC.KeepAlive((object) methodInstantiationContext);
          return o;
        }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern unsafe void ResolveType(RuntimeModule module, int typeToken, IntPtr* typeInstArgs, int typeInstCount, IntPtr* methodInstArgs, int methodInstCount, ObjectHandleOnStack type);

    /// <summary>返回由指定元数据标记标识的方法或构造函数的运行时方法句柄。</summary>
    /// <returns>由 <paramref name="methodToken" /> 标识的方法或构造函数的 <see cref="T:System.RuntimeMethodHandle" />。</returns>
    /// <param name="methodToken">一个元数据标记，用于标识模块中的方法或构造函数。</param>
    /// <filterpriority>2</filterpriority>
    public RuntimeMethodHandle GetRuntimeMethodHandleFromMetadataToken(int methodToken)
    {
      return this.ResolveMethodHandle(methodToken);
    }

    /// <summary>返回由指定元数据标记标识的方法或构造函数的运行时方法句柄。</summary>
    /// <returns>由 <paramref name="methodToken" /> 标识的方法或构造函数的 <see cref="T:System.RuntimeMethodHandle" />。</returns>
    /// <param name="methodToken">一个元数据标记，用于标识模块中的方法或构造函数。</param>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="methodToken" /> 不是当前模块中方法的有效元数据标记。- 或 -<paramref name="metadataToken" /> 不是当前模块范围内的方法或构造函数的标记。- 或 -<paramref name="metadataToken" /> 是一个 MethodSpec，它的签名中包含元素类型 var 或 mvar。</exception>
    /// <exception cref="T:System.InvalidOperationException">该方法在空方法句柄上调用。</exception>
    /// <filterpriority>1</filterpriority>
    public RuntimeMethodHandle ResolveMethodHandle(int methodToken)
    {
      return this.ResolveMethodHandle(methodToken, (RuntimeTypeHandle[]) null, (RuntimeTypeHandle[]) null);
    }

    internal static IRuntimeMethodInfo ResolveMethodHandleInternal(RuntimeModule module, int methodToken)
    {
      return ModuleHandle.ResolveMethodHandleInternal(module, methodToken, (RuntimeTypeHandle[]) null, (RuntimeTypeHandle[]) null);
    }

    /// <summary>返回由指定元数据标记标识的方法或构造函数的运行时方法句柄，指定标记所在范围内的类型和方法的泛型类型参数。</summary>
    /// <returns>由 <paramref name="methodToken" /> 标识的方法或构造函数的 <see cref="T:System.RuntimeMethodHandle" />。</returns>
    /// <param name="methodToken">一个元数据标记，用于标识模块中的方法或构造函数。</param>
    /// <param name="typeInstantiationContext">
    /// <see cref="T:System.RuntimeTypeHandle" /> 结构的数组，表示标记所在范围内的类型的泛型类型参数，如果类型不是泛型，则为 null。</param>
    /// <param name="methodInstantiationContext">
    /// <see cref="T:System.RuntimeTypeHandle" /> 结构的数组，表示标记所在范围内的方法的泛型类型参数，如果方法不是泛型，则为 null。</param>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="methodToken" /> 不是当前模块中方法的有效元数据标记。- 或 -<paramref name="metadataToken" /> 不是当前模块范围内的方法或构造函数的标记。- 或 -<paramref name="metadataToken" /> 是一个 MethodSpec，它的签名中包含元素类型 var 或 mvar。</exception>
    /// <exception cref="T:System.InvalidOperationException">该方法在空方法句柄上调用。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="methodToken " />不是有效的标记。</exception>
    public RuntimeMethodHandle ResolveMethodHandle(int methodToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
    {
      return new RuntimeMethodHandle(ModuleHandle.ResolveMethodHandleInternal(this.GetRuntimeModule(), methodToken, typeInstantiationContext, methodInstantiationContext));
    }

    [SecuritySafeCritical]
    internal static IRuntimeMethodInfo ResolveMethodHandleInternal(RuntimeModule module, int methodToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
    {
      int length1;
      IntPtr[] typeInstantiationContext1 = RuntimeTypeHandle.CopyRuntimeTypeHandles(typeInstantiationContext, out length1);
      int length2;
      IntPtr[] methodInstantiationContext1 = RuntimeTypeHandle.CopyRuntimeTypeHandles(methodInstantiationContext, out length2);
      RuntimeMethodHandleInternal methodHandleInternal = ModuleHandle.ResolveMethodHandleInternalCore(module, methodToken, typeInstantiationContext1, length1, methodInstantiationContext1, length2);
      RuntimeMethodInfoStub runtimeMethodInfoStub = new RuntimeMethodInfoStub(methodHandleInternal, (object) RuntimeMethodHandle.GetLoaderAllocator(methodHandleInternal));
      GC.KeepAlive((object) typeInstantiationContext);
      GC.KeepAlive((object) methodInstantiationContext);
      return (IRuntimeMethodInfo) runtimeMethodInfoStub;
    }

    [SecurityCritical]
    internal static unsafe RuntimeMethodHandleInternal ResolveMethodHandleInternalCore(RuntimeModule module, int methodToken, IntPtr[] typeInstantiationContext, int typeInstCount, IntPtr[] methodInstantiationContext, int methodInstCount)
    {
      ModuleHandle.ValidateModulePointer(module);
      if (!ModuleHandle.GetMetadataImport(module.GetNativeHandle()).IsValidToken(methodToken))
        throw new ArgumentOutOfRangeException("metadataToken", Environment.GetResourceString("Argument_InvalidToken", (object) methodToken, (object) new ModuleHandle(module)));
      fixed (IntPtr* typeInstArgs = typeInstantiationContext)
        fixed (IntPtr* methodInstArgs = methodInstantiationContext)
          return ModuleHandle.ResolveMethod(module.GetNativeHandle(), methodToken, typeInstArgs, typeInstCount, methodInstArgs, methodInstCount);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern unsafe RuntimeMethodHandleInternal ResolveMethod(RuntimeModule module, int methodToken, IntPtr* typeInstArgs, int typeInstCount, IntPtr* methodInstArgs, int methodInstCount);

    /// <summary>返回由指定元数据标记标识的字段的运行时句柄。</summary>
    /// <returns>由 <paramref name="fieldToken" /> 标识的字段的 <see cref="T:System.RuntimeFieldHandle" />。</returns>
    /// <param name="fieldToken">一个元数据标记，用于标识模块中的一个字段。</param>
    /// <filterpriority>2</filterpriority>
    public RuntimeFieldHandle GetRuntimeFieldHandleFromMetadataToken(int fieldToken)
    {
      return this.ResolveFieldHandle(fieldToken);
    }

    /// <summary>返回由指定元数据标记标识的字段的运行时句柄。</summary>
    /// <returns>由 <paramref name="fieldToken" /> 标识的字段的 <see cref="T:System.RuntimeFieldHandle" />。</returns>
    /// <param name="fieldToken">一个元数据标记，用于标识模块中的一个字段。</param>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的有效标记。- 或 -<paramref name="metadataToken" /> 不是当前模块范围内的字段的标记。- 或 -<paramref name="metadataToken" /> 标识了一个字段，该字段的父 TypeSpec 有一个包含元素类型 var 或 mvar 的签名。</exception>
    /// <exception cref="T:System.InvalidOperationException">该方法在空字段句柄上调用。</exception>
    /// <filterpriority>1</filterpriority>
    public RuntimeFieldHandle ResolveFieldHandle(int fieldToken)
    {
      return new RuntimeFieldHandle(ModuleHandle.ResolveFieldHandleInternal(this.GetRuntimeModule(), fieldToken, (RuntimeTypeHandle[]) null, (RuntimeTypeHandle[]) null));
    }

    /// <summary>返回由指定元数据标记标识的字段的运行时字段句柄，指定标记所在范围内的类型和方法的泛型类型参数。</summary>
    /// <returns>由 <paramref name="fieldToken" /> 标识的字段的 <see cref="T:System.RuntimeFieldHandle" />。</returns>
    /// <param name="fieldToken">一个元数据标记，用于标识模块中的一个字段。</param>
    /// <param name="typeInstantiationContext">
    /// <see cref="T:System.RuntimeTypeHandle" /> 结构的数组，表示标记所在范围内的类型的泛型类型参数，如果类型不是泛型，则为 null。</param>
    /// <param name="methodInstantiationContext">
    /// <see cref="T:System.RuntimeTypeHandle" /> 结构的数组，表示标记所在范围内的方法的泛型类型参数，如果方法不是泛型，则为 null。</param>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="metadataToken" /> 不是当前模块范围内的有效标记。- 或 -<paramref name="metadataToken" /> 不是当前模块范围内的字段的标记。- 或 -<paramref name="metadataToken" /> 标识了一个字段，该字段的父 TypeSpec 有一个包含元素类型 var 或 mvar 的签名。</exception>
    /// <exception cref="T:System.InvalidOperationException">该方法在空字段句柄上调用。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="fieldToken " />不是有效的标记。</exception>
    public RuntimeFieldHandle ResolveFieldHandle(int fieldToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
    {
      return new RuntimeFieldHandle(ModuleHandle.ResolveFieldHandleInternal(this.GetRuntimeModule(), fieldToken, typeInstantiationContext, methodInstantiationContext));
    }

    [SecuritySafeCritical]
    internal static unsafe IRuntimeFieldInfo ResolveFieldHandleInternal(RuntimeModule module, int fieldToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
    {
      ModuleHandle.ValidateModulePointer(module);
      if (!ModuleHandle.GetMetadataImport(module.GetNativeHandle()).IsValidToken(fieldToken))
        throw new ArgumentOutOfRangeException("metadataToken", Environment.GetResourceString("Argument_InvalidToken", (object) fieldToken, (object) new ModuleHandle(module)));
      int length1;
      IntPtr[] numArray1 = RuntimeTypeHandle.CopyRuntimeTypeHandles(typeInstantiationContext, out length1);
      int length2;
      IntPtr[] numArray2 = RuntimeTypeHandle.CopyRuntimeTypeHandles(methodInstantiationContext, out length2);
      IntPtr[] numArray3 = numArray1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      fixed (IntPtr* typeInstArgs = &^(numArray1 == null || numArray3.Length == 0 ? (IntPtr&) IntPtr.Zero : @numArray3[0]))
        fixed (IntPtr* methodInstArgs = numArray2)
        {
          IRuntimeFieldInfo o = (IRuntimeFieldInfo) null;
          ModuleHandle.ResolveField(module.GetNativeHandle(), fieldToken, typeInstArgs, length1, methodInstArgs, length2, JitHelpers.GetObjectHandleOnStack<IRuntimeFieldInfo>(ref o));
          GC.KeepAlive((object) typeInstantiationContext);
          GC.KeepAlive((object) methodInstantiationContext);
          return o;
        }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern unsafe void ResolveField(RuntimeModule module, int fieldToken, IntPtr* typeInstArgs, int typeInstCount, IntPtr* methodInstArgs, int methodInstCount, ObjectHandleOnStack retField);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern bool _ContainsPropertyMatchingHash(RuntimeModule module, int propertyToken, uint hash);

    [SecurityCritical]
    internal static bool ContainsPropertyMatchingHash(RuntimeModule module, int propertyToken, uint hash)
    {
      return ModuleHandle._ContainsPropertyMatchingHash(module.GetNativeHandle(), propertyToken, hash);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetAssembly(RuntimeModule handle, ObjectHandleOnStack retAssembly);

    [SecuritySafeCritical]
    internal static RuntimeAssembly GetAssembly(RuntimeModule module)
    {
      RuntimeAssembly o = (RuntimeAssembly) null;
      ModuleHandle.GetAssembly(module.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeAssembly>(ref o));
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void GetModuleType(RuntimeModule handle, ObjectHandleOnStack type);

    [SecuritySafeCritical]
    internal static RuntimeType GetModuleType(RuntimeModule module)
    {
      RuntimeType o = (RuntimeType) null;
      ModuleHandle.GetModuleType(module.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref o));
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetPEKind(RuntimeModule handle, out int peKind, out int machine);

    [SecuritySafeCritical]
    internal static void GetPEKind(RuntimeModule module, out PortableExecutableKinds peKind, out ImageFileMachine machine)
    {
      int peKind1;
      int machine1;
      ModuleHandle.GetPEKind(module.GetNativeHandle(), out peKind1, out machine1);
      peKind = (PortableExecutableKinds) peKind1;
      machine = (ImageFileMachine) machine1;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetMDStreamVersion(RuntimeModule module);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern IntPtr _GetMetadataImport(RuntimeModule module);

    [SecurityCritical]
    internal static MetadataImport GetMetadataImport(RuntimeModule module)
    {
      return new MetadataImport(ModuleHandle._GetMetadataImport(module.GetNativeHandle()), (object) module);
    }
  }
}
