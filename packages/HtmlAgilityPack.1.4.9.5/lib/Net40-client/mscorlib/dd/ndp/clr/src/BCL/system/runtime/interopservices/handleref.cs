// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.HandleRef
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>包装一个托管对象，该对象保存使用平台 invoke（调用）传递给非托管代码的资源句柄。</summary>
  [ComVisible(true)]
  public struct HandleRef
  {
    internal object m_wrapper;
    internal IntPtr m_handle;

    /// <summary>获取保存资源句柄的对象。</summary>
    /// <returns>保存资源句柄的对象。</returns>
    public object Wrapper
    {
      get
      {
        return this.m_wrapper;
      }
    }

    /// <summary>获取资源的句柄。</summary>
    /// <returns>资源的句柄。</returns>
    public IntPtr Handle
    {
      get
      {
        return this.m_handle;
      }
    }

    /// <summary>用要包装的对象和由非托管代码使用的资源的句柄初始化 <see cref="T:System.Runtime.InteropServices.HandleRef" /> 类的新实例。</summary>
    /// <param name="wrapper">在平台 invoke 调用返回前不应完成的托管对象。</param>
    /// <param name="handle">一个 <see cref="T:System.IntPtr" />，它指示资源的句柄。</param>
    public HandleRef(object wrapper, IntPtr handle)
    {
      this.m_wrapper = wrapper;
      this.m_handle = handle;
    }

    /// <summary>返回指定的 <see cref="T:System.Runtime.InteropServices.HandleRef" /> 对象的资源的句柄。</summary>
    /// <returns>指定的 <see cref="T:System.Runtime.InteropServices.HandleRef" /> 对象的资源的句柄。</returns>
    /// <param name="value">需要句柄的对象。</param>
    public static explicit operator IntPtr(HandleRef value)
    {
      return value.m_handle;
    }

    /// <summary>返回 <see cref="T:System.Runtime.InteropServices.HandleRef" /> 对象的内部整数表示形式。</summary>
    /// <returns>表示 <see cref="T:System.Runtime.InteropServices.HandleRef" /> 对象的 <see cref="T:System.IntPtr" /> 对象。</returns>
    /// <param name="value">要从其中检索内部整数表示形式的 <see cref="T:System.Runtime.InteropServices.HandleRef" /> 对象。</param>
    public static IntPtr ToIntPtr(HandleRef value)
    {
      return value.m_handle;
    }
  }
}
