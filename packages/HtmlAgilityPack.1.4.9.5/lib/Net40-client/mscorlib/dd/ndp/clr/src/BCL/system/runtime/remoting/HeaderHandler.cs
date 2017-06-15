// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.HeaderHandler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>表示将在反序列化期间处理流上的标头的方法。</summary>
  /// <returns>一个 <see cref="T:System.Object" />，它传达有关远程函数调用的信息。</returns>
  /// <param name="headers">事件的标头。</param>
  [ComVisible(true)]
  public delegate object HeaderHandler(Header[] headers);
}
