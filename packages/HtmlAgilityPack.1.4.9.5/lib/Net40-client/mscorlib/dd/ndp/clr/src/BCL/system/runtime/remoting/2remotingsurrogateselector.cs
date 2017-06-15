// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.ObjRefSurrogate
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  internal class ObjRefSurrogate : ISerializationSurrogate
  {
    [SecurityCritical]
    public virtual void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      if (info == null)
        throw new ArgumentNullException("info");
      ((ObjRef) obj).GetObjectData(info, context);
      info.AddValue("fIsMarshalled", 0);
    }

    [SecurityCritical]
    public virtual object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_PopulateData"));
    }
  }
}
