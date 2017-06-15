// Decompiled with JetBrains decompiler
// Type: System.Security.SecurityCriticalScope
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security
{
  /// <summary>指定 <see cref="T:System.Security.SecurityCriticalAttribute" /> 的范围。</summary>
  [Obsolete("SecurityCriticalScope is only used for .NET 2.0 transparency compatibility.")]
  public enum SecurityCriticalScope
  {
    Explicit,
    Everything,
  }
}
