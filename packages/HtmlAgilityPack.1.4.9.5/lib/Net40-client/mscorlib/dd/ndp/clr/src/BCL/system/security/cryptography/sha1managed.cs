// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.SHA1Managed
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>使用托管库计算输入数据的 <see cref="T:System.Security.Cryptography.SHA1" /> 哈希值。</summary>
  [ComVisible(true)]
  public class SHA1Managed : SHA1
  {
    private byte[] _buffer;
    private long _count;
    private uint[] _stateSHA1;
    private uint[] _expandedBuffer;

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.SHA1Managed" /> 类的新实例。</summary>
    /// <exception cref="T:System.InvalidOperationException">此类不符合 FIPS 算法。</exception>
    public SHA1Managed()
    {
      if (CryptoConfig.AllowOnlyFipsAlgorithms)
        throw new InvalidOperationException(Environment.GetResourceString("Cryptography_NonCompliantFIPSAlgorithm"));
      this._stateSHA1 = new uint[5];
      this._buffer = new byte[64];
      this._expandedBuffer = new uint[80];
      this.InitializeState();
    }

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.SHA1Managed" /> 的实例。</summary>
    public override void Initialize()
    {
      this.InitializeState();
      Array.Clear((Array) this._buffer, 0, this._buffer.Length);
      Array.Clear((Array) this._expandedBuffer, 0, this._expandedBuffer.Length);
    }

    /// <summary>将写入对象的数据路由到 <see cref="T:System.Security.Cryptography.SHA1Managed" /> 哈希算法以计算哈希值。</summary>
    /// <param name="rgb">输入数据。</param>
    /// <param name="ibStart">字节数组中的偏移量，从该位置开始使用数据。</param>
    /// <param name="cbSize">数组中用作数据的字节数。</param>
    protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
    {
      this._HashData(rgb, ibStart, cbSize);
    }

    /// <summary>将所有数据都写入对象后，返回计算所得的 <see cref="T:System.Security.Cryptography.SHA1" /> 哈希值。</summary>
    /// <returns>计算所得的哈希代码。</returns>
    protected override byte[] HashFinal()
    {
      return this._EndHash();
    }

    private void InitializeState()
    {
      this._count = 0L;
      this._stateSHA1[0] = 1732584193U;
      this._stateSHA1[1] = 4023233417U;
      this._stateSHA1[2] = 2562383102U;
      this._stateSHA1[3] = 271733878U;
      this._stateSHA1[4] = 3285377520U;
    }

    [SecuritySafeCritical]
    private unsafe void _HashData(byte[] partIn, int ibStart, int cbSize)
    {
      int byteCount = cbSize;
      int srcOffsetBytes = ibStart;
      int dstOffsetBytes = (int) (this._count & 63L);
      this._count = this._count + (long) byteCount;
      fixed (uint* state = this._stateSHA1)
        fixed (byte* block = this._buffer)
          fixed (uint* expandedBuffer = this._expandedBuffer)
          {
            if (dstOffsetBytes > 0 && dstOffsetBytes + byteCount >= 64)
            {
              Buffer.InternalBlockCopy((Array) partIn, srcOffsetBytes, (Array) this._buffer, dstOffsetBytes, 64 - dstOffsetBytes);
              srcOffsetBytes += 64 - dstOffsetBytes;
              byteCount -= 64 - dstOffsetBytes;
              SHA1Managed.SHATransform(expandedBuffer, state, block);
              dstOffsetBytes = 0;
            }
            while (byteCount >= 64)
            {
              Buffer.InternalBlockCopy((Array) partIn, srcOffsetBytes, (Array) this._buffer, 0, 64);
              srcOffsetBytes += 64;
              byteCount -= 64;
              SHA1Managed.SHATransform(expandedBuffer, state, block);
            }
            if (byteCount > 0)
              Buffer.InternalBlockCopy((Array) partIn, srcOffsetBytes, (Array) this._buffer, dstOffsetBytes, byteCount);
          }
    }

    private byte[] _EndHash()
    {
      byte[] block = new byte[20];
      int length = 64 - (int) (this._count & 63L);
      if (length <= 8)
        length += 64;
      byte[] partIn = new byte[length];
      partIn[0] = (byte) 128;
      long num = this._count * 8L;
      partIn[length - 8] = (byte) ((ulong) (num >> 56) & (ulong) byte.MaxValue);
      partIn[length - 7] = (byte) ((ulong) (num >> 48) & (ulong) byte.MaxValue);
      partIn[length - 6] = (byte) ((ulong) (num >> 40) & (ulong) byte.MaxValue);
      partIn[length - 5] = (byte) ((ulong) (num >> 32) & (ulong) byte.MaxValue);
      partIn[length - 4] = (byte) ((ulong) (num >> 24) & (ulong) byte.MaxValue);
      partIn[length - 3] = (byte) ((ulong) (num >> 16) & (ulong) byte.MaxValue);
      partIn[length - 2] = (byte) ((ulong) (num >> 8) & (ulong) byte.MaxValue);
      partIn[length - 1] = (byte) ((ulong) num & (ulong) byte.MaxValue);
      this._HashData(partIn, 0, partIn.Length);
      Utils.DWORDToBigEndian(block, this._stateSHA1, 5);
      this.HashValue = block;
      return block;
    }

    [SecurityCritical]
    private static unsafe void SHATransform(uint* expandedBuffer, uint* state, byte* block)
    {
      uint num1 = *state;
      uint num2 = *(uint*) ((IntPtr) state + 4);
      uint num3 = state[2];
      uint num4 = state[3];
      uint num5 = state[4];
      Utils.DWORDFromBigEndian(expandedBuffer, 16, block);
      SHA1Managed.SHAExpand(expandedBuffer);
      int index = 0;
      while (index < 20)
      {
        uint num6 = num5 + (uint) (((int) num1 << 5 | (int) (num1 >> 27)) + ((int) num4 ^ (int) num2 & ((int) num3 ^ (int) num4)) + (int) expandedBuffer[index] + 1518500249);
        uint num7 = num2 << 30 | num2 >> 2;
        uint num8 = num4 + (uint) (((int) num6 << 5 | (int) (num6 >> 27)) + ((int) num3 ^ (int) num1 & ((int) num7 ^ (int) num3)) + (int) expandedBuffer[index + 1] + 1518500249);
        uint num9 = num1 << 30 | num1 >> 2;
        uint num10 = num3 + (uint) (((int) num8 << 5 | (int) (num8 >> 27)) + ((int) num7 ^ (int) num6 & ((int) num9 ^ (int) num7)) + (int) expandedBuffer[index + 2] + 1518500249);
        num5 = num6 << 30 | num6 >> 2;
        num2 = num7 + (uint) (((int) num10 << 5 | (int) (num10 >> 27)) + ((int) num9 ^ (int) num8 & ((int) num5 ^ (int) num9)) + (int) expandedBuffer[index + 3] + 1518500249);
        num4 = num8 << 30 | num8 >> 2;
        num1 = num9 + (uint) (((int) num2 << 5 | (int) (num2 >> 27)) + ((int) num5 ^ (int) num10 & ((int) num4 ^ (int) num5)) + (int) expandedBuffer[index + 4] + 1518500249);
        num3 = num10 << 30 | num10 >> 2;
        index += 5;
      }
      while (index < 40)
      {
        uint num6 = num5 + (uint) (((int) num1 << 5 | (int) (num1 >> 27)) + ((int) num2 ^ (int) num3 ^ (int) num4) + (int) expandedBuffer[index] + 1859775393);
        uint num7 = num2 << 30 | num2 >> 2;
        uint num8 = num4 + (uint) (((int) num6 << 5 | (int) (num6 >> 27)) + ((int) num1 ^ (int) num7 ^ (int) num3) + (int) expandedBuffer[index + 1] + 1859775393);
        uint num9 = num1 << 30 | num1 >> 2;
        uint num10 = num3 + (uint) (((int) num8 << 5 | (int) (num8 >> 27)) + ((int) num6 ^ (int) num9 ^ (int) num7) + (int) expandedBuffer[index + 2] + 1859775393);
        num5 = num6 << 30 | num6 >> 2;
        num2 = num7 + (uint) (((int) num10 << 5 | (int) (num10 >> 27)) + ((int) num8 ^ (int) num5 ^ (int) num9) + (int) expandedBuffer[index + 3] + 1859775393);
        num4 = num8 << 30 | num8 >> 2;
        num1 = num9 + (uint) (((int) num2 << 5 | (int) (num2 >> 27)) + ((int) num10 ^ (int) num4 ^ (int) num5) + (int) expandedBuffer[index + 4] + 1859775393);
        num3 = num10 << 30 | num10 >> 2;
        index += 5;
      }
      while (index < 60)
      {
        uint num6 = num5 + (uint) (((int) num1 << 5 | (int) (num1 >> 27)) + ((int) num2 & (int) num3 | (int) num4 & ((int) num2 | (int) num3)) + (int) expandedBuffer[index] - 1894007588);
        uint num7 = num2 << 30 | num2 >> 2;
        uint num8 = num4 + (uint) (((int) num6 << 5 | (int) (num6 >> 27)) + ((int) num1 & (int) num7 | (int) num3 & ((int) num1 | (int) num7)) + (int) expandedBuffer[index + 1] - 1894007588);
        uint num9 = num1 << 30 | num1 >> 2;
        uint num10 = num3 + (uint) (((int) num8 << 5 | (int) (num8 >> 27)) + ((int) num6 & (int) num9 | (int) num7 & ((int) num6 | (int) num9)) + (int) expandedBuffer[index + 2] - 1894007588);
        num5 = num6 << 30 | num6 >> 2;
        num2 = num7 + (uint) (((int) num10 << 5 | (int) (num10 >> 27)) + ((int) num8 & (int) num5 | (int) num9 & ((int) num8 | (int) num5)) + (int) expandedBuffer[index + 3] - 1894007588);
        num4 = num8 << 30 | num8 >> 2;
        num1 = num9 + (uint) (((int) num2 << 5 | (int) (num2 >> 27)) + ((int) num10 & (int) num4 | (int) num5 & ((int) num10 | (int) num4)) + (int) expandedBuffer[index + 4] - 1894007588);
        num3 = num10 << 30 | num10 >> 2;
        index += 5;
      }
      while (index < 80)
      {
        uint num6 = num5 + (uint) (((int) num1 << 5 | (int) (num1 >> 27)) + ((int) num2 ^ (int) num3 ^ (int) num4) + (int) expandedBuffer[index] - 899497514);
        uint num7 = num2 << 30 | num2 >> 2;
        uint num8 = num4 + (uint) (((int) num6 << 5 | (int) (num6 >> 27)) + ((int) num1 ^ (int) num7 ^ (int) num3) + (int) expandedBuffer[index + 1] - 899497514);
        uint num9 = num1 << 30 | num1 >> 2;
        uint num10 = num3 + (uint) (((int) num8 << 5 | (int) (num8 >> 27)) + ((int) num6 ^ (int) num9 ^ (int) num7) + (int) expandedBuffer[index + 2] - 899497514);
        num5 = num6 << 30 | num6 >> 2;
        num2 = num7 + (uint) (((int) num10 << 5 | (int) (num10 >> 27)) + ((int) num8 ^ (int) num5 ^ (int) num9) + (int) expandedBuffer[index + 3] - 899497514);
        num4 = num8 << 30 | num8 >> 2;
        num1 = num9 + (uint) (((int) num2 << 5 | (int) (num2 >> 27)) + ((int) num10 ^ (int) num4 ^ (int) num5) + (int) expandedBuffer[index + 4] - 899497514);
        num3 = num10 << 30 | num10 >> 2;
        index += 5;
      }
      uint* numPtr = state;
      int num11 = (int) *numPtr + (int) num1;
      *numPtr = (uint) num11;
      IntPtr num12 = (IntPtr) state + 4;
      int num13 = (int) *(uint*) num12 + (int) num2;
      *(int*) num12 = num13;
      IntPtr num14 = (IntPtr) (state + 2);
      int num15 = (int) *(uint*) num14 + (int) num3;
      *(int*) num14 = num15;
      IntPtr num16 = (IntPtr) (state + 3);
      int num17 = (int) *(uint*) num16 + (int) num4;
      *(int*) num16 = num17;
      IntPtr num18 = (IntPtr) (state + 4);
      int num19 = (int) *(uint*) num18 + (int) num5;
      *(int*) num18 = num19;
    }

    [SecurityCritical]
    private static unsafe void SHAExpand(uint* x)
    {
      for (int index = 16; index < 80; ++index)
      {
        uint num = x[index - 3] ^ x[index - 8] ^ x[index - 14] ^ x[index - 16];
        x[index] = num << 1 | num >> 31;
      }
    }
  }
}
