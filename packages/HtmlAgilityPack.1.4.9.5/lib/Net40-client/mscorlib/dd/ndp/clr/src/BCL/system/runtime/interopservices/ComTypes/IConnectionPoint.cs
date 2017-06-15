// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.IConnectionPoint
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>提供 IConnectionPoint 接口的托管定义。</summary>
  [Guid("B196B286-BAB4-101A-B69C-00AA00341D07")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [__DynamicallyInvokable]
  [ComImport]
  public interface IConnectionPoint
  {
    /// <summary>返回由此连接点管理的输出接口的 IID。</summary>
    /// <param name="pIID">此参数返回时，包含由此连接点管理的输出接口的 IID。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void GetConnectionInterface(out Guid pIID);

    /// <summary>检索指向在概念上拥有此连接点的可连接对象的 IConnectionPointContainer 接口指针。</summary>
    /// <param name="ppCPC">此参数返回时，包含可连接对象的 IConnectionPointContainer 接口。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void GetConnectionPointContainer(out IConnectionPointContainer ppCPC);

    /// <summary>在连接点和调用方的接收器对象之间建立一个通知连接。</summary>
    /// <param name="pUnkSink">对接收器的引用，该接收器为此连接点所管理的输出接口接收调用。</param>
    /// <param name="pdwCookie">此方法返回时，包含连接 Cookie。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void Advise([MarshalAs(UnmanagedType.Interface)] object pUnkSink, out int pdwCookie);

    /// <summary>终止先前通过 <see cref="M:System.Runtime.InteropServices.ComTypes.IConnectionPoint.Advise(System.Object,System.Int32@)" /> 方法建立的顾问连接。</summary>
    /// <param name="dwCookie">先前从 <see cref="M:System.Runtime.InteropServices.ComTypes.IConnectionPoint.Advise(System.Object,System.Int32@)" /> 方法返回的连接 Cookie。</param>
    [__DynamicallyInvokable]
    void Unadvise(int dwCookie);

    /// <summary>创建枚举数对象，以便循环访问到此连接点的现有连接。</summary>
    /// <param name="ppEnum">此方法返回时，包含新创建的枚举数。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void EnumConnections(out IEnumConnections ppEnum);
  }
}
