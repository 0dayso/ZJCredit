// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComEventInterfaceAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>标识源接口和实现事件接口（从 COM 类型库导入 coclass 时生成）的方法的类。</summary>
  [AttributeUsage(AttributeTargets.Interface, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class ComEventInterfaceAttribute : Attribute
  {
    internal Type _SourceInterface;
    internal Type _EventProvider;

    /// <summary>从类型库获取原始源接口。</summary>
    /// <returns>一个包含源接口的 <see cref="T:System.Type" />。</returns>
    [__DynamicallyInvokable]
    public Type SourceInterface
    {
      [__DynamicallyInvokable] get
      {
        return this._SourceInterface;
      }
    }

    /// <summary>获取实现事件接口的方法的类。</summary>
    /// <returns>一个 <see cref="T:System.Type" />，它包含实现事件接口的方法的类。</returns>
    [__DynamicallyInvokable]
    public Type EventProvider
    {
      [__DynamicallyInvokable] get
      {
        return this._EventProvider;
      }
    }

    /// <summary>用源接口和事件提供程序类初始化 <see cref="T:System.Runtime.InteropServices.ComEventInterfaceAttribute" /> 类的新实例。</summary>
    /// <param name="SourceInterface">一个 <see cref="T:System.Type" />，它包含类型库中的原始源接口。COM 使用此接口回调到托管类。</param>
    /// <param name="EventProvider">一个 <see cref="T:System.Type" />，它包含实现事件接口的方法的类。</param>
    [__DynamicallyInvokable]
    public ComEventInterfaceAttribute(Type SourceInterface, Type EventProvider)
    {
      this._SourceInterface = SourceInterface;
      this._EventProvider = EventProvider;
    }
  }
}
