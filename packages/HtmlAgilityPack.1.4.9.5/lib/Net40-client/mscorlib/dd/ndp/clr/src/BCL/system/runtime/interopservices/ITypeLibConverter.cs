// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ITypeLibConverter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
  /// <summary>提供一组服务，将托管程序集转换为 COM 类型库或进行反向转换。</summary>
  [Guid("F1C3BF78-C3E4-11d3-88E7-00902754C43A")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComVisible(true)]
  public interface ITypeLibConverter
  {
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
    AssemblyBuilder ConvertTypeLibToAssembly([MarshalAs(UnmanagedType.Interface)] object typeLib, string asmFileName, TypeLibImporterFlags flags, ITypeLibImporterNotifySink notifySink, byte[] publicKey, StrongNameKeyPair keyPair, string asmNamespace, Version asmVersion);

    /// <summary>将程序集转换为 COM 类型库。</summary>
    /// <returns>实现 ITypeLib 接口的对象。</returns>
    /// <param name="assembly">要转换的程序集。</param>
    /// <param name="typeLibName">所产生的类型库的文件名。</param>
    /// <param name="flags">指示任何特殊设置的 <see cref="T:System.Runtime.InteropServices.TypeLibExporterFlags" /> 值。</param>
    /// <param name="notifySink">由调用方实现的 <see cref="T:System.Runtime.InteropServices.ITypeLibExporterNotifySink" /> 接口。</param>
    [return: MarshalAs(UnmanagedType.Interface)]
    object ConvertAssemblyToTypeLib(Assembly assembly, string typeLibName, TypeLibExporterFlags flags, ITypeLibExporterNotifySink notifySink);

    /// <summary>获取指定类型库的主 interop 程序集的名称及基本代码。</summary>
    /// <returns>如果在注册表中找到主 interop 程序集，则为 true；否则为 false。</returns>
    /// <param name="g">类型库的 GUID。</param>
    /// <param name="major">类型库的主版本号。</param>
    /// <param name="minor">类型库的次版本号。</param>
    /// <param name="lcid">类型库的 LCID。</param>
    /// <param name="asmName">成功返回时，为与 <paramref name="g" /> 关联的主 interop 程序集的名称。</param>
    /// <param name="asmCodeBase">成功返回时，为与 <paramref name="g" /> 关联的主 interop 程序集的基本代码。</param>
    bool GetPrimaryInteropAssembly(Guid g, int major, int minor, int lcid, out string asmName, out string asmCodeBase);

    /// <summary>将 COM 类型库转换为程序集。</summary>
    /// <returns>包含已转换类型库的 <see cref="T:System.Reflection.Emit.AssemblyBuilder" /> 对象。</returns>
    /// <param name="typeLib">实现 ITypeLib 接口的对象。</param>
    /// <param name="asmFileName">所产生的程序集的文件名。</param>
    /// <param name="flags">指示任何特殊设置的 <see cref="T:System.Runtime.InteropServices.TypeLibImporterFlags" /> 值。</param>
    /// <param name="notifySink">由调用方实现的 <see cref="T:System.Runtime.InteropServices.ITypeLibImporterNotifySink" /> 接口。</param>
    /// <param name="publicKey">包含公钥的 byte 数组。</param>
    /// <param name="keyPair">包含加密公钥和私钥对的 <see cref="T:System.Reflection.StrongNameKeyPair" /> 对象。</param>
    /// <param name="unsafeInterfaces">如果为 true，则接口要求在链接时检查 <see cref="F:System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode" /> 权限。如果为 false，则接口要求在运行时检查，运行时检查需要堆栈审核且更加昂贵，但有助于提供更大的保护。</param>
    AssemblyBuilder ConvertTypeLibToAssembly([MarshalAs(UnmanagedType.Interface)] object typeLib, string asmFileName, int flags, ITypeLibImporterNotifySink notifySink, byte[] publicKey, StrongNameKeyPair keyPair, bool unsafeInterfaces);
  }
}
