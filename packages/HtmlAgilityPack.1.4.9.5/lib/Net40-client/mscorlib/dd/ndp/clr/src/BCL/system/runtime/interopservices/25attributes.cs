// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.OutAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.InteropServices
{
  /// <summary>指示应将数据从被调用方封送回调用方。</summary>
  [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class OutAttribute : Attribute
  {
    /// <summary>初始化 <see cref="T:System.Runtime.InteropServices.OutAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public OutAttribute()
    {
    }

    internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
    {
      if (!parameter.IsOut)
        return (Attribute) null;
      return (Attribute) new OutAttribute();
    }

    internal static bool IsDefined(RuntimeParameterInfo parameter)
    {
      return parameter.IsOut;
    }
  }
}
