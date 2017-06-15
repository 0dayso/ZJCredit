// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.VariantWrapper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>将 VT_VARIANT | VT_BYREF 类型的数据从托管代码封送到非托管代码。此类不能被继承。</summary>
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class VariantWrapper
  {
    private object m_WrappedObject;

    /// <summary>获取由 <see cref="T:System.Runtime.InteropServices.VariantWrapper" /> 对象包装的对象。</summary>
    /// <returns>由 <see cref="T:System.Runtime.InteropServices.VariantWrapper" /> 对象包装的对象。</returns>
    [__DynamicallyInvokable]
    public object WrappedObject
    {
      [__DynamicallyInvokable] get
      {
        return this.m_WrappedObject;
      }
    }

    /// <summary>使用指定的 <see cref="T:System.Object" /> 参数初始化 <see cref="T:System.Runtime.InteropServices.VariantWrapper" /> 类的新实例。</summary>
    /// <param name="obj">要封送的对象。</param>
    [__DynamicallyInvokable]
    public VariantWrapper(object obj)
    {
      this.m_WrappedObject = obj;
    }
  }
}
