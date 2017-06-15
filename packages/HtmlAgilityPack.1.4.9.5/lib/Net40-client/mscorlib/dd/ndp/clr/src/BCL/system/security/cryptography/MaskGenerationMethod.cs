// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.MaskGenerationMethod
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>表示所有掩码生成器算法均必须从中派生的抽象类。</summary>
  [ComVisible(true)]
  public abstract class MaskGenerationMethod
  {
    /// <summary>当在派生类中重写时，使用指定的随机种子生成具有指定长度的掩码。</summary>
    /// <returns>长度等于 <paramref name="cbReturn" /> 参数的随机生成的掩码。</returns>
    /// <param name="rgbSeed">用于计算掩码的随机种子。</param>
    /// <param name="cbReturn">生成的掩码的长度（以字节为单位）。</param>
    [ComVisible(true)]
    public abstract byte[] GenerateMask(byte[] rgbSeed, int cbReturn);
  }
}
