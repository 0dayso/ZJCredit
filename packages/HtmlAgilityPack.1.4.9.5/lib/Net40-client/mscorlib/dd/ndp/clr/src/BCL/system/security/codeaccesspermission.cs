// Decompiled with JetBrains decompiler
// Type: System.Security.CodeAccessPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;
using System.Threading;

namespace System.Security
{
  /// <summary>定义所有代码访问权限的基础结构。</summary>
  [ComVisible(true)]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, ControlEvidence = true, ControlPolicy = true)]
  public abstract class CodeAccessPermission : IPermission, ISecurityEncodable, IStackWalk
  {
    /// <summary>导致当前框架先前的所有 <see cref="M:System.Security.CodeAccessPermission.Assert" /> 都被移除，不再有效。</summary>
    /// <exception cref="T:System.InvalidOperationException">当前框架没有前一个 <see cref="M:System.Security.CodeAccessPermission.Assert" />。</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void RevertAssert()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      SecurityRuntime.RevertAssert(ref stackMark);
    }

    /// <summary>导致当前框架先前的所有 <see cref="M:System.Security.CodeAccessPermission.Deny" /> 都被移除，不再有效。</summary>
    /// <exception cref="T:System.InvalidOperationException">当前框架没有前一个 <see cref="M:System.Security.CodeAccessPermission.Deny" />。</exception>
    [SecuritySafeCritical]
    [Obsolete("Deny is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void RevertDeny()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      SecurityRuntime.RevertDeny(ref stackMark);
    }

    /// <summary>导致当前框架先前的所有 <see cref="M:System.Security.CodeAccessPermission.PermitOnly" /> 都被移除，不再有效。</summary>
    /// <exception cref="T:System.InvalidOperationException">当前框架没有前一个 <see cref="M:System.Security.CodeAccessPermission.PermitOnly" />。</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void RevertPermitOnly()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      SecurityRuntime.RevertPermitOnly(ref stackMark);
    }

    /// <summary>使当前框架所有以前的重写都被移除并不再有效。</summary>
    /// <exception cref="T:System.InvalidOperationException">当前框架没有上一个 <see cref="M:System.Security.CodeAccessPermission.Assert" />、<see cref="M:System.Security.CodeAccessPermission.Deny" /> 和 <see cref="M:System.Security.CodeAccessPermission.PermitOnly" />。</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void RevertAll()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      SecurityRuntime.RevertAll(ref stackMark);
    }

    /// <summary>如果未给调用堆栈中处于较高位置的所有调用方授予当前实例所指定的权限，则在运行时强制 <see cref="T:System.Security.SecurityException" />。</summary>
    /// <exception cref="T:System.Security.SecurityException">调用堆栈中处于较高位置的调用方不具有当前实例所指定的权限。- 或 -调用堆栈中处于较高位置的调用方已经在当前权限对象上调用了 <see cref="M:System.Security.CodeAccessPermission.Deny" />。</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Demand()
    {
      if (this.CheckDemand((CodeAccessPermission) null))
        return;
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCallersCaller;
      CodeAccessSecurityEngine.Check(this, ref stackMark);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void Demand(PermissionType permissionType)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCallersCaller;
      CodeAccessSecurityEngine.SpecialDemand(permissionType, ref stackMark);
    }

    /// <summary>声明调用代码能够通过调用此方法的代码，访问受权限请求保护的资源，即使堆栈上部的调用方未被授予访问该资源的权限。使用 <see cref="M:System.Security.CodeAccessPermission.Assert" /> 会引起安全问题。</summary>
    /// <exception cref="T:System.Security.SecurityException">调用代码没有 <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Assertion" />。- 或 -当前框架已经有一个活动的 <see cref="M:System.Security.CodeAccessPermission.Assert" />。</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Assert()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      CodeAccessSecurityEngine.Assert(this, ref stackMark);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void Assert(bool allPossible)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      SecurityRuntime.AssertAllPossible(ref stackMark);
    }

    /// <summary>防止调用堆栈中处于较高位置的调用方通过调用此方法的代码来访问由当前实例指定的资源。</summary>
    /// <exception cref="T:System.Security.SecurityException">当前框架已经有一个活动的 <see cref="M:System.Security.CodeAccessPermission.Deny" />。</exception>
    [SecuritySafeCritical]
    [Obsolete("Deny is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Deny()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      CodeAccessSecurityEngine.Deny(this, ref stackMark);
    }

    /// <summary>防止调用堆栈中处于较高位置的调用方通过调用此方法的代码来访问除当前实例指定的资源外的所有资源。</summary>
    /// <exception cref="T:System.Security.SecurityException">当前框架已经有一个活动的 <see cref="M:System.Security.CodeAccessPermission.PermitOnly" />。</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void PermitOnly()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      CodeAccessSecurityEngine.PermitOnly(this, ref stackMark);
    }

    /// <summary>在派生类中重写时，创建作为当前权限和指定权限的并集的权限。</summary>
    /// <returns>一个新权限，它表示当前权限与指定权限的并集。</returns>
    /// <param name="other">将与当前权限合并的权限。它必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="other" /> 参数不为 null。被传递 null 时，此方法仅在该级别上受支持。</exception>
    public virtual IPermission Union(IPermission other)
    {
      if (other == null)
        return this.Copy();
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SecurityPermissionUnion"));
    }

    internal static SecurityElement CreatePermissionElement(IPermission perm, string permname)
    {
      SecurityElement element = new SecurityElement("IPermission");
      Type type = perm.GetType();
      string typename = permname;
      XMLUtil.AddClassAttribute(element, type, typename);
      string name = "version";
      string str = "1";
      element.AddAttribute(name, str);
      return element;
    }

    internal static void ValidateElement(SecurityElement elem, IPermission perm)
    {
      if (elem == null)
        throw new ArgumentNullException("elem");
      if (!XMLUtil.IsPermissionElement(perm, elem))
        throw new ArgumentException(Environment.GetResourceString("Argument_NotAPermissionElement"));
      string str = elem.Attribute("version");
      if (str != null && !str.Equals("1"))
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXMLBadVersion"));
    }

    /// <summary>在派生类中重写时，创建安全对象及其当前状态的 XML 编码。</summary>
    /// <returns>安全对象的 XML 编码，包括任何状态信息。</returns>
    public abstract SecurityElement ToXml();

    /// <summary>在派生类中重写时，用 XML 编码重新构造具有指定状态的安全对象。</summary>
    /// <param name="elem">用于重新构造安全对象的 XML 编码。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="elem" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="elem" /> 参数不包含与当前实例类型相同的实例的 XML 编码。- 或 -不支持 <paramref name="elem" /> 参数的版本号。</exception>
    public abstract void FromXml(SecurityElement elem);

    /// <summary>创建并返回当前权限对象的字符串表示形式。</summary>
    /// <returns>当前权限对象的字符串表示形式。</returns>
    public override string ToString()
    {
      return this.ToXml().ToString();
    }

    internal bool VerifyType(IPermission perm)
    {
      return perm != null && !(perm.GetType() != this.GetType());
    }

    /// <summary>通过派生类实现时，创建和返回当前权限对象的相同副本。</summary>
    /// <returns>当前权限对象的副本。</returns>
    public abstract IPermission Copy();

    /// <summary>通过派生类实现时，创建和返回是当前权限和指定权限的交集的权限。</summary>
    /// <returns>一个新权限，它表示当前权限与指定权限的交集。如果交集为空，则此新权限为 null。</returns>
    /// <param name="target">要与当前权限相交的权限。它必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不为 null，并且不是与当前权限属于相同类的实例。</exception>
    public abstract IPermission Intersect(IPermission target);

    /// <summary>通过派生类实现时，确定当前权限是否为指定权限的子集。</summary>
    /// <returns>如果当前权限是指定权限的子集，则为 true；否则为 false。</returns>
    /// <param name="target">将要测试子集关系的权限。此权限必须与当前权限属于同一类型。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="target" /> 参数不是 null，而且与当前权限不是同一类型。</exception>
    public abstract bool IsSubsetOf(IPermission target);

    /// <summary>确定指定的 <see cref="T:System.Security.CodeAccessPermission" /> 对象是否等于当前的 <see cref="T:System.Security.CodeAccessPermission" />。</summary>
    /// <returns>如果指定的 <see cref="T:System.Security.CodeAccessPermission" /> 对象等于当前的 <see cref="T:System.Security.CodeAccessPermission" />，则为 true；否则为 false。</returns>
    /// <param name="obj">要与当前的 <see cref="T:System.Security.CodeAccessPermission" /> 进行比较的 <see cref="T:System.Security.CodeAccessPermission" /> 对象。</param>
    [ComVisible(false)]
    public override bool Equals(object obj)
    {
      IPermission target = obj as IPermission;
      if (obj != null && target == null)
        return false;
      try
      {
        if (!this.IsSubsetOf(target))
          return false;
        if (target != null)
        {
          if (!target.IsSubsetOf((IPermission) this))
            return false;
        }
      }
      catch (ArgumentException ex)
      {
        return false;
      }
      return true;
    }

    /// <summary>获取适合在哈希算法和类似哈希表的数据结构中使用的 <see cref="T:System.Security.CodeAccessPermission" /> 对象的哈希代码。</summary>
    /// <returns>当前 <see cref="T:System.Security.CodeAccessPermission" /> 对象的哈希代码。</returns>
    [ComVisible(false)]
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    internal bool CheckDemand(CodeAccessPermission grant)
    {
      return this.IsSubsetOf((IPermission) grant);
    }

    internal bool CheckPermitOnly(CodeAccessPermission permitted)
    {
      return this.IsSubsetOf((IPermission) permitted);
    }

    internal bool CheckDeny(CodeAccessPermission denied)
    {
      IPermission permission = this.Intersect((IPermission) denied);
      if (permission != null)
        return permission.IsSubsetOf((IPermission) null);
      return true;
    }

    internal bool CheckAssert(CodeAccessPermission asserted)
    {
      return this.IsSubsetOf((IPermission) asserted);
    }
  }
}
