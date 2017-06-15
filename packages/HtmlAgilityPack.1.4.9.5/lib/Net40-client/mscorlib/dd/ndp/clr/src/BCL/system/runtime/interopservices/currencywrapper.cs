// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.CurrencyWrapper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>对封送拆收器应该将其作为 VT_CY 封送的对象进行包装。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class CurrencyWrapper
  {
    private Decimal m_WrappedObject;

    /// <summary>获取将作为 VT_CY 类型进行封送的包装对象。</summary>
    /// <returns>将作为 VT_CY 类型进行封送的包装对象。</returns>
    [__DynamicallyInvokable]
    public Decimal WrappedObject
    {
      [__DynamicallyInvokable] get
      {
        return this.m_WrappedObject;
      }
    }

    /// <summary>用要包装并作为 VT_CY 类型进行封送的 Decimal 来初始化 <see cref="T:System.Runtime.InteropServices.CurrencyWrapper" /> 类的新实例。</summary>
    /// <param name="obj">要包装并作为 VT_CY 进行封送的 Decimal。</param>
    [__DynamicallyInvokable]
    public CurrencyWrapper(Decimal obj)
    {
      this.m_WrappedObject = obj;
    }

    /// <summary>用包含要包装并作为 VT_CY 类型进行封送的 Decimal 的对象来初始化 <see cref="T:System.Runtime.InteropServices.CurrencyWrapper" /> 类的新实例。</summary>
    /// <param name="obj">包含要包装并作为 VT_CY 进行封送的 Decimal 的对象。</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="obj" /> 参数不是 <see cref="T:System.Decimal" /> 类型。</exception>
    [__DynamicallyInvokable]
    public CurrencyWrapper(object obj)
    {
      if (!(obj is Decimal))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDecimal"), "obj");
      this.m_WrappedObject = (Decimal) obj;
    }
  }
}
