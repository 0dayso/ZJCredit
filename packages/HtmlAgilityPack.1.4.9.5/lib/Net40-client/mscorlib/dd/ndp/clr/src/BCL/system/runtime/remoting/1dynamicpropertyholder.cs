// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.ArrayWithSize
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Remoting.Contexts
{
  internal class ArrayWithSize
  {
    internal IDynamicMessageSink[] Sinks;
    internal int Count;

    internal ArrayWithSize(IDynamicMessageSink[] sinks, int count)
    {
      this.Sinks = sinks;
      this.Count = count;
    }
  }
}
