// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.MultiProducerMultiConsumerQueue`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Threading.Tasks
{
  [DebuggerDisplay("Count = {Count}")]
  internal sealed class MultiProducerMultiConsumerQueue<T> : ConcurrentQueue<T>, IProducerConsumerQueue<T>, IEnumerable<T>, IEnumerable
  {
    bool IProducerConsumerQueue<T>.IsEmpty
    {
      get
      {
        return this.IsEmpty;
      }
    }

    int IProducerConsumerQueue<T>.Count
    {
      get
      {
        return this.Count;
      }
    }

    void IProducerConsumerQueue<T>.Enqueue(T item)
    {
      this.Enqueue(item);
    }

    bool IProducerConsumerQueue<T>.TryDequeue(out T result)
    {
      return this.TryDequeue(out result);
    }

    int IProducerConsumerQueue<T>.GetCountSafe(object syncObj)
    {
      return this.Count;
    }
  }
}
