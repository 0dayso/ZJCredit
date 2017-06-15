// Decompiled with JetBrains decompiler
// Type: System.AppDomainSetup
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Deployment.Internal.Isolation.Manifest;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Util;
using System.Text;

namespace System
{
  /// <summary>表示可以添加到 <see cref="T:System.AppDomain" /> 的实例的程序集绑定信息。</summary>
  /// <filterpriority>2</filterpriority>
  [ClassInterface(ClassInterfaceType.None)]
  [ComVisible(true)]
  [Serializable]
  public sealed class AppDomainSetup : IAppDomainSetup
  {
    private string[] _Entries;
    private LoaderOptimization _LoaderOptimization;
    private string _AppBase;
    [OptionalField(VersionAdded = 2)]
    private AppDomainInitializer _AppDomainInitializer;
    [OptionalField(VersionAdded = 2)]
    private string[] _AppDomainInitializerArguments;
    [OptionalField(VersionAdded = 2)]
    private ActivationArguments _ActivationArguments;
    [OptionalField(VersionAdded = 2)]
    private string _ApplicationTrust;
    [OptionalField(VersionAdded = 2)]
    private byte[] _ConfigurationBytes;
    [OptionalField(VersionAdded = 3)]
    private bool _DisableInterfaceCache;
    [OptionalField(VersionAdded = 4)]
    private string _AppDomainManagerAssembly;
    [OptionalField(VersionAdded = 4)]
    private string _AppDomainManagerType;
    [OptionalField(VersionAdded = 4)]
    private string[] _AptcaVisibleAssemblies;
    [OptionalField(VersionAdded = 4)]
    private Dictionary<string, object> _CompatFlags;
    [OptionalField(VersionAdded = 5)]
    private string _TargetFrameworkName;
    [NonSerialized]
    internal AppDomainSortingSetupInfo _AppDomainSortingSetupInfo;
    [OptionalField(VersionAdded = 5)]
    private bool _CheckedForTargetFrameworkName;
    [OptionalField(VersionAdded = 5)]
    private bool _UseRandomizedStringHashing;

    internal string[] Value
    {
      get
      {
        if (this._Entries == null)
          this._Entries = new string[18];
        return this._Entries;
      }
    }

    /// <summary>获取或设置程序集的显示名称，该程序集为使用此 <see cref="T:System.AppDomainSetup" /> 对象创建的应用程序域提供应用程序域管理器的类型。</summary>
    /// <returns>提供应用程序域管理器的 <see cref="T:System.Type" /> 的程序集的显示名称。</returns>
    public string AppDomainManagerAssembly
    {
      get
      {
        return this._AppDomainManagerAssembly;
      }
      set
      {
        this._AppDomainManagerAssembly = value;
      }
    }

    /// <summary>获取或设置类型的全名，该类型为使用此 <see cref="T:System.AppDomainSetup" /> 对象创建的应用程序域提供应用程序域管理器。</summary>
    /// <returns>类型的全名，其中包括命名空间。</returns>
    public string AppDomainManagerType
    {
      get
      {
        return this._AppDomainManagerType;
      }
      set
      {
        this._AppDomainManagerType = value;
      }
    }

    /// <summary>获取或设置标以 <see cref="F:System.Security.PartialTrustVisibilityLevel.NotVisibleByDefault" /> 标志的程序集的列表，这些程序集对沙箱应用程序域中的部分信任代码可见。</summary>
    /// <returns>部分程序集名称的数组，其中每个部分名称都由简单程序集名称和公钥组成。</returns>
    public string[] PartialTrustVisibleAssemblies
    {
      get
      {
        return this._AptcaVisibleAssemblies;
      }
      set
      {
        if (value != null)
        {
          this._AptcaVisibleAssemblies = (string[]) value.Clone();
          Array.Sort<string>(this._AptcaVisibleAssemblies, (IComparer<string>) StringComparer.OrdinalIgnoreCase);
        }
        else
          this._AptcaVisibleAssemblies = (string[]) null;
      }
    }

    /// <summary>获取或设置包含该应用程序的目录的名称。</summary>
    /// <returns>应用程序基目录的名称。</returns>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public string ApplicationBase
    {
      [SecuritySafeCritical] get
      {
        return this.VerifyDir(this.GetUnsecureApplicationBase(), false);
      }
      set
      {
        this.Value[0] = this.NormalizePath(value, false);
      }
    }

    internal static string ApplicationBaseKey
    {
      get
      {
        return "APPBASE";
      }
    }

    /// <summary>获取或设置应用程序域的配置文件的名称。</summary>
    /// <returns>配置文件的名称。</returns>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public string ConfigurationFile
    {
      [SecuritySafeCritical] get
      {
        return this.VerifyDir(this.Value[1], true);
      }
      set
      {
        this.Value[1] = value;
      }
    }

    internal string ConfigurationFileInternal
    {
      get
      {
        return this.NormalizePath(this.Value[1], true);
      }
    }

    internal static string ConfigurationFileKey
    {
      get
      {
        return "APP_CONFIG_FILE";
      }
    }

    private static string ConfigurationBytesKey
    {
      get
      {
        return "APP_CONFIG_BLOB";
      }
    }

    /// <summary>获取或设置为应用程序域指定目标版本和 .NET Framework 配置文件的字符串，以可由 <see cref="M:System.Runtime.Versioning.FrameworkName.#ctor(System.String)" /> 构造函数分析的格式。</summary>
    /// <returns> .NET Framework 的目标版本与配置文件。</returns>
    public string TargetFrameworkName
    {
      get
      {
        return this._TargetFrameworkName;
      }
      set
      {
        this._TargetFrameworkName = value;
      }
    }

