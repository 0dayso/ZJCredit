// Decompiled with JetBrains decompiler
// Type: Windows.Foundation.Diagnostics.ITracingStatusChangedEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace Windows.Foundation.Diagnostics
{
  [Guid("410B7711-FF3B-477F-9C9A-D2EFDA302DC3")]
  [ComImport]
  internal interface ITracingStatusChangedEventArgs
  {
    bool Enabled { get; }

    CausalityTraceLevel TraceLevel { get; }
  }
}
