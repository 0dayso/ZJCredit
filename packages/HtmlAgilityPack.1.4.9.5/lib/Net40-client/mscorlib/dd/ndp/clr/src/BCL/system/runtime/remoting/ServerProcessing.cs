﻿// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.ServerProcessing
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>指示服务器消息处理的状态。</summary>
  [ComVisible(true)]
  [Serializable]
  public enum ServerProcessing
  {
    Complete,
    OneWay,
    Async,
  }
}