    internal bool CheckedForTargetFrameworkName
    {
      get
      {
        return this._CheckedForTargetFrameworkName;
      }
      set
      {
        this._CheckedForTargetFrameworkName = value;
      }
    }

    /// <summary>获取或设置动态生成的文件所在的目录的基目录。</summary>
    /// <returns>
    /// <see cref="P:System.AppDomain.DynamicDirectory" /> 所在的目录。说明该属性的返回值不同于分配的值。请参见“备注”部分。</returns>
    /// <exception cref="T:System.MemberAccessException">无法设置此属性，因为应用程序域上的应用程序名为 null。</exception>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public string DynamicBase
    {
      [SecuritySafeCritical] get
      {
        return this.VerifyDir(this.Value[2], true);
      }
      [SecuritySafeCritical] set
      {
        if (value == null)
        {
          this.Value[2] = (string) null;
        }
        else
        {
          if (this.ApplicationName == null)
            throw new MemberAccessException(Environment.GetResourceString("AppDomain_RequireApplicationName"));
          StringBuilder stringBuilder = new StringBuilder(this.NormalizePath(value, false));
          stringBuilder.Append('\\');
          string @string = ParseNumbers.IntToString(this.ApplicationName.GetLegacyNonRandomizedHashCode(), 16, 8, '0', 256);
          stringBuilder.Append(@string);
          this.Value[2] = stringBuilder.ToString();
        }
      }
    }

    internal static string DynamicBaseKey
    {
      get
      {
        return "DYNAMIC_BASE";
      }
    }

    /// <summary>获取或设置一个值，该值指示是否将配置文件的 &lt;publisherPolicy&gt; 节应用于应用程序域。</summary>
    /// <returns>如果应用程序域忽略配置文件的 &lt;publisherPolicy&gt; 节，则为 true；如果接受所声明的发行者策略，则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public bool DisallowPublisherPolicy
    {
      get
      {
        return this.Value[11] != null;
      }
      set
      {
        if (value)
          this.Value[11] = "true";
        else
          this.Value[11] = (string) null;
      }
    }

    /// <summary>获取或设置一个值，该值指示应用程序域是否允许程序集绑定重定向。</summary>
    /// <returns>如果不允许程序集的重定向，则为 true；如果允许，则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public bool DisallowBindingRedirects
    {
      get
      {
        return this.Value[13] != null;
      }
      set
      {
        if (value)
          this.Value[13] = "true";
        else
          this.Value[13] = (string) null;
      }
    }

    /// <summary>获取或设置一个值，该值指示应用程序域是否允许通过 HTTP 下载程序集。</summary>
    /// <returns>如果不允许通过 HTTP 下载程序集，则为 true；如果允许，则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    public bool DisallowCodeDownload
    {
      get
      {
        return this.Value[12] != null;
      }
      set
      {
        if (value)
          this.Value[12] = "true";
        else
          this.Value[12] = (string) null;
      }
    }

    /// <summary>指定在搜索要加载的程序集时是否探测应用程序基路径和专用二进制路径。</summary>
    /// <returns>如果不允许探测，则为 true；否则为 false。默认值为 false。</returns>
    /// <filterpriority>1</filterpriority>
    public bool DisallowApplicationBaseProbing
    {
      get
      {
        return this.Value[14] != null;
      }
      set
      {
        if (value)
          this.Value[14] = "true";
        else
          this.Value[14] = (string) null;
      }
    }

    internal string DeveloperPath
    {
      [SecurityCritical] get
      {
        string dirs = this.Value[3];
        this.VerifyDirList(dirs);
        return dirs;
      }
      set
      {
        if (value == null)
        {
          this.Value[3] = (string) null;
        }
        else
        {
          string[] strArray = value.Split(';');
          int length = strArray.Length;
          StringBuilder sb = StringBuilderCache.Acquire(16);
          bool flag = false;
          for (int index = 0; index < length; ++index)
          {
            if (strArray[index].Length != 0)
            {
              if (flag)
                sb.Append(";");
              else
                flag = true;
              sb.Append(Path.GetFullPathInternal(strArray[index]));
            }
          }
          string stringAndRelease = StringBuilderCache.GetStringAndRelease(sb);
          if (stringAndRelease.Length == 0)
            this.Value[3] = (string) null;
          else
            this.Value[3] = stringAndRelease;
        }
      }
    }

    internal static string DisallowPublisherPolicyKey
    {
      get
      {
        return "DISALLOW_APP";
      }
    }

    internal static string DisallowCodeDownloadKey
    {
      get
      {
        return "CODE_DOWNLOAD_DISABLED";
      }
    }

    internal static string DisallowBindingRedirectsKey
    {
      get
      {
        return "DISALLOW_APP_REDIRECTS";
      }
    }

    internal static string DeveloperPathKey
    {
      get
      {
        return "DEV_PATH";
      }
    }

    internal static string DisallowAppBaseProbingKey
    {
      get
      {
        return "DISALLOW_APP_BASE_PROBING";
      }
    }

    /// <summary>获取或设置应用程序的名称。</summary>
    /// <returns>应用程序的名称。</returns>
    /// <filterpriority>2</filterpriority>
    public string ApplicationName
    {
      get
      {
        return this.Value[4];
      }
      set
      {
        this.Value[4] = value;
      }
    }

