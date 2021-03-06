﻿// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.ParallelLoopState32
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  internal class ParallelLoopState32 : ParallelLoopState
  {
    private ParallelLoopStateFlags32 m_sharedParallelStateFlags;
    private int m_currentIteration;

    internal int CurrentIteration
    {
      get
      {
        return this.m_currentIteration;
      }
      set
      {
        this.m_currentIteration = value;
      }
    }

    internal override bool InternalShouldExitCurrentIteration
    {
      get
      {
        return this.m_sharedParallelStateFlags.ShouldExitLoop(this.CurrentIteration);
      }
    }

    internal override long? InternalLowestBreakIteration
    {
      get
      {
        return this.m_sharedParallelStateFlags.NullableLowestBreakIteration;
      }
    }

    internal ParallelLoopState32(ParallelLoopStateFlags32 sharedParallelStateFlags)
      : base((ParallelLoopStateFlags) sharedParallelStateFlags)
    {
      this.m_sharedParallelStateFlags = sharedParallelStateFlags;
    }

    internal override void InternalBreak()
    {
      ParallelLoopState.Break(this.CurrentIteration, this.m_sharedParallelStateFlags);
    }
  }
}
