﻿// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.GenericFieldInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection.Emit
{
  internal sealed class GenericFieldInfo
  {
    internal RuntimeFieldHandle m_fieldHandle;
    internal RuntimeTypeHandle m_context;

    internal GenericFieldInfo(RuntimeFieldHandle fieldHandle, RuntimeTypeHandle context)
    {
      this.m_fieldHandle = fieldHandle;
      this.m_context = context;
    }
  }
}
