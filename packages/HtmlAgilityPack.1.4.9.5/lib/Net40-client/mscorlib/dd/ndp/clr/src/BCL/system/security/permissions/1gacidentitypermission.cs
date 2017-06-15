// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.GacIdentityPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>定义从全局程序集缓存中产生的文件的标识权限。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class GacIdentityPermission : CodeAccessPermission, IBuiltInPermission
  {
    /// <summary>用完全受限制的 <see cref="T:System.Security.Permissions.PermissionState" /> 初始化 <see cref="T:System.Security.Permissions.GacIdentityPermission" /> 类的新实例。</summary>
    /// <param name="state">
    /// <see cref="T:System.Security.Permissions.PermissionState" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="state" /> 不是有效的 <see cref="T:System.Security.Permissions.PermissionState" /> 值。</exception>
    public GacIdentityPermission(PermissionState state)
    {
      if (state != PermissionState.Unrestricted && state != PermissionState.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
    }

    /// <summary>初始化 <see cref="T:System.Security.Permissions.GacIdentityPermission" /> 类的新实例。</summary>
    public GacIdentityPermission()
    {
    }

    /// <summary>创建并返回当前权限的相同副本。</summary>
    /// <returns>当前权限的副本。</returns>
    public override IPermission Copy()
    {
      return (IPermission) new GacIdentityPermission();
    }

    /// <summary>指示当前权限是否为指定权限的子集。</summary>
    /// <returns>如果当前权限是指定权限的子集，则为 true；否则为 false。</returns>
    /// <param name="target">要测试子集关系的权限对象。此权限必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 不是 null，而且与当前权限不是同一类型。</exception>
    public override bool IsSubsetOf(IPermission target)
    {
      if (target == null)
        return false;
      if (!(target is GacIdentityPermission))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      return true;
    }

    /// <summary>创建并返回一个权限，该权限是当前权限和指定权限的交集。</summary>
    /// <returns>一个新权限，它表示当前权限与指定权限的交集。如果交集为空，则新权限为 null。</returns>
    /// <param name="target">要与当前权限相交的权限。它必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 不是 null，而且与当前权限不是同一类型。</exception>
    public override IPermission Intersect(IPermission target)
    {
      if (target == null)
        return (IPermission) null;
      if (!(target is GacIdentityPermission))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      return this.Copy();
    }

    /// <summary>创建并返回一个权限，该权限是当前权限和指定权限的并集。</summary>
    /// <returns>一个新权限，它表示当前权限与指定权限的并集。</returns>
    /// <param name="target">将与当前权限合并的权限。它必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 不是 null，而且与当前权限不是同一类型。</exception>
    public override IPermission Union(IPermission target)
    {
      if (target == null)
        return this.Copy();
      if (!(target is GacIdentityPermission))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      return this.Copy();
    }

    /// <summary>创建权限及其当前状态的 XML 编码。</summary>
    /// <returns>
    /// <see cref="T:System.Security.SecurityElement" />，表示权限的 XML 编码（包括任何状态信息）。</returns>
    public override SecurityElement ToXml()
    {
      return CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.GacIdentityPermission");
    }

    /// <summary>通过 XML 编码创建权限。</summary>
    /// <param name="securityElement">
    /// <see cref="T:System.Security.SecurityElement" />，包含用于创建权限的 XML 编码。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="securityElement" />为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="securityElement" /> 不是一个有效的权限元素。- 或 -<paramref name="securityElement" /> 的版本号无效。</exception>
    public override void FromXml(SecurityElement securityElement)
    {
      CodeAccessPermission.ValidateElement(securityElement, (IPermission) this);
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return GacIdentityPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 15;
    }
  }
}
