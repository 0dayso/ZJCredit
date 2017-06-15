// Decompiled with JetBrains decompiler
// Type: System.Security.SecurityZone
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security
{
  /// <summary>定义与安全策略所使用的安全区域相对应的整数值。</summary>
  [ComVisible(true)]
  [Serializable]
  public enum SecurityZone
  {
    NoZone = -1,
    MyComputer = 0,
    Intranet = 1,
    Trusted = 2,
    Internet = 3,
    Untrusted = 4,
  }
}
