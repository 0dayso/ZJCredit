// Decompiled with JetBrains decompiler
// Type: System.Threading.RegisteredWaitHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
  /// <summary>表示在调用 <see cref="M:System.Threading.ThreadPool.RegisterWaitForSingleObject(System.Threading.WaitHandle,System.Threading.WaitOrTimerCallback,System.Object,System.UInt32,System.Boolean)" /> 时已注册的句柄。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  public sealed class RegisteredWaitHandle : MarshalByRefObject
  {
    private RegisteredWaitHandleSafe internalRegisteredWait;

    internal RegisteredWaitHandle()
    {
      this.internalRegisteredWait = new RegisteredWaitHandleSafe();
    }

    internal void SetHandle(IntPtr handle)
    {
      this.internalRegisteredWait.SetHandle(handle);
    }

    [SecurityCritical]
    internal void SetWaitObject(WaitHandle waitObject)
    {
      this.internalRegisteredWait.SetWaitObject(waitObject);
    }

    /// <summary>取消由 <see cref="M:System.Threading.ThreadPool.RegisterWaitForSingleObject(System.Threading.WaitHandle,System.Threading.WaitOrTimerCallback,System.Object,System.UInt32,System.Boolean)" /> 方法发出的已注册等待操作。</summary>
    /// <returns>如果函数成功，则为 true；否则为 false。</returns>
    /// <param name="waitObject">要发出信号的 <see cref="T:System.Threading.WaitHandle" />。</param>
    /// <filterpriority>2</filterpriority>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public bool Unregister(WaitHandle waitObject)
    {
      return this.internalRegisteredWait.Unregister(waitObject);
    }
  }
}
