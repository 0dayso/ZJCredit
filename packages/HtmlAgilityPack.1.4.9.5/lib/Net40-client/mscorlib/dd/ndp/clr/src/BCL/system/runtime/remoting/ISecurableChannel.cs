// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.ISecurableChannel
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>
  /// <see cref="T:System.Runtime.Remoting.Channels.ISecurableChannel" /> 包含一个属性 <see cref="P:System.Runtime.Remoting.Channels.ISecurableChannel.IsSecured" />，它获取或设置一个布尔值，该值指示当前信道是否安全。</summary>
  public interface ISecurableChannel
  {
    /// <summary>获取或设置一个布尔值，该值指示当前信道是否安全。</summary>
    /// <returns>指示当前信道是否安全的布尔值。</returns>
    bool IsSecured { [SecurityCritical] get; [SecurityCritical] set; }
  }
}
