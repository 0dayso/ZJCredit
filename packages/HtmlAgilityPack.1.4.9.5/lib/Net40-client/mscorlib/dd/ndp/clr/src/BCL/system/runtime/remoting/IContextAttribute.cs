// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.IContextAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
  /// <summary>标识上下文特性。</summary>
  [ComVisible(true)]
  public interface IContextAttribute
  {
    /// <summary>返回一个布尔值，指示指定上下文是否满足上下文特性的要求。</summary>
    /// <returns>如果传入的上下文一切正常，则为 true；否则为 false。</returns>
    /// <param name="ctx">当前上下文特性检查所依据的上下文。</param>
    /// <param name="msg">构造调用，需要依据当前上下文检查其参数。</param>
    [SecurityCritical]
    bool IsContextOK(Context ctx, IConstructionCallMessage msg);

    /// <summary>在给定消息中将上下文属性返回给调用方。</summary>
    /// <param name="msg">将上下文属性添加到的 <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />。</param>
    [SecurityCritical]
    void GetPropertiesForNewContext(IConstructionCallMessage msg);
  }
}
