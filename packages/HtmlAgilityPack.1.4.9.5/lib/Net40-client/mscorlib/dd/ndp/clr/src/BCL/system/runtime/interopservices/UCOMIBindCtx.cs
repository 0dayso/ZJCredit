// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UCOMIBindCtx
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.BIND_OPTS" />。</summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.IBindCtx instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Guid("0000000e-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface UCOMIBindCtx
  {
    /// <summary>将传递的对象注册为已在名字对象操作期间绑定且应在此操作完成之后释放的对象之一。</summary>
    /// <param name="punk">要为释放而注册的对象。</param>
    void RegisterObjectBound([MarshalAs(UnmanagedType.Interface)] object punk);

    /// <summary>从需要释放的注册对象集中移除该对象。</summary>
    /// <param name="punk">要为释放而注销的对象。</param>
    void RevokeObjectBound([MarshalAs(UnmanagedType.Interface)] object punk);

    /// <summary>释放所有当前由 <see cref="M:System.Runtime.InteropServices.UCOMIBindCtx.RegisterObjectBound(System.Object)" /> 用上下文绑定注册的对象。</summary>
    void ReleaseBoundObjects();

    /// <summary>在上下文绑定中存储一个参数块，此参数块将应用于稍后使用此上下文绑定的 UCOMIMoniker 操作。</summary>
    /// <param name="pbindopts">包含要设置的绑定选项的结构。</param>
    void SetBindOptions([In] ref BIND_OPTS pbindopts);

    /// <summary>返回当前存储在此上下文绑定中的绑定选项。</summary>
    /// <param name="pbindopts">指向接收绑定选项的结构的指针。</param>
    void GetBindOptions(ref BIND_OPTS pbindopts);

    /// <summary>返回对与此绑定进程相关的运行对象表 (ROT) 的访问权。</summary>
    /// <param name="pprot">成功返回时对 ROT 的引用。</param>
    void GetRunningObjectTable(out UCOMIRunningObjectTable pprot);

    /// <summary>以内部维护的对象指针表中的指定名称注册给定的对象指针。</summary>
    /// <param name="pszKey">用于注册 <paramref name="punk" /> 的名称。</param>
    /// <param name="punk">要注册的对象。</param>
    void RegisterObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey, [MarshalAs(UnmanagedType.Interface)] object punk);

    /// <summary>在内部维护的上下文对象参数表中查找给定的项并返回相应的对象（如果此对象存在的话）。</summary>
    /// <param name="pszKey">要搜索的对象名称。</param>
    /// <param name="ppunk">成功返回时的对象接口指针。</param>
    void GetObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey, [MarshalAs(UnmanagedType.Interface)] out object ppunk);

    /// <summary>枚举字符串，它们是在内部维护的上下文对象参数表的项。</summary>
    /// <param name="ppenum">成功返回时，是对对象参数枚举数的引用。</param>
    void EnumObjectParam(out UCOMIEnumString ppenum);

    /// <summary>撤消当前在此项（它位于内部维护的上下文对象参数表中）下面找到的对象的注册（如果当前注册了任何此类项的话）。</summary>
    /// <param name="pszKey">要注销的项。</param>
    void RevokeObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey);
  }
}
