// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.IConnectionPointContainer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>提供 IConnectionPointContainer 接口的托管定义。</summary>
  [Guid("B196B284-BAB4-101A-B69C-00AA00341D07")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [__DynamicallyInvokable]
  [ComImport]
  public interface IConnectionPointContainer
  {
    /// <summary>创建在可连接对象中支持的所有连接点的枚举数，每个 IID 一个连接点。</summary>
    /// <param name="ppEnum">此方法返回时，包含枚举数的接口指针。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void EnumConnectionPoints(out IEnumConnectionPoints ppEnum);

    /// <summary>询问可连接对象是否具有某个特定 IID 的连接点，如果是，则返回指向此连接点的 IConnectionPoint 接口指针。</summary>
    /// <param name="riid">对输出接口 IID 的引用，此输出接口 IID 的连接点正在被请求。</param>
    /// <param name="ppCP">此方法返回时，包含管理输出接口 <paramref name="riid" /> 的连接点。该参数未经初始化即被传递。</param>
    [__DynamicallyInvokable]
    void FindConnectionPoint([In] ref Guid riid, out IConnectionPoint ppCP);
  }
}
