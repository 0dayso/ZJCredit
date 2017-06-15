// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.FileDialogPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>控制通过“文件”对话框访问文件或文件夹的能力。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class FileDialogPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
  {
    private FileDialogPermissionAccess access;

    /// <summary>获取或设置对文件的允许的访问权限。</summary>
    /// <returns>对文件的允许的访问权限。</returns>
    /// <exception cref="T:System.ArgumentException">尝试将 <paramref name="access" /> 参数设置为一个值，该值不是 <see cref="T:System.Security.Permissions.FileDialogPermissionAccess" /> 值的有效组合。</exception>
    public FileDialogPermissionAccess Access
    {
      get
      {
        return this.access;
      }
      set
      {
        FileDialogPermission.VerifyAccess(value);
        this.access = value;
      }
    }

    /// <summary>用指定的受限制或无限制的权限初始化 <see cref="T:System.Security.Permissions.FileDialogPermission" /> 类的新实例。</summary>
    /// <param name="state">
    /// <see cref="T:System.Security.Permissions.PermissionState" /> 值之一（Unrestricted 或 None）。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="state" /> 参数不是有效的 <see cref="T:System.Security.Permissions.PermissionState" /> 值。</exception>
    public FileDialogPermission(PermissionState state)
    {
      if (state == PermissionState.Unrestricted)
      {
        this.SetUnrestricted(true);
      }
      else
      {
        if (state != PermissionState.None)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
        this.SetUnrestricted(false);
        this.Reset();
      }
    }

    /// <summary>用指定的访问权限初始化 <see cref="T:System.Security.Permissions.FileDialogPermission" /> 类的新实例。</summary>
    /// <param name="access">
    /// <see cref="T:System.Security.Permissions.FileDialogPermissionAccess" /> 值的按位组合。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="access" /> 参数不是 <see cref="T:System.Security.Permissions.FileDialogPermissionAccess" /> 值的有效组合。</exception>
    public FileDialogPermission(FileDialogPermissionAccess access)
    {
      FileDialogPermission.VerifyAccess(access);
      this.access = access;
    }

    /// <summary>创建并返回当前权限的相同副本。</summary>
    /// <returns>当前权限的副本。</returns>
    public override IPermission Copy()
    {
      return (IPermission) new FileDialogPermission(this.access);
    }

    /// <summary>从 XML 编码重新构造具有指定状态的权限。</summary>
    /// <param name="esd">用于重新构造权限的 XML 编码。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="esd" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="esd" /> 参数不是有效的权限元素。- 或 -不支持 <paramref name="esd" /> 参数的版本号。</exception>
    public override void FromXml(SecurityElement esd)
    {
      CodeAccessPermission.ValidateElement(esd, (IPermission) this);
      if (XMLUtil.IsUnrestricted(esd))
      {
        this.SetUnrestricted(true);
      }
      else
      {
        this.access = FileDialogPermissionAccess.None;
        string str = esd.Attribute("Access");
        if (str == null)
          return;
        this.access = (FileDialogPermissionAccess) Enum.Parse(typeof (FileDialogPermissionAccess), str);
      }
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return FileDialogPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 1;
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
      if (!this.VerifyType(target))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      FileDialogPermissionAccess access = this.access & ((FileDialogPermission) target).Access;
      if (access == FileDialogPermissionAccess.None)
        return (IPermission) null;
      return (IPermission) new FileDialogPermission(access);
    }

    /// <summary>确定当前权限是否为指定权限的子集。</summary>
    /// <returns>如果当前权限是指定权限的子集，则为 true；否则为 false。</returns>
    /// <param name="target">将要测试子集关系的权限。此权限必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。</exception>
    public override bool IsSubsetOf(IPermission target)
    {
      if (target == null)
        return this.access == FileDialogPermissionAccess.None;
      try
      {
        FileDialogPermission dialogPermission = (FileDialogPermission) target;
        if (dialogPermission.IsUnrestricted())
          return true;
        if (this.IsUnrestricted())
          return false;
        int num1 = (int) (this.access & FileDialogPermissionAccess.Open);
        int num2 = (int) (this.access & FileDialogPermissionAccess.Save);
        int num3 = (int) (dialogPermission.Access & FileDialogPermissionAccess.Open);
        int num4 = (int) (dialogPermission.Access & FileDialogPermissionAccess.Save);
        int num5 = num3;
        return num1 <= num5 && num2 <= num4;
      }
      catch (InvalidCastException ex)
      {
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      }
    }

    /// <summary>返回一个值，该值指示当前权限是否为无限制的。</summary>
    /// <returns>如果当前权限是无限制的，则为 true；否则为 false。</returns>
    public bool IsUnrestricted()
    {
      return this.access == FileDialogPermissionAccess.OpenSave;
    }

    private void Reset()
    {
      this.access = FileDialogPermissionAccess.None;
    }

    private void SetUnrestricted(bool unrestricted)
    {
      if (!unrestricted)
        return;
      this.access = FileDialogPermissionAccess.OpenSave;
    }

    /// <summary>创建权限及其当前状态的 XML 编码。</summary>
    /// <returns>权限的 XML 编码，包括状态信息。</returns>
    public override SecurityElement ToXml()
    {
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.FileDialogPermission");
      if (!this.IsUnrestricted())
      {
        if (this.access != FileDialogPermissionAccess.None)
          permissionElement.AddAttribute("Access", Enum.GetName(typeof (FileDialogPermissionAccess), (object) this.access));
      }
      else
        permissionElement.AddAttribute("Unrestricted", "true");
      return permissionElement;
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
      return (IPermission) new FileDialogPermission(this.access | ((FileDialogPermission) target).Access);
    }

    private static void VerifyAccess(FileDialogPermissionAccess access)
    {
      if ((access & ~FileDialogPermissionAccess.OpenSave) != FileDialogPermissionAccess.None)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) access));
    }
  }
}
