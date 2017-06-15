// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.IEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  [Guid("496B0ABF-CDEE-11d3-88E8-00902754C43A")]
  internal interface IEnumerator
  {
    object Current { get; }

    bool MoveNext();

    void Reset();
  }
}
