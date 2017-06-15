// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.CryptoStream
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Security.Cryptography
{
  /// <summary>定义将数据流链接到加密转换的流。</summary>
  [ComVisible(true)]
  public class CryptoStream : Stream, IDisposable
  {
    private Stream _stream;
    private ICryptoTransform _Transform;
    private byte[] _InputBuffer;
    private int _InputBufferIndex;
    private int _InputBlockSize;
    private byte[] _OutputBuffer;
    private int _OutputBufferIndex;
    private int _OutputBlockSize;
    private CryptoStreamMode _transformMode;
    private bool _canRead;
    private bool _canWrite;
    private bool _finalBlockTransformed;

    /// <summary>获取一个值，该值指示当前的 <see cref="T:System.Security.Cryptography.CryptoStream" /> 是否可读。</summary>
    /// <returns>如果当前流可读，则为 true；否则为 false。</returns>
    public override bool CanRead
    {
      get
      {
        return this._canRead;
      }
    }

    /// <summary>获取一个值，该值指示你是否可以在当前 <see cref="T:System.Security.Cryptography.CryptoStream" /> 中搜索。</summary>
    /// <returns>总是为 false。</returns>
    public override bool CanSeek
    {
      get
      {
        return false;
      }
    }

    /// <summary>获取一个值，该值指示当前的 <see cref="T:System.Security.Cryptography.CryptoStream" /> 是否可写。</summary>
    /// <returns>如果当前流可写，则为 true；否则为 false。</returns>
    public override bool CanWrite
    {
      get
      {
        return this._canWrite;
      }
    }

    /// <summary>获取用字节表示的流长度。</summary>
    /// <returns>不支持此属性。</returns>
    /// <exception cref="T:System.NotSupportedException">不支持此属性。</exception>
    public override long Length
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
      }
    }

    /// <summary>获取或设置当前流中的位置。</summary>
    /// <returns>不支持此属性。</returns>
    /// <exception cref="T:System.NotSupportedException">不支持此属性。</exception>
    public override long Position
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
      }
      set
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
      }
    }

    /// <summary>获取一个值，该值指示最终缓冲区块是否已写入基础流。</summary>
    /// <returns>如果已刷新最终块，则为 true；否则为 false。</returns>
    public bool HasFlushedFinalBlock
    {
      get
      {
        return this._finalBlockTransformed;
      }
    }

    /// <summary>用目标数据流、要使用的转换和流的模式初始化 <see cref="T:System.Security.Cryptography.CryptoStream" /> 类的新实例。</summary>
    /// <param name="stream">对其执行加密转换的流。</param>
    /// <param name="transform">要对流执行的加密转换。</param>
    /// <param name="mode">
    /// <see cref="T:System.Security.Cryptography.CryptoStreamMode" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="stream" /> 不可读。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="stream" /> 不可写。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="stream" /> 无效。</exception>
    public CryptoStream(Stream stream, ICryptoTransform transform, CryptoStreamMode mode)
    {
      this._stream = stream;
      this._transformMode = mode;
      this._Transform = transform;
      switch (this._transformMode)
      {
        case CryptoStreamMode.Read:
          if (!this._stream.CanRead)
            throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotReadable"), "stream");
          this._canRead = true;
          break;
        case CryptoStreamMode.Write:
          if (!this._stream.CanWrite)
            throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotWritable"), "stream");
          this._canWrite = true;
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
      }
      this.InitializeBuffer();
    }

    /// <summary>用缓冲区的当前状态更新基础数据源或存储库，随后清除缓冲区。</summary>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">密钥已损坏，它可能会导致流的填充无效。</exception>
    /// <exception cref="T:System.NotSupportedException">当前流不可写。- 或 - 最后一个块已被转换。</exception>
    public void FlushFinalBlock()
    {
      if (this._finalBlockTransformed)
        throw new NotSupportedException(Environment.GetResourceString("Cryptography_CryptoStream_FlushFinalBlockTwice"));
      byte[] buffer = this._Transform.TransformFinalBlock(this._InputBuffer, 0, this._InputBufferIndex);
      this._finalBlockTransformed = true;
      if (this._canWrite && this._OutputBufferIndex > 0)
      {
        this._stream.Write(this._OutputBuffer, 0, this._OutputBufferIndex);
        this._OutputBufferIndex = 0;
      }
      if (this._canWrite)
        this._stream.Write(buffer, 0, buffer.Length);
      CryptoStream cryptoStream = this._stream as CryptoStream;
      if (cryptoStream != null)
      {
        if (!cryptoStream.HasFlushedFinalBlock)
          cryptoStream.FlushFinalBlock();
      }
      else
        this._stream.Flush();
      if (this._InputBuffer != null)
        Array.Clear((Array) this._InputBuffer, 0, this._InputBuffer.Length);
      if (this._OutputBuffer == null)
        return;
      Array.Clear((Array) this._OutputBuffer, 0, this._OutputBuffer.Length);
    }

    /// <summary>清理当前流的所有缓冲区，并使所有缓冲数据写入基础设备。</summary>
    public override void Flush()
    {
    }

    /// <summary>异步清理当前流的所有缓冲区，并使所有缓冲数据写入基础设备，并且监控取消请求。</summary>
    /// <returns>表示异步刷新操作的任务。</returns>
    /// <param name="cancellationToken">要监视取消请求的标记。默认值为 <see cref="P:System.Threading.CancellationToken.None" />。</param>
    /// <exception cref="T:System.ObjectDisposedException">流已被释放。</exception>
    public override Task FlushAsync(CancellationToken cancellationToken)
    {
      if (this.GetType() != typeof (CryptoStream))
        return base.FlushAsync(cancellationToken);
      if (!cancellationToken.IsCancellationRequested)
        return Task.CompletedTask;
      return Task.FromCancellation(cancellationToken);
    }

    /// <summary>设置当前流中的位置。</summary>
    /// <returns>不支持此方法。</returns>
    /// <param name="offset">相对于 <paramref name="origin" /> 参数的字节偏移量。</param>
    /// <param name="origin">一个 <see cref="T:System.IO.SeekOrigin" /> 对象，该对象指示用于获得新位置的参考点。</param>
    /// <exception cref="T:System.NotSupportedException">不支持此方法。</exception>
    public override long Seek(long offset, SeekOrigin origin)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
    }

    /// <summary>设置当前流的长度。</summary>
    /// <param name="value">所需的当前流的长度（以字节表示）。</param>
    /// <exception cref="T:System.NotSupportedException">此属性的存在只是为了支持从 <see cref="T:System.IO.Stream" /> 继承；无法使用此属性。</exception>
    public override void SetLength(long value)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
    }

    /// <summary>从当前流读取字节序列，并将流中的位置向前移动读取的字节数。</summary>
    /// <returns>读入缓冲区中的总字节数。如果当前可用的字节数没有请求的字节数那么多，则总字节数可能小于请求的字节数；如果已到达流的末尾，则为零。</returns>
    /// <param name="buffer">字节数组。从当前流中读取最多的 <paramref name="count" /> 个字节，并将它们存储在 <paramref name="buffer" /> 中。</param>
    /// <param name="offset">
    /// <paramref name="buffer" /> 中的字节偏移量，从该偏移量开始存储从当前流中读取的数据。</param>
    /// <param name="count">要从当前流中最多读取的字节数。</param>
    /// <exception cref="T:System.NotSupportedException">与当前 <see cref="T:System.Security.Cryptography.CryptoStreamMode" /> 对象关联的 <see cref="T:System.Security.Cryptography.CryptoStream" /> 与基础流不匹配。例如，对只写的基础流使用 <see cref="F:System.Security.Cryptography.CryptoStreamMode.Read" /> 时会引发此异常。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 参数小于零。- 或 - <paramref name="count" /> 参数小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="count" /> 参数和 <paramref name="offset" /> 参数的和比缓冲区的长度长。</exception>
    public override int Read([In, Out] byte[] buffer, int offset, int count)
    {
      if (!this.CanRead)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnreadableStream"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      int num1 = count;
      int dstOffsetBytes = offset;
      if (this._OutputBufferIndex != 0)
      {
        if (this._OutputBufferIndex <= count)
        {
          Buffer.InternalBlockCopy((Array) this._OutputBuffer, 0, (Array) buffer, offset, this._OutputBufferIndex);
          num1 -= this._OutputBufferIndex;
          dstOffsetBytes += this._OutputBufferIndex;
          this._OutputBufferIndex = 0;
        }
        else
        {
          Buffer.InternalBlockCopy((Array) this._OutputBuffer, 0, (Array) buffer, offset, count);
          Buffer.InternalBlockCopy((Array) this._OutputBuffer, count, (Array) this._OutputBuffer, 0, this._OutputBufferIndex - count);
          this._OutputBufferIndex = this._OutputBufferIndex - count;
          return count;
        }
      }
      if (this._finalBlockTransformed)
        return count - num1;
      if (num1 > this._OutputBlockSize && this._Transform.CanTransformMultipleBlocks)
      {
        int length = num1 / this._OutputBlockSize * this._InputBlockSize;
        byte[] numArray = new byte[length];
        Buffer.InternalBlockCopy((Array) this._InputBuffer, 0, (Array) numArray, 0, this._InputBufferIndex);
        int num2 = this._InputBufferIndex + this._stream.Read(numArray, this._InputBufferIndex, length - this._InputBufferIndex);
        this._InputBufferIndex = 0;
        if (num2 <= this._InputBlockSize)
        {
          this._InputBuffer = numArray;
          this._InputBufferIndex = num2;
        }
        else
        {
          int num3 = num2 / this._InputBlockSize * this._InputBlockSize;
          int byteCount1 = num2 - num3;
          if (byteCount1 != 0)
          {
            this._InputBufferIndex = byteCount1;
            Buffer.InternalBlockCopy((Array) numArray, num3, (Array) this._InputBuffer, 0, byteCount1);
          }
          byte[] outputBuffer = new byte[num3 / this._InputBlockSize * this._OutputBlockSize];
          int byteCount2 = this._Transform.TransformBlock(numArray, 0, num3, outputBuffer, 0);
          Buffer.InternalBlockCopy((Array) outputBuffer, 0, (Array) buffer, dstOffsetBytes, byteCount2);
          Array.Clear((Array) numArray, 0, numArray.Length);
          Array.Clear((Array) outputBuffer, 0, outputBuffer.Length);
          num1 -= byteCount2;
          dstOffsetBytes += byteCount2;
        }
      }
      while (num1 > 0)
      {
        int num2;
        for (; this._InputBufferIndex < this._InputBlockSize; this._InputBufferIndex = this._InputBufferIndex + num2)
        {
          num2 = this._stream.Read(this._InputBuffer, this._InputBufferIndex, this._InputBlockSize - this._InputBufferIndex);
          if (num2 == 0)
          {
            byte[] numArray = this._Transform.TransformFinalBlock(this._InputBuffer, 0, this._InputBufferIndex);
            this._OutputBuffer = numArray;
            this._OutputBufferIndex = numArray.Length;
            this._finalBlockTransformed = true;
            if (num1 < this._OutputBufferIndex)
            {
              Buffer.InternalBlockCopy((Array) this._OutputBuffer, 0, (Array) buffer, dstOffsetBytes, num1);
              this._OutputBufferIndex = this._OutputBufferIndex - num1;
              Buffer.InternalBlockCopy((Array) this._OutputBuffer, num1, (Array) this._OutputBuffer, 0, this._OutputBufferIndex);
              return count;
            }
            Buffer.InternalBlockCopy((Array) this._OutputBuffer, 0, (Array) buffer, dstOffsetBytes, this._OutputBufferIndex);
            int num3 = num1 - this._OutputBufferIndex;
            this._OutputBufferIndex = 0;
            return count - num3;
          }
        }
        int byteCount = this._Transform.TransformBlock(this._InputBuffer, 0, this._InputBlockSize, this._OutputBuffer, 0);
        this._InputBufferIndex = 0;
        if (num1 >= byteCount)
        {
          Buffer.InternalBlockCopy((Array) this._OutputBuffer, 0, (Array) buffer, dstOffsetBytes, byteCount);
          dstOffsetBytes += byteCount;
          num1 -= byteCount;
        }
        else
        {
          Buffer.InternalBlockCopy((Array) this._OutputBuffer, 0, (Array) buffer, dstOffsetBytes, num1);
          this._OutputBufferIndex = byteCount - num1;
          Buffer.InternalBlockCopy((Array) this._OutputBuffer, num1, (Array) this._OutputBuffer, 0, this._OutputBufferIndex);
          return count;
        }
      }
      return count;
    }

    /// <summary>从当前流异步读取字节序列，将此流中的位置提升读取的字节数，并监视取消请求数。</summary>
    /// <returns>表示异步读取操作的任务。目标对象的 <paramref name="TResult" /> 参数的值包含多次读入缓冲区总字节数。如果当前可用字节数少于所请求的字节数，则该结果值可能小于所请求的字节数，或者如果已到达流的末尾时，则为 0（零）。</returns>
    /// <param name="buffer">数据写入的缓冲区。</param>
    /// <param name="offset">
    /// <paramref name="buffer" /> 中的字节偏移量，从该偏移量开始写入从流中读取的数据。</param>
    /// <param name="count">最多读取的字节数。</param>
    /// <param name="cancellationToken">要监视取消请求的标记。默认值为 <see cref="P:System.Threading.CancellationToken.None" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="offset" /> 与 <paramref name="count" /> 的和大于缓冲区长度。</exception>
    /// <exception cref="T:System.NotSupportedException">流不支持读取。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">该流正在由其前一次读取操作使用。</exception>
    public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
      if (!this.CanRead)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnreadableStream"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.GetType() != typeof (CryptoStream))
        return base.ReadAsync(buffer, offset, count, cancellationToken);
      if (cancellationToken.IsCancellationRequested)
        return Task.FromCancellation<int>(cancellationToken);
      return this.ReadAsyncInternal(buffer, offset, count, cancellationToken);
    }

    private async Task<int> ReadAsyncInternal(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
      await new CryptoStream.HopToThreadPoolAwaitable();
      SemaphoreSlim sem = this.EnsureAsyncActiveSemaphoreInitialized();
      await sem.WaitAsync().ConfigureAwait(false);
      try
      {
        int bytesToDeliver = count;
        int currentOutputIndex = offset;
        if (this._OutputBufferIndex != 0)
        {
          if (this._OutputBufferIndex <= count)
          {
            Buffer.InternalBlockCopy((Array) this._OutputBuffer, 0, (Array) buffer, offset, this._OutputBufferIndex);
            bytesToDeliver -= this._OutputBufferIndex;
            currentOutputIndex += this._OutputBufferIndex;
            this._OutputBufferIndex = 0;
          }
          else
          {
            Buffer.InternalBlockCopy((Array) this._OutputBuffer, 0, (Array) buffer, offset, count);
            Buffer.InternalBlockCopy((Array) this._OutputBuffer, count, (Array) this._OutputBuffer, 0, this._OutputBufferIndex - count);
            this._OutputBufferIndex = this._OutputBufferIndex - count;
            return count;
          }
        }
        if (this._finalBlockTransformed)
          return count - bytesToDeliver;
        if (bytesToDeliver > this._OutputBlockSize && this._Transform.CanTransformMultipleBlocks)
        {
          int length = bytesToDeliver / this._OutputBlockSize * this._InputBlockSize;
          byte[] tempInputBuffer = new byte[length];
          Buffer.InternalBlockCopy((Array) this._InputBuffer, 0, (Array) tempInputBuffer, 0, this._InputBufferIndex);
          int num = this._InputBufferIndex;
          int num1 = await this._stream.ReadAsync(tempInputBuffer, this._InputBufferIndex, length - this._InputBufferIndex, cancellationToken).ConfigureAwait(false);
          int num2 = num + num1;
          this._InputBufferIndex = 0;
          if (num2 <= this._InputBlockSize)
          {
            this._InputBuffer = tempInputBuffer;
            this._InputBufferIndex = num2;
          }
          else
          {
            int num3 = num2 / this._InputBlockSize * this._InputBlockSize;
            int byteCount1 = num2 - num3;
            if (byteCount1 != 0)
            {
              this._InputBufferIndex = byteCount1;
              Buffer.InternalBlockCopy((Array) tempInputBuffer, num3, (Array) this._InputBuffer, 0, byteCount1);
            }
            byte[] outputBuffer = new byte[num3 / this._InputBlockSize * this._OutputBlockSize];
            int byteCount2 = this._Transform.TransformBlock(tempInputBuffer, 0, num3, outputBuffer, 0);
            Buffer.InternalBlockCopy((Array) outputBuffer, 0, (Array) buffer, currentOutputIndex, byteCount2);
            Array.Clear((Array) tempInputBuffer, 0, tempInputBuffer.Length);
            Array.Clear((Array) outputBuffer, 0, outputBuffer.Length);
            bytesToDeliver -= byteCount2;
            currentOutputIndex += byteCount2;
            tempInputBuffer = (byte[]) null;
          }
        }
        while (bytesToDeliver > 0)
        {
          int num;
          for (; this._InputBufferIndex < this._InputBlockSize; this._InputBufferIndex = this._InputBufferIndex + num)
          {
            num = await this._stream.ReadAsync(this._InputBuffer, this._InputBufferIndex, this._InputBlockSize - this._InputBufferIndex, cancellationToken).ConfigureAwait(false);
            if (num == 0)
            {
              byte[] numArray = this._Transform.TransformFinalBlock(this._InputBuffer, 0, this._InputBufferIndex);
              this._OutputBuffer = numArray;
              this._OutputBufferIndex = numArray.Length;
              this._finalBlockTransformed = true;
              if (bytesToDeliver < this._OutputBufferIndex)
              {
                Buffer.InternalBlockCopy((Array) this._OutputBuffer, 0, (Array) buffer, currentOutputIndex, bytesToDeliver);
                this._OutputBufferIndex = this._OutputBufferIndex - bytesToDeliver;
                Buffer.InternalBlockCopy((Array) this._OutputBuffer, bytesToDeliver, (Array) this._OutputBuffer, 0, this._OutputBufferIndex);
                return count;
              }
              Buffer.InternalBlockCopy((Array) this._OutputBuffer, 0, (Array) buffer, currentOutputIndex, this._OutputBufferIndex);
              bytesToDeliver -= this._OutputBufferIndex;
              this._OutputBufferIndex = 0;
              return count - bytesToDeliver;
            }
          }
          int byteCount = this._Transform.TransformBlock(this._InputBuffer, 0, this._InputBlockSize, this._OutputBuffer, 0);
          this._InputBufferIndex = 0;
          if (bytesToDeliver >= byteCount)
          {
            Buffer.InternalBlockCopy((Array) this._OutputBuffer, 0, (Array) buffer, currentOutputIndex, byteCount);
            currentOutputIndex += byteCount;
            bytesToDeliver -= byteCount;
          }
          else
          {
            Buffer.InternalBlockCopy((Array) this._OutputBuffer, 0, (Array) buffer, currentOutputIndex, bytesToDeliver);
            this._OutputBufferIndex = byteCount - bytesToDeliver;
            Buffer.InternalBlockCopy((Array) this._OutputBuffer, bytesToDeliver, (Array) this._OutputBuffer, 0, this._OutputBufferIndex);
            return count;
          }
        }
        return count;
      }
      finally
      {
        sem.Release();
      }
    }

    /// <summary>将一字节序列写入当前的 <see cref="T:System.Security.Cryptography.CryptoStream" />，并将通过写入的字节数提前该流的当前位置。</summary>
    /// <param name="buffer">字节数组。此方法将 <paramref name="count" /> 个字节从 <paramref name="buffer" /> 复制到当前流。</param>
    /// <param name="offset">
    /// <paramref name="buffer" /> 中的字节偏移量，从此偏移量开始将字节复制到当前流。</param>
    /// <param name="count">要写入当前流的字节数。</param>
    /// <exception cref="T:System.NotSupportedException">与当前 <see cref="T:System.Security.Cryptography.CryptoStreamMode" /> 对象关联的 <see cref="T:System.Security.Cryptography.CryptoStream" /> 与基础流不匹配。例如，对只读的基础流使用 <see cref="F:System.Security.Cryptography.CryptoStreamMode.Write" /> 时会引发此异常。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 参数小于零。- 或 - <paramref name="count" /> 参数小于零。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="count" /> 参数和 <paramref name="offset" /> 参数的和比缓冲区的长度长。</exception>
    public override void Write(byte[] buffer, int offset, int count)
    {
      if (!this.CanWrite)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnwritableStream"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      int byteCount = count;
      int num1 = offset;
      if (this._InputBufferIndex > 0)
      {
        if (count >= this._InputBlockSize - this._InputBufferIndex)
        {
          Buffer.InternalBlockCopy((Array) buffer, offset, (Array) this._InputBuffer, this._InputBufferIndex, this._InputBlockSize - this._InputBufferIndex);
          num1 += this._InputBlockSize - this._InputBufferIndex;
          byteCount -= this._InputBlockSize - this._InputBufferIndex;
          this._InputBufferIndex = this._InputBlockSize;
        }
        else
        {
          Buffer.InternalBlockCopy((Array) buffer, offset, (Array) this._InputBuffer, this._InputBufferIndex, count);
          this._InputBufferIndex = this._InputBufferIndex + count;
          return;
        }
      }
      if (this._OutputBufferIndex > 0)
      {
        this._stream.Write(this._OutputBuffer, 0, this._OutputBufferIndex);
        this._OutputBufferIndex = 0;
      }
      if (this._InputBufferIndex == this._InputBlockSize)
      {
        this._stream.Write(this._OutputBuffer, 0, this._Transform.TransformBlock(this._InputBuffer, 0, this._InputBlockSize, this._OutputBuffer, 0));
        this._InputBufferIndex = 0;
      }
      while (byteCount > 0)
      {
        if (byteCount >= this._InputBlockSize)
        {
          if (this._Transform.CanTransformMultipleBlocks)
          {
            int num2 = byteCount / this._InputBlockSize;
            int num3 = this._InputBlockSize;
            int inputCount = num2 * num3;
            int num4 = this._OutputBlockSize;
            byte[] numArray = new byte[num2 * num4];
            int count1 = this._Transform.TransformBlock(buffer, num1, inputCount, numArray, 0);
            this._stream.Write(numArray, 0, count1);
            num1 += inputCount;
            byteCount -= inputCount;
          }
          else
          {
            this._stream.Write(this._OutputBuffer, 0, this._Transform.TransformBlock(buffer, num1, this._InputBlockSize, this._OutputBuffer, 0));
            num1 += this._InputBlockSize;
            byteCount -= this._InputBlockSize;
          }
        }
        else
        {
          Buffer.InternalBlockCopy((Array) buffer, num1, (Array) this._InputBuffer, 0, byteCount);
          this._InputBufferIndex = this._InputBufferIndex + byteCount;
          break;
        }
      }
    }

    /// <summary>将字节序列异步写入当前流，将该流的当前位置向前移动写入的字节数，并监视取消请求。</summary>
    /// <returns>表示异步写入操作的任务。</returns>
    /// <param name="buffer">从中写入数据的缓冲区。</param>
    /// <param name="offset">
    /// <paramref name="buffer" /> 中的从零开始的字节偏移量，从此处开始将字节写入到该流。</param>
    /// <param name="count">最多写入的字节数。</param>
    /// <param name="cancellationToken">要监视取消请求的标记。默认值为 <see cref="P:System.Threading.CancellationToken.None" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="buffer" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="count" /> 为负。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="offset" /> 与 <paramref name="count" /> 的和大于缓冲区长度。</exception>
    /// <exception cref="T:System.NotSupportedException">流不支持写入。</exception>
    /// <exception cref="T:System.ObjectDisposedException">流已被释放。</exception>
    /// <exception cref="T:System.InvalidOperationException">该流正在由其前一次写入操作使用。</exception>
    public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
      if (!this.CanWrite)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnwritableStream"));
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.GetType() != typeof (CryptoStream))
        return base.WriteAsync(buffer, offset, count, cancellationToken);
      if (cancellationToken.IsCancellationRequested)
        return Task.FromCancellation(cancellationToken);
      return this.WriteAsyncInternal(buffer, offset, count, cancellationToken);
    }

    private async Task WriteAsyncInternal(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
      await new CryptoStream.HopToThreadPoolAwaitable();
      SemaphoreSlim sem = this.EnsureAsyncActiveSemaphoreInitialized();
      await sem.WaitAsync().ConfigureAwait(false);
      try
      {
        int bytesToWrite = count;
        int currentInputIndex = offset;
        if (this._InputBufferIndex > 0)
        {
          if (count >= this._InputBlockSize - this._InputBufferIndex)
          {
            Buffer.InternalBlockCopy((Array) buffer, offset, (Array) this._InputBuffer, this._InputBufferIndex, this._InputBlockSize - this._InputBufferIndex);
            currentInputIndex += this._InputBlockSize - this._InputBufferIndex;
            bytesToWrite -= this._InputBlockSize - this._InputBufferIndex;
            this._InputBufferIndex = this._InputBlockSize;
          }
          else
          {
            Buffer.InternalBlockCopy((Array) buffer, offset, (Array) this._InputBuffer, this._InputBufferIndex, count);
            this._InputBufferIndex = this._InputBufferIndex + count;
            return;
          }
        }
        if (this._OutputBufferIndex > 0)
        {
          await this._stream.WriteAsync(this._OutputBuffer, 0, this._OutputBufferIndex, cancellationToken).ConfigureAwait(false);
          this._OutputBufferIndex = 0;
        }
        if (this._InputBufferIndex == this._InputBlockSize)
        {
          await this._stream.WriteAsync(this._OutputBuffer, 0, this._Transform.TransformBlock(this._InputBuffer, 0, this._InputBlockSize, this._OutputBuffer, 0), cancellationToken).ConfigureAwait(false);
          this._InputBufferIndex = 0;
        }
        while (bytesToWrite > 0)
        {
          if (bytesToWrite >= this._InputBlockSize)
          {
            if (this._Transform.CanTransformMultipleBlocks)
            {
              int num = bytesToWrite / this._InputBlockSize;
              int numWholeBlocksInBytes = num * this._InputBlockSize;
              byte[] numArray = new byte[num * this._OutputBlockSize];
              int count1 = this._Transform.TransformBlock(buffer, currentInputIndex, numWholeBlocksInBytes, numArray, 0);
              await this._stream.WriteAsync(numArray, 0, count1, cancellationToken).ConfigureAwait(false);
              currentInputIndex += numWholeBlocksInBytes;
              bytesToWrite -= numWholeBlocksInBytes;
            }
            else
            {
              await this._stream.WriteAsync(this._OutputBuffer, 0, this._Transform.TransformBlock(buffer, currentInputIndex, this._InputBlockSize, this._OutputBuffer, 0), cancellationToken).ConfigureAwait(false);
              currentInputIndex += this._InputBlockSize;
              bytesToWrite -= this._InputBlockSize;
            }
          }
          else
          {
            Buffer.InternalBlockCopy((Array) buffer, currentInputIndex, (Array) this._InputBuffer, 0, bytesToWrite);
            this._InputBufferIndex = this._InputBufferIndex + bytesToWrite;
            break;
          }
        }
      }
      finally
      {
        sem.Release();
      }
    }

    /// <summary>释放由 <see cref="T:System.Security.Cryptography.CryptoStream" /> 使用的所有资源。</summary>
    public void Clear()
    {
      this.Close();
    }

    /// <summary>释放由 <see cref="T:System.Security.Cryptography.CryptoStream" /> 占用的非托管资源，还可以释放托管资源。</summary>
    /// <param name="disposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。</param>
    protected override void Dispose(bool disposing)
    {
      try
      {
        if (!disposing)
          return;
        if (!this._finalBlockTransformed)
          this.FlushFinalBlock();
        this._stream.Close();
      }
      finally
      {
        try
        {
          this._finalBlockTransformed = true;
          if (this._InputBuffer != null)
            Array.Clear((Array) this._InputBuffer, 0, this._InputBuffer.Length);
          if (this._OutputBuffer != null)
            Array.Clear((Array) this._OutputBuffer, 0, this._OutputBuffer.Length);
          this._InputBuffer = (byte[]) null;
          this._OutputBuffer = (byte[]) null;
          this._canRead = false;
          this._canWrite = false;
        }
        finally
        {
          base.Dispose(disposing);
        }
      }
    }

    private void InitializeBuffer()
    {
      if (this._Transform == null)
        return;
      this._InputBlockSize = this._Transform.InputBlockSize;
      this._InputBuffer = new byte[this._InputBlockSize];
      this._OutputBlockSize = this._Transform.OutputBlockSize;
      this._OutputBuffer = new byte[this._OutputBlockSize];
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct HopToThreadPoolAwaitable : INotifyCompletion
    {
      public bool IsCompleted
      {
        get
        {
          return false;
        }
      }

      public CryptoStream.HopToThreadPoolAwaitable GetAwaiter()
      {
        return this;
      }

      public void OnCompleted(Action continuation)
      {
        Task.Run(continuation);
      }

      public void GetResult()
      {
      }
    }
  }
}
