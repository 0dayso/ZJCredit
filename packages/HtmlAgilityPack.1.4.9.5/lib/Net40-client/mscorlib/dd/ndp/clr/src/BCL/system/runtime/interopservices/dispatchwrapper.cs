// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.DispatchWrapper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
  /// <summary>对封送拆收器应该将其作为 VT_DISPATCH 封送的对象进行包装。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class DispatchWrapper
  {
    private object m_WrappedObject;

    /// <summary>获取由 <see cref="T:System.Runtime.InteropServices.DispatchWrapper" /> 包装的对象。</summary>
    /// <returns>由 <see cref="T:System.Runtime.InteropServices.DispatchWrapper" /> 包装的对象。</returns>
    [__DynamicallyInvokable]
    public object WrappedObject
    {
      [__DynamicallyInvokable] get
      {
        return this.m_WrappedObject;
      }
    }

    /// <summary>使用正在包装的对象初始化 <see cref="T:System.Runtime.InteropServices.DispatchWrapper" /> 类的新实例。</summary>
    /// <param name="obj">要包装并转换成 <see cref="F:System.Runtime.InteropServices.VarEnum.VT_DISPATCH" /> 的对象。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="obj" /> 不是类或数组。- 或 -<paramref name="obj" /> 不支持 IDispatch。</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// <paramref name="obj" /> 参数是用被传递了一个 false 值的 <see cref="T:System.Runtime.InteropServices.ComVisibleAttribute" /> 特性标记的。- 或 -<paramref name="obj" /> 参数继承自一个类型，该类型是用一个被传递了 false 值的 <see cref="T:System.Runtime.InteropServices.ComVisibleAttribute" /> 特性标记的。</exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public DispatchWrapper(object obj)
    {
      if (obj != null)
        Marshal.Release(Marshal.GetIDispatchForObject(obj));
      this.m_WrappedObject = obj;
    }
  }
}