    internal static string ApplicationNameKey
    {
      get
      {
        return "APP_NAME";
      }
    }

    /// <summary>获取或设置 <see cref="T:System.AppDomainInitializer" /> 委托，该委托表示一个在初始化应用程序域时调用的回调方法。</summary>
    /// <returns>一个委托，表示在初始化应用程序域时调用的回调方法。</returns>
    /// <filterpriority>2</filterpriority>
    [XmlIgnoreMember]
    public AppDomainInitializer AppDomainInitializer
    {
      get
      {
        return this._AppDomainInitializer;
      }
      set
      {
        this._AppDomainInitializer = value;
      }
    }

    /// <summary>获取或设置传给 <see cref="T:System.AppDomainInitializer" /> 委托所表示的回调方法的参数。在初始化应用程序域时将调用该回调方法。</summary>
    /// <returns>一个字符串数组，当在 <see cref="T:System.AppDomain" /> 初始化过程中调用 <see cref="T:System.AppDomainInitializer" /> 委托所表示的回调方法时，该字符串数组将被传给该回调方法。</returns>
    /// <filterpriority>2</filterpriority>
    public string[] AppDomainInitializerArguments
    {
      get
      {
        return this._AppDomainInitializerArguments;
      }
      set
      {
        this._AppDomainInitializerArguments = value;
      }
    }

    /// <summary>获取或设置与应用程序域的激活有关的数据。</summary>
    /// <returns>一个对象，其中包含与应用程序域的激活有关的数据。</returns>
    /// <exception cref="T:System.InvalidOperationException">该属性被设置为一个 <see cref="T:System.Runtime.Hosting.ActivationArguments" /> 对象，该对象的应用程序标识与 <see cref="P:System.AppDomainSetup.ApplicationTrust" /> 属性返回的 <see cref="T:System.Security.Policy.ApplicationTrust" /> 对象的应用程序标识不匹配。如果 <see cref="P:System.AppDomainSetup.ApplicationTrust" /> 属性为 null，则不会引发异常。</exception>
    /// <filterpriority>1</filterpriority>
    [XmlIgnoreMember]
    public ActivationArguments ActivationArguments
    {
      get
      {
        return this._ActivationArguments;
      }
      set
      {
        this._ActivationArguments = value;
      }
    }

    /// <summary>获取或设置一个包含安全性和信任信息的对象。</summary>
    /// <returns>获取或设置一个对象，其中包含安全性和信任信息。</returns>
    /// <exception cref="T:System.InvalidOperationException">该属性被设置为一个 <see cref="T:System.Security.Policy.ApplicationTrust" /> 对象，该对象的应用程序标识与 <see cref="P:System.AppDomainSetup.ActivationArguments" /> 属性返回的 <see cref="T:System.Runtime.Hosting.ActivationArguments" /> 对象的应用程序标识不匹配。如果 <see cref="P:System.AppDomainSetup.ActivationArguments" /> 属性为 null，则不会引发异常。</exception>
    /// <exception cref="T:System.ArgumentNullException">该属性设置为 null。</exception>
    [XmlIgnoreMember]
    public ApplicationTrust ApplicationTrust
    {
      get
      {
        return this.InternalGetApplicationTrust();
      }
      set
      {
        this.InternalSetApplicationTrust(value);
      }
    }

    /// <summary>获取或设置应用程序基目录下的目录列表，这些目录被探测以寻找其中的私有程序集。</summary>
    /// <returns>目录名称的列表，用分号分隔。</returns>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public string PrivateBinPath
    {
      [SecuritySafeCritical] get
      {
        string dirs = this.Value[5];
        this.VerifyDirList(dirs);
        return dirs;
      }
      set
      {
        this.Value[5] = value;
      }
    }

    internal static string PrivateBinPathKey
    {
      get
      {
        return "PRIVATE_BINPATH";
      }
    }

    /// <summary>获取或设置一个字符串值，用于在应用程序的搜索路径中包含 <see cref="P:System.AppDomainSetup.ApplicationBase" />，或者从搜索路径中排除该基路径而只在 <see cref="P:System.AppDomainSetup.PrivateBinPath" /> 中进行搜索。</summary>
    /// <returns>空引用（ Visual Basic 中的 Nothing），包括搜索程序集时的应用程序基路径；要排除该路径的任何非空字符串值。默认值为 null。</returns>
    /// <filterpriority>2</filterpriority>
    public string PrivateBinPathProbe
    {
      get
      {
        return this.Value[6];
      }
      set
      {
        this.Value[6] = value;
      }
    }

    internal static string PrivateBinPathProbeKey
    {
      get
      {
        return "BINPATH_PROBE_ONLY";
      }
    }

    /// <summary>获取或设置目录的名称，这些目录包含要进行卷影复制的程序集。</summary>
    /// <returns>目录名称的列表，用分号分隔。</returns>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public string ShadowCopyDirectories
    {
      [SecuritySafeCritical] get
      {
        string dirs = this.Value[7];
        this.VerifyDirList(dirs);
        return dirs;
      }
      set
      {
        this.Value[7] = value;
      }
    }

    internal static string ShadowCopyDirectoriesKey
    {
      get
      {
        return "SHADOW_COPY_DIRS";
      }
    }

