// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.Evidence
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Threading;

namespace System.Security.Policy
{
  /// <summary>定义组成对安全策略决策的输入的一组信息。此类不能被继承。</summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class Evidence : ICollection, IEnumerable
  {
    [OptionalField(VersionAdded = 4)]
    private Dictionary<Type, EvidenceTypeDescriptor> m_evidence;
    [OptionalField(VersionAdded = 4)]
    private bool m_deserializedTargetEvidence;
    private volatile ArrayList m_hostList;
    private volatile ArrayList m_assemblyList;
    [NonSerialized]
    private ReaderWriterLock m_evidenceLock;
    [NonSerialized]
    private uint m_version;
    [NonSerialized]
    private IRuntimeEvidenceFactory m_target;
    private bool m_locked;
    [NonSerialized]
    private WeakReference m_cloneOrigin;
    private static volatile Type[] s_runtimeEvidenceTypes;
    private const int LockTimeout = 5000;

    internal static Type[] RuntimeEvidenceTypes
    {
      get
      {
        if (Evidence.s_runtimeEvidenceTypes == null)
        {
          Type[] array = new Type[10]{ typeof (ActivationArguments), typeof (ApplicationDirectory), typeof (ApplicationTrust), typeof (GacInstalled), typeof (Hash), typeof (Publisher), typeof (Site), typeof (StrongName), typeof (Url), typeof (Zone) };
          if (AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
          {
            int length = array.Length;
            Array.Resize<Type>(ref array, length + 1);
            array[length] = typeof (PermissionRequestEvidence);
          }
          Evidence.s_runtimeEvidenceTypes = array;
        }
        return Evidence.s_runtimeEvidenceTypes;
      }
    }

    private bool IsReaderLockHeld
    {
      get
      {
        if (this.m_evidenceLock != null)
          return this.m_evidenceLock.IsReaderLockHeld;
        return true;
      }
    }

    private bool IsWriterLockHeld
    {
      get
      {
        if (this.m_evidenceLock != null)
          return this.m_evidenceLock.IsWriterLockHeld;
        return true;
      }
    }

    internal bool IsUnmodified
    {
      get
      {
        return (int) this.m_version == 0;
      }
    }

    /// <summary>获取或设置一个值，该值指示证据是否是锁定的。</summary>
    /// <returns>如果证据是锁定的，则为 true；否则为 false。默认值为 false。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    public bool Locked
    {
      get
      {
        return this.m_locked;
      }
      [SecuritySafeCritical] set
      {
        if (!value)
        {
          new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
          this.m_locked = false;
        }
        else
          this.m_locked = true;
      }
    }

    internal IRuntimeEvidenceFactory Target
    {
      get
      {
        return this.m_target;
      }
      [SecurityCritical] set
      {
        using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Writer))
        {
          this.m_target = value;
          this.QueryHostForPossibleEvidenceTypes();
        }
      }
    }

    /// <summary>获取证据集中证据对象的数目。</summary>
    /// <returns>证据集中证据对象的数目。</returns>
    [Obsolete("Evidence should not be treated as an ICollection. Please use GetHostEnumerator and GetAssemblyEnumerator to iterate over the evidence to collect a count.")]
    public int Count
    {
      get
      {
        int num = 0;
        IEnumerator hostEnumerator = this.GetHostEnumerator();
        while (hostEnumerator.MoveNext())
          ++num;
        IEnumerator assemblyEnumerator = this.GetAssemblyEnumerator();
        while (assemblyEnumerator.MoveNext())
          ++num;
        return num;
      }
    }

    [ComVisible(false)]
    internal int RawCount
    {
      get
      {
        int num = 0;
        using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
        {
          foreach (Type evidenceType in new List<Type>((IEnumerable<Type>) this.m_evidence.Keys))
          {
            EvidenceTypeDescriptor evidenceTypeDescriptor = this.GetEvidenceTypeDescriptor(evidenceType);
            if (evidenceTypeDescriptor != null)
            {
              if (evidenceTypeDescriptor.AssemblyEvidence != null)
                ++num;
              if (evidenceTypeDescriptor.HostEvidence != null)
                ++num;
            }
          }
        }
        return num;
      }
    }

    /// <summary>获取同步根。</summary>
    /// <returns>因为不支持证据集的同步，所以总是为 this（在 Visual Basic 中为 Me）。</returns>
    public object SyncRoot
    {
      get
      {
        return (object) this;
      }
    }

    /// <summary>获取一个值，该值指示证据集是否线程安全。</summary>
    /// <returns>因为不支持线程安全证据集，所以总是为 false。</returns>
    public bool IsSynchronized
    {
      get
      {
        return true;
      }
    }

    /// <summary>获取一个值，该值指示证据集是否为只读。</summary>
    /// <returns>因为不支持只读证据集，所以总是为 false。</returns>
    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.Policy.Evidence" /> 类的新的空实例。</summary>
    public Evidence()
    {
      this.m_evidence = new Dictionary<Type, EvidenceTypeDescriptor>();
      this.m_evidenceLock = new ReaderWriterLock();
    }

