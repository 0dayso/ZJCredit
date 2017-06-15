// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.FixupHolderList
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Serialization
{
  [Serializable]
  internal class FixupHolderList
  {
    internal const int InitialSize = 2;
    internal FixupHolder[] m_values;
    internal int m_count;

    internal FixupHolderList()
      : this(2)
    {
    }

    internal FixupHolderList(int startingSize)
    {
      this.m_count = 0;
      this.m_values = new FixupHolder[startingSize];
    }

    internal virtual void Add(long id, object fixupInfo)
    {
      if (this.m_count == this.m_values.Length)
        this.EnlargeArray();
      this.m_values[this.m_count].m_id = id;
      FixupHolder[] fixupHolderArray = this.m_values;
      int num = this.m_count;
      this.m_count = num + 1;
      int index = num;
      fixupHolderArray[index].m_fixupInfo = fixupInfo;
    }

    internal virtual void Add(FixupHolder fixup)
    {
      if (this.m_count == this.m_values.Length)
        this.EnlargeArray();
      FixupHolder[] fixupHolderArray = this.m_values;
      int num = this.m_count;
      this.m_count = num + 1;
      int index = num;
      FixupHolder fixupHolder = fixup;
      fixupHolderArray[index] = fixupHolder;
    }

    private void EnlargeArray()
    {
      int length = this.m_values.Length * 2;
      if (length < 0)
      {
        if (length == int.MaxValue)
          throw new SerializationException(Environment.GetResourceString("Serialization_TooManyElements"));
        length = int.MaxValue;
      }
      FixupHolder[] fixupHolderArray = new FixupHolder[length];
      Array.Copy((Array) this.m_values, (Array) fixupHolderArray, this.m_count);
      this.m_values = fixupHolderArray;
    }
  }
}
