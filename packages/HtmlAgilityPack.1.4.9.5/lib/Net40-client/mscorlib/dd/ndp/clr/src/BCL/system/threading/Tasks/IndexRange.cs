// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.IndexRange
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading.Tasks
{
  internal struct IndexRange
  {
    internal long m_nFromInclusive;
    internal long m_nToExclusive;
    internal volatile Shared<long> m_nSharedCurrentIndexOffset;
    internal int m_bRangeFinished;
  }
}
