// Decompiled with JetBrains decompiler
// Type: System.Reflection.ParameterAttributes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>定义可与参数关联的属性。这些属性在 CorHdr.h 中定义。</summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum ParameterAttributes
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] In = 1,
    [__DynamicallyInvokable] Out = 2,
    [__DynamicallyInvokable] Lcid = 4,
    [__DynamicallyInvokable] Retval = 8,
    [__DynamicallyInvokable] Optional = 16,
    ReservedMask = 61440,
    [__DynamicallyInvokable] HasDefault = 4096,
    [__DynamicallyInvokable] HasFieldMarshal = 8192,
    Reserved3 = 16384,
    Reserved4 = 32768,
  }
}
