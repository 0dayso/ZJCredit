// Decompiled with JetBrains decompiler
// Type: System.Threading.ReaderWriterLock
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>定义支持单个写线程和多个读线程的锁。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public sealed class ReaderWriterLock : CriticalFinalizerObject
  {
    private IntPtr _hWriterEvent;
    private IntPtr _hReaderEvent;
    private IntPtr _hObjectHandle;
    private int _dwState;
    private int _dwULockID;
    private int _dwLLockID;
    private int _dwWriterID;
    private int _dwWriterSeqNum;
    private short _wWriterLevel;

    /// <summary>获取一个值，该值指示当前线程是否持有读线程锁。</summary>
    /// <returns>如果当前线程持有读线程锁，则为 true；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public bool IsReaderLockHeld
    {
      [SecuritySafeCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get
      {
        return this.PrivateGetIsReaderLockHeld();
      }
    }

    /// <summary>获取一个值，该值指示当前线程是否持有写线程锁。</summary>
    /// <returns>如果当前线程持有写线程锁，则为 true；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public bool IsWriterLockHeld
    {
      [SecuritySafeCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get
      {
        return this.PrivateGetIsWriterLockHeld();
      }
    }

    /// <summary>获取当前序列号。</summary>
    /// <returns>当前序列号。</returns>
    /// <filterpriority>2</filterpriority>
    public int WriterSeqNum
    {
      [SecuritySafeCritical] get
      {
        return this.PrivateGetWriterSeqNum();
      }
    }

    /// <summary>初始化 <see cref="T:System.Threading.ReaderWriterLock" /> 类的新实例。</summary>
    [SecuritySafeCritical]
    public ReaderWriterLock()
    {
      this.PrivateInitialize();
    }

    /// <summary>确保垃圾回收器回收 <see cref="T:System.Threading.ReaderWriterLock" /> 对象时释放资源并执行其他清理操作。</summary>
    [SecuritySafeCritical]
    ~ReaderWriterLock()
    {
      this.PrivateDestruct();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void AcquireReaderLockInternal(int millisecondsTimeout);

    /// <summary>使用一个 <see cref="T:System.Int32" /> 超时值获取读线程锁。</summary>
    /// <param name="millisecondsTimeout">以毫秒为单位的超时。</param>
    /// <exception cref="T:System.ApplicationException">
    /// <paramref name="millisecondsTimeout" /> 在授予锁定请求前过期。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public void AcquireReaderLock(int millisecondsTimeout)
    {
      this.AcquireReaderLockInternal(millisecondsTimeout);
    }

    /// <summary>使用一个 <see cref="T:System.TimeSpan" /> 超时值获取读线程锁。</summary>
    /// <param name="timeout">一个 TimeSpan，用于指定超时时间。</param>
    /// <exception cref="T:System.ApplicationException">
    /// <paramref name="timeout" /> 在授予锁定请求前过期。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="timeout" /> 可指定 -1 毫秒以外的任何负值。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public void AcquireReaderLock(TimeSpan timeout)
    {
      long num = (long) timeout.TotalMilliseconds;
      if (num < -1L || num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      this.AcquireReaderLockInternal((int) num);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void AcquireWriterLockInternal(int millisecondsTimeout);

    /// <summary>使用一个 <see cref="T:System.Int32" /> 超时值获取写线程锁。</summary>
    /// <param name="millisecondsTimeout">以毫秒为单位的超时。</param>
    /// <exception cref="T:System.ApplicationException">
    /// <paramref name="timeout" /> 在授予锁定请求前过期。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public void AcquireWriterLock(int millisecondsTimeout)
    {
      this.AcquireWriterLockInternal(millisecondsTimeout);
    }

    /// <summary>使用一个 <see cref="T:System.TimeSpan" /> 超时值获取写线程锁。</summary>
    /// <param name="timeout">TimeSpan，用于指定超时时间。</param>
    /// <exception cref="T:System.ApplicationException">
    /// <paramref name="timeout" /> 在授予锁定请求前过期。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="timeout" /> 可指定 -1 毫秒以外的任何负值。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public void AcquireWriterLock(TimeSpan timeout)
    {
      long num = (long) timeout.TotalMilliseconds;
      if (num < -1L || num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      this.AcquireWriterLockInternal((int) num);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void ReleaseReaderLockInternal();

    /// <summary>减少锁计数。</summary>
    /// <exception cref="T:System.ApplicationException">线程没有读线程锁或写线程锁。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public void ReleaseReaderLock()
    {
      this.ReleaseReaderLockInternal();
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void ReleaseWriterLockInternal();

    /// <summary>减少写线程锁上的锁计数。</summary>
    /// <exception cref="T:System.ApplicationException">线程没有写线程锁。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public void ReleaseWriterLock()
    {
      this.ReleaseWriterLockInternal();
    }

    /// <summary>使用一个 Int32 超时值将读线程锁升级为写线程锁。</summary>
    /// <returns>一个 <see cref="T:System.Threading.LockCookie" /> 值。</returns>
    /// <param name="millisecondsTimeout">以毫秒为单位的超时。</param>
    /// <exception cref="T:System.ApplicationException">
    /// <paramref name="millisecondsTimeout" /> 在授予锁定请求前过期。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public LockCookie UpgradeToWriterLock(int millisecondsTimeout)
    {
      LockCookie result = new LockCookie();
      this.FCallUpgradeToWriterLock(ref result, millisecondsTimeout);
      return result;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void FCallUpgradeToWriterLock(ref LockCookie result, int millisecondsTimeout);

    /// <summary>使用一个 TimeSpan 超时值将读线程锁升级为写线程锁。</summary>
    /// <returns>一个 <see cref="T:System.Threading.LockCookie" /> 值。</returns>
    /// <param name="timeout">TimeSpan，用于指定超时时间。</param>
    /// <exception cref="T:System.ApplicationException">
    /// <paramref name="timeout" /> 在授予锁定请求前过期。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="timeout" /> 可指定 -1 毫秒以外的任何负值。</exception>
    /// <filterpriority>2</filterpriority>
    public LockCookie UpgradeToWriterLock(TimeSpan timeout)
    {
      long num = (long) timeout.TotalMilliseconds;
      if (num < -1L || num > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      return this.UpgradeToWriterLock((int) num);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void DowngradeFromWriterLockInternal(ref LockCookie lockCookie);

    /// <summary>将线程的锁状态还原为调用 <see cref="M:System.Threading.ReaderWriterLock.UpgradeToWriterLock(System.Int32)" /> 前的状态。</summary>
    /// <param name="lockCookie">一个 <see cref="T:System.Threading.LockCookie" />，由 <see cref="M:System.Threading.ReaderWriterLock.UpgradeToWriterLock(System.Int32)" /> 返回。</param>
    /// <exception cref="T:System.ApplicationException">线程没有写线程锁。</exception>
    /// <exception cref="T:System.NullReferenceException">
    /// <paramref name="lockCookie" /> 的地址为空指针。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public void DowngradeFromWriterLock(ref LockCookie lockCookie)
    {
      this.DowngradeFromWriterLockInternal(ref lockCookie);
    }

    /// <summary>释放锁，不管线程获取锁的次数如何。</summary>
    /// <returns>一个 <see cref="T:System.Threading.LockCookie" /> 值，表示释放的锁。</returns>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public LockCookie ReleaseLock()
    {
      LockCookie result = new LockCookie();
      this.FCallReleaseLock(ref result);
      return result;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void FCallReleaseLock(ref LockCookie result);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void RestoreLockInternal(ref LockCookie lockCookie);

    /// <summary>将线程的锁状态还原为调用 <see cref="M:System.Threading.ReaderWriterLock.ReleaseLock" /> 前的状态。</summary>
    /// <param name="lockCookie">一个 <see cref="T:System.Threading.LockCookie" />，由 <see cref="M:System.Threading.ReaderWriterLock.ReleaseLock" /> 返回。</param>
    /// <exception cref="T:System.NullReferenceException">
    /// <paramref name="lockCookie" /> 的地址为空指针。</exception>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    public void RestoreLock(ref LockCookie lockCookie)
    {
      this.RestoreLockInternal(ref lockCookie);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private bool PrivateGetIsReaderLockHeld();

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private bool PrivateGetIsWriterLockHeld();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private int PrivateGetWriterSeqNum();

    /// <summary>指示获取序列号之后是否已将写线程锁授予某个线程。</summary>
    /// <returns>如果获取序列号之后已将写线程锁授予某一线程，则为 true；否则为 false。</returns>
    /// <param name="seqNum">序列号。</param>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public bool AnyWritersSince(int seqNum);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void PrivateInitialize();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private void PrivateDestruct();
  }
}
