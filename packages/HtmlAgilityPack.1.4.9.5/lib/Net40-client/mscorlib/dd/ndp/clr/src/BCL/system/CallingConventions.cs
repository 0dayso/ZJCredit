// Decompiled with JetBrains decompiler
// Type: System.Reflection.CallingConventions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>定义方法的有效调用约定。</summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum CallingConventions
  {
    [__DynamicallyInvokable] Standard = 1,
    [__DynamicallyInvokable] VarArgs = 2,
    [__DynamicallyInvokable] Any = VarArgs | Standard,
    [__DynamicallyInvokable] HasThis = 32,
    [__DynamicallyInvokable] ExplicitThis = 64,
  }
}
