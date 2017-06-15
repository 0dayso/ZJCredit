// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.RuntimeCompatibilityAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>指定是否使用 <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> 对象包装不是从 <see cref="T:System.Exception" /> 类派生的异常。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class RuntimeCompatibilityAttribute : Attribute
  {
    private bool m_wrapNonExceptionThrows;

    /// <summary>获取或设置一个值，该值指示是否使用 <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> 对象包装不是从 <see cref="T:System.Exception" /> 类派生的异常。</summary>
    /// <returns>如果不是从 <see cref="T:System.Exception" /> 类派生的异常应包装在 <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> 对象中，则返回 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    public bool WrapNonExceptionThrows
    {
      [__DynamicallyInvokable] get
      {
        return this.m_wrapNonExceptionThrows;
      }
      [__DynamicallyInvokable] set
      {
        this.m_wrapNonExceptionThrows = value;
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.CompilerServices.RuntimeCompatibilityAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public RuntimeCompatibilityAttribute()
    {
    }
  }
}
