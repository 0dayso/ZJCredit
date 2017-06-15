// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.EnvoyInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting
{
  [Serializable]
  internal sealed class EnvoyInfo : IEnvoyInfo
  {
    private IMessageSink envoySinks;

    public IMessageSink EnvoySinks
    {
      [SecurityCritical] get
      {
        return this.envoySinks;
      }
      [SecurityCritical] set
      {
        this.envoySinks = value;
      }
    }

    [SecurityCritical]
    private EnvoyInfo(IMessageSink sinks)
    {
      this.EnvoySinks = sinks;
    }

    [SecurityCritical]
    internal static IEnvoyInfo CreateEnvoyInfo(ServerIdentity serverID)
    {
      IEnvoyInfo envoyInfo = (IEnvoyInfo) null;
      if (serverID != null)
      {
        if (serverID.EnvoyChain == null)
        {
          ServerIdentity serverIdentity = serverID;
          IMessageSink envoyChain = serverIdentity.ServerContext.CreateEnvoyChain(serverID.TPOrObject);
          serverIdentity.RaceSetEnvoyChain(envoyChain);
        }
        if ((IMessageSink) (serverID.EnvoyChain as EnvoyTerminatorSink) == null)
          envoyInfo = (IEnvoyInfo) new EnvoyInfo(serverID.EnvoyChain);
      }
      return envoyInfo;
    }
  }
}
