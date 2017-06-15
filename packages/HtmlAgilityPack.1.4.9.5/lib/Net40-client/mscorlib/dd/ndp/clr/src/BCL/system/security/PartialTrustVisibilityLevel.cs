// Decompiled with JetBrains decompiler
// Type: System.Security.PartialTrustVisibilityLevel
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security
{
  /// <summary>指定用 <see cref="T:System.Security.AllowPartiallyTrustedCallersAttribute" /> (APTCA) 特性标记的代码的默认部分信任可见性。</summary>
  public enum PartialTrustVisibilityLevel
  {
    VisibleToAllHosts,
    NotVisibleByDefault,
  }
}
