// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.MessageEnd
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.IO;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class MessageEnd : IStreamable
  {
    internal MessageEnd()
    {
    }

    public void Write(__BinaryWriter sout)
    {
      sout.WriteByte((byte) 11);
    }

    [SecurityCritical]
    public void Read(__BinaryParser input)
    {
    }

    public void Dump()
    {
    }

    public void Dump(Stream sout)
    {
    }

    [Conditional("_LOGGING")]
    private void DumpInternal(Stream sout)
    {
      if (!BCLDebug.CheckEnabled("BINARY") || sout == null || !sout.CanSeek)
        return;
      long length = sout.Length;
    }
  }
}
