// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.IContextPropertyActivator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
  /// <summary>指示实现的属性要参与激活而且可能未提供消息接收器。</summary>
  [ComVisible(true)]
  public interface IContextPropertyActivator
  {
    /// <summary>指示是否可以激活 <paramref name="msg" /> 参数中指示的对象类型。</summary>
    /// <returns>指示是否可以激活所请求类型的布尔值。</returns>
    /// <param name="msg">一个 <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />。</param>
    [SecurityCritical]
    bool IsOKToActivate(IConstructionCallMessage msg);

    /// <summary>在构造请求离开客户端前，调协用每个具有此接口的客户端上下文属性。</summary>
    /// <param name="msg">一个 <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />。</param>
    [SecurityCritical]
    void CollectFromClientContext(IConstructionCallMessage msg);

    /// <summary>当构造请求从服务器返回到客户端时，调用每个具有此接口的客户端上下文属性。</summary>
    /// <returns>如果成功，则为 true；否则为 false。</returns>
    /// <param name="msg">一个 <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />。</param>
    [SecurityCritical]
    bool DeliverClientContextToServerContext(IConstructionCallMessage msg);

    /// <summary>在构造响应离开服务器返回客户端前，调用每个具有此接口的服务器上下文属性。</summary>
    /// <param name="msg">一个 <see cref="T:System.Runtime.Remoting.Activation.IConstructionReturnMessage" />。</param>
    [SecurityCritical]
    void CollectFromServerContext(IConstructionReturnMessage msg);

    /// <summary>当构造请求从服务器返回到客户端时，调用每个具有此接口的客户端上下文属性。</summary>
    /// <returns>如果成功，则为 true；否则为 false。</returns>
    /// <param name="msg">一个 <see cref="T:System.Runtime.Remoting.Activation.IConstructionReturnMessage" />。</param>
    [SecurityCritical]
    bool DeliverServerContextToClientContext(IConstructionReturnMessage msg);
  }
}
