// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.StreamingContextStates
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>定义一个标记集，用于在序列化过程中指定流的源或目标上下文。</summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum StreamingContextStates
  {
    CrossProcess = 1,
    CrossMachine = 2,
    File = 4,
    Persistence = 8,
    Remoting = 16,
    Other = 32,
    Clone = 64,
    CrossAppDomain = 128,
    All = CrossAppDomain | Clone | Other | Remoting | Persistence | File | CrossMachine | CrossProcess,
  }
}
