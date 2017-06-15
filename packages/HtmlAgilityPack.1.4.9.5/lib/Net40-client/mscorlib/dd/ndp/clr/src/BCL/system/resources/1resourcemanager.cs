// Decompiled with JetBrains decompiler
// Type: System.Resources.ResourceManager
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Resources
{
  /// <summary>表示资源管理器，其可在运行时提供对于特定文化资源的便利访问安全说明：在此类不受信任的数据中调用方法存在安全风险。仅在受信任的数据类中调用方法。有关详细信息，请参阅 Untrusted Data Security Risks。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class ResourceManager
  {
    /// <summary>保留用于标识资源文件的数字。</summary>
    public static readonly int MagicNumber = -1091581234;
    /// <summary> 指定 <see cref="T:System.Resources.ResourceManager" /> 的当前实现可解释和产生的资源文件头的版本。</summary>
    public static readonly int HeaderVersionNumber = 1;
    private static readonly Type _minResourceSet = typeof (ResourceSet);
    internal static readonly string ResReaderTypeName = typeof (ResourceReader).FullName;
    internal static readonly string ResSetTypeName = typeof (RuntimeResourceSet).FullName;
    internal static readonly string MscorlibName = typeof (ResourceReader).Assembly.FullName;
    internal static readonly int DEBUG = 0;
    /// <summary>指定 <see cref="T:System.Resources.ResourceManager" /> 在其中搜索资源的资源文件的根名称。</summary>
    protected string BaseNameField;
    /// <summary>包含 <see cref="T:System.Collections.Hashtable" />，它返回从区域性到 <see cref="T:System.Resources.ResourceSet" /> 对象的映射。</summary>
    [Obsolete("call InternalGetResourceSet instead")]
    protected Hashtable ResourceSets;
    [NonSerialized]
    private Dictionary<string, ResourceSet> _resourceSets;
    private string moduleDir;
    /// <summary>指定包含资源的主要程序集。</summary>
    protected Assembly MainAssembly;
    private Type _locationInfo;
    private Type _userResourceSet;
    private CultureInfo _neutralResourcesCulture;
    [NonSerialized]
    private ResourceManager.CultureNameResourceSetPair _lastUsedResourceCache;
    private bool _ignoreCase;
    private bool UseManifest;
    [OptionalField(VersionAdded = 1)]
    private bool UseSatelliteAssem;
    private static volatile Hashtable _installedSatelliteInfo;
    private static volatile bool _checkedConfigFile;
    [OptionalField]
    private UltimateResourceFallbackLocation _fallbackLoc;
    [OptionalField]
    private Version _satelliteContractVersion;
    [OptionalField]
    private bool _lookedForSatelliteContractVersion;
    [OptionalField(VersionAdded = 1)]
    private Assembly _callingAssembly;
    [OptionalField(VersionAdded = 4)]
    private RuntimeAssembly m_callingAssembly;
    [NonSerialized]
    private IResourceGroveler resourceGroveler;
    internal const string ResFileExtension = ".resources";
    internal const int ResFileExtensionLength = 10;
    private static volatile bool s_IsAppXModel;
    [NonSerialized]
    private bool _bUsingModernResourceManagement;
    [SecurityCritical]
    [NonSerialized]
    private WindowsRuntimeResourceManagerBase _WinRTResourceManager;
    [NonSerialized]
    private bool _PRIonAppXInitialized;
    [NonSerialized]
    private PRIExceptionInfo _PRIExceptionInfo;

    /// <summary>获取 <see cref="T:System.Resources.ResourceManager" /> 从其中搜索资源的资源文件的根名称。</summary>
    /// <returns>
    /// <see cref="T:System.Resources.ResourceManager" /> 从其中搜索资源的资源文件的根名称。</returns>
    public virtual string BaseName
    {
      get
      {
        return this.BaseNameField;
      }
    }

    /// <summary>获取或设置值，该值指示资源管理器是否允许在 <see cref="M:System.Resources.ResourceManager.GetString(System.String)" /> 和 <see cref="M:System.Resources.ResourceManager.GetObject(System.String)" /> 方法中进行不区分大小写的资源查找。</summary>
    /// <returns>要在资源查找过程中忽略大小写，则为 true；否则为 false。</returns>
    public virtual bool IgnoreCase
    {
      get
      {
        return this._ignoreCase;
      }
      set
      {
        this._ignoreCase = value;
      }
    }

    /// <summary>获取资源管理器使用构造 <see cref="T:System.Resources.ResourceSet" /> 对象的资源设置对象的类型。</summary>
    /// <returns>使用构造对象的资源管理器 <see cref="T:System.Resources.ResourceSet" /> 的设置的对象的资源类型。</returns>
    public virtual Type ResourceSetType
    {
      get
      {
        if (!(this._userResourceSet == (Type) null))
          return this._userResourceSet;
        return typeof (RuntimeResourceSet);
      }
    }

    /// <summary>获取或设置检索默认回退资源的位置。</summary>
    /// <returns>指定资源管理器能查找回退资源的位置的某个枚举值。</returns>
    protected UltimateResourceFallbackLocation FallbackLocation
    {
      get
      {
        return this._fallbackLoc;
      }
      set
      {
        this._fallbackLoc = value;
      }
    }

    /// <summary>使用默认值初始化 <see cref="T:System.Resources.ResourceManager" /> 类的新实例。</summary>
    protected ResourceManager()
    {
      this.Init();
      this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
      this.resourceGroveler = (IResourceGroveler) new ManifestBasedResourceGroveler(new ResourceManager.ResourceManagerMediator(this));
    }

    private ResourceManager(string baseName, string resourceDir, Type usingResourceSet)
    {
      if (baseName == null)
        throw new ArgumentNullException("baseName");
      if (resourceDir == null)
        throw new ArgumentNullException("resourceDir");
      this.BaseNameField = baseName;
      this.moduleDir = resourceDir;
      this._userResourceSet = usingResourceSet;
      this.ResourceSets = new Hashtable();
      this._resourceSets = new Dictionary<string, ResourceSet>();
      this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
      this.UseManifest = false;
      this.resourceGroveler = (IResourceGroveler) new FileBasedResourceGroveler(new ResourceManager.ResourceManagerMediator(this));
      if (!FrameworkEventSource.IsInitialized || !FrameworkEventSource.Log.IsEnabled())
        return;
      CultureInfo invariantCulture = CultureInfo.InvariantCulture;
      string resourceFileName = this.GetResourceFileName(invariantCulture);
      if (this.resourceGroveler.HasNeutralResources(invariantCulture, resourceFileName))
        FrameworkEventSource.Log.ResourceManagerNeutralResourcesFound(this.BaseNameField, this.MainAssembly, resourceFileName);
      else
        FrameworkEventSource.Log.ResourceManagerNeutralResourcesNotFound(this.BaseNameField, this.MainAssembly, resourceFileName);
    }

    /// <summary>初始化 <see cref="T:System.Resources.ResourceManager" /> 类的新实例，该实例在给定的程序集中查找从指定根名称导出的文件中包含的资源。</summary>
    /// <param name="baseName">资源文件的根名称，没有其扩展名但是包含所有完全限定的命名空间名称。例如，名为 MyApplication.MyResource.en-US.resources 的资源文件的根名称为 MyApplication.MyResource。</param>
    /// <param name="assembly">资源的主程序集。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="baseName" /> 或 <paramref name="assembly" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public ResourceManager(string baseName, Assembly assembly)
    {
      if (baseName == null)
        throw new ArgumentNullException("baseName");
      if ((Assembly) null == assembly)
        throw new ArgumentNullException("assembly");
      if (!(assembly is RuntimeAssembly))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
      this.MainAssembly = assembly;
      this.BaseNameField = baseName;
      this.SetAppXConfiguration();
      this.CommonAssemblyInit();
      this.m_callingAssembly = (RuntimeAssembly) Assembly.GetCallingAssembly();
      if (!(assembly == typeof (object).Assembly) || !((Assembly) this.m_callingAssembly != assembly))
        return;
      this.m_callingAssembly = (RuntimeAssembly) null;
    }

    /// <summary>初始化使用指定 <see cref="T:System.Resources.ResourceSet" /> 的 <see cref="T:System.Resources.ResourceManager" /> 类的新实例，该实例在给定的程序集中的指定根名称类的文件中查找资源。</summary>
    /// <param name="baseName">资源文件的根名称，没有其扩展名但是包含所有完全限定的命名空间名称。例如，名为 MyApplication.MyResource.en-US.resources 的资源文件的根名称为 MyApplication.MyResource。</param>
    /// <param name="assembly">资源的主程序集。</param>
    /// <param name="usingResourceSet">要使用的自定义 <see cref="T:System.Resources.ResourceSet" /> 的类型。如果为 null，则使用默认的运行时 <see cref="T:System.Resources.ResourceSet" /> 对象。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="usingResourceset" /> 不是 <see cref="T:System.Resources.ResourceSet" /> 的派生类。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="baseName" /> 或 <paramref name="assembly" /> 参数为 null。</exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public ResourceManager(string baseName, Assembly assembly, Type usingResourceSet)
    {
      if (baseName == null)
        throw new ArgumentNullException("baseName");
      if ((Assembly) null == assembly)
        throw new ArgumentNullException("assembly");
      if (!(assembly is RuntimeAssembly))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
      this.MainAssembly = assembly;
      this.BaseNameField = baseName;
      if (usingResourceSet != (Type) null && usingResourceSet != ResourceManager._minResourceSet && !usingResourceSet.IsSubclassOf(ResourceManager._minResourceSet))
        throw new ArgumentException(Environment.GetResourceString("Arg_ResMgrNotResSet"), "usingResourceSet");
      this._userResourceSet = usingResourceSet;
      this.CommonAssemblyInit();
      this.m_callingAssembly = (RuntimeAssembly) Assembly.GetCallingAssembly();
      if (!(assembly == typeof (object).Assembly) || !((Assembly) this.m_callingAssembly != assembly))
        return;
      this.m_callingAssembly = (RuntimeAssembly) null;
    }

    /// <summary>它根据指定的对象中的信息在附属程序集内查找资源来初始化 <see cref="T:System.Resources.ResourceManager" /> 类的新实例。</summary>
    /// <param name="resourceSource">一个类型，从资源管理器中派生所有用于查找 .resources 文件的信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="resourceSource" /> 参数为 null。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public ResourceManager(Type resourceSource)
    {
      if ((Type) null == resourceSource)
        throw new ArgumentNullException("resourceSource");
      if (!(resourceSource is RuntimeType))
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
      this._locationInfo = resourceSource;
      this.MainAssembly = this._locationInfo.Assembly;
      this.BaseNameField = resourceSource.Name;
      this.SetAppXConfiguration();
      this.CommonAssemblyInit();
      this.m_callingAssembly = (RuntimeAssembly) Assembly.GetCallingAssembly();
      if (!(this.MainAssembly == typeof (object).Assembly) || !((Assembly) this.m_callingAssembly != this.MainAssembly))
        return;
      this.m_callingAssembly = (RuntimeAssembly) null;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void Init()
    {
      this.m_callingAssembly = (RuntimeAssembly) Assembly.GetCallingAssembly();
    }

    [OnDeserializing]
    private void OnDeserializing(StreamingContext ctx)
    {
      this._resourceSets = (Dictionary<string, ResourceSet>) null;
      this.resourceGroveler = (IResourceGroveler) null;
      this._lastUsedResourceCache = (ResourceManager.CultureNameResourceSetPair) null;
    }

    [SecuritySafeCritical]
    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      this._resourceSets = new Dictionary<string, ResourceSet>();
      this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
      ResourceManager.ResourceManagerMediator mediator = new ResourceManager.ResourceManagerMediator(this);
      this.resourceGroveler = !this.UseManifest ? (IResourceGroveler) new FileBasedResourceGroveler(mediator) : (IResourceGroveler) new ManifestBasedResourceGroveler(mediator);
      if ((Assembly) this.m_callingAssembly == (Assembly) null)
        this.m_callingAssembly = (RuntimeAssembly) this._callingAssembly;
      if (!this.UseManifest || this._neutralResourcesCulture != null)
        return;
      this._neutralResourcesCulture = ManifestBasedResourceGroveler.GetNeutralResourcesLanguage(this.MainAssembly, ref this._fallbackLoc);
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      this._callingAssembly = (Assembly) this.m_callingAssembly;
      this.UseSatelliteAssem = this.UseManifest;
      this.ResourceSets = new Hashtable();
    }

    [SecuritySafeCritical]
    private void CommonAssemblyInit()
    {
      if (!this._bUsingModernResourceManagement)
      {
        this.UseManifest = true;
        this._resourceSets = new Dictionary<string, ResourceSet>();
        this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
        this._fallbackLoc = UltimateResourceFallbackLocation.MainAssembly;
        this.resourceGroveler = (IResourceGroveler) new ManifestBasedResourceGroveler(new ResourceManager.ResourceManagerMediator(this));
      }
      this._neutralResourcesCulture = ManifestBasedResourceGroveler.GetNeutralResourcesLanguage(this.MainAssembly, ref this._fallbackLoc);
      if (this._bUsingModernResourceManagement)
        return;
      if (FrameworkEventSource.IsInitialized && FrameworkEventSource.Log.IsEnabled())
      {
        CultureInfo invariantCulture = CultureInfo.InvariantCulture;
        string resourceFileName = this.GetResourceFileName(invariantCulture);
        if (this.resourceGroveler.HasNeutralResources(invariantCulture, resourceFileName))
        {
          FrameworkEventSource.Log.ResourceManagerNeutralResourcesFound(this.BaseNameField, this.MainAssembly, resourceFileName);
        }
        else
        {
          string resName = resourceFileName;
          if (this._locationInfo != (Type) null && this._locationInfo.Namespace != null)
            resName = this._locationInfo.Namespace + Type.Delimiter.ToString() + resourceFileName;
          FrameworkEventSource.Log.ResourceManagerNeutralResourcesNotFound(this.BaseNameField, this.MainAssembly, resName);
        }
      }
      this.ResourceSets = new Hashtable();
    }

    /// <summary>告知资源管理对所有 <see cref="T:System.Resources.ResourceSet" /> 对象调用方法 <see cref="M:System.Resources.ResourceSet.Close" />，并释放所有资源。</summary>
    public virtual void ReleaseAllResources()
    {
      if (FrameworkEventSource.IsInitialized)
        FrameworkEventSource.Log.ResourceManagerReleasingResources(this.BaseNameField, this.MainAssembly);
      Dictionary<string, ResourceSet> dictionary = this._resourceSets;
      this._resourceSets = new Dictionary<string, ResourceSet>();
      this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
      lock (dictionary)
      {
        IDictionaryEnumerator local_3 = (IDictionaryEnumerator) dictionary.GetEnumerator();
        IDictionaryEnumerator local_4 = (IDictionaryEnumerator) null;
        if (this.ResourceSets != null)
          local_4 = this.ResourceSets.GetEnumerator();
        this.ResourceSets = new Hashtable();
        while (local_3.MoveNext())
          ((ResourceSet) local_3.Value).Close();
        if (local_4 == null)
          return;
        while (local_4.MoveNext())
          ((ResourceSet) local_4.Value).Close();
      }
    }

    /// <summary>返回一个 <see cref="T:System.Resources.ResourceManager" /> 对象，它在特定的目录中而不在资源的程序集清单。</summary>
    /// <returns>搜索指定目录而不是资源的程序集清单的资源管理器的新实例。</returns>
    /// <param name="baseName">资源的根名称。例如，名为“MyResource.en-US.resources”的资源文件的根名称为“MyResource”。</param>
    /// <param name="resourceDir">要在其中搜索资源的目录的名称。<paramref name="resourceDir" /> 可以是绝对路径或应用程序目录中的相对路径。</param>
    /// <param name="usingResourceSet">要使用的自定义 <see cref="T:System.Resources.ResourceSet" /> 的类型。如果为 null，则使用默认的运行时 <see cref="T:System.Resources.ResourceSet" /> 对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="baseName" /> 或 <paramref name="resourceDir" /> 参数为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public static ResourceManager CreateFileBasedResourceManager(string baseName, string resourceDir, Type usingResourceSet)
    {
      return new ResourceManager(baseName, resourceDir, usingResourceSet);
    }

    /// <summary>生成给定的 <see cref="T:System.Globalization.CultureInfo" /> 对象的资源文件的名称。</summary>
    /// <returns>可用于给定的 <see cref="T:System.Globalization.CultureInfo" /> 对象的资源文件的名称。</returns>
    /// <param name="culture">构造资源文件名的区域性对象。</param>
    protected virtual string GetResourceFileName(CultureInfo culture)
    {
      StringBuilder stringBuilder = new StringBuilder((int) byte.MaxValue);
      stringBuilder.Append(this.BaseNameField);
      if (!culture.HasInvariantCultureName)
      {
        CultureInfo.VerifyCultureName(culture.Name, true);
        stringBuilder.Append('.');
        stringBuilder.Append(culture.Name);
      }
      stringBuilder.Append(".resources");
      return stringBuilder.ToString();
    }

    internal ResourceSet GetFirstResourceSet(CultureInfo culture)
    {
      if (this._neutralResourcesCulture != null && culture.Name == this._neutralResourcesCulture.Name)
        culture = CultureInfo.InvariantCulture;
      if (this._lastUsedResourceCache != null)
      {
        lock (this._lastUsedResourceCache)
        {
          if (culture.Name == this._lastUsedResourceCache.lastCultureName)
            return this._lastUsedResourceCache.lastResourceSet;
        }
      }
      Dictionary<string, ResourceSet> dictionary = this._resourceSets;
      ResourceSet resourceSet = (ResourceSet) null;
      if (dictionary != null)
      {
        lock (dictionary)
          dictionary.TryGetValue(culture.Name, out resourceSet);
      }
      if (resourceSet == null)
        return (ResourceSet) null;
      if (this._lastUsedResourceCache != null)
      {
        lock (this._lastUsedResourceCache)
        {
          this._lastUsedResourceCache.lastCultureName = culture.Name;
          this._lastUsedResourceCache.lastResourceSet = resourceSet;
        }
      }
      return resourceSet;
    }

    /// <summary>检索特定区域性的资源集合。</summary>
    /// <returns>指定区域性的资源集。</returns>
    /// <param name="culture">将要检索资源的区域性。</param>
    /// <param name="createIfNotExists">如果尚未加载，true 要加载资源集；否则为 false。</param>
    /// <param name="tryParents">true 表示使用资源回退加载相应资源（如果找不到资源）；false 表示绕过资源回退进程。（请参见“备注”部分。）</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="culture" /> 参数为 null。</exception>
    /// <exception cref="T:System.Resources.MissingManifestResourceException">
    /// <paramref name="tryParents" /> 为 true，未找到可用的资源集，并且没有默认区域性的资源。</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public virtual ResourceSet GetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
    {
      if (culture == null)
        throw new ArgumentNullException("culture");
      Dictionary<string, ResourceSet> localResourceSets = this._resourceSets;
      if (localResourceSets != null)
      {
        lock (localResourceSets)
        {
          ResourceSet local_1;
          if (localResourceSets.TryGetValue(culture.Name, out local_1))
            return local_1;
        }
      }
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      if (this.UseManifest && culture.HasInvariantCultureName)
      {
        Stream manifestResourceStream = ((RuntimeAssembly) this.MainAssembly).GetManifestResourceStream(this._locationInfo, this.GetResourceFileName(culture), (Assembly) this.m_callingAssembly == this.MainAssembly, ref stackMark);
        if (createIfNotExists && manifestResourceStream != null)
        {
          ResourceSet resourceSet = ((ManifestBasedResourceGroveler) this.resourceGroveler).CreateResourceSet(manifestResourceStream, this.MainAssembly);
          ResourceManager.AddResourceSet(localResourceSets, culture.Name, ref resourceSet);
          return resourceSet;
        }
      }
      return this.InternalGetResourceSet(culture, createIfNotExists, tryParents);
    }

    /// <summary>提供用于查找资源集的实现。</summary>
    /// <returns>指定的资源集。</returns>
    /// <param name="culture">要查找的区域性对象。</param>
    /// <param name="createIfNotExists">如果尚未加载，true 要加载资源集；否则为 false。</param>
    /// <param name="tryParents">要在无法加载资源集时检查父 <see cref="T:System.Globalization.CultureInfo" /> 对象，则为 true；否则为 false。</param>
    /// <exception cref="T:System.Resources.MissingManifestResourceException">主程序集不包含 .resources 文件，但查找资源需要此文件。</exception>
    /// <exception cref="T:System.ExecutionEngineException">运行时中存在内部错误。</exception>
    /// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">未能找到与 <paramref name="culture" /> 关联的附属程序集。</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    protected virtual ResourceSet InternalGetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalGetResourceSet(culture, createIfNotExists, tryParents, ref stackMark);
    }

    [SecurityCritical]
    private ResourceSet InternalGetResourceSet(CultureInfo requestedCulture, bool createIfNotExists, bool tryParents, ref StackCrawlMark stackMark)
    {
      Dictionary<string, ResourceSet> localResourceSets = this._resourceSets;
      ResourceSet rs = (ResourceSet) null;
      CultureInfo cultureInfo1 = (CultureInfo) null;
      lock (localResourceSets)
      {
        if (localResourceSets.TryGetValue(requestedCulture.Name, out rs))
        {
          if (FrameworkEventSource.IsInitialized)
            FrameworkEventSource.Log.ResourceManagerFoundResourceSetInCache(this.BaseNameField, this.MainAssembly, requestedCulture.Name);
          return rs;
        }
      }
      ResourceFallbackManager resourceFallbackManager = new ResourceFallbackManager(requestedCulture, this._neutralResourcesCulture, tryParents);
      foreach (CultureInfo culture in resourceFallbackManager)
      {
        if (FrameworkEventSource.IsInitialized)
          FrameworkEventSource.Log.ResourceManagerLookingForResourceSet(this.BaseNameField, this.MainAssembly, culture.Name);
        lock (localResourceSets)
        {
          if (localResourceSets.TryGetValue(culture.Name, out rs))
          {
            if (FrameworkEventSource.IsInitialized)
              FrameworkEventSource.Log.ResourceManagerFoundResourceSetInCache(this.BaseNameField, this.MainAssembly, culture.Name);
            if (requestedCulture != culture)
            {
              cultureInfo1 = culture;
              break;
            }
            break;
          }
        }
        rs = this.resourceGroveler.GrovelForResourceSet(culture, localResourceSets, tryParents, createIfNotExists, ref stackMark);
        if (rs != null)
        {
          cultureInfo1 = culture;
          break;
        }
      }
      if (rs != null && cultureInfo1 != null)
      {
        foreach (CultureInfo cultureInfo2 in resourceFallbackManager)
        {
          ResourceManager.AddResourceSet(localResourceSets, cultureInfo2.Name, ref rs);
          if (cultureInfo2 == cultureInfo1)
            break;
        }
      }
      return rs;
    }

    private static void AddResourceSet(Dictionary<string, ResourceSet> localResourceSets, string cultureName, ref ResourceSet rs)
    {
      lock (localResourceSets)
      {
        ResourceSet local_2;
        if (localResourceSets.TryGetValue(cultureName, out local_2))
        {
          if (local_2 == rs)
            return;
          if (!localResourceSets.ContainsValue(rs))
            rs.Dispose();
          rs = local_2;
        }
        else
          localResourceSets.Add(cultureName, rs);
      }
    }

    /// <summary>返回给定程序集中的 <see cref="T:System.Resources.SatelliteContractVersionAttribute" /> 特性指定的版本。</summary>
    /// <returns>给定程序集的附属版本，如果未找到任何版本，则为 null。</returns>
    /// <param name="a">要检查 <see cref="T:System.Resources.SatelliteContractVersionAttribute" /> 特性的程序集。</param>
    /// <exception cref="T:System.ArgumentException">程序集 <see cref="T:System.Version" /> 中找到的 <paramref name="a" /> 无效。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="a" /> 为 null。</exception>
    protected static Version GetSatelliteContractVersion(Assembly a)
    {
      if (a == (Assembly) null)
        throw new ArgumentNullException("a", Environment.GetResourceString("ArgumentNull_Assembly"));
      string version = (string) null;
      if (a.ReflectionOnly)
      {
        foreach (CustomAttributeData customAttribute in (IEnumerable<CustomAttributeData>) CustomAttributeData.GetCustomAttributes(a))
        {
          if (customAttribute.Constructor.DeclaringType == typeof (SatelliteContractVersionAttribute))
          {
            version = (string) customAttribute.ConstructorArguments[0].Value;
            break;
          }
        }
        if (version == null)
          return (Version) null;
      }
      else
      {
        object[] customAttributes = a.GetCustomAttributes(typeof (SatelliteContractVersionAttribute), false);
        if (customAttributes.Length == 0)
          return (Version) null;
        version = ((SatelliteContractVersionAttribute) customAttributes[0]).Version;
      }
      try
      {
        return new Version(version);
      }
      catch (ArgumentOutOfRangeException ex)
      {
        if (a == typeof (object).Assembly)
          return (Version) null;
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidSatelliteContract_Asm_Ver", (object) a.ToString(), (object) version), (Exception) ex);
      }
    }

    /// <summary>通过检索指定程序及上的 <see cref="T:System.Resources.NeutralResourcesLanguageAttribute" /> 特性为主程序集的默认资源返回区域性特定的信息。</summary>
    /// <returns>如果找到的话，则为 <see cref="T:System.Resources.NeutralResourcesLanguageAttribute" /> 中的特性；否则为固定区域性性。</returns>
    /// <param name="a">要返回其的区域性特定的信息的程序集。</param>
    [SecuritySafeCritical]
    protected static CultureInfo GetNeutralResourcesLanguage(Assembly a)
    {
      UltimateResourceFallbackLocation fallbackLocation = UltimateResourceFallbackLocation.MainAssembly;
      return ManifestBasedResourceGroveler.GetNeutralResourcesLanguage(a, ref fallbackLocation);
    }

    internal static bool CompareNames(string asmTypeName1, string typeName2, AssemblyName asmName2)
    {
      int startIndex = asmTypeName1.IndexOf(',');
      if ((startIndex == -1 ? asmTypeName1.Length : startIndex) != typeName2.Length || string.Compare(asmTypeName1, 0, typeName2, 0, typeName2.Length, StringComparison.Ordinal) != 0)
        return false;
      if (startIndex == -1)
        return true;
      do
        ;
      while (char.IsWhiteSpace(asmTypeName1[++startIndex]));
      AssemblyName assemblyName = new AssemblyName(asmTypeName1.Substring(startIndex));
      if (string.Compare(assemblyName.Name, asmName2.Name, StringComparison.OrdinalIgnoreCase) != 0)
        return false;
      if (string.Compare(assemblyName.Name, "mscorlib", StringComparison.OrdinalIgnoreCase) == 0)
        return true;
      if (assemblyName.CultureInfo != null && asmName2.CultureInfo != null && assemblyName.CultureInfo.LCID != asmName2.CultureInfo.LCID)
        return false;
      byte[] publicKeyToken1 = assemblyName.GetPublicKeyToken();
      byte[] publicKeyToken2 = asmName2.GetPublicKeyToken();
      if (publicKeyToken1 != null && publicKeyToken2 != null)
      {
        if (publicKeyToken1.Length != publicKeyToken2.Length)
          return false;
        for (int index = 0; index < publicKeyToken1.Length; ++index)
        {
          if ((int) publicKeyToken1[index] != (int) publicKeyToken2[index])
            return false;
        }
      }
      return true;
    }

    [SecuritySafeCritical]
    private string GetStringFromPRI(string stringName, string startingCulture, string neutralResourcesCulture)
    {
      if (stringName.Length == 0)
        return (string) null;
      return this._WinRTResourceManager.GetString(stringName, string.IsNullOrEmpty(startingCulture) ? (string) null : startingCulture, string.IsNullOrEmpty(neutralResourcesCulture) ? (string) null : neutralResourcesCulture);
    }

    [SecurityCritical]
    internal static WindowsRuntimeResourceManagerBase GetWinRTResourceManager()
    {
      return (WindowsRuntimeResourceManagerBase) Activator.CreateInstance(Type.GetType("System.Resources.WindowsRuntimeResourceManager, System.Runtime.WindowsRuntime, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", true), true);
    }

    [SecuritySafeCritical]
    private bool ShouldUseSatelliteAssemblyResourceLookupUnderAppX(RuntimeAssembly resourcesAssembly)
    {
      return resourcesAssembly.IsFrameworkAssembly();
    }

    [SecuritySafeCritical]
    private void SetAppXConfiguration()
    {
      bool flag1 = false;
      RuntimeAssembly resourcesAssembly = (RuntimeAssembly) this.MainAssembly;
      if ((Assembly) resourcesAssembly == (Assembly) null)
        resourcesAssembly = this.m_callingAssembly;
      if (!((Assembly) resourcesAssembly != (Assembly) null) || !((Assembly) resourcesAssembly != typeof (object).Assembly) || (!AppDomain.IsAppXModel() || AppDomain.IsAppXNGen))
        return;
      ResourceManager.s_IsAppXModel = true;
      string reswFilename = (this._locationInfo == (Type) null ? this.BaseNameField : this._locationInfo.FullName) ?? string.Empty;
      WindowsRuntimeResourceManagerBase resourceManagerBase = (WindowsRuntimeResourceManagerBase) null;
      bool flag2 = false;
      if (AppDomain.IsAppXDesignMode())
      {
        resourceManagerBase = ResourceManager.GetWinRTResourceManager();
        try
        {
          PRIExceptionInfo exceptionInfo;
          flag2 = resourceManagerBase.Initialize(resourcesAssembly.Location, reswFilename, out exceptionInfo);
          flag1 = !flag2;
        }
        catch (Exception ex)
        {
          flag1 = true;
          if (ex.IsTransient)
            throw;
        }
      }
      if (flag1)
        return;
      this._bUsingModernResourceManagement = !this.ShouldUseSatelliteAssemblyResourceLookupUnderAppX(resourcesAssembly);
      if (!this._bUsingModernResourceManagement)
        return;
      if (resourceManagerBase != null & flag2)
      {
        this._WinRTResourceManager = resourceManagerBase;
        this._PRIonAppXInitialized = true;
      }
      else
      {
        this._WinRTResourceManager = ResourceManager.GetWinRTResourceManager();
        try
        {
          this._PRIonAppXInitialized = this._WinRTResourceManager.Initialize(resourcesAssembly.Location, reswFilename, out this._PRIExceptionInfo);
        }
        catch (FileNotFoundException ex)
        {
        }
        catch (Exception ex)
        {
          if (ex.HResult == -2147009761)
            return;
          throw;
        }
      }
    }

    /// <summary>返回指定的字符串资源的值。</summary>
    /// <returns>为调用方的当前 UI 区域性本地化的资源的值，如果在资源集中找不到 <paramref name="name" />，则为 null。</returns>
    /// <param name="name">要检索的资源的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">指定资源的值不是字符串。</exception>
    /// <exception cref="T:System.Resources.MissingManifestResourceException">未找到可用的资源集，并且没有默认区域性的资源。有关如何处理此异常的信息，请参见<see cref="T:System.Resources.ResourceManager" /> 选件类主题中的“处理 MissingManifestResourceException 和 MissingSatelliteAssemblyException 异常”一节。</exception>
    /// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">默认区域性的资源位于无法找到的附属程序集。有关如何处理此异常的信息，请参见<see cref="T:System.Resources.ResourceManager" /> 选件类主题中的“处理 MissingManifestResourceException 和 MissingSatelliteAssemblyException 异常”一节。</exception>
    [__DynamicallyInvokable]
    public virtual string GetString(string name)
    {
      return this.GetString(name, (CultureInfo) null);
    }

    /// <summary>返回为指定区域性本地化的字符串资源的值。</summary>
    /// <returns>为指定区域性本地化的资源的值，如果在资源集中找不到 <paramref name="name" />，则为 null。</returns>
    /// <param name="name">要检索的资源的名称。</param>
    /// <param name="culture">一个对象，表示为其本地化资源的区域性。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">指定资源的值不是字符串。</exception>
    /// <exception cref="T:System.Resources.MissingManifestResourceException">未找到可用的资源集，并且没有默认区域性的资源。有关如何处理此异常的信息，请参见<see cref="T:System.Resources.ResourceManager" /> 选件类主题中的“处理 MissingManifestResourceException 和 MissingSatelliteAssemblyException 异常”一节。</exception>
    /// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">默认区域性的资源位于无法找到的附属程序集。有关如何处理此异常的信息，请参见<see cref="T:System.Resources.ResourceManager" /> 选件类主题中的“处理 MissingManifestResourceException 和 MissingSatelliteAssemblyException 异常”一节。</exception>
    [__DynamicallyInvokable]
    public virtual string GetString(string name, CultureInfo culture)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (ResourceManager.s_IsAppXModel && culture == CultureInfo.CurrentUICulture)
        culture = (CultureInfo) null;
      if (this._bUsingModernResourceManagement)
      {
        if (this._PRIonAppXInitialized)
          return this.GetStringFromPRI(name, culture == null ? (string) null : culture.Name, this._neutralResourcesCulture.Name);
        if (this._PRIExceptionInfo != null && this._PRIExceptionInfo._PackageSimpleName != null && this._PRIExceptionInfo._ResWFile != null)
          throw new MissingManifestResourceException(Environment.GetResourceString("MissingManifestResource_ResWFileNotLoaded", (object) this._PRIExceptionInfo._ResWFile, (object) this._PRIExceptionInfo._PackageSimpleName));
        throw new MissingManifestResourceException(Environment.GetResourceString("MissingManifestResource_NoPRIresources"));
      }
      if (culture == null)
        culture = Thread.CurrentThread.GetCurrentUICultureNoAppX();
      if (FrameworkEventSource.IsInitialized)
        FrameworkEventSource.Log.ResourceManagerLookupStarted(this.BaseNameField, this.MainAssembly, culture.Name);
      ResourceSet resourceSet1 = this.GetFirstResourceSet(culture);
      if (resourceSet1 != null)
      {
        string @string = resourceSet1.GetString(name, this._ignoreCase);
        if (@string != null)
          return @string;
      }
      foreach (CultureInfo culture1 in new ResourceFallbackManager(culture, this._neutralResourcesCulture, true))
      {
        ResourceSet resourceSet2 = this.InternalGetResourceSet(culture1, true, true);
        if (resourceSet2 != null)
        {
          if (resourceSet2 != resourceSet1)
          {
            string @string = resourceSet2.GetString(name, this._ignoreCase);
            if (@string != null)
            {
              if (this._lastUsedResourceCache != null)
              {
                lock (this._lastUsedResourceCache)
                {
                  this._lastUsedResourceCache.lastCultureName = culture1.Name;
                  this._lastUsedResourceCache.lastResourceSet = resourceSet2;
                }
              }
              return @string;
            }
            resourceSet1 = resourceSet2;
          }
        }
        else
          break;
      }
      if (FrameworkEventSource.IsInitialized)
        FrameworkEventSource.Log.ResourceManagerLookupFailed(this.BaseNameField, this.MainAssembly, culture.Name);
      return (string) null;
    }

    /// <summary>返回指定的非字符串资源的值。</summary>
    /// <returns>针对调用方的当前区域性设置而本地化的资源的值。如果相应的资源集存在，但无法找到 <paramref name="name" />，该方法返回 null。</returns>
    /// <param name="name">要获取的资源名。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.Resources.MissingManifestResourceException">未找到可用的本地化资源集，并且没有默认区域性资源。有关如何处理此异常的信息，请参见<see cref="T:System.Resources.ResourceManager" /> 选件类主题中的“处理 MissingManifestResourceException 和 MissingSatelliteAssemblyException 异常”一节。</exception>
    /// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">默认区域性的资源位于无法找到的附属程序集。有关如何处理此异常的信息，请参见<see cref="T:System.Resources.ResourceManager" /> 选件类主题中的“处理 MissingManifestResourceException 和 MissingSatelliteAssemblyException 异常”一节。</exception>
    public virtual object GetObject(string name)
    {
      return this.GetObject(name, (CultureInfo) null, true);
    }

    /// <summary>获取为指定区域性本地化的指定非字符串资源的值。</summary>
    /// <returns>为指定区域性本地化的资源的值。如果相应的资源集存在，但无法找到 <paramref name="name" />，该方法返回 null。</returns>
    /// <param name="name">要获取的资源名。</param>
    /// <param name="culture">要针对其本地化资源的区域性。如果资源未本地化为此区域性，则资源管理器使用回退规则找到适当的资源。如果此值为 null，则 <see cref="T:System.Globalization.CultureInfo" /> 对象使用 <see cref="P:System.Globalization.CultureInfo.CurrentUICulture" /> 属性来获取。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.Resources.MissingManifestResourceException">未找到可用的资源集，并且没有默认区域性的资源。有关如何处理此异常的信息，请参见<see cref="T:System.Resources.ResourceManager" /> 选件类主题中的“处理 MissingManifestResourceException 和 MissingSatelliteAssemblyException 异常”一节。</exception>
    /// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">默认区域性的资源位于无法找到的附属程序集。有关如何处理此异常的信息，请参见<see cref="T:System.Resources.ResourceManager" /> 选件类主题中的“处理 MissingManifestResourceException 和 MissingSatelliteAssemblyException 异常”一节。</exception>
    public virtual object GetObject(string name, CultureInfo culture)
    {
      return this.GetObject(name, culture, true);
    }

    private object GetObject(string name, CultureInfo culture, bool wrapUnmanagedMemStream)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (ResourceManager.s_IsAppXModel && culture == CultureInfo.CurrentUICulture)
        culture = (CultureInfo) null;
      if (culture == null)
        culture = Thread.CurrentThread.GetCurrentUICultureNoAppX();
      if (FrameworkEventSource.IsInitialized)
        FrameworkEventSource.Log.ResourceManagerLookupStarted(this.BaseNameField, this.MainAssembly, culture.Name);
      ResourceSet resourceSet1 = this.GetFirstResourceSet(culture);
      if (resourceSet1 != null)
      {
        object @object = resourceSet1.GetObject(name, this._ignoreCase);
        if (@object != null)
        {
          UnmanagedMemoryStream stream = @object as UnmanagedMemoryStream;
          if (stream != null & wrapUnmanagedMemStream)
            return (object) new UnmanagedMemoryStreamWrapper(stream);
          return @object;
        }
      }
      foreach (CultureInfo culture1 in new ResourceFallbackManager(culture, this._neutralResourcesCulture, true))
      {
        ResourceSet resourceSet2 = this.InternalGetResourceSet(culture1, true, true);
        if (resourceSet2 != null)
        {
          if (resourceSet2 != resourceSet1)
          {
            object @object = resourceSet2.GetObject(name, this._ignoreCase);
            if (@object != null)
            {
              if (this._lastUsedResourceCache != null)
              {
                lock (this._lastUsedResourceCache)
                {
                  this._lastUsedResourceCache.lastCultureName = culture1.Name;
                  this._lastUsedResourceCache.lastResourceSet = resourceSet2;
                }
              }
              UnmanagedMemoryStream stream = @object as UnmanagedMemoryStream;
              if (stream != null & wrapUnmanagedMemStream)
                return (object) new UnmanagedMemoryStreamWrapper(stream);
              return @object;
            }
            resourceSet1 = resourceSet2;
          }
        }
        else
          break;
      }
      if (FrameworkEventSource.IsInitialized)
        FrameworkEventSource.Log.ResourceManagerLookupFailed(this.BaseNameField, this.MainAssembly, culture.Name);
      return (object) null;
    }

    /// <summary>从指定资源返回非托管内存流对象。</summary>
    /// <returns>表示资源的非托管内存流对象。</returns>
    /// <param name="name">资源的名称。</param>
    /// <exception cref="T:System.InvalidOperationException">指定资源的值不是 <see cref="T:System.IO.MemoryStream" /> 对象。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.Resources.MissingManifestResourceException">未找到可用的资源集，并且没有默认资源。有关如何处理此异常的信息，请参见<see cref="T:System.Resources.ResourceManager" /> 选件类主题中的“处理 MissingManifestResourceException 和 MissingSatelliteAssemblyException 异常”一节。</exception>
    /// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">默认区域性的资源位于无法找到的附属程序集。有关如何处理此异常的信息，请参见<see cref="T:System.Resources.ResourceManager" /> 选件类主题中的“处理 MissingManifestResourceException 和 MissingSatelliteAssemblyException 异常”一节。</exception>
    [ComVisible(false)]
    public UnmanagedMemoryStream GetStream(string name)
    {
      return this.GetStream(name, (CultureInfo) null);
    }

    /// <summary>使用指定的区域性从指定的资源返回非托管内存流对象。</summary>
    /// <returns>表示资源的非托管内存流对象。</returns>
    /// <param name="name">资源的名称。</param>
    /// <param name="culture">指定要用于资源查找的区域性的对象。如果 <paramref name="culture" /> 为 null，则使用当前线程的区域性。</param>
    /// <exception cref="T:System.InvalidOperationException">指定资源的值不是 <see cref="T:System.IO.MemoryStream" /> 对象。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.Resources.MissingManifestResourceException">未找到可用的资源集，并且没有默认资源。有关如何处理此异常的信息，请参见<see cref="T:System.Resources.ResourceManager" /> 选件类主题中的“处理 MissingManifestResourceException 和 MissingSatelliteAssemblyException 异常”一节。</exception>
    /// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">默认区域性的资源位于无法找到的附属程序集。有关如何处理此异常的信息，请参见<see cref="T:System.Resources.ResourceManager" /> 选件类主题中的“处理 MissingManifestResourceException 和 MissingSatelliteAssemblyException 异常”一节。</exception>
    [ComVisible(false)]
    public UnmanagedMemoryStream GetStream(string name, CultureInfo culture)
    {
      object @object = this.GetObject(name, culture, false);
      UnmanagedMemoryStream unmanagedMemoryStream = @object as UnmanagedMemoryStream;
      if (unmanagedMemoryStream == null && @object != null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotStream_Name", (object) name));
      return unmanagedMemoryStream;
    }

    [SecurityCritical]
    private bool TryLookingForSatellite(CultureInfo lookForCulture)
    {
      if (!ResourceManager._checkedConfigFile)
      {
        lock (this)
        {
          if (!ResourceManager._checkedConfigFile)
          {
            ResourceManager._checkedConfigFile = true;
            ResourceManager._installedSatelliteInfo = this.GetSatelliteAssembliesFromConfig();
          }
        }
      }
      if (ResourceManager._installedSatelliteInfo == null)
        return true;
      string[] array = (string[]) ResourceManager._installedSatelliteInfo[(object) this.MainAssembly.FullName];
      if (array == null)
        return true;
      int num = Array.IndexOf<string>(array, lookForCulture.Name);
      if (FrameworkEventSource.IsInitialized && FrameworkEventSource.Log.IsEnabled())
      {
        if (num < 0)
          FrameworkEventSource.Log.ResourceManagerCultureNotFoundInConfigFile(this.BaseNameField, this.MainAssembly, lookForCulture.Name);
        else
          FrameworkEventSource.Log.ResourceManagerCultureFoundInConfigFile(this.BaseNameField, this.MainAssembly, lookForCulture.Name);
      }
      return num >= 0;
    }

    [SecurityCritical]
    private Hashtable GetSatelliteAssembliesFromConfig()
    {
      string configurationFileInternal = AppDomain.CurrentDomain.FusionStore.ConfigurationFileInternal;
      if (configurationFileInternal == null)
        return (Hashtable) null;
      if (configurationFileInternal.Length >= 2 && ((int) configurationFileInternal[1] == (int) Path.VolumeSeparatorChar || (int) configurationFileInternal[0] == (int) Path.DirectorySeparatorChar && (int) configurationFileInternal[1] == (int) Path.DirectorySeparatorChar) && !File.InternalExists(configurationFileInternal))
        return (Hashtable) null;
      ConfigTreeParser configTreeParser = new ConfigTreeParser();
      string configPath = "/configuration/satelliteassemblies";
      ConfigNode configNode = (ConfigNode) null;
      try
      {
        configNode = configTreeParser.Parse(configurationFileInternal, configPath, true);
      }
      catch (Exception ex)
      {
      }
      if (configNode == null)
        return (Hashtable) null;
      Hashtable hashtable = new Hashtable((IEqualityComparer) StringComparer.OrdinalIgnoreCase);
      foreach (ConfigNode child1 in configNode.Children)
      {
        if (!string.Equals(child1.Name, "assembly"))
          throw new ApplicationException(Environment.GetResourceString("XMLSyntax_InvalidSyntaxSatAssemTag", (object) Path.GetFileName(configurationFileInternal), (object) child1.Name));
        if (child1.Attributes.Count == 0)
          throw new ApplicationException(Environment.GetResourceString("XMLSyntax_InvalidSyntaxSatAssemTagNoAttr", (object) Path.GetFileName(configurationFileInternal)));
        DictionaryEntry dictionaryEntry = child1.Attributes[0];
        string str = (string) dictionaryEntry.Value;
        if (!object.Equals(dictionaryEntry.Key, (object) "name") || string.IsNullOrEmpty(str) || child1.Attributes.Count > 1)
          throw new ApplicationException(Environment.GetResourceString("XMLSyntax_InvalidSyntaxSatAssemTagBadAttr", (object) Path.GetFileName(configurationFileInternal), dictionaryEntry.Key, dictionaryEntry.Value));
        ArrayList arrayList = new ArrayList(5);
        foreach (ConfigNode child2 in child1.Children)
        {
          if (child2.Value != null)
            arrayList.Add((object) child2.Value);
        }
        string[] strArray = new string[arrayList.Count];
        for (int index = 0; index < strArray.Length; ++index)
        {
          string cultureName = (string) arrayList[index];
          strArray[index] = cultureName;
          if (FrameworkEventSource.IsInitialized)
            FrameworkEventSource.Log.ResourceManagerAddingCultureFromConfigFile(this.BaseNameField, this.MainAssembly, cultureName);
        }
        hashtable.Add((object) str, (object) strArray);
      }
      return hashtable;
    }

    internal class CultureNameResourceSetPair
    {
      public string lastCultureName;
      public ResourceSet lastResourceSet;
    }

    internal class ResourceManagerMediator
    {
      private ResourceManager _rm;

      internal string ModuleDir
      {
        get
        {
          return this._rm.moduleDir;
        }
      }

      internal Type LocationInfo
      {
        get
        {
          return this._rm._locationInfo;
        }
      }

      internal Type UserResourceSet
      {
        get
        {
          return this._rm._userResourceSet;
        }
      }

      internal string BaseNameField
      {
        get
        {
          return this._rm.BaseNameField;
        }
      }

      internal CultureInfo NeutralResourcesCulture
      {
        get
        {
          return this._rm._neutralResourcesCulture;
        }
        set
        {
          this._rm._neutralResourcesCulture = value;
        }
      }

      internal bool LookedForSatelliteContractVersion
      {
        get
        {
          return this._rm._lookedForSatelliteContractVersion;
        }
        set
        {
          this._rm._lookedForSatelliteContractVersion = value;
        }
      }

      internal Version SatelliteContractVersion
      {
        get
        {
          return this._rm._satelliteContractVersion;
        }
        set
        {
          this._rm._satelliteContractVersion = value;
        }
      }

      internal UltimateResourceFallbackLocation FallbackLoc
      {
        get
        {
          return this._rm.FallbackLocation;
        }
        set
        {
          this._rm._fallbackLoc = value;
        }
      }

      internal RuntimeAssembly CallingAssembly
      {
        get
        {
          return this._rm.m_callingAssembly;
        }
      }

      internal RuntimeAssembly MainAssembly
      {
        get
        {
          return (RuntimeAssembly) this._rm.MainAssembly;
        }
      }

      internal string BaseName
      {
        get
        {
          return this._rm.BaseName;
        }
      }

      internal ResourceManagerMediator(ResourceManager rm)
      {
        if (rm == null)
          throw new ArgumentNullException("rm");
        this._rm = rm;
      }

      internal string GetResourceFileName(CultureInfo culture)
      {
        return this._rm.GetResourceFileName(culture);
      }

      internal Version ObtainSatelliteContractVersion(Assembly a)
      {
        return ResourceManager.GetSatelliteContractVersion(a);
      }

      [SecurityCritical]
      internal bool TryLookingForSatellite(CultureInfo lookForCulture)
      {
        return this._rm.TryLookingForSatellite(lookForCulture);
      }
    }
  }
}
