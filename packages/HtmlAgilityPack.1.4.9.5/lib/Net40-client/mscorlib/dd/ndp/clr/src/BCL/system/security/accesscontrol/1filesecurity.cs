// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.FileSystemAuditRule
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>表示定义文件或目录的审核规则的访问控制项 (ACE) 的抽象。此类不能被继承。</summary>
  public sealed class FileSystemAuditRule : AuditRule
  {
    /// <summary>获取与当前 <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> 对象关联的 <see cref="T:System.Security.AccessControl.FileSystemRights" /> 标志。</summary>
    /// <returns>与当前 <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> 对象关联的 <see cref="T:System.Security.AccessControl.FileSystemRights" /> 标志。</returns>
    public FileSystemRights FileSystemRights
    {
      get
      {
        return FileSystemAccessRule.RightsFromAccessMask(this.AccessMask);
      }
    }

    /// <summary>使用以下内容初始化 <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> 类的新实例：对用户帐户的引用、指定与审核规则关联的操作的类型的值，以及指定何时执行审核的值。</summary>
    /// <param name="identity">封装对用户帐户的引用的 <see cref="T:System.Security.Principal.IdentityReference" /> 对象。</param>
    /// <param name="fileSystemRights">
    /// <see cref="T:System.Security.AccessControl.FileSystemRights" /> 值之一，该值指定与审核规则关联的操作的类型。</param>
    /// <param name="flags">
    /// <see cref="T:System.Security.AccessControl.AuditFlags" /> 值之一，该值指定何时执行审核。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="identity" /> 参数不是一个 <see cref="T:System.Security.Principal.IdentityReference" /> 对象。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="identity" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">一个错误枚举被传递给 <paramref name="flags" /> 参数。- 或 -<see cref="F:System.Security.AccessControl.AuditFlags.None" /> 值被传递给 <paramref name="flags" /> 参数。</exception>
    public FileSystemAuditRule(IdentityReference identity, FileSystemRights fileSystemRights, AuditFlags flags)
      : this(identity, fileSystemRights, InheritanceFlags.None, PropagationFlags.None, flags)
    {
    }

    /// <summary>使用以下内容初始化 <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> 类的新实例：对用户帐户的引用的名称、指定与审核规则关联的操作的类型的值、确定如何继承权限的值、确定如何传播权限的值，以及指定何时执行审核的值。</summary>
    /// <param name="identity">封装对用户帐户的引用的 <see cref="T:System.Security.Principal.IdentityReference" /> 对象。</param>
    /// <param name="fileSystemRights">
    /// <see cref="T:System.Security.AccessControl.FileSystemRights" /> 值之一，该值指定与审核规则关联的操作的类型。</param>
    /// <param name="inheritanceFlags">
    /// <see cref="T:System.Security.AccessControl.InheritanceFlags" /> 值之一，该值指定访问掩码如何传播到子对象。</param>
    /// <param name="propagationFlags">
    /// <see cref="T:System.Security.AccessControl.PropagationFlags" /> 值之一，该值指定访问控制项 (ACE) 如何传播到子对象。</param>
    /// <param name="flags">
    /// <see cref="T:System.Security.AccessControl.AuditFlags" /> 值之一，该值指定何时执行审核。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="identity" /> 参数不是一个 <see cref="T:System.Security.Principal.IdentityReference" /> 对象。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="identity" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">一个错误枚举被传递给 <paramref name="flags" /> 参数。- 或 -<see cref="F:System.Security.AccessControl.AuditFlags.None" /> 值被传递给 <paramref name="flags" /> 参数。</exception>
    public FileSystemAuditRule(IdentityReference identity, FileSystemRights fileSystemRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
      : this(identity, FileSystemAuditRule.AccessMaskFromRights(fileSystemRights), false, inheritanceFlags, propagationFlags, flags)
    {
    }

    /// <summary>使用以下内容初始化 <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> 类的新实例：用户帐户名称、指定与审核规则关联的操作的类型的值，以及指定何时执行审核的值。</summary>
    /// <param name="identity">用户帐户的名称。</param>
    /// <param name="fileSystemRights">
    /// <see cref="T:System.Security.AccessControl.FileSystemRights" /> 值之一，该值指定与审核规则关联的操作的类型。</param>
    /// <param name="flags">
    /// <see cref="T:System.Security.AccessControl.AuditFlags" /> 值之一，该值指定何时执行审核。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">一个错误枚举被传递给 <paramref name="flags" /> 参数。- 或 -<see cref="F:System.Security.AccessControl.AuditFlags.None" /> 值被传递给 <paramref name="flags" /> 参数。</exception>
    public FileSystemAuditRule(string identity, FileSystemRights fileSystemRights, AuditFlags flags)
      : this((IdentityReference) new NTAccount(identity), fileSystemRights, InheritanceFlags.None, PropagationFlags.None, flags)
    {
    }

    /// <summary>使用以下内容初始化 <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> 类的新实例：用户帐户的名称、指定与审核规则关联的操作的类型的值、确定如何继承权限的值、确定如何传播权限的值，以及指定何时执行审核的值。</summary>
    /// <param name="identity">用户帐户的名称。</param>
    /// <param name="fileSystemRights">
    /// <see cref="T:System.Security.AccessControl.FileSystemRights" /> 值之一，该值指定与审核规则关联的操作的类型。</param>
    /// <param name="inheritanceFlags">
    /// <see cref="T:System.Security.AccessControl.InheritanceFlags" /> 值之一，该值指定访问掩码如何传播到子对象。</param>
    /// <param name="propagationFlags">
    /// <see cref="T:System.Security.AccessControl.PropagationFlags" /> 值之一，该值指定访问控制项 (ACE) 如何传播到子对象。</param>
    /// <param name="flags">
    /// <see cref="T:System.Security.AccessControl.AuditFlags" /> 值之一，该值指定何时执行审核。</param>
    public FileSystemAuditRule(string identity, FileSystemRights fileSystemRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
      : this((IdentityReference) new NTAccount(identity), FileSystemAuditRule.AccessMaskFromRights(fileSystemRights), false, inheritanceFlags, propagationFlags, flags)
    {
    }

    internal FileSystemAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
      : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, flags)
    {
    }

    private static int AccessMaskFromRights(FileSystemRights fileSystemRights)
    {
      if (fileSystemRights < (FileSystemRights) 0 || fileSystemRights > FileSystemRights.FullControl)
        throw new ArgumentOutOfRangeException("fileSystemRights", Environment.GetResourceString("Argument_InvalidEnumValue", (object) fileSystemRights, (object) "FileSystemRights"));
      return (int) fileSystemRights;
    }
  }
}
