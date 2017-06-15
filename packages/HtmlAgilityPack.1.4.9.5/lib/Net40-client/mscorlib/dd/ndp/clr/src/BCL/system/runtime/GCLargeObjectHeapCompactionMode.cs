// Decompiled with JetBrains decompiler
// Type: System.Runtime.GCLargeObjectHeapCompactionMode
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime
{
  /// <summary>[在 .NET Framework 4.5.1 和更高版本中受支持]指示下一个阻塞垃圾回收是否压缩大型对象堆 (LOH)。</summary>
  [__DynamicallyInvokable]
  [Serializable]
  public enum GCLargeObjectHeapCompactionMode
  {
    [__DynamicallyInvokable] Default = 1,
    [__DynamicallyInvokable] CompactOnce = 2,
  }
}
