// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.DispatchChannelSinkProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.Remoting.Channels
{
  internal class DispatchChannelSinkProvider : IServerChannelSinkProvider
  {
    public IServerChannelSinkProvider Next
    {
      [SecurityCritical] get
      {
        return (IServerChannelSinkProvider) null;
      }
      [SecurityCritical] set
      {
        throw new NotSupportedException();
      }
    }

    internal DispatchChannelSinkProvider()
    {
    }

    [SecurityCritical]
    public void GetChannelData(IChannelDataStore channelData)
    {
    }

    [SecurityCritical]
    public IServerChannelSink CreateSink(IChannelReceiver channel)
    {
      return (IServerChannelSink) new DispatchChannelSink();
    }
  }
}
