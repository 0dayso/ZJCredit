// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.SecurityPermissionFlag
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>为安全权限对象指定访问标志。</summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum SecurityPermissionFlag
  {
    NoFlags = 0,
    Assertion = 1,
    UnmanagedCode = 2,
    SkipVerification = 4,
    Execution = 8,
    ControlThread = 16,
    ControlEvidence = 32,
    ControlPolicy = 64,
    SerializationFormatter = 128,
    ControlDomainPolicy = 256,
    ControlPrincipal = 512,
    ControlAppDomain = 1024,
    RemotingConfiguration = 2048,
    Infrastructure = 4096,
    BindingRedirects = 8192,
    AllFlags = BindingRedirects | Infrastructure | RemotingConfiguration | ControlAppDomain | ControlPrincipal | ControlDomainPolicy | SerializationFormatter | ControlPolicy | ControlEvidence | ControlThread | Execution | SkipVerification | UnmanagedCode | Assertion,
  }
}
