// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UCOMIConnectionPoint
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.IConnectionPoint" />。</summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.IConnectionPoint instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Guid("B196B286-BAB4-101A-B69C-00AA00341D07")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface UCOMIConnectionPoint
  {
    /// <summary>返回由此连接点管理的输出接口的 IID。</summary>
    /// <param name="pIID">成功返回时，包含由此连接点管理的输出接口的 IID。</param>
    void GetConnectionInterface(out Guid pIID);

    /// <summary>检索指向在概念上拥有此连接点的可连接对象的 IConnectionPointContainer 接口指针。</summary>
    /// <param name="ppCPC">在成功返回时包含可连接对象的 IConnectionPointContainer 接口。</param>
    void GetConnectionPointContainer(out UCOMIConnectionPointContainer ppCPC);

    /// <summary>在连接点和调用方的接收器对象之间建立一个通知连接。</summary>
    /// <param name="pUnkSink">对接收器的引用，该接收器为此连接点所管理的输出接口接收调用。</param>
    /// <param name="pdwCookie">在成功返回时包含连接 Cookie。</param>
    void Advise([MarshalAs(UnmanagedType.Interface)] object pUnkSink, out int pdwCookie);

    /// <summary>终止先前通过 <see cref="M:System.Runtime.InteropServices.UCOMIConnectionPoint.Advise(System.Object,System.Int32@)" /> 建立的通知连接。</summary>
    /// <param name="dwCookie">先前从 <see cref="M:System.Runtime.InteropServices.UCOMIConnectionPoint.Advise(System.Object,System.Int32@)" /> 返回的连接 Cookie。</param>
    void Unadvise(int dwCookie);

    /// <summary>创建枚举数对象，以便循环访问到此连接点的现有连接。</summary>
    /// <param name="ppEnum">在成功返回时包含新创建的枚举数。</param>
    void EnumConnections(out UCOMIEnumConnections ppEnum);
  }
}
