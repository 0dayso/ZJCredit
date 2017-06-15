// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TypeLibConverter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices.TCEAdapterGen;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.InteropServices
{
  /// <summary>提供一组服务，将托管程序集转换为 COM 类型库或进行反向转换。</summary>
  [Guid("F1C3BF79-C3E4-11d3-88E7-00902754C43A")]
  [ClassInterface(ClassInterfaceType.None)]
  [ComVisible(true)]
  public sealed class TypeLibConverter : ITypeLibConverter
  {
    private const string s_strTypeLibAssemblyTitlePrefix = "TypeLib ";
    private const string s_strTypeLibAssemblyDescPrefix = "Assembly generated from typelib ";
    private const int MAX_NAMESPACE_LENGTH = 1024;

    /// <summary>将 COM 类型库转换为程序集。</summary>
    /// <returns>包含已转换类型库的 <see cref="T:System.Reflection.Emit.AssemblyBuilder" /> 对象。</returns>
    /// <param name="typeLib">实现 ITypeLib 接口的对象。</param>
    /// <param name="asmFileName">所产生的程序集的文件名。</param>
    /// <param name="flags">指示任何特殊设置的 <see cref="T:System.Runtime.InteropServices.TypeLibImporterFlags" /> 值。</param>
    /// <param name="notifySink">由调用方实现的 <see cref="T:System.Runtime.InteropServices.ITypeLibImporterNotifySink" /> 接口。</param>
    /// <param name="publicKey">包含公钥的 byte 数组。</param>
    /// <param name="keyPair">包含加密公钥和私钥对的 <see cref="T:System.Reflection.StrongNameKeyPair" /> 对象。</param>
    /// <param name="unsafeInterfaces">如果为 true，则接口要求在链接时检查 <see cref="F:System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode" /> 权限。如果为 false，则接口要求在运行时检查，运行时检查需要堆栈审核且更加昂贵，但有助于提供更大的保护。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeLib" /> 为 null。- 或 -<paramref name="asmFileName" /> 为 null。- 或 -<paramref name="notifySink" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="asmFileName" /> 是空字符串。- 或 -<paramref name="asmFileName" /> 的长度超过 MAX_PATH。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="flags" /> 不是 <see cref="F:System.Runtime.InteropServices.TypeLibImporterFlags.PrimaryInteropAssembly" />。- 或 -<paramref name="publicKey" /> 和 <paramref name="keyPair" /> 为 null。</exception>
    /// <exception cref="T:System.Reflection.ReflectionTypeLoadException">产生的元数据存在错误，妨碍加载任何类型。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public AssemblyBuilder ConvertTypeLibToAssembly([MarshalAs(UnmanagedType.Interface)] object typeLib, string asmFileName, int flags, ITypeLibImporterNotifySink notifySink, byte[] publicKey, StrongNameKeyPair keyPair, bool unsafeInterfaces)
    {
      return this.ConvertTypeLibToAssembly(typeLib, asmFileName, unsafeInterfaces ? TypeLibImporterFlags.UnsafeInterfaces : TypeLibImporterFlags.None, notifySink, publicKey, keyPair, (string) null, (Version) null);
    }

    /// <summary>将 COM 类型库转换为程序集。</summary>
    /// <returns>包含已转换类型库的 <see cref="T:System.Reflection.Emit.AssemblyBuilder" /> 对象。</returns>
    /// <param name="typeLib">实现 ITypeLib 接口的对象。</param>
    /// <param name="asmFileName">所产生的程序集的文件名。</param>
    /// <param name="flags">指示任何特殊设置的 <see cref="T:System.Runtime.InteropServices.TypeLibImporterFlags" /> 值。</param>
    /// <param name="notifySink">由调用方实现的 <see cref="T:System.Runtime.InteropServices.ITypeLibImporterNotifySink" /> 接口。</param>
    /// <param name="publicKey">包含公钥的 byte 数组。</param>
    /// <param name="keyPair">包含加密公钥和私钥对的 <see cref="T:System.Reflection.StrongNameKeyPair" /> 对象。</param>
    /// <param name="asmNamespace">所产生的程序集的命名空间。</param>
    /// <param name="asmVersion">所产生的程序集的版本。如果为 null，则使用类型库的版本。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeLib" /> 为 null。- 或 -<paramref name="asmFileName" /> 为 null。- 或 -<paramref name="notifySink" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="asmFileName" /> 是空字符串。- 或 -<paramref name="asmFileName" /> 的长度超过 MAX_PATH。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="flags" /> 不是 <see cref="F:System.Runtime.InteropServices.TypeLibImporterFlags.PrimaryInteropAssembly" />。- 或 -<paramref name="publicKey" /> 和 <paramref name="keyPair" /> 为 null。</exception>
    /// <exception cref="T:System.Reflection.ReflectionTypeLoadException">产生的元数据存在错误，妨碍加载任何类型。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public AssemblyBuilder ConvertTypeLibToAssembly([MarshalAs(UnmanagedType.Interface)] object typeLib, string asmFileName, TypeLibImporterFlags flags, ITypeLibImporterNotifySink notifySink, byte[] publicKey, StrongNameKeyPair keyPair, string asmNamespace, Version asmVersion)
    {
      if (typeLib == null)
        throw new ArgumentNullException("typeLib");
      if (asmFileName == null)
        throw new ArgumentNullException("asmFileName");
      if (notifySink == null)
        throw new ArgumentNullException("notifySink");
      if (string.Empty.Equals(asmFileName))
        throw new ArgumentException(Environment.GetResourceString("Arg_InvalidFileName"), "asmFileName");
      if (asmFileName.Length > 260)
        throw new ArgumentException(Environment.GetResourceString("IO.PathTooLong"), asmFileName);
      if ((flags & TypeLibImporterFlags.PrimaryInteropAssembly) != TypeLibImporterFlags.None && publicKey == null && keyPair == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_PIAMustBeStrongNamed"));
      ArrayList eventItfInfoList = (ArrayList) null;
      AssemblyNameFlags asmNameFlags = AssemblyNameFlags.None;
      AssemblyName assemblyNameFromTypelib = TypeLibConverter.GetAssemblyNameFromTypelib(typeLib, asmFileName, publicKey, keyPair, asmVersion, asmNameFlags);
      AssemblyBuilder assemblyForTypeLib = TypeLibConverter.CreateAssemblyForTypeLib(typeLib, asmFileName, assemblyNameFromTypelib, (uint) (flags & TypeLibImporterFlags.PrimaryInteropAssembly) > 0U, (uint) (flags & TypeLibImporterFlags.ReflectionOnlyLoading) > 0U, (uint) (flags & TypeLibImporterFlags.NoDefineVersionResource) > 0U);
      string fileName = Path.GetFileName(asmFileName);
      AssemblyBuilder assemblyBuilder = assemblyForTypeLib;
      string str = fileName;
      ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(str, str);
      if (asmNamespace == null)
        asmNamespace = assemblyNameFromTypelib.Name;
      TypeLibConverter.TypeResolveHandler typeResolveHandler = new TypeLibConverter.TypeResolveHandler(moduleBuilder, notifySink);
      AppDomain domain = Thread.GetDomain();
      ResolveEventHandler resolveEventHandler1 = new ResolveEventHandler(typeResolveHandler.ResolveEvent);
      ResolveEventHandler resolveEventHandler2 = new ResolveEventHandler(typeResolveHandler.ResolveAsmEvent);
      ResolveEventHandler resolveEventHandler3 = new ResolveEventHandler(typeResolveHandler.ResolveROAsmEvent);
      ResolveEventHandler resolveEventHandler4 = resolveEventHandler1;
      domain.TypeResolve += resolveEventHandler4;
      ResolveEventHandler resolveEventHandler5 = resolveEventHandler2;
      domain.AssemblyResolve += resolveEventHandler5;
      ResolveEventHandler resolveEventHandler6 = resolveEventHandler3;
      domain.ReflectionOnlyAssemblyResolve += resolveEventHandler6;
      TypeLibConverter.nConvertTypeLibToMetadata(typeLib, (RuntimeAssembly) assemblyForTypeLib.InternalAssembly, (RuntimeModule) moduleBuilder.InternalModule, asmNamespace, flags, (ITypeLibImporterNotifySink) typeResolveHandler, out eventItfInfoList);
      TypeLibConverter.UpdateComTypesInAssembly(assemblyForTypeLib, moduleBuilder);
      if (eventItfInfoList.Count > 0)
        new TCEAdapterGenerator().Process(moduleBuilder, eventItfInfoList);
      ResolveEventHandler resolveEventHandler7 = resolveEventHandler1;
      domain.TypeResolve -= resolveEventHandler7;
      ResolveEventHandler resolveEventHandler8 = resolveEventHandler2;
      domain.AssemblyResolve -= resolveEventHandler8;
      ResolveEventHandler resolveEventHandler9 = resolveEventHandler3;
      domain.ReflectionOnlyAssemblyResolve -= resolveEventHandler9;
      return assemblyForTypeLib;
    }

    /// <summary>将程序集转换为 COM 类型库。</summary>
    /// <returns>实现 ITypeLib 接口的对象。</returns>
    /// <param name="assembly">要转换的程序集。</param>
    /// <param name="strTypeLibName">所产生的类型库的文件名。</param>
    /// <param name="flags">指示任何特殊设置的 <see cref="T:System.Runtime.InteropServices.TypeLibExporterFlags" /> 值。</param>
    /// <param name="notifySink">由调用方实现的 <see cref="T:System.Runtime.InteropServices.ITypeLibExporterNotifySink" /> 接口。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    [return: MarshalAs(UnmanagedType.Interface)]
    public object ConvertAssemblyToTypeLib(Assembly assembly, string strTypeLibName, TypeLibExporterFlags flags, ITypeLibExporterNotifySink notifySink)
    {
      AssemblyBuilder assemblyBuilder = assembly as AssemblyBuilder;
      return TypeLibConverter.nConvertAssemblyToTypeLib(!((Assembly) assemblyBuilder != (Assembly) null) ? assembly as RuntimeAssembly : (RuntimeAssembly) assemblyBuilder.InternalAssembly, strTypeLibName, flags, notifySink);
    }

    /// <summary>获取指定类型库的主 interop 程序集的名称及基本代码。</summary>
    /// <returns>如果在注册表中找到主 interop 程序集，则为 true；否则为 false。</returns>
    /// <param name="g">类型库的 GUID。</param>
    /// <param name="major">类型库的主版本号。</param>
    /// <param name="minor">类型库的次版本号。</param>
    /// <param name="lcid">类型库的 LCID。</param>
    /// <param name="asmName">成功返回时，为与 <paramref name="g" /> 关联的主 interop 程序集的名称。</param>
    /// <param name="asmCodeBase">成功返回时，为与 <paramref name="g" /> 关联的主 interop 程序集的基本代码。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public bool GetPrimaryInteropAssembly(Guid g, int major, int minor, int lcid, out string asmName, out string asmCodeBase)
    {
      string name1 = "{" + g.ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
      string name2 = major.ToString("x", (IFormatProvider) CultureInfo.InvariantCulture) + "." + minor.ToString("x", (IFormatProvider) CultureInfo.InvariantCulture);
      asmName = (string) null;
      asmCodeBase = (string) null;
      using (RegistryKey registryKey1 = Registry.ClassesRoot.OpenSubKey("TypeLib", false))
      {
        if (registryKey1 != null)
        {
          using (RegistryKey registryKey2 = registryKey1.OpenSubKey(name1))
          {
            if (registryKey2 != null)
            {
              using (RegistryKey registryKey3 = registryKey2.OpenSubKey(name2, false))
              {
                if (registryKey3 != null)
                {
                  asmName = (string) registryKey3.GetValue("PrimaryInteropAssemblyName");
                  asmCodeBase = (string) registryKey3.GetValue("PrimaryInteropAssemblyCodeBase");
                }
              }
            }
          }
        }
      }
      return asmName != null;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static AssemblyBuilder CreateAssemblyForTypeLib(object typeLib, string asmFileName, AssemblyName asmName, bool bPrimaryInteropAssembly, bool bReflectionOnly, bool bNoDefineVersionResource)
    {
      AppDomain domain = Thread.GetDomain();
      string dir = (string) null;
      if (asmFileName != null)
      {
        dir = Path.GetDirectoryName(asmFileName);
        if (string.IsNullOrEmpty(dir))
          dir = (string) null;
      }
      AssemblyBuilderAccess access = !bReflectionOnly ? AssemblyBuilderAccess.RunAndSave : AssemblyBuilderAccess.ReflectionOnly;
      AssemblyBuilder asmBldr = domain.DefineDynamicAssembly(asmName, access, dir, false, (System.Collections.Generic.IEnumerable<CustomAttributeBuilder>) new List<CustomAttributeBuilder>() { new CustomAttributeBuilder(typeof (SecurityRulesAttribute).GetConstructor(new Type[1]{ typeof (SecurityRuleSet) }), new object[1]{ (object) SecurityRuleSet.Level2 }) });
      TypeLibConverter.SetGuidAttributeOnAssembly(asmBldr, typeLib);
      TypeLibConverter.SetImportedFromTypeLibAttrOnAssembly(asmBldr, typeLib);
      if (bNoDefineVersionResource)
        TypeLibConverter.SetTypeLibVersionAttribute(asmBldr, typeLib);
      else
        TypeLibConverter.SetVersionInformation(asmBldr, typeLib, asmName);
      if (bPrimaryInteropAssembly)
        TypeLibConverter.SetPIAAttributeOnAssembly(asmBldr, typeLib);
      return asmBldr;
    }

    [SecurityCritical]
    internal static AssemblyName GetAssemblyNameFromTypelib(object typeLib, string asmFileName, byte[] publicKey, StrongNameKeyPair keyPair, Version asmVersion, AssemblyNameFlags asmNameFlags)
    {
      string strName = (string) null;
      string strDocString = (string) null;
      int dwHelpContext = 0;
      string strHelpFile = (string) null;
      ITypeLib typeLibrary = (ITypeLib) typeLib;
      typeLibrary.GetDocumentation(-1, out strName, out strDocString, out dwHelpContext, out strHelpFile);
      if (asmFileName == null)
      {
        asmFileName = strName;
      }
      else
      {
        string fileName = Path.GetFileName(asmFileName);
        if (!".dll".Equals(Path.GetExtension(asmFileName), StringComparison.OrdinalIgnoreCase))
          throw new ArgumentException(Environment.GetResourceString("Arg_InvalidFileExtension"));
        asmFileName = fileName.Substring(0, fileName.Length - ".dll".Length);
      }
      if (asmVersion == (Version) null)
      {
        int major;
        int minor;
        Marshal.GetTypeLibVersion(typeLibrary, out major, out minor);
        asmVersion = new Version(major, minor, 0, 0);
      }
      AssemblyName assemblyName = new AssemblyName();
      string name = asmFileName;
      byte[] publicKey1 = publicKey;
      // ISSUE: variable of the null type
      __Null local1 = null;
      Version version = asmVersion;
      // ISSUE: variable of the null type
      __Null local2 = null;
      int num1 = 0;
      int num2 = 1;
      // ISSUE: variable of the null type
      __Null local3 = null;
      int num3 = (int) asmNameFlags;
      StrongNameKeyPair keyPair1 = keyPair;
      assemblyName.Init(name, publicKey1, (byte[]) local1, version, (CultureInfo) local2, (AssemblyHashAlgorithm) num1, (AssemblyVersionCompatibility) num2, (string) local3, (AssemblyNameFlags) num3, keyPair1);
      return assemblyName;
    }

    private static void UpdateComTypesInAssembly(AssemblyBuilder asmBldr, ModuleBuilder modBldr)
    {
      AssemblyBuilderData assemblyBuilderData = asmBldr.m_assemblyData;
      Type[] types = modBldr.GetTypes();
      int length = types.Length;
      for (int index = 0; index < length; ++index)
        assemblyBuilderData.AddPublicComType(types[index]);
    }

    [SecurityCritical]
    private static void SetGuidAttributeOnAssembly(AssemblyBuilder asmBldr, object typeLib)
    {
      CustomAttributeBuilder customBuilder = new CustomAttributeBuilder(typeof (GuidAttribute).GetConstructor(new Type[1]{ typeof (string) }), new object[1]{ (object) Marshal.GetTypeLibGuid((ITypeLib) typeLib).ToString() });
      asmBldr.SetCustomAttribute(customBuilder);
    }

    [SecurityCritical]
    private static void SetImportedFromTypeLibAttrOnAssembly(AssemblyBuilder asmBldr, object typeLib)
    {
      CustomAttributeBuilder customBuilder = new CustomAttributeBuilder(typeof (ImportedFromTypeLibAttribute).GetConstructor(new Type[1]{ typeof (string) }), new object[1]{ (object) Marshal.GetTypeLibName((ITypeLib) typeLib) });
      asmBldr.SetCustomAttribute(customBuilder);
    }

    [SecurityCritical]
    private static void SetTypeLibVersionAttribute(AssemblyBuilder asmBldr, object typeLib)
    {
      ConstructorInfo constructor = typeof (TypeLibVersionAttribute).GetConstructor(new Type[2]{ typeof (int), typeof (int) });
      int major;
      int minor;
      Marshal.GetTypeLibVersion((ITypeLib) typeLib, out major, out minor);
      object[] constructorArgs = new object[2]{ (object) major, (object) minor };
      CustomAttributeBuilder customBuilder = new CustomAttributeBuilder(constructor, constructorArgs);
      asmBldr.SetCustomAttribute(customBuilder);
    }

    [SecurityCritical]
    private static void SetVersionInformation(AssemblyBuilder asmBldr, object typeLib, AssemblyName asmName)
    {
      string strName = (string) null;
      string strDocString = (string) null;
      int dwHelpContext = 0;
      string strHelpFile = (string) null;
      ((ITypeLib) typeLib).GetDocumentation(-1, out strName, out strDocString, out dwHelpContext, out strHelpFile);
      string product = string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("TypeLibConverter_ImportedTypeLibProductName"), (object) strName);
      asmBldr.DefineVersionInfoResource(product, asmName.Version.ToString(), (string) null, (string) null, (string) null);
      TypeLibConverter.SetTypeLibVersionAttribute(asmBldr, typeLib);
    }

    [SecurityCritical]
    private static void SetPIAAttributeOnAssembly(AssemblyBuilder asmBldr, object typeLib)
    {
      IntPtr ppTLibAttr = IntPtr.Zero;
      ITypeLib typeLib1 = (ITypeLib) typeLib;
      int num1 = 0;
      int num2 = 0;
      ConstructorInfo constructor = typeof (PrimaryInteropAssemblyAttribute).GetConstructor(new Type[2]{ typeof (int), typeof (int) });
      try
      {
        typeLib1.GetLibAttr(out ppTLibAttr);
        System.Runtime.InteropServices.ComTypes.TYPELIBATTR typelibattr = (System.Runtime.InteropServices.ComTypes.TYPELIBATTR) Marshal.PtrToStructure(ppTLibAttr, typeof (System.Runtime.InteropServices.ComTypes.TYPELIBATTR));
        num1 = (int) typelibattr.wMajorVerNum;
        num2 = (int) typelibattr.wMinorVerNum;
      }
      finally
      {
        if (ppTLibAttr != IntPtr.Zero)
          typeLib1.ReleaseTLibAttr(ppTLibAttr);
      }
      object[] constructorArgs = new object[2]{ (object) num1, (object) num2 };
      CustomAttributeBuilder customBuilder = new CustomAttributeBuilder(constructor, constructorArgs);
      asmBldr.SetCustomAttribute(customBuilder);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void nConvertTypeLibToMetadata(object typeLib, RuntimeAssembly asmBldr, RuntimeModule modBldr, string nameSpace, TypeLibImporterFlags flags, ITypeLibImporterNotifySink notifySink, out ArrayList eventItfInfoList);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern object nConvertAssemblyToTypeLib(RuntimeAssembly assembly, string strTypeLibName, TypeLibExporterFlags flags, ITypeLibExporterNotifySink notifySink);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void LoadInMemoryTypeByName(RuntimeModule module, string className);

    private class TypeResolveHandler : ITypeLibImporterNotifySink
    {
      private List<RuntimeAssembly> m_AsmList = new List<RuntimeAssembly>();
      private ModuleBuilder m_Module;
      private ITypeLibImporterNotifySink m_UserSink;

      public TypeResolveHandler(ModuleBuilder mod, ITypeLibImporterNotifySink userSink)
      {
        this.m_Module = mod;
        this.m_UserSink = userSink;
      }

      public void ReportEvent(ImporterEventKind eventKind, int eventCode, string eventMsg)
      {
        this.m_UserSink.ReportEvent(eventKind, eventCode, eventMsg);
      }

      public Assembly ResolveRef(object typeLib)
      {
        Assembly assembly = this.m_UserSink.ResolveRef(typeLib);
        if (assembly == (Assembly) null)
          throw new ArgumentNullException();
        RuntimeAssembly runtimeAssembly = assembly as RuntimeAssembly;
        if ((Assembly) runtimeAssembly == (Assembly) null)
        {
          AssemblyBuilder assemblyBuilder = assembly as AssemblyBuilder;
          if ((Assembly) assemblyBuilder != (Assembly) null)
            runtimeAssembly = (RuntimeAssembly) assemblyBuilder.InternalAssembly;
        }
        if ((Assembly) runtimeAssembly == (Assembly) null)
          throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
        this.m_AsmList.Add(runtimeAssembly);
        return (Assembly) runtimeAssembly;
      }

      [SecurityCritical]
      public Assembly ResolveEvent(object sender, ResolveEventArgs args)
      {
        try
        {
          TypeLibConverter.LoadInMemoryTypeByName(this.m_Module.GetNativeHandle(), args.Name);
          return this.m_Module.Assembly;
        }
        catch (TypeLoadException ex)
        {
          if (ex.ResourceId != -2146233054)
            throw;
        }
        foreach (RuntimeAssembly mAsm in this.m_AsmList)
        {
          try
          {
            mAsm.GetType(args.Name, true, false);
            return (Assembly) mAsm;
          }
          catch (TypeLoadException ex)
          {
            if (ex._HResult != -2146233054)
              throw;
          }
        }
        return (Assembly) null;
      }

      public Assembly ResolveAsmEvent(object sender, ResolveEventArgs args)
      {
        foreach (RuntimeAssembly mAsm in this.m_AsmList)
        {
          if (string.Compare(mAsm.FullName, args.Name, StringComparison.OrdinalIgnoreCase) == 0)
            return (Assembly) mAsm;
        }
        return (Assembly) null;
      }

      public Assembly ResolveROAsmEvent(object sender, ResolveEventArgs args)
      {
        foreach (RuntimeAssembly mAsm in this.m_AsmList)
        {
          if (string.Compare(mAsm.FullName, args.Name, StringComparison.OrdinalIgnoreCase) == 0)
            return (Assembly) mAsm;
        }
        return Assembly.ReflectionOnlyLoad(AppDomain.CurrentDomain.ApplyPolicy(args.Name));
      }
    }
  }
}
