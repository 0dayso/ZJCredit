// Decompiled with JetBrains decompiler
// Type: System.Collections.Concurrent.EnumerablePartitionerOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections.Concurrent
{
  /// <summary>指定控制分区程序的缓冲行为的选项</summary>
  [Flags]
  [__DynamicallyInvokable]
  [Serializable]
  public enum EnumerablePartitionerOptions
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] NoBuffering = 1,
  }
}
