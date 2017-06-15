// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.InternalTaskOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  [Flags]
  [Serializable]
  internal enum InternalTaskOptions
  {
    None = 0,
    InternalOptionsMask = 65280,
    ChildReplica = 256,
    ContinuationTask = 512,
    PromiseTask = 1024,
    SelfReplicating = 2048,
    LazyCancellation = 4096,
    QueuedByRuntime = 8192,
    DoNotDispose = 16384,
  }
}
