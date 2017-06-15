// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.RegisteredChannelList
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.Remoting.Channels
{
  internal class RegisteredChannelList
  {
    private RegisteredChannel[] _channels;

    internal RegisteredChannel[] RegisteredChannels
    {
      get
      {
        return this._channels;
      }
    }

    internal int Count
    {
      get
      {
        if (this._channels == null)
          return 0;
        return this._channels.Length;
      }
    }

    internal int ReceiverCount
    {
      get
      {
        if (this._channels == null)
          return 0;
        int num = 0;
        for (int index = 0; index < this._channels.Length; ++index)
        {
          if (this.IsReceiver(index))
            ++num;
        }
        return num;
      }
    }

    internal RegisteredChannelList()
    {
      this._channels = new RegisteredChannel[0];
    }

    internal RegisteredChannelList(RegisteredChannel[] channels)
    {
      this._channels = channels;
    }

    internal IChannel GetChannel(int index)
    {
      return this._channels[index].Channel;
    }

    internal bool IsSender(int index)
    {
      return this._channels[index].IsSender();
    }

    internal bool IsReceiver(int index)
    {
      return this._channels[index].IsReceiver();
    }

    internal int FindChannelIndex(IChannel channel)
    {
      object obj = (object) channel;
      for (int index = 0; index < this._channels.Length; ++index)
      {
        if (obj == this.GetChannel(index))
          return index;
      }
      return -1;
    }

    [SecurityCritical]
    internal int FindChannelIndex(string name)
    {
      for (int index = 0; index < this._channels.Length; ++index)
      {
        if (string.Compare(name, this.GetChannel(index).ChannelName, StringComparison.OrdinalIgnoreCase) == 0)
          return index;
      }
      return -1;
    }
  }
}