    /// <summary>获取或设置指示卷影复制是打开还是关闭的字符串。</summary>
    /// <returns>字符串值“true”指示打开了卷影复制；“false”则指示关闭了卷影复制。</returns>
    /// <filterpriority>2</filterpriority>
    public string ShadowCopyFiles
    {
      get
      {
        return this.Value[8];
      }
      set
      {
        if (value != null && string.Compare(value, "true", StringComparison.OrdinalIgnoreCase) == 0)
          this.Value[8] = value;
        else
          this.Value[8] = (string) null;
      }
    }

    internal static string ShadowCopyFilesKey
    {
      get
      {
        return "FORCE_CACHE_INSTALL";
      }
    }

    /// <summary>获取或设置特定于应用程序且从中对文件进行卷影复制的区域的名称。</summary>
    /// <returns>从中对文件进行卷影复制的目录路径和文件名的完全限定名。</returns>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public string CachePath
    {
      [SecuritySafeCritical] get
      {
        return this.VerifyDir(this.Value[9], false);
      }
      set
      {
        this.Value[9] = this.NormalizePath(value, false);
      }
    }

    internal static string CachePathKey
    {
      get
      {
        return "CACHE_BASE";
      }
    }

    /// <summary>获取或设置与此域关联的许可证文件的位置。</summary>
    /// <returns>许可证文件的位置和名称。</returns>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public string LicenseFile
    {
      [SecuritySafeCritical] get
      {
        return this.VerifyDir(this.Value[10], true);
      }
      set
      {
        this.Value[10] = value;
      }
    }

    /// <summary>指定用于加载可执行文件的优化策略。</summary>
    /// <returns>与 <see cref="T:System.LoaderOptimizationAttribute" /> 配合使用的枚举常数。</returns>
    /// <filterpriority>2</filterpriority>
    public LoaderOptimization LoaderOptimization
    {
      get
      {
        return this._LoaderOptimization;
      }
      set
      {
        this._LoaderOptimization = value;
      }
    }

    internal static string LoaderOptimizationKey
    {
      get
      {
        return "LOADER_OPTIMIZATION";
      }
    }

    internal static string ConfigurationExtension
    {
      get
      {
        return ".config";
      }
    }

    internal static string PrivateBinPathEnvironmentVariable
    {
      get
      {
        return "RELPATH";
      }
    }

    internal static string RuntimeConfigurationFile
    {
      get
      {
        return "config\\machine.config";
      }
    }

    internal static string MachineConfigKey
    {
      get
      {
        return "MACHINE_CONFIG";
      }
    }

    internal static string HostBindingKey
    {
      get
      {
        return "HOST_CONFIG";
      }
    }

    /// <summary>获取或设置一个值，该值指示是否在应用程序域中为互操作调用禁用接口缓存，从而对每个调用执行 QueryInterface。</summary>
    /// <returns>如果为使用当前 <see cref="T:System.AppDomainSetup" /> 对象创建的应用程序域中的互操作调用禁用接口缓存，则为 true；否则为 false。</returns>
    public bool SandboxInterop
    {
      get
      {
        return this._DisableInterfaceCache;
      }
      set
      {
        this._DisableInterfaceCache = value;
      }
    }

    [SecuritySafeCritical]
    internal AppDomainSetup(AppDomainSetup copy, bool copyDomainBoundData)
    {
      string[] strArray1 = this.Value;
      if (copy != null)
      {
        string[] strArray2 = copy.Value;
        int length1 = this._Entries.Length;
        int length2 = strArray2.Length;
        int num = length2 < length1 ? length2 : length1;
        for (int index = 0; index < num; ++index)
          strArray1[index] = strArray2[index];
        if (num < length1)
        {
          for (int index = num; index < length1; ++index)
            strArray1[index] = (string) null;
        }
        this._LoaderOptimization = copy._LoaderOptimization;
        this._AppDomainInitializerArguments = copy.AppDomainInitializerArguments;
        this._ActivationArguments = copy.ActivationArguments;
        this._ApplicationTrust = copy._ApplicationTrust;
        this._AppDomainInitializer = !copyDomainBoundData ? (AppDomainInitializer) null : copy.AppDomainInitializer;
        this._ConfigurationBytes = copy.GetConfigurationBytes();
        this._DisableInterfaceCache = copy._DisableInterfaceCache;
        this._AppDomainManagerAssembly = copy.AppDomainManagerAssembly;
        this._AppDomainManagerType = copy.AppDomainManagerType;
        this._AptcaVisibleAssemblies = copy.PartialTrustVisibleAssemblies;
        if (copy._CompatFlags != null)
          this.SetCompatibilitySwitches((IEnumerable<string>) copy._CompatFlags.Keys);
        if (copy._AppDomainSortingSetupInfo != null)
          this._AppDomainSortingSetupInfo = new AppDomainSortingSetupInfo(copy._AppDomainSortingSetupInfo);
        this._TargetFrameworkName = copy._TargetFrameworkName;
        this._UseRandomizedStringHashing = copy._UseRandomizedStringHashing;
      }
      else
        this._LoaderOptimization = LoaderOptimization.NotSpecified;
    }

    /// <summary>初始化 <see cref="T:System.AppDomainSetup" /> 类的新实例。</summary>
    public AppDomainSetup()
    {
      this._LoaderOptimization = LoaderOptimization.NotSpecified;
    }

