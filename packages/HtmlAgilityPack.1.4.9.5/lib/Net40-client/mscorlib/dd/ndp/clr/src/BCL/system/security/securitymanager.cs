// Decompiled with JetBrains decompiler
// Type: System.Security.SecurityManager
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Util;
using System.Threading;

namespace System.Security
{
  /// <summary>对与安全系统交互的类提供主访问点。此类不能被继承。</summary>
  [ComVisible(true)]
  public static class SecurityManager
  {
    private static volatile SecurityPermission executionSecurityPermission = (SecurityPermission) null;
    private static PolicyManager polmgr = new PolicyManager();
    private static int[][] s_BuiltInPermissionIndexMap = new int[6][]{ new int[2]{ 0, 10 }, new int[2]{ 1, 11 }, new int[2]{ 2, 12 }, new int[2]{ 4, 13 }, new int[2]{ 6, 14 }, new int[2]{ 7, 9 } };
    private static CodeAccessPermission[] s_UnrestrictedSpecialPermissionMap = new CodeAccessPermission[6]{ (CodeAccessPermission) new EnvironmentPermission(PermissionState.Unrestricted), (CodeAccessPermission) new FileDialogPermission(PermissionState.Unrestricted), (CodeAccessPermission) new FileIOPermission(PermissionState.Unrestricted), (CodeAccessPermission) new ReflectionPermission(PermissionState.Unrestricted), (CodeAccessPermission) new SecurityPermission(PermissionState.Unrestricted), (CodeAccessPermission) new UIPermission(PermissionState.Unrestricted) };

    internal static PolicyManager PolicyManager
    {
      get
      {
        return SecurityManager.polmgr;
      }
    }

    /// <summary>获取或设置一个值，该值指示代码是否必须具有 <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Execution" /> 才能执行。</summary>
    /// <returns>如果代码必须具有 <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Execution" /> 才能执行，则为 true；否则为 false。</returns>
    /// <exception cref="T:System.Security.SecurityException">调用此方法的代码没有 <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPolicy" />
    /// </PermissionSet>
    [Obsolete("Because execution permission checks can no longer be turned off, the CheckExecutionRights property no longer has any effect.")]
    public static bool CheckExecutionRights
    {
      get
      {
        return true;
      }
      set
      {
      }
    }

    /// <summary>获取或设置指示是否启用安全的值。</summary>
    /// <returns>如果启用了安全，则为 true；否则为 false。</returns>
    /// <exception cref="T:System.Security.SecurityException">调用此方法的代码没有 <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />。</exception>
    [Obsolete("Because security can no longer be turned off, the SecurityEnabled property no longer has any effect.")]
    public static bool SecurityEnabled
    {
      get
      {
        return true;
      }
      set
      {
      }
    }

    /// <summary>确定是否将权限授予调用方。</summary>
    /// <returns>如果授予调用方的权限包括权限 <paramref name="perm" />，则为 true；否则为 false。</returns>
    /// <param name="perm">针对调用方的权限授予测试的权限。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("IsGranted is obsolete and will be removed in a future release of the .NET Framework.  Please use the PermissionSet property of either AppDomain or Assembly instead.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static bool IsGranted(IPermission perm)
    {
      if (perm == null)
        return true;
      PermissionSet o1 = (PermissionSet) null;
      PermissionSet o2 = (PermissionSet) null;
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      SecurityManager.GetGrantedPermissions(JitHelpers.GetObjectHandleOnStack<PermissionSet>(ref o1), JitHelpers.GetObjectHandleOnStack<PermissionSet>(ref o2), JitHelpers.GetStackCrawlMarkHandle(ref stackMark));
      if (!o1.Contains(perm))
        return false;
      if (o2 != null)
        return !o2.Contains(perm);
      return true;
    }

