// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.IDynamicMessageSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
  /// <summary>指示实现的消息接收器将由动态注册的属性提供。</summary>
  [ComVisible(true)]
  public interface IDynamicMessageSink
  {
    /// <summary>指示调用正在启动。</summary>
    /// <param name="reqMsg">请求消息。</param>
    /// <param name="bCliSide">如果在客户端调用该方法，则为 true 值；如果在服务器端调用，则为 false。</param>
    /// <param name="bAsync">如果此调用为异步调用，则为 true 值，如果为同步调用，则为 false。</param>
    [SecurityCritical]
    void ProcessMessageStart(IMessage reqMsg, bool bCliSide, bool bAsync);

    /// <summary>指示调用正在返回。</summary>
    /// <param name="replyMsg">应答消息。</param>
    /// <param name="bCliSide">如果在客户端调用该方法，则为 true 值；如果在服务器端调用，则为 false。</param>
    /// <param name="bAsync">如果此调用为异步调用，则为 true 值，如果为同步调用，则为 false。</param>
    [SecurityCritical]
    void ProcessMessageFinish(IMessage replyMsg, bool bCliSide, bool bAsync);
  }
}
