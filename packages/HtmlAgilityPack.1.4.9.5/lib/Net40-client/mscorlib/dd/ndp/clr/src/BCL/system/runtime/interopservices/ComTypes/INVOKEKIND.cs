// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.INVOKEKIND
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>指定如何通过 IDispatch::Invoke 来调用函数。</summary>
  [Flags]
  [__DynamicallyInvokable]
  [Serializable]
  public enum INVOKEKIND
  {
    [__DynamicallyInvokable] INVOKE_FUNC = 1,
    [__DynamicallyInvokable] INVOKE_PROPERTYGET = 2,
    [__DynamicallyInvokable] INVOKE_PROPERTYPUT = 4,
    [__DynamicallyInvokable] INVOKE_PROPERTYPUTREF = 8,
  }
}
