﻿// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.IReferenceArray`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [Guid("61c17707-2d65-11e0-9ae8-d48564015472")]
  [ComImport]
  internal interface IReferenceArray<T> : IPropertyValue
  {
    T[] Value { get; }
  }
}
