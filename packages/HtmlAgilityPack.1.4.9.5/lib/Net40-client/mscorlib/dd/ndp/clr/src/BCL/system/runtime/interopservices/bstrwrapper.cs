// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.BStrWrapper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
  /// <summary>将 VT_BSTR 类型的数据从托管代码封送到非托管代码。此类不能被继承。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class BStrWrapper
  {
    private string m_WrappedObject;

    /// <summary>获取将作为 VT_BSTR 类型进行封送的包装的 <see cref="T:System.String" /> 对象。</summary>
    /// <returns>由 <see cref="T:System.Runtime.InteropServices.BStrWrapper" /> 包装的对象。</returns>
    [__DynamicallyInvokable]
    public string WrappedObject
    {
      [__DynamicallyInvokable] get
      {
        return this.m_WrappedObject;
      }
    }

    /// <summary>用指定的 <see cref="T:System.String" /> 对象初始化 <see cref="T:System.Runtime.InteropServices.BStrWrapper" /> 类的新实例。</summary>
    /// <param name="value">要包装并作为 VT_BSTR 进行封送的对象。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public BStrWrapper(string value)
    {
      this.m_WrappedObject = value;
    }

    /// <summary>用指定的 <see cref="T:System.Object" /> 对象初始化 <see cref="T:System.Runtime.InteropServices.BStrWrapper" /> 类的新实例。</summary>
    /// <param name="value">要包装并作为 VT_BSTR 进行封送的对象。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public BStrWrapper(object value)
    {
      this.m_WrappedObject = (string) value;
    }
  }
}
