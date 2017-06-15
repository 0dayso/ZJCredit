// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.CipherMode
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>指定用于加密的块密码模式。</summary>
  [ComVisible(true)]
  [Serializable]
  public enum CipherMode
  {
    CBC = 1,
    ECB = 2,
    OFB = 3,
    CFB = 4,
    CTS = 5,
  }
}
