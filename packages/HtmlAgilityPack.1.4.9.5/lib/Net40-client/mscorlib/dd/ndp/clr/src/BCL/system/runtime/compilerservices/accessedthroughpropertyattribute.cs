// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.AccessedThroughPropertyAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
  /// <summary>指定访问属性化字段的属性的名称。</summary>
  [AttributeUsage(AttributeTargets.Field)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AccessedThroughPropertyAttribute : Attribute
  {
    private readonly string propertyName;

    /// <summary>获取用于访问属性化字段的属性的名称。</summary>
    /// <returns>用于访问属性化字段的属性的名称。</returns>
    [__DynamicallyInvokable]
    public string PropertyName
    {
      [__DynamicallyInvokable] get
      {
        return this.propertyName;
      }
    }

    /// <summary>使用用于访问属性化字段的属性的名称初始化 AccessedThroughPropertyAttribute 类的新实例。</summary>
    /// <param name="propertyName">用于访问属性化字段的属性的名称。</param>
    [__DynamicallyInvokable]
    public AccessedThroughPropertyAttribute(string propertyName)
    {
      this.propertyName = propertyName;
    }
  }
}
