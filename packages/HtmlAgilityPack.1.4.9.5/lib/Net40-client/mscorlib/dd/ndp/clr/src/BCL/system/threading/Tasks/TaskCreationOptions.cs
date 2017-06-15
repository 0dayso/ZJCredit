// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.TaskCreationOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  /// <summary>指定用于控制任务的创建和执行的可选行为的标志。</summary>
  [Flags]
  [__DynamicallyInvokable]
  [Serializable]
  public enum TaskCreationOptions
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] PreferFairness = 1,
    [__DynamicallyInvokable] LongRunning = 2,
    [__DynamicallyInvokable] AttachedToParent = 4,
    [__DynamicallyInvokable] DenyChildAttach = 8,
    [__DynamicallyInvokable] HideScheduler = 16,
    [__DynamicallyInvokable] RunContinuationsAsynchronously = 64,
  }
}
