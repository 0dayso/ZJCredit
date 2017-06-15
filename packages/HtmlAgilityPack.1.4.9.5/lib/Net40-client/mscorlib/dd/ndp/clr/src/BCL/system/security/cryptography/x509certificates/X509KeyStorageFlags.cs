// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.X509Certificates.X509KeyStorageFlags
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
  /// <summary>定义将 X.509 证书的私钥导入到何处以及如何导出。</summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum X509KeyStorageFlags
  {
    DefaultKeySet = 0,
    UserKeySet = 1,
    MachineKeySet = 2,
    Exportable = 4,
    UserProtected = 8,
    PersistKeySet = 16,
  }
}
