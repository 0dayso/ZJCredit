// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.PolicyRights
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.Principal
{
  [Flags]
  internal enum PolicyRights
  {
    POLICY_VIEW_LOCAL_INFORMATION = 1,
    POLICY_VIEW_AUDIT_INFORMATION = 2,
    POLICY_GET_PRIVATE_INFORMATION = 4,
    POLICY_TRUST_ADMIN = 8,
    POLICY_CREATE_ACCOUNT = 16,
    POLICY_CREATE_SECRET = 32,
    POLICY_CREATE_PRIVILEGE = 64,
    POLICY_SET_DEFAULT_QUOTA_LIMITS = 128,
    POLICY_SET_AUDIT_REQUIREMENTS = 256,
    POLICY_AUDIT_LOG_ADMIN = 512,
    POLICY_SERVER_ADMIN = 1024,
    POLICY_LOOKUP_NAMES = 2048,
    POLICY_NOTIFICATION = 4096,
  }
}
