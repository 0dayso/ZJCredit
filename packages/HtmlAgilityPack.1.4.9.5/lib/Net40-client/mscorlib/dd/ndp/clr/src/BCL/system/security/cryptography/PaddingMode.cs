// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.PaddingMode
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>指定在消息数据块比加密操作所需的全部字节数短时应用的填充类型。</summary>
  [ComVisible(true)]
  [Serializable]
  public enum PaddingMode
  {
    None = 1,
    PKCS7 = 2,
    Zeros = 3,
    ANSIX923 = 4,
    ISO10126 = 5,
  }
}
