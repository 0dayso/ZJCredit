// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.ZoneIdentityPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Permissions
{
  /// <summary>为代码源自的区域定义标识权限。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class ZoneIdentityPermission : CodeAccessPermission, IBuiltInPermission
  {
    private SecurityZone m_zone = SecurityZone.NoZone;
    private const uint AllZones = 31;
    [OptionalField(VersionAdded = 2)]
    private uint m_zones;
    [OptionalField(VersionAdded = 2)]
    private string m_serializedPermission;

    /// <summary>获取或设置由当前 <see cref="T:System.Security.Permissions.ZoneIdentityPermission" /> 表示的区域。</summary>
    /// <returns>
    /// <see cref="T:System.Security.SecurityZone" /> 值之一。</returns>
    /// <exception cref="T:System.ArgumentException">该参数值不是 <see cref="T:System.Security.SecurityZone" /> 的有效值。</exception>
    public SecurityZone SecurityZone
    {
      get
      {
        SecurityZone securityZone = SecurityZone.NoZone;
        int num1 = 0;
        uint num2 = 1;
        while (num2 < 31U)
        {
          if (((int) this.m_zones & (int) num2) != 0)
          {
            if (securityZone != SecurityZone.NoZone)
              return SecurityZone.NoZone;
            securityZone = (SecurityZone) num1;
          }
          ++num1;
          num2 <<= 1;
        }
        return securityZone;
      }
      set
      {
        ZoneIdentityPermission.VerifyZone(value);
        if (value == SecurityZone.NoZone)
          this.m_zones = 0U;
        else
          this.m_zones = 1U << (int) (value & (SecurityZone) 31);
      }
    }

    /// <summary>用指定的 <see cref="T:System.Security.Permissions.PermissionState" /> 初始化 <see cref="T:System.Security.Permissions.ZoneIdentityPermission" /> 类的新实例。</summary>
    /// <param name="state">
    /// <see cref="T:System.Security.Permissions.PermissionState" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="state" /> 参数不是有效的 <see cref="T:System.Security.Permissions.PermissionState" /> 值。</exception>
    public ZoneIdentityPermission(PermissionState state)
    {
      if (state == PermissionState.Unrestricted)
      {
        this.m_zones = 31U;
      }
      else
      {
        if (state != PermissionState.None)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
        this.m_zones = 0U;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.Permissions.ZoneIdentityPermission" /> 类的新实例以表示指定的区域标识。</summary>
    /// <param name="zone">区域标识符。</param>
    public ZoneIdentityPermission(SecurityZone zone)
    {
      this.SecurityZone = zone;
    }

    internal ZoneIdentityPermission(uint zones)
    {
      this.m_zones = zones & 31U;
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) == (StreamingContextStates) 0)
        return;
      if (this.m_serializedPermission != null)
      {
        this.FromXml(SecurityElement.FromString(this.m_serializedPermission));
        this.m_serializedPermission = (string) null;
      }
      else
      {
        this.SecurityZone = this.m_zone;
        this.m_zone = SecurityZone.NoZone;
      }
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) == (StreamingContextStates) 0)
        return;
      this.m_serializedPermission = this.ToXml().ToString();
      this.m_zone = this.SecurityZone;
    }

    [OnSerialized]
    private void OnSerialized(StreamingContext ctx)
    {
      if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) == (StreamingContextStates) 0)
        return;
      this.m_serializedPermission = (string) null;
      this.m_zone = SecurityZone.NoZone;
    }

    internal void AppendZones(ArrayList zoneList)
    {
      int num1 = 0;
      uint num2 = 1;
      while (num2 < 31U)
      {
        if (((int) this.m_zones & (int) num2) != 0)
          zoneList.Add((object) (SecurityZone) num1);
        ++num1;
        num2 <<= 1;
      }
    }

    private static void VerifyZone(SecurityZone zone)
    {
      if (zone < SecurityZone.NoZone || zone > SecurityZone.Untrusted)
        throw new ArgumentException(Environment.GetResourceString("Argument_IllegalZone"));
    }

    /// <summary>创建并返回当前权限的相同副本。</summary>
    /// <returns>当前权限的副本。</returns>
    public override IPermission Copy()
    {
      return (IPermission) new ZoneIdentityPermission(this.m_zones);
    }

    /// <summary>确定当前权限是否为指定权限的子集。</summary>
    /// <returns>如果当前权限是指定权限的子集，则为 true；否则为 false。</returns>
    /// <param name="target">将要测试子集关系的权限。此权限必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，此权限不表示 <see cref="F:System.Security.SecurityZone.NoZone" /> 安全区域，而且指定的权限与当前权限不相等。</exception>
    public override bool IsSubsetOf(IPermission target)
    {
      if (target == null)
        return (int) this.m_zones == 0;
      ZoneIdentityPermission identityPermission = target as ZoneIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      return ((int) this.m_zones & (int) identityPermission.m_zones) == (int) this.m_zones;
    }

    /// <summary>创建并返回一个权限，该权限是当前权限和指定权限的交集。</summary>
    /// <returns>一个新权限，它表示当前权限与指定权限的交集。如果交集为空，则此新权限为 null。</returns>
    /// <param name="target">要与当前权限相交的权限。它必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。</exception>
    public override IPermission Intersect(IPermission target)
    {
      if (target == null)
        return (IPermission) null;
      ZoneIdentityPermission identityPermission = target as ZoneIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      uint zones = this.m_zones & identityPermission.m_zones;
      if ((int) zones == 0)
        return (IPermission) null;
      return (IPermission) new ZoneIdentityPermission(zones);
    }

    /// <summary>创建一个权限，该权限是当前权限与指定权限的并集。</summary>
    /// <returns>一个新权限，它表示当前权限与指定权限的并集。</returns>
    /// <param name="target">将与当前权限合并的权限。它必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。- 或 -这两个权限不相等，而且当前权限不表示 <see cref="F:System.Security.SecurityZone.NoZone" /> 安全区域。</exception>
    public override IPermission Union(IPermission target)
    {
      if (target == null)
      {
        if ((int) this.m_zones == 0)
          return (IPermission) null;
        return this.Copy();
      }
      ZoneIdentityPermission identityPermission = target as ZoneIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      return (IPermission) new ZoneIdentityPermission(this.m_zones | identityPermission.m_zones);
    }

    /// <summary>创建权限及其当前状态的 XML 编码。</summary>
    /// <returns>权限的 XML 编码，包括任何状态信息。</returns>
    public override SecurityElement ToXml()
    {
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.ZoneIdentityPermission");
      if (this.SecurityZone != SecurityZone.NoZone)
      {
        permissionElement.AddAttribute("Zone", Enum.GetName(typeof (SecurityZone), (object) this.SecurityZone));
      }
      else
      {
        int num1 = 0;
        uint num2 = 1;
        while (num2 < 31U)
        {
          if (((int) this.m_zones & (int) num2) != 0)
          {
            SecurityElement child = new SecurityElement("Zone");
            child.AddAttribute("Zone", Enum.GetName(typeof (SecurityZone), (object) (SecurityZone) num1));
            permissionElement.AddChild(child);
          }
          ++num1;
          num2 <<= 1;
        }
      }
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
      this.m_zones = 0U;
      CodeAccessPermission.ValidateElement(esd, (IPermission) this);
      string str = esd.Attribute("Zone");
      if (str != null)
        this.SecurityZone = (SecurityZone) Enum.Parse(typeof (SecurityZone), str);
      if (esd.Children == null)
        return;
      foreach (SecurityElement child in esd.Children)
      {
        int num = (int) Enum.Parse(typeof (SecurityZone), child.Attribute("Zone"));
        if (num != -1)
          this.m_zones = this.m_zones | (uint) (1 << num);
      }
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return ZoneIdentityPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 14;
    }
  }
}
