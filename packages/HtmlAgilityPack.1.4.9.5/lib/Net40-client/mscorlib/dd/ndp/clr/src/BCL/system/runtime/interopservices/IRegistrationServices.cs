// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.IRegistrationServices
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>提供一组用于注册和注销托管程序集以供从 COM 使用的服务。</summary>
  [Guid("CCBD682C-73A5-4568-B8B0-C7007E11ABA2")]
  [ComVisible(true)]
  public interface IRegistrationServices
  {
    /// <summary>注册托管程序集中的类以便能够从 COM 创建。</summary>
    /// <returns>如果 <paramref name="assembly" /> 包含已成功注册的类型，则为 true；否则，如果程序集不包含符合条件的类型，则为 false。</returns>
    /// <param name="assembly">要注册的程序集。</param>
    /// <param name="flags">一个 <see cref="T:System.Runtime.InteropServices.AssemblyRegistrationFlags" /> 值，该值指示当注册 <paramref name="assembly" /> 时所需的任何特殊设置。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assembly" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="assembly" /> 的全名为 null。- 或 -用 <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> 标记的方法不是 static 方法。- 或 -在层次结构的给定级别有多个用 <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> 标记的方法。- 或 -用 <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> 标记的方法的签名无效。</exception>
    [SecurityCritical]
    bool RegisterAssembly(Assembly assembly, AssemblyRegistrationFlags flags);

    /// <summary>注销托管程序集中的类。</summary>
    /// <returns>如果 <paramref name="assembly" /> 包含已成功注销的类型，则为 true；否则，如果程序集不包含符合条件的类型，则为 false。</returns>
    /// <param name="assembly">要注销的程序集。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="assembly" /> 为 null。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="assembly" /> 的全名为 null。- 或 -用 <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> 标记的方法不是 static 方法。- 或 -在层次结构的给定级别有多个用 <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> 标记的方法。- 或 -用 <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> 标记的方法的签名无效。</exception>
    [SecurityCritical]
    bool UnregisterAssembly(Assembly assembly);

    /// <summary>在通过调用 <see cref="M:System.Runtime.InteropServices.IRegistrationServices.RegisterAssembly(System.Reflection.Assembly,System.Runtime.InteropServices.AssemblyRegistrationFlags)" /> 而注册的程序集中检索类的列表。</summary>
    /// <returns>
    /// <see cref="T:System.Type" /> 数组，它包含 <paramref name="assembly" /> 中的类的列表。</returns>
    /// <param name="assembly">要搜索类的程序集。</param>
    [SecurityCritical]
    Type[] GetRegistrableTypesInAssembly(Assembly assembly);

    /// <summary>检索指定类型的 COM ProgID。</summary>
    /// <returns>指定类型的 ProgID。</returns>
    /// <param name="type">请求其 ProgID 的类型。</param>
    [SecurityCritical]
    string GetProgIdForType(Type type);

    /// <summary>使用指定的 GUID 向 COM 注册指定的类型。</summary>
    /// <param name="type">要注册以供从 COM 使用的类型。</param>
    /// <param name="g">用于注册指定类型的 GUID。</param>
    [SecurityCritical]
    void RegisterTypeForComClients(Type type, ref Guid g);

    /// <summary>返回包含托管类的 COM 类别的 GUID。</summary>
    /// <returns>包含托管类的 COM 类别的 GUID。</returns>
    Guid GetManagedCategoryGuid();

    /// <summary>确定指定的类型是否需要注册。</summary>
    /// <returns>如果该类型必须注册以供从 COM 使用，则为 true；否则为 false。</returns>
    /// <param name="type">要检查其 COM 注册要求的类型。</param>
    [SecurityCritical]
    bool TypeRequiresRegistration(Type type);

    /// <summary>确定指定的类型是否是 COM 类型。</summary>
    /// <returns>如果指定的类型是 COM 类型，则为 true；否则为 false。</returns>
    /// <param name="type">要确定其是否是 COM 类型的类型。</param>
    bool TypeRepresentsComType(Type type);
  }
}
