// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.ListToBindableVectorViewAdapter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal sealed class ListToBindableVectorViewAdapter : IBindableVectorView, IBindableIterable
  {
    private readonly IList list;

    public uint Size
    {
      get
      {
        return (uint) this.list.Count;
      }
    }

    internal ListToBindableVectorViewAdapter(IList list)
    {
      if (list == null)
        throw new ArgumentNullException("list");
      this.list = list;
    }

    private static void EnsureIndexInt32(uint index, int listCapacity)
    {
      if ((uint) int.MaxValue <= index || index >= (uint) listCapacity)
      {
        ArgumentOutOfRangeException ofRangeException = new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_IndexLargerThanMaxValue"));
        int hr = -2147483637;
        ofRangeException.SetErrorCode(hr);
        throw ofRangeException;
      }
    }

    public IBindableIterator First()
    {
      return (IBindableIterator) new EnumeratorToIteratorAdapter<object>((IEnumerator<object>) new EnumerableToBindableIterableAdapter.NonGenericToGenericEnumerator(this.list.GetEnumerator()));
    }

    public object GetAt(uint index)
    {
      ListToBindableVectorViewAdapter.EnsureIndexInt32(index, this.list.Count);
      try
      {
        return this.list[(int) index];
      }
      catch (ArgumentOutOfRangeException ex)
      {
        throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483637, (Exception) ex, "ArgumentOutOfRange_IndexOutOfRange");
      }
    }

    public bool IndexOf(object value, out uint index)
    {
      int num = this.list.IndexOf(value);
      if (-1 == num)
      {
        index = 0U;
        return false;
      }
      index = (uint) num;
      return true;
    }
  }
}
