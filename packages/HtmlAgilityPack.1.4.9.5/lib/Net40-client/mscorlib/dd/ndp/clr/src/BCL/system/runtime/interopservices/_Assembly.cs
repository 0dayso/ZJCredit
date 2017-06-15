// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices._Assembly
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Policy;

namespace System.Runtime.InteropServices
{
  /// <summary>向非托管代码公开 <see cref="T:System.Reflection.Assembly" /> 类的公共成员。</summary>
  [Guid("17156360-2f1a-384a-bc52-fde93c215c5b")]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [TypeLibImportClass(typeof (Assembly))]
  [CLSCompliant(false)]
  [ComVisible(true)]
  public interface _Assembly
  {
    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.Assembly.CodeBase" /> 属性的版本无关的访问。</summary>
    /// <returns>程序集的位置（按照最初的指定）。</returns>
    string CodeBase { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.Assembly.EscapedCodeBase" /> 属性的版本无关的访问。</summary>
    /// <returns>含有转义符的统一资源标识符 (URI)。</returns>
    string EscapedCodeBase { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.Assembly.FullName" /> 属性的版本无关的访问。</summary>
    /// <returns>程序集的显示名称。</returns>
    string FullName { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.Assembly.EntryPoint" /> 属性的版本无关的访问。</summary>
    /// <returns>表示此程序集入口点的 <see cref="T:System.Reflection.MethodInfo" /> 对象。如果没有找到入口点（例如，此程序集是一个 DLL），则返回 null。</returns>
    MethodInfo EntryPoint { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.Assembly.Location" /> 属性的版本无关的访问。</summary>
    /// <returns>包含清单的已加载文件的位置。如果已加载文件使用了影像复制，则该位置是该文件被影像复制后的位置。如果从字节数组加载程序集（如使用 <see cref="M:System.Reflection.Assembly.Load(System.Byte[])" /> 方法重载时），则返回值为空字符串 ("")。</returns>
    string Location { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.Assembly.Evidence" /> 属性的版本无关的访问。</summary>
    /// <returns>此程序集的 <see cref="T:System.Security.Policy.Evidence" /> 对象。</returns>
    Evidence Evidence { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.Reflection.Assembly.GlobalAssemblyCache" /> 属性的版本无关的访问。</summary>
    /// <returns>如果程序集是从全局程序集缓存加载的，则为 true；否则为 false。</returns>
    bool GlobalAssemblyCache { get; }

    /// <summary>为 COM 对象提供对 <see cref="E:System.Reflection.Assembly.ModuleResolve" /> 事件的版本无关的访问。</summary>
    event ModuleResolveEventHandler ModuleResolve;

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.ToString" /> 方法的版本无关的访问。</summary>
    /// <returns>程序集的全名；如果不能确定程序集的全名，则为类名。</returns>
    string ToString();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Object.Equals(System.Object)" /> 方法的版本无关的访问。</summary>
    /// <returns>如果指定的 <see cref="T:System.Object" /> 等于当前的 <see cref="T:System.Object" />，则为 true；否则为 false。</returns>
    /// <param name="other">与当前的 <see cref="T:System.Object" /> 进行比较的 <see cref="T:System.Object" />。</param>
    bool Equals(object other);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Object.GetHashCode" /> 方法的版本无关的访问。</summary>
    /// <returns>当前 <see cref="T:System.Object" /> 的哈希代码。</returns>
    int GetHashCode();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Object.GetType" /> 方法的版本无关的访问。</summary>
    /// <returns>一个 <see cref="T:System.Type" /> 对象。</returns>
    Type GetType();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetName" /> 方法的版本无关的访问。</summary>
    /// <returns>此程序集的 <see cref="T:System.Reflection.AssemblyName" />。</returns>
    AssemblyName GetName();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetName(System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>此程序集的 <see cref="T:System.Reflection.AssemblyName" />。</returns>
    /// <param name="copiedName">如果为 true，则将 <see cref="P:System.Reflection.Assembly.CodeBase" /> 设置为程序集被影像复制后的位置；如果为 false，则将 <see cref="P:System.Reflection.Assembly.CodeBase" /> 设置为原位置。</param>
    AssemblyName GetName(bool copiedName);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetType(System.String)" /> 方法的版本无关的访问。</summary>
    /// <returns>表示指定类的 <see cref="T:System.Type" /> 对象，若未找到该类则为 null。</returns>
    /// <param name="name">该类型的全名。</param>
    Type GetType(string name);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetType(System.String,System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>表示指定类的 <see cref="T:System.Type" /> 对象。</returns>
    /// <param name="name">该类型的全名。</param>
    /// <param name="throwOnError">true 表示在找不到该类型时引发异常；false 则表示返回 null。</param>
    Type GetType(string name, bool throwOnError);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetExportedTypes" /> 属性的版本无关的访问。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 对象的数组，这些对象表示此程序集中定义的在程序集外可见的类型。</returns>
    Type[] GetExportedTypes();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetTypes" /> 方法的版本无关的访问。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 类型的数组，包含此程序集中定义的所有类型的对象。</returns>
    Type[] GetTypes();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetManifestResourceStream(System.Type,System.String)" /> 方法的版本无关的访问。</summary>
    /// <returns>表示此清单资源的 <see cref="T:System.IO.Stream" />。</returns>
    /// <param name="type">其命名空间用于确定清单资源名的范围的类型。</param>
    /// <param name="name">所请求的清单资源的名称（区分大小写）。</param>
    Stream GetManifestResourceStream(Type type, string name);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetManifestResourceStream(System.String)" /> 方法的版本无关的访问。</summary>
    /// <returns>表示此清单资源的 <see cref="T:System.IO.Stream" />。</returns>
    /// <param name="name">所请求的清单资源的名称（区分大小写）。</param>
    Stream GetManifestResourceStream(string name);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetFile(System.String)" /> 方法的版本无关的访问。</summary>
    /// <returns>指定文件的 <see cref="T:System.IO.FileStream" />；如果没有找到此文件，则为 null。</returns>
    /// <param name="name">指定文件的名称。不包括文件的路径。</param>
    FileStream GetFile(string name);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetFiles" /> 方法的版本无关的访问。</summary>
    /// <returns>
    /// <see cref="T:System.IO.FileStream" /> 对象的数组。</returns>
    FileStream[] GetFiles();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetFiles(System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>
    /// <see cref="T:System.IO.FileStream" /> 对象的数组。</returns>
    /// <param name="getResourceModules">为 true，则包括资源模块；否则，为 false。</param>
    FileStream[] GetFiles(bool getResourceModules);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetManifestResourceNames" /> 方法的版本无关的访问。</summary>
    /// <returns>包含所有资源名称的 String 类型的数组。</returns>
    string[] GetManifestResourceNames();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetManifestResourceInfo(System.String)" /> 方法的版本无关的访问。</summary>
    /// <returns>用关于资源拓扑的信息填充的 <see cref="T:System.Reflection.ManifestResourceInfo" /> 对象；如果未找到资源，则为 null。</returns>
    /// <param name="resourceName">区分大小写的资源名称。</param>
    ManifestResourceInfo GetManifestResourceInfo(string resourceName);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetCustomAttributes(System.Type,System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>
    /// <see cref="T:System.Object" /> 类型的数组，包含由 <paramref name="attributeType" /> 指定的此程序集的自定义属性。</returns>
    /// <param name="attributeType">要为其返回自定义属性的 <see cref="T:System.Type" />。</param>
    /// <param name="inherit">对于 <see cref="T:System.Reflection.Assembly" /> 类型的对象，将忽略此参数。</param>
    object[] GetCustomAttributes(Type attributeType, bool inherit);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetCustomAttributes(System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>Object 类型的数组，包含此程序集的自定义属性。</returns>
    /// <param name="inherit">对于 <see cref="T:System.Reflection.Assembly" /> 类型的对象，将忽略此参数。</param>
    object[] GetCustomAttributes(bool inherit);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.IsDefined(System.Type,System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>如果定义了由指定的 <see cref="T:System.Type" /> 标识的自定义特性，则为 true；否则为 false。</returns>
    /// <param name="attributeType">要为此程序集检查的自定义特性的 <see cref="T:System.Type" />。</param>
    /// <param name="inherit">对于该类型的对象，该参数被忽略。</param>
    bool IsDefined(Type attributeType, bool inherit);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> 方法的版本无关的访问。</summary>
    /// <param name="info">用序列化信息填充的对象。</param>
    /// <param name="context">序列化的目标上下文。</param>
    [SecurityCritical]
    void GetObjectData(SerializationInfo info, StreamingContext context);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetType(System.String,System.Boolean,System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>表示指定类的 <see cref="T:System.Type" /> 对象。</returns>
    /// <param name="name">该类型的全名。</param>
    /// <param name="throwOnError">true 表示在找不到该类型时引发异常；false 则表示返回 null。</param>
    /// <param name="ignoreCase">如果为 true，则忽略类型名的大小写；否则，为 false。</param>
    Type GetType(string name, bool throwOnError, bool ignoreCase);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetSatelliteAssembly(System.Globalization.CultureInfo)" /> 方法的版本无关的访问。</summary>
    /// <returns>指定的附属程序集。</returns>
    /// <param name="culture">指定的区域性。</param>
    Assembly GetSatelliteAssembly(CultureInfo culture);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetSatelliteAssembly(System.Globalization.CultureInfo,System.Version)" /> 方法的版本无关的访问。</summary>
    /// <returns>指定的附属程序集。</returns>
    /// <param name="culture">指定的区域性。</param>
    /// <param name="version">附属程序集的版本。</param>
    Assembly GetSatelliteAssembly(CultureInfo culture, Version version);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.LoadModule(System.String,System.Byte[])" /> 方法的版本无关的访问。</summary>
    /// <returns>加载的模块。</returns>
    /// <param name="moduleName">模块的名称。必须与此程序集清单中的文件名对应。</param>
    /// <param name="rawModule">基于 COFF 映像的字节数组，该数组包含发送的模块或资源。</param>
    Module LoadModule(string moduleName, byte[] rawModule);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.LoadModule(System.String,System.Byte[],System.Byte[])" /> 方法的版本无关的访问。</summary>
    /// <returns>加载的模块。</returns>
    /// <param name="moduleName">模块的名称。必须与此程序集清单中的文件名对应。</param>
    /// <param name="rawModule">基于 COFF 映像的字节数组，该数组包含发送的模块或资源。</param>
    /// <param name="rawSymbolStore">一个字节数组，包含表示模块符号的原始字节。如果这是一个资源文件，则必须为 null。</param>
    Module LoadModule(string moduleName, byte[] rawModule, byte[] rawSymbolStore);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.CreateInstance(System.String)" /> 方法的版本无关的访问。</summary>
    /// <returns>表示该类型的 <see cref="T:System.Object" /> 的实例，其区域性、参数、联编程序和激活属性设置为 null，并且 <see cref="T:System.Reflection.BindingFlags" /> 设置为 Public 或 Instance，或者设置为 null（如果没有找到 <paramref name="typeName" />）。</returns>
    /// <param name="typeName">要查找的类型的 <see cref="P:System.Type.FullName" />。</param>
    object CreateInstance(string typeName);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.CreateInstance(System.String,System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>表示该类型的 <see cref="T:System.Object" /> 的实例，其区域性、参数、联编程序和激活属性设置为 null，并且 <see cref="T:System.Reflection.BindingFlags" /> 设置为 Public 或 Instance，或者设置为 null（如果没有找到 <paramref name="typeName" />）。</returns>
    /// <param name="typeName">要查找的类型的 <see cref="P:System.Type.FullName" />。</param>
    /// <param name="ignoreCase">如果为 true，则忽略类型名的大小写；否则，为 false。</param>
    object CreateInstance(string typeName, bool ignoreCase);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.CreateInstance(System.String,System.Boolean,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo,System.Object[])" /> 方法的版本无关的访问。</summary>
    /// <returns>表示此类型且匹配指定条件的 Object 的实例；如果没有找到 <paramref name="typeName" />，则为 null。</returns>
    /// <param name="typeName">要查找的类型的 <see cref="P:System.Type.FullName" />。</param>
    /// <param name="ignoreCase">如果为 true，则忽略类型名的大小写；否则，为 false。</param>
    /// <param name="bindingAttr">影响搜索如何进行的位屏蔽。此值是 <see cref="T:System.Reflection.BindingFlags" /> 中的位标志的组合。</param>
    /// <param name="binder">一个启用绑定、参数类型强制、成员调用以及通过反射进行 MemberInfo 对象检索的对象。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="args">Object 类型的数组，包含要传递给构造函数的参数。此参数数组在数量、顺序和类型方面必须与要调用的构造函数的参数匹配。如果需要默认的构造函数，则 <paramref name="args" /> 必须是空数组或 null。</param>
    /// <param name="culture">用于控制类型强制的 CultureInfo 的实例。如果这是 null，则使用当前线程的 CultureInfo。（例如，这对于将表示 1000 的 String 转换为 Double 值是必需的，因为不同的区域性以不同的方式表示 1000。）</param>
    /// <param name="activationAttributes">Object 类型的数组，包含一个或多个可以参与激活的激活特性。激活特性的一个示例是：URLAttribute(http://hostname/appname/objectURI)</param>
    object CreateInstance(string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetLoadedModules" /> 方法的版本无关的访问。</summary>
    /// <returns>模块的数组。</returns>
    Module[] GetLoadedModules();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetLoadedModules(System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>模块的数组。</returns>
    /// <param name="getResourceModules">为 true，则包括资源模块；否则，为 false。</param>
    Module[] GetLoadedModules(bool getResourceModules);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetModules" /> 方法的版本无关的访问。</summary>
    /// <returns>模块的数组。</returns>
    Module[] GetModules();

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetModules(System.Boolean)" /> 方法的版本无关的访问。</summary>
    /// <returns>模块的数组。</returns>
    /// <param name="getResourceModules">为 true，则包括资源模块；否则，为 false。</param>
    Module[] GetModules(bool getResourceModules);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetModule(System.String)" /> 方法的版本无关的访问。</summary>
    /// <returns>所请求的模块，若未找到该模块则为 null。</returns>
    /// <param name="name">请求的模块的名称。</param>
    Module GetModule(string name);

    /// <summary>为 COM 对象提供对 <see cref="M:System.Reflection.Assembly.GetReferencedAssemblies" /> 方法的版本无关的访问。</summary>
    /// <returns>
    /// <see cref="T:System.Reflection.AssemblyName" /> 类型的数组，包含此程序集引用的所有程序集。</returns>
    AssemblyName[] GetReferencedAssemblies();
  }
}
