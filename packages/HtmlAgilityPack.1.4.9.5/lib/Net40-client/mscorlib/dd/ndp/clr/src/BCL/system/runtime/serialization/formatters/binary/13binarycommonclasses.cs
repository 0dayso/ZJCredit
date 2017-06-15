// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.MemberPrimitiveTyped
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class MemberPrimitiveTyped : IStreamable
  {
    internal InternalPrimitiveTypeE primitiveTypeEnum;
    internal object value;

    internal MemberPrimitiveTyped()
    {
    }

    internal void Set(InternalPrimitiveTypeE primitiveTypeEnum, object value)
    {
      this.primitiveTypeEnum = primitiveTypeEnum;
      this.value = value;
    }

    public void Write(__BinaryWriter sout)
    {
      sout.WriteByte((byte) 8);
      sout.WriteByte((byte) this.primitiveTypeEnum);
      sout.WriteValue(this.primitiveTypeEnum, this.value);
    }

    [SecurityCritical]
    public void Read(__BinaryParser input)
    {
      this.primitiveTypeEnum = (InternalPrimitiveTypeE) input.ReadByte();
      this.value = input.ReadValue(this.primitiveTypeEnum);
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
