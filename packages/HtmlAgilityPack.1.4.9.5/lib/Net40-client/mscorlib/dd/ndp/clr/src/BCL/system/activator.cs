// Decompiled with JetBrains decompiler
// Type: System.Activator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Configuration.Assemblies;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Security;
using System.Security.Policy;
using System.Threading;

namespace System
{
  /// <summary>包含特定的方法，用以在本地或从远程创建对象类型，或获取对现有远程对象的引用。此类不能被继承。</summary>
  /// <filterpriority>2</filterpriority>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_Activator))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class Activator : _Activator
  {
    internal const int LookupMask = 255;
    internal const BindingFlags ConLookup = BindingFlags.Instance | BindingFlags.Public;
    internal const BindingFlags ConstructorDefault = BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;

    private Activator()
    {
    }

    /// <summary>使用与指定参数匹配程度最高的构造函数创建指定类型的实例。</summary>
    /// <returns>对新创建对象的引用。</returns>
    /// <param name="type">要创建的对象的类型。</param>
    /// <param name="bindingAttr">影响 <paramref name="type" /> 构造函数搜索的零个或多个位标志的组合。如果 <paramref name="bindingAttr" /> 为零，则对公共构造函数进行区分大小写的搜索。</param>
    /// <param name="binder">使用 <paramref name="bindingAttr" /> 和 <paramref name="args" /> 来查找和标识 <paramref name="type" /> 构造函数的对象。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="args">与要调用构造函数的参数数量、顺序和类型匹配的参数数组。如果 <paramref name="args" /> 为空数组或 null，则调用不带任何参数的构造函数（默认构造函数）。</param>
    /// <param name="culture">区域性特定的信息，这些信息控制将 <paramref name="args" /> 强制转换为 <paramref name="type" /> 构造函数所声明的正式类型。如果 <paramref name="culture" /> 为 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="type" /> 不是 RuntimeType。- 或 -<paramref name="type" /> 是一个开放式泛型类型 （即， <see cref="P:System.Type.ContainsGenericParameters" /> 属性将返回 true)。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="type" /> 不能为 <see cref="T:System.Reflection.Emit.TypeBuilder" />。- 或 - 创建 <see cref="T:System.TypedReference" />, ，<see cref="T:System.ArgIterator" />, ，<see cref="T:System.Void" />, ，和 <see cref="T:System.RuntimeArgumentHandle" /> 不支持类型或这些类型的数组。- 或 -包含的程序集 <paramref name="type" /> 是动态的程序集是使用创建 <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />。- 或 -最匹配的构造函数 <paramref name="args" /> 具有 varargs 参数。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">正在调用的构造函数引发异常。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.MemberAccessException">不能创建一个抽象类的实例或者用后期绑定机制调用了此成员。</exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">不通过 COM 类型 <see cref="Overload:System.Type.GetTypeFromProgID" /> 或 <see cref="Overload:System.Type.GetTypeFromCLSID" />。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的构造函数。</exception>
    /// <exception cref="T:System.Runtime.InteropServices.COMException">
    /// <paramref name="type" /> 是一个 COM 对象，但用来获取类型的类标识符是无效的或标识的类未注册。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="type" /> 不是有效的类型。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, RemotingConfiguration" />
    /// </PermissionSet>
    public static object CreateInstance(Type type, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture)
    {
      return Activator.CreateInstance(type, bindingAttr, binder, args, culture, (object[]) null);
    }

    /// <summary>使用与指定参数匹配程度最高的构造函数创建指定类型的实例。</summary>
    /// <returns>对新创建对象的引用。</returns>
    /// <param name="type">要创建的对象的类型。</param>
    /// <param name="bindingAttr">影响 <paramref name="type" /> 构造函数搜索的零个或多个位标志的组合。如果 <paramref name="bindingAttr" /> 为零，则对公共构造函数进行区分大小写的搜索。</param>
    /// <param name="binder">使用 <paramref name="bindingAttr" /> 和 <paramref name="args" /> 来查找和标识 <paramref name="type" /> 构造函数的对象。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="args">与要调用构造函数的参数数量、顺序和类型匹配的参数数组。如果 <paramref name="args" /> 为空数组或 null，则调用不带任何参数的构造函数（默认构造函数）。</param>
    /// <param name="culture">区域性特定的信息，这些信息控制将 <paramref name="args" /> 强制转换为 <paramref name="type" /> 构造函数所声明的正式类型。如果 <paramref name="culture" /> 为 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。这通常为包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组，该对象指定激活远程对象所需的 URL。此参数与客户端激活的对象相关。客户端激活是一项传统技术，保留用于向后兼容，但不建议用于新的开发。应改用 Windows Communication Foundation 来开发分布式应用程序。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="type" /> 不是 RuntimeType。- 或 -<paramref name="type" /> 是一个开放式泛型类型 （即， <see cref="P:System.Type.ContainsGenericParameters" /> 属性将返回 true)。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="type" /> 不能为 <see cref="T:System.Reflection.Emit.TypeBuilder" />。- 或 - 创建 <see cref="T:System.TypedReference" />, ，<see cref="T:System.ArgIterator" />, ，<see cref="T:System.Void" />, ，和 <see cref="T:System.RuntimeArgumentHandle" /> 不支持类型或这些类型的数组。- 或 - <paramref name="activationAttributes" /> 不是一个空数组和正在创建的类型不是派生自 <see cref="T:System.MarshalByRefObject" />。- 或 -包含的程序集 <paramref name="type" /> 是动态的程序集是使用创建 <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />。- 或 -最匹配的构造函数 <paramref name="args" /> 具有 varargs 参数。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">正在调用的构造函数引发异常。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.MemberAccessException">不能创建一个抽象类的实例或者用后期绑定机制调用了此成员。</exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">不通过 COM 类型 <see cref="Overload:System.Type.GetTypeFromProgID" /> 或 <see cref="Overload:System.Type.GetTypeFromCLSID" />。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的构造函数。</exception>
    /// <exception cref="T:System.Runtime.InteropServices.COMException">
    /// <paramref name="type" /> 是一个 COM 对象，但用来获取类型的类标识符是无效的或标识的类未注册。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="type" /> 不是有效的类型。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static object CreateInstance(Type type, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
    {
      if (type == null)
        throw new ArgumentNullException("type");
      if (type is TypeBuilder)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_CreateInstanceWithTypeBuilder"));
      if ((bindingAttr & (BindingFlags) 255) == BindingFlags.Default)
        bindingAttr |= BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;
      if (activationAttributes != null && activationAttributes.Length != 0)
      {
        if (!type.IsMarshalByRef)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_ActivAttrOnNonMBR"));
        if (!type.IsContextful && (activationAttributes.Length > 1 || !(activationAttributes[0] is UrlAttribute)))
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_NonUrlAttrOnMBR"));
      }
      RuntimeType runtimeType = type.UnderlyingSystemType as RuntimeType;
      // ISSUE: variable of the null type
      __Null local = null;
      if (runtimeType == (RuntimeType) local)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "type");
      StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
      int num = (int) bindingAttr;
      Binder binder1 = binder;
      object[] args1 = args;
      CultureInfo culture1 = culture;
      object[] activationAttributes1 = activationAttributes;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      StackCrawlMark& stackMark = @stackCrawlMark;
      return runtimeType.CreateInstanceImpl((BindingFlags) num, binder1, args1, culture1, activationAttributes1, stackMark);
    }

