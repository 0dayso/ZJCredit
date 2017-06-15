// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.MessageEnum
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Serialization.Formatters.Binary
{
  [Flags]
  [Serializable]
  internal enum MessageEnum
  {
    NoArgs = 1,
    ArgsInline = 2,
    ArgsIsArray = 4,
    ArgsInArray = 8,
    NoContext = 16,
    ContextInline = 32,
    ContextInArray = 64,
    MethodSignatureInArray = 128,
    PropertyInArray = 256,
    NoReturnValue = 512,
    ReturnValueVoid = 1024,
    ReturnValueInline = 2048,
    ReturnValueInArray = 4096,
    ExceptionInArray = 8192,
    GenericMethod = 32768,
  }
}
