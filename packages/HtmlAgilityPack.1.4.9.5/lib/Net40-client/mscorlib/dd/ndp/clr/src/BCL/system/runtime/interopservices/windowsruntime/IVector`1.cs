﻿// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.IVector`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [Guid("913337e9-11a1-4345-a3a2-4e7f956e222d")]
  [ComImport]
  internal interface IVector<T> : IIterable<T>, IEnumerable<T>, IEnumerable
  {
    uint Size { get; }

    T GetAt(uint index);

    IReadOnlyList<T> GetView();

    bool IndexOf(T value, out uint index);

    void SetAt(uint index, T value);

    void InsertAt(uint index, T value);

    void RemoveAt(uint index);

    void Append(T value);

    void RemoveAtEnd();

    void Clear();

    uint GetMany(uint startIndex, [Out] T[] items);

    void ReplaceAll(T[] items);
  }
}
