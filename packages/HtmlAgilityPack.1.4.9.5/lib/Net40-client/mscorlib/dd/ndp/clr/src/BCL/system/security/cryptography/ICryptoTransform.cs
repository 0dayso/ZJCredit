// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.ICryptoTransform
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>定义基本的加密转换运算。</summary>
  [ComVisible(true)]
  public interface ICryptoTransform : IDisposable
  {
    /// <summary>获取输入块大小。</summary>
    /// <returns>输入数据块的大小（以字节为单位）。</returns>
    int InputBlockSize { get; }

    /// <summary>获取输出块大小。</summary>
    /// <returns>输出数据块的大小（以字节为单位）。</returns>
    int OutputBlockSize { get; }

    /// <summary>获取一个值，该值指示是否可以转换多个块。</summary>
    /// <returns>如果可以转换多个块，则为 true；否则，为 false。</returns>
    bool CanTransformMultipleBlocks { get; }

    /// <summary>获取一个值，该值指示是否可重复使用当前转换。</summary>
    /// <returns>如果重复使用当前转换，则为 true；否则为 false。</returns>
    bool CanReuseTransform { get; }

    /// <summary>转换输入字节数组的指定区域，并将所得到的转换复制到输出字节数组的指定区域。</summary>
    /// <returns>写入的字节数。</returns>
    /// <param name="inputBuffer">要为其计算转换的输入。</param>
    /// <param name="inputOffset">输入字节数组中的偏移量，从该位置开始使用数据。</param>
    /// <param name="inputCount">输入字节数组中用作数据的字节数。</param>
    /// <param name="outputBuffer">将转换写入的输出。</param>
    /// <param name="outputOffset">输出字节数组中的偏移量，从该位置开始写入数据。</param>
    int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset);

    /// <summary>转换指定字节数组的指定区域。</summary>
    /// <returns>计算所得的转换。</returns>
    /// <param name="inputBuffer">要为其计算转换的输入。</param>
    /// <param name="inputOffset">字节数组中的偏移量，从该位置开始使用数据。</param>
    /// <param name="inputCount">字节数组中用作数据的字节数。</param>
    byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount);
  }
}
