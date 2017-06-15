// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.AssemblyBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System.Reflection.Emit
{
  /// <summary>定义并表示动态程序集。</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_AssemblyBuilder))]
  [ComVisible(true)]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public sealed class AssemblyBuilder : Assembly, _AssemblyBuilder
  {
    internal AssemblyBuilderData m_assemblyData;
    private InternalAssemblyBuilder m_internalAssemblyBuilder;
    private ModuleBuilder m_manifestModuleBuilder;
    private bool m_fManifestModuleUsedAsDefinedModule;
    internal const string MANIFEST_MODULE_NAME = "RefEmit_InMemoryManifestModule";
    private ModuleBuilder m_onDiskAssemblyModuleBuilder;
    private bool m_profileAPICheck;

    internal object SyncRoot
    {
      get
      {
        return this.InternalAssembly.SyncRoot;
      }
    }

    internal InternalAssemblyBuilder InternalAssembly
    {
      get
      {
        return this.m_internalAssemblyBuilder;
      }
    }

    internal bool ProfileAPICheck
    {
      get
      {
        return this.m_profileAPICheck;
      }
    }

    /// <summary>如果包含清单的已加载文件未被影像复制，获取该文件的位置（基本代码的格式）。</summary>
    /// <returns>包含清单的已加载文件的位置。如果已加载文件已被影像复制，则 Location 是文件在影像复制前的位置。</returns>
    /// <exception cref="T:System.NotSupportedException">目前不支持此方法。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public override string Location
    {
      get
      {
        return this.InternalAssembly.Location;
      }
    }

    /// <summary>获取将在包含清单的文件中保存的公共语言运行时的版本。</summary>
    /// <returns>表示公共语言运行时版本的字符串。</returns>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override string ImageRuntimeVersion
    {
      get
      {
        return this.InternalAssembly.ImageRuntimeVersion;
      }
    }

    /// <summary>按照最初的指定，获取程序集的位置（例如，在 <see cref="T:System.Reflection.AssemblyName" /> 对象中）。</summary>
    /// <returns>程序集的位置（按照最初的指定）。</returns>
    /// <exception cref="T:System.NotSupportedException">目前不支持此方法。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public override string CodeBase
    {
      get
      {
        return this.InternalAssembly.CodeBase;
      }
    }

    /// <summary>返回此程序集的入口点。</summary>
    /// <returns>此程序集的入口点。</returns>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public override MethodInfo EntryPoint
    {
      get
      {
        return this.m_assemblyData.m_entryPointMethod;
      }
    }

    /// <summary>获取当前动态程序集的显示名称。</summary>
    /// <returns>动态程序集的显示名称。</returns>
    public override string FullName
    {
      get
      {
        return this.InternalAssembly.FullName;
      }
    }

    /// <summary>获取此程序集的证据。</summary>
    /// <returns>此程序集的证据。</returns>
    public override Evidence Evidence
    {
      get
      {
        return this.InternalAssembly.Evidence;
      }
    }

    /// <summary>获取当前动态程序集的授予集。</summary>
    /// <returns>当前动态程序集的授予集。</returns>
    public override PermissionSet PermissionSet
    {
      [SecurityCritical] get
      {
        return this.InternalAssembly.PermissionSet;
      }
    }

    /// <summary>获取一个值，该值指示公共语言运行时 (CLR) 对此程序集强制执行的安全规则集。</summary>
    /// <returns>CLR 对该动态程序集强制执行的安全规则集。</returns>
    public override SecurityRuleSet SecurityRuleSet
    {
      get
      {
        return this.InternalAssembly.SecurityRuleSet;
      }
    }

    /// <summary>获取包含程序集清单的当前 <see cref="T:System.Reflection.Emit.AssemblyBuilder" /> 中的模块。</summary>
    /// <returns>清单模块。</returns>
    public override Module ManifestModule
    {
      get
      {
        return (Module) this.m_manifestModuleBuilder.InternalModule;
      }
    }

    /// <summary>获取一个值，该值指示动态程序集是否位于仅反射的上下文中。</summary>
    /// <returns>如果动态程序集位于仅反射的上下文中，则此值为 true；否则为 false。</returns>
    public override bool ReflectionOnly
    {
      get
      {
        return this.InternalAssembly.ReflectionOnly;
      }
    }

    /// <summary>获取一个值，该值指示程序集是否是从全局程序集缓存加载的。</summary>
    /// <returns>始终为 false。</returns>
    public override bool GlobalAssemblyCache
    {
      get
      {
        return this.InternalAssembly.GlobalAssemblyCache;
      }
    }

    /// <summary>获取创建动态程序集的主机上下文。</summary>
    /// <returns>一个值，该值指示创建动态程序集的主机上下文。</returns>
    public override long HostContext
    {
      get
      {
        return this.InternalAssembly.HostContext;
      }
    }

    /// <summary>获取一个值，该值指示当前程序集是动态程序集。</summary>
    /// <returns>始终为 true。</returns>
    public override bool IsDynamic
    {
      get
      {
        return true;
      }
    }

    [SecurityCritical]
    internal AssemblyBuilder(AppDomain domain, AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, ref StackCrawlMark stackMark, IEnumerable<CustomAttributeBuilder> unsafeAssemblyAttributes, SecurityContextSource securityContextSource)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (access != AssemblyBuilderAccess.Run && access != AssemblyBuilderAccess.Save && (access != AssemblyBuilderAccess.RunAndSave && access != AssemblyBuilderAccess.ReflectionOnly) && access != AssemblyBuilderAccess.RunAndCollect)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) access), "access");
      if (securityContextSource < SecurityContextSource.CurrentAppDomain || securityContextSource > SecurityContextSource.CurrentAssembly)
        throw new ArgumentOutOfRangeException("securityContextSource");
      name = (AssemblyName) name.Clone();
      if (name.KeyPair != null)
      {
        AssemblyName assemblyName = name;
        byte[] publicKey = assemblyName.KeyPair.PublicKey;
        assemblyName.SetPublicKey(publicKey);
      }
      if (evidence != null)
        new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
      if (access == AssemblyBuilderAccess.RunAndCollect)
        new PermissionSet(PermissionState.Unrestricted).Demand();
      List<CustomAttributeBuilder> attributeBuilderList = (List<CustomAttributeBuilder>) null;
      DynamicAssemblyFlags flags = DynamicAssemblyFlags.None;
      byte[] securityRulesBlob = (byte[]) null;
      byte[] aptcaBlob = (byte[]) null;
      if (unsafeAssemblyAttributes != null)
      {
        attributeBuilderList = new List<CustomAttributeBuilder>(unsafeAssemblyAttributes);
        foreach (CustomAttributeBuilder attributeBuilder in attributeBuilderList)
        {
          if (attributeBuilder.m_con.DeclaringType == typeof (SecurityTransparentAttribute))
            flags |= DynamicAssemblyFlags.Transparent;
          else if (attributeBuilder.m_con.DeclaringType == typeof (SecurityCriticalAttribute))
          {
            SecurityCriticalScope securityCriticalScope = SecurityCriticalScope.Everything;
            if (attributeBuilder.m_constructorArgs != null && attributeBuilder.m_constructorArgs.Length == 1 && attributeBuilder.m_constructorArgs[0] is SecurityCriticalScope)
              securityCriticalScope = (SecurityCriticalScope) attributeBuilder.m_constructorArgs[0];
            flags |= DynamicAssemblyFlags.Critical;
            if (securityCriticalScope == SecurityCriticalScope.Everything)
              flags |= DynamicAssemblyFlags.AllCritical;
          }
          else if (attributeBuilder.m_con.DeclaringType == typeof (SecurityRulesAttribute))
          {
            securityRulesBlob = new byte[attributeBuilder.m_blob.Length];
            Array.Copy((Array) attributeBuilder.m_blob, (Array) securityRulesBlob, securityRulesBlob.Length);
          }
          else if (attributeBuilder.m_con.DeclaringType == typeof (SecurityTreatAsSafeAttribute))
            flags |= DynamicAssemblyFlags.TreatAsSafe;
          else if (attributeBuilder.m_con.DeclaringType == typeof (AllowPartiallyTrustedCallersAttribute))
          {
            flags |= DynamicAssemblyFlags.Aptca;
            aptcaBlob = new byte[attributeBuilder.m_blob.Length];
            Array.Copy((Array) attributeBuilder.m_blob, (Array) aptcaBlob, aptcaBlob.Length);
          }
        }
      }
      this.m_internalAssemblyBuilder = (InternalAssemblyBuilder) AssemblyBuilder.nCreateDynamicAssembly(domain, name, evidence, ref stackMark, requiredPermissions, optionalPermissions, refusedPermissions, securityRulesBlob, aptcaBlob, access, flags, securityContextSource);
      this.m_assemblyData = new AssemblyBuilderData(this.m_internalAssemblyBuilder, name.Name, access, dir);
      this.m_assemblyData.AddPermissionRequests(requiredPermissions, optionalPermissions, refusedPermissions);
      if (AppDomain.ProfileAPICheck)
      {
        RuntimeAssembly executingAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackMark);
        if ((Assembly) executingAssembly != (Assembly) null && !executingAssembly.IsFrameworkAssembly())
          this.m_profileAPICheck = true;
      }
      this.InitManifestModule();
      if (attributeBuilderList == null)
        return;
      foreach (CustomAttributeBuilder customBuilder in attributeBuilderList)
        this.SetCustomAttribute(customBuilder);
    }

    private AssemblyBuilder()
    {
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern RuntimeModule GetInMemoryAssemblyModule(RuntimeAssembly assembly);

    [SecurityCritical]
    private Module nGetInMemoryAssemblyModule()
    {
      return (Module) AssemblyBuilder.GetInMemoryAssemblyModule(this.GetNativeHandle());
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern RuntimeModule GetOnDiskAssemblyModule(RuntimeAssembly assembly);

    [SecurityCritical]
    private ModuleBuilder GetOnDiskAssemblyModuleBuilder()
    {
      if ((Module) this.m_onDiskAssemblyModuleBuilder == (Module) null)
      {
        ModuleBuilder moduleBuilder = new ModuleBuilder(this, (InternalModuleBuilder) AssemblyBuilder.GetOnDiskAssemblyModule(this.InternalAssembly.GetNativeHandle()));
        moduleBuilder.Init("RefEmit_OnDiskManifestModule", (string) null, 0);
        this.m_onDiskAssemblyModuleBuilder = moduleBuilder;
      }
      return this.m_onDiskAssemblyModuleBuilder;
    }

    internal ModuleBuilder GetModuleBuilder(InternalModuleBuilder module)
    {
      lock (this.SyncRoot)
      {
        foreach (ModuleBuilder item_0 in this.m_assemblyData.m_moduleBuilderList)
        {
          if ((Module) item_0.InternalModule == (Module) module)
            return item_0;
        }
        if ((Module) this.m_onDiskAssemblyModuleBuilder != (Module) null && (Module) this.m_onDiskAssemblyModuleBuilder.InternalModule == (Module) module)
          return this.m_onDiskAssemblyModuleBuilder;
        if ((Module) this.m_manifestModuleBuilder.InternalModule == (Module) module)
          return this.m_manifestModuleBuilder;
        throw new ArgumentException("module");
      }
    }

    internal RuntimeAssembly GetNativeHandle()
    {
      return this.InternalAssembly.GetNativeHandle();
    }

    [SecurityCritical]
    internal Version GetVersion()
    {
      return this.InternalAssembly.GetVersion();
    }

    [SecurityCritical]
    private void InitManifestModule()
    {
      this.m_manifestModuleBuilder = new ModuleBuilder(this, (InternalModuleBuilder) this.nGetInMemoryAssemblyModule());
      this.m_manifestModuleBuilder.Init("RefEmit_InMemoryManifestModule", (string) null, 0);
      this.m_fManifestModuleUsedAsDefinedModule = false;
    }

    /// <summary>定义一个动态程序集，该动态程序集具有指定的名称和访问权限。</summary>
    /// <returns>一个表示新程序集的对象。</returns>
    /// <param name="name">程序集的名称。</param>
    /// <param name="access">程序集的访问权限。</param>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return AssemblyBuilder.InternalDefineDynamicAssembly(name, access, (string) null, (Evidence) null, (PermissionSet) null, (PermissionSet) null, (PermissionSet) null, ref stackMark, (IEnumerable<CustomAttributeBuilder>) null, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>使用指定的名称、访问模式和自定义特性定义动态程序集。</summary>
    /// <returns>一个表示新程序集的对象。</returns>
    /// <param name="name">程序集的名称。</param>
    /// <param name="access">程序集的访问权限。</param>
    /// <param name="assemblyAttributes">一个包含程序集特性的集合。</param>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, IEnumerable<CustomAttributeBuilder> assemblyAttributes)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return AssemblyBuilder.InternalDefineDynamicAssembly(name, access, (string) null, (Evidence) null, (PermissionSet) null, (PermissionSet) null, (PermissionSet) null, ref stackMark, assemblyAttributes, SecurityContextSource.CurrentAssembly);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern Assembly nCreateDynamicAssembly(AppDomain domain, AssemblyName name, Evidence identity, ref StackCrawlMark stackMark, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, byte[] securityRulesBlob, byte[] aptcaBlob, AssemblyBuilderAccess access, DynamicAssemblyFlags flags, SecurityContextSource securityContextSource);

    [SecurityCritical]
    internal static AssemblyBuilder InternalDefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, ref StackCrawlMark stackMark, IEnumerable<CustomAttributeBuilder> unsafeAssemblyAttributes, SecurityContextSource securityContextSource)
    {
      if (evidence != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
      lock (typeof (AssemblyBuilder.AssemblyBuilderLock))
        return new AssemblyBuilder(AppDomain.CurrentDomain, name, access, dir, evidence, requiredPermissions, optionalPermissions, refusedPermissions, ref stackMark, unsafeAssemblyAttributes, securityContextSource);
    }

    /// <summary>在此程序集中定义命名的瞬态动态模块。</summary>
    /// <returns>表示已定义动态模块的 <see cref="T:System.Reflection.Emit.ModuleBuilder" />。</returns>
    /// <param name="name">该动态模块的名称。长度必须小于 260 个字符。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 以空白开始。- 或 -<paramref name="name" /> 的长度为零。- 或 -<paramref name="name" /> 的长度大于或等于 260。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.ExecutionEngineException">无法加载默认符号编写器的程序集。- 或 -无法找到实现默认符号编写器接口的类型。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public ModuleBuilder DefineDynamicModule(string name)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.DefineDynamicModuleInternal(name, false, ref stackMark);
    }

    /// <summary>在此程序集中定义命名的瞬态动态模块，并指定是否发出符号信息。</summary>
    /// <returns>表示已定义动态模块的 <see cref="T:System.Reflection.Emit.ModuleBuilder" />。</returns>
    /// <param name="name">该动态模块的名称。长度必须小于 260 个字符。</param>
    /// <param name="emitSymbolInfo">如果发出符号信息，则为 true；否则，为 false。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 以空白开始。- 或 -<paramref name="name" /> 的长度为零。- 或 -<paramref name="name" /> 的长度大于或等于 260。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.ExecutionEngineException">无法加载默认符号编写器的程序集。- 或 -无法找到实现默认符号编写器接口的类型。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public ModuleBuilder DefineDynamicModule(string name, bool emitSymbolInfo)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.DefineDynamicModuleInternal(name, emitSymbolInfo, ref stackMark);
    }

    [SecurityCritical]
    private ModuleBuilder DefineDynamicModuleInternal(string name, bool emitSymbolInfo, ref StackCrawlMark stackMark)
    {
      lock (this.SyncRoot)
        return this.DefineDynamicModuleInternalNoLock(name, emitSymbolInfo, ref stackMark);
    }

    [SecurityCritical]
    private ModuleBuilder DefineDynamicModuleInternalNoLock(string name, bool emitSymbolInfo, ref StackCrawlMark stackMark)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
      if ((int) name[0] == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidName"), "name");
      ISymbolWriter writer = (ISymbolWriter) null;
      IntPtr underlyingWriter = new IntPtr();
      this.m_assemblyData.CheckNameConflict(name);
      ModuleBuilder dynModule;
      if (this.m_fManifestModuleUsedAsDefinedModule)
      {
        InternalAssemblyBuilder internalAssembly = this.InternalAssembly;
        int num1 = emitSymbolInfo ? 1 : 0;
        string str = name;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        StackCrawlMark& stackMark1 = @stackMark;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        IntPtr& pInternalSymWriter = @underlyingWriter;
        int num2 = 1;
        int tkFile1;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        int& tkFile2 = @tkFile1;
        dynModule = new ModuleBuilder(this, (InternalModuleBuilder) AssemblyBuilder.DefineDynamicModule((RuntimeAssembly) internalAssembly, num1 != 0, str, str, stackMark1, pInternalSymWriter, num2 != 0, tkFile2));
        dynModule.Init(name, (string) null, tkFile1);
      }
      else
      {
        this.m_manifestModuleBuilder.ModifyModuleName(name);
        dynModule = this.m_manifestModuleBuilder;
        if (emitSymbolInfo)
          underlyingWriter = ModuleBuilder.nCreateISymWriterForDynamicModule((Module) dynModule.InternalModule, name);
      }
      if (emitSymbolInfo)
      {
        Type type = this.LoadISymWrapper().GetType("System.Diagnostics.SymbolStore.SymWriter", true, false);
        if (type != (Type) null && !type.IsVisible)
          type = (Type) null;
        if (type == (Type) null)
          throw new TypeLoadException(Environment.GetResourceString("MissingType", (object) "SymWriter"));
        new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
        try
        {
          new PermissionSet(PermissionState.Unrestricted).Assert();
          writer = (ISymbolWriter) Activator.CreateInstance(type);
          writer.SetUnderlyingWriter(underlyingWriter);
        }
        finally
        {
          CodeAccessPermission.RevertAssert();
        }
      }
      dynModule.SetSymWriter(writer);
      this.m_assemblyData.AddModule(dynModule);
      if ((Module) dynModule == (Module) this.m_manifestModuleBuilder)
        this.m_fManifestModuleUsedAsDefinedModule = true;
      return dynModule;
    }

    /// <summary>用给定名称定义将保存到指定文件的持久动态模块。不发出符号信息。</summary>
    /// <returns>表示已定义动态模块的 <see cref="T:System.Reflection.Emit.ModuleBuilder" /> 对象。</returns>
    /// <param name="name">该动态模块的名称。长度必须小于 260 个字符。</param>
    /// <param name="fileName">该动态模块应保存到的文件的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 或 <paramref name="fileName" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 或 <paramref name="fileName" /> 的长度为零。- 或 -<paramref name="name" /> 的长度大于或等于 260。- 或 -<paramref name="fileName" /> 包含路径规范（例如，目录组件）。- 或 -与属于此程序集的另一个文件的名称有冲突。</exception>
    /// <exception cref="T:System.InvalidOperationException">此程序集以前已保存过。</exception>
    /// <exception cref="T:System.NotSupportedException">在动态程序集上用 <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Run" /> 特性调用了此程序集。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.ExecutionEngineException">无法加载默认符号编写器的程序集。- 或 -无法找到实现默认符号编写器接口的类型。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public ModuleBuilder DefineDynamicModule(string name, string fileName)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.DefineDynamicModuleInternal(name, fileName, false, ref stackMark);
    }

    /// <summary>定义持久动态模块，并指定模块名称、用于保存模块的文件名，同时指定是否使用默认符号编写器发出符号信息。</summary>
    /// <returns>表示已定义动态模块的 <see cref="T:System.Reflection.Emit.ModuleBuilder" /> 对象。</returns>
    /// <param name="name">该动态模块的名称。长度必须小于 260 个字符。</param>
    /// <param name="fileName">该动态模块应保存到的文件的名称。</param>
    /// <param name="emitSymbolInfo">如果为 true，则使用默认的符号编写器编写符号信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 或 <paramref name="fileName" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 或 <paramref name="fileName" /> 的长度为零。- 或 -<paramref name="name" /> 的长度大于或等于 260。- 或 -<paramref name="fileName" /> 包含路径规范（例如，目录组件）。- 或 -与属于此程序集的另一个文件的名称有冲突。</exception>
    /// <exception cref="T:System.InvalidOperationException">此程序集以前已保存过。</exception>
    /// <exception cref="T:System.NotSupportedException">在动态程序集上用 <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Run" /> 特性调用了此程序集。</exception>
    /// <exception cref="T:System.ExecutionEngineException">无法加载默认符号编写器的程序集。- 或 -无法找到实现默认符号编写器接口的类型。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public ModuleBuilder DefineDynamicModule(string name, string fileName, bool emitSymbolInfo)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.DefineDynamicModuleInternal(name, fileName, emitSymbolInfo, ref stackMark);
    }

    [SecurityCritical]
    private ModuleBuilder DefineDynamicModuleInternal(string name, string fileName, bool emitSymbolInfo, ref StackCrawlMark stackMark)
    {
      lock (this.SyncRoot)
        return this.DefineDynamicModuleInternalNoLock(name, fileName, emitSymbolInfo, ref stackMark);
    }

    [SecurityCritical]
    private ModuleBuilder DefineDynamicModuleInternalNoLock(string name, string fileName, bool emitSymbolInfo, ref StackCrawlMark stackMark)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
      if ((int) name[0] == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidName"), "name");
      if (fileName == null)
        throw new ArgumentNullException("fileName");
      if (fileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "fileName");
      if (!string.Equals(fileName, Path.GetFileName(fileName)))
        throw new ArgumentException(Environment.GetResourceString("Argument_NotSimpleFileName"), "fileName");
      if (this.m_assemblyData.m_access == AssemblyBuilderAccess.Run)
        throw new NotSupportedException(Environment.GetResourceString("Argument_BadPersistableModuleInTransientAssembly"));
      if (this.m_assemblyData.m_isSaved)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotAlterAssembly"));
      ISymbolWriter writer = (ISymbolWriter) null;
      IntPtr pInternalSymWriter = new IntPtr();
      this.m_assemblyData.CheckNameConflict(name);
      this.m_assemblyData.CheckFileNameConflict(fileName);
      int tkFile;
      ModuleBuilder dynModule = new ModuleBuilder(this, (InternalModuleBuilder) AssemblyBuilder.DefineDynamicModule((RuntimeAssembly) this.InternalAssembly, emitSymbolInfo, name, fileName, ref stackMark, ref pInternalSymWriter, false, out tkFile));
      dynModule.Init(name, fileName, tkFile);
      if (emitSymbolInfo)
      {
        Type type = this.LoadISymWrapper().GetType("System.Diagnostics.SymbolStore.SymWriter", true, false);
        if (type != (Type) null && !type.IsVisible)
          type = (Type) null;
        if (type == (Type) null)
          throw new TypeLoadException(Environment.GetResourceString("MissingType", (object) "SymWriter"));
        try
        {
          new PermissionSet(PermissionState.Unrestricted).Assert();
          writer = (ISymbolWriter) Activator.CreateInstance(type);
          writer.SetUnderlyingWriter(pInternalSymWriter);
        }
        finally
        {
          CodeAccessPermission.RevertAssert();
        }
      }
      dynModule.SetSymWriter(writer);
      this.m_assemblyData.AddModule(dynModule);
      return dynModule;
    }

    private Assembly LoadISymWrapper()
    {
      if (this.m_assemblyData.m_ISymWrapperAssembly != (Assembly) null)
        return this.m_assemblyData.m_ISymWrapperAssembly;
      Assembly assembly = Assembly.Load("ISymWrapper, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
      this.m_assemblyData.m_ISymWrapperAssembly = assembly;
      return assembly;
    }

    internal void CheckContext(params Type[][] typess)
    {
      if (typess == null)
        return;
      foreach (Type[] typeArray in typess)
      {
        if (typeArray != null)
          this.CheckContext(typeArray);
      }
    }

    internal void CheckContext(params Type[] types)
    {
      if (types == null)
        return;
      foreach (Type type in types)
      {
        if (!(type == (Type) null))
        {
          if (type.Module == (Module) null || type.Module.Assembly == (Assembly) null)
            throw new ArgumentException(Environment.GetResourceString("Argument_TypeNotValid"));
          if (!(type.Module.Assembly == typeof (object).Module.Assembly))
          {
            if (type.Module.Assembly.ReflectionOnly && !this.ReflectionOnly)
              throw new InvalidOperationException(Environment.GetResourceString("Arugment_EmitMixedContext1", (object) type.AssemblyQualifiedName));
            if (!type.Module.Assembly.ReflectionOnly && this.ReflectionOnly)
              throw new InvalidOperationException(Environment.GetResourceString("Arugment_EmitMixedContext2", (object) type.AssemblyQualifiedName));
          }
        }
      }
    }

    /// <summary>用默认的公共资源特性为此程序集定义独立托管资源。</summary>
    /// <returns>指定资源的 <see cref="T:System.Resources.ResourceWriter" /> 对象。</returns>
    /// <param name="name">资源的逻辑名称。</param>
    /// <param name="description">资源的文本说明。</param>
    /// <param name="fileName">逻辑名称映射到的物理文件名（.resources 文件）。这应不包括路径。</param>
    /// <exception cref="T:System.ArgumentException">以前定义过 <paramref name="name" />。- 或 -程序集中还有一个名为 <paramref name="fileName" /> 的文件。- 或 -<paramref name="name" /> 的长度为零。- 或 -<paramref name="fileName" /> 的长度为零。- 或 -<paramref name="fileName" /> 包括路径。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 或 <paramref name="fileName" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public IResourceWriter DefineResource(string name, string description, string fileName)
    {
      return this.DefineResource(name, description, fileName, ResourceAttributes.Public);
    }

    /// <summary>为此程序集定义独立托管资源。可为托管资源指定特性。</summary>
    /// <returns>指定资源的 <see cref="T:System.Resources.ResourceWriter" /> 对象。</returns>
    /// <param name="name">资源的逻辑名称。</param>
    /// <param name="description">资源的文本说明。</param>
    /// <param name="fileName">逻辑名称映射到的物理文件名（.resources 文件）。这应不包括路径。</param>
    /// <param name="attribute">资源属性。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 以前已定义过，或者如果程序集中有另一个名为 <paramref name="fileName" />。- 或 -<paramref name="name" /> 的长度为零。- 或 -<paramref name="fileName" /> 的长度为零。- 或 -<paramref name="fileName" /> 包括路径。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 或 <paramref name="fileName" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public IResourceWriter DefineResource(string name, string description, string fileName, ResourceAttributes attribute)
    {
      lock (this.SyncRoot)
        return this.DefineResourceNoLock(name, description, fileName, attribute);
    }

    private IResourceWriter DefineResourceNoLock(string name, string description, string fileName, ResourceAttributes attribute)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), name);
      if (fileName == null)
        throw new ArgumentNullException("fileName");
      if (fileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "fileName");
      if (!string.Equals(fileName, Path.GetFileName(fileName)))
        throw new ArgumentException(Environment.GetResourceString("Argument_NotSimpleFileName"), "fileName");
      this.m_assemblyData.CheckResNameConflict(name);
      this.m_assemblyData.CheckFileNameConflict(fileName);
      string str;
      ResourceWriter resWriter;
      if (this.m_assemblyData.m_strDir == null)
      {
        str = Path.Combine(Environment.CurrentDirectory, fileName);
        resWriter = new ResourceWriter(str);
      }
      else
      {
        str = Path.Combine(this.m_assemblyData.m_strDir, fileName);
        resWriter = new ResourceWriter(str);
      }
      string fullPath = Path.GetFullPath(str);
      fileName = Path.GetFileName(fullPath);
      this.m_assemblyData.AddResWriter(new ResWriterData(resWriter, (Stream) null, name, fileName, fullPath, attribute));
      return (IResourceWriter) resWriter;
    }

    /// <summary>向此程序集添加现有资源文件。</summary>
    /// <param name="name">资源的逻辑名称。</param>
    /// <param name="fileName">逻辑名称映射到的物理文件名（.resources 文件）。文件名不应包含路径；该文件必须与将其添加到的程序集位于同一目录中。</param>
    /// <exception cref="T:System.ArgumentException">以前定义过 <paramref name="name" />。- 或 -程序集中还有一个名为 <paramref name="fileName" /> 的文件。- 或 -<paramref name="name" /> 的长度为零。- 或 -<paramref name="fileName" /> 的长度为零，或者如果 <paramref name="fileName" /> 包含路径。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 或 <paramref name="fileName" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到文件 <paramref name="fileName" />。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public void AddResourceFile(string name, string fileName)
    {
      this.AddResourceFile(name, fileName, ResourceAttributes.Public);
    }

    /// <summary>向此程序集添加现有资源文件。</summary>
    /// <param name="name">资源的逻辑名称。</param>
    /// <param name="fileName">逻辑名称映射到的物理文件名（.resources 文件）。文件名不应包含路径；该文件必须与将其添加到的程序集位于同一目录中。</param>
    /// <param name="attribute">资源属性。</param>
    /// <exception cref="T:System.ArgumentException">以前定义过 <paramref name="name" />。- 或 -程序集中还有一个名为 <paramref name="fileName" /> 的文件。- 或 -<paramref name="name" /> 的长度为零，或者如果 <paramref name="fileName" /> 的长度为零。- 或 -<paramref name="fileName" /> 包括路径。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 或 <paramref name="fileName" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">如果未找到 <paramref name="fileName" /> 文件。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public void AddResourceFile(string name, string fileName, ResourceAttributes attribute)
    {
      lock (this.SyncRoot)
        this.AddResourceFileNoLock(name, fileName, attribute);
    }

    [SecuritySafeCritical]
    private void AddResourceFileNoLock(string name, string fileName, ResourceAttributes attribute)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), name);
      if (fileName == null)
        throw new ArgumentNullException("fileName");
      if (fileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), fileName);
      if (!string.Equals(fileName, Path.GetFileName(fileName)))
        throw new ArgumentException(Environment.GetResourceString("Argument_NotSimpleFileName"), "fileName");
      this.m_assemblyData.CheckResNameConflict(name);
      this.m_assemblyData.CheckFileNameConflict(fileName);
      string fullPath = Path.UnsafeGetFullPath(this.m_assemblyData.m_strDir != null ? Path.Combine(this.m_assemblyData.m_strDir, fileName) : Path.Combine(Environment.CurrentDirectory, fileName));
      fileName = Path.GetFileName(fullPath);
      if (!File.UnsafeExists(fullPath))
        throw new FileNotFoundException(Environment.GetResourceString("IO.FileNotFound_FileName", (object) fileName), fileName);
      this.m_assemblyData.AddResWriter(new ResWriterData((ResourceWriter) null, (Stream) null, name, fileName, fullPath, attribute));
    }

    /// <summary>返回一个值，该值指示此实例是否与指定的对象相等。</summary>
    /// <returns>如果 <paramref name="obj" /> 等于此实例的类型和值，则为 true；否则为 false。</returns>
    /// <param name="obj">与此实例进行比较的 object，或 null。</param>
    public override bool Equals(object obj)
    {
      return this.InternalAssembly.Equals(obj);
    }

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    public override int GetHashCode()
    {
      return this.InternalAssembly.GetHashCode();
    }

    /// <summary>返回已应用于当前 <see cref="T:System.Reflection.Emit.AssemblyBuilder" /> 的所有自定义特性。</summary>
    /// <returns>一个数组，其中包含自定义特性；如果没有特性，则该数组为空。</returns>
    /// <param name="inherit">对于该类型的对象，该参数被忽略。</param>
    public override object[] GetCustomAttributes(bool inherit)
    {
      return this.InternalAssembly.GetCustomAttributes(inherit);
    }

    /// <summary>返回已应用于当前 <see cref="T:System.Reflection.Emit.AssemblyBuilder" /> 且派生自指定的特性类型的所有自定义特性。</summary>
    /// <returns>一个数组，其中包含从 <paramref name="attributeType" /> 派生的任何级别的自定义特性；如果没有这样的特性，则该数组为空。</returns>
    /// <param name="attributeType">从中派生特性的基类。</param>
    /// <param name="inherit">对于该类型的对象，该参数被忽略。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不是一个由运行库提供的 <see cref="T:System.Type" /> 对象。例如，<paramref name="attributeType" /> 是一个 <see cref="T:System.Reflection.Emit.TypeBuilder" /> 对象。</exception>
    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      return this.InternalAssembly.GetCustomAttributes(attributeType, inherit);
    }

    /// <summary>返回一个值，该值指示是否将指定特性类型的一个或多个实例应用于此成员。</summary>
    /// <returns>如果对此动态程序集应用了一个或多个 <paramref name="attributeType" /> 实例，则为 true；否则为 false。</returns>
    /// <param name="attributeType">要测试的特性的类型。</param>
    /// <param name="inherit">对于该类型的对象，该参数被忽略。</param>
    public override bool IsDefined(Type attributeType, bool inherit)
    {
      return this.InternalAssembly.IsDefined(attributeType, inherit);
    }

    /// <summary>返回 <see cref="T:System.Reflection.CustomAttributeData" /> 对象，这些对象包含有关已应用于当前 <see cref="T:System.Reflection.Emit.AssemblyBuilder" /> 的特性的信息。</summary>
    /// <returns>一个 <see cref="T:System.Reflection.CustomAttributeData" /> 对象泛型列表，这些对象表示已应用于当前模块的特性的有关数据。</returns>
    public override IList<CustomAttributeData> GetCustomAttributesData()
    {
      return this.InternalAssembly.GetCustomAttributesData();
    }

    /// <summary>从此程序集加载指定的清单资源。</summary>
    /// <returns>包含所有资源名称的 String 类型的数组。</returns>
    /// <exception cref="T:System.NotSupportedException">在动态程序集上不支持此方法。若要获取清单资源名称，请使用 <see cref="M:System.Reflection.Assembly.GetManifestResourceNames" />。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public override string[] GetManifestResourceNames()
    {
      return this.InternalAssembly.GetManifestResourceNames();
    }

    /// <summary>获取此程序集清单的文件表中指定文件的 <see cref="T:System.IO.FileStream" />。</summary>
    /// <returns>指定文件的 <see cref="T:System.IO.FileStream" />；如果未找到此文件，则为 null。</returns>
    /// <param name="name">指定文件的名称。</param>
    /// <exception cref="T:System.NotSupportedException">目前不支持此方法。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public override FileStream GetFile(string name)
    {
      return this.InternalAssembly.GetFile(name);
    }

    /// <summary>获取程序集清单的文件表中的文件，指定是否包括资源模块。</summary>
    /// <returns>
    /// <see cref="T:System.IO.FileStream" /> 对象的数组。</returns>
    /// <param name="getResourceModules">为 true，则包括资源模块；否则，为 false。</param>
    /// <exception cref="T:System.NotSupportedException">目前不支持此方法。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public override FileStream[] GetFiles(bool getResourceModules)
    {
      return this.InternalAssembly.GetFiles(getResourceModules);
    }

    /// <summary>从此程序集加载指定清单资源，清单资源的范围由指定类型的命名空间确定。</summary>
    /// <returns>表示此清单资源的 <see cref="T:System.IO.Stream" />。</returns>
    /// <param name="type">其命名空间用于确定清单资源名的范围的类型。</param>
    /// <param name="name">请求的清单资源的名称。</param>
    /// <exception cref="T:System.NotSupportedException">目前不支持此方法。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public override Stream GetManifestResourceStream(Type type, string name)
    {
      return this.InternalAssembly.GetManifestResourceStream(type, name);
    }

    /// <summary>从此程序集加载指定的清单资源。</summary>
    /// <returns>表示此清单资源的 <see cref="T:System.IO.Stream" />。</returns>
    /// <param name="name">请求的清单资源的名称。</param>
    /// <exception cref="T:System.NotSupportedException">目前不支持此方法。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public override Stream GetManifestResourceStream(string name)
    {
      return this.InternalAssembly.GetManifestResourceStream(name);
    }

    /// <summary>返回关于给定资源如何保持的信息。</summary>
    /// <returns>用关于资源拓扑的信息填充的 <see cref="T:System.Reflection.ManifestResourceInfo" />；如果未找到资源，则为 null。</returns>
    /// <param name="resourceName">资源的名称。</param>
    /// <exception cref="T:System.NotSupportedException">目前不支持此方法。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public override ManifestResourceInfo GetManifestResourceInfo(string resourceName)
    {
      return this.InternalAssembly.GetManifestResourceInfo(resourceName);
    }

    /// <summary>获取在此程序集中定义的导出类型。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 的数组，包含此程序集中定义的导出类型。</returns>
    /// <exception cref="T:System.NotSupportedException">此方法未实现。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public override Type[] GetExportedTypes()
    {
      return this.InternalAssembly.GetExportedTypes();
    }

    /// <summary>获取在创建当前动态程序集时指定的 <see cref="T:System.Reflection.AssemblyName" />，并以指定方式设置基本代码。</summary>
    /// <returns>动态程序集的名称。</returns>
    /// <param name="copiedName">如果为 true，则将基本代码设置为程序集被影像复制后的位置；如果为 false，则将基本代码设置为原位置。</param>
    public override AssemblyName GetName(bool copiedName)
    {
      return this.InternalAssembly.GetName(copiedName);
    }

    /// <summary>从当前 <see cref="T:System.Reflection.Emit.AssemblyBuilder" /> 中已定义和创建的类型中获取指定的类型。</summary>
    /// <returns>指定的类型或 null（如果找不到或尚未创建该类型）。</returns>
    /// <param name="name">要搜索的类型的名称。</param>
    /// <param name="throwOnError">如果为 true，则在找不到该类型时引发异常；否则为 false。</param>
    /// <param name="ignoreCase">如果为 true，则在搜索时忽略类型名的大小写；否则为 false。</param>
    public override Type GetType(string name, bool throwOnError, bool ignoreCase)
    {
      return this.InternalAssembly.GetType(name, throwOnError, ignoreCase);
    }

    /// <summary>获取此程序集中的指定模块。</summary>
    /// <returns>所请求的模块，若未找到该模块则为 null。</returns>
    /// <param name="name">请求的模块的名称。</param>
    public override Module GetModule(string name)
    {
      return this.InternalAssembly.GetModule(name);
    }

    /// <summary>获取此 <see cref="T:System.Reflection.Emit.AssemblyBuilder" /> 所引用的程序集的 <see cref="T:System.Reflection.AssemblyName" /> 对象的不完整列表。</summary>
    /// <returns>引用的程序集的程序集名称的数组。此数组不是完整列表。</returns>
    public override AssemblyName[] GetReferencedAssemblies()
    {
      return this.InternalAssembly.GetReferencedAssemblies();
    }

    /// <summary>获取属于此程序集的所有模块，还可以包括资源模块。</summary>
    /// <returns>属于此程序集的模块。</returns>
    /// <param name="getResourceModules">为 true，则包括资源模块；否则，为 false。</param>
    public override Module[] GetModules(bool getResourceModules)
    {
      return this.InternalAssembly.GetModules(getResourceModules);
    }

    /// <summary>返回属于此程序集的所有已加载模块，还可以包括资源模块。</summary>
    /// <returns>属于此程序集的已加载模块。</returns>
    /// <param name="getResourceModules">为 true，则包括资源模块；否则，为 false。</param>
    public override Module[] GetLoadedModules(bool getResourceModules)
    {
      return this.InternalAssembly.GetLoadedModules(getResourceModules);
    }

    /// <summary>获取指定区域性的附属程序集。</summary>
    /// <returns>指定的附属程序集。</returns>
    /// <param name="culture">指定的区域性。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="culture" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到此程序集。</exception>
    /// <exception cref="T:System.IO.FileLoadException">找到了具有匹配文件名的附属程序集，但是 CultureInfo 与指定的不匹配。</exception>
    /// <exception cref="T:System.BadImageFormatException">附属程序集不是有效程序集。</exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public override Assembly GetSatelliteAssembly(CultureInfo culture)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalAssembly.InternalGetSatelliteAssembly(culture, (Version) null, ref stackMark);
    }

    /// <summary>获取指定区域性的附属程序集的指定版本。</summary>
    /// <returns>指定的附属程序集。</returns>
    /// <param name="culture">指定的区域性。</param>
    /// <param name="version">附属程序集的版本。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="culture" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileLoadException">找到了具有匹配文件名的附属程序集，但是 CultureInfo 或版本与指定的不匹配。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到此程序集。</exception>
    /// <exception cref="T:System.BadImageFormatException">附属程序集不是有效程序集。</exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public override Assembly GetSatelliteAssembly(CultureInfo culture, Version version)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return this.InternalAssembly.InternalGetSatelliteAssembly(culture, version, ref stackMark);
    }

    /// <summary>用给定的规范定义此程序集的非托管版本信息资源。</summary>
    /// <param name="product">用于分发此程序集的产品的名称。</param>
    /// <param name="productVersion">用于分发此程序集的产品的版本。</param>
    /// <param name="company">生产此程序集的公司名称。</param>
    /// <param name="copyright">描述应用于此程序集的所有版权声明、商标和注册商标。这包括所有声明的完整文本、合法符号、版权日期、商标号，等等。此字符串的英语表示形式应为“Copyright Microsoft Corp. 1990-2001”。</param>
    /// <param name="trademark">描述应用于此程序集的所有商标和注册商标。这包括所有声明的完整文本、合法符号、商标号，等等。该字符串的英语表示形式应为“Windows is a trademark of Microsoft Corporation”。</param>
    /// <exception cref="T:System.ArgumentException">以前定义过非托管版本信息资源。- 或 -非托管版本信息太大，无法保持。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public void DefineVersionInfoResource(string product, string productVersion, string company, string copyright, string trademark)
    {
      lock (this.SyncRoot)
        this.DefineVersionInfoResourceNoLock(product, productVersion, company, copyright, trademark);
    }

    private void DefineVersionInfoResourceNoLock(string product, string productVersion, string company, string copyright, string trademark)
    {
      if (this.m_assemblyData.m_strResourceFileName != null || this.m_assemblyData.m_resourceBytes != null || this.m_assemblyData.m_nativeVersion != null)
        throw new ArgumentException(Environment.GetResourceString("Argument_NativeResourceAlreadyDefined"));
      this.m_assemblyData.m_nativeVersion = new NativeVersionInfo();
      this.m_assemblyData.m_nativeVersion.m_strCopyright = copyright;
      this.m_assemblyData.m_nativeVersion.m_strTrademark = trademark;
      this.m_assemblyData.m_nativeVersion.m_strCompany = company;
      this.m_assemblyData.m_nativeVersion.m_strProduct = product;
      this.m_assemblyData.m_nativeVersion.m_strProductVersion = productVersion;
      this.m_assemblyData.m_hasUnmanagedVersionInfo = true;
      this.m_assemblyData.m_OverrideUnmanagedVersionInfo = true;
    }

    /// <summary>使用程序集的 AssemblyName 对象和程序集的自定义特性中指定的信息，定义非托管版本信息资源。</summary>
    /// <exception cref="T:System.ArgumentException">以前定义过非托管版本信息资源。- 或 -非托管版本信息太大，无法保持。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public void DefineVersionInfoResource()
    {
      lock (this.SyncRoot)
        this.DefineVersionInfoResourceNoLock();
    }

    private void DefineVersionInfoResourceNoLock()
    {
      if (this.m_assemblyData.m_strResourceFileName != null || this.m_assemblyData.m_resourceBytes != null || this.m_assemblyData.m_nativeVersion != null)
        throw new ArgumentException(Environment.GetResourceString("Argument_NativeResourceAlreadyDefined"));
      this.m_assemblyData.m_hasUnmanagedVersionInfo = true;
      this.m_assemblyData.m_nativeVersion = new NativeVersionInfo();
    }

    /// <summary>将此程序集的非托管资源定义为不透明的字节 Blob。</summary>
    /// <param name="resource">表示非托管资源的不透明字节 Blob。</param>
    /// <exception cref="T:System.ArgumentException">以前定义过非托管资源。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="resource" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public void DefineUnmanagedResource(byte[] resource)
    {
      if (resource == null)
        throw new ArgumentNullException("resource");
      lock (this.SyncRoot)
        this.DefineUnmanagedResourceNoLock(resource);
    }

    private void DefineUnmanagedResourceNoLock(byte[] resource)
    {
      if (this.m_assemblyData.m_strResourceFileName != null || this.m_assemblyData.m_resourceBytes != null || this.m_assemblyData.m_nativeVersion != null)
        throw new ArgumentException(Environment.GetResourceString("Argument_NativeResourceAlreadyDefined"));
      this.m_assemblyData.m_resourceBytes = new byte[resource.Length];
      Array.Copy((Array) resource, (Array) this.m_assemblyData.m_resourceBytes, resource.Length);
    }

    /// <summary>已知资源文件名，定义此程序集的非托管资源文件。</summary>
    /// <param name="resourceFileName">资源文件的名称。</param>
    /// <exception cref="T:System.ArgumentException">以前定义过非托管资源。- 或 -文件 <paramref name="resourceFileName" /> 不可读。- 或 -<paramref name="resourceFileName" /> 是空字符串 ("")。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="resourceFileName" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">未找到 <paramref name="resourceFileName" />。- 或 -<paramref name="resourceFileName" /> 是一个目录。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public void DefineUnmanagedResource(string resourceFileName)
    {
      if (resourceFileName == null)
        throw new ArgumentNullException("resourceFileName");
      lock (this.SyncRoot)
        this.DefineUnmanagedResourceNoLock(resourceFileName);
    }

    [SecurityCritical]
    private void DefineUnmanagedResourceNoLock(string resourceFileName)
    {
      if (this.m_assemblyData.m_strResourceFileName != null || this.m_assemblyData.m_resourceBytes != null || this.m_assemblyData.m_nativeVersion != null)
        throw new ArgumentException(Environment.GetResourceString("Argument_NativeResourceAlreadyDefined"));
      string str = this.m_assemblyData.m_strDir != null ? Path.Combine(this.m_assemblyData.m_strDir, resourceFileName) : Path.Combine(Environment.CurrentDirectory, resourceFileName);
      string fullPath = Path.GetFullPath(resourceFileName);
      new FileIOPermission(FileIOPermissionAccess.Read, fullPath).Demand();
      if (!File.Exists(fullPath))
        throw new FileNotFoundException(Environment.GetResourceString("IO.FileNotFound_FileName", (object) resourceFileName), resourceFileName);
      this.m_assemblyData.m_strResourceFileName = fullPath;
    }

    /// <summary>返回具有指定名称的动态模块。</summary>
    /// <returns>ModuleBuilder 对象，表示请求的动态模块。</returns>
    /// <param name="name">请求的动态模块的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 的长度为零。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public ModuleBuilder GetDynamicModule(string name)
    {
      lock (this.SyncRoot)
        return this.GetDynamicModuleNoLock(name);
    }

    private ModuleBuilder GetDynamicModuleNoLock(string name)
    {
      if (name == null)
        throw new ArgumentNullException("name");
      if (name.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
      int count = this.m_assemblyData.m_moduleBuilderList.Count;
      for (int index = 0; index < count; ++index)
      {
        ModuleBuilder moduleBuilder = this.m_assemblyData.m_moduleBuilderList[index];
        if (moduleBuilder.m_moduleData.m_strModuleName.Equals(name))
          return moduleBuilder;
      }
      return (ModuleBuilder) null;
    }

    /// <summary>设置此动态程序集的入口点，假设正在生成控制台应用程序。</summary>
    /// <param name="entryMethod">对表示此动态程序集入口点的方法的引用。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="entryMethod" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="entryMethod" /> 不包含在此程序集内。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public void SetEntryPoint(MethodInfo entryMethod)
    {
      this.SetEntryPoint(entryMethod, PEFileKinds.ConsoleApplication);
    }

    /// <summary>设置此程序集的入口点并定义正在构建的可移植执行文件（PE 文件）的类型。</summary>
    /// <param name="entryMethod">对表示此动态程序集入口点的方法的引用。</param>
    /// <param name="fileKind">正在生成的程序集可执行文件的类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="entryMethod" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="entryMethod" /> 不包含在此程序集内。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    public void SetEntryPoint(MethodInfo entryMethod, PEFileKinds fileKind)
    {
      lock (this.SyncRoot)
        this.SetEntryPointNoLock(entryMethod, fileKind);
    }

    private void SetEntryPointNoLock(MethodInfo entryMethod, PEFileKinds fileKind)
    {
      if (entryMethod == (MethodInfo) null)
        throw new ArgumentNullException("entryMethod");
      Module module = entryMethod.Module;
      if (module == (Module) null || !this.InternalAssembly.Equals((object) module.Assembly))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EntryMethodNotDefinedInAssembly"));
      this.m_assemblyData.m_entryPointMethod = entryMethod;
      this.m_assemblyData.m_peFileKind = fileKind;
      ModuleBuilder moduleBuilder = module as ModuleBuilder;
      this.m_assemblyData.m_entryPointModule = !((Module) moduleBuilder != (Module) null) ? this.GetModuleBuilder((InternalModuleBuilder) module) : moduleBuilder;
      this.m_assemblyData.m_entryPointModule.SetEntryPoint(this.m_assemblyData.m_entryPointModule.GetMethodToken(entryMethod));
    }

    /// <summary>使用指定的自定义特性 Blob 设置此程序集上的自定义特性。</summary>
    /// <param name="con">自定义属性的构造函数。</param>
    /// <param name="binaryAttribute">表示属性的字节 Blob。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="con" /> 或 <paramref name="binaryAttribute" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="con" /> 不是 RuntimeConstructorInfo。</exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
    {
      if (con == (ConstructorInfo) null)
        throw new ArgumentNullException("con");
      if (binaryAttribute == null)
        throw new ArgumentNullException("binaryAttribute");
      lock (this.SyncRoot)
        this.SetCustomAttributeNoLock(con, binaryAttribute);
    }

    [SecurityCritical]
    private void SetCustomAttributeNoLock(ConstructorInfo con, byte[] binaryAttribute)
    {
      TypeBuilder.DefineCustomAttribute(this.m_manifestModuleBuilder, 536870913, this.m_manifestModuleBuilder.GetConstructorToken(con).Token, binaryAttribute, false, typeof (DebuggableAttribute) == con.DeclaringType);
      if (this.m_assemblyData.m_access == AssemblyBuilderAccess.Run)
        return;
      this.m_assemblyData.AddCustomAttribute(con, binaryAttribute);
    }

    /// <summary>使用自定义特性生成器设置此程序集的自定义特性。</summary>
    /// <param name="customBuilder">定义自定义属性的帮助器类的实例。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="con" /> 为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    [SecuritySafeCritical]
    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
      if (customBuilder == null)
        throw new ArgumentNullException("customBuilder");
      lock (this.SyncRoot)
        this.SetCustomAttributeNoLock(customBuilder);
    }

    [SecurityCritical]
    private void SetCustomAttributeNoLock(CustomAttributeBuilder customBuilder)
    {
      customBuilder.CreateCustomAttribute(this.m_manifestModuleBuilder, 536870913);
      if (this.m_assemblyData.m_access == AssemblyBuilderAccess.Run)
        return;
      this.m_assemblyData.AddCustomAttribute(customBuilder);
    }

    /// <summary>将此动态程序集保存到磁盘。</summary>
    /// <param name="assemblyFileName">程序集的文件名。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="assemblyFileName" /> 的长度为零。- 或 -程序集中有两个或更多的模块资源文件具有相同的名称。- 或 -程序集的目标目录无效。- 或 -<paramref name="assemblyFileName" /> 不是简单文件名（例如，有目录或驱动器组件），或者此程序集中定义了一个以上非托管资源（包括版本信息资源）。- 或 -<see cref="T:System.Reflection.AssemblyCultureAttribute" /> 中的 CultureInfo 字符串不是有效字符串，并且在调用此方法之前调用了 <see cref="M:System.Reflection.Emit.AssemblyBuilder.DefineVersionInfoResource(System.String,System.String,System.String,System.String,System.String)" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyFileName" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">以前已保存过此程序集。- 或 -此程序集具有访问 Run<see cref="T:System.Reflection.Emit.AssemblyBuilderAccess" /> 的权限</exception>
    /// <exception cref="T:System.IO.IOException">在保存期间发生输出错误。</exception>
    /// <exception cref="T:System.NotSupportedException">还没有为要写到磁盘的程序集的模块中的任何类型调用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public void Save(string assemblyFileName)
    {
      this.Save(assemblyFileName, PortableExecutableKinds.ILOnly, ImageFileMachine.I386);
    }

    /// <summary>将此动态程序集保存到磁盘中，在程序集可执行文件和目标平台中指定代码的特性。</summary>
    /// <param name="assemblyFileName">程序集的文件名。</param>
    /// <param name="portableExecutableKind">
    /// <see cref="T:System.Reflection.PortableExecutableKinds" /> 值的位组合，该值用于指定代码的特性。</param>
    /// <param name="imageFileMachine">
    /// <see cref="T:System.Reflection.ImageFileMachine" /> 值之一，该值用于指定目标平台。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="assemblyFileName" /> 的长度为零。- 或 -程序集中有两个或更多的模块资源文件具有相同的名称。- 或 -程序集的目标目录无效。- 或 -<paramref name="assemblyFileName" /> 不是简单文件名（例如，有目录或驱动器组件），或者此程序集中定义了一个以上非托管资源（包括版本信息资源）。- 或 -<see cref="T:System.Reflection.AssemblyCultureAttribute" /> 中的 CultureInfo 字符串不是有效字符串，并且在调用此方法之前调用了 <see cref="M:System.Reflection.Emit.AssemblyBuilder.DefineVersionInfoResource(System.String,System.String,System.String,System.String,System.String)" />。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyFileName" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">以前已保存过此程序集。- 或 -此程序集具有访问 Run<see cref="T:System.Reflection.Emit.AssemblyBuilderAccess" /> 的权限</exception>
    /// <exception cref="T:System.IO.IOException">在保存期间发生输出错误。</exception>
    /// <exception cref="T:System.NotSupportedException">还没有为要写到磁盘的程序集的模块中的任何类型调用 <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />。</exception>
    [SecuritySafeCritical]
    public void Save(string assemblyFileName, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
    {
      lock (this.SyncRoot)
        this.SaveNoLock(assemblyFileName, portableExecutableKind, imageFileMachine);
    }

    [SecurityCritical]
    private void SaveNoLock(string assemblyFileName, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
    {
      if (assemblyFileName == null)
        throw new ArgumentNullException("assemblyFileName");
      if (assemblyFileName.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "assemblyFileName");
      if (!string.Equals(assemblyFileName, Path.GetFileName(assemblyFileName)))
        throw new ArgumentException(Environment.GetResourceString("Argument_NotSimpleFileName"), "assemblyFileName");
      int[] numArray1 = (int[]) null;
      int[] numArray2 = (int[]) null;
      string s = (string) null;
      try
      {
        if (this.m_assemblyData.m_iCABuilder != 0)
          numArray1 = new int[this.m_assemblyData.m_iCABuilder];
        if (this.m_assemblyData.m_iCAs != 0)
          numArray2 = new int[this.m_assemblyData.m_iCAs];
        if (this.m_assemblyData.m_isSaved)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AssemblyHasBeenSaved", (object) this.InternalAssembly.GetSimpleName()));
        if ((this.m_assemblyData.m_access & AssemblyBuilderAccess.Save) != AssemblyBuilderAccess.Save)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CantSaveTransientAssembly"));
        ModuleBuilder moduleWithFileName = this.m_assemblyData.FindModuleWithFileName(assemblyFileName);
        if ((Module) moduleWithFileName != (Module) null)
        {
          this.m_onDiskAssemblyModuleBuilder = moduleWithFileName;
          moduleWithFileName.m_moduleData.FileToken = 0;
        }
        else
          this.m_assemblyData.CheckFileNameConflict(assemblyFileName);
        if (this.m_assemblyData.m_strDir == null)
          this.m_assemblyData.m_strDir = Environment.CurrentDirectory;
        else if (!Directory.Exists(this.m_assemblyData.m_strDir))
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDirectory", (object) this.m_assemblyData.m_strDir));
        assemblyFileName = Path.Combine(this.m_assemblyData.m_strDir, assemblyFileName);
        assemblyFileName = Path.GetFullPath(assemblyFileName);
        new FileIOPermission(FileIOPermissionAccess.Write | FileIOPermissionAccess.Append, assemblyFileName).Demand();
        if ((Module) moduleWithFileName != (Module) null)
        {
          for (int index = 0; index < this.m_assemblyData.m_iCABuilder; ++index)
            numArray1[index] = this.m_assemblyData.m_CABuilders[index].PrepareCreateCustomAttributeToDisk(moduleWithFileName);
          for (int index = 0; index < this.m_assemblyData.m_iCAs; ++index)
            numArray2[index] = moduleWithFileName.InternalGetConstructorToken(this.m_assemblyData.m_CACons[index], true).Token;
          moduleWithFileName.PreSave(assemblyFileName, portableExecutableKind, imageFileMachine);
        }
        AssemblyBuilder.PrepareForSavingManifestToDisk(this.GetNativeHandle(), (Module) moduleWithFileName != (Module) null ? moduleWithFileName.ModuleHandle.GetRuntimeModule() : (RuntimeModule) null);
        ModuleBuilder assemblyModuleBuilder = this.GetOnDiskAssemblyModuleBuilder();
        if (this.m_assemblyData.m_strResourceFileName != null)
          assemblyModuleBuilder.DefineUnmanagedResourceFileInternalNoLock(this.m_assemblyData.m_strResourceFileName);
        else if (this.m_assemblyData.m_resourceBytes != null)
          assemblyModuleBuilder.DefineUnmanagedResourceInternalNoLock(this.m_assemblyData.m_resourceBytes);
        else if (this.m_assemblyData.m_hasUnmanagedVersionInfo)
        {
          this.m_assemblyData.FillUnmanagedVersionInfo();
          string fileVersion = this.m_assemblyData.m_nativeVersion.m_strFileVersion ?? this.GetVersion().ToString();
          AssemblyBuilder.CreateVersionInfoResource(assemblyFileName, this.m_assemblyData.m_nativeVersion.m_strTitle, (string) null, this.m_assemblyData.m_nativeVersion.m_strDescription, this.m_assemblyData.m_nativeVersion.m_strCopyright, this.m_assemblyData.m_nativeVersion.m_strTrademark, this.m_assemblyData.m_nativeVersion.m_strCompany, this.m_assemblyData.m_nativeVersion.m_strProduct, this.m_assemblyData.m_nativeVersion.m_strProductVersion, fileVersion, this.m_assemblyData.m_nativeVersion.m_lcid, this.m_assemblyData.m_peFileKind == PEFileKinds.Dll, JitHelpers.GetStringHandleOnStack(ref s));
          assemblyModuleBuilder.DefineUnmanagedResourceFileInternalNoLock(s);
        }
        if ((Module) moduleWithFileName == (Module) null)
        {
          for (int index = 0; index < this.m_assemblyData.m_iCABuilder; ++index)
            numArray1[index] = this.m_assemblyData.m_CABuilders[index].PrepareCreateCustomAttributeToDisk(assemblyModuleBuilder);
          for (int index = 0; index < this.m_assemblyData.m_iCAs; ++index)
            numArray2[index] = assemblyModuleBuilder.InternalGetConstructorToken(this.m_assemblyData.m_CACons[index], true).Token;
        }
        int count1 = this.m_assemblyData.m_moduleBuilderList.Count;
        for (int index = 0; index < count1; ++index)
        {
          ModuleBuilder moduleBuilder = this.m_assemblyData.m_moduleBuilderList[index];
          if (!moduleBuilder.IsTransient() && (Module) moduleBuilder != (Module) moduleWithFileName)
          {
            string str = moduleBuilder.m_moduleData.m_strFileName;
            if (this.m_assemblyData.m_strDir != null)
              str = Path.GetFullPath(Path.Combine(this.m_assemblyData.m_strDir, str));
            new FileIOPermission(FileIOPermissionAccess.Write | FileIOPermissionAccess.Append, str).Demand();
            moduleBuilder.m_moduleData.FileToken = AssemblyBuilder.AddFile(this.GetNativeHandle(), moduleBuilder.m_moduleData.m_strFileName);
            moduleBuilder.PreSave(str, portableExecutableKind, imageFileMachine);
            moduleBuilder.Save(str, false, portableExecutableKind, imageFileMachine);
            AssemblyBuilder.SetFileHashValue(this.GetNativeHandle(), moduleBuilder.m_moduleData.FileToken, str);
          }
        }
        for (int index = 0; index < this.m_assemblyData.m_iPublicComTypeCount; ++index)
        {
          Type type = this.m_assemblyData.m_publicComTypeList[index];
          if (type is RuntimeType)
          {
            ModuleBuilder moduleBuilder = this.GetModuleBuilder((InternalModuleBuilder) type.Module);
            if ((Module) moduleBuilder != (Module) moduleWithFileName)
              this.DefineNestedComType(type, moduleBuilder.m_moduleData.FileToken, type.MetadataToken);
          }
          else
          {
            TypeBuilder typeBuilder = (TypeBuilder) type;
            ModuleBuilder moduleBuilder = typeBuilder.GetModuleBuilder();
            if ((Module) moduleBuilder != (Module) moduleWithFileName)
              this.DefineNestedComType(type, moduleBuilder.m_moduleData.FileToken, typeBuilder.MetadataTokenInternal);
          }
        }
        if ((Module) assemblyModuleBuilder != (Module) this.m_manifestModuleBuilder)
        {
          for (int index = 0; index < this.m_assemblyData.m_iCABuilder; ++index)
            this.m_assemblyData.m_CABuilders[index].CreateCustomAttribute(assemblyModuleBuilder, 536870913, numArray1[index], true);
          for (int index = 0; index < this.m_assemblyData.m_iCAs; ++index)
            TypeBuilder.DefineCustomAttribute(assemblyModuleBuilder, 536870913, numArray2[index], this.m_assemblyData.m_CABytes[index], true, false);
        }
        if (this.m_assemblyData.m_RequiredPset != null)
          this.AddDeclarativeSecurity(this.m_assemblyData.m_RequiredPset, SecurityAction.RequestMinimum);
        if (this.m_assemblyData.m_RefusedPset != null)
          this.AddDeclarativeSecurity(this.m_assemblyData.m_RefusedPset, SecurityAction.RequestRefuse);
        if (this.m_assemblyData.m_OptionalPset != null)
          this.AddDeclarativeSecurity(this.m_assemblyData.m_OptionalPset, SecurityAction.RequestOptional);
        int count2 = this.m_assemblyData.m_resWriterList.Count;
        for (int index = 0; index < count2; ++index)
        {
          ResWriterData resWriterData = (ResWriterData) null;
          try
          {
            resWriterData = this.m_assemblyData.m_resWriterList[index];
            if (resWriterData.m_resWriter != null)
              new FileIOPermission(FileIOPermissionAccess.Write | FileIOPermissionAccess.Append, resWriterData.m_strFullFileName).Demand();
          }
          finally
          {
            if (resWriterData != null && resWriterData.m_resWriter != null)
              resWriterData.m_resWriter.Close();
          }
          AssemblyBuilder.AddStandAloneResource(this.GetNativeHandle(), resWriterData.m_strName, resWriterData.m_strFileName, resWriterData.m_strFullFileName, (int) resWriterData.m_attribute);
        }
        if ((Module) moduleWithFileName == (Module) null)
        {
          assemblyModuleBuilder.DefineNativeResource(portableExecutableKind, imageFileMachine);
          int entryPoint = (Module) this.m_assemblyData.m_entryPointModule != (Module) null ? this.m_assemblyData.m_entryPointModule.m_moduleData.FileToken : 0;
          AssemblyBuilder.SaveManifestToDisk(this.GetNativeHandle(), assemblyFileName, entryPoint, (int) this.m_assemblyData.m_peFileKind, (int) portableExecutableKind, (int) imageFileMachine);
        }
        else
        {
          if ((Module) this.m_assemblyData.m_entryPointModule != (Module) null && (Module) this.m_assemblyData.m_entryPointModule != (Module) moduleWithFileName)
            moduleWithFileName.SetEntryPoint(new MethodToken(this.m_assemblyData.m_entryPointModule.m_moduleData.FileToken));
          moduleWithFileName.Save(assemblyFileName, true, portableExecutableKind, imageFileMachine);
        }
        this.m_assemblyData.m_isSaved = true;
      }
      finally
      {
        if (s != null)
          File.Delete(s);
      }
    }

    [SecurityCritical]
    private void AddDeclarativeSecurity(PermissionSet pset, SecurityAction action)
    {
      byte[] numArray = pset.EncodeXml();
      RuntimeAssembly nativeHandle = this.GetNativeHandle();
      int num = (int) action;
      byte[] blob = numArray;
      int length = blob.Length;
      AssemblyBuilder.AddDeclarativeSecurity(nativeHandle, (SecurityAction) num, blob, length);
    }

    internal bool IsPersistable()
    {
      return (this.m_assemblyData.m_access & AssemblyBuilderAccess.Save) == AssemblyBuilderAccess.Save;
    }

    [SecurityCritical]
    private int DefineNestedComType(Type type, int tkResolutionScope, int tkTypeDef)
    {
      Type declaringType = type.DeclaringType;
      if (declaringType == (Type) null)
        return AssemblyBuilder.AddExportedTypeOnDisk(this.GetNativeHandle(), type.FullName, tkResolutionScope, tkTypeDef, type.Attributes);
      tkResolutionScope = this.DefineNestedComType(declaringType, tkResolutionScope, tkTypeDef);
      return AssemblyBuilder.AddExportedTypeOnDisk(this.GetNativeHandle(), type.Name, tkResolutionScope, tkTypeDef, type.Attributes);
    }

    [SecurityCritical]
    internal int DefineExportedTypeInMemory(Type type, int tkResolutionScope, int tkTypeDef)
    {
      Type declaringType = type.DeclaringType;
      if (declaringType == (Type) null)
        return AssemblyBuilder.AddExportedTypeInMemory(this.GetNativeHandle(), type.FullName, tkResolutionScope, tkTypeDef, type.Attributes);
      tkResolutionScope = this.DefineExportedTypeInMemory(declaringType, tkResolutionScope, tkTypeDef);
      return AssemblyBuilder.AddExportedTypeInMemory(this.GetNativeHandle(), type.Name, tkResolutionScope, tkTypeDef, type.Attributes);
    }

    void _AssemblyBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _AssemblyBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _AssemblyBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _AssemblyBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void DefineDynamicModule(RuntimeAssembly containingAssembly, bool emitSymbolInfo, string name, string filename, StackCrawlMarkHandle stackMark, ref IntPtr pInternalSymWriter, ObjectHandleOnStack retModule, bool fIsTransient, out int tkFile);

    [SecurityCritical]
    private static Module DefineDynamicModule(RuntimeAssembly containingAssembly, bool emitSymbolInfo, string name, string filename, ref StackCrawlMark stackMark, ref IntPtr pInternalSymWriter, bool fIsTransient, out int tkFile)
    {
      RuntimeModule o = (RuntimeModule) null;
      AssemblyBuilder.DefineDynamicModule(containingAssembly.GetNativeHandle(), emitSymbolInfo, name, filename, JitHelpers.GetStackCrawlMarkHandle(ref stackMark), ref pInternalSymWriter, JitHelpers.GetObjectHandleOnStack<RuntimeModule>(ref o), fIsTransient, out tkFile);
      return (Module) o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void PrepareForSavingManifestToDisk(RuntimeAssembly assembly, RuntimeModule assemblyModule);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void SaveManifestToDisk(RuntimeAssembly assembly, string strFileName, int entryPoint, int fileKind, int portableExecutableKind, int ImageFileMachine);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int AddFile(RuntimeAssembly assembly, string strFileName);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void SetFileHashValue(RuntimeAssembly assembly, int tkFile, string strFullFileName);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int AddExportedTypeInMemory(RuntimeAssembly assembly, string strComTypeName, int tkAssemblyRef, int tkTypeDef, TypeAttributes flags);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int AddExportedTypeOnDisk(RuntimeAssembly assembly, string strComTypeName, int tkAssemblyRef, int tkTypeDef, TypeAttributes flags);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void AddStandAloneResource(RuntimeAssembly assembly, string strName, string strFileName, string strFullFileName, int attribute);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void AddDeclarativeSecurity(RuntimeAssembly assembly, SecurityAction action, byte[] blob, int length);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void CreateVersionInfoResource(string filename, string title, string iconFilename, string description, string copyright, string trademark, string company, string product, string productVersion, string fileVersion, int lcid, bool isDll, StringHandleOnStack retFileName);

    private class AssemblyBuilderLock
    {
    }
  }
}
