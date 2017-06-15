// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.CryptoKeyRights
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.AccessControl
{
  /// <summary>指定授权规则控制其访问或审核的加密密钥操作。</summary>
  [Flags]
  public enum CryptoKeyRights
  {
    ReadData = 1,
    WriteData = 2,
    ReadExtendedAttributes = 8,
    WriteExtendedAttributes = 16,
    ReadAttributes = 128,
    WriteAttributes = 256,
    Delete = 65536,
    ReadPermissions = 131072,
    ChangePermissions = 262144,
    TakeOwnership = 524288,
    Synchronize = 1048576,
    FullControl = Synchronize | TakeOwnership | ChangePermissions | ReadPermissions | Delete | WriteAttributes | ReadAttributes | WriteExtendedAttributes | ReadExtendedAttributes | WriteData | ReadData,
    GenericAll = 268435456,
    GenericExecute = 536870912,
    GenericWrite = 1073741824,
    GenericRead = -2147483648,
  }
}
