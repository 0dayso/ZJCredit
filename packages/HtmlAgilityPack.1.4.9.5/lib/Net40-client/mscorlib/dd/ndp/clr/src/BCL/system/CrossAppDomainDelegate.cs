// Decompiled with JetBrains decompiler
// Type: System.CrossAppDomainDelegate
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>由 <see cref="M:System.AppDomain.DoCallBack(System.CrossAppDomainDelegate)" /> 使用，用于跨应用程序域的调用。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  public delegate void CrossAppDomainDelegate();
}
