// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.CustomQueryInterfaceResult
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>提供 <see cref="M:System.Runtime.InteropServices.ICustomQueryInterface.GetInterface(System.Guid@,System.IntPtr@)" /> 方法的返回值。</summary>
  [ComVisible(false)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum CustomQueryInterfaceResult
  {
    [__DynamicallyInvokable] Handled,
    [__DynamicallyInvokable] NotHandled,
    [__DynamicallyInvokable] Failed,
  }
}
