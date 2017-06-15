// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>控制作为非托管函数指针传入或传出非托管代码的委托签名的封送行为。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class UnmanagedFunctionPointerAttribute : Attribute
  {
    private CallingConvention m_callingConvention;
    /// <summary>指示如何向方法封送字符串参数，并控制名称重整。</summary>
    [__DynamicallyInvokable]
    public CharSet CharSet;
    /// <summary>将 Unicode 字符转换为 ANSI 字符时，启用或禁用最佳映射行为。</summary>
    [__DynamicallyInvokable]
    public bool BestFitMapping;
    /// <summary>启用或禁用在遇到已被转换为 ANSI“?”字符的无法映射的 Unicode 字符时引发异常。</summary>
    [__DynamicallyInvokable]
    public bool ThrowOnUnmappableChar;
    /// <summary>指示被调用方在从属性化方法返回之前是否调用 SetLastError Win32 API 函数。</summary>
    [__DynamicallyInvokable]
    public bool SetLastError;

    /// <summary>获取调用约定的值。</summary>
    /// <returns>
    /// <see cref="M:System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute.#ctor(System.Runtime.InteropServices.CallingConvention)" /> 构造函数指定的调用约定的值。</returns>
    [__DynamicallyInvokable]
    public CallingConvention CallingConvention
    {
      [__DynamicallyInvokable] get
      {
        return this.m_callingConvention;
      }
    }

    /// <summary>使用指定的调用约定初始化 <see cref="T:System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute" /> 类的新实例。</summary>
    /// <param name="callingConvention">指定的调用约定。</param>
    [__DynamicallyInvokable]
    public UnmanagedFunctionPointerAttribute(CallingConvention callingConvention)
    {
      this.m_callingConvention = callingConvention;
    }
  }
}
