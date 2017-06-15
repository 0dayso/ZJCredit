// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.HostProtectionResource
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>指定若由方法或类调用，可能对宿主有害的功能的类别。</summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum HostProtectionResource
  {
    None = 0,
    Synchronization = 1,
    SharedState = 2,
    ExternalProcessMgmt = 4,
    SelfAffectingProcessMgmt = 8,
    ExternalThreading = 16,
    SelfAffectingThreading = 32,
    SecurityInfrastructure = 64,
    UI = 128,
    MayLeakOnAbort = 256,
    All = MayLeakOnAbort | UI | SecurityInfrastructure | SelfAffectingThreading | ExternalThreading | SelfAffectingProcessMgmt | ExternalProcessMgmt | SharedState | Synchronization,
  }
}
