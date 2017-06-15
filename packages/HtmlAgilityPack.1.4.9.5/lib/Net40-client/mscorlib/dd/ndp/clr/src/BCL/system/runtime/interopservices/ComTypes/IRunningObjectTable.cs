// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.IRunningObjectTable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>提供 IRunningObjectTable 接口的托管定义。</summary>
  [Guid("00000010-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [__DynamicallyInvokable]
  [ComImport]
  public interface IRunningObjectTable
  {
    /// <summary>注册提供的对象已进入运行状态。</summary>
    /// <returns>一个值，该值可用于在随后对 <see cref="M:System.Runtime.InteropServices.ComTypes.IRunningObjectTable.Revoke(System.Int32)" /> 或 <see cref="M:System.Runtime.InteropServices.ComTypes.IRunningObjectTable.NoteChangeTime(System.Int32,System.Runtime.InteropServices.ComTypes.FILETIME@)" /> 的调用中标识此 ROT 项。</returns>
    /// <param name="grfFlags">指定运行对象表 (ROT) 对 <paramref name="punkObject" /> 的引用是弱引用还是强引用，并通过对象在 ROT 中的项控制对它的访问。</param>
    /// <param name="punkObject">对注册为运行对象的对象的引用。</param>
    /// <param name="pmkObjectName">对标识 <paramref name="punkObject" /> 的名字对象的引用。</param>
    [__DynamicallyInvokable]
    int Register(int grfFlags, [MarshalAs(UnmanagedType.Interface)] object punkObject, IMoniker pmkObjectName);

    /// <summary>从运行对象表 (ROT) 中注销指定的对象。</summary>
    /// <param name="dwRegister">要撤消的运行对象表 (ROT) 项。</param>
    [__DynamicallyInvokable]
    void Revoke(int dwRegister);

    /// <summary>确定指定名字对象当前是否在运行对象表 (ROT) 中注册。</summary>
    /// <returns>一个 HRESULT 值，该值指示操作是成功还是失败。</returns>
    /// <param name="pmkObjectName">对要在运行对象表 (ROT) 中搜索的名字对象的引用。</param>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsRunning(IMoniker pmkObjectName);

    /// <summary>如果提供的对象名注册为运行对象，则返回该注册对象。</summary>
    /// <returns>一个 HRESULT 值，该值指示操作是成功还是失败。</returns>
    /// <param name="pmkObjectName">对要在运行对象表 (ROT) 中搜索的名字对象的引用。</param>
    /// <param name="ppunkObject">此方法返回时，包含请求的运行对象。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetObject(IMoniker pmkObjectName, [MarshalAs(UnmanagedType.Interface)] out object ppunkObject);

    /// <summary>记录特定对象发生更改的时间，以便 IMoniker::GetTimeOfLastChange 可以报告相应更改时间。</summary>
    /// <param name="dwRegister">已更改对象的运行对象表 (ROT) 项。</param>
    /// <param name="pfiletime">对对象的上次更改时间的引用。</param>
    [__DynamicallyInvokable]
    void NoteChangeTime(int dwRegister, ref FILETIME pfiletime);

    /// <summary>在运行对象表 (ROT) 中搜索此名字对象并报告所记录的更改时间（如果存在的话）。</summary>
    /// <returns>一个 HRESULT 值，该值指示操作是成功还是失败。</returns>
    /// <param name="pmkObjectName">对要在运行对象表 (ROT) 中搜索的名字对象的引用。</param>
    /// <param name="pfiletime">此对象返回时，包含对象的上次更改时间。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetTimeOfLastChange(IMoniker pmkObjectName, out FILETIME pfiletime);

    /// <summary>枚举当前注册为运行对象的对象。</summary>
    /// <param name="ppenumMoniker">此方法返回时，包含运行对象表 (ROT) 的新枚举器。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void EnumRunning(out IEnumMoniker ppenumMoniker);
  }
}
