// Decompiled with JetBrains decompiler
// Type: System.IO.FileStreamAsyncResult
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Diagnostics.Tracing;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.IO
{
  internal sealed class FileStreamAsyncResult : IAsyncResult
  {
    private AsyncCallback _userCallback;
    private object _userStateObject;
    private ManualResetEvent _waitHandle;
    [SecurityCritical]
    private SafeFileHandle _handle;
    [SecurityCritical]
    private unsafe NativeOverlapped* _overlapped;
    internal int _EndXxxCalled;
    private int _numBytes;
    private int _errorCode;
    private int _numBufferedBytes;
    private bool _isWrite;
    private bool _isComplete;
    private bool _completedSynchronously;
    [SecurityCritical]
    private static IOCompletionCallback s_IOCallback;

    internal unsafe NativeOverlapped* OverLapped
    {
      [SecurityCritical] get
      {
        return this._overlapped;
      }
    }

    internal unsafe bool IsAsync
    {
      [SecuritySafeCritical] get
      {
        return (IntPtr) this._overlapped != IntPtr.Zero;
      }
    }

    internal int NumBytes
    {
      get
      {
        return this._numBytes;
      }
    }

    internal int ErrorCode
    {
      get
      {
        return this._errorCode;
      }
    }

    internal int NumBufferedBytes
    {
      get
      {
        return this._numBufferedBytes;
      }
    }

    internal int NumBytesRead
    {
      get
      {
        return this._numBytes + this._numBufferedBytes;
      }
    }

    internal bool IsWrite
    {
      get
      {
        return this._isWrite;
      }
    }

    public object AsyncState
    {
      get
      {
        return this._userStateObject;
      }
    }

    public bool IsCompleted
    {
      get
      {
        return this._isComplete;
      }
    }

    public unsafe WaitHandle AsyncWaitHandle
    {
      [SecuritySafeCritical] get
      {
        if (this._waitHandle == null)
        {
          ManualResetEvent manualResetEvent = new ManualResetEvent(false);
          if ((IntPtr) this._overlapped != IntPtr.Zero && this._overlapped->EventHandle != IntPtr.Zero)
            manualResetEvent.SafeWaitHandle = new SafeWaitHandle(this._overlapped->EventHandle, true);
          if (Interlocked.CompareExchange<ManualResetEvent>(ref this._waitHandle, manualResetEvent, (ManualResetEvent) null) == null)
          {
            if (this._isComplete)
              this._waitHandle.Set();
          }
          else
            manualResetEvent.Close();
        }
        return (WaitHandle) this._waitHandle;
      }
    }

    public bool CompletedSynchronously
    {
      get
      {
        return this._completedSynchronously;
      }
    }

    [SecuritySafeCritical]
    internal unsafe FileStreamAsyncResult(int numBufferedBytes, byte[] bytes, SafeFileHandle handle, AsyncCallback userCallback, object userStateObject, bool isWrite)
    {
      this._userCallback = userCallback;
      this._userStateObject = userStateObject;
      this._isWrite = isWrite;
      this._numBufferedBytes = numBufferedBytes;
      this._handle = handle;
      this._waitHandle = new ManualResetEvent(false);
      Overlapped overlapped = new Overlapped(0, 0, IntPtr.Zero, (IAsyncResult) this);
      if (userCallback != null)
      {
        IOCompletionCallback iocb = FileStreamAsyncResult.s_IOCallback;
        if (iocb == null)
          FileStreamAsyncResult.s_IOCallback = iocb = new IOCompletionCallback(FileStreamAsyncResult.AsyncFSCallback);
        this._overlapped = overlapped.Pack(iocb, (object) bytes);
      }
      else
        this._overlapped = overlapped.UnsafePack((IOCompletionCallback) null, (object) bytes);
    }

    private FileStreamAsyncResult(int numBufferedBytes, AsyncCallback userCallback, object userStateObject, bool isWrite)
    {
      this._userCallback = userCallback;
      this._userStateObject = userStateObject;
      this._isWrite = isWrite;
      this._numBufferedBytes = numBufferedBytes;
    }

    internal static FileStreamAsyncResult CreateBufferedReadResult(int numBufferedBytes, AsyncCallback userCallback, object userStateObject, bool isWrite)
    {
      FileStreamAsyncResult streamAsyncResult = new FileStreamAsyncResult(numBufferedBytes, userCallback, userStateObject, isWrite);
      streamAsyncResult.CallUserCallback();
      return streamAsyncResult;
    }

    private void CallUserCallbackWorker()
    {
      this._isComplete = true;
      Thread.MemoryBarrier();
      if (this._waitHandle != null)
        this._waitHandle.Set();
      this._userCallback((IAsyncResult) this);
    }

    internal void CallUserCallback()
    {
      if (this._userCallback != null)
      {
        this._completedSynchronously = false;
        ThreadPool.QueueUserWorkItem((WaitCallback) (state => ((FileStreamAsyncResult) state).CallUserCallbackWorker()), (object) this);
      }
      else
      {
        this._isComplete = true;
        Thread.MemoryBarrier();
        if (this._waitHandle == null)
          return;
        this._waitHandle.Set();
      }
    }

    [SecurityCritical]
    internal unsafe void ReleaseNativeResource()
    {
      if ((IntPtr) this._overlapped == IntPtr.Zero)
        return;
      Overlapped.Free(this._overlapped);
    }

    internal void Wait()
    {
      if (this._waitHandle == null)
        return;
      try
      {
        this._waitHandle.WaitOne();
      }
      finally
      {
        this._waitHandle.Close();
      }
    }

    [SecurityCritical]
    private static unsafe void AsyncFSCallback(uint errorCode, uint numBytes, NativeOverlapped* pOverlapped)
    {
      FileStreamAsyncResult streamAsyncResult = (FileStreamAsyncResult) Overlapped.Unpack(pOverlapped).AsyncResult;
      streamAsyncResult._numBytes = (int) numBytes;
      if (FrameworkEventSource.IsInitialized && FrameworkEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords) 16))
        FrameworkEventSource.Log.ThreadTransferReceive((long) streamAsyncResult.OverLapped, 2, string.Empty);
      if ((int) errorCode == 109 || (int) errorCode == 232)
        errorCode = 0U;
      streamAsyncResult._errorCode = (int) errorCode;
      streamAsyncResult._completedSynchronously = false;
      streamAsyncResult._isComplete = true;
      Thread.MemoryBarrier();
      ManualResetEvent manualResetEvent = streamAsyncResult._waitHandle;
      if (manualResetEvent != null && !manualResetEvent.Set())
        __Error.WinIOError();
      AsyncCallback asyncCallback = streamAsyncResult._userCallback;
      if (asyncCallback == null)
        return;
      asyncCallback((IAsyncResult) streamAsyncResult);
    }

    [SecuritySafeCritical]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    internal unsafe void Cancel()
    {
      if (this.IsCompleted || this._handle.IsInvalid || Win32Native.CancelIoEx(this._handle, this._overlapped))
        return;
      int lastWin32Error = Marshal.GetLastWin32Error();
      if (lastWin32Error == 1168)
        return;
      __Error.WinIOError(lastWin32Error, string.Empty);
    }
  }
}
