// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.InAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.InteropServices
{
  /// <summary>指示应将数据从调用方封送到被调用方，而不返回到调用方。</summary>
  [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class InAttribute : Attribute
  {
    /// <summary>初始化 <see cref="T:System.Runtime.InteropServices.InAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public InAttribute()
    {
    }

    internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
    {
      if (!parameter.IsIn)
        return (Attribute) null;
      return (Attribute) new InAttribute();
    }

    internal static bool IsDefined(RuntimeParameterInfo parameter)
    {
      return parameter.IsIn;
    }
  }
}