    /// <summary>获取一个权限集，对具有提供的证据的应用程序授予此权限集是安全的。</summary>
    /// <returns>一个权限集，它可用作具有提供的证据的应用程序的权限集。</returns>
    /// <param name="evidence">要与某个权限集匹配的主机证据。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="evidence" /> 为 null。</exception>
    public static PermissionSet GetStandardSandbox(Evidence evidence)
    {
      if (evidence == null)
        throw new ArgumentNullException("evidence");
      Zone hostEvidence = evidence.GetHostEvidence<Zone>();
      if (hostEvidence == null)
        return new PermissionSet(PermissionState.None);
      if (hostEvidence.SecurityZone == SecurityZone.MyComputer)
        return new PermissionSet(PermissionState.Unrestricted);
      if (hostEvidence.SecurityZone == SecurityZone.Intranet)
      {
        PermissionSet permissionSet = (PermissionSet) BuiltInPermissionSets.LocalIntranet;
        PolicyStatement policyStatement1 = new NetCodeGroup((IMembershipCondition) new AllMembershipCondition()).Resolve(evidence);
        PolicyStatement policyStatement2 = new FileCodeGroup((IMembershipCondition) new AllMembershipCondition(), FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery).Resolve(evidence);
        if (policyStatement1 != null)
          permissionSet.InplaceUnion(policyStatement1.PermissionSet);
        if (policyStatement2 != null)
          permissionSet.InplaceUnion(policyStatement2.PermissionSet);
        return permissionSet;
      }
      if (hostEvidence.SecurityZone != SecurityZone.Internet && hostEvidence.SecurityZone != SecurityZone.Trusted)
        return new PermissionSet(PermissionState.None);
      PermissionSet permissionSet1 = (PermissionSet) BuiltInPermissionSets.Internet;
      PolicyStatement policyStatement = new NetCodeGroup((IMembershipCondition) new AllMembershipCondition()).Resolve(evidence);
      if (policyStatement != null)
        permissionSet1.InplaceUnion(policyStatement.PermissionSet);
      return permissionSet1;
    }

    /// <summary>获取当前程序集的已授予的区域标识和 URL 标识权限集。</summary>
    /// <param name="zone">一个输出参数，它包含已授予的 <see cref="P:System.Security.Permissions.ZoneIdentityPermissionAttribute.Zone" /> 对象的 <see cref="T:System.Collections.ArrayList" />。</param>
    /// <param name="origin">一个输出参数，它包含已授予的 <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> 对象的 <see cref="T:System.Collections.ArrayList" />。</param>
    /// <exception cref="T:System.Security.SecurityException">对 <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> 的请求失败。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="00000000000000000400000000000000" Name="System.Windows.Forms" />
    /// </PermissionSet>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void GetZoneAndOrigin(out ArrayList zone, out ArrayList origin)
    {
      StackCrawlMark mark = StackCrawlMark.LookForMyCaller;
      CodeAccessSecurityEngine.GetZoneAndOrigin(ref mark, out zone, out origin);
    }

    /// <summary>从指定的文件加载 <see cref="T:System.Security.Policy.PolicyLevel" />。</summary>
    /// <returns>已加载的策略级别。</returns>
    /// <param name="path">包含安全策略信息文件的物理文件路径。</param>
    /// <param name="type">用于指定要加载的策略级别类型的枚举值之一。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 参数所指示的文件不存在。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用此方法的代码没有 <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />。- 或 -调用此方法的代码没有 <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Read" />。- 或 -调用此方法的代码没有 <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Write" />。- 或 -调用此方法的代码没有 <see cref="F:System.Security.Permissions.FileIOPermissionAccess.PathDiscovery" />。</exception>
    /// <exception cref="T:System.NotSupportedException">此方法使用代码访问安全性 (CAS) 策略，而该策略在 .NET Framework 4 中已过时。若要使 CAS 策略兼容于早期版本的 .NET Framework，请使用 &lt;legacyCasPolicy&gt; 元素。&lt;NetFx40_LegacySecurityPolicy&gt; 元素</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
    public static PolicyLevel LoadPolicyLevelFromFile(string path, PolicyLevelType type)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
      if (!File.InternalExists(path))
        throw new ArgumentException(Environment.GetResourceString("Argument_PolicyFileDoesNotExist"));
      string fullPath = Path.GetFullPath(path);
      FileIOPermission fileIoPermission = new FileIOPermission(PermissionState.None);
      int num1 = 1;
      string path1 = fullPath;
      fileIoPermission.AddPathList((FileIOPermissionAccess) num1, path1);
      int num2 = 2;
      string path2 = fullPath;
      fileIoPermission.AddPathList((FileIOPermissionAccess) num2, path2);
      fileIoPermission.Demand();
      using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
      {
        using (StreamReader streamReader = new StreamReader((Stream) fileStream))
          return SecurityManager.LoadPolicyLevelFromStringHelper(streamReader.ReadToEnd(), path, type);
      }
    }

