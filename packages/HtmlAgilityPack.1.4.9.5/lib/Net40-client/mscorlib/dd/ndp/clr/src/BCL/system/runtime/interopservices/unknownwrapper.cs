// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UnknownWrapper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>对封送拆收器应该将其作为 VT_UNKNOWN 封送的对象进行包装。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class UnknownWrapper
  {
    private object m_WrappedObject;

    /// <summary>获取此包装包含的对象。</summary>
    /// <returns>被包装的对象。</returns>
    [__DynamicallyInvokable]
    public object WrappedObject
    {
      [__DynamicallyInvokable] get
      {
        return this.m_WrappedObject;
      }
    }

    /// <summary>使用要被包装的对象初始化 <see cref="T:System.Runtime.InteropServices.UnknownWrapper" /> 类的新实例。</summary>
    /// <param name="obj">被包装的对象。</param>
    [__DynamicallyInvokable]
    public UnknownWrapper(object obj)
    {
      this.m_WrappedObject = obj;
    }
  }
}
