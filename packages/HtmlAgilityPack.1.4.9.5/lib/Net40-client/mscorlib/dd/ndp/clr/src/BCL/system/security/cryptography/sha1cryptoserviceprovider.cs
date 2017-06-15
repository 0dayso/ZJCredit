// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.SHA1CryptoServiceProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>使用加密服务提供程序 (CSP) 提供的实现计算输入数据的 <see cref="T:System.Security.Cryptography.SHA1" /> 哈希值。此类不能被继承。</summary>
  [ComVisible(true)]
  public sealed class SHA1CryptoServiceProvider : SHA1
  {
    [SecurityCritical]
    private SafeHashHandle _safeHashHandle;

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" /> 类的新实例。</summary>
    [SecuritySafeCritical]
    public SHA1CryptoServiceProvider()
    {
      this._safeHashHandle = Utils.CreateHash(Utils.StaticProvHandle, 32772);
    }

    [SecuritySafeCritical]
    protected override void Dispose(bool disposing)
    {
      if (this._safeHashHandle != null && !this._safeHashHandle.IsClosed)
        this._safeHashHandle.Dispose();
      base.Dispose(disposing);
    }

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" /> 的实例。</summary>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public override void Initialize()
    {
      if (this._safeHashHandle != null && !this._safeHashHandle.IsClosed)
        this._safeHashHandle.Dispose();
      this._safeHashHandle = Utils.CreateHash(Utils.StaticProvHandle, 32772);
    }

    [SecuritySafeCritical]
    protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
    {
      Utils.HashData(this._safeHashHandle, rgb, ibStart, cbSize);
    }

    [SecuritySafeCritical]
    protected override byte[] HashFinal()
    {
      return Utils.EndHash(this._safeHashHandle);
    }
  }
}