    /// <summary>从指定的字符串加载 <see cref="T:System.Security.Policy.PolicyLevel" />。</summary>
    /// <returns>已加载的策略级别。</returns>
    /// <param name="str">安全策略级别的 XML 表示形式，与它在配置文件出现的形式相同。</param>
    /// <param name="type">用于指定要加载的策略级别类型的枚举值之一。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="str" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="str" /> 参数无效。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用此方法的代码没有 <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPolicy" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
    public static PolicyLevel LoadPolicyLevelFromString(string str, PolicyLevelType type)
    {
      return SecurityManager.LoadPolicyLevelFromStringHelper(str, (string) null, type);
    }

    private static PolicyLevel LoadPolicyLevelFromStringHelper(string str, string path, PolicyLevelType type)
    {
      if (str == null)
        throw new ArgumentNullException("str");
      PolicyLevel policyLevel = new PolicyLevel(type, path);
      SecurityElement topElement = new Parser(str).GetTopElement();
      if (topElement == null)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Policy_BadXml"), (object) "configuration"));
      string tag1 = "mscorlib";
      SecurityElement securityElement1 = topElement.SearchForChildByTag(tag1);
      if (securityElement1 == null)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Policy_BadXml"), (object) "mscorlib"));
      string tag2 = "security";
      SecurityElement securityElement2 = securityElement1.SearchForChildByTag(tag2);
      if (securityElement2 == null)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Policy_BadXml"), (object) "security"));
      string tag3 = "policy";
      SecurityElement securityElement3 = securityElement2.SearchForChildByTag(tag3);
      if (securityElement3 == null)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Policy_BadXml"), (object) "policy"));
      string tag4 = "PolicyLevel";
      SecurityElement e = securityElement3.SearchForChildByTag(tag4);
      if (e == null)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Policy_BadXml"), (object) "PolicyLevel"));
      policyLevel.FromXml(e);
      return policyLevel;
    }

    /// <summary>保存 <see cref="M:System.Security.SecurityManager.LoadPolicyLevelFromFile(System.String,System.Security.PolicyLevelType)" /> 所加载的修改的安全策略级别。</summary>
    /// <param name="level">要保存的策略级别对象。</param>
    /// <exception cref="T:System.Security.SecurityException">调用此方法的代码没有 <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />。</exception>
    /// <exception cref="T:System.NotSupportedException">此方法使用代码访问安全性 (CAS) 策略，而该策略在 .NET Framework 4 中已过时。若要使 CAS 策略兼容于早期版本的 .NET Framework，请使用 &lt;legacyCasPolicy&gt; 元素。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
    public static void SavePolicyLevel(PolicyLevel level)
    {
      if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
      PolicyManager.EncodeLevel(level);
    }

    /// <summary>基于指定的证据和请求确定授予代码哪些权限。</summary>
    /// <returns>将由安全系统授予的权限集。</returns>
    /// <param name="evidence">用于评估策略的证据集。</param>
    /// <param name="reqdPset">代码运行所需的权限。</param>
    /// <param name="optPset">如果授予了权限，将使用可选权限，但它们不是运行代码所必需的。</param>
    /// <param name="denyPset">即使策略允许授予，也不得授予代码的拒绝权限。</param>
    /// <param name="denied">包含未授予权限集的输出参数。</param>
    /// <exception cref="T:System.NotSupportedException">此方法使用代码访问安全性 (CAS) 策略，而该策略在 .NET Framework 4 中已过时。若要使 CAS 策略兼容于早期版本的 .NET Framework，请使用 &lt;legacyCasPolicy&gt; 元素。&lt;NetFx40_LegacySecurityPolicy&gt; 元素</exception>
    /// <exception cref="T:System.Security.Policy.PolicyException">策略未能授予 <paramref name="reqdPset" /> 参数所指定的最小需求权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public static PermissionSet ResolvePolicy(Evidence evidence, PermissionSet reqdPset, PermissionSet optPset, PermissionSet denyPset, out PermissionSet denied)
    {
      if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
      return SecurityManager.ResolvePolicy(evidence, reqdPset, optPset, denyPset, out denied, true);
    }

    /// <summary>基于指定的证据确定授予代码哪些权限。</summary>
    /// <returns>可由安全系统授予的权限集。</returns>
    /// <param name="evidence">用于评估策略的证据集。</param>
    /// <exception cref="T:System.NotSupportedException">此方法使用代码访问安全性 (CAS) 策略，而该策略在 .NET Framework 4 中已过时。若要使 CAS 策略兼容于早期版本的 .NET Framework，请使用 &lt;legacyCasPolicy&gt; 元素。&lt;NetFx40_LegacySecurityPolicy&gt; 元素</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public static PermissionSet ResolvePolicy(Evidence evidence)
    {
      if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
      if (evidence == null)
        evidence = new Evidence();
      return SecurityManager.polmgr.Resolve(evidence);
    }

