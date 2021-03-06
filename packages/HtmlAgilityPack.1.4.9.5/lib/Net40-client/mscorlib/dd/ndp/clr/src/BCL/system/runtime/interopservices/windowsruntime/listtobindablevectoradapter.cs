﻿// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.ListToBindableVectorAdapter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal sealed class ListToBindableVectorAdapter
  {
    private ListToBindableVectorAdapter()
    {
    }

    [SecurityCritical]
    internal object GetAt(uint index)
    {
      IList list = JitHelpers.UnsafeCast<IList>((object) this);
      ListToBindableVectorAdapter.EnsureIndexInt32(index, list.Count);
      try
      {
        return list[(int) index];
      }
      catch (ArgumentOutOfRangeException ex)
      {
        throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483637, (Exception) ex, "ArgumentOutOfRange_IndexOutOfRange");
      }
    }

    [SecurityCritical]
    internal uint Size()
    {
      return (uint) JitHelpers.UnsafeCast<IList>((object) this).Count;
    }

    [SecurityCritical]
    internal IBindableVectorView GetView()
    {
      return (IBindableVectorView) new ListToBindableVectorViewAdapter(JitHelpers.UnsafeCast<IList>((object) this));
    }

    [SecurityCritical]
    internal bool IndexOf(object value, out uint index)
    {
      int num = JitHelpers.UnsafeCast<IList>((object) this).IndexOf(value);
      if (-1 == num)
      {
        index = 0U;
        return false;
      }
      index = (uint) num;
      return true;
    }

    [SecurityCritical]
    internal void SetAt(uint index, object value)
    {
      IList list = JitHelpers.UnsafeCast<IList>((object) this);
      ListToBindableVectorAdapter.EnsureIndexInt32(index, list.Count);
      try
      {
        list[(int) index] = value;
      }
      catch (ArgumentOutOfRangeException ex)
      {
        throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483637, (Exception) ex, "ArgumentOutOfRange_IndexOutOfRange");
      }
    }

    [SecurityCritical]
    internal void InsertAt(uint index, object value)
    {
      IList list = JitHelpers.UnsafeCast<IList>((object) this);
      ListToBindableVectorAdapter.EnsureIndexInt32(index, list.Count + 1);
      try
      {
        list.Insert((int) index, value);
      }
      catch (ArgumentOutOfRangeException ex)
      {
        int hr = -2147483637;
        ex.SetErrorCode(hr);
        throw;
      }
    }

    [SecurityCritical]
    internal void RemoveAt(uint index)
    {
      IList list = JitHelpers.UnsafeCast<IList>((object) this);
      ListToBindableVectorAdapter.EnsureIndexInt32(index, list.Count);
      try
      {
        list.RemoveAt((int) index);
      }
      catch (ArgumentOutOfRangeException ex)
      {
        int hr = -2147483637;
        ex.SetErrorCode(hr);
        throw;
      }
    }

    [SecurityCritical]
    internal void Append(object value)
    {
      JitHelpers.UnsafeCast<IList>((object) this).Add(value);
    }

    [SecurityCritical]
    internal void RemoveAtEnd()
    {
      IList list = JitHelpers.UnsafeCast<IList>((object) this);
      if (list.Count == 0)
      {
        InvalidOperationException operationException = new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotRemoveLastFromEmptyCollection"));
        int hr = -2147483637;
        operationException.SetErrorCode(hr);
        throw operationException;
      }
      this.RemoveAt((uint) list.Count - 1U);
    }

    [SecurityCritical]
    internal void Clear()
    {
      JitHelpers.UnsafeCast<IList>((object) this).Clear();
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
  }
}
