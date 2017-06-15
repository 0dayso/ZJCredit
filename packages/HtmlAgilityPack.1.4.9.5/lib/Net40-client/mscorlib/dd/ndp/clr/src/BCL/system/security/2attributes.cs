// Decompiled with JetBrains decompiler
// Type: System.Security.SecurityTreatAsSafeAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 2A55D587-43EC-479C-866B-425E85A3236D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security
{
  /// <summary>标识哪些非公共 <see cref="T:System.Security.SecurityCriticalAttribute" /> 成员可由程序集中的透明代码访问。</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
  [Obsolete("SecurityTreatAsSafe is only used for .NET 2.0 transparency compatibility.  Please use the SecuritySafeCriticalAttribute instead.")]
  public sealed class SecurityTreatAsSafeAttribute : Attribute
  {
  }
}
