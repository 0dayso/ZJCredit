﻿// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.Manifest.CMS_ASSEMBLY_DEPLOYMENT_FLAG
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Deployment.Internal.Isolation.Manifest
{
  internal enum CMS_ASSEMBLY_DEPLOYMENT_FLAG
  {
    CMS_ASSEMBLY_DEPLOYMENT_FLAG_BEFORE_APPLICATION_STARTUP = 4,
    CMS_ASSEMBLY_DEPLOYMENT_FLAG_RUN_AFTER_INSTALL = 16,
    CMS_ASSEMBLY_DEPLOYMENT_FLAG_INSTALL = 32,
    CMS_ASSEMBLY_DEPLOYMENT_FLAG_TRUST_URL_PARAMETERS = 64,
    CMS_ASSEMBLY_DEPLOYMENT_FLAG_DISALLOW_URL_ACTIVATION = 128,
    CMS_ASSEMBLY_DEPLOYMENT_FLAG_MAP_FILE_EXTENSIONS = 256,
    CMS_ASSEMBLY_DEPLOYMENT_FLAG_CREATE_DESKTOP_SHORTCUT = 512,
  }
}
