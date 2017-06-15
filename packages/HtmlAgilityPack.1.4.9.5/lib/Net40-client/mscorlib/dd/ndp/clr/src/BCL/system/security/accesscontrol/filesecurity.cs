// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.FileSystemAccessRule
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>表示定义文件或目录的访问规则的访问控制项 (ACE) 的抽象。此类不能被继承。</summary>
  public sealed class FileSystemAccessRule : AccessRule
  {
    /// <summary>获取与当前 <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> 对象关联的 <see cref="T:System.Security.AccessControl.FileSystemRights" /> 标志。</summary>
    /// <returns>与当前 <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> 对象关联的 <see cref="T:System.Security.AccessControl.FileSystemRights" /> 标志。</returns>
    public FileSystemRights FileSystemRights
    {
      get
      {
        return FileSystemAccessRule.RightsFromAccessMask(this.AccessMask);
      }
    }

    /// <summary>使用以下内容初始化 <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> 类的新实例：对用户帐户的引用、指定与访问规则关联的操作的类型的值，以及指定是允许还是拒绝该操作的值。</summary>
    /// <param name="identity">封装对用户帐户的引用的 <see cref="T:System.Security.Principal.IdentityReference" /> 对象。</param>
    /// <param name="fileSystemRights">
    /// <see cref="T:System.Security.AccessControl.FileSystemRights" /> 值之一，该值指定与访问规则关联的操作的类型。</param>
    /// <param name="type">
    /// <see cref="T:System.Security.AccessControl.AccessControlType" /> 值之一，该值指定是允许还是拒绝该操作。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="identity" /> 参数不是一个 <see cref="T:System.Security.Principal.IdentityReference" /> 对象。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="identity" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">一个错误枚举被传递给 <paramref name="type " /> 参数。</exception>
    public FileSystemAccessRule(IdentityReference identity, FileSystemRights fileSystemRights, AccessControlType type)
      : this(identity, FileSystemAccessRule.AccessMaskFromRights(fileSystemRights, type), false, InheritanceFlags.None, PropagationFlags.None, type)
    {
    }

    /// <summary>使用以下内容初始化 <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> 类的新实例：用户帐户的名称、指定与访问规则关联的操作的类型的值，以及描述是允许还是拒绝该操作的值。</summary>
    /// <param name="identity">用户帐户的名称。</param>
    /// <param name="fileSystemRights">
    /// <see cref="T:System.Security.AccessControl.FileSystemRights" /> 值之一，该值指定与访问规则关联的操作的类型。</param>
    /// <param name="type">
    /// <see cref="T:System.Security.AccessControl.AccessControlType" /> 值之一，该值指定是允许还是拒绝该操作。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="identity" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">一个错误枚举被传递给 <paramref name="type " /> 参数。</exception>
    public FileSystemAccessRule(string identity, FileSystemRights fileSystemRights, AccessControlType type)
      : this((IdentityReference) new NTAccount(identity), FileSystemAccessRule.AccessMaskFromRights(fileSystemRights, type), false, InheritanceFlags.None, PropagationFlags.None, type)
    {
    }

    /// <summary>使用以下内容初始化 <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> 类的新实例：对用户帐户的引用、指定与访问规则关联的操作的类型的值、确定如何继承权限的值、确定如何传播权限的值，以及指定是允许还是拒绝该操作的值。</summary>
    /// <param name="identity">封装对用户帐户的引用的 <see cref="T:System.Security.Principal.IdentityReference" /> 对象。</param>
    /// <param name="fileSystemRights">
    /// <see cref="T:System.Security.AccessControl.FileSystemRights" /> 值之一，该值指定与访问规则关联的操作的类型。</param>
    /// <param name="inheritanceFlags">
    /// <see cref="T:System.Security.AccessControl.InheritanceFlags" /> 值之一，该值指定访问掩码如何传播到子对象。</param>
    /// <param name="propagationFlags">
    /// <see cref="T:System.Security.AccessControl.PropagationFlags" /> 值之一，该值指定访问控制项 (ACE) 如何传播到子对象。</param>
    /// <param name="type">
    /// <see cref="T:System.Security.AccessControl.AccessControlType" /> 值之一，该值指定是允许还是拒绝该操作。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="identity" /> 参数不是一个 <see cref="T:System.Security.Principal.IdentityReference" /> 对象。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="identity" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">一个错误枚举被传递给 <paramref name="type " /> 参数。- 或 -一个错误枚举被传递给 <paramref name="inheritanceFlags " /> 参数。- 或 -一个错误枚举被传递给 <paramref name="propagationFlags " /> 参数。</exception>
    public FileSystemAccessRule(IdentityReference identity, FileSystemRights fileSystemRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
      : this(identity, FileSystemAccessRule.AccessMaskFromRights(fileSystemRights, type), false, inheritanceFlags, propagationFlags, type)
    {
    }

    /// <summary>使用以下内容初始化 <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> 类的新实例：用户帐户的名称、指定与访问规则关联的操作的类型的值、确定如何继承权限的值、确定如何传播权限的值，以及指定是允许还是拒绝该操作的值。</summary>
    /// <param name="identity">用户帐户的名称。</param>
    /// <param name="fileSystemRights">
    /// <see cref="T:System.Security.AccessControl.FileSystemRights" /> 值之一，该值指定与访问规则关联的操作的类型。</param>
    /// <param name="inheritanceFlags">
    /// <see cref="T:System.Security.AccessControl.InheritanceFlags" /> 值之一，该值指定访问掩码如何传播到子对象。</param>
    /// <param name="propagationFlags">
    /// <see cref="T:System.Security.AccessControl.PropagationFlags" /> 值之一，该值指定访问控制项 (ACE) 如何传播到子对象。</param>
    /// <param name="type">
    /// <see cref="T:System.Security.AccessControl.AccessControlType" /> 值之一，该值指定是允许还是拒绝该操作。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="identity" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">一个错误枚举被传递给 <paramref name="type " /> 参数。- 或 -一个错误枚举被传递给 <paramref name="inheritanceFlags " /> 参数。- 或 -一个错误枚举被传递给 <paramref name="propagationFlags " /> 参数。</exception>
    public FileSystemAccessRule(string identity, FileSystemRights fileSystemRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
      : this((IdentityReference) new NTAccount(identity), FileSystemAccessRule.AccessMaskFromRights(fileSystemRights, type), false, inheritanceFlags, propagationFlags, type)
    {
    }

    internal FileSystemAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
      : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
    {
    }

    internal static int AccessMaskFromRights(FileSystemRights fileSystemRights, AccessControlType controlType)
    {
      if (fileSystemRights < (FileSystemRights) 0 || fileSystemRights > FileSystemRights.FullControl)
        throw new ArgumentOutOfRangeException("fileSystemRights", Environment.GetResourceString("Argument_InvalidEnumValue", (object) fileSystemRights, (object) "FileSystemRights"));
      if (controlType == AccessControlType.Allow)
        fileSystemRights |= FileSystemRights.Synchronize;
      else if (controlType == AccessControlType.Deny && fileSystemRights != FileSystemRights.FullControl && fileSystemRights != (FileSystemRights.Modify | FileSystemRights.ChangePermissions | FileSystemRights.TakeOwnership | FileSystemRights.Synchronize))
        fileSystemRights &= ~FileSystemRights.Synchronize;
      return (int) fileSystemRights;
    }

    internal static FileSystemRights RightsFromAccessMask(int accessMask)
    {
      return (FileSystemRights) accessMask;
    }
  }
}
