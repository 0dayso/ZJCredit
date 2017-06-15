// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.BindableIterableToEnumerableAdapter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal sealed class BindableIterableToEnumerableAdapter
  {
    private BindableIterableToEnumerableAdapter()
    {
    }

    [SecurityCritical]
    internal IEnumerator GetEnumerator_Stub()
    {
      return (IEnumerator) new IteratorToEnumeratorAdapter<object>((IIterator<object>) new BindableIterableToEnumerableAdapter.NonGenericToGenericIterator(JitHelpers.UnsafeCast<IBindableIterable>((object) this).First()));
    }

    private sealed class NonGenericToGenericIterator : IIterator<object>
    {
      private IBindableIterator iterator;

      public object Current
      {
        get
        {
          return this.iterator.Current;
        }
      }

      public bool HasCurrent
      {
        get
        {
          return this.iterator.HasCurrent;
        }
      }

      public NonGenericToGenericIterator(IBindableIterator iterator)
      {
        this.iterator = iterator;
      }

      public bool MoveNext()
      {
        return this.iterator.MoveNext();
      }

      public int GetMany(object[] items)
      {
        throw new NotSupportedException();
      }
    }
  }
}
