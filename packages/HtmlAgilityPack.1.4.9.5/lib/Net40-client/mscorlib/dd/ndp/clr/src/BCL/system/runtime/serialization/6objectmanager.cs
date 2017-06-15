// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.ObjectHolderListEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Serialization
{
  internal class ObjectHolderListEnumerator
  {
    private bool m_isFixupEnumerator;
    private ObjectHolderList m_list;
    private int m_startingVersion;
    private int m_currPos;

    internal ObjectHolder Current
    {
      get
      {
        return this.m_list.m_values[this.m_currPos];
      }
    }

    internal ObjectHolderListEnumerator(ObjectHolderList list, bool isFixupEnumerator)
    {
      this.m_list = list;
      this.m_startingVersion = this.m_list.Version;
      this.m_currPos = -1;
      this.m_isFixupEnumerator = isFixupEnumerator;
    }

    internal bool MoveNext()
    {
      if (this.m_isFixupEnumerator)
      {
        int num;
        do
        {
          num = this.m_currPos + 1;
          this.m_currPos = num;
        }
        while (num < this.m_list.Count && this.m_list.m_values[this.m_currPos].CompletelyFixed);
        return this.m_currPos != this.m_list.Count;
      }
      this.m_currPos = this.m_currPos + 1;
      return this.m_currPos != this.m_list.Count;
    }
  }
}
