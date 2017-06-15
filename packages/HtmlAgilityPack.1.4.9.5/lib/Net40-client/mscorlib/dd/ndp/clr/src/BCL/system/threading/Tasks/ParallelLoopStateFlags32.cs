// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.ParallelLoopStateFlags32
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  internal class ParallelLoopStateFlags32 : ParallelLoopStateFlags
  {
    internal volatile int m_lowestBreakIteration = int.MaxValue;

    internal int LowestBreakIteration
    {
      get
      {
        return this.m_lowestBreakIteration;
      }
    }

    internal long? NullableLowestBreakIteration
    {
      get
      {
        if (this.m_lowestBreakIteration == int.MaxValue)
          return new long?();
        long location = (long) this.m_lowestBreakIteration;
        if (IntPtr.Size >= 8)
          return new long?(location);
        return new long?(Interlocked.Read(ref location));
      }
    }

    internal bool ShouldExitLoop(int CallerIteration)
    {
      int loopStateFlags = this.LoopStateFlags;
      if (loopStateFlags == ParallelLoopStateFlags.PLS_NONE)
        return false;
      if ((loopStateFlags & (ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_STOPPED | ParallelLoopStateFlags.PLS_CANCELED)) != 0)
        return true;
      if ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) != 0)
        return CallerIteration > this.LowestBreakIteration;
      return false;
    }

    internal bool ShouldExitLoop()
    {
      int loopStateFlags = this.LoopStateFlags;
      if (loopStateFlags != ParallelLoopStateFlags.PLS_NONE)
        return (uint) (loopStateFlags & (ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_CANCELED)) > 0U;
      return false;
    }
  }
}
