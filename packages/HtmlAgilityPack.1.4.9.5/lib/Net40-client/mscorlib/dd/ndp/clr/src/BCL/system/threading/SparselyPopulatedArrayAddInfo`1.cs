// Decompiled with JetBrains decompiler
// Type: System.Threading.SparselyPopulatedArrayAddInfo`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading
{
  internal struct SparselyPopulatedArrayAddInfo<T> where T : class
  {
    private SparselyPopulatedArrayFragment<T> m_source;
    private int m_index;

    internal SparselyPopulatedArrayFragment<T> Source
    {
      get
      {
        return this.m_source;
      }
    }

    internal int Index
    {
      get
      {
        return this.m_index;
      }
    }

    internal SparselyPopulatedArrayAddInfo(SparselyPopulatedArrayFragment<T> source, int index)
    {
      this.m_source = source;
      this.m_index = index;
    }
  }
}
