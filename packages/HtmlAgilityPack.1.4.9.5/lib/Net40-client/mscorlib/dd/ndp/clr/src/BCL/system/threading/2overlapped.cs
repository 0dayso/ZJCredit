// Decompiled with JetBrains decompiler
// Type: System.Threading.Overlapped
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
  /// <summary>提供一个 Win32 OVERLAPPED 结构的托管表示形式，包括将信息从 <see cref="T:System.Threading.Overlapped" /> 实例传输到 <see cref="T:System.Threading.NativeOverlapped" /> 结构的方法。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  public class Overlapped
  {
    private static PinnableBufferCache s_overlappedDataCache = new PinnableBufferCache("System.Threading.OverlappedData", new Func<object>(Overlapped.\u003C\u003Ec.\u003C\u003E9.\u003C\u002Ecctor\u003Eb__30_0));
    private OverlappedData m_overlappedData;

    /// <summary>获取或设置提供 I/O 操作的状态信息的对象。</summary>
    /// <returns>实现 <see cref="T:System.IAsyncResult" /> 接口的对象。</returns>
    /// <filterpriority>2</filterpriority>
    public IAsyncResult AsyncResult
    {
      get
      {
        return this.m_overlappedData.m_asyncResult;
      }
      set
      {
        this.m_overlappedData.m_asyncResult = value;
      }
    }

    /// <summary>获取或设置启动传输的文件位置的低序位字。文件位置是距文件起始处的字节偏移量。</summary>
    /// <returns>表示文件位置低位字的 <see cref="T:System.Int32" /> 值。</returns>
    /// <filterpriority>2</filterpriority>
    public int OffsetLow
    {
      get
      {
        return this.m_overlappedData.m_nativeOverlapped.OffsetLow;
      }
      set
      {
        this.m_overlappedData.m_nativeOverlapped.OffsetLow = value;
      }
    }

    /// <summary>获取或设置启动传输的文件位置的高序位字。文件位置是距文件起始处的字节偏移量。</summary>
    /// <returns>表示文件位置高位字的 <see cref="T:System.Int32" /> 值。</returns>
    /// <filterpriority>2</filterpriority>
    public int OffsetHigh
    {
      get
      {
        return this.m_overlappedData.m_nativeOverlapped.OffsetHigh;
      }
      set
      {
        this.m_overlappedData.m_nativeOverlapped.OffsetHigh = value;
      }
    }

    /// <summary>获取或设置当 I/O 操作完成时终止的同步事件的 32 位整型句柄。</summary>
    /// <returns>表示同步事件句柄的 <see cref="T:System.Int32" /> 值。</returns>
    /// <filterpriority>2</filterpriority>
    [Obsolete("This property is not 64-bit compatible.  Use EventHandleIntPtr instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
    public int EventHandle
    {
      get
      {
        return this.m_overlappedData.UserHandle.ToInt32();
      }
      set
      {
        this.m_overlappedData.UserHandle = new IntPtr(value);
      }
    }

    /// <summary>获取或设置当 I/O 操作完成时终止的同步事件的句柄。</summary>
    /// <returns>表示事件句柄的 <see cref="T:System.IntPtr" />。</returns>
    /// <filterpriority>2</filterpriority>
    [ComVisible(false)]
    public IntPtr EventHandleIntPtr
    {
      get
      {
        return this.m_overlappedData.UserHandle;
      }
      set
      {
        this.m_overlappedData.UserHandle = value;
      }
    }

    internal _IOCompletionCallback iocbHelper
    {
      get
      {
        return this.m_overlappedData.m_iocbHelper;
      }
    }

    internal IOCompletionCallback UserCallback
    {
      [SecurityCritical] get
      {
        return this.m_overlappedData.m_iocb;
      }
    }

    /// <summary>初始化 <see cref="T:System.Threading.Overlapped" /> 类的新的空实例。</summary>
    public Overlapped()
    {
      this.m_overlappedData = (OverlappedData) Overlapped.s_overlappedDataCache.Allocate();
      this.m_overlappedData.m_overlapped = this;
    }

    /// <summary>用指定的文件位置、当 I/O 操作完成时终止的事件的句柄、以及一个用来返回操作结果的接口初始化 <see cref="T:System.Threading.Overlapped" /> 类的新实例。</summary>
    /// <param name="offsetLo">启动传输的文件位置的低位字。</param>
    /// <param name="offsetHi">启动传输的文件位置的高位字。</param>
    /// <param name="hEvent">当 I/O 操作完成时终止的事件的句柄。</param>
    /// <param name="ar">一个实现 <see cref="T:System.IAsyncResult" /> 接口并提供 I/O 操作的状态信息的对象。</param>
    public Overlapped(int offsetLo, int offsetHi, IntPtr hEvent, IAsyncResult ar)
    {
      this.m_overlappedData = (OverlappedData) Overlapped.s_overlappedDataCache.Allocate();
      this.m_overlappedData.m_overlapped = this;
      this.m_overlappedData.m_nativeOverlapped.OffsetLow = offsetLo;
      this.m_overlappedData.m_nativeOverlapped.OffsetHigh = offsetHi;
      this.m_overlappedData.UserHandle = hEvent;
      this.m_overlappedData.m_asyncResult = ar;
    }

    /// <summary>用指定的文件位置、当 I/O 操作完成时终止的事件的 32 位整型句柄、以及一个用来返回操作结果的接口初始化 <see cref="T:System.Threading.Overlapped" /> 类的新实例。</summary>
    /// <param name="offsetLo">启动传输的文件位置的低位字。</param>
    /// <param name="offsetHi">启动传输的文件位置的高位字。</param>
    /// <param name="hEvent">当 I/O 操作完成时终止的事件的句柄。</param>
    /// <param name="ar">一个实现 <see cref="T:System.IAsyncResult" /> 接口并提供 I/O 操作的状态信息的对象。</param>
    [Obsolete("This constructor is not 64-bit compatible.  Use the constructor that takes an IntPtr for the event handle.  http://go.microsoft.com/fwlink/?linkid=14202")]
    public Overlapped(int offsetLo, int offsetHi, int hEvent, IAsyncResult ar)
      : this(offsetLo, offsetHi, new IntPtr(hEvent), ar)
    {
    }

    /// <summary>将当前实例打包为一个 <see cref="T:System.Threading.NativeOverlapped" /> 结构，并指定当异步 I/O 操作完成时调用的委托。</summary>
    /// <returns>指向 <see cref="T:System.Threading.NativeOverlapped" /> 结构的非托管指针。</returns>
    /// <param name="iocb">一个 <see cref="T:System.Threading.IOCompletionCallback" /> 委托，表示在异步 I/O 操作完成时调用的回调方法。</param>
    /// <exception cref="T:System.InvalidOperationException">已对当前的 <see cref="T:System.Threading.Overlapped" /> 打包。</exception>
    /// <filterpriority>2</filterpriority>
    [SecurityCritical]
    [Obsolete("This method is not safe.  Use Pack (iocb, userData) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
    [CLSCompliant(false)]
    public unsafe NativeOverlapped* Pack(IOCompletionCallback iocb)
    {
      return this.Pack(iocb, (object) null);
    }

    /// <summary>将当前实例打包为一个 <see cref="T:System.Threading.NativeOverlapped" /> 结构，并指定当异步 I/O 操作完成时调用的委托，以及作为缓冲区的托管对象。</summary>
    /// <returns>指向 <see cref="T:System.Threading.NativeOverlapped" /> 结构的非托管指针。</returns>
    /// <param name="iocb">一个 <see cref="T:System.Threading.IOCompletionCallback" /> 委托，表示在异步 I/O 操作完成时调用的回调方法。</param>
    /// <param name="userData">对象或对象的数组，表示用于操作的输入或输出缓冲区。每个对象都表示一个缓冲区，例如字节数组。</param>
    /// <exception cref="T:System.InvalidOperationException">已对当前的 <see cref="T:System.Threading.Overlapped" /> 打包。</exception>
    /// <filterpriority>2</filterpriority>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ComVisible(false)]
    public unsafe NativeOverlapped* Pack(IOCompletionCallback iocb, object userData)
    {
      return this.m_overlappedData.Pack(iocb, userData);
    }

    /// <summary>将当前实例打包为一个 <see cref="T:System.Threading.NativeOverlapped" /> 结构，并指定当异步 I/O 操作完成时调用的委托。不传播该调用堆栈。</summary>
    /// <returns>指向 <see cref="T:System.Threading.NativeOverlapped" /> 结构的非托管指针。</returns>
    /// <param name="iocb">一个 <see cref="T:System.Threading.IOCompletionCallback" /> 委托，表示在异步 I/O 操作完成时调用的回调方法。</param>
    /// <exception cref="T:System.InvalidOperationException">已对当前的 <see cref="T:System.Threading.Overlapped" /> 打包。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecurityCritical]
    [Obsolete("This method is not safe.  Use UnsafePack (iocb, userData) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
    [CLSCompliant(false)]
    public unsafe NativeOverlapped* UnsafePack(IOCompletionCallback iocb)
    {
      return this.UnsafePack(iocb, (object) null);
    }

    /// <summary>将当前实例打包为一个 <see cref="T:System.Threading.NativeOverlapped" /> 结构，并指定当异步 I/O 操作完成时调用的委托，以及作为缓冲区的托管对象。不传播该调用堆栈。</summary>
    /// <returns>指向 <see cref="T:System.Threading.NativeOverlapped" /> 结构的非托管指针。</returns>
    /// <param name="iocb">一个 <see cref="T:System.Threading.IOCompletionCallback" /> 委托，表示在异步 I/O 操作完成时调用的回调方法。</param>
    /// <param name="userData">对象或对象的数组，表示用于操作的输入或输出缓冲区。每个对象都表示一个缓冲区，例如字节数组。</param>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.InvalidOperationException">已对当前的 <see cref="T:System.Threading.Overlapped" /> 打包。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ComVisible(false)]
    public unsafe NativeOverlapped* UnsafePack(IOCompletionCallback iocb, object userData)
    {
      return this.m_overlappedData.UnsafePack(iocb, userData);
    }

    /// <summary>将指定的非托管 <see cref="T:System.Threading.NativeOverlapped" /> 结构解压缩为 <see cref="T:System.Threading.Overlapped" /> 对象。</summary>
    /// <returns>一个 <see cref="T:System.Threading.Overlapped" /> 对象，包含从本机结构解压缩的信息。</returns>
    /// <param name="nativeOverlappedPtr">指向 <see cref="T:System.Threading.NativeOverlapped" /> 结构的非托管指针。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="nativeOverlappedPtr" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    [SecurityCritical]
    [CLSCompliant(false)]
    public static unsafe Overlapped Unpack(NativeOverlapped* nativeOverlappedPtr)
    {
      if ((IntPtr) nativeOverlappedPtr == IntPtr.Zero)
        throw new ArgumentNullException("nativeOverlappedPtr");
      return OverlappedData.GetOverlappedFromNative(nativeOverlappedPtr).m_overlapped;
    }

    /// <summary>释放与 <see cref="Overload:System.Threading.Overlapped.Pack" /> 方法分配的本机重叠结构关联的非托管内存。</summary>
    /// <param name="nativeOverlappedPtr">指向要释放的 <see cref="T:System.Threading.NativeOverlapped" /> 结构的指针。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="nativeOverlappedPtr" /> 为 null。</exception>
    /// <filterpriority>1</filterpriority>
    [SecurityCritical]
    [CLSCompliant(false)]
    public static unsafe void Free(NativeOverlapped* nativeOverlappedPtr)
    {
      if ((IntPtr) nativeOverlappedPtr == IntPtr.Zero)
        throw new ArgumentNullException("nativeOverlappedPtr");
      Overlapped overlapped = OverlappedData.GetOverlappedFromNative(nativeOverlappedPtr).m_overlapped;
      OverlappedData.FreeNativeOverlapped(nativeOverlappedPtr);
      OverlappedData overlappedData = overlapped.m_overlappedData;
      // ISSUE: variable of the null type
      __Null local = null;
      overlapped.m_overlappedData = (OverlappedData) local;
      overlappedData.ReInitialize();
      Overlapped.s_overlappedDataCache.Free((object) overlappedData);
    }
  }
}
