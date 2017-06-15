// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.FileIOPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.AccessControl;

namespace System.Security.Permissions
{
  /// <summary>允许对使用声明安全性应用到代码中的 <see cref="T:System.Security.Permissions.FileIOPermission" /> 进行安全操作。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class FileIOPermissionAttribute : CodeAccessSecurityAttribute
  {
    private string m_read;
    private string m_write;
    private string m_append;
    private string m_pathDiscovery;
    private string m_viewAccess;
    private string m_changeAccess;
    [OptionalField(VersionAdded = 2)]
    private FileIOPermissionAccess m_allLocalFiles;
    [OptionalField(VersionAdded = 2)]
    private FileIOPermissionAccess m_allFiles;

    /// <summary>获取或设置对字符串值所指定的文件或目录的读访问权限。</summary>
    /// <returns>用于读访问的文件或目录的绝对路径。</returns>
    public string Read
    {
      get
      {
        return this.m_read;
      }
      set
      {
        this.m_read = value;
      }
    }

    /// <summary>获取或设置对字符串值所指定的文件或目录的写访问权限。</summary>
    /// <returns>用于写访问的文件或目录的绝对路径。</returns>
    public string Write
    {
      get
      {
        return this.m_write;
      }
      set
      {
        this.m_write = value;
      }
    }

    /// <summary>获取或设置对字符串值所指定的文件或目录的追加访问权限。</summary>
    /// <returns>用于追加访问的文件或目录的绝对路径。</returns>
    public string Append
    {
      get
      {
        return this.m_append;
      }
      set
      {
        this.m_append = value;
      }
    }

    /// <summary>获取或设置针对其授予路径发现权限的文件或目录。</summary>
    /// <returns>文件或目录的绝对路径。</returns>
    public string PathDiscovery
    {
      get
      {
        return this.m_pathDiscovery;
      }
      set
      {
        this.m_pathDiscovery = value;
      }
    }

    /// <summary>获取或设置可在其中查看访问控制信息的文件或目录。</summary>
    /// <returns>可在其中查看访问控制信息的文件或目录的绝对路径。</returns>
    public string ViewAccessControl
    {
      get
      {
        return this.m_viewAccess;
      }
      set
      {
        this.m_viewAccess = value;
      }
    }

    /// <summary>获取或设置可在其中更改访问控制信息的文件或目录。</summary>
    /// <returns>可在其中更改访问控制信息的文件或目录的绝对路径。</returns>
    public string ChangeAccessControl
    {
      get
      {
        return this.m_changeAccess;
      }
      set
      {
        this.m_changeAccess = value;
      }
    }

    /// <summary>获取或设置对字符串值所指定的文件或目录的完全访问权限。</summary>
    /// <returns>用于完全访问的文件或目录的绝对路径。</returns>
    /// <exception cref="T:System.NotSupportedException">此属性不支持 get 方法。</exception>
    [Obsolete("Please use the ViewAndModify property instead.")]
    public string All
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_GetMethod"));
      }
      set
      {
        this.m_read = value;
        this.m_write = value;
        this.m_append = value;
        this.m_pathDiscovery = value;
      }
    }

    /// <summary>获取或设置可在其中查看并修改文件数据的文件或目录。</summary>
    /// <returns>可在其中查看并修改文件数据的文件或目录的绝对路径。</returns>
    /// <exception cref="T:System.NotSupportedException">get 访问器被调用。提供访问器是为了与 C# 编译器兼容。</exception>
    public string ViewAndModify
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_GetMethod"));
      }
      set
      {
        this.m_read = value;
        this.m_write = value;
        this.m_append = value;
        this.m_pathDiscovery = value;
      }
    }

    /// <summary>获取或设置对所有文件的允许访问权限。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> 值的按位组合，表示所有文件的权限。默认值为 <see cref="F:System.Security.Permissions.FileIOPermissionAccess.NoAccess" />。</returns>
    public FileIOPermissionAccess AllFiles
    {
      get
      {
        return this.m_allFiles;
      }
      set
      {
        this.m_allFiles = value;
      }
    }

    /// <summary>获取或设置所有本地文件的允许访问权限。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> 值的按位组合，它表示所有本地文件的权限。默认值为 <see cref="F:System.Security.Permissions.FileIOPermissionAccess.NoAccess" />。</returns>
    public FileIOPermissionAccess AllLocalFiles
    {
      get
      {
        return this.m_allLocalFiles;
      }
      set
      {
        this.m_allLocalFiles = value;
      }
    }

    /// <summary>用指定的 <see cref="T:System.Security.Permissions.SecurityAction" /> 初始化 <see cref="T:System.Security.Permissions.FileIOPermissionAttribute" /> 类的新实例。</summary>
    /// <param name="action">
    /// <see cref="T:System.Security.Permissions.SecurityAction" /> 值之一。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="action" /> 参数不是有效的 <see cref="T:System.Security.Permissions.SecurityAction" />。</exception>
    public FileIOPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>创建并返回一个新的 <see cref="T:System.Security.Permissions.FileIOPermission" />。</summary>
    /// <returns>与此特性对应的 <see cref="T:System.Security.Permissions.FileIOPermission" />。</returns>
    /// <exception cref="T:System.ArgumentException">要保护访问安全的文件或目录的路径信息包含无效的字符或通配说明符。</exception>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new FileIOPermission(PermissionState.Unrestricted);
      FileIOPermission fileIoPermission = new FileIOPermission(PermissionState.None);
      if (this.m_read != null)
        fileIoPermission.SetPathList(FileIOPermissionAccess.Read, this.m_read);
      if (this.m_write != null)
        fileIoPermission.SetPathList(FileIOPermissionAccess.Write, this.m_write);
      if (this.m_append != null)
        fileIoPermission.SetPathList(FileIOPermissionAccess.Append, this.m_append);
      if (this.m_pathDiscovery != null)
        fileIoPermission.SetPathList(FileIOPermissionAccess.PathDiscovery, this.m_pathDiscovery);
      if (this.m_viewAccess != null)
        fileIoPermission.SetPathList(FileIOPermissionAccess.NoAccess, AccessControlActions.View, new string[1]
        {
          this.m_viewAccess
        }, 0 != 0);
      if (this.m_changeAccess != null)
        fileIoPermission.SetPathList(FileIOPermissionAccess.NoAccess, AccessControlActions.Change, new string[1]
        {
          this.m_changeAccess
        }, 0 != 0);
      fileIoPermission.AllFiles = this.m_allFiles;
      fileIoPermission.AllLocalFiles = this.m_allLocalFiles;
      return (IPermission) fileIoPermission;
    }
  }
}
