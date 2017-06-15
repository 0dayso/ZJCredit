// Decompiled with JetBrains decompiler
// Type: System.AppDomainInitializer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>表示在应用程序域初始化时要调用的回调方法。</summary>
  /// <param name="args">作为参数传递给回调方法的字符串数组。</param>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [Serializable]
  public delegate void AppDomainInitializer(string[] args);
}
