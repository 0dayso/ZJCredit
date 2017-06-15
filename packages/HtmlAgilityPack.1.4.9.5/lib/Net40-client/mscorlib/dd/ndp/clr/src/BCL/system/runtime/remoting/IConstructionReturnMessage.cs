// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Activation.IConstructionReturnMessage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Activation
{
  /// <summary>标识在尝试激活远程对象后返回的 <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" />。</summary>
  [ComVisible(true)]
  public interface IConstructionReturnMessage : IMethodReturnMessage, IMethodMessage, IMessage
  {
  }
}
