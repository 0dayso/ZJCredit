// Decompiled with JetBrains decompiler
// Type: System.Threading.Volatile
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Threading
{
  /// <summary>包含用于执行易失内存操作的方法。</summary>
  [__DynamicallyInvokable]
  public static class Volatile
  {
    /// <summary>读取指定字段的值。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之后，则处理器无法将其移至此方法之前。</summary>
    /// <returns>读取的值。无论处理器的数目或处理器缓存的状态如何，该值都是由计算机的任何处理器写入的最新值。</returns>
    /// <param name="location">要读取的字段。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static bool Read(ref bool location)
    {
      int num = location ? 1 : 0;
      Thread.MemoryBarrier();
      return num != 0;
    }

    /// <summary>读取指定字段的值。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之后，则处理器无法将其移至此方法之前。</summary>
    /// <returns>读取的值。无论处理器的数目或处理器缓存的状态如何，该值都是由计算机的任何处理器写入的最新值。</returns>
    /// <param name="location">要读取的字段。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte Read(ref sbyte location)
    {
      int num = (int) location;
      Thread.MemoryBarrier();
      return (sbyte) num;
    }

    /// <summary>读取指定字段的值。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之后，则处理器无法将其移至此方法之前。</summary>
    /// <returns>读取的值。无论处理器的数目或处理器缓存的状态如何，该值都是由计算机的任何处理器写入的最新值。</returns>
    /// <param name="location">要读取的字段。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static byte Read(ref byte location)
    {
      int num = (int) location;
      Thread.MemoryBarrier();
      return (byte) num;
    }

    /// <summary>读取指定字段的值。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之后，则处理器无法将其移至此方法之前。</summary>
    /// <returns>读取的值。无论处理器的数目或处理器缓存的状态如何，该值都是由计算机的任何处理器写入的最新值。</returns>
    /// <param name="location">要读取的字段。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static short Read(ref short location)
    {
      int num = (int) location;
      Thread.MemoryBarrier();
      return (short) num;
    }

    /// <summary>读取指定字段的值。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之后，则处理器无法将其移至此方法之前。</summary>
    /// <returns>读取的值。无论处理器的数目或处理器缓存的状态如何，该值都是由计算机的任何处理器写入的最新值。</returns>
    /// <param name="location">要读取的字段。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort Read(ref ushort location)
    {
      int num = (int) location;
      Thread.MemoryBarrier();
      return (ushort) num;
    }

    /// <summary>读取指定字段的值。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之后，则处理器无法将其移至此方法之前。</summary>
    /// <returns>读取的值。无论处理器的数目或处理器缓存的状态如何，该值都是由计算机的任何处理器写入的最新值。</returns>
    /// <param name="location">要读取的字段。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static int Read(ref int location)
    {
      int num = location;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>读取指定字段的值。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之后，则处理器无法将其移至此方法之前。</summary>
    /// <returns>读取的值。无论处理器的数目或处理器缓存的状态如何，该值都是由计算机的任何处理器写入的最新值。</returns>
    /// <param name="location">要读取的字段。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint Read(ref uint location)
    {
      int num = (int) location;
      Thread.MemoryBarrier();
      return (uint) num;
    }

    /// <summary>读取指定字段的值。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之后，则处理器无法将其移至此方法之前。</summary>
    /// <returns>读取的值。无论处理器的数目或处理器缓存的状态如何，该值都是由计算机的任何处理器写入的最新值。</returns>
    /// <param name="location">要读取的字段。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static long Read(ref long location)
    {
      return Interlocked.CompareExchange(ref location, 0L, 0L);
    }

    /// <summary>读取指定字段的值。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之后，则处理器无法将其移至此方法之前。</summary>
    /// <returns>读取的值。无论处理器的数目或处理器缓存的状态如何，该值都是由计算机的任何处理器写入的最新值。</returns>
    /// <param name="location">要读取的字段。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [CLSCompliant(false)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe ulong Read(ref ulong location)
    {
      fixed (ulong* numPtr = &location)
      {
        // ISSUE: cast to a reference type
        return (ulong) Interlocked.CompareExchange((long&) numPtr, 0L, 0L);
      }
    }

    /// <summary>读取指定字段的值。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之后，则处理器无法将其移至此方法之前。</summary>
    /// <returns>读取的值。无论处理器的数目或处理器缓存的状态如何，该值都是由计算机的任何处理器写入的最新值。</returns>
    /// <param name="location">要读取的字段。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public static IntPtr Read(ref IntPtr location)
    {
      IntPtr num = location;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>读取指定字段的值。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之后，则处理器无法将其移至此方法之前。</summary>
    /// <returns>读取的值。无论处理器的数目或处理器缓存的状态如何，该值都是由计算机的任何处理器写入的最新值。</returns>
    /// <param name="location">要读取的字段。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [CLSCompliant(false)]
    public static UIntPtr Read(ref UIntPtr location)
    {
      IntPtr num = (IntPtr) location;
      Thread.MemoryBarrier();
      return (UIntPtr) num;
    }

    /// <summary>读取指定字段的值。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之后，则处理器无法将其移至此方法之前。</summary>
    /// <returns>读取的值。无论处理器的数目或处理器缓存的状态如何，该值都是由计算机的任何处理器写入的最新值。</returns>
    /// <param name="location">要读取的字段。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static float Read(ref float location)
    {
      double num = (double) location;
      Thread.MemoryBarrier();
      return (float) num;
    }

    /// <summary>读取指定字段的值。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之后，则处理器无法将其移至此方法之前。</summary>
    /// <returns>读取的值。无论处理器的数目或处理器缓存的状态如何，该值都是由计算机的任何处理器写入的最新值。</returns>
    /// <param name="location">要读取的字段。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static double Read(ref double location)
    {
      return Interlocked.CompareExchange(ref location, 0.0, 0.0);
    }

    /// <summary>从指定的字段读取对象引用。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之后，则处理器无法将其移至此方法之前。</summary>
    /// <returns>对读取的 <paramref name="T" /> 的引用。无论处理器的数目或处理器缓存的状态如何，该引用都是由计算机的任何处理器写入的最新引用。</returns>
    /// <param name="location">要读取的字段。</param>
    /// <typeparam name="T">要读取的字段的类型。此类型必须是引用类型，而不是值类型。</typeparam>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static T Read<T>(ref T location) where T : class
    {
      T obj = location;
      Thread.MemoryBarrier();
      return obj;
    }

    /// <summary>将指定的值写入指定字段。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之前，则处理器无法将其移至此方法之后。</summary>
    /// <param name="location">将值写入的字段。</param>
    /// <param name="value">要写入的值。立即写入一个值，以使该值对计算机中的所有处理器都可见。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static void Write(ref bool location, bool value)
    {
      Thread.MemoryBarrier();
      location = value;
    }

    /// <summary>将指定的值写入指定字段。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之前，则处理器无法将其移至此方法之后。</summary>
    /// <param name="location">将值写入的字段。</param>
    /// <param name="value">要写入的值。立即写入一个值，以使该值对计算机中的所有处理器都可见。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static void Write(ref sbyte location, sbyte value)
    {
      Thread.MemoryBarrier();
      location = value;
    }

    /// <summary>将指定的值写入指定字段。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之前，则处理器无法将其移至此方法之后。</summary>
    /// <param name="location">将值写入的字段。</param>
    /// <param name="value">要写入的值。立即写入一个值，以使该值对计算机中的所有处理器都可见。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static void Write(ref byte location, byte value)
    {
      Thread.MemoryBarrier();
      location = value;
    }

    /// <summary>将指定的值写入指定字段。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之前，则处理器无法将其移至此方法之后。</summary>
    /// <param name="location">将值写入的字段。</param>
    /// <param name="value">要写入的值。立即写入一个值，以使该值对计算机中的所有处理器都可见。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static void Write(ref short location, short value)
    {
      Thread.MemoryBarrier();
      location = value;
    }

    /// <summary>将指定的值写入指定字段。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之前，则处理器无法将其移至此方法之后。</summary>
    /// <param name="location">将值写入的字段。</param>
    /// <param name="value">要写入的值。立即写入一个值，以使该值对计算机中的所有处理器都可见。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static void Write(ref ushort location, ushort value)
    {
      Thread.MemoryBarrier();
      location = value;
    }

    /// <summary>将指定的值写入指定字段。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之前，则处理器无法将其移至此方法之后。</summary>
    /// <param name="location">将值写入的字段。</param>
    /// <param name="value">要写入的值。立即写入一个值，以使该值对计算机中的所有处理器都可见。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static void Write(ref int location, int value)
    {
      Thread.MemoryBarrier();
      location = value;
    }

    /// <summary>将指定的值写入指定字段。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之前，则处理器无法将其移至此方法之后。</summary>
    /// <param name="location">将值写入的字段。</param>
    /// <param name="value">要写入的值。立即写入一个值，以使该值对计算机中的所有处理器都可见。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static void Write(ref uint location, uint value)
    {
      Thread.MemoryBarrier();
      location = value;
    }

    /// <summary>将指定的值写入指定字段。在需要进行此操作的系统上，插入如下所示的防止处理器重新对内存操作进行排序的内存栅：如果内存操作出现在代码中的此方法之前，则处理器不能将其移至此方法之后。</summary>
    /// <param name="location">将值写入的字段。</param>
    /// <param name="value">要写入的值。立即写入一个值，以使该值对计算机中的所有处理器都可见。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static void Write(ref long location, long value)
    {
      Interlocked.Exchange(ref location, value);
    }

    /// <summary>将指定的值写入指定字段。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之前，则处理器无法将其移至此方法之后。</summary>
    /// <param name="location">将值写入的字段。</param>
    /// <param name="value">要写入的值。立即写入一个值，以使该值对计算机中的所有处理器都可见。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [CLSCompliant(false)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe void Write(ref ulong location, ulong value)
    {
      fixed (ulong* numPtr = &location)
      {
        // ISSUE: cast to a reference type
        Interlocked.Exchange((long&) numPtr, (long) value);
      }
    }

    /// <summary>将指定的值写入指定字段。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之前，则处理器无法将其移至此方法之后。</summary>
    /// <param name="location">将值写入的字段。</param>
    /// <param name="value">要写入的值。立即写入一个值，以使该值对计算机中的所有处理器都可见。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public static void Write(ref IntPtr location, IntPtr value)
    {
      Thread.MemoryBarrier();
      location = value;
    }

    /// <summary>将指定的值写入指定字段。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之前，则处理器无法将其移至此方法之后。</summary>
    /// <param name="location">将值写入的字段。</param>
    /// <param name="value">要写入的值。立即写入一个值，以使该值对计算机中的所有处理器都可见。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [CLSCompliant(false)]
    public static void Write(ref UIntPtr location, UIntPtr value)
    {
      Thread.MemoryBarrier();
      location = value;
    }

    /// <summary>将指定的值写入指定字段。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之前，则处理器无法将其移至此方法之后。</summary>
    /// <param name="location">将值写入的字段。</param>
    /// <param name="value">要写入的值。立即写入一个值，以使该值对计算机中的所有处理器都可见。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static void Write(ref float location, float value)
    {
      Thread.MemoryBarrier();
      location = value;
    }

    /// <summary>将指定的值写入指定字段。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之前，则处理器无法将其移至此方法之后。</summary>
    /// <param name="location">将值写入的字段。</param>
    /// <param name="value">要写入的值。立即写入一个值，以使该值对计算机中的所有处理器都可见。</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static void Write(ref double location, double value)
    {
      Interlocked.Exchange(ref location, value);
    }

    /// <summary>将指定的对象引用写入指定字段。在需要进行此操作的系统上，插入防止处理器重新对内存操作进行排序的内存屏障，如下所示：如果读取或写入操作在代码中出现在此方法之前，则处理器无法将其移至此方法之后。</summary>
    /// <param name="location">将对象引用写入的字段。</param>
    /// <param name="value">要写入的对象引用。立即写入一个引用，以使该引用对计算机中的所有处理器都可见。</param>
    /// <typeparam name="T">要写入的字段的类型。此类型必须是引用类型，而不是值类型。</typeparam>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static void Write<T>(ref T location, T value) where T : class
    {
      Thread.MemoryBarrier();
      location = value;
    }
  }
}
