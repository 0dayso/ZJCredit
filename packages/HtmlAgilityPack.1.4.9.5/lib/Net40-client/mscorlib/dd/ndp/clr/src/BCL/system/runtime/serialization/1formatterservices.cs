﻿// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.SurrogateForCyclicalReference
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.Serialization
{
  internal sealed class SurrogateForCyclicalReference : ISerializationSurrogate
  {
    private ISerializationSurrogate innerSurrogate;

    internal SurrogateForCyclicalReference(ISerializationSurrogate innerSurrogate)
    {
      if (innerSurrogate == null)
        throw new ArgumentNullException("innerSurrogate");
      this.innerSurrogate = innerSurrogate;
    }

    [SecurityCritical]
    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
    {
      this.innerSurrogate.GetObjectData(obj, info, context);
    }

    [SecurityCritical]
    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
    {
      return this.innerSurrogate.SetObjectData(obj, info, context, selector);
    }
  }
}
