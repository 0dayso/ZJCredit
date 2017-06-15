// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ICustomQueryInterface
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>允许开发人员提供 IUnknown::QueryInterface(REFIID riid, void **ppvObject) 方法的自定义托管实现。</summary>
  [ComVisible(false)]
  [__DynamicallyInvokable]
  public interface ICustomQueryInterface
  {
    /// <summary>根据指定的接口 ID 返回接口。</summary>
    /// <returns>枚举值之一，指示是否使用了 IUnknown::QueryInterface 的自定义实现。</returns>
    /// <param name="iid">请求的接口的 GUID。</param>
    /// <param name="ppv">此方法返回时，对所请求的接口的引用。</param>
    [SecurityCritical]
    CustomQueryInterfaceResult GetInterface([In] ref Guid iid, out IntPtr ppv);
  }
}
