// Decompiled with JetBrains decompiler
// Type: System.Reflection.CustomAttributeEncoding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection
{
  [Serializable]
  internal enum CustomAttributeEncoding
  {
    Undefined = 0,
    Boolean = 2,
    Char = 3,
    SByte = 4,
    Byte = 5,
    Int16 = 6,
    UInt16 = 7,
    Int32 = 8,
    UInt32 = 9,
    Int64 = 10,
    UInt64 = 11,
    Float = 12,
    Double = 13,
    String = 14,
    Array = 29,
    Type = 80,
    Object = 81,
    Field = 83,
    Property = 84,
    Enum = 85,
  }
}
