// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.KeyNumber
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>指定是创建不对称签名密钥还是创建不对称交换密钥。</summary>
  [ComVisible(true)]
  [Serializable]
  public enum KeyNumber
  {
    Exchange = 1,
    Signature = 2,
  }
}
