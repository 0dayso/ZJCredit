// Decompiled with JetBrains decompiler
// Type: System.GC
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
  /// <summary>控制系统垃圾回收器（一种自动回收未使用内存的服务）。</summary>
  /// <filterpriority>2</filterpriority>
  [__DynamicallyInvokable]
  public static class GC
  {
    /// <summary>获取系统当前支持的最大代数。</summary>
    /// <returns>从零到所支持的最大代数间的一个值。</returns>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static int MaxGeneration
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return GC.GetMaxGeneration();
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetGCLatencyMode();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int SetGCLatencyMode(int newLatencyMode);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern int _StartNoGCRegion(long totalSize, bool lohSizeKnown, long lohSize, bool disallowFullBlockingGC);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern int _EndNoGCRegion();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetLOHCompactionMode();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void SetLOHCompactionMode(int newLOHCompactionyMode);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int GetGenerationWR(IntPtr handle);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern long GetTotalMemory();

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void _Collect(int generation, int mode);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int GetMaxGeneration();

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int _CollectionCount(int generation, int getSpecialGCCount);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsServerGC();

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void _AddMemoryPressure(ulong bytesAllocated);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void _RemoveMemoryPressure(ulong bytesAllocated);

    /// <summary>通知运行时在安排垃圾回收时应考虑分配大量的非托管内存。</summary>
    /// <param name="bytesAllocated">已分配的非托管内存的增量。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="bytesAllocated" /> 小于或等于 0。- 或 -在 32 位的计算机上，<paramref name="bytesAllocated" /> 大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static void AddMemoryPressure(long bytesAllocated)
    {
      if (bytesAllocated <= 0L)
        throw new ArgumentOutOfRangeException("bytesAllocated", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      if (4 == IntPtr.Size && bytesAllocated > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("pressure", Environment.GetResourceString("ArgumentOutOfRange_MustBeNonNegInt32"));
      GC._AddMemoryPressure((ulong) bytesAllocated);
    }

    /// <summary>通知运行时已释放非托管内存，在安排垃圾回收时不需要再考虑它。</summary>
    /// <param name="bytesAllocated">已释放的非托管内存量。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="bytesAllocated" /> 小于或等于 0。- 或 - 在 32 位的计算机上，<paramref name="bytesAllocated" /> 大于 <see cref="F:System.Int32.MaxValue" />。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static void RemoveMemoryPressure(long bytesAllocated)
    {
      if (bytesAllocated <= 0L)
        throw new ArgumentOutOfRangeException("bytesAllocated", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      if (4 == IntPtr.Size && bytesAllocated > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("bytesAllocated", Environment.GetResourceString("ArgumentOutOfRange_MustBeNonNegInt32"));
      GC._RemoveMemoryPressure((ulong) bytesAllocated);
    }

    /// <summary>返回指定对象的当前代数。</summary>
    /// <returns>
    /// <paramref name="obj" /> 的当前代数。</returns>
    /// <param name="obj">检索其代信息的对象。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int GetGeneration(object obj);

    /// <summary>强制对 0 代到指定代进行即时垃圾回收。</summary>
    /// <param name="generation">最后一代进行垃圾回收次数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="generation" /> 无效。</exception>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static void Collect(int generation)
    {
      GC.Collect(generation, GCCollectionMode.Default);
    }

    /// <summary>强制对所有代进行即时垃圾回收。</summary>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static void Collect()
    {
      GC._Collect(-1, 2);
    }

    /// <summary>强制在 <see cref="T:System.GCCollectionMode" /> 值所指定的时间对 0 代到指定代进行垃圾回收。</summary>
    /// <param name="generation">最后一代进行垃圾回收次数。</param>
    /// <param name="mode">一个枚举值，指定垃圾回收是强制进行（<see cref="F:System.GCCollectionMode.Default" /> 或 <see cref="F:System.GCCollectionMode.Forced" />）还是优化 (<see cref="F:System.GCCollectionMode.Optimized" />)。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="generation" /> 无效。- 或 -<paramref name="mode" /> 不是 <see cref="T:System.GCCollectionMode" /> 值之一。</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static void Collect(int generation, GCCollectionMode mode)
    {
      GC.Collect(generation, mode, true);
    }

    /// <summary>在由 <see cref="T:System.GCCollectionMode" /> 值指定的时间，强制对 0 代到指定代进行垃圾回收，另有数值指定回收是否应该为阻碍性。</summary>
    /// <param name="generation">最后一代进行垃圾回收次数。</param>
    /// <param name="mode">一个枚举值，指定垃圾回收是强制进行（<see cref="F:System.GCCollectionMode.Default" /> 或 <see cref="F:System.GCCollectionMode.Forced" />）还是优化 (<see cref="F:System.GCCollectionMode.Optimized" />)。</param>
    /// <param name="blocking">true 执行阻碍性垃圾回收；false 在可能的情况下执行后台垃圾回收。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="generation" /> 无效。- 或 -<paramref name="mode" /> 不是 <see cref="T:System.GCCollectionMode" /> 值之一。</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static void Collect(int generation, GCCollectionMode mode, bool blocking)
    {
      GC.Collect(generation, mode, blocking, false);
    }

    /// <summary>在由 <see cref="T:System.GCCollectionMode" /> 值指定的时间，强制对 0 代到指定代进行垃圾回收，另有数值指定回收应该为阻碍性还是压缩性。</summary>
    /// <param name="generation">最后一代进行垃圾回收次数。</param>
    /// <param name="mode">一个枚举值，指定垃圾回收是强制进行（<see cref="F:System.GCCollectionMode.Default" /> 或 <see cref="F:System.GCCollectionMode.Forced" />）还是优化 (<see cref="F:System.GCCollectionMode.Optimized" />)。</param>
    /// <param name="blocking">true 执行阻碍性垃圾回收；false 在可能的情况下执行后台垃圾回收。有关详细信息，请参阅备注部分。</param>
    /// <param name="compacting">true 表示压缩小对象堆；false 表示仅进行清理。有关详细信息，请参阅备注部分。</param>
    [SecuritySafeCritical]
    public static void Collect(int generation, GCCollectionMode mode, bool blocking, bool compacting)
    {
      if (generation < 0)
        throw new ArgumentOutOfRangeException("generation", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      if (mode < GCCollectionMode.Default || mode > GCCollectionMode.Optimized)
        throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      int mode1 = 0;
      if (mode == GCCollectionMode.Optimized)
        mode1 |= 4;
      if (compacting)
        mode1 |= 8;
      if (blocking)
        mode1 |= 2;
      else if (!compacting)
        mode1 |= 1;
      GC._Collect(generation, mode1);
    }

    /// <summary>返回已经对对象的指定代进行的垃圾回收次数。</summary>
    /// <returns>自启动进程以来已经对指定代进行的垃圾回收次数。</returns>
    /// <param name="generation">对象的代，将针对此代确定垃圾回收计数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="generation" /> 小于 0。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static int CollectionCount(int generation)
    {
      if (generation < 0)
        throw new ArgumentOutOfRangeException("generation", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      return GC._CollectionCount(generation, 0);
    }

    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal static int CollectionCount(int generation, bool getSpecialGCCount)
    {
      if (generation < 0)
        throw new ArgumentOutOfRangeException("generation", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      return GC._CollectionCount(generation, getSpecialGCCount ? 1 : 0);
    }

    /// <summary>引用指定对象，使其从当前例程开始到调用此方法的那一刻为止均不符合进行垃圾回收的条件。</summary>
    /// <param name="obj">要引用的对象。</param>
    /// <filterpriority>1</filterpriority>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void KeepAlive(object obj)
    {
    }

    /// <summary>返回指定弱引用的目标的当前代数。</summary>
    /// <returns>
    /// <paramref name="wo" /> 的目标的当前代数。</returns>
    /// <param name="wo">引用要确定其代数的目标对象的 <see cref="T:System.WeakReference" />。</param>
    /// <exception cref="T:System.ArgumentException">已经对 <paramref name="wo" /> 执行了垃圾回收。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    public static int GetGeneration(WeakReference wo)
    {
      int generationWr = GC.GetGenerationWR(wo.m_handle);
      GC.KeepAlive((object) wo);
      return generationWr;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void _WaitForPendingFinalizers();

    /// <summary>挂起当前线程，直到处理终结器队列的线程清空该队列为止。</summary>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static void WaitForPendingFinalizers()
    {
      GC._WaitForPendingFinalizers();
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _SuppressFinalize(object o);

    /// <summary>请求公共语言运行时不要调用指定对象的终结器。</summary>
    /// <param name="obj">不得执行其终结器的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static void SuppressFinalize(object obj)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      GC._SuppressFinalize(obj);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _ReRegisterForFinalize(object o);

    /// <summary>请求系统调用指定对象的终结器，此前已为该对象调用 <see cref="M:System.GC.SuppressFinalize(System.Object)" />。</summary>
    /// <param name="obj">必须为其调用终结器的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="obj" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static void ReRegisterForFinalize(object obj)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      GC._ReRegisterForFinalize(obj);
    }

    /// <summary>检索当前认为要分配的字节数。一个参数，指示此方法是否可以等待较短间隔再返回，以便系统回收垃圾和终结对象。</summary>
    /// <returns>一个数字，它是托管内存中当前所分配字节数的可用的最佳近似值。</returns>
    /// <param name="forceFullCollection">如果此方法可以在返回之前等待垃圾回收发生，则为 true；否则为 false。</param>
    /// <filterpriority>1</filterpriority>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static long GetTotalMemory(bool forceFullCollection)
    {
      long totalMemory = GC.GetTotalMemory();
      if (!forceFullCollection)
        return totalMemory;
      int num1 = 20;
      long num2 = totalMemory;
      float num3;
      do
      {
        GC.WaitForPendingFinalizers();
        GC.Collect();
        long num4 = num2;
        num2 = GC.GetTotalMemory();
        num3 = (float) (num2 - num4) / (float) num4;
      }
      while (num1-- > 0 && (-0.05 >= (double) num3 || (double) num3 >= 0.05));
      return num2;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool _RegisterForFullGCNotification(int maxGenerationPercentage, int largeObjectHeapPercentage);

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool _CancelFullGCNotification();

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int _WaitForFullGCApproach(int millisecondsTimeout);

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int _WaitForFullGCComplete(int millisecondsTimeout);

    /// <summary>指定当条件支持完整垃圾回收以及回收完成时，应引发垃圾回收通知。</summary>
    /// <param name="maxGenerationThreshold">一个介于 1 和 99 之间的数字，指定根据第 2 代中保留的对象，应何时引发通知。</param>
    /// <param name="largeObjectHeapThreshold">一个介于 1 和 99 之间的数字，指定根据大对象堆中分配的对象，应何时引发通知。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="maxGenerationThreshold " /> 或 <paramref name="largeObjectHeapThreshold " /> 不在 1 和 99 之间。</exception>
    [SecurityCritical]
    public static void RegisterForFullGCNotification(int maxGenerationThreshold, int largeObjectHeapThreshold)
    {
      if (maxGenerationThreshold <= 0 || maxGenerationThreshold >= 100)
        throw new ArgumentOutOfRangeException("maxGenerationThreshold", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper"), (object) 1, (object) 99));
      if (largeObjectHeapThreshold <= 0 || largeObjectHeapThreshold >= 100)
        throw new ArgumentOutOfRangeException("largeObjectHeapThreshold", string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper"), (object) 1, (object) 99));
      if (!GC._RegisterForFullGCNotification(maxGenerationThreshold, largeObjectHeapThreshold))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotWithConcurrentGC"));
    }

    /// <summary>取消注册垃圾回收通知。</summary>
    /// <exception cref="T:System.InvalidOperationException">此成员在启用并发垃圾回收时不可用。请参阅 &lt; gcConcurrent &gt; 有关如何禁用并发垃圾回收的信息的运行时设置。</exception>
    [SecurityCritical]
    public static void CancelFullGCNotification()
    {
      if (!GC._CancelFullGCNotification())
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotWithConcurrentGC"));
    }

    /// <summary>返回已注册通知的状态，用于确定公共语言运行时是否即将引发完整、阻碍性垃圾回收。</summary>
    /// <returns>已注册垃圾回收通知的状态。</returns>
    [SecurityCritical]
    public static GCNotificationStatus WaitForFullGCApproach()
    {
      return (GCNotificationStatus) GC._WaitForFullGCApproach(-1);
    }

    /// <summary>在指定的超时期限内，返回已注册通知的状态，用于确定公共语言运行时是否即将引发完整、阻碍性垃圾回收。</summary>
    /// <returns>已注册垃圾回收通知的状态。</returns>
    /// <param name="millisecondsTimeout">在获取通知状态前等待的时间长度。指定 -1 表示无限期等待。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="millisecondsTimeout" /> 必须为非负数或小于或等于 <see cref="F:System.Int32.MaxValue" /> 或 -1。</exception>
    [SecurityCritical]
    public static GCNotificationStatus WaitForFullGCApproach(int millisecondsTimeout)
    {
      if (millisecondsTimeout < -1)
        throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      return (GCNotificationStatus) GC._WaitForFullGCApproach(millisecondsTimeout);
    }

    /// <summary>返回已注册通知的状态，用于确定公共语言运行时引发的完整、阻碍性垃圾回收是否已完成。</summary>
    /// <returns>已注册垃圾回收通知的状态。</returns>
    [SecurityCritical]
    public static GCNotificationStatus WaitForFullGCComplete()
    {
      return (GCNotificationStatus) GC._WaitForFullGCComplete(-1);
    }

    /// <summary>在指定的超时期限内，返回已注册通知的状态，用于确定公共语言运行时引发的完整、阻碍性垃圾回收是否已完成。</summary>
    /// <returns>已注册垃圾回收通知的状态。</returns>
    /// <param name="millisecondsTimeout">在获取通知状态前等待的时间长度。指定 -1 表示无限期等待。</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="millisecondsTimeout" /> 必须为非负数或小于或等于 <see cref="F:System.Int32.MaxValue" /> 或 -1。</exception>
    [SecurityCritical]
    public static GCNotificationStatus WaitForFullGCComplete(int millisecondsTimeout)
    {
      if (millisecondsTimeout < -1)
        throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      return (GCNotificationStatus) GC._WaitForFullGCComplete(millisecondsTimeout);
    }

    [SecurityCritical]
    private static bool StartNoGCRegionWorker(long totalSize, bool hasLohSize, long lohSize, bool disallowFullBlockingGC)
    {
      switch ((GC.StartNoGCRegionStatus) GC._StartNoGCRegion(totalSize, hasLohSize, lohSize, disallowFullBlockingGC))
      {
        case GC.StartNoGCRegionStatus.AmountTooLarge:
          throw new ArgumentOutOfRangeException("totalSize", "totalSize is too large. For more information about setting the maximum size, see \"Latency Modes\" in http://go.microsoft.com/fwlink/?LinkId=522706");
        case GC.StartNoGCRegionStatus.AlreadyInProgress:
          throw new InvalidOperationException("The NoGCRegion mode was already in progress");
        case GC.StartNoGCRegionStatus.NotEnoughMemory:
          return false;
        default:
          return true;
      }
    }

    /// <summary>如果指定数量的内存可用，则在关键路径执行期间尝试禁止垃圾回收。</summary>
    /// <returns>如果运行时能够调配所需数量的内存，且垃圾回收器能够进入无 GC 区域延迟模式，则为 true；否则为 false。</returns>
    /// <param name="totalSize">在不会触发垃圾回收的情况下分配的内存量（以字节为单位）。它必须小于或等于临时段的大小。有关临时段的大小的信息，请参见 垃圾回收的基础 文章中的“临时代和段”部分。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="totalSize" /> 超出的暂时段的大小。</exception>
    /// <exception cref="T:System.InvalidOperationException">该进程已在没有 GC 区域低滞后时间模式。</exception>
    [SecurityCritical]
    public static bool TryStartNoGCRegion(long totalSize)
    {
      return GC.StartNoGCRegionWorker(totalSize, false, 0L, false);
    }

    /// <summary>如果指定数量的内存可用于大对象堆和小对象堆，则在关键路径执行期间尝试禁止垃圾回收。</summary>
    /// <returns>如果运行时能够调配所需数量的内存，且垃圾回收器能够进入无 GC 区域延迟模式，则为 true；否则为 false。</returns>
    /// <param name="totalSize">在不会触发垃圾回收的情况下分配的内存量（以字节为单位）。<paramref name="totalSize" /> –<paramref name="lohSize" /> 必须小于或等于临时段的大小。有关临时段的大小的信息，请参见 垃圾回收的基础 文章中的“临时代和段”部分。</param>
    /// <param name="lohSize">
    /// <paramref name="totalSize" /> 中用于大对象堆 (LOH) 分配的字节数。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="totalSize" /> – <paramref name="lohSize" />  超出暂时段的大小。</exception>
    /// <exception cref="T:System.InvalidOperationException">该进程已在没有 GC 区域低滞后时间模式。</exception>
    [SecurityCritical]
    public static bool TryStartNoGCRegion(long totalSize, long lohSize)
    {
      return GC.StartNoGCRegionWorker(totalSize, true, lohSize, false);
    }

    /// <summary>如果指定数量的内存可用，则在关键路径执行期间尝试禁止垃圾回收；并在初始没有足够内存可用的情况下，控制垃圾回收器是否进行完整的阻碍性垃圾回收。</summary>
    /// <returns>如果运行时能够调配所需数量的内存，且垃圾回收器能够进入无 GC 区域延迟模式，则为 true；否则为 false。</returns>
    /// <param name="totalSize">在不会触发垃圾回收的情况下分配的内存量（以字节为单位）。它必须小于或等于临时段的大小。有关临时段的大小的信息，请参见 垃圾回收的基础 文章中的“临时代和段”部分。</param>
    /// <param name="disallowFullBlockingGC">true 表示如果垃圾回收器初始无法分配 <paramref name="totalSize" /> 字节，则忽略完整的阻碍性垃圾回收；否则为 false。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="totalSize" /> 超出的暂时段的大小。</exception>
    /// <exception cref="T:System.InvalidOperationException">该进程已在没有 GC 区域低滞后时间模式。</exception>
    [SecurityCritical]
    public static bool TryStartNoGCRegion(long totalSize, bool disallowFullBlockingGC)
    {
      return GC.StartNoGCRegionWorker(totalSize, false, 0L, disallowFullBlockingGC);
    }

    /// <summary>如果指定数量的内存可用大对象堆和小对象堆，则在关键路径执行期间尝试禁止垃圾回收；并在初始没有足够内存可用的情况下，控制垃圾回收器是否进行完整的阻碍性垃圾回收。</summary>
    /// <returns>如果运行时能够调配所需数量的内存，且垃圾回收器能够进入无 GC 区域延迟模式，则为 true；否则为 false。</returns>
    /// <param name="totalSize">在不会触发垃圾回收的情况下分配的内存量（以字节为单位）。<paramref name="totalSize" /> –<paramref name="lohSize" /> 必须小于或等于临时段的大小。有关临时段的大小的信息，请参见 垃圾回收的基础 文章中的“临时代和段”部分。</param>
    /// <param name="lohSize">
    /// <paramref name="totalSize" /> 中用于大对象堆 (LOH) 分配的字节数。</param>
    /// <param name="disallowFullBlockingGC">true 表示如果垃圾回收器初始无法分配小对象堆 (SOH) 和 LOH 上的指定内存，则忽略完整的阻碍性垃圾回收；否则为 false。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="totalSize" /> – <paramref name="lohSize" />  超出暂时段的大小。</exception>
    /// <exception cref="T:System.InvalidOperationException">该进程已在没有 GC 区域低滞后时间模式。</exception>
    [SecurityCritical]
    public static bool TryStartNoGCRegion(long totalSize, long lohSize, bool disallowFullBlockingGC)
    {
      return GC.StartNoGCRegionWorker(totalSize, true, lohSize, disallowFullBlockingGC);
    }

    [SecurityCritical]
    private static GC.EndNoGCRegionStatus EndNoGCRegionWorker()
    {
      switch ((GC.EndNoGCRegionStatus) GC._EndNoGCRegion())
      {
        case GC.EndNoGCRegionStatus.NotInProgress:
          throw new InvalidOperationException("NoGCRegion mode must be set");
        case GC.EndNoGCRegionStatus.GCInduced:
          throw new InvalidOperationException("Garbage collection was induced in NoGCRegion mode");
        case GC.EndNoGCRegionStatus.AllocationExceeded:
          throw new InvalidOperationException("Allocated memory exceeds specified memory for NoGCRegion mode");
        default:
          return GC.EndNoGCRegionStatus.Succeeded;
      }
    }

    /// <summary>结束无 GC 区域延迟模式。</summary>
    /// <exception cref="T:System.InvalidOperationException">垃圾回收器不是在无 GC 区域滞后时间模式。有关详细信息，请参阅备注部分。- 或 -由于垃圾回收引起，无 GC 区域滞后时间模式已之前结束。- 或 -内存分配超出对调用中指定的量 <see cref="M:System.GC.TryStartNoGCRegion(System.Int64)" /> 方法。</exception>
    [SecurityCritical]
    public static void EndNoGCRegion()
    {
      int num = (int) GC.EndNoGCRegionWorker();
    }

    private enum StartNoGCRegionStatus
    {
      Succeeded,
      NotEnoughMemory,
      AmountTooLarge,
      AlreadyInProgress,
    }

    private enum EndNoGCRegionStatus
    {
      Succeeded,
      NotInProgress,
      GCInduced,
      AllocationExceeded,
    }
  }
}
