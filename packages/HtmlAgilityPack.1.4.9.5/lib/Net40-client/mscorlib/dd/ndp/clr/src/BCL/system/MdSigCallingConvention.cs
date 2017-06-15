// Decompiled with JetBrains decompiler
// Type: System.Reflection.MdSigCallingConvention
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection
{
  [Flags]
  [Serializable]
  internal enum MdSigCallingConvention : byte
  {
    CallConvMask = 15,
    Default = 0,
    C = 1,
    StdCall = 2,
    ThisCall = StdCall | C,
    FastCall = 4,
    Vararg = FastCall | C,
    Field = FastCall | StdCall,
    LocalSig = Field | C,
    Property = 8,
    Unmgd = Property | C,
    GenericInst = Property | StdCall,
    Generic = 16,
    HasThis = 32,
    ExplicitThis = 64,
  }
}
