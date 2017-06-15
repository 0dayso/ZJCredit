// Decompiled with JetBrains decompiler
// Type: System.Threading.ThreadState
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Threading
{
  /// <summary>指定 <see cref="T:System.Threading.Thread" /> 的执行状态。</summary>
  /// <filterpriority>1</filterpriority>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum ThreadState
  {
    Running = 0,
    StopRequested = 1,
    SuspendRequested = 2,
    Background = 4,
    Unstarted = 8,
    Stopped = 16,
    WaitSleepJoin = 32,
    Suspended = 64,
    AbortRequested = 128,
    Aborted = 256,
  }
}
