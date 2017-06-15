// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RSAPKCS1SHA1SignatureDescription
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.Cryptography
{
  internal class RSAPKCS1SHA1SignatureDescription : SignatureDescription
  {
    public RSAPKCS1SHA1SignatureDescription()
    {
      this.KeyAlgorithm = "System.Security.Cryptography.RSACryptoServiceProvider";
      this.DigestAlgorithm = "System.Security.Cryptography.SHA1CryptoServiceProvider";
      this.FormatterAlgorithm = "System.Security.Cryptography.RSAPKCS1SignatureFormatter";
      this.DeformatterAlgorithm = "System.Security.Cryptography.RSAPKCS1SignatureDeformatter";
    }

    public override AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
    {
      AsymmetricSignatureDeformatter signatureDeformatter = (AsymmetricSignatureDeformatter) CryptoConfig.CreateFromName(this.DeformatterAlgorithm);
      AsymmetricAlgorithm key1 = key;
      signatureDeformatter.SetKey(key1);
      string strName = "SHA1";
      signatureDeformatter.SetHashAlgorithm(strName);
      return signatureDeformatter;
    }
  }
}
