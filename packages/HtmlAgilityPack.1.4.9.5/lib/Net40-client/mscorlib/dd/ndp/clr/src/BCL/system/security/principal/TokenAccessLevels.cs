// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.TokenAccessLevels
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Principal
{
  /// <summary>定义与访问令牌相关联的用户帐户的特权。</summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum TokenAccessLevels
  {
    AssignPrimary = 1,
    Duplicate = 2,
    Impersonate = 4,
    Query = 8,
    QuerySource = 16,
    AdjustPrivileges = 32,
    AdjustGroups = 64,
    AdjustDefault = 128,
    AdjustSessionId = 256,
    Read = 131080,
    Write = 131296,
    AllAccess = 983551,
    MaximumAllowed = 33554432,
  }
}
