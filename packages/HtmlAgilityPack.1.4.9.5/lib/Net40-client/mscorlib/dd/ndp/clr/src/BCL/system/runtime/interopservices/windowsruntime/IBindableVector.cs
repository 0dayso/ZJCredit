// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.IBindableVector
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [Guid("393de7de-6fd0-4c0d-bb71-47244a113e93")]
  [ComImport]
  internal interface IBindableVector : IBindableIterable
  {
    uint Size { get; }

    object GetAt(uint index);

    IBindableVectorView GetView();

    bool IndexOf(object value, out uint index);

    void SetAt(uint index, object value);

    void InsertAt(uint index, object value);

    void RemoveAt(uint index);

    void Append(object value);

    void RemoveAtEnd();

    void Clear();
  }
}
