// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.CryptoAPITransform
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
  /// <summary>执行数据的加密转换。此类不能被继承。</summary>
  [ComVisible(true)]
  public sealed class CryptoAPITransform : ICryptoTransform, IDisposable
  {
    private int BlockSizeValue;
    private byte[] IVValue;
    private CipherMode ModeValue;
    private PaddingMode PaddingValue;
    private CryptoAPITransformMode encryptOrDecrypt;
    private byte[] _rgbKey;
    private byte[] _depadBuffer;
    [SecurityCritical]
    private SafeKeyHandle _safeKeyHandle;
    [SecurityCritical]
    private SafeProvHandle _safeProvHandle;

    /// <summary>获取密钥句柄。</summary>
    /// <returns>密钥句柄。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public IntPtr KeyHandle
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this._safeKeyHandle.DangerousGetHandle();
      }
    }

    /// <summary>获取输入块大小。</summary>
    /// <returns>输入块的大小（以字节为单位）。</returns>
    public int InputBlockSize
    {
      get
      {
        return this.BlockSizeValue / 8;
      }
    }

    /// <summary>获取输出块大小。</summary>
    /// <returns>输出块的大小（以字节为单位）。</returns>
    public int OutputBlockSize
    {
      get
      {
        return this.BlockSizeValue / 8;
      }
    }

    /// <summary>获取一个值，该值指示是否可以转换多个块。</summary>
    /// <returns>如果可以转换多个块，则为 true；否则，为 false。</returns>
    public bool CanTransformMultipleBlocks
    {
      get
      {
        return true;
      }
    }

    /// <summary>获取一个值，该值指示是否可重复使用当前转换。</summary>
    /// <returns>始终为 true。</returns>
    public bool CanReuseTransform
    {
      get
      {
        return true;
      }
    }

    private CryptoAPITransform()
    {
    }

    [SecurityCritical]
    internal CryptoAPITransform(int algid, int cArgs, int[] rgArgIds, object[] rgArgValues, byte[] rgbKey, PaddingMode padding, CipherMode cipherChainingMode, int blockSize, int feedbackSize, bool useSalt, CryptoAPITransformMode encDecMode)
    {
      this.BlockSizeValue = blockSize;
      this.ModeValue = cipherChainingMode;
      this.PaddingValue = padding;
      this.encryptOrDecrypt = encDecMode;
      int[] numArray1 = new int[rgArgIds.Length];
      Array.Copy((Array) rgArgIds, (Array) numArray1, rgArgIds.Length);
      this._rgbKey = new byte[rgbKey.Length];
      Array.Copy((Array) rgbKey, (Array) this._rgbKey, rgbKey.Length);
      object[] objArray = new object[rgArgValues.Length];
      for (int index = 0; index < rgArgValues.Length; ++index)
      {
        if (rgArgValues[index] is byte[])
        {
          byte[] numArray2 = (byte[]) rgArgValues[index];
          byte[] numArray3 = new byte[numArray2.Length];
          Array.Copy((Array) numArray2, (Array) numArray3, numArray2.Length);
          objArray[index] = (object) numArray3;
        }
        else if (rgArgValues[index] is int)
          objArray[index] = (object) (int) rgArgValues[index];
        else if (rgArgValues[index] is CipherMode)
          objArray[index] = (object) (int) rgArgValues[index];
      }
      this._safeProvHandle = Utils.AcquireProvHandle(new CspParameters(24));
      SafeKeyHandle invalidHandle = SafeKeyHandle.InvalidHandle;
      Utils._ImportBulkKey(this._safeProvHandle, algid, useSalt, this._rgbKey, ref invalidHandle);
      this._safeKeyHandle = invalidHandle;
      for (int index = 0; index < cArgs; ++index)
      {
        int dwValue;
        switch (rgArgIds[index])
        {
          case 1:
            this.IVValue = (byte[]) objArray[index];
            byte[] numArray2 = this.IVValue;
            SafeKeyHandle hKey = this._safeKeyHandle;
            int num = numArray1[index];
            byte[] numArray3 = numArray2;
            int length = numArray3.Length;
            Utils.SetKeyParamRgb(hKey, num, numArray3, length);
            continue;
          case 4:
            this.ModeValue = (CipherMode) objArray[index];
            dwValue = (int) objArray[index];
            break;
          case 5:
            dwValue = (int) objArray[index];
            break;
          case 19:
            dwValue = (int) objArray[index];
            break;
          default:
            throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeyParameter"), "_rgArgIds[i]");
        }
        Utils.SetKeyParamDw(this._safeKeyHandle, numArray1[index], dwValue);
      }
    }

    /// <summary>释放由 <see cref="T:System.Security.Cryptography.CryptoAPITransform" /> 类的当前实例占用的所有资源。</summary>
    public void Dispose()
    {
      this.Clear();
    }

    /// <summary>释放由 <see cref="T:System.Security.Cryptography.CryptoAPITransform" /> 方法使用的所有资源。</summary>
    [SecuritySafeCritical]
    public void Clear()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    [SecurityCritical]
    private void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      if (this._rgbKey != null)
      {
        Array.Clear((Array) this._rgbKey, 0, this._rgbKey.Length);
        this._rgbKey = (byte[]) null;
      }
      if (this.IVValue != null)
      {
        Array.Clear((Array) this.IVValue, 0, this.IVValue.Length);
        this.IVValue = (byte[]) null;
      }
      if (this._depadBuffer != null)
      {
        Array.Clear((Array) this._depadBuffer, 0, this._depadBuffer.Length);
        this._depadBuffer = (byte[]) null;
      }
      if (this._safeKeyHandle != null && !this._safeKeyHandle.IsClosed)
        this._safeKeyHandle.Dispose();
      if (this._safeProvHandle == null || this._safeProvHandle.IsClosed)
        return;
      this._safeProvHandle.Dispose();
    }

    /// <summary>重置 <see cref="T:System.Security.Cryptography.CryptoAPITransform" /> 的内部状态，以便它可再次用于不同的加密或解密。</summary>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public void Reset()
    {
      this._depadBuffer = (byte[]) null;
      byte[] outputBuffer = (byte[]) null;
      Utils._EncryptData(this._safeKeyHandle, EmptyArray<byte>.Value, 0, 0, ref outputBuffer, 0, this.PaddingValue, true);
    }

    /// <summary>计算输入字节数组的指定区域的转换，并将所得到的转换复制到输出字节数组的指定区域。</summary>
    /// <returns>写入的字节数。</returns>
    /// <param name="inputBuffer">对其执行操作的输入。</param>
    /// <param name="inputOffset">输入字节数组中的偏移量，从该位置开始使用数据。</param>
    /// <param name="inputCount">输入字节数组中用作数据的字节数。</param>
    /// <param name="outputBuffer">将数据写入的输出。</param>
    /// <param name="outputOffset">输出字节数组中的偏移量，从该位置开始写入数据。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="inputBuffer" /> 参数为 null。- 或 -<paramref name="outputBuffer" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">输入缓冲区的长度小于输入偏移量和输入计数之和。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="inputOffset" /> 超出范围。此参数需要非负数。</exception>
    [SecuritySafeCritical]
    public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
    {
      if (inputBuffer == null)
        throw new ArgumentNullException("inputBuffer");
      if (outputBuffer == null)
        throw new ArgumentNullException("outputBuffer");
      if (inputOffset < 0)
        throw new ArgumentOutOfRangeException("inputOffset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (inputCount <= 0 || inputCount % this.InputBlockSize != 0 || inputCount > inputBuffer.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
      if (inputBuffer.Length - inputCount < inputOffset)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (this.encryptOrDecrypt == CryptoAPITransformMode.Encrypt)
        return Utils._EncryptData(this._safeKeyHandle, inputBuffer, inputOffset, inputCount, ref outputBuffer, outputOffset, this.PaddingValue, false);
      if (this.PaddingValue == PaddingMode.Zeros || this.PaddingValue == PaddingMode.None)
        return Utils._DecryptData(this._safeKeyHandle, inputBuffer, inputOffset, inputCount, ref outputBuffer, outputOffset, this.PaddingValue, false);
      if (this._depadBuffer == null)
      {
        this._depadBuffer = new byte[this.InputBlockSize];
        int cb = inputCount - this.InputBlockSize;
        Buffer.InternalBlockCopy((Array) inputBuffer, inputOffset + cb, (Array) this._depadBuffer, 0, this.InputBlockSize);
        return Utils._DecryptData(this._safeKeyHandle, inputBuffer, inputOffset, cb, ref outputBuffer, outputOffset, this.PaddingValue, false);
      }
      Utils._DecryptData(this._safeKeyHandle, this._depadBuffer, 0, this._depadBuffer.Length, ref outputBuffer, outputOffset, this.PaddingValue, false);
      outputOffset += this.OutputBlockSize;
      int cb1 = inputCount - this.InputBlockSize;
      Buffer.InternalBlockCopy((Array) inputBuffer, inputOffset + cb1, (Array) this._depadBuffer, 0, this.InputBlockSize);
      return this.OutputBlockSize + Utils._DecryptData(this._safeKeyHandle, inputBuffer, inputOffset, cb1, ref outputBuffer, outputOffset, this.PaddingValue, false);
    }

    /// <summary>计算指定字节数组的指定区域的转换。</summary>
    /// <returns>计算所得的转换。</returns>
    /// <param name="inputBuffer">对其执行操作的输入。</param>
    /// <param name="inputOffset">字节数组中的偏移量，从该位置开始使用数据。</param>
    /// <param name="inputCount">字节数组中用作数据的字节数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="inputBuffer" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="inputOffset" /> 参数小于零。- 或 -<paramref name="inputCount" /> 参数小于零。- 或 -输入缓冲区的长度小于输入偏移量和输入计数之和。</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">
    /// <see cref="F:System.Security.Cryptography.PaddingMode.PKCS7" /> 填充无效。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="inputOffset" /> 参数超出范围。此参数需要非负数。</exception>
    [SecuritySafeCritical]
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
      if (this.encryptOrDecrypt == CryptoAPITransformMode.Encrypt)
      {
        byte[] outputBuffer = (byte[]) null;
        Utils._EncryptData(this._safeKeyHandle, inputBuffer, inputOffset, inputCount, ref outputBuffer, 0, this.PaddingValue, true);
        this.Reset();
        return outputBuffer;
      }
      if (inputCount % this.InputBlockSize != 0)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_SSD_InvalidDataSize"));
      if (this._depadBuffer == null)
      {
        byte[] outputBuffer = (byte[]) null;
        Utils._DecryptData(this._safeKeyHandle, inputBuffer, inputOffset, inputCount, ref outputBuffer, 0, this.PaddingValue, true);
        this.Reset();
        return outputBuffer;
      }
      byte[] data = new byte[this._depadBuffer.Length + inputCount];
      Buffer.InternalBlockCopy((Array) this._depadBuffer, 0, (Array) data, 0, this._depadBuffer.Length);
      Buffer.InternalBlockCopy((Array) inputBuffer, inputOffset, (Array) data, this._depadBuffer.Length, inputCount);
      byte[] outputBuffer1 = (byte[]) null;
      Utils._DecryptData(this._safeKeyHandle, data, 0, data.Length, ref outputBuffer1, 0, this.PaddingValue, true);
      this.Reset();
      return outputBuffer1;
    }
  }
}
