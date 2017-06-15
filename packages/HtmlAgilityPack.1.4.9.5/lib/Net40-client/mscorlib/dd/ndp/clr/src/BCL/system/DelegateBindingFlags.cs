// Decompiled with JetBrains decompiler
// Type: System.DelegateBindingFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  internal enum DelegateBindingFlags
  {
    StaticMethodOnly = 1,
    InstanceMethodOnly = 2,
    OpenDelegateOnly = 4,
    ClosedDelegateOnly = 8,
    NeverCloseOverNull = 16,
    CaselessMatching = 32,
    SkipSecurityChecks = 64,
    RelaxedSignature = 128,
  }
}
