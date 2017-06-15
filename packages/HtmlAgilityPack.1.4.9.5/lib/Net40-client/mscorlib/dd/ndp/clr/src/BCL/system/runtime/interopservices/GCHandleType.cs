// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.GCHandleType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>表示 <see cref="T:System.Runtime.InteropServices.GCHandle" /> 类可以分配的句柄的类型。</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum GCHandleType
  {
    [__DynamicallyInvokable] Weak,
    [__DynamicallyInvokable] WeakTrackResurrection,
    [__DynamicallyInvokable] Normal,
    [__DynamicallyInvokable] Pinned,
  }
}
