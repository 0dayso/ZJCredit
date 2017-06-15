// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.IContributeObjectSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
  /// <summary>在远程处理调用的服务器端分配对象特定的侦听接收器。</summary>
  [ComVisible(true)]
  public interface IContributeObjectSink
  {
    /// <summary>将所提供的服务器对象的消息接收器连接到给定的接收器链前面。</summary>
    /// <returns>复合接收器链。</returns>
    /// <param name="obj">提供要连接到给定的接收器链前面的消息接收器的服务器对象。</param>
    /// <param name="nextSink">到目前为止组成的接收链。</param>
    [SecurityCritical]
    IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink);
  }
}
