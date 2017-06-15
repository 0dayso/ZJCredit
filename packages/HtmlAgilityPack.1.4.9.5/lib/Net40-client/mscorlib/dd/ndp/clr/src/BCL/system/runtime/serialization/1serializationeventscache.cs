// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.SerializationEventsCache
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;

namespace System.Runtime.Serialization
{
  internal static class SerializationEventsCache
  {
    private static Hashtable cache = new Hashtable();

    internal static SerializationEvents GetSerializationEventsForType(Type t)
    {
      SerializationEvents serializationEvents;
      if ((serializationEvents = (SerializationEvents) SerializationEventsCache.cache[(object) t]) == null)
      {
        lock (SerializationEventsCache.cache.SyncRoot)
        {
          if ((serializationEvents = (SerializationEvents) SerializationEventsCache.cache[(object) t]) == null)
          {
            serializationEvents = new SerializationEvents(t);
            SerializationEventsCache.cache[(object) t] = (object) serializationEvents;
          }
        }
      }
      return serializationEvents;
    }
  }
}
