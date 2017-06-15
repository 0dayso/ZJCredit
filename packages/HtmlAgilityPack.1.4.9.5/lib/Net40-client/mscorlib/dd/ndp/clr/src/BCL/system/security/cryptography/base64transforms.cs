// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.ToBase64Transform
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography
{
  /// <summary>将 <see cref="T:System.Security.Cryptography.CryptoStream" /> 转换为 Base 64。</summary>
  [ComVisible(true)]
  public class ToBase64Transform : ICryptoTransform, IDisposable
  {
    /// <summary>获取输入块大小。</summary>
    /// <returns>输入数据块的大小（以字节为单位）。</returns>
    public int InputBlockSize
    {
      get
      {
        return 3;
      }
    }

    /// <summary>获取输出块大小。</summary>
    /// <returns>输出数据块的大小（以字节为单位）。</returns>
    public int OutputBlockSize
    {
      get
      {
        return 4;
      }
    }

    /// <summary>获取一个值，该值指示是否可转换多个块。</summary>
    /// <returns>总是为 false。</returns>
    public bool CanTransformMultipleBlocks
    {
      get
      {
        return false;
      }
    }

    /// <summary>获取一个值，该值指示是否可重复使用当前转换。</summary>
    /// <returns>总是为 true。</returns>
    public virtual bool CanReuseTransform
    {
      get
      {
        return true;
      }
    }

    /// <summary>释放 <see cref="T:System.Security.Cryptography.ToBase64Transform" /> 使用的非托管资源。</summary>
    ~ToBase64Transform()
    {
      this.Dispose(false);
    }

    /// <summary>将输入字节数组的指定区域转换为 Base 64，并将结果复制到输出字节数组的指定区域。</summary>
    /// <returns>写入的字节数。</returns>
    /// <param name="inputBuffer">要计算为 Base 64 的输入。</param>
    /// <param name="inputOffset">输入字节数组中的偏移量，从该位置开始使用数据。</param>
    /// <param name="inputCount">输入字节数组中用作数据的字节数。</param>
    /// <param name="outputBuffer">要向其写入结果的输出。</param>
    /// <param name="outputOffset">输入字节数组中的偏移量，从该位置开始使用数据。</param>
    /// <exception cref="T:System.ObjectDisposedException">已释放当前的 <see cref="T:System.Security.Cryptography.ToBase64Transform" /> 对象。</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">数据大小无效。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="inputBuffer" /> 参数包含无效偏移量长度。- 或 -<paramref name="inputCount" /> 参数包含无效值。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="inputBuffer" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="inputBuffer" /> 参数需要非负数。</exception>
    public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
    {
      if (inputBuffer == null)
        throw new ArgumentNullException("inputBuffer");
      if (inputOffset < 0)
        throw new ArgumentOutOfRangeException("inputOffset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (inputCount < 0 || inputCount > inputBuffer.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
      if (inputBuffer.Length - inputCount < inputOffset)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      char[] chArray = new char[4];
      Convert.ToBase64CharArray(inputBuffer, inputOffset, 3, chArray, 0);
      byte[] bytes = Encoding.ASCII.GetBytes(chArray);
      if (bytes.Length != 4)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_SSE_InvalidDataSize"));
      Buffer.BlockCopy((Array) bytes, 0, (Array) outputBuffer, outputOffset, bytes.Length);
      return bytes.Length;
    }

    /// <summary>将指定字节数组的指定区域转换为 Base 64。</summary>
    /// <returns>已计算的 Base 64 转换。</returns>
    /// <param name="inputBuffer">要转换为 Base 64 的输入。</param>
    /// <param name="inputOffset">字节数组中的偏移量，从该位置开始使用数据。</param>
    /// <param name="inputCount">字节数组中用作数据的字节数。</param>
    /// <exception cref="T:System.ObjectDisposedException">已释放当前的 <see cref="T:System.Security.Cryptography.ToBase64Transform" /> 对象。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="inputBuffer" /> 参数包含无效偏移量长度。- 或 -<paramref name="inputCount" /> 参数包含无效值。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="inputBuffer" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="inputBuffer" /> 参数需要非负数。</exception>
    public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
    {
      if (inputBuffer == null)
        throw new ArgumentNullException("inputBuffer");
      if (inputOffset < 0)
        throw new ArgumentOutOfRangeException("inputOffset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (inputCount < 0 || inputCount > inputBuffer.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
      if (inputBuffer.Length - inputCount < inputOffset)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (inputCount == 0)
        return EmptyArray<byte>.Value;
      char[] chArray = new char[4];
      Convert.ToBase64CharArray(inputBuffer, inputOffset, inputCount, chArray, 0);
      return Encoding.ASCII.GetBytes(chArray);
    }

    /// <summary>释放 <see cref="T:System.Security.Cryptography.ToBase64Transform" /> 类的当前实例所使用的所有资源。</summary>
    public void Dispose()
    {
      this.Clear();
    }

    /// <summary>释放由 <see cref="T:System.Security.Cryptography.ToBase64Transform" /> 使用的所有资源。</summary>
    public void Clear()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>释放由 <see cref="T:System.Security.Cryptography.ToBase64Transform" /> 占用的非托管资源，还可以释放托管资源。</summary>
    /// <param name="disposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。</param>
    protected virtual void Dispose(bool disposing)
    {
    }
  }
}
