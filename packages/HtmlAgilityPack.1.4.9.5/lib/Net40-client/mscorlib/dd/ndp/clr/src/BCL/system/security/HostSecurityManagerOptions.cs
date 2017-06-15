// Decompiled with JetBrains decompiler
// Type: System.Security.HostSecurityManagerOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security
{
  /// <summary>指定将由宿主安全管理器使用的安全策略组件。</summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum HostSecurityManagerOptions
  {
    None = 0,
    HostAppDomainEvidence = 1,
    [Obsolete("AppDomain policy levels are obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")] HostPolicyLevel = 2,
    HostAssemblyEvidence = 4,
    HostDetermineApplicationTrust = 8,
    HostResolvePolicy = 16,
    AllFlags = HostResolvePolicy | HostDetermineApplicationTrust | HostAssemblyEvidence | HostPolicyLevel | HostAppDomainEvidence,
  }
}
