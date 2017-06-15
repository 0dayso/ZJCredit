// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RNGCryptoServiceProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>使用加密服务提供程序 (CSP) 提供的实现来实现加密随机数生成器 (RNG)。此类不能被继承。</summary>
  [ComVisible(true)]
  public sealed class RNGCryptoServiceProvider : RandomNumberGenerator
  {
    [SecurityCritical]
    private SafeProvHandle m_safeProvHandle;
    private bool m_ownsHandle;

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.RNGCryptoServiceProvider" /> 类的新实例。</summary>
    public RNGCryptoServiceProvider()
      : this((CspParameters) null)
    {
    }

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.RNGCryptoServiceProvider" /> 类的新实例。</summary>
    /// <param name="str">字符串输入。忽略此参数。</param>
    public RNGCryptoServiceProvider(string str)
      : this((CspParameters) null)
    {
    }

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.RNGCryptoServiceProvider" /> 类的新实例。</summary>
    /// <param name="rgb">字节数组。忽略此值。</param>
    public RNGCryptoServiceProvider(byte[] rgb)
      : this((CspParameters) null)
    {
    }

    /// <summary>使用指定的参数初始化 <see cref="T:System.Security.Cryptography.RNGCryptoServiceProvider" /> 类的新实例。</summary>
    /// <param name="cspParams">传递给加密服务提供程序 (CSP) 的参数。</param>
    [SecuritySafeCritical]
    public RNGCryptoServiceProvider(CspParameters cspParams)
    {
      if (cspParams != null)
      {
        this.m_safeProvHandle = Utils.AcquireProvHandle(cspParams);
        this.m_ownsHandle = true;
      }
      else
      {
        this.m_safeProvHandle = Utils.StaticProvHandle;
        this.m_ownsHandle = false;
      }
    }

    [SecuritySafeCritical]
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing || !this.m_ownsHandle)
        return;
      this.m_safeProvHandle.Dispose();
    }

    /// <summary>用经过加密的强随机值序列填充字节数组。</summary>
    /// <param name="data">用经过加密的强随机值序列填充的数组。</param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="data" /> is null.</exception>
    [SecuritySafeCritical]
    public override void GetBytes(byte[] data)
    {
      if (data == null)
        throw new ArgumentNullException("data");
      SafeProvHandle hProv = this.m_safeProvHandle;
      byte[] randomBytes = data;
      int length = randomBytes.Length;
      RNGCryptoServiceProvider.GetBytes(hProv, randomBytes, length);
    }

    /// <summary>用经过加密的强随机非零值序列填充字节数组。</summary>
    /// <param name="data">用经过加密的强随机非零值序列填充的数组。 </param>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="data" /> is null.</exception>
    [SecuritySafeCritical]
    public override void GetNonZeroBytes(byte[] data)
    {
      if (data == null)
        throw new ArgumentNullException("data");
      SafeProvHandle hProv = this.m_safeProvHandle;
      byte[] randomBytes = data;
      int length = randomBytes.Length;
      RNGCryptoServiceProvider.GetNonZeroBytes(hProv, randomBytes, length);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetBytes(SafeProvHandle hProv, byte[] randomBytes, int count);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetNonZeroBytes(SafeProvHandle hProv, byte[] randomBytes, int count);
  }
}
