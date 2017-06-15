// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.TaskContinuationOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  /// <summary>为通过使用 <see cref="M:System.Threading.Tasks.Task.ContinueWith(System.Action{System.Threading.Tasks.Task},System.Threading.CancellationToken,System.Threading.Tasks.TaskContinuationOptions,System.Threading.Tasks.TaskScheduler)" /> 或 <see cref="M:System.Threading.Tasks.Task`1.ContinueWith(System.Action{System.Threading.Tasks.Task{`0}},System.Threading.Tasks.TaskContinuationOptions)" /> 方法创建的任务指定行为。</summary>
  [Flags]
  [__DynamicallyInvokable]
  [Serializable]
  public enum TaskContinuationOptions
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] PreferFairness = 1,
    [__DynamicallyInvokable] LongRunning = 2,
    [__DynamicallyInvokable] AttachedToParent = 4,
    [__DynamicallyInvokable] DenyChildAttach = 8,
    [__DynamicallyInvokable] HideScheduler = 16,
    [__DynamicallyInvokable] LazyCancellation = 32,
    [__DynamicallyInvokable] RunContinuationsAsynchronously = 64,
    [__DynamicallyInvokable] NotOnRanToCompletion = 65536,
    [__DynamicallyInvokable] NotOnFaulted = 131072,
    [__DynamicallyInvokable] NotOnCanceled = 262144,
    [__DynamicallyInvokable] OnlyOnRanToCompletion = NotOnCanceled | NotOnFaulted,
    [__DynamicallyInvokable] OnlyOnFaulted = NotOnCanceled | NotOnRanToCompletion,
    [__DynamicallyInvokable] OnlyOnCanceled = NotOnFaulted | NotOnRanToCompletion,
    [__DynamicallyInvokable] ExecuteSynchronously = 524288,
  }
}
