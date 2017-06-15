// Decompiled with JetBrains decompiler
// Type: System.Reflection.Assembly
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System.Reflection
{
  /// <summary>表示一个程序集，它是一个可重用、无版本冲突并且可自我描述的公共语言运行时应用程序构建基块。</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_Assembly))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
  public abstract class Assembly : _Assembly, IEvidenceFactory, ICustomAttributeProvider, ISerializable
  {
    /// <summary>获取最初指定的程序集的位置，例如，在 <see cref="T:System.Reflection.AssemblyName" /> 对象中指定的位置。</summary>
    /// <returns>程序集的位置（按照最初的指定）。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public virtual string CodeBase
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>获取 URI，包括表示基本代码的转义符。</summary>
    /// <returns>带有转义符的 URI。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public virtual string EscapedCodeBase
    {
      [SecuritySafeCritical] get
      {
        return AssemblyName.EscapeCodeBase(this.CodeBase);
      }
    }

    /// <summary>获取程序集的显示名称。</summary>
    /// <returns>程序集的显示名称。</returns>
    [__DynamicallyInvokable]
    public virtual string FullName
    {
      [__DynamicallyInvokable] get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>获取此程序集的入口点。</summary>
    /// <returns>表示此程序集入口点的对象。如果没有找到入口点（例如，此程序集是一个 DLL），则返回 null。</returns>
    [__DynamicallyInvokable]
    public virtual MethodInfo EntryPoint
    {
      [__DynamicallyInvokable] get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>获取此程序集中定义的公共类型的集合，这些公共类型在程序集外可见。</summary>
    /// <returns>此程序集中定义的公共类型的集合，这些公共类型在程序集外可见。</returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<Type> ExportedTypes
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<Type>) this.GetExportedTypes();
      }
    }

    /// <summary>获取定义在此程序集中的类型的集合。</summary>
    /// <returns>定义在此程序集中的类型的集合。</returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<TypeInfo> DefinedTypes
    {
      [__DynamicallyInvokable] get
      {
        Type[] types = this.GetTypes();
        TypeInfo[] typeInfoArray = new TypeInfo[types.Length];
        for (int index = 0; index < types.Length; ++index)
        {
          TypeInfo typeInfo = types[index].GetTypeInfo();
          if ((Type) typeInfo == (Type) null)
            throw new NotSupportedException(Environment.GetResourceString("NotSupported_NoTypeInfo", (object) types[index].FullName));
          typeInfoArray[index] = typeInfo;
        }
        return (IEnumerable<TypeInfo>) typeInfoArray;
      }
    }

    /// <summary>获取此程序集的证据。</summary>
    /// <returns>此程序集的证据。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    public virtual Evidence Evidence
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>获取当前程序集的授予集。</summary>
    /// <returns>当前程序集的授予集。</returns>
    public virtual PermissionSet PermissionSet
    {
      [SecurityCritical] get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>获取一个值，该值指示当前程序集是否是以完全信任方式加载的。</summary>
    /// <returns>如果当前程序集是以完全信任方式加载的，则为 true，否则为 false。</returns>
    public bool IsFullyTrusted
    {
      [SecuritySafeCritical] get
      {
        return this.PermissionSet.IsUnrestricted();
      }
    }

    /// <summary>获取一个值，该值指示公共语言运行时 (CLR) 对此程序集强制执行的安全规则集。</summary>
    /// <returns>CLR 对此程序集强制执行的安全规则集。</returns>
    public virtual SecurityRuleSet SecurityRuleSet
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>获取包含当前程序集清单的模块。</summary>
    /// <returns>包含程序集清单的模块。</returns>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public virtual Module ManifestModule
    {
      [__DynamicallyInvokable] get
      {
        RuntimeAssembly runtimeAssembly = this as RuntimeAssembly;
        if ((Assembly) runtimeAssembly != (Assembly) null)
          return runtimeAssembly.ManifestModule;
        throw new NotImplementedException();
      }
    }

    /// <summary>获取包含此程序集自定义属性的集合。</summary>
    /// <returns>包含此程序集自定义属性的集合。</returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<CustomAttributeData> CustomAttributes
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<CustomAttributeData>) this.GetCustomAttributesData();
      }
    }

    /// <summary>获取 <see cref="T:System.Boolean" /> 值，该值指示此程序集是否被加载到只反射上下文中。</summary>
    /// <returns>如果程序集被加载到只反射上下文而不是执行上下文中，则为 true；否则为 false。</returns>
    [ComVisible(false)]
    public virtual bool ReflectionOnly
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>获取包含此程序集中模块的集合。</summary>
    /// <returns>包含此程序集中模块的集合。</returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<Module> Modules
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<Module>) this.GetLoadedModules(true);
      }
    }

    /// <summary>获取包含清单的已加载文件的完整路径或 UNC 位置。</summary>
    /// <returns>包含清单的已加载文件的位置。如果已加载文件使用了影像复制，则该位置是该文件被影像复制后的位置。如果从字节数组加载程序集（如使用 <see cref="M:System.Reflection.Assembly.Load(System.Byte[])" /> 方法重载时），则返回值为空字符串 ("")。</returns>
    /// <exception cref="T:System.NotSupportedException">当前的程序集是动态的程序集由 <see cref="T:System.Reflection.Emit.AssemblyBuilder" /> 对象。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public virtual string Location
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>获取表示公共语言运行时 (CLR) 的版本的字符串，该信息保存在包含清单的文件中。</summary>
    /// <returns>CLR 版本的文件夹名。这不是完整路径。</returns>
    [ComVisible(false)]
    public virtual string ImageRuntimeVersion
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>获取一个值，该值指示程序集是否是从全局程序集缓存加载的。</summary>
    /// <returns>如果程序集是从全局程序集缓存加载的，则为 true；否则为 false。</returns>
    public virtual bool GlobalAssemblyCache
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>获取用于加载程序集的主机上下文。</summary>
    /// <returns>一个 <see cref="T:System.Int64" /> 值，指示用于加载程序集的主机上下文（如果有）。</returns>
    [ComVisible(false)]
    public virtual long HostContext
    {
      get
      {
        RuntimeAssembly runtimeAssembly = this as RuntimeAssembly;
        if ((Assembly) runtimeAssembly != (Assembly) null)
          return runtimeAssembly.HostContext;
        throw new NotImplementedException();
      }
    }

    /// <summary>获取一个值，该值指示当前程序集是否是通过使用反射发出在当前进程中动态生成的。</summary>
    /// <returns>如果当前程序集是在当前进程中动态生成的，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public virtual bool IsDynamic
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    /// <summary>当公共语言运行时类加载程序不能通过正常方法解析对程序集的内部模块的引用时发生。</summary>
    public virtual event ModuleResolveEventHandler ModuleResolve
    {
      [SecurityCritical] add
      {
        throw new NotImplementedException();
      }
      [SecurityCritical] remove
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.Assembly" /> 对象是否相等。</summary>
    /// <returns>如果 true 等于 <paramref name="left" />，则为 <paramref name="right" />；否则为 false。</returns>
    /// <param name="left">要与 <paramref name="right" /> 进行比较的程序集。</param>
    /// <param name="right">要与 <paramref name="left" /> 进行比较的程序集。</param>
    [__DynamicallyInvokable]
    public static bool operator ==(Assembly left, Assembly right)
    {
      if (left == right)
        return true;
      if (left == null || right == null || (left is RuntimeAssembly || right is RuntimeAssembly))
        return false;
      return left.Equals((object) right);
    }

    /// <summary>指示两个 <see cref="T:System.Reflection.Assembly" /> 对象是否不相等。</summary>
    /// <returns>如果 true 不等于 <paramref name="left" />，则为 <paramref name="right" />；否则为 false。</returns>
    /// <param name="left">要与 <paramref name="right" /> 进行比较的程序集。</param>
    /// <param name="right">要与 <paramref name="left" /> 进行比较的程序集。</param>
    [__DynamicallyInvokable]
    public static bool operator !=(Assembly left, Assembly right)
    {
      return !(left == right);
    }

    /// <summary>创建由类型的程序集的显示名称限定的类型的名称。</summary>
    /// <returns>由程序集的显示名称限定的类型的完整名称。</returns>
    /// <param name="assemblyName">程序集的显示名称。</param>
    /// <param name="typeName">类型的全名。</param>
    public static string CreateQualifiedName(string assemblyName, string typeName)
    {
      return typeName + ", " + assemblyName;
    }

    /// <summary>获取在其中定义指定类的当前加载的程序集。</summary>
    /// <returns>在其中定义指定类的程序集。</returns>
    /// <param name="type">一个对象，该对象表示将返回的程序集中的类。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 为 null。</exception>
    public static Assembly GetAssembly(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      Module module = type.Module;
      if (module == (Module) null)
        return (Assembly) null;
      return module.Assembly;
    }

    /// <summary>确定此程序集和指定的对象是否相等。</summary>
    /// <returns>如果 true 与此实例相等，则为 <paramref name="o" />；否则为 false。</returns>
    /// <param name="o">与该实例进行比较的对象。</param>
    [__DynamicallyInvokable]
    public override bool Equals(object o)
    {
      return base.Equals(o);
    }

    /// <summary>返回此实例的哈希代码。</summary>
    /// <returns>32 位有符号整数哈希代码。</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    /// <summary>已知程序集的文件名或路径，加载程序集。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="assemblyFile">包含程序集清单的文件的名称或路径。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyFile" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyFile" /> 未找到，或者您试图加载的模块没有指定文件扩展名。</exception>
    /// <exception cref="T:System.IO.FileLoadException">无法加载找的文件。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效的程序集 ；例如，在 64 位进程中的 32 位程序集。请参阅有关详细信息异常主题。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyFile" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.Security.SecurityException">指定了不是以"file://"开头的基本代码而不是所需 <see cref="T:System.Net.WebPermission" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="assemblyFile" /> 参数为空字符串 ("")。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">程序集名称的长度超过 MAX_PATH 个字符。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly LoadFrom(string assemblyFile)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoadFrom(assemblyFile, (Evidence) null, (byte[]) null, AssemblyHashAlgorithm.None, false, false, ref stackMark);
    }

    /// <summary>将给定路径的程序集加载到只反射上下文中。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="assemblyFile">包含程序集清单的文件的路径。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyFile" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyFile" /> 未找到，或者您试图加载的模块没有指定文件扩展名。</exception>
    /// <exception cref="T:System.IO.FileLoadException">
    /// <paramref name="assemblyFile" /> 已找到，但是无法加载。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyFile" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.Security.SecurityException">指定了不是以"file://"开头的基本代码而不是所需 <see cref="T:System.Net.WebPermission" />。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">程序集名称的长度超过 MAX_PATH 个字符。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="assemblyFile" /> 为空字符串 ("")。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly ReflectionOnlyLoadFrom(string assemblyFile)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoadFrom(assemblyFile, (Evidence) null, (byte[]) null, AssemblyHashAlgorithm.None, true, false, ref stackMark);
    }

    /// <summary>在给定程序集的文件名或路径并提供安全证据的情况下，加载程序集。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="assemblyFile">包含程序集清单的文件的名称或路径。</param>
    /// <param name="securityEvidence">用于加载程序集的证据。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyFile" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyFile" /> 未找到，或者您试图加载的模块没有指定文件扩展名。</exception>
    /// <exception cref="T:System.IO.FileLoadException">无法加载找的文件。- 或 -<paramref name="securityEvidence" /> 是明确的但确定为无效。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效的程序集 ；例如，在 64 位进程中的 32 位程序集。请参阅有关详细信息异常主题。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyFile" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.Security.SecurityException">指定了不是以"file://"开头的基本代码而不是所需 <see cref="T:System.Net.WebPermission" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="assemblyFile" /> 参数为空字符串 ("")。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">程序集名称的长度超过 MAX_PATH 个字符。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of LoadFrom which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly LoadFrom(string assemblyFile, Evidence securityEvidence)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoadFrom(assemblyFile, securityEvidence, (byte[]) null, AssemblyHashAlgorithm.None, false, false, ref stackMark);
    }

    /// <summary>通过给定程序集文件名或路径、安全证据、哈希值及哈希算法来加载程序集。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="assemblyFile">包含程序集清单的文件的名称或路径。</param>
    /// <param name="securityEvidence">用于加载程序集的证据。</param>
    /// <param name="hashValue">计算所得的哈希代码的值。</param>
    /// <param name="hashAlgorithm">用于对文件进行哈希处理并生成强名称的哈希算法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyFile" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyFile" /> 未找到，或者您试图加载的模块没有指定文件扩展名。</exception>
    /// <exception cref="T:System.IO.FileLoadException">无法加载找的文件。- 或 -<paramref name="securityEvidence" /> 是明确的但确定为无效。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效的程序集 ；例如，在 64 位进程中的 32 位程序集。请参阅有关详细信息异常主题。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyFile" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.Security.SecurityException">指定了不是以"file://"开头的基本代码而不是所需 <see cref="T:System.Net.WebPermission" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="assemblyFile" /> 参数为空字符串 ("")。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">程序集名称的长度超过 MAX_PATH 个字符。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of LoadFrom which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly LoadFrom(string assemblyFile, Evidence securityEvidence, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoadFrom(assemblyFile, securityEvidence, hashValue, hashAlgorithm, false, false, ref stackMark);
    }

    /// <summary>通过给定程序集文件名或路径、哈希值及哈希算法来加载程序集。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="assemblyFile">包含程序集清单的文件的名称或路径。</param>
    /// <param name="hashValue">计算所得的哈希代码的值。</param>
    /// <param name="hashAlgorithm">用于对文件进行哈希处理并生成强名称的哈希算法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyFile" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyFile" /> 未找到，或者您试图加载的模块没有指定文件扩展名。</exception>
    /// <exception cref="T:System.IO.FileLoadException">无法加载找的文件。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效的程序集 ；例如，在 64 位进程中的 32 位程序集。请参阅有关详细信息异常主题。- 或 -<paramref name="assemblyFile" /> 已使用比当前加载的版本更高版本的公共语言运行时编译。</exception>
    /// <exception cref="T:System.Security.SecurityException">指定了不是以"file://"开头的基本代码而不是所需 <see cref="T:System.Net.WebPermission" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="assemblyFile" /> 参数为空字符串 ("")。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">程序集名称的长度超过 MAX_PATH 个字符。</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly LoadFrom(string assemblyFile, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoadFrom(assemblyFile, (Evidence) null, hashValue, hashAlgorithm, false, false, ref stackMark);
    }

    /// <summary>绕过某些安全检查，将程序集加载到加载源上下文中。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="assemblyFile">包含程序集清单的文件的名称或路径。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyFile" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyFile" /> 未找到，或者您试图加载的模块没有指定文件扩展名。</exception>
    /// <exception cref="T:System.IO.FileLoadException">无法加载找的文件。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效的程序集。- 或 -<paramref name="assemblyFile" /> 已使用比当前加载的版本更高版本的公共语言运行时编译。</exception>
    /// <exception cref="T:System.Security.SecurityException">指定了不是以"file://"开头的基本代码而不是所需 <see cref="T:System.Net.WebPermission" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="assemblyFile" /> 参数为空字符串 ("")。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">程序集名称的长度超过 MAX_PATH 个字符。</exception>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly UnsafeLoadFrom(string assemblyFile)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoadFrom(assemblyFile, (Evidence) null, (byte[]) null, AssemblyHashAlgorithm.None, false, true, ref stackMark);
    }

    /// <summary>通过给定程序集的长格式名称加载程序集。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="assemblyString">程序集名称的长格式。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyString" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="assemblyString" /> 是一个长度为零的字符串。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyString" /> 未找到。</exception>
    /// <exception cref="T:System.IO.FileLoadException">无法加载找的文件。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyString" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyString" /> 更高版本编译的。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly Load(string assemblyString)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoad(assemblyString, (Evidence) null, ref stackMark, false);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static Type GetType_Compat(string assemblyString, string typeName)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      RuntimeAssembly assemblyFromResolveEvent;
      AssemblyName assemblyName = RuntimeAssembly.CreateAssemblyName(assemblyString, false, out assemblyFromResolveEvent);
      if ((Assembly) assemblyFromResolveEvent == (Assembly) null)
      {
        if (assemblyName.ContentType == AssemblyContentType.WindowsRuntime)
          return Type.GetType(typeName + ", " + assemblyString, true, false);
        assemblyFromResolveEvent = RuntimeAssembly.InternalLoadAssemblyName(assemblyName, (Evidence) null, (RuntimeAssembly) null, ref stackMark, true, false, false);
      }
      return assemblyFromResolveEvent.GetType(typeName, true, false);
    }

    /// <summary>将给定显示名称的程序集加载到只反射上下文中。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="assemblyString">程序集的显示名称，由 <see cref="P:System.Reflection.AssemblyName.FullName" /> 属性返回。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyString" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="assemblyString" /> 为空字符串 ("")。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyString" /> 未找到。</exception>
    /// <exception cref="T:System.IO.FileLoadException">
    /// <paramref name="assemblyString" /> 已找到，但是不能加载。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyString" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyString" /> 更高版本编译的。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly ReflectionOnlyLoad(string assemblyString)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoad(assemblyString, (Evidence) null, ref stackMark, true);
    }

    /// <summary>通过给定的程序集的显示名称来加载程序集，使用提供的证据将程序集加载到调用方的域中。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="assemblyString">程序集的显示名称。</param>
    /// <param name="assemblySecurity">用于加载程序集的证据。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyString" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyString" /> 未找到。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyString" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyString" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.IO.FileLoadException">无法加载找的文件。- 或 -两次用两个不同的证据加载了程序集或模块。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of Load which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly Load(string assemblyString, Evidence assemblySecurity)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoad(assemblyString, assemblySecurity, ref stackMark, false);
    }

    /// <summary>在给定程序集的 <see cref="T:System.Reflection.AssemblyName" /> 的情况下，加载程序集。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="assemblyRef">描述要加载的程序集的对象。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyRef" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyRef" /> 未找到。</exception>
    /// <exception cref="T:System.IO.FileLoadException">在 .NET for Windows Store apps 或 可移植类库, ，捕获该基类异常， <see cref="T:System.IO.IOException" />, 、 相反。无法加载找的文件。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyRef" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyRef" /> 更高版本编译的。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly Load(AssemblyName assemblyRef)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoadAssemblyName(assemblyRef, (Evidence) null, (RuntimeAssembly) null, ref stackMark, true, false, false);
    }

    /// <summary>在给定程序集的 <see cref="T:System.Reflection.AssemblyName" /> 的情况下，加载程序集。使用提供的证据将该程序集加载到调用方的域中。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="assemblyRef">描述要加载的程序集的对象。</param>
    /// <param name="assemblySecurity">用于加载程序集的证据。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assemblyRef" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyRef" /> 未找到。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyRef" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="assemblyRef" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of Load which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly Load(AssemblyName assemblyRef, Evidence assemblySecurity)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoadAssemblyName(assemblyRef, assemblySecurity, (RuntimeAssembly) null, ref stackMark, true, false, false);
    }

    /// <summary>使用部分名称从应用程序目录或从全局程序集缓存加载程序集。</summary>
    /// <returns>加载的程序集。如果未找到 <paramref name="partialName" />，此方法将返回 null。</returns>
    /// <param name="partialName">程序集的显示名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="partialName" /> 参数为 null。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="partialName" /> 更高版本编译的。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly LoadWithPartialName(string partialName)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.LoadWithPartialNameInternal(partialName, (Evidence) null, ref stackMark);
    }

    /// <summary>使用部分名称从应用程序目录或从全局程序集缓存加载程序集。使用提供的证据将该程序集加载到调用方的域中。</summary>
    /// <returns>加载的程序集。如果未找到 <paramref name="partialName" />，此方法将返回 null。</returns>
    /// <param name="partialName">程序集的显示名称。</param>
    /// <param name="securityEvidence">用于加载程序集的证据。</param>
    /// <exception cref="T:System.IO.FileLoadException">程序集或模块已加载两次具有两个不同的证据集。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="partialName" /> 参数为 null。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="partialName" /> 更高版本编译的。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly LoadWithPartialName(string partialName, Evidence securityEvidence)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.LoadWithPartialNameInternal(partialName, securityEvidence, ref stackMark);
    }

    /// <summary>加载带有基于通用对象文件格式 (COFF) 的映像的程序集，该映像包含已发出的程序集。此程序集将会加载到调用方的应用程序域中。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="rawAssembly">字节数组，它是包含已发出程序集的基于 COFF 的映像。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rawAssembly" /> 为 null。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="rawAssembly" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="rawAssembly" /> 更高版本编译的。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly Load(byte[] rawAssembly)
    {
      AppDomain.CheckLoadByteArraySupported();
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.nLoadImage(rawAssembly, (byte[]) null, (Evidence) null, ref stackMark, false, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>加载来自基于通用对象文件格式 (COFF) 的映像的程序集，该映像包含已发出的程序集。程序集被加载到调用方的应用程序域的只反射上下文中。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="rawAssembly">字节数组，它是包含已发出程序集的基于 COFF 的映像。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rawAssembly" /> 为 null。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="rawAssembly" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="rawAssembly" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.IO.FileLoadException">
    /// <paramref name="rawAssembly" /> 无法加载。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly ReflectionOnlyLoad(byte[] rawAssembly)
    {
      AppDomain.CheckReflectionOnlyLoadSupported();
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.nLoadImage(rawAssembly, (byte[]) null, (Evidence) null, ref stackMark, true, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>加载带有基于通用对象文件格式 (COFF) 的映像的程序集，此映像包含一个已发出的程序集，并且还可以选择包括程序集的符号。此程序集将会加载到调用方的应用程序域中。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="rawAssembly">字节数组，它是包含已发出程序集的基于 COFF 的映像。</param>
    /// <param name="rawSymbolStore">包含表示程序集符号的原始字节的字节数组。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rawAssembly" /> 为 null。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="rawAssembly" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="rawAssembly" /> 更高版本编译的。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore)
    {
      AppDomain.CheckLoadByteArraySupported();
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.nLoadImage(rawAssembly, rawSymbolStore, (Evidence) null, ref stackMark, false, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>加载带有基于通用对象文件格式 (COFF) 的映像的程序集，此映像包含一个已发出的程序集，并且还可选择包括符号和指定安全上下文的源。此程序集将会加载到调用方的应用程序域中。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="rawAssembly">字节数组，它是包含已发出程序集的基于 COFF 的映像。</param>
    /// <param name="rawSymbolStore">包含表示程序集符号的原始字节的字节数组。</param>
    /// <param name="securityContextSource">安全上下文的源。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rawAssembly" /> 为 null。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="rawAssembly" /> 不是有效的程序集。- 或 -<paramref name="rawAssembly" /> 已使用比当前加载的版本更高版本的公共语言运行时编译。</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">值 <paramref name="securityContextSource" /> 不是枚举值之一。</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, SecurityContextSource securityContextSource)
    {
      AppDomain.CheckLoadByteArraySupported();
      if (securityContextSource < SecurityContextSource.CurrentAppDomain || securityContextSource > SecurityContextSource.CurrentAssembly)
        throw new ArgumentOutOfRangeException("securityContextSource");
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.nLoadImage(rawAssembly, rawSymbolStore, (Evidence) null, ref stackMark, false, securityContextSource);
    }

    /// <summary>加载带有基于通用对象文件格式 (COFF) 的映像的程序集，此映像包含一个已发出的程序集，并且还可选择包括程序集的符号和证据。此程序集将会加载到调用方的应用程序域中。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="rawAssembly">字节数组，它是包含已发出程序集的基于 COFF 的映像。</param>
    /// <param name="rawSymbolStore">包含表示程序集符号的原始字节的字节数组。</param>
    /// <param name="securityEvidence">用于加载程序集的证据。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="rawAssembly" /> 为 null。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="rawAssembly" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="rawAssembly" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="securityEvidence" /> 不是 null。默认情况下，旧的 CAS 策略中未启用 .NET Framework 4； 如果未启用）， <paramref name="securityEvidence" /> 必须是 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of Load which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence)]
    public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence)
    {
      AppDomain.CheckLoadByteArraySupported();
      if (securityEvidence != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
      {
        Zone hostEvidence = securityEvidence.GetHostEvidence<Zone>();
        if (hostEvidence == null || hostEvidence.SecurityZone != SecurityZone.MyComputer)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      }
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.nLoadImage(rawAssembly, rawSymbolStore, securityEvidence, ref stackMark, false, SecurityContextSource.CurrentAssembly);
    }

    /// <summary>加载指定路径上的程序集文件的内容。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="path">要加载的文件的完全限定路径。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 参数不是绝对路径。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 参数为 null。</exception>
    /// <exception cref="T:System.IO.FileLoadException">无法加载找的文件。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="path" /> 参数为空字符串 ("") 或不存在。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="path" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="path" /> 更高版本编译的。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static Assembly LoadFile(string path)
    {
      AppDomain.CheckLoadFileSupported();
      new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery, path).Demand();
      return (Assembly) RuntimeAssembly.nLoadFile(path, (Evidence) null);
    }

    /// <summary>通过给定的程序集的路径来加载程序集，使用提供的证据将程序集加载到调用方的域中。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="path">程序集文件的完全限定路径。</param>
    /// <param name="securityEvidence">用于加载程序集的证据。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="path" /> 参数不是绝对路径。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> 参数为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="path" /> 参数为空字符串 ("") 或不存在。</exception>
    /// <exception cref="T:System.IO.FileLoadException">无法加载找的文件。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="path" /> 不是有效的程序集。- 或 -当前加载版本 2.0 或更高版本的公共语言运行时和 <paramref name="path" /> 更高版本编译的。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="securityEvidence" /> 不是 null。默认情况下，旧的 CAS 策略中未启用 .NET Framework 4； 如果未启用）， <paramref name="securityEvidence" /> 必须是 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of LoadFile which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence)]
    public static Assembly LoadFile(string path, Evidence securityEvidence)
    {
      AppDomain.CheckLoadFileSupported();
      if (securityEvidence != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery, path).Demand();
      return (Assembly) RuntimeAssembly.nLoadFile(path, securityEvidence);
    }

    /// <summary>获取包含当前执行的代码的程序集。</summary>
    /// <returns>包含当前执行的代码的程序集。</returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly GetExecutingAssembly()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.GetExecutingAssembly(ref stackMark);
    }

    /// <summary>返回方法（该方法调用当前正在执行的方法）的 <see cref="T:System.Reflection.Assembly" />。</summary>
    /// <returns>方法（该方法调用当前正在执行的方法）的 Assembly 对象。</returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Assembly GetCallingAssembly()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCallersCaller;
      return (Assembly) RuntimeAssembly.GetExecutingAssembly(ref stackMark);
    }

    /// <summary>获取默认应用程序域中的进程可执行文件。在其他的应用程序域中，这是由 <see cref="M:System.AppDomain.ExecuteAssembly(System.String)" /> 执行的第一个可执行文件。</summary>
    /// <returns>程序集是默认应用程序域中的进程可执行文件，或是由 <see cref="M:System.AppDomain.ExecuteAssembly(System.String)" /> 执行的第一个可执行文件。当从非托管代码调用时可返回 null。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    public static Assembly GetEntryAssembly()
    {
      return (AppDomain.CurrentDomain.DomainManager ?? new AppDomainManager()).EntryAssembly;
    }

    /// <summary>获取此程序集的 <see cref="T:System.Reflection.AssemblyName" />。</summary>
    /// <returns>包含此程序集的完全分析的显示名称的对象。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public virtual AssemblyName GetName()
    {
      return this.GetName(false);
    }

    /// <summary>获取此程序集的 <see cref="T:System.Reflection.AssemblyName" />，并按 <paramref name="copiedName" /> 指定的那样设置基本代码。</summary>
    /// <returns>包含此程序集的完全分析的显示名称的对象。</returns>
    /// <param name="copiedName">如果为 true，则将 <see cref="P:System.Reflection.Assembly.CodeBase" /> 设置为程序集被影像复制后的位置；如果为 false，则将 <see cref="P:System.Reflection.Assembly.CodeBase" /> 设置为原位置。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public virtual AssemblyName GetName(bool copiedName)
    {
      throw new NotImplementedException();
    }

    Type _Assembly.GetType()
    {
      return this.GetType();
    }

    /// <summary>获取程序集实例中具有指定名称的 <see cref="T:System.Type" /> 对象。</summary>
    /// <returns>表示指定类的对象，若未找到该类则为 null。</returns>
    /// <param name="name">类型的全名。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 无效。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="name" /> 需要依赖的程序集中找不到。</exception>
    /// <exception cref="T:System.IO.FileLoadException">在 .NET for Windows Store apps 或 可移植类库, ，捕获该基类异常， <see cref="T:System.IO.IOException" />, 、 相反。<paramref name="name" /> 需要一个已找到但无法加载的依赖程序集。- 或 -当前程序集被加载到仅反射上下文中，并 <paramref name="name" /> 需要一个未预先加载的依赖程序集。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="name" /> 需要依赖的程序集，但该文件不是有效的程序集。- 或 -<paramref name="name" /> 需要针对晚于当前加载的版本的运行时版本编译的一个依赖程序集。</exception>
    [__DynamicallyInvokable]
    public virtual Type GetType(string name)
    {
      return this.GetType(name, false, false);
    }

    /// <summary>获取程序集实例中具有指定名称的 <see cref="T:System.Type" /> 对象，并选择在找不到该类型时引发异常。</summary>
    /// <returns>表示指定类的对象。</returns>
    /// <param name="name">类型的全名。</param>
    /// <param name="throwOnError">true 表示在找不到该类型时引发异常；false 则表示返回 null。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 无效。- 或 - 长度 <paramref name="name" /> 超过 1024年个字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="throwOnError" /> 是 true, ，找不到该类型。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="name" /> 需要依赖的程序集中找不到。</exception>
    /// <exception cref="T:System.IO.FileLoadException">
    /// <paramref name="name" /> 需要一个已找到但无法加载的依赖程序集。- 或 -当前程序集被加载到仅反射上下文中，并 <paramref name="name" /> 需要一个未预先加载的依赖程序集。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="name" /> 需要依赖的程序集，但该文件不是有效的程序集。- 或 -<paramref name="name" /> 需要针对晚于当前加载的版本的运行时版本编译的一个依赖程序集。</exception>
    [__DynamicallyInvokable]
    public virtual Type GetType(string name, bool throwOnError)
    {
      return this.GetType(name, throwOnError, false);
    }

    /// <summary>获取程序集实例中具有指定名称的 <see cref="T:System.Type" /> 对象，带有忽略大小写和在找不到该类型时引发异常的选项。</summary>
    /// <returns>表示指定类的对象。</returns>
    /// <param name="name">类型的全名。</param>
    /// <param name="throwOnError">true 表示在找不到该类型时引发异常；false 则表示返回 null。</param>
    /// <param name="ignoreCase">如果为 true，则忽略类型名的大小写；否则，为 false。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 无效。- 或 - 长度 <paramref name="name" /> 超过 1024年个字符。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 为 null。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="throwOnError" /> 是 true, ，找不到该类型。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="name" /> 需要依赖的程序集中找不到。</exception>
    /// <exception cref="T:System.IO.FileLoadException">
    /// <paramref name="name" /> 需要一个已找到但无法加载的依赖程序集。- 或 -当前程序集被加载到仅反射上下文中，并 <paramref name="name" /> 需要一个未预先加载的依赖程序集。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="name" /> 需要依赖的程序集，但该文件不是有效的程序集。- 或 -<paramref name="name" /> 需要针对晚于当前加载的版本的运行时版本编译的一个依赖程序集。</exception>
    [__DynamicallyInvokable]
    public virtual Type GetType(string name, bool throwOnError, bool ignoreCase)
    {
      throw new NotImplementedException();
    }

    /// <summary>获取此程序集中定义的公共类型，这些公共类型在程序集外可见。</summary>
    /// <returns>一个数组，表示此程序集中定义并在程序集外可见的类型。</returns>
    /// <exception cref="T:System.NotSupportedException">该程序集是动态的程序集。</exception>
    [__DynamicallyInvokable]
    public virtual Type[] GetExportedTypes()
    {
      throw new NotImplementedException();
    }

    /// <summary>获取此程序集中定义的类型。</summary>
    /// <returns>一个数组，包含此程序集中定义的所有类型。</returns>
    /// <exception cref="T:System.Reflection.ReflectionTypeLoadException">该程序集包含一个或多个不能加载的类型。返回的数组 <see cref="P:System.Reflection.ReflectionTypeLoadException.Types" /> 此异常的属性包含 <see cref="T:System.Type" /> 加载每种类型的对象和 null 无法加载每种类型时 <see cref="P:System.Reflection.ReflectionTypeLoadException.LoaderExceptions" /> 属性包含无法加载每种类型的异常。</exception>
    [__DynamicallyInvokable]
    public virtual Type[] GetTypes()
    {
      Module[] modules = this.GetModules(false);
      int length1 = modules.Length;
      int length2 = 0;
      Type[][] typeArray1 = new Type[length1][];
      for (int index = 0; index < length1; ++index)
      {
        typeArray1[index] = modules[index].GetTypes();
        length2 += typeArray1[index].Length;
      }
      int destinationIndex = 0;
      Type[] typeArray2 = new Type[length2];
      for (int index = 0; index < length1; ++index)
      {
        int length3 = typeArray1[index].Length;
        Array.Copy((Array) typeArray1[index], 0, (Array) typeArray2, destinationIndex, length3);
        destinationIndex += length3;
      }
      return typeArray2;
    }

    /// <summary>从此程序集加载指定清单资源，清单资源的范围由指定类型的命名空间确定。</summary>
    /// <returns>如果在编译期间没有指定任何资源，或者资源对调用方不可见，则为清单资源或者为 null。</returns>
    /// <param name="type">其命名空间用于确定清单资源名的范围的类型。</param>
    /// <param name="name">所请求的清单资源的名称（区分大小写）。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 参数为空字符串 ("")。</exception>
    /// <exception cref="T:System.IO.FileLoadException">无法加载找的文件。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="name" /> 未找到。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="name" /> 不是有效的程序集。</exception>
    /// <exception cref="T:System.NotImplementedException">资源长度大于 <see cref="F:System.Int64.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public virtual Stream GetManifestResourceStream(Type type, string name)
    {
      throw new NotImplementedException();
    }

    /// <summary>从此程序集加载指定的清单资源。</summary>
    /// <returns>如果在编译期间没有指定任何资源，或者资源对调用方不可见，则为清单资源或者为 null。</returns>
    /// <param name="name">所请求的清单资源的名称（区分大小写）。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 参数为空字符串 ("")。</exception>
    /// <exception cref="T:System.IO.FileLoadException">在 .NET for Windows Store apps 或 可移植类库, ，捕获该基类异常， <see cref="T:System.IO.IOException" />, 、 相反。无法加载找的文件。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="name" /> 未找到。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="name" /> 不是有效的程序集。</exception>
    /// <exception cref="T:System.NotImplementedException">资源长度大于 <see cref="F:System.Int64.MaxValue" />。</exception>
    [__DynamicallyInvokable]
    public virtual Stream GetManifestResourceStream(string name)
    {
      throw new NotImplementedException();
    }

    /// <summary>获取指定区域性的附属程序集。</summary>
    /// <returns>指定的附属程序集。</returns>
    /// <param name="culture">指定的区域性。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="culture" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到该程序集。</exception>
    /// <exception cref="T:System.IO.FileLoadException">找到为附属程序集中具有匹配的文件名称，但 CultureInfo 与指定的一个不匹配。</exception>
    /// <exception cref="T:System.BadImageFormatException">为附属程序集中不是有效的程序集。</exception>
    public virtual Assembly GetSatelliteAssembly(CultureInfo culture)
    {
      throw new NotImplementedException();
    }

    /// <summary>获取指定区域性的附属程序集的指定版本。</summary>
    /// <returns>指定的附属程序集。</returns>
    /// <param name="culture">指定的区域性。</param>
    /// <param name="version">附属程序集的版本。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="culture" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileLoadException">找到为附属程序集中具有匹配的文件名称，但 CultureInfo 或版本与指定的一个不匹配。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到该程序集。</exception>
    /// <exception cref="T:System.BadImageFormatException">为附属程序集中不是有效的程序集。</exception>
    public virtual Assembly GetSatelliteAssembly(CultureInfo culture, Version version)
    {
      throw new NotImplementedException();
    }

    /// <summary>获取序列化信息，其中包含重新实例化此程序集所需的所有数据。</summary>
    /// <param name="info">用序列化信息填充的对象。</param>
    /// <param name="context">序列化的目标上下文。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="info" /> 为 null。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      throw new NotImplementedException();
    }

    /// <summary>获取此程序集的所有自定义属性。</summary>
    /// <returns>包含此程序集自定义属性的数组。</returns>
    /// <param name="inherit">对于 <see cref="T:System.Reflection.Assembly" /> 类型的对象，将忽略此参数。</param>
    [__DynamicallyInvokable]
    public virtual object[] GetCustomAttributes(bool inherit)
    {
      throw new NotImplementedException();
    }

    /// <summary>获取按类型指定的此程序集的自定义属性。</summary>
    /// <returns>一个数组，它包含由 <paramref name="attributeType" /> 指定的此程序集的自定义属性。</returns>
    /// <param name="attributeType">要为其返回自定义属性的类型。</param>
    /// <param name="inherit">对于 <see cref="T:System.Reflection.Assembly" /> 类型的对象，将忽略此参数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 不是运行时类型。</exception>
    [__DynamicallyInvokable]
    public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      throw new NotImplementedException();
    }

    /// <summary>指示指定的属性是否已应用于该程序集。</summary>
    /// <returns>如果已将该属性应用于程序集，则为 true；否则为 false。</returns>
    /// <param name="attributeType">要为此程序集检查的属性类型。</param>
    /// <param name="inherit">对于该类型的对象，将忽略此参数。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="attributeType" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="attributeType" /> 使用无效的类型。</exception>
    [__DynamicallyInvokable]
    public virtual bool IsDefined(Type attributeType, bool inherit)
    {
      throw new NotImplementedException();
    }

    /// <summary>返回有关已应用于当前 <see cref="T:System.Reflection.Assembly" />（表示为 <see cref="T:System.Reflection.CustomAttributeData" /> 对象）的特性的信息。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.CustomAttributeData" /> 对象的泛型列表，这些对象表示有关已应用于当前程序集的特性的数据。</returns>
    public virtual IList<CustomAttributeData> GetCustomAttributesData()
    {
      throw new NotImplementedException();
    }

    /// <summary>加载带有基于通用对象文件格式 (COFF) 的映像（包含已发出的模块）或资源文件的模块（该模块相对于此程序集是内部的）。</summary>
    /// <returns>加载的模块。</returns>
    /// <param name="moduleName">模块的名称。此字符串必须与程序集清单中的文件名对应。</param>
    /// <param name="rawModule">基于 COFF 映像的字节数组，该数组包含发送的模块或资源。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="moduleName" /> 或 <paramref name="rawModule" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="moduleName" /> 与此程序集清单中的文件条目不匹配。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="rawModule" /> 不是有效的模块。</exception>
    /// <exception cref="T:System.IO.FileLoadException">无法加载找的文件。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    public Module LoadModule(string moduleName, byte[] rawModule)
    {
      return this.LoadModule(moduleName, rawModule, (byte[]) null);
    }

    /// <summary>加载带有基于通用对象文件格式 (COFF) 的映像（包含已发出的模块）或资源文件的模块（该模块相对于此程序集是内部的）。还加载表示此模块的符号的原始字节。</summary>
    /// <returns>加载的模块。</returns>
    /// <param name="moduleName">模块的名称。此字符串必须与程序集清单中的文件名对应。</param>
    /// <param name="rawModule">基于 COFF 映像的字节数组，该数组包含发送的模块或资源。</param>
    /// <param name="rawSymbolStore">一个字节数组，包含表示模块符号的原始字节。如果这是一个资源文件，则必须为 null。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="moduleName" /> 或 <paramref name="rawModule" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="moduleName" /> 与此程序集清单中的文件条目不匹配。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="rawModule" /> 不是有效的模块。</exception>
    /// <exception cref="T:System.IO.FileLoadException">无法加载找的文件。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
    /// </PermissionSet>
    public virtual Module LoadModule(string moduleName, byte[] rawModule, byte[] rawSymbolStore)
    {
      throw new NotImplementedException();
    }

    /// <summary>使用区分大小写的搜索，从此程序集中查找指定的类型，然后使用系统激活器创建它的实例。</summary>
    /// <returns>使用默认构造函数创建的指定类型的实例；如果未找到 null 则为 <paramref name="typeName" />。该类型使用默认联编程序解析，而无需指定区域性或激活属性，并将 <see cref="T:System.Reflection.BindingFlags" /> 设置为 Public 或 Instance。</returns>
    /// <param name="typeName">要查找类型的 <see cref="P:System.Type.FullName" />。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="typeName" /> 为空字符串 ("") 或 null 字符开头的字符串。- 或 -当前程序集被加载到仅反射上下文。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的构造函数。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="typeName" /> 需要依赖的程序集中找不到。</exception>
    /// <exception cref="T:System.IO.FileLoadException">
    /// <paramref name="typeName" /> 需要一个已找到但无法加载的依赖程序集。- 或 -当前程序集被加载到仅反射上下文中，并 <paramref name="typeName" /> 需要一个未预先加载的依赖程序集。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="typeName" /> 需要依赖的程序集，但该文件不是有效的程序集。- 或 -<paramref name="typeName" /> 需要一个已编译的依赖程序集针对晚于当前加载的版本的运行时版本。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public object CreateInstance(string typeName)
    {
      return this.CreateInstance(typeName, false, BindingFlags.Instance | BindingFlags.Public, (Binder) null, (object[]) null, (CultureInfo) null, (object[]) null);
    }

    /// <summary>使用可选的区分大小写搜索，从此程序集中查找指定的类型，然后使用系统激活器创建它的实例。</summary>
    /// <returns>使用默认构造函数创建的指定类型的实例；如果未找到 null 则为 <paramref name="typeName" />。该类型使用默认联编程序解析，而无需指定区域性或激活属性，并将 <see cref="T:System.Reflection.BindingFlags" /> 设置为 Public 或 Instance。</returns>
    /// <param name="typeName">要查找类型的 <see cref="P:System.Type.FullName" />。</param>
    /// <param name="ignoreCase">如果为 true，则忽略类型名的大小写；否则，为 false。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="typeName" /> 为空字符串 ("") 或 null 字符开头的字符串。- 或 -当前程序集被加载到仅反射上下文。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的构造函数。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="typeName" /> 需要依赖的程序集中找不到。</exception>
    /// <exception cref="T:System.IO.FileLoadException">
    /// <paramref name="typeName" /> 需要一个已找到但无法加载的依赖程序集。- 或 -当前程序集被加载到仅反射上下文中，并 <paramref name="typeName" /> 需要一个未预先加载的依赖程序集。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="typeName" /> 需要依赖的程序集，但该文件不是有效的程序集。- 或 -<paramref name="typeName" /> 需要一个已编译的依赖程序集针对晚于当前加载的版本的运行时版本。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public object CreateInstance(string typeName, bool ignoreCase)
    {
      return this.CreateInstance(typeName, ignoreCase, BindingFlags.Instance | BindingFlags.Public, (Binder) null, (object[]) null, (CultureInfo) null, (object[]) null);
    }

    /// <summary>使用可选的区分大小写搜索并具有指定的区域性、参数和绑定及激活特性，从此程序集中查找指定的类型，并使用系统激活器创建它的实例。</summary>
    /// <returns>如果未找到 null，则为指定的类型实例或 <paramref name="typeName" />。所提供的参数用于解析类型，以及绑定用于创建实例的构造函数。</returns>
    /// <param name="typeName">要查找类型的 <see cref="P:System.Type.FullName" />。</param>
    /// <param name="ignoreCase">如果为 true，则忽略类型名的大小写；否则，为 false。</param>
    /// <param name="bindingAttr">影响执行搜索的方式的位掩码。此值是 <see cref="T:System.Reflection.BindingFlags" /> 中的位标志的组合。</param>
    /// <param name="binder">一个对象，它启用绑定、对参数类型的强制、对成员的调用，以及通过反射对 MemberInfo 对象的检索。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="args">包含要传递给构造函数的参数的数组。此参数数组在数量、顺序和类型方面必须与要调用的构造函数的参数匹配。如果需要默认的构造函数，则 <paramref name="args" /> 必须是空数组或 null。</param>
    /// <param name="culture">用于控制类型强制的 CultureInfo 的实例。如果这是 null，则使用当前线程的 CultureInfo。（例如，这对于将表示 1000 的 String 转换为 Double 值是必需的，因为不同的区域性以不同的方式表示 1000。）</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。通常，为包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组，该对象指定激活远程对象所需的 URL。此参数与客户端激活的对象相关。客户端激活是一项传统技术，保留用于向后兼容，但不建议用于新的开发。应改用 Windows Communication Foundation 来开发分布式应用程序。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="typeName" /> 为空字符串 ("") 或 null 字符开头的字符串。- 或 -当前程序集被加载到仅反射上下文。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的构造函数。</exception>
    /// <exception cref="T:System.NotSupportedException">非空激活特性数组传递给一个类型，它不会继承从 <see cref="T:System.MarshalByRefObject" />。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="typeName" /> 需要依赖的程序集中找不到。</exception>
    /// <exception cref="T:System.IO.FileLoadException">
    /// <paramref name="typeName" /> 需要一个已找到但无法加载的依赖程序集。- 或 -当前程序集被加载到仅反射上下文中，并 <paramref name="typeName" /> 需要一个未预先加载的依赖程序集。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="typeName" /> 需要依赖的程序集，但该文件不是有效的程序集。- 或 -<paramref name="typeName" /> 需要一个依赖的程序集的编译的针对晚于当前加载的版本的运行时版本。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public virtual object CreateInstance(string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
    {
      Type type = this.GetType(typeName, false, ignoreCase);
      if (type == (Type) null)
        return (object) null;
      return Activator.CreateInstance(type, bindingAttr, binder, args, culture, activationAttributes);
    }

    /// <summary>获取作为此程序集的一部分的所有加载模块。</summary>
    /// <returns>模块的数组。</returns>
    public Module[] GetLoadedModules()
    {
      return this.GetLoadedModules(false);
    }

    /// <summary>获取属于此程序集的所有已加载模块，同时指定是否包括资源模块。</summary>
    /// <returns>模块的数组。</returns>
    /// <param name="getResourceModules">true 则包括资源模块；否则，为 false。</param>
    public virtual Module[] GetLoadedModules(bool getResourceModules)
    {
      throw new NotImplementedException();
    }

    /// <summary>获取作为此程序集的一部分的所有模块。</summary>
    /// <returns>模块的数组。</returns>
    /// <exception cref="T:System.IO.FileNotFoundException">要加载的模块未指定文件扩展名。</exception>
    [__DynamicallyInvokable]
    public Module[] GetModules()
    {
      return this.GetModules(false);
    }

    /// <summary>获取属于此程序集的所有模块，同时指定是否包括资源模块。</summary>
    /// <returns>模块的数组。</returns>
    /// <param name="getResourceModules">true 则包括资源模块；否则，为 false。</param>
    public virtual Module[] GetModules(bool getResourceModules)
    {
      throw new NotImplementedException();
    }

    /// <summary>获取此程序集中的指定模块。</summary>
    /// <returns>所请求的模块，若未找到该模块则为 null。</returns>
    /// <param name="name">请求的模块的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 参数为空字符串 ("")。</exception>
    /// <exception cref="T:System.IO.FileLoadException">无法加载找的文件。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="name" /> 未找到。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="name" /> 不是有效的程序集。</exception>
    public virtual Module GetModule(string name)
    {
      throw new NotImplementedException();
    }

    /// <summary>获取此程序集清单的文件表中指定文件的 <see cref="T:System.IO.FileStream" />。</summary>
    /// <returns>包含指定文件的流，如果找不到文件则为 null。</returns>
    /// <param name="name">指定文件的名称。不包括文件的路径。</param>
    /// <exception cref="T:System.IO.FileLoadException">无法加载找的文件。</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="name" /> 参数为空字符串 ("")。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="name" /> 未找到。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="name" /> 不是有效的程序集。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public virtual FileStream GetFile(string name)
    {
      throw new NotImplementedException();
    }

    /// <summary>获取程序集清单文件表中的文件。</summary>
    /// <returns>包含这些文件的流数组。</returns>
    /// <exception cref="T:System.IO.FileLoadException">无法加载找的文件。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到一个文件。</exception>
    /// <exception cref="T:System.BadImageFormatException">一个文件不是有效的程序集。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public virtual FileStream[] GetFiles()
    {
      return this.GetFiles(false);
    }

    /// <summary>获取程序集清单的文件表中的文件，指定是否包括资源模块。</summary>
    /// <returns>包含这些文件的流数组。</returns>
    /// <param name="getResourceModules">true 则包括资源模块；否则，为 false。</param>
    /// <exception cref="T:System.IO.FileLoadException">无法加载找的文件。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">找不到一个文件。</exception>
    /// <exception cref="T:System.BadImageFormatException">一个文件不是有效的程序集。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public virtual FileStream[] GetFiles(bool getResourceModules)
    {
      throw new NotImplementedException();
    }

    /// <summary>返回此程序集中的所有资源的名称。</summary>
    /// <returns>包含所有资源名称的数组。</returns>
    [__DynamicallyInvokable]
    public virtual string[] GetManifestResourceNames()
    {
      throw new NotImplementedException();
    }

    /// <summary>获取此程序集引用的所有程序集的 <see cref="T:System.Reflection.AssemblyName" /> 对象。</summary>
    /// <returns>包含此程序集引用的所有程序集的完全分析的显示名称的数组。</returns>
    public virtual AssemblyName[] GetReferencedAssemblies()
    {
      throw new NotImplementedException();
    }

    /// <summary>返回关于给定资源如何保持的信息。</summary>
    /// <returns>用关于资源拓扑的信息填充的对象；如果未找到资源，则为 null。</returns>
    /// <param name="resourceName">区分大小写的资源名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="resourceName" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="resourceName" /> 参数为空字符串 ("")。</exception>
    [__DynamicallyInvokable]
    public virtual ManifestResourceInfo GetManifestResourceInfo(string resourceName)
    {
      throw new NotImplementedException();
    }

    /// <summary>返回程序集的全名，即所谓的显示名称。</summary>
    /// <returns>程序集的全名；如果不能确定程序集的全名，则为类名。</returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return this.FullName ?? base.ToString();
    }
  }
}
