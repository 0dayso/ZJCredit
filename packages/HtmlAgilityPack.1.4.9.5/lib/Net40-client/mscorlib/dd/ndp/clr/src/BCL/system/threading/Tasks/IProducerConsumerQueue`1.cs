// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.IProducerConsumerQueue`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;

namespace System.Threading.Tasks
{
  internal interface IProducerConsumerQueue<T> : IEnumerable<T>, IEnumerable
  {
    bool IsEmpty { get; }

    int Count { get; }

    void Enqueue(T item);

    bool TryDequeue(out T result);

    int GetCountSafe(object syncObj);
  }
}
