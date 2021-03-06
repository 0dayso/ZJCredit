﻿// Decompiled with JetBrains decompiler
// Type: System.Threading.QueueUserWorkItemCallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Threading
{
  internal sealed class QueueUserWorkItemCallback : IThreadPoolWorkItem
  {
    [SecurityCritical]
    internal static ContextCallback ccb = new ContextCallback(QueueUserWorkItemCallback.WaitCallback_Context);
    private WaitCallback callback;
    private ExecutionContext context;
    private object state;

    [SecuritySafeCritical]
    static QueueUserWorkItemCallback()
    {
    }

    [SecurityCritical]
    internal QueueUserWorkItemCallback(WaitCallback waitCallback, object stateObj, bool compressStack, ref StackCrawlMark stackMark)
    {
      this.callback = waitCallback;
      this.state = stateObj;
      if (!compressStack || ExecutionContext.IsFlowSuppressed())
        return;
      this.context = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
    }

    internal QueueUserWorkItemCallback(WaitCallback waitCallback, object stateObj, ExecutionContext ec)
    {
      this.callback = waitCallback;
      this.state = stateObj;
      this.context = ec;
    }

    [SecurityCritical]
    void IThreadPoolWorkItem.ExecuteWorkItem()
    {
      if (this.context == null)
      {
        WaitCallback waitCallback = this.callback;
        this.callback = (WaitCallback) null;
        object state = this.state;
        waitCallback(state);
      }
      else
        ExecutionContext.Run(this.context, QueueUserWorkItemCallback.ccb, (object) this, true);
    }

    [SecurityCritical]
    void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
    {
    }

    [SecurityCritical]
    private static void WaitCallback_Context(object state)
    {
      QueueUserWorkItemCallback workItemCallback = (QueueUserWorkItemCallback) state;
      workItemCallback.callback(workItemCallback.state);
    }
  }
}
