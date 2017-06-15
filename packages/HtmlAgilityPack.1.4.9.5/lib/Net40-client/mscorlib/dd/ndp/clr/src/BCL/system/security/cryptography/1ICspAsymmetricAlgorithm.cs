// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.ICspAsymmetricAlgorithm
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>定义若干方法，使 <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> 类可以枚举密钥容器信息以及导入和导出与 Microsoft Cryptographic API (CAPI) 兼容的密钥 Blob。</summary>
  [ComVisible(true)]
  public interface ICspAsymmetricAlgorithm
  {
    /// <summary>获取描述有关加密密钥对的附加信息的 <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" /> 对象。</summary>
    /// <returns>描述有关加密密钥对的附加信息的 <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" /> 对象。</returns>
    CspKeyContainerInfo CspKeyContainerInfo { get; }

    /// <summary>导出包含与 <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> 对象关联的密钥信息的 Blob。</summary>
    /// <returns>一个字节数组，包含与 <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> 对象关联的密钥信息。</returns>
    /// <param name="includePrivateParameters">要包括私钥，则为 true；否则为 false。</param>
    byte[] ExportCspBlob(bool includePrivateParameters);

    /// <summary>导入一个表示非对称密钥信息的 Blob。</summary>
    /// <param name="rawData">一个表示非对称密钥 Blob 的字节数组。</param>
    void ImportCspBlob(byte[] rawData);
  }
}
