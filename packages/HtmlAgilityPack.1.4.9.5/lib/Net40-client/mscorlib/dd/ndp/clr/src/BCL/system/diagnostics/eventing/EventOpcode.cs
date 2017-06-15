// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventOpcode
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
  /// <summary>定义标准操作代码，事件源将其添加到事件。</summary>
  [FriendAccessAllowed]
  [__DynamicallyInvokable]
  public enum EventOpcode
  {
    [__DynamicallyInvokable] Info = 0,
    [__DynamicallyInvokable] Start = 1,
    [__DynamicallyInvokable] Stop = 2,
    [__DynamicallyInvokable] DataCollectionStart = 3,
    [__DynamicallyInvokable] DataCollectionStop = 4,
    [__DynamicallyInvokable] Extension = 5,
    [__DynamicallyInvokable] Reply = 6,
    [__DynamicallyInvokable] Resume = 7,
    [__DynamicallyInvokable] Suspend = 8,
    [__DynamicallyInvokable] Send = 9,
    [__DynamicallyInvokable] Receive = 240,
  }
}
