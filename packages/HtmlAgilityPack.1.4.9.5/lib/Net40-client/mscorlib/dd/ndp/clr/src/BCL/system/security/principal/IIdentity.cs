// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.IIdentity
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Principal
{
  /// <summary>定义标识对象的基本功能。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IIdentity
  {
    /// <summary>获取当前用户的名称。</summary>
    /// <returns>用户名，代码当前即以该用户的名义运行。</returns>
    [__DynamicallyInvokable]
    string Name { [__DynamicallyInvokable] get; }

    /// <summary>获取所使用的身份验证的类型。</summary>
    /// <returns>用于标识用户的身份验证的类型。</returns>
    [__DynamicallyInvokable]
    string AuthenticationType { [__DynamicallyInvokable] get; }

    /// <summary>获取一个值，该值指示是否验证了用户。</summary>
    /// <returns>如果用户已经过验证，则为 true；否则为 false。</returns>
    [__DynamicallyInvokable]
    bool IsAuthenticated { [__DynamicallyInvokable] get; }
  }
}
