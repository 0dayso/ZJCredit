// Decompiled with JetBrains decompiler
// Type: System.Threading.WaitOrTimerCallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Threading
{
  /// <summary>表示当 <see cref="T:System.Threading.WaitHandle" /> 超时或终止时要调用的方法。</summary>
  /// <param name="state">一个对象，包含回调方法在每次执行时要使用的信息。</param>
  /// <param name="timedOut">如果 <see cref="T:System.Threading.WaitHandle" /> 超时，则为 true；如果其终止，则为 false。</param>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  public delegate void WaitOrTimerCallback(object state, bool timedOut);
}
