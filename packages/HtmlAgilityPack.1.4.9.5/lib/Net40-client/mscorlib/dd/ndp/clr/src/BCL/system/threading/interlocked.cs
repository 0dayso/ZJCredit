// Decompiled with JetBrains decompiler
// Type: System.Threading.Interlocked
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
  /// <summary>为多个线程共享的变量提供原子操作。</summary>
  /// <filterpriority>2</filterpriority>
  [__DynamicallyInvokable]
  public static class Interlocked
  {
    /// <summary>以原子操作的形式递增指定变量的值并存储结果。</summary>
    /// <returns>递增的值。</returns>
    /// <param name="location">其值要递增的变量。</param>
    /// <exception cref="T:System.NullReferenceException">The address of <paramref name="location" /> is a null pointer. </exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static int Increment(ref int location)
    {
      return Interlocked.Add(ref location, 1);
    }

    /// <summary>以原子操作的形式递增指定变量的值并存储结果。</summary>
    /// <returns>递增的值。</returns>
    /// <param name="location">其值要递增的变量。</param>
    /// <exception cref="T:System.NullReferenceException">The address of <paramref name="location" /> is a null pointer. </exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static long Increment(ref long location)
    {
      return Interlocked.Add(ref location, 1L);
    }

    /// <summary>以原子操作的形式递减指定变量的值并存储结果。</summary>
    /// <returns>递减的值。</returns>
    /// <param name="location">其值要递减的变量。</param>
    /// <exception cref="T:System.ArgumentNullException">The address of <paramref name="location" /> is a null pointer. </exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static int Decrement(ref int location)
    {
      return Interlocked.Add(ref location, -1);
    }

    /// <summary>以原子操作的形式递减指定变量的值并存储结果。</summary>
    /// <returns>递减的值。</returns>
    /// <param name="location">其值要递减的变量。</param>
    /// <exception cref="T:System.ArgumentNullException">The address of <paramref name="location" /> is a null pointer. </exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static long Decrement(ref long location)
    {
      return Interlocked.Add(ref location, -1L);
    }

    /// <summary>以原子操作的形式，将 32 位有符号整数设置为指定的值并返回原始值。</summary>
    /// <returns>
    /// <paramref name="location1" /> 的原始值。</returns>
    /// <param name="location1">要设置为指定值的变量。</param>
    /// <param name="value">
    /// <paramref name="location1" /> 参数被设置为的值。</param>
    /// <exception cref="T:System.ArgumentNullException">The address of <paramref name="location1" /> is a null pointer. </exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int Exchange(ref int location1, int value);

    /// <summary>以原子操作的形式，将 64 位有符号整数设置为指定的值并返回原始值。</summary>
    /// <returns>
    /// <paramref name="location1" /> 的原始值。</returns>
    /// <param name="location1">要设置为指定值的变量。</param>
    /// <param name="value">
    /// <paramref name="location1" /> 参数被设置为的值。</param>
    /// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern long Exchange(ref long location1, long value);

    /// <summary>以原子操作的形式，将单精度浮点数设置为指定的值并返回原始值。</summary>
    /// <returns>
    /// <paramref name="location1" /> 的原始值。</returns>
    /// <param name="location1">要设置为指定值的变量。</param>
    /// <param name="value">
    /// <paramref name="location1" /> 参数被设置为的值。</param>
    /// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern float Exchange(ref float location1, float value);

    /// <summary>以原子操作的形式，将双精度浮点数设置为指定的值并返回原始值。</summary>
    /// <returns>
    /// <paramref name="location1" /> 的原始值。</returns>
    /// <param name="location1">要设置为指定值的变量。</param>
    /// <param name="value">
    /// <paramref name="location1" /> 参数被设置为的值。</param>
    /// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Exchange(ref double location1, double value);

    /// <summary>以原子操作的形式，将对象设置为指定的值并返回对原始对象的引用。</summary>
    /// <returns>
    /// <paramref name="location1" /> 的原始值。</returns>
    /// <param name="location1">要设置为指定值的变量。</param>
    /// <param name="value">
    /// <paramref name="location1" /> 参数被设置为的值。</param>
    /// <exception cref="T:System.ArgumentNullException">The address of <paramref name="location1" /> is a null pointer. </exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern object Exchange(ref object location1, object value);

    /// <summary>以原子操作的形式，将平台特定的句柄或指针设置为指定的值并返回原始值。</summary>
    /// <returns>
    /// <paramref name="location1" /> 的原始值。</returns>
    /// <param name="location1">要设置为指定值的变量。</param>
    /// <param name="value">
    /// <paramref name="location1" /> 参数被设置为的值。</param>
    /// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer. </exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern IntPtr Exchange(ref IntPtr location1, IntPtr value);

    /// <summary>以原子操作的形式，将指定类型 <paramref name="T" /> 的变量设置为指定的值并返回原始值。</summary>
    /// <returns>
    /// <paramref name="location1" /> 的原始值。</returns>
    /// <param name="location1">要设置为指定值的变量。这是一个引用参数（在 C# 中是 ref，在 Visual Basic 中是 ByRef）。</param>
    /// <param name="value">
    /// <paramref name="location1" /> 参数被设置为的值。</param>
    /// <typeparam name="T">用于 <paramref name="location1" /> 和 <paramref name="value" /> 的类型。此类型必须是引用类型。</typeparam>
    /// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer. </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [ComVisible(false)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static T Exchange<T>(ref T location1, T value) where T : class
    {
      Interlocked._Exchange(__makeref (location1), __makeref (value));
      return value;
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _Exchange(TypedReference location1, TypedReference value);

    /// <summary>比较两个 32 位有符号整数是否相等，如果相等，则替换第一个值。</summary>
    /// <returns>
    /// <paramref name="location1" /> 中的原始值。</returns>
    /// <param name="location1">其值将与 <paramref name="comparand" /> 进行比较并且可能被替换的目标。</param>
    /// <param name="value">比较结果相等时替换目标值的值。</param>
    /// <param name="comparand">与位于 <paramref name="location1" /> 处的值进行比较的值。</param>
    /// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer. </exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int CompareExchange(ref int location1, int value, int comparand);

    /// <summary>比较两个 64 位有符号整数是否相等，如果相等，则替换第一个值。</summary>
    /// <returns>
    /// <paramref name="location1" /> 中的原始值。</returns>
    /// <param name="location1">其值将与 <paramref name="comparand" /> 进行比较并且可能被替换的目标。</param>
    /// <param name="value">比较结果相等时替换目标值的值。</param>
    /// <param name="comparand">与位于 <paramref name="location1" /> 处的值进行比较的值。</param>
    /// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern long CompareExchange(ref long location1, long value, long comparand);

    /// <summary>比较两个单精度浮点数是否相等，如果相等，则替换第一个值。</summary>
    /// <returns>
    /// <paramref name="location1" /> 中的原始值。</returns>
    /// <param name="location1">其值将与 <paramref name="comparand" /> 进行比较并且可能被替换的目标。</param>
    /// <param name="value">比较结果相等时替换目标值的值。</param>
    /// <param name="comparand">与位于 <paramref name="location1" /> 处的值进行比较的值。</param>
    /// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern float CompareExchange(ref float location1, float value, float comparand);

    /// <summary>比较两个双精度浮点数是否相等，如果相等，则替换第一个值。</summary>
    /// <returns>
    /// <paramref name="location1" /> 中的原始值。</returns>
    /// <param name="location1">其值将与 <paramref name="comparand" /> 进行比较并且可能被替换的目标。</param>
    /// <param name="value">比较结果相等时替换目标值的值。</param>
    /// <param name="comparand">与位于 <paramref name="location1" /> 处的值进行比较的值。</param>
    /// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer. </exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double CompareExchange(ref double location1, double value, double comparand);

    /// <summary>比较两个对象是否相等，如果相等，则替换第一个对象。</summary>
    /// <returns>
    /// <paramref name="location1" /> 中的原始值。</returns>
    /// <param name="location1">其值与 <paramref name="comparand" /> 进行比较并且可能被替换的目标对象。</param>
    /// <param name="value">在比较结果相等时替换目标对象的对象。</param>
    /// <param name="comparand">与位于 <paramref name="location1" /> 处的对象进行比较的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">The address of <paramref name="location1" /> is a null pointer. </exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern object CompareExchange(ref object location1, object value, object comparand);

    /// <summary>比较两个平台特定的句柄或指针是否相等，如果相等，则替换第一个。</summary>
    /// <returns>
    /// <paramref name="location1" /> 中的原始值。</returns>
    /// <param name="location1">其值与 <paramref name="comparand" /> 的值进行比较并且可能被 <paramref name="value" /> 替换的目标 <see cref="T:System.IntPtr" />。</param>
    /// <param name="value">比较结果相等时替换目标值的 <see cref="T:System.IntPtr" />。</param>
    /// <param name="comparand">与位于 <paramref name="location1" /> 处的值进行比较的 <see cref="T:System.IntPtr" />。</param>
    /// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer. </exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern IntPtr CompareExchange(ref IntPtr location1, IntPtr value, IntPtr comparand);

    /// <summary>比较指定的引用类型 <paramref name="T" /> 的两个实例是否相等，如果相等，则替换第一个。</summary>
    /// <returns>
    /// <paramref name="location1" /> 中的原始值。</returns>
    /// <param name="location1">其值将与 <paramref name="comparand" /> 进行比较并且可能被替换的目标。这是一个引用参数（在 C# 中是 ref，在 Visual Basic 中是 ByRef）。</param>
    /// <param name="value">比较结果相等时替换目标值的值。</param>
    /// <param name="comparand">与位于 <paramref name="location1" /> 处的值进行比较的值。</param>
    /// <typeparam name="T">用于 <paramref name="location1" />, <paramref name="value" /> 和 <paramref name="comparand" /> 的类型。此类型必须是引用类型。</typeparam>
    /// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer. </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [ComVisible(false)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static T CompareExchange<T>(ref T location1, T value, T comparand) where T : class
    {
      Interlocked._CompareExchange(__makeref (location1), __makeref (value), (object) comparand);
      return value;
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _CompareExchange(TypedReference location1, TypedReference value, object comparand);

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int CompareExchange(ref int location1, int value, int comparand, ref bool succeeded);

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int ExchangeAdd(ref int location1, int value);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern long ExchangeAdd(ref long location1, long value);

    /// <summary>对两个 32 位整数进行求和并用和替换第一个整数，上述操作作为一个原子操作完成。</summary>
    /// <returns>存储在 <paramref name="location1" /> 处的新值。</returns>
    /// <param name="location1">一个变量，包含要添加的第一个值。两个值的和存储在 <paramref name="location1" /> 中。</param>
    /// <param name="value">要添加到整数中的 <paramref name="location1" /> 位置的值。</param>
    /// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer. </exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static int Add(ref int location1, int value)
    {
      return Interlocked.ExchangeAdd(ref location1, value) + value;
    }

    /// <summary>对两个 64 位整数进行求和并用和替换第一个整数，上述操作作为一个原子操作完成。</summary>
    /// <returns>存储在 <paramref name="location1" /> 处的新值。</returns>
    /// <param name="location1">一个变量，包含要添加的第一个值。两个值的和存储在 <paramref name="location1" /> 中。</param>
    /// <param name="value">要添加到整数中的 <paramref name="location1" /> 位置的值。</param>
    /// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer. </exception>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static long Add(ref long location1, long value)
    {
      return Interlocked.ExchangeAdd(ref location1, value) + value;
    }

    /// <summary>返回一个以原子操作形式加载的 64 位值。</summary>
    /// <returns>加载的值。</returns>
    /// <param name="location">要加载的 64 位值。</param>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static long Read(ref long location)
    {
      return Interlocked.CompareExchange(ref location, 0L, 0L);
    }

    /// <summary>按如下方式同步内存存取：执行当前线程的处理器在对指令重新排序时，不能采用先执行 <see cref="M:System.Threading.Interlocked.MemoryBarrier" /> 调用之后的内存存取，再执行 <see cref="M:System.Threading.Interlocked.MemoryBarrier" /> 调用之前的内存存取的方式。</summary>
    [__DynamicallyInvokable]
    public static void MemoryBarrier()
    {
      Thread.MemoryBarrier();
    }
  }
}
