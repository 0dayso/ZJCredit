// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.ConstructionResponse
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>实现 <see cref="T:System.Runtime.Remoting.Activation.IConstructionReturnMessage" /> 接口以创建一个消息，该消息对实例化远程对象的调用做出响应。</summary>
  [SecurityCritical]
  [CLSCompliant(false)]
  [ComVisible(true)]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class ConstructionResponse : MethodResponse, IConstructionReturnMessage, IMethodReturnMessage, IMethodMessage, IMessage
  {
    /// <summary>获取表示远程处理消息属性集合的 <see cref="T:System.Collections.IDictionary" /> 接口。</summary>
    /// <returns>表示远程处理消息属性集合的 <see cref="T:System.Collections.IDictionary" /> 接口。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public override IDictionary Properties
    {
      [SecurityCritical] get
      {
        lock (this)
        {
          if (this.InternalProperties == null)
            this.InternalProperties = (IDictionary) new Hashtable();
          if (this.ExternalProperties == null)
            this.ExternalProperties = (IDictionary) new CRMDictionary((IConstructionReturnMessage) this, this.InternalProperties);
          return this.ExternalProperties;
        }
      }
    }

    /// <summary>从一个远程处理标头数组和一个请求消息初始化 <see cref="T:System.Runtime.Remoting.Messaging.ConstructionResponse" /> 类的一个新实例。</summary>
    /// <param name="h">包含键/值对的远程处理标头数组。该数组用于初始化属于“http://schemas.microsoft.com/clr/soap/messageProperties”命名空间的标头的 <see cref="T:System.Runtime.Remoting.Messaging.ConstructionResponse" /> 字段。</param>
    /// <param name="mcm">一个构成远程对象上的构造函数调用的请求消息。</param>
    public ConstructionResponse(Header[] h, IMethodCallMessage mcm)
      : base(h, mcm)
    {
    }

    internal ConstructionResponse(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
