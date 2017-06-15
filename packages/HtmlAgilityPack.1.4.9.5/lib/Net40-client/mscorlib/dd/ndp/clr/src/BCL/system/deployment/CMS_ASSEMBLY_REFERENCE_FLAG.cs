// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.CMS_ASSEMBLY_REFERENCE_FLAG
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Deployment.Internal.Isolation.Manifest
{
  internal enum CMS_ASSEMBLY_REFERENCE_FLAG
  {
    CMS_ASSEMBLY_REFERENCE_FLAG_OPTIONAL = 1,
    CMS_ASSEMBLY_REFERENCE_FLAG_VISIBLE = 2,
    CMS_ASSEMBLY_REFERENCE_FLAG_FOLLOW = 4,
    CMS_ASSEMBLY_REFERENCE_FLAG_IS_PLATFORM = 8,
    CMS_ASSEMBLY_REFERENCE_FLAG_CULTURE_WILDCARDED = 16,
    CMS_ASSEMBLY_REFERENCE_FLAG_PROCESSOR_ARCHITECTURE_WILDCARDED = 32,
    CMS_ASSEMBLY_REFERENCE_FLAG_PREREQUISITE = 128,
  }
}
