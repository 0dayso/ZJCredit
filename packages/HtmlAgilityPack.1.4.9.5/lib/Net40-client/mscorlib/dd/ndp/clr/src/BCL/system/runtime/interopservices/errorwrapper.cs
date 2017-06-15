// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ErrorWrapper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
  /// <summary>对封送拆收器应该将其作为 VT_ERROR 封送的对象进行包装。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class ErrorWrapper
  {
    private int m_ErrorCode;

    /// <summary>获取包装的错误代码。</summary>
    /// <returns>错误的 HRESULT。</returns>
    [__DynamicallyInvokable]
    public int ErrorCode
    {
      [__DynamicallyInvokable] get
      {
        return this.m_ErrorCode;
      }
    }

    /// <summary>使用错误的 HRESULT 初始化 <see cref="T:System.Runtime.InteropServices.ErrorWrapper" /> 类的新实例。</summary>
    /// <param name="errorCode">错误的 HRESULT。</param>
    [__DynamicallyInvokable]
    public ErrorWrapper(int errorCode)
    {
      this.m_ErrorCode = errorCode;
    }

    /// <summary>用包含错误的 HRESULT 的对象初始化 <see cref="T:System.Runtime.InteropServices.ErrorWrapper" /> 类的新实例。</summary>
    /// <param name="errorCode">包含错误的 HRESULT 的对象。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="errorCode" /> 参数不是 <see cref="T:System.Int32" /> 类型。</exception>
    [__DynamicallyInvokable]
    public ErrorWrapper(object errorCode)
    {
      if (!(errorCode is int))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeInt32"), "errorCode");
      this.m_ErrorCode = (int) errorCode;
    }

    /// <summary>使用与所提供的异常相对应的 HRESULT 初始化 <see cref="T:System.Runtime.InteropServices.ErrorWrapper" /> 类的新实例。</summary>
    /// <param name="e">要转换为错误代码的异常。</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public ErrorWrapper(Exception e)
    {
      this.m_ErrorCode = Marshal.GetHRForException(e);
    }
  }
}