    /// <summary>基于指定的证据确定授予代码哪些权限。</summary>
    /// <returns>对所有提供的证据都适合的权限集。</returns>
    /// <param name="evidences">用于评估策略的证据对象的数组。</param>
    /// <exception cref="T:System.NotSupportedException">此方法使用代码访问安全性 (CAS) 策略，而该策略在 .NET Framework 4 中已过时。若要使 CAS 策略兼容于早期版本的 .NET Framework，请使用 &lt;legacyCasPolicy&gt; 元素。&lt;NetFx40_LegacySecurityPolicy&gt; 元素</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public static PermissionSet ResolvePolicy(Evidence[] evidences)
    {
      if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
      if (evidences == null || evidences.Length == 0)
        evidences = new Evidence[1];
      PermissionSet permissionSet = SecurityManager.ResolvePolicy(evidences[0]);
      if (permissionSet == null)
        return (PermissionSet) null;
      for (int index = 1; index < evidences.Length; ++index)
      {
        permissionSet = permissionSet.Intersect(SecurityManager.ResolvePolicy(evidences[index]));
        if (permissionSet == null || permissionSet.IsEmpty())
          return permissionSet;
      }
      return permissionSet;
    }

    /// <summary>确定在必须在稍后的某个时间点重新创建当前线程的安全状态的情况下，当前线程是否需要安全上下文捕获。</summary>
    /// <returns>如果堆栈不包含部分信任的应用程序域、部分信任的程序集以及当前活动的 <see cref="M:System.Security.CodeAccessPermission.PermitOnly" /> 或 <see cref="M:System.Security.CodeAccessPermission.Deny" /> 修饰符，则为 false；如果公共语言运行时无法保证堆栈不包含其中任一项，则为 true。</returns>
    [SecurityCritical]
    public static bool CurrentThreadRequiresSecurityContextCapture()
    {
      return !CodeAccessSecurityEngine.QuickCheckForAllDemands();
    }

    /// <summary>确定基于指定的证据授予代码哪些权限，不包括 <see cref="T:System.AppDomain" /> 级别的策略。</summary>
    /// <returns>可由安全系统授予的权限集。</returns>
    /// <param name="evidence">用于评估策略的证据集。</param>
    /// <exception cref="T:System.NotSupportedException">此方法使用代码访问安全性 (CAS) 策略，而该策略在 .NET Framework 4 中已过时。若要使 CAS 策略兼容于早期版本的 .NET Framework，请使用 &lt;legacyCasPolicy&gt; 元素。&lt;NetFx40_LegacySecurityPolicy&gt; 元素</exception>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public static PermissionSet ResolveSystemPolicy(Evidence evidence)
    {
      if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
      if (PolicyManager.IsGacAssembly(evidence))
        return new PermissionSet(PermissionState.Unrestricted);
      return SecurityManager.polmgr.CodeGroupResolve(evidence, true);
    }

    /// <summary>获取与指定证据相匹配的代码组的集合。</summary>
    /// <returns>与证据相匹配的代码组集合的枚举。</returns>
    /// <param name="evidence">评估策略的证据集。</param>
    /// <exception cref="T:System.NotSupportedException">此方法使用代码访问安全性 (CAS) 策略，而该策略在 .NET Framework 4 中已过时。若要使 CAS 策略兼容于早期版本的 .NET Framework，请使用 &lt;legacyCasPolicy&gt; 元素。&lt;NetFx40_LegacySecurityPolicy&gt; 元素</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public static IEnumerator ResolvePolicyGroups(Evidence evidence)
    {
      if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
      return SecurityManager.polmgr.ResolveCodeGroups(evidence);
    }

    /// <summary>提供枚举器以按级别（如计算机策略和用户策略）访问安全策略层次结构。</summary>
    /// <returns>构成安全策略层次结构的 <see cref="T:System.Security.Policy.PolicyLevel" /> 对象的枚举器。</returns>
    /// <exception cref="T:System.NotSupportedException">此方法使用代码访问安全性 (CAS) 策略，而该策略在 .NET Framework 4 中已过时。若要使 CAS 策略兼容于早期版本的 .NET Framework，请使用 &lt;legacyCasPolicy&gt; 元素。&lt;NetFx40_LegacySecurityPolicy&gt; 元素</exception>
    /// <exception cref="T:System.Security.SecurityException">调用此方法的代码没有 <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPolicy" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public static IEnumerator PolicyHierarchy()
    {
      if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
      return SecurityManager.polmgr.PolicyHierarchy();
    }

