// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.PrincipalPolicy
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Principal
{
  /// <summary>指定应该如何为应用程序域创建用户和标识对象。默认值为 UnauthenticatedPrincipal。</summary>
  [ComVisible(true)]
  [Serializable]
  public enum PrincipalPolicy
  {
    UnauthenticatedPrincipal,
    NoPrincipal,
    WindowsPrincipal,
  }
}
