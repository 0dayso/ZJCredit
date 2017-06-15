// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.CONNECTDATA
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>请改用 <see cref="T:System.Runtime.InteropServices.ComTypes.CONNECTDATA" />。</summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.CONNECTDATA instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct CONNECTDATA
  {
    /// <summary>表示指向已连接的通知接收器上的 IUnknown 接口的指针。当不再需要 CONNECTDATA 结构时，调用方必须在此指针上调用 IUnknown::Release。</summary>
    [MarshalAs(UnmanagedType.Interface)]
    public object pUnk;
    /// <summary>表示从 <see cref="M:System.Runtime.InteropServices.UCOMIConnectionPoint.Advise(System.Object,System.Int32@)" /> 调用中返回的连接标记。</summary>
    public int dwCookie;
  }
}
