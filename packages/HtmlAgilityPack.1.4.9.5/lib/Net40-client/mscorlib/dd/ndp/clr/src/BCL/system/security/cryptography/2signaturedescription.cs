// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.DSASignatureDescription
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.Cryptography
{
  internal class DSASignatureDescription : SignatureDescription
  {
    public DSASignatureDescription()
    {
      this.KeyAlgorithm = "System.Security.Cryptography.DSACryptoServiceProvider";
      this.DigestAlgorithm = "System.Security.Cryptography.SHA1CryptoServiceProvider";
      this.FormatterAlgorithm = "System.Security.Cryptography.DSASignatureFormatter";
      this.DeformatterAlgorithm = "System.Security.Cryptography.DSASignatureDeformatter";
    }
  }
}
