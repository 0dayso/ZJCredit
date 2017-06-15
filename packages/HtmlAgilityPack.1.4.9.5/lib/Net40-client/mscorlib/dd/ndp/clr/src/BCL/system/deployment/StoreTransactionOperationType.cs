// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.StoreTransactionOperationType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Deployment.Internal.Isolation
{
  internal enum StoreTransactionOperationType
  {
    Invalid = 0,
    SetCanonicalizationContext = 14,
    StageComponent = 20,
    PinDeployment = 21,
    UnpinDeployment = 22,
    StageComponentFile = 23,
    InstallDeployment = 24,
    UninstallDeployment = 25,
    SetDeploymentMetadata = 26,
    Scavenge = 27,
  }
}