    /// <summary>从现有证据的浅表副本初始化 <see cref="T:System.Security.Policy.Evidence" /> 类的新实例。</summary>
    /// <param name="evidence">从其创建新实例的 <see cref="T:System.Security.Policy.Evidence" /> 实例。此实例不是深层复制的。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="evidence" /> 参数不是 <see cref="T:System.Security.Policy.Evidence" /> 的有效实例。</exception>
    public Evidence(Evidence evidence)
    {
      this.m_evidence = new Dictionary<Type, EvidenceTypeDescriptor>();
      if (evidence != null)
      {
        using (new Evidence.EvidenceLockHolder(evidence, Evidence.EvidenceLockHolder.LockType.Reader))
        {
          foreach (KeyValuePair<Type, EvidenceTypeDescriptor> keyValuePair in evidence.m_evidence)
          {
            EvidenceTypeDescriptor evidenceTypeDescriptor = keyValuePair.Value;
            if (evidenceTypeDescriptor != null)
              evidenceTypeDescriptor = evidenceTypeDescriptor.Clone();
            this.m_evidence[keyValuePair.Key] = evidenceTypeDescriptor;
          }
          this.m_target = evidence.m_target;
          this.m_locked = evidence.m_locked;
          this.m_deserializedTargetEvidence = evidence.m_deserializedTargetEvidence;
          if (evidence.Target != null)
            this.m_cloneOrigin = new WeakReference((object) evidence);
        }
      }
      this.m_evidenceLock = new ReaderWriterLock();
    }

    /// <summary>依据多个主机和程序集证据集初始化 <see cref="T:System.Security.Policy.Evidence" /> 类的新实例。</summary>
    /// <param name="hostEvidence">创建新实例所依据的主机证据。</param>
    /// <param name="assemblyEvidence">创建新实例所依据的程序集证据。</param>
    [Obsolete("This constructor is obsolete. Please use the constructor which takes arrays of EvidenceBase instead.")]
    public Evidence(object[] hostEvidence, object[] assemblyEvidence)
    {
      this.m_evidence = new Dictionary<Type, EvidenceTypeDescriptor>();
      if (hostEvidence != null)
      {
        foreach (object id in hostEvidence)
          this.AddHost(id);
      }
      if (assemblyEvidence != null)
      {
        foreach (object id in assemblyEvidence)
          this.AddAssembly(id);
      }
      this.m_evidenceLock = new ReaderWriterLock();
    }

    /// <summary>依据多个主机和程序集证据集初始化 <see cref="T:System.Security.Policy.Evidence" /> 类的新实例。</summary>
    /// <param name="hostEvidence">创建新实例所依据的主机证据。</param>
    /// <param name="assemblyEvidence">创建新实例所依据的程序集证据。</param>
    public Evidence(EvidenceBase[] hostEvidence, EvidenceBase[] assemblyEvidence)
    {
      this.m_evidence = new Dictionary<Type, EvidenceTypeDescriptor>();
      if (hostEvidence != null)
      {
        foreach (EvidenceBase evidence in hostEvidence)
          this.AddHostEvidence(evidence, Evidence.GetEvidenceIndexType(evidence), Evidence.DuplicateEvidenceAction.Throw);
      }
      if (assemblyEvidence != null)
      {
        foreach (EvidenceBase evidence in assemblyEvidence)
          this.AddAssemblyEvidence(evidence, Evidence.GetEvidenceIndexType(evidence), Evidence.DuplicateEvidenceAction.Throw);
      }
      this.m_evidenceLock = new ReaderWriterLock();
    }

    [SecuritySafeCritical]
    internal Evidence(IRuntimeEvidenceFactory target)
    {
      this.m_evidence = new Dictionary<Type, EvidenceTypeDescriptor>();
      this.m_target = target;
      foreach (Type runtimeEvidenceType in Evidence.RuntimeEvidenceTypes)
        this.m_evidence[runtimeEvidenceType] = (EvidenceTypeDescriptor) null;
      this.QueryHostForPossibleEvidenceTypes();
      this.m_evidenceLock = new ReaderWriterLock();
    }

    private void AcquireReaderLock()
    {
      if (this.m_evidenceLock == null)
        return;
      this.m_evidenceLock.AcquireReaderLock(5000);
    }

    private void AcquireWriterlock()
    {
      if (this.m_evidenceLock == null)
        return;
      this.m_evidenceLock.AcquireWriterLock(5000);
    }

    private void DowngradeFromWriterLock(ref LockCookie lockCookie)
    {
      if (this.m_evidenceLock == null)
        return;
      this.m_evidenceLock.DowngradeFromWriterLock(ref lockCookie);
    }

    private LockCookie UpgradeToWriterLock()
    {
      if (this.m_evidenceLock == null)
        return new LockCookie();
      return this.m_evidenceLock.UpgradeToWriterLock(5000);
    }

    private void ReleaseReaderLock()
    {
      if (this.m_evidenceLock == null)
        return;
      this.m_evidenceLock.ReleaseReaderLock();
    }

    private void ReleaseWriterLock()
    {
      if (this.m_evidenceLock == null)
        return;
      this.m_evidenceLock.ReleaseWriterLock();
    }

    /// <summary>将主机提供的指定证据添加到证据集。</summary>
    /// <param name="id">任意证据对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="id" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="id" /> 不可序列化。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [Obsolete("This method is obsolete. Please use AddHostEvidence instead.")]
    [SecuritySafeCritical]
    public void AddHost(object id)
    {
      if (id == null)
        throw new ArgumentNullException("id");
      if (!id.GetType().IsSerializable)
        throw new ArgumentException(Environment.GetResourceString("Policy_EvidenceMustBeSerializable"), "id");
      if (this.m_locked)
        new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
      EvidenceBase evidence = Evidence.WrapLegacyEvidence(id);
      Type evidenceIndexType = Evidence.GetEvidenceIndexType(evidence);
      this.AddHostEvidence(evidence, evidenceIndexType, Evidence.DuplicateEvidenceAction.Merge);
    }

    /// <summary>将指定的程序集证据添加到证据集。</summary>
    /// <param name="id">任意证据对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="id" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="id" /> 不可序列化。</exception>
    [Obsolete("This method is obsolete. Please use AddAssemblyEvidence instead.")]
    public void AddAssembly(object id)
    {
      if (id == null)
        throw new ArgumentNullException("id");
      if (!id.GetType().IsSerializable)
        throw new ArgumentException(Environment.GetResourceString("Policy_EvidenceMustBeSerializable"), "id");
      EvidenceBase evidence = Evidence.WrapLegacyEvidence(id);
      Type evidenceIndexType = Evidence.GetEvidenceIndexType(evidence);
      this.AddAssemblyEvidence(evidence, evidenceIndexType, Evidence.DuplicateEvidenceAction.Merge);
    }

