// Decompiled with JetBrains decompiler
// Type: System.TypeNameFormatFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  internal enum TypeNameFormatFlags
  {
    FormatBasic = 0,
    FormatNamespace = 1,
    FormatFullInst = 2,
    FormatAssembly = 4,
    FormatSignature = 8,
    FormatNoVersion = 16,
    FormatAngleBrackets = 64,
    FormatStubInfo = 128,
    FormatGenericParam = 256,
    FormatSerialization = 259,
  }
}
