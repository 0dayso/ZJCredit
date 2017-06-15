// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.DisablePrivateReflectionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>表示程序集的类型中包含任何私有成员不可供反射。</summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class DisablePrivateReflectionAttribute : Attribute
  {
    /// <summary>初始化的新实例<see cref="T:System.Runtime.CompilerServices.DisablePrivateReflectionAttribute" />类。 </summary>
    [__DynamicallyInvokable]
    public DisablePrivateReflectionAttribute()
    {
    }
  }
}
