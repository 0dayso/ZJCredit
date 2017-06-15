// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.IBindableVectorView
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [Guid("346dd6e7-976e-4bc3-815d-ece243bc0f33")]
  [ComImport]
  internal interface IBindableVectorView : IBindableIterable
  {
    uint Size { get; }

    object GetAt(uint index);

    bool IndexOf(object value, out uint index);
  }
}
