// Decompiled with JetBrains decompiler
// Type: System.FlagsAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>指示可以将枚举作为位域（即一组标志）处理。</summary>
  /// <filterpriority>1</filterpriority>
  [AttributeUsage(AttributeTargets.Enum, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class FlagsAttribute : Attribute
  {
    /// <summary>初始化 <see cref="T:System.FlagsAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public FlagsAttribute()
    {
    }
  }
}
