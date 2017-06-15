// Decompiled with JetBrains decompiler
// Type: System.Threading.ThreadPoolGlobals
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Threading
{
  internal static class ThreadPoolGlobals
  {
    public static uint tpQuantum = 30;
    public static int processorCount = Environment.ProcessorCount;
    public static bool tpHosted = ThreadPool.IsThreadPoolHosted();
    [SecurityCritical]
    public static ThreadPoolWorkQueue workQueue = new ThreadPoolWorkQueue();
    public static volatile bool vmTpInitialized;
    public static bool enableWorkerTracking;

    [SecuritySafeCritical]
    static ThreadPoolGlobals()
    {
    }
  }
}
