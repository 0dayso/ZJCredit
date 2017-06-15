// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.ICategoryMembershipDataEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("DA0C3B27-6B6B-4b80-A8F8-6CE14F4BC0A4")]
  [ComImport]
  internal interface ICategoryMembershipDataEntry
  {
    CategoryMembershipDataEntry AllData { [SecurityCritical] get; }

    uint index { [SecurityCritical] get; }

    string Xml { [SecurityCritical] get; }

    string Description { [SecurityCritical] get; }
  }
}
