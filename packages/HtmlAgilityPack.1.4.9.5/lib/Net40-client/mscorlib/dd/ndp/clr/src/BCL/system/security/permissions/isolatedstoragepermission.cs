// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.IsolatedStoragePermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>表示对一般独立存储功能的访问。</summary>
  [ComVisible(true)]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, ControlEvidence = true, ControlPolicy = true)]
  public abstract class IsolatedStoragePermission : CodeAccessPermission, IUnrestrictedPermission
  {
    internal long m_userQuota;
    internal long m_machineQuota;
    internal long m_expirationDays;
    internal bool m_permanentData;
    internal IsolatedStorageContainment m_allowed;
    private const string _strUserQuota = "UserQuota";
    private const string _strMachineQuota = "MachineQuota";
    private const string _strExpiry = "Expiry";
    private const string _strPermDat = "Permanent";

    /// <summary>获取或设置每位用户的总存储在总大小中的配额。</summary>
    /// <returns>给用户分配的资源的大小（以字节为单位）。</returns>
    public long UserQuota
    {
      get
      {
        return this.m_userQuota;
      }
      set
      {
        this.m_userQuota = value;
      }
    }

    /// <summary>获取或设置所允许的独立存储包容类型。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Permissions.IsolatedStorageContainment" /> 值之一。</returns>
    public IsolatedStorageContainment UsageAllowed
    {
      get
      {
        return this.m_allowed;
      }
      set
      {
        this.m_allowed = value;
      }
    }

    /// <summary>用指定的受限制或无限制的权限初始化 <see cref="T:System.Security.Permissions.IsolatedStoragePermission" /> 类的新实例。</summary>
    /// <param name="state">
    /// <see cref="T:System.Security.Permissions.PermissionState" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="state" /> 参数不是有效的 <see cref="T:System.Security.Permissions.PermissionState" /> 值。</exception>
    protected IsolatedStoragePermission(PermissionState state)
    {
      if (state == PermissionState.Unrestricted)
      {
        this.m_userQuota = long.MaxValue;
        this.m_machineQuota = long.MaxValue;
        this.m_expirationDays = long.MaxValue;
        this.m_permanentData = true;
        this.m_allowed = IsolatedStorageContainment.UnrestrictedIsolatedStorage;
      }
      else
      {
        if (state != PermissionState.None)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
        this.m_userQuota = 0L;
        this.m_machineQuota = 0L;
        this.m_expirationDays = 0L;
        this.m_permanentData = false;
        this.m_allowed = IsolatedStorageContainment.None;
      }
    }

    internal IsolatedStoragePermission(IsolatedStorageContainment UsageAllowed, long ExpirationDays, bool PermanentData)
    {
      this.m_userQuota = 0L;
      this.m_machineQuota = 0L;
      this.m_expirationDays = ExpirationDays;
      this.m_permanentData = PermanentData;
      this.m_allowed = UsageAllowed;
    }

    internal IsolatedStoragePermission(IsolatedStorageContainment UsageAllowed, long ExpirationDays, bool PermanentData, long UserQuota)
    {
      this.m_machineQuota = 0L;
      this.m_userQuota = UserQuota;
      this.m_expirationDays = ExpirationDays;
      this.m_permanentData = PermanentData;
      this.m_allowed = UsageAllowed;
    }

    /// <summary>返回一个值，该值指示当前权限是否为无限制的。</summary>
    /// <returns>如果当前权限是无限制的，则为 true；否则为 false。</returns>
    public bool IsUnrestricted()
    {
      return this.m_allowed == IsolatedStorageContainment.UnrestrictedIsolatedStorage;
    }

    internal static long min(long x, long y)
    {
      if (x <= y)
        return x;
      return y;
    }

    internal static long max(long x, long y)
    {
      if (x >= y)
        return x;
      return y;
    }

    /// <summary>创建权限及其当前状态的 XML 编码。</summary>
    /// <returns>权限的 XML 编码，包括任何状态信息。</returns>
    public override SecurityElement ToXml()
    {
      return this.ToXml(this.GetType().FullName);
    }

    internal SecurityElement ToXml(string permName)
    {
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, permName);
      if (!this.IsUnrestricted())
      {
        permissionElement.AddAttribute("Allowed", Enum.GetName(typeof (IsolatedStorageContainment), (object) this.m_allowed));
        if (this.m_userQuota > 0L)
          permissionElement.AddAttribute("UserQuota", this.m_userQuota.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        if (this.m_machineQuota > 0L)
          permissionElement.AddAttribute("MachineQuota", this.m_machineQuota.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        if (this.m_expirationDays > 0L)
          permissionElement.AddAttribute("Expiry", this.m_expirationDays.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        if (this.m_permanentData)
          permissionElement.AddAttribute("Permanent", this.m_permanentData.ToString());
      }
      else
        permissionElement.AddAttribute("Unrestricted", "true");
      return permissionElement;
    }

    /// <summary>从 XML 编码重新构造具有指定状态的权限。</summary>
    /// <param name="esd">用于重新构造权限的 XML 编码。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="esd" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="esd" /> 参数不是有效的权限元素。- 或 -<paramref name="esd" /> 参数的版本号无效。</exception>
    public override void FromXml(SecurityElement esd)
    {
      CodeAccessPermission.ValidateElement(esd, (IPermission) this);
      this.m_allowed = IsolatedStorageContainment.None;
      if (XMLUtil.IsUnrestricted(esd))
      {
        this.m_allowed = IsolatedStorageContainment.UnrestrictedIsolatedStorage;
      }
      else
      {
        string str = esd.Attribute("Allowed");
        if (str != null)
          this.m_allowed = (IsolatedStorageContainment) Enum.Parse(typeof (IsolatedStorageContainment), str);
      }
      if (this.m_allowed == IsolatedStorageContainment.UnrestrictedIsolatedStorage)
      {
        this.m_userQuota = long.MaxValue;
        this.m_machineQuota = long.MaxValue;
        this.m_expirationDays = long.MaxValue;
        this.m_permanentData = true;
      }
      else
      {
        string s1 = esd.Attribute("UserQuota");
        this.m_userQuota = s1 != null ? long.Parse(s1, (IFormatProvider) CultureInfo.InvariantCulture) : 0L;
        string s2 = esd.Attribute("MachineQuota");
        this.m_machineQuota = s2 != null ? long.Parse(s2, (IFormatProvider) CultureInfo.InvariantCulture) : 0L;
        string s3 = esd.Attribute("Expiry");
        this.m_expirationDays = s3 != null ? long.Parse(s3, (IFormatProvider) CultureInfo.InvariantCulture) : 0L;
        string str = esd.Attribute("Permanent");
        this.m_permanentData = str != null && bool.Parse(str);
      }
    }
  }
}
