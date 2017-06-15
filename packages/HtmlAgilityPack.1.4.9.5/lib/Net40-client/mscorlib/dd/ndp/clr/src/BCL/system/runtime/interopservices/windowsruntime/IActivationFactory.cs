// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.IActivationFactory
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.WindowsRuntime
{
  /// <summary>使类被 Windows 运行时 激活。</summary>
  [Guid("00000035-0000-0000-C000-000000000046")]
  [__DynamicallyInvokable]
  [ComImport]
  public interface IActivationFactory
  {
    /// <summary>返回 Windows 运行时 类的新实例，该实例由 <see cref="T:System.Runtime.InteropServices.WindowsRuntime.IActivationFactory" /> 接口创建。</summary>
    /// <returns>Windows 运行时 类的该新实例。</returns>
    [__DynamicallyInvokable]
    object ActivateInstance();
  }
}
