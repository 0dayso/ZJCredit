// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.TaskStatus
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  /// <summary>表示 <see cref="T:System.Threading.Tasks.Task" /> 的生命周期中的当前阶段。</summary>
  [__DynamicallyInvokable]
  public enum TaskStatus
  {
    [__DynamicallyInvokable] Created,
    [__DynamicallyInvokable] WaitingForActivation,
    [__DynamicallyInvokable] WaitingToRun,
    [__DynamicallyInvokable] Running,
    [__DynamicallyInvokable] WaitingForChildrenToComplete,
    [__DynamicallyInvokable] RanToCompletion,
    [__DynamicallyInvokable] Canceled,
    [__DynamicallyInvokable] Faulted,
  }
}