    /// <summary>保存修改过的安全策略状态。</summary>
    /// <exception cref="T:System.NotSupportedException">此方法使用代码访问安全性 (CAS) 策略，而该策略在 .NET Framework 4 中已过时。若要使 CAS 策略兼容于早期版本的 .NET Framework，请使用 &lt;legacyCasPolicy&gt; 元素。&lt;NetFx40_LegacySecurityPolicy&gt; 元素</exception>
    /// <exception cref="T:System.Security.SecurityException">调用此方法的代码没有 <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
    public static void SavePolicy()
    {
      if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
      SecurityManager.polmgr.Save();
    }

    [SecurityCritical]
    private static PermissionSet ResolveCasPolicy(Evidence evidence, PermissionSet reqdPset, PermissionSet optPset, PermissionSet denyPset, out PermissionSet denied, out int securitySpecialFlags, bool checkExecutionPermission)
    {
      CodeAccessPermission.Assert(true);
      PermissionSet grantSet = SecurityManager.ResolvePolicy(evidence, reqdPset, optPset, denyPset, out denied, checkExecutionPermission);
      securitySpecialFlags = SecurityManager.GetSpecialFlags(grantSet, denied);
      return grantSet;
    }

    [SecurityCritical]
    private static PermissionSet ResolvePolicy(Evidence evidence, PermissionSet reqdPset, PermissionSet optPset, PermissionSet denyPset, out PermissionSet denied, bool checkExecutionPermission)
    {
      if (SecurityManager.executionSecurityPermission == null)
        SecurityManager.executionSecurityPermission = new SecurityPermission(SecurityPermissionFlag.Execution);
      Exception exception = (Exception) null;
      PermissionSet other1 = optPset;
      PermissionSet other2 = reqdPset != null ? (other1 == null ? (PermissionSet) null : reqdPset.Union(other1)) : other1;
      if (other2 != null && !other2.IsUnrestricted())
        other2.AddPermission((IPermission) SecurityManager.executionSecurityPermission);
      if (evidence == null)
        evidence = new Evidence();
      PermissionSet target = SecurityManager.polmgr.Resolve(evidence);
      if (other2 != null)
        target.InplaceIntersect(other2);
      if (checkExecutionPermission && (!target.Contains((IPermission) SecurityManager.executionSecurityPermission) || denyPset != null && denyPset.Contains((IPermission) SecurityManager.executionSecurityPermission)))
        throw new PolicyException(Environment.GetResourceString("Policy_NoExecutionPermission"), -2146233320, exception);
      if (reqdPset != null && !reqdPset.IsSubsetOf(target))
        throw new PolicyException(Environment.GetResourceString("Policy_NoRequiredPermission"), -2146233321, exception);
      if (denyPset != null)
      {
        denied = denyPset.Copy();
        target.MergeDeniedSet(denied);
        if (denied.IsEmpty())
          denied = (PermissionSet) null;
      }
      else
        denied = (PermissionSet) null;
      target.IgnoreTypeLoadFailures = true;
      return target;
    }

