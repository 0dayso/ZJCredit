﻿// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.ParallelForReplicaTask
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  internal class ParallelForReplicaTask : Task
  {
    internal object m_stateForNextReplica;
    internal object m_stateFromPreviousReplica;
    internal Task m_handedOverChildReplica;

    internal override object SavedStateForNextReplica
    {
      get
      {
        return this.m_stateForNextReplica;
      }
      set
      {
        this.m_stateForNextReplica = value;
      }
    }

    internal override object SavedStateFromPreviousReplica
    {
      get
      {
        return this.m_stateFromPreviousReplica;
      }
      set
      {
        this.m_stateFromPreviousReplica = value;
      }
    }

    internal override Task HandedOverChildReplica
    {
      get
      {
        return this.m_handedOverChildReplica;
      }
      set
      {
        this.m_handedOverChildReplica = value;
      }
    }

    internal ParallelForReplicaTask(Action<object> taskReplicaDelegate, object stateObject, Task parentTask, TaskScheduler taskScheduler, TaskCreationOptions creationOptionsForReplica, InternalTaskOptions internalOptionsForReplica)
      : base((Delegate) taskReplicaDelegate, stateObject, parentTask, new CancellationToken(), creationOptionsForReplica, internalOptionsForReplica, taskScheduler)
    {
    }
  }
}
