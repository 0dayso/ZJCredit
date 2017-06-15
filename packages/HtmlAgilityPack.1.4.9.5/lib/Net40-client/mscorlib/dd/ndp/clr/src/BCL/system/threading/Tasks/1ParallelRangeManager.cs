// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.RangeManager
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  internal class RangeManager
  {
    internal readonly IndexRange[] m_indexRanges;
    internal int m_nCurrentIndexRangeToAssign;
    internal long m_nStep;

    internal RangeManager(long nFromInclusive, long nToExclusive, long nStep, int nNumExpectedWorkers)
    {
      this.m_nCurrentIndexRangeToAssign = 0;
      this.m_nStep = nStep;
      if (nNumExpectedWorkers == 1)
        nNumExpectedWorkers = 2;
      long num1 = nToExclusive - nFromInclusive;
      long num2 = (long) nNumExpectedWorkers;
      long num3 = (long) ((ulong) num1 / (ulong) num2);
      long num4 = nStep;
      long num5 = (long) ((ulong) num3 % (ulong) num4);
      ulong num6 = (ulong) (num3 - num5);
      if ((long) num6 == 0L)
        num6 = (ulong) nStep;
      long num7 = (long) num6;
      int length = (int) ((ulong) num1 / (ulong) num7);
      long num8 = (long) num6;
      if ((long) ((ulong) num1 % (ulong) num8) != 0L)
        ++length;
      long num9 = (long) num6;
      this.m_indexRanges = new IndexRange[length];
      long num10 = nFromInclusive;
      for (int index = 0; index < length; ++index)
      {
        this.m_indexRanges[index].m_nFromInclusive = num10;
        this.m_indexRanges[index].m_nSharedCurrentIndexOffset = (Shared<long>) null;
        this.m_indexRanges[index].m_bRangeFinished = 0;
        num10 += num9;
        long num11 = num10;
        long num12 = num9;
        long num13 = num11 - num12;
        if (num11 < num13 || num10 > nToExclusive)
          num10 = nToExclusive;
        this.m_indexRanges[index].m_nToExclusive = num10;
      }
    }

    internal RangeWorker RegisterNewWorker()
    {
      return new RangeWorker(this.m_indexRanges, (Interlocked.Increment(ref this.m_nCurrentIndexRangeToAssign) - 1) % this.m_indexRanges.Length, this.m_nStep);
    }
  }
}
