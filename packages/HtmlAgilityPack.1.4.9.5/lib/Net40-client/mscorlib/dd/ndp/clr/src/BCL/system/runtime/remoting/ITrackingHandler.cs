// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Services.ITrackingHandler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Services
{
  /// <summary>指示实现对象必须得到由远程结构发出的有关对象与代理的封送处理、取消封送处理和断开的通知。</summary>
  [ComVisible(true)]
  public interface ITrackingHandler
  {
    /// <summary>通知当前实例已封送对象。</summary>
    /// <param name="obj">已封送的对象。</param>
    /// <param name="or">
    /// <see cref="T:System.Runtime.Remoting.ObjRef" />，它由封送处理产生，并表示指定的对象。</param>
    [SecurityCritical]
    void MarshaledObject(object obj, ObjRef or);

    /// <summary>通知当前实例已将对象取消封送。</summary>
    /// <param name="obj">已取消封送的对象。</param>
    /// <param name="or">表示指定对象的 <see cref="T:System.Runtime.Remoting.ObjRef" />。</param>
    [SecurityCritical]
    void UnmarshaledObject(object obj, ObjRef or);

    /// <summary>通知当前实例某个对象已与其代理断开连接。</summary>
    /// <param name="obj">已断开连接的对象。</param>
    [SecurityCritical]
    void DisconnectedObject(object obj);
  }
}
