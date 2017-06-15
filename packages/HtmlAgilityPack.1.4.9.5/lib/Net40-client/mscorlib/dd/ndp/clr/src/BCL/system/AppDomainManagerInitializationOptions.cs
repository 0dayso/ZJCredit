// Decompiled with JetBrains decompiler
// Type: System.AppDomainManagerInitializationOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>指定在初始化一个新域时自定义应用程序域管理器应采取的操作。</summary>
  /// <filterpriority>2</filterpriority>
  [Flags]
  [ComVisible(true)]
  public enum AppDomainManagerInitializationOptions
  {
    None = 0,
    RegisterWithHost = 1,
  }
}
