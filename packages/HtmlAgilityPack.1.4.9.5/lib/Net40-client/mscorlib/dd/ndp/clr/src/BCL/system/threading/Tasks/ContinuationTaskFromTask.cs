// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.ContinuationTaskFromTask
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  internal sealed class ContinuationTaskFromTask : Task
  {
    private Task m_antecedent;

    public ContinuationTaskFromTask(Task antecedent, Delegate action, object state, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, ref StackCrawlMark stackMark)
      : base(action, state, Task.InternalCurrentIfAttached(creationOptions), new CancellationToken(), creationOptions, internalOptions, (TaskScheduler) null)
    {
      this.m_antecedent = antecedent;
      this.PossiblyCaptureContext(ref stackMark);
    }

    internal override void InnerInvoke()
    {
      Task task = this.m_antecedent;
      this.m_antecedent = (Task) null;
      task.NotifyDebuggerOfWaitCompletionIfNecessary();
      Action<Task> action1 = this.m_action as Action<Task>;
      if (action1 != null)
      {
        action1(task);
      }
      else
      {
        Action<Task, object> action2 = this.m_action as Action<Task, object>;
        if (action2 == null)
          return;
        action2(task, this.m_stateObject);
      }
    }
  }
}
