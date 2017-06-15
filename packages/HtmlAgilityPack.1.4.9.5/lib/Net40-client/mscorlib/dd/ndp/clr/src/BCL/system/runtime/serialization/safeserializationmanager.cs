// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.SafeSerializationEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;

namespace System.Runtime.Serialization
{
  /// <summary>为 <see cref="T:System.Exception.SerializeObjectState" /> 事件提供数据。</summary>
  public sealed class SafeSerializationEventArgs : EventArgs
  {
    private List<object> m_serializedStates = new List<object>();
    private StreamingContext m_streamingContext;

    internal IList<object> SerializedStates
    {
      get
      {
        return (IList<object>) this.m_serializedStates;
      }
    }

    /// <summary>获取或设置一个对象，该对象描述序列化流的源和目标。</summary>
    /// <returns>一个对象，该对象描述序列化流的源和目标。</returns>
    public StreamingContext StreamingContext
    {
      get
      {
        return this.m_streamingContext;
      }
    }

    internal SafeSerializationEventArgs(StreamingContext streamingContext)
    {
      this.m_streamingContext = streamingContext;
    }

    /// <summary>存储异常的状态。</summary>
    /// <param name="serializedState">与此实例序列化的状态对象。</param>
    public void AddSerializedState(ISafeSerializationData serializedState)
    {
      if (serializedState == null)
        throw new ArgumentNullException("serializedState");
      if (!serializedState.GetType().IsSerializable)
        throw new ArgumentException(Environment.GetResourceString("Serialization_NonSerType", (object) serializedState.GetType(), (object) serializedState.GetType().Assembly.FullName));
      this.m_serializedStates.Add((object) serializedState);
    }
  }
}
