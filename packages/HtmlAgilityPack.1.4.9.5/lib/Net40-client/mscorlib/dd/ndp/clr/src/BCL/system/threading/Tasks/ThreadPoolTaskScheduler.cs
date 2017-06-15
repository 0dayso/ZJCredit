// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.ThreadPoolTaskScheduler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Security;

namespace System.Threading.Tasks
{
  internal sealed class ThreadPoolTaskScheduler : TaskScheduler
  {
    private static readonly ParameterizedThreadStart s_longRunningThreadWork = new ParameterizedThreadStart(ThreadPoolTaskScheduler.LongRunningThreadWork);

    internal override bool RequiresAtomicStartTransition
    {
      get
      {
        return false;
      }
    }

    internal ThreadPoolTaskScheduler()
    {
    }

    private static void LongRunningThreadWork(object obj)
    {
      (obj as Task).ExecuteEntry(false);
    }

    [SecurityCritical]
    protected internal override void QueueTask(Task task)
    {
      if ((task.Options & TaskCreationOptions.LongRunning) != TaskCreationOptions.None)
      {
        Thread thread = new Thread(ThreadPoolTaskScheduler.s_longRunningThreadWork);
        thread.IsBackground = true;
        Task task1 = task;
        thread.Start((object) task1);
      }
      else
      {
        bool forceGlobal = (uint) (task.Options & TaskCreationOptions.PreferFairness) > 0U;
        ThreadPool.UnsafeQueueCustomWorkItem((IThreadPoolWorkItem) task, forceGlobal);
      }
    }

    [SecurityCritical]
    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
    {
      if (taskWasPreviouslyQueued && !ThreadPool.TryPopCustomWorkItem((IThreadPoolWorkItem) task))
        return false;
      try
      {
        return task.ExecuteEntry(false);
      }
      finally
      {
        if (taskWasPreviouslyQueued)
          this.NotifyWorkItemProgress();
      }
    }

    [SecurityCritical]
    protected internal override bool TryDequeue(Task task)
    {
      return ThreadPool.TryPopCustomWorkItem((IThreadPoolWorkItem) task);
    }

    [SecurityCritical]
    protected override IEnumerable<Task> GetScheduledTasks()
    {
      return this.FilterTasksFromWorkItems(ThreadPool.GetQueuedWorkItems());
    }

    private IEnumerable<Task> FilterTasksFromWorkItems(IEnumerable<IThreadPoolWorkItem> tpwItems)
    {
      foreach (IThreadPoolWorkItem tpwItem in tpwItems)
      {
        if (tpwItem is Task)
          yield return (Task) tpwItem;
      }
    }

    internal override void NotifyWorkItemProgress()
    {
      ThreadPool.NotifyWorkItemProgress();
    }
  }
}
