// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.IDispatchImplType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>指示对特定类使用何种 IDispatch 实现。</summary>
  [Obsolete("The IDispatchImplAttribute is deprecated.", false)]
  [ComVisible(true)]
  [Serializable]
  public enum IDispatchImplType
  {
    SystemDefinedImpl,
    InternalImpl,
    CompatibleImpl,
  }
}
