// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.SurrogateHashtable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;

namespace System.Runtime.Serialization
{
  internal class SurrogateHashtable : Hashtable
  {
    internal SurrogateHashtable(int size)
      : base(size)
    {
    }

    protected override bool KeyEquals(object key, object item)
    {
      SurrogateKey surrogateKey1 = (SurrogateKey) item;
      SurrogateKey surrogateKey2 = (SurrogateKey) key;
      if (surrogateKey2.m_type == surrogateKey1.m_type && (surrogateKey2.m_context.m_state & surrogateKey1.m_context.m_state) == surrogateKey1.m_context.m_state)
        return surrogateKey2.m_context.Context == surrogateKey1.m_context.Context;
      return false;
    }
  }
}
