// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.TokenImpersonationLevel
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Principal
{
  /// <summary>定义安全模拟级别。安全模拟级别控制服务器进程可以在何种程度上代表客户端进程执行操作。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum TokenImpersonationLevel
  {
    [__DynamicallyInvokable] None,
    [__DynamicallyInvokable] Anonymous,
    [__DynamicallyInvokable] Identification,
    [__DynamicallyInvokable] Impersonation,
    [__DynamicallyInvokable] Delegation,
  }
}
