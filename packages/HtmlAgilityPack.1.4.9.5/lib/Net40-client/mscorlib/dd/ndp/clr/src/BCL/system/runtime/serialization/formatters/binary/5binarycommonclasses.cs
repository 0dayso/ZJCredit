// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.BinaryAssembly
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class BinaryAssembly : IStreamable
  {
    internal int assemId;
    internal string assemblyString;

    internal BinaryAssembly()
    {
    }

    internal void Set(int assemId, string assemblyString)
    {
      this.assemId = assemId;
      this.assemblyString = assemblyString;
    }

    public void Write(__BinaryWriter sout)
    {
      sout.WriteByte((byte) 12);
      sout.WriteInt32(this.assemId);
      sout.WriteString(this.assemblyString);
    }

    [SecurityCritical]
    public void Read(__BinaryParser input)
    {
      this.assemId = input.ReadInt32();
      this.assemblyString = input.ReadString();
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
