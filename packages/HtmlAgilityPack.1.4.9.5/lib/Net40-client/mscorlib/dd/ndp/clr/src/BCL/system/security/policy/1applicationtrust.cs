// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.ApplicationTrustCollection
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Deployment.Internal.Isolation;
using System.Deployment.Internal.Isolation.Manifest;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Security.Policy
{
  /// <summary>表示 <see cref="T:System.Security.Policy.ApplicationTrust" /> 对象的集合。此类不能被继承。</summary>
  [SecurityCritical]
  [ComVisible(true)]
  public sealed class ApplicationTrustCollection : ICollection, IEnumerable
  {
    private static Guid ClrPropertySet = new Guid("c989bb7a-8385-4715-98cf-a741a8edb823");
    private static object s_installReference = (object) null;
    private const string ApplicationTrustProperty = "ApplicationTrust";
    private const string InstallerIdentifier = "{60051b8f-4f12-400a-8e50-dd05ebd438d1}";
    private object m_appTrusts;
    private bool m_storeBounded;
    private Store m_pStore;

    private static StoreApplicationReference InstallReference
    {
      get
      {
        if (ApplicationTrustCollection.s_installReference == null)
          Interlocked.CompareExchange(ref ApplicationTrustCollection.s_installReference, (object) new StoreApplicationReference(IsolationInterop.GUID_SXS_INSTALL_REFERENCE_SCHEME_OPAQUESTRING, "{60051b8f-4f12-400a-8e50-dd05ebd438d1}", (string) null), (object) null);
        return (StoreApplicationReference) ApplicationTrustCollection.s_installReference;
      }
    }

    private ArrayList AppTrusts
    {
      [SecurityCritical] get
      {
        if (this.m_appTrusts == null)
        {
          ArrayList arrayList = new ArrayList();
          if (this.m_storeBounded)
          {
            this.RefreshStorePointer();
            foreach (IDefinitionAppId installerDeployment in this.m_pStore.EnumInstallerDeployments(IsolationInterop.GUID_SXS_INSTALL_REFERENCE_SCHEME_OPAQUESTRING, "{60051b8f-4f12-400a-8e50-dd05ebd438d1}", "ApplicationTrust", (IReferenceAppId) null))
            {
              foreach (StoreOperationMetadataProperty deploymentProperty in this.m_pStore.EnumInstallerDeploymentProperties(IsolationInterop.GUID_SXS_INSTALL_REFERENCE_SCHEME_OPAQUESTRING, "{60051b8f-4f12-400a-8e50-dd05ebd438d1}", "ApplicationTrust", installerDeployment))
              {
                string xml = deploymentProperty.Value;
                if (xml != null && xml.Length > 0)
                {
                  SecurityElement element = SecurityElement.FromString(xml);
                  ApplicationTrust applicationTrust = new ApplicationTrust();
                  applicationTrust.FromXml(element);
                  arrayList.Add((object) applicationTrust);
                }
              }
            }
          }
          Interlocked.CompareExchange(ref this.m_appTrusts, (object) arrayList, (object) null);
        }
        return this.m_appTrusts as ArrayList;
      }
    }

    /// <summary>获取集合中包含的项数。</summary>
    /// <returns>集合中包含的项数。</returns>
    public int Count
    {
      [SecuritySafeCritical] get
      {
        return this.AppTrusts.Count;
      }
    }

    /// <summary>获取集合中位于指定索引处的 <see cref="T:System.Security.Policy.ApplicationTrust" /> 对象。</summary>
    /// <returns>集合中指定索引处的 <see cref="T:System.Security.Policy.ApplicationTrust" /> 对象。</returns>
    /// <param name="index">对象在集合中的从零开始的索引。</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 大于或等于集合中的对象数。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="index" /> 为负数。</exception>
    public ApplicationTrust this[int index]
    {
      [SecurityCritical] get
      {
        return this.AppTrusts[index] as ApplicationTrust;
      }
    }

    /// <summary>获取指定应用程序的 <see cref="T:System.Security.Policy.ApplicationTrust" /> 对象。</summary>
    /// <returns>指定应用程序的 <see cref="T:System.Security.Policy.ApplicationTrust" />，如果无法找到该对象则为 null。</returns>
    /// <param name="appFullName">应用程序的全名。</param>
    public ApplicationTrust this[string appFullName]
    {
      [SecurityCritical] get
      {
        ApplicationTrustCollection applicationTrustCollection = this.Find(new ApplicationIdentity(appFullName), ApplicationVersionMatch.MatchExactVersion);
        if (applicationTrustCollection.Count > 0)
          return applicationTrustCollection[0];
        return (ApplicationTrust) null;
      }
    }

    /// <summary>获取一个值，该值指示对集合的访问是否为同步的（线程安全）。</summary>
    /// <returns>所有情况下均为 false。</returns>
    public bool IsSynchronized
    {
      [SecuritySafeCritical] get
      {
        return false;
      }
    }

    /// <summary>获取可用于同步对集合的访问的对象。</summary>
    /// <returns>用于同步对集合的访问的对象。</returns>
    public object SyncRoot
    {
      [SecuritySafeCritical] get
      {
        return (object) this;
      }
    }

    [SecurityCritical]
    internal ApplicationTrustCollection()
      : this(false)
    {
    }

    internal ApplicationTrustCollection(bool storeBounded)
    {
      this.m_storeBounded = storeBounded;
    }

    [SecurityCritical]
    private void RefreshStorePointer()
    {
      if (this.m_pStore != null)
        Marshal.ReleaseComObject((object) this.m_pStore.InternalStore);
      this.m_pStore = IsolationInterop.GetUserStore();
    }

    [SecurityCritical]
    private void CommitApplicationTrust(ApplicationIdentity applicationIdentity, string trustXml)
    {
      StoreOperationMetadataProperty[] SetProperties = new StoreOperationMetadataProperty[1]{ new StoreOperationMetadataProperty(ApplicationTrustCollection.ClrPropertySet, "ApplicationTrust", trustXml) };
      IEnumDefinitionIdentity definitionIdentity1 = applicationIdentity.Identity.EnumAppPath();
      IDefinitionIdentity[] definitionIdentityArray = new IDefinitionIdentity[1];
      IDefinitionIdentity definitionIdentity2 = (IDefinitionIdentity) null;
      int num = 1;
      IDefinitionIdentity[] DefinitionIdentity = definitionIdentityArray;
      if ((int) definitionIdentity1.Next((uint) num, DefinitionIdentity) == 1)
        definitionIdentity2 = definitionIdentityArray[0];
      IDefinitionAppId definition = IsolationInterop.AppIdAuthority.CreateDefinition();
      definition.SetAppPath(1U, new IDefinitionIdentity[1]
      {
        definitionIdentity2
      });
      definition.put_Codebase(applicationIdentity.CodeBase);
      using (StoreTransaction storeTransaction = new StoreTransaction())
      {
        storeTransaction.Add(new StoreOperationSetDeploymentMetadata(definition, ApplicationTrustCollection.InstallReference, SetProperties));
        this.RefreshStorePointer();
        this.m_pStore.Transact(storeTransaction.Operations);
      }
      this.m_appTrusts = (object) null;
    }

    /// <summary>向集合中添加一个元素。</summary>
    /// <returns>新元素位置处插入的索引。</returns>
    /// <param name="trust">要添加的 <see cref="T:System.Security.Policy.ApplicationTrust" /> 对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="trust" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="trust" /> 中指定的 <see cref="T:System.Security.Policy.ApplicationTrust" /> 的 <see cref="P:System.Security.Policy.ApplicationTrust.ApplicationIdentity" /> 属性为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecurityCritical]
    public int Add(ApplicationTrust trust)
    {
      if (trust == null)
        throw new ArgumentNullException("trust");
      if (trust.ApplicationIdentity == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_ApplicationTrustShouldHaveIdentity"));
      if (!this.m_storeBounded)
        return this.AppTrusts.Add((object) trust);
      this.CommitApplicationTrust(trust.ApplicationIdentity, trust.ToXml().ToString());
      return -1;
    }

    /// <summary>将指定的 <see cref="T:System.Security.Policy.ApplicationTrust" /> 数组的元素复制到集合末尾。</summary>
    /// <param name="trusts">
    /// <see cref="T:System.Security.Policy.ApplicationTrust" /> 类型的数组，包含要添加到集合的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="trusts" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="trust" /> 中指定的 <see cref="T:System.Security.Policy.ApplicationTrust" /> 的 <see cref="P:System.Security.Policy.ApplicationTrust.ApplicationIdentity" /> 属性为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecurityCritical]
    public void AddRange(ApplicationTrust[] trusts)
    {
      if (trusts == null)
        throw new ArgumentNullException("trusts");
      int index1 = 0;
      try
      {
        for (; index1 < trusts.Length; ++index1)
          this.Add(trusts[index1]);
      }
      catch
      {
        for (int index2 = 0; index2 < index1; ++index2)
          this.Remove(trusts[index2]);
        throw;
      }
    }

    /// <summary>将指定 <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> 的元素复制到集合末尾。</summary>
    /// <param name="trusts">
    /// <see cref="T:System.Security.Policy.ApplicationTrustCollection" />，包含要添加到集合的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="trusts" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="trust" /> 中指定的 <see cref="T:System.Security.Policy.ApplicationTrust" /> 的 <see cref="P:System.Security.Policy.ApplicationTrust.ApplicationIdentity" /> 属性为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecurityCritical]
    public void AddRange(ApplicationTrustCollection trusts)
    {
      if (trusts == null)
        throw new ArgumentNullException("trusts");
      int num = 0;
      try
      {
        foreach (ApplicationTrust trust in trusts)
        {
          this.Add(trust);
          ++num;
        }
      }
      catch
      {
        for (int index = 0; index < num; ++index)
          this.Remove(trusts[index]);
        throw;
      }
    }

    /// <summary>获取集合中与指定应用程序标识匹配的应用程序信任。</summary>
    /// <returns>包含所有匹配的 <see cref="T:System.Security.Policy.ApplicationTrust" /> 对象的 <see cref="T:System.Security.Policy.ApplicationTrustCollection" />。</returns>
    /// <param name="applicationIdentity">描述要查找的应用程序的 <see cref="T:System.ApplicationIdentity" /> 对象。</param>
    /// <param name="versionMatch">
    /// <see cref="T:System.Security.Policy.ApplicationVersionMatch" /> 值之一。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecurityCritical]
    public ApplicationTrustCollection Find(ApplicationIdentity applicationIdentity, ApplicationVersionMatch versionMatch)
    {
      ApplicationTrustCollection applicationTrustCollection = new ApplicationTrustCollection(false);
      foreach (ApplicationTrust trust in this)
      {
        if (CmsUtils.CompareIdentities(trust.ApplicationIdentity, applicationIdentity, versionMatch))
          applicationTrustCollection.Add(trust);
      }
      return applicationTrustCollection;
    }

    /// <summary>从集合中移除与指定条件匹配的所有应用程序信任对象。</summary>
    /// <param name="applicationIdentity">要移除的 <see cref="T:System.Security.Policy.ApplicationTrust" /> 对象的 <see cref="T:System.ApplicationIdentity" />。</param>
    /// <param name="versionMatch">
    /// <see cref="T:System.Security.Policy.ApplicationVersionMatch" /> 值之一。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecurityCritical]
    public void Remove(ApplicationIdentity applicationIdentity, ApplicationVersionMatch versionMatch)
    {
      this.RemoveRange(this.Find(applicationIdentity, versionMatch));
    }

    /// <summary>从集合中移除指定的应用程序信任。</summary>
    /// <param name="trust">要移除的 <see cref="T:System.Security.Policy.ApplicationTrust" /> 对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="trust" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">由 <paramref name="trust" /> 指定的 <see cref="T:System.Security.Policy.ApplicationTrust" /> 对象的 <see cref="P:System.Security.Policy.ApplicationTrust.ApplicationIdentity" /> 属性为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecurityCritical]
    public void Remove(ApplicationTrust trust)
    {
      if (trust == null)
        throw new ArgumentNullException("trust");
      if (trust.ApplicationIdentity == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_ApplicationTrustShouldHaveIdentity"));
      if (this.m_storeBounded)
        this.CommitApplicationTrust(trust.ApplicationIdentity, (string) null);
      else
        this.AppTrusts.Remove((object) trust);
    }

    /// <summary>从集合中移除指定数组中的应用程序信任对象。</summary>
    /// <param name="trusts">包含要从当前集合中移除的项的 <see cref="T:System.Security.Policy.ApplicationTrust" /> 类型的一维数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="trusts" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecurityCritical]
    public void RemoveRange(ApplicationTrust[] trusts)
    {
      if (trusts == null)
        throw new ArgumentNullException("trusts");
      int index1 = 0;
      try
      {
        for (; index1 < trusts.Length; ++index1)
          this.Remove(trusts[index1]);
      }
      catch
      {
        for (int index2 = 0; index2 < index1; ++index2)
          this.Add(trusts[index2]);
        throw;
      }
    }

    /// <summary>从集合中移除指定集合中的应用程序信任对象。</summary>
    /// <param name="trusts">包含要从当前集合中移除的项的 <see cref="T:System.Security.Policy.ApplicationTrustCollection" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="trusts" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
    /// </PermissionSet>
    [SecurityCritical]
    public void RemoveRange(ApplicationTrustCollection trusts)
    {
      if (trusts == null)
        throw new ArgumentNullException("trusts");
      int num = 0;
      try
      {
        foreach (ApplicationTrust trust in trusts)
        {
          this.Remove(trust);
          ++num;
        }
      }
      catch
      {
        for (int index = 0; index < num; ++index)
          this.Add(trusts[index]);
        throw;
      }
    }

    /// <summary>从集合中移除所有应用程序信任。</summary>
    /// <exception cref="T:System.ArgumentException">集合中某一项的 <see cref="P:System.Security.Policy.ApplicationTrust.ApplicationIdentity" /> 属性为 null。</exception>
    [SecurityCritical]
    public void Clear()
    {
      ArrayList appTrusts = this.AppTrusts;
      if (this.m_storeBounded)
      {
        foreach (ApplicationTrust applicationTrust in appTrusts)
        {
          if (applicationTrust.ApplicationIdentity == null)
            throw new ArgumentException(Environment.GetResourceString("Argument_ApplicationTrustShouldHaveIdentity"));
          this.CommitApplicationTrust(applicationTrust.ApplicationIdentity, (string) null);
        }
      }
      appTrusts.Clear();
    }

    /// <summary>返回一个可用于循环访问该集合的对象。</summary>
    /// <returns>可用于循环访问集合的 <see cref="T:System.Security.Policy.ApplicationTrustEnumerator" />。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPolicy" />
    /// </PermissionSet>
    public ApplicationTrustEnumerator GetEnumerator()
    {
      return new ApplicationTrustEnumerator(this);
    }

    [SecuritySafeCritical]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new ApplicationTrustEnumerator(this);
    }

    [SecuritySafeCritical]
    void ICollection.CopyTo(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (array.Rank != 1)
        throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
      if (index < 0 || index >= array.Length)
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      if (array.Length - index < this.Count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      for (int index1 = 0; index1 < this.Count; ++index1)
        array.SetValue((object) this[index1], index++);
    }

    /// <summary>从目标数组的指定索引处开始，将整个集合复制到兼容的一维数组。</summary>
    /// <param name="array">
    /// <see cref="T:System.Security.Policy.ApplicationTrust" /> 类型的一维数组，它是从当前集合复制的元素的目标数组。</param>
    /// <param name="index">
    /// <paramref name="array" /> 中从零开始的索引，从此索引处开始进行复制。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index" /> 小于 <paramref name="array" /> 的下限。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="array" /> 是多维的。- 或 -该 <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> 中的元素数目大于从 <paramref name="index" /> 到目标 <paramref name="array" /> 末尾之间的可用空间。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPolicy" />
    /// </PermissionSet>
    public void CopyTo(ApplicationTrust[] array, int index)
    {
      ((ICollection) this).CopyTo((Array) array, index);
    }
  }
}
