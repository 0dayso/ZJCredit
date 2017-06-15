// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.IBindCtx
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>提供 IBindCtx 接口的托管定义。</summary>
  [Guid("0000000e-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [__DynamicallyInvokable]
  [ComImport]
  public interface IBindCtx
  {
    /// <summary>将传递的对象注册为已在名字对象操作期间绑定且应在此操作完成之后释放的对象之一。</summary>
    /// <param name="punk">要为释放而注册的对象。</param>
    [__DynamicallyInvokable]
    void RegisterObjectBound([MarshalAs(UnmanagedType.Interface)] object punk);

    /// <summary>从需要释放的注册对象集中移除该对象。</summary>
    /// <param name="punk">要为释放而注销的对象。</param>
    [__DynamicallyInvokable]
    void RevokeObjectBound([MarshalAs(UnmanagedType.Interface)] object punk);

    /// <summary>释放所有当前由 <see cref="M:System.Runtime.InteropServices.ComTypes.IBindCtx.RegisterObjectBound(System.Object)" /> 方法用绑定上下文注册的对象。</summary>
    [__DynamicallyInvokable]
    void ReleaseBoundObjects();

    /// <summary>在绑定上下文中存储参数块。这些参数将应用于稍后使用此绑定上下文的 UCOMIMoniker 操作。</summary>
    /// <param name="pbindopts">包含要设置的绑定选项的结构。</param>
    [__DynamicallyInvokable]
    void SetBindOptions([In] ref BIND_OPTS pbindopts);

    /// <summary>返回当前存储在当前绑定上下文中的绑定选项。</summary>
    /// <param name="pbindopts">指向接收绑定选项的结构的指针。</param>
    [__DynamicallyInvokable]
    void GetBindOptions(ref BIND_OPTS pbindopts);

    /// <summary>返回对与此绑定进程相关的运行对象表 (ROT) 的访问权。</summary>
    /// <param name="pprot">此方法返回时，包含对运行对象表 (ROT) 的引用。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void GetRunningObjectTable(out IRunningObjectTable pprot);

    /// <summary>以内部维护的对象指针表中的指定名称注册指定的对象指针。</summary>
    /// <param name="pszKey">用于注册 <paramref name="punk" /> 的名称。</param>
    /// <param name="punk">要注册的对象。</param>
    [__DynamicallyInvokable]
    void RegisterObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey, [MarshalAs(UnmanagedType.Interface)] object punk);

    /// <summary>在内部维护的上下文对象参数表中查找给定的项并返回相应的对象（如果此对象存在的话）。</summary>
    /// <param name="pszKey">要搜索的对象名称。</param>
    /// <param name="ppunk">此方法返回时，包含对象接口指针。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void GetObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey, [MarshalAs(UnmanagedType.Interface)] out object ppunk);

    /// <summary>枚举字符串，这些字符串是在内部维护的上下文对象参数表的项。</summary>
    /// <param name="ppenum">此方法返回时，包含对对象参数枚举数的引用。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void EnumObjectParam(out IEnumString ppenum);

    /// <summary>撤消当前在指定项（位于内部维护的上下文对象参数表中）下可找到的对象的注册（如果该项当前已注册）。</summary>
    /// <returns>如果从表中成功移除指定键，则为 S_OKHRESULT 值；否则为 S_FALSEHRESULT 值。</returns>
    /// <param name="pszKey">要注销的项。</param>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int RevokeObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey);
  }
}
