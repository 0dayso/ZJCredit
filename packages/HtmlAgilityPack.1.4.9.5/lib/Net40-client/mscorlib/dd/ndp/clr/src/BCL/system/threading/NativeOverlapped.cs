// Decompiled with JetBrains decompiler
// Type: System.Threading.NativeOverlapped
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Threading
{
  /// <summary>提供一种显式布局，它在非托管代码中可见，并将具有与 Win32 OVERLAPPED 结构相同的布局且在结尾有附加保留的字段。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  public struct NativeOverlapped
  {
    /// <summary>指定系统相关的状态。保留给操作系统使用。</summary>
    /// <filterpriority>2</filterpriority>
    public IntPtr InternalLow;
    /// <summary>指定传输的数据长度。保留给操作系统使用。</summary>
    /// <filterpriority>2</filterpriority>
    public IntPtr InternalHigh;
    /// <summary>指定开始传输的文件位置。</summary>
    /// <filterpriority>2</filterpriority>
    public int OffsetLow;
    /// <summary>指定开始传输的字节偏移量中的高字。</summary>
    /// <filterpriority>2</filterpriority>
    public int OffsetHigh;
    /// <summary>指定在操作完成后设置为终止状态的事件句柄。调用进程必须在调用任何重叠函数之前将此成员设置为零或有效事件句柄。</summary>
    /// <filterpriority>2</filterpriority>
    public IntPtr EventHandle;
  }
}
