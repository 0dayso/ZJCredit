// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.ContinuationResultTaskFromTask`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  internal sealed class ContinuationResultTaskFromTask<TResult> : Task<TResult>
  {
    private Task m_antecedent;

    public ContinuationResultTaskFromTask(Task antecedent, Delegate function, object state, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, ref StackCrawlMark stackMark)
      : base(function, state, Task.InternalCurrentIfAttached(creationOptions), new CancellationToken(), creationOptions, internalOptions, (TaskScheduler) null)
    {
      this.m_antecedent = antecedent;
      this.PossiblyCaptureContext(ref stackMark);
    }

    internal override void InnerInvoke()
    {
      Task task = this.m_antecedent;
      this.m_antecedent = (Task) null;
      task.NotifyDebuggerOfWaitCompletionIfNecessary();
      Func<Task, TResult> func1 = this.m_action as Func<Task, TResult>;
      if (func1 != null)
      {
        this.m_result = func1(task);
      }
      else
      {
        Func<Task, object, TResult> func2 = this.m_action as Func<Task, object, TResult>;
        if (func2 == null)
          return;
        this.m_result = func2(task, this.m_stateObject);
      }
    }
  }
}
