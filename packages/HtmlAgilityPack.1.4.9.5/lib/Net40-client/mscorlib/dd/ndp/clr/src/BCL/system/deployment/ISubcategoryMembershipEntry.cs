﻿// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.ISubcategoryMembershipEntry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("5A7A54D7-5AD5-418e-AB7A-CF823A8D48D0")]
  [ComImport]
  internal interface ISubcategoryMembershipEntry
  {
    SubcategoryMembershipEntry AllData { [SecurityCritical] get; }

    string Subcategory { [SecurityCritical] get; }

    ISection CategoryMembershipData { [SecurityCritical] get; }
  }
}
