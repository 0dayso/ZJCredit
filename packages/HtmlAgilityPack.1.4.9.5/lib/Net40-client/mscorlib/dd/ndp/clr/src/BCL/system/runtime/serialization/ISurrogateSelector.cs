// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.ISurrogateSelector
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization
{
  /// <summary>指示序列化代理项选择器类。</summary>
  [ComVisible(true)]
  public interface ISurrogateSelector
  {
    /// <summary>指定代理项的下一个 <see cref="T:System.Runtime.Serialization.ISurrogateSelector" />，以检查当前实例在指定上下文中是否不具有指定类型和程序集的代理项。</summary>
    /// <param name="selector">下一个要检查的代理项选择器。</param>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    [SecurityCritical]
    void ChainSelector(ISurrogateSelector selector);

    /// <summary>从指定序列化上下文的指定代理项选择器开始，查找表示指定对象类型的代理项。</summary>
    /// <returns>给定上下文中给定类型的适当代理项。</returns>
    /// <param name="type">需要代理项的对象（类）的 <see cref="T:System.Type" />。</param>
    /// <param name="context">当前序列化的源或目标上下文。</param>
    /// <param name="selector">当此方法返回时，将包含一个 <see cref="T:System.Runtime.Serialization.ISurrogateSelector" />，它持有一个指向从中找到合适代理项的代理项选择器引用。该参数未经初始化即被传递。</param>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    [SecurityCritical]
    ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector);

    /// <summary>返回链中的下一个代理项选择器。</summary>
    /// <returns>链中的下一个代理项选择器，或者为 null。</returns>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    [SecurityCritical]
    ISurrogateSelector GetNextSelector();
  }
}
