// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.ApplicationVersionMatch
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Policy
{
  /// <summary>指定在集合中查找应用程序信任时如何匹配版本。</summary>
  [ComVisible(true)]
  public enum ApplicationVersionMatch
  {
    MatchExactVersion,
    MatchAllVersions,
  }
}
