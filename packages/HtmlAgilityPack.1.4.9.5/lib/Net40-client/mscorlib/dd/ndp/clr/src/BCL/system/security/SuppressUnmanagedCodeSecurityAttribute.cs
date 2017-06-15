// Decompiled with JetBrains decompiler
// Type: System.Security.SuppressUnmanagedCodeSecurityAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security
{
  /// <summary>允许托管代码不经过堆栈步即调入非托管代码。此类不能被继承。</summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  public sealed class SuppressUnmanagedCodeSecurityAttribute : Attribute
  {
  }
}
