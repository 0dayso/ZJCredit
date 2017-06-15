// Decompiled with JetBrains decompiler
// Type: System.Security.SecurityTransparentAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security
{
  /// <summary>指定程序集无法引起特权提升。</summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class SecurityTransparentAttribute : Attribute
  {
    /// <summary>初始化 <see cref="T:System.Security.SecurityTransparentAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public SecurityTransparentAttribute()
    {
    }
  }
}
