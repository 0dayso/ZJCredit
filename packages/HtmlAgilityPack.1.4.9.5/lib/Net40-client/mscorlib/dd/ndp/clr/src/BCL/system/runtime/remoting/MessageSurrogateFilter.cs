// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.MessageSurrogateFilter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>确定在为 <see cref="T:System.MarshalByRefObject" /> 类创建 <see cref="T:System.Runtime.Remoting.ObjRef" /> 时，<see cref="T:System.Runtime.Remoting.Messaging.RemotingSurrogateSelector" /> 类是否忽略某个特定的 <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> 属性。</summary>
  /// <returns>True（如果 <see cref="T:System.Runtime.Remoting.Messaging.RemotingSurrogateSelector" /> 类应该忽略特定 <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> 属性（在创建 <see cref="T:System.Runtime.Remoting.ObjRef" /> 用于 <see cref="T:System.MarshalByRefObject" /> 类时）。</returns>
  /// <param name="key">特定远程处理消息属性的键。</param>
  /// <param name="value">特定远程处理消息属性的值。</param>
  [ComVisible(true)]
  public delegate bool MessageSurrogateFilter(string key, object value);
}
