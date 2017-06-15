// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.IsolatedStorageFilePermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>指定允许使用私有虚拟文件系统。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class IsolatedStorageFilePermission : IsolatedStoragePermission, IBuiltInPermission
  {
    /// <summary>用指定的完全受限制或不受限制的权限初始化 <see cref="T:System.Security.Permissions.IsolatedStorageFilePermission" /> 类的新实例。</summary>
    /// <param name="state">
    /// <see cref="T:System.Security.Permissions.PermissionState" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="state" /> 参数不是有效的 <see cref="T:System.Security.Permissions.PermissionState" /> 值。</exception>
    public IsolatedStorageFilePermission(PermissionState state)
      : base(state)
    {
    }

    internal IsolatedStorageFilePermission(IsolatedStorageContainment UsageAllowed, long ExpirationDays, bool PermanentData)
      : base(UsageAllowed, ExpirationDays, PermanentData)
    {
    }

    /// <summary>创建一个权限，该权限是当前权限与指定权限的并集。</summary>
    /// <returns>一个新权限，它表示当前权限与指定权限的并集。</returns>
    /// <param name="target">将与当前权限合并的权限。它必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。</exception>
    public override IPermission Union(IPermission target)
    {
      if (target == null)
        return this.Copy();
      if (!this.VerifyType(target))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      IsolatedStorageFilePermission storageFilePermission1 = (IsolatedStorageFilePermission) target;
      if (this.IsUnrestricted() || storageFilePermission1.IsUnrestricted())
        return (IPermission) new IsolatedStorageFilePermission(PermissionState.Unrestricted);
      IsolatedStorageFilePermission storageFilePermission2 = new IsolatedStorageFilePermission(PermissionState.None);
      long num1 = IsolatedStoragePermission.max(this.m_userQuota, storageFilePermission1.m_userQuota);
      storageFilePermission2.m_userQuota = num1;
      long num2 = IsolatedStoragePermission.max(this.m_machineQuota, storageFilePermission1.m_machineQuota);
      storageFilePermission2.m_machineQuota = num2;
      long num3 = IsolatedStoragePermission.max(this.m_expirationDays, storageFilePermission1.m_expirationDays);
      storageFilePermission2.m_expirationDays = num3;
      int num4 = this.m_permanentData ? 1 : (storageFilePermission1.m_permanentData ? 1 : 0);
      storageFilePermission2.m_permanentData = num4 != 0;
      int num5 = (int) IsolatedStoragePermission.max((long) this.m_allowed, (long) storageFilePermission1.m_allowed);
      storageFilePermission2.m_allowed = (IsolatedStorageContainment) num5;
      return (IPermission) storageFilePermission2;
    }

    /// <summary>确定当前权限是否为指定权限的子集。</summary>
    /// <returns>如果当前权限是指定权限的子集，则为 true；否则为 false。</returns>
    /// <param name="target">将要测试子集关系的权限。此权限必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。</exception>
    public override bool IsSubsetOf(IPermission target)
    {
      if (target == null)
      {
        if (this.m_userQuota == 0L && this.m_machineQuota == 0L && (this.m_expirationDays == 0L && !this.m_permanentData))
          return this.m_allowed == IsolatedStorageContainment.None;
        return false;
      }
      try
      {
        IsolatedStorageFilePermission storageFilePermission = (IsolatedStorageFilePermission) target;
        if (storageFilePermission.IsUnrestricted())
          return true;
        return storageFilePermission.m_userQuota >= this.m_userQuota && storageFilePermission.m_machineQuota >= this.m_machineQuota && storageFilePermission.m_expirationDays >= this.m_expirationDays && (storageFilePermission.m_permanentData || !this.m_permanentData) && storageFilePermission.m_allowed >= this.m_allowed;
      }
      catch (InvalidCastException ex)
      {
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      }
    }

    /// <summary>创建并返回一个权限，该权限是当前权限和指定权限的交集。</summary>
    /// <returns>一个新权限，它表示当前权限与指定权限的交集。如果交集为空，则此新权限为 null。</returns>
    /// <param name="target">与当前权限对象相交的权限。它必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。</exception>
    public override IPermission Intersect(IPermission target)
    {
      if (target == null)
        return (IPermission) null;
      if (!this.VerifyType(target))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      IsolatedStorageFilePermission storageFilePermission1 = (IsolatedStorageFilePermission) target;
      if (storageFilePermission1.IsUnrestricted())
        return this.Copy();
      if (this.IsUnrestricted())
        return target.Copy();
      IsolatedStorageFilePermission storageFilePermission2 = new IsolatedStorageFilePermission(PermissionState.None);
      storageFilePermission2.m_userQuota = IsolatedStoragePermission.min(this.m_userQuota, storageFilePermission1.m_userQuota);
      storageFilePermission2.m_machineQuota = IsolatedStoragePermission.min(this.m_machineQuota, storageFilePermission1.m_machineQuota);
      storageFilePermission2.m_expirationDays = IsolatedStoragePermission.min(this.m_expirationDays, storageFilePermission1.m_expirationDays);
      storageFilePermission2.m_permanentData = this.m_permanentData && storageFilePermission1.m_permanentData;
      storageFilePermission2.m_allowed = (IsolatedStorageContainment) IsolatedStoragePermission.min((long) this.m_allowed, (long) storageFilePermission1.m_allowed);
      if (storageFilePermission2.m_userQuota == 0L && storageFilePermission2.m_machineQuota == 0L && (storageFilePermission2.m_expirationDays == 0L && !storageFilePermission2.m_permanentData) && storageFilePermission2.m_allowed == IsolatedStorageContainment.None)
        return (IPermission) null;
      return (IPermission) storageFilePermission2;
    }

    /// <summary>创建并返回当前权限的相同副本。</summary>
    /// <returns>当前权限的副本。</returns>
    public override IPermission Copy()
    {
      IsolatedStorageFilePermission storageFilePermission = new IsolatedStorageFilePermission(PermissionState.Unrestricted);
      if (!this.IsUnrestricted())
      {
        storageFilePermission.m_userQuota = this.m_userQuota;
        storageFilePermission.m_machineQuota = this.m_machineQuota;
        storageFilePermission.m_expirationDays = this.m_expirationDays;
        storageFilePermission.m_permanentData = this.m_permanentData;
        storageFilePermission.m_allowed = this.m_allowed;
      }
      return (IPermission) storageFilePermission;
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return IsolatedStorageFilePermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 3;
    }

    /// <summary>创建权限及其当前状态的 XML 编码。</summary>
    /// <returns>权限的 XML 编码，包括任何状态信息。</returns>
    [ComVisible(false)]
    public override SecurityElement ToXml()
    {
      return this.ToXml("System.Security.Permissions.IsolatedStorageFilePermission");
    }
  }
}
