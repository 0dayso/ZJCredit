// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.CspParameters
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace System.Security.Cryptography
{
  /// <summary>包含传递给执行加密计算的加密服务提供程序 (CSP) 的参数。此类不能被继承。</summary>
  [ComVisible(true)]
  public sealed class CspParameters
  {
    /// <summary>表示 <see cref="T:System.Security.Cryptography.CspParameters" /> 的提供程序类型代码。</summary>
    public int ProviderType;
    /// <summary>表示 <see cref="T:System.Security.Cryptography.CspParameters" /> 的提供程序名称。</summary>
    public string ProviderName;
    /// <summary>表示 <see cref="T:System.Security.Cryptography.CspParameters" /> 的密钥容器名称。</summary>
    public string KeyContainerName;
    /// <summary>指定非对称密钥是作为签名密钥还是交换密钥创建。</summary>
    public int KeyNumber;
    private int m_flags;
    private CryptoKeySecurity m_cryptoKeySecurity;
    private SecureString m_keyPassword;
    private IntPtr m_parentWindowHandle;

    /// <summary>表示修改加密服务提供程序 (CSP) 的行为的 <see cref="T:System.Security.Cryptography.CspParameters" /> 的标志。</summary>
    /// <returns>一个枚举值，或枚举值的按位组合。</returns>
    /// <exception cref="T:System.ArgumentException">值不是有效的枚举值。</exception>
    public CspProviderFlags Flags
    {
      get
      {
        return (CspProviderFlags) this.m_flags;
      }
      set
      {
        int num1 = (int) byte.MaxValue;
        int num2 = (int) value;
        if ((num2 & ~num1) != 0)
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) value), "value");
        this.m_flags = num2;
      }
    }

    /// <summary>获取或设置表示容器访问权限及审核规则的 <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> 对象。</summary>
    /// <returns>表示容器访问权限及审核规则的 <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> 对象。</returns>
    public CryptoKeySecurity CryptoKeySecurity
    {
      get
      {
        return this.m_cryptoKeySecurity;
      }
      set
      {
        this.m_cryptoKeySecurity = value;
      }
    }

    /// <summary>获取或设置与智能卡密钥相关的密码。</summary>
    /// <returns>与智能卡密钥相关的密码。</returns>
    public SecureString KeyPassword
    {
      get
      {
        return this.m_keyPassword;
      }
      set
      {
        this.m_keyPassword = value;
        this.m_parentWindowHandle = IntPtr.Zero;
      }
    }

    /// <summary>获取或设置智能卡密码对话框的非托管父级窗口的句柄。</summary>
    /// <returns>智能卡密码对话框的父级窗口的句柄。</returns>
    public IntPtr ParentWindowHandle
    {
      get
      {
        return this.m_parentWindowHandle;
      }
      set
      {
        this.m_parentWindowHandle = value;
        this.m_keyPassword = (SecureString) null;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.Cryptography.CspParameters" /> 类的新实例。</summary>
    public CspParameters()
      : this(24, (string) null, (string) null)
    {
    }

    /// <summary>使用指定的提供程序类型代码初始化 <see cref="T:System.Security.Cryptography.CspParameters" /> 类的新实例。</summary>
    /// <param name="dwTypeIn">指定要创建的提供程序类型的提供程序类型代码。</param>
    public CspParameters(int dwTypeIn)
      : this(dwTypeIn, (string) null, (string) null)
    {
    }

    /// <summary>使用指定的提供程序类型代码和名称初始化 <see cref="T:System.Security.Cryptography.CspParameters" /> 类的新实例。</summary>
    /// <param name="dwTypeIn">指定要创建的提供程序类型的提供程序类型代码。</param>
    /// <param name="strProviderNameIn">提供程序名称。</param>
    public CspParameters(int dwTypeIn, string strProviderNameIn)
      : this(dwTypeIn, strProviderNameIn, (string) null)
    {
    }

    /// <summary>使用指定的提供程序类型代码和名称以及指定的容器名称初始化 <see cref="T:System.Security.Cryptography.CspParameters" /> 类的新实例。</summary>
    /// <param name="dwTypeIn">指定要创建的提供程序类型的提供程序类型代码。</param>
    /// <param name="strProviderNameIn">提供程序名称。</param>
    /// <param name="strContainerNameIn">容器名称。</param>
    public CspParameters(int dwTypeIn, string strProviderNameIn, string strContainerNameIn)
      : this(dwTypeIn, strProviderNameIn, strContainerNameIn, CspProviderFlags.NoFlags)
    {
    }

    /// <summary>使用提供程序类型、提供程序名称、容器名称、访问信息以及与智能卡密钥相关的密码初始化 <see cref="T:System.Security.Cryptography.CspParameters" /> 类的新实例。</summary>
    /// <param name="providerType">指定要创建的提供程序类型的提供程序类型代码。</param>
    /// <param name="providerName">提供程序名称。</param>
    /// <param name="keyContainerName">容器名称。</param>
    /// <param name="cryptoKeySecurity">表示容器的访问权限及审核规则的对象。</param>
    /// <param name="keyPassword">与智能卡密钥相关的密码。</param>
    public CspParameters(int providerType, string providerName, string keyContainerName, CryptoKeySecurity cryptoKeySecurity, SecureString keyPassword)
      : this(providerType, providerName, keyContainerName)
    {
      this.m_cryptoKeySecurity = cryptoKeySecurity;
      this.m_keyPassword = keyPassword;
    }

    /// <summary>使用提供程序类型、提供程序名称、容器名称、访问信息以及非托管智能卡密码对话框的句柄初始化 <see cref="T:System.Security.Cryptography.CspParameters" /> 类的新实例。</summary>
    /// <param name="providerType">指定要创建的提供程序类型的提供程序类型代码。</param>
    /// <param name="providerName">提供程序名称。</param>
    /// <param name="keyContainerName">容器名称。</param>
    /// <param name="cryptoKeySecurity">表示容器的访问权限及审核规则的对象。</param>
    /// <param name="parentWindowHandle">智能卡密码对话框的父级窗口的句柄。</param>
    public CspParameters(int providerType, string providerName, string keyContainerName, CryptoKeySecurity cryptoKeySecurity, IntPtr parentWindowHandle)
      : this(providerType, providerName, keyContainerName)
    {
      this.m_cryptoKeySecurity = cryptoKeySecurity;
      this.m_parentWindowHandle = parentWindowHandle;
    }

    internal CspParameters(int providerType, string providerName, string keyContainerName, CspProviderFlags flags)
    {
      this.ProviderType = providerType;
      this.ProviderName = providerName;
      this.KeyContainerName = keyContainerName;
      this.KeyNumber = -1;
      this.Flags = flags;
    }

    internal CspParameters(CspParameters parameters)
    {
      this.ProviderType = parameters.ProviderType;
      this.ProviderName = parameters.ProviderName;
      this.KeyContainerName = parameters.KeyContainerName;
      this.KeyNumber = parameters.KeyNumber;
      this.Flags = parameters.Flags;
      this.m_cryptoKeySecurity = parameters.m_cryptoKeySecurity;
      this.m_keyPassword = parameters.m_keyPassword;
      this.m_parentWindowHandle = parameters.m_parentWindowHandle;
    }
  }
}