    /// <summary>将指定类型的证据对象添加到程序集提供的证据列表。</summary>
    /// <param name="evidence">要添加的程序集证据。</param>
    /// <typeparam name="T">
    /// <paramref name="evidence" /> 中对象的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="evidence" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">类型 <paramref name="T" /> 的证据已在列表中。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="evidence" /> 不可序列化。</exception>
    [ComVisible(false)]
    public void AddAssemblyEvidence<T>(T evidence) where T : EvidenceBase
    {
      if ((object) evidence == null)
        throw new ArgumentNullException("evidence");
      Type evidenceType = typeof (T);
      if (typeof (T) == typeof (EvidenceBase) || (object) evidence is ILegacyEvidenceAdapter)
        evidenceType = Evidence.GetEvidenceIndexType((EvidenceBase) evidence);
      this.AddAssemblyEvidence((EvidenceBase) evidence, evidenceType, Evidence.DuplicateEvidenceAction.Throw);
    }

    private void AddAssemblyEvidence(EvidenceBase evidence, Type evidenceType, Evidence.DuplicateEvidenceAction duplicateAction)
    {
      using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Writer))
        this.AddAssemblyEvidenceNoLock(evidence, evidenceType, duplicateAction);
    }

    private void AddAssemblyEvidenceNoLock(EvidenceBase evidence, Type evidenceType, Evidence.DuplicateEvidenceAction duplicateAction)
    {
      this.DeserializeTargetEvidence();
      EvidenceTypeDescriptor evidenceTypeDescriptor = this.GetEvidenceTypeDescriptor(evidenceType, true);
      this.m_version = this.m_version + 1U;
      if (evidenceTypeDescriptor.AssemblyEvidence == null)
        evidenceTypeDescriptor.AssemblyEvidence = evidence;
      else
        evidenceTypeDescriptor.AssemblyEvidence = Evidence.HandleDuplicateEvidence(evidenceTypeDescriptor.AssemblyEvidence, evidence, duplicateAction);
    }

    /// <summary>将指定类型的主机证据添加到主机证据集合。</summary>
    /// <param name="evidence">要添加的主机证据。</param>
    /// <typeparam name="T">
    /// <paramref name="evidence" /> 中对象的类型。</typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="evidence" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">类型 <paramref name="T" /> 的证据已在列表中。</exception>
    [ComVisible(false)]
    public void AddHostEvidence<T>(T evidence) where T : EvidenceBase
    {
      if ((object) evidence == null)
        throw new ArgumentNullException("evidence");
      Type evidenceType = typeof (T);
      if (typeof (T) == typeof (EvidenceBase) || (object) evidence is ILegacyEvidenceAdapter)
        evidenceType = Evidence.GetEvidenceIndexType((EvidenceBase) evidence);
      this.AddHostEvidence((EvidenceBase) evidence, evidenceType, Evidence.DuplicateEvidenceAction.Throw);
    }

    [SecuritySafeCritical]
    private void AddHostEvidence(EvidenceBase evidence, Type evidenceType, Evidence.DuplicateEvidenceAction duplicateAction)
    {
      if (this.Locked)
        new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
      using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Writer))
        this.AddHostEvidenceNoLock(evidence, evidenceType, duplicateAction);
    }

    private void AddHostEvidenceNoLock(EvidenceBase evidence, Type evidenceType, Evidence.DuplicateEvidenceAction duplicateAction)
    {
      EvidenceTypeDescriptor evidenceTypeDescriptor = this.GetEvidenceTypeDescriptor(evidenceType, true);
      this.m_version = this.m_version + 1U;
      if (evidenceTypeDescriptor.HostEvidence == null)
        evidenceTypeDescriptor.HostEvidence = evidence;
      else
        evidenceTypeDescriptor.HostEvidence = Evidence.HandleDuplicateEvidence(evidenceTypeDescriptor.HostEvidence, evidence, duplicateAction);
    }

    [SecurityCritical]
    private void QueryHostForPossibleEvidenceTypes()
    {
      if (AppDomain.CurrentDomain.DomainManager == null)
        return;
      HostSecurityManager hostSecurityManager = AppDomain.CurrentDomain.DomainManager.HostSecurityManager;
      if (hostSecurityManager == null)
        return;
      Type[] typeArray = (Type[]) null;
      AppDomain appDomain = this.m_target.Target as AppDomain;
      Assembly assembly = this.m_target.Target as Assembly;
      if (assembly != (Assembly) null && (hostSecurityManager.Flags & HostSecurityManagerOptions.HostAssemblyEvidence) == HostSecurityManagerOptions.HostAssemblyEvidence)
        typeArray = hostSecurityManager.GetHostSuppliedAssemblyEvidenceTypes(assembly);
      else if (appDomain != null && (hostSecurityManager.Flags & HostSecurityManagerOptions.HostAppDomainEvidence) == HostSecurityManagerOptions.HostAppDomainEvidence)
        typeArray = hostSecurityManager.GetHostSuppliedAppDomainEvidenceTypes();
      if (typeArray == null)
        return;
      foreach (Type evidenceType in typeArray)
        this.GetEvidenceTypeDescriptor(evidenceType, true).HostCanGenerate = true;
    }

    private static Type GetEvidenceIndexType(EvidenceBase evidence)
    {
      ILegacyEvidenceAdapter legacyEvidenceAdapter = evidence as ILegacyEvidenceAdapter;
      if (legacyEvidenceAdapter != null)
        return legacyEvidenceAdapter.EvidenceType;
      return evidence.GetType();
    }

    internal EvidenceTypeDescriptor GetEvidenceTypeDescriptor(Type evidenceType)
    {
      return this.GetEvidenceTypeDescriptor(evidenceType, false);
    }

    private EvidenceTypeDescriptor GetEvidenceTypeDescriptor(Type evidenceType, bool addIfNotExist)
    {
      EvidenceTypeDescriptor evidenceTypeDescriptor = (EvidenceTypeDescriptor) null;
      if (!this.m_evidence.TryGetValue(evidenceType, out evidenceTypeDescriptor) && !addIfNotExist)
        return (EvidenceTypeDescriptor) null;
      if (evidenceTypeDescriptor == null)
      {
        evidenceTypeDescriptor = new EvidenceTypeDescriptor();
        bool flag = false;
        LockCookie lockCookie = new LockCookie();
        try
        {
          if (!this.IsWriterLockHeld)
          {
            lockCookie = this.UpgradeToWriterLock();
            flag = true;
          }
          this.m_evidence[evidenceType] = evidenceTypeDescriptor;
        }
        finally
        {
          if (flag)
            this.DowngradeFromWriterLock(ref lockCookie);
        }
      }
      return evidenceTypeDescriptor;
    }

    private static EvidenceBase HandleDuplicateEvidence(EvidenceBase original, EvidenceBase duplicate, Evidence.DuplicateEvidenceAction action)
    {
      switch (action)
      {
        case Evidence.DuplicateEvidenceAction.Throw:
          throw new InvalidOperationException(Environment.GetResourceString("Policy_DuplicateEvidence", (object) duplicate.GetType().FullName));
        case Evidence.DuplicateEvidenceAction.Merge:
          LegacyEvidenceList legacyEvidenceList = original as LegacyEvidenceList;
          if (legacyEvidenceList == null)
          {
            legacyEvidenceList = new LegacyEvidenceList();
            legacyEvidenceList.Add(original);
          }
          legacyEvidenceList.Add(duplicate);
          return (EvidenceBase) legacyEvidenceList;
        case Evidence.DuplicateEvidenceAction.SelectNewObject:
          return duplicate;
        default:
          return (EvidenceBase) null;
      }
    }

    private static EvidenceBase WrapLegacyEvidence(object evidence)
    {
      return evidence as EvidenceBase ?? (EvidenceBase) new LegacyEvidenceWrapper(evidence);
    }

    private static object UnwrapEvidence(EvidenceBase evidence)
    {
      ILegacyEvidenceAdapter legacyEvidenceAdapter = evidence as ILegacyEvidenceAdapter;
      if (legacyEvidenceAdapter != null)
        return legacyEvidenceAdapter.EvidenceObject;
      return (object) evidence;
    }

    /// <summary>将指定的证据集合并到当前证据集。</summary>
    /// <param name="evidence">要合并到当前证据集的证据集。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="evidence" /> 参数不是 <see cref="T:System.Security.Policy.Evidence" /> 的有效实例。</exception>
    /// <exception cref="T:System.Security.SecurityException">
    /// <see cref="P:System.Security.Policy.Evidence.Locked" /> 为 true，调用该方法的代码不具有 <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlEvidence" />，并且 <paramref name="evidence" /> 参数具有一个不为空的主机列表。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public void Merge(Evidence evidence)
    {
      if (evidence == null)
        return;
      using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Writer))
      {
        bool flag = false;
        IEnumerator hostEnumerator = evidence.GetHostEnumerator();
        while (hostEnumerator.MoveNext())
        {
          if (this.Locked && !flag)
          {
            new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
            flag = true;
          }
          Type type = hostEnumerator.Current.GetType();
          if (this.m_evidence.ContainsKey(type))
            this.GetHostEvidenceNoLock(type);
          EvidenceBase evidence1 = Evidence.WrapLegacyEvidence(hostEnumerator.Current);
          this.AddHostEvidenceNoLock(evidence1, Evidence.GetEvidenceIndexType(evidence1), Evidence.DuplicateEvidenceAction.Merge);
        }
        IEnumerator assemblyEnumerator = evidence.GetAssemblyEnumerator();
        while (assemblyEnumerator.MoveNext())
        {
          EvidenceBase evidence1 = Evidence.WrapLegacyEvidence(assemblyEnumerator.Current);
          this.AddAssemblyEvidenceNoLock(evidence1, Evidence.GetEvidenceIndexType(evidence1), Evidence.DuplicateEvidenceAction.Merge);
        }
      }
    }

    internal void MergeWithNoDuplicates(Evidence evidence)
    {
      if (evidence == null)
        return;
      using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Writer))
      {
        IEnumerator hostEnumerator = evidence.GetHostEnumerator();
        while (hostEnumerator.MoveNext())
        {
          EvidenceBase evidence1 = Evidence.WrapLegacyEvidence(hostEnumerator.Current);
          this.AddHostEvidenceNoLock(evidence1, Evidence.GetEvidenceIndexType(evidence1), Evidence.DuplicateEvidenceAction.SelectNewObject);
        }
        IEnumerator assemblyEnumerator = evidence.GetAssemblyEnumerator();
        while (assemblyEnumerator.MoveNext())
        {
          EvidenceBase evidence1 = Evidence.WrapLegacyEvidence(assemblyEnumerator.Current);
          this.AddAssemblyEvidenceNoLock(evidence1, Evidence.GetEvidenceIndexType(evidence1), Evidence.DuplicateEvidenceAction.SelectNewObject);
        }
      }
    }

    [ComVisible(false)]
    [OnSerializing]
    [SecurityCritical]
    [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
    private void OnSerializing(StreamingContext context)
    {
      using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
      {
        foreach (Type type in new List<Type>((IEnumerable<Type>) this.m_evidence.Keys))
          this.GetHostEvidenceNoLock(type);
        this.DeserializeTargetEvidence();
      }
      ArrayList arrayList1 = new ArrayList();
      IEnumerator hostEnumerator = this.GetHostEnumerator();
      while (hostEnumerator.MoveNext())
        arrayList1.Add(hostEnumerator.Current);
      this.m_hostList = arrayList1;
      ArrayList arrayList2 = new ArrayList();
      IEnumerator assemblyEnumerator = this.GetAssemblyEnumerator();
      while (assemblyEnumerator.MoveNext())
        arrayList2.Add(assemblyEnumerator.Current);
      this.m_assemblyList = arrayList2;
    }

    [ComVisible(false)]
    [OnDeserialized]
    [SecurityCritical]
    private void OnDeserialized(StreamingContext context)
    {
      if (this.m_evidence == null)
      {
        this.m_evidence = new Dictionary<Type, EvidenceTypeDescriptor>();
        if (this.m_hostList != null)
        {
          foreach (object mHost in this.m_hostList)
          {
            if (mHost != null)
              this.AddHost(mHost);
          }
          this.m_hostList = (ArrayList) null;
        }
        if (this.m_assemblyList != null)
        {
          foreach (object mAssembly in this.m_assemblyList)
          {
            if (mAssembly != null)
              this.AddAssembly(mAssembly);
          }
          this.m_assemblyList = (ArrayList) null;
        }
      }
      this.m_evidenceLock = new ReaderWriterLock();
    }

    private void DeserializeTargetEvidence()
    {
      if (this.m_target == null || this.m_deserializedTargetEvidence)
        return;
      bool flag = false;
      LockCookie lockCookie = new LockCookie();
      try
      {
        if (!this.IsWriterLockHeld)
        {
          lockCookie = this.UpgradeToWriterLock();
          flag = true;
        }
        this.m_deserializedTargetEvidence = true;
        foreach (EvidenceBase evidence in this.m_target.GetFactorySuppliedEvidence())
          this.AddAssemblyEvidenceNoLock(evidence, Evidence.GetEvidenceIndexType(evidence), Evidence.DuplicateEvidenceAction.Throw);
      }
      finally
      {
        if (flag)
          this.DowngradeFromWriterLock(ref lockCookie);
      }
    }

    [SecurityCritical]
    internal byte[] RawSerialize()
    {
      try
      {
        using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
        {
          Dictionary<Type, EvidenceBase> dictionary = new Dictionary<Type, EvidenceBase>();
          foreach (KeyValuePair<Type, EvidenceTypeDescriptor> keyValuePair in this.m_evidence)
          {
            if (keyValuePair.Value != null && keyValuePair.Value.HostEvidence != null)
              dictionary[keyValuePair.Key] = keyValuePair.Value.HostEvidence;
          }
          using (MemoryStream memoryStream = new MemoryStream())
          {
            new BinaryFormatter().Serialize((Stream) memoryStream, (object) dictionary);
            return memoryStream.ToArray();
          }
        }
      }
      catch (SecurityException ex)
      {
        return (byte[]) null;
      }
    }

    /// <summary>将证据对象复制到 <see cref="T:System.Array" />。</summary>
    /// <param name="array">要向其复制证据对象的目标数组。</param>
    /// <param name="index">数组中从零开始的位置，从该位置开始向其复制证据对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="index " />在目标数组的范围之外<paramref name="." /></exception>
    [Obsolete("Evidence should not be treated as an ICollection. Please use the GetHostEnumerator and GetAssemblyEnumerator methods rather than using CopyTo.")]
    public void CopyTo(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (index < 0 || index > array.Length - this.Count)
        throw new ArgumentOutOfRangeException("index");
      int index1 = index;
      IEnumerator hostEnumerator = this.GetHostEnumerator();
      while (hostEnumerator.MoveNext())
      {
        array.SetValue(hostEnumerator.Current, index1);
        ++index1;
      }
      IEnumerator assemblyEnumerator = this.GetAssemblyEnumerator();
      while (assemblyEnumerator.MoveNext())
      {
        array.SetValue(assemblyEnumerator.Current, index1);
        ++index1;
      }
    }

    /// <summary>枚举由主机提供的证据。</summary>
    /// <returns>
    /// <see cref="M:System.Security.Policy.Evidence.AddHost(System.Object)" /> 方法添加的证据的枚举数。</returns>
    public IEnumerator GetHostEnumerator()
    {
      using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
        return (IEnumerator) new Evidence.EvidenceEnumerator(this, Evidence.EvidenceEnumerator.Category.Host);
    }

    /// <summary>枚举程序集提供的证据。</summary>
    /// <returns>
    /// <see cref="M:System.Security.Policy.Evidence.AddAssembly(System.Object)" /> 方法添加的证据的枚举数。</returns>
    public IEnumerator GetAssemblyEnumerator()
    {
      using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
      {
        this.DeserializeTargetEvidence();
        return (IEnumerator) new Evidence.EvidenceEnumerator(this, Evidence.EvidenceEnumerator.Category.Assembly);
      }
    }

    internal Evidence.RawEvidenceEnumerator GetRawAssemblyEvidenceEnumerator()
    {
      this.DeserializeTargetEvidence();
      return new Evidence.RawEvidenceEnumerator(this, (IEnumerable<Type>) new List<Type>((IEnumerable<Type>) this.m_evidence.Keys), false);
    }

    internal Evidence.RawEvidenceEnumerator GetRawHostEvidenceEnumerator()
    {
      return new Evidence.RawEvidenceEnumerator(this, (IEnumerable<Type>) new List<Type>((IEnumerable<Type>) this.m_evidence.Keys), true);
    }

    /// <summary>枚举集合中的所有证据，包括由主机提供的证据和由程序集提供的证据。</summary>
    /// <returns>由 <see cref="M:System.Security.Policy.Evidence.AddHost(System.Object)" /> 方法和 <see cref="M:System.Security.Policy.Evidence.AddAssembly(System.Object)" /> 方法添加的证据的枚举数。</returns>
    [Obsolete("GetEnumerator is obsolete. Please use GetAssemblyEnumerator and GetHostEnumerator instead.")]
    public IEnumerator GetEnumerator()
    {
      using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
        return (IEnumerator) new Evidence.EvidenceEnumerator(this, Evidence.EvidenceEnumerator.Category.Host | Evidence.EvidenceEnumerator.Category.Assembly);
    }

    /// <summary>从集合中获取指定类型的程序集证据。</summary>
    /// <returns>程序集证据集合中 <paramref name="T" /> 类型的证据。</returns>
    /// <typeparam name="T">要获取的证据的类型。</typeparam>
    [ComVisible(false)]
    public T GetAssemblyEvidence<T>() where T : EvidenceBase
    {
      return Evidence.UnwrapEvidence(this.GetAssemblyEvidence(typeof (T))) as T;
    }

    internal EvidenceBase GetAssemblyEvidence(Type type)
    {
      using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
        return this.GetAssemblyEvidenceNoLock(type);
    }

    private EvidenceBase GetAssemblyEvidenceNoLock(Type type)
    {
      this.DeserializeTargetEvidence();
      EvidenceTypeDescriptor evidenceTypeDescriptor = this.GetEvidenceTypeDescriptor(type);
      if (evidenceTypeDescriptor != null)
        return evidenceTypeDescriptor.AssemblyEvidence;
      return (EvidenceBase) null;
    }

    /// <summary>从集合中获取指定类型的主机证据。</summary>
    /// <returns>主机证据集合中 <paramref name="T" /> 类型的证据。</returns>
    /// <typeparam name="T">要获取的证据的类型。</typeparam>
    [ComVisible(false)]
    public T GetHostEvidence<T>() where T : EvidenceBase
    {
      return Evidence.UnwrapEvidence(this.GetHostEvidence(typeof (T))) as T;
    }

    internal T GetDelayEvaluatedHostEvidence<T>() where T : EvidenceBase, IDelayEvaluatedEvidence
    {
      return Evidence.UnwrapEvidence(this.GetHostEvidence(typeof (T), false)) as T;
    }

    internal EvidenceBase GetHostEvidence(Type type)
    {
      return this.GetHostEvidence(type, true);
    }

    [SecuritySafeCritical]
    private EvidenceBase GetHostEvidence(Type type, bool markDelayEvaluatedEvidenceUsed)
    {
      using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
      {
        EvidenceBase hostEvidenceNoLock = this.GetHostEvidenceNoLock(type);
        if (markDelayEvaluatedEvidenceUsed)
        {
          IDelayEvaluatedEvidence evaluatedEvidence = hostEvidenceNoLock as IDelayEvaluatedEvidence;
          if (evaluatedEvidence != null)
            evaluatedEvidence.MarkUsed();
        }
        return hostEvidenceNoLock;
      }
    }

    [SecurityCritical]
    private EvidenceBase GetHostEvidenceNoLock(Type type)
    {
      EvidenceTypeDescriptor evidenceTypeDescriptor1 = this.GetEvidenceTypeDescriptor(type);
      if (evidenceTypeDescriptor1 == null)
        return (EvidenceBase) null;
      if (evidenceTypeDescriptor1.HostEvidence != null)
        return evidenceTypeDescriptor1.HostEvidence;
      if (this.m_target == null || evidenceTypeDescriptor1.Generated)
        return (EvidenceBase) null;
      using (new Evidence.EvidenceUpgradeLockHolder(this))
      {
        evidenceTypeDescriptor1.Generated = true;
        EvidenceBase hostEvidence = this.GenerateHostEvidence(type, evidenceTypeDescriptor1.HostCanGenerate);
        if (hostEvidence != null)
        {
          evidenceTypeDescriptor1.HostEvidence = hostEvidence;
          Evidence target = this.m_cloneOrigin != null ? this.m_cloneOrigin.Target as Evidence : (Evidence) null;
          if (target != null)
          {
            using (new Evidence.EvidenceLockHolder(target, Evidence.EvidenceLockHolder.LockType.Writer))
            {
              EvidenceTypeDescriptor evidenceTypeDescriptor2 = target.GetEvidenceTypeDescriptor(type);
              if (evidenceTypeDescriptor2 != null)
              {
                if (evidenceTypeDescriptor2.HostEvidence == null)
                  evidenceTypeDescriptor2.HostEvidence = hostEvidence.Clone();
              }
            }
          }
        }
        return hostEvidence;
      }
    }

    [SecurityCritical]
    private EvidenceBase GenerateHostEvidence(Type type, bool hostCanGenerate)
    {
      if (hostCanGenerate)
      {
        AppDomain appDomain = this.m_target.Target as AppDomain;
        Assembly assembly = this.m_target.Target as Assembly;
        EvidenceBase evidenceBase = (EvidenceBase) null;
        if (appDomain != null)
          evidenceBase = AppDomain.CurrentDomain.HostSecurityManager.GenerateAppDomainEvidence(type);
        else if (assembly != (Assembly) null)
          evidenceBase = AppDomain.CurrentDomain.HostSecurityManager.GenerateAssemblyEvidence(type, assembly);
        if (evidenceBase != null)
        {
          if (!type.IsAssignableFrom(evidenceBase.GetType()))
            throw new InvalidOperationException(Environment.GetResourceString("Policy_IncorrectHostEvidence", (object) AppDomain.CurrentDomain.HostSecurityManager.GetType().FullName, (object) evidenceBase.GetType().FullName, (object) type.FullName));
          return evidenceBase;
        }
      }
      return this.m_target.GenerateEvidence(type);
    }

    /// <summary>返回此证据对象的重复副本。</summary>
    /// <returns>此证据对象的重复副本。</returns>
    [ComVisible(false)]
    public Evidence Clone()
    {
      return new Evidence(this);
    }

    /// <summary>从证据集中移除主机和程序集证据。</summary>
    [ComVisible(false)]
    [SecuritySafeCritical]
    public void Clear()
    {
      if (this.Locked)
        new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
      using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Writer))
      {
        this.m_version = this.m_version + 1U;
        this.m_evidence.Clear();
      }
    }

    /// <summary>从主机和程序集枚举中移除给定类型的证据。</summary>
    /// <param name="t">要移除的证据的类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="t" /> 为 null。</exception>
    [ComVisible(false)]
    [SecuritySafeCritical]
    public void RemoveType(Type t)
    {
      if (t == (Type) null)
        throw new ArgumentNullException("t");
      using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Writer))
      {
        EvidenceTypeDescriptor evidenceTypeDescriptor = this.GetEvidenceTypeDescriptor(t);
        if (evidenceTypeDescriptor == null)
          return;
        this.m_version = this.m_version + 1U;
        if (this.Locked && (evidenceTypeDescriptor.HostEvidence != null || evidenceTypeDescriptor.HostCanGenerate))
          new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
        this.m_evidence.Remove(t);
      }
    }

    internal void MarkAllEvidenceAsUsed()
    {
      using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
      {
        foreach (KeyValuePair<Type, EvidenceTypeDescriptor> keyValuePair in this.m_evidence)
        {
          if (keyValuePair.Value != null)
          {
            IDelayEvaluatedEvidence evaluatedEvidence1 = keyValuePair.Value.HostEvidence as IDelayEvaluatedEvidence;
            if (evaluatedEvidence1 != null)
              evaluatedEvidence1.MarkUsed();
            IDelayEvaluatedEvidence evaluatedEvidence2 = keyValuePair.Value.AssemblyEvidence as IDelayEvaluatedEvidence;
            if (evaluatedEvidence2 != null)
              evaluatedEvidence2.MarkUsed();
          }
        }
      }
    }

    private bool WasStrongNameEvidenceUsed()
    {
      using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
      {
        EvidenceTypeDescriptor evidenceTypeDescriptor = this.GetEvidenceTypeDescriptor(typeof (StrongName));
        if (evidenceTypeDescriptor == null)
          return false;
        IDelayEvaluatedEvidence evaluatedEvidence = evidenceTypeDescriptor.HostEvidence as IDelayEvaluatedEvidence;
        return evaluatedEvidence != null && evaluatedEvidence.WasUsed;
      }
    }

    private enum DuplicateEvidenceAction
    {
      Throw,
      Merge,
      SelectNewObject,
    }

    private class EvidenceLockHolder : IDisposable
    {
      private Evidence m_target;
      private Evidence.EvidenceLockHolder.LockType m_lockType;

      public EvidenceLockHolder(Evidence target, Evidence.EvidenceLockHolder.LockType lockType)
      {
        this.m_target = target;
        this.m_lockType = lockType;
        if (this.m_lockType == Evidence.EvidenceLockHolder.LockType.Reader)
          this.m_target.AcquireReaderLock();
        else
          this.m_target.AcquireWriterlock();
      }

      public void Dispose()
      {
        if (this.m_lockType == Evidence.EvidenceLockHolder.LockType.Reader && this.m_target.IsReaderLockHeld)
        {
          this.m_target.ReleaseReaderLock();
        }
        else
        {
          if (this.m_lockType != Evidence.EvidenceLockHolder.LockType.Writer || !this.m_target.IsWriterLockHeld)
            return;
          this.m_target.ReleaseWriterLock();
        }
      }

      public enum LockType
      {
        Reader,
        Writer,
      }
    }

    private class EvidenceUpgradeLockHolder : IDisposable
    {
      private Evidence m_target;
      private LockCookie m_cookie;

      public EvidenceUpgradeLockHolder(Evidence target)
      {
        this.m_target = target;
        this.m_cookie = this.m_target.UpgradeToWriterLock();
      }

      public void Dispose()
      {
        if (!this.m_target.IsWriterLockHeld)
          return;
        this.m_target.DowngradeFromWriterLock(ref this.m_cookie);
      }
    }

    internal sealed class RawEvidenceEnumerator : IEnumerator<EvidenceBase>, IDisposable, IEnumerator
    {
      private Evidence m_evidence;
      private bool m_hostEnumerator;
      private uint m_evidenceVersion;
      private Type[] m_evidenceTypes;
      private int m_typeIndex;
      private EvidenceBase m_currentEvidence;
      private static volatile List<Type> s_expensiveEvidence;

      public EvidenceBase Current
      {
        get
        {
          if ((int) this.m_evidence.m_version != (int) this.m_evidenceVersion)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
          return this.m_currentEvidence;
        }
      }

      object IEnumerator.Current
      {
        get
        {
          if ((int) this.m_evidence.m_version != (int) this.m_evidenceVersion)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
          return (object) this.m_currentEvidence;
        }
      }

      private static List<Type> ExpensiveEvidence
      {
        get
        {
          if (Evidence.RawEvidenceEnumerator.s_expensiveEvidence == null)
            Evidence.RawEvidenceEnumerator.s_expensiveEvidence = new List<Type>()
            {
              typeof (Hash),
              typeof (Publisher)
            };
          return Evidence.RawEvidenceEnumerator.s_expensiveEvidence;
        }
      }

      public RawEvidenceEnumerator(Evidence evidence, IEnumerable<Type> evidenceTypes, bool hostEnumerator)
      {
        this.m_evidence = evidence;
        this.m_hostEnumerator = hostEnumerator;
        this.m_evidenceTypes = Evidence.RawEvidenceEnumerator.GenerateEvidenceTypes(evidence, evidenceTypes, hostEnumerator);
        this.m_evidenceVersion = evidence.m_version;
        this.Reset();
      }

      public void Dispose()
      {
      }

      private static Type[] GenerateEvidenceTypes(Evidence evidence, IEnumerable<Type> evidenceTypes, bool hostEvidence)
      {
        List<Type> typeList1 = new List<Type>();
        List<Type> typeList2 = new List<Type>();
        List<Type> typeList3 = new List<Type>(Evidence.RawEvidenceEnumerator.ExpensiveEvidence.Count);
        foreach (Type evidenceType in evidenceTypes)
        {
          EvidenceTypeDescriptor evidenceTypeDescriptor = evidence.GetEvidenceTypeDescriptor(evidenceType);
          if ((!hostEvidence || evidenceTypeDescriptor.HostEvidence == null ? (hostEvidence ? 0 : (evidenceTypeDescriptor.AssemblyEvidence != null ? 1 : 0)) : 1) != 0)
            typeList1.Add(evidenceType);
          else if (Evidence.RawEvidenceEnumerator.ExpensiveEvidence.Contains(evidenceType))
            typeList3.Add(evidenceType);
          else
            typeList2.Add(evidenceType);
        }
        Type[] array = new Type[typeList1.Count + typeList2.Count + typeList3.Count];
        typeList1.CopyTo(array, 0);
        typeList2.CopyTo(array, typeList1.Count);
        typeList3.CopyTo(array, typeList1.Count + typeList2.Count);
        return array;
      }

      [SecuritySafeCritical]
      public bool MoveNext()
      {
        using (new Evidence.EvidenceLockHolder(this.m_evidence, Evidence.EvidenceLockHolder.LockType.Reader))
        {
          if ((int) this.m_evidence.m_version != (int) this.m_evidenceVersion)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
          this.m_currentEvidence = (EvidenceBase) null;
          do
          {
            this.m_typeIndex = this.m_typeIndex + 1;
            if (this.m_typeIndex < this.m_evidenceTypes.Length)
              this.m_currentEvidence = !this.m_hostEnumerator ? this.m_evidence.GetAssemblyEvidenceNoLock(this.m_evidenceTypes[this.m_typeIndex]) : this.m_evidence.GetHostEvidenceNoLock(this.m_evidenceTypes[this.m_typeIndex]);
            if (this.m_typeIndex >= this.m_evidenceTypes.Length)
              break;
          }
          while (this.m_currentEvidence == null);
        }
        return this.m_currentEvidence != null;
      }

      public void Reset()
      {
        if ((int) this.m_evidence.m_version != (int) this.m_evidenceVersion)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
        this.m_typeIndex = -1;
        this.m_currentEvidence = (EvidenceBase) null;
      }
    }

    private sealed class EvidenceEnumerator : IEnumerator
    {
      private Evidence m_evidence;
      private Evidence.EvidenceEnumerator.Category m_category;
      private Stack m_enumerators;
      private object m_currentEvidence;

      public object Current
      {
        get
        {
          return this.m_currentEvidence;
        }
      }

      private IEnumerator CurrentEnumerator
      {
        get
        {
          if (this.m_enumerators.Count <= 0)
            return (IEnumerator) null;
          return this.m_enumerators.Peek() as IEnumerator;
        }
      }

      internal EvidenceEnumerator(Evidence evidence, Evidence.EvidenceEnumerator.Category category)
      {
        this.m_evidence = evidence;
        this.m_category = category;
        this.ResetNoLock();
      }

      public bool MoveNext()
      {
        IEnumerator currentEnumerator = this.CurrentEnumerator;
        if (currentEnumerator == null)
        {
          this.m_currentEvidence = (object) null;
          return false;
        }
        if (currentEnumerator.MoveNext())
        {
          LegacyEvidenceWrapper legacyEvidenceWrapper = currentEnumerator.Current as LegacyEvidenceWrapper;
          LegacyEvidenceList legacyEvidenceList = currentEnumerator.Current as LegacyEvidenceList;
          if (legacyEvidenceWrapper != null)
            this.m_currentEvidence = legacyEvidenceWrapper.EvidenceObject;
          else if (legacyEvidenceList != null)
          {
            this.m_enumerators.Push((object) legacyEvidenceList.GetEnumerator());
            this.MoveNext();
          }
          else
            this.m_currentEvidence = currentEnumerator.Current;
          return true;
        }
        this.m_enumerators.Pop();
        return this.MoveNext();
      }

      public void Reset()
      {
        using (new Evidence.EvidenceLockHolder(this.m_evidence, Evidence.EvidenceLockHolder.LockType.Reader))
          this.ResetNoLock();
      }

      private void ResetNoLock()
      {
        this.m_currentEvidence = (object) null;
        this.m_enumerators = new Stack();
        if ((this.m_category & Evidence.EvidenceEnumerator.Category.Host) == Evidence.EvidenceEnumerator.Category.Host)
          this.m_enumerators.Push((object) this.m_evidence.GetRawHostEvidenceEnumerator());
        if ((this.m_category & Evidence.EvidenceEnumerator.Category.Assembly) != Evidence.EvidenceEnumerator.Category.Assembly)
          return;
        this.m_enumerators.Push((object) this.m_evidence.GetRawAssemblyEvidenceEnumerator());
      }

      [Flags]
      internal enum Category
      {
        Host = 1,
        Assembly = 2,
      }
    }
  }
}
