// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.PolicyLevel
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Util;
using System.Text;
using System.Threading;

namespace System.Security.Policy
{
  /// <summary>表示公共语言运行时的安全策略级别。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class PolicyLevel
  {
    private static readonly string[] s_reservedNamedPermissionSets = new string[7]{ "FullTrust", "Nothing", "Execution", "SkipVerification", "Internet", "LocalIntranet", "Everything" };
    private static string[] EcmaFullTrustAssemblies = new string[9]{ "mscorlib.resources", "System", "System.resources", "System.Xml", "System.Xml.resources", "System.Windows.Forms", "System.Windows.Forms.resources", "System.Data", "System.Data.resources" };
    private static string[] MicrosoftFullTrustAssemblies = new string[12]{ "System.Security", "System.Security.resources", "System.Drawing", "System.Drawing.resources", "System.Messaging", "System.Messaging.resources", "System.ServiceProcess", "System.ServiceProcess.resources", "System.DirectoryServices", "System.DirectoryServices.resources", "System.Deployment", "System.Deployment.resources" };
    private ArrayList m_fullTrustAssemblies;
    private ArrayList m_namedPermissionSets;
    private CodeGroup m_rootCodeGroup;
    private string m_label;
    [OptionalField(VersionAdded = 2)]
    private PolicyLevelType m_type;
    private ConfigId m_configId;
    private bool m_useDefaultCodeGroupsOnReset;
    private bool m_generateQuickCacheOnLoad;
    private bool m_caching;
    private bool m_throwOnLoadError;
    private Encoding m_encoding;
    private bool m_loaded;
    private SecurityElement m_permSetElement;
    private string m_path;
    private static object s_InternalSyncObject;

    private static object InternalSyncObject
    {
      get
      {
        if (PolicyLevel.s_InternalSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange(ref PolicyLevel.s_InternalSyncObject, obj, (object) null);
        }
        return PolicyLevel.s_InternalSyncObject;
      }
    }

    /// <summary>获取策略级别的描述性标签。</summary>
    /// <returns>与该策略级别关联的标签。</returns>
    public string Label
    {
      get
      {
        if (this.m_label == null)
          this.m_label = this.DeriveLabelFromType();
        return this.m_label;
      }
    }

    /// <summary>获取策略级别的类型。</summary>
    /// <returns>
    /// <see cref="T:System.Security.PolicyLevelType" /> 值之一。</returns>
    [ComVisible(false)]
    public PolicyLevelType Type
    {
      get
      {
        return this.m_type;
      }
    }

    internal ConfigId ConfigId
    {
      get
      {
        return this.m_configId;
      }
    }

    internal string Path
    {
      get
      {
        return this.m_path;
      }
    }

    /// <summary>获取存储策略文件的路径。</summary>
    /// <returns>存储策略文件的路径，或者 null（如果 <see cref="T:System.Security.Policy.PolicyLevel" /> 没有存储位置）。</returns>
    public string StoreLocation
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)] get
      {
        return PolicyLevel.GetLocationFromType(this.m_type);
      }
    }

    /// <summary>获取或设置策略级别的根代码组。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Policy.CodeGroup" />，它是策略级别代码组的树的根。</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <see cref="P:System.Security.Policy.PolicyLevel.RootCodeGroup" /> 的值为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    public CodeGroup RootCodeGroup
    {
      [SecuritySafeCritical] get
      {
        this.CheckLoaded();
        return this.m_rootCodeGroup;
      }
      [SecuritySafeCritical] set
      {
        if (value == null)
          throw new ArgumentNullException("RootCodeGroup");
        this.CheckLoaded();
        this.m_rootCodeGroup = value.Copy();
      }
    }

    /// <summary>获取为策略级别定义的命名权限集的列表。</summary>
    /// <returns>为策略级别定义的命名权限集的列表。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public IList NamedPermissionSets
    {
      [SecuritySafeCritical] get
      {
        this.CheckLoaded();
        this.LoadAllPermissionSets();
        ArrayList arrayList = new ArrayList(this.m_namedPermissionSets.Count);
        foreach (PermissionSet namedPermissionSet in this.m_namedPermissionSets)
          arrayList.Add((object) namedPermissionSet.Copy());
        return (IList) arrayList;
      }
    }

    /// <summary>获取 <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> 对象列表，这些对象用于确定程序集是否为用于评估安全策略的程序集组的成员。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> 对象列表，这些对象用于确定程序集是否为用于评估安全策略的程序集组的成员。在对列表中没有的程序集进行安全策略评估的过程中，这些程序集被授予完全信任。</returns>
    [Obsolete("Because all GAC assemblies always get full trust, the full trust list is no longer meaningful. You should install any assemblies that are used in security policy in the GAC to ensure they are trusted.")]
    public IList FullTrustAssemblies
    {
      [SecuritySafeCritical] get
      {
        this.CheckLoaded();
        return (IList) new ArrayList((ICollection) this.m_fullTrustAssemblies);
      }
    }

    private PolicyLevel()
    {
    }

    [SecurityCritical]
    internal PolicyLevel(PolicyLevelType type)
      : this(type, PolicyLevel.GetLocationFromType(type))
    {
    }

    internal PolicyLevel(PolicyLevelType type, string path)
      : this(type, path, ConfigId.None)
    {
    }

    internal PolicyLevel(PolicyLevelType type, string path, ConfigId configId)
    {
      this.m_type = type;
      this.m_path = path;
      this.m_loaded = path == null;
      if (this.m_path == null)
      {
        this.m_rootCodeGroup = this.CreateDefaultAllGroup();
        this.SetFactoryPermissionSets();
        this.SetDefaultFullTrustAssemblies();
      }
      this.m_configId = configId;
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      if (this.m_label == null)
        return;
      this.DeriveTypeFromLabel();
    }

    private void DeriveTypeFromLabel()
    {
      if (this.m_label.Equals(Environment.GetResourceString("Policy_PL_User")))
        this.m_type = PolicyLevelType.User;
      else if (this.m_label.Equals(Environment.GetResourceString("Policy_PL_Machine")))
        this.m_type = PolicyLevelType.Machine;
      else if (this.m_label.Equals(Environment.GetResourceString("Policy_PL_Enterprise")))
      {
        this.m_type = PolicyLevelType.Enterprise;
      }
      else
      {
        if (!this.m_label.Equals(Environment.GetResourceString("Policy_PL_AppDomain")))
          throw new ArgumentException(Environment.GetResourceString("Policy_Default"));
        this.m_type = PolicyLevelType.AppDomain;
      }
    }

    private string DeriveLabelFromType()
    {
      switch (this.m_type)
      {
        case PolicyLevelType.User:
          return Environment.GetResourceString("Policy_PL_User");
        case PolicyLevelType.Machine:
          return Environment.GetResourceString("Policy_PL_Machine");
        case PolicyLevelType.Enterprise:
          return Environment.GetResourceString("Policy_PL_Enterprise");
        case PolicyLevelType.AppDomain:
          return Environment.GetResourceString("Policy_PL_AppDomain");
        default:
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) this.m_type));
      }
    }

    [SecurityCritical]
    internal static string GetLocationFromType(PolicyLevelType type)
    {
      switch (type)
      {
        case PolicyLevelType.User:
          return Config.UserDirectory + "security.config";
        case PolicyLevelType.Machine:
          return Config.MachineDirectory + "security.config";
        case PolicyLevelType.Enterprise:
          return Config.MachineDirectory + "enterprisesec.config";
        default:
          return (string) null;
      }
    }

    /// <summary>创建供在应用程序域策略级别使用的新的策略级别。</summary>
    /// <returns>新创建的 <see cref="T:System.Security.Policy.PolicyLevel" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("AppDomain policy levels are obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public static PolicyLevel CreateAppDomainLevel()
    {
      return new PolicyLevel(PolicyLevelType.AppDomain);
    }

    /// <summary>解析策略级别的策略并返回与证据匹配的代码组树的根。</summary>
    /// <returns>
    /// <see cref="T:System.Security.Policy.CodeGroup" />，表示与指定证据匹配的代码组树的根。</returns>
    /// <param name="evidence">用于解析策略的 <see cref="T:System.Security.Policy.Evidence" />。</param>
    /// <exception cref="T:System.Security.Policy.PolicyException">策略级别包含多个标记为独占的匹配代码组。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="evidence" /> 参数为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    public CodeGroup ResolveMatchingCodeGroups(Evidence evidence)
    {
      if (evidence == null)
        throw new ArgumentNullException("evidence");
      return this.RootCodeGroup.ResolveMatchingCodeGroups(evidence);
    }

    /// <summary>将与指定的 <see cref="T:System.Security.Policy.StrongName" /> 对应的 <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> 添加到 <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> 对象列表中，这些对象用于确定程序集是否为不应评估的程序集组的成员。</summary>
    /// <param name="sn">用于创建要添加到 <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> 对象（这些对象用于确定程序集是否为不应评估的程序集组的成员）列表的 <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> 的 <see cref="T:System.Security.Policy.StrongName" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sn" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="sn" /> 参数指定的 <see cref="T:System.Security.Policy.StrongName" /> 已获得完全信任。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [Obsolete("Because all GAC assemblies always get full trust, the full trust list is no longer meaningful. You should install any assemblies that are used in security policy in the GAC to ensure they are trusted.")]
    public void AddFullTrustAssembly(StrongName sn)
    {
      if (sn == null)
        throw new ArgumentNullException("sn");
      this.AddFullTrustAssembly(new StrongNameMembershipCondition(sn.PublicKey, sn.Name, sn.Version));
    }

    /// <summary>将指定的 <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> 添加到 <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> 对象（这些对象用于确定程序集是否为不应评估的程序集组的成员）的列表中。</summary>
    /// <param name="snMC">要添加到 <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> 对象（这些对象用于确定程序集是否为不应评估的程序集组的成员）的列表中的 <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="snMC" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="snMC" /> 参数指定的 <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> 已获得完全信任。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("Because all GAC assemblies always get full trust, the full trust list is no longer meaningful. You should install any assemblies that are used in security policy in the GAC to ensure they are trusted.")]
    public void AddFullTrustAssembly(StrongNameMembershipCondition snMC)
    {
      if (snMC == null)
        throw new ArgumentNullException("snMC");
      this.CheckLoaded();
      foreach (StrongNameMembershipCondition fullTrustAssembly in this.m_fullTrustAssemblies)
      {
        if (fullTrustAssembly.Equals((object) snMC))
          throw new ArgumentException(Environment.GetResourceString("Argument_AssemblyAlreadyFullTrust"));
      }
      lock (this.m_fullTrustAssemblies)
        this.m_fullTrustAssemblies.Add((object) snMC);
    }

    /// <summary>将具有指定 <see cref="T:System.Security.Policy.StrongName" /> 的程序集从策略级别用来评估策略的程序集列表中移除。</summary>
    /// <param name="sn">要从用于评估策略的程序集列表中移除的程序集的 <see cref="T:System.Security.Policy.StrongName" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sn" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">具有 <paramref name="sn" /> 参数所指定的 <see cref="T:System.Security.Policy.StrongName" /> 的程序集未获得完全信任。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [Obsolete("Because all GAC assemblies always get full trust, the full trust list is no longer meaningful. You should install any assemblies that are used in security policy in the GAC to ensure they are trusted.")]
    public void RemoveFullTrustAssembly(StrongName sn)
    {
      if (sn == null)
        throw new ArgumentNullException("assembly");
      this.RemoveFullTrustAssembly(new StrongNameMembershipCondition(sn.PublicKey, sn.Name, sn.Version));
    }

    /// <summary>将具有指定 <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> 的程序集从策略级别用来评估策略的程序集列表中移除。</summary>
    /// <param name="snMC">要从用于评估策略的程序集列表中移除的程序集的 <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="snMC" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="snMC" /> 参数所指定的 <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> 未获得完全信任。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("Because all GAC assemblies always get full trust, the full trust list is no longer meaningful. You should install any assemblies that are used in security policy in the GAC to ensure they are trusted.")]
    public void RemoveFullTrustAssembly(StrongNameMembershipCondition snMC)
    {
      if (snMC == null)
        throw new ArgumentNullException("snMC");
      this.CheckLoaded();
      object obj = (object) null;
      IEnumerator enumerator = this.m_fullTrustAssemblies.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (((StrongNameMembershipCondition) enumerator.Current).Equals((object) snMC))
        {
          obj = enumerator.Current;
          break;
        }
      }
      if (obj == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_AssemblyNotFullTrust"));
      lock (this.m_fullTrustAssemblies)
        this.m_fullTrustAssemblies.Remove(obj);
    }

    /// <summary>将 <see cref="T:System.Security.NamedPermissionSet" /> 添加到当前的策略级别中。</summary>
    /// <param name="permSet">要添加到当前策略级别中的 <see cref="T:System.Security.NamedPermissionSet" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="permSet" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="permSet" /> 参数与 <see cref="T:System.Security.Policy.PolicyLevel" /> 中的现有 <see cref="T:System.Security.NamedPermissionSet" /> 具有相同的名称。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public void AddNamedPermissionSet(NamedPermissionSet permSet)
    {
      if (permSet == null)
        throw new ArgumentNullException("permSet");
      this.CheckLoaded();
      this.LoadAllPermissionSets();
      lock (this)
      {
        foreach (NamedPermissionSet item_0 in this.m_namedPermissionSets)
        {
          if (item_0.Name.Equals(permSet.Name))
            throw new ArgumentException(Environment.GetResourceString("Argument_DuplicateName"));
        }
        NamedPermissionSet local_3 = (NamedPermissionSet) permSet.Copy();
        local_3.IgnoreTypeLoadFailures = true;
        this.m_namedPermissionSets.Add((object) local_3);
      }
    }

    /// <summary>从当前策略级别中移除指定的 <see cref="T:System.Security.NamedPermissionSet" />。</summary>
    /// <returns>已移除的 <see cref="T:System.Security.NamedPermissionSet" />。</returns>
    /// <param name="permSet">要从当前策略级别中移除的 <see cref="T:System.Security.NamedPermissionSet" />。</param>
    /// <exception cref="T:System.ArgumentException">未找到 <paramref name="permSet" /> 参数所指定的 <see cref="T:System.Security.NamedPermissionSet" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="permSet" /> 参数为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    public NamedPermissionSet RemoveNamedPermissionSet(NamedPermissionSet permSet)
    {
      if (permSet == null)
        throw new ArgumentNullException("permSet");
      return this.RemoveNamedPermissionSet(permSet.Name);
    }

    /// <summary>从当前策略级别中移除具有指定名称的 <see cref="T:System.Security.NamedPermissionSet" />。</summary>
    /// <returns>已移除的 <see cref="T:System.Security.NamedPermissionSet" />。</returns>
    /// <param name="name">要移除的 <see cref="T:System.Security.NamedPermissionSet" /> 的名称。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 参数等于保留权限集的名称。- 或 -找不到具有指定名称的 <see cref="T:System.Security.NamedPermissionSet" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public NamedPermissionSet RemoveNamedPermissionSet(string name)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      this.CheckLoaded();
      this.LoadAllPermissionSets();
      int index1 = -1;
      for (int index2 = 0; index2 < PolicyLevel.s_reservedNamedPermissionSets.Length; ++index2)
      {
        if (PolicyLevel.s_reservedNamedPermissionSets[index2].Equals(name))
          throw new ArgumentException(Environment.GetResourceString("Argument_ReservedNPMS", (object) name));
      }
      ArrayList arrayList1 = this.m_namedPermissionSets;
      for (int index2 = 0; index2 < arrayList1.Count; ++index2)
      {
        if (((NamedPermissionSet) arrayList1[index2]).Name.Equals(name))
        {
          index1 = index2;
          break;
        }
      }
      if (index1 == -1)
        throw new ArgumentException(Environment.GetResourceString("Argument_NoNPMS"));
      ArrayList arrayList2 = new ArrayList();
      arrayList2.Add((object) this.m_rootCodeGroup);
      for (int index2 = 0; index2 < arrayList2.Count; ++index2)
      {
        CodeGroup codeGroup = (CodeGroup) arrayList2[index2];
        if (codeGroup.PermissionSetName != null && codeGroup.PermissionSetName.Equals(name))
          throw new ArgumentException(Environment.GetResourceString("Argument_NPMSInUse", (object) name));
        IEnumerator enumerator = codeGroup.Children.GetEnumerator();
        if (enumerator != null)
        {
          while (enumerator.MoveNext())
            arrayList2.Add(enumerator.Current);
        }
      }
      NamedPermissionSet namedPermissionSet = (NamedPermissionSet) arrayList1[index1];
      arrayList1.RemoveAt(index1);
      return namedPermissionSet;
    }

    /// <summary>用指定的 <see cref="T:System.Security.PermissionSet" /> 替换当前策略级别中的 <see cref="T:System.Security.NamedPermissionSet" />。</summary>
    /// <returns>已替换的 <see cref="T:System.Security.NamedPermissionSet" /> 的副本。</returns>
    /// <param name="name">要替换的 <see cref="T:System.Security.NamedPermissionSet" /> 的名称。</param>
    /// <param name="pSet">用于替换 <paramref name="name" /> 参数所指定的 <see cref="T:System.Security.NamedPermissionSet" /> 的 <see cref="T:System.Security.PermissionSet" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。- 或 -<paramref name="pSet" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 参数等于保留权限集的名称。- 或 -找不到 <paramref name="pSet" /> 参数所指定的 <see cref="T:System.Security.PermissionSet" />。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public NamedPermissionSet ChangeNamedPermissionSet(string name, PermissionSet pSet)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (pSet == null)
        throw new ArgumentNullException("pSet");
      for (int index = 0; index < PolicyLevel.s_reservedNamedPermissionSets.Length; ++index)
      {
        if (PolicyLevel.s_reservedNamedPermissionSets[index].Equals(name))
          throw new ArgumentException(Environment.GetResourceString("Argument_ReservedNPMS", (object) name));
      }
      NamedPermissionSet permissionSetInternal = this.GetNamedPermissionSetInternal(name);
      if (permissionSetInternal == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_NoNPMS"));
      NamedPermissionSet namedPermissionSet = (NamedPermissionSet) permissionSetInternal.Copy();
      permissionSetInternal.Reset();
      permissionSetInternal.SetUnrestricted(pSet.IsUnrestricted());
      foreach (IPermission p in pSet)
        permissionSetInternal.SetPermission(p.Copy());
      if (pSet is NamedPermissionSet)
        permissionSetInternal.Description = ((NamedPermissionSet) pSet).Description;
      return namedPermissionSet;
    }

    /// <summary>返回具有指定名称的当前策略级别中的 <see cref="T:System.Security.NamedPermissionSet" />。</summary>
    /// <returns>如果找到，则为具有指定名称的当前策略级别中的 <see cref="T:System.Security.NamedPermissionSet" />；否则为 null。</returns>
    /// <param name="name">要查找的 <see cref="T:System.Security.NamedPermissionSet" /> 的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    [SecuritySafeCritical]
    public NamedPermissionSet GetNamedPermissionSet(string name)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      NamedPermissionSet permissionSetInternal = this.GetNamedPermissionSetInternal(name);
      if (permissionSetInternal != null)
        return new NamedPermissionSet(permissionSetInternal);
      return (NamedPermissionSet) null;
    }

    /// <summary>用上一次的备份（反映上一次保存之前的策略状态）替换此 <see cref="T:System.Security.Policy.PolicyLevel" /> 的配置文件，并将它返回到上一次保存的状态。</summary>
    /// <exception cref="T:System.Security.Policy.PolicyException">该策略级别没有有效的配置文件。</exception>
    [SecuritySafeCritical]
    public void Recover()
    {
      if (this.m_configId == ConfigId.None)
        throw new PolicyException(Environment.GetResourceString("Policy_RecoverNotFileBased"));
      lock (this)
      {
        if (!Config.RecoverData(this.m_configId))
          throw new PolicyException(Environment.GetResourceString("Policy_RecoverNoConfigFile"));
        this.m_loaded = false;
        this.m_rootCodeGroup = (CodeGroup) null;
        this.m_namedPermissionSets = (ArrayList) null;
        this.m_fullTrustAssemblies = new ArrayList();
      }
    }

    /// <summary>将当前策略级别返回到默认状态。</summary>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public void Reset()
    {
      this.SetDefault();
    }

    /// <summary>根据策略级别的证据移除策略，并返回产生的 <see cref="T:System.Security.Policy.PolicyStatement" />。</summary>
    /// <returns>产生的 <see cref="T:System.Security.Policy.PolicyStatement" />。</returns>
    /// <param name="evidence">用于解析 <see cref="T:System.Security.Policy.PolicyLevel" /> 的 <see cref="T:System.Security.Policy.Evidence" />。</param>
    /// <exception cref="T:System.Security.Policy.PolicyException">策略级别包含多个标记为独占的匹配代码组。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="evidence" /> 参数为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public PolicyStatement Resolve(Evidence evidence)
    {
      return this.Resolve(evidence, 0, (byte[]) null);
    }

    /// <summary>创建安全对象及其当前状态的 XML 编码。</summary>
    /// <returns>安全对象的 XML 编码，包括任何状态信息。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public SecurityElement ToXml()
    {
      this.CheckLoaded();
      this.LoadAllPermissionSets();
      SecurityElement securityElement = new SecurityElement("PolicyLevel");
      securityElement.AddAttribute("version", "1");
      Hashtable classes = new Hashtable();
      lock (this)
      {
        SecurityElement local_5 = new SecurityElement("NamedPermissionSets");
        foreach (PermissionSet item_0 in this.m_namedPermissionSets)
          local_5.AddChild(this.NormalizeClassDeep(item_0.ToXml(), classes));
        SecurityElement local_6 = this.NormalizeClassDeep(this.m_rootCodeGroup.ToXml(this), classes);
        SecurityElement local_7 = new SecurityElement("FullTrustAssemblies");
        foreach (StrongNameMembershipCondition item_1 in this.m_fullTrustAssemblies)
          local_7.AddChild(this.NormalizeClassDeep(item_1.ToXml(), classes));
        SecurityElement local_8 = new SecurityElement("SecurityClasses");
        IDictionaryEnumerator local_9 = classes.GetEnumerator();
        while (local_9.MoveNext())
        {
          SecurityElement local_10 = new SecurityElement("SecurityClass");
          local_10.AddAttribute("Name", (string) local_9.Value);
          local_10.AddAttribute("Description", (string) local_9.Key);
          local_8.AddChild(local_10);
        }
        securityElement.AddChild(local_8);
        securityElement.AddChild(local_5);
        securityElement.AddChild(local_6);
        securityElement.AddChild(local_7);
      }
      return securityElement;
    }

    /// <summary>从 XML 编码重新构造具有给定状态的安全对象。</summary>
    /// <param name="e">用于重新构造安全对象的 XML 编码。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="e" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="e" /> 参数所指定的 <see cref="T:System.Security.SecurityElement" /> 无效。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public void FromXml(SecurityElement e)
    {
      if (e == null)
        throw new ArgumentNullException("e");
      lock (this)
      {
        ArrayList local_3 = new ArrayList();
        SecurityElement local_4 = e.SearchForChildByTag("SecurityClasses");
        Hashtable local_0;
        if (local_4 != null)
        {
          local_0 = new Hashtable();
          foreach (SecurityElement item_0 in local_4.Children)
          {
            if (item_0.Tag.Equals("SecurityClass"))
            {
              string local_13 = item_0.Attribute("Name");
              string local_14 = item_0.Attribute("Description");
              if (local_13 != null && local_14 != null)
                local_0.Add((object) local_13, (object) local_14);
            }
          }
        }
        else
          local_0 = (Hashtable) null;
        SecurityElement local_5 = e.SearchForChildByTag("FullTrustAssemblies");
        if (local_5 != null && local_5.InternalChildren != null)
        {
          string temp_128 = typeof (StrongNameMembershipCondition).AssemblyQualifiedName;
          foreach (SecurityElement item_1 in local_5.Children)
          {
            StrongNameMembershipCondition local_16 = new StrongNameMembershipCondition();
            local_16.FromXml(item_1);
            local_3.Add((object) local_16);
          }
        }
        this.m_fullTrustAssemblies = local_3;
        ArrayList local_6 = new ArrayList();
        SecurityElement local_7 = e.SearchForChildByTag("NamedPermissionSets");
        SecurityElement local_8 = (SecurityElement) null;
        if (local_7 != null && local_7.InternalChildren != null)
        {
          local_8 = this.UnnormalizeClassDeep(local_7, local_0);
          foreach (string item_2 in PolicyLevel.s_reservedNamedPermissionSets)
            this.FindElement(local_8, item_2);
        }
        if (local_8 == null)
          local_8 = new SecurityElement("NamedPermissionSets");
        local_6.Add((object) BuiltInPermissionSets.FullTrust);
        local_6.Add((object) BuiltInPermissionSets.Everything);
        local_6.Add((object) BuiltInPermissionSets.SkipVerification);
        local_6.Add((object) BuiltInPermissionSets.Execution);
        local_6.Add((object) BuiltInPermissionSets.Nothing);
        local_6.Add((object) BuiltInPermissionSets.Internet);
        local_6.Add((object) BuiltInPermissionSets.LocalIntranet);
        foreach (PermissionSet item_3 in local_6)
          item_3.IgnoreTypeLoadFailures = true;
        this.m_namedPermissionSets = local_6;
        this.m_permSetElement = local_8;
        SecurityElement local_9 = e.SearchForChildByTag("CodeGroup");
        if (local_9 == null)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXMLElement", (object) "CodeGroup", (object) this.GetType().FullName));
        CodeGroup local_10 = XMLUtil.CreateCodeGroup(this.UnnormalizeClassDeep(local_9, local_0));
        if (local_10 == null)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXMLElement", (object) "CodeGroup", (object) this.GetType().FullName));
        local_10.FromXml(local_9, this);
        this.m_rootCodeGroup = local_10;
      }
    }

    [SecurityCritical]
    internal static PermissionSet GetBuiltInSet(string name)
    {
      if (string.IsNullOrEmpty(name))
        return (PermissionSet) null;
      if (name.Equals("FullTrust"))
        return (PermissionSet) BuiltInPermissionSets.FullTrust;
      if (name.Equals("Nothing"))
        return (PermissionSet) BuiltInPermissionSets.Nothing;
      if (name.Equals("Execution"))
        return (PermissionSet) BuiltInPermissionSets.Execution;
      if (name.Equals("SkipVerification"))
        return (PermissionSet) BuiltInPermissionSets.SkipVerification;
      if (name.Equals("Internet"))
        return (PermissionSet) BuiltInPermissionSets.Internet;
      if (name.Equals("LocalIntranet"))
        return (PermissionSet) BuiltInPermissionSets.LocalIntranet;
      return (PermissionSet) null;
    }

    [SecurityCritical]
    internal NamedPermissionSet GetNamedPermissionSetInternal(string name)
    {
      this.CheckLoaded();
      lock (PolicyLevel.InternalSyncObject)
      {
        foreach (NamedPermissionSet item_0 in this.m_namedPermissionSets)
        {
          if (item_0.Name.Equals(name))
            return item_0;
        }
        if (this.m_permSetElement != null)
        {
          SecurityElement local_6 = this.FindElement(this.m_permSetElement, name);
          if (local_6 != null)
          {
            NamedPermissionSet local_7 = new NamedPermissionSet();
            local_7.Name = name;
            this.m_namedPermissionSets.Add((object) local_7);
            try
            {
              local_7.FromXml(local_6, false, true);
            }
            catch
            {
              this.m_namedPermissionSets.Remove((object) local_7);
              return (NamedPermissionSet) null;
            }
            if (local_7.Name != null)
              return local_7;
            this.m_namedPermissionSets.Remove((object) local_7);
          }
        }
      }
      return (NamedPermissionSet) null;
    }

    [SecurityCritical]
    internal PolicyStatement Resolve(Evidence evidence, int count, byte[] serializedEvidence)
    {
      if (evidence == null)
        throw new ArgumentNullException("evidence");
      PolicyStatement policy = (PolicyStatement) null;
      if (serializedEvidence != null)
        policy = this.CheckCache(count, serializedEvidence);
      if (policy == null)
      {
        this.CheckLoaded();
        bool allConst;
        if ((this.m_fullTrustAssemblies == null ? 0 : (PolicyLevel.IsFullTrustAssembly(this.m_fullTrustAssemblies, evidence) ? 1 : 0)) != 0)
        {
          policy = new PolicyStatement(new PermissionSet(true), PolicyStatementAttribute.Nothing);
          allConst = true;
        }
        else
        {
          ArrayList arrayList = this.GenericResolve(evidence, out allConst);
          policy = new PolicyStatement();
          policy.PermissionSet = (PermissionSet) null;
          foreach (CodeGroupStackFrame codeGroupStackFrame in arrayList)
          {
            PolicyStatement policyStatement = codeGroupStackFrame.policy;
            if (policyStatement != null)
            {
              policy.GetPermissionSetNoCopy().InplaceUnion(policyStatement.GetPermissionSetNoCopy());
              policy.Attributes |= policyStatement.Attributes;
              if (policyStatement.HasDependentEvidence)
              {
                foreach (IDelayEvaluatedEvidence evaluatedEvidence in policyStatement.DependentEvidence)
                  evaluatedEvidence.MarkUsed();
              }
            }
          }
        }
        if (allConst)
          this.Cache(count, evidence.RawSerialize(), policy);
      }
      return policy;
    }

    [SecurityCritical]
    private void CheckLoaded()
    {
      if (this.m_loaded)
        return;
      lock (PolicyLevel.InternalSyncObject)
      {
        if (this.m_loaded)
          return;
        this.LoadPolicyLevel();
      }
    }

    private static byte[] ReadFile(string fileName)
    {
      using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
      {
        int count = (int) fileStream.Length;
        byte[] buffer = new byte[count];
        fileStream.Read(buffer, 0, count);
        fileStream.Close();
        return buffer;
      }
    }

    [SecurityCritical]
    private void LoadPolicyLevel()
    {
      Exception exception = (Exception) null;
      CodeAccessPermission.Assert(true);
      if (File.InternalExists(this.m_path))
      {
        Encoding utF8 = Encoding.UTF8;
        SecurityElement securityElement1;
        try
        {
          securityElement1 = SecurityElement.FromString(utF8.GetString(PolicyLevel.ReadFile(this.m_path)));
        }
        catch (Exception ex)
        {
          exception = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParseEx", (object) this.Label, (object) (string.IsNullOrEmpty(ex.Message) ? ex.GetType().AssemblyQualifiedName : ex.Message)));
          goto label_17;
        }
        if (securityElement1 == null)
        {
          exception = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", (object) this.Label));
        }
        else
        {
          SecurityElement securityElement2 = securityElement1.SearchForChildByTag("mscorlib");
          if (securityElement2 == null)
          {
            exception = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", (object) this.Label));
          }
          else
          {
            SecurityElement securityElement3 = securityElement2.SearchForChildByTag("security");
            if (securityElement3 == null)
            {
              exception = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", (object) this.Label));
            }
            else
            {
              SecurityElement securityElement4 = securityElement3.SearchForChildByTag("policy");
              if (securityElement4 == null)
              {
                exception = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", (object) this.Label));
              }
              else
              {
                SecurityElement e = securityElement4.SearchForChildByTag("PolicyLevel");
                if (e != null)
                {
                  try
                  {
                    this.FromXml(e);
                  }
                  catch (Exception ex)
                  {
                    exception = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", (object) this.Label));
                    goto label_17;
                  }
                  this.m_loaded = true;
                  return;
                }
                exception = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", (object) this.Label));
              }
            }
          }
        }
      }
label_17:
      this.SetDefault();
      this.m_loaded = true;
      if (exception != null)
        throw exception;
    }

    [SecurityCritical]
    private Exception LoadError(string message)
    {
      if (this.m_type != PolicyLevelType.User && this.m_type != PolicyLevelType.Machine && this.m_type != PolicyLevelType.Enterprise)
        return (Exception) new ArgumentException(message);
      Config.WriteToEventLog(message);
      return (Exception) null;
    }

    [SecurityCritical]
    private void Cache(int count, byte[] serializedEvidence, PolicyStatement policy)
    {
      if (this.m_configId == ConfigId.None || serializedEvidence == null)
        return;
      byte[] data = new SecurityDocument(policy.ToXml((PolicyLevel) null, true)).m_data;
      Config.AddCacheEntry(this.m_configId, count, serializedEvidence, data);
    }

    [SecurityCritical]
    private PolicyStatement CheckCache(int count, byte[] serializedEvidence)
    {
      if (this.m_configId == ConfigId.None)
        return (PolicyStatement) null;
      if (serializedEvidence == null)
        return (PolicyStatement) null;
      byte[] data;
      if (!Config.GetCacheEntry(this.m_configId, count, serializedEvidence, out data))
        return (PolicyStatement) null;
      PolicyStatement policyStatement = new PolicyStatement();
      SecurityDocument doc = new SecurityDocument(data);
      int position = 0;
      // ISSUE: variable of the null type
      __Null local = null;
      int num = 1;
      policyStatement.FromXml(doc, position, (PolicyLevel) local, num != 0);
      return policyStatement;
    }

    [SecurityCritical]
    private static bool IsFullTrustAssembly(ArrayList fullTrustAssemblies, Evidence evidence)
    {
      if (fullTrustAssemblies.Count == 0 || evidence == null)
        return false;
      lock (fullTrustAssemblies)
      {
        foreach (StrongNameMembershipCondition item_0 in fullTrustAssemblies)
        {
          if (item_0.Check(evidence))
          {
            if (Environment.GetCompatibilityFlag(CompatibilityFlag.FullTrustListAssembliesInGac))
            {
              if (new ZoneMembershipCondition().Check(evidence))
                return true;
            }
            else if (new GacMembershipCondition().Check(evidence))
              return true;
          }
        }
      }
      return false;
    }

    private CodeGroup CreateDefaultAllGroup()
    {
      UnionCodeGroup unionCodeGroup = new UnionCodeGroup();
      SecurityElement codeGroupElement = PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "FullTrust", new AllMembershipCondition().ToXml());
      unionCodeGroup.FromXml(codeGroupElement, this);
      string resourceString1 = Environment.GetResourceString("Policy_AllCode_Name");
      unionCodeGroup.Name = resourceString1;
      string resourceString2 = Environment.GetResourceString("Policy_AllCode_DescriptionFullTrust");
      unionCodeGroup.Description = resourceString2;
      return (CodeGroup) unionCodeGroup;
    }

    [SecurityCritical]
    private CodeGroup CreateDefaultMachinePolicy()
    {
      UnionCodeGroup unionCodeGroup1 = new UnionCodeGroup();
      SecurityElement codeGroupElement = PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "Nothing", new AllMembershipCondition().ToXml());
      unionCodeGroup1.FromXml(codeGroupElement, this);
      string resourceString1 = Environment.GetResourceString("Policy_AllCode_Name");
      unionCodeGroup1.Name = resourceString1;
      string resourceString2 = Environment.GetResourceString("Policy_AllCode_DescriptionNothing");
      unionCodeGroup1.Description = resourceString2;
      UnionCodeGroup unionCodeGroup2 = new UnionCodeGroup();
      unionCodeGroup2.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "FullTrust", new ZoneMembershipCondition(SecurityZone.MyComputer).ToXml()), this);
      unionCodeGroup2.Name = Environment.GetResourceString("Policy_MyComputer_Name");
      unionCodeGroup2.Description = Environment.GetResourceString("Policy_MyComputer_Description");
      StrongNamePublicKeyBlob blob1 = new StrongNamePublicKeyBlob("002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293");
      UnionCodeGroup unionCodeGroup3 = new UnionCodeGroup();
      unionCodeGroup3.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "FullTrust", new StrongNameMembershipCondition(blob1, (string) null, (Version) null).ToXml()), this);
      unionCodeGroup3.Name = Environment.GetResourceString("Policy_Microsoft_Name");
      unionCodeGroup3.Description = Environment.GetResourceString("Policy_Microsoft_Description");
      unionCodeGroup2.AddChildInternal((CodeGroup) unionCodeGroup3);
      StrongNamePublicKeyBlob blob2 = new StrongNamePublicKeyBlob("00000000000000000400000000000000");
      UnionCodeGroup unionCodeGroup4 = new UnionCodeGroup();
      unionCodeGroup4.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "FullTrust", new StrongNameMembershipCondition(blob2, (string) null, (Version) null).ToXml()), this);
      unionCodeGroup4.Name = Environment.GetResourceString("Policy_Ecma_Name");
      unionCodeGroup4.Description = Environment.GetResourceString("Policy_Ecma_Description");
      unionCodeGroup2.AddChildInternal((CodeGroup) unionCodeGroup4);
      UnionCodeGroup unionCodeGroup5 = unionCodeGroup2;
      unionCodeGroup1.AddChildInternal((CodeGroup) unionCodeGroup5);
      CodeGroup codeGroup1 = (CodeGroup) new UnionCodeGroup();
      codeGroup1.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "LocalIntranet", new ZoneMembershipCondition(SecurityZone.Intranet).ToXml()), this);
      codeGroup1.Name = Environment.GetResourceString("Policy_Intranet_Name");
      codeGroup1.Description = Environment.GetResourceString("Policy_Intranet_Description");
      CodeGroup group1 = (CodeGroup) new NetCodeGroup((IMembershipCondition) new AllMembershipCondition());
      group1.Name = Environment.GetResourceString("Policy_IntranetNet_Name");
      group1.Description = Environment.GetResourceString("Policy_IntranetNet_Description");
      codeGroup1.AddChildInternal(group1);
      CodeGroup group2 = (CodeGroup) new FileCodeGroup((IMembershipCondition) new AllMembershipCondition(), FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery);
      group2.Name = Environment.GetResourceString("Policy_IntranetFile_Name");
      group2.Description = Environment.GetResourceString("Policy_IntranetFile_Description");
      codeGroup1.AddChildInternal(group2);
      CodeGroup group3 = codeGroup1;
      unionCodeGroup1.AddChildInternal(group3);
      CodeGroup codeGroup2 = (CodeGroup) new UnionCodeGroup();
      codeGroup2.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "Internet", new ZoneMembershipCondition(SecurityZone.Internet).ToXml()), this);
      codeGroup2.Name = Environment.GetResourceString("Policy_Internet_Name");
      codeGroup2.Description = Environment.GetResourceString("Policy_Internet_Description");
      CodeGroup group4 = (CodeGroup) new NetCodeGroup((IMembershipCondition) new AllMembershipCondition());
      group4.Name = Environment.GetResourceString("Policy_InternetNet_Name");
      group4.Description = Environment.GetResourceString("Policy_InternetNet_Description");
      codeGroup2.AddChildInternal(group4);
      CodeGroup group5 = codeGroup2;
      unionCodeGroup1.AddChildInternal(group5);
      CodeGroup codeGroup3 = (CodeGroup) new UnionCodeGroup();
      codeGroup3.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "Nothing", new ZoneMembershipCondition(SecurityZone.Untrusted).ToXml()), this);
      codeGroup3.Name = Environment.GetResourceString("Policy_Untrusted_Name");
      codeGroup3.Description = Environment.GetResourceString("Policy_Untrusted_Description");
      CodeGroup group6 = codeGroup3;
      unionCodeGroup1.AddChildInternal(group6);
      CodeGroup codeGroup4 = (CodeGroup) new UnionCodeGroup();
      codeGroup4.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "Internet", new ZoneMembershipCondition(SecurityZone.Trusted).ToXml()), this);
      codeGroup4.Name = Environment.GetResourceString("Policy_Trusted_Name");
      codeGroup4.Description = Environment.GetResourceString("Policy_Trusted_Description");
      CodeGroup group7 = (CodeGroup) new NetCodeGroup((IMembershipCondition) new AllMembershipCondition());
      group7.Name = Environment.GetResourceString("Policy_TrustedNet_Name");
      group7.Description = Environment.GetResourceString("Policy_TrustedNet_Description");
      codeGroup4.AddChildInternal(group7);
      CodeGroup group8 = codeGroup4;
      unionCodeGroup1.AddChildInternal(group8);
      return (CodeGroup) unionCodeGroup1;
    }

    private static SecurityElement CreateCodeGroupElement(string codeGroupType, string permissionSetName, SecurityElement mshipElement)
    {
      SecurityElement securityElement = new SecurityElement("CodeGroup");
      string name1 = "class";
      string str1 = "System.Security." + codeGroupType + ", mscorlib, Version={VERSION}, Culture=neutral, PublicKeyToken=b77a5c561934e089" ?? "";
      securityElement.AddAttribute(name1, str1);
      string name2 = "version";
      string str2 = "1";
      securityElement.AddAttribute(name2, str2);
      string name3 = "PermissionSetName";
      string str3 = permissionSetName;
      securityElement.AddAttribute(name3, str3);
      SecurityElement child = mshipElement;
      securityElement.AddChild(child);
      return securityElement;
    }

    private void SetDefaultFullTrustAssemblies()
    {
      this.m_fullTrustAssemblies = new ArrayList();
      StrongNamePublicKeyBlob blob1 = new StrongNamePublicKeyBlob("00000000000000000400000000000000");
      for (int index = 0; index < PolicyLevel.EcmaFullTrustAssemblies.Length; ++index)
        this.m_fullTrustAssemblies.Add((object) new StrongNameMembershipCondition(blob1, PolicyLevel.EcmaFullTrustAssemblies[index], new Version("4.0.0.0")));
      StrongNamePublicKeyBlob blob2 = new StrongNamePublicKeyBlob("002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293");
      for (int index = 0; index < PolicyLevel.MicrosoftFullTrustAssemblies.Length; ++index)
        this.m_fullTrustAssemblies.Add((object) new StrongNameMembershipCondition(blob2, PolicyLevel.MicrosoftFullTrustAssemblies[index], new Version("4.0.0.0")));
    }

    [SecurityCritical]
    private void SetDefault()
    {
      lock (this)
      {
        string local_2 = PolicyLevel.GetLocationFromType(this.m_type) + ".default";
        if (File.InternalExists(local_2))
        {
          PolicyLevel local_3 = new PolicyLevel(this.m_type, local_2);
          this.m_rootCodeGroup = local_3.RootCodeGroup;
          this.m_namedPermissionSets = (ArrayList) local_3.NamedPermissionSets;
          this.m_fullTrustAssemblies = (ArrayList) local_3.FullTrustAssemblies;
          this.m_loaded = true;
        }
        else
        {
          this.m_namedPermissionSets = (ArrayList) null;
          this.m_rootCodeGroup = (CodeGroup) null;
          this.m_permSetElement = (SecurityElement) null;
          this.m_rootCodeGroup = this.m_type == PolicyLevelType.Machine ? this.CreateDefaultMachinePolicy() : this.CreateDefaultAllGroup();
          this.SetFactoryPermissionSets();
          this.SetDefaultFullTrustAssemblies();
          this.m_loaded = true;
        }
      }
    }

    private void SetFactoryPermissionSets()
    {
      lock (PolicyLevel.InternalSyncObject)
      {
        this.m_namedPermissionSets = new ArrayList();
        this.m_namedPermissionSets.Add((object) BuiltInPermissionSets.FullTrust);
        this.m_namedPermissionSets.Add((object) BuiltInPermissionSets.Everything);
        this.m_namedPermissionSets.Add((object) BuiltInPermissionSets.Nothing);
        this.m_namedPermissionSets.Add((object) BuiltInPermissionSets.SkipVerification);
        this.m_namedPermissionSets.Add((object) BuiltInPermissionSets.Execution);
        this.m_namedPermissionSets.Add((object) BuiltInPermissionSets.Internet);
        this.m_namedPermissionSets.Add((object) BuiltInPermissionSets.LocalIntranet);
      }
    }

    private SecurityElement FindElement(SecurityElement element, string name)
    {
      foreach (SecurityElement child in element.Children)
      {
        if (child.Tag.Equals("PermissionSet"))
        {
          string str = child.Attribute("Name");
          if (str != null && str.Equals(name))
          {
            element.InternalChildren.Remove((object) child);
            return child;
          }
        }
      }
      return (SecurityElement) null;
    }

    [SecurityCritical]
    private void LoadAllPermissionSets()
    {
      if (this.m_permSetElement == null || this.m_permSetElement.InternalChildren == null)
        return;
      lock (PolicyLevel.InternalSyncObject)
      {
        while (this.m_permSetElement != null && this.m_permSetElement.InternalChildren.Count != 0)
        {
          SecurityElement local_2 = (SecurityElement) this.m_permSetElement.Children[this.m_permSetElement.InternalChildren.Count - 1];
          this.m_permSetElement.InternalChildren.RemoveAt(this.m_permSetElement.InternalChildren.Count - 1);
          if (local_2.Tag.Equals("PermissionSet") && local_2.Attribute("class").Equals("System.Security.NamedPermissionSet"))
          {
            NamedPermissionSet local_3 = new NamedPermissionSet();
            local_3.FromXmlNameOnly(local_2);
            if (local_3.Name != null)
            {
              this.m_namedPermissionSets.Add((object) local_3);
              try
              {
                local_3.FromXml(local_2, false, true);
              }
              catch
              {
                this.m_namedPermissionSets.Remove((object) local_3);
              }
            }
          }
        }
        this.m_permSetElement = (SecurityElement) null;
      }
    }

    [SecurityCritical]
    private ArrayList GenericResolve(Evidence evidence, out bool allConst)
    {
      CodeGroupStack codeGroupStack = new CodeGroupStack();
      CodeGroup codeGroup = this.m_rootCodeGroup;
      if (codeGroup == null)
        throw new PolicyException(Environment.GetResourceString("Policy_NonFullTrustAssembly"));
      codeGroupStack.Push(new CodeGroupStackFrame()
      {
        current = codeGroup,
        parent = (CodeGroupStackFrame) null
      });
      ArrayList arrayList = new ArrayList();
      bool flag = false;
      allConst = true;
      Exception exception = (Exception) null;
      while (!codeGroupStack.IsEmpty())
      {
        CodeGroupStackFrame codeGroupStackFrame = codeGroupStack.Pop();
        FirstMatchCodeGroup firstMatchCodeGroup = codeGroupStackFrame.current as FirstMatchCodeGroup;
        UnionCodeGroup unionCodeGroup = codeGroupStackFrame.current as UnionCodeGroup;
        if (!(codeGroupStackFrame.current.MembershipCondition is IConstantMembershipCondition) || unionCodeGroup == null && firstMatchCodeGroup == null)
          allConst = false;
        try
        {
          codeGroupStackFrame.policy = PolicyManager.ResolveCodeGroup(codeGroupStackFrame.current, evidence);
        }
        catch (Exception ex)
        {
          if (exception == null)
            exception = ex;
        }
        if (codeGroupStackFrame.policy != null)
        {
          if ((codeGroupStackFrame.policy.Attributes & PolicyStatementAttribute.Exclusive) != PolicyStatementAttribute.Nothing)
          {
            if (flag)
              throw new PolicyException(Environment.GetResourceString("Policy_MultipleExclusive"));
            arrayList.RemoveRange(0, arrayList.Count);
            arrayList.Add((object) codeGroupStackFrame);
            flag = true;
          }
          if (!flag)
            arrayList.Add((object) codeGroupStackFrame);
        }
      }
      if (exception != null)
        throw exception;
      return arrayList;
    }

    private static string GenerateFriendlyName(string className, Hashtable classes)
    {
      if (classes.ContainsKey((object) className))
        return (string) classes[(object) className];
      System.Type type = System.Type.GetType(className, false, false);
      if (type != (System.Type) null && !type.IsVisible)
        type = (System.Type) null;
      if (type == (System.Type) null)
        return className;
      if (!classes.ContainsValue((object) type.Name))
      {
        classes.Add((object) className, (object) type.Name);
        return type.Name;
      }
      if (!classes.ContainsValue((object) type.FullName))
      {
        classes.Add((object) className, (object) type.FullName);
        return type.FullName;
      }
      classes.Add((object) className, (object) type.AssemblyQualifiedName);
      return type.AssemblyQualifiedName;
    }

    private SecurityElement NormalizeClassDeep(SecurityElement elem, Hashtable classes)
    {
      this.NormalizeClass(elem, classes);
      if (elem.InternalChildren != null && elem.InternalChildren.Count > 0)
      {
        foreach (SecurityElement child in elem.Children)
          this.NormalizeClassDeep(child, classes);
      }
      return elem;
    }

    private SecurityElement NormalizeClass(SecurityElement elem, Hashtable classes)
    {
      if (elem.m_lAttributes == null || elem.m_lAttributes.Count == 0)
        return elem;
      int count = elem.m_lAttributes.Count;
      int index = 0;
      while (index < count)
      {
        if (((string) elem.m_lAttributes[index]).Equals("class"))
        {
          string className = (string) elem.m_lAttributes[index + 1];
          elem.m_lAttributes[index + 1] = (object) PolicyLevel.GenerateFriendlyName(className, classes);
          break;
        }
        index += 2;
      }
      return elem;
    }

    private SecurityElement UnnormalizeClassDeep(SecurityElement elem, Hashtable classes)
    {
      this.UnnormalizeClass(elem, classes);
      if (elem.InternalChildren != null && elem.InternalChildren.Count > 0)
      {
        foreach (SecurityElement child in elem.Children)
          this.UnnormalizeClassDeep(child, classes);
      }
      return elem;
    }

    private SecurityElement UnnormalizeClass(SecurityElement elem, Hashtable classes)
    {
      if (classes == null || elem.m_lAttributes == null || elem.m_lAttributes.Count == 0)
        return elem;
      int count = elem.m_lAttributes.Count;
      int index = 0;
      while (index < count)
      {
        if (((string) elem.m_lAttributes[index]).Equals("class"))
        {
          string str1 = (string) elem.m_lAttributes[index + 1];
          string str2 = (string) classes[(object) str1];
          if (str2 != null)
          {
            elem.m_lAttributes[index + 1] = (object) str2;
            break;
          }
          break;
        }
        index += 2;
      }
      return elem;
    }
  }
}
