// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.ChannelDataStore
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>存储远程处理信道的信道数据。</summary>
  [SecurityCritical]
  [ComVisible(true)]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class ChannelDataStore : IChannelDataStore
  {
    private string[] _channelURIs;
    private DictionaryEntry[] _extraData;

    /// <summary>获取或设置当前信道所映射到的信道 URI 的数组。</summary>
    /// <returns>当前信道所映射到的信道 URI 的数组。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public string[] ChannelUris
    {
      [SecurityCritical] get
      {
        return this._channelURIs;
      }
      set
      {
        this._channelURIs = value;
      }
    }

    /// <summary>获取或设置与实现信道的指定键关联的数据对象。</summary>
    /// <returns>实现信道的指定数据对象。</returns>
    /// <param name="key">数据对象所关联的键。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public object this[object key]
    {
      [SecurityCritical] get
      {
        foreach (DictionaryEntry dictionaryEntry in this._extraData)
        {
          if (dictionaryEntry.Key.Equals(key))
            return dictionaryEntry.Value;
        }
        return (object) null;
      }
      [SecurityCritical] set
      {
        if (this._extraData == null)
        {
          this._extraData = new DictionaryEntry[1];
          this._extraData[0] = new DictionaryEntry(key, value);
        }
        else
        {
          int length = this._extraData.Length;
          DictionaryEntry[] dictionaryEntryArray = new DictionaryEntry[length + 1];
          int index;
          for (index = 0; index < length; ++index)
            dictionaryEntryArray[index] = this._extraData[index];
          dictionaryEntryArray[index] = new DictionaryEntry(key, value);
          this._extraData = dictionaryEntryArray;
        }
      }
    }

    private ChannelDataStore(string[] channelUrls, DictionaryEntry[] extraData)
    {
      this._channelURIs = channelUrls;
      this._extraData = extraData;
    }

    /// <summary>用当前信道所映射到的 URI 初始化 <see cref="T:System.Runtime.Remoting.Channels.ChannelDataStore" /> 类的新实例。</summary>
    /// <param name="channelURIs">当前信道所映射到的信道 URI 的数组。</param>
    public ChannelDataStore(string[] channelURIs)
    {
      this._channelURIs = channelURIs;
      this._extraData = (DictionaryEntry[]) null;
    }

    [SecurityCritical]
    internal ChannelDataStore InternalShallowCopy()
    {
      return new ChannelDataStore(this._channelURIs, this._extraData);
    }
  }
}
