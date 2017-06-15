// Decompiled with JetBrains decompiler
// Type: System.MidpointRounding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>指定数学舍入方法应如何处理两个数字间的中间值。</summary>
  /// <filterpriority>1</filterpriority>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public enum MidpointRounding
  {
    [__DynamicallyInvokable] ToEven,
    [__DynamicallyInvokable] AwayFromZero,
  }
}
