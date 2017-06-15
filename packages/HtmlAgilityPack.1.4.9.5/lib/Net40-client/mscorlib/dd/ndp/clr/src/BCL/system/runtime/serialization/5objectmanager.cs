// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.ObjectHolderList
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Serialization
{
  internal class ObjectHolderList
  {
    internal const int DefaultInitialSize = 8;
    internal ObjectHolder[] m_values;
    internal int m_count;

    internal int Version
    {
      get
      {
        return this.m_count;
      }
    }

    internal int Count
    {
      get
      {
        return this.m_count;
      }
    }

    internal ObjectHolderList()
      : this(8)
    {
    }

    internal ObjectHolderList(int startingSize)
    {
      this.m_count = 0;
      this.m_values = new ObjectHolder[startingSize];
    }

    internal virtual void Add(ObjectHolder value)
    {
      if (this.m_count == this.m_values.Length)
        this.EnlargeArray();
      ObjectHolder[] objectHolderArray = this.m_values;
      int num = this.m_count;
      this.m_count = num + 1;
      int index = num;
      ObjectHolder objectHolder = value;
      objectHolderArray[index] = objectHolder;
    }

    internal ObjectHolderListEnumerator GetFixupEnumerator()
    {
      return new ObjectHolderListEnumerator(this, true);
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
      ObjectHolder[] objectHolderArray = new ObjectHolder[length];
      Array.Copy((Array) this.m_values, (Array) objectHolderArray, this.m_count);
      this.m_values = objectHolderArray;
    }
  }
}
