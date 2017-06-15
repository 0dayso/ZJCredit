// Decompiled with JetBrains decompiler
// Type: System.Runtime.Versioning.ComponentGuaranteesAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Versioning
{
  /// <summary>定义可以跨多个版本的组件、类型或类型成员的兼容性保证。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
  public sealed class ComponentGuaranteesAttribute : Attribute
  {
    private ComponentGuaranteesOptions _guarantees;

    /// <summary>获取一个值，该值指示保证的跨多个版本的库、类型或类型成员的兼容性级别。</summary>
    /// <returns>用于指定保证的跨多个版本的兼容性级别的枚举值之一。</returns>
    public ComponentGuaranteesOptions Guarantees
    {
      get
      {
        return this._guarantees;
      }
    }

    /// <summary>使用一个指示某个库、类型或成员保证的跨多个版本的兼容性级别的值初始化 <see cref="T:System.Runtime.Versioning.ComponentGuaranteesAttribute" /> 类的新实例。</summary>
    /// <param name="guarantees">用于指定保证的跨多个版本的兼容性级别的枚举值之一。</param>
    public ComponentGuaranteesAttribute(ComponentGuaranteesOptions guarantees)
    {
      this._guarantees = guarantees;
    }
  }
}
