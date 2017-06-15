// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.BinaryCrossAppDomainAssembly
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class BinaryCrossAppDomainAssembly : IStreamable
  {
    internal int assemId;
    internal int assemblyIndex;

    internal BinaryCrossAppDomainAssembly()
    {
    }

    public void Write(__BinaryWriter sout)
    {
      sout.WriteByte((byte) 20);
      sout.WriteInt32(this.assemId);
      sout.WriteInt32(this.assemblyIndex);
    }

    [SecurityCritical]
    public void Read(__BinaryParser input)
    {
      this.assemId = input.ReadInt32();
      this.assemblyIndex = input.ReadInt32();
    }

    public void Dump()
    {
    }

    [Conditional("_LOGGING")]
    private void DumpInternal()
    {
      BCLDebug.CheckEnabled("BINARY");
    }
  }
}
