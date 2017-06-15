// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.InterfaceForwardingSupport
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [Flags]
  internal enum InterfaceForwardingSupport
  {
    None = 0,
    IBindableVector = 1,
    IVector = 2,
    IBindableVectorView = 4,
    IVectorView = 8,
    IBindableIterableOrIIterable = 16,
  }
}
