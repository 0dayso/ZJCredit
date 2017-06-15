// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComImportAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.InteropServices
{
  /// <summary>指示该属性化类型是以前在 COM 中定义的。</summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class ComImportAttribute : Attribute
  {
    /// <summary>初始化 <see cref="T:System.Runtime.InteropServices.ComImportAttribute" /> 的新实例。</summary>
    [__DynamicallyInvokable]
    public ComImportAttribute()
    {
    }

    internal static Attribute GetCustomAttribute(RuntimeType type)
    {
      if ((type.Attributes & TypeAttributes.Import) == TypeAttributes.NotPublic)
        return (Attribute) null;
      return (Attribute) new ComImportAttribute();
    }

    internal static bool IsDefined(RuntimeType type)
    {
      return (uint) (type.Attributes & TypeAttributes.Import) > 0U;
    }
  }
}
