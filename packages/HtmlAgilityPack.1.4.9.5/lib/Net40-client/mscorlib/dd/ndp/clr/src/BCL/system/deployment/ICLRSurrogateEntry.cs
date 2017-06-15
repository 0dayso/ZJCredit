// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.ICLRSurrogateEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("1E0422A1-F0D2-44ae-914B-8A2DECCFD22B")]
  [ComImport]
  internal interface ICLRSurrogateEntry
  {
    CLRSurrogateEntry AllData { [SecurityCritical] get; }

    Guid Clsid { [SecurityCritical] get; }

    string RuntimeVersion { [SecurityCritical] get; }

    string ClassName { [SecurityCritical] get; }
  }
}
