// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.SurrogateSelector
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization
{
  /// <summary>帮助格式化程序选择要将序列化或反序列化进程委托给的序列化代理项。</summary>
  [ComVisible(true)]
  public class SurrogateSelector : ISurrogateSelector
  {
    internal SurrogateHashtable m_surrogates;
    internal ISurrogateSelector m_nextSelector;

    /// <summary>初始化 <see cref="T:System.Runtime.Serialization.SurrogateSelector" /> 类的新实例。</summary>
    public SurrogateSelector()
    {
      this.m_surrogates = new SurrogateHashtable(32);
    }

    /// <summary>将代理项添加到已检查代理项的列表中。</summary>
    /// <param name="type">需要其代理项的 <see cref="T:System.Type" />。</param>
    /// <param name="context">上下文特定的数据。</param>
    /// <param name="surrogate">要为此类型调用的代理项。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 或 <paramref name="surrogate" /> 参数为 null。</exception>
    /// <exception cref="T:System.ArgumentException">此类型和上下文的代理项已存在。</exception>
    public virtual void AddSurrogate(Type type, StreamingContext context, ISerializationSurrogate surrogate)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      if (surrogate == null)
        throw new ArgumentNullException("surrogate");
      this.m_surrogates.Add((object) new SurrogateKey(type, context), (object) surrogate);
    }

    [SecurityCritical]
    private static bool HasCycle(ISurrogateSelector selector)
    {
      ISurrogateSelector surrogateSelector1 = selector;
      ISurrogateSelector surrogateSelector2 = selector;
      while (surrogateSelector1 != null)
      {
        ISurrogateSelector nextSelector = surrogateSelector1.GetNextSelector();
        if (nextSelector == null)
          return true;
        if (nextSelector == surrogateSelector2)
          return false;
        surrogateSelector1 = nextSelector.GetNextSelector();
        surrogateSelector2 = surrogateSelector2.GetNextSelector();
        if (surrogateSelector1 == surrogateSelector2)
          return false;
      }
      return true;
    }

    /// <summary>在代理项列表中添加指定的 <see cref="T:System.Runtime.Serialization.ISurrogateSelector" />，它可以处理特定的对象类型。</summary>
    /// <param name="selector">要添加的代理项选择器。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="selector" /> 参数为 null。</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">该选择器已经位于选择器列表中。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    [SecurityCritical]
    public virtual void ChainSelector(ISurrogateSelector selector)
    {
      if (selector == null)
        throw new ArgumentNullException("selector");
      if (selector == this)
        throw new SerializationException(Environment.GetResourceString("Serialization_DuplicateSelector"));
      if (!SurrogateSelector.HasCycle(selector))
        throw new ArgumentException(Environment.GetResourceString("Serialization_SurrogateCycleInArgument"), "selector");
      ISurrogateSelector nextSelector = selector.GetNextSelector();
      ISurrogateSelector surrogateSelector1 = selector;
      for (; nextSelector != null && nextSelector != this; nextSelector = nextSelector.GetNextSelector())
        surrogateSelector1 = nextSelector;
      if (nextSelector == this)
        throw new ArgumentException(Environment.GetResourceString("Serialization_SurrogateCycle"), "selector");
      ISurrogateSelector surrogateSelector2 = selector;
      ISurrogateSelector surrogateSelector3 = selector;
      while (surrogateSelector2 != null)
      {
        ISurrogateSelector surrogateSelector4 = surrogateSelector2 != surrogateSelector1 ? surrogateSelector2.GetNextSelector() : this.GetNextSelector();
        if (surrogateSelector4 != null)
        {
          if (surrogateSelector4 == surrogateSelector3)
            throw new ArgumentException(Environment.GetResourceString("Serialization_SurrogateCycle"), "selector");
          surrogateSelector2 = surrogateSelector4 != surrogateSelector1 ? surrogateSelector4.GetNextSelector() : this.GetNextSelector();
          surrogateSelector3 = surrogateSelector3 != surrogateSelector1 ? surrogateSelector3.GetNextSelector() : this.GetNextSelector();
          if (surrogateSelector2 == surrogateSelector3)
            throw new ArgumentException(Environment.GetResourceString("Serialization_SurrogateCycle"), "selector");
        }
        else
          break;
      }
      ISurrogateSelector selector1 = this.m_nextSelector;
      this.m_nextSelector = selector;
      if (selector1 == null)
        return;
      surrogateSelector1.ChainSelector(selector1);
    }

    /// <summary>返回选择器链上的下一个选择器。</summary>
    /// <returns>选择器链上的下一个 <see cref="T:System.Runtime.Serialization.ISurrogateSelector" />。</returns>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    [SecurityCritical]
    public virtual ISurrogateSelector GetNextSelector()
    {
      return this.m_nextSelector;
    }

    /// <summary>返回特定类型的代理项。</summary>
    /// <returns>特定类型的代理项。</returns>
    /// <param name="type">为其请求代理项的 <see cref="T:System.Type" />。</param>
    /// <param name="context">流上下文。</param>
    /// <param name="selector">要使用的代理项。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 参数为 null。</exception>
    /// <exception cref="T:System.Security.SecurityException">调用方没有所要求的权限。</exception>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
    /// </PermissionSet>
    [SecurityCritical]
    public virtual ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      selector = (ISurrogateSelector) this;
      ISerializationSurrogate serializationSurrogate = (ISerializationSurrogate) this.m_surrogates[(object) new SurrogateKey(type, context)];
      if (serializationSurrogate != null)
        return serializationSurrogate;
      if (this.m_nextSelector != null)
        return this.m_nextSelector.GetSurrogate(type, context, out selector);
      return (ISerializationSurrogate) null;
    }

    /// <summary>移除与给定类型关联的代理项。</summary>
    /// <param name="type">要移除其代理项的 <see cref="T:System.Type" />。</param>
    /// <param name="context">当前代理项的 <see cref="T:System.Runtime.Serialization.StreamingContext" />。</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="type" /> 参数为 null。</exception>
    public virtual void RemoveSurrogate(Type type, StreamingContext context)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      this.m_surrogates.Remove((object) new SurrogateKey(type, context));
    }
  }
}
