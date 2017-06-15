// Decompiled with JetBrains decompiler
// Type: System.Threading.SystemThreading_ThreadLocalDebugView`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;

namespace System.Threading
{
  internal sealed class SystemThreading_ThreadLocalDebugView<T>
  {
    private readonly ThreadLocal<T> m_tlocal;

    public bool IsValueCreated
    {
      get
      {
        return this.m_tlocal.IsValueCreated;
      }
    }

    public T Value
    {
      get
      {
        return this.m_tlocal.ValueForDebugDisplay;
      }
    }

    public List<T> Values
    {
      get
      {
        return this.m_tlocal.ValuesForDebugDisplay;
      }
    }

    public SystemThreading_ThreadLocalDebugView(ThreadLocal<T> tlocal)
    {
      this.m_tlocal = tlocal;
    }
  }
}
