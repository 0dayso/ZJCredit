﻿// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.VarArgMethod
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection.Emit
{
  internal sealed class VarArgMethod
  {
    internal RuntimeMethodInfo m_method;
    internal DynamicMethod m_dynamicMethod;
    internal SignatureHelper m_signature;

    internal VarArgMethod(DynamicMethod dm, SignatureHelper signature)
    {
      this.m_dynamicMethod = dm;
      this.m_signature = signature;
    }

    internal VarArgMethod(RuntimeMethodInfo method, SignatureHelper signature)
    {
      this.m_method = method;
      this.m_signature = signature;
    }
  }
}
