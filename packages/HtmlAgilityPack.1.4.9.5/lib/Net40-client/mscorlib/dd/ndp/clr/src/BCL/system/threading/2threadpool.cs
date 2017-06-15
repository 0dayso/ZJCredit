// Decompiled with JetBrains decompiler
// Type: System.Threading.ThreadPoolWorkQueueThreadLocals
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Threading
{
  internal sealed class ThreadPoolWorkQueueThreadLocals
  {
    public readonly Random random = new Random(Thread.CurrentThread.ManagedThreadId);
    [ThreadStatic]
    [SecurityCritical]
    public static ThreadPoolWorkQueueThreadLocals threadLocals;
    public readonly ThreadPoolWorkQueue workQueue;
    public readonly ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue;

    public ThreadPoolWorkQueueThreadLocals(ThreadPoolWorkQueue tpq)
    {
      this.workQueue = tpq;
      this.workStealingQueue = new ThreadPoolWorkQueue.WorkStealingQueue();
      ThreadPoolWorkQueue.allThreadQueues.Add(this.workStealingQueue);
    }

    [SecuritySafeCritical]
    ~ThreadPoolWorkQueueThreadLocals()
    {
      if (Environment.HasShutdownStarted || AppDomain.CurrentDomain.IsFinalizingForUnload())
        return;
      this.CleanUp();
    }

    [SecurityCritical]
    private void CleanUp()
    {
      if (this.workStealingQueue == null)
        return;
      if (this.workQueue != null)
      {
        bool flag = false;
        while (!flag)
        {
          try
          {
          }
          finally
          {
            IThreadPoolWorkItem callback = (IThreadPoolWorkItem) null;
            if (this.workStealingQueue.LocalPop(out callback))
              this.workQueue.Enqueue(callback, true);
            else
              flag = true;
          }
        }
      }
      ThreadPoolWorkQueue.allThreadQueues.Remove(this.workStealingQueue);
    }
  }
}
