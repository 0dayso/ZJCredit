// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UCOMIConnectionPointContainer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.IConnectionPointContainer" />。</summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.IConnectionPointContainer instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Guid("B196B284-BAB4-101A-B69C-00AA00341D07")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface UCOMIConnectionPointContainer
  {
    /// <summary>创建在可连接对象中支持的所有连接点的枚举数，每个 IID 一个连接点。</summary>
    /// <param name="ppEnum">在成功返回时包含枚举数的接口指针。</param>
    void EnumConnectionPoints(out UCOMIEnumConnectionPoints ppEnum);

    /// <summary>询问可连接对象是否具有某个特定 IID 的连接点，如果是，则返回指向此连接点的 IConnectionPoint 接口指针。</summary>
    /// <param name="riid">对输出接口 IID 的引用，此输出接口 IID 的连接点正在被请求。</param>
    /// <param name="ppCP">在成功返回时包含管理输出接口 <paramref name="riid" /> 的连接点。</param>
    void FindConnectionPoint(ref Guid riid, out UCOMIConnectionPoint ppCP);
  }
}
