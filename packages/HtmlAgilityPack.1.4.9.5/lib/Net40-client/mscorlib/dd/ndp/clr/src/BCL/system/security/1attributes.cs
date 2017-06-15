// Decompiled with JetBrains decompiler
// Type: System.Security.SecurityCriticalAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security
{
  /// <summary>指定代码或程序集执行安全性关键型操作。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class SecurityCriticalAttribute : Attribute
  {
    private SecurityCriticalScope _val;

    /// <summary>获取特性的范围。</summary>
    /// <returns>用于指定特性范围的枚举值之一。默认为 <see cref="F:System.Security.SecurityCriticalScope.Explicit" />，指示特性仅适用于直接目标。</returns>
    [Obsolete("SecurityCriticalScope is only used for .NET 2.0 transparency compatibility.")]
    public SecurityCriticalScope Scope
    {
      get
      {
        return this._val;
      }
    }

    /// <summary>初始化 <see cref="T:System.Security.SecurityCriticalAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public SecurityCriticalAttribute()
    {
    }

    /// <summary>用指定范围初始化 <see cref="T:System.Security.SecurityCriticalAttribute" /> 类的新实例。</summary>
    /// <param name="scope">用于指定特性范围的枚举值之一。</param>
    public SecurityCriticalAttribute(SecurityCriticalScope scope)
    {
      this._val = scope;
    }
  }
}
