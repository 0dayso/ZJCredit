// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.IKeyValuePair`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [Guid("02b51929-c1c4-4a7e-8940-0312b5c18500")]
  [ComImport]
  internal interface IKeyValuePair<K, V>
  {
    K Key { get; }

    V Value { get; }
  }
}
