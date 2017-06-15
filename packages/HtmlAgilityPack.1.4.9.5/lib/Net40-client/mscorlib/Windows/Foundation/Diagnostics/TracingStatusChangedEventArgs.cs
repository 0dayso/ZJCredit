// Decompiled with JetBrains decompiler
// Type: Windows.Foundation.Diagnostics.TracingStatusChangedEventArgs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Windows.Foundation.Diagnostics
{
  [Guid("410B7711-FF3B-477F-9C9A-D2EFDA302DC3")]
  [ComImport]
  internal sealed class TracingStatusChangedEventArgs : ITracingStatusChangedEventArgs
  {
    public bool Enabled { [MethodImpl(MethodImplOptions.InternalCall)] get; }

    public CausalityTraceLevel TraceLevel { [MethodImpl(MethodImplOptions.InternalCall)] get; }

    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public TracingStatusChangedEventArgs();
  }
}
