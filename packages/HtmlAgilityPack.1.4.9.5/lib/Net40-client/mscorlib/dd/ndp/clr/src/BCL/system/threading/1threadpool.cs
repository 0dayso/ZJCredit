// Decompiled with JetBrains decompiler
// Type: System.Threading.ThreadPoolWorkQueue
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics.Tracing;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Threading
{
  internal sealed class ThreadPoolWorkQueue
  {
    internal static ThreadPoolWorkQueue.SparseArray<ThreadPoolWorkQueue.WorkStealingQueue> allThreadQueues = new ThreadPoolWorkQueue.SparseArray<ThreadPoolWorkQueue.WorkStealingQueue>(16);
    internal volatile ThreadPoolWorkQueue.QueueSegment queueHead;
    internal volatile ThreadPoolWorkQueue.QueueSegment queueTail;
    internal bool loggingEnabled;
    private volatile int numOutstandingThreadRequests;

    public ThreadPoolWorkQueue()
    {
      this.queueTail = this.queueHead = new ThreadPoolWorkQueue.QueueSegment();
      this.loggingEnabled = FrameworkEventSource.Log.IsEnabled(EventLevel.Verbose, (EventKeywords) 18);
    }

    [SecurityCritical]
    public ThreadPoolWorkQueueThreadLocals EnsureCurrentThreadHasQueue()
    {
      if (ThreadPoolWorkQueueThreadLocals.threadLocals == null)
        ThreadPoolWorkQueueThreadLocals.threadLocals = new ThreadPoolWorkQueueThreadLocals(this);
      return ThreadPoolWorkQueueThreadLocals.threadLocals;
    }

    [SecurityCritical]
    internal void EnsureThreadRequested()
    {
      int num;
      for (int comparand = this.numOutstandingThreadRequests; comparand < ThreadPoolGlobals.processorCount; comparand = num)
      {
        num = Interlocked.CompareExchange(ref this.numOutstandingThreadRequests, comparand + 1, comparand);
        if (num == comparand)
        {
          ThreadPool.RequestWorkerThread();
          break;
        }
      }
    }

    [SecurityCritical]
    internal void MarkThreadRequestSatisfied()
    {
      int num;
      for (int comparand = this.numOutstandingThreadRequests; comparand > 0; comparand = num)
      {
        num = Interlocked.CompareExchange(ref this.numOutstandingThreadRequests, comparand - 1, comparand);
        if (num == comparand)
          break;
      }
    }

    [SecurityCritical]
    public void Enqueue(IThreadPoolWorkItem callback, bool forceGlobal)
    {
      ThreadPoolWorkQueueThreadLocals queueThreadLocals = (ThreadPoolWorkQueueThreadLocals) null;
      if (!forceGlobal)
        queueThreadLocals = ThreadPoolWorkQueueThreadLocals.threadLocals;
      if (this.loggingEnabled)
        FrameworkEventSource.Log.ThreadPoolEnqueueWorkObject((object) callback);
      if (queueThreadLocals != null)
      {
        queueThreadLocals.workStealingQueue.LocalPush(callback);
      }
      else
      {
        ThreadPoolWorkQueue.QueueSegment comparand = this.queueHead;
        while (!comparand.TryEnqueue(callback))
        {
          Interlocked.CompareExchange<ThreadPoolWorkQueue.QueueSegment>(ref comparand.Next, new ThreadPoolWorkQueue.QueueSegment(), (ThreadPoolWorkQueue.QueueSegment) null);
          for (; comparand.Next != null; comparand = this.queueHead)
            Interlocked.CompareExchange<ThreadPoolWorkQueue.QueueSegment>(ref this.queueHead, comparand.Next, comparand);
        }
      }
      this.EnsureThreadRequested();
    }

    [SecurityCritical]
    internal bool LocalFindAndPop(IThreadPoolWorkItem callback)
    {
      ThreadPoolWorkQueueThreadLocals queueThreadLocals = ThreadPoolWorkQueueThreadLocals.threadLocals;
      if (queueThreadLocals == null)
        return false;
      return queueThreadLocals.workStealingQueue.LocalFindAndPop(callback);
    }

    [SecurityCritical]
    public void Dequeue(ThreadPoolWorkQueueThreadLocals tl, out IThreadPoolWorkItem callback, out bool missedSteal)
    {
      callback = (IThreadPoolWorkItem) null;
      missedSteal = false;
      ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue1 = tl.workStealingQueue;
      workStealingQueue1.LocalPop(out callback);
      if (callback == null)
      {
        for (ThreadPoolWorkQueue.QueueSegment comparand = this.queueTail; !comparand.TryDequeue(out callback) && comparand.Next != null && comparand.IsUsedUp(); comparand = this.queueTail)
          Interlocked.CompareExchange<ThreadPoolWorkQueue.QueueSegment>(ref this.queueTail, comparand.Next, comparand);
      }
      if (callback != null)
        return;
      ThreadPoolWorkQueue.WorkStealingQueue[] current = ThreadPoolWorkQueue.allThreadQueues.Current;
      int num = tl.random.Next(current.Length);
      for (int length = current.Length; length > 0; --length)
      {
        ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue2 = Volatile.Read<ThreadPoolWorkQueue.WorkStealingQueue>(ref current[num % current.Length]);
        if (workStealingQueue2 != null && workStealingQueue2 != workStealingQueue1 && workStealingQueue2.TrySteal(out callback, ref missedSteal))
          break;
        ++num;
      }
    }

    [SecurityCritical]
    internal static bool Dispatch()
    {
      ThreadPoolWorkQueue threadPoolWorkQueue = ThreadPoolGlobals.workQueue;
      int tickCount = Environment.TickCount;
      threadPoolWorkQueue.MarkThreadRequestSatisfied();
      threadPoolWorkQueue.loggingEnabled = FrameworkEventSource.Log.IsEnabled(EventLevel.Verbose, (EventKeywords) 18);
      bool flag1 = true;
      IThreadPoolWorkItem callback = (IThreadPoolWorkItem) null;
      try
      {
        ThreadPoolWorkQueueThreadLocals tl = threadPoolWorkQueue.EnsureCurrentThreadHasQueue();
        while ((long) (Environment.TickCount - tickCount) < (long) ThreadPoolGlobals.tpQuantum)
        {
          try
          {
          }
          finally
          {
            bool missedSteal = false;
            threadPoolWorkQueue.Dequeue(tl, out callback, out missedSteal);
            if (callback == null)
              flag1 = missedSteal;
            else
              threadPoolWorkQueue.EnsureThreadRequested();
          }
          if (callback == null)
            return true;
          if (threadPoolWorkQueue.loggingEnabled)
            FrameworkEventSource.Log.ThreadPoolDequeueWorkObject((object) callback);
          if (ThreadPoolGlobals.enableWorkerTracking)
          {
            bool flag2 = false;
            try
            {
              try
              {
              }
              finally
              {
                ThreadPool.ReportThreadStatus(true);
                flag2 = true;
              }
              callback.ExecuteWorkItem();
              callback = (IThreadPoolWorkItem) null;
            }
            finally
            {
              if (flag2)
                ThreadPool.ReportThreadStatus(false);
            }
          }
          else
          {
            callback.ExecuteWorkItem();
            callback = (IThreadPoolWorkItem) null;
          }
          if (!ThreadPool.NotifyWorkItemComplete())
            return false;
        }
        return true;
      }
      catch (ThreadAbortException ex)
      {
        if (callback != null)
          callback.MarkAborted(ex);
        flag1 = false;
      }
      finally
      {
        if (flag1)
          threadPoolWorkQueue.EnsureThreadRequested();
      }
      return true;
    }

    internal class SparseArray<T> where T : class
    {
      private volatile T[] m_array;

      internal T[] Current
      {
        get
        {
          return this.m_array;
        }
      }

      internal SparseArray(int initialSize)
      {
        this.m_array = new T[initialSize];
      }

      internal int Add(T e)
      {
label_0:
        T[] objArray = this.m_array;
        lock (objArray)
        {
          for (int local_3 = 0; local_3 < objArray.Length; ++local_3)
          {
            if ((object) objArray[local_3] == null)
            {
              Volatile.Write<T>(ref objArray[local_3], e);
              return local_3;
            }
            if (local_3 == objArray.Length - 1 && objArray == this.m_array)
            {
              T[] local_5 = new T[objArray.Length * 2];
              Array.Copy((Array) objArray, (Array) local_5, local_3 + 1);
              local_5[local_3 + 1] = e;
              this.m_array = local_5;
              return local_3 + 1;
            }
          }
          goto label_0;
        }
      }

      internal void Remove(T e)
      {
        lock (this.m_array)
        {
          for (int local_2 = 0; local_2 < this.m_array.Length; ++local_2)
          {
            if ((object) this.m_array[local_2] == (object) e)
            {
              Volatile.Write<T>(ref this.m_array[local_2], default (T));
              break;
            }
          }
        }
      }
    }

    internal class WorkStealingQueue
    {
      internal volatile IThreadPoolWorkItem[] m_array = new IThreadPoolWorkItem[32];
      private volatile int m_mask = 31;
      private SpinLock m_foreignLock = new SpinLock(false);
      private const int INITIAL_SIZE = 32;
      private const int START_INDEX = 0;
      private volatile int m_headIndex;
      private volatile int m_tailIndex;

      public void LocalPush(IThreadPoolWorkItem obj)
      {
        int num1 = this.m_tailIndex;
        if (num1 == int.MaxValue)
        {
          bool lockTaken = false;
          try
          {
            this.m_foreignLock.Enter(ref lockTaken);
            if (this.m_tailIndex == int.MaxValue)
            {
              this.m_headIndex = this.m_headIndex & this.m_mask;
              this.m_tailIndex = num1 = this.m_tailIndex & this.m_mask;
            }
          }
          finally
          {
            if (lockTaken)
              this.m_foreignLock.Exit(true);
          }
        }
        if (num1 < this.m_headIndex + this.m_mask)
        {
          Volatile.Write<IThreadPoolWorkItem>(ref this.m_array[num1 & this.m_mask], obj);
          this.m_tailIndex = num1 + 1;
        }
        else
        {
          bool lockTaken = false;
          try
          {
            this.m_foreignLock.Enter(ref lockTaken);
            int num2 = this.m_headIndex;
            int num3 = this.m_tailIndex - this.m_headIndex;
            if (num3 >= this.m_mask)
            {
              IThreadPoolWorkItem[] threadPoolWorkItemArray = new IThreadPoolWorkItem[this.m_array.Length << 1];
              for (int index = 0; index < this.m_array.Length; ++index)
                threadPoolWorkItemArray[index] = this.m_array[index + num2 & this.m_mask];
              this.m_array = threadPoolWorkItemArray;
              this.m_headIndex = 0;
              this.m_tailIndex = num1 = num3;
              this.m_mask = this.m_mask << 1 | 1;
            }
            Volatile.Write<IThreadPoolWorkItem>(ref this.m_array[num1 & this.m_mask], obj);
            this.m_tailIndex = num1 + 1;
          }
          finally
          {
            if (lockTaken)
              this.m_foreignLock.Exit(false);
          }
        }
      }

      public bool LocalFindAndPop(IThreadPoolWorkItem obj)
      {
        if (this.m_array[this.m_tailIndex - 1 & this.m_mask] == obj)
        {
          IThreadPoolWorkItem threadPoolWorkItem;
          return this.LocalPop(out threadPoolWorkItem);
        }
        for (int index = this.m_tailIndex - 2; index >= this.m_headIndex; --index)
        {
          if (this.m_array[index & this.m_mask] == obj)
          {
            bool lockTaken = false;
            try
            {
              this.m_foreignLock.Enter(ref lockTaken);
              if (this.m_array[index & this.m_mask] == null)
                return false;
              Volatile.Write<IThreadPoolWorkItem>(ref this.m_array[index & this.m_mask], (IThreadPoolWorkItem) null);
              if (index == this.m_tailIndex)
                this.m_tailIndex = this.m_tailIndex - 1;
              else if (index == this.m_headIndex)
                this.m_headIndex = this.m_headIndex + 1;
              return true;
            }
            finally
            {
              if (lockTaken)
                this.m_foreignLock.Exit(false);
            }
          }
        }
        return false;
      }

      public bool LocalPop(out IThreadPoolWorkItem obj)
      {
label_0:
        int num1;
        int index1;
        do
        {
          int num2 = this.m_tailIndex;
          if (this.m_headIndex >= num2)
          {
            obj = (IThreadPoolWorkItem) null;
            return false;
          }
          num1 = num2 - 1;
          Interlocked.Exchange(ref this.m_tailIndex, num1);
          if (this.m_headIndex <= num1)
          {
            index1 = num1 & this.m_mask;
            obj = Volatile.Read<IThreadPoolWorkItem>(ref this.m_array[index1]);
          }
          else
            goto label_5;
        }
        while (obj == null);
        this.m_array[index1] = (IThreadPoolWorkItem) null;
        return true;
label_5:
        bool lockTaken = false;
        try
        {
          this.m_foreignLock.Enter(ref lockTaken);
          if (this.m_headIndex <= num1)
          {
            int index2 = num1 & this.m_mask;
            obj = Volatile.Read<IThreadPoolWorkItem>(ref this.m_array[index2]);
            if (obj != null)
            {
              this.m_array[index2] = (IThreadPoolWorkItem) null;
              return true;
            }
            goto label_0;
          }
          else
          {
            this.m_tailIndex = num1 + 1;
            obj = (IThreadPoolWorkItem) null;
            return false;
          }
        }
        finally
        {
          if (lockTaken)
            this.m_foreignLock.Exit(false);
        }
      }

      public bool TrySteal(out IThreadPoolWorkItem obj, ref bool missedSteal)
      {
        return this.TrySteal(out obj, ref missedSteal, 0);
      }

      private bool TrySteal(out IThreadPoolWorkItem obj, ref bool missedSteal, int millisecondsTimeout)
      {
        obj = (IThreadPoolWorkItem) null;
        while (this.m_headIndex < this.m_tailIndex)
        {
          bool lockTaken = false;
          try
          {
            this.m_foreignLock.TryEnter(millisecondsTimeout, ref lockTaken);
            if (lockTaken)
            {
              int num = this.m_headIndex;
              Interlocked.Exchange(ref this.m_headIndex, num + 1);
              if (num < this.m_tailIndex)
              {
                int index = num & this.m_mask;
                obj = Volatile.Read<IThreadPoolWorkItem>(ref this.m_array[index]);
                if (obj != null)
                {
                  this.m_array[index] = (IThreadPoolWorkItem) null;
                  return true;
                }
                continue;
              }
              this.m_headIndex = num;
              obj = (IThreadPoolWorkItem) null;
              missedSteal = true;
            }
            else
              missedSteal = true;
          }
          finally
          {
            if (lockTaken)
              this.m_foreignLock.Exit(false);
          }
          return false;
        }
        return false;
      }
    }

    internal class QueueSegment
    {
      internal readonly IThreadPoolWorkItem[] nodes;
      private const int QueueSegmentLength = 256;
      private volatile int indexes;
      public volatile ThreadPoolWorkQueue.QueueSegment Next;
      private const int SixteenBits = 65535;

      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
      public QueueSegment()
      {
        this.nodes = new IThreadPoolWorkItem[256];
      }

      private void GetIndexes(out int upper, out int lower)
      {
        int num = this.indexes;
        upper = num >> 16 & (int) ushort.MaxValue;
        lower = num & (int) ushort.MaxValue;
      }

      private bool CompareExchangeIndexes(ref int prevUpper, int newUpper, ref int prevLower, int newLower)
      {
        int comparand = prevUpper << 16 | prevLower & (int) ushort.MaxValue;
        int num = Interlocked.CompareExchange(ref this.indexes, newUpper << 16 | newLower & (int) ushort.MaxValue, comparand);
        prevUpper = num >> 16 & (int) ushort.MaxValue;
        prevLower = num & (int) ushort.MaxValue;
        return num == comparand;
      }

      public bool IsUsedUp()
      {
        int upper;
        int lower;
        this.GetIndexes(out upper, out lower);
        if (upper == this.nodes.Length)
          return lower == this.nodes.Length;
        return false;
      }

      public bool TryEnqueue(IThreadPoolWorkItem node)
      {
        int upper;
        int lower;
        this.GetIndexes(out upper, out lower);
        while (upper != this.nodes.Length)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          int& prevUpper = @upper;
          // ISSUE: explicit reference operation
          int newUpper = ^prevUpper + 1;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          int& prevLower = @lower;
          // ISSUE: explicit reference operation
          int newLower = ^prevLower;
          if (this.CompareExchangeIndexes(prevUpper, newUpper, prevLower, newLower))
          {
            Volatile.Write<IThreadPoolWorkItem>(ref this.nodes[upper], node);
            return true;
          }
        }
        return false;
      }

      public bool TryDequeue(out IThreadPoolWorkItem node)
      {
        int upper;
        int lower;
        this.GetIndexes(out upper, out lower);
        while (lower != upper)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          int& prevUpper = @upper;
          // ISSUE: explicit reference operation
          int newUpper = ^prevUpper;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          int& prevLower = @lower;
          // ISSUE: explicit reference operation
          int newLower = ^prevLower + 1;
          if (this.CompareExchangeIndexes(prevUpper, newUpper, prevLower, newLower))
          {
            SpinWait spinWait = new SpinWait();
            while ((node = Volatile.Read<IThreadPoolWorkItem>(ref this.nodes[lower])) == null)
              spinWait.SpinOnce();
            this.nodes[lower] = (IThreadPoolWorkItem) null;
            return true;
          }
        }
        node = (IThreadPoolWorkItem) null;
        return false;
      }
    }
  }
}
