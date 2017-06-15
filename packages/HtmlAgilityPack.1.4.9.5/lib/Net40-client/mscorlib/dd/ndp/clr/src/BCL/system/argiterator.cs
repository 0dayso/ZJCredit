// Decompiled with JetBrains decompiler
// Type: System.ArgIterator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
  /// <summary>表示变长参数列表；即采用可变数量的参数的函数的参数。</summary>
  /// <filterpriority>2</filterpriority>
  public struct ArgIterator
  {
    private IntPtr ArgCookie;
    private IntPtr sigPtr;
    private IntPtr sigPtrLen;
    private IntPtr ArgPtr;
    private int RemainingArgs;

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private ArgIterator(IntPtr arglist);

    /// <summary>使用指定的参数列表初始化 <see cref="T:System.ArgIterator" /> 结构的新实例。</summary>
    /// <param name="arglist">一个由强制参数和可选参数组成的参数列表。</param>
    [SecuritySafeCritical]
    public ArgIterator(RuntimeArgumentHandle arglist)
    {
      this = new ArgIterator(arglist.Value);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private unsafe ArgIterator(IntPtr arglist, void* ptr);

    /// <summary>使用指定的参数列表和指向列表项的指针来初始化 <see cref="T:System.ArgIterator" /> 结构的新实例。</summary>
    /// <param name="arglist">一个由强制参数和可选参数组成的参数列表。</param>
    /// <param name="ptr">一个指针，它指向首先访问的 <paramref name="arglist" /> 中的参数，或者如果 <paramref name="ptr" /> 为null，则指向 <paramref name="arglist" /> 中的第一个强制参数。</param>
    [SecurityCritical]
    [CLSCompliant(false)]
    public unsafe ArgIterator(RuntimeArgumentHandle arglist, void* ptr)
    {
      this = new ArgIterator(arglist.Value, ptr);
    }

    /// <summary>返回变长参数列表中的下一参数。</summary>
    /// <returns>作为 <see cref="T:System.TypedReference" /> 对象的下一参数。</returns>
    /// <exception cref="T:System.InvalidOperationException">尝试在列表结尾以外进行读取。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    public unsafe TypedReference GetNextArg()
    {
      TypedReference typedReference = new TypedReference();
      this.FCallGetNextArg((void*) &typedReference);
      return typedReference;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private unsafe void FCallGetNextArg(void* result);

    /// <summary>返回变长参数列表中具有指定类型的下一个参数。</summary>
    /// <returns>作为 <see cref="T:System.TypedReference" /> 对象的下一参数。</returns>
    /// <param name="rth">标识要检索的参数类型的运行时类型句柄。</param>
    /// <exception cref="T:System.InvalidOperationException">尝试在列表结尾以外进行读取。</exception>
    /// <exception cref="T:System.ArgumentNullException">其余参数的指针为零。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    public unsafe TypedReference GetNextArg(RuntimeTypeHandle rth)
    {
      if (this.sigPtr != IntPtr.Zero)
        return this.GetNextArg();
      if (this.ArgPtr == IntPtr.Zero)
        throw new ArgumentNullException();
      TypedReference typedReference = new TypedReference();
      this.InternalGetNextArg((void*) &typedReference, rth.GetRuntimeType());
      return typedReference;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private unsafe void InternalGetNextArg(void* result, RuntimeType rt);

    /// <summary>结束由此实例表示的变长参数列表的处理。</summary>
    /// <filterpriority>2</filterpriority>
    public void End()
    {
    }

    /// <summary>返回参数列表中剩余参数的个数。</summary>
    /// <returns>剩余参数的个数。</returns>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public int GetRemainingCount();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private unsafe void* _GetNextArgType();

    /// <summary>返回下一个参数的类型。</summary>
    /// <returns>下一个参数的类型。</returns>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public unsafe RuntimeTypeHandle GetNextArgType()
    {
      return new RuntimeTypeHandle(Type.GetTypeFromHandleUnsafe((IntPtr) this._GetNextArgType()));
    }

    /// <summary>返回此对象的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    public override int GetHashCode()
    {
      return ValueType.GetHashCodeOfPtr(this.ArgCookie);
    }

    /// <summary>此方法不受支持，它始终会引发 <see cref="T:System.NotSupportedException" />。</summary>
    /// <returns>不支持此比较。不返回任何值。</returns>
    /// <param name="o">要与该实例进行比较的对象。</param>
    /// <exception cref="T:System.NotSupportedException">此方法不受支持。</exception>
    /// <filterpriority>2</filterpriority>
    public override bool Equals(object o)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_NYI"));
    }
  }
}
