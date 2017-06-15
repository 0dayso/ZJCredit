// Decompiled with JetBrains decompiler
// Type: System.Reflection.CorElementType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection
{
  [Serializable]
  internal enum CorElementType : byte
  {
    End = 0,
    Void = 1,
    Boolean = 2,
    Char = 3,
    I1 = 4,
    U1 = 5,
    I2 = 6,
    U2 = 7,
    I4 = 8,
    U4 = 9,
    I8 = 10,
    U8 = 11,
    R4 = 12,
    R8 = 13,
    String = 14,
    Ptr = 15,
    ByRef = 16,
    ValueType = 17,
    Class = 18,
    Var = 19,
    Array = 20,
    GenericInst = 21,
    TypedByRef = 22,
    I = 24,
    U = 25,
    FnPtr = 27,
    Object = 28,
    SzArray = 29,
    MVar = 30,
    CModReqd = 31,
    CModOpt = 32,
    Internal = 33,
    Max = 34,
    Modifier = 64,
    Sentinel = 65,
    Pinned = 69,
  }
}
