// Decompiled with JetBrains decompiler
// Type: System.IServiceProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System
{
  /// <summary>定义一种检索服务对象的机制，服务对象是为其他对象提供自定义支持的对象。</summary>
  /// <filterpriority>2</filterpriority>
  [__DynamicallyInvokable]
  public interface IServiceProvider
  {
    /// <summary>获取指定类型的服务对象。</summary>
    /// <returns>
    /// <paramref name="serviceType" /> 类型的服务对象。- 或 -如果没有 <paramref name="serviceType" /> 类型的服务对象，则为 null。</returns>
    /// <param name="serviceType">一个对象，它指定要获取的服务对象的类型。</param>
    /// <filterpriority>2</filterpriority>
    [__DynamicallyInvokable]
    object GetService(Type serviceType);
  }
}
