// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyName
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Reflection
{
  /// <summary>完整描述程序集的唯一标识。</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_AssemblyName))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class AssemblyName : _AssemblyName, ICloneable, ISerializable, IDeserializationCallback
  {
    private string _Name;
    private byte[] _PublicKey;
    private byte[] _PublicKeyToken;
    private CultureInfo _CultureInfo;
    private string _CodeBase;
    private Version _Version;
    private StrongNameKeyPair _StrongNameKeyPair;
    private SerializationInfo m_siInfo;
    private byte[] _HashForControl;
    private AssemblyHashAlgorithm _HashAlgorithm;
    private AssemblyHashAlgorithm _HashAlgorithmForControl;
    private AssemblyVersionCompatibility _VersionCompatibility;
    private AssemblyNameFlags _Flags;

    /// <summary>获取或设置程序集的简单名称。这通常（但不一定）是程序集的清单文件的文件名，不包括其扩展名。</summary>
    /// <returns>程序集的简单名称。</returns>
    [__DynamicallyInvokable]
    public string Name
    {
      [__DynamicallyInvokable] get
      {
        return this._Name;
      }
      [__DynamicallyInvokable] set
      {
        this._Name = value;
      }
    }

    /// <summary>获取或设置程序集的主版本号、次版本号、内部版本号和修订号。</summary>
    /// <returns>一个对象，表示程序集的主版本号、次版本号、内部版本号和修订号。</returns>
    [__DynamicallyInvokable]
    public Version Version
    {
      [__DynamicallyInvokable] get
      {
        return this._Version;
      }
      [__DynamicallyInvokable] set
      {
        this._Version = value;
      }
    }

    /// <summary>获取或设置程序集支持的区域性。</summary>
    /// <returns>一个对象，它表示程序集支持的区域性。</returns>
    [__DynamicallyInvokable]
    public CultureInfo CultureInfo
    {
      [__DynamicallyInvokable] get
      {
        return this._CultureInfo;
      }
      [__DynamicallyInvokable] set
      {
        this._CultureInfo = value;
      }
    }

    /// <summary>获取或设置与此程序集关联的区域性名称。</summary>
    /// <returns>区域性名称。</returns>
    [__DynamicallyInvokable]
    public string CultureName
    {
      [__DynamicallyInvokable] get
      {
        if (this._CultureInfo != null)
          return this._CultureInfo.Name;
        return (string) null;
      }
      [__DynamicallyInvokable] set
      {
        this._CultureInfo = value == null ? (CultureInfo) null : new CultureInfo(value);
      }
    }

    /// <summary>获取或设置程序集的 URL 位置。</summary>
    /// <returns>一个字符串，它是程序集的 URL 位置。</returns>
    public string CodeBase
    {
      get
      {
        return this._CodeBase;
      }
      set
      {
        this._CodeBase = value;
      }
    }

    /// <summary>获取 URI，包括表示基本代码的转义符。</summary>
    /// <returns>带有转义符的 URI。</returns>
    public string EscapedCodeBase
    {
      [SecuritySafeCritical] get
      {
        if (this._CodeBase == null)
          return (string) null;
        return AssemblyName.EscapeCodeBase(this._CodeBase);
      }
    }

    /// <summary>获取或设置一个值，该值标识可执行文件的目标平台的处理器和每字位数。</summary>
    /// <returns>枚举值之一，标识可执行文件的目标平台的处理器和每字位数。</returns>
    [__DynamicallyInvokable]
    public ProcessorArchitecture ProcessorArchitecture
    {
      [__DynamicallyInvokable] get
      {
        int num = (int) (this._Flags & (AssemblyNameFlags) 112) >> 4;
        if (num > 5)
          num = 0;
        return (ProcessorArchitecture) num;
      }
      [__DynamicallyInvokable] set
      {
        int num = (int) (value & (ProcessorArchitecture.IA64 | ProcessorArchitecture.Amd64));
        if (num > 5)
          return;
        this._Flags = (AssemblyNameFlags) ((long) this._Flags & 4294967055L);
        this._Flags = this._Flags | (AssemblyNameFlags) (num << 4);
      }
    }

    /// <summary>获取或设置指示程序集包含的内容类型的值。</summary>
    /// <returns>指示程序集包含哪种内容类型的值。</returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public AssemblyContentType ContentType
    {
      [__DynamicallyInvokable] get
      {
        int num = (int) (this._Flags & (AssemblyNameFlags) 3584) >> 9;
        if (num > 1)
          num = 0;
        return (AssemblyContentType) num;
      }
      [__DynamicallyInvokable] set
      {
        int num = (int) (value & (AssemblyContentType) 7);
        if (num > 1)
          return;
        this._Flags = (AssemblyNameFlags) ((long) this._Flags & 4294963711L);
        this._Flags = this._Flags | (AssemblyNameFlags) (num << 9);
      }
    }

    /// <summary>获取或设置该程序集的属性。</summary>
    /// <returns>表示程序集特性的值。</returns>
    [__DynamicallyInvokable]
    public AssemblyNameFlags Flags
    {
      [__DynamicallyInvokable] get
      {
        return this._Flags & (AssemblyNameFlags) -3825;
      }
      [__DynamicallyInvokable] set
      {
        this._Flags = this._Flags & (AssemblyNameFlags) 3824;
        this._Flags = this._Flags | value & (AssemblyNameFlags) -3825;
      }
    }

    /// <summary>获取或设置程序集清单使用的哈希算法。</summary>
    /// <returns>程序集清单使用的哈希算法。</returns>
    public AssemblyHashAlgorithm HashAlgorithm
    {
      get
      {
        return this._HashAlgorithm;
      }
      set
      {
        this._HashAlgorithm = value;
      }
    }

    /// <summary>获取或设置与程序集同其他程序集的兼容性相关的信息。</summary>
    /// <returns>一个值，表示有关程序集同其他程序集的兼容性的信息。</returns>
    public AssemblyVersionCompatibility VersionCompatibility
    {
      get
      {
        return this._VersionCompatibility;
      }
      set
      {
        this._VersionCompatibility = value;
      }
    }

    /// <summary>获取或设置用于为程序集创建强名称签名的加密公钥/私钥对。</summary>
    /// <returns>要用于为程序集创建强名称的加密公钥/私钥对。</returns>
    public StrongNameKeyPair KeyPair
    {
      get
      {
        return this._StrongNameKeyPair;
      }
      set
      {
        this._StrongNameKeyPair = value;
      }
    }

    /// <summary>获取程序集的全名（也称为显示名称）。</summary>
    /// <returns>作为程序集的全名（也称为显示名称）的字符串。</returns>
    [__DynamicallyInvokable]
    public string FullName
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        string @string = this.nToString();
        if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8 && string.IsNullOrEmpty(@string))
          return base.ToString();
        return @string;
      }
    }

    /// <summary>初始化 <see cref="T:System.Reflection.AssemblyName" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public AssemblyName()
    {
      this._HashAlgorithm = AssemblyHashAlgorithm.None;
      this._VersionCompatibility = AssemblyVersionCompatibility.SameMachine;
      this._Flags = AssemblyNameFlags.None;
    }

    internal AssemblyName(SerializationInfo info, StreamingContext context)
    {
      this.m_siInfo = info;
    }

    /// <summary>用指定的显示名称初始化 <see cref="T:System.Reflection.AssemblyName" /> 类的新实例。</summary>
    /// <param name="assemblyName">程序集的显示名称，由 <see cref="P:System.Reflection.AssemblyName.FullName" /> 属性返回。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyName" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="assemblyName" /> 是一个零长度字符串。</exception>
    /// <exception cref="T:System.IO.FileLoadException">在 .NET for Windows Store 应用程序 或 可移植类库 中，请改为捕获基类异常 <see cref="T:System.IO.IOException" />。引用的程序集未能找到或无法加载。</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public AssemblyName(string assemblyName)
    {
      if (assemblyName == null)
        throw new ArgumentNullException("assemblyName");
      if (assemblyName.Length == 0 || (int) assemblyName[0] == 0)
        throw new ArgumentException(Environment.GetResourceString("Format_StringZeroLength"));
      this._Name = assemblyName;
      this.nInit();
    }

    /// <summary>制作此 <see cref="T:System.Reflection.AssemblyName" /> 对象的副本。</summary>
    /// <returns>作为 <see cref="T:System.Reflection.AssemblyName" /> 对象副本的对象。</returns>
    public object Clone()
    {
      AssemblyName assemblyName = new AssemblyName();
      string name = this._Name;
      byte[] publicKey = this._PublicKey;
      byte[] publicKeyToken = this._PublicKeyToken;
      Version version = this._Version;
      CultureInfo cultureInfo = this._CultureInfo;
      int num1 = (int) this._HashAlgorithm;
      int num2 = (int) this._VersionCompatibility;
      string codeBase = this._CodeBase;
      int num3 = (int) this._Flags;
      StrongNameKeyPair keyPair = this._StrongNameKeyPair;
      assemblyName.Init(name, publicKey, publicKeyToken, version, cultureInfo, (AssemblyHashAlgorithm) num1, (AssemblyVersionCompatibility) num2, codeBase, (AssemblyNameFlags) num3, keyPair);
      byte[] numArray = this._HashForControl;
      assemblyName._HashForControl = numArray;
      int num4 = (int) this._HashAlgorithmForControl;
      assemblyName._HashAlgorithmForControl = (AssemblyHashAlgorithm) num4;
      return (object) assemblyName;
    }

    /// <summary>获取给定文件的 <see cref="T:System.Reflection.AssemblyName" />。</summary>
    /// <returns>表示给定的程序集文件的对象。</returns>
    /// <param name="assemblyFile">要返回其 <see cref="T:System.Reflection.AssemblyName" /> 的程序集的路径。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyFile" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="assemblyFile" /> 无效，如包含无效区域性的程序集。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到 <paramref name="assemblyFile" />。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方不具备路径发现权限。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效程序集。</exception>
    /// <exception cref="T:System.IO.FileLoadException">用两组不同的证据将一个程序集或模块加载了两次。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static AssemblyName GetAssemblyName(string assemblyFile)
    {
      if (assemblyFile == null)
        throw new ArgumentNullException("assemblyFile");
      string fullPathInternal = Path.GetFullPathInternal(assemblyFile);
      new FileIOPermission(FileIOPermissionAccess.PathDiscovery, fullPathInternal).Demand();
      return AssemblyName.nGetFileInformation(fullPathInternal);
    }

    internal void SetHashControl(byte[] hash, AssemblyHashAlgorithm hashAlgorithm)
    {
      this._HashForControl = hash;
      this._HashAlgorithmForControl = hashAlgorithm;
    }

    /// <summary>获取程序集的公钥。</summary>
    /// <returns>字节数组，包含程序集的公钥。</returns>
    /// <exception cref="T:System.Security.SecurityException">提供了公钥（例如使用 <see cref="M:System.Reflection.AssemblyName.SetPublicKey(System.Byte[])" /> 方法），但未提供公钥标记。</exception>
    [__DynamicallyInvokable]
    public byte[] GetPublicKey()
    {
      return this._PublicKey;
    }

    /// <summary>设置用于标识程序集的公钥。</summary>
    /// <param name="publicKey">字节数组，包含程序集的公钥。</param>
    [__DynamicallyInvokable]
    public void SetPublicKey(byte[] publicKey)
    {
      this._PublicKey = publicKey;
      if (publicKey == null)
        this._Flags = this._Flags & ~AssemblyNameFlags.PublicKey;
      else
        this._Flags = this._Flags | AssemblyNameFlags.PublicKey;
    }

    /// <summary>获取公钥标记，该标记为应用程序或程序集签名时所用公钥的 SHA-1 哈希值的最后 8 个字节。</summary>
    /// <returns>包含公钥调用的字节数组。</returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public byte[] GetPublicKeyToken()
    {
      if (this._PublicKeyToken == null)
        this._PublicKeyToken = this.nGetPublicKeyToken();
      return this._PublicKeyToken;
    }

    /// <summary>设置公钥标记，该标记为应用程序或程序集签名时所用公钥的 SHA-1 哈希值的最后 8 个字节。</summary>
    /// <param name="publicKeyToken">字节数组，包含程序集的公钥标记。</param>
    [__DynamicallyInvokable]
    public void SetPublicKeyToken(byte[] publicKeyToken)
    {
      this._PublicKeyToken = publicKeyToken;
    }

    /// <summary>返回程序集的全名，即所谓的显示名称。</summary>
    /// <returns>程序集的全名；如果不能确定程序集的全名，则为类名。</returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.FullName ?? base.ToString();
    }

    /// <summary>获取序列化信息，其中包含重新创建此 AssemblyName 实例所需的所有数据。</summary>
    /// <param name="info">用序列化信息填充的对象。</param>
    /// <param name="context">序列化的目标上下文。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 为 null。</exception>
    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      info.AddValue("_Name", (object) this._Name);
      info.AddValue("_PublicKey", (object) this._PublicKey, typeof (byte[]));
      info.AddValue("_PublicKeyToken", (object) this._PublicKeyToken, typeof (byte[]));
      info.AddValue("_CultureInfo", this._CultureInfo == null ? -1 : this._CultureInfo.LCID);
      info.AddValue("_CodeBase", (object) this._CodeBase);
      info.AddValue("_Version", (object) this._Version);
      info.AddValue("_HashAlgorithm", (object) this._HashAlgorithm, typeof (AssemblyHashAlgorithm));
      info.AddValue("_HashAlgorithmForControl", (object) this._HashAlgorithmForControl, typeof (AssemblyHashAlgorithm));
      info.AddValue("_StrongNameKeyPair", (object) this._StrongNameKeyPair, typeof (StrongNameKeyPair));
      info.AddValue("_VersionCompatibility", (object) this._VersionCompatibility, typeof (AssemblyVersionCompatibility));
      info.AddValue("_Flags", (object) this._Flags, typeof (AssemblyNameFlags));
      info.AddValue("_HashForControl", (object) this._HashForControl, typeof (byte[]));
    }

    /// <summary>实现 <see cref="T:System.Runtime.Serialization.ISerializable" /> 接口，并在完成反序列化后由反序列化事件回调。</summary>
    /// <param name="sender">反序列化事件源。</param>
    public void OnDeserialization(object sender)
    {
      if (this.m_siInfo == null)
        return;
      this._Name = this.m_siInfo.GetString("_Name");
      this._PublicKey = (byte[]) this.m_siInfo.GetValue("_PublicKey", typeof (byte[]));
      this._PublicKeyToken = (byte[]) this.m_siInfo.GetValue("_PublicKeyToken", typeof (byte[]));
      int int32 = this.m_siInfo.GetInt32("_CultureInfo");
      if (int32 != -1)
        this._CultureInfo = new CultureInfo(int32);
      this._CodeBase = this.m_siInfo.GetString("_CodeBase");
      this._Version = (Version) this.m_siInfo.GetValue("_Version", typeof (Version));
      this._HashAlgorithm = (AssemblyHashAlgorithm) this.m_siInfo.GetValue("_HashAlgorithm", typeof (AssemblyHashAlgorithm));
      this._StrongNameKeyPair = (StrongNameKeyPair) this.m_siInfo.GetValue("_StrongNameKeyPair", typeof (StrongNameKeyPair));
      this._VersionCompatibility = (AssemblyVersionCompatibility) this.m_siInfo.GetValue("_VersionCompatibility", typeof (AssemblyVersionCompatibility));
      this._Flags = (AssemblyNameFlags) this.m_siInfo.GetValue("_Flags", typeof (AssemblyNameFlags));
      try
      {
        this._HashAlgorithmForControl = (AssemblyHashAlgorithm) this.m_siInfo.GetValue("_HashAlgorithmForControl", typeof (AssemblyHashAlgorithm));
        this._HashForControl = (byte[]) this.m_siInfo.GetValue("_HashForControl", typeof (byte[]));
      }
      catch (SerializationException ex)
      {
        this._HashAlgorithmForControl = AssemblyHashAlgorithm.None;
        this._HashForControl = (byte[]) null;
      }
      this.m_siInfo = (SerializationInfo) null;
    }

    /// <summary>返回指示两个程序集名称是否相同的值。该比较基于简单的程序集名称。</summary>
    /// <returns>如果简单程序集名称相同，则为 true；否则为 false。</returns>
    /// <param name="reference">引用程序集名称。</param>
    /// <param name="definition">与引用程序集进行比较的程序集名称。</param>
    [SecuritySafeCritical]
    public static bool ReferenceMatchesDefinition(AssemblyName reference, AssemblyName definition)
    {
      if (reference == definition)
        return true;
      return AssemblyName.ReferenceMatchesDefinitionInternal(reference, definition, true);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool ReferenceMatchesDefinitionInternal(AssemblyName reference, AssemblyName definition, bool parse);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal void nInit(out RuntimeAssembly assembly, bool forIntrospection, bool raiseResolveEvent);

    [SecurityCritical]
    internal void nInit()
    {
      RuntimeAssembly assembly = (RuntimeAssembly) null;
      this.nInit(out assembly, false, false);
    }

    internal void SetProcArchIndex(PortableExecutableKinds pek, ImageFileMachine ifm)
    {
      this.ProcessorArchitecture = AssemblyName.CalculateProcArchIndex(pek, ifm, this._Flags);
    }

    internal static ProcessorArchitecture CalculateProcArchIndex(PortableExecutableKinds pek, ImageFileMachine ifm, AssemblyNameFlags flags)
    {
      if ((flags & (AssemblyNameFlags) 240) == (AssemblyNameFlags) 112)
        return ProcessorArchitecture.None;
      if ((pek & PortableExecutableKinds.PE32Plus) == PortableExecutableKinds.PE32Plus)
      {
        if (ifm != ImageFileMachine.I386)
        {
          if (ifm == ImageFileMachine.IA64)
            return ProcessorArchitecture.IA64;
          if (ifm == ImageFileMachine.AMD64)
            return ProcessorArchitecture.Amd64;
        }
        else if ((pek & PortableExecutableKinds.ILOnly) == PortableExecutableKinds.ILOnly)
          return ProcessorArchitecture.MSIL;
      }
      else
      {
        if (ifm == ImageFileMachine.I386)
          return (pek & PortableExecutableKinds.Required32Bit) == PortableExecutableKinds.Required32Bit || (pek & PortableExecutableKinds.ILOnly) != PortableExecutableKinds.ILOnly ? ProcessorArchitecture.X86 : ProcessorArchitecture.MSIL;
        if (ifm == ImageFileMachine.ARM)
          return ProcessorArchitecture.Arm;
      }
      return ProcessorArchitecture.None;
    }

    internal void Init(string name, byte[] publicKey, byte[] publicKeyToken, Version version, CultureInfo cultureInfo, AssemblyHashAlgorithm hashAlgorithm, AssemblyVersionCompatibility versionCompatibility, string codeBase, AssemblyNameFlags flags, StrongNameKeyPair keyPair)
    {
      this._Name = name;
      if (publicKey != null)
      {
        this._PublicKey = new byte[publicKey.Length];
        Array.Copy((Array) publicKey, (Array) this._PublicKey, publicKey.Length);
      }
      if (publicKeyToken != null)
      {
        this._PublicKeyToken = new byte[publicKeyToken.Length];
        Array.Copy((Array) publicKeyToken, (Array) this._PublicKeyToken, publicKeyToken.Length);
      }
      if (version != (Version) null)
        this._Version = (Version) version.Clone();
      this._CultureInfo = cultureInfo;
      this._HashAlgorithm = hashAlgorithm;
      this._VersionCompatibility = versionCompatibility;
      this._CodeBase = codeBase;
      this._Flags = flags;
      this._StrongNameKeyPair = keyPair;
    }

    void _AssemblyName.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _AssemblyName.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _AssemblyName.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _AssemblyName.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }

    internal string GetNameWithPublicKey()
    {
      return this.Name + ", PublicKey=" + Hex.EncodeHexString(this.GetPublicKey());
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern AssemblyName nGetFileInformation(string s);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private string nToString();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private byte[] nGetPublicKeyToken();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string EscapeCodeBase(string codeBase);
  }
}
