// Decompiled with JetBrains decompiler
// Type: System.Security.Util.QuickCacheEntryType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.Util
{
  [Flags]
  [Serializable]
  internal enum QuickCacheEntryType
  {
    FullTrustZoneMyComputer = 16777216,
    FullTrustZoneIntranet = 33554432,
    FullTrustZoneInternet = 67108864,
    FullTrustZoneTrusted = 134217728,
    FullTrustZoneUntrusted = 268435456,
    FullTrustAll = 536870912,
  }
}
