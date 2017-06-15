// Decompiled with JetBrains decompiler
// Type: System.Threading.IOCompletionCallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
  /// <summary>当 I/O 操作在线程池上完成时接收错误代码、字节数和重叠值类型。</summary>
  /// <param name="errorCode">错误代码。</param>
  /// <param name="numBytes">传输的字节数。</param>
  /// <param name="pOVERLAP">
  /// <see cref="T:System.Threading.NativeOverlapped" />，表示指向本机重叠值类型的非托管指针。</param>
  /// <filterpriority>2</filterpriority>
  [SecurityCritical]
  [CLSCompliant(false)]
  [ComVisible(true)]
  public unsafe delegate void IOCompletionCallback(uint errorCode, uint numBytes, NativeOverlapped* pOVERLAP);
}
