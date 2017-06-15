// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.SerializationObjectManager
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Security;

namespace System.Runtime.Serialization
{
  /// <summary>在运行时管理序列化过程。此类不能被继承。</summary>
  public sealed class SerializationObjectManager
  {
    private Hashtable m_objectSeenTable = new Hashtable();
    private SerializationEventHandler m_onSerializedHandler;
    private StreamingContext m_context;

    /// <summary>初始化 <see cref="T:System.Runtime.Serialization.SerializationObjectManager" /> 类的新实例。</summary>
    /// <param name="context">
    /// <see cref="T:System.Runtime.Serialization.StreamingContext" /> 类的实例，包含有关当前序列化操作的信息。</param>
    public SerializationObjectManager(StreamingContext context)
    {
      this.m_context = context;
      this.m_objectSeenTable = new Hashtable();
    }

    /// <summary>注册将引发事件的对象。</summary>
    /// <param name="obj">要注册的对象。</param>
    [SecurityCritical]
    public void RegisterObject(object obj)
    {
      SerializationEvents serializationEventsForType = SerializationEventsCache.GetSerializationEventsForType(obj.GetType());
      if (!serializationEventsForType.HasOnSerializingEvents || this.m_objectSeenTable[obj] != null)
        return;
      this.m_objectSeenTable[obj] = (object) true;
      serializationEventsForType.InvokeOnSerializing(obj, this.m_context);
      this.AddOnSerialized(obj);
    }

    /// <summary>如果该对象的类型有一个 OnSerializing 回调事件，则调用该事件；如果该对象的类型有一个 OnSerialized 事件，则注册该对象以引发该事件。</summary>
    public void RaiseOnSerializedEvent()
    {
      if (this.m_onSerializedHandler == null)
        return;
      this.m_onSerializedHandler(this.m_context);
    }

    [SecuritySafeCritical]
    private void AddOnSerialized(object obj)
    {
      this.m_onSerializedHandler = SerializationEventsCache.GetSerializationEventsForType(obj.GetType()).AddOnSerialized(obj, this.m_onSerializedHandler);
    }
  }
}
