// Decompiled with JetBrains decompiler
// Type: System.Text.SurrogateEncoder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
  [Serializable]
  internal sealed class SurrogateEncoder : ISerializable, IObjectReference
  {
    [NonSerialized]
    private Encoding realEncoding;

    internal SurrogateEncoder(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      this.realEncoding = (Encoding) info.GetValue("m_encoding", typeof (Encoding));
    }

    [SecurityCritical]
    public object GetRealObject(StreamingContext context)
    {
      return (object) this.realEncoding.GetEncoder();
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      throw new ArgumentException(Environment.GetResourceString("Arg_ExecutionEngineException"));
    }
  }
}
