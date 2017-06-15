// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.IContributeEnvoySink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
  /// <summary>在客户端分配特使消息接收器。</summary>
  [ComVisible(true)]
  public interface IContributeEnvoySink
  {
    /// <summary>将第一个接收器放入到目前为止组成的接收器链中，然后将其消息接收器连接到已经形成的链前面。</summary>
    /// <returns>复合接收器链。</returns>
    /// <param name="obj">正在为其创建链的服务器对象。</param>
    /// <param name="nextSink">到目前为止组成的接收链。</param>
    [SecurityCritical]
    IMessageSink GetEnvoySink(MarshalByRefObject obj, IMessageSink nextSink);
  }
}
