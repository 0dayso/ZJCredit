// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.IIterator`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [Guid("6a79e863-4300-459a-9966-cbb660963ee1")]
  [ComImport]
  internal interface IIterator<T>
  {
    T Current { get; }

    bool HasCurrent { get; }

    bool MoveNext();

    int GetMany([Out] T[] items);
  }
}
