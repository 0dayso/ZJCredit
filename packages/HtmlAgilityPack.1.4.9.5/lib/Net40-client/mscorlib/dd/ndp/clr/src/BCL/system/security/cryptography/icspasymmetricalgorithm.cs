// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.CspKeyContainerInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
  /// <summary>提供有关加密密钥对的附加信息。此类不能被继承。</summary>
  [ComVisible(true)]
  public sealed class CspKeyContainerInfo
  {
    private CspParameters m_parameters;
    private bool m_randomKeyContainer;

    /// <summary>获取一个值，该值指示某个密钥是否来自计算机密钥集。</summary>
    /// <returns>如果该密钥来自计算机密钥集，则为 true；否则为 false。</returns>
    public bool MachineKeyStore
    {
      get
      {
        return (this.m_parameters.Flags & CspProviderFlags.UseMachineKeyStore) == CspProviderFlags.UseMachineKeyStore;
      }
    }

    /// <summary>获取密钥的提供程序名称。</summary>
    /// <returns>提供程序名称。</returns>
    public string ProviderName
    {
      get
      {
        return this.m_parameters.ProviderName;
      }
    }

    /// <summary>获取密钥的提供程序类型。</summary>
    /// <returns>提供程序类型。默认值为 1。</returns>
    public int ProviderType
    {
      get
      {
        return this.m_parameters.ProviderType;
      }
    }

    /// <summary>获取密钥容器名称。</summary>
    /// <returns>密钥容器名称。</returns>
    public string KeyContainerName
    {
      get
      {
        return this.m_parameters.KeyContainerName;
      }
    }

    /// <summary>获取唯一的密钥容器名称。</summary>
    /// <returns>唯一的密钥容器名称。</returns>
    /// <exception cref="T:System.NotSupportedException">该密钥类型不受支持。</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">无法找到加密服务提供程序。- 或 -未找到密钥容器。</exception>
    public string UniqueKeyContainerName
    {
      [SecuritySafeCritical] get
      {
        SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
        if (Utils._OpenCSP(this.m_parameters, 64U, ref invalidHandle) != 0)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NotFound"));
        string str = (string) Utils._GetProviderParameter(invalidHandle, this.m_parameters.KeyNumber, 8U);
        invalidHandle.Dispose();
        return str;
      }
    }

    /// <summary>获取一个值，该值描述非对称密钥是否被创建为签名密钥或交换密钥。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Cryptography.KeyNumber" /> 值之一，该值描述非对称密钥是否被创建为签名密钥或交换密钥。</returns>
    public KeyNumber KeyNumber
    {
      get
      {
        return (KeyNumber) this.m_parameters.KeyNumber;
      }
    }

    /// <summary>获取一个值，该值指示某个密钥是否可从密钥容器中导出。</summary>
    /// <returns>如果该密钥可以导出，则为 true；否则，为 false。</returns>
    /// <exception cref="T:System.NotSupportedException">该密钥类型不受支持。</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">无法找到加密服务提供程序。- 或 -未找到密钥容器。</exception>
    public bool Exportable
    {
      [SecuritySafeCritical] get
      {
        if (this.HardwareDevice)
          return false;
        SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
        if (Utils._OpenCSP(this.m_parameters, 64U, ref invalidHandle) != 0)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NotFound"));
        byte[] numArray = (byte[]) Utils._GetProviderParameter(invalidHandle, this.m_parameters.KeyNumber, 3U);
        invalidHandle.Dispose();
        int index = 0;
        return (int) numArray[index] == 1;
      }
    }

    /// <summary>获取一个值，该值指示某个密钥是否为硬件密钥。</summary>
    /// <returns>如果该密钥是硬件密钥，则为 true；否则为 false。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">无法找到加密服务提供程序。</exception>
    public bool HardwareDevice
    {
      [SecuritySafeCritical] get
      {
        SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
        CspParameters cspParameters1 = new CspParameters(this.m_parameters);
        cspParameters1.KeyContainerName = (string) null;
        CspParameters cspParameters2 = cspParameters1;
        int num = (cspParameters2.Flags & CspProviderFlags.UseMachineKeyStore) != CspProviderFlags.NoFlags ? 1 : 0;
        cspParameters2.Flags = (CspProviderFlags) num;
        uint flags = 4026531840;
        if (Utils._OpenCSP(cspParameters1, flags, ref invalidHandle) != 0)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NotFound"));
        byte[] numArray = (byte[]) Utils._GetProviderParameter(invalidHandle, cspParameters1.KeyNumber, 5U);
        invalidHandle.Dispose();
        int index = 0;
        return (int) numArray[index] == 1;
      }
    }

    /// <summary>获取一个值，该值指示某个密钥是否能从密钥容器中移除。</summary>
    /// <returns>如果该密钥是可移除的，则为 true；否则，为 false。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">未找到加密服务提供程序 (CSP)。</exception>
    public bool Removable
    {
      [SecuritySafeCritical] get
      {
        SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
        CspParameters cspParameters1 = new CspParameters(this.m_parameters);
        cspParameters1.KeyContainerName = (string) null;
        CspParameters cspParameters2 = cspParameters1;
        int num = (cspParameters2.Flags & CspProviderFlags.UseMachineKeyStore) != CspProviderFlags.NoFlags ? 1 : 0;
        cspParameters2.Flags = (CspProviderFlags) num;
        uint flags = 4026531840;
        if (Utils._OpenCSP(cspParameters1, flags, ref invalidHandle) != 0)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NotFound"));
        byte[] numArray = (byte[]) Utils._GetProviderParameter(invalidHandle, cspParameters1.KeyNumber, 4U);
        invalidHandle.Dispose();
        int index = 0;
        return (int) numArray[index] == 1;
      }
    }

    /// <summary>获取一个值，该值指示密钥容器中的某个密钥是否可访问。</summary>
    /// <returns>如果该密钥可访问，则为 true；否则，为 false。</returns>
    /// <exception cref="T:System.NotSupportedException">该密钥类型不受支持。</exception>
    public bool Accessible
    {
      [SecuritySafeCritical] get
      {
        SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
        if (Utils._OpenCSP(this.m_parameters, 64U, ref invalidHandle) != 0)
          return false;
        byte[] numArray = (byte[]) Utils._GetProviderParameter(invalidHandle, this.m_parameters.KeyNumber, 6U);
        invalidHandle.Dispose();
        int index = 0;
        return (int) numArray[index] == 1;
      }
    }

    /// <summary>获取一个值，该值指示某个密钥对是否受到保护。</summary>
    /// <returns>如果该密钥对受到保护，则为 true；否则，为 false。</returns>
    /// <exception cref="T:System.NotSupportedException">该密钥类型不受支持。</exception>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">无法找到加密服务提供程序。- 或 -未找到密钥容器。</exception>
    public bool Protected
    {
      [SecuritySafeCritical] get
      {
        if (this.HardwareDevice)
          return true;
        SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
        if (Utils._OpenCSP(this.m_parameters, 64U, ref invalidHandle) != 0)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NotFound"));
        byte[] numArray = (byte[]) Utils._GetProviderParameter(invalidHandle, this.m_parameters.KeyNumber, 7U);
        invalidHandle.Dispose();
        int index = 0;
        return (int) numArray[index] == 1;
      }
    }

    /// <summary>获取 <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> 对象，该对象表示容器的访问权限和审核规则。</summary>
    /// <returns>表示容器访问权限及审核规则的 <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> 对象。</returns>
    /// <exception cref="T:System.Security.Cryptography.CryptographicException">该密钥类型不受支持。</exception>
    /// <exception cref="T:System.NotSupportedException">无法找到加密服务提供程序。- 或 -未找到密钥容器。</exception>
    public CryptoKeySecurity CryptoKeySecurity
    {
      [SecuritySafeCritical] get
      {
        KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
        KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(this.m_parameters, KeyContainerPermissionFlags.ViewAcl | KeyContainerPermissionFlags.ChangeAcl);
        containerPermission.AccessEntries.Add(accessEntry);
        containerPermission.Demand();
        SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
        if (Utils._OpenCSP(this.m_parameters, 64U, ref invalidHandle) != 0)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NotFound"));
        using (invalidHandle)
          return Utils.GetKeySetSecurityInfo(invalidHandle, AccessControlSections.All);
      }
    }

    /// <summary>获取一个值，该值指示某个密钥容器是否由托管加密类随机生成。</summary>
    /// <returns>如果该密钥容器是随机生成的，则为 true；否则为 false。</returns>
    public bool RandomlyGenerated
    {
      get
      {
        return this.m_randomKeyContainer;
      }
    }

    private CspKeyContainerInfo()
    {
    }

    [SecurityCritical]
    internal CspKeyContainerInfo(CspParameters parameters, bool randomKeyContainer)
    {
      if (!CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
      {
        KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
        KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(parameters, KeyContainerPermissionFlags.Open);
        containerPermission.AccessEntries.Add(accessEntry);
        containerPermission.Demand();
      }
      this.m_parameters = new CspParameters(parameters);
      if (this.m_parameters.KeyNumber == -1)
      {
        if (this.m_parameters.ProviderType == 1 || this.m_parameters.ProviderType == 24)
          this.m_parameters.KeyNumber = 1;
        else if (this.m_parameters.ProviderType == 13)
          this.m_parameters.KeyNumber = 2;
      }
      this.m_randomKeyContainer = randomKeyContainer;
    }

    /// <summary>使用指定的参数初始化 <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" /> 类的新实例。</summary>
    /// <param name="parameters">一个 <see cref="T:System.Security.Cryptography.CspParameters" /> 对象，提供有关密钥的信息。</param>
    [SecuritySafeCritical]
    public CspKeyContainerInfo(CspParameters parameters)
      : this(parameters, false)
    {
    }
  }
}
