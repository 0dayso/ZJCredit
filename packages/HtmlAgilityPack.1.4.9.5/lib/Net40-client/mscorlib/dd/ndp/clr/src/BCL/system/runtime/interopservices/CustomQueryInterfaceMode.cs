// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.CustomQueryInterfaceMode
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>指示 <see cref="M:System.Runtime.InteropServices.Marshal.GetComInterfaceForObject(System.Object,System.Type,System.Runtime.InteropServices.CustomQueryInterfaceMode)" /> 方法的 IUnknown::QueryInterface 调用是否可以使用 <see cref="T:System.Runtime.InteropServices.ICustomQueryInterface" /> 接口。</summary>
  [__DynamicallyInvokable]
  [Serializable]
  public enum CustomQueryInterfaceMode
  {
    [__DynamicallyInvokable] Ignore,
    [__DynamicallyInvokable] Allow,
  }
}
