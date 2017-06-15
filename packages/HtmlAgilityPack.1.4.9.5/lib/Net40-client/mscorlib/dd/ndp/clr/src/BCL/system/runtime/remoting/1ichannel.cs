// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.TransportHeaders
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>存储在信道接收器中使用的标头的集合。</summary>
  [SecurityCritical]
  [ComVisible(true)]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class TransportHeaders : ITransportHeaders
  {
    private ArrayList _headerList;

    /// <summary>获取或设置与给定键关联的传输标头。</summary>
    /// <returns>与给定键关联的传输标头，或者如果未找到该键，则为 null。</returns>
    /// <param name="key">与请求的标头关联的 <see cref="T:System.String" />。</param>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    public object this[object key]
    {
      [SecurityCritical] get
      {
        string strB = (string) key;
        foreach (DictionaryEntry header in this._headerList)
        {
          if (string.Compare((string) header.Key, strB, StringComparison.OrdinalIgnoreCase) == 0)
            return header.Value;
        }
        return (object) null;
      }
      [SecurityCritical] set
      {
        if (key == null)
          return;
        string strB = (string) key;
        for (int index = this._headerList.Count - 1; index >= 0; --index)
        {
          if (string.Compare((string) ((DictionaryEntry) this._headerList[index]).Key, strB, StringComparison.OrdinalIgnoreCase) == 0)
          {
            this._headerList.RemoveAt(index);
            break;
          }
        }
        if (value == null)
          return;
        this._headerList.Add((object) new DictionaryEntry(key, value));
      }
    }

    /// <summary>初始化 <see cref="T:System.Runtime.Remoting.Channels.TransportHeaders" /> 类的新实例。</summary>
    public TransportHeaders()
    {
      this._headerList = new ArrayList(6);
    }

    /// <summary>返回存储传输标头的枚举数。</summary>
    /// <returns>存储传输标头的枚举数。</returns>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
    /// </PermissionSet>
    [SecurityCritical]
    public IEnumerator GetEnumerator()
    {
      return this._headerList.GetEnumerator();
    }
  }
}
