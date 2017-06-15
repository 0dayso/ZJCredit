// Decompiled with JetBrains decompiler
// Type: System._AppDomain
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Policy;
using System.Security.Principal;

namespace System
{
  /// <summary>向非托管代码公开 <see cref="T:System.AppDomain" /> 类的公共成员。</summary>
  /// <filterpriority>2</filterpriority>
  [Guid("05F696DC-2B29-3663-AD8B-C4389CF2A713")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [CLSCompliant(false)]
  [ComVisible(true)]
  public interface _AppDomain
  {
    /// <summary>为 COM 对象提供对 <see cref="P:System.AppDomain.Evidence" /> 属性的版本无关的访问。</summary>
    /// <returns>获取与此应用程序域相关联的 <see cref="T:System.Security.Policy.Evidence" />，它用作安全策略的输入。</returns>
    /// <filterpriority>2</filterpriority>
    Evidence Evidence { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.AppDomain.FriendlyName" /> 属性的版本无关的访问。</summary>
    /// <returns>此应用程序域的友好名称。</returns>
    /// <filterpriority>2</filterpriority>
    string FriendlyName { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.AppDomain.BaseDirectory" /> 属性的版本无关的访问。</summary>
    /// <returns>基目录，由程序集冲突解决程序用来探测程序集。</returns>
    /// <filterpriority>2</filterpriority>
    string BaseDirectory { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.AppDomain.RelativeSearchPath" /> 属性的版本无关的访问。</summary>
    /// <returns>基目录下的路径，在此程序集冲突解决程序应探测专用程序集。</returns>
    /// <filterpriority>2</filterpriority>
    string RelativeSearchPath { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.AppDomain.ShadowCopyFiles" /> 属性的版本无关的访问。</summary>
    /// <returns>如果应用程序域配置为影像副本文件，则为 true；否则为 false。</returns>
    /// <filterpriority>2</filterpriority>
    bool ShadowCopyFiles { get; }

    /// <summary>为 COM 对象提供对 <see cref="P:System.AppDomain.DynamicDirectory" /> 属性的版本无关的访问。</summary>
    /// <returns>获取目录，它由程序集冲突解决程序用来探测动态创建的程序集。</returns>
    /// <filterpriority>2</filterpriority>
    string DynamicDirectory { get; }

    /// <summary>为 COM 对象提供对 <see cref="E:System.AppDomain.DomainUnload" /> 事件的版本无关的访问。</summary>
    /// <filterpriority>2</filterpriority>
    event EventHandler DomainUnload;

    /// <summary>为 COM 对象提供对 <see cref="E:System.AppDomain.AssemblyLoad" /> 事件的版本无关的访问。</summary>
    /// <filterpriority>2</filterpriority>
    event AssemblyLoadEventHandler AssemblyLoad;

    /// <summary>为 COM 对象提供对 <see cref="E:System.AppDomain.ProcessExit" /> 事件的版本无关的访问。</summary>
    /// <filterpriority>2</filterpriority>
    event EventHandler ProcessExit;

    /// <summary>为 COM 对象提供对 <see cref="E:System.AppDomain.TypeResolve" /> 事件的版本无关的访问。</summary>
    /// <filterpriority>2</filterpriority>
    event ResolveEventHandler TypeResolve;

    /// <summary>为 COM 对象提供对 <see cref="E:System.AppDomain.ResourceResolve" /> 事件的版本无关的访问。</summary>
    /// <filterpriority>2</filterpriority>
    event ResolveEventHandler ResourceResolve;

    /// <summary>为 COM 对象提供对 <see cref="E:System.AppDomain.AssemblyResolve" /> 事件的版本无关的访问。</summary>
    /// <filterpriority>2</filterpriority>
    event ResolveEventHandler AssemblyResolve;

    /// <summary>为 COM 对象提供对 <see cref="E:System.AppDomain.UnhandledException" /> 事件的版本无关的访问。</summary>
    /// <filterpriority>2</filterpriority>
    event UnhandledExceptionEventHandler UnhandledException;

    /// <summary>检索对象提供的类型信息接口的数量（0 或 1）。</summary>
    /// <param name="pcTInfo">指向一个位置，该位置接收对象提供的类型信息接口的数量。</param>
    /// <exception cref="T:System.NotImplementedException">使用 COM IDispatch 接口在后期绑定调用此方法。</exception>
    void GetTypeInfoCount(out uint pcTInfo);

    /// <summary>检索对象的类型信息，然后可以使用该信息获取接口的类型信息。</summary>
    /// <param name="iTInfo">要返回的类型信息。</param>
    /// <param name="lcid">类型信息的区域设置标识符。</param>
    /// <param name="ppTInfo">接收一个指针，指向请求的类型信息对象。</param>
    /// <exception cref="T:System.NotImplementedException">使用 COM IDispatch 接口在后期绑定调用此方法。</exception>
    void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

    /// <summary>将一组名称映射为对应的一组调度标识符。</summary>
    /// <param name="riid">保留供将来使用。必须为 IID_NULL。</param>
    /// <param name="rgszNames">要映射的名称的传入数组。</param>
    /// <param name="cNames">要映射的名称的计数。</param>
    /// <param name="lcid">要在其中解释名称的区域设置上下文。</param>
    /// <param name="rgDispId">调用方分配的数组，用于接收与名称对应的 ID。</param>
    /// <exception cref="T:System.NotImplementedException">使用 COM IDispatch 接口在后期绑定调用此方法。</exception>
    void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

    /// <summary>提供对某一对象公开的属性和方法的访问。</summary>
    /// <param name="dispIdMember">标识成员。</param>
    /// <param name="riid">保留供将来使用。必须为 IID_NULL。</param>
    /// <param name="lcid">要在其中解释参数的区域设置上下文。</param>
    /// <param name="wFlags">描述调用的上下文的标志。</param>
    /// <param name="pDispParams">指向一个结构的指针，该结构包含一个参数数组、一个命名参数的 DISPID 参数数组和数组中元素数的计数。</param>
    /// <param name="pVarResult">指向要存储结果的位置的指针。</param>
    /// <param name="pExcepInfo">指向一个包含异常信息的结构的指针。</param>
    /// <param name="puArgErr">第一个出错参数的索引。</param>
    /// <exception cref="T:System.NotImplementedException">使用 COM IDispatch 接口在后期绑定调用此方法。</exception>
    void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.ToString" /> 方法的版本无关的访问。</summary>
    /// <returns>一个字符串，通过连接字符串“Name:”、应用程序域的友好名称以及上下文策略的字符串表示或字符串“There are no context policies”而成。</returns>
    /// <filterpriority>2</filterpriority>
    string ToString();

    /// <summary>为 COM 对象提供对继承的 <see cref="M:System.Object.Equals(System.Object)" /> 方法的版本无关的访问。</summary>
    /// <returns>如果指定的 <see cref="T:System.Object" /> 等于当前的 <see cref="T:System.Object" />，则为 true；否则为 false。</returns>
    /// <param name="other">要与当前 <see cref="T:System.Object" /> 进行比较的 <see cref="T:System.Object" />。</param>
    /// <filterpriority>2</filterpriority>
    bool Equals(object other);

    /// <summary>为 COM 对象提供对继承的 <see cref="M:System.Object.GetHashCode" /> 方法的版本无关的访问。</summary>
    /// <returns>当前 <see cref="T:System.Object" /> 的哈希代码。</returns>
    /// <filterpriority>2</filterpriority>
    int GetHashCode();

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.GetType" /> 方法的版本无关的访问。</summary>
    /// <returns>一个 <see cref="T:System.Type" />，表示当前实例的类型。</returns>
    /// <filterpriority>2</filterpriority>
    Type GetType();

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.InitializeLifetimeService" /> 方法的版本无关的访问。</summary>
    /// <returns>始终为 null。</returns>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    object InitializeLifetimeService();

    /// <summary>为 COM 对象提供对继承的 <see cref="M:System.MarshalByRefObject.GetLifetimeService" /> 方法的版本无关的访问。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> 类型的对象，用于控制此实例的生存期策略。</returns>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    object GetLifetimeService();

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess)" /> 方法的重载的版本无关的访问。</summary>
    /// <returns>表示创建的动态程序集。</returns>
    /// <param name="name">动态程序集的唯一标识。</param>
    /// <param name="access">动态程序集的访问模式。</param>
    /// <filterpriority>2</filterpriority>
    AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.String)" /> 方法的重载的版本无关的访问。</summary>
    /// <returns>表示创建的动态程序集。</returns>
    /// <param name="name">动态程序集的唯一标识。</param>
    /// <param name="access">访问动态程序集所采用的模式。</param>
    /// <param name="dir">保存程序集的目录的名称。如果 <paramref name="dir" /> 是 null，目录将默认为当前目录。</param>
    /// <filterpriority>2</filterpriority>
    AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.Security.Policy.Evidence)" /> 方法的重载的版本无关的访问。</summary>
    /// <returns>表示创建的动态程序集。</returns>
    /// <param name="name">动态程序集的唯一标识。</param>
    /// <param name="access">访问动态程序集所采用的模式。</param>
    /// <param name="evidence">为动态程序集提供的证据。该证据始终作为最后一组用于策略解析的证据来使用。</param>
    /// <filterpriority>2</filterpriority>
    AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, Evidence evidence);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.Security.PermissionSet,System.Security.PermissionSet,System.Security.PermissionSet)" /> 方法的重载的版本无关的访问。</summary>
    /// <returns>表示创建的动态程序集。</returns>
    /// <param name="name">动态程序集的唯一标识。</param>
    /// <param name="access">访问动态程序集所采用的模式。</param>
    /// <param name="requiredPermissions">必需的权限请求。</param>
    /// <param name="optionalPermissions">可选权限请求。</param>
    /// <param name="refusedPermissions">被拒绝的权限请求。</param>
    /// <filterpriority>2</filterpriority>
    AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.String,System.Security.Policy.Evidence)" /> 方法的重载的版本无关的访问。</summary>
    /// <returns>表示创建的动态程序集。</returns>
    /// <param name="name">动态程序集的唯一标识。</param>
    /// <param name="access">访问动态程序集所采用的模式。</param>
    /// <param name="dir">保存程序集的目录的名称。如果 <paramref name="dir" /> 是 null，目录将默认为当前目录。</param>
    /// <param name="evidence">为动态程序集提供的证据。该证据始终作为最后一组用于策略解析的证据来使用。</param>
    /// <filterpriority>2</filterpriority>
    AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.String,System.Security.PermissionSet,System.Security.PermissionSet,System.Security.PermissionSet)" /> 方法的重载的版本无关的访问。</summary>
    /// <returns>表示创建的动态程序集。</returns>
    /// <param name="name">动态程序集的唯一标识。</param>
    /// <param name="access">访问动态程序集所采用的模式。</param>
    /// <param name="dir">保存程序集的目录的名称。如果 <paramref name="dir" /> 是 null，目录将默认为当前目录。</param>
    /// <param name="requiredPermissions">必需的权限请求。</param>
    /// <param name="optionalPermissions">可选权限请求。</param>
    /// <param name="refusedPermissions">被拒绝的权限请求。</param>
    /// <filterpriority>2</filterpriority>
    AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.Security.Policy.Evidence,System.Security.PermissionSet,System.Security.PermissionSet,System.Security.PermissionSet)" /> 方法的重载的版本无关的访问。</summary>
    /// <returns>表示创建的动态程序集。</returns>
    /// <param name="name">动态程序集的唯一标识。</param>
    /// <param name="access">访问动态程序集所采用的模式。</param>
    /// <param name="evidence">为动态程序集提供的证据。该证据始终作为最后一组用于策略解析的证据来使用。</param>
    /// <param name="requiredPermissions">必需的权限请求。</param>
    /// <param name="optionalPermissions">可选权限请求。</param>
    /// <param name="refusedPermissions">被拒绝的权限请求。</param>
    /// <filterpriority>2</filterpriority>
    AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.String,System.Security.Policy.Evidence,System.Security.PermissionSet,System.Security.PermissionSet,System.Security.PermissionSet)" /> 方法的重载的版本无关的访问。</summary>
    /// <returns>表示创建的动态程序集。</returns>
    /// <param name="name">动态程序集的唯一标识。</param>
    /// <param name="access">访问动态程序集所采用的模式。</param>
    /// <param name="dir">保存程序集的目录的名称。如果 <paramref name="dir" /> 是 null，目录将默认为当前目录。</param>
    /// <param name="evidence">为动态程序集提供的证据。该证据始终作为最后一组用于策略解析的证据来使用。</param>
    /// <param name="requiredPermissions">必需的权限请求。</param>
    /// <param name="optionalPermissions">可选权限请求。</param>
    /// <param name="refusedPermissions">被拒绝的权限请求。</param>
    /// <filterpriority>2</filterpriority>
    AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.String,System.Security.Policy.Evidence,System.Security.PermissionSet,System.Security.PermissionSet,System.Security.PermissionSet,System.Boolean)" /> 方法的重载的版本无关的访问。</summary>
    /// <returns>表示创建的动态程序集。</returns>
    /// <param name="name">动态程序集的唯一标识。</param>
    /// <param name="access">访问动态程序集所采用的模式。</param>
    /// <param name="dir">保存动态程序集的目录的名称。如果 <paramref name="dir" /> 是 null，目录将默认为当前目录。</param>
    /// <param name="evidence">为动态程序集提供的证据。该证据始终作为最后一组用于策略解析的证据来使用。</param>
    /// <param name="requiredPermissions">必需的权限请求。</param>
    /// <param name="optionalPermissions">可选权限请求。</param>
    /// <param name="refusedPermissions">被拒绝的权限请求。</param>
    /// <param name="isSynchronized">若要在动态程序集中同步模块、类型和成员的创建，则为 true；否则为 false。</param>
    /// <filterpriority>2</filterpriority>
    AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, bool isSynchronized);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.CreateInstance(System.String,System.String)" /> 方法的版本无关的访问。</summary>
    /// <returns>一个对象，该对象是 <paramref name="typeName" /> 指定的新实例的包装。返回值需要打开包装才能访问真实对象。</returns>
    /// <param name="assemblyName">程序集的显示名称。请参见<see cref="P:System.Reflection.Assembly.FullName" />。</param>
    /// <param name="typeName">
    /// <see cref="P:System.Type.FullName" /> 属性返回的所请求类型的完全限定名称，包含命名空间而不是程序集。</param>
    /// <filterpriority>2</filterpriority>
    ObjectHandle CreateInstance(string assemblyName, string typeName);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.CreateInstanceFrom(System.String,System.String)" /> 方法的重载的版本无关的访问。</summary>
    /// <returns>一个对象，它是新实例的包装，或者如果找不到 <paramref name="typeName" />，则为 null。返回值需要打开包装才能访问真实对象。</returns>
    /// <param name="assemblyFile">文件的名称（包括路径），该文件包含定义所请求类型的程序集。该程序集是使用 <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" /> 方法加载的。</param>
    /// <param name="typeName">
    /// <see cref="P:System.Type.FullName" /> 属性返回的所请求类型的完全限定名称，包含命名空间而不是程序集。</param>
    /// <filterpriority>2</filterpriority>
    ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.CreateInstance(System.String,System.String,System.Object[])" /> 方法的重载的版本无关的访问。</summary>
    /// <returns>一个对象，该对象是 <paramref name="typeName" /> 指定的新实例的包装。返回值需要打开包装才能访问真实对象。</returns>
    /// <param name="assemblyName">程序集的显示名称。请参见<see cref="P:System.Reflection.Assembly.FullName" />。</param>
    /// <param name="typeName">
    /// <see cref="P:System.Type.FullName" /> 属性返回的所请求类型的完全限定名称，包含命名空间而不是程序集。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。通常是包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组。<see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 指定激活远程对象所需的 URL。</param>
    /// <filterpriority>2</filterpriority>
    ObjectHandle CreateInstance(string assemblyName, string typeName, object[] activationAttributes);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.CreateInstanceFrom(System.String,System.String,System.Object[])" /> 方法的重载的版本无关的访问。</summary>
    /// <returns>一个对象，它是新实例的包装，或者如果找不到 <paramref name="typeName" />，则为 null。返回值需要打开包装才能访问真实对象。</returns>
    /// <param name="assemblyFile">文件的名称（包括路径），该文件包含定义所请求类型的程序集。该程序集是使用 <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" /> 方法加载的。</param>
    /// <param name="typeName">
    /// <see cref="P:System.Type.FullName" /> 属性返回的所请求类型的完全限定名称，包含命名空间而不是程序集。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。通常是包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组。<see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 指定激活远程对象所需的 URL。</param>
    /// <filterpriority>2</filterpriority>
    ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, object[] activationAttributes);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.CreateInstance(System.String,System.String,System.Boolean,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo,System.Object[],System.Security.Policy.Evidence)" /> 方法的重载的版本无关的访问。</summary>
    /// <returns>一个对象，该对象是 <paramref name="typeName" /> 指定的新实例的包装。返回值需要打开包装才能访问真实对象。</returns>
    /// <param name="assemblyName">程序集的显示名称。请参见<see cref="P:System.Reflection.Assembly.FullName" />。</param>
    /// <param name="typeName">
    /// <see cref="P:System.Type.FullName" /> 属性返回的所请求类型的完全限定名称，包含命名空间而不是程序集。</param>
    /// <param name="ignoreCase">一个布尔值，指示是否执行区分大小写的搜索。</param>
    /// <param name="bindingAttr">影响 <paramref name="typeName" /> 构造函数搜索的零个或多个位标志的组合。如果 <paramref name="bindingAttr" /> 为零，则对公共构造函数进行区分大小写的搜索。</param>
    /// <param name="binder">一个对象，它使用反射启用绑定、参数类型的强制、成员的调用和 <see cref="T:System.Reflection.MemberInfo" /> 对象的检索。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="args">要传递给构造函数的实参。此实参数组必须在数量、顺序和类型方面与要调用的构造函数的形参匹配。如果默认的构造函数是首选构造函数，则 <paramref name="args" /> 必须为空数组或 Null。</param>
    /// <param name="culture">区域性特定的信息，这些信息控制将 <paramref name="args" /> 强制转换为 <paramref name="typeName" /> 构造函数所声明的正式类型。如果 <paramref name="culture" /> 为 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。通常是包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组。<see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 指定激活远程对象所需的 URL。</param>
    /// <param name="securityAttributes">用于授权创建 <paramref name="typeName" /> 的信息。</param>
    /// <filterpriority>2</filterpriority>
    ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.CreateInstanceFrom(System.String,System.String,System.Boolean,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo,System.Object[],System.Security.Policy.Evidence)" /> 方法的重载的版本无关的访问。</summary>
    /// <returns>一个对象，它是新实例的包装，或者如果找不到 <paramref name="typeName" />，则为 null。返回值需要打开包装才能访问真实对象。</returns>
    /// <param name="assemblyFile">文件的名称（包括路径），该文件包含定义所请求类型的程序集。该程序集是使用 <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" /> 方法加载的。</param>
    /// <param name="typeName">
    /// <see cref="P:System.Type.FullName" /> 属性返回的所请求类型的完全限定名称，包含命名空间而不是程序集。</param>
    /// <param name="ignoreCase">一个布尔值，指示是否执行区分大小写的搜索。</param>
    /// <param name="bindingAttr">影响 <paramref name="typeName" /> 构造函数搜索的零个或多个位标志的组合。如果 <paramref name="bindingAttr" /> 为零，则对公共构造函数进行区分大小写的搜索。</param>
    /// <param name="binder">一个对象，它启用绑定、对参数类型的强制、对成员的调用，以及通过反射对 <see cref="T:System.Reflection.MemberInfo" /> 对象的检索。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="args">要传递给构造函数的实参。此实参数组必须在数量、顺序和类型方面与要调用的构造函数的形参匹配。如果默认的构造函数是首选构造函数，则 <paramref name="args" /> 必须为空数组或 Null。</param>
    /// <param name="culture">区域性特定的信息，这些信息控制将 <paramref name="args" /> 强制转换为 <paramref name="typeName" /> 构造函数所声明的正式类型。如果 <paramref name="culture" /> 为 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。通常是包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组。<see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 指定激活远程对象所需的 URL。</param>
    /// <param name="securityAttributes">用于授权创建 <paramref name="typeName" /> 的信息。</param>
    /// <filterpriority>2</filterpriority>
    ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.Load(System.Reflection.AssemblyName)" /> 方法的重载的版本无关的访问。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="assemblyRef">描述要加载的程序集的对象。</param>
    /// <filterpriority>2</filterpriority>
    Assembly Load(AssemblyName assemblyRef);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.Load(System.String)" /> 方法的重载的版本无关的访问。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="assemblyString">程序集的显示名称。请参见<see cref="P:System.Reflection.Assembly.FullName" />。</param>
    /// <filterpriority>2</filterpriority>
    Assembly Load(string assemblyString);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.Load(System.Byte[])" /> 方法重载的版本无关的访问。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="rawAssembly">byte 类型的数组，它是包含已发出程序集的基于 COFF 的图像。</param>
    /// <filterpriority>2</filterpriority>
    Assembly Load(byte[] rawAssembly);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.Load(System.Byte[],System.Byte[])" /> 方法的重载的版本无关的访问。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="rawAssembly">byte 类型的数组，它是包含已发出程序集的基于 COFF 的图像。</param>
    /// <param name="rawSymbolStore">byte 类型的数组，它包含表示程序集符号的原始字节。</param>
    /// <filterpriority>2</filterpriority>
    Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.Load(System.Byte[],System.Byte[],System.Security.Policy.Evidence)" /> 方法的重载的版本无关的访问。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="rawAssembly">byte 类型的数组，它是包含已发出程序集的基于 COFF 的图像。</param>
    /// <param name="rawSymbolStore">byte 类型的数组，它包含表示程序集符号的原始字节。</param>
    /// <param name="securityEvidence">用于加载程序集的证据。</param>
    /// <filterpriority>2</filterpriority>
    Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.Load(System.Reflection.AssemblyName,System.Security.Policy.Evidence)" /> 方法的重载的版本无关的访问。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="assemblyRef">描述要加载的程序集的对象。</param>
    /// <param name="assemblySecurity">用于加载程序集的证据。</param>
    /// <filterpriority>2</filterpriority>
    Assembly Load(AssemblyName assemblyRef, Evidence assemblySecurity);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.Load(System.String,System.Security.Policy.Evidence)" /> 方法的重载的版本无关的访问。</summary>
    /// <returns>加载的程序集。</returns>
    /// <param name="assemblyString">程序集的显示名称。请参见<see cref="P:System.Reflection.Assembly.FullName" />。</param>
    /// <param name="assemblySecurity">用于加载程序集的证据。</param>
    /// <filterpriority>2</filterpriority>
    Assembly Load(string assemblyString, Evidence assemblySecurity);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.ExecuteAssembly(System.String,System.Security.Policy.Evidence)" /> 方法的重载的版本无关的访问。</summary>
    /// <returns>程序集的入口点返回的值。</returns>
    /// <param name="assemblyFile">包含要执行程序集的文件的名称。</param>
    /// <param name="assemblySecurity">用于加载程序集的证据。</param>
    /// <filterpriority>2</filterpriority>
    int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.ExecuteAssembly(System.String)" /> 方法的重载的版本无关的访问。</summary>
    /// <returns>程序集的入口点返回的值。</returns>
    /// <param name="assemblyFile">包含要执行程序集的文件的名称。</param>
    /// <filterpriority>2</filterpriority>
    int ExecuteAssembly(string assemblyFile);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.ExecuteAssembly(System.String,System.Security.Policy.Evidence,System.String[])" /> 方法的重载的版本无关的访问。</summary>
    /// <returns>程序集的入口点返回的值。</returns>
    /// <param name="assemblyFile">包含要执行程序集的文件的名称。</param>
    /// <param name="assemblySecurity">为程序集提供的证据。</param>
    /// <param name="args">程序集的入口点的实参。</param>
    /// <filterpriority>2</filterpriority>
    int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity, string[] args);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.GetAssemblies" /> 方法的版本无关的访问。</summary>
    /// <returns>此应用程序域中的程序集的数组。</returns>
    /// <filterpriority>2</filterpriority>
    Assembly[] GetAssemblies();

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.AppendPrivatePath(System.String)" /> 方法的版本无关的访问。</summary>
    /// <param name="path">要追加到专用路径的目录名称。</param>
    /// <filterpriority>2</filterpriority>
    [SecurityCritical]
    void AppendPrivatePath(string path);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.ClearPrivatePath" /> 方法的版本无关的访问。</summary>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
    /// </PermissionSet>
    [SecurityCritical]
    void ClearPrivatePath();

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.SetShadowCopyPath(System.String)" /> 方法的版本无关的访问。</summary>
    /// <param name="s">目录名列表，每一个名称用分号隔开。</param>
    /// <filterpriority>2</filterpriority>
    [SecurityCritical]
    void SetShadowCopyPath(string s);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.ClearShadowCopyPath" /> 方法的版本无关的访问。</summary>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
    /// </PermissionSet>
    [SecurityCritical]
    void ClearShadowCopyPath();

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.SetCachePath(System.String)" /> 方法的版本无关的访问。</summary>
    /// <param name="s">到影像副本位置的完全限定路径。</param>
    /// <filterpriority>2</filterpriority>
    [SecurityCritical]
    void SetCachePath(string s);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.SetData(System.String,System.Object)" /> 方法的版本无关的访问。</summary>
    /// <param name="name">要创建或更改的用户定义应用程序域属性的名称。</param>
    /// <param name="data">该属性的值。</param>
    /// <filterpriority>2</filterpriority>
    [SecurityCritical]
    void SetData(string name, object data);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.GetData(System.String)" /> 方法的版本无关的访问。</summary>
    /// <returns>
    /// <paramref name="name" /> 属性的值。</returns>
    /// <param name="name">预定义应用程序域属性的名称，或已定义的应用程序域属性的名称。</param>
    /// <filterpriority>2</filterpriority>
    object GetData(string name);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.SetAppDomainPolicy(System.Security.Policy.PolicyLevel)" /> 方法的版本无关的访问。</summary>
    /// <param name="domainPolicy">安全策略级别。</param>
    /// <filterpriority>2</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlDomainPolicy" />
    /// </PermissionSet>
    [SecurityCritical]
    void SetAppDomainPolicy(PolicyLevel domainPolicy);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.SetThreadPrincipal(System.Security.Principal.IPrincipal)" /> 方法的版本无关的访问。</summary>
    /// <param name="principal">要附加到线程的主体对象。</param>
    /// <filterpriority>2</filterpriority>
    void SetThreadPrincipal(IPrincipal principal);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.SetPrincipalPolicy(System.Security.Principal.PrincipalPolicy)" /> 方法的版本无关的访问。</summary>
    /// <param name="policy">
    /// <see cref="T:System.Security.Principal.PrincipalPolicy" /> 值之一，指定要附加到线程的主体对象类型。</param>
    /// <filterpriority>2</filterpriority>
    void SetPrincipalPolicy(PrincipalPolicy policy);

    /// <summary>为 COM 对象提供对 <see cref="M:System.AppDomain.DoCallBack(System.CrossAppDomainDelegate)" /> 方法的版本无关的访问。</summary>
    /// <param name="theDelegate">指定要调用的方法的委托。</param>
    /// <filterpriority>2</filterpriority>
    void DoCallBack(CrossAppDomainDelegate theDelegate);
  }
}
