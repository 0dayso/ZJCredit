// Decompiled with JetBrains decompiler
// Type: System.System_LazyDebugView`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Threading;

namespace System
{
  internal sealed class System_LazyDebugView<T>
  {
    private readonly Lazy<T> m_lazy;

    public bool IsValueCreated
    {
      get
      {
        return this.m_lazy.IsValueCreated;
      }
    }

    public T Value
    {
      get
      {
        return this.m_lazy.ValueForDebugDisplay;
      }
    }

    public LazyThreadSafetyMode Mode
    {
      get
      {
        return this.m_lazy.Mode;
      }
    }

    public bool IsValueFaulted
    {
      get
      {
        return this.m_lazy.IsValueFaulted;
      }
    }

    public System_LazyDebugView(Lazy<T> lazy)
    {
      this.m_lazy = lazy;
    }
  }
}