    /// <summary>使用指定的激活上下文（用于基于清单的应用程序域激活）初始化 <see cref="T:System.AppDomainSetup" /> 类的新实例。</summary>
    /// <param name="activationContext">要用于应用程序域的激活上下文。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="activationContext" /> 为 null。</exception>
    public AppDomainSetup(ActivationContext activationContext)
      : this(new ActivationArguments(activationContext))
    {
    }

    /// <summary>使用基于清单的应用程序域激活所需的指定激活参数初始化 <see cref="T:System.AppDomainSetup" /> 类的新实例。</summary>
    /// <param name="activationArguments">一个对象，指定以基于清单的方式激活新的应用程序域所需的信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="activationArguments" /> 为 null。</exception>
    [SecuritySafeCritical]
    public AppDomainSetup(ActivationArguments activationArguments)
    {
      if (activationArguments == null)
        throw new ArgumentNullException("activationArguments");
      this._LoaderOptimization = LoaderOptimization.NotSpecified;
      this.ActivationArguments = activationArguments;
      string entryPointFullPath = CmsUtils.GetEntryPointFullPath(activationArguments);
      if (!string.IsNullOrEmpty(entryPointFullPath))
        this.SetupDefaults(entryPointFullPath, false);
      else
        this.ApplicationBase = activationArguments.ActivationContext.ApplicationDirectory;
    }

    internal void SetupDefaults(string imageLocation, bool imageLocationAlreadyNormalized = false)
    {
      char[] anyOf = new char[2]{ '\\', '/' };
      int num = imageLocation.LastIndexOfAny(anyOf);
      if (num == -1)
      {
        this.ApplicationName = imageLocation;
      }
      else
      {
        this.ApplicationName = imageLocation.Substring(num + 1);
        string str = imageLocation.Substring(0, num + 1);
        if (imageLocationAlreadyNormalized)
          this.Value[0] = str;
        else
          this.ApplicationBase = str;
      }
      this.ConfigurationFile = this.ApplicationName + AppDomainSetup.ConfigurationExtension;
    }

    internal string GetUnsecureApplicationBase()
    {
      return this.Value[0];
    }

