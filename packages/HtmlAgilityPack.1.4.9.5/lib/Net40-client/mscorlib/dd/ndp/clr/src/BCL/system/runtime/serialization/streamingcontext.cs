// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.StreamingContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>描述给定的序列化流的源和目标，并提供一个由调用方定义的附加上下文。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct StreamingContext
  {
    internal object m_additionalContext;
    internal StreamingContextStates m_state;

    /// <summary>获取指定为附加上下文一部分的上下文。</summary>
    /// <returns>指定为附加上下文一部分的上下文。</returns>
    public object Context
    {
      get
      {
        return this.m_additionalContext;
      }
    }

    /// <summary>获取传输数据的源或目标。</summary>
    /// <returns>在序列化过程中为传输数据的目标。在反序列化过程中为数据的源。</returns>
    public StreamingContextStates State
    {
      get
      {
        return this.m_state;
      }
    }

    /// <summary>使用给定的上下文状态初始化 <see cref="T:System.Runtime.Serialization.StreamingContext" /> 类的新实例。</summary>
    /// <param name="state">指定此 <see cref="T:System.Runtime.Serialization.StreamingContext" /> 的源或目标上下文的 <see cref="T:System.Runtime.Serialization.StreamingContextStates" /> 值的按位组合。</param>
    public StreamingContext(StreamingContextStates state)
    {
      this = new StreamingContext(state, (object) null);
    }

    /// <summary>使用给定的上下文状态以及一些附加信息来初始化 <see cref="T:System.Runtime.Serialization.StreamingContext" /> 类的新实例。</summary>
    /// <param name="state">指定此 <see cref="T:System.Runtime.Serialization.StreamingContext" /> 的源或目标上下文的 <see cref="T:System.Runtime.Serialization.StreamingContextStates" /> 值的按位组合。</param>
    /// <param name="additional">任何与 <see cref="T:System.Runtime.Serialization.StreamingContext" /> 关联的附加信息。此信息对所有实现 <see cref="T:System.Runtime.Serialization.ISerializable" /> 或任何序列化代理项的对象均可用。大多数用户无需设置此参数。</param>
    public StreamingContext(StreamingContextStates state, object additional)
    {
      this.m_state = state;
      this.m_additionalContext = additional;
    }

    /// <summary>确定两个 <see cref="T:System.Runtime.Serialization.StreamingContext" /> 实例是否包含相同的值。</summary>
    /// <returns>如果指定对象是 <see cref="T:System.Runtime.Serialization.StreamingContext" /> 的实例且等于当前实例的值，则为 true；否则为 false。</returns>
    /// <param name="obj">与当前实例进行比较的对象。</param>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      return obj is StreamingContext && ((StreamingContext) obj).m_additionalContext == this.m_additionalContext && ((StreamingContext) obj).m_state == this.m_state;
    }

    /// <summary>返回该对象的哈希代码。</summary>
    /// <returns>
    /// <see cref="T:System.Runtime.Serialization.StreamingContextStates" /> 值，它包含为此 <see cref="T:System.Runtime.Serialization.StreamingContext" /> 进行序列化的源或目标。</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return (int) this.m_state;
    }
  }
}
