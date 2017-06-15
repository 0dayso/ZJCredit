// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.RangeWorker
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  internal struct RangeWorker
  {
    internal readonly IndexRange[] m_indexRanges;
    internal int m_nCurrentIndexRange;
    internal long m_nStep;
    internal long m_nIncrementValue;
    internal readonly long m_nMaxIncrementValue;

    internal RangeWorker(IndexRange[] ranges, int nInitialRange, long nStep)
    {
      this.m_indexRanges = ranges;
      this.m_nCurrentIndexRange = nInitialRange;
      this.m_nStep = nStep;
      this.m_nIncrementValue = nStep;
      this.m_nMaxIncrementValue = 16L * nStep;
    }

    internal bool FindNewWork(out long nFromInclusiveLocal, out long nToExclusiveLocal)
    {
      int length = this.m_indexRanges.Length;
      do
      {
        IndexRange indexRange = this.m_indexRanges[this.m_nCurrentIndexRange];
        if (indexRange.m_bRangeFinished == 0)
        {
          if (this.m_indexRanges[this.m_nCurrentIndexRange].m_nSharedCurrentIndexOffset == null)
            Interlocked.CompareExchange<Shared<long>>(ref this.m_indexRanges[this.m_nCurrentIndexRange].m_nSharedCurrentIndexOffset, new Shared<long>(0L), (Shared<long>) null);
          long num = Interlocked.Add(ref this.m_indexRanges[this.m_nCurrentIndexRange].m_nSharedCurrentIndexOffset.Value, this.m_nIncrementValue) - this.m_nIncrementValue;
          if (indexRange.m_nToExclusive - indexRange.m_nFromInclusive > num)
          {
            nFromInclusiveLocal = indexRange.m_nFromInclusive + num;
            nToExclusiveLocal = nFromInclusiveLocal + this.m_nIncrementValue;
            if (nToExclusiveLocal > indexRange.m_nToExclusive || nToExclusiveLocal < indexRange.m_nFromInclusive)
              nToExclusiveLocal = indexRange.m_nToExclusive;
            if (this.m_nIncrementValue < this.m_nMaxIncrementValue)
            {
              this.m_nIncrementValue = this.m_nIncrementValue * 2L;
              if (this.m_nIncrementValue > this.m_nMaxIncrementValue)
                this.m_nIncrementValue = this.m_nMaxIncrementValue;
            }
            return true;
          }
          Interlocked.Exchange(ref this.m_indexRanges[this.m_nCurrentIndexRange].m_bRangeFinished, 1);
        }
        this.m_nCurrentIndexRange = (this.m_nCurrentIndexRange + 1) % this.m_indexRanges.Length;
        --length;
      }
      while (length > 0);
      nFromInclusiveLocal = 0L;
      nToExclusiveLocal = 0L;
      return false;
    }

    internal bool FindNewWork32(out int nFromInclusiveLocal32, out int nToExclusiveLocal32)
    {
      long nFromInclusiveLocal;
      long nToExclusiveLocal;
      int num = this.FindNewWork(out nFromInclusiveLocal, out nToExclusiveLocal) ? 1 : 0;
      nFromInclusiveLocal32 = (int) nFromInclusiveLocal;
      nToExclusiveLocal32 = (int) nToExclusiveLocal;
      return num != 0;
    }
  }
}