    private string NormalizePath(string path, bool useAppBase)
    {
      if (path == null)
        return (string) null;
      if (!useAppBase)
        path = URLString.PreProcessForExtendedPathRemoval(path, false);
      int length1 = path.Length;
      if (length1 == 0)
        return (string) null;
      bool flag1 = false;
      if (length1 > 7 && string.Compare(path, 0, "file:", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
      {
        int startIndex;
        if ((int) path[6] == 92)
        {
          if ((int) path[7] == 92 || (int) path[7] == 47)
          {
            if (length1 > 8 && ((int) path[8] == 92 || (int) path[8] == 47))
              throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPathChars"));
            startIndex = 8;
          }
          else
          {
            startIndex = 5;
            flag1 = true;
          }
        }
        else if ((int) path[7] == 47)
        {
          startIndex = 8;
        }
        else
        {
          if (length1 > 8 && (int) path[7] == 92 && (int) path[8] == 92)
          {
            startIndex = 7;
          }
          else
          {
            startIndex = 5;
            StringBuilder stringBuilder = new StringBuilder(length1);
            for (int index = 0; index < length1; ++index)
            {
              char ch = path[index];
              if ((int) ch == 47)
                stringBuilder.Append('\\');
              else
                stringBuilder.Append(ch);
            }
            path = stringBuilder.ToString();
          }
          flag1 = true;
        }
        path = path.Substring(startIndex);
        length1 -= startIndex;
      }
      bool flag2;
      if (flag1 || length1 > 1 && ((int) path[0] == 47 || (int) path[0] == 92) && ((int) path[1] == 47 || (int) path[1] == 92))
      {
        flag2 = false;
      }
      else
      {
        int index = path.IndexOf(':') + 1;
        flag2 = index == 0 || length1 <= index + 1 || (int) path[index] != 47 && (int) path[index] != 92 || (int) path[index + 1] != 47 && (int) path[index + 1] != 92;
      }
      if (flag2)
      {
        if (useAppBase && (length1 == 1 || (int) path[1] != 58))
        {
          string path1 = this.Value[0];
          if (path1 == null || path1.Length == 0)
            throw new MemberAccessException(Environment.GetResourceString("AppDomain_AppBaseNotSet"));
          StringBuilder sb = StringBuilderCache.Acquire(16);
          bool flag3 = false;
          if ((int) path[0] == 47 || (int) path[0] == 92)
          {
            string str = Path.GetPathRoot(path1);
            if (str.Length == 0)
            {
              int num = path1.IndexOf(":/", StringComparison.Ordinal);
              if (num == -1)
                num = path1.IndexOf(":\\", StringComparison.Ordinal);
              int length2 = path1.Length;
              int length3 = num + 1;
              while (length3 < length2 && ((int) path1[length3] == 47 || (int) path1[length3] == 92))
                ++length3;
              while (length3 < length2 && (int) path1[length3] != 47 && (int) path1[length3] != 92)
                ++length3;
              str = path1.Substring(0, length3);
            }
            sb.Append(str);
            flag3 = true;
          }
          else
            sb.Append(path1);
          int startIndex = sb.Length - 1;
          if ((int) sb[startIndex] != 47 && (int) sb[startIndex] != 92)
          {
            if (!flag3)
            {
              if (path1.IndexOf(":/", StringComparison.Ordinal) == -1)
                sb.Append('\\');
              else
                sb.Append('/');
            }
          }
          else if (flag3)
            sb.Remove(startIndex, 1);
          sb.Append(path);
          path = StringBuilderCache.GetStringAndRelease(sb);
        }
        else
          path = Path.GetFullPathInternal(path);
      }
      return path;
    }

    private bool IsFilePath(string path)
    {
      if ((int) path[1] == 58)
        return true;
      if ((int) path[0] == 92)
        return (int) path[1] == 92;
      return false;
    }

    /// <summary>返回由 <see cref="M:System.AppDomainSetup.SetConfigurationBytes(System.Byte[])" /> 方法设置的 XML 配置信息，这些信息优先于应用程序的 XML 配置信息。</summary>
    /// <returns>一个数组，其中包含由 <see cref="M:System.AppDomainSetup.SetConfigurationBytes(System.Byte[])" /> 方法设置的 XML 配置信息；如果未调用 <see cref="M:System.AppDomainSetup.SetConfigurationBytes(System.Byte[])" /> 方法，则为 null。</returns>
    /// <filterpriority>1</filterpriority>
    public byte[] GetConfigurationBytes()
    {
      if (this._ConfigurationBytes == null)
        return (byte[]) null;
      return (byte[]) this._ConfigurationBytes.Clone();
    }

    /// <summary>提供应用程序域的 XML 配置信息，并替换应用程序的 XML 配置信息。</summary>
    /// <param name="value">一个数组，其中包含要用于应用程序域的 XML 配置信息。</param>
    /// <filterpriority>1</filterpriority>
    public void SetConfigurationBytes(byte[] value)
    {
      this._ConfigurationBytes = value;
    }

    internal Dictionary<string, object> GetCompatibilityFlags()
    {
      return this._CompatFlags;
    }

    /// <summary>设置指定的开关，从而使应用程序域针对指定问题与早期版本的 .NET Framework 兼容。</summary>
    /// <param name="switches">一组用于指定兼容性开关的可枚举字符串值；或者为 null，表示清除现有的兼容性开关。</param>
    public void SetCompatibilitySwitches(IEnumerable<string> switches)
    {
      if (this._AppDomainSortingSetupInfo != null)
      {
        this._AppDomainSortingSetupInfo._useV2LegacySorting = false;
        this._AppDomainSortingSetupInfo._useV4LegacySorting = false;
      }
      this._UseRandomizedStringHashing = false;
      if (switches != null)
      {
        this._CompatFlags = new Dictionary<string, object>();
        foreach (string @switch in switches)
        {
          if (StringComparer.OrdinalIgnoreCase.Equals("NetFx40_Legacy20SortingBehavior", @switch))
          {
            if (this._AppDomainSortingSetupInfo == null)
              this._AppDomainSortingSetupInfo = new AppDomainSortingSetupInfo();
            this._AppDomainSortingSetupInfo._useV2LegacySorting = true;
          }
          if (StringComparer.OrdinalIgnoreCase.Equals("NetFx45_Legacy40SortingBehavior", @switch))
          {
            if (this._AppDomainSortingSetupInfo == null)
              this._AppDomainSortingSetupInfo = new AppDomainSortingSetupInfo();
            this._AppDomainSortingSetupInfo._useV4LegacySorting = true;
          }
          if (StringComparer.OrdinalIgnoreCase.Equals("UseRandomizedStringHashAlgorithm", @switch))
            this._UseRandomizedStringHashing = true;
          this._CompatFlags.Add(@switch, (object) null);
        }
      }
      else
        this._CompatFlags = (Dictionary<string, object>) null;
    }

    /// <summary>向通用语言运行时提供备用字符串比较功能实现。</summary>
    /// <param name="functionName">要重载的字符串比较函数的名称。</param>
    /// <param name="functionVersion">函数版本。.NET Framework 4.5，必须为 1 或者 更大的数值。</param>
    /// <param name="functionPointer">重写 <paramref name="functionName" /> 函数的指针。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="functionName" /> 为 null。 </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="functionVersion" /> 不是 1 或者更大的值。- 或 -<paramref name="functionPointer" /> 为 <see cref="F:System.IntPtr.Zero" />。</exception>
    [SecurityCritical]
    public void SetNativeFunction(string functionName, int functionVersion, IntPtr functionPointer)
    {
      if (functionName == null)
        throw new ArgumentNullException("functionName");
      if (functionPointer == IntPtr.Zero)
        throw new ArgumentNullException("functionPointer");
      if (string.IsNullOrWhiteSpace(functionName))
        throw new ArgumentException(Environment.GetResourceString("Argument_NPMSInvalidName"), "functionName");
      if (functionVersion < 1)
        throw new ArgumentException(Environment.GetResourceString("ArgumentException_MinSortingVersion", (object) 1, (object) functionName));
      if (this._AppDomainSortingSetupInfo == null)
        this._AppDomainSortingSetupInfo = new AppDomainSortingSetupInfo();
      if (string.Equals(functionName, "IsNLSDefinedString", StringComparison.OrdinalIgnoreCase))
        this._AppDomainSortingSetupInfo._pfnIsNLSDefinedString = functionPointer;
      if (string.Equals(functionName, "CompareStringEx", StringComparison.OrdinalIgnoreCase))
        this._AppDomainSortingSetupInfo._pfnCompareStringEx = functionPointer;
      if (string.Equals(functionName, "LCMapStringEx", StringComparison.OrdinalIgnoreCase))
        this._AppDomainSortingSetupInfo._pfnLCMapStringEx = functionPointer;
      if (string.Equals(functionName, "FindNLSStringEx", StringComparison.OrdinalIgnoreCase))
        this._AppDomainSortingSetupInfo._pfnFindNLSStringEx = functionPointer;
      if (string.Equals(functionName, "CompareStringOrdinal", StringComparison.OrdinalIgnoreCase))
        this._AppDomainSortingSetupInfo._pfnCompareStringOrdinal = functionPointer;
      if (string.Equals(functionName, "GetNLSVersionEx", StringComparison.OrdinalIgnoreCase))
        this._AppDomainSortingSetupInfo._pfnGetNLSVersionEx = functionPointer;
      if (!string.Equals(functionName, "FindStringOrdinal", StringComparison.OrdinalIgnoreCase))
        return;
      this._AppDomainSortingSetupInfo._pfnFindStringOrdinal = functionPointer;
    }

    [SecurityCritical]
    private string VerifyDir(string dir, bool normalize)
    {
      if (dir != null)
      {
        if (dir.Length == 0)
        {
          dir = (string) null;
        }
        else
        {
          if (normalize)
            dir = this.NormalizePath(dir, true);
          if (this.IsFilePath(dir))
            new FileIOPermission(FileIOPermissionAccess.PathDiscovery, dir).Demand();
        }
      }
      return dir;
    }

    [SecurityCritical]
    private void VerifyDirList(string dirs)
    {
      if (dirs == null)
        return;
      string[] strArray = dirs.Split(';');
      int length = strArray.Length;
      for (int index = 0; index < length; ++index)
        this.VerifyDir(strArray[index], true);
    }

    internal ApplicationTrust InternalGetApplicationTrust()
    {
      if (this._ApplicationTrust == null)
        return (ApplicationTrust) null;
      SecurityElement securityElement = SecurityElement.FromString(this._ApplicationTrust);
      ApplicationTrust applicationTrust = new ApplicationTrust();
      SecurityElement element = securityElement;
      applicationTrust.FromXml(element);
      return applicationTrust;
    }

    internal void InternalSetApplicationTrust(ApplicationTrust value)
    {
      if (value != null)
        this._ApplicationTrust = value.ToXml().ToString();
      else
        this._ApplicationTrust = (string) null;
    }

    [SecurityCritical]
    internal bool UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation FieldValue, string FieldKey, string UpdatedField, IntPtr fusionContext, AppDomainSetup oldADS)
    {
      string str1 = this.Value[(int) FieldValue];
      string str2 = oldADS == null ? (string) null : oldADS.Value[(int) FieldValue];
      if (!(str1 != str2))
        return false;
      AppDomainSetup.UpdateContextProperty(fusionContext, FieldKey, UpdatedField == null ? (object) str1 : (object) UpdatedField);
      return true;
    }

    [SecurityCritical]
    internal void UpdateBooleanContextPropertyIfNeeded(AppDomainSetup.LoaderInformation FieldValue, string FieldKey, IntPtr fusionContext, AppDomainSetup oldADS)
    {
      if (this.Value[(int) FieldValue] != null)
      {
        AppDomainSetup.UpdateContextProperty(fusionContext, FieldKey, (object) "true");
      }
      else
      {
        if (oldADS == null || oldADS.Value[(int) FieldValue] == null)
          return;
        AppDomainSetup.UpdateContextProperty(fusionContext, FieldKey, (object) "false");
      }
    }

    [SecurityCritical]
    internal static bool ByteArraysAreDifferent(byte[] A, byte[] B)
    {
      int length = A.Length;
      if (length != B.Length)
        return true;
      for (int index = 0; index < length; ++index)
      {
        if ((int) A[index] != (int) B[index])
          return true;
      }
      return false;
    }

    [SecurityCritical]
    internal static void UpdateByteArrayContextPropertyIfNeeded(byte[] NewArray, byte[] OldArray, string FieldKey, IntPtr fusionContext)
    {
      if ((NewArray == null || OldArray != null) && (NewArray != null || OldArray == null) && (NewArray == null || OldArray == null || !AppDomainSetup.ByteArraysAreDifferent(NewArray, OldArray)))
        return;
      AppDomainSetup.UpdateContextProperty(fusionContext, FieldKey, (object) NewArray);
    }

    [SecurityCritical]
    internal void SetupFusionContext(IntPtr fusionContext, AppDomainSetup oldADS)
    {
      this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.ApplicationBaseValue, AppDomainSetup.ApplicationBaseKey, (string) null, fusionContext, oldADS);
      this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.PrivateBinPathValue, AppDomainSetup.PrivateBinPathKey, (string) null, fusionContext, oldADS);
      this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.DevPathValue, AppDomainSetup.DeveloperPathKey, (string) null, fusionContext, oldADS);
      this.UpdateBooleanContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.DisallowPublisherPolicyValue, AppDomainSetup.DisallowPublisherPolicyKey, fusionContext, oldADS);
      this.UpdateBooleanContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.DisallowCodeDownloadValue, AppDomainSetup.DisallowCodeDownloadKey, fusionContext, oldADS);
      this.UpdateBooleanContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.DisallowBindingRedirectsValue, AppDomainSetup.DisallowBindingRedirectsKey, fusionContext, oldADS);
      this.UpdateBooleanContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.DisallowAppBaseProbingValue, AppDomainSetup.DisallowAppBaseProbingKey, fusionContext, oldADS);
      if (this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.ShadowCopyFilesValue, AppDomainSetup.ShadowCopyFilesKey, this.ShadowCopyFiles, fusionContext, oldADS))
      {
        if (this.Value[7] == null)
          this.ShadowCopyDirectories = this.BuildShadowCopyDirectories();
        this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.ShadowCopyDirectoriesValue, AppDomainSetup.ShadowCopyDirectoriesKey, (string) null, fusionContext, oldADS);
      }
      this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.CachePathValue, AppDomainSetup.CachePathKey, (string) null, fusionContext, oldADS);
      this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.PrivateBinPathProbeValue, AppDomainSetup.PrivateBinPathProbeKey, this.PrivateBinPathProbe, fusionContext, oldADS);
      this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.ConfigurationFileValue, AppDomainSetup.ConfigurationFileKey, (string) null, fusionContext, oldADS);
      AppDomainSetup.UpdateByteArrayContextPropertyIfNeeded(this._ConfigurationBytes, oldADS == null ? (byte[]) null : oldADS.GetConfigurationBytes(), AppDomainSetup.ConfigurationBytesKey, fusionContext);
      this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.ApplicationNameValue, AppDomainSetup.ApplicationNameKey, this.ApplicationName, fusionContext, oldADS);
      this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.DynamicBaseValue, AppDomainSetup.DynamicBaseKey, (string) null, fusionContext, oldADS);
      AppDomainSetup.UpdateContextProperty(fusionContext, AppDomainSetup.MachineConfigKey, (object) (RuntimeEnvironment.GetRuntimeDirectoryImpl() + AppDomainSetup.RuntimeConfigurationFile));
      string hostBindingFile = RuntimeEnvironment.GetHostBindingFile();
      if (hostBindingFile == null && oldADS == null)
        return;
      AppDomainSetup.UpdateContextProperty(fusionContext, AppDomainSetup.HostBindingKey, (object) hostBindingFile);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void UpdateContextProperty(IntPtr fusionContext, string key, object value);

    internal static int Locate(string s)
    {
      if (string.IsNullOrEmpty(s))
        return -1;
      char ch = s[0];
      if ((uint) ch <= 76U)
      {
        switch (ch)
        {
          case 'A':
            if (s == "APP_CONFIG_FILE")
              return 1;
            if (s == "APP_NAME")
              return 4;
            if (s == "APPBASE")
              return 0;
            if (s == "APP_CONFIG_BLOB")
              return 15;
            break;
          case 'B':
            if (s == "BINPATH_PROBE_ONLY")
              return 6;
            break;
          case 'C':
            if (s == "CACHE_BASE")
              return 9;
            if (s == "CODE_DOWNLOAD_DISABLED")
              return 12;
            break;
          case 'D':
            if (s == "DEV_PATH")
              return 3;
            if (s == "DYNAMIC_BASE")
              return 2;
            if (s == "DISALLOW_APP")
              return 11;
            if (s == "DISALLOW_APP_REDIRECTS")
              return 13;
            if (s == "DISALLOW_APP_BASE_PROBING")
              return 14;
            break;
          case 'F':
            if (s == "FORCE_CACHE_INSTALL")
              return 8;
            break;
          case 'L':
            if (s == "LICENSE_FILE")
              return 10;
            break;
        }
      }
      else if ((int) ch != 80)
      {
        if ((int) ch == 83 && s == "SHADOW_COPY_DIRS")
          return 7;
      }
      else if (s == "PRIVATE_BINPATH")
        return 5;
      return -1;
    }

    private string BuildShadowCopyDirectories()
    {
      string str1 = this.Value[5];
      if (str1 == null)
        return (string) null;
      StringBuilder sb = StringBuilderCache.Acquire(16);
      string str2 = this.Value[0];
      if (str2 != null)
      {
        char[] chArray = new char[1]{ ';' };
        string[] strArray = str1.Split(chArray);
        int length = strArray.Length;
        string str3 = str2;
        int index1 = str3.Length - 1;
        int num;
        if ((int) str3[index1] != 47)
        {
          string str4 = str2;
          int index2 = str4.Length - 1;
          num = (int) str4[index2] != 92 ? 1 : 0;
        }
        else
          num = 0;
        bool flag = num != 0;
        if (length == 0)
        {
          sb.Append(str2);
          if (flag)
            sb.Append('\\');
          sb.Append(str1);
        }
        else
        {
          for (int index2 = 0; index2 < length; ++index2)
          {
            sb.Append(str2);
            if (flag)
              sb.Append('\\');
            sb.Append(strArray[index2]);
            if (index2 < length - 1)
              sb.Append(';');
          }
        }
      }
      return StringBuilderCache.GetStringAndRelease(sb);
    }

    [Serializable]
    internal enum LoaderInformation
    {
      ApplicationBaseValue = 0,
      ConfigurationFileValue = 1,
      DynamicBaseValue = 2,
      DevPathValue = 3,
      ApplicationNameValue = 4,
      PrivateBinPathValue = 5,
      PrivateBinPathProbeValue = 6,
      ShadowCopyDirectoriesValue = 7,
      ShadowCopyFilesValue = 8,
      CachePathValue = 9,
      LicenseFileValue = 10,
      DisallowPublisherPolicyValue = 11,
      DisallowCodeDownloadValue = 12,
      DisallowBindingRedirectsValue = 13,
      DisallowAppBaseProbingValue = 14,
      ConfigurationBytesValue = 15,
      LoaderMaximum = 18,
    }
  }
}