    internal static int GetSpecialFlags(PermissionSet grantSet, PermissionSet deniedSet)
    {
      if (grantSet != null && grantSet.IsUnrestricted() && (deniedSet == null || deniedSet.IsEmpty()))
        return -1;
      SecurityPermissionFlag securityPermissionFlags = SecurityPermissionFlag.NoFlags;
      ReflectionPermissionFlag reflectionPermissionFlags = ReflectionPermissionFlag.NoFlags;
      CodeAccessPermission[] accessPermissionArray = new CodeAccessPermission[6];
      if (grantSet != null)
      {
        if (grantSet.IsUnrestricted())
        {
          securityPermissionFlags = SecurityPermissionFlag.AllFlags;
          reflectionPermissionFlags = ReflectionPermissionFlag.AllFlags | ReflectionPermissionFlag.RestrictedMemberAccess;
          for (int index = 0; index < accessPermissionArray.Length; ++index)
            accessPermissionArray[index] = SecurityManager.s_UnrestrictedSpecialPermissionMap[index];
        }
        else
        {
          SecurityPermission securityPermission = grantSet.GetPermission(6) as SecurityPermission;
          if (securityPermission != null)
            securityPermissionFlags = securityPermission.Flags;
          ReflectionPermission reflectionPermission = grantSet.GetPermission(4) as ReflectionPermission;
          if (reflectionPermission != null)
            reflectionPermissionFlags = reflectionPermission.Flags;
          for (int index = 0; index < accessPermissionArray.Length; ++index)
            accessPermissionArray[index] = grantSet.GetPermission(SecurityManager.s_BuiltInPermissionIndexMap[index][0]) as CodeAccessPermission;
        }
      }
      if (deniedSet != null)
      {
        if (deniedSet.IsUnrestricted())
        {
          securityPermissionFlags = SecurityPermissionFlag.NoFlags;
          reflectionPermissionFlags = ReflectionPermissionFlag.NoFlags;
          for (int index = 0; index < SecurityManager.s_BuiltInPermissionIndexMap.Length; ++index)
            accessPermissionArray[index] = (CodeAccessPermission) null;
        }
        else
        {
          SecurityPermission securityPermission = deniedSet.GetPermission(6) as SecurityPermission;
          if (securityPermission != null)
            securityPermissionFlags &= ~securityPermission.Flags;
          ReflectionPermission reflectionPermission = deniedSet.GetPermission(4) as ReflectionPermission;
          if (reflectionPermission != null)
            reflectionPermissionFlags &= ~reflectionPermission.Flags;
          for (int index = 0; index < SecurityManager.s_BuiltInPermissionIndexMap.Length; ++index)
          {
            CodeAccessPermission accessPermission = deniedSet.GetPermission(SecurityManager.s_BuiltInPermissionIndexMap[index][0]) as CodeAccessPermission;
            if (accessPermission != null && !accessPermission.IsSubsetOf((IPermission) null))
              accessPermissionArray[index] = (CodeAccessPermission) null;
          }
        }
      }
      int specialFlags = SecurityManager.MapToSpecialFlags(securityPermissionFlags, reflectionPermissionFlags);
      if (specialFlags != -1)
      {
        for (int index = 0; index < accessPermissionArray.Length; ++index)
        {
          if (accessPermissionArray[index] != null && ((IUnrestrictedPermission) accessPermissionArray[index]).IsUnrestricted())
            specialFlags |= 1 << SecurityManager.s_BuiltInPermissionIndexMap[index][1];
        }
      }
      return specialFlags;
    }

    private static int MapToSpecialFlags(SecurityPermissionFlag securityPermissionFlags, ReflectionPermissionFlag reflectionPermissionFlags)
    {
      int num = 0;
      if ((securityPermissionFlags & SecurityPermissionFlag.UnmanagedCode) == SecurityPermissionFlag.UnmanagedCode)
        num |= 1;
      if ((securityPermissionFlags & SecurityPermissionFlag.SkipVerification) == SecurityPermissionFlag.SkipVerification)
        num |= 2;
      if ((securityPermissionFlags & SecurityPermissionFlag.Assertion) == SecurityPermissionFlag.Assertion)
        num |= 8;
      if ((securityPermissionFlags & SecurityPermissionFlag.SerializationFormatter) == SecurityPermissionFlag.SerializationFormatter)
        num |= 32;
      if ((securityPermissionFlags & SecurityPermissionFlag.BindingRedirects) == SecurityPermissionFlag.BindingRedirects)
        num |= 256;
      if ((securityPermissionFlags & SecurityPermissionFlag.ControlEvidence) == SecurityPermissionFlag.ControlEvidence)
        num |= 65536;
      if ((securityPermissionFlags & SecurityPermissionFlag.ControlPrincipal) == SecurityPermissionFlag.ControlPrincipal)
        num |= 131072;
      if ((reflectionPermissionFlags & ReflectionPermissionFlag.RestrictedMemberAccess) == ReflectionPermissionFlag.RestrictedMemberAccess)
        num |= 64;
      if ((reflectionPermissionFlags & ReflectionPermissionFlag.MemberAccess) == ReflectionPermissionFlag.MemberAccess)
        num |= 16;
      return num;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern bool IsSameType(string strLeft, string strRight);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool _SetThreadSecurity(bool bThreadSecurity);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void GetGrantedPermissions(ObjectHandleOnStack retGranted, ObjectHandleOnStack retDenied, StackCrawlMarkHandle stackMark);
  }
}
