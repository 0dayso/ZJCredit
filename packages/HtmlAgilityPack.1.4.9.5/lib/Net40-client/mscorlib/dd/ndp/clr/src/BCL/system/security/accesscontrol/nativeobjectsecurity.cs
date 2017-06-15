// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.NativeObjectSecurity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace System.Security.AccessControl
{
  /// <summary>提供无需直接操作访问控制列表 (ACL) 而控制对本机对象的访问的能力。本机对象类型由 <see cref="T:System.Security.AccessControl.ResourceType" /> 枚举定义。</summary>
  public abstract class NativeObjectSecurity : CommonObjectSecurity
  {
    private readonly uint ProtectedDiscretionaryAcl = 2147483648;
    private readonly uint ProtectedSystemAcl = 1073741824;
    private readonly uint UnprotectedDiscretionaryAcl = 536870912;
    private readonly uint UnprotectedSystemAcl = 268435456;
    private readonly ResourceType _resourceType;
    private NativeObjectSecurity.ExceptionFromErrorCode _exceptionFromErrorCode;
    private object _exceptionContext;

    /// <summary>用指定的值初始化 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 类的新实例。</summary>
    /// <param name="isContainer">如果新的 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象是一个容器对象，则为 true。</param>
    /// <param name="resourceType">
    /// <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象与其相关联的可保护对象类型。</param>
    protected NativeObjectSecurity(bool isContainer, ResourceType resourceType)
      : base(isContainer)
    {
      this._resourceType = resourceType;
    }

    /// <summary>使用指定的值初始化 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 类的一个新实例。</summary>
    /// <param name="isContainer">如果新的 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象是一个容器对象，则为 true。</param>
    /// <param name="resourceType">
    /// <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象与其相关联的可保护对象类型。</param>
    /// <param name="exceptionFromErrorCode">由提供自定义异常的集成器实现的委托。</param>
    /// <param name="exceptionContext">包含有关异常的源或目标的上下文信息的对象。</param>
    protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
      : this(isContainer, resourceType)
    {
      this._exceptionContext = exceptionContext;
      this._exceptionFromErrorCode = exceptionFromErrorCode;
    }

    [SecurityCritical]
    internal NativeObjectSecurity(ResourceType resourceType, CommonSecurityDescriptor securityDescriptor)
      : this(resourceType, securityDescriptor, (NativeObjectSecurity.ExceptionFromErrorCode) null)
    {
    }

    [SecurityCritical]
    internal NativeObjectSecurity(ResourceType resourceType, CommonSecurityDescriptor securityDescriptor, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode)
      : base(securityDescriptor)
    {
      this._resourceType = resourceType;
      this._exceptionFromErrorCode = exceptionFromErrorCode;
    }

    /// <summary>用指定的值初始化 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 类的新实例。建议传递给构造函数和 Persist 方法的 <paramref name="includeSections" /> 参数的值应该相同。有关更多信息，请参见“备注”。</summary>
    /// <param name="isContainer">如果新的 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象是一个容器对象，则为 true。</param>
    /// <param name="resourceType">
    /// <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象与其相关联的可保护对象类型。</param>
    /// <param name="name">新的 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象与其相关联的可保护对象的名称。</param>
    /// <param name="includeSections">
    /// <see cref="T:System.Security.AccessControl.AccessControlSections" /> 枚举值之一，指定要包括在此 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />  对象中的可保护对象的安全说明符（访问规则、审核规则、所有者和主要组）的各个部分。</param>
    /// <param name="exceptionFromErrorCode">由提供自定义异常的集成器实现的委托。</param>
    /// <param name="exceptionContext">包含有关异常的源或目标的上下文信息的对象。</param>
    [SecuritySafeCritical]
    protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, string name, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
      : this(resourceType, NativeObjectSecurity.CreateInternal(resourceType, isContainer, name, (SafeHandle) null, includeSections, true, exceptionFromErrorCode, exceptionContext), exceptionFromErrorCode)
    {
    }

    /// <summary>用指定的值初始化 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 类的新实例。建议传递给构造函数和 Persist 方法的 <paramref name="includeSections" /> 参数的值应该相同。有关更多信息，请参见“备注”。</summary>
    /// <param name="isContainer">如果新 <see cref="T:System.Security.AccessControl.NativObjectSecurity" /> 对象是一个容器对象，则为 true。</param>
    /// <param name="resourceType">
    /// <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象与其相关联的可保护对象类型。</param>
    /// <param name="name">新的 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象与其相关联的可保护对象的名称。</param>
    /// <param name="includeSections">
    /// <see cref="T:System.Security.AccessControl.AccessControlSections" /> 枚举值之一，指定要包括在此 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />  对象中的可保护对象的安全说明符（访问规则、审核规则、所有者和主要组）的各个部分。</param>
    protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, string name, AccessControlSections includeSections)
      : this(isContainer, resourceType, name, includeSections, (NativeObjectSecurity.ExceptionFromErrorCode) null, (object) null)
    {
    }

    /// <summary>用指定的值初始化 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 类的新实例。建议传递给构造函数和 Persist 方法的 <paramref name="includeSections" /> 参数的值应该相同。有关更多信息，请参见“备注”。</summary>
    /// <param name="isContainer">如果新的 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象是一个容器对象，则为 true。</param>
    /// <param name="resourceType">
    /// <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象与其相关联的可保护对象类型。</param>
    /// <param name="handle">新的 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象与其相关联的可保护对象的句柄。</param>
    /// <param name="includeSections">
    /// <see cref="T:System.Security.AccessControl.AccessControlSections" /> 枚举值之一，指定要包括在此 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />  对象中的可保护对象的安全说明符（访问规则、审核规则、所有者和主要组）的各个部分。</param>
    /// <param name="exceptionFromErrorCode">由提供自定义异常的集成器实现的委托。</param>
    /// <param name="exceptionContext">包含有关异常的源或目标的上下文信息的对象。</param>
    [SecuritySafeCritical]
    protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, SafeHandle handle, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
      : this(resourceType, NativeObjectSecurity.CreateInternal(resourceType, isContainer, (string) null, handle, includeSections, false, exceptionFromErrorCode, exceptionContext), exceptionFromErrorCode)
    {
    }

    /// <summary>用指定的值初始化 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 类的新实例。建议传递给构造函数和 Persist 方法的 <paramref name="includeSections" /> 参数的值应该相同。有关更多信息，请参见“备注”。</summary>
    /// <param name="isContainer">如果新的 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象是一个容器对象，则为 true。</param>
    /// <param name="resourceType">
    /// <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象与其相关联的可保护对象类型。</param>
    /// <param name="handle">新的 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象与其相关联的可保护对象的句柄。</param>
    /// <param name="includeSections">
    /// <see cref="T:System.Security.AccessControl.AccessControlSections" /> 枚举值之一，指定要包括在此 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" />  对象中的可保护对象的安全说明符（访问规则、审核规则、所有者和主要组）的各个部分。</param>
    [SecuritySafeCritical]
    protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, SafeHandle handle, AccessControlSections includeSections)
      : this(isContainer, resourceType, handle, includeSections, (NativeObjectSecurity.ExceptionFromErrorCode) null, (object) null)
    {
    }

    [SecurityCritical]
    private static CommonSecurityDescriptor CreateInternal(ResourceType resourceType, bool isContainer, string name, SafeHandle handle, AccessControlSections includeSections, bool createByName, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
    {
      if (createByName && name == null)
        throw new ArgumentNullException("name");
      if (!createByName && handle == null)
        throw new ArgumentNullException("handle");
      RawSecurityDescriptor resultSd;
      int securityInfo = Win32.GetSecurityInfo(resourceType, name, handle, includeSections, out resultSd);
      if (securityInfo != 0)
      {
        Exception exception = (Exception) null;
        if (exceptionFromErrorCode != null)
          exception = exceptionFromErrorCode(securityInfo, name, handle, exceptionContext);
        if (exception == null)
        {
          if (securityInfo == 5)
            exception = (Exception) new UnauthorizedAccessException();
          else if (securityInfo == 1307)
            exception = (Exception) new InvalidOperationException(Environment.GetResourceString("AccessControl_InvalidOwner"));
          else if (securityInfo == 1308)
            exception = (Exception) new InvalidOperationException(Environment.GetResourceString("AccessControl_InvalidGroup"));
          else if (securityInfo == 87)
            exception = (Exception) new InvalidOperationException(Environment.GetResourceString("AccessControl_UnexpectedError", (object) securityInfo));
          else if (securityInfo == 123)
            exception = (Exception) new ArgumentException(Environment.GetResourceString("Argument_InvalidName"), "name");
          else if (securityInfo == 2)
            exception = name == null ? (Exception) new FileNotFoundException() : (Exception) new FileNotFoundException(name);
          else if (securityInfo == 1350)
            exception = (Exception) new NotSupportedException(Environment.GetResourceString("AccessControl_NoAssociatedSecurity"));
          else
            exception = (Exception) new InvalidOperationException(Environment.GetResourceString("AccessControl_UnexpectedError", (object) securityInfo));
        }
        throw exception;
      }
      return new CommonSecurityDescriptor(isContainer, false, resultSd, true);
    }

    [SecurityCritical]
    private void Persist(string name, SafeHandle handle, AccessControlSections includeSections, object exceptionContext)
    {
      this.WriteLock();
      try
      {
        SecurityInfos securityInformation = (SecurityInfos) 0;
        SecurityIdentifier owner = (SecurityIdentifier) null;
        SecurityIdentifier group = (SecurityIdentifier) null;
        SystemAcl systemAcl = (SystemAcl) null;
        DiscretionaryAcl discretionaryAcl = (DiscretionaryAcl) null;
        if ((includeSections & AccessControlSections.Owner) != AccessControlSections.None && this._securityDescriptor.Owner != (SecurityIdentifier) null)
        {
          securityInformation |= SecurityInfos.Owner;
          owner = this._securityDescriptor.Owner;
        }
        if ((includeSections & AccessControlSections.Group) != AccessControlSections.None && this._securityDescriptor.Group != (SecurityIdentifier) null)
        {
          securityInformation |= SecurityInfos.Group;
          group = this._securityDescriptor.Group;
        }
        if ((includeSections & AccessControlSections.Audit) != AccessControlSections.None)
        {
          SecurityInfos securityInfos = securityInformation | SecurityInfos.SystemAcl;
          systemAcl = !this._securityDescriptor.IsSystemAclPresent || this._securityDescriptor.SystemAcl == null || this._securityDescriptor.SystemAcl.Count <= 0 ? (SystemAcl) null : this._securityDescriptor.SystemAcl;
          securityInformation = (this._securityDescriptor.ControlFlags & ControlFlags.SystemAclProtected) == ControlFlags.None ? securityInfos | (SecurityInfos) this.UnprotectedSystemAcl : securityInfos | (SecurityInfos) this.ProtectedSystemAcl;
        }
        if ((includeSections & AccessControlSections.Access) != AccessControlSections.None && this._securityDescriptor.IsDiscretionaryAclPresent)
        {
          SecurityInfos securityInfos = securityInformation | SecurityInfos.DiscretionaryAcl;
          discretionaryAcl = !this._securityDescriptor.DiscretionaryAcl.EveryOneFullAccessForNullDacl ? this._securityDescriptor.DiscretionaryAcl : (DiscretionaryAcl) null;
          securityInformation = (this._securityDescriptor.ControlFlags & ControlFlags.DiscretionaryAclProtected) == ControlFlags.None ? securityInfos | (SecurityInfos) this.UnprotectedDiscretionaryAcl : securityInfos | (SecurityInfos) this.ProtectedDiscretionaryAcl;
        }
        if (securityInformation == (SecurityInfos) 0)
          return;
        int errorCode = Win32.SetSecurityInfo(this._resourceType, name, handle, securityInformation, owner, group, (GenericAcl) systemAcl, (GenericAcl) discretionaryAcl);
        if (errorCode != 0)
        {
          Exception exception = (Exception) null;
          if (this._exceptionFromErrorCode != null)
            exception = this._exceptionFromErrorCode(errorCode, name, handle, exceptionContext);
          if (exception == null)
          {
            if (errorCode == 5)
              exception = (Exception) new UnauthorizedAccessException();
            else if (errorCode == 1307)
              exception = (Exception) new InvalidOperationException(Environment.GetResourceString("AccessControl_InvalidOwner"));
            else if (errorCode == 1308)
              exception = (Exception) new InvalidOperationException(Environment.GetResourceString("AccessControl_InvalidGroup"));
            else if (errorCode == 123)
              exception = (Exception) new ArgumentException(Environment.GetResourceString("Argument_InvalidName"), "name");
            else if (errorCode == 6)
              exception = (Exception) new NotSupportedException(Environment.GetResourceString("AccessControl_InvalidHandle"));
            else if (errorCode == 2)
              exception = (Exception) new FileNotFoundException();
            else if (errorCode == 1350)
              exception = (Exception) new NotSupportedException(Environment.GetResourceString("AccessControl_NoAssociatedSecurity"));
            else
              exception = (Exception) new InvalidOperationException(Environment.GetResourceString("AccessControl_UnexpectedError", (object) errorCode));
          }
          throw exception;
        }
        this.OwnerModified = false;
        this.GroupModified = false;
        this.AccessRulesModified = false;
        this.AuditRulesModified = false;
      }
      finally
      {
        this.WriteUnlock();
      }
    }

    /// <summary>将与此 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象关联的安全说明符的指定部分保存到永久性存储。建议传递给构造函数和 Persist 方法的 <paramref name="includeSections" /> 参数的值应该相同。有关更多信息，请参见“备注”。</summary>
    /// <param name="name">此 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象与其相关联的可保护对象的名称。</param>
    /// <param name="includeSections">
    /// <see cref="T:System.Security.AccessControl.AccessControlSections" /> 枚举值之一，指定要保存的可保护对象的安全说明符（访问规则、审核规则、所有者和主要组）的各个部分。</param>
    /// <exception cref="T:System.IO.FileNotFoundException">此 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象与之关联的可保护对象是一个目录或一个文件，且该目录或文件未能找到。</exception>
    protected override sealed void Persist(string name, AccessControlSections includeSections)
    {
      this.Persist(name, includeSections, this._exceptionContext);
    }

    /// <summary>将与此 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象关联的安全说明符的指定部分保存到永久性存储。建议传递给构造函数和 Persist 方法的 <paramref name="includeSections" /> 参数的值应该相同。有关更多信息，请参见“备注”。</summary>
    /// <param name="name">此 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象与其相关联的可保护对象的名称。</param>
    /// <param name="includeSections">
    /// <see cref="T:System.Security.AccessControl.AccessControlSections" /> 枚举值之一，指定要保存的可保护对象的安全说明符（访问规则、审核规则、所有者和主要组）的各个部分。</param>
    /// <param name="exceptionContext">包含有关异常的源或目标的上下文信息的对象。</param>
    /// <exception cref="T:System.IO.FileNotFoundException">此 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象与之关联的可保护对象是一个目录或一个文件，且该目录或文件未能找到。</exception>
    [SecuritySafeCritical]
    protected void Persist(string name, AccessControlSections includeSections, object exceptionContext)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      this.Persist(name, (SafeHandle) null, includeSections, exceptionContext);
    }

    /// <summary>将与此 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象关联的安全说明符的指定部分保存到永久性存储。建议传递给构造函数和 Persist 方法的 <paramref name="includeSections" /> 参数的值应该相同。有关更多信息，请参见“备注”。</summary>
    /// <param name="handle">此 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象与其相关联的可保护对象的句柄。</param>
    /// <param name="includeSections">
    /// <see cref="T:System.Security.AccessControl.AccessControlSections" /> 枚举值之一，指定要保存的可保护对象的安全说明符（访问规则、审核规则、所有者和主要组）的各个部分。</param>
    /// <exception cref="T:System.IO.FileNotFoundException">此 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象与之关联的可保护对象是一个目录或一个文件，且该目录或文件未能找到。</exception>
    [SecuritySafeCritical]
    protected override sealed void Persist(SafeHandle handle, AccessControlSections includeSections)
    {
      this.Persist(handle, includeSections, this._exceptionContext);
    }

    /// <summary>将与此 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象关联的安全说明符的指定部分保存到永久性存储。建议传递给构造函数和 Persist 方法的 <paramref name="includeSections" /> 参数的值应该相同。有关更多信息，请参见“备注”。</summary>
    /// <param name="handle">此 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象与其相关联的可保护对象的句柄。</param>
    /// <param name="includeSections">
    /// <see cref="T:System.Security.AccessControl.AccessControlSections" /> 枚举值之一，指定要保存的可保护对象的安全说明符（访问规则、审核规则、所有者和主要组）的各个部分。</param>
    /// <param name="exceptionContext">包含有关异常的源或目标的上下文信息的对象。</param>
    /// <exception cref="T:System.IO.FileNotFoundException">此 <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象与之关联的可保护对象是一个目录或一个文件，且该目录或文件未能找到。</exception>
    [SecuritySafeCritical]
    protected void Persist(SafeHandle handle, AccessControlSections includeSections, object exceptionContext)
    {
      if (handle == null)
        throw new ArgumentNullException("handle");
      this.Persist((string) null, handle, includeSections, exceptionContext);
    }

    /// <summary>为集成器提供一种将数字错误代码映射到它们创建的特定异常的方式。</summary>
    /// <returns>此委托创建的 <see cref="T:System.Exception" />。</returns>
    /// <param name="errorCode">数字错误代码。</param>
    /// <param name="name">
    /// <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象所关联的可保护对象的名称。</param>
    /// <param name="handle">
    /// <see cref="T:System.Security.AccessControl.NativeObjectSecurity" /> 对象所关联的可保护对象的句柄。</param>
    /// <param name="context">包含有关异常的源或目标的上下文信息的对象。</param>
    [SecuritySafeCritical]
    protected internal delegate Exception ExceptionFromErrorCode(int errorCode, string name, SafeHandle handle, object context);
  }
}
