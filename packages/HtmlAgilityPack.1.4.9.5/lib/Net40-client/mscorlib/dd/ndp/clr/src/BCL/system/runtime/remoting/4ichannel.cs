// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.BaseChannelWithProperties
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>提供希望向其属性公开字典接口的信道的基实现。</summary>
  [SecurityCritical]
  [ComVisible(true)]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public abstract class BaseChannelWithProperties : BaseChannelObjectWithProperties
  {
    /// <summary>指示信道接收器堆栈中最上面的信道接收器。</summary>
    protected IChannelSinkBase SinksWithProperties;

    /// <summary>获取与当前信道对象关联的信道属性的 <see cref="T:System.Collections.IDictionary" />。</summary>
    /// <returns>与当前信道对象关联的信道属性的 <see cref="T:System.Collections.IDictionary" />。</returns>
    /// <exception cref="T:System.Security.SecurityException">直接调用方没有基础结构权限。</exception>
    public override IDictionary Properties
    {
      [SecurityCritical] get
      {
        ArrayList arrayList = new ArrayList();
        arrayList.Add((object) this);
        if (this.SinksWithProperties != null)
        {
          IServerChannelSink serverChannelSink = this.SinksWithProperties as IServerChannelSink;
          if (serverChannelSink != null)
          {
            for (; serverChannelSink != null; serverChannelSink = serverChannelSink.NextChannelSink)
            {
              IDictionary properties = serverChannelSink.Properties;
              if (properties != null)
                arrayList.Add((object) properties);
            }
          }
          else
          {
            for (IClientChannelSink clientChannelSink = (IClientChannelSink) this.SinksWithProperties; clientChannelSink != null; clientChannelSink = clientChannelSink.NextChannelSink)
            {
              IDictionary properties = clientChannelSink.Properties;
              if (properties != null)
                arrayList.Add((object) properties);
            }
          }
        }
        return (IDictionary) new AggregateDictionary((ICollection) arrayList);
      }
    }
  }
}
