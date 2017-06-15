// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.ChannelInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting.Channels;
using System.Security;

namespace System.Runtime.Remoting
{
  [Serializable]
  internal sealed class ChannelInfo : IChannelInfo
  {
    private object[] channelData;

    public object[] ChannelData
    {
      [SecurityCritical] get
      {
        return this.channelData;
      }
      [SecurityCritical] set
      {
        this.channelData = value;
      }
    }

    [SecurityCritical]
    internal ChannelInfo()
    {
      this.ChannelData = ChannelServices.CurrentChannelData;
    }
  }
}
