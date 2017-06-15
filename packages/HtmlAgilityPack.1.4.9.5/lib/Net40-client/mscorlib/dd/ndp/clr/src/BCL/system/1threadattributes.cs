// Decompiled with JetBrains decompiler
// Type: System.MTAThreadAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>指示应用程序的 COM 线程模型为多线程单元 (MTA)。</summary>
  /// <filterpriority>1</filterpriority>
  [AttributeUsage(AttributeTargets.Method)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class MTAThreadAttribute : Attribute
  {
    /// <summary>初始化 <see cref="T:System.MTAThreadAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public MTAThreadAttribute()
    {
    }
  }
}
