// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.STORE_ASSEMBLY_STATUS_FLAGS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Deployment.Internal.Isolation
{
  [Flags]
  internal enum STORE_ASSEMBLY_STATUS_FLAGS
  {
    STORE_ASSEMBLY_STATUS_MANIFEST_ONLY = 1,
    STORE_ASSEMBLY_STATUS_PAYLOAD_RESIDENT = 2,
    STORE_ASSEMBLY_STATUS_PARTIAL_INSTALL = 4,
  }
}
