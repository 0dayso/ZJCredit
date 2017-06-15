// Decompiled with JetBrains decompiler
// Type: System.DateTimeKind
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>指定 <see cref="T:System.DateTime" /> 对象是表示本地时间、协调通用时间 (UTC)，还是既不指定为本地时间，也不指定为 UTC。</summary>
  /// <filterpriority>2</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum DateTimeKind
  {
    [__DynamicallyInvokable] Unspecified,
    [__DynamicallyInvokable] Utc,
    [__DynamicallyInvokable] Local,
  }
}
