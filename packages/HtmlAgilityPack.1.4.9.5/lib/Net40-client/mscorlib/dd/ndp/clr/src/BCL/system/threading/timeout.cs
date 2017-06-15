// Decompiled with JetBrains decompiler
// Type: System.Threading.Timeout
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Threading
{
  /// <summary>包含指定无限期超时间隔的常数。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public static class Timeout
  {
    /// <summary>用于指定无限等待期限的常数，接受 <see cref="T:System.TimeSpan" /> 参数的方法。</summary>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public static readonly TimeSpan InfiniteTimeSpan = new TimeSpan(0, 0, 0, 0, -1);
    /// <summary>一个用于指定无限长等待时间的常数，适用于接受 <see cref="T:System.Int32" /> 参数的线程处理方法。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public const int Infinite = -1;
    internal const uint UnsignedInfinite = 4294967295;
  }
}
