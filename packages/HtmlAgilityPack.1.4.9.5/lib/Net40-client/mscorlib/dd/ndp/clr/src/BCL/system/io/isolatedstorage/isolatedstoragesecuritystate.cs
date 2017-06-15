// Decompiled with JetBrains decompiler
// Type: System.IO.IsolatedStorage.IsolatedStorageSecurityState
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.IO.IsolatedStorage
{
  /// <summary>提供用于维护独立存储的配额大小的设置。</summary>
  [SecurityCritical]
  public class IsolatedStorageSecurityState : SecurityState
  {
    private long m_UsedSize;
    private long m_Quota;
    private IsolatedStorageSecurityOptions m_Options;

    /// <summary>获取用于管理独立存储安全性的选项。</summary>
    /// <returns>用于增加独立存储的配额大小的选项。</returns>
    public IsolatedStorageSecurityOptions Options
    {
      get
      {
        return this.m_Options;
      }
    }

    /// <summary>获取独立存储中的当前使用大小。</summary>
    /// <returns>当前使用大小，以字节为单位。</returns>
    public long UsedSize
    {
      get
      {
        return this.m_UsedSize;
      }
    }

    /// <summary>获取或设置独立存储的当前配额大小。</summary>
    /// <returns>当前配额大小，以字节为单位。</returns>
    public long Quota
    {
      get
      {
        return this.m_Quota;
      }
      set
      {
        this.m_Quota = value;
      }
    }

    [SecurityCritical]
    private IsolatedStorageSecurityState()
    {
    }

    internal static IsolatedStorageSecurityState CreateStateToIncreaseQuotaForApplication(long newQuota, long usedSize)
    {
      return new IsolatedStorageSecurityState() { m_Options = IsolatedStorageSecurityOptions.IncreaseQuotaForApplication, m_Quota = newQuota, m_UsedSize = usedSize };
    }

    /// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">状态不可用。</exception>
    [SecurityCritical]
    public override void EnsureState()
    {
      if (!this.IsStateAvailable())
        throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_Operation"));
    }
  }
}
