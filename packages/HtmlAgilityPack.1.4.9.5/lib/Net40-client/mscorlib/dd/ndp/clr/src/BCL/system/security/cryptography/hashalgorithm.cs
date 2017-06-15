// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.HashAlgorithm
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>表示所有加密哈希算法实现均必须从中派生的基类。</summary>
  [ComVisible(true)]
  public abstract class HashAlgorithm : IDisposable, ICryptoTransform
  {
    /// <summary>表示计算所得的哈希代码的大小（以位为单位）。</summary>
    protected int HashSizeValue;
    /// <summary>表示计算所得的哈希代码的值。</summary>
    protected internal byte[] HashValue;
    /// <summary>表示哈希计算的状态。</summary>
    protected int State;
    private bool m_bDisposed;

    /// <summary>获取计算所得的哈希代码的大小（以位为单位）。</summary>
    /// <returns>计算所得的哈希代码的大小（以位为单位）。</returns>
    public virtual int HashSize
    {
      get
      {
        return this.HashSizeValue;
      }
    }

    /// <summary>获取计算所得的哈希代码的值。</summary>
    /// <returns>计算所得的哈希代码的当前值。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">
    /// <see cref="F:System.Security.Cryptography.HashAlgorithm.HashValue" /> 为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">对象已被释放。</exception>
    public virtual byte[] Hash
    {
      get
      {
        if (this.m_bDisposed)
          throw new ObjectDisposedException((string) null);
        if (this.State != 0)
          throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_HashNotYetFinalized"));
        return (byte[]) this.HashValue.Clone();
      }
    }

    /// <summary>当在派生类中重写时，获取输入块的大小。</summary>
    /// <returns>输入块的大小。</returns>
    public virtual int InputBlockSize
    {
      get
      {
        return 1;
      }
    }

    /// <summary>当在派生类中重写时，获取输出块的大小。</summary>
    /// <returns>输出块的大小。</returns>
    public virtual int OutputBlockSize
    {
      get
      {
        return 1;
      }
    }

    /// <summary>当在派生类中重写时，获取一个值，该值指示是否可以转换多个块。</summary>
    /// <returns>如果可以转换多个块，则为 true；否则，为 false。</returns>
    public virtual bool CanTransformMultipleBlocks
    {
      get
      {
        return true;
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

    /// <summary>创建哈希算法的默认实现的实例。</summary>
    /// <returns>新的 <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" /> 实例，除非已使用 &lt;cryptoClass&gt; 元素更改默认设置。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static HashAlgorithm Create()
    {
      return HashAlgorithm.Create("System.Security.Cryptography.HashAlgorithm");
    }

    /// <summary>创建哈希算法的指定实现的实例。</summary>
    /// <returns>指定哈希算法的新实例，如果 <paramref name="hashName" /> 不是有效哈希算法，则为 null。</returns>
    /// <param name="hashName">要使用的哈希算法的实现。下表显示 <paramref name="hashName" /> 参数的有效值以及它们映射到的算法。参数值 Implements SHA <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" />SHA1 <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" />System.Security.Cryptography.SHA1 <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" />System.Security.Cryptography.HashAlgorithm <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" />MD5 <see cref="T:System.Security.Cryptography.MD5CryptoServiceProvider" />System.Security.Cryptography.MD5 <see cref="T:System.Security.Cryptography.MD5CryptoServiceProvider" />SHA256 <see cref="T:System.Security.Cryptography.SHA256Managed" />SHA-256 <see cref="T:System.Security.Cryptography.SHA256Managed" />System.Security.Cryptography.SHA256 <see cref="T:System.Security.Cryptography.SHA256Managed" />SHA384 <see cref="T:System.Security.Cryptography.SHA384Managed" />SHA-384 <see cref="T:System.Security.Cryptography.SHA384Managed" />System.Security.Cryptography.SHA384 <see cref="T:System.Security.Cryptography.SHA384Managed" />SHA512 <see cref="T:System.Security.Cryptography.SHA512Managed" />SHA 512 <see cref="T:System.Security.Cryptography.SHA512Managed" />System.Security.Cryptography.SHA512 <see cref="T:System.Security.Cryptography.SHA512Managed" /></param>
    public static HashAlgorithm Create(string hashName)
    {
      return (HashAlgorithm) CryptoConfig.CreateFromName(hashName);
    }

    /// <summary>计算指定 <see cref="T:System.IO.Stream" /> 对象的哈希值。</summary>
    /// <returns>计算所得的哈希代码。</returns>
    /// <param name="inputStream">要计算其哈希代码的输入。</param>
    /// <exception cref="T:System.ObjectDisposedException">对象已被释放。</exception>
    public byte[] ComputeHash(Stream inputStream)
    {
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null);
      byte[] numArray1 = new byte[4096];
      int cbSize;
      do
      {
        cbSize = inputStream.Read(numArray1, 0, 4096);
        if (cbSize > 0)
          this.HashCore(numArray1, 0, cbSize);
      }
      while (cbSize > 0);
      this.HashValue = this.HashFinal();
      byte[] numArray2 = (byte[]) this.HashValue.Clone();
      this.Initialize();
      return numArray2;
    }

    /// <summary>计算指定字节数组的哈希值。</summary>
    /// <returns>计算所得的哈希代码。</returns>
    /// <param name="buffer">要计算其哈希代码的输入。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ObjectDisposedException">对象已被释放。</exception>
    public byte[] ComputeHash(byte[] buffer)
    {
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null);
      if (buffer == null)
        throw new ArgumentNullException("buffer");
      this.HashCore(buffer, 0, buffer.Length);
      this.HashValue = this.HashFinal();
      byte[] numArray = (byte[]) this.HashValue.Clone();
      this.Initialize();
      return numArray;
    }

    /// <summary>计算指定字节数组的指定区域的哈希值。</summary>
    /// <returns>计算所得的哈希代码。</returns>
    /// <param name="buffer">要计算其哈希代码的输入。</param>
    /// <param name="offset">字节数组中的偏移量，从该位置开始使用数据。</param>
    /// <param name="count">数组中用作数据的字节数。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="count" /> 是无效值。- 或 -<paramref name="buffer" /> 长度无效。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 超出范围。此参数需要非负数。</exception>
    /// <exception cref="T:System.ObjectDisposedException">对象已被释放。</exception>
    public byte[] ComputeHash(byte[] buffer, int offset, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException("buffer");
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0 || count > buffer.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
      if (buffer.Length - count < offset)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null);
      this.HashCore(buffer, offset, count);
      this.HashValue = this.HashFinal();
      byte[] numArray = (byte[]) this.HashValue.Clone();
      this.Initialize();
      return numArray;
    }

    /// <summary>计算输入字节数组指定区域的哈希值，并将输入字节数组指定区域复制到输出字节数组的指定区域。</summary>
    /// <returns>写入的字节数。</returns>
    /// <param name="inputBuffer">要计算其哈希代码的输入。</param>
    /// <param name="inputOffset">输入字节数组中的偏移量，从该位置开始使用数据。</param>
    /// <param name="inputCount">输入字节数组中用作数据的字节数。</param>
    /// <param name="outputBuffer">用于计算哈希代码的部分输入数组的副本。</param>
    /// <param name="outputOffset">输入字节数组中的偏移量，从该位置开始使用数据。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="inputCount" /> 使用了无效值。- 或 -<paramref name="inputBuffer" /> 具有无效的长度。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="inputBuffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="inputOffset" /> 超出范围。此参数需要非负数。</exception>
    /// <exception cref="T:System.ObjectDisposedException">对象已被释放。</exception>
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
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null);
      this.State = 1;
      this.HashCore(inputBuffer, inputOffset, inputCount);
      if (outputBuffer != null && (inputBuffer != outputBuffer || inputOffset != outputOffset))
        Buffer.BlockCopy((Array) inputBuffer, inputOffset, (Array) outputBuffer, outputOffset, inputCount);
      return inputCount;
    }

    /// <summary>计算指定字节数组的指定区域的哈希值。</summary>
    /// <returns>一个数组，该数组是输入中计算了哈希值的部分的副本。</returns>
    /// <param name="inputBuffer">要计算其哈希代码的输入。</param>
    /// <param name="inputOffset">字节数组中的偏移量，从该位置开始使用数据。</param>
    /// <param name="inputCount">字节数组中用作数据的字节数。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="inputCount" /> 使用了无效值。- 或 -<paramref name="inputBuffer" /> 具有无效偏移量长度。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="inputBuffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="inputOffset" /> 超出范围。此参数需要非负数。</exception>
    /// <exception cref="T:System.ObjectDisposedException">对象已被释放。</exception>
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
      if (this.m_bDisposed)
        throw new ObjectDisposedException((string) null);
      this.HashCore(inputBuffer, inputOffset, inputCount);
      this.HashValue = this.HashFinal();
      byte[] numArray;
      if (inputCount != 0)
      {
        numArray = new byte[inputCount];
        Buffer.InternalBlockCopy((Array) inputBuffer, inputOffset, (Array) numArray, 0, inputCount);
      }
      else
        numArray = EmptyArray<byte>.Value;
      this.State = 0;
      return numArray;
    }

    /// <summary>释放 <see cref="T:System.Security.Cryptography.HashAlgorithm" /> 类的当前实例所使用的所有资源。</summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>释放 <see cref="T:System.Security.Cryptography.HashAlgorithm" /> 类使用的所有资源。</summary>
    public void Clear()
    {
      this.Dispose();
    }

    /// <summary>释放由 <see cref="T:System.Security.Cryptography.HashAlgorithm" /> 占用的非托管资源，还可以释放托管资源。</summary>
    /// <param name="disposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。</param>
    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      if (this.HashValue != null)
        Array.Clear((Array) this.HashValue, 0, this.HashValue.Length);
      this.HashValue = (byte[]) null;
      this.m_bDisposed = true;
    }

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.HashAlgorithm" /> 类的实现。</summary>
    public abstract void Initialize();

    /// <summary>当在派生类中重写时，将写入对象的数据路由到哈希算法以计算哈希值。</summary>
    /// <param name="array">要计算其哈希代码的输入。</param>
    /// <param name="ibStart">字节数组中的偏移量，从该位置开始使用数据。</param>
    /// <param name="cbSize">字节数组中用作数据的字节数。</param>
    protected abstract void HashCore(byte[] array, int ibStart, int cbSize);

    /// <summary>当在派生类中重写时，在加密流对象处理完最后的数据后完成哈希计算。</summary>
    /// <returns>计算所得的哈希代码。</returns>
    protected abstract byte[] HashFinal();
  }
}
