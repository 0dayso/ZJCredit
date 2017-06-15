// Decompiled with JetBrains decompiler
// Type: System.Runtime.GCSettings
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime
{
  /// <summary>指定当前进程的垃圾回收设置。</summary>
  [__DynamicallyInvokable]
  public static class GCSettings
  {
    /// <summary>获取或设置垃圾收集的当前滞后时间模式。</summary>
    /// <returns>指定滞后时间模式的枚举值之一。</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <see cref="P:System.Runtime.GCSettings.LatencyMode" />属性设置为无效值。- 或 -<see cref="P:System.Runtime.GCSettings.LatencyMode" />属性不能设置为<see cref="F:System.Runtime.GCLatencyMode.NoGCRegion" />。</exception>
    [__DynamicallyInvokable]
    public static GCLatencyMode LatencyMode
    {
      [SecuritySafeCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), __DynamicallyInvokable] get
      {
        return (GCLatencyMode) GC.GetGCLatencyMode();
      }
      [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), __DynamicallyInvokable, HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)] set
      {
        if (value < GCLatencyMode.Batch || value > GCLatencyMode.SustainedLowLatency)
          throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_Enum"));
        if (GC.SetGCLatencyMode((int) value) == 1)
          throw new InvalidOperationException("The NoGCRegion mode is in progress. End it and then set a different mode.");
      }
    }

    /// <summary>[在 .NET Framework 4.5.1 和更高版本中受支持]获取或设置指示完全阻止垃圾回收是否压缩大型对象堆 (LOH) 的值。</summary>
    /// <returns>指示完全阻止垃圾回收是否压缩 LOH 的枚举值之一。</returns>
    [__DynamicallyInvokable]
    public static GCLargeObjectHeapCompactionMode LargeObjectHeapCompactionMode
    {
      [SecuritySafeCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), __DynamicallyInvokable] get
      {
        return (GCLargeObjectHeapCompactionMode) GC.GetLOHCompactionMode();
      }
      [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), __DynamicallyInvokable, HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)] set
      {
        if (value < GCLargeObjectHeapCompactionMode.Default || value > GCLargeObjectHeapCompactionMode.CompactOnce)
          throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_Enum"));
        GC.SetLOHCompactionMode((int) value);
      }
    }

    /// <summary>获取一个值，该值指示是否启用了服务器垃圾回收。</summary>
    /// <returns>如果启用了服务器垃圾回收，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public static bool IsServerGC
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return GC.IsServerGC();
      }
    }

    private enum SetLatencyModeStatus
    {
      Succeeded,
      NoGCInProgress,
    }
  }
}
