﻿// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.IIterable`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [Guid("faa585ea-6214-4217-afda-7f46de5869b3")]
  [ComImport]
  internal interface IIterable<T> : IEnumerable<T>, IEnumerable
  {
    IIterator<T> First();
  }
}
