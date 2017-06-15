// Decompiled with JetBrains decompiler
// Type: System.Threading.WaitHandleExtensions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.Security;

namespace System.Threading
{
  /// <summary>提供了便利方法，以使用安全句柄为等待处理。</summary>
  [__DynamicallyInvokable]
  public static class WaitHandleExtensions
  {
    /// <summary>获取安全句柄的本机操作系统等待句柄。</summary>
    /// <returns>包装本机操作系统的安全等待句柄等待句柄。</returns>
    /// <param name="waitHandle">本机操作系统句柄。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="waitHandle" /> 为 null。</exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static SafeWaitHandle GetSafeWaitHandle(this WaitHandle waitHandle)
    {
      if (waitHandle == null)
        throw new ArgumentNullException("waitHandle");
      return waitHandle.SafeWaitHandle;
    }

    /// <summary>设置安全句柄的本机操作系统等待句柄。</summary>
    /// <param name="waitHandle">封装等待对共享资源的独占访问的特定于操作系统的对象某种等待句柄。</param>
    /// <param name="value">安全句柄来包装操作系统句柄。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="waitHandle" /> 为 null。</exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static void SetSafeWaitHandle(this WaitHandle waitHandle, SafeWaitHandle value)
    {
      if (waitHandle == null)
        throw new ArgumentNullException("waitHandle");
      waitHandle.SafeWaitHandle = value;
    }
  }
}
