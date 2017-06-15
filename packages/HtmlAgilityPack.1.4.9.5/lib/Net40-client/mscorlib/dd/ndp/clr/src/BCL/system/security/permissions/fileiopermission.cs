// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.FileIOPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>控制访问文件和文件夹的能力。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class FileIOPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
  {
    private FileIOAccess m_read;
    private FileIOAccess m_write;
    private FileIOAccess m_append;
    private FileIOAccess m_pathDiscovery;
    [OptionalField(VersionAdded = 2)]
    private FileIOAccess m_viewAcl;
    [OptionalField(VersionAdded = 2)]
    private FileIOAccess m_changeAcl;
    private bool m_unrestricted;

    /// <summary>获取或设置所有本地文件的允许访问权限。</summary>
    /// <returns>所有本地文件的文件 I/O 标志集合。</returns>
    public FileIOPermissionAccess AllLocalFiles
    {
      get
      {
        if (this.m_unrestricted)
          return FileIOPermissionAccess.AllAccess;
        FileIOPermissionAccess permissionAccess = FileIOPermissionAccess.NoAccess;
        if (this.m_read != null && this.m_read.AllLocalFiles)
          permissionAccess |= FileIOPermissionAccess.Read;
        if (this.m_write != null && this.m_write.AllLocalFiles)
          permissionAccess |= FileIOPermissionAccess.Write;
        if (this.m_append != null && this.m_append.AllLocalFiles)
          permissionAccess |= FileIOPermissionAccess.Append;
        if (this.m_pathDiscovery != null && this.m_pathDiscovery.AllLocalFiles)
          permissionAccess |= FileIOPermissionAccess.PathDiscovery;
        return permissionAccess;
      }
      set
      {
        if ((value & FileIOPermissionAccess.Read) != FileIOPermissionAccess.NoAccess)
        {
          if (this.m_read == null)
            this.m_read = new FileIOAccess();
          this.m_read.AllLocalFiles = true;
        }
        else if (this.m_read != null)
          this.m_read.AllLocalFiles = false;
        if ((value & FileIOPermissionAccess.Write) != FileIOPermissionAccess.NoAccess)
        {
          if (this.m_write == null)
            this.m_write = new FileIOAccess();
          this.m_write.AllLocalFiles = true;
        }
        else if (this.m_write != null)
          this.m_write.AllLocalFiles = false;
        if ((value & FileIOPermissionAccess.Append) != FileIOPermissionAccess.NoAccess)
        {
          if (this.m_append == null)
            this.m_append = new FileIOAccess();
          this.m_append.AllLocalFiles = true;
        }
        else if (this.m_append != null)
          this.m_append.AllLocalFiles = false;
        if ((value & FileIOPermissionAccess.PathDiscovery) != FileIOPermissionAccess.NoAccess)
        {
          if (this.m_pathDiscovery == null)
            this.m_pathDiscovery = new FileIOAccess(true);
          this.m_pathDiscovery.AllLocalFiles = true;
        }
        else
        {
          if (this.m_pathDiscovery == null)
            return;
          this.m_pathDiscovery.AllLocalFiles = false;
        }
      }
    }

    /// <summary>获取或设置对所有文件的允许访问权限。</summary>
    /// <returns>所有文件的文件 I/O 标志集合。</returns>
    public FileIOPermissionAccess AllFiles
    {
      get
      {
        if (this.m_unrestricted)
          return FileIOPermissionAccess.AllAccess;
        FileIOPermissionAccess permissionAccess = FileIOPermissionAccess.NoAccess;
        if (this.m_read != null && this.m_read.AllFiles)
          permissionAccess |= FileIOPermissionAccess.Read;
        if (this.m_write != null && this.m_write.AllFiles)
          permissionAccess |= FileIOPermissionAccess.Write;
        if (this.m_append != null && this.m_append.AllFiles)
          permissionAccess |= FileIOPermissionAccess.Append;
        if (this.m_pathDiscovery != null && this.m_pathDiscovery.AllFiles)
          permissionAccess |= FileIOPermissionAccess.PathDiscovery;
        return permissionAccess;
      }
      set
      {
        if (value == FileIOPermissionAccess.AllAccess)
        {
          this.m_unrestricted = true;
        }
        else
        {
          if ((value & FileIOPermissionAccess.Read) != FileIOPermissionAccess.NoAccess)
          {
            if (this.m_read == null)
              this.m_read = new FileIOAccess();
            this.m_read.AllFiles = true;
          }
          else if (this.m_read != null)
            this.m_read.AllFiles = false;
          if ((value & FileIOPermissionAccess.Write) != FileIOPermissionAccess.NoAccess)
          {
            if (this.m_write == null)
              this.m_write = new FileIOAccess();
            this.m_write.AllFiles = true;
          }
          else if (this.m_write != null)
            this.m_write.AllFiles = false;
          if ((value & FileIOPermissionAccess.Append) != FileIOPermissionAccess.NoAccess)
          {
            if (this.m_append == null)
              this.m_append = new FileIOAccess();
            this.m_append.AllFiles = true;
          }
          else if (this.m_append != null)
            this.m_append.AllFiles = false;
          if ((value & FileIOPermissionAccess.PathDiscovery) != FileIOPermissionAccess.NoAccess)
          {
            if (this.m_pathDiscovery == null)
              this.m_pathDiscovery = new FileIOAccess(true);
            this.m_pathDiscovery.AllFiles = true;
          }
          else
          {
            if (this.m_pathDiscovery == null)
              return;
            this.m_pathDiscovery.AllFiles = false;
          }
        }
      }
    }

    /// <summary>用指定的完全受限制或不受限制的权限初始化 <see cref="T:System.Security.Permissions.FileIOPermission" /> 类的新实例。</summary>
    /// <param name="state">
    /// <see cref="T:System.Security.Permissions.PermissionState" /> 枚举值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="state" /> 参数不是有效的 <see cref="T:System.Security.Permissions.PermissionState" /> 值。</exception>
    public FileIOPermission(PermissionState state)
    {
      if (state == PermissionState.Unrestricted)
      {
        this.m_unrestricted = true;
      }
      else
      {
        if (state != PermissionState.None)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
        this.m_unrestricted = false;
      }
    }

    /// <summary>用对指定文件或目录的指定访问权限初始化 <see cref="T:System.Security.Permissions.FileIOPermission" /> 类的新实例。</summary>
    /// <param name="access">
    /// <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> 枚举值的按位组合。</param>
    /// <param name="path">文件或目录的绝对路径。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="access" /> 参数不是有效的 <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> 值。- 或 -<paramref name="path" /> 参数不是一个有效的字符串。- 或 -<paramref name="path" /> 参数未指定文件或目录的绝对路径。</exception>
    [SecuritySafeCritical]
    public FileIOPermission(FileIOPermissionAccess access, string path)
    {
      FileIOPermission.VerifyAccess(access);
      string[] pathListOrig = new string[1]{ path };
      this.AddPathList(access, pathListOrig, false, true, false);
    }

    /// <summary>用对指定文件和目录的指定访问权限初始化 <see cref="T:System.Security.Permissions.FileIOPermission" /> 类的新实例。</summary>
    /// <param name="access">
    /// <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> 枚举值的按位组合。</param>
    /// <param name="pathList">一个数组，它包含文件和目录的绝对路径。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="access" /> 参数不是有效的 <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> 值。- 或 -<paramref name="pathList" /> 数组中的项不是有效的字符串。</exception>
    [SecuritySafeCritical]
    public FileIOPermission(FileIOPermissionAccess access, string[] pathList)
    {
      FileIOPermission.VerifyAccess(access);
      this.AddPathList(access, pathList, false, true, false);
    }

    /// <summary>使用对指定文件或目录的指定访问权限以及对文件控制信息的指定访问权限，初始化 <see cref="T:System.Security.Permissions.FileIOPermission" /> 类的新实例。</summary>
    /// <param name="access">
    /// <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> 枚举值的按位组合。</param>
    /// <param name="control">
    /// <see cref="T:System.Security.AccessControl.AccessControlActions" /> 枚举值的按位组合。</param>
    /// <param name="path">文件或目录的绝对路径。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="access" /> 参数不是有效的 <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> 值。- 或 -<paramref name="path" /> 参数不是一个有效的字符串。- 或 -<paramref name="path" /> 参数未指定文件或目录的绝对路径。</exception>
    [SecuritySafeCritical]
    public FileIOPermission(FileIOPermissionAccess access, AccessControlActions control, string path)
    {
      FileIOPermission.VerifyAccess(access);
      string[] pathListOrig = new string[1]{ path };
      this.AddPathList(access, control, pathListOrig, false, true, false);
    }

    /// <summary>使用对指定文件和目录的指定访问权限以及对文件控制信息的指定访问权限，初始化 <see cref="T:System.Security.Permissions.FileIOPermission" /> 类的新实例。</summary>
    /// <param name="access">
    /// <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> 枚举值的按位组合。</param>
    /// <param name="control">
    /// <see cref="T:System.Security.AccessControl.AccessControlActions" /> 枚举值的按位组合。</param>
    /// <param name="pathList">一个数组，它包含文件和目录的绝对路径。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="access" /> 参数不是有效的 <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> 值。- 或 -<paramref name="pathList" /> 数组中的项不是有效的字符串。</exception>
    [SecuritySafeCritical]
    public FileIOPermission(FileIOPermissionAccess access, AccessControlActions control, string[] pathList)
      : this(access, control, pathList, true, true)
    {
    }

    [SecurityCritical]
    internal FileIOPermission(FileIOPermissionAccess access, string[] pathList, bool checkForDuplicates, bool needFullPath)
    {
      FileIOPermission.VerifyAccess(access);
      this.AddPathList(access, pathList, checkForDuplicates, needFullPath, true);
    }

    [SecurityCritical]
    internal FileIOPermission(FileIOPermissionAccess access, AccessControlActions control, string[] pathList, bool checkForDuplicates, bool needFullPath)
    {
      FileIOPermission.VerifyAccess(access);
      this.AddPathList(access, control, pathList, checkForDuplicates, needFullPath, true);
    }

    /// <summary>设置对指定文件或目录的指定访问权限，同时替换该权限的现有状态。</summary>
    /// <param name="access">
    /// <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> 值的按位组合。</param>
    /// <param name="path">文件或目录的绝对路径。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="access" /> 参数不是有效的 <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> 值。- 或 -<paramref name="path" /> 参数不是一个有效的字符串。- 或 -<paramref name="path" /> 参数未指定文件或目录的绝对路径。</exception>
    public void SetPathList(FileIOPermissionAccess access, string path)
    {
      string[] pathList;
      if (path == null)
        pathList = new string[0];
      else
        pathList = new string[1]{ path };
      this.SetPathList(access, pathList, false);
    }

    /// <summary>设置对指定文件和目录的指定访问权限，同时用一组新路径替换指定访问权限的当前状态。</summary>
    /// <param name="access">
    /// <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> 值的按位组合。</param>
    /// <param name="pathList">一个数组，它包含文件和目录的绝对路径。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="access" /> 参数不是有效的 <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> 值。- 或 -<paramref name="pathList" /> 参数中的项不是有效的字符串。</exception>
    public void SetPathList(FileIOPermissionAccess access, string[] pathList)
    {
      this.SetPathList(access, pathList, true);
    }

    internal void SetPathList(FileIOPermissionAccess access, string[] pathList, bool checkForDuplicates)
    {
      this.SetPathList(access, AccessControlActions.None, pathList, checkForDuplicates);
    }

    [SecuritySafeCritical]
    internal void SetPathList(FileIOPermissionAccess access, AccessControlActions control, string[] pathList, bool checkForDuplicates)
    {
      FileIOPermission.VerifyAccess(access);
      if ((access & FileIOPermissionAccess.Read) != FileIOPermissionAccess.NoAccess)
        this.m_read = (FileIOAccess) null;
      if ((access & FileIOPermissionAccess.Write) != FileIOPermissionAccess.NoAccess)
        this.m_write = (FileIOAccess) null;
      if ((access & FileIOPermissionAccess.Append) != FileIOPermissionAccess.NoAccess)
        this.m_append = (FileIOAccess) null;
      if ((access & FileIOPermissionAccess.PathDiscovery) != FileIOPermissionAccess.NoAccess)
        this.m_pathDiscovery = (FileIOAccess) null;
      if ((control & AccessControlActions.View) != AccessControlActions.None)
        this.m_viewAcl = (FileIOAccess) null;
      if ((control & AccessControlActions.Change) != AccessControlActions.None)
        this.m_changeAcl = (FileIOAccess) null;
      this.m_unrestricted = false;
      this.AddPathList(access, control, pathList, checkForDuplicates, true, true);
    }

    /// <summary>将对指定文件或目录的访问权限添加到权限的现有状态。</summary>
    /// <param name="access">
    /// <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> 值的按位组合。</param>
    /// <param name="path">文件或目录的绝对路径。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="access" /> 参数不是有效的 <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> 值。- 或 -<paramref name="path" /> 参数不是一个有效的字符串。- 或 -<paramref name="path" /> 参数未指定文件或目录的绝对路径。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 参数为 null。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="path" /> 参数具有无效的格式。</exception>
    [SecuritySafeCritical]
    public void AddPathList(FileIOPermissionAccess access, string path)
    {
      string[] pathListOrig;
      if (path == null)
        pathListOrig = new string[0];
      else
        pathListOrig = new string[1]{ path };
      this.AddPathList(access, pathListOrig, false, true, false);
    }

    /// <summary>将对指定文件和目录的访问权限添加到权限的现有状态。</summary>
    /// <param name="access">
    /// <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> 值的按位组合。</param>
    /// <param name="pathList">一个数组，它包含文件和目录的绝对路径。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="access" /> 参数不是有效的 <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> 值。- 或 -<paramref name="pathList" /> 数组中的项无效。</exception>
    /// <exception cref="T:System.NotSupportedException">在 <paramref name="pathList" /> 数组中的条目有一个无效格式。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="pathList" /> 参数为 null。</exception>
    [SecuritySafeCritical]
    public void AddPathList(FileIOPermissionAccess access, string[] pathList)
    {
      this.AddPathList(access, pathList, true, true, true);
    }

    [SecurityCritical]
    internal void AddPathList(FileIOPermissionAccess access, string[] pathListOrig, bool checkForDuplicates, bool needFullPath, bool copyPathList)
    {
      this.AddPathList(access, AccessControlActions.None, pathListOrig, checkForDuplicates, needFullPath, copyPathList);
    }

    [SecurityCritical]
    internal void AddPathList(FileIOPermissionAccess access, AccessControlActions control, string[] pathListOrig, bool checkForDuplicates, bool needFullPath, bool copyPathList)
    {
      if (pathListOrig == null)
        throw new ArgumentNullException("pathList");
      if (pathListOrig.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      FileIOPermission.VerifyAccess(access);
      if (this.m_unrestricted)
        return;
      string[] str = pathListOrig;
      if (copyPathList)
      {
        str = new string[pathListOrig.Length];
        Array.Copy((Array) pathListOrig, (Array) str, pathListOrig.Length);
      }
      FileIOPermission.CheckIllegalCharacters(str);
      ArrayList listFromExpressions = StringExpressionSet.CreateListFromExpressions(str, needFullPath);
      if ((access & FileIOPermissionAccess.Read) != FileIOPermissionAccess.NoAccess)
      {
        if (this.m_read == null)
          this.m_read = new FileIOAccess();
        this.m_read.AddExpressions(listFromExpressions, checkForDuplicates);
      }
      if ((access & FileIOPermissionAccess.Write) != FileIOPermissionAccess.NoAccess)
      {
        if (this.m_write == null)
          this.m_write = new FileIOAccess();
        this.m_write.AddExpressions(listFromExpressions, checkForDuplicates);
      }
      if ((access & FileIOPermissionAccess.Append) != FileIOPermissionAccess.NoAccess)
      {
        if (this.m_append == null)
          this.m_append = new FileIOAccess();
        this.m_append.AddExpressions(listFromExpressions, checkForDuplicates);
      }
      if ((access & FileIOPermissionAccess.PathDiscovery) != FileIOPermissionAccess.NoAccess)
      {
        if (this.m_pathDiscovery == null)
          this.m_pathDiscovery = new FileIOAccess(true);
        this.m_pathDiscovery.AddExpressions(listFromExpressions, checkForDuplicates);
      }
      if ((control & AccessControlActions.View) != AccessControlActions.None)
      {
        if (this.m_viewAcl == null)
          this.m_viewAcl = new FileIOAccess();
        this.m_viewAcl.AddExpressions(listFromExpressions, checkForDuplicates);
      }
      if ((control & AccessControlActions.Change) == AccessControlActions.None)
        return;
      if (this.m_changeAcl == null)
        this.m_changeAcl = new FileIOAccess();
      this.m_changeAcl.AddExpressions(listFromExpressions, checkForDuplicates);
    }

    /// <summary>获取具有指定 <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> 的所有文件和目录。</summary>
    /// <returns>一个包含文件和目录的路径的数组，用户被授予对这些文件和目录 <paramref name="access" /> 参数所指定的访问权限。</returns>
    /// <param name="access">表示单一类型的文件访问的 <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> 值。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="access" /> 不是有效的 <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> 值。- 或 -<paramref name="access" /> 为 <see cref="F:System.Security.Permissions.FileIOPermissionAccess.AllAccess" />（它表示多种类型的文件访问）或 <see cref="F:System.Security.Permissions.FileIOPermissionAccess.NoAccess" />（它不表示任何类型的文件访问）。</exception>
    [SecuritySafeCritical]
    public string[] GetPathList(FileIOPermissionAccess access)
    {
      FileIOPermission.VerifyAccess(access);
      FileIOPermission.ExclusiveAccess(access);
      if (FileIOPermission.AccessIsSet(access, FileIOPermissionAccess.Read))
      {
        if (this.m_read == null)
          return (string[]) null;
        return this.m_read.ToStringArray();
      }
      if (FileIOPermission.AccessIsSet(access, FileIOPermissionAccess.Write))
      {
        if (this.m_write == null)
          return (string[]) null;
        return this.m_write.ToStringArray();
      }
      if (FileIOPermission.AccessIsSet(access, FileIOPermissionAccess.Append))
      {
        if (this.m_append == null)
          return (string[]) null;
        return this.m_append.ToStringArray();
      }
      if (!FileIOPermission.AccessIsSet(access, FileIOPermissionAccess.PathDiscovery))
        return (string[]) null;
      if (this.m_pathDiscovery == null)
        return (string[]) null;
      return this.m_pathDiscovery.ToStringArray();
    }

    private static void VerifyAccess(FileIOPermissionAccess access)
    {
      if ((access & ~FileIOPermissionAccess.AllAccess) != FileIOPermissionAccess.NoAccess)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) access));
    }

    private static void ExclusiveAccess(FileIOPermissionAccess access)
    {
      if (access == FileIOPermissionAccess.NoAccess)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
      if ((access & access - 1) != FileIOPermissionAccess.NoAccess)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
    }

    private static void CheckIllegalCharacters(string[] str)
    {
      for (int index = 0; index < str.Length; ++index)
        Path.CheckInvalidPathChars(str[index], true);
    }

    private static bool AccessIsSet(FileIOPermissionAccess access, FileIOPermissionAccess question)
    {
      return (uint) (access & question) > 0U;
    }

    private bool IsEmpty()
    {
      if (this.m_unrestricted || this.m_read != null && !this.m_read.IsEmpty() || (this.m_write != null && !this.m_write.IsEmpty() || this.m_append != null && !this.m_append.IsEmpty()) || (this.m_pathDiscovery != null && !this.m_pathDiscovery.IsEmpty() || this.m_viewAcl != null && !this.m_viewAcl.IsEmpty()))
        return false;
      if (this.m_changeAcl != null)
        return this.m_changeAcl.IsEmpty();
      return true;
    }

    /// <summary>返回一个值，该值指示当前权限是否为无限制的。</summary>
    /// <returns>如果当前权限是无限制的，则为 true；否则为 false。</returns>
    public bool IsUnrestricted()
    {
      return this.m_unrestricted;
    }

    /// <summary>确定当前权限是否为指定权限的子集。</summary>
    /// <returns>如果当前权限是指定权限的子集，则为 true；否则为 false。</returns>
    /// <param name="target">将要测试子集关系的权限。此权限必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。</exception>
    public override bool IsSubsetOf(IPermission target)
    {
      if (target == null)
        return this.IsEmpty();
      FileIOPermission fileIoPermission = target as FileIOPermission;
      if (fileIoPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (fileIoPermission.IsUnrestricted())
        return true;
      if (this.IsUnrestricted() || this.m_read != null && !this.m_read.IsSubsetOf(fileIoPermission.m_read) || (this.m_write != null && !this.m_write.IsSubsetOf(fileIoPermission.m_write) || this.m_append != null && !this.m_append.IsSubsetOf(fileIoPermission.m_append)) || (this.m_pathDiscovery != null && !this.m_pathDiscovery.IsSubsetOf(fileIoPermission.m_pathDiscovery) || this.m_viewAcl != null && !this.m_viewAcl.IsSubsetOf(fileIoPermission.m_viewAcl)))
        return false;
      if (this.m_changeAcl != null)
        return this.m_changeAcl.IsSubsetOf(fileIoPermission.m_changeAcl);
      return true;
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
      FileIOPermission fileIoPermission = target as FileIOPermission;
      if (fileIoPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (this.IsUnrestricted())
        return target.Copy();
      if (fileIoPermission.IsUnrestricted())
        return this.Copy();
      FileIOAccess fileIoAccess1 = this.m_read == null ? (FileIOAccess) null : this.m_read.Intersect(fileIoPermission.m_read);
      FileIOAccess fileIoAccess2 = this.m_write == null ? (FileIOAccess) null : this.m_write.Intersect(fileIoPermission.m_write);
      FileIOAccess fileIoAccess3 = this.m_append == null ? (FileIOAccess) null : this.m_append.Intersect(fileIoPermission.m_append);
      FileIOAccess fileIoAccess4 = this.m_pathDiscovery == null ? (FileIOAccess) null : this.m_pathDiscovery.Intersect(fileIoPermission.m_pathDiscovery);
      FileIOAccess fileIoAccess5 = this.m_viewAcl == null ? (FileIOAccess) null : this.m_viewAcl.Intersect(fileIoPermission.m_viewAcl);
      FileIOAccess fileIoAccess6 = this.m_changeAcl == null ? (FileIOAccess) null : this.m_changeAcl.Intersect(fileIoPermission.m_changeAcl);
      if ((fileIoAccess1 == null || fileIoAccess1.IsEmpty()) && (fileIoAccess2 == null || fileIoAccess2.IsEmpty()) && ((fileIoAccess3 == null || fileIoAccess3.IsEmpty()) && (fileIoAccess4 == null || fileIoAccess4.IsEmpty())) && ((fileIoAccess5 == null || fileIoAccess5.IsEmpty()) && (fileIoAccess6 == null || fileIoAccess6.IsEmpty())))
        return (IPermission) null;
      return (IPermission) new FileIOPermission(PermissionState.None) { m_unrestricted = false, m_read = fileIoAccess1, m_write = fileIoAccess2, m_append = fileIoAccess3, m_pathDiscovery = fileIoAccess4, m_viewAcl = fileIoAccess5, m_changeAcl = fileIoAccess6 };
    }

    /// <summary>创建一个权限，该权限是当前权限与指定权限的并集。</summary>
    /// <returns>一个新权限，它表示当前权限与指定权限的并集。</returns>
    /// <param name="other">将与当前权限合并的权限。它必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="other" /> 参数不是 null，而且与当前权限不是同一类型。</exception>
    public override IPermission Union(IPermission other)
    {
      if (other == null)
        return this.Copy();
      FileIOPermission fileIoPermission = other as FileIOPermission;
      if (fileIoPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (this.IsUnrestricted() || fileIoPermission.IsUnrestricted())
        return (IPermission) new FileIOPermission(PermissionState.Unrestricted);
      FileIOAccess fileIoAccess1 = this.m_read == null ? fileIoPermission.m_read : this.m_read.Union(fileIoPermission.m_read);
      FileIOAccess fileIoAccess2 = this.m_write == null ? fileIoPermission.m_write : this.m_write.Union(fileIoPermission.m_write);
      FileIOAccess fileIoAccess3 = this.m_append == null ? fileIoPermission.m_append : this.m_append.Union(fileIoPermission.m_append);
      FileIOAccess fileIoAccess4 = this.m_pathDiscovery == null ? fileIoPermission.m_pathDiscovery : this.m_pathDiscovery.Union(fileIoPermission.m_pathDiscovery);
      FileIOAccess fileIoAccess5 = this.m_viewAcl == null ? fileIoPermission.m_viewAcl : this.m_viewAcl.Union(fileIoPermission.m_viewAcl);
      FileIOAccess fileIoAccess6 = this.m_changeAcl == null ? fileIoPermission.m_changeAcl : this.m_changeAcl.Union(fileIoPermission.m_changeAcl);
      if ((fileIoAccess1 == null || fileIoAccess1.IsEmpty()) && (fileIoAccess2 == null || fileIoAccess2.IsEmpty()) && ((fileIoAccess3 == null || fileIoAccess3.IsEmpty()) && (fileIoAccess4 == null || fileIoAccess4.IsEmpty())) && ((fileIoAccess5 == null || fileIoAccess5.IsEmpty()) && (fileIoAccess6 == null || fileIoAccess6.IsEmpty())))
        return (IPermission) null;
      return (IPermission) new FileIOPermission(PermissionState.None) { m_unrestricted = false, m_read = fileIoAccess1, m_write = fileIoAccess2, m_append = fileIoAccess3, m_pathDiscovery = fileIoAccess4, m_viewAcl = fileIoAccess5, m_changeAcl = fileIoAccess6 };
    }

    /// <summary>创建并返回当前权限的相同副本。</summary>
    /// <returns>当前权限的副本。</returns>
    public override IPermission Copy()
    {
      FileIOPermission fileIoPermission = new FileIOPermission(PermissionState.None);
      if (this.m_unrestricted)
      {
        fileIoPermission.m_unrestricted = true;
      }
      else
      {
        fileIoPermission.m_unrestricted = false;
        if (this.m_read != null)
          fileIoPermission.m_read = this.m_read.Copy();
        if (this.m_write != null)
          fileIoPermission.m_write = this.m_write.Copy();
        if (this.m_append != null)
          fileIoPermission.m_append = this.m_append.Copy();
        if (this.m_pathDiscovery != null)
          fileIoPermission.m_pathDiscovery = this.m_pathDiscovery.Copy();
        if (this.m_viewAcl != null)
          fileIoPermission.m_viewAcl = this.m_viewAcl.Copy();
        if (this.m_changeAcl != null)
          fileIoPermission.m_changeAcl = this.m_changeAcl.Copy();
      }
      return (IPermission) fileIoPermission;
    }

    /// <summary>创建权限及其当前状态的 XML 编码。</summary>
    /// <returns>权限的 XML 编码，包括任何状态信息。</returns>
    public override SecurityElement ToXml()
    {
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.FileIOPermission");
      if (!this.IsUnrestricted())
      {
        if (this.m_read != null && !this.m_read.IsEmpty())
          permissionElement.AddAttribute("Read", SecurityElement.Escape(this.m_read.ToString()));
        if (this.m_write != null && !this.m_write.IsEmpty())
          permissionElement.AddAttribute("Write", SecurityElement.Escape(this.m_write.ToString()));
        if (this.m_append != null && !this.m_append.IsEmpty())
          permissionElement.AddAttribute("Append", SecurityElement.Escape(this.m_append.ToString()));
        if (this.m_pathDiscovery != null && !this.m_pathDiscovery.IsEmpty())
          permissionElement.AddAttribute("PathDiscovery", SecurityElement.Escape(this.m_pathDiscovery.ToString()));
        if (this.m_viewAcl != null && !this.m_viewAcl.IsEmpty())
          permissionElement.AddAttribute("ViewAcl", SecurityElement.Escape(this.m_viewAcl.ToString()));
        if (this.m_changeAcl != null && !this.m_changeAcl.IsEmpty())
          permissionElement.AddAttribute("ChangeAcl", SecurityElement.Escape(this.m_changeAcl.ToString()));
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
    /// <paramref name="esd" /> 参数不是有效的权限元素。- 或 -<paramref name="esd" /> 参数的版本号不兼容。</exception>
    [SecuritySafeCritical]
    public override void FromXml(SecurityElement esd)
    {
      CodeAccessPermission.ValidateElement(esd, (IPermission) this);
      if (XMLUtil.IsUnrestricted(esd))
      {
        this.m_unrestricted = true;
      }
      else
      {
        this.m_unrestricted = false;
        string str1 = esd.Attribute("Read");
        this.m_read = str1 == null ? (FileIOAccess) null : new FileIOAccess(str1);
        string str2 = esd.Attribute("Write");
        this.m_write = str2 == null ? (FileIOAccess) null : new FileIOAccess(str2);
        string str3 = esd.Attribute("Append");
        this.m_append = str3 == null ? (FileIOAccess) null : new FileIOAccess(str3);
        string str4 = esd.Attribute("PathDiscovery");
        if (str4 != null)
        {
          this.m_pathDiscovery = new FileIOAccess(str4);
          this.m_pathDiscovery.PathDiscovery = true;
        }
        else
          this.m_pathDiscovery = (FileIOAccess) null;
        string str5 = esd.Attribute("ViewAcl");
        this.m_viewAcl = str5 == null ? (FileIOAccess) null : new FileIOAccess(str5);
        string str6 = esd.Attribute("ChangeAcl");
        if (str6 != null)
          this.m_changeAcl = new FileIOAccess(str6);
        else
          this.m_changeAcl = (FileIOAccess) null;
      }
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return FileIOPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 2;
    }

    /// <summary>确定指定的 <see cref="T:System.Security.Permissions.FileIOPermission" /> 对象是否等于当前的 <see cref="T:System.Security.Permissions.FileIOPermission" />。</summary>
    /// <returns>如果指定的 <see cref="T:System.Security.Permissions.FileIOPermission" /> 等于当前的 <see cref="T:System.Security.Permissions.FileIOPermission" /> 对象，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前的 <see cref="T:System.Security.Permissions.FileIOPermission" /> 进行比较的 <see cref="T:System.Security.Permissions.FileIOPermission" /> 对象。</param>
    [ComVisible(false)]
    public override bool Equals(object obj)
    {
      FileIOPermission fileIoPermission = obj as FileIOPermission;
      if (fileIoPermission == null)
        return false;
      if (this.m_unrestricted && fileIoPermission.m_unrestricted)
        return true;
      if (this.m_unrestricted != fileIoPermission.m_unrestricted)
        return false;
      if (this.m_read == null)
      {
        if (fileIoPermission.m_read != null && !fileIoPermission.m_read.IsEmpty())
          return false;
      }
      else if (!this.m_read.Equals((object) fileIoPermission.m_read))
        return false;
      if (this.m_write == null)
      {
        if (fileIoPermission.m_write != null && !fileIoPermission.m_write.IsEmpty())
          return false;
      }
      else if (!this.m_write.Equals((object) fileIoPermission.m_write))
        return false;
      if (this.m_append == null)
      {
        if (fileIoPermission.m_append != null && !fileIoPermission.m_append.IsEmpty())
          return false;
      }
      else if (!this.m_append.Equals((object) fileIoPermission.m_append))
        return false;
      if (this.m_pathDiscovery == null)
      {
        if (fileIoPermission.m_pathDiscovery != null && !fileIoPermission.m_pathDiscovery.IsEmpty())
          return false;
      }
      else if (!this.m_pathDiscovery.Equals((object) fileIoPermission.m_pathDiscovery))
        return false;
      if (this.m_viewAcl == null)
      {
        if (fileIoPermission.m_viewAcl != null && !fileIoPermission.m_viewAcl.IsEmpty())
          return false;
      }
      else if (!this.m_viewAcl.Equals((object) fileIoPermission.m_viewAcl))
        return false;
      if (this.m_changeAcl == null)
      {
        if (fileIoPermission.m_changeAcl != null && !fileIoPermission.m_changeAcl.IsEmpty())
          return false;
      }
      else if (!this.m_changeAcl.Equals((object) fileIoPermission.m_changeAcl))
        return false;
      return true;
    }

    /// <summary>获取适合在哈希算法和类似哈希表的数据结构中使用的 <see cref="T:System.Security.Permissions.FileIOPermission" /> 对象的哈希代码。</summary>
    /// <returns>当前 <see cref="T:System.Security.Permissions.FileIOPermission" /> 对象的哈希代码。</returns>
    [ComVisible(false)]
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    [SecuritySafeCritical]
    internal static void QuickDemand(FileIOPermissionAccess access, string fullPath, bool checkForDuplicates, bool needFullPath)
    {
      if (!CodeAccessSecurityEngine.QuickCheckForAllDemands())
      {
        new FileIOPermission(access, new string[1]{ fullPath }, (checkForDuplicates ? 1 : 0) != 0, (needFullPath ? 1 : 0) != 0).Demand();
      }
      else
      {
        Path.CheckInvalidPathChars(fullPath, true);
        if (fullPath.Length > 2 && fullPath.IndexOf(':', 2) != -1)
          throw new NotSupportedException(Environment.GetResourceString("Argument_PathFormatNotSupported"));
      }
    }
  }
}
