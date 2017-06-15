// Decompiled with JetBrains decompiler
// Type: System.Collections.Concurrent.SystemCollectionsConcurrent_ProducerConsumerCollectionDebugView`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;

namespace System.Collections.Concurrent
{
  internal sealed class SystemCollectionsConcurrent_ProducerConsumerCollectionDebugView<T>
  {
    private IProducerConsumerCollection<T> m_collection;

    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public T[] Items
    {
      get
      {
        return this.m_collection.ToArray();
      }
    }

    public SystemCollectionsConcurrent_ProducerConsumerCollectionDebugView(IProducerConsumerCollection<T> collection)
    {
      if (collection == null)
        throw new ArgumentNullException("collection");
      this.m_collection = collection;
    }
  }
}
