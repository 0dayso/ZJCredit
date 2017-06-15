// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UCOMIRunningObjectTable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.IRunningObjectTable" />。</summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.IRunningObjectTable instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Guid("00000010-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface UCOMIRunningObjectTable
  {
    /// <summary>注册提供的对象已进入运行状态。</summary>
    /// <param name="grfFlags">指定运行对象表 (ROT) 对 <paramref name="punkObject" /> 的引用是弱引用还是强引用，并通过对象在 ROT 中的项控制对它的访问。</param>
    /// <param name="punkObject">对注册为运行对象的对象的引用。</param>
    /// <param name="pmkObjectName">对标识 <paramref name="punkObject" /> 的名字对象的引用。</param>
    /// <param name="pdwRegister">对 32 位值的引用，该值在随后对 <see cref="M:System.Runtime.InteropServices.UCOMIRunningObjectTable.Revoke(System.Int32)" /> 或 <see cref="M:System.Runtime.InteropServices.UCOMIRunningObjectTable.NoteChangeTime(System.Int32,System.Runtime.InteropServices.FILETIME@)" /> 的调用中可用于标识此 ROT 项。</param>
    void Register(int grfFlags, [MarshalAs(UnmanagedType.Interface)] object punkObject, UCOMIMoniker pmkObjectName, out int pdwRegister);

    /// <summary>从 ROT 中注销指定的对象。</summary>
    /// <param name="dwRegister">要撤消的 ROT 项。</param>
    void Revoke(int dwRegister);

    /// <summary>确定指定名字对象当前是否注册在运行对象表中。</summary>
    /// <param name="pmkObjectName">对要在运行对象表中搜索的名字对象的引用。</param>
    void IsRunning(UCOMIMoniker pmkObjectName);

    /// <summary>如果提供的对象名注册为运行对象，则返回该注册对象。</summary>
    /// <param name="pmkObjectName">对要在 ROT 中搜索的名字对象的引用。</param>
    /// <param name="ppunkObject">成功返回时包含所请求的运行对象。</param>
    void GetObject(UCOMIMoniker pmkObjectName, [MarshalAs(UnmanagedType.Interface)] out object ppunkObject);

    /// <summary>记下某个特定对象更改的时间，以使 IMoniker::GetTimeOfLastChange 能够报告适当的更改时间。</summary>
    /// <param name="dwRegister">所更改对象的 ROT 项。</param>
    /// <param name="pfiletime">对对象的上次更改时间的引用。</param>
    void NoteChangeTime(int dwRegister, ref FILETIME pfiletime);

    /// <summary>在 ROT 中搜索此名字对象并报告所记录的更改时间（如果存在的话）。</summary>
    /// <param name="pmkObjectName">对要在 ROT 中搜索的名字对象的引用。</param>
    /// <param name="pfiletime">成功返回时包含对象上次更改的时间。</param>
    void GetTimeOfLastChange(UCOMIMoniker pmkObjectName, out FILETIME pfiletime);

    /// <summary>枚举当前注册为运行对象的对象。</summary>
    /// <param name="ppenumMoniker">成功返回时 ROT 的新枚举数。</param>
    void EnumRunning(out UCOMIEnumMoniker ppenumMoniker);
  }
}
