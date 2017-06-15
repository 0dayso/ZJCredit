// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.IRemotingFormatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>提供所有格式化程序的远程过程调用 (RPC) 接口。</summary>
  [ComVisible(true)]
  public interface IRemotingFormatter : IFormatter
  {
    /// <summary>开始远程过程调用 (RPC) 的反序列化进程。</summary>
    /// <returns>反序列化的对象图的根。</returns>
    /// <param name="serializationStream">要从中反序列化数据的 <see cref="T:System.IO.Stream" />。</param>
    /// <param name="handler">设计为处理 <see cref="T:System.Runtime.Remoting.Messaging.Header" /> 对象的委托。可以为 null。</param>
    object Deserialize(Stream serializationStream, HeaderHandler handler);

    /// <summary>开始远程过程调用 (RPC) 的序列化进程。</summary>
    /// <param name="serializationStream">向其上序列化指定的图的 <see cref="T:System.IO.Stream" />。</param>
    /// <param name="graph">要序列化的对象图的根。</param>
    /// <param name="headers">要与 <paramref name="graph" /> 参数所指定的图一起传输的 <see cref="T:System.Runtime.Remoting.Messaging.Header" /> 对象数组。可以为 null。</param>
    void Serialize(Stream serializationStream, object graph, Header[] headers);
  }
}
