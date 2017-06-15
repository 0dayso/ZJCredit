// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.IWindowClassEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("8AD3FC86-AFD3-477a-8FD5-146C291195BA")]
  [ComImport]
  internal interface IWindowClassEntry
  {
    WindowClassEntry AllData { [SecurityCritical] get; }

    string ClassName { [SecurityCritical] get; }

    string HostDll { [SecurityCritical] get; }

    bool fVersioned { [SecurityCritical] get; }
  }
}
