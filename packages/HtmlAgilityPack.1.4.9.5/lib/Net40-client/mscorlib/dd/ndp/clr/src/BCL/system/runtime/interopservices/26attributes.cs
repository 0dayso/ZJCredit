// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.OptionalAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.InteropServices
{
  /// <summary>指示参数是可选的。</summary>
  [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class OptionalAttribute : Attribute
  {
    /// <summary>使用默认值初始化 OptionalAttribute 类的新实例。</summary>
    [__DynamicallyInvokable]
    public OptionalAttribute()
    {
    }

    internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
    {
      if (!parameter.IsOptional)
        return (Attribute) null;
      return (Attribute) new OptionalAttribute();
    }

    internal static bool IsDefined(RuntimeParameterInfo parameter)
    {
      return parameter.IsOptional;
    }
  }
}
