// Decompiled with JetBrains decompiler
// Type: System.Security.IStackWalk
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security
{
  /// <summary>对堆栈遍历进行管理。</summary>
  [ComVisible(true)]
  public interface IStackWalk
  {
    /// <summary>断言调用代码能访问当前权限对象所标识的资源，即使堆栈中的高级调用方未被授权访问该资源。</summary>
    /// <exception cref="T:System.Security.SecurityException">调用代码没有 <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Assertion" />。</exception>
    void Assert();

    /// <summary>在运行时确定调用堆栈中的所有调用方是否已被授予当前权限对象所指定的权限。</summary>
    /// <exception cref="T:System.Security.SecurityException">调用堆栈中的某个处于较高位置的调用方不拥有当前权限对象所指定的权限。- 或 -调用堆栈中的调用方已调用当前权限对象上的 <see cref="M:System.Security.IStackWalk.Deny" />。</exception>
    void Demand();

    /// <summary>导致通过调用代码传递的当前对象的每个 <see cref="M:System.Security.IStackWalk.Demand" /> 失败。</summary>
    void Deny();

    /// <summary>导致除当前对象（通过调用代码来传递）以外的所有对象的每个 <see cref="M:System.Security.IStackWalk.Demand" /> 失败，即使调用堆栈中的高级代码已被授权访问其他资源也是如此。</summary>
    void PermitOnly();
  }
}
