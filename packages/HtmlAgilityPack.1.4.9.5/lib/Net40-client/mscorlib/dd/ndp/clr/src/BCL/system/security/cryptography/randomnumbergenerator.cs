// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RandomNumberGenerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>表示加密随机数生成器的所有实现从中派生的抽象类。</summary>
  [ComVisible(true)]
  public abstract class RandomNumberGenerator : IDisposable
  {
    /// <summary>在派生类中重写时，创建可用于生成随机数据的加密随机数生成器默认实现的实例。</summary>
    /// <returns>加密随机数生成器的新实例。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public static RandomNumberGenerator Create()
    {
      return RandomNumberGenerator.Create("System.Security.Cryptography.RandomNumberGenerator");
    }

    /// <summary>在派生类中重写时，创建加密随机数生成器的指定实现的实例。</summary>
    /// <returns>加密随机数生成器的新实例。</returns>
    /// <param name="rngName">要使用的随机数生成器实现的名称。</param>
    public static RandomNumberGenerator Create(string rngName)
    {
      return (RandomNumberGenerator) CryptoConfig.CreateFromName(rngName);
    }

    /// <summary>在派生类中重写时，释放由 <see cref="T:System.Security.Cryptography.RandomNumberGenerator" /> 类的当前实例使用的所有资源。</summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>在派生类中被重写时，释放由 <see cref="T:System.Security.Cryptography.RandomNumberGenerator" /> 使用的非托管资源，也可以根据需要释放托管资源。</summary>
    /// <param name="disposing">若要释放托管资源和非托管资源，则为 true；若仅释放非托管资源，则为 false。</param>
    protected virtual void Dispose(bool disposing)
    {
    }

    /// <summary>当在派生类中重写时，用加密型强随机值序列填充字节数组。</summary>
    /// <param name="data">要用加密型强随机字节填充的数组。</param>
    public abstract void GetBytes(byte[] data);

    /// <summary>用加密型强随机值序列填充指定的字节数组。</summary>
    /// <param name="data">要用加密型强随机字节填充的数组。</param>
    /// <param name="offset">开始填充操作的数组的索引。</param>
    /// <param name="count">要填充的字节数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="data" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="offset" /> 或 <paramref name="count" /> 小于 0</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="offset" /> 加上 <paramref name="count" /> 超过了 <paramref name="data" />。</exception>
    public virtual void GetBytes(byte[] data, int offset, int count)
    {
      if (data == null)
        throw new ArgumentNullException("data");
      if (offset < 0)
        throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (offset + count > data.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (count <= 0)
        return;
      byte[] data1 = new byte[count];
      this.GetBytes(data1);
      Array.Copy((Array) data1, 0, (Array) data, offset, count);
    }

    /// <summary>当在派生类中重写时，用加密型强随机非零值序列填充字节数组。</summary>
    /// <param name="data">用加密型强随机非零字节填充的数组。</param>
    public virtual void GetNonZeroBytes(byte[] data)
    {
      throw new NotImplementedException();
    }
  }
}
