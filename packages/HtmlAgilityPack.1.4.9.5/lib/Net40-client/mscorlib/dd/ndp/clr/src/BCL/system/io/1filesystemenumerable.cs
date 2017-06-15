// Decompiled with JetBrains decompiler
// Type: System.IO.Iterator`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace System.IO
{
  internal abstract class Iterator<TSource> : IEnumerable<TSource>, IEnumerable, IEnumerator<TSource>, IDisposable, IEnumerator
  {
    private int threadId;
    internal int state;
    internal TSource current;

    public TSource Current
    {
      get
      {
        return this.current;
      }
    }

    object IEnumerator.Current
    {
      get
      {
        return (object) this.Current;
      }
    }

    public Iterator()
    {
      this.threadId = Thread.CurrentThread.ManagedThreadId;
    }

    protected abstract Iterator<TSource> Clone();

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      this.current = default (TSource);
      this.state = -1;
    }

    public IEnumerator<TSource> GetEnumerator()
    {
      if (this.threadId == Thread.CurrentThread.ManagedThreadId && this.state == 0)
      {
        this.state = 1;
        return (IEnumerator<TSource>) this;
      }
      Iterator<TSource> iterator = this.Clone();
      int num = 1;
      iterator.state = num;
      return (IEnumerator<TSource>) iterator;
    }

    public abstract bool MoveNext();

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    void IEnumerator.Reset()
    {
      throw new NotSupportedException();
    }
  }
}