    /// <summary>使用与指定参数匹配程度最高的构造函数创建指定类型的实例。</summary>
    /// <returns>对新创建对象的引用。</returns>
    /// <param name="type">要创建的对象的类型。</param>
    /// <param name="args">与要调用构造函数的参数数量、顺序和类型匹配的参数数组。如果 <paramref name="args" /> 为空数组或 null，则调用不带任何参数的构造函数（默认构造函数）。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="type" /> 不是 RuntimeType。- 或 -<paramref name="type" /> 是一个开放式泛型类型 （即， <see cref="P:System.Type.ContainsGenericParameters" /> 属性将返回 true)。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="type" /> 不能为 <see cref="T:System.Reflection.Emit.TypeBuilder" />。- 或 - 创建 <see cref="T:System.TypedReference" />, ，<see cref="T:System.ArgIterator" />, ，<see cref="T:System.Void" />, ，和 <see cref="T:System.RuntimeArgumentHandle" /> 不支持类型或这些类型的数组。- 或 -包含的程序集 <paramref name="type" /> 是动态的程序集是使用创建 <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />。- 或 -最匹配的构造函数 <paramref name="args" /> 具有 varargs 参数。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">正在调用的构造函数引发异常。</exception>
    /// <exception cref="T:System.MethodAccessException">在 .NET for Windows Store apps 或 可移植类库, ，捕获该基类异常， <see cref="T:System.MemberAccessException" />, 、 相反。调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.MemberAccessException">不能创建一个抽象类的实例或者用后期绑定机制调用了此成员。</exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">不通过 COM 类型 <see cref="Overload:System.Type.GetTypeFromProgID" /> 或 <see cref="Overload:System.Type.GetTypeFromCLSID" />。</exception>
    /// <exception cref="T:System.MissingMethodException">在 .NET for Windows Store apps 或 可移植类库, ，捕获该基类异常， <see cref="T:System.MissingMemberException" />, 、 相反。不找到任何匹配的公共构造函数。</exception>
    /// <exception cref="T:System.Runtime.InteropServices.COMException">
    /// <paramref name="type" /> 是一个 COM 对象，但用来获取类型的类标识符是无效的或标识的类未注册。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="type" /> 不是有效的类型。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, RemotingConfiguration" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public static object CreateInstance(Type type, params object[] args)
    {
      return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, (Binder) null, args, (CultureInfo) null, (object[]) null);
    }

    /// <summary>使用与指定参数匹配程度最高的构造函数创建指定类型的实例。</summary>
    /// <returns>对新创建对象的引用。</returns>
    /// <param name="type">要创建的对象的类型。</param>
    /// <param name="args">与要调用构造函数的参数数量、顺序和类型匹配的参数数组。如果 <paramref name="args" /> 为空数组或 null，则调用不带任何参数的构造函数（默认构造函数）。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。这通常为包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组，该对象指定激活远程对象所需的 URL。此参数与客户端激活的对象相关。客户端激活是一项传统技术，保留用于向后兼容，但不建议用于新的开发。应改用 Windows Communication Foundation 来开发分布式应用程序。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="type" /> 不是 RuntimeType。- 或 -<paramref name="type" /> 是一个开放式泛型类型 （即， <see cref="P:System.Type.ContainsGenericParameters" /> 属性将返回 true)。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="type" /> 不能为 <see cref="T:System.Reflection.Emit.TypeBuilder" />。- 或 - 创建 <see cref="T:System.TypedReference" />, ，<see cref="T:System.ArgIterator" />, ，<see cref="T:System.Void" />, ，和 <see cref="T:System.RuntimeArgumentHandle" /> 不支持类型或这些类型的数组。- 或 - <paramref name="activationAttributes" /> 不是一个空数组和正在创建的类型不是派生自 <see cref="T:System.MarshalByRefObject" />。- 或 -包含的程序集 <paramref name="type" /> 是动态的程序集是使用创建 <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />。- 或 -最匹配的构造函数 <paramref name="args" /> 具有 varargs 参数。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">正在调用的构造函数引发异常。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.MemberAccessException">不能创建一个抽象类的实例或者用后期绑定机制调用了此成员。</exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">不通过 COM 类型 <see cref="Overload:System.Type.GetTypeFromProgID" /> 或 <see cref="Overload:System.Type.GetTypeFromCLSID" />。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的公共构造函数。</exception>
    /// <exception cref="T:System.Runtime.InteropServices.COMException">
    /// <paramref name="type" /> 是一个 COM 对象，但用来获取类型的类标识符是无效的或标识的类未注册。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="type" /> 不是有效的类型。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, RemotingConfiguration" />
    /// </PermissionSet>
    public static object CreateInstance(Type type, object[] args, object[] activationAttributes)
    {
      return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, (Binder) null, args, (CultureInfo) null, activationAttributes);
    }

    /// <summary>使用指定类型的默认构造函数来创建该类型的实例。</summary>
    /// <returns>对新创建对象的引用。</returns>
    /// <param name="type">要创建的对象的类型。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="type" /> 不是 RuntimeType。- 或 -<paramref name="type" /> 是一个开放式泛型类型 （即， <see cref="P:System.Type.ContainsGenericParameters" /> 属性将返回 true)。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="type" /> 不能为 <see cref="T:System.Reflection.Emit.TypeBuilder" />。- 或 - 创建 <see cref="T:System.TypedReference" />, ，<see cref="T:System.ArgIterator" />, ，<see cref="T:System.Void" />, ，和 <see cref="T:System.RuntimeArgumentHandle" /> 不支持类型或这些类型的数组。- 或 -包含的程序集 <paramref name="type" /> 是动态的程序集是使用创建 <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">正在调用的构造函数引发异常。</exception>
    /// <exception cref="T:System.MethodAccessException">在 .NET for Windows Store apps 或 可移植类库, ，捕获该基类异常， <see cref="T:System.MemberAccessException" />, 、 相反。调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.MemberAccessException">不能创建一个抽象类的实例或者用后期绑定机制调用了此成员。</exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">不通过 COM 类型 <see cref="Overload:System.Type.GetTypeFromProgID" /> 或 <see cref="Overload:System.Type.GetTypeFromCLSID" />。</exception>
    /// <exception cref="T:System.MissingMethodException">在 .NET for Windows Store apps 或 可移植类库, ，捕获该基类异常， <see cref="T:System.MissingMemberException" />, 、 相反。不找到任何匹配的公共构造函数。</exception>
    /// <exception cref="T:System.Runtime.InteropServices.COMException">
    /// <paramref name="type" /> 是一个 COM 对象，但用来获取类型的类标识符是无效的或标识的类未注册。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="type" /> 不是有效的类型。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [__DynamicallyInvokable]
    public static object CreateInstance(Type type)
    {
      return Activator.CreateInstance(type, false);
    }

    /// <summary>使用命名的程序集和默认构造函数，创建名称已指定的类型的实例。</summary>
    /// <returns>要访问新创建的实例则必须解包的句柄。</returns>
    /// <param name="assemblyName">将在其中查找名为 <paramref name="typeName" /> 的类型的程序集的名称。有关详细信息，请参阅“备注”部分。如果 <paramref name="assemblyName" /> 为 null，则搜索正在执行的程序集。</param>
    /// <param name="typeName">首选类型的完全限定名。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的公共构造函数。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typename" /> 中未找到 <paramref name="assemblyName" />。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyName" /> 未找到。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.MemberAccessException">无法创建一个抽象类的实例，或使用后期绑定机制调用了该成员。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">构造函数，通过反射调用引发了异常。</exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">不通过 COM 类型 <see cref="Overload:System.Type.GetTypeFromProgID" /> 或 <see cref="Overload:System.Type.GetTypeFromCLSID" />。</exception>
    /// <exception cref="T:System.NotSupportedException">创建 <see cref="T:System.TypedReference" />, ，<see cref="T:System.ArgIterator" />, ，<see cref="T:System.Void" />, ，和 <see cref="T:System.RuntimeArgumentHandle" /> 不支持类型或这些类型的数组。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyName" /> 不是有效的程序集。- 或 -当前加载公共语言运行时 (CLR) 2.0 或更高版本，并 <paramref name="assemblyName" /> 编译为晚于当前加载的版本的 clr 版本。请注意.NET Framework 版本 2.0、 3.0 和 3.5 所有使用 CLR 版本 2.0。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。- 或 -程序集名称或基本代码无效。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static ObjectHandle CreateInstance(string assemblyName, string typeName)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Activator.CreateInstance(assemblyName, typeName, false, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, (Binder) null, (object[]) null, (CultureInfo) null, (object[]) null, (Evidence) null, ref stackMark);
    }

    /// <summary>使用命名的程序集和默认构造函数，创建名称已指定的类型的实例。</summary>
    /// <returns>要访问新创建的实例则必须解包的句柄。</returns>
    /// <param name="assemblyName">将在其中查找名为 <paramref name="typeName" /> 的类型的程序集的名称。如果 <paramref name="assemblyName" /> 为 null，则搜索正在执行的程序集。</param>
    /// <param name="typeName">首选类型的完全限定名。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。这通常为包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组，该对象指定激活远程对象所需的 URL。此参数与客户端激活的对象相关。客户端激活是一项传统技术，保留用于向后兼容，但不建议用于新的开发。应改用 Windows Communication Foundation 来开发分布式应用程序。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的公共构造函数。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typename" /> 中未找到 <paramref name="assemblyName" />。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyName" /> 未找到。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.MemberAccessException">不能创建一个抽象类的实例或者用后期绑定机制调用了此成员。</exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">不通过 COM 类型 <see cref="Overload:System.Type.GetTypeFromProgID" /> 或 <see cref="Overload:System.Type.GetTypeFromCLSID" />。</exception>
    /// <exception cref="T:System.NotSupportedException">创建 <see cref="T:System.TypedReference" />, ，<see cref="T:System.ArgIterator" />, ，<see cref="T:System.Void" />, ，和 <see cref="T:System.RuntimeArgumentHandle" /> 不支持类型或这些类型的数组。- 或 - <paramref name="activationAttributes" /> 不是一个空数组和正在创建的类型不是派生自 <see cref="T:System.MarshalByRefObject" />。- 或 -<paramref name="activationAttributes" /> 不是 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />数组。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyName" /> 不是有效的程序集。- 或 -当前加载公共语言运行时 (CLR) 2.0 或更高版本，并 <paramref name="assemblyName" /> 编译为晚于当前加载的版本的 clr 版本。请注意.NET Framework 版本 2.0、 3.0 和 3.5 所有使用 CLR 版本 2.0。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。- 或 -程序集名称或基本代码无效。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">当尝试在指定的目标中的远程激活时出错 <paramref name="activationAttributes" />。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static ObjectHandle CreateInstance(string assemblyName, string typeName, object[] activationAttributes)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Activator.CreateInstance(assemblyName, typeName, false, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, (Binder) null, (object[]) null, (CultureInfo) null, activationAttributes, (Evidence) null, ref stackMark);
    }

    /// <summary>使用指定类型的默认构造函数来创建该类型的实例。</summary>
    /// <returns>对新创建对象的引用。</returns>
    /// <param name="type">要创建的对象的类型。</param>
    /// <param name="nonPublic">如果公共或非公共默认构造函数可以匹配，则为 true；如果只有公共默认构造函数可以匹配，则为 false。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="type" /> 不是 RuntimeType。- 或 -<paramref name="type" /> 是一个开放式泛型类型 （即， <see cref="P:System.Type.ContainsGenericParameters" /> 属性将返回 true)。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="type" /> 不能为 <see cref="T:System.Reflection.Emit.TypeBuilder" />。- 或 - 创建 <see cref="T:System.TypedReference" />, ，<see cref="T:System.ArgIterator" />, ，<see cref="T:System.Void" />, ，和 <see cref="T:System.RuntimeArgumentHandle" /> 不支持类型或这些类型的数组。- 或 -包含的程序集 <paramref name="type" /> 是动态的程序集是使用创建 <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Save" />。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">正在调用的构造函数引发异常。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.MemberAccessException">不能创建一个抽象类的实例或者用后期绑定机制调用了此成员。</exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">不通过 COM 类型 <see cref="Overload:System.Type.GetTypeFromProgID" /> 或 <see cref="Overload:System.Type.GetTypeFromCLSID" />。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的公共构造函数。</exception>
    /// <exception cref="T:System.Runtime.InteropServices.COMException">
    /// <paramref name="type" /> 是一个 COM 对象，但用来获取类型的类标识符是无效的或标识的类未注册。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="type" /> 不是有效的类型。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static object CreateInstance(Type type, bool nonPublic)
    {
      if (type == null)
        throw new ArgumentNullException("type");
      RuntimeType runtimeType = type.UnderlyingSystemType as RuntimeType;
      // ISSUE: variable of the null type
      __Null local = null;
      if (runtimeType == (RuntimeType) local)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "type");
      StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
      int num1 = !nonPublic ? 1 : 0;
      int num2 = 0;
      int num3 = 1;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      StackCrawlMark& stackMark = @stackCrawlMark;
      return runtimeType.CreateInstanceDefaultCtor(num1 != 0, num2 != 0, num3 != 0, stackMark);
    }

    /// <summary>使用无参数构造函数，创建指定泛型类型参数所指定类型的实例。</summary>
    /// <returns>对新创建对象的引用。</returns>
    /// <typeparam name="T">要创建的类型。</typeparam>
    /// <exception cref="T:System.MissingMethodException">在 .NET for Windows Store apps 或 可移植类库, ，捕获该基类异常， <see cref="T:System.MissingMemberException" />, 、 相反。为指定的类型 <paramref name="T" /> 不具有无参数构造函数。</exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T CreateInstance<T>()
    {
      RuntimeType runtimeType = typeof (T) as RuntimeType;
      if (runtimeType.HasElementType)
        throw new MissingMethodException(Environment.GetResourceString("Arg_NoDefCTor"));
      StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
      int num1 = 1;
      int num2 = 1;
      int num3 = 1;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      StackCrawlMark& stackMark = @stackCrawlMark;
      return (T) runtimeType.CreateInstanceDefaultCtor(num1 != 0, num2 != 0, num3 != 0, stackMark);
    }

    /// <summary>使用命名的程序集文件和默认构造函数，创建名称已指定的类型的实例。</summary>
    /// <returns>要访问新创建的实例则必须解包的句柄。</returns>
    /// <param name="assemblyFile">包含某程序集的文件的名称，将在该程序集内查找名为 <paramref name="typeName" /> 的类型。</param>
    /// <param name="typeName">首选类型的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的公共构造函数。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typename" /> 中未找到 <paramref name="assemblyFile" />。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyFile" /> 未找到。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.MemberAccessException">不能创建一个抽象类的实例或者用后期绑定机制调用了此成员。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">构造函数，通过反射调用引发了异常。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所需 <see cref="T:System.Security.Permissions.FileIOPermission" />。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效的程序集。- 或 -当前加载公共语言运行时 (CLR) 2.0 或更高版本，并 <paramref name="assemblyName" /> 编译为晚于当前加载的版本的 clr 版本。请注意.NET Framework 版本 2.0、 3.0 和 3.5 所有使用 CLR 版本 2.0。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName)
    {
      return Activator.CreateInstanceFrom(assemblyFile, typeName, (object[]) null);
    }

    /// <summary>使用命名的程序集文件和默认构造函数，创建名称已指定的类型的实例。</summary>
    /// <returns>要访问新创建的实例则必须解包的句柄。</returns>
    /// <param name="assemblyFile">包含某程序集的文件的名称，将在该程序集内查找名为 <paramref name="typeName" /> 的类型。</param>
    /// <param name="typeName">首选类型的名称。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。这通常为包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组，该对象指定激活远程对象所需的 URL。此参数与客户端激活的对象相关。客户端激活是一项传统技术，保留用于向后兼容，但不建议用于新的开发。应改用 Windows Communication Foundation 来开发分布式应用程序。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的公共构造函数。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typename" /> 中未找到 <paramref name="assemblyFile" />。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyFile" /> 未找到。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.MemberAccessException">不能创建一个抽象类的实例或者用后期绑定机制调用了此成员。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">构造函数，通过反射调用引发了异常。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="activationAttributes" /> 不是一个空数组和正在创建的类型不是派生自 <see cref="T:System.MarshalByRefObject" />。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所需 <see cref="T:System.Security.Permissions.FileIOPermission" />。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效的程序集。- 或 -当前加载公共语言运行时 (CLR) 2.0 或更高版本，并 <paramref name="assemblyName" /> 编译为晚于当前加载的版本的 clr 版本。请注意.NET Framework 版本 2.0、 3.0 和 3.5 所有使用 CLR 版本 2.0。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, object[] activationAttributes)
    {
      return Activator.CreateInstanceFrom(assemblyFile, typeName, false, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, (Binder) null, (object[]) null, (CultureInfo) null, activationAttributes);
    }

    /// <summary>使用指定的程序集和与指定参数匹配程度最高的构造函数来创建指定名称的类型的实例。</summary>
    /// <returns>要访问新创建的实例则必须解包的句柄。</returns>
    /// <param name="assemblyName">将在其中查找名为 <paramref name="typeName" /> 的类型的程序集的名称。如果 <paramref name="assemblyName" /> 为 null，则搜索正在执行的程序集。</param>
    /// <param name="typeName">首选类型的完全限定名。</param>
    /// <param name="ignoreCase">使用 true 指定对 <paramref name="typeName" /> 的搜索不区分大小写；使用 false 则指定搜索区分大小写。</param>
    /// <param name="bindingAttr">影响 <paramref name="typeName" /> 构造函数搜索的零个或多个位标志的组合。如果 <paramref name="bindingAttr" /> 为零，则对公共构造函数进行区分大小写的搜索。</param>
    /// <param name="binder">使用 <paramref name="bindingAttr" /> 和 <paramref name="args" /> 来查找和标识 <paramref name="typeName" /> 构造函数的对象。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="args">与要调用构造函数的参数数量、顺序和类型匹配的参数数组。如果 <paramref name="args" /> 为空数组或 null，则调用不带任何参数的构造函数（默认构造函数）。</param>
    /// <param name="culture">区域性特定的信息，这些信息控制将 <paramref name="args" /> 强制转换为 <paramref name="typeName" /> 构造函数所声明的正式类型。如果 <paramref name="culture" /> 为 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。这通常为包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组，该对象指定激活远程对象所需的 URL。此参数与客户端激活的对象相关。客户端激活是一项传统技术，保留用于向后兼容，但不建议用于新的开发。应改用 Windows Communication Foundation 来开发分布式应用程序。</param>
    /// <param name="securityInfo">用于做出安全策略决策和授予代码权限的信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的构造函数。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typename" /> 中未找到 <paramref name="assemblyName" />。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyName" /> 未找到。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.MemberAccessException">不能创建一个抽象类的实例或者用后期绑定机制调用了此成员。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">构造函数，通过反射调用引发了异常。</exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">不通过 COM 类型 <see cref="Overload:System.Type.GetTypeFromProgID" /> 或 <see cref="Overload:System.Type.GetTypeFromCLSID" />。</exception>
    /// <exception cref="T:System.NotSupportedException">创建 <see cref="T:System.TypedReference" />, ，<see cref="T:System.ArgIterator" />, ，<see cref="T:System.Void" />, ，和 <see cref="T:System.RuntimeArgumentHandle" /> 不支持类型或这些类型的数组。- 或 - <paramref name="activationAttributes" /> 不是一个空数组和正在创建的类型不是派生自 <see cref="T:System.MarshalByRefObject" />。- 或 -最匹配的构造函数 <paramref name="args" /> 具有 varargs 参数。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyName" /> 不是有效的程序集。- 或 -当前加载公共语言运行时 (CLR) 2.0 或更高版本，并 <paramref name="assemblyName" /> 编译为晚于当前加载的版本的 clr 版本。请注意.NET Framework 版本 2.0、 3.0 和 3.5 所有使用 CLR 版本 2.0。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。- 或 -程序集名称或基本代码无效。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, RemotingConfiguration" />
    /// </PermissionSet>
    [SecuritySafeCritical]
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstance which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityInfo)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Activator.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityInfo, ref stackMark);
    }

    /// <summary>使用指定的程序集和与指定参数匹配程度最高的构造函数来创建指定名称的类型的实例。</summary>
    /// <returns>要访问新创建的实例则必须解包的句柄。</returns>
    /// <param name="assemblyName">将在其中查找名为 <paramref name="typeName" /> 的类型的程序集的名称。如果 <paramref name="assemblyName" /> 为 null，则搜索正在执行的程序集。</param>
    /// <param name="typeName">首选类型的完全限定名。</param>
    /// <param name="ignoreCase">使用 true 指定对 <paramref name="typeName" /> 的搜索不区分大小写；使用 false 则指定搜索区分大小写。</param>
    /// <param name="bindingAttr">影响 <paramref name="typeName" /> 构造函数搜索的零个或多个位标志的组合。如果 <paramref name="bindingAttr" /> 为零，则对公共构造函数进行区分大小写的搜索。</param>
    /// <param name="binder">使用 <paramref name="bindingAttr" /> 和 <paramref name="args" /> 来查找和标识 <paramref name="typeName" /> 构造函数的对象。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="args">与要调用构造函数的参数数量、顺序和类型匹配的参数数组。如果 <paramref name="args" /> 为空数组或 null，则调用不带任何参数的构造函数（默认构造函数）。</param>
    /// <param name="culture">区域性特定的信息，这些信息控制将 <paramref name="args" /> 强制转换为 <paramref name="typeName" /> 构造函数所声明的正式类型。如果 <paramref name="culture" /> 为 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。这通常为包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组，该对象指定激活远程对象所需的 URL。此参数与客户端激活的对象相关。客户端激活是一项传统技术，保留用于向后兼容，但不建议用于新的开发。应改用 Windows Communication Foundation 来开发分布式应用程序。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的构造函数。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typename" /> 中未找到 <paramref name="assemblyName" />。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyName" /> 未找到。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.MemberAccessException">不能创建一个抽象类的实例或者用后期绑定机制调用了此成员。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">构造函数，通过反射调用引发了异常。</exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">不通过 COM 类型 <see cref="Overload:System.Type.GetTypeFromProgID" /> 或 <see cref="Overload:System.Type.GetTypeFromCLSID" />。</exception>
    /// <exception cref="T:System.NotSupportedException">创建 <see cref="T:System.TypedReference" />, ，<see cref="T:System.ArgIterator" />, ，<see cref="T:System.Void" />, ，和 <see cref="T:System.RuntimeArgumentHandle" /> 不支持类型或这些类型的数组。- 或 - <paramref name="activationAttributes" /> 不是一个空数组和正在创建的类型不是派生自 <see cref="T:System.MarshalByRefObject" />。- 或 -最匹配的构造函数 <paramref name="args" /> 具有 varargs 参数。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyName" /> 不是有效的程序集。- 或 -当前加载公共语言运行时 (CLR) 2.0 或更高版本，并 <paramref name="assemblyName" /> 编译为晚于当前加载的版本的 clr 版本。请注意.NET Framework 版本 2.0、 3.0 和 3.5 所有使用 CLR 版本 2.0。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。- 或 -程序集名称或基本代码无效。</exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return Activator.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, (Evidence) null, ref stackMark);
    }

    [SecurityCritical]
    internal static ObjectHandle CreateInstance(string assemblyString, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityInfo, ref StackCrawlMark stackMark)
    {
      if (securityInfo != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      Type type = (Type) null;
      Assembly assembly = (Assembly) null;
      if (assemblyString == null)
      {
        assembly = (Assembly) RuntimeAssembly.GetExecutingAssembly(ref stackMark);
      }
      else
      {
        RuntimeAssembly assemblyFromResolveEvent;
        AssemblyName assemblyName = RuntimeAssembly.CreateAssemblyName(assemblyString, false, out assemblyFromResolveEvent);
        if ((Assembly) assemblyFromResolveEvent != (Assembly) null)
          assembly = (Assembly) assemblyFromResolveEvent;
        else if (assemblyName.ContentType == AssemblyContentType.WindowsRuntime)
          type = Type.GetType(typeName + ", " + assemblyString, true, ignoreCase);
        else
          assembly = (Assembly) RuntimeAssembly.InternalLoadAssemblyName(assemblyName, securityInfo, (RuntimeAssembly) null, ref stackMark, true, false, false);
      }
      if (type == (Type) null)
      {
        if (assembly == (Assembly) null)
          return (ObjectHandle) null;
        type = assembly.GetType(typeName, true, ignoreCase);
      }
      object instance = Activator.CreateInstance(type, bindingAttr, binder, args, culture, activationAttributes);
      if (instance == null)
        return (ObjectHandle) null;
      return new ObjectHandle(instance);
    }

    /// <summary>使用指定的程序集文件和与指定参数匹配程度最高的构造函数来创建指定名称的类型的实例。</summary>
    /// <returns>要访问新创建的实例则必须解包的句柄。</returns>
    /// <param name="assemblyFile">包含某程序集的文件的名称，将在该程序集内查找名为 <paramref name="typeName" /> 的类型。</param>
    /// <param name="typeName">首选类型的名称。</param>
    /// <param name="ignoreCase">使用 true 指定对 <paramref name="typeName" /> 的搜索不区分大小写；使用 false 则指定搜索区分大小写。</param>
    /// <param name="bindingAttr">影响 <paramref name="typeName" /> 构造函数搜索的零个或多个位标志的组合。如果 <paramref name="bindingAttr" /> 为零，则对公共构造函数进行区分大小写的搜索。</param>
    /// <param name="binder">使用 <paramref name="bindingAttr" /> 和 <paramref name="args" /> 来查找和标识 <paramref name="typeName" /> 构造函数的对象。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="args">与要调用构造函数的参数数量、顺序和类型匹配的参数数组。如果 <paramref name="args" /> 为空数组或 null，则调用不带任何参数的构造函数（默认构造函数）。</param>
    /// <param name="culture">区域性特定的信息，这些信息控制将 <paramref name="args" /> 强制转换为 <paramref name="typeName" /> 构造函数所声明的正式类型。如果 <paramref name="culture" /> 为 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。这通常为包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组，该对象指定激活远程对象所需的 URL。此参数与客户端激活的对象相关。客户端激活是一项传统技术，保留用于向后兼容，但不建议用于新的开发。应改用 Windows Communication Foundation 来开发分布式应用程序。</param>
    /// <param name="securityInfo">用于做出安全策略决策和授予代码权限的信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的构造函数。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typename" /> 中未找到 <paramref name="assemblyFile" />。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyFile" /> 未找到。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.MemberAccessException">不能创建一个抽象类的实例或者用后期绑定机制调用了此成员。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">构造函数，通过反射调用引发了异常。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所需 <see cref="T:System.Security.Permissions.FileIOPermission" />。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="activationAttributes" /> 不是一个空数组和正在创建的类型不是派生自 <see cref="T:System.MarshalByRefObject" />。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效的程序集。- 或 -当前加载公共语言运行时 (CLR) 2.0 或更高版本，并 <paramref name="assemblyName" /> 编译为晚于当前加载的版本的 clr 版本。请注意.NET Framework 版本 2.0、 3.0 和 3.5 所有使用 CLR 版本 2.0。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstanceFrom which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityInfo)
    {
      if (securityInfo != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      return Activator.CreateInstanceFromInternal(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityInfo);
    }

    /// <summary>使用指定的程序集文件和与指定参数匹配程度最高的构造函数来创建指定名称的类型的实例。</summary>
    /// <returns>要访问新创建的实例则必须解包的句柄。</returns>
    /// <param name="assemblyFile">包含某程序集的文件的名称，将在该程序集内查找名为 <paramref name="typeName" /> 的类型。</param>
    /// <param name="typeName">首选类型的名称。</param>
    /// <param name="ignoreCase">使用 true 指定对 <paramref name="typeName" /> 的搜索不区分大小写；使用 false 则指定搜索区分大小写。</param>
    /// <param name="bindingAttr">影响 <paramref name="typeName" /> 构造函数搜索的零个或多个位标志的组合。如果 <paramref name="bindingAttr" /> 为零，则对公共构造函数进行区分大小写的搜索。</param>
    /// <param name="binder">使用 <paramref name="bindingAttr" /> 和 <paramref name="args" /> 来查找和标识 <paramref name="typeName" /> 构造函数的对象。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="args">与要调用构造函数的参数数量、顺序和类型匹配的参数数组。如果 <paramref name="args" /> 为空数组或 null，则调用不带任何参数的构造函数（默认构造函数）。</param>
    /// <param name="culture">区域性特定的信息，这些信息控制将 <paramref name="args" /> 强制转换为 <paramref name="typeName" /> 构造函数所声明的正式类型。如果 <paramref name="culture" /> 为 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。这通常为包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组，该对象指定激活远程对象所需的 URL。此参数与客户端激活的对象相关。客户端激活是一项传统技术，保留用于向后兼容，但不建议用于新的开发。应改用 Windows Communication Foundation 来开发分布式应用程序。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的构造函数。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typename" /> 中未找到 <paramref name="assemblyFile" />。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyFile" /> 未找到。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.MemberAccessException">不能创建一个抽象类的实例或者用后期绑定机制调用了此成员。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">构造函数，通过反射调用引发了异常。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所需 <see cref="T:System.Security.Permissions.FileIOPermission" />。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="activationAttributes" /> 不是一个空数组和正在创建的类型不是派生自 <see cref="T:System.MarshalByRefObject" />。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效的程序集。- 或 -当前加载公共语言运行时 (CLR) 2.0 或更高版本，并 <paramref name="assemblyName" /> 编译为晚于当前加载的版本的 clr 版本。请注意.NET Framework 版本 2.0、 3.0 和 3.5 所有使用 CLR 版本 2.0。</exception>
    public static ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
    {
      return Activator.CreateInstanceFromInternal(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, (Evidence) null);
    }

    private static ObjectHandle CreateInstanceFromInternal(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityInfo)
    {
      object instance = Activator.CreateInstance(Assembly.LoadFrom(assemblyFile, securityInfo).GetType(typeName, true, ignoreCase), bindingAttr, binder, args, culture, activationAttributes);
      if (instance == null)
        return (ObjectHandle) null;
      return new ObjectHandle(instance);
    }

    /// <summary>使用命名的程序集和默认构造函数，来创建其名称在指定的远程域中指定的类型的实例。</summary>
    /// <returns>要访问新创建的实例则必须解包的句柄。</returns>
    /// <param name="domain">在其中创建名为 <paramref name="typeName" /> 的类型的远程域。</param>
    /// <param name="assemblyName">将在其中查找名为 <paramref name="typeName" /> 的类型的程序集的名称。如果 <paramref name="assemblyName" /> 为 null，则搜索正在执行的程序集。</param>
    /// <param name="typeName">首选类型的完全限定名。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeName" /> 或 <paramref name="domain" /> 为 null。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的公共构造函数。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typename" /> 中未找到 <paramref name="assemblyName" />。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyName" /> 未找到。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.MemberAccessException">无法创建抽象类型的实例。- 或 -使用后期绑定机制调用了该成员。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">构造函数，通过反射调用引发了异常。</exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">不通过 COM 类型 <see cref="Overload:System.Type.GetTypeFromProgID" /> 或 <see cref="Overload:System.Type.GetTypeFromCLSID" />。</exception>
    /// <exception cref="T:System.NotSupportedException">创建 <see cref="T:System.TypedReference" />, ，<see cref="T:System.ArgIterator" />, ，<see cref="T:System.Void" />, ，和 <see cref="T:System.RuntimeArgumentHandle" /> 不支持类型或这些类型的数组。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyName" /> 不是有效的程序集。- 或 -当前加载公共语言运行时 (CLR) 2.0 或更高版本，并 <paramref name="assemblyName" /> 编译为晚于当前加载的版本的 clr 版本。请注意.NET Framework 版本 2.0、 3.0 和 3.5 所有使用 CLR 版本 2.0。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。- 或 -程序集名称或基本代码无效。</exception>
    [SecurityCritical]
    public static ObjectHandle CreateInstance(AppDomain domain, string assemblyName, string typeName)
    {
      if (domain == null)
        throw new ArgumentNullException("domain");
      return domain.InternalCreateInstanceWithNoSecurity(assemblyName, typeName);
    }

    /// <summary>使用命名的程序集和最匹配所指定参数的构造函数，来创建其名称在指定的远程域中指定的类型的实例。</summary>
    /// <returns>要访问新创建的实例则必须解包的句柄。</returns>
    /// <param name="domain">在其中创建名为 <paramref name="typeName" /> 的类型的域。</param>
    /// <param name="assemblyName">将在其中查找名为 <paramref name="typeName" /> 的类型的程序集的名称。如果 <paramref name="assemblyName" /> 为 null，则搜索正在执行的程序集。</param>
    /// <param name="typeName">首选类型的完全限定名。</param>
    /// <param name="ignoreCase">使用 true 指定对 <paramref name="typeName" /> 的搜索不区分大小写；使用 false 则指定搜索区分大小写。</param>
    /// <param name="bindingAttr">影响 <paramref name="typeName" /> 构造函数搜索的零个或多个位标志的组合。如果 <paramref name="bindingAttr" /> 为零，则对公共构造函数进行区分大小写的搜索。</param>
    /// <param name="binder">使用 <paramref name="bindingAttr" /> 和 <paramref name="args" /> 来查找和标识 <paramref name="typeName" /> 构造函数的对象。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="args">与要调用构造函数的参数数量、顺序和类型匹配的参数数组。如果 <paramref name="args" /> 为空数组或 null，则调用不带任何参数的构造函数（默认构造函数）。</param>
    /// <param name="culture">区域性特定的信息，这些信息控制将 <paramref name="args" /> 强制转换为 <paramref name="typeName" /> 构造函数所声明的正式类型。如果 <paramref name="culture" /> 为 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。通常为包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组。<see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 指定激活远程对象所需的 URL。</param>
    /// <param name="securityAttributes">用于做出安全策略决策和授予代码权限的信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="domain" /> 或 <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的构造函数。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typename" /> 中未找到 <paramref name="assemblyName" />。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyName" /> 未找到。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.MemberAccessException">不能创建一个抽象类的实例或者用后期绑定机制调用了此成员。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">构造函数，通过反射调用引发了异常。</exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">不通过 COM 类型 <see cref="Overload:System.Type.GetTypeFromProgID" /> 或 <see cref="Overload:System.Type.GetTypeFromCLSID" />。</exception>
    /// <exception cref="T:System.NotSupportedException">创建 <see cref="T:System.TypedReference" />, ，<see cref="T:System.ArgIterator" />, ，<see cref="T:System.Void" />, ，和 <see cref="T:System.RuntimeArgumentHandle" /> 不支持类型或这些类型的数组。- 或 - <paramref name="activationAttributes" /> 不是一个空数组和正在创建的类型不是派生自 <see cref="T:System.MarshalByRefObject" />。- 或 -最匹配的构造函数 <paramref name="args" /> 具有 varargs 参数。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyName" /> 不是有效的程序集。- 或 -当前加载公共语言运行时 (CLR) 2.0 或更高版本，并 <paramref name="assemblyName" /> 编译为晚于当前加载的版本的 clr 版本。请注意.NET Framework 版本 2.0、 3.0 和 3.5 所有使用 CLR 版本 2.0。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。- 或 -程序集名称或基本代码无效。</exception>
    [SecurityCritical]
    [Obsolete("Methods which use evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstance which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public static ObjectHandle CreateInstance(AppDomain domain, string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
    {
      if (domain == null)
        throw new ArgumentNullException("domain");
      if (securityAttributes != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      return domain.InternalCreateInstanceWithNoSecurity(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
    }

    /// <summary>使用命名的程序集和最匹配所指定参数的构造函数，来创建其名称在指定的远程域中指定的类型的实例。</summary>
    /// <returns>要访问新创建的实例则必须解包的句柄。</returns>
    /// <param name="domain">在其中创建名为 <paramref name="typeName" /> 的类型的域。</param>
    /// <param name="assemblyName">将在其中查找名为 <paramref name="typeName" /> 的类型的程序集的名称。如果 <paramref name="assemblyName" /> 为 null，则搜索正在执行的程序集。</param>
    /// <param name="typeName">首选类型的完全限定名。</param>
    /// <param name="ignoreCase">使用 true 指定对 <paramref name="typeName" /> 的搜索不区分大小写；使用 false 则指定搜索区分大小写。</param>
    /// <param name="bindingAttr">影响 <paramref name="typeName" /> 构造函数搜索的零个或多个位标志的组合。如果 <paramref name="bindingAttr" /> 为零，则对公共构造函数进行区分大小写的搜索。</param>
    /// <param name="binder">使用 <paramref name="bindingAttr" /> 和 <paramref name="args" /> 来查找和标识 <paramref name="typeName" /> 构造函数的对象。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="args">与要调用构造函数的参数数量、顺序和类型匹配的参数数组。如果 <paramref name="args" /> 为空数组或 null，则调用不带任何参数的构造函数（默认构造函数）。</param>
    /// <param name="culture">区域性特定的信息，这些信息控制将 <paramref name="args" /> 强制转换为 <paramref name="typeName" /> 构造函数所声明的正式类型。如果 <paramref name="culture" /> 为 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。这通常为包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组，该对象指定激活远程对象所需的 URL。此参数与客户端激活的对象相关。客户端激活是一项传统技术，保留用于向后兼容，但不建议用于新的开发。应改用 Windows Communication Foundation 来开发分布式应用程序。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="domain" /> 或 <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的构造函数。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typename" /> 中未找到 <paramref name="assemblyName" />。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyName" /> 未找到。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.MemberAccessException">不能创建一个抽象类的实例或者用后期绑定机制调用了此成员。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">构造函数，通过反射调用引发了异常。</exception>
    /// <exception cref="T:System.Runtime.InteropServices.InvalidComObjectException">不通过 COM 类型 <see cref="Overload:System.Type.GetTypeFromProgID" /> 或 <see cref="Overload:System.Type.GetTypeFromCLSID" />。</exception>
    /// <exception cref="T:System.NotSupportedException">创建 <see cref="T:System.TypedReference" />, ，<see cref="T:System.ArgIterator" />, ，<see cref="T:System.Void" />, ，和 <see cref="T:System.RuntimeArgumentHandle" /> 不支持类型或这些类型的数组。- 或 - <paramref name="activationAttributes" /> 不是一个空数组和正在创建的类型不是派生自 <see cref="T:System.MarshalByRefObject" />。- 或 -最匹配的构造函数 <paramref name="args" /> 具有 varargs 参数。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyName" /> 不是有效的程序集。- 或 -当前加载公共语言运行时 (CLR) 2.0 或更高版本，并 <paramref name="assemblyName" /> 编译为晚于当前加载的版本的 clr 版本。请注意.NET Framework 版本 2.0、 3.0 和 3.5 所有使用 CLR 版本 2.0。</exception>
    /// <exception cref="T:System.IO.FileLoadException">两次用两个不同的证据加载了程序集或模块。- 或 -程序集名称或基本代码无效。</exception>
    [SecurityCritical]
    public static ObjectHandle CreateInstance(AppDomain domain, string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
    {
      if (domain == null)
        throw new ArgumentNullException("domain");
      return domain.InternalCreateInstanceWithNoSecurity(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, (Evidence) null);
    }

    /// <summary>使用命名的程序集文件和默认构造函数，来创建其名称在指定的远程域中指定的类型的实例。</summary>
    /// <returns>要访问新创建的实例则必须解包的句柄。</returns>
    /// <param name="domain">在其中创建名为 <paramref name="typeName" /> 的类型的远程域。</param>
    /// <param name="assemblyFile">包含某程序集的文件的名称，将在该程序集内查找名为 <paramref name="typeName" /> 的类型。</param>
    /// <param name="typeName">首选类型的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="domain" /> 或 <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的公共构造函数。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typename" /> 中未找到 <paramref name="assemblyFile" />。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyFile" /> 未找到。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.MemberAccessException">不能创建一个抽象类的实例或者用后期绑定机制调用了此成员。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">构造函数，通过反射调用引发了异常。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所需 <see cref="T:System.Security.Permissions.FileIOPermission" />。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效的程序集。- 或 -当前加载公共语言运行时 (CLR) 2.0 或更高版本，并 <paramref name="assemblyName" /> 编译为晚于当前加载的版本的 clr 版本。请注意.NET Framework 版本 2.0、 3.0 和 3.5 所有使用 CLR 版本 2.0。</exception>
    [SecurityCritical]
    public static ObjectHandle CreateInstanceFrom(AppDomain domain, string assemblyFile, string typeName)
    {
      if (domain == null)
        throw new ArgumentNullException("domain");
      return domain.InternalCreateInstanceFromWithNoSecurity(assemblyFile, typeName);
    }

    /// <summary>使用命名的程序集文件和最匹配所指定参数的构造函数，来创建其名称在指定的远程域中指定的类型的实例。</summary>
    /// <returns>要访问新创建的实例则必须解包的句柄。</returns>
    /// <param name="domain">在其中创建名为 <paramref name="typeName" /> 的类型的远程域。</param>
    /// <param name="assemblyFile">包含某程序集的文件的名称，将在该程序集内查找名为 <paramref name="typeName" /> 的类型。</param>
    /// <param name="typeName">首选类型的名称。</param>
    /// <param name="ignoreCase">使用 true 指定对 <paramref name="typeName" /> 的搜索不区分大小写；使用 false 则指定搜索区分大小写。</param>
    /// <param name="bindingAttr">影响 <paramref name="typeName" /> 构造函数搜索的零个或多个位标志的组合。如果 <paramref name="bindingAttr" /> 为零，则对公共构造函数进行区分大小写的搜索。</param>
    /// <param name="binder">使用 <paramref name="bindingAttr" /> 和 <paramref name="args" /> 来查找和标识 <paramref name="typeName" /> 构造函数的对象。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="args">与要调用构造函数的参数数量、顺序和类型匹配的参数数组。如果 <paramref name="args" /> 为空数组或 null，则调用不带任何参数的构造函数（默认构造函数）。</param>
    /// <param name="culture">区域性特定的信息，这些信息控制将 <paramref name="args" /> 强制转换为 <paramref name="typeName" /> 构造函数所声明的正式类型。如果 <paramref name="culture" /> 为 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。这通常为包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组，该对象指定激活远程对象所需的 URL。此参数与客户端激活的对象相关。客户端激活是一项传统技术，保留用于向后兼容，但不建议用于新的开发。应改用 Windows Communication Foundation 来开发分布式应用程序。</param>
    /// <param name="securityAttributes">用于做出安全策略决策和授予代码权限的信息。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="domain" /> 或 <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的构造函数。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typename" /> 中未找到 <paramref name="assemblyFile" />。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyFile" /> 未找到。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.MemberAccessException">不能创建一个抽象类的实例或者用后期绑定机制调用了此成员。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">构造函数，通过反射调用引发了异常。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所需 <see cref="T:System.Security.Permissions.FileIOPermission" />。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="activationAttributes" /> 不是一个空数组和正在创建的类型不是派生自 <see cref="T:System.MarshalByRefObject" />。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效的程序集。- 或 -当前加载公共语言运行时 (CLR) 2.0 或更高版本，并 <paramref name="assemblyName" /> 编译为晚于当前加载的版本的 clr 版本。请注意.NET Framework 版本 2.0、 3.0 和 3.5 所有使用 CLR 版本 2.0。</exception>
    [SecurityCritical]
    [Obsolete("Methods which use Evidence to sandbox are obsolete and will be removed in a future release of the .NET Framework. Please use an overload of CreateInstanceFrom which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    public static ObjectHandle CreateInstanceFrom(AppDomain domain, string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
    {
      if (domain == null)
        throw new ArgumentNullException("domain");
      if (securityAttributes != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
      return domain.InternalCreateInstanceFromWithNoSecurity(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
    }

    /// <summary>使用命名的程序集文件和最匹配所指定参数的构造函数，来创建其名称在指定的远程域中指定的类型的实例。</summary>
    /// <returns>要访问新创建的实例则必须解包的句柄。</returns>
    /// <param name="domain">在其中创建名为 <paramref name="typeName" /> 的类型的远程域。</param>
    /// <param name="assemblyFile">包含某程序集的文件的名称，将在该程序集内查找名为 <paramref name="typeName" /> 的类型。</param>
    /// <param name="typeName">首选类型的名称。</param>
    /// <param name="ignoreCase">使用 true 指定对 <paramref name="typeName" /> 的搜索不区分大小写；使用 false 则指定搜索区分大小写。</param>
    /// <param name="bindingAttr">影响 <paramref name="typeName" /> 构造函数搜索的零个或多个位标志的组合。如果 <paramref name="bindingAttr" /> 为零，则对公共构造函数进行区分大小写的搜索。</param>
    /// <param name="binder">使用 <paramref name="bindingAttr" /> 和 <paramref name="args" /> 来查找和标识 <paramref name="typeName" /> 构造函数的对象。如果 <paramref name="binder" /> 为 null，则使用默认联编程序。</param>
    /// <param name="args">与要调用构造函数的参数数量、顺序和类型匹配的参数数组。如果 <paramref name="args" /> 为空数组或 null，则调用不带任何参数的构造函数（默认构造函数）。</param>
    /// <param name="culture">区域性特定的信息，这些信息控制将 <paramref name="args" /> 强制转换为 <paramref name="typeName" /> 构造函数所声明的正式类型。如果 <paramref name="culture" /> 为 null，则使用当前线程的 <see cref="T:System.Globalization.CultureInfo" />。</param>
    /// <param name="activationAttributes">包含一个或多个可以参与激活的特性的数组。这通常为包含单个 <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> 对象的数组，该对象指定激活远程对象所需的 URL。此参数与客户端激活的对象相关。客户端激活是一项传统技术，保留用于向后兼容，但不建议用于新的开发。应改用 Windows Communication Foundation 来开发分布式应用程序。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="domain" /> 或 <paramref name="typeName" /> 为 null。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的构造函数。</exception>
    /// <exception cref="T:System.TypeLoadException">
    /// <paramref name="typename" /> 中未找到 <paramref name="assemblyFile" />。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyFile" /> 未找到。</exception>
    /// <exception cref="T:System.MethodAccessException">调用方没有权限来调用此构造函数。</exception>
    /// <exception cref="T:System.MemberAccessException">不能创建一个抽象类的实例或者用后期绑定机制调用了此成员。</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">构造函数，通过反射调用引发了异常。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所需 <see cref="T:System.Security.Permissions.FileIOPermission" />。</exception>
    /// <exception cref="T:System.NotSupportedException">
    /// <paramref name="activationAttributes" /> 不是一个空数组和正在创建的类型不是派生自 <see cref="T:System.MarshalByRefObject" />。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyFile" /> 不是有效的程序集。- 或 -<paramref name="assemblyName" /> 已编译的版本晚于当前加载的版本的公共语言运行。</exception>
    [SecurityCritical]
    public static ObjectHandle CreateInstanceFrom(AppDomain domain, string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
    {
      if (domain == null)
        throw new ArgumentNullException("domain");
      return domain.InternalCreateInstanceFromWithNoSecurity(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, (Evidence) null);
    }

    /// <summary>创建类型的一个实例，该类型由指定的 <see cref="T:System.ActivationContext" /> 对象指定。</summary>
    /// <returns>要访问新创建的对象则必须解包的句柄。</returns>
    /// <param name="activationContext">一个激活上下文对象，用于指定要创建的对象。</param>
    [SecuritySafeCritical]
    public static ObjectHandle CreateInstance(ActivationContext activationContext)
    {
      return (AppDomain.CurrentDomain.DomainManager ?? new AppDomainManager()).ApplicationActivator.CreateInstance(activationContext);
    }

    /// <summary>创建类型的一个实例，该类型由指定的 <see cref="T:System.ActivationContext" /> 对象指定，并由指定的自定义激活数据激活。</summary>
    /// <returns>要访问新创建的对象则必须解包的句柄。</returns>
    /// <param name="activationContext">一个激活上下文对象，用于指定要创建的对象。</param>
    /// <param name="activationCustomData">一个包含自定义激活数据的 Unicode 字符串数组。</param>
    [SecuritySafeCritical]
    public static ObjectHandle CreateInstance(ActivationContext activationContext, string[] activationCustomData)
    {
      return (AppDomain.CurrentDomain.DomainManager ?? new AppDomainManager()).ApplicationActivator.CreateInstance(activationContext, activationCustomData);
    }

    /// <summary>使用指定的程序集文件和与指定参数匹配程度最高的构造函数来创建指定名称的 COM 对象的实例。</summary>
    /// <returns>要访问新创建的实例则必须解包的句柄。</returns>
    /// <param name="assemblyName">包含某程序集的文件的名称，将在该程序集内查找名为 <paramref name="typeName" /> 的类型。</param>
    /// <param name="typeName">首选类型的名称。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeName" /> 或 <paramref name="assemblyName" /> 为 null。</exception>
    /// <exception cref="T:System.TypeLoadException">不能通过 COM 创建实例- 或 -<paramref name="typename" /> 中未找到 <paramref name="assemblyName" />。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的构造函数。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyName" /> 未找到，或者您试图加载的模块没有指定文件扩展名。</exception>
    /// <exception cref="T:System.MemberAccessException">无法创建抽象类的实例。- 或 -使用后期绑定机制调用了该成员。</exception>
    /// <exception cref="T:System.NotSupportedException">调用方不能提供一个对象，不会继承从激活特性 <see cref="T:System.MarshalByRefObject" />。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="assemblyName" /> 为空字符串 ("")。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public static ObjectHandle CreateComInstanceFrom(string assemblyName, string typeName)
    {
      return Activator.CreateComInstanceFrom(assemblyName, typeName, (byte[]) null, AssemblyHashAlgorithm.None);
    }

    /// <summary>使用命名的程序集文件和默认构造函数，创建指定名称的 COM 对象的实例。</summary>
    /// <returns>要访问新创建的实例则必须解包的句柄。</returns>
    /// <param name="assemblyName">包含某程序集的文件的名称，将在该程序集内查找名为 <paramref name="typeName" /> 的类型。</param>
    /// <param name="typeName">首选类型的名称。</param>
    /// <param name="hashValue">计算所得的哈希代码的值。</param>
    /// <param name="hashAlgorithm">用于对文件进行哈希计算并生成强名称的哈希算法。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeName" /> 或 <paramref name="assemblyName" /> 为 null。</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="assemblyName" /> 为空字符串 ("")。</exception>
    /// <exception cref="T:System.IO.PathTooLongException">两次用两个不同的证据，加载了程序集或模块或程序集名称的长度超过 MAX_PATH 个字符。</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="assemblyName" /> 未找到，或者您试图加载的模块没有指定文件扩展名。</exception>
    /// <exception cref="T:System.IO.FileLoadException">
    /// <paramref name="assemblyName" /> 找到但不能加载。</exception>
    /// <exception cref="T:System.BadImageFormatException">
    /// <paramref name="assemblyName" /> 不是有效的程序集。</exception>
    /// <exception cref="T:System.Security.SecurityException">指定了不是以"file://"开头的代码库而不是所需 WebPermission。</exception>
    /// <exception cref="T:System.TypeLoadException">不能通过 COM 创建实例- 或 - <paramref name="typename" /> 中未找到 <paramref name="assemblyName" />。</exception>
    /// <exception cref="T:System.MissingMethodException">不找到任何匹配的构造函数。</exception>
    /// <exception cref="T:System.MemberAccessException">不能创建抽象类的实例。- 或 -使用后期绑定机制调用了该成员。</exception>
    /// <exception cref="T:System.NotSupportedException">调用方不能提供一个对象，不会继承从激活特性 <see cref="T:System.MarshalByRefObject" />。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    /// </PermissionSet>
    public static ObjectHandle CreateComInstanceFrom(string assemblyName, string typeName, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
    {
      Assembly assembly = Assembly.LoadFrom(assemblyName, hashValue, hashAlgorithm);
      string name = typeName;
      int num1 = 1;
      int num2 = 0;
      Type type = assembly.GetType(name, num1 != 0, num2 != 0);
      object[] customAttributes = type.GetCustomAttributes(typeof (ComVisibleAttribute), false);
      if (customAttributes.Length != 0 && !((ComVisibleAttribute) customAttributes[0]).Value)
        throw new TypeLoadException(Environment.GetResourceString("Argument_TypeMustBeVisibleFromCom"));
      // ISSUE: variable of the null type
      __Null local = null;
      if (assembly == (Assembly) local)
        return (ObjectHandle) null;
      object instance = Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, (Binder) null, (object[]) null, (CultureInfo) null, (object[]) null);
      if (instance == null)
        return (ObjectHandle) null;
      return new ObjectHandle(instance);
    }

    /// <summary>为指定类型和 URL 所指示的已知对象创建一个代理。</summary>
    /// <returns>一个代理，它指向由所请求的已知对象服务的终结点。</returns>
    /// <param name="type">希望连接到的已知对象的类型。</param>
    /// <param name="url">已知对象的 URL。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 或 <paramref name="url" /> 为 null。</exception>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    /// <paramref name="type" /> 不按引用封送并不是一个接口。</exception>
    /// <exception cref="T:System.MemberAccessException">使用后期绑定机制调用了该成员。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecurityCritical]
    public static object GetObject(Type type, string url)
    {
      return Activator.GetObject(type, url, (object) null);
    }

    /// <summary>为指定类型、URL 和通道数据所指示的已知对象创建一个代理。</summary>
    /// <returns>一个代理，它指向由所请求的已知对象服务的终结点。</returns>
    /// <param name="type">希望连接到的已知对象的类型。</param>
    /// <param name="url">已知对象的 URL。</param>
    /// <param name="state">通道特定的数据或 null。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 或 <paramref name="url" /> 为 null。</exception>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    /// <paramref name="type" /> 不按引用封送并不是一个接口。</exception>
    /// <exception cref="T:System.MemberAccessException">使用后期绑定机制调用了该成员。</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
    /// </PermissionSet>
    [SecurityCritical]
    public static object GetObject(Type type, string url, object state)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      return RemotingServices.Connect(type, url, state);
    }

    [Conditional("_DEBUG")]
    private static void Log(bool test, string title, string success, string failure)
    {
      int num = test ? 1 : 0;
    }

    void _Activator.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _Activator.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _Activator.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _Activator.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}
