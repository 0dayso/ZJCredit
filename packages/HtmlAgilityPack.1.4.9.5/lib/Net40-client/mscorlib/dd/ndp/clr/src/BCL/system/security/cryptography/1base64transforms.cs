// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.FromBase64Transform
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography
{
  /// <summary>从 Base 64 转换 <see cref="T:System.Security.Cryptography.CryptoStream" />。</summary>
  [ComVisible(true)]
  public class FromBase64Transform : ICryptoTransform, IDisposable
  {
    private byte[] _inputBuffer = new byte[4];
    private int _inputIndex;
    private FromBase64TransformMode _whitespaces;

    /// <summary>获取输入块大小。</summary>
    /// <returns>输入数据块的大小（以字节为单位）。</returns>
    public int InputBlockSize
    {
      get
      {
        return 1;
      }
    }

    /// <summary>获取输出块大小。</summary>
    /// <returns>输出数据块的大小（以字节为单位）。</returns>
    public int OutputBlockSize
    {
      get
      {
        return 3;
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

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.FromBase64Transform" /> 类的新实例。</summary>
    public FromBase64Transform()
      : this(FromBase64TransformMode.IgnoreWhiteSpaces)
    {
    }

    /// <summary>使用指定的转换模式初始化 <see cref="T:System.Security.Cryptography.FromBase64Transform" /> 类的新实例。</summary>
    /// <param name="whitespaces">
    /// <see cref="T:System.Security.Cryptography.FromBase64Transform" /> 值之一。</param>
    public FromBase64Transform(FromBase64TransformMode whitespaces)
    {
      this._whitespaces = whitespaces;
      this._inputIndex = 0;
    }

    /// <summary>释放 <see cref="T:System.Security.Cryptography.FromBase64Transform" /> 使用的非托管资源。</summary>
    ~FromBase64Transform()
    {
      this.Dispose(false);
    }

    /// <summary>将输入字节数组的指定区域从 Base 64 进行转换，并将结果复制到输出字节数组的指定区域。</summary>
    /// <returns>写入的字节数。</returns>
    /// <param name="inputBuffer">从 Base 64 计算的输入。</param>
    /// <param name="inputOffset">输入字节数组中的偏移量，从该位置开始使用数据。</param>
    /// <param name="inputCount">输入字节数组中用作数据的字节数。</param>
    /// <param name="outputBuffer">要向其写入结果的输出。</param>
    /// <param name="outputOffset">输入字节数组中的偏移量，从该位置开始使用数据。</param>
    /// <exception cref="T:System.ObjectDisposedException">已释放当前的 <see cref="T:System.Security.Cryptography.FromBase64Transform" /> 对象。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="inputCount" /> 使用了无效值。- 或 -<paramref name="inputBuffer" /> 具有无效偏移量长度。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="inputOffset" /> 超出范围。此参数需要非负数。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="inputBuffer" /> 为 null。</exception>
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
      if (this._inputBuffer == null)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_Generic"));
      byte[] numArray1 = new byte[inputCount];
      int byteCount;
      if (this._whitespaces == FromBase64TransformMode.IgnoreWhiteSpaces)
      {
        numArray1 = this.DiscardWhiteSpaces(inputBuffer, inputOffset, inputCount);
        byteCount = numArray1.Length;
      }
      else
      {
        Buffer.InternalBlockCopy((Array) inputBuffer, inputOffset, (Array) numArray1, 0, inputCount);
        byteCount = inputCount;
      }
      if (byteCount + this._inputIndex < 4)
      {
        Buffer.InternalBlockCopy((Array) numArray1, 0, (Array) this._inputBuffer, this._inputIndex, byteCount);
        this._inputIndex = this._inputIndex + byteCount;
        return 0;
      }
      int num = (byteCount + this._inputIndex) / 4;
      byte[] bytes = new byte[this._inputIndex + byteCount];
      Buffer.InternalBlockCopy((Array) this._inputBuffer, 0, (Array) bytes, 0, this._inputIndex);
      Buffer.InternalBlockCopy((Array) numArray1, 0, (Array) bytes, this._inputIndex, byteCount);
      this._inputIndex = (byteCount + this._inputIndex) % 4;
      Buffer.InternalBlockCopy((Array) numArray1, byteCount - this._inputIndex, (Array) this._inputBuffer, 0, this._inputIndex);
      byte[] numArray2 = Convert.FromBase64CharArray(Encoding.ASCII.GetChars(bytes, 0, 4 * num), 0, 4 * num);
      Buffer.BlockCopy((Array) numArray2, 0, (Array) outputBuffer, outputOffset, numArray2.Length);
      return numArray2.Length;
    }

    /// <summary>从 base 64 转换指定字节数组的指定区域。</summary>
    /// <returns>已计算的转换。</returns>
    /// <param name="inputBuffer">要从 Base 64 转换的输入。</param>
    /// <param name="inputOffset">字节数组中的偏移量，从该位置开始使用数据。</param>
    /// <param name="inputCount">字节数组中用作数据的字节数。</param>
    /// <exception cref="T:System.ObjectDisposedException">已释放当前的 <see cref="T:System.Security.Cryptography.FromBase64Transform" /> 对象。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="inputBuffer" /> 具有无效偏移量长度。- 或 -<paramref name="inputCount" /> 具有无效值。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="inputOffset" /> 超出范围。此参数需要非负数。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="inputBuffer" /> 为 null。</exception>
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
      if (this._inputBuffer == null)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_Generic"));
      byte[] numArray1 = new byte[inputCount];
      int byteCount;
      if (this._whitespaces == FromBase64TransformMode.IgnoreWhiteSpaces)
      {
        numArray1 = this.DiscardWhiteSpaces(inputBuffer, inputOffset, inputCount);
        byteCount = numArray1.Length;
      }
      else
      {
        Buffer.InternalBlockCopy((Array) inputBuffer, inputOffset, (Array) numArray1, 0, inputCount);
        byteCount = inputCount;
      }
      if (byteCount + this._inputIndex < 4)
      {
        this.Reset();
        return EmptyArray<byte>.Value;
      }
      int num = (byteCount + this._inputIndex) / 4;
      byte[] bytes = new byte[this._inputIndex + byteCount];
      Buffer.InternalBlockCopy((Array) this._inputBuffer, 0, (Array) bytes, 0, this._inputIndex);
      Buffer.InternalBlockCopy((Array) numArray1, 0, (Array) bytes, this._inputIndex, byteCount);
      this._inputIndex = (byteCount + this._inputIndex) % 4;
      Buffer.InternalBlockCopy((Array) numArray1, byteCount - this._inputIndex, (Array) this._inputBuffer, 0, this._inputIndex);
      byte[] numArray2 = Convert.FromBase64CharArray(Encoding.ASCII.GetChars(bytes, 0, 4 * num), 0, 4 * num);
      this.Reset();
      return numArray2;
    }

    private byte[] DiscardWhiteSpaces(byte[] inputBuffer, int inputOffset, int inputCount)
    {
      int num1 = 0;
      for (int index = 0; index < inputCount; ++index)
      {
        if (char.IsWhiteSpace((char) inputBuffer[inputOffset + index]))
          ++num1;
      }
      byte[] numArray = new byte[inputCount - num1];
      int num2 = 0;
      for (int index = 0; index < inputCount; ++index)
      {
        if (!char.IsWhiteSpace((char) inputBuffer[inputOffset + index]))
          numArray[num2++] = inputBuffer[inputOffset + index];
      }
      return numArray;
    }

    /// <summary>释放 <see cref="T:System.Security.Cryptography.FromBase64Transform" /> 类的当前实例所使用的所有资源。</summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    private void Reset()
    {
      this._inputIndex = 0;
    }

    /// <summary>释放由 <see cref="T:System.Security.Cryptography.FromBase64Transform" /> 使用的所有资源。</summary>
    public void Clear()
    {
      this.Dispose();
    }

    /// <summary>释放由 <see cref="T:System.Security.Cryptography.FromBase64Transform" /> 占用的非托管资源，还可以释放托管资源。</summary>
    /// <param name="disposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。</param>
    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      if (this._inputBuffer != null)
        Array.Clear((Array) this._inputBuffer, 0, this._inputBuffer.Length);
      this._inputBuffer = (byte[]) null;
      this._inputIndex = 0;
    }
  }
}
