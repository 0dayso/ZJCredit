// Decompiled with JetBrains decompiler
// Type: System.EventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>表示包含事件数据的类的基类，并提供要用于不包含事件数据的事件的值。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class EventArgs
  {
    /// <summary>提供要用于没有事件数据的事件的值。</summary>
    /// <filterpriority>1</filterpriority>
    [__DynamicallyInvokable]
    public static readonly EventArgs Empty = new EventArgs();

    /// <summary>初始化 <see cref="T:System.EventArgs" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public EventArgs()
    {
    }
  }
}
