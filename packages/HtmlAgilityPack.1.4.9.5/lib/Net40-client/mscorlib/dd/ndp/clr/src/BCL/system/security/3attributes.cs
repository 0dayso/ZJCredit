// Decompiled with JetBrains decompiler
// Type: System.Security.SecuritySafeCriticalAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security
{
  /// <summary>将类型或成员标识为安全关键并且可供透明代码安全访问。</summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class SecuritySafeCriticalAttribute : Attribute
  {
    /// <summary>初始化 <see cref="T:System.Security.SecuritySafeCriticalAttribute" /> 类的新实例。</summary>
    [__DynamicallyInvokable]
    public SecuritySafeCriticalAttribute()
    {
    }
  }
}
