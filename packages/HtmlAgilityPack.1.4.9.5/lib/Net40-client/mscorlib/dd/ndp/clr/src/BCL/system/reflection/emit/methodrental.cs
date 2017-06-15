// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.MethodRental
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Reflection.Emit
{
  /// <summary>在给定类的方法的情况下，提供一种交换方法体实现的快速方法。</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_MethodRental))]
  [ComVisible(true)]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public sealed class MethodRental : _MethodRental
  {
    /// <summary>指定应在需要时以实时 (JIT) 方式编译该方法。</summary>
    public const int JitOnDemand = 0;
    /// <summary>指定应该以实时 (JIT) 方式立即编译该方法。</summary>
    public const int JitImmediate = 1;

    private MethodRental()
    {
    }

    /// <summary>交换方法体。</summary>
    /// <param name="cls">包含方法的类。</param>
    /// <param name="methodtoken">方法的标记。</param>
    /// <param name="rgIL">指向方法的指针。它应包含方法头。</param>
    /// <param name="methodSize">新方法体的大小（以字节为单位）。</param>
    /// <param name="flags">控制交换的标志。请参见常数的定义。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="cls" /> 为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="cls" /> 类型不完整。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="methodSize" /> 小于 1，或大于 4128767（十六进制 3effff）。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
    public static void SwapMethodBody(Type cls, int methodtoken, IntPtr rgIL, int methodSize, int flags)
    {
      if (methodSize <= 0 || methodSize >= 4128768)
        throw new ArgumentException(Environment.GetResourceString("Argument_BadSizeForData"), "methodSize");
      if (cls == (Type) null)
        throw new ArgumentNullException("cls");
      Module module = cls.Module;
      ModuleBuilder moduleBuilder = module as ModuleBuilder;
      InternalModuleBuilder internalModuleBuilder = !((Module) moduleBuilder != (Module) null) ? module as InternalModuleBuilder : moduleBuilder.InternalModule;
      if ((Module) internalModuleBuilder == (Module) null)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_NotDynamicModule"));
      RuntimeType runtimeType;
      if (cls is TypeBuilder)
      {
        TypeBuilder typeBuilder = (TypeBuilder) cls;
        if (!typeBuilder.IsCreated())
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_NotAllTypesAreBaked", (object) typeBuilder.Name));
        runtimeType = typeBuilder.BakedRuntimeType;
      }
      else
        runtimeType = cls as RuntimeType;
      if (runtimeType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "cls");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      lock (internalModuleBuilder.GetRuntimeAssembly().SyncRoot)
        MethodRental.SwapMethodBody(runtimeType.GetTypeHandleInternal(), methodtoken, rgIL, methodSize, flags, JitHelpers.GetStackCrawlMarkHandle(ref stackMark));
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void SwapMethodBody(RuntimeTypeHandle cls, int methodtoken, IntPtr rgIL, int methodSize, int flags, StackCrawlMarkHandle stackMark);

    void _MethodRental.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _MethodRental.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _MethodRental.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _MethodRental.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
